/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 13.05.2014
 * Time: 7:40
 * 
 * License: GPL 2.1
 */
using System;
using System.IO;
using System.Windows.Forms;

using FilesWorker		= Core.Common.FilesWorker;
using SharpZipLibWorker = Core.Common.SharpZipLibWorker;

namespace Core.Common
{
	/// <summary>
	/// ZipFB2Validation: валидация fb2.zip, fbz и fb2
	/// </summary>
	public class ZipFB2Worker
	{
		#region Закрытые данные класса
		private readonly static string				m_TempDir		= Settings.Settings.TempDir;
		private readonly static SharpZipLibWorker	m_sharpZipLib	= new SharpZipLibWorker();
		#endregion
		
		public ZipFB2Worker()
		{
		}
		
		// правка fb2 и перепаковка fb2 из zip, fbz
		public static void StartFB2_FBZForEdit( string FilePath, string sFB2ProgrammPath, string sTitle ) {
			// правка fb2 и перепаковка fb2 из zip, fbz
			Cursor.Current = Cursors.WaitCursor;
			string SourceFile = FilePath;
			bool IsFromZip = getFileFromFB2_FB2Z( ref FilePath, m_TempDir );
			FilesWorker.StartFile( sFB2ProgrammPath, FilePath );
			
			// обработка исправленного файла-архива
			WorksWithBooks.zipMoveTempFB2FileTo( m_sharpZipLib, SourceFile, IsFromZip, FilePath );
			FilesWorker.RemoveDir( m_TempDir );
			Cursor.Current = Cursors.Default;
		}
		
		// сравнение 2-х файлов: fb2, fb2 из zip или fb2 из fbz
		public static void DiffFB2( string DiffPath, string FB2File1Path, string FB2File2Path, string TempDirForFile1, string TempDirForFile2 ) {
			Cursor.Current = Cursors.WaitCursor;
			string SourceFile1Path = FB2File1Path;
			string SourceFile2Path = FB2File2Path;
			// смотрим 1-й файл - если это архив - распаковываем во временную папку
			bool IsFromZip1 = getFileFromFB2_FB2Z( ref FB2File1Path, TempDirForFile1 );
			// смотрим 2-й файл - если это архив - распаковываем во временную папку
			bool IsFromZip2 = getFileFromFB2_FB2Z( ref FB2File2Path, TempDirForFile2 );
			FilesWorker.StartDiff( DiffPath, FB2File1Path, FB2File2Path );

			// обработка исправленного(ых) файла(ов)-архива(ов)
			WorksWithBooks.zipMoveTempFB2FileTo( m_sharpZipLib, SourceFile1Path, IsFromZip1, FB2File1Path );
			WorksWithBooks.zipMoveTempFB2FileTo( m_sharpZipLib, SourceFile2Path, IsFromZip2, FB2File2Path );
			
			FilesWorker.RemoveDir( TempDirForFile1 );
			FilesWorker.RemoveDir( TempDirForFile2 );
			Cursor.Current = Cursors.Default;
		}
		
		/// <summary>
		/// Распаковка zip архива в Temp папку
		/// </summary>
		/// <param name="FilePath">Путь к исходному zip-файлу</param>
		/// <param name="TempDir">Временный каталог для распаковки архива</param>
		/// <returns>Если FilePath - архив, то извлекает файл из него в Temp папку и возвращает путь и признак архива (true). Иначе - возвращает исходный путь и признак, что файл - не из архива (false)</returns>
		public static bool getFileFromFB2_FB2Z( ref string FilePath, string TempDir ) {
			if( File.Exists( FilePath ) ) {
				string Ext = Path.GetExtension( FilePath ).ToLower();
				if( Ext == ".zip" || Ext == ".fbz" ) {
					if ( m_sharpZipLib.UnZipFiles(FilePath, TempDir, 0, false, null, 4096) == -1 )
						return false;
					else {
						FilePath = Directory.GetFiles( TempDir )[0];
						return true;
					}
				}
			}
			return false;
		}
	}
}
