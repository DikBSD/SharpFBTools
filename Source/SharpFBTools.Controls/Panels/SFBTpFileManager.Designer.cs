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
			System.Windows.Forms.ListViewGroup listViewGroup7 = new System.Windows.Forms.ListViewGroup("Основные", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup8 = new System.Windows.Forms.ListViewGroup("Что делать с одинаковыми файлами", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup9 = new System.Windows.Forms.ListViewGroup("Папки с проблемными fb2-файлами", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewItem listViewItem51 = new System.Windows.Forms.ListViewItem(new string[] {
									"Регистр имени файла (папок)",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem52 = new System.Windows.Forms.ListViewItem(new string[] {
									"Раскладка файлов по Авторам",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem53 = new System.Windows.Forms.ListViewItem(new string[] {
									"Раскладка файлов по Жанрам",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem54 = new System.Windows.Forms.ListViewItem(new string[] {
									"Вид папки - Жанра",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem55 = new System.Windows.Forms.ListViewItem(new string[] {
									"Транслитерация имен файлов",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem56 = new System.Windows.Forms.ListViewItem(new string[] {
									"\"Строгие\" имена файлов",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem57 = new System.Windows.Forms.ListViewItem(new string[] {
									"Обработка пробелов",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem58 = new System.Windows.Forms.ListViewItem(new string[] {
									"Упаковка в архив",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem59 = new System.Windows.Forms.ListViewItem(new string[] {
									"Режим",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem60 = new System.Windows.Forms.ListViewItem(new string[] {
									"Добавить к имени файла ID Книги",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem61 = new System.Windows.Forms.ListViewItem(new string[] {
									"Удалить исходные файлы после сортировки",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem62 = new System.Windows.Forms.ListViewItem(new string[] {
									"Папка для нечитаемых fb2-файлов (архивов)",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem63 = new System.Windows.Forms.ListViewItem(new string[] {
									"Папка для fb2-файлов с длинными именами",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem64 = new System.Windows.Forms.ListViewItem(new string[] {
									"Всего папок",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem65 = new System.Windows.Forms.ListViewItem(new string[] {
									"Всего файлов",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem66 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные fb2-файлы",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem67 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные fb2 в .zip-архивах",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem68 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные fb2 в .rar-архивах",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem69 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные fb2 в .7z-архивах",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem70 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные fb2 в .BZip2-архивах",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem71 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные fb2 в .Gzip-архивах",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem72 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные fb2 в .Tar-пакетах",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem73 = new System.Windows.Forms.ListViewItem(new string[] {
									"Другие файлы",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem74 = new System.Windows.Forms.ListViewItem(new string[] {
									"Всего отсортировано",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem75 = new System.Windows.Forms.ListViewItem(new string[] {
									"Нечитаемые fb2-файлы (архивы)",
									"0"}, -1);
			this.ssProgress = new System.Windows.Forms.StatusStrip();
			this.tsslblProgress = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.tsFileManager = new System.Windows.Forms.ToolStrip();
			this.tsbtnOpenDir = new System.Windows.Forms.ToolStripButton();
			this.tsSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnSortFilesTo = new System.Windows.Forms.ToolStripButton();
			this.tsSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.pSource = new System.Windows.Forms.Panel();
			this.lblSortAllTargetDir = new System.Windows.Forms.Label();
			this.btnSortAllToDir = new System.Windows.Forms.Button();
			this.tboxSortAllToDir = new System.Windows.Forms.TextBox();
			this.chBoxScanSubDir = new System.Windows.Forms.CheckBox();
			this.tboxSourceDir = new System.Windows.Forms.TextBox();
			this.lblDir = new System.Windows.Forms.Label();
			this.fbdScanDir = new System.Windows.Forms.FolderBrowserDialog();
			this.gBoxRenameTemplates = new System.Windows.Forms.GroupBox();
			this.txtBoxTemplatesFromLine = new System.Windows.Forms.TextBox();
			this.cboxTemplatesPrepared = new System.Windows.Forms.ComboBox();
			this.rBtnTemplatesFromLine = new System.Windows.Forms.RadioButton();
			this.rBtnTemplatesPrepared = new System.Windows.Forms.RadioButton();
			this.pProgress = new System.Windows.Forms.Panel();
			this.lvSettings = new System.Windows.Forms.ListView();
			this.cHeaderSettings = new System.Windows.Forms.ColumnHeader();
			this.cHeaderSettingsValue = new System.Windows.Forms.ColumnHeader();
			this.lvFilesCount = new System.Windows.Forms.ListView();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.gBoxTemplatesDescription = new System.Windows.Forms.GroupBox();
			this.richTxtBoxDescTemplates = new System.Windows.Forms.RichTextBox();
			this.ssProgress.SuspendLayout();
			this.tsFileManager.SuspendLayout();
			this.pSource.SuspendLayout();
			this.gBoxRenameTemplates.SuspendLayout();
			this.pProgress.SuspendLayout();
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
									this.tsSep2});
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
			this.tsbtnSortFilesTo.ToolTipText = "Сортировать файлы в соответствии с шаблонами подстановки";
			this.tsbtnSortFilesTo.Click += new System.EventHandler(this.TsbtnSortFilesToClick);
			// 
			// tsSep2
			// 
			this.tsSep2.Name = "tsSep2";
			this.tsSep2.Size = new System.Drawing.Size(6, 31);
			// 
			// pSource
			// 
			this.pSource.Controls.Add(this.lblSortAllTargetDir);
			this.pSource.Controls.Add(this.btnSortAllToDir);
			this.pSource.Controls.Add(this.chBoxScanSubDir);
			this.pSource.Controls.Add(this.tboxSortAllToDir);
			this.pSource.Controls.Add(this.tboxSourceDir);
			this.pSource.Controls.Add(this.lblDir);
			this.pSource.Dock = System.Windows.Forms.DockStyle.Top;
			this.pSource.Location = new System.Drawing.Point(0, 31);
			this.pSource.Name = "pSource";
			this.pSource.Size = new System.Drawing.Size(828, 55);
			this.pSource.TabIndex = 20;
			// 
			// lblSortAllTargetDir
			// 
			this.lblSortAllTargetDir.AutoSize = true;
			this.lblSortAllTargetDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblSortAllTargetDir.Location = new System.Drawing.Point(2, 33);
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
			this.btnSortAllToDir.Location = new System.Drawing.Point(776, 28);
			this.btnSortAllToDir.Name = "btnSortAllToDir";
			this.btnSortAllToDir.Size = new System.Drawing.Size(37, 24);
			this.btnSortAllToDir.TabIndex = 7;
			this.btnSortAllToDir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSortAllToDir.UseVisualStyleBackColor = true;
			this.btnSortAllToDir.Click += new System.EventHandler(this.BtnSortAllToDirClick);
			// 
			// tboxSortAllToDir
			// 
			this.tboxSortAllToDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxSortAllToDir.Location = new System.Drawing.Point(162, 30);
			this.tboxSortAllToDir.Name = "tboxSortAllToDir";
			this.tboxSortAllToDir.Size = new System.Drawing.Size(608, 20);
			this.tboxSortAllToDir.TabIndex = 6;
			// 
			// chBoxScanSubDir
			// 
			this.chBoxScanSubDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chBoxScanSubDir.Checked = true;
			this.chBoxScanSubDir.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chBoxScanSubDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxScanSubDir.ForeColor = System.Drawing.Color.Navy;
			this.chBoxScanSubDir.Location = new System.Drawing.Point(652, 5);
			this.chBoxScanSubDir.Name = "chBoxScanSubDir";
			this.chBoxScanSubDir.Size = new System.Drawing.Size(172, 24);
			this.chBoxScanSubDir.TabIndex = 8;
			this.chBoxScanSubDir.Text = "Сканировать и подпапки";
			this.chBoxScanSubDir.UseVisualStyleBackColor = true;
			// 
			// tboxSourceDir
			// 
			this.tboxSourceDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxSourceDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tboxSourceDir.Location = new System.Drawing.Point(162, 5);
			this.tboxSourceDir.Name = "tboxSourceDir";
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
			// gBoxRenameTemplates
			// 
			this.gBoxRenameTemplates.Controls.Add(this.txtBoxTemplatesFromLine);
			this.gBoxRenameTemplates.Controls.Add(this.cboxTemplatesPrepared);
			this.gBoxRenameTemplates.Controls.Add(this.rBtnTemplatesFromLine);
			this.gBoxRenameTemplates.Controls.Add(this.rBtnTemplatesPrepared);
			this.gBoxRenameTemplates.Dock = System.Windows.Forms.DockStyle.Top;
			this.gBoxRenameTemplates.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gBoxRenameTemplates.ForeColor = System.Drawing.Color.Indigo;
			this.gBoxRenameTemplates.Location = new System.Drawing.Point(0, 86);
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
									"*L*\\*G*\\[*BAL*_*BAF*][_*BAM*][_*BAN*]\\[*SN*]\\*BAL*[_(*SN*-*SI*)]_*BT*",
									"[*L*]\\*G*\\[*BAL*_*BAF*][_*BAM*][_*BAN*]\\[*SN*]\\*BAL*[_(*SN*-*SI*)]_*BT*",
									"*L*\\*G*\\*BAL*_*BAF*[_*BAM*][_*BAN*]\\[*SN*]\\*BAL*[_(*SN*-*SI*)]_*BT*",
									"[*L*]\\*G*\\*BAL*_*BAF*[_*BAM*][_*BAN*]\\[*SN*]\\*BAL*[_(*SN*-*SI*)]_*BT*",
									"*G*\\[*BAL*_*BAF*][_*BAM*][_*BAN*]\\[*SN*]\\*BAL*[_(*SN*-*SI*)]_*BT*",
									"[*G*]\\[*BAL*_*BAF*][_*BAM*][_*BAN*]\\[*SN*]\\*BAL*[_(*SN*-*SI*)]_*BT*",
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
			// pProgress
			// 
			this.pProgress.Controls.Add(this.lvSettings);
			this.pProgress.Controls.Add(this.lvFilesCount);
			this.pProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pProgress.Location = new System.Drawing.Point(0, 333);
			this.pProgress.Name = "pProgress";
			this.pProgress.Size = new System.Drawing.Size(828, 205);
			this.pProgress.TabIndex = 29;
			// 
			// lvSettings
			// 
			this.lvSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lvSettings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.cHeaderSettings,
									this.cHeaderSettingsValue});
			this.lvSettings.GridLines = true;
			listViewGroup7.Header = "Основные";
			listViewGroup7.Name = "listViewGroup1";
			listViewGroup8.Header = "Что делать с одинаковыми файлами";
			listViewGroup8.Name = "listViewGroup2";
			listViewGroup9.Header = "Папки с проблемными fb2-файлами";
			listViewGroup9.Name = "listViewGroup3";
			this.lvSettings.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
									listViewGroup7,
									listViewGroup8,
									listViewGroup9});
			listViewItem51.Group = listViewGroup7;
			listViewItem51.StateImageIndex = 0;
			listViewItem52.Group = listViewGroup7;
			listViewItem53.Group = listViewGroup7;
			listViewItem54.Group = listViewGroup7;
			listViewItem55.Group = listViewGroup7;
			listViewItem56.Group = listViewGroup7;
			listViewItem57.Group = listViewGroup7;
			listViewItem58.Group = listViewGroup7;
			listViewItem59.Group = listViewGroup8;
			listViewItem60.Group = listViewGroup8;
			listViewItem61.Group = listViewGroup7;
			listViewItem62.Group = listViewGroup9;
			listViewItem63.Group = listViewGroup9;
			this.lvSettings.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
									listViewItem51,
									listViewItem52,
									listViewItem53,
									listViewItem54,
									listViewItem55,
									listViewItem56,
									listViewItem57,
									listViewItem58,
									listViewItem59,
									listViewItem60,
									listViewItem61,
									listViewItem62,
									listViewItem63});
			this.lvSettings.Location = new System.Drawing.Point(282, 0);
			this.lvSettings.Name = "lvSettings";
			this.lvSettings.Size = new System.Drawing.Size(546, 202);
			this.lvSettings.TabIndex = 22;
			this.lvSettings.UseCompatibleStateImageBehavior = false;
			this.lvSettings.View = System.Windows.Forms.View.Details;
			// 
			// cHeaderSettings
			// 
			this.cHeaderSettings.Text = "Настройки Сортировки";
			this.cHeaderSettings.Width = 255;
			// 
			// cHeaderSettingsValue
			// 
			this.cHeaderSettingsValue.Text = "Значение";
			this.cHeaderSettingsValue.Width = 270;
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
									listViewItem64,
									listViewItem65,
									listViewItem66,
									listViewItem67,
									listViewItem68,
									listViewItem69,
									listViewItem70,
									listViewItem71,
									listViewItem72,
									listViewItem73,
									listViewItem74,
									listViewItem75});
			this.lvFilesCount.Location = new System.Drawing.Point(4, 3);
			this.lvFilesCount.Name = "lvFilesCount";
			this.lvFilesCount.Size = new System.Drawing.Size(272, 199);
			this.lvFilesCount.TabIndex = 21;
			this.lvFilesCount.UseCompatibleStateImageBehavior = false;
			this.lvFilesCount.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Папки и файлы";
			this.columnHeader6.Width = 180;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Количество";
			this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader7.Width = 80;
			// 
			// gBoxTemplatesDescription
			// 
			this.gBoxTemplatesDescription.Controls.Add(this.richTxtBoxDescTemplates);
			this.gBoxTemplatesDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gBoxTemplatesDescription.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gBoxTemplatesDescription.ForeColor = System.Drawing.Color.Maroon;
			this.gBoxTemplatesDescription.Location = new System.Drawing.Point(0, 155);
			this.gBoxTemplatesDescription.Name = "gBoxTemplatesDescription";
			this.gBoxTemplatesDescription.Size = new System.Drawing.Size(828, 178);
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
			this.richTxtBoxDescTemplates.Size = new System.Drawing.Size(822, 159);
			this.richTxtBoxDescTemplates.TabIndex = 0;
			this.richTxtBoxDescTemplates.Text = "";
			// 
			// SFBTpFileManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.gBoxTemplatesDescription);
			this.Controls.Add(this.pProgress);
			this.Controls.Add(this.gBoxRenameTemplates);
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
			this.pProgress.ResumeLayout(false);
			this.gBoxTemplatesDescription.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ListView lvSettings;
		private System.Windows.Forms.ColumnHeader cHeaderSettingsValue;
		private System.Windows.Forms.ColumnHeader cHeaderSettings;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ListView lvFilesCount;
		private System.Windows.Forms.CheckBox chBoxScanSubDir;
		private System.Windows.Forms.Panel pProgress;
		private System.Windows.Forms.Panel pSource;
		private System.Windows.Forms.RichTextBox richTxtBoxDescTemplates;
		private System.Windows.Forms.GroupBox gBoxTemplatesDescription;
		private System.Windows.Forms.TextBox txtBoxTemplatesFromLine;
		private System.Windows.Forms.ComboBox cboxTemplatesPrepared;
		private System.Windows.Forms.RadioButton rBtnTemplatesPrepared;
		private System.Windows.Forms.RadioButton rBtnTemplatesFromLine;
		private System.Windows.Forms.GroupBox gBoxRenameTemplates;
		private System.Windows.Forms.TextBox tboxSortAllToDir;
		private System.Windows.Forms.Button btnSortAllToDir;
		private System.Windows.Forms.Label lblSortAllTargetDir;
		private System.Windows.Forms.FolderBrowserDialog fbdScanDir;
		private System.Windows.Forms.Label lblDir;
		private System.Windows.Forms.TextBox tboxSourceDir;
		private System.Windows.Forms.ToolStrip tsFileManager;
		private System.Windows.Forms.ToolStripButton tsbtnSortFilesTo;
		private System.Windows.Forms.ToolStripSeparator tsSep2;
		private System.Windows.Forms.ToolStripSeparator tsSep1;
		private System.Windows.Forms.ToolStripButton tsbtnOpenDir;
		private System.Windows.Forms.ToolStripProgressBar tsProgressBar;
		private System.Windows.Forms.ToolStripStatusLabel tsslblProgress;
		private System.Windows.Forms.StatusStrip ssProgress;
	}
}
