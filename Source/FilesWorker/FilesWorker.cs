﻿/*
 * Created by SharpDevelop.
 * User: DikBSD
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

namespace FilesWorker
{
	/// <summary>
	/// Description of FilesWorker.
	/// </summary>
	public class FilesWorker
	{
		public FilesWorker()
		{
		}
		
		#region Закрытые члены-данные
		private static string m_sTempDir = "Temp"; // временный каталог
		#endregion
		
		#region Открытые статические методы класса
		public static string GetTempDir() {
			// возвращает временную папку
			return m_sTempDir;
		}
		
		public static List<string> DirsParser( string sStartDir, Label lblDirCount ) {
			// список всех вложенных папок для стартового, включая и стартовый - замена рекурсии
			List<string> lAllDirsList = new List<string>();
			// рабочий список папок - по нему парсим вложенные папки и из него удаляем отработанные
			List<string> lWorkDirList = new List<string>();
			// начальное заполнение списков
			lWorkDirList = DirListMaker( sStartDir );
			lAllDirsList.Add( sStartDir );
			lAllDirsList.AddRange( lWorkDirList );
			lblDirCount.Text = lAllDirsList.Count.ToString();
			while( lWorkDirList.Count != 0 ) {
				// перебор папок в указанной папке s
				int nWorkCount = lWorkDirList.Count;
				for( int i=0; i!=nWorkCount; ++i  ) {
					// l - список найденных папок в указанной папке sWD
					List<string> l = DirListMaker( lWorkDirList[i] );
					// заносим найденные папки в рабочий и полный список папок
					lWorkDirList.AddRange( l );
					lAllDirsList.AddRange( l );
					lblDirCount.Text = lAllDirsList.Count.ToString();
				}
				// удаляем из рабочего списка обработанные папки
				lWorkDirList.RemoveRange( 0, nWorkCount );
			}
			return lAllDirsList; 
		}
		
		public static List<string> AllFilesParser( List<string> lsDirs, StatusStrip ssProgress, Panel pCount,
		                                          Label lblFilesCount, System.Windows.Forms.ToolStripProgressBar pBar ) {
			// список всех файлов - по cписку папок - замена рекурсии
			pBar.Maximum = lsDirs.Count+1;
			List<string> lFilesList = new List<string>();
			foreach( string s in lsDirs ) {
				DirectoryInfo diFolder = new DirectoryInfo( s );
				foreach( FileInfo fiNextFile in diFolder.GetFiles() ) {
//					if( fiNextFile.Extension==".fb2" || fiNextFile.Extension==".zip" || fiNextFile.Extension==".rar" ) {
						lFilesList.Add( s + "\\" + fiNextFile.Name );
						lblFilesCount.Text = lFilesList.Count.ToString();
//						ssProgress.Refresh();
//						pCount.Refresh();
//					}
				}
				++pBar.Value;
				ssProgress.Refresh();
				pCount.Refresh();
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
			FileInfo fi = new FileInfo( lw.Items[0].SubItems[0].Text.Split('/')[0] );
			Microsoft.VisualBasic.Interaction.Shell( "c:\\WINDOWS\\explorer.exe " + fi.Directory.ToString(), Microsoft.VisualBasic.AppWinStyle.NormalFocus, true, -1 );
		}
		
		public static void StartFile( System.Windows.Forms.ListView lw ) {
			ListView.SelectedListViewItemCollection si = lw.SelectedItems;
			FileInfo fi = new FileInfo( lw.Items[0].SubItems[0].Text.Split('/')[0] );
			Microsoft.VisualBasic.Interaction.Shell( "c:\\WINDOWS\\explorer.exe " + fi.FullName.ToString(), Microsoft.VisualBasic.AppWinStyle.NormalFocus, true, -1 );
		}
		
		public static string FormatFileLenght( long lLenght ) {
			float f = lLenght;
			if( lLenght < 1024 ) {
				return lLenght.ToString()+" байт";
			} else if( lLenght < 1048576 ) { // >=1 Мб
				return (f/1024).ToString()+" Кб";
			} else { // <=1 Гб
				return (f/(1024*1024)).ToString()+" Мб";
			}
		}
		#endregion
	}
}