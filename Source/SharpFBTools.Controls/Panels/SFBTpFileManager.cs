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
		public ListView GetSettingsInfoListView()
		{
			return lvSettings;
		}
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

			// загружаем в ListView-индикатор настроек данные 
			Settings.Settings.SetInfoSettings( lvSettings );
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
			txtBoxTemplatesFromLine.Focus();
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
			
			string sLineTemplate = "";
			if( rBtnTemplatesPrepared.Checked ) {
				sLineTemplate = cboxTemplatesPrepared.Text.Trim();
			} else {
				sLineTemplate = txtBoxTemplatesFromLine.Text.Trim();
			}
			// проверка "пустоту" строки с шаблонами
			if( sLineTemplate == "" ) {
				MessageBox.Show( "Строка шаблонов не может быть пустой!\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			// проверка на корректность строки с шаблонами
			if( !FilesWorker.TemplatesVerify.IsLineTemplatesCorrect( sLineTemplate ) ) {
				MessageBox.Show( "Строка содержит или недопустимые шаблоны,\nили недопустимые символы */|?<>\"&«» между шаблонами!\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			// проверка на четность * в строке с шаблонами
			if( !FilesWorker.TemplatesVerify.IsEvenElements( sLineTemplate, '*' ) ) {
				MessageBox.Show( "Строка с шаблонами подстановки содержит нечетное число *!\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			// проверка, не стоит ли ] перед [
			if( sLineTemplate.IndexOf('[')!=-1 && sLineTemplate.IndexOf(']')!=-1 ) {
				if( sLineTemplate.IndexOf('[') > sLineTemplate.IndexOf(']') ) {
					MessageBox.Show( "В строке с шаблонами закрывающая скобка ] не может стоять перед открывающей [ !\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return;
				}
			}
			
			// проверка на соответствие [ ] в строке с шаблонами
			if( !FilesWorker.TemplatesVerify.IsBracketsCorrect( sLineTemplate, '[', ']' ) ) {
				MessageBox.Show( "В строке с шаблонами переименования нет соответствия между открывающим и закрывающими скобками [ ]!\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			// проверка на соответствие ( ) в строке с шаблонами
			if( !FilesWorker.TemplatesVerify.IsBracketsCorrect( sLineTemplate, '(', ')' ) ) {
				MessageBox.Show( "В строке с шаблонами нет соответствия между открывающим и закрывающими скобками ( )!\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			// проверка на \ в начале строки с шаблонами
			if( sLineTemplate[0]=='\\' ) {
				MessageBox.Show( "Строка с шаблонами не может начинаться с '\\'!\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			// проверка на \ в конце строки с шаблонами
			if( sLineTemplate[sLineTemplate.Length-1]=='\\' ) {
				MessageBox.Show( "Строка с шаблонами не может заканчиваться на '\\' !\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			// проверка условных шаблонов на наличие в них вспом. символов без самих шаблонов
			if( !FilesWorker.TemplatesVerify.IsConditionalPatternCorrect( sLineTemplate ) ) {
				MessageBox.Show( "Условные шаблоны [] в строке с шаблонами не могут содержать вспомогательных символов БЕЗ самих шаблонов!\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			// проверка на множественность символа папки \ в строке с шаблонами
			if( sLineTemplate.IndexOf( "\\\\" )!=-1 ) {
				MessageBox.Show( "Строка с шаблонами не может содержать несколько идущих подряд символов папки '\\' !\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
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
				lDirList.Add( sSource );
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
			string sTempDir = Settings.Settings.GetTempDir();
			string sArchType = FilesWorker.StringProcessing.GetArchiveExt( Settings.Settings.ReadArchiveTypeText() );
			foreach( string sFromFilePath in lFilesList ) {
				// смотрим, что это за файл
				string sExt = Path.GetExtension( sFromFilePath ).ToLower();
				if( sExt == ".fb2" ) {
					// обработка fb2-файла
					// TODO: Вместо sToFilePath - ф-я формирования пути файла из его данных Description
					// TODO: пока просто копирует ВСЕ файлы. Надо сделать отлов только fb2 и всех архивов. Для Архивов - копирование распакованного
					// TODO: Если опция запаковать включена - тогда и fb2 и из архивов - запаковать и скопировать
					string sToFilePath = sTarget + sFromFilePath.Remove( 0, sSource.Length );
					string s = FilesWorker.TemplatesParser.Parse( sLineTemplate, sFromFilePath );
					if( !Settings.Settings.ReadToArchiveMode() ) {
						CopyFileToTargetDir( sFromFilePath, sToFilePath, nFileExistMode, bAddToFileNameBookIDMode );
					} else {
						// упаковка в архив
						CopyFileToArchive( sArchType, sFromFilePath, sToFilePath+"."+sArchType, nFileExistMode, bAddToFileNameBookIDMode );
					}
					if( bDelFB2FilesMode ) {
						// удаляем исходный fb2-файл
						if( File.Exists( sFromFilePath ) ) {
							File.Delete( sFromFilePath );
						}
					}
					++tsProgressBar.Value;
					ssProgress.Refresh();
				} else {
					// это архив?
					if( IsArchive( sExt ) ) {
						List<string> lFilesListFromArchive = GetFileListFromArchive( sFromFilePath );
						// TODO: Все вышеизложенное, тольео для каждого файла из списка lFilesListFromArchive
						foreach( string sFromFB2Path in lFilesListFromArchive ) {
							string sToFilePath = sTarget + sFromFB2Path.Remove( 0, sTempDir.Length );
							if( !Settings.Settings.ReadToArchiveMode() ) {
								CopyFileToTargetDir( sFromFB2Path, sToFilePath, nFileExistMode, bAddToFileNameBookIDMode );
							} else {
								// упаковка в архив
								CopyFileToArchive( sArchType, sFromFB2Path, sToFilePath+"."+sArchType, nFileExistMode, bAddToFileNameBookIDMode );
							}
						}
						if( bDelFB2FilesMode ) {
							// удаляем исходный fb2-файл
							if( File.Exists( sFromFilePath ) ) {
								File.Delete( sFromFilePath );
							}
						}
						++tsProgressBar.Value;
						ssProgress.Refresh();
					} else {
						// пропускаем не fb2-файлы и архивы
						++tsProgressBar.Value;
						ssProgress.Refresh();
						continue;
					}
				}
			}
			FilesWorker.FilesWorker.RemoveDir( sTempDir );
			DateTime dtEnd = DateTime.Now;
			string sTime = dtEnd.Subtract( dtStart ).ToString() + " (час.:мин.:сек.)";
			string sMess = "Сортировка файлов в указанную папку завершена!\nЗатрачено времени: "+sTime;
			MessageBox.Show( sMess, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
			tsslblProgress.Text = Settings.Settings.GetReady();
			tsProgressBar.Visible = false;
		}
		
		private void CopyFileToArchive( string sArchType, string sFromFilePath, string sToFilePath, int nFileExistMode, bool bAddToFileNameBookIDMode )
		{
			// архивирование файла с сформированным именем (путь)
			string s7zPath = Settings.Settings.Read7zaPath();
			string sRarPath = Settings.Settings.ReadRarPath();
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
						sSufix = FilesWorker.StringProcessing.GetFMBookID( sFromFilePath );
					}
					sSufix += FilesWorker.StringProcessing.GetDateTimeExt();
					sToFilePath = sToFilePath.Remove( sToFilePath.Length - (sArchType.Length+5) ) + sSufix + ".fb2." + sArchType;
				}
			}
			if( sArchType == "rar" ) {
				Archiver.Archiver.rar( sRarPath, sFromFilePath, sToFilePath, true );
			} else {
				Archiver.Archiver.zip( s7zPath, sArchType, sFromFilePath, sToFilePath );
			}
		}
		
		private void CopyFileToTargetDir( string sFromFilePath, string sToFilePath, int nFileExistMode, bool bAddToFileNameBookIDMode )
		{
			// копирование файла с сформированным именем (путь)
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
						sSufix = FilesWorker.StringProcessing.GetFMBookID( sFromFilePath );
					}
					sSufix += FilesWorker.StringProcessing.GetDateTimeExt();
					sToFilePath = sToFilePath.Remove( sToFilePath.Length-4 ) + sSufix + ".fb2";
				}
			}
			if( File.Exists( sFromFilePath ) ) {
				File.Copy( sFromFilePath, sToFilePath );
			}
		}
		
		private List<string> GetFileListFromArchive( string sFromFile ) {
			// Распаковать архив во временную папку
			List<string> lFilesList = new List<string>(); // список fb2-файлов из файла-архива sFromFile
			string sTempDir 	= Settings.Settings.GetTempDir();
			string sExt			= Path.GetExtension( sFromFile ).ToLower();
			string s7zaPath		= Settings.Settings.Read7zaPath();
			string sUnRarPath	= Settings.Settings.ReadUnRarPath();
			if( sExt != "" ) {
				FilesWorker.FilesWorker.RemoveDir( sTempDir );
				switch( sExt ) {
					case ".rar":
						if( !Directory.Exists( sTempDir ) ) {
							Directory.CreateDirectory( sTempDir );
						}
						Archiver.Archiver.unrar( sUnRarPath, sFromFile, sTempDir );
						break;
					case ".zip":
						Archiver.Archiver.unzip( s7zaPath, sFromFile, sTempDir );
						break;
					case ".7z":
						Archiver.Archiver.unzip( s7zaPath, sFromFile, sTempDir );
						break;
					case ".bz2":
						Archiver.Archiver.unzip( s7zaPath, sFromFile, sTempDir );
						break;
					case ".gz":
						Archiver.Archiver.unzip( s7zaPath, sFromFile, sTempDir );
						break;
					case ".tar":
						Archiver.Archiver.unzip( s7zaPath, sFromFile, sTempDir );
						break;
				}
				// составляем список файлов (или одного) из архива
				if( Directory.Exists( sTempDir ) ) {
					string [] files = Directory.GetFiles( sTempDir );
					foreach( string sFile in files ) {
						if( Path.GetExtension( Path.GetFileName( sFile ) )==".fb2" ) {
							lFilesList.Add( sFile );
						}
					}
				}
			}
			return lFilesList;
		}
				
		private bool IsArchive( string sExt )
		{
			return ( sExt==".zip" || sExt==".rar" || sExt==".7z" || sExt==".bz2" || sExt==".gz" || sExt==".tar" );
		}
		
	}
}
