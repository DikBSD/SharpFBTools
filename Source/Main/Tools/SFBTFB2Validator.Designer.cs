/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 13.03.2009
 * Time: 14:34
 * 
 * License: GPL 2.1
 */
namespace SharpFBTools.Tools
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SFBTpFB2Validator));
			System.Windows.Forms.ListViewItem listViewItem31 = new System.Windows.Forms.ListViewItem(new string[] {
									"Всего папок",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem32 = new System.Windows.Forms.ListViewItem(new string[] {
									"Всего файлов",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem33 = new System.Windows.Forms.ListViewItem(new string[] {
									"fb2-файлов",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem34 = new System.Windows.Forms.ListViewItem(new string[] {
									"fb2 в .zip-архивах",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem35 = new System.Windows.Forms.ListViewItem(new string[] {
									"fb2 в .rar-архивах",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem36 = new System.Windows.Forms.ListViewItem(new string[] {
									"Другие файлы",
									"0"}, -1);
			this.tsValidator = new System.Windows.Forms.ToolStrip();
			this.tsbtnOpenDir = new System.Windows.Forms.ToolStripButton();
			this.tsSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.tSBValidate = new System.Windows.Forms.ToolStripButton();
			this.tsSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnCopyFilesTo = new System.Windows.Forms.ToolStripButton();
			this.tsbtnMoveFilesTo = new System.Windows.Forms.ToolStripButton();
			this.tsbtnDeleteFiles = new System.Windows.Forms.ToolStripButton();
			this.tsSep3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsddbtnMakeFileList = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsmiMakeNotValidFileList = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiMakeValidFileList = new System.Windows.Forms.ToolStripMenuItem();
			this.tsSep4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsddbtnMakeReport = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsmiReportAsHTML = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiReportAsFB2 = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiReportAsCSV_CSV = new System.Windows.Forms.ToolStripMenuItem();
			this.pScanDir = new System.Windows.Forms.Panel();
			this.cboxScanSubDir = new System.Windows.Forms.CheckBox();
			this.tboxSourceDir = new System.Windows.Forms.TextBox();
			this.lblDir = new System.Windows.Forms.Label();
			this.tcResult = new System.Windows.Forms.TabControl();
			this.tpNotValid = new System.Windows.Forms.TabPage();
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
			this.cmsFB2 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiFileReValidate = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmi3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiEditInTextEditor = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiEditInFB2Editor = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmi1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiViewInReader = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmi2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiOpenFileDir = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiDeleteFileFromDisk = new System.Windows.Forms.ToolStripMenuItem();
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
			this.cmsNotFB2 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiOpenNotFB2FileDir = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiDeleteNotFB2FromDisk = new System.Windows.Forms.ToolStripMenuItem();
			this.gbNotFB2 = new System.Windows.Forms.GroupBox();
			this.lblNotFB2FilesMoveDir = new System.Windows.Forms.Label();
			this.btnNotFB2MoveTo = new System.Windows.Forms.Button();
			this.tboxNotFB2DirMoveTo = new System.Windows.Forms.TextBox();
			this.lblNotFB2FilesCopyDir = new System.Windows.Forms.Label();
			this.btnNotFB2CopyTo = new System.Windows.Forms.Button();
			this.tboxNotFB2DirCopyTo = new System.Windows.Forms.TextBox();
			this.imgl16 = new System.Windows.Forms.ImageList(this.components);
			this.ssProgress = new System.Windows.Forms.StatusStrip();
			this.tsslblProgress = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.tlCentral = new System.Windows.Forms.TableLayoutPanel();
			this.fbdDir = new System.Windows.Forms.FolderBrowserDialog();
			this.sfdReport = new System.Windows.Forms.SaveFileDialog();
			this.lvFilesCount = new System.Windows.Forms.ListView();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.pInfo = new System.Windows.Forms.Panel();
			this.gboxCopyMoveOptions = new System.Windows.Forms.GroupBox();
			this.chBoxAddBookID = new System.Windows.Forms.CheckBox();
			this.cboxExistFile = new System.Windows.Forms.ComboBox();
			this.lblExistFile = new System.Windows.Forms.Label();
			this.pCentral = new System.Windows.Forms.Panel();
			this.cmsArchive = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiFileAndArchiveReValidate = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmi5 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiOpenInArchivator = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmi4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiOpenArchiveDir = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiFileDeleteFromDisk = new System.Windows.Forms.ToolStripMenuItem();
			this.tsValidator.SuspendLayout();
			this.pScanDir.SuspendLayout();
			this.tcResult.SuspendLayout();
			this.tpNotValid.SuspendLayout();
			this.gbFB2NotValidFiles.SuspendLayout();
			this.pErrors.SuspendLayout();
			this.cmsFB2.SuspendLayout();
			this.tpValid.SuspendLayout();
			this.pValidLV.SuspendLayout();
			this.gbFB2Valid.SuspendLayout();
			this.tpNotFB2Files.SuspendLayout();
			this.pNotValidLV.SuspendLayout();
			this.cmsNotFB2.SuspendLayout();
			this.gbNotFB2.SuspendLayout();
			this.ssProgress.SuspendLayout();
			this.tlCentral.SuspendLayout();
			this.pInfo.SuspendLayout();
			this.gboxCopyMoveOptions.SuspendLayout();
			this.pCentral.SuspendLayout();
			this.cmsArchive.SuspendLayout();
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
									this.tsddbtnMakeFileList,
									this.tsSep4,
									this.tsddbtnMakeReport});
			this.tsValidator.Location = new System.Drawing.Point(0, 0);
			this.tsValidator.Name = "tsValidator";
			this.tsValidator.Size = new System.Drawing.Size(831, 31);
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
			this.tsbtnCopyFilesTo.Size = new System.Drawing.Size(116, 28);
			this.tsbtnCopyFilesTo.Text = "Копировать Все";
			this.tsbtnCopyFilesTo.ToolTipText = "Копировать файлы (для выбранной вкладки)";
			this.tsbtnCopyFilesTo.Click += new System.EventHandler(this.TsbtnCopyFilesToClick);
			// 
			// tsbtnMoveFilesTo
			// 
			this.tsbtnMoveFilesTo.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnMoveFilesTo.Image")));
			this.tsbtnMoveFilesTo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnMoveFilesTo.Name = "tsbtnMoveFilesTo";
			this.tsbtnMoveFilesTo.Size = new System.Drawing.Size(121, 28);
			this.tsbtnMoveFilesTo.Text = "Переместить Все";
			this.tsbtnMoveFilesTo.ToolTipText = "Переместить файлы (для  выбранной вкладки)";
			this.tsbtnMoveFilesTo.Click += new System.EventHandler(this.TsbtnMoveFilesToClick);
			// 
			// tsbtnDeleteFiles
			// 
			this.tsbtnDeleteFiles.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnDeleteFiles.Image")));
			this.tsbtnDeleteFiles.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnDeleteFiles.Name = "tsbtnDeleteFiles";
			this.tsbtnDeleteFiles.Size = new System.Drawing.Size(99, 28);
			this.tsbtnDeleteFiles.Text = "Удалить Все";
			this.tsbtnDeleteFiles.ToolTipText = "Удалить файлы (для  выбранной вкладки)";
			this.tsbtnDeleteFiles.Click += new System.EventHandler(this.TsbtnDeleteFilesClick);
			// 
			// tsSep3
			// 
			this.tsSep3.Name = "tsSep3";
			this.tsSep3.Size = new System.Drawing.Size(6, 31);
			// 
			// tsddbtnMakeFileList
			// 
			this.tsddbtnMakeFileList.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsmiMakeNotValidFileList,
									this.tsmiMakeValidFileList});
			this.tsddbtnMakeFileList.Image = ((System.Drawing.Image)(resources.GetObject("tsddbtnMakeFileList.Image")));
			this.tsddbtnMakeFileList.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsddbtnMakeFileList.Name = "tsddbtnMakeFileList";
			this.tsddbtnMakeFileList.Size = new System.Drawing.Size(121, 28);
			this.tsddbtnMakeFileList.Text = "Список файлов";
			// 
			// tsmiMakeNotValidFileList
			// 
			this.tsmiMakeNotValidFileList.Image = ((System.Drawing.Image)(resources.GetObject("tsmiMakeNotValidFileList.Image")));
			this.tsmiMakeNotValidFileList.Name = "tsmiMakeNotValidFileList";
			this.tsmiMakeNotValidFileList.Size = new System.Drawing.Size(288, 22);
			this.tsmiMakeNotValidFileList.Text = "Сохранить список Не валидных файлов";
			this.tsmiMakeNotValidFileList.Click += new System.EventHandler(this.TsmiMakeNotValidFileListClick);
			// 
			// tsmiMakeValidFileList
			// 
			this.tsmiMakeValidFileList.Image = ((System.Drawing.Image)(resources.GetObject("tsmiMakeValidFileList.Image")));
			this.tsmiMakeValidFileList.Name = "tsmiMakeValidFileList";
			this.tsmiMakeValidFileList.Size = new System.Drawing.Size(288, 22);
			this.tsmiMakeValidFileList.Text = "Сохранить список Валидных файлов";
			this.tsmiMakeValidFileList.Click += new System.EventHandler(this.TsmiMakeValidFileListClick);
			// 
			// tsSep4
			// 
			this.tsSep4.Name = "tsSep4";
			this.tsSep4.Size = new System.Drawing.Size(6, 31);
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
			this.pScanDir.Controls.Add(this.cboxScanSubDir);
			this.pScanDir.Controls.Add(this.tboxSourceDir);
			this.pScanDir.Controls.Add(this.lblDir);
			this.pScanDir.Dock = System.Windows.Forms.DockStyle.Top;
			this.pScanDir.Location = new System.Drawing.Point(0, 0);
			this.pScanDir.Margin = new System.Windows.Forms.Padding(0);
			this.pScanDir.Name = "pScanDir";
			this.pScanDir.Size = new System.Drawing.Size(831, 32);
			this.pScanDir.TabIndex = 9;
			// 
			// cboxScanSubDir
			// 
			this.cboxScanSubDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cboxScanSubDir.Checked = true;
			this.cboxScanSubDir.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cboxScanSubDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.cboxScanSubDir.ForeColor = System.Drawing.Color.Navy;
			this.cboxScanSubDir.Location = new System.Drawing.Point(652, 5);
			this.cboxScanSubDir.Name = "cboxScanSubDir";
			this.cboxScanSubDir.Size = new System.Drawing.Size(172, 24);
			this.cboxScanSubDir.TabIndex = 5;
			this.cboxScanSubDir.Text = "Сканировать и подпапки";
			this.cboxScanSubDir.UseVisualStyleBackColor = true;
			// 
			// tboxSourceDir
			// 
			this.tboxSourceDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxSourceDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tboxSourceDir.Location = new System.Drawing.Point(162, 5);
			this.tboxSourceDir.Name = "tboxSourceDir";
			this.tboxSourceDir.Size = new System.Drawing.Size(484, 20);
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
			// tcResult
			// 
			this.tcResult.Controls.Add(this.tpNotValid);
			this.tcResult.Controls.Add(this.tpValid);
			this.tcResult.Controls.Add(this.tpNotFB2Files);
			this.tcResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tcResult.ImageList = this.imgl16;
			this.tcResult.Location = new System.Drawing.Point(0, 0);
			this.tcResult.Margin = new System.Windows.Forms.Padding(0);
			this.tcResult.Name = "tcResult";
			this.tcResult.SelectedIndex = 0;
			this.tcResult.Size = new System.Drawing.Size(831, 299);
			this.tcResult.TabIndex = 16;
			// 
			// tpNotValid
			// 
			this.tpNotValid.Controls.Add(this.gbFB2NotValidFiles);
			this.tpNotValid.Controls.Add(this.pErrors);
			this.tpNotValid.Controls.Add(this.listViewNotValid);
			this.tpNotValid.ImageIndex = 0;
			this.tpNotValid.Location = new System.Drawing.Point(4, 23);
			this.tpNotValid.Name = "tpNotValid";
			this.tpNotValid.Padding = new System.Windows.Forms.Padding(3);
			this.tpNotValid.Size = new System.Drawing.Size(823, 272);
			this.tpNotValid.TabIndex = 0;
			this.tpNotValid.Text = " Не валидные fb2-файлы ";
			this.tpNotValid.UseVisualStyleBackColor = true;
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
			this.gbFB2NotValidFiles.Size = new System.Drawing.Size(817, 76);
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
			this.btnFB2NotValidMoveTo.Image = ((System.Drawing.Image)(resources.GetObject("btnFB2NotValidMoveTo.Image")));
			this.btnFB2NotValidMoveTo.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnFB2NotValidMoveTo.Location = new System.Drawing.Point(112, 44);
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
			this.tboxFB2NotValidDirMoveTo.Location = new System.Drawing.Point(155, 46);
			this.tboxFB2NotValidDirMoveTo.Name = "tboxFB2NotValidDirMoveTo";
			this.tboxFB2NotValidDirMoveTo.Size = new System.Drawing.Size(644, 20);
			this.tboxFB2NotValidDirMoveTo.TabIndex = 7;
			this.tboxFB2NotValidDirMoveTo.TextChanged += new System.EventHandler(this.TboxFB2NotValidDirMoveToTextChanged);
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
			this.btnFB2NotValidCopyTo.Image = ((System.Drawing.Image)(resources.GetObject("btnFB2NotValidCopyTo.Image")));
			this.btnFB2NotValidCopyTo.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnFB2NotValidCopyTo.Location = new System.Drawing.Point(112, 18);
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
			this.tboxFB2NotValidDirCopyTo.Location = new System.Drawing.Point(155, 20);
			this.tboxFB2NotValidDirCopyTo.Name = "tboxFB2NotValidDirCopyTo";
			this.tboxFB2NotValidDirCopyTo.Size = new System.Drawing.Size(644, 20);
			this.tboxFB2NotValidDirCopyTo.TabIndex = 0;
			this.tboxFB2NotValidDirCopyTo.TextChanged += new System.EventHandler(this.TboxFB2NotValidDirCopyToTextChanged);
			// 
			// pErrors
			// 
			this.pErrors.Controls.Add(this.rеboxNotValid);
			this.pErrors.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pErrors.Location = new System.Drawing.Point(3, 180);
			this.pErrors.Name = "pErrors";
			this.pErrors.Size = new System.Drawing.Size(817, 89);
			this.pErrors.TabIndex = 1;
			// 
			// rеboxNotValid
			// 
			this.rеboxNotValid.BackColor = System.Drawing.SystemColors.Window;
			this.rеboxNotValid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rеboxNotValid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rеboxNotValid.Location = new System.Drawing.Point(0, 0);
			this.rеboxNotValid.Name = "rеboxNotValid";
			this.rеboxNotValid.ReadOnly = true;
			this.rеboxNotValid.Size = new System.Drawing.Size(817, 89);
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
			this.listViewNotValid.ContextMenuStrip = this.cmsFB2;
			this.listViewNotValid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.listViewNotValid.FullRowSelect = true;
			this.listViewNotValid.GridLines = true;
			this.listViewNotValid.Location = new System.Drawing.Point(3, 83);
			this.listViewNotValid.MultiSelect = false;
			this.listViewNotValid.Name = "listViewNotValid";
			this.listViewNotValid.ShowItemToolTips = true;
			this.listViewNotValid.Size = new System.Drawing.Size(817, 91);
			this.listViewNotValid.TabIndex = 0;
			this.listViewNotValid.UseCompatibleStateImageBehavior = false;
			this.listViewNotValid.View = System.Windows.Forms.View.Details;
			this.listViewNotValid.SelectedIndexChanged += new System.EventHandler(this.ListViewNotValidSelectedIndexChanged);
			this.listViewNotValid.DoubleClick += new System.EventHandler(this.ListViewNotValidDoubleClick);
			this.listViewNotValid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ListViewNotValidKeyPress);
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
			// cmsFB2
			// 
			this.cmsFB2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsmiFileReValidate,
									this.tsmi3,
									this.tsmiEditInTextEditor,
									this.tsmiEditInFB2Editor,
									this.tsmi1,
									this.tsmiViewInReader,
									this.tsmi2,
									this.tsmiOpenFileDir,
									this.tsmiDeleteFileFromDisk});
			this.cmsFB2.Name = "cmsValidator";
			this.cmsFB2.Size = new System.Drawing.Size(293, 154);
			// 
			// tsmiFileReValidate
			// 
			this.tsmiFileReValidate.Image = ((System.Drawing.Image)(resources.GetObject("tsmiFileReValidate.Image")));
			this.tsmiFileReValidate.Name = "tsmiFileReValidate";
			this.tsmiFileReValidate.Size = new System.Drawing.Size(292, 22);
			this.tsmiFileReValidate.Text = "Проверить файл заново (валидация)";
			this.tsmiFileReValidate.Click += new System.EventHandler(this.TsmiFileReValidateClick);
			// 
			// tsmi3
			// 
			this.tsmi3.Name = "tsmi3";
			this.tsmi3.Size = new System.Drawing.Size(289, 6);
			// 
			// tsmiEditInTextEditor
			// 
			this.tsmiEditInTextEditor.Image = ((System.Drawing.Image)(resources.GetObject("tsmiEditInTextEditor.Image")));
			this.tsmiEditInTextEditor.Name = "tsmiEditInTextEditor";
			this.tsmiEditInTextEditor.Size = new System.Drawing.Size(292, 22);
			this.tsmiEditInTextEditor.Text = "Редактировать в текстовом редакторе";
			this.tsmiEditInTextEditor.Click += new System.EventHandler(this.TsmiEditInTextEditorClick);
			// 
			// tsmiEditInFB2Editor
			// 
			this.tsmiEditInFB2Editor.Image = ((System.Drawing.Image)(resources.GetObject("tsmiEditInFB2Editor.Image")));
			this.tsmiEditInFB2Editor.Name = "tsmiEditInFB2Editor";
			this.tsmiEditInFB2Editor.Size = new System.Drawing.Size(292, 22);
			this.tsmiEditInFB2Editor.Text = "Редактировать в fb2-редакторе";
			this.tsmiEditInFB2Editor.Click += new System.EventHandler(this.TsmiEditInFB2EditorClick);
			// 
			// tsmi1
			// 
			this.tsmi1.Name = "tsmi1";
			this.tsmi1.Size = new System.Drawing.Size(289, 6);
			// 
			// tsmiViewInReader
			// 
			this.tsmiViewInReader.Image = ((System.Drawing.Image)(resources.GetObject("tsmiViewInReader.Image")));
			this.tsmiViewInReader.Name = "tsmiViewInReader";
			this.tsmiViewInReader.Size = new System.Drawing.Size(292, 22);
			this.tsmiViewInReader.Text = "Запустить в fb2-читалке (Просмотр)";
			this.tsmiViewInReader.Click += new System.EventHandler(this.TsmiVienInReaderClick);
			// 
			// tsmi2
			// 
			this.tsmi2.Name = "tsmi2";
			this.tsmi2.Size = new System.Drawing.Size(289, 6);
			// 
			// tsmiOpenFileDir
			// 
			this.tsmiOpenFileDir.Image = ((System.Drawing.Image)(resources.GetObject("tsmiOpenFileDir.Image")));
			this.tsmiOpenFileDir.Name = "tsmiOpenFileDir";
			this.tsmiOpenFileDir.Size = new System.Drawing.Size(292, 22);
			this.tsmiOpenFileDir.Text = "Открыть папку для выделенного файла";
			this.tsmiOpenFileDir.Click += new System.EventHandler(this.TsmiOpenFileDirClick);
			// 
			// tsmiDeleteFileFromDisk
			// 
			this.tsmiDeleteFileFromDisk.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDeleteFileFromDisk.Image")));
			this.tsmiDeleteFileFromDisk.Name = "tsmiDeleteFileFromDisk";
			this.tsmiDeleteFileFromDisk.Size = new System.Drawing.Size(292, 22);
			this.tsmiDeleteFileFromDisk.Text = "Удалить файл с диска";
			this.tsmiDeleteFileFromDisk.Click += new System.EventHandler(this.TsmiDeleteFileFromDiskClick);
			// 
			// tpValid
			// 
			this.tpValid.Controls.Add(this.pValidLV);
			this.tpValid.Controls.Add(this.gbFB2Valid);
			this.tpValid.ImageIndex = 1;
			this.tpValid.Location = new System.Drawing.Point(4, 23);
			this.tpValid.Name = "tpValid";
			this.tpValid.Padding = new System.Windows.Forms.Padding(3);
			this.tpValid.Size = new System.Drawing.Size(823, 272);
			this.tpValid.TabIndex = 1;
			this.tpValid.Text = " Валидные fb2-файлы ";
			this.tpValid.UseVisualStyleBackColor = true;
			// 
			// pValidLV
			// 
			this.pValidLV.Controls.Add(this.listViewValid);
			this.pValidLV.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pValidLV.Location = new System.Drawing.Point(3, 79);
			this.pValidLV.Name = "pValidLV";
			this.pValidLV.Size = new System.Drawing.Size(817, 190);
			this.pValidLV.TabIndex = 9;
			// 
			// listViewValid
			// 
			this.listViewValid.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.chValidFile,
									this.chValidLenght});
			this.listViewValid.ContextMenuStrip = this.cmsFB2;
			this.listViewValid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewValid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.listViewValid.FullRowSelect = true;
			this.listViewValid.GridLines = true;
			this.listViewValid.Location = new System.Drawing.Point(0, 0);
			this.listViewValid.MultiSelect = false;
			this.listViewValid.Name = "listViewValid";
			this.listViewValid.ShowItemToolTips = true;
			this.listViewValid.Size = new System.Drawing.Size(817, 190);
			this.listViewValid.TabIndex = 1;
			this.listViewValid.UseCompatibleStateImageBehavior = false;
			this.listViewValid.View = System.Windows.Forms.View.Details;
			this.listViewValid.SelectedIndexChanged += new System.EventHandler(this.ListViewValidSelectedIndexChanged);
			this.listViewValid.DoubleClick += new System.EventHandler(this.ListViewNotValidDoubleClick);
			this.listViewValid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ListViewNotValidKeyPress);
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
			this.gbFB2Valid.Size = new System.Drawing.Size(817, 76);
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
			this.btnFB2ValidMoveTo.Image = ((System.Drawing.Image)(resources.GetObject("btnFB2ValidMoveTo.Image")));
			this.btnFB2ValidMoveTo.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnFB2ValidMoveTo.Location = new System.Drawing.Point(112, 44);
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
			this.tboxFB2ValidDirMoveTo.Location = new System.Drawing.Point(155, 46);
			this.tboxFB2ValidDirMoveTo.Name = "tboxFB2ValidDirMoveTo";
			this.tboxFB2ValidDirMoveTo.Size = new System.Drawing.Size(644, 20);
			this.tboxFB2ValidDirMoveTo.TabIndex = 7;
			this.tboxFB2ValidDirMoveTo.TextChanged += new System.EventHandler(this.TboxFB2ValidDirMoveToTextChanged);
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
			this.btnFB2ValidCopyTo.Image = ((System.Drawing.Image)(resources.GetObject("btnFB2ValidCopyTo.Image")));
			this.btnFB2ValidCopyTo.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnFB2ValidCopyTo.Location = new System.Drawing.Point(112, 18);
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
			this.tboxFB2ValidDirCopyTo.Location = new System.Drawing.Point(155, 20);
			this.tboxFB2ValidDirCopyTo.Name = "tboxFB2ValidDirCopyTo";
			this.tboxFB2ValidDirCopyTo.Size = new System.Drawing.Size(644, 20);
			this.tboxFB2ValidDirCopyTo.TabIndex = 0;
			this.tboxFB2ValidDirCopyTo.TextChanged += new System.EventHandler(this.TboxFB2ValidDirCopyToTextChanged);
			// 
			// tpNotFB2Files
			// 
			this.tpNotFB2Files.Controls.Add(this.pNotValidLV);
			this.tpNotFB2Files.Controls.Add(this.gbNotFB2);
			this.tpNotFB2Files.Location = new System.Drawing.Point(4, 23);
			this.tpNotFB2Files.Name = "tpNotFB2Files";
			this.tpNotFB2Files.Padding = new System.Windows.Forms.Padding(3);
			this.tpNotFB2Files.Size = new System.Drawing.Size(823, 272);
			this.tpNotFB2Files.TabIndex = 2;
			this.tpNotFB2Files.Text = " Не fb2-файлы ";
			this.tpNotFB2Files.UseVisualStyleBackColor = true;
			// 
			// pNotValidLV
			// 
			this.pNotValidLV.Controls.Add(this.listViewNotFB2);
			this.pNotValidLV.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pNotValidLV.Location = new System.Drawing.Point(3, 79);
			this.pNotValidLV.Name = "pNotValidLV";
			this.pNotValidLV.Size = new System.Drawing.Size(817, 190);
			this.pNotValidLV.TabIndex = 10;
			// 
			// listViewNotFB2
			// 
			this.listViewNotFB2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeader1,
									this.columnHeader2,
									this.columnHeader3});
			this.listViewNotFB2.ContextMenuStrip = this.cmsNotFB2;
			this.listViewNotFB2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewNotFB2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.listViewNotFB2.FullRowSelect = true;
			this.listViewNotFB2.GridLines = true;
			this.listViewNotFB2.Location = new System.Drawing.Point(0, 0);
			this.listViewNotFB2.MultiSelect = false;
			this.listViewNotFB2.Name = "listViewNotFB2";
			this.listViewNotFB2.ShowItemToolTips = true;
			this.listViewNotFB2.Size = new System.Drawing.Size(817, 190);
			this.listViewNotFB2.TabIndex = 2;
			this.listViewNotFB2.UseCompatibleStateImageBehavior = false;
			this.listViewNotFB2.View = System.Windows.Forms.View.Details;
			this.listViewNotFB2.DoubleClick += new System.EventHandler(this.TsmiOpenFileDirClick);
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
			// cmsNotFB2
			// 
			this.cmsNotFB2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsmiOpenNotFB2FileDir,
									this.tsmiDeleteNotFB2FromDisk});
			this.cmsNotFB2.Name = "cmsValidator";
			this.cmsNotFB2.Size = new System.Drawing.Size(293, 48);
			// 
			// tsmiOpenNotFB2FileDir
			// 
			this.tsmiOpenNotFB2FileDir.Image = ((System.Drawing.Image)(resources.GetObject("tsmiOpenNotFB2FileDir.Image")));
			this.tsmiOpenNotFB2FileDir.Name = "tsmiOpenNotFB2FileDir";
			this.tsmiOpenNotFB2FileDir.Size = new System.Drawing.Size(292, 22);
			this.tsmiOpenNotFB2FileDir.Text = "Открыть папку для выделенного файла";
			this.tsmiOpenNotFB2FileDir.Click += new System.EventHandler(this.TsmiOpenFileDirClick);
			// 
			// tsmiDeleteNotFB2FromDisk
			// 
			this.tsmiDeleteNotFB2FromDisk.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDeleteNotFB2FromDisk.Image")));
			this.tsmiDeleteNotFB2FromDisk.Name = "tsmiDeleteNotFB2FromDisk";
			this.tsmiDeleteNotFB2FromDisk.Size = new System.Drawing.Size(292, 22);
			this.tsmiDeleteNotFB2FromDisk.Text = "Удалить файл с диска";
			this.tsmiDeleteNotFB2FromDisk.Click += new System.EventHandler(this.TsmiDeleteFileFromDiskClick);
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
			this.gbNotFB2.Size = new System.Drawing.Size(817, 76);
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
			this.btnNotFB2MoveTo.Image = ((System.Drawing.Image)(resources.GetObject("btnNotFB2MoveTo.Image")));
			this.btnNotFB2MoveTo.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnNotFB2MoveTo.Location = new System.Drawing.Point(112, 44);
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
			this.tboxNotFB2DirMoveTo.Location = new System.Drawing.Point(155, 46);
			this.tboxNotFB2DirMoveTo.Name = "tboxNotFB2DirMoveTo";
			this.tboxNotFB2DirMoveTo.Size = new System.Drawing.Size(644, 20);
			this.tboxNotFB2DirMoveTo.TabIndex = 7;
			this.tboxNotFB2DirMoveTo.TextChanged += new System.EventHandler(this.TboxNotFB2DirMoveToTextChanged);
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
			this.btnNotFB2CopyTo.Image = ((System.Drawing.Image)(resources.GetObject("btnNotFB2CopyTo.Image")));
			this.btnNotFB2CopyTo.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnNotFB2CopyTo.Location = new System.Drawing.Point(112, 18);
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
			this.tboxNotFB2DirCopyTo.Location = new System.Drawing.Point(155, 20);
			this.tboxNotFB2DirCopyTo.Name = "tboxNotFB2DirCopyTo";
			this.tboxNotFB2DirCopyTo.Size = new System.Drawing.Size(644, 20);
			this.tboxNotFB2DirCopyTo.TabIndex = 0;
			this.tboxNotFB2DirCopyTo.TextChanged += new System.EventHandler(this.TboxNotFB2DirCopyToTextChanged);
			// 
			// imgl16
			// 
			this.imgl16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgl16.ImageStream")));
			this.imgl16.TransparentColor = System.Drawing.Color.Transparent;
			this.imgl16.Images.SetKeyName(0, "notvalid.ICO");
			this.imgl16.Images.SetKeyName(1, "valid.ICO");
			// 
			// ssProgress
			// 
			this.ssProgress.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsslblProgress,
									this.tsProgressBar});
			this.ssProgress.Location = new System.Drawing.Point(0, 478);
			this.ssProgress.Name = "ssProgress";
			this.ssProgress.Size = new System.Drawing.Size(831, 22);
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
			this.tlCentral.ColumnCount = 1;
			this.tlCentral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlCentral.Controls.Add(this.pScanDir, 0, 0);
			this.tlCentral.Dock = System.Windows.Forms.DockStyle.Top;
			this.tlCentral.Location = new System.Drawing.Point(0, 31);
			this.tlCentral.Margin = new System.Windows.Forms.Padding(0);
			this.tlCentral.Name = "tlCentral";
			this.tlCentral.RowCount = 1;
			this.tlCentral.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlCentral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
			this.tlCentral.Size = new System.Drawing.Size(831, 32);
			this.tlCentral.TabIndex = 19;
			// 
			// fbdDir
			// 
			this.fbdDir.Description = "Укажите папку для проверки fb2-файлов";
			// 
			// sfdReport
			// 
			this.sfdReport.RestoreDirectory = true;
			this.sfdReport.Title = "Укажите название файла отчета";
			// 
			// lvFilesCount
			// 
			this.lvFilesCount.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left)));
			this.lvFilesCount.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeader6,
									this.columnHeader7});
			this.lvFilesCount.GridLines = true;
			this.lvFilesCount.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
									listViewItem31,
									listViewItem32,
									listViewItem33,
									listViewItem34,
									listViewItem35,
									listViewItem36});
			this.lvFilesCount.Location = new System.Drawing.Point(0, 0);
			this.lvFilesCount.Name = "lvFilesCount";
			this.lvFilesCount.Size = new System.Drawing.Size(223, 116);
			this.lvFilesCount.TabIndex = 20;
			this.lvFilesCount.UseCompatibleStateImageBehavior = false;
			this.lvFilesCount.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Папки и файлы";
			this.columnHeader6.Width = 120;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Количество";
			this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader7.Width = 80;
			// 
			// pInfo
			// 
			this.pInfo.Controls.Add(this.gboxCopyMoveOptions);
			this.pInfo.Controls.Add(this.lvFilesCount);
			this.pInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pInfo.Location = new System.Drawing.Point(0, 362);
			this.pInfo.Name = "pInfo";
			this.pInfo.Size = new System.Drawing.Size(831, 116);
			this.pInfo.TabIndex = 21;
			// 
			// gboxCopyMoveOptions
			// 
			this.gboxCopyMoveOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.gboxCopyMoveOptions.Controls.Add(this.chBoxAddBookID);
			this.gboxCopyMoveOptions.Controls.Add(this.cboxExistFile);
			this.gboxCopyMoveOptions.Controls.Add(this.lblExistFile);
			this.gboxCopyMoveOptions.Location = new System.Drawing.Point(229, 3);
			this.gboxCopyMoveOptions.Name = "gboxCopyMoveOptions";
			this.gboxCopyMoveOptions.Size = new System.Drawing.Size(597, 112);
			this.gboxCopyMoveOptions.TabIndex = 21;
			this.gboxCopyMoveOptions.TabStop = false;
			this.gboxCopyMoveOptions.Text = " Настройки для Копирования / Перемещения файлов ";
			// 
			// chBoxAddBookID
			// 
			this.chBoxAddBookID.Enabled = false;
			this.chBoxAddBookID.Location = new System.Drawing.Point(228, 45);
			this.chBoxAddBookID.Name = "chBoxAddBookID";
			this.chBoxAddBookID.Size = new System.Drawing.Size(246, 18);
			this.chBoxAddBookID.TabIndex = 19;
			this.chBoxAddBookID.Text = " Добавить ID Книги к имени файла";
			this.chBoxAddBookID.UseVisualStyleBackColor = true;
			// 
			// cboxExistFile
			// 
			this.cboxExistFile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxExistFile.FormattingEnabled = true;
			this.cboxExistFile.Items.AddRange(new object[] {
									"Заменить существующий файл новым",
									"Добавить к новому файлу очередной номер",
									"Добавить к новому файлу дату и время"});
			this.cboxExistFile.Location = new System.Drawing.Point(228, 21);
			this.cboxExistFile.Name = "cboxExistFile";
			this.cboxExistFile.Size = new System.Drawing.Size(260, 21);
			this.cboxExistFile.TabIndex = 18;
			this.cboxExistFile.SelectedIndexChanged += new System.EventHandler(this.CboxExistFileSelectedIndexChanged);
			// 
			// lblExistFile
			// 
			this.lblExistFile.AutoSize = true;
			this.lblExistFile.Location = new System.Drawing.Point(15, 24);
			this.lblExistFile.Name = "lblExistFile";
			this.lblExistFile.Size = new System.Drawing.Size(212, 13);
			this.lblExistFile.TabIndex = 17;
			this.lblExistFile.Text = "Одинаковые файлы в папке-приемнике:";
			// 
			// pCentral
			// 
			this.pCentral.Controls.Add(this.tcResult);
			this.pCentral.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pCentral.Location = new System.Drawing.Point(0, 63);
			this.pCentral.Name = "pCentral";
			this.pCentral.Size = new System.Drawing.Size(831, 299);
			this.pCentral.TabIndex = 22;
			// 
			// cmsArchive
			// 
			this.cmsArchive.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsmiFileAndArchiveReValidate,
									this.tsmi5,
									this.tsmiOpenInArchivator,
									this.tsmi4,
									this.tsmiOpenArchiveDir,
									this.tsmiFileDeleteFromDisk});
			this.cmsArchive.Name = "cmsValidator";
			this.cmsArchive.Size = new System.Drawing.Size(297, 104);
			// 
			// tsmiFileAndArchiveReValidate
			// 
			this.tsmiFileAndArchiveReValidate.Image = ((System.Drawing.Image)(resources.GetObject("tsmiFileAndArchiveReValidate.Image")));
			this.tsmiFileAndArchiveReValidate.Name = "tsmiFileAndArchiveReValidate";
			this.tsmiFileAndArchiveReValidate.Size = new System.Drawing.Size(296, 22);
			this.tsmiFileAndArchiveReValidate.Text = "Проверить файл заново (валидация)";
			this.tsmiFileAndArchiveReValidate.Click += new System.EventHandler(this.TsmiFileReValidateClick);
			// 
			// tsmi5
			// 
			this.tsmi5.Name = "tsmi5";
			this.tsmi5.Size = new System.Drawing.Size(293, 6);
			// 
			// tsmiOpenInArchivator
			// 
			this.tsmiOpenInArchivator.Image = ((System.Drawing.Image)(resources.GetObject("tsmiOpenInArchivator.Image")));
			this.tsmiOpenInArchivator.Name = "tsmiOpenInArchivator";
			this.tsmiOpenInArchivator.Size = new System.Drawing.Size(296, 22);
			this.tsmiOpenInArchivator.Text = "Открыть файл в архиваторе";
			this.tsmiOpenInArchivator.Click += new System.EventHandler(this.TsmiOpenFileInArchivatorClick);
			// 
			// tsmi4
			// 
			this.tsmi4.Name = "tsmi4";
			this.tsmi4.Size = new System.Drawing.Size(293, 6);
			// 
			// tsmiOpenArchiveDir
			// 
			this.tsmiOpenArchiveDir.Image = ((System.Drawing.Image)(resources.GetObject("tsmiOpenArchiveDir.Image")));
			this.tsmiOpenArchiveDir.Name = "tsmiOpenArchiveDir";
			this.tsmiOpenArchiveDir.Size = new System.Drawing.Size(296, 22);
			this.tsmiOpenArchiveDir.Text = "Открыть папку для выделенного архива";
			this.tsmiOpenArchiveDir.Click += new System.EventHandler(this.TsmiOpenFileDirClick);
			// 
			// tsmiFileDeleteFromDisk
			// 
			this.tsmiFileDeleteFromDisk.Image = ((System.Drawing.Image)(resources.GetObject("tsmiFileDeleteFromDisk.Image")));
			this.tsmiFileDeleteFromDisk.Name = "tsmiFileDeleteFromDisk";
			this.tsmiFileDeleteFromDisk.Size = new System.Drawing.Size(296, 22);
			this.tsmiFileDeleteFromDisk.Text = "Удалить упакованный файл с диска";
			this.tsmiFileDeleteFromDisk.Click += new System.EventHandler(this.TsmiDeleteFileFromDiskClick);
			// 
			// SFBTpFB2Validator
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pCentral);
			this.Controls.Add(this.pInfo);
			this.Controls.Add(this.tlCentral);
			this.Controls.Add(this.ssProgress);
			this.Controls.Add(this.tsValidator);
			this.Name = "SFBTpFB2Validator";
			this.Size = new System.Drawing.Size(831, 500);
			this.Tag = "validator";
			this.tsValidator.ResumeLayout(false);
			this.tsValidator.PerformLayout();
			this.pScanDir.ResumeLayout(false);
			this.pScanDir.PerformLayout();
			this.tcResult.ResumeLayout(false);
			this.tpNotValid.ResumeLayout(false);
			this.gbFB2NotValidFiles.ResumeLayout(false);
			this.gbFB2NotValidFiles.PerformLayout();
			this.pErrors.ResumeLayout(false);
			this.cmsFB2.ResumeLayout(false);
			this.tpValid.ResumeLayout(false);
			this.pValidLV.ResumeLayout(false);
			this.gbFB2Valid.ResumeLayout(false);
			this.gbFB2Valid.PerformLayout();
			this.tpNotFB2Files.ResumeLayout(false);
			this.pNotValidLV.ResumeLayout(false);
			this.cmsNotFB2.ResumeLayout(false);
			this.gbNotFB2.ResumeLayout(false);
			this.gbNotFB2.PerformLayout();
			this.ssProgress.ResumeLayout(false);
			this.ssProgress.PerformLayout();
			this.tlCentral.ResumeLayout(false);
			this.tlCentral.PerformLayout();
			this.pInfo.ResumeLayout(false);
			this.gboxCopyMoveOptions.ResumeLayout(false);
			this.gboxCopyMoveOptions.PerformLayout();
			this.pCentral.ResumeLayout(false);
			this.cmsArchive.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.CheckBox chBoxAddBookID;
		private System.Windows.Forms.FolderBrowserDialog fbdDir;
		private System.Windows.Forms.CheckBox cboxScanSubDir;
		private System.Windows.Forms.ImageList imgl16;
		private System.Windows.Forms.ToolStripMenuItem tsmiMakeNotValidFileList;
		private System.Windows.Forms.ToolStripMenuItem tsmiMakeValidFileList;
		private System.Windows.Forms.ToolStripSeparator tsSep4;
		private System.Windows.Forms.ToolStripDropDownButton tsddbtnMakeFileList;
		private System.Windows.Forms.ToolStripMenuItem tsmiDeleteNotFB2FromDisk;
		private System.Windows.Forms.ToolStripMenuItem tsmiOpenNotFB2FileDir;
		private System.Windows.Forms.ContextMenuStrip cmsNotFB2;
		private System.Windows.Forms.ToolStripMenuItem tsmiFileDeleteFromDisk;
		private System.Windows.Forms.ToolStripMenuItem tsmiDeleteFileFromDisk;
		private System.Windows.Forms.ToolStripMenuItem tsmiViewInReader;
		private System.Windows.Forms.ToolStripSeparator tsmi4;
		private System.Windows.Forms.ToolStripMenuItem tsmiFileAndArchiveReValidate;
		private System.Windows.Forms.ToolStripSeparator tsmi5;
		private System.Windows.Forms.ToolStripMenuItem tsmiFileReValidate;
		private System.Windows.Forms.ToolStripSeparator tsmi3;
		private System.Windows.Forms.ContextMenuStrip cmsFB2;
		private System.Windows.Forms.ToolStripMenuItem tsmiOpenArchiveDir;
		private System.Windows.Forms.ToolStripMenuItem tsmiOpenInArchivator;
		private System.Windows.Forms.ContextMenuStrip cmsArchive;
		private System.Windows.Forms.ToolStripMenuItem tsmiOpenFileDir;
		private System.Windows.Forms.ToolStripMenuItem tsmiEditInTextEditor;
		private System.Windows.Forms.ToolStripMenuItem tsmiEditInFB2Editor;
		private System.Windows.Forms.ToolStripSeparator tsmi2;
		private System.Windows.Forms.ToolStripSeparator tsmi1;
		private System.Windows.Forms.Label lblExistFile;
		private System.Windows.Forms.ComboBox cboxExistFile;
		private System.Windows.Forms.GroupBox gboxCopyMoveOptions;
		private System.Windows.Forms.Panel pCentral;
		private System.Windows.Forms.Panel pInfo;
		private System.Windows.Forms.ListView lvFilesCount;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.TabPage tpNotValid;
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
		private System.Windows.Forms.ToolStripSeparator tsSep1;
		private System.Windows.Forms.ToolStripButton tsbtnOpenDir;
		private System.Windows.Forms.GroupBox gbFB2NotValidFiles;
		private System.Windows.Forms.Label lblFB2NotValidFilesMoveDir;
		private System.Windows.Forms.Button btnFB2NotValidMoveTo;
		private System.Windows.Forms.TextBox tboxFB2NotValidDirMoveTo;
		private System.Windows.Forms.Label lblFB2NotValidFilesCopyDir;
		private System.Windows.Forms.Button btnFB2NotValidCopyTo;
		private System.Windows.Forms.TextBox tboxFB2NotValidDirCopyTo;
		private System.Windows.Forms.RichTextBox rеboxNotValid;
		private System.Windows.Forms.ListView listViewNotValid;
		private System.Windows.Forms.Panel pScanDir;
		private System.Windows.Forms.TableLayoutPanel tlCentral;
		private System.Windows.Forms.ToolStripStatusLabel tsslblProgress;
		private System.Windows.Forms.ToolStripProgressBar tsProgressBar;
		private System.Windows.Forms.StatusStrip ssProgress;
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
		private System.Windows.Forms.TabControl tcResult;
		private System.Windows.Forms.Label lblDir;
		private System.Windows.Forms.TextBox tboxSourceDir;
		private System.Windows.Forms.ToolStrip tsValidator;
	}
}
