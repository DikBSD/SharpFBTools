/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 16.03.2009
 * Time: 9:03
 * 
 * License: GPL 2.1
 */
namespace SharpFBTools.Controls.Panels
{
	partial class SFBTpFileManager
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SFBTpFileManager));
			this.ssProgress = new System.Windows.Forms.StatusStrip();
			this.tsslblProgress = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.tsFileManager = new System.Windows.Forms.ToolStrip();
			this.tsbtnOpenDir = new System.Windows.Forms.ToolStripButton();
			this.tsSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnSortFilesTo = new System.Windows.Forms.ToolStripButton();
			this.tsSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsddbtnMakeReport = new System.Windows.Forms.ToolStripDropDownButton();
			this.tsmiReportAsHTML = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiReportAsFB2 = new System.Windows.Forms.ToolStripMenuItem();
			this.pSource = new System.Windows.Forms.Panel();
			this.cboxScanSubDir = new System.Windows.Forms.CheckBox();
			this.tboxSourceDir = new System.Windows.Forms.TextBox();
			this.lblDir = new System.Windows.Forms.Label();
			this.fbdScanDir = new System.Windows.Forms.FolderBrowserDialog();
			this.gBoxViewResult = new System.Windows.Forms.GroupBox();
			this.gBoxRenameTemplates = new System.Windows.Forms.GroupBox();
			this.txtBoxTemplatesFromLine = new System.Windows.Forms.TextBox();
			this.cboxTemplatesPrepared = new System.Windows.Forms.ComboBox();
			this.rBtnTemplatesFromLine = new System.Windows.Forms.RadioButton();
			this.rBtnTemplatesPrepared = new System.Windows.Forms.RadioButton();
			this.tcFileManager = new System.Windows.Forms.TabControl();
			this.tpSortAll = new System.Windows.Forms.TabPage();
			this.pSortAllToDir = new System.Windows.Forms.Panel();
			this.lblSortAllTargetDir = new System.Windows.Forms.Label();
			this.btnSortAllToDir = new System.Windows.Forms.Button();
			this.tboxSortAllToDir = new System.Windows.Forms.TextBox();
			this.tsSortSelected = new System.Windows.Forms.TabPage();
			this.pSortSelectedToDir = new System.Windows.Forms.Panel();
			this.lblSortSelectedTargetDir = new System.Windows.Forms.Label();
			this.btnSortSelectedToDir = new System.Windows.Forms.Button();
			this.tboxSortSelectedToDir = new System.Windows.Forms.TextBox();
			this.pProgress = new System.Windows.Forms.Panel();
			this.gBoxTemplatesDescription = new System.Windows.Forms.GroupBox();
			this.richTxtBoxDescTemplates = new System.Windows.Forms.RichTextBox();
			this.ssProgress.SuspendLayout();
			this.tsFileManager.SuspendLayout();
			this.pSource.SuspendLayout();
			this.gBoxRenameTemplates.SuspendLayout();
			this.tcFileManager.SuspendLayout();
			this.tpSortAll.SuspendLayout();
			this.pSortAllToDir.SuspendLayout();
			this.tsSortSelected.SuspendLayout();
			this.pSortSelectedToDir.SuspendLayout();
			this.gBoxTemplatesDescription.SuspendLayout();
			this.SuspendLayout();
			// 
			// ssProgress
			// 
			this.ssProgress.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsslblProgress,
									this.tsProgressBar});
			this.ssProgress.Location = new System.Drawing.Point(0, 538);
			this.ssProgress.Name = "ssProgress";
			this.ssProgress.Size = new System.Drawing.Size(828, 22);
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
			// tsFileManager
			// 
			this.tsFileManager.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tsFileManager.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsbtnOpenDir,
									this.tsSep1,
									this.tsbtnSortFilesTo,
									this.tsSep2,
									this.tsddbtnMakeReport});
			this.tsFileManager.Location = new System.Drawing.Point(0, 0);
			this.tsFileManager.Name = "tsFileManager";
			this.tsFileManager.Size = new System.Drawing.Size(828, 31);
			this.tsFileManager.TabIndex = 19;
			// 
			// tsbtnOpenDir
			// 
			this.tsbtnOpenDir.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnOpenDir.Image")));
			this.tsbtnOpenDir.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnOpenDir.Name = "tsbtnOpenDir";
			this.tsbtnOpenDir.Size = new System.Drawing.Size(114, 28);
			this.tsbtnOpenDir.Text = "Открыть папку";
			this.tsbtnOpenDir.ToolTipText = "Открыть папку с fb2-файлами и (или) архивами...";
			this.tsbtnOpenDir.Click += new System.EventHandler(this.TsbtnOpenDirClick);
			// 
			// tsSep1
			// 
			this.tsSep1.Name = "tsSep1";
			this.tsSep1.Size = new System.Drawing.Size(6, 31);
			// 
			// tsbtnSortFilesTo
			// 
			this.tsbtnSortFilesTo.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSortFilesTo.Image")));
			this.tsbtnSortFilesTo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnSortFilesTo.Name = "tsbtnSortFilesTo";
			this.tsbtnSortFilesTo.Size = new System.Drawing.Size(102, 28);
			this.tsbtnSortFilesTo.Text = "Сортировать";
			this.tsbtnSortFilesTo.ToolTipText = "Сортировать файлы (для выбранной вкладки)";
			// 
			// tsSep2
			// 
			this.tsSep2.Name = "tsSep2";
			this.tsSep2.Size = new System.Drawing.Size(6, 31);
			// 
			// tsddbtnMakeReport
			// 
			this.tsddbtnMakeReport.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsmiReportAsHTML,
									this.tsmiReportAsFB2});
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
			// 
			// tsmiReportAsFB2
			// 
			this.tsmiReportAsFB2.Name = "tsmiReportAsFB2";
			this.tsmiReportAsFB2.Size = new System.Drawing.Size(196, 22);
			this.tsmiReportAsFB2.Text = "Как fb2-файл...";
			// 
			// pSource
			// 
			this.pSource.Controls.Add(this.cboxScanSubDir);
			this.pSource.Controls.Add(this.tboxSourceDir);
			this.pSource.Controls.Add(this.lblDir);
			this.pSource.Dock = System.Windows.Forms.DockStyle.Top;
			this.pSource.Location = new System.Drawing.Point(0, 31);
			this.pSource.Name = "pSource";
			this.pSource.Size = new System.Drawing.Size(828, 34);
			this.pSource.TabIndex = 20;
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
			this.cboxScanSubDir.TabIndex = 8;
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
			this.tboxSourceDir.ReadOnly = true;
			this.tboxSourceDir.Size = new System.Drawing.Size(484, 20);
			this.tboxSourceDir.TabIndex = 7;
			// 
			// lblDir
			// 
			this.lblDir.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.lblDir.Location = new System.Drawing.Point(2, 8);
			this.lblDir.Name = "lblDir";
			this.lblDir.Size = new System.Drawing.Size(162, 19);
			this.lblDir.TabIndex = 6;
			this.lblDir.Text = "Папка для сканирования:";
			// 
			// fbdScanDir
			// 
			this.fbdScanDir.Description = "Укажите папку для сканирования с fb2-файлами и архивами";
			// 
			// gBoxViewResult
			// 
			this.gBoxViewResult.Dock = System.Windows.Forms.DockStyle.Top;
			this.gBoxViewResult.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gBoxViewResult.ForeColor = System.Drawing.Color.Maroon;
			this.gBoxViewResult.Location = new System.Drawing.Point(0, 195);
			this.gBoxViewResult.Name = "gBoxViewResult";
			this.gBoxViewResult.Size = new System.Drawing.Size(828, 50);
			this.gBoxViewResult.TabIndex = 27;
			this.gBoxViewResult.TabStop = false;
			this.gBoxViewResult.Text = " Вид результата ";
			// 
			// gBoxRenameTemplates
			// 
			this.gBoxRenameTemplates.Controls.Add(this.txtBoxTemplatesFromLine);
			this.gBoxRenameTemplates.Controls.Add(this.cboxTemplatesPrepared);
			this.gBoxRenameTemplates.Controls.Add(this.rBtnTemplatesFromLine);
			this.gBoxRenameTemplates.Controls.Add(this.rBtnTemplatesPrepared);
			this.gBoxRenameTemplates.Dock = System.Windows.Forms.DockStyle.Top;
			this.gBoxRenameTemplates.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gBoxRenameTemplates.ForeColor = System.Drawing.Color.Indigo;
			this.gBoxRenameTemplates.Location = new System.Drawing.Point(0, 126);
			this.gBoxRenameTemplates.Name = "gBoxRenameTemplates";
			this.gBoxRenameTemplates.Size = new System.Drawing.Size(828, 69);
			this.gBoxRenameTemplates.TabIndex = 26;
			this.gBoxRenameTemplates.TabStop = false;
			this.gBoxRenameTemplates.Text = " Шаблоны переименовывания ";
			// 
			// txtBoxTemplatesFromLine
			// 
			this.txtBoxTemplatesFromLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxTemplatesFromLine.Enabled = false;
			this.txtBoxTemplatesFromLine.Location = new System.Drawing.Point(143, 42);
			this.txtBoxTemplatesFromLine.Name = "txtBoxTemplatesFromLine";
			this.txtBoxTemplatesFromLine.Size = new System.Drawing.Size(667, 20);
			this.txtBoxTemplatesFromLine.TabIndex = 3;
			// 
			// cboxTemplatesPrepared
			// 
			this.cboxTemplatesPrepared.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.cboxTemplatesPrepared.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxTemplatesPrepared.FormattingEnabled = true;
			this.cboxTemplatesPrepared.Items.AddRange(new object[] {
									"*L*\\*G*\\[*BAL*_*BAF*_*BAM*_][*BAN*]\\[*SN*]\\*BAL*[_(*SN*-*SI*)]_*BT*",
									"[*L*]\\*G*\\[*BAL*_*BAF*_*BAM*_][*BAN*]\\[*SN*]\\*BAL*[_(*SN*-*SI*)]_*BT*",
									"*L*\\*G*\\*BAL*_*BAF*[_*BAM*][_*BAN*]\\[*SN*]\\*BAL*[_(*SN*-*SI*)]_*BT*",
									"[*L*]\\*G*\\*BAL*_*BAF*[_*BAM*][_*BAN*]\\[*SN*]\\*BAL*[_(*SN*-*SI*)]_*BT*",
									"*G*\\[*BAL*_*BAF*_*BAM*_][*BAN*]\\[*SN*]\\*BAL*[_(*SN*-*SI*)]_*BT*",
									"[*G*]\\[*BAL*_*BAF*_*BAM*_][*BAN*]\\[*SN*]\\*BAL*[_(*SN*-*SI*)]_*BT*",
									"*G*\\*BAL*_*BAF*[_*BAM*][_*BAN*]\\[*SN*]\\*BAL*[_(*SN*-*SI*)]_*BT*",
									"[*G*]\\*BAL*_*BAF*[_*BAM*][_*BAN*]\\[*SN*]\\*BAL*[_(*SN*-*SI*)]_*BT*"});
			this.cboxTemplatesPrepared.Location = new System.Drawing.Point(143, 18);
			this.cboxTemplatesPrepared.Name = "cboxTemplatesPrepared";
			this.cboxTemplatesPrepared.Size = new System.Drawing.Size(667, 21);
			this.cboxTemplatesPrepared.TabIndex = 2;
			// 
			// rBtnTemplatesFromLine
			// 
			this.rBtnTemplatesFromLine.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rBtnTemplatesFromLine.Location = new System.Drawing.Point(17, 41);
			this.rBtnTemplatesFromLine.Name = "rBtnTemplatesFromLine";
			this.rBtnTemplatesFromLine.Size = new System.Drawing.Size(126, 18);
			this.rBtnTemplatesFromLine.TabIndex = 1;
			this.rBtnTemplatesFromLine.Text = "Ввести вручную:";
			this.rBtnTemplatesFromLine.UseVisualStyleBackColor = true;
			this.rBtnTemplatesFromLine.CheckedChanged += new System.EventHandler(this.RBtnTemplatesFromLineCheckedChanged);
			// 
			// rBtnTemplatesPrepared
			// 
			this.rBtnTemplatesPrepared.Checked = true;
			this.rBtnTemplatesPrepared.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rBtnTemplatesPrepared.Location = new System.Drawing.Point(17, 19);
			this.rBtnTemplatesPrepared.Name = "rBtnTemplatesPrepared";
			this.rBtnTemplatesPrepared.Size = new System.Drawing.Size(126, 18);
			this.rBtnTemplatesPrepared.TabIndex = 0;
			this.rBtnTemplatesPrepared.TabStop = true;
			this.rBtnTemplatesPrepared.Text = "Выбор готовых:";
			this.rBtnTemplatesPrepared.UseVisualStyleBackColor = true;
			this.rBtnTemplatesPrepared.CheckedChanged += new System.EventHandler(this.RBtnTemplatesPreparedCheckedChanged);
			// 
			// tcFileManager
			// 
			this.tcFileManager.Controls.Add(this.tpSortAll);
			this.tcFileManager.Controls.Add(this.tsSortSelected);
			this.tcFileManager.Dock = System.Windows.Forms.DockStyle.Top;
			this.tcFileManager.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.tcFileManager.Location = new System.Drawing.Point(0, 65);
			this.tcFileManager.Name = "tcFileManager";
			this.tcFileManager.SelectedIndex = 0;
			this.tcFileManager.Size = new System.Drawing.Size(828, 61);
			this.tcFileManager.TabIndex = 25;
			// 
			// tpSortAll
			// 
			this.tpSortAll.Controls.Add(this.pSortAllToDir);
			this.tpSortAll.Location = new System.Drawing.Point(4, 22);
			this.tpSortAll.Name = "tpSortAll";
			this.tpSortAll.Padding = new System.Windows.Forms.Padding(3);
			this.tpSortAll.Size = new System.Drawing.Size(820, 35);
			this.tpSortAll.TabIndex = 0;
			this.tpSortAll.Text = " Полная Сортировка ";
			this.tpSortAll.UseVisualStyleBackColor = true;
			// 
			// pSortAllToDir
			// 
			this.pSortAllToDir.Controls.Add(this.lblSortAllTargetDir);
			this.pSortAllToDir.Controls.Add(this.btnSortAllToDir);
			this.pSortAllToDir.Controls.Add(this.tboxSortAllToDir);
			this.pSortAllToDir.Dock = System.Windows.Forms.DockStyle.Top;
			this.pSortAllToDir.Location = new System.Drawing.Point(3, 3);
			this.pSortAllToDir.Name = "pSortAllToDir";
			this.pSortAllToDir.Size = new System.Drawing.Size(814, 33);
			this.pSortAllToDir.TabIndex = 3;
			// 
			// lblSortAllTargetDir
			// 
			this.lblSortAllTargetDir.AutoSize = true;
			this.lblSortAllTargetDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblSortAllTargetDir.Location = new System.Drawing.Point(3, 9);
			this.lblSortAllTargetDir.Name = "lblSortAllTargetDir";
			this.lblSortAllTargetDir.Size = new System.Drawing.Size(152, 13);
			this.lblSortAllTargetDir.TabIndex = 18;
			this.lblSortAllTargetDir.Text = "Папка-приемник файлов:";
			// 
			// btnSortAllToDir
			// 
			this.btnSortAllToDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSortAllToDir.Image = ((System.Drawing.Image)(resources.GetObject("btnSortAllToDir.Image")));
			this.btnSortAllToDir.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnSortAllToDir.Location = new System.Drawing.Point(766, 3);
			this.btnSortAllToDir.Name = "btnSortAllToDir";
			this.btnSortAllToDir.Size = new System.Drawing.Size(37, 24);
			this.btnSortAllToDir.TabIndex = 7;
			this.btnSortAllToDir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSortAllToDir.UseVisualStyleBackColor = true;
			// 
			// tboxSortAllToDir
			// 
			this.tboxSortAllToDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxSortAllToDir.Location = new System.Drawing.Point(155, 5);
			this.tboxSortAllToDir.Name = "tboxSortAllToDir";
			this.tboxSortAllToDir.ReadOnly = true;
			this.tboxSortAllToDir.Size = new System.Drawing.Size(605, 20);
			this.tboxSortAllToDir.TabIndex = 6;
			// 
			// tsSortSelected
			// 
			this.tsSortSelected.Controls.Add(this.pSortSelectedToDir);
			this.tsSortSelected.Location = new System.Drawing.Point(4, 22);
			this.tsSortSelected.Name = "tsSortSelected";
			this.tsSortSelected.Padding = new System.Windows.Forms.Padding(3);
			this.tsSortSelected.Size = new System.Drawing.Size(820, 70);
			this.tsSortSelected.TabIndex = 1;
			this.tsSortSelected.Text = " Избранная Сортировка ";
			this.tsSortSelected.UseVisualStyleBackColor = true;
			// 
			// pSortSelectedToDir
			// 
			this.pSortSelectedToDir.Controls.Add(this.lblSortSelectedTargetDir);
			this.pSortSelectedToDir.Controls.Add(this.btnSortSelectedToDir);
			this.pSortSelectedToDir.Controls.Add(this.tboxSortSelectedToDir);
			this.pSortSelectedToDir.Dock = System.Windows.Forms.DockStyle.Top;
			this.pSortSelectedToDir.Location = new System.Drawing.Point(3, 3);
			this.pSortSelectedToDir.Name = "pSortSelectedToDir";
			this.pSortSelectedToDir.Size = new System.Drawing.Size(814, 33);
			this.pSortSelectedToDir.TabIndex = 4;
			// 
			// lblSortSelectedTargetDir
			// 
			this.lblSortSelectedTargetDir.AutoSize = true;
			this.lblSortSelectedTargetDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblSortSelectedTargetDir.Location = new System.Drawing.Point(3, 9);
			this.lblSortSelectedTargetDir.Name = "lblSortSelectedTargetDir";
			this.lblSortSelectedTargetDir.Size = new System.Drawing.Size(152, 13);
			this.lblSortSelectedTargetDir.TabIndex = 18;
			this.lblSortSelectedTargetDir.Text = "Папка-приемник файлов:";
			// 
			// btnSortSelectedToDir
			// 
			this.btnSortSelectedToDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSortSelectedToDir.Image = ((System.Drawing.Image)(resources.GetObject("btnSortSelectedToDir.Image")));
			this.btnSortSelectedToDir.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnSortSelectedToDir.Location = new System.Drawing.Point(766, 3);
			this.btnSortSelectedToDir.Name = "btnSortSelectedToDir";
			this.btnSortSelectedToDir.Size = new System.Drawing.Size(37, 24);
			this.btnSortSelectedToDir.TabIndex = 7;
			this.btnSortSelectedToDir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSortSelectedToDir.UseVisualStyleBackColor = true;
			// 
			// tboxSortSelectedToDir
			// 
			this.tboxSortSelectedToDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxSortSelectedToDir.Location = new System.Drawing.Point(155, 5);
			this.tboxSortSelectedToDir.Name = "tboxSortSelectedToDir";
			this.tboxSortSelectedToDir.ReadOnly = true;
			this.tboxSortSelectedToDir.Size = new System.Drawing.Size(605, 20);
			this.tboxSortSelectedToDir.TabIndex = 6;
			// 
			// pProgress
			// 
			this.pProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pProgress.Location = new System.Drawing.Point(0, 475);
			this.pProgress.Name = "pProgress";
			this.pProgress.Size = new System.Drawing.Size(828, 63);
			this.pProgress.TabIndex = 29;
			// 
			// gBoxTemplatesDescription
			// 
			this.gBoxTemplatesDescription.Controls.Add(this.richTxtBoxDescTemplates);
			this.gBoxTemplatesDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gBoxTemplatesDescription.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gBoxTemplatesDescription.ForeColor = System.Drawing.Color.Maroon;
			this.gBoxTemplatesDescription.Location = new System.Drawing.Point(0, 245);
			this.gBoxTemplatesDescription.Name = "gBoxTemplatesDescription";
			this.gBoxTemplatesDescription.Size = new System.Drawing.Size(828, 230);
			this.gBoxTemplatesDescription.TabIndex = 30;
			this.gBoxTemplatesDescription.TabStop = false;
			this.gBoxTemplatesDescription.Text = " Описание шаблонов ";
			// 
			// richTxtBoxDescTemplates
			// 
			this.richTxtBoxDescTemplates.BackColor = System.Drawing.SystemColors.Window;
			this.richTxtBoxDescTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTxtBoxDescTemplates.Font = new System.Drawing.Font("Tahoma", 8F);
			this.richTxtBoxDescTemplates.Location = new System.Drawing.Point(3, 16);
			this.richTxtBoxDescTemplates.Name = "richTxtBoxDescTemplates";
			this.richTxtBoxDescTemplates.ReadOnly = true;
			this.richTxtBoxDescTemplates.Size = new System.Drawing.Size(822, 211);
			this.richTxtBoxDescTemplates.TabIndex = 0;
			this.richTxtBoxDescTemplates.Text = "";
			// 
			// SFBTpFileManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gBoxTemplatesDescription);
			this.Controls.Add(this.pProgress);
			this.Controls.Add(this.gBoxViewResult);
			this.Controls.Add(this.gBoxRenameTemplates);
			this.Controls.Add(this.tcFileManager);
			this.Controls.Add(this.pSource);
			this.Controls.Add(this.tsFileManager);
			this.Controls.Add(this.ssProgress);
			this.Name = "SFBTpFileManager";
			this.Size = new System.Drawing.Size(828, 560);
			this.ssProgress.ResumeLayout(false);
			this.ssProgress.PerformLayout();
			this.tsFileManager.ResumeLayout(false);
			this.tsFileManager.PerformLayout();
			this.pSource.ResumeLayout(false);
			this.pSource.PerformLayout();
			this.gBoxRenameTemplates.ResumeLayout(false);
			this.gBoxRenameTemplates.PerformLayout();
			this.tcFileManager.ResumeLayout(false);
			this.tpSortAll.ResumeLayout(false);
			this.pSortAllToDir.ResumeLayout(false);
			this.pSortAllToDir.PerformLayout();
			this.tsSortSelected.ResumeLayout(false);
			this.pSortSelectedToDir.ResumeLayout(false);
			this.pSortSelectedToDir.PerformLayout();
			this.gBoxTemplatesDescription.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Panel pProgress;
		private System.Windows.Forms.Panel pSource;
		private System.Windows.Forms.RichTextBox richTxtBoxDescTemplates;
		private System.Windows.Forms.GroupBox gBoxTemplatesDescription;
		private System.Windows.Forms.GroupBox gBoxViewResult;
		private System.Windows.Forms.TextBox txtBoxTemplatesFromLine;
		private System.Windows.Forms.ComboBox cboxTemplatesPrepared;
		private System.Windows.Forms.RadioButton rBtnTemplatesPrepared;
		private System.Windows.Forms.RadioButton rBtnTemplatesFromLine;
		private System.Windows.Forms.GroupBox gBoxRenameTemplates;
		private System.Windows.Forms.TextBox tboxSortSelectedToDir;
		private System.Windows.Forms.Button btnSortSelectedToDir;
		private System.Windows.Forms.Label lblSortSelectedTargetDir;
		private System.Windows.Forms.Panel pSortSelectedToDir;
		private System.Windows.Forms.TextBox tboxSortAllToDir;
		private System.Windows.Forms.Button btnSortAllToDir;
		private System.Windows.Forms.Label lblSortAllTargetDir;
		private System.Windows.Forms.Panel pSortAllToDir;
		private System.Windows.Forms.FolderBrowserDialog fbdScanDir;
		private System.Windows.Forms.TabControl tcFileManager;
		private System.Windows.Forms.Label lblDir;
		private System.Windows.Forms.TextBox tboxSourceDir;
		private System.Windows.Forms.CheckBox cboxScanSubDir;
		private System.Windows.Forms.TabPage tsSortSelected;
		private System.Windows.Forms.TabPage tpSortAll;
		private System.Windows.Forms.ToolStrip tsFileManager;
		private System.Windows.Forms.ToolStripButton tsbtnSortFilesTo;
		private System.Windows.Forms.ToolStripMenuItem tsmiReportAsFB2;
		private System.Windows.Forms.ToolStripMenuItem tsmiReportAsHTML;
		private System.Windows.Forms.ToolStripDropDownButton tsddbtnMakeReport;
		private System.Windows.Forms.ToolStripSeparator tsSep2;
		private System.Windows.Forms.ToolStripSeparator tsSep1;
		private System.Windows.Forms.ToolStripButton tsbtnOpenDir;
		private System.Windows.Forms.ToolStripProgressBar tsProgressBar;
		private System.Windows.Forms.ToolStripStatusLabel tsslblProgress;
		private System.Windows.Forms.StatusStrip ssProgress;
	}
}
