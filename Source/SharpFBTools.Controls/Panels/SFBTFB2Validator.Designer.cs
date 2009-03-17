/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 13.03.2009
 * Time: 14:34
 * 
 * License: GPL 2.1
 */
namespace SharpFBTools.Controls.Panels
{
	partial class SFBTpFB2Validator
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SFBTpFB2Validator));
			this.tsValidator = new System.Windows.Forms.ToolStrip();
			this.tsbtnOpenDir = new System.Windows.Forms.ToolStripButton();
			this.tsSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.tSBValidate = new System.Windows.Forms.ToolStripButton();
			this.tsSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnCopyFilesTo = new System.Windows.Forms.ToolStripButton();
			this.tsbtnMoveFilesTo = new System.Windows.Forms.ToolStripButton();
			this.tsbtnDeleteFiles = new System.Windows.Forms.ToolStripButton();
			this.tsSep3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsddbtnMakeReport = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsmiReportAsHTML = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiReportAsFB2 = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiReportAsCSV_CSV = new System.Windows.Forms.ToolStripMenuItem();
			this.pScanDir = new System.Windows.Forms.Panel();
			this.tboxSourceDir = new System.Windows.Forms.TextBox();
			this.lblDir = new System.Windows.Forms.Label();
			this.tlpCount = new System.Windows.Forms.TableLayoutPanel();
			this.lblDirs = new System.Windows.Forms.Label();
			this.lblDirsCount = new System.Windows.Forms.Label();
			this.lblFiles = new System.Windows.Forms.Label();
			this.lblFilesCount = new System.Windows.Forms.Label();
			this.lblFB2Files = new System.Windows.Forms.Label();
			this.lblFB2FilesCount = new System.Windows.Forms.Label();
			this.lblFB2ZipFiles = new System.Windows.Forms.Label();
			this.lblFB2ZipFilesCount = new System.Windows.Forms.Label();
			this.lblFB2RarFiles = new System.Windows.Forms.Label();
			this.lblFB2RarFilesCount = new System.Windows.Forms.Label();
			this.lblNotFB2Files = new System.Windows.Forms.Label();
			this.lblNotFB2FilesCount = new System.Windows.Forms.Label();
			this.tcResult = new System.Windows.Forms.TabControl();
			this.tpNonValid = new System.Windows.Forms.TabPage();
			this.gbFB2NotValidFiles = new System.Windows.Forms.GroupBox();
			this.lblFB2NotValidFilesMoveDir = new System.Windows.Forms.Label();
			this.btnFB2NotValidMoveTo = new System.Windows.Forms.Button();
			this.tboxFB2NotValidDirMoveTo = new System.Windows.Forms.TextBox();
			this.lblFB2NotValidFilesCopyDir = new System.Windows.Forms.Label();
			this.btnFB2NotValidCopyTo = new System.Windows.Forms.Button();
			this.tboxFB2NotValidDirCopyTo = new System.Windows.Forms.TextBox();
			this.pErrors = new System.Windows.Forms.Panel();
			this.rеboxNotValid = new System.Windows.Forms.RichTextBox();
			this.listViewNotValid = new System.Windows.Forms.ListView();
			this.chNonValidFile = new System.Windows.Forms.ColumnHeader();
			this.chNonValidError = new System.Windows.Forms.ColumnHeader();
			this.chNonValidLenght = new System.Windows.Forms.ColumnHeader();
			this.tpValid = new System.Windows.Forms.TabPage();
			this.pValidLV = new System.Windows.Forms.Panel();
			this.listViewValid = new System.Windows.Forms.ListView();
			this.chValidFile = new System.Windows.Forms.ColumnHeader();
			this.chValidLenght = new System.Windows.Forms.ColumnHeader();
			this.gbFB2Valid = new System.Windows.Forms.GroupBox();
			this.lblFB2ValidFilesMoveDir = new System.Windows.Forms.Label();
			this.btnFB2ValidMoveTo = new System.Windows.Forms.Button();
			this.tboxFB2ValidDirMoveTo = new System.Windows.Forms.TextBox();
			this.lblFB2ValidFilesCopyDir = new System.Windows.Forms.Label();
			this.btnFB2ValidCopyTo = new System.Windows.Forms.Button();
			this.tboxFB2ValidDirCopyTo = new System.Windows.Forms.TextBox();
			this.tpNotFB2Files = new System.Windows.Forms.TabPage();
			this.pNotValidLV = new System.Windows.Forms.Panel();
			this.listViewNotFB2 = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.gbNotFB2 = new System.Windows.Forms.GroupBox();
			this.lblNotFB2FilesMoveDir = new System.Windows.Forms.Label();
			this.btnNotFB2MoveTo = new System.Windows.Forms.Button();
			this.tboxNotFB2DirMoveTo = new System.Windows.Forms.TextBox();
			this.lblNotFB2FilesCopyDir = new System.Windows.Forms.Label();
			this.btnNotFB2CopyTo = new System.Windows.Forms.Button();
			this.tboxNotFB2DirCopyTo = new System.Windows.Forms.TextBox();
			this.ssProgress = new System.Windows.Forms.StatusStrip();
			this.tsslblProgress = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.tlCentral = new System.Windows.Forms.TableLayoutPanel();
			this.pCount = new System.Windows.Forms.Panel();
			this.fbdNotValidFB2 = new System.Windows.Forms.FolderBrowserDialog();
			this.fbdValidFB2 = new System.Windows.Forms.FolderBrowserDialog();
			this.fbdNotFB2 = new System.Windows.Forms.FolderBrowserDialog();
			this.fbdSource = new System.Windows.Forms.FolderBrowserDialog();
			this.sfdReport = new System.Windows.Forms.SaveFileDialog();
			this.tsValidator.SuspendLayout();
			this.pScanDir.SuspendLayout();
			this.tlpCount.SuspendLayout();
			this.tcResult.SuspendLayout();
			this.tpNonValid.SuspendLayout();
			this.gbFB2NotValidFiles.SuspendLayout();
			this.pErrors.SuspendLayout();
			this.tpValid.SuspendLayout();
			this.pValidLV.SuspendLayout();
			this.gbFB2Valid.SuspendLayout();
			this.tpNotFB2Files.SuspendLayout();
			this.pNotValidLV.SuspendLayout();
			this.gbNotFB2.SuspendLayout();
			this.ssProgress.SuspendLayout();
			this.tlCentral.SuspendLayout();
			this.pCount.SuspendLayout();
			this.SuspendLayout();
			// 
			// tsValidator
			// 
			this.tsValidator.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tsValidator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsbtnOpenDir,
									this.tsSep1,
									this.tSBValidate,
									this.tsSep2,
									this.tsbtnCopyFilesTo,
									this.tsbtnMoveFilesTo,
									this.tsbtnDeleteFiles,
									this.tsSep3,
									this.tsddbtnMakeReport});
			this.tsValidator.Location = new System.Drawing.Point(0, 0);
			this.tsValidator.Name = "tsValidator";
			this.tsValidator.Size = new System.Drawing.Size(726, 31);
			this.tsValidator.TabIndex = 0;
			// 
			// tsbtnOpenDir
			// 
			this.tsbtnOpenDir.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnOpenDir.Image")));
			this.tsbtnOpenDir.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnOpenDir.Name = "tsbtnOpenDir";
			this.tsbtnOpenDir.Size = new System.Drawing.Size(114, 28);
			this.tsbtnOpenDir.Text = "Открыть папку";
			this.tsbtnOpenDir.ToolTipText = "Открыть папку с fb2, fb2.zip, fb2.rar, zip или rar файлами...";
			this.tsbtnOpenDir.Click += new System.EventHandler(this.TsbtnOpenDirClick);
			// 
			// tsSep1
			// 
			this.tsSep1.Name = "tsSep1";
			this.tsSep1.Size = new System.Drawing.Size(6, 31);
			// 
			// tSBValidate
			// 
			this.tSBValidate.Image = ((System.Drawing.Image)(resources.GetObject("tSBValidate.Image")));
			this.tSBValidate.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tSBValidate.Name = "tSBValidate";
			this.tSBValidate.Size = new System.Drawing.Size(90, 28);
			this.tSBValidate.Text = "Валидация";
			this.tSBValidate.ToolTipText = "Проверить fb2-файлы на соответствие схеме (валидация)";
			this.tSBValidate.Click += new System.EventHandler(this.TSBValidateClick);
			// 
			// tsSep2
			// 
			this.tsSep2.Name = "tsSep2";
			this.tsSep2.Size = new System.Drawing.Size(6, 31);
			// 
			// tsbtnCopyFilesTo
			// 
			this.tsbtnCopyFilesTo.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnCopyFilesTo.Image")));
			this.tsbtnCopyFilesTo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnCopyFilesTo.Name = "tsbtnCopyFilesTo";
			this.tsbtnCopyFilesTo.Size = new System.Drawing.Size(96, 28);
			this.tsbtnCopyFilesTo.Text = "Копировать";
			this.tsbtnCopyFilesTo.ToolTipText = "Копировать файлы (для выбранной вкладки)";
			this.tsbtnCopyFilesTo.Click += new System.EventHandler(this.TsbtnCopyFilesToClick);
			// 
			// tsbtnMoveFilesTo
			// 
			this.tsbtnMoveFilesTo.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnMoveFilesTo.Image")));
			this.tsbtnMoveFilesTo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnMoveFilesTo.Name = "tsbtnMoveFilesTo";
			this.tsbtnMoveFilesTo.Size = new System.Drawing.Size(101, 28);
			this.tsbtnMoveFilesTo.Text = "Переместить";
			this.tsbtnMoveFilesTo.ToolTipText = "Переместить файлы (для  выбранной вкладки)";
			this.tsbtnMoveFilesTo.Click += new System.EventHandler(this.TsbtnMoveFilesToClick);
			// 
			// tsbtnDeleteFiles
			// 
			this.tsbtnDeleteFiles.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnDeleteFiles.Image")));
			this.tsbtnDeleteFiles.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnDeleteFiles.Name = "tsbtnDeleteFiles";
			this.tsbtnDeleteFiles.Size = new System.Drawing.Size(79, 28);
			this.tsbtnDeleteFiles.Text = "Удалить";
			this.tsbtnDeleteFiles.ToolTipText = "Удалить файлы (для  выбранной вкладки)";
			this.tsbtnDeleteFiles.Click += new System.EventHandler(this.TsbtnDeleteFilesClick);
			// 
			// tsSep3
			// 
			this.tsSep3.Name = "tsSep3";
			this.tsSep3.Size = new System.Drawing.Size(6, 31);
			// 
			// tsddbtnMakeReport
			// 
			this.tsddbtnMakeReport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsmiReportAsHTML,
									this.tsmiReportAsFB2,
									this.tsmiReportAsCSV_CSV});
			this.tsddbtnMakeReport.Image = ((System.Drawing.Image)(resources.GetObject("tsddbtnMakeReport.Image")));
			this.tsddbtnMakeReport.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsddbtnMakeReport.Name = "tsddbtnMakeReport";
			this.tsddbtnMakeReport.Size = new System.Drawing.Size(120, 28);
			this.tsddbtnMakeReport.Text = "Создать отчет";
			this.tsddbtnMakeReport.ToolTipText = "Создать отчет (для  выбранной вкладки)";
			// 
			// tsmiReportAsHTML
			// 
			this.tsmiReportAsHTML.Name = "tsmiReportAsHTML";
			this.tsmiReportAsHTML.Size = new System.Drawing.Size(196, 22);
			this.tsmiReportAsHTML.Text = "Как html-файл...";
			this.tsmiReportAsHTML.Click += new System.EventHandler(this.TsmiReportAsHTMLClick);
			// 
			// tsmiReportAsFB2
			// 
			this.tsmiReportAsFB2.Name = "tsmiReportAsFB2";
			this.tsmiReportAsFB2.Size = new System.Drawing.Size(196, 22);
			this.tsmiReportAsFB2.Text = "Как fb2-файл...";
			this.tsmiReportAsFB2.Click += new System.EventHandler(this.TsmiReportAsFB2Click);
			// 
			// tsmiReportAsCSV_CSV
			// 
			this.tsmiReportAsCSV_CSV.Name = "tsmiReportAsCSV_CSV";
			this.tsmiReportAsCSV_CSV.Size = new System.Drawing.Size(196, 22);
			this.tsmiReportAsCSV_CSV.Text = "Как csv-файл (.csv)...";
			this.tsmiReportAsCSV_CSV.Click += new System.EventHandler(this.TsmiReportAsCSV_CSVClick);
			// 
			// pScanDir
			// 
			this.pScanDir.AutoSize = true;
			this.pScanDir.Controls.Add(this.tboxSourceDir);
			this.pScanDir.Controls.Add(this.lblDir);
			this.pScanDir.Dock = System.Windows.Forms.DockStyle.Top;
			this.pScanDir.Location = new System.Drawing.Point(0, 0);
			this.pScanDir.Margin = new System.Windows.Forms.Padding(0);
			this.pScanDir.Name = "pScanDir";
			this.pScanDir.Size = new System.Drawing.Size(720, 28);
			this.pScanDir.TabIndex = 9;
			// 
			// tboxSourceDir
			// 
			this.tboxSourceDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxSourceDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tboxSourceDir.Location = new System.Drawing.Point(162, 5);
			this.tboxSourceDir.Name = "tboxSourceDir";
			this.tboxSourceDir.ReadOnly = true;
			this.tboxSourceDir.Size = new System.Drawing.Size(555, 20);
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
			// tlpCount
			// 
			this.tlpCount.AutoSize = true;
			this.tlpCount.ColumnCount = 12;
			this.tlpCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
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
			this.tlpCount.Controls.Add(this.lblFB2ZipFiles, 6, 0);
			this.tlpCount.Controls.Add(this.lblFB2ZipFilesCount, 7, 0);
			this.tlpCount.Controls.Add(this.lblFB2RarFiles, 8, 0);
			this.tlpCount.Controls.Add(this.lblFB2RarFilesCount, 9, 0);
			this.tlpCount.Controls.Add(this.lblNotFB2Files, 10, 0);
			this.tlpCount.Controls.Add(this.lblNotFB2FilesCount, 11, 0);
			this.tlpCount.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpCount.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.tlpCount.Location = new System.Drawing.Point(0, 0);
			this.tlpCount.Margin = new System.Windows.Forms.Padding(0);
			this.tlpCount.Name = "tlpCount";
			this.tlpCount.RowCount = 1;
			this.tlpCount.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpCount.Size = new System.Drawing.Size(726, 13);
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
			this.lblFB2Files.Size = new System.Drawing.Size(79, 13);
			this.lblFB2Files.TabIndex = 4;
			this.lblFB2Files.Text = ".fb2-файлов:";
			// 
			// lblFB2FilesCount
			// 
			this.lblFB2FilesCount.AutoSize = true;
			this.lblFB2FilesCount.Location = new System.Drawing.Point(310, 0);
			this.lblFB2FilesCount.Name = "lblFB2FilesCount";
			this.lblFB2FilesCount.Size = new System.Drawing.Size(14, 13);
			this.lblFB2FilesCount.TabIndex = 5;
			this.lblFB2FilesCount.Text = "0";
			// 
			// lblFB2ZipFiles
			// 
			this.lblFB2ZipFiles.AutoSize = true;
			this.lblFB2ZipFiles.Location = new System.Drawing.Point(330, 0);
			this.lblFB2ZipFiles.Name = "lblFB2ZipFiles";
			this.lblFB2ZipFiles.Size = new System.Drawing.Size(98, 13);
			this.lblFB2ZipFiles.TabIndex = 6;
			this.lblFB2ZipFiles.Text = ".zip-файлов fb2:";
			// 
			// lblFB2ZipFilesCount
			// 
			this.lblFB2ZipFilesCount.AutoSize = true;
			this.lblFB2ZipFilesCount.Location = new System.Drawing.Point(434, 0);
			this.lblFB2ZipFilesCount.Name = "lblFB2ZipFilesCount";
			this.lblFB2ZipFilesCount.Size = new System.Drawing.Size(14, 13);
			this.lblFB2ZipFilesCount.TabIndex = 7;
			this.lblFB2ZipFilesCount.Text = "0";
			// 
			// lblFB2RarFiles
			// 
			this.lblFB2RarFiles.AutoSize = true;
			this.lblFB2RarFiles.Location = new System.Drawing.Point(454, 0);
			this.lblFB2RarFiles.Name = "lblFB2RarFiles";
			this.lblFB2RarFiles.Size = new System.Drawing.Size(99, 13);
			this.lblFB2RarFiles.TabIndex = 8;
			this.lblFB2RarFiles.Text = ".rar-файлов fb2:";
			// 
			// lblFB2RarFilesCount
			// 
			this.lblFB2RarFilesCount.AutoSize = true;
			this.lblFB2RarFilesCount.Location = new System.Drawing.Point(559, 0);
			this.lblFB2RarFilesCount.Name = "lblFB2RarFilesCount";
			this.lblFB2RarFilesCount.Size = new System.Drawing.Size(14, 13);
			this.lblFB2RarFilesCount.TabIndex = 9;
			this.lblFB2RarFilesCount.Text = "0";
			// 
			// lblNotFB2Files
			// 
			this.lblNotFB2Files.AutoSize = true;
			this.lblNotFB2Files.Location = new System.Drawing.Point(579, 0);
			this.lblNotFB2Files.Name = "lblNotFB2Files";
			this.lblNotFB2Files.Size = new System.Drawing.Size(53, 13);
			this.lblNotFB2Files.TabIndex = 10;
			this.lblNotFB2Files.Text = "Другие:";
			// 
			// lblNotFB2FilesCount
			// 
			this.lblNotFB2FilesCount.AutoSize = true;
			this.lblNotFB2FilesCount.Location = new System.Drawing.Point(638, 0);
			this.lblNotFB2FilesCount.Name = "lblNotFB2FilesCount";
			this.lblNotFB2FilesCount.Size = new System.Drawing.Size(14, 13);
			this.lblNotFB2FilesCount.TabIndex = 11;
			this.lblNotFB2FilesCount.Text = "0";
			// 
			// tcResult
			// 
			this.tcResult.Controls.Add(this.tpNonValid);
			this.tcResult.Controls.Add(this.tpValid);
			this.tcResult.Controls.Add(this.tpNotFB2Files);
			this.tcResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tcResult.Location = new System.Drawing.Point(0, 28);
			this.tcResult.Margin = new System.Windows.Forms.Padding(0);
			this.tcResult.Name = "tcResult";
			this.tcResult.SelectedIndex = 0;
			this.tcResult.Size = new System.Drawing.Size(720, 400);
			this.tcResult.TabIndex = 16;
			// 
			// tpNonValid
			// 
			this.tpNonValid.Controls.Add(this.gbFB2NotValidFiles);
			this.tpNonValid.Controls.Add(this.pErrors);
			this.tpNonValid.Controls.Add(this.listViewNotValid);
			this.tpNonValid.Location = new System.Drawing.Point(4, 22);
			this.tpNonValid.Name = "tpNonValid";
			this.tpNonValid.Padding = new System.Windows.Forms.Padding(3);
			this.tpNonValid.Size = new System.Drawing.Size(712, 374);
			this.tpNonValid.TabIndex = 0;
			this.tpNonValid.Text = " Не валидные fb2-файлы ";
			this.tpNonValid.UseVisualStyleBackColor = true;
			// 
			// gbFB2NotValidFiles
			// 
			this.gbFB2NotValidFiles.Controls.Add(this.lblFB2NotValidFilesMoveDir);
			this.gbFB2NotValidFiles.Controls.Add(this.btnFB2NotValidMoveTo);
			this.gbFB2NotValidFiles.Controls.Add(this.tboxFB2NotValidDirMoveTo);
			this.gbFB2NotValidFiles.Controls.Add(this.lblFB2NotValidFilesCopyDir);
			this.gbFB2NotValidFiles.Controls.Add(this.btnFB2NotValidCopyTo);
			this.gbFB2NotValidFiles.Controls.Add(this.tboxFB2NotValidDirCopyTo);
			this.gbFB2NotValidFiles.Dock = System.Windows.Forms.DockStyle.Top;
			this.gbFB2NotValidFiles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gbFB2NotValidFiles.Location = new System.Drawing.Point(3, 3);
			this.gbFB2NotValidFiles.Name = "gbFB2NotValidFiles";
			this.gbFB2NotValidFiles.Size = new System.Drawing.Size(706, 75);
			this.gbFB2NotValidFiles.TabIndex = 6;
			this.gbFB2NotValidFiles.TabStop = false;
			this.gbFB2NotValidFiles.Text = " Обработка не валидных fb2-файлов: ";
			// 
			// lblFB2NotValidFilesMoveDir
			// 
			this.lblFB2NotValidFilesMoveDir.AutoSize = true;
			this.lblFB2NotValidFilesMoveDir.Location = new System.Drawing.Point(6, 49);
			this.lblFB2NotValidFilesMoveDir.Name = "lblFB2NotValidFilesMoveDir";
			this.lblFB2NotValidFilesMoveDir.Size = new System.Drawing.Size(101, 13);
			this.lblFB2NotValidFilesMoveDir.TabIndex = 9;
			this.lblFB2NotValidFilesMoveDir.Text = "Переместить в:";
			this.lblFB2NotValidFilesMoveDir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnFB2NotValidMoveTo
			// 
			this.btnFB2NotValidMoveTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFB2NotValidMoveTo.Image = ((System.Drawing.Image)(resources.GetObject("btnFB2NotValidMoveTo.Image")));
			this.btnFB2NotValidMoveTo.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnFB2NotValidMoveTo.Location = new System.Drawing.Point(654, 44);
			this.btnFB2NotValidMoveTo.Name = "btnFB2NotValidMoveTo";
			this.btnFB2NotValidMoveTo.Size = new System.Drawing.Size(37, 24);
			this.btnFB2NotValidMoveTo.TabIndex = 8;
			this.btnFB2NotValidMoveTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnFB2NotValidMoveTo.UseVisualStyleBackColor = true;
			this.btnFB2NotValidMoveTo.Click += new System.EventHandler(this.BtnFB2NotValidMoveToClick);
			// 
			// tboxFB2NotValidDirMoveTo
			// 
			this.tboxFB2NotValidDirMoveTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxFB2NotValidDirMoveTo.Location = new System.Drawing.Point(109, 46);
			this.tboxFB2NotValidDirMoveTo.Name = "tboxFB2NotValidDirMoveTo";
			this.tboxFB2NotValidDirMoveTo.ReadOnly = true;
			this.tboxFB2NotValidDirMoveTo.Size = new System.Drawing.Size(539, 20);
			this.tboxFB2NotValidDirMoveTo.TabIndex = 7;
			// 
			// lblFB2NotValidFilesCopyDir
			// 
			this.lblFB2NotValidFilesCopyDir.AutoSize = true;
			this.lblFB2NotValidFilesCopyDir.Location = new System.Drawing.Point(6, 23);
			this.lblFB2NotValidFilesCopyDir.Name = "lblFB2NotValidFilesCopyDir";
			this.lblFB2NotValidFilesCopyDir.Size = new System.Drawing.Size(92, 13);
			this.lblFB2NotValidFilesCopyDir.TabIndex = 6;
			this.lblFB2NotValidFilesCopyDir.Text = "Копировать в:";
			this.lblFB2NotValidFilesCopyDir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnFB2NotValidCopyTo
			// 
			this.btnFB2NotValidCopyTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFB2NotValidCopyTo.Image = ((System.Drawing.Image)(resources.GetObject("btnFB2NotValidCopyTo.Image")));
			this.btnFB2NotValidCopyTo.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnFB2NotValidCopyTo.Location = new System.Drawing.Point(654, 18);
			this.btnFB2NotValidCopyTo.Name = "btnFB2NotValidCopyTo";
			this.btnFB2NotValidCopyTo.Size = new System.Drawing.Size(37, 24);
			this.btnFB2NotValidCopyTo.TabIndex = 5;
			this.btnFB2NotValidCopyTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnFB2NotValidCopyTo.UseVisualStyleBackColor = true;
			this.btnFB2NotValidCopyTo.Click += new System.EventHandler(this.BtnFB2NotValidCopyToClick);
			// 
			// tboxFB2NotValidDirCopyTo
			// 
			this.tboxFB2NotValidDirCopyTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxFB2NotValidDirCopyTo.Location = new System.Drawing.Point(109, 20);
			this.tboxFB2NotValidDirCopyTo.Name = "tboxFB2NotValidDirCopyTo";
			this.tboxFB2NotValidDirCopyTo.ReadOnly = true;
			this.tboxFB2NotValidDirCopyTo.Size = new System.Drawing.Size(539, 20);
			this.tboxFB2NotValidDirCopyTo.TabIndex = 0;
			// 
			// pErrors
			// 
			this.pErrors.Controls.Add(this.rеboxNotValid);
			this.pErrors.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pErrors.Location = new System.Drawing.Point(3, 312);
			this.pErrors.Name = "pErrors";
			this.pErrors.Size = new System.Drawing.Size(706, 59);
			this.pErrors.TabIndex = 1;
			// 
			// rеboxNotValid
			// 
			this.rеboxNotValid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rеboxNotValid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rеboxNotValid.Location = new System.Drawing.Point(0, 0);
			this.rеboxNotValid.Name = "rеboxNotValid";
			this.rеboxNotValid.Size = new System.Drawing.Size(706, 59);
			this.rеboxNotValid.TabIndex = 2;
			this.rеboxNotValid.Text = "";
			// 
			// listViewNotValid
			// 
			this.listViewNotValid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.listViewNotValid.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.chNonValidFile,
									this.chNonValidError,
									this.chNonValidLenght});
			this.listViewNotValid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.listViewNotValid.FullRowSelect = true;
			this.listViewNotValid.GridLines = true;
			this.listViewNotValid.Location = new System.Drawing.Point(3, 84);
			this.listViewNotValid.MultiSelect = false;
			this.listViewNotValid.Name = "listViewNotValid";
			this.listViewNotValid.ShowItemToolTips = true;
			this.listViewNotValid.Size = new System.Drawing.Size(706, 222);
			this.listViewNotValid.TabIndex = 0;
			this.listViewNotValid.UseCompatibleStateImageBehavior = false;
			this.listViewNotValid.View = System.Windows.Forms.View.Details;
			this.listViewNotValid.DoubleClick += new System.EventHandler(this.ListViewNonValidDoubleClick);
			this.listViewNotValid.SelectedIndexChanged += new System.EventHandler(this.ListViewNotValidSelectedIndexChanged);
			// 
			// chNonValidFile
			// 
			this.chNonValidFile.Text = "fb2-файл";
			this.chNonValidFile.Width = 400;
			// 
			// chNonValidError
			// 
			this.chNonValidError.Text = "Ошибка";
			this.chNonValidError.Width = 300;
			// 
			// chNonValidLenght
			// 
			this.chNonValidLenght.Text = "Размер";
			this.chNonValidLenght.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// tpValid
			// 
			this.tpValid.Controls.Add(this.pValidLV);
			this.tpValid.Controls.Add(this.gbFB2Valid);
			this.tpValid.Location = new System.Drawing.Point(4, 22);
			this.tpValid.Name = "tpValid";
			this.tpValid.Padding = new System.Windows.Forms.Padding(3);
			this.tpValid.Size = new System.Drawing.Size(712, 374);
			this.tpValid.TabIndex = 1;
			this.tpValid.Text = " Валидные fb2-файлы ";
			this.tpValid.UseVisualStyleBackColor = true;
			// 
			// pValidLV
			// 
			this.pValidLV.Controls.Add(this.listViewValid);
			this.pValidLV.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pValidLV.Location = new System.Drawing.Point(3, 78);
			this.pValidLV.Name = "pValidLV";
			this.pValidLV.Size = new System.Drawing.Size(706, 293);
			this.pValidLV.TabIndex = 9;
			// 
			// listViewValid
			// 
			this.listViewValid.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.chValidFile,
									this.chValidLenght});
			this.listViewValid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewValid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.listViewValid.FullRowSelect = true;
			this.listViewValid.GridLines = true;
			this.listViewValid.Location = new System.Drawing.Point(0, 0);
			this.listViewValid.MultiSelect = false;
			this.listViewValid.Name = "listViewValid";
			this.listViewValid.ShowItemToolTips = true;
			this.listViewValid.Size = new System.Drawing.Size(706, 293);
			this.listViewValid.TabIndex = 1;
			this.listViewValid.UseCompatibleStateImageBehavior = false;
			this.listViewValid.View = System.Windows.Forms.View.Details;
			this.listViewValid.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListViewValidColumnClick);
			// 
			// chValidFile
			// 
			this.chValidFile.Text = "fb2-файл";
			this.chValidFile.Width = 700;
			// 
			// chValidLenght
			// 
			this.chValidLenght.Text = "Размер";
			this.chValidLenght.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// gbFB2Valid
			// 
			this.gbFB2Valid.Controls.Add(this.lblFB2ValidFilesMoveDir);
			this.gbFB2Valid.Controls.Add(this.btnFB2ValidMoveTo);
			this.gbFB2Valid.Controls.Add(this.tboxFB2ValidDirMoveTo);
			this.gbFB2Valid.Controls.Add(this.lblFB2ValidFilesCopyDir);
			this.gbFB2Valid.Controls.Add(this.btnFB2ValidCopyTo);
			this.gbFB2Valid.Controls.Add(this.tboxFB2ValidDirCopyTo);
			this.gbFB2Valid.Dock = System.Windows.Forms.DockStyle.Top;
			this.gbFB2Valid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gbFB2Valid.Location = new System.Drawing.Point(3, 3);
			this.gbFB2Valid.Name = "gbFB2Valid";
			this.gbFB2Valid.Size = new System.Drawing.Size(706, 75);
			this.gbFB2Valid.TabIndex = 8;
			this.gbFB2Valid.TabStop = false;
			this.gbFB2Valid.Text = " Обработка валидных fb2-файлов: ";
			// 
			// lblFB2ValidFilesMoveDir
			// 
			this.lblFB2ValidFilesMoveDir.AutoSize = true;
			this.lblFB2ValidFilesMoveDir.Location = new System.Drawing.Point(6, 49);
			this.lblFB2ValidFilesMoveDir.Name = "lblFB2ValidFilesMoveDir";
			this.lblFB2ValidFilesMoveDir.Size = new System.Drawing.Size(101, 13);
			this.lblFB2ValidFilesMoveDir.TabIndex = 9;
			this.lblFB2ValidFilesMoveDir.Text = "Переместить в:";
			this.lblFB2ValidFilesMoveDir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnFB2ValidMoveTo
			// 
			this.btnFB2ValidMoveTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFB2ValidMoveTo.Image = ((System.Drawing.Image)(resources.GetObject("btnFB2ValidMoveTo.Image")));
			this.btnFB2ValidMoveTo.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnFB2ValidMoveTo.Location = new System.Drawing.Point(654, 44);
			this.btnFB2ValidMoveTo.Name = "btnFB2ValidMoveTo";
			this.btnFB2ValidMoveTo.Size = new System.Drawing.Size(37, 24);
			this.btnFB2ValidMoveTo.TabIndex = 8;
			this.btnFB2ValidMoveTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnFB2ValidMoveTo.UseVisualStyleBackColor = true;
			this.btnFB2ValidMoveTo.Click += new System.EventHandler(this.BtnFB2ValidMoveToClick);
			// 
			// tboxFB2ValidDirMoveTo
			// 
			this.tboxFB2ValidDirMoveTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxFB2ValidDirMoveTo.Location = new System.Drawing.Point(109, 46);
			this.tboxFB2ValidDirMoveTo.Name = "tboxFB2ValidDirMoveTo";
			this.tboxFB2ValidDirMoveTo.ReadOnly = true;
			this.tboxFB2ValidDirMoveTo.Size = new System.Drawing.Size(539, 20);
			this.tboxFB2ValidDirMoveTo.TabIndex = 7;
			// 
			// lblFB2ValidFilesCopyDir
			// 
			this.lblFB2ValidFilesCopyDir.AutoSize = true;
			this.lblFB2ValidFilesCopyDir.Location = new System.Drawing.Point(6, 23);
			this.lblFB2ValidFilesCopyDir.Name = "lblFB2ValidFilesCopyDir";
			this.lblFB2ValidFilesCopyDir.Size = new System.Drawing.Size(92, 13);
			this.lblFB2ValidFilesCopyDir.TabIndex = 6;
			this.lblFB2ValidFilesCopyDir.Text = "Копировать в:";
			this.lblFB2ValidFilesCopyDir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnFB2ValidCopyTo
			// 
			this.btnFB2ValidCopyTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFB2ValidCopyTo.Image = ((System.Drawing.Image)(resources.GetObject("btnFB2ValidCopyTo.Image")));
			this.btnFB2ValidCopyTo.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnFB2ValidCopyTo.Location = new System.Drawing.Point(654, 18);
			this.btnFB2ValidCopyTo.Name = "btnFB2ValidCopyTo";
			this.btnFB2ValidCopyTo.Size = new System.Drawing.Size(37, 24);
			this.btnFB2ValidCopyTo.TabIndex = 5;
			this.btnFB2ValidCopyTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnFB2ValidCopyTo.UseVisualStyleBackColor = true;
			this.btnFB2ValidCopyTo.Click += new System.EventHandler(this.BtnFB2ValidCopyToClick);
			// 
			// tboxFB2ValidDirCopyTo
			// 
			this.tboxFB2ValidDirCopyTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxFB2ValidDirCopyTo.Location = new System.Drawing.Point(109, 20);
			this.tboxFB2ValidDirCopyTo.Name = "tboxFB2ValidDirCopyTo";
			this.tboxFB2ValidDirCopyTo.ReadOnly = true;
			this.tboxFB2ValidDirCopyTo.Size = new System.Drawing.Size(539, 20);
			this.tboxFB2ValidDirCopyTo.TabIndex = 0;
			// 
			// tpNotFB2Files
			// 
			this.tpNotFB2Files.Controls.Add(this.pNotValidLV);
			this.tpNotFB2Files.Controls.Add(this.gbNotFB2);
			this.tpNotFB2Files.Location = new System.Drawing.Point(4, 22);
			this.tpNotFB2Files.Name = "tpNotFB2Files";
			this.tpNotFB2Files.Padding = new System.Windows.Forms.Padding(3);
			this.tpNotFB2Files.Size = new System.Drawing.Size(712, 374);
			this.tpNotFB2Files.TabIndex = 2;
			this.tpNotFB2Files.Text = " Не fb2-файлы ";
			this.tpNotFB2Files.UseVisualStyleBackColor = true;
			// 
			// pNotValidLV
			// 
			this.pNotValidLV.Controls.Add(this.listViewNotFB2);
			this.pNotValidLV.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pNotValidLV.Location = new System.Drawing.Point(3, 78);
			this.pNotValidLV.Name = "pNotValidLV";
			this.pNotValidLV.Size = new System.Drawing.Size(706, 293);
			this.pNotValidLV.TabIndex = 10;
			// 
			// listViewNotFB2
			// 
			this.listViewNotFB2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeader1,
									this.columnHeader2,
									this.columnHeader3});
			this.listViewNotFB2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewNotFB2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.listViewNotFB2.FullRowSelect = true;
			this.listViewNotFB2.GridLines = true;
			this.listViewNotFB2.Location = new System.Drawing.Point(0, 0);
			this.listViewNotFB2.MultiSelect = false;
			this.listViewNotFB2.Name = "listViewNotFB2";
			this.listViewNotFB2.ShowItemToolTips = true;
			this.listViewNotFB2.Size = new System.Drawing.Size(706, 293);
			this.listViewNotFB2.TabIndex = 2;
			this.listViewNotFB2.UseCompatibleStateImageBehavior = false;
			this.listViewNotFB2.View = System.Windows.Forms.View.Details;
			this.listViewNotFB2.DoubleClick += new System.EventHandler(this.ListViewNotFB2DoubleClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Разные файлы и zip, rar архивы";
			this.columnHeader1.Width = 600;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Тип";
			this.columnHeader2.Width = 40;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Размер";
			this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader3.Width = 80;
			// 
			// gbNotFB2
			// 
			this.gbNotFB2.Controls.Add(this.lblNotFB2FilesMoveDir);
			this.gbNotFB2.Controls.Add(this.btnNotFB2MoveTo);
			this.gbNotFB2.Controls.Add(this.tboxNotFB2DirMoveTo);
			this.gbNotFB2.Controls.Add(this.lblNotFB2FilesCopyDir);
			this.gbNotFB2.Controls.Add(this.btnNotFB2CopyTo);
			this.gbNotFB2.Controls.Add(this.tboxNotFB2DirCopyTo);
			this.gbNotFB2.Dock = System.Windows.Forms.DockStyle.Top;
			this.gbNotFB2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gbNotFB2.Location = new System.Drawing.Point(3, 3);
			this.gbNotFB2.Name = "gbNotFB2";
			this.gbNotFB2.Size = new System.Drawing.Size(706, 75);
			this.gbNotFB2.TabIndex = 9;
			this.gbNotFB2.TabStop = false;
			this.gbNotFB2.Text = " Обработка не валидных fb2-файлов: ";
			// 
			// lblNotFB2FilesMoveDir
			// 
			this.lblNotFB2FilesMoveDir.AutoSize = true;
			this.lblNotFB2FilesMoveDir.Location = new System.Drawing.Point(6, 49);
			this.lblNotFB2FilesMoveDir.Name = "lblNotFB2FilesMoveDir";
			this.lblNotFB2FilesMoveDir.Size = new System.Drawing.Size(101, 13);
			this.lblNotFB2FilesMoveDir.TabIndex = 9;
			this.lblNotFB2FilesMoveDir.Text = "Переместить в:";
			this.lblNotFB2FilesMoveDir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnNotFB2MoveTo
			// 
			this.btnNotFB2MoveTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNotFB2MoveTo.Image = ((System.Drawing.Image)(resources.GetObject("btnNotFB2MoveTo.Image")));
			this.btnNotFB2MoveTo.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnNotFB2MoveTo.Location = new System.Drawing.Point(654, 44);
			this.btnNotFB2MoveTo.Name = "btnNotFB2MoveTo";
			this.btnNotFB2MoveTo.Size = new System.Drawing.Size(37, 24);
			this.btnNotFB2MoveTo.TabIndex = 8;
			this.btnNotFB2MoveTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnNotFB2MoveTo.UseVisualStyleBackColor = true;
			this.btnNotFB2MoveTo.Click += new System.EventHandler(this.BtnNotFB2MoveToClick);
			// 
			// tboxNotFB2DirMoveTo
			// 
			this.tboxNotFB2DirMoveTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxNotFB2DirMoveTo.Location = new System.Drawing.Point(109, 46);
			this.tboxNotFB2DirMoveTo.Name = "tboxNotFB2DirMoveTo";
			this.tboxNotFB2DirMoveTo.ReadOnly = true;
			this.tboxNotFB2DirMoveTo.Size = new System.Drawing.Size(539, 20);
			this.tboxNotFB2DirMoveTo.TabIndex = 7;
			// 
			// lblNotFB2FilesCopyDir
			// 
			this.lblNotFB2FilesCopyDir.AutoSize = true;
			this.lblNotFB2FilesCopyDir.Location = new System.Drawing.Point(6, 23);
			this.lblNotFB2FilesCopyDir.Name = "lblNotFB2FilesCopyDir";
			this.lblNotFB2FilesCopyDir.Size = new System.Drawing.Size(92, 13);
			this.lblNotFB2FilesCopyDir.TabIndex = 6;
			this.lblNotFB2FilesCopyDir.Text = "Копировать в:";
			this.lblNotFB2FilesCopyDir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnNotFB2CopyTo
			// 
			this.btnNotFB2CopyTo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNotFB2CopyTo.Image = ((System.Drawing.Image)(resources.GetObject("btnNotFB2CopyTo.Image")));
			this.btnNotFB2CopyTo.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnNotFB2CopyTo.Location = new System.Drawing.Point(654, 18);
			this.btnNotFB2CopyTo.Name = "btnNotFB2CopyTo";
			this.btnNotFB2CopyTo.Size = new System.Drawing.Size(37, 24);
			this.btnNotFB2CopyTo.TabIndex = 5;
			this.btnNotFB2CopyTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnNotFB2CopyTo.UseVisualStyleBackColor = true;
			this.btnNotFB2CopyTo.Click += new System.EventHandler(this.BtnNotFB2CopyToClick);
			// 
			// tboxNotFB2DirCopyTo
			// 
			this.tboxNotFB2DirCopyTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxNotFB2DirCopyTo.Location = new System.Drawing.Point(109, 20);
			this.tboxNotFB2DirCopyTo.Name = "tboxNotFB2DirCopyTo";
			this.tboxNotFB2DirCopyTo.ReadOnly = true;
			this.tboxNotFB2DirCopyTo.Size = new System.Drawing.Size(539, 20);
			this.tboxNotFB2DirCopyTo.TabIndex = 0;
			// 
			// ssProgress
			// 
			this.ssProgress.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsslblProgress,
									this.tsProgressBar});
			this.ssProgress.Location = new System.Drawing.Point(0, 478);
			this.ssProgress.Name = "ssProgress";
			this.ssProgress.Size = new System.Drawing.Size(726, 22);
			this.ssProgress.TabIndex = 17;
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
			// tlCentral
			// 
			this.tlCentral.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tlCentral.ColumnCount = 1;
			this.tlCentral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlCentral.Controls.Add(this.pScanDir, 0, 0);
			this.tlCentral.Controls.Add(this.tcResult, 0, 1);
			this.tlCentral.Location = new System.Drawing.Point(3, 31);
			this.tlCentral.Margin = new System.Windows.Forms.Padding(0);
			this.tlCentral.Name = "tlCentral";
			this.tlCentral.RowCount = 2;
			this.tlCentral.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlCentral.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlCentral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlCentral.Size = new System.Drawing.Size(720, 428);
			this.tlCentral.TabIndex = 19;
			// 
			// pCount
			// 
			this.pCount.AutoSize = true;
			this.pCount.Controls.Add(this.tlpCount);
			this.pCount.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pCount.Location = new System.Drawing.Point(0, 465);
			this.pCount.Margin = new System.Windows.Forms.Padding(0);
			this.pCount.Name = "pCount";
			this.pCount.Size = new System.Drawing.Size(726, 13);
			this.pCount.TabIndex = 20;
			// 
			// fbdNotValidFB2
			// 
			this.fbdNotValidFB2.Description = "Укажите папку для не валидных fb2-файлов";
			// 
			// fbdValidFB2
			// 
			this.fbdValidFB2.Description = "Укажите папку для валидных fb2-файлов";
			// 
			// fbdNotFB2
			// 
			this.fbdNotFB2.Description = "Укажите папку для не fb2-файлов";
			// 
			// fbdSource
			// 
			this.fbdSource.Description = "Укажите папку для проверки fb2-файлов";
			// 
			// sfdReport
			// 
			this.sfdReport.Title = "Укажите название файла отчета";
			// 
			// SFBTpFB2Validator
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pCount);
			this.Controls.Add(this.tlCentral);
			this.Controls.Add(this.ssProgress);
			this.Controls.Add(this.tsValidator);
			this.Name = "SFBTpFB2Validator";
			this.Size = new System.Drawing.Size(726, 500);
			this.Tag = "validator";
			this.tsValidator.ResumeLayout(false);
			this.tsValidator.PerformLayout();
			this.pScanDir.ResumeLayout(false);
			this.pScanDir.PerformLayout();
			this.tlpCount.ResumeLayout(false);
			this.tlpCount.PerformLayout();
			this.tcResult.ResumeLayout(false);
			this.tpNonValid.ResumeLayout(false);
			this.gbFB2NotValidFiles.ResumeLayout(false);
			this.gbFB2NotValidFiles.PerformLayout();
			this.pErrors.ResumeLayout(false);
			this.tpValid.ResumeLayout(false);
			this.pValidLV.ResumeLayout(false);
			this.gbFB2Valid.ResumeLayout(false);
			this.gbFB2Valid.PerformLayout();
			this.tpNotFB2Files.ResumeLayout(false);
			this.pNotValidLV.ResumeLayout(false);
			this.gbNotFB2.ResumeLayout(false);
			this.gbNotFB2.PerformLayout();
			this.ssProgress.ResumeLayout(false);
			this.ssProgress.PerformLayout();
			this.tlCentral.ResumeLayout(false);
			this.tlCentral.PerformLayout();
			this.pCount.ResumeLayout(false);
			this.pCount.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.SaveFileDialog sfdReport;
		private System.Windows.Forms.ToolStripMenuItem tsmiReportAsCSV_CSV;
		private System.Windows.Forms.ToolStripMenuItem tsmiReportAsFB2;
		private System.Windows.Forms.ToolStripMenuItem tsmiReportAsHTML;
		private System.Windows.Forms.ToolStripDropDownButton tsddbtnMakeReport;
		private System.Windows.Forms.ToolStripSeparator tsSep3;
		private System.Windows.Forms.ToolStripButton tsbtnDeleteFiles;
		private System.Windows.Forms.ToolStripButton tsbtnMoveFilesTo;
		private System.Windows.Forms.ToolStripButton tsbtnCopyFilesTo;
		private System.Windows.Forms.ToolStripSeparator tsSep2;
		private System.Windows.Forms.ToolStripButton tSBValidate;
		private System.Windows.Forms.FolderBrowserDialog fbdSource;
		private System.Windows.Forms.ToolStripSeparator tsSep1;
		private System.Windows.Forms.ToolStripButton tsbtnOpenDir;
		private System.Windows.Forms.FolderBrowserDialog fbdNotFB2;
		private System.Windows.Forms.FolderBrowserDialog fbdValidFB2;
		private System.Windows.Forms.FolderBrowserDialog fbdNotValidFB2;
		private System.Windows.Forms.GroupBox gbFB2NotValidFiles;
		private System.Windows.Forms.Label lblFB2NotValidFilesMoveDir;
		private System.Windows.Forms.Button btnFB2NotValidMoveTo;
		private System.Windows.Forms.TextBox tboxFB2NotValidDirMoveTo;
		private System.Windows.Forms.Label lblFB2NotValidFilesCopyDir;
		private System.Windows.Forms.Button btnFB2NotValidCopyTo;
		private System.Windows.Forms.TextBox tboxFB2NotValidDirCopyTo;
		private System.Windows.Forms.RichTextBox rеboxNotValid;
		private System.Windows.Forms.ListView listViewNotValid;
		private System.Windows.Forms.TableLayoutPanel tlpCount;
		private System.Windows.Forms.Panel pScanDir;
		private System.Windows.Forms.Panel pCount;
		private System.Windows.Forms.TableLayoutPanel tlCentral;
		private System.Windows.Forms.ToolStripStatusLabel tsslblProgress;
		private System.Windows.Forms.ToolStripProgressBar tsProgressBar;
		private System.Windows.Forms.StatusStrip ssProgress;
		private System.Windows.Forms.Label lblNotFB2Files;
		private System.Windows.Forms.Label lblNotFB2FilesCount;
		private System.Windows.Forms.Label lblFB2RarFilesCount;
		private System.Windows.Forms.Label lblFB2RarFiles;
		private System.Windows.Forms.Label lblFB2ZipFilesCount;
		private System.Windows.Forms.Label lblFB2ZipFiles;
		private System.Windows.Forms.Label lblFB2FilesCount;
		private System.Windows.Forms.Label lblFB2Files;
		private System.Windows.Forms.Label lblFilesCount;
		private System.Windows.Forms.Label lblFiles;
		private System.Windows.Forms.Label lblDirsCount;
		private System.Windows.Forms.Label lblDirs;
		private System.Windows.Forms.TextBox tboxNotFB2DirCopyTo;
		private System.Windows.Forms.Button btnNotFB2CopyTo;
		private System.Windows.Forms.Label lblNotFB2FilesCopyDir;
		private System.Windows.Forms.TextBox tboxNotFB2DirMoveTo;
		private System.Windows.Forms.Button btnNotFB2MoveTo;
		private System.Windows.Forms.Label lblNotFB2FilesMoveDir;
		private System.Windows.Forms.GroupBox gbNotFB2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ListView listViewNotFB2;
		private System.Windows.Forms.Panel pNotValidLV;
		private System.Windows.Forms.TabPage tpNotFB2Files;
		private System.Windows.Forms.TextBox tboxFB2ValidDirCopyTo;
		private System.Windows.Forms.Button btnFB2ValidCopyTo;
		private System.Windows.Forms.Label lblFB2ValidFilesCopyDir;
		private System.Windows.Forms.TextBox tboxFB2ValidDirMoveTo;
		private System.Windows.Forms.Button btnFB2ValidMoveTo;
		private System.Windows.Forms.Label lblFB2ValidFilesMoveDir;
		private System.Windows.Forms.GroupBox gbFB2Valid;
		private System.Windows.Forms.ColumnHeader chValidLenght;
		private System.Windows.Forms.ColumnHeader chValidFile;
		private System.Windows.Forms.ListView listViewValid;
		private System.Windows.Forms.Panel pValidLV;
		private System.Windows.Forms.TabPage tpValid;
		private System.Windows.Forms.ColumnHeader chNonValidLenght;
		private System.Windows.Forms.ColumnHeader chNonValidError;
		private System.Windows.Forms.ColumnHeader chNonValidFile;
		private System.Windows.Forms.Panel pErrors;
		private System.Windows.Forms.TabPage tpNonValid;
		private System.Windows.Forms.TabControl tcResult;
		private System.Windows.Forms.Label lblDir;
		private System.Windows.Forms.TextBox tboxSourceDir;
		private System.Windows.Forms.ToolStrip tsValidator;
	}
}
