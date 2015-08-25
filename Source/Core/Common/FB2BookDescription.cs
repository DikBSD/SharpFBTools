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
using System.Xml;

using Core.FB2.Description.Common;
using Core.FB2.Description.TitleInfo;
using Core.FB2.Description.CustomInfo;
using Core.FB2.Binary;
using Core.FB2.FB2Parsers;

using FB2Validator	= Core.FB2Parser.FB2Validator;
using filesWorker	= Core.Common.FilesWorker;

namespace Core.Common
{
	/// <summary>
	/// Все данные на книгу и fb2 файл
	/// </summary>
	public class FB2BookDescription
	{
		#region Закрытые данные класса
		private readonly FictionBook m_fb2	= null; // fb2-данные о книге
		private string m_sFromFilePath		= string.Empty;	// путь к анализируемой книге
		#endregion
		
		public FB2BookDescription( string sFromFilePath)
		{
			m_sFromFilePath = sFromFilePath;
			m_fb2 = new FictionBook( sFromFilePath );
		}
		
		#region Закрытые вспомогательные методы класса
		// формирование строки с Авторами Книги из списка всех Авторов ЭТОЙ Книги
		private string MakeAutorsString( IList<Author> AuthorsList) {
			if( AuthorsList == null )
				return string.Empty;
			string sA = string.Empty;
			foreach( Author a in AuthorsList ) {
				if ( a != null ) {
					if( a.LastName!=null && a.LastName.Value!=null )
						sA += a.LastName.Value + " ";
					if( a.FirstName!=null && a.FirstName.Value!=null )
						sA += a.FirstName.Value + " ";
					if( a.MiddleName!=null && a.MiddleName.Value!=null )
						sA += a.MiddleName.Value + " ";
					if( a.NickName!=null && a.NickName.Value!=null )
						sA += a.NickName.Value;
					sA = sA.Trim();
					sA += "; ";
				}
			}
			return sA.Substring( 0, sA.LastIndexOf( ';' ) ).Trim();
		}
		
		// формирование строки с Датой Написания Книги или Датой Создания fb2-файла
		private string MakeDateString( Date Date ) {
			if( Date == null )
				return string.Empty;
			
			string sDate = string.Empty;
			if( Date.Text != null )
				sDate += Date.Text;
			if( Date.Value!=null )
				sDate += " (" + Date.Value + ")";
			return sDate;
		}
		
		// формирование строки с Жанрами Книги из списка всех Жанров ЭТОЙ Книги
		private string MakeGenresString( IList<Genre> GenresList ) {
			if( GenresList == null )
				return string.Empty;
			
			string sG = string.Empty;
			foreach( Genre g in GenresList ) {
				if ( g != null ) {
					if( g.Name != null )
						sG += g.Name;
					sG += "; ";
				}
			}
			sG = sG.Trim();
			return sG.Substring( 0, sG.LastIndexOf( ';' ) ).Trim();
		}
		
		// формирование строки с Сериями Книги из списка всех Серий ЭТОЙ Книги
		private string MakeSequencesString( IList<Sequence> Sequences ) {
			if( Sequences == null ) return string.Empty;
			string sSeq = string.Empty;
			foreach( Sequence s in Sequences ) {
				if ( s != null ) {
					if( s.Name != null )
						sSeq += s.Name;
					else
						sSeq += "Нет";
					if( s.Number != null )
						sSeq += " ("+s.Number+") ";
					sSeq += "; ";
				}
			}
			sSeq = sSeq.Trim();
			return sSeq.Substring( 0, sSeq.LastIndexOf( ';' ) ).Trim();
		}
		#endregion
		
		#region Свойства класса
		#region Разное
		public virtual string Encoding {
			get {
				string sEncoding = m_fb2.getEncoding();
				return !string.IsNullOrEmpty( sEncoding ) ? sEncoding : "?";
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
		
		public virtual string IsValidFB22 {
			get {
				FB2Validator fv2Validator = new FB2Validator();
				return fv2Validator.ValidatingFB22File( m_sFromFilePath );
			}
		}
		
		public virtual string IsValidFB2Librusec {
			get {
				FB2Validator fv2Validator = new FB2Validator();
				return fv2Validator.ValidatingFB2LibrusecFile( m_sFromFilePath );
			}
		}
		
		public virtual string FilePath {
			get {
				return m_sFromFilePath;
			}
		}
		
		#endregion
		
		#region TitleInfo
		public virtual string TIAnnotation {
			get {
				return ( m_fb2.TIAnnotation != null && m_fb2.TIAnnotation.Value != null )
					? m_fb2.TIAnnotation.Value
					: string.Empty;
			}
		}
		
		public virtual string TIBookTitle {
			get {
				return ( m_fb2.TIBookTitle != null && m_fb2.TIBookTitle.Value != null )
					? m_fb2.TIBookTitle.Value
					: string.Empty;
			}
		}
		
		public virtual string TILang {
			get {
				return m_fb2.TILang;
			}
		}
		
		public virtual string TISrcLang {
			get {
				return m_fb2.TISrcLang;
			}
		}
		
		public virtual string TIGenres {
			get {
				return MakeGenresString( m_fb2.TIGenres );
			}
		}
		
		public virtual string TIAuthors {
			get {
				return MakeAutorsString( m_fb2.TIAuthors );
			}
		}
		
		public virtual IList<Genre> Genres {
			get {
				return m_fb2.TIGenres;
			}
		}
		
		public virtual IList<Author> Authors {
			get {
				return m_fb2.TIAuthors;
			}
		}
		
		public virtual string TIDate {
			get {
				return MakeDateString( m_fb2.TIDate );
			}
		}
		
		public virtual string TIKeywords {
			get {
				return ( m_fb2.TIKeywords != null && m_fb2.TIKeywords.Value != null )
					? m_fb2.TIKeywords.Value
					: string.Empty;
			}
		}
		
		public virtual string TITranslators {
			get {
				return MakeAutorsString( m_fb2.TITranslators );
			}
		}
		
		public virtual string TISequences {
			get {
				return MakeSequencesString( m_fb2.TISequences );
			}
		}
		
		public virtual IList<BinaryBase64> TICoversBase64 {
			get {
				return m_fb2.getCoversBase64( Enums.TitleInfoEnum.TitleInfo );
			}
		}
		#endregion
		
		#region SourceTitleInfo
		public virtual string STIAnnotation {
			get {
				return ( m_fb2.STIAnnotation != null && m_fb2.STIAnnotation.Value != null )
					? m_fb2.TIAnnotation.Value
					: "";
			}
		}
		
		public virtual string STIBookTitle {
			get {
				return ( m_fb2.STIBookTitle != null && m_fb2.STIBookTitle.Value != null )
					? m_fb2.STIBookTitle.Value
					: string.Empty;
			}
		}
		
		public virtual string STILang {
			get {
				return m_fb2.STILang;
			}
		}
		
		public virtual string STISrcLang {
			get {
				return m_fb2.STISrcLang;
			}
		}
		
		public virtual string STIGenres {
			get {
				return MakeGenresString( m_fb2.STIGenres );
			}
		}
		
		public virtual string STIAuthors {
			get {
				return MakeAutorsString( m_fb2.STIAuthors );
			}
		}
		
		public virtual string STIDate {
			get {
				return MakeDateString( m_fb2.STIDate );
			}
		}
		
		public virtual string STIKeywords {
			get {
				return ( m_fb2.STIKeywords != null && m_fb2.STIKeywords.Value != null )
					? m_fb2.STIKeywords.Value
					: string.Empty;
			}
		}
		
		public virtual string STITranslators {
			get {
				return MakeAutorsString( m_fb2.STITranslators );
			}
		}
		
		public virtual string STISequences {
			get {
				return MakeSequencesString( m_fb2.STISequences );
			}
		}
		
		public virtual IList<BinaryBase64> STICoversBase64 {
			get {
				return m_fb2.getCoversBase64( Enums.TitleInfoEnum.SourceTitleInfo );
			}
		}
		#endregion
		
		#region DocumentInfo
		public virtual string DIID {
			get {
				return m_fb2.DIID;
			}
		}
		
		public virtual string DIVersion {
			get {
				return m_fb2.DIVersion;
			}
		}
		
		public virtual string DIFB2Date {
			get {
				return MakeDateString( m_fb2.DIDate );
			}
		}
		
		public virtual string DIProgramUsed {
			get {
				return ( m_fb2.DIProgramUsed != null && m_fb2.DIProgramUsed.Value != null )
					? m_fb2.DIProgramUsed.Value
					: string.Empty;
			}
		}
		
		public virtual string DISrcOcr {
			get {
				return ( m_fb2.DISrcOCR != null && m_fb2.DISrcOCR.Value != null )
					? m_fb2.DISrcOCR.Value
					: string.Empty;
			}
		}

		public virtual string DISrcUrls {
			get {
				if( m_fb2.DISrcUrls == null )
					return string.Empty;
				
				string sURLs = string.Empty;
				int n = 0;
				foreach( string s in m_fb2.DISrcUrls ) {
					if( s != null ) {
						if( s.Length > 0 ) {
							sURLs += Convert.ToString(++n) + ": ";
							sURLs += s;
							sURLs += "; ";
						}
					}
				}
				return sURLs;
			}
		}
		
		public virtual string DIFB2Authors {
			get {
				return MakeAutorsString( m_fb2.DIAuthors );
			}
		}
		
		public virtual string DIHistory {
			get {
				return ( m_fb2.DIHistory != null && m_fb2.DIHistory.Value != null )
					? m_fb2.DIHistory.Value
					: string.Empty;
			}
		}
		
		#endregion
		
		#region PublishInfo
		// Заголовок Книги
		public virtual string PIBookName {
			get {
				return ( m_fb2.PIBookName != null && m_fb2.PIBookName.Value != null )
					? m_fb2.PIBookName.Value
					: string.Empty;
			}
		}
		// Издатель
		public virtual string PIPublisher {
			get {
				return ( m_fb2.PIPublisher != null && m_fb2.PIPublisher.Value != null )
					? m_fb2.PIPublisher.Value
					: string.Empty;
			}
		}
		// Город
		public virtual string PICity {
			get {
				return ( m_fb2.PICity != null && m_fb2.PICity.Value != null )
					? m_fb2.PICity.Value
					: string.Empty;
			}
		}
		// Год издания
		public virtual string PIYear {
			get {
				return ( m_fb2.PIYear != null )
					? m_fb2.PIYear
					: string.Empty;
			}
		}
		// ISBN
		public virtual string PIISBN {
			get {
				return ( m_fb2.PIISBN != null && m_fb2.PIISBN.Value != null )
					? m_fb2.PIISBN.Value
					: string.Empty;
			}
		}
		// Серии
		public virtual string PISequences {
			get {
				return MakeSequencesString( m_fb2.PISequences );
			}
		}
		#endregion
		
		#region CustomInfo
		public virtual IList<CustomInfo> CICustomInfo {
			get {
				return m_fb2.CICustomInfo;
			}
		}
		#endregion
		
		#endregion
		
		#region Открытые методы класса
		public XmlDocument getXmlDoc() {
			return m_fb2.getXmlDoc();
		}
		public FictionBook getFictionBook() {
			return m_fb2;
		}
		#endregion
	}
}
