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
			this.tsValidator = new System.Windows.Forms.ToolStrip();
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
			this.lbArchivelType = new System.Windows.Forms.Label();
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
			this.tsValidator.SuspendLayout();
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
			this.SuspendLayout();
			// 
			// tsValidator
			// 
			this.tsValidator.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tsValidator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsbtnOpenDir,
									this.tsSep1,
									this.tsbtnArchive});
			this.tsValidator.Location = new System.Drawing.Point(3, 3);
			this.tsValidator.Name = "tsValidator";
			this.tsValidator.Size = new System.Drawing.Size(754, 31);
			this.tsValidator.TabIndex = 2;
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
			this.tpArchive.Controls.Add(this.tsValidator);
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
			this.gboxOptions.Text = " Опции ";
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
			this.gboxRar.Text = " Опции для Rar-архиватора ";
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
			this.pType.Controls.Add(this.lbArchivelType);
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
			// lbArchivelType
			// 
			this.lbArchivelType.AutoSize = true;
			this.lbArchivelType.Location = new System.Drawing.Point(3, 6);
			this.lbArchivelType.Name = "lbArchivelType";
			this.lbArchivelType.Size = new System.Drawing.Size(90, 13);
			this.lbArchivelType.TabIndex = 14;
			this.lbArchivelType.Text = "Вид упаковки:";
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
			// SFBTpArchiveManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pCentral);
			this.Controls.Add(this.ssProgress);
			this.Name = "SFBTpArchiveManager";
			this.Size = new System.Drawing.Size(768, 549);
			this.tsValidator.ResumeLayout(false);
			this.tsValidator.PerformLayout();
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
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ComboBox cboxExistArchive;
		private System.Windows.Forms.Label lblExistArchive;
		private System.Windows.Forms.Label lbArchivelType;
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
		private System.Windows.Forms.ToolStrip tsValidator;
	}
}
