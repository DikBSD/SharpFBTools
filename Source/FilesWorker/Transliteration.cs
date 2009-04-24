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
	/// Description of Transliteration.
	/// </summary>
	public class Transliteration
	{
		#region Çàêğûòûå äàííûå êëàññà
		private const string m_sTemplate = "àáâãäå¸æçèéêëìíîïğñòóôõö÷øùúûüışÿÀÁÂÃÄÅ¨ÆÇÈÉÊËÌÍÎÏĞÑÒÓÔÕÖ×ØÙÚÛÜİŞßabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 `~'!@#¹$%^[](){}-+=_;.,\\/:";
		private static string[] m_saTranslitLetters = MakeTranslitLettersArray();
		#endregion
		
		#region Çàêğûòûå âñïîìîãàòåëüíûå ìåòîäû êëàññà
		private static string[] MakeTranslitLettersArray() {
			// ìàññèâ çàìåíû ğóññêèõ ñèìâîëîâ ëàòèíñêèìè
			#region Êîä
			string[] m_saTranslitLetters = new string[155];
			m_saTranslitLetters[0]="a";		// à
			m_saTranslitLetters[1]="b";		// á
			m_saTranslitLetters[2]="v";		// â
			m_saTranslitLetters[3]="g";		// ã
			m_saTranslitLetters[4]="d";		// ä
			m_saTranslitLetters[5]="e";		// å
			m_saTranslitLetters[6]="yo";	// ¸
			m_saTranslitLetters[7]="zh";	// æ
			m_saTranslitLetters[8]="z";		// ç
			m_saTranslitLetters[9]="i";		// è
			m_saTranslitLetters[10]="y";	// é
			m_saTranslitLetters[11]="k";	// ê
			m_saTranslitLetters[12]="l";	// ë
			m_saTranslitLetters[13]="m";	// ì
			m_saTranslitLetters[14]="n";	// í
			m_saTranslitLetters[15]="o";	// î
			m_saTranslitLetters[16]="p";	// ï
			m_saTranslitLetters[17]="r";	// ğ
			m_saTranslitLetters[18]="s";	// ñ
			m_saTranslitLetters[19]="t";	// ò
			m_saTranslitLetters[20]="u";	// ó
			m_saTranslitLetters[21]="f";	// ô
			m_saTranslitLetters[22]="h";	// õ
			m_saTranslitLetters[23]="ts";	// ö
			m_saTranslitLetters[24]="ch";	// ÷
			m_saTranslitLetters[25]="sh";	// ø
			m_saTranslitLetters[26]="sch";	// ù
			m_saTranslitLetters[27]="'";	// ú
			m_saTranslitLetters[28]="y";	// û
			m_saTranslitLetters[29]="'";	// ü
			m_saTranslitLetters[30]="e";	// ı
			m_saTranslitLetters[31]="yu";	// ş
			m_saTranslitLetters[32]="ya";	// ÿ
			m_saTranslitLetters[33]="A";	// À
			m_saTranslitLetters[34]="B";	// Á
			m_saTranslitLetters[35]="V";	// Â
			m_saTranslitLetters[36]="G";	// Ã
			m_saTranslitLetters[37]="D";	// Ä
			m_saTranslitLetters[38]="E";	// Å
			m_saTranslitLetters[39]="YO";	// ¨
			m_saTranslitLetters[40]="ZH";	// Æ
			m_saTranslitLetters[41]="Z";	// Ç
			m_saTranslitLetters[42]="I";	// È
			m_saTranslitLetters[43]="Y";	// É
			m_saTranslitLetters[44]="K";	// Ê
			m_saTranslitLetters[45]="L";	// Ë
			m_saTranslitLetters[46]="M";	// Ì
			m_saTranslitLetters[47]="N";	// Í
			m_saTranslitLetters[48]="O";	// Î
			m_saTranslitLetters[49]="P";	// Ï
			m_saTranslitLetters[50]="R";	// Ğ
			m_saTranslitLetters[51]="S";	// Ñ
			m_saTranslitLetters[52]="T";	// Ò
			m_saTranslitLetters[53]="U";	// Ó
			m_saTranslitLetters[54]="F";	// Ô
			m_saTranslitLetters[55]="H";	// Õ
			m_saTranslitLetters[56]="TS";	// Ö
			m_saTranslitLetters[57]="CH";	// ×
			m_saTranslitLetters[58]="SH";	// Ø
			m_saTranslitLetters[59]="SCH";	// Ù
			m_saTranslitLetters[60]="'";	// Ú
			m_saTranslitLetters[61]="Y";	// Û
			m_saTranslitLetters[62]="'";	// Ü
			m_saTranslitLetters[63]="E";	// İ
			m_saTranslitLetters[64]="YU";	// Ş
			m_saTranslitLetters[65]="YA";	// ß
			
			m_saTranslitLetters[66]="a";
			m_saTranslitLetters[67]="b";
			m_saTranslitLetters[68]="c";
			m_saTranslitLetters[69]="d";
			m_saTranslitLetters[70]="e";
			m_saTranslitLetters[71]="f";
			m_saTranslitLetters[72]="g";
			m_saTranslitLetters[73]="h";
			m_saTranslitLetters[74]="i";
			m_saTranslitLetters[75]="j";
			m_saTranslitLetters[76]="k";
			m_saTranslitLetters[77]="l";
			m_saTranslitLetters[78]="m";
			m_saTranslitLetters[79]="n";
			m_saTranslitLetters[80]="o";
			m_saTranslitLetters[81]="p";
			m_saTranslitLetters[82]="q";
			m_saTranslitLetters[83]="r";
			m_saTranslitLetters[84]="s";
			m_saTranslitLetters[85]="t";
			m_saTranslitLetters[86]="u";
			m_saTranslitLetters[87]="v";
			m_saTranslitLetters[88]="w";
			m_saTranslitLetters[89]="x";
			m_saTranslitLetters[90]="y";
			m_saTranslitLetters[91]="z";
			m_saTranslitLetters[92]="A";
			m_saTranslitLetters[93]="B";
			m_saTranslitLetters[94]="C";
			m_saTranslitLetters[95]="D";
			m_saTranslitLetters[96]="E";
			m_saTranslitLetters[97]="F";
			m_saTranslitLetters[98]="G";
			m_saTranslitLetters[99]="H";
			m_saTranslitLetters[100]="I";
			m_saTranslitLetters[101]="J";
			m_saTranslitLetters[102]="K";
			m_saTranslitLetters[103]="L";
			m_saTranslitLetters[104]="M";
			m_saTranslitLetters[105]="N";
			m_saTranslitLetters[106]="O";
			m_saTranslitLetters[107]="P";
			m_saTranslitLetters[108]="Q";
			m_saTranslitLetters[109]="R";
			m_saTranslitLetters[110]="S";
			m_saTranslitLetters[111]="T";
			m_saTranslitLetters[112]="U";
			m_saTranslitLetters[113]="V";
			m_saTranslitLetters[114]="W";
			m_saTranslitLetters[115]="X";
			m_saTranslitLetters[116]="Y";
			m_saTranslitLetters[117]="Z";
			m_saTranslitLetters[118]="0";
			m_saTranslitLetters[119]="1";
			m_saTranslitLetters[120]="2";
			m_saTranslitLetters[121]="3";
			m_saTranslitLetters[122]="4";
			m_saTranslitLetters[123]="5";
			m_saTranslitLetters[124]="6";
			m_saTranslitLetters[125]="7";
			m_saTranslitLetters[126]="8";
			m_saTranslitLetters[127]="9";
			
			m_saTranslitLetters[128]=" ";
			m_saTranslitLetters[129]="`";
			m_saTranslitLetters[130]="~";
			m_saTranslitLetters[131]="'";
			m_saTranslitLetters[132]="!";
			m_saTranslitLetters[133]="@";
			m_saTranslitLetters[134]="#";
			m_saTranslitLetters[135]="¹";
			m_saTranslitLetters[136]="$";
			m_saTranslitLetters[137]="%";
			m_saTranslitLetters[138]="^";
			m_saTranslitLetters[139]=".";
			m_saTranslitLetters[140]=",";
			m_saTranslitLetters[141]=";";
			m_saTranslitLetters[142]="_";
			m_saTranslitLetters[143]="=";
			m_saTranslitLetters[144]="+";
			m_saTranslitLetters[145]="-";
			m_saTranslitLetters[146]="(";
			m_saTranslitLetters[147]=")";
			m_saTranslitLetters[148]="{";
			m_saTranslitLetters[149]="}";
			m_saTranslitLetters[150]="[";
			m_saTranslitLetters[151]="]";
			
			m_saTranslitLetters[152]="\\";
			m_saTranslitLetters[153]="/";
			m_saTranslitLetters[154]=":";
			
			return m_saTranslitLetters;
			#endregion
		}
		#endregion
		
		public Transliteration()
		{
			MakeTranslitLettersArray();
		}
		
		#region Îòêğûòûå ìåòîäû êëàññà
		public static string TransliterationString( string sString ) {
			// "ñòğîãîå" çíà÷åíèå ñòğîêè
			string sStr = sString;
			if( sString.Trim()=="" ) {
				return sString;
			}
			string sTranslit = "";
			for( int i=0; i!=sStr.Length; ++i ) {
				char ci = sStr[i];
				for( int j=0; j!=m_sTemplate.Length; ++j ) {
					char cj = m_sTemplate[j];
					if( ci==cj ) {
						string cc = m_saTranslitLetters[j];
						sTranslit += m_saTranslitLetters[j];
						break;
					}
				}
			}
			return sTranslit;
		}
		#endregion
	}
}
