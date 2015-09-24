/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 02.09.2015
 * Время: 7:23
 * 
 * License: GPL 2.1
 */
using System;
using System.Xml;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Globalization;

using Core.Common;
using Core.FB2.FB2Parsers;

using TitleInfoEnum	= Core.Common.Enums.TitleInfoEnum;
using AuthorEnum	= Core.Common.Enums.AuthorEnum;

namespace Core.Common
{
	/// <summary>
	/// Корректура структуры fb2 книги
	/// </summary>
	public class FB2Corrector
	{
		#region Закрытые данные класса
		private readonly FictionBook _fb2 = null;
		private static string[] _Tags = {
			"<p>", "<p ", "</p>", "<p/>", "<p />", "<empty-line/>", "<empty-line />",
			"<strong>", "</strong>", "<emphasis>", "</emphasis>", "<a ", "</a>",
			"<section>", "</section>", "<section ",
			"<title>", "</title>", "<title ", "<subtitle>", "<subtitle ", "</subtitle>",
			"<image>", "</image>", "<image ",
			"<poem>", "<poem ", "</poem>", "<stanza>", "<stanza ", "</stanza>", "<v>", "<v ", "</v>",
			"<cite>", "<cite ", "</cite>", "<epigraph>", "</epigraph>", "<epigraph ",
			"<strikethrough>", "</strikethrough>", "<sub>", "</sub>", "<sup>", "</sup>", "<code>", "</code>",
			"<table>", "<table ", "</table>", "<th>", "<th ", "</th>", "<td>", "<td ", "</td>",
			
			"<title-info>", "</title-info>", "<title-info ", "<genre>", "</genre>", "<genre ",
			"<author>", "</author>", "<author ", "<book-title>", "</book-title>", "<book-title ",
			"<annotation>", "</annotation>", "<annotation ", "<keywords>", "</keywords>", "<keywords ",
			"<date>", "</date>", "<date ", "<date/>", "<date />",
			"<coverpage>", "</coverpage>", "<coverpage ",
			"<lang>", "</lang>", "<lang ", "<src-lang>", "</src-lang>", "<src-lang ", "<translator>", "</translator>", "<translator ",
			"<sequence>", "</sequence>", "<sequence ", "<src-title-info>", "</src-title-info>", "<src-title-info ",
			"<document-info>", "</document-info>", "<document-info ", "<program-used>", "</program-used>", "<program-used ",
			"<src-url>", "</src-url>", "<src-url ", "<src-ocr>", "</src-ocr>", "<src-ocr ",
			"<id>", "</id>", "<id ", "<version>", "</version>", "<version ", "<binary ", "</binary>",
			"<publish-info>", "</publish-info>", "<publish-info ", "<book-name>", "</book-name>", "<book-name ",
			"<publisher>", "</publisher>", "<publisher ", "<description>", "</description>", "<description ",
			"<city>", "</city>", "<city ", "<year>", "</year>", "<year ", "<isbn>", "<isbn ", "</isbn>",
			"<history>", "</history>", "<history ", "<custom-info ", "</custom-info>",
			"<body>", "<body ", "</body>", "<text-author>", "<text-author ", "</text-author>",
			"<first-name>", "<first-name ", "</first-name>", "<first-name/>", "<first-name />",
			"<middle-name>", "<middle-name ", "<middle-name/>", "<middle-name />", "</middle-name>",
			"<last-name>", "<last-name ", "</last-name>", "<last-name/>", "<last-name />",
			"<nickname>", "<nickname ", "</nickname>", "<last-name/>", "<last-name />",
			"<home-page>", "<home-page ", "</home-page>", "<home-page/>", "<home-page />",
			"<email>", "<email ", "</email>", "<email/>", "<email />",
			"<FictionBook ", "</FictionBook>", "<stylesheet ", "</stylesheet>", "<style>", "</style>", "<style ",
			"<output>", "</output>", "<part>", "</part>", "<part ",
			"<output-document-class>", "</output-document-class>", "<output-document-class ", "<?xml "
		};
		#endregion
		
		public FB2Corrector( ref FictionBook fb2 )
		{
			_fb2 = fb2;
		}
		
		#region Закрытые вспомогательные основные методы класса
		// создание новой структуры по 1 заданному данному
		private XmlElement createStructure( string ElementName, string ElementValue ) {
			XmlElement xmlElement = null;
			if( !string.IsNullOrEmpty(ElementValue) ) {
				xmlElement = _fb2.getXmlDoc().CreateElement( _fb2.getPrefix(), ElementName, _fb2.getNamespaceURI() );
				xmlElement.InnerText = ElementValue;
			}
			return xmlElement;
		}
		// создание новой структуры по 1 заданному массиву строк
		private XmlElement createStructure( string ElementName, ref string [] ElementStringArray ) {
			XmlElement xmlElement = null;
			if( ElementStringArray != null && ElementStringArray.Length > 0) {
				xmlElement = _fb2.getXmlDoc().CreateElement( _fb2.getPrefix(), ElementName, _fb2.getNamespaceURI() );
				for( int i = 0; i != ElementStringArray.Length; ++i ) {
					if( !string.IsNullOrEmpty( ElementStringArray[i] ) )
						xmlElement.AppendChild( createPStructure( ElementStringArray[i].Trim() ) );
					else
						xmlElement.AppendChild( createEmptyLineStructure() );
				}
			}
			return xmlElement;
		}
		// создание новой структуры по 2-м заданному значению тега и его аттрибуту
		private XmlElement createStructure( string ElementName, string AttributeName, string ElementValue, string AttributeValue) {
			XmlElement xmlCustomInfo = null;
			if( !string.IsNullOrEmpty(ElementValue) || !string.IsNullOrEmpty(AttributeValue) ) {
				xmlCustomInfo = _fb2.getXmlDoc().CreateElement( _fb2.getPrefix(), ElementName, _fb2.getNamespaceURI() );
				xmlCustomInfo.SetAttribute( AttributeName, !string.IsNullOrEmpty(AttributeValue.Trim()) ? AttributeValue.Trim() : "" );
				xmlCustomInfo.InnerText = ElementValue.Trim();
			}
			return xmlCustomInfo;
		}
		#endregion
		
		#region Открытые методы СОЗДАНИЯ элементов структуры раздела description
		// создание нового раздела description с минимальными необходимыми данными
		public XmlElement makeDescriptionNodeWithMinimalElements() {
			XmlElement xmlDesc = _fb2.getXmlDoc().CreateElement( _fb2.getPrefix(), "description", _fb2.getNamespaceURI() );
			xmlDesc.AppendChild( makeTitleInfoNodeWithMinimalElements( TitleInfoEnum.TitleInfo ) );
			xmlDesc.AppendChild( makeDocumentInfoNodeWithMinimalElements() );
			return xmlDesc;
		}
		// создание нового раздела title-info с минимальными необходимыми данными
		public XmlElement makeTitleInfoNodeWithMinimalElements( TitleInfoEnum TitleInfoType ) {
			XmlElement xmlTI = _fb2.getXmlDoc().CreateElement(
				_fb2.getPrefix(),
				TitleInfoType == TitleInfoEnum.TitleInfo ? "title-info" : "source-title-info",
				_fb2.getNamespaceURI()
			);
			xmlTI.AppendChild( makeGenreNode( "other", null ) );
			xmlTI.AppendChild(
				makeAuthorNode(
					Enums.AuthorEnum.AuthorOfBook, string.Empty, string.Empty, "Неизвестный", string.Empty,
					null, null, string.Empty
				)
			);
			xmlTI.AppendChild( makeBookTitleNode( "Неизвестная книга" ) );
			xmlTI.AppendChild( makeLangNode() );
			return xmlTI;
		}
		// создание нового раздела document-info с минимальными необходимыми данными
		public XmlElement makeDocumentInfoNodeWithMinimalElements() {
			XmlElement xmlDI = _fb2.getXmlDoc().CreateElement( _fb2.getPrefix(), "document-info", _fb2.getNamespaceURI() );
			xmlDI.AppendChild(
				makeAuthorNode(
					Enums.AuthorEnum.AuthorOfFB2, string.Empty, string.Empty, "fb2 создатель", string.Empty,
					null, null, string.Empty
				)
			);
			xmlDI.AppendChild( makeProgramUsedNode() );
			xmlDI.AppendChild( makeDateNode() );
			xmlDI.AppendChild( makeIDNode() );
			xmlDI.AppendChild( makeVersionNode() );
			return xmlDI;
		}
		
		// реконструкция раздела description
		public bool recoveryDescriptionNode() {
			XmlNode xmlDesc = _fb2.getDescriptionNode();
			if ( xmlDesc == null ) {
				XmlNode xmlFictionBook = _fb2.getFictionBookNode();
				if ( xmlFictionBook != null ) {
					// создание нового раздела description
					XmlElement xmlDescNew = _fb2.getXmlDoc().CreateElement( _fb2.getPrefix(), "description", _fb2.getNamespaceURI() );
					xmlDescNew.AppendChild(
						makeTitleInfoNodeWithMinimalElements( TitleInfoEnum.TitleInfo )
					);
					xmlDescNew.AppendChild(
						makeDocumentInfoNodeWithMinimalElements()
					);
					xmlFictionBook.InsertBefore( xmlDescNew, _fb2.getBodyNodes()[0] );
					return true;
				}
				else
					return false;
			} else {
				XmlNode xmlSTIOld = _fb2.getTitleInfoNode( TitleInfoEnum.SourceTitleInfo );
				if ( xmlSTIOld != null ) {
					if ( !string.IsNullOrWhiteSpace( xmlSTIOld.InnerText ) )
						xmlDesc.ReplaceChild(
							recoveryTitleInfoNode( TitleInfoEnum.SourceTitleInfo ), xmlSTIOld
						);
					else
						xmlDesc.RemoveChild( xmlSTIOld );
				}
				
				XmlNode xmlTIOld = _fb2.getTitleInfoNode( TitleInfoEnum.TitleInfo );
				xmlSTIOld = _fb2.getTitleInfoNode( TitleInfoEnum.SourceTitleInfo );
				XmlNode xmlDIOld = _fb2.getDocumentInfoNode();
				
				if ( xmlSTIOld != null ) {
					// упорядочивание title-info относительно src-title-info
					if ( xmlTIOld == null ) {
						xmlDesc.InsertBefore(
							makeTitleInfoNodeWithMinimalElements( TitleInfoEnum.TitleInfo ),
							_fb2.getTitleInfoNode( TitleInfoEnum.SourceTitleInfo )
						);
					} else if ( xmlTIOld != null ) {
						xmlDesc.ReplaceChild(
							recoveryTitleInfoNode( TitleInfoEnum.TitleInfo ),
							xmlTIOld
						);
						xmlDesc.InsertBefore(
							_fb2.getTitleInfoNode( TitleInfoEnum.TitleInfo ),
							_fb2.getTitleInfoNode( TitleInfoEnum.SourceTitleInfo )
						);
					}
					
					// упорядочивание document-info относительно src-title-info
					if ( xmlDIOld == null )
						xmlDesc.InsertAfter(
							makeDocumentInfoNodeWithMinimalElements(),
							_fb2.getTitleInfoNode( TitleInfoEnum.SourceTitleInfo )
						);
					else if ( xmlDIOld != null ) {
						xmlDesc.ReplaceChild(
							recoveryDocumentInfoNode(),
							xmlDIOld
						);
						xmlDesc.InsertAfter(
							_fb2.getDocumentInfoNode(),
							_fb2.getTitleInfoNode( TitleInfoEnum.SourceTitleInfo )
						);
					}
				} else {
					// нет src-title-info
					// упорядочивание document-info относительно title-info
					if ( xmlTIOld == null && xmlDIOld == null ) {
						xmlDesc.AppendChild(
							makeTitleInfoNodeWithMinimalElements( TitleInfoEnum.TitleInfo )
						);
						xmlDesc.AppendChild(
							makeDocumentInfoNodeWithMinimalElements()
						);
					} else if ( xmlTIOld != null && xmlDIOld != null ) {
						xmlDesc.ReplaceChild(
							recoveryTitleInfoNode( TitleInfoEnum.TitleInfo ), xmlTIOld
						);
						xmlDesc.ReplaceChild(
							recoveryDocumentInfoNode(), xmlDIOld
						);
					} else if ( xmlTIOld == null && xmlDIOld != null ) {
						xmlDesc.ReplaceChild(
							recoveryDocumentInfoNode(), xmlDIOld
						);
						xmlDesc.InsertBefore(
							makeTitleInfoNodeWithMinimalElements( TitleInfoEnum.TitleInfo ),
							_fb2.getDocumentInfoNode()
						);
					} else if ( xmlTIOld != null && xmlDIOld == null ) {
						xmlDesc.ReplaceChild(
							recoveryTitleInfoNode( TitleInfoEnum.TitleInfo ), xmlTIOld
						);
						xmlDesc.InsertAfter(
							makeDocumentInfoNodeWithMinimalElements(),
							_fb2.getTitleInfoNode( TitleInfoEnum.TitleInfo )
						);
					}
				}
				
				// восстановление publish-info
				XmlNode xmlPIOld = _fb2.getPublishInfoNode();
				if ( xmlPIOld != null ) {
					if ( !string.IsNullOrWhiteSpace( xmlPIOld.InnerText ) ) {
						XmlNode xmlPINew = recoveryPublisInfoNode();
						xmlDesc.RemoveChild( xmlPIOld );
						xmlDesc.InsertAfter( xmlPINew, _fb2.getDocumentInfoNode() );
					} else
						xmlDesc.RemoveChild( xmlPIOld );
				}
				
				// восстановление custom-info
				XmlNodeList xmlCIListOld = _fb2.getCustomInfoNode();
				if( xmlCIListOld != null && xmlCIListOld.Count > 0 ) {
					foreach ( XmlNode xmlCIOld in xmlCIListOld ) {
						if ( xmlCIOld != null ) {
							XmlAttribute xmlInfoType = xmlCIOld.Attributes["info-type"];
							if ( xmlInfoType != null ) {
								if ( string.IsNullOrWhiteSpace( xmlInfoType.Value ) ) {
									if ( !string.IsNullOrWhiteSpace( xmlCIOld.InnerText ) ) {
										XmlNode xmlCINew = makeCustomInfoNode(
											xmlCIOld.Attributes["info-type"].Value, xmlCIOld.InnerText.Trim()
										);
										xmlDesc.RemoveChild( xmlCIOld );
										xmlDesc.AppendChild( xmlCINew );
									} else
										xmlDesc.RemoveChild( xmlCIOld );
								} else {
									XmlNode xmlCINew = makeCustomInfoNode(
										xmlCIOld.Attributes["info-type"].Value, xmlCIOld.InnerText.Trim()
									);
									xmlDesc.RemoveChild( xmlCIOld );
									xmlDesc.AppendChild( xmlCINew );
								}
							} else {
								if ( !string.IsNullOrWhiteSpace( xmlCIOld.InnerText ) ) {
									XmlNode xmlCINew = makeCustomInfoNode( "?", xmlCIOld.InnerText.Trim() );
									xmlDesc.RemoveChild( xmlCIOld );
									xmlDesc.AppendChild( xmlCINew );
								} else
									xmlDesc.RemoveChild( xmlCIOld );
							}
						}
					}
				}
				
				return true;
			}
		}
		// реконструкция раздела title-info
		public XmlNode recoveryTitleInfoNode( TitleInfoEnum TitleInfoType ) {
			XmlNode xmlTI = _fb2.getTitleInfoNode( TitleInfoType );
			if ( xmlTI == null ) {
				// создание нового раздела title-info с минимальными необходимыми данными
				return makeTitleInfoNodeWithMinimalElements( TitleInfoType );
			} else {
				XmlElement xmlTINew = _fb2.getXmlDoc().CreateElement(
					_fb2.getPrefix(),
					TitleInfoType == TitleInfoEnum.TitleInfo ? "title-info" : "src-title-info",
					_fb2.getNamespaceURI()
				);
				// Жанры
				XmlNodeList xmlGenres = _fb2.getGenreNodes( TitleInfoType );
				if ( xmlGenres != null) {
					if ( xmlGenres.Count > 0 ) {
						foreach( XmlNode Genre in xmlGenres ) {
							if ( string.IsNullOrWhiteSpace( Genre.InnerText ) )
								Genre.InnerText = "other";
							if ( Genre.Attributes["match"] != null ) {
								if ( string.IsNullOrWhiteSpace( Genre.Attributes["match"].Value ) )
									Genre.Attributes["match"].Value = "100";
							}
							xmlTINew.AppendChild( Genre );
						}
					} else
						xmlTINew.AppendChild( makeGenreNode( "other", null ) );
				} else
					xmlTINew.AppendChild( makeGenreNode( "other", null ) );
				// Авторы
				XmlNodeList xmlAuthors = _fb2.getAuthorNodes( TitleInfoType );
				if ( xmlAuthors != null ) {
					if ( xmlAuthors.Count == 0 ) {
						xmlTINew.AppendChild(
							makeAuthorNode(
								Enums.AuthorEnum.AuthorOfBook, string.Empty, string.Empty, "Неизвестный", string.Empty,
								null, null, string.Empty
							)
						);
					} else if ( xmlAuthors.Count > 0 ) {
						if ( xmlAuthors.Count == 1 ) {
							foreach( XmlNode Author in xmlAuthors ) {
								// реконструкция Автора
								xmlTINew.AppendChild(
									recoveryAuthorNode( Author, AuthorEnum.AuthorOfBook )
								);
							}
						} else {
							// несколько авторов
							foreach( XmlNode Author in xmlAuthors ) {
								// реконструкция Автора
								XmlNode xmlFirstName = Author.SelectSingleNode( "." + _fb2.getNamespace() + "first-name", _fb2.getNamespaceManager() );
								XmlNode xmlMiddleName = Author.SelectSingleNode( "." + _fb2.getNamespace() + "middle-name", _fb2.getNamespaceManager() );
								XmlNode xmlLastName = Author.SelectSingleNode( "." + _fb2.getNamespace() + "last-name", _fb2.getNamespaceManager() );
								XmlNode xmlNickName = Author.SelectSingleNode( "." + _fb2.getNamespace() + "nickname", _fb2.getNamespaceManager() );
								XmlNodeList xmlHomePageList = Author.SelectNodes( "." + _fb2.getNamespace() + "home-page", _fb2.getNamespaceManager() );
								XmlNodeList xmlEmailList = Author.SelectNodes( "." + _fb2.getNamespace() + "email", _fb2.getNamespaceManager() );
								XmlNode xmlID = Author.SelectSingleNode( "." + _fb2.getNamespace() + "id", _fb2.getNamespaceManager() );
								if ( ( xmlFirstName != null && !string.IsNullOrWhiteSpace(xmlFirstName.InnerText) ) ||
								    ( xmlMiddleName != null && !string.IsNullOrWhiteSpace(xmlMiddleName.InnerText) ) ||
								    ( xmlLastName != null && !string.IsNullOrWhiteSpace(xmlLastName.InnerText) ) ||
								    ( xmlNickName != null && !string.IsNullOrWhiteSpace(xmlNickName.InnerText) ) ||
								    ( xmlID != null && !string.IsNullOrWhiteSpace(xmlID.InnerText) ) ||
								    ( xmlHomePageList != null && xmlHomePageList.Count > 0 ) ||
								    ( xmlEmailList != null && xmlEmailList.Count > 0 ) ) {
									xmlTINew.AppendChild(
										recoveryAuthorNode( Author, AuthorEnum.AuthorOfBook )
									);
								}
							}
						}
					}
				} else {
					xmlTINew.AppendChild(
						makeAuthorNode(
							Enums.AuthorEnum.AuthorOfBook, string.Empty, string.Empty, "Неизвестный", string.Empty,
							null, null, string.Empty
						)
					);
				}
				// Book Title
				XmlNode xmlBookTitle = _fb2.getBookTitleNode( TitleInfoType );
				if ( xmlBookTitle != null ) {
					if ( string.IsNullOrWhiteSpace( xmlBookTitle.InnerText ) )
						xmlBookTitle.InnerText = "Неизвестная книга";
					xmlTINew.AppendChild( xmlBookTitle );
				} else {
					xmlTINew.AppendChild(
						makeBookTitleNode( "Неизвестная книга" )
					);
				}
				// Аннотация
				XmlNode xmlAnnot = _fb2.getAnnotationNode( TitleInfoType );
				if ( xmlAnnot != null )
					xmlTINew.AppendChild( xmlAnnot );
				// keywords
				XmlNode xmlKeywords = _fb2.getKeywordsNode( TitleInfoType );
				if ( xmlKeywords != null ) {
					xmlTINew.AppendChild( xmlKeywords );
				}
				// date
				XmlNode xmlDate = _fb2.getDateNode( TitleInfoType );
				if ( xmlDate != null ) {
					if ( xmlDate.Attributes["value"] != null ) {
						if ( string.IsNullOrWhiteSpace( xmlDate.Attributes["value"].Value ) )
							xmlDate.Attributes.RemoveAll();
					}
					xmlTINew.AppendChild( xmlDate );
				} else
					xmlTINew.AppendChild( makeDateNode( null ) );
				// coverpage
				XmlNode xmlCoverpages = _fb2.getCoverNode( TitleInfoType );
				if ( xmlCoverpages != null )
					xmlTINew.AppendChild( xmlCoverpages );
				// lang
				XmlNode xmlLang = _fb2.getLangNode( TitleInfoType );
				if ( xmlLang != null ) {
					if ( string.IsNullOrWhiteSpace( xmlLang.InnerText ) )
						xmlLang.InnerText = "ru";
					xmlTINew.AppendChild( xmlLang );
				} else
					xmlTINew.AppendChild( makeLangNode() );
				// src-lang
				XmlNode xmlSrcLang = _fb2.getSrcLangNode( TitleInfoType );
				if ( xmlSrcLang != null ) {
					xmlSrcLang.InnerText = xmlSrcLang.InnerText.Trim();
					xmlTINew.AppendChild( xmlSrcLang );
				}
				// translators
				XmlNodeList xmlTranslators = _fb2.getTranslatorNodes( TitleInfoType );
				if ( xmlTranslators != null ) {
					foreach( XmlNode Translator in xmlTranslators ) {
						// реконструкция Переводчика
						xmlTINew.AppendChild(
							recoveryAuthorNode( Translator, AuthorEnum.Translator )
						);
					}
				}
				// sequence
				XmlNodeList xmlSequences = _fb2.getSequencesNode( TitleInfoType );
				if ( xmlSequences != null ) {
					foreach( XmlNode Sequence in xmlSequences ) {
						if ( Sequence.Attributes["name"] != null ) {
							// аттрибут name есть
							if ( Sequence.Attributes["number"] != null ) {
								// аттрибут number есть
								if ( !string.IsNullOrWhiteSpace( Sequence.Attributes["name"].Value ) &&
								    !string.IsNullOrWhiteSpace( Sequence.Attributes["number"].Value ) )
									xmlTINew.AppendChild( Sequence );
								else if ( !string.IsNullOrWhiteSpace( Sequence.Attributes["name"].Value ) &&
								         string.IsNullOrWhiteSpace( Sequence.Attributes["number"].Value ) )
									xmlTINew.AppendChild(
										makeSequenceNode( Sequence.Attributes["name"].Value.Trim(), null )
									);
							} else {
								// аттрибута number нет
								if ( !string.IsNullOrWhiteSpace( Sequence.Attributes["name"].Value ) )
									xmlTINew.AppendChild( Sequence );
							}
						}
					}
				}
				return xmlTINew;
			}
		}
		// реконструкция раздела document-info
		public XmlNode recoveryDocumentInfoNode() {
			XmlNode xmlDI = _fb2.getDocumentInfoNode();
			if ( xmlDI == null ) {
				// создание нового раздела document-info с минимальными необходимыми данными
				return makeDocumentInfoNodeWithMinimalElements();
			} else {
				XmlNode xmlDINew = _fb2.getXmlDoc().CreateElement( _fb2.getPrefix(), "document-info", _fb2.getNamespaceURI() );
				// Авторы fb2 документа
				XmlNodeList xmlAuthors = _fb2.getFB2AuthorNodes();
				if ( xmlAuthors != null && xmlAuthors.Count > 0 ) {
					foreach( XmlNode Author in xmlAuthors ) {
						// реконструкция Автора
						xmlDINew.AppendChild(
							recoveryAuthorNode( Author, AuthorEnum.AuthorOfFB2 )
						);
					}
				} else {
					xmlDINew.AppendChild(
						makeAuthorNode(
							Enums.AuthorEnum.AuthorOfFB2, string.Empty, string.Empty, "Создатель fb2-файла", string.Empty,
							null, null, string.Empty
						)
					);
				}
				// Program Used
				XmlNode xmlDIProgUsed = _fb2.getFB2ProgramUsedNode();
				if ( xmlDIProgUsed != null ) {
					if ( xmlDIProgUsed.InnerText.IndexOf( "SharpFBTools" ) == -1 )
						xmlDIProgUsed.InnerText += ", SharpFBTools";
					xmlDINew.AppendChild( xmlDIProgUsed );
				} else
					xmlDINew.AppendChild( makeProgramUsedNode() );
				// date
				XmlNode xmlDIDate = _fb2.getFB2CreateDate();
				if ( xmlDIDate != null ) {
					if ( xmlDIDate.Attributes["value"] != null ) {
						if ( string.IsNullOrWhiteSpace( xmlDIDate.Attributes["value"].Value ) )
							xmlDIDate.Attributes.RemoveAll();
					}
					xmlDINew.AppendChild( xmlDIDate );
				} else
					xmlDINew.AppendChild( makeDateNode() );
				// src-url
				XmlNodeList xmlFB2SrcUrls = _fb2.getFB2SrcUrls();
				if ( xmlFB2SrcUrls != null ) {
					foreach( XmlNode URL in xmlFB2SrcUrls ) {
						if ( !string.IsNullOrWhiteSpace( URL.InnerText ) )
							xmlDINew.AppendChild( URL );
					}
				}
				// src-ocr
				XmlNode xmlSrcOcr = _fb2.getFB2SrcOcr();
				if ( xmlSrcOcr != null ) {
					if ( !string.IsNullOrWhiteSpace( xmlSrcOcr.InnerText ) )
						xmlDINew.AppendChild( xmlSrcOcr );
				}
				// id
				XmlNode xmlFB2ID = _fb2.getFB2IDNode();
				if ( xmlFB2ID != null ) {
					if ( string.IsNullOrWhiteSpace( xmlFB2ID.InnerText ) )
						xmlFB2ID.InnerText = Guid.NewGuid().ToString().ToUpper();
					xmlDINew.AppendChild( xmlFB2ID );
				} else
					xmlDINew.AppendChild( makeIDNode() );
				// version
				XmlNode xmlFB2Version = _fb2.getFB2VersionNode();
				if ( xmlFB2Version != null ) {
					if ( string.IsNullOrWhiteSpace( xmlFB2Version.InnerText ) )
						xmlFB2Version.InnerText = "1.0";
					xmlDINew.AppendChild( xmlFB2Version );
				} else
					xmlDINew.AppendChild( makeVersionNode() );
				// history
				XmlNode xmlFB2History = _fb2.getFB2History();
				if ( xmlFB2History != null ) {
					if ( !string.IsNullOrWhiteSpace( xmlFB2History.InnerText ) ) {
						if ( xmlFB2History.InnerText.IndexOf( "SharpFBTools" ) == -1 )
							xmlFB2History.InnerXml += "<p>Восстановление структуры fb2 файла с помощью SharpFBTools</p>";
					}
					xmlDINew.AppendChild( xmlFB2History );
				} else {
					string [] HistoryArray = {"1.0 - Восстановление структуры fb2 файла с помощью SharpFBTools"};
					xmlDINew.AppendChild( makeHistoryNode( HistoryArray ) );
				}
				
				return xmlDINew;
			}
		}
		// реконструкция раздела publish-info
		public XmlNode recoveryPublisInfoNode() {
			XmlNode xmlPIOld = _fb2.getPublishInfoNode();
			if ( xmlPIOld != null ) {
				XmlNode xmlPINew = _fb2.getXmlDoc().CreateElement( _fb2.getPrefix(), "publish-info", _fb2.getNamespaceURI() );
				// book-name
				XmlNode xmlBookName = _fb2.getPIBookName();
				if ( xmlBookName != null ) {
					if ( !string.IsNullOrWhiteSpace( xmlBookName.InnerText ) )
						xmlPINew.AppendChild( xmlBookName );
				}
				// publisher
				XmlNode xmlPublisher = _fb2.getPIPublisher();
				if ( xmlPublisher != null ) {
					if ( !string.IsNullOrWhiteSpace( xmlPublisher.InnerText ) )
						xmlPINew.AppendChild( xmlPublisher );
				}
				// city
				XmlNode xmlCity = _fb2.getPICity();
				if ( xmlCity != null ) {
					if ( !string.IsNullOrWhiteSpace( xmlCity.InnerText ) )
						xmlPINew.AppendChild( xmlCity );
				}
				// year
				XmlNode xmlYear = _fb2.getPIYear();
				if ( xmlYear != null ) {
					if ( !string.IsNullOrWhiteSpace( xmlYear.InnerText ) )
						xmlPINew.AppendChild( xmlYear );
				}
				// isbn
				XmlNode xmlISBN = _fb2.getPIISBN();
				if ( xmlISBN != null ) {
					if ( !string.IsNullOrWhiteSpace( xmlISBN.InnerText ) )
						xmlPINew.AppendChild( xmlISBN );
				}
				// sequence
				XmlNodeList xmlSequences = _fb2.getPISequencesNode();
				if ( xmlSequences != null ) {
					foreach( XmlNode Sequence in xmlSequences ) {
						if ( Sequence.Attributes["name"] != null ) {
							// аттрибут name есть
							if ( Sequence.Attributes["number"] != null ) {
								// аттрибут number есть
								if ( !string.IsNullOrWhiteSpace( Sequence.Attributes["name"].Value ) &&
								    !string.IsNullOrWhiteSpace( Sequence.Attributes["number"].Value ) )
									xmlPINew.AppendChild( Sequence );
								else if ( !string.IsNullOrWhiteSpace( Sequence.Attributes["name"].Value ) &&
								         string.IsNullOrWhiteSpace( Sequence.Attributes["number"].Value ) )
									xmlPINew.AppendChild( makeSequenceNode( Sequence.Attributes["name"].Value.Trim(), null ) );
							} else {
								// аттрибута number нет
								if ( !string.IsNullOrWhiteSpace( Sequence.Attributes["name"].Value ) )
									xmlPINew.AppendChild( Sequence );
							}
						}
					}
				}
				return xmlPINew;
			}
			return xmlPIOld;
		}
		// реконструкция Автора/Переводчика
		public XmlNode recoveryAuthorNode( XmlNode xmlAuthor, AuthorEnum AuthorType ) {
			string author = AuthorType == Enums.AuthorEnum.Translator ? "translator" : "author";
			if ( xmlAuthor.Name == author ) {
				XmlNode xmlFirstName = xmlAuthor.SelectSingleNode( "." + _fb2.getNamespace() + "first-name", _fb2.getNamespaceManager() );
				XmlNode xmlMiddleName = xmlAuthor.SelectSingleNode( "." + _fb2.getNamespace() + "middle-name", _fb2.getNamespaceManager() );
				XmlNode xmlLastName = xmlAuthor.SelectSingleNode( "." + _fb2.getNamespace() + "last-name", _fb2.getNamespaceManager() );
				XmlNode xmlNickName = xmlAuthor.SelectSingleNode( "." + _fb2.getNamespace() + "nickname", _fb2.getNamespaceManager() );
				XmlNodeList xmlHomePageList = xmlAuthor.SelectNodes( "." + _fb2.getNamespace() + "home-page", _fb2.getNamespaceManager() );
				XmlNodeList xmlEmailList = xmlAuthor.SelectNodes( "." + _fb2.getNamespace() + "email", _fb2.getNamespaceManager() );
				XmlNode xmlID = xmlAuthor.SelectSingleNode( "." + _fb2.getNamespace() + "id", _fb2.getNamespaceManager() );
				
				XmlElement xmlAuthorNew = _fb2.getXmlDoc().CreateElement(
					_fb2.getPrefix(), author, _fb2.getNamespaceURI()
				);
				if ( xmlFirstName != null && !string.IsNullOrWhiteSpace( xmlFirstName.InnerText ) )
					xmlAuthorNew.AppendChild( xmlFirstName );
				else
					xmlAuthorNew.AppendChild(
						_fb2.getXmlDoc().CreateElement( _fb2.getPrefix(), "first-name", _fb2.getNamespaceURI() )
					);
				
				if ( xmlMiddleName != null && !string.IsNullOrWhiteSpace( xmlMiddleName.InnerText ) )
					xmlAuthorNew.AppendChild( xmlMiddleName );
				else
					xmlAuthorNew.AppendChild(
						_fb2.getXmlDoc().CreateElement(_fb2.getPrefix(), "middle-name", _fb2.getNamespaceURI())
					);
				
				if ( xmlLastName != null && !string.IsNullOrWhiteSpace( xmlLastName.InnerText ) )
					xmlAuthorNew.AppendChild( xmlLastName );
				else {
					XmlElement xmlLastNameNew = _fb2.getXmlDoc().CreateElement(_fb2.getPrefix(), "last-name", _fb2.getNamespaceURI());
					xmlLastNameNew.InnerText = "Неизвестный";
					xmlAuthorNew.AppendChild( xmlLastNameNew );
				}
				
				if ( xmlNickName != null && !string.IsNullOrWhiteSpace( xmlNickName.InnerText ) )
					xmlAuthorNew.AppendChild( xmlNickName );
				
				if ( xmlHomePageList != null && xmlHomePageList.Count > 0 ) {
					foreach( XmlNode hp in xmlHomePageList ) {
						if( !string.IsNullOrEmpty( hp.InnerText ) )
							xmlAuthorNew.AppendChild(hp);
					}
				}
				
				if ( xmlEmailList != null && xmlEmailList.Count > 0 ) {
					foreach( XmlNode email in xmlEmailList ) {
						if( !string.IsNullOrEmpty( email.InnerText ) )
							xmlAuthorNew.AppendChild(email);
					}
				}
				
				if ( xmlID != null && !string.IsNullOrWhiteSpace( xmlID.InnerText ) )
					xmlAuthorNew.AppendChild( xmlID );

				return xmlAuthorNew;
			} else
				return xmlAuthor;
		}
		
		// создание нового Автора NewAuthor по заданным данным
		public XmlElement makeAuthorNode( AuthorEnum AuthorType, string FirstName, string MiddleName, string LastName, string Nickname,
		                                 IList<string> HomePage, IList<string> Email, string ID ) {
			XmlElement xmlNewAuthor = _fb2.getXmlDoc().CreateElement(
				_fb2.getPrefix(), AuthorType == Enums.AuthorEnum.Translator
				? "translator"
				: "author",
				_fb2.getNamespaceURI()
			);
			XmlElement el = null;
			el = _fb2.getXmlDoc().CreateElement(_fb2.getPrefix(), "first-name", _fb2.getNamespaceURI());
			if( !string.IsNullOrEmpty(FirstName) )
				el.InnerText = FirstName;
			xmlNewAuthor.AppendChild(el);
			
			if( !string.IsNullOrEmpty(MiddleName) ) {
				el = _fb2.getXmlDoc().CreateElement(_fb2.getPrefix(), "middle-name", _fb2.getNamespaceURI());
				el.InnerText = MiddleName;
				xmlNewAuthor.AppendChild(el);
			}
			
			el = _fb2.getXmlDoc().CreateElement(_fb2.getPrefix(), "last-name", _fb2.getNamespaceURI());
			if( !string.IsNullOrEmpty(LastName) )
				el.InnerText = LastName;
			xmlNewAuthor.AppendChild(el);

			if( !string.IsNullOrEmpty(Nickname) ) {
				el = _fb2.getXmlDoc().CreateElement(_fb2.getPrefix(), "nickname", _fb2.getNamespaceURI());
				el.InnerText = Nickname;
				xmlNewAuthor.AppendChild(el);
			}
			
			if( HomePage != null ) {
				if( HomePage.Count > 0 ) {
					foreach( string hp in HomePage ) {
						if( !string.IsNullOrEmpty( hp ) ) {
							el = _fb2.getXmlDoc().CreateElement(_fb2.getPrefix(), "home-page", _fb2.getNamespaceURI());
							el.InnerText = hp.Trim();
							xmlNewAuthor.AppendChild(el);
						}
					}
				}
			}
			
			if( Email != null ) {
				if( Email.Count > 0 ) {
					foreach( string email in Email ) {
						if( !string.IsNullOrEmpty( email ) ) {
							el = _fb2.getXmlDoc().CreateElement(_fb2.getPrefix(), "email", _fb2.getNamespaceURI());
							el.InnerText = email.Trim();
							xmlNewAuthor.AppendChild(el);
						}
					}
				}
			}
			
			if( !string.IsNullOrEmpty(ID) ) {
				el = _fb2.getXmlDoc().CreateElement(_fb2.getPrefix(), "id", _fb2.getNamespaceURI());
				el.InnerText = ID;
				xmlNewAuthor.AppendChild(el);
			}

			return xmlNewAuthor;
		}
		
		// создание нового Автора NewAuthor по частичным/полным данным Автора FromAuthor
		public XmlElement makeAuthorNodeFromAuthor( AuthorEnum AuthorType, XmlNode FromAuthor ) {
			if( FromAuthor == null ) return null;
			
			XmlElement xmlNewAuthor = _fb2.getXmlDoc().CreateElement(
				_fb2.getPrefix(), AuthorType == Enums.AuthorEnum.Translator ? "translator" : "author", _fb2.getNamespaceURI()
			);
			
			XmlNode		fn = FromAuthor.SelectSingleNode("." + _fb2.getNamespace() + "first-name", _fb2.getNamespaceManager());
			XmlNode		mn = FromAuthor.SelectSingleNode("." + _fb2.getNamespace() + "middle-name", _fb2.getNamespaceManager());
			XmlNode		ln = FromAuthor.SelectSingleNode("." + _fb2.getNamespace() + "last-name", _fb2.getNamespaceManager());
			XmlNode		nn = FromAuthor.SelectSingleNode("." + _fb2.getNamespace() + "nickname", _fb2.getNamespaceManager());
			XmlNodeList	hp = FromAuthor.SelectNodes("." + _fb2.getNamespace() + "home-page", _fb2.getNamespaceManager());
			XmlNodeList	em = FromAuthor.SelectNodes("." + _fb2.getNamespace() + "email", _fb2.getNamespaceManager());
			XmlNode		id = FromAuthor.SelectSingleNode("." + _fb2.getNamespace() + "id", _fb2.getNamespaceManager());

			XmlElement el = null;
			el = _fb2.getXmlDoc().CreateElement(_fb2.getPrefix(), "first-name", _fb2.getNamespaceURI());
			if( fn != null )
				el.InnerText = fn.InnerText;
			xmlNewAuthor.AppendChild(el);
			
			el = _fb2.getXmlDoc().CreateElement(_fb2.getPrefix(), "middle-name", _fb2.getNamespaceURI());
			if( mn != null )
				el.InnerText = mn.InnerText;
			xmlNewAuthor.AppendChild(el);

			el = _fb2.getXmlDoc().CreateElement(_fb2.getPrefix(), "last-name", _fb2.getNamespaceURI());
			if( ln != null )
				el.InnerText = ln.InnerText;
			xmlNewAuthor.AppendChild(el);

			el = _fb2.getXmlDoc().CreateElement(_fb2.getPrefix(), "nickname", _fb2.getNamespaceURI());
			if( nn != null )
				el.InnerText = nn.InnerText;
			xmlNewAuthor.AppendChild(el);
			
			string s = string.Empty;
			el = _fb2.getXmlDoc().CreateElement(_fb2.getPrefix(), "home-page", _fb2.getNamespaceURI());
			if( hp.Count > 0 ) {
				foreach( XmlNode node in hp )
					s += node.InnerText;
				el.InnerText = s;
			}
			xmlNewAuthor.AppendChild(el);

			el = _fb2.getXmlDoc().CreateElement(_fb2.getPrefix(), "email", _fb2.getNamespaceURI());
			if( em.Count > 0 ) {
				foreach( XmlNode node in em )
					s += node.InnerText;
				el.InnerText = s;
			}
			xmlNewAuthor.AppendChild(el);
			
			el = _fb2.getXmlDoc().CreateElement(_fb2.getPrefix(), "id", _fb2.getNamespaceURI());
			if( id != null )
				el.InnerText = id.InnerText;
			xmlNewAuthor.AppendChild(el);

			return xmlNewAuthor;
		}
		
		// создание нового Жанра NewGenre  по заданным данным
		public XmlElement makeGenreNode( string GenreName = "other", string GenreMatch = "100" ) {
			XmlElement xmlNewGenre = _fb2.getXmlDoc().CreateElement( _fb2.getPrefix(), "genre", _fb2.getNamespaceURI() );
			xmlNewGenre.InnerText = !string.IsNullOrEmpty(GenreName) ? GenreName : "other";
			if( !string.IsNullOrEmpty(GenreMatch) )
				xmlNewGenre.SetAttribute("match", GenreMatch);
			return xmlNewGenre;

		}

		// создание новой Серии NewSequence по заданным данным
		public XmlElement makeSequenceNode( string SequenceName, string Number ) {
			XmlElement xmlNewSequence = _fb2.getXmlDoc().CreateElement( _fb2.getPrefix(), "sequence", _fb2.getNamespaceURI() );
			xmlNewSequence.SetAttribute( "name", !string.IsNullOrEmpty(SequenceName) ? SequenceName : "" );
			if( !string.IsNullOrEmpty(Number) )
				xmlNewSequence.SetAttribute("number", Number);
			return xmlNewSequence;
		}
		
		// создание нового Названия книги - BookTitle по заданным данным
		public XmlElement makeBookTitleNode( string BookTitle ) {
			return createStructure( "book-title", BookTitle );
		}
		
		// создание Аннотации на книгу по заданным данным
		public XmlElement makeAnnotationNode( string [] AnnotationArray ) {
			return createStructure( "annotation", ref AnnotationArray );
		}
		
		// создание Ключевых слов по заданным данным
		public XmlElement makeKeywordsNode( string Keywords ) {
			return createStructure( "keywords", Keywords );
		}
		
		// создание новой Даты по заданным данным
		// Date = null - создаем пустую ноду даты
		public XmlElement makeDateNode( string Date = "", string DateValue = "" ) {
			XmlElement xmlDate = _fb2.getXmlDoc().CreateElement( _fb2.getPrefix(), "date", _fb2.getNamespaceURI() );
			if( !string.IsNullOrEmpty(DateValue) )
				xmlDate.SetAttribute( "value", DateValue );
			if ( Date != null ) {
				if( !string.IsNullOrWhiteSpace(Date) )
					xmlDate.InnerText = Date;
				else {
					DateTime localDate = DateTime.Now;
					CultureInfo culture = new CultureInfo("ru-RU");
					xmlDate.InnerText = localDate.ToString(culture);
				}
			}
			
			return xmlDate;
		}
		
		// создание новой структуры Языка книги ("ru" по-умолчанию) по заданным данным
		public XmlElement makeLangNode( string lang = "ru" ) {
			XmlElement xmlLang = _fb2.getXmlDoc().CreateElement( _fb2.getPrefix(), "lang", _fb2.getNamespaceURI() );
			xmlLang.InnerText = !string.IsNullOrEmpty(lang) ? lang : "ru";
			return xmlLang;
		}
		
		// создание новой структуры Языка Оригинала ("en" по-умолчанию) по заданным данным
		public XmlElement makeSrcLangNode( string lang = "en" ) {
			XmlElement xmlLang = _fb2.getXmlDoc().CreateElement( _fb2.getPrefix(), "src-lang", _fb2.getNamespaceURI() );
			xmlLang.InnerText = !string.IsNullOrEmpty(lang) ? lang : "en";
			return xmlLang;
		}
		
		// создание новой структуры program-used по заданным данным
		public XmlElement makeProgramUsedNode( string ProgramUsed = "SharpFBTools" ) {
			return createStructure( "program-used", ProgramUsed );
		}
		
		// создание новой структуры src-ocr по заданным данным
		public XmlElement makeSrcOcrNode( string SrcOcr ) {
			return createStructure( "src-ocr", SrcOcr );
		}
		
		// создание новой структуры src-url по заданным данным
		public IList<XmlNode> makeSrcUrlNode( ref string [] SrcUrlArray ) {
			IList<XmlNode> lSrcUrls = new List<XmlNode>();
			for( int i = 0; i != SrcUrlArray.Length; ++i ) {
				XmlElement xmlElement = _fb2.getXmlDoc().CreateElement( _fb2.getPrefix(), "src-url", _fb2.getNamespaceURI() );
				if( xmlElement != null ) {
					string URL = SrcUrlArray[i].Trim();
					if( !string.IsNullOrEmpty(URL) )
						xmlElement.InnerText = URL;
					lSrcUrls.Add( xmlElement );
				}
			}
			return lSrcUrls;
		}
		
		// создание новой структуры id fb2 файла по заданным данным
		public XmlElement makeIDNode( string ID = "" ) {
			return createStructure( "id", !string.IsNullOrEmpty(ID) ? ID : Guid.NewGuid().ToString().ToUpper() );
		}
		
		// создание новой структуры Версии fb2-файла по заданным данным
		public XmlElement makeVersionNode( string Version = "1.0") {
			return createStructure( "version", Version );
		}
		
		// создание новой Истории развития fb2 файла по заданным данным
		public XmlElement makeHistoryNode( string [] HistoryArray ) {
			return createStructure( "history", ref HistoryArray );
		}
		
		// создание нового Названия Бумажной книги по заданным данным
		public XmlElement makePaperBookNameNode( string BookName ) {
			return createStructure( "book-name", BookName );
		}
		
		// создание новой структуры Издателя бумажной книги по заданным данным
		public XmlElement makePaperPublisherNode( string Publisher ) {
			return createStructure( "publisher", Publisher );
		}
		
		// создание новой структуры Города Издателя бумажной книги по заданным данным
		public XmlElement makePaperCityNode( string City ) {
			return createStructure( "city", City );
		}
		
		// создание новой структуры Года издания бумажной книги по заданным данным
		public XmlElement makePaperYearNode( string Year ) {
			return createStructure( "year", Year );
		}
		
		// создание новой структуры ISBN издания бумажной книги по заданным данным
		public XmlElement makePaperISBNNode( string ISBN ) {
			return createStructure( "isbn", ISBN );
		}
		
		// создание новой структуры custom-info по заданным данным
		public XmlElement makeCustomInfoNode( string InfoType, string ElementValue ) {
			return createStructure( "custom-info", "info-type", ElementValue, InfoType);
		}
		
		// создание новой структуры cover
		public XmlElement makeCoverpageNode( IList<string> ImagesName ) {
			if( ImagesName != null && ImagesName.Count > 0 ) {
				XmlElement xmlCover = _fb2.getXmlDoc().CreateElement( _fb2.getPrefix(), "coverpage", _fb2.getNamespaceURI() );
				if( xmlCover != null ) {
					string L = "l";
					foreach( string Image in ImagesName ) {
						XmlElement xmlImage = _fb2.getXmlDoc().CreateElement( _fb2.getPrefix(), "image", _fb2.getNamespaceURI() );
						XmlNode l = _fb2.getXmlDoc().DocumentElement.GetAttributeNode("xmlns:l");
						if ( l == null ) {
							l = _fb2.getXmlDoc().DocumentElement.GetAttributeNode("xmlns:xlink");
							L = "xlink";
						}
						XmlAttribute xmlHref = _fb2.getXmlDoc().CreateAttribute( l.LocalName, "href", _fb2.getNamespaceURI() );
						xmlHref.InnerText = Image.Substring( 0, 1 ) == "#" ? Image : "#" + Image;
						xmlImage.SetAttributeNode( xmlHref );
						xmlCover.AppendChild( xmlImage );
					}
					// грубый хак: убираем ненужный аттрибут xmlns:l="http://www.gribuser.ru/xml/fictionbook/2.0" или xmlns:l="http://www.gribuser.ru/xml/fictionbook/2.1"
					string ns = _fb2.getNamespace();
					XmlNode xmlFB = _fb2.getFictionBookNode();
					if( xmlFB != null ) {
						xmlFB.AppendChild( xmlCover );
						xmlFB.InnerXml = xmlFB.InnerXml.Replace( "xmlns:"+L+"=\"" + _fb2.getFB2Namespace() + "\"", "" );
						return (XmlElement)_fb2.getXmlDoc().SelectSingleNode( ns + "FictionBook" + ns + "coverpage", _fb2.getNamespaceManager() );
					}
				}
			}
			return null;
		}
		
		// создание новой структуры binary
		public XmlElement makeBinaryNode( string Id, string ContentType, string Base64String ) {
			if( !string.IsNullOrEmpty(Id) && !string.IsNullOrEmpty(ContentType) && !string.IsNullOrEmpty(Base64String) ) {
				XmlElement xmlBinary = _fb2.getXmlDoc().CreateElement( _fb2.getPrefix(), "binary", _fb2.getNamespaceURI() );
				if( xmlBinary != null ) {
					xmlBinary.InnerText = Base64String;
					xmlBinary.SetAttribute("id", Id);
					xmlBinary.SetAttribute("content-type", ContentType);
					return xmlBinary;
				}
			}
			return null;
		}
		
		// создание новой структуры P по заданным данным
		public XmlElement createPStructure( string TextPara ) {
			return createStructure( "p", TextPara );
		}
		
		// создание новой структуры "пустой строки" - <e4mpty-line />
		public XmlElement createEmptyLineStructure() {
			return _fb2.getXmlDoc().CreateElement( _fb2.getPrefix(), "empty-line", _fb2.getNamespaceURI() );
		}
		
		// удаление пробелов, табуляций, переносов строк в тексте тегов
		public void removeWiteSpaceInTagsText( XmlNode ParrentNode ) {
			_fb2.removeWiteSpaceInTagsText( ParrentNode );
		}
		#endregion
		
		#region Открытые методы ПРАВКИ элементов структуры раздела description
		// задание нового ID для книги
		public void setNewID() {
			_fb2.getDocumentInfoNode().ReplaceChild(
				makeIDNode(), _fb2.getFB2IDNode()
			);
		}
		// задание нового Названия книги
		public void setNewBookTitle( string BookTitleNew ) {
			_fb2.getTitleInfoNode(Enums.TitleInfoEnum.TitleInfo).ReplaceChild(
				makeBookTitleNode( BookTitleNew ), _fb2.getBookTitleNode(Enums.TitleInfoEnum.TitleInfo)
			);
		}
		#endregion
		
		#region Открытые методы
		public void saveToFB2File( string FilePath ) {
			_fb2.saveToFB2File( FilePath );
		}
		public static string getEncoding( string FilePath ) {
			string encoding = "UTF-8";
			string str = string.Empty;
			using ( StreamReader reader = File.OpenText(FilePath)) {
				str = reader.ReadLine();
			}
			
			if ( string.IsNullOrWhiteSpace( str ) || str.Length == 0 )
				return encoding;
			
			Match match = Regex.Match( str, "(?<=encoding=\").+?(?=\")", RegexOptions.IgnoreCase);
			if ( match.Success )
				encoding = match.Value;
			return encoding;
		}
		#endregion
		
		#region Закрытые вспомогательные методы
		// проверка, содержит ли тестируемый текст вначале fb2 тего
		private static bool isFB2Tag( string Text, Hashtable htTags ) {
			if ( string.IsNullOrWhiteSpace( Text ) )
				return false;

			int start = Text.IndexOf('<');
			int end = -1;
			if ( start == -1 )
				return false;
			else {
				end = Text.IndexOf(' ');
				if ( end == -1 )
					end = Text.IndexOf('>');
				if ( end == -1 )
					return false;
			}
			return htTags.ContainsValue(Text.Substring(start, end+1 - start));
		}
		// разбивка текста по токенам с границами <>
		private static List<string> splitString( string InputString ) {
			List<string> list = new List<string>();
			int start = 0;
			while (true) {
				// ищем открывающий символ <
				int indexLeft = InputString.IndexOf( '<', start );
				if ( indexLeft != -1 ) {
					_splitString( InputString.Substring( start + 1, indexLeft - start - ( indexLeft > start ? 1 : 0 ) ), ref list );
					// ищем закрывающий символ >
					int indexRight = InputString.IndexOf( '>', indexLeft );
					if ( indexRight != -1 ) {
						_splitString( InputString.Substring( indexLeft, indexRight - indexLeft + 1 ), ref list );
					} else {
						_splitString( InputString.Substring( indexLeft ), ref list );
						break;
					}
					start = indexRight;
				} else {
					list.Add( InputString.Substring(start) );
					break;
				}
			}
			return list;
		}
		// разбивка текста по токенам с границами <>
		private static void _splitString( string InputString, ref List<string> list ) {
			if ( InputString.Equals( "\n" ) ) {
				list.Add( InputString );
				return;
			}
			int start = 0;
			while (true) {
				// ищем открывающий символ <
				int indexLeft = InputString.IndexOf( '<', start );
				if ( indexLeft != -1 ) {
					string s = InputString.Substring( start + 1, indexLeft - start - ( indexLeft > start ? 1 : 0 ) );
					if ( !string.IsNullOrEmpty( s ) )
						list.Add( s );
					// ищем закрывающий символ >
					int indexRight = InputString.IndexOf( '>', indexLeft );
					if ( indexRight != -1 ) {
						string s1 = InputString.Substring( indexLeft, indexRight - indexLeft + 1 );
						if ( !string.IsNullOrEmpty( s ) )
							list.Add( s1 );
					} else {
						string s2 = InputString.Substring( indexLeft );
						if ( !string.IsNullOrEmpty( s ) )
							list.Add( s2 );
						break;
					}
					start = indexRight;
				} else {
					list.Add( InputString );
					break;
				}
			}
		}
		// Автокорректировка теста файла FilePath
		public static void autoCorrector( string FilePath ) {
			FileInfo fi = new FileInfo( FilePath );
			if ( !fi.Exists )
				return;
			else if ( fi.Length < 4 )
				return;
			
			string encoding = getEncoding( FilePath );
			string InputString = string.Empty;
			using (StreamReader reader = new StreamReader( File.OpenRead (FilePath), Encoding.GetEncoding(encoding) ) ) {
				InputString = reader.ReadToEnd();
			}
			
			// разделение файла на части ДО и ПОСЛЕ графики для ускорения обработки и предотвращения случайной порчи кода графики
			string BeforeBinary = string.Empty;
			string AfterBinary = string.Empty;
			int IndexBinary = InputString.IndexOf( "<binary " );
			if ( IndexBinary != -1 ) {
				AfterBinary = InputString.Substring( IndexBinary ); // часть графики
				BeforeBinary = InputString.Substring( 0, IndexBinary ); // часть до графики
				InputString = string.Empty;
			}
			
			// обработка < > в тексте, кроме fb2 тегов
			Hashtable htTags = new Hashtable(_Tags.Length);
			for ( int i = 0; i != _Tags.Length; ++i )
				htTags.Add(i, _Tags[i]);
			
			List<string> list = splitString( IndexBinary != -1 ? BeforeBinary : InputString );
			StringBuilder sbNewString = new StringBuilder( list.Count );
			string token = string.Empty;
			foreach ( string item in list ) {
				token = item.Replace("\r\n", "").TrimEnd();
				if ( isFB2Tag( token, htTags ) )
					sbNewString.AppendLine( token );
				else {
					if ( token.IndexOf("\r\n", 0) == 0 || token.IndexOf("\n", 0) == 0
					    || string.IsNullOrEmpty( token ) || token.IndexOf("\r", 0) == 0 )
						continue;
					else if ( token.Trim().Equals( ">" ) )
						continue;
					else if ( token.IndexOf(">\n", 0) == -1 && token.IndexOf(">\r\n", 0) == -1 )
						sbNewString.AppendLine( token.Replace("<", "&lt;").Replace(">", "&gt;") );
					else
						sbNewString.AppendLine( "\r\n" );
				}
			}

			// автокорректировка файла
			InputString = autoCorrect( IndexBinary != -1 ? BeforeBinary : sbNewString.ToString() );

			// запись откорректированного файла
			using ( StreamWriter writer = new StreamWriter( FilePath, false, Encoding.GetEncoding(encoding) ) ) {
				writer.Write( IndexBinary != -1 ? ( InputString + AfterBinary ) : InputString );
			}
		}
		// Автокорректировка текста строки InputString
		private static string autoCorrect( string InputString ) {
			if ( string.IsNullOrWhiteSpace( InputString ) || InputString.Length == 0 )
				return InputString;
			
			//  правка пространство имен
			string search21 = "xmlns=\"http://www.gribuser.ru/xml/fictionbook/2.1\"";
			string search22 = "xmlns=\"http://www.gribuser.ru/xml/fictionbook/2.2\"";
			string replace = "xmlns=\"http://www.gribuser.ru/xml/fictionbook/2.0\"";
			int index = InputString.IndexOf( search21 );
			if ( index > 0 ) {
				InputString = InputString.Replace( search21, replace );
			} else {
				index = InputString.IndexOf( search22 );
				if ( index > 0 )
					InputString = InputString.Replace( search22, replace );
			}
			
			try {
				// незавершенный тег <p>: <p текст
				InputString = Regex.Replace(InputString, "(^\\s*)(<p)(?=\\s+[^i])", "$2>", RegexOptions.IgnoreCase | RegexOptions.Multiline); // (^\s*)(<p)(?=\s+[^i])
				// восстановление пропущенных </p>
				InputString = Regex.Replace(InputString, "(\\:|;|\"|»\\d|\\w|\\.|,|!|\\?)\\s*(<p>)", "$1</p>\n$2", RegexOptions.IgnoreCase | RegexOptions.Multiline); // (\:|;|\"|»\d|\w|\.|,|!|\?)\s*(<p>)
				// восстановление пропущенных <p>
				InputString = Regex.Replace(InputString, "(</p>)\\s*(\\d|\\w|\\.|,|!|\\?|\\:|;|\"|«)", "$1\n<p>$2", RegexOptions.IgnoreCase | RegexOptions.Multiline); // (</p>)\s*(\d|\w|\.|,|!|\?|\:|;|\"|«)
				
				// обработка жанра
				InputString = Regex.Replace(InputString, "(<genre>)(?:\\s*)(?:proce|literature19)(?:\\s*)(</genre>)", "$1prose$2", RegexOptions.IgnoreCase); // (<genre>)(\s*)(proce|literature19)(\s*)(</genre>)
				// замена <lang> для русских книг на ru для fb2 без <src-title-info>
				if ( InputString.IndexOf( "<src-title-info>" ) == -1 ) {
					if ( Regex.Match(InputString, "[ЯяЭэЮюЁёЪъЬьЫыЖжЧчЩщ]").Success ) { // [ЯяЭэЮюЁёЪъЬьЫыЖжЧчЩщ]
						// только для книг с русскими буквами
						InputString = Regex.Replace(InputString, "(?<=lang>)\\s*.+\\s*(?=</lang>)", "ru", RegexOptions.IgnoreCase | RegexOptions.Multiline); // (?<=lang>)\s*.+\s*(?=</lang>)
					}
				}
				
				// обработка неверно заданного русского языка
				InputString = Regex.Replace(InputString, "(<lang>)(?:\\s*)(?:RU-ru|Rus)(?:\\s*)(</lang>)", "$1ru$2", RegexOptions.IgnoreCase); // (<lang>)(\s*)(RU-ru|Rus)(\s*)(</lang>)
				// обработка неверно заданного английского языка
				InputString = Regex.Replace(InputString, "(<lang>)(?:\\s*)(?:EN-en|Eng)(?:\\s*)(</lang>)", "$1en$2", RegexOptions.IgnoreCase); // (<lang>)(?:\s*)(?:EN-en|Eng)(?:\s*)(</lang>)
				
				// обработка тегов полужирного
				InputString = Regex.Replace(InputString, "<(/?)[bB]\\b((?:[^>\"']|\"[^\"]*\"|'[^']*')*)>", "<$1strong>", RegexOptions.IgnoreCase); // <(/?)[bB]\b((?:[^>"']|"[^"]*"|'[^']*')*)>
				// обработка тегов курсива
				InputString = Regex.Replace(InputString, "<(/?)[iI]\\b((?:[^>\"']|\"[^\"]*\"|'[^']*')*)>", "<$1emphasis>", RegexOptions.IgnoreCase); // <(/?)[iI]\b((?:[^>"']|"[^"]*"|'[^']*')*)>
				// обработка тегов курсива
				InputString = Regex.Replace(InputString, "<(/?)[eE][mM]\\b((?:[^>\"']|\"[^\"]*\"|'[^']*')*)>", "<$1emphasis>", RegexOptions.IgnoreCase); // <(/?)[eE][mM]\b((?:[^>"']|"[^"]*"|'[^']*')*)>
				
				// обработка картинок
				InputString = Regex.Replace(InputString, "(<image .+)(>)(?:\\s*)(?:</image>)", "$1 /$2", RegexOptions.IgnoreCase); //(<image .+)(>)(?:\s*)(?:</image>)
				// обработка тегов body и section
				InputString = Regex.Replace(InputString, "(<body\\b|section\\b)(?:\\s*)xmlns=\"\"(?:\\s*)(>)", "$1$2", RegexOptions.IgnoreCase); // (<body\b|section\b)(?:\s*)xmlns=""(?:\s*)(>)
				// удаление тегов <DIV ...></DIV>
				InputString = Regex.Replace(InputString, "(<[Dd][Ii][Vv]\\s*.+?>)|(<\\s*/[Dd][Ii][Vv])", "", RegexOptions.IgnoreCase); //(<[Dd][Ii][Vv]\s*.+?>)|(<\s*/[Dd][Ii][Vv])
				// удаление тегов <br> <BR> <br/> <BR/> <br /> <BR /> <R>
				InputString = Regex.Replace(InputString, "(?:<br|<BR|<R)(?:\\s*)(?:/?>)", "", RegexOptions.IgnoreCase); // (?:<br|<BR|<R)(?:\s*)(?:/?>)
				// удаление тегов <cite id="nnnnnn" /> , <cite id="nnnnnn"></cite>
				InputString = Regex.Replace(InputString, "<(cite)\\s+id=\"\\w+\"(?:></\\1>|\\s+/>)", "", RegexOptions.IgnoreCase); // <(cite)\s+id="\w+"(?:></\1>|\s+/>)
				
				// обработка вложенных друг в друга тегов strong или emphasis
				InputString = Regex.Replace(InputString, "^\\s*(<strong>|<emphasis>)\\s*\\1\\s*(<p>)", "$2$1", RegexOptions.IgnoreCase | RegexOptions.Multiline) ; // ^\s*(<strong>|<emphasis>)\s*\1\s*(<p>)
				InputString = Regex.Replace(InputString, "(</p>)\\s*(</strong>|</emphasis>)\\s*\\2", "$2$1", RegexOptions.IgnoreCase); // (</p>)\s*(</strong>|</emphasis>)\s*\2
				
				// обработка вложенных друг в друга тегов cite (epigraph)
				InputString = Regex.Replace(InputString, "^\\s*((<(cite|epigraph)>\\s*)\\s*\\2)(.+)(\\s*</\\3>){2}", "<text-author>$3</text-author>", RegexOptions.IgnoreCase | RegexOptions.Multiline); // ^\s*((<(cite|epigraph)>\s*)\s*\2)(.+)(\s*</\3>){2}
				
				// обработка пустого id
				InputString = InputString = Regex.Replace(InputString, "(?<=<id>)\\s*\\s*(?=</id>)", Guid.NewGuid().ToString().ToUpper(), RegexOptions.IgnoreCase);
				
				// обработка Либрусековских id
				InputString = Regex.Replace(InputString, "(?<=<id>)\\s*(Mon|Tue|Wed|Thu|Fri|Sat|Sun)\\s+(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)\\s+\\d{2}\\s+(\\d{2}\\:){2}\\d{2}\\s+\\d{4}\\s*(?=</id>)", Guid.NewGuid().ToString().ToUpper()); // (?<=<id>)\s*(Mon|Tue|Wed|Thu|Fri|Sat|Sun)\s+(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)\s+\d{2}\s+(\d{2}\:){2}\d{2}\s+\d{4}\s*(?=</id>)
				
				// удаление недопустимых символов
				InputString = Regex.Replace(InputString, "(?<=[ЁёА-Яа-я])(&)(?=[-ЁёА-Яа-я])|[\x00-\x08\x0B\x0C\x0E-\x1F]", ""); // (?<=[ЁёА-Яа-я])(&)(?=[-ЁёА-Яа-я])|[\x00-\x08\x0B\x0C\x0E-\x1F]
				// замена & на &amp
				InputString = Regex.Replace(InputString, "([A-Za-zЁёА-Яа-я.,!?«»\"]\\s*)&(\\s*[A-KM-Za-km-zЁёА-Яа-я.,!?«»\"])", "$1&amp;$2"); // ([A-Za-zЁёА-Яа-я.,!?«»\"]\s*)&(\s*[A-KM-Za-km-zЁёА-Яа-я.,!?«»\"])
				
				// Вставка между </epigraph> или <image ... /> (<image ... ></image>) недостающего тега <empty-line/>
				InputString = Regex.Replace(InputString, "((<image\\s+.+/>)|(<image\\s+.+>\\s*</image>)|(</epigraph>))\\s*(</section>)", "$1\n<empty-line/>\n$5"); // ((<image\s+.+/>)|(<image\s+.+>\s*</image>)|(</epigraph>))\s*(</section>)

			} catch (Exception /*ex*/) {
//				MessageBox.Show(ex.Message);
			}
			
			return InputString;
		}
		#endregion
	}
}
