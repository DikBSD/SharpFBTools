/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 16.03.2009
 * Time: 9:03
 * 
 * License: GPL 2.1
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Threading;
using System.Diagnostics;

using Core.FB2.FB2Parsers;
using Core.FB2.Description.TitleInfo;
using Core.FB2.Description.Common;
using Core.FB2.Genres;
using Core.BookSorting;
using Core.FileManager;
using Core.Misc;
using Settings;

using Core.Templates.Lexems;

using fB2Parser					= Core.FB2.FB2Parsers.FB2Parser;
using filesWorker				= Core.FilesWorker.FilesWorker;
using fb2Validator				= Core.FB2Parser.FB2Validator;
using templatesParser			= Core.Templates.TemplatesParser;
using templatesVerify			= Core.Templates.TemplatesVerify;
using templatesLexemsSimple		= Core.Templates.Lexems.TPSimple;
using selectedSortQueryCriteria	= Core.BookSorting.SelectedSortQueryCriteria;

namespace SharpFBTools.Tools
{
	/// <summary>
	/// Режим сортировки книг - по числу Авторов и Жанров
	/// </summary>
	public enum SortModeType {
		_1Genre1Author,			// по первому Жанру и первому Автору Книги
		_1GenreAllAuthor, 		// по первому Жанру и всем Авторам Книги
		_AllGenre1Author,		// по всем Жанрам и первому Автору Книги
		_AllGenreAllAuthor, 	// по всем Жанрам и всем Авторам Книги
	}
	
	/// <summary>
	/// Сортировщик fb2-файлов
	/// </summary>
	public partial class SFBTpFileManager : UserControl
	{
		#region Закрытые данные класса
		private fb2Validator fv2V = new fb2Validator();
		private List<selectedSortQueryCriteria> m_lSSQCList = null; // список критериев поиска для Избранной Сортировки
		private DateTime m_dtStart;
		private BackgroundWorker m_bw = null;
		private string m_sLineTemplate	= string.Empty;
		private string m_sMessTitle		= string.Empty;
		private bool m_bFullSort		= true;
		private bool m_bScanSubDirs		= true;
		private Core.FileManager.StatusView m_sv	= new Core.FileManager.StatusView();
		private MiscListView m_mscLV				= new MiscListView(); // класс по работе с ListView
		private const string m_space		= " "; // для задания отступов данных от границ колонов в Списке
		private Core.FilesWorker.SharpZipLibWorker sharpZipLib = new Core.FilesWorker.SharpZipLibWorker();
		FullNameTemplates m_fnt = new FullNameTemplates();
		#endregion
		
		public SFBTpFileManager()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			InitializeBackgroundWorker();
			Init();
			// читаем сохраненные пути к папкам и шаблон Менеджера Файлов, если они есть
			ReadFMTempData();
			lvFilesCount.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
			rtboxTemplatesList.Clear();
			string sTDPath = Settings.FileManagerSettings.GetDefFMDescTemplatePath();
			if( File.Exists( sTDPath ) )
				rtboxTemplatesList.LoadFile( sTDPath );
			else
				rtboxTemplatesList.Text = "Не найден файл описания Шаблонов подстановки: \""+sTDPath+"\"";
		}
		
		#region Закрытые методы реализации BackgroundWorker
		// Инициализация перед использование BackgroundWorker
		private void InitializeBackgroundWorker() {
			m_bw = new BackgroundWorker();
			m_bw.WorkerReportsProgress		= true; // Позволить выводить прогресс процесса
			m_bw.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
			m_bw.DoWork 			+= new DoWorkEventHandler( bw_DoWork );
			m_bw.ProgressChanged 	+= new ProgressChangedEventHandler( bw_ProgressChanged );
			m_bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler( bw_RunWorkerCompleted );
		}
		
		// сортировка файлов по папкам, согласно шаблонам подстановки
		private void bw_DoWork( object sender, DoWorkEventArgs e ) {
			#region Код
			m_sv.Clear();
			textBoxFiles.Clear();
			List<string> lDirList			= new List<string>();
			List<string> lFileList			= new List<string>();
			
			if(m_bFullSort) {
				// ========================================================================
				//                              Полная Сортировка
				// ========================================================================
				// формируем список помеченных папок
				List<string> lCheckedDirList	= new List<string>();
				List<string> lCheckedFileList	= new List<string>();
				System.Windows.Forms.ListView.CheckedListViewItemCollection checkedItems = listViewSource.CheckedItems;
				foreach( ListViewItem lvi in checkedItems ) {
					ListViewItemType it = (ListViewItemType)lvi.Tag;
					if(it.Type.Trim() == "d")
						lCheckedDirList.Add(it.Value.Trim());
					else if(it.Type.Trim() == "f")
						lCheckedFileList.Add(it.Value.Trim());
				}
				
				lFileList.AddRange( lCheckedFileList ); // помеченные файлы
				lDirList.Add( getSourceDir() );
				if( !m_bScanSubDirs ) {
					// сканировать только указанную папку (ее папки, но не их подпапки)
					lDirList.AddRange( lCheckedDirList );
					foreach(string dir in lCheckedDirList)
						lFileList.AddRange( Directory.GetFiles( dir ) );

				} else {
					// сканировать и все подпапки
					foreach(string dir in lCheckedDirList)
						filesWorker.DirsFilesParser( m_bw, e, dir, ref lDirList, ref lFileList );
				}
				lCheckedDirList.Clear();
				lCheckedFileList.Clear();
			} else {
				// ========================================================================
				//                            Избранная Сортировка
				// ========================================================================
				if( !m_bScanSubDirs ) {
					// сканировать только указанную папку
					lDirList.Add( getSourceDir() );
					lFileList.AddRange( Directory.GetFiles( getSourceDir() ) );
				} else {
					// сканировать и все подпапки
					filesWorker.DirsFilesParser( m_bw, e, getSourceDir(), ref lDirList, ref lFileList );
				}
			}
			lvFilesCount.Items[0].SubItems[1].Text = lDirList.Count.ToString();
			lvFilesCount.Items[1].SubItems[1].Text = lFileList.Count.ToString();
			
			// Проверить флаг на остановку процесса
			if( ( m_bw.CancellationPending == true ) ) {
				e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
				return;
			}
			
			// проверка, есть ли хоть один файл в папке для сканирования
			if( lFileList.Count == 0 ) {
				MessageBox.Show( "В папке сканирования не найдено ни одного файла!\nРабота прекращена.", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				tsslblProgress.Text = Settings.Settings.GetReady();
				SetSelectedSortingStartEnabled( true );
				return;
			}
			
			tsslblProgress.Text		= "Сортировка файлов:";
			tsProgressBar.Maximum	= lFileList.Count;
			tsProgressBar.Value		= 0;
			
			// данные настроек для сортировки по шаблонам
			Settings.DataFM dfm = new Settings.DataFM();
			
			// папки для проблемных файлов
			string TargetDir = getTargetDir() + "\\";
			dfm.NotReadFB2Dir	= TargetDir + dfm.NotReadFB2Dir;
			dfm.FileLongPathDir	= TargetDir + dfm.FileLongPathDir;
			dfm.NotValidFB2Dir	= TargetDir + dfm.NotValidFB2Dir;
			dfm.NotOpenArchDir	= TargetDir + dfm.NotOpenArchDir;
			
			// Отображение папок для обработки
			textBoxFiles.Text += "Папки для обработки:\r\n";
			long lCount = 0;
			foreach( string dir in lDirList )
				textBoxFiles.Text += (++lCount).ToString() + ".\t" + dir + "\r\n";

			textBoxFiles.Text += "\r\nФайлы для обработки:\r\n";
			lDirList.Clear();
			
			// формируем лексемы шаблонной строки
			List<templatesLexemsSimple> lSLexems = templatesParser.GemSimpleLexems( m_sLineTemplate );
			// сортировка
			lCount = 0;
			if( m_bFullSort ) {
				// ========================================================================
				//                              Полная Сортировка
				// ========================================================================
				foreach( string filePath in lFileList ) {
					// Проверить флаг на остановку процесса
					if( ( m_bw.CancellationPending == true ) ) {
						e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
						return;
					}
					// создаем файл по заданному пути
					if ( chBoxViewLogFiles.Checked ) {
						textBoxFiles.Text += (++lCount).ToString() + ".\t" + filePath + "\r\n";
						textBoxFiles.Select( textBoxFiles.TextLength, 0 ); textBoxFiles.ScrollToCaret();
					}
					// создание отсортированного fb2 по Жанру(ам) и Автору(ам)
					FullSorting( filePath, getSourceDir(), getTargetDir(), lSLexems, dfm );
					filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
					m_bw.ReportProgress( 0 ); // отобразим данные в контролах
				}
				
				if(!Settings.FileManagerSettings.FullSortingNotDelFB2Files)
					GenerateSourceList(getSourceDir());
			} else {
				// ========================================================================
				//                            Избранная Сортировка
				// ========================================================================
				foreach( string filePath in lFileList ) {
					// Проверить флаг на остановку процесса
					if( ( m_bw.CancellationPending == true ) ) {
						e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
						return;
					}
					if ( chBoxViewLogFiles.Checked ) {
						textBoxFiles.Text += (++lCount).ToString() + ".\t" + filePath + "\r\n";
						textBoxFiles.Select( textBoxFiles.TextLength, 0 ); textBoxFiles.ScrollToCaret();
					}
					// создаем отсортированный fb2 файл по новому пути
					SelectedSorting( filePath, getSourceDir(), getTargetDir(), lSLexems, dfm );
					m_bw.ReportProgress( 0 ); // отобразим данные в контролах
				}
			}
			lFileList.Clear();
			#endregion
		}
		
		// Отобразим результат сортировки
		private void bw_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
			if( chBoxViewProgress.Checked )
				SortingProgressData();

			++tsProgressBar.Value;
		}
		
		// Проверяем это отмена, ошибка, или конец задачи и сообщить
		private void bw_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {
			SortingProgressData(); // Отобразим результат сортировки
			DateTime dtEnd = DateTime.Now;
			filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
			
			string sTime = dtEnd.Subtract( m_dtStart ).ToString() + " (час.:мин.:сек.)";
			string sMessCanceled	= "Сортировка остановлена!\nЗатрачено времени: "+sTime;
			string sMessError		= string.Empty;
			string sMessDone		= "Сортировка файлов в указанную папку завершена!\nЗатрачено времени: "+sTime;
			
			if( ( e.Cancelled == true ) ) {
				MessageBox.Show( sMessCanceled, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			} else if( e.Error != null ) {
				sMessError = "Error!\n" + e.Error.Message + "\n" + e.Error.StackTrace + "\nЗатрачено времени: "+sTime;
				MessageBox.Show( sMessError, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			} else {
				MessageBox.Show( sMessDone, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
			
			tsslblProgress.Text = Settings.Settings.GetReady();

			if( m_bFullSort )
				SetFullSortingStartEnabled( true );
			else
				SetSelectedSortingStartEnabled( true );
		}
		#endregion
		
		#region Закрытые вспомогательные методы класса
		// получение source папки
		private string getSourceDir() {
			return m_bFullSort
				? filesWorker.WorkingDirPath( textBoxAddress.Text.Trim() )
				: filesWorker.WorkingDirPath( tboxSSSourceDir.Text.Trim() );
		}
		// получение target папки
		private string getTargetDir() {
			return m_bFullSort
				? filesWorker.WorkingDirPath( textBoxAddress.Text.Trim() ) + " - OUT"
				: filesWorker.WorkingDirPath( tboxSSToDir.Text.Trim() );
		}
		
		private void ConnectListsEventHandlers( bool bConnect ) {
			if( !bConnect ) {
				// отключаем обработчики событий для Списка (убираем "тормоза")
				this.listViewSource.DoubleClick -= new System.EventHandler(this.ListViewSourceDoubleClick);
				this.listViewSource.ItemChecked -= new System.Windows.Forms.ItemCheckedEventHandler(this.ListViewSourceItemChecked);
				this.listViewSource.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.ListViewSourceItemCheck);
				this.listViewSource.KeyPress -= new System.Windows.Forms.KeyPressEventHandler(this.ListViewSourceKeyPress);
			} else {
				// подключаем обработчики событий для Списка
				this.listViewSource.DoubleClick += new System.EventHandler(this.ListViewSourceDoubleClick);
				this.listViewSource.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.ListViewSourceItemChecked);
				this.listViewSource.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ListViewSourceItemCheck);
				this.listViewSource.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ListViewSourceKeyPress);
			}
		}
		
		// заполнение списка данными указанной папки
		private void GenerateSourceList(string dirPath) {
			bool IsFB2LibrusecGenres = m_bFullSort ? Settings.FileManagerSettings.FullSortingFB2LibrusecGenres
				: Settings.FileManagerSettings.SelectedSortingFB2LibrusecGenres;
			Core.FileManager.FileManagerWork.GenerateSourceList(
				dirPath, listViewSource, true, IsFB2LibrusecGenres, checkBoxTagsView.Checked, chBoxStartExplorerColumnsAutoReize.Checked
			);
		}

		// Отобразим результат сортировки
		private void SortingProgressData() {
			/*lvFilesCount.Items[0].SubItems[1].Text = Convert.ToString( m_sv.AllDirs );
            lvFilesCount.Items[1].SubItems[1].Text = Convert.ToString( m_sv.AllFiles );*/
			lvFilesCount.Items[2].SubItems[1].Text = Convert.ToString( m_sv.SourceFB2 );
			lvFilesCount.Items[3].SubItems[1].Text = Convert.ToString( m_sv.Zip );
			lvFilesCount.Items[4].SubItems[1].Text = Convert.ToString( m_sv.FB2FromZips );
			lvFilesCount.Items[5].SubItems[1].Text = Convert.ToString( m_sv.Other );
			lvFilesCount.Items[6].SubItems[1].Text = Convert.ToString( m_sv.CreateInTarget );
			lvFilesCount.Items[7].SubItems[1].Text = Convert.ToString( m_sv.NotRead );
			lvFilesCount.Items[8].SubItems[1].Text = Convert.ToString( m_sv.NotValidFB2 );
			lvFilesCount.Items[9].SubItems[1].Text = Convert.ToString( m_sv.BadZip );
			lvFilesCount.Items[10].SubItems[1].Text = Convert.ToString( m_sv.LongPath );
			lvFilesCount.Items[11].SubItems[1].Text = Convert.ToString( m_sv.NotSort );
		}
		
		// инициализация контролов и переменных
		private void Init() {
			for( int i=0; i!=lvFilesCount.Items.Count; ++i )
				lvFilesCount.Items[i].SubItems[1].Text	= "0";

			tsProgressBar.Value		= 0;
			tsslblProgress.Text		= Settings.Settings.GetReady();
			tsProgressBar.Visible	= false;
			// очистка временной папки
			filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
			m_sv.Clear();
		}
		
		// доступность контролов при Полной Сортировки
		private void SetFullSortingStartEnabled( bool bEnabled ) {
			tpSelectedSort.Enabled = panelTemplate.Enabled = panelAddress.Enabled = listViewSource.Enabled = bEnabled;
			buttonFullSortStop.Enabled = tsProgressBar.Visible = !bEnabled;
			tcSort.Refresh();
			ssProgress.Refresh();
		}
		
		// доступность контролов при Избранной Сортировки
		private void SetSelectedSortingStartEnabled( bool bEnabled ) {
			tpFullSort.Enabled = buttonSSortFilesTo.Enabled = pSelectedSortDirs.Enabled = pSSTemplate.Enabled = pSSData.Enabled = bEnabled;
			buttonSSortStop.Enabled = tsProgressBar.Visible = !bEnabled;
			tcSort.Refresh();
			ssProgress.Refresh();
		}
		
		// Полная Сортировка: проверка на корректность данных папок источника и приемника файлов
		private bool IsSourceDirDataCorrect()
		{
			// проверки на корректность папок источника и приемника
			if( getSourceDir().Length == 0 ) {
				MessageBox.Show( "Выберите папку для сканирования!", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			DirectoryInfo diFolder = new DirectoryInfo( getSourceDir() );
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка не найдена: " + getSourceDir(), m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка папки-приемника и создание ее, если нужно
			if( !filesWorker.CreateDirIfNeed( getTargetDir(), m_sMessTitle ) )
				return false;

			return true;
		}
		
		// Селективная Сортировка: проверка на корректность данных папок источника и приемника файлов
		private bool IsFoldersDataCorrect()
		{
			// проверки на корректность папок источника и приемника
			if( getSourceDir().Length == 0 ) {
				MessageBox.Show( "Выберите папку для сканирования!", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			DirectoryInfo diFolder = new DirectoryInfo( getSourceDir() );
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка не найдена: " + getSourceDir(), m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			if( getTargetDir().Length == 0 ) {
				MessageBox.Show( "Не указана папка-приемник файлов!\nРабота прекращена.", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			if( getSourceDir() == getTargetDir() ) {
				MessageBox.Show( "Папка-приемник файлов совпадает с папкой сканирования!\nРабота прекращена.", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка папки-приемника и создание ее, если нужно
			if( !filesWorker.CreateDirIfNeed( getTargetDir(), m_sMessTitle ) )
				return false;

			return true;
		}
		
		// чтение данных Полной Сортировки из xml-файла
		private void ReadFMTempData() {
			this.checkBoxTagsView.Click -= new System.EventHandler(this.CheckBoxTagsViewClick);
			checkBoxTagsView.Checked = Settings.FileManagerSettings.ReadXmlFullSortingBooksTagsView();
			this.checkBoxTagsView.Click += new System.EventHandler(this.CheckBoxTagsViewClick);
			
			textBoxAddress.Text = Settings.FileManagerSettings.ReadXmlFullSortingSourceDir();
			txtBoxTemplatesFromLine.Text = Settings.FileManagerSettings.ReadXmlFullSortingTemplate();
			chBoxScanSubDir.Checked = Settings.FileManagerSettings.ReadXmlFullSortingInSubDir();
			chBoxStartExplorerColumnsAutoReize.Checked = Settings.FileManagerSettings.ReadXmlFullSortingStartExplorerColumnsAutoReize();
			chBoxFSToZip.Checked = Settings.FileManagerSettings.ReadXmlFullSortingToZip();
			chBoxFSNotDelFB2Files.Checked = Settings.FileManagerSettings.ReadXmlFullSortingNotDelFB2Files();
			rbtnFMFSFB2Librusec.Checked = Settings.FileManagerSettings.ReadXmlFullSortingFB2Librusec();
			rbtnFMFSFB22.Checked = Settings.FileManagerSettings.ReadXmlFullSortingFB22();
			
			// чтение данных Избранной Сортировки из xml-файла
			tboxSSSourceDir.Text = Settings.FileManagerSettings.ReadXmlSelectedSortingSourceDir();
			tboxSSToDir.Text = Settings.FileManagerSettings.ReadXmlSelectedSortingTargetDir();
			txtBoxSSTemplatesFromLine.Text = Settings.FileManagerSettings.ReadXmlSelectedSortingTemplate();
			chBoxSSScanSubDir.Checked = Settings.FileManagerSettings.ReadXmlSelectedSortingInSubDir();
			chBoxSSToZip.Checked = Settings.FileManagerSettings.ReadXmlSelectedSortingToZip();
			chBoxSSNotDelFB2Files.Checked = Settings.FileManagerSettings.ReadXmlSelectedSortingNotDelFB2Files();

//			if(File.Exists(Settings.FileManagerSettings.FileManagerSettingsPath)) {
//				GenerateSourceList(Settings.FileManagerSettings.FullSortingSourceDir);
//			}
		}
		
		//=================================================================
		//							Полная Сортировка
		//=================================================================
		private void FullSorting( string FromFilePath, string SourceDir, string TargetDir,
		                         List<templatesLexemsSimple> lSLexems, Settings.DataFM dfm ) {
			#region Код
			bool NotDelOriginalFiles = m_bFullSort ? Settings.FileManagerSettings.FullSortingNotDelFB2Files
					: Settings.FileManagerSettings.SelectedSortingNotDelFB2Files;
			string SourceExt = Path.GetExtension( FromFilePath ).ToLower();
			if( SourceExt==".fb2" ) {
				// создание файла по новому пути для Жанра(ов) и Автора(ов) Книги из исходного fb2
				MakeFileForGenreAndAuthorFromFB2( FromFilePath, SourceDir, TargetDir, lSLexems, dfm );
				++m_sv.SourceFB2;
				
				// удаляем исходный fb2-файл, если включена эта опция
				if( !NotDelOriginalFiles ) {
					if( File.Exists( FromFilePath ) )
						File.Delete( FromFilePath );
				}
			} else if( SourceExt==".zip" ) {
				string TempDir = Settings.Settings.GetTempDir();
				long UnZipCount = sharpZipLib.UnZipFiles( FromFilePath, TempDir, 0, true, null, 4096 );
				List<string> FilesListFromZip = filesWorker.MakeFileListFromDir( TempDir, false, false );
				if (UnZipCount==-1) {
					// не получилось открыть архив - "битый"
					CopyBadZipToBadDir( FromFilePath, SourceDir, dfm.NotOpenArchDir, dfm.FileExistMode );
					++m_sv.BadZip;
					return;
				} else {
					++m_sv.Zip;
					if( FilesListFromZip==null ) {
						// в архиве нет fb2-файлов
						CopyBadZipToBadDir( FromFilePath, SourceDir, dfm.NotOpenArchDir, dfm.FileExistMode );
						++m_sv.BadZip;
						return;
					}
					foreach( string FB2FromArchPath in FilesListFromZip ) {
						// создание файла по новому пути для Жанра(ов) и Автора(ов) Книги из исходного fb2
						MakeFileForGenreAndAuthorFromFB2( FB2FromArchPath, TempDir, TargetDir, lSLexems, dfm );
						++m_sv.FB2FromZips;
					}
					
					// очистка временной папки
					foreach( string FB2FromArchPath in FilesListFromZip ) {
						if( File.Exists( FB2FromArchPath ) )
							File.Delete( FB2FromArchPath );
					}
				}
				
				// удаляем исходный zip-файл, если включена эта опция
				if( !NotDelOriginalFiles ) {
					if( File.Exists( FromFilePath ) )
						File.Delete( FromFilePath );
				}
			} else {
				// пропускаем не fb2-файлы и не zip-архивы
				++m_sv.Other;
			}
			#endregion
		}
		
		//=================================================================
		// 							Избранная Сортировка
		//=================================================================
		private void SelectedSorting( string FromFilePath, string SourceDir, string TargetDir,
		                             List<templatesLexemsSimple> lSLexems, Settings.DataFM dfm ) {
			#region Код
			bool NotDelOriginalFiles = m_bFullSort ? Settings.FileManagerSettings.FullSortingNotDelFB2Files
					: Settings.FileManagerSettings.SelectedSortingNotDelFB2Files;
			string TempDir = Settings.Settings.GetTempDir();
			string SourceExt = Path.GetExtension( FromFilePath ).ToLower();
			FB2SelectedSorting fb2ss = new FB2SelectedSorting();
			bool IsNotRead = false;
			if( SourceExt==".fb2" ) {
				if( fb2ss.IsConformity( FromFilePath, m_lSSQCList, out IsNotRead ) ) {
					// создание файла по новому пути для Жанра(ов) и Автора(ов) Книги из исходного fb2
					MakeFileForGenreAndAuthorFromFB2( FromFilePath, SourceDir, TargetDir, lSLexems, dfm );
					++m_sv.SourceFB2;
				} else {
					if (IsNotRead)
						CopyBadFileToDir( FromFilePath, SourceDir, dfm.NotReadFB2Dir, dfm.FileExistMode );
				}
				// удаляем исходный fb2-файл, если включена эта опция
				if( !NotDelOriginalFiles ) {
					if( File.Exists( FromFilePath ) )
						File.Delete( FromFilePath );
				}
			} else if( SourceExt==".zip" ) {
				long UnZipCount = sharpZipLib.UnZipFiles( FromFilePath, TempDir, 0, true, null, 4096 );
				List<string> FilesListFromZip = filesWorker.MakeFileListFromDir( TempDir, false, false );
				if (UnZipCount==-1) {
					// не получилось открыть архив - "битый"
					CopyBadZipToBadDir( FromFilePath, SourceDir, dfm.NotOpenArchDir, dfm.FileExistMode );
					++m_sv.BadZip;
					return;
				} else {
					++m_sv.Zip;
					if( FilesListFromZip==null ) {
						// в архиве нет fb2-файлов
						CopyBadZipToBadDir( FromFilePath, SourceDir, dfm.NotOpenArchDir, dfm.FileExistMode );
						++m_sv.BadZip;
						return;
					}
				}
				m_sv.FB2FromZips += FilesListFromZip.Count;
				foreach( string FB2FromZipPath in FilesListFromZip ) {
					// проверка, соответствует ли текущий файл критерия поиска для Избранной Сортировки
					if( fb2ss.IsConformity( FB2FromZipPath, m_lSSQCList, out IsNotRead ) ) {
						string FileFromZipExt = Path.GetExtension( FB2FromZipPath ).ToLower();
						if( FileFromZipExt==".fb2" ) {
							// создание файла по новому пути для Жанра(ов) и Автора(ов) Книги из исходного fb2
							MakeFileForGenreAndAuthorFromFB2( FB2FromZipPath, TempDir, TargetDir, lSLexems, dfm );
							++m_sv.SourceFB2;
							
							// удаляем исходный fb2-файл, если включена эта опция
							if( !NotDelOriginalFiles ) {
								if( File.Exists( FromFilePath ) )
									File.Delete( FromFilePath );
							}
						} else {
							// пропускаем не fb2-файлы и не zip-архивы
							++m_sv.Other;
						}
					} else {
						// fb2-файл не соответствует критериям сортировки
						++m_sv.NotSort;
						if (IsNotRead) // к тому же еще и не читается
							CopyBadFileToDir( FB2FromZipPath, TempDir, dfm.NotReadFB2Dir, dfm.FileExistMode );
					}
				}
				// очистка временной папки
				foreach( string FB2FromArchPath in FilesListFromZip ) {
					if( File.Exists( FB2FromArchPath ) )
						File.Delete( FB2FromArchPath );
				}
				
				// удаляем исходный zip-файл, если включена эта опция
				if( !NotDelOriginalFiles ) {
					if( File.Exists( FromFilePath ) )
						File.Delete( FromFilePath );
				}
			} else {
				// пропускаем не fb2-файлы и не zip-архивы
				++m_sv.Other;
			}
			#endregion
		}
		
		// создание файла по новому пути для Жанра(ов) и Автора(ов) Книги из исходного fb2
		private void MakeFileForGenreAndAuthorFromFB2( string FromFilePath, string SourceDir, string TargetDir,
		                                              List<templatesLexemsSimple> lSLexems, Settings.DataFM dfm ) {
			string Ext = Path.GetExtension( FromFilePath ).ToLower();
			if( dfm.GenreOneMode && dfm.AuthorOneMode ) {
				// по первому Жанру и первому Автору Книги
				MakeFileFor1Genre1AuthorWorker( Ext, FromFilePath, SourceDir, TargetDir, lSLexems, dfm );
			} else if( dfm.GenreOneMode && !dfm.AuthorOneMode ) {
				// по первому Жанру и всем Авторам Книги
				MakeFileFor1GenreAllAuthorWorker( Ext, FromFilePath, SourceDir, TargetDir, lSLexems, dfm );
			} else if( !dfm.GenreOneMode && dfm.AuthorOneMode ) {
				// по всем Жанрам и первому Автору Книги
				MakeFileForAllGenre1AuthorWorker( Ext, FromFilePath, SourceDir, TargetDir, lSLexems, dfm );
			} else {
				// по всем Жанрам и всем Авторам Книги
				MakeFileForAllGenreAllAuthorWorker( Ext, FromFilePath, SourceDir, TargetDir, lSLexems, dfm );
			}
		}
		//=================================================================

		private void MakeFileFor1Genre1AuthorWorker( string Ext, string FromFilePath, string SourceDir, string TargetDir,
		                                            List<templatesLexemsSimple> lSLexems, Settings.DataFM dfm ) {
			try {
				MakeFB2File( FromFilePath, SourceDir, TargetDir, lSLexems, dfm, 0, 0 );
			} catch {
				if( Ext==".fb2" )
					CopyBadFileToDir( FromFilePath, SourceDir, dfm.NotReadFB2Dir, dfm.FileExistMode );
			}
		}

		private void MakeFileForAllGenre1AuthorWorker( string Ext, string FromFilePath, string SourceDir, string TargetDir,
		                                              List<templatesLexemsSimple> lSLexems, Settings.DataFM dfm ) {
			try {
				fB2Parser fb2 = new fB2Parser( FromFilePath );
				TitleInfo ti = fb2.GetTitleInfo();
				IList<Genre> lGenres = ti.Genres;
				IList<Author> lAuthors = ti.Authors;
				for( int i=0; i!=lGenres.Count; ++i )
					MakeFB2File( FromFilePath, SourceDir, TargetDir, lSLexems, dfm, i, 0 );
			} catch {
				if( Ext==".fb2" )
					CopyBadFileToDir( FromFilePath, SourceDir, dfm.NotReadFB2Dir, dfm.FileExistMode );
			}
		}

		private void MakeFileFor1GenreAllAuthorWorker( string Ext, string FromFilePath, string SourceDir, string TargetDir,
		                                              List<templatesLexemsSimple> lSLexems, Settings.DataFM dfm ) {
			try {
				fB2Parser fb2 = new fB2Parser( FromFilePath );
				TitleInfo ti = fb2.GetTitleInfo();
				IList<Genre> lGenres = ti.Genres;
				IList<Author> lAuthors = ti.Authors;
				for( int i=0; i!=lAuthors.Count; ++i )
					MakeFB2File( FromFilePath, SourceDir, TargetDir, lSLexems, dfm, 0, i );
			} catch {
				if( Ext==".fb2" )
					CopyBadFileToDir( FromFilePath, SourceDir, dfm.NotReadFB2Dir, dfm.FileExistMode );
			}
		}

		private void MakeFileForAllGenreAllAuthorWorker( string Ext, string FromFilePath, string SourceDir, string TargetDir,
		                                                List<templatesLexemsSimple> lSLexems, Settings.DataFM dfm ) {
			try {
				fB2Parser fb2 = new fB2Parser( FromFilePath );
				TitleInfo ti = fb2.GetTitleInfo();
				IList<Genre> lGenres = ti.Genres;
				IList<Author> lAuthors = ti.Authors;
				for( int i=0; i!= lGenres.Count; ++i ) {
					for( int j=0; j!=lAuthors.Count; ++j )
						MakeFB2File( FromFilePath, SourceDir, TargetDir, lSLexems, dfm, i, j );
				}
			} catch {
				if( Ext==".fb2" )
					CopyBadFileToDir( FromFilePath, SourceDir, dfm.NotReadFB2Dir, dfm.FileExistMode );
			}
		}
		
		// если режим сортировки "только валидные" - то проверка и копирование невалидных в папку
		private bool IsValid( string FromFilePath, string SourceDir, Settings.DataFM dfm, int GenreIndex, int AuthorIndex ) {
			
			string Result = string.Empty;
			bool IsFB2LibrusecGenres = m_bFullSort ? Settings.FileManagerSettings.FullSortingFB2LibrusecGenres
				: Settings.FileManagerSettings.SelectedSortingFB2LibrusecGenres;
			Result = IsFB2LibrusecGenres ? fv2V.ValidatingFB2LibrusecFile( FromFilePath ) : fv2V.ValidatingFB22File( FromFilePath );
			if ( Result.Length != 0 ) {
				// защита от многократного копирования невалимдного файла в папку для невалидных
				if( GenreIndex==0 && AuthorIndex==0 ) {
					// помещаем его в папку для невалидных файлов
					CopyBadFileToDir( FromFilePath, SourceDir, dfm.NotValidFB2Dir, dfm.FileExistMode );
					++m_sv.NotValidFB2;
					return false; // файл невалидный - пропускаем его, сортируем дальше
				} else {
					return false; // файл уже скопирован - пропускаем его, сортируем дальше
				}
			}
			return true;
		}
		
		// нечитаемый fb2-файл или архив - копируем его в папку Bad
		private void CopyBadFileToDir( string FromFilePath, string SourceDir, string BadDir, int FileExistMode ) {
			Directory.CreateDirectory( BadDir );
			string FileName = FromFilePath.Remove( 0, SourceDir.Length );
			string ToFilePath = BadDir + (FileName.Substring(0,1)=="\\" ? string.Empty : "\\") + FileName;
			CopyFileToTargetDir( FromFilePath, ToFilePath, FileExistMode );
			++m_sv.NotRead;
		}
		
		// создаем файл по новому пути
		private void MakeFB2File( string FromFilePath, string SourceDir, string TargetDir, List<templatesLexemsSimple> lSLexems,
		                         Settings.DataFM dfm, int nGenreIndex, int AuthorIndex ) {
			string TempDir = Settings.Settings.GetTempDir();
			// смотрим, что это за файл
			string Ext = Path.GetExtension( FromFilePath ).ToLower();
			
			bool IsFB2LibrusecGenres = m_bFullSort ? Settings.FileManagerSettings.FullSortingFB2LibrusecGenres
				: Settings.FileManagerSettings.SelectedSortingFB2LibrusecGenres;
			if( Ext == ".fb2" ) {
				if( !dfm.SortValidType  ) {
					// тип сортировки
					if( !IsValid( FromFilePath, SourceDir, dfm, nGenreIndex, AuthorIndex ) )
						return;
				}
				try {
					string ToFilePath = TargetDir + "\\" +
						templatesParser.Parse( FromFilePath, lSLexems, IsFB2LibrusecGenres, dfm, nGenreIndex, AuthorIndex ) + ".fb2";
					CreateFileTo( FromFilePath, ToFilePath, dfm.FileExistMode, dfm );
				} catch /*( System.IO.FileLoadException )*/ {
					// нечитаемый fb2-файл - копируем его в папку Bad
					CopyBadFileToDir( FromFilePath, SourceDir, dfm.NotReadFB2Dir, dfm.FileExistMode );
				}
			}
		}
		
		// архивирование файла с сформированным именем (путь)
		private void CopyFileToArchive( string FromFilePath, string ToFilePath, int FileExistMode ) {
			// обработка уже существующих файлов в папке
			ToFilePath = FileExsistWorker( FromFilePath, ToFilePath, FileExistMode, true );
			sharpZipLib.ZipFile( FromFilePath, ToFilePath, 9, ICSharpCode.SharpZipLib.Zip.CompressionMethod.Deflated, 4096 );
		}
		
		// копирование файла с сформированным именем (путь)
		private void CopyFileToTargetDir( string FromFilePath, string ToFilePath, int FileExistMode )
		{
			// обработка уже существующих файлов в папке
			ToFilePath = FileExsistWorker( FromFilePath, ToFilePath, FileExistMode, false );
			if( File.Exists( FromFilePath ) )
				File.Copy( FromFilePath, ToFilePath );
		}
		
		// создание нового файла или архива
		private void CreateFileTo( string FromFilePath, string ToFilePath, int FileExistMode, Settings.DataFM dfm ) {
			bool ToZip = m_bFullSort ? Settings.FileManagerSettings.FullSortingToZip
				: Settings.FileManagerSettings.SelectedSortingToZip;
			string ToPath = ToFilePath;
			try {
				if( !ToZip )
					CopyFileToTargetDir( FromFilePath, ToPath, FileExistMode );
				else {
					// упаковка в архив
					ToPath += ".zip";
					CopyFileToArchive( FromFilePath, ToPath, FileExistMode );
				}
				if( File.Exists( ToPath ) )
					++m_sv.CreateInTarget;

			} catch ( System.IO.PathTooLongException ) {
				// файл с длинным путем (название книги слишком длинное...)
				string FileLongPathDir = dfm.FileLongPathDir;
				Directory.CreateDirectory( FileLongPathDir );
				ToFilePath = FileLongPathDir+"\\"+Path.GetFileName( FromFilePath );
				CopyFileToTargetDir( FromFilePath, ToFilePath, FileExistMode );
				++m_sv.LongPath;
			}
		}
		
		// обработка уже существующих файлов в папке
		private string FileExsistWorker( string FromFilePath, string ToFilePath, int FileExistMode, bool ToZip )
		{
			FileInfo fi = new FileInfo( ToFilePath );
			if( !fi.Directory.Exists )
				Directory.CreateDirectory( fi.Directory.ToString() );

			if( File.Exists( ToFilePath ) ) {
				if( FileExistMode == 0 )
					File.Delete( ToFilePath );
				else {
					string Sufix = filesWorker.createSufix( ToFilePath, FileExistMode );
					if( ToZip )
						ToFilePath = ToFilePath.Remove( ToFilePath.Length - 8 ) + Sufix + ".fb2.zip";
					else
						ToFilePath = ToFilePath.Remove( ToFilePath.Length - 4 ) + Sufix + ".fb2";
				}
			}
			return ToFilePath;
		}
		
		// копирование "битого" архива с сформированным именем (путь)
		private void CopyBadZipToBadDir( string FromFilePath, string SourceDir, string TargetDir, int FileExistMode )
		{
			string ToFilePath = TargetDir+"\\"+FromFilePath.Remove( 0, SourceDir.Length );
			FileInfo fi = new FileInfo( ToFilePath );
			if( !fi.Directory.Exists )
				Directory.CreateDirectory( fi.Directory.ToString() );

			// обработка уже существующих файлов в папке
			if( File.Exists( ToFilePath ) ) {
				if( FileExistMode == 0 )
					File.Delete( ToFilePath );
				else {
					ToFilePath = ToFilePath.Remove( ToFilePath.Length-4 )
						+ filesWorker.createSufix( ToFilePath, FileExistMode ) //Sufix
						+ Path.GetExtension( ToFilePath );
				}
			}
			if( File.Exists( FromFilePath ) ) {
				File.Copy( FromFilePath, ToFilePath );
			}
		}
		//------------------------------------------------------------------------------------------
		
		// проверки на корректность шаблонных строк
		private bool IsLineTemplateCorrect( string sLineTemplate ) {
			// проверка "пустоту" строки с шаблонами
			if( sLineTemplate.Length == 0 ) {
				MessageBox.Show( "Строка шаблонов не может быть пустой!\nРабота прекращена.", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на наличие недопустимого условного шаблона [*GROUP*]
			if( sLineTemplate.IndexOf("[*GROUP*]")!=-1 ) {
				MessageBox.Show( "Шаблон для Группы Жанров *GROUP* не миожет буть условным [*GROUP*]!\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на корректность строки с шаблонами
			if( !templatesVerify.IsLineTemplatesCorrect( sLineTemplate ) ) {
				MessageBox.Show( "Строка содержит или недопустимые шаблоны,\nили недопустимые символы */|?<>\"&\\t\\r\\n между шаблонами!\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на четность * в строке с шаблонами
			if( !templatesVerify.IsEvenElements( sLineTemplate, '*' ) ) {
				MessageBox.Show( "Строка с шаблонами подстановки содержит нечетное число *!\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка, не стоит ли ] перед [
			if( sLineTemplate.IndexOf('[')!=-1 && sLineTemplate.IndexOf(']')!=-1 ) {
				if( sLineTemplate.IndexOf('[') > sLineTemplate.IndexOf(']') ) {
					MessageBox.Show( "В строке с шаблонами закрывающая скобка ] не может стоять перед открывающей [ !\nРабота прекращена.",
					                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return false;
				}
			}
			// проверка на соответствие [ ] в строке с шаблонами
			if( !templatesVerify.IsBracketsCorrect( sLineTemplate, '[', ']' ) ) {
				MessageBox.Show( "В строке с шаблонами переименования нет соответствия между открывающим и закрывающими скобками [ ]!\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на соответствие ( ) в строке с шаблонами
			if( !templatesVerify.IsBracketsCorrect( sLineTemplate, '(', ')' ) ) {
				MessageBox.Show( "В строке с шаблонами нет соответствия между открывающим и закрывающими скобками ( )!\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на \ в начале строки с шаблонами
			if( sLineTemplate[0]=='\\' ) {
				MessageBox.Show( "Строка с шаблонами не может начинаться с '\\'!\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на \ в конце строки с шаблонами
			if( sLineTemplate[sLineTemplate.Length-1]=='\\' ) {
				MessageBox.Show( "Строка с шаблонами не может заканчиваться на '\\' !\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка условных шаблонов на наличие в них вспом. символов без самих шаблонов
			if( !templatesVerify.IsConditionalPatternCorrect( sLineTemplate ) ) {
				MessageBox.Show( "Условные шаблоны [] в строке с шаблонами не могут содержать вспомогательных символов БЕЗ самих шаблонов!\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на множественность символа папки \ в строке с шаблонами
			if( sLineTemplate.IndexOf( "\\\\" )!=-1 ) {
				MessageBox.Show( "Строка с шаблонами не может содержать несколько идущих подряд символов папки '\\' !\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			return true;
		}
		
		private void SetTemplateInInputControl(System.Windows.Forms.TextBox textBox, string Template) {
			int CursorPosition = textBox.SelectionStart;
			int NewPosition = CursorPosition + Template.Length;
			string TextBeforeCursor = textBox.Text.Substring(0, CursorPosition);
			string TextAfterCursor = textBox.Text.Substring(CursorPosition, textBox.TextLength-CursorPosition);
			textBox.Text = TextBeforeCursor + Template + TextAfterCursor;
			textBox.Focus();
			textBox.Select(NewPosition, 0);
		}
		
		#endregion
		
		#region Обработчики событий
		void BtnGroupClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.Group);
		}
		
		void BtnLetterFamilyClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.LetterFamily);
		}
		
		void BtnGroupGenreClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.GroupGenre);
		}
		
		void BtnLangClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.Language);
		}
		
		void BtnFamilyClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.Family);
		}
		
		void BtnNameClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.Name);
		}
		
		void BtnPatronimicClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.Patronimic);
		}
		
		void BtnGenreClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.Genre);
		}
		
		void BtnBookClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.BookTitle);
		}
		
		void BtnSequenceClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.Series);
		}
		
		void BtnSequenceNumberClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.SeriesNumber);
		}
		
		void BtnDirClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, @"\");
		}
		
		void BtnLeftBracketClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, "[");
		}
		
		void BtnRightBracketClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, "]");
		}
		
		void BtnSSLetterFamilyClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.LetterFamily);
		}
		
		void BtnSSGroupGenreClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.GroupGenre);
		}
		
		void BtnSSGroupClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.Group);
		}
		
		void BtnSSLangClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.Language);
		}
		
		void BtnSSFamilyClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.Family);
		}
		
		void BtnSSNameClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.Name);
		}
		
		void BtnSSPatronimicClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.Patronimic);
		}
		
		void BtnSSGenreClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.Genre);
		}
		
		void BtnSSBookClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.BookTitle);
		}
		
		void BtnSSSequenceClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.Series);
		}
		
		void BtnSSSequenceNumberClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.SeriesNumber);
		}

		void BtnSSDirClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, @"\");
		}
		
		void BtnSSLeftBracketClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, "[");
		}
		
		void BtnSSRightBracketClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, "]");
		}
		
		void TsmiColumnsExplorerAutoReizeClick(object sender, EventArgs e)
		{
			Core.FileManager.FileManagerWork.AutoResizeColumns(listViewSource);
		}
		
		// Отображать/скрывать описание книг
		void CheckBoxTagsViewClick(object sender, EventArgs e)
		{
			if(checkBoxTagsView.Checked) {
				if(File.Exists(Settings.FileManagerSettings.FileManagerSettingsPath)) {
					if(Settings.FileManagerSettings.ReadXmlFullSortingViewMessageForLongTime()) {
						string Mess = "При включении этой опции для создания списка книг с их описанием может потребоваться очень много времени!\nБольше не показывать это сообщение?";
						DialogResult result = MessageBox.Show( Mess, "Отображение описания книг", MessageBoxButtons.YesNo, MessageBoxIcon.Question );
						Settings.FileManagerSettings.ViewMessageForLongTime = (result == DialogResult.Yes) ? false : true;
					}
				}
			}
			
			Settings.FileManagerSettings.BooksTagsView = checkBoxTagsView.Checked;
			Settings.FileManagerSettings.WriteFileManagerSettings();
			if( listViewSource.Items.Count > 0 ) {
				Cursor.Current = Cursors.WaitCursor;
				listViewSource.BeginUpdate();
				DirectoryInfo di = null;
				FB2BookDescription bd = null;
				Settings.DataFM dfm = new Settings.DataFM();
				bool IsFB2LibrusecGenres = Settings.FileManagerSettings.FullSortingFB2LibrusecGenres;
				string TempDir = Settings.Settings.GetTempDir();
				for(int i=0; i!= listViewSource.Items.Count; ++i) {
					ListViewItemType it = (ListViewItemType)listViewSource.Items[i].Tag;
					if(it.Type=="f") {
						di = new DirectoryInfo(it.Value);
						if(checkBoxTagsView.Checked) {
							// показать данные книг
							try {
								if(di.Extension.ToLower()==".fb2") {
									// показать данные fb2 файлов
									bd = new FB2BookDescription( it.Value );
									listViewSource.Items[i].SubItems[1].Text = m_space+bd.TIBookTitle+m_space;
									listViewSource.Items[i].SubItems[2].Text = m_space+bd.TISequences+m_space;
									listViewSource.Items[i].SubItems[3].Text = m_space+bd.TIAuthors+m_space;
									listViewSource.Items[i].SubItems[4].Text = m_space+Core.FileManager.FileManagerWork.CyrillicGenreName(IsFB2LibrusecGenres, bd.TIGenres)+m_space;
									listViewSource.Items[i].SubItems[5].Text = m_space+bd.TILang+m_space;
									listViewSource.Items[i].SubItems[6].Text = m_space+bd.Encoding+m_space;
								} else if(di.Extension.ToLower()==".zip") {
									if(checkBoxTagsView.Checked) {
										// показать данные архивов
										filesWorker.RemoveDir( TempDir );
										sharpZipLib.UnZipFiles(it.Value, TempDir, 0, true, null, 4096);
										string [] files = Directory.GetFiles( TempDir );
										bd = new FB2BookDescription( files[0] );
										listViewSource.Items[i].SubItems[1].Text = m_space+bd.TIBookTitle+m_space;
										listViewSource.Items[i].SubItems[2].Text = m_space+bd.TISequences+m_space;
										listViewSource.Items[i].SubItems[3].Text = m_space+bd.TIAuthors+m_space;
										listViewSource.Items[i].SubItems[4].Text = m_space+Core.FileManager.FileManagerWork.CyrillicGenreName(IsFB2LibrusecGenres, bd.TIGenres)+m_space;
										listViewSource.Items[i].SubItems[5].Text = m_space+bd.TILang+m_space;
										listViewSource.Items[i].SubItems[6].Text = m_space+bd.Encoding+m_space;
									}
								}
							} catch(System.Exception) {
								listViewSource.Items[i].ForeColor = Color.Blue;
							}
						} else {
							// скрыть данные книг
							for(int j=1; j!=listViewSource.Items[i].SubItems.Count; ++j) {
								listViewSource.Items[i].SubItems[j].Text = "";
							}
						}
					}
				}
				// очистка временной папки
				filesWorker.RemoveDir( TempDir );
				// авторазмер колонок Списка
				if(chBoxStartExplorerColumnsAutoReize.Checked) {
					Core.FileManager.FileManagerWork.AutoResizeColumns(listViewSource);
				}
				listViewSource.EndUpdate();
				Cursor.Current = Cursors.Default;
			}
		}
		
		// задание папки с fb2-файлами и архивами для сканирования
		void ButtonOpenSourceDirClick(object sender, EventArgs e)
		{
			if(filesWorker.OpenDirDlg( textBoxAddress, fbdScanDir, "Укажите папку для сканирования с fb2-файлами и архивами:" )) {
				ButtonGoClick(sender, e);
			}
		}
		
		// переход на заданную папку-источник fb2-файлов
		void ButtonGoClick(object sender, EventArgs e)
		{
			string s = textBoxAddress.Text.Trim();
			if(s != string.Empty) {
				DirectoryInfo info = new DirectoryInfo(s);
				if(info.Exists)
					GenerateSourceList(info.FullName);
				else
					MessageBox.Show( "Не удается найти папку " + textBoxAddress.Text + ".\nПроверьте правильность пути.", "Переход по выбранному адресу", MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
		}
		
		// обработка нажатия клавиш в поле ввода пути к папке-источнику
		void TextBoxAddressKeyPress(object sender, KeyPressEventArgs e)
		{
			if ( e.KeyChar == (char)Keys.Return ) {
				// отображение папок и/или фалов в заданной папке
				ButtonGoClick( sender, e );
			}
		}
		
		// переход в выбранную папку
		void ListViewSourceDoubleClick(object sender, EventArgs e)
		{
			if( listViewSource.Items.Count > 0 && listViewSource.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = listViewSource.SelectedItems;
				ListViewItemType it = (ListViewItemType)si[0].Tag;
				if(it.Type=="d" || it.Type=="dUp") {
					textBoxAddress.Text = it.Value;
					GenerateSourceList(it.Value);
				}
			}
		}
		
		// обработка нажатия клавиш на списке папок и файлов
		void ListViewSourceKeyPress(object sender, KeyPressEventArgs e)
		{
			if ( e.KeyChar == (char)Keys.Return ) {
				// переход в выбранную папку
				ListViewSourceDoubleClick(sender, e);
			} else if ( e.KeyChar == (char)Keys.Back ) {
				// переход на каталог выше
				ListViewItemType it = (ListViewItemType)listViewSource.Items[0].Tag;
				textBoxAddress.Text = it.Value;
				GenerateSourceList(it.Value);
			}
		}
		
		// Пометить все файлы и папки
		void TsmiCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			m_mscLV.CheckAllListViewItems( listViewSource, true );
			if(listViewSource.Items.Count > 0) {
				listViewSource.Items[0].Checked = false;
			}
			ConnectListsEventHandlers( true );
		}
		
		// Снять отметки со всех файлов и папок
		void TsmiUnCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			m_mscLV.UnCheckAllListViewItems( listViewSource.CheckedItems );
			ConnectListsEventHandlers( true );
		}
		
		// Пометить все файлы
		void TsmiFilesCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			m_mscLV.CheckAllFiles(listViewSource, true);
			ConnectListsEventHandlers( true );
		}
		
		// Снять пометки со всех файлов
		void TsmiFilesUnCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			m_mscLV.UnCheckAllFiles(listViewSource);
			ConnectListsEventHandlers( true );
		}
		
		// Пометить все папки
		void TsmiDirCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			m_mscLV.CheckAllDirs(listViewSource, true);
			ConnectListsEventHandlers( true );
		}
		
		// Снять пометки со всех папок
		void TsmiDirUnCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			m_mscLV.UnCheckAllDirs(listViewSource);
			ConnectListsEventHandlers( true );
		}
		
		// Пометить все fb2 файлы
		void TsmiFB2CheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			m_mscLV.CheckTypeAllFiles(listViewSource, "fb2", true);
			ConnectListsEventHandlers( true );
		}
		
		// Снять пометки со всех fb2 файлов
		void TsmiFB2UnCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			m_mscLV.UnCheckTypeAllFiles(listViewSource, "fb2");
			ConnectListsEventHandlers( true );
		}
		
		// Пометить все zip файлы
		void TsmiZipCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			m_mscLV.CheckTypeAllFiles(listViewSource, "zip", true);
			ConnectListsEventHandlers( true );
		}
		
		// Снять пометки со всех zip файлов
		void TsmiZipUnCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			m_mscLV.UnCheckTypeAllFiles(listViewSource, "zip");
			ConnectListsEventHandlers( true );
		}
		
		// Пометить всё выделенное
		void TsmiCheckedAllSelectedClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			m_mscLV.ChekAllSelectedItems(listViewSource, true);
			ConnectListsEventHandlers( true );
			listViewSource.Focus();
		}
		
		// Снять пометки со всего выделенного
		void TsmiUnCheckedAllSelectedClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			m_mscLV.ChekAllSelectedItems(listViewSource, false);
			ConnectListsEventHandlers( true );
			listViewSource.Focus();
		}
		
		void ListViewSourceItemCheck(object sender, ItemCheckEventArgs e)
		{
			if( listViewSource.Items.Count > 0 && listViewSource.SelectedItems.Count != 0 ) {
				// при двойном клике на папке ".." пометку не ставим
				if(e.Index == 0) // ".."
					e.NewValue = CheckState.Unchecked;
			}
		}
		
		// пометка/снятие пометки по check на 0-й item - папка ".."
		void ListViewSourceItemChecked(object sender, ItemCheckedEventArgs e)
		{
			if( listViewSource.Items.Count > 0 ) {
				ListViewItemType it = (ListViewItemType)e.Item.Tag;
				if(it.Type=="dUp") {
					ConnectListsEventHandlers( false );
					if(e.Item.Checked)
						m_mscLV.CheckAllListViewItems( listViewSource, true );
					else
						m_mscLV.UnCheckAllListViewItems( listViewSource.CheckedItems );;
					ConnectListsEventHandlers( true );
				}
			}
		}
		
		// ********* Полная сортировка *************
		void ButtonSortFilesToClick(object sender, EventArgs e)
		{
			// обработка заданных каталого
			Settings.FileManagerSettings.FullSortingSourceDir = getSourceDir();
			
			m_bFullSort = true;
			m_bScanSubDirs = chBoxScanSubDir.Checked ? true : false;
			m_sLineTemplate	= txtBoxTemplatesFromLine.Text.Trim();
			m_sMessTitle	= "SharpFBTools - Полная Сортировка";
			// проверка на корректность данных папок источника и приемника файлов
			if( !IsSourceDirDataCorrect() )
				return;

			// приведение к одному виду шаблонов
			m_sLineTemplate = templatesVerify.ToOneTemplateType( @m_sLineTemplate );
			// проверки на корректность шаблонных строк
			if( !IsLineTemplateCorrect( m_sLineTemplate ) )
				return;
			
			// инициализация контролов
			Init();
			SetFullSortingStartEnabled( false );

			m_dtStart = DateTime.Now;
			tsslblProgress.Text = "Создание списка файлов:";
			
			// Запуск процесса DoWork от Бекграунд Воркера
			if( m_bw.IsBusy != true )
				m_bw.RunWorkerAsync(); //если не занят то запустить процесс
		}
		
		void ChBoxFSToZipClick(object sender, EventArgs e)
		{
			Settings.FileManagerSettings.FullSortingToZip = chBoxFSToZip.Checked;
			Settings.FileManagerSettings.WriteFileManagerSettings();
		}
		
		void ChBoxFSNotDelFB2FilesClick(object sender, EventArgs e)
		{
			Settings.FileManagerSettings.FullSortingNotDelFB2Files = chBoxFSNotDelFB2Files.Checked;
			Settings.FileManagerSettings.WriteFileManagerSettings();
		}
		
		void RbtnFMFSFB2LibrusecClick(object sender, EventArgs e)
		{
			Settings.FileManagerSettings.FullSortingFB2LibrusecGenres = rbtnFMFSFB2Librusec.Checked;
			Settings.FileManagerSettings.FullSortingFB22Genres = rbtnFMFSFB22.Checked;
			Settings.FileManagerSettings.WriteFileManagerSettings();
		}
		
		void RbtnFMFSFB22Click(object sender, EventArgs e)
		{
			Settings.FileManagerSettings.FullSortingFB2LibrusecGenres = rbtnFMFSFB2Librusec.Checked;
			Settings.FileManagerSettings.FullSortingFB22Genres = rbtnFMFSFB22.Checked;
			Settings.FileManagerSettings.WriteFileManagerSettings();
		}
		
		void ChBoxSSToZipClick(object sender, EventArgs e)
		{
			Settings.FileManagerSettings.SelectedSortingToZip = chBoxSSToZip.Checked;
			Settings.FileManagerSettings.WriteFileManagerSettings();
		}
		
		void ChBoxSSNotDelFB2FilesClick(object sender, EventArgs e)
		{
			Settings.FileManagerSettings.SelectedSortingNotDelFB2Files = chBoxSSNotDelFB2Files.Checked;
			Settings.FileManagerSettings.WriteFileManagerSettings();
		}
		
		void ChBoxScanSubDirClick(object sender, EventArgs e)
		{
			Settings.FileManagerSettings.FullSortingInSubDir = chBoxScanSubDir.Checked;
			Settings.FileManagerSettings.WriteFileManagerSettings();
		}

		void ChBoxSSScanSubDirClick(object sender, EventArgs e)
		{
			Settings.FileManagerSettings.SelectedSortingInSubDir = chBoxSSScanSubDir.Checked;
			Settings.FileManagerSettings.WriteFileManagerSettings();
		}
		
		void TextBoxAddressTextChanged(object sender, EventArgs e)
		{
			Settings.FileManagerSettings.FullSortingSourceDir = textBoxAddress.Text;
		}
		
		void TxtBoxTemplatesFromLineTextChanged(object sender, EventArgs e)
		{
			Settings.FileManagerSettings.FullSortingTemplate = txtBoxTemplatesFromLine.Text;
		}
		
		void TboxSSSourceDirTextChanged(object sender, EventArgs e)
		{
			Settings.FileManagerSettings.SelectedSortingSourceDir = tboxSSSourceDir.Text;
		}
		
		void TboxSSToDirTextChanged(object sender, EventArgs e)
		{
			Settings.FileManagerSettings.SelectedSortingTargetDir = tboxSSToDir.Text;
		}
		
		void TxtBoxSSTemplatesFromLineTextChanged(object sender, EventArgs e)
		{
			Settings.FileManagerSettings.SelectedSortingTemplate = txtBoxSSTemplatesFromLine.Text;
		}
		
		// запуск диалога Вставки готовых шаблонов
		void BtnInsertTemplatesClick(object sender, EventArgs e)
		{
			Core.BookSorting.BasicTemplates btfrm = new Core.BookSorting.BasicTemplates();
			btfrm.ShowDialog();
			if( btfrm.GetTemplateLine()!=null )
				txtBoxTemplatesFromLine.Text = btfrm.GetTemplateLine();

			btfrm.Dispose();
		}
		
		// запуск диалога Сбора данных для Избранной Сортировки
		void BtnSSGetDataClick(object sender, EventArgs e)
		{
			#region Код
			Core.BookSorting.SelectedSortData ssdfrm = new Core.BookSorting.SelectedSortData();
			// если в основном списке критериев поиска уже есть записи, то копируем их в форму сбора данных
			if( lvSSData.Items.Count > 0 ) {
				for( int i=0; i!=lvSSData.Items.Count; ++i ) {
					ListViewItem lvi = new ListViewItem( lvSSData.Items[i].Text );
					lvi.SubItems.Add( lvSSData.Items[i].SubItems[1].Text );
					lvi.SubItems.Add( lvSSData.Items[i].SubItems[2].Text );
					lvi.SubItems.Add( lvSSData.Items[i].SubItems[3].Text );
					lvi.SubItems.Add( lvSSData.Items[i].SubItems[4].Text );
					lvi.SubItems.Add( lvSSData.Items[i].SubItems[5].Text );
					lvi.SubItems.Add( lvSSData.Items[i].SubItems[6].Text );
					lvi.SubItems.Add( lvSSData.Items[i].SubItems[7].Text );
					lvi.SubItems.Add( lvSSData.Items[i].SubItems[8].Text );
					lvi.SubItems.Add( lvSSData.Items[i].SubItems[9].Text );
					lvi.SubItems.Add( lvSSData.Items[i].SubItems[10].Text );
					ssdfrm.lvSSData.Items.Add( lvi );
				}
				ssdfrm.lblCount.Text = Convert.ToString( lvSSData.Items.Count );
			}

			ssdfrm.ShowDialog();
			if( ssdfrm.IsOKClicked() ) {
				/* обрабатываем собранные данные */
				if( ssdfrm.lvSSData.Items.Count > 0 ) {
					// удаляем записи в списке, если они есть
					lvSSData.Items.Clear();
					m_lSSQCList = new List<selectedSortQueryCriteria>();
					string sLang, sLast, sFirst, sMiddle, sNick, sGGroup, sGenre, sSequence, sBTitle, sExactFit, sGenreScheme;
					bool IsFB2LibrusecGenres = Settings.FileManagerSettings.SelectedSortingFB2LibrusecGenres;
					FB2SelectedSorting fb2ss = new FB2SelectedSorting();
					for( int i=0; i!=ssdfrm.lvSSData.Items.Count; ++i ) {
						sLang	= ssdfrm.lvSSData.Items[i].Text;
						sGGroup	= ssdfrm.lvSSData.Items[i].SubItems[1].Text;
						sGenre	= ssdfrm.lvSSData.Items[i].SubItems[2].Text;
						sLast	= ssdfrm.lvSSData.Items[i].SubItems[3].Text;
						sFirst	= ssdfrm.lvSSData.Items[i].SubItems[4].Text;
						sMiddle	= ssdfrm.lvSSData.Items[i].SubItems[5].Text;
						sNick	= ssdfrm.lvSSData.Items[i].SubItems[6].Text;
						sSequence	= ssdfrm.lvSSData.Items[i].SubItems[7].Text;
						sBTitle		= ssdfrm.lvSSData.Items[i].SubItems[8].Text;
						sExactFit	= ssdfrm.lvSSData.Items[i].SubItems[9].Text;
						sGenreScheme = ssdfrm.lvSSData.Items[i].SubItems[10].Text;
						ListViewItem lvi = new ListViewItem( sLang );
						lvi.SubItems.Add( sGGroup );
						lvi.SubItems.Add( sGenre );
						lvi.SubItems.Add( sLast );
						lvi.SubItems.Add( sFirst );
						lvi.SubItems.Add( sMiddle );
						lvi.SubItems.Add( sNick );
						lvi.SubItems.Add( sSequence );
						lvi.SubItems.Add( sBTitle );
						lvi.SubItems.Add( sExactFit );
						lvi.SubItems.Add( sGenreScheme );
						// добавление записи в список
						lvSSData.Items.Add( lvi );
						// заполняем список критериев поиска для Избранной Сортировки
						m_lSSQCList.AddRange( fb2ss.MakeSelectedSortQuerysList( sLang, sLast, sFirst, sMiddle, sNick,
						                                                       sGGroup, sGenre, sSequence, sBTitle,
						                                                       sExactFit, IsFB2LibrusecGenres ) );
					}
				}
			}
			
			ssdfrm.Dispose();
			#endregion
		}
		
		// запуск диалога Вставки готовых шаблонов
		void BtnSSInsertTemplatesClick(object sender, EventArgs e)
		{
			Core.BookSorting.BasicTemplates btfrm = new Core.BookSorting.BasicTemplates();
			btfrm.ShowDialog();
			if( btfrm.GetTemplateLine()!= null )
				txtBoxSSTemplatesFromLine.Text = btfrm.GetTemplateLine();

			btfrm.Dispose();
		}
		
		// задание папки с fb2-файлами и архивами для сканирования (Избранная Сортировка)
		void BtnSSOpenDirClick(object sender, EventArgs e)
		{
			filesWorker.OpenDirDlg( tboxSSSourceDir, fbdScanDir, "Укажите папку для сканирования с fb2-файлами и архивами (Избранная Сортировка):" );
		}
		
		// задание папки-приемника для размешения отсортированных файлов (Избранная Сортировка)
		void BtnSSTargetDirClick(object sender, EventArgs e)
		{
			filesWorker.OpenDirDlg( tboxSSToDir, fbdScanDir, "Укажите папку-приемник для размешения отсортированных файлов (Избранная Сортировка):" );
		}
		
		// Остановка выполнения процесса Полной Сортировки
		void ButtonFullSortStopClick(object sender, EventArgs e)
		{
			if( m_bw.WorkerSupportsCancellation == true )
				m_bw.CancelAsync();
		}
		
		// Остановка выполнения процесса Избранной Сортировки
		void ButtonSSortStopClick(object sender, EventArgs e)
		{
			if( m_bw.WorkerSupportsCancellation == true )
				m_bw.CancelAsync();
		}
		
		// ********* Избранная Сортировка ***********
		void ButtonSSortFilesToClick(object sender, EventArgs e)
		{
			m_bFullSort = false;
			
			// обработка заданных каталогов
			Settings.FileManagerSettings.SelectedSortingSourceDir = getSourceDir();
			
			m_bScanSubDirs = chBoxSSScanSubDir.Checked ? true : false;
			m_sLineTemplate = txtBoxSSTemplatesFromLine.Text.Trim();
			m_sMessTitle = "SharpFBTools - Избранная Сортировка";
			// проверка на корректность данных папок источника и приемника файлов
			if( !IsFoldersDataCorrect() ) {
				return;
			}
			// проверка на наличие критериев поиска для Избранной Сортировки
			if( lvSSData.Items.Count == 0 ) {
				MessageBox.Show( "Задайте хоть один критерий для Избранной Сортировки (кнопка \"Собрать данные для Избранной Сортировки\")!", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				btnSSGetData.Focus();
				return;
			}

			// приведение к одному виду шаблонов
			m_sLineTemplate = templatesVerify.ToOneTemplateType( @m_sLineTemplate );
			// проверки на корректность шаблонных строк
			if( !IsLineTemplateCorrect( m_sLineTemplate ) )
				return;
			
			// инициализация контролов
			Init();
			SetSelectedSortingStartEnabled( false );

			m_dtStart = DateTime.Now;
			tsslblProgress.Text = "Создание списка файлов:";
			
			// Запуск процесса DoWork от Бекграунд Воркера
			if( m_bw.IsBusy != true )
				m_bw.RunWorkerAsync(); //если не занят то запустить процесс
		}
		
		// сохранить список критериев Избранной Сортировки в файл
		void BtnSSDataListSaveClick(object sender, EventArgs e)
		{
			string sMessTitle = "SharpFBTools - Избранная Сортировка";
			if( lvSSData.Items.Count == 0 ) {
				MessageBox.Show( "Список данных для Избранной Сортировки пуст.\nЗадайте хоть один критерий Сортировки (кнопка 'Собрать данные для Избранной Сортировки').",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			sfdSaveXMLFile.Filter = "SharpFBTools файлы (*.xml)|*.xml|Все файлы (*.*)|*.*";;
			sfdSaveXMLFile.FileName = "";
			DialogResult result = sfdSaveXMLFile.ShowDialog();
			if( result == DialogResult.OK ) {
				XmlWriter writer = null;
				try {
					XmlWriterSettings settings = new XmlWriterSettings();
					settings.Indent = true;
					settings.IndentChars = ("\t");
					settings.OmitXmlDeclaration = true;
					writer = XmlWriter.Create( sfdSaveXMLFile.FileName, settings );
					writer.WriteStartElement( "SelectedSortingData" );
					for( int i=0; i!=lvSSData.Items.Count; ++i ) {
						writer.WriteStartElement( "Item" );
						writer.WriteAttributeString( "Lang", lvSSData.Items[i].Text );
						writer.WriteAttributeString( "GGroup", lvSSData.Items[i].SubItems[1].Text );
						writer.WriteAttributeString( "Genre", lvSSData.Items[i].SubItems[2].Text );
						writer.WriteAttributeString( "Last", lvSSData.Items[i].SubItems[3].Text );
						writer.WriteAttributeString( "First", lvSSData.Items[i].SubItems[4].Text );
						writer.WriteAttributeString( "Middle", lvSSData.Items[i].SubItems[5].Text );
						writer.WriteAttributeString( "Nick", lvSSData.Items[i].SubItems[6].Text );
						writer.WriteAttributeString( "Sequence", lvSSData.Items[i].SubItems[7].Text );
						writer.WriteAttributeString( "BookTitle", lvSSData.Items[i].SubItems[8].Text );
						writer.WriteAttributeString( "ExactFit", lvSSData.Items[i].SubItems[9].Text );
						writer.WriteEndElement();
					}
					writer.WriteEndElement();
					writer.Flush();
					MessageBox.Show( "Список данных для Избранной Сортировки сохранен в файл!", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				}  finally  {
					if (writer != null)
						writer.Close();
				}
			}
		}
		
		// загрузить список критериев Избранной Сортировки из файла
		void BtnSSDataListLoadClick(object sender, EventArgs e)
		{
			sfdOpenXMLFile.Filter = "SharpFBTools файлы (*.xml)|*.xml|Все файлы (*.*)|*.*";
			sfdOpenXMLFile.FileName = "";
			DialogResult result = sfdOpenXMLFile.ShowDialog();
			if( result == DialogResult.OK ) {
				XmlReaderSettings xml = new XmlReaderSettings();
				xml.IgnoreWhitespace = true;
				using ( XmlReader reader = XmlReader.Create( sfdOpenXMLFile.FileName, xml ) ) {
					try {
						reader.ReadToFollowing("Item");
						if( reader.HasAttributes ) {
							// удаляем записи в списке, если они есть
							lvSSData.Items.Clear();
							DataFM dfm = new DataFM();
							FB2SelectedSorting fb2ss = new FB2SelectedSorting();
							m_lSSQCList = new List<selectedSortQueryCriteria>();
							string sLang, sLast, sFirst, sMiddle, sNick, sGGroup, sGenre, sSequence, sBTitle, sExactFit;
							do {
								sLang		= reader.GetAttribute("Lang");
								sLast		= reader.GetAttribute("Last");
								sFirst		= reader.GetAttribute("First");
								sMiddle		= reader.GetAttribute("Middle");
								sNick		= reader.GetAttribute("Nick");
								sGGroup		= reader.GetAttribute("GGroup");
								sGenre		= reader.GetAttribute("Genre");
								sSequence	= reader.GetAttribute("Sequence");
								sBTitle		= reader.GetAttribute("BookTitle");
								sExactFit	= reader.GetAttribute("ExactFit");
								
								ListViewItem lvi = new ListViewItem( sLang );
								lvi.SubItems.Add( sGGroup );
								lvi.SubItems.Add( sGenre );
								lvi.SubItems.Add( sLast );
								lvi.SubItems.Add( sFirst );
								lvi.SubItems.Add( sMiddle );
								lvi.SubItems.Add( sNick );
								lvi.SubItems.Add( sSequence );
								lvi.SubItems.Add( sBTitle );
								lvi.SubItems.Add( sExactFit );
								// добавление записи в список
								lvSSData.Items.Add( lvi );
								// заполняем список критериев поиска для Избранной Сортировки
								m_lSSQCList.AddRange( fb2ss.MakeSelectedSortQuerysList( sLang, sLast, sFirst, sMiddle, sNick,
								                                                       sGGroup, sGenre, sSequence, sBTitle,
								                                                       sExactFit, dfm.SSGenresFB2LibrusecScheme ) );
							} while( reader.ReadToNextSibling("Item") );
						}
					} catch {
						MessageBox.Show( "Поврежден список данных для Избранной Сортировки:\n\""+sfdOpenXMLFile.FileName+"\".", "SharpFBTools - Избранная Сортировка", MessageBoxButtons.OK, MessageBoxIcon.Warning );
					} finally {
						reader.Close();
					}
				}
			}
		}
		
		void ChBoxStartExplorerColumnsAutoReizeCheckedChanged(object sender, EventArgs e)
		{
			Settings.FileManagerSettings.StartExplorerColumnsAutoReize = chBoxStartExplorerColumnsAutoReize.Checked;
			if(chBoxStartExplorerColumnsAutoReize.Checked)
				Core.FileManager.FileManagerWork.AutoResizeColumns(listViewSource);
		}
		#endregion
	}
}
