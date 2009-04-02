/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 16.03.2009
 * Time: 9:55
 * 
 * License: GPL 2.1
 */
namespace SharpFBTools.Controls.Panels
{
	partial class SFBTpArchiveManager
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SFBTpArchiveManager));
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
									"Всего папок",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
									"Всего файлов",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
									"fb2-файлов",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
									"Всего папок",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
									"Всего файлов",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
									"Распаковано архивов",
									"0"}, -1);
			System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Архивы", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
									"Rar",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem(new string[] {
									"Zip",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem(new string[] {
									"7z",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem(new string[] {
									"BZip2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem(new string[] {
									"GZip",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem(new string[] {
									"Tar",
									"0"}, -1);
			this.tsArchiver = new System.Windows.Forms.ToolStrip();
			this.tsbtnOpenDir = new System.Windows.Forms.ToolStripButton();
			this.tsSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnArchive = new System.Windows.Forms.ToolStripButton();
			this.ssProgress = new System.Windows.Forms.StatusStrip();
			this.tsslblProgress = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.pScanDir = new System.Windows.Forms.Panel();
			this.tboxSourceDir = new System.Windows.Forms.TextBox();
			this.lblDir = new System.Windows.Forms.Label();
			this.pCentral = new System.Windows.Forms.Panel();
			this.tcArchiver = new System.Windows.Forms.TabControl();
			this.tpArchive = new System.Windows.Forms.TabPage();
			this.pOptions = new System.Windows.Forms.Panel();
			this.gboxCount = new System.Windows.Forms.GroupBox();
			this.lvGeneralCount = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.gboxOptions = new System.Windows.Forms.GroupBox();
			this.cboxAddRestoreInfo = new System.Windows.Forms.CheckBox();
			this.cboxDelFB2Files = new System.Windows.Forms.CheckBox();
			this.pToAnotherDir = new System.Windows.Forms.Panel();
			this.btnToAnotherDir = new System.Windows.Forms.Button();
			this.tboxToAnotherDir = new System.Windows.Forms.TextBox();
			this.rbtnToAnotherDir = new System.Windows.Forms.RadioButton();
			this.rbtnToSomeDir = new System.Windows.Forms.RadioButton();
			this.pType = new System.Windows.Forms.Panel();
			this.cboxExistArchive = new System.Windows.Forms.ComboBox();
			this.lblExistArchive = new System.Windows.Forms.Label();
			this.lblArchiveType = new System.Windows.Forms.Label();
			this.cboxArchiveType = new System.Windows.Forms.ComboBox();
			this.tpUnArchive = new System.Windows.Forms.TabPage();
			this.pUAOptions = new System.Windows.Forms.Panel();
			this.gboxUAOptions = new System.Windows.Forms.GroupBox();
			this.gboxUACount = new System.Windows.Forms.GroupBox();
			this.lvUAGeneralCount = new System.Windows.Forms.ListView();
			this.cHeaderDirsFiles = new System.Windows.Forms.ColumnHeader();
			this.cHeaderCount = new System.Windows.Forms.ColumnHeader();
			this.lvUACount = new System.Windows.Forms.ListView();
			this.cHeaderArchive = new System.Windows.Forms.ColumnHeader();
			this.cHeaderArchiveCount = new System.Windows.Forms.ColumnHeader();
			this.cboxUADelFB2Files = new System.Windows.Forms.CheckBox();
			this.pUAToAnotherDir = new System.Windows.Forms.Panel();
			this.btnUAToAnotherDir = new System.Windows.Forms.Button();
			this.tboxUAToAnotherDir = new System.Windows.Forms.TextBox();
			this.rbtnUAToAnotherDir = new System.Windows.Forms.RadioButton();
			this.rbtnUAToSomeDir = new System.Windows.Forms.RadioButton();
			this.pUAType = new System.Windows.Forms.Panel();
			this.cboxUAExistArchive = new System.Windows.Forms.ComboBox();
			this.lblUAExistArchive = new System.Windows.Forms.Label();
			this.lblUAType = new System.Windows.Forms.Label();
			this.cboxUAType = new System.Windows.Forms.ComboBox();
			this.pUAScanDir = new System.Windows.Forms.Panel();
			this.tboxUASourceDir = new System.Windows.Forms.TextBox();
			this.lblUAScanDir = new System.Windows.Forms.Label();
			this.tsUnArchiver = new System.Windows.Forms.ToolStrip();
			this.tsbtnUAOpenDir = new System.Windows.Forms.ToolStripButton();
			this.tsSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnUAAnalyze = new System.Windows.Forms.ToolStripButton();
			this.tsSep3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnUnArchive = new System.Windows.Forms.ToolStripButton();
			this.tpTest = new System.Windows.Forms.TabPage();
			this.gboxRar = new System.Windows.Forms.GroupBox();
			this.lblRarDir = new System.Windows.Forms.Label();
			this.tboxRarDir = new System.Windows.Forms.TextBox();
			this.btnRarDir = new System.Windows.Forms.Button();
			this.fbdDir = new System.Windows.Forms.FolderBrowserDialog();
			this.pRar = new System.Windows.Forms.Panel();
			this.tsArchiver.SuspendLayout();
			this.ssProgress.SuspendLayout();
			this.pScanDir.SuspendLayout();
			this.pCentral.SuspendLayout();
			this.tcArchiver.SuspendLayout();
			this.tpArchive.SuspendLayout();
			this.pOptions.SuspendLayout();
			this.gboxCount.SuspendLayout();
			this.gboxOptions.SuspendLayout();
			this.pToAnotherDir.SuspendLayout();
			this.pType.SuspendLayout();
			this.tpUnArchive.SuspendLayout();
			this.pUAOptions.SuspendLayout();
			this.gboxUAOptions.SuspendLayout();
			this.gboxUACount.SuspendLayout();
			this.pUAToAnotherDir.SuspendLayout();
			this.pUAType.SuspendLayout();
			this.pUAScanDir.SuspendLayout();
			this.tsUnArchiver.SuspendLayout();
			this.gboxRar.SuspendLayout();
			this.pRar.SuspendLayout();
			this.SuspendLayout();
			// 
			// tsArchiver
			// 
			this.tsArchiver.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tsArchiver.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsbtnOpenDir,
									this.tsSep1,
									this.tsbtnArchive});
			this.tsArchiver.Location = new System.Drawing.Point(3, 3);
			this.tsArchiver.Name = "tsArchiver";
			this.tsArchiver.Size = new System.Drawing.Size(754, 31);
			this.tsArchiver.TabIndex = 2;
			// 
			// tsbtnOpenDir
			// 
			this.tsbtnOpenDir.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnOpenDir.Image")));
			this.tsbtnOpenDir.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnOpenDir.Name = "tsbtnOpenDir";
			this.tsbtnOpenDir.Size = new System.Drawing.Size(114, 28);
			this.tsbtnOpenDir.Text = "Открыть папку";
			this.tsbtnOpenDir.ToolTipText = "Открыть папку с fb2-файлами...";
			this.tsbtnOpenDir.Click += new System.EventHandler(this.TsbtnOpenDirClick);
			// 
			// tsSep1
			// 
			this.tsSep1.Name = "tsSep1";
			this.tsSep1.Size = new System.Drawing.Size(6, 31);
			// 
			// tsbtnArchive
			// 
			this.tsbtnArchive.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnArchive.Image")));
			this.tsbtnArchive.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnArchive.Name = "tsbtnArchive";
			this.tsbtnArchive.Size = new System.Drawing.Size(95, 28);
			this.tsbtnArchive.Text = "Запаковать";
			this.tsbtnArchive.ToolTipText = "Запаковать fb2-файлы";
			this.tsbtnArchive.Click += new System.EventHandler(this.TsbtnArchiveClick);
			// 
			// ssProgress
			// 
			this.ssProgress.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsslblProgress,
									this.tsProgressBar});
			this.ssProgress.Location = new System.Drawing.Point(0, 527);
			this.ssProgress.Name = "ssProgress";
			this.ssProgress.Size = new System.Drawing.Size(768, 22);
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
			// pScanDir
			// 
			this.pScanDir.AutoSize = true;
			this.pScanDir.Controls.Add(this.tboxSourceDir);
			this.pScanDir.Controls.Add(this.lblDir);
			this.pScanDir.Dock = System.Windows.Forms.DockStyle.Top;
			this.pScanDir.Location = new System.Drawing.Point(3, 34);
			this.pScanDir.Margin = new System.Windows.Forms.Padding(0);
			this.pScanDir.Name = "pScanDir";
			this.pScanDir.Size = new System.Drawing.Size(754, 28);
			this.pScanDir.TabIndex = 22;
			// 
			// tboxSourceDir
			// 
			this.tboxSourceDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxSourceDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tboxSourceDir.Location = new System.Drawing.Point(162, 5);
			this.tboxSourceDir.Name = "tboxSourceDir";
			this.tboxSourceDir.ReadOnly = true;
			this.tboxSourceDir.Size = new System.Drawing.Size(589, 20);
			this.tboxSourceDir.TabIndex = 4;
			// 
			// lblDir
			// 
			this.lblDir.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.lblDir.Location = new System.Drawing.Point(2, 8);
			this.lblDir.Name = "lblDir";
			this.lblDir.Size = new System.Drawing.Size(162, 19);
			this.lblDir.TabIndex = 3;
			this.lblDir.Text = "Папка для сканирования:";
			// 
			// pCentral
			// 
			this.pCentral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.pCentral.Controls.Add(this.tcArchiver);
			this.pCentral.Location = new System.Drawing.Point(0, 0);
			this.pCentral.Name = "pCentral";
			this.pCentral.Size = new System.Drawing.Size(768, 466);
			this.pCentral.TabIndex = 23;
			// 
			// tcArchiver
			// 
			this.tcArchiver.Controls.Add(this.tpArchive);
			this.tcArchiver.Controls.Add(this.tpUnArchive);
			this.tcArchiver.Controls.Add(this.tpTest);
			this.tcArchiver.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcArchiver.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.tcArchiver.Location = new System.Drawing.Point(0, 0);
			this.tcArchiver.Name = "tcArchiver";
			this.tcArchiver.SelectedIndex = 0;
			this.tcArchiver.Size = new System.Drawing.Size(768, 466);
			this.tcArchiver.TabIndex = 0;
			// 
			// tpArchive
			// 
			this.tpArchive.Controls.Add(this.pOptions);
			this.tpArchive.Controls.Add(this.pType);
			this.tpArchive.Controls.Add(this.pScanDir);
			this.tpArchive.Controls.Add(this.tsArchiver);
			this.tpArchive.Location = new System.Drawing.Point(4, 22);
			this.tpArchive.Name = "tpArchive";
			this.tpArchive.Padding = new System.Windows.Forms.Padding(3);
			this.tpArchive.Size = new System.Drawing.Size(760, 440);
			this.tpArchive.TabIndex = 0;
			this.tpArchive.Text = " Запаковать ";
			this.tpArchive.UseVisualStyleBackColor = true;
			// 
			// pOptions
			// 
			this.pOptions.Controls.Add(this.gboxCount);
			this.pOptions.Controls.Add(this.gboxOptions);
			this.pOptions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pOptions.Location = new System.Drawing.Point(3, 92);
			this.pOptions.Name = "pOptions";
			this.pOptions.Size = new System.Drawing.Size(754, 345);
			this.pOptions.TabIndex = 26;
			// 
			// gboxCount
			// 
			this.gboxCount.Controls.Add(this.lvGeneralCount);
			this.gboxCount.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gboxCount.Location = new System.Drawing.Point(0, 157);
			this.gboxCount.Name = "gboxCount";
			this.gboxCount.Size = new System.Drawing.Size(754, 188);
			this.gboxCount.TabIndex = 3;
			this.gboxCount.TabStop = false;
			this.gboxCount.Text = " Ход работы ";
			// 
			// lvGeneralCount
			// 
			this.lvGeneralCount.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeader1,
									this.columnHeader2});
			this.lvGeneralCount.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvGeneralCount.GridLines = true;
			this.lvGeneralCount.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
									listViewItem1,
									listViewItem2,
									listViewItem3});
			this.lvGeneralCount.Location = new System.Drawing.Point(3, 16);
			this.lvGeneralCount.Name = "lvGeneralCount";
			this.lvGeneralCount.Size = new System.Drawing.Size(748, 169);
			this.lvGeneralCount.TabIndex = 2;
			this.lvGeneralCount.UseCompatibleStateImageBehavior = false;
			this.lvGeneralCount.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Папки и файлы";
			this.columnHeader1.Width = 200;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Количество";
			this.columnHeader2.Width = 80;
			// 
			// gboxOptions
			// 
			this.gboxOptions.Controls.Add(this.cboxAddRestoreInfo);
			this.gboxOptions.Controls.Add(this.cboxDelFB2Files);
			this.gboxOptions.Controls.Add(this.pToAnotherDir);
			this.gboxOptions.Controls.Add(this.rbtnToAnotherDir);
			this.gboxOptions.Controls.Add(this.rbtnToSomeDir);
			this.gboxOptions.Dock = System.Windows.Forms.DockStyle.Top;
			this.gboxOptions.Location = new System.Drawing.Point(0, 0);
			this.gboxOptions.Name = "gboxOptions";
			this.gboxOptions.Size = new System.Drawing.Size(754, 157);
			this.gboxOptions.TabIndex = 1;
			this.gboxOptions.TabStop = false;
			this.gboxOptions.Text = " Настройки ";
			// 
			// cboxAddRestoreInfo
			// 
			this.cboxAddRestoreInfo.Dock = System.Windows.Forms.DockStyle.Top;
			this.cboxAddRestoreInfo.Location = new System.Drawing.Point(3, 121);
			this.cboxAddRestoreInfo.Name = "cboxAddRestoreInfo";
			this.cboxAddRestoreInfo.Size = new System.Drawing.Size(748, 24);
			this.cboxAddRestoreInfo.TabIndex = 4;
			this.cboxAddRestoreInfo.Text = " Добавить в архив информацию для его восстановления";
			this.cboxAddRestoreInfo.UseVisualStyleBackColor = true;
			this.cboxAddRestoreInfo.Visible = false;
			// 
			// cboxDelFB2Files
			// 
			this.cboxDelFB2Files.Dock = System.Windows.Forms.DockStyle.Top;
			this.cboxDelFB2Files.Location = new System.Drawing.Point(3, 97);
			this.cboxDelFB2Files.Name = "cboxDelFB2Files";
			this.cboxDelFB2Files.Size = new System.Drawing.Size(748, 24);
			this.cboxDelFB2Files.TabIndex = 3;
			this.cboxDelFB2Files.Text = " Удалить fb2-файлы после упаковки";
			this.cboxDelFB2Files.UseVisualStyleBackColor = true;
			// 
			// pToAnotherDir
			// 
			this.pToAnotherDir.Controls.Add(this.btnToAnotherDir);
			this.pToAnotherDir.Controls.Add(this.tboxToAnotherDir);
			this.pToAnotherDir.Dock = System.Windows.Forms.DockStyle.Top;
			this.pToAnotherDir.Location = new System.Drawing.Point(3, 64);
			this.pToAnotherDir.Name = "pToAnotherDir";
			this.pToAnotherDir.Size = new System.Drawing.Size(748, 33);
			this.pToAnotherDir.TabIndex = 2;
			// 
			// btnToAnotherDir
			// 
			this.btnToAnotherDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnToAnotherDir.Enabled = false;
			this.btnToAnotherDir.Image = ((System.Drawing.Image)(resources.GetObject("btnToAnotherDir.Image")));
			this.btnToAnotherDir.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnToAnotherDir.Location = new System.Drawing.Point(700, 3);
			this.btnToAnotherDir.Name = "btnToAnotherDir";
			this.btnToAnotherDir.Size = new System.Drawing.Size(37, 24);
			this.btnToAnotherDir.TabIndex = 7;
			this.btnToAnotherDir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnToAnotherDir.UseVisualStyleBackColor = true;
			this.btnToAnotherDir.Click += new System.EventHandler(this.BtnToAnotherDirClick);
			// 
			// tboxToAnotherDir
			// 
			this.tboxToAnotherDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxToAnotherDir.Location = new System.Drawing.Point(20, 5);
			this.tboxToAnotherDir.Name = "tboxToAnotherDir";
			this.tboxToAnotherDir.ReadOnly = true;
			this.tboxToAnotherDir.Size = new System.Drawing.Size(674, 20);
			this.tboxToAnotherDir.TabIndex = 6;
			// 
			// rbtnToAnotherDir
			// 
			this.rbtnToAnotherDir.Dock = System.Windows.Forms.DockStyle.Top;
			this.rbtnToAnotherDir.Location = new System.Drawing.Point(3, 40);
			this.rbtnToAnotherDir.Name = "rbtnToAnotherDir";
			this.rbtnToAnotherDir.Size = new System.Drawing.Size(748, 24);
			this.rbtnToAnotherDir.TabIndex = 1;
			this.rbtnToAnotherDir.TabStop = true;
			this.rbtnToAnotherDir.Text = " Поместить архив в другую папку:";
			this.rbtnToAnotherDir.UseVisualStyleBackColor = true;
			this.rbtnToAnotherDir.CheckedChanged += new System.EventHandler(this.RbtnToAnotherDirCheckedChanged);
			// 
			// rbtnToSomeDir
			// 
			this.rbtnToSomeDir.Checked = true;
			this.rbtnToSomeDir.Dock = System.Windows.Forms.DockStyle.Top;
			this.rbtnToSomeDir.Location = new System.Drawing.Point(3, 16);
			this.rbtnToSomeDir.Name = "rbtnToSomeDir";
			this.rbtnToSomeDir.Size = new System.Drawing.Size(748, 24);
			this.rbtnToSomeDir.TabIndex = 0;
			this.rbtnToSomeDir.TabStop = true;
			this.rbtnToSomeDir.Text = " Поместить архив в ту же папку, где находится исходный fb2-файл";
			this.rbtnToSomeDir.UseVisualStyleBackColor = true;
			// 
			// pType
			// 
			this.pType.Controls.Add(this.cboxExistArchive);
			this.pType.Controls.Add(this.lblExistArchive);
			this.pType.Controls.Add(this.lblArchiveType);
			this.pType.Controls.Add(this.cboxArchiveType);
			this.pType.Dock = System.Windows.Forms.DockStyle.Top;
			this.pType.Location = new System.Drawing.Point(3, 62);
			this.pType.Name = "pType";
			this.pType.Size = new System.Drawing.Size(754, 30);
			this.pType.TabIndex = 25;
			// 
			// cboxExistArchive
			// 
			this.cboxExistArchive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxExistArchive.FormattingEnabled = true;
			this.cboxExistArchive.Items.AddRange(new object[] {
									"Заменить существующий fb2-архив создаваемым",
									"Добавить к создаваемому fb2-архиву дату и время"});
			this.cboxExistArchive.Location = new System.Drawing.Point(353, 3);
			this.cboxExistArchive.Name = "cboxExistArchive";
			this.cboxExistArchive.Size = new System.Drawing.Size(387, 21);
			this.cboxExistArchive.TabIndex = 16;
			// 
			// lblExistArchive
			// 
			this.lblExistArchive.AutoSize = true;
			this.lblExistArchive.Location = new System.Drawing.Point(195, 7);
			this.lblExistArchive.Name = "lblExistArchive";
			this.lblExistArchive.Size = new System.Drawing.Size(155, 13);
			this.lblExistArchive.TabIndex = 15;
			this.lblExistArchive.Text = "Одинаковые fb2-архивы:";
			// 
			// lblArchiveType
			// 
			this.lblArchiveType.AutoSize = true;
			this.lblArchiveType.Location = new System.Drawing.Point(3, 6);
			this.lblArchiveType.Name = "lblArchiveType";
			this.lblArchiveType.Size = new System.Drawing.Size(90, 13);
			this.lblArchiveType.TabIndex = 14;
			this.lblArchiveType.Text = "Вид упаковки:";
			// 
			// cboxArchiveType
			// 
			this.cboxArchiveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxArchiveType.FormattingEnabled = true;
			this.cboxArchiveType.Items.AddRange(new object[] {
									"Rar",
									"Zip",
									"7z",
									"BZip2",
									"GZip",
									"Tar"});
			this.cboxArchiveType.Location = new System.Drawing.Point(99, 3);
			this.cboxArchiveType.Name = "cboxArchiveType";
			this.cboxArchiveType.Size = new System.Drawing.Size(83, 21);
			this.cboxArchiveType.TabIndex = 13;
			this.cboxArchiveType.SelectedIndexChanged += new System.EventHandler(this.CboxArchiveTypeSelectedIndexChanged);
			// 
			// tpUnArchive
			// 
			this.tpUnArchive.Controls.Add(this.pUAOptions);
			this.tpUnArchive.Controls.Add(this.pUAType);
			this.tpUnArchive.Controls.Add(this.pUAScanDir);
			this.tpUnArchive.Controls.Add(this.tsUnArchiver);
			this.tpUnArchive.Location = new System.Drawing.Point(4, 22);
			this.tpUnArchive.Name = "tpUnArchive";
			this.tpUnArchive.Padding = new System.Windows.Forms.Padding(3);
			this.tpUnArchive.Size = new System.Drawing.Size(760, 440);
			this.tpUnArchive.TabIndex = 1;
			this.tpUnArchive.Text = " Распаковать ";
			this.tpUnArchive.UseVisualStyleBackColor = true;
			// 
			// pUAOptions
			// 
			this.pUAOptions.Controls.Add(this.gboxUAOptions);
			this.pUAOptions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pUAOptions.Location = new System.Drawing.Point(3, 92);
			this.pUAOptions.Name = "pUAOptions";
			this.pUAOptions.Size = new System.Drawing.Size(754, 345);
			this.pUAOptions.TabIndex = 28;
			// 
			// gboxUAOptions
			// 
			this.gboxUAOptions.Controls.Add(this.gboxUACount);
			this.gboxUAOptions.Controls.Add(this.cboxUADelFB2Files);
			this.gboxUAOptions.Controls.Add(this.pUAToAnotherDir);
			this.gboxUAOptions.Controls.Add(this.rbtnUAToAnotherDir);
			this.gboxUAOptions.Controls.Add(this.rbtnUAToSomeDir);
			this.gboxUAOptions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gboxUAOptions.Location = new System.Drawing.Point(0, 0);
			this.gboxUAOptions.Name = "gboxUAOptions";
			this.gboxUAOptions.Size = new System.Drawing.Size(754, 345);
			this.gboxUAOptions.TabIndex = 1;
			this.gboxUAOptions.TabStop = false;
			this.gboxUAOptions.Text = " Настройки ";
			// 
			// gboxUACount
			// 
			this.gboxUACount.Controls.Add(this.lvUAGeneralCount);
			this.gboxUACount.Controls.Add(this.lvUACount);
			this.gboxUACount.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gboxUACount.Location = new System.Drawing.Point(3, 121);
			this.gboxUACount.Name = "gboxUACount";
			this.gboxUACount.Size = new System.Drawing.Size(748, 221);
			this.gboxUACount.TabIndex = 4;
			this.gboxUACount.TabStop = false;
			this.gboxUACount.Text = " Ход работы ";
			// 
			// lvUAGeneralCount
			// 
			this.lvUAGeneralCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left)));
			this.lvUAGeneralCount.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.cHeaderDirsFiles,
									this.cHeaderCount});
			this.lvUAGeneralCount.GridLines = true;
			this.lvUAGeneralCount.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
									listViewItem4,
									listViewItem5,
									listViewItem6});
			this.lvUAGeneralCount.Location = new System.Drawing.Point(0, 16);
			this.lvUAGeneralCount.Name = "lvUAGeneralCount";
			this.lvUAGeneralCount.Size = new System.Drawing.Size(392, 202);
			this.lvUAGeneralCount.TabIndex = 1;
			this.lvUAGeneralCount.UseCompatibleStateImageBehavior = false;
			this.lvUAGeneralCount.View = System.Windows.Forms.View.Details;
			// 
			// cHeaderDirsFiles
			// 
			this.cHeaderDirsFiles.Text = "Папки и файлы";
			this.cHeaderDirsFiles.Width = 200;
			// 
			// cHeaderCount
			// 
			this.cHeaderCount.Text = "Количество";
			this.cHeaderCount.Width = 80;
			// 
			// lvUACount
			// 
			this.lvUACount.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lvUACount.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.cHeaderArchive,
									this.cHeaderArchiveCount});
			this.lvUACount.GridLines = true;
			listViewGroup1.Header = "Архивы";
			listViewGroup1.Name = "lvgArchive";
			this.lvUACount.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
									listViewGroup1});
			listViewItem7.Group = listViewGroup1;
			listViewItem7.StateImageIndex = 0;
			listViewItem8.Group = listViewGroup1;
			listViewItem9.Group = listViewGroup1;
			listViewItem10.Group = listViewGroup1;
			listViewItem11.Group = listViewGroup1;
			listViewItem12.Group = listViewGroup1;
			this.lvUACount.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
									listViewItem7,
									listViewItem8,
									listViewItem9,
									listViewItem10,
									listViewItem11,
									listViewItem12});
			this.lvUACount.Location = new System.Drawing.Point(398, 16);
			this.lvUACount.Name = "lvUACount";
			this.lvUACount.Size = new System.Drawing.Size(347, 202);
			this.lvUACount.TabIndex = 0;
			this.lvUACount.UseCompatibleStateImageBehavior = false;
			this.lvUACount.View = System.Windows.Forms.View.Details;
			// 
			// cHeaderArchive
			// 
			this.cHeaderArchive.Text = "Тип архива";
			this.cHeaderArchive.Width = 200;
			// 
			// cHeaderArchiveCount
			// 
			this.cHeaderArchiveCount.Text = "Количество";
			this.cHeaderArchiveCount.Width = 80;
			// 
			// cboxUADelFB2Files
			// 
			this.cboxUADelFB2Files.Dock = System.Windows.Forms.DockStyle.Top;
			this.cboxUADelFB2Files.Location = new System.Drawing.Point(3, 97);
			this.cboxUADelFB2Files.Name = "cboxUADelFB2Files";
			this.cboxUADelFB2Files.Size = new System.Drawing.Size(748, 24);
			this.cboxUADelFB2Files.TabIndex = 3;
			this.cboxUADelFB2Files.Text = " Удалить архивы после распаковки";
			this.cboxUADelFB2Files.UseVisualStyleBackColor = true;
			// 
			// pUAToAnotherDir
			// 
			this.pUAToAnotherDir.Controls.Add(this.btnUAToAnotherDir);
			this.pUAToAnotherDir.Controls.Add(this.tboxUAToAnotherDir);
			this.pUAToAnotherDir.Dock = System.Windows.Forms.DockStyle.Top;
			this.pUAToAnotherDir.Location = new System.Drawing.Point(3, 64);
			this.pUAToAnotherDir.Name = "pUAToAnotherDir";
			this.pUAToAnotherDir.Size = new System.Drawing.Size(748, 33);
			this.pUAToAnotherDir.TabIndex = 2;
			// 
			// btnUAToAnotherDir
			// 
			this.btnUAToAnotherDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnUAToAnotherDir.Enabled = false;
			this.btnUAToAnotherDir.Image = ((System.Drawing.Image)(resources.GetObject("btnUAToAnotherDir.Image")));
			this.btnUAToAnotherDir.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnUAToAnotherDir.Location = new System.Drawing.Point(700, 3);
			this.btnUAToAnotherDir.Name = "btnUAToAnotherDir";
			this.btnUAToAnotherDir.Size = new System.Drawing.Size(37, 24);
			this.btnUAToAnotherDir.TabIndex = 7;
			this.btnUAToAnotherDir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnUAToAnotherDir.UseVisualStyleBackColor = true;
			this.btnUAToAnotherDir.Click += new System.EventHandler(this.BtnUAToAnotherDirClick);
			// 
			// tboxUAToAnotherDir
			// 
			this.tboxUAToAnotherDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxUAToAnotherDir.Location = new System.Drawing.Point(20, 5);
			this.tboxUAToAnotherDir.Name = "tboxUAToAnotherDir";
			this.tboxUAToAnotherDir.ReadOnly = true;
			this.tboxUAToAnotherDir.Size = new System.Drawing.Size(674, 20);
			this.tboxUAToAnotherDir.TabIndex = 6;
			// 
			// rbtnUAToAnotherDir
			// 
			this.rbtnUAToAnotherDir.Dock = System.Windows.Forms.DockStyle.Top;
			this.rbtnUAToAnotherDir.Location = new System.Drawing.Point(3, 40);
			this.rbtnUAToAnotherDir.Name = "rbtnUAToAnotherDir";
			this.rbtnUAToAnotherDir.Size = new System.Drawing.Size(748, 24);
			this.rbtnUAToAnotherDir.TabIndex = 1;
			this.rbtnUAToAnotherDir.TabStop = true;
			this.rbtnUAToAnotherDir.Text = " Поместить fb2-файл в другую папку:";
			this.rbtnUAToAnotherDir.UseVisualStyleBackColor = true;
			this.rbtnUAToAnotherDir.CheckedChanged += new System.EventHandler(this.RbtnUAToAnotherDirCheckedChanged);
			// 
			// rbtnUAToSomeDir
			// 
			this.rbtnUAToSomeDir.Checked = true;
			this.rbtnUAToSomeDir.Dock = System.Windows.Forms.DockStyle.Top;
			this.rbtnUAToSomeDir.Location = new System.Drawing.Point(3, 16);
			this.rbtnUAToSomeDir.Name = "rbtnUAToSomeDir";
			this.rbtnUAToSomeDir.Size = new System.Drawing.Size(748, 24);
			this.rbtnUAToSomeDir.TabIndex = 0;
			this.rbtnUAToSomeDir.TabStop = true;
			this.rbtnUAToSomeDir.Text = " Поместить fb2-файл в ту же папку, где находится исходный архив";
			this.rbtnUAToSomeDir.UseVisualStyleBackColor = true;
			// 
			// pUAType
			// 
			this.pUAType.Controls.Add(this.cboxUAExistArchive);
			this.pUAType.Controls.Add(this.lblUAExistArchive);
			this.pUAType.Controls.Add(this.lblUAType);
			this.pUAType.Controls.Add(this.cboxUAType);
			this.pUAType.Dock = System.Windows.Forms.DockStyle.Top;
			this.pUAType.Location = new System.Drawing.Point(3, 62);
			this.pUAType.Name = "pUAType";
			this.pUAType.Size = new System.Drawing.Size(754, 30);
			this.pUAType.TabIndex = 26;
			// 
			// cboxUAExistArchive
			// 
			this.cboxUAExistArchive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxUAExistArchive.FormattingEnabled = true;
			this.cboxUAExistArchive.Items.AddRange(new object[] {
									"Заменить существующий fb2-файл создаваемым",
									"Добавить к создаваемому fb2-файлу дату и время"});
			this.cboxUAExistArchive.Location = new System.Drawing.Point(371, 3);
			this.cboxUAExistArchive.Name = "cboxUAExistArchive";
			this.cboxUAExistArchive.Size = new System.Drawing.Size(369, 21);
			this.cboxUAExistArchive.TabIndex = 16;
			// 
			// lblUAExistArchive
			// 
			this.lblUAExistArchive.AutoSize = true;
			this.lblUAExistArchive.Location = new System.Drawing.Point(215, 7);
			this.lblUAExistArchive.Name = "lblUAExistArchive";
			this.lblUAExistArchive.Size = new System.Drawing.Size(155, 13);
			this.lblUAExistArchive.TabIndex = 15;
			this.lblUAExistArchive.Text = "Одинаковые fb2-архивы:";
			// 
			// lblUAType
			// 
			this.lblUAType.AutoSize = true;
			this.lblUAType.Location = new System.Drawing.Point(3, 6);
			this.lblUAType.Name = "lblUAType";
			this.lblUAType.Size = new System.Drawing.Size(103, 13);
			this.lblUAType.TabIndex = 14;
			this.lblUAType.Text = "Вид распаковки:";
			// 
			// cboxUAType
			// 
			this.cboxUAType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxUAType.FormattingEnabled = true;
			this.cboxUAType.Items.AddRange(new object[] {
									"Rar",
									"Zip",
									"7z",
									"BZip2",
									"GZip",
									"Tar",
									"Все архивы"});
			this.cboxUAType.Location = new System.Drawing.Point(107, 3);
			this.cboxUAType.Name = "cboxUAType";
			this.cboxUAType.Size = new System.Drawing.Size(102, 21);
			this.cboxUAType.TabIndex = 13;
			// 
			// pUAScanDir
			// 
			this.pUAScanDir.AutoSize = true;
			this.pUAScanDir.Controls.Add(this.tboxUASourceDir);
			this.pUAScanDir.Controls.Add(this.lblUAScanDir);
			this.pUAScanDir.Dock = System.Windows.Forms.DockStyle.Top;
			this.pUAScanDir.Location = new System.Drawing.Point(3, 34);
			this.pUAScanDir.Margin = new System.Windows.Forms.Padding(0);
			this.pUAScanDir.Name = "pUAScanDir";
			this.pUAScanDir.Size = new System.Drawing.Size(754, 28);
			this.pUAScanDir.TabIndex = 23;
			// 
			// tboxUASourceDir
			// 
			this.tboxUASourceDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxUASourceDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tboxUASourceDir.Location = new System.Drawing.Point(162, 5);
			this.tboxUASourceDir.Name = "tboxUASourceDir";
			this.tboxUASourceDir.ReadOnly = true;
			this.tboxUASourceDir.Size = new System.Drawing.Size(589, 20);
			this.tboxUASourceDir.TabIndex = 4;
			// 
			// lblUAScanDir
			// 
			this.lblUAScanDir.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.lblUAScanDir.Location = new System.Drawing.Point(2, 8);
			this.lblUAScanDir.Name = "lblUAScanDir";
			this.lblUAScanDir.Size = new System.Drawing.Size(162, 19);
			this.lblUAScanDir.TabIndex = 3;
			this.lblUAScanDir.Text = "Папка для сканирования:";
			// 
			// tsUnArchiver
			// 
			this.tsUnArchiver.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tsUnArchiver.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsbtnUAOpenDir,
									this.tsSep2,
									this.tsbtnUAAnalyze,
									this.tsSep3,
									this.tsbtnUnArchive});
			this.tsUnArchiver.Location = new System.Drawing.Point(3, 3);
			this.tsUnArchiver.Name = "tsUnArchiver";
			this.tsUnArchiver.Size = new System.Drawing.Size(754, 31);
			this.tsUnArchiver.TabIndex = 3;
			// 
			// tsbtnUAOpenDir
			// 
			this.tsbtnUAOpenDir.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnUAOpenDir.Image")));
			this.tsbtnUAOpenDir.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnUAOpenDir.Name = "tsbtnUAOpenDir";
			this.tsbtnUAOpenDir.Size = new System.Drawing.Size(114, 28);
			this.tsbtnUAOpenDir.Text = "Открыть папку";
			this.tsbtnUAOpenDir.ToolTipText = "Открыть папку с fb2-файлами...";
			this.tsbtnUAOpenDir.Click += new System.EventHandler(this.TsbtnUAOpenDirClick);
			// 
			// tsSep2
			// 
			this.tsSep2.Name = "tsSep2";
			this.tsSep2.Size = new System.Drawing.Size(6, 31);
			// 
			// tsbtnUAAnalyze
			// 
			this.tsbtnUAAnalyze.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnUAAnalyze.Image")));
			this.tsbtnUAAnalyze.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnUAAnalyze.Name = "tsbtnUAAnalyze";
			this.tsbtnUAAnalyze.Size = new System.Drawing.Size(112, 28);
			this.tsbtnUAAnalyze.Text = "Анализ файлов";
			this.tsbtnUAAnalyze.Click += new System.EventHandler(this.TsbtnUAAnalyzeClick);
			// 
			// tsSep3
			// 
			this.tsSep3.Name = "tsSep3";
			this.tsSep3.Size = new System.Drawing.Size(6, 31);
			// 
			// tsbtnUnArchive
			// 
			this.tsbtnUnArchive.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnUnArchive.Image")));
			this.tsbtnUnArchive.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnUnArchive.Name = "tsbtnUnArchive";
			this.tsbtnUnArchive.Size = new System.Drawing.Size(100, 28);
			this.tsbtnUnArchive.Text = "Распаковать";
			this.tsbtnUnArchive.ToolTipText = "Рапаковать архивы";
			this.tsbtnUnArchive.Click += new System.EventHandler(this.TsbtnUnArchiveClick);
			// 
			// tpTest
			// 
			this.tpTest.Location = new System.Drawing.Point(4, 22);
			this.tpTest.Name = "tpTest";
			this.tpTest.Size = new System.Drawing.Size(760, 440);
			this.tpTest.TabIndex = 2;
			this.tpTest.Text = " Тестировать архивы ";
			this.tpTest.UseVisualStyleBackColor = true;
			// 
			// gboxRar
			// 
			this.gboxRar.Controls.Add(this.lblRarDir);
			this.gboxRar.Controls.Add(this.tboxRarDir);
			this.gboxRar.Controls.Add(this.btnRarDir);
			this.gboxRar.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gboxRar.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gboxRar.Location = new System.Drawing.Point(0, 0);
			this.gboxRar.Name = "gboxRar";
			this.gboxRar.Size = new System.Drawing.Size(768, 61);
			this.gboxRar.TabIndex = 12;
			this.gboxRar.TabStop = false;
			this.gboxRar.Text = " Настройки для Rar-архиватора ";
			// 
			// lblRarDir
			// 
			this.lblRarDir.AutoSize = true;
			this.lblRarDir.Location = new System.Drawing.Point(4, 25);
			this.lblRarDir.Name = "lblRarDir";
			this.lblRarDir.Size = new System.Drawing.Size(175, 13);
			this.lblRarDir.TabIndex = 10;
			this.lblRarDir.Text = "Папка с установленным Rar:";
			// 
			// tboxRarDir
			// 
			this.tboxRarDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxRarDir.Location = new System.Drawing.Point(185, 21);
			this.tboxRarDir.Name = "tboxRarDir";
			this.tboxRarDir.ReadOnly = true;
			this.tboxRarDir.Size = new System.Drawing.Size(529, 20);
			this.tboxRarDir.TabIndex = 8;
			// 
			// btnRarDir
			// 
			this.btnRarDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRarDir.Image = ((System.Drawing.Image)(resources.GetObject("btnRarDir.Image")));
			this.btnRarDir.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnRarDir.Location = new System.Drawing.Point(720, 19);
			this.btnRarDir.Name = "btnRarDir";
			this.btnRarDir.Size = new System.Drawing.Size(37, 24);
			this.btnRarDir.TabIndex = 9;
			this.btnRarDir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnRarDir.UseVisualStyleBackColor = true;
			this.btnRarDir.Click += new System.EventHandler(this.BtnRarDirClick);
			// 
			// pRar
			// 
			this.pRar.Controls.Add(this.gboxRar);
			this.pRar.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pRar.Location = new System.Drawing.Point(0, 466);
			this.pRar.Name = "pRar";
			this.pRar.Size = new System.Drawing.Size(768, 61);
			this.pRar.TabIndex = 13;
			// 
			// SFBTpArchiveManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pRar);
			this.Controls.Add(this.pCentral);
			this.Controls.Add(this.ssProgress);
			this.Name = "SFBTpArchiveManager";
			this.Size = new System.Drawing.Size(768, 549);
			this.tsArchiver.ResumeLayout(false);
			this.tsArchiver.PerformLayout();
			this.ssProgress.ResumeLayout(false);
			this.ssProgress.PerformLayout();
			this.pScanDir.ResumeLayout(false);
			this.pScanDir.PerformLayout();
			this.pCentral.ResumeLayout(false);
			this.tcArchiver.ResumeLayout(false);
			this.tpArchive.ResumeLayout(false);
			this.tpArchive.PerformLayout();
			this.pOptions.ResumeLayout(false);
			this.gboxCount.ResumeLayout(false);
			this.gboxOptions.ResumeLayout(false);
			this.pToAnotherDir.ResumeLayout(false);
			this.pToAnotherDir.PerformLayout();
			this.pType.ResumeLayout(false);
			this.pType.PerformLayout();
			this.tpUnArchive.ResumeLayout(false);
			this.tpUnArchive.PerformLayout();
			this.pUAOptions.ResumeLayout(false);
			this.gboxUAOptions.ResumeLayout(false);
			this.gboxUACount.ResumeLayout(false);
			this.pUAToAnotherDir.ResumeLayout(false);
			this.pUAToAnotherDir.PerformLayout();
			this.pUAType.ResumeLayout(false);
			this.pUAType.PerformLayout();
			this.pUAScanDir.ResumeLayout(false);
			this.pUAScanDir.PerformLayout();
			this.tsUnArchiver.ResumeLayout(false);
			this.tsUnArchiver.PerformLayout();
			this.gboxRar.ResumeLayout(false);
			this.gboxRar.PerformLayout();
			this.pRar.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.GroupBox gboxCount;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ListView lvGeneralCount;
		private System.Windows.Forms.CheckBox cboxAddRestoreInfo;
		private System.Windows.Forms.ColumnHeader cHeaderCount;
		private System.Windows.Forms.ColumnHeader cHeaderDirsFiles;
		private System.Windows.Forms.ListView lvUAGeneralCount;
		private System.Windows.Forms.ToolStripSeparator tsSep3;
		private System.Windows.Forms.ToolStripSeparator tsSep2;
		private System.Windows.Forms.ToolStripButton tsbtnUAAnalyze;
		private System.Windows.Forms.ColumnHeader cHeaderArchiveCount;
		private System.Windows.Forms.ColumnHeader cHeaderArchive;
		private System.Windows.Forms.ListView lvUACount;
		private System.Windows.Forms.GroupBox gboxUACount;
		private System.Windows.Forms.Panel pRar;
		private System.Windows.Forms.RadioButton rbtnUAToSomeDir;
		private System.Windows.Forms.RadioButton rbtnUAToAnotherDir;
		private System.Windows.Forms.TextBox tboxUAToAnotherDir;
		private System.Windows.Forms.Button btnUAToAnotherDir;
		private System.Windows.Forms.Panel pUAToAnotherDir;
		private System.Windows.Forms.CheckBox cboxUADelFB2Files;
		private System.Windows.Forms.GroupBox gboxUAOptions;
		private System.Windows.Forms.Panel pUAOptions;
		private System.Windows.Forms.ComboBox cboxUAType;
		private System.Windows.Forms.Label lblUAType;
		private System.Windows.Forms.Label lblUAExistArchive;
		private System.Windows.Forms.ComboBox cboxUAExistArchive;
		private System.Windows.Forms.Panel pUAType;
		private System.Windows.Forms.Label lblUAScanDir;
		private System.Windows.Forms.TextBox tboxUASourceDir;
		private System.Windows.Forms.Panel pUAScanDir;
		private System.Windows.Forms.ToolStripButton tsbtnUAOpenDir;
		private System.Windows.Forms.Label lblArchiveType;
		private System.Windows.Forms.ToolStripButton tsbtnUnArchive;
		private System.Windows.Forms.ToolStrip tsUnArchiver;
		private System.Windows.Forms.ToolStrip tsArchiver;
		private System.Windows.Forms.ComboBox cboxExistArchive;
		private System.Windows.Forms.Label lblExistArchive;
		private System.Windows.Forms.ComboBox cboxArchiveType;
		private System.Windows.Forms.Label lblRarDir;
		private System.Windows.Forms.GroupBox gboxRar;
		private System.Windows.Forms.FolderBrowserDialog fbdDir;
		private System.Windows.Forms.TextBox tboxRarDir;
		private System.Windows.Forms.Button btnRarDir;
		private System.Windows.Forms.ToolStripButton tsbtnArchive;
		private System.Windows.Forms.CheckBox cboxDelFB2Files;
		private System.Windows.Forms.TextBox tboxToAnotherDir;
		private System.Windows.Forms.Button btnToAnotherDir;
		private System.Windows.Forms.Panel pToAnotherDir;
		private System.Windows.Forms.RadioButton rbtnToSomeDir;
		private System.Windows.Forms.RadioButton rbtnToAnotherDir;
		private System.Windows.Forms.Panel pOptions;
		private System.Windows.Forms.GroupBox gboxOptions;
		private System.Windows.Forms.Panel pType;
		private System.Windows.Forms.TabPage tpTest;
		private System.Windows.Forms.TabPage tpUnArchive;
		private System.Windows.Forms.TabPage tpArchive;
		private System.Windows.Forms.TabControl tcArchiver;
		private System.Windows.Forms.Panel pCentral;
		private System.Windows.Forms.Label lblDir;
		private System.Windows.Forms.TextBox tboxSourceDir;
		private System.Windows.Forms.Panel pScanDir;
		private System.Windows.Forms.ToolStripProgressBar tsProgressBar;
		private System.Windows.Forms.ToolStripStatusLabel tsslblProgress;
		private System.Windows.Forms.StatusStrip ssProgress;
		private System.Windows.Forms.ToolStripSeparator tsSep1;
		private System.Windows.Forms.ToolStripButton tsbtnOpenDir;
	}
}
