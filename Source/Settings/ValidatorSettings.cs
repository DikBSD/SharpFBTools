/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 01.10.2013
 * Time: 13:54
 * 
 * License: GPL 2.1
 */
using System;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace Settings
{
	/// <summary>
	/// ValidatorSettings: класс для работы с сохранением в xml и чтением настроек Валидатора
	/// </summary>
	public class ValidatorSettings
	{
		#region Закрытые статические члены-данные класса
		private static string m_FileSettingsPath		= Settings.ProgDir + @"\ValidatorSettings.xml";
		private static string m_sFB2ValidatorHelpPath	= Settings.ProgDir + @"\Help\FB2ValidatorHelp.rtf";
		// вид ToolButtons инструмента
		private static string m_cboxDSValidatorText		= "ImageAndText";
		private static string m_cboxTIRValidatorText	= "ImageBeforeText";
		#endregion
		
		public ValidatorSettings()
		{
		}
		
		#region Открытые статические методы
		public static string GetFB2ValidatorHelpPath() {
			return m_sFB2ValidatorHelpPath;
		}
		
		// задание для кнопок ToolStrip стиля и положения текста и картинки
		public static string GetDefValidatorcboxDSValidatorText() {
			return m_cboxDSValidatorText;
		}
		public static string GetDefValidatorcboxTIRValidatorText() {
			return m_cboxTIRValidatorText;
		}
		
		public static void SetToolButtonsSettings( ToolStrip ts ) {
			Settings.SetToolButtonsSettings( "Settings/General/ToolButtons/ValidatorToolButtons", "DSValidatorText", "TIRValidatorText", ts );
		}
		#endregion
	}
}
