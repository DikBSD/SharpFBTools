/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 14.05.2009
 * Time: 16:37
 * 
 * License: GPL 2.1
 */
using System;
using System.Xml;
using System.Windows.Forms;

namespace Settings
{
	/// <summary>
	/// Description of SettingsAM.
	/// </summary>
	public class SettingsAM
	{
		#region Закрытые статические члены-данные класса
		private static string m_sArchiveManagerHelpPath = Settings.GetProgDir()+"\\Help\\ArchiveManagerHelp.rtf";
		//
		private static string m_sAScanDir 			= "";
		private static string m_sUAScanDir 			= "";
		private static string m_sATargetDir			= "";
		private static string m_sUATargetDir		= "";
		// вид ToolButtons инструмента
		private static string m_cboxDSArchiveManagerText	= "ImageAndText";
		private static string m_cboxTIRArchiveManagerText	= "ImageBeforeText";
		#endregion
		
		public SettingsAM()
		{
		}
		
		#region Открытые статические свойства класса для Папок Валидатора
		public static string AMAScanDir {
			// папка для сканирования для Упаковки
			get { return m_sAScanDir; }
			set { m_sAScanDir = value; }
		}
		
		public static string AMATargetDir {
			// папка-приемник для Упаковки
			get { return m_sATargetDir; }
			set { m_sATargetDir = value; }
		}
		
		public static string AMUAScanDir {
			// папка для сканирования для Распаковки
			get { return m_sUAScanDir; }
			set { m_sUAScanDir = value; }
		}
		
		public static string AMAUATargetDir {
			// папка-приемник для Распаковки
			get { return m_sUATargetDir; }
			set { m_sUATargetDir = value; }
		}
		#endregion

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
		public static void SetToolButtonsSettings( ToolStrip ts ) {
			// задание для кнопок ToolStrip стиля и положения текста и картинки
			Settings.SetToolButtonsSettings( "ArchiveManagerToolButtons", "cboxDSArchiveManagerText", "cboxTIRArchiveManagerText", ts );
		}
		#endregion
	}
}
