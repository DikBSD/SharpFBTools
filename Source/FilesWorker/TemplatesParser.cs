/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 04.05.2009
 * Time: 11:30
 * 
 * License: GPL 2.1
 */
using System;
using System.Collections.Generic;

namespace FilesWorker
{
	/// <summary>
	/// Description of TemplatesParser.
	/// </summary>
	public class TemplatesParser
	{
		public TemplatesParser()
		{
		}
		
		private static string GetTemplateLexem( string sLine, char cRChar ) {
			// выделение лексемы для символа cRChar с начала строки sLine
			int n = sLine.IndexOf( cRChar );
			return ( n > 0 ? sLine.Substring( 1, n-1 )
			        		: sLine.Substring( 0, sLine.Length ) );
			
		}
		private static string GetSymbolsLexem( string sLine ) {
			// выделение лексемы с начала строки sLine до (,[ или *. Порядок поиска важен!
			int n = sLine.IndexOf( '(' );
			if( n!=-1 ) {
				return sLine.Substring( 0, n );
			}
			n = sLine.IndexOf( '[' );
			if( n!=-1 ) {
				return sLine.Substring( 0, n );
			}
			n = sLine.IndexOf( '*' );
			if( n!=-1 ) {
				return sLine.Substring( 0, n );
			}
			return sLine.Substring( 0, sLine.Length );
		}
		
		public static List<string> GetLexems( string sLine ) {
			// разбивка строки на лексемы для дальнейшего анализа
			if( sLine==null || sLine=="" ) return null;
			string sTemp = sLine;
			List<string> ls = new List<string>();
			string s = "";
			while( sTemp.Length!=0 ) {
				if( sTemp[0]=='[' ) {
					s =  GetTemplateLexem( sTemp, ']' );
					sTemp = ( s.Length<sTemp.Length ? sTemp.Remove( 0, s.Length+2 )
					         						: sTemp.Remove( 0, s.Length ) );
				} else if( sTemp[0]=='(' ) {
					s =  GetTemplateLexem( sTemp, ')' );
					sTemp = ( s.Length<sTemp.Length ? sTemp.Remove( 0, s.Length+2 )
					         						: sTemp.Remove( 0, s.Length ) );
				} else if( sTemp[0]=='*' ) {
					s =  GetTemplateLexem( sTemp, '*' );
					sTemp = ( s.Length<sTemp.Length ? sTemp.Remove( 0, s.Length+2 )
					         						: sTemp.Remove( 0, s.Length ) );
				} else {
					s =  GetSymbolsLexem( sTemp );
					sTemp = sTemp.Remove( 0, s.Length );
				}
				ls.Add( s );
			}
			
			return ls;
		}
	}
}


/*
		private static string GetTemplateLexem( string sLine, char cRChar ) {
			// выделение лексемы для символа cRChar с начала строки sLine
			int n = sLine.IndexOf( cRChar );
			return ( n > 0 ? sLine.Substring( 0, n+1 )
			        		: sLine.Substring( 0, sLine.Length ) );
			
		}
		private static string GetSymbolsLexem( string sLine ) {
			// выделение лексемы с начала строки sLine до (,[ или *. Порядок поиска важен!
			int n = sLine.IndexOf( '(' );
			if( n!=-1 ) {
				return sLine.Substring( 0, n );
			}
			n = sLine.IndexOf( '[' );
			if( n!=-1 ) {
				return sLine.Substring( 0, n );
			}
			n = sLine.IndexOf( '*' );
			if( n!=-1 ) {
				return sLine.Substring( 0, n );
			}
			return sLine.Substring( 0, sLine.Length );
		}
		
		public static List<string> GetLexems( string sLine ) {
			// разбивка строки на лексемы для дальнейшего анализа
			if( sLine==null || sLine=="" ) return null;
			string sTemp = sLine;
			List<string> ls = new List<string>();
			string s = "";
			while( sTemp.Length!=0 ) {
				if( sTemp[0]=='[' ) {
					s =  GetTemplateLexem( sTemp, ']' );
					sTemp = sTemp.Remove( 0, s.Length );
				} else if( sTemp[0]=='(' ) {
					s =  GetTemplateLexem( sTemp, ')' );
					sTemp = sTemp.Remove( 0, s.Length );
				} else if( sTemp[0]=='*' ) {
					s =  GetTemplateLexem( sTemp, '*' );
					sTemp = sTemp.Remove( 0, s.Length );
				} else {
					s =  GetSymbolsLexem( sTemp );
					sTemp = sTemp.Remove( 0, s.Length );
				}
				ls.Add( s );
			}
			
			return ls;
		}
 * */