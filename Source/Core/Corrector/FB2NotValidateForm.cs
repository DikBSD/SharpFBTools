/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 19.02.2016
 * Время: 14:38
 * 
 * License: GPL 2.1
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;

using System.IO;
using System.Linq;
using System.Xml.Linq;

using Core.Common;
using Core.FB2.Genres;

using FB2Validator		= Core.FB2Parser.FB2Validator;
using FilesWorker		= Core.Common.FilesWorker;
using SharpZipLibWorker = Core.Common.SharpZipLibWorker;
using EndWorkMode 		= Core.Common.EndWorkMode;
using MiscListView		= Core.Common.MiscListView;
using Colors			= Core.Common.Colors;

// enums
using EndWorkModeEnum		= Core.Common.Enums.EndWorkModeEnum;
using ResultViewCollumnEnum	= Core.Common.Enums.ResultViewCollumnEnum;
using BooksValidateModeEnum = Core.Common.Enums.BooksValidateModeEnum;

namespace Core.Corrector
{
	/// <summary>
	/// Поиск всех невалидных файлов в заданной папке
	/// </summary>
	public partial class FB2NotValidateForm : Form
	{
		#region Закрытые данные класса
		private const string m_sMessTitle	= "SharpFBTools - Поиск невалидных fb2 файлов";
		private readonly FB2Validator		m_fv2Validator	= new FB2Validator();
		private readonly FB2UnionGenres		m_fb2Genres		= new FB2UnionGenres();
		private readonly SharpZipLibWorker	m_sharpZipLib	= new SharpZipLibWorker();
		
		private readonly ListView	m_listViewFB2Files	= new ListView();
		private readonly EndWorkMode m_EndMode			= new EndWorkMode();
		private readonly string		m_TempDir			= Settings.Settings.TempDirPath;
		private readonly string		m_SourceDir			= string.Empty;
		private readonly bool		m_autoResizeColumns	= false;
		private readonly string		m_fromXmlPath		= null;	// null - полный поиск; Если указан Путь - возобновление поиска из xml
		private bool				m_StopToSave		= false;// true, если остановка с сохранением необработанного списка книг в файл.
		
		private int m_AllDirs = 0;
		private long m_AllFiles = 0;
		private List<string> m_FilesList	= new List<string>();
		private BackgroundWorker m_bw		= new BackgroundWorker();
		
		private DateTime m_dtStart = DateTime.Now;
		#endregion
		
		public FB2NotValidateForm( string fromXmlPath, ListView listView, string SourceDir, bool AutoResizeColumns )
		{
			InitializeComponent();
			
			m_autoResizeColumns = AutoResizeColumns;
			m_listViewFB2Files	= listView;
			m_SourceDir			= SourceDir;
			
			InitializeBackgroundWorker();
			
			m_fromXmlPath = fromXmlPath; // путь к xml файлу для возобновления поиска копий fb2 книг
			// Запуск процесса DoWork от RunWorker
			if ( !m_bw.IsBusy )
				m_bw.RunWorkerAsync(); // если не занят, то запустить процесс
		}
		
		// =============================================================================================
		// 								ОТКРЫТЫЕ СВОЙСТВА
		// =============================================================================================
		#region Открытые свойства
		public virtual EndWorkMode EndMode {
			get { return m_EndMode; }
		}
		#endregion
		
		// =============================================================================================
		// 								ОТКРЫТЫЕ МЕТОДЫ
		// =============================================================================================
		#region Открытые методы
		public bool IsStopToXmlClicked() {
			return m_StopToSave;
		}
		
		public string getSourceDirFromRenew() {
			return m_SourceDir;
		}
		#endregion
		
		// =============================================================================================
		//			BackgroundWorker: Сепрерывный поиск невалидных файлов
		// =============================================================================================
		#region BackgroundWorker: Сепрерывный поиск невалидных файлов
		private void InitializeBackgroundWorker() {
			// Инициализация перед использование BackgroundWorker
			m_bw.WorkerReportsProgress		= true; // Позволить выводить прогресс процесса
			m_bw.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
			m_bw.DoWork 			+= new DoWorkEventHandler( bw_DoWork );
			m_bw.ProgressChanged 	+= new ProgressChangedEventHandler( bw_ProgressChanged );
			m_bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler( bw_RunWorkerCompleted );
		}
		
		// поиск одинаковых fb2-файлов
		private void bw_DoWork( object sender, DoWorkEventArgs e ) {
			m_dtStart = DateTime.Now;
			BackgroundWorker worker = sender as BackgroundWorker;
			ControlPanel.Enabled = false;
			m_FilesList.Clear();
			
			if ( string.IsNullOrEmpty( m_fromXmlPath ) ) {
				/* непрерывный поиск */
				StatusLabel.Text += "Создание списка файлов для поиска копий fb2 книг...\r";
				List<string> lDirList = new List<string>();
				
				// сканировать все подпапки исходной папки
				m_AllDirs	= FilesWorker.recursionDirsSearch( m_SourceDir, ref lDirList, true );
				m_AllFiles	= FilesWorker.makeFilesListFromDirs( ref worker, ref e, ref lDirList, ref m_FilesList, true );
				
				// проверка, есть ли хоть один файл в папке для сканирования
				if ( m_AllFiles == 0 ) {
					MessageBox.Show( "В папке сканирования не найдено ни одного файла!\nРабота прекращена.",
					                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				
				this.Text += string.Format( ": Всего {0} каталогов; {1} файлов", m_AllDirs, m_AllFiles );
				StatusLabel.Text += string.Format( "Осталось проверить {0} книг...\r", m_AllFiles );
				lDirList.Clear();
			} else {
				/* возобновление поиска */
				// загрузка данных из xml
				StatusLabel.Text += "Возобновление поиска невалидных fb2 книг из xml файла:\r";
				StatusLabel.Text += m_fromXmlPath + "\r";
				StatusLabel.Text += "Загрузка списка непроверенных книг из xml файла...\r";
				XElement xTree = XElement.Load( m_fromXmlPath );

				//загрузка данных о ходе сравнения
				XElement xCompareData = xTree.Element("SearchData");
				m_AllDirs = Convert.ToInt32( xCompareData.Element("AllDirs").Value );
				m_AllFiles = Convert.ToInt32( xCompareData.Element("AllFiles").Value );
				this.Text += string.Format( ": Всего {0} каталогов; {1} файлов", m_AllDirs, m_AllFiles );

				// заполнение списка необработанных файлов
				IEnumerable<XElement> files = xTree.Element("NotWorkingFiles").Elements("File");
				int NotWorkingFiles = files.ToList().Count;
				StatusLabel.Text += string.Format( "Осталось проверить {0} книг...\r", NotWorkingFiles.ToString() );
				ProgressBar.Maximum = NotWorkingFiles;
				int i = 0;
				foreach ( XElement element in files ) {
					m_FilesList.Add( element.Value );
					worker.ReportProgress( i++ );
				}

				// загрузка из xml-файла в хэш-лист данных о невалидных книгах
				int FB2NotValidate = Convert.ToInt32(xTree.Element("FB2NotValidate").Attribute("count").Value);
				StatusLabel.Text += string.Format( "Загрузка из xml файла в визуальный список данных об {0} ранее найденных невалидных книгах...\r", FB2NotValidate);
				files = xTree.Element("FB2NotValidate").Elements("File");
				ProgressBar.Maximum	= files.ToList().Count;
				i = 0;
				foreach ( XElement element in files ) {
					if ( worker.CancellationPending )  {
						e.Cancel = true;
						return;
					}
					createNotValidateBookItem( element.Value, m_fb2Genres, m_fv2Validator, m_sharpZipLib );
					worker.ReportProgress( i++ );
				}
				StatusLabel.Text += "Продолжение поиска невалидных книг и создание их визуального списка...\r";
			}

			ControlPanel.Enabled = true;
			
			if ( worker.CancellationPending ) {
				e.Cancel = true;
				return;
			}

			// Создание списка невалидных fb2-книг
			searchNotValidateFiles( sender, e, m_FilesList );
			
			if ( m_autoResizeColumns )
				MiscListView.AutoResizeColumns( m_listViewFB2Files );
		}
		
		// Отображение результата
		private void bw_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
			ProgressBar.Value = e.ProgressPercentage;
		}
		
		// Проверяем - это отмена, ошибка, или конец задачи и сообщить
		private void bw_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {
			DateTime dtEnd = DateTime.Now;
			FilesWorker.RemoveDir( Settings.Settings.TempDirPath );

			string sTime = dtEnd.Subtract( m_dtStart ).ToString().Substring( 0, 8 ) + " (час.:мин.:сек.)";
			if ( e.Cancelled ) {
				m_EndMode.EndMode = EndWorkModeEnum.Cancelled;
				if ( m_StopToSave ) {
					// остановка поиска копий с сохранением списка необработанных книг в файл
					m_StopToSave = false;
					// сохранение в xml-файл списка данных о невалидных и необработанных книг
					sfdList.Title		= "Укажите файл для будущего возобновления поиска всех невалидных книг:";
					sfdList.Filter		= "SharpFBTools Файлы хода работы Корректора (*.corr_break)|*.corr_break";
					sfdList.FileName	= string.Empty;
					sfdList.InitialDirectory = Settings.Settings.ProgDir;
					DialogResult result = sfdList.ShowDialog();
					if ( result == DialogResult.OK ) {
						ControlPanel.Enabled = false;
						StatusLabel.Text += "Сохранение данных анализа в файл:\r";
						StatusLabel.Text += sfdList.FileName;
						saveSearchedDataToXmlFile( sfdList.FileName, ref m_FilesList );
						m_EndMode.Message = "Поиск всех невалидных fb2 файлов прерван!\nДанные поиска и список оставшихся для обработки книг сохранены в xml-файл:\n\n"+sfdList.FileName+"\n\nЗатрачено времени: " + sTime;
					}
				} else {
					// остановка поиска без сохранения результата работы в xml-файл
					m_EndMode.Message = "Поиск всех невалидных fb2-файлов остановлен!\nСписок невалидных fb2-файлов не сформирован полностью!\nЗатрачено времени: " + sTime;
				}
			} else if( e.Error != null ) {
				m_EndMode.EndMode = EndWorkModeEnum.Error;
				m_EndMode.Message = "Ошибка:\n" + e.Error.Message + "\n" + e.Error.StackTrace + "\nЗатрачено времени: " + sTime;
			} else {
				m_EndMode.EndMode = EndWorkModeEnum.Done;
				m_EndMode.Message = "Поиск всех невалидных fb2-файлов завершен!\nЗатрачено времени: " + sTime;
				if ( m_listViewFB2Files.Items.Count == 0 )
					m_EndMode.Message += "\n\nНе найдено НИ ОДНОЙ невалидной книги!";
			}
			
			m_FilesList.Clear();
			this.Close();
		}
		
		// остановка поиска / возобновления поиска из xml
		// StopForSaveToXml: false - остановка поиска; true - остановка возобновления поиска
		private void StopCompare( bool StopForSaveToXml ) {
			m_StopToSave = StopForSaveToXml;
			if ( m_bw.IsBusy ) {
				if ( m_bw.WorkerSupportsCancellation )
					m_bw.CancelAsync();
			}
		}
		#endregion
		
		// =============================================================================================
		// 							ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ ДЛЯ BACKGROUNDWORKER
		// =============================================================================================
		#region BackgroundWorker: Вспомогательные методы
		/// <summary>
		/// поиск невалидных книг
		/// </summary>
		private void searchNotValidateFiles( object sender, DoWorkEventArgs e, List<string> FilesList ) {
			BackgroundWorker worker = sender as BackgroundWorker;
			ProgressBar.Maximum	= FilesList.Count;
			int i = 0;
			List<string> FinishedFilesList = new List<string>();
			foreach ( string FilePath in FilesList ) {
				if ( worker.CancellationPending )  {
					// удаление из списка всех файлов обработанные книги (файлы)
					WorksWithBooks.removeFinishedFilesInFilesList( ref FilesList, ref FinishedFilesList);
					e.Cancel = true;
					return;
				}
				createNotValidateBookItem( FilePath, m_fb2Genres, m_fv2Validator, m_sharpZipLib );
				// обработанные файлы
				FinishedFilesList.Add( FilePath );
				worker.ReportProgress( i++ );
			}
			
			// удаление из списка всех файлов обработанные книги (файлы)
			WorksWithBooks.removeFinishedFilesInFilesList( ref FilesList, ref FinishedFilesList);
		}
		/// <summary>
		/// создание итема в списке для невалидной книги
		/// </summary>
		private void createNotValidateBookItem( string FilePath, FB2UnionGenres fb2g,
		                                       FB2Validator fv2Validator, SharpZipLibWorker sharpZipLib ) {
			if ( File.Exists( FilePath ) ) {
				string TempDir = Settings.Settings.TempDirPath;
				string FileExt = Path.GetExtension( FilePath );
				ListViewItem.ListViewSubItem[] subItems;
				if ( FilesWorker.isFB2File( FilePath ) || FilesWorker.isFB2Archive( FilePath ) ) {
					ListViewItem item = new ListViewItem( FilePath, FilesWorker.isFB2File( FilePath ) ? 1 : 2 );
					try {
						if ( FilesWorker.isFB2File( FilePath ) ) {
							item.ForeColor = Colors.FB2ForeColor;
							subItems = createSubItemsWithMetaData( FilePath, FileExt, item, ref fb2g, ref fv2Validator );
						} else {
							// для zip-архивов
							FilesWorker.RemoveDir( TempDir );
							sharpZipLib.UnZipFB2Files( FilePath, TempDir );
							string [] files = Directory.GetFiles( TempDir );
							if ( FilesWorker.isFB2File( files[0] ) ) {
								item.ForeColor = Colors.ZipFB2ForeColor;
								subItems = createSubItemsWithMetaData( files[0], FileExt, item, ref fb2g, ref fv2Validator );
							} else {
								item.ForeColor = Colors.BadZipForeColor;
								subItems = WorksWithBooks.createEmptySubItemsForItem( item );
							}
						}
						if ( subItems != null )
							item.SubItems.AddRange(subItems);
					} catch ( Exception ex) {
						Debug.DebugMessage(
							null, ex, "FB2NotValidateForm.createNotValidateBookItem(): Создание итема в списке для невалидной книги."
						);
						subItems = WorksWithBooks.createEmptySubItemsForItem( item );
						if ( subItems != null ) {
							item.SubItems.AddRange(subItems);
							item.ForeColor = Colors.BadZipForeColor;
						}
					}
					
					item.Tag = new ListViewItemType( "f", FilePath );
					item.BackColor = Colors.FileBackColor;

					if ( subItems != null ) {
						m_listViewFB2Files.Items.Add(item);
					}
				}
			}
		}
		
		/// <summary>
		/// Создание заполненных subitems для невалидных книг для Корректора
		/// </summary>
		public static ListViewItem.ListViewSubItem[]
			createSubItemsWithMetaData(
				string FilePath, string SourceFileExt, ListViewItem Item,
				ref FB2UnionGenres FB2FullSortGenres, ref FB2Validator fv2Validator
			) {
			FB2BookDescription bd = null;
			try {
				bd = new FB2BookDescription( FilePath );
			} catch ( System.Exception ex ) {
				Debug.DebugMessage(
					null, ex, "FB2NotValidateForm.createSubItemsWithMetaData(): Создание заполненных subitems для невалидных книг для Корректора."
				);
			}
			
			string valid = bd != null ? bd.IsValidFB2Union : fv2Validator.ValidatingFB2File( FilePath );
			if ( !string.IsNullOrEmpty( valid ) ) {
				valid = "Нет";
				Item.ForeColor = Colors.FB2NotValidForeColor;
			} else {
				return null;
			}
			
			return new ListViewItem.ListViewSubItem[] {
				new ListViewItem.ListViewSubItem(Item, bd != null ? bd.TIBookTitle : string.Empty),
				new ListViewItem.ListViewSubItem(Item, bd != null ? bd.TIAuthors : string.Empty),
				new ListViewItem.ListViewSubItem(Item, bd != null ? GenresWorker.cyrillicGenreName( bd.TIGenres, ref FB2FullSortGenres ) : string.Empty),
				new ListViewItem.ListViewSubItem(Item, bd != null ? bd.TISequences : string.Empty),
				new ListViewItem.ListViewSubItem(Item, bd != null ? bd.TILang : string.Empty),
				new ListViewItem.ListViewSubItem(Item, bd != null ? bd.DIID : string.Empty),
				new ListViewItem.ListViewSubItem(Item, bd != null ? bd.DIVersion : string.Empty),
				new ListViewItem.ListViewSubItem(Item, bd != null ? bd.Encoding : string.Empty),
				new ListViewItem.ListViewSubItem(Item, valid),
				new ListViewItem.ListViewSubItem(Item, SourceFileExt),
				new ListViewItem.ListViewSubItem(Item, bd != null ? bd.FileLength : string.Empty),
				new ListViewItem.ListViewSubItem(Item, bd != null ? bd.FileCreationTime : string.Empty),
				new ListViewItem.ListViewSubItem(Item, bd != null ? bd.FileLastWriteTime : string.Empty)
			};
		}
		
		/// <summary>
		/// сохранение данных о найденных копиях и о необработанных книгах при прерывании проверки для записи
		/// </summary>
		private void saveSearchedDataToXmlFile(string ToFileName, ref List<string> FilesList) {
			int fileNumber = 0;
			XDocument doc = new XDocument(
				new XDeclaration("1.0", "utf-8", "yes"),
				new XComment("Файл списка невалидных fb2 книг, сохраненный после прерывания работы Корректора. Используется для возобновления поиска"),
				new XElement("Files", new XAttribute("type", "corr_break"),
				             new XComment("Папка для поиска невалидных книг"),
				             new XElement("SourceDir", m_SourceDir),
				             new XComment("Данные о каталогах и файлах"),
				             new XElement("SearchData",
				                          new XElement("AllDirs", m_AllDirs),
				                          new XElement("AllFiles", m_AllFiles)
				                         ),
				             new XComment("Найденные невалидные файлы"),
				             new XElement("FB2NotValidate", new XAttribute("count", m_listViewFB2Files.Items.Count)),
				             new XComment("Не обработанные файлы"),
				             new XElement("NotWorkingFiles", new XAttribute("count", FilesList.Count))
				            )
			);

			// обработанные книги
			if ( m_listViewFB2Files.Items.Count > 0 ) {
				fileNumber = 0;
				for ( int i = 0; i != m_listViewFB2Files.Items.Count; ++i ) {
					doc.Root.Element("FB2NotValidate").Add(
						new XElement(
							"File", new XAttribute("number", fileNumber++),
							new XElement("Path", m_listViewFB2Files.Items[i].Text)
						)
					);
//					worker.ReportProgress( ++i );
				}
			}
			
			// необработанные книги
			if ( FilesList.Count > 0 ) {
				fileNumber = 0;
				for (int i = 0; i != FilesList.Count; ++i) {
					doc.Root.Element("NotWorkingFiles").Add(
						new XElement(
							"File", new XAttribute("number", fileNumber++),
							new XElement("Path", FilesList[i])
						)
					);
//					worker.ReportProgress( ++i );
				}
			}
			StatusLabel.Text += "\nПодождите, пожалуйста - происходит сохранение данных в файл...";
			doc.Save( ToFileName );
		}
		#endregion
		
		// =============================================================================================
		// 								ОБРАБОТЧИКИ СОБЫТИЙ
		// =============================================================================================
		#region Обработчики событий
		void BtnSaveToXmlClick(object sender, EventArgs e)
		{
			StopCompare( true );
		}
		void BtnStopClick(object sender, EventArgs e)
		{
			StopCompare( false );
		}
		#endregion
	}
}
