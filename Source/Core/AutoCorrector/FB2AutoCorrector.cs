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
using Core.Common;

namespace Core.AutoCorrector
{
	
	/// <summary>
	/// Автокорректор fb2 файла в текстовом режиме с помощью регулярных выражений
	/// </summary>
	public class FB2AutoCorrector
	{
		private const string _MessageTitle = "Автокорректор";
		
		
		public FB2AutoCorrector()
		{
			// Удаляем Log файл для новой отладки, если она включена в Настройках
			if ( File.Exists( Debug.LogFilePath ) )
				File.Delete( Debug.LogFilePath );
		}
		
		/// <summary>
		/// Автокорректировка теста файла FilePath
		/// </summary>
		/// <param name="FilePath">Путь к обрабатываемой книге</param>
		public static void autoCorrector( string FilePath ) {
			if ( string.IsNullOrWhiteSpace( FilePath ) )
				return;
			FileInfo fi = new FileInfo( FilePath );
			if ( !fi.Exists )
				return;
			if ( fi.Length < 4 )
				return;
			
			// Хэш таблица fb2 тегов
			Hashtable htTags = FB2CleanCode.getTagsHashtable(); // обработка < > в тексте, кроме fb2 тегов
			
			// обработка головного тега FictionBook и пространства имен
			FB2Text fb2Text = null;
			try {
				fb2Text = new FB2Text( FilePath );
			} catch ( Exception ex ) {
				Debug.DebugMessage(
					FilePath, ex, "AutoCorrector.FB2AutoCorrector.autoCorrector():\r\nАвтокорректировка теста файла."
				);
				// Если структура fb2 файла сильно "битая", или же основные разделы располагаются не по стандарту
				throw ex;
			}
			
			if ( fb2Text.Description.IndexOf( "<FictionBook", StringComparison.CurrentCulture ) == -1 ) {
				// тег FictionBook отсутствует в книге
				FictionBookTagCorrector fbtc = new FictionBookTagCorrector( FilePath );
				fb2Text.Description = fb2Text.Description.Insert(
					fb2Text.Description.IndexOf( "<description>", StringComparison.CurrentCulture ),
					fbtc.NewFictionBookTag
				);
			}

			// обработка неверного значения кодировки файла
			try {
				Regex regex = new Regex( "(?<=encoding=\")(?:(?:wutf-8)|(?:utf8))(?=\")", RegexOptions.IgnoreCase );
				fb2Text.Description = regex.Replace( fb2Text.Description, "utf-8" );
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					FilePath, ex, "AutoCorrector.FB2AutoCorrector::autoCorrector():\r\nОбработка неверного значения кодировки файла."
				);
			}
			
			
			/******************************************
			 * Автокорректировка раздела <description> *
			 *******************************************/
			fb2Text.Description = autoCorrectDescription( FilePath, fb2Text.Bodies, fb2Text.Description, htTags );
			
			/* Автокорректировка разделов <body> */
			fb2Text.Bodies = autoCorrect( FilePath, fb2Text.Bodies, htTags );
			
			/* автокорректировка разделов binary */
			if ( fb2Text.BinariesExists ) {
				// обработка ссылок-названий картинок в binary
				BinaryCorrector binaryCorrector = new BinaryCorrector( FilePath, fb2Text.Binaries );
				fb2Text.Binaries = binaryCorrector.correct();
			}
			
			try {
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.LoadXml( fb2Text.toXML() );
				xmlDoc.Save( FilePath );
			} catch ( Exception ex ) {
				Debug.DebugMessage(
					FilePath, ex, "AutoCorrector.FB2AutoCorrector::autoCorrector():\r\nпересохранение файла."
				);
				fb2Text.saveFile();
			}
		}
		
		/// <summary>
		/// Корректировка описания книги (description)
		/// </summary>
		/// <param name="XmlBody">xml текст body (для определения языка книги)</param>
		/// <param name="XmlDescription">Строка description для корректировки</param>
		/// <param name="htTags">Хэш таблица fb2 тегов</param>
		/// <returns>Откорректированная строка типа string</returns>
		private static string autoCorrectDescription( string FilePath, string XmlBody, string XmlDescription, Hashtable htTags ) {
			if ( string.IsNullOrWhiteSpace( XmlDescription ) || XmlDescription.Length == 0 )
				return XmlDescription;
			
			/* обработка головного тега FictionBook и пространства имен */
			FictionBookTagCorrector fbtc = new FictionBookTagCorrector( FilePath );
			XmlDescription = fbtc.StartTagCorrect( XmlDescription );
			
			try {
				/****************************************
				 * удаление атрибута в теге description *
				 ****************************************/
				// удаление атрибута в теге description
				try {
					Regex regex  = new Regex( "<description>", RegexOptions.IgnoreCase );
					Match m = regex.Match( XmlDescription );
					if ( !m.Success ) {
						XmlDescription = Regex.Replace(
							XmlDescription, "(?:<description [^<]+?(?:\"[^\"]*\"|'[^']*')?)(?'more'>)",
							"<description>", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline
						);
					}
				} catch ( RegexMatchTimeoutException ex ) {
					Debug.DebugMessage(
						FilePath, ex, "Обработка раздела <description>:\r\nУдаление атрибута в теге description. Исключение RegexMatchTimeoutException."
					);
				}
				catch ( Exception ex ) {
					Debug.DebugMessage(
						FilePath, ex, "Обработка раздела <description>:\r\nУдаление атрибута в теге description. Исключение Exception."
					);
				}
				
				/*********************
				 * Обработка графики *
				 *********************/
				// удаление "пустого" тега <coverpage></coverpage>
				try {
					XmlDescription = Regex.Replace(
						XmlDescription, @"(?:<coverpage>\s*?</coverpage>)",
						"", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException ex ) {
					Debug.DebugMessage(
						FilePath, ex, "Обработка раздела <description>:\r\nУдаление \"пустого\" тега <coverpage></coverpage>. Исключение RegexMatchTimeoutException."
					);
				}
				catch ( Exception ex ) {
					Debug.DebugMessage(
						FilePath, ex, "Обработка раздела <description>:\r\nУдаление \"пустого\" тега <coverpage></coverpage>. Исключение Exception."
					);
				}
				
				// обработка картинок: <image l:href="#img_0.png"> </image> или <image l:href="#img_0.png">\n</image>
				try {
					XmlDescription = Regex.Replace(
						XmlDescription, "(?'img'<image [^<]+?(?:\"[^\"]*\"|'[^']*')?)(?'more'>)(?:\\s*?</image>)",
						"${img} /${more}", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException ex ) {
					Debug.DebugMessage(
						FilePath, ex, "Обработка раздела <description>:\r\nОбработка раздела <description>:\r\nОбработка картинок: <image l:href=\"img_0.png\"> </image> или <image l:href=\"img_0.png\"></image>. Исключение RegexMatchTimeoutException."
					);
				}
				catch ( Exception ex ) {
					Debug.DebugMessage(
						FilePath, ex, "Обработка раздела <description>:\r\nОбработка картинок: <image l:href=\"img_0.png\"> </image> или <image l:href=\"img_0.png\"></image>. Исключение м."
					);
				}
				
				/****************
				 * Обработка id *
				 ****************/
				// обработка пустого id
				try {
					XmlDescription = Regex.Replace(
						XmlDescription, @"(?<=<id>)(?:\s*\s*)(?=</id>)",
						Guid.NewGuid().ToString().ToUpper(), RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException ex ) {
					Debug.DebugMessage(
						FilePath, ex, "Обработка раздела <description>:\r\nОбработка пустого id. Исключение RegexMatchTimeoutException."
					);
				}
				catch ( Exception ex ) {
					Debug.DebugMessage(
						FilePath, ex, "Обработка раздела <description>:\r\nОбработка пустого id. Исключение Exception."
					);
				}

				// обработка Либрусековских id
				try{
					XmlDescription = Regex.Replace(
						XmlDescription, @"(?<=<id>)\s*(Mon|Tue|Wed|Thu|Fri|Sat|Sun)\s+(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)\s+\d{2}\s+(\d{2}\:){2}\d{2}\s+\d{4}\s*(?=</id>)",
						Guid.NewGuid().ToString().ToUpper(), RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException ex ) {
					Debug.DebugMessage(
						FilePath, ex, "Обработка раздела <description>:\r\nОбработка Либрусековских id. Исключение RegexMatchTimeoutException."
					);
				}
				catch ( Exception ex ) {
					Debug.DebugMessage(
						FilePath, ex, "Обработка раздела <description>:\r\nОбработка Либрусековских id. Исключение Exception."
					);
				}
				
				/************************
				 * Обработка annotation *
				 ************************/
				// обработка annotation без тегов <p>: текст annotation обрамляется тегами <p> ... </p>
				try {
					XmlDescription = Regex.Replace(
						XmlDescription, @"(?<=<annotation>)\s*?(?'text_annotation'(?:<(?'tag_s'strong|emphasis)>)?\s*?(?:[^<]+)?(?:</\k'tag_s'>)?\s*?)\s*?(?=</annotation>)",
						"<p>${text_annotation}</p>", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException ex ) {
					Debug.DebugMessage(
						FilePath, ex, "Обработка раздела <description>:\r\nОбработка annotation без тегов <p>: текст annotation обрамляется тегами <p> ... </p>. Исключение RegexMatchTimeoutException."
					);
				}
				catch ( Exception ex ) {
					Debug.DebugMessage(
						FilePath, ex, "Обработка раздела <description>:\r\nОбработка annotation без тегов <p>: текст annotation обрамляется тегами <p> ... </p>. Исключение Exception."
					);
				}
				
				// обработка annotation: картинка без тегов <p>: <annotation><image l:href="#ficbook_logo.png" /> => <annotation><p><image l:href="#ficbook_logo.png" /></p>
				try {
					XmlDescription = Regex.Replace(
						XmlDescription, "(?<=<annotation>)\\s*?(?'img'<image [^<]+?(?:\"[^\"]*\"|'[^']*')?>)",
						"<p>${img}</p>", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException ex ) {
					Debug.DebugMessage(
						FilePath, ex, "Обработка annotation: картинка без тегов <p>: <annotation><image l:href=\"#ficbook_logo.png\" /> => <annotation><p><image l:href=\"#ficbook_logo.png\" /></p>. Исключение RegexMatchTimeoutException."
					);
				}
				catch ( Exception ex ) {
					Debug.DebugMessage(
						FilePath, ex, "Обработка annotation: картинка без тегов <p>: <annotation><image l:href=\"#ficbook_logo.png\" /> => <annotation><p><image l:href=\"#ficbook_logo.png\" /></p>. Исключение Exception."
					);
				}
				
				/******************
				 * Обработка Жанра *
				 ******************/
				if ( XmlDescription.IndexOf( "<genre", StringComparison.CurrentCulture ) != -1 ) {
					GenreCorrector genreCorrector = new GenreCorrector( FilePath, ref XmlDescription, false, false );
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
				} catch ( RegexMatchTimeoutException ex ) {
					Debug.DebugMessage(
						FilePath, ex, "Обработка раздела <description>:\r\nОбработка неверно заданного русского языка. Исключение RegexMatchTimeoutException."
					);
				}
				catch ( Exception ex ) {
					Debug.DebugMessage(
						FilePath, ex, "Обработка раздела <description>:\r\nОбработка неверно заданного русского языка. Исключение Exception."
					);
				}
				
				// обработка неверно заданного английского языка
				try {
					XmlDescription = Regex.Replace(
						XmlDescription, @"(?<=<lang>)(?:\s*)(?:EN-en|Eng)(?:\s*)(?=</lang>)",
						"en", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException ex ) {
					Debug.DebugMessage(
						FilePath, ex, "Обработка раздела <description>:\r\nОбработка неверно заданного английского языка. Исключение RegexMatchTimeoutException."
					);
				}
				catch ( Exception ex ) {
					Debug.DebugMessage(
						FilePath, ex, "Обработка раздела <description>:\r\nОбработка неверно заданного английского языка. Исключение Exception."
					);
				}
				
				/*****************
				 * Обработка дат *
				 *****************/
				try {
					if ( XmlDescription.IndexOf( "<date", StringComparison.CurrentCulture ) != -1 ) {
						DateCorrector dateCorrector = new DateCorrector( FilePath, ref XmlDescription, false, false );
						XmlDescription = dateCorrector.correct();
					}
				} catch ( RegexMatchTimeoutException ex ) {
					Debug.DebugMessage(
						FilePath, ex, "Обработка раздела <description>:\r\nОбработка дат (тег <date>). Исключение RegexMatchTimeoutException."
					);
				}
				catch ( Exception ex ) {
					Debug.DebugMessage(
						FilePath, ex, "Обработка раздела <description>:\r\nОбработка дат (тег <date>). Исключение Exception."
					);
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
				} catch ( RegexMatchTimeoutException ex ) {
					Debug.DebugMessage(
						FilePath, ex, "Обработка раздела <description>:\r\nПреобразование <title> в аннотации на <subtitle> (Заголовок - без тегов <strong>) и т.п. Исключение RegexMatchTimeoutException."
					);
				}
				catch ( Exception ex ) {
					Debug.DebugMessage(
						FilePath, ex, "Обработка раздела <description>:\r\nПреобразование <title> в аннотации на <subtitle> (Заголовок - без тегов <strong>) и т.п. Исключение Exception."
					);
				}
				
				// преобразование <annotation><annotation><p>Текст.</p><p>Еще текст.</p></annotation></annotation> => <annotation><p>Текст.</p><p>Еще текст.</p></annotation>
				try {
					XmlDescription = Regex.Replace(
						XmlDescription, @"(?:<annotation>\s*?<annotation>\s*?)(?'texts'(<p>\s*?(?:<(?'tag'strong|emphasis)\b>)?[^<]+?(?:</\k'tag'>)?\s*?</p>\s*?){1,}\s*?)(?:</annotation>\s*?</annotation>)",
						"<annotation>${texts}</annotation>", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException ex ) {
					Debug.DebugMessage(
						FilePath, ex, "Обработка раздела <description>:\r\nПреобразование <annotation><annotation><p>Текст.</p><p>Еще текст.</p></annotation></annotation> => <annotation><p>Текст.</p><p>Еще текст.</p></annotation>. Исключение RegexMatchTimeoutException."
					);
				}
				catch ( Exception ex ) {
					Debug.DebugMessage(
						FilePath, ex, "Обработка раздела <description>:\r\nПреобразование <annotation><annotation><p>Текст.</p><p>Еще текст.</p></annotation></annotation> => <annotation><p>Текст.</p><p>Еще текст.</p></annotation>. Исключение Exception."
					);
				}

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
				
			} catch ( Exception ex ) {
				Debug.DebugMessage(
					FilePath, ex, "Обработка раздела <description>:\r\nМетод autoCorrectDescription().\r\nОшибка уровня всего метода (главный catch ( Exception ex )):"
				);
			}
			return autoCorrect( FilePath, XmlDescription, htTags );
		}
		
		/// <summary>
		/// Автокорректировка текста строки InputString
		/// </summary>
		/// <param name="FilePath">Путь к обрабатываемому файлу</param>
		/// <param name="InputString">Строка для корректировки</param>
		/// <param name="htTags">Хэш таблица fb2 тегов</param>
		/// <returns>Откорректированную строку типа string</returns>
		private static string autoCorrect( string FilePath, string InputString, Hashtable htTags ) {
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
					FilePath, FB2CleanCode.processingCharactersMoreAndLessAndAmp( InputString, htTags )
				)
			);
		}
		
		/// <summary>
		/// Автокорректировка текста строки InputString
		/// </summary>
		/// <param name="FilePath">Путь к обрабатываемому файлу</param>
		/// <param name="InputString">Строка для корректировки</param>
		/// <returns>Откорректированную строку типа string</returns>
		private static string _autoCorrect( string FilePath, string InputString ) {
			if ( string.IsNullOrWhiteSpace( InputString ) || InputString.Length == 0 )
				return InputString;
			
			try {
				// Удаление пустышек типа <body name="notes"><title><p>Примечания</p></title></body>
				try {
					InputString = Regex.Replace(
						InputString, "(?:<body +?name=\"notes\">\\s*<title>\\s*(?:(?:<p>(?:(?:\\w+?\\W?\\w+?)+)</p>){1,}\\s*){1,}</title>\\s*</body>)",
						"", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nУдаление пустышек типа <body name=\"notes\"><title><p>Примечания</p></title></body>. Исключение RegexMatchTimeoutException."
					);
				}
				catch ( Exception ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nУдаление пустышек типа <body name=\"notes\"><title><p>Примечания</p></title></body>. Исключение RegexMatchTimeoutException."
					);
				}
				
				// Удаление пустышек типа <body name="notes" />
				try {
					InputString = Regex.Replace(
						InputString, "<body name=\"notes\" ?/>",
						"", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nУдаление пустышек типа <body name=\"notes\" />. Исключение RegexMatchTimeoutException."
					);
				}
				catch ( Exception ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nУдаление пустышек типа <body name=\"notes\" />. Исключение Exception."
					);
				}
				
				// Обработка блоков типа <p></section> => </section>
				try {
					InputString = Regex.Replace(
						InputString, "(?:<p>\\s*?)(?'tag'</section>)",
						"${tag}", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nОбработка блоков типа <p></section> => </section>. Исключение RegexMatchTimeoutException."
					);
				}
				catch ( Exception ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nОбработка блоков типа <p></section> => </section>. Исключение Exception."
					);
				}
				
				// Обработка блоков типа <title></p> => <title>
				try {
					InputString = Regex.Replace(
						InputString, "(?'tag'<title>\\s*?)(?:</p>)",
						"${tag}", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nОбработка блоков типа <title></p> => <title>. Исключение RegexMatchTimeoutException."
					);
				}
				catch ( Exception ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nОбработка блоков типа <title></p> => <title>. Исключение Exception."
					);
				}
				
				// Обработка блоков типа </title><empty-line /></section><p> => </title><p> или </title><empty-line /></section><subtitle> => </title><subtitle>
				try {
					InputString = Regex.Replace(
						InputString, "(?'title'</title>)(?:\\s*?<empty-line />\\s*?</section>\\s*?)(?'_p'(?:<p>)|(?:<subtitle>))",
						"${title}${_p}", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nОбработка блоков типа </title><empty-line /></section><p> => </title><p> или </title><empty-line /></section><subtitle> => </title><subtitle>. Исключение RegexMatchTimeoutException."
					);
				}
				catch ( Exception ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nОбработка блоков типа </title><empty-line /></section><p> => </title><p> или </title><empty-line /></section><subtitle> => </title><subtitle>. Исключение Exception."
					);
				}
				
				/********************
				 * Обработка ссылок *
				 *******************/
				// обработка ссылок
				if ( InputString.IndexOf( "<a ", StringComparison.CurrentCulture ) != -1 ) {
					LinksCorrector linksCorrector = new LinksCorrector( FilePath, InputString );
					InputString = linksCorrector.correct();
				}
				
				/****************************
				 * Обработка <empty-line /> *
				 ***************************/
				if ( InputString.IndexOf( "<empty-line/>", StringComparison.CurrentCulture ) != -1 || InputString.IndexOf( "<empty-line />", StringComparison.CurrentCulture ) != -1) {
					EmptyLineCorrector emCorrector = new EmptyLineCorrector( FilePath, InputString );
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
				} catch ( RegexMatchTimeoutException ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nУдаление пустого атрибута xmlns=\"\" в тегах body и section. Исключение RegexMatchTimeoutException."
					);
				}
				catch ( Exception ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nУдаление пустого атрибута xmlns=\"\" в тегах body и section. Исключение Exception."
					);
				}
				
				// удаление ненужных атрибутов в теге <body> в ситуации: xmlns:fb="http://www.gribuser.ru/xml/fictionbook/2.0" xmlns:xlink="http://www.w3.org/1999/xlink">
				try {
					InputString = Regex.Replace(
						InputString, "(?<=<body) \\b(xmlns|l)\\b:fb=\"http://www.gribuser.ru/xml/fictionbook/2.0\" \\b(xmlns|l)\\b:xlink=\"http://www.w3.org/1999/xlink\"(?=>)",
						"", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nУдаление ненужных атрибутов в теге <body> в ситуации: xmlns:fb=\"http://www.gribuser.ru/xml/fictionbook/2.0\" xmlns:xlink=\"http://www.w3.org/1999/xlink\". Исключение RegexMatchTimeoutException."
					);
				}
				catch ( Exception ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nУдаление ненужных атрибутов в теге <body> в ситуации: xmlns:fb=\"http://www.gribuser.ru/xml/fictionbook/2.0\" xmlns:xlink=\"http://www.w3.org/1999/xlink\". Исключение Exception."
					);
				}
				
				/***********************
				 * Обработка <section> *
				 ***********************/
				// Удаление "пустышек": <section><empty-line /><empty-line /></section>
				try {
					InputString = Regex.Replace(
						InputString, "(<section>)\\s*?(<empty-line />\\s*?){1,}\\s*?(</section>)",
						"", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nОбработка <section>.\r\nУдаление \"пустышек\": <section><empty-line /><empty-line /></section>. Исключение RegexMatchTimeoutException."
					);
				}
				catch ( Exception ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nОбработка <section>.\r\nУдаление \"пустышек\": <section><empty-line /><empty-line /></section>. Исключение Exception."
					);
				}
				
				/****************************
				 * Обработка болоков сносок *
				 ****************************/
				// Обработка блоков сносок типа <section id="id20150519063123_1"><title><p>1</p></title></section> =>
				// <section id="id20150519063123_1"><title><p>1</p></title><empty-line /></section>
				try {
					InputString = Regex.Replace(
						InputString, "(?'start'<section id=\"(:?(:?\\w+?\\W?\\w+?)+?)+\">\\s*?<title>\\s*?<p>\\d{1,5}</p>\\s*?</title>\\s*?)(?'end'</section>)",
						"${start}<empty-line />${end}", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nОбработка блоков сносок типа <section id=\"id20150519063123_1'\"><title><p>1</p></title></section> => <section id=\"id20150519063123_1\"><title><p>1</p></title><empty-line /></section>. Исключение RegexMatchTimeoutException."
					);
				}
				catch ( Exception ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nОбработка блоков сносок типа <section id=\"id20150519063123_1'\"><title><p>1</p></title></section> => <section id=\"id20150519063123_1\"><title><p>1</p></title><empty-line /></section>. Исключение Exception."
					);
				}

				/*********************
				 * Обработка графики *
				 ********************/
				if ( InputString.IndexOf( "<image", StringComparison.CurrentCulture ) != -1 ) {
					ImageCorrector imageCorrector = new ImageCorrector( FilePath, ref InputString, false, false );
					InputString = imageCorrector.correct();
				}
				
				/**************************
				 * Обработка цитат <cite> *
				 **************************/
				if ( InputString.IndexOf( "<cite", StringComparison.CurrentCulture ) != -1 ) {
					CiteCorrector citeCorrector = new CiteCorrector( FilePath, ref InputString, false, false );
					InputString = citeCorrector.correct();
				}
				
				// Создание цитаты для текста автора, идущего после тега </p> или <empty-line />: </p><text-author>Автор</text-author><p>Текст</p> => </p><cite><text-author>Автор</text-author></cite><p>Текст</p>
				try {
					InputString = Regex.Replace(
						InputString, @"(?'left'(?:<empty-line ?/>|</p>|<section>|</title>))\s*?(?'text_a'(?:<text-author>\s*?(?:<(?'tag'strong|emphasis)\b>)?[^<]+?(?:</\k'tag'>)?\s*?</text-author>\s*?){1,})\s*?(?'right'(?:<p>|</section>|<empty-line ?/>))",
						"${left}<cite>${text_a}</cite>${right}", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nСоздание цитаты для текста автора, идущего после тега </p> или <empty-line />: </p><text-author>Автор</text-author><p>Текст</p> => </p><cite><text-author>Автор</text-author></cite><p>Текст</p>. Исключение RegexMatchTimeoutException."
					);
				}
				catch ( Exception ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nСоздание цитаты для текста автора, идущего после тега </p> или <empty-line />: </p><text-author>Автор</text-author><p>Текст</p> => </p><cite><text-author>Автор</text-author></cite><p>Текст</p>. Исключение Exception."
					);
				}
				
				/**************************************
				 * Обработка подзаголовков <subtitle> *
				 **************************************/
				// обработка подзаголовков <subtitle> (<subtitle>\n<p>\nТекст\n</p>\n</subtitle>)
				try {
					InputString = Regex.Replace(
						InputString, @"(?'tag_start'<subtitle>)\s*<p>\s*(?'text'.+?)\s*</p>\s*(?'tag_end'</subtitle>)",
						"${tag_start}${text}${tag_end}", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nОбработка подзаголовков <subtitle>. Исключение RegexMatchTimeoutException."
					);
				}
				catch ( Exception ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nОбработка подзаголовков <subtitle>. Исключение Exception."
					);
				}
				
				// обработка подзаголовков <subtitle> </section><section><subtitle>Текст</subtitle><epigraph> => </section><section><title><p>Текст</p></title><epigraph>
				try {
					InputString = Regex.Replace(
						InputString, @"</section>\s*?<section>\s*?<subtitle>\s*?(?'sub'[^<]+?)\s*?</subtitle>\s*?<epigraph>",
						"</section><section><title><p>${sub}</p></title><epigraph>", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nОбработка подзаголовков <subtitle> </section><section><subtitle>Текст</subtitle><epigraph> => </section><section><title><p>Текст</p></title><epigraph>. Исключение RegexMatchTimeoutException."
					);
				}
				catch ( Exception ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nОбработка подзаголовков <subtitle> </section><section><subtitle>Текст</subtitle><epigraph> => </section><section><title><p>Текст</p></title><epigraph>. Исключение Exception."
					);
				}

				/************************
				 * Обработка <epigraph> *
				 ************************/
				if ( InputString.IndexOf( "<epigraph", StringComparison.CurrentCulture ) != -1 ) {
					EpigraphCorrector epigraphCorrector = new EpigraphCorrector ( FilePath, ref InputString, true, false );
					InputString = epigraphCorrector.correct();
				}

				/*********************
				 * Обработка <title> *
				 ********************/
				if ( InputString.IndexOf( "<title", StringComparison.CurrentCulture ) != -1 ) {
					TitleCorrector titleCorrector = new TitleCorrector ( FilePath, ref InputString, true, false );
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
				} catch ( RegexMatchTimeoutException ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nОбработка <annotation><i>text</i></annotation> => <annotation><p>text</p></annotation>. Исключение RegexMatchTimeoutException."
					);
				}
				catch ( Exception ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nОбработка <annotation><i>text</i></annotation> => <annotation><p>text</p></annotation>. Исключение Exception."
					);
				}
				
				// Удаление <empty-line /> между <section> и <annotation>: <section><empty-line /><annotation> => <section><annotation>
				try {
					InputString = Regex.Replace(
						InputString, @"(?<=<section>)\s*(?:<empty-line */>\s*){1,}\s*(?=<annotation>)",
						"", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nУдаление <empty-line /> между <section> и <annotation>: <section><empty-line /><annotation> => <section><annotation>. Исключение RegexMatchTimeoutException."
					);
				}
				catch ( Exception ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nУдаление <empty-line /> между <section> и <annotation>: <section><empty-line /><annotation> => <section><annotation>. Исключение Exception."
					);
				}
				
				// Вставка <empty-line /> между </annotation> и </section>: </annotation></section> => </annotation><empty-line /></section>
				try {
					InputString = Regex.Replace(
						InputString, @"(?'ann'</annotation>)(\s*)(?'end_sect'</section>)",
						"${ann}<empty-line />${end_sect}", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nВставка <empty-line /> между </annotation> и </section>: </annotation></section> => </annotation><empty-line /></section>.  Исключение RegexMatchTimeoutException."
					);
				}
				catch ( Exception ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nВставка <empty-line /> между </annotation> и </section>: </annotation></section> => </annotation><empty-line /></section>.  Исключение Exception."
					);
				}
				
				/********************
				 * Обработка Стихов *
				 ********************/
				if ( InputString.IndexOf( "<poem", StringComparison.CurrentCulture ) != -1 ) {
					StanzaCorrector stanzaCorrector = new StanzaCorrector( FilePath, ref InputString, false, false );
					InputString = stanzaCorrector.correct();

					PoemCorrector poemCorrector = new PoemCorrector( FilePath, ref InputString, false, false );
					InputString = poemCorrector.correct();
				}
				
				/****************************
				 * Обработка форматирования *
				 ***************************/
				// Обработка вложенных друг в друга тегов strong или emphasis: <emphasis><emphasis><p>text</p></emphasis></emphasis> => <p><emphasis>text</emphasis></p>
				try {
					InputString = Regex.Replace(
						InputString, "(?:(?'format'<(?'tag'strong|emphasis)>)\\s*?){2}(?'p'(?:<p(?:(?:[^>\"']|\"[^\"]*\"|'[^']*')*)>))\\s*?(?'text'(?:[^<]+))?(?'_p'(?:</p>))\\s*?(?'_format'</\\k'tag'>)\\s*?\\k'_format'",
						"${p}${format}${text}${_format}${_p}", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nОбработка вложенных друг в друга тегов strong или emphasis: <emphasis><emphasis><p>text</p></emphasis></emphasis> => <p><emphasis>text</emphasis></p>. Исключение RegexMatchTimeoutException."
					);
				}
				catch ( Exception ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nОбработка вложенных друг в друга тегов strong или emphasis: <emphasis><emphasis><p>text</p></emphasis></emphasis> => <p><emphasis>text</emphasis></p>. Исключение Exception."
					);
				}
				
				// внесение тегов strong или emphasis в теги <p> </p>: <emphasis><p>text</p></emphasis> => <p><emphasis>text</emphasis></p>
				try {
					InputString = Regex.Replace(
						InputString, "(?'format'<(?'tag'strong|emphasis)>)\\s*?(?'p'(?:<p(?:(?:[^>\"']|\"[^\"]*\"|'[^']*')*)>))\\s*?(?'text'(?:[^<]+))?(?'_p'(?:</p>))\\s*?(?'_format'</\\k'tag'>)\\s*?",
						"${p}${format}${text}${_format}${_p}", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nВнесение тегов strong или emphasis в теги <p> </p>: <emphasis><p>text</p></emphasis> => <p><emphasis>text</emphasis></p>. Исключение RegexMatchTimeoutException."
					);
				}
				catch ( Exception ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nВнесение тегов strong или emphasis в теги <p> </p>: <emphasis><p>text</p></emphasis> => <p><emphasis>text</emphasis></p>. Исключение Exception."
					);
				}
				
				// замена тегов <strong> или <emphasis>, обрамляющих множественный текст на Цитату: <emphasis><p>Текст</p><p>Текст</p></emphasis> => <cite><p>Текст</p><p>Текст</p></cite>
				try {
					InputString = Regex.Replace(
						InputString, "(?:<(?'tag'strong|emphasis)>)\\s*?(?'text'(?:(?:<p(?:(?:[^>\"']|\"[^\"]*\"|'[^']*')*)>)\\s*?(?:[^<]+)?(?:</p>)\\s*?){2,})(?:</\\k'tag'>)",
						"<cite>${text}</cite>", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nЗамена тегов <strong> или <emphasis>, обрамляющих множественный текст на Цитату: <emphasis><p>Текст</p><p>Текст</p></emphasis> => <cite><p>Текст</p><p>Текст</p></cite>. Исключение RegexMatchTimeoutException."
					);
				}
				catch ( Exception ex ) {
					Debug.DebugMessage(
						FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nЗамена тегов <strong> или <emphasis>, обрамляющих множественный текст на Цитату: <emphasis><p>Текст</p><p>Текст</p></emphasis> => <cite><p>Текст</p><p>Текст</p></cite>. Исключение Exception."
					);
				}

				/********************
				 * Обработка Таблиц *
				 *******************/
				if ( InputString.IndexOf( "<table", StringComparison.CurrentCulture ) != -1 ) {
					TableCorrector tableCorrector = new TableCorrector ( FilePath, ref InputString, false, false );
					InputString = tableCorrector.correct();
				}
				
				/**********************
				 * Обработка тега <p> *
				 *********************/
				ParaCorrector paraCorrector = new ParaCorrector( FilePath, ref InputString, false, false );
				InputString = paraCorrector.correct();

			} catch ( Exception ex ) {
				Debug.DebugMessage(
					FilePath, ex, "FB2AutoCorrector._autoCorrect()\r\nАвтокорректировка текста. Метод _autoCorrect( string InputString ). Ошибка уровня всего метода (главный catch (Exception ex))."
				);
			}
			
			return InputString;
		}

	}
}
