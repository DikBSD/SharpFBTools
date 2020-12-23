/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим [DikBSD]
 * Date: 24.04.2009
 * Time: 10:26
 * 
 * License: GPL 2.1
 */
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

using System.Windows.Forms;

namespace Core.Common
{
	/// <summary>
	/// StringProcessing: обработка строк и путей файлов
	/// </summary>
	public class StringProcessing
	{
		private readonly static string _StrictLetters = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 \\[](){}~-—=–_.,!@#$%^&№`';«»";
		
		#region Закрытые вспомогательные методы класса
		private static string[] MakeTranslitLettersArray() {
			// массив замены русских символов латинскими
			#region Код
			string[] saTranslitLetters = new string[153];
			saTranslitLetters[0]="a";	// а
			saTranslitLetters[1]="b";	// б
			saTranslitLetters[2]="v";	// в
			saTranslitLetters[3]="g";	// г
			saTranslitLetters[4]="d";	// д
			saTranslitLetters[5]="e";	// е
			saTranslitLetters[6]="yo";	// ё
			saTranslitLetters[7]="zh";	// ж
			saTranslitLetters[8]="z";	// з
			saTranslitLetters[9]="i";	// и
			saTranslitLetters[10]="y";	// й
			saTranslitLetters[11]="k";	// к
			saTranslitLetters[12]="l";	// л
			saTranslitLetters[13]="m";	// м
			saTranslitLetters[14]="n";	// н
			saTranslitLetters[15]="o";	// о
			saTranslitLetters[16]="p";	// п
			saTranslitLetters[17]="r";	// р
			saTranslitLetters[18]="s";	// с
			saTranslitLetters[19]="t";	// т
			saTranslitLetters[20]="u";	// у
			saTranslitLetters[21]="f";	// ф
			saTranslitLetters[22]="h";	// х
			saTranslitLetters[23]="ts";	// ц
			saTranslitLetters[24]="ch";	// ч
			saTranslitLetters[25]="sh";	// ш
			saTranslitLetters[26]="sch";// щ
			saTranslitLetters[27]="'";	// ъ
			saTranslitLetters[28]="y";	// ы
			saTranslitLetters[29]="'";	// ь
			saTranslitLetters[30]="e";	// э
			saTranslitLetters[31]="yu";	// ю
			saTranslitLetters[32]="ya";	// я
			saTranslitLetters[33]="A";	// А
			saTranslitLetters[34]="B";	// Б
			saTranslitLetters[35]="V";	// В
			saTranslitLetters[36]="G";	// Г
			saTranslitLetters[37]="D";	// Д
			saTranslitLetters[38]="E";	// Е
			saTranslitLetters[39]="YO";	// Ё
			saTranslitLetters[40]="ZH";	// Ж
			saTranslitLetters[41]="Z";	// З
			saTranslitLetters[42]="I";	// И
			saTranslitLetters[43]="Y";	// Й
			saTranslitLetters[44]="K";	// К
			saTranslitLetters[45]="L";	// Л
			saTranslitLetters[46]="M";	// М
			saTranslitLetters[47]="N";	// Н
			saTranslitLetters[48]="O";	// О
			saTranslitLetters[49]="P";	// П
			saTranslitLetters[50]="R";	// Р
			saTranslitLetters[51]="S";	// С
			saTranslitLetters[52]="T";	// Т
			saTranslitLetters[53]="U";	// У
			saTranslitLetters[54]="F";	// Ф
			saTranslitLetters[55]="H";	// Х
			saTranslitLetters[56]="TS";	// Ц
			saTranslitLetters[57]="CH";	// Ч
			saTranslitLetters[58]="SH";	// Ш
			saTranslitLetters[59]="SCH";// Щ
			saTranslitLetters[60]="'";	// Ъ
			saTranslitLetters[61]="Y";	// Ы
			saTranslitLetters[62]="'";	// Ь
			saTranslitLetters[63]="E";	// Э
			saTranslitLetters[64]="YU";	// Ю
			saTranslitLetters[65]="YA";	// Я
			
			saTranslitLetters[66]="a";
			saTranslitLetters[67]="b";
			saTranslitLetters[68]="c";
			saTranslitLetters[69]="d";
			saTranslitLetters[70]="e";
			saTranslitLetters[71]="f";
			saTranslitLetters[72]="g";
			saTranslitLetters[73]="h";
			saTranslitLetters[74]="i";
			saTranslitLetters[75]="j";
			saTranslitLetters[76]="k";
			saTranslitLetters[77]="l";
			saTranslitLetters[78]="m";
			saTranslitLetters[79]="n";
			saTranslitLetters[80]="o";
			saTranslitLetters[81]="p";
			saTranslitLetters[82]="q";
			saTranslitLetters[83]="r";
			saTranslitLetters[84]="s";
			saTranslitLetters[85]="t";
			saTranslitLetters[86]="u";
			saTranslitLetters[87]="v";
			saTranslitLetters[88]="w";
			saTranslitLetters[89]="x";
			saTranslitLetters[90]="y";
			saTranslitLetters[91]="z";
			saTranslitLetters[92]="A";
			saTranslitLetters[93]="B";
			saTranslitLetters[94]="C";
			saTranslitLetters[95]="D";
			saTranslitLetters[96]="E";
			saTranslitLetters[97]="F";
			saTranslitLetters[98]="G";
			saTranslitLetters[99]="H";
			saTranslitLetters[100]="I";
			saTranslitLetters[101]="J";
			saTranslitLetters[102]="K";
			saTranslitLetters[103]="L";
			saTranslitLetters[104]="M";
			saTranslitLetters[105]="N";
			saTranslitLetters[106]="O";
			saTranslitLetters[107]="P";
			saTranslitLetters[108]="Q";
			saTranslitLetters[109]="R";
			saTranslitLetters[110]="S";
			saTranslitLetters[111]="T";
			saTranslitLetters[112]="U";
			saTranslitLetters[113]="V";
			saTranslitLetters[114]="W";
			saTranslitLetters[115]="X";
			saTranslitLetters[116]="Y";
			saTranslitLetters[117]="Z";
			saTranslitLetters[118]="0";
			saTranslitLetters[119]="1";
			saTranslitLetters[120]="2";
			saTranslitLetters[121]="3";
			saTranslitLetters[122]="4";
			saTranslitLetters[123]="5";
			saTranslitLetters[124]="6";
			saTranslitLetters[125]="7";
			saTranslitLetters[126]="8";
			saTranslitLetters[127]="9";
			
			saTranslitLetters[128]=" ";
			saTranslitLetters[129]="`";
			saTranslitLetters[130]="~";
			saTranslitLetters[131]="'";
			saTranslitLetters[132]="!";
			saTranslitLetters[133]="@";
			saTranslitLetters[134]="#";
			saTranslitLetters[135]="№";
			saTranslitLetters[136]="$";
			saTranslitLetters[137]="%";
			saTranslitLetters[138]="^";
			saTranslitLetters[139]="[";
			saTranslitLetters[140]="]";
			saTranslitLetters[141]="(";
			saTranslitLetters[142]=")";
			saTranslitLetters[143]="{";
			saTranslitLetters[144]="}";
			saTranslitLetters[145]="-";
			saTranslitLetters[146]="+";
			saTranslitLetters[147]="=";
			saTranslitLetters[148]="_";
			saTranslitLetters[149]=";";
			saTranslitLetters[150]=".";
			saTranslitLetters[151]=",";
			saTranslitLetters[152]="\\";
			
			return saTranslitLetters;
			#endregion
		}
		#endregion
		
		public StringProcessing()
		{
		}
		
		#region Открытые методы класса
		// обработка пробелов в строке
		public static string SpaceString( string sString, int nMode ) {
			if ( string.IsNullOrEmpty( sString ) )
				return sString;

			string s = string.Empty;
			for ( int i=0; i!=sString.Length; ++i ) {
				if ( sString[i]==' ' ) {
					switch ( nMode ) {
						case 0: // оставить пробел
							s += sString[i];
							break;
						case 1: // удалить пробел
							break;
						case 2: // заменить пробел на '_'
							s += '_';
							break;
						case 3: // заменить пробел на '-'
							s += '-';
							break;
						case 4: // заменить пробел на '+'
							s += '+';
							break;
						case 5: // заменить пробел на '~'
							s += '~';
							break;
						case 6: // заменить пробел на '.'
							s += '.';
							break;
					}
				} else {
					s += sString[i];
				}
			}
			return s;
		}

		// Все Слова Строки Начать с Большой Буквы
		public static string ToUpperFirstLetterAnyWord( string sString, char c ) {
			if( sString.IndexOf( c ) !=-1 ) {
				StringBuilder sb = new StringBuilder();
				for( int i=0; i!=sString.Length; ++i ) {
					if( i >= sString.Length )
						break;
					else {
						if( sString[i] != c )
							sb.Append( sString[i] );
						else {
							if( i >= sString.Length-1 ) {
								sb.Append( sString[i] ); // c - это последний символ в строке
								break;
							}
							sb.Append( sString[i]+sString[i+1].ToString().ToUpper() );
							++i;
						}
					}
				}
				sb[0] = Convert.ToChar( sString[0].ToString().ToUpper() );
				sString = sb.ToString();
			}
			return sString;
		}
		
		// задание регистра строке
		public static string RegisterString( string sString, int nMode ) {
			if ( string.IsNullOrEmpty( sString ) )
				return sString;

			switch ( nMode ) {
				case 0: // Как есть
					return sString;
				case 1: // строчные
					return sString.ToLower();
				case 2: // ПРОПИСНЫЕ
					return sString.ToUpper();
				case 3: // Все Слова С Большой Буквы
					string sRet = sString.ToLower();
					// Символ справа от '' делаем Прописным
					sRet = ToUpperFirstLetterAnyWord( sRet, ' ' );
					sRet = ToUpperFirstLetterAnyWord( sRet, '_' );
					sRet = ToUpperFirstLetterAnyWord( sRet, '[' );
					sRet = ToUpperFirstLetterAnyWord( sRet, '(' );
					sRet = ToUpperFirstLetterAnyWord( sRet, ']' );
					sRet = ToUpperFirstLetterAnyWord( sRet, ')' );
					sRet = ToUpperFirstLetterAnyWord( sRet, '\\' );
					return sRet;
				default:
					return sString;
			}
		}
		
		// транслитерация строки
		public static string TransliterationString( string sString ) {
			string sStr = sString;
			if ( string.IsNullOrEmpty( sString ) )
				return sString;

			const string sTemplate = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 `~'!@#№$%^[](){}-+=_;.,\\";
			string[] saTranslitLetters = MakeTranslitLettersArray();
			string sTranslit = string.Empty;
			for ( int i=0; i!=sStr.Length; ++i ) {
				int nInd = sTemplate.IndexOf( sStr[i] );
				sTranslit += ( nInd!=-1 ) ? saTranslitLetters[nInd] : "_";
			}
			return sTranslit;
		}
		
		// "строгое" значение строки
		public static string StrictString( string sString ) {
			string s = sString;
			if ( string.IsNullOrEmpty( sString ) )
				return sString;

			string sStrict = string.Empty;
			for ( int i = 0; i != s.Length; ++i ) {
				int nInd = _StrictLetters.IndexOf( s[i] );
				if ( nInd != -1 )
					sStrict += s[i];
			}
			return sStrict;
		}
		
		// "строгое" значение Юникодной строки - псевдо-транслитерация
		public static string TranslateUnicodeString( string sString ) {
			// расширенная латиница
			string s = string.Empty;
			Regex rx = new Regex( "[ÀÁÂÃÄÅÆĀĂĄǍǺǼẠẢẤẦẨẪẬẮẰẲẴẶ]" );
			s = rx.Replace( sString, "A" );
			rx = new Regex( "[ÈÉÊËĒĔĖĘĚƏẸẺẼẾỀỂỄỆ]" );
			s = rx.Replace( s, "E" );
			rx = new Regex( "[ÌÍÎÏĪĬĮİǏỈỊ]" );
			s = rx.Replace( s, "I" );
			rx = new Regex( "[ÒÓÔÕÖØŌŎŐǑǾỌỎỐỒỔỖỘỚỜỞỠỢ]" );
			s = rx.Replace( s, "O" );
			rx = new Regex( "[ÙÚÛÜŨŪŬŮŰŲƯǓǕǗǙǛỤỦỨỪỬỮỰ]" );
			s = rx.Replace( s, "U" );
			rx = new Regex( "[ÝŶŸỲỴỶỸ]" );
			s = rx.Replace( s, "Y" );
			rx = new Regex( "[ÇĆĈĊČŒƠǑ]" );
			s = rx.Replace( s, "C" );
			rx = new Regex( "[ÐĎĐ]" );
			s = rx.Replace( s, "D" );
			rx = new Regex( "[ÑŃŅŇ]" );
			s = rx.Replace( s, "N" );
			rx = new Regex( "[ß]" );
			s = rx.Replace( s, "B" );
			rx = new Regex( "[Þ]" );
			s = rx.Replace( s, "P" );
			rx = new Regex( "[ĜĞĠĢ]" );
			s = rx.Replace( s, "G" );
			rx = new Regex( "[ĤĦ]" );
			s = rx.Replace( s, "H" );
			rx = new Regex( "[Ĵ]" );
			s = rx.Replace( s, "J" );
			rx = new Regex( "[Ķ]" );
			s = rx.Replace( s, "K" );
			rx = new Regex( "[ĹĻĽĿŁ]" );
			s = rx.Replace( s, "L" );
			rx = new Regex( "[ŔŖŘ]" );
			s = rx.Replace( s, "R" );
			rx = new Regex( "[ŚŜŞŠ]" );
			s = rx.Replace( s, "S" );
			rx = new Regex( "[ŢŤŦ]" );
			s = rx.Replace( s, "T" );
			rx = new Regex( "[ŴẀẂẄ]" );
			s = rx.Replace( s, "W" );
			rx = new Regex( "[ŹŻŽ]" );
			s = rx.Replace( s, "Z" );
			
			rx = new Regex( "[àáâãäåæāăąǎǻǽạảấầẩẫậắằẳẵặ]" );
			s = rx.Replace( s, "a" );
			rx = new Regex( "[èéêëēĕėęěəẹẻếềểễệ]" );
			s = rx.Replace( s, "e" );
			rx = new Regex( "[ìíîïĩīĭįıǐỉị]" );
			s = rx.Replace( s, "i" );
			rx = new Regex( "[òóôõöøōŏőơǒǒǿọỏốồổỗộớờởỡợ]" );
			s = rx.Replace( s, "o" );
			rx = new Regex( "[ùúûüũūŭůűųưǔǖǘǚǜụủứừửữự]" );
			s = rx.Replace( s, "u" );
			rx = new Regex( "[ýÿŷỳỵỹ]" );
			s = rx.Replace( s, "y" );
			rx = new Regex( "[çćĉċčœ]" );
			s = rx.Replace( s, "c" );
			rx = new Regex( "[ðďďđ]" );
			s = rx.Replace( s, "d" );
			rx = new Regex( "[ñńņňŉ]" );
			s = rx.Replace( s, "n" );
			rx = new Regex( "[þ]" );
			s = rx.Replace( s, "p" );
			rx = new Regex( "[ĝğġģ]" );
			s = rx.Replace( s, "g" );
			rx = new Regex( "[ĥħ]" );
			s = rx.Replace( s, "h" );
			rx = new Regex( "[ĵ]" );
			s = rx.Replace( s, "j" );
			rx = new Regex( "[ķĸ]" );
			s = rx.Replace( s, "k" );
			rx = new Regex( "[ĺļľŀł]" );
			s = rx.Replace( s, "l" );
			rx = new Regex( "[ŕŗř]" );
			s = rx.Replace( s, "r" );
			rx = new Regex( "[śŝşš]" );
			s = rx.Replace( s, "s" );
			rx = new Regex( "[ţťŧ]" );
			s = rx.Replace( s, "t" );
			rx = new Regex( "[ŵẁẃẅ]" );
			s = rx.Replace( s, "w" );
			rx = new Regex( "[źżž]" );
			s = rx.Replace( s, "z" );
			
			// древняя кирилица
			rx = new Regex( "[ҐҒЃ]" );
			s = rx.Replace( s, "Г" );
			rx = new Regex( "[Җ]" );
			s = rx.Replace( s, "Ж" );
			rx = new Regex( "[ҚҜЌ]" );
			s = rx.Replace( s, "К" );
			rx = new Regex( "[Ң]" );
			s = rx.Replace( s, "Н" );
			rx = new Regex( "[ҮҰ]" );
			s = rx.Replace( s, "Y" );
			rx = new Regex( "[Ҳ]" );
			s = rx.Replace( s, "Х" );
			rx = new Regex( "[Ҹ]" );
			s = rx.Replace( s, "Ч" );
			rx = new Regex( "[ӘЄ]" );
			s = rx.Replace( s, "Э" );
			rx = new Regex( "[Ө]" );
			s = rx.Replace( s, "О" );
			rx = new Regex( "[Ѕ]" );
			s = rx.Replace( s, "S" );
			rx = new Regex( "[ІЇ]" );
			s = rx.Replace( s, "I" );
			rx = new Regex( "[Ј]" );
			s = rx.Replace( s, "J" );
			rx = new Regex( "[Љ]" );
			s = rx.Replace( s, "ЛЬ" );
			rx = new Regex( "[Њ]" );
			s = rx.Replace( s, "НЬ" );
			rx = new Regex( "[Ћ]" );
			s = rx.Replace( s, "h" );
			rx = new Regex( "[Ў]" );
			s = rx.Replace( s, "У" );
			rx = new Regex( "[Џ]" );
			s = rx.Replace( s, "Ц" );
			
			rx = new Regex( "[ўүұ]" );
			s = rx.Replace( s, "у" );
			rx = new Regex( "[җ]" );
			s = rx.Replace( s, "ж" );
			rx = new Regex( "[ѓґғ]" );
			s = rx.Replace( s, "г" );
			rx = new Regex( "[є]" );
			s = rx.Replace( s, "э" );
			rx = new Regex( "[ђћҺһ]" );
			s = rx.Replace( s, "h" );
			rx = new Regex( "[ѕ]" );
			s = rx.Replace( s, "s" );
			rx = new Regex( "[ії]" );
			s = rx.Replace( s, "i" );
			rx = new Regex( "[ј]" );
			s = rx.Replace( s, "j" );
			rx = new Regex( "[њ]" );
			s = rx.Replace( s, "л" );
			rx = new Regex( "[њ]" );
			s = rx.Replace( s, "н" );
			rx = new Regex( "[ќқҝ]" );
			s = rx.Replace( s, "к" );
			rx = new Regex( "[џ]" );
			s = rx.Replace( s, "ц" );
			rx = new Regex( "[ң]" );
			s = rx.Replace( s, "н" );
			rx = new Regex( "[ҳ]" );
			s = rx.Replace( s, "х" );
			rx = new Regex( "[ҹ]" );
			s = rx.Replace( s, "ч" );
			rx = new Regex( "[ә]" );
			s = rx.Replace( s, "э" );
			rx = new Regex( "[ө]" );
			s = rx.Replace( s, "о" );
			
			return s;
		}
		
		// "строгое" значение строки пути к файлу
		public static string StrictPath(string sPath) {
			if (string.IsNullOrEmpty(sPath))
				return sPath;
			
			// "строгое" значение Юникодной строки - псевдо-транслитерация
			string s = TranslateUnicodeString(sPath);
			
			// замена двойных кавычек " на елочки вокруг русских слов
			s = Regex.Replace(
				s, "(?'letter'[а-яА-ЯёЁ]\\b)\"", "${letter}»",
				RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			s = Regex.Replace(
				s, "\"(?'letter'\\b[а-яА-ЯёЁ])", "«${letter}",
				RegexOptions.IgnoreCase | RegexOptions.Multiline
			);

			// замена двойных кавычек " на две одинарные кавычки ' вокруг латинских слов
			s = Regex.Replace(
				s, "(?'letter'[a-zA-Z]\\b)\"", "${letter}''",
				RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			s = Regex.Replace(
				s, "\"(?'letter'\\b[a-zA-Z])", "''${letter}",
				RegexOptions.IgnoreCase | RegexOptions.Multiline
			);

			// неразрывный пробел зпменяем на простой
			s = s.Replace(' ', ' ');

			string sStrict = string.Empty;
			for (int i = 0; i != s.Length; ++i) {
				int nInd = _StrictLetters.IndexOf(s[i]);
				if (nInd != -1) {
					sStrict += s[i];
				}
			}
			return sStrict;
		}
		
		// только корректные символы для имен файлов
		public static string OnlyCorrectSymbolsForString( string sString ) {
			string s = sString;
			if ( string.IsNullOrWhiteSpace( sString ) )
				return sString;

			const string sBad = "*/|?\\<>\"&:\t\r\n";
			string sCorrect = string.Empty;
			for ( int i = 0; i != s.Length; ++i ) {
				int nInd = sBad.IndexOf( s[i] );
				if ( nInd == -1 ) {
					sCorrect += s[i];
				} else {
					if ( s[i]=='\t' ) {
						sCorrect += " ";
					} else if ( s[i]=='\r' || s[i]=='\r' ) {
						
					} else {
						sCorrect += "_";
					}
				}
			}
			return sCorrect;
		}
		
		// только корректные символы для путей файлов
		public static string OnlyCorrectSymbolsForPath( string sString ) {
			string s = sString;
			if ( string.IsNullOrWhiteSpace( sString ) )
				return sString;

			const string sBad = "*/|?<>\"&:\t\r\n";
			string sCorrect = string.Empty;
			for ( int i = 0; i != s.Length; ++i ) {
				int nInd = sBad.IndexOf( s[i] );
				if( nInd == -1 ) {
					sCorrect += s[i];
				} else {
					if ( s[i]=='\t' ) {
						sCorrect += " ";
					} else if ( s[i]=='\r' || s[i]=='\r' ) {
						
					} else {
						sCorrect += "_";
					}
				}
			}
			return sCorrect;
		}
		
		public static string MakeGeneralWorkedString( string sString, int RegisterMode, int SpaceProcessMode, bool StrictMode, bool TranslitMode )
		{
			string s = string.Empty;
			// регистр
			s = RegisterString( sString, RegisterMode );
			// пробелы
			s = SpaceString( s, SpaceProcessMode );
			// "строгие" символы
			s = StrictMode ? StrictString( s ) : OnlyCorrectSymbolsForString( s );
			// транслитерация
			if ( TranslitMode )
				s = TransliterationString( s );
			return s;
		}
		
		public static string MakeGeneralWorkedPath( string sFB2FilePath, int RegisterMode, int SpaceProcessMode, bool StrictMode, bool TranslitMode ) {
			string s = string.Empty;
			// регистр
			s = RegisterString( sFB2FilePath, RegisterMode );
			// пробелы
			s = SpaceString( s, SpaceProcessMode );
			// транслитерация
			if ( TranslitMode ) {
				s = TransliterationString( s );
			}
			// "строгие" символы
			return StrictMode ? StrictPath( s ) : OnlyCorrectSymbolsForPath( s );
		}
		
		// если в строке sNumber - число (1, 01, 34 ...), то возвращается true, если нет (0x3, 2v ...) - false
		public static bool IsNumberInString( string sNumber ) {
			try {
				Convert.ToInt32( sNumber );
			} catch {
				return false;
			}
			return true;
		}

		public static string makeIINumber( int Number ) {
			// число, смотрим, сколько цифр и добавляем слева нужное число 0.
			if( Number >= 0 && Number < 10 )
				return "0" + Convert.ToString( Number );
			else
				return Convert.ToString( Number ); // число символов >= 2
		}
		
		// удаление последнего символа, если он соответствует Шаблону
		public static string trimLastTemplateSymbol( string Text, char Template ) {
			if ( !string.IsNullOrEmpty( Text ) ) {
				if ( Text.Substring( Text.Length-1, 1 ) == Template.ToString() )
					Text = Text.Substring( 0, Text.Length-1 );
			}
			return Text;
		}
		
		// удаление последнего символа, если он соответствует массиву Шаблонных символов
		public static string trimLastTemplateSymbol( string Text, Char [] Template ) {
			foreach ( char symbol in Template )
				Text = trimLastTemplateSymbol( Text, symbol );
			return Text;
		}
		
		#endregion
		
		#region Поиск одинаковых строк в списке List
		private static string m_sForFind = string.Empty; // для предиката поска в списке
		private static bool IsFileNameExsist( String s ) {
			// предикат для поиска в List всех одинаковых фафлов m_sForFind
			return ( s == m_sForFind ) ? false : true;
		}
		// предикат для поиска в List lFilesList всех одинаковых фафлов sFileName
		public static List<string> GetFilesWithNames( List<string> lFilesList, string sFileName ) {
			m_sForFind = sFileName;
			return lFilesList.FindAll( IsFileNameExsist );
		}
		// число одинаковыъх файлов sFileName в списке lFilesList
		public static long GetFilesCount( List<string> lFilesList, string sFileName ) {
			return GetFilesWithNames( lFilesList, sFileName ).Count;
		}
		// удаление всех тэгов из стоки
		public static string getDeleteAllTags( string sText ) {
			Regex rx = new Regex( "</p>" );
			string s = rx.Replace( sText, "</p>\r\n" );
			rx = new Regex( "<empty-line[^>]*>" );
			s = rx.Replace( s, "\r\n" );
			rx = new Regex( "<[^>]*>" );
			return rx.Replace( s, string.Empty );
		}
		#endregion
		
		#region Разное
		// генерация строки из элементов списка
		public static string makeStringFromListItems( IList<string> list ) {
			string StringFromListItems = string.Empty;
			if ( list != null ) {
				foreach ( string s in list )
					StringFromListItems += s + "; ";
				StringFromListItems = StringFromListItems.Trim();
				if ( StringFromListItems.Substring( StringFromListItems.Length-1, 1 ) == ";" )
					StringFromListItems = StringFromListItems.Substring( 0, StringFromListItems.Length-1 );
			}
			return StringFromListItems;
		}
		// формирование строки из номера по Шаблону 00X
		public static string makeNNNStringOfNumber( int Number ) {
			// число, смотрим, сколько цифр и добавляем слева нужное число 0.
			if ( Number > 0 && Number <= 9 )
				return "00" + Number.ToString();
			else if ( Number >= 10 && Number <= 99 )
				return "0" + Number.ToString();
			else
				return Number.ToString(); // число символов >= 3
		}
		#endregion
	}
}
