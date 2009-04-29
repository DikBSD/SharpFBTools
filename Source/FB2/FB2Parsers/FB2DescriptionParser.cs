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
using System.Text.RegularExpressions;
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
	/// Description of FB2DescriptionParser.
	/// </summary>
	public class FB2DescriptionParser
	{
		#region Закрытые данные класса
		private XmlNamespaceManager m_NsManager;
        private XmlDocument			m_RawData;
        #endregion
        
		#region Конструкторы класса
        public FB2DescriptionParser()
        {
        	m_RawData = new XmlDocument();
        }
        #endregion
        
        #region Открытые свойства класса
        public XmlDocument RawData
        {
            get { return m_RawData; }
        }
        #endregion
        
        #region Открытые методы класса
        public Description.Description Parse( string sFB2Path )
        {
        	/// TODO доделать
            m_RawData.Load( sFB2Path );


            m_NsManager = new XmlNamespaceManager( m_RawData.NameTable );
            m_NsManager.AddNamespace("fb", "http://www.gribuser.ru/xml/fictionbook/2.0");
            //            nsManager.AddNamespace("genre", "http://www.gribuser.ru/xml/fictionbook/2.0/genres");
            //            nsManager.AddNamespace("link", "http://www.w3.org/1999/xlink");
        	
        	// loading description element
            TitleInfo titleInfo =
                TitleInfo(m_RawData.SelectSingleNode("/fb:FictionBook/fb:description/fb:title-info", m_NsManager));

            TitleInfo srcTitleInfo =
                TitleInfo( m_RawData.SelectSingleNode("/fb:FictionBook/fb:description/fb:src-title-info", m_NsManager) );

            DocumentInfo documentInfo =
                DocumentInfo( m_RawData.SelectSingleNode("/fb:FictionBook/fb:description/fb:document-info", m_NsManager) );

            PublishInfo publishInfo =
                PublishInfo( m_RawData.SelectSingleNode("/fb:FictionBook/fb:description/fb:publish-info", m_NsManager) );

            CustomInfo customInfo =
                CustomInfo( m_RawData.SelectSingleNode("/fb:FictionBook/fb:description/fb:custom-info", m_NsManager) );

            return new Description.Description( titleInfo, srcTitleInfo, documentInfo, publishInfo, customInfo );
        }
        #endregion
        
        
        private TitleInfo TitleInfo( XmlNode elem )
        {
            if( elem == null ) {
                return null;
            }

            // loading genres
            XmlNodeList xmlNodes = elem.SelectNodes("./fb:genre", m_NsManager);
            IList<Genre> genres = new List<Genre>();
            foreach( XmlNode node in xmlNodes ) {
                genres.Add( Genre( node ) );
            }

            // loading authors
            xmlNodes = elem.SelectNodes("./fb:author", m_NsManager);
            IList<Author> authors = new List<Author>();
            foreach (XmlNode node in xmlNodes)
            {
                Author author = Author(node);
                authors.Add(author);
            }

            // loading book title
            BookTitle bookTitle = TextFieldType<BookTitle>(elem.SelectSingleNode("./fb:book-title", m_NsManager));

            // loading annotation
            Annotation annotation =
                AnnotationType<Annotation>(elem.SelectSingleNode("./fb:annotation", m_NsManager));

            // loading keywords
            Keywords keywords = TextFieldType<Keywords>(elem.SelectSingleNode("./fb:keywords", m_NsManager));

            // loading data
            XmlNode xmlNode = elem.SelectSingleNode("./fb:date", m_NsManager);
            Date date = new Date();
            if (xmlNode != null) {
                date.Text = xmlNode.InnerText;
                if (xmlNode.Attributes["value"] != null) {
            	    date.Value = xmlNode.Attributes["value"].Value;
  	        	}
                if (xmlNode.Attributes["lang"] != null) {
            	    date.Lang = xmlNode.Attributes["lang"].Value;
  	        	}
            }

            // loading coverpage
            xmlNode = elem.SelectSingleNode("./fb:coverpage", m_NsManager);
            Coverpage coverpage = new Coverpage();
            if (xmlNode != null) {
				coverpage.Value = xmlNode.InnerXml;
            }
            
            string lang = elem.SelectSingleNode("./fb:lang", m_NsManager).InnerText;

            string srcLang = "";
            xmlNode = elem.SelectSingleNode("./fb:src-lang", m_NsManager);
            if (xmlNode != null)
            {
                srcLang = xmlNode.InnerText;
            }

            // loading translators
            IList<Author> tranlators = null;
            xmlNodes = elem.SelectNodes("./fb:translator", m_NsManager);
            if (xmlNodes.Count > 0)
            {
                tranlators = new List<Author>();
                foreach (XmlNode node in xmlNodes)
                {
                    Author translator = Author(node);
                    tranlators.Add(translator);
                }
            }

            // loading sequences
            IList<Sequence> sequences = null;
            xmlNodes = elem.SelectNodes("./fb:sequence", m_NsManager);
            if (xmlNodes.Count > 0)
            {
                sequences = new List<Sequence>();
                foreach (XmlNode node in xmlNodes)
                {
                    sequences.Add(Sequence(node));
                }
            }

            // TODO: реализовать поддержку coverpage

            return
                new TitleInfo(genres, authors, bookTitle, annotation, keywords, date, coverpage, lang, srcLang, tranlators,
                              sequences);
        }

        private PublishInfo PublishInfo(XmlNode elem)
        {
            if (elem == null)
            {
                return null;
            }

            BookName bookName = TextFieldType<BookName>(elem.SelectSingleNode("./fb:book-name", m_NsManager));
            Publisher publisher = TextFieldType<Publisher>(elem.SelectSingleNode("./fb:publisher", m_NsManager));
            City city = TextFieldType<City>(elem.SelectSingleNode("./fb:city", m_NsManager));

            string year = null;
            XmlNode xmlNode = elem.SelectSingleNode("./fb:year", m_NsManager);
            if (xmlNode != null)
            {
                year = xmlNode.InnerText;
            }
            ISBN isbn = TextFieldType<ISBN>(elem.SelectSingleNode("./fb:isbn", m_NsManager));

            // loading sequences
            IList<Sequence> sequences = null;
            XmlNodeList xmlNodes = elem.SelectNodes("./fb:sequence", m_NsManager);
            if (xmlNodes.Count > 0)
            {
                sequences = new List<Sequence>();
                foreach (XmlNode node in xmlNodes)
                {
                    sequences.Add(Sequence(node));
                }
            }

            return new PublishInfo(bookName, publisher, city, year, isbn, sequences);
        }

        private DocumentInfo DocumentInfo(XmlNode elem)
        {
            if (elem == null)
            {
                return null;
            }

            // loading authors
            XmlNodeList xmlNodes = elem.SelectNodes("./fb:author", m_NsManager);
            IList<Author> authors = new List<Author>();
            foreach (XmlNode node in xmlNodes)
            {
                Author author = Author(node);
                authors.Add(author);
            }

            // loading data
            XmlNode xmlNode = elem.SelectSingleNode("./fb:date", m_NsManager);
            Date date = new Date();
            if (xmlNode != null) {
                date.Text = xmlNode.InnerText;
                if (xmlNode.Attributes["value"] != null) {
            	    date.Value = xmlNode.Attributes["value"].Value;
  	        	}
                if (xmlNode.Attributes["lang"] != null) {
            	    date.Lang = xmlNode.Attributes["lang"].Value;
  	        	}
            }

            // loading data
            string id = elem.SelectSingleNode("./fb:id", m_NsManager).InnerText;

            // loading version
            string version = elem.SelectSingleNode("./fb:version", m_NsManager).InnerText;

            // loading data
            ProgramUsed programUsed = new ProgramUsed();
            xmlNode = elem.SelectSingleNode("./fb:program-used", m_NsManager);
            if (xmlNode != null)
            {
                programUsed.Value = xmlNode.InnerText;
            }

            // loading source urls
            IList<string> srcUrls = null;
            xmlNodes = elem.SelectNodes("./fb:src-url", m_NsManager);
            if (xmlNodes.Count > 0)
            {
                srcUrls = new List<string>();
                foreach (XmlNode node in xmlNodes)
                {
                    srcUrls.Add(node.InnerText);
                }
            }

            SrcOCR srcOcr = new SrcOCR();
            xmlNode = elem.SelectSingleNode("./fb:src-ocr", m_NsManager);
            if (xmlNode != null)
            {
                srcOcr.Value = xmlNode.InnerText;
            }

            History history = AnnotationType<History>(elem.SelectSingleNode("./fb:history", m_NsManager));
            return new DocumentInfo(authors, programUsed, date, srcUrls, srcOcr, id, version, history );
        }

        private CustomInfo CustomInfo(XmlNode node)
        {
            if (node == null)
            {
                return null;
            }
            CustomInfo customInfo = new CustomInfo(node.InnerText, node.Attributes["info-type"].Value);
            if (node.Attributes["lang"] != null)
            {
                customInfo.Lang = node.Attributes["lang"].Value;
            }
            return customInfo;
        }

        private Genre Genre(XmlNode elem)
        {
            Genre genre = null;
            try
            {
                genre = new Genre((Genres) Enum.Parse(typeof (Genres), elem.InnerText));
            }
            catch (ArgumentException e)
            {
 //               throw new FB2ParserException("Unknown genre.", e);
            }
            if (elem.Attributes["match"] != null)
            {
                try
                {
                    genre.Math = Convert.ToUInt32(elem.Attributes["match"].Value);
                }
                catch (Exception)
                {
                    genre.Math = 100;
                }
            }


            return genre;
        }

        private Author Author(XmlNode elem)
        {
            if (elem == null)
            {
                return null;
            }

            Author Author;
            Regex re = new Regex("(^ )|( $)");

            XmlNode firstName = elem.SelectSingleNode("./fb:first-name", m_NsManager);
            XmlNode middleName = elem.SelectSingleNode("./fb:middle-name", m_NsManager);
            XmlNode lastName = elem.SelectSingleNode("./fb:last-name", m_NsManager);
            XmlNode nickName = elem.SelectSingleNode("./fb:nickname", m_NsManager);
            XmlNodeList rawHomePages = elem.SelectNodes("./fb:home-page", m_NsManager);
            XmlNodeList rawEmails = elem.SelectNodes("./fb:email", m_NsManager);
            XmlNode id = elem.SelectSingleNode("./fb:id", m_NsManager);

            if (firstName != null && lastName != null)
            {
                Author = new Author(firstName.InnerText, lastName.InnerText);

                if (nickName != null)
                {
                    Author.NickName = nickName.InnerText;
                }

                if (middleName != null)
                {
                    Author.MiddleName = middleName.InnerText;
                }
            }
            else
            {
                Author = new Author(nickName.InnerText);
            }

            if (id != null)
            {
                Author.ID = id.InnerText;
            }

            if (rawHomePages.Count > 0)
            {
                IList<string> homePages = new List<string>();
                foreach (XmlNode node in rawHomePages)
                {
                    homePages.Add(node.InnerText);
                }
                Author.HomePages = homePages;
            }

            if (rawEmails.Count > 0)
            {
                IList<string> emails = new List<string>();
                foreach (XmlNode node in rawEmails)
                {
                    emails.Add(node.InnerText);
                }
                Author.Emails = emails;
            }


            if (Author.FirstName != null)
            {
                Author.FirstName = re.Replace(Author.FirstName, "");
            }
            if (Author.MiddleName != null)
            {
                Author.MiddleName = re.Replace(Author.MiddleName, "");
            }
            if (Author.LastName != null)
            {
                Author.LastName = re.Replace(Author.LastName, "");
            }
            if (Author.NickName != null)
            {
                Author.NickName = re.Replace(Author.NickName, "");
            }
//            string sid = Author.ID; // get Id for count

            return Author;
        }

        private T IAttrLang<T>(XmlNode xmlNode) where T : IAttrLang, new()
        {
            if (xmlNode == null)
            {
                return default(T);
            }

            T textField = new T();
            //textField.Value = xmlNode.InnerText;
            if (xmlNode.Attributes["lang"] != null)
            {
                textField.Lang = xmlNode.Attributes["lang"].Value;
            }
            return textField;
        }
        
        private T TextFieldType<T>(XmlNode xmlNode) where T : ITextFieldType, new()
        {
            if (xmlNode == null)
            {
                return default(T);
            }

            T textField = new T();
            textField.Value = xmlNode.InnerText;
            if (xmlNode.Attributes["lang"] != null)
            {
                textField.Lang = xmlNode.Attributes["lang"].Value;
            }
            return textField;
        }

        private T AnnotationType<T>(XmlNode xmlNode) where T : IAnnotationType, new()
        {
            T annotation = default(T);
            if (xmlNode != null)
            {
                annotation = new T();
                annotation.Value = xmlNode.InnerXml;
                if (xmlNode.Attributes["id"] != null)
                {
                    annotation.Id = xmlNode.Attributes["id"].Value;
                }
                if (xmlNode.Attributes["lang"] != null)
                {
                    annotation.Lang = xmlNode.Attributes["lang"].Value;
                }
            }
            return annotation;
        }

        private Sequence Sequence(XmlNode node)
        {
            Sequence sequence = null;
            try
            {
                sequence = new Sequence(node.Attributes["name"].Value);
            }
            catch (NullReferenceException e)
            {
//                throw new FB2ParserException("Attribute NAME in tag SEQUENCE not found.", e);
            }
            if (node.Attributes["number"] != null)
            {
                try
                {
                    sequence.Number = Convert.ToUInt32(node.Attributes["number"].Value);
                }
                catch (FormatException e)
                {
 //                   throw new FB2ParserException("Attribute NUMBER in tag SEQUENCE was not in a correct format.", e);
                }
            }
            if (node.Attributes["lang"] != null)
            {
                sequence.Lang = node.Attributes["lang"].Value;
            }
            return sequence;
        }
	}
}
