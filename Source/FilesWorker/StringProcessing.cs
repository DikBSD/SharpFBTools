/*
 * Created by SharpDevelop.
 * User: Êóçíåöîâ Âàäèì [DikBSD]
 * Date: 24.04.2009
 * Time: 10:26
 * 
 * License: GPL 2.1
 */
using System;

namespace FilesWorker
{
	/// <summary>
	/// Description of StringProcessing.
	/// </summary>
	public class StringProcessing
	{
		#region Çàêğûòûå âñïîìîãàòåëüíûå ìåòîäû êëàññà
		private static string[] MakeTranslitLettersArray() {
			// ìàññèâ çàìåíû ğóññêèõ ñèìâîëîâ ëàòèíñêèìè
			#region Êîä
			string[] saTranslitLetters = new string[152];
			saTranslitLetters[0]="a";	// à
			saTranslitLetters[1]="b";	// á
			saTranslitLetters[2]="v";	// â
			saTranslitLetters[3]="g";	// ã
			saTranslitLetters[4]="d";	// ä
			saTranslitLetters[5]="e";	// å
			saTranslitLetters[6]="yo";	// ¸
			saTranslitLetters[7]="zh";	// æ
			saTranslitLetters[8]="z";	// ç
			saTranslitLetters[9]="i";	// è
			saTranslitLetters[10]="y";	// é
			saTranslitLetters[11]="k";	// ê
			saTranslitLetters[12]="l";	// ë
			saTranslitLetters[13]="m";	// ì
			saTranslitLetters[14]="n";	// í
			saTranslitLetters[15]="o";	// î
			saTranslitLetters[16]="p";	// ï
			saTranslitLetters[17]="r";	// ğ
			saTranslitLetters[18]="s";	// ñ
			saTranslitLetters[19]="t";	// ò
			saTranslitLetters[20]="u";	// ó
			saTranslitLetters[21]="f";	// ô
			saTranslitLetters[22]="h";	// õ
			saTranslitLetters[23]="ts";	// ö
			saTranslitLetters[24]="ch";	// ÷
			saTranslitLetters[25]="sh";	// ø
			saTranslitLetters[26]="sch";// ù
			saTranslitLetters[27]="'";	// ú
			saTranslitLetters[28]="y";	// û
			saTranslitLetters[29]="'";	// ü
			saTranslitLetters[30]="e";	// ı
			saTranslitLetters[31]="yu";	// ş
			saTranslitLetters[32]="ya";	// ÿ
			saTranslitLetters[33]="A";	// À
			saTranslitLetters[34]="B";	// Á
			saTranslitLetters[35]="V";	// Â
			saTranslitLetters[36]="G";	// Ã
			saTranslitLetters[37]="D";	// Ä
			saTranslitLetters[38]="E";	// Å
			saTranslitLetters[39]="YO";	// ¨
			saTranslitLetters[40]="ZH";	// Æ
			saTranslitLetters[41]="Z";	// Ç
			saTranslitLetters[42]="I";	// È
			saTranslitLetters[43]="Y";	// É
			saTranslitLetters[44]="K";	// Ê
			saTranslitLetters[45]="L";	// Ë
			saTranslitLetters[46]="M";	// Ì
			saTranslitLetters[47]="N";	// Í
			saTranslitLetters[48]="O";	// Î
			saTranslitLetters[49]="P";	// Ï
			saTranslitLetters[50]="R";	// Ğ
			saTranslitLetters[51]="S";	// Ñ
			saTranslitLetters[52]="T";	// Ò
			saTranslitLetters[53]="U";	// Ó
			saTranslitLetters[54]="F";	// Ô
			saTranslitLetters[55]="H";	// Õ
			saTranslitLetters[56]="TS";	// Ö
			saTranslitLetters[57]="CH";	// ×
			saTranslitLetters[58]="SH";	// Ø
			saTranslitLetters[59]="SCH";// Ù
			saTranslitLetters[60]="'";	// Ú
			saTranslitLetters[61]="Y";	// Û
			saTranslitLetters[62]="'";	// Ü
			saTranslitLetters[63]="E";	// İ
			saTranslitLetters[64]="YU";	// Ş
			saTranslitLetters[65]="YA";	// ß
			
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
			saTranslitLetters[135]="¹";
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
			
			return saTranslitLetters;
			#endregion
		}
		#endregion
		
		public StringProcessing()
		{
		}
		
		#region Îòêğûòûå ìåòîäû êëàññà
		public static string TransliterationString( string sString ) {
			// òğàíñëèòåğàöèÿ ñòğîêè
			string sStr = sString;
			if( sString.Trim()=="" ) {
				return sString;
			}
			const string sTemplate = "àáâãäå¸æçèéêëìíîïğñòóôõö÷øùúûüışÿÀÁÂÃÄÅ¨ÆÇÈÉÊËÌÍÎÏĞÑÒÓÔÕÖ×ØÙÚÛÜİŞßabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 `~'!@#¹$%^[](){}-+=_;.,";
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
			// "ñòğîãîå" çíà÷åíèå ñòğîêè
			string s = sString;
			if( sString.Trim()=="" ) {
				return sString;
			}
			const string sStrictLetters = "àáâãäå¸æçèéêëìíîïğñòóôõö÷øùúûüışÿÀÁÂÃÄÅ¨ÆÇÈÉÊËÌÍÎÏĞÑÒÓÔÕÖ×ØÙÚÛÜİŞßabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 [](){}-_";
			string sStrict = "";
			for( int i=0; i!=s.Length; ++i ) {
				int nInd = sStrictLetters.IndexOf( s[i] );
				if( nInd!=-1 ) {
					sStrict += s[i];
				}
			}
			return sStrict;
		}
		
		public static string SpaceString( string sString, int nMode ) {
			// îáğàáîòêà ïğîáåëîâ â ñòğîêå
			if( sString.Trim()=="" ) {
				return "";
			}
			string s = "";
			for( int i=0; i!=sString.Length; ++i ) {
				if( sString[i]==' ' ) {
					switch( nMode ) {
						case 0: // îñòàâèòü ïğîáåë
							s += sString[i];
							break;
						case 1: // óäàëèòü ïğîáåë
							break;
						case 2: // çàìåíèòü ïğîáåë íà '_'
							s += '_';
							break;
						case 3: // çàìåíèòü ïğîáåë íà '-'
							s += '-';
							break;
						case 4: // çàìåíèòü ïğîáåë íà '+'
							s += '+';
							break;
						case 5: // çàìåíèòü ïğîáåë íà '='
							s += '=';
							break;
					}
				} else {
					s += sString[i];
				}
				
			}
			return s;
		}
		
		public static string RegisterString( string sString, int nMode ) {
			// çàäàíèå ğåãèñòğà ñòğîêå
			if( sString.Trim()=="" ) {
				return "";
			}
			switch( nMode ) {
				case 0: // Êàê åñòü
					return sString;
				case 1: // íèæíèé ğåãèñòğ
					return sString.ToLower();
				case 2: // âåğõíèé ğåãèñòğ
					return sString.ToUpper();
				default:
					return sString;
			}
		}
		
		#endregion
	}
}
