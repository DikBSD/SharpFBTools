/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 16.03.2009
 * Time: 10:03
 * 
 * License: GPL 2.1
 */

using System;
using System.IO;
using System.Xml;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Core.Misc;
using Core.FB2Dublicator;
using Core.FB2.Description.Common;
using Core.FB2.Description.TitleInfo;
using Core.FB2.Description.CustomInfo;

using fB2Parser 		= Core.FB2.FB2Parsers.FB2Parser;
using filesWorker		= Core.FilesWorker.FilesWorker;
using archivesWorker	= Core.FilesWorker.Archiver;

namespace SharpFBTools.Tools
{
	/// <summary>
	/// Description of SFBTpFB2Dublicator.
	/// </summary>
	public partial class SFBTpFB2Dublicator : UserControl
	{
		#region Закрытые данные класса
		private StatusView m_sv 		= null; 
		private DateTime m_dtStart;
        private BackgroundWorker m_bw	= null;
        private string m_sSource		= "";
        private bool m_bScanSubDirs		= true;
        private string m_sMessTitle		= "";
		private List<string> m_lFilesList	= null; // список всех проверяемых файлов
		private List<string> m_lDupFiles	= null; // список файлов, имеющих копии, соответственно условию сравнения
		private bool m_bCheckValid			= false;	// проверять или нет fb2-файл на валидность
		#endregion
		
		public SFBTpFB2Dublicator()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			InitializeBackgroundWorker();
			
			Init();
			// читаем сохраненные пути к папкам Поиска одинаковых fb2-файлов, если они есть
			ReadFB2DupTempData();
			m_sv = new StatusView();
			cboxMode.SelectedIndex = 0; // Условия для Сравнения fb2-файлов: Автор(ы) и Название Книги
		}

		#region Закрытые методы реализации BackgroundWorker
		private void InitializeBackgroundWorker() {
			// Инициализация перед использование BackgroundWorker 
            m_bw = new BackgroundWorker();
            m_bw.WorkerReportsProgress		= true; // Позволить выводить прогресс процесса
            m_bw.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
            m_bw.DoWork 			+= new DoWorkEventHandler( bw_DoWork );
            m_bw.ProgressChanged 	+= new ProgressChangedEventHandler( bw_ProgressChanged );
            m_bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler( bw_RunWorkerCompleted );
		}
		
		private void bw_DoWork( object sender, DoWorkEventArgs e ) {
			// поиск одинаковых fb2-файлов
			List<string> lDirList = new List<string>();
			if( !m_bScanSubDirs ) {
				// сканировать только указанную папку
				lDirList.Add( m_sSource );
				lvFilesCount.Items[0].SubItems[1].Text = "1";
			} else {
				// сканировать и все подпапки
				lDirList = filesWorker.DirsParser( m_sSource, lvFilesCount, false );
			}
			
			// не сортированный список всех файлов
			m_lFilesList = filesWorker.AllFilesParser( m_bw, e, lDirList, lvFilesCount, tsProgressBar, false );
			lDirList.Clear();
			// Проверить флаг на остановку процесса 
			if( ( m_bw.CancellationPending == true ) ) {
				e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
				return;
			}
			
			// проверка, есть ли хоть один файл в папке для сканирования
			if( m_lFilesList.Count == 0 ) {
				MessageBox.Show( "В папке сканирования не найдено ни одного файла!\nРабота прекращена.", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				tsslblProgress.Text = Settings.Settings.GetReady();
				SetSearchFB2DupStartEnabled( true );
				return;
			}
			
			tsslblProgress.Text		= "Поиск одинаковых fb2-файлов:";
			tsProgressBar.Maximum	= m_lFilesList.Count;
			tsProgressBar.Value		= 0;
			
			// данные настроек для поиска одинаковых fb2-книг
			Settings.DataFB2Dup dfb2dup = new Settings.DataFB2Dup();
			
			 // список книг, имеющих копии
			if( m_lDupFiles == null ) 	m_lDupFiles = new List<string>();
			else 						m_lDupFiles.Clear();
			
			// Сравнение fb2-файлов
			Hashtable htBookGroups = new Hashtable(); // хеш-таблица групп одинаковых книг
			ListViewGroup lvg = null; // группа одинаковых книг
			lvResult.BeginUpdate();
			foreach( string sFromFilePath in m_lFilesList ) {
				// Проверить флаг на остановку процесса 
				if( ( m_bw.CancellationPending == true ) ) {
					e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
					break;
				} else {
					string sExt = Path.GetExtension( sFromFilePath ).ToLower();
					if( sExt==".fb2" ) {
						++m_sv.FB2; // число fb2-файлов из списка всех анализируемых файлов
						// сравнение fb2-файлов, согласно заданного условия сравнения
						if( CompareFB2Files( sFromFilePath, cboxMode.SelectedIndex, m_lFilesList, m_lDupFiles ) ) {
							++m_sv.AllFB2InGroups;
							// заносим путь к дублю в список дублей
							m_lDupFiles.Add( sFromFilePath );
							// формирование списка одинаковых Книг для просмотра
							FB2BookDataForDup bd = new FB2BookDataForDup( sFromFilePath );
							string sValid = "?";
							if( m_bCheckValid ) sValid = ( bd.IsValid == "" ) ? "Да" : "Нет";

							if( cboxMode.SelectedIndex == 0 ) {
								string sID = bd.DIID;
								lvg = new ListViewGroup( sID );

								ListViewItem lvi = new ListViewItem( sFromFilePath );
								lvi.SubItems.Add( bd.TIBookTitle );
								lvi.SubItems.Add( bd.TIAuthors );
								lvi.SubItems.Add( bd.TIGenres );
								lvi.SubItems.Add( bd.DIVersion );
								lvi.SubItems.Add( sValid );
								lvi.SubItems.Add( bd.FileLength );
								
								lvi.SubItems.Add( bd.FileCreationTime );
								lvi.SubItems.Add( bd.FileLastWriteTime );
									
								// заносим группу в хеш, если она там отсутствует
								if( AddBookGroupInHashTable( htBookGroups, lvg ) ) {
									++m_sv.Group; // новая группа книг
								}
									
								// присваиваем группу книге
								lvResult.Groups.Add( (ListViewGroup)htBookGroups[sID] );
								lvi.Group = (ListViewGroup)htBookGroups[sID];
								lvResult.Items.Add( lvi );
							} else {
								string sBookTitle = bd.TIBookTitle;
								lvg = new ListViewGroup( sBookTitle );

								ListViewItem lvi = new ListViewItem( sFromFilePath );
								lvi.SubItems.Add( bd.TIAuthors );
								lvi.SubItems.Add( bd.TIGenres );
								lvi.SubItems.Add( bd.DIID );
								lvi.SubItems.Add( bd.DIVersion );
								lvi.SubItems.Add( sValid==""?"Да":"Нет" );
								lvi.SubItems.Add( bd.FileLength );
								
								lvi.SubItems.Add( bd.FileCreationTime );
								lvi.SubItems.Add( bd.FileLastWriteTime );
									
								// заносим группу в хеш, если она там отсутствует
								if( AddBookGroupInHashTable( htBookGroups, lvg ) ) {
									++m_sv.Group; // новая группа книг
								}
									
								// присваиваем группу книге
								lvResult.Groups.Add( (ListViewGroup)htBookGroups[sBookTitle] );
								lvi.Group = (ListViewGroup)htBookGroups[sBookTitle];
								lvResult.Items.Add( lvi );
							}
						}
					} else {
						// это архив?
						if( archivesWorker.IsArchive( sExt ) ) {
							++m_sv.Archive;	// пропускаем архивы
						}  else {
							++m_sv.Other;	// пропускаем не fb2-файлы
						}
					}
				}
				m_bw.ReportProgress( 0 ); // отобразим данные в контролах
			}
		}
		
		private void bw_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
            // Отобразим результат
			Misc msc = new Misc();
			msc.ListViewStatus( lvFilesCount, 2, m_sv.FB2 );
			msc.ListViewStatus( lvFilesCount, 3, m_sv.Archive );
			msc.ListViewStatus( lvFilesCount, 4, m_sv.Other );
			msc.ListViewStatus( lvFilesCount, 5, m_sv.Group );
			msc.ListViewStatus( lvFilesCount, 6, m_sv.AllFB2InGroups );
			++tsProgressBar.Value;
        }
		
		 private void bw_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {   
            // Проверяем - это отмена, ошибка, или конец задачи и сообщить
            lvResult.EndUpdate();
            DateTime dtEnd = DateTime.Now;
            m_lFilesList.Clear();
            m_sv.Clear();
            filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
            
            string sTime = dtEnd.Subtract( m_dtStart ).ToString() + " (час.:мин.:сек.)";
			string sMessCanceled	= "Поиск одинаковых fb2-файлов остановлен!\nЗатрачено времени: "+sTime;
			string sMessError		= "";
			string sMessDone		= "Поиск одинаковых fb2-файлов завершен!\nЗатрачено времени: "+sTime;
           
			if( ( e.Cancelled == true ) ) {
                MessageBox.Show( sMessCanceled, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
            } else if( e.Error != null ) {
                sMessError = "Error!\n" + e.Error.Message + "\n" + e.Error.StackTrace + "\nЗатрачено времени: "+sTime;
            	MessageBox.Show( sMessError, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
            } else {
            	MessageBox.Show( sMessDone, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
            }
			
			tsslblProgress.Text = Settings.Settings.GetReady();

			SetSearchFB2DupStartEnabled( true );
        }
		#endregion
		
		#region Закрытые вспомогательные методы класса
		// увеличение значения 2-й колонки ListView на 1
		private void Init() {
			// инициализация контролов и переменных
			lvResult.Items.Clear();
			for( int i=0; i!=lvFilesCount.Items.Count; ++i ) {
				lvFilesCount.Items[i].SubItems[1].Text	= "0";
			}
			tsProgressBar.Value		= 0;
			tsslblProgress.Text		= Settings.Settings.GetReady();
			tsProgressBar.Visible	= false;
			// очистка временной папки
			filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
			if( m_sv!=null ) m_sv.Clear(); // сброс данных класса для отображения прогресса
			lvResult.Groups.Clear();
		}
		
		private void ReadFB2DupTempData() {
			// чтение путей к данным поиска одинаковых fb2-файлов из xml-файла
			string sSettings = Settings.Settings.WorksDataSettingsPath;
			if( !File.Exists( sSettings ) ) return;
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.IgnoreWhitespace = true;
			using ( XmlReader reader = XmlReader.Create( sSettings, settings ) ) {
				// Полная Сортировка
				reader.ReadToFollowing("FB2DupScanDir");
				if (reader.HasAttributes ) {
					tboxSourceDir.Text = reader.GetAttribute("tboxSourceDir");
					Settings.SettingsFB2Dup.FMDataScanDir = tboxSourceDir.Text.Trim();
				}
				reader.Close();
			}
		}
		
		private void SetSearchFB2DupStartEnabled( bool bEnabled ) {
			// доступность контролов при Поиске одинаковых fb2-файлов
			pSearchFBDup2Dirs.Enabled	= bEnabled;
			pMode.Enabled				= bEnabled;
			tsbtnOpenDir.Enabled		= bEnabled;
			tsbtnSearchDubls.Enabled	= bEnabled;
			tsbtnSearchFb2DupStop.Enabled	= !bEnabled;
			tsProgressBar.Visible			= !bEnabled;
			ssProgress.Refresh();
		}
		
		private bool IsScanFolderDataCorrect( TextBox tbSource ) {
			// проверка на корректность данных папок источника
			string sSource = tbSource.Text.Trim();
			Regex rx = new Regex( @"\\+$" );
			sSource = rx.Replace( sSource, "" );
			tbSource.Text = sSource;
			
			// проверки на корректность папок источника
			if( sSource.Length == 0 ) {
				MessageBox.Show( "Выберите папку для сканирования!", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			DirectoryInfo diFolder = new DirectoryInfo( sSource );
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка не найдена: " + sSource, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			return true;
		}
		
		private bool IsArchivatorsExist() {
			// проверка на наличие архиваторов
			string s7zPath	= Settings.Settings.Read7zaPath();
			string sRarPath	= Settings.Settings.ReadRarPath();
			
			if( s7zPath.Trim().Length==0 ) {
				MessageBox.Show( "В Настройках не указана папка с установленным консольным 7Zip-архиватором!\nУкажите путь к нему в Настройках.\nРабота остановлена!",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			} else {
				if( !File.Exists( s7zPath ) ) {
					MessageBox.Show( "Не найден файл Zip-архиватора \""+s7zPath+"\"!\nУкажите путь к нему в Настройках.\nРабота остановлена!",
					                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return false;
				}
			}
			
			if( sRarPath.Trim().Length==0 ) {
				MessageBox.Show( "В Настройках не указана папка с установленным консольным rar-архиватором!\nУкажите путь к нему в Настройках.\nРабота остановлена!",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			} else {
				if( !File.Exists( sRarPath ) ) {
					MessageBox.Show( "Не найден файл консольного rar-архиватора \""+sRarPath+"\"!\nУкажите путь к нему в Настройках.\nРабота остановлена!",
					                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return false;
				}
			}
			return true;
		}
		
		// есть ли искомый файл в списке
		private bool FileExistsInList( string sFromFilePath, List<string> slDupFiles ) {
			if( m_lDupFiles != null ) {
				foreach( string sDup in slDupFiles ) {
					if( sFromFilePath == sDup ) {
						return true;
					}
				}
			}
			return false;
		}
		
		// Сравнение fb2-файлов, согласно заданного условия сравнения
		// Параметры:
		// m_lFilesList - список всех файлов в папке-источнике; m_lDupFiles - список файлов, которые имеют копии;
		// nMode - режим сравнения книг: 0 - по Id Книги; 1 - по Автору(ам) и Названию Книги
		private bool CompareFB2Files( string sFromFilePath, int nMode, List<string> m_lFilesList, List<string> m_lDupFiles ) {
			for( int i=0; i!=m_lFilesList.Count; ++i ) {
				// смотрим, не сравниваем ли книгу с самой собой
				if( sFromFilePath == m_lFilesList[i] ) continue;
				// смотрим, не сравниваем ли книгу с уже добавленной в список дублей
				if( FileExistsInList( sFromFilePath, m_lDupFiles ) ) continue;
				// сравниваем две книги
				try {
					fB2Parser	fb2_1	= new fB2Parser( sFromFilePath );
					fB2Parser	fb2_2	= null;
					string sExt = Path.GetExtension( m_lFilesList[i] ).ToLower();
					if( sExt==".fb2" ) {
						fb2_2 = new fB2Parser( m_lFilesList[i] );
						Fb2Comparer	fb2c = new Fb2Comparer( fb2_1.GetDescription(), fb2_2.GetDescription() );
						// обработка режимов сравнения
						if( nMode == 0 ) {
							// по Id Книги
							if( fb2c.IsIdEquality() ) return true;
						} else {
							// по Автору(ам) и Названию Книги
							if( fb2c.IsBookAuthorEquality() && fb2c.IsBookTitleEquality() ) return true;
						}
					}
				} catch {
					// проблемные файлы игнорируем
				}
			}
			return false;
		}
		
		// создание хеш-таблицы для групп одинаковых книг
		private bool AddBookGroupInHashTable( Hashtable groups, ListViewGroup lvg ) {
			if( groups == null ) return false;
			if( !groups.Contains( lvg.Header ) ) {
				groups.Add( lvg.Header, lvg );
				return true;
			}
			return false;
		}
		#endregion
		
		#region Обработчики событий
		void TboxSourceDirTextChanged(object sender, EventArgs e)
		{
			Settings.SettingsFB2Dup.FMDataScanDir = tboxSourceDir.Text;
		}
		
		void TsbtnOpenDirClick(object sender, EventArgs e)
		{
			// задание папки с fb2-файлами и архивами для сканирования
			filesWorker.OpenDirDlg( tboxSourceDir, fbdScanDir, "Укажите папку для сканирования с fb2-файлами и архивами:" );
		}
		
		void TsbtnFullSortStopClick(object sender, EventArgs e)
		{
			// Остановка выполнения процесса Поиска одинаковых fb2-файлов
			if( m_bw.WorkerSupportsCancellation == true ) {
				m_bw.CancelAsync();
			}
		}
		
		void TsbtnSearchDublsClick(object sender, EventArgs e)
		{
			// Поиск одинаковых fb2-файлов
			if( chBoxScanSubDir.Checked ) {
				m_bScanSubDirs = true;
			} else {
				m_bScanSubDirs = false;
			}
			m_sSource = tboxSourceDir.Text;
			m_sMessTitle = "SharpFBTools - Поиск одинаковых fb2-файлов";
			// проверка на корректность данных папок источника и приемника файлов
			if( !IsScanFolderDataCorrect( tboxSourceDir ) ) {
				return;
			}
			// проверка на наличие архиваторов
			if( !IsArchivatorsExist() ) {
				return;
			}
			
			// инициализация контролов
			Init();
			SetSearchFB2DupStartEnabled( false );

			m_bCheckValid = chBoxIsValid.Checked;
			m_dtStart = DateTime.Now;
			tsslblProgress.Text = "Создание списка файлов:";
			
			// Запуск процесса DoWork от Бекграунд Воркера
			if( m_bw.IsBusy != true ) {
				//если не занят то запустить процесс
            	m_bw.RunWorkerAsync();
			}		
		}
		
		void CboxModeSelectedIndexChanged(object sender, EventArgs e)
		{
			// изменение колонок просмотрщика найденного, взависимости от режима сравнения
			lvResult.Columns.Clear();
			if( cboxMode.SelectedIndex == 0 ) {
				// по Id Книги
				lvResult.Columns.Add( "Одинаковые ID (Путь к Книге)", 300 );
				lvResult.Columns.Add( "Название Книги", 180 );
				lvResult.Columns.Add( "Автор(ы) Книги", 180 );
				lvResult.Columns.Add( "Жанр(ы) Книги", 180 );
			} else {
				// по Автору(ам) и Названию Книги
				lvResult.Columns.Add( "Книга (Путь к Книге)", 300 );
				lvResult.Columns.Add( "Автор(ы) Книги", 180 );
				lvResult.Columns.Add( "Жанр(ы) Книги", 180 );
				lvResult.Columns.Add( "ID Книги", 200 );
			}
			lvResult.Columns.Add( "Версия", 50 );
			lvResult.Columns.Add( "Валидность", 50, HorizontalAlignment.Center );
			lvResult.Columns.Add( "Размер", 90, HorizontalAlignment.Center );
			
			lvResult.Columns.Add( "Дата создания", 120 );
			lvResult.Columns.Add( "Последнее изменение", 120 );
		}
		
		void LvResultSelectedIndexChanged(object sender, EventArgs e)
		{
			// занесение ошибки валидации в бокс
			#region Код
			ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
			if( si.Count > 0 ) {
				// пропускаем ситуацию, когда курсор переходит от одной строки к другой - нет выбранного item'а
				Misc				msc	= new Misc();
				FB2BookDataForDup	bd	= new FB2BookDataForDup( si[0].Text );
				// считываем данные TitleInfo
				msc.ListViewStatus( lvTitleInfo, 0, bd.TIBookTitle );
				msc.ListViewStatus( lvTitleInfo, 1, bd.TIGenres );
				msc.ListViewStatus( lvTitleInfo, 2, bd.TILang );
				msc.ListViewStatus( lvTitleInfo, 3, bd.TISrcLang );
				msc.ListViewStatus( lvTitleInfo, 4, bd.TIAuthors );
				msc.ListViewStatus( lvTitleInfo, 5, bd.TIDate );
				msc.ListViewStatus( lvTitleInfo, 6, bd.TIKeywords );
				msc.ListViewStatus( lvTitleInfo, 7, bd.TICoverpage );
				msc.ListViewStatus( lvTitleInfo, 8, bd.TITranslators );
				msc.ListViewStatus( lvTitleInfo, 9, bd.TISequences );
				// считываем данные SourceTitleInfo
				msc.ListViewStatus( lvSourceTitleInfo, 0, bd.STIBookTitle );
				msc.ListViewStatus( lvSourceTitleInfo, 1, bd.STIGenres );
				msc.ListViewStatus( lvSourceTitleInfo, 2, bd.STILang );
				msc.ListViewStatus( lvSourceTitleInfo, 3, bd.STISrcLang );
				msc.ListViewStatus( lvSourceTitleInfo, 4, bd.STIAuthors );
				msc.ListViewStatus( lvSourceTitleInfo, 5, bd.STIDate );
				msc.ListViewStatus( lvSourceTitleInfo, 6, bd.STIKeywords );
				msc.ListViewStatus( lvSourceTitleInfo, 7, bd.STICoverpage );
				msc.ListViewStatus( lvSourceTitleInfo, 8, bd.STITranslators );
				msc.ListViewStatus( lvSourceTitleInfo, 9, bd.STISequences );
				// считываем данные DocumentInfo
				msc.ListViewStatus( lvDocumentInfo, 0, bd.DIID );
				msc.ListViewStatus( lvDocumentInfo, 1, bd.DIVersion );
				msc.ListViewStatus( lvDocumentInfo, 2, bd.DIFB2Date );
				msc.ListViewStatus( lvDocumentInfo, 3, bd.DIProgramUsed );
				msc.ListViewStatus( lvDocumentInfo, 4, bd.DISrcOcr );
				msc.ListViewStatus( lvDocumentInfo, 5, bd.DISrcUrls );
				msc.ListViewStatus( lvDocumentInfo, 6, bd.DIFB2Authors );
				// считываем данные PublishInfo
				msc.ListViewStatus( lvPublishInfo, 0, bd.PIBookName );
				msc.ListViewStatus( lvPublishInfo, 1, bd.PIPublisher );
				msc.ListViewStatus( lvPublishInfo, 2, bd.PIYear );
				msc.ListViewStatus( lvPublishInfo, 3, bd.PICity );
				msc.ListViewStatus( lvPublishInfo, 4, bd.PIISBN );
				msc.ListViewStatus( lvPublishInfo, 5, bd.PISequences );
				// считываем данные CustomInfo
				lvCustomInfo.Items.Clear();
				IList<CustomInfo> lcu = bd.CICustomInfo;
				if( lcu != null ) {
					foreach( CustomInfo ci in lcu ) {
						ListViewItem lvi = new ListViewItem( ci.InfoType );
						lvi.SubItems.Add( ci.Value );
						lvCustomInfo.Items.Add( lvi );
					}
				}
				// считываем данные History
				rtbHistory.Clear(); rtbHistory.Text = bd.DIHistory;
				// считываем данные Annotation
				rtbAnnotation.Clear(); rtbAnnotation.Text = bd.TIAnnotation;
			}
			#endregion
		}
		#endregion
		
	}
	
	/// <summary>
	/// Description of StatusView.
	/// </summary>
	public class StatusView {
		
		#region Закрытые данные класса
		private int m_nFB2				= 0;
		private int m_nArchive			= 0;
		private int m_nOther			= 0;
		private int m_nGroup			= 0;
		private int m_nAllFB2InGroups	= 0;
		#endregion
		
		public StatusView() {

		}
		
		#region Открытые методы класса
		public void Clear() {
			// сброс всех данных
			m_nFB2				= 0;
			m_nArchive			= 0;
			m_nOther			= 0;
			m_nGroup			= 0;
			m_nAllFB2InGroups	= 0;
		}
		#endregion
		
		#region Свойства класса
		public virtual int FB2 {
			get { return m_nFB2; }
			set { m_nFB2 = value; }
        }
		
		public virtual int Archive {
			get { return m_nArchive; }
			set { m_nArchive = value; }
        }
		
		public virtual int Other {
			get { return m_nOther; }
			set { m_nOther = value; }
        }
		
		public virtual int Group {
			get { return m_nGroup; }
			set { m_nGroup = value; }
        }
		
		public virtual int AllFB2InGroups {
			get { return m_nAllFB2InGroups; }
			set { m_nAllFB2InGroups = value; }
        }
		#endregion
	}
	
}
