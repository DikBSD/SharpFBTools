/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 19.09.2013
 * Time: 13:50
 * 
 * License: GPL 2.1
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using Core.FileManager;
using Core.Misc;

using filesWorker		= Core.FilesWorker.FilesWorker;
using archivesWorker	= Core.FilesWorker.Archiver;

namespace SharpFBTools.Tools
{
	/// <summary>
	/// Description of SFBTpFB2DescEditor.
	/// </summary>
	public partial class SFBTpFB2DescEditor : UserControl
	{
		#region Закрытые данные класса
		private string m_CurrentDir = "";
		//private StatusView	m_sv 			= null;
		private DateTime	m_dtStart;
        private BackgroundWorker m_bw		= null;
        //private BackgroundWorker m_bwcmd	= null;
        private string	m_sSource			= "";
        private bool	m_bScanSubDirs		= true;
        private string	m_sMessTitle		= "";
		private MiscListView m_mscLV		= new MiscListView(); // класс по работе с ListView
		private const string space		= " "; // для задания отступов данных от границ колонов в Списке
		#endregion
		public SFBTpFB2DescEditor()
		{
			InitializeComponent();
			
			lvFilesCount.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
		}
		
		#region Закрытые методы реализации BackgroundWorker
		private void InitializeBackgroundWorker() {
			// Инициализация перед использование BackgroundWorker 
//            m_bw = new BackgroundWorker();
//            m_bw.WorkerReportsProgress		= true; // Позволить выводить прогресс процесса
//            m_bw.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
//            m_bw.DoWork 			+= new DoWorkEventHandler( bw_DoWork );
//            m_bw.ProgressChanged 	+= new ProgressChangedEventHandler( bw_ProgressChanged );
//            m_bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler( bw_RunWorkerCompleted );
		}
		#endregion
		
		#region Закрытые вспомогательные методы класса
		private void ConnectListsEventHandlers( bool bConnect ) {
			if( !bConnect ) {
				// отключаем обработчики событий для Списка (убираем "тормоза")
				this.listViewSource.DoubleClick -= new System.EventHandler(this.ListViewSourceDoubleClick);
				this.listViewSource.ItemChecked -= new System.Windows.Forms.ItemCheckedEventHandler(this.ListViewSourceItemChecked);
				this.listViewSource.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.ListViewSourceItemCheck);
				this.listViewSource.KeyPress -= new System.Windows.Forms.KeyPressEventHandler(this.ListViewSourceKeyPress);
			} else {
				// подключаем обработчики событий для Списка
				this.listViewSource.DoubleClick += new System.EventHandler(this.ListViewSourceDoubleClick);
				this.listViewSource.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.ListViewSourceItemChecked);
				this.listViewSource.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ListViewSourceItemCheck);
				this.listViewSource.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ListViewSourceKeyPress);
			}
		}
		
		private void GenerateSourceList(string dirPath) {
        	// заполнение списка данными указанной папки
        	m_CurrentDir = dirPath;
        	Core.FileManager.FileManagerWork.GenerateSourceList(
        		dirPath, listViewSource, false, checkBoxTagsView.Checked, chBoxStartExplorerColumnsAutoReize.Checked
        	);
		}
		#endregion
		
		#region Обработчики событий
		void ButtonSourceDirClick(object sender, EventArgs e)
		{
			// задание папки с fb2-файлами и архивами для сканирования
			filesWorker.OpenDirDlg( tboxSourceDir, fbdScanDir, "Укажите папку для сканирования с fb2 файлами:" );
		}
		
		void ButtonTargetDirClick(object sender, EventArgs e)
		{
			// задание папки-приемника для размешения копий
			filesWorker.OpenDirDlg( tboxTargetDir, fbdScanDir, "Укажите папку-приемник для размешения копий книг:" );
		}
		
		void ListViewSourceDoubleClick(object sender, EventArgs e)
		{
			// переход в выбранную папку
			if( listViewSource.Items.Count > 0 && listViewSource.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = listViewSource.SelectedItems;
				ListViewItemType it = (ListViewItemType)si[0].Tag;
				if(it.Type=="d" || it.Type=="dUp") {
					textBoxAddress.Text = it.Value;
					GenerateSourceList(it.Value);
				}
			}
		}
		
		void ListViewSourceKeyPress(object sender, KeyPressEventArgs e)
		{
			// обработка нажатия клавиш на списке папок и файлов
			if ( e.KeyChar == (char)Keys.Return ) {
				// переход в выбранную папку
				ListViewSourceDoubleClick(sender, e);
			} else if ( e.KeyChar == (char)Keys.Back ) {
				// переход на каталог выше
				ListViewItemType it = (ListViewItemType)listViewSource.Items[0].Tag;
				textBoxAddress.Text = it.Value;
				GenerateSourceList(it.Value);
			}
		}
		
		void ListViewSourceItemCheck(object sender, ItemCheckEventArgs e)
		{
			if( listViewSource.Items.Count > 0 && listViewSource.SelectedItems.Count != 0 ) {
				// при двойном клике на папке ".." пометку не ставим
				if(e.Index == 0) { // ".."
					e.NewValue = CheckState.Unchecked;
				}
			}
		}
		
		void ListViewSourceItemChecked(object sender, ItemCheckedEventArgs e)
		{
			// пометка/снятие пометки по check на 0-й item - папка ".."
			if( listViewSource.Items.Count > 0 ) {
				ListViewItemType it = (ListViewItemType)e.Item.Tag;
				if(it.Type=="dUp") {
					ConnectListsEventHandlers( false );
					if(e.Item.Checked) {
						m_mscLV.CheckdAllListViewItems( listViewSource, true );
					} else {
						m_mscLV.UnCheckdAllListViewItems( listViewSource.CheckedItems );
					}
					ConnectListsEventHandlers( true );
				}
			}
		}
		
		void ButtonGoClick(object sender, EventArgs e)
		{
			// переход на заданную папку-источник fb2-файлов
			string s = textBoxAddress.Text.Trim();
			if(s != "") {
				DirectoryInfo info = new DirectoryInfo(s);
				if(info.Exists) {
					GenerateSourceList(info.FullName);
				} else {
					MessageBox.Show( "Не удается найти папку " + textBoxAddress.Text + ".\nПроверьте правильность пути.", "Переход по выбранному адресу", MessageBoxButtons.OK, MessageBoxIcon.Error );
				}
			}
		}
		
		void TextBoxAddressKeyPress(object sender, KeyPressEventArgs e)
		{
			// обработка нажатия клавиш в поле ввода пути к папке-источнику
			if ( e.KeyChar == (char)Keys.Return ) {
				// отображение папок и/или фалов в заданной папке
				ButtonGoClick( sender, e );
			}
		}
		
		void ChBoxStartExplorerColumnsAutoReizeCheckedChanged(object sender, EventArgs e)
		{
			Settings.FileManagerSettings.StartExplorerColumnsAutoReize = chBoxStartExplorerColumnsAutoReize.Checked;
			if(chBoxStartExplorerColumnsAutoReize.Checked) {
				Core.FileManager.FileManagerWork.AutoResizeColumns(listViewSource);
			}
		}
		
		void TextBoxAddressTextChanged(object sender, EventArgs e)
		{
			//TODO //Settings.FileManagerSettings.FullSortingSourceDir = textBoxAddress.Text;
		}
		
		void TsmiCheckedAllClick(object sender, EventArgs e)
		{
			// Пометить все файлы и папки
			ConnectListsEventHandlers( false );
			m_mscLV.CheckdAllListViewItems( listViewSource, true );
			if(listViewSource.Items.Count > 0) {
				listViewSource.Items[0].Checked = false;
			}
			ConnectListsEventHandlers( true );
		}
		
		void TsmiUnCheckedAllClick(object sender, EventArgs e)
		{
			// Снять отметки со всех файлов и папок
			ConnectListsEventHandlers( false );
			m_mscLV.UnCheckdAllListViewItems( listViewSource.CheckedItems );
			ConnectListsEventHandlers( true );
		}
		
		void TsmiFilesCheckedAllClick(object sender, EventArgs e)
		{
			// Пометить все файлы
			ConnectListsEventHandlers( false );
			m_mscLV.CheckAllFiles(listViewSource, true);
			ConnectListsEventHandlers( true );
		}
		
		void TsmiFilesUnCheckedAllClick(object sender, EventArgs e)
		{
			// Снять пометки со всех файлов
			ConnectListsEventHandlers( false );
			m_mscLV.UnCheckAllFiles(listViewSource);
			ConnectListsEventHandlers( true );
		}
		
		void TsmiDirCheckedAllClick(object sender, EventArgs e)
		{
			// Пометить все папки
			ConnectListsEventHandlers( false );
			m_mscLV.CheckAllDirs(listViewSource, true);
			ConnectListsEventHandlers( true );
		}
		
		void TsmiDirUnCheckedAllClick(object sender, EventArgs e)
		{
			// Снять пометки со всех папок
			ConnectListsEventHandlers( false );
			m_mscLV.UnCheckAllDirs(listViewSource);
			ConnectListsEventHandlers( true );
		}
		
		void TsmiFB2CheckedAllClick(object sender, EventArgs e)
		{
			// Пометить все fb2 файлы
			ConnectListsEventHandlers( false );
			m_mscLV.CheckTypeAllFiles(listViewSource, "fb2", true);
			ConnectListsEventHandlers( true );
		}
		
		void TsmiFB2UnCheckedAllClick(object sender, EventArgs e)
		{
			// Снять пометки со всех fb2 файлов
			ConnectListsEventHandlers( false );
			m_mscLV.UnCheckTypeAllFiles(listViewSource, "fb2");
			ConnectListsEventHandlers( true );
		}
		
		void TsmiColumnsExplorerAutoReizeClick(object sender, EventArgs e)
		{
			Core.FileManager.FileManagerWork.AutoResizeColumns(listViewSource);
		}
		
		void TsmiZipCheckedAllClick(object sender, EventArgs e)
		{
			// Пометить все zip файлы
			ConnectListsEventHandlers( false );
			m_mscLV.CheckTypeAllFiles(listViewSource, "zip", true);
			ConnectListsEventHandlers( true );
		}
		
		void TsmiZipUnCheckedAllClick(object sender, EventArgs e)
		{
			// Снять пометки со всех zip файлов
			ConnectListsEventHandlers( false );
			m_mscLV.UnCheckTypeAllFiles(listViewSource, "zip");
			ConnectListsEventHandlers( true );
		}
		
		void TsmiCheckedAllSelectedClick(object sender, EventArgs e)
		{
			// Пометить всё выделенное
			ConnectListsEventHandlers( false );
			m_mscLV.ChekAllSelectedItems(listViewSource, true);
			ConnectListsEventHandlers( true );
			listViewSource.Focus();
		}
		
		void TsmiUnCheckedAllSelectedClick(object sender, EventArgs e)
		{
			// Снять пометки со всего выделенного
			ConnectListsEventHandlers( false );
			m_mscLV.ChekAllSelectedItems(listViewSource, false);
			ConnectListsEventHandlers( true );
			listViewSource.Focus();
		}
		#endregion
		
		void CheckBoxTagsViewClick(object sender, EventArgs e)
		{
			// Отображать/скрывать описание книг
			if(checkBoxTagsView.Checked) {
				if(File.Exists(Settings.FileManagerSettings.FileManagerSettingsPath)) {
					if(Settings.FileManagerSettings.ReadXmlFullSortingViewMessageForLongTime()) {
						string sMess = "При включении этой опции для создания списка книг с их описанием может потребоваться очень много времени!\nБольше не показывать это сообщение?";
						DialogResult result = MessageBox.Show( sMess, "Отображение описания книг", MessageBoxButtons.YesNo, MessageBoxIcon.Question );
						Settings.FileManagerSettings.ViewMessageForLongTime = (result == DialogResult.Yes) ? false : true;
					}
				}
			}
			
			Settings.FileManagerSettings.BooksTagsView = checkBoxTagsView.Checked;
			Settings.FileManagerSettings.WriteFileManagerSettings();
			if( listViewSource.Items.Count > 0 ) {
				Cursor.Current = Cursors.WaitCursor;
				listViewSource.BeginUpdate();
				DirectoryInfo di = null;
				FB2BookDescription bd = null;
				Settings.DataFM dfm = new Settings.DataFM();
				for(int i=0; i!= listViewSource.Items.Count; ++i) {
					ListViewItemType it = (ListViewItemType)listViewSource.Items[i].Tag;
					if(it.Type=="f") {
						di = new DirectoryInfo(it.Value);
						if(checkBoxTagsView.Checked) {
							// показать данные книг
							try {
								if(di.Extension.ToLower()==".fb2") {
									// показать данные fb2 файлов
									bd = new FB2BookDescription( it.Value );
									listViewSource.Items[i].SubItems[1].Text = space+bd.TIBookTitle+space;
									listViewSource.Items[i].SubItems[2].Text = space+bd.TISequences+space;
									listViewSource.Items[i].SubItems[3].Text = space+bd.TIAuthors+space;
									listViewSource.Items[i].SubItems[4].Text = space+Core.FileManager.FileManagerWork.CyrillicGenreName(bd.TIGenres)+space;
									listViewSource.Items[i].SubItems[5].Text = space+bd.TILang+space;
									listViewSource.Items[i].SubItems[6].Text = space+bd.Encoding+space;
								} else if(di.Extension.ToLower()==".zip") {
									if(checkBoxTagsView.Checked) {
										// показать данные архивов
										filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
										archivesWorker.unzip(dfm.A7zaPath, it.Value, Settings.Settings.GetTempDir(), ProcessPriorityClass.AboveNormal );
										string [] files = Directory.GetFiles( Settings.Settings.GetTempDir() );
										bd = new FB2BookDescription( files[0] );
										listViewSource.Items[i].SubItems[1].Text = space+bd.TIBookTitle+space;
										listViewSource.Items[i].SubItems[2].Text = space+bd.TISequences+space;
										listViewSource.Items[i].SubItems[3].Text = space+bd.TIAuthors+space;
										listViewSource.Items[i].SubItems[4].Text = space+Core.FileManager.FileManagerWork.CyrillicGenreName(bd.TIGenres)+space;
										listViewSource.Items[i].SubItems[5].Text = space+bd.TILang+space;
										listViewSource.Items[i].SubItems[6].Text = space+bd.Encoding+space;
									}
								}
							} catch(System.Exception) {
								listViewSource.Items[i].ForeColor = Color.Blue;
							}
						} else {
							// скрыть данные книг
							for(int j=1; j!=listViewSource.Items[i].SubItems.Count; ++j) {
								listViewSource.Items[i].SubItems[j].Text = "";
							}
						}
					}
				}
				// авторазмер колонок Списка
				if(chBoxStartExplorerColumnsAutoReize.Checked) {
					Core.FileManager.FileManagerWork.AutoResizeColumns(listViewSource);
				}
				listViewSource.EndUpdate();
				Cursor.Current = Cursors.Default;
			}
		}
	}
}
