/*
 * Сделано в SharpDevelop.
 * Пользователь: Вадим Кузнецов (dikbsd)
 * Дата: 29.06.2015
 * Время: 13:16
 * 
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Collections.Generic;
using System.IO;

using Core.FB2.FB2Parsers;
using Core.FB2.Description.TitleInfo;
using Core.FB2.Description.DocumentInfo;
using Core.FB2.Description.PublishInfo;
using Core.FB2.Description.CustomInfo;
using Core.FB2.Description.Common;
using Core.FB2.Binary;
using Core.FB2.Genres;

using StringProcessing	= Core.Common.StringProcessing;
using ImageWorker		= Core.Common.ImageWorker;
using MiscListView		= Core.Common.MiscListView;

namespace Core.Common
{
	/// <summary>
	/// EditDescriptionForm: Форма для правки метаданных описания fb2 книг
	/// </summary>
	public partial class EditDescriptionForm : Form
	{
		#region Закрытые данные класса
		private FictionBook m_fb2 = null;
		private bool m_ApplyData = false;
		private string m_DirForSavedCover = string.Empty;
		private const string m_sTitle = "Правка метаданных описания fb2 книги";
		private readonly FB2DescriptionCorrector _fB2Corrector = null;
		#endregion
		
		public EditDescriptionForm( FictionBook fb2 )
		{
			#region Код Конструктора
			InitializeComponent();
			m_fb2 = fb2;
			// восстанавливаем структуру
			_fB2Corrector = new FB2DescriptionCorrector( ref m_fb2 );
			_fB2Corrector.recoveryDescriptionNode();
			// первоначальное заполнение контролов
			init();
			// загрузка метаданных TitleInfo книги
			loadFB2TitleInfo( ref m_fb2, Enums.TitleInfoEnum.TitleInfo );
			// загрузка метаданных SourceTitleInfo книги
			loadFB2TitleInfo( ref m_fb2, Enums.TitleInfoEnum.SourceTitleInfo );
			if ( m_fb2.STIAuthors != null || m_fb2.STIBookTitle != null || m_fb2.STILang != null ||
			    m_fb2.STISrcLang != null || m_fb2.STIDate != null || m_fb2.STITranslators != null ||
			    m_fb2.STIGenres != null || m_fb2.STISequences != null || m_fb2.STIKeywords != null ||
			    m_fb2.STIAnnotation != null || m_fb2.STICoverpages != null ) {
				STIEnableCheckBox.Checked = true;
				STITabControl.Enabled = true;
			}
			// загрузка метаданных DocumentInfo
			loadDocumentInfo( ref m_fb2 );
			// загрузка CustomInfo
			loadPublisherInfo( ref m_fb2 );
			// загрузка метаданных CustomInfo
			loadCustomInfo( ref m_fb2 );
			// загрузка Истории развития fb2 файла
			History his = m_fb2.DIHistory;
			if( his != null )
				DIHistoryRichTextEdit.Text =
					StringProcessing.getDeleteAllTags( his.Value != null ? his.Value : string.Empty );
			// загрузка Аннатации на книгу
			Annotation ann = m_fb2.TIAnnotation;
			if( ann != null )
				TIAnnotationRichTextEdit.Text =
					StringProcessing.getDeleteAllTags( ann.Value != null ? ann.Value : string.Empty );
			// загрузка Аннатации на Оригинал книги
			Annotation annOrig = m_fb2.STIAnnotation;
			if( annOrig != null )
				STIAnnotationRichTextEdit.Text =
					StringProcessing.getDeleteAllTags( annOrig.Value != null ? annOrig.Value : string.Empty );
			
			// загрузка обложек книги
			IList<BinaryBase64> Covers = m_fb2.getCoversBase64( Enums.TitleInfoEnum.TitleInfo );
			ImageWorker.makeListViewCoverNameItems( TICoverListView, ref Covers );
			
			// загрузка обложек оригинала книги
			Covers = m_fb2.getCoversBase64( Enums.TitleInfoEnum.SourceTitleInfo );
			ImageWorker.makeListViewCoverNameItems( STICoverListView, ref Covers );
			#endregion
		}
		
		#region Открытые методы класса
		public bool isApplyData() {
			return m_ApplyData;
		}
		public FictionBook getFB2() {
			return m_fb2;
		}
		#endregion
		
		#region Закрытые вспомогательные методы
		// первоначальное заполнение контролов
		private void init() {
			// создание списков языков
			TILangComboBox.Items.AddRange( LangList.LangsList );
			TISrcLangComboBox.Items.AddRange( LangList.LangsList );
			STILangComboBox.Items.AddRange( LangList.LangsList );
			STISrcLangComboBox.Items.AddRange( LangList.LangsList );
			// формирование Списка Групп Жанров
			WorksWithBooks.makeListGenresGroups( TIGroupComboBox );
			WorksWithBooks.makeListGenresGroups( STIGroupComboBox );
		}
		
		#region Вспомогательные методы для ЗАГРУЗКИ метаданных в контролы
		// загрузка Названия книги в зависимости от типа TitleInfo
		private void loadBookTitle( ref FictionBook fb2, Enums.TitleInfoEnum TitleInfoType ) {
			BookTitle bookTitle = TitleInfoType == Enums.TitleInfoEnum.TitleInfo
				? fb2.TIBookTitle : fb2.STIBookTitle;
			if( bookTitle != null ) {
				if( TitleInfoType == Enums.TitleInfoEnum.TitleInfo )
					TIBookTitleTextBox.Text = bookTitle.Value;
				else
					STIBookTitleTextBox.Text = bookTitle.Value;
			}
		}
		// загрузка Языков книги в зависимости от типа TitleInfo
		private void loadFB2Langs( ref FictionBook fb2, Enums.TitleInfoEnum TitleInfoType ) {
			ComboBox cbLang = TitleInfoType == Enums.TitleInfoEnum.TitleInfo ? TILangComboBox : STILangComboBox;
			ComboBox cbSrcLang = TitleInfoType == Enums.TitleInfoEnum.TitleInfo ? TISrcLangComboBox : STISrcLangComboBox;
			string Lang = TitleInfoType == Enums.TitleInfoEnum.TitleInfo
				? "(" + fb2.TILang + ")" : "(" + fb2.STILang + ")";
			string SrcLang = TitleInfoType == Enums.TitleInfoEnum.TitleInfo
				? "(" + fb2.TISrcLang + ")" : "(" + fb2.STISrcLang + ")";
			if( !string.IsNullOrEmpty(Lang) ) {
				for( int i = 0; i != cbLang.Items.Count; ++i ) {
					string s = cbLang.Items[i].ToString().ToLower();
					if( s.IndexOf( Lang.ToLower() ) > -1 ) {
						cbLang.SelectedIndex = i;
						break;
					}
				}
			}
			if( !string.IsNullOrEmpty(SrcLang) ) {
				for( int i = 0; i != cbSrcLang.Items.Count; ++i ) {
					string s = cbSrcLang.Items[i].ToString().ToLower();
					if( s.IndexOf( SrcLang.ToLower() ) > -1 ) {
						cbSrcLang.SelectedIndex = i;
						break;
					}
				}
			}
		}
		// загрузка Жанров взависимости от типа TitleInfo
		private void loadGenres( ref FictionBook fb2, Enums.TitleInfoEnum TitleInfoType ) {
			ListView lv = TitleInfoType == Enums.TitleInfoEnum.TitleInfo
				? TIGenresListView : STIGenresListView;
			
			FB2UnionGenres fb2g = new FB2UnionGenres();
			
			IList<Genre> Genres = TitleInfoType == Enums.TitleInfoEnum.TitleInfo
				? fb2.TIGenres : fb2.STIGenres;
			
			if( Genres != null ) {
				foreach( Genre g in Genres ) {
					if ( !WorksWithBooks.genreIsExist( lv, g, fb2g ) ) {
						ListViewItem lvi = new ListViewItem(
							!string.IsNullOrEmpty( g.Name )
							? fb2g.GetFBGenreName( g.Name ) + " (" + g.Name + ")"
							: string.Empty
						);
						lvi.SubItems.Add( !string.IsNullOrEmpty( g.Math.ToString() ) ? g.Math.ToString() : string.Empty );
						if ( !string.IsNullOrEmpty( g.Name ) )
							lv.Items.Add( lvi );
					}
				}
			}
		}
		// загрузка Даты создания книги в зависимости от типа TitleInfo
		private void loadDate( ref FictionBook fb2, Enums.TitleInfoEnum TitleInfoType ) {
			Date date = TitleInfoType == Enums.TitleInfoEnum.TitleInfo ? fb2.TIDate : fb2.STIDate;
			if( date != null ) {
				TextBox mtbDate = TitleInfoType == Enums.TitleInfoEnum.TitleInfo
					? TIDateTextBox : STIDateTextBox;
				MaskedTextBox mtbValue = TitleInfoType == Enums.TitleInfoEnum.TitleInfo
					? TIDateValueMaskedTextBox : STIDateValueMaskedTextBox;
				mtbDate.Text = date.Text;
				mtbValue.Text = date.Value;
			}
		}
		// загрузка метаданных автора/переводчика книги / создателя fb2 файла
		private void loadAuthors( ref FictionBook fb2, Enums.TitleInfoEnum TitleInfoType,
		                         Enums.AuthorEnum AuthorType ) {
			IList<Author> Authors = null;
			ListView lv = null;
			if( TitleInfoType == Enums.TitleInfoEnum.TitleInfo ) {
				if( AuthorType == Enums.AuthorEnum.AuthorOfBook ) {
					Authors = fb2.TIAuthors;
					lv = TIAuthorsListView;
				} else if( AuthorType == Enums.AuthorEnum.Translator ) {
					Authors = fb2.TITranslators;
					lv = TITranslatorListView;
				} else {
					//AuthorType == Enums.AuthorEnum.AuthorOfFB2
					Authors = fb2.DIAuthors;
					lv = DIFB2AuthorListView;
				}
			} else {
				// TitleInfoType == Enums.TitleInfoEnum.SourceTitleInfo
				if( AuthorType == Enums.AuthorEnum.AuthorOfBook ) {
					Authors = fb2.STIAuthors;
					lv = STIAuthorsListView;
				} else if( AuthorType == Enums.AuthorEnum.Translator ) {
					Authors = fb2.STITranslators;
					lv = STITranslatorListView;
				} else {
					//AuthorType == Enums.AuthorEnum.AuthorOfFB2
					Authors = fb2.DIAuthors;
					lv = DIFB2AuthorListView;
				}
			}
			
			if( Authors != null ) {
				foreach( Author a in Authors ) {
					if( a != null ) {
						if ( !WorksWithBooks.authorIsExist( lv, a ) ) {
							ListViewItem lvi = null;
							if( a.LastName != null )
								lvi = new ListViewItem( !string.IsNullOrEmpty( a.LastName.Value ) ? a.LastName.Value : string.Empty );
							else
								lvi = new ListViewItem( "" );
							if( a.FirstName != null )
								lvi.SubItems.Add( !string.IsNullOrEmpty( a.FirstName.Value ) ? a.FirstName.Value : string.Empty );
							else
								lvi.SubItems.Add( string.Empty );
							if( a.MiddleName != null )
								lvi.SubItems.Add( !string.IsNullOrEmpty( a.MiddleName.Value ) ? a.MiddleName.Value : string.Empty );
							else
								lvi.SubItems.Add( string.Empty );
							if( a.NickName != null )
								lvi.SubItems.Add( !string.IsNullOrEmpty( a.NickName.Value ) ? a.NickName.Value : string.Empty );
							else
								lvi.SubItems.Add( string.Empty );
							lvi.SubItems.Add( StringProcessing.makeStringFromListItems( a.HomePages ) );
							lvi.SubItems.Add( StringProcessing.makeStringFromListItems( a.Emails ) );
							
							lvi.SubItems.Add( a.ID != null ? a.ID : string.Empty );
							
							lv.Items.Add( lvi );
						}
					}
				}
			}
		}
		// загрузка метаданных Серий книги / Серий Бумажной книги
		private void loadSequences( ref FictionBook fb2, Enums.TitleInfoEnum TitleInfoType,
		                           Enums.SequenceEnum SequenceType ) {
			IList<Sequence> Sequences = null;
			ListView lv = null;
			if( SequenceType == Enums.SequenceEnum.Ebook ) {
				Sequences = TitleInfoType == Enums.TitleInfoEnum.TitleInfo
					? fb2.TISequences : fb2.STISequences;
				lv = TitleInfoType == Enums.TitleInfoEnum.TitleInfo
					? TISequenceListView : STISequenceListView;
			} else {
				// SequenceType == Enums.SequenceEnum.PaperBook
				Sequences = fb2.PISequences;
				lv = PISequenceListView;
			}
			
			if( Sequences != null ) {
				foreach( Sequence seq in Sequences ) {
					if( seq != null ) {
						ListViewItem lvi = null;
						lvi = new ListViewItem( !string.IsNullOrEmpty( seq.Name ) ? seq.Name : string.Empty );
						lvi.SubItems.Add( !string.IsNullOrEmpty( seq.Number ) ? seq.Number : string.Empty );
						lv.Items.Add( lvi );
					}
				}
			}
		}
		// загрузка ключевых слов
		private void loadKeyWords( ref FictionBook fb2, Enums.TitleInfoEnum TitleInfoType ) {
			TextBox tbKey = TitleInfoType == Enums.TitleInfoEnum.TitleInfo ? TIKeyTextBox : STIKeyTextBox;
			Keywords keys = TitleInfoType == Enums.TitleInfoEnum.TitleInfo ? fb2.TIKeywords : fb2.STIKeywords;
			if( keys != null )
				tbKey.Text = keys.Value;
		}
		// загрузка метаданных TitleInfo книги
		private void loadFB2TitleInfo( ref FictionBook fb2, Enums.TitleInfoEnum TitleInfoType ) {
			// book-info
			loadBookTitle( ref fb2, TitleInfoType );
			// языки
			loadFB2Langs( ref fb2, TitleInfoType );
			// genres
			loadGenres( ref fb2, TitleInfoType );
			// date
			loadDate( ref fb2, TitleInfoType );
			// Авторы книги
			loadAuthors( ref fb2, TitleInfoType, Enums.AuthorEnum.AuthorOfBook );
			// Переводчики книги
			loadAuthors( ref fb2, TitleInfoType, Enums.AuthorEnum.Translator );
			// ключевые слова
			loadKeyWords( ref fb2, TitleInfoType );
			// Серии
			loadSequences( ref fb2, TitleInfoType, Enums.SequenceEnum.Ebook );
		}
		// загрузка метаданных DocumentInfo
		private void loadDocumentInfo( ref FictionBook fb2 ) {
			DIIDTextBox.Text = !string.IsNullOrEmpty( fb2.DIID ) ? fb2.DIID : string.Empty;
			DIVersionTextBox.Text = !string.IsNullOrEmpty( fb2.DIVersion ) ? fb2.DIVersion : string.Empty;
			DIOCRTextBox.Text = fb2.DISrcOCR != null ? fb2.DISrcOCR.Value : string.Empty;
			DIProgramUsedTextBox.Text = fb2.DIProgramUsed != null ? fb2.DIProgramUsed.Value : string.Empty;
			// SrcUrls
			IList<string> lSrcUrls = fb2.DISrcUrls;
			if( lSrcUrls != null ) {
				if( lSrcUrls.Count > 0 ) {
					foreach( string s in lSrcUrls )
						DIURLTextBox.Text += s + "; ";
					DIURLTextBox.Text = DIURLTextBox.Text.Trim();
					if( DIURLTextBox.Text[DIURLTextBox.TextLength-1] == ';' )
						DIURLTextBox.Text = DIURLTextBox.Text.Remove(DIURLTextBox.TextLength-1);
				}
			}

			// Date
			Date date = fb2.DIDate;
			if( date != null ) {
				DIDateTextBox.Text = date.Text;
				DIDateValueMaskedTextBox.Text = date.Value;
			}
			// Создатели fb2 файла
			loadAuthors( ref fb2, Enums.TitleInfoEnum.TitleInfo, Enums.AuthorEnum.AuthorOfFB2 );
		}
		// загрузка метаданных PublisherInfo
		private void loadPublisherInfo( ref FictionBook fb2 ) {
			// BookName
			BookName bn = fb2.PIBookName;
			if( bn != null )
				DIBookNameTextBox.Text = !string.IsNullOrEmpty( bn.Value ) ? bn.Value : string.Empty;
			// Publisher
			Publisher pub = fb2.PIPublisher;
			if( pub != null )
				DIPublisherTextBox.Text = !string.IsNullOrEmpty( pub.Value ) ? pub.Value : string.Empty;
			// City
			City city = fb2.PICity;
			if( city != null )
				DICityTextBox.Text = !string.IsNullOrEmpty( city.Value ) ? city.Value : string.Empty;
			// Year
			DIYearTextBox.Text = !string.IsNullOrEmpty( fb2.PIYear ) ? fb2.PIYear : string.Empty;
			// ISBN
			ISBN isbn = fb2.PIISBN;
			if( isbn != null )
				DIISBNTextBox.Text = !string.IsNullOrEmpty( isbn.Value ) ? isbn.Value : string.Empty;
			// Sequence
			loadSequences( ref fb2, Enums.TitleInfoEnum.TitleInfo, Enums.SequenceEnum.PaperBook );
		}
		// загрузка метаданных CustomInfo
		private void loadCustomInfo( ref FictionBook fb2 ) {
			IList<CustomInfo> lCI = fb2.getCustomInfo();
			if( lCI != null ) {
				if( lCI.Count > 0 ) {
					foreach( CustomInfo ci in lCI ) {
						if( ci != null ) {
							ListViewItem lvi = null;
							lvi = new ListViewItem( !string.IsNullOrEmpty( ci.InfoType ) ? ci.InfoType : string.Empty );
							lvi.SubItems.Add( !string.IsNullOrEmpty( ci.Value ) ? ci.Value : string.Empty );
							CICustomInfoListView.Items.Add( lvi );
						}
					}
				}
			}
		}
		#endregion
		
		#region Вспомогательные методы Удаления структур метаданных
		private bool isImageExsistInBody( string Href ) {
			XmlNodeList xmlBodys = m_fb2.getBodyNodes();
			foreach( XmlNode xmlBody in xmlBodys ) {
				string Body = xmlBody.InnerXml;
				if( Body.IndexOf( Href ) > -1 )
					return true;
			}
			return false;
		}
		// удаление Image тэга Cover по Href
		private void deleteCoverImageForHref( Enums.TitleInfoEnum TitleInfoType, string Href ) {
			XmlNode xmlCover = m_fb2.getCoverNode( TitleInfoType );
			if( xmlCover != null ) {
				XmlNode xmlImageNode = m_fb2.getCoverImageNodeForHref( TitleInfoType, "#" + Href );
				if( xmlImageNode != null )
					xmlCover.RemoveChild( xmlImageNode );
			}
		}
		// удаление binary Обложки по ID
		private void deleteCoverBinaryForID( string ID ) {
			XmlNode xmlFB = m_fb2.getFictionBookNode();
			if( xmlFB != null ) {
				XmlNode xmlCoverBinaryNode = m_fb2.getBinaryNodeForID( ID );
				if( xmlCoverBinaryNode != null )
					xmlFB.RemoveChild( xmlCoverBinaryNode );
			}
		}
		// удаление Обложк (cover и binary) по Href (ID)
		private void deleteCoverForHref( Enums.TitleInfoEnum TitleInfoType, string Href ) {
			deleteCoverImageForHref( TitleInfoType, Href );
			if( ! isImageExsistInBody( Href ) )
				deleteCoverBinaryForID( Href );
		}
		
		// удаление тэга cover - т.е. удаление всех Обложек
		private void deleteCoverTag( Enums.TitleInfoEnum TitleInfoType ) {
			XmlNode xmlCover = m_fb2.getCoverNode( TitleInfoType );
			if( xmlCover != null )
				m_fb2.getTitleInfoNode( TitleInfoType ).RemoveChild( xmlCover );
		}
		// удаление всех binary тэга cover - т.е. удаление всех Обложек
		private void deleteAllBinaryTagsOfCover( ref IList<string> lIDList ) {

			XmlNode xmlFB = m_fb2.getFictionBookNode();
			XmlNodeList xmlBinaryNodes = m_fb2.getBinaryNodes();
			if( xmlBinaryNodes != null && xmlBinaryNodes.Count > 0 ) {
				foreach( string ID in lIDList ) {
					if( ! isImageExsistInBody( ID ) )
						deleteCoverBinaryForID( ID );
				}
			}
		}
		// удаление всех Обложек (cover и binary)
		private void deleteAllCover( Enums.TitleInfoEnum TitleInfoType, ref IList<string> lIDList ) {
			deleteCoverTag( TitleInfoType );
			deleteAllBinaryTagsOfCover( ref lIDList );
		}
		#endregion
		
		#region Вспомогательные методы СОЗДАНИЯ структур метаданных
		private IList<XmlNode> makeAuthorNode( Enums.AuthorEnum AuthorType, ref FictionBook fb2, ListView lv ) {
			FB2DescriptionCorrector fB2Corrector = new FB2DescriptionCorrector( ref fb2 );
			IList<XmlNode> Authors = null;
			XmlNode xmlAuthor = null;
			if( lv.Items.Count > 0 ) {
				Authors = new List<XmlNode>( lv.Items.Count );
				foreach( ListViewItem item in lv.Items ) {
					string HPs = StringProcessing.trimLastTemplateSymbol( item.SubItems[4].Text.Trim(), new Char [] { ',',';' } );
					IList<string> lHPs = HPs.Split( new Char [] { ',',';' } );
					string Emails = StringProcessing.trimLastTemplateSymbol( item.SubItems[5].Text.Trim(), new Char [] { ',',';' } );
					IList<string> lEmails = Emails.Split( new Char [] { ',',';' } );
					xmlAuthor = fB2Corrector.makeAuthorNode(
						AuthorType,
						item.SubItems[1].Text, item.SubItems[2].Text, item.Text, item.SubItems[3].Text,
						lHPs, lEmails, item.SubItems[6].Text
					);
					Authors.Add(xmlAuthor);
				}
			} else {
				if( AuthorType == Enums.AuthorEnum.AuthorOfBook ) {
					Authors = new List<XmlNode>();
					xmlAuthor = fB2Corrector.makeAuthorNode( AuthorType, null, null, null, null, null, null, null );
					Authors.Add(xmlAuthor);
				}
			}
			return Authors;
		}
		private XmlNode makeTitleInfoNode( ref XmlDocument xmlDoc, Enums.TitleInfoEnum TitleInfoType ) {
			if( TitleInfoType == Enums.TitleInfoEnum.SourceTitleInfo && !STIEnableCheckBox.Checked )
				return null;
			
			string TitleInfoNode = string.Empty;
			ListView GenresListView;
			ListView AuthorsListView;
			TextBox BookTitleTextBox;
			RichTextBox AnnotationRichTextEdit;
			TextBox KeyTextBox;
			TextBox DateTextBox;
			MaskedTextBox DateValueMaskedTextBox;
			ComboBox LangComboBox;
			ComboBox SrcLangComboBox;
			ListView TranslatorListView;
			ListView SequenceListView;
			ListView CoverListView;
			
			if( TitleInfoType == Enums.TitleInfoEnum.TitleInfo ) {
				TitleInfoNode = "title-info";
				GenresListView = TIGenresListView;
				AuthorsListView = TIAuthorsListView;
				BookTitleTextBox = TIBookTitleTextBox;
				AnnotationRichTextEdit = TIAnnotationRichTextEdit;
				KeyTextBox = TIKeyTextBox;
				DateTextBox = TIDateTextBox;
				DateValueMaskedTextBox = TIDateValueMaskedTextBox;
				LangComboBox = TILangComboBox;
				SrcLangComboBox = TISrcLangComboBox;
				TranslatorListView = TITranslatorListView;
				SequenceListView = TISequenceListView;
				CoverListView = TICoverListView;
			} else {
				TitleInfoNode = "src-title-info";
				GenresListView = STIGenresListView;
				AuthorsListView = STIAuthorsListView;
				BookTitleTextBox = STIBookTitleTextBox;
				AnnotationRichTextEdit = STIAnnotationRichTextEdit;
				KeyTextBox = STIKeyTextBox;
				DateTextBox = STIDateTextBox;
				DateValueMaskedTextBox = STIDateValueMaskedTextBox;
				LangComboBox = STILangComboBox;
				SrcLangComboBox = STISrcLangComboBox;
				TranslatorListView = STITranslatorListView;
				SequenceListView = STISequenceListView;
				CoverListView = STICoverListView;
			}
			
			XmlNode xmlTI = xmlDoc.CreateElement( m_fb2.getPrefix(), TitleInfoNode, m_fb2.getNamespaceURI() );
			// Жанры
			XmlNode xmlGenre = null;
			if( GenresListView.Items.Count > 0 ) {
				foreach( ListViewItem item in GenresListView.Items ) {
					string code = item.Text.Substring( item.Text.IndexOf('(') + 1 );
					xmlGenre = _fB2Corrector.makeGenreNode( code.Substring( 0, code.Length - 1), item.SubItems[1].Text );
					xmlTI.AppendChild( xmlGenre );
				}
			} else {
				xmlGenre = _fB2Corrector.makeGenreNode( "other", null );
				xmlTI.AppendChild( xmlGenre );
			}
			
			// Авторы
			IList<XmlNode> Authors = makeAuthorNode( Enums.AuthorEnum.AuthorOfBook, ref m_fb2, AuthorsListView );
			foreach( XmlNode Author in Authors )
				xmlTI.AppendChild( Author );
			
			// Book Title
			xmlTI.AppendChild( _fB2Corrector.makeBookTitleNode( BookTitleTextBox.Text.Trim() ) );
			
			// Аннотация
			XmlElement Annot = _fB2Corrector.makeAnnotationNode( AnnotationRichTextEdit.Lines );
			if( Annot != null )
				if( !string.IsNullOrEmpty( Annot.InnerText.Trim() ) )
					xmlTI.AppendChild( Annot );
			
			// keywords
			if( !string.IsNullOrEmpty( KeyTextBox.Text.Trim() ) )
				xmlTI.AppendChild( _fB2Corrector.makeKeywordsNode( KeyTextBox.Text.Trim() ) );
			
			// date
			string DateValue = DateValueMaskedTextBox.Text.Trim();
			if( !string.IsNullOrEmpty( DateTextBox.Text.Trim() ) ) {
				if( !DateValue.Equals( "-  -" ) )
					xmlTI.AppendChild( _fB2Corrector.makeDateNode( DateTextBox.Text.Trim(), DateValue ) );
				else
					xmlTI.AppendChild( _fB2Corrector.makeDateNode( DateTextBox.Text.Trim(), null ) );
			} else {
				if( !DateValue.Equals( "-  -" ) )
					xmlTI.AppendChild( _fB2Corrector.makeDateNode( null, DateValue ) );
				else
					xmlTI.AppendChild( _fB2Corrector.makeDateNode( null, null ) );
			}
			
			// coverpage и binary
			// удаление всех binary Обложек
			IList<string> lIDList = new List<string>( CoverListView.Items.Count );
			foreach( ListViewItem item in CoverListView.Items )
				lIDList.Add( item.Text );
			deleteAllBinaryTagsOfCover( ref lIDList );
			
			if( CoverListView.Items.Count > 0 ) {
				// coverpage
				IList<string> list = new List<string>();
				foreach( ListViewItem item in CoverListView.Items )
					list.Add( item.Text );
				
				xmlTI.AppendChild(
					_fB2Corrector.makeCoverpageNode( list )
				);
				
				// binary
				XmlNode xmlFB = m_fb2.getFictionBookNode();
				foreach( ListViewItem item in CoverListView.Items ) {
					// добавление всех binary Обложек
					if( m_fb2.getBinaryNodeForID( item.Text ) == null )
						xmlFB.AppendChild(
							_fB2Corrector.makeBinaryNode(
								item.Text, item.SubItems[1].Text, item.Tag.ToString()
							)
						);
				}
			}
			
			// lang
			if( !string.IsNullOrEmpty( LangComboBox.Text ) )
				xmlTI.AppendChild(
					_fB2Corrector.makeLangNode( LangComboBox.Text.Substring( LangComboBox.Text.IndexOf('(')+1, 2 ) )
				);
			else
				xmlTI.AppendChild( _fB2Corrector.makeLangNode() );
			
			// src-lang
			if( !string.IsNullOrEmpty( SrcLangComboBox.Text ) ) {
				xmlTI.AppendChild(
					_fB2Corrector.makeSrcLangNode( SrcLangComboBox.Text.Substring( SrcLangComboBox.Text.IndexOf('(')+1, 2 ) )
				);
			}
			
			// translator
			IList<XmlNode> Translators = makeAuthorNode( Enums.AuthorEnum.Translator, ref m_fb2, TranslatorListView ) ;
			if( Translators != null ) {
				foreach( XmlNode Translator in Translators )
					xmlTI.AppendChild( Translator );
			}
			
			// sequence
			if( SequenceListView.Items.Count > 0 ) {
				foreach( ListViewItem item in SequenceListView.Items )
					xmlTI.AppendChild( _fB2Corrector.makeSequenceNode( item.Text, item.SubItems[1].Text ) );
			}
			return xmlTI;
		}
		private bool processTitleInfoNode( ref XmlDocument xmlDoc, XmlNode xmlTINew ) {
			string ns = m_fb2.getNamespace();
			XmlNode xmlTIOld = xmlDoc.SelectSingleNode( ns + "FictionBook" + ns + "description" + ns + "title-info", m_fb2.getNamespaceManager() );
			XmlNode xmlDesc = m_fb2.getDescriptionNode();
			if( xmlDesc != null ) {
				if( xmlTIOld != null ) {
					xmlDesc.ReplaceChild( xmlTINew, xmlTIOld );
					return true;
				}
			}
			return false;
		}
		private bool processSourceTitleInfoNode( ref XmlDocument xmlDoc, XmlNode xmlSTINew ) {
			string ns = m_fb2.getNamespace();
			XmlNode xmlSTIOld = xmlDoc.SelectSingleNode( ns + "FictionBook" + ns + "description" + ns + "src-title-info", m_fb2.getNamespaceManager() );
			XmlNode xmlDesc = m_fb2.getDescriptionNode();
			if( xmlDesc != null ) {
				if( STIEnableCheckBox.Checked ) {
					if( xmlSTIOld != null ) {
						xmlDesc.ReplaceChild( xmlSTINew, xmlSTIOld );
						return true;
					} else {
						XmlNode xmlTI = m_fb2.getTitleInfoNode( Enums.TitleInfoEnum.TitleInfo );
						if( xmlTI != null ) {
							xmlDesc.InsertAfter( xmlSTINew, xmlTI );
							return true;
						}
					}
				} else {
					if( xmlSTIOld != null ) {
						xmlDesc.RemoveChild( xmlSTIOld );
						return true;
					}
				}
			}
			return false;
		}
		private XmlNode makeDocumentInfoNode( ref XmlDocument xmlDoc ) {
			XmlNode xmlDI = xmlDoc.CreateElement( m_fb2.getPrefix(), "document-info", m_fb2.getNamespaceURI() );
			
			// Авторы
			IList<XmlNode> Authors = makeAuthorNode( Enums.AuthorEnum.AuthorOfFB2, ref m_fb2, DIFB2AuthorListView ) ;
			foreach( XmlNode Author in Authors )
				xmlDI.AppendChild( Author );
			
			// Program Used
			XmlElement xmlProgramUsed = _fB2Corrector.makeProgramUsedNode( DIProgramUsedTextBox.Text.Trim() );
			if( xmlProgramUsed != null )
				xmlDI.AppendChild( xmlProgramUsed );
			
			// date
			string DateValue = DIDateValueMaskedTextBox.Text.Trim();
			if( !string.IsNullOrWhiteSpace( DIDateTextBox.Text ) ) {
				if( !DateValue.Equals( "-  -" ) )
					xmlDI.AppendChild( _fB2Corrector.makeDateNode( DIDateTextBox.Text.Trim(), DateValue ) );
				else
					xmlDI.AppendChild( _fB2Corrector.makeDateNode( DIDateTextBox.Text.Trim(), null ) );
			} else {
				if( !DateValue.Equals( "-  -" ) )
					xmlDI.AppendChild( _fB2Corrector.makeDateNode( null, DateValue ) );
				else
					xmlDI.AppendChild( _fB2Corrector.makeDateNode( null, null ) );
			}
			
			// src-url
			if( !string.IsNullOrWhiteSpace( DIURLTextBox.Text ) ) {
				string [] URLs = DIURLTextBox.Text.Split( new Char [] { ',',';' } );
				IList<XmlNode> lSrcUrls = _fB2Corrector.makeSrcUrlNode( ref URLs );
				if( lSrcUrls != null && lSrcUrls.Count > 0 ) {
					foreach( XmlNode URL in lSrcUrls )
						xmlDI.AppendChild( URL );
				}
			}
			
			// src-ocr
			XmlElement xmlSrcOcr = _fB2Corrector.makeSrcOcrNode( DIOCRTextBox.Text.Trim() );
			if( xmlSrcOcr != null )
				xmlDI.AppendChild( xmlSrcOcr );
			
			// id
			xmlDI.AppendChild( _fB2Corrector.makeIDNode( DIIDTextBox.Text.Trim() ) );
			
			// version
			xmlDI.AppendChild( _fB2Corrector.makeVersionNode( DIVersionTextBox.Text.Trim() ) );
			
			// history
			XmlElement History = _fB2Corrector.makeHistoryNode( DIHistoryRichTextEdit.Lines );
			if( History != null )
				if( !string.IsNullOrWhiteSpace( History.InnerText ) )
					xmlDI.AppendChild( History );
			return xmlDI;
		}
		private bool processDocumentInfoNode( ref XmlDocument xmlDoc, XmlNode xmlDINew ) {
			string ns = m_fb2.getNamespace();
			XmlNode xmlDIOld = xmlDoc.SelectSingleNode( ns + "FictionBook" + ns + "description" + ns + "document-info", m_fb2.getNamespaceManager() );
			XmlNode xmlDesc = m_fb2.getDescriptionNode();
			if( xmlDesc != null ) {
				if( xmlDIOld != null ) {
					xmlDesc.ReplaceChild( xmlDINew, xmlDIOld );
					return true;
				}
			}
			return false;
		}
		private XmlNode makePublishInfoNode( ref XmlDocument xmlDoc ) {
			bool PIExists = false;
			XmlNode xmlPI = xmlDoc.CreateElement( m_fb2.getPrefix(), "publish-info", m_fb2.getNamespaceURI() );
			
			// Book Name
			XmlNode bn = _fB2Corrector.makePaperBookNameNode( DIBookNameTextBox.Text.Trim() );
			if( bn != null ) {
				xmlPI.AppendChild( bn );
				PIExists = true;
			}
			
			// publisher
			XmlNode pub = _fB2Corrector.makePaperPublisherNode( DIPublisherTextBox.Text.Trim() );
			if( pub != null ) {
				xmlPI.AppendChild( pub );
				PIExists = true;
			}
			
			// city
			XmlNode city = _fB2Corrector.makePaperCityNode( DICityTextBox.Text.Trim() );
			if( city != null ) {
				xmlPI.AppendChild( city);
				PIExists = true;
			}
			
			// year
			XmlNode year = _fB2Corrector.makePaperYearNode( DIYearTextBox.Text.Trim() );
			if( year != null ) {
				xmlPI.AppendChild( year );
				PIExists = true;
			}
			
			// isbn
			XmlNode isbn = _fB2Corrector.makePaperISBNNode( DIISBNTextBox.Text.Trim() );
			if( isbn != null ) {
				xmlPI.AppendChild( isbn );
				PIExists = true;
			}
			
			// sequence
			XmlNode xmlSequence = null;
			if( PISequenceListView.Items.Count > 0 ) {
				foreach( ListViewItem item in PISequenceListView.Items ) {
					xmlSequence = _fB2Corrector.makeSequenceNode( item.Text, item.SubItems[1].Text );
					xmlPI.AppendChild( xmlSequence );
				}
				PIExists = true;
			}
			
			return PIExists ? xmlPI : null;
		}
		private bool processPublishInfoNode( ref XmlDocument xmlDoc, XmlNode xmlPINew ) {
			string ns = m_fb2.getNamespace();
			XmlNode xmlPIOld = xmlDoc.SelectSingleNode( ns + "FictionBook" + ns + "description" + ns + "publish-info", m_fb2.getNamespaceManager() );
			XmlNode xmlDesc = m_fb2.getDescriptionNode();
			if( xmlDesc != null ) {
				if( xmlPINew != null ) {
					if( xmlPIOld != null ) {
						xmlDesc.ReplaceChild( xmlPINew, xmlPIOld );
						return true;
					}
				} else {
					if( xmlPIOld != null ) {
						xmlDesc.RemoveChild( xmlPIOld );
						return true;
					}
				}
			}
			return false;
		}
		private bool processCustomInfoNode( ref XmlDocument xmlDoc ) {
			string ns = m_fb2.getNamespace();
			XmlNodeList xmlCIOldList = xmlDoc.SelectNodes( ns + "FictionBook" + ns + "description" + ns + "custom-info", m_fb2.getNamespaceManager() );
			XmlNode xmlDesc = m_fb2.getDescriptionNode();
			
			if( xmlDesc != null ) {
				if( xmlCIOldList != null && xmlCIOldList.Count > 0 ) {
					foreach( XmlNode node in xmlCIOldList )
						xmlDesc.RemoveChild( node );
				}
				
				if( CICustomInfoListView.Items.Count > 0 ) {
					foreach( ListViewItem item in CICustomInfoListView.Items )
						xmlDesc.AppendChild(
							_fB2Corrector.makeCustomInfoNode( item.Text, item.SubItems[1].Text )
						);
					return true;
				}
			}
			return false;
		}
		#endregion
		
		#region Вспомогательные методы ДЛЯ РАБОТЫ КОНТРОЛОВ
		// создание нового Автора книги / Переводчика книги / Создателя fb2-файла
		private void createNewAuthor( ListView lv, Enums.AuthorEnum AuthorType ) {
			AuthorInfo ai = new AuthorInfo( AuthorType, true );
			AuthorInfoForm authorInfoForm = new AuthorInfoForm( ref ai );
			authorInfoForm.ShowDialog();
			AuthorInfo NewAuthorInfo = authorInfoForm.AuthorInfo;
			ListViewItem lvi = new ListViewItem( NewAuthorInfo.LastName );
			lvi.SubItems.Add( NewAuthorInfo.FirstName );
			lvi.SubItems.Add( NewAuthorInfo.MiddleName );
			lvi.SubItems.Add( NewAuthorInfo.NickName );
			lvi.SubItems.Add( NewAuthorInfo.HomePage );
			lvi.SubItems.Add( NewAuthorInfo.Email );
			lvi.SubItems.Add( NewAuthorInfo.ID );
			lv.Items.Add( lvi );
			authorInfoForm.Dispose();
		}
		// редактирование Автора книги / Переводчика книги / Создателя fb2-файла
		private void editSelectedAuthor( ListView lv, Enums.AuthorEnum AuthorType ) {
			if( lv.SelectedItems.Count > 0 ) {
				ListViewItem lvi = lv.SelectedItems[0];
				AuthorInfo ai = new AuthorInfo( AuthorType, false,
				                               lvi.Text, lvi.SubItems[1].Text, lvi.SubItems[2].Text, lvi.SubItems[3].Text,
				                               lvi.SubItems[4].Text, lvi.SubItems[5].Text, lvi.SubItems[6].Text );
				Core.Common.AuthorInfoForm authorInfoForm = new Core.Common.AuthorInfoForm( ref ai );
				authorInfoForm.ShowDialog();
				AuthorInfo NewAuthorInfo = authorInfoForm.AuthorInfo;
				lvi.Text = NewAuthorInfo.LastName;
				lvi.SubItems[1].Text = NewAuthorInfo.FirstName;
				lvi.SubItems[2].Text = NewAuthorInfo.MiddleName;
				lvi.SubItems[3].Text = NewAuthorInfo.NickName;
				lvi.SubItems[4].Text = NewAuthorInfo.HomePage;
				lvi.SubItems[5].Text = NewAuthorInfo.Email;
				lvi.SubItems[6].Text = NewAuthorInfo.ID;
				authorInfoForm.Dispose();
			}
			lv.Focus();
		}
		// создание новой Серии для Электронной / Бумажной книги
		private void createNewSequence( ListView lv, Enums.SequenceEnum SequenceType ) {
			SequenceInfo si = new SequenceInfo( SequenceType, true );
			Core.Common.SequenceInfoForm sequenceInfoForm = new Core.Common.SequenceInfoForm( ref si );
			sequenceInfoForm.ShowDialog();
			SequenceInfo NewSequenceInfo = sequenceInfoForm.SequenceInfo;
			ListViewItem lvi = new ListViewItem( NewSequenceInfo.Name );
			lvi.SubItems.Add( NewSequenceInfo.Number );
			lv.Items.Add( lvi );
			sequenceInfoForm.Dispose();
		}
		// редактирование Серии для Электронной / Бумажной книги
		private void editSelectedSequence( ListView lv, Enums.SequenceEnum SequenceType ) {
			if( lv.SelectedItems.Count > 0 ) {
				ListViewItem lvi = lv.SelectedItems[0];
				SequenceInfo si = new SequenceInfo( SequenceType, false, lvi.Text, lvi.SubItems[1].Text );
				Core.Common.SequenceInfoForm sequenceInfoForm = new Core.Common.SequenceInfoForm( ref si );
				sequenceInfoForm.ShowDialog();
				SequenceInfo NewSequenceInfo = sequenceInfoForm.SequenceInfo;
				lvi.Text = NewSequenceInfo.Name;
				lvi.SubItems[1].Text = NewSequenceInfo.Number;
				sequenceInfoForm.Dispose();
			}
			lv.Focus();
		}
		// поиск максимально номера обложки
		private int getMaxCoverNumber( ListView lv ) {
			int MaxNumber = -1;
			foreach( ListViewItem item in lv.Items ) {
				int ind = item.Text.IndexOf("cover");
				if( ind != -1 ) {
					string Number = item.Text.Substring(ind + 5).Split('.')[0];
					if( StringProcessing.IsNumberInString( Number ) ) {
						int Numb = Convert.ToInt16( Number );
						if( MaxNumber < Numb )
							MaxNumber = Numb;
					}
				}
			}
			return MaxNumber;
		}
		// добавление обложек в список
		private void addCoverToList( Enums.TitleInfoEnum TitleInfoType )
		{
			ListView CoverListView = TitleInfoType == Enums.TitleInfoEnum.TitleInfo
				? TICoverListView : STICoverListView;
			CoverOpenFileDialog.InitialDirectory = Settings.Settings.ProgDir;
			CoverOpenFileDialog.Title		= "Выберите изображение/изображения для Обложки/Обложек";
			CoverOpenFileDialog.Filter		= "Файлы изображений (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png";
			CoverOpenFileDialog.FileName	= string.Empty;
			DialogResult result				= CoverOpenFileDialog.ShowDialog();
			if( result == DialogResult.OK ) {
				XmlNode xmlFB = m_fb2.getFictionBookNode();
				for( int i = 0; i != CoverOpenFileDialog.FileNames.Length; ++i ) {
					// занесение данных в список
					string FilePath = CoverOpenFileDialog.FileNames[i];
					string CoverName = "cover" + StringProcessing.makeIINumber( i+1 ) + Path.GetExtension( FilePath ).ToLower();
					if( MiscListView.isExistListViewItem( CoverListView, CoverName ) ) {
						// поиск максимально номера обложки
						int MaxNumber = getMaxCoverNumber( CoverListView );
						CoverName = "cover" + StringProcessing.makeIINumber( ++MaxNumber ) + Path.GetExtension( FilePath ).ToLower();
					}
					
					string NotOpenedCorers = string.Empty;
					try {
						ListViewItem lvi = new ListViewItem( CoverName );
						lvi.SubItems.Add( ImageWorker.getContentType( FilePath ) );
						
						using ( System.Drawing.Image image = System.Drawing.Image.FromFile(FilePath) ) {
							lvi.SubItems.Add( string.Format( "{0} x {1} dpi", image.VerticalResolution, image.HorizontalResolution ) );
							lvi.SubItems.Add( string.Format( "{0} x {1} Pixels", image.Width, image.Height ) );
							
						}
						FileInfo file = new FileInfo( FilePath );
						lvi.SubItems.Add( FilesWorker.FormatFileLength( file.Length ) );
						
						lvi.Tag = ImageWorker.toBase64( FilePath );
						CoverListView.Items.Add( lvi );
					} catch ( System.Exception /*e*/ ) {
						NotOpenedCorers += FilePath + "\n";
					}
					
					if ( !string.IsNullOrEmpty( NotOpenedCorers ) )
						MessageBox.Show(
							"Не могу открыть следующие 'битые' обложки:\n" + NotOpenedCorers, m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning
						);
				}
			}
		}
		#endregion
		#endregion
		
		#region Обработчики событий
		/* Title Info */
		// Жанры
		void TIGenreAddButtonClick(object sender, EventArgs e)
		{
			if( !MiscListView.isExistListViewItem( TIGenresListView, TIGenresComboBox.Text ) ) {
				ListViewItem lvi = new ListViewItem( TIGenresComboBox.Text );
				lvi.SubItems.Add( TIMatchMaskedTextBox.Text.Trim() );
				TIGenresListView.Items.Add( lvi );
				TIMatchMaskedTextBox.Clear();
			}
		}
		void TIGenreDeleteButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteSelectedItem( TIGenresListView, m_sTitle, "Жанров" );
		}
		void TIGenreDeleteAllButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteAllItems( TIGenresListView, m_sTitle, "Жанров" );
		}
		void TIGenreUpButtonClick(object sender, EventArgs e)
		{
			if( TIGenresListView.Items.Count > 0 && TIGenresListView.SelectedItems.Count > 0 ) {
				if( TIGenresListView.SelectedItems.Count == 1 ) {
					MiscListView.moveUpSelectedItem( TIGenresListView );
				} else {
					MessageBox.Show( "Выберите только один Жанр для перемещения!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				}
			}
			TIGenresListView.Select();
		}
		void TIGenreDownButtonClick(object sender, EventArgs e)
		{
			if( TIGenresListView.Items.Count > 0 && TIGenresListView.SelectedItems.Count > 0 ) {
				if( TIGenresListView.SelectedItems.Count == 1 ) {
					MiscListView.moveDownSelectedItem( TIGenresListView );
				} else {
					MessageBox.Show( "Выберите только один Жанр для перемещения!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				}
			}
			TIGenresListView.Select();
		}
		void TIGroupComboBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			// формирование Списка Жанров в контролы, в зависимости от Группы
			WorksWithBooks.makeListGenres( TIGenresComboBox, TIGroupComboBox.Text );
		}
		// Авторы Книги
		void TIAuthorAddButtonClick(object sender, EventArgs e)
		{
			// создание нового Автора книги
			createNewAuthor( TIAuthorsListView, Enums.AuthorEnum.AuthorOfBook );
		}
		void TIAuthorEditButtonClick(object sender, EventArgs e)
		{
			// редактирование Автора книги
			editSelectedAuthor( TIAuthorsListView, Enums.AuthorEnum.AuthorOfBook );
		}
		void TIAuthorDeleteButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteSelectedItem( TIAuthorsListView, m_sTitle, "Авторов Книги" );
		}
		void TIAuthorDeleteAllButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteAllItems( TIAuthorsListView, m_sTitle, "Авторов Книги" );
		}
		void TIAuthorUpButtonClick(object sender, EventArgs e)
		{
			if( TIAuthorsListView.Items.Count > 0 && TIAuthorsListView.SelectedItems.Count > 0 ) {
				if( TIAuthorsListView.SelectedItems.Count == 1 )
					MiscListView.moveUpSelectedItem( TIAuthorsListView );
				else
					MessageBox.Show( "Выберите только одного Автора для перемещения!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
			TIAuthorsListView.Select();
		}
		void TIAuthorDownButtonClick(object sender, EventArgs e)
		{
			if( TIAuthorsListView.Items.Count > 0 && TIAuthorsListView.SelectedItems.Count > 0 ) {
				if( TIAuthorsListView.SelectedItems.Count == 1 )
					MiscListView.moveDownSelectedItem( TIAuthorsListView );
				else
					MessageBox.Show( "Выберите только одного Автора для перемещения!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
			TIAuthorsListView.Select();
		}
		// Переводчики
		void TITranslatorAddButtonClick(object sender, EventArgs e)
		{
			// создание нового Переводчика книги
			createNewAuthor( TITranslatorListView, Enums.AuthorEnum.Translator );
		}
		void TITranslatorEditButtonClick(object sender, EventArgs e)
		{
			// редактирование Переводчика книги
			editSelectedAuthor( TITranslatorListView, Enums.AuthorEnum.Translator );
		}
		void TITranslatorDeleteButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteSelectedItem( TITranslatorListView, m_sTitle, "Переводчиков Книги" );
		}
		void TITranslatorDeleteAllButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteAllItems( TITranslatorListView, m_sTitle, "Переводчиков Книги" );
		}
		// Серии
		void TISequenceAddButtonClick(object sender, EventArgs e)
		{
			// создание новой Серии для Электронной книги
			createNewSequence( TISequenceListView, Enums.SequenceEnum.Ebook );
		}
		void TISequenceEditButtonClick(object sender, EventArgs e)
		{
			// редактирование Серии для Электронной книги
			editSelectedSequence( TISequenceListView, Enums.SequenceEnum.Ebook );
		}
		void TISequenceDeleteButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteSelectedItem( TISequenceListView, m_sTitle, "Серий Книги" );
		}
		void TISequenceDeleteAllButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteAllItems( TISequenceListView, m_sTitle, "Серий Книги" );
		}
		void TISequenceUpButtonClick(object sender, EventArgs e)
		{
			if( TISequenceListView.Items.Count > 0 && TISequenceListView.SelectedItems.Count > 0 ) {
				if( TISequenceListView.SelectedItems.Count == 1 )
					MiscListView.moveUpSelectedItem( TISequenceListView );
				else
					MessageBox.Show( "Выберите только одну Серию для перемещения!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
			TISequenceListView.Select();
		}
		void TISequenceDownButtonClick(object sender, EventArgs e)
		{
			if( TISequenceListView.Items.Count > 0 && TISequenceListView.SelectedItems.Count > 0 ) {
				if( TISequenceListView.SelectedItems.Count == 1 )
					MiscListView.moveDownSelectedItem( TISequenceListView );
				else
					MessageBox.Show( "Выберите только одну Серию для перемещения!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
			TISequenceListView.Select();
		}
		
		/* Source Title Info */
		void STIEnableCheckBoxClick(object sender, EventArgs e)
		{
			STITabControl.Enabled = STIEnableCheckBox.Checked;
		}
		// Жанры
		void STIGenreAddButtonClick(object sender, EventArgs e)
		{
			if( !MiscListView.isExistListViewItem( STIGenresListView, STIGenresComboBox.Text ) ) {
				ListViewItem lvi = new ListViewItem( STIGenresComboBox.Text );
				lvi.SubItems.Add( STIMatchMaskedTextBox.Text.Trim() );
				STIGenresListView.Items.Add( lvi );
				STIMatchMaskedTextBox.Clear();
			}
		}
		void STIGenreDeleteButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteSelectedItem( STIGenresListView, m_sTitle, "Жанров" );
		}
		void STIGenreDeleteAllButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteAllItems( STIGenresListView, m_sTitle, "Жанров" );
		}
		void STIGenreUpButtonClick(object sender, EventArgs e)
		{
			if( STIGenresListView.Items.Count > 0 && STIGenresListView.SelectedItems.Count > 0 ) {
				if( STIGenresListView.SelectedItems.Count == 1 ) {
					MiscListView.moveUpSelectedItem( STIGenresListView );
				} else {
					MessageBox.Show( "Выберите только один Жанр для перемещения!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				}
			}
			STIGenresListView.Select();
		}
		void STIGenreDownButtonClick(object sender, EventArgs e)
		{
			if( STIGenresListView.Items.Count > 0 && STIGenresListView.SelectedItems.Count > 0 ) {
				if( STIGenresListView.SelectedItems.Count == 1 ) {
					MiscListView.moveDownSelectedItem( STIGenresListView );
				} else {
					MessageBox.Show( "Выберите только один Жанр для перемещения!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				}
			}
			STIGenresListView.Select();
		}
		void STIGroupComboBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			// формирование Списка Жанров в контролы, в зависимости от Группы
			WorksWithBooks.makeListGenres( STIGenresComboBox, STIGroupComboBox.Text );
		}
		// Авторы книги
		void STIAuthorAddButtonClick(object sender, EventArgs e)
		{
			// создание нового Автора книги
			createNewAuthor( STIAuthorsListView, Enums.AuthorEnum.AuthorOfBook );
		}
		void STIAuthorEditButtonClick(object sender, EventArgs e)
		{
			// редактирование Автора книги
			editSelectedAuthor( STIAuthorsListView, Enums.AuthorEnum.AuthorOfBook );
		}
		void STIAuthorDeleteButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteSelectedItem( STIAuthorsListView, m_sTitle, "Авторов Книги" );
		}
		void STIAuthorDeleteAllButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteAllItems( STIAuthorsListView, m_sTitle, "Авторов Книги" );
		}
		void STIAuthorUpButtonClick(object sender, EventArgs e)
		{
			if( STIAuthorsListView.Items.Count > 0 && STIAuthorsListView.SelectedItems.Count > 0 ) {
				if( STIAuthorsListView.SelectedItems.Count == 1 )
					MiscListView.moveUpSelectedItem( STIAuthorsListView );
				else
					MessageBox.Show( "Выберите только одного Автора для перемещения!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
			STIAuthorsListView.Select();
		}
		void STIAuthorDownButtonClick(object sender, EventArgs e)
		{
			if( STIAuthorsListView.Items.Count > 0 && STIAuthorsListView.SelectedItems.Count > 0 ) {
				if( STIAuthorsListView.SelectedItems.Count == 1 )
					MiscListView.moveDownSelectedItem( STIAuthorsListView );
				else
					MessageBox.Show( "Выберите только одного Автора для перемещения!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
			STIAuthorsListView.Select();
		}
		// Переводчики
		void STITranslatorAddButtonClick(object sender, EventArgs e)
		{
			// создание нового Переводчика книги
			createNewAuthor( STITranslatorListView, Enums.AuthorEnum.Translator );
		}
		void STITranslatorEditButtonClick(object sender, EventArgs e)
		{
			// редактирование Переводчика книги
			editSelectedAuthor( STITranslatorListView, Enums.AuthorEnum.Translator );
		}
		void STITranslatorDeleteButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteSelectedItem( STITranslatorListView, m_sTitle, "Переводчиков Книги" );
		}
		void STITranslatorDeleteAllButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteAllItems( STITranslatorListView, m_sTitle, "Переводчиков Книги" );
		}
		// Серии
		void STISequenceAddButtonClick(object sender, EventArgs e)
		{
			// создание новой Серии для Электронной книги
			createNewSequence( STISequenceListView, Enums.SequenceEnum.Ebook );
		}
		void STISequenceEditButtonClick(object sender, EventArgs e)
		{
			// редактирование Серии для Электронной книги
			editSelectedSequence( STISequenceListView, Enums.SequenceEnum.Ebook );
		}
		void STISequenceDeleteButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteSelectedItem( STISequenceListView, m_sTitle, "Серий Книги" );
		}
		void STISequenceDeleteAllButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteAllItems( STISequenceListView, m_sTitle, "Серий Книги" );
		}
		void STISequenceUpButtonClick(object sender, EventArgs e)
		{
			if( STISequenceListView.Items.Count > 0 && STISequenceListView.SelectedItems.Count > 0 ) {
				if( STISequenceListView.SelectedItems.Count == 1 )
					MiscListView.moveUpSelectedItem( STISequenceListView );
				else
					MessageBox.Show( "Выберите только одну Серию для перемещения!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
			STISequenceListView.Select();
		}
		void STISequenceDownButtonClick(object sender, EventArgs e)
		{
			if( STISequenceListView.Items.Count > 0 && STISequenceListView.SelectedItems.Count > 0 ) {
				if( STISequenceListView.SelectedItems.Count == 1 )
					MiscListView.moveDownSelectedItem( STISequenceListView );
				else
					MessageBox.Show( "Выберите только одну Серию для перемещения!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
			STISequenceListView.Select();
		}
		
		/* Document Info */
		void DINewIDButtonClick(object sender, EventArgs e)
		{
			string sMess = "Создать новый id?";
			const MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			DialogResult result = MessageBox.Show( sMess, "Создание нового id", buttons, MessageBoxIcon.Question );
			if( result == DialogResult.Yes )
				DIIDTextBox.Text = Guid.NewGuid().ToString().ToUpper();
		}
		void DIFB2AuthorAddButtonClick(object sender, EventArgs e)
		{
			// создание нового Создателя fb2 файла
			createNewAuthor( DIFB2AuthorListView, Enums.AuthorEnum.AuthorOfFB2 );
		}
		void DIFB2AuthorEditButtonClick(object sender, EventArgs e)
		{
			// редактирование Создателя fb2 файла
			editSelectedAuthor( DIFB2AuthorListView, Enums.AuthorEnum.AuthorOfFB2 );
		}
		void DIFB2AuthorDeleteButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteSelectedItem( DIFB2AuthorListView, m_sTitle, "Создателей fb2 файла" );
		}
		void DIFB2AuthorDeleteAllButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteAllItems( DIFB2AuthorListView, m_sTitle, "Создателей fb2 файла" );
		}
		void DIFB2AuthorUpButtonClick(object sender, EventArgs e)
		{
			if( DIFB2AuthorListView.Items.Count > 0 && DIFB2AuthorListView.SelectedItems.Count > 0 ) {
				if( DIFB2AuthorListView.SelectedItems.Count == 1 )
					MiscListView.moveUpSelectedItem( DIFB2AuthorListView );
				else
					MessageBox.Show( "Выберите только одного Автора fb2-файла для перемещения!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
			DIFB2AuthorListView.Select();
		}
		void DIFB2AuthorDownButtonClick(object sender, EventArgs e)
		{
			if( DIFB2AuthorListView.Items.Count > 0 && DIFB2AuthorListView.SelectedItems.Count > 0 ) {
				if( DIFB2AuthorListView.SelectedItems.Count == 1 )
					MiscListView.moveDownSelectedItem( DIFB2AuthorListView );
				else
					MessageBox.Show( "Выберите только одного Автора fb2-файла для перемещения!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
			DIFB2AuthorListView.Select();
		}
		
		/* Publish Info */
		void PISequenceAddButtonClick(object sender, EventArgs e)
		{
			// создание новой Серии для Бумажной книги
			createNewSequence( PISequenceListView, Enums.SequenceEnum.PaperBook );
		}
		void PISequenceEditButtonClick(object sender, EventArgs e)
		{
			// редактирование Серии для Бумажной книги
			editSelectedSequence( PISequenceListView, Enums.SequenceEnum.PaperBook );
		}
		void PISequenceDeleteButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteSelectedItem( PISequenceListView, m_sTitle, "Бумажной книги" );
		}
		void PISequenceDeleteAllButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteAllItems( PISequenceListView, m_sTitle, "Бумажной книги" );
		}
		void PISequenceUpButtonClick(object sender, EventArgs e)
		{
			if( PISequenceListView.Items.Count > 0 && PISequenceListView.SelectedItems.Count > 0 ) {
				if( PISequenceListView.SelectedItems.Count == 1 )
					MiscListView.moveUpSelectedItem( PISequenceListView );
				else
					MessageBox.Show( "Выберите только одну Серию для перемещения!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
			PISequenceListView.Select();
		}
		void PISequenceDownButtonClick(object sender, EventArgs e)
		{
			if( PISequenceListView.Items.Count > 0 && PISequenceListView.SelectedItems.Count > 0 ) {
				if( PISequenceListView.SelectedItems.Count == 1 )
					MiscListView.moveDownSelectedItem( PISequenceListView );
				else
					MessageBox.Show( "Выберите только одну Серию для перемещения!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
			PISequenceListView.Select();
		}
		
		/* CustomInfo */
		void CICustomInfoAddButtonClick(object sender, EventArgs e)
		{
			// создание новой Дополнительной информации
			CustomInfoInfo ci = new CustomInfoInfo( true );
			Core.Common.CustomInfoForm customInfoForm = new Core.Common.CustomInfoForm( ref ci );
			customInfoForm.ShowDialog();
			CustomInfoInfo NewCustomInfoInfo = customInfoForm.CustomInfoInfo;
			ListViewItem lvi = new ListViewItem( NewCustomInfoInfo.Type );
			lvi.SubItems.Add( NewCustomInfoInfo.Value );
			CICustomInfoListView.Items.Add( lvi );
			customInfoForm.Dispose();
		}
		void CICustomInfoEditButtonClick(object sender, EventArgs e)
		{
			// редактирование выбранной Дополнительной информации
			if( CICustomInfoListView.SelectedItems.Count > 0 ) {
				ListViewItem lvi = CICustomInfoListView.SelectedItems[0];
				CustomInfoInfo ci = new CustomInfoInfo( false, lvi.Text, lvi.SubItems[1].Text );
				Core.Common.CustomInfoForm customInfoForm = new Core.Common.CustomInfoForm( ref ci );
				customInfoForm.ShowDialog();
				CustomInfoInfo NewCustomInfoInfo = customInfoForm.CustomInfoInfo;
				lvi.Text = NewCustomInfoInfo.Type;
				lvi.SubItems[1].Text = NewCustomInfoInfo.Value;
				customInfoForm.Dispose();
			}
			CICustomInfoListView.Focus();
		}
		void CICustomInfoDeleteButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteSelectedItem( CICustomInfoListView, m_sTitle, "Дополнительных данных" );
		}
		void CICustomInfoDeleteAllButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteAllItems( CICustomInfoListView, m_sTitle, "Дополнительных данных" );
		}
		void CICustomInfoUpButtonClick(object sender, EventArgs e)
		{
			if( CICustomInfoListView.Items.Count > 0 && CICustomInfoListView.SelectedItems.Count > 0 ) {
				if( CICustomInfoListView.SelectedItems.Count == 1 )
					MiscListView.moveUpSelectedItem( CICustomInfoListView );
				else
					MessageBox.Show( "Выберите только один элемент для перемещения!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
			CICustomInfoListView.Select();
		}
		void CICustomInfoDownButtonClick(object sender, EventArgs e)
		{
			if( CICustomInfoListView.Items.Count > 0 && CICustomInfoListView.SelectedItems.Count > 0 ) {
				if( CICustomInfoListView.SelectedItems.Count == 1 )
					MiscListView.moveDownSelectedItem( CICustomInfoListView );
				else
					MessageBox.Show( "Выберите только один элемент для перемещения!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
			CICustomInfoListView.Select();
		}
		
		/* Обложки */
		void TICoverListViewClick(object sender, EventArgs e)
		{
			if( TICoverListView.SelectedItems.Count > 0 )
				TICoverPictureBox.Image = ImageWorker.base64ToImage( TICoverListView.SelectedItems[0].Tag.ToString() );
		}
		void TICoverAddButtonClick(object sender, EventArgs e)
		{
			addCoverToList( Enums.TitleInfoEnum.TitleInfo );
		}
		void TICoverDeleteButtonClick(object sender, EventArgs e)
		{
			// удаление выбранной обложки в тэге cover
			if( TICoverListView.SelectedItems.Count > 0 ) {
				string Href = TICoverListView.SelectedItems[0].Text;
				if( MiscListView.deleteSelectedItem( TICoverListView, m_sTitle, "Обложек" ) )
					deleteCoverForHref( Enums.TitleInfoEnum.TitleInfo, Href );
			}
		}
		void TICoverDeleteAllButtonClick(object sender, EventArgs e)
		{
			IList<string> lIDList = new List<string>( TICoverListView.Items.Count );
			foreach( ListViewItem item in TICoverListView.Items )
				lIDList.Add( item.Text );
			if( MiscListView.deleteAllItems( TICoverListView, m_sTitle, "Обложек" ) )
				// удаление тэга cover и всех binary Обложек
				deleteAllCover( Enums.TitleInfoEnum.TitleInfo, ref lIDList );
		}
		void TICoverUpButtonClick(object sender, EventArgs e)
		{
			if( TICoverListView.Items.Count > 0 && TICoverListView.SelectedItems.Count > 0 ) {
				if( TICoverListView.SelectedItems.Count == 1 )
					MiscListView.moveUpSelectedItem( TICoverListView );
				else
					MessageBox.Show( "Выберите только одну Обложку для перемещения!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
			TICoverListView.Select();
		}
		void TICoverDownButtonClick(object sender, EventArgs e)
		{
			if( TICoverListView.Items.Count > 0 && TICoverListView.SelectedItems.Count > 0 ) {
				if( TICoverListView.SelectedItems.Count == 1 )
					MiscListView.moveDownSelectedItem( TICoverListView );
				else
					MessageBox.Show( "Выберите только одну Обложку для перемещения!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
			TICoverListView.Select();
		}
		void TICoverSaveAllSelectedImageButtonClick(object sender, EventArgs e)
		{
			// сохранение выделенных обложек на диск
			ImageWorker.saveSelectedCovers( TICoverListView, ref m_DirForSavedCover, m_sTitle, fbdSaveDir );
		}
		
		void STICoverListViewClick(object sender, EventArgs e)
		{
			if( STICoverListView.SelectedItems.Count > 0 )
				STICoverPictureBox.Image = ImageWorker.base64ToImage( STICoverListView.SelectedItems[0].Tag.ToString() );
		}
		void STICoverAddButtonClick(object sender, EventArgs e)
		{
			addCoverToList( Enums.TitleInfoEnum.SourceTitleInfo );
		}
		void STICoverDeleteButtonClick(object sender, EventArgs e)
		{
			// удаление выбранной обложки в тэге cover Оригинала
			if( STICoverListView.SelectedItems.Count > 0 ) {
				string Href = STICoverListView.SelectedItems[0].Text;
				if( MiscListView.deleteSelectedItem( STICoverListView, m_sTitle, "Обложек" ) )
					deleteCoverForHref( Enums.TitleInfoEnum.SourceTitleInfo, Href );
			}
		}
		void STICoverDeleteAllButtonClick(object sender, EventArgs e)
		{
			IList<string> lIDList = new List<string>( STICoverListView.Items.Count );
			foreach( ListViewItem item in STICoverListView.Items )
				lIDList.Add( item.Text );
			if( MiscListView.deleteAllItems( STICoverListView, m_sTitle, "Обложек" ) )
				// удаление тэга cover и всех binary Обложек Оригинала
				deleteAllCover( Enums.TitleInfoEnum.TitleInfo, ref lIDList );
		}
		void STICoverUpButtonClick(object sender, EventArgs e)
		{
			if( STICoverListView.Items.Count > 0 && STICoverListView.SelectedItems.Count > 0 ) {
				if( STICoverListView.SelectedItems.Count == 1 )
					MiscListView.moveUpSelectedItem( STICoverListView );
				else
					MessageBox.Show( "Выберите только одну Обложку для перемещения!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
			STICoverListView.Select();
		}
		void STICoverDownButtonClick(object sender, EventArgs e)
		{
			if( STICoverListView.Items.Count > 0 && STICoverListView.SelectedItems.Count > 0 ) {
				if( STICoverListView.SelectedItems.Count == 1 )
					MiscListView.moveDownSelectedItem( STICoverListView );
				else
					MessageBox.Show( "Выберите только одну Обложку для перемещения!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
			STICoverListView.Select();
		}
		void STICoverSaveAllSelectedImageButtonClick(object sender, EventArgs e)
		{
			// сохранение выделенных обложек на диск
			ImageWorker.saveSelectedCovers( STICoverListView, ref m_DirForSavedCover, m_sTitle, fbdSaveDir );
		}
		
		void CancelBtnClick(object sender, EventArgs e)
		{
			Close();
		}
		void ApplyBtnClick(object sender, EventArgs e)
		{
			if( string.IsNullOrWhiteSpace(TIBookTitleTextBox.Text) ) {
				TITabControl.SelectedTab = tpTittleInfoGeneral;
				tcViewFB2Desc.SelectedTab = tpTitleInfo;
				MessageBox.Show( "Поле Названия книги должно быть обязательно заполнено!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				TIBookTitleTextBox.Focus();
				return;
			} else if ( TILangComboBox.SelectedIndex == -1 ) {
				TITabControl.SelectedTab = tpTittleInfoGeneral;
				tcViewFB2Desc.SelectedTab = tpTitleInfo;
				MessageBox.Show( "Поле Языка книги должно быть обязательно заполнено!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				TILangComboBox.Focus();
				return;
			} else if ( TIGenresListView.Items.Count == 0 ) {
				TITabControl.SelectedTab = tpTittleInfoGeneral;
				tcViewFB2Desc.SelectedTab = tpTitleInfo;
				MessageBox.Show( "Задайте хотя бы один Жанр книги!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				TIGenresComboBox.Focus();
				return;
			} else if( string.IsNullOrWhiteSpace(DIIDTextBox.Text) ) {
				tcViewFB2Desc.SelectedTab = tpDocumentInfo;
				MessageBox.Show( "Поле ID книги должно быть обязательно заполнено!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				DIIDTextBox.Focus();
				return;
			} else if( string.IsNullOrWhiteSpace(DIVersionTextBox.Text) ) {
				tcViewFB2Desc.SelectedTab = tpDocumentInfo;
				MessageBox.Show( "Поле Версии книги должно быть обязательно заполнено!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				DIVersionTextBox.Focus();
				return;
			}
			
			if( STIEnableCheckBox.Checked ) {
				if( string.IsNullOrWhiteSpace(STIBookTitleTextBox.Text) ) {
					STITabControl.SelectedTab = tpSTittleInfoGeneral;
					tcViewFB2Desc.SelectedTab = tpSourceTitleInfo;
					MessageBox.Show( "Поле Названия книги для Оригинала должно быть обязательно заполнено!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					STIBookTitleTextBox.Focus();
					return;
				} else if ( STILangComboBox.SelectedIndex == -1 ) {
					STITabControl.SelectedTab = tpSTittleInfoGeneral;
					tcViewFB2Desc.SelectedTab = tpSourceTitleInfo;
					MessageBox.Show( "Поле Языка книги для Оригинала должно быть обязательно заполнено!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					STILangComboBox.Focus();
					return;
				} else if ( STIGenresListView.Items.Count == 0 ) {
					STITabControl.SelectedTab = tpSTittleInfoGeneral;
					tcViewFB2Desc.SelectedTab = tpSourceTitleInfo;
					MessageBox.Show( "Задайте хотя бы один Жанр для Оригинала книги!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					STIGenresComboBox.Focus();
					return;
				}
			}
			
			if( m_fb2.getFictionBookNode() != null ) {
				XmlDocument xmlDoc = m_fb2.getXmlDoc();
				// создаем ВСЕ разделя description, заполняем их и потом МЕНЯЕМ с существующими разделами,
				// а если таких разделов нет - то ВСТАВЛЯЕМ созданные
				/* Title Info */
				processTitleInfoNode( ref xmlDoc, makeTitleInfoNode( ref xmlDoc, Enums.TitleInfoEnum.TitleInfo ) );
				/* Source Title Info */
				processSourceTitleInfoNode( ref xmlDoc, makeTitleInfoNode( ref xmlDoc, Enums.TitleInfoEnum.SourceTitleInfo ) );
				/* Document Info */
				processDocumentInfoNode( ref xmlDoc, makeDocumentInfoNode( ref xmlDoc ) );
				/* Publish Info */
				processPublishInfoNode( ref xmlDoc, makePublishInfoNode( ref xmlDoc ) );
				/* Custom Info */
				processCustomInfoNode( ref xmlDoc );
				m_ApplyData = true; // применить изменения к описанию книги
			}
			Close();
		}
		
		#endregion
	}
}
