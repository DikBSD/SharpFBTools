/*
 * Created by SharpDevelop.
 * User: �������� ����� [DikBSD]
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
		#region �������� ������ ������
		private const string m_sRusLetters = "�������������������������������������Ũ��������������������������";
		private string[] m_saLatinLetters = new string[66];
		#endregion
		
		private void MakeEnglishArray() {
			// ������ ������ ������� �������� ����������
			m_saLatinLetters[0]="a";	// �
			m_saLatinLetters[1]="b";	// �
			m_saLatinLetters[2]="v";	// �
			m_saLatinLetters[3]="g";	// �
			m_saLatinLetters[4]="d";	// �
			m_saLatinLetters[5]="e";	// �
			m_saLatinLetters[6]="ie";	// �
			m_saLatinLetters[7]="j";	// �
			m_saLatinLetters[8]="z";	// �
			m_saLatinLetters[9]="i";	// �
			m_saLatinLetters[10]="i";	// �
			m_saLatinLetters[11]="k";	// �
			m_saLatinLetters[12]="l";	// �
			m_saLatinLetters[13]="m";	// �
			m_saLatinLetters[14]="n";	// �
			m_saLatinLetters[15]="o";	// �
			m_saLatinLetters[16]="p";	// �
			m_saLatinLetters[17]="r";	// �
			m_saLatinLetters[18]="s";	// �
			m_saLatinLetters[19]="t";	// �
			m_saLatinLetters[20]="u";	// �
			m_saLatinLetters[21]="f";	// �
			m_saLatinLetters[22]="h";	// �
			m_saLatinLetters[23]="ts";	// �
			m_saLatinLetters[24]="ch";	// �
			m_saLatinLetters[25]="sh";	// �
			m_saLatinLetters[26]="sch";	// �
			m_saLatinLetters[27]="";	// �
			m_saLatinLetters[28]="y";	// �
			m_saLatinLetters[29]="";	// �
			m_saLatinLetters[30]="e";	// �
			m_saLatinLetters[31]="iu";	// �
			m_saLatinLetters[32]="ia";	// �
			m_saLatinLetters[33]="A";	// �
			m_saLatinLetters[34]="B";	// �
			m_saLatinLetters[35]="V";	// �
			m_saLatinLetters[36]="G";	// �
			m_saLatinLetters[37]="D";	// �
			m_saLatinLetters[38]="E";	// �
			m_saLatinLetters[39]="YE";	// �
			m_saLatinLetters[40]="J";	// �
			m_saLatinLetters[41]="Z";	// �
			m_saLatinLetters[42]="I";	// �
			m_saLatinLetters[43]="I";	// �
			m_saLatinLetters[44]="K";	// �
			m_saLatinLetters[45]="L";	// �
			m_saLatinLetters[46]="M";	// �
			m_saLatinLetters[47]="N";	// �
			m_saLatinLetters[48]="O";	// �
			m_saLatinLetters[49]="P";	// �
			m_saLatinLetters[50]="R";	// �
			m_saLatinLetters[51]="S";	// �
			m_saLatinLetters[52]="T";	// �
			m_saLatinLetters[53]="U";	// �
			m_saLatinLetters[54]="F";	// �
			m_saLatinLetters[55]="H";	// �
			m_saLatinLetters[56]="TS";	// �
			m_saLatinLetters[57]="CH";	// �
			m_saLatinLetters[58]="SH";	// �
			m_saLatinLetters[59]="SCH";	// �
			m_saLatinLetters[60]="";	// �
			m_saLatinLetters[61]="Y";	// �
			m_saLatinLetters[62]="";	// �
			m_saLatinLetters[63]="E";	// �
			m_saLatinLetters[64]="YU";	// �
			m_saLatinLetters[65]="YA";	// �
		}
		public Transliteration()
		{
			MakeEnglishArray();
		}
	}
}
