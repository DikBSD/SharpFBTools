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
using Core.FB2.Binary;

using fB2Parser = Core.FB2.FB2Parsers.FB2Parser;

namespace Core.FB2BookData
{
	/// <summary>
	/// FB2BookData: Данные описания fb2 файла
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
		
		public fB2Parser GetFB2Parser()
		{
			return m_fb2;
		}
		
		#region Свойства класса
		
		#region TitleInfo
		public virtual Annotation TIAnnotation {
			get {
				return ( m_fb2.GetTitleInfo() != null )
					? m_fb2.GetTitleInfo().Annotation
					: null;
			}
        }
		
		public virtual BookTitle TIBookTitle {
			get {
				return ( m_fb2.GetTitleInfo() != null )
					? m_fb2.GetTitleInfo().BookTitle
					: null;
			}
        }
		
		public virtual string TILang {
			get {
				return ( m_fb2.GetTitleInfo() != null )
					? m_fb2.GetTitleInfo().Lang
					: null;
			}
        }
		
		public virtual string TISrcLang {
			get {
				return ( m_fb2.GetTitleInfo() != null )
					? m_fb2.GetTitleInfo().SrcLang
					: null;
			}
        }
		
		public virtual IList<Genre> TIGenres {
			get {
				return ( m_fb2.GetTitleInfo() != null )
					? m_fb2.GetTitleInfo().Genres
					: null;
			}
        }
		
		public virtual IList<Author> TIAuthors {
			get {
				return ( m_fb2.GetTitleInfo() != null )
					? m_fb2.GetTitleInfo().Authors
					: null;
			}
        }
		
		public virtual Date TIDate {
			get {
				return ( m_fb2.GetTitleInfo() != null )
					? m_fb2.GetTitleInfo().Date
					: null;
			}
        }
		
		public virtual Keywords TIKeywords {
			get {
				return ( m_fb2.GetTitleInfo() != null )
					? m_fb2.GetTitleInfo().Keywords
					: null;
			}
        }
		
		public virtual int TICoverpagesCount {
			get {
				int coverCount = 0;
				if (m_fb2.GetTitleInfo() != null) {
					if (m_fb2.GetTitleInfo().Coverpages != null) {
						coverCount = m_fb2.GetTitleInfo().Coverpages.Count;
					}
				}
				return coverCount;
			}
        }
		
		public virtual IList<Author> TITranslators {
			get {
				return ( m_fb2.GetTitleInfo() != null )
					? m_fb2.GetTitleInfo().Translators
					: null;
			}
        }
		
		public virtual IList<Sequence> TISequences {
			get {
				return ( m_fb2.GetTitleInfo() != null )
					? m_fb2.GetTitleInfo().Sequences
					: null;
			}
        }
		
		#endregion
		
		#region SourceTitleInfo
		public virtual Annotation STIAnnotation {
			get {
				return ( m_fb2.GetSourceTitleInfo() != null )
					? m_fb2.GetSourceTitleInfo().Annotation
					: null;
			}
        }
		
		public virtual BookTitle STIBookTitle {
			get {
				return ( m_fb2.GetSourceTitleInfo() != null )
					? m_fb2.GetSourceTitleInfo().BookTitle
					: null;
			}
        }
		
		public virtual string STILang {
			get {
				return ( m_fb2.GetSourceTitleInfo() != null )
					? m_fb2.GetSourceTitleInfo().Lang
					: null;
			}
        }
		
		public virtual string STISrcLang {
			get {
				return ( m_fb2.GetSourceTitleInfo() != null )
					? m_fb2.GetSourceTitleInfo().SrcLang
					: null;
			}
        }
		
		public virtual IList<Genre> STIGenres {
			get {
				return ( m_fb2.GetSourceTitleInfo() != null )
					? m_fb2.GetSourceTitleInfo().Genres
					: null;
			}
        }
		
		public virtual IList<Author> STIAuthors {
			get {
				return ( m_fb2.GetSourceTitleInfo() != null )
					? m_fb2.GetSourceTitleInfo().Authors
					: null;
			}
        }
		
		public virtual Date STIDate {
			get {
				return ( m_fb2.GetSourceTitleInfo() != null )
					? m_fb2.GetSourceTitleInfo().Date
					: null;
			}
        }
		
		public virtual Keywords STIKeywords {
			get {
				return ( m_fb2.GetSourceTitleInfo() != null )
					? m_fb2.GetSourceTitleInfo().Keywords
					: null;
			}
        }
		
		public virtual int STICoverpagesCount {
			get {
				int coverCount = 0;
				if (m_fb2.GetSourceTitleInfo() != null) {
					if (m_fb2.GetSourceTitleInfo().Coverpages != null) {
						coverCount = m_fb2.GetSourceTitleInfo().Coverpages.Count;
					}
				}
				return coverCount;
			}
        }
		
		public virtual IList<Author> STITranslators {
			get {
				return ( m_fb2.GetSourceTitleInfo() != null )
					? m_fb2.GetSourceTitleInfo().Translators
					: null;
			}
        }
		
		public virtual IList<Sequence> STISequences {
			get {
				return ( m_fb2.GetSourceTitleInfo() != null )
					? m_fb2.GetSourceTitleInfo().Sequences
					: null;
			}
        }
		
		#endregion
		
		#region DocumentInfo
		public virtual string DIID {
			get {
				return ( m_fb2.GetDocumentInfo() != null )
					? m_fb2.GetDocumentInfo().ID
					: null;
			}
        }
		
		public virtual string DIVersion {
			get {
				return ( m_fb2.GetDocumentInfo() != null )
					? m_fb2.GetDocumentInfo().Version
					: null;
			}
        }
		
		public virtual Date DIFB2Date {
			get {
				return ( m_fb2.GetDocumentInfo() != null )
					? m_fb2.GetDocumentInfo().Date
					: null;
			}
        }
		
		public virtual ProgramUsed DIProgramUsed {
			get {
				return ( m_fb2.GetDocumentInfo() != null )
					? m_fb2.GetDocumentInfo().ProgramUsed
					: null;
			}
        }
		
		public virtual SrcOCR DISrcOcr {
			get {
				return ( m_fb2.GetDocumentInfo() != null )
					? m_fb2.GetDocumentInfo().SrcOcr
					: null;
			}
        }

		public virtual IList<string> DISrcUrls {
			get {
				return ( m_fb2.GetDocumentInfo() != null )
					? m_fb2.GetDocumentInfo().SrcUrls
					: null;
			}
        }
		
		public virtual IList<Author> DIFB2Authors {
			get {
				return ( m_fb2.GetDocumentInfo() != null )
					? m_fb2.GetDocumentInfo().Authors
					: null;
			}
        }
		
		public virtual History DIHistory {
			get {
				return ( m_fb2.GetDocumentInfo() != null )
					? m_fb2.GetDocumentInfo().History
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
		public virtual IList<CustomInfo> CICustomInfo {
			get {
				return m_fb2.GetCustomInfo();
			}
        }
		
		#endregion
		
		#region Binary
		public virtual IList<BinaryBase64> CoversBase64 {
			get {
				return m_fb2.GetCoversBase64();
			}
        }
		#endregion
		
		#endregion
	}
}
