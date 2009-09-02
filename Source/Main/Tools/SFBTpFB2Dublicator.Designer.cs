/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 16.03.2009
 * Time: 10:03
 * 
 * License: GPL 2.1
 */
namespace SharpFBTools.Tools
{
	partial class SFBTpFB2Dublicator
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
			System.Windows.Forms.ListViewGroup listViewGroup6 = new System.Windows.Forms.ListViewGroup("Основные", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup7 = new System.Windows.Forms.ListViewGroup("Что делать с одинаковыми файлами", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup8 = new System.Windows.Forms.ListViewGroup("Папки с проблемными fb2-файлами", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup9 = new System.Windows.Forms.ListViewGroup("Названия папок для тэгов без данных", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup10 = new System.Windows.Forms.ListViewGroup("Названия Групп Жанров", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewItem listViewItem59 = new System.Windows.Forms.ListViewItem(new string[] {
									"Регистр имени файла (папок)",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem60 = new System.Windows.Forms.ListViewItem(new string[] {
									"Раскладка файлов по Авторам",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem61 = new System.Windows.Forms.ListViewItem(new string[] {
									"Раскладка файлов по Жанрам",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem62 = new System.Windows.Forms.ListViewItem(new string[] {
									"Вид папки - Жанра",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem63 = new System.Windows.Forms.ListViewItem(new string[] {
									"Транслитерация имен файлов",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem64 = new System.Windows.Forms.ListViewItem(new string[] {
									"\"Строгие\" имена файлов",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem65 = new System.Windows.Forms.ListViewItem(new string[] {
									"Обработка пробелов",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem66 = new System.Windows.Forms.ListViewItem(new string[] {
									"Упаковка в архив",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem67 = new System.Windows.Forms.ListViewItem(new string[] {
									"Режим",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem68 = new System.Windows.Forms.ListViewItem(new string[] {
									"Добавить к имени файла ID Книги",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem69 = new System.Windows.Forms.ListViewItem(new string[] {
									"Удалить исходные файлы после сортировки",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem70 = new System.Windows.Forms.ListViewItem(new string[] {
									"Папка для нечитаемых fb2-файлов (архивов)",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem71 = new System.Windows.Forms.ListViewItem(new string[] {
									"Папка для fb2-файлов с длинными именами",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem72 = new System.Windows.Forms.ListViewItem(new string[] {
									"Схема Жанров",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem73 = new System.Windows.Forms.ListViewItem(new string[] {
									"Для неизвестной группы Жанров",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem74 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Жанра Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem75 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Языка Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem76 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Имени Автора Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem77 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Отчества Автора Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem78 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Фамилия Автора Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem79 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Ника Автора Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem80 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Названия Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem81 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Серии Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem82 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Номера Серии Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem83 = new System.Windows.Forms.ListViewItem(new string[] {
									"Тип Сортировки",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem84 = new System.Windows.Forms.ListViewItem(new string[] {
									"Папка для не валидных fb2-файлов",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem85 = new System.Windows.Forms.ListViewItem(new string[] {
									"Папка для \"битых\" архивов",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem86 = new System.Windows.Forms.ListViewItem(new string[] {
									"Фантастика, Фэнтэзи",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem87 = new System.Windows.Forms.ListViewItem(new string[] {
									"Детективы, Боевики",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem88 = new System.Windows.Forms.ListViewItem(new string[] {
									"Проза",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem89 = new System.Windows.Forms.ListViewItem(new string[] {
									"Любовные романы",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem90 = new System.Windows.Forms.ListViewItem(new string[] {
									"Приключения",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem91 = new System.Windows.Forms.ListViewItem(new string[] {
									"Детское",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem92 = new System.Windows.Forms.ListViewItem(new string[] {
									"Поэзия, Драматургия",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem93 = new System.Windows.Forms.ListViewItem(new string[] {
									"Старинное",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem94 = new System.Windows.Forms.ListViewItem(new string[] {
									"Наука, Образование",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem95 = new System.Windows.Forms.ListViewItem(new string[] {
									"Компьютеры",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem96 = new System.Windows.Forms.ListViewItem(new string[] {
									"Справочники",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem97 = new System.Windows.Forms.ListViewItem(new string[] {
									"Документальное",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem98 = new System.Windows.Forms.ListViewItem(new string[] {
									"Религия",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem99 = new System.Windows.Forms.ListViewItem(new string[] {
									"Юмор",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem100 = new System.Windows.Forms.ListViewItem(new string[] {
									"Дом, Семья",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem101 = new System.Windows.Forms.ListViewItem(new string[] {
									"Бизнес",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem102 = new System.Windows.Forms.ListViewItem(new string[] {
									"Всего папок",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem103 = new System.Windows.Forms.ListViewItem(new string[] {
									"Всего файлов",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem104 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные fb2-файлы",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem105 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные  Zip-архивы с fb2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem106 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные  Rar-архивы с fb2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem107 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные 7zip-архивы с fb2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem108 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные BZip2-архивы с fb2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem109 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные Gzip-архивы с fb2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem110 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные Tar-пакеты с fb2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem111 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные fb2-файлы из архивов",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem112 = new System.Windows.Forms.ListViewItem(new string[] {
									"Другие файлы",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem113 = new System.Windows.Forms.ListViewItem(new string[] {
									"Создано в папке-приемнике",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem114 = new System.Windows.Forms.ListViewItem(new string[] {
									"Нечитаемые fb2-файлы (архивы)",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem115 = new System.Windows.Forms.ListViewItem(new string[] {
									"Не валидные fb2-файлы (при вкл. опции)",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem116 = new System.Windows.Forms.ListViewItem(new string[] {
									"Битые архивы (не открылись)",
									"0"}, -1);
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SFBTpFB2Dublicator));
			this.ssProgress = new System.Windows.Forms.StatusStrip();
			this.tsslblProgress = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.pProgress = new System.Windows.Forms.Panel();
			this.lvSettings = new System.Windows.Forms.ListView();
			this.cHeaderSettings = new System.Windows.Forms.ColumnHeader();
			this.cHeaderSettingsValue = new System.Windows.Forms.ColumnHeader();
			this.lvFilesCount = new System.Windows.Forms.ListView();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.pFullSortDirs = new System.Windows.Forms.Panel();
			this.chBoxScanSubDir = new System.Windows.Forms.CheckBox();
			this.tboxSourceDir = new System.Windows.Forms.TextBox();
			this.lblScanDir = new System.Windows.Forms.Label();
			this.tsFullSort = new System.Windows.Forms.ToolStrip();
			this.tsbtnOpenDir = new System.Windows.Forms.ToolStripButton();
			this.tsSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnSearchDubls = new System.Windows.Forms.ToolStripButton();
			this.tsbtnFullSortStop = new System.Windows.Forms.ToolStripButton();
			this.lwResult = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.pMode = new System.Windows.Forms.Panel();
			this.cboxMode = new System.Windows.Forms.ComboBox();
			this.lblMode = new System.Windows.Forms.Label();
			this.ssProgress.SuspendLayout();
			this.pProgress.SuspendLayout();
			this.pFullSortDirs.SuspendLayout();
			this.tsFullSort.SuspendLayout();
			this.pMode.SuspendLayout();
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
			this.ssProgress.TabIndex = 19;
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
			// pProgress
			// 
			this.pProgress.Controls.Add(this.lvSettings);
			this.pProgress.Controls.Add(this.lvFilesCount);
			this.pProgress.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pProgress.Location = new System.Drawing.Point(0, 292);
			this.pProgress.Name = "pProgress";
			this.pProgress.Size = new System.Drawing.Size(828, 246);
			this.pProgress.TabIndex = 34;
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
			listViewGroup6.Header = "Основные";
			listViewGroup6.Name = "listViewGroup1";
			listViewGroup7.Header = "Что делать с одинаковыми файлами";
			listViewGroup7.Name = "listViewGroup2";
			listViewGroup8.Header = "Папки с проблемными fb2-файлами";
			listViewGroup8.Name = "listViewGroup3";
			listViewGroup9.Header = "Названия папок для тэгов без данных";
			listViewGroup9.Name = "listViewGroup4";
			listViewGroup10.Header = "Названия Групп Жанров";
			listViewGroup10.Name = "listViewGroup5";
			this.lvSettings.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
									listViewGroup6,
									listViewGroup7,
									listViewGroup8,
									listViewGroup9,
									listViewGroup10});
			listViewItem59.Group = listViewGroup6;
			listViewItem59.StateImageIndex = 0;
			listViewItem60.Group = listViewGroup6;
			listViewItem61.Group = listViewGroup6;
			listViewItem62.Group = listViewGroup6;
			listViewItem63.Group = listViewGroup6;
			listViewItem64.Group = listViewGroup6;
			listViewItem65.Group = listViewGroup6;
			listViewItem66.Group = listViewGroup6;
			listViewItem67.Group = listViewGroup7;
			listViewItem68.Group = listViewGroup7;
			listViewItem69.Group = listViewGroup6;
			listViewItem70.Group = listViewGroup8;
			listViewItem71.Group = listViewGroup8;
			listViewItem72.Group = listViewGroup6;
			listViewItem73.Group = listViewGroup9;
			listViewItem74.Group = listViewGroup9;
			listViewItem75.Group = listViewGroup9;
			listViewItem76.Group = listViewGroup9;
			listViewItem77.Group = listViewGroup9;
			listViewItem78.Group = listViewGroup9;
			listViewItem79.Group = listViewGroup9;
			listViewItem80.Group = listViewGroup9;
			listViewItem81.Group = listViewGroup9;
			listViewItem82.Group = listViewGroup9;
			listViewItem83.Group = listViewGroup6;
			listViewItem84.Group = listViewGroup8;
			listViewItem85.Group = listViewGroup8;
			listViewItem86.Group = listViewGroup10;
			listViewItem87.Group = listViewGroup10;
			listViewItem88.Group = listViewGroup10;
			listViewItem89.Group = listViewGroup10;
			listViewItem90.Group = listViewGroup10;
			listViewItem91.Group = listViewGroup10;
			listViewItem92.Group = listViewGroup10;
			listViewItem93.Group = listViewGroup10;
			listViewItem94.Group = listViewGroup10;
			listViewItem95.Group = listViewGroup10;
			listViewItem96.Group = listViewGroup10;
			listViewItem97.Group = listViewGroup10;
			listViewItem98.Group = listViewGroup10;
			listViewItem99.Group = listViewGroup10;
			listViewItem100.Group = listViewGroup10;
			listViewItem101.Group = listViewGroup10;
			this.lvSettings.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
									listViewItem59,
									listViewItem60,
									listViewItem61,
									listViewItem62,
									listViewItem63,
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
									listViewItem75,
									listViewItem76,
									listViewItem77,
									listViewItem78,
									listViewItem79,
									listViewItem80,
									listViewItem81,
									listViewItem82,
									listViewItem83,
									listViewItem84,
									listViewItem85,
									listViewItem86,
									listViewItem87,
									listViewItem88,
									listViewItem89,
									listViewItem90,
									listViewItem91,
									listViewItem92,
									listViewItem93,
									listViewItem94,
									listViewItem95,
									listViewItem96,
									listViewItem97,
									listViewItem98,
									listViewItem99,
									listViewItem100,
									listViewItem101});
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
									listViewItem102,
									listViewItem103,
									listViewItem104,
									listViewItem105,
									listViewItem106,
									listViewItem107,
									listViewItem108,
									listViewItem109,
									listViewItem110,
									listViewItem111,
									listViewItem112,
									listViewItem113,
									listViewItem114,
									listViewItem115,
									listViewItem116});
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
			// pFullSortDirs
			// 
			this.pFullSortDirs.Controls.Add(this.chBoxScanSubDir);
			this.pFullSortDirs.Controls.Add(this.tboxSourceDir);
			this.pFullSortDirs.Controls.Add(this.lblScanDir);
			this.pFullSortDirs.Dock = System.Windows.Forms.DockStyle.Top;
			this.pFullSortDirs.Location = new System.Drawing.Point(0, 31);
			this.pFullSortDirs.Name = "pFullSortDirs";
			this.pFullSortDirs.Size = new System.Drawing.Size(828, 37);
			this.pFullSortDirs.TabIndex = 36;
			// 
			// chBoxScanSubDir
			// 
			this.chBoxScanSubDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chBoxScanSubDir.Checked = true;
			this.chBoxScanSubDir.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chBoxScanSubDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxScanSubDir.ForeColor = System.Drawing.Color.Navy;
			this.chBoxScanSubDir.Location = new System.Drawing.Point(656, 4);
			this.chBoxScanSubDir.Name = "chBoxScanSubDir";
			this.chBoxScanSubDir.Size = new System.Drawing.Size(172, 24);
			this.chBoxScanSubDir.TabIndex = 2;
			this.chBoxScanSubDir.Text = "Сканировать и подпапки";
			this.chBoxScanSubDir.UseVisualStyleBackColor = true;
			// 
			// tboxSourceDir
			// 
			this.tboxSourceDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxSourceDir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tboxSourceDir.Location = new System.Drawing.Point(166, 6);
			this.tboxSourceDir.Name = "tboxSourceDir";
			this.tboxSourceDir.Size = new System.Drawing.Size(484, 21);
			this.tboxSourceDir.TabIndex = 1;
			// 
			// lblScanDir
			// 
			this.lblScanDir.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.lblScanDir.Location = new System.Drawing.Point(3, 8);
			this.lblScanDir.Name = "lblScanDir";
			this.lblScanDir.Size = new System.Drawing.Size(162, 19);
			this.lblScanDir.TabIndex = 6;
			this.lblScanDir.Text = "Папка для сканирования:";
			// 
			// tsFullSort
			// 
			this.tsFullSort.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tsFullSort.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsbtnOpenDir,
									this.tsSep1,
									this.tsbtnSearchDubls,
									this.tsbtnFullSortStop});
			this.tsFullSort.Location = new System.Drawing.Point(0, 0);
			this.tsFullSort.Name = "tsFullSort";
			this.tsFullSort.Size = new System.Drawing.Size(828, 31);
			this.tsFullSort.TabIndex = 35;
			// 
			// tsbtnOpenDir
			// 
			this.tsbtnOpenDir.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnOpenDir.Image")));
			this.tsbtnOpenDir.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnOpenDir.Name = "tsbtnOpenDir";
			this.tsbtnOpenDir.Size = new System.Drawing.Size(123, 28);
			this.tsbtnOpenDir.Text = "Папка - источник";
			this.tsbtnOpenDir.ToolTipText = "Открыть папку с fb2-файлами и (или) архивами...";
			// 
			// tsSep1
			// 
			this.tsSep1.Name = "tsSep1";
			this.tsSep1.Size = new System.Drawing.Size(6, 31);
			// 
			// tsbtnSearchDubls
			// 
			this.tsbtnSearchDubls.AutoToolTip = false;
			this.tsbtnSearchDubls.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSearchDubls.Image")));
			this.tsbtnSearchDubls.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnSearchDubls.Name = "tsbtnSearchDubls";
			this.tsbtnSearchDubls.Size = new System.Drawing.Size(157, 28);
			this.tsbtnSearchDubls.Text = "Поиск одинаковых книг";
			// 
			// tsbtnFullSortStop
			// 
			this.tsbtnFullSortStop.AutoToolTip = false;
			this.tsbtnFullSortStop.Enabled = false;
			this.tsbtnFullSortStop.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnFullSortStop.Image")));
			this.tsbtnFullSortStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnFullSortStop.Name = "tsbtnFullSortStop";
			this.tsbtnFullSortStop.Size = new System.Drawing.Size(96, 28);
			this.tsbtnFullSortStop.Text = "Остановить";
			// 
			// lwResult
			// 
			this.lwResult.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeader1,
									this.columnHeader2,
									this.columnHeader3,
									this.columnHeader4});
			this.lwResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lwResult.FullRowSelect = true;
			this.lwResult.GridLines = true;
			this.lwResult.Location = new System.Drawing.Point(0, 94);
			this.lwResult.MultiSelect = false;
			this.lwResult.Name = "lwResult";
			this.lwResult.Size = new System.Drawing.Size(828, 198);
			this.lwResult.TabIndex = 38;
			this.lwResult.UseCompatibleStateImageBehavior = false;
			this.lwResult.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Книга (путь к файлу)";
			this.columnHeader1.Width = 255;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Id";
			this.columnHeader2.Width = 120;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Версия";
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Размер";
			// 
			// pMode
			// 
			this.pMode.Controls.Add(this.cboxMode);
			this.pMode.Controls.Add(this.lblMode);
			this.pMode.Dock = System.Windows.Forms.DockStyle.Top;
			this.pMode.Location = new System.Drawing.Point(0, 68);
			this.pMode.Name = "pMode";
			this.pMode.Size = new System.Drawing.Size(828, 26);
			this.pMode.TabIndex = 37;
			// 
			// cboxMode
			// 
			this.cboxMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.cboxMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxMode.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.cboxMode.FormattingEnabled = true;
			this.cboxMode.Items.AddRange(new object[] {
									"Автор(ы) и Название Книги",
									"Автор(ы),  Название Книги, Жанр(ы)",
									"Id Книги",
									"Id Книги, Автор(ы) и Название Книги",
									"Id Книги, Автор(ы), Название Книги, Жанр(ы)"});
			this.cboxMode.Location = new System.Drawing.Point(166, 1);
			this.cboxMode.Name = "cboxMode";
			this.cboxMode.Size = new System.Drawing.Size(484, 21);
			this.cboxMode.TabIndex = 17;
			// 
			// lblMode
			// 
			this.lblMode.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblMode.Location = new System.Drawing.Point(4, 4);
			this.lblMode.Name = "lblMode";
			this.lblMode.Size = new System.Drawing.Size(161, 18);
			this.lblMode.TabIndex = 0;
			this.lblMode.Text = "Данные для Сравнения:";
			// 
			// SFBTpFB2Dublicator
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lwResult);
			this.Controls.Add(this.pMode);
			this.Controls.Add(this.pFullSortDirs);
			this.Controls.Add(this.tsFullSort);
			this.Controls.Add(this.pProgress);
			this.Controls.Add(this.ssProgress);
			this.Name = "SFBTpFB2Dublicator";
			this.Size = new System.Drawing.Size(828, 560);
			this.ssProgress.ResumeLayout(false);
			this.ssProgress.PerformLayout();
			this.pProgress.ResumeLayout(false);
			this.pFullSortDirs.ResumeLayout(false);
			this.pFullSortDirs.PerformLayout();
			this.tsFullSort.ResumeLayout(false);
			this.tsFullSort.PerformLayout();
			this.pMode.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ComboBox cboxMode;
		private System.Windows.Forms.Label lblMode;
		private System.Windows.Forms.Panel pMode;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ListView lwResult;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ListView lvFilesCount;
		private System.Windows.Forms.ColumnHeader cHeaderSettingsValue;
		private System.Windows.Forms.ColumnHeader cHeaderSettings;
		private System.Windows.Forms.ListView lvSettings;
		private System.Windows.Forms.Panel pProgress;
		private System.Windows.Forms.Label lblScanDir;
		private System.Windows.Forms.TextBox tboxSourceDir;
		private System.Windows.Forms.CheckBox chBoxScanSubDir;
		private System.Windows.Forms.Panel pFullSortDirs;
		private System.Windows.Forms.ToolStripButton tsbtnFullSortStop;
		private System.Windows.Forms.ToolStripButton tsbtnSearchDubls;
		private System.Windows.Forms.ToolStripSeparator tsSep1;
		private System.Windows.Forms.ToolStripButton tsbtnOpenDir;
		private System.Windows.Forms.ToolStrip tsFullSort;
		private System.Windows.Forms.ToolStripProgressBar tsProgressBar;
		private System.Windows.Forms.ToolStripStatusLabel tsslblProgress;
		private System.Windows.Forms.StatusStrip ssProgress;
	}
}
