/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим [DikBSD]
 * Date: 24.04.2009
 * Time: 10:26
 * 
 * License: GPL 2.1
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Text;

using Core.FB2.FB2Parsers;
using Core.FB2.Description.DocumentInfo;

using fB2Parser	= Core.FB2.FB2Parsers.FB2Parser;

namespace Core.StringProcessing
{
	/// <summary>
	/// Description of StringProcessing.
	/// </summary>
	public class StringProcessing
	{
		private static ulong m_ulDateCount = 0;
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
		public static string GetArchiveExt( string sArchiveType ) {
			string sExt = "";
			switch( sArchiveType ) {
				case "Rar":
					sExt = "rar";
					break;
				case "Zip":
					sExt = "zip";
					break;
				case "7z":
					sExt = "7z";
					break;
				case "BZip2":
					sExt = "bz2";
					break;
				case "GZip":
					sExt = "gz";
					break;
				case "Tar":
					sExt = "tar";
					break;
			}
			return sExt;
		}
		
		public static string GetDateTimeExt()
		{
			++m_ulDateCount;
			DateTime dt = DateTime.Now;
			return	dt.Year.ToString()+"-"+dt.Month.ToString()+"-"+dt.Day.ToString()+"-"+
				dt.Hour.ToString()+"-"+dt.Minute.ToString()+"-"+dt.Second.ToString()+"-"+
				Convert.ToString( m_ulDateCount );
		}
		
		public static string RemoveComaBeforeSlash( string sFilePath ){
			// обработка последней "." перед \
			string [] sSlash = sFilePath.Split('\\');
			sFilePath = "";
			foreach( string sI in sSlash ) {
				string sNew = "";
				if( sI.Substring( sI.Length-1, 1  ) == "." ) {
					sNew = sI.Remove( sI.Length-1, 1  );
				} else {
					sNew = sI;
				}
				sFilePath += sNew+"\\";
			}
			return sFilePath = sFilePath.Remove( sFilePath.Length-1, 1 );
		}
		
		public static long GetFileNewNumber( string sFilePath ) {
			// номер для нового файла, если уже есть несколько таких же
			Regex rx = new Regex( @"\\+" );
			sFilePath = rx.Replace( sFilePath, "\\" );
			
			string [] files = Directory.GetFiles( Path.GetDirectoryName( sFilePath ) );
			string sFilePathLower = sFilePath.ToLower();
			
			// обработка последней "." перед \
			sFilePathLower = RemoveComaBeforeSlash( sFilePathLower );

			string sFilePathNotExtLower = "";
			
			if( sFilePathLower.IndexOf( ".fb2" )!=-1 ) {
				sFilePathNotExtLower = sFilePathLower.Substring( 0, sFilePathLower.IndexOf( ".fb2" ) );
			} else {
				sFilePathNotExtLower = sFilePathLower.Substring( 0, sFilePathLower.IndexOf(
																	Path.GetExtension( sFilePathLower ) ) );
			}
			string s = sFilePathLower.Substring( 0, sFilePathNotExtLower.Length );
			s = s.Replace( '.', '_' );
			
			long lCount = 0;
			foreach( string sFile in files ) {
				string sIter = sFile.ToLower().Replace( '.', '_' );
				if( sIter.IndexOf( s )!=-1) {
					++lCount;
				}
			}
			return lCount;
		}
			
		public static string GetBookID( string sFB2FilePath )
		{
			// возвращает либо _ID книги, либо _ID_Нет, если в книге нет тега ID
			Regex rx = new Regex( @"\\+" );
			sFB2FilePath = rx.Replace( sFB2FilePath, "\\" );
			
			fB2Parser fb2p = new fB2Parser( sFB2FilePath );
			DocumentInfo di = fb2p.GetDocumentInfo();
			
			string sID = OnlyCorrectSymbolsForString( di.ID );
			return ( sID != null ? sID : Settings.Settings.GetNoID() );
		}
		
		public static string GetFMBookID( string sFB2FilePath )
		{
			// возвращает либо _ID книги, либо _ID_Нет, если в книге нет тега ID (транслитерация и регистр при включенных опциях) - для Менеджера Файлов
			Regex rx = new Regex( @"\\+" );
			sFB2FilePath = rx.Replace( sFB2FilePath, "\\" );
			
			fB2Parser fb2p = new fB2Parser( sFB2FilePath );
			DocumentInfo di = fb2p.GetDocumentInfo();
			
			string sID = OnlyCorrectSymbolsForString( di.ID );
			return ( sID != null ? sID : GetGeneralWorkedString( Settings.Settings.GetNoID() ) );
		}

		public static string SpaceString( string sString, int nMode ) {
			// обработка пробелов в строке
			if( sString==null || sString.Length==0 ) {
				return sString;
			}
			string s = "";
			for( int i=0; i!=sString.Length; ++i ) {
				if( sString[i]==' ' ) {
					switch( nMode ) {
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
					}
				} else {
					s += sString[i];
				}
			}
			return s;
		}

		public static string ToUpperFirstLetterAnyWord( string sString, char c ) {
			// Все Слова Строки Начать с Большой Буквы
			if( sString.IndexOf( c ) !=-1 ) {
				StringBuilder sb = new StringBuilder();
				for( int i=0; i!=sString.Length; ++i ) {
					if( i >= sString.Length ) {
						break;
					} else {
						if( sString[i] != c ) {
							sb.Append( sString[i] );
						} else {
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
		
		public static string RegisterString( string sString, int nMode ) {
			// задание регистра строке
			if( sString==null || sString.Length==0 ) {
				return "";
			}
			switch( nMode ) {
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
		
		public static string TransliterationString( string sString ) {
			// транслитерация строки
			string sStr = sString;
			if( sString==null || sString.Length==0 ) {
				return sString;
			}
			const string sTemplate = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 `~'!@#№$%^[](){}-+=_;.,\\";
			string[] saTranslitLetters = MakeTranslitLettersArray();
			string sTranslit = "";
			for( int i=0; i!=sStr.Length; ++i ) {
				int nInd = sTemplate.IndexOf( sStr[i] );
				if( nInd!=-1 ) {
					sTranslit += saTranslitLetters[nInd];
				} else {
					sTranslit += "_";
				}
			}
			return sTranslit;
		}
		
		public static string StrictString( string sString ) {
			// "строгое" значение строки
			string s = sString;
			if( sString==null || sString.Length==0 ) {
				return sString;
			}
			const string sStrictLetters = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 [](){}-_";
			string sStrict = "";
			for( int i=0; i!=s.Length; ++i ) {
				int nInd = sStrictLetters.IndexOf( s[i] );
				if( nInd!=-1 ) {
					sStrict += s[i];
				}
			}
			return sStrict;
		}
		
		public static string StrictPath( string sPath ) {
			// "строгое" значение строки
			string s = sPath;
			if( sPath==null || sPath.Length==0 ) {
				return sPath;
			}
			const string sStrictLetters = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 \\[](){}-_";
			string sStrict = "";
			for( int i=0; i!=s.Length; ++i ) {
				int nInd = sStrictLetters.IndexOf( s[i] );
				if( nInd!=-1 ) {
					sStrict += s[i];
				}
			}
			return sStrict;
		}
		
		public static string OnlyCorrectSymbolsForString( string sString ) {
			// только корректные символы для имен файлов
			string s = sString;
			if( sString==null || sString.Length==0 ) {
				return sString;
			}
			const string sBad = "*/|?\\<>\"&:\t\r\n";
			string sCorrect = "";
			for( int i=0; i!=s.Length; ++i ) {
				int nInd = sBad.IndexOf( s[i] );
				if( nInd==-1 ) {
					sCorrect += s[i];
				} else {
					if( s[i]=='\t' ) {
						sCorrect += " ";
					} else if( s[i]=='\r' || s[i]=='\r' ) {
						
					} else {
						sCorrect += "_";
					}
				}
			}
			return sCorrect;
		}
		
		public static string OnlyCorrectSymbolsForPath( string sString ) {
			// только корректные символы для путей файлов
			string s = sString;
			if( sString==null || sString.Length==0 ) {
				return sString;
			}
			const string sBad = "*/|?<>\"&:\t\r\n";
			string sCorrect = "";
			for( int i=0; i!=s.Length; ++i ) {
				int nInd = sBad.IndexOf( s[i] );
				if( nInd==-1 ) {
					sCorrect += s[i];
				} else {
					if( s[i]=='\t' ) {
						sCorrect += " ";
					} else if( s[i]=='\r' || s[i]=='\r' ) {
						
					} else {
						sCorrect += "_";
					}
				}
			}
			return sCorrect;
		}
		
		public static string GetGeneralWorkedString( string sString )
		{
			string s = "";
			// регистр
			s = RegisterString( sString, Settings.SettingsFM.ReadRegisterMode() );
			// пробелы
			s = SpaceString( s, Settings.SettingsFM.ReadSpaceProcessMode() );
			// "строгие" символы
			if( Settings.SettingsFM.ReadStrictMode() ) {
				s = StrictString( s );
			} else {
				s = OnlyCorrectSymbolsForString( s );
			}
			// транслитерация
			if( Settings.SettingsFM.ReadTranslitMode() ) {
				s = TransliterationString( s );
			}
			return s;
		}
		
		public static string GetGeneralWorkedPath( string sFB2FilePath )
		{
			string s = "";
			// регистр
			s = RegisterString( sFB2FilePath, Settings.SettingsFM.ReadRegisterMode() );
			// пробелы
			s = SpaceString( s, Settings.SettingsFM.ReadSpaceProcessMode() );
			// транслитерация
			if( Settings.SettingsFM.ReadTranslitMode() ) {
				s = TransliterationString( s );
			}
			// "строгие" символы
			if( Settings.SettingsFM.ReadStrictMode() ) {
				s = StrictPath( s );
			} else {
				s = OnlyCorrectSymbolsForPath( s );
			}
			return s;
		}
		
		public static bool IsNumberInString( string sNumber ) {
			// если в строке sNumber - число (1, 01, 34 ...), то возвращается true, если нет (0x3, 2v ...) - false
			try {
				Convert.ToInt32( sNumber );
			} catch {
				return false;
			}
			return true;
		}
		#endregion
		
		#region Поиск одинаковых строк в списке List
		private static string m_sForFind = ""; // для предиката поска в списке
		private static bool IsFileNameExsist( String s ) {
			// предикат для поиска в List всех одинаковых фафлов m_sForFind
			return ( s == m_sForFind ) ? false : true;
        }
		public static List<string> GetFilesWithNames( List<string> lFilesList, string sFileName ) {
			// предикат для поиска в List lFilesList всех одинаковых фафлов sFileName
			m_sForFind = sFileName;
			return lFilesList.FindAll( IsFileNameExsist );
		}
		public static long GetFilesCount( List<string> lFilesList, string sFileName ) {
			// число одинаковыъх файлов sFileName в списке lFilesList
			return GetFilesWithNames( lFilesList, sFileName ).Count;
		}
		#endregion
	}
}
