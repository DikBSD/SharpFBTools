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
using Settings;

using fB2Parser					= Core.FB2.FB2Parsers.FB2Parser;
using filesWorker				= Core.FilesWorker.FilesWorker;
using archivesWorker			= Core.FilesWorker.Archiver;
using fb2Validator				= Core.FB2Parser.FB2Validator;
using stringProcessing			= Core.StringProcessing.StringProcessing;
using templatesParser			= Core.Templates.TemplatesParser;
using templatesVerify			= Core.Templates.TemplatesVerify;
using templatesLexemsSimple		= Core.Templates.Lexems.TPSimple;
using selectedSortQueryCriteria	= Core.BookSorting.SelectedSortQueryCriteria;

namespace SharpFBTools.Tools
{
	/// <summary>
	/// Description of SFBTpFileManager.
	/// </summary>
	public partial class SFBTpFileManager : UserControl
	{
		#region Закрытые данные класса
		private fb2Validator fv2V = new fb2Validator();
		private List<selectedSortQueryCriteria> m_lSSQCList = null; // список критериев поиска для Избранной Сортировки
        private DateTime m_dtStart;
        private BackgroundWorker m_bw = null;
		private string m_sSource		= "";
		private string m_sTarget		= "";
		private string m_sLineTemplate	= "";
		private string m_sMessTitle		= "";
        private bool m_bFullSort		= true;
        private bool m_bScanSubDirs		= true;
        #endregion
        
		public ListView GetSettingsInfoListView()
		{
			return lvSettings;
		}
		
		public SFBTpFileManager()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			InitializeBackgroundWorker();
			
			Init();
			// читаем сохраненные пути к папкам и шаблон Менеджера Файлов, если они есть
			ReadFMTempData();
			//
			string sTDPath = Settings.SettingsFM.GetDefFMDescTemplatePath();
			if( File.Exists( sTDPath ) ) {
				richTxtBoxDescTemplates.LoadFile( sTDPath );
			} else {
				richTxtBoxDescTemplates.Text = "Не найден файл описания Шаблонов подстановки: \""+sTDPath+"\"";
			}

			// загружаем в ListView-индикатор настроек данные 
			Settings.SettingsFM.SetInfoSettings( lvSettings );
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
			// сортировка файлов по папкам, согласно шаблонам подстановки
			int nAllFiles = 0;
			List<string> lDirList = new List<string>();
			if( !m_bScanSubDirs ) {
				// сканировать только указанную папку
				lDirList.Add( m_sSource );
				lvFilesCount.Items[0].SubItems[1].Text = "1";
			} else {
				// сканировать и все подпапки
				nAllFiles = filesWorker.DirsParser( m_bw, e, m_sSource, lvFilesCount, ref lDirList, false );
			}
			
			// отобразим число всех файлов в папке сканирования
			lvFilesCount.Items[1].SubItems[1].Text = nAllFiles.ToString();
			
			// Проверить флаг на остановку процесса 
			if( ( m_bw.CancellationPending == true ) ) {
				e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
				return;
			}
			
			// проверка, есть ли хоть один файл в папке для сканирования
			if( nAllFiles == 0 ) {
				MessageBox.Show( "В папке сканирования не найдено ни одного файла!\nРабота прекращена.", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				tsslblProgress.Text = Settings.Settings.GetReady();
				SetSelectedSortingStartEnabled( true );
				return;
			}
			
			tsslblProgress.Text		= "Сортировка файлов:";
			tsProgressBar.Maximum	= nAllFiles;
			tsProgressBar.Value		= 0;
			
			// данные настроек для сортировки по шаблонам
			Settings.DataFM dfm = new Settings.DataFM();
			
			// формируем лексемы шаблонной строки
			List<templatesLexemsSimple> lSLexems = templatesParser.GemSimpleLexems( m_sLineTemplate );
			// сортировка
			if( m_bFullSort ) {
				// Полная Сортировка
				foreach( string s in lDirList ) {
					DirectoryInfo diFolder = new DirectoryInfo( s );
					foreach( FileInfo fiNextFile in diFolder.GetFiles() ) {
						// Проверить флаг на остановку процесса 
						if( ( m_bw.CancellationPending == true ) ) {
							e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
							break;
						} 
						string sFromFilePath = s + "\\" + fiNextFile.Name;
						
						// создаем файл по новому пути
						if( dfm.GenreOneMode && dfm.AuthorOneMode ) {
							// по первому Жанру и первому Автору Книги
							MakeFileFor1Genre1Author( sFromFilePath, m_sSource, m_sTarget, lSLexems, dfm );
						} else if( dfm.GenreOneMode && !dfm.AuthorOneMode ) {
							// по первому Жанру и всем Авторам Книги
							MakeFileFor1GenreAllAuthor( sFromFilePath, m_sSource, m_sTarget, lSLexems, dfm );
						} else if( !dfm.GenreOneMode && dfm.AuthorOneMode ) {
							// по всем Жанрам и первому Автору Книги
							MakeFileForAllGenre1Author( sFromFilePath, m_sSource, m_sTarget, lSLexems, dfm );
						} else {
							// по всем Жанрам и всем Авторам Книги
							MakeFileForAllGenreAllAuthor( sFromFilePath, m_sSource, m_sTarget, lSLexems, dfm );
						}

						m_bw.ReportProgress( 0 ); // отобразим данные в контролах
					}
				}
			} else {
				// Избранная Сортировка
				foreach( string s in lDirList ) {
					DirectoryInfo diFolder = new DirectoryInfo( s );
					foreach( FileInfo fiNextFile in diFolder.GetFiles() ) {
						// Проверить флаг на остановку процесса 
						if( ( m_bw.CancellationPending == true ) ) {
							e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
							break;
						} 
						string sFromFilePath = s + "\\" + fiNextFile.Name;
						string sExt = Path.GetExtension( sFromFilePath ).ToLower();
						// создаем файл по новому пути
						if( sExt==".fb2" ) {
							// Создание файла по критериям Избранной сортировки
							MakeFileForSelectedSortingWorker( sFromFilePath, m_sSource, m_sTarget, lSLexems, dfm );
						} else {
							// это архив?
							if( archivesWorker.IsArchive( sExt ) ) {
								List<string> lFilesListFromArchive = archivesWorker.GetFileListFromArchive( sFromFilePath, Settings.Settings.GetTempDir(), dfm.A7zaPath, dfm.UnRarPath );
								IncArchiveInfo( sExt ); // Увеличить число определенного файла-архива на 1
								if( lFilesListFromArchive!=null ) {
									foreach( string sFB2FromArchPath in lFilesListFromArchive ) {
										// Создание файла по критериям Избранной сортировки
										MakeFileForSelectedSortingWorker( sFB2FromArchPath, m_sSource, m_sTarget, lSLexems, dfm );
									}
								}
								filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
							}
						}
						m_bw.ReportProgress( 0 ); // отобразим данные в контролах
					}
				}
			}
			lDirList.Clear();
        }

		private void bw_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
            // Отобразим результат

            ++tsProgressBar.Value;
        }
		
        private void bw_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {   
            // Проверяем это отмена, ошибка, или конец задачи и сообщить
            DateTime dtEnd = DateTime.Now;
            filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
            
            string sTime = dtEnd.Subtract( m_dtStart ).ToString() + " (час.:мин.:сек.)";
			string sMessCanceled	= "Сортировка остановлена!\nЗатрачено времени: "+sTime;
			string sMessError		= "";
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

			if( m_bFullSort ) {
				SetFullSortingStartEnabled( true );
			} else {
				SetSelectedSortingStartEnabled( true );
			}
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
		
		private void SetFullSortingStartEnabled( bool bEnabled ) {
			// доступность контролов при Полной Сортировки
			tpSelectedSort.Enabled	= bEnabled;
			tsbtnOpenDir.Enabled	= bEnabled;
			tsbtnTargetDir.Enabled	= bEnabled;
			tsbtnSortFilesTo.Enabled= bEnabled;
			pFullSortDirs.Enabled	= bEnabled;
			gBoxFullSortRenameTemplates.Enabled	= bEnabled;
			tsbtnFullSortStop.Enabled	= !bEnabled;
			tsProgressBar.Visible		= !bEnabled;
			tcSort.Refresh();
			ssProgress.Refresh();
		}
		
		private void SetSelectedSortingStartEnabled( bool bEnabled ) {
			// доступность контролов при Избранной Сортировки
			tpFullSort.Enabled			= bEnabled;
			tsbtnSSOpenDir.Enabled		= bEnabled;
			tsbtnSSTargetDir.Enabled	= bEnabled;
			tsbtnSSSortFilesTo.Enabled	= bEnabled;
			pSelectedSortDirs.Enabled	= bEnabled;
			gBoxSelectedlSortRenameTemplates.Enabled	= bEnabled;
			pSSData.Enabled				= bEnabled;
			tsbtnSSSortStop.Enabled		= !bEnabled;
			tsProgressBar.Visible		= !bEnabled;
			tcSort.Refresh();
			ssProgress.Refresh();
		}
		
		private bool IsFolderdataCorrect( TextBox tbSource, TextBox tbTarget )
		{
			// проверка на корректность данных папок источника и приемника файлов
			// обработка заданных каталогов
			m_sSource		= filesWorker.WorkingDirPath( tbSource.Text.Trim() );
			tbSource.Text	= m_sSource;
			m_sTarget		= filesWorker.WorkingDirPath( tbTarget.Text.Trim() );
			tbTarget.Text	= m_sTarget;
			
			// проверки на корректность папок источника и приемника
			if( m_sSource.Length == 0 ) {
				MessageBox.Show( "Выберите папку для сканирования!", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			DirectoryInfo diFolder = new DirectoryInfo( m_sSource );
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка не найдена: " + m_sSource, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			if( m_sTarget.Length == 0 ) {
				MessageBox.Show( "Не указана папка-приемник файлов!\nРабота прекращена.", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			if( m_sSource == m_sTarget ) {
				MessageBox.Show( "Папка-приемник файлов совпадает с папкой сканирования!\nРабота прекращена.", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка папки-приемника и создание ее, если нужно
			if( !filesWorker.CreateDirIfNeed( m_sTarget, m_sMessTitle ) ) {
				return false;
			}
			return true;
		}
		
		private void ReadFMTempData() {
			// чтение путей к данным Менеджера Файлов из xml-файла
			string sSettings = Settings.Settings.WorksDataSettingsPath;
			if( !File.Exists( sSettings ) ) return;
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.IgnoreWhitespace = true;
			using ( XmlReader reader = XmlReader.Create( sSettings, settings ) ) {
				// Полная Сортировка
				reader.ReadToFollowing("FMScanDir");
				if (reader.HasAttributes ) {
					tboxSourceDir.Text = reader.GetAttribute("tboxSourceDir");
					Settings.SettingsFM.FMDataScanDir = tboxSourceDir.Text.Trim();
				}
				reader.ReadToFollowing("FMTargetDir");
				if (reader.HasAttributes ) {
					tboxSortAllToDir.Text = reader.GetAttribute("tboxSortAllToDir");
					Settings.SettingsFM.FMDataTargetDir = tboxSortAllToDir.Text.Trim();
				}
				reader.ReadToFollowing("FMTemplate");
				if (reader.HasAttributes ) {
					txtBoxTemplatesFromLine.Text = reader.GetAttribute("txtBoxTemplatesFromLine");
					Settings.SettingsFM.FMDataTemplate =  txtBoxTemplatesFromLine.Text.Trim();
				}
				// Избранная Сортировка
				reader.ReadToFollowing("FMSSScanDir");
				if (reader.HasAttributes ) {
					tboxSSSourceDir.Text = reader.GetAttribute("tboxSSSourceDir");
					Settings.SettingsFM.FMDataSSScanDir = tboxSSSourceDir.Text.Trim();
				}
				reader.ReadToFollowing("FMSSTargetDir");
				if (reader.HasAttributes ) {
					tboxSSToDir.Text = reader.GetAttribute("tboxSSToDir");
					Settings.SettingsFM.FMDataSSTargetDir = tboxSSToDir.Text.Trim();
				}
				reader.ReadToFollowing("FMSSTemplate");
				if (reader.HasAttributes ) {
					txtBoxSSTemplatesFromLine.Text = reader.GetAttribute("txtBoxSSTemplatesFromLine");
					Settings.SettingsFM.FMDataSSTemplate =  txtBoxSSTemplatesFromLine.Text.Trim();
				}
				reader.Close();
			}
		}
		
		private void IncArchiveInfo( string sExt ) {
			// Увеличить число определенного файла-архива на 1
			switch( sExt ) {
				case ".rar":
					IncStatus( 4 );
					break;
				case ".zip":
					IncStatus( 3 );
					break;
				case ".7z":
					IncStatus( 5 );
					break;
				case ".bz2":
					IncStatus( 6 );
					break;
				case ".gz":
					IncStatus( 7 );
					break;
				case ".tar":
					IncStatus( 8 );
					break;
			}
		}
		
		private void CreateFileTo( string sFromFilePath, string sToFilePath, int nFileExistMode,
		                          	bool bAddToFileNameBookIDMode, Settings.DataFM dfm ) {
			// создание нового файла или архива
			try {
				if( !dfm.ToArchiveMode ) {
					CopyFileToTargetDir( sFromFilePath, sToFilePath, false, nFileExistMode, bAddToFileNameBookIDMode );
				} else {
					// упаковка в архив
					string sArchType = stringProcessing.GetArchiveExt( dfm.ArchiveTypeText );
					CopyFileToArchive( dfm.A7zaPath, dfm.RarPath, sArchType, sFromFilePath, sToFilePath+"."+sArchType, 
					                  nFileExistMode, bAddToFileNameBookIDMode );
				}
			} catch ( System.IO.PathTooLongException ) {
				string sFileLongPathDir = dfm.FileLongPathDir;
				Directory.CreateDirectory( sFileLongPathDir );
				sToFilePath = sFileLongPathDir+"\\"+Path.GetFileName( sFromFilePath );
				CopyFileToTargetDir( sFromFilePath, sToFilePath, true, nFileExistMode, false );	
			}
		}
		
		private void CopyFileToArchive( string s7zaPath, string sRarPath, string sArchType,
		                               string sFromFilePath, string sToFilePath,
		                               int nFileExistMode, bool bAddToFileNameBookIDMode ) {
			// архивирование файла с сформированным именем (путь)
			// обработка уже существующих файлов в папке
			Regex rx = new Regex( @"\\+" );
			sFromFilePath = rx.Replace( sFromFilePath, "\\" );
			sToFilePath = rx.Replace( sToFilePath, "\\" );
			
			sToFilePath = FileExsistWorker( sFromFilePath, sToFilePath, nFileExistMode, bAddToFileNameBookIDMode, sArchType );
			if( sArchType == "rar" ) {
				archivesWorker.rar( sRarPath, sFromFilePath, sToFilePath, true, ProcessPriorityClass.AboveNormal );
			} else {
				archivesWorker.zip( s7zaPath, sArchType, sFromFilePath, sToFilePath, ProcessPriorityClass.AboveNormal );
			}
			IncStatus( 11 ); // всего создано
		}
		
		private void CopyFileToTargetDir( string sFromFilePath, string sToFilePath, bool bBad,
		                                 int nFileExistMode, bool bAddToFileNameBookIDMode )
		{
			// копирование файла с сформированным именем (путь)
			Regex rx = new Regex( @"\\+" );
			sFromFilePath = rx.Replace( sFromFilePath, "\\" );
			sToFilePath = rx.Replace( sToFilePath, "\\" );
			// обработка уже существующих файлов в папке
			sToFilePath = FileExsistWorker( sFromFilePath, sToFilePath, nFileExistMode, bAddToFileNameBookIDMode, "" );
			if( File.Exists( sFromFilePath ) ) {
				File.Copy( sFromFilePath, sToFilePath );
				if( !bBad ) {
					if( File.Exists( sToFilePath ) ) {
					   	IncStatus( 11 ); // всего создано
					}
				}
			}
		}
		
		private void CopyBadArchiveToBadDir( string sFromFilePath, string sSource, string sToDir, int nFileExistMode )
		{
			// копирование "битого" с сформированным именем (путь)
			string sToFilePath = sToDir+"\\"+sFromFilePath.Remove( 0, sSource.Length );
			Regex rx = new Regex( @"\\+" );
			sFromFilePath = rx.Replace( sFromFilePath, "\\" );
			sToFilePath = rx.Replace( sToFilePath, "\\" );
			string sSufix = "";
			FileInfo fi = new FileInfo( sToFilePath );
			if( !fi.Directory.Exists ) {
				Directory.CreateDirectory( fi.Directory.ToString() );
			}
			// обработка уже существующих файлов в папке
			if( File.Exists( sToFilePath ) ) {
				if( nFileExistMode == 0 ) {
					File.Delete( sToFilePath );
				} else {
					if( nFileExistMode == 1 ) {
						// Добавить к создаваемому архиву очередной номер
						sSufix += "_" + stringProcessing.GetFileNewNumber( sToFilePath ).ToString();
					} else {
						// Добавить к создаваемому архиву дату и время
						sSufix += "_" + stringProcessing.GetDateTimeExt();
					}
					sToFilePath = sToFilePath.Remove( sToFilePath.Length-4 ) + sSufix + Path.GetExtension( sToFilePath );
				}
			}
			if( File.Exists( sFromFilePath ) ) {
				File.Copy( sFromFilePath, sToFilePath );
			}
			IncStatus( 14 ); // "битые" архивы - не открылись
		}
		
		private string FileExsistWorker( string sFromFilePath, string sToFilePath, int nFileExistMode,
		                                bool bAddToFileNameBookIDMode, string sArchType )
		{
			// обработка уже существующих файлов в папке
			string sSufix = "";
			FileInfo fi = new FileInfo( sToFilePath );
			if( !fi.Directory.Exists ) {
				Directory.CreateDirectory( fi.Directory.ToString() );
			}
			if( File.Exists( sToFilePath ) ) {
				if( nFileExistMode == 0 ) {
					File.Delete( sToFilePath );
				} else {
					if( bAddToFileNameBookIDMode ) {
						sSufix = "_" + stringProcessing.GetFMBookID( sFromFilePath );
					}
					if( nFileExistMode == 1 ) {
						// Добавить к создаваемому файлу очередной номер
						sSufix += "_" + stringProcessing.GetFileNewNumber( sToFilePath ).ToString();
					} else {
						// Добавить к создаваемому файлу дату и время
						sSufix += "_" + stringProcessing.GetDateTimeExt();
					}
					if( sArchType.Length==0 ) {
						sToFilePath = sToFilePath.Remove( sToFilePath.Length-4 ) + sSufix + ".fb2";
					} else {
						sToFilePath = sToFilePath.Remove( sToFilePath.Length - (sArchType.Length+5) ) + sSufix + ".fb2." + sArchType;
					}
				}
			}
			return sToFilePath;
		}
		
		private void MakeFileFor1Genre1Author( string sFromFilePath, string sSource, string sTarget,
		                                      List<templatesLexemsSimple> lSLexems, Settings.DataFM dfm ) {
			// создаем файл по новому пути для первого Жанра и для первого Автора Книги
			string sExt = Path.GetExtension( sFromFilePath ).ToLower();
			if( sExt==".fb2" ) {
				MakeFileFor1Genre1AuthorWorker( sExt, sFromFilePath, sSource, sTarget, lSLexems, dfm, false );
				IncStatus( 2 ); // исходные fb2-файлы
			} else {
				// это архив?
				if( archivesWorker.IsArchive( sExt ) ) {
					List<string> lFilesListFromArchive = archivesWorker.GetFileListFromArchive( sFromFilePath, Settings.Settings.GetTempDir(), dfm.A7zaPath, dfm.UnRarPath );
					IncArchiveInfo( sExt ); // Увеличить число определенного файла-архива на 1
					if( lFilesListFromArchive==null ) {
						CopyBadArchiveToBadDir( sFromFilePath, sSource, dfm.NotOpenArchDir, dfm.FileExistMode );
						return; // не получилось открыть архив - "битый"
					}
					foreach( string sFB2FromArchPath in lFilesListFromArchive ) {
						MakeFileFor1Genre1AuthorWorker( Path.GetExtension( sFB2FromArchPath ).ToLower(), sFB2FromArchPath,
						                               sSource, sTarget, lSLexems, dfm, true );
						IncStatus( 9 ); // Исходные fb2-файлы из архивов
					}
					filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
				}  else {
					// пропускаем не fb2-файлы и архивы
					IncStatus( 10 ); // другие файлы
				}
			}
		}
		private void MakeFileFor1Genre1AuthorWorker( string sExt, string sFromFilePath, string sSource, string sTarget,
		                                      List<templatesLexemsSimple> lSLexems, Settings.DataFM dfm, bool bFromArchive ) {
			try {
				MakeFB2File( sFromFilePath, sSource, sTarget, lSLexems, dfm, bFromArchive, 0, 0 );
			} catch {
				if( sExt==".fb2" ) {
					CopyBadFileToDir( sFromFilePath, sSource, bFromArchive, dfm.NotReadFB2Dir, dfm.FileExistMode );
				}
			}
		}
		
		private void MakeFileForAllGenre1Author( string sFromFilePath, string sSource, string sTarget,
		                                      List<templatesLexemsSimple> lSLexems, Settings.DataFM dfm ) {
			// создаем файл по новому пути для всех Жанров и для первого Автора Книги
			string sExt = Path.GetExtension( sFromFilePath ).ToLower();
			if( sExt==".fb2" ) {
				MakeFileForAllGenre1AuthorWorker( sExt, sFromFilePath, sSource, sTarget, lSLexems, dfm, false ) ;
				IncStatus( 2 ); // исходные fb2-файлы
			} else {
				// это архив?
				if( archivesWorker.IsArchive( sExt ) ) {
					List<string> lFilesListFromArchive = archivesWorker.GetFileListFromArchive( sFromFilePath, Settings.Settings.GetTempDir(), dfm.A7zaPath, dfm.UnRarPath );
					IncArchiveInfo( sExt ); // Увеличить число определенного файла-архива на 1
					if( lFilesListFromArchive==null ) {
						CopyBadArchiveToBadDir( sFromFilePath, sSource, dfm.NotOpenArchDir, dfm.FileExistMode );
						return; // не получилось открыть архив - "битый"
					}
					foreach( string sFB2FromArchPath in lFilesListFromArchive ) {
						MakeFileForAllGenre1AuthorWorker( Path.GetExtension( sFB2FromArchPath ).ToLower(), sFB2FromArchPath,
						                                 sSource, sTarget, lSLexems, dfm, true ) ;
						IncStatus( 9 ); // Исходные fb2-файлы из архивов
					}
					filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
				}  else {
					// пропускаем не fb2-файлы и архивы
					IncStatus( 10 ); // другие файлы
				}
			}
		}
		private void MakeFileForAllGenre1AuthorWorker( string sExt, string sFromFilePath, string sSource, string sTarget,
		                                      List<templatesLexemsSimple> lSLexems, Settings.DataFM dfm, bool bFromArchive ) {
			try {
				fB2Parser fb2 = new fB2Parser( sFromFilePath );
				TitleInfo ti = fb2.GetTitleInfo();
				IList<Genre> lGenres = ti.Genres;
				IList<Author> lAuthors = ti.Authors;
				for( int i=0; i!=lGenres.Count; ++i ) {
					MakeFB2File( sFromFilePath, sSource, sTarget, lSLexems, dfm, bFromArchive, i, 0 );
				}
			} catch {
				if( sExt==".fb2" ) {
					CopyBadFileToDir( sFromFilePath, sSource, bFromArchive, dfm.NotReadFB2Dir, dfm.FileExistMode );
				}
			}
		}

		private void MakeFileFor1GenreAllAuthor( string sFromFilePath, string sSource, string sTarget,
		                                      	List<templatesLexemsSimple> lSLexems, Settings.DataFM dfm ) {
			// создаем файл по новому пути для первого Жанра и для всех Авторов Книги
			string sExt = Path.GetExtension( sFromFilePath ).ToLower();
			if( sExt==".fb2" ) {
				MakeFileFor1GenreAllAuthorWorker( sExt, sFromFilePath, sSource, sTarget, lSLexems, dfm, false );
				IncStatus( 2 ); // исходные fb2-файлы
			} else {
				// это архив?
				if( archivesWorker.IsArchive( sExt ) ) {
					List<string> lFilesListFromArchive = archivesWorker.GetFileListFromArchive( sFromFilePath, Settings.Settings.GetTempDir(), dfm.A7zaPath, dfm.UnRarPath );
					IncArchiveInfo( sExt ); // Увеличить число определенного файла-архива на 1
					if( lFilesListFromArchive==null ) {
						CopyBadArchiveToBadDir( sFromFilePath, sSource, dfm.NotOpenArchDir, dfm.FileExistMode );
						return; // не получилось открыть архив - "битый"
					}
					foreach( string sFB2FromArchPath in lFilesListFromArchive ) {
						MakeFileFor1GenreAllAuthorWorker( Path.GetExtension( sFB2FromArchPath ).ToLower(), sFB2FromArchPath,
						                                 sSource, sTarget, lSLexems, dfm, true );
						IncStatus( 9 ); // Исходные fb2-файлы из архивов
					}
					filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
				} else {
					// пропускаем не fb2-файлы и архивы
					IncStatus( 10 ); // другие файлы
				}
			}
		}
		private void MakeFileFor1GenreAllAuthorWorker( string sExt, string sFromFilePath, string sSource, string sTarget,
		                                      List<templatesLexemsSimple> lSLexems, Settings.DataFM dfm, bool bFromArchive ) {
			try {
				fB2Parser fb2 = new fB2Parser( sFromFilePath );
				TitleInfo ti = fb2.GetTitleInfo();
				IList<Genre> lGenres = ti.Genres;
				IList<Author> lAuthors = ti.Authors;
				for( int i=0; i!=lAuthors.Count; ++i ) {
					MakeFB2File( sFromFilePath, sSource, sTarget, lSLexems, dfm, bFromArchive, 0, i );
				}
			} catch {
				if( sExt==".fb2" ) {
					CopyBadFileToDir( sFromFilePath, sSource, bFromArchive, dfm.NotReadFB2Dir, dfm.FileExistMode );
				}
			}
		}
		
		private void MakeFileForAllGenreAllAuthor( string sFromFilePath, string sSource, string sTarget,
		                                      		List<templatesLexemsSimple> lSLexems, Settings.DataFM dfm) {
			// создаем файл по новому пути для всех Жанров и для всех Авторов Книги
			string sExt = Path.GetExtension( sFromFilePath ).ToLower();
			if( sExt==".fb2" ) {
				MakeFileForAllGenreAllAuthorWorker( sExt, sFromFilePath, sSource, sTarget, lSLexems, dfm, false );
				IncStatus( 2 ); // исходные fb2-файлы
			} else {
				// это архив?
				if( archivesWorker.IsArchive( sExt ) ) {
					List<string> lFilesListFromArchive = archivesWorker.GetFileListFromArchive( sFromFilePath, Settings.Settings.GetTempDir(), dfm.A7zaPath, dfm.UnRarPath );
					IncArchiveInfo( sExt ); // Увеличить число определенного файла-архива на 1
					if( lFilesListFromArchive==null ) {
						CopyBadArchiveToBadDir( sFromFilePath, sSource, dfm.NotOpenArchDir, dfm.FileExistMode );
						return; // не получилось открыть архив - "битый"
					}
					foreach( string sFB2FromArchPath in lFilesListFromArchive ) {
						MakeFileForAllGenreAllAuthorWorker( Path.GetExtension( sFB2FromArchPath ).ToLower(), sFB2FromArchPath,
						                                   sSource, sTarget, lSLexems, dfm, true );
						IncStatus( 9 ); // Исходные fb2-файлы из архивов
					}
					filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
				}  else {
					// пропускаем не fb2-файлы и архивы
					IncStatus( 10 ); // другие файлы
				}
			}
		}
		private void MakeFileForAllGenreAllAuthorWorker( string sExt, string sFromFilePath, string sSource, string sTarget,
		                                      List<templatesLexemsSimple> lSLexems, Settings.DataFM dfm, bool bFromArchive ) {
			try {
				fB2Parser fb2 = new fB2Parser( sFromFilePath );
				TitleInfo ti = fb2.GetTitleInfo();
				IList<Genre> lGenres = ti.Genres;
				IList<Author> lAuthors = ti.Authors;
				for( int i=0; i!= lGenres.Count; ++i ) {
					for( int j=0; j!=lAuthors.Count; ++j ) {
						MakeFB2File( sFromFilePath, sSource, sTarget, lSLexems, dfm, bFromArchive, i, j );
					}
				}
			} catch {
				if( sExt==".fb2" ) {
					CopyBadFileToDir( sFromFilePath, sSource, bFromArchive, dfm.NotReadFB2Dir, dfm.FileExistMode );
				}
			}
		}
		
		private bool IsValid( string sFromFilePath, string sSource, Settings.DataFM dfm,
		                     bool bFromArchive, int nGenreIndex, int nAuthorIndex ) {
			// если режим сортировки - только валидные - то проверка и копирование невалидных в папку
			string sResult = fv2V.ValidatingFB2File( sFromFilePath );
			if ( sResult.Length != 0 ) {
				// защита от многократного копирования невалимдного файла в папку для невалидных
				if( nGenreIndex==0 && nAuthorIndex==0 ) {
					// помещаем его в папку для невалидных файлов
					CopyBadFileToDir( sFromFilePath, sSource, bFromArchive, dfm.NotValidFB2Dir, dfm.FileExistMode );
					IncStatus( 13 ); // не валидные fb2-файлы
					return false; // файл невалидный - пропускаем его, сортируем дальше
				} else {
					return false; // файл уже скопирован - пропускаем его, сортируем дальше
				}
			}
			return true;
		}
		
		private void CopyBadFileToDir( string sFromFilePath, string sSource, bool bFromArchive,
		                              string sBadDir, int nFileExistMode ) {
			// нечитаемый fb2-файл или архив - копируем его в папку Bad
			Directory.CreateDirectory( sBadDir );
			string sFrom = ( !bFromArchive ? sSource : Settings.Settings.GetTempDir() );
			string sToFilePath = sBadDir+"\\"+sFromFilePath.Remove( 0, sFrom.Length );
			CopyFileToTargetDir( sFromFilePath, sToFilePath, true, nFileExistMode, false );
			IncStatus( 12 ); // нечитаемые fb2-файлы или архивы
		}
			
		private void MakeFB2File( string sFromFilePath, string sSource, string sTarget,
		                      List<templatesLexemsSimple> lSLexems, Settings.DataFM dfm,
		                      bool bFromArchive, int nGenreIndex, int nAuthorIndex ) {
			// создаем файл по новому пути
			string sTempDir = Settings.Settings.GetTempDir();
			// смотрим, что это за файл
			string sExt = Path.GetExtension( sFromFilePath ).ToLower();
			if( sExt == ".fb2" ) {
				// обработка fb2-файла
				// тип сортировки
				if( !dfm.SortValidType  ) {
					if( !IsValid( sFromFilePath, sSource, dfm, bFromArchive, nGenreIndex, nAuthorIndex ) ) {
						return;
					}
				}
				try {
					string sToFilePath = sTarget + "\\" +
							templatesParser.Parse( sFromFilePath, lSLexems, dfm, nGenreIndex, nAuthorIndex ) + ".fb2";
					CreateFileTo( sFromFilePath, sToFilePath, dfm.FileExistMode, dfm.AddToFileNameBookIDMode, dfm );
				} catch /*( System.IO.FileLoadException )*/ {
					// нечитаемый fb2-файл - копируем его в папку Bad
					CopyBadFileToDir( sFromFilePath, sSource, bFromArchive, dfm.NotReadFB2Dir, dfm.FileExistMode );
				}
				
				if( dfm.DelFB2FilesMode ) {
					// удаляем исходный fb2-файл
					if( File.Exists( sFromFilePath ) ) {
						File.Delete( sFromFilePath );
					}
				}
			}
		}
		
		private void IncStatus( int nItem ) {
			lvFilesCount.Items[nItem].SubItems[1].Text	=
					Convert.ToString( 1+Convert.ToInt32( lvFilesCount.Items[nItem].SubItems[1].Text ) );
		}

		private bool IsArchivatorsExist() {
			// проверка на наличие архиваторов
			string s7zPath	= Settings.Settings.Read7zaPath();
			string sRarPath	= Settings.Settings.ReadRarPath();
			if( Settings.SettingsFM.ReadToArchiveMode() ) {
				if( Settings.SettingsFM.ReadArchiveTypeText().ToLower()=="rar" ) {
					if( sRarPath.Trim().Length==0 ) {
						MessageBox.Show( "В Настройках выбрана rar-архивация отсортированных файлов.\nПри этом не указана папка с установленным консольным Rar-архиватором!\nУкажите путь к нему в Настройках.\nРабота остановлена!",
						                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
						return false;
					} else {
						// проверка на наличие архиваторов
						if( !File.Exists( sRarPath ) ) {
							MessageBox.Show( "В Настройках выбрана rar-архивация отсортированных файлов.\nПри этом не найден файл консольного Rar-архиватора "+sRarPath+"!\nУкажите путь к нему в Настройках.\nРабота остановлена!",
							                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
							return false;
						}
					}
				}
			}
			
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
			return true;
		}
		
		private bool IsLineTemplateCorrect( string sLineTemplate ) {
			// проверки на корректность шаблонных строк
			// проверка "пустоту" строки с шаблонами
			if( sLineTemplate.Length == 0 ) {
				MessageBox.Show( "Строка шаблонов не может быть пустой!\nРабота прекращена.", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
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
		
		private void MakeFileForSelectedSortingWorker( string sFromFilePath, string sSource, string sTarget,
		                                             List<templatesLexemsSimple> lSLexems, Settings.DataFM dfm ) {
			// Создание файла по критериям Избранной сортировки
			// проверка, соответствует ли текущий файл критерия поиска для Избранной Сортировки
			FB2SelectedSorting fb2ss = new FB2SelectedSorting();
			if( fb2ss.IsConformity( sFromFilePath, m_lSSQCList ) ) {
				if( dfm.GenreOneMode && dfm.AuthorOneMode ) {
					// по первому Жанру и первому Автору Книги
					MakeFileFor1Genre1Author( sFromFilePath, sSource, sTarget, lSLexems, dfm );
				} else if( dfm.GenreOneMode && !dfm.AuthorOneMode ) {
					// по первому Жанру и всем Авторам Книги
					MakeFileFor1GenreAllAuthor( sFromFilePath, sSource, sTarget, lSLexems, dfm );
				} else if( !dfm.GenreOneMode && dfm.AuthorOneMode ) {
					// по всем Жанрам и первому Автору Книги
					MakeFileForAllGenre1Author( sFromFilePath, sSource, sTarget, lSLexems, dfm );
				} else {
					// по всем Жанрам и всем Авторам Книги
					MakeFileForAllGenreAllAuthor( sFromFilePath, sSource, sTarget, lSLexems, dfm );
				}
			}
		}
		#endregion
		
		#region Обработчики событий
		void TsbtnOpenDirClick(object sender, EventArgs e)
		{
			// задание папки с fb2-файлами и архивами для сканирования
			filesWorker.OpenDirDlg( tboxSourceDir, fbdScanDir, "Укажите папку для сканирования с fb2-файлами и архивами:" );
		}
		
		void TsbtnSortFilesToClick(object sender, EventArgs e)
		{
			// Полная сортировка
			m_bFullSort = true;
			if( chBoxScanSubDir.Checked ) {
				m_bScanSubDirs = true;
			} else {
				m_bScanSubDirs = false;
			}
			
			m_sLineTemplate = txtBoxTemplatesFromLine.Text.Trim();
			m_sMessTitle = "SharpFBTools - Полная Сортировка";
			// проверка на корректность данных папок источника и приемника файлов
			if( !IsFolderdataCorrect( tboxSourceDir, tboxSortAllToDir ) ) {
				return;
			}
			// проверка на наличие архиваторов
			if( !IsArchivatorsExist() ) {
				return;
			}
			// проверки на корректность шаблонных строк
			if( !IsLineTemplateCorrect( m_sLineTemplate ) ) {
				return;
			}
			
			// инициализация контролов
			Init();
			SetFullSortingStartEnabled( false );

			m_dtStart = DateTime.Now;
			tsslblProgress.Text = "Создание списка файлов:";
			
			// Запуск процесса DoWork от Бекграунд Воркера
			if( m_bw.IsBusy != true ) {
				//если не занят то запустить процесс
            	m_bw.RunWorkerAsync();
			}
		}
		
		void BtnSortAllToDirClick(object sender, EventArgs e)
		{
			// задание папки-приемника для размешения отсортированных файлов
			filesWorker.OpenDirDlg( tboxSortAllToDir, fbdScanDir, "Укажите папку-приемник для размешения отсортированных файлов:" );
		}
		
		void TboxSourceDirTextChanged(object sender, EventArgs e)
		{
			Settings.SettingsFM.FMDataScanDir = tboxSourceDir.Text;
		}
		
		void TboxSortAllToDirTextChanged(object sender, EventArgs e)
		{
			Settings.SettingsFM.FMDataTargetDir = tboxSortAllToDir.Text;
		}
		
		void TxtBoxTemplatesFromLineTextChanged(object sender, EventArgs e)
		{
			Settings.SettingsFM.FMDataTemplate = txtBoxTemplatesFromLine.Text;
		}
		
		void TboxSSSourceDirTextChanged(object sender, EventArgs e)
		{
			Settings.SettingsFM.FMDataSSScanDir = tboxSSSourceDir.Text;
		}
		
		void TboxSSToDirTextChanged(object sender, EventArgs e)
		{
			Settings.SettingsFM.FMDataSSTargetDir = tboxSSToDir.Text;
		}
		
		void TxtBoxSSTemplatesFromLineTextChanged(object sender, EventArgs e)
		{
			Settings.SettingsFM.FMDataSSTemplate = txtBoxSSTemplatesFromLine.Text;
		}
		
		void BtnInsertTemplatesClick(object sender, EventArgs e)
		{
			// запуск диалога Вставки готовых шаблонов
			Core.BookSorting.BasiclTemplates btfrm = new Core.BookSorting.BasiclTemplates();
			btfrm.ShowDialog();
			if( btfrm.GetTemplateLine()!=null ) {
				txtBoxTemplatesFromLine.Text = btfrm.GetTemplateLine();
			}
			btfrm.Dispose();
		}
		
		void BtnSSGetDataClick(object sender, EventArgs e)
		{
			// запуск диалога Сбора данных для Избранной Сортировки
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
					string sLang, sLast, sFirst, sMiddle, sNick, sGGroup, sGenre, sSequence, sBTitle, sExactFit;
					DataFM dfm = new DataFM();
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
																			sExactFit, dfm.GenresFB21Scheme ) );
					}
				}
			}
			
			ssdfrm.Dispose();
			#endregion
		}
		
		void BtnSSInsertTemplatesClick(object sender, EventArgs e)
		{
			// запуск диалога Вставки готовых шаблонов
			Core.BookSorting.BasiclTemplates btfrm = new Core.BookSorting.BasiclTemplates();
			btfrm.ShowDialog();
			if( btfrm.GetTemplateLine()!=null ) {
				txtBoxSSTemplatesFromLine.Text = btfrm.GetTemplateLine();
			}
			btfrm.Dispose();
		}
		
		void TsbtnSSOpenDirClick(object sender, EventArgs e)
		{
			// задание папки с fb2-файлами и архивами для сканирования (Избранная Сортировка)
			filesWorker.OpenDirDlg( tboxSSSourceDir, fbdScanDir, "Укажите папку для сканирования с fb2-файлами и архивами (Избранная Сортировка):" );
		}
		
		void TsbtnSSTargetDirClick(object sender, EventArgs e)
		{
			// задание папки-приемника для размешения отсортированных файлов (Избранная Сортировка)
			filesWorker.OpenDirDlg( tboxSSToDir, fbdScanDir, "Укажите папку-приемник для размешения отсортированных файлов (Избранная Сортировка):" );
		}
	
		void TsbtnFullSortStopClick(object sender, EventArgs e)
		{
			// Остановка выполнения процесса Полной Сортировки
			if( m_bw.WorkerSupportsCancellation == true ) {
				m_bw.CancelAsync();
			}
		}
		
		void TsbtnSSSortFilesToClick(object sender, EventArgs e)
		{
			// Избранная Сортировка
			m_bFullSort = false;
			if( chBoxSSScanSubDir.Checked ) {
				m_bScanSubDirs = true;
			} else {
				m_bScanSubDirs = false;
			}
			
			m_sLineTemplate = txtBoxSSTemplatesFromLine.Text.Trim();
			m_sMessTitle = "SharpFBTools - Избранная Сортировка";
			// проверка на корректность данных папок источника и приемника файлов
			if( !IsFolderdataCorrect( tboxSSSourceDir, tboxSSToDir ) ) {
				return;
			}
			// проверка на наличие критериев поиска для Избранной Сортировки
			if( lvSSData.Items.Count == 0 ) {
				MessageBox.Show( "Задайте хоть один критерий для Избранной Сортировки (кнопка \"Собрать данные для Избранной Сортировки\")!", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				btnSSGetData.Focus();
				return;
			}
			// проверка на наличие архиваторов
			if( !IsArchivatorsExist() ) {
				return;
			}
			// проверки на корректность шаблонных строк
			if( !IsLineTemplateCorrect( m_sLineTemplate ) ) {
				return;
			}
			
			// инициализация контролов
			Init();
			SetSelectedSortingStartEnabled( false );

			m_dtStart = DateTime.Now;
			tsslblProgress.Text = "Создание списка файлов:";
			
			// Запуск процесса DoWork от Бекграунд Воркера
			if( m_bw.IsBusy != true ) {
				//если не занят то запустить процесс
            	m_bw.RunWorkerAsync();
			}
		}
		
		void TsbtnSSSortStopClick(object sender, EventArgs e)
		{
			// Остановка выполнения процесса Избранной Сортировки
			if( m_bw.WorkerSupportsCancellation == true ) {
				m_bw.CancelAsync();
			}
		}
		
		void BtnSSDataListSaveClick(object sender, EventArgs e)
		{
			// сохранить список критериев Избранной Сортировки в файл
			string sMessTitle = "SharpFBTools - Избранная Сортировка";
			if( lvSSData.Items.Count == 0 ) {
				MessageBox.Show( "Список данных для Избранной Сортировки пуст.\nЗадайте хоть один критерий Сортировки (кнопка 'Собрать данные для Избранной Сортировки').",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			sfdSaveXMLFile.Filter = "SharpFBTools файлы (*.qss)|*.qss|Все файлы (*.*)|*.*";;
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
		
		void BtnSSDataListLoadClick(object sender, EventArgs e)
		{
			// загрузить список критериев Избранной Сортировки из файла
			sfdOpenXMLFile.Filter = "SharpFBTools файлы (*.qss)|*.qss|Все файлы (*.*)|*.*";
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
																					sExactFit, dfm.GenresFB21Scheme ) );
    						} while( reader.ReadToNextSibling("Item") );
						}
					} catch {
						MessageBox.Show( "Поврежден списка данных для Избранной Сортировки:\n\""+sfdOpenXMLFile.FileName+"\".", "SharpFBTools - Избранная Сортировка", MessageBoxButtons.OK, MessageBoxIcon.Warning );
					} finally {
						reader.Close();
					}
				}
			}
		}
		#endregion

	}
}
