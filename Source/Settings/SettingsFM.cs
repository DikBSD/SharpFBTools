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
using System.Xml;

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
		private static bool m_rbtnAsSentenceCheked	= false;
		private static bool m_brbtnLowerCheked	= false;
		private static bool m_brbtnUpperCheked	= false;
		private static bool m_brbtnGenreOneCheked	= true;
		private static bool m_brbtnGenreAllCheked	= false;
		private static bool m_brbtnAuthorOneCheked	= true;
		private static bool m_brbtnAuthorAllCheked	= false;
		private static bool m_brbtnGenreSchemaCheked	= true;
		private static bool m_brbtnGenreTextCheked		= false;
		private static bool m_brbtnFMAllFB2Cheked		= true;
		private static bool m_brbtnFMOnleValidFB2Cheked	= false;
		private static bool m_bchBoxAddToFileNameBookIDChecked = false;
		// внешний вид ToolButtons инструмента
		private static string m_cboxDSFileManagerText	= "ImageAndText";
		private static string m_cboxTIRFileManagerText	= "ImageBeforeText";
		// пути к файлам-справкам
		private static string m_sFileManagerHelpPath	= Settings.GetProgDir()+"\\Help\\FileManagerHelp.rtf";
		private static string m_sDescTemplatePath		= Settings.GetProgDir()+"\\Help\\TemplatesDescription.rtf";
		// папки для "проблемных" файлов
		private static string m_sFMFB2NotReadDir	= Settings.GetProgDir()+"\\_NotReadFB2";
		private static string m_sFMFB2LongPathDir	= Settings.GetProgDir()+"\\_FB2LongPath";
		private static string m_sFMFB2NotValidDir	= Settings.GetProgDir()+"\\_NotValidFB2";
		private static string m_sFMArchNotOpenDir	= Settings.GetProgDir()+"\\_NotOpenArchive";
		// названия папок для шаблонных тэгов без данных
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
		private static string m_sFMNoDateText	= "Даты (Текст) Нет";
		private static string m_sFMNoDateValue	= "Даты (Значение) Нет";
		private static string m_sFMNoYear		= "Года издания Нет";
		private static string m_sFMNoPublisher	= "Издательства Нет";
		private static string m_sFMNoCity		= "Города Издания Нет";
		private static string m_sFMNoFB2FirstName	= "Имени fb2-создателя Нет";
		private static string m_sFMNoFB2MiddleName	= "Отчества fb2-создателя Нет";
		private static string m_sFMNoFB2LastName	= "Фамилия fb2-создателя Нет";
		private static string m_sFMNoFB2NickName	= "Ника fb2-создателя Нет";
		// название Групп Жанров
		private static string m_sFMsf			= "Фантастика, Фэнтэзи";
		private static string m_sFMdetective	= "Детективы, Боевики";
		private static string m_sFMprose		= "Проза";
		private static string m_sFMlove			= "Любовные романы";
		private static string m_sFMadventure	= "Приключения";
		private static string m_sFMchildren		= "Детское";
		private static string m_sFMpoetry		= "Поэзия, Драматургия";
		private static string m_sFMantique		= "Старинное";
		private static string m_sFMscience		= "Наука, Образование";
		private static string m_sFMcomputers	= "Компьютеры";
		private static string m_sFMreference	= "Справочники";
		private static string m_sFMnonfiction	= "Документальное";
		private static string m_sFMreligion		= "Религия";
		private static string m_sFMhumor		= "Юмор";
		private static string m_sFMhome			= "Дом, Семья";
		private static string m_sFMbusiness		= "Бизнес";
		// рабочие папки и данные для Полной Сортировки
		private static string m_sFMDataScanDir		= "";
		private static string m_sFMDataTargetDir	= "";
		private static string m_sFMDataTemplate		= "";
		// рабочие папки и данные для Избранной Сортировки
		private static string m_sFMDataSSScanDir	= "";
		private static string m_sFMDataSSTargetDir	= "";
		private static string m_sFMDataSSTemplate	= "";
		
		#endregion
		
		public SettingsFM()
		{
		}
		
		#region Открытые статические методы класса
		public static void SetToolButtonsSettings( ToolStrip ts ) {
			// задание для кнопок ToolStrip стиля и положения текста и картинки
			Settings.SetToolButtonsSettings( "FileManagerToolButtons", "cboxDSFileManagerText", "cboxTIRFileManagerText", ts );
		}
		
		public static string GetDefFMcboxDSFileManagerText() {
			return m_cboxDSFileManagerText;
		}
		public static string GetDefFMcboxTIRFileManagerText() {
			return m_cboxTIRFileManagerText;
		}
		
		public static bool GetDefFMrbtnFMAllFB2Cheked() {
			return m_brbtnFMAllFB2Cheked;
		}
		public static bool GetDefFMrbtnFMOnleValidFB2Cheked() {
			return m_brbtnFMOnleValidFB2Cheked;
		}
		
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
		
		public static string GetDefFMFB2NotValidDir() {
			return m_sFMFB2NotValidDir;
		}
		
		public static string GetDefFMArchNotOpenDir() {
			return m_sFMArchNotOpenDir;
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
		public static bool GetDefFMrbtnAsSentenceCheked() {
			return m_rbtnAsSentenceCheked;
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
		
		public static bool ReadSortValidType() {
			return Settings.ReadAttribute( "SortType", "rbtnFMAllFB2Checked", GetDefFMrbtnFMAllFB2Cheked() );
		}
		
		public static bool ReadFMGenresScheme() {
			return Settings.ReadAttribute( "FMGenresScheme", "rbtnFMFB21Checked", GetDefFMrbtnGenreFB21Cheked() );
		}
		
		public static string ReadFMFB2NotReadDir() {
			return Settings.ReadAttribute( "FB2NotReadDir", "txtBoxFB2NotReadDir", GetDefFMFB2NotReadDir() );
		}
		public static string ReadFMFB2LongPathDir() {
			return Settings.ReadAttribute( "FB2LongPathDir", "txtBoxFB2LongPathDir", GetDefFMFB2LongPathDir() );
		}
		public static string ReadFMFB2NotValidDir() {
			return Settings.ReadAttribute( "FB2NotValidDir", "txtBoxFB2NotValidDir", GetDefFMFB2NotValidDir() );
		}
		public static string ReadFMArchNotOpenDir() {
			return Settings.ReadAttribute( "ArchNotOpenDir", "txtBoxArchNotOpenDir", GetDefFMArchNotOpenDir() );
		}
		
		public static bool ReadRegisterAsIsChecked() {
			// читаем режим для регистра Как есть
			return Settings.ReadAttribute( "Register", "rbtnAsIsChecked", GetDefFMrbtnAsIsCheked() );
		}
		public static bool ReadRegisterAsSentenceChecked() {
			// читаем режим для регистра Как в предложениях
			return Settings.ReadAttribute( "Register", "rbtnAsSentenceChecked", GetDefFMrbtnAsSentenceCheked() );
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
			// возврат 0 - КАК есть; 1 - Как в предложении; 2 - нижний; 3 - ВЕРХНИЙ
			bool bAsIs		= Settings.ReadAttribute( "Register", "rbtnAsIsChecked", GetDefFMrbtnAsIsCheked() );
			bool bSentence	= Settings.ReadAttribute( "Register", "rbtnAsSentenceChecked", GetDefFMrbtnAsSentenceCheked() );
			bool bLower		= Settings.ReadAttribute( "Register", "rbtnLowerChecked", GetDefFMrbtnLowerCheked() );
			bool bUpper		= Settings.ReadAttribute( "Register", "rbtnUpperChecked", GetDefFMrbtnUpperCheked() );
			if( bAsIs ) {
				return 0;
			} else if ( bLower ) {
				return 1;
			} else if ( bUpper ) {
				return 2;
			} else if ( bSentence ) {
				return 3;
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
		public static string GetDefFMNoDateText() {
			return m_sFMNoDateText;
		}
		public static string GetDefFMNoDateValue() {
			return m_sFMNoDateValue;
		}
		public static string GetDefFMNoYear() {
			return m_sFMNoYear;
		}
		public static string GetDefFMNoPublisher() {
			return m_sFMNoPublisher;
		}
		public static string GetDefFMNoCity() {
			return m_sFMNoCity;
		}
		public static string GetDefFMNoFB2FirstName() {
			return m_sFMNoFB2FirstName;
		}
		public static string GetDefFMNoFB2MiddleName() {
			return m_sFMNoFB2MiddleName;
		}
		public static string GetDefFMNoFB2LastName() {
			return m_sFMNoFB2LastName;
		}
		public static string GetDefFMNoFB2NickName() {
			return m_sFMNoFB2NickName;
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
		public static string ReadFMNoDateText() {
			return Settings.ReadAttribute( "TagsNoText", "txtBoxFMNoDateText", GetDefFMNoDateText() );
		}
		public static string ReadFMNoDateValue() {
			return Settings.ReadAttribute( "TagsNoText", "txtBoxFMNoDateValue", GetDefFMNoDateValue() );
		}
		public static string ReadFMNoYear() {
			return Settings.ReadAttribute( "TagsNoText", "txtBoxFMNoYear", GetDefFMNoYear() );
		}
		public static string ReadFMNoPublisher() {
			return Settings.ReadAttribute( "TagsNoText", "txtBoxFMNoPublisher", GetDefFMNoPublisher() );
		}
		public static string ReadFMNoCity() {
			return Settings.ReadAttribute( "TagsNoText", "txtBoxFMNoCity", GetDefFMNoCity() );
		}
		public static string ReadFMNoFB2FirstName() {
			return Settings.ReadAttribute( "TagsNoText", "txtBoxFMNoFB2FirstName", GetDefFMNoFB2FirstName() );
		}
		public static string ReadFMNoFB2MiddleName() {
			return Settings.ReadAttribute( "TagsNoText", "txtBoxFMNoFB2MiddleName", GetDefFMNoFB2MiddleName() );
		}
		public static string ReadFMNoFB2LastName() {
			return Settings.ReadAttribute( "TagsNoText", "txtBoxFMNoFB2LastName", GetDefFMNoFB2LastName() );
		}
		public static string ReadFMNoFB2NickName() {
			return Settings.ReadAttribute( "TagsNoText", "txtBoxFMNoFB2NickName", GetDefFMNoFB2NickName() );
		}
		
		// название папок Групп Жанров
		public static string GetDefFMGenresGroupSf() {
			return m_sFMsf;
		}
		public static string GetDefFMGenresGroupDetective() {
			return m_sFMdetective;
		}
		public static string GetDefFMGenresGroupProse() {
			return m_sFMprose;
		}
		public static string GetDefFMGenresGroupLove() {
			return m_sFMlove;
		}
		public static string GetDefFMGenresGroupAdventure() {
			return m_sFMadventure;
		}
		public static string GetDefFMGenresGroupChildren() {
			return m_sFMchildren;
		}
		public static string GetDefFMGenresGroupPoetry() {
			return m_sFMpoetry;
		}
		public static string GetDefFMGenresGroupAntique() {
			return m_sFMantique;
		}
		public static string GetDefFMGenresGroupScience() {
			return m_sFMscience;
		}
		public static string GetDefFMGenresGroupComputers() {
			return m_sFMcomputers;
		}
		public static string GetDefFMGenresGroupReference() {
			return m_sFMreference;
		}
		public static string GetDefFMGenresGroupNonfiction() {
			return m_sFMnonfiction;
		}
		public static string GetDefFMGenresGroupReligion() {
			return m_sFMreligion;
		}
		public static string GetDefFMGenresGroupHumor() {
			return m_sFMhumor;
		}
		public static string GetDefFMGenresGroupHome() {
			return m_sFMhome;
		}
		public static string GetDefFMGenresGroupBusiness() {
			return m_sFMbusiness;
		}
		// чтение названий Групп Жанров
		public static string ReadFMSf() {
			return Settings.ReadAttribute( "GenresGroups", "txtboxFMsf", GetDefFMGenresGroupSf() );
		}
		public static string ReadFMDetective() {
			return Settings.ReadAttribute( "GenresGroups", "txtboxFMdetective", GetDefFMGenresGroupDetective() );
		}
		public static string ReadFMProse() {
			return Settings.ReadAttribute( "GenresGroups", "txtboxFMprose", GetDefFMGenresGroupProse() );
		}
		public static string ReadFMLove() {
			return Settings.ReadAttribute( "GenresGroups", "txtboxFMlove", GetDefFMGenresGroupLove() );
		}
		public static string ReadFMAdventure() {
			return Settings.ReadAttribute( "GenresGroups", "txtboxFMadventure", GetDefFMGenresGroupAdventure() );
		}
		public static string ReadFMChildren() {
			return Settings.ReadAttribute( "GenresGroups", "txtboxFMchildren", GetDefFMGenresGroupChildren() );
		}
		public static string ReadFMPoetry() {
			return Settings.ReadAttribute( "GenresGroups", "txtboxFMpoetry", GetDefFMGenresGroupPoetry() );
		}
		public static string ReadFMAntique() {
			return Settings.ReadAttribute( "GenresGroups", "txtboxFMantique", GetDefFMGenresGroupAntique() );
		}
		public static string ReadFMScience() {
			return Settings.ReadAttribute( "GenresGroups", "txtboxFMscience", GetDefFMGenresGroupScience() );
		}
		public static string ReadFMComputers() {
			return Settings.ReadAttribute( "GenresGroups", "txtboxFMcomputers", GetDefFMGenresGroupComputers() );
		}
		public static string ReadFMReference() {
			return Settings.ReadAttribute( "GenresGroups", "txtboxFMreference", GetDefFMGenresGroupReference() );
		}
		public static string ReadFMNonfiction() {
			return Settings.ReadAttribute( "GenresGroups", "txtboxFMnonfiction", GetDefFMGenresGroupNonfiction() );
		}
		public static string ReadFMReligion() {
			return Settings.ReadAttribute( "GenresGroups", "txtboxFMreligion", GetDefFMGenresGroupReligion() );
		}
		public static string ReadFMHumor() {
			return Settings.ReadAttribute( "GenresGroups", "txtboxFMhumor", GetDefFMGenresGroupHumor() );
		}
		public static string ReadFMHome() {
			return Settings.ReadAttribute( "GenresGroups", "txtboxFMhome", GetDefFMGenresGroupHome() );
		}
		public static string ReadFMBusiness() {
			return Settings.ReadAttribute( "GenresGroups", "txtboxFMbusiness", GetDefFMGenresGroupBusiness() );
		}
		
		
		// заполнение индикатора настроек
		public static void SetInfoSettings( ListView lv ) {
			// загружаем в ListView-индикатор настроек данные 
			// регистр
			if( ReadRegisterLowerChecked() ) {
				lv.Items[0].SubItems[1].Text = "строчные";
			} else if( ReadRegisterUpperChecked() ) {
				lv.Items[0].SubItems[1].Text = "ПРОПИСНЫЕ";
			} else if( ReadRegisterAsSentenceChecked() ) {
				lv.Items[0].SubItems[1].Text = "Каждое Слово С Большой Буквы";
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
			lv.Items[43].SubItems[1].Text = ReadFMNoDateText();
			lv.Items[44].SubItems[1].Text = ReadFMNoDateValue();
			lv.Items[45].SubItems[1].Text = ReadFMNoYear();
			lv.Items[46].SubItems[1].Text = ReadFMNoPublisher();
			lv.Items[47].SubItems[1].Text = ReadFMNoCity();
			lv.Items[48].SubItems[1].Text = ReadFMNoFB2FirstName();
			lv.Items[49].SubItems[1].Text = ReadFMNoFB2MiddleName();
			lv.Items[50].SubItems[1].Text = ReadFMNoFB2LastName();
			lv.Items[51].SubItems[1].Text = ReadFMNoFB2NickName();
			// тип сортировки - или Все файлы, или Только валидные
			if( ReadSortValidType() ) {
				lv.Items[24].SubItems[1].Text = "Любые fb2-файлы";
			} else {
				lv.Items[24].SubItems[1].Text = "Только Валидные файлы";
			}
			// папка для невалидных fb2
			lv.Items[25].SubItems[1].Text = ReadFMFB2NotValidDir();
			// папка для "битых" архивов
			lv.Items[26].SubItems[1].Text = ReadFMArchNotOpenDir();
			// названия Групп Жанров
			lv.Items[27].SubItems[1].Text = ReadFMSf();
			lv.Items[28].SubItems[1].Text = ReadFMDetective();
			lv.Items[29].SubItems[1].Text = ReadFMProse();
			lv.Items[30].SubItems[1].Text = ReadFMLove();
			lv.Items[31].SubItems[1].Text = ReadFMAdventure();
			lv.Items[32].SubItems[1].Text = ReadFMChildren();
			lv.Items[33].SubItems[1].Text = ReadFMPoetry();
			lv.Items[34].SubItems[1].Text = ReadFMAntique();
			lv.Items[35].SubItems[1].Text = ReadFMScience();
			lv.Items[36].SubItems[1].Text = ReadFMComputers();
			lv.Items[37].SubItems[1].Text = ReadFMReference();
			lv.Items[38].SubItems[1].Text = ReadFMNonfiction();
			lv.Items[39].SubItems[1].Text = ReadFMReligion();
			lv.Items[40].SubItems[1].Text = ReadFMHumor();
			lv.Items[41].SubItems[1].Text = ReadFMHome();
			lv.Items[42].SubItems[1].Text = ReadFMBusiness();
			
		}
		#endregion
		
		#region Открытые статические свойства класса для данных Менеджера Файлов
		public static string FMDataScanDir {
			get { return m_sFMDataScanDir; }
			set { m_sFMDataScanDir = value; }
		}
		public static string FMDataTargetDir {
			get { return m_sFMDataTargetDir; }
			set { m_sFMDataTargetDir = value; }
		}
		public static string FMDataTemplate {
			get { return m_sFMDataTemplate; }
			set { m_sFMDataTemplate = value; }
		}
		
		public static string FMDataSSScanDir {
			get { return m_sFMDataSSScanDir; }
			set { m_sFMDataSSScanDir = value; }
		}
		public static string FMDataSSTargetDir {
			get { return m_sFMDataSSTargetDir; }
			set { m_sFMDataSSTargetDir = value; }
		}
		public static string FMDataSSTemplate {
			get { return m_sFMDataSSTemplate; }
			set { m_sFMDataSSTemplate = value; }
		}
		#endregion
		
	}
}
