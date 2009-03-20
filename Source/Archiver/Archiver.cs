/*
 * Created by SharpDevelop.
 * User: DikBSD
 * Date: 09.03.2009
 * Time: 9:41
 * 
 * License: GPL 2.1
 */

using System;

namespace Archiver
{
	/// <summary>
	/// Description of Archiver.
	/// </summary>
	public class Archiver
	{
		public Archiver()
		{
		}
		
		public static int unzip( string sZipPath, string sFilePath, string sTempDir ) {
			// распаковка zip-фрхива
			string s = "\"" + sZipPath + "\" e"; // Распаковать (для полных путей - x)
			s += " -y"; // На все отвечать yes
			s += " " + "\"" + sFilePath + "\""; // Файл который нужно распаковать
			s += " -o" + sTempDir; // Временная папка распаковки
			return Microsoft.VisualBasic.Interaction.Shell(s, Microsoft.VisualBasic.AppWinStyle.Hide, true, -1);
		}
		
		public static int unrar( string sUnRarPath, string sFilePath, string sTempDir ) {
			// распаковка rar-фрхива
			string s = "\"" + sUnRarPath + "\" e"; // Распаковать (для полных путей - x)
			s += " -y"; // На все отвечать yes
			s += " " + "\"" + sFilePath + "\""; // Файл который нужно распаковать
			s += " " + sTempDir; // Временная папка распаковки
			return Microsoft.VisualBasic.Interaction.Shell(s, Microsoft.VisualBasic.AppWinStyle.Hide, true, -1);
		}
		
		public static int zip( string sZipPath, string sFilePath,
		                      string sFB2ZipFilePath ) {
			// упаковка в zip-фрхив
			string s = "\"" + sZipPath + "\" a"; // запаковать
			s += " -tzip"; // в zip
			s += " -y"; // На все отвечать yes
			s += " \"" + sFB2ZipFilePath + "\""; // файл-архив .fb2.zip
			s += " \"" + sFilePath + "\""; // Файл который нужно запаковать
			return Microsoft.VisualBasic.Interaction.Shell(s, Microsoft.VisualBasic.AppWinStyle.Hide, true, -1);
		}
		
		public static int rar( string sRarPath, string sFilePath,
		                      string sFB2RarFilePath, bool bRestoreInfo ) {
			// упаковка в rar-фрхив
			string s = "\"" + sRarPath + "\" a -m5"; // запаковать с максимальным сжатием
			if( bRestoreInfo ) {
				s += " -rr"; // добавить информацию для восстановления
			}
			s += " -y"; // На все отвечать yes
			s += " \"" + sFB2RarFilePath + "\""; // файл-архив .fb2.rar
			s += " \"" + sFilePath + "\""; // Файл который нужно запаковать
			return Microsoft.VisualBasic.Interaction.Shell(s, Microsoft.VisualBasic.AppWinStyle.Hide, true, -1);
		}
	}
}
