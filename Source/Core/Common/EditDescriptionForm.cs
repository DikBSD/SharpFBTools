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
using System.Text;
using System.IO;

using Core.FB2.FB2Parsers;
using Core.FB2.Description.TitleInfo;
using Core.FB2.Description.DocumentInfo;
using Core.FB2.Description.PublishInfo;
using Core.FB2.Description.CustomInfo;
using Core.FB2.Description.Common;
using Core.FB2.Binary;
using Core.Misc;

using StringProcessing = Core.Misc.StringProcessing;
using ImageWorker = Core.Misc.ImageWorker;
using MiscListView = Core.Misc.MiscListView;

namespace Core.Common
{
	/// <summary>
	/// EditDescriptionForm: Форма для правки метаданных описания fb2 книг
	/// </summary>
	public partial class EditDescriptionForm : Form
	{
		#region Вспомогательные классы
		#region AuthorInfo
		/// <summary>
		/// AuthorInfo: Класс хранения данных редактируемого Автора/Переводчика, для передачи в форму редактирования EditDescriptionForm
		/// </summary>
		public class AuthorInfo {
			#region Закрытые данные класса
			private readonly bool m_IsCreate = true;
			private readonly Core.Misc.Enums.AuthorEnum m_AuthorEnumType;
			private string m_LastName = string.Empty;
			private string m_FirstName = string.Empty;
			private string m_MiddleName = string.Empty;
			private string m_NickName = string.Empty;
			private string m_HomePage = string.Empty;
			private string m_Email = string.Empty;
			private string m_ID = string.Empty;
			#endregion
			public AuthorInfo( Core.Misc.Enums.AuthorEnum AuthorEnumType, bool IsCreate,
			                  string LastName = "", string FirstName = "", string MiddleName = "",
			                  string NickName = "", string HomePage = "", string Email = "", string ID = "" ) {
				m_IsCreate = IsCreate;
				m_AuthorEnumType = AuthorEnumType;
				
				m_LastName = LastName;
				m_FirstName = FirstName;
				m_MiddleName = MiddleName;
				m_NickName = NickName;
				m_HomePage = HomePage;
				m_Email = Email;
				m_ID = ID;
			}
			
			#region Открытые свойства
			public virtual bool IsCreate {
				get {
					return m_IsCreate;
				}
			}
			public virtual Core.Misc.Enums.AuthorEnum AuthorType {
				get {
					return m_AuthorEnumType;
				}
			}
			public virtual string LastName {
				get {
					return m_LastName;
				}
				set {
					m_LastName = value;
				}
			}
			public virtual string FirstName {
				get {
					return m_FirstName;
				}
				set {
					m_FirstName = value;
				}
			}
			public virtual string MiddleName {
				get {
					return m_MiddleName;
				}
				set {
					m_MiddleName = value;
				}
			}
			public virtual string NickName {
				get {
					return m_NickName;
				}
				set {
					m_NickName = value;
				}
			}
			public virtual string HomePage {
				get {
					return m_HomePage;
				}
				set {
					m_HomePage = value;
				}
			}
			public virtual string Email {
				get {
					return m_Email;
				}
				set {
					m_Email = value;
				}
			}
			public virtual string ID {
				get {
					return m_ID;
				}
				set {
					m_ID = value;
				}
			}
			#endregion
		}
		#endregion
		
		#region SequenceInfo
		/// <summary>
		/// SequenceInfo: Класс хранения данных редактируемой Серии, для передачи в форму редактирования EditDescriptionForm
		/// </summary>
		public class SequenceInfo {
			#region Закрытые данные класса
			private readonly bool m_IsCreate = true;
			private readonly Core.Misc.Enums.SequenceEnum m_SequenceEnumType;
			private string m_Name = string.Empty;
			private string m_Number = string.Empty;
			#endregion
			public SequenceInfo( Core.Misc.Enums.SequenceEnum SequenceEnumType, bool IsCreate,
			                    string Name = "", string Number = "" ) {
				m_IsCreate = IsCreate;
				m_SequenceEnumType = SequenceEnumType;
				m_Name = Name;
				m_Number = Number;
			}
			
			#region Открытые свойства
			public virtual bool IsCreate {
				get {
					return m_IsCreate;
				}
			}
			public virtual Core.Misc.Enums.SequenceEnum SequenceType {
				get {
					return m_SequenceEnumType;
				}
			}
			public virtual string Name {
				get {
					return m_Name;
				}
				set {
					m_Name = value;
				}
			}
			public virtual string Number {
				get {
					return m_Number;
				}
				set {
					m_Number = value;
				}
			}
			#endregion
		}
		#endregion
		
		#region CustomInfoInfo
		/// <summary>
		/// CustomInfoInfo: Класс хранения данных редактируемой Дополнительной информации, для передачи в форму редактирования EditDescriptionForm
		/// </summary>
		public class CustomInfoInfo {
			#region Закрытые данные класса
			private readonly bool m_IsCreate = true;
			private string m_Type = string.Empty;
			private string m_Value = string.Empty;
			#endregion
			public CustomInfoInfo( bool IsCreate, string Type = "", string Value = "" ) {
				m_IsCreate = IsCreate;
				m_Type = Type;
				m_Value = Value;
			}
			
			#region Открытые свойства
			public virtual bool IsCreate {
				get {
					return m_IsCreate;
				}
			}
			public virtual string Type {
				get {
					return m_Type;
				}
				set {
					m_Type = value;
				}
			}
			public virtual string Value {
				get {
					return m_Value;
				}
				set {
					m_Value = value;
				}
			}
			#endregion
		}
		#endregion
		
		#endregion
		
		#region Закрытые данные класса
		private readonly string m_FilePath = string.Empty;
		private readonly FictionBook m_fb2 = null;
		private bool m_ApplyData = false;
		private const string m_sTitle = "Правка метаданных описания fb2 книги";
		#endregion
		
		public EditDescriptionForm( string FilePath )
		{
			#region Код Конструктора
			InitializeComponent();
			m_FilePath = FilePath;
			m_fb2 = new FictionBook( FilePath );
			// первоначальное заполнение контролов
			init();
			// загрузка метаданных TitleInfo книги
			loadFB2TitleInfo( ref m_fb2, Core.Misc.Enums.TitleInfoEnum.TitleInfo );
			// загрузка метаданных SourceTitleInfo книги
			loadFB2TitleInfo( ref m_fb2, Core.Misc.Enums.TitleInfoEnum.SourceTitleInfo );
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
					Core.Misc.StringProcessing.getDeleteAllTags( his.Value != null ? his.Value : string.Empty );
			// загрузка Аннатации на книгу
			Annotation ann = m_fb2.TIAnnotation;
			if( ann != null )
				TIAnnotationRichTextEdit.Text =
					Core.Misc.StringProcessing.getDeleteAllTags( ann.Value != null ? ann.Value : string.Empty );
			// загрузка Аннатации на Оригинал книги
			Annotation annOrig = m_fb2.STIAnnotation;
			if( annOrig != null )
				STIAnnotationRichTextEdit.Text =
					Core.Misc.StringProcessing.getDeleteAllTags( annOrig.Value != null ? annOrig.Value : string.Empty );
			
			// загрузка обложек книги
			IList<BinaryBase64> Covers = m_fb2.getCoversBase64( Core.Misc.Enums.TitleInfoEnum.TitleInfo );
			ImageWorker.makeListViewCoverNameItems( TICoverListView, ref Covers );
			
			// загрузка обложек оригинала книги
			Covers = m_fb2.getCoversBase64( Core.Misc.Enums.TitleInfoEnum.SourceTitleInfo );
			ImageWorker.makeListViewCoverNameItems( STICoverListView, ref Covers );
			#endregion
		}
		
		#region Открытые методы класса
		public bool isApplyData() {
			return m_ApplyData;
		}
		public XmlDocument getFB2XmlDocument() {
			return m_fb2.getXmlDoc();
		}
		#endregion
		
		#region Закрытые вспомогательные методы
		// первоначальное заполнение контролов
		private void init() {
			#region Код
			// создание списков языков
			TILangComboBox.Items.AddRange( Core.Common.LangList.LangsList );
			TISrcLangComboBox.Items.AddRange( Core.Common.LangList.LangsList );
			STILangComboBox.Items.AddRange( Core.Common.LangList.LangsList );
			STISrcLangComboBox.Items.AddRange( Core.Common.LangList.LangsList );
			// формирование Списка Жанров
			makeListGenres( Core.Misc.Enums.TitleInfoEnum.TitleInfo );
			makeListGenres( Core.Misc.Enums.TitleInfoEnum.SourceTitleInfo );
			#endregion
		}
		
		#region Вспомогательные методы для ЗАГРУЗКИ метаданных в контролы
		// формирование Списка Жанров в контролы
		private void makeListGenres( Core.Misc.Enums.TitleInfoEnum TitleInfoType ) {
			#region Код
			ComboBox cBox = TitleInfoType == Core.Misc.Enums.TitleInfoEnum.TitleInfo
				? TIGenresComboBox : STIGenresComboBox;
			cBox.Items.Clear();
			RadioButton rb = TitleInfoType == Core.Misc.Enums.TitleInfoEnum.TitleInfo
				? rbtnTIFB2Librusec : rbtnSTIFB2Librusec;
			
			Core.FileManager.SortingFB2Tags sortTags = new Core.FileManager.SortingFB2Tags( null );
			Core.FB2.Genres.IFBGenres fb2g = null;
			if( rb.Checked )
				fb2g = new Core.FB2.Genres.FB2LibrusecGenres( ref sortTags );
			else
				fb2g = new Core.FB2.Genres.FB22Genres( ref sortTags );

			string[] sGenresNames	= fb2g.GetFBGenreNamesArray();
			string[] sCodes			= fb2g.GetFBGenreCodesArray();
			
			for( int i = 0; i != sGenresNames.Length; ++i )
				cBox.Items.Add( sGenresNames[i] + " (" + sCodes[i] + ")" );
			cBox.SelectedIndex = 0;
			#endregion
		}
		// загрузка Названия книги в зависимости от типа TitleInfo
		private void loadBookTitle( ref FictionBook fb2, Core.Misc.Enums.TitleInfoEnum TitleInfoType ) {
			#region Код
			BookTitle bookTitle = TitleInfoType == Core.Misc.Enums.TitleInfoEnum.TitleInfo
				? fb2.TIBookTitle : fb2.STIBookTitle;
			if( bookTitle != null ) {
				if( TitleInfoType == Core.Misc.Enums.TitleInfoEnum.TitleInfo )
					TIBookTitleTextBox.Text = bookTitle.Value;
				else
					STIBookTitleTextBox.Text = bookTitle.Value;
			}
			#endregion
		}
		// загрузка Языков книги в зависимости от типа TitleInfo
		private void loadFB2Langs( ref FictionBook fb2, Core.Misc.Enums.TitleInfoEnum TitleInfoType ) {
			#region Код
			ComboBox cbLang = TitleInfoType == Core.Misc.Enums.TitleInfoEnum.TitleInfo ? TILangComboBox : STILangComboBox;
			ComboBox cbSrcLang = TitleInfoType == Core.Misc.Enums.TitleInfoEnum.TitleInfo ? TISrcLangComboBox : STISrcLangComboBox;
			string Lang = TitleInfoType == Core.Misc.Enums.TitleInfoEnum.TitleInfo
				? "(" + fb2.TILang + ")" : "(" + fb2.STILang + ")";
			string SrcLang = TitleInfoType == Core.Misc.Enums.TitleInfoEnum.TitleInfo
				? "(" + fb2.TISrcLang + ")" : "(" + fb2.STISrcLang + ")";
			if( !string.IsNullOrEmpty(Lang) ) {
				for( int i = 0; i != cbLang.Items.Count; ++i ) {
					string s = cbLang.Items[i].ToString();
					if( s.IndexOf( Lang ) > -1 ) {
						cbLang.SelectedIndex = i;
						break;
					}
				}
			}
			if( !string.IsNullOrEmpty(SrcLang) ) {
				for( int i = 0; i != cbSrcLang.Items.Count; ++i ) {
					string s = cbSrcLang.Items[i].ToString();
					if( s.IndexOf( SrcLang ) > -1 ) {
						cbSrcLang.SelectedIndex = i;
						break;
					}
				}
			}
			#endregion
		}
		// загрузка Жанров взависимости от типа TitleInfo
		private void loadGenres( ref FictionBook fb2, Core.Misc.Enums.TitleInfoEnum TitleInfoType ) {
			#region Код
			RadioButton rbFB2Librusec = TitleInfoType == Core.Misc.Enums.TitleInfoEnum.TitleInfo
				? rbtnTIFB2Librusec : rbtnSTIFB2Librusec;
			ListView lv = TitleInfoType == Core.Misc.Enums.TitleInfoEnum.TitleInfo
				? TIGenresListView : STIGenresListView;
			
			Core.FileManager.SortingFB2Tags sortTags = new Core.FileManager.SortingFB2Tags( null );
			Core.FB2.Genres.IFBGenres fb2g = null;
			if( rbFB2Librusec.Checked )
				fb2g = new Core.FB2.Genres.FB2LibrusecGenres( ref sortTags );
			else
				fb2g = new Core.FB2.Genres.FB22Genres( ref sortTags );
			
			IList<Genre> Genres = TitleInfoType == Core.Misc.Enums.TitleInfoEnum.TitleInfo
				? fb2.TIGenres : fb2.STIGenres;
			
			if( Genres != null ) {
				foreach( Genre g in Genres ) {
					ListViewItem lvi = new ListViewItem( fb2g.GetFBGenreName(g.Name) + " (" + g.Name + ")");
					lvi.SubItems.Add( g.Math.ToString() );
					lv.Items.Add( lvi );
				}
			}
			#endregion
		}
		// загрузка Даты создания книги в зависимости от типа TitleInfo
		private void loadDate( ref FictionBook fb2, Core.Misc.Enums.TitleInfoEnum TitleInfoType ) {
			#region Код
			Date date = TitleInfoType == Core.Misc.Enums.TitleInfoEnum.TitleInfo ? fb2.TIDate : fb2.STIDate;
			if( date != null ) {
				TextBox mtbDate = TitleInfoType == Core.Misc.Enums.TitleInfoEnum.TitleInfo
					? TIDateTextBox : STIDateTextBox;
				MaskedTextBox mtbValue = TitleInfoType == Core.Misc.Enums.TitleInfoEnum.TitleInfo
					? TIDateValueMaskedTextBox : STIDateValueMaskedTextBox;
				mtbDate.Text = date.Text;
				mtbValue.Text = date.Value;
			}
			#endregion
		}
		// загрузка метаданных автора/переводчика книги / создателя fb2 файла
		private void loadAuthors( ref FictionBook fb2, Core.Misc.Enums.TitleInfoEnum TitleInfoType,
		                         Core.Misc.Enums.AuthorEnum AuthorType ) {
			#region Код
			IList<Author> Authors = null;
			ListView lv = null;
			if( TitleInfoType == Core.Misc.Enums.TitleInfoEnum.TitleInfo ) {
				if( AuthorType == Core.Misc.Enums.AuthorEnum.AuthorOfBook ) {
					Authors = fb2.TIAuthors;
					lv = TIAuthorsListView;
				} else if( AuthorType == Core.Misc.Enums.AuthorEnum.Translator ) {
					Authors = fb2.TITranslators;
					lv = TITranslatorListView;
				} else {
					//AuthorType == Core.Misc.Enums.AuthorEnum.AuthorOfFB2
					Authors = fb2.DIAuthors;
					lv = DIFB2AuthorListView;
				}
			} else {
				// TitleInfoType == Core.Misc.Enums.TitleInfoEnum.SourceTitleInfo
				if( AuthorType == Core.Misc.Enums.AuthorEnum.AuthorOfBook ) {
					Authors = fb2.STIAuthors;
					lv = STIAuthorsListView;
				} else if( AuthorType == Core.Misc.Enums.AuthorEnum.Translator ) {
					Authors = fb2.STITranslators;
					lv = STITranslatorListView;
				} else {
					//AuthorType == Core.Misc.Enums.AuthorEnum.AuthorOfFB2
					Authors = fb2.DIAuthors;
					lv = DIFB2AuthorListView;
				}
			}
			
			if( Authors != null ) {
				foreach( Author a in Authors ) {
					if( a != null ) {
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
						
						IList<string> HPs = a.HomePages;
						string sHomePages = string.Empty;
						if( HPs != null && HPs.Count > 0 ) {
							foreach( string hp in HPs )
								sHomePages += hp + "; ";
						}
						lvi.SubItems.Add( sHomePages );
						
						IList<string> Emails = a.Emails;
						string sEmails = string.Empty;
						if( Emails != null && Emails.Count > 0 ) {
							foreach( string e in Emails )
								sEmails += e + "; ";
						}
						lvi.SubItems.Add( sEmails );
						
						lvi.SubItems.Add( a.ID != null ? a.ID : string.Empty );
						
						lv.Items.Add( lvi );
					}
				}
			}
			#endregion
		}
		// загрузка метаданных Серий книги / Серий Бумажной книги
		private void loadSequences( ref FictionBook fb2, Core.Misc.Enums.TitleInfoEnum TitleInfoType,
		                           Core.Misc.Enums.SequenceEnum SequenceType ) {
			#region Код
			IList<Sequence> Sequences = null;
			ListView lv = null;
			if( SequenceType == Core.Misc.Enums.SequenceEnum.Ebook ) {
				Sequences = TitleInfoType == Core.Misc.Enums.TitleInfoEnum.TitleInfo
					? fb2.TISequences : fb2.STISequences;
				lv = TitleInfoType == Core.Misc.Enums.TitleInfoEnum.TitleInfo
					? TISequenceListView : STISequenceListView;
			} else {
				// SequenceType == Core.Misc.Enums.SequenceEnum.PaperBook
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
			#endregion
		}
		// загрузка ключевых слов
		private void loadKeyWords( ref FictionBook fb2, Core.Misc.Enums.TitleInfoEnum TitleInfoType ) {
			#region Код
			TextBox tbKey = TitleInfoType == Core.Misc.Enums.TitleInfoEnum.TitleInfo ? TIKeyTextBox : STIKeyTextBox;
			Keywords keys = TitleInfoType == Core.Misc.Enums.TitleInfoEnum.TitleInfo ? fb2.TIKeywords : fb2.STIKeywords;
			if( keys != null )
				tbKey.Text = keys.Value;
			#endregion
		}
		// загрузка метаданных TitleInfo книги
		private void loadFB2TitleInfo( ref FictionBook fb2, Core.Misc.Enums.TitleInfoEnum TitleInfoType ) {
			#region Код
			// book-info
			loadBookTitle( ref fb2, TitleInfoType );
			// языки
			loadFB2Langs( ref fb2, TitleInfoType );
			// genres
			loadGenres( ref fb2, TitleInfoType );
			// date
			loadDate( ref fb2, TitleInfoType );
			// Авторы книги
			loadAuthors( ref fb2, TitleInfoType, Core.Misc.Enums.AuthorEnum.AuthorOfBook );
			// Переводчики книги
			loadAuthors( ref fb2, TitleInfoType, Core.Misc.Enums.AuthorEnum.Translator );
			// ключевые слова
			loadKeyWords( ref fb2, TitleInfoType );
			// Серии
			loadSequences( ref fb2, TitleInfoType, Core.Misc.Enums.SequenceEnum.Ebook );
			#endregion
		}
		// загрузка метаданных DocumentInfo
		private void loadDocumentInfo( ref FictionBook fb2 ) {
			#region Код
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
			loadAuthors( ref fb2, Core.Misc.Enums.TitleInfoEnum.TitleInfo, Core.Misc.Enums.AuthorEnum.AuthorOfFB2 );
			#endregion
		}
		// загрузка метаданных PublisherInfo
		private void loadPublisherInfo( ref FictionBook fb2 ) {
			#region Код
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
			if( city != null )
				DIISBNTextBox.Text = !string.IsNullOrEmpty( isbn.Value ) ? isbn.Value : string.Empty;
			// Sequence
			loadSequences( ref fb2, Core.Misc.Enums.TitleInfoEnum.TitleInfo, Core.Misc.Enums.SequenceEnum.PaperBook );
			
			
			#endregion
		}
		// загрузка метаданных CustomInfo
		private void loadCustomInfo( ref FictionBook fb2 ) {
			#region Код
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
			#endregion
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
		// удаление Imsge тэга Cover по Href
		private void deleteCoverImageForHref( Core.Misc.Enums.TitleInfoEnum TitleInfoType, string Href ) {
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
		private void deleteCoverForHref( Core.Misc.Enums.TitleInfoEnum TitleInfoType, string Href ) {
			deleteCoverImageForHref( TitleInfoType, Href );
			if( ! isImageExsistInBody( Href ) )
				deleteCoverBinaryForID( Href );
		}
		
		// удаление тэга cover - т.е. удаление всех Обложек
		private void deleteCoverTag( Core.Misc.Enums.TitleInfoEnum TitleInfoType ) {
			XmlNode xmlCover = m_fb2.getCoverNode( TitleInfoType );
			if( xmlCover != null )
				m_fb2.getTitleInfoNode( TitleInfoType ).RemoveChild( xmlCover );
		}
		// удаление всех binary тэга cover - т.е. удаление всех Обложек
		private void deleteAllBinaryTagsOfCover( ref IList<string> lIDList ) {
			#region Код
			XmlNode xmlFB = m_fb2.getFictionBookNode();
			XmlNodeList xmlBinaryNodes = m_fb2.getBinaryNodes();
			if( xmlBinaryNodes != null && xmlBinaryNodes.Count > 0 ) {
				foreach( string ID in lIDList ) {
					if( ! isImageExsistInBody( ID ) )
						deleteCoverBinaryForID( ID );
				}
			}
			#endregion
		}
		// удаление всех Обложек (cover и binary)
		private void deleteAllCover( Core.Misc.Enums.TitleInfoEnum TitleInfoType, ref IList<string> lIDList ) {
			deleteCoverTag( TitleInfoType );
			deleteAllBinaryTagsOfCover( ref lIDList );
		}
		#endregion
		
		#region Вспомогательные методы СОЗДАНИЯ структур метаданных
		private IList<XmlNode> makeAuthorNode( Core.Misc.Enums.AuthorEnum AuthorType, ListView lv ) {
			#region Код
			IList<XmlNode> Authors = null;
			XmlNode xmlAuthor = null;
			if( lv.Items.Count > 0 ) {
				Authors = new List<XmlNode>( lv.Items.Count );
				foreach( ListViewItem item in lv.Items ) {
					string HPs = StringProcessing.trimLastTemplateSymbol( item.SubItems[4].Text.Trim(), new Char [] { ',',';' } );
					IList<string> lHPs = HPs.Split( new Char [] { ',',';' } );
					string Emails = StringProcessing.trimLastTemplateSymbol( item.SubItems[5].Text.Trim(), new Char [] { ',',';' } );
					IList<string> lEmails = Emails.Split( new Char [] { ',',';' } );
					xmlAuthor = m_fb2.makeAuthor(
						AuthorType,
						item.SubItems[1].Text, item.SubItems[2].Text, item.Text, item.SubItems[3].Text,
						lHPs, lEmails, item.SubItems[6].Text
					);
					Authors.Add(xmlAuthor);
				}
			} else {
				if( AuthorType == Core.Misc.Enums.AuthorEnum.AuthorOfBook ) {
					Authors = new List<XmlNode>();
					xmlAuthor = m_fb2.makeAuthor( AuthorType, null, null, null, null, null, null, null );
					Authors.Add(xmlAuthor);
				}
			}
			return Authors;
			#endregion
		}
		private XmlNode makeTitleInfoNode( ref XmlDocument xmlDoc, Core.Misc.Enums.TitleInfoEnum TitleInfoType ) {
			#region Код
			if( TitleInfoType == Core.Misc.Enums.TitleInfoEnum.SourceTitleInfo && !STIEnableCheckBox.Checked )
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
			
			if( TitleInfoType == Core.Misc.Enums.TitleInfoEnum.TitleInfo ) {
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
					xmlGenre = m_fb2.makeGenre( code.Substring( 0, code.Length - 1), item.SubItems[1].Text );
					xmlTI.AppendChild( xmlGenre );
				}
			} else {
				xmlGenre = m_fb2.makeGenre( "other", null );
				xmlTI.AppendChild( xmlGenre );
			}
			
			// Авторы
			IList<XmlNode> Authors = makeAuthorNode( Core.Misc.Enums.AuthorEnum.AuthorOfBook, AuthorsListView );
			foreach( XmlNode Author in Authors )
				xmlTI.AppendChild( Author );
			
			// Book Title
			xmlTI.AppendChild( m_fb2.makeBookTitle( BookTitleTextBox.Text.Trim() ) );
			
			// Аннотация
			XmlElement Annot = m_fb2.makeAnnotation( AnnotationRichTextEdit.Lines );
			if( Annot != null )
				if( !string.IsNullOrEmpty( Annot.InnerText.Trim() ) )
					xmlTI.AppendChild( Annot );
			
			// keywords
			if( !string.IsNullOrEmpty( KeyTextBox.Text.Trim() ) )
				xmlTI.AppendChild( m_fb2.makeKeywords( KeyTextBox.Text.Trim() ) );
			
			// date
			string DateValue = DateValueMaskedTextBox.Text.Trim();
			if( !string.IsNullOrEmpty( DateTextBox.Text.Trim() ) ) {
				if( !DateValue.Equals( "-  -" ) )
					xmlTI.AppendChild( m_fb2.makeDate( DateTextBox.Text.Trim(), DateValue ) );
				else
					xmlTI.AppendChild( m_fb2.makeDate( DateTextBox.Text.Trim(), null ) );
			} else {
				if( !DateValue.Equals( "-  -" ) )
					xmlTI.AppendChild( m_fb2.makeDate( null, DateValue ) );
				else
					xmlTI.AppendChild( m_fb2.makeDate( null, null ) );
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
					m_fb2.makeCoverpage( list )
				);
				
				// binary
				XmlNode xmlFB = m_fb2.getFictionBookNode();
				foreach( ListViewItem item in CoverListView.Items ) {
					// добавление всех binary Обложек
					if( m_fb2.getBinaryNodeForID( item.Text ) == null )
						xmlFB.AppendChild(
							m_fb2.makeBinary(
								item.Text, item.SubItems[1].Text, item.Tag.ToString()
							)
						);
				}
			}
			
			// lang
			if( !string.IsNullOrEmpty( LangComboBox.Text ) )
				xmlTI.AppendChild(
					m_fb2.makeLang( LangComboBox.Text.Substring( LangComboBox.Text.IndexOf('(')+1, 2 ) )
				);
			else
				xmlTI.AppendChild( m_fb2.makeLang() );
			
			// src-lang
			if( !string.IsNullOrEmpty( SrcLangComboBox.Text ) ) {
				xmlTI.AppendChild(
					m_fb2.makeSrcLang( SrcLangComboBox.Text.Substring( SrcLangComboBox.Text.IndexOf('(')+1, 2 ) )
				);
			}
			
			// translator
			IList<XmlNode> Translators = makeAuthorNode( Core.Misc.Enums.AuthorEnum.Translator, TranslatorListView ) ;
			if( Translators != null ) {
				foreach( XmlNode Translator in Translators )
					xmlTI.AppendChild( Translator );
			}
			
			// sequence
			if( SequenceListView.Items.Count > 0 ) {
				foreach( ListViewItem item in SequenceListView.Items )
					xmlTI.AppendChild( m_fb2.makeSequence( item.Text, item.SubItems[1].Text ) );
			}
			return xmlTI;
			#endregion
		}
		private bool processTitleInfoNode( ref XmlDocument xmlDoc, XmlNode xmlTINew ) {
			#region Код
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
			#endregion
		}
		private bool processSourceTitleInfoNode( ref XmlDocument xmlDoc, XmlNode xmlSTINew ) {
			#region Код
			string ns = m_fb2.getNamespace();
			XmlNode xmlSTIOld = xmlDoc.SelectSingleNode( ns + "FictionBook" + ns + "description" + ns + "src-title-info", m_fb2.getNamespaceManager() );
			XmlNode xmlDesc = m_fb2.getDescriptionNode();
			if( xmlDesc != null ) {
				if( STIEnableCheckBox.Checked ) {
					if( xmlSTIOld != null ) {
						xmlDesc.ReplaceChild( xmlSTINew, xmlSTIOld );
						return true;
					} else {
						XmlNode xmlTI = m_fb2.getTitleInfoNode( Core.Misc.Enums.TitleInfoEnum.TitleInfo );
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
			#endregion
		}
		private XmlNode makeDocumentInfoNode( ref XmlDocument xmlDoc ) {
			#region Код
			XmlNode xmlDI = xmlDoc.CreateElement( m_fb2.getPrefix(), "document-info", m_fb2.getNamespaceURI() );
			
			// Авторы
			IList<XmlNode> Authors = makeAuthorNode( Core.Misc.Enums.AuthorEnum.AuthorOfFB2, DIFB2AuthorListView ) ;
			foreach( XmlNode Author in Authors )
				xmlDI.AppendChild( Author );
			
			// Program Used
			XmlElement xmlProgramUsed = m_fb2.makeProgramUsed( DIProgramUsedTextBox.Text.Trim() );
			if( xmlProgramUsed != null )
				xmlDI.AppendChild( xmlProgramUsed );
			
			// date
			string DateValue = DIDateValueMaskedTextBox.Text.Trim();
			if( !string.IsNullOrWhiteSpace( DIDateTextBox.Text ) ) {
				if( !DateValue.Equals( "-  -" ) )
					xmlDI.AppendChild( m_fb2.makeDate( DIDateTextBox.Text.Trim(), DateValue ) );
				else
					xmlDI.AppendChild( m_fb2.makeDate( DIDateTextBox.Text.Trim(), null ) );
			} else {
				if( !DateValue.Equals( "-  -" ) )
					xmlDI.AppendChild( m_fb2.makeDate( null, DateValue ) );
				else
					xmlDI.AppendChild( m_fb2.makeDate( null, null ) );
			}
			
			// src-url
			if( !string.IsNullOrWhiteSpace( DIURLTextBox.Text ) ) {
				string [] URLs = DIURLTextBox.Text.Split( new Char [] { ',',';' } );
				IList<XmlNode> lSrcUrls = m_fb2.makeSrcUrl( ref URLs );
				if( lSrcUrls != null && lSrcUrls.Count > 0 ) {
					foreach( XmlNode URL in lSrcUrls )
						xmlDI.AppendChild( URL );
				}
			}
			
			// src-ocr
			XmlElement xmlSrcOcr = m_fb2.makeSrcOcr( DIOCRTextBox.Text.Trim() );
			if( xmlSrcOcr != null )
				xmlDI.AppendChild( xmlSrcOcr );
			
			// id
			xmlDI.AppendChild( m_fb2.makeID( DIIDTextBox.Text.Trim() ) );
			
			// version
			xmlDI.AppendChild( m_fb2.makeVersion( DIVersionTextBox.Text.Trim() ) );
			
			// history
			XmlElement History = m_fb2.makeHistory( DIHistoryRichTextEdit.Lines );
			if( History != null )
				if( !string.IsNullOrWhiteSpace( History.InnerText ) )
					xmlDI.AppendChild( History );
			return xmlDI;
			#endregion
		}
		private bool processDocumentInfoNode( ref XmlDocument xmlDoc, XmlNode xmlDINew ) {
			#region Код
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
			#endregion
		}
		private XmlNode makePublishInfoNode( ref XmlDocument xmlDoc ) {
			#region Код
			bool PIExists = false;
			XmlNode xmlPI = xmlDoc.CreateElement( m_fb2.getPrefix(), "publish-info", m_fb2.getNamespaceURI() );
			
			// Book Name
			XmlNode bn = m_fb2.makePaperBookName( DIBookNameTextBox.Text.Trim() );
			if( bn != null ) {
				xmlPI.AppendChild( bn );
				PIExists = true;
			}
			
			// publisher
			XmlNode pub = m_fb2.makePaperPublisher( DIPublisherTextBox.Text.Trim() );
			if( pub != null ) {
				xmlPI.AppendChild( pub );
				PIExists = true;
			}
			
			// city
			XmlNode city = m_fb2.makePaperCity( DICityTextBox.Text.Trim() );
			if( city != null ) {
				xmlPI.AppendChild( city);
				PIExists = true;
			}
			
			// year
			XmlNode year = m_fb2.makePaperYear( DIYearTextBox.Text.Trim() );
			if( year != null ) {
				xmlPI.AppendChild( year );
				PIExists = true;
			}
			
			// isbn
			XmlNode isbn = m_fb2.makePaperISBN( DIISBNTextBox.Text.Trim() );
			if( isbn != null ) {
				xmlPI.AppendChild( isbn );
				PIExists = true;
			}
			
			// sequence
			XmlNode xmlSequence = null;
			if( PISequenceListView.Items.Count > 0 ) {
				foreach( ListViewItem item in PISequenceListView.Items ) {
					xmlSequence = m_fb2.makeSequence( item.Text, item.SubItems[1].Text );
					xmlPI.AppendChild( xmlSequence );
				}
				PIExists = true;
			}
			
			return PIExists ? xmlPI : null;
			#endregion
		}
		private bool processPublishInfoNode( ref XmlDocument xmlDoc, XmlNode xmlPINew ) {
			#region Код
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
			#endregion
		}
		private bool processCustomInfoNode( ref XmlDocument xmlDoc ) {
			#region Код
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
						xmlDesc.AppendChild( m_fb2.makeCustomInfo( item.Text, item.SubItems[1].Text ) );
					return true;
				}
			}
			return false;
			#endregion
		}
		#endregion
		
		#region Вспомогательные методы ДЛЯ РАБОТЫ КОНТРОЛОВ
		// создание нового Автора книги / Переводчика книги / Создателя fb2-файла
		private void createNewAuthor( ListView lv, Core.Misc.Enums.AuthorEnum AuthorType ) {
			#region Код
			AuthorInfo ai = new AuthorInfo( AuthorType, true );
			Core.Common.AuthorInfoForm authorInfoForm = new Core.Common.AuthorInfoForm( ref ai );
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
			#endregion
		}
		// редактирование Автора книги / Переводчика книги / Создателя fb2-файла
		private void editSelectedAuthor( ListView lv, Core.Misc.Enums.AuthorEnum AuthorType ) {
			#region Код
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
			#endregion
		}
		// создание новой Серии для Электронной / Бумажной книги
		private void createNewSequence( ListView lv, Core.Misc.Enums.SequenceEnum SequenceType ) {
			#region Код
			SequenceInfo si = new SequenceInfo( SequenceType, true );
			Core.Common.SequenceInfoForm sequenceInfoForm = new Core.Common.SequenceInfoForm( ref si );
			sequenceInfoForm.ShowDialog();
			SequenceInfo NewSequenceInfo = sequenceInfoForm.SequenceInfo;
			ListViewItem lvi = new ListViewItem( NewSequenceInfo.Name );
			lvi.SubItems.Add( NewSequenceInfo.Number );
			lv.Items.Add( lvi );
			sequenceInfoForm.Dispose();
			#endregion
		}
		// редактирование Серии для Электронной / Бумажной книги
		private void editSelectedSequence( ListView lv, Core.Misc.Enums.SequenceEnum SequenceType ) {
			#region Код
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
			#endregion
		}
		// поиск максимально номера обложки
		private int getMaxCoverNumber( ListView lv ) {
			#region Код
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
			#endregion
		}
		// добавление обложек в спиок
		private void addCoverToList( Core.Misc.Enums.TitleInfoEnum TitleInfoType )
		{
			#region Код
			ListView CoverListView = TitleInfoType == Core.Misc.Enums.TitleInfoEnum.TitleInfo
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
					string CoverName = "cover" + StringProcessing.makeIINumber( i+1 ) + Path.GetExtension( FilePath );
					if( MiscListView.isExistListViewItem( CoverListView, CoverName ) ) {
						// поиск максимально номера обложки
						int MaxNumber = getMaxCoverNumber( CoverListView );
						CoverName = "cover" + StringProcessing.makeIINumber( ++MaxNumber ) + Path.GetExtension( FilePath );
					}
					ListViewItem lvi = new ListViewItem( CoverName );
					lvi.SubItems.Add( ImageWorker.getContentType( FilePath ) );
					lvi.Tag = ImageWorker.toBase64( FilePath );
					CoverListView.Items.Add( lvi );
				}
			}
			#endregion
		}
		#endregion
		#endregion
		
		#region Обработчики событий
		/* Source Title Info */
		// Жанры
		void RbtnTIFB2LibrusecClick(object sender, EventArgs e)
		{
			makeListGenres( Core.Misc.Enums.TitleInfoEnum.TitleInfo );
		}
		void RbtnTIFB22Click(object sender, EventArgs e)
		{
			makeListGenres( Core.Misc.Enums.TitleInfoEnum.TitleInfo );
		}
		void TIGenreAddButtonClick(object sender, EventArgs e)
		{
			if( !MiscListView.isExistListViewItem( TIGenresListView, TIGenresComboBox.Text ) ) {
				ListViewItem lvi = new ListViewItem( TIGenresComboBox.Text );
				lvi.SubItems.Add( TIMatchMaskedTextBox.Text );
				TIGenresListView.Items.Add( lvi );
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
		// Авторы Книги
		void TIAuthorAddButtonClick(object sender, EventArgs e)
		{
			// создание нового Автора книги
			createNewAuthor( TIAuthorsListView, Core.Misc.Enums.AuthorEnum.AuthorOfBook );
		}
		void TIAuthorEditButtonClick(object sender, EventArgs e)
		{
			// редактирование Автора книги
			editSelectedAuthor( TIAuthorsListView, Core.Misc.Enums.AuthorEnum.AuthorOfBook );
		}
		void TIAuthorDeleteButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteSelectedItem( TIAuthorsListView, m_sTitle, "Авторов Книги" );
		}
		void TIAuthorDeleteAllButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteAllItems( TIAuthorsListView, m_sTitle, "Авторов Книги" );
		}
		// Переводчики
		void TITranslatorAddButtonClick(object sender, EventArgs e)
		{
			// создание нового Переводчика книги
			createNewAuthor( TITranslatorListView, Core.Misc.Enums.AuthorEnum.Translator );
		}
		void TITranslatorEditButtonClick(object sender, EventArgs e)
		{
			// редактирование Переводчика книги
			editSelectedAuthor( TITranslatorListView, Core.Misc.Enums.AuthorEnum.Translator );
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
			createNewSequence( TISequenceListView, Core.Misc.Enums.SequenceEnum.Ebook );
		}
		void TISequenceEditButtonClick(object sender, EventArgs e)
		{
			// редактирование Серии для Электронной книги
			editSelectedSequence( TISequenceListView, Core.Misc.Enums.SequenceEnum.Ebook );
		}
		void TISequenceDeleteButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteSelectedItem( TISequenceListView, m_sTitle, "Серий Книги" );
		}
		void TISequenceDeleteAllButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteAllItems( TISequenceListView, m_sTitle, "Серий Книги" );
		}
		
		/* Source Title Info */
		void STIEnableCheckBoxClick(object sender, EventArgs e)
		{
			STITabControl.Enabled = STIEnableCheckBox.Checked;
		}
		// Жанры
		void RbtnSTIFB2LibrusecClick(object sender, EventArgs e)
		{
			makeListGenres( Core.Misc.Enums.TitleInfoEnum.SourceTitleInfo );
		}
		void RbtnSTIFB22Click(object sender, EventArgs e)
		{
			makeListGenres( Core.Misc.Enums.TitleInfoEnum.SourceTitleInfo );
		}
		void STIGenreAddButtonClick(object sender, EventArgs e)
		{
			if( !MiscListView.isExistListViewItem( STIGenresListView, STIGenresComboBox.Text ) ) {
				ListViewItem lvi = new ListViewItem( STIGenresComboBox.Text );
				lvi.SubItems.Add( STIMatchMaskedTextBox.Text );
				STIGenresListView.Items.Add( lvi );
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
		// Авторы книги
		void STIAuthorAddButtonClick(object sender, EventArgs e)
		{
			// создание нового Автора книги
			createNewAuthor( STIAuthorsListView, Core.Misc.Enums.AuthorEnum.AuthorOfBook );
		}
		void STIAuthorEditButtonClick(object sender, EventArgs e)
		{
			// редактирование Автора книги
			editSelectedAuthor( STIAuthorsListView, Core.Misc.Enums.AuthorEnum.AuthorOfBook );
		}
		void STIAuthorDeleteButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteSelectedItem( STIAuthorsListView, m_sTitle, "Авторов Книги" );
		}
		void STIAuthorDeleteAllButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteAllItems( STIAuthorsListView, m_sTitle, "Авторов Книги" );
		}
		// Переводчики
		void STITranslatorAddButtonClick(object sender, EventArgs e)
		{
			// создание нового Переводчика книги
			createNewAuthor( STITranslatorListView, Core.Misc.Enums.AuthorEnum.Translator );
		}
		void STITranslatorEditButtonClick(object sender, EventArgs e)
		{
			// редактирование Переводчика книги
			editSelectedAuthor( STITranslatorListView, Core.Misc.Enums.AuthorEnum.Translator );
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
			createNewSequence( STISequenceListView, Core.Misc.Enums.SequenceEnum.Ebook );
		}
		void STISequenceEditButtonClick(object sender, EventArgs e)
		{
			// редактирование Серии для Электронной книги
			editSelectedSequence( STISequenceListView, Core.Misc.Enums.SequenceEnum.Ebook );
		}
		void STISequenceDeleteButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteSelectedItem( STISequenceListView, m_sTitle, "Серий Книги" );
		}
		void STISequenceDeleteAllButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteAllItems( STISequenceListView, m_sTitle, "Серий Книги" );
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
			createNewAuthor( DIFB2AuthorListView, Core.Misc.Enums.AuthorEnum.AuthorOfFB2 );
		}
		void DIFB2AuthorEditButtonClick(object sender, EventArgs e)
		{
			// редактирование Создателя fb2 файла
			editSelectedAuthor( DIFB2AuthorListView, Core.Misc.Enums.AuthorEnum.AuthorOfFB2 );
		}
		void DIFB2AuthorDeleteButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteSelectedItem( DIFB2AuthorListView, m_sTitle, "Создателей fb2 файла" );
		}
		void DIFB2AuthorDeleteAllButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteAllItems( DIFB2AuthorListView, m_sTitle, "Создателей fb2 файла" );
		}
		
		/* Publish Info */
		void PISequenceAddButtonClick(object sender, EventArgs e)
		{
			// создание новой Серии для Бумажной книги
			createNewSequence( PISequenceListView, Core.Misc.Enums.SequenceEnum.PaperBook );
		}
		void PISequenceEditButtonClick(object sender, EventArgs e)
		{
			// редактирование Серии для Бумажной книги
			editSelectedSequence( PISequenceListView, Core.Misc.Enums.SequenceEnum.PaperBook );
		}
		void PISequenceDeleteButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteSelectedItem( PISequenceListView, m_sTitle, "Бумажной книги" );
		}
		void PISequenceDeleteAllButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteAllItems( PISequenceListView, m_sTitle, "Бумажной книги" );
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
		
		/* Обложки */
		void TICoverListViewClick(object sender, EventArgs e)
		{
			if( TICoverListView.SelectedItems.Count > 0 )
				TICoverPictureBox.Image = ImageWorker.base64ToImage( TICoverListView.SelectedItems[0].Tag.ToString() );
		}
		void TICoverAddButtonClick(object sender, EventArgs e)
		{
			addCoverToList( Core.Misc.Enums.TitleInfoEnum.TitleInfo );
		}
		void TICoverDeleteButtonClick(object sender, EventArgs e)
		{
			// удаление выбранной обложки в тэге cover
			if( TICoverListView.SelectedItems.Count > 0 ) {
				string Href = TICoverListView.SelectedItems[0].Text;
				if( MiscListView.deleteSelectedItem( TICoverListView, m_sTitle, "Обложек" ) )
					deleteCoverForHref( Core.Misc.Enums.TitleInfoEnum.TitleInfo, Href );
			}
		}
		void TICoverDeleteAllButtonClick(object sender, EventArgs e)
		{
			IList<string> lIDList = new List<string>( TICoverListView.Items.Count );
			foreach( ListViewItem item in TICoverListView.Items )
				lIDList.Add( item.Text );
			if( MiscListView.deleteAllItems( TICoverListView, m_sTitle, "Обложек" ) )
				// удаление тэга cover и всех binary Обложек
				deleteAllCover( Core.Misc.Enums.TitleInfoEnum.TitleInfo, ref lIDList );
		}
		
		void STICoverListViewClick(object sender, EventArgs e)
		{
			if( STICoverListView.SelectedItems.Count > 0 )
				STICoverPictureBox.Image = ImageWorker.base64ToImage( STICoverListView.SelectedItems[0].Tag.ToString() );
		}
		void STICoverAddButtonClick(object sender, EventArgs e)
		{
			addCoverToList( Core.Misc.Enums.TitleInfoEnum.SourceTitleInfo );
		}
		void STICoverDeleteButtonClick(object sender, EventArgs e)
		{
			// удаление выбранной обложки в тэге cover Оригинала
			if( STICoverListView.SelectedItems.Count > 0 ) {
				string Href = STICoverListView.SelectedItems[0].Text;
				if( MiscListView.deleteSelectedItem( STICoverListView, m_sTitle, "Обложек" ) )
					deleteCoverForHref( Core.Misc.Enums.TitleInfoEnum.SourceTitleInfo, Href );
			}
		}
		void STICoverDeleteAllButtonClick(object sender, EventArgs e)
		{
			IList<string> lIDList = new List<string>( STICoverListView.Items.Count );
			foreach( ListViewItem item in STICoverListView.Items )
				lIDList.Add( item.Text );
			if( MiscListView.deleteAllItems( STICoverListView, m_sTitle, "Обложек" ) )
				// удаление тэга cover и всех binary Обложек Оригинала
				deleteAllCover( Core.Misc.Enums.TitleInfoEnum.TitleInfo, ref lIDList );
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
			
			XmlNode fb = m_fb2.getFictionBookNode();
			if( fb != null ) {
				XmlDocument xmlDoc = m_fb2.getXmlDoc();
				// создаем ВСЕ разделя description, заполняем их и потом МЕНЯЕМ с существующими разделами,
				// а если таких разделов нет - то ВСТАВЛЯЕМ созданные
				/* Title Info */
				processTitleInfoNode( ref xmlDoc, makeTitleInfoNode( ref xmlDoc, Core.Misc.Enums.TitleInfoEnum.TitleInfo ) );
				/* Source Title Info */
				processSourceTitleInfoNode( ref xmlDoc, makeTitleInfoNode( ref xmlDoc, Core.Misc.Enums.TitleInfoEnum.SourceTitleInfo ) );
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
