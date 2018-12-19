/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 03.05.2009
 * Time: 16:00
 * 
 * * License: GPL 2.1
 */
using System;
using System.Collections.Generic;
using Core.Sorter.Templates.Lexems;

namespace Core.Sorter.Templates
{
	/// <summary>
	/// TemplatesVerify: Работа с лексемами шаблонов подстановки
	/// </summary>
	public class TemplatesVerify
	{
		public TemplatesVerify()
		{
		}

		#region Закрытые Вспомогательные методы
		// проверка списка лексем на соответствие шаблонам
		private static bool IsTemplateCorrect( string[] sLexems ) {
			if( sLexems == null ) return false;

			char[] sBad = new char[] { ':','*','/','|','?','<','>','\"','&','\t','\r','\n' };
			foreach ( string sLexem in sLexems ) {
				foreach ( char sSym in sBad ) {
					if ( sLexem.IndexOf( sSym ) != -1 ) {
						// есть недопустимые символы в лексемах, не являющихся шаблонами
						return false;
					}
				}
			}
			return true;
		}
		
		// разбивка строки на лексемы, согласно шаблонам переименовывания
		private static string[] GetLexemsToVerify( string sString ) {
			string[] sSymbols = new string[] {
					"[","]","\\","(",")","{","}"," ","`","~","'","!","@","#","№","$","%","^",
					"-","+","=","_",";",".",","
			};
			string[] sAllTemplates = new string[AllTemplates.Templates.Length + sSymbols.Length ];
			AllTemplates.Templates.CopyTo( sAllTemplates, 0 );
			sSymbols.CopyTo( sAllTemplates, AllTemplates.Templates.Length );
			return sString.Split( sAllTemplates, StringSplitOptions.RemoveEmptyEntries );
		}
		#endregion

		#region Открытые методы
		// приведение 2-х видов шаблонов к одному
		public static string ToOneTemplateType( string sString ) {
			string TemplateString = sString;
			TemplateString = TemplateString.Replace(@"*Letter\Family*", "*LBAL*");
			TemplateString = TemplateString.Replace(@"*Group\Genre*", "*GG*");
			TemplateString = TemplateString.Replace("*Family*", "*BAL*");
			TemplateString = TemplateString.Replace("*Language*", "*L*");
			TemplateString = TemplateString.Replace("*Name*", "*BAF*");
			TemplateString = TemplateString.Replace("*Patronimic*", "*BAM*");
			TemplateString = TemplateString.Replace("*Genre*", "*G*");
			TemplateString = TemplateString.Replace("*BookTitle*", "*BT*");
			TemplateString = TemplateString.Replace("*Series*", "*SN*");
			TemplateString = TemplateString.Replace("*SeriesNumber*", "*SII*");
			return TemplateString;
		}
		
		// проверка на корректность элементов строки шаблонов
		public static bool IsLineTemplatesCorrect( string sLine ) {
			return IsTemplateCorrect( GetLexemsToVerify( @sLine ) );
		}
		
		// проверка на четность элементов в строке
		public static bool IsEvenElements( string sLine, char cChar ) {
			int nCount = 0;
			for ( int i = 0; i != sLine.Length; ++i ) {
				if ( sLine[i] == cChar ) {
					++nCount;
				}
			}
			return ( nCount % 2 == 0 ? true : false );
		}
		
		// проверка на соответствие [ ] или ( ) в строке
		public static bool IsBracketsCorrect( string sLine, char cLChar, char cRChar ) {
			int nLCount = 0;
			int nRCount = 0;
			for ( int i = 0; i != sLine.Length; ++i ) {
				if ( sLine[i] == cLChar ) {
					++nLCount;
				}
			}
			for ( int i=0; i!=sLine.Length; ++i ) {
				if ( sLine[i]== cRChar ) {
					++nRCount;
				}
			}
			return ( nLCount==nRCount ? true : false );
		}
		
		// проверка, корректен ли условный шаблон - не содержит ли вспомогат. символов без шаблона
		public static bool IsConditionalPatternCorrect( string sLine ) {
			// формируем строки условных шаблонов
			string s = sLine;
			if ( s.IndexOf('[')==-1 && s.IndexOf(']')==-1 ) return true;
			if ( s.IndexOf('[')!=-1 && s.IndexOf(']')!=-1 ) {
				if ( s.IndexOf('[') > s.IndexOf(']') ) return false;
			}
			
			List<string> ls = new List<string>();
			for ( int i=0; i!=s.Length; ++i ) {
				int i1=s.IndexOf('[');
				int i2=s.IndexOf(']');
				if ( i1==-1 ) break;
				ls.Add( s.Substring( i1+1, i2-(i1+1) ) );
				s = s.Remove( i1, i2-(i1-1) );
				i = i2;
			}
			// проверяем, есть ли в условных шаблонах вспомогат. символы И *
			const string sSymbols = "\\`~'!@#№$%^(){}-+=_;., абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			foreach( string str in ls ) {
				foreach( char sSym in sSymbols ) {
					if( str.IndexOf( sSym )!=-1 ) {
						if( str.IndexOf( '*' )!=-1 ) {
							// все в порядке
							break;
						} else {
							// вспомогат. символы в условном шаблоне без самого шаблона
							return false;
						}
					}
				}
			}
			return true;
		}
		#endregion
		
	}
}
