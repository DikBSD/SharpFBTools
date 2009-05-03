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

		private static bool IsTemplateCorrect( string[] sLexems ) {
			// проверка списка лексем на соответствие шаблонам
			if( sLexems==null ) {
				return false;
			}
			string[] sTemplates = new string[18];
					sTemplates[0]="L";
					sTemplates[1]="G";
					sTemplates[2]="BAF";
					sTemplates[3]="BAM";
					sTemplates[4]="BAL";
					sTemplates[5]="BAN";
					sTemplates[6]="BT";
					sTemplates[7]="SN";
					sTemplates[8]="SI";
					sTemplates[9]="*";
					sTemplates[10]="[";
					sTemplates[11]="]";
					sTemplates[12]="\\";
					sTemplates[13]="(";
					sTemplates[14]=")";
					sTemplates[15]=" ";
					sTemplates[16]="-";
					sTemplates[17]="_";
			
			bool bRet = false;
			foreach( string sLexem in sLexems ) {
				bRet = false;
				foreach( string sTemplate in sTemplates ) {
					if( sLexem == sTemplate ) {
						bRet = true;
						break;
					}
				}
				if(!bRet) return false;
			}
			return bRet;
		}
		
		private static string[] GetLexems( string sString ) {
			// разбивка строки на длексемы. согласно шаблонам переименовывания
			char[] charSeparators = new char[] {'[',']','*','-','_','(',')','\\'};
			return sString.Split( charSeparators, StringSplitOptions.RemoveEmptyEntries );
		}
		
		public static bool IsTemplate( string s ) {
			// проверка s на принадлежность к шаблонам переименовывания
			try {
                Templates.TIT t = (Templates.TIT) Enum.Parse( typeof(Templates.TIT), s );
                return true;
            } catch( ArgumentException ) {
               return false;
            }
		}
		
		public static bool IsLineTemplatesCorrect( string sLine ) {
			// проверка на корректность элементов строки шаблонов
			return IsTemplateCorrect( GetLexems( @sLine ) );
		}
		
	}
}
