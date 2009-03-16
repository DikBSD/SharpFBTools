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

namespace SharpFBTools.Controls.Panels
{
	/// <summary>
	/// Description of SFBTpValidator.
	/// </summary>
	public partial class SFBTpFB2Validator : UserControl
	{
		public SFBTpFB2Validator()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			// инициализация контролов
			Init();
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
		//
		private long	m_lFB2Valid		= 0; // число валидных файлов
		private long	m_lFB2NotValid	= 0; // число не валидных файлов
		private long	m_lFB2Files		= 0; // число fb2 файлов (не сжатых)
		private long	m_lFB2ZipFiles	= 0; // число fb2.zip файлов
		private long	m_lFB2RarFiles	= 0; // число fb2.rar файлов
		private long	m_lNonFB2Files	= 0; // число других (не fb2) файлов
		//
		private	string	m_sReady		= "Готово.";
		private string	m_sNotValid		= " Не валидные fb2-файлы ";
		private	string	m_sValid		= " Валидные fb2-файлы ";
		private string	m_sNotFB2Files	= " Не fb2-файлы ";
		// Report
		private string	m_FB2NotValidReportEmpty	= "Список не валидных fb2-файлов пуст!\nОтчет не сохранен.";
		private string	m_FB2ValidReportEmpty		= "Список валидных fb2-файлов пуст!\nОтчет не сохранен.";
		private string	m_NotFB2FileReportEmpty		= "Список не fb2-файлов пуст!\nОтчет не сохранен.";
		private string	m_FB2NotValidFilesListReport = "Список не валидных fb2-файлов";
		private string	m_FB2ValidFilesListReport 	= "Список валидных fb2-файлов";
		private string	m_NotFB2FilesListReport 	= "Список не fb2-файлов";
		private string	m_ReportSaveOk = "Отчет сохранен в файл:\n";
		private string	m_HTMLFilter 	= "HTML файлы (*.hml)|*.html|Все файлы (*.*)|*.*";
		private string	m_FB2Filter 	= "fb2 файлы (*.fb2)|*.fb2|Все файлы (*.*)|*.*";
		private string	m_CSV_csv_Filter = "CVS файлы (*.csv)|*.csv|Все файлы (*.*)|*.*";
		private string	m_CSV_txt_Filter = "Txt файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
		#endregion
		
		private void Init() {
			// инициализация контролов и переменных
			listViewNotValid.Items.Clear();
			listViewValid.Items.Clear();
			listViewNotFB2.Items.Clear();
			rеboxNotValid.Clear();
			tpNonValid.Text		= m_sNotValid;
			tpValid.Text		= m_sValid;
			tpNotFB2Files.Text	= m_sNotFB2Files;
			lblDirsCount.Text	= "0";
			lblFilesCount.Text	= "0";
			lblFB2FilesCount.Text 		= "0";
			lblFB2ZipFilesCount.Text 	= "0";
			lblFB2RarFilesCount.Text 	= "0";
			lblNotFB2FilesCount.Text 	= "0";
			tsProgressBar.Value	= 1;
			tsslblProgress.Text		= m_sReady;
			tsProgressBar.Visible	= false;
			m_lFB2Valid		= 0;
			m_lFB2NotValid 	= 0;
			m_lFB2Files		= 0;
			m_lFB2ZipFiles 	= 0;
			m_lFB2RarFiles 	= 0;
			m_lNonFB2Files 	= 0;
			// очистка временной папки
			FilesWorker.FilesWorker.RemoveDir( FilesWorker.FilesWorker.GetTempDir() );
		}
		
		#region Парсеры файлов и архивов
		private void ParseFB2File( string sFile, FB2Parser.FB2Validator fv2Validator ) {
			// парсер несжатого fb2-файла
			string msg = fv2Validator.ValidatingFB2File( sFile );
			if ( msg == "" ) {
           		// файл валидный
           		++this.m_lFB2Valid;
				//listViewValid.Items.Add( sFile );
				ListViewItem item = new ListViewItem( sFile, 0 );
   				item.ForeColor = m_FB2ValidFontColor;
				FileInfo fi = new FileInfo( sFile );
   				item.SubItems.Add( FilesWorker.FilesWorker.FormatFileLenght( fi.Length ) );
				listViewValid.Items.AddRange( new ListViewItem[]{ item } );
				tpValid.Text = m_sValid + "( " + m_lFB2Valid.ToString() + " ) " ;
           	} else {
           		// файл не валидный
           		++this.m_lFB2NotValid;
				ListViewItem item = new ListViewItem( sFile, 0 );
   				item.ForeColor = m_FB2NotValidFontColor;
				item.SubItems.Add( msg );
   				FileInfo fi = new FileInfo( sFile );
   				item.SubItems.Add( FilesWorker.FilesWorker.FormatFileLenght( fi.Length ) );
				listViewNotValid.Items.AddRange( new ListViewItem[]{ item } );
				tpNonValid.Text = m_sNotValid + "( " + m_lFB2NotValid.ToString() + " ) " ;
           	}
			++tsProgressBar.Value;
		}
		
		private void ParseArchiveFile( string sArchiveFile, FB2Parser.FB2Validator fv2Validator, string sTempDir ) {
			// парсер архива
			string sExt = Path.GetExtension( sArchiveFile );
			if( sExt.ToLower() == ".zip" ) {
				Archiver.Archiver.unzip( sArchiveFile, sTempDir );
			} else if( sExt.ToLower() == ".rar" ) {
				Archiver.Archiver.unrar( sArchiveFile, sTempDir );
			}
			string [] files = Directory.GetFiles( sTempDir );
			if( files.Length <= 0 ) return;
				
			string msg = fv2Validator.ValidatingFB2File( files[0] );
			string sFileName = Path.GetFileName( files[0] );

			if ( msg == "" ) {
           		// файл валидный - это fb2
           		++this.m_lFB2Valid;
				//listViewValid.Items.Add( sArchiveFile + "/" + sFileName );
				ListViewItem item = new ListViewItem( sArchiveFile + "/" + sFileName , 0 );
				if( sExt.ToLower() == ".zip" ) {
					item.ForeColor = m_ZipFB2ValidFontColor;
					++this.m_lFB2ZipFiles;
				} else if( sExt.ToLower() == ".rar" ) {
					item.ForeColor = m_RarFB2ValidFontColor;
					++this.m_lFB2RarFiles;
				}
				FileInfo fi = new FileInfo( sArchiveFile );
   				string s = FilesWorker.FilesWorker.FormatFileLenght( fi.Length );
   				fi = new FileInfo( FilesWorker.FilesWorker.GetTempDir()+"\\"+sFileName );
   				s += " / "+FilesWorker.FilesWorker.FormatFileLenght( fi.Length );
   				item.SubItems.Add( s );
				listViewValid.Items.AddRange( new ListViewItem[]{ item } );
				tpValid.Text = m_sValid + "( " + this.m_lFB2Valid.ToString() + " ) " ;
           	} else {
           		// архив не валидный - посмотрим, что это
        		if( sExt.ToLower() == ".zip" ) {
					// определяем тип разархивированного файла
           			sExt = Path.GetExtension( sFileName );
					if( sExt.ToLower() != ".fb2" ) {
           				msg = "Тип файла: " + sExt;
           				++this.m_lNonFB2Files;
           				ListViewItem item = new ListViewItem( sArchiveFile + "/" + sFileName, 0 );
   						item.ForeColor = m_ZipFontColor;
           				item.SubItems.Add( Path.GetExtension( sArchiveFile + "/" + sFileName ) );
   						FileInfo fi = new FileInfo( sArchiveFile );
   						string s = FilesWorker.FilesWorker.FormatFileLenght( fi.Length );
   						fi = new FileInfo( FilesWorker.FilesWorker.GetTempDir()+"\\"+sFileName );
   						s += " / "+FilesWorker.FilesWorker.FormatFileLenght( fi.Length );
   						item.SubItems.Add( s );
   						listViewNotFB2.Items.AddRange( new ListViewItem[]{ item } );
						tpNotFB2Files.Text = this.m_sNotFB2Files + "( " + this.m_lNonFB2Files.ToString() + " ) ";
					} else {
						++this.m_lFB2ZipFiles;
						++this.m_lFB2NotValid;
						ListViewItem item = new ListViewItem( sArchiveFile + "/" + sFileName, 0 );
   						item.ForeColor = m_ZipFB2NotValidFontColor;
						item.SubItems.Add( msg );
   						FileInfo fi = new FileInfo( sArchiveFile );
   						string s = FilesWorker.FilesWorker.FormatFileLenght( fi.Length );
   						fi = new FileInfo( FilesWorker.FilesWorker.GetTempDir()+"\\"+sFileName );
   						s += " / "+FilesWorker.FilesWorker.FormatFileLenght( fi.Length );
   						item.SubItems.Add( s );
						listViewNotValid.Items.AddRange( new ListViewItem[]{ item } );
						tpNonValid.Text = this.m_sNotValid + "( " + this.m_lFB2NotValid.ToString() + " ) ";
					}
				} else if( sExt.ToLower() == ".rar" ) {
					// определяем тип разархивированного файла
           			sExt = Path.GetExtension( sFileName );
					if( sExt.ToLower() != ".fb2" ) {
        				msg = "Тип файла: " + sExt;
          				++this.m_lNonFB2Files;
          				ListViewItem item = new ListViewItem( sArchiveFile + "/" + sFileName, 0 );
   						item.ForeColor = m_RarFontColor;
          				item.SubItems.Add( Path.GetExtension( sArchiveFile + "/" + sFileName ) );
   						FileInfo fi = new FileInfo( sArchiveFile );
   						string s = FilesWorker.FilesWorker.FormatFileLenght( fi.Length );
   						fi = new FileInfo( FilesWorker.FilesWorker.GetTempDir()+"\\"+sFileName );
   						s += " / "+FilesWorker.FilesWorker.FormatFileLenght( fi.Length );
   						item.SubItems.Add( s );
   						listViewNotFB2.Items.AddRange( new ListViewItem[]{ item } );
						tpNotFB2Files.Text = this.m_sNotFB2Files + "( " + this.m_lNonFB2Files.ToString() + " ) ";
					} else {
						++this.m_lFB2RarFiles;
						++this.m_lFB2NotValid;
						ListViewItem item = new ListViewItem( sArchiveFile + "/" + sFileName, 0 );
   						item.ForeColor = m_RarFB2NotValidFontColor;
						item.SubItems.Add( msg );
   						FileInfo fi = new FileInfo( sArchiveFile );
   						string s = FilesWorker.FilesWorker.FormatFileLenght( fi.Length );
   						fi = new FileInfo( FilesWorker.FilesWorker.GetTempDir()+"\\"+sFileName );
   						s += " / "+FilesWorker.FilesWorker.FormatFileLenght( fi.Length );
   						item.SubItems.Add( s );
						listViewNotValid.Items.AddRange( new ListViewItem[]{ item } );
						tpNonValid.Text = this.m_sNotValid + "( " + this.m_lFB2NotValid.ToString() + " ) ";
					}
				}
            }
			++tsProgressBar.Value;
		}
		#endregion
		
		#region Обработчики событий
		void ListViewNotValidSelectedIndexChanged(object sender, EventArgs e)
		{
			// занесение ошибки валидации в бокс
			ListView.SelectedListViewItemCollection si = listViewNotValid.SelectedItems;
			foreach ( ListViewItem item in si ) {
				rеboxNotValid.Text = item.SubItems[1].Text;
			}
		}
		
		void ListViewNonValidDoubleClick(object sender, EventArgs e)
		{
			// открытие папки с указанным файлом
			FilesWorker.FilesWorker.ShowDir( listViewNotValid );
		}
		
		void ListViewValidColumnClick(object sender, ColumnClickEventArgs e)
		{
			// открытие папки с указанным файлом
			FilesWorker.FilesWorker.ShowDir( listViewValid );
		}
		
		void ListViewNotFB2DoubleClick(object sender, EventArgs e)
		{
			// открытие папки с указанным файлом
			FilesWorker.FilesWorker.ShowDir( listViewNotFB2 );
		}
		
		void BtnFB2NotValidCopyToClick(object sender, EventArgs e)
		{
			// задание папки для копирования невалидных fb2-файлов
			DialogResult result = fbdNotValidFB2.ShowDialog();
			if (result == DialogResult.OK) {
                string openFolderName = fbdNotValidFB2.SelectedPath;
                tboxFB2NotValidDirCopyTo.Text = openFolderName;
            }
		}
		
		void BtnFB2NotValidMoveToClick(object sender, EventArgs e)
		{
			// задание папки для перемещения невалидных fb2-файлов
			DialogResult result = fbdNotValidFB2.ShowDialog();
			if (result == DialogResult.OK) {
                string openFolderName = fbdNotValidFB2.SelectedPath;
                tboxFB2NotValidDirMoveTo.Text = openFolderName;
            }
		}
		
		void BtnFB2ValidCopyToClick(object sender, EventArgs e)
		{
			// задание папки для валидных fb2-файлов
			DialogResult result = fbdValidFB2.ShowDialog();
			if (result == DialogResult.OK) {
                string openFolderName = fbdValidFB2.SelectedPath;
                tboxFB2ValidDirCopyTo.Text = openFolderName;
            }
		}
		
		void BtnFB2ValidMoveToClick(object sender, EventArgs e)
		{
			// задание папки для перемещения валидных fb2-файлов
			DialogResult result = fbdValidFB2.ShowDialog();
			if (result == DialogResult.OK) {
                string openFolderName = fbdValidFB2.SelectedPath;
                tboxFB2ValidDirMoveTo.Text = openFolderName;
            }
		}
		
		void BtnNotFB2CopyToClick(object sender, EventArgs e)
		{
			// задание папки для не fb2-файлов
			DialogResult result = fbdNotFB2.ShowDialog();
			if (result == DialogResult.OK) {
                string openFolderName = fbdNotFB2.SelectedPath;
                tboxNotFB2DirCopyTo.Text = openFolderName;
            }
		}
		
		void BtnNotFB2MoveToClick(object sender, EventArgs e)
		{
			// задание папки для перемещения не fb2-файлов
			DialogResult result = fbdNotFB2.ShowDialog();
			if (result == DialogResult.OK) {
                string openFolderName = fbdNotFB2.SelectedPath;
                tboxNotFB2DirMoveTo.Text = openFolderName;
            }
		}
		
		void TsbtnOpenDirClick(object sender, EventArgs e)
		{
			// задание папки с fb2-файлами для сканирования
			DialogResult result = fbdSource.ShowDialog();
			if (result == DialogResult.OK) {
                string openFolderName = fbdSource.SelectedPath;
                tboxSourceDir.Text = openFolderName;
                // инициализация контролов
				Init();
            }
		}
		#endregion
		
		void TSBValidateClick(object sender, EventArgs e)
		{
			// Ввлидация fb2-файлов в выбранной папке
			tlCentral.Refresh(); // обновление контролов на форме
			if( tboxSourceDir.Text == "" ) {
				MessageBox.Show( "Выберите папку для сканирования!", "FB2SharpValidator", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			DirectoryInfo diFolder = new DirectoryInfo(tboxSourceDir.Text);
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка не найдена:" + tboxSourceDir.Text, "FB2SharpValidator", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			
			DateTime dtStart = DateTime.Now;
			// инициализация контролов
			Init();
			tsProgressBar.Visible = true;
			// сортированный список всех вложенных папок
			List<string> lDirList = FilesWorker.FilesWorker.DirsParser( diFolder.FullName, tlCentral, lblDirsCount );
			lDirList.Sort();
			// сортированный список всех fb2,zip и rar файлов
			tsslblProgress.Text = "Создание списка файлов:";
			tlCentral.Refresh(); // обновление контролов на форме
			List<string> lFilesList = FilesWorker.FilesWorker.AllFilesParser( lDirList, tlCentral, lblFilesCount, tsProgressBar );
			lFilesList.Sort();
			
			if( lFilesList.Count == 0 ) {
				MessageBox.Show( "Не найдено ни одного файла!\nРабота прекращена.", "FB2SharpValidator", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				Init();
				return;
			}
			
			// проверка файлов
			tsslblProgress.Text = "Проверка найденных файлов на соответствие схеме (валидация):";
			tsProgressBar.Maximum = lFilesList.Count+1;
			tsProgressBar.Value = 1;
			tlCentral.Refresh(); // обновление контролов на форме
			FB2Parser.FB2Validator fv2V = new FB2Parser.FB2Validator();
			foreach( string sFile in lFilesList ) {
				string sExt = Path.GetExtension( sFile );
				if( sExt.ToLower() == ".fb2" ) {
					++this.m_lFB2Files;
					ParseFB2File( sFile, fv2V );
				} else if( sExt.ToLower() == ".zip" || sExt.ToLower() == ".rar" ) {
					// очистка временной папки
					FilesWorker.FilesWorker.RemoveDir( FilesWorker.FilesWorker.GetTempDir() );
					Directory.CreateDirectory( FilesWorker.FilesWorker.GetTempDir() );
					ParseArchiveFile( sFile, fv2V, FilesWorker.FilesWorker.GetTempDir() );
				} else {
					// разные файлы
					++this.m_lNonFB2Files;
					ListViewItem item = new ListViewItem( sFile, 0 );
   					item.ForeColor = m_NotFB2FontColor;
					item.SubItems.Add( Path.GetExtension( sFile ) );
   					FileInfo fi = new FileInfo( sFile );
   					item.SubItems.Add( FilesWorker.FilesWorker.FormatFileLenght( fi.Length ) );
					listViewNotFB2.Items.AddRange( new ListViewItem[]{ item } );
					tpNotFB2Files.Text = m_sNotFB2Files + "( " + m_lNonFB2Files.ToString() + " ) " ;
					++tsProgressBar.Value;
				}
			}
			lblFB2FilesCount.Text		= m_lFB2Files.ToString();
			lblFB2ZipFilesCount.Text	= m_lFB2ZipFiles.ToString();
			lblFB2RarFilesCount.Text 	= m_lFB2RarFiles.ToString();
			lblNotFB2FilesCount.Text 	= m_lNonFB2Files.ToString();
			
			DateTime dtEnd = DateTime.Now;
			string sTime = dtEnd.Subtract( dtStart ).ToString() + " (час. : мин. : сек.)";
			MessageBox.Show( "Проверка файлов на соответствие FictionBook.xsd схеме завершена!\nЗатрачено времени: "+sTime, "FB2SharpValidator", MessageBoxButtons.OK, MessageBoxIcon.Information );
			tsslblProgress.Text = m_sReady;
			tsProgressBar.Visible = false;
			// очистка временной папки
			FilesWorker.FilesWorker.RemoveDir( FilesWorker.FilesWorker.GetTempDir() );
		}
	}
}
