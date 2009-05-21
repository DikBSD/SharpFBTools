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
	/// Description of FB21Genres.
	/// </summary>
	public class FB21Genres : IFBGenres
	{
		#region Закрытые данные класса
		private Dictionary<string, string> m_dFB21Genres = new Dictionary<string, string>();
		private Dictionary<string, string> m_dFB21GenresGroup = new Dictionary<string, string>();
		
		private string[] m_sFB21GenreCode = {
			/*0*/"sf_history","sf_action","sf_epic","sf_heroic","sf_detective","sf_cyberpunk","sf_space","sf_social","sf_horror","sf_humor",
			"sf_fantasy","sf",/*11*/
			
			/*12*/"det_classic","det_police","det_action","det_irony","det_history","det_espionage","det_crime","det_political","det_maniac",
			"det_hard","thriller","detective",/*23*/
			
			/*24*/"prose_classic","prose_history","prose_contemporary","prose_counter","prose_rus_classic","prose_su_classics",/*29*/
			
			/*30*/"love_contemporary","love_history","love_detective","love_short","love_erotica",/*34*/
			
			/*35*/"adv_western","adv_history","adv_indian","adv_maritime","adv_geo","adv_animal","adventure",/*41*/
			
			/*42*/"child_tale","child_verse","child_prose","child_sf","child_det","child_adv","child_education","children",/*49*/
			
			/*50*/"poetry","dramaturgy",/*51*/
			
			/*52*/"antique_ant","antique_european","antique_russian","antique_east","antique_myths","antique",/*57*/
			
			/*58*/"sci_history","sci_psychology","sci_culture","sci_philosophy","sci_politics","sci_business","sci_juris",
			"sci_linguistic","sci_medicine","sci_phys","sci_math","sci_chem","sci_biology","sci_tech","science",/*72*/
			
			/*73*/"comp_www","comp_programming","comp_hard","comp_soft","comp_db","comp_osnet","computers",/*79*/
			
			/*80*/"ref_encyc","ref_dict","ref_ref","ref_guide","reference",/*84*/

			/*85*/"nonf_biography","nonf_publicism","nonf_criticism","design","nonfiction",/*89*/
			
			/*90*/"sci_religion","religion_rel","religion_esoterics","religion_self","religion",/*94*/
			
			/*95*/"humor_anecdote","humor_prose","humor_verse","humor",/*98*/
			
			/*99*/"home_cooking","home_pets","home_crafts","home_entertain","home_health","home_garden","home_diy","home_sport","home_sex","home"/*109*/
		};
		private string[] m_sFB21GenreNames = {
			"Альтернативная история","Боевая Фантастика","Эпическая Фантастика","Героическая фантастика","Детективная Фантастика","Киберпанк",
			"Космическая Фантастика","Социальная фантастика","Ужасы и Мистика","Юмористическая фантастика","Фэнтези","Научная Фантастика",
				
			"Классический Детектив","Полицейский Детектив","Боевики","Иронический Детектив","Исторический Детектив","Шпионский Детектив",
			"Криминальный Детектив","Политический детектив","Маньяки","Крутой Детектив","Триллеры","Детектив",
				
			"Классическая Проза","Историческая Проза","Современная Проза","Контркультура","Русская Классика","Советская Классика",
				
			"Современные Любовные Романы","Исторические Любовные Романы","Остросюжетные Любовные Романы","Короткие Любовные Романы","Эротика",
				
			"Вестерны","Исторические Приключения","Приключения - Индейцы","Морские Приключения", "Путешествия и География","Природа и Животные",
			"Приключения - Прочее",
			
			"Сказки","Детские Стихи","Детская Проза","Детская Фантастика","Детские Остросюжетные","Детские Приключения",
			"Детская Образовательная литература","Детское - Прочее",
				
			"Поэзия","Драматургия",
				
			"Античная Литература","Европейская Старинная Литература","Древнерусская Литература","Древневосточная Литература","Мифы. Легенды. Эпос",
			"Старинная Литература - Прочее",
				
			"История","Психология","Культурология","Философия","Политика","Деловая литература","Юриспруденция",
			"Языкознание","Медицина","Физика","Математика","Химия","Биология","Технические","Научно-образовательная - Прочее",
				
			"Интернет","Программирование","Компьютерное Железо","Программы","Базы данных","ОС и Сети","Компьютеры - Прочее",
			
			"Энциклопедии","Словари","Справочники","Руководства","Справочная Литература - Прочее",
				
			"Биографии и Мемуары","Публицистика","Критика","Искусство, Дизайн","Документальное - Прочее",
				
			"Религиоведение","Религия","Эзотерика","Самосовершенствование","Религия и духовность - Прочее",
			
			"Анекдоты","Юмористическая Проза","Юмористические стихи","Юмор - Прочее",
			
			"Кулинария","Домашние Животные","Хобби, Ремесла","Развлечения","Здоровье","Сад и Огород","Сделай Сам","Спорт","Эротика, Секс",
			"Дом и Семья - Прочее"
		};
		#endregion
		
		public FB21Genres()
		{
			#region Код
			// инициализация словаря
			for( int i=0; i!= m_sFB21GenreCode.Length; ++i ) {
				m_dFB21Genres.Add( m_sFB21GenreCode[i], m_sFB21GenreNames[i] );
			}
			/* инициализация словаря групп жанров */
			Settings.DataFM dfm = new Settings.DataFM();
			for( int i=0; i!= 12; ++i ) {
				m_dFB21GenresGroup.Add( m_sFB21GenreCode[i], dfm.GenresGroupSf );
			}
			for( int i=12; i!= 24; ++i ) {
				m_dFB21GenresGroup.Add( m_sFB21GenreCode[i], dfm.GenresGroupDetective );
			}
			for( int i=24; i!= 30; ++i ) {
				m_dFB21GenresGroup.Add( m_sFB21GenreCode[i], dfm.GenresGroupProse );
			}
			for( int i=30; i!= 35; ++i ) {
				m_dFB21GenresGroup.Add( m_sFB21GenreCode[i], dfm.GenresGroupLove );
			}
			for( int i=35; i!= 42; ++i ) {
				m_dFB21GenresGroup.Add( m_sFB21GenreCode[i], dfm.GenresGroupAdventure );
			}
			for( int i=42; i!= 50; ++i ) {
				m_dFB21GenresGroup.Add( m_sFB21GenreCode[i], dfm.GenresGroupChildren );
			}
			for( int i=50; i!= 52; ++i ) {
				m_dFB21GenresGroup.Add( m_sFB21GenreCode[i], dfm.GenresGroupPoetry );
			}
			for( int i=52; i!= 58; ++i ) {
				m_dFB21GenresGroup.Add( m_sFB21GenreCode[i], dfm.GenresGroupAntique );
			}
			for( int i=58; i!= 73; ++i ) {
				m_dFB21GenresGroup.Add( m_sFB21GenreCode[i], dfm.GenresGroupScience );
			}
			for( int i=73; i!= 80; ++i ) {
				m_dFB21GenresGroup.Add( m_sFB21GenreCode[i], dfm.GenresGroupComputers );
			}
			for( int i=80; i!= 85; ++i ) {
				m_dFB21GenresGroup.Add( m_sFB21GenreCode[i], dfm.GenresGroupReference );
			}
			for( int i=85; i!= 90; ++i ) {
				m_dFB21GenresGroup.Add( m_sFB21GenreCode[i], dfm.GenresGroupNonfiction );
			}
			for( int i=90; i!= 95; ++i ) {
				m_dFB21GenresGroup.Add( m_sFB21GenreCode[i], dfm.GenresGroupReligion );
			}
			for( int i=95; i!= 99; ++i ) {
				m_dFB21GenresGroup.Add( m_sFB21GenreCode[i], dfm.GenresGroupHumor );
			}
			for( int i=99; i!= m_sFB21GenreCode.Length; ++i ) {
				m_dFB21GenresGroup.Add( m_sFB21GenreCode[i], dfm.GenresGroupHome );
			}
			#endregion
		}
		
		#region Открытые методы класса
		public string GetFBGenreName( string sGenreCode ) {
			// возвращает расшифрованное значение Жанра
			if( !m_dFB21Genres.ContainsKey( sGenreCode ) ) return "";
			return StringProcessing.StringProcessing.OnlyCorrectSymbolsForString( m_dFB21Genres[sGenreCode] );
		}
		
		public string GetFBGenreGroup( string sGenreCode ) {
			// возвращает Группу для указанного Жанра
			if( !m_dFB21GenresGroup.ContainsKey( sGenreCode ) ) return "";
			return StringProcessing.StringProcessing.OnlyCorrectSymbolsForString( m_dFB21GenresGroup[sGenreCode] );
		}
		#endregion
	}
}
