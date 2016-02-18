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

//using System.Windows.Forms;

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
			
			Hashtable htTags = FB2CleanCode.getTagsHashtable(); // обработка < > в тексте, кроме fb2 тегов
			
			// обработка головного тега FictionBook и пространства имен
			FB2Text fb2Text = new FB2Text( FilePath );
			if ( fb2Text.Description.IndexOf( "<FictionBook" ) == -1 ) {
				// тег FictionBook отсутствует в книге
				FictionBookTagCorrector fbtc = new FictionBookTagCorrector();
				fb2Text.Description = fb2Text.Description.Insert(
					fb2Text.Description.IndexOf( "<description>" ),
					fbtc.NewFictionBookTag
				);
			}

			// обработка неверного значения кодировки файла
			Regex regex = new Regex( "(?<=encoding=\")(?:(?:wutf-8)|(?:utf8))(?=\")", RegexOptions.IgnoreCase );
			fb2Text.Description = regex.Replace( fb2Text.Description, "utf-8" );
			// автокорректировка раздела description
			fb2Text.Description = autoCorrectDescription( fb2Text.Bodies, fb2Text.Description, htTags );
			// автокорректировка разделов body
			fb2Text.Bodies = autoCorrect( fb2Text.Bodies, htTags );
			// автокорректировка разделов binary
			if( fb2Text.BinariesExists ) {
				// обработка ссылок-названий картинок в binary
				BinaryCorrector binaryCorrector = new BinaryCorrector( fb2Text.Binaries );
				fb2Text.Binaries = binaryCorrector.correct();
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
			
			// обработка головного тега FictionBook и пространства имен
			FictionBookTagCorrector fbtc = new FictionBookTagCorrector();
			XmlDescription = fbtc.StartTagCorrect( XmlDescription );

			try {
				/***********************************************
				 * удаление атрибутов xmlns в теге description *
				 ***********************************************/
				// удаление пустого атрибута xmlns="" в теге description
				try {
					XmlDescription = Regex.Replace(
						XmlDescription, "(?<=<)(?'tag'description)(?:\\s+?xmlns=\"\"\\s*?)(?=>)",
						"${tag}", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException /*ex*/ ) {}
				// удаление атрибута xmlns:xlink="www" в теге description
				try {
					XmlDescription = Regex.Replace(
						XmlDescription, "(?<=<)(?'tag'description)(?:\\s+?xmlns:(xlink|rdf)=\"[^\"]+?\"\\s*?)(?=>)",
						"${tag}", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException /*ex*/ ) {}
				
				/*********************
				 * Обработка графики *
				 ********************/
				// удаление "пустого" тега <coverpage></coverpage>
				try {
					XmlDescription = Regex.Replace(
						XmlDescription, @"(?:<coverpage>\s*?</coverpage>)",
						"", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException /*ex*/ ) {}
				// обработка картинок: <image l:href="#img_0.png"> </image> или <image l:href="#img_0.png">\n</image>
				try {
					XmlDescription = Regex.Replace(
						XmlDescription, "(?'img'<image [^<]+?(?:\"[^\"]*\"|'[^']*')?)(?'more'>)(?:\\s*?</image>)",
						"${img} /${more}", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException /*ex*/ ) {}
				
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
				
				/************************
				 * Обработка annotation *
				 ************************/
				// обработка annotation без тегов <p>: текст annotation обрамляется тегами <p> ... </p>
				try {
					XmlDescription = Regex.Replace(
						XmlDescription, @"(?<=<annotation>)\s*?(?'text_annotation'(?:<(?'tag_s'strong|emphasis)>)?\s*?(?:[^<]+)?(?:</\k'tag_s'>)?\s*?)\s*?(?=</annotation>)",
						"<p>${text_annotation}</p>", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException /*ex*/ ) {}
				
				/******************
				 * Обработка Жанра *
				 ******************/
				if ( XmlDescription.IndexOf( "<genre" ) != -1 ) {
					GenreCorrector genreCorrector = new GenreCorrector( ref XmlDescription, false, false );
					XmlDescription = genreCorrector.correct();
				}
				
				/******************
				 * Обработка языка *
				 ******************/
				// замена <lang> для русских книг на ru для fb2 без <src-title-info>
				LangRuUkBeCorrector langRuUkBeCorrector = new LangRuUkBeCorrector( ref XmlDescription, XmlBody );
				bool IsCorrected = false;
				XmlDescription = langRuUkBeCorrector.correct( ref IsCorrected );
				
				// обработка неверно заданного русского языка
				try {
					XmlDescription = Regex.Replace(
						XmlDescription, @"(?<=<lang>)(?:\s*)(?:RU-ru|Rus)(?:\s*)(?=</lang>)",
						"ru", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException /*ex*/ ) {}
				// обработка неверно заданного английского языка
				try {
					XmlDescription = Regex.Replace(
						XmlDescription, @"(?<=<lang>)(?:\s*)(?:EN-en|Eng)(?:\s*)(?=</lang>)",
						"en", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException /*ex*/ ) {}
				
				/*****************
				 * Обработка дат *
				 *****************/
				if ( XmlDescription.IndexOf( "<date" ) != -1 ) {
					DateCorrector dateCorrector = new DateCorrector( ref XmlDescription, false, false );
					XmlDescription = dateCorrector.correct();
				}
				
				/************************
				 * Обработка annotation *
				 ************************/
				// преобразование <title> в аннотации на <subtitle> (Заголовок - без тегов <strong>) и т.п.
				try {
					XmlDescription = Regex.Replace(
						XmlDescription, @"(?<=<annotation>)\s*?(?:<title>\s*?)(?:<p>\s*?)(?'title'[^<]+?)(?:\s*?</p>\s*?</title>)",
						"<subtitle>${title}</subtitle>", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException /*ex*/ ) {}
				
				/**********************************
				 * Обработка history и annotation *
				 **********************************/
//				// вставка недостающего <p> : <history>Текст</p></history> => <history><p>Текст</p></history>
//				try {
//					XmlDescription = Regex.Replace(
//						XmlDescription, @"(?'tag'<(history|annotation)>)(?!<p>)",
//						"${tag}<p>", RegexOptions.IgnoreCase | RegexOptions.Multiline
//					);
//				} catch ( RegexMatchTimeoutException /*ex*/ ) {}
//				// вставка недостающего </p> : <history><p>Текст</history> => <history><p>Текст</p></history>
//				try {
//					XmlDescription = Regex.Replace(
//						XmlDescription, @"(?<!</p>)(?'tag'</(history|annotation)>)",
//						"</p>${tag}", RegexOptions.IgnoreCase | RegexOptions.Multiline
//					);
//				} catch ( RegexMatchTimeoutException /*ex*/ ) {}
				
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
				if ( InputString.IndexOf( "<a " ) != -1 ) {
					LinksCorrector linksCorrector = new LinksCorrector( InputString );
					InputString = linksCorrector.correct();
				}
				
				/****************************
				 * Обработка <empty-line /> *
				 ***************************/
				if ( InputString.IndexOf( "<empty-line/>" ) != -1 || InputString.IndexOf( "<empty-line />" ) != -1) {
					EmptyLineCorrector emCorrector = new EmptyLineCorrector( InputString );
					InputString = emCorrector.correct();
				}
				
				/********************************************************************
				 * удаление пустых атрибутов xmlns="" в тегах body, title и section *
				 ********************************************************************/
				// удаление пустого атрибута xmlns="" в тегах body и section
				try {
					InputString = Regex.Replace(
						InputString, "(?<=<)(?'tag'body|section|title)(?:\\s+?xmlns=\"\"\\s*?)(?=>)",
						"${tag}", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException /*ex*/ ) {}
				// удаление ненужных атрибутов в теге <body> в ситуации: xmlns:fb="http://www.gribuser.ru/xml/fictionbook/2.0" xmlns:xlink="http://www.w3.org/1999/xlink">
				try {
					InputString = Regex.Replace(
						InputString, "(?<=<body) \\b(xmlns|l)\\b:fb=\"http://www.gribuser.ru/xml/fictionbook/2.0\" \\b(xmlns|l)\\b:xlink=\"http://www.w3.org/1999/xlink\"(?=>)",
						"", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException /*ex*/ ) {}
				
				/*********************
				 * Обработка графики *
				 ********************/
				if ( InputString.IndexOf( "<image" ) != -1 ) {
					ImageCorrector imageCorrector = new ImageCorrector( ref InputString, false, false );
					InputString = imageCorrector.correct();
				}
				
				/**************************
				 * Обработка цитат <cite> *
				 **************************/
				if ( InputString.IndexOf( "<cite" ) != -1 ) {
					CiteCorrector citeCorrector = new CiteCorrector( ref InputString, false, false );
					InputString = citeCorrector.correct();
				}
				// Создание цитаты для текста автора, идущего после тега </p> или <empty-line />: </p><text-author>Автор</text-author><p>Текст</p> => </p><cite><text-author>Автор</text-author></cite><p>Текст</p>
				InputString = Regex.Replace(
					InputString, @"(?'left'(?:<empty-line ?/>|</p>|<section>|</title>))\s*?(?'text_a'(?:<text-author>\s*?(?:<(?'tag'strong|emphasis)\b>)?[^<]+?(?:</\k'tag'>)?\s*?</text-author>\s*?){1,})\s*?(?'right'(?:<p>|</section>|<empty-line ?/>))",
					"${left}<cite>${text_a}</cite>${right}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				
				/**************************************
				 * Обработка подзаголовков <subtitle> *
				 **************************************/
				// обработка подзаголовков <subtitle> (<subtitle>\n<p>\nТекст\n</p>\n</subtitle>)
				try {
					InputString = Regex.Replace(
						InputString, @"(?'tag_start'<subtitle>)\s*<p>\s*(?'text'.+?)\s*</p>\s*(?'tag_end'</subtitle>)",
						"${tag_start}${text}${tag_end}", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException /*ex*/ ) {}
				// обработка подзаголовков <subtitle> </section><section><subtitle>Текст</subtitle><epigraph> => </section><section><title><p>Текст</p></title><epigraph>
				try {
					InputString = Regex.Replace(
						InputString, @"</section>\s*?<section>\s*?<subtitle>\s*?(?'sub'[^<]+?)\s*?</subtitle>\s*?<epigraph>",
						"</section><section><title><p>${sub}</p></title><epigraph>", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException /*ex*/ ) {}

				/**********************
				 * Обработка epigraph *
				 **********************/
				if ( InputString.IndexOf( "<epigraph" ) != -1 ) {
					EpigraphCorrector epigraphCorrector = new EpigraphCorrector ( ref InputString, true, false );
					InputString = epigraphCorrector.correct();
				}

				/*********************
				 * Обработка <title> *
				 ********************/
				if ( InputString.IndexOf( "<title" ) != -1 ) {
					TitleCorrector titleCorrector = new TitleCorrector ( ref InputString, true, false );
					InputString = titleCorrector.correct();
				}
				
				/**************************
				 * Обработка <annotation> *
				 *************************/
				// Обработка <annotation><i>text</i></annotation> => <annotation><p>text</p></annotation>
				try {
					InputString = Regex.Replace(
						InputString, @"<(?'tag'annotation|cite)\b>\s*?<(?'format'i|b|emphasis|strong)\b>\s*?(?'text'[^<]+?\s*)</\k'format'>\s*?</\k'tag'>",
						"<${tag}><p>${text}</p></${tag}>", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException /*ex*/ ) {}
				// Удаление <empty-line /> между <section> и <annotation>: <section><empty-line /><annotation> => <section><annotation>
				try {
					InputString = Regex.Replace(
						InputString, @"(?<=<section>)\s*(?:<empty-line */>\s*){1,}\s*(?=<annotation>)",
						"", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException /*ex*/ ) {}
				// Вставка <empty-line /> между </annotation> и </section>: </annotation></section> => </annotation><empty-line /></section>
				try {
					InputString = Regex.Replace(
						InputString, @"(?'ann'</annotation>)(\s*)(?'end_sect'</section>)",
						"${ann}<empty-line />${end_sect}", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException /*ex*/ ) {}
				
				/********************
				 * Обработка Стихов *
				 ********************/
				if ( InputString.IndexOf( "<poem" ) != -1 ) {
					StanzaCorrector stanzaCorrector = new StanzaCorrector( ref InputString, false, false );
					InputString = stanzaCorrector.correct();
					
					PoemCorrector poemCorrector = new PoemCorrector( ref InputString, false, false );
					InputString = poemCorrector.correct();
				}
				
				/****************************
				 * Обработка форматирования *
				 ***************************/
				// обработка вложенных друг в друга тегов strong или emphasis: <emphasis><emphasis><p>text</p></emphasis></emphasis> => <p><emphasis>text</emphasis></p>
				try {
					InputString = Regex.Replace(
						InputString, "(?:(?'format'<(?'tag'strong|emphasis)>)\\s*?){2}(?'p'(?:<p(?:(?:[^>\"']|\"[^\"]*\"|'[^']*')*)>))\\s*?(?'text'(?:[^<]+))?(?'_p'(?:</p>))\\s*?(?'_format'</\\k'tag'>)\\s*?\\k'_format'",
						"${p}${format}${text}${_format}${_p}", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException /*ex*/ ) {}
				// внесение тегов strong или emphasis в теги <p> </p>: <emphasis><p>text</p></emphasis> => <p><emphasis>text</emphasis></p>
				try {
					InputString = Regex.Replace(
						InputString, "(?'format'<(?'tag'strong|emphasis)>)\\s*?(?'p'(?:<p(?:(?:[^>\"']|\"[^\"]*\"|'[^']*')*)>))\\s*?(?'text'(?:[^<]+))?(?'_p'(?:</p>))\\s*?(?'_format'</\\k'tag'>)\\s*?",
						"${p}${format}${text}${_format}${_p}", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException /*ex*/ ) {}
				
				// замена тегов <strong> или <emphasis>, обрамляющих множественный текст на Цитату: <emphasis><p>Текст</p><p>Текст</p></emphasis> => <cite><p>Текст</p><p>Текст</p></cite>
				try {
					InputString = Regex.Replace(
						InputString, "(?:<(?'tag'strong|emphasis)>)\\s*?(?'text'(?:(?:<p(?:(?:[^>\"']|\"[^\"]*\"|'[^']*')*)>)\\s*?(?:[^<]+)?(?:</p>)\\s*?){2,})(?:</\\k'tag'>)",
						"<cite>${text}</cite>", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException /*ex*/ ) {}

				/********************
				 * Обработка Таблиц *
				 *******************/
				if ( InputString.IndexOf( "<table" ) != -1 ) {
					TableCorrector tableCorrector = new TableCorrector ( ref InputString, false, false );
					InputString = tableCorrector.correct();
				}
					
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
