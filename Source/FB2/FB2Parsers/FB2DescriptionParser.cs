/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 17:21
 * 
 * License: GPL 2.1
 */
using System;
using System.Xml;
using System.Collections.Generic;
using FB2.Common;
using FB2.Description;
using FB2.Description.TitleInfo;
using FB2.Description.DocumentInfo;
using FB2.Description.PublishInfo;
using FB2.Description.CustomInfo;
using FB2.Description.Common;

using System.Windows.Forms;
namespace FB2.FB2Parsers
{
	/// <summary>
	/// Description of FB2DescriptionParser.
	/// </summary>
	public class FB2DescriptionParser
	{
		#region Закрытые данные класса
		private XmlNamespaceManager m_NsManager;
        private XmlDocument			m_xmlDoc;
        #endregion
        
		#region Конструкторы класса
        public FB2DescriptionParser()
        {
        	m_xmlDoc = new XmlDocument();
        }
        #endregion
        
        #region Открытые свойства класса
        public XmlDocument XmlDoc
        {
            get { return m_xmlDoc; }
        }
        #endregion
        
        #region Открытые методы класса
        public Description.Description Parse( string sFB2Path )
        {
            // парсер description
        	#region Код
            m_xmlDoc.Load( sFB2Path );

            m_NsManager = new XmlNamespaceManager( m_xmlDoc.NameTable );
            m_NsManager.AddNamespace("fb", "http://www.gribuser.ru/xml/fictionbook/2.0");

            TitleInfo titleInfo = GetTitleInfo(m_xmlDoc.SelectSingleNode("/fb:FictionBook/fb:description/fb:title-info", m_NsManager));
            TitleInfo srcTitleInfo = GetTitleInfo( m_xmlDoc.SelectSingleNode("/fb:FictionBook/fb:description/fb:src-title-info", m_NsManager) );
            DocumentInfo documentInfo = GetDocumentInfo( m_xmlDoc.SelectSingleNode("/fb:FictionBook/fb:description/fb:document-info", m_NsManager) );
            PublishInfo publishInfo = GetPublishInfo( m_xmlDoc.SelectSingleNode("/fb:FictionBook/fb:description/fb:publish-info", m_NsManager) );
            CustomInfo customInfo = GetCustomInfo( m_xmlDoc.SelectSingleNode("/fb:FictionBook/fb:description/fb:custom-info", m_NsManager) );

            return new Description.Description( titleInfo, srcTitleInfo, documentInfo, publishInfo, customInfo );
            #endregion
        }
        #endregion
        
        #region Закрытые Вспомогательные методы класса
        private TitleInfo GetTitleInfo( XmlNode xn )
        {
            // извлечение информации по title-info
        	#region Код
        	if( xn == null ) {
                return null;
            }

            // Жанры
            XmlNodeList xmlNodes = xn.SelectNodes("./fb:genre", m_NsManager);
            IList<Genre> ilGenres = new List<Genre>();
            foreach( XmlNode node in xmlNodes ) {
                ilGenres.Add( GetGenre( node ) );
            }

            // Авторы
            IList<Author> ilAuthors = null;
            xmlNodes = xn.SelectNodes("./fb:author", m_NsManager);
            if( xmlNodes.Count > 0  ) {
            	ilAuthors = new List<Author>();
            	foreach( XmlNode node in xmlNodes ) {
                	Author author = GetAuthor( node );
                	ilAuthors.Add( author );
           		}
            }
            

            // Название Книги
            BookTitle bookTitle = TextFieldType<BookTitle>( xn.SelectSingleNode("./fb:book-title", m_NsManager) );

            // Аннотация
            Annotation annotation = AnnotationType<Annotation>( xn.SelectSingleNode("./fb:annotation", m_NsManager) );

            // Ключевые слова
            Keywords keywords = TextFieldType<Keywords>( xn.SelectSingleNode("./fb:keywords", m_NsManager) );

            // Дата написания Книги
            Date date = null;

            // Обложка
            Coverpage coverpage = null;
            XmlNode xmlNode = xn.SelectSingleNode("./fb:coverpage", m_NsManager);
            if( xmlNode != null ) {
            	coverpage = new Coverpage( xmlNode.InnerXml );
            }
            
            // Язык Книги
            string sLang = null;
            xmlNode = xn.SelectSingleNode("./fb:lang", m_NsManager);
            if( xmlNode != null ) {
                sLang = xmlNode.InnerText;
            }
            
			// Язык Оригинала Книги
            string sSrcLang = null;
            xmlNode = xn.SelectSingleNode("./fb:src-lang", m_NsManager);
            if( xmlNode != null ) {
                sSrcLang = xmlNode.InnerText;
            }

            // переводчики
            IList<Author> tranlators = null;
            xmlNodes = xn.SelectNodes("./fb:translator", m_NsManager);
            if( xmlNodes.Count > 0 ) {
                tranlators = new List<Author>();
                foreach( XmlNode node in xmlNodes ) {
                    Author translator = GetAuthor( node );
                    tranlators.Add( translator );
                }
            }

            // Серии
            IList<Sequence> sequences = null;
            xmlNodes = xn.SelectNodes("./fb:sequence", m_NsManager);
            if(xmlNodes.Count > 0) {
                sequences = new List<Sequence>();
                foreach( XmlNode node in xmlNodes ) {
                   sequences.Add( GetSequence(node) );
                }
            }

            return new TitleInfo( ilGenres, ilAuthors, bookTitle, annotation, keywords, date,
                                 coverpage, sLang, sSrcLang, tranlators, sequences);
            #endregion
        }
        
        private DocumentInfo GetDocumentInfo( XmlNode xn )
        {
            // извлечение информации по document-info
        	#region Код
        	if( xn == null ) {
                return null;
            }

            // Авторы fb2-документа
            IList<Author> ilAuthors = null;
            XmlNodeList xmlNodes = xn.SelectNodes("./fb:author", m_NsManager);
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
            XmlNode xmlNode = xn.SelectSingleNode("./fb:id", m_NsManager);
            if( xmlNode != null ) {
            	id = xmlNode.InnerText;
            }

            // Версия fb2-документа
            string version = null;
            xmlNode = xn.SelectSingleNode("./fb:version", m_NsManager);
            if( xmlNode != null ) {
            	version = xmlNode.InnerText;
            }

            // Программа создания fb2-документа
            ProgramUsed programUsed = null;
            xmlNode = xn.SelectSingleNode("./fb:program-used", m_NsManager);
            if( xmlNode != null ) {
                programUsed = new ProgramUsed( xmlNode.InnerText );
            }

            // Источник текста
            IList<string> srcUrls = null;
            xmlNodes = xn.SelectNodes("./fb:src-url", m_NsManager);
            if( xmlNodes.Count > 0 ) {
                srcUrls = new List<string>();
                foreach( XmlNode node in xmlNodes ) {
                    srcUrls.Add( node.InnerText );
                }
            }
            SrcOCR srcOcr = null;
            xmlNode = xn.SelectSingleNode("./fb:src-ocr", m_NsManager);
            if( xmlNode != null ) {
                srcOcr = new SrcOCR( xmlNode.InnerText );
            }

            // История развития fb2-документа
            History history = AnnotationType<History>( xn.SelectSingleNode("./fb:history", m_NsManager) );
            return new DocumentInfo( ilAuthors, programUsed, date, srcUrls, srcOcr, id, version, history );
            #endregion
        }
        
        private Date GetDate( XmlNode xn ) 
        {
			// Дата создания fb2-документа
            #region Код
        	Date date = null;
            XmlNode xmlNode = xn.SelectSingleNode("./fb:date", m_NsManager);
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
        
        private PublishInfo GetPublishInfo( XmlNode xn )
        {
            // извлечение информации по publish-info
        	#region Код
            if( xn == null ) {
                return null;
            }

            // Название Бумажной Книги
        	BookName bookName = TextFieldType<BookName>(xn.SelectSingleNode("./fb:book-name", m_NsManager));
         
        	// Издатель Бумажной Книги
        	Publisher publisher = TextFieldType<Publisher>(xn.SelectSingleNode("./fb:publisher", m_NsManager));
            
        	// Город издания
        	City city = TextFieldType<City>(xn.SelectSingleNode("./fb:city", m_NsManager));
			
        	// Год издания
            string sYear = null;
            XmlNode xmlNode = xn.SelectSingleNode("./fb:year", m_NsManager);
            if( xmlNode != null ) {
                sYear = xmlNode.InnerText;
            }
            
            // ISBN Бумажной Книги
            ISBN isbn = TextFieldType<ISBN>( xn.SelectSingleNode("./fb:isbn", m_NsManager) );
            
            // Серии Бумажной Книги
            IList<Sequence> sequences = null;
            XmlNodeList xmlNodes = xn.SelectNodes("./fb:sequence", m_NsManager);
            if( xmlNodes.Count > 0 ) {
                sequences = new List<Sequence>();
                foreach( XmlNode node in xmlNodes ) {
                    sequences.Add( GetSequence( node ) );
                }
            }

            return new PublishInfo( bookName, publisher, city, sYear, isbn, sequences );
            #endregion
        }

        private CustomInfo GetCustomInfo( XmlNode node )
        {
            // извлечение информации по custom-info
        	#region Код
        	if( node == null ) {
                return null;
            }
            CustomInfo customInfo = new CustomInfo(node.InnerText, node.Attributes["info-type"].Value);
            if( node.Attributes["lang"] != null ) {
                customInfo.Lang = node.Attributes["lang"].Value;
            }
            return customInfo;
            #endregion
        }


        private TextFieldType TextFieldTypeData( XmlNode node )
        {
        	// извлечение информации по TextFieldType
        	#region Код
        	if( node == null ) {
                return null;
            }
        	TextFieldType e = new TextFieldType( node.InnerText );
            if ( node.Attributes["lang"] != null ) {
            	e.Lang = node.Attributes["lang"].Value;
  	       	}	
            return e;
            #endregion
        }
        
        
        private Genre GetGenre( XmlNode xn )
        {
            // извлечение информации по custom-info
        	#region
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
            XmlNode fn = xn.SelectSingleNode("./fb:first-name", m_NsManager);
            XmlNode mn = xn.SelectSingleNode("./fb:middle-name", m_NsManager);
            XmlNode ln = xn.SelectSingleNode("./fb:last-name", m_NsManager);
            XmlNode nn = xn.SelectSingleNode("./fb:nickname", m_NsManager);
            XmlNodeList hp = xn.SelectNodes("./fb:home-page", m_NsManager);
            XmlNodeList em = xn.SelectNodes("./fb:email", m_NsManager);
            XmlNode id = xn.SelectSingleNode("./fb:id", m_NsManager);

            if( fn != null && ln != null ) {
            	Author = new Author( TextFieldTypeData( fn ), TextFieldTypeData( ln ) );
                if( mn != null ) {
                	Author.MiddleName = TextFieldTypeData( mn );
                }
            	if( nn != null ) {
                	Author.NickName = TextFieldTypeData( nn );
                }
            }
            else {
            	Author = new Author( TextFieldTypeData( nn ) );
            }

            if( id != null ) {
                Author.ID = id.InnerText;
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

            return Author;
            #endregion
        }
        
        private Sequence GetSequence( XmlNode node )
        {
            // извлечение информации по author
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
                    sequence.Number = Convert.ToUInt32( node.Attributes["number"].Value) ;
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

        #endregion
	}
}
