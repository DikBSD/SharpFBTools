/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 13.03.2009
 * Time: 14:34
 * 
 * License: GPL 2.1
 */

using System;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using System.Threading;
using System.Diagnostics;

using Settings;
using Core.FB2.FB2Parsers;
using Core.FB2.Description.DocumentInfo;
using Core.StringProcessing;
using Core.FilesWorker;

using filesWorker		= Core.FilesWorker.FilesWorker;
using archivesWorker	= Core.FilesWorker.Archiver;
using stringProcessing	= Core.StringProcessing.StringProcessing;
using FB2Validator		= Core.FB2Parser.FB2Validator;
using ValidatorReports	= Core.Reports.ValidatorReports;

namespace SharpFBTools.Tools
{
	/// <summary>
	/// Description of SFBTpValidator.
	/// </summary>
	public partial class SFBTpFB2Validator : UserControl
	{
		#region Закрытые данные класса
		private string m_s7zPath	= Settings.Settings.Read7zaPath().Trim();
		private string m_sUnRarPath	= Settings.Settings.ReadUnRarPath().Trim();
		private DateTime m_dtStart;
        private BackgroundWorker m_bwv	= null;
        private BackgroundWorker m_bwcmd= null;
        private string m_sMessTitle		= "";
        private string m_sScan			= "";
        private string m_sFileWorkerMode = "";
        private List<string> m_lFilesList	= null;
		#endregion
		
		public SFBTpFB2Validator()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			InitializeValidateBackgroundWorker();
			InitializeFilesWorkerBackgroundWorker();
			cboxExistFile.SelectedIndex = 1;
			// инициализация контролов
			Init();
			// читаем сохраненные пути к папкам Валидатора, если они есть
			ReadValidatorDirs();
		}
		
		#region Закрытые данные класса
		// Color
		private Color	m_FB2ValidFontColor			= Color.Black;		// цвет для несжатых валидных fb2
		private Color	m_FB2NotValidFontColor		= Color.Black;		// цвет для несжатых не валидных fb2
		private Color	m_ZipFB2ValidFontColor		= Color.Blue;		// цвет для валидных fb2 в zip
		private Color	m_ZipFB2NotValidFontColor	= Color.Blue;		// цвет для не валидных fb2 в zip
		private Color	m_RarFB2ValidFontColor		= Color.DarkGreen;	// цвет для валидных fb2 в rar
		private Color	m_RarFB2NotValidFontColor	= Color.DarkGreen;	// цвет для не валидных fb2 в rar
		private Color	m_ZipFontColor				= Color.BlueViolet;	// цвет для zip не fb2
		private Color	m_RarFontColor				= Color.DarkCyan; 	// цвет для rar не fb2
		private Color	m_NotFB2FontColor			= Color.Black;		// цвет для всех остальных файлов
		// найденные файлы
		private int	m_nFB2Valid		= 0; // число валидных файлов
		private int	m_nFB2NotValid	= 0; // число не валидных файлов
		private int	m_nFB2Files		= 0; // число fb2 файлов (не сжатых)
		private int	m_nFB2ZipFiles	= 0; // число fb2.zip файлов
		private int	m_nFB2RarFiles	= 0; // число fb2.rar файлов
		private int	m_nNotFB2Files	= 0; // число других (не fb2) файлов
		//
		private string	m_sNotValid		= " Не валидные fb2-файлы ";
		private	string	m_sValid		= " Валидные fb2-файлы ";
		private string	m_sNotFB2Files	= " Не fb2-файлы ";
		// Report
		private string	m_FB2NotValidReportEmpty		= "Список не валидных fb2-файлов пуст!\nОтчет не сохранен.";
		private string	m_FB2ValidReportEmpty			= "Список валидных fb2-файлов пуст!\nОтчет не сохранен.";
		private string	m_NotFB2FileReportEmpty			= "Список не fb2-файлов пуст!\nОтчет не сохранен.";
		private string	m_FB2NotValidFilesListReport	= "Список не валидных fb2-файлов";
		private string	m_FB2ValidFilesListReport	 	= "Список валидных fb2-файлов";
		private string	m_NotFB2FilesListReport 		= "Список не fb2-файлов";
		private string	m_GeneratingReport				= "Генерация отчета";
		private string	m_ReportSaveOk 		= "Отчет сохранен в файл:\n";
		private string	m_HTMLFilter 		= "HTML файлы (*.html)|*.html|Все файлы (*.*)|*.*";
		private string	m_FB2Filter 		= "fb2 файлы (*.fb2)|*.fb2|Все файлы (*.*)|*.*";
		private string	m_CSV_csv_Filter	= "CVS файлы (*.csv)|*.csv|Все файлы (*.*)|*.*";
		private string	m_TXTFilter 		= "TXT файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
		#endregion
		
		#region Закрытые методы реализации BackgroundWorker Валидатора
		private void InitializeValidateBackgroundWorker() {
			// Инициализация перед использование BackgroundWorker Валидации
            m_bwv = new BackgroundWorker();
            m_bwv.WorkerReportsProgress			= true; // Позволить выводить прогресс процесса
            m_bwv.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
            m_bwv.DoWork 				+= new DoWorkEventHandler( bwv_DoWork );
            m_bwv.ProgressChanged 		+= new ProgressChangedEventHandler( bwv_ProgressChanged );
            m_bwv.RunWorkerCompleted 	+= new RunWorkerCompletedEventHandler( bwv_RunWorkerCompleted );
		}
		
		private void bwv_DoWork( object sender, DoWorkEventArgs e ) {
			// Валидация
			// сортированный список всех вложенных папок
			List<string> lDirList = new List<string>();
			if( !cboxScanSubDir.Checked ) {
				// сканировать только указанную папку
				lDirList.Add( m_sScan );
				lvFilesCount.Items[0].SubItems[1].Text = "1";
			} else {
				// сканировать и все подпапки
				lDirList = filesWorker.DirsParser( m_sScan, lvFilesCount, false );
			}
			
			// сортированный список всех файлов
			m_lFilesList = filesWorker.AllFilesParser( m_bwv, e, lDirList, lvFilesCount, tsProgressBar, false );
			lDirList.Clear();
			
			// проверка остановки процесса
			if( ( m_bwv.CancellationPending == true ) )  {
				e.Cancel = true; // Выставить окончание - по отмене, сработает событие bwv_RunWorkerCompleted
				return;
			}
			
			// проверка, есть ли хоть один файл в папке для сканирования
			if( m_lFilesList.Count == 0 ) {
				MessageBox.Show( "В указанной папке не найдено ни одного файла!", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				Init();
				return;
			}
			
			// проверка файлов
			tsslblProgress.Text		= "Проверка найденных файлов на валидность:";
			tsProgressBar.Maximum	= m_lFilesList.Count;
			tsProgressBar.Value		= 0;

			FB2Validator fv2V = new FB2Validator();
			string sTempDir = Settings.Settings.GetTempDir();
			listViewNotValid.BeginUpdate();
			listViewValid.BeginUpdate();
			listViewNotFB2.BeginUpdate();
			string sExt = "";
			foreach( string sFile in m_lFilesList ) {
				// Проверить флаг на остановку процесса 
				if( ( m_bwv.CancellationPending == true ) ) {
					e.Cancel = true; // Выставить окончание - по отмене, сработает событие bwv_RunWorkerCompleted
					break;
				} else {
					sExt = Path.GetExtension( sFile );
					if( sExt.ToLower() == ".fb2" ) {
						++m_nFB2Files;
						ParseFB2File( sFile, fv2V );
					} else if( sExt.ToLower() == ".zip" || sExt.ToLower() == ".rar" ) {
						// очистка временной папки
						filesWorker.RemoveDir( sTempDir );
						Directory.CreateDirectory( sTempDir );
						ParseArchiveFile( sFile, fv2V, sTempDir );
					} else {
						// разные файлы
						++m_nNotFB2Files;
						ListViewItem item = new ListViewItem( sFile, 0 );
	   					item.ForeColor = m_NotFB2FontColor;
						item.SubItems.Add( Path.GetExtension( sFile ) );
   						FileInfo fi = new FileInfo( sFile );
   						item.SubItems.Add( filesWorker.FormatFileLength( fi.Length ) );
						listViewNotFB2.Items.AddRange( new ListViewItem[]{ item } );

						m_bwv.ReportProgress( 0 ); // отобразим данные в контролах
					}
				}
			}

		}
		
		private void bwv_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
            // Отобразим результат Валидации
			lvFilesCount.Items[2].SubItems[1].Text = m_nFB2Files.ToString();
			lvFilesCount.Items[3].SubItems[1].Text = m_nFB2ZipFiles.ToString();
			lvFilesCount.Items[4].SubItems[1].Text = m_nFB2RarFiles.ToString();
			lvFilesCount.Items[5].SubItems[1].Text = m_nNotFB2Files.ToString();
			
			tpNotFB2Files.Text	= m_sNotFB2Files + "( " + m_nNotFB2Files.ToString() + " ) " ;
			tpValid.Text		= m_sValid + "( " + m_nFB2Valid.ToString() + " ) " ;
			tpNotValid.Text		= m_sNotValid + "( " + m_nFB2NotValid.ToString() + " ) " ;

            ++tsProgressBar.Value;
        }
		
		private void bwv_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {   
            // Проверяем это отмена, ошибка, или конец задачи и сообщить
			listViewNotValid.EndUpdate();
			listViewValid.EndUpdate();
			listViewNotFB2.EndUpdate();
			
            DateTime dtEnd = DateTime.Now;
            m_lFilesList.Clear();
			filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
           
            tsslblProgress.Text = Settings.Settings.GetReady();
			SetValidingStartEnabled( true );
			
            string sTime = dtEnd.Subtract( m_dtStart ).ToString() + " (час.:мин.:сек.)";
			string sMessCanceled	= "Проверка файлов на соответствие FictionBook.xsd схеме остановлена!\nЗатрачено времени: "+sTime;
			string sMessError		= "";
			string sMessDone		= "Проверка файлов на соответствие FictionBook.xsd схеме завершена!\nЗатрачено времени: "+sTime;
           
			if( ( e.Cancelled == true ) ) {
                MessageBox.Show( sMessCanceled, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
            } else if( e.Error != null ) {
                sMessError = "Error!\n" + e.Error.Message + "\n" + e.Error.StackTrace + "\nЗатрачено времени: "+sTime;
            	MessageBox.Show( sMessError, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
            } else {
            	MessageBox.Show( sMessDone, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
            }
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
			switch( m_sFileWorkerMode ) {
				case "Copy":
					switch( tcResult.SelectedIndex ) {
						case 0:
							// не валидные fb2-файлы
							CopyOrMoveFilesTo( m_bwcmd, e, true, tboxSourceDir.Text.Trim(), tboxFB2NotValidDirCopyTo.Text.Trim(),
		                		       listViewNotValid, tpNotValid, "Копирование не валидных fb2-файлов:", m_sNotValid );
							break;
						case 1:
							// валидные fb2-файлы
							CopyOrMoveFilesTo( m_bwcmd, e, true, tboxSourceDir.Text.Trim(), tboxFB2ValidDirCopyTo.Text.Trim(),
		        		               listViewValid, tpValid, "Копирование валидных fb2-файлов:", m_sValid );
							break;
						case 2:
							// не fb2-файлы
							CopyOrMoveFilesTo( m_bwcmd, e, true, tboxSourceDir.Text.Trim(), tboxNotFB2DirCopyTo.Text.Trim(),
				                       listViewNotFB2, tpNotFB2Files, "Копирование не fb2-файлов:", m_sNotFB2Files );
							break;
					}
					break;
				case "Move":
					switch( tcResult.SelectedIndex ) {
						case 0:
							// не валидные fb2-файлы
							CopyOrMoveFilesTo( m_bwcmd, e, false, tboxSourceDir.Text.Trim(), tboxFB2NotValidDirMoveTo.Text.Trim(),
		                		       listViewNotValid, tpNotValid, "Перемещение не валидных fb2-файлов:", m_sNotValid );
							break;
						case 1:
							// валидные fb2-файлы
							CopyOrMoveFilesTo( m_bwcmd, e, false, tboxSourceDir.Text.Trim(), tboxFB2ValidDirMoveTo.Text.Trim(),
		        		               listViewValid, tpValid, "Перемещение валидных fb2-файлов:", m_sValid );
							break;
					case 2:
							// не fb2-файлы
							CopyOrMoveFilesTo( m_bwcmd, e, false, tboxSourceDir.Text.Trim(), tboxNotFB2DirMoveTo.Text.Trim(),
		                    		   listViewNotFB2, tpNotFB2Files, "Перемещение не fb2-файлов:", m_sNotFB2Files );
							break;
					}
					break;
				case "Delete":
					switch( tcResult.SelectedIndex ) {
						case 0:
							// не валидные fb2-файлы
							DeleteFiles( m_bwcmd, e, listViewNotValid, tpNotValid, "Удаление не валидных fb2-файлов:", m_sNotValid );
							break;
						case 1:
							// валидные fb2-файлы
							DeleteFiles( m_bwcmd, e, listViewValid, tpValid, "Удаление валидных fb2-файлов:", m_sValid );
							break;
						case 2:
							// не fb2-файлы
							DeleteFiles( m_bwcmd, e, listViewNotFB2, tpNotFB2Files, "Удаление не fb2-файлов:", m_sNotFB2Files );
							break;
					}
					break;
				default:
					return;
			}
		}
		
		private void bwcmd_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
            // Отобразим результат Копирования / Перемещения / Удаления
            ++tsProgressBar.Value;
        }
		
		private void bwcmd_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {   
            // Завершение работы Обработчика Файлов
			string sMessCanceled, sMessError, sMessDone, sTabPageDefText, sMessTitle;
            sMessCanceled = sMessError = sMessDone = sTabPageDefText = sMessTitle = "";
            ListView lw	= null;
            TabPage	tp	= null;
            switch( tcResult.SelectedIndex ) {
				case 0:
					// не валидные fb2-файлы
					sTabPageDefText = m_sNotValid;
					lw = listViewNotValid;
					tp = tpNotValid;
					break;
				case 1:
					// валидные fb2-файлы
					sTabPageDefText = m_sValid;
					lw = listViewValid;
					tp = tpValid;
					break;
				case 2:
					// не fb2-файлы
					sTabPageDefText = m_sNotFB2Files;
					lw = listViewNotFB2;
					tp = tpNotFB2Files;
					break;
			}
            
            switch( m_sFileWorkerMode ) {
            	case "Copy":
					sMessTitle		= "SharpFBTools - Копирование файлов";
            		sMessDone 		= "Копирование файлов в указанную папку завершено!";
					sMessCanceled	= "Копирование файлов в указанную папку остановлено!";
            		break;
            	case "Move":
					sMessTitle		= "SharpFBTools - Перемещение файлов";
            		sMessDone 		= "Перемещение файлов в указанную папку завершено!";
					sMessCanceled	= "Перемещение файлов в указанную папку остановлено!";
					tp.Text	= sTabPageDefText;
            		break;
            	case "Delete":
            		sMessTitle		= "SharpFBTools - Удаление файлов";
            		sMessDone 		= "Удаление файлов из папки-источника завершено!";
					sMessCanceled	= "Удаление файлов из папки-источника остановлено!";
					tp.Text	= sTabPageDefText;
            		break;
            }
            lvFilesCount.Items[1].SubItems[1].Text = ( listViewNotValid.Items.Count + listViewValid.Items.Count +
														listViewNotFB2.Items.Count ).ToString();
			tsslblProgress.Text = Settings.Settings.GetReady();
			SetFilesWorkerStartEnabled( true );
			
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
		private void Init() {
			// инициализация контролов и переменных
			for( int i=0; i!=lvFilesCount.Items.Count; ++i ) {
				lvFilesCount.Items[i].SubItems[1].Text	= "0";
			}
			listViewNotValid.Items.Clear();
			listViewValid.Items.Clear();
			listViewNotFB2.Items.Clear();
			rеboxNotValid.Clear();
			tpNotValid.Text		= m_sNotValid;
			tpValid.Text		= m_sValid;
			tpNotFB2Files.Text	= m_sNotFB2Files;
			tsProgressBar.Value	= 0;
			tsslblProgress.Text		= Settings.Settings.GetReady();
			tsProgressBar.Visible	= false;
			m_nFB2Valid	= m_nFB2NotValid = m_nFB2Files = m_nFB2ZipFiles = m_nFB2RarFiles = m_nNotFB2Files = 0;
			// очистка временной папки
			filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
		}
		
		private void SetValidingStartEnabled( bool bEnabled ) {
			// доступность контролов при Валидации
			tcResult.Enabled			= bEnabled;
			tSBValidate.Enabled			= bEnabled;
			tsbtnCopyFilesTo.Enabled	= bEnabled;
			tsbtnMoveFilesTo.Enabled	= bEnabled;
			tsbtnDeleteFiles.Enabled	= bEnabled;
			tsddbtnMakeFileList.Enabled	= bEnabled;
			tsddbtnMakeReport.Enabled	= bEnabled;
			pScanDir.Enabled			= bEnabled;
			gboxCopyMoveOptions.Enabled	= bEnabled;
			tSBValidateStop.Enabled		= !bEnabled;
			tsProgressBar.Visible		= !bEnabled;
			tcResult.Refresh();
			ssProgress.Refresh();
		}
		
		private void SetFilesWorkerStartEnabled( bool bEnabled ) {
			// доступность контролов при Обработке файлов
			tcResult.Enabled			= bEnabled;
			tSBValidate.Enabled			= bEnabled;
			tsbtnCopyFilesTo.Enabled	= bEnabled;
			tsbtnMoveFilesTo.Enabled	= bEnabled;
			tsbtnDeleteFiles.Enabled	= bEnabled;
			tsddbtnMakeFileList.Enabled	= bEnabled;
			tsddbtnMakeReport.Enabled	= bEnabled;
			pScanDir.Enabled			= bEnabled;
			gboxCopyMoveOptions.Enabled	= bEnabled;
			tsbtnFilesWorkStop.Enabled	= !bEnabled;
			tsProgressBar.Visible		= !bEnabled;
			tcResult.Refresh();
			ssProgress.Refresh();
		}
		
		private ListView GetCurrentListWiew()
		{
			// возвращает текущий ListView в зависимости от выбранной вкладки
			ListView l = null;
			switch( tcResult.SelectedIndex ) {
				case 0:
					// не валидные fb2-файлы
					l = listViewNotValid;
					break;
				case 1:
					// валидные fb2-файлы
					l = listViewValid;
					break;
				case 2:
					// не fb2-файлы
					l = listViewNotFB2;
					break;
			}
			return l;
		}
		
		private void ReadValidatorDirs() {
			// чтение путей к папкам Валидатора из xml-файла
			string sSettings = Settings.Settings.WorksDataSettingsPath;
			if( !File.Exists( sSettings ) ) return;
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.IgnoreWhitespace = true;
			using ( XmlReader reader = XmlReader.Create( sSettings, settings ) ) {
				reader.ReadToFollowing("VScanDir");
				if (reader.HasAttributes ) {
					tboxSourceDir.Text = reader.GetAttribute("tboxSourceDir");
					Settings.SettingsValidator.ScanDir =  tboxSourceDir.Text.Trim();
				}
				reader.ReadToFollowing("VNotValidFB2Files");
				if (reader.HasAttributes ) {
					tboxFB2NotValidDirCopyTo.Text = reader.GetAttribute("tboxFB2NotValidDirCopyTo");
					Settings.SettingsValidator.FB2NotValidDirCopyTo = tboxFB2NotValidDirCopyTo.Text.Trim();
					tboxFB2NotValidDirMoveTo.Text = reader.GetAttribute("tboxFB2NotValidDirMoveTo");
					Settings.SettingsValidator.FB2NotValidDirMoveTo = tboxFB2NotValidDirMoveTo.Text.Trim();
				}
				reader.ReadToFollowing("VValidFB2Files");
				if (reader.HasAttributes ) {
					tboxFB2ValidDirCopyTo.Text = reader.GetAttribute("tboxFB2ValidDirCopyTo");
					Settings.SettingsValidator.FB2ValidDirCopyTo = tboxFB2ValidDirCopyTo.Text.Trim();
					tboxFB2ValidDirMoveTo.Text = reader.GetAttribute("tboxFB2ValidDirMoveTo");
					Settings.SettingsValidator.FB2ValidDirMoveTo = tboxFB2ValidDirMoveTo.Text.Trim();
				}
				reader.ReadToFollowing("VNotFB2Files");
				if (reader.HasAttributes ) {
					tboxNotFB2DirCopyTo.Text = reader.GetAttribute("tboxNotFB2DirCopyTo");
					Settings.SettingsValidator.NotFB2DirCopyTo = tboxNotFB2DirCopyTo.Text.Trim();
					tboxNotFB2DirMoveTo.Text = reader.GetAttribute("tboxNotFB2DirMoveTo");
					Settings.SettingsValidator.NotFB2DirMoveTo = tboxNotFB2DirMoveTo.Text.Trim();
				}
				reader.Close();
			}
		}
		#endregion
				
		#region Парсеры файлов и архивов
		private void ParseFB2File( string sFile, FB2Validator fv2Validator ) {
			// парсер несжатого fb2-файла
			string sMsg = fv2Validator.ValidatingFB2File( sFile );
			if ( sMsg == "" ) {
           		// файл валидный
           		++m_nFB2Valid;
				//listViewValid.Items.Add( sFile );
				ListViewItem item = new ListViewItem( sFile, 0 );
   				item.ForeColor = m_FB2ValidFontColor;
				FileInfo fi = new FileInfo( sFile );
   				item.SubItems.Add( filesWorker.FormatFileLength( fi.Length ) );
				listViewValid.Items.AddRange( new ListViewItem[]{ item } );
           	} else {
           		// файл не валидный
           		++m_nFB2NotValid;
				ListViewItem item = new ListViewItem( sFile, 0 );
   				item.ForeColor = m_FB2NotValidFontColor;
				item.SubItems.Add( sMsg );
   				FileInfo fi = new FileInfo( sFile );
   				item.SubItems.Add( filesWorker.FormatFileLength( fi.Length ) );
				listViewNotValid.Items.AddRange( new ListViewItem[]{ item } );
           	}
			m_bwv.ReportProgress( 0 ); // отобразим данные в контролах
		}
		
		private void ParseArchiveFile( string sArchiveFile, FB2Validator fv2Validator, string sTempDir ) {
			// парсер архива
			//TODO: заменить все unrar на unzip
			string sExt = Path.GetExtension( sArchiveFile );
			if( sExt.ToLower() == ".zip" ) {
				archivesWorker.unzip( m_s7zPath, sArchiveFile, sTempDir, ProcessPriorityClass.AboveNormal );
			} else if( sExt.ToLower() == ".rar" ) {
				archivesWorker.unrar( m_sUnRarPath, sArchiveFile, sTempDir, ProcessPriorityClass.AboveNormal );
			}
			string [] files = Directory.GetFiles( sTempDir );
			if( files.Length <= 0 ) return;
				
			string sMsg = fv2Validator.ValidatingFB2File( files[0] );
			string sFileName = Path.GetFileName( files[0] );

			if ( sMsg == "" ) {
           		// файл валидный - это fb2
           		++m_nFB2Valid;
				//listViewValid.Items.Add( sArchiveFile + "/" + sFileName );
				ListViewItem item = new ListViewItem( sArchiveFile + "/" + sFileName , 0 );
				if( sExt.ToLower() == ".zip" ) {
					item.ForeColor = m_ZipFB2ValidFontColor;
					++m_nFB2ZipFiles;
				} else if( sExt.ToLower() == ".rar" ) {
					item.ForeColor = m_RarFB2ValidFontColor;
					++m_nFB2RarFiles;
				}
				FileInfo fi = new FileInfo( sArchiveFile );
   				string s = filesWorker.FormatFileLength( fi.Length );
   				fi = new FileInfo( sTempDir+"\\"+sFileName );
   				s += " / "+filesWorker.FormatFileLength( fi.Length );
   				item.SubItems.Add( s );
				listViewValid.Items.AddRange( new ListViewItem[]{ item } );
           	} else {
           		// архив не валидный - посмотрим, что это
        		if( sExt.ToLower() == ".zip" ) {
					// определяем тип разархивированного файла
           			sExt = Path.GetExtension( sFileName );
					if( sExt.ToLower() != ".fb2" ) {
           				sMsg = "Тип файла: " + sExt;
           				++m_nNotFB2Files;
           				ListViewItem item = new ListViewItem( sArchiveFile + "/" + sFileName, 0 );
   						item.ForeColor = m_ZipFontColor;
           				item.SubItems.Add( Path.GetExtension( sArchiveFile + "/" + sFileName ) );
   						FileInfo fi = new FileInfo( sArchiveFile );
   						string s = filesWorker.FormatFileLength( fi.Length );
   						fi = new FileInfo( sTempDir+"\\"+sFileName );
   						s += " / "+filesWorker.FormatFileLength( fi.Length );
   						item.SubItems.Add( s );
   						listViewNotFB2.Items.AddRange( new ListViewItem[]{ item } );
					} else {
						++m_nFB2ZipFiles;
						++m_nFB2NotValid;
						ListViewItem item = new ListViewItem( sArchiveFile + "/" + sFileName, 0 );
   						item.ForeColor = m_ZipFB2NotValidFontColor;
						item.SubItems.Add( sMsg );
   						FileInfo fi = new FileInfo( sArchiveFile );
   						string s = filesWorker.FormatFileLength( fi.Length );
   						fi = new FileInfo( sTempDir+"\\"+sFileName );
   						s += " / "+filesWorker.FormatFileLength( fi.Length );
   						item.SubItems.Add( s );
						listViewNotValid.Items.AddRange( new ListViewItem[]{ item } );
					}
				} else if( sExt.ToLower() == ".rar" ) {
					// определяем тип разархивированного файла
           			sExt = Path.GetExtension( sFileName );
					if( sExt.ToLower() != ".fb2" ) {
        				sMsg = "Тип файла: " + sExt;
          				++m_nNotFB2Files;
          				ListViewItem item = new ListViewItem( sArchiveFile + "/" + sFileName, 0 );
   						item.ForeColor = m_RarFontColor;
          				item.SubItems.Add( Path.GetExtension( sArchiveFile + "/" + sFileName ) );
   						FileInfo fi = new FileInfo( sArchiveFile );
   						string s = filesWorker.FormatFileLength( fi.Length );
   						fi = new FileInfo( sTempDir+"\\"+sFileName );
   						s += " / "+filesWorker.FormatFileLength( fi.Length );
   						item.SubItems.Add( s );
   						listViewNotFB2.Items.AddRange( new ListViewItem[]{ item } );
					} else {
						++m_nFB2RarFiles;
						++m_nFB2NotValid;
						ListViewItem item = new ListViewItem( sArchiveFile + "/" + sFileName, 0 );
   						item.ForeColor = m_RarFB2NotValidFontColor;
						item.SubItems.Add( sMsg );
   						FileInfo fi = new FileInfo( sArchiveFile );
   						string s = filesWorker.FormatFileLength( fi.Length );
   						fi = new FileInfo( sTempDir+"\\"+sFileName );
   						s += " / "+filesWorker.FormatFileLength( fi.Length );
   						item.SubItems.Add( s );
						listViewNotValid.Items.AddRange( new ListViewItem[]{ item } );
					}
				}
            }
			m_bwv.ReportProgress( 0 ); // отобразим данные в контролах
		}
		#endregion
		
		#region Копирование, перемещение, удаление файлов
		void CopyOrMoveFilesTo( BackgroundWorker bw, DoWorkEventArgs e,
		                       bool bCopy, string sSource, string sTarget,
		                       ListView lv, TabPage tp,
		                       string sProgressText, string sTabPageDefText ) {
			// копировать или переместить файлы в...
			#region Код
			int nCount = lv.Items.Count;
			tsslblProgress.Text = sProgressText;
			string sTempDir = Settings.Settings.GetTempDir();
			for( int i=0; i!=nCount; ++i ) {
				// Проверить флаг на остановку процесса 
				if( ( bw.CancellationPending == true ) ) {
					e.Cancel = true; // Выставить окончание - по отмене, сработает событие bwcmd_RunWorkerCompleted
					break;
				} else {
					string sItemText = ( bCopy ? lv.Items[i].SubItems[0].Text
					                    : sItemText = lv.Items[0].SubItems[0].Text );
					string sFilePath = sItemText.Split('/')[0];
					string sNewPath = sTarget + sFilePath.Remove( 0, sSource.Length );
					FileInfo fi = new FileInfo( sNewPath );
					if( !fi.Directory.Exists ) {
						Directory.CreateDirectory( fi.Directory.ToString() );
					}
					string sSufix = "";
					if( File.Exists( sNewPath ) ) {
						if( cboxExistFile.SelectedIndex==0 ) {
							File.Delete( sNewPath );
						} else {
							if( chBoxAddBookID.Checked ) {
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
							if( cboxExistFile.SelectedIndex == 1 ) {
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
							lv.Items[0].Remove();
							tp.Text = sTabPageDefText + "( " + Convert.ToString( lv.Items.Count ) +" )";
						}
					}
					bw.ReportProgress( i ); // отобразим данные в контролах
				}
			}
			#endregion
		}
		
		void DeleteFiles( BackgroundWorker bw, DoWorkEventArgs e,
		                 ListView lv, TabPage tp, string sProgressText, string sTabPageDefText ) {
			// удалить файлы...
			#region Код
			int nCount = lv.Items.Count;
			tsslblProgress.Text = sProgressText;
			for( int i=0; i!=nCount; ++i ) {
				// Проверить флаг на остановку процесса 
				if( ( bw.CancellationPending == true ) ) {
					e.Cancel = true; // Выставить окончание - по отмене, сработает событие bwcmd_RunWorkerCompleted
					break;
				} else {
					string sFilePath = lv.Items[0].SubItems[0].Text.Split('/')[0];
					if( File.Exists( sFilePath) ) {
						File.Delete( sFilePath );
						lv.Items[0].Remove();
						tp.Text = sTabPageDefText + "( " + Convert.ToString( lv.Items.Count ) +" )";
					}
					bw.ReportProgress( i ); // отобразим данные в контролах
				}
			}
			#endregion
		}
		#endregion
		
		#region Генерация отчетов
		void MakeReport( int nModeReport ) {
			// создание отчета заданного через nModeReport вида для разных вкладок (видов найденных файлов)
			// 0 - html; 1 - fb2; 3 - csv(csv); 4 - csv(txt)
			switch( tcResult.SelectedIndex ) {
				case 0:
					// не валидные fb2-файлы
					MakeReport( listViewNotValid, m_FB2NotValidReportEmpty, m_FB2NotValidFilesListReport, nModeReport );
					break;
				case 1:
					// валидные fb2-файлы
					MakeReport( listViewValid, m_FB2ValidReportEmpty, m_FB2ValidFilesListReport, nModeReport );
					break;
				case 2:
					// не fb2-файлы
					MakeReport( listViewNotFB2, m_NotFB2FileReportEmpty, m_NotFB2FilesListReport, nModeReport );
					break;
			}
		}
		
		private bool MakeReport( ListView lw, string sReportListEmpty, string sReportTitle, int nModeReport ) {
			// создание отчета
			string sDelem = ";";
			switch( nModeReport ) {
				case 0: // Как html-файл
					// сохранение списка не валидных файлов как html-файла
					if( lw.Items.Count < 1 ) {
						MessageBox.Show( sReportListEmpty, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
						return false;
					} else {
						sfdReport.Filter = m_HTMLFilter;
						sfdReport.FileName = "";
						DialogResult result = sfdReport.ShowDialog();
						if (result == DialogResult.OK) {
    	          			tsslblProgress.Text = m_GeneratingReport;
    	          			tsProgressBar.Visible = true;
							ValidatorReports.MakeHTMLReport( lw, sfdReport.FileName, sReportTitle, tsProgressBar, ssProgress  );
							MessageBox.Show( m_ReportSaveOk+sfdReport.FileName, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
							tsProgressBar.Visible = false;
							tsslblProgress.Text = Settings.Settings.GetReady();
	          			}
					}
					break;
				case 1: // Как fb2-файл
					// сохранение списка не валидных файлов как fb2-файла
					if( lw.Items.Count < 1 ) {
						MessageBox.Show( sReportListEmpty, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
						return false;
					} else {
						sfdReport.Filter = m_FB2Filter;
						sfdReport.FileName = "";
						DialogResult result = sfdReport.ShowDialog();
						if (result == DialogResult.OK) {
    	          			tsslblProgress.Text = m_GeneratingReport;
    	          			tsProgressBar.Visible = true;
							ValidatorReports.MakeFB2Report( lw, sfdReport.FileName, sReportTitle, tsProgressBar, ssProgress );
							MessageBox.Show( m_ReportSaveOk+sfdReport.FileName, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
							tsProgressBar.Visible = false;
							tsslblProgress.Text = Settings.Settings.GetReady();
	          			}
					}
					break;
				case 2: // Как csv-файл (.csv)
					// сохранение списка не валидных файлов как csv-файла
					if( lw.Items.Count < 1 ) {
						MessageBox.Show( sReportListEmpty, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
						return false;
					} else {
						sfdReport.Filter = m_CSV_csv_Filter;
						sfdReport.FileName = "";
						DialogResult result = sfdReport.ShowDialog();
						if (result == DialogResult.OK) {
							tsslblProgress.Text = m_GeneratingReport;
    	          			tsProgressBar.Visible = true;
    	       				ValidatorReports.MakeCSVReport( lw, sfdReport.FileName, sDelem, tsProgressBar, ssProgress );
							MessageBox.Show( m_ReportSaveOk+sfdReport.FileName, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
							tsProgressBar.Visible = false;
							tsslblProgress.Text = Settings.Settings.GetReady();
	        			}
					}
					break;
			}
			return true;
		}
		#endregion
		
		#region Обработчики событий
		void TSBValidateClick(object sender, EventArgs e)
		{
			// Ввлидация fb2-файлов в выбранной папке
			// проверка на наличие архиваторов
			m_s7zPath		= Settings.Settings.Read7zaPath().Trim();
			m_sUnRarPath	= Settings.Settings.ReadUnRarPath().Trim();
			m_sMessTitle	= "SharpFBTools - Валидация";
			
			m_sScan		= tboxSourceDir.Text.Trim();
			Regex rx	= new Regex( @"\\+$" );
			m_sScan		= rx.Replace( m_sScan, "" );
			tboxSourceDir.Text = m_sScan;
			
			// проверки задания папки сканирования
			if( m_sScan.Length == 0 ) {
				MessageBox.Show( "Выберите папку для сканирования!",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			DirectoryInfo diFolder = new DirectoryInfo( m_sScan );
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка для сканирования не найдена: " + m_sScan,
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			
			// проверка на наличие архиваторов и корректность путей к ним
			if( !archivesWorker.IsArchivatorsPathCorrectForUnArchive( m_s7zPath, m_sUnRarPath, m_sMessTitle ) ) {
				return;
			}

			// инициализация контролов
			Init();
			SetValidingStartEnabled( false );
			
			m_dtStart = DateTime.Now;
			tsslblProgress.Text = "Создание списка файлов:";
			
			// Запуск процесса DoWork от Бекграунд Воркера
			if( m_bwv.IsBusy != true ) {
				// если не занят то запустить процесс
            	m_bwv.RunWorkerAsync();
			}
		}
		
		void TsbtnCopyFilesToClick(object sender, EventArgs e)
		{
			// копирование файлов в зависимости от выбранной вкладки
			m_sFileWorkerMode = "Copy";

			string sMessTitle = "SharpFBTools - Копирование файлов";
			string sSource = tboxSourceDir.Text.Trim();
			string sTarget, sType, sType1;
			sTarget = sType = sType1 = "";
			ListView lv = GetCurrentListWiew();
			switch( tcResult.SelectedIndex ) {
				case 0: // не валидные fb2-файлы
					sTarget = tboxFB2NotValidDirCopyTo.Text.Trim();
					sType	= "не валидных fb2-файлов";
					sType1	= "не валидные fb2-файлы";
					break;
				case 1: // валидные fb2-файлы
					sTarget = tboxFB2ValidDirCopyTo.Text.Trim();
					sType	= "валидных fb2-файлов";
					sType1	= "валидные fb2-файлы";
					break;
				case 2: // не fb2-файлы
					sTarget = tboxNotFB2DirCopyTo.Text.Trim();
					sType	= "не fb2-файлов";
					sType1	= "не fb2-файлы";
					break;
			}

			// проверки корректности путей к папкам
			if( lv.Items.Count == 0 ) {
				MessageBox.Show( "Список "+sType+" пуст!\nРабота прекращена.",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			if( sTarget.Length == 0 ) {
				MessageBox.Show( "Не указана папка-приемник файлов!\nРабота прекращена.",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			if( sSource == sTarget ) {
				MessageBox.Show( "Папка-приемник файлов совпадает с папкой сканирования!\nРабота прекращена.",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			DirectoryInfo diFolder = new DirectoryInfo( sTarget );
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка-приемник не найдена: " + sTarget + "\nРабота прекращена.",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			
			string sMess = "Вы действительно хотите скопировать "+sType1+" в папку \""+sTarget+"\"?";
			MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			DialogResult result;
			result = MessageBox.Show( sMess, sMessTitle, buttons, MessageBoxIcon.Question );
	        if(result == DialogResult.No) {
				tsslblProgress.Text = Settings.Settings.GetReady();
				SetFilesWorkerStartEnabled( true );
	            return;
			}
			
			// инициализация контролов
			tsProgressBar.Maximum 	= lv.Items.Count;
			tsProgressBar.Value		= 0;
			SetFilesWorkerStartEnabled( false );
			
			// Запуск процесса DoWork от Бекграунд Воркера
			if( m_bwcmd.IsBusy != true ) {
				// если не занят то запустить процесс
            	m_bwcmd.RunWorkerAsync();
			}
		}
		
		void TsbtnMoveFilesToClick(object sender, EventArgs e)
		{
			// перемещение файлов в зависимости от выбранной вкладки
			m_sFileWorkerMode = "Move";
			
			string sMessTitle = "SharpFBTools - Перемещение файлов";
			string sSource = tboxSourceDir.Text.Trim();
			string sTarget, sType, sType1;
			sTarget = sType = sType1 = "";
			ListView lv = GetCurrentListWiew();
			switch( tcResult.SelectedIndex ) {
				case 0: // не валидные fb2-файлы
					sTarget = tboxFB2NotValidDirMoveTo.Text.Trim();
					sType	= "не валидных fb2-файлов";
					sType1	= "не валидные fb2-файлы";
					break;
				case 1: // валидные fb2-файлы
					sTarget = tboxFB2ValidDirMoveTo.Text.Trim();
					sType	= "валидных fb2-файлов";
					sType1	= "валидные fb2-файлы";
					break;
				case 2: // не fb2-файлы
					sTarget = tboxNotFB2DirMoveTo.Text.Trim();
					sType	= "не fb2-файлов";
					sType1	= "не fb2-файлы";
					break;
			}

			// проверки корректности путей к папкам
			if( lv.Items.Count == 0 ) {
				MessageBox.Show( "Список "+sType+" пуст!\nРабота прекращена.",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			if( sTarget.Length == 0 ) {
				MessageBox.Show( "Не указана папка-приемник файлов!\nРабота прекращена.",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			if( sSource == sTarget ) {
				MessageBox.Show( "Папка-приемник файлов совпадает с папкой сканирования!\nРабота прекращена.",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			DirectoryInfo diFolder = new DirectoryInfo( sTarget );
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка-приемник не найдена: " + sTarget + "\nРабота прекращена.",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			
			string sMess = "Вы действительно хотите переместить "+sType1+" в папку \""+sTarget+"\"?";
			MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			DialogResult result;
			result = MessageBox.Show( sMess, sMessTitle, buttons, MessageBoxIcon.Question );
	        if( result == DialogResult.No ) {
				tsslblProgress.Text = Settings.Settings.GetReady();
				SetFilesWorkerStartEnabled( true );
	            return;
			}
			
			// инициализация контролов
			tsProgressBar.Maximum 	= lv.Items.Count;
			tsProgressBar.Value		= 0;
			SetFilesWorkerStartEnabled( false );
		
			// Запуск процесса DoWork от Бекграунд Воркера
			if( m_bwcmd.IsBusy != true ) {
				// если не занят то запустить процесс
            	m_bwcmd.RunWorkerAsync();
			}
		}
		
		void TsbtnDeleteFilesClick(object sender, EventArgs e)
		{
			// удаление файлов в зависимости от выбранной вкладки
			m_sFileWorkerMode = "Delete";
			
			string sMessTitle = "SharpFBTools - Удаление файлов";
			string sType, sType1;
			sType = sType1 = "";
			ListView lv = GetCurrentListWiew();
			switch( tcResult.SelectedIndex ) {
				case 0: // не валидные fb2-файлы
					sType	= "не валидных fb2-файлов";
					sType1	= "не валидные fb2-файлы";
					break;
				case 1: // валидные fb2-файлы
					sType	= "валидных fb2-файлов";
					sType1	= "валидные fb2-файлы";
					break;
				case 2: // не fb2-файлы
					sType	= "не fb2-файлов";
					sType1	= "не fb2-файлы";
					break;
			}
			int nCount = lv.Items.Count;
			if( nCount == 0) {
				MessageBox.Show( "Список "+sType+" пуст!\nРабота прекращена.",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			string sMess = "Вы действительно хотите удалить "+sType1+"?";
			MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			DialogResult result;
			result = MessageBox.Show( sMess, sMessTitle, buttons, MessageBoxIcon.Question );
	        if( result == DialogResult.No ) {
	            return;
			}
			
			// инициализация контролов
			tsProgressBar.Maximum 	= lv.Items.Count;
			tsProgressBar.Value		= 0;
			SetFilesWorkerStartEnabled( false );
		
			// Запуск процесса DoWork от Бекграунд Воркера
			if( m_bwcmd.IsBusy != true ) {
				// если не занят то запустить процесс
            	m_bwcmd.RunWorkerAsync();
			}
		}
		
		void ListViewNotValidSelectedIndexChanged(object sender, EventArgs e)
		{
			// занесение ошибки валидации в бокс
			ListView.SelectedListViewItemCollection si = listViewNotValid.SelectedItems;
			if( si.Count > 0 ) {
				rеboxNotValid.Text = si[0].SubItems[1].Text;
				if( listViewNotValid.Items.Count > 0 && listViewNotValid.SelectedItems.Count != 0 ) {
					// путь к выделенному файлу
					string s = si[0].SubItems[0].Text.Split('/')[0];
					// отределяем его расширение
					string sExt = Path.GetExtension( s );
					if( sExt.ToLower() == ".fb2" ) {
						listViewNotValid.ContextMenuStrip = cmsFB2;
					} else {
						listViewNotValid.ContextMenuStrip = cmsArchive;
					}
				} else {
					rеboxNotValid.Clear();
				}
			}
		}
		
		void ListViewValidSelectedIndexChanged(object sender, EventArgs e)
		{
			ListView.SelectedListViewItemCollection si = listViewValid.SelectedItems;
			if( listViewValid.Items.Count > 0 && listViewValid.SelectedItems.Count != 0 ) {
				// путь к выделенному файлу
				string s = si[0].SubItems[0].Text.Split('/')[0];
				// отределяем его расширение
				string sExt = Path.GetExtension( s );
				if( sExt.ToLower() == ".fb2" ) {
					listViewValid.ContextMenuStrip = cmsFB2;
				} else {
					listViewValid.ContextMenuStrip = cmsArchive;
				}
			}
		}
		
		void BtnFB2NotValidCopyToClick(object sender, EventArgs e)
		{
			// задание папки для копирования невалидных fb2-файлов
			filesWorker.OpenDirDlg( tboxFB2NotValidDirCopyTo, fbdDir, "Укажите папку для не валидных fb2-файлов" );
		}
		
		void BtnFB2NotValidMoveToClick(object sender, EventArgs e)
		{
			// задание папки для перемещения невалидных fb2-файлов
			filesWorker.OpenDirDlg( tboxFB2NotValidDirMoveTo, fbdDir, "Укажите папку для не валидных fb2-файлов" );
		}
		
		void BtnFB2ValidCopyToClick(object sender, EventArgs e)
		{
			// задание папки для валидных fb2-файлов
			filesWorker.OpenDirDlg( tboxFB2ValidDirCopyTo, fbdDir, "Укажите папку для валидных fb2-файлов" );
		}
		
		void BtnFB2ValidMoveToClick(object sender, EventArgs e)
		{
			// задание папки для перемещения валидных fb2-файлов
			filesWorker.OpenDirDlg( tboxFB2ValidDirMoveTo, fbdDir, "Укажите папку для валидных fb2-файлов" );
		}
		
		void BtnNotFB2CopyToClick(object sender, EventArgs e)
		{
			// задание папки для не fb2-файлов
			filesWorker.OpenDirDlg( tboxNotFB2DirCopyTo, fbdDir, "Укажите папку для не fb2-файлов" );
		}
		
		void BtnNotFB2MoveToClick(object sender, EventArgs e)
		{
			// задание папки для перемещения не fb2-файлов
			filesWorker.OpenDirDlg( tboxNotFB2DirMoveTo, fbdDir, "Укажите папку для не fb2-файлов" );
		}
		
		void TsbtnOpenDirClick(object sender, EventArgs e)
		{
			// задание папки с fb2-файлами для сканирования
			filesWorker.OpenDirDlg( tboxSourceDir, fbdDir, "Укажите папку для проверки fb2-файлов" );
		}
		
		void TsmiReportAsHTMLClick(object sender, EventArgs e)
		{
			// отчет в виде html-файла
			MakeReport( 0 );
		}
		
		void TsmiReportAsFB2Click(object sender, EventArgs e)
		{
			// отчет в виде fb2-файла
			MakeReport( 1 );
		}
		
		void TsmiReportAsCSV_CSVClick(object sender, EventArgs e)
		{
			// отчет в виде cvs-файла
			MakeReport( 2 );
		}

		void TsmiEditInTextEditorClick(object sender, EventArgs e)
		{
			// редактировать выделенный файл в текстовом редакторе
			// читаем путь к текстовому редактору из настроек
			string sTFB2Path = Settings.Settings.ReadTextFB2EPath();
			string sTitle = "SharpFBTools - Открытие файла в текстовом редакторе";
			if( !File.Exists( sTFB2Path ) ) {
				MessageBox.Show( "Не могу найти текстовый редактор \""+sTFB2Path+"\"!\nПроверьте, правильно ли задан путь в Настройках.", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 && l.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = l.SelectedItems;
				string sFilePath = si[0].SubItems[0].Text.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: "+sFilePath+"\" не найден!", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				filesWorker.StartAsyncFile( sTFB2Path, sFilePath );
			}
		}
		
		void TsmiEditInFB2EditorClick(object sender, EventArgs e)
		{
			// редактировать выделенный файл в fb2-редакторе
			// читаем путь к FBE из настроек
			string sFBEPath = Settings.Settings.ReadFBEPath();
			string sTitle = "SharpFBTools - Открытие файла в fb2-редакторе";
			if( !File.Exists( sFBEPath ) ) {
				MessageBox.Show( "Не могу найти fb2-редактор \""+sFBEPath+"\"!\nПроверьте, правильно ли задан путь в Настройках.", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 && l.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = l.SelectedItems;
				string sFilePath = si[0].SubItems[0].Text.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: "+sFilePath+"\" не найден!", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				filesWorker.StartAsyncFile( sFBEPath, sFilePath );
			}
		}
		
		void TsmiVienInReaderClick(object sender, EventArgs e)
		{
			// запустить файл в fb2-читалке (Просмотр)
			// читаем путь к читалке из настроек
			string sFBReaderPath = Settings.Settings.ReadFBReaderPath();
			string sTitle = "SharpFBTools - Открытие папки для файла";
			if( !File.Exists( sFBReaderPath ) ) {
				MessageBox.Show( "Не могу найти Читалку \""+sFBReaderPath+"\"!\nПроверьте, правильно ли задан путь в Настройках.", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 && l.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = l.SelectedItems;
				string sFilePath = si[0].SubItems[0].Text.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: "+sFilePath+"\" не найден!", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				filesWorker.StartAsyncFile( sFBReaderPath, sFilePath );
			}
		}
		
		void TsmiOpenFileDirClick(object sender, EventArgs e)
		{
			// Открыть папку для выделенного файла
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 && l.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = l.SelectedItems;
				FileInfo fi = new FileInfo( si[0].SubItems[0].Text.Split('/')[0] );
				string sDir = fi.Directory.ToString();
				if( !Directory.Exists( sDir ) ) {
					MessageBox.Show( "Папка: "+sDir+"\" не найдена!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				filesWorker.ShowAsyncDir( sDir );
			}
		}
		
		void TsmiOpenFileInArchivatorClick(object sender, EventArgs e)
		{
			// Запустить выделенный файл в архиваторе
			// читаем путь к архиватору из настроек
			string sWinRarPath = Settings.Settings.ReadWinRARPath();
			string sTitle = "SharpFBTools - Запуск файла в Архиваторе";
			if( !File.Exists( sWinRarPath ) ) {
				MessageBox.Show( "Не могу найти WinRAR \""+sWinRarPath+"\"!\nПроверьте, правильно ли задан путь в Настройках.", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 && l.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = l.SelectedItems;
				string sFilePath = si[0].SubItems[0].Text.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: "+sFilePath+"\" не найден!", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				filesWorker.StartAsyncFile( sWinRarPath, sFilePath );
			}
		}
		
		void TsmiFileReValidateClick(object sender, EventArgs e)
		{
			// Повторная Проверка выбранного fb2-файла или архива (Валидация)
			#region Код
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 && l.SelectedItems.Count != 0 ) {
				DateTime dtStart = DateTime.Now;
				string sTempDir = Settings.Settings.GetTempDir();
				ListView.SelectedListViewItemCollection si = l.SelectedItems;
				string sSelectedItemText = si[0].SubItems[0].Text;
				string sFilePath = sSelectedItemText.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: "+sFilePath+"\" не найден!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				MessageBoxIcon mbi = MessageBoxIcon.Information;
				string sExt = Path.GetExtension( sFilePath );
				string sMsg = "";
				string sErrorMsg = "СООБЩЕНИЕ ОБ ОШИБКЕ:";
				string sOkMsg = "ОШИБОК НЕТ - ФАЙЛ ВАЛИДЕН";
				string sMoveNotValToVal = "Путь к этому файлу перенесен из Списка не валидных fb2-файлов в Список валидных.";
				string sMoveValToNotVal = "Путь к этому файлу перенесен из Списка валидных fb2-файлов в Список не валидных.";
				FB2Validator fv2V = new FB2Validator();
				if( sExt.ToLower() == ".fb2" ) {
					// для несжатого fb2-файла
					sMsg = fv2V.ValidatingFB2File( sFilePath );
					if ( sMsg == "" ) {
           				// файл валидный
           				mbi = MessageBoxIcon.Information;
           				if( l.Name == "listViewNotValid" ) {
           					sErrorMsg = sOkMsg + ":";
           					sMsg = sMoveNotValToVal;
							listViewNotValid.Items[ l.SelectedItems[0].Index ].Remove();
							rеboxNotValid.Text = "";
							tpNotValid.Text = m_sNotValid + "( " + listViewNotValid.Items.Count.ToString() + " ) ";
							ListViewItem item = new ListViewItem( sSelectedItemText, 0 );
	   						item.ForeColor = m_FB2ValidFontColor;
							FileInfo fi = new FileInfo( sSelectedItemText );
   							item.SubItems.Add( filesWorker.FormatFileLength( fi.Length ) );
							listViewValid.Items.AddRange( new ListViewItem[]{ item } );
							tpValid.Text = m_sValid + "( " + listViewValid.Items.Count.ToString() + " ) ";
           				} else {
           					sErrorMsg = sOkMsg;
           				}
					} else {
						// файл не валидный
						mbi = MessageBoxIcon.Error;
						if( l.Name == "listViewNotValid" ) {
							l.Items[ l.SelectedItems[0].Index ].SubItems[1].Text = sMsg;
							rеboxNotValid.Text = sMsg;
						} else if( l.Name == "listViewValid" ) {
							// валидный файл был как-то "испорчен"
							listViewValid.Items[ l.SelectedItems[0].Index ].Remove();
							tpValid.Text = m_sValid + "( " + listViewValid.Items.Count.ToString() + " ) ";
							ListViewItem item = new ListViewItem( sSelectedItemText, 0 );
   							item.ForeColor = m_FB2NotValidFontColor;
   							item.SubItems.Add( sMsg );
							FileInfo fi = new FileInfo( sSelectedItemText );
			   				item.SubItems.Add( filesWorker.FormatFileLength( fi.Length ) );
							listViewNotValid.Items.AddRange( new ListViewItem[]{ item } );
							tpNotValid.Text = m_sNotValid + "( " + listViewNotValid.Items.Count.ToString() + " ) ";
							sMsg += "\n\n" + sMoveValToNotVal;
						}
					}
				} else if( sExt.ToLower() == ".zip" || sExt.ToLower() == ".rar" ) {
					// очистка временной папки
					filesWorker.RemoveDir( sTempDir );
//					Directory.CreateDirectory( sTempDir );
					if( sExt.ToLower() == ".zip" ) {
						archivesWorker.unzip( Settings.Settings.Read7zaPath(), sFilePath, sTempDir, ProcessPriorityClass.AboveNormal );
					} else if( sExt.ToLower() == ".rar" ) {
						archivesWorker.unrar( Settings.Settings.ReadUnRarPath(), sFilePath, sTempDir, ProcessPriorityClass.AboveNormal );
					}
					string [] files = Directory.GetFiles( sTempDir );
					if( files.Length > 0 ) {
						sMsg = fv2V.ValidatingFB2File( files[0] );
						if ( sMsg == "" ) {
							mbi = MessageBoxIcon.Information;
        			   		//  Запакованный fb2-файл валидный
        			   		if( l.Name == "listViewNotValid" ) {
        			   			sErrorMsg = sOkMsg + ":";
           						sMsg = sMoveNotValToVal;
								listViewNotValid.Items[ l.SelectedItems[0].Index ].Remove();
								rеboxNotValid.Text = "";
								tpNotValid.Text = m_sNotValid + "( " + listViewNotValid.Items.Count.ToString() + " ) ";
								string sFB2FileName = sSelectedItemText.Split('/')[1];
								ListViewItem item = new ListViewItem( sSelectedItemText , 0 );
								if( sExt.ToLower() == ".zip" ) {
									item.ForeColor = m_ZipFB2ValidFontColor;
								} else if( sExt.ToLower() == ".rar" ) {
									item.ForeColor = m_RarFB2ValidFontColor;
								}
								FileInfo fi = new FileInfo( sFilePath );
				   				string s = filesWorker.FormatFileLength( fi.Length );
				   				fi = new FileInfo( sTempDir+"\\"+sFB2FileName );
				   				s += " / "+filesWorker.FormatFileLength( fi.Length );
				   				item.SubItems.Add( s );
								listViewValid.Items.AddRange( new ListViewItem[]{ item } );
								tpValid.Text = m_sValid + "( " + listViewValid.Items.Count.ToString() + " ) ";
							} else {
           						sErrorMsg = sOkMsg;
           					}
           				} else {
							mbi = MessageBoxIcon.Error;
							// Запакованный fb2-файл невалиден
							if( l.Name == "listViewNotValid" ) {
								l.Items[ l.SelectedItems[0].Index ].SubItems[1].Text = sMsg;
								rеboxNotValid.Text = sMsg;
							} else if( l.Name == "listViewValid" ) {
								// валидный файл в архиве был как-то "испорчен"
								listViewValid.Items[ l.SelectedItems[0].Index ].Remove();
								tpValid.Text = m_sValid + "( " + listViewValid.Items.Count.ToString() + " ) ";
								string sFB2FileName = sSelectedItemText.Split('/')[1];
								ListViewItem item = new ListViewItem( sSelectedItemText , 0 );
								if( sExt.ToLower() == ".zip" ) {
									item.ForeColor = m_ZipFB2NotValidFontColor;
								} else if( sExt.ToLower() == ".rar" ) {
									item.ForeColor = m_RarFB2NotValidFontColor;
								}
								item.SubItems.Add( sMsg );
								FileInfo fi = new FileInfo( sFilePath );
				   				string s = filesWorker.FormatFileLength( fi.Length );
				   				fi = new FileInfo( sTempDir+"\\"+sFB2FileName );
				   				s += " / "+filesWorker.FormatFileLength( fi.Length );
				   				item.SubItems.Add( s );
								listViewNotValid.Items.AddRange( new ListViewItem[]{ item } );
								tpNotValid.Text = m_sNotValid + "( " + listViewNotValid.Items.Count.ToString() + " ) ";
								sMsg += "\n\n" + sMoveValToNotVal;
							}
						}
					}
				}
				DateTime dtEnd = DateTime.Now;
				string sTime = dtEnd.Subtract( dtStart ).ToString() + " (час.:мин.:сек.)";
				// очистка временной папки
				filesWorker.RemoveDir( sTempDir );
				MessageBox.Show( "Повторная проверка выделенного файла на соответствие FictionBook.xsd схеме завершена.\nЗатрачено времени: "+sTime+"\n\nФайл: \""+sFilePath+"\"\n\n"+sErrorMsg+"\n"+sMsg, "SharpFBTools - "+sErrorMsg, MessageBoxButtons.OK, mbi );
			}
			#endregion
		}
		
		void TsmiDeleteFileFromDiskClick(object sender, EventArgs e)
		{
			// удаление выделенного файла с диска
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 && l.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = l.SelectedItems;
				string sFilePath = si[0].SubItems[0].Text.Split('/')[0];
				string sTitle = "SharpFBTools - Удаление файла с диска";
				if( !File.Exists( sFilePath ) ) {
					if( MessageBox.Show( "Файл: "+sFilePath+"\" не найден!\nУдалить путь к этому файлу из списка?",
					                    sTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes ) {
						l.Items[ l.SelectedItems[0].Index ].Remove();
					}
				} else {
					if( MessageBox.Show( "Вы действительно хотите удалить файл: "+sFilePath+"\" с диска?", sTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes ) {
						File.Delete( sFilePath );
						l.Items[ l.SelectedItems[0].Index ].Remove();
					}
				}
				switch( tcResult.SelectedIndex ) {
					case 0:
						// не валидные fb2-файлы
						tpNotValid.Text = m_sNotValid + "( " + l.Items.Count.ToString() + " ) ";
						break;
					case 1:
						// валидные fb2-файлы
						tpValid.Text = m_sValid + "( " + l.Items.Count.ToString() + " ) ";
						break;
					case 2:
						// не fb2-файлы
						tpNotFB2Files.Text = m_sNotFB2Files + "( " + l.Items.Count.ToString() + " ) ";
						break;
				}
			}
		}
		
		void ListViewNotValidDoubleClick(object sender, EventArgs e)
		{
			// выбор варианта работы по двойному щелчку на списках
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 && l.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = l.SelectedItems;
				string sSelectedItemText = si[0].SubItems[0].Text;
				string sFilePath = sSelectedItemText.Split('/')[0];
				string sExt = Path.GetExtension( sFilePath );
				if( sExt.ToLower() == ".fb2" ) {
					switch ( Settings.SettingsValidator.ReadValidatorFB2SelectedIndex() ) {
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
							TsmiVienInReaderClick( sender, e );
							break;
						case 4: // Открыть папку файла
							TsmiOpenFileDirClick( sender, e );
							break;
					}
				} else if( sExt.ToLower() == ".zip" || sExt.ToLower() == ".rar" ) {
					switch ( Settings.SettingsValidator.ReadValidatorFB2ArchiveSelectedIndex() ) {
						case 0: // Повторная Валидация
							TsmiFileReValidateClick( sender, e );
							break;
						case 1: // Запустить в Архиваторе
							TsmiOpenFileInArchivatorClick( sender, e );
							break;
						case 2: // Открыть папку файла
							TsmiOpenFileDirClick( sender, e );
							break;
					}
				}
			}
		}
		
		void ListViewNotValidKeyPress(object sender, KeyPressEventArgs e)
		{
			// выбор варианта работы по нажатию Enter на списках
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 && l.SelectedItems.Count != 0 ) {
				if ( e.KeyChar == (char)Keys.Return ) {
            		e.Handled = true;
            		ListView.SelectedListViewItemCollection si = l.SelectedItems;
					string sSelectedItemText = si[0].SubItems[0].Text;
					string sFilePath = sSelectedItemText.Split('/')[0];
					string sExt = Path.GetExtension( sFilePath );
					if( sExt.ToLower() == ".fb2" ) {
						switch ( Settings.SettingsValidator.ReadValidatorFB2SelectedIndexPE() ) {
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
								TsmiVienInReaderClick( sender, e );
								break;
							case 4: // Открыть папку файла
								TsmiOpenFileDirClick( sender, e );
								break;
						}
					} else if( sExt.ToLower() == ".zip" || sExt.ToLower() == ".rar" ) {
						switch ( Settings.SettingsValidator.ReadValidatorFB2ArchiveSelectedIndexPE() ) {
							case 0: // Повторная Валидация
								TsmiFileReValidateClick( sender, e );
								break;
							case 1: // Запустить в Архиваторе
								TsmiOpenFileInArchivatorClick( sender, e );
								break;
							case 2: // Открыть папку файла
								TsmiOpenFileDirClick( sender, e );
								break;
						}
					}
        		}
			}
		}

		void TsmiMakeNotValidFileListClick(object sender, EventArgs e)
		{
			// сохранение списка Не валидных файлов
			ValidatorReports.SaveFilesList( listViewNotValid, sfdReport, m_TXTFilter,
			              ssProgress,  tsslblProgress, tsProgressBar, m_FB2NotValidFilesListReport,
			              "Нет ни одного Не валидного файла!", "Создание списка Не валидных файлов завершено.", Settings.Settings.GetReady() );
		}
		
		void TsmiMakeValidFileListClick(object sender, EventArgs e)
		{
			// сохранение списка Валидных файлов
			ValidatorReports.SaveFilesList( listViewValid, sfdReport, m_TXTFilter, ssProgress,
			              tsslblProgress, tsProgressBar, m_FB2ValidFilesListReport,
			              "Нет ни одного Валидного файла!", "Создание списка Валидных файлов завершено.", Settings.Settings.GetReady() );
		}
		
		void CboxExistFileSelectedIndexChanged(object sender, EventArgs e)
		{
			chBoxAddBookID.Enabled = ( cboxExistFile.SelectedIndex != 0 );
		}	
		
		void TboxSourceDirTextChanged(object sender, EventArgs e)
		{
			Settings.SettingsValidator.ScanDir = tboxSourceDir.Text;
		}
		
		void TboxFB2NotValidDirCopyToTextChanged(object sender, EventArgs e)
		{
			Settings.SettingsValidator.FB2NotValidDirCopyTo = tboxFB2NotValidDirCopyTo.Text;
		}
		
		void TboxFB2NotValidDirMoveToTextChanged(object sender, EventArgs e)
		{
			Settings.SettingsValidator.FB2NotValidDirMoveTo = tboxFB2NotValidDirMoveTo.Text;
		}
		
		void TboxFB2ValidDirCopyToTextChanged(object sender, EventArgs e)
		{
			Settings.SettingsValidator.FB2ValidDirCopyTo = tboxFB2ValidDirCopyTo.Text;
		}
		
		void TboxFB2ValidDirMoveToTextChanged(object sender, EventArgs e)
		{
			Settings.SettingsValidator.FB2ValidDirMoveTo = tboxFB2ValidDirMoveTo.Text;
		}
		
		void TboxNotFB2DirCopyToTextChanged(object sender, EventArgs e)
		{
			Settings.SettingsValidator.NotFB2DirCopyTo = tboxNotFB2DirCopyTo.Text;
		}
		
		void TboxNotFB2DirMoveToTextChanged(object sender, EventArgs e)
		{
			Settings.SettingsValidator.NotFB2DirMoveTo = tboxNotFB2DirMoveTo.Text;
		}
		
		void TSBValidateStopClick(object sender, EventArgs e)
		{
			// Остановка выполнения процесса Валидации
			if( m_bwv.WorkerSupportsCancellation == true ) {
				m_bwv.CancelAsync();
			}
		}
		
		void TsbtnFilesWorkStopClick(object sender, EventArgs e)
		{
			// Остановка выполнения процесса Копирование / Перемещение / Удаление
			if( m_bwcmd.WorkerSupportsCancellation == true ) {
				m_bwcmd.CancelAsync();
			}
		}
		
		void TboxSourceDirKeyPress(object sender, KeyPressEventArgs e)
		{
			// запуск Валидации по нажатию Enter на поле ввода папки для сканирования
			if ( e.KeyChar == (char)Keys.Return ) {
				TSBValidateClick( sender, e );
			}
		}
		#endregion
	}
}
