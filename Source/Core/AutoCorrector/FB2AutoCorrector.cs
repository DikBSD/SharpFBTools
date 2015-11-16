/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 06.10.2015
 * Время: 12:35
 * 
 * License: GPL 2.1
 */
using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;

using System.Windows.Forms;

using Core.FB2.FB2Parsers;

namespace Core.AutoCorrector
{
	/// <summary>
	/// Автокорректор fb2 файла в текстовом режиме с помощью регулярных выражений
	/// </summary>
	public class FB2AutoCorrector
	{
		// обработка картинок: <image l:href="#img_0.png"> </image> или <image l:href="#img_0.png">\n</image>
		private readonly static string _ImageFind = "(?'img'<image [^<]+?(?:\"[^\"]*\"|'[^']*')?)(?'more'>)(?:\\s*?</image>)";
		private readonly static string _ImageRepl = "${img} /${more}";
			
		public FB2AutoCorrector()
		{
		}
		
		/// <summary>
		/// Автокорректировка теста файла FilePath
		/// </summary>
		/// <param name="FilePath">Путь к обрабатываемой книге</param>
		public static void autoCorrector( string FilePath ) {
			FileInfo fi = new FileInfo( FilePath );
			if ( !fi.Exists )
				return;
			else if ( fi.Length < 4 )
				return;
			
			// обработка < > в тексте, кроме fb2 тегов
			Hashtable htTags = FB2CleanCode.getTagsHashtable();
			
			FB2Text fb2Text = new FB2Text( FilePath );
			string enc = fb2Text.Description.Substring( 0, fb2Text.Description.IndexOf( "<FictionBook" ) );
			if ( enc.ToLower().IndexOf( "wutf-8" ) > 0 ) {
				enc = enc.Substring( enc.ToLower().IndexOf( "wutf-8" ), 6 );
				fb2Text.Description = fb2Text.Description.Replace( enc, "utf-8" );
			} else if ( enc.ToLower().IndexOf( "utf8" ) > 0 ) {
				enc = enc.Substring( enc.ToLower().IndexOf( "utf8" ), 4 );
				fb2Text.Description = fb2Text.Description.Replace( enc, "utf-8" );
			}
			fb2Text.Description = autoCorrectDescription( fb2Text.Bodies, fb2Text.Description, htTags );
			fb2Text.Bodies = autoCorrect( fb2Text.Bodies, htTags );
			if( fb2Text.BinariesExists ) {
				// обработка ссылок
				LinksCorrector linksCorrector = new LinksCorrector( fb2Text.Binaries );
				fb2Text.Binaries = linksCorrector.correct();
			}
			
			try {
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.LoadXml( fb2Text.toXML() );
				xmlDoc.Save( FilePath );
			} catch {
				fb2Text.saveFile();
			}
		}
		
		/// <summary>
		/// Корректировка описания книги (description)
		/// </summary>
		/// <param name="XmlBody">xml текст body (для определения языка книги)</param>
		/// <param name="XmlDescription">Строка description для корректировки</param>
		/// <param name="htTags">Хэш таблица fb2 тегов</param>
		private static string autoCorrectDescription( string XmlBody, string XmlDescription, Hashtable htTags ) {
			if ( string.IsNullOrWhiteSpace( XmlDescription ) || XmlDescription.Length == 0 )
				return XmlDescription;
			
			//  правка пространство имен
			string search21 = "xmlns=\"http://www.gribuser.ru/xml/fictionbook/2.1\"";
			string search22 = "xmlns=\"http://www.gribuser.ru/xml/fictionbook/2.2\"";
			string replace = "xmlns=\"http://www.gribuser.ru/xml/fictionbook/2.0\"";
			int index = XmlDescription.IndexOf( search21 );
			if ( index > 0 ) {
				XmlDescription = XmlDescription.Replace( search21, replace );
			} else {
				index = XmlDescription.IndexOf( search22 );
				if ( index > 0 )
					XmlDescription = XmlDescription.Replace( search22, replace );
			}
			
			try {
				/***********************************************
				 * удаление атрибутов xmlns в теге description *
				 ***********************************************/
				// удаление пустого атрибута xmlns="" в теге description
				XmlDescription = Regex.Replace(
					XmlDescription, "(?<=<)(?'tag'description)(?:\\s+?xmlns=\"\"\\s*?)(?=>)",
					"${tag}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// удаление атрибута xmlns:xlink="www" в теге description
				XmlDescription = Regex.Replace(
					XmlDescription, "(?<=<)(?'tag'description)(?:\\s+?xmlns:(xlink|rdf)=\"[^\"]+?\"\\s*?)(?=>)",
					"${tag}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				
				/*********************
				 * Обработка графики *
				 ********************/
				// удаление "пустого" тега <coverpage></coverpage>
				XmlDescription = Regex.Replace(
					XmlDescription, @"(?:<coverpage>\s*?</coverpage>)",
					"", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка картинок: <image l:href="#img_0.png"> </image> или <image l:href="#img_0.png">\n</image>
				XmlDescription = Regex.Replace(
					XmlDescription, _ImageFind,
					_ImageRepl, RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
//				// обработка картинок: удаление атрибута xmlns:l="http://www.w3.org/1999/xlink" />
//				XmlDescription = Regex.Replace(
//					XmlDescription, "(?'image'<image l:href=\"[^\"]+\") xmlns:l=\"http://www.w3.org/1999/xlink\" ?(?=/>)",
//					"${image}", RegexOptions.IgnoreCase | RegexOptions.Multiline
//				);
				
				/****************
				 * Обработка id *
				 ****************/
				// обработка пустого id
				XmlDescription = Regex.Replace(
					XmlDescription, @"(?<=<id>)(?:\s*\s*)(?=</id>)",
					Guid.NewGuid().ToString().ToUpper(), RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка Либрусековских id
				XmlDescription = Regex.Replace(
					XmlDescription, @"(?<=<id>)\s*(Mon|Tue|Wed|Thu|Fri|Sat|Sun)\s+(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)\s+\d{2}\s+(\d{2}\:){2}\d{2}\s+\d{4}\s*(?=</id>)",
					Guid.NewGuid().ToString().ToUpper(), RegexOptions.Multiline
				);
				
				/******************
				 * Обработка Жанра *
				 ******************/				
				/* Фантастика */
				// обработка жанра romance_fantasy, romance_sf
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(romance_fantasy|romance_sf)\\s*?(?=</genre>)",
					"${genre}sf_fantasy", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра horror 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?horror\\s*?(?=</genre>)",
					"${genre}sf_horror", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра horror_fantasy, horror_vampires, horror_occult
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(horror_fantasy|horror_vampires|horror_occult)\\s*?(?=</genre>)",
					"${genre}sf_mystic", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра sf_history_avant, fantasy_alt_hist
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(sf_history_avant|fantasy_alt_hist)\\s*?(?=</genre>)",
					"${genre}historical_fantasy", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра sf_cyber_punk 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?sf_cyber_punk\\s*?(?=</genre>)",
					"${genre}sf_cyberpunk", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				
				/* Юмор */
				// обработка жанра entert_humor
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?entert_humor\\s*?(?=</genre>)",
					"${genre}humor", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				
				/* Детективы и триллеры */
				// обработка жанра thriller_police
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?thriller_police\\s*?(?=</genre>)",
					"${genre}det_police", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра thriller_mystery 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?thriller_mystery\\s*?(?=</genre>)",
					"${genre}thriller</genre><genre>detective", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				
				/* Приключения */
				// обработка жанра literature_western
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature_western\\s*?(?=</genre>)",
					"${genre}adv_western</genre><genre>detective", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра romance_historical, adv_history_avant
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(romance_historical|adv_history_avant)\\s*?(?=</genre>)",
					"${genre}adv_history", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра literature_sea 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature_sea\\s*?(?=</genre>)",
					"${genre}adv_maritime", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра literature_adv 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature_adv\\s*?(?=</genre>)",
					"${genre}adv_story", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				
				/* Детское и подростковое */
				// обработка жанра child_4, literature_fairy
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(child_4|literature_fairy)\\s*?(?=</genre>)",
					"${genre}child_tale", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра child_9 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?child_9\\s*?(?=</genre>)",
					"${genre}children", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра child_animals, outdoors_fauna
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(child_animals|outdoors_fauna)\\s*?(?=</genre>)",
					"${genre}adv_animal", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра teens_history, teens_literature
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(teens_history|teens_literature)\\s*?(?=</genre>)",
					"${genre}ya", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра teens_sf 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?teens_sf\\s*?(?=</genre>)",
					"${genre}child_sf", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра child_history 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?child_history\\s*?(?=</genre>)",
					"${genre}child_adv", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				
				/* Проза. драма */
				// обработка жанра proce, literature, prose_root
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(proce|literature|prose_root)\\s*?(?=</genre>)",
					"${genre}prose", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра literature_classics
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature_classics\\s*?(?=</genre>)",
					"${genre}prose_classic", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра literature19
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature19\\s*?(?=</genre>)",
					"${genre}literature_19", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра literature_su_classics
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature_su_classics\\s*?(?=</genre>)",
					"${genre}prose_su_classics", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра literature_rus_classsic
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature_rus_classsic\\s*?(?=</genre>)",
					"${genre}prose_rus_classic", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра romance_contemporary
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?romance_contemporary\\s*?(?=</genre>)",
					"${genre}prose_contemporary", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра literature_drama 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature_drama\\s*?(?=</genre>)",
					"${genre}dramaturgy", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра literature_short 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature_short\\s*?(?=</genre>)",
					"${genre}short_story", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра romance 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?romance\\s*?(?=</genre>)",
					"${genre}roman", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра literature_essay 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature_essay\\s*?(?=</genre>)",
					"${genre}network_literature", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				
				// обработка жанра literature_world 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature_world\\s*?(?=</genre>)",
					"${genre}foreign_prose", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				
				/* Научные и образовательные */
				// обработка жанра science_medicine 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?science_medicine\\s*?(?=</genre>)",
					"${genre}sci_medicine", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра history_russia, history_asia, history_middle_east, history_usa, history_europe, literature_history, history_ancient, history_world
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(history_russia|history_asia|history_middle_east|history_usa|history_europe|literature_history|history_ancient|history_world)\\s*?(?=</genre>)",
					"${genre}sci_history", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра science_biolog 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?science_biolog\\s*?(?=</genre>)",
					"${genre}sci_biology", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра science_history_philosophy 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?science_history_philosophy\\s*?(?=</genre>)",
					"${genre}sci_philosophy", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра science_earth 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?science_earth\\s*?(?=</genre>)",
					"${genre}sci_geo", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				
				/* Поэзия */
				// обработка жанра literature_poetry
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature_poetry\\s*?(?=</genre>)",
					"${genre}poetry", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				
				/* Фольклер */
				// обработка жанра nonfiction_folklor
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?nonfiction_folklor\\s*?(?=</genre>)",
					"${genre}folklore", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				
				/* Религия */
				// обработка жанра religion_buddhism, rel_boddizm
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(religion_buddhism|rel_boddizm)\\s*?(?=</genre>)",
					"${genre}religion_budda", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра chris_pravoslavie, chris_orthodoxy
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(chris_pravoslavie|chris_orthodoxy)\\s*?(?=</genre>)",
					"${genre}religion_orthodoxy", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра literature_religion 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature_religion\\s*?(?=</genre>)",
					"${genre}religion_rel", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра religion_spirituality 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?religion_spirituality\\s*?(?=</genre>)",
					"${genre}religion", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра nonfiction_philosophy 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?nonfiction_philosophy\\s*?(?=</genre>)",
					"${genre}sci_philosophy", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра chris_fiction 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?chris_fiction\\s*?(?=</genre>)",
					"${genre}religion_christianity", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				
				/* Военное */
				// обработка жанра literature_war 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature_war\\s*?(?=</genre>)",
					"${genre}prose_military", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра histor_military, history_military_science
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(histor_military|history_military_science)\\s*?(?=</genre>)",
					"${genre}military_history", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				
				/* Путеществие и туризм */
				// обработка жанра travel_guidebook_series 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?travel_guidebook_series\\s*?(?=</genre>)",
					"${genre}ref_guide", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра outdoors_nature_writing, outdoors_conservation
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(outdoors_nature_writing|outdoors_conservation)\\s*?(?=</genre>)",
					"${genre}travel_notes", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра travel, travel_asia, travel_europe, travel_africa, travel_lat_am, travel_spec
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(travel|travel_asia|travel_europe|travel_africa|travel_lat_am|travel_spec)\\s*?(?=</genre>)",
					"${genre}adv_geo", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				
				/* Публицистика и документалистика */
				// обработка жанра biogr_historical, biogr_arts, biography, biogr_leaders, biogr_professionals
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(biogr_historical|biography|biogr_leaders|biogr_arts|biogr_professionals)\\s*?(?=</genre>)",
					"${genre}nonf_biography", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				
				/* Политика, экономика юриспруденция, деловая литература */
				// обработка жанра nonfiction_politics, literature_political
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(nonfiction_politics|literature_political)\\s*?(?=</genre>)",
					"${genre}sci_politics", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра biz_economics 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?biz_economics\\s*?(?=</genre>)",
					"${genre}sci_economy", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра nonfiction_law, professional_law 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(nonfiction_law|professional_law)\\s*?(?=</genre>)",
					"${genre}sci_juris", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				
				/* Семья и здоровье */
				// обработка жанра sport 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?sport\\s*?(?=</genre>)",
					"${genre}home_sport", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра health, health_men, health_women, teens_health, health_self_help, health_rel
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(health|health_men|health_women|teens_health|health_self_help|health_rel)\\s*?(?=</genre>)",
					"${genre}home_health", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра health_alt_medicine 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?health_alt_medicine\\s*?(?=</genre>)",
					"${genre}sci_medicine_alternative", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра health_sex 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?health_sex\\s*?(?=</genre>)",
					"${genre}home_sex", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра family_relations, family_parenting
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(family_relations|family_parenting)\\s*?(?=</genre>)",
					"${genre}family", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра family_pregnancy, family_health
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(family_pregnancy|family_health)\\s*?(?=</genre>)",
					"${genre}psy_sex_and_family", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра health_nutrition 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?health_nutrition\\s*?(?=</genre>)",
					"${genre}home_cooking", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра health_psy, science_psy 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?(health_psy|science_psy)\\s*?(?=</genre>)",
					"${genre}sci_psychology", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);

				/* Любовные книги */
				// обработка жанра literature_erotica 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?literature_erotica\\s*?(?=</genre>)",
					"${genre}love_erotica", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра women_single 
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'genre'<genre(?:\\s*?match=\"\\d{1,3}\")?\\s*?>)\\s*?women_single\\s*?(?=</genre>)",
					"${genre}love", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				
				/******************
				 * Обработка языка *
				 ******************/
				// замена <lang> для русских книг на ru для fb2 без <src-title-info>
				LangRuUkBeCorrector langRuUkBeCorrector = new LangRuUkBeCorrector( ref XmlDescription, XmlBody );
				bool IsCorrected = false;
				XmlDescription = langRuUkBeCorrector.correct( ref IsCorrected );
				
				// обработка неверно заданного русского языка
				XmlDescription = Regex.Replace(
					XmlDescription, @"(?<=<lang>)(?:\s*)(?:RU-ru|Rus)(?:\s*)(?=</lang>)",
					"ru", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка неверно заданного английского языка
				XmlDescription = Regex.Replace(
					XmlDescription, @"(?<=<lang>)(?:\s*)(?:EN-en|Eng)(?:\s*)(?=</lang>)",
					"en", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				
				/*****************
				 * Обработка дат *
				 *****************/
				// обработка атрибута в датах типа <date value="16.01.2006">16.01.2006</date> => <date value="2006-01-16">16.01.2006</date>
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'start'<date +?value=\")(?'d'\\d\\d)\\.(?'m'\\d\\d)\\.(?'y'\\d\\d\\d\\d)(?'end'\">(?:\\d\\d\\.\\d\\d\\.\\d\\d\\d\\d</date>))",
					"${start}${y}-${m}-${d}${end}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка атрибута в датах типа <date value="2006.03.08">08.03.2006</date> => <date value="2006-03-08">08.03.2006</date>
				XmlDescription = Regex.Replace(
					XmlDescription, "(?'start'<date +?value=\")(?'y'\\d\\d\\d\\d)\\.(?'m'\\d\\d)\\.(?'d'\\d\\d)(?'end'\">(?:\\d\\d\\.\\d\\d\\.\\d\\d\\d\\d</date>))",
					"${start}${y}-${m}-${d}${end}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// удаление атрибута в датах типа <date value="05-06-21">21.06.05</date>
				XmlDescription = Regex.Replace(
					XmlDescription, "(?<=<date) value=\"\\d\\d[-.]\\d\\d[-.]\\d\\d\">\\s*?(?=\\d\\d\\.\\d\\d\\.\\d\\d\\s*?</date>)",
					">", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// удаление атрибута в датах типа <date value="2006">2006</date>
				XmlDescription = Regex.Replace(
					XmlDescription, "(?<=<date) value=\"\\d\\d\\d\\d\">\\s*?(?=\\d\\d\\d\\d\\s*?</date>)",
					">", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				/************************
				 * Обработка annotation *
				 ************************/
				// преобразование <title> в аннотации на <subtitle> (Заголовок - без тегов <strong>) и т.п.
				XmlDescription = Regex.Replace(
					XmlDescription, @"(?<=<annotation>)\s*?(?:<title>\s*?)(?:<p>\s*?)(?'title'[^<]+?)(?:\s*?</p>\s*?</title>)",
					"<subtitle>${title}</subtitle>", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				
			} catch (Exception /*ex*/) {
//				MessageBox.Show(ex.Message);
			}
			return autoCorrect( XmlDescription, htTags );
		}
		
		/// <summary>
		/// Корректировка "тела" книги (body)
		/// </summary>
		/// <param name="InputString">Строка для корректировки</param>
		/// <param name="htTags">Хэш таблица fb2 тегов</param>
		private static string autoCorrect( string InputString, Hashtable htTags ) {
			/* предварительная обработка текста */
			InputString = FB2CleanCode.preProcessing(
				/* чистка кода */
				FB2CleanCode.cleanFb2Code(
					/* удаление недопустимых символов */
					FB2CleanCode.deleteIllegalCharacters( InputString )
				)
			);
			
			/* пост обработка текста: разбиение на теги */
			return FB2CleanCode.postProcessing(
				/* автокорректировка файла */
				_autoCorrect(
					/* обработка < и > */
					FB2CleanCode.processingCharactersMoreAndLessAndAmp( InputString, htTags )
				)
			);
		}
		
		/// <summary>
		/// Автокорректировка текста строки InputString
		/// </summary>
		/// <param name="InputString">Строка для корректировки</param>
		private static string _autoCorrect( string InputString ) {
			if ( string.IsNullOrWhiteSpace( InputString ) || InputString.Length == 0 )
				return InputString;
			
			try {
				/********************
				 * Обработка ссылок *
				 *******************/
				// обработка ссылок
				LinksCorrector linksCorrector = new LinksCorrector( InputString );
				InputString = linksCorrector.correct();
				
				/****************************
				 * Обработка <empty-line /> *
				 ***************************/
				// удаление тегов <p> и </p> в структуре <p> <empty-line /> </p>
				InputString = Regex.Replace(
					InputString, @"(?:(?:<p>\s*)(?'empty'<empty-line\s*?/>)(?:\s*</p>))",
					"${empty}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				
				/********************************************************************
				 * удаление пустых атрибутов xmlns="" в тегах body, title и section *
				 ********************************************************************/
				// удаление пустого атрибута xmlns="" в тегах body и section
				InputString = Regex.Replace(
					InputString, "(?<=<)(?'tag'body|section|title)(?:\\s+?xmlns=\"\"\\s*?)(?=>)",
					"${tag}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				
				/*********************
				 * Обработка графики *
				 ********************/
				// обработка картинок: <image l:href="#img_0.png"> </image> или <image l:href="#img_0.png">\n</image>
				InputString = Regex.Replace(
					InputString, _ImageFind,
					_ImageRepl, RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка картинки между <section> и <title>
				InputString = Regex.Replace(
					InputString, "(?'sect'<section>\\s*)(?'img'<image[^/]+?(?:\"[^\"]*\"|'[^']*')?/>\\s*)(?'title'<title>\\s*)(?'p'(?:(?:<p[^>]*?(?:\"[^\"]*\"|'[^']*')?>\\s*)(?:.*?)\\s*(?:</p>\\s*)){1,})(?'_title'\\s*</title>)",
					"${sect}${title}${p}${_title}${img}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// вставка <empty-line /> после картинки, заключенной в тегах <section> ... </section>: <section><image l:href="#index.jpg" /></section> => <section><image l:href="#index.jpg" /><empty-line /></section>
				InputString = Regex.Replace(
					InputString, "(?'sect'<(?'section'section)>\\s*)(?'img'<image[^/]+?(?:\"[^\"]*\"|'[^']*')?/>\\s*)\\s*?(?'_sect'</\\k'section'>)",
					"${sect}${img}<empty-line />${_sect}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				
				/**********************
				 * Обработка структур *
				 *********************/
				// Удаление <empty-line/> между </section> и <section>
				InputString = Regex.Replace(
					InputString, @"(?<=</section>)\s*?<empty-line\s*?/>\s*?(?=<section>)",
					"", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// Преобразование вложенных друг в друга тегов cite в Автора: <cite><cite><p>Иванов</p></cite></cite> => <cite><text-author><p>Иванов</p></text-author></cite>
				InputString = Regex.Replace(
					InputString, @"(?:(?:<(?'tag'cite)\b>\s*?){2})(?:<p>\s*?)(?'text'(?:<(?'tag1'strong|emphasis)\b>)?\s*?(?:[^<]+)(?:</\k'tag1'>)?)(?:\s*?</p>)(?:\s*?</\k'tag'>){2}",
					"<${tag}><text-author>${text}</text-author></${tag}>", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// вставка <text-author> внутрь <poem> </poem>
				InputString = Regex.Replace(
					InputString, @"(?'_poem'</poem>)(?'ws'\s*)(?'textauthor'<text-author>\s*.+?\s*</text-author>)",
					"${textauthor}${ws}${_poem}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				
				// перестановка местами Текста Цитаты и ее автора: <cite><text-author>Автор</text-author><p>Цитата</p></cite> =><cite><p>Цитата</p><text-author>Автор</text-author></cite>
				InputString = Regex.Replace(
					InputString, @"(?<=<cite>)\s*?(?'author'<text-author>\s*?.*?</text-author>)\s*?(?'texts'(?:<p>\s*?.+?\s*?</p>\s*?){1,})\s*?(?=</cite>)",
					"${texts}${author}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				
				/**************************************
				 * Обработка подзаголовков <subtitle> *
				 **************************************/
				// обработка подзаголовков <subtitle> (<subtitle>\n<p>\nТекст\n</p>\n</subtitle>)
				InputString = Regex.Replace(
					InputString, @"(?'tag_start'<subtitle>)\s*<p>\s*(?'text'.+?)\s*</p>\s*(?'tag_end'</subtitle>)",
					"${tag_start}${text}${tag_end}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				
				/***********************************
				 * Обработка image и <empty-line/> *
				 ***********************************/
				// Вставка между <image ... /> (<image ... ></image>) и </section> недостающего тега <empty-line/>
				InputString = Regex.Replace(
					InputString, "(?'tag'(?:<image\\s+\\w+:href=\"#[^\"]*\"\\s*?/>)|(?:<image\\s+\\w+:href=\"#[^\"]*\">\\s*?</image>))(?:\\s*?)(?'_sect'</section>)",
					"${tag}<empty-line/>${_sect}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				
				/**********************
				 * Обработка epigraph *
				 **********************/
				if ( InputString.IndexOf( "<epigraph>" ) != -1 ) {
					EpigraphCorrector epigraphCorrector = new EpigraphCorrector ( ref InputString, true, false );
					InputString = epigraphCorrector.correct();
				}

				/*********************
				 * Обработка <title> *
				 ********************/
				if ( InputString.IndexOf( "<title>" ) != -1 ) {
					TitleCorrector titleCorrector = new TitleCorrector ( ref InputString, true, false );
					InputString = titleCorrector.correct();
				}
				
				/**************************
				 * Обработка <annotation> *
				 *************************/
				// Обработка <annotation><i>text</i></annotation> => <annotation><p>text</p></annotation>
				InputString = Regex.Replace(
					InputString, @"<(?'tag'annotation|cite)\b>\s*?<(?'format'i|b|emphasis|strong)\b>\s*?(?'text'[^<]+?\s*)</\k'format'>\s*?</\k'tag'>",
					"<${tag}><p>${text}</p></${tag}>", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				
				/********************
				 * Обработка Стихов *
				 ********************/
				// Удаление <empty-line /> в ситуациях: <v><empty-line />
				InputString = Regex.Replace(
					InputString, @"(?<=<v>)\s*?<empty-line ?/>",
					"", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// Удаление структур <poem><stanza /></poem>
				InputString = Regex.Replace(
					InputString, @"<poem>\s*?<stanza ?/>\s*?</poem>",
					"", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				
				/****************************
				 * Обработка форматирования *
				 ***************************/
				// обработка вложенных друг в друга тегов strong или emphasis: <emphasis><emphasis><p>text</p></emphasis></emphasis> => <p><emphasis>text</emphasis></p>
				InputString = Regex.Replace(
					InputString, "(?:(?'format'<(?'tag'strong|emphasis)>)\\s*?){2}(?'p'(?:<p(?:(?:[^>\"']|\"[^\"]*\"|'[^']*')*)>))\\s*?(?'text'(?:[^<]+))?(?'_p'(?:</p>))\\s*?(?'_format'</\\k'tag'>)\\s*?\\k'_format'",
					"${p}${format}${text}${_format}${_p}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// внесение тегов strong или emphasis в теги <p> </p>: <emphasis><p>text</p></emphasis> => <p><emphasis>text</emphasis></p>
				InputString = Regex.Replace(
					InputString, "(?'format'<(?'tag'strong|emphasis)>)\\s*?(?'p'(?:<p(?:(?:[^>\"']|\"[^\"]*\"|'[^']*')*)>))\\s*?(?'text'(?:[^<]+))?(?'_p'(?:</p>))\\s*?(?'_format'</\\k'tag'>)\\s*?",
					"${p}${format}${text}${_format}${_p}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				
				// замена тегов <strong> или <emphasis>, обрамляющих множественный текст на Цитату: <emphasis><p>Текст</p><p>Текст</p></emphasis> => <cite><p>Текст</p><p>Текст</p></cite>
				InputString = Regex.Replace(
					InputString, "(?:<(?'tag'strong|emphasis)>)\\s*?(?'text'(?:(?:<p(?:(?:[^>\"']|\"[^\"]*\"|'[^']*')*)>)\\s*?(?:[^<]+)?(?:</p>)\\s*?){2,})(?:</\\k'tag'>)",
					"<cite>${text}</cite>", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);

				/**********************
				 * Обработка тега <p> *
				 *********************/
				ParaCorrector paraCorrector = new ParaCorrector( ref InputString, false, false );
				InputString = paraCorrector.correct();

			} catch (Exception /*ex*/) {
//				MessageBox.Show(ex.Message);
			}
			
			return InputString;
		}
		
	}
}
