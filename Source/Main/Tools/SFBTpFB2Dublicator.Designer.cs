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
			System.Windows.Forms.ListViewGroup listViewGroup11 = new System.Windows.Forms.ListViewGroup("Основные", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup12 = new System.Windows.Forms.ListViewGroup("Что делать с одинаковыми файлами", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup13 = new System.Windows.Forms.ListViewGroup("Папки с проблемными fb2-файлами", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup14 = new System.Windows.Forms.ListViewGroup("Названия папок для тэгов без данных", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup15 = new System.Windows.Forms.ListViewGroup("Названия Групп Жанров", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewItem listViewItem117 = new System.Windows.Forms.ListViewItem(new string[] {
									"Регистр имени файла (папок)",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem118 = new System.Windows.Forms.ListViewItem(new string[] {
									"Раскладка файлов по Авторам",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem119 = new System.Windows.Forms.ListViewItem(new string[] {
									"Раскладка файлов по Жанрам",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem120 = new System.Windows.Forms.ListViewItem(new string[] {
									"Вид папки - Жанра",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem121 = new System.Windows.Forms.ListViewItem(new string[] {
									"Транслитерация имен файлов",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem122 = new System.Windows.Forms.ListViewItem(new string[] {
									"\"Строгие\" имена файлов",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem123 = new System.Windows.Forms.ListViewItem(new string[] {
									"Обработка пробелов",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem124 = new System.Windows.Forms.ListViewItem(new string[] {
									"Упаковка в архив",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem125 = new System.Windows.Forms.ListViewItem(new string[] {
									"Режим",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem126 = new System.Windows.Forms.ListViewItem(new string[] {
									"Добавить к имени файла ID Книги",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem127 = new System.Windows.Forms.ListViewItem(new string[] {
									"Удалить исходные файлы после сортировки",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem128 = new System.Windows.Forms.ListViewItem(new string[] {
									"Папка для нечитаемых fb2-файлов (архивов)",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem129 = new System.Windows.Forms.ListViewItem(new string[] {
									"Папка для fb2-файлов с длинными именами",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem130 = new System.Windows.Forms.ListViewItem(new string[] {
									"Схема Жанров",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem131 = new System.Windows.Forms.ListViewItem(new string[] {
									"Для неизвестной группы Жанров",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem132 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Жанра Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem133 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Языка Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem134 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Имени Автора Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem135 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Отчества Автора Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem136 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Фамилия Автора Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem137 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Ника Автора Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem138 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Названия Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem139 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Серии Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem140 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Номера Серии Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem141 = new System.Windows.Forms.ListViewItem(new string[] {
									"Тип Сортировки",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem142 = new System.Windows.Forms.ListViewItem(new string[] {
									"Папка для не валидных fb2-файлов",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem143 = new System.Windows.Forms.ListViewItem(new string[] {
									"Папка для \"битых\" архивов",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem144 = new System.Windows.Forms.ListViewItem(new string[] {
									"Фантастика, Фэнтэзи",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem145 = new System.Windows.Forms.ListViewItem(new string[] {
									"Детективы, Боевики",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem146 = new System.Windows.Forms.ListViewItem(new string[] {
									"Проза",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem147 = new System.Windows.Forms.ListViewItem(new string[] {
									"Любовные романы",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem148 = new System.Windows.Forms.ListViewItem(new string[] {
									"Приключения",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem149 = new System.Windows.Forms.ListViewItem(new string[] {
									"Детское",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem150 = new System.Windows.Forms.ListViewItem(new string[] {
									"Поэзия, Драматургия",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem151 = new System.Windows.Forms.ListViewItem(new string[] {
									"Старинное",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem152 = new System.Windows.Forms.ListViewItem(new string[] {
									"Наука, Образование",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem153 = new System.Windows.Forms.ListViewItem(new string[] {
									"Компьютеры",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem154 = new System.Windows.Forms.ListViewItem(new string[] {
									"Справочники",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem155 = new System.Windows.Forms.ListViewItem(new string[] {
									"Документальное",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem156 = new System.Windows.Forms.ListViewItem(new string[] {
									"Религия",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem157 = new System.Windows.Forms.ListViewItem(new string[] {
									"Юмор",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem158 = new System.Windows.Forms.ListViewItem(new string[] {
									"Дом, Семья",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem159 = new System.Windows.Forms.ListViewItem(new string[] {
									"Бизнес",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem160 = new System.Windows.Forms.ListViewItem(new string[] {
									"Всего папок",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem161 = new System.Windows.Forms.ListViewItem(new string[] {
									"Всего файлов",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem162 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные fb2-файлы",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem163 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные  Zip-архивы с fb2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem164 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные  Rar-архивы с fb2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem165 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные 7zip-архивы с fb2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem166 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные BZip2-архивы с fb2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem167 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные Gzip-архивы с fb2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem168 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные Tar-пакеты с fb2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem169 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные fb2-файлы из архивов",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem170 = new System.Windows.Forms.ListViewItem(new string[] {
									"Другие файлы",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem171 = new System.Windows.Forms.ListViewItem(new string[] {
									"Создано в папке-приемнике",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem172 = new System.Windows.Forms.ListViewItem(new string[] {
									"Нечитаемые fb2-файлы (архивы)",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem173 = new System.Windows.Forms.ListViewItem(new string[] {
									"Не валидные fb2-файлы (при вкл. опции)",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem174 = new System.Windows.Forms.ListViewItem(new string[] {
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
			this.tcDubles = new System.Windows.Forms.TabControl();
			this.tpMode = new System.Windows.Forms.TabPage();
			this.tpResult = new System.Windows.Forms.TabPage();
			this.lwResult = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.ssProgress.SuspendLayout();
			this.pProgress.SuspendLayout();
			this.pFullSortDirs.SuspendLayout();
			this.tsFullSort.SuspendLayout();
			this.tcDubles.SuspendLayout();
			this.tpResult.SuspendLayout();
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
			listViewGroup11.Header = "Основные";
			listViewGroup11.Name = "listViewGroup1";
			listViewGroup12.Header = "Что делать с одинаковыми файлами";
			listViewGroup12.Name = "listViewGroup2";
			listViewGroup13.Header = "Папки с проблемными fb2-файлами";
			listViewGroup13.Name = "listViewGroup3";
			listViewGroup14.Header = "Названия папок для тэгов без данных";
			listViewGroup14.Name = "listViewGroup4";
			listViewGroup15.Header = "Названия Групп Жанров";
			listViewGroup15.Name = "listViewGroup5";
			this.lvSettings.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
									listViewGroup11,
									listViewGroup12,
									listViewGroup13,
									listViewGroup14,
									listViewGroup15});
			listViewItem117.Group = listViewGroup11;
			listViewItem117.StateImageIndex = 0;
			listViewItem118.Group = listViewGroup11;
			listViewItem119.Group = listViewGroup11;
			listViewItem120.Group = listViewGroup11;
			listViewItem121.Group = listViewGroup11;
			listViewItem122.Group = listViewGroup11;
			listViewItem123.Group = listViewGroup11;
			listViewItem124.Group = listViewGroup11;
			listViewItem125.Group = listViewGroup12;
			listViewItem126.Group = listViewGroup12;
			listViewItem127.Group = listViewGroup11;
			listViewItem128.Group = listViewGroup13;
			listViewItem129.Group = listViewGroup13;
			listViewItem130.Group = listViewGroup11;
			listViewItem131.Group = listViewGroup14;
			listViewItem132.Group = listViewGroup14;
			listViewItem133.Group = listViewGroup14;
			listViewItem134.Group = listViewGroup14;
			listViewItem135.Group = listViewGroup14;
			listViewItem136.Group = listViewGroup14;
			listViewItem137.Group = listViewGroup14;
			listViewItem138.Group = listViewGroup14;
			listViewItem139.Group = listViewGroup14;
			listViewItem140.Group = listViewGroup14;
			listViewItem141.Group = listViewGroup11;
			listViewItem142.Group = listViewGroup13;
			listViewItem143.Group = listViewGroup13;
			listViewItem144.Group = listViewGroup15;
			listViewItem145.Group = listViewGroup15;
			listViewItem146.Group = listViewGroup15;
			listViewItem147.Group = listViewGroup15;
			listViewItem148.Group = listViewGroup15;
			listViewItem149.Group = listViewGroup15;
			listViewItem150.Group = listViewGroup15;
			listViewItem151.Group = listViewGroup15;
			listViewItem152.Group = listViewGroup15;
			listViewItem153.Group = listViewGroup15;
			listViewItem154.Group = listViewGroup15;
			listViewItem155.Group = listViewGroup15;
			listViewItem156.Group = listViewGroup15;
			listViewItem157.Group = listViewGroup15;
			listViewItem158.Group = listViewGroup15;
			listViewItem159.Group = listViewGroup15;
			this.lvSettings.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
									listViewItem117,
									listViewItem118,
									listViewItem119,
									listViewItem120,
									listViewItem121,
									listViewItem122,
									listViewItem123,
									listViewItem124,
									listViewItem125,
									listViewItem126,
									listViewItem127,
									listViewItem128,
									listViewItem129,
									listViewItem130,
									listViewItem131,
									listViewItem132,
									listViewItem133,
									listViewItem134,
									listViewItem135,
									listViewItem136,
									listViewItem137,
									listViewItem138,
									listViewItem139,
									listViewItem140,
									listViewItem141,
									listViewItem142,
									listViewItem143,
									listViewItem144,
									listViewItem145,
									listViewItem146,
									listViewItem147,
									listViewItem148,
									listViewItem149,
									listViewItem150,
									listViewItem151,
									listViewItem152,
									listViewItem153,
									listViewItem154,
									listViewItem155,
									listViewItem156,
									listViewItem157,
									listViewItem158,
									listViewItem159});
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
									listViewItem160,
									listViewItem161,
									listViewItem162,
									listViewItem163,
									listViewItem164,
									listViewItem165,
									listViewItem166,
									listViewItem167,
									listViewItem168,
									listViewItem169,
									listViewItem170,
									listViewItem171,
									listViewItem172,
									listViewItem173,
									listViewItem174});
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
			this.tsbtnSearchDubls.Size = new System.Drawing.Size(131, 28);
			this.tsbtnSearchDubls.Text = "Поиск одинаковых";
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
			// tcDubles
			// 
			this.tcDubles.Controls.Add(this.tpMode);
			this.tcDubles.Controls.Add(this.tpResult);
			this.tcDubles.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcDubles.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.tcDubles.Location = new System.Drawing.Point(0, 68);
			this.tcDubles.Name = "tcDubles";
			this.tcDubles.SelectedIndex = 0;
			this.tcDubles.Size = new System.Drawing.Size(828, 224);
			this.tcDubles.TabIndex = 37;
			// 
			// tpMode
			// 
			this.tpMode.Location = new System.Drawing.Point(4, 22);
			this.tpMode.Name = "tpMode";
			this.tpMode.Padding = new System.Windows.Forms.Padding(3);
			this.tpMode.Size = new System.Drawing.Size(820, 198);
			this.tpMode.TabIndex = 0;
			this.tpMode.Text = " Опции поиска ";
			this.tpMode.UseVisualStyleBackColor = true;
			// 
			// tpResult
			// 
			this.tpResult.Controls.Add(this.lwResult);
			this.tpResult.Location = new System.Drawing.Point(4, 22);
			this.tpResult.Name = "tpResult";
			this.tpResult.Padding = new System.Windows.Forms.Padding(3);
			this.tpResult.Size = new System.Drawing.Size(820, 198);
			this.tpResult.TabIndex = 1;
			this.tpResult.Text = " Результат ";
			this.tpResult.UseVisualStyleBackColor = true;
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
			this.lwResult.Location = new System.Drawing.Point(3, 3);
			this.lwResult.MultiSelect = false;
			this.lwResult.Name = "lwResult";
			this.lwResult.Size = new System.Drawing.Size(814, 192);
			this.lwResult.TabIndex = 12;
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
			// SFBTpFB2Dublicator
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tcDubles);
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
			this.tcDubles.ResumeLayout(false);
			this.tpResult.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ListView lwResult;
		private System.Windows.Forms.TabPage tpResult;
		private System.Windows.Forms.TabPage tpMode;
		private System.Windows.Forms.TabControl tcDubles;
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
