/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 30.04.2009
 * Time: 8:41
 * 
 * License: GPL 2.1
 */
using System;
using System.Xml;
using System.Collections.Generic;
using System.Windows.Forms;

using Core.FB2.Common;
using Core.FB2.Description;
using Core.FB2.Description.TitleInfo;
using Core.FB2.Description.DocumentInfo;
using Core.FB2.Description.PublishInfo;
using Core.FB2.Description.CustomInfo;
using Core.FB2.Description.Common;
using Core.FB2.Binary;

namespace Core.FB2.FB2Parsers
{
	/// <summary>
	/// Description of FB2Parser.
	/// </summary>
	public class FB2Parser
	{
		#region Закрытые данные класса
		private readonly XmlNamespaceManager	m_NsManager	= null;
		private readonly XmlDocument			m_xmlDoc	= null;
		private const string m_aFB20Namespace = "http://www.gribuser.ru/xml/fictionbook/2.0";
		private const string m_aFB21Namespace = "http://www.gribuser.ru/xml/fictionbook/2.1";
		private string m_ns = "/fb20:";
		#endregion

		#region Конструкторы класса
		public FB2Parser( string sFB2Path )
		{
			m_xmlDoc = null;
			try {
				m_xmlDoc = new XmlDocument();
				m_xmlDoc.Load( sFB2Path );
				m_NsManager = new XmlNamespaceManager( m_xmlDoc.NameTable );
				string fb2FileNamespaceURI = m_xmlDoc.DocumentElement.NamespaceURI;
				if( fb2FileNamespaceURI.Equals( m_aFB21Namespace ) ) {
					m_NsManager.AddNamespace( "fb21", m_aFB21Namespace );
					m_ns = "/fb21:";
				} else {
					m_NsManager.AddNamespace( "fb20", m_aFB20Namespace );
					m_ns = "/fb20:";
				}
			} catch {
				throw new System.IO.FileLoadException( "Bad File: " + sFB2Path );
			}
			
		}
		#endregion
		
		#region Закрытые вспомогательные основные методы класса
		private TitleInfo TitleInfo( bool bTitleInfo )
		{
			// извлечение информации по title-info
			#region Код
			XmlNode xn = null;
			xn = bTitleInfo
				? m_xmlDoc.SelectSingleNode( m_ns + "FictionBook" + m_ns + "description" + m_ns + "title-info", m_NsManager )
				: m_xmlDoc.SelectSingleNode( m_ns + "FictionBook" + m_ns + "description" + m_ns + "src-title-info", m_NsManager );
			
			if( xn == null )
				return null;
			
			// Жанры
			IList<Genre> ilGenres = null;
			XmlNodeList xmlNodes = xn.SelectNodes("." + m_ns + "genre", m_NsManager);
			if( xmlNodes.Count > 0  ) {
				ilGenres = new List<Genre>();
				foreach( XmlNode node in xmlNodes )
					ilGenres.Add( GetGenre( node ) );
			}

			// Авторы
			IList<Author> ilAuthors = null;
			xmlNodes = xn.SelectNodes("." + m_ns + "author", m_NsManager);
			if( xmlNodes.Count > 0  ) {
				ilAuthors = new List<Author>();
				foreach( XmlNode node in xmlNodes ) {
					Author author = GetAuthor( node );
					ilAuthors.Add( author );
				}
			}
			
			// Название Книги
			BookTitle bookTitle = TextFieldType<BookTitle>( xn.SelectSingleNode("." + m_ns + "book-title", m_NsManager) );
			
			// Аннотация
			Annotation annotation = AnnotationType<Annotation>( xn.SelectSingleNode("." + m_ns + "annotation", m_NsManager) );

			// Ключевые слова
			Keywords keywords = TextFieldType<Keywords>( xn.SelectSingleNode("." + m_ns + "keywords", m_NsManager) );

			// Дата написания Книги
			Date date = GetDate( xn );

			// Обложка
			IList<Coverpage> ilCoverpages = null;
			xmlNodes = xn.SelectNodes("." + m_ns + "coverpage", m_NsManager);
			if( xmlNodes.Count > 0  ) {
				ilCoverpages = new List<Coverpage>();
				makeCoverPageNameList( ref ilCoverpages );
			}
			
			// Язык Книги
			string sLang = null;
			XmlNode xmlNode = xn.SelectSingleNode("." + m_ns + "lang", m_NsManager);
			if( xmlNode != null ) {
				sLang = xmlNode.InnerText;
			}
			
			// Язык Оригинала Книги
			string sSrcLang = null;
			xmlNode = xn.SelectSingleNode("." + m_ns + "src-lang", m_NsManager);
			if( xmlNode != null ) {
				sSrcLang = xmlNode.InnerText;
			}

			// переводчики
			IList<Author> tranlators = null;
			xmlNodes = xn.SelectNodes("." + m_ns + "translator", m_NsManager);
			if( xmlNodes.Count > 0 ) {
				tranlators = new List<Author>();
				foreach( XmlNode node in xmlNodes ) {
					Author translator = GetAuthor( node );
					tranlators.Add( translator );
				}
			}

			// Серии
			IList<Sequence> sequences = null;
			xmlNodes = xn.SelectNodes("." + m_ns + "sequence", m_NsManager);
			if(xmlNodes.Count > 0) {
				sequences = new List<Sequence>();
				foreach( XmlNode node in xmlNodes ) {
					sequences.Add( GetSequence(node) );
					GetSequences( node, sequences );
				}
			}

			return new TitleInfo( ilGenres, ilAuthors, bookTitle, annotation, keywords, date,
			                     ilCoverpages, sLang, sSrcLang, tranlators, sequences);
			
		}
		#endregion
		
		private Date GetDate( XmlNode xn )
		{
			// Дата создания fb2-документа
			#region Код
			Date date = null;
			XmlNode xmlNode = xn.SelectSingleNode("." + m_ns + "date", m_NsManager);
			if( xmlNode != null ) {
				date = new Date( xmlNode.InnerText );
				if( xmlNode.Attributes["value"] != null ) {
					date.Value = xmlNode.Attributes["value"].Value;
				}
				if( xmlNode.Attributes["lang"] != null ) {
					date.Lang = xmlNode.Attributes["lang"].Value;
				}
			}
			return date;
			#endregion
		}
		private Genre GetGenre( XmlNode xn )
		{
			// извлечение информации по custom-info
			#region Код
			Genre genre = new Genre( xn.InnerText );
			if( xn.Attributes["match"] != null ) {
				try {
					genre.Math = Convert.ToUInt32( xn.Attributes["match"].Value );
				} catch( Exception ) {
					genre.Math = 100;
				}
			}

			return genre;
			#endregion
		}

		private Author GetAuthor( XmlNode xn )
		{
			// извлечение информации по author
			#region Код
			if( xn == null ) {
				return null;
			}

			Author Author = null;
			XmlNode		fn = xn.SelectSingleNode("." + m_ns + "first-name", m_NsManager);
			XmlNode		mn = xn.SelectSingleNode("." + m_ns + "middle-name", m_NsManager);
			XmlNode		ln = xn.SelectSingleNode("." + m_ns + "last-name", m_NsManager);
			XmlNode		nn = xn.SelectSingleNode("." + m_ns + "nickname", m_NsManager);
			XmlNodeList	hp = xn.SelectNodes("." + m_ns + "home-page", m_NsManager);
			XmlNodeList	em = xn.SelectNodes("." + m_ns + "email", m_NsManager);
			XmlNode		id = xn.SelectSingleNode("." + m_ns + "id", m_NsManager);

			if( fn != null || mn != null || ln != null || nn != null || hp != null || em != null || id != null ) {
				Author = new Author();
				if( fn != null ) {
					Author.FirstName = TextFieldType<TextFieldType>( fn );
				}
				if( mn != null ) {
					Author.MiddleName = TextFieldType<TextFieldType>( mn );
				}
				if( ln != null ) {
					Author.LastName = TextFieldType<TextFieldType>( ln );
				}
				if( nn != null ) {
					Author.NickName = TextFieldType<TextFieldType>( nn );
				}
				if( hp.Count > 0 ) {
					IList<string> homePages = new List<string>();
					foreach( XmlNode node in hp ) {
						homePages.Add( node.InnerText );
					}
					Author.HomePages = homePages;
				}
				if( em.Count > 0 ) {
					IList<string> emails = new List<string>();
					foreach( XmlNode node in em ) {
						emails.Add( node.InnerText );
					}
					Author.Emails = emails;
				}
				if( id != null ) {
					Author.ID = id.InnerText;
				}
			}
			return Author;
			#endregion
		}
		
		private Sequence GetSequence( XmlNode node )
		{
			// извлечение информации по sequence
			#region Код
			Sequence sequence = null;
			if( node.Attributes["name"] != null )  {
				sequence = new Sequence( node.Attributes["name"].Value );
			}
			if( node.Attributes["number"] != null ) {
				if( sequence == null ) {
					sequence = new Sequence();
				}
				try {
					sequence.Number = node.Attributes["number"].Value ;
				} catch( FormatException ) {
				}
			}
			if( node.Attributes["lang"] != null ) {
				if( sequence == null ) {
					sequence = new Sequence();
				}
				sequence.Lang = node.Attributes["lang"].Value;
			}
			return sequence;
			#endregion
		}
		
		private IList<Sequence> GetSequences( XmlNode xn, IList<Sequence> sequences ) {
			// извлечение информации по вложенным sequence в sequence
			#region Код
			XmlNodeList xmlNodes = xn.SelectNodes("." + m_ns + "sequence", m_NsManager);
			if( xmlNodes.Count > 0 ) {
				foreach( XmlNode node in xmlNodes ) {
					sequences.Add( GetSequence( node ) );
					GetSequences( node, sequences );
				}
			}
			return sequences;
			#endregion
		}
		
		private T TextFieldType<T>( XmlNode xmlNode ) where T : ITextFieldType, new()
		{
			if( xmlNode == null ) {
				return default(T);
			}

			T textField = new T();
			textField.Value = xmlNode.InnerText;
			if( xmlNode.Attributes["lang"] != null ) {
				textField.Lang = xmlNode.Attributes["lang"].Value;
			}
			return textField;
		}
		
		private T AnnotationType<T>( XmlNode xmlNode ) where T : IAnnotationType, new()
		{
			T annotation = default(T);
			if( xmlNode != null ) {
				annotation = new T();
				annotation.Value = xmlNode.InnerXml;
				if( xmlNode.Attributes["id"] != null ) {
					annotation.Id = xmlNode.Attributes["id"].Value;
				}
				if( xmlNode.Attributes["lang"] != null ) {
					annotation.Lang = xmlNode.Attributes["lang"].Value;
				}
			}
			return annotation;
		}
		
		private void makeCoverPageNameList( ref IList<Coverpage> ilCoverpages )
		{
			// Обложки
			XmlNode xn = m_xmlDoc.SelectSingleNode( m_ns + "FictionBook" + m_ns + "description" + m_ns + "title-info" + m_ns + "coverpage", m_NsManager );
			if( xn == null )
				return;

			XmlNodeList xmlNodes = xn.SelectNodes("." + m_ns + "image", m_NsManager);
			if( xmlNodes.Count > 0  ) {
				foreach( XmlNode node in xmlNodes ) {
					if( node != null ) {
						if( node.Attributes["l:href"] != null ) {
							Coverpage cover = new Coverpage(node.Attributes["l:href"].Value.Substring(1));
							ilCoverpages.Add(cover);
						}
					}
				}
			}

		}
		#endregion
		
		#region Открытые свойства класса
		public XmlDocument XmlDoc
		{
			get { return m_xmlDoc; }
		}
		#endregion
		
		#region Открытые основные методы класса
		public TitleInfo GetTitleInfo()
		{
			return TitleInfo( true );
		}
		
		public TitleInfo GetSourceTitleInfo()
		{
			return TitleInfo( false );
		}
		
		public DocumentInfo GetDocumentInfo()
		{
			// извлечение информации по document-info
			#region Код
			XmlNode xn = m_xmlDoc.SelectSingleNode( m_ns + "FictionBook" + m_ns + "description" + m_ns + "document-info", m_NsManager );
			if( xn == null ) {
				return null;
			}

			// Авторы fb2-документа
			IList<Author> ilAuthors = null;
			XmlNodeList xmlNodes = xn.SelectNodes("." + m_ns + "author", m_NsManager);
			if( xmlNodes.Count > 0  ) {
				ilAuthors = new List<Author>();
				foreach( XmlNode node in xmlNodes ) {
					Author author = GetAuthor( node );
					ilAuthors.Add( author );
				}
			}

			// Дата создания fb2-документа
			Date date = GetDate( xn );
			
			// ID fb2-документа
			string id = null;
			XmlNode xmlNode = xn.SelectSingleNode("." + m_ns + "id", m_NsManager);
			if( xmlNode != null ) {
				id = xmlNode.InnerText;
			}

			// Версия fb2-документа
			string version = null;
			xmlNode = xn.SelectSingleNode("." + m_ns + "version", m_NsManager);
			if( xmlNode != null ) {
				version = xmlNode.InnerText;
			}

			// Программа создания fb2-документа
			ProgramUsed programUsed = null;
			xmlNode = xn.SelectSingleNode("." + m_ns + "program-used", m_NsManager);
			if( xmlNode != null ) {
				programUsed = new ProgramUsed( xmlNode.InnerText );
			}

			// Источник текста
			IList<string> srcUrls = null;
			xmlNodes = xn.SelectNodes("." + m_ns + "src-url", m_NsManager);
			if( xmlNodes.Count > 0 ) {
				srcUrls = new List<string>();
				foreach( XmlNode node in xmlNodes ) {
					srcUrls.Add( node.InnerText );
				}
			}
			SrcOCR srcOcr = null;
			xmlNode = xn.SelectSingleNode("." + m_ns + "src-ocr", m_NsManager);
			if( xmlNode != null ) {
				srcOcr = new SrcOCR( xmlNode.InnerText );
			}

			// История развития fb2-документа
			History history = AnnotationType<History>( xn.SelectSingleNode("." + m_ns + "history", m_NsManager) );
			return new DocumentInfo( ilAuthors, programUsed, date, srcUrls, srcOcr, id, version, history );
			#endregion
		}
		
		public PublishInfo GetPublishInfo()
		{
			// извлечение информации по publish-info
			#region Код
			XmlNode xn = m_xmlDoc.SelectSingleNode( m_ns + "FictionBook" + m_ns + "description" + m_ns + "publish-info", m_NsManager );
			if( xn == null ) {
				return null;
			}

			// Название Бумажной Книги
			BookName bookName = TextFieldType<BookName>(xn.SelectSingleNode("." + m_ns + "book-name", m_NsManager));
			
			// Издатель Бумажной Книги
			Publisher publisher = TextFieldType<Publisher>(xn.SelectSingleNode("." + m_ns + "publisher", m_NsManager));
			
			// Город издания
			City city = TextFieldType<City>(xn.SelectSingleNode("." + m_ns + "city", m_NsManager));
			
			// Год издания
			string sYear = null;
			XmlNode xmlNode = xn.SelectSingleNode("." + m_ns + "year", m_NsManager);
			if( xmlNode != null ) {
				sYear = xmlNode.InnerText;
			}
			
			// ISBN Бумажной Книги
			ISBN isbn = TextFieldType<ISBN>( xn.SelectSingleNode("." + m_ns + "isbn", m_NsManager) );
			
			// Серии Бумажной Книги
			IList<Sequence> sequences = null;
			XmlNodeList xmlNodes = xn.SelectNodes("." + m_ns + "sequence", m_NsManager);
			if( xmlNodes.Count > 0 ) {
				sequences = new List<Sequence>();
				foreach( XmlNode node in xmlNodes ) {
					sequences.Add( GetSequence( node ) );
					GetSequences( node, sequences );
				}
			}

			return new PublishInfo( bookName, publisher, city, sYear, isbn, sequences );
			#endregion
		}
		
		public IList<CustomInfo> GetCustomInfo()
		{
			// извлечение информации по custom-info
			#region Код
			IList<CustomInfo> ilCustomInfos = null;
			
			XmlNodeList xmlNodes = m_xmlDoc.SelectNodes( m_ns + "FictionBook" + m_ns + "description" + m_ns + "custom-info", m_NsManager );
			if( xmlNodes == null ) return null;
			
			if( xmlNodes.Count > 0  ) {
				ilCustomInfos = new List<CustomInfo>();
				foreach( XmlNode node in xmlNodes ) {
					CustomInfo customInfo = null;
					if( node.Attributes["info-type"] != null ) {
						customInfo = new CustomInfo( node.InnerText, node.Attributes["info-type"].Value);
					} else {
						customInfo = new CustomInfo( node.InnerText, null );
					}
					if( node.Attributes["lang"] != null ) {
						customInfo.Lang = node.Attributes["lang"].Value;
					}
					ilCustomInfos.Add( customInfo );
				}
			}
			
			return ilCustomInfos;
			#endregion
		}
		
		public Description.Description GetDescription()
		{
			// парсер description
			#region Код
			TitleInfo titleInfo				= GetTitleInfo();
			TitleInfo srcTitleInfo			= GetSourceTitleInfo();
			DocumentInfo documentInfo		= GetDocumentInfo();
			PublishInfo publishInfo			= GetPublishInfo();
			IList<CustomInfo> customInfo	= GetCustomInfo();
			
			return new Description.Description( titleInfo, srcTitleInfo, documentInfo, publishInfo, customInfo );
			#endregion
		}
		
		public IList<BinaryBase64> GetCoversBase64()
		{
			// извлечение информации по binary, в зависимости от id
			#region Код
			XmlNode xn = m_xmlDoc.SelectSingleNode( m_ns + "FictionBook" + m_ns + "description" + m_ns + "title-info", m_NsManager );
			if( xn == null ) {
				return null;
			}

			IList<BinaryBase64> covers = null;
			BinaryBase64 coverBase64 = null;
			// Обложка
			IList<Coverpage> ilCoverpages = new List<Coverpage>();
			makeCoverPageNameList( ref ilCoverpages );
			
			if( ilCoverpages != null && ilCoverpages.Count > 0) {
				XmlNodeList xmlNodes = xn.SelectNodes(m_ns + "FictionBook" + m_ns + "binary", m_NsManager);
				covers = new List<BinaryBase64>();
				int count = 0;
				foreach( XmlNode node in xmlNodes ) {
					if( node.Attributes["id"] != null ) {
						if( node.Attributes["id"].Value == ilCoverpages[count].Value ) {
							coverBase64 = new BinaryBase64();
							try {
								coverBase64.id = ilCoverpages[count].Value;
								coverBase64.base64String = node.InnerText;
								++count;
							} catch( Exception ) {
								coverBase64.base64String = null;
								break;
							}
							covers.Add(coverBase64);
						}
					}
				}
			}
			return covers;
			#endregion
		}
		#endregion
	}
}
