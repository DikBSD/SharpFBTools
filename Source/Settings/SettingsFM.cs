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
		private static bool m_bchBoxTranslitCheked	= false;
		private static bool m_bchBoxStrictCheked	= false;
		private static Int16 m_ncboxSpaceSelectedIndex	= 0;
		private static Int16 m_ncboxFileExistSelectedIndex	= 1;
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
		private static bool m_brbtnFMOnlyValidFB2Cheked	= false;
		// внешний вид ToolButtons инструмента
		private static string m_cboxDSFileManagerText	= "ImageAndText";
		private static string m_cboxTIRFileManagerText	= "ImageBeforeText";
		// папки для "проблемных" файлов
		private static string m_sFMFB2NotReadDir	= "_'Не читаемые' fb2 файлы";
		private static string m_sFMFB2LongPathDir	= "_fb2 c длинными путями";
		private static string m_sFMFB2NotValidDir	= "_Не валидные fb2 файлы";
		private static string m_sFMArchNotOpenDir	= "_'Битые' fb2 архивы, не fb2 архивы";
		// названия папок для шаблонных тэгов без данных
		private static string m_sFMNoGenreGroup	= "_Нестандартные Жанры";
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
		private static string m_sFMtech			= "Техника";
		private static string m_sFMmilitary		= "Военное дело";
		private static string m_sFMfolklore		= "Фольклор";
		private static string m_sFMother		= "Прочее";
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
		public static bool GetDefFMrbtnFMOnlyValidFB2Cheked() {
			return m_brbtnFMOnlyValidFB2Cheked;
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
		
		public static bool GetDefFMchBoxTranslitCheked() {
			return m_bchBoxTranslitCheked;
		}
		
		public static bool GetDefFMchBoxStrictCheked() {
			return m_bchBoxStrictCheked;
		}
		
		public static Int16 GetDefFMcboxSpaceSelectedIndex() {
			return m_ncboxSpaceSelectedIndex;
		}
		
		public static Int16 GetDefFMcboxFileExistSelectedIndex() {
			return m_ncboxFileExistSelectedIndex;
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
		
		public static bool ReadSortValidType() {
			return Settings.ReadAttribute( "SortType", "rbtnFMAllFB2Checked", GetDefFMrbtnFMAllFB2Cheked() );
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
		
		public static Int16 ReadFileExistMode() {
			// читаем режим обработки файлов с одинаковыми именами из настроек
			return Settings.ReadAttribute( "IsFileExist", "cboxFileExistSelectedIndex", GetDefFMcboxFileExistSelectedIndex() );
		}
		public static string ReadFileExistText() {
			// читаем режим обработки файлов с одинаковыми именами (текст) из настроек
			return Settings.ReadAttribute( "IsFileExist", "cboxFileExistText", "Добавить к создаваемому файлу дату и время" );
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
		public static string GetDefFMGenresGroupTech() {
			return m_sFMtech;
		}
		public static string GetDefFMGenresGroupMilitary() {
			return m_sFMmilitary;
		}
		public static string GetDefFMGenresGroupFolklore() {
			return m_sFMfolklore;
		}
		public static string GetDefFMGenresGroupOther() {
			return m_sFMother;
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
		public static string ReadFMTech() {
			return Settings.ReadAttribute( "GenresGroups", "txtboxFMtech", GetDefFMGenresGroupTech() );
		}
		public static string ReadFMMilitary() {
			return Settings.ReadAttribute( "GenresGroups", "txtboxFMmilitary", GetDefFMGenresGroupMilitary() );
		}
		public static string ReadFMFolklore() {
			return Settings.ReadAttribute( "GenresGroups", "txtboxFMfolklore", GetDefFMGenresGroupFolklore() );
		}
		public static string ReadFMOther() {
			return Settings.ReadAttribute( "GenresGroups", "txtboxFMother", GetDefFMGenresGroupOther() );
		}
		#endregion
	}
}
