/*
 * Сделано в SharpDevelop.
 * Пользователь: Вадим Кузнецов (DikBSD)
 * Дата: 21.07.2015
 * Время: 8:37
 * 
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using System.IO;

using Core.FB2.Description.TitleInfo;
using Core.Common;
using Core.FB2.FB2Parsers;
using Core.FB2.Genres;

using MiscListView = Core.Common.MiscListView;
using TitleInfoEnum = Core.Common.Enums.TitleInfoEnum;
using AuthorEnum = Core.Common.Enums.AuthorEnum;

namespace Core.Common
{
	/// <summary>
	/// EditGenreInfoForm: форма для правки выбранного Жанра...
	/// </summary>
	public partial class EditGenreInfoForm : Form
	{
		private readonly string	m_TempDir = Settings.Settings.TempDirPath;
		private const string m_sTitle = "Правка Жанров fb2 книг";
		private readonly IList<FB2ItemInfo> m_GenreFB2InfoList = null;
		private bool m_ApplyData = false;
		private readonly SharpZipLibWorker m_sharpZipLib = new SharpZipLibWorker();
		private BackgroundWorker m_bw = null;
		
		public EditGenreInfoForm( ref IList<FB2ItemInfo> GenreFB2InfoList )
		{
			InitializeComponent();
			initializeBackgroundWorker();
			
			this.Text += String.Format( " : {0} книг", GenreFB2InfoList.Count );
			m_GenreFB2InfoList = GenreFB2InfoList;
			
			// формирование Списка Групп Жанров
			WorksWithBooks.makeListGenresGroups( GroupComboBox );

			// загрузка Жанров для правки
			loadGenresFromFB2Files( TitleInfoEnum.TitleInfo );
			
			ControlPanel.Enabled = true;
			GenresSchemePanel.Enabled = true;
			GenreWorkPanel.Enabled = true;
			
			ProgressBar.Maximum = GenreFB2InfoList.Count;
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
			foreach ( FB2ItemInfo Info in m_GenreFB2InfoList ) {
				FictionBook fb2 = Info.FictionBook;
				if ( fb2 != null ) {
					// восстанавление раздела description до структуры с необходимыми элементами для валидности
					fB2Corrector = new FB2DescriptionCorrector( fb2 );
					fB2Corrector.recoveryDescriptionNode();
					
					IList<XmlNode> xmlNewGenres = makeGenreNode( ref fb2, GenresListView );
					if ( xmlNewGenres != null ) {
						XmlDocument xmlDoc = fb2.getXmlDoc();
						XmlNodeList xmlGenreList = fb2.getGenreNodes( TitleInfoEnum.TitleInfo );
						if ( xmlGenreList != null ) {
							XmlNodeList xmlAuthorList = fb2.getAuthorNodes( TitleInfoEnum.TitleInfo );
							if ( xmlAuthorList != null ) {
								XmlNode xmlTINode = fb2.getTitleInfoNode( TitleInfoEnum.TitleInfo );
								if ( xmlTINode != null ) {
									// удаление старых данных Жанров
									foreach ( XmlNode g in xmlGenreList )
										xmlTINode.RemoveChild( g );
									// добавление новых данных Жанров
									foreach ( XmlNode g in xmlNewGenres )
										xmlTINode.InsertBefore( g, xmlAuthorList[0] );
									// сохранение fb2 файла
									if ( !Directory.Exists( m_TempDir ) )
										Directory.CreateDirectory( m_TempDir );
									string NewPath = Info.IsFromZip ? Info.FilePathIfFromZip : Info.FilePathSource;
									fb2.saveToFB2File( NewPath, false );
									if ( Info.IsFromZip )
										WorksWithBooks.zipMoveTempFB2FileTo( m_sharpZipLib, NewPath, Info.FilePathSource );
									if ( Info.IsFromZip && File.Exists( NewPath ) )
										File.Delete( NewPath );
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
		
		#region Открытые свойства
		public bool isApplyData() {
			return m_ApplyData;
		}
		#endregion
		
		#region Закрытые вспомогательные методы
		// загрузка Жанров для правки
		private void loadGenresFromFB2Files( Enums.TitleInfoEnum TitleInfoType ) {
			FB2UnionGenres fb2g = new FB2UnionGenres();
			foreach ( FB2ItemInfo Info in m_GenreFB2InfoList ) {
				if ( Info.FictionBook != null ) {
					IList<Genre> GenresList = TitleInfoType == Enums.TitleInfoEnum.TitleInfo
						? Info.FictionBook.TIGenres : Info.FictionBook.STIGenres;
					if ( GenresList != null ) {
						foreach ( Genre g in GenresList ) {
							if ( g != null ) {
								if ( !WorksWithBooks.genreIsExist( GenresListView, g, fb2g ) ) {
									ListViewItem lvi = new ListViewItem(
										!string.IsNullOrEmpty( g.Name )
										? fb2g.GetFBGenreName( g.Name ) + " (" + g.Name + ")"
										: string.Empty
									);
									lvi.SubItems.Add( !string.IsNullOrEmpty( g.Math.ToString() ) ? g.Math.ToString() : string.Empty );
									if ( !string.IsNullOrEmpty( g.Name ) )
										GenresListView.Items.Add( lvi );
								}
							}
						}
					}
				}
			}
		}
		private IList<XmlNode> makeGenreNode( ref FictionBook fb2, ListView lv ) {
			IList<XmlNode> Genres = null;
			XmlNode xmlGenre = null;
			if ( lv.Items.Count > 0 ) {
				Genres = new List<XmlNode>( lv.Items.Count );
				FB2DescriptionCorrector fB2Corrector = new FB2DescriptionCorrector( fb2 );
				if ( lv.Items.Count > 0 ) {
					foreach ( ListViewItem item in lv.Items ) {
						string code = item.Text.Substring( item.Text.IndexOf('(') + 1 );
						xmlGenre = fB2Corrector.makeGenreNode( code.Substring( 0, code.Length - 1), item.SubItems[1].Text );
						Genres.Add(xmlGenre);
					}
				} else {
					xmlGenre = fB2Corrector.makeGenreNode( "other", null );
					Genres.Add(xmlGenre);
				}
			}
			return Genres;
		}
		#endregion
		
		#region Обработчики событий
		void CancelBtnClick(object sender, EventArgs e)
		{
			Close();
		}
		void GroupComboBoxSelectedIndexChanged(object sender, EventArgs e)
		{
			// формирование Списка Жанров в контролы, в зависимости от Группы
			WorksWithBooks.makeListGenres( GenresComboBox, GroupComboBox.Text );
		}
		void GenreAddButtonClick(object sender, EventArgs e)
		{
			if ( !MiscListView.isExistListViewItem( GenresListView, GenresComboBox.Text ) ) {
				ListViewItem lvi = new ListViewItem( GenresComboBox.Text );
				lvi.SubItems.Add( MatchMaskedTextBox.Text.Trim() );
				GenresListView.Items.Add( lvi );
				MatchMaskedTextBox.Clear();
			}
		}
		void GenreDeleteButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteSelectedItem( GenresListView, m_sTitle, "Жанров" );
		}
		void GenreDeleteAllButtonClick(object sender, EventArgs e)
		{
			MiscListView.deleteAllItems( GenresListView, m_sTitle, "Жанров" );
		}
		void GenreUpButtonClick(object sender, EventArgs e)
		{
			if ( GenresListView.Items.Count > 0 && GenresListView.SelectedItems.Count > 0 ) {
				if ( GenresListView.SelectedItems.Count == 1 )
					MiscListView.moveUpSelectedItem( GenresListView );
				else
					MessageBox.Show( "Выберите только один Жанр для перемещения!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
			GenresListView.Select();
		}
		void GenreDownButtonClick(object sender, EventArgs e)
		{
			if ( GenresListView.Items.Count > 0 && GenresListView.SelectedItems.Count > 0 ) {
				if ( GenresListView.SelectedItems.Count == 1 )
					MiscListView.moveDownSelectedItem( GenresListView );
				else
					MessageBox.Show( "Выберите только один Жанр для перемещения!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
			GenresListView.Select();
		}
		void ApplyBtnClick(object sender, EventArgs e)
		{
			if ( GenresListView.Items.Count == 0 ) {
				MessageBox.Show( "Заполните данные хотя бы для одного Жанра!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
			} else {
				m_ApplyData = true;
				ControlPanel.Enabled = false;
				GenresSchemePanel.Enabled = false;
				GenreWorkPanel.Enabled = false;
				if ( !m_bw.IsBusy )
					m_bw.RunWorkerAsync();
			}
		}
		void GenresListViewSelectedIndexChanged(object sender, EventArgs e)
		{
			// Select Группу жанра и сам Жанр в выпадающих списках по выделенному Жанру в списке
			if ( GenresListView.Items.Count > 0 && GenresListView.SelectedItems.Count == 1 ) {
				ListViewItem SelItem = GenresListView.SelectedItems[0];
				FB2UnionGenres fb2g = new FB2UnionGenres();
				string GenreCode = SelItem.Text.Substring( SelItem.Text.IndexOf('(') + 1 );
				GenreCode = GenreCode.Substring( 0, GenreCode.Length - 1);
				string GenreGroup = fb2g.GetFBGenreGroup( GenreCode );
				GroupComboBox.Text = GenreGroup;
				GenresComboBox.Text = SelItem.Text;
			}
		}
		#endregion
	}
}
