/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 04.09.2009
 * Time: 17:21
 * 
 * License: GPL 2.1
 */
using System;
using System.Windows.Forms;

namespace Settings
{
	/// <summary>
	/// Description of SettingsFB2Dup.
	/// </summary>
	public class SettingsFB2Dup
	{
		#region Закрытые статические данные класса
		private static string m_sDuplicatorHelpPath = Settings.GetProgDir()+"\\Help\\FB2DuplicatorHelp.rtf";
		private static string m_sFB2DupScanDir	= string.Empty; // папка сканирования
		private static string m_sFB2DupToDir 	= string.Empty; // папка приемник
		private static bool m_FB2LibrusecGenres	= true;
		private static bool m_FB22Genres		= false;
		// вид ToolButtons инструмента
		private static string m_cboxDSFB2DupText	= "ImageAndText";
		private static string m_cboxTIRFB2DupText	= "ImageBeforeText";
		#endregion
		
		public SettingsFB2Dup()
		{
		}
		
		#region Открытые статические методы класса
		public static string GetDuplicatorHelpPath() {
			return m_sDuplicatorHelpPath;
		}
		public static string GetDefDupcboxDSFB2DupText() {
			return m_cboxDSFB2DupText;
		}
		public static string GetDefDupcboxTIRFB2DupText() {
			return m_cboxTIRFB2DupText;
		}
		public static void SetToolButtonsSettings( ToolStrip ts ) {
			// задание для кнопок ToolStrip стиля и положения текста и картинки
			Settings.SetToolButtonsSettings( "DupToolButtons", "cboxDSFB2DupText", "cboxTIRFB2DupText", ts );
		}
		#endregion
		
		#region Открытые статические свойства класса
		public static string DupScanDir {
			get { return m_sFB2DupScanDir; }
			set { m_sFB2DupScanDir = value; }
		}
		public static string DupToDir {
			get { return m_sFB2DupToDir; }
			set { m_sFB2DupToDir = value; }
		}
		
		// схема жанров
		public static bool FB2LibrusecGenres {
			get { return m_FB2LibrusecGenres; }
			set { m_FB2LibrusecGenres = value; }
		}
		public static bool FB22Genres {
			get { return m_FB22Genres; }
			set { m_FB22Genres = value; }
		}
		#endregion
	}
}
