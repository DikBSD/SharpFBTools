/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 07.04.2009
 * Time: 22:18
 * 
 * License: GPL 2.1
 */
using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace Settings
{
	/// <summary>
	/// Description of Settings.
	/// </summary>
	public class Settings
	{
		#region Закрытые статические члены-данные класса
		#region Общее
		private static string m_sProgDir = Environment.CurrentDirectory;
		private static string m_sTempDir = GetProgDir()+"\\Temp"; // временный каталог
		private static string m_settings = GetProgDir()+"\\settings.xml";
		private static string m_sFB21SchemePath = GetProgDir()+"\\FictionBook.xsd";
		private static string m_sTFB2Path = "c:\\WINDOWS\\NOTEPAD.EXE";
		private static string m_sWinRarPath = "c:\\Program Files\\WinRAR\\WinRAR.exe";
		private static string m_sRarPath = "c:\\Program Files\\WinRAR\\Rar.exe";
		private static string m_sUnRARPath = GetProgDir()+"\\UnRAR.exe";
		private static string m_s7zaPath = GetProgDir()+"\\7za.exe";		
		private static string m_sFBEPath = "c:\\Program Files\\FictionBook Editor\\FBE.exe";
		private static string m_sFBReaderPath = "c:\\Program Files\\AlReader 2\\AlReader2.exe";
		private static string m_sLicensePath = GetProgDir()+"\\License GPL 2.1.rtf";
		private static string m_sChangeFilePath = GetProgDir()+"\\Change.rtf";
		#endregion
		
		#region Валидатор
		private static string m_sFB2ValidatorHelpPath = GetProgDir()+"\\Help\\FB2ValidatorHelp.rtf";
		private static Int16 m_nValidatorForFB2SelectedIndex = 1;
		private static Int16 m_nValidatorForFB2ArchiveSelectedIndex = 1;
		private static Int16 m_nValidatorForFB2SelectedIndexPE = 0;
		private static Int16 m_nValidatorForFB2ArchiveSelectedIndexPE = 0;
		#endregion
		
		#region Менеджер Архивов
		private static string m_sArchiveManagerHelpPath = GetProgDir()+"\\Help\\ArchiveManagerHelp.rtf";
		#endregion
		
		#region Менеджер Файлов
		private static bool m_bchBoxTranslitCheked = true;
		private static bool m_bchBoxStrictCheked = true;
		private static Int16 m_ncboxSpaceSelectedIndex = 0;
		private static bool m_bchBoxToArchiveCheked = true;
		private static Int16 m_ncboxArchiveTypeSelectedIndex = 1;
		private static Int16 m_ncboxFileExistSelectedIndex = 1;
		private static bool m_bchBoxDelFB2FilesCheked = false;
		private static bool m_brbtnAsIsCheked = true;
		private static bool m_brbtnLowerCheked = false;
		private static bool m_brbtnUpperCheked = false;
		private static bool m_brbtnGenreOneCheked = true;
		private static bool m_brbtnGenreAllCheked = false;
		private static bool m_brbtnAuthorOneCheked = true;
		private static bool m_brbtnAuthorAllCheked = false;
		private static bool m_brbtnGenreSchemaCheked = true;
		private static bool m_brbtnGenreTextCheked = false;
		private static bool m_bchBoxAddToFileNameBookIDChecked = false;
		
		#endregion
		
		#endregion
		
		#region Закрытые методы класса
		private static string ReadAttribute( string sTag, string sAttr, string sAttrDefValue ) {
			// читаем атрибут тега из настроек
			string sAttrValue = sAttrDefValue;
			if( File.Exists( GetSettingsPath() ) ) {
				XmlReaderSettings settings = new XmlReaderSettings();
				settings.IgnoreWhitespace = true;
				using ( XmlReader reader = XmlReader.Create( GetSettingsPath(), settings ) ) {
					reader.ReadToFollowing(sTag);
					if ( reader.HasAttributes ) {
						string s = reader.GetAttribute( sAttr );
						if( s != null ) {
							sAttrValue = s;
						}
					}
					reader.Close();
				}
			}
			return sAttrValue;
		}
		private static Int16 ReadAttribute( string sTag, string sAttr, Int16 nAttrDefValue ) {
			// читаем атрибут тега из настроек
			Int16 nAttrValue = nAttrDefValue;
			if( File.Exists( GetSettingsPath() ) ) {
				XmlReaderSettings settings = new XmlReaderSettings();
				settings.IgnoreWhitespace = true;
				using ( XmlReader reader = XmlReader.Create( GetSettingsPath(), settings ) ) {
					reader.ReadToFollowing(sTag);
					if ( reader.HasAttributes ) {
						string s = reader.GetAttribute( sAttr );
						if( s != null ) {
							nAttrValue = Convert.ToInt16( s );
						}
					}
					reader.Close();
				}
			}
			return nAttrValue;
		}
		private static bool ReadAttribute( string sTag, string sAttr, bool bAttrDefValue ) {
			// читаем атрибут тега из настроек
			bool bAttrValue = bAttrDefValue;
			if( File.Exists( GetSettingsPath() ) ) {
				XmlReaderSettings settings = new XmlReaderSettings();
				settings.IgnoreWhitespace = true;
				using ( XmlReader reader = XmlReader.Create( GetSettingsPath(), settings ) ) {
					reader.ReadToFollowing(sTag);
					if ( reader.HasAttributes ) {
						string s = reader.GetAttribute( sAttr );
						if( s != null ) {
							bAttrValue = Convert.ToBoolean( s );
						}
					}
					reader.Close();
				}
			}
			return bAttrValue;
		}
		#endregion
		
		#region Открытые статические члены-данные класса
		#region Общее
		public static void SetProgDir( string sProgDir ) {
			m_sProgDir = sProgDir;
		}

		public static string GetProgDir() {
			return m_sProgDir;
		}
		
		public static string GetTempDir() {
			// возвращает временную папку
			return m_sTempDir;
		}
		
		public static string GetFB21SchemePath() {
			// возвращает путь к схеме fb2.1
			return m_sFB21SchemePath;
		}
		
		public static string GetSettingsPath() {
			return m_settings;
		}
		
		public static string GetDefTFB2Path() {
			return m_sTFB2Path;
		}
		
		public static string GetDefWinRARPath() {
			return m_sWinRarPath;
		}
		
		public static string GetDefRarPath() {
			return m_sRarPath;
		}
		
		public static string GetDefUnRARPath() {
			return m_sUnRARPath;
		}		
		
		public static string GetDef7zaPath() {
			return m_s7zaPath;
		}
		
		public static string GetDefFBEPath() {
			return m_sFBEPath;
		}
		
		public static string GetDefFBReaderPath() {
			return m_sFBReaderPath;
		}
		
		public static string GetLicensePath() {
			return m_sLicensePath;
		}

		public static string GetChangeFilePath() {
			return m_sChangeFilePath;
		}
		
		////// Чтение из файла настроек данных по конкретному параметру
		
		public static string ReadWinRARPath() {
			// читаем путь к WinRar из настроек
			return ReadAttribute( "WinRar", "WinRarPath", GetDefWinRARPath() );
		}
		public static string ReadRarPath() {
			// читаем путь к консольному Rar из настроек
			return ReadAttribute( "WinRar", "RarPath", GetDefRarPath() );
		}
		public static string ReadUnRarPath() {
			// читаем путь к консольному UnRar из настроек
			return ReadAttribute( "WinRar", "UnRarPath", GetDefUnRARPath() );
		}
		public static string Read7zaPath() {
			// читаем путь к консольному 7za из настроек
			return ReadAttribute( "A7za", "A7zaPath", GetDef7zaPath() );
		}
		
		public static string ReadTextFB2EPath() {
			// читаем путь к текстовому редактору из настроек
			return ReadAttribute( "Editors", "TextFB2EPath", GetDefTFB2Path() );
		}
		
		public static string ReadFBEPath() {
			// читаем путь к FBE из настроек
			return ReadAttribute( "Editors", "FBEPath", GetDefFBEPath() );
		}
		
		public static string ReadFBReaderPath() {
			// читаем путь к читалке из настроек
			return ReadAttribute( "Reader", "FBReaderPath", GetDefFBReaderPath() );
		}
		
		#endregion
				
		#region Валидатор
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
			return Convert.ToInt16( ReadAttribute( "ValidatorDoubleClick", "cboxValidatorForFB2SelectedIndex", GetDefValidatorFB2SelectedIndex().ToString() ) );
		}
		
		public static Int16 ReadValidatorFB2ArchiveSelectedIndex() {
			// читаем номер выделенного итема для комбобокса cboxValidatorForFB2Archive из настроек
			return Convert.ToInt16( ReadAttribute( "ValidatorDoubleClick", "cboxValidatorForFB2ArchiveSelectedIndex", GetDefValidatorFB2ArchiveSelectedIndex().ToString() ) );
		}
		
		public static Int16 ReadValidatorFB2SelectedIndexPE() {
			// читаем номер выделенного итема для комбобокса cboxValidatorForFB2 из настроек для нажатия Enter
			return Convert.ToInt16( ReadAttribute( "ValidatorPressEnter", "cboxValidatorForFB2SelectedIndexPE", GetDefValidatorFB2SelectedIndexPE().ToString() ) );
		}
		
		public static Int16 ReadValidatorFB2ArchiveSelectedIndexPE() {
			// читаем номер выделенного итема для комбобокса cboxValidatorForFB2Archive из настроек для нажатия Enter
			return Convert.ToInt16( ReadAttribute( "ValidatorPressEnter", "cboxValidatorForFB2ArchiveSelectedIndexPE", GetDefValidatorFB2ArchiveSelectedIndexPE().ToString() ) );
		}
		#endregion

		#region Менеджер Архивов
		public static string GetArchiveManagerHelpPath() {
			return m_sArchiveManagerHelpPath;
		}
		#endregion
		
		#region Менеджер Файлов
		public static bool GetDefFMchBoxTranslitCheked() {
			return m_bchBoxTranslitCheked;
		}
		
		public static bool GetDefFMchBoxStrictCheked() {
			return m_bchBoxStrictCheked;
		}
		
		public static Int16 GetDefFMcboxSpaceSelectedIndex() {
			return m_ncboxSpaceSelectedIndex;
		}
		
		public static bool GetDefFMchBoxToArchiveCheked() {
			return m_bchBoxToArchiveCheked;
		}
		
		public static Int16 GetDefFMcboxArchiveTypeSelectedIndex() {
			return m_ncboxArchiveTypeSelectedIndex;
		}
		
		public static Int16 GetDefFMcboxFileExistSelectedIndex() {
			return m_ncboxFileExistSelectedIndex;
		}
				
		public static bool GetDefFMchBoxDelFB2FilesCheked() {
			return m_bchBoxDelFB2FilesCheked;
		}
		
		public static bool GetDefFMrbtnAsIsCheked() {
			return m_brbtnAsIsCheked;
		}
		public static bool GetDefFMrbtnLowerCheked() {
			return m_brbtnLowerCheked;
		}
		public static bool GetDefFMrbtnUpperCheked() {
			return m_brbtnUpperCheked;
		}
		
		public static bool GetDefFMrbtnGenreOneCheked() {
			return m_brbtnGenreOneCheked;
		}
		public static bool GetDefFMrbtnGenreAllCheked() {
			return m_brbtnGenreAllCheked;
		}
		
		public static bool GetDefFMrbtnAuthorOneCheked() {
			return m_brbtnAuthorOneCheked;
		}
		public static bool GetDefFMrbtnAuthorAllCheked() {
			return m_brbtnAuthorAllCheked;
		}
		
		public static bool GetDefFMrbtnGenreSchemaCheked() {
			return m_brbtnGenreSchemaCheked;
		}
		public static bool GetDefFMrbtnGenreTextCheked() {
			return m_brbtnGenreTextCheked;
		}
		public static bool GetDefFMchBoxAddToFileNameBookIDChecked() {
			return m_bchBoxAddToFileNameBookIDChecked;
		}
		
		public static int ReadRegisterMode() {
			// читаем режим для регистра из настроек
			// возврат 0 - как есть; 1 - нижний; 2 - верхний
			bool bAsIs = ReadAttribute( "Register", "rbtnAsIsChecked", GetDefFMrbtnAsIsCheked() );
			bool bLower = ReadAttribute( "Register", "rbtnLowerChecked", GetDefFMrbtnLowerCheked() );
			bool bUpper = ReadAttribute( "Register", "rbtnUpperChecked", GetDefFMrbtnUpperCheked() );
			if( bAsIs ) {
				return 0;
			} else if ( bLower ) {
				return 1;
			} else if ( bUpper ) {
				return 2;
			} else {
				return 0;
			}
		}
		
		public static Int16 ReadSpaceProcessMode() {
			// читаем режим обработки пробелов в строке из настроек
			return ReadAttribute( "Space", "cboxSpaceSelectedIndex", GetDefFMcboxSpaceSelectedIndex() );
		}
		
		public static bool ReadToArchiveMode() {
			// читаем режим упаковки в архив из настроек
			return ReadAttribute( "Archive", "chBoxToArchiveChecked", GetDefFMchBoxToArchiveCheked() );
		}
		
		public static Int16 ReadArchiveTypeMode() {
			// читаем режим типа архивации из настроек
			return ReadAttribute( "Archive", "cboxArchiveTypeSelectedIndex", GetDefFMcboxArchiveTypeSelectedIndex() );
		}
		
		public static Int16 ReadFileExistMode() {
			// читаем режим обработки файлов с одинаковыми именами из настроек
			return ReadAttribute( "IsFileExist", "cboxFileExistSelectedIndex", GetDefFMcboxFileExistSelectedIndex() );
		}
		
		public static bool ReadAddToFileNameBookIDMode() {
			// читаем режим добавления ID книги к имени файла из настроек
			return ReadAttribute( "AddToFileNameBookID", "chBoxAddToFileNameBookIDChecked", GetDefFMchBoxAddToFileNameBookIDChecked() );
		}
		
		public static bool ReadDelFB2FilesMode() {
			// читаем режим удаления файла после сортировки из настроек
			return ReadAttribute( "FileDelete", "chBoxDelFB2FilesChecked", GetDefFMchBoxDelFB2FilesCheked() );
		}
		
		public static bool ReadAuthorOneMode() {
			// читаем режим раскладки файлов по первому автору из настроек
			if( ReadAttribute( "AuthorsToDirs", "rbtnAuthorOneChecked", GetDefFMrbtnAuthorOneCheked() ) ) {
				return true;
			} else {
				return false;
			}
		}
		#endregion
		
		#endregion
		
		#region Настройки для папок Валидатора
		#region Закрытые статические члены-данные класса для Папок Валидатор
		private static string m_sValidatorDirsSettingsPath = GetProgDir()+"\\ValidatorDirs.xml";
		private static string m_sScanDir = "";
		private static string m_sFB2NotValidDirCopyTo = "";
		private static string m_sFB2NotValidDirMoveTo = "";
		private static string m_sFB2ValidDirCopyTo = "";
		private static string m_sFB2ValidDirMoveTo = "";
		private static string m_sNotFB2DirCopyTo = "";
		private static string m_sNotFB2DirMoveTo = "";
		#endregion
		
		#region Открытые статические члены-данные класса для Папок Валидатор
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
		#endregion
	}
}