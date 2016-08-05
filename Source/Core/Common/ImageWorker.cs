/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 09.07.2015
 * Время: 8:55
 * 
 */
using System;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;

using Core.FB2.Binary;

using FilesWorker = Core.Common.FilesWorker;

namespace Core.Common
{
	/// <summary>
	/// ImageWorker: Класс для работы с изображениями
	/// </summary>
	public class ImageWorker
	{
		public ImageWorker()
		{
		}
		
		public static string getContentType( string FilePath ) {
			string Ext = Path.GetExtension( FilePath ).ToLower();
			if( Ext.Equals(".jpg") || Ext.Equals(".jpeg") )
				return "image/jpeg";
			else if( Ext.Equals(".png") )
				return "image/png";
			else
				return null;
		}
		
		// Получение картинки из base64
		public static Image base64ToImage(string Base64String) {
			// Convert Base64 String to byte[]
			byte[] ImageBytes = Convert.FromBase64String( Base64String );
			// Convert byte[] to Image
			MemoryStream ms = new MemoryStream( ImageBytes, 0, ImageBytes.Length );
			ms.Write( ImageBytes, 0, ImageBytes.Length );
			Image image = Image.FromStream( ms, true );
			return image;
		}
		
		public static string toBase64( string FilePath ) {
			byte[] BinaryData;
			try {
				System.IO.FileStream InFile = new System.IO.FileStream( FilePath, System.IO.FileMode.Open, System.IO.FileAccess.Read );
				BinaryData = new Byte[InFile.Length];
				long bytesRead = InFile.Read( BinaryData, 0, (int)InFile.Length );
				InFile.Close();
			} catch (System.Exception) {
				return null;
			}
			
			try {
				return System.Convert.ToBase64String( BinaryData, 0, BinaryData.Length );
			} catch (System.ArgumentNullException) {
				return null; // Binary data array is null
			}
		}
		
		// Создание итемов ListView названий Обложек
		public static int makeListViewCoverNameItems( ListView lv, ref IList<BinaryBase64> Covers ) {
			int ImageCount = 0;
			if ( Covers != null && Covers.Count > 0 ) {
				string Temp = Settings.Settings.TempDirPath;
				if ( !Directory.Exists( Temp ) )
					Directory.CreateDirectory( Temp );

				foreach ( BinaryBase64 Cover in Covers ) {
					if ( Cover != null ) {
						ListViewItem lvi = new ListViewItem( Cover.id );
						lvi.SubItems.Add( Cover.contentType );

						Image image = null;
						try {
							image = ImageWorker.base64ToImage( Cover.base64String );
						} catch ( Exception /*exp*/ ) {
							continue;
						}

						lvi.SubItems.Add( string.Format( "{0} x {1} dpi", image.VerticalResolution, image.HorizontalResolution ) );
						lvi.SubItems.Add( string.Format( "{0} x {1} Pixels", image.Width, image.Height ) );

//						string Type = Cover.contentType.Substring( Cover.contentType.IndexOf( '/' ) + 1 );
						string TempFile =  Path.Combine( Temp, string.Format( "__temp_image__{0}", Cover.id ) );
						
						try {
							image.Save( TempFile );
							FileInfo file = new FileInfo( TempFile );
							lvi.SubItems.Add( FilesWorker.FormatFileLength( file.Length ) );
							if ( File.Exists( TempFile ) )
								File.Delete( TempFile );
						} catch ( System.Exception /*exp*/ ) {
							lvi.SubItems.Add( "? kb" );
						}
						
						lvi.Tag = Cover.base64String;
						lv.Items.Add( lvi );
						++ImageCount;
					}
				}
			}
			return ImageCount;
		}
		
		// сохранение выделенных обложек на диск
		public static void saveSelectedCovers( ListView listView, ref string DirForSavedCover,
		                                      string MessageTitle, FolderBrowserDialog fbdSaveDir ) {
			if( listView.Items.Count > 0 && listView.SelectedItems.Count > 0 ) {
				string TempDesc = fbdSaveDir.Description;
				string TargetDir = FilesWorker.OpenDirDlg( DirForSavedCover, fbdSaveDir, "Укажите папку-приемник для сохранения выбранных обложек:" );
				fbdSaveDir.Description = TempDesc;
				DirForSavedCover = TargetDir;
				if( TargetDir == null )
					return;
				
				string NotSavedCovers = string.Empty;
				foreach ( ListViewItem Item in listView.SelectedItems ) {
					Image image = ImageWorker.base64ToImage( Item.Tag.ToString() );
					try {
						image.Save( Path.Combine( TargetDir, Item.Text.Trim() ) );
					} catch ( System.Exception /*e*/ ) {
						NotSavedCovers += Item.Text.Trim() + "\n";
					}
				}
				if ( !string.IsNullOrEmpty( NotSavedCovers ) )
					MessageBox.Show(
						"Следующие обложки не удалось сохранить корректно (битые):\n" + NotSavedCovers, MessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning
					);
			}
		}
		
	}
}
