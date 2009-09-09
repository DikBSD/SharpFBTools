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

using Core.FB2BookData;
using Core.FB2.Description.Common;
using Core.FB2.Description.TitleInfo;
using Core.FB2.Description.DocumentInfo;
using Core.FB2.Description.PublishInfo;
using Core.FB2.Description.CustomInfo;

using fb2BookData 	= Core.FB2BookData.FB2BookData;
using FB2Validator	= Core.FB2Parser.FB2Validator;
using filesWorker	= Core.FilesWorker.FilesWorker;

namespace Core.FB2Dublicator
{
	/// <summary>
	/// Description of BookData.
	/// </summary>
	public class FB2BookDataForDup
	{
		#region Закрытые данные класса
		private fb2BookData m_fb2bd		= null; // fb2-данные о книге
		private string m_sFromFilePath	= "";	// путь к анализируемой книге
		#endregion
		
		public FB2BookDataForDup( string sFromFilePath )
		{
			m_sFromFilePath = sFromFilePath;
			m_fb2bd = new fb2BookData( sFromFilePath );
		}
		
		#region Закрытые вспомогательные методы класса
		
		// формирование строки с Авторами Книги из списка всех Авторов ЭТОЙ Книги
		private string MakeAutorsString( IList<Author> Authors ) {
			if( Authors == null ) return ""; 
			string sA = ""; int n = 0;
			foreach( Author a in Authors ) {
				sA += Convert.ToString(++n)+": ";
				if( a.LastName!=null && a.LastName.Value!=null )
					sA += a.LastName.Value+" ";
				if( a.FirstName!=null && a.FirstName.Value!=null )
					sA += a.FirstName.Value+" ";
				if( a.MiddleName!=null && a.FirstName.Value!=null )
					sA += a.MiddleName.Value+" ";
				if( a.NickName!=null && a.NickName.Value!=null )
					sA += a.NickName.Value;
				sA += "; ";
			}
			return sA;
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
			string sG = ""; int n = 0;
			foreach( Genre g in Genres ) {
				sG += Convert.ToString(++n)+": ";
				if( g.Name!=null ) sG += g.Name;
				sG += "; ";
			}
			return sG;
		}
		
		// формирование строки с Сериями Книги из списка всех Серий ЭТОЙ Книги
		private string MakeSequencesString( IList<Sequence> Sequences ) {
			if( Sequences == null ) return ""; 
			string sSeq = ""; int n = 0;
			foreach( Sequence s in Sequences ) {
				sSeq += Convert.ToString(++n)+": ";
				if( s.Name!=null )	sSeq += s.Name;
				else 				sSeq += "Нет";
				if( s.Number!=null )	sSeq += " ("+s.Number+") ";
				else					sSeq += "Нет";
				sSeq += "; ";
			}
			return sSeq;
		}
		
		#endregion
		
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
		public virtual string BookTitle {
			get {
				return ( m_fb2bd.BookTitle != null && m_fb2bd.BookTitle.Value != null )
					? m_fb2bd.BookTitle.Value
					: "";
			}
        }
		
		public virtual string Lang {
			get {
				return m_fb2bd.Lang;
			}
        }
		
		public virtual string SrcLang {
			get {
				return m_fb2bd.SrcLang;
			}
        }
		
		public virtual string Genres {
			get {
				return MakeGenresString( m_fb2bd.Genres );
			}
        }
		
		public virtual string Authors {
			get {
				return MakeAutorsString( m_fb2bd.Authors );
			}
        }
		
		public virtual string Date {
			get {
				return MakeDateString( m_fb2bd.Date );
			}
        }
		
		public virtual string Keywords {
			get {
				return ( m_fb2bd.Keywords != null && m_fb2bd.Keywords.Value != null )
					? m_fb2bd.Keywords.Value
					: "";
			}
        }
		
		public virtual string Coverpage {
			get {
				return ( m_fb2bd.Coverpage != null && m_fb2bd.Coverpage.Value != null )
					? m_fb2bd.Coverpage.Value
					: "";
			}
        }
		
		public virtual string Translators {
			get {
				return MakeAutorsString( m_fb2bd.Translators );
			}
        }
		
		public virtual string Sequences {
			get {
				return MakeSequencesString( m_fb2bd.Sequences );
			}
        }
		
		#endregion
		
		#region DocumentInfo
		public virtual string ID {
			get {
				return m_fb2bd.ID;
			}
        }
		
		public virtual string Version {
			get {
				return m_fb2bd.Version;
			}
        }
		
		public virtual string FB2Date {
			get {
				return MakeDateString( m_fb2bd.FB2Date );
			}
        }
		
		public virtual string ProgramUsed {
			get {
				return ( m_fb2bd.ProgramUsed != null && m_fb2bd.ProgramUsed.Value != null )
					? m_fb2bd.ProgramUsed.Value
					: "";
			}
        }
		
		public virtual string SrcOcr {
			get {
				return ( m_fb2bd.SrcOcr != null && m_fb2bd.SrcOcr.Value != null )
					? m_fb2bd.SrcOcr.Value
					: "";
			}
        }

		public virtual string SrcUrls {
			get {
				if( m_fb2bd.SrcUrls == null ) return ""; 
				string sURLs = ""; int n = 0;
				foreach( string s in m_fb2bd.SrcUrls ) {
					sURLs += Convert.ToString(++n)+": ";
					if( s!=null || s.Length!=0 ) sURLs += s;
					sURLs += "; ";
				}
				return sURLs;
			}
        }
		
		public virtual string FB2Authors {
			get {
				return MakeAutorsString( m_fb2bd.FB2Authors );
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
		
		#endregion
	}
}
