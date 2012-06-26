/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 16.03.2009
 * Time: 9:03
 * 
 * License: GPL 2.1
 */
namespace SharpFBTools.Tools
{
	partial class SFBTpFileManager
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the control.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SFBTpFileManager));
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
									"Всего папок",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
									"Всего файлов",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные fb2-файлы",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные  Zip-архивы с fb2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные  Rar-архивы с fb2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные 7zip-архивы с fb2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные BZip2-архивы с fb2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные Gzip-архивы с fb2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные Tar-пакеты с fb2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные fb2-файлы из архивов",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem(new string[] {
									"Другие файлы",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem(new string[] {
									"Создано в папке-приемнике",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem(new string[] {
									"Нечитаемые fb2-файлы (архивы)",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem(new string[] {
									"Не валидные fb2-файлы (при вкл. опции)",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem(new string[] {
									"Битые архивы (не открылись)",
									"0"}, -1);
			this.ssProgress = new System.Windows.Forms.StatusStrip();
			this.tsslblProgress = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.fbdScanDir = new System.Windows.Forms.FolderBrowserDialog();
			this.btnInsertTemplates = new System.Windows.Forms.Button();
			this.txtBoxTemplatesFromLine = new System.Windows.Forms.TextBox();
			this.richTxtBoxDescTemplates = new System.Windows.Forms.RichTextBox();
			this.tcSort = new System.Windows.Forms.TabControl();
			this.tpFullSort = new System.Windows.Forms.TabPage();
			this.panelStart = new System.Windows.Forms.Panel();
			this.chBoxScanSubDir = new System.Windows.Forms.CheckBox();
			this.buttonFullSortStop = new System.Windows.Forms.Button();
			this.buttonSortFilesTo = new System.Windows.Forms.Button();
			this.listViewSource = new System.Windows.Forms.ListView();
			this.colHeaderFileName = new System.Windows.Forms.ColumnHeader();
			this.colHeaderNewFileName = new System.Windows.Forms.ColumnHeader();
			this.colHeaderBookName = new System.Windows.Forms.ColumnHeader();
			this.colHeaderSequence = new System.Windows.Forms.ColumnHeader();
			this.colHeaderSequenceNumber = new System.Windows.Forms.ColumnHeader();
			this.colHeaderFIOBookAuthor = new System.Windows.Forms.ColumnHeader();
			this.colHeaderGenre = new System.Windows.Forms.ColumnHeader();
			this.colHeaderLang = new System.Windows.Forms.ColumnHeader();
			this.cmsItems = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmi3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCheckedAll = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiUnCheckedAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiFilesCheckedAll = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiFilesUnCheckedAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiDirCheckedAll = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiDirUnCheckedAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiFB2CheckedAll = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiFB2UnCheckedAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiZipCheckedAll = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiZipUnCheckedAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCheckedAllSelected = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiUnCheckedAllSelected = new System.Windows.Forms.ToolStripMenuItem();
			this.imageListItems = new System.Windows.Forms.ImageList(this.components);
			this.panelAddress = new System.Windows.Forms.Panel();
			this.buttonGo = new System.Windows.Forms.Button();
			this.textBoxAddress = new System.Windows.Forms.TextBox();
			this.labelAddress = new System.Windows.Forms.Label();
			this.checkBoxDirsView = new System.Windows.Forms.CheckBox();
			this.buttonOpenSourceDir = new System.Windows.Forms.Button();
			this.gBoxFullSortRenameTemplates = new System.Windows.Forms.GroupBox();
			this.tpSelectedSort = new System.Windows.Forms.TabPage();
			this.pData = new System.Windows.Forms.Panel();
			this.lvSSData = new System.Windows.Forms.ListView();
			this.cHeaderLang = new System.Windows.Forms.ColumnHeader();
			this.cHeaderGenresGroup = new System.Windows.Forms.ColumnHeader();
			this.cHeaderGenre = new System.Windows.Forms.ColumnHeader();
			this.cHeaderLast = new System.Windows.Forms.ColumnHeader();
			this.cHeaderFirst = new System.Windows.Forms.ColumnHeader();
			this.cHeaderMiddle = new System.Windows.Forms.ColumnHeader();
			this.cHeaderNick = new System.Windows.Forms.ColumnHeader();
			this.cHeaderSequence = new System.Windows.Forms.ColumnHeader();
			this.cHeaderBookTitle = new System.Windows.Forms.ColumnHeader();
			this.cHeaderExactFit = new System.Windows.Forms.ColumnHeader();
			this.pSSData = new System.Windows.Forms.Panel();
			this.btnSSDataListLoad = new System.Windows.Forms.Button();
			this.btnSSDataListSave = new System.Windows.Forms.Button();
			this.btnSSGetData = new System.Windows.Forms.Button();
			this.gBoxSelectedlSortRenameTemplates = new System.Windows.Forms.GroupBox();
			this.btnSSInsertTemplates = new System.Windows.Forms.Button();
			this.txtBoxSSTemplatesFromLine = new System.Windows.Forms.TextBox();
			this.pSelectedSortDirs = new System.Windows.Forms.Panel();
			this.chBoxSSScanSubDir = new System.Windows.Forms.CheckBox();
			this.lblSSTargetDir = new System.Windows.Forms.Label();
			this.tboxSSToDir = new System.Windows.Forms.TextBox();
			this.tboxSSSourceDir = new System.Windows.Forms.TextBox();
			this.lbSSlDir = new System.Windows.Forms.Label();
			this.tsSelectedSort = new System.Windows.Forms.ToolStrip();
			this.tsbtnSSOpenDir = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnSSTargetDir = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnSSSortFilesTo = new System.Windows.Forms.ToolStripButton();
			this.tsbtnSSSortStop = new System.Windows.Forms.ToolStripButton();
			this.gBoxFullSortTemplatesDescription = new System.Windows.Forms.GroupBox();
			this.lvFilesCount = new System.Windows.Forms.ListView();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.pProgress = new System.Windows.Forms.Panel();
			this.chBoxViewProgress = new System.Windows.Forms.CheckBox();
			this.sfdSaveXMLFile = new System.Windows.Forms.SaveFileDialog();
			this.sfdOpenXMLFile = new System.Windows.Forms.OpenFileDialog();
			this.ssProgress.SuspendLayout();
			this.tcSort.SuspendLayout();
			this.tpFullSort.SuspendLayout();
			this.panelStart.SuspendLayout();
			this.cmsItems.SuspendLayout();
			this.panelAddress.SuspendLayout();
			this.gBoxFullSortRenameTemplates.SuspendLayout();
			this.tpSelectedSort.SuspendLayout();
			this.pData.SuspendLayout();
			this.pSSData.SuspendLayout();
			this.gBoxSelectedlSortRenameTemplates.SuspendLayout();
			this.pSelectedSortDirs.SuspendLayout();
			this.tsSelectedSort.SuspendLayout();
			this.gBoxFullSortTemplatesDescription.SuspendLayout();
			this.pProgress.SuspendLayout();
			this.SuspendLayout();
			// 
			// ssProgress
			// 
			this.ssProgress.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsslblProgress,
									this.tsProgressBar});
			this.ssProgress.Location = new System.Drawing.Point(0, 538);
			this.ssProgress.Name = "ssProgress";
			this.ssProgress.Size = new System.Drawing.Size(828, 22);
			this.ssProgress.TabIndex = 18;
			this.ssProgress.Text = "statusStrip1";
			// 
			// tsslblProgress
			// 
			this.tsslblProgress.Name = "tsslblProgress";
			this.tsslblProgress.Size = new System.Drawing.Size(47, 17);
			this.tsslblProgress.Text = "Готово.";
			// 
			// tsProgressBar
			// 
			this.tsProgressBar.Name = "tsProgressBar";
			this.tsProgressBar.Size = new System.Drawing.Size(400, 16);
			// 
			// fbdScanDir
			// 
			this.fbdScanDir.Description = "Укажите папку для сканирования с fb2-файлами и архивами";
			// 
			// btnInsertTemplates
			// 
			this.btnInsertTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnInsertTemplates.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnInsertTemplates.Location = new System.Drawing.Point(660, 15);
			this.btnInsertTemplates.Name = "btnInsertTemplates";
			this.btnInsertTemplates.Size = new System.Drawing.Size(142, 28);
			this.btnInsertTemplates.TabIndex = 9;
			this.btnInsertTemplates.Text = "Вставить готовый";
			this.btnInsertTemplates.UseVisualStyleBackColor = true;
			this.btnInsertTemplates.Click += new System.EventHandler(this.BtnInsertTemplatesClick);
			// 
			// txtBoxTemplatesFromLine
			// 
			this.txtBoxTemplatesFromLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxTemplatesFromLine.Location = new System.Drawing.Point(6, 20);
			this.txtBoxTemplatesFromLine.Name = "txtBoxTemplatesFromLine";
			this.txtBoxTemplatesFromLine.Size = new System.Drawing.Size(630, 20);
			this.txtBoxTemplatesFromLine.TabIndex = 8;
			this.txtBoxTemplatesFromLine.TextChanged += new System.EventHandler(this.TxtBoxTemplatesFromLineTextChanged);
			// 
			// richTxtBoxDescTemplates
			// 
			this.richTxtBoxDescTemplates.BackColor = System.Drawing.SystemColors.Window;
			this.richTxtBoxDescTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTxtBoxDescTemplates.Font = new System.Drawing.Font("Tahoma", 8F);
			this.richTxtBoxDescTemplates.Location = new System.Drawing.Point(3, 16);
			this.richTxtBoxDescTemplates.Name = "richTxtBoxDescTemplates";
			this.richTxtBoxDescTemplates.ReadOnly = true;
			this.richTxtBoxDescTemplates.Size = new System.Drawing.Size(497, 219);
			this.richTxtBoxDescTemplates.TabIndex = 9;
			this.richTxtBoxDescTemplates.Text = "";
			// 
			// tcSort
			// 
			this.tcSort.Controls.Add(this.tpFullSort);
			this.tcSort.Controls.Add(this.tpSelectedSort);
			this.tcSort.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcSort.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.tcSort.Location = new System.Drawing.Point(0, 0);
			this.tcSort.Name = "tcSort";
			this.tcSort.SelectedIndex = 0;
			this.tcSort.Size = new System.Drawing.Size(828, 270);
			this.tcSort.TabIndex = 31;
			// 
			// tpFullSort
			// 
			this.tpFullSort.Controls.Add(this.panelStart);
			this.tpFullSort.Controls.Add(this.listViewSource);
			this.tpFullSort.Controls.Add(this.panelAddress);
			this.tpFullSort.Controls.Add(this.gBoxFullSortRenameTemplates);
			this.tpFullSort.Font = new System.Drawing.Font("Tahoma", 8F);
			this.tpFullSort.Location = new System.Drawing.Point(4, 22);
			this.tpFullSort.Name = "tpFullSort";
			this.tpFullSort.Padding = new System.Windows.Forms.Padding(3);
			this.tpFullSort.Size = new System.Drawing.Size(820, 244);
			this.tpFullSort.TabIndex = 0;
			this.tpFullSort.Text = " Полная Сортировка ";
			this.tpFullSort.UseVisualStyleBackColor = true;
			// 
			// panelStart
			// 
			this.panelStart.Controls.Add(this.chBoxScanSubDir);
			this.panelStart.Controls.Add(this.buttonFullSortStop);
			this.panelStart.Controls.Add(this.buttonSortFilesTo);
			this.panelStart.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panelStart.Location = new System.Drawing.Point(3, 183);
			this.panelStart.Name = "panelStart";
			this.panelStart.Size = new System.Drawing.Size(814, 58);
			this.panelStart.TabIndex = 36;
			// 
			// chBoxScanSubDir
			// 
			this.chBoxScanSubDir.Checked = true;
			this.chBoxScanSubDir.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chBoxScanSubDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxScanSubDir.ForeColor = System.Drawing.Color.Navy;
			this.chBoxScanSubDir.Location = new System.Drawing.Point(3, 4);
			this.chBoxScanSubDir.Name = "chBoxScanSubDir";
			this.chBoxScanSubDir.Size = new System.Drawing.Size(172, 24);
			this.chBoxScanSubDir.TabIndex = 4;
			this.chBoxScanSubDir.Text = "Сканировать и подпапки";
			this.chBoxScanSubDir.UseVisualStyleBackColor = true;
			// 
			// buttonFullSortStop
			// 
			this.buttonFullSortStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonFullSortStop.Enabled = false;
			this.buttonFullSortStop.Image = ((System.Drawing.Image)(resources.GetObject("buttonFullSortStop.Image")));
			this.buttonFullSortStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonFullSortStop.Location = new System.Drawing.Point(688, 4);
			this.buttonFullSortStop.Name = "buttonFullSortStop";
			this.buttonFullSortStop.Size = new System.Drawing.Size(123, 49);
			this.buttonFullSortStop.TabIndex = 3;
			this.buttonFullSortStop.Text = "Остановить";
			this.buttonFullSortStop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.buttonFullSortStop.UseVisualStyleBackColor = true;
			this.buttonFullSortStop.Click += new System.EventHandler(this.ButtonFullSortStopClick);
			// 
			// buttonSortFilesTo
			// 
			this.buttonSortFilesTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonSortFilesTo.Image = ((System.Drawing.Image)(resources.GetObject("buttonSortFilesTo.Image")));
			this.buttonSortFilesTo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonSortFilesTo.Location = new System.Drawing.Point(557, 4);
			this.buttonSortFilesTo.Name = "buttonSortFilesTo";
			this.buttonSortFilesTo.Size = new System.Drawing.Size(125, 49);
			this.buttonSortFilesTo.TabIndex = 2;
			this.buttonSortFilesTo.Text = "Сортировать";
			this.buttonSortFilesTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.buttonSortFilesTo.UseVisualStyleBackColor = true;
			this.buttonSortFilesTo.Click += new System.EventHandler(this.ButtonSortFilesToClick);
			// 
			// listViewSource
			// 
			this.listViewSource.AllowColumnReorder = true;
			this.listViewSource.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.listViewSource.CheckBoxes = true;
			this.listViewSource.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.colHeaderFileName,
									this.colHeaderNewFileName,
									this.colHeaderBookName,
									this.colHeaderSequence,
									this.colHeaderSequenceNumber,
									this.colHeaderFIOBookAuthor,
									this.colHeaderGenre,
									this.colHeaderLang});
			this.listViewSource.ContextMenuStrip = this.cmsItems;
			this.listViewSource.FullRowSelect = true;
			this.listViewSource.GridLines = true;
			this.listViewSource.Location = new System.Drawing.Point(6, 98);
			this.listViewSource.Name = "listViewSource";
			this.listViewSource.ShowItemToolTips = true;
			this.listViewSource.Size = new System.Drawing.Size(811, 83);
			this.listViewSource.SmallImageList = this.imageListItems;
			this.listViewSource.TabIndex = 35;
			this.listViewSource.UseCompatibleStateImageBehavior = false;
			this.listViewSource.View = System.Windows.Forms.View.Details;
			this.listViewSource.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.ListViewSourceItemChecked);
			this.listViewSource.DoubleClick += new System.EventHandler(this.ListViewSourceDoubleClick);
			this.listViewSource.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ListViewSourceItemCheck);
			// 
			// colHeaderFileName
			// 
			this.colHeaderFileName.Text = "Имя файла";
			// 
			// colHeaderNewFileName
			// 
			this.colHeaderNewFileName.Text = "Новое имя файла";
			// 
			// colHeaderBookName
			// 
			this.colHeaderBookName.Text = "Название книги";
			// 
			// colHeaderSequence
			// 
			this.colHeaderSequence.Text = "Серия";
			// 
			// colHeaderSequenceNumber
			// 
			this.colHeaderSequenceNumber.Text = "№ Cерии";
			// 
			// colHeaderFIOBookAuthor
			// 
			this.colHeaderFIOBookAuthor.Text = "ФИО автора книги";
			// 
			// colHeaderGenre
			// 
			this.colHeaderGenre.Text = "Жанр";
			// 
			// colHeaderLang
			// 
			this.colHeaderLang.Text = "Язык книги";
			// 
			// cmsItems
			// 
			this.cmsItems.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsmi3,
									this.tsmiCheckedAll,
									this.tsmiUnCheckedAll,
									this.toolStripMenuItem1,
									this.tsmiFilesCheckedAll,
									this.tsmiFilesUnCheckedAll,
									this.toolStripMenuItem2,
									this.tsmiDirCheckedAll,
									this.tsmiDirUnCheckedAll,
									this.toolStripMenuItem3,
									this.tsmiFB2CheckedAll,
									this.tsmiFB2UnCheckedAll,
									this.toolStripMenuItem4,
									this.tsmiZipCheckedAll,
									this.tsmiZipUnCheckedAll,
									this.toolStripMenuItem5,
									this.tsmiCheckedAllSelected,
									this.tsmiUnCheckedAllSelected});
			this.cmsItems.Name = "cmsValidator";
			this.cmsItems.Size = new System.Drawing.Size(285, 304);
			// 
			// tsmi3
			// 
			this.tsmi3.Name = "tsmi3";
			this.tsmi3.Size = new System.Drawing.Size(281, 6);
			// 
			// tsmiCheckedAll
			// 
			this.tsmiCheckedAll.Image = ((System.Drawing.Image)(resources.GetObject("tsmiCheckedAll.Image")));
			this.tsmiCheckedAll.Name = "tsmiCheckedAll";
			this.tsmiCheckedAll.Size = new System.Drawing.Size(284, 22);
			this.tsmiCheckedAll.Text = "Пометить все файлы и папки";
			this.tsmiCheckedAll.Click += new System.EventHandler(this.TsmiCheckedAllClick);
			// 
			// tsmiUnCheckedAll
			// 
			this.tsmiUnCheckedAll.Image = ((System.Drawing.Image)(resources.GetObject("tsmiUnCheckedAll.Image")));
			this.tsmiUnCheckedAll.Name = "tsmiUnCheckedAll";
			this.tsmiUnCheckedAll.Size = new System.Drawing.Size(284, 22);
			this.tsmiUnCheckedAll.Text = "Снять пометки со всех файлов и папок";
			this.tsmiUnCheckedAll.Click += new System.EventHandler(this.TsmiUnCheckedAllClick);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(281, 6);
			// 
			// tsmiFilesCheckedAll
			// 
			this.tsmiFilesCheckedAll.Name = "tsmiFilesCheckedAll";
			this.tsmiFilesCheckedAll.Size = new System.Drawing.Size(284, 22);
			this.tsmiFilesCheckedAll.Text = "Пометить все файлы";
			this.tsmiFilesCheckedAll.Click += new System.EventHandler(this.TsmiFilesCheckedAllClick);
			// 
			// tsmiFilesUnCheckedAll
			// 
			this.tsmiFilesUnCheckedAll.Name = "tsmiFilesUnCheckedAll";
			this.tsmiFilesUnCheckedAll.Size = new System.Drawing.Size(284, 22);
			this.tsmiFilesUnCheckedAll.Text = "Снять пометки со всех файлов";
			this.tsmiFilesUnCheckedAll.Click += new System.EventHandler(this.TsmiFilesUnCheckedAllClick);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(281, 6);
			// 
			// tsmiDirCheckedAll
			// 
			this.tsmiDirCheckedAll.Name = "tsmiDirCheckedAll";
			this.tsmiDirCheckedAll.Size = new System.Drawing.Size(284, 22);
			this.tsmiDirCheckedAll.Text = "Пометить все папки";
			this.tsmiDirCheckedAll.Click += new System.EventHandler(this.TsmiDirCheckedAllClick);
			// 
			// tsmiDirUnCheckedAll
			// 
			this.tsmiDirUnCheckedAll.Name = "tsmiDirUnCheckedAll";
			this.tsmiDirUnCheckedAll.Size = new System.Drawing.Size(284, 22);
			this.tsmiDirUnCheckedAll.Text = "Снять пометки со всех папок";
			this.tsmiDirUnCheckedAll.Click += new System.EventHandler(this.TsmiDirUnCheckedAllClick);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(281, 6);
			// 
			// tsmiFB2CheckedAll
			// 
			this.tsmiFB2CheckedAll.Name = "tsmiFB2CheckedAll";
			this.tsmiFB2CheckedAll.Size = new System.Drawing.Size(284, 22);
			this.tsmiFB2CheckedAll.Text = "Пометить все fb2 файлы";
			this.tsmiFB2CheckedAll.Click += new System.EventHandler(this.TsmiFB2CheckedAllClick);
			// 
			// tsmiFB2UnCheckedAll
			// 
			this.tsmiFB2UnCheckedAll.Name = "tsmiFB2UnCheckedAll";
			this.tsmiFB2UnCheckedAll.Size = new System.Drawing.Size(284, 22);
			this.tsmiFB2UnCheckedAll.Text = "Снять пометки со всех fb2 файлов";
			this.tsmiFB2UnCheckedAll.Click += new System.EventHandler(this.TsmiFB2UnCheckedAllClick);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(281, 6);
			// 
			// tsmiZipCheckedAll
			// 
			this.tsmiZipCheckedAll.Name = "tsmiZipCheckedAll";
			this.tsmiZipCheckedAll.Size = new System.Drawing.Size(284, 22);
			this.tsmiZipCheckedAll.Text = "Пометить все zip файлы";
			this.tsmiZipCheckedAll.Click += new System.EventHandler(this.TsmiZipCheckedAllClick);
			// 
			// tsmiZipUnCheckedAll
			// 
			this.tsmiZipUnCheckedAll.Name = "tsmiZipUnCheckedAll";
			this.tsmiZipUnCheckedAll.Size = new System.Drawing.Size(284, 22);
			this.tsmiZipUnCheckedAll.Text = "Снять пометки со всех zip файлов";
			this.tsmiZipUnCheckedAll.Click += new System.EventHandler(this.TsmiZipUnCheckedAllClick);
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(281, 6);
			// 
			// tsmiCheckedAllSelected
			// 
			this.tsmiCheckedAllSelected.Name = "tsmiCheckedAllSelected";
			this.tsmiCheckedAllSelected.Size = new System.Drawing.Size(284, 22);
			this.tsmiCheckedAllSelected.Text = "Пометить всё выделенное";
			this.tsmiCheckedAllSelected.Click += new System.EventHandler(this.TsmiCheckedAllSelectedClick);
			// 
			// tsmiUnCheckedAllSelected
			// 
			this.tsmiUnCheckedAllSelected.Name = "tsmiUnCheckedAllSelected";
			this.tsmiUnCheckedAllSelected.Size = new System.Drawing.Size(284, 22);
			this.tsmiUnCheckedAllSelected.Text = "Снять пометки со всего выделенного";
			this.tsmiUnCheckedAllSelected.Click += new System.EventHandler(this.TsmiUnCheckedAllSelectedClick);
			// 
			// imageListItems
			// 
			this.imageListItems.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListItems.ImageStream")));
			this.imageListItems.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListItems.Images.SetKeyName(0, "0_folder.png");
			this.imageListItems.Images.SetKeyName(1, "1_fb2.png");
			this.imageListItems.Images.SetKeyName(2, "2_zip.png");
			this.imageListItems.Images.SetKeyName(3, "3_up.png");
			// 
			// panelAddress
			// 
			this.panelAddress.Controls.Add(this.buttonGo);
			this.panelAddress.Controls.Add(this.textBoxAddress);
			this.panelAddress.Controls.Add(this.labelAddress);
			this.panelAddress.Controls.Add(this.checkBoxDirsView);
			this.panelAddress.Controls.Add(this.buttonOpenSourceDir);
			this.panelAddress.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelAddress.Location = new System.Drawing.Point(3, 59);
			this.panelAddress.Name = "panelAddress";
			this.panelAddress.Size = new System.Drawing.Size(814, 33);
			this.panelAddress.TabIndex = 33;
			// 
			// buttonGo
			// 
			this.buttonGo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.buttonGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonGo.Location = new System.Drawing.Point(562, 2);
			this.buttonGo.Name = "buttonGo";
			this.buttonGo.Size = new System.Drawing.Size(74, 24);
			this.buttonGo.TabIndex = 6;
			this.buttonGo.Text = "Перейти";
			this.buttonGo.UseVisualStyleBackColor = true;
			this.buttonGo.Click += new System.EventHandler(this.ButtonGoClick);
			// 
			// textBoxAddress
			// 
			this.textBoxAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxAddress.Location = new System.Drawing.Point(93, 5);
			this.textBoxAddress.Name = "textBoxAddress";
			this.textBoxAddress.Size = new System.Drawing.Size(458, 20);
			this.textBoxAddress.TabIndex = 5;
			this.textBoxAddress.TextChanged += new System.EventHandler(this.TextBoxAddressTextChanged);
			this.textBoxAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxAddressKeyPress);
			// 
			// labelAddress
			// 
			this.labelAddress.AutoSize = true;
			this.labelAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelAddress.Location = new System.Drawing.Point(40, 9);
			this.labelAddress.Name = "labelAddress";
			this.labelAddress.Size = new System.Drawing.Size(47, 13);
			this.labelAddress.TabIndex = 4;
			this.labelAddress.Text = "Адрес:";
			this.labelAddress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// checkBoxDirsView
			// 
			this.checkBoxDirsView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBoxDirsView.Checked = true;
			this.checkBoxDirsView.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxDirsView.Location = new System.Drawing.Point(660, 2);
			this.checkBoxDirsView.Name = "checkBoxDirsView";
			this.checkBoxDirsView.Size = new System.Drawing.Size(124, 24);
			this.checkBoxDirsView.TabIndex = 1;
			this.checkBoxDirsView.Text = "Отображать папки";
			this.checkBoxDirsView.UseVisualStyleBackColor = true;
			this.checkBoxDirsView.Click += new System.EventHandler(this.CheckBoxDirsViewClick);
			// 
			// buttonOpenSourceDir
			// 
			this.buttonOpenSourceDir.Image = ((System.Drawing.Image)(resources.GetObject("buttonOpenSourceDir.Image")));
			this.buttonOpenSourceDir.Location = new System.Drawing.Point(3, 3);
			this.buttonOpenSourceDir.Name = "buttonOpenSourceDir";
			this.buttonOpenSourceDir.Size = new System.Drawing.Size(31, 27);
			this.buttonOpenSourceDir.TabIndex = 7;
			this.buttonOpenSourceDir.UseVisualStyleBackColor = true;
			this.buttonOpenSourceDir.Click += new System.EventHandler(this.ButtonOpenSourceDirClick);
			// 
			// gBoxFullSortRenameTemplates
			// 
			this.gBoxFullSortRenameTemplates.Controls.Add(this.btnInsertTemplates);
			this.gBoxFullSortRenameTemplates.Controls.Add(this.txtBoxTemplatesFromLine);
			this.gBoxFullSortRenameTemplates.Dock = System.Windows.Forms.DockStyle.Top;
			this.gBoxFullSortRenameTemplates.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gBoxFullSortRenameTemplates.ForeColor = System.Drawing.Color.Indigo;
			this.gBoxFullSortRenameTemplates.Location = new System.Drawing.Point(3, 3);
			this.gBoxFullSortRenameTemplates.Name = "gBoxFullSortRenameTemplates";
			this.gBoxFullSortRenameTemplates.Size = new System.Drawing.Size(814, 56);
			this.gBoxFullSortRenameTemplates.TabIndex = 32;
			this.gBoxFullSortRenameTemplates.TabStop = false;
			this.gBoxFullSortRenameTemplates.Text = " Шаблоны подстановки ";
			// 
			// tpSelectedSort
			// 
			this.tpSelectedSort.Controls.Add(this.pData);
			this.tpSelectedSort.Controls.Add(this.gBoxSelectedlSortRenameTemplates);
			this.tpSelectedSort.Controls.Add(this.pSelectedSortDirs);
			this.tpSelectedSort.Controls.Add(this.tsSelectedSort);
			this.tpSelectedSort.Font = new System.Drawing.Font("Tahoma", 8F);
			this.tpSelectedSort.Location = new System.Drawing.Point(4, 22);
			this.tpSelectedSort.Name = "tpSelectedSort";
			this.tpSelectedSort.Padding = new System.Windows.Forms.Padding(3);
			this.tpSelectedSort.Size = new System.Drawing.Size(820, 244);
			this.tpSelectedSort.TabIndex = 1;
			this.tpSelectedSort.Text = " Избранная Сортировка ";
			this.tpSelectedSort.UseVisualStyleBackColor = true;
			// 
			// pData
			// 
			this.pData.Controls.Add(this.lvSSData);
			this.pData.Controls.Add(this.pSSData);
			this.pData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pData.Location = new System.Drawing.Point(3, 145);
			this.pData.Name = "pData";
			this.pData.Size = new System.Drawing.Size(814, 96);
			this.pData.TabIndex = 62;
			// 
			// lvSSData
			// 
			this.lvSSData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.cHeaderLang,
									this.cHeaderGenresGroup,
									this.cHeaderGenre,
									this.cHeaderLast,
									this.cHeaderFirst,
									this.cHeaderMiddle,
									this.cHeaderNick,
									this.cHeaderSequence,
									this.cHeaderBookTitle,
									this.cHeaderExactFit});
			this.lvSSData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvSSData.FullRowSelect = true;
			this.lvSSData.GridLines = true;
			this.lvSSData.Location = new System.Drawing.Point(0, 35);
			this.lvSSData.Name = "lvSSData";
			this.lvSSData.Size = new System.Drawing.Size(814, 61);
			this.lvSSData.TabIndex = 61;
			this.lvSSData.UseCompatibleStateImageBehavior = false;
			this.lvSSData.View = System.Windows.Forms.View.Details;
			// 
			// cHeaderLang
			// 
			this.cHeaderLang.Text = "Язык Книги";
			this.cHeaderLang.Width = 80;
			// 
			// cHeaderGenresGroup
			// 
			this.cHeaderGenresGroup.Text = "Группа Жанров";
			this.cHeaderGenresGroup.Width = 120;
			// 
			// cHeaderGenre
			// 
			this.cHeaderGenre.Text = "Жанр";
			this.cHeaderGenre.Width = 120;
			// 
			// cHeaderLast
			// 
			this.cHeaderLast.Text = "Фамилия";
			this.cHeaderLast.Width = 120;
			// 
			// cHeaderFirst
			// 
			this.cHeaderFirst.Text = "Имя";
			this.cHeaderFirst.Width = 80;
			// 
			// cHeaderMiddle
			// 
			this.cHeaderMiddle.Text = "Отчество";
			this.cHeaderMiddle.Width = 80;
			// 
			// cHeaderNick
			// 
			this.cHeaderNick.Text = "Ник";
			this.cHeaderNick.Width = 50;
			// 
			// cHeaderSequence
			// 
			this.cHeaderSequence.Text = "Серия";
			this.cHeaderSequence.Width = 140;
			// 
			// cHeaderBookTitle
			// 
			this.cHeaderBookTitle.Text = "Название Книги";
			this.cHeaderBookTitle.Width = 110;
			// 
			// cHeaderExactFit
			// 
			this.cHeaderExactFit.Text = "Точное соответствие";
			// 
			// pSSData
			// 
			this.pSSData.Controls.Add(this.btnSSDataListLoad);
			this.pSSData.Controls.Add(this.btnSSDataListSave);
			this.pSSData.Controls.Add(this.btnSSGetData);
			this.pSSData.Dock = System.Windows.Forms.DockStyle.Top;
			this.pSSData.Location = new System.Drawing.Point(0, 0);
			this.pSSData.Name = "pSSData";
			this.pSSData.Size = new System.Drawing.Size(814, 35);
			this.pSSData.TabIndex = 62;
			// 
			// btnSSDataListLoad
			// 
			this.btnSSDataListLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSSDataListLoad.Image = ((System.Drawing.Image)(resources.GetObject("btnSSDataListLoad.Image")));
			this.btnSSDataListLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnSSDataListLoad.Location = new System.Drawing.Point(660, 3);
			this.btnSSDataListLoad.Name = "btnSSDataListLoad";
			this.btnSSDataListLoad.Size = new System.Drawing.Size(140, 28);
			this.btnSSDataListLoad.TabIndex = 12;
			this.btnSSDataListLoad.Text = "Загрузить список";
			this.btnSSDataListLoad.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSSDataListLoad.UseVisualStyleBackColor = true;
			this.btnSSDataListLoad.Click += new System.EventHandler(this.BtnSSDataListLoadClick);
			// 
			// btnSSDataListSave
			// 
			this.btnSSDataListSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSSDataListSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSSDataListSave.Image")));
			this.btnSSDataListSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnSSDataListSave.Location = new System.Drawing.Point(495, 4);
			this.btnSSDataListSave.Name = "btnSSDataListSave";
			this.btnSSDataListSave.Size = new System.Drawing.Size(141, 28);
			this.btnSSDataListSave.TabIndex = 11;
			this.btnSSDataListSave.Text = "Сохранить список";
			this.btnSSDataListSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSSDataListSave.UseVisualStyleBackColor = true;
			this.btnSSDataListSave.Click += new System.EventHandler(this.BtnSSDataListSaveClick);
			// 
			// btnSSGetData
			// 
			this.btnSSGetData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.btnSSGetData.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.btnSSGetData.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSGetData.Image = ((System.Drawing.Image)(resources.GetObject("btnSSGetData.Image")));
			this.btnSSGetData.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnSSGetData.Location = new System.Drawing.Point(23, 3);
			this.btnSSGetData.Name = "btnSSGetData";
			this.btnSSGetData.Size = new System.Drawing.Size(447, 28);
			this.btnSSGetData.TabIndex = 10;
			this.btnSSGetData.Text = "Собрать данные для Избранной Сортировки";
			this.btnSSGetData.UseVisualStyleBackColor = true;
			this.btnSSGetData.Click += new System.EventHandler(this.BtnSSGetDataClick);
			// 
			// gBoxSelectedlSortRenameTemplates
			// 
			this.gBoxSelectedlSortRenameTemplates.Controls.Add(this.btnSSInsertTemplates);
			this.gBoxSelectedlSortRenameTemplates.Controls.Add(this.txtBoxSSTemplatesFromLine);
			this.gBoxSelectedlSortRenameTemplates.Dock = System.Windows.Forms.DockStyle.Top;
			this.gBoxSelectedlSortRenameTemplates.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gBoxSelectedlSortRenameTemplates.ForeColor = System.Drawing.Color.Indigo;
			this.gBoxSelectedlSortRenameTemplates.Location = new System.Drawing.Point(3, 89);
			this.gBoxSelectedlSortRenameTemplates.Name = "gBoxSelectedlSortRenameTemplates";
			this.gBoxSelectedlSortRenameTemplates.Size = new System.Drawing.Size(814, 56);
			this.gBoxSelectedlSortRenameTemplates.TabIndex = 46;
			this.gBoxSelectedlSortRenameTemplates.TabStop = false;
			this.gBoxSelectedlSortRenameTemplates.Text = " Шаблоны подстановки ";
			// 
			// btnSSInsertTemplates
			// 
			this.btnSSInsertTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSSInsertTemplates.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSInsertTemplates.Location = new System.Drawing.Point(660, 15);
			this.btnSSInsertTemplates.Name = "btnSSInsertTemplates";
			this.btnSSInsertTemplates.Size = new System.Drawing.Size(142, 28);
			this.btnSSInsertTemplates.TabIndex = 9;
			this.btnSSInsertTemplates.Text = "Вставить готовый";
			this.btnSSInsertTemplates.UseVisualStyleBackColor = true;
			this.btnSSInsertTemplates.Click += new System.EventHandler(this.BtnSSInsertTemplatesClick);
			// 
			// txtBoxSSTemplatesFromLine
			// 
			this.txtBoxSSTemplatesFromLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxSSTemplatesFromLine.Location = new System.Drawing.Point(6, 20);
			this.txtBoxSSTemplatesFromLine.Name = "txtBoxSSTemplatesFromLine";
			this.txtBoxSSTemplatesFromLine.Size = new System.Drawing.Size(630, 20);
			this.txtBoxSSTemplatesFromLine.TabIndex = 8;
			this.txtBoxSSTemplatesFromLine.TextChanged += new System.EventHandler(this.TxtBoxSSTemplatesFromLineTextChanged);
			// 
			// pSelectedSortDirs
			// 
			this.pSelectedSortDirs.Controls.Add(this.chBoxSSScanSubDir);
			this.pSelectedSortDirs.Controls.Add(this.lblSSTargetDir);
			this.pSelectedSortDirs.Controls.Add(this.tboxSSToDir);
			this.pSelectedSortDirs.Controls.Add(this.tboxSSSourceDir);
			this.pSelectedSortDirs.Controls.Add(this.lbSSlDir);
			this.pSelectedSortDirs.Dock = System.Windows.Forms.DockStyle.Top;
			this.pSelectedSortDirs.Location = new System.Drawing.Point(3, 34);
			this.pSelectedSortDirs.Name = "pSelectedSortDirs";
			this.pSelectedSortDirs.Size = new System.Drawing.Size(814, 55);
			this.pSelectedSortDirs.TabIndex = 32;
			// 
			// chBoxSSScanSubDir
			// 
			this.chBoxSSScanSubDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chBoxSSScanSubDir.Checked = true;
			this.chBoxSSScanSubDir.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chBoxSSScanSubDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxSSScanSubDir.ForeColor = System.Drawing.Color.Navy;
			this.chBoxSSScanSubDir.Location = new System.Drawing.Point(642, 4);
			this.chBoxSSScanSubDir.Name = "chBoxSSScanSubDir";
			this.chBoxSSScanSubDir.Size = new System.Drawing.Size(172, 24);
			this.chBoxSSScanSubDir.TabIndex = 2;
			this.chBoxSSScanSubDir.Text = "Сканировать и подпапки";
			this.chBoxSSScanSubDir.UseVisualStyleBackColor = true;
			// 
			// lblSSTargetDir
			// 
			this.lblSSTargetDir.AutoSize = true;
			this.lblSSTargetDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblSSTargetDir.Location = new System.Drawing.Point(3, 33);
			this.lblSSTargetDir.Name = "lblSSTargetDir";
			this.lblSSTargetDir.Size = new System.Drawing.Size(152, 13);
			this.lblSSTargetDir.TabIndex = 18;
			this.lblSSTargetDir.Text = "Папка-приемник файлов:";
			// 
			// tboxSSToDir
			// 
			this.tboxSSToDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxSSToDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.tboxSSToDir.Location = new System.Drawing.Point(166, 31);
			this.tboxSSToDir.Name = "tboxSSToDir";
			this.tboxSSToDir.Size = new System.Drawing.Size(634, 20);
			this.tboxSSToDir.TabIndex = 3;
			this.tboxSSToDir.TextChanged += new System.EventHandler(this.TboxSSToDirTextChanged);
			// 
			// tboxSSSourceDir
			// 
			this.tboxSSSourceDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxSSSourceDir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tboxSSSourceDir.Location = new System.Drawing.Point(166, 6);
			this.tboxSSSourceDir.Name = "tboxSSSourceDir";
			this.tboxSSSourceDir.Size = new System.Drawing.Size(470, 21);
			this.tboxSSSourceDir.TabIndex = 1;
			this.tboxSSSourceDir.TextChanged += new System.EventHandler(this.TboxSSSourceDirTextChanged);
			// 
			// lbSSlDir
			// 
			this.lbSSlDir.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.lbSSlDir.Location = new System.Drawing.Point(3, 8);
			this.lbSSlDir.Name = "lbSSlDir";
			this.lbSSlDir.Size = new System.Drawing.Size(162, 19);
			this.lbSSlDir.TabIndex = 6;
			this.lbSSlDir.Text = "Папка для сканирования:";
			// 
			// tsSelectedSort
			// 
			this.tsSelectedSort.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tsSelectedSort.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsbtnSSOpenDir,
									this.toolStripSeparator1,
									this.tsbtnSSTargetDir,
									this.toolStripSeparator2,
									this.tsbtnSSSortFilesTo,
									this.tsbtnSSSortStop});
			this.tsSelectedSort.Location = new System.Drawing.Point(3, 3);
			this.tsSelectedSort.Name = "tsSelectedSort";
			this.tsSelectedSort.Size = new System.Drawing.Size(814, 31);
			this.tsSelectedSort.TabIndex = 20;
			// 
			// tsbtnSSOpenDir
			// 
			this.tsbtnSSOpenDir.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSSOpenDir.Image")));
			this.tsbtnSSOpenDir.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnSSOpenDir.Name = "tsbtnSSOpenDir";
			this.tsbtnSSOpenDir.Size = new System.Drawing.Size(123, 28);
			this.tsbtnSSOpenDir.Text = "Папка - источник";
			this.tsbtnSSOpenDir.ToolTipText = "Открыть папку с fb2-файлами и (или) архивами...";
			this.tsbtnSSOpenDir.Click += new System.EventHandler(this.TsbtnSSOpenDirClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			// 
			// tsbtnSSTargetDir
			// 
			this.tsbtnSSTargetDir.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSSTargetDir.Image")));
			this.tsbtnSSTargetDir.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnSSTargetDir.Name = "tsbtnSSTargetDir";
			this.tsbtnSSTargetDir.Size = new System.Drawing.Size(124, 28);
			this.tsbtnSSTargetDir.Text = "Папка - приемник";
			this.tsbtnSSTargetDir.ToolTipText = "Папка - приемник отсортированных fb2-файлов (архивов)";
			this.tsbtnSSTargetDir.Click += new System.EventHandler(this.TsbtnSSTargetDirClick);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			// 
			// tsbtnSSSortFilesTo
			// 
			this.tsbtnSSSortFilesTo.AutoToolTip = false;
			this.tsbtnSSSortFilesTo.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSSSortFilesTo.Image")));
			this.tsbtnSSSortFilesTo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnSSSortFilesTo.Name = "tsbtnSSSortFilesTo";
			this.tsbtnSSSortFilesTo.Size = new System.Drawing.Size(102, 28);
			this.tsbtnSSSortFilesTo.Text = "Сортировать";
			this.tsbtnSSSortFilesTo.Click += new System.EventHandler(this.TsbtnSSSortFilesToClick);
			// 
			// tsbtnSSSortStop
			// 
			this.tsbtnSSSortStop.AutoToolTip = false;
			this.tsbtnSSSortStop.Enabled = false;
			this.tsbtnSSSortStop.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSSSortStop.Image")));
			this.tsbtnSSSortStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnSSSortStop.Name = "tsbtnSSSortStop";
			this.tsbtnSSSortStop.Size = new System.Drawing.Size(96, 28);
			this.tsbtnSSSortStop.Text = "Остановить";
			this.tsbtnSSSortStop.Click += new System.EventHandler(this.TsbtnSSSortStopClick);
			// 
			// gBoxFullSortTemplatesDescription
			// 
			this.gBoxFullSortTemplatesDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.gBoxFullSortTemplatesDescription.Controls.Add(this.richTxtBoxDescTemplates);
			this.gBoxFullSortTemplatesDescription.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gBoxFullSortTemplatesDescription.ForeColor = System.Drawing.Color.Maroon;
			this.gBoxFullSortTemplatesDescription.Location = new System.Drawing.Point(318, 30);
			this.gBoxFullSortTemplatesDescription.Name = "gBoxFullSortTemplatesDescription";
			this.gBoxFullSortTemplatesDescription.Size = new System.Drawing.Size(503, 238);
			this.gBoxFullSortTemplatesDescription.TabIndex = 33;
			this.gBoxFullSortTemplatesDescription.TabStop = false;
			this.gBoxFullSortTemplatesDescription.Text = " Описание шаблонов подстановки ";
			// 
			// lvFilesCount
			// 
			this.lvFilesCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left)));
			this.lvFilesCount.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeader6,
									this.columnHeader7});
			this.lvFilesCount.FullRowSelect = true;
			this.lvFilesCount.GridLines = true;
			this.lvFilesCount.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
									listViewItem1,
									listViewItem2,
									listViewItem3,
									listViewItem4,
									listViewItem5,
									listViewItem6,
									listViewItem7,
									listViewItem8,
									listViewItem9,
									listViewItem10,
									listViewItem11,
									listViewItem12,
									listViewItem13,
									listViewItem14,
									listViewItem15});
			this.lvFilesCount.Location = new System.Drawing.Point(3, 30);
			this.lvFilesCount.Name = "lvFilesCount";
			this.lvFilesCount.Size = new System.Drawing.Size(309, 238);
			this.lvFilesCount.TabIndex = 10;
			this.lvFilesCount.UseCompatibleStateImageBehavior = false;
			this.lvFilesCount.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Папки и файлы";
			this.columnHeader6.Width = 220;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Количество";
			this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader7.Width = 80;
			// 
			// pProgress
			// 
			this.pProgress.Controls.Add(this.gBoxFullSortTemplatesDescription);
			this.pProgress.Controls.Add(this.chBoxViewProgress);
			this.pProgress.Controls.Add(this.lvFilesCount);
			this.pProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pProgress.Location = new System.Drawing.Point(0, 270);
			this.pProgress.Name = "pProgress";
			this.pProgress.Size = new System.Drawing.Size(828, 268);
			this.pProgress.TabIndex = 33;
			// 
			// chBoxViewProgress
			// 
			this.chBoxViewProgress.Checked = true;
			this.chBoxViewProgress.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chBoxViewProgress.Dock = System.Windows.Forms.DockStyle.Top;
			this.chBoxViewProgress.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxViewProgress.Location = new System.Drawing.Point(0, 0);
			this.chBoxViewProgress.Name = "chBoxViewProgress";
			this.chBoxViewProgress.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.chBoxViewProgress.Size = new System.Drawing.Size(828, 24);
			this.chBoxViewProgress.TabIndex = 23;
			this.chBoxViewProgress.Text = "Отображать изменение хода работы";
			this.chBoxViewProgress.UseVisualStyleBackColor = true;
			// 
			// sfdSaveXMLFile
			// 
			this.sfdSaveXMLFile.RestoreDirectory = true;
			this.sfdSaveXMLFile.Title = "Сохранение Данных для Избранной Сортировки в файл";
			// 
			// sfdOpenXMLFile
			// 
			this.sfdOpenXMLFile.RestoreDirectory = true;
			this.sfdOpenXMLFile.Title = "Загрузка Данных для Избранной Сортировки из файла";
			// 
			// SFBTpFileManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tcSort);
			this.Controls.Add(this.pProgress);
			this.Controls.Add(this.ssProgress);
			this.Name = "SFBTpFileManager";
			this.Size = new System.Drawing.Size(828, 560);
			this.ssProgress.ResumeLayout(false);
			this.ssProgress.PerformLayout();
			this.tcSort.ResumeLayout(false);
			this.tpFullSort.ResumeLayout(false);
			this.panelStart.ResumeLayout(false);
			this.cmsItems.ResumeLayout(false);
			this.panelAddress.ResumeLayout(false);
			this.panelAddress.PerformLayout();
			this.gBoxFullSortRenameTemplates.ResumeLayout(false);
			this.gBoxFullSortRenameTemplates.PerformLayout();
			this.tpSelectedSort.ResumeLayout(false);
			this.tpSelectedSort.PerformLayout();
			this.pData.ResumeLayout(false);
			this.pSSData.ResumeLayout(false);
			this.gBoxSelectedlSortRenameTemplates.ResumeLayout(false);
			this.gBoxSelectedlSortRenameTemplates.PerformLayout();
			this.pSelectedSortDirs.ResumeLayout(false);
			this.pSelectedSortDirs.PerformLayout();
			this.tsSelectedSort.ResumeLayout(false);
			this.tsSelectedSort.PerformLayout();
			this.gBoxFullSortTemplatesDescription.ResumeLayout(false);
			this.pProgress.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.CheckBox chBoxScanSubDir;
		private System.Windows.Forms.ColumnHeader colHeaderFileName;
		private System.Windows.Forms.ColumnHeader colHeaderNewFileName;
		private System.Windows.Forms.ColumnHeader colHeaderBookName;
		private System.Windows.Forms.ColumnHeader colHeaderLang;
		private System.Windows.Forms.ColumnHeader colHeaderGenre;
		private System.Windows.Forms.ColumnHeader colHeaderFIOBookAuthor;
		private System.Windows.Forms.ColumnHeader colHeaderSequenceNumber;
		private System.Windows.Forms.ColumnHeader colHeaderSequence;
		private System.Windows.Forms.Button buttonFullSortStop;
		private System.Windows.Forms.ToolStripMenuItem tsmiUnCheckedAllSelected;
		private System.Windows.Forms.ToolStripMenuItem tsmiCheckedAllSelected;
		private System.Windows.Forms.ToolStripMenuItem tsmiZipCheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiZipUnCheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiFB2UnCheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiFB2CheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiDirCheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiDirUnCheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiFilesUnCheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiFilesCheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiUnCheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiCheckedAll;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ContextMenuStrip cmsItems;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripSeparator tsmi3;
		private System.Windows.Forms.ImageList imageListItems;
		private System.Windows.Forms.Button buttonSortFilesTo;
		private System.Windows.Forms.Button buttonOpenSourceDir;
		private System.Windows.Forms.Label labelAddress;
		private System.Windows.Forms.TextBox textBoxAddress;
		private System.Windows.Forms.Button buttonGo;
		private System.Windows.Forms.Panel panelAddress;
		private System.Windows.Forms.ListView listViewSource;
		private System.Windows.Forms.CheckBox checkBoxDirsView;
		private System.Windows.Forms.Panel panelStart;
		private System.Windows.Forms.CheckBox chBoxViewProgress;
		private System.Windows.Forms.Panel pSSData;
		private System.Windows.Forms.OpenFileDialog sfdOpenXMLFile;
		private System.Windows.Forms.Button btnSSDataListLoad;
		private System.Windows.Forms.SaveFileDialog sfdSaveXMLFile;
		private System.Windows.Forms.Button btnSSDataListSave;
		private System.Windows.Forms.ColumnHeader cHeaderBookTitle;
		private System.Windows.Forms.ToolStripButton tsbtnSSSortStop;
		private System.Windows.Forms.ColumnHeader cHeaderExactFit;
		private System.Windows.Forms.Panel pData;
		private System.Windows.Forms.Button btnSSGetData;
		private System.Windows.Forms.ColumnHeader cHeaderSequence;
		private System.Windows.Forms.ColumnHeader cHeaderNick;
		private System.Windows.Forms.ColumnHeader cHeaderMiddle;
		private System.Windows.Forms.ColumnHeader cHeaderFirst;
		private System.Windows.Forms.ColumnHeader cHeaderLast;
		private System.Windows.Forms.ColumnHeader cHeaderGenre;
		private System.Windows.Forms.ColumnHeader cHeaderGenresGroup;
		private System.Windows.Forms.ColumnHeader cHeaderLang;
		private System.Windows.Forms.ListView lvSSData;
		private System.Windows.Forms.TextBox txtBoxSSTemplatesFromLine;
		private System.Windows.Forms.Button btnSSInsertTemplates;
		private System.Windows.Forms.GroupBox gBoxSelectedlSortRenameTemplates;
		private System.Windows.Forms.TextBox tboxSSSourceDir;
		private System.Windows.Forms.TextBox tboxSSToDir;
		private System.Windows.Forms.CheckBox chBoxSSScanSubDir;
		private System.Windows.Forms.Label lbSSlDir;
		private System.Windows.Forms.Label lblSSTargetDir;
		private System.Windows.Forms.Panel pSelectedSortDirs;
		private System.Windows.Forms.ToolStripButton tsbtnSSSortFilesTo;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton tsbtnSSTargetDir;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsbtnSSOpenDir;
		private System.Windows.Forms.ToolStrip tsSelectedSort;
		private System.Windows.Forms.GroupBox gBoxFullSortTemplatesDescription;
		private System.Windows.Forms.GroupBox gBoxFullSortRenameTemplates;
		private System.Windows.Forms.TabPage tpSelectedSort;
		private System.Windows.Forms.TabPage tpFullSort;
		private System.Windows.Forms.TabControl tcSort;
		private System.Windows.Forms.Button btnInsertTemplates;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ListView lvFilesCount;
		private System.Windows.Forms.Panel pProgress;
		private System.Windows.Forms.RichTextBox richTxtBoxDescTemplates;
		private System.Windows.Forms.TextBox txtBoxTemplatesFromLine;
		private System.Windows.Forms.FolderBrowserDialog fbdScanDir;
		private System.Windows.Forms.ToolStripProgressBar tsProgressBar;
		private System.Windows.Forms.ToolStripStatusLabel tsslblProgress;
		private System.Windows.Forms.StatusStrip ssProgress;
	}
}
