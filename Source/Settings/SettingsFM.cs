/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 14.05.2009
 * Time: 17:06
 * 
 * License: GPL 2.1
 */
using System;
using System.Windows.Forms;

namespace Settings
{
	/// <summary>
	/// Description of SettingsFM.
	/// </summary>
	public class SettingsFM
	{
		#region Закрытые статические данные класса
		private static bool m_rbtnGenreFB21Cheked	= true;
		private static bool m_rbtnGenreFB22Cheked	= false;
		private static bool m_bchBoxTranslitCheked	= false;
		private static bool m_bchBoxStrictCheked	= false;
		private static Int16 m_ncboxSpaceSelectedIndex	= 0;
		private static bool m_bchBoxToArchiveCheked		= false;
		private static Int16 m_ncboxArchiveTypeSelectedIndex	= 1;
		private static Int16 m_ncboxFileExistSelectedIndex		= 1;
		private static bool m_bchBoxDelFB2FilesCheked	= false;
		private static bool m_brbtnAsIsCheked	= true;
		private static bool m_brbtnLowerCheked	= false;
		private static bool m_brbtnUpperCheked	= false;
		private static bool m_brbtnGenreOneCheked	= true;
		private static bool m_brbtnGenreAllCheked	= false;
		private static bool m_brbtnAuthorOneCheked	= true;
		private static bool m_brbtnAuthorAllCheked	= false;
		private static bool m_brbtnGenreSchemaCheked	= true;
		private static bool m_brbtnGenreTextCheked		= false;
		private static bool m_bchBoxAddToFileNameBookIDChecked = false;
		private static string m_sFileManagerHelpPath	= Settings.GetProgDir()+"\\Help\\FileManagerHelp.rtf";
		private static string m_sDescTemplatePath	= Settings.GetProgDir()+"\\Help\\TemplatesDescription.rtf";
		private static string m_sFMFB2NotReadDir	= Settings.GetProgDir()+"\\_NotReadFB2";
		private static string m_sFMFB2LongPathDir	= Settings.GetProgDir()+"\\_FB2LongPathDir";
		//
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
		
		public SettingsFM()
		{
		}
		
		#region Открытые статические члены-данные класса
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
			return Settings.ReadAttribute( "FMGenresScheme", "rbtnFMFB21Checked", GetDefFMrbtnGenreFB21Cheked() );
		}
		public static string ReadFMFB2NotReadDir() {
			// читаем режим для регистра Как есть
			return Settings.ReadAttribute( "FB2NotReadDir", "txtBoxFB2NotReadDir", GetDefFMFB2NotReadDir() );
		}
		public static string ReadFMFB2LongPathDir() {
			// читаем режим для регистра Как есть
			return Settings.ReadAttribute( "FB2LongPathDir", "txtBoxFB2LongPathDir", GetDefFMFB2LongPathDir() );
		}
		
		public static bool ReadRegisterAsIsChecked() {
			// читаем режим для регистра Как есть
			return Settings.ReadAttribute( "Register", "rbtnAsIsChecked", GetDefFMrbtnAsIsCheked() );
		}
		public static bool ReadRegisterLowerChecked() {
			// читаем режим для Нижнего регистра
			return Settings.ReadAttribute( "Register", "rbtnLowerChecked", GetDefFMrbtnLowerCheked() );
		}
		public static bool ReadRegisterUpperChecked() {
			// читаем режим для Верхнего регистра
			return Settings.ReadAttribute( "Register", "rbtnUpperChecked", GetDefFMrbtnUpperCheked() );
		}
		
		public static int ReadRegisterMode() {
			// читаем режим для регистра из настроек
			// возврат 0 - как есть; 1 - нижний; 2 - верхний
			bool bAsIs	= Settings.ReadAttribute( "Register", "rbtnAsIsChecked", GetDefFMrbtnAsIsCheked() );
			bool bLower	= Settings.ReadAttribute( "Register", "rbtnLowerChecked", GetDefFMrbtnLowerCheked() );
			bool bUpper	= Settings.ReadAttribute( "Register", "rbtnUpperChecked", GetDefFMrbtnUpperCheked() );
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
			return Settings.ReadAttribute( "Space", "cboxSpaceSelectedIndex", GetDefFMcboxSpaceSelectedIndex() );
		}
		public static string ReadSpaceProcessModeText() {
			// читаем режим обработки пробелов в строке (текст) из настроек
			return Settings.ReadAttribute( "Space", "cboxSpaceText", "Оставить" );
		}
		
		public static bool ReadToArchiveMode() {
			// читаем режим упаковки в архив из настроек
			return Settings.ReadAttribute( "Archive", "chBoxToArchiveChecked", GetDefFMchBoxToArchiveCheked() );
		}
		public static Int16 ReadArchiveTypeMode() {
			// читаем режим типа архивации из настроек
			return Settings.ReadAttribute( "Archive", "cboxArchiveTypeSelectedIndex", GetDefFMcboxArchiveTypeSelectedIndex() );
		}
		public static string ReadArchiveTypeText() {
			// читаем вид архивации из настроек
			return Settings.ReadAttribute( "Archive", "cboxArchiveTypeText", "Zip" );
		}
		
		public static Int16 ReadFileExistMode() {
			// читаем режим обработки файлов с одинаковыми именами из настроек
			return Settings.ReadAttribute( "IsFileExist", "cboxFileExistSelectedIndex", GetDefFMcboxFileExistSelectedIndex() );
		}
		public static string ReadFileExistText() {
			// читаем режим обработки файлов с одинаковыми именами (текст) из настроек
			return Settings.ReadAttribute( "IsFileExist", "cboxFileExistText", "Добавить к создаваемому файлу дату и время" );
		}
		
		public static bool ReadAddToFileNameBookIDMode() {
			// читаем режим добавления ID книги к имени файла из настроек
			return Settings.ReadAttribute( "AddToFileNameBookID", "chBoxAddToFileNameBookIDChecked", GetDefFMchBoxAddToFileNameBookIDChecked() );
		}
		
		public static bool ReadDelFB2FilesMode() {
			// читаем режим удаления файла после сортировки из настроек
			return Settings.ReadAttribute( "FileDelete", "chBoxDelFB2FilesChecked", GetDefFMchBoxDelFB2FilesCheked() );
		}
		
		public static bool ReadAuthorOneMode() {
			// читаем режим раскладки файлов по первому автору из настроек
			return Settings.ReadAttribute( "AuthorsToDirs", "rbtnAuthorOneChecked", GetDefFMrbtnAuthorOneCheked() );
		}
		
		public static bool ReadGenreOneMode() {
			// читаем режим раскладки файлов по первому жанру из настроек
			return Settings.ReadAttribute( "GenresToDirs", "rbtnGenreOneChecked", GetDefFMrbtnGenreOneCheked() );
		}
		
		public static bool ReadGenreTypeMode() {
			// читаем вид папки с жанром из настроек
			return Settings.ReadAttribute( "GenresType", "rbtnGenreSchemaChecked", GetDefFMrbtnGenreSchemaCheked() );
		}
		
		public static bool ReadTranslitMode() {
			// читаем режим транслитерации из настроек
			return Settings.ReadAttribute( "Translit", "chBoxTranslitChecked", GetDefFMchBoxTranslitCheked() );
		}
		
		public static bool ReadStrictMode() {
			// читаем режим "Строгих" имен из настроек
			return Settings.ReadAttribute( "Strict", "chBoxStrictChecked", GetDefFMchBoxStrictCheked() );
		}
		
		
		// для папок тэгов, данных для которых нет
		public static string GetDefFMNoGenreGroup() {
			return m_sFMNoGenreGroup;
		}
		public static string GetDefFMNoGenre() {
			return m_sFMNoGenre;
		}
		public static string GetDefFMNoLang() {
			return m_sFMNoLang;
		}
		public static string GetDefFMNoFirstName() {
			return m_sFMNoFirstName;
		}
		public static string GetDefFMNoMiddleName() {
			return m_sFMNoMiddleName;
		}
		public static string GetDefFMNoLastName() {
			return m_sFMNoLastName;
		}
		public static string GetDefFMNoNickName() {
			return m_sFMNoNickName;
		}
		public static string GetDefFMNoBookTitle() {
			return m_sFMNoBookTitle;
		}
		public static string GetDefFMNoSequence() {
			return m_sFMNoSequence;
		}
		public static string GetDefFMNoNSequence() {
			return m_sFMNoNSequence;
		}
		
		// чтение названий папок тэгов, данных у которых нет
		public static string ReadFMNoGenreGroup() {
			return Settings.ReadAttribute( "TagsNoText", "txtBoxFMNoGenreGroup", GetDefFMNoGenreGroup() );
		}
		public static string ReadFMNoGenre() {
			return Settings.ReadAttribute( "TagsNoText", "txtBoxFMNoGenre", GetDefFMNoGenre() );
		}
		public static string ReadFMNoLang() {
			return Settings.ReadAttribute( "TagsNoText", "txtBoxFMNoLang", GetDefFMNoLang() );
		}
		public static string ReadFMNoFirstName() {
			return Settings.ReadAttribute( "TagsNoText", "txtBoxFMNoFirstName", GetDefFMNoFirstName() );
		}
		public static string ReadFMNoMiddleName() {
			return Settings.ReadAttribute( "TagsNoText", "txtBoxFMNoMiddleName", GetDefFMNoMiddleName() );
		}
		public static string ReadFMNoLastName() {
			return Settings.ReadAttribute( "TagsNoText", "txtBoxFMNoLastName", GetDefFMNoLastName() );
		}
		public static string ReadFMNoNickName() {
			return Settings.ReadAttribute( "TagsNoText", "txtBoxFMNoNickName", GetDefFMNoNickName() );
		}
		public static string ReadFMNoBookTitle() {
			return Settings.ReadAttribute( "TagsNoText", "txtBoxFMNoBookTitle", GetDefFMNoBookTitle() );
		}
		public static string ReadFMNoSequence() {
			return Settings.ReadAttribute( "TagsNoText", "txtBoxFMNoSequence", GetDefFMNoSequence() );
		}
		public static string ReadFMNoNSequence() {
			return Settings.ReadAttribute( "TagsNoText", "txtBoxFMNoNSequence", GetDefFMNoNSequence() );
		}
		
		// заполнение индикатора настроек
		public static void SetInfoSettings( ListView lv ) {
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
			// названия папки для тэга. у которого нет данных
			lv.Items[14].SubItems[1].Text = ReadFMNoGenreGroup();
			lv.Items[15].SubItems[1].Text = ReadFMNoGenre();
			lv.Items[16].SubItems[1].Text = ReadFMNoLang();
			lv.Items[17].SubItems[1].Text = ReadFMNoFirstName();
			lv.Items[18].SubItems[1].Text = ReadFMNoMiddleName();
			lv.Items[19].SubItems[1].Text = ReadFMNoLastName();
			lv.Items[20].SubItems[1].Text = ReadFMNoNickName();
			lv.Items[21].SubItems[1].Text = ReadFMNoBookTitle();
			lv.Items[22].SubItems[1].Text = ReadFMNoSequence();
			lv.Items[23].SubItems[1].Text = ReadFMNoNSequence();
		}
		#endregion
		
	}
}
