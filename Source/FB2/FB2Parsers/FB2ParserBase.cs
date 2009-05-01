/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 01.05.2009
 * Time: 14:26
 * 
 * * License: GPL 2.1
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

namespace FB2.FB2Parsers
{
	/// <summary>
	/// Description of FB2ParserBase.
	/// </summary>
	public class FB2ParserBase : TextFieldTypeData
	{
		#region Закрытые данные класса
		private XmlNamespaceManager m_NsManager;
        private XmlDocument			m_xmlDoc;
        private string				m_aFBNamespace	= "http://www.gribuser.ru/xml/fictionbook/2.0";
        #endregion
		
		#region Конструкторы класса
        public FB2ParserBase( string sFB2Path )
		{
			m_xmlDoc = new XmlDocument();
        	m_xmlDoc.Load( sFB2Path );
            m_NsManager = new XmlNamespaceManager( m_xmlDoc.NameTable );
            m_NsManager.AddNamespace( "fb", m_aFBNamespace );
		}
        #endregion
        
        #region Открытые свойства класса
        public XmlDocument XmlDoc
        {
            get { return m_xmlDoc; }
        }
        #endregion
        
        #region Закрытые вспомогательные основные методы класса
        private TitleInfo TitleInfo( bool bTitleInfo )
        {
        	// извлечение информации по title-info
        	#region Код
        	XmlNode xn = null;
        	if( bTitleInfo ) {
        		xn = m_xmlDoc.SelectSingleNode( "/fb:FictionBook/fb:description/fb:title-info", m_NsManager );
        	} else {
        		xn = m_xmlDoc.SelectSingleNode( "/fb:FictionBook/fb:description/fb:src-title-info", m_NsManager );
        	}
        	
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
			AnnotationTypeData atd = new AnnotationTypeData();
            Annotation annotation = atd.AnnotationType<Annotation>( xn.SelectSingleNode("./fb:annotation", m_NsManager) );

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
					GetSequences( node, sequences );
                }
            }

            return new TitleInfo( ilGenres, ilAuthors, bookTitle, annotation, keywords, date,
                                 coverpage, sLang, sSrcLang, tranlators, sequences);
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
            	Author = new Author( TextFieldType<TextFieldType>( fn ), TextFieldType<TextFieldType>( ln ) );
            	if( mn != null ) {
                	Author.MiddleName = TextFieldType<TextFieldType>( mn );
                }
            	if( nn != null ) {
                	Author.NickName = TextFieldType<TextFieldType>( nn );
                }
            }
            else {
            	Author = new Author( TextFieldType<TextFieldType>( nn ) );
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
        
		private IList<Sequence> GetSequences( XmlNode xn, IList<Sequence> sequences ) {
        	// извлечение информации по вложенным sequence в sequence
        	#region Код
        	XmlNodeList xmlNodes = xn.SelectNodes("./fb:sequence", m_NsManager);
            if( xmlNodes.Count > 0 ) {
                foreach( XmlNode node in xmlNodes ) {
                    sequences.Add( GetSequence( node ) );
                    GetSequences( node, sequences );
                }
            }
        	return sequences;
        	#endregion
        }
        #endregion
	
        
	}
}
