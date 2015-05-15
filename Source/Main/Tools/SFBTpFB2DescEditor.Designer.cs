/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 13.02.2015
 * Время: 9:34
 * 
 */
namespace SharpFBTools.Tools
{
	partial class SFBTpFB2DescEditor
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SFBTpFB2DescEditor));
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
			"Всего папок",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
			"Всего файлов",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
			"Исходные fb2-файлы",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
			"Исходные  Zip-архивы с fb2",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
			"Исходные  Rar-архивы с fb2",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
			"Исходные 7zip-архивы с fb2",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
			"Исходные BZip2-архивы с fb2",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem(new string[] {
			"Исходные Gzip-архивы с fb2",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem(new string[] {
			"Исходные Tar-пакеты с fb2",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem(new string[] {
			"Исходные fb2-файлы из архивов",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem(new string[] {
			"Другие файлы",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem(new string[] {
			"Создано в папке-приемнике",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem(new string[] {
			"Нечитаемые fb2-файлы (архивы)",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem(new string[] {
			"Не валидные fb2-файлы (при вкл. опции)",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem(new string[] {
			"Битые архивы (не открылись)",
			"0"}, 0);
			this.ssProgress = new System.Windows.Forms.StatusStrip();
			this.tsslblProgress = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.fbdScanDir = new System.Windows.Forms.FolderBrowserDialog();
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
			this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiColumnsExplorerAutoReize = new System.Windows.Forms.ToolStripMenuItem();
			this.imageListItems = new System.Windows.Forms.ImageList(this.components);
			this.tcDesc = new System.Windows.Forms.TabControl();
			this.tpFullSort = new System.Windows.Forms.TabPage();
			this.listViewSource = new System.Windows.Forms.ListView();
			this.colHeaderFileName = new System.Windows.Forms.ColumnHeader();
			this.colHeaderBookName = new System.Windows.Forms.ColumnHeader();
			this.colHeaderSequence = new System.Windows.Forms.ColumnHeader();
			this.colHeaderFIOBookAuthor = new System.Windows.Forms.ColumnHeader();
			this.colHeaderGenre = new System.Windows.Forms.ColumnHeader();
			this.colHeaderLang = new System.Windows.Forms.ColumnHeader();
			this.colHeaderEncoding = new System.Windows.Forms.ColumnHeader();
			this.panelExplorerAddress = new System.Windows.Forms.Panel();
			this.panelAddress = new System.Windows.Forms.Panel();
			this.chBoxStartExplorerColumnsAutoReize = new System.Windows.Forms.CheckBox();
			this.checkBoxTagsView = new System.Windows.Forms.CheckBox();
			this.buttonGo = new System.Windows.Forms.Button();
			this.textBoxAddress = new System.Windows.Forms.TextBox();
			this.labelAddress = new System.Windows.Forms.Label();
			this.buttonOpenSourceDir = new System.Windows.Forms.Button();
			this.tpDescSelected = new System.Windows.Forms.TabPage();
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBoxSearchMode = new System.Windows.Forms.GroupBox();
			this.rbSearchModeAllBooks = new System.Windows.Forms.RadioButton();
			this.rbSearchModeFromLetter = new System.Windows.Forms.RadioButton();
			this.pSearchFBDup2Dirs = new System.Windows.Forms.Panel();
			this.buttonTargetDir = new System.Windows.Forms.Button();
			this.buttonSourceDir = new System.Windows.Forms.Button();
			this.lblDupToDir = new System.Windows.Forms.Label();
			this.tboxTargetDir = new System.Windows.Forms.TextBox();
			this.chBoxScanSubDir = new System.Windows.Forms.CheckBox();
			this.tboxSourceDir = new System.Windows.Forms.TextBox();
			this.lblScanDir = new System.Windows.Forms.Label();
			this.paneBuildListProcess = new System.Windows.Forms.Panel();
			this.buttonBuildListStop = new System.Windows.Forms.Button();
			this.buttonBuildList = new System.Windows.Forms.Button();
			this.tcLog = new System.Windows.Forms.TabPage();
			this.textBoxFiles = new System.Windows.Forms.TextBox();
			this.pProgress = new System.Windows.Forms.Panel();
			this.panelData = new System.Windows.Forms.Panel();
			this.chBoxViewProgress = new System.Windows.Forms.CheckBox();
			this.lvFilesCount = new System.Windows.Forms.ListView();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.ssProgress.SuspendLayout();
			this.cmsItems.SuspendLayout();
			this.tcDesc.SuspendLayout();
			this.tpFullSort.SuspendLayout();
			this.panelExplorerAddress.SuspendLayout();
			this.panelAddress.SuspendLayout();
			this.tpDescSelected.SuspendLayout();
			this.panel1.SuspendLayout();
			this.groupBoxSearchMode.SuspendLayout();
			this.pSearchFBDup2Dirs.SuspendLayout();
			this.paneBuildListProcess.SuspendLayout();
			this.tcLog.SuspendLayout();
			this.pProgress.SuspendLayout();
			this.panelData.SuspendLayout();
			this.SuspendLayout();
			// 
			// ssProgress
			// 
			this.ssProgress.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.ssProgress.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.tsslblProgress,
			this.tsProgressBar});
			this.ssProgress.Location = new System.Drawing.Point(0, 661);
			this.ssProgress.Name = "ssProgress";
			this.ssProgress.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
			this.ssProgress.Size = new System.Drawing.Size(1429, 26);
			this.ssProgress.TabIndex = 41;
			this.ssProgress.Text = "statusStrip1";
			// 
			// tsslblProgress
			// 
			this.tsslblProgress.Name = "tsslblProgress";
			this.tsslblProgress.Size = new System.Drawing.Size(60, 21);
			this.tsslblProgress.Text = "Готово.";
			// 
			// tsProgressBar
			// 
			this.tsProgressBar.Name = "tsProgressBar";
			this.tsProgressBar.Size = new System.Drawing.Size(933, 20);
			// 
			// fbdScanDir
			// 
			this.fbdScanDir.Description = "Укажите папку для сканирования с fb2-файлами:";
			// 
			// cmsItems
			// 
			this.cmsItems.ImageScalingSize = new System.Drawing.Size(20, 20);
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
			this.tsmiUnCheckedAllSelected,
			this.toolStripMenuItem6,
			this.tsmiColumnsExplorerAutoReize});
			this.cmsItems.Name = "cmsValidator";
			this.cmsItems.Size = new System.Drawing.Size(392, 384);
			// 
			// tsmi3
			// 
			this.tsmi3.Name = "tsmi3";
			this.tsmi3.Size = new System.Drawing.Size(388, 6);
			// 
			// tsmiCheckedAll
			// 
			this.tsmiCheckedAll.Image = ((System.Drawing.Image)(resources.GetObject("tsmiCheckedAll.Image")));
			this.tsmiCheckedAll.Name = "tsmiCheckedAll";
			this.tsmiCheckedAll.Size = new System.Drawing.Size(391, 26);
			this.tsmiCheckedAll.Text = "Пометить все файлы и папки";
			// 
			// tsmiUnCheckedAll
			// 
			this.tsmiUnCheckedAll.Image = ((System.Drawing.Image)(resources.GetObject("tsmiUnCheckedAll.Image")));
			this.tsmiUnCheckedAll.Name = "tsmiUnCheckedAll";
			this.tsmiUnCheckedAll.Size = new System.Drawing.Size(391, 26);
			this.tsmiUnCheckedAll.Text = "Снять пометки со всех файлов и папок";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(388, 6);
			// 
			// tsmiFilesCheckedAll
			// 
			this.tsmiFilesCheckedAll.Name = "tsmiFilesCheckedAll";
			this.tsmiFilesCheckedAll.Size = new System.Drawing.Size(391, 26);
			this.tsmiFilesCheckedAll.Text = "Пометить все файлы";
			// 
			// tsmiFilesUnCheckedAll
			// 
			this.tsmiFilesUnCheckedAll.Name = "tsmiFilesUnCheckedAll";
			this.tsmiFilesUnCheckedAll.Size = new System.Drawing.Size(391, 26);
			this.tsmiFilesUnCheckedAll.Text = "Снять пометки со всех файлов";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(388, 6);
			// 
			// tsmiDirCheckedAll
			// 
			this.tsmiDirCheckedAll.Name = "tsmiDirCheckedAll";
			this.tsmiDirCheckedAll.Size = new System.Drawing.Size(391, 26);
			this.tsmiDirCheckedAll.Text = "Пометить все папки";
			// 
			// tsmiDirUnCheckedAll
			// 
			this.tsmiDirUnCheckedAll.Name = "tsmiDirUnCheckedAll";
			this.tsmiDirUnCheckedAll.Size = new System.Drawing.Size(391, 26);
			this.tsmiDirUnCheckedAll.Text = "Снять пометки со всех папок";
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(388, 6);
			// 
			// tsmiFB2CheckedAll
			// 
			this.tsmiFB2CheckedAll.Name = "tsmiFB2CheckedAll";
			this.tsmiFB2CheckedAll.Size = new System.Drawing.Size(391, 26);
			this.tsmiFB2CheckedAll.Text = "Пометить все fb2 файлы";
			// 
			// tsmiFB2UnCheckedAll
			// 
			this.tsmiFB2UnCheckedAll.Name = "tsmiFB2UnCheckedAll";
			this.tsmiFB2UnCheckedAll.Size = new System.Drawing.Size(391, 26);
			this.tsmiFB2UnCheckedAll.Text = "Снять пометки со всех fb2 файлов";
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(388, 6);
			// 
			// tsmiZipCheckedAll
			// 
			this.tsmiZipCheckedAll.Name = "tsmiZipCheckedAll";
			this.tsmiZipCheckedAll.Size = new System.Drawing.Size(391, 26);
			this.tsmiZipCheckedAll.Text = "Пометить все zip файлы";
			// 
			// tsmiZipUnCheckedAll
			// 
			this.tsmiZipUnCheckedAll.Name = "tsmiZipUnCheckedAll";
			this.tsmiZipUnCheckedAll.Size = new System.Drawing.Size(391, 26);
			this.tsmiZipUnCheckedAll.Text = "Снять пометки со всех zip файлов";
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(388, 6);
			// 
			// tsmiCheckedAllSelected
			// 
			this.tsmiCheckedAllSelected.Name = "tsmiCheckedAllSelected";
			this.tsmiCheckedAllSelected.Size = new System.Drawing.Size(391, 26);
			this.tsmiCheckedAllSelected.Text = "Пометить всё выделенное";
			// 
			// tsmiUnCheckedAllSelected
			// 
			this.tsmiUnCheckedAllSelected.Name = "tsmiUnCheckedAllSelected";
			this.tsmiUnCheckedAllSelected.Size = new System.Drawing.Size(391, 26);
			this.tsmiUnCheckedAllSelected.Text = "Снять пометки со всего выделенного";
			// 
			// toolStripMenuItem6
			// 
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			this.toolStripMenuItem6.Size = new System.Drawing.Size(388, 6);
			// 
			// tsmiColumnsExplorerAutoReize
			// 
			this.tsmiColumnsExplorerAutoReize.Name = "tsmiColumnsExplorerAutoReize";
			this.tsmiColumnsExplorerAutoReize.Size = new System.Drawing.Size(391, 26);
			this.tsmiColumnsExplorerAutoReize.Text = "Обновить авторазмер колонок Проводника";
			// 
			// imageListItems
			// 
			this.imageListItems.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListItems.ImageStream")));
			this.imageListItems.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListItems.Images.SetKeyName(0, "0_folder.png");
			this.imageListItems.Images.SetKeyName(1, "1_fb2.png");
			this.imageListItems.Images.SetKeyName(2, "2_zip.png");
			this.imageListItems.Images.SetKeyName(3, "3_up.png");
			this.imageListItems.Images.SetKeyName(4, "options.png");
			// 
			// tcDesc
			// 
			this.tcDesc.Controls.Add(this.tpFullSort);
			this.tcDesc.Controls.Add(this.tpDescSelected);
			this.tcDesc.Controls.Add(this.tcLog);
			this.tcDesc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcDesc.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.tcDesc.Location = new System.Drawing.Point(0, 0);
			this.tcDesc.Margin = new System.Windows.Forms.Padding(4);
			this.tcDesc.Name = "tcDesc";
			this.tcDesc.SelectedIndex = 0;
			this.tcDesc.Size = new System.Drawing.Size(1429, 661);
			this.tcDesc.TabIndex = 42;
			// 
			// tpFullSort
			// 
			this.tpFullSort.Controls.Add(this.listViewSource);
			this.tpFullSort.Controls.Add(this.panelExplorerAddress);
			this.tpFullSort.Font = new System.Drawing.Font("Tahoma", 8F);
			this.tpFullSort.Location = new System.Drawing.Point(4, 25);
			this.tpFullSort.Margin = new System.Windows.Forms.Padding(4);
			this.tpFullSort.Name = "tpFullSort";
			this.tpFullSort.Padding = new System.Windows.Forms.Padding(4);
			this.tpFullSort.Size = new System.Drawing.Size(1421, 632);
			this.tpFullSort.TabIndex = 0;
			this.tpFullSort.Text = " Проводник ";
			this.tpFullSort.UseVisualStyleBackColor = true;
			// 
			// listViewSource
			// 
			this.listViewSource.AllowColumnReorder = true;
			this.listViewSource.CheckBoxes = true;
			this.listViewSource.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.colHeaderFileName,
			this.colHeaderBookName,
			this.colHeaderSequence,
			this.colHeaderFIOBookAuthor,
			this.colHeaderGenre,
			this.colHeaderLang,
			this.colHeaderEncoding});
			this.listViewSource.ContextMenuStrip = this.cmsItems;
			this.listViewSource.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewSource.FullRowSelect = true;
			this.listViewSource.GridLines = true;
			this.listViewSource.Location = new System.Drawing.Point(4, 72);
			this.listViewSource.Margin = new System.Windows.Forms.Padding(4);
			this.listViewSource.Name = "listViewSource";
			this.listViewSource.ShowItemToolTips = true;
			this.listViewSource.Size = new System.Drawing.Size(1413, 556);
			this.listViewSource.SmallImageList = this.imageListItems;
			this.listViewSource.TabIndex = 35;
			this.listViewSource.UseCompatibleStateImageBehavior = false;
			this.listViewSource.View = System.Windows.Forms.View.Details;
			// 
			// colHeaderFileName
			// 
			this.colHeaderFileName.Text = "Имя файла";
			// 
			// colHeaderBookName
			// 
			this.colHeaderBookName.Text = "Книга";
			// 
			// colHeaderSequence
			// 
			this.colHeaderSequence.Text = "Серия (№)";
			// 
			// colHeaderFIOBookAuthor
			// 
			this.colHeaderFIOBookAuthor.Text = "Автор(ы)";
			// 
			// colHeaderGenre
			// 
			this.colHeaderGenre.Text = "Жанр(ы)";
			// 
			// colHeaderLang
			// 
			this.colHeaderLang.Text = "Язык";
			// 
			// colHeaderEncoding
			// 
			this.colHeaderEncoding.Text = "Кодировка";
			// 
			// panelExplorerAddress
			// 
			this.panelExplorerAddress.Controls.Add(this.panelAddress);
			this.panelExplorerAddress.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelExplorerAddress.Location = new System.Drawing.Point(4, 4);
			this.panelExplorerAddress.Margin = new System.Windows.Forms.Padding(4);
			this.panelExplorerAddress.Name = "panelExplorerAddress";
			this.panelExplorerAddress.Size = new System.Drawing.Size(1413, 68);
			this.panelExplorerAddress.TabIndex = 37;
			// 
			// panelAddress
			// 
			this.panelAddress.Controls.Add(this.chBoxStartExplorerColumnsAutoReize);
			this.panelAddress.Controls.Add(this.checkBoxTagsView);
			this.panelAddress.Controls.Add(this.buttonGo);
			this.panelAddress.Controls.Add(this.textBoxAddress);
			this.panelAddress.Controls.Add(this.labelAddress);
			this.panelAddress.Controls.Add(this.buttonOpenSourceDir);
			this.panelAddress.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelAddress.Location = new System.Drawing.Point(0, 0);
			this.panelAddress.Margin = new System.Windows.Forms.Padding(4);
			this.panelAddress.Name = "panelAddress";
			this.panelAddress.Size = new System.Drawing.Size(1413, 68);
			this.panelAddress.TabIndex = 38;
			// 
			// chBoxStartExplorerColumnsAutoReize
			// 
			this.chBoxStartExplorerColumnsAutoReize.Checked = true;
			this.chBoxStartExplorerColumnsAutoReize.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chBoxStartExplorerColumnsAutoReize.Font = new System.Drawing.Font("Tahoma", 8F);
			this.chBoxStartExplorerColumnsAutoReize.Location = new System.Drawing.Point(128, 34);
			this.chBoxStartExplorerColumnsAutoReize.Margin = new System.Windows.Forms.Padding(4);
			this.chBoxStartExplorerColumnsAutoReize.Name = "chBoxStartExplorerColumnsAutoReize";
			this.chBoxStartExplorerColumnsAutoReize.Size = new System.Drawing.Size(287, 30);
			this.chBoxStartExplorerColumnsAutoReize.TabIndex = 8;
			this.chBoxStartExplorerColumnsAutoReize.Text = "Авторазмер колонок Проводника";
			this.chBoxStartExplorerColumnsAutoReize.UseVisualStyleBackColor = true;
			// 
			// checkBoxTagsView
			// 
			this.checkBoxTagsView.Font = new System.Drawing.Font("Tahoma", 8F);
			this.checkBoxTagsView.Location = new System.Drawing.Point(452, 34);
			this.checkBoxTagsView.Margin = new System.Windows.Forms.Padding(4);
			this.checkBoxTagsView.Name = "checkBoxTagsView";
			this.checkBoxTagsView.Size = new System.Drawing.Size(357, 30);
			this.checkBoxTagsView.TabIndex = 5;
			this.checkBoxTagsView.Text = "Показывать описание книг в Проводнике";
			this.checkBoxTagsView.UseVisualStyleBackColor = true;
			// 
			// buttonGo
			// 
			this.buttonGo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.buttonGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonGo.Location = new System.Drawing.Point(1214, 5);
			this.buttonGo.Margin = new System.Windows.Forms.Padding(4);
			this.buttonGo.Name = "buttonGo";
			this.buttonGo.Size = new System.Drawing.Size(189, 30);
			this.buttonGo.TabIndex = 6;
			this.buttonGo.Text = "Перейти/Обновить";
			this.buttonGo.UseVisualStyleBackColor = true;
			// 
			// textBoxAddress
			// 
			this.textBoxAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxAddress.Location = new System.Drawing.Point(127, 7);
			this.textBoxAddress.Margin = new System.Windows.Forms.Padding(4);
			this.textBoxAddress.Name = "textBoxAddress";
			this.textBoxAddress.Size = new System.Drawing.Size(1078, 24);
			this.textBoxAddress.TabIndex = 5;
			// 
			// labelAddress
			// 
			this.labelAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelAddress.Location = new System.Drawing.Point(59, 9);
			this.labelAddress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelAddress.Name = "labelAddress";
			this.labelAddress.Size = new System.Drawing.Size(63, 23);
			this.labelAddress.TabIndex = 4;
			this.labelAddress.Text = "Адрес:";
			this.labelAddress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// buttonOpenSourceDir
			// 
			this.buttonOpenSourceDir.Image = ((System.Drawing.Image)(resources.GetObject("buttonOpenSourceDir.Image")));
			this.buttonOpenSourceDir.Location = new System.Drawing.Point(4, 4);
			this.buttonOpenSourceDir.Margin = new System.Windows.Forms.Padding(4);
			this.buttonOpenSourceDir.Name = "buttonOpenSourceDir";
			this.buttonOpenSourceDir.Size = new System.Drawing.Size(41, 33);
			this.buttonOpenSourceDir.TabIndex = 7;
			this.buttonOpenSourceDir.UseVisualStyleBackColor = true;
			// 
			// tpDescSelected
			// 
			this.tpDescSelected.Controls.Add(this.panel1);
			this.tpDescSelected.Controls.Add(this.pSearchFBDup2Dirs);
			this.tpDescSelected.Controls.Add(this.paneBuildListProcess);
			this.tpDescSelected.Font = new System.Drawing.Font("Tahoma", 8F);
			this.tpDescSelected.Location = new System.Drawing.Point(4, 25);
			this.tpDescSelected.Margin = new System.Windows.Forms.Padding(4);
			this.tpDescSelected.Name = "tpDescSelected";
			this.tpDescSelected.Padding = new System.Windows.Forms.Padding(4);
			this.tpDescSelected.Size = new System.Drawing.Size(1421, 632);
			this.tpDescSelected.TabIndex = 1;
			this.tpDescSelected.Text = " Поиск по критерию ";
			this.tpDescSelected.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.groupBoxSearchMode);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(4, 80);
			this.panel1.Margin = new System.Windows.Forms.Padding(4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1413, 101);
			this.panel1.TabIndex = 65;
			// 
			// groupBoxSearchMode
			// 
			this.groupBoxSearchMode.Controls.Add(this.rbSearchModeAllBooks);
			this.groupBoxSearchMode.Controls.Add(this.rbSearchModeFromLetter);
			this.groupBoxSearchMode.Location = new System.Drawing.Point(4, 4);
			this.groupBoxSearchMode.Margin = new System.Windows.Forms.Padding(4);
			this.groupBoxSearchMode.Name = "groupBoxSearchMode";
			this.groupBoxSearchMode.Padding = new System.Windows.Forms.Padding(4);
			this.groupBoxSearchMode.Size = new System.Drawing.Size(216, 95);
			this.groupBoxSearchMode.TabIndex = 10;
			this.groupBoxSearchMode.TabStop = false;
			this.groupBoxSearchMode.Text = "Режим поиска книг";
			// 
			// rbSearchModeAllBooks
			// 
			this.rbSearchModeAllBooks.Location = new System.Drawing.Point(8, 17);
			this.rbSearchModeAllBooks.Margin = new System.Windows.Forms.Padding(4);
			this.rbSearchModeAllBooks.Name = "rbSearchModeAllBooks";
			this.rbSearchModeAllBooks.Size = new System.Drawing.Size(115, 30);
			this.rbSearchModeAllBooks.TabIndex = 8;
			this.rbSearchModeAllBooks.TabStop = true;
			this.rbSearchModeAllBooks.Text = "Все книги";
			this.rbSearchModeAllBooks.UseVisualStyleBackColor = true;
			// 
			// rbSearchModeFromLetter
			// 
			this.rbSearchModeFromLetter.Location = new System.Drawing.Point(8, 47);
			this.rbSearchModeFromLetter.Margin = new System.Windows.Forms.Padding(4);
			this.rbSearchModeFromLetter.Name = "rbSearchModeFromLetter";
			this.rbSearchModeFromLetter.Size = new System.Drawing.Size(167, 30);
			this.rbSearchModeFromLetter.TabIndex = 9;
			this.rbSearchModeFromLetter.TabStop = true;
			this.rbSearchModeFromLetter.Text = "По заданной Букве";
			this.rbSearchModeFromLetter.UseVisualStyleBackColor = true;
			// 
			// pSearchFBDup2Dirs
			// 
			this.pSearchFBDup2Dirs.Controls.Add(this.buttonTargetDir);
			this.pSearchFBDup2Dirs.Controls.Add(this.buttonSourceDir);
			this.pSearchFBDup2Dirs.Controls.Add(this.lblDupToDir);
			this.pSearchFBDup2Dirs.Controls.Add(this.tboxTargetDir);
			this.pSearchFBDup2Dirs.Controls.Add(this.chBoxScanSubDir);
			this.pSearchFBDup2Dirs.Controls.Add(this.tboxSourceDir);
			this.pSearchFBDup2Dirs.Controls.Add(this.lblScanDir);
			this.pSearchFBDup2Dirs.Dock = System.Windows.Forms.DockStyle.Top;
			this.pSearchFBDup2Dirs.Location = new System.Drawing.Point(4, 4);
			this.pSearchFBDup2Dirs.Margin = new System.Windows.Forms.Padding(4);
			this.pSearchFBDup2Dirs.Name = "pSearchFBDup2Dirs";
			this.pSearchFBDup2Dirs.Size = new System.Drawing.Size(1413, 76);
			this.pSearchFBDup2Dirs.TabIndex = 64;
			// 
			// buttonTargetDir
			// 
			this.buttonTargetDir.Image = ((System.Drawing.Image)(resources.GetObject("buttonTargetDir.Image")));
			this.buttonTargetDir.Location = new System.Drawing.Point(221, 38);
			this.buttonTargetDir.Margin = new System.Windows.Forms.Padding(4);
			this.buttonTargetDir.Name = "buttonTargetDir";
			this.buttonTargetDir.Size = new System.Drawing.Size(41, 33);
			this.buttonTargetDir.TabIndex = 22;
			this.buttonTargetDir.UseVisualStyleBackColor = true;
			// 
			// buttonSourceDir
			// 
			this.buttonSourceDir.Image = ((System.Drawing.Image)(resources.GetObject("buttonSourceDir.Image")));
			this.buttonSourceDir.Location = new System.Drawing.Point(221, 4);
			this.buttonSourceDir.Margin = new System.Windows.Forms.Padding(4);
			this.buttonSourceDir.Name = "buttonSourceDir";
			this.buttonSourceDir.Size = new System.Drawing.Size(41, 33);
			this.buttonSourceDir.TabIndex = 21;
			this.buttonSourceDir.UseVisualStyleBackColor = true;
			// 
			// lblDupToDir
			// 
			this.lblDupToDir.AutoSize = true;
			this.lblDupToDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblDupToDir.Location = new System.Drawing.Point(4, 46);
			this.lblDupToDir.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDupToDir.Name = "lblDupToDir";
			this.lblDupToDir.Size = new System.Drawing.Size(197, 17);
			this.lblDupToDir.TabIndex = 20;
			this.lblDupToDir.Text = "Папка-приемник файлов:";
			// 
			// tboxTargetDir
			// 
			this.tboxTargetDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxTargetDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.tboxTargetDir.Location = new System.Drawing.Point(271, 43);
			this.tboxTargetDir.Margin = new System.Windows.Forms.Padding(4);
			this.tboxTargetDir.Name = "tboxTargetDir";
			this.tboxTargetDir.Size = new System.Drawing.Size(869, 24);
			this.tboxTargetDir.TabIndex = 19;
			// 
			// chBoxScanSubDir
			// 
			this.chBoxScanSubDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chBoxScanSubDir.Checked = true;
			this.chBoxScanSubDir.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chBoxScanSubDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxScanSubDir.ForeColor = System.Drawing.Color.Navy;
			this.chBoxScanSubDir.Location = new System.Drawing.Point(1149, 5);
			this.chBoxScanSubDir.Margin = new System.Windows.Forms.Padding(4);
			this.chBoxScanSubDir.Name = "chBoxScanSubDir";
			this.chBoxScanSubDir.Size = new System.Drawing.Size(260, 30);
			this.chBoxScanSubDir.TabIndex = 2;
			this.chBoxScanSubDir.Text = "Обрабатывать подкаталоги";
			this.chBoxScanSubDir.UseVisualStyleBackColor = true;
			// 
			// tboxSourceDir
			// 
			this.tboxSourceDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxSourceDir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tboxSourceDir.Location = new System.Drawing.Point(271, 7);
			this.tboxSourceDir.Margin = new System.Windows.Forms.Padding(4);
			this.tboxSourceDir.Name = "tboxSourceDir";
			this.tboxSourceDir.Size = new System.Drawing.Size(869, 24);
			this.tboxSourceDir.TabIndex = 1;
			// 
			// lblScanDir
			// 
			this.lblScanDir.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.lblScanDir.Location = new System.Drawing.Point(4, 10);
			this.lblScanDir.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblScanDir.Name = "lblScanDir";
			this.lblScanDir.Size = new System.Drawing.Size(216, 23);
			this.lblScanDir.TabIndex = 6;
			this.lblScanDir.Text = "Папка для сканирования:";
			// 
			// paneBuildListProcess
			// 
			this.paneBuildListProcess.BackColor = System.Drawing.SystemColors.Control;
			this.paneBuildListProcess.Controls.Add(this.buttonBuildListStop);
			this.paneBuildListProcess.Controls.Add(this.buttonBuildList);
			this.paneBuildListProcess.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.paneBuildListProcess.Location = new System.Drawing.Point(4, 557);
			this.paneBuildListProcess.Margin = new System.Windows.Forms.Padding(4);
			this.paneBuildListProcess.Name = "paneBuildListProcess";
			this.paneBuildListProcess.Size = new System.Drawing.Size(1413, 71);
			this.paneBuildListProcess.TabIndex = 63;
			// 
			// buttonBuildListStop
			// 
			this.buttonBuildListStop.Enabled = false;
			this.buttonBuildListStop.Font = new System.Drawing.Font("Tahoma", 11F);
			this.buttonBuildListStop.Image = ((System.Drawing.Image)(resources.GetObject("buttonBuildListStop.Image")));
			this.buttonBuildListStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonBuildListStop.Location = new System.Drawing.Point(4, 7);
			this.buttonBuildListStop.Margin = new System.Windows.Forms.Padding(4);
			this.buttonBuildListStop.Name = "buttonBuildListStop";
			this.buttonBuildListStop.Size = new System.Drawing.Size(239, 60);
			this.buttonBuildListStop.TabIndex = 3;
			this.buttonBuildListStop.Text = "Остановить     ";
			this.buttonBuildListStop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.buttonBuildListStop.UseVisualStyleBackColor = true;
			// 
			// buttonBuildList
			// 
			this.buttonBuildList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.buttonBuildList.Font = new System.Drawing.Font("Tahoma", 11F);
			this.buttonBuildList.Image = ((System.Drawing.Image)(resources.GetObject("buttonBuildList.Image")));
			this.buttonBuildList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonBuildList.Location = new System.Drawing.Point(1149, 7);
			this.buttonBuildList.Margin = new System.Windows.Forms.Padding(4);
			this.buttonBuildList.Name = "buttonBuildList";
			this.buttonBuildList.Size = new System.Drawing.Size(255, 60);
			this.buttonBuildList.TabIndex = 2;
			this.buttonBuildList.Text = "Построить список  ";
			this.buttonBuildList.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.buttonBuildList.UseVisualStyleBackColor = true;
			// 
			// tcLog
			// 
			this.tcLog.Controls.Add(this.textBoxFiles);
			this.tcLog.Controls.Add(this.pProgress);
			this.tcLog.Location = new System.Drawing.Point(4, 25);
			this.tcLog.Margin = new System.Windows.Forms.Padding(4);
			this.tcLog.Name = "tcLog";
			this.tcLog.Padding = new System.Windows.Forms.Padding(4);
			this.tcLog.Size = new System.Drawing.Size(1421, 632);
			this.tcLog.TabIndex = 2;
			this.tcLog.Text = "Статистика";
			this.tcLog.UseVisualStyleBackColor = true;
			// 
			// textBoxFiles
			// 
			this.textBoxFiles.BackColor = System.Drawing.SystemColors.Window;
			this.textBoxFiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxFiles.Font = new System.Drawing.Font("Tahoma", 8F);
			this.textBoxFiles.Location = new System.Drawing.Point(4, 4);
			this.textBoxFiles.Margin = new System.Windows.Forms.Padding(4);
			this.textBoxFiles.Multiline = true;
			this.textBoxFiles.Name = "textBoxFiles";
			this.textBoxFiles.ReadOnly = true;
			this.textBoxFiles.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxFiles.Size = new System.Drawing.Size(1413, 294);
			this.textBoxFiles.TabIndex = 35;
			// 
			// pProgress
			// 
			this.pProgress.Controls.Add(this.panelData);
			this.pProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pProgress.Location = new System.Drawing.Point(4, 298);
			this.pProgress.Margin = new System.Windows.Forms.Padding(4);
			this.pProgress.Name = "pProgress";
			this.pProgress.Size = new System.Drawing.Size(1413, 330);
			this.pProgress.TabIndex = 34;
			// 
			// panelData
			// 
			this.panelData.Controls.Add(this.chBoxViewProgress);
			this.panelData.Controls.Add(this.lvFilesCount);
			this.panelData.Location = new System.Drawing.Point(4, 4);
			this.panelData.Margin = new System.Windows.Forms.Padding(4);
			this.panelData.Name = "panelData";
			this.panelData.Size = new System.Drawing.Size(476, 322);
			this.panelData.TabIndex = 0;
			// 
			// chBoxViewProgress
			// 
			this.chBoxViewProgress.Checked = true;
			this.chBoxViewProgress.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chBoxViewProgress.Dock = System.Windows.Forms.DockStyle.Top;
			this.chBoxViewProgress.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxViewProgress.Location = new System.Drawing.Point(0, 0);
			this.chBoxViewProgress.Margin = new System.Windows.Forms.Padding(4);
			this.chBoxViewProgress.Name = "chBoxViewProgress";
			this.chBoxViewProgress.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
			this.chBoxViewProgress.Size = new System.Drawing.Size(476, 30);
			this.chBoxViewProgress.TabIndex = 25;
			this.chBoxViewProgress.Text = "Отображать изменение хода работы";
			this.chBoxViewProgress.UseVisualStyleBackColor = true;
			// 
			// lvFilesCount
			// 
			this.lvFilesCount.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader6,
			this.columnHeader7});
			this.lvFilesCount.Dock = System.Windows.Forms.DockStyle.Bottom;
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
			this.lvFilesCount.Location = new System.Drawing.Point(0, 30);
			this.lvFilesCount.Margin = new System.Windows.Forms.Padding(4);
			this.lvFilesCount.Name = "lvFilesCount";
			this.lvFilesCount.Size = new System.Drawing.Size(476, 292);
			this.lvFilesCount.TabIndex = 24;
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
			// SFBTpFB2DescEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tcDesc);
			this.Controls.Add(this.ssProgress);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "SFBTpFB2DescEditor";
			this.Size = new System.Drawing.Size(1429, 687);
			this.ssProgress.ResumeLayout(false);
			this.ssProgress.PerformLayout();
			this.cmsItems.ResumeLayout(false);
			this.tcDesc.ResumeLayout(false);
			this.tpFullSort.ResumeLayout(false);
			this.panelExplorerAddress.ResumeLayout(false);
			this.panelAddress.ResumeLayout(false);
			this.panelAddress.PerformLayout();
			this.tpDescSelected.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.groupBoxSearchMode.ResumeLayout(false);
			this.pSearchFBDup2Dirs.ResumeLayout(false);
			this.pSearchFBDup2Dirs.PerformLayout();
			this.paneBuildListProcess.ResumeLayout(false);
			this.tcLog.ResumeLayout(false);
			this.tcLog.PerformLayout();
			this.pProgress.ResumeLayout(false);
			this.panelData.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		private System.Windows.Forms.TabControl tcDesc;
		private System.Windows.Forms.TabPage tpFullSort;
		private System.Windows.Forms.ListView listViewSource;
		private System.Windows.Forms.ColumnHeader colHeaderFileName;
		private System.Windows.Forms.ColumnHeader colHeaderBookName;
		private System.Windows.Forms.ColumnHeader colHeaderSequence;
		private System.Windows.Forms.ColumnHeader colHeaderFIOBookAuthor;
		private System.Windows.Forms.ColumnHeader colHeaderGenre;
		private System.Windows.Forms.ColumnHeader colHeaderLang;
		private System.Windows.Forms.ColumnHeader colHeaderEncoding;
		private System.Windows.Forms.Panel panelExplorerAddress;
		private System.Windows.Forms.Panel panelAddress;
		private System.Windows.Forms.CheckBox chBoxStartExplorerColumnsAutoReize;
		private System.Windows.Forms.CheckBox checkBoxTagsView;
		private System.Windows.Forms.Button buttonGo;
		private System.Windows.Forms.TextBox textBoxAddress;
		private System.Windows.Forms.Label labelAddress;
		private System.Windows.Forms.Button buttonOpenSourceDir;
		private System.Windows.Forms.TabPage tpDescSelected;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox groupBoxSearchMode;
		private System.Windows.Forms.RadioButton rbSearchModeAllBooks;
		private System.Windows.Forms.RadioButton rbSearchModeFromLetter;
		private System.Windows.Forms.Panel pSearchFBDup2Dirs;
		private System.Windows.Forms.Button buttonTargetDir;
		private System.Windows.Forms.Button buttonSourceDir;
		private System.Windows.Forms.Label lblDupToDir;
		private System.Windows.Forms.TextBox tboxTargetDir;
		private System.Windows.Forms.CheckBox chBoxScanSubDir;
		private System.Windows.Forms.TextBox tboxSourceDir;
		private System.Windows.Forms.Label lblScanDir;
		private System.Windows.Forms.Panel paneBuildListProcess;
		private System.Windows.Forms.Button buttonBuildListStop;
		private System.Windows.Forms.Button buttonBuildList;
		private System.Windows.Forms.TabPage tcLog;
		private System.Windows.Forms.TextBox textBoxFiles;
		private System.Windows.Forms.Panel pProgress;
		private System.Windows.Forms.Panel panelData;
		private System.Windows.Forms.CheckBox chBoxViewProgress;
		private System.Windows.Forms.ListView lvFilesCount;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.StatusStrip ssProgress;
		private System.Windows.Forms.ToolStripStatusLabel tsslblProgress;
		private System.Windows.Forms.ToolStripProgressBar tsProgressBar;
		private System.Windows.Forms.FolderBrowserDialog fbdScanDir;
		private System.Windows.Forms.ContextMenuStrip cmsItems;
		private System.Windows.Forms.ToolStripSeparator tsmi3;
		private System.Windows.Forms.ToolStripMenuItem tsmiCheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiUnCheckedAll;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem tsmiFilesCheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiFilesUnCheckedAll;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem tsmiDirCheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiDirUnCheckedAll;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem tsmiFB2CheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiFB2UnCheckedAll;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem tsmiZipCheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiZipUnCheckedAll;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
		private System.Windows.Forms.ToolStripMenuItem tsmiCheckedAllSelected;
		private System.Windows.Forms.ToolStripMenuItem tsmiUnCheckedAllSelected;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
		private System.Windows.Forms.ToolStripMenuItem tsmiColumnsExplorerAutoReize;
		private System.Windows.Forms.ImageList imageListItems;
	}
}
