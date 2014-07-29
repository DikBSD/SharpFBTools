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
		#region Designer
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
			this.fbdScanDir = new System.Windows.Forms.FolderBrowserDialog();
			this.ssProgress = new System.Windows.Forms.StatusStrip();
			this.tsslblProgress = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
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
			this.tcDesc.SuspendLayout();
			this.tpFullSort.SuspendLayout();
			this.cmsItems.SuspendLayout();
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
			// fbdScanDir
			// 
			this.fbdScanDir.Description = "Укажите папку для сканирования с fb2-файлами:";
			// 
			// ssProgress
			// 
			this.ssProgress.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsslblProgress,
									this.tsProgressBar});
			this.ssProgress.Location = new System.Drawing.Point(0, 536);
			this.ssProgress.Name = "ssProgress";
			this.ssProgress.Size = new System.Drawing.Size(1072, 22);
			this.ssProgress.TabIndex = 40;
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
			this.tsProgressBar.Size = new System.Drawing.Size(700, 16);
			// 
			// tcDesc
			// 
			this.tcDesc.Controls.Add(this.tpFullSort);
			this.tcDesc.Controls.Add(this.tpDescSelected);
			this.tcDesc.Controls.Add(this.tcLog);
			this.tcDesc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcDesc.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.tcDesc.Location = new System.Drawing.Point(0, 0);
			this.tcDesc.Name = "tcDesc";
			this.tcDesc.SelectedIndex = 0;
			this.tcDesc.Size = new System.Drawing.Size(1072, 536);
			this.tcDesc.TabIndex = 41;
			// 
			// tpFullSort
			// 
			this.tpFullSort.Controls.Add(this.listViewSource);
			this.tpFullSort.Controls.Add(this.panelExplorerAddress);
			this.tpFullSort.Font = new System.Drawing.Font("Tahoma", 8F);
			this.tpFullSort.Location = new System.Drawing.Point(4, 22);
			this.tpFullSort.Name = "tpFullSort";
			this.tpFullSort.Padding = new System.Windows.Forms.Padding(3);
			this.tpFullSort.Size = new System.Drawing.Size(1064, 510);
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
			this.listViewSource.Location = new System.Drawing.Point(3, 58);
			this.listViewSource.Name = "listViewSource";
			this.listViewSource.ShowItemToolTips = true;
			this.listViewSource.Size = new System.Drawing.Size(1058, 449);
			this.listViewSource.SmallImageList = this.imageListItems;
			this.listViewSource.TabIndex = 35;
			this.listViewSource.UseCompatibleStateImageBehavior = false;
			this.listViewSource.View = System.Windows.Forms.View.Details;
			this.listViewSource.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.ListViewSourceItemChecked);
			this.listViewSource.DoubleClick += new System.EventHandler(this.ListViewSourceDoubleClick);
			this.listViewSource.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ListViewSourceItemCheck);
			this.listViewSource.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ListViewSourceKeyPress);
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
									this.tsmiUnCheckedAllSelected,
									this.toolStripMenuItem6,
									this.tsmiColumnsExplorerAutoReize});
			this.cmsItems.Name = "cmsValidator";
			this.cmsItems.Size = new System.Drawing.Size(308, 332);
			// 
			// tsmi3
			// 
			this.tsmi3.Name = "tsmi3";
			this.tsmi3.Size = new System.Drawing.Size(304, 6);
			// 
			// tsmiCheckedAll
			// 
			this.tsmiCheckedAll.Image = ((System.Drawing.Image)(resources.GetObject("tsmiCheckedAll.Image")));
			this.tsmiCheckedAll.Name = "tsmiCheckedAll";
			this.tsmiCheckedAll.Size = new System.Drawing.Size(307, 22);
			this.tsmiCheckedAll.Text = "Пометить все файлы и папки";
			this.tsmiCheckedAll.Click += new System.EventHandler(this.TsmiCheckedAllClick);
			// 
			// tsmiUnCheckedAll
			// 
			this.tsmiUnCheckedAll.Image = ((System.Drawing.Image)(resources.GetObject("tsmiUnCheckedAll.Image")));
			this.tsmiUnCheckedAll.Name = "tsmiUnCheckedAll";
			this.tsmiUnCheckedAll.Size = new System.Drawing.Size(307, 22);
			this.tsmiUnCheckedAll.Text = "Снять пометки со всех файлов и папок";
			this.tsmiUnCheckedAll.Click += new System.EventHandler(this.TsmiUnCheckedAllClick);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(304, 6);
			// 
			// tsmiFilesCheckedAll
			// 
			this.tsmiFilesCheckedAll.Name = "tsmiFilesCheckedAll";
			this.tsmiFilesCheckedAll.Size = new System.Drawing.Size(307, 22);
			this.tsmiFilesCheckedAll.Text = "Пометить все файлы";
			this.tsmiFilesCheckedAll.Click += new System.EventHandler(this.TsmiFilesCheckedAllClick);
			// 
			// tsmiFilesUnCheckedAll
			// 
			this.tsmiFilesUnCheckedAll.Name = "tsmiFilesUnCheckedAll";
			this.tsmiFilesUnCheckedAll.Size = new System.Drawing.Size(307, 22);
			this.tsmiFilesUnCheckedAll.Text = "Снять пометки со всех файлов";
			this.tsmiFilesUnCheckedAll.Click += new System.EventHandler(this.TsmiFilesUnCheckedAllClick);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(304, 6);
			// 
			// tsmiDirCheckedAll
			// 
			this.tsmiDirCheckedAll.Name = "tsmiDirCheckedAll";
			this.tsmiDirCheckedAll.Size = new System.Drawing.Size(307, 22);
			this.tsmiDirCheckedAll.Text = "Пометить все папки";
			this.tsmiDirCheckedAll.Click += new System.EventHandler(this.TsmiDirCheckedAllClick);
			// 
			// tsmiDirUnCheckedAll
			// 
			this.tsmiDirUnCheckedAll.Name = "tsmiDirUnCheckedAll";
			this.tsmiDirUnCheckedAll.Size = new System.Drawing.Size(307, 22);
			this.tsmiDirUnCheckedAll.Text = "Снять пометки со всех папок";
			this.tsmiDirUnCheckedAll.Click += new System.EventHandler(this.TsmiDirUnCheckedAllClick);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(304, 6);
			// 
			// tsmiFB2CheckedAll
			// 
			this.tsmiFB2CheckedAll.Name = "tsmiFB2CheckedAll";
			this.tsmiFB2CheckedAll.Size = new System.Drawing.Size(307, 22);
			this.tsmiFB2CheckedAll.Text = "Пометить все fb2 файлы";
			this.tsmiFB2CheckedAll.Click += new System.EventHandler(this.TsmiFB2CheckedAllClick);
			// 
			// tsmiFB2UnCheckedAll
			// 
			this.tsmiFB2UnCheckedAll.Name = "tsmiFB2UnCheckedAll";
			this.tsmiFB2UnCheckedAll.Size = new System.Drawing.Size(307, 22);
			this.tsmiFB2UnCheckedAll.Text = "Снять пометки со всех fb2 файлов";
			this.tsmiFB2UnCheckedAll.Click += new System.EventHandler(this.TsmiFB2UnCheckedAllClick);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(304, 6);
			// 
			// tsmiZipCheckedAll
			// 
			this.tsmiZipCheckedAll.Name = "tsmiZipCheckedAll";
			this.tsmiZipCheckedAll.Size = new System.Drawing.Size(307, 22);
			this.tsmiZipCheckedAll.Text = "Пометить все zip файлы";
			this.tsmiZipCheckedAll.Click += new System.EventHandler(this.TsmiZipCheckedAllClick);
			// 
			// tsmiZipUnCheckedAll
			// 
			this.tsmiZipUnCheckedAll.Name = "tsmiZipUnCheckedAll";
			this.tsmiZipUnCheckedAll.Size = new System.Drawing.Size(307, 22);
			this.tsmiZipUnCheckedAll.Text = "Снять пометки со всех zip файлов";
			this.tsmiZipUnCheckedAll.Click += new System.EventHandler(this.TsmiZipUnCheckedAllClick);
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(304, 6);
			// 
			// tsmiCheckedAllSelected
			// 
			this.tsmiCheckedAllSelected.Name = "tsmiCheckedAllSelected";
			this.tsmiCheckedAllSelected.Size = new System.Drawing.Size(307, 22);
			this.tsmiCheckedAllSelected.Text = "Пометить всё выделенное";
			this.tsmiCheckedAllSelected.Click += new System.EventHandler(this.TsmiCheckedAllSelectedClick);
			// 
			// tsmiUnCheckedAllSelected
			// 
			this.tsmiUnCheckedAllSelected.Name = "tsmiUnCheckedAllSelected";
			this.tsmiUnCheckedAllSelected.Size = new System.Drawing.Size(307, 22);
			this.tsmiUnCheckedAllSelected.Text = "Снять пометки со всего выделенного";
			this.tsmiUnCheckedAllSelected.Click += new System.EventHandler(this.TsmiUnCheckedAllSelectedClick);
			// 
			// toolStripMenuItem6
			// 
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			this.toolStripMenuItem6.Size = new System.Drawing.Size(304, 6);
			// 
			// tsmiColumnsExplorerAutoReize
			// 
			this.tsmiColumnsExplorerAutoReize.Name = "tsmiColumnsExplorerAutoReize";
			this.tsmiColumnsExplorerAutoReize.Size = new System.Drawing.Size(307, 22);
			this.tsmiColumnsExplorerAutoReize.Text = "Обновить авторазмер колонок Проводника";
			this.tsmiColumnsExplorerAutoReize.Click += new System.EventHandler(this.TsmiColumnsExplorerAutoReizeClick);
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
			// panelExplorerAddress
			// 
			this.panelExplorerAddress.Controls.Add(this.panelAddress);
			this.panelExplorerAddress.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelExplorerAddress.Location = new System.Drawing.Point(3, 3);
			this.panelExplorerAddress.Name = "panelExplorerAddress";
			this.panelExplorerAddress.Size = new System.Drawing.Size(1058, 55);
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
			this.panelAddress.Name = "panelAddress";
			this.panelAddress.Size = new System.Drawing.Size(1058, 55);
			this.panelAddress.TabIndex = 38;
			// 
			// chBoxStartExplorerColumnsAutoReize
			// 
			this.chBoxStartExplorerColumnsAutoReize.Checked = true;
			this.chBoxStartExplorerColumnsAutoReize.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chBoxStartExplorerColumnsAutoReize.Font = new System.Drawing.Font("Tahoma", 8F);
			this.chBoxStartExplorerColumnsAutoReize.Location = new System.Drawing.Point(96, 28);
			this.chBoxStartExplorerColumnsAutoReize.Name = "chBoxStartExplorerColumnsAutoReize";
			this.chBoxStartExplorerColumnsAutoReize.Size = new System.Drawing.Size(215, 24);
			this.chBoxStartExplorerColumnsAutoReize.TabIndex = 8;
			this.chBoxStartExplorerColumnsAutoReize.Text = "Авторазмер колонок Проводника";
			this.chBoxStartExplorerColumnsAutoReize.UseVisualStyleBackColor = true;
			this.chBoxStartExplorerColumnsAutoReize.CheckedChanged += new System.EventHandler(this.ChBoxStartExplorerColumnsAutoReizeCheckedChanged);
			// 
			// checkBoxTagsView
			// 
			this.checkBoxTagsView.Font = new System.Drawing.Font("Tahoma", 8F);
			this.checkBoxTagsView.Location = new System.Drawing.Point(339, 28);
			this.checkBoxTagsView.Name = "checkBoxTagsView";
			this.checkBoxTagsView.Size = new System.Drawing.Size(268, 24);
			this.checkBoxTagsView.TabIndex = 5;
			this.checkBoxTagsView.Text = "Показывать описание книг в Проводнике";
			this.checkBoxTagsView.UseVisualStyleBackColor = true;
			this.checkBoxTagsView.Click += new System.EventHandler(this.CheckBoxTagsViewClick);
			// 
			// buttonGo
			// 
			this.buttonGo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.buttonGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonGo.Location = new System.Drawing.Point(909, 4);
			this.buttonGo.Name = "buttonGo";
			this.buttonGo.Size = new System.Drawing.Size(142, 24);
			this.buttonGo.TabIndex = 6;
			this.buttonGo.Text = "Перейти/Обновить";
			this.buttonGo.UseVisualStyleBackColor = true;
			this.buttonGo.Click += new System.EventHandler(this.ButtonGoClick);
			// 
			// textBoxAddress
			// 
			this.textBoxAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxAddress.Location = new System.Drawing.Point(95, 6);
			this.textBoxAddress.Name = "textBoxAddress";
			this.textBoxAddress.Size = new System.Drawing.Size(808, 20);
			this.textBoxAddress.TabIndex = 5;
			this.textBoxAddress.TextChanged += new System.EventHandler(this.TextBoxAddressTextChanged);
			this.textBoxAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxAddressKeyPress);
			// 
			// labelAddress
			// 
			this.labelAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelAddress.Location = new System.Drawing.Point(44, 7);
			this.labelAddress.Name = "labelAddress";
			this.labelAddress.Size = new System.Drawing.Size(47, 19);
			this.labelAddress.TabIndex = 4;
			this.labelAddress.Text = "Адрес:";
			this.labelAddress.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// buttonOpenSourceDir
			// 
			this.buttonOpenSourceDir.Image = ((System.Drawing.Image)(resources.GetObject("buttonOpenSourceDir.Image")));
			this.buttonOpenSourceDir.Location = new System.Drawing.Point(3, 3);
			this.buttonOpenSourceDir.Name = "buttonOpenSourceDir";
			this.buttonOpenSourceDir.Size = new System.Drawing.Size(31, 27);
			this.buttonOpenSourceDir.TabIndex = 7;
			this.buttonOpenSourceDir.UseVisualStyleBackColor = true;
			// 
			// tpDescSelected
			// 
			this.tpDescSelected.Controls.Add(this.panel1);
			this.tpDescSelected.Controls.Add(this.pSearchFBDup2Dirs);
			this.tpDescSelected.Controls.Add(this.paneBuildListProcess);
			this.tpDescSelected.Font = new System.Drawing.Font("Tahoma", 8F);
			this.tpDescSelected.Location = new System.Drawing.Point(4, 22);
			this.tpDescSelected.Name = "tpDescSelected";
			this.tpDescSelected.Padding = new System.Windows.Forms.Padding(3);
			this.tpDescSelected.Size = new System.Drawing.Size(1064, 510);
			this.tpDescSelected.TabIndex = 1;
			this.tpDescSelected.Text = " Поиск по критерию ";
			this.tpDescSelected.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.groupBoxSearchMode);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(3, 65);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1058, 82);
			this.panel1.TabIndex = 65;
			// 
			// groupBoxSearchMode
			// 
			this.groupBoxSearchMode.Controls.Add(this.rbSearchModeAllBooks);
			this.groupBoxSearchMode.Controls.Add(this.rbSearchModeFromLetter);
			this.groupBoxSearchMode.Location = new System.Drawing.Point(3, 3);
			this.groupBoxSearchMode.Name = "groupBoxSearchMode";
			this.groupBoxSearchMode.Size = new System.Drawing.Size(162, 77);
			this.groupBoxSearchMode.TabIndex = 10;
			this.groupBoxSearchMode.TabStop = false;
			this.groupBoxSearchMode.Text = "Режим поиска книг";
			// 
			// rbSearchModeAllBooks
			// 
			this.rbSearchModeAllBooks.Location = new System.Drawing.Point(6, 14);
			this.rbSearchModeAllBooks.Name = "rbSearchModeAllBooks";
			this.rbSearchModeAllBooks.Size = new System.Drawing.Size(86, 24);
			this.rbSearchModeAllBooks.TabIndex = 8;
			this.rbSearchModeAllBooks.TabStop = true;
			this.rbSearchModeAllBooks.Text = "Все книги";
			this.rbSearchModeAllBooks.UseVisualStyleBackColor = true;
			// 
			// rbSearchModeFromLetter
			// 
			this.rbSearchModeFromLetter.Location = new System.Drawing.Point(6, 38);
			this.rbSearchModeFromLetter.Name = "rbSearchModeFromLetter";
			this.rbSearchModeFromLetter.Size = new System.Drawing.Size(125, 24);
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
			this.pSearchFBDup2Dirs.Location = new System.Drawing.Point(3, 3);
			this.pSearchFBDup2Dirs.Name = "pSearchFBDup2Dirs";
			this.pSearchFBDup2Dirs.Size = new System.Drawing.Size(1058, 62);
			this.pSearchFBDup2Dirs.TabIndex = 64;
			// 
			// buttonTargetDir
			// 
			this.buttonTargetDir.Image = ((System.Drawing.Image)(resources.GetObject("buttonTargetDir.Image")));
			this.buttonTargetDir.Location = new System.Drawing.Point(166, 31);
			this.buttonTargetDir.Name = "buttonTargetDir";
			this.buttonTargetDir.Size = new System.Drawing.Size(31, 27);
			this.buttonTargetDir.TabIndex = 22;
			this.buttonTargetDir.UseVisualStyleBackColor = true;
			// 
			// buttonSourceDir
			// 
			this.buttonSourceDir.Image = ((System.Drawing.Image)(resources.GetObject("buttonSourceDir.Image")));
			this.buttonSourceDir.Location = new System.Drawing.Point(166, 3);
			this.buttonSourceDir.Name = "buttonSourceDir";
			this.buttonSourceDir.Size = new System.Drawing.Size(31, 27);
			this.buttonSourceDir.TabIndex = 21;
			this.buttonSourceDir.UseVisualStyleBackColor = true;
			// 
			// lblDupToDir
			// 
			this.lblDupToDir.AutoSize = true;
			this.lblDupToDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblDupToDir.Location = new System.Drawing.Point(3, 37);
			this.lblDupToDir.Name = "lblDupToDir";
			this.lblDupToDir.Size = new System.Drawing.Size(152, 13);
			this.lblDupToDir.TabIndex = 20;
			this.lblDupToDir.Text = "Папка-приемник файлов:";
			// 
			// tboxTargetDir
			// 
			this.tboxTargetDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxTargetDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.tboxTargetDir.Location = new System.Drawing.Point(203, 35);
			this.tboxTargetDir.Name = "tboxTargetDir";
			this.tboxTargetDir.Size = new System.Drawing.Size(651, 20);
			this.tboxTargetDir.TabIndex = 19;
			// 
			// chBoxScanSubDir
			// 
			this.chBoxScanSubDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chBoxScanSubDir.Checked = true;
			this.chBoxScanSubDir.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chBoxScanSubDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxScanSubDir.ForeColor = System.Drawing.Color.Navy;
			this.chBoxScanSubDir.Location = new System.Drawing.Point(860, 4);
			this.chBoxScanSubDir.Name = "chBoxScanSubDir";
			this.chBoxScanSubDir.Size = new System.Drawing.Size(195, 24);
			this.chBoxScanSubDir.TabIndex = 2;
			this.chBoxScanSubDir.Text = "Обрабатывать подкаталоги";
			this.chBoxScanSubDir.UseVisualStyleBackColor = true;
			// 
			// tboxSourceDir
			// 
			this.tboxSourceDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxSourceDir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tboxSourceDir.Location = new System.Drawing.Point(203, 6);
			this.tboxSourceDir.Name = "tboxSourceDir";
			this.tboxSourceDir.Size = new System.Drawing.Size(651, 21);
			this.tboxSourceDir.TabIndex = 1;
			// 
			// lblScanDir
			// 
			this.lblScanDir.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.lblScanDir.Location = new System.Drawing.Point(3, 8);
			this.lblScanDir.Name = "lblScanDir";
			this.lblScanDir.Size = new System.Drawing.Size(162, 19);
			this.lblScanDir.TabIndex = 6;
			this.lblScanDir.Text = "Папка для сканирования:";
			// 
			// paneBuildListProcess
			// 
			this.paneBuildListProcess.BackColor = System.Drawing.SystemColors.Control;
			this.paneBuildListProcess.Controls.Add(this.buttonBuildListStop);
			this.paneBuildListProcess.Controls.Add(this.buttonBuildList);
			this.paneBuildListProcess.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.paneBuildListProcess.Location = new System.Drawing.Point(3, 449);
			this.paneBuildListProcess.Name = "paneBuildListProcess";
			this.paneBuildListProcess.Size = new System.Drawing.Size(1058, 58);
			this.paneBuildListProcess.TabIndex = 63;
			// 
			// buttonBuildListStop
			// 
			this.buttonBuildListStop.Enabled = false;
			this.buttonBuildListStop.Font = new System.Drawing.Font("Tahoma", 11F);
			this.buttonBuildListStop.Image = ((System.Drawing.Image)(resources.GetObject("buttonBuildListStop.Image")));
			this.buttonBuildListStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonBuildListStop.Location = new System.Drawing.Point(3, 6);
			this.buttonBuildListStop.Name = "buttonBuildListStop";
			this.buttonBuildListStop.Size = new System.Drawing.Size(179, 49);
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
			this.buttonBuildList.Location = new System.Drawing.Point(860, 6);
			this.buttonBuildList.Name = "buttonBuildList";
			this.buttonBuildList.Size = new System.Drawing.Size(191, 49);
			this.buttonBuildList.TabIndex = 2;
			this.buttonBuildList.Text = "Построить список  ";
			this.buttonBuildList.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.buttonBuildList.UseVisualStyleBackColor = true;
			// 
			// tcLog
			// 
			this.tcLog.Controls.Add(this.textBoxFiles);
			this.tcLog.Controls.Add(this.pProgress);
			this.tcLog.Location = new System.Drawing.Point(4, 22);
			this.tcLog.Name = "tcLog";
			this.tcLog.Padding = new System.Windows.Forms.Padding(3);
			this.tcLog.Size = new System.Drawing.Size(1064, 510);
			this.tcLog.TabIndex = 2;
			this.tcLog.Text = "Статистика";
			this.tcLog.UseVisualStyleBackColor = true;
			// 
			// textBoxFiles
			// 
			this.textBoxFiles.BackColor = System.Drawing.SystemColors.Window;
			this.textBoxFiles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.textBoxFiles.Font = new System.Drawing.Font("Tahoma", 8F);
			this.textBoxFiles.Location = new System.Drawing.Point(3, 3);
			this.textBoxFiles.Multiline = true;
			this.textBoxFiles.Name = "textBoxFiles";
			this.textBoxFiles.ReadOnly = true;
			this.textBoxFiles.ScrollBars = System.Windows.Forms.ScrollBars.Both;
			this.textBoxFiles.Size = new System.Drawing.Size(1058, 236);
			this.textBoxFiles.TabIndex = 35;
			// 
			// pProgress
			// 
			this.pProgress.Controls.Add(this.panelData);
			this.pProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pProgress.Location = new System.Drawing.Point(3, 239);
			this.pProgress.Name = "pProgress";
			this.pProgress.Size = new System.Drawing.Size(1058, 268);
			this.pProgress.TabIndex = 34;
			// 
			// panelData
			// 
			this.panelData.Controls.Add(this.chBoxViewProgress);
			this.panelData.Controls.Add(this.lvFilesCount);
			this.panelData.Location = new System.Drawing.Point(3, 3);
			this.panelData.Name = "panelData";
			this.panelData.Size = new System.Drawing.Size(357, 262);
			this.panelData.TabIndex = 0;
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
			this.chBoxViewProgress.Size = new System.Drawing.Size(357, 24);
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
			this.lvFilesCount.Location = new System.Drawing.Point(0, 24);
			this.lvFilesCount.Name = "lvFilesCount";
			this.lvFilesCount.Size = new System.Drawing.Size(357, 238);
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
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tcDesc);
			this.Controls.Add(this.ssProgress);
			this.Name = "SFBTpFB2DescEditor";
			this.Size = new System.Drawing.Size(1072, 558);
			this.ssProgress.ResumeLayout(false);
			this.ssProgress.PerformLayout();
			this.tcDesc.ResumeLayout(false);
			this.tpFullSort.ResumeLayout(false);
			this.cmsItems.ResumeLayout(false);
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
		private System.Windows.Forms.CheckBox chBoxStartExplorerColumnsAutoReize;
		private System.Windows.Forms.ImageList imageListItems;
		private System.Windows.Forms.ToolStripMenuItem tsmiColumnsExplorerAutoReize;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
		private System.Windows.Forms.ToolStripMenuItem tsmiUnCheckedAllSelected;
		private System.Windows.Forms.ToolStripMenuItem tsmiCheckedAllSelected;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
		private System.Windows.Forms.ToolStripMenuItem tsmiZipUnCheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiZipCheckedAll;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem tsmiFB2UnCheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiFB2CheckedAll;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem tsmiDirUnCheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiDirCheckedAll;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem tsmiFilesUnCheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiFilesCheckedAll;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem tsmiUnCheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiCheckedAll;
		private System.Windows.Forms.ToolStripSeparator tsmi3;
		private System.Windows.Forms.ContextMenuStrip cmsItems;
		private System.Windows.Forms.ToolStripProgressBar tsProgressBar;
		private System.Windows.Forms.ToolStripStatusLabel tsslblProgress;
		private System.Windows.Forms.StatusStrip ssProgress;
		private System.Windows.Forms.Button buttonBuildList;
		private System.Windows.Forms.Button buttonBuildListStop;
		private System.Windows.Forms.Panel paneBuildListProcess;
		private System.Windows.Forms.CheckBox checkBoxTagsView;
		private System.Windows.Forms.Panel panelExplorerAddress;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ListView lvFilesCount;
		private System.Windows.Forms.CheckBox chBoxViewProgress;
		private System.Windows.Forms.Panel panelData;
		private System.Windows.Forms.TextBox textBoxFiles;
		private System.Windows.Forms.Panel pProgress;
		private System.Windows.Forms.TabPage tcLog;
		private System.Windows.Forms.TabPage tpDescSelected;
		private System.Windows.Forms.Button buttonOpenSourceDir;
		private System.Windows.Forms.Label labelAddress;
		private System.Windows.Forms.TextBox textBoxAddress;
		private System.Windows.Forms.Button buttonGo;
		private System.Windows.Forms.Panel panelAddress;
		private System.Windows.Forms.ColumnHeader colHeaderEncoding;
		private System.Windows.Forms.ColumnHeader colHeaderLang;
		private System.Windows.Forms.ColumnHeader colHeaderGenre;
		private System.Windows.Forms.ColumnHeader colHeaderFIOBookAuthor;
		private System.Windows.Forms.ColumnHeader colHeaderSequence;
		private System.Windows.Forms.ColumnHeader colHeaderBookName;
		private System.Windows.Forms.ColumnHeader colHeaderFileName;
		private System.Windows.Forms.ListView listViewSource;
		private System.Windows.Forms.TabPage tpFullSort;
		private System.Windows.Forms.TabControl tcDesc;
		private System.Windows.Forms.RadioButton rbSearchModeFromLetter;
		private System.Windows.Forms.RadioButton rbSearchModeAllBooks;
		private System.Windows.Forms.GroupBox groupBoxSearchMode;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.FolderBrowserDialog fbdScanDir;
		private System.Windows.Forms.TextBox tboxTargetDir;
		private System.Windows.Forms.Button buttonSourceDir;
		private System.Windows.Forms.Button buttonTargetDir;
		private System.Windows.Forms.Label lblScanDir;
		private System.Windows.Forms.TextBox tboxSourceDir;
		private System.Windows.Forms.CheckBox chBoxScanSubDir;
		private System.Windows.Forms.Label lblDupToDir;
		private System.Windows.Forms.Panel pSearchFBDup2Dirs;
		#endregion
		
		#region Закрытые данные класса
		private string m_CurrentDir = "";
		//private StatusView	m_sv 			= null;
		//private BackgroundWorker m_bwcmd	= null;
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
			//TODO
			//        	Core.FileManager.FileManagerWork.GenerateSourceList(
			//        		dirPath, listViewSource, false, checkBoxTagsView.Checked, chBoxStartExplorerColumnsAutoReize.Checked
			//        	);
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
						m_mscLV.CheckAllListViewItems( listViewSource, true );
					} else {
						m_mscLV.UnCheckAllListViewItems( listViewSource.CheckedItems );
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
//			Settings.FileManagerSettings.StartExplorerColumnsAutoReize = chBoxStartExplorerColumnsAutoReize.Checked;
			if(chBoxStartExplorerColumnsAutoReize.Checked)
				m_mscLV.AutoResizeColumns(listViewSource);
		}
		
		void TextBoxAddressTextChanged(object sender, EventArgs e)
		{
			//TODO //Settings.FileManagerSettings.FullSortingSourceDir = textBoxAddress.Text;
		}
		
		void TsmiCheckedAllClick(object sender, EventArgs e)
		{
			// Пометить все файлы и папки
			ConnectListsEventHandlers( false );
			m_mscLV.CheckAllListViewItems( listViewSource, true );
			if(listViewSource.Items.Count > 0) {
				listViewSource.Items[0].Checked = false;
			}
			ConnectListsEventHandlers( true );
		}
		
		void TsmiUnCheckedAllClick(object sender, EventArgs e)
		{
			// Снять отметки со всех файлов и папок
			ConnectListsEventHandlers( false );
			m_mscLV.UnCheckAllListViewItems( listViewSource.CheckedItems );
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
			m_mscLV.AutoResizeColumns(listViewSource);
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
//			if(checkBoxTagsView.Checked) {
//				if(File.Exists(Settings.FileManagerSettings.FileManagerSettingsPath)) {
//					if(Settings.FileManagerSettings.ReadXmlFullSortingViewMessageForLongTime()) {
//						string sMess = "При включении этой опции для создания списка книг с их описанием может потребоваться очень много времени!\nБольше не показывать это сообщение?";
//						DialogResult result = MessageBox.Show( sMess, "Отображение описания книг", MessageBoxButtons.YesNo, MessageBoxIcon.Question );
//						Settings.FileManagerSettings.ViewMessageForLongTime = (result == DialogResult.Yes) ? false : true;
//					}
//				}
//			}
//
//			Settings.FileManagerSettings.BooksTagsView = checkBoxTagsView.Checked;
//			Settings.FileManagerSettings.WriteFileManagerSettings();
//			if( listViewSource.Items.Count > 0 ) {
//				Cursor.Current = Cursors.WaitCursor;
//				listViewSource.BeginUpdate();
//				DirectoryInfo di = null;
//				FB2BookDescription bd = null;
//				Settings.DataFM dfm = new Settings.DataFM();
//				for(int i=0; i!= listViewSource.Items.Count; ++i) {
//					ListViewItemType it = (ListViewItemType)listViewSource.Items[i].Tag;
//					if(it.Type=="f") {
//						di = new DirectoryInfo(it.Value);
//						if(checkBoxTagsView.Checked) {
//							// показать данные книг
//							try {
//								if(di.Extension.ToLower()==".fb2") {
//									// показать данные fb2 файлов
//									bd = new FB2BookDescription( it.Value );
//									listViewSource.Items[i].SubItems[1].Text = space+bd.TIBookTitle+space;
//									listViewSource.Items[i].SubItems[2].Text = space+bd.TISequences+space;
//									listViewSource.Items[i].SubItems[3].Text = space+bd.TIAuthors+space;
//									listViewSource.Items[i].SubItems[4].Text = space+Core.FileManager.FileManagerWork.CyrillicGenreName(bd.TIGenres)+space;
//									listViewSource.Items[i].SubItems[5].Text = space+bd.TILang+space;
//									listViewSource.Items[i].SubItems[6].Text = space+bd.Encoding+space;
//								} else if(di.Extension.ToLower()==".zip") {
//									if(checkBoxTagsView.Checked) {
//										// показать данные архивов
//										filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
//										archivesWorker.unzip(dfm.A7zaPath, it.Value, Settings.Settings.GetTempDir(), ProcessPriorityClass.AboveNormal );
//										string [] files = Directory.GetFiles( Settings.Settings.GetTempDir() );
//										bd = new FB2BookDescription( files[0] );
//										listViewSource.Items[i].SubItems[1].Text = space+bd.TIBookTitle+space;
//										listViewSource.Items[i].SubItems[2].Text = space+bd.TISequences+space;
//										listViewSource.Items[i].SubItems[3].Text = space+bd.TIAuthors+space;
//										listViewSource.Items[i].SubItems[4].Text = space+Core.FileManager.FileManagerWork.CyrillicGenreName(bd.TIGenres)+space;
//										listViewSource.Items[i].SubItems[5].Text = space+bd.TILang+space;
//										listViewSource.Items[i].SubItems[6].Text = space+bd.Encoding+space;
//									}
//								}
//							} catch(System.Exception) {
//								listViewSource.Items[i].ForeColor = Color.Blue;
//							}
//						} else {
//							// скрыть данные книг
//							for(int j=1; j!=listViewSource.Items[i].SubItems.Count; ++j) {
//								listViewSource.Items[i].SubItems[j].Text = "";
//							}
//						}
//					}
//				}
//				// авторазмер колонок Списка
//				if(chBoxStartExplorerColumnsAutoReize.Checked) {
//					m_mscLV.AutoResizeColumns(listViewSource);
//				}
//				listViewSource.EndUpdate();
//				Cursor.Current = Cursors.Default;
//			}
		}
	}
}
