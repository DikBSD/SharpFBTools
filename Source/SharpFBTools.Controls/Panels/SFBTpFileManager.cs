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
using FB2.FB2Parsers;
using FB2.Common;
using FB2.Description;
using FB2.Description.TitleInfo;
using FB2.Description.DocumentInfo;
using FB2.Description.PublishInfo;
using FB2.Description.CustomInfo;
using FB2.Description.Common;

namespace SharpFBTools.Controls.Panels
{
	/// <summary>
	/// Description of SFBTpFileManager.
	/// </summary>
	public partial class SFBTpFileManager : UserControl
	{
		public SFBTpFileManager()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			cboxTemplatesPrepared.SelectedIndex = 0;
			Init();
			
			string sTDPath = Settings.Settings.GetDefFMDescTemplatePath();
			if( File.Exists( sTDPath ) ) {
				richTxtBoxDescTemplates.LoadFile( sTDPath );
			} else {
				richTxtBoxDescTemplates.Text = "Не найден файл описания Шаблонов переименования: \""+sTDPath+"\"";
			}

			FB2.FB2Parsers.FB2Parser fb2p = new FB2.FB2Parsers.FB2Parser( "d:\\1.fb2" );
			TitleInfo ti = fb2p.GetTitleInfo();
			DocumentInfo di = fb2p.GetDocumentInfo();
			string sID = di.ID;
		}
		
		#region Закрытые вспомогательные методы класса
		private void Init() {
			// инициализация контролов и переменных
			for( int i=0; i!=lvFilesCount.Items.Count; ++i ) {
				lvFilesCount.Items[i].SubItems[1].Text	= "0";
			}
			tsProgressBar.Value	= 1;
			tsslblProgress.Text		= Settings.Settings.GetReady();
			tsProgressBar.Visible	= false;
//			m_lFB2Valid	= m_lFB2NotValid = m_lFB2Files = m_lFB2ZipFiles = m_lFB2RarFiles = m_lNotFB2Files = 0;
			// очистка временной папки
			FilesWorker.FilesWorker.RemoveDir( Settings.Settings.GetTempDir() );
		}
		#endregion
		
		#region Обработчики событий
		void TsbtnOpenDirClick(object sender, EventArgs e)
		{
			// задание папки с fb2-файлами и архивами для сканирования
			if( tboxSourceDir.Text !="" ) {
				fbdScanDir.SelectedPath = tboxSourceDir.Text;
			}
			DialogResult result = fbdScanDir.ShowDialog();
			if (result == DialogResult.OK) {
				string openFolderName = fbdScanDir.SelectedPath;
                tboxSourceDir.Text = openFolderName;
            }
		}

		void RBtnTemplatesPreparedCheckedChanged(object sender, EventArgs e)
		{
			cboxTemplatesPrepared.Enabled = rBtnTemplatesPrepared.Checked;
		}
		
		void RBtnTemplatesFromLineCheckedChanged(object sender, EventArgs e)
		{
			txtBoxTemplatesFromLine.Enabled = rBtnTemplatesFromLine.Checked;
		}
		
		void TsbtnSortFilesToClick(object sender, EventArgs e)
		{
			// сортировка
			string sSource = tboxSourceDir.Text.Trim();
			if( sSource == "" ) {
				MessageBox.Show( "Выберите папку для сканирования!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			DirectoryInfo diFolder = new DirectoryInfo( sSource );
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка не найдена: " + sSource, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			
			if( tcFileManager.SelectedIndex == 0 ) {
				SortFull( sSource );
			} else {
				MessageBox.Show( "Режим Избранной сортировки еще не готов.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
		}
		
		void BtnSortAllToDirClick(object sender, EventArgs e)
		{
			// задание папки-приемника для размешения отсортированных файлов
			FilesWorker.FilesWorker.OpenDirDlg( tboxSortAllToDir, fbdScanDir, "Укажите папку-приемник для размешения отсортированных файлов:" );
		}
		#endregion
		
		private void SortFull( string sSource )
		{
			// полная сортировка файлов
			string sTarget = tboxSortAllToDir.Text.Trim();
			if( sTarget == "") {
				MessageBox.Show( "Не указана папка-приемник файлов!\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			if( sSource == sTarget ) {
				MessageBox.Show( "Папка-приемник файлов совпадает с папкой сканирования!\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			DirectoryInfo diFolder = new DirectoryInfo( sTarget );
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка не найдена: " + sTarget + "\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			
			DateTime dtStart = DateTime.Now;
			// инициализация контролов
			Init();
			tsProgressBar.Visible = true;
			// сортированный список всех вложенных папок
			List<string> lDirList = new List<string>();
			if( !chBoxScanSubDir.Checked ) {
				// сканировать только указанную папку
				lDirList.Add( diFolder.FullName );
				lvFilesCount.Items[0].SubItems[1].Text = "1";
				lvFilesCount.Refresh();
			} else {
				// сканировать и все подпапки
				lDirList = FilesWorker.FilesWorker.DirsParser( sSource, lvFilesCount );
				lDirList.Sort();
			}
			// сортированный список всех файлов
			tsslblProgress.Text = "Создание списка файлов:";
			List<string> lFilesList = FilesWorker.FilesWorker.AllFilesParser( lDirList, ssProgress, lvFilesCount, tsProgressBar );
			lFilesList.Sort();
			int nFilesCount = lFilesList.Count;
			if( nFilesCount == 0 ) {
				MessageBox.Show( "В папке сканирования не найдено ни одного файла!\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
				tsslblProgress.Text = Settings.Settings.GetReady();
				tsProgressBar.Visible = false;
				return;
			}
			
			// сортировка
			tsslblProgress.Text = "Сортировка файлов:";
			tsProgressBar.Visible = true;
			tsProgressBar.Maximum = nFilesCount+1;
			tsProgressBar.Value = 1;
			int nFileExistMode = Settings.Settings.ReadFileExistMode();
			bool bAddToFileNameBookIDMode = Settings.Settings.ReadAddToFileNameBookIDMode();
			bool bDelFB2FilesMode = Settings.Settings.ReadDelFB2FilesMode();
			for( int i=0; i!=nFilesCount; ++i ) {
				// TODO: Вместо sToFilePath - ф-я фопмирования пути файла из его данных Description
				// TODO: пока просто копирует ВСЕ файлы. Надо сделать отлов только fb2 и всех архивов. Для Архивов - копирование распакованного
				// TODO: Если опция запаковать включена - тогда и fb2 и из архивов - запаковать и скопировать
				string sFromFilePath = lFilesList[i];
				string sToFilePath = sTarget + sFromFilePath.Remove( 0, sSource.Length );
				CopyFileToTargetDir( sFromFilePath, sToFilePath, nFileExistMode, bAddToFileNameBookIDMode );
				if( bDelFB2FilesMode ) {
					// удаляем исходный fb2-файл
					if( File.Exists( sFromFilePath ) ) {
						File.Delete( sFromFilePath );
					}
				}
				++tsProgressBar.Value;
				ssProgress.Refresh();
			}
			string sMess = "Сортировка файлов в указанную папку завершена!";
			MessageBox.Show( sMess, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
			tsslblProgress.Text = Settings.Settings.GetReady();
			tsProgressBar.Visible = false;
		}
		
		private void CopyFileToTargetDir( string sFromFilePath, string sToFilePath, int nFileExistMode, bool bAddToFileNameBookIDMode )
		{
			// копирование файла с сформированным именем (путь)
			FileInfo fi = new FileInfo( sToFilePath );
			if( !fi.Directory.Exists ) {
				Directory.CreateDirectory( fi.Directory.ToString() );
			}
			string sSufix = "";
			if( File.Exists( sToFilePath ) ) {
				if( nFileExistMode == 0 ) {
					File.Delete( sToFilePath );
				} else {
					if( bAddToFileNameBookIDMode ) {
						FB2.FB2Parsers.FB2Parser fb2p = new FB2.FB2Parsers.FB2Parser( sFromFilePath );
						DocumentInfo di = fb2p.GetDocumentInfo();
						sSufix = "_"+ ( di.ID != null ? di.ID : Settings.Settings.GetNoID() );
					}
					string sExt = Path.GetExtension( sToFilePath );
					DateTime dt = DateTime.Now;
					sSufix += "_"+dt.Year.ToString()+"-"+dt.Month.ToString()+"-"+dt.Day.ToString()+"-"+
								dt.Hour.ToString()+"-"+dt.Minute.ToString()+"-"+dt.Second.ToString()+"-"+dt.Millisecond.ToString();
					sToFilePath = sToFilePath.Remove( sToFilePath.Length - sExt.Length ) + sSufix + sExt;
				}
			}
			if( File.Exists( sFromFilePath ) ) {
				File.Copy( sFromFilePath, sToFilePath );
			}
		}
	}
}
