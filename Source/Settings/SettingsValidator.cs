/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 14.05.2009
 * Time: 16:44
 * 
 * License: GPL 2.1
 */
using System;
using System.Xml;
using System.Windows.Forms;

namespace Settings
{
	/// <summary>
	/// Description of SettingsValidator.
	/// </summary>
	public class SettingsValidator
	{
		#region Закрытые статические члены-данные класса
		private static string m_sFB2ValidatorHelpPath = Settings.GetProgDir()+"\\Help\\FB2ValidatorHelp.rtf";
		private static Int16 m_nValidatorForFB2SelectedIndex		= 1;
		private static Int16 m_nValidatorForFB2ArchiveSelectedIndex	= 1;
		private static Int16 m_nValidatorForFB2SelectedIndexPE		= 0;
		private static Int16 m_nValidatorForFB2ArchiveSelectedIndexPE	= 0;
		// вид ToolButtons инструмента
		private static string m_cboxDSValidatorText		= "ImageAndText";
		private static string m_cboxTIRValidatorText	= "ImageBeforeText";
		//
		private static string m_sScanDir 				= "";
		private static string m_sFB2NotValidDirCopyTo	= "";
		private static string m_sFB2NotValidDirMoveTo	= "";
		private static string m_sFB2ValidDirCopyTo		= "";
		private static string m_sFB2ValidDirMoveTo		= "";
		private static string m_sNotFB2DirCopyTo		= "";
		private static string m_sNotFB2DirMoveTo		= "";
		#endregion
		
		public SettingsValidator()
		{
		}
		
		#region Открытые статические методы
		public static void SetToolButtonsSettings( ToolStrip ts ) {
			// задание для кнопок ToolStrip стиля и положения текста и картинки
			Settings.SetToolButtonsSettings( "ValidatorToolButtons", "cboxDSValidatorText", "cboxTIRValidatorText", ts );
		}
		
		public static string GetDefValidatorcboxDSValidatorText() {
			return m_cboxDSValidatorText;
		}
		public static string GetDefValidatorcboxTIRValidatorText() {
			return m_cboxTIRValidatorText;
		}
		
		public static Int16 GetDefValidatorFB2SelectedIndex() {
			return m_nValidatorForFB2SelectedIndex;
		}
		
		public static Int16 GetDefValidatorFB2ArchiveSelectedIndex() {
			return m_nValidatorForFB2ArchiveSelectedIndex;
		}
		
		public static Int16 GetDefValidatorFB2SelectedIndexPE() {
			return m_nValidatorForFB2SelectedIndexPE;
		}
		
		public static Int16 GetDefValidatorFB2ArchiveSelectedIndexPE() {
			return m_nValidatorForFB2ArchiveSelectedIndexPE;
		}
		
		public static string GetFB2ValidatorHelpPath() {
			return m_sFB2ValidatorHelpPath;
		}
		
		public static Int16 ReadValidatorFB2SelectedIndex() {
			// читаем номер выделенного итема для комбобокса cboxValidatorForFB2 из настроек
			return Convert.ToInt16( Settings.ReadAttribute( "ValidatorDoubleClick", "cboxValidatorForFB2SelectedIndex", GetDefValidatorFB2SelectedIndex().ToString() ) );
		}
		
		public static Int16 ReadValidatorFB2ArchiveSelectedIndex() {
			// читаем номер выделенного итема для комбобокса cboxValidatorForFB2Archive из настроек
			return Convert.ToInt16( Settings.ReadAttribute( "ValidatorDoubleClick", "cboxValidatorForFB2ArchiveSelectedIndex", GetDefValidatorFB2ArchiveSelectedIndex().ToString() ) );
		}
		
		public static Int16 ReadValidatorFB2SelectedIndexPE() {
			// читаем номер выделенного итема для комбобокса cboxValidatorForFB2 из настроек для нажатия Enter
			return Convert.ToInt16( Settings.ReadAttribute( "ValidatorPressEnter", "cboxValidatorForFB2SelectedIndexPE", GetDefValidatorFB2SelectedIndexPE().ToString() ) );
		}
		
		public static Int16 ReadValidatorFB2ArchiveSelectedIndexPE() {
			// читаем номер выделенного итема для комбобокса cboxValidatorForFB2Archive из настроек для нажатия Enter
			return Convert.ToInt16( Settings.ReadAttribute( "ValidatorPressEnter", "cboxValidatorForFB2ArchiveSelectedIndexPE", GetDefValidatorFB2ArchiveSelectedIndexPE().ToString() ) );
		}
		#endregion

		#region Открытые статические свойства класса для Папок Валидатора
		public static string ScanDir {
			// папка для сканирования
			get { return m_sScanDir; }
			set { m_sScanDir = value; }
		}

		public static string FB2NotValidDirCopyTo {
			// папка для копирования не валидных fb2-файлов
			get { return m_sFB2NotValidDirCopyTo; }
			set { m_sFB2NotValidDirCopyTo = value; }
		}
		
		public static string FB2NotValidDirMoveTo {
			// папка для перемещения не валидных fb2-файлов
			get { return m_sFB2NotValidDirMoveTo; }
			set { m_sFB2NotValidDirMoveTo = value; }
		}
		
		public static string FB2ValidDirCopyTo {
			// папка для копирования валидных fb2-файлов
			get { return m_sFB2ValidDirCopyTo; }
			set { m_sFB2ValidDirCopyTo = value; }
		}
		
		public static string FB2ValidDirMoveTo {
			// папка для перемещения валидных fb2-файлов
			get { return m_sFB2ValidDirMoveTo; }
			set { m_sFB2ValidDirMoveTo = value; }
		}
		
		public static string NotFB2DirCopyTo {
			// папка для копирования не fb2-файлов
			get { return m_sNotFB2DirCopyTo; }
			set { m_sNotFB2DirCopyTo = value; }
		}
		
		public static string NotFB2DirMoveTo {
			// папка для перемещения не fb2-файлов
			get { return m_sNotFB2DirMoveTo; }
			set { m_sNotFB2DirMoveTo = value; }
		}
		#endregion

	}
}
