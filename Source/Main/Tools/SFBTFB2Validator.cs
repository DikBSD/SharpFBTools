/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 13.03.2009
 * Time: 14:34
 * 
 * License: GPL 2.1
 */

using System;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Xml;
using System.Linq;
using System.Xml.Linq;

using Core.Misc;

using FB2Validator		= Core.FB2Parser.FB2Validator;
using ValidatorReports	= Core.Validator.ValidatorReports;
using SharpZipLibWorker = Core.Misc.SharpZipLibWorker;
using filesWorker		= Core.Misc.FilesWorker;

namespace SharpFBTools.Tools
{
	/// <summary>
	/// Description of SFBTpValidator.
	/// </summary>
	public partial class SFBTpFB2Validator : UserControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SFBTpFB2Validator));
			System.Windows.Forms.ListViewItem listViewItem16 = new System.Windows.Forms.ListViewItem(new string[] {
			"Всего папок",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem17 = new System.Windows.Forms.ListViewItem(new string[] {
			"Всего файлов",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem18 = new System.Windows.Forms.ListViewItem(new string[] {
			"fb2-файлов",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem19 = new System.Windows.Forms.ListViewItem(new string[] {
			"fb2 в .zip-архивах",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem20 = new System.Windows.Forms.ListViewItem(new string[] {
			"Другие файлы",
			"0"}, 0);
			this.tsValidator = new System.Windows.Forms.ToolStrip();
			this.tsbtnValidate = new System.Windows.Forms.ToolStripButton();
			this.tsbtnValidateStop = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnCopyFilesTo = new System.Windows.Forms.ToolStripButton();
			this.tsbtnMoveFilesTo = new System.Windows.Forms.ToolStripButton();
			this.tsbtnDeleteFiles = new System.Windows.Forms.ToolStripButton();
			this.tsbtnFilesWorkStop = new System.Windows.Forms.ToolStripButton();
			this.tsSep3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsddbtnMakeFileList = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsmiMakeNotValidFileList = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiMakeValidFileList = new System.Windows.Forms.ToolStripMenuItem();
			this.tsSep4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsddbtnMakeReport = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsmiReportAsHTML = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiReportAsFB2 = new System.Windows.Forms.ToolStripMenuItem();
			this.pScanDir = new System.Windows.Forms.Panel();
			this.buttonOpenDir = new System.Windows.Forms.Button();
			this.chBoxScanSubDir = new System.Windows.Forms.CheckBox();
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
			this.pViewError = new System.Windows.Forms.Panel();
			this.btnLoadList = new System.Windows.Forms.Button();
			this.btnSaveList = new System.Windows.Forms.Button();
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
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiFB2CheckedAll = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiFB2UnCheckedAll = new System.Windows.Forms.ToolStripMenuItem();
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
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiNotFB2CheckedAll = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiNotFB2UnCheckedAll = new System.Windows.Forms.ToolStripMenuItem();
			this.gbNotFB2 = new System.Windows.Forms.GroupBox();
			this.lblNotFB2FilesMoveDir = new System.Windows.Forms.Label();
			this.btnNotFB2MoveTo = new System.Windows.Forms.Button();
			this.tboxNotFB2DirMoveTo = new System.Windows.Forms.TextBox();
			this.lblNotFB2FilesCopyDir = new System.Windows.Forms.Label();
			this.btnNotFB2CopyTo = new System.Windows.Forms.Button();
			this.tboxNotFB2DirCopyTo = new System.Windows.Forms.TextBox();
			this.tpOptions = new System.Windows.Forms.TabPage();
			this.pArchivator = new System.Windows.Forms.Panel();
			this.lblArchivatorPath = new System.Windows.Forms.Label();
			this.tboxArchivatorPath = new System.Windows.Forms.TextBox();
			this.btnArchivatorPath = new System.Windows.Forms.Button();
			this.gboxValidatorPE = new System.Windows.Forms.GroupBox();
			this.cboxValidatorForZipPE = new System.Windows.Forms.ComboBox();
			this.cboxValidatorForFB2PE = new System.Windows.Forms.ComboBox();
			this.lblValidatorForFB2ArchivePE = new System.Windows.Forms.Label();
			this.lblValidatorForFB2PE = new System.Windows.Forms.Label();
			this.gboxValidatorDoubleClick = new System.Windows.Forms.GroupBox();
			this.cboxValidatorForZip = new System.Windows.Forms.ComboBox();
			this.cboxValidatorForFB2 = new System.Windows.Forms.ComboBox();
			this.lblValidatorForFB2Archive = new System.Windows.Forms.Label();
			this.lblValidatorForFB2 = new System.Windows.Forms.Label();
			this.gboxCopyMoveOptions = new System.Windows.Forms.GroupBox();
			this.cboxExistFile = new System.Windows.Forms.ComboBox();
			this.lblExistFile = new System.Windows.Forms.Label();
			this.imgl16 = new System.Windows.Forms.ImageList(this.components);
			this.ssProgress = new System.Windows.Forms.StatusStrip();
			this.tsslblProgress = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.tlCentral = new System.Windows.Forms.TableLayoutPanel();
			this.pGenres = new System.Windows.Forms.Panel();
			this.rbtnFB22 = new System.Windows.Forms.RadioButton();
			this.rbtnFB2Librusec = new System.Windows.Forms.RadioButton();
			this.lblFMFSGenres = new System.Windows.Forms.Label();
			this.fbdDir = new System.Windows.Forms.FolderBrowserDialog();
			this.sfdReport = new System.Windows.Forms.SaveFileDialog();
			this.lvFilesCount = new System.Windows.Forms.ListView();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.pInfo = new System.Windows.Forms.Panel();
			this.chBoxViewProgress = new System.Windows.Forms.CheckBox();
			this.pCentral = new System.Windows.Forms.Panel();
			this.cmsArchive = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiFileAndArchiveReValidate = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmi5 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiOpenInArchivator = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmi4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiOpenArchiveDir = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiFileDeleteFromDisk = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiArchiveCheckedAll = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiArchiveUnCheckedAll = new System.Windows.Forms.ToolStripMenuItem();
			this.sfdLoadList = new System.Windows.Forms.OpenFileDialog();
			this.ofDlg = new System.Windows.Forms.OpenFileDialog();
			this.tsValidator.SuspendLayout();
			this.pScanDir.SuspendLayout();
			this.tcResult.SuspendLayout();
			this.tpNotValid.SuspendLayout();
			this.gbFB2NotValidFiles.SuspendLayout();
			this.pErrors.SuspendLayout();
			this.pViewError.SuspendLayout();
			this.cmsFB2.SuspendLayout();
			this.tpValid.SuspendLayout();
			this.pValidLV.SuspendLayout();
			this.gbFB2Valid.SuspendLayout();
			this.tpNotFB2Files.SuspendLayout();
			this.pNotValidLV.SuspendLayout();
			this.cmsNotFB2.SuspendLayout();
			this.gbNotFB2.SuspendLayout();
			this.tpOptions.SuspendLayout();
			this.pArchivator.SuspendLayout();
			this.gboxValidatorPE.SuspendLayout();
			this.gboxValidatorDoubleClick.SuspendLayout();
			this.gboxCopyMoveOptions.SuspendLayout();
			this.ssProgress.SuspendLayout();
			this.tlCentral.SuspendLayout();
			this.pGenres.SuspendLayout();
			this.pInfo.SuspendLayout();
			this.pCentral.SuspendLayout();
			this.cmsArchive.SuspendLayout();
			this.SuspendLayout();
			// 
			// tsValidator
			// 
			this.tsValidator.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tsValidator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.tsbtnValidate,
			this.tsbtnValidateStop,
			this.toolStripSeparator4,
			this.tsbtnCopyFilesTo,
			this.tsbtnMoveFilesTo,
			this.tsbtnDeleteFiles,
			this.tsbtnFilesWorkStop,
			this.tsSep3,
			this.tsddbtnMakeFileList,
			this.tsSep4,
			this.tsddbtnMakeReport});
			this.tsValidator.Location = new System.Drawing.Point(0, 0);
			this.tsValidator.Name = "tsValidator";
			this.tsValidator.Size = new System.Drawing.Size(1401, 31);
			this.tsValidator.TabIndex = 0;
			// 
			// tsbtnValidate
			// 
			this.tsbtnValidate.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnValidate.Image")));
			this.tsbtnValidate.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnValidate.Name = "tsbtnValidate";
			this.tsbtnValidate.Size = new System.Drawing.Size(113, 28);
			this.tsbtnValidate.Text = "Валидация";
			this.tsbtnValidate.Click += new System.EventHandler(this.TsbtnValidateClick);
			// 
			// tsbtnValidateStop
			// 
			this.tsbtnValidateStop.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnValidateStop.Image")));
			this.tsbtnValidateStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnValidateStop.Name = "tsbtnValidateStop";
			this.tsbtnValidateStop.Size = new System.Drawing.Size(118, 28);
			this.tsbtnValidateStop.Text = "Остановить";
			this.tsbtnValidateStop.Click += new System.EventHandler(this.TsbtnValidateStopClick);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 31);
			// 
			// tsbtnCopyFilesTo
			// 
			this.tsbtnCopyFilesTo.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnCopyFilesTo.Image")));
			this.tsbtnCopyFilesTo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnCopyFilesTo.Name = "tsbtnCopyFilesTo";
			this.tsbtnCopyFilesTo.Size = new System.Drawing.Size(121, 28);
			this.tsbtnCopyFilesTo.Text = "Копировать";
			this.tsbtnCopyFilesTo.ToolTipText = "Копировать файлы (для выбранной вкладки)";
			this.tsbtnCopyFilesTo.Click += new System.EventHandler(this.TsbtnCopyFilesToClick);
			// 
			// tsbtnMoveFilesTo
			// 
			this.tsbtnMoveFilesTo.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnMoveFilesTo.Image")));
			this.tsbtnMoveFilesTo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnMoveFilesTo.Name = "tsbtnMoveFilesTo";
			this.tsbtnMoveFilesTo.Size = new System.Drawing.Size(128, 28);
			this.tsbtnMoveFilesTo.Text = "Переместить";
			this.tsbtnMoveFilesTo.ToolTipText = "Переместить файлы (для  выбранной вкладки)";
			this.tsbtnMoveFilesTo.Click += new System.EventHandler(this.TsbtnMoveFilesToClick);
			// 
			// tsbtnDeleteFiles
			// 
			this.tsbtnDeleteFiles.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnDeleteFiles.Image")));
			this.tsbtnDeleteFiles.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnDeleteFiles.Name = "tsbtnDeleteFiles";
			this.tsbtnDeleteFiles.Size = new System.Drawing.Size(93, 28);
			this.tsbtnDeleteFiles.Text = "Удалить";
			this.tsbtnDeleteFiles.ToolTipText = "Удалить файлы (для  выбранной вкладки)";
			this.tsbtnDeleteFiles.Click += new System.EventHandler(this.TsbtnDeleteFilesClick);
			// 
			// tsbtnFilesWorkStop
			// 
			this.tsbtnFilesWorkStop.AutoToolTip = false;
			this.tsbtnFilesWorkStop.Enabled = false;
			this.tsbtnFilesWorkStop.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnFilesWorkStop.Image")));
			this.tsbtnFilesWorkStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnFilesWorkStop.Name = "tsbtnFilesWorkStop";
			this.tsbtnFilesWorkStop.Size = new System.Drawing.Size(118, 28);
			this.tsbtnFilesWorkStop.Text = "Остановить";
			this.tsbtnFilesWorkStop.Click += new System.EventHandler(this.TsbtnFilesWorkStopClick);
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
			this.tsddbtnMakeFileList.Size = new System.Drawing.Size(153, 28);
			this.tsddbtnMakeFileList.Text = "Список файлов";
			// 
			// tsmiMakeNotValidFileList
			// 
			this.tsmiMakeNotValidFileList.Image = ((System.Drawing.Image)(resources.GetObject("tsmiMakeNotValidFileList.Image")));
			this.tsmiMakeNotValidFileList.Name = "tsmiMakeNotValidFileList";
			this.tsmiMakeNotValidFileList.Size = new System.Drawing.Size(359, 26);
			this.tsmiMakeNotValidFileList.Text = "Сохранить список Не валидных файлов";
			this.tsmiMakeNotValidFileList.Click += new System.EventHandler(this.TsmiMakeNotValidFileListClick);
			// 
			// tsmiMakeValidFileList
			// 
			this.tsmiMakeValidFileList.Image = ((System.Drawing.Image)(resources.GetObject("tsmiMakeValidFileList.Image")));
			this.tsmiMakeValidFileList.Name = "tsmiMakeValidFileList";
			this.tsmiMakeValidFileList.Size = new System.Drawing.Size(359, 26);
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
			this.tsmiReportAsFB2});
			this.tsddbtnMakeReport.Image = ((System.Drawing.Image)(resources.GetObject("tsddbtnMakeReport.Image")));
			this.tsddbtnMakeReport.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsddbtnMakeReport.Name = "tsddbtnMakeReport";
			this.tsddbtnMakeReport.Size = new System.Drawing.Size(143, 28);
			this.tsddbtnMakeReport.Text = "Создать отчет";
			this.tsddbtnMakeReport.ToolTipText = "Создать отчет (для  выбранной вкладки)";
			// 
			// tsmiReportAsHTML
			// 
			this.tsmiReportAsHTML.Name = "tsmiReportAsHTML";
			this.tsmiReportAsHTML.Size = new System.Drawing.Size(186, 24);
			this.tsmiReportAsHTML.Text = "Как html-файл...";
			this.tsmiReportAsHTML.Click += new System.EventHandler(this.TsmiReportAsHTMLClick);
			// 
			// tsmiReportAsFB2
			// 
			this.tsmiReportAsFB2.Name = "tsmiReportAsFB2";
			this.tsmiReportAsFB2.Size = new System.Drawing.Size(186, 24);
			this.tsmiReportAsFB2.Text = "Как fb2-файл...";
			this.tsmiReportAsFB2.Click += new System.EventHandler(this.TsmiReportAsFB2Click);
			// 
			// pScanDir
			// 
			this.pScanDir.AutoSize = true;
			this.pScanDir.Controls.Add(this.buttonOpenDir);
			this.pScanDir.Controls.Add(this.chBoxScanSubDir);
			this.pScanDir.Controls.Add(this.tboxSourceDir);
			this.pScanDir.Controls.Add(this.lblDir);
			this.pScanDir.Dock = System.Windows.Forms.DockStyle.Top;
			this.pScanDir.Location = new System.Drawing.Point(0, 0);
			this.pScanDir.Margin = new System.Windows.Forms.Padding(0);
			this.pScanDir.Name = "pScanDir";
			this.pScanDir.Size = new System.Drawing.Size(1401, 41);
			this.pScanDir.TabIndex = 9;
			// 
			// buttonOpenDir
			// 
			this.buttonOpenDir.Image = ((System.Drawing.Image)(resources.GetObject("buttonOpenDir.Image")));
			this.buttonOpenDir.Location = new System.Drawing.Point(9, 4);
			this.buttonOpenDir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.buttonOpenDir.Name = "buttonOpenDir";
			this.buttonOpenDir.Size = new System.Drawing.Size(41, 33);
			this.buttonOpenDir.TabIndex = 8;
			this.buttonOpenDir.UseVisualStyleBackColor = true;
			this.buttonOpenDir.Click += new System.EventHandler(this.ButtonOpenDirClick);
			// 
			// chBoxScanSubDir
			// 
			this.chBoxScanSubDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chBoxScanSubDir.Checked = true;
			this.chBoxScanSubDir.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chBoxScanSubDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxScanSubDir.ForeColor = System.Drawing.Color.Navy;
			this.chBoxScanSubDir.Location = new System.Drawing.Point(1120, 4);
			this.chBoxScanSubDir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.chBoxScanSubDir.Name = "chBoxScanSubDir";
			this.chBoxScanSubDir.Size = new System.Drawing.Size(260, 30);
			this.chBoxScanSubDir.TabIndex = 5;
			this.chBoxScanSubDir.Text = "Обрабатывать подкаталоги";
			this.chBoxScanSubDir.UseVisualStyleBackColor = true;
			this.chBoxScanSubDir.Click += new System.EventHandler(this.ChBoxScanSubDirClick);
			// 
			// tboxSourceDir
			// 
			this.tboxSourceDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxSourceDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tboxSourceDir.Location = new System.Drawing.Point(140, 6);
			this.tboxSourceDir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tboxSourceDir.Name = "tboxSourceDir";
			this.tboxSourceDir.Size = new System.Drawing.Size(971, 23);
			this.tboxSourceDir.TabIndex = 4;
			this.tboxSourceDir.TextChanged += new System.EventHandler(this.TboxSourceDirTextChanged);
			this.tboxSourceDir.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TboxSourceDirKeyPress);
			// 
			// lblDir
			// 
			this.lblDir.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.lblDir.Location = new System.Drawing.Point(63, 10);
			this.lblDir.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDir.Name = "lblDir";
			this.lblDir.Size = new System.Drawing.Size(69, 23);
			this.lblDir.TabIndex = 3;
			this.lblDir.Text = "Адрес:";
			// 
			// tcResult
			// 
			this.tcResult.Controls.Add(this.tpNotValid);
			this.tcResult.Controls.Add(this.tpValid);
			this.tcResult.Controls.Add(this.tpNotFB2Files);
			this.tcResult.Controls.Add(this.tpOptions);
			this.tcResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tcResult.ImageList = this.imgl16;
			this.tcResult.Location = new System.Drawing.Point(0, 0);
			this.tcResult.Margin = new System.Windows.Forms.Padding(0);
			this.tcResult.Name = "tcResult";
			this.tcResult.SelectedIndex = 0;
			this.tcResult.Size = new System.Drawing.Size(1090, 562);
			this.tcResult.TabIndex = 16;
			this.tcResult.SelectedIndexChanged += new System.EventHandler(this.TcResultSelectedIndexChanged);
			// 
			// tpNotValid
			// 
			this.tpNotValid.Controls.Add(this.gbFB2NotValidFiles);
			this.tpNotValid.Controls.Add(this.pErrors);
			this.tpNotValid.Controls.Add(this.listViewNotValid);
			this.tpNotValid.ImageIndex = 0;
			this.tpNotValid.Location = new System.Drawing.Point(4, 26);
			this.tpNotValid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tpNotValid.Name = "tpNotValid";
			this.tpNotValid.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tpNotValid.Size = new System.Drawing.Size(1082, 532);
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
			this.gbFB2NotValidFiles.Location = new System.Drawing.Point(4, 4);
			this.gbFB2NotValidFiles.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.gbFB2NotValidFiles.Name = "gbFB2NotValidFiles";
			this.gbFB2NotValidFiles.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.gbFB2NotValidFiles.Size = new System.Drawing.Size(1074, 94);
			this.gbFB2NotValidFiles.TabIndex = 6;
			this.gbFB2NotValidFiles.TabStop = false;
			this.gbFB2NotValidFiles.Text = " Обработка не валидных fb2-файлов: ";
			// 
			// lblFB2NotValidFilesMoveDir
			// 
			this.lblFB2NotValidFilesMoveDir.AutoSize = true;
			this.lblFB2NotValidFilesMoveDir.Location = new System.Drawing.Point(12, 60);
			this.lblFB2NotValidFilesMoveDir.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblFB2NotValidFilesMoveDir.Name = "lblFB2NotValidFilesMoveDir";
			this.lblFB2NotValidFilesMoveDir.Size = new System.Drawing.Size(124, 17);
			this.lblFB2NotValidFilesMoveDir.TabIndex = 9;
			this.lblFB2NotValidFilesMoveDir.Text = "Переместить в:";
			this.lblFB2NotValidFilesMoveDir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnFB2NotValidMoveTo
			// 
			this.btnFB2NotValidMoveTo.Image = ((System.Drawing.Image)(resources.GetObject("btnFB2NotValidMoveTo.Image")));
			this.btnFB2NotValidMoveTo.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnFB2NotValidMoveTo.Location = new System.Drawing.Point(152, 54);
			this.btnFB2NotValidMoveTo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnFB2NotValidMoveTo.Name = "btnFB2NotValidMoveTo";
			this.btnFB2NotValidMoveTo.Size = new System.Drawing.Size(49, 30);
			this.btnFB2NotValidMoveTo.TabIndex = 8;
			this.btnFB2NotValidMoveTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnFB2NotValidMoveTo.UseVisualStyleBackColor = true;
			this.btnFB2NotValidMoveTo.Click += new System.EventHandler(this.BtnFB2NotValidMoveToClick);
			// 
			// tboxFB2NotValidDirMoveTo
			// 
			this.tboxFB2NotValidDirMoveTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxFB2NotValidDirMoveTo.Location = new System.Drawing.Point(207, 57);
			this.tboxFB2NotValidDirMoveTo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tboxFB2NotValidDirMoveTo.Name = "tboxFB2NotValidDirMoveTo";
			this.tboxFB2NotValidDirMoveTo.Size = new System.Drawing.Size(842, 23);
			this.tboxFB2NotValidDirMoveTo.TabIndex = 7;
			this.tboxFB2NotValidDirMoveTo.TextChanged += new System.EventHandler(this.TboxFB2NotValidDirMoveToTextChanged);
			// 
			// lblFB2NotValidFilesCopyDir
			// 
			this.lblFB2NotValidFilesCopyDir.AutoSize = true;
			this.lblFB2NotValidFilesCopyDir.Location = new System.Drawing.Point(12, 28);
			this.lblFB2NotValidFilesCopyDir.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblFB2NotValidFilesCopyDir.Name = "lblFB2NotValidFilesCopyDir";
			this.lblFB2NotValidFilesCopyDir.Size = new System.Drawing.Size(114, 17);
			this.lblFB2NotValidFilesCopyDir.TabIndex = 6;
			this.lblFB2NotValidFilesCopyDir.Text = "Копировать в:";
			this.lblFB2NotValidFilesCopyDir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnFB2NotValidCopyTo
			// 
			this.btnFB2NotValidCopyTo.Image = ((System.Drawing.Image)(resources.GetObject("btnFB2NotValidCopyTo.Image")));
			this.btnFB2NotValidCopyTo.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnFB2NotValidCopyTo.Location = new System.Drawing.Point(152, 22);
			this.btnFB2NotValidCopyTo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnFB2NotValidCopyTo.Name = "btnFB2NotValidCopyTo";
			this.btnFB2NotValidCopyTo.Size = new System.Drawing.Size(49, 30);
			this.btnFB2NotValidCopyTo.TabIndex = 5;
			this.btnFB2NotValidCopyTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnFB2NotValidCopyTo.UseVisualStyleBackColor = true;
			this.btnFB2NotValidCopyTo.Click += new System.EventHandler(this.BtnFB2NotValidCopyToClick);
			// 
			// tboxFB2NotValidDirCopyTo
			// 
			this.tboxFB2NotValidDirCopyTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxFB2NotValidDirCopyTo.Location = new System.Drawing.Point(207, 25);
			this.tboxFB2NotValidDirCopyTo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tboxFB2NotValidDirCopyTo.Name = "tboxFB2NotValidDirCopyTo";
			this.tboxFB2NotValidDirCopyTo.Size = new System.Drawing.Size(842, 23);
			this.tboxFB2NotValidDirCopyTo.TabIndex = 0;
			this.tboxFB2NotValidDirCopyTo.TextChanged += new System.EventHandler(this.TboxFB2NotValidDirCopyToTextChanged);
			// 
			// pErrors
			// 
			this.pErrors.Controls.Add(this.pViewError);
			this.pErrors.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pErrors.Location = new System.Drawing.Point(4, 418);
			this.pErrors.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.pErrors.Name = "pErrors";
			this.pErrors.Size = new System.Drawing.Size(1074, 110);
			this.pErrors.TabIndex = 1;
			// 
			// pViewError
			// 
			this.pViewError.Controls.Add(this.btnLoadList);
			this.pViewError.Controls.Add(this.btnSaveList);
			this.pViewError.Controls.Add(this.rеboxNotValid);
			this.pViewError.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pViewError.Location = new System.Drawing.Point(0, 0);
			this.pViewError.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.pViewError.Name = "pViewError";
			this.pViewError.Size = new System.Drawing.Size(1074, 110);
			this.pViewError.TabIndex = 3;
			// 
			// btnLoadList
			// 
			this.btnLoadList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLoadList.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadList.Image")));
			this.btnLoadList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnLoadList.Location = new System.Drawing.Point(867, 75);
			this.btnLoadList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnLoadList.Name = "btnLoadList";
			this.btnLoadList.Size = new System.Drawing.Size(191, 31);
			this.btnLoadList.TabIndex = 4;
			this.btnLoadList.Text = "Загрузить список";
			this.btnLoadList.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnLoadList.UseVisualStyleBackColor = true;
			this.btnLoadList.Click += new System.EventHandler(this.BtnLoadListClick);
			// 
			// btnSaveList
			// 
			this.btnSaveList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSaveList.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveList.Image")));
			this.btnSaveList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnSaveList.Location = new System.Drawing.Point(867, 10);
			this.btnSaveList.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnSaveList.Name = "btnSaveList";
			this.btnSaveList.Size = new System.Drawing.Size(191, 31);
			this.btnSaveList.TabIndex = 3;
			this.btnSaveList.Text = "Сохранить список";
			this.btnSaveList.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSaveList.UseVisualStyleBackColor = true;
			this.btnSaveList.Click += new System.EventHandler(this.BtnSaveListClick);
			// 
			// rеboxNotValid
			// 
			this.rеboxNotValid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.rеboxNotValid.BackColor = System.Drawing.SystemColors.Window;
			this.rеboxNotValid.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rеboxNotValid.Location = new System.Drawing.Point(0, 0);
			this.rеboxNotValid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.rеboxNotValid.Name = "rеboxNotValid";
			this.rеboxNotValid.ReadOnly = true;
			this.rеboxNotValid.Size = new System.Drawing.Size(858, 109);
			this.rеboxNotValid.TabIndex = 2;
			this.rеboxNotValid.Text = "";
			// 
			// listViewNotValid
			// 
			this.listViewNotValid.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.listViewNotValid.CheckBoxes = true;
			this.listViewNotValid.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.chNonValidFile,
			this.chNonValidError,
			this.chNonValidLenght});
			this.listViewNotValid.ContextMenuStrip = this.cmsFB2;
			this.listViewNotValid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.listViewNotValid.FullRowSelect = true;
			this.listViewNotValid.GridLines = true;
			this.listViewNotValid.Location = new System.Drawing.Point(4, 97);
			this.listViewNotValid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.listViewNotValid.MultiSelect = false;
			this.listViewNotValid.Name = "listViewNotValid";
			this.listViewNotValid.ShowItemToolTips = true;
			this.listViewNotValid.Size = new System.Drawing.Size(1070, 309);
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
			this.chNonValidFile.Width = 600;
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
			this.cmsFB2.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.cmsFB2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.tsmiFileReValidate,
			this.tsmi3,
			this.tsmiEditInTextEditor,
			this.tsmiEditInFB2Editor,
			this.tsmi1,
			this.tsmiViewInReader,
			this.tsmi2,
			this.tsmiOpenFileDir,
			this.tsmiDeleteFileFromDisk,
			this.toolStripSeparator1,
			this.tsmiFB2CheckedAll,
			this.tsmiFB2UnCheckedAll});
			this.cmsFB2.Name = "cmsValidator";
			this.cmsFB2.Size = new System.Drawing.Size(357, 236);
			// 
			// tsmiFileReValidate
			// 
			this.tsmiFileReValidate.Image = ((System.Drawing.Image)(resources.GetObject("tsmiFileReValidate.Image")));
			this.tsmiFileReValidate.Name = "tsmiFileReValidate";
			this.tsmiFileReValidate.Size = new System.Drawing.Size(356, 26);
			this.tsmiFileReValidate.Text = "Проверить файл заново (валидация)";
			this.tsmiFileReValidate.Click += new System.EventHandler(this.TsmiFileReValidateClick);
			// 
			// tsmi3
			// 
			this.tsmi3.Name = "tsmi3";
			this.tsmi3.Size = new System.Drawing.Size(353, 6);
			// 
			// tsmiEditInTextEditor
			// 
			this.tsmiEditInTextEditor.Image = ((System.Drawing.Image)(resources.GetObject("tsmiEditInTextEditor.Image")));
			this.tsmiEditInTextEditor.Name = "tsmiEditInTextEditor";
			this.tsmiEditInTextEditor.Size = new System.Drawing.Size(356, 26);
			this.tsmiEditInTextEditor.Text = "Редактировать в текстовом редакторе";
			this.tsmiEditInTextEditor.Click += new System.EventHandler(this.TsmiEditInTextEditorClick);
			// 
			// tsmiEditInFB2Editor
			// 
			this.tsmiEditInFB2Editor.Image = ((System.Drawing.Image)(resources.GetObject("tsmiEditInFB2Editor.Image")));
			this.tsmiEditInFB2Editor.Name = "tsmiEditInFB2Editor";
			this.tsmiEditInFB2Editor.Size = new System.Drawing.Size(356, 26);
			this.tsmiEditInFB2Editor.Text = "Редактировать в fb2-редакторе";
			this.tsmiEditInFB2Editor.Click += new System.EventHandler(this.TsmiEditInFB2EditorClick);
			// 
			// tsmi1
			// 
			this.tsmi1.Name = "tsmi1";
			this.tsmi1.Size = new System.Drawing.Size(353, 6);
			// 
			// tsmiViewInReader
			// 
			this.tsmiViewInReader.Image = ((System.Drawing.Image)(resources.GetObject("tsmiViewInReader.Image")));
			this.tsmiViewInReader.Name = "tsmiViewInReader";
			this.tsmiViewInReader.Size = new System.Drawing.Size(356, 26);
			this.tsmiViewInReader.Text = "Запустить в fb2-читалке (Просмотр)";
			this.tsmiViewInReader.Click += new System.EventHandler(this.TsmiVienInReaderClick);
			// 
			// tsmi2
			// 
			this.tsmi2.Name = "tsmi2";
			this.tsmi2.Size = new System.Drawing.Size(353, 6);
			// 
			// tsmiOpenFileDir
			// 
			this.tsmiOpenFileDir.Image = ((System.Drawing.Image)(resources.GetObject("tsmiOpenFileDir.Image")));
			this.tsmiOpenFileDir.Name = "tsmiOpenFileDir";
			this.tsmiOpenFileDir.Size = new System.Drawing.Size(356, 26);
			this.tsmiOpenFileDir.Text = "Открыть папку для выделенного файла";
			this.tsmiOpenFileDir.Click += new System.EventHandler(this.TsmiOpenFileDirClick);
			// 
			// tsmiDeleteFileFromDisk
			// 
			this.tsmiDeleteFileFromDisk.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDeleteFileFromDisk.Image")));
			this.tsmiDeleteFileFromDisk.Name = "tsmiDeleteFileFromDisk";
			this.tsmiDeleteFileFromDisk.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.tsmiDeleteFileFromDisk.Size = new System.Drawing.Size(356, 26);
			this.tsmiDeleteFileFromDisk.Text = "Удалить файл с диска";
			this.tsmiDeleteFileFromDisk.Click += new System.EventHandler(this.TsmiDeleteFileFromDiskClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(353, 6);
			// 
			// tsmiFB2CheckedAll
			// 
			this.tsmiFB2CheckedAll.Image = ((System.Drawing.Image)(resources.GetObject("tsmiFB2CheckedAll.Image")));
			this.tsmiFB2CheckedAll.Name = "tsmiFB2CheckedAll";
			this.tsmiFB2CheckedAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
			| System.Windows.Forms.Keys.C)));
			this.tsmiFB2CheckedAll.Size = new System.Drawing.Size(356, 26);
			this.tsmiFB2CheckedAll.Text = "Пометить все файлы";
			this.tsmiFB2CheckedAll.Click += new System.EventHandler(this.TsmiFB2CheckedAllClick);
			// 
			// tsmiFB2UnCheckedAll
			// 
			this.tsmiFB2UnCheckedAll.Image = ((System.Drawing.Image)(resources.GetObject("tsmiFB2UnCheckedAll.Image")));
			this.tsmiFB2UnCheckedAll.Name = "tsmiFB2UnCheckedAll";
			this.tsmiFB2UnCheckedAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
			| System.Windows.Forms.Keys.U)));
			this.tsmiFB2UnCheckedAll.Size = new System.Drawing.Size(356, 26);
			this.tsmiFB2UnCheckedAll.Text = "Снять все отметки";
			this.tsmiFB2UnCheckedAll.Click += new System.EventHandler(this.TsmiFB2UnCheckedAllClick);
			// 
			// tpValid
			// 
			this.tpValid.Controls.Add(this.pValidLV);
			this.tpValid.Controls.Add(this.gbFB2Valid);
			this.tpValid.ImageIndex = 1;
			this.tpValid.Location = new System.Drawing.Point(4, 26);
			this.tpValid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tpValid.Name = "tpValid";
			this.tpValid.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tpValid.Size = new System.Drawing.Size(1083, 524);
			this.tpValid.TabIndex = 1;
			this.tpValid.Text = " Валидные fb2-файлы ";
			this.tpValid.UseVisualStyleBackColor = true;
			// 
			// pValidLV
			// 
			this.pValidLV.Controls.Add(this.listViewValid);
			this.pValidLV.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pValidLV.Location = new System.Drawing.Point(4, 98);
			this.pValidLV.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.pValidLV.Name = "pValidLV";
			this.pValidLV.Size = new System.Drawing.Size(1075, 422);
			this.pValidLV.TabIndex = 9;
			// 
			// listViewValid
			// 
			this.listViewValid.CheckBoxes = true;
			this.listViewValid.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.chValidFile,
			this.chValidLenght});
			this.listViewValid.ContextMenuStrip = this.cmsFB2;
			this.listViewValid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewValid.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.listViewValid.FullRowSelect = true;
			this.listViewValid.GridLines = true;
			this.listViewValid.Location = new System.Drawing.Point(0, 0);
			this.listViewValid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.listViewValid.MultiSelect = false;
			this.listViewValid.Name = "listViewValid";
			this.listViewValid.ShowItemToolTips = true;
			this.listViewValid.Size = new System.Drawing.Size(1075, 422);
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
			this.gbFB2Valid.Location = new System.Drawing.Point(4, 4);
			this.gbFB2Valid.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.gbFB2Valid.Name = "gbFB2Valid";
			this.gbFB2Valid.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.gbFB2Valid.Size = new System.Drawing.Size(1075, 94);
			this.gbFB2Valid.TabIndex = 8;
			this.gbFB2Valid.TabStop = false;
			this.gbFB2Valid.Text = " Обработка валидных fb2-файлов: ";
			// 
			// lblFB2ValidFilesMoveDir
			// 
			this.lblFB2ValidFilesMoveDir.AutoSize = true;
			this.lblFB2ValidFilesMoveDir.Location = new System.Drawing.Point(12, 60);
			this.lblFB2ValidFilesMoveDir.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblFB2ValidFilesMoveDir.Name = "lblFB2ValidFilesMoveDir";
			this.lblFB2ValidFilesMoveDir.Size = new System.Drawing.Size(124, 17);
			this.lblFB2ValidFilesMoveDir.TabIndex = 9;
			this.lblFB2ValidFilesMoveDir.Text = "Переместить в:";
			this.lblFB2ValidFilesMoveDir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnFB2ValidMoveTo
			// 
			this.btnFB2ValidMoveTo.Image = ((System.Drawing.Image)(resources.GetObject("btnFB2ValidMoveTo.Image")));
			this.btnFB2ValidMoveTo.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnFB2ValidMoveTo.Location = new System.Drawing.Point(152, 54);
			this.btnFB2ValidMoveTo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnFB2ValidMoveTo.Name = "btnFB2ValidMoveTo";
			this.btnFB2ValidMoveTo.Size = new System.Drawing.Size(49, 30);
			this.btnFB2ValidMoveTo.TabIndex = 8;
			this.btnFB2ValidMoveTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnFB2ValidMoveTo.UseVisualStyleBackColor = true;
			this.btnFB2ValidMoveTo.Click += new System.EventHandler(this.BtnFB2ValidMoveToClick);
			// 
			// tboxFB2ValidDirMoveTo
			// 
			this.tboxFB2ValidDirMoveTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxFB2ValidDirMoveTo.Location = new System.Drawing.Point(207, 57);
			this.tboxFB2ValidDirMoveTo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tboxFB2ValidDirMoveTo.Name = "tboxFB2ValidDirMoveTo";
			this.tboxFB2ValidDirMoveTo.Size = new System.Drawing.Size(843, 23);
			this.tboxFB2ValidDirMoveTo.TabIndex = 7;
			this.tboxFB2ValidDirMoveTo.TextChanged += new System.EventHandler(this.TboxFB2ValidDirMoveToTextChanged);
			// 
			// lblFB2ValidFilesCopyDir
			// 
			this.lblFB2ValidFilesCopyDir.AutoSize = true;
			this.lblFB2ValidFilesCopyDir.Location = new System.Drawing.Point(12, 28);
			this.lblFB2ValidFilesCopyDir.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblFB2ValidFilesCopyDir.Name = "lblFB2ValidFilesCopyDir";
			this.lblFB2ValidFilesCopyDir.Size = new System.Drawing.Size(114, 17);
			this.lblFB2ValidFilesCopyDir.TabIndex = 6;
			this.lblFB2ValidFilesCopyDir.Text = "Копировать в:";
			this.lblFB2ValidFilesCopyDir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnFB2ValidCopyTo
			// 
			this.btnFB2ValidCopyTo.Image = ((System.Drawing.Image)(resources.GetObject("btnFB2ValidCopyTo.Image")));
			this.btnFB2ValidCopyTo.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnFB2ValidCopyTo.Location = new System.Drawing.Point(152, 22);
			this.btnFB2ValidCopyTo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnFB2ValidCopyTo.Name = "btnFB2ValidCopyTo";
			this.btnFB2ValidCopyTo.Size = new System.Drawing.Size(49, 30);
			this.btnFB2ValidCopyTo.TabIndex = 5;
			this.btnFB2ValidCopyTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnFB2ValidCopyTo.UseVisualStyleBackColor = true;
			this.btnFB2ValidCopyTo.Click += new System.EventHandler(this.BtnFB2ValidCopyToClick);
			// 
			// tboxFB2ValidDirCopyTo
			// 
			this.tboxFB2ValidDirCopyTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxFB2ValidDirCopyTo.Location = new System.Drawing.Point(207, 25);
			this.tboxFB2ValidDirCopyTo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tboxFB2ValidDirCopyTo.Name = "tboxFB2ValidDirCopyTo";
			this.tboxFB2ValidDirCopyTo.Size = new System.Drawing.Size(843, 23);
			this.tboxFB2ValidDirCopyTo.TabIndex = 0;
			this.tboxFB2ValidDirCopyTo.TextChanged += new System.EventHandler(this.TboxFB2ValidDirCopyToTextChanged);
			// 
			// tpNotFB2Files
			// 
			this.tpNotFB2Files.Controls.Add(this.pNotValidLV);
			this.tpNotFB2Files.Controls.Add(this.gbNotFB2);
			this.tpNotFB2Files.Location = new System.Drawing.Point(4, 26);
			this.tpNotFB2Files.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tpNotFB2Files.Name = "tpNotFB2Files";
			this.tpNotFB2Files.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tpNotFB2Files.Size = new System.Drawing.Size(1083, 524);
			this.tpNotFB2Files.TabIndex = 2;
			this.tpNotFB2Files.Text = " Не fb2-файлы ";
			this.tpNotFB2Files.UseVisualStyleBackColor = true;
			// 
			// pNotValidLV
			// 
			this.pNotValidLV.Controls.Add(this.listViewNotFB2);
			this.pNotValidLV.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pNotValidLV.Location = new System.Drawing.Point(4, 98);
			this.pNotValidLV.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.pNotValidLV.Name = "pNotValidLV";
			this.pNotValidLV.Size = new System.Drawing.Size(1075, 422);
			this.pNotValidLV.TabIndex = 10;
			// 
			// listViewNotFB2
			// 
			this.listViewNotFB2.CheckBoxes = true;
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
			this.listViewNotFB2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.listViewNotFB2.MultiSelect = false;
			this.listViewNotFB2.Name = "listViewNotFB2";
			this.listViewNotFB2.ShowItemToolTips = true;
			this.listViewNotFB2.Size = new System.Drawing.Size(1075, 422);
			this.listViewNotFB2.TabIndex = 2;
			this.listViewNotFB2.UseCompatibleStateImageBehavior = false;
			this.listViewNotFB2.View = System.Windows.Forms.View.Details;
			this.listViewNotFB2.DoubleClick += new System.EventHandler(this.TsmiOpenFileDirClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Самые разные файлы (не fb2)";
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
			this.cmsNotFB2.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.cmsNotFB2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.tsmiOpenNotFB2FileDir,
			this.tsmiDeleteNotFB2FromDisk,
			this.toolStripSeparator3,
			this.tsmiNotFB2CheckedAll,
			this.tsmiNotFB2UnCheckedAll});
			this.cmsNotFB2.Name = "cmsValidator";
			this.cmsNotFB2.Size = new System.Drawing.Size(357, 114);
			// 
			// tsmiOpenNotFB2FileDir
			// 
			this.tsmiOpenNotFB2FileDir.Image = ((System.Drawing.Image)(resources.GetObject("tsmiOpenNotFB2FileDir.Image")));
			this.tsmiOpenNotFB2FileDir.Name = "tsmiOpenNotFB2FileDir";
			this.tsmiOpenNotFB2FileDir.Size = new System.Drawing.Size(356, 26);
			this.tsmiOpenNotFB2FileDir.Text = "Открыть папку для выделенного файла";
			this.tsmiOpenNotFB2FileDir.Click += new System.EventHandler(this.TsmiOpenFileDirClick);
			// 
			// tsmiDeleteNotFB2FromDisk
			// 
			this.tsmiDeleteNotFB2FromDisk.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDeleteNotFB2FromDisk.Image")));
			this.tsmiDeleteNotFB2FromDisk.Name = "tsmiDeleteNotFB2FromDisk";
			this.tsmiDeleteNotFB2FromDisk.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.tsmiDeleteNotFB2FromDisk.Size = new System.Drawing.Size(356, 26);
			this.tsmiDeleteNotFB2FromDisk.Text = "Удалить файл с диска";
			this.tsmiDeleteNotFB2FromDisk.Click += new System.EventHandler(this.TsmiDeleteFileFromDiskClick);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(353, 6);
			// 
			// tsmiNotFB2CheckedAll
			// 
			this.tsmiNotFB2CheckedAll.Image = ((System.Drawing.Image)(resources.GetObject("tsmiNotFB2CheckedAll.Image")));
			this.tsmiNotFB2CheckedAll.Name = "tsmiNotFB2CheckedAll";
			this.tsmiNotFB2CheckedAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
			| System.Windows.Forms.Keys.C)));
			this.tsmiNotFB2CheckedAll.Size = new System.Drawing.Size(356, 26);
			this.tsmiNotFB2CheckedAll.Text = "Пометить все файлы";
			this.tsmiNotFB2CheckedAll.Click += new System.EventHandler(this.TsmiNotFB2CheckedAllClick);
			// 
			// tsmiNotFB2UnCheckedAll
			// 
			this.tsmiNotFB2UnCheckedAll.Image = ((System.Drawing.Image)(resources.GetObject("tsmiNotFB2UnCheckedAll.Image")));
			this.tsmiNotFB2UnCheckedAll.Name = "tsmiNotFB2UnCheckedAll";
			this.tsmiNotFB2UnCheckedAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
			| System.Windows.Forms.Keys.U)));
			this.tsmiNotFB2UnCheckedAll.Size = new System.Drawing.Size(356, 26);
			this.tsmiNotFB2UnCheckedAll.Text = "Снять все отметки";
			this.tsmiNotFB2UnCheckedAll.Click += new System.EventHandler(this.TsmiNotFB2UnCheckedAllClick);
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
			this.gbNotFB2.Location = new System.Drawing.Point(4, 4);
			this.gbNotFB2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.gbNotFB2.Name = "gbNotFB2";
			this.gbNotFB2.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.gbNotFB2.Size = new System.Drawing.Size(1075, 94);
			this.gbNotFB2.TabIndex = 9;
			this.gbNotFB2.TabStop = false;
			this.gbNotFB2.Text = " Обработка не fb2-файлов: ";
			// 
			// lblNotFB2FilesMoveDir
			// 
			this.lblNotFB2FilesMoveDir.AutoSize = true;
			this.lblNotFB2FilesMoveDir.Location = new System.Drawing.Point(12, 60);
			this.lblNotFB2FilesMoveDir.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblNotFB2FilesMoveDir.Name = "lblNotFB2FilesMoveDir";
			this.lblNotFB2FilesMoveDir.Size = new System.Drawing.Size(124, 17);
			this.lblNotFB2FilesMoveDir.TabIndex = 9;
			this.lblNotFB2FilesMoveDir.Text = "Переместить в:";
			this.lblNotFB2FilesMoveDir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnNotFB2MoveTo
			// 
			this.btnNotFB2MoveTo.Image = ((System.Drawing.Image)(resources.GetObject("btnNotFB2MoveTo.Image")));
			this.btnNotFB2MoveTo.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnNotFB2MoveTo.Location = new System.Drawing.Point(152, 54);
			this.btnNotFB2MoveTo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnNotFB2MoveTo.Name = "btnNotFB2MoveTo";
			this.btnNotFB2MoveTo.Size = new System.Drawing.Size(49, 30);
			this.btnNotFB2MoveTo.TabIndex = 8;
			this.btnNotFB2MoveTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnNotFB2MoveTo.UseVisualStyleBackColor = true;
			this.btnNotFB2MoveTo.Click += new System.EventHandler(this.BtnNotFB2MoveToClick);
			// 
			// tboxNotFB2DirMoveTo
			// 
			this.tboxNotFB2DirMoveTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxNotFB2DirMoveTo.Location = new System.Drawing.Point(207, 57);
			this.tboxNotFB2DirMoveTo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tboxNotFB2DirMoveTo.Name = "tboxNotFB2DirMoveTo";
			this.tboxNotFB2DirMoveTo.Size = new System.Drawing.Size(843, 23);
			this.tboxNotFB2DirMoveTo.TabIndex = 7;
			this.tboxNotFB2DirMoveTo.TextChanged += new System.EventHandler(this.TboxNotFB2DirMoveToTextChanged);
			// 
			// lblNotFB2FilesCopyDir
			// 
			this.lblNotFB2FilesCopyDir.AutoSize = true;
			this.lblNotFB2FilesCopyDir.Location = new System.Drawing.Point(12, 28);
			this.lblNotFB2FilesCopyDir.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblNotFB2FilesCopyDir.Name = "lblNotFB2FilesCopyDir";
			this.lblNotFB2FilesCopyDir.Size = new System.Drawing.Size(114, 17);
			this.lblNotFB2FilesCopyDir.TabIndex = 6;
			this.lblNotFB2FilesCopyDir.Text = "Копировать в:";
			this.lblNotFB2FilesCopyDir.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btnNotFB2CopyTo
			// 
			this.btnNotFB2CopyTo.Image = ((System.Drawing.Image)(resources.GetObject("btnNotFB2CopyTo.Image")));
			this.btnNotFB2CopyTo.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnNotFB2CopyTo.Location = new System.Drawing.Point(152, 22);
			this.btnNotFB2CopyTo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnNotFB2CopyTo.Name = "btnNotFB2CopyTo";
			this.btnNotFB2CopyTo.Size = new System.Drawing.Size(49, 30);
			this.btnNotFB2CopyTo.TabIndex = 5;
			this.btnNotFB2CopyTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnNotFB2CopyTo.UseVisualStyleBackColor = true;
			this.btnNotFB2CopyTo.Click += new System.EventHandler(this.BtnNotFB2CopyToClick);
			// 
			// tboxNotFB2DirCopyTo
			// 
			this.tboxNotFB2DirCopyTo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxNotFB2DirCopyTo.Location = new System.Drawing.Point(207, 25);
			this.tboxNotFB2DirCopyTo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tboxNotFB2DirCopyTo.Name = "tboxNotFB2DirCopyTo";
			this.tboxNotFB2DirCopyTo.Size = new System.Drawing.Size(843, 23);
			this.tboxNotFB2DirCopyTo.TabIndex = 0;
			this.tboxNotFB2DirCopyTo.TextChanged += new System.EventHandler(this.TboxNotFB2DirCopyToTextChanged);
			// 
			// tpOptions
			// 
			this.tpOptions.Controls.Add(this.pArchivator);
			this.tpOptions.Controls.Add(this.gboxValidatorPE);
			this.tpOptions.Controls.Add(this.gboxValidatorDoubleClick);
			this.tpOptions.Controls.Add(this.gboxCopyMoveOptions);
			this.tpOptions.Location = new System.Drawing.Point(4, 26);
			this.tpOptions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tpOptions.Name = "tpOptions";
			this.tpOptions.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tpOptions.Size = new System.Drawing.Size(1083, 524);
			this.tpOptions.TabIndex = 3;
			this.tpOptions.Text = "Настройки";
			this.tpOptions.UseVisualStyleBackColor = true;
			// 
			// pArchivator
			// 
			this.pArchivator.Controls.Add(this.lblArchivatorPath);
			this.pArchivator.Controls.Add(this.tboxArchivatorPath);
			this.pArchivator.Controls.Add(this.btnArchivatorPath);
			this.pArchivator.Dock = System.Windows.Forms.DockStyle.Top;
			this.pArchivator.Location = new System.Drawing.Point(4, 253);
			this.pArchivator.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.pArchivator.Name = "pArchivator";
			this.pArchivator.Size = new System.Drawing.Size(1075, 37);
			this.pArchivator.TabIndex = 24;
			// 
			// lblArchivatorPath
			// 
			this.lblArchivatorPath.AutoSize = true;
			this.lblArchivatorPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.lblArchivatorPath.ForeColor = System.Drawing.Color.Maroon;
			this.lblArchivatorPath.Location = new System.Drawing.Point(8, 7);
			this.lblArchivatorPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblArchivatorPath.Name = "lblArchivatorPath";
			this.lblArchivatorPath.Size = new System.Drawing.Size(172, 17);
			this.lblArchivatorPath.TabIndex = 13;
			this.lblArchivatorPath.Text = "Путь к Архиватору (GUI):";
			// 
			// tboxArchivatorPath
			// 
			this.tboxArchivatorPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxArchivatorPath.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tboxArchivatorPath.Location = new System.Drawing.Point(231, 4);
			this.tboxArchivatorPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tboxArchivatorPath.Name = "tboxArchivatorPath";
			this.tboxArchivatorPath.ReadOnly = true;
			this.tboxArchivatorPath.Size = new System.Drawing.Size(823, 23);
			this.tboxArchivatorPath.TabIndex = 11;
			this.tboxArchivatorPath.TextChanged += new System.EventHandler(this.TboxArchivatorPathTextChanged);
			// 
			// btnArchivatorPath
			// 
			this.btnArchivatorPath.Image = ((System.Drawing.Image)(resources.GetObject("btnArchivatorPath.Image")));
			this.btnArchivatorPath.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnArchivatorPath.Location = new System.Drawing.Point(179, 1);
			this.btnArchivatorPath.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnArchivatorPath.Name = "btnArchivatorPath";
			this.btnArchivatorPath.Size = new System.Drawing.Size(49, 30);
			this.btnArchivatorPath.TabIndex = 12;
			this.btnArchivatorPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnArchivatorPath.UseVisualStyleBackColor = true;
			this.btnArchivatorPath.Click += new System.EventHandler(this.BtnArchivatorPathClick);
			// 
			// gboxValidatorPE
			// 
			this.gboxValidatorPE.Controls.Add(this.cboxValidatorForZipPE);
			this.gboxValidatorPE.Controls.Add(this.cboxValidatorForFB2PE);
			this.gboxValidatorPE.Controls.Add(this.lblValidatorForFB2ArchivePE);
			this.gboxValidatorPE.Controls.Add(this.lblValidatorForFB2PE);
			this.gboxValidatorPE.Dock = System.Windows.Forms.DockStyle.Top;
			this.gboxValidatorPE.Font = new System.Drawing.Font("Tahoma", 8F);
			this.gboxValidatorPE.ForeColor = System.Drawing.Color.Maroon;
			this.gboxValidatorPE.Location = new System.Drawing.Point(4, 161);
			this.gboxValidatorPE.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.gboxValidatorPE.Name = "gboxValidatorPE";
			this.gboxValidatorPE.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.gboxValidatorPE.Size = new System.Drawing.Size(1075, 92);
			this.gboxValidatorPE.TabIndex = 23;
			this.gboxValidatorPE.TabStop = false;
			this.gboxValidatorPE.Text = " Действие по нажатию клавиши Enter на Списках ";
			// 
			// cboxValidatorForZipPE
			// 
			this.cboxValidatorForZipPE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxValidatorForZipPE.FormattingEnabled = true;
			this.cboxValidatorForZipPE.Items.AddRange(new object[] {
			"Проверить файл заново (валидация)",
			"Открыть файл в архиваторе",
			"Открыть папку для выделенного файла"});
			this.cboxValidatorForZipPE.Location = new System.Drawing.Point(231, 53);
			this.cboxValidatorForZipPE.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.cboxValidatorForZipPE.Name = "cboxValidatorForZipPE";
			this.cboxValidatorForZipPE.Size = new System.Drawing.Size(448, 24);
			this.cboxValidatorForZipPE.TabIndex = 3;
			this.cboxValidatorForZipPE.SelectedIndexChanged += new System.EventHandler(this.CboxValidatorForZipPESelectedIndexChanged);
			// 
			// cboxValidatorForFB2PE
			// 
			this.cboxValidatorForFB2PE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxValidatorForFB2PE.FormattingEnabled = true;
			this.cboxValidatorForFB2PE.Items.AddRange(new object[] {
			"Проверить файл заново (валидация)",
			"Редактировать в текстовом редакторе",
			"Редактировать в fb2-редакторе",
			"Запустить в fb2-читалке (Просмотр)",
			"Открыть папку для выделенного файла"});
			this.cboxValidatorForFB2PE.Location = new System.Drawing.Point(231, 25);
			this.cboxValidatorForFB2PE.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.cboxValidatorForFB2PE.Name = "cboxValidatorForFB2PE";
			this.cboxValidatorForFB2PE.Size = new System.Drawing.Size(448, 24);
			this.cboxValidatorForFB2PE.TabIndex = 2;
			this.cboxValidatorForFB2PE.SelectedIndexChanged += new System.EventHandler(this.CboxValidatorForFB2PESelectedIndexChanged);
			// 
			// lblValidatorForFB2ArchivePE
			// 
			this.lblValidatorForFB2ArchivePE.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblValidatorForFB2ArchivePE.Location = new System.Drawing.Point(13, 57);
			this.lblValidatorForFB2ArchivePE.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblValidatorForFB2ArchivePE.Name = "lblValidatorForFB2ArchivePE";
			this.lblValidatorForFB2ArchivePE.Size = new System.Drawing.Size(209, 22);
			this.lblValidatorForFB2ArchivePE.TabIndex = 1;
			this.lblValidatorForFB2ArchivePE.Text = "Для запакованных fb2:";
			// 
			// lblValidatorForFB2PE
			// 
			this.lblValidatorForFB2PE.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblValidatorForFB2PE.Location = new System.Drawing.Point(13, 30);
			this.lblValidatorForFB2PE.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblValidatorForFB2PE.Name = "lblValidatorForFB2PE";
			this.lblValidatorForFB2PE.Size = new System.Drawing.Size(216, 22);
			this.lblValidatorForFB2PE.TabIndex = 0;
			this.lblValidatorForFB2PE.Text = "Для незапакованных fb2:";
			// 
			// gboxValidatorDoubleClick
			// 
			this.gboxValidatorDoubleClick.Controls.Add(this.cboxValidatorForZip);
			this.gboxValidatorDoubleClick.Controls.Add(this.cboxValidatorForFB2);
			this.gboxValidatorDoubleClick.Controls.Add(this.lblValidatorForFB2Archive);
			this.gboxValidatorDoubleClick.Controls.Add(this.lblValidatorForFB2);
			this.gboxValidatorDoubleClick.Dock = System.Windows.Forms.DockStyle.Top;
			this.gboxValidatorDoubleClick.Font = new System.Drawing.Font("Tahoma", 8F);
			this.gboxValidatorDoubleClick.ForeColor = System.Drawing.Color.Maroon;
			this.gboxValidatorDoubleClick.Location = new System.Drawing.Point(4, 69);
			this.gboxValidatorDoubleClick.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.gboxValidatorDoubleClick.Name = "gboxValidatorDoubleClick";
			this.gboxValidatorDoubleClick.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.gboxValidatorDoubleClick.Size = new System.Drawing.Size(1075, 92);
			this.gboxValidatorDoubleClick.TabIndex = 22;
			this.gboxValidatorDoubleClick.TabStop = false;
			this.gboxValidatorDoubleClick.Text = " Действие по двойному щелчку мышки на Списках ";
			// 
			// cboxValidatorForZip
			// 
			this.cboxValidatorForZip.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxValidatorForZip.FormattingEnabled = true;
			this.cboxValidatorForZip.Items.AddRange(new object[] {
			"Проверить файл заново (валидация)",
			"Открыть файл в архиваторе",
			"Открыть папку для выделенного файла"});
			this.cboxValidatorForZip.Location = new System.Drawing.Point(231, 53);
			this.cboxValidatorForZip.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.cboxValidatorForZip.Name = "cboxValidatorForZip";
			this.cboxValidatorForZip.Size = new System.Drawing.Size(448, 24);
			this.cboxValidatorForZip.TabIndex = 3;
			this.cboxValidatorForZip.SelectedIndexChanged += new System.EventHandler(this.CboxValidatorForZipSelectedIndexChanged);
			// 
			// cboxValidatorForFB2
			// 
			this.cboxValidatorForFB2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxValidatorForFB2.FormattingEnabled = true;
			this.cboxValidatorForFB2.Items.AddRange(new object[] {
			"Проверить файл заново (валидация)",
			"Редактировать в текстовом редакторе",
			"Редактировать в fb2-редакторе",
			"Запустить в fb2-читалке (Просмотр)",
			"Открыть папку для выделенного файла"});
			this.cboxValidatorForFB2.Location = new System.Drawing.Point(231, 25);
			this.cboxValidatorForFB2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.cboxValidatorForFB2.Name = "cboxValidatorForFB2";
			this.cboxValidatorForFB2.Size = new System.Drawing.Size(448, 24);
			this.cboxValidatorForFB2.TabIndex = 2;
			this.cboxValidatorForFB2.SelectedIndexChanged += new System.EventHandler(this.CboxValidatorForFB2SelectedIndexChanged);
			// 
			// lblValidatorForFB2Archive
			// 
			this.lblValidatorForFB2Archive.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblValidatorForFB2Archive.Location = new System.Drawing.Point(13, 57);
			this.lblValidatorForFB2Archive.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblValidatorForFB2Archive.Name = "lblValidatorForFB2Archive";
			this.lblValidatorForFB2Archive.Size = new System.Drawing.Size(209, 22);
			this.lblValidatorForFB2Archive.TabIndex = 1;
			this.lblValidatorForFB2Archive.Text = "Для запакованных fb2:";
			// 
			// lblValidatorForFB2
			// 
			this.lblValidatorForFB2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblValidatorForFB2.Location = new System.Drawing.Point(13, 30);
			this.lblValidatorForFB2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblValidatorForFB2.Name = "lblValidatorForFB2";
			this.lblValidatorForFB2.Size = new System.Drawing.Size(216, 22);
			this.lblValidatorForFB2.TabIndex = 0;
			this.lblValidatorForFB2.Text = "Для незапакованных fb2:";
			// 
			// gboxCopyMoveOptions
			// 
			this.gboxCopyMoveOptions.Controls.Add(this.cboxExistFile);
			this.gboxCopyMoveOptions.Controls.Add(this.lblExistFile);
			this.gboxCopyMoveOptions.Dock = System.Windows.Forms.DockStyle.Top;
			this.gboxCopyMoveOptions.Font = new System.Drawing.Font("Tahoma", 8F);
			this.gboxCopyMoveOptions.ForeColor = System.Drawing.Color.Maroon;
			this.gboxCopyMoveOptions.Location = new System.Drawing.Point(4, 4);
			this.gboxCopyMoveOptions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.gboxCopyMoveOptions.Name = "gboxCopyMoveOptions";
			this.gboxCopyMoveOptions.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.gboxCopyMoveOptions.Size = new System.Drawing.Size(1075, 65);
			this.gboxCopyMoveOptions.TabIndex = 21;
			this.gboxCopyMoveOptions.TabStop = false;
			this.gboxCopyMoveOptions.Text = " Настройки для Копирования / Перемещения файлов ";
			// 
			// cboxExistFile
			// 
			this.cboxExistFile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxExistFile.FormattingEnabled = true;
			this.cboxExistFile.Items.AddRange(new object[] {
			"Заменить существующий файл новым",
			"Добавить к новому файлу очередной номер",
			"Добавить к новому файлу дату и время"});
			this.cboxExistFile.Location = new System.Drawing.Point(312, 30);
			this.cboxExistFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.cboxExistFile.Name = "cboxExistFile";
			this.cboxExistFile.Size = new System.Drawing.Size(417, 24);
			this.cboxExistFile.TabIndex = 18;
			this.cboxExistFile.SelectedIndexChanged += new System.EventHandler(this.CboxExistFileSelectedIndexChanged);
			// 
			// lblExistFile
			// 
			this.lblExistFile.AutoSize = true;
			this.lblExistFile.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblExistFile.Location = new System.Drawing.Point(20, 33);
			this.lblExistFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblExistFile.Name = "lblExistFile";
			this.lblExistFile.Size = new System.Drawing.Size(267, 17);
			this.lblExistFile.TabIndex = 17;
			this.lblExistFile.Text = "Одинаковые файлы в папке-приемнике:";
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
			this.ssProgress.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.ssProgress.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.tsslblProgress,
			this.tsProgressBar});
			this.ssProgress.Location = new System.Drawing.Point(0, 677);
			this.ssProgress.Name = "ssProgress";
			this.ssProgress.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
			this.ssProgress.Size = new System.Drawing.Size(1401, 26);
			this.ssProgress.TabIndex = 17;
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
			this.tsProgressBar.Size = new System.Drawing.Size(533, 20);
			// 
			// tlCentral
			// 
			this.tlCentral.ColumnCount = 1;
			this.tlCentral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlCentral.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 27F));
			this.tlCentral.Controls.Add(this.pScanDir, 0, 0);
			this.tlCentral.Controls.Add(this.pGenres, 0, 2);
			this.tlCentral.Dock = System.Windows.Forms.DockStyle.Top;
			this.tlCentral.Location = new System.Drawing.Point(0, 31);
			this.tlCentral.Margin = new System.Windows.Forms.Padding(0);
			this.tlCentral.Name = "tlCentral";
			this.tlCentral.RowCount = 3;
			this.tlCentral.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlCentral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 10F));
			this.tlCentral.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
			this.tlCentral.Size = new System.Drawing.Size(1401, 84);
			this.tlCentral.TabIndex = 19;
			// 
			// pGenres
			// 
			this.pGenres.Controls.Add(this.rbtnFB22);
			this.pGenres.Controls.Add(this.rbtnFB2Librusec);
			this.pGenres.Controls.Add(this.lblFMFSGenres);
			this.pGenres.Dock = System.Windows.Forms.DockStyle.Top;
			this.pGenres.Location = new System.Drawing.Point(4, 55);
			this.pGenres.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.pGenres.Name = "pGenres";
			this.pGenres.Size = new System.Drawing.Size(1393, 26);
			this.pGenres.TabIndex = 10;
			// 
			// rbtnFB22
			// 
			this.rbtnFB22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbtnFB22.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnFB22.Location = new System.Drawing.Point(280, 0);
			this.rbtnFB22.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.rbtnFB22.Name = "rbtnFB22";
			this.rbtnFB22.Size = new System.Drawing.Size(72, 26);
			this.rbtnFB22.TabIndex = 25;
			this.rbtnFB22.Text = "fb2.2";
			this.rbtnFB22.UseVisualStyleBackColor = true;
			this.rbtnFB22.CheckedChanged += new System.EventHandler(this.RbtnFB22CheckedChanged);
			this.rbtnFB22.Click += new System.EventHandler(this.RbtnFB22Click);
			// 
			// rbtnFB2Librusec
			// 
			this.rbtnFB2Librusec.Checked = true;
			this.rbtnFB2Librusec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbtnFB2Librusec.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnFB2Librusec.Location = new System.Drawing.Point(141, 0);
			this.rbtnFB2Librusec.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.rbtnFB2Librusec.Name = "rbtnFB2Librusec";
			this.rbtnFB2Librusec.Size = new System.Drawing.Size(127, 26);
			this.rbtnFB2Librusec.TabIndex = 29;
			this.rbtnFB2Librusec.TabStop = true;
			this.rbtnFB2Librusec.Text = "fb2 Либрусек";
			this.rbtnFB2Librusec.UseVisualStyleBackColor = true;
			this.rbtnFB2Librusec.CheckedChanged += new System.EventHandler(this.RbtnFB2LibrusecCheckedChanged);
			this.rbtnFB2Librusec.Click += new System.EventHandler(this.RbtnFB2LibrusecClick);
			// 
			// lblFMFSGenres
			// 
			this.lblFMFSGenres.ForeColor = System.Drawing.Color.Navy;
			this.lblFMFSGenres.Location = new System.Drawing.Point(5, 0);
			this.lblFMFSGenres.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblFMFSGenres.Name = "lblFMFSGenres";
			this.lblFMFSGenres.Size = new System.Drawing.Size(128, 20);
			this.lblFMFSGenres.TabIndex = 10;
			this.lblFMFSGenres.Text = "Схема Жанров:";
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
			this.lvFilesCount.FullRowSelect = true;
			this.lvFilesCount.GridLines = true;
			this.lvFilesCount.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
			listViewItem16,
			listViewItem17,
			listViewItem18,
			listViewItem19,
			listViewItem20});
			this.lvFilesCount.Location = new System.Drawing.Point(0, 30);
			this.lvFilesCount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.lvFilesCount.Name = "lvFilesCount";
			this.lvFilesCount.Size = new System.Drawing.Size(296, 531);
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
			this.pInfo.Controls.Add(this.chBoxViewProgress);
			this.pInfo.Controls.Add(this.lvFilesCount);
			this.pInfo.Dock = System.Windows.Forms.DockStyle.Right;
			this.pInfo.Location = new System.Drawing.Point(1090, 115);
			this.pInfo.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.pInfo.Name = "pInfo";
			this.pInfo.Size = new System.Drawing.Size(311, 562);
			this.pInfo.TabIndex = 21;
			// 
			// chBoxViewProgress
			// 
			this.chBoxViewProgress.Dock = System.Windows.Forms.DockStyle.Top;
			this.chBoxViewProgress.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxViewProgress.Location = new System.Drawing.Point(0, 0);
			this.chBoxViewProgress.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.chBoxViewProgress.Name = "chBoxViewProgress";
			this.chBoxViewProgress.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
			this.chBoxViewProgress.Size = new System.Drawing.Size(311, 30);
			this.chBoxViewProgress.TabIndex = 22;
			this.chBoxViewProgress.Text = "Отображать ход работы";
			this.chBoxViewProgress.UseVisualStyleBackColor = true;
			this.chBoxViewProgress.CheckStateChanged += new System.EventHandler(this.ChBoxViewProgressCheckStateChanged);
			// 
			// pCentral
			// 
			this.pCentral.Controls.Add(this.tcResult);
			this.pCentral.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pCentral.Location = new System.Drawing.Point(0, 115);
			this.pCentral.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.pCentral.Name = "pCentral";
			this.pCentral.Size = new System.Drawing.Size(1090, 562);
			this.pCentral.TabIndex = 22;
			// 
			// cmsArchive
			// 
			this.cmsArchive.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.cmsArchive.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.tsmiFileAndArchiveReValidate,
			this.tsmi5,
			this.tsmiOpenInArchivator,
			this.tsmi4,
			this.tsmiOpenArchiveDir,
			this.tsmiFileDeleteFromDisk,
			this.toolStripSeparator2,
			this.tsmiArchiveCheckedAll,
			this.tsmiArchiveUnCheckedAll});
			this.cmsArchive.Name = "cmsValidator";
			this.cmsArchive.Size = new System.Drawing.Size(363, 206);
			// 
			// tsmiFileAndArchiveReValidate
			// 
			this.tsmiFileAndArchiveReValidate.Image = ((System.Drawing.Image)(resources.GetObject("tsmiFileAndArchiveReValidate.Image")));
			this.tsmiFileAndArchiveReValidate.Name = "tsmiFileAndArchiveReValidate";
			this.tsmiFileAndArchiveReValidate.Size = new System.Drawing.Size(362, 26);
			this.tsmiFileAndArchiveReValidate.Text = "Проверить файл заново (валидация)";
			this.tsmiFileAndArchiveReValidate.Click += new System.EventHandler(this.TsmiFileReValidateClick);
			// 
			// tsmi5
			// 
			this.tsmi5.Name = "tsmi5";
			this.tsmi5.Size = new System.Drawing.Size(359, 6);
			// 
			// tsmiOpenInArchivator
			// 
			this.tsmiOpenInArchivator.Image = ((System.Drawing.Image)(resources.GetObject("tsmiOpenInArchivator.Image")));
			this.tsmiOpenInArchivator.Name = "tsmiOpenInArchivator";
			this.tsmiOpenInArchivator.Size = new System.Drawing.Size(362, 26);
			this.tsmiOpenInArchivator.Text = "Открыть файл в архиваторе";
			this.tsmiOpenInArchivator.Click += new System.EventHandler(this.TsmiOpenFileInArchivatorClick);
			// 
			// tsmi4
			// 
			this.tsmi4.Name = "tsmi4";
			this.tsmi4.Size = new System.Drawing.Size(359, 6);
			// 
			// tsmiOpenArchiveDir
			// 
			this.tsmiOpenArchiveDir.Image = ((System.Drawing.Image)(resources.GetObject("tsmiOpenArchiveDir.Image")));
			this.tsmiOpenArchiveDir.Name = "tsmiOpenArchiveDir";
			this.tsmiOpenArchiveDir.Size = new System.Drawing.Size(362, 26);
			this.tsmiOpenArchiveDir.Text = "Открыть папку для выделенного архива";
			this.tsmiOpenArchiveDir.Click += new System.EventHandler(this.TsmiOpenFileDirClick);
			// 
			// tsmiFileDeleteFromDisk
			// 
			this.tsmiFileDeleteFromDisk.Image = ((System.Drawing.Image)(resources.GetObject("tsmiFileDeleteFromDisk.Image")));
			this.tsmiFileDeleteFromDisk.Name = "tsmiFileDeleteFromDisk";
			this.tsmiFileDeleteFromDisk.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.tsmiFileDeleteFromDisk.Size = new System.Drawing.Size(362, 26);
			this.tsmiFileDeleteFromDisk.Text = "Удалить zip архив с диска";
			this.tsmiFileDeleteFromDisk.Click += new System.EventHandler(this.TsmiDeleteFileFromDiskClick);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(359, 6);
			// 
			// tsmiArchiveCheckedAll
			// 
			this.tsmiArchiveCheckedAll.Image = ((System.Drawing.Image)(resources.GetObject("tsmiArchiveCheckedAll.Image")));
			this.tsmiArchiveCheckedAll.Name = "tsmiArchiveCheckedAll";
			this.tsmiArchiveCheckedAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
			| System.Windows.Forms.Keys.C)));
			this.tsmiArchiveCheckedAll.Size = new System.Drawing.Size(362, 26);
			this.tsmiArchiveCheckedAll.Text = "Пометить все файлы";
			this.tsmiArchiveCheckedAll.Click += new System.EventHandler(this.TsmiArchiveCheckedAllClick);
			// 
			// tsmiArchiveUnCheckedAll
			// 
			this.tsmiArchiveUnCheckedAll.Image = ((System.Drawing.Image)(resources.GetObject("tsmiArchiveUnCheckedAll.Image")));
			this.tsmiArchiveUnCheckedAll.Name = "tsmiArchiveUnCheckedAll";
			this.tsmiArchiveUnCheckedAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
			| System.Windows.Forms.Keys.U)));
			this.tsmiArchiveUnCheckedAll.Size = new System.Drawing.Size(362, 26);
			this.tsmiArchiveUnCheckedAll.Text = "Снять все отметки";
			this.tsmiArchiveUnCheckedAll.Click += new System.EventHandler(this.TsmiArchiveUnCheckedAllClick);
			// 
			// sfdLoadList
			// 
			this.sfdLoadList.RestoreDirectory = true;
			this.sfdLoadList.Title = "Загрузка Списка невалидных файлов";
			// 
			// ofDlg
			// 
			this.ofDlg.RestoreDirectory = true;
			// 
			// SFBTpFB2Validator
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pCentral);
			this.Controls.Add(this.pInfo);
			this.Controls.Add(this.tlCentral);
			this.Controls.Add(this.ssProgress);
			this.Controls.Add(this.tsValidator);
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "SFBTpFB2Validator";
			this.Size = new System.Drawing.Size(1401, 703);
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
			this.pViewError.ResumeLayout(false);
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
			this.tpOptions.ResumeLayout(false);
			this.pArchivator.ResumeLayout(false);
			this.pArchivator.PerformLayout();
			this.gboxValidatorPE.ResumeLayout(false);
			this.gboxValidatorDoubleClick.ResumeLayout(false);
			this.gboxCopyMoveOptions.ResumeLayout(false);
			this.gboxCopyMoveOptions.PerformLayout();
			this.ssProgress.ResumeLayout(false);
			this.ssProgress.PerformLayout();
			this.tlCentral.ResumeLayout(false);
			this.tlCentral.PerformLayout();
			this.pGenres.ResumeLayout(false);
			this.pInfo.ResumeLayout(false);
			this.pCentral.ResumeLayout(false);
			this.cmsArchive.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		private System.Windows.Forms.OpenFileDialog ofDlg;
		private System.Windows.Forms.Button btnArchivatorPath;
		private System.Windows.Forms.TextBox tboxArchivatorPath;
		private System.Windows.Forms.Label lblArchivatorPath;
		private System.Windows.Forms.Panel pArchivator;
		private System.Windows.Forms.ComboBox cboxValidatorForZipPE;
		private System.Windows.Forms.ComboBox cboxValidatorForZip;
		private System.Windows.Forms.Label lblValidatorForFB2;
		private System.Windows.Forms.Label lblValidatorForFB2Archive;
		private System.Windows.Forms.ComboBox cboxValidatorForFB2;
		private System.Windows.Forms.GroupBox gboxValidatorDoubleClick;
		private System.Windows.Forms.Label lblValidatorForFB2PE;
		private System.Windows.Forms.Label lblValidatorForFB2ArchivePE;
		private System.Windows.Forms.ComboBox cboxValidatorForFB2PE;
		private System.Windows.Forms.GroupBox gboxValidatorPE;
		private System.Windows.Forms.TabPage tpOptions;
		private System.Windows.Forms.Panel pGenres;
		private System.Windows.Forms.Label lblFMFSGenres;
		private System.Windows.Forms.CheckBox chBoxScanSubDir;
		private System.Windows.Forms.RadioButton rbtnFB22;
		private System.Windows.Forms.RadioButton rbtnFB2Librusec;
		private System.Windows.Forms.Button buttonOpenDir;
		private System.Windows.Forms.ToolStripButton tsbtnValidateStop;
		private System.Windows.Forms.ToolStripButton tsbtnValidate;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.OpenFileDialog sfdLoadList;
		private System.Windows.Forms.Button btnSaveList;
		private System.Windows.Forms.Button btnLoadList;
		private System.Windows.Forms.Panel pViewError;
		private System.Windows.Forms.ToolStripMenuItem tsmiArchiveUnCheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiNotFB2UnCheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiFB2UnCheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiArchiveCheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiNotFB2CheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiFB2CheckedAll;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.CheckBox chBoxViewProgress;
		private System.Windows.Forms.ToolStripButton tsbtnFilesWorkStop;
		private System.Windows.Forms.FolderBrowserDialog fbdDir;
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
		private System.Windows.Forms.ToolStripMenuItem tsmiReportAsFB2;
		private System.Windows.Forms.ToolStripMenuItem tsmiReportAsHTML;
		private System.Windows.Forms.ToolStripDropDownButton tsddbtnMakeReport;
		private System.Windows.Forms.ToolStripSeparator tsSep3;
		private System.Windows.Forms.ToolStripButton tsbtnDeleteFiles;
		private System.Windows.Forms.ToolStripButton tsbtnMoveFilesTo;
		private System.Windows.Forms.ToolStripButton tsbtnCopyFilesTo;
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
		#endregion
		
		#region Закрытые данные класса
		private readonly string m_FileSettingsPath	= Settings.Settings.ProgDir + @"\ValidatorSettings.xml";
		private bool			m_isSettingsLoaded	= false; // Только при true все изменения настроек сохраняются в файл.
		private DateTime m_dtStart;
		private BackgroundWorker m_bwv			= null;
		private BackgroundWorker m_bwcmd		= null;
		private string		m_sMessTitle		= string.Empty;
		private string		m_sScan				= string.Empty;
		private string		m_sFileWorkerMode	= string.Empty;
		private bool		m_bScanSubDirs		= true;
		private MiscListView m_mscLV			= new MiscListView(); // класс по работе с ListView
		private bool		m_bFilesWorked		= false; // флаг = true, если хоть один файл был на диске и был обработан (copy, move или delete)
		private string		m_TempDir			= Settings.Settings.TempDir;
		
		private SharpZipLibWorker m_sharpZipLib = new SharpZipLibWorker();

		// Color
		private Color	m_FB2ValidFontColor			= Color.Black;		// цвет для несжатых валидных fb2
		private Color	m_FB2NotValidFontColor		= Color.Black;		// цвет для несжатых не валидных fb2
		private Color	m_ZipFB2ValidFontColor		= Color.Green;		// цвет для валидных fb2 в zip
		private Color	m_ZipFB2NotValidFontColor	= Color.Blue;		// цвет для не валидных fb2 в zip
		private Color	m_ZipFontColor				= Color.BlueViolet;	// цвет для zip не fb2
		private Color	m_NotFB2FontColor			= Color.Black;		// цвет для всех остальных файлов
		// найденные файлы
		private int	m_nFB2Valid		= 0; // число валидных файлов
		private int	m_nFB2NotValid	= 0; // число не валидных файлов
		private int	m_nFB2Files		= 0; // число fb2 файлов (не сжатых)
		private int	m_nFB2ZipFiles	= 0; // число fb2.zip файлов
		private int	m_nNotFB2Files	= 0; // число других (не fb2) файлов
		//
		private const string	m_sNotValid		= " Не валидные fb2-файлы ";
		private	const string	m_sValid		= " Валидные fb2-файлы ";
		private const string	m_sNotFB2Files	= " Не fb2-файлы ";
		// Report
		private const string	m_FB2NotValidReportEmpty		= "Список не валидных fb2-файлов пуст!\nОтчет не сохранен.";
		private const string	m_FB2ValidReportEmpty			= "Список валидных fb2-файлов пуст!\nОтчет не сохранен.";
		private const string	m_NotFB2FileReportEmpty			= "Список не fb2-файлов пуст!\nОтчет не сохранен.";
		private const string	m_FB2NotValidFilesListReport	= "Список не валидных fb2-файлов";
		private const string	m_FB2ValidFilesListReport	 	= "Список валидных fb2-файлов";
		private const string	m_NotFB2FilesListReport 		= "Список не fb2-файлов";
		private const string	m_GeneratingReport				= "Генерация отчета";
		private const string	m_ReportSaveOk 					= "Отчет сохранен в файл:\n";
		private const string	m_HTMLFilter 					= "HTML файлы (*.html)|*.html|Все файлы (*.*)|*.*";
		private const string	m_FB2Filter 					= "fb2 файлы (*.fb2)|*.fb2|Все файлы (*.*)|*.*";
		private const string	m_TXTFilter 					= "TXT файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
		#endregion
		
		public SFBTpFB2Validator()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			// задание для кнопок ToolStrip стиля и положения текста и картинки
			SetToolButtonsSettings();
			
			InitializeValidateBackgroundWorker();
			InitializeFilesWorkerBackgroundWorker();
			
			#region первоначальная установка значений по умолчанию
			cboxExistFile.SelectedIndex			= 1;
			cboxValidatorForFB2.SelectedIndex	= 1;
			cboxValidatorForZip.SelectedIndex	= 1;
			cboxValidatorForFB2PE.SelectedIndex	= 0;
			cboxValidatorForZipPE.SelectedIndex	= 0;
			#endregion
			
			// инициализация контролов
			Init();
			// чтение настроек из xml-файла
			readSettingsFromXML();
			m_isSettingsLoaded = true;
		}
		#region Закрытые методы
		
		#region Закрытые общие вспомогательные методы
		// отключаем обработчики событий для Списков (убираем "тормоза")
		private void ConnectListsEventHandlers( bool bConnect ) {
			if( !bConnect ) {
				// для listViewNotValid
				listViewNotValid.BeginUpdate();
				this.listViewNotValid.SelectedIndexChanged -= new System.EventHandler(this.ListViewNotValidSelectedIndexChanged);
				this.listViewNotValid.DoubleClick -= new System.EventHandler(this.ListViewNotValidDoubleClick);
				this.listViewNotValid.KeyPress -= new System.Windows.Forms.KeyPressEventHandler(this.ListViewNotValidKeyPress);
				// для listViewValid
				listViewValid.BeginUpdate();
				this.listViewValid.SelectedIndexChanged -= new System.EventHandler(this.ListViewValidSelectedIndexChanged);
				this.listViewValid.DoubleClick -= new System.EventHandler(this.ListViewNotValidDoubleClick);
				this.listViewValid.KeyPress -= new System.Windows.Forms.KeyPressEventHandler(this.ListViewNotValidKeyPress);
				// для listViewNotFB2
				listViewNotFB2.BeginUpdate();
				this.tsmiOpenFileDir.Click -= new System.EventHandler(this.TsmiOpenFileDirClick);
				
			} else {
				// подключаем обработчики событий для Списков
				// для istViewNotValid
				listViewNotValid.EndUpdate();
				this.listViewNotValid.SelectedIndexChanged += new System.EventHandler(this.ListViewNotValidSelectedIndexChanged);
				this.listViewNotValid.DoubleClick += new System.EventHandler(this.ListViewNotValidDoubleClick);
				this.listViewNotValid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ListViewNotValidKeyPress);
				// для listViewValid
				listViewValid.EndUpdate();
				this.listViewValid.SelectedIndexChanged += new System.EventHandler(this.ListViewValidSelectedIndexChanged);
				this.listViewValid.DoubleClick += new System.EventHandler(this.ListViewNotValidDoubleClick);
				this.listViewValid.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ListViewNotValidKeyPress);
				// для listViewNotFB2
				listViewNotFB2.EndUpdate();
				this.tsmiOpenFileDir.Click += new System.EventHandler(this.TsmiOpenFileDirClick);
			}
		}
		#endregion
		
		#region Закрытые методы реализации BackgroundWorker Валидатора
		// Инициализация перед использование BackgroundWorker Валидации
		private void InitializeValidateBackgroundWorker() {
			m_bwv = new BackgroundWorker();
			m_bwv.WorkerReportsProgress			= true; // Позволить выводить прогресс процесса
			m_bwv.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
			m_bwv.DoWork 				+= new DoWorkEventHandler( bwv_DoWork );
			m_bwv.ProgressChanged 		+= new ProgressChangedEventHandler( bwv_ProgressChanged );
			m_bwv.RunWorkerCompleted 	+= new RunWorkerCompletedEventHandler( bwv_RunWorkerCompleted );
		}
		
		// Валидация
		private void bwv_DoWork( object sender, DoWorkEventArgs e ) {
			int nAllFiles = 0;
			List<string> lDirList = new List<string>();
			if( !m_bScanSubDirs ) {
				// сканировать только указанную папку
				lDirList.Add( m_sScan );
				lvFilesCount.Items[0].SubItems[1].Text = "1";
			} else {
				// сканировать и все подпапки
				nAllFiles = filesWorker.DirsParser( m_bwv, e, m_sScan, ref lvFilesCount, ref lDirList, false );
			}
			
			// отобразим число всех файлов в папке сканирования
			lvFilesCount.Items[1].SubItems[1].Text = nAllFiles.ToString();
			
			// проверка остановки процесса
			if( ( m_bwv.CancellationPending == true ) )  {
				e.Cancel = true; // Выставить окончание - по отмене, сработает событие bwv_RunWorkerCompleted
				return;
			}
			
			// проверка, есть ли хоть один файл в папке для сканирования
			if( nAllFiles == 0 ) {
				MessageBox.Show( "В указанной папке не найдено ни одного файла!", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				Init();
				return;
			}
			
			// проверка файлов
			tsslblProgress.Text		= "Проверка найденных файлов на валидность:";
			tsProgressBar.Maximum	= nAllFiles;
			tsProgressBar.Value		= 0;

			FB2Validator fv2Validator = new FB2Validator();
			ConnectListsEventHandlers( false );
			string sExt = "", sFile = "";
			foreach( string s in lDirList ) {
				DirectoryInfo diFolder = new DirectoryInfo( s );
				foreach( FileInfo fiNextFile in diFolder.GetFiles() ) {
					// Проверить флаг на остановку процесса
					if( ( m_bwv.CancellationPending == true ) ) {
						e.Cancel = true; // Выставить окончание - по отмене, сработает событие bwv_RunWorkerCompleted
						return;
					}
					sFile	= s + "\\" + fiNextFile.Name;
					sExt	= Path.GetExtension( sFile ).ToLower();
					if( sExt == ".fb2" ) {
						++m_nFB2Files;
						ParseFB2File( sFile, fv2Validator );
					} else if( sExt == ".zip" || sExt == ".fbz" ) {
						// очистка временной папки
						filesWorker.RemoveDir( m_TempDir );
						Directory.CreateDirectory( m_TempDir );
						ParseArchiveFile( sFile, fv2Validator, m_TempDir );
					} else {
						// разные файлы
						++m_nNotFB2Files;
						ListViewItem item = new ListViewItem( sFile, 0 );
						item.ForeColor = m_NotFB2FontColor;
						item.SubItems.Add( Path.GetExtension( sFile ) );
						FileInfo fi = new FileInfo( sFile );
						item.SubItems.Add( filesWorker.FormatFileLength( fi.Length ) );
						listViewNotFB2.Items.AddRange( new ListViewItem[]{ item } );

						m_bwv.ReportProgress( 0 ); // отобразим данные в контролах
					}
				}
			}
			lDirList.Clear();
		}
		
		// Отобразим результат Валидации
		private void bwv_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
			if( chBoxViewProgress.Checked )
				ValidProgressData();
			++tsProgressBar.Value;
		}
		
		// Проверяем это отмена, ошибка, или конец задачи и сообщить
		private void bwv_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {
			ValidProgressData(); // Отобразим результат Валидации
			ConnectListsEventHandlers( true );
			
			DateTime dtEnd = DateTime.Now;
			filesWorker.RemoveDir( m_TempDir );
			
			// авторазмер колонок списков
			m_mscLV.AutoResizeColumns(listViewNotValid);
			m_mscLV.AutoResizeColumns(listViewValid);
			m_mscLV.AutoResizeColumns(listViewNotFB2);
			
			tsslblProgress.Text = Settings.Settings.GetReady();
			SetValidingStartEnabled( true );
			
			string sTime = dtEnd.Subtract( m_dtStart ).ToString() + " (час.:мин.:сек.)";
			string sMessCanceled	= "Проверка файлов на соответствие FictionBook.xsd схеме остановлена!\nЗатрачено времени: "+sTime;
			string sMessError		= "";
			string sMessDone		= "Проверка файлов на соответствие FictionBook.xsd схеме завершена!\nЗатрачено времени: "+sTime;
			
			if( ( e.Cancelled == true ) ) {
				MessageBox.Show( sMessCanceled, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			} else if( e.Error != null ) {
				sMessError = "Error!\n" + e.Error.Message + "\n" + e.Error.StackTrace + "\nЗатрачено времени: "+sTime;
				MessageBox.Show( sMessError, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			} else {
				MessageBox.Show( sMessDone, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
			
			// выделяем 1-ю найденную книгу
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 ) {
				l.Focus();
				l.Items[0].Focused	= true;
				l.Items[0].Selected	= true;
			}
		}
		#endregion
		
		#region Закрытые методы реализации BackgroundWorker Копирование / Перемещение / Удаление
		// Инициализация перед использование BackgroundWorker Копирование / Перемещение / Удаление
		private void InitializeFilesWorkerBackgroundWorker() {
			m_bwcmd = new BackgroundWorker();
			m_bwcmd.WorkerReportsProgress		= true; // Позволить выводить прогресс процесса
			m_bwcmd.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
			m_bwcmd.DoWork				+= new DoWorkEventHandler( bwcmd_DoWork );
			m_bwcmd.ProgressChanged 	+= new ProgressChangedEventHandler( bwcmd_ProgressChanged );
			m_bwcmd.RunWorkerCompleted	+= new RunWorkerCompletedEventHandler( bwcmd_RunWorkerCompleted );
		}
		
		// Обработка файлов
		private void bwcmd_DoWork( object sender, DoWorkEventArgs e ) {
			ConnectListsEventHandlers( false );
			m_bFilesWorked = false;
			switch( m_sFileWorkerMode ) {
				case "Copy":
					switch( tcResult.SelectedIndex ) {
						case 0:
							// не валидные fb2-файлы
							CopyOrMoveFilesTo( m_bwcmd, e, true, tboxSourceDir.Text.Trim(), tboxFB2NotValidDirCopyTo.Text.Trim(),
							                  listViewNotValid, tpNotValid, "Копирование не валидных fb2-файлов:", m_sNotValid );
							break;
						case 1:
							// валидные fb2-файлы
							CopyOrMoveFilesTo( m_bwcmd, e, true, tboxSourceDir.Text.Trim(), tboxFB2ValidDirCopyTo.Text.Trim(),
							                  listViewValid, tpValid, "Копирование валидных fb2-файлов:", m_sValid );
							break;
						case 2:
							// не fb2-файлы
							CopyOrMoveFilesTo( m_bwcmd, e, true, tboxSourceDir.Text.Trim(), tboxNotFB2DirCopyTo.Text.Trim(),
							                  listViewNotFB2, tpNotFB2Files, "Копирование не fb2-файлов:", m_sNotFB2Files );
							break;
					}
					break;
				case "Move":
					switch( tcResult.SelectedIndex ) {
						case 0:
							// не валидные fb2-файлы
							CopyOrMoveFilesTo( m_bwcmd, e, false, tboxSourceDir.Text.Trim(), tboxFB2NotValidDirMoveTo.Text.Trim(),
							                  listViewNotValid, tpNotValid, "Перемещение не валидных fb2-файлов:", m_sNotValid );
							break;
						case 1:
							// валидные fb2-файлы
							CopyOrMoveFilesTo( m_bwcmd, e, false, tboxSourceDir.Text.Trim(), tboxFB2ValidDirMoveTo.Text.Trim(),
							                  listViewValid, tpValid, "Перемещение валидных fb2-файлов:", m_sValid );
							break;
						case 2:
							// не fb2-файлы
							CopyOrMoveFilesTo( m_bwcmd, e, false, tboxSourceDir.Text.Trim(), tboxNotFB2DirMoveTo.Text.Trim(),
							                  listViewNotFB2, tpNotFB2Files, "Перемещение не fb2-файлов:", m_sNotFB2Files );
							break;
					}
					break;
				case "Delete":
					switch( tcResult.SelectedIndex ) {
						case 0:
							// не валидные fb2-файлы
							DeleteFiles( m_bwcmd, e, listViewNotValid, tpNotValid, "Удаление не валидных fb2-файлов:", m_sNotValid );
							break;
						case 1:
							// валидные fb2-файлы
							DeleteFiles( m_bwcmd, e, listViewValid, tpValid, "Удаление валидных fb2-файлов:", m_sValid );
							break;
						case 2:
							// не fb2-файлы
							DeleteFiles( m_bwcmd, e, listViewNotFB2, tpNotFB2Files, "Удаление не fb2-файлов:", m_sNotFB2Files );
							break;
					}
					break;
				default:
					return;
			}
		}
		
		// Отобразим результат Копирования / Перемещения / Удаления
		private void bwcmd_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
			++tsProgressBar.Value;
		}
		
		// Завершение работы Обработчика Файлов
		private void bwcmd_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {
			ConnectListsEventHandlers( true );
			
			string sMessCanceled, sMessError, sMessDone, sTabPageDefText, sMessTitle;
			sMessCanceled = sMessError = sMessDone = sTabPageDefText = sMessTitle = "";
			ListView	lv	= null;
			TabPage		tp	= null;
			switch( tcResult.SelectedIndex ) {
				case 0:
					// не валидные fb2-файлы
					sTabPageDefText = m_sNotValid;
					lv = listViewNotValid;
					tp = tpNotValid;
					break;
				case 1:
					// валидные fb2-файлы
					sTabPageDefText = m_sValid;
					lv = listViewValid;
					tp = tpValid;
					break;
				case 2:
					// не fb2-файлы
					sTabPageDefText = m_sNotFB2Files;
					lv = listViewNotFB2;
					tp = tpNotFB2Files;
					break;
			}
			
			switch( m_sFileWorkerMode ) {
				case "Copy":
					sMessTitle		= "SharpFBTools - Копирование помеченных файлов";
					sMessDone 		= "Копирование файлов в указанную папку завершено!";
					sMessCanceled	= "Копирование файлов в указанную папку остановлено!";
					break;
				case "Move":
					sMessTitle		= "SharpFBTools - Перемещение помеченных файлов";
					sMessDone 		= "Перемещение файлов в указанную папку завершено!";
					sMessCanceled	= "Перемещение файлов в указанную папку остановлено!";
					break;
				case "Delete":
					sMessTitle		= "SharpFBTools - Удаление помеченных файлов";
					sMessDone 		= "Удаление файлов из папки-источника завершено!";
					sMessCanceled	= "Удаление файлов из папки-источника остановлено!";
					break;
			}
			if( !m_bFilesWorked ) {
				string s = "На диске не найдено ни одного файла из помеченных!\n";
				switch( m_sFileWorkerMode ) {
					case "Copy":
						sMessDone = s + "Копирование файлов в указанную папку не произведено!";
						break;
					case "Move":
						sMessDone = s + "Перемещение файлов в указанную папку не произведено!";
						break;
					case "Delete":
						sMessDone = s + "Удаление файлов из папки-источника не произведено!";
						break;
				}
			}
			
			tp.Text = sTabPageDefText + "( " + lv.Items.Count.ToString() +" )";
			lvFilesCount.Items[1].SubItems[1].Text = ( listViewNotValid.Items.Count + listViewValid.Items.Count +
			                                          listViewNotFB2.Items.Count ).ToString();
			tsslblProgress.Text = Settings.Settings.GetReady();
			SetFilesWorkerStartEnabled( true );
			
			// Проверяем это отмена, ошибка, или конец задачи и сообщить
			if( ( e.Cancelled == true ) ) {
				MessageBox.Show( sMessCanceled, sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			} else if( e.Error != null ) {
				sMessError = "Ошибка:\n" + e.Error.Message + "\n" + e.Error.StackTrace;
				MessageBox.Show( sMessError, sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			} else {
				MessageBox.Show( sMessDone, sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
		}
		
		#endregion
		
		#region Закрытые вспомогательные методы класса
		// сохранение настроек в xml-файл
		private void saveSettingsToXml() {
			#region Код
			if( m_isSettingsLoaded ) {
				// защита от "затирания" настроек в файле, когда в некоторые контролы данные еще не загрузились
				XDocument doc = new XDocument(
					new XDeclaration("1.0", "utf-8", "yes"),
					new XElement("Settings",
					             new XComment("Папка для проверяемых fb2-файлов"),
					             new XElement("SourceDir", tboxSourceDir.Text.Trim()),
					             new XComment("Невалидные файлы"),
					             new XElement("NotValidFB2Files",
					                          new XElement("CopyTo", tboxFB2NotValidDirCopyTo.Text),
					                          new XElement("MoveTo", tboxFB2NotValidDirMoveTo.Text)
					                         ),
					             new XComment("Валидные файлы"),
					             new XElement("ValidFB2Files",
					                          new XElement("CopyTo", tboxFB2ValidDirCopyTo.Text),
					                          new XElement("MoveTo", tboxFB2ValidDirMoveTo.Text)
					                         ),
					             new XComment("Не fb2-файлы"),
					             new XElement("NotFB2Files",
					                          new XElement("CopyTo", tboxNotFB2DirCopyTo.Text),
					                          new XElement("MoveTo", tboxNotFB2DirMoveTo.Text)
					                         ),
					             new XComment("Активная Схема Жанров"),
					             new XElement("FB2Genres",
					                          new XAttribute("Librusec", rbtnFB2Librusec.Checked),
					                          new XAttribute("FB22", rbtnFB22.Checked)
					                         ),
					             new XComment("Операции с одинаковыми файлами в папке-приемнике"),
					             new XElement("FileExsistMode",
					                          new XAttribute("Index", cboxExistFile.SelectedIndex),
					                          new XAttribute("Name", cboxExistFile.Text)
					                         ),
					             new XComment("Обрабатывать подкаталоги"),
					             new XElement("ScanSubDirs", chBoxScanSubDir.Checked),
					             new XComment("Действие по двойному щелчку мышки на Списках"),
					             new XElement("DoubleClick",
					                          new XComment("Для незапакованных fb2"),
					                          new XElement("FB2",
					                                       new XAttribute("Index", cboxValidatorForFB2.SelectedIndex),
					                                       new XAttribute("Name", cboxValidatorForFB2.Text)
					                                      ),
					                          new XComment("Для запакованных fb2"),
					                          new XElement("Zip",
					                                       new XAttribute("Index", cboxValidatorForZip.SelectedIndex),
					                                       new XAttribute("Name", cboxValidatorForZip.Text)
					                                      )
					                         ),
					             new XComment("Действие по нажатию клавиши Enter на Списках"),
					             new XElement("EnterPress",
					                          new XComment("Для незапакованных fb2"),
					                          new XElement("FB2",
					                                       new XAttribute("Index", cboxValidatorForFB2PE.SelectedIndex),
					                                       new XAttribute("Name", cboxValidatorForFB2PE.Text)
					                                      ),
					                          new XComment("Для запакованных fb2"),
					                          new XElement("Zip",
					                                       new XAttribute("Index", cboxValidatorForZipPE.SelectedIndex),
					                                       new XAttribute("Name", cboxValidatorForZipPE.Text)
					                                      )
					                         ),
					             
					             new XComment("Путь к Архиватору (GUI)"),
					             new XElement("ArchivatorGUIPath", tboxArchivatorPath.Text),
					             new XComment("Отображать изменения хода работы"),
					             new XElement("Progress", chBoxViewProgress.Checked)
					            )
				);
				doc.Save(m_FileSettingsPath);
			}
			#endregion
		}
		
		// загрузка настроек из xml-файла
		private void readSettingsFromXML() {
			#region Код
			if( File.Exists( m_FileSettingsPath ) ) {
				XElement xmlTree = XElement.Load( m_FileSettingsPath );
				// Папка для проверяемых fb2-файлов
				if( xmlTree.Element("SourceDir") != null )
					tboxSourceDir.Text = xmlTree.Element("SourceDir").Value;
				// Невалидные файлы
				if( xmlTree.Element("NotValidFB2Files") != null ) {
					XElement xmlNotValidFB2Files = xmlTree.Element("NotValidFB2Files");
					if( xmlNotValidFB2Files.Element("CopyTo") != null )
						tboxFB2NotValidDirCopyTo.Text = xmlNotValidFB2Files.Element("CopyTo").Value;
					if( xmlNotValidFB2Files.Element("MoveTo") != null )
						tboxFB2NotValidDirMoveTo.Text = xmlNotValidFB2Files.Element("MoveTo").Value;
				}
				// Валидные файлы
				if( xmlTree.Element("ValidFB2Files") != null ) {
					XElement xmlValidFB2Files = xmlTree.Element("ValidFB2Files");
					if( xmlValidFB2Files.Element("CopyTo") != null )
						tboxFB2ValidDirCopyTo.Text = xmlValidFB2Files.Element("CopyTo").Value;
					if( xmlValidFB2Files.Element("MoveTo") != null )
						tboxFB2ValidDirMoveTo.Text = xmlValidFB2Files.Element("MoveTo").Value;
				}
				// Не fb2-файлы
				if( xmlTree.Element("NotFB2Files") != null ) {
					XElement xmlNotFB2Files = xmlTree.Element("NotFB2Files");
					if( xmlNotFB2Files.Element("CopyTo") != null )
						tboxNotFB2DirCopyTo.Text = xmlNotFB2Files.Element("CopyTo").Value;
					if( xmlNotFB2Files.Element("MoveTo") != null )
						tboxNotFB2DirMoveTo.Text = xmlNotFB2Files.Element("MoveTo").Value;
				}
				// Активная Схема Жанров
				if( xmlTree.Element("FB2Genres") != null ) {
					XElement xmlFB2Genres = xmlTree.Element("FB2Genres");
					if( xmlFB2Genres.Attribute("Librusec") != null )
						rbtnFB2Librusec.Checked = Convert.ToBoolean( xmlFB2Genres.Attribute("Librusec").Value );
					if( xmlFB2Genres.Attribute("FB22") != null )
						rbtnFB22.Checked = Convert.ToBoolean( xmlFB2Genres.Attribute("FB22").Value );
				}
				// Операции с одинаковыми файлами в папке-приемнике
				if( xmlTree.Element("FileExsistMode") != null ) {
					if( xmlTree.Element("FileExsistMode").Attribute("Index") != null )
						cboxExistFile.SelectedIndex = Convert.ToInt16( xmlTree.Element("FileExsistMode").Attribute("Index").Value );
				}
				// Обрабатывать подкаталоги
				if( xmlTree.Element("ScanSubDirs") != null )
					chBoxScanSubDir.Checked = Convert.ToBoolean( xmlTree.Element("ScanSubDirs").Value );
				
				/* Действие по двойному щелчку мышки на Списках */
				if( xmlTree.Element("DoubleClick") != null ) {
					XElement xmlDoubleClick = xmlTree.Element("DoubleClick");
					// Для незапакованных fb2
					if( xmlDoubleClick.Element("FB2") != null ) {
						if( xmlDoubleClick.Element("FB2").Attribute("Index") != null )
							cboxValidatorForFB2.SelectedIndex = Convert.ToInt16( xmlDoubleClick.Element("FB2").Attribute("Index").Value );
					}
					// Для запакованных fb2
					if( xmlDoubleClick.Element("Zip") != null ) {
						if( xmlDoubleClick.Element("Zip").Attribute("Index") != null )
							cboxValidatorForZip.SelectedIndex = Convert.ToInt16( xmlDoubleClick.Element("Zip").Attribute("Index").Value );
					}
				}
				
				/* Действие по нажатию клавиши Enter на Списках */
				if( xmlTree.Element("EnterPress") != null ) {
					XElement xmlEnterPress = xmlTree.Element("EnterPress");
					// Для незапакованных fb2
					if( xmlEnterPress.Element("FB2") != null ) {
						if( xmlEnterPress.Element("FB2").Attribute("Index") != null )
							cboxValidatorForFB2PE.SelectedIndex = Convert.ToInt16( xmlEnterPress.Element("FB2").Attribute("Index").Value );
					}
					// Для запакованных fb2
					if( xmlEnterPress.Element("Zip") != null ) {
						if( xmlEnterPress.Element("Zip").Attribute("Index") != null )
							cboxValidatorForZipPE.SelectedIndex = Convert.ToInt16( xmlEnterPress.Element("Zip").Attribute("Index").Value );
					}
				}
				
				// Путь к Архиватору (GUI)
				if( xmlTree.Element("ArchivatorGUIPath") != null )
					tboxArchivatorPath.Text = xmlTree.Element("ArchivatorGUIPath").Value;
				// Отображать изменения хода работы
				if( xmlTree.Element("Progress") != null )
					chBoxViewProgress.Checked = Convert.ToBoolean( xmlTree.Element("Progress").Value );
			}
			#endregion
		}
		
		// Отобразим результат Валидации
		private void ValidProgressData() {
			lvFilesCount.Items[2].SubItems[1].Text = m_nFB2Files.ToString();
			lvFilesCount.Items[3].SubItems[1].Text = m_nFB2ZipFiles.ToString();
			lvFilesCount.Items[4].SubItems[1].Text = m_nNotFB2Files.ToString();
			
			tpNotFB2Files.Text	= m_sNotFB2Files + "( " + m_nNotFB2Files.ToString() + " ) " ;
			tpValid.Text		= m_sValid + "( " + m_nFB2Valid.ToString() + " ) " ;
			tpNotValid.Text		= m_sNotValid + "( " + m_nFB2NotValid.ToString() + " ) " ;
		}
		
		// инициализация контролов и переменных
		private void Init() {
			for( int i=0; i!=lvFilesCount.Items.Count; ++i ) {
				lvFilesCount.Items[i].SubItems[1].Text	= "0";
			}
			listViewNotValid.Items.Clear();
			listViewValid.Items.Clear();
			listViewNotFB2.Items.Clear();
			rеboxNotValid.Clear();
			tpNotValid.Text		= m_sNotValid;
			tpValid.Text		= m_sValid;
			tpNotFB2Files.Text	= m_sNotFB2Files;
			tsProgressBar.Value	= 0;
			tsslblProgress.Text		= Settings.Settings.GetReady();
			tsProgressBar.Visible	= false;
			m_nFB2Valid	= m_nFB2NotValid = m_nFB2Files = m_nFB2ZipFiles = m_nNotFB2Files = 0;
			// очистка временной папки
			filesWorker.RemoveDir( m_TempDir );
		}
		
		// доступность контролов при Валидации
		private void SetValidingStartEnabled( bool bEnabled ) {
			tcResult.Enabled = tsbtnValidate.Enabled = tsbtnCopyFilesTo.Enabled = tsbtnMoveFilesTo.Enabled =
				tsbtnDeleteFiles.Enabled = tsddbtnMakeFileList.Enabled = tsddbtnMakeReport.Enabled = pScanDir.Enabled =
				gboxCopyMoveOptions.Enabled = pGenres.Enabled = pViewError.Enabled = bEnabled;
			tsbtnValidateStop.Enabled = tsProgressBar.Visible = !bEnabled;
			tcResult.Refresh();
			ssProgress.Refresh();
		}
		
		// доступность контролов при Обработке файлов
		private void SetFilesWorkerStartEnabled( bool bEnabled ) {
			tcResult.Enabled = tsbtnValidate.Enabled = tsbtnCopyFilesTo.Enabled = tsbtnMoveFilesTo.Enabled = tsbtnDeleteFiles.Enabled =
				tsddbtnMakeFileList.Enabled = tsddbtnMakeReport.Enabled = pScanDir.Enabled =
				gboxCopyMoveOptions.Enabled = pGenres.Enabled = pViewError.Enabled = bEnabled;
			tsbtnFilesWorkStop.Enabled	= tsProgressBar.Visible = !bEnabled;
			tcResult.Refresh();
			ssProgress.Refresh();
		}
		
		// возвращает текущий ListView в зависимости от выбранной вкладки
		private ListView GetCurrentListWiew()
		{
			ListView l = null;
			switch( tcResult.SelectedIndex ) {
				case 0:
					// не валидные fb2-файлы
					l = listViewNotValid;
					break;
				case 1:
					// валидные fb2-файлы
					l = listViewValid;
					break;
				case 2:
					// не fb2-файлы
					l = listViewNotFB2;
					break;
			}
			return l;
		}
		
		// отметить все итемы ListView
		private void CheckAll() {
			ListView l = GetCurrentListWiew();
			m_mscLV.CheckAllListViewItems( l, true );
		}
		
		// снять отметки со всех итемов ListView
		private void UnCheckAll() {
			ListView l = GetCurrentListWiew();
			m_mscLV.UnCheckAllListViewItems( l.CheckedItems );
		}
		#endregion
		
		#region Закрытые Парсеры файлов и архивов
		// парсер несжатого fb2-файла
		private void ParseFB2File( string sFile, FB2Validator fv2Validator ) {
			string sMsg = rbtnFB2Librusec.Checked ? fv2Validator.ValidatingFB2LibrusecFile( sFile ) : fv2Validator.ValidatingFB22File( sFile );
			if ( sMsg == "" ) {
				// файл валидный
				++m_nFB2Valid;
				//listViewValid.Items.Add( sFile );
				ListViewItem item = new ListViewItem( sFile, 0 );
				item.ForeColor = m_FB2ValidFontColor;
				FileInfo fi = new FileInfo( sFile );
				item.SubItems.Add( filesWorker.FormatFileLength( fi.Length ) );
				listViewValid.Items.AddRange( new ListViewItem[]{ item } );
			} else {
				// файл не валидный
				++m_nFB2NotValid;
				ListViewItem item = new ListViewItem( sFile, 0 );
				item.ForeColor = m_FB2NotValidFontColor;
				item.SubItems.Add( sMsg );
				FileInfo fi = new FileInfo( sFile );
				item.SubItems.Add( filesWorker.FormatFileLength( fi.Length ) );
				listViewNotValid.Items.AddRange( new ListViewItem[]{ item } );
			}
			m_bwv.ReportProgress( 0 ); // отобразим данные в контролах
		}
		
		// парсер архива
		private void ParseArchiveFile( string sArchiveFile, FB2Validator fv2Validator, string sTempDir ) {
			string sExt = Path.GetExtension( sArchiveFile ).ToLower();
			m_sharpZipLib.UnZipFiles(sArchiveFile, sTempDir, 0, false, null, 4096);

			string [] files = Directory.GetFiles( sTempDir );
			if( files.Length > 0 ) {
				string sMsg = string.Empty;
				string sFileName = string.Empty;
				foreach (string file in files) {
					// валидация
					sMsg = rbtnFB2Librusec.Checked ? fv2Validator.ValidatingFB2LibrusecFile( file ) : fv2Validator.ValidatingFB22File( file );
					sFileName = Path.GetFileName( file );
					if ( sMsg == "" ) {
						// файл валидный - это fb2
						++m_nFB2Valid;
						++m_nFB2ZipFiles;
						//listViewValid.Items.Add( sArchiveFile + "/" + sFileName );
						ListViewItem item = new ListViewItem( sArchiveFile + "/" + sFileName, 0 );
						item.ForeColor = m_ZipFB2ValidFontColor;
						FileInfo fi = new FileInfo( sArchiveFile );
						string s = filesWorker.FormatFileLength( fi.Length );
						fi = new FileInfo( sTempDir+"\\"+sFileName );
						s += " / "+filesWorker.FormatFileLength( fi.Length );
						item.SubItems.Add( s );
						listViewValid.Items.AddRange( new ListViewItem[]{ item } );
					} else {
						// файл в архиве не валидный - посмотрим, что это
						sExt = Path.GetExtension( sFileName ).ToLower();
						if( sExt != ".fb2" ) {
							sMsg = "Тип файла: " + sExt;
							++m_nNotFB2Files;
							ListViewItem item = new ListViewItem( sArchiveFile + "/" + sFileName, 0 );
							item.ForeColor = m_ZipFontColor;
							item.SubItems.Add( Path.GetExtension( sArchiveFile + "/" + sFileName ) );
							FileInfo fi = new FileInfo( sArchiveFile );
							string s = filesWorker.FormatFileLength( fi.Length );
							fi = new FileInfo( sTempDir+"\\"+sFileName );
							s += " / "+filesWorker.FormatFileLength( fi.Length );
							item.SubItems.Add( s );
							listViewNotFB2.Items.AddRange( new ListViewItem[]{ item } );
						} else {
							++m_nFB2ZipFiles;
							++m_nFB2NotValid;
							ListViewItem item = new ListViewItem( sArchiveFile + "/" + sFileName, 0 );
							item.ForeColor = m_ZipFB2NotValidFontColor;
							item.SubItems.Add( sMsg );
							FileInfo fi = new FileInfo( sArchiveFile );
							string s = filesWorker.FormatFileLength( fi.Length );
							fi = new FileInfo( sTempDir+"\\"+sFileName );
							s += " / "+filesWorker.FormatFileLength( fi.Length );
							item.SubItems.Add( s );
							listViewNotValid.Items.AddRange( new ListViewItem[]{ item } );
						}
					}
				}
			}
			m_bwv.ReportProgress( 0 ); // отобразим данные в контролах
		}
		#endregion
		
		#region Закрытая Реализация Copy/Move/Delete помеченных файлов
		// копировать или переместить файлы в...
		private void CopyOrMoveFilesTo( BackgroundWorker bw, DoWorkEventArgs e,
		                               bool IsCopy, string SourceDir, string TargetDir,
		                               ListView lv, TabPage tp,
		                               string ProgressText, string TabPageDefText ) {
			#region Код
			System.Windows.Forms.ListView.CheckedListViewItemCollection checkedItems = lv.CheckedItems;
			tsslblProgress.Text = ProgressText;
			int i=0;
			foreach( ListViewItem lvi in checkedItems ) {
				// Проверить флаг на остановку процесса
				if( ( bw.CancellationPending == true ) ) {
					e.Cancel = true; // Выставить окончание - по отмене, сработает событие bwcmd_RunWorkerCompleted
					break;
				} else {
					string FilePath = lvi.Text.Split('/')[0];
					// есть ли такая книга на диске? Если нет - то смотрим следующую
					if( !File.Exists( FilePath ) ) {
						bw.ReportProgress( ++i ); // отобразим данные в контролах
						break;
					}
					
					if (lvi.Text.IndexOf('/') != -1) {
						// обработка файлов из архивов
						string FileFromZip = lvi.Text.Split('/')[1];
						if( File.Exists( FilePath ) ) {
							// Copy
							m_sharpZipLib.UnZipSelectedFile( FilePath, FileFromZip,
							                                filesWorker.buildTargetDir(FilePath, SourceDir, TargetDir),
							                                cboxExistFile.SelectedIndex, null, 4096 );
							if( !IsCopy ) {
								// Move
								if (m_sharpZipLib.DeleteSelectedFile( FilePath, FileFromZip, null ) ) {
									lv.Items.Remove( lvi );
									tp.Text = TabPageDefText + "( " + lv.Items.Count.ToString() +" )";
								}
							}
							m_bFilesWorked |= true;
						}
					} else {
						// обработка НЕ zip (fb2-файлы и другое)
						string NewPath = TargetDir + FilePath.Remove( 0, SourceDir.Length );
						FileInfo fi = new FileInfo( NewPath );
						if( !fi.Directory.Exists )
							Directory.CreateDirectory( fi.Directory.ToString() );

						if( File.Exists( NewPath ) ) {
							if( cboxExistFile.SelectedIndex==0 )
								File.Delete( NewPath );
							else
								NewPath = filesWorker.createFilePathWithSufix(NewPath, cboxExistFile.SelectedIndex);
						}
						
//						Regex rx = new Regex( @"\\+" );
//						FilePath = rx.Replace( FilePath, "\\" );
						if( File.Exists( FilePath ) ) {
							if( IsCopy )
								File.Copy( FilePath, NewPath );
							else {
								File.Move( FilePath, NewPath );
								lv.Items.Remove( lvi );
								tp.Text = TabPageDefText + "( " + lv.Items.Count.ToString() +" )";
							}
							m_bFilesWorked |= true;
						}
					}
					bw.ReportProgress( ++i ); // отобразим данные в контролах
				}
			}
			#endregion
		}
		
		// удалить файлы...
		private void DeleteFiles( BackgroundWorker bw, DoWorkEventArgs e,
		                         ListView lv, TabPage tp, string sProgressText, string sTabPageDefText ) {
			#region Код
			int i = 0;
			System.Windows.Forms.ListView.CheckedListViewItemCollection checkedItems = lv.CheckedItems;
			tsslblProgress.Text = sProgressText;
			string sFilePath = "";
			foreach( ListViewItem lvi in checkedItems ) {
				// Проверить флаг на остановку процесса
				if( ( bw.CancellationPending == true ) ) {
					e.Cancel = true; // Выставить окончание - по отмене, сработает событие bwcmd_RunWorkerCompleted
					break;
				} else {
					sFilePath = lvi.Text.Split('/')[0];
					if (lvi.Text.IndexOf('/') != -1) {
						// обработка файлов из архивов
						string fileFromZip = lvi.Text.Split('/')[1];
						if( File.Exists( sFilePath ) ) {
							if ( m_sharpZipLib.DeleteSelectedFile(sFilePath, fileFromZip, null) ) {
								lv.Items.Remove( lvi );
								tp.Text = sTabPageDefText + "( " + lv.Items.Count.ToString() +" )";
								m_bFilesWorked |= true;
							}
						}
					} else {
						// обработка НЕ zip (fb2-файлы и другое)
						if( File.Exists( sFilePath) ) {
							File.Delete( sFilePath );
							lv.Items.Remove( lvi );
							tp.Text = sTabPageDefText + "( " + lv.Items.Count.ToString() +" )";
							m_bFilesWorked |= true;
						}
					}
					bw.ReportProgress( ++i ); // отобразим данные в контролах
				}
			}
			#endregion
		}
		#endregion
		
		#region Закрытая Генерация отчетов
		// создание отчета заданного через nModeReport вида для разных вкладок (видов найденных файлов)
		// nModeReport: 0 - html; 1 - fb2;
		private void MakeReport( int nModeReport ) {
			switch( tcResult.SelectedIndex ) {
				case 0:
					// не валидные fb2-файлы
					MakeReport( listViewNotValid, m_FB2NotValidReportEmpty, m_FB2NotValidFilesListReport, nModeReport );
					break;
				case 1:
					// валидные fb2-файлы
					MakeReport( listViewValid, m_FB2ValidReportEmpty, m_FB2ValidFilesListReport, nModeReport );
					break;
				case 2:
					// не fb2-файлы
					MakeReport( listViewNotFB2, m_NotFB2FileReportEmpty, m_NotFB2FilesListReport, nModeReport );
					break;
			}
		}
		
		// создание отчета
		private bool MakeReport( ListView lw, string sReportListEmpty, string sReportTitle, int nModeReport ) {
			switch( nModeReport ) {
				case 0: // Как html-файл
					// сохранение списка не валидных файлов как html-файла
					if( lw.Items.Count < 1 ) {
						MessageBox.Show( sReportListEmpty, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
						return false;
					} else {
						sfdReport.Filter = m_HTMLFilter;
						sfdReport.FileName = "";
						DialogResult result = sfdReport.ShowDialog();
						if (result == DialogResult.OK) {
							tsslblProgress.Text = m_GeneratingReport;
							tsProgressBar.Visible = true;
							ValidatorReports.MakeHTMLReport( lw, sfdReport.FileName, sReportTitle, tsProgressBar, ssProgress  );
							MessageBox.Show( m_ReportSaveOk+sfdReport.FileName, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
							tsProgressBar.Visible = false;
							tsslblProgress.Text = Settings.Settings.GetReady();
						}
					}
					break;
				case 1: // Как fb2-файл
					// сохранение списка не валидных файлов как fb2-файла
					if( lw.Items.Count < 1 ) {
						MessageBox.Show( sReportListEmpty, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
						return false;
					} else {
						sfdReport.Filter = m_FB2Filter;
						sfdReport.FileName = "";
						DialogResult result = sfdReport.ShowDialog();
						if (result == DialogResult.OK) {
							tsslblProgress.Text = m_GeneratingReport;
							tsProgressBar.Visible = true;
							ValidatorReports.MakeFB2Report( lw, sfdReport.FileName, sReportTitle, tsProgressBar, ssProgress );
							MessageBox.Show( m_ReportSaveOk+sfdReport.FileName, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
							tsProgressBar.Visible = false;
							tsslblProgress.Text = Settings.Settings.GetReady();
						}
					}
					break;
			}
			return true;
		}
		#endregion
		#endregion
		
		#region Открытые методы класса
		// задание для кнопок ToolStrip стиля и положения текста и картинки
		public void SetToolButtonsSettings() {
			Settings.ValidatorSettings.SetToolButtonsSettings( tsValidator );
		}
		#endregion
		
		#region Обработчики событий
		void ChBoxScanSubDirClick(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void RbtnFB2LibrusecClick(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void RbtnFB22Click(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		// Ввлидация fb2-файлов в выбранной папке
		void TsbtnValidateClick(object sender, EventArgs e)
		{
			m_sMessTitle		= "SharpFBTools - Валидация";
			m_sScan				= filesWorker.WorkingDirPath( tboxSourceDir.Text.Trim() );
			tboxSourceDir.Text	= m_sScan;
			
			// проверки задания папки сканирования
			if( m_sScan.Length == 0 ) {
				MessageBox.Show( "Выберите папку для сканирования!",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			DirectoryInfo diFolder = new DirectoryInfo( m_sScan );
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка для сканирования не найдена: " + m_sScan,
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}

			// инициализация контролов
			Init();
			SetValidingStartEnabled( false );
			
			m_dtStart = DateTime.Now;
			m_bScanSubDirs = chBoxScanSubDir.Checked;
			tsslblProgress.Text = "Создание списка файлов:";
			
			// Запуск процесса DoWork от Бекграунд Воркера
			if( m_bwv.IsBusy != true ) {
				// если не занят то запустить процесс
				m_bwv.RunWorkerAsync();
			}
		}

		// Остановка Валидации
		void TsbtnValidateStopClick(object sender, EventArgs e)
		{
			if( m_bwv.WorkerSupportsCancellation == true ) {
				m_bwv.CancelAsync();
			}
		}
		
		// копирование файлов в зависимости от выбранной вкладки
		void TsbtnCopyFilesToClick(object sender, EventArgs e)
		{
			m_sFileWorkerMode = "Copy";

			string sMessTitle = "SharpFBTools - Копирование помеченных файлов";
			string sSource 		= filesWorker.WorkingDirPath( tboxSourceDir.Text.Trim() );
			tboxSourceDir.Text	= sSource;
			
			string sTarget, sType;
			sTarget = sType = "";
			ListView lv = GetCurrentListWiew();
			switch( tcResult.SelectedIndex ) {
				case 0: // не валидные fb2-файлы
					sTarget = filesWorker.WorkingDirPath( tboxFB2NotValidDirCopyTo.Text.Trim() );
					tboxFB2NotValidDirCopyTo.Text = sTarget;
					sType	= "отмеченных не валидных fb2-файлов";
					break;
				case 1: // валидные fb2-файлы
					sTarget = filesWorker.WorkingDirPath( tboxFB2ValidDirCopyTo.Text.Trim() );
					tboxFB2ValidDirCopyTo.Text = sTarget;
					sType	= "отмеченных валидных fb2-файлов";
					break;
				case 2: // не fb2-файлы
					sTarget = filesWorker.WorkingDirPath( tboxNotFB2DirCopyTo.Text.Trim() );
					tboxNotFB2DirCopyTo.Text = sTarget;
					sType	= "отмеченных не fb2-файлов";
					break;
			}

			// проверки корректности путей к папкам
			if( lv.Items.Count == 0 ) {
				MessageBox.Show( "Список "+sType+" пуст!\nРабота прекращена.",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			if( lv.CheckedItems.Count == 0 ) {
				MessageBox.Show( "Нет "+sType+"!\nРабота прекращена.",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			if( sTarget.Length == 0 ) {
				MessageBox.Show( "Не указана папка-приемник файлов!\nРабота прекращена.",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			if( sSource == sTarget ) {
				MessageBox.Show( "Папка-приемник файлов совпадает с папкой сканирования!\nРабота прекращена.",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			// проверка папки-приемника и создание ее, если нужно
			if( !filesWorker.CreateDirIfNeed( sTarget, sMessTitle ) ) {
				return;
			}
			
			int nCount = lv.CheckedItems.Count;
			string sMess = "Вы действительно хотите скопировать "+nCount.ToString()+" "+sType+" в папку \""+sTarget+"\"?";
			MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			DialogResult result;
			result = MessageBox.Show( sMess, sMessTitle, buttons, MessageBoxIcon.Question );
			if(result == DialogResult.No) {
				tsslblProgress.Text = Settings.Settings.GetReady();
				SetFilesWorkerStartEnabled( true );
				return;
			}
			
			// инициализация контролов
			tsProgressBar.Maximum 	= lv.CheckedItems.Count;
			tsProgressBar.Value		= 0;
			SetFilesWorkerStartEnabled( false );
			
			// Запуск процесса DoWork от Бекграунд Воркера
			if( m_bwcmd.IsBusy != true ) {
				// если не занят то запустить процесс
				m_bwcmd.RunWorkerAsync();
			}
		}
		
		// перемещение файлов в зависимости от выбранной вкладки
		void TsbtnMoveFilesToClick(object sender, EventArgs e)
		{
			m_sFileWorkerMode = "Move";
			
			string sMessTitle = "SharpFBTools - Перемещение помеченных файлов";
			string sSource 		= filesWorker.WorkingDirPath( tboxSourceDir.Text.Trim() );
			tboxSourceDir.Text	= sSource;
			string sTarget, sType;
			sTarget = sType = "";
			ListView lv = GetCurrentListWiew();
			switch( tcResult.SelectedIndex ) {
				case 0: // не валидные fb2-файлы
					sTarget = filesWorker.WorkingDirPath( tboxFB2NotValidDirMoveTo.Text.Trim() );
					tboxFB2NotValidDirMoveTo.Text = sTarget;
					sType	= "отмеченных не валидных fb2-файлов";
					break;
				case 1: // валидные fb2-файлы
					sTarget = filesWorker.WorkingDirPath( tboxFB2ValidDirMoveTo.Text.Trim() );
					tboxFB2ValidDirMoveTo.Text = sTarget;
					sType	= "отмеченных валидных fb2-файлов";
					break;
				case 2: // не fb2-файлы
					sTarget = filesWorker.WorkingDirPath( tboxNotFB2DirMoveTo.Text.Trim() );
					tboxNotFB2DirMoveTo.Text = sTarget;
					sType	= "отмеченных не fb2-файлов";
					break;
			}

			// проверки корректности путей к папкам
			if( lv.Items.Count == 0 ) {
				MessageBox.Show( "Список "+sType+" пуст!\nРабота прекращена.",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			if( lv.CheckedItems.Count == 0 ) {
				MessageBox.Show( "Нет "+sType+"!\nРабота прекращена.",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			if( sTarget.Length == 0 ) {
				MessageBox.Show( "Не указана папка-приемник файлов!\nРабота прекращена.",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			if( sSource == sTarget ) {
				MessageBox.Show( "Папка-приемник файлов совпадает с папкой сканирования!\nРабота прекращена.",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			// проверка папки-приемника и создание ее, если нужно
			if( !filesWorker.CreateDirIfNeed( sTarget, sMessTitle ) ) {
				return;
			}
			
			int nCount = lv.CheckedItems.Count;
			string sMess = "Вы действительно хотите переместить "+nCount.ToString()+" "+sType+" в папку \""+sTarget+"\"?";
			MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			DialogResult result;
			result = MessageBox.Show( sMess, sMessTitle, buttons, MessageBoxIcon.Question );
			if( result == DialogResult.No ) {
				tsslblProgress.Text = Settings.Settings.GetReady();
				SetFilesWorkerStartEnabled( true );
				return;
			}
			
			// инициализация контролов
			tsProgressBar.Maximum 	= lv.CheckedItems.Count;
			tsProgressBar.Value		= 0;
			SetFilesWorkerStartEnabled( false );
			
			// Запуск процесса DoWork от Бекграунд Воркера
			if( m_bwcmd.IsBusy != true ) {
				// если не занят то запустить процесс
				m_bwcmd.RunWorkerAsync();
			}
		}
		
		// удаление файлов в зависимости от выбранной вкладки
		void TsbtnDeleteFilesClick(object sender, EventArgs e)
		{
			m_sFileWorkerMode = "Delete";
			
			string sMessTitle = "SharpFBTools - Удаление помеченных файлов";
			string sType = "";
			ListView lv = GetCurrentListWiew();
			switch( tcResult.SelectedIndex ) {
				case 0: // не валидные fb2-файлы
					sType	= "отмеченных не валидных fb2-файлов";
					break;
				case 1: // валидные fb2-файлы
					sType	= "отмеченных валидных fb2-файлов";
					break;
				case 2: // не fb2-файлы
					sType	= "отмеченных не fb2-файлов";
					break;
			}
			int nCount = lv.Items.Count;
			if( nCount == 0) {
				MessageBox.Show( "Список "+sType+" пуст!\nРабота прекращена.", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			if( lv.CheckedItems.Count == 0 ) {
				MessageBox.Show( "Нет "+sType+"!\nРабота прекращена.", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			
			nCount = lv.CheckedItems.Count;
			string sMess = "Вы действительно хотите удалить "+nCount.ToString()+" "+sType+"?";
			MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			DialogResult result;
			result = MessageBox.Show( sMess, sMessTitle, buttons, MessageBoxIcon.Question );
			if( result == DialogResult.No ) {
				return;
			}
			
			// инициализация контролов
			tsProgressBar.Maximum 	= lv.CheckedItems.Count;
			tsProgressBar.Value		= 0;
			SetFilesWorkerStartEnabled( false );
			
			// Запуск процесса DoWork от Бекграунд Воркера
			if( m_bwcmd.IsBusy != true ) {
				// если не занят то запустить процесс
				m_bwcmd.RunWorkerAsync();
			}
		}
		
		// занесение ошибки валидации в бокс
		void ListViewNotValidSelectedIndexChanged(object sender, EventArgs e)
		{
			ListView.SelectedListViewItemCollection si = listViewNotValid.SelectedItems;
			if( si.Count > 0 ) {
				rеboxNotValid.Text = si[0].SubItems[1].Text;
				if( listViewNotValid.Items.Count > 0 && listViewNotValid.SelectedItems.Count != 0 ) {
					// путь к выделенному файлу
					string s = si[0].SubItems[0].Text.Split('/')[0];
					// отределяем его расширение
					string sExt = Path.GetExtension( s ).ToLower();
					if( sExt == ".fb2" ) {
						listViewNotValid.ContextMenuStrip = cmsFB2;
					} else {
						listViewNotValid.ContextMenuStrip = cmsArchive;
					}
				} else {
					rеboxNotValid.Clear();
				}
			}
		}
		
		void ListViewValidSelectedIndexChanged(object sender, EventArgs e)
		{
			ListView.SelectedListViewItemCollection si = listViewValid.SelectedItems;
			if( listViewValid.Items.Count > 0 && listViewValid.SelectedItems.Count != 0 ) {
				// путь к выделенному файлу
				string s = si[0].SubItems[0].Text.Split('/')[0];
				// отределяем его расширение
				string sExt = Path.GetExtension( s ).ToLower();
				if( sExt == ".fb2" ) {
					listViewValid.ContextMenuStrip = cmsFB2;
				} else {
					listViewValid.ContextMenuStrip = cmsArchive;
				}
			}
		}
		
		// задание папки для копирования невалидных fb2-файлов
		void BtnFB2NotValidCopyToClick(object sender, EventArgs e)
		{
			filesWorker.OpenDirDlg( tboxFB2NotValidDirCopyTo, fbdDir, "Укажите папку для не валидных fb2-файлов" );
		}
		
		// задание папки для перемещения невалидных fb2-файлов
		void BtnFB2NotValidMoveToClick(object sender, EventArgs e)
		{
			filesWorker.OpenDirDlg( tboxFB2NotValidDirMoveTo, fbdDir, "Укажите папку для не валидных fb2-файлов" );
		}
		
		// задание папки для валидных fb2-файлов
		void BtnFB2ValidCopyToClick(object sender, EventArgs e)
		{
			filesWorker.OpenDirDlg( tboxFB2ValidDirCopyTo, fbdDir, "Укажите папку для валидных fb2-файлов" );
		}
		
		// задание папки для перемещения валидных fb2-файлов
		void BtnFB2ValidMoveToClick(object sender, EventArgs e)
		{
			filesWorker.OpenDirDlg( tboxFB2ValidDirMoveTo, fbdDir, "Укажите папку для валидных fb2-файлов" );
		}
		
		// задание папки для не fb2-файлов
		void BtnNotFB2CopyToClick(object sender, EventArgs e)
		{
			filesWorker.OpenDirDlg( tboxNotFB2DirCopyTo, fbdDir, "Укажите папку для не fb2-файлов" );
		}
		
		// задание папки для перемещения не fb2-файлов
		void BtnNotFB2MoveToClick(object sender, EventArgs e)
		{
			filesWorker.OpenDirDlg( tboxNotFB2DirMoveTo, fbdDir, "Укажите папку для не fb2-файлов" );
		}
		
		// задание папки с fb2-файлами для сканирования
		void ButtonOpenDirClick(object sender, EventArgs e)
		{
			filesWorker.OpenDirDlg( tboxSourceDir, fbdDir, "Укажите папку для проверки fb2-файлов" );
		}
		
		// отчет в виде html-файла
		void TsmiReportAsHTMLClick(object sender, EventArgs e)
		{
			MakeReport( 0 );
		}
		
		// отчет в виде fb2-файла
		void TsmiReportAsFB2Click(object sender, EventArgs e)
		{
			MakeReport( 1 );
		}

		// редактировать выделенный файл в текстовом редакторе
		void TsmiEditInTextEditorClick(object sender, EventArgs e)
		{
			// читаем путь к текстовому редактору из настроек
			string sTFB2Path = Settings.Settings.ReadTextFB2EPath();
			string sTitle = "SharpFBTools - Открытие файла в текстовом редакторе";
			if( !File.Exists( sTFB2Path ) ) {
				MessageBox.Show( "Не могу найти текстовый редактор \""+sTFB2Path+"\"!\nПроверьте, правильно ли задан путь в Настройках.", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 && l.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = l.SelectedItems;
				string sFilePath = si[0].SubItems[0].Text.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: "+sFilePath+"\" не найден!", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				filesWorker.StartAsyncFile( sTFB2Path, sFilePath );
			}
		}
		
		// редактировать выделенный файл в fb2-редакторе
		void TsmiEditInFB2EditorClick(object sender, EventArgs e)
		{
			// читаем путь к FBE из настроек
			string sFBEPath = Settings.Settings.ReadFBEPath();
			string sTitle = "SharpFBTools - Открытие файла в fb2-редакторе";
			if( !File.Exists( sFBEPath ) ) {
				MessageBox.Show( "Не могу найти fb2-редактор \""+sFBEPath+"\"!\nПроверьте, правильно ли задан путь в Настройках.", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 && l.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = l.SelectedItems;
				string sFilePath = si[0].SubItems[0].Text.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: "+sFilePath+"\" не найден!", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				filesWorker.StartAsyncFile( sFBEPath, sFilePath );
			}
		}
		
		// запустить файл в fb2-читалке (Просмотр)
		void TsmiVienInReaderClick(object sender, EventArgs e)
		{
			// читаем путь к читалке из настроек
			string sFBReaderPath = Settings.Settings.ReadFBReaderPath();
			string sTitle = "SharpFBTools - Открытие папки для файла";
			if( !File.Exists( sFBReaderPath ) ) {
				MessageBox.Show( "Не могу найти fb2 Читалку \""+sFBReaderPath+"\"!\nПроверьте, правильно ли задан путь в Настройках.", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 && l.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = l.SelectedItems;
				string sFilePath = si[0].SubItems[0].Text.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: "+sFilePath+"\" не найден!", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				filesWorker.StartAsyncFile( sFBReaderPath, sFilePath );
			}
		}
		
		// Открыть папку для выделенного файла
		void TsmiOpenFileDirClick(object sender, EventArgs e)
		{
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 && l.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = l.SelectedItems;
				FileInfo fi = new FileInfo( si[0].SubItems[0].Text.Split('/')[0] );
				string sDir = fi.Directory.ToString();
				if( !Directory.Exists( sDir ) ) {
					MessageBox.Show( "Папка: "+sDir+"\" не найдена!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				filesWorker.ShowAsyncDir( sDir );
			}
		}
		
		// Запустить выделенный файл в архиваторе
		void TsmiOpenFileInArchivatorClick(object sender, EventArgs e)
		{
			// читаем путь к архиватору из настроек
			string sArchPath = tboxArchivatorPath.Text;
			string sTitle = "SharpFBTools - Запуск файла в Архиваторе";
			if( !File.Exists( sArchPath ) ) {
				MessageBox.Show( "Не могу найти Архиватор \""+sArchPath+"\"!\nПроверьте, правильно ли задан путь в Настройках.", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 && l.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = l.SelectedItems;
				string sFilePath = si[0].SubItems[0].Text.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: "+sFilePath+"\" не найден!", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				filesWorker.StartAsyncFile( sArchPath, sFilePath );
			}
		}
		
		// Повторная Проверка выбранного fb2-файла или архива (Валидация)
		void TsmiFileReValidateClick(object sender, EventArgs e)
		{
			#region Код
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 && l.SelectedItems.Count != 0 ) {
				DateTime dtStart = DateTime.Now;
				ListView.SelectedListViewItemCollection si = l.SelectedItems;
				string sSelectedItemText = si[0].SubItems[0].Text;
				bool isZip = sSelectedItemText.IndexOf('/') != -1 ? true : false;
				string sFilePath = sSelectedItemText.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: "+sFilePath+"\" не найден!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				
				MessageBoxIcon mbi = MessageBoxIcon.Information;
				string sMsg = "";
				string sErrorMsg = "Сообщение об ошибке:";
				string sOkMsg = "Ошибок нет - Файл ВАЛИДЕН";
				string sMoveNotValToVal = "Путь к этому файлу перенесен из Списка не валидных fb2-файлов в Список валидных.";
				string sMoveValToNotVal = "Путь к этому файлу перенесен из Списка валидных fb2-файлов в Список не валидных.";
				FB2Validator fv2Validator = new FB2Validator();
				
				if( !isZip ) {
					// для несжатого fb2-файла
					sMsg = rbtnFB2Librusec.Checked ? fv2Validator.ValidatingFB2LibrusecFile( sFilePath ) : fv2Validator.ValidatingFB22File( sFilePath );
					if ( sMsg == "" ) {
						// файл валидный
						mbi = MessageBoxIcon.Information;
						if( l.Name == "listViewNotValid" ) {
							sErrorMsg = sOkMsg + ":";
							sMsg = sMoveNotValToVal;
							listViewNotValid.Items[ l.SelectedItems[0].Index ].Remove();
							rеboxNotValid.Text = "";
							tpNotValid.Text = m_sNotValid + "( " + listViewNotValid.Items.Count.ToString() + " ) ";
							ListViewItem item = new ListViewItem( sSelectedItemText, 0 );
							item.ForeColor = m_FB2ValidFontColor;
							FileInfo fi = new FileInfo( sSelectedItemText );
							item.SubItems.Add( filesWorker.FormatFileLength( fi.Length ) );
							listViewValid.Items.AddRange( new ListViewItem[]{ item } );
							tpValid.Text = m_sValid + "( " + listViewValid.Items.Count.ToString() + " ) ";
						} else {
							sErrorMsg = sOkMsg;
						}
					} else {
						// файл не валидный
						mbi = MessageBoxIcon.Error;
						if( l.Name == "listViewNotValid" ) {
							l.Items[ l.SelectedItems[0].Index ].SubItems[1].Text = sMsg;
							rеboxNotValid.Text = sMsg;
						} else if( l.Name == "listViewValid" ) {
							// валидный файл был как-то "испорчен"
							listViewValid.Items[ l.SelectedItems[0].Index ].Remove();
							tpValid.Text = m_sValid + "( " + listViewValid.Items.Count.ToString() + " ) ";
							ListViewItem item = new ListViewItem( sSelectedItemText, 0 );
							item.ForeColor = m_FB2NotValidFontColor;
							item.SubItems.Add( sMsg );
							FileInfo fi = new FileInfo( sSelectedItemText );
							item.SubItems.Add( filesWorker.FormatFileLength( fi.Length ) );
							listViewNotValid.Items.AddRange( new ListViewItem[]{ item } );
							tpNotValid.Text = m_sNotValid + "( " + listViewNotValid.Items.Count.ToString() + " ) ";
							sMsg += "\n\n" + sMoveValToNotVal;
						}
					}
				} else {
					// zip архив
					filesWorker.RemoveDir( m_TempDir ); // очистка временной папки
					m_sharpZipLib.UnZipFiles(sFilePath, m_TempDir, 0, false, null, 4096);
					string [] files = Directory.GetFiles( m_TempDir );
					if( files.Length > 0 ) {
						// ищем проверяемый файл среди всех распакованных во временную папку
						string sUnzipedFilePath = m_TempDir + "\\" + sSelectedItemText.Split('/')[1];
						sMsg = rbtnFB2Librusec.Checked ? fv2Validator.ValidatingFB2LibrusecFile( sUnzipedFilePath ) : fv2Validator.ValidatingFB22File( sUnzipedFilePath );
						if ( sMsg == "" ) {
							mbi = MessageBoxIcon.Information;
							//  Запакованный fb2-файл валидный
							if( l.Name == "listViewNotValid" ) {
								sErrorMsg = sOkMsg + ":";
								sMsg = sMoveNotValToVal;
								listViewNotValid.Items[ l.SelectedItems[0].Index ].Remove();
								rеboxNotValid.Text = "";
								tpNotValid.Text = m_sNotValid + "( " + listViewNotValid.Items.Count.ToString() + " ) ";
								string sFB2FileName = sSelectedItemText.Split('/')[1];
								ListViewItem item = new ListViewItem( sSelectedItemText , 0 );
								item.ForeColor = m_ZipFB2ValidFontColor;
								FileInfo fi = new FileInfo( sFilePath );
								string s = filesWorker.FormatFileLength( fi.Length );
								fi = new FileInfo( m_TempDir+"\\"+sFB2FileName );
								s += " / "+filesWorker.FormatFileLength( fi.Length );
								item.SubItems.Add( s );
								listViewValid.Items.AddRange( new ListViewItem[]{ item } );
								tpValid.Text = m_sValid + "( " + listViewValid.Items.Count.ToString() + " ) ";
							} else {
								sErrorMsg = sOkMsg;
							}
						} else {
							mbi = MessageBoxIcon.Error;
							// Запакованный fb2-файл невалиден
							if( l.Name == "listViewNotValid" ) {
								l.Items[ l.SelectedItems[0].Index ].SubItems[1].Text = sMsg;
								rеboxNotValid.Text = sMsg;
							} else if( l.Name == "listViewValid" ) {
								// валидный файл в архиве был как-то "испорчен"
								listViewValid.Items[ l.SelectedItems[0].Index ].Remove();
								tpValid.Text = m_sValid + "( " + listViewValid.Items.Count.ToString() + " ) ";
								string sFB2FileName = sSelectedItemText.Split('/')[1];
								ListViewItem item = new ListViewItem( sSelectedItemText , 0 );
								item.ForeColor = m_ZipFB2NotValidFontColor;
								item.SubItems.Add( sMsg );
								FileInfo fi = new FileInfo( sFilePath );
								string s = filesWorker.FormatFileLength( fi.Length );
								fi = new FileInfo( m_TempDir+"\\"+sFB2FileName );
								s += " / "+filesWorker.FormatFileLength( fi.Length );
								item.SubItems.Add( s );
								listViewNotValid.Items.AddRange( new ListViewItem[]{ item } );
								tpNotValid.Text = m_sNotValid + "( " + listViewNotValid.Items.Count.ToString() + " ) ";
								sMsg += "\n\n" + sMoveValToNotVal;
							}
						}
					}
				}
				DateTime dtEnd = DateTime.Now;
				string sTime = dtEnd.Subtract( dtStart ).ToString() + " (час.:мин.:сек.)";
				filesWorker.RemoveDir( m_TempDir ); // очистка временной папки
				MessageBox.Show( "Повторная проверка выделенного файла на соответствие FictionBook.xsd схеме завершена.\nЗатрачено времени: "+sTime+"\n\nФайл: \""+sSelectedItemText+"\"\n\n"+sErrorMsg+"\n"+sMsg, "SharpFBTools - "+sErrorMsg, MessageBoxButtons.OK, mbi );
			}
			#endregion
		}
		
		// удаление выделенного файла с диска
		void TsmiDeleteFileFromDiskClick(object sender, EventArgs e)
		{
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 && l.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = l.SelectedItems;
				string sFilePath = si[0].SubItems[0].Text.Split('/')[0];
				const string sTitle = "SharpFBTools - Удаление файла с диска";
				if( !File.Exists( sFilePath ) ) {
					if( MessageBox.Show( "Файл: \""+sFilePath+"\" не найден!\nУдалить путь к этому файлу из списка?",
					                    sTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes ) {
						l.Items[ l.SelectedItems[0].Index ].Remove();
					}
				} else {
					if( MessageBox.Show( "Вы действительно хотите удалить файл: \""+sFilePath+"\" с диска?", sTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes ) {
						File.Delete( sFilePath );
						if (si[0].SubItems[0].Text.IndexOf('/') != -1) {
							// удаление zip архивов
							ListViewItem item = null;
							l.BeginUpdate();
							foreach (ListViewItem lvi in l.Items) {
								item = l.FindItemWithText(sFilePath);
								if (item != null)
									item.Remove();
							}
							l.EndUpdate();
						} else {
							// удаление НЕ zip (fb2 файлы и другое)
							l.Items[ l.SelectedItems[0].Index ].Remove();
						}
					}
				}
				switch( tcResult.SelectedIndex ) {
					case 0:
						// не валидные fb2-файлы
						tpNotValid.Text = m_sNotValid + "( " + l.Items.Count.ToString() + " ) ";
						break;
					case 1:
						// валидные fb2-файлы
						tpValid.Text = m_sValid + "( " + l.Items.Count.ToString() + " ) ";
						break;
					case 2:
						// не fb2-файлы
						tpNotFB2Files.Text = m_sNotFB2Files + "( " + l.Items.Count.ToString() + " ) ";
						break;
				}
			}
		}
		
		// выбор варианта работы по двойному щелчку на списках
		void ListViewNotValidDoubleClick(object sender, EventArgs e)
		{
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 && l.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = l.SelectedItems;
				string sSelectedItemText = si[0].SubItems[0].Text;
				string sFilePath = sSelectedItemText.Split('/')[0];
				string sExt = Path.GetExtension( sFilePath ).ToLower();
				if( sExt == ".fb2" ) {
					switch ( cboxValidatorForFB2.SelectedIndex ) {
						case 0: // Повторная Валидация
							TsmiFileReValidateClick( sender, e );
							break;
						case 1: // Править в текстовом редакторе
							TsmiEditInTextEditorClick( sender, e );
							break;
						case 2: // Править в fb2-редакторе
							TsmiEditInFB2EditorClick( sender, e );
							break;
						case 3: // Просмотр в Читалке
							TsmiVienInReaderClick( sender, e );
							break;
						case 4: // Открыть папку файла
							TsmiOpenFileDirClick( sender, e );
							break;
					}
				} else if( sExt == ".zip" || sExt == ".fbz" ) {
					switch ( cboxValidatorForZip.SelectedIndex ) {
						case 0: // Повторная Валидация
							TsmiFileReValidateClick( sender, e );
							break;
						case 1: // Запустить в Архиваторе
							TsmiOpenFileInArchivatorClick( sender, e );
							break;
						case 2: // Открыть папку файла
							TsmiOpenFileDirClick( sender, e );
							break;
					}
				}
			}
		}
		
		// выбор варианта работы по нажатию Enter на списках
		void ListViewNotValidKeyPress(object sender, KeyPressEventArgs e)
		{
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 && l.SelectedItems.Count != 0 ) {
				if ( e.KeyChar == (char)Keys.Return ) {
					e.Handled = true;
					ListView.SelectedListViewItemCollection si = l.SelectedItems;
					string sSelectedItemText = si[0].SubItems[0].Text;
					string sFilePath = sSelectedItemText.Split('/')[0];
					string sExt = Path.GetExtension( sFilePath ).ToLower();
					if( sExt == ".fb2" ) {
						switch ( cboxValidatorForFB2PE.SelectedIndex ) {
							case 0: // Повторная Валидация
								TsmiFileReValidateClick( sender, e );
								break;
							case 1: // Править в текстовом редакторе
								TsmiEditInTextEditorClick( sender, e );
								break;
							case 2: // Править в fb2-редакторе
								TsmiEditInFB2EditorClick( sender, e );
								break;
							case 3: // Просмотр в Читалке
								TsmiVienInReaderClick( sender, e );
								break;
							case 4: // Открыть папку файла
								TsmiOpenFileDirClick( sender, e );
								break;
						}
					} else if( sExt == ".zip" || sExt == ".fbz" ) {
						switch ( cboxValidatorForZipPE.SelectedIndex ) {
							case 0: // Повторная Валидация
								TsmiFileReValidateClick( sender, e );
								break;
							case 1: // Запустить в Архиваторе
								TsmiOpenFileInArchivatorClick( sender, e );
								break;
							case 2: // Открыть папку файла
								TsmiOpenFileDirClick( sender, e );
								break;
						}
					}
				}
			}
		}

		// сохранение списка Не валидных файлов
		void TsmiMakeNotValidFileListClick(object sender, EventArgs e)
		{
			ValidatorReports.SaveFilesList( listViewNotValid, sfdReport, m_TXTFilter,
			                               ssProgress,  tsslblProgress, tsProgressBar, m_FB2NotValidFilesListReport,
			                               "Нет ни одного Не валидного файла!", "Создание списка Не валидных файлов завершено.", Settings.Settings.GetReady() );
		}
		
		// сохранение списка Валидных файлов
		void TsmiMakeValidFileListClick(object sender, EventArgs e)
		{
			ValidatorReports.SaveFilesList( listViewValid, sfdReport, m_TXTFilter, ssProgress,
			                               tsslblProgress, tsProgressBar, m_FB2ValidFilesListReport,
			                               "Нет ни одного Валидного файла!", "Создание списка Валидных файлов завершено.", Settings.Settings.GetReady() );
		}
		
		void RbtnFB2LibrusecCheckedChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void RbtnFB22CheckedChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void ChBoxViewProgressCheckStateChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void CboxExistFileSelectedIndexChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void CboxValidatorForFB2SelectedIndexChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void CboxValidatorForZipSelectedIndexChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void CboxValidatorForFB2PESelectedIndexChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void CboxValidatorForZipPESelectedIndexChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void TboxArchivatorPathTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void BtnArchivatorPathClick(object sender, EventArgs e)
		{
			// указание пути к Архиватору
			ofDlg.Title = "Укажите путь к Архиватору:";
			ofDlg.FileName = "";
			ofDlg.Filter = "Файлы .exe|*.exe|Все файлы (*.*)|*.*";
			DialogResult result = ofDlg.ShowDialog();
			if (result == DialogResult.OK)
				tboxArchivatorPath.Text = ofDlg.FileName;
		}
		
		void TboxSourceDirTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void TboxFB2NotValidDirCopyToTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void TboxFB2NotValidDirMoveToTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void TboxFB2ValidDirCopyToTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void TboxFB2ValidDirMoveToTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void TboxNotFB2DirCopyToTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void TboxNotFB2DirMoveToTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		// Остановка выполнения процесса Копирование / Перемещение / Удаление
		void TsbtnFilesWorkStopClick(object sender, EventArgs e)
		{
			if( m_bwcmd.WorkerSupportsCancellation == true )
				m_bwcmd.CancelAsync();
		}
		
		// запуск Валидации по нажатию Enter на поле ввода папки для сканирования
		void TboxSourceDirKeyPress(object sender, KeyPressEventArgs e)
		{
			if ( e.KeyChar == (char)Keys.Return )
				TsbtnValidateClick( sender, e );
		}
		
		// отметить все FB2 книги
		void TsmiFB2CheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			CheckAll();
			ConnectListsEventHandlers( true );
		}
		
		// снять отметки со всех FB2 книг
		void TsmiFB2UnCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			UnCheckAll();
			ConnectListsEventHandlers( true );
		}
		
		// отметить все Архивы
		void TsmiArchiveCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			CheckAll();
			ConnectListsEventHandlers( true );
		}
		
		// снять отметки со всех Архивов
		void TsmiArchiveUnCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			UnCheckAll();
			ConnectListsEventHandlers( true );
		}
		
		// отметить все не FB2 файлы
		void TsmiNotFB2CheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			CheckAll();
			ConnectListsEventHandlers( true );
		}
		
		// снять отметки со не FB2 файлов
		void TsmiNotFB2UnCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			UnCheckAll();
			ConnectListsEventHandlers( true );
		}
		
		// сохранение списка найденных невалидных файлов
		void BtnSaveListClick(object sender, EventArgs e)
		{
			#region Код
			if( listViewNotValid.Items.Count==0 ) {
				MessageBox.Show( "Список невалидных файлов пуст!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}

			string sPath = "";
			sfdReport.Filter = "SharpFBTools файлы списков (*.xml)|*.xml|Все файлы (*.*)|*.*";
			sfdReport.FileName = "";
			sfdReport.InitialDirectory = Settings.Settings.ProgDir;
			DialogResult result = sfdReport.ShowDialog();
			if( result == DialogResult.OK ) {
				sPath = sfdReport.FileName;
				Environment.CurrentDirectory = Settings.Settings.ProgDir;
				XmlWriter writer = null;
				try {
					XmlWriterSettings data = new XmlWriterSettings();
					data.Indent = true;
					data.IndentChars = ("\t");
					data.OmitXmlDeclaration = true;
					
					writer = XmlWriter.Create( sPath, data );
					writer.WriteStartElement( "Validator" );
					// папка-источник
					writer.WriteStartElement( "ScanDir" );
					writer.WriteAttributeString( "scandir", m_sScan );
					writer.WriteFullEndElement();
					// число стьолбцов и записей
					writer.WriteStartElement( "Count" );
					writer.WriteAttributeString( "ItemsCount", listViewNotValid.Items.Count.ToString() );
					writer.WriteAttributeString( "ColumnsCount", listViewNotValid.Columns.Count.ToString() );
					writer.WriteFullEndElement();
					// данные поиска
					writer.WriteStartElement( "Items" );
					for( int i=0; i!=listViewNotValid.Items.Count; ++i ) {
						ListViewItem item = listViewNotValid.Items[i];
						writer.WriteStartElement( "item"+i.ToString() );
						for( int j=0; j!=listViewNotValid.Columns.Count; ++j ) {
							writer.WriteAttributeString( "c"+j.ToString(), item.SubItems[j].Text );
						}
						writer.WriteFullEndElement();
					}
					writer.WriteEndElement();
					writer.WriteEndElement();
					writer.Flush();
				}  finally  {
					if (writer != null)
						writer.Close();
				}
				MessageBox.Show( "Сохранение списка невалидных файлов завершено!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
			
			#endregion
		}
		
		// загрузка списка невалидных файлов
		void BtnLoadListClick(object sender, EventArgs e)
		{
			#region Код
			sfdLoadList.InitialDirectory = Settings.Settings.ProgDir;
			sfdLoadList.Filter		= "SharpFBTools файлы списков (*.xml)|*.xml|Все файлы (*.*)|*.*";
			sfdLoadList.FileName	= "";
			string sPath = "";
			DialogResult result = sfdLoadList.ShowDialog();
			if( result == DialogResult.OK ) {
				sPath = sfdLoadList.FileName;
				// инициализация контролов
				Init();
				// установка режима поиска
				if( !File.Exists( sPath ) ) {
					MessageBox.Show( "Не найден файл списка Валидатора: \""+sPath+"\"!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return;
				}
				XmlReaderSettings data = new XmlReaderSettings();
				data.IgnoreWhitespace = true;
				using ( XmlReader reader = XmlReader.Create( sPath, data ) ) {
					// отключаем обработчики событий для lvResult (убираем "тормоза")
					ConnectListsEventHandlers( false );
					try {
						// папка-источник
						reader.ReadToFollowing("ScanDir");
						if( reader.HasAttributes ) {
							m_sScan = reader.GetAttribute("scandir");
							tboxSourceDir.Text = m_sScan;
						}
						// число стьолбцов и записей
						int nItemsCount	= 0, nColumnsCount = 0;
						reader.ReadToFollowing("Count");
						if( reader.HasAttributes ) {
							nItemsCount		= Convert.ToInt32( reader.GetAttribute("ItemsCount") );
							nColumnsCount	= Convert.ToInt32( reader.GetAttribute("ColumnsCount") );
						}
						// данные поиска
						ListViewItem lvi = null;
						for( int i=0; i!=nItemsCount; ++i ) {
							reader.ReadToFollowing("item"+i.ToString());
							if( reader.HasAttributes ) {
								lvi = new ListViewItem( reader.GetAttribute("c0") );
								for( int j=1; j!=nColumnsCount; ++j ) {
									lvi.SubItems.Add( reader.GetAttribute("c"+j.ToString()) );
								}
								listViewNotValid.Items.Add( lvi );
							}
						}
						// отобразим число невалидных файлов
						tpNotValid.Text = m_sNotValid + "( " + listViewNotValid.Items.Count.ToString() +" )";
						MessageBox.Show( "Список невалидных файлов загружен.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
					} catch {
						MessageBox.Show( "Поврежден файл списка Валидатора: \""+sPath+"\"!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
					} finally {
						reader.Close();
						ConnectListsEventHandlers( true );
					}
				}
			}
			#endregion
		}
		
		void TcResultSelectedIndexChanged(object sender, EventArgs e)
		{
			tsValidator.Enabled = tcResult.SelectedIndex != 3 ? true : false;
		}
		#endregion

	}
}
