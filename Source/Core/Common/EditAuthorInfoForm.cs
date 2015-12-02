/*
 * Сделано в SharpDevelop.
 * Пользователь: Вадим Кузнецов (DikBSD)
 * Дата: 17.07.2015
 * Время: 7:46
 * 
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using System.IO;
using System.Linq;


using Core.FB2.Description.Common;
using Core.FB2.FB2Parsers;
using Core.Common;

using MiscListView = Core.Common.MiscListView;
using TitleInfoEnum = Core.Common.Enums.TitleInfoEnum;
using AuthorEnum = Core.Common.Enums.AuthorEnum;

namespace Core.Common
{
	/// <summary>
	/// EditAuthorInfoForm: форма для правки выбранного Автора...
	/// </summary>
	public partial class EditAuthorInfoForm : Form
	{
		private readonly string	m_TempDir = Settings.Settings.TempDir;
		private const string m_sTitle = "Правка метаданных Авторов fb2 книг";
		private readonly IList<FB2ItemInfo> m_AuthorFB2InfoList = null;
		private bool m_ApplyData = false;
		private readonly SharpZipLibWorker m_sharpZipLib = new SharpZipLibWorker();
		private BackgroundWorker m_bw = null;
		
		private bool m_EditMode = false; // В режиме правки Автора m_EditMode = true; В режиме добавления Нового Автора m_EditMode = false;
		
		public EditAuthorInfoForm( ref IList<FB2ItemInfo> AuthorFB2InfoList )
		{
			InitializeComponent();
			initializeBackgroundWorker();
			
			this.Text += " : " + AuthorFB2InfoList.Count.ToString() + " книг";
			m_AuthorFB2InfoList = AuthorFB2InfoList;
			// загрузка Авторов для правки
			loadAuthorsFromFB2Files();
			
			ControlPanel.Enabled = true;
			AuthorDataPanel.Enabled = true;
			AuthorsWorkPanel.Enabled = true;
			
			ProgressBar.Maximum = AuthorFB2InfoList.Count;
		}
		
		#region BackgroundWorker
		// Инициализация перед использование BackgroundWorker
		private void initializeBackgroundWorker() {
			m_bw = new BackgroundWorker();
			m_bw.WorkerReportsProgress		= true; // Позволить выводить прогресс процесса
			m_bw.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
			m_bw.DoWork 			+= new DoWorkEventHandler( bw_DoWork );
			m_bw.ProgressChanged 	+= new ProgressChangedEventHandler( bw_ProgressChanged );
			m_bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler( bw_RunWorkerCompleted );
		}
		private void bw_DoWork( object sender, DoWorkEventArgs e ) {
			Cursor.Current = Cursors.WaitCursor;
			FB2DescriptionCorrector fB2Corrector = null;
			foreach( FB2ItemInfo Info in m_AuthorFB2InfoList ) {
				FictionBook fb2 = Info.FictionBook;
				if( fb2 != null ) {
					// восстанавление раздела description до структуры с необходимыми элементами для валидности
					fB2Corrector = new FB2DescriptionCorrector( ref fb2 );
					fB2Corrector.recoveryDescriptionNode();
					
					IList<XmlNode> xmlNewAuthors = makeAuthorNode( Enums.AuthorEnum.AuthorOfBook, ref fb2, AuthorsListView );
					if( xmlNewAuthors != null ) {
						XmlNodeList xmlAuthorList = fb2.getAuthorNodes( TitleInfoEnum.TitleInfo );
						if( xmlAuthorList != null ) {
							XmlNode xmlBookTitleNode = fb2.getBookTitleNode( TitleInfoEnum.TitleInfo );
							if( xmlBookTitleNode != null ) {
								XmlNode xmlTINode = fb2.getTitleInfoNode( TitleInfoEnum.TitleInfo );
								if( xmlTINode != null ) {
									// удаление старых данных Авторов
									foreach( XmlNode Author in xmlAuthorList )
										xmlTINode.RemoveChild( Author );
									// добавление новых данных Авторов
									foreach( XmlNode Author in xmlNewAuthors )
										xmlTINode.InsertBefore( Author, xmlBookTitleNode );
									
									// сохранение fb2 файла
									if( !Directory.Exists( m_TempDir ) )
										Directory.CreateDirectory( m_TempDir );
									string NewPath = Info.IsFromArhive ? Info.FilePathIfFromZip : Info.FilePathSource;
									fb2.saveToFB2File( NewPath, false );
									WorksWithBooks.zipMoveTempFB2FileTo( m_sharpZipLib, Info.FilePathSource, Info.IsFromArhive, NewPath );
								}
							}
						}
					}
				}
				m_bw.ReportProgress( 1 );
			}
			Cursor.Current = Cursors.Default;
		}
		private void bw_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
			++ProgressBar.Value;
		}
		private void bw_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {
			Close();
		}
		#endregion
		
		#region Открытые методы
		public bool isApplyData() {
			return m_ApplyData;
		}
		#endregion
		
		#region Закрытые вспомогательные методы
		// загрузка Авторов для правки
		private void loadAuthorsFromFB2Files() {
			foreach( FB2ItemInfo Info in m_AuthorFB2InfoList ) {
				if( Info.FictionBook != null ) {
					IList<Author> AuthorsList = Info.FictionBook.TIAuthors;
					if ( AuthorsList != null ) {
						foreach( Author a in AuthorsList ) {
							if( a != null ) {
								if ( !WorksWithBooks.authorIsExist( AuthorsListView, a ) ) {
									ListViewItem lvi = new ListViewItem( "" );
									if( a.LastName != null )
										lvi.Text = !string.IsNullOrEmpty( a.LastName.Value ) ? a.LastName.Value : string.Empty;
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
									if( a.HomePages != null )
										lvi.SubItems.Add( StringProcessing.makeStringFromListItems( a.HomePages ) );
									else
										lvi.SubItems.Add( string.Empty );
									if( a.Emails != null )
										lvi.SubItems.Add( StringProcessing.makeStringFromListItems( a.Emails ) );
									else
										lvi.SubItems.Add( string.Empty );
									lvi.SubItems.Add( !string.IsNullOrEmpty( a.ID ) ? a.ID : string.Empty );
									AuthorsListView.Items.Add( lvi );
								}
							}
						}
					}
				}
			}
		}
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
		// завершение режима правки автора
		private void cancelEditMode() {
			LastNameTextBox.Text = FirstNameTextBox.Text = MiddleNameTextBox.Text =
				NickNameTextBox.Text = HomePageTextBox.Text = EmailTextBox.Text = IDTextBox.Text = string.Empty;
			AuthorsWorkPanel.Enabled = ApplyBtn.Enabled = AuthorsListView.Enabled = true;
			m_EditMode = false;
		}
		#endregion
		
		#region Обработчики событий
		void CancelBtnClick(object sender, EventArgs e)
		{
			Close();
		}
		void NewIDButtonClick(object sender, EventArgs e)
		{
			string sMess = "Создать новый id Автора?";
			const MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			DialogResult result = MessageBox.Show( sMess, "Создание нового id", buttons, MessageBoxIcon.Question );
			if( result == DialogResult.Yes )
				IDTextBox.Text = Guid.NewGuid().ToString().ToUpper();
		}
		void AuthorAddButtonClick(object sender, EventArgs e)
		{
			if( LastNameTextBox.Text.Trim().Length == 0 && FirstNameTextBox.Text.Trim().Length == 0 &&
			   MiddleNameTextBox.Text.Trim().Length == 0 && NickNameTextBox.Text.Trim().Length == 0 &&
			   IDTextBox.Text.Trim().Length == 0 && HomePageTextBox.Text.Trim().Length == 0 && EmailTextBox.Text.Trim().Length == 0 ) {
				MessageBox.Show( "Ни одно поле не заполнено!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				LastNameTextBox.Focus();
				return;
			} else if( LastNameTextBox.Text.Trim().Length == 0 ) {
				MessageBox.Show( "Поле 'Фамилия' должно быть заполнено обязательно!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				LastNameTextBox.Focus();
				return;
			}
			
			AuthorBreakEditButton.Visible = false;
			if ( m_EditMode ) {
				// режим правки автора
				ListViewItem SelectedItem = AuthorsListView.SelectedItems[0];
				SelectedItem.SubItems[0].Text = LastNameTextBox.Text.Trim() ;
				SelectedItem.SubItems[1].Text = FirstNameTextBox.Text.Trim();
				SelectedItem.SubItems[2].Text = MiddleNameTextBox.Text.Trim();
				SelectedItem.SubItems[3].Text = NickNameTextBox.Text.Trim();
				SelectedItem.SubItems[4].Text = HomePageTextBox.Text.Trim();
				SelectedItem.SubItems[5].Text = EmailTextBox.Text.Trim();
				SelectedItem.SubItems[6].Text = IDTextBox.Text.Trim();
			} else {
				List<string> list1 = new List<string>();
				list1.Add(
					LastNameTextBox.Text.Trim() + FirstNameTextBox.Text.Trim() + MiddleNameTextBox.Text.Trim() +
					NickNameTextBox.Text.Trim() + IDTextBox.Text.Trim()
				);
				foreach ( ListViewItem Item in AuthorsListView.Items ) {
					List<string> list2 = new List<string>();
					list2.Add(
						Item.SubItems[0].Text.Trim() + Item.SubItems[1].Text.Trim() + Item.SubItems[2].Text.Trim() +
						Item.SubItems[3].Text.Trim() + Item.SubItems[6].Text.Trim()
					);
					List<string> list3 = list1.Intersect(list2, new FB2EqualityComparer()).ToList();
					if ( list3.Count >= 1 ) {
						MessageBox.Show( "В списке Авторов уже есть Автор с точно такими же данными!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning );
						LastNameTextBox.Focus();
						return;
					}
				}

				ListViewItem lvi = new ListViewItem( LastNameTextBox.Text.Trim() );
				lvi.SubItems.Add( FirstNameTextBox.Text.Trim() );
				lvi.SubItems.Add( MiddleNameTextBox.Text.Trim() );
				lvi.SubItems.Add( NickNameTextBox.Text.Trim() );
				lvi.SubItems.Add( HomePageTextBox.Text.Trim() );
				lvi.SubItems.Add( EmailTextBox.Text.Trim() );
				lvi.SubItems.Add( IDTextBox.Text.Trim() );
				AuthorsListView.Items.Add( lvi );
			}
			
			cancelEditMode();
			LastNameTextBox.Focus();
		}
		void TextBoxKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
				AuthorAddButtonClick( sender, e );
		}
		void AuthorBreakEditButtonClick(object sender, EventArgs e)
		{
			cancelEditMode();
			AuthorBreakEditButton.Visible = false;
		}
		void AuthorEditButtonClick(object sender, EventArgs e)
		{
			if( AuthorsListView.SelectedItems.Count > 1 ) {
				MessageBox.Show( "Выберите только одного Автора для редактирования!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
			} else if( AuthorsListView.SelectedItems.Count != 1 ) {
				MessageBox.Show( "Выберите одного Автора для редактирования.", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
			} else {
				m_EditMode = true;
				AuthorBreakEditButton.Visible = true;
				AuthorsWorkPanel.Enabled = ApplyBtn.Enabled = AuthorsListView.Enabled = false;
				LastNameTextBox.Text = AuthorsListView.SelectedItems[0].SubItems[0].Text;
				FirstNameTextBox.Text = AuthorsListView.SelectedItems[0].SubItems[1].Text;
				MiddleNameTextBox.Text = AuthorsListView.SelectedItems[0].SubItems[2].Text;
				NickNameTextBox.Text = AuthorsListView.SelectedItems[0].SubItems[3].Text;
				HomePageTextBox.Text = AuthorsListView.SelectedItems[0].SubItems[4].Text;
				EmailTextBox.Text = AuthorsListView.SelectedItems[0].SubItems[5].Text;
				IDTextBox.Text = AuthorsListView.SelectedItems[0].SubItems[6].Text;
				LastNameTextBox.Focus();
			}
		}
		void AuthorDeleteButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteSelectedItem( AuthorsListView, m_sTitle, "Авторов Книг" );
		}
		void AuthorDeleteAllButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteAllItems( AuthorsListView, m_sTitle, "Авторов Книг" );
		}
		void AuthorUpButtonClick(object sender, EventArgs e)
		{
			if( AuthorsListView.Items.Count > 0 && AuthorsListView.SelectedItems.Count > 0 ) {
				if( AuthorsListView.SelectedItems.Count == 1 )
					MiscListView.moveUpSelectedItem( AuthorsListView );
				else
					MessageBox.Show( "Выберите только одного Автора для перемещения!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
			AuthorsListView.Select();
		}
		void AuthorDownButtonClick(object sender, EventArgs e)
		{
			if( AuthorsListView.Items.Count > 0 && AuthorsListView.SelectedItems.Count > 0 ) {
				if( AuthorsListView.SelectedItems.Count == 1 )
					MiscListView.moveDownSelectedItem( AuthorsListView );
				else
					MessageBox.Show( "Выберите только одного Автора для перемещения!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
			AuthorsListView.Select();
		}
		void ApplyBtnClick(object sender, EventArgs e)
		{
			if( AuthorsListView.Items.Count == 0 ) {
				MessageBox.Show( "Заполните данные хотя бы для одного Автора!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			} else {
				m_ApplyData = true;
				ControlPanel.Enabled = false;
				AuthorDataPanel.Enabled = false;
				AuthorsWorkPanel.Enabled = false;
				if( !m_bw.IsBusy )
					m_bw.RunWorkerAsync();
			}
		}
		
		#endregion
	}
}
