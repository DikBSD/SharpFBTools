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
using System.Xml;

namespace SharpFBTools.Controls.Panels
{
	/// <summary>
	/// Description of SFBTpArchiveManager.
	/// </summary>
	public partial class SFBTpArchiveManager : UserControl
	{
		#region Закрытые члены-данные класса
		private string m_sReady = "Готово.";
		#endregion
		
		public SFBTpArchiveManager()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			InitA();	// инициализация контролов (Упаковка)
			InitUA();	// инициализация контролов (Распаковка
			cboxExistArchive.SelectedIndex = 1; // добавление к создаваемому fb2-архиву _год-месяц...
			cboxArchiveType.SelectedIndex = 1;	// Zip
			cboxUAExistArchive.SelectedIndex = 1;	// добавление к создаваемому fb2-файлу _год-месяц...
			cboxUAType.SelectedIndex = 6;	// Все архивы
		}
		
		private void InitA() {
			// инициализация контролов и переменных  (Упаковка)
			for( int i=0; i!=lvGeneralCount.Items.Count; ++i ) {
				lvGeneralCount.Items[i].SubItems[1].Text = "0";
			}
			tsProgressBar.Value		= 1;
			tsslblProgress.Text		= m_sReady;
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
			tsslblProgress.Text		= m_sReady;
			tsProgressBar.Visible	= false;
		}
		
		string ExistsFB2FileFileToDirWorker( string sNewFile, string sSufix ) {
			// Обработка существующих в папке-приемнике файлов при копировании
			if( File.Exists( sNewFile ) ) {
				if( cboxUAExistArchive.SelectedIndex==0 ) {
					File.Delete( sNewFile );
				} else {
					DateTime dt = DateTime.Now;
					sSufix = "_"+dt.Year.ToString()+"-"+dt.Month.ToString()+"-"+dt.Day.ToString()+"-"+
							dt.Hour.ToString()+"-"+dt.Minute.ToString()+"-"+dt.Second.ToString()+"-"+dt.Millisecond.ToString();
					sNewFile = sNewFile.Remove( sNewFile.Length-4 ) + sSufix + ".fb2";
				}
			}
			return sNewFile;
		}
		
		bool FileToDir( string sFile, string sArchiveFile, string sTargetDir, bool bFB2 ) {
			// Переместить в папку
			// bFB2=true - копировать только fb2-файлы. false - любые
			if( bFB2 && Path.GetExtension( sFile ).ToLower() != ".fb2" ) return false;
			string sFileSourceDir = tboxUASourceDir.Text;
			string sNewDir = Path.GetDirectoryName( sTargetDir + sArchiveFile.Remove( 0, sFileSourceDir.Length ) );
			string sNewFile = "";
			string sSufix = ""; // для добавления к имени нового файла суфикса
			if( rbtnUAToSomeDir.Checked ) {
				// файл - в ту же папку, где и исходный архив
				sNewFile = Path.GetDirectoryName( sArchiveFile )+"\\"+sFile;
				// Обработка существующих в папке-приемнике файлов при копировании
				sNewFile = ExistsFB2FileFileToDirWorker( sNewFile, sSufix );
			} else {
				// файл - в другую папку
				sNewFile = sNewDir + "\\" + sFile;
				FileInfo fi = new FileInfo( sNewFile );
				if( !fi.Directory.Exists ) {
					Directory.CreateDirectory( fi.Directory.ToString() );
				}
				// Обработка существующих в папке-приемнике файлов при копировании
				sNewFile = ExistsFB2FileFileToDirWorker( sNewFile, sSufix );
			}
			File.Move( FilesWorker.FilesWorker.GetTempDir()+"\\"+sFile, sNewFile );
			return true;
		}
		
		#region Архивация
		string GetArchiveExt( string sType ) {
			string sExt = "";
			switch( sType ) {
				case "Rar":
					sExt = "rar";
					break;
				case "Zip":
					sExt = "zip";
					break;
				case "7z":
					sExt = "7z";
					break;
				case "BZip2":
					sExt = "bz2";
					break;
				case "GZip":
					sExt = "gz";
					break;
				case "Tar":
					sExt = "tar";
					break;
			}
			return sExt;
		}
		
		void FileToArchive( string sArchPath, List<string> lFilesList, bool bZip, ToolStripProgressBar pBar ) {
			// упаковка fb2-файлов в .fb2.zip
			long lFB2 = 0;
			foreach( string sFile in lFilesList ) {
				string sExt = Path.GetExtension( sFile );
				if( sExt.ToLower() == ".fb2" ) {
					lvGeneralCount.Items[2].SubItems[1].Text = (++lFB2).ToString();
					lvGeneralCount.Refresh();
					// упаковываем
					string sArchiveFile = "";
					string sDotExt = "."+GetArchiveExt( cboxArchiveType.Text );
					string sSufix = ""; // для добавления к имени нового архива суфикса
					if( rbtnToSomeDir.Checked ) {
						// создаем архив в той же папке, где и исходный fb2-файл
						sArchiveFile = sFile + sDotExt;
						if( File.Exists( sArchiveFile ) ) {
							if( cboxExistArchive.SelectedIndex==0 ) {
								File.Delete( sArchiveFile );
							} else {
								DateTime dt = DateTime.Now;
								sSufix = "_"+dt.Year.ToString()+"-"+dt.Month.ToString()+"-"+dt.Day.ToString()+"-"+
										dt.Hour.ToString()+"-"+dt.Minute.ToString()+"-"+dt.Second.ToString()+"-"+dt.Millisecond.ToString();
								sArchiveFile = sFile.Remove( sFile.Length-4 ) + sSufix + ".fb2" + sDotExt;
							}
						}
					} else {
						// создаем архив в другой папке
						string sSource = tboxSourceDir.Text;
						string sTarget = tboxToAnotherDir.Text;
						string sNewFilePath = sFile.Remove( 0, sSource.Length );
						sArchiveFile = sTarget + sNewFilePath + sDotExt;
						FileInfo fi = new FileInfo( sArchiveFile );
						if( !fi.Directory.Exists ) {
							Directory.CreateDirectory( fi.Directory.ToString() );
						}
						if( File.Exists( sArchiveFile ) ) {
							if( cboxExistArchive.SelectedIndex==0 ) {
								File.Delete( sArchiveFile );
							} else {
								DateTime dt = DateTime.Now;
								sSufix = "_"+dt.Year.ToString()+"-"+dt.Month.ToString()+"-"+dt.Day.ToString()+"-"+
											dt.Hour.ToString()+"-"+dt.Minute.ToString()+"-"+dt.Second.ToString()+"-"+dt.Millisecond.ToString();
								sArchiveFile = sTarget + sNewFilePath.Remove( sNewFilePath.Length-4 ) + sSufix + ".fb2" + sDotExt;
							}
						}
					}
					if( bZip ) {
						Archiver.Archiver.zip( sArchPath, cboxArchiveType.Text.ToLower(), sFile, sArchiveFile );
					} else {
						Archiver.Archiver.rar( sArchPath, sFile, sArchiveFile, cboxAddRestoreInfo.Checked );
					}
					
					if( cboxDelFB2Files.Checked ) {
						// удаляем исходный fb2-файл
						if( File.Exists( sFile ) ) {
							File.Delete( sFile );
						}
					}
				}
				++pBar.Value;
			}
		}
		#endregion
				
		#region Распаковка
		void AllArchivesToFile( List<string> lFilesList, ToolStripProgressBar pBar ) {
			// Распаковать ахривы
			long lCount, lFB2, lRar, lZip, l7Z, lBZip2, lGZip, lTar;
			lCount = lFB2 = lRar = lZip = l7Z = lBZip2 = lGZip = lTar = 0;
			foreach( string sFile in lFilesList ) {
				string sExt = Path.GetExtension( sFile );
				if( sExt.ToLower() != "" ) {
					FilesWorker.FilesWorker.RemoveDir( FilesWorker.FilesWorker.GetTempDir() );
					switch( sExt.ToLower() ) {
						case ".rar":
							if( !Directory.Exists( FilesWorker.FilesWorker.GetTempDir() ) ) {
								Directory.CreateDirectory( FilesWorker.FilesWorker.GetTempDir() );
							}
							Archiver.Archiver.unrar( "unrar.exe", sFile, FilesWorker.FilesWorker.GetTempDir() );
							lvUACount.Items[0].SubItems[1].Text = (++lRar).ToString();
							++lCount;
							break;
						case ".zip":
							Archiver.Archiver.unzip( "7za.exe", sFile, FilesWorker.FilesWorker.GetTempDir() );
							lvUACount.Items[1].SubItems[1].Text = (++lZip).ToString();
							++lCount;
							break;
						case ".7z":
							Archiver.Archiver.unzip( "7za.exe", sFile, FilesWorker.FilesWorker.GetTempDir() );
							lvUACount.Items[2].SubItems[1].Text = (++l7Z).ToString();
							++lCount;
							break;
						case ".bz2":
							Archiver.Archiver.unzip( "7za.exe", sFile, FilesWorker.FilesWorker.GetTempDir() );
							lvUACount.Items[3].SubItems[1].Text = (++lBZip2).ToString();
							++lCount;
							break;
						case ".gz":
							Archiver.Archiver.unzip( "7za.exe", sFile, FilesWorker.FilesWorker.GetTempDir() );
							lvUACount.Items[4].SubItems[1].Text = (++lGZip).ToString();
							++lCount;
							break;
						case ".tar":
							Archiver.Archiver.unzip( "7za.exe", sFile, FilesWorker.FilesWorker.GetTempDir() );
							lvUACount.Items[5].SubItems[1].Text = (++lTar).ToString();
							++lCount;
							break;
					}
					if( Directory.Exists( FilesWorker.FilesWorker.GetTempDir() ) ) {
						string [] files = Directory.GetFiles( FilesWorker.FilesWorker.GetTempDir() );
						if( files.Length > 0 ) {
							string sFileName = Path.GetFileName( files[0] );
							if( Path.GetExtension( sFileName )==".fb2" ) {
								lvUAGeneralCount.Items[3].SubItems[1].Text = (++lFB2).ToString();
							}
							if( FileToDir( sFileName, sFile, tboxUAToAnotherDir.Text, false ) ) {
								lvUAGeneralCount.Items[2].SubItems[1].Text = (lCount).ToString();
							} else {
								File.Delete( sFileName );
							}
						}
					}
					lvUAGeneralCount.Refresh();
					lvUACount.Refresh();
				}
				++pBar.Value;
			}
		}
		
		void TypeArchToFile( List<string> lFilesList, ToolStripProgressBar pBar,
		                   string sExt, int nArchCountItem, int nFB2CountItem ) {
			long lCount = 0;
			long lAllArchive = 0;
			long lFB2 = 0;
			foreach( string sFile in lFilesList ) {
				if( Path.GetExtension( sFile.ToLower() ) == sExt ) {
					Archiver.Archiver.unzip( "7za.exe", sFile, FilesWorker.FilesWorker.GetTempDir() );
					lvUAGeneralCount.Items[2].SubItems[1].Text = (++lAllArchive).ToString();
					if( Directory.Exists( FilesWorker.FilesWorker.GetTempDir() ) ) {
						string [] files = Directory.GetFiles( FilesWorker.FilesWorker.GetTempDir() );
						if( files.Length > 0 ) {
							string sFileName = Path.GetFileName( files[0] );
							if( Path.GetExtension( sFileName )==".fb2" ) {
								lvUAGeneralCount.Items[nFB2CountItem].SubItems[1].Text = (++lFB2).ToString();
							}
							if( FileToDir( sFileName, sFile, tboxUAToAnotherDir.Text, false ) ) {
								lvUACount.Items[nArchCountItem].SubItems[1].Text = (++lCount).ToString();
							} else {
								File.Delete( sFileName );
							}
						}
					}
				}
				lvUAGeneralCount.Refresh();
				lvUACount.Refresh();
				++pBar.Value;
			}
		}
		
		void ArchivesToFile( List<string> lFilesList, ToolStripProgressBar pBar ) {
			// Распаковать ахривы
			string sArchType = GetArchiveExt( cboxUAType.Text );
			long lAllArchive = 0;
			long lRar = 0;
			long lFB2 = 0;
			FilesWorker.FilesWorker.RemoveDir( FilesWorker.FilesWorker.GetTempDir() );
			switch( sArchType.ToLower() ) {
				case "":
					AllArchivesToFile( lFilesList, pBar );
					break;
				case "rar":
					foreach( string sFile in lFilesList ) {
						string sExt = Path.GetExtension( sFile );
						if( sExt.ToLower() == ".rar" ) {
							if( !Directory.Exists( FilesWorker.FilesWorker.GetTempDir() ) ) {
								Directory.CreateDirectory( FilesWorker.FilesWorker.GetTempDir() );
							}
							Archiver.Archiver.unrar( "unrar.exe", sFile, FilesWorker.FilesWorker.GetTempDir() );
							lvUAGeneralCount.Items[2].SubItems[1].Text = (++lAllArchive).ToString();
							if( Directory.Exists( FilesWorker.FilesWorker.GetTempDir() ) ) {
								string [] files = Directory.GetFiles( FilesWorker.FilesWorker.GetTempDir() );
								if( files.Length > 0 ) {
									string sFileName = Path.GetFileName( files[0] );
									if( Path.GetExtension( sFileName )==".fb2" ) {
										lvUAGeneralCount.Items[3].SubItems[1].Text = (++lFB2).ToString();
									}
									if( FileToDir( sFileName, sFile, tboxUAToAnotherDir.Text, false ) ) {
										lvUACount.Items[0].SubItems[1].Text = (++lRar).ToString();
									}  else {
										File.Delete( sFileName );
									}
								}
							}
						}
						lvUAGeneralCount.Refresh();
						lvUACount.Refresh();
						++pBar.Value;
					}
					break;
				case "zip":
					TypeArchToFile( lFilesList, pBar, ".zip", 1, 3 );
					break;
				case "7z":
					TypeArchToFile( lFilesList, pBar, ".7z", 2, 3 );
					break;
				case "bz2":
					TypeArchToFile( lFilesList, pBar, ".bz2", 3, 3 );
					break;
				case "gz":
					TypeArchToFile( lFilesList, pBar, ".gz", 4, 3 );
					break;
				case "tar":
					TypeArchToFile( lFilesList, pBar, ".tar", 5, 3 );
					break;
			}
		}
		#endregion
		
		#region Обработчики событий
		void TsbtnOpenDirClick(object sender, EventArgs e)
		{
			// задание папки с fb2-файлами для сканирования (Архивация)
			fbdDir.Description = "Укажите папку с fb2-файлами для Упаковки";
			DialogResult result = fbdDir.ShowDialog();
			if (result == DialogResult.OK) {
                InitA();
				string openFolderName = fbdDir.SelectedPath;
                tboxSourceDir.Text = openFolderName;
            }
		}
		
		void RbtnToAnotherDirCheckedChanged(object sender, EventArgs e)
		{
			btnToAnotherDir.Enabled = rbtnToAnotherDir.Checked;
		}
		
		void BtnToAnotherDirClick(object sender, EventArgs e)
		{
			// задание папки для копирования запакованных fb2-файлов
			fbdDir.Description = "Укажите папку для размещения упакованных fb2-файлов";
			DialogResult result = fbdDir.ShowDialog();
			if (result == DialogResult.OK) {
                string openFolderName = fbdDir.SelectedPath;
                tboxToAnotherDir.Text = openFolderName;
            }
		}
		
		void CboxArchiveTypeSelectedIndexChanged(object sender, EventArgs e)
		{
			cboxAddRestoreInfo.Visible = cboxArchiveType.SelectedIndex == 0;
		}
		
		void TsbtnArchiveClick(object sender, EventArgs e)
		{
			// Запаковка fb2-файлов
			// проверки перед запуском архивации
			if( tboxSourceDir.Text == "" ) {
				MessageBox.Show( "Выберите папку для сканирования!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			DirectoryInfo diFolder = new DirectoryInfo(tboxSourceDir.Text);
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка не найдена:" + tboxSourceDir.Text, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			if( rbtnToAnotherDir.Checked && tboxToAnotherDir.Text == "" ) {
				MessageBox.Show( "Не задана папка-приемник архивов!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			// читаем путь к WinRar из настроек
			string sRarPath = Options.OptionsForm.GetDefRarPath();
			if( File.Exists( Options.OptionsForm.GetSettingsPath() ) ) {
				XmlReaderSettings settings = new XmlReaderSettings();
				settings.IgnoreWhitespace = true;
				using (XmlReader reader = XmlReader.Create(Options.OptionsForm.GetSettingsPath(), settings)) {
					reader.ReadToFollowing("WinRar");
					sRarPath = reader.GetAttribute("RarPath");
					reader.Close();
				}
			}
			if( cboxArchiveType.SelectedIndex == 0 && sRarPath == "" ) {
				MessageBox.Show( "Не указана папка с установленным консольным Rar-архиватором!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			// проверка на наличие архиваторов
			if( cboxArchiveType.SelectedIndex == 0 ) {
				if( !File.Exists( sRarPath ) ) {
					MessageBox.Show( "Не найден файл консольного Rar-архиватора "+sRarPath+"!\nРабота остановлена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return;
				} else {
					tsslblProgress.Text = "Упаковка найденных файлов в rar:";
				}
			} else {
				if( !File.Exists( "7za.exe" ) ) {
					MessageBox.Show( "Не найден файл Zip-архиватора 7za.exe в корневой папке программы!\nРабота остановлена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return;
				} else {
					tsslblProgress.Text = "Упаковка найденных файлов в zip:";
				}
			}
			// Упаковываем fb2-файлы
			DateTime dtStart = DateTime.Now;
			InitA();
			tsProgressBar.Visible = true;
			// сортированный список всех вложенных папок
			List<string> lDirList = FilesWorker.FilesWorker.DirsParser( diFolder.FullName, lvGeneralCount );
			lDirList.Sort();
			// сортированный список всех файлов
			tsslblProgress.Text = "Создание списка файлов:";
			List<string> lFilesList = FilesWorker.FilesWorker.AllFilesParser( lDirList, ssProgress, lvGeneralCount, tsProgressBar );
			lFilesList.Sort();
			
			if( lFilesList.Count == 0 ) {
				MessageBox.Show( "Не найдено ни одного файла!\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				InitA();
				return;
			}
			tsProgressBar.Maximum = lFilesList.Count+1;
			tsProgressBar.Value = 1;
			ssProgress.Refresh();
			if( cboxArchiveType.SelectedIndex == 0 ) {
				FileToArchive( sRarPath, lFilesList, false, tsProgressBar ); // rar
			} else {
				FileToArchive( "7za.exe", lFilesList, true, tsProgressBar ); // zip, 7z...
			}
			DateTime dtEnd = DateTime.Now;
			string sTime = dtEnd.Subtract( dtStart ).ToString() + " (час.:мин.:сек.)";
			MessageBox.Show( "Упаковка fb2-файлов завершена!\nЗатрачено времени: "+sTime, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
			tsslblProgress.Text = m_sReady;
			tsProgressBar.Visible = false;
		}
		
		void TsbtnUAOpenDirClick(object sender, EventArgs e)
		{
			// задание папки с fb2-архивами для сканирования (Распаковка)
			fbdDir.Description = "Укажите папку с fb2-архивами для Распаковки";
			DialogResult result = fbdDir.ShowDialog();
			if (result == DialogResult.OK) {
                InitA();
				string openFolderName = fbdDir.SelectedPath;
                tboxUASourceDir.Text = openFolderName;
            }
		}
		
		void RbtnUAToAnotherDirCheckedChanged(object sender, EventArgs e)
		{
			btnUAToAnotherDir.Enabled = rbtnUAToAnotherDir.Checked;
		}
		
		void TsbtnUAAnalyzeClick(object sender, EventArgs e)
		{
			// анализ файлов - какие архивы есть в папке сканирования
			if( tboxUASourceDir.Text == "" ) {
				MessageBox.Show( "Выберите папку для сканирования!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			DirectoryInfo diFolder = new DirectoryInfo(tboxUASourceDir.Text);
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка не найдена:" + tboxSourceDir.Text, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			DateTime dtStart = DateTime.Now;
			InitUA();
			tsProgressBar.Visible = true;
			// сортированный список всех вложенных папок
			List<string> lDirList = FilesWorker.FilesWorker.DirsParser( diFolder.FullName, lvUAGeneralCount );
			ssProgress.Update();
			lDirList.Sort();
			// сортированный список всех файлов
			tsslblProgress.Text = "Создание списка файлов:";
			gboxUACount.Refresh();
			List<string> lFilesList = FilesWorker.FilesWorker.AllFilesParser( lDirList, ssProgress, lvUAGeneralCount, tsProgressBar );
			lFilesList.Sort();
			
			if( lFilesList.Count == 0 ) {
				MessageBox.Show( "В указанной папке не найдено ни одного файла!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
			tsProgressBar.Maximum = lFilesList.Count+1;
			tsProgressBar.Value = 1;
			ssProgress.Refresh();
			gboxUACount.Refresh();

			long lRar, lZip, l7Z, lBZip2, lGZip, lTar;
			lRar = lZip = l7Z = lBZip2 = lGZip = lTar = 0;
			foreach( string sFile in lFilesList ) {
				string sExt = Path.GetExtension( sFile );
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
			
			DateTime dtEnd = DateTime.Now;
			string sTime = dtEnd.Subtract( dtStart ).ToString() + " (час.:мин.:сек.)";
			MessageBox.Show( "Анализ имеющихся файлов завершена!\nЗатрачено времени: "+sTime, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
			tsslblProgress.Text = m_sReady;
			tsProgressBar.Visible = false;
		}
		
		void TsbtnUnArchiveClick(object sender, EventArgs e)
		{
			// Распаковка архивов
			if( tboxUASourceDir.Text == "" ) {
				MessageBox.Show( "Выберите папку для сканирования!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			DirectoryInfo diFolder = new DirectoryInfo(tboxUASourceDir.Text);
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка не найдена:" + tboxSourceDir.Text, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			if( rbtnUAToAnotherDir.Checked ) {
				if( tboxUAToAnotherDir.Text == "") {
					MessageBox.Show( "Не указана папка-приемник файлов!\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return;
				}
				if( tboxUAToAnotherDir.Text == tboxUASourceDir.Text ) {
					MessageBox.Show( "Папка-приемник файлов совпадает с папкой сканирования!\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return;
				}
			}
			
			DateTime dtStart = DateTime.Now;
			InitUA();
			tsProgressBar.Visible = true;
			// сортированный список всех вложенных папок
			List<string> lDirList = FilesWorker.FilesWorker.DirsParser( diFolder.FullName, lvUAGeneralCount );
			ssProgress.Update();
			lDirList.Sort();
			// сортированный список всех файлов
			tsslblProgress.Text = "Создание списка файлов:";
			gboxUACount.Refresh();
			List<string> lFilesList = FilesWorker.FilesWorker.AllFilesParser( lDirList, ssProgress, lvUAGeneralCount, tsProgressBar );
			lFilesList.Sort();
			
			if( lFilesList.Count == 0 ) {
				MessageBox.Show( "В указанной папке не найдено ни одного файла!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
			tsProgressBar.Maximum = lFilesList.Count+1;
			tsProgressBar.Value = 1;
			ssProgress.Refresh();
			gboxUACount.Refresh();
			
			ArchivesToFile( lFilesList, tsProgressBar );
			
			DateTime dtEnd = DateTime.Now;
			string sTime = dtEnd.Subtract( dtStart ).ToString() + " (час.:мин.:сек.)";
			MessageBox.Show( "Распаковка архивов завершена!\nЗатрачено времени: "+sTime, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
			tsslblProgress.Text = m_sReady;
			tsProgressBar.Visible = false;
		}
		
		void BtnUAToAnotherDirClick(object sender, EventArgs e)
		{
			// задание папки для копирования распакованных файлов
			fbdDir.Description = "Укажите папку для размещения распакованных файлов";
			DialogResult result = fbdDir.ShowDialog();
			if (result == DialogResult.OK) {
                string openFolderName = fbdDir.SelectedPath;
                tboxUAToAnotherDir.Text = openFolderName;
            }
		}
		#endregion
	}
}
