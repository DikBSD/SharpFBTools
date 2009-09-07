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
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Core.FB2Dublicator;
using Core.Misc;

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
		private DateTime m_dtStart;
        private BackgroundWorker m_bw	= null;
        private string m_sSource		= "";
        private bool m_bScanSubDirs		= true;
        private string m_sMessTitle		= "";
		private List<string> m_lFilesList	= null; // список всех проверяемых файлов
		private List<string> m_lDupFiles	= null; // список файлов, имеющих копии, соответственно условию сравнения
        #endregion
		
		public SFBTpFB2Dublicator()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			InitializeBackgroundWorker();
			
			Init();
			// читаем сохраненные пути к папкам Поиска одинаковых fb2-файлов, если они есть
			ReadFB2DupTempData();
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
			
			Misc misc = new Misc();
			// Сравнение fb2-файлов
			List<string> m_lLVGC = new List<string>(); // список групп  одинаковых книг
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
						// сравнение fb2-файлов, согласно заданного условия сравнения
						if( CompareFB2Files( sFromFilePath, cboxMode.SelectedIndex, m_lFilesList, m_lDupFiles ) ) {
							// заносим путь к дублю в список дублей
							m_lDupFiles.Add( sFromFilePath );
							
							fB2Parser fb2 = new fB2Parser( sFromFilePath );
							string sID = fb2.GetDocumentInfo().ID;
							if( m_lLVGC.IndexOf( sID ) == -1 ) {
								m_lLVGC.Add( sID );
								lvg = new ListViewGroup( sID);
							}
							if( lvg != null ) {
								lvResult.Groups.Add( lvg );
								ListViewItem lvi = new ListViewItem( sFromFilePath );
								lvi.Group = lvg;
								lvResult.Items.Add( lvi );
							}
						}
						//misc.IncListViewStatus( lvResult, 2 ); // исходные fb2-файлы
					} else {
						// это архив?
						if( archivesWorker.IsArchive( sExt ) ) {
							// пропускаем архивы
							//misc.IncListViewStatus( lvResult, 3 ); // архивы
						}  else {
							// пропускаем не fb2-файлы
							//misc.IncListViewStatus( lvResult, 4 ); // другие файлы 
						}
					}
				}
				m_bw.ReportProgress( 0 ); // отобразим данные в контролах
			}
		}
		
		private void bw_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
            // Отобразим результат

            ++tsProgressBar.Value;
        }
		
		 private void bw_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {   
            // Проверяем - это отмена, ошибка, или конец задачи и сообщить
            lvResult.EndUpdate();
            DateTime dtEnd = DateTime.Now;
            m_lFilesList.Clear();
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
		private void Init() {
			// инициализация контролов и переменных
			for( int i=0; i!=lvFilesCount.Items.Count; ++i ) {
				lvFilesCount.Items[i].SubItems[1].Text	= "0";
			}
			tsProgressBar.Value		= 0;
			tsslblProgress.Text		= Settings.Settings.GetReady();
			tsProgressBar.Visible	= false;
			// очистка временной папки
			filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
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

			m_dtStart = DateTime.Now;
			tsslblProgress.Text = "Создание списка файлов:";
			
			// Запуск процесса DoWork от Бекграунд Воркера
			if( m_bw.IsBusy != true ) {
				//если не занят то запустить процесс
            	m_bw.RunWorkerAsync();
			}		
		}
		#endregion

	}
}
