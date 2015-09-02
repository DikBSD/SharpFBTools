/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 13.02.2015
 * Время: 9:34
 * 
  * License: GPL 2.1
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Linq;
using System.Xml.Linq;

using Core.Common;
using Core.FB2.Genres;
using Core.FB2.Binary;
using Core.FB2.Description.CustomInfo;
using Core.Corrector;
using Core.FB2.FB2Parsers;

using FB2Validator		= Core.FB2Parser.FB2Validator;
using FilesWorker		= Core.Common.FilesWorker;
using WorksWithBooks	= Core.Common.WorksWithBooks;

// enums
using ResultViewCollumn = Core.Common.Enums.ResultViewCollumn;
using BooksWorkMode		= Core.Common.Enums.BooksWorkMode;
using EndWorkModeEnum	= Core.Common.Enums.EndWorkModeEnum;
using BooksValidateMode = Core.Common.Enums.BooksValidateMode;

namespace SharpFBTools.Tools
{
	/// <summary>
	/// Правка / массая правка метаданных книг
	/// </summary>
	public partial class SFBTpFB2Corrector : UserControl
	{
		#region Закрытые данные класса
		private readonly FB2Validator m_fv2Validator = new FB2Validator();
		private readonly string	m_TempDir	= Settings.Settings.TempDir;
		private bool m_isSettingsLoaded		= false; 		// Только при true все изменения настроек сохраняются в файл.
		private string	m_TargetDir			= string.Empty; // Папка для Copy / Move помеченных книг
		private string	m_DirForSavedCover	= string.Empty;	// папка для сохранения обложек
		private int	  m_CurrentResultItem	= -1;
		private readonly SharpZipLibWorker	m_sharpZipLib = new SharpZipLibWorker();
		private readonly MiscListView.FilemanagerColumnSorter m_lvwColumnSorter = new MiscListView.FilemanagerColumnSorter();
		#endregion
		
		public SFBTpFB2Corrector()
		{
			InitializeComponent();
			
			cboxExistFile.SelectedIndex			= 1;
			cboxDblClickForFB2.SelectedIndex	= 1;
			cboxPressEnterForFB2.SelectedIndex	= 0;
			
			/* читаем сохраненные пути к папкам и шаблон Менеджера Файлов, если они есть */
			readSettingsFromXML();
			m_isSettingsLoaded = true;
//			MiscListView.AutoResizeColumns( listViewFB2Files );
		}
		
		#region Закрытые вспомогательные методы класса
		// сохранение настроек в xml-файл
		private void saveSettingsToXml() {
			// защита от "затирания" настроек в файле, когда в некоторые контролы данные еще не загрузились
			if( m_isSettingsLoaded ) {
				XDocument doc = new XDocument(
					new XDeclaration("1.0", "utf-8", "yes"),
					new XElement("Settings", new XAttribute("type", "editor_settings"),
					             new XComment("xml файл настроек Редактора метаданных"),
					             new XComment("Проводник"),
					             new XElement("Explorer",
					                          new XComment("Папка исходных fb2-файлов"),
					                          new XElement("SourceDir", textBoxAddress.Text.Trim()),
					                          new XComment("Проверять на валидность"),
					                          new XElement("NeedValid", checkBoxNeedValid.Checked),
					                          new XComment("Активная Схема Жанров"),
					                          new XElement("FB2Genres",
					                                       new XAttribute("Librusec", rbtnFB2Librusec.Checked),
					                                       new XAttribute("FB22", rbtnFB22.Checked)
					                                      ),
					                          new XComment("Папка для копирования/перемещения копий fb2 книг"),
					                          new XElement("TargetDir", m_TargetDir),
					                          
					                          new XComment("Настройки для Копирования / Перемещения файлов - Одинаковые файлы в папке-приемнике"),
					                          new XElement("ExistFile", cboxExistFile.SelectedIndex),
					                          new XComment("Действие по двойному щелчку мышки на Списке"),
					                          new XElement("DblClickForFB2", cboxDblClickForFB2.SelectedIndex),
					                          new XComment("Действие по нажатию клавиши Enter на Списке"),
					                          new XElement("PressEnterForFB2", cboxPressEnterForFB2.SelectedIndex)
					                         )
					            )
				);
				doc.Save( Settings.CorrectorSettings.CorrectorPath );
			}
		}
		// загрузка настроек из xml-файла
		private void readSettingsFromXML() {
			if( File.Exists( Settings.CorrectorSettings.CorrectorPath ) ) {
				XElement xmlTree = XElement.Load( Settings.CorrectorSettings.CorrectorPath );
				/* Explorer */
				if( xmlTree.Element("Explorer") != null ) {
					XElement xmlExplorer = xmlTree.Element("Explorer");
					// Папка исходных fb2-файлов
					if( xmlExplorer.Element("SourceDir") != null )
						textBoxAddress.Text = xmlExplorer.Element("SourceDir").Value;
					// Активная Схема Жанров
					if( xmlExplorer.Element("FB2Genres") != null ) {
						XElement xmlFB2Genres = xmlExplorer.Element("FB2Genres");
						if( xmlFB2Genres.Attribute("Librusec") != null )
							rbtnFB2Librusec.Checked = Convert.ToBoolean( xmlFB2Genres.Attribute("Librusec").Value );
						if( xmlFB2Genres.Attribute("FB22") != null )
							rbtnFB22.Checked = Convert.ToBoolean( xmlFB2Genres.Attribute("FB22").Value );
					}
					// Проверять на валидность
					if( xmlExplorer.Element("NeedValid") != null )
						checkBoxNeedValid.Checked = Convert.ToBoolean( xmlExplorer.Element("NeedValid").Value );
					// Папка для копирования/перемещения копий fb2 книг
					if( xmlTree.Element("TargetDir") != null )
						m_TargetDir = xmlTree.Element("TargetDir").Value;
					
					// Настройки для Копирования / Перемещения файлов - Одинаковые файлы в папке-приемнике
					if( xmlExplorer.Element("ExistFile") != null )
						cboxExistFile.SelectedIndex = Convert.ToInt16( xmlExplorer.Element("ExistFile").Value );
					// Действие по двойному щелчку мышки на Списке
					if( xmlExplorer.Element("DblClickForFB2") != null )
						cboxDblClickForFB2.SelectedIndex = Convert.ToInt16( xmlExplorer.Element("DblClickForFB2").Value );
					// Действие по нажатию клавиши Enter на Списке
					if( xmlExplorer.Element("PressEnterForFB2") != null )
						cboxPressEnterForFB2.SelectedIndex = Convert.ToInt16( xmlExplorer.Element("PressEnterForFB2").Value );
				}
			}
		}
		
		private void ConnectListsEventHandlers( bool bConnect ) {
			if( !bConnect ) {
				// отключаем обработчики событий для Списка (убираем "тормоза")
				this.listViewFB2Files.DoubleClick -= new System.EventHandler(this.ListViewFB2FilesDoubleClick);
				this.listViewFB2Files.ItemChecked -= new System.Windows.Forms.ItemCheckedEventHandler(this.ListViewFB2FilesItemChecked);
				this.listViewFB2Files.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.ListViewFB2FilesItemCheck);
				this.listViewFB2Files.KeyPress -= new System.Windows.Forms.KeyPressEventHandler(this.ListViewFB2FilesKeyPress);
				this.listViewFB2Files.ColumnClick -= new System.Windows.Forms.ColumnClickEventHandler(this.ListViewFB2FilesColumnClick);
				this.listViewFB2Files.SelectedIndexChanged -= new System.EventHandler(this.ListViewFB2FilesSelectedIndexChanged);
			} else {
				// подключаем обработчики событий для Списка
				this.listViewFB2Files.DoubleClick += new System.EventHandler(this.ListViewFB2FilesDoubleClick);
				this.listViewFB2Files.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.ListViewFB2FilesItemChecked);
				this.listViewFB2Files.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ListViewFB2FilesItemCheck);
				this.listViewFB2Files.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ListViewFB2FilesKeyPress);
				this.listViewFB2Files.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListViewFB2FilesColumnClick);
				this.listViewFB2Files.SelectedIndexChanged += new System.EventHandler(this.ListViewFB2FilesSelectedIndexChanged);
			}
		}
		// заполнение списка данными указанной папки
		private void generateFB2List( string dirPath ) {
			// отображение метаданных книг
			DirectoryInfo dirInfo = new DirectoryInfo( dirPath );
			// запуск формы прогресса отображения метаданных книг
			Cursor.Current = Cursors.WaitCursor;
			listViewFB2Files.BeginUpdate();
			ConnectListsEventHandlers( false );
			
			IGenresGroup GenresGroup = new GenresGroup();
			IFBGenres fb2Genres = GenresWorker.genresListOfGenreSheme( rbtnFB2Librusec.Checked, ref GenresGroup );
			FB2TagsListGenerateForm fb2TagsListGenerateForm = new FB2TagsListGenerateForm(
				rbtnFB2Librusec.Checked, fb2Genres, listViewFB2Files,
				dirPath, checkBoxNeedValid.Checked, false
			);
			fb2TagsListGenerateForm.ShowDialog();
			EndWorkMode EndWorkMode = fb2TagsListGenerateForm.EndMode;
			fb2TagsListGenerateForm.Dispose();
			if( EndWorkMode.EndMode != EndWorkModeEnum.Done )
				MessageBox.Show( EndWorkMode.Message, "Отображение метаданных книг", MessageBoxButtons.OK, MessageBoxIcon.Information );
			ConnectListsEventHandlers( true );
			listViewFB2Files.EndUpdate();
			Cursor.Current = Cursors.Default;
		}
		// очистка контролов вывода данных по книге по ее выбору
		private void clearDataFields() {
			for( int i = 0; i != lvTitleInfo.Items.Count; ++i ) {
				lvTitleInfo.Items[i].SubItems[1].Text		= string.Empty;
				lvSourceTitleInfo.Items[i].SubItems[1].Text	= string.Empty;
			}
			for( int i = 0; i != lvDocumentInfo.Items.Count; ++i )
				lvDocumentInfo.Items[i].SubItems[1].Text = string.Empty;

			for( int i = 0; i != lvPublishInfo.Items.Count; ++i )
				lvPublishInfo.Items[i].SubItems[1].Text = string.Empty;

			lvCustomInfo.Items.Clear();
			rtbHistory.Clear();
			rtbTIAnnotation.Clear();
			rtbSTIAnnotation.Clear();
			tbValidate.Clear();
			TICoversListView.Items.Clear();
			STICoversListView.Items.Clear();
			
			picBoxTICover.Image = imageListDescEditor.Images[0];
			picBoxSTICover.Image = imageListDescEditor.Images[0];
		}
		
		// занесение данных книги в контролы для просмотра
		private void viewBookMetaDataFull( ref FB2BookDescription fb2Desc, ListViewItem SelectedItem ) {
			ConnectListsEventHandlers( false );
			
			try {
				if ( File.Exists( fb2Desc.FilePath ) && !SelectedItem.Font.Strikeout ) {
					// очистка контролов вывода данных по книге по ее выбору
					m_CurrentResultItem = SelectedItem.Index;
					clearDataFields();
					if( fb2Desc != null ) {
						// загрузка обложек книги
						IList<BinaryBase64> Covers = fb2Desc.TICoversBase64;
						if ( Covers != null ) {
							ImageWorker.makeListViewCoverNameItems( TICoversListView, ref Covers );
							if( TICoversListView.Items.Count > 0 ) {
								TICoversListView.Items[0].Selected = true;
								TICoverListViewButtonPanel.Enabled = true;
							} else {
								picBoxTICover.Image = imageListDescEditor.Images[0];
								TICoverListViewButtonPanel.Enabled = false;
							}
						}

						// загруxзка обложек оригинала книги
						Covers = fb2Desc.STICoversBase64;
						if ( Covers != null ) {
							ImageWorker.makeListViewCoverNameItems( STICoversListView, ref Covers );
							if( STICoversListView.Items.Count > 0 ) {
								STICoversListView.Items[0].Selected = true;
								STICoverListViewButtonPanel.Enabled = true;
							} else {
								picBoxSTICover.Image = imageListDescEditor.Images[0];
								STICoverListViewButtonPanel.Enabled = false;
							}
						}
						
						// спиок жанров, в зависимости от схемы Жанров
						IGenresGroup GenresGroup = new GenresGroup();
						IFBGenres fb2g = GenresWorker.genresListOfGenreSheme( rbtnFB2Librusec.Checked, ref GenresGroup );
						
						// считываем данные TitleInfo
						MiscListView.ListViewStatus( lvTitleInfo, 0, fb2Desc.TIBookTitle );
						MiscListView.ListViewStatus( lvTitleInfo, 1, GenresWorker.cyrillicGenreNameAndCode( fb2Desc.TIGenres, ref fb2g ) );
						MiscListView.ListViewStatus( lvTitleInfo, 2, fb2Desc.TILang );
						MiscListView.ListViewStatus( lvTitleInfo, 3, fb2Desc.TISrcLang );
						MiscListView.ListViewStatus( lvTitleInfo, 4, fb2Desc.TIAuthors );
						MiscListView.ListViewStatus( lvTitleInfo, 5, fb2Desc.TIDate );
						MiscListView.ListViewStatus( lvTitleInfo, 6, fb2Desc.TIKeywords );
						MiscListView.ListViewStatus( lvTitleInfo, 7, fb2Desc.TITranslators );
						MiscListView.ListViewStatus( lvTitleInfo, 8, fb2Desc.TISequences );
						MiscListView.AutoResizeColumns( lvTitleInfo );
						// считываем данные SourceTitleInfo
						MiscListView.ListViewStatus( lvSourceTitleInfo, 0, fb2Desc.STIBookTitle );
						MiscListView.ListViewStatus( lvSourceTitleInfo, 1, GenresWorker.cyrillicGenreNameAndCode( fb2Desc.STIGenres, ref fb2g ) );
						MiscListView.ListViewStatus( lvSourceTitleInfo, 2, fb2Desc.STILang );
						MiscListView.ListViewStatus( lvSourceTitleInfo, 3, fb2Desc.STISrcLang );
						MiscListView.ListViewStatus( lvSourceTitleInfo, 4, fb2Desc.STIAuthors );
						MiscListView.ListViewStatus( lvSourceTitleInfo, 5, fb2Desc.STIDate );
						MiscListView.ListViewStatus( lvSourceTitleInfo, 6, fb2Desc.STIKeywords );
						MiscListView.ListViewStatus( lvSourceTitleInfo, 7, fb2Desc.STITranslators );
						MiscListView.ListViewStatus( lvSourceTitleInfo, 8, fb2Desc.STISequences );
						MiscListView.AutoResizeColumns( lvSourceTitleInfo );
						// считываем данные DocumentInfo
						MiscListView.ListViewStatus( lvDocumentInfo, 0, fb2Desc.DIID );
						MiscListView.ListViewStatus( lvDocumentInfo, 1, fb2Desc.DIVersion );
						MiscListView.ListViewStatus( lvDocumentInfo, 2, fb2Desc.DIFB2Date );
						MiscListView.ListViewStatus( lvDocumentInfo, 3, fb2Desc.DIProgramUsed );
						MiscListView.ListViewStatus( lvDocumentInfo, 4, fb2Desc.DISrcOcr );
						MiscListView.ListViewStatus( lvDocumentInfo, 5, fb2Desc.DISrcUrls );
						MiscListView.ListViewStatus( lvDocumentInfo, 6, fb2Desc.DIFB2Authors );
						MiscListView.AutoResizeColumns( lvDocumentInfo );
						// считываем данные PublishInfo
						MiscListView.ListViewStatus( lvPublishInfo, 0, fb2Desc.PIBookName );
						MiscListView.ListViewStatus( lvPublishInfo, 1, fb2Desc.PIPublisher );
						MiscListView.ListViewStatus( lvPublishInfo, 2, fb2Desc.PICity );
						MiscListView.ListViewStatus( lvPublishInfo, 3, fb2Desc.PIYear );
						MiscListView.ListViewStatus( lvPublishInfo, 4, fb2Desc.PIISBN );
						MiscListView.ListViewStatus( lvPublishInfo, 5, fb2Desc.PISequences );
						MiscListView.AutoResizeColumns( lvPublishInfo );
						// считываем данные CustomInfo
						lvCustomInfo.Items.Clear();
						IList<CustomInfo> lcu = fb2Desc.CICustomInfo;
						if( lcu != null ) {
							foreach( CustomInfo ci in lcu ) {
								ListViewItem lvi = new ListViewItem( ci.InfoType );
								lvi.SubItems.Add( ci.Value );
								lvCustomInfo.Items.Add( lvi );
							}
							MiscListView.AutoResizeColumns( lvCustomInfo );
						}
						// считываем данные History
						rtbHistory.Clear();
						rtbHistory.Text = StringProcessing.getDeleteAllTags( fb2Desc.DIHistory );
						// считываем данные Annotation
						rtbTIAnnotation.Clear();
						rtbTIAnnotation.Text = StringProcessing.getDeleteAllTags( fb2Desc.TIAnnotation );
						rtbSTIAnnotation.Clear();
						rtbSTIAnnotation.Text = StringProcessing.getDeleteAllTags( fb2Desc.STIAnnotation );
						// Валидность файла
						tbValidate.Clear();
						if( SelectedItem.SubItems[(int)ResultViewCollumn.Validate].Text == "Нет" ) {
							string FilePath = Path.Combine( textBoxAddress.Text.Trim(), SelectedItem.Text.Trim() );
							string sResult	= rbtnFB2Librusec.Checked
								? m_fv2Validator.ValidatingFB2LibrusecFile( FilePath )
								: m_fv2Validator.ValidatingFB22File( FilePath );
							tbValidate.Text = "Файл невалидный. Ошибка:";
							tbValidate.AppendText( Environment.NewLine );
							tbValidate.AppendText( Environment.NewLine );
							tbValidate.AppendText( sResult );
						} else if( SelectedItem.SubItems[(int)ResultViewCollumn.Validate].Text == "Да" )
							tbValidate.Text = "Все в порядке - файл валидный!";
						else
							tbValidate.Text = "Валидация файла не производилась.";
						FilesWorker.RemoveDir( m_TempDir );
//				MiscListView.AutoResizeColumns(listViewFB2Files);
					}
				} else
					clearDataFields();
			} catch ( System.Exception e ) {
				MessageBox.Show( "Ошибка при отображении метаданных книги " + fb2Desc.FilePath + "\n" + e.Message );
			}
			
			ConnectListsEventHandlers( true );
		}
		// занесение данных книги в контролы для просмотра
		private void viewBookMetaDataFull( ListViewItem SelectedItem ) {
			string FilePath = Path.Combine( textBoxAddress.Text.Trim(), SelectedItem.Text.Trim() );
			if ( File.Exists( FilePath ) && !SelectedItem.Font.Strikeout ) {
				ZipFB2Worker.getFileFromFB2_FB2Z( ref FilePath, m_TempDir );
				try {
					FB2BookDescription fb2Desc = new FB2BookDescription( FilePath );
					viewBookMetaDataFull( ref fb2Desc, SelectedItem );
				} catch (System.Exception) {
					clearDataFields();
				}
			} else
				clearDataFields();
		}
		
		// удалить помеченные файлы (с удалением элементов списка копий - медленно)
		// Fast = false: с удалением элементов списка копий - медленно. Fast = true: без удаления элементов списка копий - быстро
		private void deleteCheckedFb2( bool Fast ) {
			if( listViewFB2Files.Items.Count > 0 && listViewFB2Files.CheckedItems.Count > 0 ) {
				const string sMessTitle = "SharpFBTools - Удаление книг";
				int nCount = listViewFB2Files.CheckedItems.Count;
				string sMess = "Вы действительно хотите удалить " + nCount.ToString() + " помеченных копии книг?";
				const MessageBoxButtons buttons = MessageBoxButtons.YesNo;
				if( MessageBox.Show( sMess, sMessTitle, buttons, MessageBoxIcon.Question ) != DialogResult.No ) {
					listViewFB2Files.BeginUpdate();
					ConnectListsEventHandlers( false );
					CopyMoveDeleteForm copyMoveDeleteForm = new CopyMoveDeleteForm(
						Fast, BooksWorkMode.DeleteCheckedBooks, textBoxAddress.Text.Trim(), null,
						cboxExistFile.SelectedIndex, listViewFB2Files
					);
					copyMoveDeleteForm.ShowDialog();
					EndWorkMode EndWorkMode = copyMoveDeleteForm.EndMode;
					copyMoveDeleteForm.Dispose();
					ConnectListsEventHandlers( true );
					listViewFB2Files.EndUpdate();
					MessageBox.Show( EndWorkMode.Message, sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				}
			}
		}
		// переместить помеченные файлы в папку-приемник
		// Fast = false: с удалением элементов списка копий - медленно. Fast = true: без удаления элементов списка копий - быстро
		private void moveCheckedFb2To( bool Fast ) {
			if( listViewFB2Files.Items.Count > 0 && listViewFB2Files.CheckedItems.Count > 0 ) {
				string sTarget = FilesWorker.OpenDirDlg( m_TargetDir, fbdScanDir, "Укажите папку-приемник для размешения копий книг:" );
				if( sTarget == null )
					return;

				saveSettingsToXml();
				
				const string MessTitle = "SharpFBTools - Перемещение книг";
				if( textBoxAddress.Text.Trim() == sTarget ) {
					MessageBox.Show( "Папка-приемник файлов совпадает с папкой сканирования!\nРабота прекращена.",
					                MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return;
				}
				listViewFB2Files.BeginUpdate();
				ConnectListsEventHandlers( false );
				CopyMoveDeleteForm copyMoveDeleteForm = new CopyMoveDeleteForm(
					Fast, BooksWorkMode.MoveCheckedBooks, textBoxAddress.Text.Trim(), sTarget,
					cboxExistFile.SelectedIndex, listViewFB2Files
				);
				copyMoveDeleteForm.ShowDialog();
				EndWorkMode EndWorkMode = copyMoveDeleteForm.EndMode;
				copyMoveDeleteForm.Dispose();
				ConnectListsEventHandlers( true );
				listViewFB2Files.EndUpdate();
				MessageBox.Show( EndWorkMode.Message, MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
		}
		private void editFB2InProgram( string ProgPath, string FilePath, string Title ) {
			if( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count != 0 ) {
				ListViewItem SelectedItem = listViewFB2Files.SelectedItems[0];
				if( !File.Exists( FilePath ) ) {
					MessageBox.Show( "Файл: " + FilePath + "\" не найден!", Title, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				} else {
					// правка fb2 и перепаковка fb2 из zip, fbz
					ZipFB2Worker.StartFB2_FBZForEdit( FilePath, ProgPath, Title );
					Cursor.Current = Cursors.WaitCursor;
					// занесение данных в выделенный итем (метаданные книги после правки)
					ZipFB2Worker.getFileFromFB2_FB2Z( ref FilePath, m_TempDir );
					// спиок жанров, в зависимости от схемы Жанров
					IGenresGroup GenresGroup = new GenresGroup();
					IFBGenres fb2g = GenresWorker.genresListOfGenreSheme( rbtnFB2Librusec.Checked, ref GenresGroup );
					try {
						FB2BookDescription bd = new FB2BookDescription( FilePath );
						// занесение данных в выделенный итем (метаданные книги после правки)
						WorksWithBooks.viewBookMetaDataLocal(
							ref bd, SelectedItem, checkBoxNeedValid.Checked, rbtnFB2Librusec.Checked, ref fb2g
						);
						// отображение метаданных после правки книги
						viewBookMetaDataFull( ref bd, SelectedItem );
					} catch ( System.Exception) {
					}
					Cursor.Current = Cursors.Default;
				}
			}
		}
		
		// генерация нового id для выделенной/помеченной книги
		private bool setNewBookID( ListViewItem Item ) {
			string SourceFilePath = Path.Combine( textBoxAddress.Text.Trim(), Item.Text );
			string FilePath = SourceFilePath;
			bool IsFromZip = ZipFB2Worker.getFileFromFB2_FB2Z( ref FilePath, m_TempDir );
			FictionBook fb2 = null;
			try {
				fb2 = new FictionBook( FilePath );
			} catch {
				MessageBox.Show( "Файл \""+FilePath+"\" невозможно открыть для извлечения fb2 метаданных!\nФайл обработан не будет.",
				                "Задание нового ID", MessageBoxButtons.OK, MessageBoxIcon.Error );
				return false;
			}
			
			if( fb2 != null ) {
				// восстанавление раздела description до структуры с необходимыми элементами для валидности
				FB2Corrector fB2Corrector = new FB2Corrector( ref fb2 );
				WorksWithBooks.recoveryFB2Structure( ref fB2Corrector, Item );
				fB2Corrector.setNewID ();
				fB2Corrector.saveToFB2File( FilePath );
				
				if( IsFromZip ) {
					// обработка исправленного файла-архива
					string ArchFile = FilePath + ".zip";
					m_sharpZipLib.ZipFile( FilePath, ArchFile, 9, ICSharpCode.SharpZipLib.Zip.CompressionMethod.Deflated, 4096 );
					if( File.Exists( SourceFilePath ) )
						File.Delete( SourceFilePath );
					File.Move( ArchFile, SourceFilePath );
				}

				// отображение нового id в строке списка
				if( IsFromZip )
					ZipFB2Worker.getFileFromFB2_FB2Z( ref SourceFilePath, m_TempDir );
				// спиок жанров, в зависимости от схемы Жанров
				IGenresGroup GenresGroup = new GenresGroup();
				IFBGenres fb2g = GenresWorker.genresListOfGenreSheme( rbtnFB2Librusec.Checked, ref GenresGroup );
				try {
					FB2BookDescription fb2Corrected = new FB2BookDescription( SourceFilePath );
					WorksWithBooks.viewBookMetaDataLocal(
						ref fb2Corrected, Item, checkBoxNeedValid.Checked, rbtnFB2Librusec.Checked, ref fb2g
					);
					viewBookMetaDataFull( Item );
					FilesWorker.RemoveDir( m_TempDir );
				} catch ( System.Exception ) {
					FilesWorker.RemoveDir( m_TempDir );
					return false;
				}
				return true;
			}
			return false;
		}
		
		// восстановление структуры для всех выделеннеых/помеченных книг
		private bool recoveryDescription( ListViewItem Item ) {
			string SourceFilePath = Path.Combine( textBoxAddress.Text.Trim(), Item.Text );
			string FilePath = SourceFilePath;
			bool IsFromZip = ZipFB2Worker.getFileFromFB2_FB2Z( ref FilePath, m_TempDir );
			FictionBook fb2 = null;
			try {
				fb2 = new FictionBook( FilePath );
			} catch {
				MessageBox.Show( "Файл \""+FilePath+"\" невозможно открыть для извлечения fb2 метаданных!\nФайл обработан не будет.",
				                "Задание нового ID", MessageBoxButtons.OK, MessageBoxIcon.Error );
				return false;
			}
			
			if( fb2 != null ) {
				// восстанавление раздела description до структуры с необходимыми элементами для валидности
				FB2Corrector fB2Corrector = new FB2Corrector( ref fb2 );
				WorksWithBooks.recoveryFB2Structure( ref fB2Corrector, Item );
				fB2Corrector.saveToFB2File( FilePath );
				
				if( IsFromZip ) {
					// обработка исправленного файла-архива
					string ArchFile = FilePath + ".zip";
					m_sharpZipLib.ZipFile( FilePath, ArchFile, 9, ICSharpCode.SharpZipLib.Zip.CompressionMethod.Deflated, 4096 );
					if( File.Exists( SourceFilePath ) )
						File.Delete( SourceFilePath );
					File.Move( ArchFile, SourceFilePath );
				}

				// отображение нового id в строке списка
				if( IsFromZip )
					ZipFB2Worker.getFileFromFB2_FB2Z( ref SourceFilePath, m_TempDir );
				// спиок жанров, в зависимости от схемы Жанров
				IGenresGroup GenresGroup = new GenresGroup();
				IFBGenres fb2g = GenresWorker.genresListOfGenreSheme( rbtnFB2Librusec.Checked, ref GenresGroup );
				try {
					FB2BookDescription fb2Corrected = new FB2BookDescription( SourceFilePath );
					WorksWithBooks.viewBookMetaDataLocal(
						ref fb2Corrected, Item, checkBoxNeedValid.Checked, rbtnFB2Librusec.Checked, ref fb2g
					);
					viewBookMetaDataFull( Item );
					FilesWorker.RemoveDir( m_TempDir );
				} catch ( System.Exception ) {
					FilesWorker.RemoveDir( m_TempDir );
					return false;
				}
				return true;
			}
			return false;
		}
		
		// правка Авторов выделенных/помеченных книг
		private bool editAuthors( bool IsSelectedItems ) {
			bool Ret = false;
			Cursor.Current = Cursors.WaitCursor;
			// создание списка FB2ItemInfo данных для выбранных книг
			IList<FB2ItemInfo> AuthorFB2InfoList = WorksWithBooks.makeFB2InfoList(
				listViewFB2Files, textBoxAddress.Text.Trim(), IsSelectedItems, tsProgressBar
			);
			Cursor.Current = Cursors.Default;
			
			EditAuthorInfoForm editAuthorInfoForm = new EditAuthorInfoForm( ref AuthorFB2InfoList );
			editAuthorInfoForm.ShowDialog();
			
			Cursor.Current = Cursors.WaitCursor;
			if( editAuthorInfoForm.isApplyData() ) {
				// отображаем новые данные Авторов для выбранных книг
				// спиок жанров, в зависимости от схемы Жанров
				IGenresGroup GenresGroup = new GenresGroup();
				IFBGenres fb2g = GenresWorker.genresListOfGenreSheme( rbtnFB2Librusec.Checked, ref GenresGroup );
				foreach( FB2ItemInfo Info in AuthorFB2InfoList ) {
					string FilePath = Info.FilePathSource;
					if( Info.IsFromArhive )
						ZipFB2Worker.getFileFromFB2_FB2Z( ref FilePath, m_TempDir );
					try {
						FB2BookDescription fb2Desc = new FB2BookDescription( FilePath );
						WorksWithBooks.viewBookMetaDataLocal(
							ref fb2Desc, Info.FB2ListViewItem, checkBoxNeedValid.Checked, rbtnFB2Librusec.Checked, ref fb2g
						);
						viewBookMetaDataFull( Info.FB2ListViewItem );
						FilesWorker.RemoveDir( m_TempDir );
					} catch ( System.Exception ) {
						FilesWorker.RemoveDir( m_TempDir );
					}
				}
				Ret = true;
			}
			editAuthorInfoForm.Dispose();
			FilesWorker.RemoveDir( m_TempDir );
//			MiscListView.AutoResizeColumns( listViewFB2Files );
			Cursor.Current = Cursors.Default;
			return Ret;
		}
		// правка Жанров выделенных/помеченных книг
		private bool editGenres( bool IsSelectedItems ) {
			bool Ret = false;
			Cursor.Current = Cursors.WaitCursor;
			// создание списка FB2ItemInfo данных для выбранных книг
			IList<FB2ItemInfo> GenreFB2InfoList = WorksWithBooks.makeFB2InfoList(
				listViewFB2Files, textBoxAddress.Text.Trim(), IsSelectedItems, tsProgressBar
			);
			Cursor.Current = Cursors.Default;
			
			EditGenreInfoForm editGenreInfoForm = new EditGenreInfoForm( ref GenreFB2InfoList );
			editGenreInfoForm.ShowDialog();
			
			Cursor.Current = Cursors.WaitCursor;
			if( editGenreInfoForm.isApplyData() ) {
				// отображаем новые данные Жанров для выбранных книг
				// спиок жанров, в зависимости от схемы Жанров
				IGenresGroup GenresGroup = new GenresGroup();
				IFBGenres fb2g = GenresWorker.genresListOfGenreSheme( rbtnFB2Librusec.Checked, ref GenresGroup );
				foreach( FB2ItemInfo Info in GenreFB2InfoList ) {
					string FilePath = Info.FilePathSource;
					if( Info.IsFromArhive )
						ZipFB2Worker.getFileFromFB2_FB2Z( ref FilePath, m_TempDir );
					try {
						FB2BookDescription fb2Desc = new FB2BookDescription( FilePath );
						WorksWithBooks.viewBookMetaDataLocal(
							ref fb2Desc, Info.FB2ListViewItem, checkBoxNeedValid.Checked, rbtnFB2Librusec.Checked, ref fb2g
						);
						viewBookMetaDataFull( Info.FB2ListViewItem );
						FilesWorker.RemoveDir( m_TempDir );
					} catch ( System.Exception ) {
						FilesWorker.RemoveDir( m_TempDir );
					}
				}
				Ret = true;
			}
			editGenreInfoForm.Dispose();
			FilesWorker.RemoveDir( m_TempDir );
//			MiscListView.AutoResizeColumns( listViewFB2Files );
			Cursor.Current = Cursors.Default;
			return Ret;
		}
		// правка Языка для выделенных/помеченных книг
		private bool editLang( bool IsSelectedItems ) {
			bool Ret = false;
			Cursor.Current = Cursors.WaitCursor;
			// создание списка FB2ItemInfo данных для выбранных книг
			IList<FB2ItemInfo> LangFB2InfoList = WorksWithBooks.makeFB2InfoList(
				listViewFB2Files, textBoxAddress.Text.Trim(), IsSelectedItems, tsProgressBar
			);
			Cursor.Current = Cursors.Default;
			
			EditLangForm editLangForm = new EditLangForm( ref LangFB2InfoList );
			editLangForm.ShowDialog();
			
			Cursor.Current = Cursors.WaitCursor;
			if( editLangForm.isApplyData() ) {
				// отображаем новые данные Авторов для выбранных книг
				// спиок жанров, в зависимости от схемы Жанров
				IGenresGroup GenresGroup = new GenresGroup();
				IFBGenres fb2g = GenresWorker.genresListOfGenreSheme( rbtnFB2Librusec.Checked, ref GenresGroup );
				foreach( FB2ItemInfo Info in LangFB2InfoList ) {
					string FilePath = Info.FilePathSource;
					if( Info.IsFromArhive )
						ZipFB2Worker.getFileFromFB2_FB2Z( ref FilePath, m_TempDir );
					try {
						FB2BookDescription fb2Desc = new FB2BookDescription( FilePath );
						WorksWithBooks.viewBookMetaDataLocal(
							ref fb2Desc, Info.FB2ListViewItem, checkBoxNeedValid.Checked, rbtnFB2Librusec.Checked, ref fb2g
						);
						viewBookMetaDataFull( Info.FB2ListViewItem );
						FilesWorker.RemoveDir( m_TempDir );
					} catch ( System.Exception ) {
						FilesWorker.RemoveDir( m_TempDir );
					}
				}
				Ret = true;
			}
			editLangForm.Dispose();
			FilesWorker.RemoveDir( m_TempDir );
//			MiscListView.AutoResizeColumns( listViewFB2Files );
			Cursor.Current = Cursors.Default;
			return Ret;
		}
		#endregion
		
		#region Обработчики событий
		void ButtonOpenSourceDirClick(object sender, EventArgs e)
		{
			if(FilesWorker.OpenDirDlg( textBoxAddress, fbdScanDir, "Укажите папку для сканирования с fb2-файлами и архивами:" ))
				ButtonGoClick(sender, e);
		}
		void ButtonGoClick(object sender, EventArgs e)
		{
			string s = textBoxAddress.Text.Trim();
			if(s != string.Empty) {
				if ( s.Substring(s.Length-1, 1) != "\\" )
					s = textBoxAddress.Text = textBoxAddress.Text + "\\";
				DirectoryInfo Info = new DirectoryInfo(s);
				if( Info.Exists )
					generateFB2List( Info.FullName );
				else
					MessageBox.Show( "Не удается найти папку " + textBoxAddress.Text + ".\nПроверьте правильность пути.", "Переход по выбранному адресу", MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
		}
		void TextBoxAddressKeyPress(object sender, KeyPressEventArgs e)
		{
			if ( e.KeyChar == (char)Keys.Return ) {
				// отображение папок и/или фалов в заданной папке
				ButtonGoClick( sender, e );
			} else if ( e.KeyChar == '/' || e.KeyChar == '*' || e.KeyChar == '<' || e.KeyChar == '>' || e.KeyChar == '?' || e.KeyChar == '"') {
				e.Handled = true;
			}
		}

		void ListViewFB2FilesColumnClick(object sender, ColumnClickEventArgs e)
		{
			if ( e.Column == m_lvwColumnSorter.SortColumn ) {
				// Изменить сортировку на обратную для выбранного столбца
				if( m_lvwColumnSorter.Order == SortOrder.Ascending )
					m_lvwColumnSorter.Order = SortOrder.Descending;
				else
					m_lvwColumnSorter.Order = SortOrder.Ascending;
			} else {
				// Задать номер столбца для сортировки (по-умолчанию Ascending)
				m_lvwColumnSorter.SortColumn = e.Column;
				m_lvwColumnSorter.Order = SortOrder.Ascending;
			}
			listViewFB2Files.ListViewItemSorter = m_lvwColumnSorter; // перед listViewFB2Files.Sort(); иначе - "тормоза"
			listViewFB2Files.Sort();
		}
		
		// обработка нажатия клавиш на списке папок и файлов
		void ListViewFB2FilesKeyPress(object sender, KeyPressEventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count != 0 ) {
				if ( e.KeyChar == (char)Keys.Return ) {
					ListViewItemType it = (ListViewItemType)listViewFB2Files.SelectedItems[0].Tag;
					if( it.Type == "d" || it.Type == "dUp" ) {
						// переход в выбранную папку
						ListViewFB2FilesDoubleClick(sender, e);
					} else if( it.Type == "f" ) {
						if( listViewFB2Files.SelectedItems.Count == 1 )
							goHandlerWorker( cboxPressEnterForFB2, sender, e );
					}
					
				} else if ( e.KeyChar == (char)Keys.Back ) {
					// переход на каталог выше
					ListViewItemType it = (ListViewItemType)listViewFB2Files.Items[0].Tag;
					textBoxAddress.Text = it.Value;
					generateFB2List( it.Value );
					//TODO index в listViewFB2Files.Items[index].Selected = true; из стека
				}
			}
			e.Handled = true;
		}
		private void goHandlerWorker( ComboBox comboBox, object sender, EventArgs e ) {
			switch ( comboBox.SelectedIndex ) {
				case 0: // Повторная Валидация
					TsmiFileReValidateClick( sender, e );
					break;
				case 1: // Править в текстовом редакторе
					TsmiEditInTextEditorClick( sender, e );
					break;
				case 2: // Править в fb2-редакторе
					TsmiEditInFB2EditorClick( sender, e );
					break;
				case 3: // Просмотр в Читалке
					TsmiViewInReaderClick( sender, e );
					break;
				case 5: // Правка метаданных описания книги
					TsmiEditDescriptionClick( sender, e );
					break;
			}
		}

		// переход в выбранную папку
		void ListViewFB2FilesDoubleClick(object sender, EventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = listViewFB2Files.SelectedItems;
				ListViewItemType it = (ListViewItemType)si[0].Tag;
				if( it.Type == "d" || it.Type == "dUp" ) {
					//TODO index = listViewFB2Files.SelectedItems[0].Index; - в стек
					/*if( it.Type == "d" )
						index = listViewFB2Files.SelectedItems[0].Index;*/
					textBoxAddress.Text = it.Value;
					generateFB2List( it.Value );
					//TODO index в listViewFB2Files.Items[index].Selected = true; из стека
					/*if( it.Type == "dUp" )
						listViewFB2Files.Items[index].Selected = true;*/
				} else if( it.Type == "f" ){
					if( listViewFB2Files.SelectedItems.Count == 1 )
						goHandlerWorker( cboxDblClickForFB2, sender, e );
				}
			}
		}
		void ListViewFB2FilesItemCheck(object sender, ItemCheckEventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count != 0 ) {
				// при двойном клике на папке ".." пометку не ставим
				ConnectListsEventHandlers( false );
				if( e.Index == 0 ) // ".."
					e.NewValue = CheckState.Unchecked;
				ConnectListsEventHandlers( true );
			}
		}
		// пометка/снятие пометки по check на 0-й item - папка ".."
		void ListViewFB2FilesItemChecked(object sender, ItemCheckedEventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 ) {
				if( ((ListViewItemType)e.Item.Tag).Type == "dUp" ) {
					ConnectListsEventHandlers( false );
					if( e.Item.Checked )
						MiscListView.CheckAllListViewItems( listViewFB2Files, true );
					else
						MiscListView.UnCheckAllListViewItems( listViewFB2Files.CheckedItems );;
					ConnectListsEventHandlers( true );
				}
			}
		}
		void ListViewFB2FilesSelectedIndexChanged(object sender, EventArgs e)
		{
			// пропускаем ситуацию, когда курсор переходит от одной строки к другой - нет выбранного item'а
			if( listViewFB2Files.SelectedItems.Count == 1 ) {
				ListViewItem SelectedItem = listViewFB2Files.SelectedItems[0];
				if( SelectedItem != null ) {
					TICoverDPILabel.Text = STICoverDPILabel.Text = "DPI";
					TICoverPixelsLabel.Text = STICoverPixelsLabel.Text = "В пикселах";
					TICoverLenghtLabel.Text = STICoverLenghtLabel.Text = "Размер";
					TICoverListViewButtonPanel.Enabled = STICoverListViewButtonPanel.Enabled = false;

					// защита от двойного срабатывания
					if( m_CurrentResultItem != SelectedItem.Index ) {
						if( ((ListViewItemType)SelectedItem.Tag).Type == "f" )
							viewBookMetaDataFull( SelectedItem );
						else
							clearDataFields();
					}
				}
			}
		}
		
		void RbtnFMFSFB2LibrusecClick(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		void RbtnFMFSFB22Click(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		void CheckBoxNeedValidClick(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		void CboxExistFileSelectedIndexChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		void CboxDblClickForFB2SelectedIndexChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		void CboxPressEnterForFB2SelectedIndexChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		void TextBoxAddressTextChanged(object sender, EventArgs e)
		{
			if ( !string.IsNullOrEmpty( textBoxAddress.Text ) ) {
				if( textBoxAddress.Text.Substring(textBoxAddress.Text.Length - 2, 2) == "\\\\" ) {
					textBoxAddress.Text = textBoxAddress.Text.Remove(textBoxAddress.Text.Length - 1, 1);
					textBoxAddress.SelectionStart = textBoxAddress.Text.Length;
				} else if( textBoxAddress.Text.Substring(textBoxAddress.Text.Length - 2, 2) == "\\." ) {
					textBoxAddress.Text = textBoxAddress.Text.Remove(textBoxAddress.Text.Length-1, 1);
					textBoxAddress.SelectionStart = textBoxAddress.Text.Length;
				} else if( textBoxAddress.Text.Substring( textBoxAddress.Text.Length - 3, 3) == "\\.." ) {
					textBoxAddress.Text = textBoxAddress.Text.Remove( textBoxAddress.Text.Length - 1, 1 );
					textBoxAddress.SelectionStart = textBoxAddress.Text.Length;
				}
			}
			saveSettingsToXml();
		}
		
		void TICoversListViewSelectedIndexChanged(object sender, EventArgs e)
		{
			WorksWithBooks.viewCover( TICoversListView, picBoxTICover, TICoverDPILabel, TICoverPixelsLabel, TICoverLenghtLabel );
		}
		void TiSaveSelectedCoverButtonClick(object sender, EventArgs e)
		{
			// сохранение выделенных обложек на диск
			ImageWorker.saveSelectedCovers( TICoversListView, ref m_DirForSavedCover, "Сохранение обложек на диск", fbdScanDir );
		}

		void STICoversListViewSelectedIndexChanged(object sender, EventArgs e)
		{
			WorksWithBooks.viewCover( STICoversListView, picBoxSTICover, STICoverDPILabel, STICoverPixelsLabel, STICoverLenghtLabel );
		}
		void STISaveSelectedCoverButtonClick(object sender, EventArgs e)
		{
			// сохранение выделенных обложек на диск
			ImageWorker.saveSelectedCovers( STICoversListView, ref m_DirForSavedCover, "Сохранение обложек на диск", fbdScanDir );
		}
		
		// удалить помеченные файлы (с удалением элементов списка копий - медленно)
		void TsmiDeleteCheckedFb2ViewClick(object sender, EventArgs e)
		{
			deleteCheckedFb2( false );
		}
		// удалить помеченные файлы (без удаления элементов списка копий - быстро)
		void TsmiDeleteCheckedFb2FastClick(object sender, EventArgs e)
		{
			deleteCheckedFb2( true );
		}
		// переместить помеченные файлы в папку-приемник (с удалением элементов списка копий - медленно)
		void TsmiMoveCheckedFb2ToViewClick(object sender, EventArgs e)
		{
			moveCheckedFb2To( false );
		}
		// переместить помеченные файлы в папку-приемник (без удаления элементов списка копий - быстро)
		void TsmiMoveCheckedFb2ToFastClick(object sender, EventArgs e)
		{
			moveCheckedFb2To( true );
		}
		// Удаление элементов Списка, файлы которых были удалены с жесткого диска
		void TsmiDeleteAllItemForNonExistFileClick(object sender, EventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 ) {
				const string MessTitle = "SharpFBTools - Удаление элементов Списка \"без файлов\"";
				string sMess = "Вы действительно хотите удалить все элементы Списка, для которых отсутствуют файлы на жестком диске?";
				const MessageBoxButtons buttons = MessageBoxButtons.YesNo;
				if( MessageBox.Show( sMess, MessTitle, buttons, MessageBoxIcon.Question ) != DialogResult.No ) {
					ConnectListsEventHandlers( false );
					MiscListView.removeAllItemForNonExistFile( textBoxAddress.Text.Trim(), listViewFB2Files );
					ConnectListsEventHandlers( true );
					MessageBox.Show( "Удаление элементов Списка \"без файловэ\" завершено.", MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				}
			}
		}
		// удаление всех помеченных элементов Списка (их файлы на жестком диске не удаляются)
		void TsmiDeleteChechedItemsNotDeleteFilesClick(object sender, EventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 ) {
				const string MessTitle = "SharpFBTools - Удаление помеченных элементов Списка";
				string sMess = "Вы действительно хотите удалить все помеченные элементы Списка (их файлы на жестком диске не удаляются)?";
				const MessageBoxButtons buttons = MessageBoxButtons.YesNo;
				if( MessageBox.Show( sMess, MessTitle, buttons, MessageBoxIcon.Question ) != DialogResult.No ) {
					listViewFB2Files.BeginUpdate();
					ConnectListsEventHandlers( false );
					MiscListView.removeChechedItemsNotDeleteFiles( listViewFB2Files );
					// Удаление элементов Списка, файлы которых были удалены с жесткого диска
					MiscListView.removeAllItemForNonExistFile( textBoxAddress.Text.Trim(), listViewFB2Files );
					ConnectListsEventHandlers( true );
					listViewFB2Files.EndUpdate();
					MessageBox.Show( "Удаление помеченных элементов Списка завершено.", MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				}
			}
		}
		// копировать помеченные файлы в папку-приемник
		void TsmiCopyCheckedFb2ToClick(object sender, EventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 && listViewFB2Files.CheckedItems.Count > 0 ) {
				string sTarget = FilesWorker.OpenDirDlg( m_TargetDir, fbdScanDir, "Укажите папку-приемник для размешения книг:" );
				if( sTarget == null )
					return;

				saveSettingsToXml();

				const string MessTitle = "SharpFBTools - Копирование помеченных книг";
				if( textBoxAddress.Text.Trim() == sTarget ) {
					MessageBox.Show( "Папка-приемник файлов совпадает с папкой исходных книг!\nРабота прекращена.",
					                MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return;
				}
				
				ConnectListsEventHandlers( false );
				CopyMoveDeleteForm copyMoveDeleteForm = new CopyMoveDeleteForm(
					false, BooksWorkMode.CopyCheckedBooks, textBoxAddress.Text.Trim(), sTarget,
					cboxExistFile.SelectedIndex, listViewFB2Files
				);
				copyMoveDeleteForm.ShowDialog();
				EndWorkMode EndWorkMode = copyMoveDeleteForm.EndMode;
				copyMoveDeleteForm.Dispose();
				ConnectListsEventHandlers( true );
				MessageBox.Show( EndWorkMode.Message, MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
		}
		// Пометить всё выделенное
		void TsmiCheckedAllInGroupClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			MiscListView.ChekAllSelectedItems(listViewFB2Files, true);
			ConnectListsEventHandlers( true );
			listViewFB2Files.Focus();
		}
		// Снять пометки со всего выделенного
		void TsmiUnCheckedAllSelectedClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			MiscListView.ChekAllSelectedItems(listViewFB2Files, false);
			ConnectListsEventHandlers( true );
			listViewFB2Files.Focus();
		}
		// Пометить все файлы
		void TsmiCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			MiscListView.CheckAllFiles(listViewFB2Files, true);
			ConnectListsEventHandlers( true );
		}
		// Снять пометки со всех файлов
		void TsmiUnCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			MiscListView.UnCheckAllFiles(listViewFB2Files);
			ConnectListsEventHandlers( true );
		}
		// запустить файл в fb2-читалке (Просмотр)
		void TsmiViewInReaderClick(object sender, EventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count != 0 ) {
				// читаем путь к читалке из настроек
				string sFBReaderPath = Settings.Settings.ReadFBReaderPath();
				const string sTitle = "SharpFBTools - Открытие папки для файла";
				if( !File.Exists( sFBReaderPath ) ) {
					MessageBox.Show( "Не могу найти Читалку \""+sFBReaderPath+"\"!\nПроверьте, правильно ли задан путь в Настройках.", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				ListView.SelectedListViewItemCollection si = listViewFB2Files.SelectedItems;
				string sFilePath = Path.Combine( textBoxAddress.Text.Trim(), si[0].SubItems[0].Text.Split('/')[0] );
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: \""+sFilePath+"\" не найден!", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				FilesWorker.StartFile( sFBReaderPath, sFilePath );
			}
		}
		// редактировать выделенный файл в fb2-редакторе
		void TsmiEditInFB2EditorClick(object sender, EventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count != 0 ) {
				// читаем путь к FBE из настроек
				string FBEPath = Settings.Settings.ReadFBEPath();
				const string Title = "SharpFBTools - Открытие файла в fb2-редакторе";
				if( !File.Exists( FBEPath ) ) {
					MessageBox.Show( "Не могу найти fb2 редактор \""+FBEPath+"\"!\nПроверьте, правильно ли задан путь в Настройках.", Title, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				editFB2InProgram(
					FBEPath, Path.Combine( textBoxAddress.Text.Trim(), listViewFB2Files.SelectedItems[0].SubItems[0].Text.Trim() ), Title
				);
			}
		}
		// редактировать выделенный файл в текстовом редакторе
		void TsmiEditInTextEditorClick(object sender, EventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count != 0 ) {
				// читаем путь к текстовому редактору из настроек
				string TextEditorPath = Settings.Settings.ReadTextFB2EPath();
				const string Title = "SharpFBTools - Открытие файла в текстовом редакторе";
				if( !File.Exists( TextEditorPath ) ) {
					MessageBox.Show( "Не могу найти текстовый редактор \""+TextEditorPath+"\"!\nПроверьте, правильно ли задан путь в Настройках.", Title, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				editFB2InProgram(
					TextEditorPath, Path.Combine( textBoxAddress.Text.Trim(), listViewFB2Files.SelectedItems[0].SubItems[0].Text.Trim() ), Title
				);
			}
		}
		// комплексное редактирование метаданных в специальном диалоге
		void TsmiEditDescriptionClick(object sender, EventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count != 0 ) {
				ListViewItem SelectedItem = listViewFB2Files.SelectedItems[0];
				if( SelectedItem != null ) {
					string SourceFilePath = Path.Combine( textBoxAddress.Text.Trim(), SelectedItem.Text );
					string FilePath = SourceFilePath;
					bool IsFromArhive = ZipFB2Worker.getFileFromFB2_FB2Z( ref FilePath, m_TempDir );
					if( File.Exists( FilePath ) ) {
						FictionBook fb2 = null;
						try {
							fb2 = new FictionBook( FilePath );
						} catch (System.Exception /*m*/) {
							MessageBox.Show( "Файл \""+FilePath+"\" невозможно открыть для извлечения fb2 метаданных!\nФайл обработан не будет.",
							                "Задание нового ID", MessageBoxButtons.OK, MessageBoxIcon.Error );
							return;
						}
						EditDescriptionForm editDescriptionForm = new EditDescriptionForm( fb2 );
						editDescriptionForm.ShowDialog();
						Cursor.Current = Cursors.WaitCursor;
						if( editDescriptionForm.isApplyData() ) {
							editDescriptionForm.getFB2XmlDocument().Save( FilePath );
							if( IsFromArhive ) {
								// обработка исправленного файла-архива
								string ArchFile = FilePath + ".zip";
								m_sharpZipLib.ZipFile( FilePath, ArchFile, 9, ICSharpCode.SharpZipLib.Zip.CompressionMethod.Deflated, 4096 );
								if( File.Exists( SourceFilePath ) )
									File.Delete( SourceFilePath );
								File.Move( ArchFile, SourceFilePath );
							}
							// спиок жанров, в зависимости от схемы Жанров
							IGenresGroup GenresGroup = new GenresGroup();
							IFBGenres fb2g = GenresWorker.genresListOfGenreSheme( rbtnFB2Librusec.Checked, ref GenresGroup );
							try {
								// занесение данных в выделенный итем (метаданные книги после правки)
								FB2BookDescription bd = new FB2BookDescription( FilePath );
								WorksWithBooks.viewBookMetaDataLocal(
									ref bd, SelectedItem, checkBoxNeedValid.Checked, rbtnFB2Librusec.Checked, ref fb2g
								);
								// отображение метаданных после правки книги
								viewBookMetaDataFull( ref bd, SelectedItem );
							} catch ( System.Exception /*m*/ ) {
							} finally {
								FilesWorker.RemoveDir( m_TempDir );
							}
						}
						editDescriptionForm.Dispose();
						Cursor.Current = Cursors.Default;
					}
				}
			}
		}
		// правка Жанров помеченных книг
		void TsmiSetGenresClick(object sender, EventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 && listViewFB2Files.CheckedItems.Count > 0 ) {
				if( WorksWithBooks.chechedDirExsist( listViewFB2Files.CheckedItems ) ) {
					MessageBox.Show( "Обработка Жанров возможно только для файлов (папки и вложенные в них файлы не обрабатываются).\nСнимите пометки с папок.",
					                "Правка метаданных Жанров для помеченных книг", MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				editGenres( false );
			}
		}
		// правка Жанров выделенных книг
		void TsmiSetGenresForSelectedBooksClick(object sender, EventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count > 0 ) {
				if( WorksWithBooks.chechedDirExsist( listViewFB2Files.SelectedItems ) ) {
					MessageBox.Show( "Обработка Жанров возможно только для файлов (папки и вложенные в них файлы не обрабатываются).\nСнимите выделение с папок.",
					                "Правка метаданных Жанров для выделенных книг", MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				editGenres( true );
			}
		}
		// правка Авторов помеченных книг
		void TsmiSetAuthorsClick(object sender, EventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 && listViewFB2Files.CheckedItems.Count > 0 ) {
				if( WorksWithBooks.chechedDirExsist( listViewFB2Files.CheckedItems ) ) {
					MessageBox.Show( "Обработка Авторов возможно только для файлов (папки и вложенные в них файлы не обрабатываются).\nСнимите пометки с папок.",
					                "Правка метаданных Авторов для помеченных книг", MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				editAuthors( false );
			}
		}
		// правка Авторов выделенных книг
		void TsmiSetAuthorsForSelectedBooksClick(object sender, EventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count > 0 ) {
				if( WorksWithBooks.chechedDirExsist( listViewFB2Files.SelectedItems ) ) {
					MessageBox.Show( "Обработка Авторов возможно только для файлов (папки и вложенные в них файлы не обрабатываются).\nСнимите выделение с папок.",
					                "Правка метаданных Авторов для выделенных книг", MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				editAuthors( true );
			}
		}
		// Повторная Проверка выбранного fb2-файла (Валидация)
		void TsmiFileReValidateClick(object sender, EventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count != 0 &&
			   WorksWithBooks.isFileItem( listViewFB2Files.SelectedItems[0] ) ) {
				DateTime dtStart = DateTime.Now;
				ListViewItem SelectedItem = listViewFB2Files.SelectedItems[0];
				string SelectedItemText = SelectedItem.SubItems[(int)ResultViewCollumn.Path].Text;
				string FilePath = Path.Combine( textBoxAddress.Text.Trim(), SelectedItemText );
				if( !File.Exists( FilePath ) ) {
					MessageBox.Show( "Файл: \""+FilePath+"\" не найден!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				MessageBoxIcon mbi = MessageBoxIcon.Information;
				string Msg		= string.Empty;
				string ErrorMsg	= "СООБЩЕНИЕ ОБ ОШИБКЕ:";
				string OkMsg	= "ОШИБОК НЕТ - ФАЙЛ ВАЛИДЕН";
				Msg = ZipFB2Worker.IsValid( FilePath, rbtnFB2Librusec.Checked );
				
				// отобразить данные в детализации
				if( ((ListViewItemType)SelectedItem.Tag).Type == "f" )
					viewBookMetaDataFull( SelectedItem );
				
				if ( Msg == string.Empty ) {
					// файл валидный
					mbi = MessageBoxIcon.Information;
					ErrorMsg = OkMsg;
					SelectedItem.SubItems[(int)ResultViewCollumn.Validate].Text = "Да";
					SelectedItem.ForeColor = Path.GetExtension(FilePath).ToLower() == ".fb2"
						? Color.FromName( "WindowText" )
						: Colors.ZipFB2ForeColor;
					tbValidate.Text = "Все в порядке - файл валидный!";
				} else {
					// файл не валидный
					mbi = MessageBoxIcon.Error;
					SelectedItem.SubItems[(int)ResultViewCollumn.Validate].Text = "Нет";
					SelectedItem.ForeColor = Colors.FB2NotValidForeColor;
					tbValidate.Text = "Файл невалидный. Ошибка:";
					tbValidate.AppendText( Environment.NewLine );
					tbValidate.AppendText( Environment.NewLine );
					tbValidate.AppendText( Msg );
				}
				
				DateTime dtEnd = DateTime.Now;
				string sTime = dtEnd.Subtract( dtStart ).ToString() + " (час.:мин.:сек.)";
				FilesWorker.RemoveDir( m_TempDir );
				MessageBox.Show( "Проверка выделенного файла на соответствие FictionBook.xsd схеме завершена.\nЗатрачено времени: "+sTime+"\n\nФайл: \""+FilePath+"\"\n\n"+ErrorMsg+"\n"+Msg, "SharpFBTools - "+ErrorMsg, MessageBoxButtons.OK, mbi );
			}
		}
		// Проверить все помеченные книги на валидность
		void TsmiAllCheckedFilesReValidateClick(object sender, EventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 ) {
				ConnectListsEventHandlers( false );
				Core.Corrector.ValidatorForm validatorForm = new Core.Corrector.ValidatorForm(
					BooksValidateMode.CheckedBooks, rbtnFB2Librusec.Checked,
					listViewFB2Files, textBoxAddress.Text.Trim()
				);
				validatorForm.ShowDialog();
				EndWorkMode EndWorkMode = validatorForm.EndMode;
				validatorForm.Dispose();
				// отобразить данные в детализации
				if ( listViewFB2Files.SelectedItems.Count > 0 )
					viewBookMetaDataFull( listViewFB2Files.SelectedItems[0] );
				ConnectListsEventHandlers( true );
				MessageBox.Show( EndWorkMode.Message, "SharpFBTools - Валидация всех помеченных книг", MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
		}
		// Проверить все выделенные книги на валидность
		void TsmiAllSelectedFilesReValidateClick(object sender, EventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 ) {
				ConnectListsEventHandlers( false );
				Core.Corrector.ValidatorForm validatorForm = new Core.Corrector.ValidatorForm(
					BooksValidateMode.SelectedBooks, rbtnFB2Librusec.Checked,
					listViewFB2Files, textBoxAddress.Text.Trim()
				);
				validatorForm.ShowDialog();
				EndWorkMode EndWorkMode = validatorForm.EndMode;
				validatorForm.Dispose();
				// отобразить данные в детализации
				if ( listViewFB2Files.SelectedItems.Count > 0 )
					viewBookMetaDataFull( listViewFB2Files.SelectedItems[0] );
				ConnectListsEventHandlers( true );
				MessageBox.Show( EndWorkMode.Message, "SharpFBTools - Валидация всех выделенных книг", MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
		}
		// Проверить все книги на валидность
		void TsmiAllFilesReValidateClick(object sender, EventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 ) {
				ConnectListsEventHandlers( false );
				Core.Corrector.ValidatorForm validatorForm = new Core.Corrector.ValidatorForm(
					BooksValidateMode.AllBooks, rbtnFB2Librusec.Checked,
					listViewFB2Files, textBoxAddress.Text.Trim()
				);
				validatorForm.ShowDialog();
				EndWorkMode EndWorkMode = validatorForm.EndMode;
				validatorForm.Dispose();
				// отобразить данные в детализации
				if ( listViewFB2Files.SelectedItems.Count > 0 )
					viewBookMetaDataFull( listViewFB2Files.SelectedItems[0] );
				ConnectListsEventHandlers( true );
				MessageBox.Show( EndWorkMode.Message, "SharpFBTools - Валидация всех книг", MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
		}
		// diff - две помеченные fb2-книги
		void TsmiDiffFB2Click(object sender, EventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count != 0 ) {
				ListViewItem SelectedItem = listViewFB2Files.SelectedItems[0];
				// проверка на наличие diff-программы
				const string sDiffTitle = "SharpFBTools - diff";
				string sDiffPath = Settings.Settings.ReadDiffPath();
				
				if( sDiffPath.Trim().Length == 0 ) {
					MessageBox.Show( "В Настройках не указан путь к установленной diff-программе визуального сравнения файлов!\nУкажите путь к ней в Настройках.\nРабота остановлена!",
					                sDiffTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return;
				} else {
					if( !File.Exists( sDiffPath ) ) {
						MessageBox.Show( "Не найден файл diff-программы визуального сравнения файлов \""+sDiffPath+"\"!\nУкажите путь к ней в Настройках.\nРабота остановлена!",
						                sDiffTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
						return;
					}
				}

				ListView.CheckedListViewItemCollection ChekedItems = listViewFB2Files.CheckedItems;
				if( ChekedItems.Count != 2) {
					MessageBox.Show( "Сравнивать можно только 2 помеченных книгу в одной группе!",
					                sDiffTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return;
				}
				
				// запускаем инструмент сравнения
				string sFilesNotExists = string.Empty;
				string FilePath1 = Path.Combine( textBoxAddress.Text.Trim(), ChekedItems[0].Text );
				string FilePath2 = Path.Combine( textBoxAddress.Text.Trim(), ChekedItems[1].Text );
				if( !File.Exists( FilePath1 ) ) {
					sFilesNotExists += FilePath1; sFilesNotExists += "\n";
				}
				if( !File.Exists( FilePath2 ) ) {
					sFilesNotExists += FilePath2; sFilesNotExists += "\n";
				}

				if( sFilesNotExists != string.Empty )
					MessageBox.Show( "Не найден(ы) файл(ы) для сравнения:\n" + sFilesNotExists + "\nРабота остановлена!",
					                sDiffTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				else {
					ZipFB2Worker.DiffFB2( sDiffPath, FilePath1, FilePath2, m_TempDir + "\\1", m_TempDir + "\\2" );
					// спиок жанров, в зависимости от схемы Жанров
					IGenresGroup GenresGroup = new GenresGroup();
					IFBGenres fb2g = GenresWorker.genresListOfGenreSheme( rbtnFB2Librusec.Checked, ref GenresGroup );
					Cursor.Current = Cursors.WaitCursor;
					// занесение данных в выделенный итем (метаданные книги после правки)
					string FilePath = FilePath1;
					ZipFB2Worker.getFileFromFB2_FB2Z( ref FilePath, m_TempDir );
					FB2BookDescription bd = null;
					try {
						bd = new FB2BookDescription( FilePath );
						// занесение данных в выделенный итем (метаданные книги после правки)
						WorksWithBooks.viewBookMetaDataLocal(
							ref bd, ChekedItems[0], checkBoxNeedValid.Checked, rbtnFB2Librusec.Checked, ref fb2g
						);
					} catch ( System.Exception ) {
					}
					
					FilePath = FilePath2;
					ZipFB2Worker.getFileFromFB2_FB2Z( ref FilePath, m_TempDir );
					try {
						bd = new FB2BookDescription( FilePath );
						WorksWithBooks.viewBookMetaDataLocal(
							ref bd, ChekedItems[1], checkBoxNeedValid.Checked, rbtnFB2Librusec.Checked, ref fb2g
						);
						// отображение метаданных после правки книги
						viewBookMetaDataFull( SelectedItem );
					} catch ( System.Exception ) {
					}
					Cursor.Current = Cursors.Default;
				}
			}
		}
		// сохранение обрабатываемого списка fb2 файлов в xml файл
		void SaveFB2FilesToListButtonClick(object sender, EventArgs e)
		{
			const string MessTitle = "SharpFBTools - Сохранение списка fb2 книг";
			if( listViewFB2Files.Items.Count == 0 ) {
				MessageBox.Show( "Нет ни одной fb2 книги!", MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			sfdList.Title = "Укажите название файла списка fb2 скниг:";
			sfdList.Filter = "SharpFBTools Файлы метаредактора книг (*.editor_elb)|*.editor_elb";
			sfdList.FileName = string.Empty;
			sfdList.InitialDirectory = Settings.Settings.ProgDir;
			DialogResult result = sfdList.ShowDialog();
			if( result == DialogResult.OK ) {
				ConnectListsEventHandlers( false );
				// удаление всех элементов Списка, для которых отсутствуют файлы на жестком диске (защита от сохранения пустых Групп)
				MiscListView.removeAllItemForNonExistFile( textBoxAddress.Text.Trim(), listViewFB2Files );
				Environment.CurrentDirectory = Settings.Settings.ProgDir;
				Core.Corrector.BooksListWorkerForm fileWorkerForm = new Core.Corrector.BooksListWorkerForm(
					BooksWorkMode.SaveFB2List, sfdList.FileName,
					listViewFB2Files, textBoxAddress, checkBoxNeedValid, rbtnFB2Librusec,
					listViewFB2Files.SelectedItems.Count >= 1 ? listViewFB2Files.SelectedItems[0].Index : -1
				);
				fileWorkerForm.ShowDialog();
				EndWorkMode EndWorkMode = fileWorkerForm.EndMode;
				fileWorkerForm.Dispose();
				ConnectListsEventHandlers( true );
				MessageBox.Show( EndWorkMode.Message, MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
		}
		// загрузка списка обрабатываемых книг
		void OpenFB2FilesListButtonClick(object sender, EventArgs e)
		{
			sfdLoadList.InitialDirectory = Settings.Settings.ProgDir;
			sfdLoadList.Title 		= "Загрузка Списка обрабатываемых книг:";
			sfdLoadList.Filter		= "SharpFBTools Файлы метаредактора книг (*.editor_elb)|*.editor_elb";
			sfdLoadList.FileName	= string.Empty;
			string FromXML = string.Empty;
			DialogResult result = sfdLoadList.ShowDialog();
			if( result == DialogResult.OK ) {
				const string MessTitle = "SharpFBTools - Загрузка редактируемых копий книг";
				FromXML = sfdLoadList.FileName;
				// установка режима поиска
				if( !File.Exists( FromXML ) ) {
					MessageBox.Show( "Не найден файл списка редактируемых fb2 книг: \""+FromXML+"\"!", MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return;
				}
				XmlReaderSettings data = new XmlReaderSettings();
				data.IgnoreWhitespace = true;
				try {
					// отключаем обработчики событий для lvResult (убираем "тормоза")
					ConnectListsEventHandlers( false );
					listViewFB2Files.BeginUpdate();
					// очистка временной папки
					FilesWorker.RemoveDir( Settings.Settings.TempDir );
					listViewFB2Files.Items.Clear();
					Environment.CurrentDirectory = Settings.Settings.ProgDir;
					Core.Corrector.BooksListWorkerForm fileWorkerForm = new Core.Corrector.BooksListWorkerForm(
						BooksWorkMode.LoadFB2List, FromXML, listViewFB2Files, textBoxAddress, checkBoxNeedValid, rbtnFB2Librusec, -1
					);
					fileWorkerForm.ShowDialog();
					EndWorkMode EndWorkMode = fileWorkerForm.EndMode;
					fileWorkerForm.Dispose();
					listViewFB2Files.EndUpdate();
					ConnectListsEventHandlers( true );
					// отображение метаданных
					viewBookMetaDataFull( listViewFB2Files.SelectedItems[0] );
					MessageBox.Show( EndWorkMode.Message, MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					listViewFB2Files.Focus();
				} catch {
					listViewFB2Files.EndUpdate();
					ConnectListsEventHandlers( true );
					MessageBox.Show( "Поврежден файл списка редактируемых fb2 книг: \""+FromXML+"\"!", MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				}
			}
		}
		// генерация нового id для всех выделенных книг
		void TsmiSetNewIDForAllSelectedBooksClick(object sender, EventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count > 0 ) {
				ListView.SelectedListViewItemCollection SelectedItems = listViewFB2Files.SelectedItems;
				const string Message = "Вы действительно хотите измменить id всех выделенных книг на новые?";
				const string MessTitle = "SharpFBTools - Генерация новых ID для выделенных книг";
				MessageBoxButtons Buttons = MessageBoxButtons.YesNo;
				DialogResult Result = MessageBox.Show( Message, MessTitle, Buttons);
				if( Result == DialogResult.Yes ) {
					Cursor.Current = Cursors.WaitCursor;
					foreach( ListViewItem SelectedItem in SelectedItems ) {
						if( WorksWithBooks.isFileItem( SelectedItem ) )
							setNewBookID( SelectedItem );
					}
//					MiscListView.AutoResizeColumns( listViewFB2Files );
					Cursor.Current = Cursors.Default;
				}
			}
		}
		// генерация нового id для всех помеченных книг
		void TsmiSetNewIDForAllCheckedBooksClick(object sender, EventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 && listViewFB2Files.CheckedItems.Count > 0 ) {
				ListView.CheckedListViewItemCollection CheckedItems = listViewFB2Files.CheckedItems;
				const string Message = "Вы действительно хотите измменить id всех помеченных книг на новые?";
				const string MessTitle = "SharpFBTools - Генерация новых ID для помеченных книг";
				MessageBoxButtons Buttons = MessageBoxButtons.YesNo;
				DialogResult Result = MessageBox.Show( Message, MessTitle, Buttons);
				if( Result == DialogResult.Yes ) {
					Cursor.Current = Cursors.WaitCursor;
					foreach( ListViewItem CheckedItem in CheckedItems ) {
						if( WorksWithBooks.isFileItem( CheckedItem ) )
							setNewBookID( CheckedItem );
					}
//					MiscListView.AutoResizeColumns( listViewFB2Files );
					Cursor.Current = Cursors.Default;
				}
			}
		}
		
		// правка Языка для выделенных книг
		void TsmiSetLangForSelectedBooksClick(object sender, EventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count > 0 ) {
				if( WorksWithBooks.chechedDirExsist( listViewFB2Files.SelectedItems ) ) {
					MessageBox.Show( "Обработка Языка возможно только для файлов (папки и вложенные в них файлы не обрабатываются).\nСнимите выделение с папок.",
					                "Правка метаданных Авторов для выделенных книг", MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				editLang( true );
			}
		}
		// правка Языка для помеченных книг
		void TsmiSetLangForCheckedBooksClick(object sender, EventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 && listViewFB2Files.CheckedItems.Count > 0 ) {
				if( WorksWithBooks.chechedDirExsist( listViewFB2Files.CheckedItems ) ) {
					MessageBox.Show( "Обработка Языка возможно только для файлов (папки и вложенные в них файлы не обрабатываются).\nСнимите пометки с папок.",
					                "Правка метаданных Авторов для помеченных книг", MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				editLang( false );
			}
		}
		// правка Названия книги для выделенной книги
		void TsmiEditBookNameClick(object sender, EventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count != 0 ) {
				string SourceFilePath = Path.Combine( textBoxAddress.Text.Trim(), listViewFB2Files.SelectedItems[0].Text );
				string FilePath = SourceFilePath;
				bool IsFromZip = ZipFB2Worker.getFileFromFB2_FB2Z( ref FilePath, m_TempDir );
				FictionBook fb2 = null;
				try {
					fb2 = new FictionBook( FilePath );
				} catch {
					MessageBox.Show( "Файл \""+FilePath+"\" невозможно открыть для извлечения fb2 метаданных!\nФайл обработан не будет.",
					                "Задание нового Названия книги", MessageBoxButtons.OK, MessageBoxIcon.Error );
					return;
				}
				
				if( fb2 != null ) {
					string BookTitleNew = fb2.TIBookTitle != null ? fb2.TIBookTitle.Value : "Новое название книги";
					if ( WorksWithBooks.InputBox( "Правка названия книги", "Новое название книги:", ref BookTitleNew ) == DialogResult.OK) {
						// восстанавление раздела description до структуры с необходимыми элементами для валидности
						FB2Corrector fB2Corrector = new FB2Corrector( ref fb2 );
						WorksWithBooks.recoveryFB2Structure( ref fB2Corrector, listViewFB2Files.SelectedItems[0]  );
						fB2Corrector.setNewBookTitle( BookTitleNew );
						fB2Corrector.saveToFB2File( FilePath );
						
						if( IsFromZip ) {
							// обработка исправленного файла-архива
							string ArchFile = FilePath + ".zip";
							m_sharpZipLib.ZipFile( FilePath, ArchFile, 9, ICSharpCode.SharpZipLib.Zip.CompressionMethod.Deflated, 4096 );
							if( File.Exists( SourceFilePath ) )
								File.Delete( SourceFilePath );
							File.Move( ArchFile, SourceFilePath );
						}

						// отображение нового названия книги в строке списка
						if( IsFromZip )
							ZipFB2Worker.getFileFromFB2_FB2Z( ref SourceFilePath, m_TempDir );
						// спиок жанров, в зависимости от схемы Жанров
						IGenresGroup GenresGroup = new GenresGroup();
						IFBGenres fb2g = GenresWorker.genresListOfGenreSheme( rbtnFB2Librusec.Checked, ref GenresGroup );
						try {
							FB2BookDescription fb2Corrected = new FB2BookDescription( SourceFilePath );
							WorksWithBooks.viewBookMetaDataLocal(
								ref fb2Corrected, listViewFB2Files.SelectedItems[0],
								checkBoxNeedValid.Checked, rbtnFB2Librusec.Checked, ref fb2g
							);
							viewBookMetaDataFull( listViewFB2Files.SelectedItems[0] );
							FilesWorker.RemoveDir( m_TempDir );
						} catch ( System.Exception ) {
							return;
						}
					}
				}
			}
		}

		// восстановление структуры для всех выделеннеых книг
		void TsmiRecoveryDescriptionForAllSelectedBooksClick(object sender, EventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count > 0 ) {
				ListView.SelectedListViewItemCollection SelectedItems = listViewFB2Files.SelectedItems;
				const string Message = "Вы действительно хотите восстановить структуру раздела description для всех выделенных книг?";
				const string MessTitle = "SharpFBTools - Восстановление структуры раздела description для выделенных книг";
				MessageBoxButtons Buttons = MessageBoxButtons.YesNo;
				DialogResult Result = MessageBox.Show( Message, MessTitle, Buttons);
				if( Result == DialogResult.Yes ) {
					Cursor.Current = Cursors.WaitCursor;
					foreach( ListViewItem SelectedItem in SelectedItems ) {
						if( WorksWithBooks.isFileItem( SelectedItem ) )
							recoveryDescription( SelectedItem );
					}
//					MiscListView.AutoResizeColumns( listViewFB2Files );
					Cursor.Current = Cursors.Default;
				}
			}
		}
		// восстановление структуры для всех помеченных книг
		void TsmiRecoveryDescriptionForAllCheckedBooksClick(object sender, EventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 && listViewFB2Files.CheckedItems.Count > 0 ) {
				ListView.CheckedListViewItemCollection CheckedItems = listViewFB2Files.CheckedItems;
				const string Message = "Вы действительно хотите восстановить структуру раздела description для всех помеченных книг на новые?";
				const string MessTitle = "SharpFBTools - Восстановление структуры раздела description для помеченных книг";
				MessageBoxButtons Buttons = MessageBoxButtons.YesNo;
				DialogResult Result = MessageBox.Show( Message, MessTitle, Buttons);
				if( Result == DialogResult.Yes ) {
					Cursor.Current = Cursors.WaitCursor;
					foreach( ListViewItem CheckedItem in CheckedItems ) {
						if( WorksWithBooks.isFileItem( CheckedItem ) )
							recoveryDescription( CheckedItem );
					}
//					MiscListView.AutoResizeColumns( listViewFB2Files );
					Cursor.Current = Cursors.Default;
				}
			}
		}
		
		// авторазмер ширины колонок Проводника
		void TsmiColumnsExplorerAutoReizeClick(object sender, EventArgs e)
		{
			MiscListView.AutoResizeColumns( listViewFB2Files );
		}
		
		#endregion
	}
}
