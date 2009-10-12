/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 07.09.2009
 * Time: 13:25
 * 
 * License: GPL 2.1
 */
using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using filesWorker		= Core.FilesWorker.FilesWorker;
using archivesWorker	= Core.FilesWorker.Archiver;
using stringProcessing	= Core.StringProcessing.StringProcessing;

namespace Core.Misc
{
	/// <summary>
	/// Description of Misc.
	/// </summary>
	public class MiscListView
	{
		public MiscListView()
		{
		}
		
		#region Работа с отдельными итемами ListView
		// увеличение значения 2-й колонки ListView на 1
		public void IncListViewStatus( ListView lv, int nItem ) {
			lv.Items[nItem].SubItems[1].Text =
				Convert.ToString( 1+Convert.ToInt32( lv.Items[nItem].SubItems[1].Text ) );
		}
		
		// занесение в нужный item определеного значения
		public void ListViewStatus( ListView lv, int nItem, int nValue ) {
			lv.Items[nItem].SubItems[1].Text = Convert.ToString( nValue );
		}
		
		// занесение в нужный item определеного значения
		public void ListViewStatus( ListView lv, int nItem, string sValue ) {
			lv.Items[nItem].SubItems[1].Text = sValue;
		}
		#endregion
		
		#region Пометить / Снять отметки
		// отметить все итемы (снять все отметки)
		public void CheckdAllListViewItems( ListView lv, bool bCheck ) {
			if( lv.Items.Count > 0  ) {
				for( int i=0; i!=lv.Items.Count; ++i ) {
					lv.Items[i].Checked = bCheck;
				}
			}
		}
		// снять отметки с помеченных итемов
		public void UnCheckdAllListViewItems( System.Windows.Forms.ListView.CheckedListViewItemCollection checkedItems) {
			foreach( ListViewItem lvi in checkedItems ) {
				lvi.Checked = false;
			}
		}
		#endregion
		
		#region Копирование(перемещение) или удаление помеченных итемов и файлов, на которые они указывают
		
		// Копирование, перемещение, удаление файлов
		public void CopyOrMoveFilesTo( BackgroundWorker bw, DoWorkEventArgs e,
		                       bool bCopy, string sSource, string sTarget, string sTempDir, 
		                       ListView lvResult,
		                       int nFileExistMode, bool bAddFileNameBookID,
		                       ToolStripStatusLabel tsslblProgress, string sProgressText ) {
			// копировать или переместить файлы в...
			#region Код
			tsslblProgress.Text = sProgressText;
			System.Windows.Forms.ListView.CheckedListViewItemCollection checkedItems = lvResult.CheckedItems;
			int i=0;
			foreach( ListViewItem lvi in checkedItems ) {
				// Проверить флаг на остановку процесса 
				if( ( bw.CancellationPending == true ) ) {
					e.Cancel = true; // Выставить окончание - по отмене, сработает событие bwcmd_RunWorkerCompleted
					return;
				} else {
					string sFilePath = lvi.Text;
					string sNewPath = sTarget + sFilePath.Remove( 0, sSource.Length );
					FileInfo fi = new FileInfo( sNewPath );
					if( !fi.Directory.Exists ) {
						Directory.CreateDirectory( fi.Directory.ToString() );
					}
					string sSufix = "";
					if( File.Exists( sNewPath ) ) {
						if( nFileExistMode==0 ) {
							File.Delete( sNewPath );
						} else {
							if( bAddFileNameBookID ) {
								string sExtTemp = Path.GetExtension( sFilePath ).ToLower();
								if( sExtTemp != ".fb2" ) {
									filesWorker.RemoveDir( sTempDir );
//									Directory.CreateDirectory( sTempDir );
									if( sExtTemp.ToLower() == ".rar" ) {
										archivesWorker.unrar( Settings.Settings.ReadUnRarPath(), sFilePath, sTempDir, ProcessPriorityClass.AboveNormal );
									} else {
										archivesWorker.unzip( Settings.Settings.Read7zaPath(), sFilePath, sTempDir, ProcessPriorityClass.AboveNormal );
									}
									if( Directory.Exists( sTempDir ) ) {
										string [] files = Directory.GetFiles( sTempDir );
										try {
											sSufix = "_" + stringProcessing.GetBookID( files[0] );
										} catch { }
										filesWorker.RemoveDir( sTempDir );
									}
								} else {
									try {
										sSufix = "_" + stringProcessing.GetBookID( sFilePath );
									} catch { }
								}
							}
							if( nFileExistMode == 1 ) {
								// Добавить к создаваемому файлу очередной номер
								sSufix += "_" + stringProcessing.GetFileNewNumber( sNewPath ).ToString();
							} else {
								// Добавить к создаваемому файлу дату и время
								sSufix += "_" + stringProcessing.GetDateTimeExt();
							}
						
							string sFB2File = sNewPath.ToLower();
							if( sFB2File.IndexOf( ".fb2" )!=1 ) {
								sFB2File = sFB2File.Substring( 0, sFB2File.IndexOf( ".fb2" )+4 );
							}
							string sExt = sNewPath.Remove( 0, sFB2File.Length );
							if( sExt.Length == 0 ) {
								sExt = Path.GetExtension( sNewPath );
								sNewPath = sNewPath.Remove( sNewPath.Length - sExt.Length ) + sSufix + sExt;
							} else {
								sExt = Path.GetExtension( sFB2File ) + Path.GetExtension( sNewPath );
								sNewPath = sNewPath.Remove( sNewPath.Length - sExt.Length ) + sSufix + sExt;
							}
						}
					}
				
					Regex rx = new Regex( @"\\+" );
					sFilePath = rx.Replace( sFilePath, "\\" );
					if( File.Exists( sFilePath ) ) {
						if( bCopy ) {
							File.Copy( sFilePath, sNewPath );
						} else {
							File.Move( sFilePath, sNewPath );
							ListViewGroup lvg = lvi.Group;
							lvResult.Items.Remove( lvi );
							if( lvg.Items.Count == 0 )
								lvResult.Groups.Remove( lvg );
						}
					}
					bw.ReportProgress( ++i ); // отобразим данные в контролах
				}
			}
			#endregion
		}
		
		public void DeleteFiles( BackgroundWorker bw, DoWorkEventArgs e,
		                 ListView lvResult,
		                 ToolStripStatusLabel tsslblProgress, string sProgressText ) {
			// удалить помеченные файлы...
			#region Код
			tsslblProgress.Text = sProgressText;
			System.Windows.Forms.ListView.CheckedListViewItemCollection checkedItems = lvResult.CheckedItems;
			int i=0;
			foreach( ListViewItem lvi in checkedItems ) {
				// Проверить флаг на остановку процесса 
				if( ( bw.CancellationPending == true ) ) {
					e.Cancel = true; // Выставить окончание - по отмене, сработает событие bwcmd_RunWorkerCompleted
					return;
				} else {
					string sFilePath = lvi.Text;
					if( File.Exists( sFilePath) ) {
						File.Delete( sFilePath );
					}
					
					ListViewGroup lvg = lvi.Group;
					lvResult.Items.Remove( lvi );
					if( lvg.Items.Count == 0 )
						lvResult.Groups.Remove( lvg );
					
					bw.ReportProgress( ++i ); // отобразим данные в контролах
				}
			}
			#endregion
		}
		#endregion

	}
}
