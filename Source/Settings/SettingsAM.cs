/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 14.05.2009
 * Time: 16:37
 * 
 * License: GPL 2.1
 */
using System;

namespace Settings
{
	/// <summary>
	/// Description of SettingsAM.
	/// </summary>
	public class SettingsAM
	{
		#region Закрытые статические члены-данные класса
		private static string m_sArchiveManagerHelpPath = Settings.GetProgDir()+"\\Help\\ArchiveManagerHelp.rtf";
		#endregion
		
		public SettingsAM()
		{
		}
		
		#region Открытые статические метода
		public static string GetArchiveManagerHelpPath() {
			return m_sArchiveManagerHelpPath;
		}
		#endregion
		
	}
}
