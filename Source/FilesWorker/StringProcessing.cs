/*
 * Created by SharpDevelop.
 * User: �������� ����� [DikBSD]
 * Date: 24.04.2009
 * Time: 10:26
 * 
 * License: GPL 2.1
 */
using System;
using System.IO;
using System.Collections.Generic;

using FB2.FB2Parsers;
using FB2.Description.DocumentInfo;

namespace FilesWorker
{
	/// <summary>
	/// Description of StringProcessing.
	/// </summary>
	public class StringProcessing
	{
		private static ulong m_ulDateCount = 0;
		#region �������� ��������������� ������ ������
		private static string[] MakeTranslitLettersArray() {
			// ������ ������ ������� �������� ����������
			#region ���
			string[] saTranslitLetters = new string[153];
			saTranslitLetters[0]="a";	// �
			saTranslitLetters[1]="b";	// �
			saTranslitLetters[2]="v";	// �
			saTranslitLetters[3]="g";	// �
			saTranslitLetters[4]="d";	// �
			saTranslitLetters[5]="e";	// �
			saTranslitLetters[6]="yo";	// �
			saTranslitLetters[7]="zh";	// �
			saTranslitLetters[8]="z";	// �
			saTranslitLetters[9]="i";	// �
			saTranslitLetters[10]="y";	// �
			saTranslitLetters[11]="k";	// �
			saTranslitLetters[12]="l";	// �
			saTranslitLetters[13]="m";	// �
			saTranslitLetters[14]="n";	// �
			saTranslitLetters[15]="o";	// �
			saTranslitLetters[16]="p";	// �
			saTranslitLetters[17]="r";	// �
			saTranslitLetters[18]="s";	// �
			saTranslitLetters[19]="t";	// �
			saTranslitLetters[20]="u";	// �
			saTranslitLetters[21]="f";	// �
			saTranslitLetters[22]="h";	// �
			saTranslitLetters[23]="ts";	// �
			saTranslitLetters[24]="ch";	// �
			saTranslitLetters[25]="sh";	// �
			saTranslitLetters[26]="sch";// �
			saTranslitLetters[27]="'";	// �
			saTranslitLetters[28]="y";	// �
			saTranslitLetters[29]="'";	// �
			saTranslitLetters[30]="e";	// �
			saTranslitLetters[31]="yu";	// �
			saTranslitLetters[32]="ya";	// �
			saTranslitLetters[33]="A";	// �
			saTranslitLetters[34]="B";	// �
			saTranslitLetters[35]="V";	// �
			saTranslitLetters[36]="G";	// �
			saTranslitLetters[37]="D";	// �
			saTranslitLetters[38]="E";	// �
			saTranslitLetters[39]="YO";	// �
			saTranslitLetters[40]="ZH";	// �
			saTranslitLetters[41]="Z";	// �
			saTranslitLetters[42]="I";	// �
			saTranslitLetters[43]="Y";	// �
			saTranslitLetters[44]="K";	// �
			saTranslitLetters[45]="L";	// �
			saTranslitLetters[46]="M";	// �
			saTranslitLetters[47]="N";	// �
			saTranslitLetters[48]="O";	// �
			saTranslitLetters[49]="P";	// �
			saTranslitLetters[50]="R";	// �
			saTranslitLetters[51]="S";	// �
			saTranslitLetters[52]="T";	// �
			saTranslitLetters[53]="U";	// �
			saTranslitLetters[54]="F";	// �
			saTranslitLetters[55]="H";	// �
			saTranslitLetters[56]="TS";	// �
			saTranslitLetters[57]="CH";	// �
			saTranslitLetters[58]="SH";	// �
			saTranslitLetters[59]="SCH";// �
			saTranslitLetters[60]="'";	// �
			saTranslitLetters[61]="Y";	// �
			saTranslitLetters[62]="'";	// �
			saTranslitLetters[63]="E";	// �
			saTranslitLetters[64]="YU";	// �
			saTranslitLetters[65]="YA";	// �
			
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
			saTranslitLetters[135]="�";
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
		
		#region �������� ������ ������
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
			return	"_"+dt.Year.ToString()+"-"+dt.Month.ToString()+"-"+dt.Day.ToString()+"-"+
				dt.Hour.ToString()+"-"+dt.Minute.ToString()+"-"+dt.Second.ToString()+"-"+
				Convert.ToString( m_ulDateCount );
		}
		
		public static long GetFileNewNumber( string sFileName ) {
			// ����� ��� ������ �����, ���� ��� ���� ��������� ����� ��
			string [] files = Directory.GetFiles( Path.GetDirectoryName( sFileName ) );
			string sTemp = sFileName.ToLower();
			if( sTemp.IndexOf( ".fb2" )!=1 ) {
				sTemp = sTemp.Substring( 0, sTemp.IndexOf( ".fb2" ) );
			}
			string s = sFileName.Substring( 0, sTemp.Length );
			long lCount = 0;
			foreach( string sFile in files ) {
				if( sFile.IndexOf( s )!=-1) {
					++lCount;
				}
			}
			return lCount;
		}
			
		public static string GetBookID( string sFB2FilePath )
		{
			// ���������� ���� _ID �����, ���� _ID_���, ���� � ����� ��� ���� ID
			FB2.FB2Parsers.FB2Parser fb2p = new FB2.FB2Parsers.FB2Parser( sFB2FilePath );
			DocumentInfo di = fb2p.GetDocumentInfo();
			return ( "_"+ ( di.ID != null ? di.ID : Settings.Settings.GetNoID() ) );
		}
		
		public static string GetFMBookID( string sFB2FilePath )
		{
			// ���������� ���� _ID �����, ���� _ID_���, ���� � ����� ��� ���� ID (�������������� � ������� ��� ���������� ������) - ��� �\��������� ������
			FB2.FB2Parsers.FB2Parser fb2p = new FB2.FB2Parsers.FB2Parser( sFB2FilePath );
			DocumentInfo di = fb2p.GetDocumentInfo();
			return "_" + ( di.ID != null ? di.ID : GetGeneralWorkedString( Settings.Settings.GetNoID() ) );
		}

		public static string SpaceString( string sString, int nMode ) {
			// ��������� �������� � ������
			if( sString==null || sString=="" ) {
				return sString;
			}
			string s = "";
			for( int i=0; i!=sString.Length; ++i ) {
				if( sString[i]==' ' ) {
					switch( nMode ) {
						case 0: // �������� ������
							s += sString[i];
							break;
						case 1: // ������� ������
							break;
						case 2: // �������� ������ �� '_'
							s += '_';
							break;
						case 3: // �������� ������ �� '-'
							s += '-';
							break;
						case 4: // �������� ������ �� '+'
							s += '+';
							break;
						case 5: // �������� ������ �� '~'
							s += '~';
							break;
					}
				} else {
					s += sString[i];
				}
			}
			return s;
		}
		
		public static string RegisterString( string sString, int nMode ) {
			// ������� �������� ������
			if( sString==null || sString=="" ) {
				return "";
			}
			switch( nMode ) {
				case 0: // ��� ����
					return sString;
				case 1: // ������ �������
					return sString.ToLower();
				case 2: // ������� �������
					return sString.ToUpper();
				default:
					return sString;
			}
		}
		
		public static string TransliterationString( string sString ) {
			// �������������� ������
			string sStr = sString;
			if( sString==null || sString=="" ) {
				return sString;
			}
			const string sTemplate = "�������������������������������������Ũ��������������������������abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 `~'!@#�$%^[](){}-+=_;.,\\";
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
			// "�������" �������� ������
			string s = sString;
			if( sString==null || sString=="" ) {
				return sString;
			}
			const string sStrictLetters = "�������������������������������������Ũ��������������������������abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 [](){}-_";
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
			// "�������" �������� ������
			string s = sPath;
			if( sPath==null || sPath=="" ) {
				return sPath;
			}
			const string sStrictLetters = "�������������������������������������Ũ��������������������������abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 \\[](){}-_";
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
			// ������ ���������� ������� ��� ���� ������
			string s = sString;
			if( sString==null || sString=="" ) {
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
			// ������ ���������� ������� ��� ���� ������
			string s = sString;
			if( sString==null || sString=="" ) {
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
			// �������
			s = RegisterString( sString, Settings.Settings.ReadRegisterMode() );
			// �������
			s = SpaceString( s, Settings.Settings.ReadSpaceProcessMode() );
			// "�������" �������
			if( Settings.Settings.ReadStrictMode() ) {
				s = StrictString( s );
			} else {
				s = OnlyCorrectSymbolsForString( s );
			}
			// ��������������
			if( Settings.Settings.ReadTranslitMode() ) {
				s = TransliterationString( s );
			}
			return s;
		}
		
		public static string GetGeneralWorkedPath( string sFB2FilePath )
		{
			string s = "";
			// �������
			s = RegisterString( sFB2FilePath, Settings.Settings.ReadRegisterMode() );
			// �������
			s = SpaceString( s, Settings.Settings.ReadSpaceProcessMode() );
			// ��������������
			if( Settings.Settings.ReadTranslitMode() ) {
				s = TransliterationString( s );
			}
			// "�������" �������
			if( Settings.Settings.ReadStrictMode() ) {
				s = StrictPath( s );
			} else {
				s = OnlyCorrectSymbolsForPath( s );
			}
			return s;
		}
		#endregion
		
		#region ����� ���������� ����� � ������ List
		private static string m_sForFind = ""; // ��� ��������� ����� � ������
		private static bool IsFileNameExsist( String s ) {
			// �������� ��� ������ � List ���� ���������� ������ m_sForFind
			return ( s == m_sForFind ) ? false : true;
        }
		public static List<string> GetFilesWithNames( List<string> lFilesList, string sFileName ) {
			// �������� ��� ������ � List lFilesList ���� ���������� ������ sFileName
			m_sForFind = sFileName;
			return lFilesList.FindAll( IsFileNameExsist );
		}
		public static long GetFilesCount( List<string> lFilesList, string sFileName ) {
			// ����� ����������� ������ sFileName � ������ lFilesList
			return GetFilesWithNames( lFilesList, sFileName ).Count;
		}
		#endregion
	}
}
