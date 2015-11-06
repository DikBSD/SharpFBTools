/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 14.05.2009
 * Time: 16:37
 * 
 * License: GPL 2.1
 */
using System;
using System.Windows.Forms;

namespace Settings
{
	/// <summary>
	/// Работа с настройками Менеджера Архивов
	/// </summary>
	public class ArchiveManagerSettings
	{
		#region Закрытые статические члены-данные класса
		private readonly static string m_sArchiveManagerHelpPath = Settings.ProgDir + "\\Help\\ArchiveManagerHelp.rtf";
		// вид ToolButtons инструмента
		private readonly static string m_cboxDSArchiveManagerText	= "ImageAndText";
		private readonly static string m_cboxTIRArchiveManagerText	= "ImageBeforeText";
		#endregion
		
		public ArchiveManagerSettings()
		{
		}

		#region Открытые статические методы класса
		public static string GetArchiveManagerHelpPath() {
			return m_sArchiveManagerHelpPath;
		}
		public static string GetDefAMcboxDSArchiveManagerText() {
			return m_cboxDSArchiveManagerText;
		}
		public static string GetDefAMcboxTIRArchiveManagerText() {
			return m_cboxTIRArchiveManagerText;
		}
		// задание для кнопок ToolStrip стиля и положения текста и картинки
		public static void SetToolButtonsSettings( ToolStrip ts ) {
			Settings.SetToolButtonsSettings( "Settings/General/ToolButtons/ArchiveManagerToolButtons", "DSArchiveManagerText", "TIRArchiveManagerText", ts );
		}
		#endregion
	}
}
