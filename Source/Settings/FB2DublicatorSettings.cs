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
	/// Работа с настройками Дубликатора
	/// </summary>
	public class FB2DublicatorSettings
	{
		#region Закрытые статические данные класса
		private readonly static string m_sDuplicatorHelpPath = Settings.ProgDir + "\\Help\\FB2DuplicatorHelp.rtf";
		private readonly static string m_DublicatorPath	= Settings.ProgDir + @"\DuplicatorSettings.xml";
		// вид ToolButtons инструмента
		private readonly static string m_cboxDSFB2DupText	= "ImageAndText";
		private readonly static string m_cboxTIRFB2DupText	= "ImageBeforeText";
		#endregion
		
		public FB2DublicatorSettings()
		{
		}
		
		#region Открытые статические методы класса
		public static string DublicatorPath {
			get { return m_DublicatorPath; }
		}
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
			Settings.SetToolButtonsSettings(
				"Settings/General/ToolButtons/DupToolButtons", "DSFB2DupText", "TIRFB2DupText", ts
			);
		}
		#endregion
	}
}
