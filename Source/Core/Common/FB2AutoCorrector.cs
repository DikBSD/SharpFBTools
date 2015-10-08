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

namespace Core.Common
{
	/// <summary>
	/// Автокорректор fb2 файла в текстовом режиме с помощью регулярных выражений
	/// </summary>
	public class FB2AutoCorrector
	{
		#region Закрытые данные класса
		private static string[] _Tags = {
			"<p>", "<p ", "</p>", "<p/>", "<p />", "<empty-line/>", "<empty-line />", "<empty-line ",
			"<strong>", "</strong>", "<emphasis>", "</emphasis>", "<a ", "</a>",
			"<section>", "</section>", "<section ",
			"<title>", "</title>", "<title ", "<subtitle>", "<subtitle ", "</subtitle>",
			"<image>", "</image>", "<image ",
			"<poem>", "<poem ", "</poem>", "<stanza>", "<stanza ", "</stanza>", "<v>", "<v ", "</v>",
			"<cite>", "<cite ", "</cite>", "<epigraph>", "</epigraph>", "<epigraph ",
			"<strikethrough>", "</strikethrough>", "<sub>", "</sub>", "<sup>", "</sup>", "<code>", "</code>",
			"<table>", "<table ", "</table>", "<th>", "<th ", "</th>", "<td>", "<td ", "</td>",
			
			"<title-info>", "</title-info>", "<title-info ", "<genre>", "</genre>", "<genre ",
			"<author>", "</author>", "<author ", "<book-title>", "</book-title>", "<book-title ",
			"<annotation>", "</annotation>", "<annotation ", "<keywords>", "</keywords>", "<keywords ",
			"<date>", "</date>", "<date ", "<date/>", "<date />",
			"<coverpage>", "</coverpage>", "<coverpage ",
			"<lang>", "</lang>", "<lang ", "<src-lang>", "</src-lang>", "<src-lang ", "<translator>", "</translator>", "<translator ",
			"<sequence>", "</sequence>", "<sequence ", "<src-title-info>", "</src-title-info>", "<src-title-info ",
			"<document-info>", "</document-info>", "<document-info ", "<program-used>", "</program-used>", "<program-used ",
			"<src-url>", "</src-url>", "<src-url ", "<src-ocr>", "</src-ocr>", "<src-ocr ",
			"<id>", "</id>", "<id ", "<version>", "</version>", "<version ", "<binary ", "</binary>",
			"<publish-info>", "</publish-info>", "<publish-info ", "<book-name>", "</book-name>", "<book-name ",
			"<publisher>", "</publisher>", "<publisher ", "<description>", "</description>", "<description ",
			"<city>", "</city>", "<city ", "<year>", "</year>", "<year ", "<isbn>", "<isbn ", "</isbn>",
			"<history>", "</history>", "<history ", "<custom-info ", "</custom-info>",
			"<body>", "<body ", "</body>", "<text-author>", "<text-author ", "</text-author>",
			"<first-name>", "<first-name ", "</first-name>", "<first-name/>", "<first-name />",
			"<middle-name>", "<middle-name ", "<middle-name/>", "<middle-name />", "</middle-name>",
			"<last-name>", "<last-name ", "</last-name>", "<last-name/>", "<last-name />",
			"<nickname>", "<nickname ", "</nickname>", "<last-name/>", "<last-name />",
			"<home-page>", "<home-page ", "</home-page>", "<home-page/>", "<home-page />",
			"<email>", "<email ", "</email>", "<email/>", "<email />",
			"<FictionBook ", "</FictionBook>", "<stylesheet ", "</stylesheet>", "<style>", "</style>", "<style ",
			"<output>", "</output>", "<part>", "</part>", "<part ",
			"<output-document-class>", "</output-document-class>", "<output-document-class ", "<?xml "
		};
		#endregion
		
		public FB2AutoCorrector()
		{
		}
		
		// проверка, содержит ли тестируемый текст вначале fb2 тего
		private static bool isFB2Tag( string Text, Hashtable htTags ) {
			if ( string.IsNullOrWhiteSpace( Text ) )
				return false;

			int start = Text.IndexOf('<');
			int end = -1;
			if ( start == -1 )
				return false;
			else {
				end = Text.IndexOf(' ');
				if ( end == -1 )
					end = Text.IndexOf('>');
				if ( end == -1 )
					return false;
			}
			return htTags.ContainsValue(Text.Substring(start, end+1 - start));
		}
		// разбивка текста по токенам с границами <>
		private static List<string> splitString( string InputString ) {
			List<string> list = new List<string>();
			int start = 0;
			while (true) {
				// ищем открывающий символ <
				int indexLeft = InputString.IndexOf( '<', start );
				if ( indexLeft != -1 ) {
					_splitString( InputString.Substring( start + 1, indexLeft - start - ( indexLeft > start ? 1 : 0 ) ), ref list );
					// ищем закрывающий символ >
					int indexRight = InputString.IndexOf( '>', indexLeft );
					if ( indexRight != -1 ) {
						_splitString( InputString.Substring( indexLeft, indexRight - indexLeft + 1 ), ref list );
					} else {
						_splitString( InputString.Substring( indexLeft ), ref list );
						break;
					}
					start = indexRight;
				} else {
					list.Add( InputString.Substring(start) );
					break;
				}
			}
			return list;
		}
		// разбивка текста по токенам с границами <>
		private static void _splitString( string InputString, ref List<string> list ) {
			if ( InputString.Equals( "\n" ) ) {
				list.Add( InputString );
				return;
			}
			int start = 0;
			while (true) {
				// ищем открывающий символ <
				int indexLeft = InputString.IndexOf( '<', start );
				if ( indexLeft != -1 ) {
					string s = InputString.Substring( start + 1, indexLeft - start - ( indexLeft > start ? 1 : 0 ) );
					if ( !string.IsNullOrEmpty( s ) )
						list.Add( s );
					// ищем закрывающий символ >
					int indexRight = InputString.IndexOf( '>', indexLeft );
					if ( indexRight != -1 ) {
						string s1 = InputString.Substring( indexLeft, indexRight - indexLeft + 1 );
						if ( !string.IsNullOrEmpty( s ) )
							list.Add( s1 );
					} else {
						string s2 = InputString.Substring( indexLeft );
						if ( !string.IsNullOrEmpty( s ) )
							list.Add( s2 );
						break;
					}
					start = indexRight;
				} else {
					list.Add( InputString );
					break;
				}
			}
		}
		// Автокорректировка теста файла FilePath
		public static void autoCorrector( string FilePath ) {
			FileInfo fi = new FileInfo( FilePath );
			if ( !fi.Exists )
				return;
			else if ( fi.Length < 4 )
				return;
			
			// обработка < > в тексте, кроме fb2 тегов
			Hashtable htTags = new Hashtable(_Tags.Length);
			for ( int i = 0; i != _Tags.Length; ++i )
				htTags.Add(i, _Tags[i]);
			
			FB2Text fb2Text = new FB2Text( FilePath );
			fb2Text.Description = autoCorrect( fb2Text.Description, htTags );
			fb2Text.Bodies = autoCorrect( fb2Text.Bodies, htTags );
			
			try {
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.LoadXml(fb2Text.toXML());
				xmlDoc.Save(FilePath);
			} catch {
				fb2Text.saveFile();
			}
		}
		private static string autoCorrect( string InputString, Hashtable htTags ) {
			// обработка < и >
			List<string> list = splitString( InputString );
			StringBuilder sbNewString = new StringBuilder( list.Count );
			string token = string.Empty;
			foreach ( string item in list ) {
				token = item.Replace("\r\n", "");
				if ( string.IsNullOrWhiteSpace( token ) )
					token = token.Trim();
				if ( isFB2Tag( token, htTags ) )
					sbNewString.AppendLine( token );
				else {
					if ( token.IndexOf("\r\n", 0) == 0 || token.IndexOf("\n", 0) == 0
					    || string.IsNullOrEmpty( token ) || token.IndexOf("\r", 0) == 0 )
						continue;
					else if ( token.Trim().Equals( ">" ) )
						continue;
					else if ( token.IndexOf(">\n", 0) == -1 && token.IndexOf(">\r\n", 0) == -1 )
						sbNewString.AppendLine( token.Replace("<", "&lt;").Replace(">", "&gt;") );
					else
						sbNewString.AppendLine( "\n" );
				}
			}
			
			// автокорректировка файла
			return _autoCorrect( mergeTagWithText( sbNewString.ToString() ) );
		}
		// склейка тегов абзацев
		private static string mergeTagWithText( string InputString ) {
			//
			InputString = Regex.Replace(
				InputString, @"(>)\s+([^<]+?)\s+(<)",
				"$1$2$3", RegexOptions.None
			);
			//
			InputString = Regex.Replace(
				InputString, @"(<p>)\s+(<)",
				"$1$2", RegexOptions.None
			);
			//
			InputString = Regex.Replace(
				InputString, @"(>)\s+(</p>)",
				"$1$2", RegexOptions.None
			);
			
			//
			InputString = Regex.Replace(
				InputString, @"(<strong>)\s+(<emphasis>)",
				"$1$2", RegexOptions.None
			);
			//
			InputString = Regex.Replace(
				InputString, @"(<emphasis>)\s+(<strong>)",
				"$1$2", RegexOptions.None
			);
			//
			InputString = Regex.Replace(
				InputString, @"(</strong>)\s+(</emphasis>)",
				"$1$2", RegexOptions.None
			);
			//
			InputString = Regex.Replace(
				InputString, @"(</emphasis>)\s+(</strong>)",
				"$1$2", RegexOptions.None
			);
			
			return InputString;
		}
		// Автокорректировка текста строки InputString
		private static string _autoCorrect( string InputString ) {
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
				/*********************
				 * Обработка тега <p> *
				 *********************/
				// незавершенный тег <p>: <p текст
				InputString = Regex.Replace(
					InputString, @"(^\s*)(<p)(?=\s+[^i/>])",
					"$2>", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// восстановление пропущенных </p>
				InputString = Regex.Replace(
					InputString, "(\\:|;|\"|»\\d|\\w|\\.|,|!|\\?)\\s*(<p>)",
					"$1</p>\n$2", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// восстановление пропущенных <p>
				InputString = Regex.Replace(
					InputString, "(</p>)\\s*(\\d|\\w|\\.|,|!|\\?|\\:|;|\"|«)",
					"$1\n<p>$2", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// обработка строк типа <p> бла-бла-бла <p> бла-бла-бла </p> бла-бла-бла </p>
				InputString = Regex.Replace(
					InputString, @"(<p>)(.+)(<p>)(.+)(</p>)(.+)(</p>)\s",
					"$1$2$5\n$3$4$6$7", RegexOptions.None
				);
				
				
				/******************
				 * Обработка Жанра *
				 ******************/
				// обработка жанра
				InputString = Regex.Replace(
					InputString, @"(<genre>)(\s*)(proce|literature19)(\s*)(</genre>)",
					"$1prose$2", RegexOptions.IgnoreCase
				);
				
				/******************
				 * Обработка языка *
				 ******************/
				// замена <lang> для русских книг на ru для fb2 без <src-title-info>
				if ( InputString.IndexOf( "<src-title-info>" ) == -1 ) {
					if ( Regex.Match(InputString, "[ЯяЭэЮюЁёЪъЬьЫыЖжЧчЩщ]").Success ) { // [ЯяЭэЮюЁёЪъЬьЫыЖжЧчЩщ]
						// только для книг с русскими буквами
						InputString = Regex.Replace(
							InputString, @"(?<=lang>)\s*.+\s*(?=</lang>)",
							"ru", RegexOptions.IgnoreCase | RegexOptions.Multiline
						);
					}
				}
				// обработка неверно заданного русского языка
				InputString = Regex.Replace(
					InputString, @"(<lang>)(?:\s*)(?:RU-ru|Rus)(?:\s*)(</lang>)",
					"$1ru$2", RegexOptions.IgnoreCase
				);
				// обработка неверно заданного английского языка
				InputString = Regex.Replace(
					InputString, @"(<lang>)(?:\s*)(?:EN-en|Eng)(?:\s*)(</lang>)",
					"$1en$2", RegexOptions.IgnoreCase
				);
				
				/*****************
				 * Удаление тегов *
				 *****************/
				// удаление тегов <DIV ...></DIV>
				InputString = Regex.Replace(
					InputString, @"(<[Dd][Ii][Vv]\s*.+?>)|(<\s*/[Dd][Ii][Vv])",
					"", RegexOptions.IgnoreCase
				);
				// удаление тегов <br> <BR> <br/> <BR/> <br /> <BR /> <R>
				InputString = Regex.Replace(
					InputString, @"(?:<br|<BR|<R)(?:\s*)(?:/?>)",
					"", RegexOptions.IgnoreCase
				);
				// удаление тегов <cite id="nnnnnn" /> , <cite id="nnnnnn"></cite>
				InputString = Regex.Replace(
					InputString, "<(cite)\\s+id=\"(.+?)\"(?:>\\s*</\\1>|\\s*/>)",
					"", RegexOptions.IgnoreCase
				);
				// удаление тегов <p> и </p> в структуре <p> <empty-line /> </p>
				InputString = Regex.Replace(
					InputString, @"<p>\s*(<empty-line />\s*)</p>",
					"$1", RegexOptions.IgnoreCase
				);
				
				/*********************
				 * Обработка структур *
				 **********************/
				// обработка тегов body и section
				InputString = Regex.Replace(
					InputString, @"(<body\b|section\b)(?:\s*)xmlns=""(?:\s*)(>)",
					"$1$2", RegexOptions.IgnoreCase
				);
				// обработка вложенных друг в друга тегов cite (epigraph)
				InputString = Regex.Replace(
					InputString, @"^\s*((<(cite|epigraph)\b>\s*)\s*\2)(.+)(\s*</\3>){2}",
					"<text-author>$3</text-author>", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				// Вставка между </epigraph> или <image ... /> (<image ... ></image>) недостающего тега <empty-line/>
				InputString = Regex.Replace(
					InputString, @"((<image\s+.+/>)|(<image\s+.+>\s*</image>)|(</epigraph>))\s*(</section>)",
					"$1\n<empty-line/>\n$5", RegexOptions.None
				);
				// Перемещение пустой строки между Заголовком и Эпиграфом за Эпиграф (</title><empty-line/><epigraph><p>Эпиграф</p></epigraph><empty-line/>)
				InputString = Regex.Replace(
					InputString, @"(</title>\s*?)<empty-line ?/>\s*(<epigraph>)",
					"$1$2", RegexOptions.None
				);
				// Обрамление Эпиграфа в тексте блоками </section><section>
				InputString = Regex.Replace(
					InputString, @"(</p>\s*)(<epigraph>\s*.+?\s*</epigraph>\s*<empty-line\s*/>)",
					"$1</section>\n<section>\n$2\n</section>\n<section>", RegexOptions.None
				);
				// вставка <text-author> внутрь <poem> </poem>
				InputString = Regex.Replace(
					InputString, @"(</poem>)(\s*)(<text-author>\s*.+?\s*</text-author>)",
					"$3$2$1", RegexOptions.None
				);
				// удаление <empty-line /> между </title> и <epigraph>
				InputString = Regex.Replace(
					InputString, @"(</title>\s*)<empty-line ?/>\s*(<epigraph>)",
					"$1$2", RegexOptions.None
				);
				// удаление <empty-line /> между </epigraph> и <epigraph>
				InputString = Regex.Replace(
					InputString, @"(?<=</epigraph>)(\s*<empty-line ?/>\s*)(?=<epigraph>)",
					"\n\t\t", RegexOptions.None
				);
				
				// обработка подзаголовков <subtitle> (<subtitle>\n<p>\n11111\n</p>\n</subtitle>)
				InputString = Regex.Replace(
					InputString, @"(<subtitle>)\s*<p>(\s*.+?\s*)</p>\s*(</subtitle>)",
					"$1$2$3", RegexOptions.None
				);
				
				/**********************
				 * Обработка epigraph *
				 *********************/
				// обработка Эпиграфа с вложенным Эпиграфом и Эпиграфом вместо Автора эпиграфа: <epigraph><epigraph><p><strong>Текст</strong></p><p>Текст</p></epigraph><epigraph><emphasis><p>Достоевский</p></emphasis></epigraph></epigraph>
				InputString = Regex.Replace(
					InputString, @"(<epigraph>\s*)(<epigraph>\s*)((<p>\s*.+\s*</p>\s*){1,})</epigraph>\s*<epigraph>\s*<(emphasis|strong)\b>(\s*<p>)([^<]+?)(</p>\s*)</\5>\s*</epigraph>\s*(</epigraph>)",
					"$1$3<text-author><$5>$7</$5></text-author>\n$9", RegexOptions.None
				);
				// обработка Эпиграфа с вложенным Эпиграфом вместо Автора эпиграфа: <epigraph><p>Текст</p><p>Текст</p><epigraph><emphasis><p>Достоевский</p></emphasis></epigraph></epigraph><epigraph>
				InputString = Regex.Replace(
					InputString, @"(<epigraph>\s*)((<p>\s*.+\s*</p>\s*){1,})<epigraph>\s*<(emphasis|strong)\b>(\s*<p>)([^<]+?)(</p>\s*)</\4>\s*</epigraph>\s*(</epigraph>)",
					"$1$2<text-author><$4>$6</$4></text-author>\n$8", RegexOptions.None
				);
				// обработка Эпиграфа с вложенным Эпиграфом вместо Автора эпиграфа: <epigraph><p>Текст</p><p>Текст</p><epigraph><p><emphasis>Достоевский</emphasis></p></epigraph></epigraph><epigraph>
				InputString = Regex.Replace(
					InputString, @"(<epigraph>\s*)((<p>\s*.+\s*</p>\s*){1,})<epigraph>\s*<p>\s*(<(emphasis|strong)\b>)([^<]+?)</\5>\s*</p>\s*</epigraph>\s*(</epigraph>)",
					"$1$2<text-author>$4$6</$5></text-author>\n$7", RegexOptions.None
				);
				
				// Преобразование Эпиграфа в Цитату в случае <subtitle>Текст</subtitle><empty-line/><epigraph><p>Текст</p></epigraph>
				InputString = Regex.Replace(
					InputString, @"(<subtitle>\s*[^<]+?\s*</subtitle>\s*<empty-line ?/>\s*)(<epigraph>)\s*((<p>\s*.+?\s*</p>\s*){1,})\s*(</epigraph>)",
					"$1<cite>\n$3</cite>", RegexOptions.None
				);
				// Преобразование Эпиграфа в Цитату в случае <subtitle>Текст</subtitle><epigraph><p>Текст</p></epigraph>
				InputString = Regex.Replace(
					InputString, @"(<subtitle>\s*[^<]+?\s*</subtitle>\s*)(<epigraph>)\s*((<p>\s*.+?\s*</p>\s*){1,})\s*(</epigraph>)",
					"$1<cite>\n$3</cite>", RegexOptions.None
				);
				
				// Преобразование Эпиграфа в Цитату в случае <subtitle>Текст</subtitle><empty-line/><epigraph><emphasis><p>Текст</p></emphasis></epigraph>
				InputString = Regex.Replace(
					InputString, @"(<subtitle>\s*[^<]+?\s*</subtitle>\s*<empty-line ?/>\s*)(<epigraph>)\s*(<(strong|emphasis)\b>\s*)((<p>\s*.+?\s*</p>\s*){1,})\s*</\4>\s*(</epigraph>)",
					"$1<cite>\n$5</cite>", RegexOptions.None
				);
				// Преобразование Эпиграфа в Цитату в случае <subtitle>Текст</subtitle><epigraph><emphasis><p>Текст</p></emphasis></epigraph>
				InputString = Regex.Replace(
					InputString, @"(<subtitle>\s*[^<]+?\s*</subtitle>\s*)(<epigraph>)\s*(<(strong|emphasis)\b>\s*)((<p>\s*.+?\s*</p>\s*){1,})\s*</\4>\s*(</epigraph>)",
					"$1<cite>\n$5</cite>", RegexOptions.None
				);

				// Преобразование Эпиграфа после текста в тегах <p> в Цитату: <p>11</p><epigraph><p>11</p><p>11</p></epigraph>
				InputString = Regex.Replace(
					InputString, @"(<p>\s*[^<]+?\s*</p>\s*)(<epigraph>\s*)((<p>\s*.+?\s*</p>\s*){1,})\s*(</epigraph>)",
					"$1<cite>\n$3</cite>", RegexOptions.None
				);
				
				
				/*********************
				 * Обработка <title> *
				 ********************/
				// Обработка </section> между </title> и <section> (Заголовок Книги игнорируется): <section><title><p>Сны Ольги Ивановны</p></title></section><section>
				InputString = Regex.Replace(
					InputString, @"(</section>\s*<section>\s*<title>\s*)((<p>\s*.+\s*</p>\s*){1,}</title>\s*)(</section>\s*)(?=<section>)",
					"$1$2<empty-line/>\n$4", RegexOptions.None
				);
				
				// удаление <section> </section> вокруг Заголовка Книги
				InputString = Regex.Replace(
					InputString, @"(<body>)(\s*<section>\s*)(<title>\s*)((<p>\s*.+\s*</p>\s*){1,}</title>\s*)(</section>\s*)(?=<section>)",
					"$1$3$4", RegexOptions.None
				);
				
				// Обработка Заголовка и ее </section> в конце книги перед </body>
				InputString = Regex.Replace(
					InputString, @"(<section>\s*<title>\s*(<p>\s*[^<]+\s*</p>\s*){1,}</title>\s*)(</section>\s*)(?=</body>)",
					"$1<empty-line/>\n\t$3", RegexOptions.None
				);
				// Обработка Заголовка и ее </section> в конце книги перед </body>
				InputString = Regex.Replace(
					InputString, @"(<section>\s*<title>\s*(<p>\s*<(strong|emphasis)\b>\s*.+\s*</\3>\s*</p>\s*){1,}</title>\s*)(</section>\s*)(?=</body>)",
					"$1<empty-line/>\n\t$4", RegexOptions.None
				);
				
				
				// Обработка <annotation><i>
				InputString = Regex.Replace(
					InputString, @"<(annotation|cite)>\s*<(i|b)>\s*(([^<]+\s*){1,})</\2>\s*</\1>",
					"<$1>\n<p>$3</p>\n</$1>", RegexOptions.None
				);
				
				/****************
				 * Обработка id *
				 ****************/
				// обработка пустого id
				InputString = InputString = Regex.Replace(
					InputString, @"(?<=<id>)\s*\s*(?=</id>)",
					Guid.NewGuid().ToString().ToUpper(), RegexOptions.IgnoreCase
				);
				// обработка Либрусековских id
				InputString = Regex.Replace(
					InputString, @"(?<=<id>)\s*(Mon|Tue|Wed|Thu|Fri|Sat|Sun)\s+(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)\s+\d{2}\s+(\d{2}\:){2}\d{2}\s+\d{4}\s*(?=</id>)",
					Guid.NewGuid().ToString().ToUpper()
				);
				
				/********************
				 * Обработка ссылок *
				 *******************/
				// некорректное id ссылки (начинается цифра)
				InputString = InputString = Regex.Replace(
					InputString, "id=\"(\\d[^\"]*)\"",
					"id=\"_$1\"", RegexOptions.None
				);
				// обработка Либрусековских id
				InputString = Regex.Replace(
					InputString, "=\"#(\\d[^\"]*)\"",
					"=\"#_$1\"", RegexOptions.None
				);
				
				/****************************
				 * Обработка форматирования *
				 ***************************/
				// обработка тегов полужирного
				InputString = Regex.Replace(
					InputString, "<(/?)[bB]\\b((?:[^>\"']|\"[^\"]*\"|'[^']*')*)>",
					"<$1strong>", RegexOptions.IgnoreCase
				);
				// обработка тегов курсива
				InputString = Regex.Replace(
					InputString, "<(/?)[iI]\\b((?:[^>\"']|\"[^\"]*\"|'[^']*')*)>",
					"<$1emphasis>", RegexOptions.IgnoreCase
				);
				// обработка тегов курсива
				InputString = Regex.Replace(
					InputString, "<(/?)[eE][mM]\\b((?:[^>\"']|\"[^\"]*\"|'[^']*')*)>",
					"<$1emphasis>", RegexOptions.IgnoreCase
				);
				// обработка вложенных друг в друга тегов strong или emphasis
				InputString = Regex.Replace(
					InputString, @"^\s*(<strong>|<emphasis>)\s*\1\s*(<p>)",
					"$2$1", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				InputString = Regex.Replace(
					InputString, @"(</p>)\s*(</strong>|</emphasis>)\s*\2",
					"$2$1", RegexOptions.IgnoreCase
				);
				// внесение тегов strong или emphasis в теги <p> </p>
				InputString = Regex.Replace(
					InputString, @"<(strong|emphasis)\b>\s*(<p>)([^<]+)(</p>)\s*</\1>",
					"$2<$1>$3</$1>$4", RegexOptions.IgnoreCase
				);
				
				
				/*********************
				 * Обработка графики *
				 ********************/
				// обработка картинок
				InputString = Regex.Replace(
					InputString, @"(<image .+)(>)(?:\s*)(?:</image>)",
					"$1 /$2", RegexOptions.IgnoreCase
				);
				// обработка картинки между <section> и <title>
				InputString = Regex.Replace(
					InputString, @"(<section>\s*)(<image[^/]+?/>\s*)(<title>\s*)(<p[^>]+?>)([^<]+?)(</p>\s*</title>)",
					"$1$3$4$5$6\n\t\t$2", RegexOptions.IgnoreCase
				);
				
				
				/***********************************
				 * обработка недопустимых символов *
				 **********************************/
				// удаление недопустимых символов
				InputString = Regex.Replace(
					InputString, "(?<=[ЁёА-Яа-я])(&)(?=[-ЁёА-Яа-я])|[\x00-\x08\x0B\x0C\x0E-\x1F]",
					"", RegexOptions.None
				);
				// замена & на &amp
				InputString = Regex.Replace(
					InputString, "([A-Za-zЁёА-Яа-я.,!?«»\"]\\s*)&(\\s*[A-KM-Za-km-zЁёА-Яа-я.,!?«»\"])",
					"$1&amp;$2", RegexOptions.None
				);

				/*************
				 * чистка код *
				 *************/
				// удаление <p><emphasis></emphasis></p>
				InputString = Regex.Replace(
					InputString, @"<p>\s*<(strong|emphasis|strikethrough|sub|sup|code|image|a|style)\b>\s*</\1>\s*</p>",
					"", RegexOptions.None
				);
				// удаление <emphasis></emphasis>
				InputString = Regex.Replace(
					InputString, @"<(strong|emphasis|strikethrough|sub|sup|code|image|a|style)\b>\s*</\1>",
					"", RegexOptions.None
				);

				
			} catch (Exception /*ex*/) {
//				MessageBox.Show(ex.Message);
			}
			
			return InputString;
		}
		
	}
}
