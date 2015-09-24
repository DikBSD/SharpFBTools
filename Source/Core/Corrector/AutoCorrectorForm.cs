/*
 * Сделано в SharpDevelop.
 * Пользователь: Вадим Кузнецов (DikBSD)
 * Дата: 09.09.2015
 * Время: 13:23
 * 
 */
using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

using Core.Common;
using Core.FB2.Genres;

using EndWorkMode		= Core.Common.EndWorkMode;
using FilesWorker		= Core.Common.FilesWorker;
using WorksWithBooks	= Core.Common.WorksWithBooks;
using MiscListView		= Core.Common.MiscListView;

// enums
using EndWorkModeEnum	= Core.Common.Enums.EndWorkModeEnum;

namespace Core.Corrector
{
	/// <summary>
	/// прогресс автокорректировки книг Сортировщика
	/// </summary>
	public partial class AutoCorrectorForm : Form
	{
		#region Закрытые данные класса
		private const string m_sMessTitle			= "SharpFBTools - Автокорректировка fb2 файлов";
		private readonly ListView m_listViewFB2Files = new ListView();
		private readonly SharpZipLibWorker	m_sharpZipLib = new SharpZipLibWorker();
		private readonly string	m_SourceRootDir				= string.Empty; // корневой каталог для поиска помеченных папок и файлов
		private readonly IList<ListViewItemInfo> m_ListViewItemInfoList = null; // помеченные / выделенные книги и папки в корневой папке
		private readonly IList<string> m_SourceDirs			= new List<string>();	// подпапки корневой папки для поиска книг
		private readonly IList<string> m_SourceRootFiles	= new List<string>();	// выбранные книги в корневой папке
		private readonly EndWorkMode m_EndMode		= new EndWorkMode();
		private readonly string m_TempDir			= Settings.Settings.TempDir;
		private readonly string m_fromXmlPath		= null;	// null - полное сканирование; Путь - возобновление Автокорректировки их xml
		private List<string> m_DirsList				= new List<string>(); // каталоги
		private List<string> m_NotWorkingFilesList	= new List<string>(); // не обработанные файлы
		private List<string> m_WorkingFilesList		= new List<string>(); // обработанные файлы
		private bool m_StopToSave = false;	// true, если остановка с сохранением необработанного списка книг в файл.
		private int m_AllDirs = 0;
		private int m_AllFiles = 0;
		private readonly bool m_IsFB2Librusec = true;
		private readonly DateTime m_dtStart = DateTime.Now;
		
		private BackgroundWorker m_bw		= null; // фоновый обработчик для Непрерывной Автокорректировки
		private BackgroundWorker m_bwRenew	= null; // фоновый обработчик для Возобновления Автокорректировки
		#endregion
		
		public AutoCorrectorForm( string fromXmlPath, string SourceRootDir, IList<ListViewItemInfo> ListViewItemInfoList,
		                         ListView listViewFB2Files, bool IsFB2Librusec )
		{
			InitializeComponent();
			m_SourceRootDir 		= SourceRootDir;
			m_ListViewItemInfoList	= ListViewItemInfoList;
			
			m_listViewFB2Files	= listViewFB2Files;
			m_IsFB2Librusec		= IsFB2Librusec;
			ProgressBar.Value	= 0;
			
			InitializeBackgroundWorker();
			InitializeRenewBackgroundWorker();
			
			// Запуск процесса DoWork от RunWorker
			if( fromXmlPath == null ) {
				if( !m_bw.IsBusy )
					m_bw.RunWorkerAsync();
			} else {
				m_fromXmlPath = fromXmlPath; // путь к xml файлу для возобновления Автокорректировки
				if( !m_bwRenew.IsBusy )
					m_bwRenew.RunWorkerAsync();
			}
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
		public bool isStopToXmlClicked() {
			return m_StopToSave;
		}
		public string getSourceDirFromRenew() {
			return m_SourceRootDir;
		}
		#endregion
		
		// =============================================================================================
		// 				BACKGROUNDWORKER ДЛЯ НЕПРЕРЫВНОИ АВТОКОРРЕКЦИИ и ПРЕРЫВАНИЯ / ВОЗОБНОВЛЕНИЯ
		// =============================================================================================
		#region BackgroundWorker для Непрерывной Автокорректировки и прерывания / возобновления
		// =============================================================================================
		//			BACKGROUNDWORKER: ОБРАБОТКА ФАЙЛОВ
		// =============================================================================================
		#region BackgroundWorker: Автокорректировка файлов
		private void InitializeBackgroundWorker() {
			// Инициализация перед использование BackgroundWorker
			m_bw = new BackgroundWorker();
			m_bw.WorkerReportsProgress		= true; // Позволить выводить прогресс процесса
			m_bw.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
			m_bw.DoWork 			+= new DoWorkEventHandler( bw_DoWork );
			m_bw.ProgressChanged 	+= new ProgressChangedEventHandler( bw_ProgressChanged );
			m_bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler( bw_RunWorkerCompleted );
		}
		
		// Обработка файлов
		private void bw_DoWork( object sender, DoWorkEventArgs e ) {
			ProgressBar.Value = 0;
			StatusLabel.Text += "Создание списка файлов для Автокорректировки...\r";
			
			foreach( ListViewItemInfo Item in m_ListViewItemInfoList ) {
				if ( Item.IsDirListViewItem )
					m_SourceDirs.Add( Item.FilePathSource );
				else
					m_SourceRootFiles.Add( Item.FilePathSource );
			}
			
			m_NotWorkingFilesList.Clear();
			foreach( string dir in m_SourceDirs )
				m_AllDirs += FilesWorker.recursionDirsSearch( dir, ref m_DirsList, true );

			m_AllFiles = FilesWorker.makeFilesListFromDirs( ref m_bw, ref e, ref m_DirsList, ref m_NotWorkingFilesList, true );
			m_NotWorkingFilesList.AddRange( m_SourceRootFiles );
			m_AllFiles += m_SourceRootFiles.Count;
			// только теперь добавляем корневой каталог, если в нем выделен / помечен хоть один файл
			if ( m_SourceRootFiles.Count > 0 ) {
				m_DirsList.Add( m_SourceRootDir );
				++m_AllDirs;
			}

			ControlPanel.Enabled = true;
			if( ( m_bw.CancellationPending ) ) {
				e.Cancel = true;
				return;
			}
			// проверка, есть ли хоть один файл в папке для сканирования
			if( m_AllFiles == 0 ) {
				MessageBox.Show( "В папке сканирования не найдено ни одного файла!\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			
			ControlPanel.Enabled = true;
			
			// Автокорреетировка книг
			StatusLabel.Text += "Запуск автокорректировки fb2-файлов...\r";
			autoCorrect( ref m_bw, ref e, ref m_NotWorkingFilesList, ref m_WorkingFilesList, false );

			if( ( m_bw.CancellationPending ) ) {
				e.Cancel = true;
				return;
			}
		}
		
		// Отображение результата
		private void bw_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
			++ProgressBar.Value;
		}
		
		// Проверяем - это отмена, ошибка, или конец задачи и сообщить
		private void bw_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {
			DateTime dtEnd = DateTime.Now;
			string sTime = dtEnd.Subtract( m_dtStart ).ToString() + " (час.:мин.:сек.)";
			if ( e.Cancelled ) {
				m_EndMode.EndMode = EndWorkModeEnum.Cancelled;
				m_EndMode.Message = "Автокорректировка fb2-файлов прервана!\nЗатрачено времени: "+sTime;
				if (m_StopToSave) {
					// остановка поиска копий с сохранением списка необработанных книг в файл
					m_StopToSave = false;
					// сохранение в xml-файл списка необработанных книг
					sfdList.Title		= "Укажите файл для будущего возобновления Автокорректировки книг:";
					sfdList.Filter		= "Автокорректор (*.acor_break)|*.acor_break";
					sfdList.FileName	= string.Empty;
					sfdList.InitialDirectory = Settings.Settings.ProgDir;
					DialogResult result = sfdList.ShowDialog();
					if( result == DialogResult.OK ) {
						ControlPanel.Enabled = false;
						StatusLabel.Text += "Сохранение списка обработанных и необработанных книг в файл:\r";
						StatusLabel.Text += sfdList.FileName;
						saveSearchDataToXmlFile( sfdList.FileName, ref m_DirsList, ref m_WorkingFilesList, ref m_NotWorkingFilesList );
						m_EndMode.Message = "Автокорректировка fb2-файлов прервана!\nСписок оставшихся для обработки книг сохранены в xml-файл:\n\n"+sfdList.FileName+"\n\nЗатрачено времени: "+sTime;
					}
				}
			} else if( e.Error != null ) {
				m_EndMode.EndMode = EndWorkModeEnum.Error;
				m_EndMode.Message = "Ошибка:\n" + e.Error.Message + "\n" + e.Error.StackTrace + "\nЗатрачено времени: "+sTime;
			} else {
				m_EndMode.EndMode = EndWorkModeEnum.Done;
				m_EndMode.Message = "Обработка fb2-файлов завершена!\nЗатрачено времени: "+sTime;
			}
			m_NotWorkingFilesList.Clear();
			this.Close();
		}
		#endregion

		// =============================================================================================
		//					BackgroundWorker: Возобновление Автокорректироки
		// =============================================================================================
		#region BackgroundWorker: Возобновление Автокорректировки
		private void InitializeRenewBackgroundWorker() {
			m_bwRenew = new BackgroundWorker();
			m_bwRenew.WorkerReportsProgress			= true; // Позволить выводить прогресс процесса
			m_bwRenew.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
			m_bwRenew.DoWork 				+= new DoWorkEventHandler( m_bwRenew_reNewAutoCorrecrFromFile_DoWork );
			m_bwRenew.ProgressChanged 		+= new ProgressChangedEventHandler( bw_ProgressChanged );
			m_bwRenew.RunWorkerCompleted	+= new RunWorkerCompletedEventHandler( bw_RunWorkerCompleted );
		}
		// возобновление Автокорректировки - загрузка отчета о необработанных книгах из xml-файла
		private void m_bwRenew_reNewAutoCorrecrFromFile_DoWork( object sender, DoWorkEventArgs e ) {
			ControlPanel.Enabled = false;
			
			// загрузка данных из xml
			StatusLabel.Text += "Возобновление Автокорректировки fb2 книг из xml файла:\r";
			StatusLabel.Text += m_fromXmlPath + "\r";
			XElement xmlTree = XElement.Load( m_fromXmlPath );

			//загрузка данных о ходе сравнения
			XElement compareData = xmlTree.Element("Data");
			m_AllFiles = Convert.ToInt32( compareData.Element("AllFiles").Value );
			m_AllDirs = Convert.ToInt32( compareData.Element("AllDirs").Value );
			ProgressBar.Maximum	= m_AllFiles;
			
			// заполнение списка каталогов
			StatusLabel.Text += "Загрузка списка каталогов xml файла...\r";
			IEnumerable<XElement> dirs = xmlTree.Element("Dirs").Elements("Dir");
			ProgressBar.Value = 0;
			m_DirsList.Clear();
			int d = 0;
			foreach (XElement element in dirs) {
				m_DirsList.Add(element.Value);
				m_bw.ReportProgress( ++d );
			}

			// заполнение списка обработанных файлов
			loadFileList( ref m_bwRenew, "Загрузка списка обработанных книг из xml файла...\r", ref xmlTree, "WorkingFiles", ref m_WorkingFilesList );
			// заполнение списка необработанных файлов
			loadFileList( ref m_bwRenew, "Загрузка списка необработанных книг из xml файла...\r", ref xmlTree, "NotWorkingFiles", ref m_NotWorkingFilesList );

			ControlPanel.Enabled = true;
			
			// Автокорректировка файлов
			StatusLabel.Text += "Возобновление Автокорректировки книг...\r";
			autoCorrect( ref m_bwRenew, ref e, ref m_NotWorkingFilesList, ref m_WorkingFilesList, true );

			// оздание итемов списка всех файлов и каталогов
			StatusLabel.Text += "Отображение списка файлов с метаданными...\r";
			IGenresGroup GenresGroup = new GenresGroup();
			IFBGenres fb2Genres = GenresWorker.genresListOfGenreSheme( m_IsFB2Librusec, ref GenresGroup );
			// генерация списка файлов - создание итемов listViewSource
			if ( !WorksWithBooks.generateBooksListWithMetaData( m_listViewFB2Files, m_SourceRootDir, ref fb2Genres, m_IsFB2Librusec,
			                                                   true, false, false, this, ProgressBar, m_bwRenew, e ) )
				e.Cancel = true;
		}
		#endregion
		#endregion
		
		// =============================================================================================
		// 						ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ
		// =============================================================================================
		#region Вспомогательные методы
		// удаление из списка всех обработанных книг (файлы)
		private void removeFinishedFilesInFilesList( ref List<string> FilesList, ref List<string> FinishedFilesList) {
			List<string> FilesToWorkingList = new List<string>();
			foreach (var file in FilesList.Except(FinishedFilesList))
				FilesToWorkingList.Add(file);
			
			FilesList.Clear();
			FilesList.AddRange(FilesToWorkingList);
		}
		// Автокорректировка файлов
		private void autoCorrect( ref BackgroundWorker bw, ref DoWorkEventArgs e, ref List<string> NotWorлingFilesList, ref List<string> WorkingFilesList, bool IsReNew = false ) {
			this.Text = string.Format( "Автокорректировка книг: всего {0} книг в {1} каталогах", m_AllFiles, m_AllDirs );
			if ( IsReNew )
				this.Text += string.Format( " / Для обработки {0} файлов", m_NotWorkingFilesList.Count );
			ProgressBar.Maximum	= NotWorлingFilesList.Count;
			ProgressBar.Value = 0;
			
			int i = 0;
			foreach( string file in NotWorлingFilesList ) {
				if( ( bw.CancellationPending ) ) {
					// удаление из списка всех файлов обработанные книги (файлы)
					removeFinishedFilesInFilesList( ref NotWorлingFilesList, ref m_WorkingFilesList);
					e.Cancel = true;
					return;
				}
				// обработка файла
				WorksWithBooks.autoCorrect( file, m_sharpZipLib );
				// обработанные файлы
				m_WorkingFilesList.Add( NotWorлingFilesList[i] );
				m_bw.ReportProgress( ++i );
			}
			// удаление из списка всех файлов обработанные книги (файлы)
			removeFinishedFilesInFilesList( ref NotWorлingFilesList, ref WorkingFilesList);
		}
		
		// сохранение данных о необработанных книгах при прерывании проверки для записи
		private void saveSearchDataToXmlFile(string ToFileName, ref List<string> DirsList, ref List<string> WorkingFilesList, ref List<string> NotWorkingFilesList) {
			int fileNumber = 0;
			XDocument doc = new XDocument(
				new XDeclaration("1.0", "utf-8", "yes"),
				new XComment("Файл списка fb2 книг, сохраненный после прерывания работы Автокорректировки. Используется для возобновления Автокорректировки"),
				new XElement("Files", new XAttribute("type", "acor_break"),
				             new XComment("Корневая папка для поиска книг"),
				             new XElement("SourceRootDir", m_SourceRootDir),
				             new XComment("Данные о числе каталогов и файлов для автокорректировки"),
				             new XElement("Data",
				                          new XElement("AllDirs", m_AllDirs),
				                          new XElement("AllFiles", m_AllFiles)
				                         ),
				             new XComment("Настройки"),
				             new XElement("Settings",
				                          new XElement("GenresFB2Librusec", m_IsFB2Librusec)
				                         ),
				             new XComment("Заданные каталоги для обработки"),
				             new XElement("Dirs", new XAttribute("count", DirsList.Count)),
				             new XComment("Обработанные файлы"),
				             new XElement("WorkingFiles", new XAttribute("count", WorkingFilesList.Count)),
				             new XComment("Не обработанные файлы"),
				             new XElement("NotWorkingFiles", new XAttribute("count", NotWorkingFilesList.Count))
				            )
			);
			
			// каталоги
			if ( DirsList.Count > 0 ) {
				StatusLabel.Text += string.Format( "Сохранение списка {0} каталогов в файл:\r", DirsList.Count );
				ProgressBar.Maximum = DirsList.Count;
				ProgressBar.Value = 0;
				fileNumber = 0;
				for (int i = 0; i != m_DirsList.Count; ++i) {
					doc.Root.Element("Dirs").Add(
						new XElement("Dir", new XAttribute("number", fileNumber++),
						             new XElement("Path", DirsList[i])
						            )
					);
					++ProgressBar.Value;
				}
			}
			
			// обработанные книги
			saveFilesList( string.Format( "Сохранение списка обработанных {0} книг в файл:\r", WorkingFilesList.Count ), ref doc, "WorkingFiles", ref WorkingFilesList );

			// необработанные книги
			saveFilesList( string.Format( "Сохранение списка необработанных {0} книг в файл:\r", NotWorkingFilesList.Count ), ref doc, "NotWorkingFiles", ref NotWorkingFilesList );
			
			doc.Save(ToFileName);
		}
		
		// заполнение списка файлов
		private void loadFileList( ref BackgroundWorker bw, string LogMessage, ref XElement xmlTree, string ElementName, ref List<string> FilesList ) {
			StatusLabel.Text += LogMessage;
			IEnumerable<XElement> files = xmlTree.Element(ElementName).Elements("File");
			ProgressBar.Value = 0;
			FilesList.Clear();
			int i = 0;
			foreach (XElement element in files) {
				FilesList.Add(element.Value);
				bw.ReportProgress( ++i );
			}
		}
		
		// сохранить список книг
		private void saveFilesList( string LogMessage, ref XDocument doc, string ElementName, ref List<string> FilesList ) {
			if ( FilesList.Count > 0 ) {
				StatusLabel.Text += LogMessage;
				ProgressBar.Maximum = FilesList.Count;
				ProgressBar.Value = 0;
				int fileNumber = 0;
				for (int i = 0; i != FilesList.Count; ++i) {
					doc.Root.Element(ElementName).Add(
						new XElement("File", new XAttribute("number", fileNumber++),
						             new XElement("Path", FilesList[i])
						            )
					);
					++ProgressBar.Value;
				}
			}
		}
		
		// остановка Автокорректировки / возобновления Автокорректировки из xml
		// StopForSaveToXml: false - остановка Автокорректировки; true - остановка возобновления Автокорректировки
		private void StopAutoCorrect( bool StopForSaveToXml ) {
			m_StopToSave = StopForSaveToXml;
			if ( m_bw.IsBusy ) {
				if( m_bw.WorkerSupportsCancellation )
					m_bw.CancelAsync();
			} else {
				if( m_bwRenew.WorkerSupportsCancellation )
					m_bwRenew.CancelAsync();
			}
		}
		#endregion
		
		// =============================================================================================
		// 								ОБРАБОТЧИКИ СОБЫТИЙ
		// =============================================================================================
		#region Обработчики событий
		// нажатие кнопки прерывания в файл...
		void BtnSaveToXmlClick(object sender, EventArgs e)
		{
			StopAutoCorrect( true );
		}
		// нажатие кнопки прерывания работы
		void BtnStopClick(object sender, EventArgs e)
		{
			StopAutoCorrect( false );
		}
		#endregion
	}
}
