/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 16.03.2009
 * Time: 9:55
 * 
 * License: GPL 2.1
 */

using System;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;
using System.Threading;

using FB2.Description.DocumentInfo;
using StringProcessing;
using FilesWorker;

using System.Text;

namespace SharpFBTools.Tools
{
	/// <summary>
	/// Description of SFBTpArchiveManager.
	/// </summary>
	public partial class SFBTpArchiveManager : UserControl
	{
		#region Закрытые члены-данные класса
		private string m_s7zPath	= Settings.Settings.Read7zaPath().Trim();
		private string m_sRarPath	= Settings.Settings.ReadRarPath().Trim();
		private string m_sUnRarPath	= Settings.Settings.ReadUnRarPath().Trim();
		private BackgroundWorker m_bwa = null;
		private BackgroundWorker m_bwu = null;
		private BackgroundWorker m_bwt = null;
		private long m_lUnpackCount		= 0;
		private DateTime m_dtStart;
		private List<string> m_lFilesList = null;
		private string m_sMessTitle		= "";
		private string m_sSource		= "";
		private string m_sTarget		= "";
		#endregion
		
		public SFBTpArchiveManager()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			InitializeArchiveBackgroundWorker();
			InitializeUnPackBackgroundWorker();
			InitializeAnalyzeBackgroundWorker();
			InitA();	// инициализация контролов (Упаковка)
			InitUA();	// инициализация контролов (Распаковка
			// читаем сохраненные пути к папкам Менеджера Архивов, если они есть
			ReadMADirs();
			cboxExistArchive.SelectedIndex		= 1; // добавление к создаваемому fb2-архиву очередного номера
			cboxArchiveType.SelectedIndex		= 1; // Zip
			cboxUAExistArchive.SelectedIndex	= 1; // добавление к создаваемому fb2-файлу очередного номера
			cboxUAType.SelectedIndex			= 6; // Все архивы
		}
		
		#region Закрытые методы реализации BackgroundWorker
		private void InitializeArchiveBackgroundWorker() {
			// Инициализация перед использование BackgroundWorker Упаковщика
            m_bwa = new BackgroundWorker();
            //m_bwa.WorkerReportsProgress		= true; // Позволить выводить прогресс процесса
            m_bwa.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
            m_bwa.DoWork += new DoWorkEventHandler( bwa_DoWork );
            m_bwa.RunWorkerCompleted += new RunWorkerCompletedEventHandler( bwa_RunWorkerCompleted );
		}
		
		private void InitializeUnPackBackgroundWorker() {
			// Инициализация перед использование BackgroundWorker Распаковщика
            m_bwu = new BackgroundWorker();
            //m_bwu.WorkerReportsProgress		= true; // Позволить выводить прогресс процесса
            m_bwu.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
            m_bwu.DoWork += new DoWorkEventHandler( bwu_DoWork );
            m_bwu.RunWorkerCompleted += new RunWorkerCompletedEventHandler( bwu_RunWorkerCompleted );
		}
		
		private void InitializeAnalyzeBackgroundWorker() {
			// Инициализация перед использование BackgroundWorker Анализатора
            m_bwt = new BackgroundWorker();
            //m_bwt.WorkerReportsProgress		= true; // Позволить выводить прогресс процесса
            m_bwt.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
            m_bwt.DoWork += new DoWorkEventHandler( bwt_DoWork );
            m_bwt.RunWorkerCompleted += new RunWorkerCompletedEventHandler( bwt_RunWorkerCompleted );
		}

		private void bwa_DoWork( object sender, DoWorkEventArgs e ) {
			// упаковка файлов в архивы
			m_dtStart = DateTime.Now;
			// сортированный список всех вложенных папок
			List<string> lDirList = new List<string>();
			if( !cboxScanSubDirToArchive.Checked ) {
				// сканировать только указанную папку
				lDirList.Add( m_sSource );
				lvGeneralCount.Items[0].SubItems[1].Text = "1";
			} else {
				// сканировать и все подпапки
				tsslblProgress.Text = "Создание списка папок:";
				lDirList = FilesWorker.FilesWorker.DirsParser( m_sSource, lvGeneralCount, false );
			}
			
			// сортированный список всех файлов
			tsslblProgress.Text = "Создание списка файлов:";
			m_lFilesList = FilesWorker.FilesWorker.AllFilesParser( m_bwa, e, lDirList, lvGeneralCount, tsProgressBar, false );
			lDirList.Clear();
			if( ( m_bwa.CancellationPending == true ) )  {
				e.Cancel = true; // Выставить окончание - по отмене, сработает событие Bw_RunWorkerCompleted
				return;
			}
			
			// проверка, есть ли хоть один файл в папке для сканирования
			if( m_lFilesList.Count == 0 ) {
				MessageBox.Show( "Не найдено ни одного файла!\nРабота прекращена.", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				InitA();
				return;
			}

			tsslblProgress.Text = "Упаковка найденных файлов в " + cboxArchiveType.Text + ":";
			tsProgressBar.Maximum = m_lFilesList.Count+1;
			tsProgressBar.Value = 1;
			BackgroundWorker bw = sender as BackgroundWorker;
			if( cboxArchiveType.SelectedIndex == 0 ) {
				FileToArchive( bw, e, m_sRarPath, m_lFilesList, false, tsProgressBar );// rar
			} else {
				FileToArchive( bw, e, m_s7zPath, m_lFilesList, true, tsProgressBar );	// zip, 7z...
			}
        }
     
        private void bwa_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {   
            // Проверяем это отмена, ошибка, или конец задачи и сообщить
            DateTime dtEnd = DateTime.Now;
            m_lFilesList.Clear();
            FilesWorker.FilesWorker.RemoveDir( Settings.Settings.GetTempDir() );
            
            string sTime = dtEnd.Subtract( m_dtStart ).ToString() + " (час.:мин.:сек.)";
			string sMessCanceled	= "Упаковка fb2-файлов основлена!\nЗатрачено времени: "+sTime;
			string sMessError		= "";
			string sMessDone		= "Упаковка fb2-файлов завершена!\nЗатрачено времени: "+sTime;
           
			if( ( e.Cancelled == true ) ) {
                MessageBox.Show( sMessCanceled, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
            } else if( e.Error != null ) {
                sMessError = "Error!\n" + e.Error.Message + "\n" + e.Error.StackTrace + "\nЗатрачено времени: "+sTime;
            	MessageBox.Show( sMessError, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
            } else {
            	MessageBox.Show( sMessDone, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
            }
			
			tsslblProgress.Text = Settings.Settings.GetReady();
			SetArhivingStartEnabled( true );
            m_bwa.Dispose();
        }
		
		private void bwu_DoWork( object sender, DoWorkEventArgs e ) {
			// распаковка архивов в файлы
			// сортированный список всех вложенных папок
			List<string> lDirList = new List<string>();
			if( !cboxScanSubDirToUnArchive.Checked ) {
				// сканировать только указанную папку
				lDirList.Add( m_sSource );
				lvUAGeneralCount.Items[0].SubItems[1].Text = "1";
			} else {
				// сканировать и все подпапки
				tsslblProgress.Text = "Создание списка папок:";
				lDirList = FilesWorker.FilesWorker.DirsParser( m_sSource, lvUAGeneralCount, false );
			}
			
			// сортированный список всех файлов
			tsslblProgress.Text = "Создание списка файлов:";
			m_lFilesList = FilesWorker.FilesWorker.AllFilesParser( m_bwu, e, lDirList, lvGeneralCount, tsProgressBar, false );
			lDirList.Clear();
			if( ( m_bwu.CancellationPending == true ) )  {
				e.Cancel = true; // Выставить окончание - по отмене, сработает событие Bw_RunWorkerCompleted
				return;
			}
			
			// проверка, есть ли хоть один файл в папке для сканирования
			if( m_lFilesList.Count == 0 ) {
				MessageBox.Show( "В указанной папке не найдено ни одного файла!", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				InitUA();
				return;
			}
			
			tsslblProgress.Text = "Распаковка архивов:";
			tsProgressBar.Maximum = m_lFilesList.Count+1;
			tsProgressBar.Value = 1;
			BackgroundWorker bw = sender as BackgroundWorker;
			m_lUnpackCount = ArchivesToFile( bw, e, m_lFilesList, m_sTarget, tsProgressBar );
		}
		
		private void bwu_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {   
			// Проверяем это отмена, ошибка, или конец задачи и сообщить
			DateTime dtEnd = DateTime.Now;
            m_lFilesList.Clear();
            FilesWorker.FilesWorker.RemoveDir( Settings.Settings.GetTempDir() );
            
            string sTime = dtEnd.Subtract( m_dtStart ).ToString() + " (час.:мин.:сек.)";
			string sMessCanceled	= "Распаковка архивов в fb2-файлы основлена!\nЗатрачено времени: "+sTime;
			string sMessError		= "";
			string sMessDone		= "";
			if( m_lUnpackCount > 0 ) {
				sMessDone	= "Распаковка архивов в fb2-файлы завершена!\nЗатрачено времени: "+sTime;
			} else {
				sMessDone	= "В папке для сканирования не найдено ни одного архива указанного типа!\nРаспаковка не произведена."+sTime;
			}
			
			if( ( e.Cancelled == true ) ) {
                MessageBox.Show( sMessCanceled, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
            } else if( e.Error != null ) {
                sMessError = "Error!\n" + e.Error.Message + "\n" + e.Error.StackTrace + "\nЗатрачено времени: "+sTime;
            	MessageBox.Show( sMessError, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
            } else {
            	MessageBox.Show( sMessDone, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
            }
			
			tsslblProgress.Text = Settings.Settings.GetReady();
			SetUnPackingStartEnabled( true );
            m_bwu.Dispose();
		}
		
		private void bwt_DoWork( object sender, DoWorkEventArgs e ) {
			// Анализ файлов в папке на определение числа и типа архивов
			// сортированный список всех вложенных папок
			List<string> lDirList = new List<string>();
			if( !cboxScanSubDirToUnArchive.Checked ) {
				// сканировать только указанную папку
				lDirList.Add( m_sSource );
				lvUAGeneralCount.Items[0].SubItems[1].Text = "1";
			} else {
				// сканировать и все подпапки
				tsslblProgress.Text = "Создание списка папок:";
				lDirList = FilesWorker.FilesWorker.DirsParser( m_sSource, lvUAGeneralCount, false );
			}

			// сортированный список всех файлов
			tsslblProgress.Text = "Создание списка файлов:";
			m_lFilesList = FilesWorker.FilesWorker.AllFilesParser( m_bwt, e, lDirList, lvGeneralCount, tsProgressBar, false );
			lDirList.Clear();
			if( ( m_bwt.CancellationPending == true ) )  {
				e.Cancel = true; // Выставить окончание - по отмене, сработает событие Bw_RunWorkerCompleted
				return;
			}
			
			// проверка, есть ли хоть один файл в папке для сканирования
			if( m_lFilesList.Count == 0 ) {
				MessageBox.Show( "В указанной папке не найдено ни одного файла!", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				InitUA();
				return;
			}

			tsslblProgress.Text = "Анализ файлов на наличие архивов:";
			tsProgressBar.Maximum = m_lFilesList.Count+1;
			tsProgressBar.Value = 1;

			long lRar, lZip, l7Z, lBZip2, lGZip, lTar;
			lRar = lZip = l7Z = lBZip2 = lGZip = lTar = 0;
			string sExt = "";
			foreach( string sFile in m_lFilesList ) {
				// Проверить флаг на остановку процесса 
				if( ( m_bwt.CancellationPending == true ) ) {
					e.Cancel = true; // Выставить окончание - по отмене, сработает событие bwt_RunWorkerCompleted
					break;
				} else {
					sExt = Path.GetExtension( sFile );
					if( sExt.ToLower() == ".rar" )
						lvUACount.Items[0].SubItems[1].Text = (++lRar).ToString();
					else if( sExt.ToLower() == ".zip" )
						lvUACount.Items[1].SubItems[1].Text = (++lZip).ToString();
					else if( sExt.ToLower() == ".7z" )	
						lvUACount.Items[2].SubItems[1].Text = (++l7Z).ToString();
					else if( sExt.ToLower() == ".bz2" )	
						lvUACount.Items[3].SubItems[1].Text = (++lBZip2).ToString();
					else if( sExt.ToLower() == ".gz" )	
						lvUACount.Items[4].SubItems[1].Text = (++lGZip).ToString();
					else if( sExt.ToLower() == ".tar" )
						lvUACount.Items[5].SubItems[1].Text = (++lTar).ToString();
					++tsProgressBar.Value;
				}
			}
		}
		
		private void bwt_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {   
			// Проверяем это отмена, ошибка, или конец задачи и сообщить
			DateTime dtEnd = DateTime.Now;
            m_lFilesList.Clear();
            
            string sTime = dtEnd.Subtract( m_dtStart ).ToString() + " (час.:мин.:сек.)";
			string sMessCanceled	= "Анализ имеющихся файлов основлен!\nЗатрачено времени: "+sTime;
			string sMessError		= "";
			string sMessDone		= "Анализ имеющихся файлов завершена!\nЗатрачено времени: ";
			
			if( ( e.Cancelled == true ) ) {
                MessageBox.Show( sMessCanceled, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
            } else if( e.Error != null ) {
                sMessError = "Error!\n" + e.Error.Message + "\n" + e.Error.StackTrace + "\nЗатрачено времени: "+sTime;
            	MessageBox.Show( sMessError, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
            } else {
            	MessageBox.Show( sMessDone, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
            }
			
			tsslblProgress.Text = Settings.Settings.GetReady();
			SetAnalyzingStartEnabled( true );
            m_bwt.Dispose();
		}
		#endregion
		
		#region Закрытые Общие Вспомогательны методы класса
		private void InitA() {
			// инициализация контролов и переменных  (Упаковка)
			for( int i=0; i!=lvGeneralCount.Items.Count; ++i ) {
				lvGeneralCount.Items[i].SubItems[1].Text = "0";
			}
			tsProgressBar.Value		= 1;
			tsslblProgress.Text		= Settings.Settings.GetReady();
			tsProgressBar.Visible	= false;
		}
		
		private void InitUA() {
			// инициализация контролов и переменных  (Распаковка)
			for( int i=0; i!=lvUAGeneralCount.Items.Count; ++i ) {
				lvUAGeneralCount.Items[i].SubItems[1].Text = "0";
			}
			for( int i=0; i!=lvUACount.Items.Count; ++i ) {
				lvUACount.Items[i].SubItems[1].Text = "0";
			}
			tsProgressBar.Value		= 1;
			tsslblProgress.Text		= Settings.Settings.GetReady();
			tsProgressBar.Visible	= false;
			m_lUnpackCount			= 0;
		}
		
		private void SetArhivingStartEnabled( bool bEnabled ) {
			// доступность контролов при Упаковке
			tsbtnOpenDir.Enabled	= bEnabled;
			tsbtnArchive.Enabled	= bEnabled;
			pScanDir.Enabled		= bEnabled;
			pType.Enabled			= bEnabled;
			gboxOptions.Enabled		= bEnabled;
			tpUnArchive.Enabled		= bEnabled;
			tsbtnArchiveStop.Enabled	= !bEnabled;
			tsProgressBar.Visible		= !bEnabled;
			tcArchiver.Refresh();
			ssProgress.Refresh();
		}
		private void SetUnPackingStartEnabled( bool bEnabled ) {
			// доступность контролов при Распаковке
			tsbtnUAOpenDir.Enabled	= bEnabled;
			tsbtnUAAnalyze.Enabled	= bEnabled;
			tsbtnUnArchive.Enabled	= bEnabled;
			pUAScanDir.Enabled		= bEnabled;
			pUAType.Enabled			= bEnabled;
			gboxUAOptions.Enabled	= bEnabled;
			tpArchive.Enabled		= bEnabled;
			tsbtnUnArchiveStop.Enabled	= !bEnabled;
			tsProgressBar.Visible		= !bEnabled;
			tcArchiver.Refresh();
			ssProgress.Refresh();
		}
		
		private void SetAnalyzingStartEnabled( bool bEnabled ) {
			// доступность контролов при Анализе
			tsbtnUAOpenDir.Enabled	= bEnabled;
			tsbtnUAAnalyze.Enabled	= bEnabled;
			tsbtnUnArchive.Enabled	= bEnabled;
			pUAScanDir.Enabled		= bEnabled;
			pUAType.Enabled			= bEnabled;
			gboxUAOptions.Enabled	= bEnabled;
			tpArchive.Enabled		= bEnabled;
			tsbtnUAAnalyzeStop.Enabled	= !bEnabled;
			tsProgressBar.Visible		= !bEnabled;
			tcArchiver.Refresh();
			ssProgress.Refresh();
		}
		
		private bool IsSourceDirCorrect( string sSource, DirectoryInfo diFolder ) {
			// проверки папки для сканирования
			if( sSource.Length == 0 ) {
				MessageBox.Show( "Выберите папку для сканирования!", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка для сканирования не найдена: " + sSource, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			return true;
		}
		
		private bool IsTargetDirCorrect( string sTarget, bool bToAnotherDir ) {
			// проверки папки-приемника
			if( bToAnotherDir ) {
				if( sTarget.Length == 0 ) {
					MessageBox.Show( "Не задана папка-приемник архивов!", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return false;
				} else {
					DirectoryInfo df = new DirectoryInfo( sTarget );
					if( !df.Exists ) {
						MessageBox.Show( "Папка-приемник не найдена: " + sTarget, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
						return false;
					}
				}
			}
			return true;
		}
		
		private bool IsArchivatorsPathCorrectForArchive( string s7zPath, string sRarPath ) {
			// проверка на наличие архиваторов и корректность путей к ним
			if( cboxArchiveType.SelectedIndex==0 && sRarPath.Length==0 ) {
				MessageBox.Show( "Не указана папка с установленным консольным Rar-архиватором!\nУкажите путь к нему в Настройках.\nРабота остановлена!",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на наличие архиваторов
			if( cboxArchiveType.SelectedIndex == 0 ) {
				if( !File.Exists( sRarPath ) ) {
					MessageBox.Show( "Не найден файл консольного Rar-архиватора "+sRarPath+"!\nУкажите путь к нему в Настройках.\nРабота остановлена!",
					                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return false;
				}
			} else {
				if( !File.Exists( s7zPath ) ) {
					MessageBox.Show( "Не найден файл Zip-архиватора \""+s7zPath+"\"!\nУкажите путь к нему в Настройках.\nРабота остановлена.",
					                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return false;
				}
			}
			return true;
		}
		
		private bool IsArchivatorsPathCorrectForUnArchive( string s7zPath, string sUnRarPath ) {
			// проверка на наличие архиваторов и корректность путей к ним
			if( s7zPath.Trim().Length == 0 ) {
				MessageBox.Show( "В Настройках не указана папка с установленным консольным 7z(a).exe!", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			if( sUnRarPath.Trim().Length == 0 ) {
				MessageBox.Show( "В Настройках не указана папка с установленным консольным UnRar.exe!", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			
			if( !File.Exists( s7zPath ) ) {
				MessageBox.Show( "Не найден файл Zip-архиватора \""+s7zPath+"\"!\nУкажите путь к нему в Настройках.\nРабота остановлена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			if( !File.Exists( sUnRarPath ) ) {
				MessageBox.Show( "Не найден файл консольного UnRar-распаковщика \""+sUnRarPath+"\"!\nУкажите путь к нему в Настройках.\nРабота остановлена!",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			return true;
		}
		
		private string ExistsFB2FileFileToDirWorker( string sFromFile, string sNewFile, string sSufix ) {
			// Обработка существующих в папке-приемнике файлов при копировании
			if( File.Exists( sNewFile ) ) {
				if( cboxUAExistArchive.SelectedIndex==0 ) {
					File.Delete( sNewFile );
				} else {
					if( chBoxAddFileNameBookID.Checked ) {
						try {
							sSufix = "_" + StringProcessing.StringProcessing.GetBookID( sFromFile );
						} catch { }
					}
					if( cboxUAExistArchive.SelectedIndex == 1 ) {
						// Добавить к создаваемому файлу очередной номер
						sSufix += "_" + StringProcessing.StringProcessing.GetFileNewNumber( sNewFile ).ToString();
					} else {
						// Добавить к создаваемому файлу дату и время
						sSufix += "_" + StringProcessing.StringProcessing.GetDateTimeExt();
					}
					sNewFile = sNewFile.Remove( sNewFile.Length-4 ) + sSufix + ".fb2";
				}
			}
			return sNewFile;
		}
		
		private bool FileToDir( string sFile, string sArchiveFile, string sTargetDir, bool bFB2 ) {
			// Переместить в папку
			// bFB2=true - копировать только fb2-файлы. false - любые
			if( bFB2 && Path.GetExtension( sFile ).ToLower() != ".fb2" ) return false;
			
			Regex rx = new Regex( @"\\+" );
			sFile = rx.Replace( sFile, "\\" );
			sArchiveFile = rx.Replace( sArchiveFile, "\\" );
			
			string sFileSourceDir = tboxUASourceDir.Text.Trim();
			string sNewDir = Path.GetDirectoryName( sTargetDir+"\\"+sArchiveFile.Remove( 0, sFileSourceDir.Length ) );
			string sNewFile = "";
			string sSufix = ""; // для добавления к имени нового файла суфикса
			string sFromFile = Settings.Settings.GetTempDir() + "\\" + sFile;
			if( rbtnUAToSomeDir.Checked ) {
				// файл - в ту же папку, где и исходный архив
				sNewFile = Path.GetDirectoryName( sArchiveFile )+"\\"+sFile;
				// Обработка существующих в папке-приемнике файлов при копировании
				sNewFile = ExistsFB2FileFileToDirWorker( sFromFile, sNewFile, sSufix );
			} else {
				// файл - в другую папку
				sNewFile = sNewDir + "\\" + sFile;
				FileInfo fi = new FileInfo( sNewFile );
				if( !fi.Directory.Exists ) {
					Directory.CreateDirectory( fi.Directory.ToString() );
				}
				// Обработка существующих в папке-приемнике файлов при копировании
				sNewFile = ExistsFB2FileFileToDirWorker( sFromFile, sNewFile, sSufix );
			}
			File.Move( Settings.Settings.GetTempDir()+"\\"+sFile, sNewFile );
			return true;
		}
		
		private bool DeleteSourceFileIsNeeds( string sFile, bool bIsDelete ) {
			if( bIsDelete ) {
				// удаляем исходный файл, если задана опция
				if( File.Exists( sFile ) ) {
					File.Delete( sFile );
					return true;
				}
			}
			return false;
		}
		
		private void ReadMADirs() {
			// чтение путей к папкам Менеджера Архивов из xml-файла
			string sSettings = Settings.Settings.WorksDataSettingsPath;
			if( !File.Exists( sSettings ) ) return;
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.IgnoreWhitespace = true;
			using ( XmlReader reader = XmlReader.Create( sSettings, settings ) ) {
				reader.ReadToFollowing("AMScanDirForArchive");
				if (reader.HasAttributes ) {
					tboxSourceDir.Text = reader.GetAttribute("tboxSourceDir");
					Settings.SettingsAM.AMAScanDir =  tboxSourceDir.Text.Trim();
				}
				reader.ReadToFollowing("AMTargetDirForArchive");
				if (reader.HasAttributes ) {
					tboxToAnotherDir.Text = reader.GetAttribute("tboxToAnotherDir");
					Settings.SettingsAM.AMATargetDir = tboxToAnotherDir.Text.Trim();
				}
				reader.ReadToFollowing("AMScanDirForUnArchive");
				if (reader.HasAttributes ) {
					tboxUASourceDir.Text = reader.GetAttribute("tboxUASourceDir");
					Settings.SettingsAM.AMUAScanDir = tboxUASourceDir.Text.Trim();
				}
				reader.ReadToFollowing("AMTargetDirForUnArchive");
				if (reader.HasAttributes ) {
					tboxUAToAnotherDir.Text = reader.GetAttribute("tboxUAToAnotherDir");
					Settings.SettingsAM.AMAUATargetDir = tboxUAToAnotherDir.Text.Trim();
				}
				reader.Close();
			}
		}
		#endregion
		
		#region Архивация
		private void FileToArchive( BackgroundWorker bw, DoWorkEventArgs e,
		                           string sArchPath, List<string> lFilesList, bool bZip,
		                           ToolStripProgressBar pBar ) {
			// упаковка fb2-файлов в .fb2.??? - где ??? - тип архива (задается в cboxArchiveType)
			#region Код
			long lFB2 = 0; string sExt = "";
			foreach( string sFile in lFilesList ) {
				// Проверить флаг на остановку процесса 
				if( ( bw.CancellationPending == true ) ) {
					e.Cancel = true; // Выставить окончание - по отмене, сработает событие bwa_RunWorkerCompleted
					break;
				} else {
					sExt = Path.GetExtension( sFile );
					if( sExt.ToLower() == ".fb2" ) {
						lvGeneralCount.Items[2].SubItems[1].Text = (++lFB2).ToString();
						// упаковываем
						string sArchiveFile = "";
						string sDotExt = "."+StringProcessing.StringProcessing.GetArchiveExt( cboxArchiveType.Text );
						string sSufix = ""; // для добавления к имени нового архива суфикса
						if( rbtnToSomeDir.Checked ) {
							// создаем архив в той же папке, где и исходный fb2-файл
							sArchiveFile = sFile + sDotExt;
							if( File.Exists( sArchiveFile ) ) {
								if( cboxExistArchive.SelectedIndex==0 ) {
									File.Delete( sArchiveFile );
								} else {
									if( chBoxAddArchiveNameBookID.Checked ) {
										try {
											sSufix = "_" + StringProcessing.StringProcessing.GetBookID( sFile );
										} catch { }
									}
									if( cboxExistArchive.SelectedIndex == 1 ) {
										// Добавить к создаваемому файлу очередной номер
										sSufix += "_" + StringProcessing.StringProcessing.GetFileNewNumber( sFile ).ToString();
									} else {
										// Добавить к создаваемому файлу дату и время
										sSufix += "_" + StringProcessing.StringProcessing.GetDateTimeExt();
									}
									sArchiveFile = sFile.Remove( sFile.Length-4 ) + sSufix + ".fb2" + sDotExt;
								}
							}
						} else {
							// создаем архив в другой папке
							string sNewFilePath = sFile.Remove( 0, m_sSource.Length );
							sArchiveFile = m_sTarget + sNewFilePath + sDotExt;
							FileInfo fi = new FileInfo( sArchiveFile );
							if( !fi.Directory.Exists ) {
								Directory.CreateDirectory( fi.Directory.ToString() );
							}
							if( File.Exists( sArchiveFile ) ) {
								if( cboxExistArchive.SelectedIndex==0 ) {
									File.Delete( sArchiveFile );
								} else {
									if( chBoxAddArchiveNameBookID.Checked ) {
										try {
											sSufix = "_" + StringProcessing.StringProcessing.GetBookID( sFile );
										} catch { }
									}
									if( cboxExistArchive.SelectedIndex == 1 ) {
										// Добавить к создаваемому файлу очередной номер
										sSufix += "_" + StringProcessing.StringProcessing.GetFileNewNumber( sArchiveFile ).ToString();
									} else {
										// Добавить к создаваемому файлу дату и время
										sSufix += "_" + StringProcessing.StringProcessing.GetDateTimeExt();
									}
									sArchiveFile = m_sTarget + sNewFilePath.Remove( sNewFilePath.Length-4 ) + sSufix + ".fb2" + sDotExt;
								}
							}
						}
						if( bZip ) {
							FilesWorker.Archiver.zip( sArchPath, cboxArchiveType.Text.ToLower(), sFile, sArchiveFile );
						} else {
							FilesWorker.Archiver.rar( sArchPath, sFile, sArchiveFile, cboxAddRestoreInfo.Checked );
						}
					
						// удаляем исходный файл, если задана опция
						DeleteSourceFileIsNeeds( sFile, cboxDelFB2Files.Checked );
					}
					++pBar.Value;
				}
			}
			#endregion
		}
		#endregion
				
		#region Распаковка
		private long AllArchivesToFile( BackgroundWorker bw, DoWorkEventArgs e,
		                               List<string> lFilesList, string sMoveToDir, ToolStripProgressBar pBar ) {
			// Распаковать все архивы
			long lCount, lFB2, lRar, lZip, l7Z, lBZip2, lGZip, lTar;
			lCount = lFB2 = lRar = lZip = l7Z = lBZip2 = lGZip = lTar = 0;
			string sTempDir	= Settings.Settings.GetTempDir();
			string sExt = "";
			foreach( string sFile in lFilesList ) {
				if( ( bw.CancellationPending == true ) ) {
					e.Cancel = true; // Выставить окончание - по отмене, сработает событие Bw_RunWorkerCompleted
					break;
				} else {
					sExt = Path.GetExtension( sFile );
					if( sExt.ToLower() != "" ) {
						FilesWorker.FilesWorker.RemoveDir( sTempDir );
						//TODO: заменить все unrar на unzip
						switch( sExt.ToLower() ) {
							case ".rar":
								FilesWorker.Archiver.unrar( m_sUnRarPath, sFile, sTempDir );
								lvUACount.Items[0].SubItems[1].Text = (++lRar).ToString();
								++lCount;
								// удаление исходного архива, если включена опция
								DeleteSourceFileIsNeeds( sFile, cboxUADelFB2Files.Checked );
								break;
							case ".zip":
								FilesWorker.Archiver.unzip( m_s7zPath, sFile, sTempDir );
								lvUACount.Items[1].SubItems[1].Text = (++lZip).ToString();
								++lCount;
								// удаление исходного архива, если включена опция
								DeleteSourceFileIsNeeds( sFile, cboxUADelFB2Files.Checked );
								break;
							case ".7z":
								FilesWorker.Archiver.unzip( m_s7zPath, sFile, sTempDir );
								lvUACount.Items[2].SubItems[1].Text = (++l7Z).ToString();
								++lCount;
								// удаление исходного архива, если включена опция
								DeleteSourceFileIsNeeds( sFile, cboxUADelFB2Files.Checked );
								break;
							case ".bz2":
								FilesWorker.Archiver.unzip( m_s7zPath, sFile, sTempDir );
								lvUACount.Items[3].SubItems[1].Text = (++lBZip2).ToString();
								++lCount;
								// удаление исходного архива, если включена опция
								DeleteSourceFileIsNeeds( sFile, cboxUADelFB2Files.Checked );
								break;
							case ".gz":
								FilesWorker.Archiver.unzip( m_s7zPath, sFile, sTempDir );
								lvUACount.Items[4].SubItems[1].Text = (++lGZip).ToString();
								++lCount;
								// удаление исходного архива, если включена опция
								DeleteSourceFileIsNeeds( sFile, cboxUADelFB2Files.Checked );
								break;
							case ".tar":
								FilesWorker.Archiver.unzip( m_s7zPath, sFile, sTempDir );
								lvUACount.Items[5].SubItems[1].Text = (++lTar).ToString();
								++lCount;
								// удаление исходного архива, если включена опция
								DeleteSourceFileIsNeeds( sFile, cboxUADelFB2Files.Checked );
								break;
						}
						if( Directory.Exists( sTempDir ) ) {
							lvUAGeneralCount.Items[2].SubItems[1].Text = (lCount).ToString();
							string [] files = Directory.GetFiles( sTempDir );
							foreach( string sFB2File in files ) {
								string sFileName = Path.GetFileName( sFB2File );
								if( Path.GetExtension( sFileName ).ToLower()==".fb2" ) {
									lvUAGeneralCount.Items[3].SubItems[1].Text = (++lFB2).ToString();
								}
								if( FileToDir( sFileName, sFile, sMoveToDir, false ) ) {
								} else {
									File.Delete( sFileName );
								}
							}
						}
					}
					++pBar.Value;
				}
			}
			return lCount;
		}
		
		private long TypeArchToFile( BackgroundWorker bw, DoWorkEventArgs e,
		                            List<string> lFilesList, string sMoveToDir, ToolStripProgressBar pBar,
		                   			string sExt, int nArchCountItem, int nFB2CountItem ) {
			// Распаковать выбранный тип ахрива
			long lCount, lAllArchive, lFB2;
			lCount = lAllArchive = lFB2 = 0;
			string sTempDir = Settings.Settings.GetTempDir();
			foreach( string sFile in lFilesList ) {
				if( ( bw.CancellationPending == true ) ) {
					e.Cancel = true; // Выставить окончание - по отмене, сработает событие Bw_RunWorkerCompleted
					break;
				} else {
					if( Path.GetExtension( sFile.ToLower() ) == sExt ) {
						FilesWorker.Archiver.unzip( m_s7zPath, sFile, sTempDir );
						lvUAGeneralCount.Items[2].SubItems[1].Text = (++lAllArchive).ToString();
						lvUACount.Items[nArchCountItem].SubItems[1].Text = (++lCount).ToString();
						if( Directory.Exists( sTempDir ) ) {
							string [] files = Directory.GetFiles( sTempDir );
							foreach( string sFB2File in files ) {
								string sFileName = Path.GetFileName( sFB2File );
								if( Path.GetExtension( sFileName ).ToLower()==".fb2" ) {
									lvUAGeneralCount.Items[nFB2CountItem].SubItems[1].Text = (++lFB2).ToString();
								}
								if( FileToDir( sFileName, sFile, sMoveToDir, false ) ) {
								} else {
									File.Delete( sFileName );
								}
							}
						}
						// удаление исходного архива, если включена опция
						DeleteSourceFileIsNeeds( sFile, cboxUADelFB2Files.Checked );
					}
					++pBar.Value;
				}
			}
			return lCount;
		}
		
		private long ArchivesToFile( BackgroundWorker bw, DoWorkEventArgs e,
		                            List<string> lFilesList, string sMoveToDir, ToolStripProgressBar pBar ) {
			// Распаковать ахривы
			string sArchType = StringProcessing.StringProcessing.GetArchiveExt( cboxUAType.Text );
			long lAllArchive, lRar, lFB2, lCount;
			lAllArchive = lRar = lFB2 = lCount = 0;
			string sTempDir = Settings.Settings.GetTempDir();
			FilesWorker.FilesWorker.RemoveDir( sTempDir );
			string sExt = "";
			switch( sArchType.ToLower() ) {
				case "":
					lCount = AllArchivesToFile( bw, e, lFilesList, sMoveToDir, pBar );
					break;
				case "rar":
					foreach( string sFile in lFilesList ) {
						if( ( bw.CancellationPending == true ) ) {
							e.Cancel = true; // Выставить окончание - по отмене, сработает событие Bw_RunWorkerCompleted
							break;
						} else {
							sExt = Path.GetExtension( sFile );
							if( sExt.ToLower() == ".rar" ) {
								//TODO: заменить все unrar на unzip
								FilesWorker.Archiver.unrar( m_sUnRarPath, sFile, sTempDir );
								lvUAGeneralCount.Items[2].SubItems[1].Text = (++lAllArchive).ToString();
								lvUACount.Items[0].SubItems[1].Text = (++lRar).ToString();
								if( Directory.Exists( sTempDir ) ) {
									string [] files = Directory.GetFiles( sTempDir );
									foreach( string sFB2File in files ) {
										string sFileName = Path.GetFileName( sFB2File );
										if( Path.GetExtension( sFileName ).ToLower()==".fb2" ) {
											lvUAGeneralCount.Items[3].SubItems[1].Text = (++lFB2).ToString();
										}
										if( FileToDir( sFileName, sFile, sMoveToDir, false ) ) {
										}  else {
											File.Delete( sFileName );
										}
									}
								}
								// удаление исходного архива, если включена опция
								DeleteSourceFileIsNeeds( sFile, cboxUADelFB2Files.Checked );
							}
							++pBar.Value;
							lCount = lRar;
						}
					}
					break;
				case "zip":
					lCount = TypeArchToFile( bw, e, lFilesList, sMoveToDir, pBar, ".zip", 1, 3 );
					break;
				case "7z":
					lCount = TypeArchToFile( bw, e, lFilesList, sMoveToDir, pBar, ".7z", 2, 3 );
					break;
				case "bz2":
					lCount = TypeArchToFile( bw, e, lFilesList, sMoveToDir, pBar, ".bz2", 3, 3 );
					break;
				case "gz":
					lCount = TypeArchToFile( bw, e, lFilesList, sMoveToDir, pBar, ".gz", 4, 3 );
					break;
				case "tar":
					lCount = TypeArchToFile( bw, e, lFilesList, sMoveToDir, pBar, ".tar", 5, 3 );
					break;
			}
			return lCount;
		}
		#endregion
		
		#region Обработчики событий
		void TsbtnOpenDirClick(object sender, EventArgs e)
		{
			// задание папки с fb2-файлами для сканирования (Архивация)
			if( FilesWorker.FilesWorker.OpenDirDlg( tboxSourceDir, fbdDir, "Укажите папку с fb2-файлами для Упаковки" ) ) {
				InitA();
			}
		}
		void BtnToAnotherDirClick(object sender, EventArgs e)
		{
			// задание папки для копирования запакованных fb2-файлов
			FilesWorker.FilesWorker.OpenDirDlg( tboxToAnotherDir, fbdDir, "Укажите папку для размещения упакованных fb2-файлов" );
		}
		void TsbtnUAOpenDirClick(object sender, EventArgs e)
		{
			// задание папки с fb2-архивами для сканирования (Распаковка)
			if( FilesWorker.FilesWorker.OpenDirDlg( tboxUASourceDir, fbdDir, "Укажите папку с fb2-архивами для Распаковки" ) ) {
				InitUA();
			}
		}
		void BtnUAToAnotherDirClick(object sender, EventArgs e)
		{
			// задание папки для копирования распакованных файлов
			FilesWorker.FilesWorker.OpenDirDlg( tboxUAToAnotherDir, fbdDir, "Укажите папку для размещения распакованных файлов" );
		}
		
		void RbtnToAnotherDirCheckedChanged(object sender, EventArgs e)
		{
			btnToAnotherDir.Enabled = rbtnToAnotherDir.Checked;
			tboxToAnotherDir.ReadOnly = !rbtnToAnotherDir.Checked;
			if( rbtnToAnotherDir.Checked ) {
				tboxToAnotherDir.Focus();
			}
		}
		
		void CboxArchiveTypeSelectedIndexChanged(object sender, EventArgs e)
		{
			cboxAddRestoreInfo.Visible = cboxArchiveType.SelectedIndex == 0;
		}
		
		void TsbtnArchiveClick(object sender, EventArgs e)
		{
			// Запаковка fb2-файлов
			m_sMessTitle = "SharpFBTools - Упаковка в архивы";
			m_sSource = tboxSourceDir.Text.Trim();
			m_sTarget = tboxToAnotherDir.Text.Trim();
			DirectoryInfo diFolder = new DirectoryInfo( m_sSource );
			
			// проверки папки для сканирования
			if( !IsSourceDirCorrect( m_sSource, diFolder ) ) {
				return;
			}
			// проверки папки-приемника
			if( !IsTargetDirCorrect( m_sTarget, rbtnToAnotherDir.Checked ) ) {
				return;
			}
			
			// читаем путь к архиваторам из настроек
			m_s7zPath	= Settings.Settings.Read7zaPath().Trim();
			m_sRarPath	= Settings.Settings.ReadRarPath().Trim();

			// проверка на наличие архиваторов и корректность путей к ним
			if( !IsArchivatorsPathCorrectForArchive( m_s7zPath, m_sRarPath ) ) {
			   	return;
			}
	
			// инициализация контролов
			InitA();
			SetArhivingStartEnabled( false );
			
			// Запуск процесса DoWork от Бекграунд Воркера
			if( m_bwa.IsBusy != true ) {
				//если не занят то запустить процесс
            	m_bwa.RunWorkerAsync();
			}
		}

		void RbtnUAToAnotherDirCheckedChanged(object sender, EventArgs e)
		{
			btnUAToAnotherDir.Enabled	= rbtnUAToAnotherDir.Checked;
			tboxUAToAnotherDir.ReadOnly	= !rbtnUAToAnotherDir.Checked;
			if( rbtnUAToAnotherDir.Checked ) {
				tboxUAToAnotherDir.Focus();
			}
		}
		
		void TsbtnUAAnalyzeClick(object sender, EventArgs e)
		{
			// анализ файлов - какие архивы есть в папке сканирования
			m_sMessTitle = "SharpFBTools - Анализ файлов";
			m_sSource = tboxUASourceDir.Text.Trim();
			DirectoryInfo diFolder = new DirectoryInfo( m_sSource );
			
			// проверки папки для сканирования перед запуском Анализа
			if( !IsSourceDirCorrect( m_sSource, diFolder ) ) {
				return;
			}

			// инициализация контролов
			InitUA();
			SetAnalyzingStartEnabled( false );
			
			// Запуск процесса DoWork от Бекграунд Воркера
			if( m_bwt.IsBusy != true ) {
				//если не занят то запустить процесс
            	m_bwt.RunWorkerAsync();
			}
		}
		
		void TsbtnUnArchiveClick(object sender, EventArgs e)
		{
			// Распаковка архивов
			m_sMessTitle = "SharpFBTools - Распаковка архивов";
			m_sSource = tboxUASourceDir.Text.Trim();
			m_sTarget = tboxUAToAnotherDir.Text.Trim();
			DirectoryInfo diFolder = new DirectoryInfo( m_sSource );
			
			// проверки папки для сканирования
			if( !IsSourceDirCorrect( m_sSource, diFolder ) ) {
				return;
			}
			// проверки папки-приемника
			if( !IsTargetDirCorrect( m_sTarget, rbtnUAToAnotherDir.Checked ) ) {
				return;
			}
			
			// читаем путь к UnRar и к 7z из настроек
			m_s7zPath		= Settings.Settings.Read7zaPath().Trim();
			m_sUnRarPath	= Settings.Settings.ReadUnRarPath().Trim();
			if( !IsArchivatorsPathCorrectForUnArchive( m_s7zPath, m_sUnRarPath ) ) {
				return;
			}

			// инициализация контролов
			InitUA();
			SetUnPackingStartEnabled( false );
			
			// Запуск процесса DoWork от Бекграунд Воркера
			if( m_bwu.IsBusy != true ) {
				//если не занят то запустить процесс
            	m_bwu.RunWorkerAsync();
			}
		}
		
		void CboxExistArchiveSelectedIndexChanged(object sender, EventArgs e)
		{
			chBoxAddArchiveNameBookID.Enabled = ( cboxExistArchive.SelectedIndex != 0 );
		}
		
		void CboxUAExistArchiveSelectedIndexChanged(object sender, EventArgs e)
		{
			chBoxAddFileNameBookID.Enabled = ( cboxUAExistArchive.SelectedIndex != 0 );
		}
		
		void TboxSourceDirTextChanged(object sender, EventArgs e)
		{
			Settings.SettingsAM.AMAScanDir = tboxSourceDir.Text;
		}
		
		void TboxToAnotherDirTextChanged(object sender, EventArgs e)
		{
			Settings.SettingsAM.AMATargetDir = tboxToAnotherDir.Text;
		}
		
		void TboxUASourceDirTextChanged(object sender, EventArgs e)
		{
			Settings.SettingsAM.AMUAScanDir = tboxUASourceDir.Text;
		}
		
		void TboxUAToAnotherDirTextChanged(object sender, EventArgs e)
		{
			Settings.SettingsAM.AMAUATargetDir = tboxUAToAnotherDir.Text;
		}
		
		void TsbtnArchiveStopClick(object sender, EventArgs e)
		{
			// Остановка выполнения процесса Архивации
			if( m_bwa.WorkerSupportsCancellation == true ) {
				m_bwa.CancelAsync();
			}
		}
		
		void TsbtnUnArchiveStopClick(object sender, EventArgs e)
		{
			// Остановка выполнения процесса Распаковки
			if( m_bwu.WorkerSupportsCancellation == true ) {
				m_bwu.CancelAsync();
			}
		}
		
		void TsbtnUAAnalyzeStopClick(object sender, EventArgs e)
		{
			// Остановка выполнения процесса Анализа
			if( m_bwt.WorkerSupportsCancellation == true ) {
				m_bwt.CancelAsync();
			}
		}
		#endregion
	}
}
