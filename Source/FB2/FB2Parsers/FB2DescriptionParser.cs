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
        	m_xmlDoc.Load( sFB2Path );

            m_NsManager = new XmlNamespaceManager( m_xmlDoc.NameTable );
            m_NsManager.AddNamespace("fb", "http://www.gribuser.ru/xml/fictionbook/2.0");

            TitleInfo titleInfo = GetTitleInfo(m_xmlDoc.SelectSingleNode("/fb:FictionBook/fb:description/fb:title-info", m_NsManager));
            TitleInfo srcTitleInfo = GetTitleInfo( m_xmlDoc.SelectSingleNode("/fb:FictionBook/fb:description/fb:src-title-info", m_NsManager) );
            DocumentInfo documentInfo = GetDocumentInfo( m_xmlDoc.SelectSingleNode("/fb:FictionBook/fb:description/fb:document-info", m_NsManager) );
            PublishInfo publishInfo = GetPublishInfo( m_xmlDoc.SelectSingleNode("/fb:FictionBook/fb:description/fb:publish-info", m_NsManager) );
            CustomInfo customInfo = GetCustomInfo( m_xmlDoc.SelectSingleNode("/fb:FictionBook/fb:description/fb:custom-info", m_NsManager) );

            return new Description.Description( titleInfo, srcTitleInfo, documentInfo, publishInfo, customInfo );
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

            // loading genres
            XmlNodeList xmlNodes = xn.SelectNodes("./fb:genre", m_NsManager);
            IList<Genre> genres = new List<Genre>();
            foreach( XmlNode node in xmlNodes ) {
                genres.Add( GetGenre( node ) );
            }

            // loading authors
            xmlNodes = xn.SelectNodes("./fb:author", m_NsManager);
            IList<Author> authors = new List<Author>();
            foreach (XmlNode node in xmlNodes)
            {
                Author author = GetAuthor(node);
                authors.Add(author);
            }

            // loading book title
            BookTitle bookTitle = TextFieldType<BookTitle>(xn.SelectSingleNode("./fb:book-title", m_NsManager));

            // loading annotation
            Annotation annotation =
                AnnotationType<Annotation>(xn.SelectSingleNode("./fb:annotation", m_NsManager));

            // loading keywords
            Keywords keywords = TextFieldType<Keywords>(xn.SelectSingleNode("./fb:keywords", m_NsManager));

            // loading data
            XmlNode xmlNode = xn.SelectSingleNode("./fb:date", m_NsManager);
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
            xmlNode = xn.SelectSingleNode("./fb:coverpage", m_NsManager);
            Coverpage coverpage = new Coverpage();
            if (xmlNode != null) {
				coverpage.Value = xmlNode.InnerXml;
            }
            
            string lang = xn.SelectSingleNode("./fb:lang", m_NsManager).InnerText;

            string srcLang = "";
            xmlNode = xn.SelectSingleNode("./fb:src-lang", m_NsManager);
            if (xmlNode != null)
            {
                srcLang = xmlNode.InnerText;
            }

            // loading translators
            IList<Author> tranlators = null;
            xmlNodes = xn.SelectNodes("./fb:translator", m_NsManager);
            if (xmlNodes.Count > 0)
            {
                tranlators = new List<Author>();
                foreach (XmlNode node in xmlNodes)
                {
                    Author translator = GetAuthor(node);
                    tranlators.Add(translator);
                }
            }

            // loading sequences
            IList<Sequence> sequences = null;
            xmlNodes = xn.SelectNodes("./fb:sequence", m_NsManager);
            if (xmlNodes.Count > 0)
            {
                sequences = new List<Sequence>();
                foreach (XmlNode node in xmlNodes)
                {
                    sequences.Add( GetSequence(node) );
                }
            }

            // TODO: реализовать поддержку coverpage

            return
                new TitleInfo(genres, authors, bookTitle, annotation, keywords, date, coverpage, lang, srcLang, tranlators,
                              sequences);
            #endregion
        }
        
        private DocumentInfo GetDocumentInfo( XmlNode xn )
        {
            // извлечение информации по document-info
        	#region Код
        	if( xn == null ) {
                return null;
            }

            // loading authors
            XmlNodeList xmlNodes = xn.SelectNodes("./fb:author", m_NsManager);
            IList<Author> authors = new List<Author>();
            foreach (XmlNode node in xmlNodes)
            {
                Author author = GetAuthor(node);
                authors.Add(author);
            }

            // loading data
            XmlNode xmlNode = xn.SelectSingleNode("./fb:date", m_NsManager);
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
            string id = xn.SelectSingleNode("./fb:id", m_NsManager).InnerText;

            // loading version
            string version = xn.SelectSingleNode("./fb:version", m_NsManager).InnerText;

            // loading data
            ProgramUsed programUsed = new ProgramUsed();
            xmlNode = xn.SelectSingleNode("./fb:program-used", m_NsManager);
            if (xmlNode != null)
            {
                programUsed.Value = xmlNode.InnerText;
            }

            // loading source urls
            IList<string> srcUrls = null;
            xmlNodes = xn.SelectNodes("./fb:src-url", m_NsManager);
            if (xmlNodes.Count > 0)
            {
                srcUrls = new List<string>();
                foreach (XmlNode node in xmlNodes)
                {
                    srcUrls.Add(node.InnerText);
                }
            }

            SrcOCR srcOcr = new SrcOCR();
            xmlNode = xn.SelectSingleNode("./fb:src-ocr", m_NsManager);
            if (xmlNode != null)
            {
                srcOcr.Value = xmlNode.InnerText;
            }

            History history = AnnotationType<History>(xn.SelectSingleNode("./fb:history", m_NsManager));
            return new DocumentInfo(authors, programUsed, date, srcUrls, srcOcr, id, version, history );
            #endregion
        }
        
        private PublishInfo GetPublishInfo( XmlNode xn )
        {
            // извлечение информации по publish-info
        	#region Код
            if( xn == null ) {
                return null;
            }

            BookName bookName = TextFieldType<BookName>(xn.SelectSingleNode("./fb:book-name", m_NsManager));
            Publisher publisher = TextFieldType<Publisher>(xn.SelectSingleNode("./fb:publisher", m_NsManager));
            City city = TextFieldType<City>(xn.SelectSingleNode("./fb:city", m_NsManager));

            string year = null;
            XmlNode xmlNode = xn.SelectSingleNode("./fb:year", m_NsManager);
            if (xmlNode != null)
            {
                year = xmlNode.InnerText;
            }
            ISBN isbn = TextFieldType<ISBN>(xn.SelectSingleNode("./fb:isbn", m_NsManager));

            // loading sequences
            IList<Sequence> sequences = null;
            XmlNodeList xmlNodes = xn.SelectNodes("./fb:sequence", m_NsManager);
            if (xmlNodes.Count > 0)
            {
                sequences = new List<Sequence>();
                foreach (XmlNode node in xmlNodes)
                {
                    sequences.Add( GetSequence(node) );
                }
            }

            return new PublishInfo(bookName, publisher, city, year, isbn, sequences);
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
            if (node.Attributes["lang"] != null)
            {
                customInfo.Lang = node.Attributes["lang"].Value;
            }
            return customInfo;
            #endregion
        }


        private TextFieldType TextFieldTypeData( XmlNode node )
        {
        	// извлечение информации по TextFieldType
        	#region Код
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
            Genre genre = null;
            try {
                genre = new Genre( (Genres) Enum.Parse(typeof (Genres), xn.InnerText) );
            } catch ( ArgumentException e ) {
 //               throw new FB2ParserException("Unknown genre.", e);
            }
            if (xn.Attributes["match"] != null)
            {
                try
                {
                    genre.Math = Convert.ToUInt32(xn.Attributes["match"].Value);
                }
                catch (Exception)
                {
                    genre.Math = 100;
                }
            }

            return genre;
            #endregion
        }

        private Author GetAuthor(XmlNode xn)
        {
            // извлечение информации по author
        	#region Код
            if( xn == null ) {
                return null;
            }

            Author Author;
            Regex re = new Regex("(^ )|( $)");

            XmlNode firstName = xn.SelectSingleNode("./fb:first-name", m_NsManager);
            XmlNode middleName = xn.SelectSingleNode("./fb:middle-name", m_NsManager);
            XmlNode lastName = xn.SelectSingleNode("./fb:last-name", m_NsManager);
            XmlNode nickName = xn.SelectSingleNode("./fb:nickname", m_NsManager);
            XmlNodeList rawHomePages = xn.SelectNodes("./fb:home-page", m_NsManager);
            XmlNodeList rawEmails = xn.SelectNodes("./fb:email", m_NsManager);
            XmlNode id = xn.SelectSingleNode("./fb:id", m_NsManager);

            if (firstName != null && lastName != null) {
            	Author = new Author( TextFieldTypeData( firstName ), TextFieldTypeData( lastName ) );
                if (nickName != null) {
                	Author.NickName = TextFieldTypeData( nickName );
                }
                if (middleName != null) {
                	Author.MiddleName = TextFieldTypeData( middleName );
                }
            }
            else {
            	Author = new Author( TextFieldTypeData( nickName ) );
            }

            if (id != null) {
                Author.ID = id.InnerText;
            }

            if (rawHomePages.Count > 0) {
                IList<string> homePages = new List<string>();
                foreach (XmlNode node in rawHomePages) {
                    homePages.Add(node.InnerText);
                }
                Author.HomePages = homePages;
            }

            if (rawEmails.Count > 0) {
                IList<string> emails = new List<string>();
                foreach (XmlNode node in rawEmails)
                {
                    emails.Add(node.InnerText);
                }
                Author.Emails = emails;
            }


            if (Author.FirstName != null)
            {
                Author.FirstName.Value = re.Replace(Author.FirstName.Value, "");
            }
            if (Author.MiddleName != null)
            {
                Author.MiddleName.Value = re.Replace(Author.MiddleName.Value, "");
            }
            if (Author.LastName != null)
            {
                Author.LastName.Value = re.Replace(Author.LastName.Value, "");
            }
            if (Author.NickName != null)
            {
                Author.NickName.Value = re.Replace(Author.NickName.Value, "");
            }
//            string sid = Author.ID; // get Id for count

            return Author;
            #endregion
        }
        
        private Sequence GetSequence(XmlNode node)
        {
            // извлечение информации по author
            #region Код
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
            #endregion
        }
                
        private T TextFieldType<T>(XmlNode xmlNode) where T : ITextFieldType, new()
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
        #endregion
	}
}
