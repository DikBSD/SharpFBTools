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
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Globalization;

//using System.Windows.Forms;

using Core.Common;
using Core.FB2.FB2Parsers;
using Core.AutoCorrector;

using TitleInfoEnum	= Core.Common.Enums.TitleInfoEnum;
using AuthorEnum	= Core.Common.Enums.AuthorEnum;

namespace Core.Common
{
	/// <summary>
	/// Корректура структуры description fb2 книги. Сохранение fb2 - средствами самого fb2
	/// </summary>
	
	public class FB2DescriptionCorrector
	{
		#region Закрытые данные класса
		private readonly FictionBook _fb2 = null;
		private readonly FB2Text _fb2Text = null;
		#endregion
		
		/// <param name="fb2">ссылка на корректируемый экземпляр FictionBook</param>
		public FB2DescriptionCorrector( ref FictionBook fb2 )
		{
			_fb2 = fb2;
			_fb2Text = fb2.getFB2TextXmlIsExists();
		}
		
		#region Открытые методы
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
					Enums.AuthorEnum.AuthorOfBook, string.Empty, string.Empty, "Автор Неизвестен", string.Empty,
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
								Enums.AuthorEnum.AuthorOfBook, string.Empty, string.Empty, "Автор Неизвестен", string.Empty,
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
							Enums.AuthorEnum.AuthorOfBook, string.Empty, string.Empty, "Автор Неизвестен", string.Empty,
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
				
				// для замены в русских fb2 1-го латинского символа в ФИО на соответствующий русский
				bool isRusLang = _fb2.TILang == "ru" ? true : false;
				bool isNeedLatinToRus = false;
				if ( isRusLang && AuthorType == Enums.AuthorEnum.AuthorOfBook )
					isNeedLatinToRus = true;
				LatinToRus latinToRus = new LatinToRus();
				
				if ( xmlFirstName != null && !string.IsNullOrWhiteSpace( xmlFirstName.InnerText ) ) {
					if ( isNeedLatinToRus ) {
						string FirstName = latinToRus.replaceFirstCharLatinToRus( xmlFirstName.InnerText );
						if ( !string.IsNullOrEmpty( FirstName ) )
							xmlFirstName.InnerText = FirstName;
					}
					xmlAuthorNew.AppendChild( xmlFirstName );
				} else
					xmlAuthorNew.AppendChild(
						_fb2.getXmlDoc().CreateElement( _fb2.getPrefix(), "first-name", _fb2.getNamespaceURI() )
					);
				
				if ( xmlMiddleName != null && !string.IsNullOrWhiteSpace( xmlMiddleName.InnerText ) ) {
					if ( isNeedLatinToRus ) {
						string MiddleName = latinToRus.replaceFirstCharLatinToRus( xmlMiddleName.InnerText );
						if ( !string.IsNullOrEmpty( MiddleName ) )
							xmlMiddleName.InnerText = MiddleName;
					}
					xmlAuthorNew.AppendChild( xmlMiddleName );
				} else
					xmlAuthorNew.AppendChild(
						_fb2.getXmlDoc().CreateElement(_fb2.getPrefix(), "middle-name", _fb2.getNamespaceURI())
					);
				
				if ( xmlLastName != null && !string.IsNullOrWhiteSpace( xmlLastName.InnerText ) ) {
					if ( isNeedLatinToRus ) {
						string LastName = latinToRus.replaceFirstCharLatinToRus( xmlLastName.InnerText );
						if ( !string.IsNullOrEmpty( LastName ) )
							xmlLastName.InnerText = LastName;
					}
					xmlAuthorNew.AppendChild( xmlLastName );
				} else {
					XmlElement xmlLastNameNew = _fb2.getXmlDoc().CreateElement(_fb2.getPrefix(), "last-name", _fb2.getNamespaceURI());
					xmlLastNameNew.InnerText = "Автор Неизвестен";
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

	}
}
