/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 08.03.2009
 * Time: 17:06
 * 
 * License: GPL 2.1
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Globalization;
using System.ComponentModel;
using System.Threading;
using System.Diagnostics;

namespace Core.FilesWorker
{
	/// <summary>
	/// Description of FilesWorker.
	/// </summary>
	public class FilesWorker
	{

		public FilesWorker()
		{
		}
		
		#region Открытые статические методы класса
		public static List<string> DirsParser( string sStartDir, ListView lv, bool bSort ) {
			// список всех вложенных папок для стартового, включая и стартовый - замена рекурсии
			List<string> lAllDirsList = new List<string>();
			// рабочий список папок - по нему парсим вложенные папки и из него удаляем отработанные
			List<string> lWorkDirList = new List<string>();
			// начальное заполнение списков
			lWorkDirList = DirListMaker( sStartDir );
			lAllDirsList.Add( sStartDir );
			lAllDirsList.AddRange( lWorkDirList );
			lv.Items[0].SubItems[1].Text = lAllDirsList.Count.ToString();
			while( lWorkDirList.Count != 0 ) {
				// перебор папок в указанной папке s
				int nWorkCount = lWorkDirList.Count;
				for( int i=0; i!=nWorkCount; ++i  ) {
					// l - список найденных папок в указанной папке sWD
					List<string> l = DirListMaker( lWorkDirList[i] );
					// заносим найденные папки в рабочий и полный список папок
					lWorkDirList.AddRange( l );
					lAllDirsList.AddRange( l );
					lv.Items[0].SubItems[1].Text = lAllDirsList.Count.ToString();
					lv.Refresh();
				}
				// удаляем из рабочего списка обработанные папки
				lWorkDirList.RemoveRange( 0, nWorkCount );
			}
			if( bSort ) {
				lAllDirsList.Sort();
			}
			return lAllDirsList;
		}
		
		public static List<string> AllFilesParser( BackgroundWorker bw, DoWorkEventArgs e,
		                                          List<string> lsDirs, ListView lv,
		                                          ToolStripProgressBar pBar, bool bSort ) {
			// список всех файлов - по cписку папок - замена рекурсии
			pBar.Maximum = lsDirs.Count+1;
			List<string> lFilesList = new List<string>();
			foreach( string s in lsDirs ) {
				// Проверить флаг на остановку процесса 
        	    if( ( bw.CancellationPending == true ) ) {
					e.Cancel = true; // Выставить окончание - по отмене, сработает событие Bw_RunWorkerCompleted
					break;
				} else {
					DirectoryInfo diFolder = new DirectoryInfo( s );
					foreach( FileInfo fiNextFile in diFolder.GetFiles() ) {
						lFilesList.Add( s + "\\" + fiNextFile.Name );
						lv.Items[1].SubItems[1].Text = lFilesList.Count.ToString();
					}
					++pBar.Value;
				}
			}
			if( bSort ) {
				lFilesList.Sort();
			}
			return lFilesList;
		}
		
		public static void DumpReadOnlyAttrForFiles( string sDir ) {
			// сброс для списка файлов аттрибута только для чтения
			DirectoryInfo diFolder = new DirectoryInfo( sDir );
			foreach( FileInfo fiNextFile in diFolder.GetFiles() ) {
				if ( ( File.GetAttributes( diFolder.FullName+"\\"+fiNextFile.ToString() ) &
				      FileAttributes.ReadOnly ) == FileAttributes.ReadOnly ) {
					File.SetAttributes( diFolder.FullName+"\\"+fiNextFile.ToString(), FileAttributes.Normal ) ;
				}
			}
		}
		
		public static void RemoveDir( string sDir ) {
			// удалить папку sDir и все ее подпапки и файлы
			if( Directory.Exists( sDir ) ) {
				DumpReadOnlyAttrForFiles( sDir );
				Directory.Delete( sDir, true );
			}
		}
		
		public static List<string> DirListMaker( string sStartDir ) {
			// папки в текущей папке
			DirectoryInfo diFolder = new DirectoryInfo( sStartDir );
			List<string> lDirList = new List<string>();
			foreach( DirectoryInfo diNextFolder in diFolder.GetDirectories() ) {
				lDirList.Add( sStartDir + "\\" + diNextFolder.Name );
			}
			return lDirList; 
		}
		
		public static void ShowDir( System.Windows.Forms.ListView lw ) {
			ListView.SelectedListViewItemCollection si = lw.SelectedItems;
			FileInfo fi = new FileInfo( si[0].SubItems[0].Text.Split('/')[0] );
			CommandManager manag = new CommandManager();
			manag.Run( "c:\\WINDOWS\\explorer.exe", "\""+fi.Directory.ToString()+"\"", ProcessWindowStyle.Maximized, Core.FilesWorker.Priority.GetPriority( "Средний" ) );
		}
		
		public static void ShowAsyncDir( System.Windows.Forms.ListView lw ) {
			ListView.SelectedListViewItemCollection si = lw.SelectedItems;
			FileInfo fi = new FileInfo( si[0].SubItems[0].Text.Split('/')[0] );
			CommandManager manag = new CommandManager();
			manag.RunAsync( "c:\\WINDOWS\\explorer.exe", "\""+fi.Directory.ToString()+"\"", ProcessWindowStyle.Maximized, Core.FilesWorker.Priority.GetPriority( "Средний" ) );
		}
		
		public static void ShowDir( string sDir ) {
			CommandManager manag = new CommandManager();
			manag.Run( "c:\\WINDOWS\\explorer.exe", "\""+sDir+"\"", ProcessWindowStyle.Maximized, Core.FilesWorker.Priority.GetPriority( "Средний" ) );
		}
		
		public static void ShowAsyncDir( string sDir ) {
			CommandManager manag = new CommandManager();
			manag.RunAsync( "c:\\WINDOWS\\explorer.exe", "\""+sDir+"\"", ProcessWindowStyle.Maximized, Core.FilesWorker.Priority.GetPriority( "Средний" ) );
		}
		
		public static void StartFile( string sProgramPath, System.Windows.Forms.ListView lw ) {
			ListView.SelectedListViewItemCollection si = lw.SelectedItems;
			CommandManager manag = new CommandManager();
			manag.Run( "\""+sProgramPath+"\"", "\""+si[0].SubItems[0].Text.Split('/')[0]+"\"", ProcessWindowStyle.Maximized, Core.FilesWorker.Priority.GetPriority( "Средний" ) );
		}
		
		public static void StartAsyncFile( string sProgramPath, System.Windows.Forms.ListView lw ) {
			ListView.SelectedListViewItemCollection si = lw.SelectedItems;
			CommandManager manag = new CommandManager();
			manag.RunAsync( "\""+sProgramPath+"\"", "\""+si[0].SubItems[0].Text.Split('/')[0]+"\"", ProcessWindowStyle.Maximized, Core.FilesWorker.Priority.GetPriority( "Средний" ) );
		}
		
		public static void StartFile( string sProgramPath, string sStartFilePath ) {
			CommandManager manag = new CommandManager();
			manag.Run( "\""+sProgramPath+"\"", "\""+sStartFilePath+"\"", ProcessWindowStyle.Maximized, Core.FilesWorker.Priority.GetPriority( "Средний" ) );
		}
		
		public static void StartAsyncFile( string sProgramPath, string sStartFilePath ) {
			CommandManager manag = new CommandManager();
			manag.RunAsync( "\""+sProgramPath+"\"", "\""+sStartFilePath+"\"", ProcessWindowStyle.Maximized, Core.FilesWorker.Priority.GetPriority( "Средний" ) );
		}
		
		public static string FormatFileLength( long lLength ) {
			float f = lLength;
			if( lLength < 1024 ) {
				return lLength.ToString()+" байт";
			} else if( lLength < 1048576 ) { // >=1 Мб
				return (f/1024).ToString()+" Кб";
			} else { // <=1 Гб
				return (f/(1024*1024)).ToString()+" Мб";
			}
		}
		
		public static bool OpenDirDlg( TextBox tb, FolderBrowserDialog fbd, string sTitle ) {
			// задание папки черед диалог открытия папки
			if( tb.Text.Trim() !="" ) {
				fbd.SelectedPath = tb.Text.Trim();
			}
			fbd.Description = sTitle;
			DialogResult result = fbd.ShowDialog();
			if (result == DialogResult.OK) {
                string openFolderName = fbd.SelectedPath;
                tb.Text = openFolderName;
                return true;
            }
			return false;
		}
		
		// составляем список файлов (или одного) из папки
		// Параметры:
		// sFromDir - папка-источник; bSort=true - сортировать список; bFB2Only=true - список только fb2-файлов
		public static List<string> MakeFileListFromDir( string sFromDir, bool bSort, bool bFB2Only ) {
			List<string> lFilesList = null;
			if( Directory.Exists( sFromDir ) ) {
				string [] files = Directory.GetFiles( sFromDir );
				if( files.Length != 0 ) {
					lFilesList = new List<string>();
					foreach( string sFile in files ) {
						if( bFB2Only ) {
							if( Path.GetExtension( Path.GetFileName( sFile ) ).ToLower()==".fb2" ) {
								lFilesList.Add( sFile );
							}
						} else {
							lFilesList.Add( sFile );
						}
					}
					if( bSort ) {
						lFilesList.Sort();
					}
				}
			}
			return lFilesList;
		}
		
		// *********************************************************************************** //
		// создание списка всех подпапок в заданной
		// параметры:	sStartDir - папка для сканирования;
		//				lAllDirsList - заполняемый список папок в папке сканирования и ее подпапках
		//				bSort = true - сортировать созданный список папок
		// возвращает: число всех файлов в папке для сканирования и ее подпапках
		public static int DirsParser( string sStartDir, ListView lv, ref List<string> lAllDirsList, bool bSort ) {
			int nAllFilesCount = 0;
			// рабочий список папок - по нему парсим вложенные папки и из него удаляем отработанные
			List<string> lWorkDirList = new List<string>();
			// начальное заполнение списков
			nAllFilesCount = DirListMaker( sStartDir, ref lWorkDirList );
			lAllDirsList.Add( sStartDir );
			lAllDirsList.AddRange( lWorkDirList );
			lv.Items[0].SubItems[1].Text = lAllDirsList.Count.ToString();
			while( lWorkDirList.Count != 0 ) {
				// перебор папок в указанной папке s
				int nWorkCount = lWorkDirList.Count;
				for( int i=0; i!=nWorkCount; ++i ) {
					// l - список найденных папок в указанной папке sWD
					List<string> l = new List<string>();
					nAllFilesCount += DirListMaker( lWorkDirList[i], ref l );
					// заносим найденные папки в рабочий и полный список папок
					lWorkDirList.AddRange( l );
					lAllDirsList.AddRange( l );
					lv.Items[0].SubItems[1].Text = lAllDirsList.Count.ToString();
//					lv.Refresh();
				}
				// удаляем из рабочего списка обработанные папки
				lWorkDirList.RemoveRange( 0, nWorkCount );
			}
			if( bSort ) {
				lAllDirsList.Sort();
			}
			return nAllFilesCount;
		}

		#endregion]
		
		#region Закрытые вспомогательные методы
		// создание списка подпапок в заданной
		// параметры:	sStartDir - папка для сканирования;
		//				lDirList - заполняемый список папок в текущей папке
		// возвращает: число файлов в текущем каталоге
		private static int DirListMaker( string sStartDir, ref List<string> lDirList ) {
			int nFilesCount = 0;
			// папки в текущей папке
			string[] dirs = Directory.GetDirectories(sStartDir);
			if( dirs.Length==0 ) {
				nFilesCount = Directory.GetFiles( sStartDir ).Length;
				return nFilesCount;
			}
			
			foreach( string sDir in dirs ) {
				lDirList.Add( sDir );
				nFilesCount += Directory.GetFiles( sDir ).Length;
			}
			return nFilesCount; 
		}
		/*
		private static int DirListMaker( string sStartDir, ref List<string> lDirList ) {
			int nFilesCount = 0;
			// папки в текущей папке
			DirectoryInfo diFolder = new DirectoryInfo( sStartDir );
			if( diFolder.GetDirectories().Length==0 ) {
				DirectoryInfo di = new DirectoryInfo( sStartDir );
				nFilesCount = di.GetFiles().Length;
				return nFilesCount;
			}
			
			foreach( DirectoryInfo diNextFolder in diFolder.GetDirectories() ) {
				lDirList.Add( sStartDir + "\\" + diNextFolder.Name );
				DirectoryInfo di = new DirectoryInfo( sStartDir + "\\" + diNextFolder.Name );
				nFilesCount += di.GetFiles().Length;
			}
			return nFilesCount; 
		}*/
		#endregion
		
	}
}
