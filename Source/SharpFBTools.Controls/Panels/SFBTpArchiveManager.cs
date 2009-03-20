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

namespace SharpFBTools.Controls.Panels
{
	/// <summary>
	/// Description of SFBTpArchiveManager.
	/// </summary>
	public partial class SFBTpArchiveManager : UserControl
	{
		#region Закрытые члены-данные класса
		private string m_sRarDir = "c:\\Program Files\\WinRAR";
		private string m_sReady = "Готово.";
		private long m_lFB2Files = 0;
		#endregion
		
		public SFBTpArchiveManager()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			// инициализация контролов
			Init();
			// путь к папке с Rar`м
			tboxRarDir.Text = m_sRarDir;
			cboxArchiveType.SelectedIndex = 1; // Zip
		}
		
		private void Init() {
			// инициализация контролов и переменных
			lblDirsCount.Text		= "0";
			lblFilesCount.Text		= "0";
			lblFB2FilesCount.Text	= "0";
			tsProgressBar.Value		= 1;
			tsslblProgress.Text		= m_sReady;
			tsProgressBar.Visible	= false;
		}
		
		void TsbtnOpenDirClick(object sender, EventArgs e)
		{
			// задание папки с fb2-файлами для сканирования
			fbdDir.Description = "Укажите папку для архивирования fb2-файлов";
			DialogResult result = fbdDir.ShowDialog();
			if (result == DialogResult.OK) {
                Init();
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
		
		void BtnRarDirClick(object sender, EventArgs e)
		{
			// задание папки для установленного в системе WinRar`a
			fbdDir.Description = "Укажите папку, где установлен Rar-архиватор";
			DialogResult result = fbdDir.ShowDialog();
			if (result == DialogResult.OK) {
                string openFolderName = fbdDir.SelectedPath;
                tboxRarDir.Text = openFolderName;
            }
		}
		
		void CboxArchiveTypeSelectedIndexChanged(object sender, EventArgs e)
		{
			gboxRar.Visible = cboxArchiveType.SelectedIndex == 0;
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
			if( cboxArchiveType.SelectedIndex == 0 && tboxRarDir.Text == "" ) {
				MessageBox.Show( "Не указана папка с установленным Rar-архиватором!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			// проверка на наличие архиваторов
			if( cboxArchiveType.SelectedIndex == 0 ) {
				string sRarPath = m_sRarDir + "\\Rar.exe";
				if( !File.Exists( sRarPath ) ) {
					MessageBox.Show( "Не найден файл Rar-архиватора "+sRarPath+"!\nРабота остановлена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
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
			Init();
			tsProgressBar.Visible = true;
			// сортированный список всех вложенных папок
			List<string> lDirList = FilesWorker.FilesWorker.DirsParser( diFolder.FullName, lblDirsCount );
			lDirList.Sort();
			// сортированный список всех файлов
			tsslblProgress.Text = "Создание списка файлов:";
			pCount.Refresh();
			List<string> lFilesList = FilesWorker.FilesWorker.AllFilesParser( lDirList, ssProgress, pCount, lblFilesCount, tsProgressBar );
			lFilesList.Sort();
			
			if( lFilesList.Count == 0 ) {
				MessageBox.Show( "Не найдено ни одного файла!\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				Init();
				return;
			}
			tsProgressBar.Maximum = lFilesList.Count+1;
			tsProgressBar.Value = 1;
			pCount.Refresh();
			if( cboxArchiveType.SelectedIndex == 0 ) {
				FileToRar( lFilesList );
			} else {
				FileToZip( lFilesList );
			}
			DateTime dtEnd = DateTime.Now;
			string sTime = dtEnd.Subtract( dtStart ).ToString() + " (час.:мин.:сек.)";
			MessageBox.Show( "Упаковка fb2-файлов завершена!\nЗатрачено времени: "+sTime, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
			tsslblProgress.Text = m_sReady;
			tsProgressBar.Visible = false;
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
		
		void FileToZip( List<string> lFilesList ) {
			// упаковка fb2-файлов в .fb2.zip
			foreach( string sFile in lFilesList ) {
				string sExt = Path.GetExtension( sFile );
				if( sExt.ToLower() == ".fb2" ) {
					++this.m_lFB2Files;
					// упаковываем
					string sArchiveFile = "";
					string sDotExt = "."+GetArchiveExt( cboxArchiveType.Text );
					if( rbtnToSomeDir.Checked ) {
						sArchiveFile = sFile + sDotExt;
						// замена - удаляем, если такой есть
						if( File.Exists( sArchiveFile ) ) {
							File.Delete( sArchiveFile );
						}
					} else {
						string sSource = tboxSourceDir.Text;
						string sTarget = tboxToAnotherDir.Text;
						sArchiveFile = sTarget + sFile.Remove( 0, sSource.Length ) + sDotExt;
						// замена - удаляем, если такой есть
						FileInfo fi = new FileInfo( sArchiveFile );
						if( !fi.Directory.Exists ) {
							Directory.CreateDirectory( fi.Directory.ToString() );
						}
						if( File.Exists( sArchiveFile ) ) {
							File.Delete( sArchiveFile );
						}
					}
					Archiver.Archiver.zip( "7za.exe", cboxArchiveType.Text.ToLower(), sFile, sArchiveFile );
					if( cboxDelFB2Files.Checked ) {
						// удаляем исходный fb2-файл
						if( File.Exists( sFile ) ) {
							File.Delete( sFile );
						}
					}
				}
				++tsProgressBar.Value;
			}
			lblFB2FilesCount.Text = m_lFB2Files.ToString();
		}
		
		void FileToRar( List<string> lFilesList ) {
			// упаковка fb2-файлов в .fb2.rar
			string sRarPath = m_sRarDir + "\\Rar.exe";
			foreach( string sFile in lFilesList ) {
				string sExt = Path.GetExtension( sFile );
				if( sExt.ToLower() == ".fb2" ) {
					++this.m_lFB2Files;
					// упаковываем
					string sArchiveFile = "";
					if( rbtnToSomeDir.Checked ) {
						sArchiveFile = sFile + ".rar";
						// замена - удаляем, если такой есть
						if( File.Exists( sArchiveFile ) ) {
							File.Delete( sArchiveFile );
						}
					} else {
						string sSource = tboxSourceDir.Text;
						string sTarget = tboxToAnotherDir.Text;
						sArchiveFile = sTarget + sFile.Remove( 0, sSource.Length ) + ".rar";
						// замена - удаляем, если такой есть
						FileInfo fi = new FileInfo( sArchiveFile );
						if( !fi.Directory.Exists ) {
							Directory.CreateDirectory( fi.Directory.ToString() );
						}
						if( File.Exists( sArchiveFile ) ) {
							File.Delete( sArchiveFile );
						}
					}
					Archiver.Archiver.rar( sRarPath, sFile, sArchiveFile, cboxAddRestoreInfo.Checked );
					if( cboxDelFB2Files.Checked ) {
						// удаляем исходный fb2-файл
						if( File.Exists( sFile ) ) {
							File.Delete( sFile );
						}
					}
				}
				++tsProgressBar.Value;
			}
			lblFB2FilesCount.Text = m_lFB2Files.ToString();
		}
		#endregion
	}
}
