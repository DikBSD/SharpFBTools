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
		
		/// <summary>
		/// Распаковка zip архива в Temp папку и получение пути к распакованному файлу
		/// </summary>
		/// <param name="SourceZipFBZPath">Путь к исходному zip или fbz-файлу</param>
		/// <param name="TempDir">Временный каталог для распаковки архива</param>
		/// <returns>Путь к распакованному fb2 файлу из архива, null, если файл не является zip или fbz архивом</returns>
		public static string getFileFromZipFBZ( string SourceZipFBZPath, string TempDir ) {
			if ( File.Exists( SourceZipFBZPath ) ) {
				if ( ! Directory.Exists( TempDir ) )
					Directory.CreateDirectory( TempDir );
				if ( FilesWorker.isFB2Archive( SourceZipFBZPath ) ) {
					return ( m_sharpZipLib.UnZipFB2Files(SourceZipFBZPath, TempDir) > 0 )
						? Directory.GetFiles( TempDir )[0] : null;
				}
			}
			return null;
		}
		
		/// <summary>
		/// Правка fb2 и перепаковка fb2 из zip, fbz
		/// </summary>
		/// <param name="SourceFilePath">Путь к исходному fb2, zip или fbz-файлу</param>
		/// <param name="FB2ProgramPath">Путь к программе правки fb2 файла</param>
		public static void StartFB2_FBZForEdit( string SourceFilePath, string FB2ProgramPath ) {
			Cursor.Current = Cursors.WaitCursor;
			if ( FilesWorker.isFB2Archive( SourceFilePath ) ) {
				string TempFileFromZipPath = getFileFromZipFBZ( SourceFilePath, m_TempDir );
				if ( ! string.IsNullOrWhiteSpace( TempFileFromZipPath ) ) {
					FilesWorker.StartFile( true, FB2ProgramPath, TempFileFromZipPath );
					WorksWithBooks.zipMoveTempFB2FileTo( m_sharpZipLib, TempFileFromZipPath, SourceFilePath );
					FilesWorker.RemoveDir( m_TempDir );
				}
			} else
				FilesWorker.StartFile( true, FB2ProgramPath, SourceFilePath );
			Cursor.Current = Cursors.Default;
		}
		
		/// <summary>
		/// Сравнение 2-х файлов: fb2, fb2 из zip или fb2 из fbz
		/// </summary>
		/// <param name="DiffPath">Путь к diff-программе сравнения fb2 файла</param>
		/// <param name="FB2File1Path">Путь к 1-му исходному fb2, zip или fbz-файлу</param>
		/// <param name="FB2File2Path">Путь ко 2-му исходному fb2, zip или fbz-файлу</param>
		/// <param name="TempDirForFile1">Путь к временной папке для 1-го распакованного fb2 файла </param>
		/// <param name="TempDirForFile2">Путь к временной папке для 2-го распакованного fb2 файла </param>
		public static void DiffFB2( string DiffPath, string FB2File1Path, string FB2File2Path, string TempDirForFile1, string TempDirForFile2 ) {
			Cursor.Current = Cursors.WaitCursor;
			string TempFile1Path = null;
			string TempFile2Path = null;
			
			// создание путей (или распаковка) к исходным fb2 файлам, участвующим в сравнении
			if ( FilesWorker.isFB2File( FB2File1Path ) )
				TempFile1Path = FB2File1Path;
			else if ( FilesWorker.isFB2Archive( FB2File1Path ) )
				TempFile1Path = getFileFromZipFBZ( FB2File1Path, TempDirForFile1 );

			if ( FilesWorker.isFB2File( FB2File2Path ) )
				TempFile2Path = FB2File2Path;
			else if ( FilesWorker.isFB2Archive( FB2File2Path ) )
				TempFile2Path = getFileFromZipFBZ( FB2File2Path, TempDirForFile2 );
			
			// Сравнение файлов
			if ( ! string.IsNullOrWhiteSpace( TempFile1Path ) && ! string.IsNullOrWhiteSpace( TempFile2Path ) )
				FilesWorker.StartDiff( true, DiffPath, TempFile1Path, TempFile2Path );
			
			// завершенн работы (перепаковка файла(ов), если он(они) были извлечены из архива)
			if ( FilesWorker.isFB2Archive( FB2File1Path ) && ! string.IsNullOrWhiteSpace( TempFile1Path ) )
				WorksWithBooks.zipMoveTempFB2FileTo( m_sharpZipLib, TempFile1Path, FB2File1Path );
			if ( FilesWorker.isFB2Archive( FB2File2Path ) && ! string.IsNullOrWhiteSpace( TempFile2Path ) )
				WorksWithBooks.zipMoveTempFB2FileTo( m_sharpZipLib, TempFile2Path, FB2File2Path );

			// удаление временных файлов, если они есть
			FilesWorker.RemoveDir( TempDirForFile1 );
			FilesWorker.RemoveDir( TempDirForFile2 );
			Cursor.Current = Cursors.Default;
		}

	}
}
