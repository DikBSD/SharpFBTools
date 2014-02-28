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
using Core.Misc;
using Core.FilesWorker;
using Core.FB2.FB2Parsers;
using Core.FB2.Description.DocumentInfo;

using ICSharpCode.SharpZipLib.Zip;

using filesWorker		= Core.FilesWorker.FilesWorker;
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
		private DateTime m_dtStart;
		private BackgroundWorker m_bwv		= null;
		private BackgroundWorker m_bwcmd	= null;
		private string	m_sMessTitle		= "";
		private string	m_sScan				= "";
		private string	m_sFileWorkerMode	= "";
		private bool	m_bScanSubDirs		= true;
		private MiscListView m_mscLV		= new MiscListView(); // класс по работе с ListView
		private bool	m_bFilesWorked		= false; // флаг = true, если хоть один файл был на диске и был обработан (copy, move или delete)
		
		private Core.FilesWorker.SharpZipLibWorker sharpZipLib = new Core.FilesWorker.SharpZipLibWorker();
		#endregion
		
		public SFBTpFB2Validator()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			// задание для кнопок ToolStrip стиля и положения текста и картинки
			SetToolButtonsSettings();
			
			InitializeValidateBackgroundWorker();
			InitializeFilesWorkerBackgroundWorker();
			
			#region первоначальная установка значений по умолчанию
			this.cboxExistFile.SelectedIndexChanged -= new System.EventHandler(this.CboxExistFileSelectedIndexChanged);
			cboxExistFile.SelectedIndex = 1;
			this.cboxExistFile.SelectedIndexChanged += new System.EventHandler(this.CboxExistFileSelectedIndexChanged);

			this.cboxValidatorForFB2.SelectedIndexChanged -= new System.EventHandler(this.CboxValidatorForFB2SelectedIndexChanged);
			cboxValidatorForFB2.SelectedIndex = 1;
			this.cboxValidatorForFB2.SelectedIndexChanged += new System.EventHandler(this.CboxValidatorForFB2SelectedIndexChanged);
			
			this.cboxValidatorForZip.SelectedIndexChanged -= new System.EventHandler(this.CboxValidatorForZipSelectedIndexChanged);
			cboxValidatorForZip.SelectedIndex = 1;
			this.cboxValidatorForZip.SelectedIndexChanged += new System.EventHandler(this.CboxValidatorForZipSelectedIndexChanged);
			
			this.cboxValidatorForFB2PE.SelectedIndexChanged -= new System.EventHandler(this.CboxValidatorForFB2PESelectedIndexChanged);
			cboxValidatorForFB2PE.SelectedIndex = 0;
			this.cboxValidatorForFB2PE.SelectedIndexChanged += new System.EventHandler(this.CboxValidatorForFB2PESelectedIndexChanged);
			
			this.cboxValidatorForZipPE.SelectedIndexChanged -= new System.EventHandler(this.CboxValidatorForZipPESelectedIndexChanged);
			cboxValidatorForZipPE.SelectedIndex = 0;
			this.cboxValidatorForZipPE.SelectedIndexChanged += new System.EventHandler(this.CboxValidatorForZipPESelectedIndexChanged);
			#endregion
			
			// инициализация контролов
			Init();
			// чтение настроек Валидатора
			ReadValidatorSettings();
			
		}
		
		#region Закрытые данные класса
		// Color
		private Color	m_FB2ValidFontColor			= Color.Black;		// цвет для несжатых валидных fb2
		private Color	m_FB2NotValidFontColor		= Color.Black;		// цвет для несжатых не валидных fb2
		private Color	m_ZipFB2ValidFontColor		= Color.Green;		// цвет для валидных fb2 в zip
		private Color	m_ZipFB2NotValidFontColor	= Color.Blue;		// цвет для не валидных fb2 в zip
		private Color	m_ZipFontColor				= Color.BlueViolet;	// цвет для zip не fb2
		private Color	m_NotFB2FontColor			= Color.Black;		// цвет для всех остальных файлов
		// найденные файлы
		private int	m_nFB2Valid		= 0; // число валидных файлов
		private int	m_nFB2NotValid	= 0; // число не валидных файлов
		private int	m_nFB2Files		= 0; // число fb2 файлов (не сжатых)
		private int	m_nFB2ZipFiles	= 0; // число fb2.zip файлов
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
		private string	m_TXTFilter 		= "TXT файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
		#endregion
		
		#region Закрытые методы
		
		#region Закрытые общие вспомогательные методы
		// отключаем обработчики событий для Списков (убираем "тормоза")
		private void ConnectListsEventHandlers( bool bConnect ) {
			if( !bConnect ) {
				// для listViewNotValid
				listViewNotValid.BeginUpdate();
				this.listViewNotValid.SelectedIndexChanged -= new System.EventHandler(this.ListViewNotValidSelectedIndexChanged);
				this.listViewNotValid.DoubleClick -= new System.EventHandler(this.ListViewNotValidDoubleClick);
				this.listViewNotValid.KeyPress -= new System.Windows.Forms.KeyPressEventHandler(this.ListViewNotValidKeyPress);
				// для listViewValid
				listViewValid.BeginUpdate();
				this.listViewValid.SelectedIndexChanged -= new System.EventHandler(this.ListViewValidSelectedIndexChanged);
				this.listViewValid.DoubleClick -= new System.EventHandler(this.ListViewNotValidDoubleClick);
				this.listViewValid.KeyPress -= new System.Windows.Forms.KeyPressEventHandler(this.ListViewNotValidKeyPress);
				// для listViewNotFB2
				listViewNotFB2.BeginUpdate();
				this.tsmiOpenFileDir.Click -= new System.EventHandler(this.TsmiOpenFileDirClick);
				
			} else {
				// подключаем обработчики событий для Списков
				// для istViewNotValid
				listViewNotValid.EndUpdate();
				this.listViewNotValid.SelectedIndexChanged += new System.EventHandler(this.ListViewNotValidSelectedIndexChanged);
				this.listViewNotValid.DoubleClick += new System.EventHandler(this.ListViewNotValidDoubleClick);
				this.listViewNotValid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ListViewNotValidKeyPress);
				// для listViewValid
				listViewValid.EndUpdate();
				this.listViewValid.SelectedIndexChanged += new System.EventHandler(this.ListViewValidSelectedIndexChanged);
				this.listViewValid.DoubleClick += new System.EventHandler(this.ListViewNotValidDoubleClick);
				this.listViewValid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ListViewNotValidKeyPress);
				// для listViewNotFB2
				listViewNotFB2.EndUpdate();
				this.tsmiOpenFileDir.Click += new System.EventHandler(this.TsmiOpenFileDirClick);
			}
		}
		
		// Смена схемы Жанров
		private void GenresSchemeChange()
		{
			Settings.ValidatorSettings.FB2LibrusecGenres	= rbtnFB2Librusec.Checked;
			Settings.ValidatorSettings.FB22Genres			= rbtnFB22.Checked;
			Settings.ValidatorSettings.WriteValidatorSettings();
		}
		#endregion
		
		#region Закрытые методы реализации BackgroundWorker Валидатора
		// Инициализация перед использование BackgroundWorker Валидации
		private void InitializeValidateBackgroundWorker() {
			m_bwv = new BackgroundWorker();
			m_bwv.WorkerReportsProgress			= true; // Позволить выводить прогресс процесса
			m_bwv.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
			m_bwv.DoWork 				+= new DoWorkEventHandler( bwv_DoWork );
			m_bwv.ProgressChanged 		+= new ProgressChangedEventHandler( bwv_ProgressChanged );
			m_bwv.RunWorkerCompleted 	+= new RunWorkerCompletedEventHandler( bwv_RunWorkerCompleted );
		}
		
		// Валидация
		private void bwv_DoWork( object sender, DoWorkEventArgs e ) {
			int nAllFiles = 0;
			List<string> lDirList = new List<string>();
			if( !m_bScanSubDirs ) {
				// сканировать только указанную папку
				lDirList.Add( m_sScan );
				lvFilesCount.Items[0].SubItems[1].Text = "1";
			} else {
				// сканировать и все подпапки
				nAllFiles = filesWorker.DirsParser( m_bwv, e, m_sScan, ref lvFilesCount, ref lDirList, false );
			}
			
			// отобразим число всех файлов в папке сканирования
			lvFilesCount.Items[1].SubItems[1].Text = nAllFiles.ToString();
			
			// проверка остановки процесса
			if( ( m_bwv.CancellationPending == true ) )  {
				e.Cancel = true; // Выставить окончание - по отмене, сработает событие bwv_RunWorkerCompleted
				return;
			}
			
			// проверка, есть ли хоть один файл в папке для сканирования
			if( nAllFiles == 0 ) {
				MessageBox.Show( "В указанной папке не найдено ни одного файла!", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				Init();
				return;
			}
			
			// проверка файлов
			tsslblProgress.Text		= "Проверка найденных файлов на валидность:";
			tsProgressBar.Maximum	= nAllFiles;
			tsProgressBar.Value		= 0;

			FB2Validator fv2Validator = new FB2Validator();
			string sTempDir = Settings.Settings.GetTempDir();
			ConnectListsEventHandlers( false );
			string sExt = "", sFile = "";
			foreach( string s in lDirList ) {
				DirectoryInfo diFolder = new DirectoryInfo( s );
				foreach( FileInfo fiNextFile in diFolder.GetFiles() ) {
					// Проверить флаг на остановку процесса
					if( ( m_bwv.CancellationPending == true ) ) {
						e.Cancel = true; // Выставить окончание - по отмене, сработает событие bwv_RunWorkerCompleted
						return;
					}
					sFile	= s + "\\" + fiNextFile.Name;
					sExt	= Path.GetExtension( sFile ).ToLower();
					if( sExt == ".fb2" ) {
						++m_nFB2Files;
						ParseFB2File( sFile, fv2Validator );
					} else if( sExt == ".zip" ) {
						// очистка временной папки
						filesWorker.RemoveDir( sTempDir );
						Directory.CreateDirectory( sTempDir );
						ParseArchiveFile( sFile, fv2Validator, sTempDir );
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
			lDirList.Clear();
		}
		
		// Отобразим результат Валидации
		private void bwv_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
			if( chBoxViewProgress.Checked )
				ValidProgressData();
			++tsProgressBar.Value;
		}
		
		// Проверяем это отмена, ошибка, или конец задачи и сообщить
		private void bwv_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {
			ValidProgressData(); // Отобразим результат Валидации
			ConnectListsEventHandlers( true );
			
			DateTime dtEnd = DateTime.Now;
			filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
			
			// авторазмер колонок списков
			Core.FileManager.FileManagerWork.AutoResizeColumns(listViewNotValid);
			Core.FileManager.FileManagerWork.AutoResizeColumns(listViewValid);
			Core.FileManager.FileManagerWork.AutoResizeColumns(listViewNotFB2);
			
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
			
			// выделяем 1-ю найденную книгу
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 ) {
				l.Focus();
				l.Items[0].Focused	= true;
				l.Items[0].Selected	= true;
			}
		}
		#endregion
		
		#region Закрытые методы реализации BackgroundWorker Копирование / Перемещение / Удаление
		// Инициализация перед использование BackgroundWorker Копирование / Перемещение / Удаление
		private void InitializeFilesWorkerBackgroundWorker() {
			m_bwcmd = new BackgroundWorker();
			m_bwcmd.WorkerReportsProgress		= true; // Позволить выводить прогресс процесса
			m_bwcmd.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
			m_bwcmd.DoWork				+= new DoWorkEventHandler( bwcmd_DoWork );
			m_bwcmd.ProgressChanged 	+= new ProgressChangedEventHandler( bwcmd_ProgressChanged );
			m_bwcmd.RunWorkerCompleted	+= new RunWorkerCompletedEventHandler( bwcmd_RunWorkerCompleted );
		}
		
		// Обработка файлов
		private void bwcmd_DoWork( object sender, DoWorkEventArgs e ) {
			ConnectListsEventHandlers( false );
			m_bFilesWorked = false;
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
		
		// Отобразим результат Копирования / Перемещения / Удаления
		private void bwcmd_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
			++tsProgressBar.Value;
		}
		
		// Завершение работы Обработчика Файлов
		private void bwcmd_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {
			ConnectListsEventHandlers( true );
			
			string sMessCanceled, sMessError, sMessDone, sTabPageDefText, sMessTitle;
			sMessCanceled = sMessError = sMessDone = sTabPageDefText = sMessTitle = "";
			ListView	lv	= null;
			TabPage		tp	= null;
			switch( tcResult.SelectedIndex ) {
				case 0:
					// не валидные fb2-файлы
					sTabPageDefText = m_sNotValid;
					lv = listViewNotValid;
					tp = tpNotValid;
					break;
				case 1:
					// валидные fb2-файлы
					sTabPageDefText = m_sValid;
					lv = listViewValid;
					tp = tpValid;
					break;
				case 2:
					// не fb2-файлы
					sTabPageDefText = m_sNotFB2Files;
					lv = listViewNotFB2;
					tp = tpNotFB2Files;
					break;
			}
			
			switch( m_sFileWorkerMode ) {
				case "Copy":
					sMessTitle		= "SharpFBTools - Копирование помеченных файлов";
					sMessDone 		= "Копирование файлов в указанную папку завершено!";
					sMessCanceled	= "Копирование файлов в указанную папку остановлено!";
					break;
				case "Move":
					sMessTitle		= "SharpFBTools - Перемещение помеченных файлов";
					sMessDone 		= "Перемещение файлов в указанную папку завершено!";
					sMessCanceled	= "Перемещение файлов в указанную папку остановлено!";
					break;
				case "Delete":
					sMessTitle		= "SharpFBTools - Удаление помеченных файлов";
					sMessDone 		= "Удаление файлов из папки-источника завершено!";
					sMessCanceled	= "Удаление файлов из папки-источника остановлено!";
					break;
			}
			if( !m_bFilesWorked ) {
				string s = "На диске не найдено ни одного файла из помеченных!\n";
				switch( m_sFileWorkerMode ) {
					case "Copy":
						sMessDone = s + "Копирование файлов в указанную папку не произведено!";
						break;
					case "Move":
						sMessDone = s + "Перемещение файлов в указанную папку не произведено!";
						break;
					case "Delete":
						sMessDone = s + "Удаление файлов из папки-источника не произведено!";
						break;
				}
			}
			
			tp.Text = sTabPageDefText + "( " + lv.Items.Count.ToString() +" )";
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
		// чтение настроек Валидатора
		private void ReadValidatorSettings() {
			// чтение путей к папкам Валидатора из xml-файла
			tboxSourceDir.Text = Settings.ValidatorSettings.ReadXmlSourceDir();
			tboxFB2NotValidDirCopyTo.Text = Settings.ValidatorSettings.ReadXmlFB2NotValidDirCopyTo();
			tboxFB2NotValidDirMoveTo.Text = Settings.ValidatorSettings.ReadXmlFB2NotValidDirMoveTo();
			tboxFB2ValidDirCopyTo.Text = Settings.ValidatorSettings.ReadXmlFB2ValidDirCopyTo();
			tboxFB2ValidDirMoveTo.Text = Settings.ValidatorSettings.ReadXmlFB2ValidDirMoveTo();
			tboxNotFB2DirCopyTo.Text = Settings.ValidatorSettings.ReadXmlNotFB2DirCopyTo();
			tboxNotFB2DirMoveTo.Text = Settings.ValidatorSettings.ReadXmlNotFB2DirMoveTo();
			
			// обработка подкаталогов
			chBoxScanSubDir.Checked = Settings.ValidatorSettings.ReadXmlScanSubDir();
			// что делать, если такой файл уже есть
			cboxExistFile.SelectedIndex = Settings.ValidatorSettings.ReadXmlExistFileWorker();
			// чтение схемы жанров
			rbtnFB2Librusec.Checked = Settings.ValidatorSettings.ReadXmlFB2Librusec();
			rbtnFB22.Checked = Settings.ValidatorSettings.ReadXmlFB22();
			
			// настройки действий мышки и Enter
			cboxValidatorForFB2.SelectedIndex	= Settings.ValidatorSettings.ReadXmlMouseDoubleClickForFB2Mode();
			cboxValidatorForZip.SelectedIndex	= Settings.ValidatorSettings.ReadXmlMouseDoubleClickForZipMode();
			cboxValidatorForFB2PE.SelectedIndex	= Settings.ValidatorSettings.ReadXmlEnterPressForFB2Mode();
			cboxValidatorForZipPE.SelectedIndex	= Settings.ValidatorSettings.ReadXmlEnterPressForZipMode();
			
			// путь к GUI Архиватора
			tboxArchivatorPath.Text	= Settings.ValidatorSettings.ReadXmlArchivatorPath();
		}
		
		// Отобразим результат Валидации
		private void ValidProgressData() {
			lvFilesCount.Items[2].SubItems[1].Text = m_nFB2Files.ToString();
			lvFilesCount.Items[3].SubItems[1].Text = m_nFB2ZipFiles.ToString();
			lvFilesCount.Items[4].SubItems[1].Text = m_nNotFB2Files.ToString();
			
			tpNotFB2Files.Text	= m_sNotFB2Files + "( " + m_nNotFB2Files.ToString() + " ) " ;
			tpValid.Text		= m_sValid + "( " + m_nFB2Valid.ToString() + " ) " ;
			tpNotValid.Text		= m_sNotValid + "( " + m_nFB2NotValid.ToString() + " ) " ;
		}
		
		// инициализация контролов и переменных
		private void Init() {
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
			m_nFB2Valid	= m_nFB2NotValid = m_nFB2Files = m_nFB2ZipFiles = m_nNotFB2Files = 0;
			// очистка временной папки
			filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
		}
		
		// доступность контролов при Валидации
		private void SetValidingStartEnabled( bool bEnabled ) {
			tcResult.Enabled = tsbtnValidate.Enabled = tsbtnCopyFilesTo.Enabled = tsbtnMoveFilesTo.Enabled =
				tsbtnDeleteFiles.Enabled = tsddbtnMakeFileList.Enabled = tsddbtnMakeReport.Enabled = pScanDir.Enabled =
				gboxCopyMoveOptions.Enabled = pGenres.Enabled = pViewError.Enabled = bEnabled;
			tsbtnValidateStop.Enabled = tsProgressBar.Visible = !bEnabled;
			tcResult.Refresh();
			ssProgress.Refresh();
		}
		
		// доступность контролов при Обработке файлов
		private void SetFilesWorkerStartEnabled( bool bEnabled ) {
			tcResult.Enabled = tsbtnValidate.Enabled = tsbtnCopyFilesTo.Enabled = tsbtnMoveFilesTo.Enabled = tsbtnDeleteFiles.Enabled =
				tsddbtnMakeFileList.Enabled = tsddbtnMakeReport.Enabled = pScanDir.Enabled =
				gboxCopyMoveOptions.Enabled = pGenres.Enabled = pViewError.Enabled = bEnabled;
			tsbtnFilesWorkStop.Enabled	= tsProgressBar.Visible = !bEnabled;
			tcResult.Refresh();
			ssProgress.Refresh();
		}
		
		// возвращает текущий ListView в зависимости от выбранной вкладки
		private ListView GetCurrentListWiew()
		{
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
		
		// отметить все итемы ListView
		private void CheckAll() {
			ListView l = GetCurrentListWiew();
			m_mscLV.CheckAllListViewItems( l, true );
		}
		
		// снять отметки со всех итемов ListView
		private void UnCheckAll() {
			ListView l = GetCurrentListWiew();
			m_mscLV.UnCheckAllListViewItems( l.CheckedItems );
		}
		#endregion
		
		#region Закрытые Парсеры файлов и архивов
		// парсер несжатого fb2-файла
		private void ParseFB2File( string sFile, FB2Validator fv2Validator ) {
			string sMsg = rbtnFB2Librusec.Checked ? fv2Validator.ValidatingFB2LibrusecFile( sFile ) : fv2Validator.ValidatingFB22File( sFile );
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
		
		// парсер архива
		private void ParseArchiveFile( string sArchiveFile, FB2Validator fv2Validator, string sTempDir ) {
			string sExt = Path.GetExtension( sArchiveFile ).ToLower();
			sharpZipLib.UnZipFiles(sArchiveFile, sTempDir, 0, false, null, 4096);

			string [] files = Directory.GetFiles( sTempDir );
			if( files.Length <= 0 ) return;
			
			string sMsg = string.Empty;
			string sFileName = string.Empty;
			foreach (string file in files) {
				// валидация
				sMsg = rbtnFB2Librusec.Checked ? fv2Validator.ValidatingFB2LibrusecFile( file ) : fv2Validator.ValidatingFB22File( file );
				sFileName = Path.GetFileName( file );
				if ( sMsg == "" ) {
					// файл валидный - это fb2
					++m_nFB2Valid;
					++m_nFB2ZipFiles;
					//listViewValid.Items.Add( sArchiveFile + "/" + sFileName );
					ListViewItem item = new ListViewItem( sArchiveFile + "/" + sFileName, 0 );
					item.ForeColor = m_ZipFB2ValidFontColor;
					FileInfo fi = new FileInfo( sArchiveFile );
					string s = filesWorker.FormatFileLength( fi.Length );
					fi = new FileInfo( sTempDir+"\\"+sFileName );
					s += " / "+filesWorker.FormatFileLength( fi.Length );
					item.SubItems.Add( s );
					listViewValid.Items.AddRange( new ListViewItem[]{ item } );
				} else {
					// файл в архиве не валидный - посмотрим, что это
					sExt = Path.GetExtension( sFileName ).ToLower();
					if( sExt != ".fb2" ) {
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
				}
			}
			m_bwv.ReportProgress( 0 ); // отобразим данные в контролах
		}
		#endregion
		
		#region Закрытая Реализация Copy/Move/Delete помеченных файлов
		// копировать или переместить файлы в...
		private void CopyOrMoveFilesTo( BackgroundWorker bw, DoWorkEventArgs e,
		                               bool IsCopy, string SourceDir, string TargetDir,
		                               ListView lv, TabPage tp,
		                               string ProgressText, string TabPageDefText ) {
			#region Код
			System.Windows.Forms.ListView.CheckedListViewItemCollection checkedItems = lv.CheckedItems;
			tsslblProgress.Text = ProgressText;
			int i=0;
			foreach( ListViewItem lvi in checkedItems ) {
				// Проверить флаг на остановку процесса
				if( ( bw.CancellationPending == true ) ) {
					e.Cancel = true; // Выставить окончание - по отмене, сработает событие bwcmd_RunWorkerCompleted
					break;
				} else {
					string FilePath = lvi.Text.Split('/')[0];
					// есть ли такая книга на диске? Если нет - то смотрим следующую
					if( !File.Exists( FilePath ) ) {
						bw.ReportProgress( ++i ); // отобразим данные в контролах
						break;
					}
					
					if (lvi.Text.IndexOf('/') != -1) {
						// обработка файлов из архивов
						string FileFromZip = lvi.Text.Split('/')[1];
						if( File.Exists( FilePath ) ) {
							// Copy
							sharpZipLib.UnZipSelectedFile( FilePath, FileFromZip,
							                               filesWorker.buildTargetDir(FilePath, SourceDir, TargetDir),
							                               cboxExistFile.SelectedIndex, null, 4096 );
							if( !IsCopy ) {
								// Move
								if (sharpZipLib.DeleteSelectedFile( FilePath, FileFromZip, null ) ) {
									lv.Items.Remove( lvi );
									tp.Text = TabPageDefText + "( " + lv.Items.Count.ToString() +" )";
								}
							}
							m_bFilesWorked |= true;
						}
					} else {
						// обработка НЕ zip (fb2-файлы и другое)
						string NewPath = TargetDir + FilePath.Remove( 0, SourceDir.Length );
						FileInfo fi = new FileInfo( NewPath );
						if( !fi.Directory.Exists )
							Directory.CreateDirectory( fi.Directory.ToString() );

						if( File.Exists( NewPath ) ) {
							if( cboxExistFile.SelectedIndex==0 )
								File.Delete( NewPath );
							else
								NewPath = filesWorker.createFilePathWithSufix(NewPath, cboxExistFile.SelectedIndex);
						}
						
//						Regex rx = new Regex( @"\\+" );
//						FilePath = rx.Replace( FilePath, "\\" );
						if( File.Exists( FilePath ) ) {
							if( IsCopy )
								File.Copy( FilePath, NewPath );
							else {
								File.Move( FilePath, NewPath );
								lv.Items.Remove( lvi );
								tp.Text = TabPageDefText + "( " + lv.Items.Count.ToString() +" )";
							}
							m_bFilesWorked |= true;
						}
					}
					bw.ReportProgress( ++i ); // отобразим данные в контролах
				}
			}
			#endregion
		}
		
		// удалить файлы...
		private void DeleteFiles( BackgroundWorker bw, DoWorkEventArgs e,
		                         ListView lv, TabPage tp, string sProgressText, string sTabPageDefText ) {
			#region Код
			int i = 0;
			System.Windows.Forms.ListView.CheckedListViewItemCollection checkedItems = lv.CheckedItems;
			tsslblProgress.Text = sProgressText;
			string sFilePath = "";
			foreach( ListViewItem lvi in checkedItems ) {
				// Проверить флаг на остановку процесса
				if( ( bw.CancellationPending == true ) ) {
					e.Cancel = true; // Выставить окончание - по отмене, сработает событие bwcmd_RunWorkerCompleted
					break;
				} else {
					sFilePath = lvi.Text.Split('/')[0];
					if (lvi.Text.IndexOf('/') != -1) {
						// обработка файлов из архивов
						string fileFromZip = lvi.Text.Split('/')[1];
						if( File.Exists( sFilePath ) ) {
							if ( sharpZipLib.DeleteSelectedFile(sFilePath, fileFromZip, null) ) {
								lv.Items.Remove( lvi );
								tp.Text = sTabPageDefText + "( " + lv.Items.Count.ToString() +" )";
								m_bFilesWorked |= true;
							}
						}
					} else {
						// обработка НЕ zip (fb2-файлы и другое)
						if( File.Exists( sFilePath) ) {
							File.Delete( sFilePath );
							lv.Items.Remove( lvi );
							tp.Text = sTabPageDefText + "( " + lv.Items.Count.ToString() +" )";
							m_bFilesWorked |= true;
						}
					}
					bw.ReportProgress( ++i ); // отобразим данные в контролах
				}
			}
			#endregion
		}
		#endregion
		
		#region Закрытая Генерация отчетов
		// создание отчета заданного через nModeReport вида для разных вкладок (видов найденных файлов)
		// nModeReport: 0 - html; 1 - fb2;
		private void MakeReport( int nModeReport ) {
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
		
		// создание отчета
		private bool MakeReport( ListView lw, string sReportListEmpty, string sReportTitle, int nModeReport ) {
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
			}
			return true;
		}
		#endregion
		#endregion
		
		#region Открытые методы класса
		// задание для кнопок ToolStrip стиля и положения текста и картинки
		public void SetToolButtonsSettings() {
			Settings.ValidatorSettings.SetToolButtonsSettings( tsValidator );
		}
		#endregion
		
		#region Обработчики событий
		void ChBoxScanSubDirClick(object sender, EventArgs e)
		{
			Settings.ValidatorSettings.ScanSubDir = chBoxScanSubDir.Checked;
			Settings.ValidatorSettings.WriteValidatorSettings();
		}
		
		void RbtnFB2LibrusecClick(object sender, EventArgs e)
		{
			GenresSchemeChange();
		}
		
		void RbtnFB22Click(object sender, EventArgs e)
		{
			GenresSchemeChange();
		}
		
		// Ввлидация fb2-файлов в выбранной папке
		void TsbtnValidateClick(object sender, EventArgs e)
		{
			m_sMessTitle		= "SharpFBTools - Валидация";
			m_sScan				= filesWorker.WorkingDirPath( tboxSourceDir.Text.Trim() );
			tboxSourceDir.Text	= m_sScan;
			
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

			// инициализация контролов
			Init();
			SetValidingStartEnabled( false );
			
			m_dtStart = DateTime.Now;
			m_bScanSubDirs = chBoxScanSubDir.Checked;
			tsslblProgress.Text = "Создание списка файлов:";
			
			// Запуск процесса DoWork от Бекграунд Воркера
			if( m_bwv.IsBusy != true ) {
				// если не занят то запустить процесс
				m_bwv.RunWorkerAsync();
			}
		}

		// Остановка Валидации
		void TsbtnValidateStopClick(object sender, EventArgs e)
		{
			if( m_bwv.WorkerSupportsCancellation == true ) {
				m_bwv.CancelAsync();
			}
		}
		
		// копирование файлов в зависимости от выбранной вкладки
		void TsbtnCopyFilesToClick(object sender, EventArgs e)
		{
			m_sFileWorkerMode = "Copy";

			string sMessTitle = "SharpFBTools - Копирование помеченных файлов";
			string sSource 		= filesWorker.WorkingDirPath( tboxSourceDir.Text.Trim() );
			tboxSourceDir.Text	= sSource;
			
			string sTarget, sType;
			sTarget = sType = "";
			ListView lv = GetCurrentListWiew();
			switch( tcResult.SelectedIndex ) {
				case 0: // не валидные fb2-файлы
					sTarget = filesWorker.WorkingDirPath( tboxFB2NotValidDirCopyTo.Text.Trim() );
					tboxFB2NotValidDirCopyTo.Text = sTarget;
					sType	= "отмеченных не валидных fb2-файлов";
					break;
				case 1: // валидные fb2-файлы
					sTarget = filesWorker.WorkingDirPath( tboxFB2ValidDirCopyTo.Text.Trim() );
					tboxFB2ValidDirCopyTo.Text = sTarget;
					sType	= "отмеченных валидных fb2-файлов";
					break;
				case 2: // не fb2-файлы
					sTarget = filesWorker.WorkingDirPath( tboxNotFB2DirCopyTo.Text.Trim() );
					tboxNotFB2DirCopyTo.Text = sTarget;
					sType	= "отмеченных не fb2-файлов";
					break;
			}

			// проверки корректности путей к папкам
			if( lv.Items.Count == 0 ) {
				MessageBox.Show( "Список "+sType+" пуст!\nРабота прекращена.",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			if( lv.CheckedItems.Count == 0 ) {
				MessageBox.Show( "Нет "+sType+"!\nРабота прекращена.",
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
			// проверка папки-приемника и создание ее, если нужно
			if( !filesWorker.CreateDirIfNeed( sTarget, sMessTitle ) ) {
				return;
			}
			
			int nCount = lv.CheckedItems.Count;
			string sMess = "Вы действительно хотите скопировать "+nCount.ToString()+" "+sType+" в папку \""+sTarget+"\"?";
			MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			DialogResult result;
			result = MessageBox.Show( sMess, sMessTitle, buttons, MessageBoxIcon.Question );
			if(result == DialogResult.No) {
				tsslblProgress.Text = Settings.Settings.GetReady();
				SetFilesWorkerStartEnabled( true );
				return;
			}
			
			// инициализация контролов
			tsProgressBar.Maximum 	= lv.CheckedItems.Count;
			tsProgressBar.Value		= 0;
			SetFilesWorkerStartEnabled( false );
			
			// Запуск процесса DoWork от Бекграунд Воркера
			if( m_bwcmd.IsBusy != true ) {
				// если не занят то запустить процесс
				m_bwcmd.RunWorkerAsync();
			}
		}
		
		// перемещение файлов в зависимости от выбранной вкладки
		void TsbtnMoveFilesToClick(object sender, EventArgs e)
		{
			m_sFileWorkerMode = "Move";
			
			string sMessTitle = "SharpFBTools - Перемещение помеченных файлов";
			string sSource 		= filesWorker.WorkingDirPath( tboxSourceDir.Text.Trim() );
			tboxSourceDir.Text	= sSource;
			string sTarget, sType;
			sTarget = sType = "";
			ListView lv = GetCurrentListWiew();
			switch( tcResult.SelectedIndex ) {
				case 0: // не валидные fb2-файлы
					sTarget = filesWorker.WorkingDirPath( tboxFB2NotValidDirMoveTo.Text.Trim() );
					tboxFB2NotValidDirMoveTo.Text = sTarget;
					sType	= "отмеченных не валидных fb2-файлов";
					break;
				case 1: // валидные fb2-файлы
					sTarget = filesWorker.WorkingDirPath( tboxFB2ValidDirMoveTo.Text.Trim() );
					tboxFB2ValidDirMoveTo.Text = sTarget;
					sType	= "отмеченных валидных fb2-файлов";
					break;
				case 2: // не fb2-файлы
					sTarget = filesWorker.WorkingDirPath( tboxNotFB2DirMoveTo.Text.Trim() );
					tboxNotFB2DirMoveTo.Text = sTarget;
					sType	= "отмеченных не fb2-файлов";
					break;
			}

			// проверки корректности путей к папкам
			if( lv.Items.Count == 0 ) {
				MessageBox.Show( "Список "+sType+" пуст!\nРабота прекращена.",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			if( lv.CheckedItems.Count == 0 ) {
				MessageBox.Show( "Нет "+sType+"!\nРабота прекращена.",
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
			// проверка папки-приемника и создание ее, если нужно
			if( !filesWorker.CreateDirIfNeed( sTarget, sMessTitle ) ) {
				return;
			}
			
			int nCount = lv.CheckedItems.Count;
			string sMess = "Вы действительно хотите переместить "+nCount.ToString()+" "+sType+" в папку \""+sTarget+"\"?";
			MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			DialogResult result;
			result = MessageBox.Show( sMess, sMessTitle, buttons, MessageBoxIcon.Question );
			if( result == DialogResult.No ) {
				tsslblProgress.Text = Settings.Settings.GetReady();
				SetFilesWorkerStartEnabled( true );
				return;
			}
			
			// инициализация контролов
			tsProgressBar.Maximum 	= lv.CheckedItems.Count;
			tsProgressBar.Value		= 0;
			SetFilesWorkerStartEnabled( false );
			
			// Запуск процесса DoWork от Бекграунд Воркера
			if( m_bwcmd.IsBusy != true ) {
				// если не занят то запустить процесс
				m_bwcmd.RunWorkerAsync();
			}
		}
		
		// удаление файлов в зависимости от выбранной вкладки
		void TsbtnDeleteFilesClick(object sender, EventArgs e)
		{
			m_sFileWorkerMode = "Delete";
			
			string sMessTitle = "SharpFBTools - Удаление помеченных файлов";
			string sType = "";
			ListView lv = GetCurrentListWiew();
			switch( tcResult.SelectedIndex ) {
				case 0: // не валидные fb2-файлы
					sType	= "отмеченных не валидных fb2-файлов";
					break;
				case 1: // валидные fb2-файлы
					sType	= "отмеченных валидных fb2-файлов";
					break;
				case 2: // не fb2-файлы
					sType	= "отмеченных не fb2-файлов";
					break;
			}
			int nCount = lv.Items.Count;
			if( nCount == 0) {
				MessageBox.Show( "Список "+sType+" пуст!\nРабота прекращена.", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			if( lv.CheckedItems.Count == 0 ) {
				MessageBox.Show( "Нет "+sType+"!\nРабота прекращена.", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			
			nCount = lv.CheckedItems.Count;
			string sMess = "Вы действительно хотите удалить "+nCount.ToString()+" "+sType+"?";
			MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			DialogResult result;
			result = MessageBox.Show( sMess, sMessTitle, buttons, MessageBoxIcon.Question );
			if( result == DialogResult.No ) {
				return;
			}
			
			// инициализация контролов
			tsProgressBar.Maximum 	= lv.CheckedItems.Count;
			tsProgressBar.Value		= 0;
			SetFilesWorkerStartEnabled( false );
			
			// Запуск процесса DoWork от Бекграунд Воркера
			if( m_bwcmd.IsBusy != true ) {
				// если не занят то запустить процесс
				m_bwcmd.RunWorkerAsync();
			}
		}
		
		// занесение ошибки валидации в бокс
		void ListViewNotValidSelectedIndexChanged(object sender, EventArgs e)
		{
			ListView.SelectedListViewItemCollection si = listViewNotValid.SelectedItems;
			if( si.Count > 0 ) {
				rеboxNotValid.Text = si[0].SubItems[1].Text;
				if( listViewNotValid.Items.Count > 0 && listViewNotValid.SelectedItems.Count != 0 ) {
					// путь к выделенному файлу
					string s = si[0].SubItems[0].Text.Split('/')[0];
					// отределяем его расширение
					string sExt = Path.GetExtension( s ).ToLower();
					if( sExt == ".fb2" ) {
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
				string sExt = Path.GetExtension( s ).ToLower();
				if( sExt == ".fb2" ) {
					listViewValid.ContextMenuStrip = cmsFB2;
				} else {
					listViewValid.ContextMenuStrip = cmsArchive;
				}
			}
		}
		
		// задание папки для копирования невалидных fb2-файлов
		void BtnFB2NotValidCopyToClick(object sender, EventArgs e)
		{
			filesWorker.OpenDirDlg( tboxFB2NotValidDirCopyTo, fbdDir, "Укажите папку для не валидных fb2-файлов" );
		}
		
		// задание папки для перемещения невалидных fb2-файлов
		void BtnFB2NotValidMoveToClick(object sender, EventArgs e)
		{
			filesWorker.OpenDirDlg( tboxFB2NotValidDirMoveTo, fbdDir, "Укажите папку для не валидных fb2-файлов" );
		}
		
		// задание папки для валидных fb2-файлов
		void BtnFB2ValidCopyToClick(object sender, EventArgs e)
		{
			filesWorker.OpenDirDlg( tboxFB2ValidDirCopyTo, fbdDir, "Укажите папку для валидных fb2-файлов" );
		}
		
		// задание папки для перемещения валидных fb2-файлов
		void BtnFB2ValidMoveToClick(object sender, EventArgs e)
		{
			filesWorker.OpenDirDlg( tboxFB2ValidDirMoveTo, fbdDir, "Укажите папку для валидных fb2-файлов" );
		}
		
		// задание папки для не fb2-файлов
		void BtnNotFB2CopyToClick(object sender, EventArgs e)
		{
			filesWorker.OpenDirDlg( tboxNotFB2DirCopyTo, fbdDir, "Укажите папку для не fb2-файлов" );
		}
		
		// задание папки для перемещения не fb2-файлов
		void BtnNotFB2MoveToClick(object sender, EventArgs e)
		{
			filesWorker.OpenDirDlg( tboxNotFB2DirMoveTo, fbdDir, "Укажите папку для не fb2-файлов" );
		}
		
		// задание папки с fb2-файлами для сканирования
		void ButtonOpenDirClick(object sender, EventArgs e)
		{
			filesWorker.OpenDirDlg( tboxSourceDir, fbdDir, "Укажите папку для проверки fb2-файлов" );
		}
		
		// отчет в виде html-файла
		void TsmiReportAsHTMLClick(object sender, EventArgs e)
		{
			MakeReport( 0 );
		}
		
		// отчет в виде fb2-файла
		void TsmiReportAsFB2Click(object sender, EventArgs e)
		{
			MakeReport( 1 );
		}

		// редактировать выделенный файл в текстовом редакторе
		void TsmiEditInTextEditorClick(object sender, EventArgs e)
		{
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
		
		// редактировать выделенный файл в fb2-редакторе
		void TsmiEditInFB2EditorClick(object sender, EventArgs e)
		{
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
		
		// запустить файл в fb2-читалке (Просмотр)
		void TsmiVienInReaderClick(object sender, EventArgs e)
		{
			// читаем путь к читалке из настроек
			string sFBReaderPath = Settings.Settings.ReadFBReaderPath();
			string sTitle = "SharpFBTools - Открытие папки для файла";
			if( !File.Exists( sFBReaderPath ) ) {
				MessageBox.Show( "Не могу найти fb2 Читалку \""+sFBReaderPath+"\"!\nПроверьте, правильно ли задан путь в Настройках.", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
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
		
		// Открыть папку для выделенного файла
		void TsmiOpenFileDirClick(object sender, EventArgs e)
		{
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
		
		// Запустить выделенный файл в архиваторе
		void TsmiOpenFileInArchivatorClick(object sender, EventArgs e)
		{
			// читаем путь к архиватору из настроек
			string sArchPath = Settings.ValidatorSettings.ReadXmlArchivatorPath();
			string sTitle = "SharpFBTools - Запуск файла в Архиваторе";
			if( !File.Exists( sArchPath ) ) {
				MessageBox.Show( "Не могу найти Архиватор \""+sArchPath+"\"!\nПроверьте, правильно ли задан путь в Настройках.", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
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
				filesWorker.StartAsyncFile( sArchPath, sFilePath );
			}
		}
		
		// Повторная Проверка выбранного fb2-файла или архива (Валидация)
		void TsmiFileReValidateClick(object sender, EventArgs e)
		{
			#region Код
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 && l.SelectedItems.Count != 0 ) {
				DateTime dtStart = DateTime.Now;
				string sTempDir = Settings.Settings.GetTempDir();
				ListView.SelectedListViewItemCollection si = l.SelectedItems;
				string sSelectedItemText = si[0].SubItems[0].Text;
				bool isZip = sSelectedItemText.IndexOf('/') != -1 ? true : false;
				string sFilePath = sSelectedItemText.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: "+sFilePath+"\" не найден!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				
				MessageBoxIcon mbi = MessageBoxIcon.Information;
				string sMsg = "";
				string sErrorMsg = "Сообщение об ошибке:";
				string sOkMsg = "Ошибок нет - Файл ВАЛИДЕН";
				string sMoveNotValToVal = "Путь к этому файлу перенесен из Списка не валидных fb2-файлов в Список валидных.";
				string sMoveValToNotVal = "Путь к этому файлу перенесен из Списка валидных fb2-файлов в Список не валидных.";
				FB2Validator fv2Validator = new FB2Validator();
				
				if( !isZip ) {
					// для несжатого fb2-файла
					sMsg = rbtnFB2Librusec.Checked ? fv2Validator.ValidatingFB2LibrusecFile( sFilePath ) : fv2Validator.ValidatingFB22File( sFilePath );
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
				} else {
					// zip архив
					filesWorker.RemoveDir( sTempDir ); // очистка временной папки
					sharpZipLib.UnZipFiles(sFilePath, sTempDir, 0, false, null, 4096);
					string [] files = Directory.GetFiles( sTempDir );
					if( files.Length > 0 ) {
						// ищем проверяемый файл среди всех распакованных во временную папку
						string sUnzipedFilePath = sTempDir + "\\" + sSelectedItemText.Split('/')[1];
						sMsg = rbtnFB2Librusec.Checked ? fv2Validator.ValidatingFB2LibrusecFile( sUnzipedFilePath ) : fv2Validator.ValidatingFB22File( sUnzipedFilePath );
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
								item.ForeColor = m_ZipFB2ValidFontColor;
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
								item.ForeColor = m_ZipFB2NotValidFontColor;
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
				filesWorker.RemoveDir( sTempDir ); // очистка временной папки
				MessageBox.Show( "Повторная проверка выделенного файла на соответствие FictionBook.xsd схеме завершена.\nЗатрачено времени: "+sTime+"\n\nФайл: \""+sSelectedItemText+"\"\n\n"+sErrorMsg+"\n"+sMsg, "SharpFBTools - "+sErrorMsg, MessageBoxButtons.OK, mbi );
			}
			#endregion
		}
		
		// удаление выделенного файла с диска
		void TsmiDeleteFileFromDiskClick(object sender, EventArgs e)
		{
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 && l.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = l.SelectedItems;
				string sFilePath = si[0].SubItems[0].Text.Split('/')[0];
				const string sTitle = "SharpFBTools - Удаление файла с диска";
				if( !File.Exists( sFilePath ) ) {
					if( MessageBox.Show( "Файл: \""+sFilePath+"\" не найден!\nУдалить путь к этому файлу из списка?",
					                    sTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes ) {
						l.Items[ l.SelectedItems[0].Index ].Remove();
					}
				} else {
					if( MessageBox.Show( "Вы действительно хотите удалить файл: \""+sFilePath+"\" с диска?", sTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes ) {
						File.Delete( sFilePath );
						if (si[0].SubItems[0].Text.IndexOf('/') != -1) {
							// удаление zip архивов
							ListViewItem item = null;
							l.BeginUpdate();
							foreach (ListViewItem lvi in l.Items) {
								item = l.FindItemWithText(sFilePath);
								if (item != null)
									item.Remove();
							}
							l.EndUpdate();
						} else {
							// удаление НЕ zip (fb2 файлы и другое)
							l.Items[ l.SelectedItems[0].Index ].Remove();
						}
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
		
		// выбор варианта работы по двойному щелчку на списках
		void ListViewNotValidDoubleClick(object sender, EventArgs e)
		{
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 && l.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = l.SelectedItems;
				string sSelectedItemText = si[0].SubItems[0].Text;
				string sFilePath = sSelectedItemText.Split('/')[0];
				string sExt = Path.GetExtension( sFilePath ).ToLower();
				if( sExt == ".fb2" ) {
					switch ( Settings.ValidatorSettings.ReadXmlMouseDoubleClickForFB2Mode() ) {
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
				} else if( sExt == ".zip") {
					switch ( Settings.ValidatorSettings.ReadXmlMouseDoubleClickForZipMode() ) {
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
		
		// выбор варианта работы по нажатию Enter на списках
		void ListViewNotValidKeyPress(object sender, KeyPressEventArgs e)
		{
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 && l.SelectedItems.Count != 0 ) {
				if ( e.KeyChar == (char)Keys.Return ) {
					e.Handled = true;
					ListView.SelectedListViewItemCollection si = l.SelectedItems;
					string sSelectedItemText = si[0].SubItems[0].Text;
					string sFilePath = sSelectedItemText.Split('/')[0];
					string sExt = Path.GetExtension( sFilePath ).ToLower();
					if( sExt == ".fb2" ) {
						switch ( Settings.ValidatorSettings.ReadXmlEnterPressForFB2Mode() ) {
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
					} else if( sExt == ".zip" ) {
						switch ( Settings.ValidatorSettings.ReadXmlEnterPressForZipMode() ) {
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

		// сохранение списка Не валидных файлов
		void TsmiMakeNotValidFileListClick(object sender, EventArgs e)
		{
			ValidatorReports.SaveFilesList( listViewNotValid, sfdReport, m_TXTFilter,
			                               ssProgress,  tsslblProgress, tsProgressBar, m_FB2NotValidFilesListReport,
			                               "Нет ни одного Не валидного файла!", "Создание списка Не валидных файлов завершено.", Settings.Settings.GetReady() );
		}
		
		// сохранение списка Валидных файлов
		void TsmiMakeValidFileListClick(object sender, EventArgs e)
		{
			ValidatorReports.SaveFilesList( listViewValid, sfdReport, m_TXTFilter, ssProgress,
			                               tsslblProgress, tsProgressBar, m_FB2ValidFilesListReport,
			                               "Нет ни одного Валидного файла!", "Создание списка Валидных файлов завершено.", Settings.Settings.GetReady() );
		}
		
		void CboxExistFileSelectedIndexChanged(object sender, EventArgs e)
		{
			Settings.ValidatorSettings.ExistFileWorker = cboxExistFile.SelectedIndex;
			Settings.ValidatorSettings.WriteValidatorSettings();
		}
		
		void CboxValidatorForFB2SelectedIndexChanged(object sender, EventArgs e)
		{
			Settings.ValidatorSettings.MouseDoubleClickForFB2Mode = cboxValidatorForFB2.SelectedIndex;
			Settings.ValidatorSettings.WriteValidatorSettings();
		}
		
		void CboxValidatorForZipSelectedIndexChanged(object sender, EventArgs e)
		{
			Settings.ValidatorSettings.MouseDoubleClickForZipMode = cboxValidatorForZip.SelectedIndex;
			Settings.ValidatorSettings.WriteValidatorSettings();
		}
		
		void CboxValidatorForFB2PESelectedIndexChanged(object sender, EventArgs e)
		{
			Settings.ValidatorSettings.EnterPressForFB2Mode = cboxValidatorForFB2PE.SelectedIndex;
			Settings.ValidatorSettings.WriteValidatorSettings();
		}
		
		void CboxValidatorForZipPESelectedIndexChanged(object sender, EventArgs e)
		{
			Settings.ValidatorSettings.EnterPressForZipMode = cboxValidatorForZipPE.SelectedIndex;
			Settings.ValidatorSettings.WriteValidatorSettings();
		}
		
		void BtnArchivatorPathClick(object sender, EventArgs e)
		{
			// указание пути к Архиватору
			ofDlg.Title = "Укажите путь к Архиватору:";
			ofDlg.FileName = "";
			ofDlg.Filter = "Файлы .exe|*.exe|Все файлы (*.*)|*.*";
			DialogResult result = ofDlg.ShowDialog();
			if (result == DialogResult.OK) {
				tboxArchivatorPath.Text = ofDlg.FileName;
				Settings.ValidatorSettings.ArchivatorPath = ofDlg.FileName;
				Settings.ValidatorSettings.WriteValidatorSettings();
			}
		}
		
		void TboxSourceDirTextChanged(object sender, EventArgs e)
		{
			Settings.ValidatorSettings.SourceDir = tboxSourceDir.Text;
		}
		
		void TboxFB2NotValidDirCopyToTextChanged(object sender, EventArgs e)
		{
			Settings.ValidatorSettings.FB2NotValidDirCopyTo = tboxFB2NotValidDirCopyTo.Text;
		}
		
		void TboxFB2NotValidDirMoveToTextChanged(object sender, EventArgs e)
		{
			Settings.ValidatorSettings.FB2NotValidDirMoveTo = tboxFB2NotValidDirMoveTo.Text;
		}
		
		void TboxFB2ValidDirCopyToTextChanged(object sender, EventArgs e)
		{
			Settings.ValidatorSettings.FB2ValidDirCopyTo = tboxFB2ValidDirCopyTo.Text;
		}
		
		void TboxFB2ValidDirMoveToTextChanged(object sender, EventArgs e)
		{
			Settings.ValidatorSettings.FB2ValidDirMoveTo = tboxFB2ValidDirMoveTo.Text;
		}
		
		void TboxNotFB2DirCopyToTextChanged(object sender, EventArgs e)
		{
			Settings.ValidatorSettings.NotFB2DirCopyTo = tboxNotFB2DirCopyTo.Text;
		}
		
		void TboxNotFB2DirMoveToTextChanged(object sender, EventArgs e)
		{
			Settings.ValidatorSettings.NotFB2DirMoveTo = tboxNotFB2DirMoveTo.Text;
		}
		
		// Остановка выполнения процесса Копирование / Перемещение / Удаление
		void TsbtnFilesWorkStopClick(object sender, EventArgs e)
		{
			if( m_bwcmd.WorkerSupportsCancellation == true ) {
				m_bwcmd.CancelAsync();
			}
		}
		
		// запуск Валидации по нажатию Enter на поле ввода папки для сканирования
		void TboxSourceDirKeyPress(object sender, KeyPressEventArgs e)
		{
			if ( e.KeyChar == (char)Keys.Return ) {
				TsbtnValidateClick( sender, e );
			}
		}
		
		// отметить все FB2 книги
		void TsmiFB2CheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			CheckAll();
			ConnectListsEventHandlers( true );
		}
		
		// снять отметки со всех FB2 книг
		void TsmiFB2UnCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			UnCheckAll();
			ConnectListsEventHandlers( true );
		}
		
		// отметить все Архивы
		void TsmiArchiveCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			CheckAll();
			ConnectListsEventHandlers( true );
		}
		
		// снять отметки со всех Архивов
		void TsmiArchiveUnCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			UnCheckAll();
			ConnectListsEventHandlers( true );
		}
		
		// отметить все не FB2 файлы
		void TsmiNotFB2CheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			CheckAll();
			ConnectListsEventHandlers( true );
		}
		
		// снять отметки со не FB2 файлов
		void TsmiNotFB2UnCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			UnCheckAll();
			ConnectListsEventHandlers( true );
		}
		
		// сохранение списка найденных невалидных файлов
		void BtnSaveListClick(object sender, EventArgs e)
		{
			#region Код
			if( listViewNotValid.Items.Count==0 ) {
				MessageBox.Show( "Список невалидных файлов пуст!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}

			string sPath = "";
			sfdReport.Filter = "SharpFBTools файлы списков (*.xml)|*.xml|Все файлы (*.*)|*.*";
			sfdReport.FileName = "";
			sfdReport.InitialDirectory = Settings.Settings.GetProgDir();
			DialogResult result = sfdReport.ShowDialog();
			if( result == DialogResult.OK ) {
				sPath = sfdReport.FileName;
				Environment.CurrentDirectory = Settings.Settings.GetProgDir();
				XmlWriter writer = null;
				try {
					XmlWriterSettings data = new XmlWriterSettings();
					data.Indent = true;
					data.IndentChars = ("\t");
					data.OmitXmlDeclaration = true;
					
					writer = XmlWriter.Create( sPath, data );
					writer.WriteStartElement( "Validator" );
					// папка-источник
					writer.WriteStartElement( "ScanDir" );
					writer.WriteAttributeString( "scandir", m_sScan );
					writer.WriteFullEndElement();
					// число стьолбцов и записей
					writer.WriteStartElement( "Count" );
					writer.WriteAttributeString( "ItemsCount", listViewNotValid.Items.Count.ToString() );
					writer.WriteAttributeString( "ColumnsCount", listViewNotValid.Columns.Count.ToString() );
					writer.WriteFullEndElement();
					// данные поиска
					writer.WriteStartElement( "Items" );
					for( int i=0; i!=listViewNotValid.Items.Count; ++i ) {
						ListViewItem item = listViewNotValid.Items[i];
						writer.WriteStartElement( "item"+i.ToString() );
						for( int j=0; j!=listViewNotValid.Columns.Count; ++j ) {
							writer.WriteAttributeString( "c"+j.ToString(), item.SubItems[j].Text );
						}
						writer.WriteFullEndElement();
					}
					writer.WriteEndElement();
					writer.WriteEndElement();
					writer.Flush();
				}  finally  {
					if (writer != null)
						writer.Close();
				}
				MessageBox.Show( "Сохранение списка невалидных файлов завершено!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
			
			#endregion
		}
		
		// загрузка списка невалидных файлов
		void BtnLoadListClick(object sender, EventArgs e)
		{
			#region Код
			sfdLoadList.InitialDirectory = Settings.Settings.GetProgDir();
			sfdLoadList.Filter		= "SharpFBTools файлы списков (*.xml)|*.xml|Все файлы (*.*)|*.*";
			sfdLoadList.FileName	= "";
			string sPath = "";
			DialogResult result = sfdLoadList.ShowDialog();
			if( result == DialogResult.OK ) {
				sPath = sfdLoadList.FileName;
				// инициализация контролов
				Init();
				// установка режима поиска
				if( !File.Exists( sPath ) ) {
					MessageBox.Show( "Не найден файл списка Валидатора: \""+sPath+"\"!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return;
				}
				XmlReaderSettings data = new XmlReaderSettings();
				data.IgnoreWhitespace = true;
				using ( XmlReader reader = XmlReader.Create( sPath, data ) ) {
					// отключаем обработчики событий для lvResult (убираем "тормоза")
					ConnectListsEventHandlers( false );
					try {
						// папка-источник
						reader.ReadToFollowing("ScanDir");
						if( reader.HasAttributes ) {
							m_sScan = reader.GetAttribute("scandir");
							tboxSourceDir.Text = m_sScan;
						}
						// число стьолбцов и записей
						int nItemsCount	= 0, nColumnsCount = 0;
						reader.ReadToFollowing("Count");
						if( reader.HasAttributes ) {
							nItemsCount		= Convert.ToInt32( reader.GetAttribute("ItemsCount") );
							nColumnsCount	= Convert.ToInt32( reader.GetAttribute("ColumnsCount") );
						}
						// данные поиска
						ListViewItem lvi = null;
						for( int i=0; i!=nItemsCount; ++i ) {
							reader.ReadToFollowing("item"+i.ToString());
							if( reader.HasAttributes ) {
								lvi = new ListViewItem( reader.GetAttribute("c0") );
								for( int j=1; j!=nColumnsCount; ++j ) {
									lvi.SubItems.Add( reader.GetAttribute("c"+j.ToString()) );
								}
								listViewNotValid.Items.Add( lvi );
							}
						}
						// отобразим число невалидных файлов
						tpNotValid.Text = m_sNotValid + "( " + listViewNotValid.Items.Count.ToString() +" )";
						MessageBox.Show( "Список невалидных файлов загружен.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
					} catch {
						MessageBox.Show( "Поврежден файл списка Валидатора: \""+sPath+"\"!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
					} finally {
						reader.Close();
						ConnectListsEventHandlers( true );
					}
				}
			}
			#endregion
		}
		
		void TcResultSelectedIndexChanged(object sender, EventArgs e)
		{
			tsValidator.Enabled = tcResult.SelectedIndex != 3 ? true : false;
		}
		#endregion
		
	}
}
