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
using Core.FB2.Description;
using Core.FB2.Description.Common;
using Core.FB2.Description.TitleInfo;
using Core.FB2.Description.DocumentInfo;
using Core.FB2.Description.CustomInfo;

//using fB2Parser 		= Core.FB2.FB2Parsers.FB2Parser;
using archivesWorker	= Core.FilesWorker.Archiver;
using FB2Validator		= Core.FB2Parser.FB2Validator;
using filesWorker		= Core.FilesWorker.FilesWorker;
using fB2Parser 		= Core.FB2Dublicator.FB2ParserForDup;
using stringProcessing	= Core.StringProcessing.StringProcessing;

namespace SharpFBTools.Tools
{
	/// <summary>
	/// Description of SFBTpFB2Dublicator.
	/// </summary>
	public partial class SFBTpFB2Dublicator : UserControl
	{
		#region Закрытые данные класса
		private StatusView	m_sv 			= null; 
		private DateTime	m_dtStart;
        private BackgroundWorker m_bw		= null;
        private BackgroundWorker m_bwcmd	= null;
        private string	m_sSource			= "";
        private bool	m_bScanSubDirs		= true;
        private string	m_sMessTitle		= "";
		private bool	m_bCheckValid		= false;	// проверять или нет fb2-файл на валидность
		private string	m_sFileWorkerMode	= "";
		private MiscListView m_mscLV		= new MiscListView(); // класс по работе с ListView
		#endregion
		
		public SFBTpFB2Dublicator()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			InitializeBackgroundWorker();
			InitializeFilesWorkerBackgroundWorker();
			
			Init();
			// читаем сохраненные пути к папкам Поиска одинаковых fb2-файлов, если они есть
			ReadFB2DupTempData();
			m_sv = new StatusView();
			cboxMode.SelectedIndex			= 0; // Условия для Сравнения fb2-файлов: Автор(ы) и Название Книги
			cboxDupExistFile.SelectedIndex	= 1; // добавление к создаваемому fb2-файлу очередного номера
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
				m_sv.AllFiles = filesWorker.DirsParser( m_bw, e, m_sSource, lvFilesCount, ref lDirList, false );
			}
			m_bw.ReportProgress( 0 ); // отобразим данные в контролах
			
			// Проверить флаг на остановку процесса 
			if( ( m_bw.CancellationPending == true ) ) {
				e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
				return;
			}
			
			// проверка, есть ли хоть один файл в папке для сканирования
			if( m_sv.AllFiles == 0 ) {
				MessageBox.Show( "В папке сканирования не найдено ни одного файла!\nРабота прекращена.", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				tsslblProgress.Text = Settings.Settings.GetReady();
				SetSearchFB2DupStartEnabled( true );
				return;
			}
			
			// очистка контролов вывода данных по книге по ее выбору
			ClearDataFields();
			
			tsslblProgress.Text		= "Хеширование fb2-файлов:";
			tsProgressBar.Maximum	= m_sv.AllFiles;
			tsProgressBar.Value		= 0;
			
			// Сравнение fb2-файлов
			lvResult.BeginUpdate();
			Hashtable htBookGroups = new Hashtable(); // хеш-таблица групп одинаковых книг
			ListViewGroup lvg = null; // группа одинаковых книг
           	
			string sProgress = "Создание Списка одинаковых fb2-файлов:";
			if( cboxMode.SelectedIndex == 0 ) {
           		MakeColumns( 0 ); // изменение колонок просмотрщика найденного, взависимости от режима сравнения
           		Hashtable htFB2ForID = FilesHashForIDParser( m_bw, e, lDirList );
           		// Проверить флаг на остановку процесса
				if( ( m_bw.CancellationPending == true ) ) {
					e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
					return;
				}
           		
           		tsslblProgress.Text		= sProgress;
           		tsProgressBar.Maximum	= htFB2ForID.Values.Count;
           		tsProgressBar.Value		= 0;
				foreach( FB2FilesDataIDList fb2f in htFB2ForID.Values ) {
           			// Проверить флаг на остановку процесса 
					if( ( m_bw.CancellationPending == true ) ) {
						e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
						return;
					}
	           		if( fb2f.Count > 1 ) {
     	      			++m_sv.Group; // число групп одинаковых книг
        	   			foreach( BookData bd in fb2f ) {
           					++m_sv.AllFB2InGroups; // число книг во всех группах одинаковых книг
           					lvg = new ListViewGroup( fb2f.Id );
           					ListViewItem lvi = new ListViewItem( bd.Path );
           					lvi.SubItems.Add( MakeBookTitleString( bd.BookTitle ) );
							lvi.SubItems.Add( MakeAutorsString( bd.Authors, true ) );
							lvi.SubItems.Add( MakeGenresString( bd.Genres, true ) );
							lvi.SubItems.Add( bd.Version );
							lvi.SubItems.Add( bd.Encoding );
							lvi.SubItems.Add( m_bCheckValid ? IsValid( bd.Path ) : "?" );
							lvi.SubItems.Add( GetFileLength( bd.Path ) );
							lvi.SubItems.Add( GetFileCreationTime( bd.Path ) );
							lvi.SubItems.Add( FileLastWriteTime( bd.Path ) );
							// заносим группу в хеш, если она там отсутствует
							AddBookGroupInHashTable( htBookGroups, lvg );
							// присваиваем группу книге
							lvResult.Groups.Add( (ListViewGroup)htBookGroups[fb2f.Id] );
							lvi.Group = (ListViewGroup)htBookGroups[fb2f.Id];
							lvResult.Items.Add( lvi );
    	       			}
        	   		}
           			m_bw.ReportProgress( 0 );
           		}
           	} else if( cboxMode.SelectedIndex == 1 ) {
				MakeColumns( 1 ); // изменение колонок просмотрщика найденного, взависимости от режима сравнения
           		Hashtable htFB2ForABT = FilesHashForABTParser( m_bw, e, lDirList );
           		tsslblProgress.Text		= sProgress;
           		tsProgressBar.Maximum	= htFB2ForABT.Values.Count;
           		tsProgressBar.Value		= 0;
           		foreach( FB2FilesDataABTList fb2f in htFB2ForABT.Values ) {
           			// Проверить флаг на остановку процесса 
					if( ( m_bw.CancellationPending == true ) ) {
						e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
						return;
					}
           			if( fb2f.Count > 1 ) {
           				// разбивка на группы для одинакового Названия по Авторам
   						++m_sv.Group; // число групп одинаковых книг
           				foreach( BookData bd in fb2f ) {
	       					++m_sv.AllFB2InGroups; // число книг во всех группах одинаковых книг
	       					lvg = new ListViewGroup( fb2f.BookTitleForKey );
							ListViewItem lvi = new ListViewItem( bd.Path );
							lvi.SubItems.Add( MakeAutorsString( bd.Authors, true ) );
							lvi.SubItems.Add( MakeGenresString( bd.Genres, true ) );
							lvi.SubItems.Add( bd.Id );
							lvi.SubItems.Add( bd.Version );
							lvi.SubItems.Add( bd.Encoding );
							lvi.SubItems.Add( m_bCheckValid ? IsValid( bd.Path ) : "?" );
							lvi.SubItems.Add( GetFileLength( bd.Path ) );
							lvi.SubItems.Add( GetFileCreationTime( bd.Path ) );
							lvi.SubItems.Add( FileLastWriteTime( bd.Path ) );
							// заносим группу в хеш, если она там отсутствует
							AddBookGroupInHashTable( htBookGroups, lvg );
							// присваиваем группу книге
							lvResult.Groups.Add( (ListViewGroup)htBookGroups[fb2f.BookTitleForKey] );
							lvi.Group = (ListViewGroup)htBookGroups[fb2f.BookTitleForKey];
							lvResult.Items.Add( lvi );
       					}
           			}
           			m_bw.ReportProgress( 0 );
           		}
			} else {
           		MakeColumns( 1 ); // изменение колонок просмотрщика найденного, взависимости от режима сравнения
           		Hashtable htFB2ForABT = FilesHashForABTParser( m_bw, e, lDirList );
           		tsslblProgress.Text		= sProgress;
           		tsProgressBar.Maximum	= htFB2ForABT.Values.Count;
           		tsProgressBar.Value		= 0;
           		foreach( FB2FilesDataABTList fb2f in htFB2ForABT.Values ) {
           			// Проверить флаг на остановку процесса 
					if( ( m_bw.CancellationPending == true ) ) {
						e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
						return;
					}
           			if( fb2f.Count > 1 ) {
           				// разбивка на группы для одинакового Названия по Авторам
           				Hashtable ht = FindDupForAuthors( fb2f );
           				foreach( FB2FilesDataABTList fb2List in ht.Values ) {
           					if( fb2List.Count > 1 ) {
           						++m_sv.Group; // число групп одинаковых книг
		           				foreach( BookData bd in fb2List ) {
    		       					++m_sv.AllFB2InGroups; // число книг во всех группах одинаковых книг
    		       					lvg = new ListViewGroup( fb2List.BookTitleForKey );
									ListViewItem lvi = new ListViewItem( bd.Path );
									lvi.SubItems.Add( MakeAutorsString( bd.Authors, true ) );
									lvi.SubItems.Add( MakeGenresString( bd.Genres, true ) );
									lvi.SubItems.Add( bd.Id );
									lvi.SubItems.Add( bd.Version );
									lvi.SubItems.Add( bd.Encoding );
									lvi.SubItems.Add( m_bCheckValid ? IsValid( bd.Path ) : "?" );
									lvi.SubItems.Add( GetFileLength( bd.Path ) );
									lvi.SubItems.Add( GetFileCreationTime( bd.Path ) );
									lvi.SubItems.Add( FileLastWriteTime( bd.Path ) );
									// заносим группу в хеш, если она там отсутствует
									AddBookGroupInHashTable( htBookGroups, lvg );
									// присваиваем группу книге
									lvResult.Groups.Add( (ListViewGroup)htBookGroups[fb2List.BookTitleForKey] );
									lvi.Group = (ListViewGroup)htBookGroups[fb2List.BookTitleForKey];
									lvResult.Items.Add( lvi );
    	       					}
           					}
           				}
           			}
           			m_bw.ReportProgress( 0 );
           		}
           	}
			lDirList.Clear();
		}
		
		private void bw_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
            // Отобразим результат
            if( chBoxViewProgress.Checked ) ViewDupProgressData();
			++tsProgressBar.Value;
        }
		
		 private void bw_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {   
            // Проверяем - это отмена, ошибка, или конец задачи и сообщить
            ViewDupProgressData(); // отображение данных прогресса
            lvResult.EndUpdate();
            DateTime dtEnd = DateTime.Now;
            m_sv.Clear();
            filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
            
            string sTime = dtEnd.Subtract( m_dtStart ).ToString() + " (час.:мин.:сек.)";
			string sMessCanceled	= "Поиск одинаковых fb2-файлов остановлен!\nЗатрачено времени: "+sTime;
			string sMessError		= "";
			string sMessDone		= "Поиск одинаковых fb2-файлов завершен!\nЗатрачено времени: "+sTime;
			if( lvResult.Items.Count==0 ) sMessDone += "\n\nНе найдено НИ ОДНОЙ копии книг!";
			
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
		
		#region Закрытые методы реализации BackgroundWorker Копирование / Перемещение / Удаление
		private void InitializeFilesWorkerBackgroundWorker() {
			// Инициализация перед использование BackgroundWorker Копирование / Перемещение / Удаление
            m_bwcmd = new BackgroundWorker();
            m_bwcmd.WorkerReportsProgress		= true; // Позволить выводить прогресс процесса
            m_bwcmd.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
            m_bwcmd.DoWork				+= new DoWorkEventHandler( bwcmd_DoWork );
			m_bwcmd.ProgressChanged 	+= new ProgressChangedEventHandler( bwcmd_ProgressChanged );
            m_bwcmd.RunWorkerCompleted	+= new RunWorkerCompletedEventHandler( bwcmd_RunWorkerCompleted );
		}
		
		private void bwcmd_DoWork( object sender, DoWorkEventArgs e ) {
			// Обработка файлов
			string sTempDir = Settings.Settings.GetTempDir();
			switch( m_sFileWorkerMode ) {
				case "Copy":
					CopyOrMoveFilesTo( m_bwcmd, e, true,
					                    m_sSource, tboxDupToDir.Text.Trim(), sTempDir, lvResult, 
										cboxDupExistFile.SelectedIndex, chBoxAddFileNameBookID.Checked,
										tsslblProgress, "Копирование помеченных копий книг:" );
	               	break;
				case "Move":
	               	CopyOrMoveFilesTo( m_bwcmd, e, false,
	               	                    m_sSource, tboxDupToDir.Text.Trim(), sTempDir, lvResult,
										cboxDupExistFile.SelectedIndex, chBoxAddFileNameBookID.Checked,
										tsslblProgress, "Перемещение помеченных копий книг:" );
       	        	break;
				case "Delete":
       	        	DeleteFiles( m_bwcmd, e, lvResult, tsslblProgress, "Удаление помеченных копий книг:" );
					break;
				default:
					return;
			}
		}
		
		private void bwcmd_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
            // Отобразим результат Копирования / Перемещения / Удаление
       		// новое число групп и книг во всех группах
			m_mscLV.ListViewStatus( lvFilesCount, 5, lvResult.Groups.Count.ToString() );
			// число книг во всех группах
			m_mscLV.ListViewStatus( lvFilesCount, 6, lvResult.Items.Count.ToString() );
            ++tsProgressBar.Value;
        }
		
		private void bwcmd_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {   
            // Завершение работы Обработчика Файлов
			string sMessCanceled, sMessError, sMessDone, sTabPageDefText, sMessTitle;
            sMessCanceled = sMessError = sMessDone = sTabPageDefText = sMessTitle = "";
            switch( m_sFileWorkerMode ) {
            	case "Copy":
					sMessTitle		= "SharpFBTools - Копирование копий книг";
            		sMessDone 		= "Копирование файлов в указанную папку завершено!";
					sMessCanceled	= "Копирование файлов в указанную папку остановлено!";
            		break;
            	case "Move":
					sMessTitle		= "SharpFBTools - Перемещение копий книг";
            		sMessDone 		= "Перемещение файлов в указанную папку завершено!";
					sMessCanceled	= "Перемещение файлов в указанную папку остановлено!";
            		break;
            	case "Delete":
            		sMessTitle		= "SharpFBTools - Удаление копий книг";
            		sMessDone 		= "Удаление файлов из папки-источника завершено!";
					sMessCanceled	= "Удаление файлов из папки-источника остановлено!";
            		break;
            }

			tsslblProgress.Text = Settings.Settings.GetReady();
			SetFilesWorkerStartEnabled( true );
			tsProgressBar.Visible = false;
			
            // Проверяем это отмена, ошибка, или конец задачи и сообщить
			if( ( e.Cancelled == true ) ) {
                MessageBox.Show( sMessCanceled, sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
            } else if( e.Error != null ) {
                sMessError = "Ошибка:\n" + e.Error.Message + "\n" + e.Error.StackTrace;
            	MessageBox.Show( sMessError, sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
            } else {
            	MessageBox.Show( sMessDone, sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
            }
		}
		
		#endregion
		
		#region Закрытые вспомогательные методы класса
		private void ViewDupProgressData() {
            // Отобразим результат
			m_mscLV.ListViewStatus( lvFilesCount, 1, m_sv.AllFiles );
			m_mscLV.ListViewStatus( lvFilesCount, 2, m_sv.FB2 );
			m_mscLV.ListViewStatus( lvFilesCount, 3, m_sv.Archive );
			m_mscLV.ListViewStatus( lvFilesCount, 4, m_sv.Other );
			m_mscLV.ListViewStatus( lvFilesCount, 5, m_sv.Group );
			m_mscLV.ListViewStatus( lvFilesCount, 6, m_sv.AllFB2InGroups );
        }
		
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
			lvResult.Items.Clear();
			lvResult.Groups.Clear();
		}
		
		// очистка контролов вывода данных по книге по ее выбору
		private void ClearDataFields() {
			for( int i=0; i!=lvTitleInfo.Items.Count; ++i ) {
				lvTitleInfo.Items[i].SubItems[1].Text		= "";
				lvSourceTitleInfo.Items[i].SubItems[1].Text	= "";
				
			}
			for( int i=0; i!=lvDocumentInfo.Items.Count; ++i ) {
				lvDocumentInfo.Items[i].SubItems[1].Text = "";
				
			}
			for( int i=0; i!=lvPublishInfo.Items.Count; ++i ) {
				lvPublishInfo.Items[i].SubItems[1].Text = "";
				
			}
			lvCustomInfo.Items.Clear();
			rtbHistory.Clear();
			rtbAnnotation.Clear();
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
					Settings.SettingsFB2Dup.DupScanDir = tboxSourceDir.Text.Trim();
				}
				reader.ReadToFollowing("FB2DupToDir");
				if (reader.HasAttributes ) {
					tboxDupToDir.Text = reader.GetAttribute("tboxDupToDir");
					Settings.SettingsFB2Dup.DupToDir = tboxDupToDir.Text.Trim();
				}
				reader.Close();
			}
		}
		
		private void SetSearchFB2DupStartEnabled( bool bEnabled ) {
			// доступность контролов при Поиске одинаковых fb2-файлов
			lvResult.Enabled			= bEnabled;
			pSearchFBDup2Dirs.Enabled	= bEnabled;
			pMode.Enabled				= bEnabled;
			pExistFile.Enabled			= bEnabled;
			tsbtnOpenDir.Enabled		= bEnabled;
			tsbtnSearchDubls.Enabled	= bEnabled;
			tsbtnSearchFb2DupStop.Enabled	= !bEnabled;
			tsProgressBar.Visible			= !bEnabled;
			ssProgress.Refresh();
		}
		
		private void SetFilesWorkerStartEnabled( bool bEnabled ) {
			// доступность контролов при Обработке файлов
			lvResult.Enabled			= bEnabled;
			tsbtnOpenDir.Enabled		= bEnabled;
			tsbtnToDir.Enabled			= bEnabled;
			tsbtnSearchDubls.Enabled	= bEnabled;
			if( lvResult.Items.Count==0 ) {
				tsbtnDupCopy.Enabled	= !bEnabled;
				tsbtnDupMove.Enabled	= !bEnabled;
				tsbtnDupDelete.Enabled	= !bEnabled;
			} else {
				tsbtnDupCopy.Enabled	= bEnabled;
				tsbtnDupMove.Enabled	= bEnabled;
				tsbtnDupDelete.Enabled	= bEnabled;
			}
			pSearchFBDup2Dirs.Enabled	= bEnabled;
			pMode.Enabled				= bEnabled;
			pExistFile.Enabled			= bEnabled;
			tsbtnDupWorkStop.Enabled	= !bEnabled;
			tsProgressBar.Visible		= !bEnabled;
			ssProgress.Refresh();
		}
		
		private bool IsScanFolderDataCorrect( TextBox tbSource ) {
			// проверка на корректность данных папок источника
			string sSource	= filesWorker.WorkingDirPath( tbSource.Text.Trim() );
			tbSource.Text	= sSource;
			
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
		
/*		private bool IsArchivatorsExist() {
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
		}*/
	
		// создание хеш-таблицы для групп одинаковых книг
		private bool AddBookGroupInHashTable( Hashtable groups, ListViewGroup lvg ) {
			if( groups == null ) return false;
			if( !groups.Contains( lvg.Header ) ) {
				groups.Add( lvg.Header, lvg );
				return true;
			}
			return false;
		}
		
		// хеширование файлов в контексте Id книг
		// параметры: lsDirs - список папок для сканирования
		private Hashtable FilesHashForIDParser( BackgroundWorker bw, DoWorkEventArgs e, List<string> lsDirs ) {
			// список всех файлов - по cписку папок - замена рекурсии
			Hashtable htFB2ForID = new Hashtable();
			foreach( string s in lsDirs ) {
				DirectoryInfo diFolder = new DirectoryInfo( s );
				foreach( FileInfo fiNextFile in diFolder.GetFiles() ) {
					if( ( bw.CancellationPending == true ) )  {
						e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
						return htFB2ForID;
					}
					
					string sPath	= s + "\\" + fiNextFile.Name;
					string sExt		= Path.GetExtension( sPath ).ToLower();
					if( sExt==".fb2" ) {
						++m_sv.FB2;
						// заполнение хеш таблицы данными о fb2-книгах в контексте их ID
						MakeFB2IDHashTable( sPath, ref htFB2ForID );
					}  else {
						// это архив?
						if( archivesWorker.IsArchive( sExt ) ) {
							++m_sv.Archive;	// пропускаем архивы
						}  else {
							++m_sv.Other;	// пропускаем не fb2-файлы
						}
					}
					bw.ReportProgress( 0 ); // отобразим данные в контролах
				}
			}
			return htFB2ForID;
		}
		
		// заполнение хеш таблицы данными о fb2-книгах в контексте их ID
		// параметры: sPath - путь к fb2-файлу; htFB2ForID - хеш-таблица
		private void MakeFB2IDHashTable( string sPath, ref Hashtable htFB2ForID ) {
			try {
				fB2Parser fb2 = new fB2Parser( sPath );
				string sEncoding = filesWorker.GetFileEncoding( fb2.XmlDoc.InnerXml.Split('>')[0] );
				if( sEncoding == null )  sEncoding = "?";
				string sID = fb2.Id;
				if( sID==null ) return;

				if( sID.Length==0 ) sID = "Тег <id> в этих книгах \"пустой\"";
				string sVersion = fb2.Version;;
				BookTitle 		bookTitle	= fb2.BookTitle;
				IList<Author>	authors		= fb2.Authors;
				
				// данные о книге
				BookData fb2BookData = new BookData( bookTitle, authors, fb2.Genres, sID, sVersion, sPath, sEncoding );
				
				if( !htFB2ForID.ContainsKey( sID ) ) {
					// такой книги в числе дублей еще нет
					FB2FilesDataIDList fb2f = new FB2FilesDataIDList();
					fb2f.AddBookData( fb2BookData );
					fb2f.Id = sID;
					htFB2ForID.Add( sID, fb2f );
				} else {
					// такая книга в числе дублей уже есть
					FB2FilesDataIDList fb2f = (FB2FilesDataIDList)htFB2ForID[sID] ;
					fb2f.AddBookData( fb2BookData );
					//htFB2ForID[sID] = fb2f; //ИЗБЫТОЧНЫЙ КОД
				}
			} catch {} // пропускаем проблемные файлы
		}
		
		// хеширование файлов в контексте Авторов и Названия книг
		// параметры: lsDirs - список папок для сканирования
		private Hashtable FilesHashForABTParser( BackgroundWorker bw, DoWorkEventArgs e, List<string> lsDirs ) {
			// список всех файлов - по cписку папок - замена рекурсии
			Hashtable htFB2ForABT = new Hashtable();
			foreach( string s in lsDirs ) {
				DirectoryInfo diFolder = new DirectoryInfo( s );
				foreach( FileInfo fiNextFile in diFolder.GetFiles() ) {
					if( ( bw.CancellationPending == true ) )  {
						e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
						return htFB2ForABT;
					}
					string sPath	= s + "\\" + fiNextFile.Name;
					string sExt		= Path.GetExtension( sPath ).ToLower();
					if( sExt==".fb2" ) {
						++m_sv.FB2;
						// заполнение хеш таблицы данными о fb2-книгах в контексте их Авторов и Названия
						MakeFB2ABTHashTable( sPath, ref htFB2ForABT );
					}  else {
						// это архив?
						if( archivesWorker.IsArchive( sExt ) ) {
							++m_sv.Archive;	// пропускаем архивы
						}  else {
							++m_sv.Other;	// пропускаем не fb2-файлы
						}
					}
					bw.ReportProgress( 0 ); // отобразим данные в контролах
				}
			}
			return htFB2ForABT;
		}
		
		// заполнение хеш таблицы данными о fb2-книгах в контексте их Названия
		// параметры: sPath - путь к fb2-файлу; htFB2ForABT - хеш-таблица
		private void MakeFB2ABTHashTable( string sPath, ref Hashtable htFB2ForABT ) {
			try {
				fB2Parser fb2 = new fB2Parser( sPath );
				BookTitle bookTitle	= fb2.BookTitle;
				if( bookTitle!=null && bookTitle.Value!=null ) {
					IList<Author> authors	= fb2.Authors;
					string sId			= fb2.Id;
					string sVersion		= fb2.Version;
					string sEncoding	= filesWorker.GetFileEncoding( fb2.XmlDoc.InnerXml.Split('>')[0] );
					if( sEncoding == null )  sEncoding = "?";
					// данные о книге
					BookData fb2BookData = new BookData( bookTitle, authors, fb2.Genres, sId, sVersion, sPath, sEncoding );
					string sBT = bookTitle.Value.Trim();
					if( !htFB2ForABT.ContainsKey( sBT ) ) {
						// такой книги в числе дублей еще нет
						FB2FilesDataABTList fb2f = new FB2FilesDataABTList();
						fb2f.BookTitleForKey = sBT;
						fb2f.AddBookData( fb2BookData );
						htFB2ForABT.Add( sBT, fb2f );
					} else {
						// такая книга в числе дублей уже есть
						FB2FilesDataABTList fb2f = (FB2FilesDataABTList)htFB2ForABT[sBT];
						fb2f.AddBookData( fb2BookData );
						//htFB2ForID[sBT] = fb2f; //ИЗБЫТОЧНЫЙ КОД
					}
				}

			} catch {} // пропускаем проблемные файлы
		}

		// создание групп копий по Авторам, относительно найденного Названия Книги
		private Hashtable FindDupForAuthors( FB2FilesDataABTList fb2Group ) {
			Hashtable ht = new Hashtable();
			bool bDone = false;
			while( fb2Group.Count > 0 ) {
				if( bDone ) break;
				// перебор всех книг группы
				for( int i=0; i!=fb2Group.Count; ++i ) {
					// группировка книг по Авторам
					if( fb2Group.Count==0 ) {
						bDone = true; break;
					}
					BookData bd = fb2Group[i];
					// ключ
					BookDataABTKey htKey = new BookDataABTKey( bd.Authors, bd.BookTitle );
					// ищем в группе одинакового названия книги дубли
					FB2FilesDataABTList fb2NewGroup = MakeDupGroup( ref fb2Group, bd.Authors );
					if( fb2NewGroup!=null ) {
						// заносим группу в хэш
						fb2NewGroup.BookTitleForKey = bd.BookTitle.Value+" ( " + MakeAutorsString( bd.Authors, false )+" )";
						ht.Add( htKey, fb2NewGroup );
						// удаляем найденное из основной группы fb2Group, чтобы повторно их не проверять
						foreach ( BookData b in fb2NewGroup ) {
							fb2Group.Remove( fb2Group.GetBookData( b.Path ) );
						}
					}
					// защита от зацикливания, если в списке с одинаковым Названием Книги - по одной книге на разных Авторов
					if( i==fb2Group.Count-1 && fb2NewGroup==null ) return ht;
				}
			}
			return ht;
		}
		
		// создание списка дублей книг для конкретного Названия по Авторам
		// возвращаем: null, если не нашли копии, или созданный список копий
		private FB2FilesDataABTList MakeDupGroup( ref FB2FilesDataABTList fb2Group, IList<Author> Authors ) {
			if( fb2Group.Count < 2 ) return null;
			FB2FilesDataABTList g = new FB2FilesDataABTList();
			foreach ( BookData bd in fb2Group ) {
           		FB2AuthorsComparer fb2c = new FB2AuthorsComparer( Authors, bd.Authors );
           		if( fb2c.IsBookAuthorEquality() ) {
           			// данные о книге
					BookData fb2BookData = new BookData( bd.BookTitle, bd.Authors, bd.Genres, bd.Id, bd.Version, bd.Path, bd.Encoding );
           			g.Add( fb2BookData );
           		}
           	}
			// возвращаем только группу копий, а не 1 книгу
			if( g.Count > 1 ) return g;
			return null;
		}

		// формирование строки с Названием Книги
		private string MakeBookTitleString( BookTitle bookTitle ) {
			if( bookTitle==null ) return "?";
			return bookTitle.Value!=null ? bookTitle.Value : "?";
		}
		
		// формирование строки с Авторами Книги из списка всех Авторов ЭТОЙ Книги
		private string MakeAutorsString( IList<Author> Authors, bool bNumber ) {
			if( Authors==null ) return "Тег <authors> в книге отсутствует";
			string sA = ""; int n = 0;
			foreach( Author a in Authors ) {
				++n;
				if( a.LastName!=null && a.LastName.Value!=null )
					sA += a.LastName.Value+" ";
				if( a.FirstName!=null && a.FirstName.Value!=null )
					sA += a.FirstName.Value+" ";
				if( a.MiddleName!=null && a.FirstName.Value!=null )
					sA += a.MiddleName.Value+" ";
				if( a.NickName!=null && a.NickName.Value!=null )
					sA += a.NickName.Value;
				sA += "; ";
			}
			if( bNumber ) sA = Convert.ToString(n)+": " + sA;
			return sA.Substring( 0, sA.LastIndexOf( ";" )-1 );
		}
		
		// формирование строки с Жанрами Книги из списка всех Жанров ЭТОЙ Книги
		private string MakeGenresString( IList<Genre> Genres, bool bNumber ) {
			if( Genres==null ) return "?";
			string sG = ""; int n = 0;
			foreach( Genre g in Genres ) {
				++n;
				if( g.Name!=null ) sG += g.Name;
				sG += "; ";
			}
			if( bNumber ) sG = Convert.ToString(n)+": " + sG;
			return sG.Substring( 0, sG.LastIndexOf( ";" )-1 );
		}
		
		// изменение колонок просмотрщика найденного, взависимости от режима сравнения
		private void MakeColumns( int nMode ) {
			lvResult.Columns.Clear();
			if( nMode == 0 ) {
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
			lvResult.Columns.Add( "Кодировка", 90, HorizontalAlignment.Center );
			lvResult.Columns.Add( "Валидность", 50, HorizontalAlignment.Center );
			lvResult.Columns.Add( "Размер", 90, HorizontalAlignment.Center );
			
			lvResult.Columns.Add( "Дата создания", 120 );
			lvResult.Columns.Add( "Последнее изменение", 120 );
		}

		private string GetFileLength( string sFilePath ) {
			FileInfo fi = new FileInfo( sFilePath );
			return filesWorker.FormatFileLength( fi.Length );
        }
		
		// время создания файла
		private  string GetFileCreationTime( string sFilePath ) {
			FileInfo fi = new FileInfo( sFilePath );
			return fi.CreationTime.ToString();
        }
		
		// время последней записи в файл
		private  string FileLastWriteTime( string sFilePath ) {
			FileInfo fi = new FileInfo( sFilePath );
			return fi.LastWriteTime.ToString();
        }
		
		private string IsValid( string sFilePath ) {
			FB2Validator fv2Validator = new FB2Validator();
			return fv2Validator.ValidatingFB2File( sFilePath ) == "" ? "Да" : "Нет";
        }
		
		private string GetSubtring( string sP, string sStart, string sEnd ) {
			string s = "";
			int nStart = sP.IndexOf( sStart );
			int nEnd = -1;
			if( nStart!=-1 ) {
				nEnd = sP.IndexOf( sEnd );
				if( nEnd!=-1 ) {
					nEnd += sEnd.Length;
				}
				s = sP.Substring( nStart, nEnd-nStart );
			}
			return s;
		}
		// извлечение информации из текста тэга <p>, убираем инлайн-теги для простоты
		private string GetDataFromTagP( string sP ) {
			sP = sP.Replace( "</p>","\r\n" ); sP = sP.Replace( "</P>","\r\n" );
			string s = GetSubtring( sP, "<p ", ">" );
			if( s.Length!=0 ) sP = sP.Replace( s, "" );
			s = GetSubtring( sP, "<P ", ">" );
			if( s.Length!=0 ) sP = sP.Replace( s, "" );
			string[] sIT = { "<strong>", "<STRONG>", "</strong>", "</STRONG>",
								"<emphasis>", "<EMPHASIS>", "</emphasis>", "</EMPHASIS>",
								"<sup>", "<SUP>", "</sup>", "</SUP>",
								"<sub>", "<SUB>", "</sub>", "</SUB>",
								"<code>", "<CODE>", "</code>", "</CODE>",
								"<strikethrough>", "<STRIKETHROUGH>", "</strikethrough>", "</STRIKETHROUGH>" };
			foreach( string sT in sIT ) {
				sP = sP.Replace( sT, "" );
			}
			return sP;
		}
		
		#endregion
	
		#region Реализация Copy/Move/Delete помеченных книг
		// Копирование, перемещение, удаление файлов
		public void CopyOrMoveFilesTo( BackgroundWorker bw, DoWorkEventArgs e,
		                       bool bCopy, string sSource, string sTarget, string sTempDir, 
		                       ListView lvResult,
		                       int nFileExistMode, bool bAddFileNameBookID,
		                       ToolStripStatusLabel tsslblProgress, string sProgressText ) {
			// копировать или переместить файлы в...
			#region Код
			tsslblProgress.Text = sProgressText;
			System.Windows.Forms.ListView.CheckedListViewItemCollection checkedItems = lvResult.CheckedItems;
			int i=0;
			foreach( ListViewItem lvi in checkedItems ) {
				// Проверить флаг на остановку процесса 
				if( ( bw.CancellationPending == true ) ) {
					e.Cancel = true; // Выставить окончание - по отмене, сработает событие bwcmd_RunWorkerCompleted
					return;
				} else {
					string sFilePath = lvi.Text;
					string sNewPath = sTarget + sFilePath.Remove( 0, sSource.Length );
					FileInfo fi = new FileInfo( sNewPath );
					if( !fi.Directory.Exists ) {
						Directory.CreateDirectory( fi.Directory.ToString() );
					}
					string sSufix = "";
					if( File.Exists( sNewPath ) ) {
						if( nFileExistMode==0 ) {
							File.Delete( sNewPath );
						} else {
							if( bAddFileNameBookID ) {
								string sExtTemp = Path.GetExtension( sFilePath ).ToLower();
								if( sExtTemp != ".fb2" ) {
									filesWorker.RemoveDir( sTempDir );
//									Directory.CreateDirectory( sTempDir );
									if( sExtTemp.ToLower() == ".rar" ) {
										archivesWorker.unrar( Settings.Settings.ReadUnRarPath(), sFilePath, sTempDir, ProcessPriorityClass.AboveNormal );
									} else {
										archivesWorker.unzip( Settings.Settings.Read7zaPath(), sFilePath, sTempDir, ProcessPriorityClass.AboveNormal );
									}
									if( Directory.Exists( sTempDir ) ) {
										string [] files = Directory.GetFiles( sTempDir );
										try {
											sSufix = "_" + stringProcessing.GetBookID( files[0] );
										} catch { }
										filesWorker.RemoveDir( sTempDir );
									}
								} else {
									try {
										sSufix = "_" + stringProcessing.GetBookID( sFilePath );
									} catch { }
								}
							}
							if( nFileExistMode == 1 ) {
								// Добавить к создаваемому файлу очередной номер
								sSufix += "_" + stringProcessing.GetFileNewNumber( sNewPath ).ToString();
							} else {
								// Добавить к создаваемому файлу дату и время
								sSufix += "_" + stringProcessing.GetDateTimeExt();
							}
						
							string sFB2File = sNewPath.ToLower();
							if( sFB2File.IndexOf( ".fb2" )!=1 ) {
								sFB2File = sFB2File.Substring( 0, sFB2File.IndexOf( ".fb2" )+4 );
							}
							string sExt = sNewPath.Remove( 0, sFB2File.Length );
							if( sExt.Length == 0 ) {
								sExt = Path.GetExtension( sNewPath );
								sNewPath = sNewPath.Remove( sNewPath.Length - sExt.Length ) + sSufix + sExt;
							} else {
								sExt = Path.GetExtension( sFB2File ) + Path.GetExtension( sNewPath );
								sNewPath = sNewPath.Remove( sNewPath.Length - sExt.Length ) + sSufix + sExt;
							}
						}
					}
				
					Regex rx = new Regex( @"\\+" );
					sFilePath = rx.Replace( sFilePath, "\\" );
					if( File.Exists( sFilePath ) ) {
						if( bCopy ) {
							File.Copy( sFilePath, sNewPath );
						} else {
							File.Move( sFilePath, sNewPath );
							ListViewGroup lvg = lvi.Group;
							lvResult.Items.Remove( lvi );
							if( lvg.Items.Count == 0 ) lvResult.Groups.Remove( lvg );
						}
					}
					bw.ReportProgress( ++i ); // отобразим данные в контролах
				}
			}
			#endregion
		}
		
		public void DeleteFiles( BackgroundWorker bw, DoWorkEventArgs e,
		                 ListView lvResult,
		                 ToolStripStatusLabel tsslblProgress, string sProgressText ) {
			// удалить помеченные файлы...
			#region Код
			tsslblProgress.Text = sProgressText;
			System.Windows.Forms.ListView.CheckedListViewItemCollection checkedItems = lvResult.CheckedItems;
			int i=0;
			foreach( ListViewItem lvi in checkedItems ) {
				// Проверить флаг на остановку процесса 
				if( ( bw.CancellationPending == true ) ) {
					e.Cancel = true; // Выставить окончание - по отмене, сработает событие bwcmd_RunWorkerCompleted
					return;
				} else {
					string sFilePath = lvi.Text;
					if( File.Exists( sFilePath) ) {
						File.Delete( sFilePath );
					}
					
					ListViewGroup lvg = lvi.Group;
					lvResult.Items.Remove( lvi );
					if( lvg.Items.Count == 0 )
						lvResult.Groups.Remove( lvg );
					
					bw.ReportProgress( ++i ); // отобразим данные в контролах
				}
			}
			#endregion
		}
		#endregion
		
		#region Обработчики событий
		void TboxSourceDirTextChanged(object sender, EventArgs e)
		{
			Settings.SettingsFB2Dup.DupScanDir = tboxSourceDir.Text;
		}
		
		void TboxDupToDirTextChanged(object sender, EventArgs e)
		{
			Settings.SettingsFB2Dup.DupToDir = tboxDupToDir.Text;
		}
		
		void TsbtnOpenDirClick(object sender, EventArgs e)
		{
			// задание папки с fb2-файлами и архивами для сканирования
			filesWorker.OpenDirDlg( tboxSourceDir, fbdScanDir, "Укажите папку для сканирования с fb2-файлами и архивами:" );
		}
		
		void TsbtnToDirClick(object sender, EventArgs e)
		{
			// задание папки-приемника для размешения копий
			filesWorker.OpenDirDlg( tboxDupToDir, fbdScanDir, "Укажите папку-приемник для размешения копий книг:" );
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
			
			m_sMessTitle = "SharpFBTools - Поиск одинаковых fb2-файлов";
			// проверка на корректность данных папок источника и приемника файлов
			if( !IsScanFolderDataCorrect( tboxSourceDir ) ) {
				return;
			}
			// проверка на наличие архиваторов
/*			if( !IsArchivatorsExist() ) {
				return;
			}*/
			
			m_sSource = tboxSourceDir.Text;
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
		
		void LvResultSelectedIndexChanged(object sender, EventArgs e)
		{
			// занесение данных книги в контролы для просмотра
			#region Код
			ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
			if( si.Count > 0 ) {
				// пропускаем ситуацию, когда курсор переходит от одной строки к другой - нет выбранного item'а
				FB2BookDataForDup	bd	= new FB2BookDataForDup( si[0].Text );
				// считываем данные TitleInfo
				m_mscLV.ListViewStatus( lvTitleInfo, 0, bd.TIBookTitle );
				m_mscLV.ListViewStatus( lvTitleInfo, 1, bd.TIGenres );
				m_mscLV.ListViewStatus( lvTitleInfo, 2, bd.TILang );
				m_mscLV.ListViewStatus( lvTitleInfo, 3, bd.TISrcLang );
				m_mscLV.ListViewStatus( lvTitleInfo, 4, bd.TIAuthors );
				m_mscLV.ListViewStatus( lvTitleInfo, 5, bd.TIDate );
				m_mscLV.ListViewStatus( lvTitleInfo, 6, bd.TIKeywords );
				m_mscLV.ListViewStatus( lvTitleInfo, 7, bd.TITranslators );
				m_mscLV.ListViewStatus( lvTitleInfo, 8, bd.TISequences );
				m_mscLV.ListViewStatus( lvTitleInfo, 9, (bd.TICoverpage.Split('#').Length-1).ToString() );
				// считываем данные SourceTitleInfo
				m_mscLV.ListViewStatus( lvSourceTitleInfo, 0, bd.STIBookTitle );
				m_mscLV.ListViewStatus( lvSourceTitleInfo, 1, bd.STIGenres );
				m_mscLV.ListViewStatus( lvSourceTitleInfo, 2, bd.STILang );
				m_mscLV.ListViewStatus( lvSourceTitleInfo, 3, bd.STISrcLang );
				m_mscLV.ListViewStatus( lvSourceTitleInfo, 4, bd.STIAuthors );
				m_mscLV.ListViewStatus( lvSourceTitleInfo, 5, bd.STIDate );
				m_mscLV.ListViewStatus( lvSourceTitleInfo, 6, bd.STIKeywords );
				m_mscLV.ListViewStatus( lvSourceTitleInfo, 7, bd.STITranslators );
				m_mscLV.ListViewStatus( lvSourceTitleInfo, 8, bd.STISequences );
				m_mscLV.ListViewStatus( lvSourceTitleInfo, 9, (bd.STICoverpage.Split('#').Length-1).ToString() );
				// считываем данные DocumentInfo
				m_mscLV.ListViewStatus( lvDocumentInfo, 0, bd.DIID );
				m_mscLV.ListViewStatus( lvDocumentInfo, 1, bd.DIVersion );
				m_mscLV.ListViewStatus( lvDocumentInfo, 2, bd.DIFB2Date );
				m_mscLV.ListViewStatus( lvDocumentInfo, 3, bd.DIProgramUsed );
				m_mscLV.ListViewStatus( lvDocumentInfo, 4, bd.DISrcOcr );
				m_mscLV.ListViewStatus( lvDocumentInfo, 5, bd.DISrcUrls );
				m_mscLV.ListViewStatus( lvDocumentInfo, 6, bd.DIFB2Authors );
				// считываем данные PublishInfo
				m_mscLV.ListViewStatus( lvPublishInfo, 0, bd.PIBookName );
				m_mscLV.ListViewStatus( lvPublishInfo, 1, bd.PIPublisher );
				m_mscLV.ListViewStatus( lvPublishInfo, 2, bd.PIYear );
				m_mscLV.ListViewStatus( lvPublishInfo, 3, bd.PICity );
				m_mscLV.ListViewStatus( lvPublishInfo, 4, bd.PIISBN );
				m_mscLV.ListViewStatus( lvPublishInfo, 5, bd.PISequences );
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
				rtbHistory.Clear(); rtbHistory.Text = GetDataFromTagP( bd.DIHistory );
				// считываем данные Annotation
				rtbAnnotation.Clear(); rtbAnnotation.Text = GetDataFromTagP( bd.TIAnnotation );
			}
			#endregion
		}
		
		void TboxSourceDirKeyPress(object sender, KeyPressEventArgs e)
		{
			// запуск сканирования по нажатию Enter на поле ввода папки для сканирования
			if ( e.KeyChar == (char)Keys.Return ) {
				TsbtnSearchDublsClick( sender, e );
			}
		}
		
		void CboxDupExistFileSelectedIndexChanged(object sender, EventArgs e)
		{
			chBoxAddFileNameBookID.Enabled = ( cboxDupExistFile.SelectedIndex != 0 );
		}
		
		void LvResultItemChecked(object sender, ItemCheckedEventArgs e)
		{
			// (раз)блокировка кнопок групповой обработки помеченных книг
			if( lvResult.CheckedItems.Count > 0 ) {
				tsbtnDupCopy.Enabled	= true;
				tsbtnDupMove.Enabled	= true;
				tsbtnDupDelete.Enabled	= true;
			} else {
				tsbtnDupCopy.Enabled	= false;
				tsbtnDupMove.Enabled	= false;
				tsbtnDupDelete.Enabled	= false;
			}
		}
		
		void TsbtnDupCopyClick(object sender, EventArgs e)
		{
			// копировать помеченные файлы в папку-приемник
			string sMessTitle	= "SharpFBTools - Копирование копий книг";
			string sTarget		= filesWorker.WorkingDirPath( tboxDupToDir.Text.Trim() );
			tboxDupToDir.Text	= sTarget;
			// проверки корректности путей к папкам
			if( sTarget.Length == 0 ) {
				MessageBox.Show( "Не указана папка-приемник файлов!\nРабота прекращена.",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			if( m_sSource == sTarget ) {
				MessageBox.Show( "Папка-приемник файлов совпадает с папкой сканирования!\nРабота прекращена.",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			// проверка папки-приемника и создание ее, если нужно
			if( !filesWorker.CreateDirIfNeed( sTarget, sMessTitle ) ) {
				return;
			}
			
			int nCount = lvResult.CheckedItems.Count;
			string sMess = "Вы действительно хотите скопировать "+nCount.ToString()+" отмеченные книги в папку \""+sTarget+"\"?";
			DialogResult result = MessageBox.Show( sMess, sMessTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question );
	        if(result == DialogResult.No) {
				tsslblProgress.Text = Settings.Settings.GetReady();
				SetFilesWorkerStartEnabled( true );
	            return;
			}
			
			m_sFileWorkerMode = "Copy";
			// инициализация контролов
			tsProgressBar.Maximum 	= nCount;
			tsProgressBar.Value		= 0;
			SetFilesWorkerStartEnabled( false );
			
			// Запуск процесса DoWork
			if( m_bwcmd.IsBusy != true ) {
				// если не занят то запустить процесс
            	m_bwcmd.RunWorkerAsync();
			}
		}
		
		void TsbtnDupMoveClick(object sender, EventArgs e)
		{
			// переместить помеченные файлы в папку-приемник
			string sMessTitle = "SharpFBTools - Перемещение копий книг";
			string sTarget		= filesWorker.WorkingDirPath( tboxDupToDir.Text.Trim() );
			tboxDupToDir.Text	= sTarget;
			// проверки корректности путей к папкам
			if( sTarget.Length == 0 ) {
				MessageBox.Show( "Не указана папка-приемник файлов!\nРабота прекращена.",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			if( m_sSource == sTarget ) {
				MessageBox.Show( "Папка-приемник файлов совпадает с папкой сканирования!\nРабота прекращена.",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			// проверка папки-приемника и создание ее, если нужно
			if( !filesWorker.CreateDirIfNeed( sTarget, sMessTitle ) ) {
				return;
			}
			
			int nCount = lvResult.CheckedItems.Count;
			string sMess = "Вы действительно хотите переместить "+nCount.ToString()+" отмеченные книги в папку \""+sTarget+"\"?";
			MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			DialogResult result;
			result = MessageBox.Show( sMess, sMessTitle, buttons, MessageBoxIcon.Question );
	        if(result == DialogResult.No) {
				tsslblProgress.Text = Settings.Settings.GetReady();
				SetFilesWorkerStartEnabled( true );
	            return;
			}
			
			m_sFileWorkerMode = "Move";
			// инициализация контролов
			tsProgressBar.Maximum 	= nCount;
			tsProgressBar.Value		= 0;
			SetFilesWorkerStartEnabled( false );
			
			// Запуск процесса DoWork
			if( m_bwcmd.IsBusy != true ) {
				// если не занят то запустить процесс
            	m_bwcmd.RunWorkerAsync();
			}
		}
		
		void TsbtnDupDeleteClick(object sender, EventArgs e)
		{
			// удалить помеченные файлы
			string sMessTitle = "SharpFBTools - Удаление копий книг";
			int nCount = lvResult.CheckedItems.Count;
			string sMess = "Вы действительно хотите удалить "+nCount.ToString()+" помеченные копии книг?";
			MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			DialogResult result;
			result = MessageBox.Show( sMess, sMessTitle, buttons, MessageBoxIcon.Question );
	        if( result == DialogResult.No ) {
	            return;
			}
			
			m_sFileWorkerMode = "Delete";
			// инициализация контролов
			tsProgressBar.Maximum 	= nCount;
			tsProgressBar.Value		= 0;
			SetFilesWorkerStartEnabled( false );
		
			// Запуск процесса DoWork
			if( m_bwcmd.IsBusy != true ) {
				// если не занят то запустить процесс
            	m_bwcmd.RunWorkerAsync();
			}
		}
		
		void TsbtnDupWorkStopClick(object sender, EventArgs e)
		{
			// Остановка выполнения процесса копирования(перемещения, удаления) fb2-файлов
			if( m_bwcmd.WorkerSupportsCancellation == true ) {
				m_bwcmd.CancelAsync();
			}
		}
		#endregion
		
		#region Обработчики для контекстного меню
		void TsmiDeleteFileFromDiskClick(object sender, EventArgs e)
		{
			// удаление выделенного файла с диска
			#region Код
			if( lvResult.Items.Count > 0 && lvResult.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
				ListViewGroup	lvg			= si[0].Group;
				string			sFilePath	= si[0].SubItems[0].Text.Split('/')[0];
				string sTitle = "SharpFBTools - Удаление файла с диска";
				if( !File.Exists( sFilePath ) ) {
					if( MessageBox.Show( "Файл: "+sFilePath+"\" не найден!\nУдалить путь к этому файлу из списка?",
					                    sTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes ) {
						lvResult.Items[ lvResult.SelectedItems[0].Index ].Remove();
					}
				} else {
					if( MessageBox.Show( "Вы действительно хотите удалить файл: "+sFilePath+"\" с диска?", sTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes ) {
						File.Delete( sFilePath );
						lvResult.Items[ lvResult.SelectedItems[0].Index ].Remove();
					} else return;
				}
				
				// новое число групп и книг во всех группах
				// число групп
				if( lvg.Items.Count == 0 )
					lvResult.Groups.Remove( lvg );					
				m_mscLV.ListViewStatus( lvFilesCount, 5, lvResult.Groups.Count.ToString() );
				// число книг во всех группах
				m_mscLV.ListViewStatus( lvFilesCount, 6, lvResult.Items.Count.ToString() );
			}
			#endregion
		}
		
		void TsmiOpenFileDirClick(object sender, EventArgs e)
		{
			// Открыть папку для выделенного файла
			#region Код
			if( lvResult.Items.Count > 0 && lvResult.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
				FileInfo fi = new FileInfo( si[0].SubItems[0].Text.Split('/')[0] );
				string sDir = fi.Directory.ToString();
				if( !Directory.Exists( sDir ) ) {
					MessageBox.Show( "Папка: "+sDir+"\" не найдена!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				filesWorker.ShowAsyncDir( sDir );
			}
			#endregion
		}
		
		void TsmiViewInReaderClick(object sender, EventArgs e)
		{
			// запустить файл в fb2-читалке (Просмотр)
			#region Код
			// читаем путь к читалке из настроек
			string sFBReaderPath = Settings.Settings.ReadFBReaderPath();
			string sTitle = "SharpFBTools - Открытие папки для файла";
			if( !File.Exists( sFBReaderPath ) ) {
				MessageBox.Show( "Не могу найти Читалку \""+sFBReaderPath+"\"!\nПроверьте, правильно ли задан путь в Настройках.", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			if( lvResult.Items.Count > 0 && lvResult.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
				string sFilePath = si[0].SubItems[0].Text.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: "+sFilePath+"\" не найден!", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				filesWorker.StartAsyncFile( sFBReaderPath, sFilePath );
			}
			#endregion
		}
		
		void TsmiEditInFB2EditorClick(object sender, EventArgs e)
		{
			// редактировать выделенный файл в fb2-редакторе
			#region Код
			// читаем путь к FBE из настроек
			string sFBEPath = Settings.Settings.ReadFBEPath();
			string sTitle = "SharpFBTools - Открытие файла в fb2-редакторе";
			if( !File.Exists( sFBEPath ) ) {
				MessageBox.Show( "Не могу найти fb2-редактор \""+sFBEPath+"\"!\nПроверьте, правильно ли задан путь в Настройках.", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			if( lvResult.Items.Count > 0 && lvResult.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
				string sFilePath = si[0].SubItems[0].Text.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: "+sFilePath+"\" не найден!", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				filesWorker.StartAsyncFile( sFBEPath, sFilePath );
			}
			#endregion
		}
		
		void TsmiEditInTextEditorClick(object sender, EventArgs e)
		{
			// редактировать выделенный файл в текстовом редакторе
			#region Код
			// читаем путь к текстовому редактору из настроек
			string sTFB2Path = Settings.Settings.ReadTextFB2EPath();
			string sTitle = "SharpFBTools - Открытие файла в текстовом редакторе";
			if( !File.Exists( sTFB2Path ) ) {
				MessageBox.Show( "Не могу найти текстовый редактор \""+sTFB2Path+"\"!\nПроверьте, правильно ли задан путь в Настройках.", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			if( lvResult.Items.Count > 0 && lvResult.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
				string sFilePath = si[0].SubItems[0].Text.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: "+sFilePath+"\" не найден!", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				filesWorker.StartAsyncFile( sTFB2Path, sFilePath );
			}
			#endregion
		}
		
		void TsmiFileReValidateClick(object sender, EventArgs e)
		{
			// Повторная Проверка выбранного fb2-файла или архива (Валидация)
			#region Код
			if( lvResult.Items.Count > 0 && lvResult.SelectedItems.Count != 0 ) {
				DateTime dtStart = DateTime.Now;
				string sTempDir = Settings.Settings.GetTempDir();
				ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
				string sSelectedItemText = si[0].SubItems[0].Text;
				string sFilePath = sSelectedItemText.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: "+sFilePath+"\" не найден!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				MessageBoxIcon mbi = MessageBoxIcon.Information;
				string sMsg			= "";
				string sErrorMsg	= "СООБЩЕНИЕ ОБ ОШИБКЕ:";
				string sOkMsg		= "ОШИБОК НЕТ - ФАЙЛ ВАЛИДЕН";
				FB2Validator fv2V = new FB2Validator();
				// для несжатого fb2-файла
				sMsg = fv2V.ValidatingFB2File( sFilePath );
				if ( sMsg == "" ) {
           			// файл валидный
           			mbi = MessageBoxIcon.Information;
					sErrorMsg = sOkMsg;
					si[0].SubItems[6].Text = "Да";
				} else {
					// файл не валидный
					mbi = MessageBoxIcon.Error;
					si[0].SubItems[6].Text = "Нет";
				}
				DateTime dtEnd = DateTime.Now;
				string sTime = dtEnd.Subtract( dtStart ).ToString() + " (час.:мин.:сек.)";
				MessageBox.Show( "Проверка выделенного файла на соответствие FictionBook.xsd схеме завершена.\nЗатрачено времени: "+sTime+"\n\nФайл: \""+sFilePath+"\"\n\n"+sErrorMsg+"\n"+sMsg, "SharpFBTools - "+sErrorMsg, MessageBoxButtons.OK, mbi );
			}
			#endregion
		}
		
		void TsmiDiffFB2Click(object sender, EventArgs e)
		{
			// diff - две помеченные fb2-книги
			#region Код
			ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
			if( lvResult.Items.Count > 0 && lvResult.SelectedItems.Count != 0 ) {
				// проверка на наличие diff-программы
				string sDiffPath = Settings.Settings.ReadDiffPath();
			
				if( sDiffPath.Trim().Length==0 ) {
					MessageBox.Show( "В Настройках не указан путь к установленной diff-программе визуального сравнения файлов!\nУкажите путь к ней в Настройках.\nРабота остановлена!",
					                "SharpFBTools - diff", MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return;
				} else {
					if( !File.Exists( sDiffPath ) ) {
						MessageBox.Show( "Не найден файл diff-программы визуального сравнения файлов \""+sDiffPath+"\"!\nУкажите путь к ней в Настройках.\nРабота остановлена!",
						                "SharpFBTools - diff", MessageBoxButtons.OK, MessageBoxIcon.Warning );
						return;
					}
				}
				// группа для выделенной книги
				ListViewGroup lvg = si[0].Group;
				// книги выбранной группы
				ListView.ListViewItemCollection glvic = lvg.Items;
				List<string> l = new List<string>();
				foreach( ListViewItem lvi in glvic ) {
					if( lvi.Checked ) {
						l.Add( lvi.Text );
					}
					if( l.Count==2 ) break;
				}
				// запускаем инструмент сравнения
				if( l.Count==2 ) {
					filesWorker.StartAsyncDiff( sDiffPath, l[0], l[1] );
				}
			}
			#endregion
		}
		
		void TsmiCheckedAllClick(object sender, EventArgs e)
		{
			// отметить все книги
			m_mscLV.CheckdAllListViewItems( lvResult, true );
		}
		
		void TsmiUnCheckedAllClick(object sender, EventArgs e)
		{
			// снять отметки со всех книг
			m_mscLV.UnCheckdAllListViewItems( lvResult.CheckedItems );
		}
		
		void TsbtnDupSaveListClick(object sender, EventArgs e)
		{
			// сохранение списка найденных копий
			#region Код
			if( lvResult.Items.Count==0 ) {
				MessageBox.Show( "Нет ни одной копии книг!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}

			string sPath = "";
			sfdList.Filter = "SharpFBTools Файлы копий (*.sdf)|*.sdf|Все файлы (*.*)|*.*";
			sfdList.FileName = "";
			sfdList.InitialDirectory = Settings.Settings.GetProgDir();
			DialogResult result = sfdList.ShowDialog();
			if( result == DialogResult.OK ) {
				sPath = sfdList.FileName;
				Environment.CurrentDirectory = Settings.Settings.GetProgDir();
				XmlWriter writer = null;
				try {
					XmlWriterSettings data = new XmlWriterSettings();
					data.Indent = true;
					data.IndentChars = ("\t");
					data.OmitXmlDeclaration = true;
				
					writer = XmlWriter.Create( sPath, data );
					writer.WriteStartElement( "Duplicator" );
						// режим поиска
						writer.WriteStartElement( "Mode" );
							writer.WriteAttributeString( "mode", cboxMode.SelectedIndex.ToString() );
						writer.WriteFullEndElement();
						// число стьолбцов и записей
						writer.WriteStartElement( "Count" );
							writer.WriteAttributeString( "ItemsCount", lvResult.Items.Count.ToString() );
							writer.WriteAttributeString( "ColumnsCount", lvResult.Columns.Count.ToString() );
						writer.WriteFullEndElement();
						// данные поиска
						writer.WriteStartElement( "Items" );
						for( int i=0; i!=lvResult.Items.Count; ++i ) {
							ListViewItem item = lvResult.Items[i];
							writer.WriteStartElement( "item"+i.ToString() );
							for( int j=0; j!=lvResult.Columns.Count; ++j ) {
								writer.WriteAttributeString( "c"+j.ToString(), item.SubItems[j].Text );
							}
							writer.WriteAttributeString( "g", item.Group.Header );
							writer.WriteFullEndElement();
						}
						writer.WriteEndElement();
					writer.WriteEndElement();
					writer.Flush();
				}  finally  {
					if (writer != null)
					writer.Close();
				}
				MessageBox.Show( "Сохранение списка копий завершено!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
			
			#endregion
		}
		
		void TsbtnDupOpenListClick(object sender, EventArgs e)
		{
			// загрузка списка копий
			#region Код
			sfdLoadList.InitialDirectory = Settings.Settings.GetProgDir();
			sfdLoadList.Filter		= "SharpFBTools Файлы копий (*.sdf)|*.sdf|Все файлы (*.*)|*.*";
			sfdLoadList.FileName	= "";
			string sPath = "";
			DialogResult result = sfdLoadList.ShowDialog();
			if( result == DialogResult.OK ) {
				sPath = sfdLoadList.FileName;
				// инициализация контролов
				Init();
				// установка режима поиска
				if( !File.Exists( sPath ) ) {
					MessageBox.Show( "Не найден файл списка Дубликатора: \""+sPath+"\"!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return;
				}
				XmlReaderSettings data = new XmlReaderSettings();
				data.IgnoreWhitespace = true;
				using ( XmlReader reader = XmlReader.Create( sPath, data ) ) {
					try {
						lvResult.BeginUpdate();
						// режим поиска
						reader.ReadToFollowing("Mode");
						if( reader.HasAttributes ) {
							cboxMode.SelectedIndex = Convert.ToInt32( reader.GetAttribute("mode") );
							// изменение колонок просмотрщика найденного, взависимости от режима сравнения
							MakeColumns( cboxMode.SelectedIndex );
						}
						// число стьолбцов и записей
						int nItemsCount	= 0, nColumnsCount = 0;
						reader.ReadToFollowing("Count");
						if( reader.HasAttributes ) {
							nItemsCount		= Convert.ToInt32( reader.GetAttribute("ItemsCount") );
							nColumnsCount	= Convert.ToInt32( reader.GetAttribute("ColumnsCount") );
						}
						// данные поиска
						Hashtable htBookGroups = new Hashtable(); // хеш-таблица групп одинаковых книг
						ListViewGroup lvg = null; // группа одинаковых книг
						ListViewItem lvi = null;
						for( int i=0; i!=nItemsCount; ++i ) {
							reader.ReadToFollowing("item"+i.ToString());
							string sGroup	= reader.GetAttribute("g");
							lvg = new ListViewGroup( sGroup );
							lvi = new ListViewItem( reader.GetAttribute("c0") );
							for( int j=1; j!=nColumnsCount; ++j ) {
								if( reader.HasAttributes ) {
									lvi.SubItems.Add( reader.GetAttribute("c"+j.ToString()) );
								}
							}
							// заносим группу в хеш, если она там отсутствует
							AddBookGroupInHashTable( htBookGroups, lvg );
							// присваиваем группу книге
							lvResult.Groups.Add( (ListViewGroup)htBookGroups[sGroup] );
							lvi.Group = (ListViewGroup)htBookGroups[sGroup];
							lvResult.Items.Add( lvi );
						}
						// Отобразим результат в индикаторе прогресса
						m_mscLV.ListViewStatus( lvFilesCount, 5, lvResult.Groups.Count );
						m_mscLV.ListViewStatus( lvFilesCount, 6, lvResult.Items.Count );
					} catch {
						MessageBox.Show( "Поврежден файл списка Дубликатора: \""+sPath+"\"!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
					} finally {
						lvResult.EndUpdate();
						reader.Close();
					}
				}
			}
			#endregion
		}
		#endregion

	}
}
