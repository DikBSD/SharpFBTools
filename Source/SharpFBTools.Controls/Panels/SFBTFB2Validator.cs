/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 13.03.2009
 * Time: 14:34
 * 
 * License: GPL 2.1
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

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
		#endregion
		
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
	}
}
