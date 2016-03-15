/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 24.06.2015
 * Время: 13:52
 * 
 * License: GPL 2.1
 */
using System;
using System.IO;
using System.Xml;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using System.Windows.Forms;

using Core.Common;
using Core.FB2.Common;
using Core.FB2.Description.TitleInfo;
using Core.FB2.Description.DocumentInfo;
using Core.FB2.Description.PublishInfo;
using Core.FB2.Description.CustomInfo;
using Core.FB2.Description.Common;
using Core.FB2.Binary;

using TitleInfoEnum = Core.Common.Enums.TitleInfoEnum;
using AuthorEnum = Core.Common.Enums.AuthorEnum;

namespace Core.FB2.FB2Parsers
{
	/// <summary>
	/// Клас чтения в xml / изменения данных xml fb2 книги
	/// </summary>
	public class FictionBook
	{
		#region Закрытые данные класса
		private readonly string			_fb2Path	= string.Empty;
		private List<XmlNode> _BodyNodesXmlNodeList = new List<XmlNode>(); // Реальные структуры body книги
		private FB2Text _fb2TextXml					= null; // fb2 книга в текстовой xml форме
		
		private readonly	XmlDocument _xmlDoc		= null;
		private const		string _aFB20Namespace	= "http://www.gribuser.ru/xml/fictionbook/2.0";
		private const		string _aFB21Namespace	= "http://www.gribuser.ru/xml/fictionbook/2.1";
		private const		string _aFB22Namespace	= "http://www.gribuser.ru/xml/fictionbook/2.2";
		private				string _ns				= "/fb20:";
		private				string _FBNamespace		= string.Empty;
		private				XmlNamespaceManager	_NsManager	= null;
		#endregion

		public FictionBook( string FB2Path, bool onlyDescription = false )
		{
			_fb2Path = FB2Path;
			_xmlDoc = new XmlDocument();
			try {
				try {
					if ( !onlyDescription )
						_xmlDoc.Load( FB2Path );
					else
						_fb2TextXml = new FB2Text( FB2Path, true );
					setNameSpace();
				} catch {
					_fb2TextXml = new FB2Text( FB2Path );
					_fb2TextXml.ProxyMode = true;
					_xmlDoc.LoadXml( _fb2TextXml.toXML() );
					setNameSpace();
				}
			} catch ( Exception ex ) {
				throw new System.IO.FileLoadException(
					string.Format( "Файл {0}:\nНевозможно открыть для извлечения fb2 метаданных.\n\n{1}", FB2Path, ex.Message )
				);
			}
		}
		
		#region Открытые вспомогательные методы и свойства
		public string getFilePath() {
			return _fb2Path;
		}
		public string getEncoding() {
			string encoding = "UTF-8";
			string str = string.Empty;
			using ( StreamReader reader = File.OpenText( _fb2Path ) ) {
				str = reader.ReadLine();
			}
			
			if ( string.IsNullOrWhiteSpace( str ) || str.Length == 0 )
				return encoding;
			
			Match match = Regex.Match( str, "(?<=encoding=\").+?(?=\")", RegexOptions.IgnoreCase );
			if ( match.Success )
				encoding = match.Value;
			if ( encoding.ToLower() == "wutf-8" || encoding.ToLower() == "utf8" )
				encoding = "utf-8";
			return encoding;
		}
		// удаление пробелов, табуляций, переносов строк в тексте тегов
		public void removeWiteSpaceInTagsText( XmlNode ParrentNode ) {
			foreach ( XmlNode node in ParrentNode.ChildNodes )
				xmlRemoveWiteSpace( node );
		}
		// находится ли экземпляр FictionBook в proxy режиме...
		public virtual bool ProxyMode {
			get {
				if ( _fb2TextXml == null )
					return false;
				else
					return _fb2TextXml.ProxyMode;
			}
		}
		public FB2Text getFB2TextXmlIsExists() {
			return _fb2TextXml;
		}
		// сохранение книги: если экземпляр FictionBook был создан с помощью FB2Text _fb2TextXml,
		// и задействован Proxy режим, то сохраняем методами FB2Text. Если Proxy режим не задействован, то - через _xmlDoc
		public void saveToFB2File( string FilePath, bool PreserveWhitespace = false ) {
			_xmlDoc.PreserveWhitespace = PreserveWhitespace; // если true, то все теги склеиваются в одну строку
			if ( _fb2TextXml == null )
				_xmlDoc.Save( FilePath );
			else {
				_fb2TextXml.ProxyMode = false;
				_fb2TextXml.Description = _fb2TextXml.StartTags + getDescriptionXmlText();
				_fb2TextXml.saveToFile( FilePath );
			}
		}
		public string getDescriptionXmlText() {
			string xmlns20 = " xmlns=\"http://www.gribuser.ru/xml/fictionbook/2.0\"";
			string xmlns21 = " xmlns=\"http://www.gribuser.ru/xml/fictionbook/2.1\"";
			string xmlns22 = " xmlns=\"http://www.gribuser.ru/xml/fictionbook/2.2\"";
			
			string DescXml = getDescriptionNode().InnerXml;
			DescXml = DescXml.Replace( xmlns20, "" ).Replace( xmlns21, "" ).Replace( xmlns22, "" );
			DescXml = Regex.Replace( DescXml, "><", ">\n<", RegexOptions.None );
			return "<description>\n" + DescXml + "\n</description>";
		}
//		// переключение на режим proxy-body / режим реального body
//		public void setBodiesProxyMode( bool ProxyMode ) {
//			if ( ProxyMode ) {
//				// переключение на режим proxy-body
//				foreach ( XmlNode node in getBodyNodes() )
//					_BodyNodesXmlNodeList.Add(node);
//
//				XmlNode xmlFictionBook = getFictionBookNode();
//				XmlNode xmlDesc = getDescriptionNode();
//
//				foreach ( XmlNode node in getBodyNodes() )
//					xmlFictionBook.RemoveChild( node );
//
//				XmlNode xmlBodyProxy = getXmlDoc().CreateElement( getPrefix(), "body", getNamespaceURI() );
//				XmlNode xmlSection = getXmlDoc().CreateElement( getPrefix(), "section", getNamespaceURI() );
//				XmlNode xmlEmptyLine = getXmlDoc().CreateElement( getPrefix(), "empty-line", getNamespaceURI() );
//				xmlSection.AppendChild(xmlEmptyLine);
//				xmlBodyProxy.AppendChild(xmlSection);
//				xmlFictionBook.InsertAfter( xmlBodyProxy, xmlDesc );
//			} else {
//				// переключение на режим реального body
//				if ( _BodyNodesXmlNodeList.Count > 0 ) {
//					XmlNode xmlFictionBook = getFictionBookNode();
//					XmlNode xmlDesc = getDescriptionNode();
//					foreach ( XmlNode node in getBodyNodes() )
//						xmlFictionBook.RemoveChild( node );
//					for ( int i = _BodyNodesXmlNodeList.Count-1; i != -1; --i )
//						xmlFictionBook.InsertAfter( _BodyNodesXmlNodeList[i], xmlDesc );
//				}
//			}
//		}
		#endregion
		
		#region Закрытые вспомогательные методы
		// удаление пробелов, табуляций, переносов строк в тексте тегов
		private void xmlRemoveWiteSpace( XmlNode next ) {
			if ( next.HasChildNodes ) {
				foreach ( XmlNode c in next.ChildNodes ) {
					if ( c.Name == "#text" )
						c.InnerText = c.InnerText.Trim();
					xmlRemoveWiteSpace(c);
				}
			}
		}
		
		private void setNameSpace() {
			_NsManager = new XmlNamespaceManager( _xmlDoc.NameTable );
			string fb2FileNamespaceURI = _xmlDoc.DocumentElement.NamespaceURI;
			if( fb2FileNamespaceURI.Equals( _aFB21Namespace ) ) {
				_NsManager.AddNamespace( "fb21", _aFB21Namespace );
				_ns = "/fb21:";
				_FBNamespace = _aFB21Namespace;
			} else if ( fb2FileNamespaceURI.Equals( _aFB22Namespace ) ){
				_NsManager.AddNamespace( "fb22", _aFB22Namespace );
				_ns = "/fb22:";
				_FBNamespace = _aFB22Namespace;
			} else {
				_NsManager.AddNamespace( "fb20", _aFB20Namespace );
				_ns = "/fb20:";
				_FBNamespace = _aFB20Namespace;
			}
			// удаление переносов абзацев и лишних стартовых/конечных пробелов/табуляция в данных тегов описания книги
			removeWiteSpaceInTagsText( getDescriptionNode() );
		}
		#endregion
		
		#region Свойства класса (Получение ДАННЫХ всех элементов структура fb2 файла)
		#region TitleInfo
		public virtual BookTitle TIBookTitle {
			get {
				return getBookTitle( TitleInfoEnum.TitleInfo );
			}
		}
		
		public virtual string TILang {
			get {
				return getLang( TitleInfoEnum.TitleInfo );
			}
		}
		
		public virtual string TISrcLang {
			get {
				return getSrcLang( TitleInfoEnum.TitleInfo );
			}
		}
		
		public virtual Annotation TIAnnotation {
			get {
				return getAnnotation( TitleInfoEnum.TitleInfo );
			}
		}

		public virtual Keywords TIKeywords {
			get {
				return getKeywords( TitleInfoEnum.TitleInfo );
			}
		}

		public virtual Date TIDate {
			get {
				return getDate( TitleInfoEnum.TitleInfo );
			}
		}
		
		public virtual IList<Author> TIAuthors {
			get {
				return getAuthors( TitleInfoEnum.TitleInfo, AuthorEnum.AuthorOfBook );
			}
		}
		
		public virtual IList<Author> TITranslators {
			get {
				return getAuthors( TitleInfoEnum.TitleInfo, AuthorEnum.Translator );
			}
		}
		
		public virtual IList<Genre> TIGenres {
			get {
				return getGenres( TitleInfoEnum.TitleInfo );
			}
		}
		
		public virtual IList<Coverpage> TICoverpages {
			get {
				return getCoverpages( TitleInfoEnum.TitleInfo );
			}
		}
		
		public virtual IList<Sequence> TISequences {
			get {
				return getSequences( TitleInfoEnum.TitleInfo );
			}
		}
		
		#endregion
		
		#region SourceTitleInfo
		public virtual BookTitle STIBookTitle {
			get {
				return getBookTitle( TitleInfoEnum.SourceTitleInfo );
			}
		}
		
		public virtual string STILang {
			get {
				return getLang( TitleInfoEnum.SourceTitleInfo );
			}
		}
		
		public virtual string STISrcLang {
			get {
				return getSrcLang( TitleInfoEnum.SourceTitleInfo );
			}
		}
		
		public virtual Annotation STIAnnotation {
			get {
				return getAnnotation( TitleInfoEnum.SourceTitleInfo );
			}
		}

		public virtual Keywords STIKeywords {
			get {
				return getKeywords( TitleInfoEnum.SourceTitleInfo );
			}
		}

		public virtual Date STIDate {
			get {
				return getDate( TitleInfoEnum.SourceTitleInfo );
			}
		}
		
		public virtual IList<Author> STIAuthors {
			get {
				return getAuthors( TitleInfoEnum.SourceTitleInfo, AuthorEnum.AuthorOfBook );
			}
		}
		
		public virtual IList<Author> STITranslators {
			get {
				return getAuthors( TitleInfoEnum.SourceTitleInfo, AuthorEnum.Translator );
			}
		}
		
		public virtual IList<Genre> STIGenres {
			get {
				return getGenres( TitleInfoEnum.SourceTitleInfo );
			}
		}
		
		public virtual IList<Coverpage> STICoverpages {
			get {
				return getCoverpages( TitleInfoEnum.SourceTitleInfo );
			}
		}
		
		public virtual IList<Sequence> STISequences {
			get {
				return getSequences( TitleInfoEnum.SourceTitleInfo );
			}
		}
		
		#endregion
		
		#region DocumentInfo
		public virtual IList<Author> DIAuthors {
			get {
				return getAuthors( TitleInfoEnum.TitleInfo, AuthorEnum.AuthorOfFB2 );
			}
		}
		
		public virtual Date DIDate {
			get {
				return getDate( getDocumentInfoNode() );
			}
		}
		
		public virtual string DIID {
			get {
				XmlNode xmlDINode = getDocumentInfoNode();
				if( xmlDINode != null ) {
					XmlNode xmlNode = xmlDINode.SelectSingleNode("." + _ns + "id", _NsManager);
					return ( xmlNode != null )
						? xmlNode.InnerText.Trim()
						: null;
				}
				return null;
			}
		}
		
		public virtual string DIVersion {
			get {
				XmlNode xmlDINode = getDocumentInfoNode();
				if( xmlDINode != null ) {
					XmlNode xmlNode = xmlDINode.SelectSingleNode("." + _ns + "version", _NsManager);
					return ( xmlNode != null )
						? xmlNode.InnerText.Trim()
						: null;
				}
				return null;
			}
		}
		
		public virtual ProgramUsed DIProgramUsed {
			get {
				XmlNode xmlDINode = getDocumentInfoNode();
				if( xmlDINode != null ) {
					XmlNode xmlNode = xmlDINode.SelectSingleNode("." + _ns + "program-used", _NsManager);
					return ( xmlNode != null )
						? new ProgramUsed( xmlNode.InnerText )
						: null;
				}
				return null;
			}
		}
		
		public virtual History DIHistory {
			get {
				XmlNode xmlDINode = getDocumentInfoNode();
				if( xmlDINode != null ) {
					XmlNode xmlHistNode = xmlDINode.SelectSingleNode("." + _ns + "history", _NsManager);
					return ( xmlHistNode != null )
						? AnnotationType<History>( xmlHistNode )
						: null;
				}
				return null;
			}
		}

		public virtual IList<string> DISrcUrls {
			get {
				IList<string> ilSrcUrls = null;
				XmlNode xmlDINode = getDocumentInfoNode();
				if( xmlDINode != null ) {
					XmlNodeList xmlNodes = xmlDINode.SelectNodes("." + _ns + "src-url", _NsManager);
					if( xmlNodes != null ) {
						if( xmlNodes.Count > 0  ) {
							ilSrcUrls = new List<string>();
							foreach( XmlNode node in xmlNodes )
								ilSrcUrls.Add( node.InnerText.Trim() );
						}
					}
				}
				return ilSrcUrls;
			}
		}
		
		public virtual SrcOCR DISrcOCR{
			get {
				XmlNode xmlDINode = getDocumentInfoNode();
				if( xmlDINode != null ) {
					XmlNode xmlNode = xmlDINode.SelectSingleNode("." + _ns + "src-ocr", _NsManager);
					return ( xmlNode != null )
						? new SrcOCR( xmlNode.InnerText.Trim() )
						: null;
				}
				return null;
			}
		}

		#endregion
		
		#region PublishInfo
		public virtual BookName PIBookName {
			get {
				XmlNode xmlPINode = getPublishInfoNode();
				if( xmlPINode != null ) {
					XmlNode xmlNode = xmlPINode.SelectSingleNode("." + _ns + "book-name", _NsManager);
					return ( xmlNode != null )
						? TextFieldType<BookName>( xmlNode )
						: null;
				}
				return null;
			}
		}
		
		public virtual Publisher PIPublisher {
			get {
				XmlNode xmlPINode = getPublishInfoNode();
				if( xmlPINode != null ) {
					XmlNode xmlNode = xmlPINode.SelectSingleNode("." + _ns + "publisher", _NsManager);
					return ( xmlNode != null )
						? TextFieldType<Publisher>( xmlNode )
						: null;
				}
				return null;
			}
		}

		public virtual City PICity {
			get {
				XmlNode xmlPINode = getPublishInfoNode() ;
				if( xmlPINode != null ) {
					XmlNode xmlNode = xmlPINode.SelectSingleNode("." + _ns + "city", _NsManager);
					return ( xmlNode != null )
						? TextFieldType<City>( xmlNode )
						: null;
				}
				return null;
			}
		}
		
		public virtual ISBN PIISBN {
			get {
				XmlNode xmlPINode = getPublishInfoNode();
				if( xmlPINode != null ) {
					XmlNode xmlNode = xmlPINode.SelectSingleNode("." + _ns + "isbn", _NsManager);
					return ( xmlNode != null )
						? TextFieldType<ISBN>( xmlNode )
						: null;
				}
				return null;
			}
		}
		
		public virtual string PIYear {
			get {
				XmlNode xmlPINode = getPublishInfoNode();
				if( xmlPINode != null ) {
					XmlNode xmlYearNode = xmlPINode.SelectSingleNode("." + _ns + "year", _NsManager);
					return ( xmlYearNode != null )
						? xmlYearNode.InnerText.Trim()
						: null;
				}
				return null;
			}
		}

		public virtual IList<Sequence> PISequences {
			get {
				IList<Sequence> ilSequences = null;
				XmlNode xmlPINode = getPublishInfoNode();
				if( xmlPINode != null ) {
					XmlNodeList xmlNodes = xmlPINode.SelectNodes("." + _ns + "sequence", _NsManager);
					if(xmlNodes != null) {
						if(xmlNodes.Count > 0) {
							ilSequences = new List<Sequence>();
							foreach( XmlNode node in xmlNodes ) {
								ilSequences.Add( getSequence(node) );
								getSequences( node, ilSequences );
							}
						}
					}
				}
				return ilSequences;
			}
		}

		#endregion
		
		#region CustomInfo
		public virtual IList<CustomInfo> CICustomInfo {
			get {
				return getCustomInfo();
			}
		}
		#endregion
		#endregion

		#region Открытые методы получения ДАННЫХ главных разделов fb2-файла
		public TitleInfo getTitleInfo()
		{
			return TitleInfo( TitleInfoEnum.TitleInfo );
		}
		
		public TitleInfo getSourceTitleInfo()
		{
			return TitleInfo( TitleInfoEnum.SourceTitleInfo );
		}
		
		public DocumentInfo getDocumentInfo()
		{
			return new DocumentInfo( DIAuthors, DIProgramUsed, DIDate, DISrcUrls, DISrcOCR, DIID, DIVersion, DIHistory );
		}
		
		public PublishInfo getPublishInfo()
		{
			return new PublishInfo( PIBookName, PIPublisher, PICity, PIYear, PIISBN, PISequences );
		}
		
		public IList<CustomInfo> getCustomInfo()
		{
			IList<CustomInfo> ilCustomInfos = null;
			
			XmlNodeList xmlNodes = getCustomInfoNode();
			if( xmlNodes == null ) return null;
			
			if( xmlNodes.Count > 0  ) {
				ilCustomInfos = new List<CustomInfo>();
				foreach( XmlNode node in xmlNodes ) {
					CustomInfo customInfo = null;
					if( node.Attributes["info-type"] != null )
						customInfo = new CustomInfo( node.InnerText, node.Attributes["info-type"].Value.Trim());
					else
						customInfo = new CustomInfo( node.InnerText, null );

					if( node.Attributes["lang"] != null )
						customInfo.Lang = node.Attributes["lang"].Value.Trim();

					ilCustomInfos.Add( customInfo );
				}
			}
			
			return ilCustomInfos;
		}
		
		public Description.Description getDescription()
		{
			return new Description.Description(
				getTitleInfo(), getSourceTitleInfo(), getDocumentInfo(), getPublishInfo(), getCustomInfo()
			);
		}
		
		// полное описание картинок (id, content-type и base64 значение)
		public IList<BinaryBase64> getCoversBase64( TitleInfoEnum TitleInfoType )
		{
			XmlNode xmlTINode = getTitleInfoNode( TitleInfoType );
			if( xmlTINode == null )
				return null;

			IList<BinaryBase64> BinaryBase64List = null;
			BinaryBase64 binaryBase64 = null;
			// список всех Обложек
			IList<Coverpage> ilCoverpages = getCoverpages( TitleInfoType );
			if( ilCoverpages != null && ilCoverpages.Count > 0) {
				XmlNodeList xmlNodes = xmlTINode.SelectNodes(_ns + "FictionBook" + _ns + "binary", _NsManager);
				BinaryBase64List = new List<BinaryBase64>();
				if(xmlNodes != null) {
					for( int i = 0; i != ilCoverpages.Count; ++i ) {
						// извлечение информации по binary, в зависимости от атрибута id бинарного объекта
						foreach( XmlNode node in xmlNodes ) {
							if( node.Attributes["id"] != null && node.Attributes["content-type"] != null ) {
								if( ilCoverpages[i].Value == node.Attributes["id"].Value )
									binaryBase64 = new BinaryBase64(
										ilCoverpages[i].Value, node.Attributes["content-type"].Value, node.InnerText
									);
							}
						}
						BinaryBase64List.Add( binaryBase64 );
						if( BinaryBase64List.Count == ilCoverpages.Count )
							break;
					}
				}
			}
			return BinaryBase64List;
		}
		
		public string getBase64ForID( string ID )
		{
			// извлечение информации по binary, в зависимости от атрибута id бинарного объекта
			XmlNode xmlNode = getBinaryNodeForID( ID );
			return xmlNode != null
				? xmlNode.InnerText.Trim()
				: null;
		}
		
		public string getFB2Namespace() {
			return 	_FBNamespace;
		}
		
		public string getNamespace() {
			return 	_ns;
		}
		
		public string getPrefix() {
			return 	_xmlDoc.DocumentElement.Prefix.Trim();
		}
		
		public string getNamespaceURI() {
			return _xmlDoc.DocumentElement.NamespaceURI.Trim();
		}

		public XmlNamespaceManager getNamespaceManager() {
			return 	_NsManager;
		}
		#endregion
		
		#region Открытые методы ПОЛУЧЕНИЯ xml УЗЛОВ главных разделов fb2 структуры
		public XmlDocument getXmlDoc() {
			return _xmlDoc;
		}
		public XmlNode getFictionBookNode() {
			return _xmlDoc.SelectSingleNode( _ns + "FictionBook", _NsManager );
		}
		public XmlNode getDescriptionNode() {
			XmlNode fb = getFictionBookNode();
			return fb != null
				? fb.SelectSingleNode( "." + _ns + "description", _NsManager )
				: null;
		}

		// title-info (source-title-info) nodes
		public XmlNode getTitleInfoNode( TitleInfoEnum TitleInfoType ) {
			XmlNode xn = null;
			XmlNode fb = getFictionBookNode();
			if( fb != null ) {
				XmlNode desc = getDescriptionNode();
				if( desc != null ) {
					if( TitleInfoType == TitleInfoEnum.TitleInfo )
						xn = desc.SelectSingleNode( "." + _ns + "title-info", _NsManager );
					else
						xn = desc.SelectSingleNode( "." + _ns + "src-title-info", _NsManager );
				}
			}
			return xn;
		}
		public XmlNodeList getGenreNodes( TitleInfoEnum TitleInfoType ) {
			XmlNode fb2TINode = getTitleInfoNode( TitleInfoType );
			return fb2TINode != null
				? fb2TINode.SelectNodes( "." + _ns + "genre", _NsManager )
				: null;
		}
		public XmlNodeList getAuthorNodes( TitleInfoEnum TitleInfoType ) {
			XmlNode fb2TINode = getTitleInfoNode( TitleInfoType );
			return fb2TINode != null
				? fb2TINode.SelectNodes( "." + _ns + "author", _NsManager )
				: null;
		}
		public XmlNodeList getTranslatorNodes( TitleInfoEnum TitleInfoType ) {
			XmlNode fb2TINode = getTitleInfoNode( TitleInfoType );
			return fb2TINode != null
				? fb2TINode.SelectNodes( "." + _ns + "translator", _NsManager )
				: null;
		}
		public XmlNode getBookTitleNode( TitleInfoEnum TitleInfoType ) {
			XmlNode fb2TINode = getTitleInfoNode( TitleInfoType );
			return fb2TINode != null
				? fb2TINode.SelectSingleNode( "." + _ns + "book-title", _NsManager )
				: null;
		}
		public XmlNode getLangNode( TitleInfoEnum TitleInfoType ) {
			XmlNode fb2TINode = getTitleInfoNode( TitleInfoType );
			return fb2TINode != null
				? fb2TINode.SelectSingleNode( "." + _ns + "lang", _NsManager )
				: null;
		}
		public XmlNode getSrcLangNode( TitleInfoEnum TitleInfoType ) {
			XmlNode fb2TINode = getTitleInfoNode( TitleInfoType );
			return fb2TINode != null
				? fb2TINode.SelectSingleNode( "." + _ns + "src-lang", _NsManager )
				: null;
		}
		public XmlNode getDateNode( TitleInfoEnum TitleInfoType ) {
			XmlNode fb2TINode = getTitleInfoNode( TitleInfoType );
			return fb2TINode != null
				? fb2TINode.SelectSingleNode( "." + _ns + "date", _NsManager )
				: null;
		}
		public XmlNode getKeywordsNode( TitleInfoEnum TitleInfoType ) {
			XmlNode fb2TINode = getTitleInfoNode( TitleInfoType );
			return fb2TINode != null
				? fb2TINode.SelectSingleNode( "." + _ns + "keywords", _NsManager )
				: null;
		}
		public XmlNode getAnnotationNode( TitleInfoEnum TitleInfoType ) {
			XmlNode fb2TINode = getTitleInfoNode( TitleInfoType );
			return fb2TINode != null
				? fb2TINode.SelectSingleNode( "." + _ns + "annotation", _NsManager )
				: null;
		}
		public XmlNodeList getSequencesNode( TitleInfoEnum TitleInfoType ) {
			XmlNode fb2TINode = getTitleInfoNode( TitleInfoType );
			return fb2TINode != null
				? fb2TINode.SelectNodes( "." + _ns + "sequence", _NsManager )
				: null;
		}
		public XmlNode getCoverImageNodeForHref( TitleInfoEnum TitleInfoType, string Href ) {
			XmlNodeList xmlCoverImageNodeList = getCoverAllImageNodes( TitleInfoType );
			if( xmlCoverImageNodeList != null && xmlCoverImageNodeList.Count > 0 ) {
				foreach( XmlNode ImageNode in xmlCoverImageNodeList ) {
					if( ImageNode.Attributes["l:href"] != null ) {
						if( Href == ImageNode.Attributes["l:href"].Value )
							return ImageNode;
					} else if( ImageNode.Attributes["xlink:href"] != null ) {
						if( Href == ImageNode.Attributes["xlink:href"].Value )
							return ImageNode;
						
					}
				}
			}
			return null;
		}
		public XmlNode getCoverNode( TitleInfoEnum TitleInfoType ) {
			XmlNode xmlCover = null;
			XmlNode xmlTI = getTitleInfoNode( TitleInfoType );
			if( xmlTI != null ) {
				string ns = getNamespace();
				xmlCover = xmlTI.SelectSingleNode( "." + ns + "coverpage", getNamespaceManager() );
			}
			return xmlCover;
		}
		public XmlNodeList getCoverAllImageNodes( TitleInfoEnum TitleInfoType ) {
			XmlNode xmlCoverNode = getCoverNode(TitleInfoType );
			return xmlCoverNode != null
				? xmlCoverNode.SelectNodes( "." + _ns +"image", _NsManager )
				: null;
		}
		
		// document-info nodes
		public XmlNode getDocumentInfoNode() {
			XmlNode xn = null;
			XmlNode fb = getFictionBookNode();
			if( fb != null ) {
				XmlNode desc = getDescriptionNode();
				if( desc != null )
					xn = desc.SelectSingleNode( "." + _ns + "document-info", _NsManager );
			}
			return xn;
		}
		public XmlNodeList getFB2AuthorNodes() {
			XmlNode fbDI = getDocumentInfoNode();
			return fbDI != null
				? fbDI.SelectNodes( "." + _ns + "author", _NsManager )
				: null;
		}
		public XmlNode getFB2IDNode() {
			XmlNode fb2DINode = getDocumentInfoNode();
			return fb2DINode != null
				? fb2DINode.SelectSingleNode( "." + _ns + "id", _NsManager )
				: null;
		}
		public XmlNode getFB2VersionNode() {
			XmlNode fb2DINode = getDocumentInfoNode();
			return fb2DINode != null
				? fb2DINode.SelectSingleNode( "." + _ns + "version", _NsManager )
				: null;
		}
		public XmlNode getFB2ProgramUsedNode() {
			XmlNode fb2DINode = getDocumentInfoNode();
			return fb2DINode != null
				? fb2DINode.SelectSingleNode( "." + _ns + "program-used", _NsManager )
				: null;
		}
		public XmlNode getFB2CreateDate() {
			XmlNode fb2DINode = getDocumentInfoNode();
			return fb2DINode != null
				? fb2DINode.SelectSingleNode( "." + _ns + "date", _NsManager )
				: null;
		}
		public XmlNodeList getFB2SrcUrls() {
			XmlNode fb2DINode = getDocumentInfoNode();
			return fb2DINode != null
				? fb2DINode.SelectNodes( "." + _ns + "src-url", _NsManager )
				: null;
		}
		public XmlNode getFB2SrcOcr() {
			XmlNode fb2DINode = getDocumentInfoNode();
			return fb2DINode != null
				? fb2DINode.SelectSingleNode( "." + _ns + "src-ocr", _NsManager )
				: null;
		}
		public XmlNode getFB2History() {
			XmlNode fb2DINode = getDocumentInfoNode();
			return fb2DINode != null
				? fb2DINode.SelectSingleNode( "." + _ns + "history", _NsManager )
				: null;
		}
		
		// publish-info nodes
		public XmlNode getPublishInfoNode() {
			XmlNode xn = null;
			XmlNode fb = getFictionBookNode();
			if( fb != null ) {
				XmlNode desc = getDescriptionNode();
				if( desc != null )
					xn = desc.SelectSingleNode( "." + _ns + "publish-info", _NsManager );
			}
			return xn;
		}
		public XmlNode getPIBookName() {
			XmlNode fb2PINode = getPublishInfoNode();
			return fb2PINode != null
				? fb2PINode.SelectSingleNode( "." + _ns + "book-name", _NsManager )
				: null;
		}
		public XmlNode getPIPublisher() {
			XmlNode fb2PINode = getPublishInfoNode();
			return fb2PINode != null
				? fb2PINode.SelectSingleNode( "." + _ns + "publisher", _NsManager )
				: null;
		}
		public XmlNode getPICity() {
			XmlNode fb2PINode = getPublishInfoNode();
			return fb2PINode != null
				? fb2PINode.SelectSingleNode( "." + _ns + "city", _NsManager )
				: null;
		}
		public XmlNode getPIYear() {
			XmlNode fb2PINode = getPublishInfoNode();
			return fb2PINode != null
				? fb2PINode.SelectSingleNode( "." + _ns + "year", _NsManager )
				: null;
		}
		public XmlNode getPIISBN() {
			XmlNode fb2PINode = getPublishInfoNode();
			return fb2PINode != null
				? fb2PINode.SelectSingleNode( "." + _ns + "isbn", _NsManager )
				: null;
		}
		public XmlNodeList getPISequencesNode() {
			XmlNode fb2PINode = getPublishInfoNode();
			return fb2PINode != null
				? fb2PINode.SelectNodes( "." + _ns + "sequence", _NsManager )
				: null;
		}
		
		// custom-info nodes
		public XmlNodeList getCustomInfoNode() {
			XmlNodeList xn = null;
			XmlNode fb = getFictionBookNode();
			if( fb != null ) {
				XmlNode desc = getDescriptionNode();
				if( desc != null )
					xn = desc.SelectNodes( "." + _ns + "custom-info", _NsManager );
			}
			return xn;
		}
		
		// body nodes
		public XmlNodeList getBodyNodes() {
			XmlNode fb = getFictionBookNode();
			return fb != null
				? fb.SelectNodes( "." + _ns + "body", _NsManager )
				: null;
		}
		
		// binary nodes
		public XmlNodeList getBinaryNodes() {
			XmlNode fb = getFictionBookNode();
			return fb != null
				? fb.SelectNodes( "." + _ns + "binary", _NsManager )
				: null;
		}
		public XmlNode getBinaryNodeForID( string ID ) {
			XmlNode fb = getFictionBookNode();
			if( fb != null ) {
				XmlNodeList xmlBinaryNodes = fb.SelectNodes( "." + _ns + "binary", _NsManager );
				if( xmlBinaryNodes != null && xmlBinaryNodes.Count > 0 ) {
					foreach( XmlNode Binary in xmlBinaryNodes ) {
						if( Binary.Attributes["id"] != null ) {
							if( ID == Binary.Attributes["id"].Value.Trim() )
								return Binary;
						}
					}
				}
			}
			return null;
		}

		#endregion
		
		#region Закрытые вспомогательные основные методы класса
		// извлечение информации по title-info
		private TitleInfo TitleInfo( TitleInfoEnum TitleInfoType ) {
			return TitleInfoType == TitleInfoEnum.TitleInfo
				? new TitleInfo( TIGenres, TIAuthors, TIBookTitle, TIAnnotation, TIKeywords, TIDate,
				                TICoverpages, TILang, TISrcLang, TITranslators, TISequences)
				: new TitleInfo( STIGenres, STIAuthors, STIBookTitle, STIAnnotation, STIKeywords, STIDate,
				                STICoverpages, STILang, STISrcLang, STITranslators, STISequences);
		}
		
		private Date getDate( XmlNode xn )
		{
			// Дата создания fb2-документа
			Date date = null;
			if( xn != null ) {
				XmlNode xmlNode = xn.SelectSingleNode("." + _ns + "date", _NsManager);
				if( xmlNode != null ) {
					date = new Date( xmlNode.InnerText );
					if( xmlNode.Attributes["value"] != null ) {
						date.Value = xmlNode.Attributes["value"].Value;
					}
					if( xmlNode.Attributes["lang"] != null ) {
						date.Lang = xmlNode.Attributes["lang"].Value;
					}
				}
			}
			return date;
		}
		
		private Genre getGenre( XmlNode xn )
		{
			// извлечение информации по custom-info
			if( xn == null )
				return null;
			
			Genre genre = new Genre( xn.InnerText );
			if( xn.Attributes["match"] != null ) {
				try {
					genre.Math = Convert.ToUInt32( xn.Attributes["match"].Value.Trim() );
				} catch( Exception ) {
					genre.Math = 100;
				}
			}

			return genre;
		}

		// извлечение информации по author
		private Author getAuthor( XmlNode xn )
		{
			if( xn == null )
				return null;

			Author Author = null;
			XmlNode		fn = xn.SelectSingleNode("." + _ns + "first-name", _NsManager);
			XmlNode		mn = xn.SelectSingleNode("." + _ns + "middle-name", _NsManager);
			XmlNode		ln = xn.SelectSingleNode("." + _ns + "last-name", _NsManager);
			XmlNode		nn = xn.SelectSingleNode("." + _ns + "nickname", _NsManager);
			XmlNodeList	hp = xn.SelectNodes("." + _ns + "home-page", _NsManager);
			XmlNodeList	em = xn.SelectNodes("." + _ns + "email", _NsManager);
			XmlNode		id = xn.SelectSingleNode("." + _ns + "id", _NsManager);

			if( fn != null || mn != null || ln != null || nn != null || hp != null || em != null || id != null ) {
				Author = new Author();
				if( fn != null )
					Author.FirstName = TextFieldType<TextFieldType>( fn );

				if( mn != null )
					Author.MiddleName = TextFieldType<TextFieldType>( mn );

				if( ln != null )
					Author.LastName = TextFieldType<TextFieldType>( ln );

				if( nn != null )
					Author.NickName = TextFieldType<TextFieldType>( nn );

				if(hp != null) {
					if( hp.Count > 0 ) {
						IList<string> homePages = new List<string>();
						foreach( XmlNode node in hp )
							homePages.Add( node.InnerText.Trim() );
						Author.HomePages = homePages;
					}
				}
				
				if(em != null) {
					if( em.Count > 0 ) {
						IList<string> emails = new List<string>();
						foreach( XmlNode node in em )
							emails.Add( node.InnerText.Trim() );
						Author.Emails = emails;
					}
				}
				
				if( id != null )
					Author.ID = id.InnerText.Trim();
			}
			return Author;
		}
		
		// извлечение информации по sequence
		private Sequence getSequence( XmlNode node )
		{
			if( node == null )
				return null;
			
			Sequence sequence = null;
			if( node.Attributes["name"] != null )
				sequence = new Sequence( node.Attributes["name"].Value.Trim() );

			if( node.Attributes["number"] != null ) {
				if( sequence == null )
					sequence = new Sequence();
				try {
					sequence.Number = node.Attributes["number"].Value.Trim() ;
				} catch( FormatException ) {
				}
			}
			if( node.Attributes["lang"] != null ) {
				if( sequence == null )
					sequence = new Sequence();
				sequence.Lang = node.Attributes["lang"].Value.Trim();
			}
			return sequence;
		}
		
		// извлечение информации по вложенным sequence в sequence
		private IList<Sequence> getSequences( XmlNode xn, IList<Sequence> sequences ) {
			if( xn == null )
				return null;
			
			XmlNodeList xmlNodes = xn.SelectNodes("." + _ns + "sequence", _NsManager);
			if(xmlNodes != null) {
				if( xmlNodes.Count > 0 ) {
					foreach( XmlNode node in xmlNodes ) {
						sequences.Add( getSequence( node ) );
						getSequences( node, sequences );
					}
				}
			}
			return sequences;
		}
		
		private BookTitle getBookTitle( TitleInfoEnum TitleInfoMode ) {
			XmlNode xmlTINode = getTitleInfoNode( TitleInfoMode );
			if( xmlTINode != null ) {
				XmlNode xmlBTNode = xmlTINode.SelectSingleNode("." + _ns + "book-title", _NsManager);
				return ( xmlBTNode != null )
					? TextFieldType<BookTitle>( xmlBTNode )
					: null;
			}
			return null;
		}
		
		private string getLang( TitleInfoEnum TitleInfoMode ) {
			XmlNode xmlTINode = getTitleInfoNode( TitleInfoMode );
			if( xmlTINode != null ) {
				XmlNode xmlLangNode = xmlTINode.SelectSingleNode("." + _ns + "lang", _NsManager);
				return ( xmlLangNode != null )
					? xmlLangNode.InnerText.Trim()
					: null;
			}
			return null;
		}
		
		private string getSrcLang( TitleInfoEnum TitleInfoMode ) {
			XmlNode xmlTINode = getTitleInfoNode( TitleInfoMode );
			if( xmlTINode != null ) {
				XmlNode xmlSrcLangNode = xmlTINode.SelectSingleNode("." + _ns + "src-lang", _NsManager);
				return ( xmlSrcLangNode != null )
					? xmlSrcLangNode.InnerText.Trim()
					: null;
			}
			return null;
		}
		
		private Annotation getAnnotation( TitleInfoEnum TitleInfoMode ) {
			XmlNode xmlTINode = getTitleInfoNode( TitleInfoMode );
			if( xmlTINode != null ) {
				XmlNode xmlBANode = xmlTINode.SelectSingleNode("." + _ns + "annotation", _NsManager);
				return ( xmlBANode != null )
					? AnnotationType<Annotation>( xmlBANode )
					: null;
			}
			return null;
		}

		private Keywords getKeywords( TitleInfoEnum TitleInfoMode ) {
			XmlNode xmlTINode = getTitleInfoNode( TitleInfoMode );
			if( xmlTINode != null ) {
				XmlNode xmlKeyNode = xmlTINode.SelectSingleNode("." + _ns + "keywords", _NsManager);
				return ( xmlKeyNode != null )
					? TextFieldType<Keywords>( xmlKeyNode )
					: null;
			}
			return null;
		}
		
		private Date getDate( TitleInfoEnum TitleInfoMode ) {
			return getDate( getTitleInfoNode( TitleInfoMode ) );
		}
		
		private IList<Author> getAuthors( TitleInfoEnum TitleInfoMode, AuthorEnum AuthorMode ) {
			IList<Author> ilAuthors = null;
			XmlNode xmlParrentNode = AuthorMode == AuthorEnum.AuthorOfFB2
				? getDocumentInfoNode() : getTitleInfoNode( TitleInfoMode );
			if( xmlParrentNode != null ) {
				XmlNodeList xmlNodes = xmlParrentNode.SelectNodes(
					"." + _ns + (AuthorMode == AuthorEnum.Translator ? "translator" : "author"), _NsManager
				);
				if(xmlNodes != null) {
					if( xmlNodes.Count > 0  ) {
						ilAuthors = new List<Author>();
						foreach( XmlNode node in xmlNodes )
							ilAuthors.Add( getAuthor( node ) );
					}
				}
			}
			return ilAuthors;
		}
		
		private IList<Genre> getGenres( TitleInfoEnum TitleInfoMode ) {
			IList<Genre> ilGenres = null;
			XmlNode xmlTINode = getTitleInfoNode( TitleInfoMode );
			if( xmlTINode != null ) {
				XmlNodeList xmlNodes = xmlTINode.SelectNodes("." + _ns + "genre", _NsManager);
				if( xmlNodes != null ) {
					if( xmlNodes.Count > 0  ) {
						ilGenres = new List<Genre>();
						foreach( XmlNode node in xmlNodes )
							ilGenres.Add( getGenre( node ) );
					}
				}
			}
			return ilGenres;
		}
		
		// Обложки - список значений l:href обложек
		private IList<Coverpage> getCoverpages( TitleInfoEnum TitleInfoMode ) {
			IList<Coverpage> ilCoverpages = null;
			XmlNode xmlTINode = getTitleInfoNode( TitleInfoMode );
			if( xmlTINode != null ) {
				XmlNode xmlCoverNode = xmlTINode.SelectSingleNode("." + _ns + "coverpage", _NsManager);
				if( xmlCoverNode != null ) {
					XmlNodeList xmlImageNodes = xmlCoverNode.SelectNodes("." + _ns + "image", _NsManager);
					if( xmlImageNodes != null && xmlImageNodes.Count > 0 ) {
						ilCoverpages = new List<Coverpage>();
						foreach( XmlNode ImageNode in xmlImageNodes ) {
							if( ImageNode != null ) {
								if( ImageNode.Attributes["l:href"] != null ) {
									string Value = ImageNode.Attributes["l:href"].Value.Trim();
									ilCoverpages.Add(
										new Coverpage( Value.Substring( 0, 1 ) == "#" ? Value.Substring(1) : Value )
									);
								} else if( ImageNode.Attributes["xlink:href"] != null ) {
									string Value = ImageNode.Attributes["xlink:href"].Value;
									ilCoverpages.Add(
										new Coverpage( Value.Substring( 0, 1 ) == "#" ? Value.Substring(1) : Value )
									);
								}
							}
						}
					}
				}
			}
			return ilCoverpages;
		}
		
		private IList<Sequence> getSequences( TitleInfoEnum TitleInfoMode ) {
			IList<Sequence> ilSequences = null;
			XmlNode xmlTINode = getTitleInfoNode( TitleInfoMode );
			if( xmlTINode != null ) {
				XmlNodeList xmlNodes = xmlTINode.SelectNodes("." + _ns + "sequence", _NsManager);
				if(xmlNodes != null) {
					if(xmlNodes.Count > 0) {
						ilSequences = new List<Sequence>();
						foreach( XmlNode node in xmlNodes ) {
							ilSequences.Add( getSequence(node) );
							getSequences( node, ilSequences );
						}
					}
				}
			}
			return ilSequences;
		}
		
		private T TextFieldType<T>( XmlNode xmlNode ) where T : ITextFieldType, new()
		{
			if( xmlNode == null )
				return default(T);

			T textField = new T();
			textField.Value = xmlNode.InnerText;
			if( xmlNode.Attributes["lang"] != null )
				textField.Lang = xmlNode.Attributes["lang"].Value.Trim();

			return textField;
		}
		
		private T AnnotationType<T>( XmlNode xmlNode ) where T : IAnnotationType, new()
		{
			T annotation = default(T);
			if( xmlNode != null ) {
				annotation = new T();
				annotation.Value = xmlNode.InnerXml;
				if( xmlNode.Attributes["id"] != null )
					annotation.Id = xmlNode.Attributes["id"].Value.Trim();
				if( xmlNode.Attributes["lang"] != null )
					annotation.Lang = xmlNode.Attributes["lang"].Value.Trim();
			}
			return annotation;
		}
		#endregion
	}
}
