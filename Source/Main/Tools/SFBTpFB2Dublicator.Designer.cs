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
			System.Windows.Forms.ListViewGroup listViewGroup51 = new System.Windows.Forms.ListViewGroup("Основные", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup52 = new System.Windows.Forms.ListViewGroup("Что делать с одинаковыми файлами", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup53 = new System.Windows.Forms.ListViewGroup("Папки с проблемными fb2-файлами", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup54 = new System.Windows.Forms.ListViewGroup("Названия папок для тэгов без данных", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewGroup listViewGroup55 = new System.Windows.Forms.ListViewGroup("Названия Групп Жанров", System.Windows.Forms.HorizontalAlignment.Left);
			System.Windows.Forms.ListViewItem listViewItem581 = new System.Windows.Forms.ListViewItem(new string[] {
									"Регистр имени файла (папок)",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem582 = new System.Windows.Forms.ListViewItem(new string[] {
									"Раскладка файлов по Авторам",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem583 = new System.Windows.Forms.ListViewItem(new string[] {
									"Раскладка файлов по Жанрам",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem584 = new System.Windows.Forms.ListViewItem(new string[] {
									"Вид папки - Жанра",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem585 = new System.Windows.Forms.ListViewItem(new string[] {
									"Транслитерация имен файлов",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem586 = new System.Windows.Forms.ListViewItem(new string[] {
									"\"Строгие\" имена файлов",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem587 = new System.Windows.Forms.ListViewItem(new string[] {
									"Обработка пробелов",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem588 = new System.Windows.Forms.ListViewItem(new string[] {
									"Упаковка в архив",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem589 = new System.Windows.Forms.ListViewItem(new string[] {
									"Режим",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem590 = new System.Windows.Forms.ListViewItem(new string[] {
									"Добавить к имени файла ID Книги",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem591 = new System.Windows.Forms.ListViewItem(new string[] {
									"Удалить исходные файлы после сортировки",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem592 = new System.Windows.Forms.ListViewItem(new string[] {
									"Папка для нечитаемых fb2-файлов (архивов)",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem593 = new System.Windows.Forms.ListViewItem(new string[] {
									"Папка для fb2-файлов с длинными именами",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem594 = new System.Windows.Forms.ListViewItem(new string[] {
									"Схема Жанров",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem595 = new System.Windows.Forms.ListViewItem(new string[] {
									"Для неизвестной группы Жанров",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem596 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Жанра Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem597 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Языка Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem598 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Имени Автора Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem599 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Отчества Автора Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem600 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Фамилия Автора Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem601 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Ника Автора Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem602 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Названия Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem603 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Серии Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem604 = new System.Windows.Forms.ListViewItem(new string[] {
									"Когда Номера Серии Книги Нет",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem605 = new System.Windows.Forms.ListViewItem(new string[] {
									"Тип Сортировки",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem606 = new System.Windows.Forms.ListViewItem(new string[] {
									"Папка для не валидных fb2-файлов",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem607 = new System.Windows.Forms.ListViewItem(new string[] {
									"Папка для \"битых\" архивов",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem608 = new System.Windows.Forms.ListViewItem(new string[] {
									"Фантастика, Фэнтэзи",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem609 = new System.Windows.Forms.ListViewItem(new string[] {
									"Детективы, Боевики",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem610 = new System.Windows.Forms.ListViewItem(new string[] {
									"Проза",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem611 = new System.Windows.Forms.ListViewItem(new string[] {
									"Любовные романы",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem612 = new System.Windows.Forms.ListViewItem(new string[] {
									"Приключения",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem613 = new System.Windows.Forms.ListViewItem(new string[] {
									"Детское",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem614 = new System.Windows.Forms.ListViewItem(new string[] {
									"Поэзия, Драматургия",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem615 = new System.Windows.Forms.ListViewItem(new string[] {
									"Старинное",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem616 = new System.Windows.Forms.ListViewItem(new string[] {
									"Наука, Образование",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem617 = new System.Windows.Forms.ListViewItem(new string[] {
									"Компьютеры",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem618 = new System.Windows.Forms.ListViewItem(new string[] {
									"Справочники",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem619 = new System.Windows.Forms.ListViewItem(new string[] {
									"Документальное",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem620 = new System.Windows.Forms.ListViewItem(new string[] {
									"Религия",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem621 = new System.Windows.Forms.ListViewItem(new string[] {
									"Юмор",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem622 = new System.Windows.Forms.ListViewItem(new string[] {
									"Дом, Семья",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem623 = new System.Windows.Forms.ListViewItem(new string[] {
									"Бизнес",
									""}, -1);
			System.Windows.Forms.ListViewItem listViewItem624 = new System.Windows.Forms.ListViewItem(new string[] {
									"Всего папок",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem625 = new System.Windows.Forms.ListViewItem(new string[] {
									"Всего файлов",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem626 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные fb2-файлы",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem627 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные  Zip-архивы с fb2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem628 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные  Rar-архивы с fb2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem629 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные 7zip-архивы с fb2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem630 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные BZip2-архивы с fb2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem631 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные Gzip-архивы с fb2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem632 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные Tar-пакеты с fb2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem633 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные fb2-файлы из архивов",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem634 = new System.Windows.Forms.ListViewItem(new string[] {
									"Другие файлы",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem635 = new System.Windows.Forms.ListViewItem(new string[] {
									"Создано в папке-приемнике",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem636 = new System.Windows.Forms.ListViewItem(new string[] {
									"Нечитаемые fb2-файлы (архивы)",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem637 = new System.Windows.Forms.ListViewItem(new string[] {
									"Не валидные fb2-файлы (при вкл. опции)",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem638 = new System.Windows.Forms.ListViewItem(new string[] {
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
			listViewGroup51.Header = "Основные";
			listViewGroup51.Name = "listViewGroup1";
			listViewGroup52.Header = "Что делать с одинаковыми файлами";
			listViewGroup52.Name = "listViewGroup2";
			listViewGroup53.Header = "Папки с проблемными fb2-файлами";
			listViewGroup53.Name = "listViewGroup3";
			listViewGroup54.Header = "Названия папок для тэгов без данных";
			listViewGroup54.Name = "listViewGroup4";
			listViewGroup55.Header = "Названия Групп Жанров";
			listViewGroup55.Name = "listViewGroup5";
			this.lvSettings.Groups.AddRange(new System.Windows.Forms.ListViewGroup[] {
									listViewGroup51,
									listViewGroup52,
									listViewGroup53,
									listViewGroup54,
									listViewGroup55});
			listViewItem581.Group = listViewGroup51;
			listViewItem581.StateImageIndex = 0;
			listViewItem582.Group = listViewGroup51;
			listViewItem583.Group = listViewGroup51;
			listViewItem584.Group = listViewGroup51;
			listViewItem585.Group = listViewGroup51;
			listViewItem586.Group = listViewGroup51;
			listViewItem587.Group = listViewGroup51;
			listViewItem588.Group = listViewGroup51;
			listViewItem589.Group = listViewGroup52;
			listViewItem590.Group = listViewGroup52;
			listViewItem591.Group = listViewGroup51;
			listViewItem592.Group = listViewGroup53;
			listViewItem593.Group = listViewGroup53;
			listViewItem594.Group = listViewGroup51;
			listViewItem595.Group = listViewGroup54;
			listViewItem596.Group = listViewGroup54;
			listViewItem597.Group = listViewGroup54;
			listViewItem598.Group = listViewGroup54;
			listViewItem599.Group = listViewGroup54;
			listViewItem600.Group = listViewGroup54;
			listViewItem601.Group = listViewGroup54;
			listViewItem602.Group = listViewGroup54;
			listViewItem603.Group = listViewGroup54;
			listViewItem604.Group = listViewGroup54;
			listViewItem605.Group = listViewGroup51;
			listViewItem606.Group = listViewGroup53;
			listViewItem607.Group = listViewGroup53;
			listViewItem608.Group = listViewGroup55;
			listViewItem609.Group = listViewGroup55;
			listViewItem610.Group = listViewGroup55;
			listViewItem611.Group = listViewGroup55;
			listViewItem612.Group = listViewGroup55;
			listViewItem613.Group = listViewGroup55;
			listViewItem614.Group = listViewGroup55;
			listViewItem615.Group = listViewGroup55;
			listViewItem616.Group = listViewGroup55;
			listViewItem617.Group = listViewGroup55;
			listViewItem618.Group = listViewGroup55;
			listViewItem619.Group = listViewGroup55;
			listViewItem620.Group = listViewGroup55;
			listViewItem621.Group = listViewGroup55;
			listViewItem622.Group = listViewGroup55;
			listViewItem623.Group = listViewGroup55;
			this.lvSettings.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
									listViewItem581,
									listViewItem582,
									listViewItem583,
									listViewItem584,
									listViewItem585,
									listViewItem586,
									listViewItem587,
									listViewItem588,
									listViewItem589,
									listViewItem590,
									listViewItem591,
									listViewItem592,
									listViewItem593,
									listViewItem594,
									listViewItem595,
									listViewItem596,
									listViewItem597,
									listViewItem598,
									listViewItem599,
									listViewItem600,
									listViewItem601,
									listViewItem602,
									listViewItem603,
									listViewItem604,
									listViewItem605,
									listViewItem606,
									listViewItem607,
									listViewItem608,
									listViewItem609,
									listViewItem610,
									listViewItem611,
									listViewItem612,
									listViewItem613,
									listViewItem614,
									listViewItem615,
									listViewItem616,
									listViewItem617,
									listViewItem618,
									listViewItem619,
									listViewItem620,
									listViewItem621,
									listViewItem622,
									listViewItem623});
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
									listViewItem624,
									listViewItem625,
									listViewItem626,
									listViewItem627,
									listViewItem628,
									listViewItem629,
									listViewItem630,
									listViewItem631,
									listViewItem632,
									listViewItem633,
									listViewItem634,
									listViewItem635,
									listViewItem636,
									listViewItem637,
									listViewItem638});
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
			this.cboxMode.FormattingEnabled = true;
			this.cboxMode.Items.AddRange(new object[] {
									"Автор(ы) и Название Книги",
									"Автор(ы),  Название Книги, Жанр(ы)",
									"Id Книги",
									"Id Книги, Автор(ы) и Название Книги",
									"Id Книги, Версия fb2-файла, Автор(ы) и Название Книги",
									"Id Книги, Версия fb2-файла, Название Книги, Автор(ы), Жанр(ы)"});
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
