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
using System.Windows.Forms;

using Core.Common;

namespace Core.AutoCorrector
{
	/// <summary>
	/// Обработка Жанров
	/// </summary>
	public class GenreCorrector
	{
		private readonly string _FilePath = string.Empty; // Путь к обрабатываемому файлу
		
		private string _xmlText = string.Empty;
		
		private bool _preProcess = false;
		private bool _postProcess = false;
		
		/// <summary>
		/// Конструктор класса GenreCorrector
		/// </summary>
		/// <param name="FilePath">Путь к обрабатываемому файлу</param>
		/// <param name="xmlText">Строка для корректировки</param>
		/// <param name="preProcess">Удаление стартовых пробелов и перевода строки => всю книгу - в одну строку</param>
		/// <param name="postProcess">Вставка разрыва абзаца между смежными тегами</param>
		public GenreCorrector( string FilePath, ref string xmlText, bool preProcess, bool postProcess )
		{
			_FilePath = FilePath;
			_xmlText = xmlText;
			_preProcess = preProcess;
			_postProcess = postProcess;
		}
		
		/// <summary>
		/// Корректировка тегов genre
		/// </summary>
		/// <returns>Откорректированную строку типа string </returns>
		public string correct() {
			if ( _xmlText.IndexOf( "<genre", StringComparison.CurrentCulture ) == -1 )
				return _xmlText;
			
			// преобработка (удаление стартовых пробелов ДО тегов и удаление завершающих пробелов ПОСЛЕ тегов и символов переноса строк)
			if ( _preProcess )
				_xmlText = FB2CleanCode.preProcessing( _xmlText );

			/* ЛитРПГ */
			// обработка жанра sf_litRPG
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(sf_litRPG)\\s*?(?=</genre>)",
					"${genre}sf_litrpg", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			}
			catch (RegexMatchTimeoutException /*ex*/ ) { }
			catch (Exception ex)
			{
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра sf_litRPG."
				);
			}

			// обработка жанра litrpg, LitRPG, ЛитРПГ
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(litrpg|LitRPG|ЛитРПГ)\\s*?(?=</genre>)",
					"${genre}sf_litrpg", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			}
			catch (RegexMatchTimeoutException /*ex*/ ) { }
			catch (Exception ex)
			{
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра litrpg, LitRPG, ЛитРПГ."
				);
			}


			/* Фантастика */
			// обработка жанра romance_fantasy, romance_sf, magician_book, foreign_fantasy,
			// dragon_fantasy, fantasy, fantasy-action, dark-fantasy 
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(romance_fantasy|romance_sf|magician_book|foreign_fantasy|dragon_fantasy|fantasy|dark-fantasy|fantasy-action)\\s*?(?=</genre>)",
					"${genre}sf_fantasy", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра romance_fantasy, romance_sf, magician_book, foreign_fantasy, dragon_fantasy, fantasy, dark-fantasy, fantasy-action."
				);
			}
			
			// обработка жанра horror, vampire_book, horror_usa
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(horror|vampire_book|horror_usa)\\s*?(?=</genre>)",
					"${genre}sf_horror", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра horror, vampire_book, horror_usa."
				);
			}

			// обработка жанра horror_fantasy, horror_vampires, horror_occult, paranormal
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(paranormal|horror_fantasy|horror_vampires|horror_occult)\\s*?(?=</genre>)",
					"${genre}sf_mystic", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра paranormal, horror_fantasy, horror_vampires, horror_occult."
				);
			}

			// обработка жанра sf_history_avant, fantasy_alt_hist, historical-fantasy
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(sf_history_avant|fantasy_alt_hist|historical-fantasy)\\s*?(?=</genre>)",
					"${genre}historical_fantasy", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра sf_history_avant, fantasy_alt_hist, historical-fantasy."
				);
			}

			// обработка жанра Альтернативная история, sf-history
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(sf-history|Альтернативная история)\\s*?(?=</genre>)",
					"${genre}sf_history", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанров Альтернативная история, sf-history."
				);
			}

			// обработка жанра heroic-fantasy, sf-heroic
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(heroic-fantasy|sf-heroic)\\s*?(?=</genre>)",
					"${genre}sf_heroic", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			}
			catch (RegexMatchTimeoutException /*ex*/ ) { }
			catch (Exception ex)
			{
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанров heroic-fantasy, sf-heroic."
				);
			}

			// обработка жанра sf_cyber_punk, cyberpunk
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(sf_cyber_punk|cyberpunk)\\s*?(?=</genre>)",
					"${genre}sf_cyberpunk", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанроов sf_cyber_punk, cyberpunk."
				);
			}
			
			// обработка жанра city_fantasy
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?city_fantasy\\s*?(?=</genre>)",
					"${genre}sf_fantasy_city", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра city_fantasy."
				);
			}

			// обработка жанра epic-fantasy
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?epic-fantasy\\s*?(?=</genre>)",
					"${genre}sf_epic", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			}
			catch (RegexMatchTimeoutException /*ex*/ ) { }
			catch (Exception ex)
			{
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра epic-fantasy."
				);
			}

			// обработка жанра fantasy_fight, Боевая фантастика, sf-action
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(fantasy_fight|sf-action|Боевая фантастика)\\s*?(?=</genre>)",
					"${genre}sf_action", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра fantasy_fight, sf-action, Боевая фантастика."
				);
			}
			
			// обработка жанра foreign_sf, Фантастика
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(foreign_sf|Фантастика)\\s*?(?=</genre>)",
					"${genre}sf_etc", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра foreign_sf, Фантастика."
				);
			}
			
			// обработка жанра love_fantasy
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?love_fantasy\\s*?(?=</genre>)",
					"${genre}love_sf", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра love_fantasy."
				);
			}

			// обработка жанра SF, Научная Фантастика, science-fiction, sci-fi
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(SF|science-fiction|sci-fi|Научная Фантастика)\\s*?(?=</genre>)",
					"${genre}sf", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра SF, Научная Фантастика, science-fiction, sci-fi."
				);
			}
			
			// обработка жанра Юмористическая фантастика
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Юмористическая фантастика\\s*?(?=</genre>)",
					"${genre}sf_humor", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра Юмористическая фантастика."
				);
			}

			// обработка жанра ironical-fantasy, Фэнтези Юмор
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(ironical-fantasy|Фэнтези Юмор)\\s*?(?=</genre>)",
					"${genre}humor_fantasy", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра ironical-fantasy, Фэнтези Юмор."
				);
			}

			// обработка жанра Космическая фантастика, sf-space
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(sf-space|Космическая фантастика)\\s*?(?=</genre>)",
					"${genre}sf_space", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			}
			catch (RegexMatchTimeoutException /*ex*/ ) { }
			catch (Exception ex)
			{
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанров Космическая фантастика, sf-space."
				);
			}

			// обработка жанра sf-social
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(sf-social)\\s*?(?=</genre>)",
					"${genre}sf_social", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			}
			catch (RegexMatchTimeoutException /*ex*/ ) { }
			catch (Exception ex)
			{
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра sf-social."
				);
			}

			// обработка жанра fantasy-erotika
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(fantasy-erotika)\\s*?(?=</genre>)",
					"<genre>sf_fantasy</genre>${genre}love_sf", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			}
			catch (RegexMatchTimeoutException /*ex*/ ) { }
			catch (Exception ex)
			{
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра fantasy-erotika."
				);
			}

			// обработка жанра Боевое фэнтези, fantasy-action
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(Боевое фэнтези|fantasy-action)\\s*?(?=</genre>)",
					"<genre>sf_fantasy</genre>${genre}sf_action", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанров Боевое фэнтези, fantasy-action."
				);
			}

			// обработка жанра popadantsy-v-magicheskie-miry
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(popadantsy-v-magicheskie-miry)\\s*?(?=</genre>)",
					"<genre>popadanec</genre>${genre}sf_fantasy", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			}
			catch (RegexMatchTimeoutException /*ex*/ ) { }
			catch (Exception ex)
			{
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра popadantsy-v-magicheskie-miry."
				);
			}

			// обработка жанра popadantsy-v-kosmos
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(popadantsy-v-kosmos)\\s*?(?=</genre>)",
					"<genre>popadanec</genre>${genre}sf_space", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			}
			catch (RegexMatchTimeoutException /*ex*/ ) { }
			catch (Exception ex)
			{
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра popadantsy-v-kosmos."
				);
			}

			// обработка жанра postapocalyptic
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?postapocalyptic\\s*?(?=</genre>)",
					"${genre}sf_postapocalyptic", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			}
			catch (RegexMatchTimeoutException /*ex*/ ) { }
			catch (Exception ex)
			{
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра postapocalyptic."
				);
			}

			// обработка жанра popadantsy, popadantsy-vo-vremeni
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(popadantsy|popadantsy-vo-vremeni)\\s*?(?=</genre>)",
					"${genre}popadanec", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			}
			catch (RegexMatchTimeoutException /*ex*/ ) { }
			catch (Exception ex)
			{
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра popadantsy, popadantsy-vo-vremeni."
				);
			}

			/* Юмор */
			// обработка жанра entert_humor, foreign_humor, humor_all
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(entert_humor|foreign_humor|humor_all)\\s*?(?=</genre>)",
					"${genre}humor", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра entert_humor, foreign_humor, humor_all."
				);
			}
			
			
			// обработка жанра юмористическая проза
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?юмористическая проза\\s*?(?=</genre>)",
					"${genre}humor_prose", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра 'Юмористическая проза'."
				);
			}
			
			// обработка жанра Комедия
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Комедия\\s*?(?=</genre>)",
					"${genre}comedy", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра Комедия."
				);
			}

			/* Детективы и триллеры */
			// обработка жанра thriller_police
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?thriller_police\\s*?(?=</genre>)",
					"${genre}det_police", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра thriller_police."
				);
			}

			// обработка жанра kriminal_detective
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?kriminal_detective\\s*?(?=</genre>)",
					"${genre}det_crime", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			}
			catch (RegexMatchTimeoutException /*ex*/ ) { }
			catch (Exception ex)
			{
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра kriminal_detective."
				);
			}

			// обработка жанра thriller_mystery
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?thriller_mystery\\s*?(?=</genre>)",
					"${genre}thriller</genre><genre>sf_mystic", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра thriller_mystery."
				);
			}

			// обработка жанра thriller_psychology
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?thriller_psychology\\s*?(?=</genre>)",
					"${genre}thriller</genre><genre>detective", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			}
			catch (RegexMatchTimeoutException /*ex*/ ) { }
			catch (Exception ex)
			{
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра thriller_psychology."
				);
			}

			// обработка жанра foreign_detective, detektive
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(detektive|foreign_detective)\\s*?(?=</genre>)",
					"${genre}detective", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанров foreign_detective, detektive."
				);
			}

			// обработка жанра foreign_action, action
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(foreign_action|action)\\s*?(?=</genre>)",
					"${genre}det_action", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанров foreign_action, action."
				);
			}
			
			// обработка жанра Шпионский Детектив
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Шпионский Детектив\\s*?(?=</genre>)",
					"${genre}det_espionage", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра 'Шпионский Детектив'."
				);
			}
			
			// обработка жанра исторический детектив
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?исторический детектив\\s*?(?=</genre>)",
					"${genre}det_history", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра 'Исторический детектив'."
				);
			}
			
			// обработка жанра Иронический детектив
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Иронический детектив\\s*?(?=</genre>)",
					"${genre}det_irony", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра 'Иронический детектив'."
				);
			}
			
			/* Приключения */
			// обработка жанра literature_western
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature_western\\s*?(?=</genre>)",
					"${genre}adv_western</genre><genre>detective", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра literature_western."
				);
			}
			
			// обработка жанра romance_historical, adv_history_avant
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(romance_historical|adv_history_avant)\\s*?(?=</genre>)",
					"${genre}adv_history", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра romance_historical, adv_history_avant."
				);
			}
			
			// обработка жанра literature_sea
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature_sea\\s*?(?=</genre>)",
					"${genre}adv_maritime", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра literature_sea."
				);
			}
			
			// обработка жанра literature_adv
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature_adv\\s*?(?=</genre>)",
					"${genre}adv_story", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра literature_adv."
				);
			}
			
			// обработка жанра foreign_adventure
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?foreign_adventure\\s*?(?=</genre>)",
					"${genre}adv_modern", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра foreign_adventure."
				);
			}
			
			// обработка жанра Приключения
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Приключения\\s*?(?=</genre>)",
					"${genre}adventure", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра 'Приключения'."
				);
			}
			
			/* Детское и подростковое */
			// обработка жанра child_4, literature_fairy, Сказка, Сказки
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(child_4|literature_fairy|Сказка|Сказки)\\s*?(?=</genre>)",
					"${genre}child_tale", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра child_4, literature_fairy, Сказка, Сказки."
				);
			}

			// обработка жанра child_9, child_characters, Детская литература, detskaya-literatura
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(child_9|child_characters|detskaya-literatura|Детская литература)\\s*?(?=</genre>)",
					"${genre}children", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра child_9, child_characters, Детская литература, detskaya-literatura."
				);
			}
			
			// обработка жанра child_animals, outdoors_fauna, outdoors_hunt_fish, child_nature
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(child_animals|outdoors_fauna|outdoors_hunt_fish|child_nature)\\s*?(?=</genre>)",
					"${genre}adv_animal", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра child_animals, outdoors_fauna, outdoors_hunt_fish, child_nature."
				);
			}
			
			// обработка жанра teens_history, teens_literature, prose_teen
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(teens_history|teens_literature|prose_teen)\\s*?(?=</genre>)",
					"${genre}ya", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра teens_history, teens_literature, prose_teen."
				);
			}
			
			// обработка жанра teens_sf
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?teens_sf\\s*?(?=</genre>)",
					"${genre}child_sf", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра teens_sf."
				);
			}
			
			// обработка жанра child_history
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?child_history\\s*?(?=</genre>)",
					"${genre}child_adv", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра child_history."
				);
			}
			
			// обработка жанра Детская Проза
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Детская Проза\\s*?(?=</genre>)",
					"${genre}child_prose", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра 'Детская Проза'."
				);
			}
			
			/* Проза, драма */
			// обработка жанра prose_su_classic
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?prose_su_classic\\s*?(?=</genre>)",
					"${genre}prose_su_classics", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра prose_su_classic."
				);
			}
			
			// обработка жанра prose_rus_classics
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?prose_rus_classics\\s*?(?=</genre>)",
					"${genre}prose_rus_classic", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра prose_rus_classics."
				);
			}
			
			// обработка жанра proce, literature, prose_root, literature_books, Проза, literature_antology
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(proce|literature|prose_root|literature_books|Проза|literature_antology)\\s*?(?=</genre>)",
					"${genre}prose", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра proce, literature, prose_root, literature_books, Проза, literature_antology."
				);
			}
			
			// обработка жанра literature_classics, Классическая Проза
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(literature_classics|Классическая Проза)\\s*?(?=</genre>)",
					"${genre}prose_classic", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра literature_classics, Классическая Проза."
				);
			}
			
			// обработка жанра literature19
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature19\\s*?(?=</genre>)",
					"${genre}literature_19", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра literature19."
				);
			}
			
			// обработка жанра literature_su_classics
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature_su_classics\\s*?(?=</genre>)",
					"${genre}prose_su_classics", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра literature_su_classics."
				);
			}
			
			// обработка жанра literature_rus_classsic
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature_rus_classsic\\s*?(?=</genre>)",
					"${genre}prose_rus_classic", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра literature_rus_classsic."
				);
			}
			
			// обработка жанра Контркультура
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Контркультура\\s*?(?=</genre>)",
					"${genre}prose_counter", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра 'Контркультура'."
				);
			}

			// обработка жанра romance_contemporary, russian_contemporary, Современная Проза, modern-prose
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(romance_contemporary|modern-prose|russian_contemporary|Современная Проза)\\s*?(?=</genre>)",
					"${genre}prose_contemporary", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра modern-prose, romance_contemporary, russian_contemporary, Современная Проза."
				);
			}
			
			// обработка жанра Историческая проза
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Историческая проза\\s*?(?=</genre>)",
					"${genre}prose_history", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра 'Историческая проза'."
				);
			}
			
			// обработка жанра literature_drama, foreign_dramaturgy
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(literature_drama|foreign_dramaturgy)\\s*?(?=</genre>)",
					"${genre}dramaturgy", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра literature_drama, foreign_dramaturgy."
				);
			}
			
			// обработка жанра literature_short
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature_short\\s*?(?=</genre>)",
					"${genre}short_story", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра literature_short."
				);
			}
			
			// обработка жанра narrative
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?narrative\\s*?(?=</genre>)",
					"${genre}great_story", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра narrative."
				);
			}
			
			// обработка жанра foreign_novel
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?foreign_novel\\s*?(?=</genre>)",
					"${genre}story", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра foreign_novel."
				);
			}
			
			// обработка жанра romance
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?romance\\s*?(?=</genre>)",
					"${genre}roman", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра romance."
				);
			}
			
			// обработка жанра literature_world, foreign_contemporary
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(literature_world|foreign_contemporary)\\s*?(?=</genre>)",
					"${genre}foreign_prose", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра literature_world, foreign_contemporary."
				);
			}
			
			// обработка жанра sketch
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?sketch\\s*?(?=</genre>)",
					"${genre}essay", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра sketch."
				);
			}

			// обработка жанра aphorism_quote
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?aphorism_quote\\s*?(?=</genre>)",
					"${genre}aphorisms", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра aphorism_quote."
				);
			}
			
			// обработка жанра essays
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?essays\\s*?(?=</genre>)",
					"${genre}essay", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра essays."
				);
			}
			
			/* Прочее */
			// обработка жанра literature_essay, beginning_authors
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(literature_essay|beginning_authors)\\s*?(?=</genre>)",
					"${genre}network_literature", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра literature_essay, beginning_authors."
				);
			}
			
			// обработка жанра romance_multicultural
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?romance_multicultural\\s*?(?=</genre>)",
					"${genre}art_world_culture", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра romance_multicultural."
				);
			}
			
			// обработка жанра Журналы, newspapers, Газеты и журналы
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(Журналы|newspapers|Газеты и журналы)\\s*?(?=</genre>)",
					"${genre}periodic", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра Журналы, newspapers, Газеты и журналы."
				);
			}
			
			// обработка жанра Фанфик
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(Фанфик)\\s*?(?=</genre>)",
					"${genre}fanfiction", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра Фанфик."
				);
			}
			
			// обработка жанра samizdat
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(samizdat)\\s*?(?=</genre>)",
					"${genre}network_literature", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра samizdat."
				);
			}
			
			/* Научные и образовательные */
			// обработка жанра science_medicine, medicine
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(science_medicine|medicine)\\s*?(?=</genre>)",
					"${genre}sci_medicine", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра science_medicine, medicine."
				);
			}

			// обработка жанра culture_all
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(culture_all)\\s*?(?=</genre>)",
					"${genre}sci_culture", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			}
			catch (RegexMatchTimeoutException /*ex*/ ) { }
			catch (Exception ex)
			{
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра culture_all."
				);
			}

			// обработка жанра history_russia, history_asia, history_middle_east, history_usa, history_europe, literature_history, history_ancient, history_world, history_australia, history_africa, История
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(history_russia|history_asia|history_middle_east|history_usa|history_europe|literature_history|history_ancient|history_world|history_australia|history_africa|История)\\s*?(?=</genre>)",
					"${genre}sci_history", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра history_russia, history_asia, history_middle_east, history_usa, history_europe, literature_history, history_ancient, history_world, history_australia, history_africa, История."
				);
			}
			
			// обработка жанра science_biolog
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?science_biolog\\s*?(?=</genre>)",
					"${genre}sci_biology", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра science_biolog."
				);
			}
			
			// обработка жанра science_history_philosophy
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?science_history_philosophy\\s*?(?=</genre>)",
					"${genre}sci_philosophy", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра science_history_philosophy."
				);
			}
			
			// обработка жанра science_earth, geography_book
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(science_earth|geography_book)\\s*?(?=</genre>)",
					"${genre}sci_geo", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра science_earth, geography_book."
				);
			}
			
			// обработка жанра foreign_edu
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?foreign_edu\\s*?(?=</genre>)",
					"${genre}sci_popular", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра foreign_edu."
				);
			}
			
			// обработка жанра psy_social, sociology_book, nonfiction_sociology, nonfiction_social_sci
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(psy_social|sociology_book|nonfiction_sociology|nonfiction_social_sci|nonfiction_social_work)\\s*?(?=</genre>)",
					"${genre}sci_social_studies", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра psy_social, sociology_book, nonfiction_sociology, nonfiction_social_sci."
				);
			}
			
			// обработка жанра pedagogy_book
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?pedagogy_book\\s*?(?=</genre>)",
					"${genre}sci_pedagogy", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра pedagogy_book."
				);
			}
			
			// обработка жанра health_psy, science_psy, upbringing_book, foreign_psychology, psy_generic, psy_alassic
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(health_psy|science_psy|upbringing_book|foreign_psychology|psy_generic|psy_alassic)\\s*?(?=</genre>)",
					"${genre}sci_psychology", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра health_psy, science_psy, upbringing_book, foreign_psychology, psy_generic, psy_alassic."
				);
			}
			
			// обработка жанра языкознание
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?языкознание\\s*?(?=</genre>)",
					"${genre}sci_linguistic", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра 'Языкознание'."
				);
			}

			// обработка жанра Религиоведение, religion_earth, religion_all
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(Религиоведение|religion_earth|religion_all)\\s*?(?=</genre>)",
					"${genre}sci_religion", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра Религиоведение, religion_earth, religion_all."
				);
			}
			
			// обработка жанра Математика
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Математика\\s*?(?=</genre>)",
					"${genre}sci_math", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра 'Математика'."
				);
			}
			
			// обработка жанра unrecognised
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?unrecognised\\s*?(?=</genre>)",
					"${genre}sci_theories", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра unrecognised."
				);
			}
			
			// обработка жанра health_alt_medicine, Альтернативная медицина
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(health_alt_medicine|Альтернативная медицина)\\s*?(?=</genre>)",
					"${genre}sci_medicine_alternative", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра health_alt_medicine, Альтернативная медицина."
				);
			}
			
			/* Техника */
			// обработка жанра science_measurement
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?science_measurement\\s*?(?=</genre>)",
					"${genre}sci_tech", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра science_measurement."
				);
			}

			/* Поэзия */
			// обработка жанра literature_poetry, Поэзия, поэзия, poetry_all
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(literature_poetry|poetry_all|Поэзия)\\s*?(?=</genre>)",
					"${genre}poetry", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанров literature_poetry, poetry_all, Поэзия, поэзия."
				);
			}
			
			// обработка жанра foreign_poetry
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?foreign_poetry\\s*?(?=</genre>)",
					"${genre}poetry_for_classical", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра foreign_poetry."
				);
			}
			
			// обработка жанра Existential poetry
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Existential poetry\\s*?(?=</genre>)",
					"${genre}experimental_poetry", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра 'Existential poetry'."
				);
			}
			
			/* Фольклер */
			// обработка жанра nonfiction_folklor
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?nonfiction_folklor\\s*?(?=</genre>)",
					"${genre}folklore", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра nonfiction_folklor."
				);
			}
			
			/* Религия */
			// обработка жанра religion_buddhism, rel_boddizm
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(religion_buddhism|rel_boddizm)\\s*?(?=</genre>)",
					"${genre}religion_budda", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра religion_buddhism, rel_boddizm."
				);
			}
			
			// обработка жанра chris_pravoslavie, chris_orthodoxy, Православие
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(chris_pravoslavie|chris_orthodoxy|Православие)\\s*?(?=</genre>)",
					"${genre}religion_orthodoxy", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра chris_pravoslavie, chris_orthodoxy, Православие."
				);
			}
			
			// обработка жанра literature_religion, foreign_religion, Религия
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(literature_religion|foreign_religion|Религия)\\s*?(?=</genre>)",
					"${genre}religion_rel", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра literature_religion, foreign_religion, Религия."
				);
			}
			
			// обработка жанра religion_spirituality, Religion, religion_bibles
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(religion_spirituality|Religion|religion_bibles)\\s*?(?=</genre>)",
					"${genre}religion", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра religion_spirituality, Religion, religion_bibles."
				);
			}
			
			// обработка жанра nonfiction_philosophy
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?nonfiction_philosophy\\s*?(?=</genre>)",
					"${genre}sci_philosophy", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра nonfiction_philosophy."
				);
			}
			
			// обработка жанра chris_fiction, Христианство
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(chris_fiction|Христианство)\\s*?(?=</genre>)",
					"${genre}religion_christianity", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра chris_fiction, Христианство."
				);
			}
			
			// обработка жанра Эзотерика
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Эзотерика\\s*?(?=</genre>)",
					"${genre}religion_esoterics", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра 'Эзотерика'."
				);
			}
			
			// обработка жанра Самосовершенствование
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Самосовершенствование\\s*?(?=</genre>)",
					"${genre}religion_self", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра 'Самосовершенствование'."
				);
			}
			
			/* Военное */
			// обработка жанра literature_war
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature_war\\s*?(?=</genre>)",
					"${genre}prose_military", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра literature_war."
				);
			}
			
			// обработка жанра histor_military, history_military_science
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(histor_military|history_military_science)\\s*?(?=</genre>)",
					"${genre}military_history", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра histor_military, history_military_science."
				);
			}
			
			/* Путеществие и туризм */
			// обработка жанра travel_guidebook_series
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?travel_guidebook_series\\s*?(?=</genre>)",
					"${genre}ref_guide", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра travel_guidebook_series."
				);
			}
			
			// обработка жанра outdoors_nature_writing, outdoors_conservation, outdoors_travel
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(outdoors_nature_writing|outdoors_conservation|outdoors_travel)\\s*?(?=</genre>)",
					"${genre}travel_notes", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра outdoors_nature_writing, outdoors_conservation, outdoors_travel."
				);
			}
			
			// обработка жанра travel, travel_asia, travel_europe, travel_africa, travel_lat_am, travel_spec, travel_polar
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(travel|travel_asia|travel_europe|travel_africa|travel_lat_am|travel_spec|travel_polar)\\s*?(?=</genre>)",
					"${genre}adv_geo", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра travel, travel_asia, travel_europe, travel_africa, travel_lat_am, travel_spec, travel_polar."
				);
			}
			
			/* Публицистика и документалистика */
			// обработка жанра biogr_historical, biogr_arts, biography, biogr_leaders, biogr_professionals, biogr_sports, biz_beogr, biogr_travel, Биографии и мемуары
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(biogr_historical|biography|biogr_leaders|biogr_arts|biogr_professionals|biogr_sports|biz_beogr|biogr_travel|Биографии и мемуары)\\s*?(?=</genre>)",
					"${genre}nonf_biography", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра biogr_historical, biogr_arts, biography, biogr_leaders, biogr_professionals, biogr_sports, biz_beogr, biogr_travel, Биографии и мемуары."
				);
			}

			// обработка жанра foreign_publicism, Публицистика, publicism
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(foreign_publicism|Публицистика|publicism)\\s*?(?=</genre>)",
					"${genre}nonf_publicism", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра foreign_publicism, Публицистика, publicism."
				);
			}
			
			// обработка жанра foreign_desc, nonfiction_spec_group, people, nonfiction_crime, nonfiction_edu
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(foreign_desc|nonfiction_spec_group|people|nonfiction_crime|nonfiction_edu)\\s*?(?=</genre>)",
					"${genre}nonfiction", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра foreign_desc, nonfiction_spec_group, people, nonfiction_crime, nonfiction_edu."
				);
			}
			
			// обработка жанра literature_critic
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature_critic\\s*?(?=</genre>)",
					"${genre}nonf_criticism", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра literature_critic."
				);
			}
			
			/* Политика, экономика юриспруденция, деловая литература */
			// обработка жанра nonfiction_politics, literature_political
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(nonfiction_politics|literature_political)\\s*?(?=</genre>)",
					"${genre}sci_politics", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра nonfiction_politics, literature_political."
				);
			}

			// обработка жанра biz_economics, sci_economics
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(biz_economics|sci_economics)\\s*?(?=</genre>)",
					"${genre}sci_economy", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанров biz_economics, sci_economics."
				);
			}
			
			// обработка жанра nonfiction_law, professional_law
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(nonfiction_law|professional_law)\\s*?(?=</genre>)",
					"${genre}sci_juris", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра nonfiction_law, professional_law."
				);
			}
			
			// обработка жанра foreign_business
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?foreign_business\\s*?(?=</genre>)",
					"${genre}economics_ref", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра foreign_business."
				);
			}
			
			// обработка жанра psy_personal, biz_management
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(psy_personal|biz_management)\\s*?(?=</genre>)",
					"${genre}management", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра psy_personal, biz_management."
				);
			}
			
			// обработка жанра business
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?business\\s*?(?=</genre>)",
					"${genre}popular_business", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра business."
				);
			}
			
			// обработка жанра Недвижимость
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Недвижимость\\s*?(?=</genre>)",
					"${genre}real_estate", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра 'Недвижимость'."
				);
			}
			
			/* Семья и здоровье */
			// обработка жанра sport
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?sport\\s*?(?=</genre>)",
					"${genre}home_sport", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра sport."
				);
			}
			
			// обработка жанра health, health_men, health_women, teens_health, health_self_help, health_rel
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(health|health_men|health_women|teens_health|health_self_help|health_rel)\\s*?(?=</genre>)",
					"${genre}home_health", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра health, health_men, health_women, teens_health, health_self_help, health_rel."
				);
			}
			
			// обработка жанра health_sex
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?health_sex\\s*?(?=</genre>)",
					"${genre}home_sex", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра health_sex."
				);
			}
			
			// обработка жанра family_relations, family_parenting, foreign_home
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(family_relations|family_parenting|foreign_home)\\s*?(?=</genre>)",
					"${genre}family", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра family_relations, family_parenting, foreign_home."
				);
			}
			
			// обработка жанра family_pregnancy, family_health
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(family_pregnancy|family_health)\\s*?(?=</genre>)",
					"${genre}psy_sex_and_family", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра family_pregnancy, family_health."
				);
			}
			
			// обработка жанра health_nutrition, cooking, Кулинария
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(health_nutrition|cooking|Кулинария)\\s*?(?=</genre>)",
					"${genre}home_cooking", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра health_nutrition, cooking, Кулинария."
				);
			}
			
			// обработка жанра entertainment
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?entertainment\\s*?(?=</genre>)",
					"${genre}home_entertain", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра entertainment."
				);
			}
			
			/* Любовные книги */
			// обработка жанра literature_erotica, Эротика
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(literature_erotica|Эротика)\\s*?(?=</genre>)",
					"${genre}love_erotica", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра literature_erotica, Эротика."
				);
			}

			// обработка жанра women_single, literature_women, foreign_love, love_all
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(women_single|literature_women|foreign_love|love_all)\\s*?(?=</genre>)",
					"${genre}love", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра women_single, literature_women, foreign_love, love_all."
				);
			}
			
			// обработка жанра slash
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?slash\\s*?(?=</genre>)",
					"${genre}love_hard", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра slash."
				);
			}

			// обработка жанра Современные Любовные Романы, love-contemporary, love_conremporary, love_contrmporary
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(love_conremporary|love_contrmporary|love-contemporary|Современные Любовные Романы)\\s*?(?=</genre>)",
					"${genre}love_contemporary", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанров love_conremporary, love_contrmporary, love-contemporary, Современные Любовные Романы."
				);
			}
			
			// обработка жанра Исторические любовные романы
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Исторические любовные романы\\s*?(?=</genre>)",
					"${genre}love_history", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра 'Исторические любовные романы'."
				);
			}
			
			/* Искусство, Искусствоведение, Дизайн */
			// обработка жанра music_dancing
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?music_dancing\\s*?(?=</genre>)",
					"${genre}music", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра music_dancing."
				);
			}
			
			// обработка жанра cinema_theatre
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?cinema_theatre\\s*?(?=</genre>)",
					"${genre}cine", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра cinema_theatre."
				);
			}
			
			/* Компьютеры и Интернет */
			// обработка жанра foreign_comp
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?foreign_comp\\s*?(?=</genre>)",
					"${genre}computers", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра foreign_comp."
				);
			}
			
			// обработка жанра Интернет
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?Интернет\\s*?(?=</genre>)",
					"${genre}comp_www", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра 'Интернет'."
				);
			}
			
			/* Справочная литература */
			// обработка жанра ref_books
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?ref_books\\s*?(?=</genre>)",
					"${genre}ref_ref", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра ref_books."
				);
			}
			
			// обработка жанра hand-book
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?hand-book\\s*?(?=</genre>)",
					"${genre}ref_guide", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра hand-book."
				);
			}

			// обработка жанра ref_encyclopedia, ref_all
			try
			{
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(ref_encyclopedia|ref_all)\\s*?(?=</genre>)",
					"${genre}ref_encyc", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра ref_encyclopedia, ref_all."
				);
			}
			
			// обработка жанра geo_guide, Путеводители
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(geo_guide|Путеводители)\\s*?(?=</genre>)",
					"${genre}geo_guides", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "GenreCorrector:\r\nОбработка жанра  geo_guide, Путеводители."
				);
			}
			
			// постобработка (разбиение на теги (смежные теги) )
			if ( _postProcess )
				_xmlText = FB2CleanCode.postProcessing( _xmlText );
			
			return _xmlText;
		}
	}
}
