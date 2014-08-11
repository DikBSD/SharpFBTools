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
using Core.FB2.Description.Common;
using Core.FB2.Binary;

namespace Core.FB2Dublicator
{
	/// <summary>
	/// Парсер для Дубликатора
	/// </summary>
	public class FB2ParserForDup
	{
		#region Закрытые данные класса
		private readonly XmlNamespaceManager	m_NsManager	= null;
        private readonly XmlDocument			m_xmlDoc	= null;
        private const string m_aFB20Namespace = "http://www.gribuser.ru/xml/fictionbook/2.0";
        private const string m_aFB21Namespace = "http://www.gribuser.ru/xml/fictionbook/2.1";
        private string m_ns = "/fb20:";
        #endregion

		#region Конструкторы класса
        public FB2ParserForDup( string sFB2Path )
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
        		throw new System.IO.FileLoadException( "Bad File!" );
        	}
        	
        }
        #endregion
               
        #region Закрытые вспомогательные основные методы класса
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
        #endregion
        
		#region Свойства класса
		public XmlDocument XmlDoc {
            get { return m_xmlDoc; }
        }
		
		public virtual string Id {
        	get {
        		XmlNode xn = m_xmlDoc.SelectSingleNode( m_ns + "FictionBook" + m_ns + "description" + m_ns + "document-info", m_NsManager );
            	if( xn == null ) {
					return null;
           		}
				string id = null;
				XmlNode xmlNode = xn.SelectSingleNode("." + m_ns + "id", m_NsManager);
				if( xmlNode != null ) {
					id = xmlNode.InnerText;
				}
				return id;
			}
        }
		
		public virtual string Version {
        	get {
        		XmlNode xn = m_xmlDoc.SelectSingleNode( m_ns + "FictionBook" + m_ns + "description" + m_ns + "document-info", m_NsManager );
            	if( xn == null ) {
					return null;
           		}
				string version = null;
				XmlNode xmlNode = xn.SelectSingleNode("." + m_ns + "version", m_NsManager);
				if( xmlNode != null ) {
					version = xmlNode.InnerText;
				}
				return version;
			}
        }
		
		public virtual BookTitle BookTitle {
        	get {
				XmlNode xn = null;
				xn = m_xmlDoc.SelectSingleNode( m_ns + "FictionBook" + m_ns + "description" + m_ns + "title-info", m_NsManager );
				if( xn == null ) {
					return null;
				}
				return TextFieldType<BookTitle>( xn.SelectSingleNode("." + m_ns + "book-title", m_NsManager) );
			}
        }
		
		public virtual IList<Author> Authors {
        	get {
        		XmlNode xn = null;
				xn = m_xmlDoc.SelectSingleNode( m_ns + "FictionBook" + m_ns + "description" + m_ns + "title-info", m_NsManager );
				if( xn == null ) {
					return null;
				}
				IList<Author> ilAuthors = null;
				XmlNodeList xmlNodes = xn.SelectNodes("." + m_ns + "author", m_NsManager);
				if( xmlNodes.Count > 0  ) {
					ilAuthors = new List<Author>();
					foreach( XmlNode node in xmlNodes ) {
						Author author = GetAuthor( node );
						ilAuthors.Add( author );
					}
				}
				return ilAuthors;
			}
        }
		
		public virtual IList<Genre> Genres {
        	get {
        		XmlNode xn = null;
				xn = m_xmlDoc.SelectSingleNode( m_ns + "FictionBook" + m_ns + "description" + m_ns + "title-info", m_NsManager );
				if( xn == null ) {
					return null;
				}
				IList<Genre> ilGenres = null;
				XmlNodeList xmlNodes = xn.SelectNodes("." + m_ns + "genre", m_NsManager);
				if( xmlNodes.Count > 0  ) {
					ilGenres = new List<Genre>();
					foreach( XmlNode node in xmlNodes ) {
						ilGenres.Add( GetGenre( node ) );
					}
				}
				return ilGenres;
			}
        }
		#endregion
	}
}
