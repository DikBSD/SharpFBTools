/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 20.07.2012
 * Time: 12:52
 * 
 * License: GPL 2.1
 */
using System;

namespace Core.FileManager.Templates.Lexems 
{
	/// <summary>
	/// FullNameTemplates: Полное написание шаблонов
	/// </summary>
	public class FullNameTemplates
	{
		public FullNameTemplates()
		{
		}
		
		public string LetterFamily {
            get { return @"*Letter\Family*"; }
        }
		public string GroupGenre {
            get { return @"*Group\Genre*"; }
        }
		public string Family {
            get { return "*Family*"; }
        }
		public string Language {
            get { return "*Language*"; }
        }
		public string Name {
            get { return "*Name*"; }
        }
		public string Patronimic {
            get { return "*Patronimic*"; }
        }
		public string Genre {
            get { return "*Genre*"; }
        }
		public string BookTitle {
            get { return "*BookTitle*"; }
        }
		public string Series {
            get { return "*Series*"; }
        }
		public string SeriesNumber {
            get { return "*SeriesNumber*"; }
        }
		public string Group {
            get { return "*Group*"; }
        }
	}
}
