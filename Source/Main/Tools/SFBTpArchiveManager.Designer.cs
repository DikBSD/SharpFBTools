/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 16.03.2009
 * Time: 9:55
 * 
 * License: GPL 2.1
 */
namespace SharpFBTools.Tools
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
			this.components = new System.ComponentModel.Container();
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
			System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
									"fb2-файлы из этих архивов",
									"0"}, -1);
			this.tsArchiver = new System.Windows.Forms.ToolStrip();
			this.tsbtnArchive = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnArchiveStop = new System.Windows.Forms.ToolStripButton();
			this.ssProgress = new System.Windows.Forms.StatusStrip();
			this.tsslblProgress = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.pScanDir = new System.Windows.Forms.Panel();
			this.btnOpenDir = new System.Windows.Forms.Button();
			this.cboxToSomeDir = new System.Windows.Forms.CheckBox();
			this.cboxScanSubDirToArchive = new System.Windows.Forms.CheckBox();
			this.tboxSourceDir = new System.Windows.Forms.TextBox();
			this.lblDir = new System.Windows.Forms.Label();
			this.pCentral = new System.Windows.Forms.Panel();
			this.tcArchiver = new System.Windows.Forms.TabControl();
			this.tpArchive = new System.Windows.Forms.TabPage();
			this.gboxCount = new System.Windows.Forms.GroupBox();
			this.lvGeneralCount = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.chBoxViewProgressA = new System.Windows.Forms.CheckBox();
			this.pOptions = new System.Windows.Forms.Panel();
			this.cboxDelFB2Files = new System.Windows.Forms.CheckBox();
			this.pType = new System.Windows.Forms.Panel();
			this.cboxExistArchive = new System.Windows.Forms.ComboBox();
			this.lblExistArchive = new System.Windows.Forms.Label();
			this.pToAnotherDir = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.tboxToAnotherDir = new System.Windows.Forms.TextBox();
			this.btnToAnotherDir = new System.Windows.Forms.Button();
			this.tpUnArchive = new System.Windows.Forms.TabPage();
			this.gboxUACount = new System.Windows.Forms.GroupBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.lvUAGeneralCount = new System.Windows.Forms.ListView();
			this.cHeaderDirsFiles = new System.Windows.Forms.ColumnHeader();
			this.cHeaderCount = new System.Windows.Forms.ColumnHeader();
			this.chBoxViewProgressU = new System.Windows.Forms.CheckBox();
			this.pUAOptions = new System.Windows.Forms.Panel();
			this.cboxUADelFB2Files = new System.Windows.Forms.CheckBox();
			this.pUAType = new System.Windows.Forms.Panel();
			this.cboxUAExistArchive = new System.Windows.Forms.ComboBox();
			this.lblUAExistArchive = new System.Windows.Forms.Label();
			this.pUAToAnotherDir = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.btnUAToAnotherDir = new System.Windows.Forms.Button();
			this.tboxUAToAnotherDir = new System.Windows.Forms.TextBox();
			this.pUAScanDir = new System.Windows.Forms.Panel();
			this.cboxUAToSomeDir = new System.Windows.Forms.CheckBox();
			this.btnUAOpenDir = new System.Windows.Forms.Button();
			this.cboxScanSubDirToUnArchive = new System.Windows.Forms.CheckBox();
			this.tboxUASourceDir = new System.Windows.Forms.TextBox();
			this.lblUAScanDir = new System.Windows.Forms.Label();
			this.tsUnArchiver = new System.Windows.Forms.ToolStrip();
			this.tsbtnUnArchive = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnUnArchiveStop = new System.Windows.Forms.ToolStripButton();
			this.imgl16 = new System.Windows.Forms.ImageList(this.components);
			this.fbdDir = new System.Windows.Forms.FolderBrowserDialog();
			this.tsArchiver.SuspendLayout();
			this.ssProgress.SuspendLayout();
			this.pScanDir.SuspendLayout();
			this.pCentral.SuspendLayout();
			this.tcArchiver.SuspendLayout();
			this.tpArchive.SuspendLayout();
			this.gboxCount.SuspendLayout();
			this.pOptions.SuspendLayout();
			this.pType.SuspendLayout();
			this.pToAnotherDir.SuspendLayout();
			this.tpUnArchive.SuspendLayout();
			this.gboxUACount.SuspendLayout();
			this.panel2.SuspendLayout();
			this.pUAOptions.SuspendLayout();
			this.pUAType.SuspendLayout();
			this.pUAToAnotherDir.SuspendLayout();
			this.pUAScanDir.SuspendLayout();
			this.tsUnArchiver.SuspendLayout();
			this.SuspendLayout();
			// 
			// tsArchiver
			// 
			this.tsArchiver.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tsArchiver.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsbtnArchive,
									this.toolStripSeparator1,
									this.tsbtnArchiveStop});
			this.tsArchiver.Location = new System.Drawing.Point(3, 3);
			this.tsArchiver.Name = "tsArchiver";
			this.tsArchiver.Size = new System.Drawing.Size(809, 31);
			this.tsArchiver.TabIndex = 2;
			// 
			// tsbtnArchive
			// 
			this.tsbtnArchive.AutoToolTip = false;
			this.tsbtnArchive.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnArchive.Image")));
			this.tsbtnArchive.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnArchive.Name = "tsbtnArchive";
			this.tsbtnArchive.Size = new System.Drawing.Size(90, 28);
			this.tsbtnArchive.Text = "Упаковать";
			this.tsbtnArchive.Click += new System.EventHandler(this.TsbtnArchiveClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			// 
			// tsbtnArchiveStop
			// 
			this.tsbtnArchiveStop.AutoToolTip = false;
			this.tsbtnArchiveStop.Enabled = false;
			this.tsbtnArchiveStop.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnArchiveStop.Image")));
			this.tsbtnArchiveStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnArchiveStop.Name = "tsbtnArchiveStop";
			this.tsbtnArchiveStop.Size = new System.Drawing.Size(96, 28);
			this.tsbtnArchiveStop.Text = "Остановить";
			this.tsbtnArchiveStop.Click += new System.EventHandler(this.TsbtnArchiveStopClick);
			// 
			// ssProgress
			// 
			this.ssProgress.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsslblProgress,
									this.tsProgressBar});
			this.ssProgress.Location = new System.Drawing.Point(0, 527);
			this.ssProgress.Name = "ssProgress";
			this.ssProgress.Size = new System.Drawing.Size(823, 22);
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
			this.tsProgressBar.Size = new System.Drawing.Size(700, 16);
			// 
			// pScanDir
			// 
			this.pScanDir.AutoSize = true;
			this.pScanDir.Controls.Add(this.btnOpenDir);
			this.pScanDir.Controls.Add(this.cboxToSomeDir);
			this.pScanDir.Controls.Add(this.cboxScanSubDirToArchive);
			this.pScanDir.Controls.Add(this.tboxSourceDir);
			this.pScanDir.Controls.Add(this.lblDir);
			this.pScanDir.Dock = System.Windows.Forms.DockStyle.Top;
			this.pScanDir.Location = new System.Drawing.Point(3, 34);
			this.pScanDir.Margin = new System.Windows.Forms.Padding(0);
			this.pScanDir.Name = "pScanDir";
			this.pScanDir.Size = new System.Drawing.Size(809, 76);
			this.pScanDir.TabIndex = 22;
			// 
			// btnOpenDir
			// 
			this.btnOpenDir.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenDir.Image")));
			this.btnOpenDir.Location = new System.Drawing.Point(165, 3);
			this.btnOpenDir.Name = "btnOpenDir";
			this.btnOpenDir.Size = new System.Drawing.Size(37, 27);
			this.btnOpenDir.TabIndex = 10;
			this.btnOpenDir.UseVisualStyleBackColor = true;
			this.btnOpenDir.Click += new System.EventHandler(this.BtnOpenDirClick);
			// 
			// cboxToSomeDir
			// 
			this.cboxToSomeDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.cboxToSomeDir.ForeColor = System.Drawing.Color.Navy;
			this.cboxToSomeDir.Location = new System.Drawing.Point(208, 49);
			this.cboxToSomeDir.Name = "cboxToSomeDir";
			this.cboxToSomeDir.Size = new System.Drawing.Size(597, 24);
			this.cboxToSomeDir.TabIndex = 9;
			this.cboxToSomeDir.Text = "Поместить zip-архив в ту же папку, где находится исходный fb2-файл";
			this.cboxToSomeDir.UseVisualStyleBackColor = true;
			this.cboxToSomeDir.CheckedChanged += new System.EventHandler(this.CboxToAnotherDirCheckedChanged);
			// 
			// cboxScanSubDirToArchive
			// 
			this.cboxScanSubDirToArchive.Checked = true;
			this.cboxScanSubDirToArchive.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cboxScanSubDirToArchive.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.cboxScanSubDirToArchive.ForeColor = System.Drawing.Color.Navy;
			this.cboxScanSubDirToArchive.Location = new System.Drawing.Point(208, 29);
			this.cboxScanSubDirToArchive.Name = "cboxScanSubDirToArchive";
			this.cboxScanSubDirToArchive.Size = new System.Drawing.Size(597, 24);
			this.cboxScanSubDirToArchive.TabIndex = 6;
			this.cboxScanSubDirToArchive.Text = "Сканировать и подпапки";
			this.cboxScanSubDirToArchive.UseVisualStyleBackColor = true;
			// 
			// tboxSourceDir
			// 
			this.tboxSourceDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxSourceDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tboxSourceDir.Location = new System.Drawing.Point(208, 5);
			this.tboxSourceDir.Name = "tboxSourceDir";
			this.tboxSourceDir.Size = new System.Drawing.Size(597, 20);
			this.tboxSourceDir.TabIndex = 4;
			this.tboxSourceDir.TextChanged += new System.EventHandler(this.TboxSourceDirTextChanged);
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
			this.pCentral.Controls.Add(this.tcArchiver);
			this.pCentral.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pCentral.Location = new System.Drawing.Point(0, 0);
			this.pCentral.Name = "pCentral";
			this.pCentral.Size = new System.Drawing.Size(823, 527);
			this.pCentral.TabIndex = 23;
			// 
			// tcArchiver
			// 
			this.tcArchiver.Controls.Add(this.tpArchive);
			this.tcArchiver.Controls.Add(this.tpUnArchive);
			this.tcArchiver.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcArchiver.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.tcArchiver.ImageList = this.imgl16;
			this.tcArchiver.Location = new System.Drawing.Point(0, 0);
			this.tcArchiver.Name = "tcArchiver";
			this.tcArchiver.SelectedIndex = 0;
			this.tcArchiver.Size = new System.Drawing.Size(823, 527);
			this.tcArchiver.TabIndex = 0;
			// 
			// tpArchive
			// 
			this.tpArchive.Controls.Add(this.gboxCount);
			this.tpArchive.Controls.Add(this.pOptions);
			this.tpArchive.Controls.Add(this.pScanDir);
			this.tpArchive.Controls.Add(this.tsArchiver);
			this.tpArchive.ImageIndex = 0;
			this.tpArchive.Location = new System.Drawing.Point(4, 23);
			this.tpArchive.Name = "tpArchive";
			this.tpArchive.Padding = new System.Windows.Forms.Padding(3);
			this.tpArchive.Size = new System.Drawing.Size(815, 500);
			this.tpArchive.TabIndex = 0;
			this.tpArchive.Text = " Упаковать (Zip)";
			this.tpArchive.UseVisualStyleBackColor = true;
			// 
			// gboxCount
			// 
			this.gboxCount.Controls.Add(this.lvGeneralCount);
			this.gboxCount.Controls.Add(this.chBoxViewProgressA);
			this.gboxCount.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gboxCount.Location = new System.Drawing.Point(3, 191);
			this.gboxCount.Name = "gboxCount";
			this.gboxCount.Size = new System.Drawing.Size(809, 306);
			this.gboxCount.TabIndex = 27;
			this.gboxCount.TabStop = false;
			this.gboxCount.Text = " Ход работы ";
			// 
			// lvGeneralCount
			// 
			this.lvGeneralCount.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeader1,
									this.columnHeader2});
			this.lvGeneralCount.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvGeneralCount.FullRowSelect = true;
			this.lvGeneralCount.GridLines = true;
			this.lvGeneralCount.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
									listViewItem1,
									listViewItem2,
									listViewItem3});
			this.lvGeneralCount.Location = new System.Drawing.Point(3, 40);
			this.lvGeneralCount.Name = "lvGeneralCount";
			this.lvGeneralCount.Size = new System.Drawing.Size(803, 263);
			this.lvGeneralCount.TabIndex = 25;
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
			// chBoxViewProgressA
			// 
			this.chBoxViewProgressA.Dock = System.Windows.Forms.DockStyle.Top;
			this.chBoxViewProgressA.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxViewProgressA.Location = new System.Drawing.Point(3, 16);
			this.chBoxViewProgressA.Name = "chBoxViewProgressA";
			this.chBoxViewProgressA.Size = new System.Drawing.Size(803, 24);
			this.chBoxViewProgressA.TabIndex = 24;
			this.chBoxViewProgressA.Text = "Отображать изменение хода работы";
			this.chBoxViewProgressA.UseVisualStyleBackColor = true;
			// 
			// pOptions
			// 
			this.pOptions.AutoSize = true;
			this.pOptions.Controls.Add(this.cboxDelFB2Files);
			this.pOptions.Controls.Add(this.pType);
			this.pOptions.Controls.Add(this.pToAnotherDir);
			this.pOptions.Dock = System.Windows.Forms.DockStyle.Top;
			this.pOptions.Location = new System.Drawing.Point(3, 110);
			this.pOptions.Name = "pOptions";
			this.pOptions.Size = new System.Drawing.Size(809, 81);
			this.pOptions.TabIndex = 26;
			// 
			// cboxDelFB2Files
			// 
			this.cboxDelFB2Files.Dock = System.Windows.Forms.DockStyle.Top;
			this.cboxDelFB2Files.Location = new System.Drawing.Point(0, 57);
			this.cboxDelFB2Files.Name = "cboxDelFB2Files";
			this.cboxDelFB2Files.Size = new System.Drawing.Size(809, 24);
			this.cboxDelFB2Files.TabIndex = 12;
			this.cboxDelFB2Files.Text = " Удалить fb2-файлы после упаковки";
			this.cboxDelFB2Files.UseVisualStyleBackColor = true;
			// 
			// pType
			// 
			this.pType.AutoSize = true;
			this.pType.Controls.Add(this.cboxExistArchive);
			this.pType.Controls.Add(this.lblExistArchive);
			this.pType.Dock = System.Windows.Forms.DockStyle.Top;
			this.pType.Location = new System.Drawing.Point(0, 30);
			this.pType.Name = "pType";
			this.pType.Size = new System.Drawing.Size(809, 27);
			this.pType.TabIndex = 11;
			// 
			// cboxExistArchive
			// 
			this.cboxExistArchive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxExistArchive.FormattingEnabled = true;
			this.cboxExistArchive.Items.AddRange(new object[] {
									"Заменить существующий fb2-архив создаваемым",
									"Добавить к создаваемому fb2-архиву очередной номер",
									"Добавить к создаваемому fb2-архиву дату и время"});
			this.cboxExistArchive.Location = new System.Drawing.Point(208, 3);
			this.cboxExistArchive.Name = "cboxExistArchive";
			this.cboxExistArchive.Size = new System.Drawing.Size(405, 21);
			this.cboxExistArchive.TabIndex = 16;
			// 
			// lblExistArchive
			// 
			this.lblExistArchive.AutoSize = true;
			this.lblExistArchive.Location = new System.Drawing.Point(3, 6);
			this.lblExistArchive.Name = "lblExistArchive";
			this.lblExistArchive.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblExistArchive.Size = new System.Drawing.Size(155, 13);
			this.lblExistArchive.TabIndex = 15;
			this.lblExistArchive.Text = "Одинаковые fb2-архивы:";
			this.lblExistArchive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pToAnotherDir
			// 
			this.pToAnotherDir.AutoSize = true;
			this.pToAnotherDir.Controls.Add(this.label1);
			this.pToAnotherDir.Controls.Add(this.tboxToAnotherDir);
			this.pToAnotherDir.Controls.Add(this.btnToAnotherDir);
			this.pToAnotherDir.Dock = System.Windows.Forms.DockStyle.Top;
			this.pToAnotherDir.Location = new System.Drawing.Point(0, 0);
			this.pToAnotherDir.Name = "pToAnotherDir";
			this.pToAnotherDir.Size = new System.Drawing.Size(809, 30);
			this.pToAnotherDir.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.label1.Location = new System.Drawing.Point(3, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(162, 19);
			this.label1.TabIndex = 8;
			this.label1.Text = "Где размещать архивы";
			// 
			// tboxToAnotherDir
			// 
			this.tboxToAnotherDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxToAnotherDir.Location = new System.Drawing.Point(208, 6);
			this.tboxToAnotherDir.Name = "tboxToAnotherDir";
			this.tboxToAnotherDir.Size = new System.Drawing.Size(597, 20);
			this.tboxToAnotherDir.TabIndex = 6;
			this.tboxToAnotherDir.TextChanged += new System.EventHandler(this.TboxToAnotherDirTextChanged);
			// 
			// btnToAnotherDir
			// 
			this.btnToAnotherDir.Image = ((System.Drawing.Image)(resources.GetObject("btnToAnotherDir.Image")));
			this.btnToAnotherDir.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnToAnotherDir.Location = new System.Drawing.Point(165, 3);
			this.btnToAnotherDir.Name = "btnToAnotherDir";
			this.btnToAnotherDir.Size = new System.Drawing.Size(37, 24);
			this.btnToAnotherDir.TabIndex = 7;
			this.btnToAnotherDir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnToAnotherDir.UseVisualStyleBackColor = true;
			this.btnToAnotherDir.Click += new System.EventHandler(this.BtnToAnotherDirClick);
			// 
			// tpUnArchive
			// 
			this.tpUnArchive.Controls.Add(this.gboxUACount);
			this.tpUnArchive.Controls.Add(this.pUAOptions);
			this.tpUnArchive.Controls.Add(this.pUAScanDir);
			this.tpUnArchive.Controls.Add(this.tsUnArchiver);
			this.tpUnArchive.ImageIndex = 1;
			this.tpUnArchive.Location = new System.Drawing.Point(4, 23);
			this.tpUnArchive.Name = "tpUnArchive";
			this.tpUnArchive.Padding = new System.Windows.Forms.Padding(3);
			this.tpUnArchive.Size = new System.Drawing.Size(815, 500);
			this.tpUnArchive.TabIndex = 1;
			this.tpUnArchive.Text = " Распаковать (UnZip)";
			this.tpUnArchive.UseVisualStyleBackColor = true;
			// 
			// gboxUACount
			// 
			this.gboxUACount.Controls.Add(this.panel2);
			this.gboxUACount.Controls.Add(this.chBoxViewProgressU);
			this.gboxUACount.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gboxUACount.Location = new System.Drawing.Point(3, 191);
			this.gboxUACount.Name = "gboxUACount";
			this.gboxUACount.Size = new System.Drawing.Size(809, 306);
			this.gboxUACount.TabIndex = 29;
			this.gboxUACount.TabStop = false;
			this.gboxUACount.Text = " Ход работы ";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.lvUAGeneralCount);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(3, 40);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(803, 263);
			this.panel2.TabIndex = 7;
			// 
			// lvUAGeneralCount
			// 
			this.lvUAGeneralCount.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.cHeaderDirsFiles,
									this.cHeaderCount});
			this.lvUAGeneralCount.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvUAGeneralCount.FullRowSelect = true;
			this.lvUAGeneralCount.GridLines = true;
			this.lvUAGeneralCount.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
									listViewItem4,
									listViewItem5,
									listViewItem6,
									listViewItem7});
			this.lvUAGeneralCount.Location = new System.Drawing.Point(0, 0);
			this.lvUAGeneralCount.Name = "lvUAGeneralCount";
			this.lvUAGeneralCount.Size = new System.Drawing.Size(803, 263);
			this.lvUAGeneralCount.TabIndex = 2;
			this.lvUAGeneralCount.UseCompatibleStateImageBehavior = false;
			this.lvUAGeneralCount.View = System.Windows.Forms.View.Details;
			// 
			// cHeaderDirsFiles
			// 
			this.cHeaderDirsFiles.Text = "Папки и файлы";
			this.cHeaderDirsFiles.Width = 250;
			// 
			// cHeaderCount
			// 
			this.cHeaderCount.Text = "Количество";
			this.cHeaderCount.Width = 100;
			// 
			// chBoxViewProgressU
			// 
			this.chBoxViewProgressU.Dock = System.Windows.Forms.DockStyle.Top;
			this.chBoxViewProgressU.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxViewProgressU.Location = new System.Drawing.Point(3, 16);
			this.chBoxViewProgressU.Name = "chBoxViewProgressU";
			this.chBoxViewProgressU.Size = new System.Drawing.Size(803, 24);
			this.chBoxViewProgressU.TabIndex = 25;
			this.chBoxViewProgressU.Text = "Отображать изменение хода работы";
			this.chBoxViewProgressU.UseVisualStyleBackColor = true;
			// 
			// pUAOptions
			// 
			this.pUAOptions.AutoSize = true;
			this.pUAOptions.Controls.Add(this.cboxUADelFB2Files);
			this.pUAOptions.Controls.Add(this.pUAType);
			this.pUAOptions.Controls.Add(this.pUAToAnotherDir);
			this.pUAOptions.Dock = System.Windows.Forms.DockStyle.Top;
			this.pUAOptions.Location = new System.Drawing.Point(3, 110);
			this.pUAOptions.Name = "pUAOptions";
			this.pUAOptions.Size = new System.Drawing.Size(809, 81);
			this.pUAOptions.TabIndex = 28;
			// 
			// cboxUADelFB2Files
			// 
			this.cboxUADelFB2Files.Dock = System.Windows.Forms.DockStyle.Top;
			this.cboxUADelFB2Files.Location = new System.Drawing.Point(0, 57);
			this.cboxUADelFB2Files.Name = "cboxUADelFB2Files";
			this.cboxUADelFB2Files.Size = new System.Drawing.Size(809, 24);
			this.cboxUADelFB2Files.TabIndex = 30;
			this.cboxUADelFB2Files.Text = " Удалить архивы после распаковки";
			this.cboxUADelFB2Files.UseVisualStyleBackColor = true;
			// 
			// pUAType
			// 
			this.pUAType.AutoSize = true;
			this.pUAType.Controls.Add(this.cboxUAExistArchive);
			this.pUAType.Controls.Add(this.lblUAExistArchive);
			this.pUAType.Dock = System.Windows.Forms.DockStyle.Top;
			this.pUAType.Location = new System.Drawing.Point(0, 30);
			this.pUAType.Name = "pUAType";
			this.pUAType.Size = new System.Drawing.Size(809, 27);
			this.pUAType.TabIndex = 27;
			// 
			// cboxUAExistArchive
			// 
			this.cboxUAExistArchive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxUAExistArchive.FormattingEnabled = true;
			this.cboxUAExistArchive.Items.AddRange(new object[] {
									"Заменить существующий fb2-файл создаваемым",
									"Добавить к создаваемому fb2-файлу очередной номер",
									"Добавить к создаваемому fb2-файлу дату и время"});
			this.cboxUAExistArchive.Location = new System.Drawing.Point(208, 3);
			this.cboxUAExistArchive.Name = "cboxUAExistArchive";
			this.cboxUAExistArchive.Size = new System.Drawing.Size(405, 21);
			this.cboxUAExistArchive.TabIndex = 16;
			// 
			// lblUAExistArchive
			// 
			this.lblUAExistArchive.AutoSize = true;
			this.lblUAExistArchive.Location = new System.Drawing.Point(3, 6);
			this.lblUAExistArchive.Name = "lblUAExistArchive";
			this.lblUAExistArchive.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblUAExistArchive.Size = new System.Drawing.Size(150, 13);
			this.lblUAExistArchive.TabIndex = 15;
			this.lblUAExistArchive.Text = "Одинаковые fb2-файлы:";
			this.lblUAExistArchive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pUAToAnotherDir
			// 
			this.pUAToAnotherDir.AutoSize = true;
			this.pUAToAnotherDir.Controls.Add(this.label2);
			this.pUAToAnotherDir.Controls.Add(this.btnUAToAnotherDir);
			this.pUAToAnotherDir.Controls.Add(this.tboxUAToAnotherDir);
			this.pUAToAnotherDir.Dock = System.Windows.Forms.DockStyle.Top;
			this.pUAToAnotherDir.Location = new System.Drawing.Point(0, 0);
			this.pUAToAnotherDir.Name = "pUAToAnotherDir";
			this.pUAToAnotherDir.Size = new System.Drawing.Size(809, 30);
			this.pUAToAnotherDir.TabIndex = 9;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.label2.Location = new System.Drawing.Point(3, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(162, 19);
			this.label2.TabIndex = 8;
			this.label2.Text = "Где размещать fb2:";
			// 
			// btnUAToAnotherDir
			// 
			this.btnUAToAnotherDir.Image = ((System.Drawing.Image)(resources.GetObject("btnUAToAnotherDir.Image")));
			this.btnUAToAnotherDir.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnUAToAnotherDir.Location = new System.Drawing.Point(165, 3);
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
			this.tboxUAToAnotherDir.Location = new System.Drawing.Point(208, 6);
			this.tboxUAToAnotherDir.Name = "tboxUAToAnotherDir";
			this.tboxUAToAnotherDir.Size = new System.Drawing.Size(597, 20);
			this.tboxUAToAnotherDir.TabIndex = 6;
			this.tboxUAToAnotherDir.TextChanged += new System.EventHandler(this.TboxUAToAnotherDirTextChanged);
			// 
			// pUAScanDir
			// 
			this.pUAScanDir.AutoSize = true;
			this.pUAScanDir.Controls.Add(this.cboxUAToSomeDir);
			this.pUAScanDir.Controls.Add(this.btnUAOpenDir);
			this.pUAScanDir.Controls.Add(this.cboxScanSubDirToUnArchive);
			this.pUAScanDir.Controls.Add(this.tboxUASourceDir);
			this.pUAScanDir.Controls.Add(this.lblUAScanDir);
			this.pUAScanDir.Dock = System.Windows.Forms.DockStyle.Top;
			this.pUAScanDir.Location = new System.Drawing.Point(3, 34);
			this.pUAScanDir.Margin = new System.Windows.Forms.Padding(0);
			this.pUAScanDir.Name = "pUAScanDir";
			this.pUAScanDir.Size = new System.Drawing.Size(809, 76);
			this.pUAScanDir.TabIndex = 23;
			// 
			// cboxUAToSomeDir
			// 
			this.cboxUAToSomeDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.cboxUAToSomeDir.ForeColor = System.Drawing.Color.Navy;
			this.cboxUAToSomeDir.Location = new System.Drawing.Point(208, 49);
			this.cboxUAToSomeDir.Name = "cboxUAToSomeDir";
			this.cboxUAToSomeDir.Size = new System.Drawing.Size(597, 24);
			this.cboxUAToSomeDir.TabIndex = 12;
			this.cboxUAToSomeDir.Text = "Поместить fb2-файл в ту же папку, где находится исходный архив";
			this.cboxUAToSomeDir.UseVisualStyleBackColor = true;
			this.cboxUAToSomeDir.CheckedChanged += new System.EventHandler(this.CboxUAToSomeDirCheckedChanged);
			// 
			// btnUAOpenDir
			// 
			this.btnUAOpenDir.Image = ((System.Drawing.Image)(resources.GetObject("btnUAOpenDir.Image")));
			this.btnUAOpenDir.Location = new System.Drawing.Point(165, 3);
			this.btnUAOpenDir.Name = "btnUAOpenDir";
			this.btnUAOpenDir.Size = new System.Drawing.Size(37, 27);
			this.btnUAOpenDir.TabIndex = 11;
			this.btnUAOpenDir.UseVisualStyleBackColor = true;
			this.btnUAOpenDir.Click += new System.EventHandler(this.BtnUAOpenDirClick);
			// 
			// cboxScanSubDirToUnArchive
			// 
			this.cboxScanSubDirToUnArchive.Checked = true;
			this.cboxScanSubDirToUnArchive.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cboxScanSubDirToUnArchive.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.cboxScanSubDirToUnArchive.ForeColor = System.Drawing.Color.Navy;
			this.cboxScanSubDirToUnArchive.Location = new System.Drawing.Point(208, 29);
			this.cboxScanSubDirToUnArchive.Name = "cboxScanSubDirToUnArchive";
			this.cboxScanSubDirToUnArchive.Size = new System.Drawing.Size(598, 24);
			this.cboxScanSubDirToUnArchive.TabIndex = 7;
			this.cboxScanSubDirToUnArchive.Text = "Сканировать и подпапки";
			this.cboxScanSubDirToUnArchive.UseVisualStyleBackColor = true;
			// 
			// tboxUASourceDir
			// 
			this.tboxUASourceDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxUASourceDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tboxUASourceDir.Location = new System.Drawing.Point(208, 5);
			this.tboxUASourceDir.Name = "tboxUASourceDir";
			this.tboxUASourceDir.Size = new System.Drawing.Size(597, 20);
			this.tboxUASourceDir.TabIndex = 4;
			this.tboxUASourceDir.TextChanged += new System.EventHandler(this.TboxUASourceDirTextChanged);
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
									this.tsbtnUnArchive,
									this.toolStripSeparator2,
									this.tsbtnUnArchiveStop});
			this.tsUnArchiver.Location = new System.Drawing.Point(3, 3);
			this.tsUnArchiver.Name = "tsUnArchiver";
			this.tsUnArchiver.Size = new System.Drawing.Size(809, 31);
			this.tsUnArchiver.TabIndex = 3;
			// 
			// tsbtnUnArchive
			// 
			this.tsbtnUnArchive.AutoToolTip = false;
			this.tsbtnUnArchive.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnUnArchive.Image")));
			this.tsbtnUnArchive.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnUnArchive.Name = "tsbtnUnArchive";
			this.tsbtnUnArchive.Size = new System.Drawing.Size(100, 28);
			this.tsbtnUnArchive.Text = "Распаковать";
			this.tsbtnUnArchive.Click += new System.EventHandler(this.TsbtnUnArchiveClick);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			// 
			// tsbtnUnArchiveStop
			// 
			this.tsbtnUnArchiveStop.AutoToolTip = false;
			this.tsbtnUnArchiveStop.Enabled = false;
			this.tsbtnUnArchiveStop.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnUnArchiveStop.Image")));
			this.tsbtnUnArchiveStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnUnArchiveStop.Name = "tsbtnUnArchiveStop";
			this.tsbtnUnArchiveStop.Size = new System.Drawing.Size(96, 28);
			this.tsbtnUnArchiveStop.Text = "Остановить";
			this.tsbtnUnArchiveStop.Click += new System.EventHandler(this.TsbtnUnArchiveStopClick);
			// 
			// imgl16
			// 
			this.imgl16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgl16.ImageStream")));
			this.imgl16.TransparentColor = System.Drawing.Color.Transparent;
			this.imgl16.Images.SetKeyName(0, "Archive1.png");
			this.imgl16.Images.SetKeyName(1, "UnArchive1.png");
			// 
			// SFBTpArchiveManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pCentral);
			this.Controls.Add(this.ssProgress);
			this.Name = "SFBTpArchiveManager";
			this.Size = new System.Drawing.Size(823, 549);
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
			this.gboxCount.ResumeLayout(false);
			this.pOptions.ResumeLayout(false);
			this.pOptions.PerformLayout();
			this.pType.ResumeLayout(false);
			this.pType.PerformLayout();
			this.pToAnotherDir.ResumeLayout(false);
			this.pToAnotherDir.PerformLayout();
			this.tpUnArchive.ResumeLayout(false);
			this.tpUnArchive.PerformLayout();
			this.gboxUACount.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.pUAOptions.ResumeLayout(false);
			this.pUAOptions.PerformLayout();
			this.pUAType.ResumeLayout(false);
			this.pUAType.PerformLayout();
			this.pUAToAnotherDir.ResumeLayout(false);
			this.pUAToAnotherDir.PerformLayout();
			this.pUAScanDir.ResumeLayout(false);
			this.pUAScanDir.PerformLayout();
			this.tsUnArchiver.ResumeLayout(false);
			this.tsUnArchiver.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.CheckBox cboxUAToSomeDir;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox cboxToSomeDir;
		private System.Windows.Forms.Button btnUAOpenDir;
		private System.Windows.Forms.Button btnOpenDir;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chBoxViewProgressU;
		private System.Windows.Forms.CheckBox chBoxViewProgressA;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.ToolStripButton tsbtnUnArchiveStop;
		private System.Windows.Forms.ToolStripButton tsbtnArchiveStop;
		private System.Windows.Forms.CheckBox cboxScanSubDirToUnArchive;
		private System.Windows.Forms.CheckBox cboxScanSubDirToArchive;
		private System.Windows.Forms.ImageList imgl16;
		private System.Windows.Forms.GroupBox gboxCount;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ListView lvGeneralCount;
		private System.Windows.Forms.ColumnHeader cHeaderCount;
		private System.Windows.Forms.ColumnHeader cHeaderDirsFiles;
		private System.Windows.Forms.ListView lvUAGeneralCount;
		private System.Windows.Forms.GroupBox gboxUACount;
		private System.Windows.Forms.TextBox tboxUAToAnotherDir;
		private System.Windows.Forms.Button btnUAToAnotherDir;
		private System.Windows.Forms.Panel pUAToAnotherDir;
		private System.Windows.Forms.CheckBox cboxUADelFB2Files;
		private System.Windows.Forms.Panel pUAOptions;
		private System.Windows.Forms.Label lblUAExistArchive;
		private System.Windows.Forms.ComboBox cboxUAExistArchive;
		private System.Windows.Forms.Panel pUAType;
		private System.Windows.Forms.Label lblUAScanDir;
		private System.Windows.Forms.TextBox tboxUASourceDir;
		private System.Windows.Forms.Panel pUAScanDir;
		private System.Windows.Forms.ToolStripButton tsbtnUnArchive;
		private System.Windows.Forms.ToolStrip tsUnArchiver;
		private System.Windows.Forms.ToolStrip tsArchiver;
		private System.Windows.Forms.ComboBox cboxExistArchive;
		private System.Windows.Forms.Label lblExistArchive;
		private System.Windows.Forms.FolderBrowserDialog fbdDir;
		private System.Windows.Forms.ToolStripButton tsbtnArchive;
		private System.Windows.Forms.CheckBox cboxDelFB2Files;
		private System.Windows.Forms.TextBox tboxToAnotherDir;
		private System.Windows.Forms.Button btnToAnotherDir;
		private System.Windows.Forms.Panel pToAnotherDir;
		private System.Windows.Forms.Panel pOptions;
		private System.Windows.Forms.Panel pType;
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
	}
}
