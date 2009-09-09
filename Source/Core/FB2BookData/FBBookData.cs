/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 07.09.2009
 * Time: 15:29
 * 
 * License: GPL 2.1
 */
using System;
using System.IO;
using System.Collections.Generic;

using Core.FB2.Description.Common;
using Core.FB2.Description.TitleInfo;
using Core.FB2.Description.DocumentInfo;
using Core.FB2.Description.PublishInfo;
using Core.FB2.Description.CustomInfo;

using fB2Parser 	= Core.FB2.FB2Parsers.FB2Parser;
using FB2Validator	= Core.FB2Parser.FB2Validator;
using filesWorker	= Core.FilesWorker.FilesWorker;

namespace Core.FB2BookData
{
	/// <summary>
	/// Description of BookData.
	/// </summary>
	public class FB2BookData
	{
		#region Закрытые данные класса
		private fB2Parser m_fb2			= null; // fb2-парсер
		private string m_sFromFilePath	= "";	// путь к анализируемой книге
		#endregion
		
		public FB2BookData( string sFromFilePath )
		{
			m_sFromFilePath = sFromFilePath;
			m_fb2 = new fB2Parser( sFromFilePath );
		}
		
		#region Свойства класса
		
		#region Разное
		public virtual string FileLength {
			get {
				FileInfo fi = new FileInfo( m_sFromFilePath );
				return filesWorker.FormatFileLength( fi.Length );
			}
        }
		
		public virtual string IsValid {
			get {
				FB2Validator fv2Validator = new FB2Validator();
				return fv2Validator.ValidatingFB2File( m_sFromFilePath );
			}
        }
		#endregion
		
		#region TitleInfo
		public virtual BookTitle BookTitle {
			get {
				return m_fb2.GetTitleInfo().BookTitle;
			}
        }
		
		public virtual string Lang {
			get {
				return m_fb2.GetTitleInfo().Lang;
			}
        }
		
		public virtual string SrcLang {
			get {
				return m_fb2.GetTitleInfo().SrcLang;
			}
        }
		
		public virtual IList<Genre> Genres {
			get {
				return m_fb2.GetTitleInfo().Genres;
			}
        }
		
		public virtual IList<Author> Authors {
			get {
				return m_fb2.GetTitleInfo().Authors;
			}
        }
		
		public virtual Date Date {
			get {
				return m_fb2.GetTitleInfo().Date;
			}
        }
		
		public virtual Keywords Keywords {
			get {
				return m_fb2.GetTitleInfo().Keywords;
			}
        }
		
		public virtual Coverpage Coverpage {
			get {
				return m_fb2.GetTitleInfo().Coverpage;
			}
        }
		
		public virtual IList<Author> Translators {
			get {
				return m_fb2.GetTitleInfo().Translators;
			}
        }
		
		public virtual IList<Sequence> Sequences {
			get {
				return m_fb2.GetTitleInfo().Sequences;
			}
        }
		
		#endregion
		
		#region DocumentInfo
		public virtual string ID {
			get {
				return m_fb2.GetDocumentInfo().ID;
			}
        }
		
		public virtual string Version {
			get {
				return m_fb2.GetDocumentInfo().Version;
			}
        }
		
		public virtual Date FB2Date {
			get {
				return m_fb2.GetDocumentInfo().Date;
			}
        }
		
		public virtual ProgramUsed ProgramUsed {
			get {
				return m_fb2.GetDocumentInfo().ProgramUsed;
			}
        }
		
		public virtual SrcOCR SrcOcr {
			get {
				return m_fb2.GetDocumentInfo().SrcOcr;
			}
        }

		public virtual IList<string> SrcUrls {
			get {
				return m_fb2.GetDocumentInfo().SrcUrls;
			}
        }
		
		public virtual IList<Author> FB2Authors {
			get {
				return m_fb2.GetDocumentInfo().Authors;
			}
        }
		#endregion
		
		#endregion
	}
}
