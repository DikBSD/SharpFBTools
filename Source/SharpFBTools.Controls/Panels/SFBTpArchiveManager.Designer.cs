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
			this.gboxOptions = new System.Windows.Forms.GroupBox();
			this.gboxRar = new System.Windows.Forms.GroupBox();
			this.lblRarDir = new System.Windows.Forms.Label();
			this.tboxRarDir = new System.Windows.Forms.TextBox();
			this.cboxAddRestoreInfo = new System.Windows.Forms.CheckBox();
			this.btnRarDir = new System.Windows.Forms.Button();
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
			this.pCount = new System.Windows.Forms.Panel();
			this.tlpCount = new System.Windows.Forms.TableLayoutPanel();
			this.lblDirs = new System.Windows.Forms.Label();
			this.lblDirsCount = new System.Windows.Forms.Label();
			this.lblFiles = new System.Windows.Forms.Label();
			this.lblFilesCount = new System.Windows.Forms.Label();
			this.lblFB2Files = new System.Windows.Forms.Label();
			this.lblFB2FilesCount = new System.Windows.Forms.Label();
			this.tpUnArchive = new System.Windows.Forms.TabPage();
			this.tpTest = new System.Windows.Forms.TabPage();
			this.fbdDir = new System.Windows.Forms.FolderBrowserDialog();
			this.tsUnArchiver = new System.Windows.Forms.ToolStrip();
			this.tsbtnUAOpenDir = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnUnArchive = new System.Windows.Forms.ToolStripButton();
			this.pUAScanDir = new System.Windows.Forms.Panel();
			this.tboxUASourceDir = new System.Windows.Forms.TextBox();
			this.lblUAScanDir = new System.Windows.Forms.Label();
			this.pUAType = new System.Windows.Forms.Panel();
			this.cboxUAExistArchive = new System.Windows.Forms.ComboBox();
			this.lblUAExistArchive = new System.Windows.Forms.Label();
			this.lblUAType = new System.Windows.Forms.Label();
			this.cboxUAType = new System.Windows.Forms.ComboBox();
			this.pUnArchiverCount = new System.Windows.Forms.Panel();
			this.tlpUnArchiverCount = new System.Windows.Forms.TableLayoutPanel();
			this.lblUADirs = new System.Windows.Forms.Label();
			this.lblUADirsCount = new System.Windows.Forms.Label();
			this.lblUAFiles = new System.Windows.Forms.Label();
			this.lblUAFilesCount = new System.Windows.Forms.Label();
			this.lblUAFB2Files = new System.Windows.Forms.Label();
			this.lblUAFB2FilesCount = new System.Windows.Forms.Label();
			this.pUAOptions = new System.Windows.Forms.Panel();
			this.gboxUAOptions = new System.Windows.Forms.GroupBox();
			this.cboxUADelFB2Files = new System.Windows.Forms.CheckBox();
			this.pUAToAnotherDir = new System.Windows.Forms.Panel();
			this.btnUAToAnotherDir = new System.Windows.Forms.Button();
			this.tboxUAToAnotherDir = new System.Windows.Forms.TextBox();
			this.rbtnUAToAnotherDir = new System.Windows.Forms.RadioButton();
			this.rbtnUAToSomeDir = new System.Windows.Forms.RadioButton();
			this.btnUARarDir = new System.Windows.Forms.Button();
			this.tboxUARarDir = new System.Windows.Forms.TextBox();
			this.lblUARarDir = new System.Windows.Forms.Label();
			this.gboxUARar = new System.Windows.Forms.GroupBox();
			this.tsArchiver.SuspendLayout();
			this.ssProgress.SuspendLayout();
			this.pScanDir.SuspendLayout();
			this.pCentral.SuspendLayout();
			this.tcArchiver.SuspendLayout();
			this.tpArchive.SuspendLayout();
			this.pOptions.SuspendLayout();
			this.gboxOptions.SuspendLayout();
			this.gboxRar.SuspendLayout();
			this.pToAnotherDir.SuspendLayout();
			this.pType.SuspendLayout();
			this.pCount.SuspendLayout();
			this.tlpCount.SuspendLayout();
			this.tpUnArchive.SuspendLayout();
			this.tsUnArchiver.SuspendLayout();
			this.pUAScanDir.SuspendLayout();
			this.pUAType.SuspendLayout();
			this.pUnArchiverCount.SuspendLayout();
			this.tlpUnArchiverCount.SuspendLayout();
			this.pUAOptions.SuspendLayout();
			this.gboxUAOptions.SuspendLayout();
			this.pUAToAnotherDir.SuspendLayout();
			this.gboxUARar.SuspendLayout();
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
			this.tcArchiver.Controls.Add(this.tpTest);
			this.tcArchiver.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcArchiver.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
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
			this.tpArchive.Controls.Add(this.pCount);
			this.tpArchive.Controls.Add(this.pScanDir);
			this.tpArchive.Controls.Add(this.tsArchiver);
			this.tpArchive.Location = new System.Drawing.Point(4, 22);
			this.tpArchive.Name = "tpArchive";
			this.tpArchive.Padding = new System.Windows.Forms.Padding(3);
			this.tpArchive.Size = new System.Drawing.Size(760, 501);
			this.tpArchive.TabIndex = 0;
			this.tpArchive.Text = " Запаковать ";
			this.tpArchive.UseVisualStyleBackColor = true;
			// 
			// pOptions
			// 
			this.pOptions.Controls.Add(this.gboxOptions);
			this.pOptions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pOptions.Location = new System.Drawing.Point(3, 92);
			this.pOptions.Name = "pOptions";
			this.pOptions.Size = new System.Drawing.Size(754, 388);
			this.pOptions.TabIndex = 26;
			// 
			// gboxOptions
			// 
			this.gboxOptions.Controls.Add(this.gboxRar);
			this.gboxOptions.Controls.Add(this.cboxDelFB2Files);
			this.gboxOptions.Controls.Add(this.pToAnotherDir);
			this.gboxOptions.Controls.Add(this.rbtnToAnotherDir);
			this.gboxOptions.Controls.Add(this.rbtnToSomeDir);
			this.gboxOptions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gboxOptions.Location = new System.Drawing.Point(0, 0);
			this.gboxOptions.Name = "gboxOptions";
			this.gboxOptions.Size = new System.Drawing.Size(754, 388);
			this.gboxOptions.TabIndex = 1;
			this.gboxOptions.TabStop = false;
			this.gboxOptions.Text = " Настройки ";
			// 
			// gboxRar
			// 
			this.gboxRar.Controls.Add(this.lblRarDir);
			this.gboxRar.Controls.Add(this.tboxRarDir);
			this.gboxRar.Controls.Add(this.cboxAddRestoreInfo);
			this.gboxRar.Controls.Add(this.btnRarDir);
			this.gboxRar.Dock = System.Windows.Forms.DockStyle.Top;
			this.gboxRar.Location = new System.Drawing.Point(3, 121);
			this.gboxRar.Name = "gboxRar";
			this.gboxRar.Size = new System.Drawing.Size(748, 72);
			this.gboxRar.TabIndex = 12;
			this.gboxRar.TabStop = false;
			this.gboxRar.Text = " Настройки для Rar-архиватора ";
			this.gboxRar.Visible = false;
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
			this.tboxRarDir.Size = new System.Drawing.Size(509, 20);
			this.tboxRarDir.TabIndex = 8;
			// 
			// cboxAddRestoreInfo
			// 
			this.cboxAddRestoreInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.cboxAddRestoreInfo.Location = new System.Drawing.Point(3, 45);
			this.cboxAddRestoreInfo.Name = "cboxAddRestoreInfo";
			this.cboxAddRestoreInfo.Size = new System.Drawing.Size(742, 24);
			this.cboxAddRestoreInfo.TabIndex = 4;
			this.cboxAddRestoreInfo.Text = " Добавить в архив информацию для его восстановления";
			this.cboxAddRestoreInfo.UseVisualStyleBackColor = true;
			// 
			// btnRarDir
			// 
			this.btnRarDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRarDir.Image = ((System.Drawing.Image)(resources.GetObject("btnRarDir.Image")));
			this.btnRarDir.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnRarDir.Location = new System.Drawing.Point(700, 19);
			this.btnRarDir.Name = "btnRarDir";
			this.btnRarDir.Size = new System.Drawing.Size(37, 24);
			this.btnRarDir.TabIndex = 9;
			this.btnRarDir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnRarDir.UseVisualStyleBackColor = true;
			this.btnRarDir.Click += new System.EventHandler(this.BtnRarDirClick);
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
			// pCount
			// 
			this.pCount.Controls.Add(this.tlpCount);
			this.pCount.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pCount.Location = new System.Drawing.Point(3, 480);
			this.pCount.Margin = new System.Windows.Forms.Padding(0);
			this.pCount.Name = "pCount";
			this.pCount.Size = new System.Drawing.Size(754, 18);
			this.pCount.TabIndex = 21;
			// 
			// tlpCount
			// 
			this.tlpCount.AutoSize = true;
			this.tlpCount.ColumnCount = 6;
			this.tlpCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpCount.Controls.Add(this.lblDirs, 0, 0);
			this.tlpCount.Controls.Add(this.lblDirsCount, 1, 0);
			this.tlpCount.Controls.Add(this.lblFiles, 2, 0);
			this.tlpCount.Controls.Add(this.lblFilesCount, 3, 0);
			this.tlpCount.Controls.Add(this.lblFB2Files, 4, 0);
			this.tlpCount.Controls.Add(this.lblFB2FilesCount, 5, 0);
			this.tlpCount.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpCount.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.tlpCount.Location = new System.Drawing.Point(0, 0);
			this.tlpCount.Margin = new System.Windows.Forms.Padding(0);
			this.tlpCount.Name = "tlpCount";
			this.tlpCount.RowCount = 1;
			this.tlpCount.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpCount.Size = new System.Drawing.Size(754, 18);
			this.tlpCount.TabIndex = 15;
			// 
			// lblDirs
			// 
			this.lblDirs.AutoSize = true;
			this.lblDirs.Location = new System.Drawing.Point(3, 0);
			this.lblDirs.Name = "lblDirs";
			this.lblDirs.Size = new System.Drawing.Size(81, 13);
			this.lblDirs.TabIndex = 0;
			this.lblDirs.Text = "Всего папок:";
			// 
			// lblDirsCount
			// 
			this.lblDirsCount.AutoSize = true;
			this.lblDirsCount.Location = new System.Drawing.Point(90, 0);
			this.lblDirsCount.Name = "lblDirsCount";
			this.lblDirsCount.Size = new System.Drawing.Size(14, 13);
			this.lblDirsCount.TabIndex = 1;
			this.lblDirsCount.Text = "0";
			// 
			// lblFiles
			// 
			this.lblFiles.AutoSize = true;
			this.lblFiles.Location = new System.Drawing.Point(110, 0);
			this.lblFiles.Name = "lblFiles";
			this.lblFiles.Size = new System.Drawing.Size(89, 13);
			this.lblFiles.TabIndex = 2;
			this.lblFiles.Text = "Всего файлов:";
			// 
			// lblFilesCount
			// 
			this.lblFilesCount.AutoSize = true;
			this.lblFilesCount.Location = new System.Drawing.Point(205, 0);
			this.lblFilesCount.Name = "lblFilesCount";
			this.lblFilesCount.Size = new System.Drawing.Size(14, 13);
			this.lblFilesCount.TabIndex = 3;
			this.lblFilesCount.Text = "0";
			// 
			// lblFB2Files
			// 
			this.lblFB2Files.AutoSize = true;
			this.lblFB2Files.Location = new System.Drawing.Point(225, 0);
			this.lblFB2Files.Name = "lblFB2Files";
			this.lblFB2Files.Size = new System.Drawing.Size(76, 13);
			this.lblFB2Files.TabIndex = 4;
			this.lblFB2Files.Text = "fb2-файлов:";
			// 
			// lblFB2FilesCount
			// 
			this.lblFB2FilesCount.AutoSize = true;
			this.lblFB2FilesCount.Location = new System.Drawing.Point(307, 0);
			this.lblFB2FilesCount.Name = "lblFB2FilesCount";
			this.lblFB2FilesCount.Size = new System.Drawing.Size(14, 13);
			this.lblFB2FilesCount.TabIndex = 5;
			this.lblFB2FilesCount.Text = "0";
			// 
			// tpUnArchive
			// 
			this.tpUnArchive.Controls.Add(this.pUAOptions);
			this.tpUnArchive.Controls.Add(this.pUnArchiverCount);
			this.tpUnArchive.Controls.Add(this.pUAType);
			this.tpUnArchive.Controls.Add(this.pUAScanDir);
			this.tpUnArchive.Controls.Add(this.tsUnArchiver);
			this.tpUnArchive.Location = new System.Drawing.Point(4, 22);
			this.tpUnArchive.Name = "tpUnArchive";
			this.tpUnArchive.Padding = new System.Windows.Forms.Padding(3);
			this.tpUnArchive.Size = new System.Drawing.Size(760, 501);
			this.tpUnArchive.TabIndex = 1;
			this.tpUnArchive.Text = " Распаковать ";
			this.tpUnArchive.UseVisualStyleBackColor = true;
			// 
			// tpTest
			// 
			this.tpTest.Location = new System.Drawing.Point(4, 22);
			this.tpTest.Name = "tpTest";
			this.tpTest.Size = new System.Drawing.Size(760, 501);
			this.tpTest.TabIndex = 2;
			this.tpTest.Text = " Тестировать архивы ";
			this.tpTest.UseVisualStyleBackColor = true;
			// 
			// tsUnArchiver
			// 
			this.tsUnArchiver.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tsUnArchiver.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsbtnUAOpenDir,
									this.toolStripSeparator1,
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
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			// 
			// tsbtnUnArchive
			// 
			this.tsbtnUnArchive.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnUnArchive.Image")));
			this.tsbtnUnArchive.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnUnArchive.Name = "tsbtnUnArchive";
			this.tsbtnUnArchive.Size = new System.Drawing.Size(100, 28);
			this.tsbtnUnArchive.Text = "Распаковать";
			this.tsbtnUnArchive.ToolTipText = "Рапаковать архивы";
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
			// pUnArchiverCount
			// 
			this.pUnArchiverCount.Controls.Add(this.tlpUnArchiverCount);
			this.pUnArchiverCount.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pUnArchiverCount.Location = new System.Drawing.Point(3, 480);
			this.pUnArchiverCount.Margin = new System.Windows.Forms.Padding(0);
			this.pUnArchiverCount.Name = "pUnArchiverCount";
			this.pUnArchiverCount.Size = new System.Drawing.Size(754, 18);
			this.pUnArchiverCount.TabIndex = 27;
			// 
			// tlpUnArchiverCount
			// 
			this.tlpUnArchiverCount.AutoSize = true;
			this.tlpUnArchiverCount.ColumnCount = 6;
			this.tlpUnArchiverCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpUnArchiverCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpUnArchiverCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpUnArchiverCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpUnArchiverCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpUnArchiverCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpUnArchiverCount.Controls.Add(this.lblUADirs, 0, 0);
			this.tlpUnArchiverCount.Controls.Add(this.lblUADirsCount, 1, 0);
			this.tlpUnArchiverCount.Controls.Add(this.lblUAFiles, 2, 0);
			this.tlpUnArchiverCount.Controls.Add(this.lblUAFilesCount, 3, 0);
			this.tlpUnArchiverCount.Controls.Add(this.lblUAFB2Files, 4, 0);
			this.tlpUnArchiverCount.Controls.Add(this.lblUAFB2FilesCount, 5, 0);
			this.tlpUnArchiverCount.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpUnArchiverCount.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.tlpUnArchiverCount.Location = new System.Drawing.Point(0, 0);
			this.tlpUnArchiverCount.Margin = new System.Windows.Forms.Padding(0);
			this.tlpUnArchiverCount.Name = "tlpUnArchiverCount";
			this.tlpUnArchiverCount.RowCount = 1;
			this.tlpUnArchiverCount.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpUnArchiverCount.Size = new System.Drawing.Size(754, 18);
			this.tlpUnArchiverCount.TabIndex = 15;
			// 
			// lblUADirs
			// 
			this.lblUADirs.AutoSize = true;
			this.lblUADirs.Location = new System.Drawing.Point(3, 0);
			this.lblUADirs.Name = "lblUADirs";
			this.lblUADirs.Size = new System.Drawing.Size(81, 13);
			this.lblUADirs.TabIndex = 0;
			this.lblUADirs.Text = "Всего папок:";
			// 
			// lblUADirsCount
			// 
			this.lblUADirsCount.AutoSize = true;
			this.lblUADirsCount.Location = new System.Drawing.Point(90, 0);
			this.lblUADirsCount.Name = "lblUADirsCount";
			this.lblUADirsCount.Size = new System.Drawing.Size(14, 13);
			this.lblUADirsCount.TabIndex = 1;
			this.lblUADirsCount.Text = "0";
			// 
			// lblUAFiles
			// 
			this.lblUAFiles.AutoSize = true;
			this.lblUAFiles.Location = new System.Drawing.Point(110, 0);
			this.lblUAFiles.Name = "lblUAFiles";
			this.lblUAFiles.Size = new System.Drawing.Size(89, 13);
			this.lblUAFiles.TabIndex = 2;
			this.lblUAFiles.Text = "Всего файлов:";
			// 
			// lblUAFilesCount
			// 
			this.lblUAFilesCount.AutoSize = true;
			this.lblUAFilesCount.Location = new System.Drawing.Point(205, 0);
			this.lblUAFilesCount.Name = "lblUAFilesCount";
			this.lblUAFilesCount.Size = new System.Drawing.Size(14, 13);
			this.lblUAFilesCount.TabIndex = 3;
			this.lblUAFilesCount.Text = "0";
			// 
			// lblUAFB2Files
			// 
			this.lblUAFB2Files.AutoSize = true;
			this.lblUAFB2Files.Location = new System.Drawing.Point(225, 0);
			this.lblUAFB2Files.Name = "lblUAFB2Files";
			this.lblUAFB2Files.Size = new System.Drawing.Size(76, 13);
			this.lblUAFB2Files.TabIndex = 4;
			this.lblUAFB2Files.Text = "fb2-файлов:";
			// 
			// lblUAFB2FilesCount
			// 
			this.lblUAFB2FilesCount.AutoSize = true;
			this.lblUAFB2FilesCount.Location = new System.Drawing.Point(307, 0);
			this.lblUAFB2FilesCount.Name = "lblUAFB2FilesCount";
			this.lblUAFB2FilesCount.Size = new System.Drawing.Size(14, 13);
			this.lblUAFB2FilesCount.TabIndex = 5;
			this.lblUAFB2FilesCount.Text = "0";
			// 
			// pUAOptions
			// 
			this.pUAOptions.Controls.Add(this.gboxUAOptions);
			this.pUAOptions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pUAOptions.Location = new System.Drawing.Point(3, 92);
			this.pUAOptions.Name = "pUAOptions";
			this.pUAOptions.Size = new System.Drawing.Size(754, 388);
			this.pUAOptions.TabIndex = 28;
			// 
			// gboxUAOptions
			// 
			this.gboxUAOptions.Controls.Add(this.gboxUARar);
			this.gboxUAOptions.Controls.Add(this.cboxUADelFB2Files);
			this.gboxUAOptions.Controls.Add(this.pUAToAnotherDir);
			this.gboxUAOptions.Controls.Add(this.rbtnUAToAnotherDir);
			this.gboxUAOptions.Controls.Add(this.rbtnUAToSomeDir);
			this.gboxUAOptions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gboxUAOptions.Location = new System.Drawing.Point(0, 0);
			this.gboxUAOptions.Name = "gboxUAOptions";
			this.gboxUAOptions.Size = new System.Drawing.Size(754, 388);
			this.gboxUAOptions.TabIndex = 1;
			this.gboxUAOptions.TabStop = false;
			this.gboxUAOptions.Text = " Настройки ";
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
			// btnUARarDir
			// 
			this.btnUARarDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnUARarDir.Image = ((System.Drawing.Image)(resources.GetObject("btnUARarDir.Image")));
			this.btnUARarDir.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnUARarDir.Location = new System.Drawing.Point(700, 19);
			this.btnUARarDir.Name = "btnUARarDir";
			this.btnUARarDir.Size = new System.Drawing.Size(37, 24);
			this.btnUARarDir.TabIndex = 9;
			this.btnUARarDir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnUARarDir.UseVisualStyleBackColor = true;
			// 
			// tboxUARarDir
			// 
			this.tboxUARarDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxUARarDir.Location = new System.Drawing.Point(185, 21);
			this.tboxUARarDir.Name = "tboxUARarDir";
			this.tboxUARarDir.ReadOnly = true;
			this.tboxUARarDir.Size = new System.Drawing.Size(509, 20);
			this.tboxUARarDir.TabIndex = 8;
			// 
			// lblUARarDir
			// 
			this.lblUARarDir.AutoSize = true;
			this.lblUARarDir.Location = new System.Drawing.Point(4, 25);
			this.lblUARarDir.Name = "lblUARarDir";
			this.lblUARarDir.Size = new System.Drawing.Size(175, 13);
			this.lblUARarDir.TabIndex = 10;
			this.lblUARarDir.Text = "Папка с установленным Rar:";
			// 
			// gboxUARar
			// 
			this.gboxUARar.Controls.Add(this.lblUARarDir);
			this.gboxUARar.Controls.Add(this.tboxUARarDir);
			this.gboxUARar.Controls.Add(this.btnUARarDir);
			this.gboxUARar.Dock = System.Windows.Forms.DockStyle.Top;
			this.gboxUARar.Location = new System.Drawing.Point(3, 121);
			this.gboxUARar.Name = "gboxUARar";
			this.gboxUARar.Size = new System.Drawing.Size(748, 58);
			this.gboxUARar.TabIndex = 12;
			this.gboxUARar.TabStop = false;
			this.gboxUARar.Text = " Настройки для Rar-архиватора ";
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
			this.gboxOptions.ResumeLayout(false);
			this.gboxRar.ResumeLayout(false);
			this.gboxRar.PerformLayout();
			this.pToAnotherDir.ResumeLayout(false);
			this.pToAnotherDir.PerformLayout();
			this.pType.ResumeLayout(false);
			this.pType.PerformLayout();
			this.pCount.ResumeLayout(false);
			this.pCount.PerformLayout();
			this.tlpCount.ResumeLayout(false);
			this.tlpCount.PerformLayout();
			this.tpUnArchive.ResumeLayout(false);
			this.tpUnArchive.PerformLayout();
			this.tsUnArchiver.ResumeLayout(false);
			this.tsUnArchiver.PerformLayout();
			this.pUAScanDir.ResumeLayout(false);
			this.pUAScanDir.PerformLayout();
			this.pUAType.ResumeLayout(false);
			this.pUAType.PerformLayout();
			this.pUnArchiverCount.ResumeLayout(false);
			this.pUnArchiverCount.PerformLayout();
			this.tlpUnArchiverCount.ResumeLayout(false);
			this.tlpUnArchiverCount.PerformLayout();
			this.pUAOptions.ResumeLayout(false);
			this.gboxUAOptions.ResumeLayout(false);
			this.pUAToAnotherDir.ResumeLayout(false);
			this.pUAToAnotherDir.PerformLayout();
			this.gboxUARar.ResumeLayout(false);
			this.gboxUARar.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.RadioButton rbtnUAToSomeDir;
		private System.Windows.Forms.RadioButton rbtnUAToAnotherDir;
		private System.Windows.Forms.TextBox tboxUAToAnotherDir;
		private System.Windows.Forms.Button btnUAToAnotherDir;
		private System.Windows.Forms.Panel pUAToAnotherDir;
		private System.Windows.Forms.CheckBox cboxUADelFB2Files;
		private System.Windows.Forms.Button btnUARarDir;
		private System.Windows.Forms.TextBox tboxUARarDir;
		private System.Windows.Forms.Label lblUARarDir;
		private System.Windows.Forms.GroupBox gboxUARar;
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
		private System.Windows.Forms.Label lblUAFB2FilesCount;
		private System.Windows.Forms.Label lblUAFB2Files;
		private System.Windows.Forms.Label lblUAFilesCount;
		private System.Windows.Forms.Label lblUAFiles;
		private System.Windows.Forms.Label lblUADirsCount;
		private System.Windows.Forms.Label lblUADirs;
		private System.Windows.Forms.TableLayoutPanel tlpUnArchiverCount;
		private System.Windows.Forms.Panel pUnArchiverCount;
		private System.Windows.Forms.Label lblArchiveType;
		private System.Windows.Forms.ToolStripButton tsbtnUnArchive;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStrip tsUnArchiver;
		private System.Windows.Forms.ToolStrip tsArchiver;
		private System.Windows.Forms.ComboBox cboxExistArchive;
		private System.Windows.Forms.Label lblExistArchive;
		private System.Windows.Forms.ComboBox cboxArchiveType;
		private System.Windows.Forms.Label lblFB2FilesCount;
		private System.Windows.Forms.Label lblFB2Files;
		private System.Windows.Forms.Label lblRarDir;
		private System.Windows.Forms.GroupBox gboxRar;
		private System.Windows.Forms.FolderBrowserDialog fbdDir;
		private System.Windows.Forms.TextBox tboxRarDir;
		private System.Windows.Forms.Button btnRarDir;
		private System.Windows.Forms.ToolStripButton tsbtnArchive;
		private System.Windows.Forms.CheckBox cboxAddRestoreInfo;
		private System.Windows.Forms.CheckBox cboxDelFB2Files;
		private System.Windows.Forms.TextBox tboxToAnotherDir;
		private System.Windows.Forms.Button btnToAnotherDir;
		private System.Windows.Forms.Panel pToAnotherDir;
		private System.Windows.Forms.RadioButton rbtnToSomeDir;
		private System.Windows.Forms.RadioButton rbtnToAnotherDir;
		private System.Windows.Forms.Label lblFilesCount;
		private System.Windows.Forms.Label lblFiles;
		private System.Windows.Forms.Label lblDirsCount;
		private System.Windows.Forms.Label lblDirs;
		private System.Windows.Forms.TableLayoutPanel tlpCount;
		private System.Windows.Forms.Panel pCount;
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
