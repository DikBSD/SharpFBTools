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
using System.Linq;
using System.Xml.Linq;
using System.Threading;
using System.Diagnostics;
using System.Text;

using Core.FB2.Description.DocumentInfo;
using Core.FilesWorker;

using filesWorker = Core.FilesWorker.FilesWorker;

using ICSharpCode.SharpZipLib.Zip;

namespace SharpFBTools.Tools
{
	/// <summary>
	/// Description of SFBTpArchiveManager.
	/// </summary>
	public partial class SFBTpArchiveManager : UserControl
	{
		#region Закрытые члены-данные класса
		private string m_FileSettingsPath = Settings.Settings.ProgDir + @"\ArchiveManagerSettings.xml";
		private bool m_isSettingsLoaded	= false; // Только при true все изменения настроек сохраняются в файл.
		private Core.FilesWorker.SharpZipLibWorker sharpZipLib = new Core.FilesWorker.SharpZipLibWorker();
		private string m_TempDir = Settings.Settings.TempDir;
		// Общие
		private DateTime m_dtStart;
		private string	m_sMessTitle	= string.Empty;
		// Для Упаковки
		private BackgroundWorker m_bwa	= null;
		private int	m_nFB2A				= 0;
		// Для Распаковки
		private BackgroundWorker m_bwu	= null;
		private int	m_nUnpackCount		= 0;
		private int	m_nCountU 			= 0;
		private long m_nFB2U 			= 0;
		private int	m_nZipU 			= 0;
		#endregion
		
		public SFBTpArchiveManager()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			// задание для кнопок ToolStrip стиля и положения текста и картинки
			SetToolButtonsSettings();
			
			InitializeArchiveBackgroundWorker();
			InitializeUnPackBackgroundWorker();
			InitA();	// инициализация контролов (Упаковка)
			InitUA();	// инициализация контролов (Распаковка

			cboxExistArchive.SelectedIndex		= 1; // добавление к создаваемому fb2-архиву очередного номера
			cboxUAExistArchive.SelectedIndex	= 1; // добавление к создаваемому fb2-файлу очередного номера
			// чтение настроек из xml-файла
			readSettingsFromXML();
			m_isSettingsLoaded = true; // все настройки запгружены
		}
		
		#region Открытые методы класса
		// задание для кнопок ToolStrip стиля и положения текста и картинки
		public void SetToolButtonsSettings() {
			Settings.ArchiveManagerSettings.SetToolButtonsSettings( tsArchiver );
			Settings.ArchiveManagerSettings.SetToolButtonsSettings( tsUnArchiver );
		}
		#endregion
		
		#region Закрытые Общие Вспомогательны методы класса
		// удаляем исходный файл
		private bool DeleteFileIsNeeds( string sFile ) {
			if( File.Exists( sFile ) ) {
				File.Delete( sFile );
				return true;
			}
			return false;
		}
		
		// сохранение настроек в xml-файл
		private void saveSettingsToXml() {
			#region Код
			if( m_isSettingsLoaded ) {
				// защита от "затирания" настроек в файле, когда в некоторые контролы данные еще не загрузились
				XDocument doc = new XDocument(
					new XDeclaration("1.0", "utf-8", "yes"),
					new XElement("Settings",
					             new XElement("Zip",
					                          new XComment("Папка исходных fb2-файлов"),
					                          new XElement("SourceDir", tboxSourceDir.Text.Trim()),
					                          new XComment("Папка для размещения архивов"),
					                          new XElement("TargetDir", tboxToAnotherDir.Text.Trim()),
					                          new XComment("Операции с одинаковыми zip-архивами"),
					                          new XElement("ZipExsistMode",
					                                       new XAttribute("Index", cboxExistArchive.SelectedIndex),
					                                       new XAttribute("Name", cboxExistArchive.Text)
					                                      ),
					                          new XComment("Настройки архивации"),
					                          new XElement("Options",
					                                       new XComment("Сканировать и подпапки"),
					                                       new XElement("ScanSubDirs", cboxScanSubDirToArchive.Checked),
					                                       new XComment("Поместить zip-архив в ту же папку, где находится исходный fb2-файл"),
					                                       new XElement("ToSomeDir", cboxToSomeDir.Checked),
					                                       new XComment("Удалить fb2-файлы после упаковки"),
					                                       new XElement("DeleteFB2Files", cboxDelFB2Files.Checked)
					                                      ),
					                          new XComment("Отображать изменения хода работы"),
					                          new XElement("Progress", chBoxViewProgressA.Checked)
					                         ),
					             new XElement("UnZip",
					                          new XComment("Папка для исходных zip-архивов"),
					                          new XElement("SourceDir", tboxUASourceDir.Text.Trim()),
					                          new XComment("Папка для размещения распакованных fb2-файлов"),
					                          new XElement("TargetDir", tboxUAToAnotherDir.Text.Trim()),
					                          new XComment("Операции с одинаковыми fb2-файлами"),
					                          new XElement("FB2ExsistMode",
					                                       new XAttribute("Index", cboxUAExistArchive.SelectedIndex),
					                                       new XAttribute("Name", cboxUAExistArchive.Text)
					                                      ),
					                          new XComment("Настройки распаковки"),
					                          new XElement("Options",
					                                       new XComment("Сканировать и подпапки"),
					                                       new XElement("ScanSubDirs", cboxScanSubDirToUnArchive.Checked),
					                                       new XComment("Поместить fb2-файл в ту же папку, где находится исходный архив"),
					                                       new XElement("ToSomeDir", cboxUAToSomeDir.Checked),
					                                       new XComment("Удалить zip-архивы после распаковки"),
					                                       new XElement("DeleteZip", cboxUADelFB2Files.Checked)
					                                      ),
					                          new XComment("Отображать изменения хода работы"),
					                          new XElement("Progress", chBoxViewProgressU.Checked)
					                         )
					            )
				);
				doc.Save(m_FileSettingsPath);
			}
			
			#endregion
		}
		
		// загрузка настроек из xml-файла
		private void readSettingsFromXML() {
			#region Код
			if( File.Exists( m_FileSettingsPath ) ) {
				XElement xmlTree = XElement.Load( m_FileSettingsPath );
				/* Zip */
				if( xmlTree.Element("Zip") != null ) {
					XElement xmlZip = xmlTree.Element("Zip");
					// Папка исходных fb2-файлов
					if( xmlZip.Element("SourceDir") != null )
						tboxSourceDir.Text = xmlZip.Element("SourceDir").Value;
					// Папка для размещения архивов
					if( xmlZip.Element("TargetDir") != null )
						tboxToAnotherDir.Text = xmlZip.Element("TargetDir").Value;
					// Операции с одинаковыми zip-архивами
					if( xmlZip.Element("ZipExsistMode") != null ) {
						if( xmlZip.Element("ZipExsistMode").Attribute("Index") != null )
							cboxExistArchive.SelectedIndex = Convert.ToInt16( xmlZip.Element("ZipExsistMode").Attribute("Index").Value );
					}
					// Настройки архивации:
					if( xmlZip.Element("Options") != null ) {
						XElement xmlOptions = xmlZip.Element("Options");
						// Сканировать и подпапки
						if( xmlOptions.Element("ScanSubDirs") != null )
							cboxScanSubDirToArchive.Checked	= Convert.ToBoolean( xmlOptions.Element("ScanSubDirs").Value );
						// Поместить zip-архив в ту же папку, где находится исходный fb2-файл"
						if( xmlOptions.Element("ToSomeDir") != null )
							cboxToSomeDir.Checked	= Convert.ToBoolean( xmlOptions.Element("ToSomeDir").Value );
						// Удалить fb2-файлы после упаковки"
						if( xmlOptions.Element("DeleteFB2Files") != null )
							cboxDelFB2Files.Checked	= Convert.ToBoolean( xmlOptions.Element("DeleteFB2Files").Value );
					}
					// Отображать изменения хода работы
					if( xmlZip.Element("Progress") != null )
						chBoxViewProgressA.Checked = Convert.ToBoolean( xmlZip.Element("Progress").Value );
				}
				
				/* UnZip */
				if( xmlTree.Element("UnZip") != null ) {
					XElement xmlUnZip = xmlTree.Element("UnZip");
					// Папка исходных zip-файлов
					if( xmlUnZip.Element("SourceDir") != null )
						tboxUASourceDir.Text = xmlUnZip.Element("SourceDir").Value;
					// Папка для размещения архивов
					if( xmlUnZip.Element("TargetDir") != null )
						tboxUAToAnotherDir.Text = xmlUnZip.Element("TargetDir").Value;
					// Операции с одинаковыми fb2-архивами
					if( xmlUnZip.Element("FB2ExsistMode") != null ) {
						if( xmlUnZip.Element("FB2ExsistMode").Attribute("Index") != null )
							cboxUAExistArchive.SelectedIndex = Convert.ToInt16( xmlUnZip.Element("FB2ExsistMode").Attribute("Index").Value );
					}
					// Настройки распаковки:
					if( xmlUnZip.Element("Options") != null ) {
						XElement xmlOptions = xmlUnZip.Element("Options");
						// Сканировать и подпапки
						if( xmlOptions.Element("ScanSubDirs") != null )
							cboxScanSubDirToUnArchive.Checked = Convert.ToBoolean( xmlOptions.Element("ScanSubDirs").Value );
						// Поместить fb2-архив в ту же папку, где находится исходный архив"
						if( xmlOptions.Element("ToSomeDir") != null )
							cboxUAToSomeDir.Checked	= Convert.ToBoolean( xmlOptions.Element("ToSomeDir").Value );
						// Удалить zip-архивы после упаковки"
						if( xmlOptions.Element("DeleteZip") != null )
							cboxUADelFB2Files.Checked	= Convert.ToBoolean( xmlOptions.Element("DeleteZip").Value );
					}
					// Отображать изменения хода работы
					if( xmlUnZip.Element("Progress") != null )
						chBoxViewProgressU.Checked = Convert.ToBoolean( xmlUnZip.Element("Progress").Value );
				}
			}
			#endregion
		}
		#endregion
		
		#region Архивация
		// Инициализация перед использование BackgroundWorker Упаковщика
		private void InitializeArchiveBackgroundWorker() {
			m_bwa = new BackgroundWorker();
			m_bwa.WorkerReportsProgress			= true; // Позволить выводить прогресс процесса
			m_bwa.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
			m_bwa.DoWork 				+= new DoWorkEventHandler( bwa_DoWork );
			m_bwa.ProgressChanged 		+= new ProgressChangedEventHandler( bwa_ProgressChanged );
			m_bwa.RunWorkerCompleted 	+= new RunWorkerCompletedEventHandler( bwa_RunWorkerCompleted );
		}
		
		
		// упаковка файлов в архивы
		private void bwa_DoWork( object sender, DoWorkEventArgs e ) {
			int nAllFiles = 0;
			List<string> lDirList = new List<string>();
			string SourceDir = getSourceDirForZip();
			if( !getScanSubDirsForZip() ) {
				// сканировать только указанную папку
				lDirList.Add( SourceDir );
				lvGeneralCount.Items[0].SubItems[1].Text = "1";
			} else {
				// сканировать и все подпапки
				nAllFiles = filesWorker.DirsParser( m_bwa, e, SourceDir, ref lvGeneralCount, ref lDirList, false );
			}
			
			// отобразим число всех файлов в папке сканирования
			lvGeneralCount.Items[1].SubItems[1].Text = nAllFiles.ToString();

			// проверка остановки процесса
			if( ( m_bwa.CancellationPending == true ) )  {
				e.Cancel = true; // Выставить окончание - по отмене, сработает событие bwa_RunWorkerCompleted
				return;
			}
			
			// проверка, есть ли хоть один файл в папке для сканирования
			if( nAllFiles == 0 ) {
				MessageBox.Show( "Не найдено ни одного файла!\nРабота прекращена.", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				InitA();
				return;
			}

			tsslblProgress.Text = "Упаковка найденных файлов в zip:";
			tsProgressBar.Maximum	= nAllFiles;
			tsProgressBar.Value		= 0;
			m_nFB2A = 0;
			string sFile = string.Empty;
			string TargetDir = getTargetDirForZip();
			int n = 0;
			foreach( string s in lDirList ) {
				DirectoryInfo diFolder = new DirectoryInfo( s );
				foreach( FileInfo fiNextFile in diFolder.GetFiles() ) {
					if( ( m_bwa.CancellationPending == true ) )  {
						e.Cancel = true; // Выставить окончание - по отмене, сработает событие bwa_RunWorkerCompleted
						return;
					}
					sFile = s + "\\" + fiNextFile.Name;
					FileToZip( sFile, SourceDir, TargetDir ); // zip
					m_bwa.ReportProgress( ++n ); // отобразим данные в контролах
				}
			}
			lDirList.Clear();
		}
		
		// Отобразим результат Упаковки
		private void bwa_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
			if( chBoxViewProgressA.Checked )
				ArchiveProgressData();
			tsProgressBar.Value	= e.ProgressPercentage;
		}
		
		// Проверяем это отмена, ошибка, или конец задачи и сообщить
		private void bwa_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {
			ArchiveProgressData(); // Отобразим результат Упаковки
			DateTime dtEnd = DateTime.Now;
			filesWorker.RemoveDir( m_TempDir );
			
			tsslblProgress.Text = Settings.Settings.GetReady();
			SetPackingStartEnabled( true );
			
			string sTime = dtEnd.Subtract( m_dtStart ).ToString() + " (час.:мин.:сек.)";
			string sMessCanceled	= "Упаковка fb2-файлов остановлена!\nЗатрачено времени: "+sTime;
			string sMessError		= string.Empty;
			string sMessDone		= "Упаковка fb2-файлов завершена!\nЗатрачено времени: "+sTime;
			
			if( ( e.Cancelled == true ) )
				MessageBox.Show( sMessCanceled, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			else if( e.Error != null ) {
				sMessError = "Ошибка:\n" + e.Error.Message + "\n" + e.Error.StackTrace + "\nЗатрачено времени: "+sTime;
				MessageBox.Show( sMessError, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			} else {
				MessageBox.Show( sMessDone, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
		}
		
		// создание уникального пути создаваемого архива, помещаемого в ту же папку, где лежит и исхлжный файл
		private string MakeNewArchivePathToSomeDirWithSufix( string FilePath ) {
			string FileExt = Path.GetExtension( FilePath ).ToLower();
			string ArchiveFile = FilePath + ".zip";
			if( File.Exists( ArchiveFile ) ) {
				if( cboxExistArchive.SelectedIndex == 0 )
					File.Delete( ArchiveFile );
				else {
					// или FilePath вместо sArchiveFile
					ArchiveFile = FilePath.Remove( FilePath.Length-4 )
						+ filesWorker.createSufix( ArchiveFile, cboxExistArchive.SelectedIndex )//Sufix
						+ FileExt + ".zip";
				}
			}
			return ArchiveFile;
		}
		
		// создание уникального пути создаваемого архива, помещаемого в другую папку
		private string MakeNewArchivePathToAnotherDirWithSufix( string SourceDir, string TargetDir, string FilePath ) {
			string FileExt = Path.GetExtension( FilePath ).ToLower();
			string NewFilePath = FilePath.Remove( 0, SourceDir.Length );
			string ArchiveFile = TargetDir + NewFilePath + ".zip";
			FileInfo fi = new FileInfo( ArchiveFile );
			if( !fi.Directory.Exists )
				Directory.CreateDirectory( fi.Directory.ToString() );

			if( File.Exists( ArchiveFile ) ) {
				if( cboxExistArchive.SelectedIndex == 0 )
					File.Delete( ArchiveFile );
				else {
					ArchiveFile = TargetDir + NewFilePath.Remove( NewFilePath.Length-4 )
						+ filesWorker.createSufix( ArchiveFile, cboxExistArchive.SelectedIndex )//Sufix
						+ FileExt + ".zip";
				}
			}
			return ArchiveFile;
		}
		
		// упаковка fb2-файлов в .fb2.zip
		private void FileToZip( string FilePath, string SourceDir, string TargetDir ) {
			// упаковываем только fb2-файлы
			if( Path.GetExtension( FilePath ).ToLower() == ".fb2" ) {
				++m_nFB2A;
				string ArchiveFile = string.Empty;
				if( cboxToSomeDir.Checked ) {
					// создаем архив в тот же папке, где и исходный fb2-файл
					ArchiveFile = MakeNewArchivePathToSomeDirWithSufix( FilePath );
				} else {
					// создаем архив в другой папке
					ArchiveFile = MakeNewArchivePathToAnotherDirWithSufix( SourceDir, TargetDir, FilePath );
				}
				sharpZipLib.ZipFile( FilePath, ArchiveFile, 9, ICSharpCode.SharpZipLib.Zip.CompressionMethod.Deflated, 4096 );
				
				// удаляем исходный файл, если задана опция
				if ( cboxDelFB2Files.Checked )
					DeleteFileIsNeeds( FilePath );
			}
		}
		
		// доступность контролов при Упаковке
		private void SetPackingStartEnabled( bool bEnabled ) {
			tsbtnArchive.Enabled	= bEnabled;
			pScanDir.Enabled		= bEnabled;
			pType.Enabled			= bEnabled;
			pToAnotherDir.Enabled	= bEnabled;
			cboxToSomeDir.Enabled	= bEnabled;
			tpUnArchive.Enabled		= bEnabled;

			tsbtnArchiveStop.Enabled	= !bEnabled;
			tsProgressBar.Visible		= !bEnabled;
			tcArchiver.Refresh();
			ssProgress.Refresh();
		}
		
		// сканировать ли подпапки для режима упаковки
		private bool getScanSubDirsForZip() {
			return cboxScanSubDirToArchive.Checked;
		}
		
		// получение source папки для режима упаковки
		private string getSourceDirForZip() {
			return filesWorker.WorkingDirPath( tboxSourceDir.Text.Trim() );
		}
		
		// получение target папки для режима упаковки
		private string getTargetDirForZip() {
			if (cboxToSomeDir.Checked)
				return filesWorker.WorkingDirPath( tboxSourceDir.Text.Trim() );
			else
				return filesWorker.WorkingDirPath( tboxToAnotherDir.Text.Trim() );
		}
		
		// Отобразим результат Упаковки
		private void ArchiveProgressData() {
			lvGeneralCount.Items[2].SubItems[1].Text = (m_nFB2A).ToString();
		}
		
		// инициализация контролов и переменных (Упаковка)
		private void InitA() {
			for( int i=0; i!=lvGeneralCount.Items.Count; ++i ) {
				lvGeneralCount.Items[i].SubItems[1].Text = "0";
			}
			tsProgressBar.Value		= 0;
			tsslblProgress.Text		= Settings.Settings.GetReady();
			tsProgressBar.Visible	= false;
		}
		
		// проверки папки для сканирования
		private bool IsSourceDirCorrect( string SourceDir, string sMessTitle ) {
			DirectoryInfo diFolder = new DirectoryInfo( SourceDir );
			if( SourceDir.Length == 0 ) {
				MessageBox.Show( "Выберите папку для сканирования!", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка для сканирования не найдена: " + SourceDir, sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			return true;
		}
		
		#region Обработчики событий
		void CboxToAnotherDirCheckedChanged(object sender, EventArgs e)
		{
			pToAnotherDir.Enabled = !cboxToSomeDir.Checked;
			if( !cboxToSomeDir.Checked )
				tboxToAnotherDir.Focus();
			saveSettingsToXml();
		}
		
		// задание папки для копирования запакованных fb2-файлов
		void BtnToAnotherDirClick(object sender, EventArgs e)
		{
			filesWorker.OpenDirDlg( tboxToAnotherDir, fbdDir, "Укажите папку для размещения упакованных fb2-файлов" );
		}
		
		// задание папки с fb2-файлами для сканирования (Архивация)
		void BtnOpenDirClick(object sender, EventArgs e)
		{
			if( filesWorker.OpenDirDlg( tboxSourceDir, fbdDir, "Укажите папку с fb2-файлами для Упаковки" ) )
				InitA();
		}
		
		// Упаковка fb2-файлов
		void TsbtnArchiveClick(object sender, EventArgs e)
		{
			m_sMessTitle = "SharpFBTools - Упаковка в архивы";

			// проверки папки для сканирования
			if( !IsSourceDirCorrect( getSourceDirForZip(), m_sMessTitle ) )
				return;
			
			// проверки папки-приемника
			if( !IsTargetDirCorrect( getTargetDirForZip(), cboxToSomeDir.Checked, m_sMessTitle ) )
				return;

			// инициализация контролов
			InitA();
			SetPackingStartEnabled( false );
			
			m_dtStart = DateTime.Now;
			tsslblProgress.Text = "Создание списка папок:";
			// Запуск процесса DoWork от Бекграунд Воркера
			if( m_bwa.IsBusy != true ) {
				//если не занят то запустить процесс
				m_bwa.RunWorkerAsync();
			}
		}
		
		void TboxSourceDirTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void TboxToAnotherDirTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void CboxScanSubDirToArchiveCheckStateChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void CboxExistArchiveSelectedIndexChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void CboxDelFB2FilesCheckStateChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void ChBoxViewProgressACheckStateChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		// Остановка выполнения процесса Архивации
		void TsbtnArchiveStopClick(object sender, EventArgs e)
		{
			if( m_bwa.WorkerSupportsCancellation == true )
				m_bwa.CancelAsync();
		}
		
		#endregion
		
		#endregion
		
		#region Распаковка
		// Инициализация перед использование BackgroundWorker Распаковщика
		private void InitializeUnPackBackgroundWorker() {
			m_bwu = new BackgroundWorker();
			m_bwu.WorkerReportsProgress			= true; // Позволить выводить прогресс процесса
			m_bwu.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
			m_bwu.DoWork 				+= new DoWorkEventHandler( bwu_DoWork );
			m_bwu.ProgressChanged 		+= new ProgressChangedEventHandler( bwu_ProgressChanged );
			m_bwu.RunWorkerCompleted 	+= new RunWorkerCompletedEventHandler( bwu_RunWorkerCompleted );
		}
		
		// распаковка архивов в файлы
		private void bwu_DoWork( object sender, DoWorkEventArgs e ) {
			int nAllFiles = 0;
			List<string> lDirList = new List<string>();
			string SourceDir = getSourceDirForUnZip();
			if( !getScanSubDirsForUnZip() ) {
				// сканировать только указанную папку
				lDirList.Add( SourceDir );
				lvUAGeneralCount.Items[0].SubItems[1].Text = "1";
			} else {
				// сканировать и все подпапки
				nAllFiles = filesWorker.DirsParser( m_bwu, e, SourceDir, ref lvUAGeneralCount, ref lDirList, false );
			}
			
			// отобразим число всех файлов в папке сканирования
			lvUAGeneralCount.Items[1].SubItems[1].Text = nAllFiles.ToString();

			// проверка остановки процесса
			if( ( m_bwu.CancellationPending == true ) )  {
				e.Cancel = true; // Выставить окончание - по отмене, сработает событие bwu_RunWorkerCompleted
				return;
			}
			
			// проверка, есть ли хоть один файл в папке для сканирования
			if( nAllFiles == 0 ) {
				MessageBox.Show( "В указанной папке не найдено ни одного файла!", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				InitUA();
				return;
			}
			
			tsslblProgress.Text 	= "Распаковка архивов:";
			tsProgressBar.Maximum 	= nAllFiles;
			tsProgressBar.Value 	= 0;
			m_nCountU = m_nZipU		= 0;
			m_nFB2U = 0;

			filesWorker.RemoveDir( m_TempDir );
			
			BackgroundWorker bw = sender as BackgroundWorker;
			m_nUnpackCount = UnZipToFile( bw, e, SourceDir, lDirList, getTargetDirForUnZip() );
			lDirList.Clear();
		}
		
		// Отобразим результат Распаковки
		private void bwu_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
			if( chBoxViewProgressU.Checked ) UnArchiveProgressData();
			tsProgressBar.Value	= e.ProgressPercentage;
		}
		
		// Проверяем это отмена, ошибка, или конец задачи и сообщить
		private void bwu_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {
			UnArchiveProgressData(); // Отобразим результат Распаковки
			DateTime dtEnd = DateTime.Now;
			filesWorker.RemoveDir( m_TempDir );
			
			tsslblProgress.Text = Settings.Settings.GetReady();
			SetUnPackingStartEnabled( true );
			
			string sTime = dtEnd.Subtract( m_dtStart ).ToString() + " (час.:мин.:сек.)";
			string sMessCanceled	= "Распаковка архивов в fb2-файлы остановлена!\nЗатрачено времени: "+sTime;
			string sMessError		= string.Empty;
			string sMessDone		= string.Empty;
			if( m_nUnpackCount > 0 ) {
				sMessDone	= "Распаковка архивов в fb2-файлы завершена!\nЗатрачено времени: "+sTime;
			} else {
				sMessDone	= "В папке для сканирования не найдено ни одного архива указанного типа!\nРаспаковка не произведена."+sTime;
			}
			
			if( ( e.Cancelled == true ) ) {
				MessageBox.Show( sMessCanceled, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			} else if( e.Error != null ) {
				sMessError = "Ошибка:\n" + e.Error.Message + "\n" + e.Error.StackTrace + "\nЗатрачено времени: "+sTime;
				MessageBox.Show( sMessError, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			} else {
				MessageBox.Show( sMessDone, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
		}
		
		// Распаковать архива
		private int UnZipToFile( BackgroundWorker bw, DoWorkEventArgs e, string FileSourceDir, List<string> lDirList, string TargetDir ) {
			int nCount = 0;
			int n = 0;
			foreach( string dir in lDirList ) {
				DirectoryInfo diFolder = new DirectoryInfo( dir );
				foreach( FileInfo fiNextFile in diFolder.GetFiles() ) {
					if( ( bw.CancellationPending == true ) ) {
						e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
						return nCount;
					}
					string sFile = dir + "\\" + fiNextFile.Name;
					if( Path.GetExtension( sFile.ToLower() ) == ".zip" ) {
						//string sNewDir = Path.GetDirectoryName( sMoveToDir+"\\"+sFile.Remove( 0, sFileSourceDir.Length ) );
						m_nFB2U += sharpZipLib.UnZipFiles(sFile, filesWorker.buildTargetDir(sFile, FileSourceDir, TargetDir),
						                                  cboxUAExistArchive.SelectedIndex, true, null, 4096);
						++m_nZipU; ++m_nCountU; ++nCount;
						// удаление исходного архива, если включена опция
						if ( cboxUADelFB2Files.Checked )
							DeleteFileIsNeeds( sFile );
					}
					bw.ReportProgress( ++n ); // отобразим данные в контролах
				}
			}
			return nCount;
		}
		
		// инициализация контролов и переменных (Распаковка)
		private void InitUA() {
			for( int i=0; i!=lvUAGeneralCount.Items.Count; ++i ) {
				lvUAGeneralCount.Items[i].SubItems[1].Text = "0";
			}
			tsProgressBar.Value		= 0;
			tsslblProgress.Text		= Settings.Settings.GetReady();
			tsProgressBar.Visible	= false;
			m_nUnpackCount			= 0;
		}
		
		// проверки папки-приемника
		private bool IsTargetDirCorrect( string sTarget, bool bToAnotherDir, string sMessTitle ) {
			if( bToAnotherDir ) {
				// папка-приемник - отличная от источника
				if( sTarget.Length == 0 ) {
					MessageBox.Show( "Не задана папка-приемник архивов!", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return false;
				} else {
					// проверка папки-приемника и создание ее, если нужно
					if( !filesWorker.CreateDirIfNeed( sTarget, sMessTitle ) )
						return false;
				}
			}
			return true;
		}
		
		// доступность контролов при Распаковке
		private void SetUnPackingStartEnabled( bool bEnabled ) {
			tsbtnUnArchive.Enabled		= bEnabled;
			pUAScanDir.Enabled			= bEnabled;
			pUAType.Enabled				= bEnabled;
			pUAToAnotherDir.Enabled		= bEnabled;
			cboxUADelFB2Files.Enabled	= bEnabled;
			tpArchive.Enabled			= bEnabled;
			
			tsbtnUnArchiveStop.Enabled	= !bEnabled;
			tsProgressBar.Visible		= !bEnabled;
			tcArchiver.Refresh();
			ssProgress.Refresh();
		}
		
		// сканировать ли подпапки для режима распаковки
		private bool getScanSubDirsForUnZip() {
			return cboxScanSubDirToUnArchive.Checked;
		}
		
		// получение source папки для режима распаковки
		private string getSourceDirForUnZip() {
			return filesWorker.WorkingDirPath( tboxUASourceDir.Text.Trim() );
		}
		
		// получение target папки для режима распаковки
		private string getTargetDirForUnZip() {
			if (cboxUAToSomeDir.Checked)
				return filesWorker.WorkingDirPath( tboxUASourceDir.Text.Trim() );
			else
				return filesWorker.WorkingDirPath( tboxUAToAnotherDir.Text.Trim() );
		}
		
		// Отобразим результат Распаковки
		private void UnArchiveProgressData() {
			lvUAGeneralCount.Items[2].SubItems[1].Text = (m_nCountU).ToString();
			lvUAGeneralCount.Items[3].SubItems[1].Text = (m_nFB2U).ToString();
		}
		
		#region Обработчики событий
		// задание папки с fb2-архивами для сканирования (Распаковка)
		void BtnUAOpenDirClick(object sender, EventArgs e)
		{
			if( filesWorker.OpenDirDlg( tboxUASourceDir, fbdDir, "Укажите папку с fb2-архивами для Распаковки" ) )
				InitUA();
		}
		
		// задание папки для копирования распакованных файлов
		void BtnUAToAnotherDirClick(object sender, EventArgs e)
		{
			filesWorker.OpenDirDlg( tboxUAToAnotherDir, fbdDir, "Укажите папку для размещения распакованных файлов" );
		}
		
		void CboxUAToSomeDirCheckedChanged(object sender, EventArgs e)
		{
			pUAToAnotherDir.Enabled = !cboxUAToSomeDir.Checked;
			if( !cboxUAToSomeDir.Checked )
				tboxUAToAnotherDir.Focus();
			saveSettingsToXml();
		}
		
		// Распаковка архивов
		void TsbtnUnArchiveClick(object sender, EventArgs e)
		{
			m_sMessTitle = "SharpFBTools - Распаковка архивов";

			// проверки папки для сканирования
			if( !IsSourceDirCorrect( getSourceDirForUnZip(), m_sMessTitle ) )
				return;

			// проверки папки-приемника
			if( !IsTargetDirCorrect( getTargetDirForUnZip(), cboxUAToSomeDir.Checked, m_sMessTitle ) )
				return;

			// инициализация контролов
			InitUA();
			SetUnPackingStartEnabled( false );
			
			m_dtStart = DateTime.Now;
			tsslblProgress.Text = "Создание списка папок:";

			// Запуск процесса DoWork от Бекграунд Воркера
			if( m_bwu.IsBusy != true ) {
				//если не занят то запустить процесс
				m_bwu.RunWorkerAsync();
			}
		}
		
		void TboxUASourceDirTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void TboxUAToAnotherDirTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void CboxScanSubDirToUnArchiveCheckStateChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void CboxUAExistArchiveSelectedIndexChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void CboxUADelFB2FilesCheckStateChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void ChBoxViewProgressUCheckStateChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		// Остановка выполнения процесса Распаковки
		void TsbtnUnArchiveStopClick(object sender, EventArgs e)
		{
			if( m_bwu.WorkerSupportsCancellation == true )
				m_bwu.CancelAsync();
		}
		#endregion
		
		#endregion

	}
}
