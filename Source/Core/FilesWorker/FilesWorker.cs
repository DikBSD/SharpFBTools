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
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.Globalization;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
		
		public static void StartAsyncDiff( string sProgramPath, string sPath1, string sPath2 ) {
			CommandManager manag = new CommandManager();
			manag.RunAsync( "\""+sProgramPath+"\"", "\""+sPath1+"\" \""+sPath2+"\"", ProcessWindowStyle.Maximized, Core.FilesWorker.Priority.GetPriority( "Средний" ) );
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
			if( tb.Text.Trim() != "" ) {
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
		public static int DirsParser( BackgroundWorker bw, DoWorkEventArgs e, string sStartDir, ListView lv, ref List<string> lAllDirsList, bool bSort ) {
			int nAllFilesCount = 0;
			// рабочий список папок - по нему парсим вложенные папки и из него удаляем отработанные
			List<string> lWorkDirList = new List<string>();
			// начальное заполнение списков
			nAllFilesCount = Directory.GetFiles( sStartDir ).Length;
			nAllFilesCount += DirListMaker( sStartDir, ref lWorkDirList );
			lAllDirsList.Add( sStartDir );
			lAllDirsList.AddRange( lWorkDirList );
			lv.Items[0].SubItems[1].Text = lAllDirsList.Count.ToString();
			while( lWorkDirList.Count != 0 ) {
				// перебор папок в указанной папке s
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
		
		// обработка строки с папкой
		public static string WorkingDirPath( string sDir ) {
			if( sDir.Length>3 ) {
				Regex rx = new Regex( @"\\+$" );
				sDir = rx.Replace( sDir, "" );
			} else if( sDir.Length==2 ) {
				sDir += "\\";
			}
			return sDir;
		}
		
		// проверка на наличие указаной папки и ее создание, если ее нет
		public static bool CreateDirIfNeed( string sDir, string sMessTitle ) {
			DirectoryInfo diFolder = new DirectoryInfo( sDir );
			if( !diFolder.Exists ) {
				string sMess = "Папка-приемник не найдена: " + sDir + ".\nСоздать ее?";
				DialogResult result = MessageBox.Show( sMess, sMessTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question );
	        	if( result == DialogResult.No ) {
					MessageBox.Show( "Работа прекращена.", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
	            	return false;
				}
				try {
					diFolder.Create();
				} catch {
					MessageBox.Show( "Не могу создать папку "+sDir+"!\nРабота прекращена.", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return false;
				}
			}
			return true;
		}
		
		// считывание кодировки файла, если она задана в строке sLine
		public static string GetFileEncoding( string sLine ) {
			string sFileEncoding = "";
			int nStart = sLine.IndexOf( "encoding" );
			if( nStart!=-1 ) {
				sFileEncoding = sLine.Substring( nStart+8 );
				nStart = sFileEncoding.IndexOf( "\"" );
				if( nStart!=-1 ) {
					sFileEncoding = sFileEncoding.Substring( nStart+1 );
					nStart = sFileEncoding.IndexOf( "\"" );
					if( nStart!=-1 ) {
						sFileEncoding = sFileEncoding.Substring( 0, nStart );
						return sFileEncoding;
					}
				}
			}
			return null;
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
			try {
				string[] dirs = Directory.GetDirectories(sStartDir);
				foreach( string sDir in dirs ) {
					try {
						nFilesCount += Directory.GetFiles( sDir ).Length;
						lDirList.Add( sDir );
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
		
	}
}
