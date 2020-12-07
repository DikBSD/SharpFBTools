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
using System.Windows.Forms;

using Core.Common;

namespace Core.AutoCorrector
{
	/// <summary>
	/// Чистка кода fb2 файла от лишних и/или пустых тегов и символов
	/// </summary>
	public class FB2CleanCode
	{
		#region Закрытые данные класса
		private const string _MessageTitle = "Автокорректор";
		
		private static string _FilePath = string.Empty; // Путь к обрабатываемому файлу
		
		private static string[] _Tags = {
			"<p>", "<p ", "</p>", "<p/>", "<p />",
			"<empty-line/>", "<empty-line />", "<empty-line ", "<empty-line>", "</empty-line>",
			"<strong>", "</strong>", "<strong/>", "<strong />", "<strong ",
			"<emphasis>", "</emphasis>", "<emphasis/>", "<emphasis />", "<emphasis ",
			"<a ", "</a>",
			"<section>", "</section>", "<section ", "<section/>", "<section />",
			"<title>", "</title>", "<title ", "<title/>", "<title />",
			"<subtitle>", "<subtitle ", "</subtitle>", "<subtitle/>", "<subtitle />",
			"<image>", "</image>", "<image ", "<image/>", "<image />",
			"<poem>", "<poem ", "</poem>", "<poem/>", "<poem />",
			"<stanza>", "<stanza ", "</stanza>", "<stanza/>", "<stanza />",
			"<v>", "<v ", "</v>", "<v/>", "<v />",
			"<cite>", "<cite ", "</cite>", "<cite/>", "<cite />",
			"<epigraph>", "</epigraph>", "<epigraph ", "<epigraph/>", "<epigraph />",
			"<annotation>", "</annotation>", "<annotation ", "<annotation/>", "<annotation />",
			"<strikethrough>", "</strikethrough>", "<strikethrough/>", "<strikethrough />", "<strikethrough ",
			"<sub>", "</sub>", "<sub/>", "<sub />", "<sub ",
			"<sup>", "</sup>", "<sup/>", "<sup />", "<sup ",
			"<code>", "</code>", "<code/>", "<code />", "<code ",
			"<table>", "<table ", "</table>", "<table/>", "<table />",
			"<tr>", "<tr ", "</tr>", "<tr/>", "<tr />",
			"<th>", "<th ", "</th>", "<th/>", "<th />",
			"<td>", "<td ", "</td>", "<td/>", "<td/>",
			"<align>", "<align ", "<align/>", "<align />",
			
			"<body>", "<body ", "</body>", "<body/>", "<body />",
			"<binary ", "</binary>", "<binary/>", "<binary />",
			"<title-info>", "</title-info>", "<title-info ", "<title-info/>", "<title-info />",
			"<genre>", "</genre>", "<genre ", "<genre/>", "<genre />",
			"<author>", "</author>", "<author ", "<author/>", "<author />",
			"<book-title>", "</book-title>", "<book-title ", "<book-title/>", "<book-title />",
			"<keywords>", "</keywords>", "<keywords ", "<keywords/>", "<keywords />",
			"<date>", "</date>", "<date ", "<date/>", "<date />",
			"<coverpage>", "</coverpage>", "<coverpage ", "<coverpage/>", "<coverpage />",
			"<lang>", "</lang>", "<lang ", "<lang/>", "<lang />",
			"<src-lang>", "</src-lang>", "<src-lang ", "<src-lang/>", "<src-lang />",
			"<translator>", "</translator>", "<translator ", "<translator/>", "<translator />",
			"<sequence>", "</sequence>", "<sequence ", "<sequence/>", "<sequence />",
			"<src-title-info>", "</src-title-info>", "<src-title-info ", "<src-title-info/>", "<src-title-info />",
			"<document-info>", "</document-info>", "<document-info ", "<document-info/>", "<document-info />",
			"<program-used>", "</program-used>", "<program-used ", "<program-used/>", "<program-used />",
			"<src-url>", "</src-url>", "<src-url ", "<src-url/>", "<src-url />",
			"<src-ocr>", "</src-ocr>", "<src-ocr ", "<src-ocr/>", "<src-ocr />",
			"<id>", "</id>", "<id ", "<id/>", "<id />",
			"<version>", "</version>", "<version ", "<version/>", "<version />",
			"<publish-info>", "</publish-info>", "<publish-info ", "<publish-info/>", "<publish-info />",
			"<book-name>", "</book-name>", "<book-name ", "<book-name/>", "<book-name />",
			"<publisher>", "</publisher>", "<publisher ", "<publisher/>", "<publisher />",
			"<city>", "</city>", "<city ", "<city/>", "<city />",
			"<year>", "</year>", "<year ", "<year/>", "<year />",
			"<isbn>", "<isbn ", "</isbn>", "<isbn/>", "<isbn />",
			"<history>", "</history>", "<history ", "<history/>", "<history />",
			"<text-author>", "<text-author ", "</text-author>", "<text-author/>", "<text-author />",
			"<first-name>", "<first-name ", "</first-name>", "<first-name/>", "<first-name />",
			"<middle-name>", "<middle-name ", "</middle-name>", "<middle-name/>", "<middle-name />",
			"<last-name>", "<last-name ", "</last-name>", "<last-name/>", "<last-name />",
			"<nickname>", "<nickname ", "</nickname>", "<nickname/>", "<nickname />",
			"<home-page>", "<home-page ", "</home-page>", "<home-page/>", "<home-page />",
			"<email>", "<email ", "</email>", "<email/>", "<email />",
			"<description>", "</description>", "<description ", "<description/>", "<description />",
			"<custom-info>", "<custom-info ", "</custom-info>", "<custom-info/>", "<custom-info />",
			"<FictionBook ", "</FictionBook>",
			"<stylesheet ", "</stylesheet>", "<style>", "</style>", "<style ",
			"<output>", "</output>", "<part>", "</part>", "<part ",
			"<output-document-class>", "</output-document-class>", "<output-document-class ", "<?xml ",
			
			"<!--", "-->"
		};
		private static readonly string _RegAmp = @"(?!&(?:amp|lt|gt|(#x[\d\w]{4})|(?:#\d{1,6}));)(&)";//пропускае юникод, символы в десятичной кодировке и меняем уголки
		
		#endregion
		
		/// <summary>
		/// Конструктор класса FB2CleanCode
		/// </summary>
		/// <param name="FilePath">Путь к обрабатываемому файлу</param>
		public FB2CleanCode( string FilePath )
		{
			_FilePath = FilePath;
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
		
		/// <summary>
		/// Предварительная обработка текста
		/// </summary>
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
				InputString, "><",
				">\n<", RegexOptions.Multiline
			);
		}
		
		// чистка кода
		public static string cleanFb2Code( string InputString ) {
			/****************************
			 * Обработка форматирования *
			 ***************************/
			// обработка тегов полужирного
			try {
				InputString = Regex.Replace(
					InputString, "<(/?)(b|big)>",
					"<$1strong>", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "FB2CleanCode:cleanFb2Code():\r\nОбработка форматирования: теги полужирного."
				);
			}
			
			// обработка тегов курсива
			try {
				InputString = Regex.Replace(
					InputString, "<(/?)(i|em)>",
					"<$1emphasis>", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "FB2CleanCode:cleanFb2Code():\r\nОбработка форматирования: теги курсива."
				);
			}

			/*****************
			 * Удаление тегов *
			 *****************/
			// удаление тегов <DIV ...></DIV>
			try {
				InputString = Regex.Replace(
					InputString, "(?!<>)<(?:/?)(?:[dD][iI][vV]\\s?(?:[^<]+)?(?:\"[^\"]*\"|'[^']*')?)*>",
					"", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "FB2CleanCode:cleanFb2Code():\r\nУдаление тегов <DIV ...></DIV>."
				);
			}
			
			// удаление тегов <cite />, <cite id="nnnnnn" />
			try {
				InputString = Regex.Replace(
					InputString, "(?!<>)(?:<(?:(?:cite|epigraph|poem|annotation)\\s*?(?:[^<]+)?(?:\"[^\"]*\"|'[^']*')?\\s*/)*>)",
					"", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "FB2CleanCode:cleanFb2Code():\r\nУдаление тегов <cite />, <cite id=\"nnnnnn\" />."
				);
			}
			
			// удаление тегов <cite id="nnnnnn"></cite>, <cite></cite>
			try {
				InputString = Regex.Replace(
					InputString, "(?!<>)(?:<(?:(?:cite|epigraph|poem|annotation)\\s*?(?:[^<]+)?(?:\"[^\"]*\"|'[^']*')?)*>)(?:<\\s*/(?:cite|epigraph|poem|annotation)>)",
					"", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "FB2CleanCode:cleanFb2Code():\r\nУдаление тегов <cite id=\"nnnnnn\"></cite>, <cite></cite>."
				);
			}
			
			// удаление тегов <cite id="bdn__4"></cite>
			try {
				InputString = Regex.Replace(
					InputString, "<cite +?id=\"[^\"]+\">\\s*?</cite>",
					"", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "FB2CleanCode:cleanFb2Code():\r\nУдаление тегов <cite id=\"bdn__4\"></cite>."
				);
			}
			
			// удаление тегов <br> <BR> <br/> <BR/> <br /> <BR /> <R>
			try {
				InputString = Regex.Replace(
					InputString, "<(?:b?r(?:(?:[^>\"']|\"[^\"]*\"|'[^']*')*)/?)>",
					"", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "FB2CleanCode:cleanFb2Code():\r\nУдаление тегов <br> <BR> <br/> <BR/> <br /> <BR /> <R>."
				);
			}
			
			// удаление пустых тегов <p><emphasis></emphasis></p>
			try {
				InputString = Regex.Replace(
					InputString, @"(?:(?:<p>\s*)<(?'tag'strong|emphasis|strikethrough|sub|sup|code|image|a|style)\b>\s*</\k'tag'>(?:\s*</p>))",
					"", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "FB2CleanCode:cleanFb2Code():\r\nУдаление пустых тегов <p><emphasis></emphasis></p>."
				);
			}
			
			// удаление пустых тегов <emphasis></emphasis>
			try {
				InputString = Regex.Replace(
					InputString, @"<(?'tag'strong|emphasis|strikethrough|sub|sup|code|image|a|style|poem|stanza|section|history|author|text-author)\b>\s*</\k'tag'>",
					"", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "FB2CleanCode:cleanFb2Code():\r\nУдаление пустых тегов <emphasis></emphasis>."
				);
			}
			
			// удаление <stanza/>, <stanza />
			try {
				InputString = Regex.Replace(
					InputString, @"<(?:stanza|poem|SUBTITLE|sup|sub|section|table|annotation|title|cite|epigraph|strong|emphasis|strikethrough|code|style|history|author|text-author) ?/>",
					"", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "FB2CleanCode:cleanFb2Code():\r\nУдаление <stanza/>, <stanza />."
				);
			}
			
			// удаление <h1> ... <h6> с их содержимым
			try {
				InputString = Regex.Replace(
					InputString, @"<\b(h1|h2|h3|h4|h5|h6)\b\s+[^>]*?\w*?[^>]*>((?:(?!</\1>).)*)</\1>",
					"", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "FB2CleanCode:cleanFb2Code():\r\nУдаление <h1> ... <h6> с их содержимым."
				);
			}

			// удаление тегов <style> вида <style>
			try
			{
				InputString = Regex.Replace(
					InputString, "<style>",
					"", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			}
			catch (RegexMatchTimeoutException /*ex*/ ) { }
			catch (Exception ex)
			{
				Debug.DebugMessage(
					_FilePath, ex, "FB2CleanCode:cleanFb2Code():\r\nУдаление тегов <style> вида <style>."
				);
			}

			// удаление тегов <style> вида <style name="MsoFootnoteReference">, <style name="Italic Style"> и </style>
			try
			{
				InputString = Regex.Replace(
					InputString, "<style name=\"([\\w\\s?|\\d\\s?])+\">",
					"", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				InputString = Regex.Replace(
					InputString, "</style>",
					"", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "FB2CleanCode:cleanFb2Code():\r\nУдаление тегов <style> вида <style name=\"MsoFootnoteReference\">, <style name=\"Italic Style\"> и </style>."
				);
			}
			
			return InputString;
		}
		
		/// <summary>
		/// Удаление недопустимых символов
		/// </summary>
		public static string deleteIllegalCharacters( string InputString ) {
			try {
				InputString = Regex.Replace(
					InputString, "[\x00-\x08\x0B\x0C\x0E-\x1F]",
					"", RegexOptions.None
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "FB2CleanCode:deleteIllegalCharacters():\r\nУдаление недопустимых символов."
				);
			}
			return InputString;
		}
		
		/// <summary>
		/// Обработка < и > и &
		/// </summary>
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
					if ( token.IndexOf("\r\n", 0, StringComparison.CurrentCulture) == 0 || token.IndexOf("\n", 0, StringComparison.CurrentCulture) == 0
					    || string.IsNullOrEmpty( token ) || token.IndexOf("\r", 0, StringComparison.CurrentCulture) == 0 )
						continue;
					else if ( token.Trim().Equals( ">" ) )
						continue;
					else if ( token.IndexOf(">\n", 0, StringComparison.CurrentCulture) == -1 && token.IndexOf(">\r\n", 0, StringComparison.CurrentCulture) == -1 )
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
		
		/// <summary>
		/// Проверка, содержит ли тестируемый текст вначале fb2 тего
		/// </summary>
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

		/// <summary>
		/// Разбивка текста по токенам с границами <>
		/// </summary>
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
		/// <summary>
		/// Разбивка текста по токенам с границами <>
		/// </summary>
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
