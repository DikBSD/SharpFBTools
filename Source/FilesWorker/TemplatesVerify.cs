﻿/*
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

		private static bool IsTemplateCorrect( List<string> lsLexems ) {
			// проверка списка лексем на соответствие шаблонам
			if( lsLexems==null ) {
				return false;
			}
			string[] sAllTemplates = new string[] {
					"*L*","*G*","*BAF*","*BAM*","*BAL*","*BAN*","*BT*","*SN*",
					"*SI*","[","]","\\","(",")"," ","-","_"
			};
			bool bRet = false;
			foreach( string sLexem in lsLexems ) {
				bRet = false;
				foreach( string sTemplate in sAllTemplates ) {
					if( sLexem == sTemplate ) {
						bRet = true;
						break;
					}
				}
				if(!bRet) return false;
			}
			return bRet;
		}
		
		private static List<string> GetLexemsToVerify( string sString ) {
			// разбивка строки на лексемы, согласно шаблонам переименовывания
			char[] charSeparators = new char[] {'[',']','-','_','(',')','\\'};
			string[] sTemp = sString.Split( charSeparators, StringSplitOptions.RemoveEmptyEntries );
			char[] sTemplates = new char[] { ' ','-','_' };
			List<string> lsLexems = new List<string>();
			foreach( string s in sTemp ) {
				lsLexems.AddRange( s.Split( sTemplates, StringSplitOptions.RemoveEmptyEntries ) );
			}
			
			return lsLexems;
		}
		
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
			
	}
}
