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
			System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
									"Всего папок",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
									"Всего файлов",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
									"Распаковано архивов",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem(new string[] {
									"fb2-файлы из этих архивов",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem(new string[] {
									"Другие файлы из этих архивов",
									"0"}, -1);
			System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Архивы", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem(new string[] {
									"Rar",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem(new string[] {
									"Zip",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem(new string[] {
									"7z",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem(new string[] {
									"BZip2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem(new string[] {
									"GZip",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem(new string[] {
									"Tar",
									"0"}, -1);
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
									"Другие файлы",
									"0"}, -1);
			this.tsArchiver = new System.Windows.Forms.ToolStrip();
			this.tsbtnOpenDir = new System.Windows.Forms.ToolStripButton();
			this.tsSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnArchive = new System.Windows.Forms.ToolStripButton();
			this.tsbtnArchiveStop = new System.Windows.Forms.ToolStripButton();
			this.ssProgress = new System.Windows.Forms.StatusStrip();
			this.tsslblProgress = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.pScanDir = new System.Windows.Forms.Panel();
			this.cboxScanSubDirToArchive = new System.Windows.Forms.CheckBox();
			this.tboxSourceDir = new System.Windows.Forms.TextBox();
			this.lblDir = new System.Windows.Forms.Label();
			this.pCentral = new System.Windows.Forms.Panel();
			this.tcArchiver = new System.Windows.Forms.TabControl();
			this.tpArchive = new System.Windows.Forms.TabPage();
			this.pOptions = new System.Windows.Forms.Panel();
			this.gboxCount = new System.Windows.Forms.GroupBox();
			this.gboxOptions = new System.Windows.Forms.GroupBox();
			this.cboxAddRestoreInfo = new System.Windows.Forms.CheckBox();
			this.cboxDelFB2Files = new System.Windows.Forms.CheckBox();
			this.pArchiveFiles = new System.Windows.Forms.Panel();
			this.cboxPriorityA = new System.Windows.Forms.ComboBox();
			this.lblPriorityA = new System.Windows.Forms.Label();
			this.rbAny = new System.Windows.Forms.RadioButton();
			this.lblFilesType = new System.Windows.Forms.Label();
			this.rbFB2 = new System.Windows.Forms.RadioButton();
			this.pToAnotherDir = new System.Windows.Forms.Panel();
			this.btnToAnotherDir = new System.Windows.Forms.Button();
			this.tboxToAnotherDir = new System.Windows.Forms.TextBox();
			this.rbtnToAnotherDir = new System.Windows.Forms.RadioButton();
			this.rbtnToSomeDir = new System.Windows.Forms.RadioButton();
			this.pType = new System.Windows.Forms.Panel();
			this.chBoxAddArchiveNameBookID = new System.Windows.Forms.CheckBox();
			this.cboxExistArchive = new System.Windows.Forms.ComboBox();
			this.lblExistArchive = new System.Windows.Forms.Label();
			this.lblArchiveType = new System.Windows.Forms.Label();
			this.cboxArchiveType = new System.Windows.Forms.ComboBox();
			this.tpUnArchive = new System.Windows.Forms.TabPage();
			this.pUAOptions = new System.Windows.Forms.Panel();
			this.gboxUAOptions = new System.Windows.Forms.GroupBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.gboxUACount = new System.Windows.Forms.GroupBox();
			this.lvUAGeneralCount = new System.Windows.Forms.ListView();
			this.cHeaderDirsFiles = new System.Windows.Forms.ColumnHeader();
			this.cHeaderCount = new System.Windows.Forms.ColumnHeader();
			this.lvUACount = new System.Windows.Forms.ListView();
			this.cHeaderArchive = new System.Windows.Forms.ColumnHeader();
			this.cHeaderArchiveCount = new System.Windows.Forms.ColumnHeader();
			this.cboxUADelFB2Files = new System.Windows.Forms.CheckBox();
			this.pUnbPackFiles = new System.Windows.Forms.Panel();
			this.cboxPriorityU = new System.Windows.Forms.ComboBox();
			this.lblPriorityU = new System.Windows.Forms.Label();
			this.rbAnyU = new System.Windows.Forms.RadioButton();
			this.rbFB2U = new System.Windows.Forms.RadioButton();
			this.lblFilesUType = new System.Windows.Forms.Label();
			this.pUAToAnotherDir = new System.Windows.Forms.Panel();
			this.btnUAToAnotherDir = new System.Windows.Forms.Button();
			this.tboxUAToAnotherDir = new System.Windows.Forms.TextBox();
			this.rbtnUAToAnotherDir = new System.Windows.Forms.RadioButton();
			this.rbtnUAToSomeDir = new System.Windows.Forms.RadioButton();
			this.pUAType = new System.Windows.Forms.Panel();
			this.chBoxAddFileNameBookID = new System.Windows.Forms.CheckBox();
			this.cboxUAExistArchive = new System.Windows.Forms.ComboBox();
			this.lblUAExistArchive = new System.Windows.Forms.Label();
			this.lblUAType = new System.Windows.Forms.Label();
			this.cboxUAType = new System.Windows.Forms.ComboBox();
			this.pUAScanDir = new System.Windows.Forms.Panel();
			this.cboxScanSubDirToUnArchive = new System.Windows.Forms.CheckBox();
			this.tboxUASourceDir = new System.Windows.Forms.TextBox();
			this.lblUAScanDir = new System.Windows.Forms.Label();
			this.tsUnArchiver = new System.Windows.Forms.ToolStrip();
			this.tsbtnUAOpenDir = new System.Windows.Forms.ToolStripButton();
			this.tsSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnUAAnalyze = new System.Windows.Forms.ToolStripButton();
			this.tsbtnUAAnalyzeStop = new System.Windows.Forms.ToolStripButton();
			this.tsSep3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnUnArchive = new System.Windows.Forms.ToolStripButton();
			this.tsbtnUnArchiveStop = new System.Windows.Forms.ToolStripButton();
			this.imgl16 = new System.Windows.Forms.ImageList(this.components);
			this.fbdDir = new System.Windows.Forms.FolderBrowserDialog();
			this.chBoxViewProgressA = new System.Windows.Forms.CheckBox();
			this.lvGeneralCount = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.chBoxViewProgressU = new System.Windows.Forms.CheckBox();
			this.tsArchiver.SuspendLayout();
			this.ssProgress.SuspendLayout();
			this.pScanDir.SuspendLayout();
			this.pCentral.SuspendLayout();
			this.tcArchiver.SuspendLayout();
			this.tpArchive.SuspendLayout();
			this.pOptions.SuspendLayout();
			this.gboxCount.SuspendLayout();
			this.gboxOptions.SuspendLayout();
			this.pArchiveFiles.SuspendLayout();
			this.pToAnotherDir.SuspendLayout();
			this.pType.SuspendLayout();
			this.tpUnArchive.SuspendLayout();
			this.pUAOptions.SuspendLayout();
			this.gboxUAOptions.SuspendLayout();
			this.panel2.SuspendLayout();
			this.gboxUACount.SuspendLayout();
			this.pUnbPackFiles.SuspendLayout();
			this.pUAToAnotherDir.SuspendLayout();
			this.pUAType.SuspendLayout();
			this.pUAScanDir.SuspendLayout();
			this.tsUnArchiver.SuspendLayout();
			this.SuspendLayout();
			// 
			// tsArchiver
			// 
			this.tsArchiver.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tsArchiver.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsbtnOpenDir,
									this.tsSep1,
									this.tsbtnArchive,
									this.tsbtnArchiveStop});
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
			this.tsbtnOpenDir.Size = new System.Drawing.Size(123, 28);
			this.tsbtnOpenDir.Text = "Папка - источник";
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
			this.tsbtnArchive.AutoToolTip = false;
			this.tsbtnArchive.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnArchive.Image")));
			this.tsbtnArchive.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnArchive.Name = "tsbtnArchive";
			this.tsbtnArchive.Size = new System.Drawing.Size(95, 28);
			this.tsbtnArchive.Text = "Запаковать";
			this.tsbtnArchive.Click += new System.EventHandler(this.TsbtnArchiveClick);
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
			this.pScanDir.Controls.Add(this.cboxScanSubDirToArchive);
			this.pScanDir.Controls.Add(this.tboxSourceDir);
			this.pScanDir.Controls.Add(this.lblDir);
			this.pScanDir.Dock = System.Windows.Forms.DockStyle.Top;
			this.pScanDir.Location = new System.Drawing.Point(3, 34);
			this.pScanDir.Margin = new System.Windows.Forms.Padding(0);
			this.pScanDir.Name = "pScanDir";
			this.pScanDir.Size = new System.Drawing.Size(754, 31);
			this.pScanDir.TabIndex = 22;
			// 
			// cboxScanSubDirToArchive
			// 
			this.cboxScanSubDirToArchive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cboxScanSubDirToArchive.Checked = true;
			this.cboxScanSubDirToArchive.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cboxScanSubDirToArchive.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.cboxScanSubDirToArchive.ForeColor = System.Drawing.Color.Navy;
			this.cboxScanSubDirToArchive.Location = new System.Drawing.Point(579, 4);
			this.cboxScanSubDirToArchive.Name = "cboxScanSubDirToArchive";
			this.cboxScanSubDirToArchive.Size = new System.Drawing.Size(172, 24);
			this.cboxScanSubDirToArchive.TabIndex = 6;
			this.cboxScanSubDirToArchive.Text = "Сканировать и подпапки";
			this.cboxScanSubDirToArchive.UseVisualStyleBackColor = true;
			// 
			// tboxSourceDir
			// 
			this.tboxSourceDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxSourceDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tboxSourceDir.Location = new System.Drawing.Point(162, 5);
			this.tboxSourceDir.Name = "tboxSourceDir";
			this.tboxSourceDir.Size = new System.Drawing.Size(411, 20);
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
			this.pCentral.Size = new System.Drawing.Size(768, 527);
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
			this.tcArchiver.Size = new System.Drawing.Size(768, 527);
			this.tcArchiver.TabIndex = 0;
			// 
			// tpArchive
			// 
			this.tpArchive.Controls.Add(this.pOptions);
			this.tpArchive.Controls.Add(this.pType);
			this.tpArchive.Controls.Add(this.pScanDir);
			this.tpArchive.Controls.Add(this.tsArchiver);
			this.tpArchive.ImageIndex = 0;
			this.tpArchive.Location = new System.Drawing.Point(4, 23);
			this.tpArchive.Name = "tpArchive";
			this.tpArchive.Padding = new System.Windows.Forms.Padding(3);
			this.tpArchive.Size = new System.Drawing.Size(760, 500);
			this.tpArchive.TabIndex = 0;
			this.tpArchive.Text = " Запаковать ";
			this.tpArchive.UseVisualStyleBackColor = true;
			// 
			// pOptions
			// 
			this.pOptions.Controls.Add(this.gboxCount);
			this.pOptions.Controls.Add(this.gboxOptions);
			this.pOptions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pOptions.Location = new System.Drawing.Point(3, 113);
			this.pOptions.Name = "pOptions";
			this.pOptions.Size = new System.Drawing.Size(754, 384);
			this.pOptions.TabIndex = 26;
			// 
			// gboxCount
			// 
			this.gboxCount.Controls.Add(this.lvGeneralCount);
			this.gboxCount.Controls.Add(this.chBoxViewProgressA);
			this.gboxCount.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gboxCount.Location = new System.Drawing.Point(0, 177);
			this.gboxCount.Name = "gboxCount";
			this.gboxCount.Size = new System.Drawing.Size(754, 207);
			this.gboxCount.TabIndex = 4;
			this.gboxCount.TabStop = false;
			this.gboxCount.Text = " Ход работы ";
			// 
			// gboxOptions
			// 
			this.gboxOptions.Controls.Add(this.cboxAddRestoreInfo);
			this.gboxOptions.Controls.Add(this.cboxDelFB2Files);
			this.gboxOptions.Controls.Add(this.pArchiveFiles);
			this.gboxOptions.Controls.Add(this.pToAnotherDir);
			this.gboxOptions.Controls.Add(this.rbtnToAnotherDir);
			this.gboxOptions.Controls.Add(this.rbtnToSomeDir);
			this.gboxOptions.Dock = System.Windows.Forms.DockStyle.Top;
			this.gboxOptions.Location = new System.Drawing.Point(0, 0);
			this.gboxOptions.Name = "gboxOptions";
			this.gboxOptions.Size = new System.Drawing.Size(754, 177);
			this.gboxOptions.TabIndex = 1;
			this.gboxOptions.TabStop = false;
			this.gboxOptions.Text = " Настройки ";
			// 
			// cboxAddRestoreInfo
			// 
			this.cboxAddRestoreInfo.Dock = System.Windows.Forms.DockStyle.Top;
			this.cboxAddRestoreInfo.Location = new System.Drawing.Point(3, 151);
			this.cboxAddRestoreInfo.Name = "cboxAddRestoreInfo";
			this.cboxAddRestoreInfo.Size = new System.Drawing.Size(748, 24);
			this.cboxAddRestoreInfo.TabIndex = 7;
			this.cboxAddRestoreInfo.Text = " Добавить в архив информацию для его восстановления";
			this.cboxAddRestoreInfo.UseVisualStyleBackColor = true;
			this.cboxAddRestoreInfo.Visible = false;
			// 
			// cboxDelFB2Files
			// 
			this.cboxDelFB2Files.Dock = System.Windows.Forms.DockStyle.Top;
			this.cboxDelFB2Files.Location = new System.Drawing.Point(3, 127);
			this.cboxDelFB2Files.Name = "cboxDelFB2Files";
			this.cboxDelFB2Files.Size = new System.Drawing.Size(748, 24);
			this.cboxDelFB2Files.TabIndex = 6;
			this.cboxDelFB2Files.Text = " Удалить fb2-файлы после упаковки";
			this.cboxDelFB2Files.UseVisualStyleBackColor = true;
			// 
			// pArchiveFiles
			// 
			this.pArchiveFiles.Controls.Add(this.cboxPriorityA);
			this.pArchiveFiles.Controls.Add(this.lblPriorityA);
			this.pArchiveFiles.Controls.Add(this.rbAny);
			this.pArchiveFiles.Controls.Add(this.lblFilesType);
			this.pArchiveFiles.Controls.Add(this.rbFB2);
			this.pArchiveFiles.Dock = System.Windows.Forms.DockStyle.Top;
			this.pArchiveFiles.Location = new System.Drawing.Point(3, 97);
			this.pArchiveFiles.Name = "pArchiveFiles";
			this.pArchiveFiles.Size = new System.Drawing.Size(748, 30);
			this.pArchiveFiles.TabIndex = 5;
			// 
			// cboxPriorityA
			// 
			this.cboxPriorityA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxPriorityA.FormattingEnabled = true;
			this.cboxPriorityA.Items.AddRange(new object[] {
									"Низкий (Idle)",
									"Ниже Среднего",
									"Средний",
									"Выше Среднего",
									"Высокий",
									"Реального времени (RealTime)"});
			this.cboxPriorityA.Location = new System.Drawing.Point(409, 5);
			this.cboxPriorityA.Name = "cboxPriorityA";
			this.cboxPriorityA.Size = new System.Drawing.Size(328, 21);
			this.cboxPriorityA.TabIndex = 25;
			// 
			// lblPriorityA
			// 
			this.lblPriorityA.AutoSize = true;
			this.lblPriorityA.Location = new System.Drawing.Point(331, 9);
			this.lblPriorityA.Name = "lblPriorityA";
			this.lblPriorityA.Size = new System.Drawing.Size(72, 13);
			this.lblPriorityA.TabIndex = 24;
			this.lblPriorityA.Text = "Приоритет:";
			// 
			// rbAny
			// 
			this.rbAny.Location = new System.Drawing.Point(240, 8);
			this.rbAny.Name = "rbAny";
			this.rbAny.Size = new System.Drawing.Size(76, 18);
			this.rbAny.TabIndex = 23;
			this.rbAny.Text = "Любые";
			this.rbAny.UseVisualStyleBackColor = true;
			// 
			// lblFilesType
			// 
			this.lblFilesType.AutoSize = true;
			this.lblFilesType.Location = new System.Drawing.Point(4, 9);
			this.lblFilesType.Name = "lblFilesType";
			this.lblFilesType.Size = new System.Drawing.Size(133, 13);
			this.lblFilesType.TabIndex = 21;
			this.lblFilesType.Text = "Упаковывать Файлы:";
			// 
			// rbFB2
			// 
			this.rbFB2.Checked = true;
			this.rbFB2.Location = new System.Drawing.Point(147, 8);
			this.rbFB2.Name = "rbFB2";
			this.rbFB2.Size = new System.Drawing.Size(93, 18);
			this.rbFB2.TabIndex = 22;
			this.rbFB2.TabStop = true;
			this.rbFB2.Text = "Только fb2";
			this.rbFB2.UseVisualStyleBackColor = true;
			this.rbFB2.CheckedChanged += new System.EventHandler(this.RbFB2CheckedChanged);
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
			this.btnToAnotherDir.Enabled = false;
			this.btnToAnotherDir.Image = ((System.Drawing.Image)(resources.GetObject("btnToAnotherDir.Image")));
			this.btnToAnotherDir.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnToAnotherDir.Location = new System.Drawing.Point(22, 3);
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
			this.tboxToAnotherDir.Location = new System.Drawing.Point(65, 5);
			this.tboxToAnotherDir.Name = "tboxToAnotherDir";
			this.tboxToAnotherDir.ReadOnly = true;
			this.tboxToAnotherDir.Size = new System.Drawing.Size(672, 20);
			this.tboxToAnotherDir.TabIndex = 6;
			this.tboxToAnotherDir.TextChanged += new System.EventHandler(this.TboxToAnotherDirTextChanged);
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
			this.pType.Controls.Add(this.chBoxAddArchiveNameBookID);
			this.pType.Controls.Add(this.cboxExistArchive);
			this.pType.Controls.Add(this.lblExistArchive);
			this.pType.Controls.Add(this.lblArchiveType);
			this.pType.Controls.Add(this.cboxArchiveType);
			this.pType.Dock = System.Windows.Forms.DockStyle.Top;
			this.pType.Location = new System.Drawing.Point(3, 65);
			this.pType.Name = "pType";
			this.pType.Size = new System.Drawing.Size(754, 48);
			this.pType.TabIndex = 25;
			// 
			// chBoxAddArchiveNameBookID
			// 
			this.chBoxAddArchiveNameBookID.Enabled = false;
			this.chBoxAddArchiveNameBookID.Location = new System.Drawing.Point(353, 27);
			this.chBoxAddArchiveNameBookID.Name = "chBoxAddArchiveNameBookID";
			this.chBoxAddArchiveNameBookID.Size = new System.Drawing.Size(246, 18);
			this.chBoxAddArchiveNameBookID.TabIndex = 20;
			this.chBoxAddArchiveNameBookID.Text = " Добавить ID Книги к имени архива";
			this.chBoxAddArchiveNameBookID.UseVisualStyleBackColor = true;
			// 
			// cboxExistArchive
			// 
			this.cboxExistArchive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxExistArchive.FormattingEnabled = true;
			this.cboxExistArchive.Items.AddRange(new object[] {
									"Заменить существующий fb2-архив создаваемым",
									"Добавить к создаваемому fb2-архиву очередной номер",
									"Добавить к создаваемому fb2-архиву дату и время"});
			this.cboxExistArchive.Location = new System.Drawing.Point(353, 3);
			this.cboxExistArchive.Name = "cboxExistArchive";
			this.cboxExistArchive.Size = new System.Drawing.Size(387, 21);
			this.cboxExistArchive.TabIndex = 16;
			this.cboxExistArchive.SelectedIndexChanged += new System.EventHandler(this.CboxExistArchiveSelectedIndexChanged);
			// 
			// lblExistArchive
			// 
			this.lblExistArchive.AutoSize = true;
			this.lblExistArchive.Location = new System.Drawing.Point(195, 7);
			this.lblExistArchive.Name = "lblExistArchive";
			this.lblExistArchive.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblExistArchive.Size = new System.Drawing.Size(155, 13);
			this.lblExistArchive.TabIndex = 15;
			this.lblExistArchive.Text = "Одинаковые fb2-архивы:";
			this.lblExistArchive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this.tpUnArchive.ImageIndex = 1;
			this.tpUnArchive.Location = new System.Drawing.Point(4, 23);
			this.tpUnArchive.Name = "tpUnArchive";
			this.tpUnArchive.Padding = new System.Windows.Forms.Padding(3);
			this.tpUnArchive.Size = new System.Drawing.Size(760, 500);
			this.tpUnArchive.TabIndex = 1;
			this.tpUnArchive.Text = " Распаковать ";
			this.tpUnArchive.UseVisualStyleBackColor = true;
			// 
			// pUAOptions
			// 
			this.pUAOptions.Controls.Add(this.gboxUAOptions);
			this.pUAOptions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pUAOptions.Location = new System.Drawing.Point(3, 113);
			this.pUAOptions.Name = "pUAOptions";
			this.pUAOptions.Size = new System.Drawing.Size(754, 384);
			this.pUAOptions.TabIndex = 28;
			// 
			// gboxUAOptions
			// 
			this.gboxUAOptions.Controls.Add(this.panel2);
			this.gboxUAOptions.Controls.Add(this.cboxUADelFB2Files);
			this.gboxUAOptions.Controls.Add(this.pUnbPackFiles);
			this.gboxUAOptions.Controls.Add(this.pUAToAnotherDir);
			this.gboxUAOptions.Controls.Add(this.rbtnUAToAnotherDir);
			this.gboxUAOptions.Controls.Add(this.rbtnUAToSomeDir);
			this.gboxUAOptions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gboxUAOptions.Location = new System.Drawing.Point(0, 0);
			this.gboxUAOptions.Name = "gboxUAOptions";
			this.gboxUAOptions.Size = new System.Drawing.Size(754, 384);
			this.gboxUAOptions.TabIndex = 1;
			this.gboxUAOptions.TabStop = false;
			this.gboxUAOptions.Text = " Настройки ";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.gboxUACount);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(3, 151);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(748, 230);
			this.panel2.TabIndex = 7;
			// 
			// gboxUACount
			// 
			this.gboxUACount.Controls.Add(this.chBoxViewProgressU);
			this.gboxUACount.Controls.Add(this.lvUAGeneralCount);
			this.gboxUACount.Controls.Add(this.lvUACount);
			this.gboxUACount.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gboxUACount.Location = new System.Drawing.Point(0, 0);
			this.gboxUACount.Name = "gboxUACount";
			this.gboxUACount.Size = new System.Drawing.Size(748, 230);
			this.gboxUACount.TabIndex = 8;
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
			this.lvUAGeneralCount.FullRowSelect = true;
			this.lvUAGeneralCount.GridLines = true;
			this.lvUAGeneralCount.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
									listViewItem5,
									listViewItem6,
									listViewItem7,
									listViewItem8,
									listViewItem9});
			this.lvUAGeneralCount.Location = new System.Drawing.Point(0, 46);
			this.lvUAGeneralCount.Name = "lvUAGeneralCount";
			this.lvUAGeneralCount.Size = new System.Drawing.Size(392, 181);
			this.lvUAGeneralCount.TabIndex = 1;
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
			// lvUACount
			// 
			this.lvUACount.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lvUACount.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.cHeaderArchive,
									this.cHeaderArchiveCount});
			this.lvUACount.FullRowSelect = true;
			this.lvUACount.GridLines = true;
			listViewGroup1.Header = "Архивы";
			listViewGroup1.Name = "lvgArchive";
			this.lvUACount.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
									listViewGroup1});
			listViewItem10.Group = listViewGroup1;
			listViewItem10.StateImageIndex = 0;
			listViewItem11.Group = listViewGroup1;
			listViewItem12.Group = listViewGroup1;
			listViewItem13.Group = listViewGroup1;
			listViewItem14.Group = listViewGroup1;
			listViewItem15.Group = listViewGroup1;
			this.lvUACount.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
									listViewItem10,
									listViewItem11,
									listViewItem12,
									listViewItem13,
									listViewItem14,
									listViewItem15});
			this.lvUACount.Location = new System.Drawing.Point(398, 46);
			this.lvUACount.Name = "lvUACount";
			this.lvUACount.Size = new System.Drawing.Size(347, 181);
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
			this.cboxUADelFB2Files.Location = new System.Drawing.Point(3, 127);
			this.cboxUADelFB2Files.Name = "cboxUADelFB2Files";
			this.cboxUADelFB2Files.Size = new System.Drawing.Size(748, 24);
			this.cboxUADelFB2Files.TabIndex = 6;
			this.cboxUADelFB2Files.Text = " Удалить архивы после распаковки";
			this.cboxUADelFB2Files.UseVisualStyleBackColor = true;
			// 
			// pUnbPackFiles
			// 
			this.pUnbPackFiles.Controls.Add(this.cboxPriorityU);
			this.pUnbPackFiles.Controls.Add(this.lblPriorityU);
			this.pUnbPackFiles.Controls.Add(this.rbAnyU);
			this.pUnbPackFiles.Controls.Add(this.rbFB2U);
			this.pUnbPackFiles.Controls.Add(this.lblFilesUType);
			this.pUnbPackFiles.Dock = System.Windows.Forms.DockStyle.Top;
			this.pUnbPackFiles.Location = new System.Drawing.Point(3, 97);
			this.pUnbPackFiles.Name = "pUnbPackFiles";
			this.pUnbPackFiles.Size = new System.Drawing.Size(748, 30);
			this.pUnbPackFiles.TabIndex = 5;
			// 
			// cboxPriorityU
			// 
			this.cboxPriorityU.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxPriorityU.FormattingEnabled = true;
			this.cboxPriorityU.Items.AddRange(new object[] {
									"Низкий (Idle)",
									"Ниже Среднего",
									"Средний",
									"Выше Среднего",
									"Высокий",
									"Реального времени (RealTime)"});
			this.cboxPriorityU.Location = new System.Drawing.Point(409, 5);
			this.cboxPriorityU.Name = "cboxPriorityU";
			this.cboxPriorityU.Size = new System.Drawing.Size(328, 21);
			this.cboxPriorityU.TabIndex = 28;
			// 
			// lblPriorityU
			// 
			this.lblPriorityU.AutoSize = true;
			this.lblPriorityU.Location = new System.Drawing.Point(331, 9);
			this.lblPriorityU.Name = "lblPriorityU";
			this.lblPriorityU.Size = new System.Drawing.Size(72, 13);
			this.lblPriorityU.TabIndex = 27;
			this.lblPriorityU.Text = "Приоритет:";
			// 
			// rbAnyU
			// 
			this.rbAnyU.Location = new System.Drawing.Point(220, 8);
			this.rbAnyU.Name = "rbAnyU";
			this.rbAnyU.Size = new System.Drawing.Size(76, 18);
			this.rbAnyU.TabIndex = 26;
			this.rbAnyU.Text = "Любые";
			this.rbAnyU.UseVisualStyleBackColor = true;
			// 
			// rbFB2U
			// 
			this.rbFB2U.Checked = true;
			this.rbFB2U.Location = new System.Drawing.Point(127, 8);
			this.rbFB2U.Name = "rbFB2U";
			this.rbFB2U.Size = new System.Drawing.Size(93, 18);
			this.rbFB2U.TabIndex = 25;
			this.rbFB2U.TabStop = true;
			this.rbFB2U.Text = "Только fb2";
			this.rbFB2U.UseVisualStyleBackColor = true;
			this.rbFB2U.CheckedChanged += new System.EventHandler(this.RbFB2UCheckedChanged);
			// 
			// lblFilesUType
			// 
			this.lblFilesUType.AutoSize = true;
			this.lblFilesUType.Location = new System.Drawing.Point(5, 9);
			this.lblFilesUType.Name = "lblFilesUType";
			this.lblFilesUType.Size = new System.Drawing.Size(115, 13);
			this.lblFilesUType.TabIndex = 24;
			this.lblFilesUType.Text = "Извлекать Файлы:";
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
			this.btnUAToAnotherDir.Enabled = false;
			this.btnUAToAnotherDir.Image = ((System.Drawing.Image)(resources.GetObject("btnUAToAnotherDir.Image")));
			this.btnUAToAnotherDir.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnUAToAnotherDir.Location = new System.Drawing.Point(22, 3);
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
			this.tboxUAToAnotherDir.Location = new System.Drawing.Point(65, 5);
			this.tboxUAToAnotherDir.Name = "tboxUAToAnotherDir";
			this.tboxUAToAnotherDir.ReadOnly = true;
			this.tboxUAToAnotherDir.Size = new System.Drawing.Size(672, 20);
			this.tboxUAToAnotherDir.TabIndex = 6;
			this.tboxUAToAnotherDir.TextChanged += new System.EventHandler(this.TboxUAToAnotherDirTextChanged);
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
			this.pUAType.Controls.Add(this.chBoxAddFileNameBookID);
			this.pUAType.Controls.Add(this.cboxUAExistArchive);
			this.pUAType.Controls.Add(this.lblUAExistArchive);
			this.pUAType.Controls.Add(this.lblUAType);
			this.pUAType.Controls.Add(this.cboxUAType);
			this.pUAType.Dock = System.Windows.Forms.DockStyle.Top;
			this.pUAType.Location = new System.Drawing.Point(3, 65);
			this.pUAType.Name = "pUAType";
			this.pUAType.Size = new System.Drawing.Size(754, 48);
			this.pUAType.TabIndex = 26;
			// 
			// chBoxAddFileNameBookID
			// 
			this.chBoxAddFileNameBookID.Enabled = false;
			this.chBoxAddFileNameBookID.Location = new System.Drawing.Point(371, 27);
			this.chBoxAddFileNameBookID.Name = "chBoxAddFileNameBookID";
			this.chBoxAddFileNameBookID.Size = new System.Drawing.Size(246, 18);
			this.chBoxAddFileNameBookID.TabIndex = 21;
			this.chBoxAddFileNameBookID.Text = " Добавить ID Книги к имени файла";
			this.chBoxAddFileNameBookID.UseVisualStyleBackColor = true;
			// 
			// cboxUAExistArchive
			// 
			this.cboxUAExistArchive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxUAExistArchive.FormattingEnabled = true;
			this.cboxUAExistArchive.Items.AddRange(new object[] {
									"Заменить существующий fb2-файл создаваемым",
									"Добавить к создаваемому fb2-файлу очередной номер",
									"Добавить к создаваемому fb2-файлу дату и время"});
			this.cboxUAExistArchive.Location = new System.Drawing.Point(371, 3);
			this.cboxUAExistArchive.Name = "cboxUAExistArchive";
			this.cboxUAExistArchive.Size = new System.Drawing.Size(369, 21);
			this.cboxUAExistArchive.TabIndex = 16;
			this.cboxUAExistArchive.SelectedIndexChanged += new System.EventHandler(this.CboxUAExistArchiveSelectedIndexChanged);
			// 
			// lblUAExistArchive
			// 
			this.lblUAExistArchive.AutoSize = true;
			this.lblUAExistArchive.Location = new System.Drawing.Point(215, 7);
			this.lblUAExistArchive.Name = "lblUAExistArchive";
			this.lblUAExistArchive.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblUAExistArchive.Size = new System.Drawing.Size(150, 13);
			this.lblUAExistArchive.TabIndex = 15;
			this.lblUAExistArchive.Text = "Одинаковые fb2-файлы:";
			this.lblUAExistArchive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
			this.pUAScanDir.Controls.Add(this.cboxScanSubDirToUnArchive);
			this.pUAScanDir.Controls.Add(this.tboxUASourceDir);
			this.pUAScanDir.Controls.Add(this.lblUAScanDir);
			this.pUAScanDir.Dock = System.Windows.Forms.DockStyle.Top;
			this.pUAScanDir.Location = new System.Drawing.Point(3, 34);
			this.pUAScanDir.Margin = new System.Windows.Forms.Padding(0);
			this.pUAScanDir.Name = "pUAScanDir";
			this.pUAScanDir.Size = new System.Drawing.Size(754, 31);
			this.pUAScanDir.TabIndex = 23;
			// 
			// cboxScanSubDirToUnArchive
			// 
			this.cboxScanSubDirToUnArchive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cboxScanSubDirToUnArchive.Checked = true;
			this.cboxScanSubDirToUnArchive.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cboxScanSubDirToUnArchive.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.cboxScanSubDirToUnArchive.ForeColor = System.Drawing.Color.Navy;
			this.cboxScanSubDirToUnArchive.Location = new System.Drawing.Point(579, 4);
			this.cboxScanSubDirToUnArchive.Name = "cboxScanSubDirToUnArchive";
			this.cboxScanSubDirToUnArchive.Size = new System.Drawing.Size(172, 24);
			this.cboxScanSubDirToUnArchive.TabIndex = 7;
			this.cboxScanSubDirToUnArchive.Text = "Сканировать и подпапки";
			this.cboxScanSubDirToUnArchive.UseVisualStyleBackColor = true;
			// 
			// tboxUASourceDir
			// 
			this.tboxUASourceDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxUASourceDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tboxUASourceDir.Location = new System.Drawing.Point(162, 5);
			this.tboxUASourceDir.Name = "tboxUASourceDir";
			this.tboxUASourceDir.Size = new System.Drawing.Size(411, 20);
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
									this.tsbtnUAOpenDir,
									this.tsSep2,
									this.tsbtnUAAnalyze,
									this.tsbtnUAAnalyzeStop,
									this.tsSep3,
									this.tsbtnUnArchive,
									this.tsbtnUnArchiveStop});
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
			this.tsbtnUAOpenDir.Size = new System.Drawing.Size(123, 28);
			this.tsbtnUAOpenDir.Text = "Папка - источник";
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
			// tsbtnUAAnalyzeStop
			// 
			this.tsbtnUAAnalyzeStop.AutoToolTip = false;
			this.tsbtnUAAnalyzeStop.Enabled = false;
			this.tsbtnUAAnalyzeStop.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnUAAnalyzeStop.Image")));
			this.tsbtnUAAnalyzeStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnUAAnalyzeStop.Name = "tsbtnUAAnalyzeStop";
			this.tsbtnUAAnalyzeStop.Size = new System.Drawing.Size(96, 28);
			this.tsbtnUAAnalyzeStop.Text = "Остановить";
			this.tsbtnUAAnalyzeStop.Click += new System.EventHandler(this.TsbtnUAAnalyzeStopClick);
			// 
			// tsSep3
			// 
			this.tsSep3.Name = "tsSep3";
			this.tsSep3.Size = new System.Drawing.Size(6, 31);
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
			// chBoxViewProgressA
			// 
			this.chBoxViewProgressA.Checked = true;
			this.chBoxViewProgressA.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chBoxViewProgressA.Dock = System.Windows.Forms.DockStyle.Top;
			this.chBoxViewProgressA.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxViewProgressA.Location = new System.Drawing.Point(3, 16);
			this.chBoxViewProgressA.Name = "chBoxViewProgressA";
			this.chBoxViewProgressA.Size = new System.Drawing.Size(748, 24);
			this.chBoxViewProgressA.TabIndex = 24;
			this.chBoxViewProgressA.Text = "Отображать изменение хода работы";
			this.chBoxViewProgressA.UseVisualStyleBackColor = true;
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
									listViewItem3,
									listViewItem4});
			this.lvGeneralCount.Location = new System.Drawing.Point(3, 40);
			this.lvGeneralCount.Name = "lvGeneralCount";
			this.lvGeneralCount.Size = new System.Drawing.Size(748, 164);
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
			// chBoxViewProgressU
			// 
			this.chBoxViewProgressU.Checked = true;
			this.chBoxViewProgressU.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chBoxViewProgressU.Dock = System.Windows.Forms.DockStyle.Top;
			this.chBoxViewProgressU.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxViewProgressU.Location = new System.Drawing.Point(3, 16);
			this.chBoxViewProgressU.Name = "chBoxViewProgressU";
			this.chBoxViewProgressU.Size = new System.Drawing.Size(742, 24);
			this.chBoxViewProgressU.TabIndex = 25;
			this.chBoxViewProgressU.Text = "Отображать изменение хода работы";
			this.chBoxViewProgressU.UseVisualStyleBackColor = true;
			// 
			// SFBTpArchiveManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
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
			this.pArchiveFiles.ResumeLayout(false);
			this.pArchiveFiles.PerformLayout();
			this.pToAnotherDir.ResumeLayout(false);
			this.pToAnotherDir.PerformLayout();
			this.pType.ResumeLayout(false);
			this.pType.PerformLayout();
			this.tpUnArchive.ResumeLayout(false);
			this.tpUnArchive.PerformLayout();
			this.pUAOptions.ResumeLayout(false);
			this.gboxUAOptions.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.gboxUACount.ResumeLayout(false);
			this.pUnbPackFiles.ResumeLayout(false);
			this.pUnbPackFiles.PerformLayout();
			this.pUAToAnotherDir.ResumeLayout(false);
			this.pUAToAnotherDir.PerformLayout();
			this.pUAType.ResumeLayout(false);
			this.pUAType.PerformLayout();
			this.pUAScanDir.ResumeLayout(false);
			this.pUAScanDir.PerformLayout();
			this.tsUnArchiver.ResumeLayout(false);
			this.tsUnArchiver.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.CheckBox chBoxViewProgressU;
		private System.Windows.Forms.CheckBox chBoxViewProgressA;
		private System.Windows.Forms.Label lblPriorityU;
		private System.Windows.Forms.ComboBox cboxPriorityU;
		private System.Windows.Forms.Label lblPriorityA;
		private System.Windows.Forms.ComboBox cboxPriorityA;
		private System.Windows.Forms.Panel pArchiveFiles;
		private System.Windows.Forms.Panel pUnbPackFiles;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label lblFilesUType;
		private System.Windows.Forms.RadioButton rbFB2U;
		private System.Windows.Forms.RadioButton rbAnyU;
		private System.Windows.Forms.Label lblFilesType;
		private System.Windows.Forms.RadioButton rbFB2;
		private System.Windows.Forms.RadioButton rbAny;
		private System.Windows.Forms.ToolStripButton tsbtnUAAnalyzeStop;
		private System.Windows.Forms.ToolStripButton tsbtnUnArchiveStop;
		private System.Windows.Forms.ToolStripButton tsbtnArchiveStop;
		private System.Windows.Forms.CheckBox chBoxAddFileNameBookID;
		private System.Windows.Forms.CheckBox chBoxAddArchiveNameBookID;
		private System.Windows.Forms.CheckBox cboxScanSubDirToUnArchive;
		private System.Windows.Forms.CheckBox cboxScanSubDirToArchive;
		private System.Windows.Forms.ImageList imgl16;
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
		private System.Windows.Forms.FolderBrowserDialog fbdDir;
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
