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

namespace FilesWorker
{
	/// <summary>
	/// Description of TemplatesVerify.
	/// </summary>
	public class TemplatesVerify
	{
		public TemplatesVerify()
		{
		}

		#region Закрытые Вспомогательные методы
		private static bool IsTemplateCorrect( string[] sLexems ) {
			// проверка списка лексем на соответствие шаблонам
			if( sLexems==null ) return false;

			char[] sBad = new char[] { ':','*','/','|','?','<','>','\"','&','\t','\r','\n' };
			foreach( string sLexem in sLexems ) {
				foreach( char sSym in sBad ) {
					if( sLexem.IndexOf( sSym )!=-1 ) {
						// есть недопустимые символы в лексемах, не являющихся шаблонами
						return false;
					}
				}
			}
			return true;
		}
		
		private static string[] GetLexemsToVerify( string sString ) {
			// разбивка строки на лексемы, согласно шаблонам переименовывания
			string[] sAllTemplates = new string[] {
					"*L*","*G*","*BAF*","*BAM*","*BAL*","*BAN*","*BT*","*SN*","*SI*",
					"[","]","\\","(",")","{","}"," ","`","~","'","!","@","#","№","$","%","^",
					"-","+","=","_",";",".",","
			};
			return sString.Split( sAllTemplates, StringSplitOptions.RemoveEmptyEntries );
		}
		#endregion

		#region Открытые методы
		public static bool IsLineTemplatesCorrect( string sLine ) {
			// проверка на корректность элементов строки шаблонов
			return IsTemplateCorrect( GetLexemsToVerify( @sLine ) );
		}
		
		public static bool IsEvenElements( string sLine, char cChar ) {
			// проверка на четность элементов в строке
			int nCount = 0;
			for( int i=0; i!=sLine.Length; ++i ) {
				if( sLine[i]== cChar ) {
					++nCount;
				}
			}
			return ( nCount % 2 == 0 ? true : false );
		}
		
		public static bool IsBracketsCorrect( string sLine, char cLChar, char cRChar ) {
			// проверка на соответствие [ ] или ( ) в строке
			int nLCount = 0;
			int nRCount = 0;
			for( int i=0; i!=sLine.Length; ++i ) {
				if( sLine[i]== cLChar ) {
					++nLCount;
				}
			}
			for( int i=0; i!=sLine.Length; ++i ) {
				if( sLine[i]== cRChar ) {
					++nRCount;
				}
			}
			return ( nLCount==nRCount ? true : false );
		}
		
		public static bool IsConditionalPatternCorrect( string sLine ) {
			// проверка, корректен ли условный шаблон - не содержит ли вспомогат. символов без шаблона
			// формируем строки условных шаблонов
			string s = sLine;
			if( s.IndexOf('[')==-1 && s.IndexOf(']')==-1 ) return true;
			if( s.IndexOf('[')!=-1 && s.IndexOf(']')!=-1 ) {
				if( s.IndexOf('[') > s.IndexOf(']') ) return false;
			}
			
			List<string> ls = new List<string>();
			for( int i=0; i!=s.Length; ++i ) {
				int i1=s.IndexOf('[');
				int i2=s.IndexOf(']');
				if( i1==-1 ) break;
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
