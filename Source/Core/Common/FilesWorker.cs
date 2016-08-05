/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 08.03.2009
 * Time: 17:06
 * 
 * License: GPL 2.1
 */

using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Core.Common
{
	/// <summary>
	/// класс для работы с файлами/папками
	/// </summary>
	public class FilesWorker
	{
		private static ulong m_ulDateCount = 0;

		public FilesWorker()
		{
		}
		
		#region Открытые статические методы класса
		
		// сброс для списка файлов аттрибута только для чтения
		public static void DumpReadOnlyAttrForFiles( string sDir ) {
			if( Directory.Exists( sDir ) ) {
				DirectoryInfo diFolder = new DirectoryInfo( sDir );
				foreach ( FileInfo fiNextFile in diFolder.GetFiles() ) {
					string s = Path.Combine( diFolder.FullName, fiNextFile.ToString() );
					if ( ( File.GetAttributes( s ) & FileAttributes.ReadOnly ) == FileAttributes.ReadOnly )
						File.SetAttributes( s, FileAttributes.Normal ) ;
				}
			}
		}
		
		// удалить содержимое папки sDir и все ее подпапки и файлы
		public static void _RemoveDir( string sDir ) {
			if( Directory.Exists( sDir ) ) {
				DirectoryInfo diFolder = new DirectoryInfo( sDir );
				foreach ( FileInfo fiNextFile in diFolder.GetFiles() ) {
					string s = Path.Combine( diFolder.FullName, fiNextFile.ToString() );
					if ( ( File.GetAttributes( s ) & FileAttributes.ReadOnly ) == FileAttributes.ReadOnly )
						File.SetAttributes( s, FileAttributes.Normal ) ;
					File.Delete( s );
				}
			}
		}
		
		// удалить папку sDir и все ее подпапки и файлы
		public static void RemoveDir( string sDir ) {
			if( Directory.Exists( sDir ) ) {
				DumpReadOnlyAttrForFiles( sDir );
				Directory.Delete( sDir, true );
			}
		}
		
		public static string ShowDir( bool RunSync, System.Windows.Forms.ListView lw ) {
			ListView.SelectedListViewItemCollection si = lw.SelectedItems;
			FileInfo fi = new FileInfo( si[0].SubItems[0].Text.Split('/')[0] );
			CommandManager manag = new CommandManager();
			return manag.Run( RunSync, "c:\\WINDOWS\\explorer.exe", "\""+fi.Directory.ToString()+"\"", ProcessWindowStyle.Maximized, Priority.GetPriority( "Средний" ) );
		}
		
		public static string ShowDir( bool RunSync, string sDir ) {
			CommandManager manag = new CommandManager();
			return manag.Run( RunSync, "c:\\WINDOWS\\explorer.exe", "\""+sDir+"\"", ProcessWindowStyle.Maximized, Priority.GetPriority( "Средний" ) );
		}
		
		public static string StartFile( bool RunSync, string sProgramPath, System.Windows.Forms.ListView lw ) {
			ListView.SelectedListViewItemCollection si = lw.SelectedItems;
			CommandManager manag = new CommandManager();
			return manag.Run( RunSync, "\""+sProgramPath+"\"", "\""+si[0].SubItems[0].Text.Split('/')[0]+"\"", ProcessWindowStyle.Maximized, Priority.GetPriority( "Средний" ) );
		}
		
		public static string StartFile( bool RunSync, string sProgramPath, string sStartFilePath ) {
			CommandManager manag = new CommandManager();
			return manag.Run( RunSync, "\""+sProgramPath+"\"", "\""+sStartFilePath+"\"", ProcessWindowStyle.Maximized, Priority.GetPriority( "Средний" ) );
		}
		
		public static string StartDiff( bool RunSync, string sProgramPath, string sPath1, string sPath2 ) {
			CommandManager manag = new CommandManager();
			return manag.Run( RunSync, "\""+sProgramPath+"\"", "\""+sPath1+"\" \""+sPath2+"\"", ProcessWindowStyle.Maximized, Priority.GetPriority( "Средний" ) );
		}
		
		public static string FormatFileLength( long lLength ) {
			float f = lLength;
			if ( lLength < 1024 )
				return string.Format( "{0:N2} байт", lLength );
			else if( lLength < 1048576 ) // >=1 Мб
				return string.Format( "{0:N2} Кб", (f/1024) );
			else // <=1 Гб
				return string.Format( "{0:N2} Мб", (f/(1024*1024)) );
		}
		
		/// <summary>
		/// задание папки через диалог открытия папки
		/// возвращает: true, если путь к папке выбран; false - если была нажата кнопка отмены
		/// </summary>
		public static bool OpenDirDlg( TextBox tb, FolderBrowserDialog fbd, string sTitle ) {
			fbd.Description = sTitle;
			if ( ! string.IsNullOrEmpty( tb.Text.Trim() ) )
				fbd.SelectedPath = tb.Text.Trim();
			DialogResult result = fbd.ShowDialog();
			if ( result == DialogResult.OK ) {
				string openFolderName = fbd.SelectedPath;
				tb.Text = openFolderName;
				return true;
			}
			return false;
		}
		
		/// <summary>
		/// задание папки через диалог открытия папки
		/// возвращает: путь к выбранной папке, если нажата кнопка OK; null - если была нажата кнопка отмены
		/// </summary>
		public static string OpenDirDlg( string InitDir, FolderBrowserDialog fbd, string sTitle ) {
			fbd.Description = sTitle;
			if ( !string.IsNullOrWhiteSpace( InitDir ) )
				fbd.SelectedPath = InitDir.Trim();
			DialogResult result = fbd.ShowDialog();
			return result == DialogResult.OK ? fbd.SelectedPath : null;
		}
		
		/// <summary>
		/// генерация списка fb2-файлов из папки
		/// возвращает: список fb2-файлов из папки, либо null, если fb2 файлов в архиве не было
		/// </summary>
		/// <param name="sFromDir"> - папка-источник;</param>
		///	<param name="bSort">true - сортировать список;</param>
		///	<param name="bFB2Only">true - список только fb2-файлов</param>
		public static List<string> MakeFileListFromDir( string sFromDir, bool bSort, bool bFB2Only ) {
			List<string> lFilesList = null;
			if ( Directory.Exists( sFromDir ) ) {
				string [] files = Directory.GetFiles( sFromDir );
				if ( files.Length != 0 ) {
					lFilesList = new List<string>();
					int nFB2 = 0;
					foreach ( string sFile in files ) {
						if ( bFB2Only ) {
							if ( FilesWorker.isFB2File( sFile ) ) {
								lFilesList.Add( sFile );
								++nFB2;
							}
						} else {
							lFilesList.Add( sFile );
						}
					}
					if ( bFB2Only && nFB2 == 0 )
						return null;
					if ( bSort )
						lFilesList.Sort();
				}
			}
			return lFilesList;
		}
		
		/// <summary>
		/// генерация списка файлов из папки
		/// возвращает: список fb2-файлов из папки, либо null, если fb2 файлов в архиве не было
		/// </summary>
		/// <param name="sFromDir"> - папка-источник;</param>
		public static List<string> MakeFileListFromDir( string sFromDir ) {
			List<string> lFilesList = null;
			if( Directory.Exists( sFromDir ) ) {
				string [] files = Directory.GetFiles( sFromDir );
				if( files.Length != 0 ) {
					lFilesList = new List<string>();
					lFilesList.AddRange( files );
				}
			}
			return lFilesList;
		}
		
		/// <summary>
		/// генерация списка fb2-файлов из папки
		/// возвращает: список fb2-файлов из папки, либо null, если fb2 файлов в архиве не было
		/// </summary>
		/// <param name="sFromDir"> - папка-источник;</param>
		public static List<string> MakeFilesListFromDirs( BackgroundWorker bw, DoWorkEventArgs e, string sFromDir ) {
			List<string> lFilesList = new List<string>();
			// рабочий список папок - по нему парсим вложенные папки и из него удаляем обработанные
			List<string> lWorkDirList = new List<string>();
			lFilesList.AddRange( Directory.GetFiles( sFromDir ) );
			DirFilesListMaker( sFromDir, ref lWorkDirList, ref lFilesList );
			while( lWorkDirList.Count != 0 ) {
				// перебор папок в указанной папке s
				int nWorkCount = lWorkDirList.Count;
				for( int i=0; i!=nWorkCount; ++i ) {
					if( ( bw.CancellationPending == true ) )  {
						e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
						return null;
					}
					// l - список найденных папок в указанной папке s
					List<string> l = new List<string>();
					DirFilesListMaker( lWorkDirList[i], ref l, ref lFilesList );
					// заносим найденные папки в рабочий список папок
					lWorkDirList.AddRange( l );
				}
				// удаляем из рабочего списка обработанные папки
				lWorkDirList.RemoveRange( 0, nWorkCount );
			}
			return lFilesList;
		}
		
		// *********************************************************************************** //
		/// <summary>
		/// создание списков всех:	1. вложенных папок и их подпапок в заданной папке
		/// 						2. файлов во всех папках и подпапках
		/// возвращает: число всех папок и подпапках в папке для сканирования
		/// </summary>
		/// <param name="sStartDir"> - папка для сканирования;</param>
		/// <param name="lAllDirsList"> - заполняемый список папок в папке сканирования и ее подпапках;</param>
		/// <param name="lAllFilesList"> - заполняемый список файлов во всех подкапках;</param>
		public static int DirsFilesParser( BackgroundWorker bw, DoWorkEventArgs e,
		                                  string sStartDir, ref List<string> lAllDirsList, ref List<string> lAllFilesList ) {
			int nAllDirsCount = 0;
			// рабочий список папок - по нему парсим вложенные папки и из него удаляем обработанные
			List<string> lWorkDirList = new List<string>();
			// начальное заполнение списков
			lAllFilesList.AddRange( Directory.GetFiles( sStartDir ) );
			nAllDirsCount = DirFilesListMaker( sStartDir, ref lWorkDirList, ref lAllFilesList );
			lAllDirsList.Add( sStartDir );
			lAllDirsList.AddRange( lWorkDirList );
			while( lWorkDirList.Count != 0 ) {
				// перебор папок в указанной папке s
				int nWorkCount = lWorkDirList.Count;
				for( int i=0; i!=nWorkCount; ++i ) {
					if( ( bw.CancellationPending == true ) )  {
						e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
						return nAllDirsCount;
					}
					// l - список найденных папок в указанной папке s
					List<string> l = new List<string>();
					nAllDirsCount += DirFilesListMaker( lWorkDirList[i], ref l, ref lAllFilesList );
					// заносим найденные папки в рабочий и полный список папок
					lWorkDirList.AddRange( l );
					lAllDirsList.AddRange( l );
				}
				// удаляем из рабочего списка обработанные папки
				lWorkDirList.RemoveRange( 0, nWorkCount );
			}
			return nAllDirsCount;
		}
		
		
		// *********************************************************************************** //
		// создание списка всех вложенных папок и их подпапок в заданной папке
		// параметры:	sStartDir - папка для сканирования;
		//				lAllDirsList - заполняемый список папок в папке сканирования и ее подпапках
		//				bSort = true - сортировать созданный список папок
		// возвращает: число всех файлов в папке для сканирования и ее подпапках
		public static int DirsParser( BackgroundWorker bw, DoWorkEventArgs e, string sStartDir,
		                             ref ListView lv, ref List<string> lAllDirsList, bool bSort ) {
			int nAllFilesCount = 0;
			// рабочий список папок - по нему парсим вложенные папки и из него удаляем отработанные
			List<string> lWorkDirList = new List<string>();
			// начальное заполнение списков
			nAllFilesCount = Directory.GetFiles( sStartDir ).Length;
			nAllFilesCount += DirListMaker( sStartDir, ref lWorkDirList );
			lAllDirsList.Add( sStartDir );
			lAllDirsList.AddRange( lWorkDirList );
			lv.Items[0].SubItems[1].Text = lAllDirsList.Count.ToString(); // общее число папок
			while( lWorkDirList.Count != 0 ) {
				// перебор папок в указанной папке
				int nWorkCount = lWorkDirList.Count;
				for( int i=0; i!=nWorkCount; ++i ) {
					if( ( bw.CancellationPending == true ) )  {
						e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
						return nAllFilesCount;
					}
					// l - список найденных папок в указанной папке sWD
					List<string> l = new List<string>();
					nAllFilesCount += DirListMaker( lWorkDirList[i], ref l );
					// заносим найденные папки в рабочий и полный список папок
					lWorkDirList.AddRange( l );
					lAllDirsList.AddRange( l );
					lv.Items[0].SubItems[1].Text = lAllDirsList.Count.ToString(); // общее число папок
//					lv.Refresh();
				}
				// удаляем из рабочего списка обработанные папки
				lWorkDirList.RemoveRange( 0, nWorkCount );
			}
			if( bSort )
				lAllDirsList.Sort();

			return nAllFilesCount;
		}

		// обработка строки с папкой
		public static string WorkingDirPath( string sDir ) {
			if( sDir.Length > 3 ) {
				Regex rx = new Regex( @"\\+$" );
				sDir = rx.Replace( sDir, "" );
			} else if( sDir.Length == 2 ) {
				sDir += "\\";
			}
			return sDir;
		}
		
		// проверка на наличие указаной папки и ее создание, если ее нет
		public static bool CreateDirIfNeed( string sDir, string sMessTitle ) {
			if ( !Directory.Exists(sDir) ) {
				string sMess = "Папка-приемник не найдена: " + sDir + ".\nСоздать ее?";
				DialogResult result = MessageBox.Show( sMess, sMessTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question );
				if ( result == DialogResult.No ) {
					MessageBox.Show( "Работа прекращена.", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return false;
				}
				try {
					Directory.CreateDirectory( sDir );
				} catch {
					MessageBox.Show( "Не могу создать папку "+sDir+"!\nРабота прекращена.", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return false;
				}
			}
			return true;
		}
		
		// считывание кодировки файла, если она задана в строке sLine
		public static string getFileEncoding( string sLine ) {
			string sFileEncoding = string.Empty;
			int nStart = sLine.IndexOf( "encoding", StringComparison.CurrentCulture );
			if( nStart!=-1 ) {
				sFileEncoding = sLine.Substring( nStart+8 );
				nStart = sFileEncoding.IndexOf( "\"", StringComparison.CurrentCulture );
				if( nStart!=-1 ) {
					sFileEncoding = sFileEncoding.Substring( nStart+1 );
					nStart = sFileEncoding.IndexOf( "\"", StringComparison.CurrentCulture );
					if( nStart!=-1 ) {
						sFileEncoding = sFileEncoding.Substring( 0, nStart );
						return sFileEncoding;
					}
				}
			}
			return null;
		}
		
		public static string GetDateTimeExt()
		{
			++m_ulDateCount;
			DateTime dt = DateTime.Now;
			return	dt.Year.ToString()+"-"+dt.Month.ToString()+"-"+dt.Day.ToString()+"-"+
				dt.Hour.ToString()+"-"+dt.Minute.ToString()+"-"+dt.Second.ToString()+"-"+
				Convert.ToString( m_ulDateCount );
		}
		
		// обработка последней "." перед \
		public static string RemoveComaBeforeSlash( string sFilePath ){
			string [] sSlash = sFilePath.Split('\\');
			sFilePath = "";
			foreach( string sI in sSlash ) {
				sFilePath += sI.Substring( sI.Length-1, 1  ) == "."
					? sI.Remove( sI.Length-1, 1  )
					: sI;
				sFilePath +=  "\\";
			}
			return sFilePath = sFilePath.Remove( sFilePath.Length-1, 1 );
		}
		
		// номер для нового файла, если уже есть несколько таких же
		public static long GetFileNewNumber( string sFilePath ) {
			Regex rx = new Regex( @"\\+" );
			sFilePath = rx.Replace( sFilePath, "\\" );
			
			string [] files = Directory.GetFiles( Path.GetDirectoryName( sFilePath ) );
			string sFilePathLower = sFilePath.ToLower();
			
			// обработка последней "." перед \
			sFilePathLower = RemoveComaBeforeSlash( sFilePathLower );

			string sFilePathNotExtLower = "";
			
			if( sFilePathLower.IndexOf( ".fb2" )!=-1 ) {
				sFilePathNotExtLower = sFilePathLower.Substring( 0, sFilePathLower.IndexOf( ".fb2" ) );
			} else {
				sFilePathNotExtLower = sFilePathLower.Substring( 0, sFilePathLower.IndexOf( Path.GetExtension( sFilePathLower ) ) );
			}
			string s = sFilePathLower.Substring( 0, sFilePathNotExtLower.Length );
			s = s.Replace( '.', '_' );
			
			long lCount = 0;
			foreach( string sFile in files ) {
				string sIter = sFile.ToLower().Replace( '.', '_' );
				if( sIter.IndexOf( s )!=-1) {
					++lCount;
				}
			}
			return lCount;
		}
		
		/// <summary>
		/// Создание суффикса для файла, для которого есть копии
		/// </summary>
		/// <param name="FilePath">Путь к заданному файлу</param>
		/// <param name="Mode">Режим для суффикса: 0 - замена; 1 - новый номер; 2 - дата</param>
		public static string createSufix( string FilePath, int Mode ) {
			string Sufix = string.Empty;
			if( Mode == 1 ) {
				// Добавить к создаваемому файлу очередной номер
				Sufix = "_" + GetFileNewNumber( FilePath ).ToString();
			} else if( Mode == 2 ) {
				// Добавить к создаваемому файлу дату и время
				Sufix = "_" + GetDateTimeExt();
			}
			return Sufix;
		}
		
		/// <summary>
		/// Создание пути файла с суффиксом
		/// </summary>
		/// <param name="FilePath">Путь к заданному файлу</param>
		/// <param name="Mode">Режим для суффикса: 0 - замена; 1 - новый номер; 2 - дата</param>
		public static string createFilePathWithSufix( string FilePath, int Mode ) {
			string Sufix = createSufix( FilePath, Mode );
			// извлекаем название файла с расширением (для файла .fb2.zip)
			string FB2File = FilePath.ToLower();
			if( FB2File.IndexOf( ".fb2" ) != 1 )
				FB2File = FB2File.Substring( 0, FB2File.IndexOf( ".fb2" ) + 4 );

			string Ext = FilePath.Remove( 0, FB2File.Length );
			Ext = Ext.Length == 0
				? Path.GetExtension( FilePath )
				: Path.GetExtension( FB2File ) + Path.GetExtension( FilePath );
			return FilePath.Remove( FilePath.Length - Ext.Length ) + Sufix + Ext;
		}
		
		/// <summary>
		/// Генерация target-папки файла на основе родительских source-папки, target-папки и исходному пути файла.
		/// Используется для копирования файлов из папки source в папку target с сохранение путей/подпапок
		/// </summary>
		/// <param name="SourceFilePath">Путь к исходному файлу</param>
		/// <param name="RootSourceDir">Родительская Source папка для исходного файла SourceFilePath</param>
		/// <param name="RootTargetDir">Родительская Target папка для копирования исходного файла SourceFilePath</param>
		public static string buildTargetDir( string SourceFilePath, string RootSourceDir, string RootTargetDir) {
			string SourceFileDir = Path.GetDirectoryName( SourceFilePath );
			if (!RootSourceDir.Equals(SourceFileDir))
				SourceFileDir = SourceFileDir.Remove(0,1);
			return Path.Combine(RootTargetDir, SourceFileDir.Remove(0, RootSourceDir.Length));
		}

		#endregion
		
		#region Закрытые вспомогательные методы
		// создание списка подпапок и файлов в заданной папке
		// параметры:	sStartDir - папка для сканирования;
		//				lDirList - заполняемый список папок в текущей папке
		//				lFileList - заполняемый список файлов в текущей папке
		// возвращает: число папок в текущем каталоге
		private static int DirFilesListMaker( string sStartDir, ref List<string> lDirList, ref List<string> lFileList ) {
			int nDirCount = 0;
			// папки в текущей папке
			try {
				string[] dirs = Directory.GetDirectories( sStartDir );
				foreach( string dir in dirs ) {
					try {
						lDirList.Add( dir );
						lFileList.AddRange( Directory.GetFiles( dir ) );
						nDirCount += lDirList.Count;
					} catch { continue; }
				}
			} catch { lDirList.Remove( sStartDir ); }
			return nDirCount;
		}
		
		// создание списка подпапок в заданной папке
		// параметры:	sStartDir - папка для сканирования;
		//				lDirList - заполняемый список папок в текущей папке
		// возвращает: число файлов в текущем каталоге
		private static int DirListMaker( string sStartDir, ref List<string> lDirList ) {
			int nFilesCount = 0;
			// папки в текущей папке
			try {
				string[] dirs = Directory.GetDirectories( sStartDir );
				foreach( string dir in dirs ) {
					try {
						nFilesCount += Directory.GetFiles( dir ).Length;
						lDirList.Add( dir );
					} catch { continue; }
				}
			} catch { lDirList.Remove( sStartDir ); }
			return nFilesCount;
		}
		/*
		private static int DirListMaker( string sStartDir, ref List<string> lDirList ) {
			int nFilesCount = 0;
			// папки в текущей папке
			try {
				DirectoryInfo diFolder = new DirectoryInfo( sStartDir );
				foreach( DirectoryInfo diNextFolder in diFolder.GetDirectories() ) {
					try {
						DirectoryInfo di = new DirectoryInfo( sStartDir + "\\" + diNextFolder.Name );
						nFilesCount += di.GetFiles().Length;
						lDirList.Add( sStartDir + "\\" + diNextFolder.Name );
					} catch { continue; }
				}
			} catch { lDirList.Remove( sStartDir ); }
			return nFilesCount;
		}*/
		#endregion

		/// <summary>
		/// Прповерка является ли файл .fb2.zip или .fbz архивом
		/// </summary>
		/// <param name="FilePath">Путь к проверяемому файлу</param>
		/// <returns>true - если файл - .fb2.zip или .fbz архив; false - если не .fb2.zip или .fbz архив</returns>
		public static bool isFB2Archive( string FilePath ) {
			if ( File.Exists( FilePath ) ) {
				string Extention = Path.GetExtension( FilePath ).ToLower();
				return ( Extention == ".zip" || Extention == ".fbz" ) ? true : false;
			} else
				return false;
		}
		
		/// <summary>
		/// Прповерка является ли файл fb2 файлом
		/// </summary>
		/// <param name="FilePath">Путь к проверяемому файлу</param>
		/// <returns>true - если файл - fb2 файлом; false - если не fb2 файл</returns>
		public static bool isFB2File( string FilePath ) {
			if ( File.Exists( FilePath ) ) {
				return Path.GetExtension( FilePath ).ToLower() == ".fb2" ? true : false;
			} else
				return false;
		}
		
		public static List<string> TraverseTree(string root) {
			List<string> lFilesList = new List<string>();
			Stack<string> dirs = new Stack<string>(20);

			if (!System.IO.Directory.Exists(root)) {
				throw new ArgumentException();
			}
			dirs.Push(root);

			while (dirs.Count > 0) {
				string currentDir = dirs.Pop();
				string[] subDirs;
				try {
					subDirs = System.IO.Directory.GetDirectories(currentDir);
				} catch (UnauthorizedAccessException e) {
					Console.WriteLine(e.Message);
					continue;
				} catch (System.IO.DirectoryNotFoundException e) {
					Console.WriteLine(e.Message);
					continue;
				}

				string[] files = null;
				try {
					files = System.IO.Directory.GetFiles(currentDir);
				} catch (UnauthorizedAccessException e) {
					Console.WriteLine(e.Message);
					continue;
				} catch (System.IO.DirectoryNotFoundException e) {
					Console.WriteLine(e.Message);
					continue;
				}

				lFilesList.AddRange(files);
//				foreach (string file in files) {
//					try {
//						// Perform whatever action is required in your scenario.
//						System.IO.FileInfo fi = new System.IO.FileInfo(file);
//						Console.WriteLine("{0}: {1}, {2}", fi.Name, fi.Length, fi.CreationTime);
//					} catch (System.IO.FileNotFoundException e) {
//						Console.WriteLine(e.Message);
//						continue;
//					}
//				}

				foreach (string str in subDirs)
					dirs.Push(str);
			}
			return lFilesList;
		}
		
		
		// создание списка всех файлов в заданной папке и ее подпапках
		public static int recursionFilesSearch( string sDir, ref List<string> lFilesList, bool sort )
		{
			lFilesList.AddRange(Directory.GetFiles(sDir));
			_recursionFilesSearch( sDir, ref lFilesList );
			if (sort)
				lFilesList.Sort();
			return lFilesList.Count;
		}
		
		// рекурсивный поиск всех файлов в заданной папке и ее подпапках
		public static void _recursionFilesSearch( string sDir, ref List<string> lFilesList )
		{
			try	{
				foreach (string d in Directory.GetDirectories(sDir)) {
					lFilesList.AddRange(Directory.GetFiles(d));
					_recursionFilesSearch(d, ref lFilesList);
				}
			} catch /*(System.Exception excpt)*/ {
				//Console.WriteLine(excpt.Message);
			}
		}
		
		// создание списка всех подпапок в заданной папке, включая и ее
		public static int recursionDirsSearch( string sDir, ref List<string> lDirsList, bool sort )
		{
			lDirsList.Add(sDir);
			_recursionDirsSearch( sDir, ref lDirsList );
			if (sort)
				lDirsList.Sort();
			return lDirsList.Count;
		}
		
		// рекурсивный поиск всех подпапок в заданной папке
		public static void _recursionDirsSearch( string sDir, ref List<string> lDirsList )
		{
			try	{
				string[] dirs = Directory.GetDirectories(sDir);
				lDirsList.AddRange(dirs);
				foreach (string d in dirs)
					_recursionDirsSearch(d, ref lDirsList);
			} catch /*(System.Exception excpt)*/ {
				//Console.WriteLine(excpt.Message);
			}
		}
		
		// создание списка всех файлов по списку папок
		public static int makeFilesListFromDirs( ref List<string> lDirsList, ref List<string> lFilesList, bool sort )
		{
			try	{
				foreach (string dir in lDirsList) {
					if( Directory.Exists( dir ) ) {
						string [] files = Directory.GetFiles( dir );
						if( files.Length != 0 )
							lFilesList.AddRange(Directory.GetFiles(dir));
					}
				}
			} catch /*(System.Exception excpt)*/ {
				//Console.WriteLine(excpt.Message);
			}
			if (sort)
				lFilesList.Sort();
			return lFilesList.Count;
		}
		
		// создание списка всех файлов по списку папок
		public static int makeFilesListFromDirs( ref BackgroundWorker bw, ref DoWorkEventArgs e,
		                                        ref List<string> lDirsList, ref List<string> lFilesList, bool sort )
		{
			try	{
				foreach ( string dir in lDirsList ) {
					if( ( bw.CancellationPending == true ) )  {
						e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
						return 0; // поиск прерван, дальнейшая работа со списком файлов бессмысленна. Возвращаем 0 файлов.
					}
					if( Directory.Exists( dir ) ) {
						string [] files = Directory.GetFiles( dir );
						if( files.Length != 0 )
							lFilesList.AddRange(Directory.GetFiles(dir));
					}
				}
			} catch /*(System.Exception excpt)*/ {
				//Console.WriteLine(excpt.Message);
			}
			if (sort)
				lFilesList.Sort();
			return lFilesList.Count;
		}
		
		// создание списка всех файлов в заданной папке
		public static int  makeFilesListFromDir( string sFromDir, ref List<string> lFilesList, bool sort ) {
			try	{
				if( Directory.Exists( sFromDir ) ) {
					string [] files = Directory.GetFiles( sFromDir );
					if( files.Length != 0 ) {
						lFilesList = new List<string>();
						lFilesList.AddRange( files );
					}
				}
			} catch /*(System.Exception excpt)*/ {
				//Console.WriteLine(excpt.Message);
			}
			if (sort)
				lFilesList.Sort();
			return lFilesList.Count;
		}

	}

}
