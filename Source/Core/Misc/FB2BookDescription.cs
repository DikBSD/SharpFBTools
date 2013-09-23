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
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using Core.FB2BookData;
using Core.FB2.Description.Common;
using Core.FB2.Description.TitleInfo;
using Core.FB2.Description.DocumentInfo;
using Core.FB2.Description.PublishInfo;
using Core.FB2.Description.CustomInfo;

using fb2BookData 	= Core.FB2BookData.FB2BookData;
using FB2Validator	= Core.FB2Parser.FB2Validator;
using filesWorker	= Core.FilesWorker.FilesWorker;

namespace Core.Misc
{
	/// <summary>
	/// Description of BookData.
	/// </summary>
	public class FB2BookDescription
	{
		#region Закрытые данные класса
		private fb2BookData m_fb2bd		= null; // fb2-данные о книге
		private string m_sFromFilePath	= "";	// путь к анализируемой книге
		#endregion
		
		public FB2BookDescription( string sFromFilePath )
		{
			m_sFromFilePath = sFromFilePath;
			m_fb2bd = new fb2BookData( sFromFilePath );
		}
		
		#region Закрытые вспомогательные методы класса
		
		// формирование строки с Авторами Книги из списка всех Авторов ЭТОЙ Книги
		private string MakeAutorsString( IList<Author> Authors) {
			if( Authors == null ) return ""; 
			string sA = ""; //int n = 0;
			foreach( Author a in Authors ) {
//				++n;
				if( a.LastName!=null && a.LastName.Value!=null )
					sA += a.LastName.Value+" ";
				if( a.FirstName!=null && a.FirstName.Value!=null )
					sA += a.FirstName.Value+" ";
				if( a.MiddleName!=null && a.FirstName.Value!=null )
					sA += a.MiddleName.Value+" ";
				if( a.NickName!=null && a.NickName.Value!=null )
					sA += a.NickName.Value;
				sA = sA.Trim();
				sA += "; ";
			}
//			sA = Convert.ToString(n)+": " + sA;
			return sA.Substring( 0, sA.LastIndexOf( ";" ) ).Trim();
		}
		
		// формирование строки с Датой Написания Книги или Датой Создания fb2-файла
		private string MakeDateString( Date Date ) {
			if( Date == null ) return ""; 
			string sDate = "";
			if( Date.Text!=null )	sDate += Date.Text;
			if( Date.Value!=null )	sDate += " ("+Date.Value+")";
			return sDate;
		}
		
		// формирование строки с Жанрами Книги из списка всех Жанров ЭТОЙ Книги
		private string MakeGenresString( IList<Genre> Genres ) {
			if( Genres == null ) return ""; 
			string sG = ""; //int n = 0;
			foreach( Genre g in Genres ) {
//				++n;
				if( g.Name!=null ) sG += g.Name;
				sG += "; ";
			}
//			if(n>1) {
//				sG = Convert.ToString(n)+": " + sG;
//			}
			
			//sG = Convert.ToString(n)+": " + sG;
			sG = sG.Trim();
			return sG.Substring( 0, sG.LastIndexOf( ";" ) ).Trim();
		}
		
		// формирование строки с Сериями Книги из списка всех Серий ЭТОЙ Книги
		private string MakeSequencesString( IList<Sequence> Sequences ) {
			if( Sequences == null ) return ""; 
			string sSeq = ""; //int n = 0;
			foreach( Sequence s in Sequences ) {
//				++n;
				if( s.Name!=null )	sSeq += s.Name;
				else 				sSeq += "Нет";
				if( s.Number!=null )	sSeq += " ("+s.Number+") ";
				else					sSeq += " (Нет) ";
				sSeq += "; ";
			}
//			sSeq = Convert.ToString(n)+": " + sSeq;
			sSeq = sSeq.Trim();
			return sSeq.Substring( 0, sSeq.LastIndexOf( ";" ) ).Trim();
		}
		
		#endregion
		
		#region Свойства класса
		
		#region Разное
		public virtual string Encoding {
			get {
				string sEncoding = filesWorker.GetFileEncoding( m_fb2bd.GetFB2Parser().XmlDoc.InnerXml.Split('>')[0] );
				return sEncoding != null ? sEncoding : "?";
			}
        }
		
		public virtual string FileLength {
			get {
				FileInfo fi = new FileInfo( m_sFromFilePath );
				return filesWorker.FormatFileLength( fi.Length );
			}
        }
		
		// время создания файла
		public virtual string FileCreationTime {
			get {
				FileInfo fi = new FileInfo( m_sFromFilePath );
				return fi.CreationTime.ToString();
			}
        }
		
		// время последней записи в файл
		public virtual string FileLastWriteTime {
			get {
				FileInfo fi = new FileInfo( m_sFromFilePath );
				return fi.LastWriteTime.ToString();
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
		public virtual string TIAnnotation {
			get {
				return ( m_fb2bd.TIAnnotation != null && m_fb2bd.TIAnnotation.Value != null )
					? m_fb2bd.TIAnnotation.Value
					: "";
			}
        }
		
		public virtual string TIBookTitle {
			get {
				return ( m_fb2bd.TIBookTitle != null && m_fb2bd.TIBookTitle.Value != null )
					? m_fb2bd.TIBookTitle.Value
					: "";
			}
        }
		
		public virtual string TILang {
			get {
				return m_fb2bd.TILang;
			}
        }
		
		public virtual string TISrcLang {
			get {
				return m_fb2bd.TISrcLang;
			}
        }
		
		public virtual string TIGenres {
			get {
				return MakeGenresString( m_fb2bd.TIGenres );
			}
        }
		
		public virtual string TIAuthors {
			get {
				return MakeAutorsString( m_fb2bd.TIAuthors );
			}
        }
		
		public virtual string TIDate {
			get {
				return MakeDateString( m_fb2bd.TIDate );
			}
        }
		
		public virtual string TIKeywords {
			get {
				return ( m_fb2bd.TIKeywords != null && m_fb2bd.TIKeywords.Value != null )
					? m_fb2bd.TIKeywords.Value
					: "";
			}
        }
		
		public virtual string TICoverpage {
			get {
				return ( m_fb2bd.TICoverpage != null && m_fb2bd.TICoverpage.Value != null )
					? m_fb2bd.TICoverpage.Value
					: "";
			}
        }
		
		public virtual string TITranslators {
			get {
				return MakeAutorsString( m_fb2bd.TITranslators );
			}
        }
		
		public virtual string TISequences {
			get {
				return MakeSequencesString( m_fb2bd.TISequences );
			}
        }
		
		#endregion
		
		#region SourceTitleInfo
		public virtual string STIAnnotation {
			get {
				return ( m_fb2bd.STIAnnotation != null && m_fb2bd.STIAnnotation.Value != null )
					? m_fb2bd.TIAnnotation.Value
					: "";
			}
        }
		
		public virtual string STIBookTitle {
			get {
				return ( m_fb2bd.STIBookTitle != null && m_fb2bd.STIBookTitle.Value != null )
					? m_fb2bd.STIBookTitle.Value
					: "";
			}
        }
		
		public virtual string STILang {
			get {
				return m_fb2bd.STILang;
			}
        }
		
		public virtual string STISrcLang {
			get {
				return m_fb2bd.STISrcLang;
			}
        }
		
		public virtual string STIGenres {
			get {
				return MakeGenresString( m_fb2bd.STIGenres );
			}
        }
		
		public virtual string STIAuthors {
			get {
				return MakeAutorsString( m_fb2bd.STIAuthors );
			}
        }
		
		public virtual string STIDate {
			get {
				return MakeDateString( m_fb2bd.STIDate );
			}
        }
		
		public virtual string STIKeywords {
			get {
				return ( m_fb2bd.STIKeywords != null && m_fb2bd.STIKeywords.Value != null )
					? m_fb2bd.STIKeywords.Value
					: "";
			}
        }
		
		public virtual string STICoverpage {
			get {
				return ( m_fb2bd.STICoverpage != null && m_fb2bd.STICoverpage.Value != null )
					? m_fb2bd.STICoverpage.Value
					: "";
			}
        }
		
		public virtual string STITranslators {
			get {
				return MakeAutorsString( m_fb2bd.STITranslators );
			}
        }
		
		public virtual string STISequences {
			get {
				return MakeSequencesString( m_fb2bd.STISequences );
			}
        }
		
		#endregion
		
		#region DocumentInfo
		public virtual string DIID {
			get {
				return m_fb2bd.DIID;
			}
        }
		
		public virtual string DIVersion {
			get {
				return m_fb2bd.DIVersion;
			}
        }
		
		public virtual string DIFB2Date {
			get {
				return MakeDateString( m_fb2bd.DIFB2Date );
			}
        }
		
		public virtual string DIProgramUsed {
			get {
				return ( m_fb2bd.DIProgramUsed != null && m_fb2bd.DIProgramUsed.Value != null )
					? m_fb2bd.DIProgramUsed.Value
					: "";
			}
        }
		
		public virtual string DISrcOcr {
			get {
				return ( m_fb2bd.DISrcOcr != null && m_fb2bd.DISrcOcr.Value != null )
					? m_fb2bd.DISrcOcr.Value
					: "";
			}
        }

		public virtual string DISrcUrls {
			get {
				if( m_fb2bd.DISrcUrls == null ) return ""; 
				string sURLs = ""; int n = 0;
				foreach( string s in m_fb2bd.DISrcUrls ) {
					sURLs += Convert.ToString(++n)+": ";
					if( s!=null || s.Length!=0 ) sURLs += s;
					sURLs += "; ";
				}
				return sURLs;
			}
        }
		
		public virtual string DIFB2Authors {
			get {
				return MakeAutorsString( m_fb2bd.DIFB2Authors );
			}
        }
		
		public virtual string DIHistory {
			get {
				return ( m_fb2bd.DIHistory != null && m_fb2bd.DIHistory.Value != null )
					? m_fb2bd.DIHistory.Value
					: "";
			}
        }
		
		#endregion
		
		#region PublishInfo
		// Заголовок Книги
		public virtual string PIBookName {
			get {
				return ( m_fb2bd.PIBookName != null && m_fb2bd.PIBookName.Value != null )
					? m_fb2bd.PIBookName.Value
					: "";
			}
        }
		// Издатель
		public virtual string PIPublisher {
			get {
				return ( m_fb2bd.PIPublisher != null && m_fb2bd.PIPublisher.Value != null )
					? m_fb2bd.PIPublisher.Value
					: "";
			}
        }
		// Город
		public virtual string PICity {
			get {
				return ( m_fb2bd.PICity != null && m_fb2bd.PICity.Value != null )
					? m_fb2bd.PICity.Value
					: "";
			}
        }
		// Год издания
		public virtual string PIYear {
			get {
				return ( m_fb2bd.PIYear != null )
					? m_fb2bd.PIYear
					: "";
			}
        }
		// ISBN
		public virtual string PIISBN {
			get {
				return ( m_fb2bd.PIISBN != null && m_fb2bd.PIISBN.Value != null )
					? m_fb2bd.PIISBN.Value
					: "";
			}
        }
		// Серии
		public virtual string PISequences {
			get {
				return MakeSequencesString( m_fb2bd.PISequences );
			}
        }
		#endregion
		
		#region CustomInfo
		public virtual IList<CustomInfo> CICustomInfo {
			get {
				return m_fb2bd.CICustomInfo;
			}
        }
		#endregion
		
		#endregion
	}
}
