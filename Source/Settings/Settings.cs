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
using System.Windows.Forms;

namespace Settings
{
	/// <summary>
	/// Description of Settings.
	/// </summary>
	public class Settings
	{
		#region Закрытые статические члены-данные класса
		#region Общее Настройки
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
		
		#region Общие Сообщения
		private static string m_sReady	= "Готово.";
		private static string m_sNoID	= "Id_Нет";
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
		private static bool m_rbtnGenreFB21Cheked = true;
		private static bool m_rbtnGenreFB22Cheked = false;
		private static bool m_bchBoxTranslitCheked = false;
		private static bool m_bchBoxStrictCheked = false;
		private static Int16 m_ncboxSpaceSelectedIndex = 0;
		private static bool m_bchBoxToArchiveCheked = false;
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
		private static string m_sFileManagerHelpPath = GetProgDir()+"\\Help\\FileManagerHelp.rtf";
		private static string m_sDescTemplatePath = GetProgDir()+"\\Help\\TemplatesDescription.rtf";
		private static string m_sFMFB2NotReadDir = GetProgDir()+"\\_NotReadFB2";
		private static string m_sFMFB2LongPathDir = GetProgDir()+"\\_FB2LongPathDir";
		
		#endregion
		
		#region Сообщения Менеджера Файлов
		private static string m_sFMNoGenreGroup	= "Неизвестная Группа Жанров";
		private static string m_sFMNoGenre		= "Жанра Нет";
		private static string m_sFMNoLang		= "Языка Книги Нет";
		private static string m_sFMNoFirstName	= "Имени Автора Нет";
		private static string m_sFMNoMiddleName	= "Отчества Автора Нет";
		private static string m_sFMNoLastName	= "Фамилия Автора Нет";
		private static string m_sFMNoNickName	= "Ника Автора Нет";
		private static string m_sFMNoBookTitle	= "Названия Книги Нет";
		private static string m_sFMNoSequence	= "Серии Нет";
		private static string m_sFMNoNSequence	= "Номера Серии Нет";
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
					try {
						reader.ReadToFollowing(sTag);
						if ( reader.HasAttributes ) {
							string s = reader.GetAttribute( sAttr );
							if( s != null ) {
								sAttrValue = s;
							}
						}
					} catch {
						
					} finally {
						reader.Close();
					}
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
					try {
						reader.ReadToFollowing(sTag);
						if ( reader.HasAttributes ) {
							string s = reader.GetAttribute( sAttr );
							if( s != null ) {
								nAttrValue = Convert.ToInt16( s );
							}
						}
					} catch {
						
					} finally {
						reader.Close();
					}
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
					try {
						reader.ReadToFollowing(sTag);
						if ( reader.HasAttributes ) {
							string s = reader.GetAttribute( sAttr );
							if( s != null ) {
								bAttrValue = Convert.ToBoolean( s );
							}
						}
					} catch {
						
					} finally {
						reader.Close();
					}
				}
			}
			return bAttrValue;
		}
		#endregion
		
		#region Открытые статические члены-данные класса
		#region Общее Настройки
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
		
		#region Общие Сообщения
		public static string GetReady() {
			return m_sReady;
		}
		
		public static string GetNoID() {
			return m_sNoID;
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
		public static bool GetDefFMrbtnGenreFB21Cheked() {
			return m_rbtnGenreFB21Cheked;
		}
		public static bool GetDefFMrbtnGenreFB22Cheked() {
			return m_rbtnGenreFB22Cheked;
		}
		
		public static string GetFileManagerHelpPath() {
			return m_sFileManagerHelpPath;
		}
		
		public static string GetDefFMFB2NotReadDir() {
			return m_sFMFB2NotReadDir;
		}
		
		public static string GetDefFMFB2LongPathDir() {
			return m_sFMFB2LongPathDir;
		}
		
		public static string GetDefFMDescTemplatePath() {
			return m_sDescTemplatePath;
		}
		
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
		
		
		public static bool ReadFMGenresScheme() {
			// читаем режим для регистра Как есть 
			return ReadAttribute( "FMGenresScheme", "rbtnFMFB21Checked", GetDefFMrbtnGenreFB21Cheked() );
		}
		public static string ReadFMFB2NotReadDir() {
			// читаем режим для регистра Как есть
			return ReadAttribute( "FB2NotReadDir", "txtBoxFB2NotReadDir", GetDefFMFB2NotReadDir() );
		}
		public static string ReadFMFB2LongPathDir() {
			// читаем режим для регистра Как есть
			return ReadAttribute( "FB2LongPathDir", "txtBoxFB2LongPathDir", GetDefFMFB2LongPathDir() );
		}
		
		public static bool ReadRegisterAsIsChecked() {
			// читаем режим для регистра Как есть
			return ReadAttribute( "Register", "rbtnAsIsChecked", GetDefFMrbtnAsIsCheked() );
		}
		public static bool ReadRegisterLowerChecked() {
			// читаем режим для Нижнего регистра
			return ReadAttribute( "Register", "rbtnLowerChecked", GetDefFMrbtnLowerCheked() );
		}
		public static bool ReadRegisterUpperChecked() {
			// читаем режим для Верхнего регистра
			return ReadAttribute( "Register", "rbtnUpperChecked", GetDefFMrbtnUpperCheked() );
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
		public static string ReadSpaceProcessModeText() {
			// читаем режим обработки пробелов в строке (текст) из настроек
			return ReadAttribute( "Space", "cboxSpaceText", "Оставить" );
		}
		
		public static bool ReadToArchiveMode() {
			// читаем режим упаковки в архив из настроек
			return ReadAttribute( "Archive", "chBoxToArchiveChecked", GetDefFMchBoxToArchiveCheked() );
		}
		public static Int16 ReadArchiveTypeMode() {
			// читаем режим типа архивации из настроек
			return ReadAttribute( "Archive", "cboxArchiveTypeSelectedIndex", GetDefFMcboxArchiveTypeSelectedIndex() );
		}
		public static string ReadArchiveTypeText() {
			// читаем вид архивации из настроек
			return ReadAttribute( "Archive", "cboxArchiveTypeText", "Zip" );
		}
		
		public static Int16 ReadFileExistMode() {
			// читаем режим обработки файлов с одинаковыми именами из настроек
			return ReadAttribute( "IsFileExist", "cboxFileExistSelectedIndex", GetDefFMcboxFileExistSelectedIndex() );
		}
		public static string ReadFileExistText() {
			// читаем режим обработки файлов с одинаковыми именами (текст) из настроек
			return ReadAttribute( "IsFileExist", "cboxFileExistText", "Добавить к создаваемому файлу дату и время" );
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
			return ReadAttribute( "AuthorsToDirs", "rbtnAuthorOneChecked", GetDefFMrbtnAuthorOneCheked() );
		}
		
		public static bool ReadGenreOneMode() {
			// читаем режим раскладки файлов по первому жанру из настроек
			return ReadAttribute( "GenresToDirs", "rbtnGenreOneChecked", GetDefFMrbtnGenreOneCheked() );
		}
		
		public static bool ReadGenreTypeMode() {
			// читаем вид папки с жанром из настроек
			return ReadAttribute( "GenresType", "rbtnGenreSchemaChecked", GetDefFMrbtnGenreSchemaCheked() );
		}
		
		public static bool ReadTranslitMode()
		{
			// читаем режим транслитерации из настроек
			return ReadAttribute( "Translit", "chBoxTranslitChecked", GetDefFMchBoxTranslitCheked() );
		}
		
		public static bool ReadStrictMode()
		{
			// читаем режим "Строгих" имен из настроек
			return ReadAttribute( "Strict", "chBoxStrictChecked", GetDefFMchBoxStrictCheked() );
		}
		
		public static void SetInfoSettings( ListView lv )
		{
			// загружаем в ListView-индикатор настроек данные 
			// регистр
			if( ReadRegisterLowerChecked() ) {
				lv.Items[0].SubItems[1].Text = "Нижний регистр";
			} else if( ReadRegisterUpperChecked() ) {
				lv.Items[0].SubItems[1].Text = "Верхний регистр";
			} else {
				lv.Items[0].SubItems[1].Text = "Как есть";
			}
			// раскладка файлов по авторам
			if( ReadAuthorOneMode() ) {
				lv.Items[1].SubItems[1].Text = "По первому Автору";
			} else {
				lv.Items[1].SubItems[1].Text = "По всем Авторам";
			}
			// раскладка файлов по жанрам
			if( ReadGenreOneMode() ) {
				lv.Items[2].SubItems[1].Text = "По первому Жанру";
			} else {
				lv.Items[2].SubItems[1].Text = "По всем Жанрам";
			}
			// вид папки с жанрам
			if( ReadGenreTypeMode() ) {
				lv.Items[3].SubItems[1].Text = "Как в схеме (например: prose_rus_classic)";
			} else {
				lv.Items[3].SubItems[1].Text = "Расшифровано (например: Русская классика)";
			}
			// транслитерация
			if( ReadTranslitMode() ) {
				lv.Items[4].SubItems[1].Text = "Да";
			} else {
				lv.Items[4].SubItems[1].Text = "Нет";
			}
			// "Строгие" имена папок и файлов
			if( ReadStrictMode() ) {
				lv.Items[5].SubItems[1].Text = "Да";
			} else {
				lv.Items[5].SubItems[1].Text = "Нет";
			}
			// Обработка пробелов
			lv.Items[6].SubItems[1].Text = ReadSpaceProcessModeText();
			// Упаковка файлов в архив
			if( !ReadToArchiveMode() ) {
				lv.Items[7].SubItems[1].Text = "Нет";
			} else {
				lv.Items[7].SubItems[1].Text = ReadArchiveTypeText();
			}
			// Одинаковые файлы
			lv.Items[8].SubItems[1].Text = ReadFileExistText();
			// добавление ID книги к имени файла
			if( ReadAddToFileNameBookIDMode() ) {
				lv.Items[9].SubItems[1].Text = "Да";
			} else {
				lv.Items[9].SubItems[1].Text = "Нет";
			}
			// удаление исходных файлов после сортировки
			if( ReadDelFB2FilesMode() ) {
				lv.Items[10].SubItems[1].Text = "Да";
			} else {
				lv.Items[10].SubItems[1].Text = "Нет";
			}
			// папки проблемных fb2-файлов
			lv.Items[11].SubItems[1].Text = ReadFMFB2NotReadDir();
			lv.Items[12].SubItems[1].Text = ReadFMFB2LongPathDir();
			// схема Жанров
			if( ReadFMGenresScheme() ) {
				lv.Items[13].SubItems[1].Text = "fb2.1";
			} else {
				lv.Items[13].SubItems[1].Text = "fb2.2";
			}
		}
		
		public static string GetFMNoGenreGroup() {
			return m_sFMNoGenreGroup;
		}
		public static string GetFMNoGenre() {
			return m_sFMNoGenre;
		}
		public static string GetFMNoLang() {
			return m_sFMNoLang;
		}
		public static string GetFMNoFirstName() {
			return m_sFMNoFirstName;
		}
		public static string GetFMNoMiddleName() {
			return m_sFMNoMiddleName;
		}
		public static string GetFMNoLastName() {
			return m_sFMNoLastName;
		}
		public static string GetFMNoNickName() {
			return m_sFMNoNickName;
		}
		public static string GetFMNoBookTitle() {
			return m_sFMNoBookTitle;
		}
		public static string GetFMNoSequence() {
			return m_sFMNoSequence;
		}
		public static string GetFMNoNSequence() {
			return m_sFMNoNSequence;
		}
		
		#endregion

		#endregion

		
		#region Настройки для папок Валидатора
		#region Закрытые статические члены-данные класса для Папок Валидатор
		private static string m_sValidatorDirsSettingsPath = GetProgDir()+"\\ValidatorDirs.xml";
		private static string m_sScanDir 				= "";
		private static string m_sFB2NotValidDirCopyTo	= "";
		private static string m_sFB2NotValidDirMoveTo	= "";
		private static string m_sFB2ValidDirCopyTo		= "";
		private static string m_sFB2ValidDirMoveTo		= "";
		private static string m_sNotFB2DirCopyTo		= "";
		private static string m_sNotFB2DirMoveTo		= "";
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