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
		
		public static int unzip( string sFilePath, string sTempDir ) {
			// распаковка zip-фрхива
			string s = "7za.exe  e"; // Распаковать (полные пути - x)
			s += " -y"; // На все отвечать yes
			s += " " + "\"" + sFilePath + "\""; // Файл который нужно распаковать
			s += " -o" + sTempDir; // Временная папка распаковки
			return Microsoft.VisualBasic.Interaction.Shell(s, Microsoft.VisualBasic.AppWinStyle.Hide, true, -1);
		}
		
		public static int unrar( string sFilePath, string sTempDir ) {
			// распаковка rar-фрхива
			string s = "unrar.exe e"; // Распаковать (полные пути -x)
			s += " -y"; // На все отвечать yes
			s += " " + "\"" + sFilePath + "\""; // Файл который нужно распаковать
			s += " " + sTempDir; // Временная папка распаковки
			return Microsoft.VisualBasic.Interaction.Shell(s, Microsoft.VisualBasic.AppWinStyle.Hide, true, -1);
		}
	}
}
