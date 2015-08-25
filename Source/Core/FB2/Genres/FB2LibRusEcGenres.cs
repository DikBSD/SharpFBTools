/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 23.09.2013
 * Time: 13:15
 * 
 * License: GPL 2.1
 */
using System;
using System.Collections.Generic;

using Core.Common;

using stringProcessing = Core.Common.StringProcessing;

namespace Core.FB2.Genres
{
	/// <summary>
	/// FB2LibRusEcGenres: Жанры Либрусек
	/// </summary>
	public class FB2LibrusecGenres : IFBGenres
	{
		#region Закрытые данные класса
		private readonly Dictionary<string, string> m_dFB2LibRusEcGenres		= new Dictionary<string, string>();
		private readonly Dictionary<string, string> m_dFB2LibRusEcGenresGroup	= new Dictionary<string, string>();
				
		private string[] m_sFB2LibRusEcGenreCode = {
			/*Фантастика:*/
			/*[0]*/"sf", "sf_fantasy", "sf_action", "sf_humor", "sf_history", "sf_social", "sf_space", "sf_detective", "sf_heroic", "sf_epic", "sf_fantasy_city", "sf_etc", "fairy_fantasy", "humor_fantasy", "nsf", "historical_fantasy", "sf_fantasy_irony", "sf_irony", "sf_mystic", "sf_horror", "sf_postapocalyptic", "sf_cyberpunk", "popadanec", "sf_space_opera", "gothic_novel", "sf_stimpank", "sf_technofantasy",
			/*Проза:*/
			/*[27]*/"prose_contemporary", "prose_classic", "prose_rus_classic", "prose_history", "prose_su_classics", "prose_sentimental", "prose_military", "prose", "prose_counter", "roman", "great_story", "short_story", "story", "essay", "epistolary_fiction", "aphorisms", "prose_magic", "dissident", "sagas", "extravaganza", "prose_epic",
			/*Наука, Образование:*/
			/*[48]*/"sci_history", "sci_psychology", "sci_tech", "sci_philosophy", "sci_politics", "sci_culture", "science", "sci_philology", "sci_medicine", "sci_linguistic", "sci_textbook", "sci_religion", "sci_juris", "sci_biology", "sci_math", "sci_phys", "sci_pedagogy", "sci_business", "sci_cosmos", "sci_economy", "sci_medicine_alternative", "sci_geo", "sci_chem", "sci_social_studies", "sci_state", "sci_zoo", "sci_botany", "psy_sex_and_family", "foreign_language", "sci_ecology", "psy_childs", "psy_theraphy", "sci_biochem", "sci_veterinary", "sci_physchem", "sci_orgchem", "sci_biophys", "sci_anachem", "sci_abstract", "sci_crib",
			/*Детективы и Триллеры:*/
			/*[88]*/"detective", "det_classic", "det_action", "det_police", "det_crime", "det_history", "det_irony", "det_espionage", "det_hard", "det_political", "det_cozy", "det_maniac", "thriller", "thriller_legal", "thriller_medical", "thriller_techno",
			/*Документальная литература:*/
			/*[104]*/"nonf_biography", "nonf_publicism", "sci_popular", "nonfiction", "nonf_criticism",
			/*Любовные романы:*/
			/*[109]*/"love_short", "love_contemporary", "love_history", "love_sf", "love_detective", "love", "love_erotica", "love_hard",
			/*Детское:*/
			/*[117]*/"child_tale", "children", "child_prose", "child_sf", "child_det", "child_verse", "child_adv", "ya", "child_folklore", "prose_game", "child_education",
			/*Прочее:*/
			/*[128]*/"periodic", "music", "cine", "theatre", "visual_arts", "notes", "unfinished", "fanfiction", "other",
			/*Домоводство <Дом и семья>:*/
			/*[137]*/"home_sex", "home_health", "home_cooking", "home_diy", "home_sport", "home_pets", "home_garden", "home", "home_entertain", "home_crafts", "home_collecting",
			/*Религия и духовность:*/
			/*[148]*/"religion_rel", "religion", "religion_self", "religion_christianity", "religion_orthodoxy", "religion_catholicism", "religion_protestantism", "religion_islam", "religion_judaism", "religion_budda", "religion_hinduism", "religion_paganism", "astrology", "palmistry", "religion_esoterics",
			/*Приключения:*/
			/*[163]*/"adventure", "adv_history", "adv_geo", "adv_animal", "adv_maritime", "adv_western", "adv_indian",
			/*Юмор:*/
			/*[170]*/"humor_prose", "humor_satire", "humor_verse", "humor", "humor_anecdote",
			/*Поэзия и Драматургия:*/
			/*[175]*/"poetry", "lyrics", "in_verse", "epic_poetry", "song_poetry", "fable", "palindromes", "experimental_poetry", "visual_poetry", "vers_libre", "dramaturgy", "comedy", "drama", "screenplays", "tragedy", "scenarios", "vaudeville", "mystery",
			/*Техника:*/
			/*[193]*/"sci_radio", "sci_transport", "sci_build", "auto_regulations", "architecture_book", "sci_metal",
			/*Справочная литература:*/
			/*[199]*/"ref_ref", "ref_guide", "ref_encyc", "ref_dict", "reference", "geo_guides", "design",
			/*Военное дело:*/
			/*[206]*/"military_weapon", "military_history", "nonf_military", "military_special", "military_arts", "military",
			/*Компьютеры и Интернет:*/
			/*[212]*/"computers", "comp_programming", "comp_soft", "comp_osnet", "comp_www", "comp_db", "comp_dsp", "comp_hard",
			/*Старинное:*/
			/*[220]*/"antique_myths", "antique_ant", "antique_east", "antique_european", "antique_russian", "antique",
			/*Деловая литература:*/
			/*[226]*/"popular_business", "marketing", "management", "economics", "stock", "accounting", "small_business", "job_hunting", "personal_finance", "org_behavior", "banking", "paper_work", "trade", "industries", "global_economy", "real_estate",
			/*Фольклор:*/
			/*[242]*/"folk_tale", "folklore", "epic", "proverbs", "folk_songs", "limerick", "riddles"/*[248]*/
		};
		private string[] m_sFB2LibRusEcGenreNames = {
			/*Фантастика:*/
			"Научная фантастика", "Фэнтези", "Боевая фантастика", "Юмористическая фантастика", "Альтернативная история", "Социальная фантастика", "Космическая фантастика", "Детективная фантастика", "Героическая фантастика", "Эпическая фантастика", "Городское фэнтези", "Фантастика: прочее", "Сказочная фантастика", "Юмористическое фэнтези", "Ненаучная фантастика", "Историческое фэнтези", "Ироническое фэнтези", "Ироническая фантастика", "Мистика", "Ужасы", "Постапокалипсис", "Киберпанк", "Попаданцы", "Космоопера", "Готический роман", "Стимпанк", "Технофэнтези",
			/*Проза:*/
			"Современная проза", "Классическая проза", "Русская классическая проза", "Историческая проза", "Советская классическая проза", "Сентиментальная проза", "О войне", "Проза", "Контркультура", "Роман", "Повесть", "Рассказ", "Новелла", "Эссе, очерк, этюд, набросок", "Эпистолярная проза", "Афоризмы", "Магический реализм", "Антисоветская литература", "Семейный роман/Семейная сага", "Феерия", "Эпопея",
			/*Наука, Образование:*/
			"История", "Психология", "Технические науки", "Философия", "Политика", "Культурология", "Научная литература: прочее", "Литературоведение", "Медицина", "Языкознание", "Учебники", "Религиоведение", "Юриспруденция", "Биология", "Математика", "Физика", "Педагогика", "Деловая литература", "Астрономия и Космос", "Экономика", "Альтернативная медицина", "Геология и география", "Химия", "Обществознание", "Государство и право", "Зоология", "Ботаника", "Секс и семейная психология", "Иностранные языки", "Экология", "Детская психология", "Психотерапия и консультирование", "Биохимия", "Ветеринария", "Физическая химия", "Органическая химия", "Биофизика", "Аналитическая химия", "Рефераты", "Шпаргалки",
			/*Детективы и Триллеры:*/
			"Детективы: прочее", "Классический детектив", "Боевик", "Полицейский детектив", "Криминальный детектив", "Исторический детектив", "Иронический детектив", "Шпионский детектив", "Крутой детектив", "Политический детектив", "Дамский детективный роман", "Маньяки", "Триллер", "Юридический триллер", "Медицинский триллер", "Техно триллер",
			/*Документальная литература:*/
			"Биографии и Мемуары", "Публицистика", "Научно-популяное", "Документальная литература", "Критика",
			/*Любовные романы:*/
			"Короткие любовные романы", "Современные любовные романы", "Исторические любовные романы", "Любовная фантастика", "Любовные детективы", "О любви", "Эротика", "Порно",
			/*Детское:*/
			"Сказка", "Детская литература: прочее", "Детская проза", "Детская фантастика", "Детские остросюжетные", "Детские стихи", "Детские приключения", "Подростковая литература", "Детский фольклор", "Книга-игра", "Образовательная литература",
			/*Прочее:*/
			"Газеты и журналы", "Музыка", "Кино", "Театр", "Изобразительное искусство, фотография", "Партитуры", "Недописанное", "Фанфик", "Неотсортированное",
			/*Домоводство (Дом и семья):*/
			"Эротика, Секс", "Здоровье", "Кулинария", "Сделай сам", "Спорт", "Домашние животные", "Сад и огород", "Домоводство", "Развлечения", "Хобби и ремесла", "Коллекционирование",
			/*Религия и духовность:*/
			"Религия", "Религиозная литература: прочее", "Самосовершенствование", "Христианство", "Православие", "Католицизм", "Протестантизм", "Ислам", "Иудаизм", "Буддизм", "Индуизм", "Язычество", "Астрология", "Хиромантия", "Эзотерика",
			/*Приключения:*/
			"Приключения: прочее", "Исторические приключения", "Путешествия и география", "Природа и животные", "Морские приключения", "Вестерн", "Приключения про индейцев",
			/*Юмор:*/
			"Юмористическая проза", "Сатира", "Юмористические стихи", "Юмор: прочее", "Анекдоты",
			/*Поэзия и Драматургия:*/
			"Поэзия: прочее", "Лирика", "в стихах", "Эпическая поэзия", "Песенная поэзия", "Басни", "Палиндромы", "Экспериментальная поэзия", "Визуальная поэзия", "Верлибры", "Драматургия: прочее", "Комедия", "Драма", "Киносценарии", "Трагедия", "Сценарии", "Водевиль", "Мистерия",
			/*Техника:*/
			"Радиоэлектроника", "Транспорт и авиация", "Строительство и сопромат", "Автомобили и ПДД", "Архитектура", "Металлургия",
			/*Справочная литература:*/
			"Справочники", "Руководства", "Энциклопедии", "Словари", "Справочная литература", "Путеводители", "Искусство и Дизайн",
			/*Военное дело:*/
			"Военная техника и вооружение", "Военная история", "Военная документалистика", "Спецслужбы", "Боевые искусства", "Военное дело: прочее",
			/*Компьютеры и Интернет:*/
			"Околокомпьютерная литература", "Программирование", "Программы", "ОС и Сети", "Интернет", "Базы данных", "Цифровая обработка сигналов", "Аппаратное обеспечение",
			/*Старинное:*/
			"Мифы. Легенды. Эпос", "Античная литература", "Древневосточная литература", "Древнеевропейская литература", "Древнерусская литература", "Старинная литература: прочее",
			/*Деловая литература:*/
			"О бизнесе популярно", "Маркетинг, PR, реклама", "Управление, подбор персонала", "Экономика", "Ценные бумаги, инвестиции", "Бухучет и аудит", "Малый бизнес", "Поиск работы, карьера", "Личные финансы", "Корпоративная культура", "Банковское дело", "Делопроизводство", "Торговля", "Отраслевые издания", 
			"Внешняя торговля", "Недвижимость",
			/*Фольклор:*/
			"Народные сказки", "Фольклор: прочее", "Былины", "Пословицы, поговорки", "Народные песни", "Частушки, прибаутки, потешки", "Загадки"
		};
		#endregion
		
		public FB2LibrusecGenres( ref IGenresGroup GenresGroup )
		{
			#region Код
			// инициализация словаря
			for( int i=0; i!= m_sFB2LibRusEcGenreCode.Length; ++i ) {
				m_dFB2LibRusEcGenres.Add( m_sFB2LibRusEcGenreCode[i], m_sFB2LibRusEcGenreNames[i] );
			}
			/* инициализация словаря групп жанров */
			for( int i=0; i!= 27; ++i ) {
				m_dFB2LibRusEcGenresGroup.Add( m_sFB2LibRusEcGenreCode[i], GenresGroup.GenresGroupSf );
			}
			for( int i=27; i!= 48; ++i ) {
				m_dFB2LibRusEcGenresGroup.Add( m_sFB2LibRusEcGenreCode[i], GenresGroup.GenresGroupProse );
			}
			for( int i=48; i!= 88; ++i ) {
				m_dFB2LibRusEcGenresGroup.Add( m_sFB2LibRusEcGenreCode[i], GenresGroup.GenresGroupScience );
			}
			for( int i=88; i!= 104; ++i ) {
				m_dFB2LibRusEcGenresGroup.Add( m_sFB2LibRusEcGenreCode[i], GenresGroup.GenresGroupDetective );
			}
			for( int i=104; i!= 109; ++i ) {
				m_dFB2LibRusEcGenresGroup.Add( m_sFB2LibRusEcGenreCode[i], GenresGroup.GenresGroupNonfiction );
			}
			for( int i=109; i!= 117; ++i ) {
				m_dFB2LibRusEcGenresGroup.Add( m_sFB2LibRusEcGenreCode[i], GenresGroup.GenresGroupLove );
			}
			for( int i=117; i!= 128; ++i ) {
				m_dFB2LibRusEcGenresGroup.Add( m_sFB2LibRusEcGenreCode[i], GenresGroup.GenresGroupChildren );
			}
			for( int i=128; i!= 137; ++i ) {
				m_dFB2LibRusEcGenresGroup.Add( m_sFB2LibRusEcGenreCode[i], GenresGroup.GenresGroupOther );
			}
			for( int i=137; i!= 148; ++i ) {
				m_dFB2LibRusEcGenresGroup.Add( m_sFB2LibRusEcGenreCode[i], GenresGroup.GenresGroupHome );
			}
			for( int i=148; i!= 163; ++i ) {
				m_dFB2LibRusEcGenresGroup.Add( m_sFB2LibRusEcGenreCode[i], GenresGroup.GenresGroupReligion );
			}
			for( int i=163; i!= 170; ++i ) {
				m_dFB2LibRusEcGenresGroup.Add( m_sFB2LibRusEcGenreCode[i], GenresGroup.GenresGroupAdventure );
			}
			for( int i=170; i!= 175; ++i ) {
				m_dFB2LibRusEcGenresGroup.Add( m_sFB2LibRusEcGenreCode[i], GenresGroup.GenresGroupHumor );
			}
			for( int i=175; i!= 193; ++i ) {
				m_dFB2LibRusEcGenresGroup.Add( m_sFB2LibRusEcGenreCode[i], GenresGroup.GenresGroupPoetry );
			}
			for( int i=193; i!= 199; ++i ) {
				m_dFB2LibRusEcGenresGroup.Add( m_sFB2LibRusEcGenreCode[i], GenresGroup.GenresGroupTech );
			}
			for( int i=199; i!= 206; ++i ) {
				m_dFB2LibRusEcGenresGroup.Add( m_sFB2LibRusEcGenreCode[i], GenresGroup.GenresGroupReference );
			}
			for( int i=206; i!= 212; ++i ) {
				m_dFB2LibRusEcGenresGroup.Add( m_sFB2LibRusEcGenreCode[i], GenresGroup.GenresGroupMilitary );
			}
			for( int i=212; i!= 220; ++i ) {
				m_dFB2LibRusEcGenresGroup.Add( m_sFB2LibRusEcGenreCode[i], GenresGroup.GenresGroupComputers );
			}
			for( int i=220; i!= 226; ++i ) {
				m_dFB2LibRusEcGenresGroup.Add( m_sFB2LibRusEcGenreCode[i], GenresGroup.GenresGroupAntique );
			}
			for( int i=226; i!= 242; ++i ) {
				m_dFB2LibRusEcGenresGroup.Add( m_sFB2LibRusEcGenreCode[i], GenresGroup.GenresGroupBusiness );
			}
			for( int i=242; i!= m_sFB2LibRusEcGenreCode.Length; ++i ) {
				m_dFB2LibRusEcGenresGroup.Add( m_sFB2LibRusEcGenreCode[i], GenresGroup.GenresGroupFolklore );
			}
			#endregion
		}
		
		#region Открытые методы класса
		public string GetFBGenreName( string sGenreCode ) {
			// возвращает расшифрованное значение Жанра
			if( !m_dFB2LibRusEcGenres.ContainsKey( sGenreCode ) )
				return string.Empty;
			return stringProcessing.OnlyCorrectSymbolsForString( m_dFB2LibRusEcGenres[sGenreCode] );
		}
		
		public string GetFBGenreGroup( string sGenreCode ) {
			// возвращает Группу для указанного Жанра
			if( !m_dFB2LibRusEcGenresGroup.ContainsKey( sGenreCode ) )
				return string.Empty;
			return stringProcessing.OnlyCorrectSymbolsForString( m_dFB2LibRusEcGenresGroup[sGenreCode] );
		}
		
		public string[] GetFBGenreNamesArray() {
			return m_sFB2LibRusEcGenreNames;
		}
		public string[] GetFBGenreCodesArray() {
			return m_sFB2LibRusEcGenreCode;
		}
		
		public List<string> GetFBGenresForGroup( string Group ) {
			if( m_dFB2LibRusEcGenresGroup.ContainsValue( Group ) ) {
				List<string> lsGenresForGroup = new List<string>();
				foreach( string g in m_dFB2LibRusEcGenresGroup.Keys ) {
					// m_dFB2LibRusEcGenresGroup.Values - Группы; m_dFB2LibRusEcGenresGroup.Keys - Жанры
					if( m_dFB2LibRusEcGenresGroup[g] == Group )
						lsGenresForGroup.Add( g );
				}
				return lsGenresForGroup;
			}
			return null;
		}
		#endregion
	}
}
