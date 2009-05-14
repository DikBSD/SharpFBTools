/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 07.05.2009
 * Time: 8:40
 * 
 * License: GPL 2.1
 */
using System;
using System.Collections.Generic;

namespace FB2.Genres
{
	/// <summary>
	/// Description of FB22Genres.
	/// </summary>
	public class FB22Genres : IFBGenres
	{
		#region Закрытые данные класса
		private Dictionary<string, string> m_dFB22Genres = new Dictionary<string, string>();
		private Dictionary<string, string> m_dFB22GenresGroup = new Dictionary<string, string>();
		
		private string[] m_sFB22GenreCode = {
			/*0*/"sf_history","sf_action","sf_epic","sf_heroic","sf_detective","sf_cyberpunk","sf_space","sf_social","sf_horror","sf_humor",
			"sf_fantasy","sf","love_sf",
			
			/*13*/"det_classic","det_police","det_action","det_irony","det_history","det_espionage","det_crime","det_political","det_maniac",
			"det_hard","thriller","detective",
			
			/*25*/"prose_classic","prose_history","prose_contemporary","prose_counter","prose_rus_classic","prose_su_classics","prose_military",
			
			/*32*/"love_contemporary","love_history","love_detective","love_short","love_erotica",
			
			/*37*/"adv_western","adv_history","adv_indian","adv_maritime","adv_geo","adv_animal","adventure",
			
			/*44*/"child_tale","child_verse","child_prose","child_sf","child_det","child_adv","child_education","children",
			
			/*52*/"poetry","dramaturgy",
			
			/*54*/"antique_ant","antique_european","antique_russian","antique_east","antique_myths","antique",
			
			/*60*/"sci_history","sci_psychology","sci_culture","sci_philosophy","sci_politics","sci_juris","sci_linguistic",
			"sci_medicine","sci_phys","sci_math","sci_chem","sci_biology","sci_tech","science",
			
			/*74*/"comp_www","comp_programming","comp_hard","comp_soft","comp_db","comp_osnet","computers",
			
			/*81*/"ref_encyc","ref_dict","ref_ref","ref_guide","reference",
			
			/*86*/"nonf_biography","nonf_publicism","nonf_criticism","design","nonfiction",
			
			/*91*/"sci_religion","religion_rel","religion_esoterics","religion_self","religion",
			
			/*96*/"humor_anecdote","humor_prose","humor_verse","humor",
			
			/*100*/"home_cooking","home_pets","home_crafts","home_entertain","home_health","home_garden","home_diy","home_sport","home_sex","home",
			
			/*110*/"job_hunting","management","marketing","banking","stock","accounting","global_economy","economics","industries","org_behavior",
			"personal_finance","real_estate","popular_business","small_business","paper_work","economics_ref"/*125*/
		};
		private string[] m_sFB22GenreNames = {
			"Альтернативная история","Боевая Фантастика","Эпическая Фантастика","Героическая фантастика","Детективная Фантастика","Киберпанк",
			"Космическая Фантастика","Социальная фантастика","Ужасы и Мистика","Юмористическая фантастика","Фэнтези","Научная Фантастика",
			"Любовная фантастика",
			
			"Классический Детектив","Полицейский Детектив","Боевики","Иронический Детектив","Исторический Детектив","Шпионский Детектив",
			"Криминальный Детектив","Политический детектив","Маньяки","Крутой Детектив","Триллеры","Детектив",
			
			"Классическая Проза","Историческая Проза","Современная Проза","Контркультура","Русская Классика","Советская Классика","Военная проза",
			
			"Современные Любовные Романы","Исторические Любовные Романы","Остросюжетные Любовные Романы","Короткие Любовные Романы","Эротика",
			
			"Вестерны","Исторические Приключения","Приключения - Индейцы","Морские Приключения", "Путешествия и География","Природа и Животные",
			"Приключения - Прочее",
			
			"Сказки","Детские Стихи","Детская Проза","Детская Фантастика","Детские Остросюжетные","Детские Приключения",
			"Детская Образовательная литература","Детское - Прочее",
				
			"Поэзия","Драматургия",
				
			"Античная Литература","Европейская Старинная Литература","Древнерусская Литература","Древневосточная Литература","Мифы. Легенды. Эпос",
			"Старинная Литература - Прочее",
				
			"История","Психология","Культурология","Философия","Политика","Юриспруденция","Языкознание","Медицина",
			"Физика","Математика","Химия","Биология","Технические","Научно-образовательная - Прочее",
				
			"Интернет","Программирование","Компьютерное Железо","Программы","Базы данных","ОС и Сети","Компьютеры - Прочее",
			
			"Энциклопедии","Словари","Справочники","Руководства","Справочная Литература - Прочее",
				
			"Биографии и Мемуары","Публицистика","Критика","Искусство, Дизайн","Документальное - Прочее",
				
			"Религиоведение","Религия","Эзотерика","Самосовершенствование","Религия и духовность - Прочее",
			
			"Анекдоты","Юмористическая Проза","Юмористические стихи","Юмор - Прочее",
			
			"Кулинария","Домашние Животные","Хобби, Ремесла","Развлечения","Здоровье","Сад и Огород","Сделай Сам","Спорт","Эротика, Секс",
			"Дом и Семья - Прочее",
			
			"Поиск работы, карьера","Управление, подбор персонала","Маркетинг, PR, реклама","Банковское дело","Ценные бумаги, инвестиции",
			"Бухучет, налогообложение, аудит","Внешнеэкономическая деятельность","Экономика","Отраслевые издания","Корпоративная культура",
			"Личные финансы","Недвижимость","О бизнесе популярно","Малый бизнес","Делопроизводство","Справочники"
		};
		#endregion
		
		public FB22Genres()
		{
			#region Код
			// инициализация словаря
			for( int i=0; i!= m_sFB22GenreCode.Length; ++i ) {
				m_dFB22Genres.Add(m_sFB22GenreCode[i], m_sFB22GenreNames[i] );
			}
			/* инициализация словаря групп жанров */
			for( int i=0; i!= 13; ++i ) {
				m_dFB22GenresGroup.Add( m_sFB22GenreCode[i], "Фантастика, Фэнтэзи" );
			}
			for( int i=13; i!= 25; ++i ) {
				m_dFB22GenresGroup.Add( m_sFB22GenreCode[i], "Детективы, Боевики" );
			}
			for( int i=25; i!= 32; ++i ) {
				m_dFB22GenresGroup.Add( m_sFB22GenreCode[i], "Проза" );
			}
			for( int i=32; i!= 37; ++i ) {
				m_dFB22GenresGroup.Add( m_sFB22GenreCode[i], "Любовные романы" );
			}
			for( int i=37; i!= 44; ++i ) {
				m_dFB22GenresGroup.Add( m_sFB22GenreCode[i], "Приключения" );
			}
			for( int i=44; i!= 52; ++i ) {
				m_dFB22GenresGroup.Add( m_sFB22GenreCode[i], "Детское" );
			}
			for( int i=52; i!= 54; ++i ) {
				m_dFB22GenresGroup.Add( m_sFB22GenreCode[i], "Поэзия, Драматургия" );
			}
			for( int i=54; i!= 60; ++i ) {
				m_dFB22GenresGroup.Add( m_sFB22GenreCode[i], "Старинное" );
			}
			for( int i=60; i!= 74; ++i ) {
				m_dFB22GenresGroup.Add( m_sFB22GenreCode[i], "Наука, Образование" );
			}
			for( int i=74; i!= 81; ++i ) {
				m_dFB22GenresGroup.Add( m_sFB22GenreCode[i], "Компьютеры" );
			}
			for( int i=81; i!= 86; ++i ) {
				m_dFB22GenresGroup.Add( m_sFB22GenreCode[i], "Справочники" );
			}
			for( int i=86; i!= 91; ++i ) {
				m_dFB22GenresGroup.Add( m_sFB22GenreCode[i], "Документальное" );
			}
			for( int i=91; i!= 96; ++i ) {
				m_dFB22GenresGroup.Add( m_sFB22GenreCode[i], "Религия" );
			}
			for( int i=96; i!= 100; ++i ) {
				m_dFB22GenresGroup.Add( m_sFB22GenreCode[i], "Юмор" );
			}
			for( int i=100; i!= 110; ++i ) {
				m_dFB22GenresGroup.Add( m_sFB22GenreCode[i], "Дом, Семья" );
			}
			for( int i=110; i!= m_sFB22GenreCode.Length; ++i ) {
				m_dFB22GenresGroup.Add( m_sFB22GenreCode[i], "Бизнес" );
			}
			#endregion
		}
		
		#region Открытые методы класса
		public string GetFBGenreName( string sGenreCode ) {
			// возвращает расшифрованное значение Жанра
			if( !m_dFB22Genres.ContainsKey( sGenreCode ) ) return "";
			return StringProcessing.StringProcessing.OnlyCorrectSymbolsForString( m_dFB22Genres[sGenreCode] );
		}
		
		public string GetFBGenreGroup( string sGenreCode ) {
			// возвращает Группу для указанного Жанра
			if( !m_dFB22GenresGroup.ContainsKey( sGenreCode ) ) return "";
			return StringProcessing.StringProcessing.OnlyCorrectSymbolsForString( m_dFB22GenresGroup[sGenreCode] );
		}
		#endregion
	}
}
