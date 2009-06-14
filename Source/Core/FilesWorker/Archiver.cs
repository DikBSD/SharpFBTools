/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 09.03.2009
 * Time: 9:41
 * 
 * License: GPL 2.1
 */

using System;
using System.Text.RegularExpressions;
using System.IO;

using System.Collections.Generic;

namespace FilesWorker
{
	/// <summary>
	/// Description of Archiver.
	/// </summary>
	public class Archiver
	{
		public Archiver()
		{
		}
		
		public static int debug_unzip( List<string> gebug, string sZipPath, string sFilePath, string sTempDir ) {
			// распаковка zip-фрхива
			Regex rx = new Regex( @"\\+" );
			sZipPath = rx.Replace( sZipPath, "\\" );
			sFilePath = rx.Replace( sFilePath, "\\" );
			sTempDir = rx.Replace( sTempDir, "\\" );
			
			if( !Directory.Exists( sTempDir ) ) {
				Directory.CreateDirectory( sTempDir );
			}
			
			string s = "\"" + sZipPath + "\" e"; // Распаковать (для полных путей - x)
			s += " -y"; // На все отвечать yes
			s += " " + "\"" + sFilePath + "\""; // Файл который нужно распаковать
			s += " -o" + "\"" + sTempDir + "\""; // Временная папка распаковки
			
			try {
				return Microsoft.VisualBasic.Interaction.Shell(s, Microsoft.VisualBasic.AppWinStyle.Hide, true, -1);
			} catch {
				gebug.Add( "строка s= "+s+"sZipPath= "+sZipPath+" | sFilePath="+sFilePath+" | sTempDir="+sTempDir );
				return -1;
			}
		}
		
		public static int unzip( string sZipPath, string sFilePath, string sTempDir ) {
			// распаковка zip-фрхива
			Regex rx = new Regex( @"\\+" );
			sZipPath = rx.Replace( sZipPath, "\\" );
			sFilePath = rx.Replace( sFilePath, "\\" );
			sTempDir = rx.Replace( sTempDir, "\\" );
			
			if( !Directory.Exists( sTempDir ) ) {
				Directory.CreateDirectory( sTempDir );
			}
			
			string s = "\"" + sZipPath + "\" e"; // Распаковать (для полных путей - x)
			s += " -y"; // На все отвечать yes
			s += " " + "\"" + sFilePath + "\""; // Файл который нужно распаковать
			s += " -o" + "\"" + sTempDir + "\""; // Временная папка распаковки
			
			return Microsoft.VisualBasic.Interaction.Shell(s, Microsoft.VisualBasic.AppWinStyle.Hide, true, -1);
		}
		
		public static int unrar( string sUnRarPath, string sFilePath, string sTempDir ) {
			// распаковка rar-фрхива
			Regex rx = new Regex( @"\\+" );
			sUnRarPath = rx.Replace( sUnRarPath, "\\" );
			sFilePath = rx.Replace( sFilePath, "\\" );
			sTempDir = rx.Replace( sTempDir, "\\" );
			
			if( !Directory.Exists( sTempDir ) ) {
				Directory.CreateDirectory( sTempDir );
			}
			
			string s = "\"" + sUnRarPath + "\" e"; // Распаковать (для полных путей - x)
			s += " -y"; // На все отвечать yes
			s += " " + "\"" + sFilePath + "\""; // Файл который нужно распаковать
			s += " " + "\"" + sTempDir + "\""; // Временная папка распаковки
			return Microsoft.VisualBasic.Interaction.Shell(s, Microsoft.VisualBasic.AppWinStyle.Hide, true, -1);
		}
		
		public static int zip( string sZipPath, string sType, string sFilePath,
		                      string sFB2ZipFilePath ) {
			// упаковка в zip-фрхив
			Regex rx = new Regex( @"\\+" );
			sZipPath = rx.Replace( sZipPath, "\\" );
			sFilePath = rx.Replace( sFilePath, "\\" );
			sFB2ZipFilePath = rx.Replace( sFB2ZipFilePath, "\\" );
			
			string s = "\"" + sZipPath + "\" a"; // запаковать
			s += " -t"+sType.ToLower(); // в sType - тип архивации
			s += " -y"; // На все отвечать yes
			s += " \"" + sFB2ZipFilePath + "\""; // файл-архив .fb2.sType
			s += " \"" + sFilePath + "\""; // Файл который нужно запаковать
			return Microsoft.VisualBasic.Interaction.Shell(s, Microsoft.VisualBasic.AppWinStyle.Hide, true, -1);
		}
		
		public static int rar( string sRarPath, string sFilePath,
		                      string sFB2RarFilePath, bool bRestoreInfo ) {
			// упаковка в rar-фрхив
			Regex rx = new Regex( @"\\+" );
			sRarPath = rx.Replace( sRarPath, "\\" );
			sFilePath = rx.Replace( sFilePath, "\\" );
			sFB2RarFilePath = rx.Replace( sFB2RarFilePath, "\\" );
			
			string s = "\"" + sRarPath + "\" a -m5"; // запаковать с максимальным сжатием
			if( bRestoreInfo ) {
				s += " -rr"; // добавить информацию для восстановления
			}
			s += " -y"; // На все отвечать yes
			s += " -ep"; // Исключить пути из имен
			s += " \"" + sFB2RarFilePath + "\""; // файл-архив .fb2.rar
			s += " \"" + sFilePath + "\""; // Файл который нужно запаковать
			return Microsoft.VisualBasic.Interaction.Shell(s, Microsoft.VisualBasic.AppWinStyle.Hide, true, -1);
		}
	}
}
