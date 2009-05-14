/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 14.05.2009
 * Time: 16:44
 * 
 * License: GPL 2.1
 */
using System;

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
		//
		private static string m_sValidatorDirsSettingsPath = Settings.GetProgDir()+"\\ValidatorDirs.xml";
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
		
		#region Открытые статические метода
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
		
		#region Открытые статические члены-данные класса для Папок Валидатора
		public static string GetValidatorDirsSettingsPath() {
			return m_sValidatorDirsSettingsPath;
		}
		
		public static string GetScanDir() {
			// папка для сканирования
			return m_sScanDir;
		}
		
		public static string GetFB2NotValidDirCopyTo() {
			// папка для копирования не валидных fb2-файлов
			return m_sFB2NotValidDirCopyTo;
		}
		
		public static string GetFB2NotValidDirMoveTo() {
			// папка для перемещения не валидных fb2-файлов
			return m_sFB2NotValidDirMoveTo;
		}
		
		public static string GetFB2ValidDirCopyTo() {
			// папка для копирования валидных fb2-файлов
			return m_sFB2ValidDirCopyTo;
		}
		
		public static string GetFB2ValidDirMoveTo() {
			// папка для перемещения валидных fb2-файлов
			return m_sFB2ValidDirMoveTo;
		}
		
		public static string GetNotFB2DirCopyTo() {
			// папка для копирования не fb2-файлов
			return m_sNotFB2DirCopyTo;
		}
		
		public static string GetNotFB2DirMoveTo() {
			// папка для перемещения не fb2-файлов
			return m_sNotFB2DirMoveTo;
		}
		
		public static void SetScanDir( string sScanDir ) {
			m_sScanDir = sScanDir;
		}
		public static void SetFB2NotValidDirCopyTo( string sFB2NotValidDirCopyTo ) {
			m_sFB2NotValidDirCopyTo = sFB2NotValidDirCopyTo;
		}
		public static void SetFB2NotValidDirMoveTo( string sFB2NotValidDirMoveTo ) {
			m_sFB2NotValidDirMoveTo = sFB2NotValidDirMoveTo;
		}
		public static void SetFB2ValidDirCopyTo( string sFB2ValidDirCopyTo ) {
			m_sFB2ValidDirCopyTo = sFB2ValidDirCopyTo;
		}
		public static void SetFB2ValidDirMoveTo( string sFB2ValidDirMoveTo ) {
			m_sFB2ValidDirMoveTo = sFB2ValidDirMoveTo;
		}
		public static void SetNotFB2DirCopyTo( string sNotFB2DirCopyTo ) {
			m_sNotFB2DirCopyTo = sNotFB2DirCopyTo;
		}
		public static void SetNotFB2DirMoveTo( string sNotFB2DirMoveTo ) {
			m_sNotFB2DirMoveTo = sNotFB2DirMoveTo;
		}
		#endregion

	}
}
