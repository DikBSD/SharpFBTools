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
		public FB2AutoCorrector()
		{
		}
		
		// Автокорректировка теста файла FilePath
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
			fb2Text.Description = autoCorrectDescription( fb2Text.toXML(), fb2Text.Description, htTags );
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
		
		// обработка описания книги. XmlText - xml текст всей книги (для определения языка книги)
		private static string autoCorrectDescription( string XmlText, string InputString, Hashtable htTags ) {
			if ( string.IsNullOrWhiteSpace( InputString ) || InputString.Length == 0 )
				return InputString;
			
			//  правка пространство имен
			string search21 = "xmlns=\"http://www.gribuser.ru/xml/fictionbook/2.1\"";
			string search22 = "xmlns=\"http://www.gribuser.ru/xml/fictionbook/2.2\"";
			string replace = "xmlns=\"http://www.gribuser.ru/xml/fictionbook/2.0\"";
			int index = InputString.IndexOf( search21 );
			if ( index > 0 ) {
				InputString = InputString.Replace( search21, replace );
			} else {
				index = InputString.IndexOf( search22 );
				if ( index > 0 )
					InputString = InputString.Replace( search22, replace );
			}
			
			try {
				/***********************************************
				 * удаление атрибутов xmlns в теге description *
				 ***********************************************/
				// удаление пустого атрибута xmlns="" в теге description
				InputString = Regex.Replace(
					InputString, "(?<=<)(?'tag'description)(?:\\s+?xmlns=\"\"\\s*?)(?=>)",
					"${tag}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// удаление атрибута xmlns:xlink="www" в теге description
				InputString = Regex.Replace(
					InputString, "(?<=<)(?'tag'description)(?:\\s+?xmlns:(xlink|rdf)=\"[^\"]+?\"\\s*?)(?=>)",
					"${tag}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
					
				/****************
				 * Обработка id *
				 ****************/
				// обработка пустого id
				InputString = Regex.Replace(
					InputString, @"(?<=<id>)(?:\s*\s*)(?=</id>)",
					Guid.NewGuid().ToString().ToUpper(), RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка Либрусековских id
				InputString = Regex.Replace(
					InputString, @"(?<=<id>)\s*(Mon|Tue|Wed|Thu|Fri|Sat|Sun)\s+(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)\s+\d{2}\s+(\d{2}\:){2}\d{2}\s+\d{4}\s*(?=</id>)",
					Guid.NewGuid().ToString().ToUpper(), RegexOptions.Multiline
				);
				
				/******************
				 * Обработка Жанра *
				 ******************/
				// обработка жанра proce
				InputString = Regex.Replace(
					InputString, "(?'genre'<genre(?:\\s*match=\"\\d{1,3}\")?\\s*?>)\\s*proce\\s*(?=</genre>)",
					"${genre}prose", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка жанра literature19
				InputString = Regex.Replace(
					InputString, "(?'genre'<genre(?:\\s*match=\"\\d{1,3}\")?\\s*?>)\\s*literature19\\s*(?=</genre>)",
					"${genre}literature_19", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				
				/******************
				 * Обработка языка *
				 ******************/
				// замена <lang> для русских книг на ru для fb2 без <src-title-info>
				LangRuUkBeCorrector langRuUkBeCorrector = new LangRuUkBeCorrector( XmlText, ref InputString );
				bool IsCorrected = false;
				InputString = langRuUkBeCorrector.correct( ref IsCorrected );
				
				// обработка неверно заданного русского языка
				InputString = Regex.Replace(
					InputString, @"(?<=<lang>)(?:\s*)(?:RU-ru|Rus)(?:\s*)(?=</lang>)",
					"ru", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка неверно заданного английского языка
				InputString = Regex.Replace(
					InputString, @"(?<=<lang>)(?:\s*)(?:EN-en|Eng)(?:\s*)(?=</lang>)",
					"en", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch (Exception /*ex*/) {
//				MessageBox.Show(ex.Message);
			}
			return autoCorrect( InputString, htTags );
		}
		
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
		
		// Автокорректировка текста строки InputString
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
					InputString, "(?'img'<image [^<]+?(?:\"[^\"]*\"|'[^']*')?)(?'more'>)(?:\\s*?</image>)",
					"${img} /${more}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка картинки между <section> и <title>
				InputString = Regex.Replace(
					InputString, "(?'sect'<section>\\s*)(?'img'<image[^/]+?(?:\"[^\"]*\"|'[^']*')?/>\\s*)(?'title'<title>\\s*)(?'p'(?:(?:<p[^>]*?(?:\"[^\"]*\"|'[^']*')?>\\s*)(?:.*?)\\s*(?:</p>\\s*)){1,})(?'_title'\\s*</title>)",
					"${sect}${title}${p}${_title}${img}", RegexOptions.IgnoreCase | RegexOptions.Multiline
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
