/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 18.11.2015
 * Время: 12:40
 * 
 * License: GPL 2.1
 */
using System;
using System.Text.RegularExpressions;

namespace Core.AutoCorrector
{
	/// <summary>
	/// Обработка Жанров
	/// </summary>
	public class GenreCorrector
	{
		private string _xmlText = string.Empty;
		
		private bool _preProcess = false;
		private bool _postProcess = false;
		
		/// <summary>
		/// Конструктор класса GenreCorrector
		/// </summary>
		/// <param name="xmlText">Строка для корректировки</param>
		/// <param name="preProcess">Удаление стартовых пробелов и перевода строки => всю книгу - в одну строку</param>
		/// <param name="postProcess">Вставка разрыва абзаца между смежными тегами</param>
		public GenreCorrector( ref string xmlText, bool preProcess, bool postProcess )
		{
			_xmlText = xmlText;
			_preProcess = preProcess;
			_postProcess = postProcess;
		}
		
		/// <summary>
		/// Корректировка тегов genre
		/// </summary>
		/// <returns>Откорректированную строку типа string </returns>
		public string correct() {
			if ( _xmlText.IndexOf( "<genre" ) == -1 )
				return _xmlText;
			
			// преобработка (удаление стартовых пробелов ДО тегов и удаление завершающих пробелов ПОСЛЕ тегов и символов переноса строк)
			if ( _preProcess )
				_xmlText = FB2CleanCode.preProcessing( _xmlText );
			
			/*******************
			 * Обработка genre *
			 *******************/
			/* Фантастика */
			// обработка жанра romance_fantasy, romance_sf, magician_book, foreign_fantasy, dragon_fantasy, fantasy, Фентези, Фэнтези
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(romance_fantasy|romance_sf|magician_book|foreign_fantasy|dragon_fantasy|fantasy|Фентези|Фэнтези)\\s*?(?=</genre>)",
				"${genre}sf_fantasy", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра horror, vampire_book, horror_usa
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(horror|vampire_book|horror_usa)\\s*?(?=</genre>)",
				"${genre}sf_horror", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра horror_fantasy, horror_vampires, horror_occult, Мистика
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(horror_fantasy|horror_vampires|horror_occult|Мистика)\\s*?(?=</genre>)",
				"${genre}sf_mystic", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра sf_history_avant, fantasy_alt_hist
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(sf_history_avant|fantasy_alt_hist)\\s*?(?=</genre>)",
				"${genre}historical_fantasy", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра альтернативная история
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?альтернативная история\\s*?(?=</genre>)",
				"${genre}sf_history", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра sf_cyber_punk
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?sf_cyber_punk\\s*?(?=</genre>)",
				"${genre}sf_cyberpunk", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра city_fantasy
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?city_fantasy\\s*?(?=</genre>)",
				"${genre}sf_fantasy_city", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра fantasy_fight, Боевая фантастика
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(fantasy_fight|Боевая фантастика)\\s*?(?=</genre>)",
				"${genre}sf_action", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра foreign_sf
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?foreign_sf\\s*?(?=</genre>)",
				"${genre}sf_etc", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра love_fantasy
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?love_fantasy\\s*?(?=</genre>)",
				"${genre}love_sf", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра litrpg, sf_litRPG, LitRPG
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(litrpg|sf_litRPG|LitRPG)\\s*?(?=</genre>)",
				"${genre}sf_litrpg", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра SF, Научная Фантастика
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(SF|Научная Фантастика)\\s*?(?=</genre>)",
				"${genre}sf", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра Юмористическая фантастика
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Юмористическая фантастика\\s*?(?=</genre>)",
				"${genre}sf_humor", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра Фэнтези Юмор
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Фэнтези Юмор\\s*?(?=</genre>)",
				"${genre}humor_fantasy", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			
			/* Юмор */
			// обработка жанра entert_humor, foreign_humor
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(entert_humor|foreign_humor)\\s*?(?=</genre>)",
				"${genre}humor", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра юмористическая проза
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?юмористическая проза\\s*?(?=</genre>)",
				"${genre}humor_prose", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра Комедия
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Комедия\\s*?(?=</genre>)",
				"${genre}comedy", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра Юмор
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Юмор\\s*?(?=</genre>)",
				"${genre}humor", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			
			/* Детективы и триллеры */
			// обработка жанра thriller_police
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?thriller_police\\s*?(?=</genre>)",
				"${genre}det_police", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра thriller_mystery
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?thriller_mystery\\s*?(?=</genre>)",
				"${genre}thriller</genre><genre>detective", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра foreign_detective
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?foreign_detective\\s*?(?=</genre>)",
				"${genre}detective", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра foreign_action
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?foreign_action\\s*?(?=</genre>)",
				"${genre}det_action", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра Шпионский Детектив
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Шпионский Детектив\\s*?(?=</genre>)",
				"${genre}det_espionage", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра исторический детектив
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?исторический детектив\\s*?(?=</genre>)",
				"${genre}det_history", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра Иронический детектив
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Иронический детектив\\s*?(?=</genre>)",
				"${genre}det_irony", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			
			/* Приключения */
			// обработка жанра literature_western
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature_western\\s*?(?=</genre>)",
				"${genre}adv_western</genre><genre>detective", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра romance_historical, adv_history_avant
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(romance_historical|adv_history_avant)\\s*?(?=</genre>)",
				"${genre}adv_history", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра literature_sea
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature_sea\\s*?(?=</genre>)",
				"${genre}adv_maritime", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра literature_adv
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature_adv\\s*?(?=</genre>)",
				"${genre}adv_story", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра foreign_adventure
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?foreign_adventure\\s*?(?=</genre>)",
				"${genre}adv_modern", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра Приключения
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Приключения\\s*?(?=</genre>)",
				"${genre}adventure", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			
			/* Детское и подростковое */
			// обработка жанра child_4, literature_fairy, Сказка, Сказки
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(child_4|literature_fairy|Сказка|Сказки)\\s*?(?=</genre>)",
				"${genre}child_tale", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра child_9, child_characters, Детская литература
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(child_9|child_characters|Детская литература)\\s*?(?=</genre>)",
				"${genre}children", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра child_animals, outdoors_fauna, outdoors_hunt_fish, child_nature
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(child_animals|outdoors_fauna|outdoors_hunt_fish|child_nature)\\s*?(?=</genre>)",
				"${genre}adv_animal", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра teens_history, teens_literature, prose_teen
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(teens_history|teens_literature|prose_teen)\\s*?(?=</genre>)",
				"${genre}ya", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра teens_sf
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?teens_sf\\s*?(?=</genre>)",
				"${genre}child_sf", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра child_history
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?child_history\\s*?(?=</genre>)",
				"${genre}child_adv", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра Детская Проза
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Детская Проза\\s*?(?=</genre>)",
				"${genre}child_prose", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			
			/* Проза, драма */
			// обработка жанра prose_su_classic
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?prose_su_classic\\s*?(?=</genre>)",
				"${genre}prose_su_classics", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра prose_rus_classics
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?prose_rus_classics\\s*?(?=</genre>)",
				"${genre}prose_rus_classic", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра proce, literature, prose_root, literature_books, Проза, literature_antology
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(proce|literature|prose_root|literature_books|Проза|literature_antology)\\s*?(?=</genre>)",
				"${genre}prose", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра literature_classics, Классическая Проза
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(literature_classics|Классическая Проза)\\s*?(?=</genre>)",
				"${genre}prose_classic", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра literature19
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature19\\s*?(?=</genre>)",
				"${genre}literature_19", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра literature_su_classics
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature_su_classics\\s*?(?=</genre>)",
				"${genre}prose_su_classics", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра literature_rus_classsic
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature_rus_classsic\\s*?(?=</genre>)",
				"${genre}prose_rus_classic", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра Контркультура
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Контркультура\\s*?(?=</genre>)",
				"${genre}prose_counter", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра romance_contemporary, russian_contemporary, Современная Проза
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(romance_contemporary|russian_contemporary|Современная Проза)\\s*?(?=</genre>)",
				"${genre}prose_contemporary", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра Историческая проза
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Историческая проза\\s*?(?=</genre>)",
				"${genre}prose_history", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра literature_drama, foreign_dramaturgy
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(literature_drama|foreign_dramaturgy)\\s*?(?=</genre>)",
				"${genre}dramaturgy", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра literature_short
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature_short\\s*?(?=</genre>)",
				"${genre}short_story", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра narrative
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?narrative\\s*?(?=</genre>)",
				"${genre}great_story", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра foreign_novel
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?foreign_novel\\s*?(?=</genre>)",
				"${genre}story", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра romance, Романы, Роман
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(romance|Романы|Роман)\\s*?(?=</genre>)",
				"${genre}roman", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра literature_world, foreign_contemporary
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(literature_world|foreign_contemporary)\\s*?(?=</genre>)",
				"${genre}foreign_prose", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра sketch
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?sketch\\s*?(?=</genre>)",
				"${genre}essay", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра aphorism_quote
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?aphorism_quote\\s*?(?=</genre>)",
				"${genre}aphorisms", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра essays
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?essays\\s*?(?=</genre>)",
				"${genre}essay", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			
			/* Прочее */
			// обработка жанра literature_essay, beginning_authors
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(literature_essay|beginning_authors)\\s*?(?=</genre>)",
				"${genre}network_literature", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра romance_multicultural
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?romance_multicultural\\s*?(?=</genre>)",
				"${genre}art_world_culture", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра Журналы, newspapers
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(Журналы|newspapers)\\s*?(?=</genre>)",
				"${genre}periodic", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			
			/* Научные и образовательные */
			// обработка жанра science_medicine, medicine
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(science_medicine|medicine)\\s*?(?=</genre>)",
				"${genre}sci_medicine", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра history_russia, history_asia, history_middle_east, history_usa, history_europe, literature_history, history_ancient, history_world, history_australia, history_africa, История
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(history_russia|history_asia|history_middle_east|history_usa|history_europe|literature_history|history_ancient|history_world|history_australia|history_africa|История)\\s*?(?=</genre>)",
				"${genre}sci_history", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра science_biolog
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?science_biolog\\s*?(?=</genre>)",
				"${genre}sci_biology", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра science_history_philosophy
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?science_history_philosophy\\s*?(?=</genre>)",
				"${genre}sci_philosophy", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра science_earth, geography_book
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(science_earth|geography_book)\\s*?(?=</genre>)",
				"${genre}sci_geo", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра foreign_edu
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?foreign_edu\\s*?(?=</genre>)",
				"${genre}sci_popular", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра psy_social, sociology_book, nonfiction_sociology, nonfiction_social_sci
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(psy_social|sociology_book|nonfiction_sociology|nonfiction_social_sci|nonfiction_social_work)\\s*?(?=</genre>)",
				"${genre}sci_social_studies", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра pedagogy_book
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?pedagogy_book\\s*?(?=</genre>)",
				"${genre}sci_pedagogy", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра health_psy, science_psy, upbringing_book, foreign_psychology, psy_generic, psy_alassic, Психология
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(health_psy|science_psy|upbringing_book|foreign_psychology|psy_generic|psy_alassic|Психология)\\s*?(?=</genre>)",
				"${genre}sci_psychology", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра языкознание
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?языкознание\\s*?(?=</genre>)",
				"${genre}sci_linguistic", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра Религиоведение, religion_earth
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(Религиоведение|religion_earth)\\s*?(?=</genre>)",
				"${genre}sci_religion", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра Математика
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Математика\\s*?(?=</genre>)",
				"${genre}sci_math", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра unrecognised
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?unrecognised\\s*?(?=</genre>)",
				"${genre}sci_theories", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра health_alt_medicine, Альтернативная медицина
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(health_alt_medicine|Альтернативная медицина)\\s*?(?=</genre>)",
				"${genre}sci_medicine_alternative", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			
			/* Техника */
			// обработка жанра science_measurement
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?science_measurement\\s*?(?=</genre>)",
				"${genre}sci_tech", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			
			/* Поэзия */
			// обработка жанра literature_poetry
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature_poetry\\s*?(?=</genre>)",
				"${genre}poetry", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра foreign_poetry
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?foreign_poetry\\s*?(?=</genre>)",
				"${genre}poetry_for_classical", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра Existential poetry
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Existential poetry\\s*?(?=</genre>)",
				"${genre}experimental_poetry", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			
			/* Фольклер */
			// обработка жанра nonfiction_folklor
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?nonfiction_folklor\\s*?(?=</genre>)",
				"${genre}folklore", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			
			/* Религия */
			// обработка жанра religion_buddhism, rel_boddizm
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(religion_buddhism|rel_boddizm)\\s*?(?=</genre>)",
				"${genre}religion_budda", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра chris_pravoslavie, chris_orthodoxy, Православие
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(chris_pravoslavie|chris_orthodoxy|Православие)\\s*?(?=</genre>)",
				"${genre}religion_orthodoxy", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра literature_religion, foreign_religion, Религия
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(literature_religion|foreign_religion|Религия)\\s*?(?=</genre>)",
				"${genre}religion_rel", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра religion_spirituality, Religion, religion_bibles
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(religion_spirituality|Religion|religion_bibles)\\s*?(?=</genre>)",
				"${genre}religion", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра nonfiction_philosophy
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?nonfiction_philosophy\\s*?(?=</genre>)",
				"${genre}sci_philosophy", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра chris_fiction, Христианство
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(chris_fiction|Христианство)\\s*?(?=</genre>)",
				"${genre}religion_christianity", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра Эзотерика
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Эзотерика\\s*?(?=</genre>)",
				"${genre}religion_esoterics", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра Самосовершенствование
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Самосовершенствование\\s*?(?=</genre>)",
				"${genre}religion_self", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			
			/* Военное */
			// обработка жанра literature_war
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature_war\\s*?(?=</genre>)",
				"${genre}prose_military", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра histor_military, history_military_science
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(histor_military|history_military_science)\\s*?(?=</genre>)",
				"${genre}military_history", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			
			/* Путеществие и туризм */
			// обработка жанра travel_guidebook_series
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?travel_guidebook_series\\s*?(?=</genre>)",
				"${genre}ref_guide", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра outdoors_nature_writing, outdoors_conservation, outdoors_travel
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(outdoors_nature_writing|outdoors_conservation|outdoors_travel)\\s*?(?=</genre>)",
				"${genre}travel_notes", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра travel, travel_asia, travel_europe, travel_africa, travel_lat_am, travel_spec, travel_polar
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(travel|travel_asia|travel_europe|travel_africa|travel_lat_am|travel_spec|travel_polar)\\s*?(?=</genre>)",
				"${genre}adv_geo", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			
			/* Публицистика и документалистика */
			// обработка жанра biogr_historical, biogr_arts, biography, biogr_leaders, biogr_professionals, biogr_sports, biz_beogr, biogr_travel, Биографии и мемуары
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(biogr_historical|biography|biogr_leaders|biogr_arts|biogr_professionals|biogr_sports|biz_beogr|biogr_travel|Биографии и мемуары)\\s*?(?=</genre>)",
				"${genre}nonf_biography", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра foreign_publicism, Публицистика
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(foreign_publicism|Публицистика)\\s*?(?=</genre>)",
				"${genre}nonf_publicism", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра foreign_desc, nonfiction_spec_group, people, nonfiction_crime, nonfiction_edu
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(foreign_desc|nonfiction_spec_group|people|nonfiction_crime|nonfiction_edu)\\s*?(?=</genre>)",
				"${genre}nonfiction", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра literature_critic
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature_critic\\s*?(?=</genre>)",
				"${genre}nonf_criticism", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			
			/* Политика, экономика юриспруденция, деловая литература */
			// обработка жанра nonfiction_politics, literature_political
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(nonfiction_politics|literature_political)\\s*?(?=</genre>)",
				"${genre}sci_politics", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра biz_economics
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?biz_economics\\s*?(?=</genre>)",
				"${genre}sci_economy", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра nonfiction_law, professional_law
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(nonfiction_law|professional_law)\\s*?(?=</genre>)",
				"${genre}sci_juris", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра foreign_business
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?foreign_business\\s*?(?=</genre>)",
				"${genre}economics_ref", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра psy_personal, biz_management
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(psy_personal|biz_management)\\s*?(?=</genre>)",
				"${genre}management", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра business
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?business\\s*?(?=</genre>)",
				"${genre}popular_business", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра Недвижимость
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Недвижимость\\s*?(?=</genre>)",
				"${genre}real_estate", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			
			/* Семья и здоровье */
			// обработка жанра sport
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?sport\\s*?(?=</genre>)",
				"${genre}home_sport", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра health, health_men, health_women, teens_health, health_self_help, health_rel
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(health|health_men|health_women|teens_health|health_self_help|health_rel)\\s*?(?=</genre>)",
				"${genre}home_health", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра health_sex
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?health_sex\\s*?(?=</genre>)",
				"${genre}home_sex", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра family_relations, family_parenting, foreign_home
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(family_relations|family_parenting|foreign_home)\\s*?(?=</genre>)",
				"${genre}family", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра family_pregnancy, family_health
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(family_pregnancy|family_health)\\s*?(?=</genre>)",
				"${genre}psy_sex_and_family", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра health_nutrition, cooking, Кулинария
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(health_nutrition|cooking|Кулинария)\\s*?(?=</genre>)",
				"${genre}home_cooking", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра entertainment
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?entertainment\\s*?(?=</genre>)",
				"${genre}home_entertain", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);

			/* Любовные книги */
			// обработка жанра literature_erotica, Эротика
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(literature_erotica|Эротика)\\s*?(?=</genre>)",
				"${genre}love_erotica", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра women_single, literature_women, foreign_love
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(women_single|literature_women|foreign_love)\\s*?(?=</genre>)",
				"${genre}love", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра slash
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?slash\\s*?(?=</genre>)",
				"${genre}love_hard", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра Современные Любовные Романы
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Современные Любовные Романы\\s*?(?=</genre>)",
				"${genre}love_contemporary", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра Исторические любовные романы
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Исторические любовные романы\\s*?(?=</genre>)",
				"${genre}love_history", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			
			/* Искусство, Искусствоведение, Дизайн */
			// обработка жанра music_dancing
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?music_dancing\\s*?(?=</genre>)",
				"${genre}music", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра cinema_theatre
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?cinema_theatre\\s*?(?=</genre>)",
				"${genre}cine", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			
			/* Компьютеры и Интернет */
			// обработка жанра foreign_comp
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?foreign_comp\\s*?(?=</genre>)",
				"${genre}computers", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра Интернет
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Интернет\\s*?(?=</genre>)",
				"${genre}comp_www", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			
			/* Справочная литература */
			// обработка жанра ref_books
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?ref_books\\s*?(?=</genre>)",
				"${genre}ref_ref", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра hand-book
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?hand-book\\s*?(?=</genre>)",
				"${genre}ref_guide", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра ref_encyclopedia
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?ref_encyclopedia\\s*?(?=</genre>)",
				"${genre}ref_encyc", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка жанра geo_guide, Путеводители
			_xmlText = Regex.Replace(
				_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(geo_guide|Путеводители)\\s*?(?=</genre>)",
				"${genre}geo_guides", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);

			// постобработка (разбиение на теги (смежные теги) )
			if ( _postProcess )
				_xmlText = FB2CleanCode.postProcessing( _xmlText );
			
			return _xmlText;
		}
	}
}
