/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 12.10.2015
 * Время: 6:53
 * 
 * License: GPL 2.1
 */
using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Collections.Generic;

namespace Core.AutoCorrector
{
	/// <summary>
	/// Чистка кода fb2 файла от лишних и/или пустых тегов и символов
	/// </summary>
	public class FB2CleanCode
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
			"<table>", "<table ", "</table>", "<tr>", "<tr ", "</tr>", "<th>", "<th ", "</th>", "<td>", "<td ", "</td>",
			"<align>", "<align ", 
			
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
			"<history>", "</history>", "<history ", "<custom-info>", "<custom-info ", "</custom-info>",
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
		private static string _RegAmp = @"(?!&(?:amp|lt|gt|(#x[\d\w]{4})|(?:#\d{1,6}));)(&)";//пропускае юникод, символы в десятичной кодировке и меняем уголки
		
		#endregion
		
		public FB2CleanCode()
		{
		}
		
		public static string getRegAmpString() {
			return _RegAmp;
		}
		
		public static string[] getTagsArray() {
			return _Tags;
		}
		
		public static Hashtable getTagsHashtable() {
			Hashtable htTags = new Hashtable(_Tags.Length);
			for ( int i = 0; i != _Tags.Length; ++i )
				htTags.Add(i, _Tags[i]);
			return htTags;
		}
		
		// предварительная обработка текста
		public static string preProcessing( string InputString ) {
			// удаление стартовых пробелов ДО тегов
			InputString = Regex.Replace(
				InputString, @"^\s+<",
				"<", RegexOptions.Multiline
			);
			// удаление завершающих пробелов ПОСЛЕ тегов и символов переноса строк
			return Regex.Replace(
				InputString, @"(?:\s*\r?\n)",
				"", RegexOptions.Multiline
			);
		}
		
		// пост обработка текста
		public static string postProcessing( string InputString ) {
			// разбиение на теги (смежные теги)
			return Regex.Replace(
				InputString, @"><",
				">\n<", RegexOptions.Multiline
			);
		}
		
		// чистка кода
		public static string cleanFb2Code( string InputString ) {
			/****************************
			 * Обработка форматирования *
			 ***************************/
			// обработка тегов полужирного
			InputString = Regex.Replace(
				InputString, "<(/?)(b|big)>",
				"<$1strong>", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка тегов курсива
			InputString = Regex.Replace(
				InputString, "<(/?)(i|em)>",
				"<$1emphasis>", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);

			/*****************
			 * Удаление тегов *
			 *****************/
			// удаление тегов <DIV ...></DIV>
			InputString = Regex.Replace(
				InputString, "(?!<>)<(?:/?)(?:[dD][iI][vV]\\s?(?:[^<]+)?(?:\"[^\"]*\"|'[^']*')?)*>",
				"", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// удаление тегов <cite />, <cite id="nnnnnn" />
			InputString = Regex.Replace(
				InputString, "(?!<>)(?:<(?:(?:cite|epigraph|poem|annotation)\\s*?(?:[^<]+)?(?:\"[^\"]*\"|'[^']*')?\\s*/)*>)",
				"", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// удаление тегов <cite id="nnnnnn"></cite>, <cite></cite>
			InputString = Regex.Replace(
				InputString, "(?!<>)(?:<(?:(?:cite|epigraph|poem|annotation)\\s*?(?:[^<]+)?(?:\"[^\"]*\"|'[^']*')?)*>)(?:<\\s*/(?:cite|epigraph|poem|annotation)>)",
				"", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// удаление тегов <br> <BR> <br/> <BR/> <br /> <BR /> <R>
			InputString = Regex.Replace(
				InputString, "<(?:b?r(?:(?:[^>\"']|\"[^\"]*\"|'[^']*')*)/?)>",
				"", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// удаление <p><emphasis></emphasis></p>
			InputString = Regex.Replace(
				InputString, @"(?:(?:<p>\s*)<(?'tag'strong|emphasis|strikethrough|sub|sup|code|image|a|style)\b>\s*</\k'tag'>(?:\s*</p>))",
				"", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// удаление <emphasis></emphasis>
			InputString = Regex.Replace(
				InputString, @"<(?'tag'strong|emphasis|strikethrough|sub|sup|code|image|a|style)\b>\s*</\k'tag'>",
				"", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			
			return InputString;
		}
		
		// удаление недопустимых символов
		public static string deleteIllegalCharacters( string InputString ) {
			return Regex.Replace(
				InputString, "[\x00-\x08\x0B\x0C\x0E-\x1F]",
				"", RegexOptions.None
			);
		}
		// обработка < и > и &
		public static string processingCharactersMoreAndLessAndAmp( string InputString, Hashtable htTags ) {
			Regex regex = new Regex(_RegAmp); // пропускае юникод, символы в десятичной кодировке и меняем уголки
			List<string> list = splitString( InputString );
			StringBuilder sbNewString = new StringBuilder( list.Count );
			string token = string.Empty;
			foreach ( string item in list ) {
				token = item.Replace("\r\n", "");
				if ( isFB2Tag( token, htTags ) ) {
					sbNewString.Append( token );
				} else {
					if ( token.IndexOf("\r\n", 0) == 0 || token.IndexOf("\n", 0) == 0
					    || string.IsNullOrEmpty( token ) || token.IndexOf("\r", 0) == 0 )
						continue;
					else if ( token.Trim().Equals( ">" ) )
						continue;
					else if ( token.IndexOf(">\n", 0) == -1 && token.IndexOf(">\r\n", 0) == -1 )
						sbNewString.Append(
							regex.Replace(
								token.Replace("<", "&lt;").Replace(">", "&gt;"), "&amp;"
							)
						);
					else
						sbNewString.Append( "\n" );
				}
			}
			return sbNewString.ToString();
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
			if ( !string.IsNullOrWhiteSpace( InputString ) ) {
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
						string s = InputString.Substring(start+1);
						if ( !string.IsNullOrEmpty( s ) )
							list.Add( s );
						break;
					}
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
			
			int LastIndexLess = InputString.LastIndexOf( '<' );
			if ( LastIndexLess > 0 ) {
				string s = InputString.Substring( 0, LastIndexLess );
				if ( !string.IsNullOrEmpty( s ) )
					list.Add( s );
				InputString = InputString.Substring( LastIndexLess );
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
					if ( !string.IsNullOrEmpty( InputString ) )
						list.Add( InputString );
					break;
				}
			}
		}
//
//		// склейка тегов абзацев
//		private static string mergeTagWithText( string InputString ) {
//			// вставка маркеров пробелов (для предотвращения удаления стартовых пробелов)
//			InputString = Regex.Replace(
//				InputString, @"^( +)(\w|\d)",
//				"%$1%$2", RegexOptions.Multiline
//			);
//
//			//
//			InputString = Regex.Replace(
//				InputString, @"(>)\s+([^<]+?)\s+(<)",
//				"$1$2$3", RegexOptions.None
//			);
//			//
//			InputString = Regex.Replace(
//				InputString, @"(<p>)\s+(<)",
//				"$1$2", RegexOptions.None
//			);
//			//
//			InputString = Regex.Replace(
//				InputString, @"(>)\s+(</p>)",
//				"$1$2", RegexOptions.None
//			);
//
//			//
//			InputString = Regex.Replace(
//				InputString, @"(<strong>)\s+(<emphasis>)",
//				"$1$2", RegexOptions.None
//			);
//			//
//			InputString = Regex.Replace(
//				InputString, @"(<emphasis>)\s+(<strong>)",
//				"$1$2", RegexOptions.None
//			);
//			//
//			InputString = Regex.Replace(
//				InputString, @"(</strong>)\s+(</emphasis>)",
//				"$1$2", RegexOptions.None
//			);
//			//
//			InputString = Regex.Replace(
//				InputString, @"(</emphasis>)\s+(</strong>)",
//				"$1$2", RegexOptions.None
//			);
//			//
//			InputString = Regex.Replace(
//				InputString, @"(</strong>)\s+(<emphasis>)",
//				"$1$2", RegexOptions.None
//			);
//			//
//			InputString = Regex.Replace(
//				InputString, @"(</emphasis>)\s+(<strong>)",
//				"$1$2", RegexOptions.None
//			);
//
//			// удаление маркеров пробелов
//			InputString = Regex.Replace(
//				InputString, @"(>)%( +)%(\w|\d)",
//				"$1$2$3", RegexOptions.None
//			);
//			return InputString;
//		}
		
	}
}
