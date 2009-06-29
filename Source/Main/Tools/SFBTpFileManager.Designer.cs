/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 16.03.2009
 * Time: 9:03
 * 
 * License: GPL 2.1
 */
namespace SharpFBTools.Tools
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
			System.Windows.Forms.ListViewGroup listViewGroup1 = new System.Windows.Forms.ListViewGroup("Основные", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup2 = new System.Windows.Forms.ListViewGroup("Что делать с одинаковыми файлами", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup3 = new System.Windows.Forms.ListViewGroup("Папки с проблемными fb2-файлами", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup4 = new System.Windows.Forms.ListViewGroup("Названия папок для тэгов без данных", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup5 = new System.Windows.Forms.ListViewGroup("Названия Групп Жанров", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
									"Регистр имени файла (папок)",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
									"Раскладка файлов по Авторам",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
									"Раскладка файлов по Жанрам",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
									"Вид папки - Жанра",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
									"Транслитерация имен файлов",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
									"\"Строгие\" имена файлов",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
									"Обработка пробелов",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem(new string[] {
									"Упаковка в архив",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem(new string[] {
									"Режим",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem(new string[] {
									"Добавить к имени файла ID Книги",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem(new string[] {
									"Удалить исходные файлы после сортировки",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem(new string[] {
									"Папка для нечитаемых fb2-файлов (архивов)",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem(new string[] {
									"Папка для fb2-файлов с длинными именами",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem(new string[] {
									"Схема Жанров",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem(new string[] {
									"Для неизвестной группы Жанров",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem16 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Жанра Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem17 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Языка Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem18 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Имени Автора Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem19 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Отчества Автора Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem20 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Фамилия Автора Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem21 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Ника Автора Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem22 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Названия Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem23 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Серии Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem24 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Номера Серии Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem25 = new System.Windows.Forms.ListViewItem(new string[] {
									"Тип Сортировки",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem26 = new System.Windows.Forms.ListViewItem(new string[] {
									"Папка для не валидных fb2-файлов",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem27 = new System.Windows.Forms.ListViewItem(new string[] {
									"Папка для \"битых\" архивов",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem28 = new System.Windows.Forms.ListViewItem(new string[] {
									"Фантастика, Фэнтэзи",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem29 = new System.Windows.Forms.ListViewItem(new string[] {
									"Детективы, Боевики",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem30 = new System.Windows.Forms.ListViewItem(new string[] {
									"Проза",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem31 = new System.Windows.Forms.ListViewItem(new string[] {
									"Любовные романы",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem32 = new System.Windows.Forms.ListViewItem(new string[] {
									"Приключения",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem33 = new System.Windows.Forms.ListViewItem(new string[] {
									"Детское",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem34 = new System.Windows.Forms.ListViewItem(new string[] {
									"Поэзия, Драматургия",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem35 = new System.Windows.Forms.ListViewItem(new string[] {
									"Старинное",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem36 = new System.Windows.Forms.ListViewItem(new string[] {
									"Наука, Образование",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem37 = new System.Windows.Forms.ListViewItem(new string[] {
									"Компьютеры",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem38 = new System.Windows.Forms.ListViewItem(new string[] {
									"Справочники",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem39 = new System.Windows.Forms.ListViewItem(new string[] {
									"Документальное",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem40 = new System.Windows.Forms.ListViewItem(new string[] {
									"Религия",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem41 = new System.Windows.Forms.ListViewItem(new string[] {
									"Юмор",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem42 = new System.Windows.Forms.ListViewItem(new string[] {
									"Дом, Семья",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem43 = new System.Windows.Forms.ListViewItem(new string[] {
									"Бизнес",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem44 = new System.Windows.Forms.ListViewItem(new string[] {
									"Всего папок",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem45 = new System.Windows.Forms.ListViewItem(new string[] {
									"Всего файлов",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem46 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные fb2-файлы",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem47 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные  Zip-архивы с fb2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem48 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные  Rar-архивы с fb2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem49 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные 7zip-архивы с fb2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem50 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные BZip2-архивы с fb2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem51 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные Gzip-архивы с fb2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem52 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные Tar-пакеты с fb2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem53 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные fb2-файлы из архивов",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem54 = new System.Windows.Forms.ListViewItem(new string[] {
									"Другие файлы",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem55 = new System.Windows.Forms.ListViewItem(new string[] {
									"Создано в папке-приемнике",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem56 = new System.Windows.Forms.ListViewItem(new string[] {
									"Нечитаемые fb2-файлы (архивы)",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem57 = new System.Windows.Forms.ListViewItem(new string[] {
									"Не валидные fb2-файлы (при вкл. опции)",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem58 = new System.Windows.Forms.ListViewItem(new string[] {
									"Битые архивы (не открылись)",
									"0"}, -1);
			this.ssProgress = new System.Windows.Forms.StatusStrip();
			this.tsslblProgress = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.tsFullSort = new System.Windows.Forms.ToolStrip();
			this.tsbtnOpenDir = new System.Windows.Forms.ToolStripButton();
			this.tsSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnTargetDir = new System.Windows.Forms.ToolStripButton();
			this.tsSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnSortFilesTo = new System.Windows.Forms.ToolStripButton();
			this.tsbtnFullSortStop = new System.Windows.Forms.ToolStripButton();
			this.lblFullSortTargetDir = new System.Windows.Forms.Label();
			this.tboxSortAllToDir = new System.Windows.Forms.TextBox();
			this.tboxSourceDir = new System.Windows.Forms.TextBox();
			this.lblFullSortDir = new System.Windows.Forms.Label();
			this.fbdScanDir = new System.Windows.Forms.FolderBrowserDialog();
			this.btnInsertTemplates = new System.Windows.Forms.Button();
			this.txtBoxTemplatesFromLine = new System.Windows.Forms.TextBox();
			this.richTxtBoxDescTemplates = new System.Windows.Forms.RichTextBox();
			this.tcSort = new System.Windows.Forms.TabControl();
			this.tpFullSort = new System.Windows.Forms.TabPage();
			this.gBoxFullSortTemplatesDescription = new System.Windows.Forms.GroupBox();
			this.gBoxFullSortRenameTemplates = new System.Windows.Forms.GroupBox();
			this.pFullSortDirs = new System.Windows.Forms.Panel();
			this.chBoxScanSubDir = new System.Windows.Forms.CheckBox();
			this.tpSelectedSort = new System.Windows.Forms.TabPage();
			this.pData = new System.Windows.Forms.Panel();
			this.lvSSData = new System.Windows.Forms.ListView();
			this.cHeaderLang = new System.Windows.Forms.ColumnHeader();
			this.cHeaderGenresGroup = new System.Windows.Forms.ColumnHeader();
			this.cHeaderGenre = new System.Windows.Forms.ColumnHeader();
			this.cHeaderLast = new System.Windows.Forms.ColumnHeader();
			this.cHeaderFirst = new System.Windows.Forms.ColumnHeader();
			this.cHeaderMiddle = new System.Windows.Forms.ColumnHeader();
			this.cHeaderNick = new System.Windows.Forms.ColumnHeader();
			this.cHeaderSequence = new System.Windows.Forms.ColumnHeader();
			this.cHeaderExactFit = new System.Windows.Forms.ColumnHeader();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnSSGetData = new System.Windows.Forms.Button();
			this.gBoxSelectedlSortRenameTemplates = new System.Windows.Forms.GroupBox();
			this.btnSSInsertTemplates = new System.Windows.Forms.Button();
			this.txtBoxSSTemplatesFromLine = new System.Windows.Forms.TextBox();
			this.pSelectedSortDirs = new System.Windows.Forms.Panel();
			this.chBoxSSScanSubDir = new System.Windows.Forms.CheckBox();
			this.lblSSTargetDir = new System.Windows.Forms.Label();
			this.tboxSSToDir = new System.Windows.Forms.TextBox();
			this.tboxSSSourceDir = new System.Windows.Forms.TextBox();
			this.lbSSlDir = new System.Windows.Forms.Label();
			this.tsSelectedSort = new System.Windows.Forms.ToolStrip();
			this.tsbtnSSOpenDir = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnSSTargetDir = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnSSSortFilesTo = new System.Windows.Forms.ToolStripButton();
			this.tsbtnSSSortStop = new System.Windows.Forms.ToolStripButton();
			this.lvSettings = new System.Windows.Forms.ListView();
			this.cHeaderSettings = new System.Windows.Forms.ColumnHeader();
			this.cHeaderSettingsValue = new System.Windows.Forms.ColumnHeader();
			this.lvFilesCount = new System.Windows.Forms.ListView();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.pProgress = new System.Windows.Forms.Panel();
			this.ssProgress.SuspendLayout();
			this.tsFullSort.SuspendLayout();
			this.tcSort.SuspendLayout();
			this.tpFullSort.SuspendLayout();
			this.gBoxFullSortTemplatesDescription.SuspendLayout();
			this.gBoxFullSortRenameTemplates.SuspendLayout();
			this.pFullSortDirs.SuspendLayout();
			this.tpSelectedSort.SuspendLayout();
			this.pData.SuspendLayout();
			this.panel1.SuspendLayout();
			this.gBoxSelectedlSortRenameTemplates.SuspendLayout();
			this.pSelectedSortDirs.SuspendLayout();
			this.tsSelectedSort.SuspendLayout();
			this.pProgress.SuspendLayout();
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
			// tsFullSort
			// 
			this.tsFullSort.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tsFullSort.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsbtnOpenDir,
									this.tsSep1,
									this.tsbtnTargetDir,
									this.tsSep2,
									this.tsbtnSortFilesTo,
									this.tsbtnFullSortStop});
			this.tsFullSort.Location = new System.Drawing.Point(3, 3);
			this.tsFullSort.Name = "tsFullSort";
			this.tsFullSort.Size = new System.Drawing.Size(814, 31);
			this.tsFullSort.TabIndex = 19;
			// 
			// tsbtnOpenDir
			// 
			this.tsbtnOpenDir.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnOpenDir.Image")));
			this.tsbtnOpenDir.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnOpenDir.Name = "tsbtnOpenDir";
			this.tsbtnOpenDir.Size = new System.Drawing.Size(123, 28);
			this.tsbtnOpenDir.Text = "Папка - источник";
			this.tsbtnOpenDir.ToolTipText = "Открыть папку с fb2-файлами и (или) архивами...";
			this.tsbtnOpenDir.Click += new System.EventHandler(this.TsbtnOpenDirClick);
			// 
			// tsSep1
			// 
			this.tsSep1.Name = "tsSep1";
			this.tsSep1.Size = new System.Drawing.Size(6, 31);
			// 
			// tsbtnTargetDir
			// 
			this.tsbtnTargetDir.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnTargetDir.Image")));
			this.tsbtnTargetDir.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnTargetDir.Name = "tsbtnTargetDir";
			this.tsbtnTargetDir.Size = new System.Drawing.Size(124, 28);
			this.tsbtnTargetDir.Text = "Папка - приемник";
			this.tsbtnTargetDir.ToolTipText = "Папка - приемник отсортированных fb2-файлов (архивов)";
			this.tsbtnTargetDir.Click += new System.EventHandler(this.BtnSortAllToDirClick);
			// 
			// tsSep2
			// 
			this.tsSep2.Name = "tsSep2";
			this.tsSep2.Size = new System.Drawing.Size(6, 31);
			// 
			// tsbtnSortFilesTo
			// 
			this.tsbtnSortFilesTo.AutoToolTip = false;
			this.tsbtnSortFilesTo.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSortFilesTo.Image")));
			this.tsbtnSortFilesTo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnSortFilesTo.Name = "tsbtnSortFilesTo";
			this.tsbtnSortFilesTo.Size = new System.Drawing.Size(102, 28);
			this.tsbtnSortFilesTo.Text = "Сортировать";
			this.tsbtnSortFilesTo.Click += new System.EventHandler(this.TsbtnSortFilesToClick);
			// 
			// tsbtnFullSortStop
			// 
			this.tsbtnFullSortStop.Enabled = false;
			this.tsbtnFullSortStop.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnFullSortStop.Image")));
			this.tsbtnFullSortStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnFullSortStop.Name = "tsbtnFullSortStop";
			this.tsbtnFullSortStop.Size = new System.Drawing.Size(96, 28);
			this.tsbtnFullSortStop.Text = "Остановить";
			this.tsbtnFullSortStop.Click += new System.EventHandler(this.TsbtnFullSortStopClick);
			// 
			// lblFullSortTargetDir
			// 
			this.lblFullSortTargetDir.AutoSize = true;
			this.lblFullSortTargetDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblFullSortTargetDir.Location = new System.Drawing.Point(3, 33);
			this.lblFullSortTargetDir.Name = "lblFullSortTargetDir";
			this.lblFullSortTargetDir.Size = new System.Drawing.Size(152, 13);
			this.lblFullSortTargetDir.TabIndex = 18;
			this.lblFullSortTargetDir.Text = "Папка-приемник файлов:";
			// 
			// tboxSortAllToDir
			// 
			this.tboxSortAllToDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxSortAllToDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.tboxSortAllToDir.Location = new System.Drawing.Point(166, 31);
			this.tboxSortAllToDir.Name = "tboxSortAllToDir";
			this.tboxSortAllToDir.Size = new System.Drawing.Size(634, 20);
			this.tboxSortAllToDir.TabIndex = 3;
			this.tboxSortAllToDir.TextChanged += new System.EventHandler(this.TboxSortAllToDirTextChanged);
			// 
			// tboxSourceDir
			// 
			this.tboxSourceDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxSourceDir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tboxSourceDir.Location = new System.Drawing.Point(166, 6);
			this.tboxSourceDir.Name = "tboxSourceDir";
			this.tboxSourceDir.Size = new System.Drawing.Size(470, 21);
			this.tboxSourceDir.TabIndex = 1;
			this.tboxSourceDir.TextChanged += new System.EventHandler(this.TboxSourceDirTextChanged);
			// 
			// lblFullSortDir
			// 
			this.lblFullSortDir.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.lblFullSortDir.Location = new System.Drawing.Point(3, 8);
			this.lblFullSortDir.Name = "lblFullSortDir";
			this.lblFullSortDir.Size = new System.Drawing.Size(162, 19);
			this.lblFullSortDir.TabIndex = 6;
			this.lblFullSortDir.Text = "Папка для сканирования:";
			// 
			// fbdScanDir
			// 
			this.fbdScanDir.Description = "Укажите папку для сканирования с fb2-файлами и архивами";
			// 
			// btnInsertTemplates
			// 
			this.btnInsertTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnInsertTemplates.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnInsertTemplates.Location = new System.Drawing.Point(660, 15);
			this.btnInsertTemplates.Name = "btnInsertTemplates";
			this.btnInsertTemplates.Size = new System.Drawing.Size(142, 28);
			this.btnInsertTemplates.TabIndex = 9;
			this.btnInsertTemplates.Text = "Вставить готовый";
			this.btnInsertTemplates.UseVisualStyleBackColor = true;
			this.btnInsertTemplates.Click += new System.EventHandler(this.BtnInsertTemplatesClick);
			// 
			// txtBoxTemplatesFromLine
			// 
			this.txtBoxTemplatesFromLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxTemplatesFromLine.Location = new System.Drawing.Point(6, 20);
			this.txtBoxTemplatesFromLine.Name = "txtBoxTemplatesFromLine";
			this.txtBoxTemplatesFromLine.Size = new System.Drawing.Size(646, 20);
			this.txtBoxTemplatesFromLine.TabIndex = 8;
			this.txtBoxTemplatesFromLine.TextChanged += new System.EventHandler(this.TxtBoxTemplatesFromLineTextChanged);
			// 
			// richTxtBoxDescTemplates
			// 
			this.richTxtBoxDescTemplates.BackColor = System.Drawing.SystemColors.Window;
			this.richTxtBoxDescTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
			this.richTxtBoxDescTemplates.Font = new System.Drawing.Font("Tahoma", 8F);
			this.richTxtBoxDescTemplates.Location = new System.Drawing.Point(3, 16);
			this.richTxtBoxDescTemplates.Name = "richTxtBoxDescTemplates";
			this.richTxtBoxDescTemplates.ReadOnly = true;
			this.richTxtBoxDescTemplates.Size = new System.Drawing.Size(808, 99);
			this.richTxtBoxDescTemplates.TabIndex = 9;
			this.richTxtBoxDescTemplates.Text = "";
			// 
			// tcSort
			// 
			this.tcSort.Controls.Add(this.tpFullSort);
			this.tcSort.Controls.Add(this.tpSelectedSort);
			this.tcSort.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcSort.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.tcSort.Location = new System.Drawing.Point(0, 0);
			this.tcSort.Name = "tcSort";
			this.tcSort.SelectedIndex = 0;
			this.tcSort.Size = new System.Drawing.Size(828, 292);
			this.tcSort.TabIndex = 31;
			// 
			// tpFullSort
			// 
			this.tpFullSort.Controls.Add(this.gBoxFullSortTemplatesDescription);
			this.tpFullSort.Controls.Add(this.gBoxFullSortRenameTemplates);
			this.tpFullSort.Controls.Add(this.pFullSortDirs);
			this.tpFullSort.Controls.Add(this.tsFullSort);
			this.tpFullSort.Font = new System.Drawing.Font("Tahoma", 8F);
			this.tpFullSort.Location = new System.Drawing.Point(4, 22);
			this.tpFullSort.Name = "tpFullSort";
			this.tpFullSort.Padding = new System.Windows.Forms.Padding(3);
			this.tpFullSort.Size = new System.Drawing.Size(820, 266);
			this.tpFullSort.TabIndex = 0;
			this.tpFullSort.Text = " Полная Сортировка ";
			this.tpFullSort.UseVisualStyleBackColor = true;
			// 
			// gBoxFullSortTemplatesDescription
			// 
			this.gBoxFullSortTemplatesDescription.Controls.Add(this.richTxtBoxDescTemplates);
			this.gBoxFullSortTemplatesDescription.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gBoxFullSortTemplatesDescription.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gBoxFullSortTemplatesDescription.ForeColor = System.Drawing.Color.Maroon;
			this.gBoxFullSortTemplatesDescription.Location = new System.Drawing.Point(3, 145);
			this.gBoxFullSortTemplatesDescription.Name = "gBoxFullSortTemplatesDescription";
			this.gBoxFullSortTemplatesDescription.Size = new System.Drawing.Size(814, 118);
			this.gBoxFullSortTemplatesDescription.TabIndex = 33;
			this.gBoxFullSortTemplatesDescription.TabStop = false;
			this.gBoxFullSortTemplatesDescription.Text = " Описание шаблонов подстановки ";
			// 
			// gBoxFullSortRenameTemplates
			// 
			this.gBoxFullSortRenameTemplates.Controls.Add(this.btnInsertTemplates);
			this.gBoxFullSortRenameTemplates.Controls.Add(this.txtBoxTemplatesFromLine);
			this.gBoxFullSortRenameTemplates.Dock = System.Windows.Forms.DockStyle.Top;
			this.gBoxFullSortRenameTemplates.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gBoxFullSortRenameTemplates.ForeColor = System.Drawing.Color.Indigo;
			this.gBoxFullSortRenameTemplates.Location = new System.Drawing.Point(3, 89);
			this.gBoxFullSortRenameTemplates.Name = "gBoxFullSortRenameTemplates";
			this.gBoxFullSortRenameTemplates.Size = new System.Drawing.Size(814, 56);
			this.gBoxFullSortRenameTemplates.TabIndex = 32;
			this.gBoxFullSortRenameTemplates.TabStop = false;
			this.gBoxFullSortRenameTemplates.Text = " Шаблоны подстановки ";
			// 
			// pFullSortDirs
			// 
			this.pFullSortDirs.Controls.Add(this.chBoxScanSubDir);
			this.pFullSortDirs.Controls.Add(this.lblFullSortTargetDir);
			this.pFullSortDirs.Controls.Add(this.tboxSortAllToDir);
			this.pFullSortDirs.Controls.Add(this.tboxSourceDir);
			this.pFullSortDirs.Controls.Add(this.lblFullSortDir);
			this.pFullSortDirs.Dock = System.Windows.Forms.DockStyle.Top;
			this.pFullSortDirs.Location = new System.Drawing.Point(3, 34);
			this.pFullSortDirs.Name = "pFullSortDirs";
			this.pFullSortDirs.Size = new System.Drawing.Size(814, 55);
			this.pFullSortDirs.TabIndex = 31;
			// 
			// chBoxScanSubDir
			// 
			this.chBoxScanSubDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chBoxScanSubDir.Checked = true;
			this.chBoxScanSubDir.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chBoxScanSubDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxScanSubDir.ForeColor = System.Drawing.Color.Navy;
			this.chBoxScanSubDir.Location = new System.Drawing.Point(642, 4);
			this.chBoxScanSubDir.Name = "chBoxScanSubDir";
			this.chBoxScanSubDir.Size = new System.Drawing.Size(172, 24);
			this.chBoxScanSubDir.TabIndex = 2;
			this.chBoxScanSubDir.Text = "Сканировать и подпапки";
			this.chBoxScanSubDir.UseVisualStyleBackColor = true;
			// 
			// tpSelectedSort
			// 
			this.tpSelectedSort.Controls.Add(this.pData);
			this.tpSelectedSort.Controls.Add(this.gBoxSelectedlSortRenameTemplates);
			this.tpSelectedSort.Controls.Add(this.pSelectedSortDirs);
			this.tpSelectedSort.Controls.Add(this.tsSelectedSort);
			this.tpSelectedSort.Font = new System.Drawing.Font("Tahoma", 8F);
			this.tpSelectedSort.Location = new System.Drawing.Point(4, 22);
			this.tpSelectedSort.Name = "tpSelectedSort";
			this.tpSelectedSort.Padding = new System.Windows.Forms.Padding(3);
			this.tpSelectedSort.Size = new System.Drawing.Size(820, 266);
			this.tpSelectedSort.TabIndex = 1;
			this.tpSelectedSort.Text = " Избранная Сортировка ";
			this.tpSelectedSort.UseVisualStyleBackColor = true;
			// 
			// pData
			// 
			this.pData.Controls.Add(this.lvSSData);
			this.pData.Controls.Add(this.panel1);
			this.pData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pData.Location = new System.Drawing.Point(3, 145);
			this.pData.Name = "pData";
			this.pData.Size = new System.Drawing.Size(814, 118);
			this.pData.TabIndex = 62;
			// 
			// lvSSData
			// 
			this.lvSSData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.cHeaderLang,
									this.cHeaderGenresGroup,
									this.cHeaderGenre,
									this.cHeaderLast,
									this.cHeaderFirst,
									this.cHeaderMiddle,
									this.cHeaderNick,
									this.cHeaderSequence,
									this.cHeaderExactFit});
			this.lvSSData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvSSData.FullRowSelect = true;
			this.lvSSData.GridLines = true;
			this.lvSSData.Location = new System.Drawing.Point(0, 35);
			this.lvSSData.Name = "lvSSData";
			this.lvSSData.Size = new System.Drawing.Size(814, 83);
			this.lvSSData.TabIndex = 61;
			this.lvSSData.UseCompatibleStateImageBehavior = false;
			this.lvSSData.View = System.Windows.Forms.View.Details;
			// 
			// cHeaderLang
			// 
			this.cHeaderLang.Text = "Язык Книги";
			this.cHeaderLang.Width = 80;
			// 
			// cHeaderGenresGroup
			// 
			this.cHeaderGenresGroup.Text = "Группа Жанров";
			this.cHeaderGenresGroup.Width = 120;
			// 
			// cHeaderGenre
			// 
			this.cHeaderGenre.Text = "Жанр";
			this.cHeaderGenre.Width = 120;
			// 
			// cHeaderLast
			// 
			this.cHeaderLast.Text = "Фамилия";
			this.cHeaderLast.Width = 120;
			// 
			// cHeaderFirst
			// 
			this.cHeaderFirst.Text = "Имя";
			this.cHeaderFirst.Width = 80;
			// 
			// cHeaderMiddle
			// 
			this.cHeaderMiddle.Text = "Отчество";
			this.cHeaderMiddle.Width = 80;
			// 
			// cHeaderNick
			// 
			this.cHeaderNick.Text = "Ник";
			this.cHeaderNick.Width = 50;
			// 
			// cHeaderSequence
			// 
			this.cHeaderSequence.Text = "Серия";
			this.cHeaderSequence.Width = 140;
			// 
			// cHeaderExactFit
			// 
			this.cHeaderExactFit.Text = "Точное соответствие";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnSSGetData);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(814, 35);
			this.panel1.TabIndex = 62;
			// 
			// btnSSGetData
			// 
			this.btnSSGetData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.btnSSGetData.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.btnSSGetData.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSGetData.Location = new System.Drawing.Point(23, 3);
			this.btnSSGetData.Name = "btnSSGetData";
			this.btnSSGetData.Size = new System.Drawing.Size(763, 28);
			this.btnSSGetData.TabIndex = 10;
			this.btnSSGetData.Text = "Собрать данные для Избранной Сортировки";
			this.btnSSGetData.UseVisualStyleBackColor = true;
			this.btnSSGetData.Click += new System.EventHandler(this.BtnSSGetDataClick);
			// 
			// gBoxSelectedlSortRenameTemplates
			// 
			this.gBoxSelectedlSortRenameTemplates.Controls.Add(this.btnSSInsertTemplates);
			this.gBoxSelectedlSortRenameTemplates.Controls.Add(this.txtBoxSSTemplatesFromLine);
			this.gBoxSelectedlSortRenameTemplates.Dock = System.Windows.Forms.DockStyle.Top;
			this.gBoxSelectedlSortRenameTemplates.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gBoxSelectedlSortRenameTemplates.ForeColor = System.Drawing.Color.Indigo;
			this.gBoxSelectedlSortRenameTemplates.Location = new System.Drawing.Point(3, 89);
			this.gBoxSelectedlSortRenameTemplates.Name = "gBoxSelectedlSortRenameTemplates";
			this.gBoxSelectedlSortRenameTemplates.Size = new System.Drawing.Size(814, 56);
			this.gBoxSelectedlSortRenameTemplates.TabIndex = 46;
			this.gBoxSelectedlSortRenameTemplates.TabStop = false;
			this.gBoxSelectedlSortRenameTemplates.Text = " Шаблоны подстановки ";
			// 
			// btnSSInsertTemplates
			// 
			this.btnSSInsertTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSSInsertTemplates.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSInsertTemplates.Location = new System.Drawing.Point(660, 15);
			this.btnSSInsertTemplates.Name = "btnSSInsertTemplates";
			this.btnSSInsertTemplates.Size = new System.Drawing.Size(142, 28);
			this.btnSSInsertTemplates.TabIndex = 9;
			this.btnSSInsertTemplates.Text = "Вставить готовый";
			this.btnSSInsertTemplates.UseVisualStyleBackColor = true;
			this.btnSSInsertTemplates.Click += new System.EventHandler(this.BtnSSInsertTemplatesClick);
			// 
			// txtBoxSSTemplatesFromLine
			// 
			this.txtBoxSSTemplatesFromLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxSSTemplatesFromLine.Location = new System.Drawing.Point(6, 20);
			this.txtBoxSSTemplatesFromLine.Name = "txtBoxSSTemplatesFromLine";
			this.txtBoxSSTemplatesFromLine.Size = new System.Drawing.Size(646, 20);
			this.txtBoxSSTemplatesFromLine.TabIndex = 8;
			this.txtBoxSSTemplatesFromLine.TextChanged += new System.EventHandler(this.TxtBoxSSTemplatesFromLineTextChanged);
			// 
			// pSelectedSortDirs
			// 
			this.pSelectedSortDirs.Controls.Add(this.chBoxSSScanSubDir);
			this.pSelectedSortDirs.Controls.Add(this.lblSSTargetDir);
			this.pSelectedSortDirs.Controls.Add(this.tboxSSToDir);
			this.pSelectedSortDirs.Controls.Add(this.tboxSSSourceDir);
			this.pSelectedSortDirs.Controls.Add(this.lbSSlDir);
			this.pSelectedSortDirs.Dock = System.Windows.Forms.DockStyle.Top;
			this.pSelectedSortDirs.Location = new System.Drawing.Point(3, 34);
			this.pSelectedSortDirs.Name = "pSelectedSortDirs";
			this.pSelectedSortDirs.Size = new System.Drawing.Size(814, 55);
			this.pSelectedSortDirs.TabIndex = 32;
			// 
			// chBoxSSScanSubDir
			// 
			this.chBoxSSScanSubDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chBoxSSScanSubDir.Checked = true;
			this.chBoxSSScanSubDir.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chBoxSSScanSubDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxSSScanSubDir.ForeColor = System.Drawing.Color.Navy;
			this.chBoxSSScanSubDir.Location = new System.Drawing.Point(642, 4);
			this.chBoxSSScanSubDir.Name = "chBoxSSScanSubDir";
			this.chBoxSSScanSubDir.Size = new System.Drawing.Size(172, 24);
			this.chBoxSSScanSubDir.TabIndex = 2;
			this.chBoxSSScanSubDir.Text = "Сканировать и подпапки";
			this.chBoxSSScanSubDir.UseVisualStyleBackColor = true;
			// 
			// lblSSTargetDir
			// 
			this.lblSSTargetDir.AutoSize = true;
			this.lblSSTargetDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblSSTargetDir.Location = new System.Drawing.Point(3, 33);
			this.lblSSTargetDir.Name = "lblSSTargetDir";
			this.lblSSTargetDir.Size = new System.Drawing.Size(152, 13);
			this.lblSSTargetDir.TabIndex = 18;
			this.lblSSTargetDir.Text = "Папка-приемник файлов:";
			// 
			// tboxSSToDir
			// 
			this.tboxSSToDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxSSToDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.tboxSSToDir.Location = new System.Drawing.Point(166, 31);
			this.tboxSSToDir.Name = "tboxSSToDir";
			this.tboxSSToDir.Size = new System.Drawing.Size(634, 20);
			this.tboxSSToDir.TabIndex = 3;
			this.tboxSSToDir.TextChanged += new System.EventHandler(this.TboxSSToDirTextChanged);
			// 
			// tboxSSSourceDir
			// 
			this.tboxSSSourceDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxSSSourceDir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tboxSSSourceDir.Location = new System.Drawing.Point(166, 6);
			this.tboxSSSourceDir.Name = "tboxSSSourceDir";
			this.tboxSSSourceDir.Size = new System.Drawing.Size(470, 21);
			this.tboxSSSourceDir.TabIndex = 1;
			this.tboxSSSourceDir.TextChanged += new System.EventHandler(this.TboxSSSourceDirTextChanged);
			// 
			// lbSSlDir
			// 
			this.lbSSlDir.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.lbSSlDir.Location = new System.Drawing.Point(3, 8);
			this.lbSSlDir.Name = "lbSSlDir";
			this.lbSSlDir.Size = new System.Drawing.Size(162, 19);
			this.lbSSlDir.TabIndex = 6;
			this.lbSSlDir.Text = "Папка для сканирования:";
			// 
			// tsSelectedSort
			// 
			this.tsSelectedSort.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tsSelectedSort.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsbtnSSOpenDir,
									this.toolStripSeparator1,
									this.tsbtnSSTargetDir,
									this.toolStripSeparator2,
									this.tsbtnSSSortFilesTo,
									this.tsbtnSSSortStop});
			this.tsSelectedSort.Location = new System.Drawing.Point(3, 3);
			this.tsSelectedSort.Name = "tsSelectedSort";
			this.tsSelectedSort.Size = new System.Drawing.Size(814, 31);
			this.tsSelectedSort.TabIndex = 20;
			// 
			// tsbtnSSOpenDir
			// 
			this.tsbtnSSOpenDir.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSSOpenDir.Image")));
			this.tsbtnSSOpenDir.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnSSOpenDir.Name = "tsbtnSSOpenDir";
			this.tsbtnSSOpenDir.Size = new System.Drawing.Size(123, 28);
			this.tsbtnSSOpenDir.Text = "Папка - источник";
			this.tsbtnSSOpenDir.ToolTipText = "Открыть папку с fb2-файлами и (или) архивами...";
			this.tsbtnSSOpenDir.Click += new System.EventHandler(this.TsbtnSSOpenDirClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			// 
			// tsbtnSSTargetDir
			// 
			this.tsbtnSSTargetDir.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSSTargetDir.Image")));
			this.tsbtnSSTargetDir.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnSSTargetDir.Name = "tsbtnSSTargetDir";
			this.tsbtnSSTargetDir.Size = new System.Drawing.Size(124, 28);
			this.tsbtnSSTargetDir.Text = "Папка - приемник";
			this.tsbtnSSTargetDir.ToolTipText = "Папка - приемник отсортированных fb2-файлов (архивов)";
			this.tsbtnSSTargetDir.Click += new System.EventHandler(this.TsbtnSSTargetDirClick);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			// 
			// tsbtnSSSortFilesTo
			// 
			this.tsbtnSSSortFilesTo.AutoToolTip = false;
			this.tsbtnSSSortFilesTo.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSSSortFilesTo.Image")));
			this.tsbtnSSSortFilesTo.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnSSSortFilesTo.Name = "tsbtnSSSortFilesTo";
			this.tsbtnSSSortFilesTo.Size = new System.Drawing.Size(102, 28);
			this.tsbtnSSSortFilesTo.Text = "Сортировать";
			this.tsbtnSSSortFilesTo.Click += new System.EventHandler(this.TsbtnSSSortFilesToClick);
			// 
			// tsbtnSSSortStop
			// 
			this.tsbtnSSSortStop.Enabled = false;
			this.tsbtnSSSortStop.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSSSortStop.Image")));
			this.tsbtnSSSortStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnSSSortStop.Name = "tsbtnSSSortStop";
			this.tsbtnSSSortStop.Size = new System.Drawing.Size(96, 28);
			this.tsbtnSSSortStop.Text = "Остановить";
			this.tsbtnSSSortStop.Click += new System.EventHandler(this.TsbtnSSSortStopClick);
			// 
			// lvSettings
			// 
			this.lvSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.lvSettings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.cHeaderSettings,
									this.cHeaderSettingsValue});
			this.lvSettings.FullRowSelect = true;
			this.lvSettings.GridLines = true;
			listViewGroup1.Header = "Основные";
			listViewGroup1.Name = "listViewGroup1";
			listViewGroup2.Header = "Что делать с одинаковыми файлами";
			listViewGroup2.Name = "listViewGroup2";
			listViewGroup3.Header = "Папки с проблемными fb2-файлами";
			listViewGroup3.Name = "listViewGroup3";
			listViewGroup4.Header = "Названия папок для тэгов без данных";
			listViewGroup4.Name = "listViewGroup4";
			listViewGroup5.Header = "Названия Групп Жанров";
			listViewGroup5.Name = "listViewGroup5";
			this.lvSettings.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
									listViewGroup1,
									listViewGroup2,
									listViewGroup3,
									listViewGroup4,
									listViewGroup5});
			listViewItem1.Group = listViewGroup1;
			listViewItem1.StateImageIndex = 0;
			listViewItem2.Group = listViewGroup1;
			listViewItem3.Group = listViewGroup1;
			listViewItem4.Group = listViewGroup1;
			listViewItem5.Group = listViewGroup1;
			listViewItem6.Group = listViewGroup1;
			listViewItem7.Group = listViewGroup1;
			listViewItem8.Group = listViewGroup1;
			listViewItem9.Group = listViewGroup2;
			listViewItem10.Group = listViewGroup2;
			listViewItem11.Group = listViewGroup1;
			listViewItem12.Group = listViewGroup3;
			listViewItem13.Group = listViewGroup3;
			listViewItem14.Group = listViewGroup1;
			listViewItem15.Group = listViewGroup4;
			listViewItem16.Group = listViewGroup4;
			listViewItem17.Group = listViewGroup4;
			listViewItem18.Group = listViewGroup4;
			listViewItem19.Group = listViewGroup4;
			listViewItem20.Group = listViewGroup4;
			listViewItem21.Group = listViewGroup4;
			listViewItem22.Group = listViewGroup4;
			listViewItem23.Group = listViewGroup4;
			listViewItem24.Group = listViewGroup4;
			listViewItem25.Group = listViewGroup1;
			listViewItem26.Group = listViewGroup3;
			listViewItem27.Group = listViewGroup3;
			listViewItem28.Group = listViewGroup5;
			listViewItem29.Group = listViewGroup5;
			listViewItem30.Group = listViewGroup5;
			listViewItem31.Group = listViewGroup5;
			listViewItem32.Group = listViewGroup5;
			listViewItem33.Group = listViewGroup5;
			listViewItem34.Group = listViewGroup5;
			listViewItem35.Group = listViewGroup5;
			listViewItem36.Group = listViewGroup5;
			listViewItem37.Group = listViewGroup5;
			listViewItem38.Group = listViewGroup5;
			listViewItem39.Group = listViewGroup5;
			listViewItem40.Group = listViewGroup5;
			listViewItem41.Group = listViewGroup5;
			listViewItem42.Group = listViewGroup5;
			listViewItem43.Group = listViewGroup5;
			this.lvSettings.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
									listViewItem1,
									listViewItem2,
									listViewItem3,
									listViewItem4,
									listViewItem5,
									listViewItem6,
									listViewItem7,
									listViewItem8,
									listViewItem9,
									listViewItem10,
									listViewItem11,
									listViewItem12,
									listViewItem13,
									listViewItem14,
									listViewItem15,
									listViewItem16,
									listViewItem17,
									listViewItem18,
									listViewItem19,
									listViewItem20,
									listViewItem21,
									listViewItem22,
									listViewItem23,
									listViewItem24,
									listViewItem25,
									listViewItem26,
									listViewItem27,
									listViewItem28,
									listViewItem29,
									listViewItem30,
									listViewItem31,
									listViewItem32,
									listViewItem33,
									listViewItem34,
									listViewItem35,
									listViewItem36,
									listViewItem37,
									listViewItem38,
									listViewItem39,
									listViewItem40,
									listViewItem41,
									listViewItem42,
									listViewItem43});
			this.lvSettings.Location = new System.Drawing.Point(317, 4);
			this.lvSettings.Name = "lvSettings";
			this.lvSettings.Size = new System.Drawing.Size(505, 245);
			this.lvSettings.TabIndex = 11;
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
			this.cHeaderSettingsValue.Width = 310;
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
									listViewItem44,
									listViewItem45,
									listViewItem46,
									listViewItem47,
									listViewItem48,
									listViewItem49,
									listViewItem50,
									listViewItem51,
									listViewItem52,
									listViewItem53,
									listViewItem54,
									listViewItem55,
									listViewItem56,
									listViewItem57,
									listViewItem58});
			this.lvFilesCount.Location = new System.Drawing.Point(3, 4);
			this.lvFilesCount.Name = "lvFilesCount";
			this.lvFilesCount.Size = new System.Drawing.Size(309, 242);
			this.lvFilesCount.TabIndex = 10;
			this.lvFilesCount.UseCompatibleStateImageBehavior = false;
			this.lvFilesCount.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Папки и файлы";
			this.columnHeader6.Width = 220;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Количество";
			this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader7.Width = 80;
			// 
			// pProgress
			// 
			this.pProgress.Controls.Add(this.lvSettings);
			this.pProgress.Controls.Add(this.lvFilesCount);
			this.pProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pProgress.Location = new System.Drawing.Point(0, 292);
			this.pProgress.Name = "pProgress";
			this.pProgress.Size = new System.Drawing.Size(828, 246);
			this.pProgress.TabIndex = 33;
			// 
			// SFBTpFileManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tcSort);
			this.Controls.Add(this.pProgress);
			this.Controls.Add(this.ssProgress);
			this.Name = "SFBTpFileManager";
			this.Size = new System.Drawing.Size(828, 560);
			this.ssProgress.ResumeLayout(false);
			this.ssProgress.PerformLayout();
			this.tsFullSort.ResumeLayout(false);
			this.tsFullSort.PerformLayout();
			this.tcSort.ResumeLayout(false);
			this.tpFullSort.ResumeLayout(false);
			this.tpFullSort.PerformLayout();
			this.gBoxFullSortTemplatesDescription.ResumeLayout(false);
			this.gBoxFullSortRenameTemplates.ResumeLayout(false);
			this.gBoxFullSortRenameTemplates.PerformLayout();
			this.pFullSortDirs.ResumeLayout(false);
			this.pFullSortDirs.PerformLayout();
			this.tpSelectedSort.ResumeLayout(false);
			this.tpSelectedSort.PerformLayout();
			this.pData.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.gBoxSelectedlSortRenameTemplates.ResumeLayout(false);
			this.gBoxSelectedlSortRenameTemplates.PerformLayout();
			this.pSelectedSortDirs.ResumeLayout(false);
			this.pSelectedSortDirs.PerformLayout();
			this.tsSelectedSort.ResumeLayout(false);
			this.tsSelectedSort.PerformLayout();
			this.pProgress.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ToolStripButton tsbtnFullSortStop;
		private System.Windows.Forms.ToolStripButton tsbtnSSSortStop;
		private System.Windows.Forms.ColumnHeader cHeaderExactFit;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Panel pData;
		private System.Windows.Forms.Button btnSSGetData;
		private System.Windows.Forms.ColumnHeader cHeaderSequence;
		private System.Windows.Forms.ColumnHeader cHeaderNick;
		private System.Windows.Forms.ColumnHeader cHeaderMiddle;
		private System.Windows.Forms.ColumnHeader cHeaderFirst;
		private System.Windows.Forms.ColumnHeader cHeaderLast;
		private System.Windows.Forms.ColumnHeader cHeaderGenre;
		private System.Windows.Forms.ColumnHeader cHeaderGenresGroup;
		private System.Windows.Forms.ColumnHeader cHeaderLang;
		private System.Windows.Forms.ListView lvSSData;
		private System.Windows.Forms.TextBox txtBoxSSTemplatesFromLine;
		private System.Windows.Forms.Button btnSSInsertTemplates;
		private System.Windows.Forms.GroupBox gBoxSelectedlSortRenameTemplates;
		private System.Windows.Forms.TextBox tboxSSSourceDir;
		private System.Windows.Forms.TextBox tboxSSToDir;
		private System.Windows.Forms.CheckBox chBoxSSScanSubDir;
		private System.Windows.Forms.Label lblFullSortDir;
		private System.Windows.Forms.Label lblFullSortTargetDir;
		private System.Windows.Forms.Label lbSSlDir;
		private System.Windows.Forms.Label lblSSTargetDir;
		private System.Windows.Forms.Panel pSelectedSortDirs;
		private System.Windows.Forms.ToolStrip tsFullSort;
		private System.Windows.Forms.ToolStripButton tsbtnSSSortFilesTo;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripButton tsbtnSSTargetDir;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripButton tsbtnSSOpenDir;
		private System.Windows.Forms.ToolStrip tsSelectedSort;
		private System.Windows.Forms.GroupBox gBoxFullSortTemplatesDescription;
		private System.Windows.Forms.GroupBox gBoxFullSortRenameTemplates;
		private System.Windows.Forms.Panel pFullSortDirs;
		private System.Windows.Forms.TabPage tpSelectedSort;
		private System.Windows.Forms.TabPage tpFullSort;
		private System.Windows.Forms.TabControl tcSort;
		private System.Windows.Forms.Button btnInsertTemplates;
		private System.Windows.Forms.ToolStripButton tsbtnTargetDir;
		private System.Windows.Forms.ListView lvSettings;
		private System.Windows.Forms.ColumnHeader cHeaderSettingsValue;
		private System.Windows.Forms.ColumnHeader cHeaderSettings;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ListView lvFilesCount;
		private System.Windows.Forms.CheckBox chBoxScanSubDir;
		private System.Windows.Forms.Panel pProgress;
		private System.Windows.Forms.RichTextBox richTxtBoxDescTemplates;
		private System.Windows.Forms.TextBox txtBoxTemplatesFromLine;
		private System.Windows.Forms.TextBox tboxSortAllToDir;
		private System.Windows.Forms.FolderBrowserDialog fbdScanDir;
		private System.Windows.Forms.TextBox tboxSourceDir;
		private System.Windows.Forms.ToolStripButton tsbtnSortFilesTo;
		private System.Windows.Forms.ToolStripSeparator tsSep2;
		private System.Windows.Forms.ToolStripSeparator tsSep1;
		private System.Windows.Forms.ToolStripButton tsbtnOpenDir;
		private System.Windows.Forms.ToolStripProgressBar tsProgressBar;
		private System.Windows.Forms.ToolStripStatusLabel tsslblProgress;
		private System.Windows.Forms.StatusStrip ssProgress;
	}
}
