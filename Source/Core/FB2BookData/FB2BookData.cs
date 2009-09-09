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

using fB2Parser = Core.FB2.FB2Parsers.FB2Parser;

namespace Core.FB2BookData
{
	/// <summary>
	/// Description of BookData.
	/// </summary>
	public class FB2BookData
	{
		#region Закрытые данные класса
		private fB2Parser m_fb2	= null; // fb2-парсер
		#endregion
		
		public FB2BookData( string sFromFilePath )
		{
			m_fb2 = new fB2Parser( sFromFilePath );
		}
		
		#region Свойства класса
		
		#region TitleInfo
		public virtual BookTitle BookTitle {
			get {
				return ( m_fb2.GetTitleInfo() != null )
					? m_fb2.GetTitleInfo().BookTitle
					: null;
			}
        }
		
		public virtual string Lang {
			get {
				return ( m_fb2.GetTitleInfo() != null )
					? m_fb2.GetTitleInfo().Lang
					: null;
			}
        }
		
		public virtual string SrcLang {
			get {
				return ( m_fb2.GetTitleInfo() != null )
					? m_fb2.GetTitleInfo().SrcLang
					: null;
			}
        }
		
		public virtual IList<Genre> Genres {
			get {
				return ( m_fb2.GetTitleInfo() != null )
					? m_fb2.GetTitleInfo().Genres
					: null;
			}
        }
		
		public virtual IList<Author> Authors {
			get {
				return ( m_fb2.GetTitleInfo() != null )
					? m_fb2.GetTitleInfo().Authors
					: null;
			}
        }
		
		public virtual Date Date {
			get {
				return ( m_fb2.GetTitleInfo() != null )
					? m_fb2.GetTitleInfo().Date
					: null;
			}
        }
		
		public virtual Keywords Keywords {
			get {
				return ( m_fb2.GetTitleInfo() != null )
					? m_fb2.GetTitleInfo().Keywords
					: null;
			}
        }
		
		public virtual Coverpage Coverpage {
			get {
				return ( m_fb2.GetTitleInfo() != null )
					? m_fb2.GetTitleInfo().Coverpage
					: null;
			}
        }
		
		public virtual IList<Author> Translators {
			get {
				return ( m_fb2.GetTitleInfo() != null )
					? m_fb2.GetTitleInfo().Translators
					: null;
			}
        }
		
		public virtual IList<Sequence> Sequences {
			get {
				return ( m_fb2.GetTitleInfo() != null )
					? m_fb2.GetTitleInfo().Sequences
					: null;
			}
        }
		
		#endregion
		
		#region DocumentInfo
		public virtual string ID {
			get {
				return ( m_fb2.GetDocumentInfo() != null )
					? m_fb2.GetDocumentInfo().ID
					: null;
			}
        }
		
		public virtual string Version {
			get {
				return ( m_fb2.GetDocumentInfo() != null )
					? m_fb2.GetDocumentInfo().Version
					: null;
			}
        }
		
		public virtual Date FB2Date {
			get {
				return ( m_fb2.GetDocumentInfo() != null )
					? m_fb2.GetDocumentInfo().Date
					: null;
			}
        }
		
		public virtual ProgramUsed ProgramUsed {
			get {
				return ( m_fb2.GetDocumentInfo() != null )
					? m_fb2.GetDocumentInfo().ProgramUsed
					: null;
			}
        }
		
		public virtual SrcOCR SrcOcr {
			get {
				return ( m_fb2.GetDocumentInfo() != null )
					? m_fb2.GetDocumentInfo().SrcOcr
					: null;
			}
        }

		public virtual IList<string> SrcUrls {
			get {
				return ( m_fb2.GetDocumentInfo() != null )
					? m_fb2.GetDocumentInfo().SrcUrls
					: null;
			}
        }
		
		public virtual IList<Author> FB2Authors {
			get {
				return ( m_fb2.GetDocumentInfo() != null )
					? m_fb2.GetDocumentInfo().Authors
					: null;
			}
        }
		#endregion
		
		#region PublishInfo
		// Заголовок Книги
		public virtual BookName PIBookName {
			get {
				return ( m_fb2.GetPublishInfo() != null )
					? m_fb2.GetPublishInfo().BookName
					: null;
			}
        }
		// Издатель
		public virtual Publisher PIPublisher {
			get {
				return ( m_fb2.GetPublishInfo() != null )
					? m_fb2.GetPublishInfo().Publisher
					: null;
			}
        }
		// Город
		public virtual City PICity {
			get {
				return ( m_fb2.GetPublishInfo() != null )
					? m_fb2.GetPublishInfo().City
					: null;
			}
        }
		// Год издания
		public virtual string PIYear {
			get {
				return ( m_fb2.GetPublishInfo() != null )
					? m_fb2.GetPublishInfo().Year
					: null;
			}
        }
		// ISBN
		public virtual ISBN PIISBN {
			get {
				return ( m_fb2.GetPublishInfo() != null )
					? m_fb2.GetPublishInfo().ISBN
					: null;
			}
        }
		// Серии
		public virtual IList<Sequence> PISequences {
			get {
				return ( m_fb2.GetPublishInfo() != null )
					? m_fb2.GetPublishInfo().Sequences
					: null;
			}
        }
		#endregion
		
		#region CustomInfo
		public virtual CustomInfo CICustomInfo {
			get {
				return m_fb2.GetCustomInfo();
			}
        }
		#endregion
		
		#endregion
	}
}
