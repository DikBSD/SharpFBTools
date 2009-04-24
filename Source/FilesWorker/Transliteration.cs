/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим [DikBSD]
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
		#region Закрытые данные класса
		private const string m_sRusLetters = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ";
		private string[] m_saLatinLetters = new string[66];
		#endregion
		
		private void MakeEnglishArray() {
			// массив замены русских символов латинскими
			m_saLatinLetters[0]="a";	// а
			m_saLatinLetters[1]="b";	// б
			m_saLatinLetters[2]="v";	// в
			m_saLatinLetters[3]="g";	// г
			m_saLatinLetters[4]="d";	// д
			m_saLatinLetters[5]="e";	// е
			m_saLatinLetters[6]="ie";	// ё
			m_saLatinLetters[7]="j";	// ж
			m_saLatinLetters[8]="z";	// з
			m_saLatinLetters[9]="i";	// и
			m_saLatinLetters[10]="i";	// й
			m_saLatinLetters[11]="k";	// к
			m_saLatinLetters[12]="l";	// л
			m_saLatinLetters[13]="m";	// м
			m_saLatinLetters[14]="n";	// н
			m_saLatinLetters[15]="o";	// о
			m_saLatinLetters[16]="p";	// п
			m_saLatinLetters[17]="r";	// р
			m_saLatinLetters[18]="s";	// с
			m_saLatinLetters[19]="t";	// т
			m_saLatinLetters[20]="u";	// у
			m_saLatinLetters[21]="f";	// ф
			m_saLatinLetters[22]="h";	// х
			m_saLatinLetters[23]="ts";	// ц
			m_saLatinLetters[24]="ch";	// ч
			m_saLatinLetters[25]="sh";	// ш
			m_saLatinLetters[26]="sch";	// щ
			m_saLatinLetters[27]="";	// ъ
			m_saLatinLetters[28]="y";	// ы
			m_saLatinLetters[29]="";	// ь
			m_saLatinLetters[30]="e";	// э
			m_saLatinLetters[31]="iu";	// ю
			m_saLatinLetters[32]="ia";	// я
			m_saLatinLetters[33]="A";	// А
			m_saLatinLetters[34]="B";	// Б
			m_saLatinLetters[35]="V";	// В
			m_saLatinLetters[36]="G";	// Г
			m_saLatinLetters[37]="D";	// Д
			m_saLatinLetters[38]="E";	// Е
			m_saLatinLetters[39]="YE";	// Ё
			m_saLatinLetters[40]="J";	// Ж
			m_saLatinLetters[41]="Z";	// З
			m_saLatinLetters[42]="I";	// И
			m_saLatinLetters[43]="I";	// Й
			m_saLatinLetters[44]="K";	// К
			m_saLatinLetters[45]="L";	// Л
			m_saLatinLetters[46]="M";	// М
			m_saLatinLetters[47]="N";	// Н
			m_saLatinLetters[48]="O";	// О
			m_saLatinLetters[49]="P";	// П
			m_saLatinLetters[50]="R";	// Р
			m_saLatinLetters[51]="S";	// С
			m_saLatinLetters[52]="T";	// Т
			m_saLatinLetters[53]="U";	// У
			m_saLatinLetters[54]="F";	// Ф
			m_saLatinLetters[55]="H";	// Х
			m_saLatinLetters[56]="TS";	// Ц
			m_saLatinLetters[57]="CH";	// Ч
			m_saLatinLetters[58]="SH";	// Ш
			m_saLatinLetters[59]="SCH";	// Щ
			m_saLatinLetters[60]="";	// Ъ
			m_saLatinLetters[61]="Y";	// Ы
			m_saLatinLetters[62]="";	// Ь
			m_saLatinLetters[63]="E";	// Э
			m_saLatinLetters[64]="YU";	// Ю
			m_saLatinLetters[65]="YA";	// Я
		}
		public Transliteration()
		{
			MakeEnglishArray();
		}
	}
}
