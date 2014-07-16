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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SFBTpFileManager));
			System.Windows.Forms.ListViewItem listViewItem25 = new System.Windows.Forms.ListViewItem(new string[] {
									"Всего папок",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem26 = new System.Windows.Forms.ListViewItem(new string[] {
									"Всего файлов",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem27 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные fb2-файлы",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem28 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные  Zip-архивы с fb2",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem29 = new System.Windows.Forms.ListViewItem(new string[] {
									"Исходные fb2-файлы из архивов",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem30 = new System.Windows.Forms.ListViewItem(new string[] {
									"Другие файлы",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem31 = new System.Windows.Forms.ListViewItem(new string[] {
									"Создано в папке-приемнике",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem32 = new System.Windows.Forms.ListViewItem(new string[] {
									"Нечитаемые fb2-файлы (архивы)",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem33 = new System.Windows.Forms.ListViewItem(new string[] {
									"Не валидные fb2-файлы (при вкл. опции)",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem34 = new System.Windows.Forms.ListViewItem(new string[] {
									"Битые архивы (не открылись)",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem35 = new System.Windows.Forms.ListViewItem(new string[] {
									"Длинный путь к создаваемому файлу",
									"0"}, -1);
			System.Windows.Forms.ListViewItem listViewItem36 = new System.Windows.Forms.ListViewItem(new string[] {
									"Не удовлетворяющие условиям сортировки",
									"0"}, -1);
			this.ssProgress = new System.Windows.Forms.StatusStrip();
			this.fbdScanDir = new System.Windows.Forms.FolderBrowserDialog();
			this.btnInsertTemplates = new System.Windows.Forms.Button();
			this.txtBoxTemplatesFromLine = new System.Windows.Forms.TextBox();
			this.tcSort = new System.Windows.Forms.TabControl();
			this.tpFullSort = new System.Windows.Forms.TabPage();
			this.listViewSource = new System.Windows.Forms.ListView();
			this.colHeaderFileName = new System.Windows.Forms.ColumnHeader();
			this.colHeaderBookName = new System.Windows.Forms.ColumnHeader();
			this.colHeaderSequence = new System.Windows.Forms.ColumnHeader();
			this.colHeaderFIOBookAuthor = new System.Windows.Forms.ColumnHeader();
			this.colHeaderGenre = new System.Windows.Forms.ColumnHeader();
			this.colHeaderLang = new System.Windows.Forms.ColumnHeader();
			this.colHeaderEncoding = new System.Windows.Forms.ColumnHeader();
			this.cmsItems = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmi3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCheckedAll = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiUnCheckedAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiFilesCheckedAll = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiFilesUnCheckedAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiDirCheckedAll = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiDirUnCheckedAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiFB2CheckedAll = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiFB2UnCheckedAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiZipCheckedAll = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiZipUnCheckedAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCheckedAllSelected = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiUnCheckedAllSelected = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiColumnsExplorerAutoReize = new System.Windows.Forms.ToolStripMenuItem();
			this.imageListItems = new System.Windows.Forms.ImageList(this.components);
			this.panelExplorer = new System.Windows.Forms.Panel();
			this.panelAddress = new System.Windows.Forms.Panel();
			this.buttonGo = new System.Windows.Forms.Button();
			this.textBoxAddress = new System.Windows.Forms.TextBox();
			this.labelAddress = new System.Windows.Forms.Label();
			this.buttonOpenSourceDir = new System.Windows.Forms.Button();
			this.panelTemplate = new System.Windows.Forms.Panel();
			this.buttonFullSortRenew = new System.Windows.Forms.Button();
			this.gBoxFullSortOptions = new System.Windows.Forms.GroupBox();
			this.rbtnFMFSFB22 = new System.Windows.Forms.RadioButton();
			this.chBoxStartExplorerColumnsAutoReize = new System.Windows.Forms.CheckBox();
			this.rbtnFMFSFB2Librusec = new System.Windows.Forms.RadioButton();
			this.checkBoxTagsView = new System.Windows.Forms.CheckBox();
			this.lblFMFSGenres = new System.Windows.Forms.Label();
			this.chBoxScanSubDir = new System.Windows.Forms.CheckBox();
			this.chBoxFSNotDelFB2Files = new System.Windows.Forms.CheckBox();
			this.chBoxFSToZip = new System.Windows.Forms.CheckBox();
			this.gBoxFullSortRenameTemplates = new System.Windows.Forms.GroupBox();
			this.btnGroup = new System.Windows.Forms.Button();
			this.btnGroupGenre = new System.Windows.Forms.Button();
			this.btnLang = new System.Windows.Forms.Button();
			this.btnRightBracket = new System.Windows.Forms.Button();
			this.btnBook = new System.Windows.Forms.Button();
			this.btnFamily = new System.Windows.Forms.Button();
			this.btnLeftBracket = new System.Windows.Forms.Button();
			this.btnGenre = new System.Windows.Forms.Button();
			this.btnSequenceNumber = new System.Windows.Forms.Button();
			this.btnSequence = new System.Windows.Forms.Button();
			this.btnPatronimic = new System.Windows.Forms.Button();
			this.btnName = new System.Windows.Forms.Button();
			this.btnDir = new System.Windows.Forms.Button();
			this.btnLetterFamily = new System.Windows.Forms.Button();
			this.buttonFullSortFilesTo = new System.Windows.Forms.Button();
			this.tpSelectedSort = new System.Windows.Forms.TabPage();
			this.panelLV = new System.Windows.Forms.Panel();
			this.lvSSData = new System.Windows.Forms.ListView();
			this.cHeaderLang = new System.Windows.Forms.ColumnHeader();
			this.cHeaderGenresGroup = new System.Windows.Forms.ColumnHeader();
			this.cHeaderGenre = new System.Windows.Forms.ColumnHeader();
			this.cHeaderLast = new System.Windows.Forms.ColumnHeader();
			this.cHeaderFirst = new System.Windows.Forms.ColumnHeader();
			this.cHeaderMiddle = new System.Windows.Forms.ColumnHeader();
			this.cHeaderNick = new System.Windows.Forms.ColumnHeader();
			this.cHeaderSequence = new System.Windows.Forms.ColumnHeader();
			this.cHeaderBookTitle = new System.Windows.Forms.ColumnHeader();
			this.cHeaderExactFit = new System.Windows.Forms.ColumnHeader();
			this.cHeaderGenreScheme = new System.Windows.Forms.ColumnHeader();
			this.pSSData = new System.Windows.Forms.Panel();
			this.btnSSDataListLoad = new System.Windows.Forms.Button();
			this.btnSSDataListSave = new System.Windows.Forms.Button();
			this.btnSSGetData = new System.Windows.Forms.Button();
			this.pSelectedSortDirs = new System.Windows.Forms.Panel();
			this.btnSSTargetDir = new System.Windows.Forms.Button();
			this.btnSSOpenDir = new System.Windows.Forms.Button();
			this.lblSSTargetDir = new System.Windows.Forms.Label();
			this.tboxSSToDir = new System.Windows.Forms.TextBox();
			this.tboxSSSourceDir = new System.Windows.Forms.TextBox();
			this.lbSSlDir = new System.Windows.Forms.Label();
			this.pSSTemplate = new System.Windows.Forms.Panel();
			this.buttonSSortRenew = new System.Windows.Forms.Button();
			this.buttonSSortFilesTo = new System.Windows.Forms.Button();
			this.gBoxSelectedlSortOptions = new System.Windows.Forms.GroupBox();
			this.chBoxSSScanSubDir = new System.Windows.Forms.CheckBox();
			this.chBoxSSNotDelFB2Files = new System.Windows.Forms.CheckBox();
			this.chBoxSSToZip = new System.Windows.Forms.CheckBox();
			this.gBoxSelectedlSortRenameTemplates = new System.Windows.Forms.GroupBox();
			this.btnSSGroup = new System.Windows.Forms.Button();
			this.btnSSGroupGenre = new System.Windows.Forms.Button();
			this.btnSSLang = new System.Windows.Forms.Button();
			this.btnSSRightBracket = new System.Windows.Forms.Button();
			this.btnSSBook = new System.Windows.Forms.Button();
			this.btnSSFamily = new System.Windows.Forms.Button();
			this.btnSSLeftBracket = new System.Windows.Forms.Button();
			this.btnSSGenre = new System.Windows.Forms.Button();
			this.btnSSSequenceNumber = new System.Windows.Forms.Button();
			this.btnSSSequence = new System.Windows.Forms.Button();
			this.btnSSPatronimic = new System.Windows.Forms.Button();
			this.btnSSName = new System.Windows.Forms.Button();
			this.btnSSDir = new System.Windows.Forms.Button();
			this.btnSSLetterFamily = new System.Windows.Forms.Button();
			this.btnSSInsertTemplates = new System.Windows.Forms.Button();
			this.txtBoxSSTemplatesFromLine = new System.Windows.Forms.TextBox();
			this.tcTemplates = new System.Windows.Forms.TabPage();
			this.rtboxTemplatesList = new System.Windows.Forms.RichTextBox();
			this.tpSettings = new System.Windows.Forms.TabPage();
			this.tcFM = new System.Windows.Forms.TabControl();
			this.tpFMGeneral = new System.Windows.Forms.TabPage();
			this.gboxApportionment = new System.Windows.Forms.GroupBox();
			this.gBoxGenres = new System.Windows.Forms.GroupBox();
			this.gBoxGenresType = new System.Windows.Forms.GroupBox();
			this.rbtnGenreText = new System.Windows.Forms.RadioButton();
			this.rbtnGenreSchema = new System.Windows.Forms.RadioButton();
			this.gBoxGenresCount = new System.Windows.Forms.GroupBox();
			this.rbtnGenreAll = new System.Windows.Forms.RadioButton();
			this.rbtnGenreOne = new System.Windows.Forms.RadioButton();
			this.gBoxAuthors = new System.Windows.Forms.GroupBox();
			this.rbtnAuthorAll = new System.Windows.Forms.RadioButton();
			this.rbtnAuthorOne = new System.Windows.Forms.RadioButton();
			this.gboxFMGeneral = new System.Windows.Forms.GroupBox();
			this.pSortFB2 = new System.Windows.Forms.Panel();
			this.rbtnFMOnlyValidFB2 = new System.Windows.Forms.RadioButton();
			this.rbtnFMAllFB2 = new System.Windows.Forms.RadioButton();
			this.label11 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.cboxFileExist = new System.Windows.Forms.ComboBox();
			this.lbFilelExist = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.cboxSpace = new System.Windows.Forms.ComboBox();
			this.lblSpace = new System.Windows.Forms.Label();
			this.chBoxStrict = new System.Windows.Forms.CheckBox();
			this.chBoxTranslit = new System.Windows.Forms.CheckBox();
			this.gboxRegister = new System.Windows.Forms.GroupBox();
			this.rbtnAsSentence = new System.Windows.Forms.RadioButton();
			this.rbtnUpper = new System.Windows.Forms.RadioButton();
			this.rbtnLower = new System.Windows.Forms.RadioButton();
			this.rbtnAsIs = new System.Windows.Forms.RadioButton();
			this.tpFMNoTagsText = new System.Windows.Forms.TabPage();
			this.tcDesc = new System.Windows.Forms.TabControl();
			this.tpBookInfo = new System.Windows.Forms.TabPage();
			this.gBoxFMBINoTags = new System.Windows.Forms.GroupBox();
			this.panel30 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoDateValue = new System.Windows.Forms.TextBox();
			this.label31 = new System.Windows.Forms.Label();
			this.panel29 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoDateText = new System.Windows.Forms.TextBox();
			this.label30 = new System.Windows.Forms.Label();
			this.panel12 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoNSequence = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.panel11 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoSequence = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.panel10 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoBookTitle = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.panel9 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoNickName = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.panel8 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoLastName = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.panel7 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoMiddleName = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.panel6 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoFirstName = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.panel5 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoLang = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.panel4 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoGenre = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.panel13 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoGenreGroup = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tpPublishInfo = new System.Windows.Forms.TabPage();
			this.gBoxFMPINoTags = new System.Windows.Forms.GroupBox();
			this.panel33 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoCity = new System.Windows.Forms.TextBox();
			this.label34 = new System.Windows.Forms.Label();
			this.panel32 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoPublisher = new System.Windows.Forms.TextBox();
			this.label33 = new System.Windows.Forms.Label();
			this.panel31 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoYear = new System.Windows.Forms.TextBox();
			this.label32 = new System.Windows.Forms.Label();
			this.tpFB2Info = new System.Windows.Forms.TabPage();
			this.gBoxFMFB2INoTags = new System.Windows.Forms.GroupBox();
			this.panel34 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoFB2NickName = new System.Windows.Forms.TextBox();
			this.label35 = new System.Windows.Forms.Label();
			this.panel35 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoFB2LastName = new System.Windows.Forms.TextBox();
			this.label36 = new System.Windows.Forms.Label();
			this.panel36 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoFB2MiddleName = new System.Windows.Forms.TextBox();
			this.label37 = new System.Windows.Forms.Label();
			this.panel37 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoFB2FirstName = new System.Windows.Forms.TextBox();
			this.label38 = new System.Windows.Forms.Label();
			this.tpFMGenreGroups = new System.Windows.Forms.TabPage();
			this.panel41 = new System.Windows.Forms.Panel();
			this.txtboxFMother = new System.Windows.Forms.TextBox();
			this.label40 = new System.Windows.Forms.Label();
			this.panel40 = new System.Windows.Forms.Panel();
			this.txtboxFMfolklore = new System.Windows.Forms.TextBox();
			this.label39 = new System.Windows.Forms.Label();
			this.panel39 = new System.Windows.Forms.Panel();
			this.txtboxFMmilitary = new System.Windows.Forms.TextBox();
			this.label13 = new System.Windows.Forms.Label();
			this.panel38 = new System.Windows.Forms.Panel();
			this.txtboxFMtech = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.panel28 = new System.Windows.Forms.Panel();
			this.txtboxFMbusiness = new System.Windows.Forms.TextBox();
			this.label29 = new System.Windows.Forms.Label();
			this.panel27 = new System.Windows.Forms.Panel();
			this.txtboxFMhome = new System.Windows.Forms.TextBox();
			this.label28 = new System.Windows.Forms.Label();
			this.panel26 = new System.Windows.Forms.Panel();
			this.txtboxFMhumor = new System.Windows.Forms.TextBox();
			this.label27 = new System.Windows.Forms.Label();
			this.panel25 = new System.Windows.Forms.Panel();
			this.txtboxFMreligion = new System.Windows.Forms.TextBox();
			this.label26 = new System.Windows.Forms.Label();
			this.panel24 = new System.Windows.Forms.Panel();
			this.txtboxFMnonfiction = new System.Windows.Forms.TextBox();
			this.label25 = new System.Windows.Forms.Label();
			this.panel23 = new System.Windows.Forms.Panel();
			this.txtboxFMreference = new System.Windows.Forms.TextBox();
			this.label24 = new System.Windows.Forms.Label();
			this.panel22 = new System.Windows.Forms.Panel();
			this.txtboxFMcomputers = new System.Windows.Forms.TextBox();
			this.label23 = new System.Windows.Forms.Label();
			this.panel21 = new System.Windows.Forms.Panel();
			this.txtboxFMscience = new System.Windows.Forms.TextBox();
			this.label22 = new System.Windows.Forms.Label();
			this.panel20 = new System.Windows.Forms.Panel();
			this.txtboxFMantique = new System.Windows.Forms.TextBox();
			this.label21 = new System.Windows.Forms.Label();
			this.panel19 = new System.Windows.Forms.Panel();
			this.txtboxFMpoetry = new System.Windows.Forms.TextBox();
			this.label20 = new System.Windows.Forms.Label();
			this.panel18 = new System.Windows.Forms.Panel();
			this.txtboxFMchildren = new System.Windows.Forms.TextBox();
			this.label19 = new System.Windows.Forms.Label();
			this.panel17 = new System.Windows.Forms.Panel();
			this.txtboxFMadventure = new System.Windows.Forms.TextBox();
			this.label18 = new System.Windows.Forms.Label();
			this.panel16 = new System.Windows.Forms.Panel();
			this.txtboxFMlove = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.panel15 = new System.Windows.Forms.Panel();
			this.txtboxFMprose = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.panel14 = new System.Windows.Forms.Panel();
			this.txtboxFMdetective = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.panel42 = new System.Windows.Forms.Panel();
			this.txtboxFMsf = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnDefRestore = new System.Windows.Forms.Button();
			this.sfdSaveXMLFile = new System.Windows.Forms.SaveFileDialog();
			this.sfdOpenXMLFile = new System.Windows.Forms.OpenFileDialog();
			this.sfdLoadList = new System.Windows.Forms.OpenFileDialog();
			this.panelProgress = new System.Windows.Forms.Panel();
			this.lvFilesCount = new System.Windows.Forms.ListView();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.chBoxViewProgress = new System.Windows.Forms.CheckBox();
			this.labelTarget = new System.Windows.Forms.Label();
			this.labelTargetPath = new System.Windows.Forms.Label();
			this.tcSort.SuspendLayout();
			this.tpFullSort.SuspendLayout();
			this.cmsItems.SuspendLayout();
			this.panelExplorer.SuspendLayout();
			this.panelAddress.SuspendLayout();
			this.panelTemplate.SuspendLayout();
			this.gBoxFullSortOptions.SuspendLayout();
			this.gBoxFullSortRenameTemplates.SuspendLayout();
			this.tpSelectedSort.SuspendLayout();
			this.panelLV.SuspendLayout();
			this.pSSData.SuspendLayout();
			this.pSelectedSortDirs.SuspendLayout();
			this.pSSTemplate.SuspendLayout();
			this.gBoxSelectedlSortOptions.SuspendLayout();
			this.gBoxSelectedlSortRenameTemplates.SuspendLayout();
			this.tcTemplates.SuspendLayout();
			this.tpSettings.SuspendLayout();
			this.tcFM.SuspendLayout();
			this.tpFMGeneral.SuspendLayout();
			this.gboxApportionment.SuspendLayout();
			this.gBoxGenres.SuspendLayout();
			this.gBoxGenresType.SuspendLayout();
			this.gBoxGenresCount.SuspendLayout();
			this.gBoxAuthors.SuspendLayout();
			this.gboxFMGeneral.SuspendLayout();
			this.pSortFB2.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.gboxRegister.SuspendLayout();
			this.tpFMNoTagsText.SuspendLayout();
			this.tcDesc.SuspendLayout();
			this.tpBookInfo.SuspendLayout();
			this.gBoxFMBINoTags.SuspendLayout();
			this.panel30.SuspendLayout();
			this.panel29.SuspendLayout();
			this.panel12.SuspendLayout();
			this.panel11.SuspendLayout();
			this.panel10.SuspendLayout();
			this.panel9.SuspendLayout();
			this.panel8.SuspendLayout();
			this.panel7.SuspendLayout();
			this.panel6.SuspendLayout();
			this.panel5.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel13.SuspendLayout();
			this.tpPublishInfo.SuspendLayout();
			this.gBoxFMPINoTags.SuspendLayout();
			this.panel33.SuspendLayout();
			this.panel32.SuspendLayout();
			this.panel31.SuspendLayout();
			this.tpFB2Info.SuspendLayout();
			this.gBoxFMFB2INoTags.SuspendLayout();
			this.panel34.SuspendLayout();
			this.panel35.SuspendLayout();
			this.panel36.SuspendLayout();
			this.panel37.SuspendLayout();
			this.tpFMGenreGroups.SuspendLayout();
			this.panel41.SuspendLayout();
			this.panel40.SuspendLayout();
			this.panel39.SuspendLayout();
			this.panel38.SuspendLayout();
			this.panel28.SuspendLayout();
			this.panel27.SuspendLayout();
			this.panel26.SuspendLayout();
			this.panel25.SuspendLayout();
			this.panel24.SuspendLayout();
			this.panel23.SuspendLayout();
			this.panel22.SuspendLayout();
			this.panel21.SuspendLayout();
			this.panel20.SuspendLayout();
			this.panel19.SuspendLayout();
			this.panel18.SuspendLayout();
			this.panel17.SuspendLayout();
			this.panel16.SuspendLayout();
			this.panel15.SuspendLayout();
			this.panel14.SuspendLayout();
			this.panel42.SuspendLayout();
			this.panel1.SuspendLayout();
			this.panelProgress.SuspendLayout();
			this.SuspendLayout();
			// 
			// ssProgress
			// 
			this.ssProgress.Location = new System.Drawing.Point(0, 628);
			this.ssProgress.Name = "ssProgress";
			this.ssProgress.Size = new System.Drawing.Size(1256, 22);
			this.ssProgress.TabIndex = 18;
			this.ssProgress.Text = "statusStrip1";
			// 
			// fbdScanDir
			// 
			this.fbdScanDir.Description = "Укажите папку для сканирования с fb2-файлами и архивами";
			// 
			// btnInsertTemplates
			// 
			this.btnInsertTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnInsertTemplates.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnInsertTemplates.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnInsertTemplates.Location = new System.Drawing.Point(477, 17);
			this.btnInsertTemplates.Name = "btnInsertTemplates";
			this.btnInsertTemplates.Size = new System.Drawing.Size(120, 28);
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
			this.txtBoxTemplatesFromLine.Size = new System.Drawing.Size(465, 20);
			this.txtBoxTemplatesFromLine.TabIndex = 8;
			this.txtBoxTemplatesFromLine.TextChanged += new System.EventHandler(this.TxtBoxTemplatesFromLineTextChanged);
			// 
			// tcSort
			// 
			this.tcSort.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tcSort.Controls.Add(this.tpFullSort);
			this.tcSort.Controls.Add(this.tpSelectedSort);
			this.tcSort.Controls.Add(this.tcTemplates);
			this.tcSort.Controls.Add(this.tpSettings);
			this.tcSort.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.tcSort.ImageList = this.imageListItems;
			this.tcSort.Location = new System.Drawing.Point(0, 0);
			this.tcSort.Name = "tcSort";
			this.tcSort.SelectedIndex = 0;
			this.tcSort.Size = new System.Drawing.Size(919, 628);
			this.tcSort.TabIndex = 31;
			// 
			// tpFullSort
			// 
			this.tpFullSort.Controls.Add(this.listViewSource);
			this.tpFullSort.Controls.Add(this.panelExplorer);
			this.tpFullSort.Controls.Add(this.panelTemplate);
			this.tpFullSort.Font = new System.Drawing.Font("Tahoma", 8F);
			this.tpFullSort.Location = new System.Drawing.Point(4, 23);
			this.tpFullSort.Name = "tpFullSort";
			this.tpFullSort.Padding = new System.Windows.Forms.Padding(3);
			this.tpFullSort.Size = new System.Drawing.Size(911, 601);
			this.tpFullSort.TabIndex = 0;
			this.tpFullSort.Text = " Полная Сортировка ";
			this.tpFullSort.UseVisualStyleBackColor = true;
			// 
			// listViewSource
			// 
			this.listViewSource.AllowColumnReorder = true;
			this.listViewSource.CheckBoxes = true;
			this.listViewSource.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.colHeaderFileName,
									this.colHeaderBookName,
									this.colHeaderSequence,
									this.colHeaderFIOBookAuthor,
									this.colHeaderGenre,
									this.colHeaderLang,
									this.colHeaderEncoding});
			this.listViewSource.ContextMenuStrip = this.cmsItems;
			this.listViewSource.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewSource.FullRowSelect = true;
			this.listViewSource.GridLines = true;
			this.listViewSource.Location = new System.Drawing.Point(3, 237);
			this.listViewSource.Name = "listViewSource";
			this.listViewSource.ShowItemToolTips = true;
			this.listViewSource.Size = new System.Drawing.Size(905, 361);
			this.listViewSource.SmallImageList = this.imageListItems;
			this.listViewSource.TabIndex = 35;
			this.listViewSource.UseCompatibleStateImageBehavior = false;
			this.listViewSource.View = System.Windows.Forms.View.Details;
			this.listViewSource.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.ListViewSourceItemChecked);
			this.listViewSource.DoubleClick += new System.EventHandler(this.ListViewSourceDoubleClick);
			this.listViewSource.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ListViewSourceItemCheck);
			this.listViewSource.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ListViewSourceKeyPress);
			// 
			// colHeaderFileName
			// 
			this.colHeaderFileName.Text = "Имя файла";
			// 
			// colHeaderBookName
			// 
			this.colHeaderBookName.Text = "Книга";
			// 
			// colHeaderSequence
			// 
			this.colHeaderSequence.Text = "Серия (№)";
			// 
			// colHeaderFIOBookAuthor
			// 
			this.colHeaderFIOBookAuthor.Text = "Автор(ы)";
			// 
			// colHeaderGenre
			// 
			this.colHeaderGenre.Text = "Жанр(ы)";
			// 
			// colHeaderLang
			// 
			this.colHeaderLang.Text = "Язык";
			// 
			// colHeaderEncoding
			// 
			this.colHeaderEncoding.Text = "Кодировка";
			// 
			// cmsItems
			// 
			this.cmsItems.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsmi3,
									this.tsmiCheckedAll,
									this.tsmiUnCheckedAll,
									this.toolStripMenuItem1,
									this.tsmiFilesCheckedAll,
									this.tsmiFilesUnCheckedAll,
									this.toolStripMenuItem2,
									this.tsmiDirCheckedAll,
									this.tsmiDirUnCheckedAll,
									this.toolStripMenuItem3,
									this.tsmiFB2CheckedAll,
									this.tsmiFB2UnCheckedAll,
									this.toolStripMenuItem4,
									this.tsmiZipCheckedAll,
									this.tsmiZipUnCheckedAll,
									this.toolStripMenuItem5,
									this.tsmiCheckedAllSelected,
									this.tsmiUnCheckedAllSelected,
									this.toolStripMenuItem6,
									this.tsmiColumnsExplorerAutoReize});
			this.cmsItems.Name = "cmsValidator";
			this.cmsItems.Size = new System.Drawing.Size(308, 332);
			// 
			// tsmi3
			// 
			this.tsmi3.Name = "tsmi3";
			this.tsmi3.Size = new System.Drawing.Size(304, 6);
			// 
			// tsmiCheckedAll
			// 
			this.tsmiCheckedAll.Image = ((System.Drawing.Image)(resources.GetObject("tsmiCheckedAll.Image")));
			this.tsmiCheckedAll.Name = "tsmiCheckedAll";
			this.tsmiCheckedAll.Size = new System.Drawing.Size(307, 22);
			this.tsmiCheckedAll.Text = "Пометить все файлы и папки";
			this.tsmiCheckedAll.Click += new System.EventHandler(this.TsmiCheckedAllClick);
			// 
			// tsmiUnCheckedAll
			// 
			this.tsmiUnCheckedAll.Image = ((System.Drawing.Image)(resources.GetObject("tsmiUnCheckedAll.Image")));
			this.tsmiUnCheckedAll.Name = "tsmiUnCheckedAll";
			this.tsmiUnCheckedAll.Size = new System.Drawing.Size(307, 22);
			this.tsmiUnCheckedAll.Text = "Снять пометки со всех файлов и папок";
			this.tsmiUnCheckedAll.Click += new System.EventHandler(this.TsmiUnCheckedAllClick);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(304, 6);
			// 
			// tsmiFilesCheckedAll
			// 
			this.tsmiFilesCheckedAll.Name = "tsmiFilesCheckedAll";
			this.tsmiFilesCheckedAll.Size = new System.Drawing.Size(307, 22);
			this.tsmiFilesCheckedAll.Text = "Пометить все файлы";
			this.tsmiFilesCheckedAll.Click += new System.EventHandler(this.TsmiFilesCheckedAllClick);
			// 
			// tsmiFilesUnCheckedAll
			// 
			this.tsmiFilesUnCheckedAll.Name = "tsmiFilesUnCheckedAll";
			this.tsmiFilesUnCheckedAll.Size = new System.Drawing.Size(307, 22);
			this.tsmiFilesUnCheckedAll.Text = "Снять пометки со всех файлов";
			this.tsmiFilesUnCheckedAll.Click += new System.EventHandler(this.TsmiFilesUnCheckedAllClick);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(304, 6);
			// 
			// tsmiDirCheckedAll
			// 
			this.tsmiDirCheckedAll.Name = "tsmiDirCheckedAll";
			this.tsmiDirCheckedAll.Size = new System.Drawing.Size(307, 22);
			this.tsmiDirCheckedAll.Text = "Пометить все папки";
			this.tsmiDirCheckedAll.Click += new System.EventHandler(this.TsmiDirCheckedAllClick);
			// 
			// tsmiDirUnCheckedAll
			// 
			this.tsmiDirUnCheckedAll.Name = "tsmiDirUnCheckedAll";
			this.tsmiDirUnCheckedAll.Size = new System.Drawing.Size(307, 22);
			this.tsmiDirUnCheckedAll.Text = "Снять пометки со всех папок";
			this.tsmiDirUnCheckedAll.Click += new System.EventHandler(this.TsmiDirUnCheckedAllClick);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(304, 6);
			// 
			// tsmiFB2CheckedAll
			// 
			this.tsmiFB2CheckedAll.Name = "tsmiFB2CheckedAll";
			this.tsmiFB2CheckedAll.Size = new System.Drawing.Size(307, 22);
			this.tsmiFB2CheckedAll.Text = "Пометить все fb2 файлы";
			this.tsmiFB2CheckedAll.Click += new System.EventHandler(this.TsmiFB2CheckedAllClick);
			// 
			// tsmiFB2UnCheckedAll
			// 
			this.tsmiFB2UnCheckedAll.Name = "tsmiFB2UnCheckedAll";
			this.tsmiFB2UnCheckedAll.Size = new System.Drawing.Size(307, 22);
			this.tsmiFB2UnCheckedAll.Text = "Снять пометки со всех fb2 файлов";
			this.tsmiFB2UnCheckedAll.Click += new System.EventHandler(this.TsmiFB2UnCheckedAllClick);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(304, 6);
			// 
			// tsmiZipCheckedAll
			// 
			this.tsmiZipCheckedAll.Name = "tsmiZipCheckedAll";
			this.tsmiZipCheckedAll.Size = new System.Drawing.Size(307, 22);
			this.tsmiZipCheckedAll.Text = "Пометить все zip файлы";
			this.tsmiZipCheckedAll.Click += new System.EventHandler(this.TsmiZipCheckedAllClick);
			// 
			// tsmiZipUnCheckedAll
			// 
			this.tsmiZipUnCheckedAll.Name = "tsmiZipUnCheckedAll";
			this.tsmiZipUnCheckedAll.Size = new System.Drawing.Size(307, 22);
			this.tsmiZipUnCheckedAll.Text = "Снять пометки со всех zip файлов";
			this.tsmiZipUnCheckedAll.Click += new System.EventHandler(this.TsmiZipUnCheckedAllClick);
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(304, 6);
			// 
			// tsmiCheckedAllSelected
			// 
			this.tsmiCheckedAllSelected.Name = "tsmiCheckedAllSelected";
			this.tsmiCheckedAllSelected.Size = new System.Drawing.Size(307, 22);
			this.tsmiCheckedAllSelected.Text = "Пометить всё выделенное";
			this.tsmiCheckedAllSelected.Click += new System.EventHandler(this.TsmiCheckedAllSelectedClick);
			// 
			// tsmiUnCheckedAllSelected
			// 
			this.tsmiUnCheckedAllSelected.Name = "tsmiUnCheckedAllSelected";
			this.tsmiUnCheckedAllSelected.Size = new System.Drawing.Size(307, 22);
			this.tsmiUnCheckedAllSelected.Text = "Снять пометки со всего выделенного";
			this.tsmiUnCheckedAllSelected.Click += new System.EventHandler(this.TsmiUnCheckedAllSelectedClick);
			// 
			// toolStripMenuItem6
			// 
			this.toolStripMenuItem6.Name = "toolStripMenuItem6";
			this.toolStripMenuItem6.Size = new System.Drawing.Size(304, 6);
			// 
			// tsmiColumnsExplorerAutoReize
			// 
			this.tsmiColumnsExplorerAutoReize.Name = "tsmiColumnsExplorerAutoReize";
			this.tsmiColumnsExplorerAutoReize.Size = new System.Drawing.Size(307, 22);
			this.tsmiColumnsExplorerAutoReize.Text = "Обновить авторазмер колонок Проводника";
			this.tsmiColumnsExplorerAutoReize.Click += new System.EventHandler(this.TsmiColumnsExplorerAutoReizeClick);
			// 
			// imageListItems
			// 
			this.imageListItems.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListItems.ImageStream")));
			this.imageListItems.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListItems.Images.SetKeyName(0, "0_folder.png");
			this.imageListItems.Images.SetKeyName(1, "1_fb2.png");
			this.imageListItems.Images.SetKeyName(2, "2_zip.png");
			this.imageListItems.Images.SetKeyName(3, "3_up.png");
			this.imageListItems.Images.SetKeyName(4, "options.png");
			// 
			// panelExplorer
			// 
			this.panelExplorer.Controls.Add(this.panelAddress);
			this.panelExplorer.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelExplorer.Location = new System.Drawing.Point(3, 183);
			this.panelExplorer.Name = "panelExplorer";
			this.panelExplorer.Size = new System.Drawing.Size(905, 54);
			this.panelExplorer.TabIndex = 37;
			// 
			// panelAddress
			// 
			this.panelAddress.Controls.Add(this.labelTargetPath);
			this.panelAddress.Controls.Add(this.labelTarget);
			this.panelAddress.Controls.Add(this.buttonGo);
			this.panelAddress.Controls.Add(this.textBoxAddress);
			this.panelAddress.Controls.Add(this.labelAddress);
			this.panelAddress.Controls.Add(this.buttonOpenSourceDir);
			this.panelAddress.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelAddress.Location = new System.Drawing.Point(0, 0);
			this.panelAddress.Name = "panelAddress";
			this.panelAddress.Size = new System.Drawing.Size(905, 52);
			this.panelAddress.TabIndex = 38;
			// 
			// buttonGo
			// 
			this.buttonGo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.buttonGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonGo.Location = new System.Drawing.Point(756, 4);
			this.buttonGo.Name = "buttonGo";
			this.buttonGo.Size = new System.Drawing.Size(142, 26);
			this.buttonGo.TabIndex = 6;
			this.buttonGo.Text = "Перейти/Обновить";
			this.buttonGo.UseVisualStyleBackColor = true;
			this.buttonGo.Click += new System.EventHandler(this.ButtonGoClick);
			// 
			// textBoxAddress
			// 
			this.textBoxAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBoxAddress.Location = new System.Drawing.Point(121, 5);
			this.textBoxAddress.Name = "textBoxAddress";
			this.textBoxAddress.Size = new System.Drawing.Size(629, 20);
			this.textBoxAddress.TabIndex = 5;
			this.textBoxAddress.TextChanged += new System.EventHandler(this.TextBoxAddressTextChanged);
			this.textBoxAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxAddressKeyPress);
			// 
			// labelAddress
			// 
			this.labelAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelAddress.Location = new System.Drawing.Point(3, 7);
			this.labelAddress.Name = "labelAddress";
			this.labelAddress.Size = new System.Drawing.Size(47, 19);
			this.labelAddress.TabIndex = 4;
			this.labelAddress.Text = "Адрес:";
			this.labelAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// buttonOpenSourceDir
			// 
			this.buttonOpenSourceDir.Image = ((System.Drawing.Image)(resources.GetObject("buttonOpenSourceDir.Image")));
			this.buttonOpenSourceDir.Location = new System.Drawing.Point(80, 3);
			this.buttonOpenSourceDir.Name = "buttonOpenSourceDir";
			this.buttonOpenSourceDir.Size = new System.Drawing.Size(31, 27);
			this.buttonOpenSourceDir.TabIndex = 7;
			this.buttonOpenSourceDir.UseVisualStyleBackColor = true;
			this.buttonOpenSourceDir.Click += new System.EventHandler(this.ButtonOpenSourceDirClick);
			// 
			// panelTemplate
			// 
			this.panelTemplate.Controls.Add(this.buttonFullSortRenew);
			this.panelTemplate.Controls.Add(this.gBoxFullSortOptions);
			this.panelTemplate.Controls.Add(this.gBoxFullSortRenameTemplates);
			this.panelTemplate.Controls.Add(this.buttonFullSortFilesTo);
			this.panelTemplate.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelTemplate.Location = new System.Drawing.Point(3, 3);
			this.panelTemplate.Name = "panelTemplate";
			this.panelTemplate.Size = new System.Drawing.Size(905, 180);
			this.panelTemplate.TabIndex = 34;
			// 
			// buttonFullSortRenew
			// 
			this.buttonFullSortRenew.Font = new System.Drawing.Font("Tahoma", 11F);
			this.buttonFullSortRenew.Image = ((System.Drawing.Image)(resources.GetObject("buttonFullSortRenew.Image")));
			this.buttonFullSortRenew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonFullSortRenew.Location = new System.Drawing.Point(170, 5);
			this.buttonFullSortRenew.Name = "buttonFullSortRenew";
			this.buttonFullSortRenew.Size = new System.Drawing.Size(229, 44);
			this.buttonFullSortRenew.TabIndex = 34;
			this.buttonFullSortRenew.Text = "Возобновить из файла...";
			this.buttonFullSortRenew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.buttonFullSortRenew.UseVisualStyleBackColor = true;
			this.buttonFullSortRenew.Click += new System.EventHandler(this.ButtonFullSortRenewClick);
			// 
			// gBoxFullSortOptions
			// 
			this.gBoxFullSortOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.gBoxFullSortOptions.Controls.Add(this.rbtnFMFSFB22);
			this.gBoxFullSortOptions.Controls.Add(this.chBoxStartExplorerColumnsAutoReize);
			this.gBoxFullSortOptions.Controls.Add(this.rbtnFMFSFB2Librusec);
			this.gBoxFullSortOptions.Controls.Add(this.checkBoxTagsView);
			this.gBoxFullSortOptions.Controls.Add(this.lblFMFSGenres);
			this.gBoxFullSortOptions.Controls.Add(this.chBoxScanSubDir);
			this.gBoxFullSortOptions.Controls.Add(this.chBoxFSNotDelFB2Files);
			this.gBoxFullSortOptions.Controls.Add(this.chBoxFSToZip);
			this.gBoxFullSortOptions.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gBoxFullSortOptions.Location = new System.Drawing.Point(618, 5);
			this.gBoxFullSortOptions.Name = "gBoxFullSortOptions";
			this.gBoxFullSortOptions.Size = new System.Drawing.Size(280, 167);
			this.gBoxFullSortOptions.TabIndex = 33;
			this.gBoxFullSortOptions.TabStop = false;
			this.gBoxFullSortOptions.Text = " Настройки ";
			// 
			// rbtnFMFSFB22
			// 
			this.rbtnFMFSFB22.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnFMFSFB22.Location = new System.Drawing.Point(212, 143);
			this.rbtnFMFSFB22.Name = "rbtnFMFSFB22";
			this.rbtnFMFSFB22.Size = new System.Drawing.Size(54, 17);
			this.rbtnFMFSFB22.TabIndex = 6;
			this.rbtnFMFSFB22.Text = "fb2.2";
			this.rbtnFMFSFB22.UseVisualStyleBackColor = true;
			this.rbtnFMFSFB22.Click += new System.EventHandler(this.RbtnFMFSFB22Click);
			// 
			// chBoxStartExplorerColumnsAutoReize
			// 
			this.chBoxStartExplorerColumnsAutoReize.Checked = true;
			this.chBoxStartExplorerColumnsAutoReize.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chBoxStartExplorerColumnsAutoReize.Font = new System.Drawing.Font("Tahoma", 8F);
			this.chBoxStartExplorerColumnsAutoReize.Location = new System.Drawing.Point(6, 75);
			this.chBoxStartExplorerColumnsAutoReize.Name = "chBoxStartExplorerColumnsAutoReize";
			this.chBoxStartExplorerColumnsAutoReize.Size = new System.Drawing.Size(269, 24);
			this.chBoxStartExplorerColumnsAutoReize.TabIndex = 7;
			this.chBoxStartExplorerColumnsAutoReize.Text = "Авторазмер колонок Проводника";
			this.chBoxStartExplorerColumnsAutoReize.UseVisualStyleBackColor = true;
			this.chBoxStartExplorerColumnsAutoReize.CheckedChanged += new System.EventHandler(this.ChBoxStartExplorerColumnsAutoReizeCheckedChanged);
			// 
			// rbtnFMFSFB2Librusec
			// 
			this.rbtnFMFSFB2Librusec.Checked = true;
			this.rbtnFMFSFB2Librusec.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnFMFSFB2Librusec.Location = new System.Drawing.Point(101, 143);
			this.rbtnFMFSFB2Librusec.Name = "rbtnFMFSFB2Librusec";
			this.rbtnFMFSFB2Librusec.Size = new System.Drawing.Size(110, 17);
			this.rbtnFMFSFB2Librusec.TabIndex = 5;
			this.rbtnFMFSFB2Librusec.TabStop = true;
			this.rbtnFMFSFB2Librusec.Text = "fb2 Либрусек";
			this.rbtnFMFSFB2Librusec.UseVisualStyleBackColor = true;
			this.rbtnFMFSFB2Librusec.Click += new System.EventHandler(this.RbtnFMFSFB2LibrusecClick);
			// 
			// checkBoxTagsView
			// 
			this.checkBoxTagsView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBoxTagsView.Font = new System.Drawing.Font("Tahoma", 8F);
			this.checkBoxTagsView.Location = new System.Drawing.Point(6, 94);
			this.checkBoxTagsView.Name = "checkBoxTagsView";
			this.checkBoxTagsView.Size = new System.Drawing.Size(268, 24);
			this.checkBoxTagsView.TabIndex = 5;
			this.checkBoxTagsView.Text = "Показывать описание книг в Проводнике";
			this.checkBoxTagsView.UseVisualStyleBackColor = true;
			this.checkBoxTagsView.Click += new System.EventHandler(this.CheckBoxTagsViewClick);
			// 
			// lblFMFSGenres
			// 
			this.lblFMFSGenres.ForeColor = System.Drawing.Color.Navy;
			this.lblFMFSGenres.Location = new System.Drawing.Point(4, 144);
			this.lblFMFSGenres.Name = "lblFMFSGenres";
			this.lblFMFSGenres.Size = new System.Drawing.Size(100, 16);
			this.lblFMFSGenres.TabIndex = 4;
			this.lblFMFSGenres.Text = "Схема Жанров:";
			// 
			// chBoxScanSubDir
			// 
			this.chBoxScanSubDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chBoxScanSubDir.Checked = true;
			this.chBoxScanSubDir.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chBoxScanSubDir.Font = new System.Drawing.Font("Tahoma", 8F);
			this.chBoxScanSubDir.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chBoxScanSubDir.Location = new System.Drawing.Point(6, 16);
			this.chBoxScanSubDir.Name = "chBoxScanSubDir";
			this.chBoxScanSubDir.Size = new System.Drawing.Size(268, 24);
			this.chBoxScanSubDir.TabIndex = 4;
			this.chBoxScanSubDir.Text = "Обрабатывать подкаталоги";
			this.chBoxScanSubDir.UseVisualStyleBackColor = true;
			this.chBoxScanSubDir.Click += new System.EventHandler(this.ChBoxScanSubDirClick);
			// 
			// chBoxFSNotDelFB2Files
			// 
			this.chBoxFSNotDelFB2Files.Checked = true;
			this.chBoxFSNotDelFB2Files.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chBoxFSNotDelFB2Files.Font = new System.Drawing.Font("Tahoma", 8F);
			this.chBoxFSNotDelFB2Files.Location = new System.Drawing.Point(6, 54);
			this.chBoxFSNotDelFB2Files.Name = "chBoxFSNotDelFB2Files";
			this.chBoxFSNotDelFB2Files.Size = new System.Drawing.Size(269, 24);
			this.chBoxFSNotDelFB2Files.TabIndex = 6;
			this.chBoxFSNotDelFB2Files.Text = "Сохранять оригиналы";
			this.chBoxFSNotDelFB2Files.UseVisualStyleBackColor = true;
			this.chBoxFSNotDelFB2Files.Click += new System.EventHandler(this.ChBoxFSNotDelFB2FilesClick);
			// 
			// chBoxFSToZip
			// 
			this.chBoxFSToZip.Font = new System.Drawing.Font("Tahoma", 8F);
			this.chBoxFSToZip.Location = new System.Drawing.Point(6, 35);
			this.chBoxFSToZip.Name = "chBoxFSToZip";
			this.chBoxFSToZip.Size = new System.Drawing.Size(268, 24);
			this.chBoxFSToZip.TabIndex = 5;
			this.chBoxFSToZip.Text = "Архивировать в zip";
			this.chBoxFSToZip.UseVisualStyleBackColor = true;
			this.chBoxFSToZip.Click += new System.EventHandler(this.ChBoxFSToZipClick);
			// 
			// gBoxFullSortRenameTemplates
			// 
			this.gBoxFullSortRenameTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.gBoxFullSortRenameTemplates.Controls.Add(this.btnGroup);
			this.gBoxFullSortRenameTemplates.Controls.Add(this.btnGroupGenre);
			this.gBoxFullSortRenameTemplates.Controls.Add(this.btnLang);
			this.gBoxFullSortRenameTemplates.Controls.Add(this.btnRightBracket);
			this.gBoxFullSortRenameTemplates.Controls.Add(this.btnBook);
			this.gBoxFullSortRenameTemplates.Controls.Add(this.btnFamily);
			this.gBoxFullSortRenameTemplates.Controls.Add(this.btnLeftBracket);
			this.gBoxFullSortRenameTemplates.Controls.Add(this.btnGenre);
			this.gBoxFullSortRenameTemplates.Controls.Add(this.btnSequenceNumber);
			this.gBoxFullSortRenameTemplates.Controls.Add(this.btnSequence);
			this.gBoxFullSortRenameTemplates.Controls.Add(this.btnPatronimic);
			this.gBoxFullSortRenameTemplates.Controls.Add(this.btnName);
			this.gBoxFullSortRenameTemplates.Controls.Add(this.btnDir);
			this.gBoxFullSortRenameTemplates.Controls.Add(this.btnLetterFamily);
			this.gBoxFullSortRenameTemplates.Controls.Add(this.btnInsertTemplates);
			this.gBoxFullSortRenameTemplates.Controls.Add(this.txtBoxTemplatesFromLine);
			this.gBoxFullSortRenameTemplates.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gBoxFullSortRenameTemplates.Location = new System.Drawing.Point(3, 55);
			this.gBoxFullSortRenameTemplates.Name = "gBoxFullSortRenameTemplates";
			this.gBoxFullSortRenameTemplates.Size = new System.Drawing.Size(607, 117);
			this.gBoxFullSortRenameTemplates.TabIndex = 32;
			this.gBoxFullSortRenameTemplates.TabStop = false;
			this.gBoxFullSortRenameTemplates.Text = " Шаблоны подстановки ";
			// 
			// btnGroup
			// 
			this.btnGroup.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnGroup.Location = new System.Drawing.Point(7, 82);
			this.btnGroup.Name = "btnGroup";
			this.btnGroup.Size = new System.Drawing.Size(80, 23);
			this.btnGroup.TabIndex = 23;
			this.btnGroup.Text = "Группа";
			this.btnGroup.UseVisualStyleBackColor = true;
			this.btnGroup.Click += new System.EventHandler(this.BtnGroupClick);
			// 
			// btnGroupGenre
			// 
			this.btnGroupGenre.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnGroupGenre.Location = new System.Drawing.Point(93, 82);
			this.btnGroupGenre.Name = "btnGroupGenre";
			this.btnGroupGenre.Size = new System.Drawing.Size(96, 23);
			this.btnGroupGenre.TabIndex = 22;
			this.btnGroupGenre.Text = "Группа\\Жанр";
			this.btnGroupGenre.UseVisualStyleBackColor = true;
			this.btnGroupGenre.Click += new System.EventHandler(this.BtnGroupGenreClick);
			// 
			// btnLang
			// 
			this.btnLang.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnLang.Location = new System.Drawing.Point(7, 54);
			this.btnLang.Name = "btnLang";
			this.btnLang.Size = new System.Drawing.Size(80, 23);
			this.btnLang.TabIndex = 21;
			this.btnLang.Text = "Язык";
			this.btnLang.UseVisualStyleBackColor = true;
			this.btnLang.Click += new System.EventHandler(this.BtnLangClick);
			// 
			// btnRightBracket
			// 
			this.btnRightBracket.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnRightBracket.Location = new System.Drawing.Point(508, 54);
			this.btnRightBracket.Name = "btnRightBracket";
			this.btnRightBracket.Size = new System.Drawing.Size(23, 23);
			this.btnRightBracket.TabIndex = 20;
			this.btnRightBracket.Text = "]";
			this.btnRightBracket.UseVisualStyleBackColor = true;
			this.btnRightBracket.Click += new System.EventHandler(this.BtnRightBracketClick);
			// 
			// btnBook
			// 
			this.btnBook.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnBook.Location = new System.Drawing.Point(279, 82);
			this.btnBook.Name = "btnBook";
			this.btnBook.Size = new System.Drawing.Size(80, 23);
			this.btnBook.TabIndex = 15;
			this.btnBook.Text = "Книга";
			this.btnBook.UseVisualStyleBackColor = true;
			this.btnBook.Click += new System.EventHandler(this.BtnBookClick);
			// 
			// btnFamily
			// 
			this.btnFamily.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnFamily.Location = new System.Drawing.Point(195, 54);
			this.btnFamily.Name = "btnFamily";
			this.btnFamily.Size = new System.Drawing.Size(80, 23);
			this.btnFamily.TabIndex = 12;
			this.btnFamily.Text = "Фамилия";
			this.btnFamily.UseVisualStyleBackColor = true;
			this.btnFamily.Click += new System.EventHandler(this.BtnFamilyClick);
			// 
			// btnLeftBracket
			// 
			this.btnLeftBracket.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnLeftBracket.Location = new System.Drawing.Point(482, 54);
			this.btnLeftBracket.Name = "btnLeftBracket";
			this.btnLeftBracket.Size = new System.Drawing.Size(23, 23);
			this.btnLeftBracket.TabIndex = 19;
			this.btnLeftBracket.Text = "[";
			this.btnLeftBracket.UseVisualStyleBackColor = true;
			this.btnLeftBracket.Click += new System.EventHandler(this.BtnLeftBracketClick);
			// 
			// btnGenre
			// 
			this.btnGenre.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnGenre.Location = new System.Drawing.Point(193, 82);
			this.btnGenre.Name = "btnGenre";
			this.btnGenre.Size = new System.Drawing.Size(80, 23);
			this.btnGenre.TabIndex = 18;
			this.btnGenre.Text = "Жанр";
			this.btnGenre.UseVisualStyleBackColor = true;
			this.btnGenre.Click += new System.EventHandler(this.BtnGenreClick);
			// 
			// btnSequenceNumber
			// 
			this.btnSequenceNumber.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSequenceNumber.Location = new System.Drawing.Point(450, 82);
			this.btnSequenceNumber.Name = "btnSequenceNumber";
			this.btnSequenceNumber.Size = new System.Drawing.Size(80, 23);
			this.btnSequenceNumber.TabIndex = 17;
			this.btnSequenceNumber.Text = "№ Серии";
			this.btnSequenceNumber.UseVisualStyleBackColor = true;
			this.btnSequenceNumber.Click += new System.EventHandler(this.BtnSequenceNumberClick);
			// 
			// btnSequence
			// 
			this.btnSequence.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSequence.Location = new System.Drawing.Point(365, 82);
			this.btnSequence.Name = "btnSequence";
			this.btnSequence.Size = new System.Drawing.Size(80, 23);
			this.btnSequence.TabIndex = 16;
			this.btnSequence.Text = "Серия";
			this.btnSequence.UseVisualStyleBackColor = true;
			this.btnSequence.Click += new System.EventHandler(this.BtnSequenceClick);
			// 
			// btnPatronimic
			// 
			this.btnPatronimic.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnPatronimic.Location = new System.Drawing.Point(366, 54);
			this.btnPatronimic.Name = "btnPatronimic";
			this.btnPatronimic.Size = new System.Drawing.Size(80, 23);
			this.btnPatronimic.TabIndex = 14;
			this.btnPatronimic.Text = "Отчество";
			this.btnPatronimic.UseVisualStyleBackColor = true;
			this.btnPatronimic.Click += new System.EventHandler(this.BtnPatronimicClick);
			// 
			// btnName
			// 
			this.btnName.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnName.Location = new System.Drawing.Point(281, 54);
			this.btnName.Name = "btnName";
			this.btnName.Size = new System.Drawing.Size(80, 23);
			this.btnName.TabIndex = 13;
			this.btnName.Text = "Имя";
			this.btnName.UseVisualStyleBackColor = true;
			this.btnName.Click += new System.EventHandler(this.BtnNameClick);
			// 
			// btnDir
			// 
			this.btnDir.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnDir.Location = new System.Drawing.Point(453, 54);
			this.btnDir.Name = "btnDir";
			this.btnDir.Size = new System.Drawing.Size(23, 23);
			this.btnDir.TabIndex = 11;
			this.btnDir.Text = "\\";
			this.btnDir.UseVisualStyleBackColor = true;
			this.btnDir.Click += new System.EventHandler(this.BtnDirClick);
			// 
			// btnLetterFamily
			// 
			this.btnLetterFamily.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnLetterFamily.Location = new System.Drawing.Point(93, 54);
			this.btnLetterFamily.Name = "btnLetterFamily";
			this.btnLetterFamily.Size = new System.Drawing.Size(96, 23);
			this.btnLetterFamily.TabIndex = 10;
			this.btnLetterFamily.Text = "Буква\\Фамилия ";
			this.btnLetterFamily.UseVisualStyleBackColor = true;
			this.btnLetterFamily.Click += new System.EventHandler(this.BtnLetterFamilyClick);
			// 
			// buttonFullSortFilesTo
			// 
			this.buttonFullSortFilesTo.Font = new System.Drawing.Font("Tahoma", 11F);
			this.buttonFullSortFilesTo.Image = ((System.Drawing.Image)(resources.GetObject("buttonFullSortFilesTo.Image")));
			this.buttonFullSortFilesTo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonFullSortFilesTo.Location = new System.Drawing.Point(3, 5);
			this.buttonFullSortFilesTo.Name = "buttonFullSortFilesTo";
			this.buttonFullSortFilesTo.Size = new System.Drawing.Size(158, 44);
			this.buttonFullSortFilesTo.TabIndex = 2;
			this.buttonFullSortFilesTo.Text = "Сортировать  ";
			this.buttonFullSortFilesTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.buttonFullSortFilesTo.UseVisualStyleBackColor = true;
			this.buttonFullSortFilesTo.Click += new System.EventHandler(this.ButtonSortFilesToClick);
			// 
			// tpSelectedSort
			// 
			this.tpSelectedSort.Controls.Add(this.panelLV);
			this.tpSelectedSort.Controls.Add(this.pSSData);
			this.tpSelectedSort.Controls.Add(this.pSelectedSortDirs);
			this.tpSelectedSort.Controls.Add(this.pSSTemplate);
			this.tpSelectedSort.Font = new System.Drawing.Font("Tahoma", 8F);
			this.tpSelectedSort.Location = new System.Drawing.Point(4, 23);
			this.tpSelectedSort.Name = "tpSelectedSort";
			this.tpSelectedSort.Padding = new System.Windows.Forms.Padding(3);
			this.tpSelectedSort.Size = new System.Drawing.Size(911, 601);
			this.tpSelectedSort.TabIndex = 1;
			this.tpSelectedSort.Text = " Избранная Сортировка ";
			this.tpSelectedSort.UseVisualStyleBackColor = true;
			// 
			// panelLV
			// 
			this.panelLV.Controls.Add(this.lvSSData);
			this.panelLV.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelLV.Location = new System.Drawing.Point(3, 307);
			this.panelLV.Name = "panelLV";
			this.panelLV.Size = new System.Drawing.Size(905, 291);
			this.panelLV.TabIndex = 66;
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
									this.cHeaderBookTitle,
									this.cHeaderExactFit,
									this.cHeaderGenreScheme});
			this.lvSSData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvSSData.FullRowSelect = true;
			this.lvSSData.GridLines = true;
			this.lvSSData.Location = new System.Drawing.Point(0, 0);
			this.lvSSData.Name = "lvSSData";
			this.lvSSData.Size = new System.Drawing.Size(905, 291);
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
			// cHeaderBookTitle
			// 
			this.cHeaderBookTitle.Text = "Название Книги";
			this.cHeaderBookTitle.Width = 110;
			// 
			// cHeaderExactFit
			// 
			this.cHeaderExactFit.Text = "Точное соответствие";
			// 
			// cHeaderGenreScheme
			// 
			this.cHeaderGenreScheme.Text = "Схема Жанров";
			this.cHeaderGenreScheme.Width = 100;
			// 
			// pSSData
			// 
			this.pSSData.Controls.Add(this.btnSSDataListLoad);
			this.pSSData.Controls.Add(this.btnSSDataListSave);
			this.pSSData.Controls.Add(this.btnSSGetData);
			this.pSSData.Dock = System.Windows.Forms.DockStyle.Top;
			this.pSSData.Location = new System.Drawing.Point(3, 255);
			this.pSSData.Name = "pSSData";
			this.pSSData.Size = new System.Drawing.Size(905, 52);
			this.pSSData.TabIndex = 62;
			// 
			// btnSSDataListLoad
			// 
			this.btnSSDataListLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSSDataListLoad.Image = ((System.Drawing.Image)(resources.GetObject("btnSSDataListLoad.Image")));
			this.btnSSDataListLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnSSDataListLoad.Location = new System.Drawing.Point(751, 5);
			this.btnSSDataListLoad.Name = "btnSSDataListLoad";
			this.btnSSDataListLoad.Size = new System.Drawing.Size(142, 40);
			this.btnSSDataListLoad.TabIndex = 12;
			this.btnSSDataListLoad.Text = "Загрузить список ";
			this.btnSSDataListLoad.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSSDataListLoad.UseVisualStyleBackColor = true;
			this.btnSSDataListLoad.Click += new System.EventHandler(this.BtnSSDataListLoadClick);
			// 
			// btnSSDataListSave
			// 
			this.btnSSDataListSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSSDataListSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSSDataListSave.Image")));
			this.btnSSDataListSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnSSDataListSave.Location = new System.Drawing.Point(598, 5);
			this.btnSSDataListSave.Name = "btnSSDataListSave";
			this.btnSSDataListSave.Size = new System.Drawing.Size(141, 40);
			this.btnSSDataListSave.TabIndex = 11;
			this.btnSSDataListSave.Text = "Сохранить список ";
			this.btnSSDataListSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSSDataListSave.UseVisualStyleBackColor = true;
			this.btnSSDataListSave.Click += new System.EventHandler(this.BtnSSDataListSaveClick);
			// 
			// btnSSGetData
			// 
			this.btnSSGetData.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.btnSSGetData.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSGetData.Image = ((System.Drawing.Image)(resources.GetObject("btnSSGetData.Image")));
			this.btnSSGetData.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnSSGetData.Location = new System.Drawing.Point(8, 5);
			this.btnSSGetData.Name = "btnSSGetData";
			this.btnSSGetData.Size = new System.Drawing.Size(317, 40);
			this.btnSSGetData.TabIndex = 10;
			this.btnSSGetData.Text = "Собрать данные для Избранной Сортировки ";
			this.btnSSGetData.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnSSGetData.UseVisualStyleBackColor = true;
			this.btnSSGetData.Click += new System.EventHandler(this.BtnSSGetDataClick);
			// 
			// pSelectedSortDirs
			// 
			this.pSelectedSortDirs.Controls.Add(this.btnSSTargetDir);
			this.pSelectedSortDirs.Controls.Add(this.btnSSOpenDir);
			this.pSelectedSortDirs.Controls.Add(this.lblSSTargetDir);
			this.pSelectedSortDirs.Controls.Add(this.tboxSSToDir);
			this.pSelectedSortDirs.Controls.Add(this.tboxSSSourceDir);
			this.pSelectedSortDirs.Controls.Add(this.lbSSlDir);
			this.pSelectedSortDirs.Dock = System.Windows.Forms.DockStyle.Top;
			this.pSelectedSortDirs.Location = new System.Drawing.Point(3, 183);
			this.pSelectedSortDirs.Name = "pSelectedSortDirs";
			this.pSelectedSortDirs.Size = new System.Drawing.Size(905, 72);
			this.pSelectedSortDirs.TabIndex = 65;
			// 
			// btnSSTargetDir
			// 
			this.btnSSTargetDir.Image = ((System.Drawing.Image)(resources.GetObject("btnSSTargetDir.Image")));
			this.btnSSTargetDir.Location = new System.Drawing.Point(164, 38);
			this.btnSSTargetDir.Name = "btnSSTargetDir";
			this.btnSSTargetDir.Size = new System.Drawing.Size(31, 27);
			this.btnSSTargetDir.TabIndex = 4;
			this.btnSSTargetDir.UseVisualStyleBackColor = true;
			this.btnSSTargetDir.Click += new System.EventHandler(this.BtnSSTargetDirClick);
			// 
			// btnSSOpenDir
			// 
			this.btnSSOpenDir.Image = ((System.Drawing.Image)(resources.GetObject("btnSSOpenDir.Image")));
			this.btnSSOpenDir.Location = new System.Drawing.Point(164, 6);
			this.btnSSOpenDir.Name = "btnSSOpenDir";
			this.btnSSOpenDir.Size = new System.Drawing.Size(31, 27);
			this.btnSSOpenDir.TabIndex = 2;
			this.btnSSOpenDir.UseVisualStyleBackColor = true;
			this.btnSSOpenDir.Click += new System.EventHandler(this.BtnSSOpenDirClick);
			// 
			// lblSSTargetDir
			// 
			this.lblSSTargetDir.AutoSize = true;
			this.lblSSTargetDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblSSTargetDir.Location = new System.Drawing.Point(3, 43);
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
			this.tboxSSToDir.Location = new System.Drawing.Point(202, 41);
			this.tboxSSToDir.Name = "tboxSSToDir";
			this.tboxSSToDir.Size = new System.Drawing.Size(682, 20);
			this.tboxSSToDir.TabIndex = 3;
			this.tboxSSToDir.TextChanged += new System.EventHandler(this.TboxSSToDirTextChanged);
			// 
			// tboxSSSourceDir
			// 
			this.tboxSSSourceDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxSSSourceDir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tboxSSSourceDir.Location = new System.Drawing.Point(202, 7);
			this.tboxSSSourceDir.Name = "tboxSSSourceDir";
			this.tboxSSSourceDir.Size = new System.Drawing.Size(682, 21);
			this.tboxSSSourceDir.TabIndex = 1;
			this.tboxSSSourceDir.TextChanged += new System.EventHandler(this.TboxSSSourceDirTextChanged);
			// 
			// lbSSlDir
			// 
			this.lbSSlDir.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.lbSSlDir.Location = new System.Drawing.Point(3, 10);
			this.lbSSlDir.Name = "lbSSlDir";
			this.lbSSlDir.Size = new System.Drawing.Size(162, 19);
			this.lbSSlDir.TabIndex = 6;
			this.lbSSlDir.Text = "Папка для сканирования:";
			// 
			// pSSTemplate
			// 
			this.pSSTemplate.Controls.Add(this.buttonSSortRenew);
			this.pSSTemplate.Controls.Add(this.buttonSSortFilesTo);
			this.pSSTemplate.Controls.Add(this.gBoxSelectedlSortOptions);
			this.pSSTemplate.Controls.Add(this.gBoxSelectedlSortRenameTemplates);
			this.pSSTemplate.Dock = System.Windows.Forms.DockStyle.Top;
			this.pSSTemplate.Location = new System.Drawing.Point(3, 3);
			this.pSSTemplate.Name = "pSSTemplate";
			this.pSSTemplate.Size = new System.Drawing.Size(905, 180);
			this.pSSTemplate.TabIndex = 64;
			// 
			// buttonSSortRenew
			// 
			this.buttonSSortRenew.Font = new System.Drawing.Font("Tahoma", 11F);
			this.buttonSSortRenew.Image = ((System.Drawing.Image)(resources.GetObject("buttonSSortRenew.Image")));
			this.buttonSSortRenew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonSSortRenew.Location = new System.Drawing.Point(170, 5);
			this.buttonSSortRenew.Name = "buttonSSortRenew";
			this.buttonSSortRenew.Size = new System.Drawing.Size(229, 44);
			this.buttonSSortRenew.TabIndex = 65;
			this.buttonSSortRenew.Text = "Возобновить из файла...";
			this.buttonSSortRenew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.buttonSSortRenew.UseVisualStyleBackColor = true;
			this.buttonSSortRenew.Click += new System.EventHandler(this.ButtonSSortRenewClick);
			// 
			// buttonSSortFilesTo
			// 
			this.buttonSSortFilesTo.Font = new System.Drawing.Font("Tahoma", 11F);
			this.buttonSSortFilesTo.Image = ((System.Drawing.Image)(resources.GetObject("buttonSSortFilesTo.Image")));
			this.buttonSSortFilesTo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonSSortFilesTo.Location = new System.Drawing.Point(3, 5);
			this.buttonSSortFilesTo.Name = "buttonSSortFilesTo";
			this.buttonSSortFilesTo.Size = new System.Drawing.Size(158, 44);
			this.buttonSSortFilesTo.TabIndex = 2;
			this.buttonSSortFilesTo.Text = "Сортировать  ";
			this.buttonSSortFilesTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.buttonSSortFilesTo.UseVisualStyleBackColor = true;
			this.buttonSSortFilesTo.Click += new System.EventHandler(this.ButtonSSortFilesToClick);
			// 
			// gBoxSelectedlSortOptions
			// 
			this.gBoxSelectedlSortOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.gBoxSelectedlSortOptions.Controls.Add(this.chBoxSSScanSubDir);
			this.gBoxSelectedlSortOptions.Controls.Add(this.chBoxSSNotDelFB2Files);
			this.gBoxSelectedlSortOptions.Controls.Add(this.chBoxSSToZip);
			this.gBoxSelectedlSortOptions.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gBoxSelectedlSortOptions.Location = new System.Drawing.Point(618, 5);
			this.gBoxSelectedlSortOptions.Name = "gBoxSelectedlSortOptions";
			this.gBoxSelectedlSortOptions.Size = new System.Drawing.Size(280, 167);
			this.gBoxSelectedlSortOptions.TabIndex = 64;
			this.gBoxSelectedlSortOptions.TabStop = false;
			this.gBoxSelectedlSortOptions.Text = " Настройки ";
			// 
			// chBoxSSScanSubDir
			// 
			this.chBoxSSScanSubDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chBoxSSScanSubDir.Checked = true;
			this.chBoxSSScanSubDir.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chBoxSSScanSubDir.Font = new System.Drawing.Font("Tahoma", 8F);
			this.chBoxSSScanSubDir.Location = new System.Drawing.Point(6, 16);
			this.chBoxSSScanSubDir.Name = "chBoxSSScanSubDir";
			this.chBoxSSScanSubDir.Size = new System.Drawing.Size(173, 24);
			this.chBoxSSScanSubDir.TabIndex = 2;
			this.chBoxSSScanSubDir.Text = "Обрабатывать подкаталоги";
			this.chBoxSSScanSubDir.UseVisualStyleBackColor = true;
			this.chBoxSSScanSubDir.Click += new System.EventHandler(this.ChBoxSSScanSubDirClick);
			// 
			// chBoxSSNotDelFB2Files
			// 
			this.chBoxSSNotDelFB2Files.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chBoxSSNotDelFB2Files.Checked = true;
			this.chBoxSSNotDelFB2Files.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chBoxSSNotDelFB2Files.Font = new System.Drawing.Font("Tahoma", 8F);
			this.chBoxSSNotDelFB2Files.Location = new System.Drawing.Point(6, 54);
			this.chBoxSSNotDelFB2Files.Name = "chBoxSSNotDelFB2Files";
			this.chBoxSSNotDelFB2Files.Size = new System.Drawing.Size(149, 24);
			this.chBoxSSNotDelFB2Files.TabIndex = 19;
			this.chBoxSSNotDelFB2Files.Text = "Сохранять оригиналы";
			this.chBoxSSNotDelFB2Files.UseVisualStyleBackColor = true;
			this.chBoxSSNotDelFB2Files.Click += new System.EventHandler(this.ChBoxSSNotDelFB2FilesClick);
			// 
			// chBoxSSToZip
			// 
			this.chBoxSSToZip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chBoxSSToZip.Font = new System.Drawing.Font("Tahoma", 8F);
			this.chBoxSSToZip.Location = new System.Drawing.Point(6, 35);
			this.chBoxSSToZip.Name = "chBoxSSToZip";
			this.chBoxSSToZip.Size = new System.Drawing.Size(130, 24);
			this.chBoxSSToZip.TabIndex = 13;
			this.chBoxSSToZip.Text = "Архивировать в zip";
			this.chBoxSSToZip.UseVisualStyleBackColor = true;
			this.chBoxSSToZip.Click += new System.EventHandler(this.ChBoxSSToZipClick);
			// 
			// gBoxSelectedlSortRenameTemplates
			// 
			this.gBoxSelectedlSortRenameTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.gBoxSelectedlSortRenameTemplates.Controls.Add(this.btnSSGroup);
			this.gBoxSelectedlSortRenameTemplates.Controls.Add(this.btnSSGroupGenre);
			this.gBoxSelectedlSortRenameTemplates.Controls.Add(this.btnSSLang);
			this.gBoxSelectedlSortRenameTemplates.Controls.Add(this.btnSSRightBracket);
			this.gBoxSelectedlSortRenameTemplates.Controls.Add(this.btnSSBook);
			this.gBoxSelectedlSortRenameTemplates.Controls.Add(this.btnSSFamily);
			this.gBoxSelectedlSortRenameTemplates.Controls.Add(this.btnSSLeftBracket);
			this.gBoxSelectedlSortRenameTemplates.Controls.Add(this.btnSSGenre);
			this.gBoxSelectedlSortRenameTemplates.Controls.Add(this.btnSSSequenceNumber);
			this.gBoxSelectedlSortRenameTemplates.Controls.Add(this.btnSSSequence);
			this.gBoxSelectedlSortRenameTemplates.Controls.Add(this.btnSSPatronimic);
			this.gBoxSelectedlSortRenameTemplates.Controls.Add(this.btnSSName);
			this.gBoxSelectedlSortRenameTemplates.Controls.Add(this.btnSSDir);
			this.gBoxSelectedlSortRenameTemplates.Controls.Add(this.btnSSLetterFamily);
			this.gBoxSelectedlSortRenameTemplates.Controls.Add(this.btnSSInsertTemplates);
			this.gBoxSelectedlSortRenameTemplates.Controls.Add(this.txtBoxSSTemplatesFromLine);
			this.gBoxSelectedlSortRenameTemplates.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gBoxSelectedlSortRenameTemplates.ForeColor = System.Drawing.SystemColors.ControlText;
			this.gBoxSelectedlSortRenameTemplates.Location = new System.Drawing.Point(3, 55);
			this.gBoxSelectedlSortRenameTemplates.Name = "gBoxSelectedlSortRenameTemplates";
			this.gBoxSelectedlSortRenameTemplates.Size = new System.Drawing.Size(607, 117);
			this.gBoxSelectedlSortRenameTemplates.TabIndex = 63;
			this.gBoxSelectedlSortRenameTemplates.TabStop = false;
			this.gBoxSelectedlSortRenameTemplates.Text = " Шаблоны подстановки ";
			// 
			// btnSSGroup
			// 
			this.btnSSGroup.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSSGroup.Location = new System.Drawing.Point(7, 82);
			this.btnSSGroup.Name = "btnSSGroup";
			this.btnSSGroup.Size = new System.Drawing.Size(80, 23);
			this.btnSSGroup.TabIndex = 36;
			this.btnSSGroup.Text = "Группа";
			this.btnSSGroup.UseVisualStyleBackColor = true;
			this.btnSSGroup.Click += new System.EventHandler(this.BtnSSGroupClick);
			// 
			// btnSSGroupGenre
			// 
			this.btnSSGroupGenre.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSSGroupGenre.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSGroupGenre.Location = new System.Drawing.Point(93, 82);
			this.btnSSGroupGenre.Name = "btnSSGroupGenre";
			this.btnSSGroupGenre.Size = new System.Drawing.Size(96, 23);
			this.btnSSGroupGenre.TabIndex = 35;
			this.btnSSGroupGenre.Text = "Группа\\Жанр";
			this.btnSSGroupGenre.UseVisualStyleBackColor = true;
			this.btnSSGroupGenre.Click += new System.EventHandler(this.BtnSSGroupGenreClick);
			// 
			// btnSSLang
			// 
			this.btnSSLang.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSSLang.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSLang.Location = new System.Drawing.Point(7, 54);
			this.btnSSLang.Name = "btnSSLang";
			this.btnSSLang.Size = new System.Drawing.Size(80, 23);
			this.btnSSLang.TabIndex = 34;
			this.btnSSLang.Text = "Язык";
			this.btnSSLang.UseVisualStyleBackColor = true;
			this.btnSSLang.Click += new System.EventHandler(this.BtnSSLangClick);
			// 
			// btnSSRightBracket
			// 
			this.btnSSRightBracket.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSSRightBracket.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSRightBracket.Location = new System.Drawing.Point(508, 54);
			this.btnSSRightBracket.Name = "btnSSRightBracket";
			this.btnSSRightBracket.Size = new System.Drawing.Size(23, 23);
			this.btnSSRightBracket.TabIndex = 33;
			this.btnSSRightBracket.Text = "]";
			this.btnSSRightBracket.UseVisualStyleBackColor = true;
			this.btnSSRightBracket.Click += new System.EventHandler(this.BtnSSRightBracketClick);
			// 
			// btnSSBook
			// 
			this.btnSSBook.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSSBook.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSBook.Location = new System.Drawing.Point(279, 82);
			this.btnSSBook.Name = "btnSSBook";
			this.btnSSBook.Size = new System.Drawing.Size(80, 23);
			this.btnSSBook.TabIndex = 28;
			this.btnSSBook.Text = "Книга";
			this.btnSSBook.UseVisualStyleBackColor = true;
			this.btnSSBook.Click += new System.EventHandler(this.BtnSSBookClick);
			// 
			// btnSSFamily
			// 
			this.btnSSFamily.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSSFamily.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSFamily.Location = new System.Drawing.Point(195, 54);
			this.btnSSFamily.Name = "btnSSFamily";
			this.btnSSFamily.Size = new System.Drawing.Size(80, 23);
			this.btnSSFamily.TabIndex = 25;
			this.btnSSFamily.Text = "Фамилия";
			this.btnSSFamily.UseVisualStyleBackColor = true;
			this.btnSSFamily.Click += new System.EventHandler(this.BtnSSFamilyClick);
			// 
			// btnSSLeftBracket
			// 
			this.btnSSLeftBracket.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSSLeftBracket.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSLeftBracket.Location = new System.Drawing.Point(482, 54);
			this.btnSSLeftBracket.Name = "btnSSLeftBracket";
			this.btnSSLeftBracket.Size = new System.Drawing.Size(23, 23);
			this.btnSSLeftBracket.TabIndex = 32;
			this.btnSSLeftBracket.Text = "[";
			this.btnSSLeftBracket.UseVisualStyleBackColor = true;
			this.btnSSLeftBracket.Click += new System.EventHandler(this.BtnSSLeftBracketClick);
			// 
			// btnSSGenre
			// 
			this.btnSSGenre.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSSGenre.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSGenre.Location = new System.Drawing.Point(193, 82);
			this.btnSSGenre.Name = "btnSSGenre";
			this.btnSSGenre.Size = new System.Drawing.Size(80, 23);
			this.btnSSGenre.TabIndex = 31;
			this.btnSSGenre.Text = "Жанр";
			this.btnSSGenre.UseVisualStyleBackColor = true;
			this.btnSSGenre.Click += new System.EventHandler(this.BtnSSGenreClick);
			// 
			// btnSSSequenceNumber
			// 
			this.btnSSSequenceNumber.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSSSequenceNumber.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSSequenceNumber.Location = new System.Drawing.Point(450, 82);
			this.btnSSSequenceNumber.Name = "btnSSSequenceNumber";
			this.btnSSSequenceNumber.Size = new System.Drawing.Size(80, 23);
			this.btnSSSequenceNumber.TabIndex = 30;
			this.btnSSSequenceNumber.Text = "№ Серии";
			this.btnSSSequenceNumber.UseVisualStyleBackColor = true;
			this.btnSSSequenceNumber.Click += new System.EventHandler(this.BtnSSSequenceNumberClick);
			// 
			// btnSSSequence
			// 
			this.btnSSSequence.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSSSequence.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSSequence.Location = new System.Drawing.Point(365, 82);
			this.btnSSSequence.Name = "btnSSSequence";
			this.btnSSSequence.Size = new System.Drawing.Size(80, 23);
			this.btnSSSequence.TabIndex = 29;
			this.btnSSSequence.Text = "Серия";
			this.btnSSSequence.UseVisualStyleBackColor = true;
			this.btnSSSequence.Click += new System.EventHandler(this.BtnSSSequenceClick);
			// 
			// btnSSPatronimic
			// 
			this.btnSSPatronimic.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSSPatronimic.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSPatronimic.Location = new System.Drawing.Point(366, 54);
			this.btnSSPatronimic.Name = "btnSSPatronimic";
			this.btnSSPatronimic.Size = new System.Drawing.Size(80, 23);
			this.btnSSPatronimic.TabIndex = 27;
			this.btnSSPatronimic.Text = "Отчество";
			this.btnSSPatronimic.UseVisualStyleBackColor = true;
			this.btnSSPatronimic.Click += new System.EventHandler(this.BtnSSPatronimicClick);
			// 
			// btnSSName
			// 
			this.btnSSName.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSSName.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSName.Location = new System.Drawing.Point(281, 54);
			this.btnSSName.Name = "btnSSName";
			this.btnSSName.Size = new System.Drawing.Size(80, 23);
			this.btnSSName.TabIndex = 26;
			this.btnSSName.Text = "Имя";
			this.btnSSName.UseVisualStyleBackColor = true;
			this.btnSSName.Click += new System.EventHandler(this.BtnSSNameClick);
			// 
			// btnSSDir
			// 
			this.btnSSDir.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSSDir.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSDir.Location = new System.Drawing.Point(453, 54);
			this.btnSSDir.Name = "btnSSDir";
			this.btnSSDir.Size = new System.Drawing.Size(23, 23);
			this.btnSSDir.TabIndex = 24;
			this.btnSSDir.Text = "\\";
			this.btnSSDir.UseVisualStyleBackColor = true;
			this.btnSSDir.Click += new System.EventHandler(this.BtnSSDirClick);
			// 
			// btnSSLetterFamily
			// 
			this.btnSSLetterFamily.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSSLetterFamily.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSLetterFamily.Location = new System.Drawing.Point(93, 54);
			this.btnSSLetterFamily.Name = "btnSSLetterFamily";
			this.btnSSLetterFamily.Size = new System.Drawing.Size(96, 23);
			this.btnSSLetterFamily.TabIndex = 23;
			this.btnSSLetterFamily.Text = "Буква\\Фамилия ";
			this.btnSSLetterFamily.UseVisualStyleBackColor = true;
			this.btnSSLetterFamily.Click += new System.EventHandler(this.BtnSSLetterFamilyClick);
			// 
			// btnSSInsertTemplates
			// 
			this.btnSSInsertTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSSInsertTemplates.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSSInsertTemplates.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSInsertTemplates.Location = new System.Drawing.Point(477, 17);
			this.btnSSInsertTemplates.Name = "btnSSInsertTemplates";
			this.btnSSInsertTemplates.Size = new System.Drawing.Size(120, 28);
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
			this.txtBoxSSTemplatesFromLine.Size = new System.Drawing.Size(465, 20);
			this.txtBoxSSTemplatesFromLine.TabIndex = 8;
			this.txtBoxSSTemplatesFromLine.TextChanged += new System.EventHandler(this.TxtBoxSSTemplatesFromLineTextChanged);
			// 
			// tcTemplates
			// 
			this.tcTemplates.Controls.Add(this.rtboxTemplatesList);
			this.tcTemplates.Location = new System.Drawing.Point(4, 23);
			this.tcTemplates.Name = "tcTemplates";
			this.tcTemplates.Padding = new System.Windows.Forms.Padding(3);
			this.tcTemplates.Size = new System.Drawing.Size(633, 601);
			this.tcTemplates.TabIndex = 2;
			this.tcTemplates.Text = "Шаблоны подстановки";
			this.tcTemplates.UseVisualStyleBackColor = true;
			// 
			// rtboxTemplatesList
			// 
			this.rtboxTemplatesList.BackColor = System.Drawing.SystemColors.Window;
			this.rtboxTemplatesList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtboxTemplatesList.ForeColor = System.Drawing.SystemColors.WindowText;
			this.rtboxTemplatesList.Location = new System.Drawing.Point(3, 3);
			this.rtboxTemplatesList.Name = "rtboxTemplatesList";
			this.rtboxTemplatesList.ReadOnly = true;
			this.rtboxTemplatesList.Size = new System.Drawing.Size(627, 595);
			this.rtboxTemplatesList.TabIndex = 35;
			this.rtboxTemplatesList.Text = "";
			// 
			// tpSettings
			// 
			this.tpSettings.Controls.Add(this.tcFM);
			this.tpSettings.Controls.Add(this.panel1);
			this.tpSettings.Location = new System.Drawing.Point(4, 23);
			this.tpSettings.Name = "tpSettings";
			this.tpSettings.Padding = new System.Windows.Forms.Padding(3);
			this.tpSettings.Size = new System.Drawing.Size(633, 601);
			this.tpSettings.TabIndex = 3;
			this.tpSettings.Text = "Настройки";
			this.tpSettings.UseVisualStyleBackColor = true;
			// 
			// tcFM
			// 
			this.tcFM.Controls.Add(this.tpFMGeneral);
			this.tcFM.Controls.Add(this.tpFMNoTagsText);
			this.tcFM.Controls.Add(this.tpFMGenreGroups);
			this.tcFM.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcFM.Location = new System.Drawing.Point(3, 3);
			this.tcFM.Name = "tcFM";
			this.tcFM.SelectedIndex = 0;
			this.tcFM.Size = new System.Drawing.Size(627, 551);
			this.tcFM.TabIndex = 38;
			// 
			// tpFMGeneral
			// 
			this.tpFMGeneral.Controls.Add(this.gboxApportionment);
			this.tpFMGeneral.Controls.Add(this.gboxFMGeneral);
			this.tpFMGeneral.Location = new System.Drawing.Point(4, 22);
			this.tpFMGeneral.Name = "tpFMGeneral";
			this.tpFMGeneral.Padding = new System.Windows.Forms.Padding(3);
			this.tpFMGeneral.Size = new System.Drawing.Size(619, 525);
			this.tpFMGeneral.TabIndex = 0;
			this.tpFMGeneral.Text = " Основные ";
			this.tpFMGeneral.UseVisualStyleBackColor = true;
			// 
			// gboxApportionment
			// 
			this.gboxApportionment.Controls.Add(this.gBoxGenres);
			this.gboxApportionment.Controls.Add(this.gBoxAuthors);
			this.gboxApportionment.Dock = System.Windows.Forms.DockStyle.Top;
			this.gboxApportionment.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gboxApportionment.ForeColor = System.Drawing.Color.Maroon;
			this.gboxApportionment.Location = new System.Drawing.Point(3, 176);
			this.gboxApportionment.Name = "gboxApportionment";
			this.gboxApportionment.Size = new System.Drawing.Size(613, 156);
			this.gboxApportionment.TabIndex = 30;
			this.gboxApportionment.TabStop = false;
			this.gboxApportionment.Text = " Раскладка файлов по папкам ";
			// 
			// gBoxGenres
			// 
			this.gBoxGenres.Controls.Add(this.gBoxGenresType);
			this.gBoxGenres.Controls.Add(this.gBoxGenresCount);
			this.gBoxGenres.Dock = System.Windows.Forms.DockStyle.Top;
			this.gBoxGenres.ForeColor = System.Drawing.Color.Navy;
			this.gBoxGenres.Location = new System.Drawing.Point(3, 56);
			this.gBoxGenres.Name = "gBoxGenres";
			this.gBoxGenres.Size = new System.Drawing.Size(607, 91);
			this.gBoxGenres.TabIndex = 28;
			this.gBoxGenres.TabStop = false;
			this.gBoxGenres.Text = " Жанры ";
			// 
			// gBoxGenresType
			// 
			this.gBoxGenresType.Controls.Add(this.rbtnGenreText);
			this.gBoxGenresType.Controls.Add(this.rbtnGenreSchema);
			this.gBoxGenresType.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gBoxGenresType.ForeColor = System.Drawing.Color.Purple;
			this.gBoxGenresType.Location = new System.Drawing.Point(260, 19);
			this.gBoxGenresType.Name = "gBoxGenresType";
			this.gBoxGenresType.Size = new System.Drawing.Size(331, 57);
			this.gBoxGenresType.TabIndex = 27;
			this.gBoxGenresType.TabStop = false;
			this.gBoxGenresType.Text = " Вид папки - жанра ";
			// 
			// rbtnGenreText
			// 
			this.rbtnGenreText.Dock = System.Windows.Forms.DockStyle.Top;
			this.rbtnGenreText.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnGenreText.Location = new System.Drawing.Point(3, 34);
			this.rbtnGenreText.Name = "rbtnGenreText";
			this.rbtnGenreText.Size = new System.Drawing.Size(325, 18);
			this.rbtnGenreText.TabIndex = 1;
			this.rbtnGenreText.Text = "Расшифровано (например: Русская классика)";
			this.rbtnGenreText.UseVisualStyleBackColor = true;
			this.rbtnGenreText.Click += new System.EventHandler(this.RbtnGenreSchemaClick);
			// 
			// rbtnGenreSchema
			// 
			this.rbtnGenreSchema.Checked = true;
			this.rbtnGenreSchema.Dock = System.Windows.Forms.DockStyle.Top;
			this.rbtnGenreSchema.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnGenreSchema.Location = new System.Drawing.Point(3, 16);
			this.rbtnGenreSchema.Name = "rbtnGenreSchema";
			this.rbtnGenreSchema.Size = new System.Drawing.Size(325, 18);
			this.rbtnGenreSchema.TabIndex = 0;
			this.rbtnGenreSchema.TabStop = true;
			this.rbtnGenreSchema.Text = "Как в схеме (например: prose_rus_classic)";
			this.rbtnGenreSchema.UseVisualStyleBackColor = true;
			this.rbtnGenreSchema.Click += new System.EventHandler(this.RbtnGenreSchemaClick);
			// 
			// gBoxGenresCount
			// 
			this.gBoxGenresCount.Controls.Add(this.rbtnGenreAll);
			this.gBoxGenresCount.Controls.Add(this.rbtnGenreOne);
			this.gBoxGenresCount.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gBoxGenresCount.ForeColor = System.Drawing.Color.Purple;
			this.gBoxGenresCount.Location = new System.Drawing.Point(6, 19);
			this.gBoxGenresCount.Name = "gBoxGenresCount";
			this.gBoxGenresCount.Size = new System.Drawing.Size(248, 57);
			this.gBoxGenresCount.TabIndex = 26;
			this.gBoxGenresCount.TabStop = false;
			this.gBoxGenresCount.Text = " Раскладка файлов по жанрам ";
			// 
			// rbtnGenreAll
			// 
			this.rbtnGenreAll.Dock = System.Windows.Forms.DockStyle.Top;
			this.rbtnGenreAll.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnGenreAll.Location = new System.Drawing.Point(3, 34);
			this.rbtnGenreAll.Name = "rbtnGenreAll";
			this.rbtnGenreAll.Size = new System.Drawing.Size(242, 18);
			this.rbtnGenreAll.TabIndex = 1;
			this.rbtnGenreAll.Text = "По всем жанрам";
			this.rbtnGenreAll.UseVisualStyleBackColor = true;
			this.rbtnGenreAll.Click += new System.EventHandler(this.RbtnGenreOneClick);
			// 
			// rbtnGenreOne
			// 
			this.rbtnGenreOne.Checked = true;
			this.rbtnGenreOne.Dock = System.Windows.Forms.DockStyle.Top;
			this.rbtnGenreOne.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnGenreOne.Location = new System.Drawing.Point(3, 16);
			this.rbtnGenreOne.Name = "rbtnGenreOne";
			this.rbtnGenreOne.Size = new System.Drawing.Size(242, 18);
			this.rbtnGenreOne.TabIndex = 0;
			this.rbtnGenreOne.TabStop = true;
			this.rbtnGenreOne.Text = "По первому жанру";
			this.rbtnGenreOne.UseVisualStyleBackColor = true;
			this.rbtnGenreOne.Click += new System.EventHandler(this.RbtnGenreOneClick);
			// 
			// gBoxAuthors
			// 
			this.gBoxAuthors.Controls.Add(this.rbtnAuthorAll);
			this.gBoxAuthors.Controls.Add(this.rbtnAuthorOne);
			this.gBoxAuthors.Dock = System.Windows.Forms.DockStyle.Top;
			this.gBoxAuthors.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gBoxAuthors.ForeColor = System.Drawing.Color.Navy;
			this.gBoxAuthors.Location = new System.Drawing.Point(3, 16);
			this.gBoxAuthors.Name = "gBoxAuthors";
			this.gBoxAuthors.Size = new System.Drawing.Size(607, 40);
			this.gBoxAuthors.TabIndex = 27;
			this.gBoxAuthors.TabStop = false;
			this.gBoxAuthors.Text = " Авторы ";
			// 
			// rbtnAuthorAll
			// 
			this.rbtnAuthorAll.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnAuthorAll.Location = new System.Drawing.Point(154, 16);
			this.rbtnAuthorAll.Name = "rbtnAuthorAll";
			this.rbtnAuthorAll.Size = new System.Drawing.Size(132, 18);
			this.rbtnAuthorAll.TabIndex = 1;
			this.rbtnAuthorAll.Text = "По всем авторам";
			this.rbtnAuthorAll.UseVisualStyleBackColor = true;
			this.rbtnAuthorAll.Click += new System.EventHandler(this.RbtnAuthorOneClick);
			// 
			// rbtnAuthorOne
			// 
			this.rbtnAuthorOne.Checked = true;
			this.rbtnAuthorOne.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnAuthorOne.Location = new System.Drawing.Point(3, 16);
			this.rbtnAuthorOne.Name = "rbtnAuthorOne";
			this.rbtnAuthorOne.Size = new System.Drawing.Size(145, 18);
			this.rbtnAuthorOne.TabIndex = 0;
			this.rbtnAuthorOne.TabStop = true;
			this.rbtnAuthorOne.Text = "По первому автору";
			this.rbtnAuthorOne.UseVisualStyleBackColor = true;
			this.rbtnAuthorOne.Click += new System.EventHandler(this.RbtnAuthorOneClick);
			// 
			// gboxFMGeneral
			// 
			this.gboxFMGeneral.Controls.Add(this.pSortFB2);
			this.gboxFMGeneral.Controls.Add(this.panel2);
			this.gboxFMGeneral.Controls.Add(this.panel3);
			this.gboxFMGeneral.Controls.Add(this.chBoxStrict);
			this.gboxFMGeneral.Controls.Add(this.chBoxTranslit);
			this.gboxFMGeneral.Controls.Add(this.gboxRegister);
			this.gboxFMGeneral.Dock = System.Windows.Forms.DockStyle.Top;
			this.gboxFMGeneral.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gboxFMGeneral.ForeColor = System.Drawing.Color.Maroon;
			this.gboxFMGeneral.Location = new System.Drawing.Point(3, 3);
			this.gboxFMGeneral.Name = "gboxFMGeneral";
			this.gboxFMGeneral.Size = new System.Drawing.Size(613, 173);
			this.gboxFMGeneral.TabIndex = 28;
			this.gboxFMGeneral.TabStop = false;
			this.gboxFMGeneral.Text = " Основные настройки ";
			// 
			// pSortFB2
			// 
			this.pSortFB2.Controls.Add(this.rbtnFMOnlyValidFB2);
			this.pSortFB2.Controls.Add(this.rbtnFMAllFB2);
			this.pSortFB2.Controls.Add(this.label11);
			this.pSortFB2.Dock = System.Windows.Forms.DockStyle.Top;
			this.pSortFB2.Location = new System.Drawing.Point(3, 145);
			this.pSortFB2.Name = "pSortFB2";
			this.pSortFB2.Size = new System.Drawing.Size(607, 23);
			this.pSortFB2.TabIndex = 30;
			// 
			// rbtnFMOnlyValidFB2
			// 
			this.rbtnFMOnlyValidFB2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.rbtnFMOnlyValidFB2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnFMOnlyValidFB2.Location = new System.Drawing.Point(292, 1);
			this.rbtnFMOnlyValidFB2.Name = "rbtnFMOnlyValidFB2";
			this.rbtnFMOnlyValidFB2.Size = new System.Drawing.Size(182, 17);
			this.rbtnFMOnlyValidFB2.TabIndex = 2;
			this.rbtnFMOnlyValidFB2.Text = "Только Валидные файлы";
			this.rbtnFMOnlyValidFB2.UseVisualStyleBackColor = true;
			this.rbtnFMOnlyValidFB2.Click += new System.EventHandler(this.RbtnFMAllFB2Click);
			// 
			// rbtnFMAllFB2
			// 
			this.rbtnFMAllFB2.Checked = true;
			this.rbtnFMAllFB2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.rbtnFMAllFB2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnFMAllFB2.Location = new System.Drawing.Point(140, 2);
			this.rbtnFMAllFB2.Name = "rbtnFMAllFB2";
			this.rbtnFMAllFB2.Size = new System.Drawing.Size(145, 17);
			this.rbtnFMAllFB2.TabIndex = 1;
			this.rbtnFMAllFB2.TabStop = true;
			this.rbtnFMAllFB2.Text = "Любые fb2-файлы";
			this.rbtnFMAllFB2.UseVisualStyleBackColor = true;
			this.rbtnFMAllFB2.Click += new System.EventHandler(this.RbtnFMAllFB2Click);
			// 
			// label11
			// 
			this.label11.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.label11.ForeColor = System.Drawing.Color.Navy;
			this.label11.Location = new System.Drawing.Point(5, 3);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(133, 16);
			this.label11.TabIndex = 0;
			this.label11.Text = "Сортировка файлов:";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.cboxFileExist);
			this.panel2.Controls.Add(this.lbFilelExist);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(3, 120);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(607, 25);
			this.panel2.TabIndex = 16;
			// 
			// cboxFileExist
			// 
			this.cboxFileExist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxFileExist.FormattingEnabled = true;
			this.cboxFileExist.Items.AddRange(new object[] {
									"Заменить существующий файл новым",
									"Добавить к создаваемому файлу очередной номер",
									"Добавить к создаваемому файлу дату и время"});
			this.cboxFileExist.Location = new System.Drawing.Point(151, 2);
			this.cboxFileExist.Name = "cboxFileExist";
			this.cboxFileExist.Size = new System.Drawing.Size(425, 21);
			this.cboxFileExist.TabIndex = 20;
			this.cboxFileExist.SelectedIndexChanged += new System.EventHandler(this.CboxFileExistSelectedIndexChanged);
			// 
			// lbFilelExist
			// 
			this.lbFilelExist.AutoSize = true;
			this.lbFilelExist.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lbFilelExist.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lbFilelExist.Location = new System.Drawing.Point(2, 5);
			this.lbFilelExist.Name = "lbFilelExist";
			this.lbFilelExist.Size = new System.Drawing.Size(127, 13);
			this.lbFilelExist.TabIndex = 19;
			this.lbFilelExist.Text = "Одинаковые файлы:";
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.cboxSpace);
			this.panel3.Controls.Add(this.lblSpace);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(3, 91);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(607, 29);
			this.panel3.TabIndex = 14;
			// 
			// cboxSpace
			// 
			this.cboxSpace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxSpace.FormattingEnabled = true;
			this.cboxSpace.Items.AddRange(new object[] {
									"Оставить",
									"Удалить",
									"Заменить на  _",
									"Заменить на  -",
									"Заменить на  +",
									"Заменить на  ~",
									"Заменить на  ."});
			this.cboxSpace.Location = new System.Drawing.Point(151, 5);
			this.cboxSpace.Name = "cboxSpace";
			this.cboxSpace.Size = new System.Drawing.Size(123, 21);
			this.cboxSpace.TabIndex = 24;
			this.cboxSpace.SelectedIndexChanged += new System.EventHandler(this.CboxSpaceSelectedIndexChanged);
			// 
			// lblSpace
			// 
			this.lblSpace.AutoSize = true;
			this.lblSpace.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblSpace.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblSpace.Location = new System.Drawing.Point(2, 8);
			this.lblSpace.Name = "lblSpace";
			this.lblSpace.Size = new System.Drawing.Size(133, 13);
			this.lblSpace.TabIndex = 23;
			this.lblSpace.Text = "Обработка пробелов:";
			// 
			// chBoxStrict
			// 
			this.chBoxStrict.Dock = System.Windows.Forms.DockStyle.Top;
			this.chBoxStrict.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxStrict.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chBoxStrict.Location = new System.Drawing.Point(3, 73);
			this.chBoxStrict.Name = "chBoxStrict";
			this.chBoxStrict.Size = new System.Drawing.Size(607, 18);
			this.chBoxStrict.TabIndex = 13;
			this.chBoxStrict.Text = "\"Строгие\" имена файлов: не юникодные алфавитно-цифровые символы, а так же [](){}~" +
			"-+=_.,!@#$%^&№`\';«»";
			this.chBoxStrict.UseVisualStyleBackColor = true;
			this.chBoxStrict.Click += new System.EventHandler(this.ChBoxStrictClick);
			// 
			// chBoxTranslit
			// 
			this.chBoxTranslit.Dock = System.Windows.Forms.DockStyle.Top;
			this.chBoxTranslit.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxTranslit.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chBoxTranslit.Location = new System.Drawing.Point(3, 55);
			this.chBoxTranslit.Name = "chBoxTranslit";
			this.chBoxTranslit.Size = new System.Drawing.Size(607, 18);
			this.chBoxTranslit.TabIndex = 12;
			this.chBoxTranslit.Text = "Транслитерация имен файлов";
			this.chBoxTranslit.UseVisualStyleBackColor = true;
			this.chBoxTranslit.Click += new System.EventHandler(this.ChBoxTranslitClick);
			// 
			// gboxRegister
			// 
			this.gboxRegister.Controls.Add(this.rbtnAsSentence);
			this.gboxRegister.Controls.Add(this.rbtnUpper);
			this.gboxRegister.Controls.Add(this.rbtnLower);
			this.gboxRegister.Controls.Add(this.rbtnAsIs);
			this.gboxRegister.Dock = System.Windows.Forms.DockStyle.Top;
			this.gboxRegister.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gboxRegister.ForeColor = System.Drawing.Color.Navy;
			this.gboxRegister.Location = new System.Drawing.Point(3, 16);
			this.gboxRegister.Name = "gboxRegister";
			this.gboxRegister.Size = new System.Drawing.Size(607, 39);
			this.gboxRegister.TabIndex = 10;
			this.gboxRegister.TabStop = false;
			this.gboxRegister.Text = " Регистр имени файла ";
			// 
			// rbtnAsSentence
			// 
			this.rbtnAsSentence.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnAsSentence.Location = new System.Drawing.Point(359, 16);
			this.rbtnAsSentence.Name = "rbtnAsSentence";
			this.rbtnAsSentence.Size = new System.Drawing.Size(217, 18);
			this.rbtnAsSentence.TabIndex = 3;
			this.rbtnAsSentence.Text = "Каждое Слово С Большой Буквы";
			this.rbtnAsSentence.UseVisualStyleBackColor = true;
			this.rbtnAsSentence.Click += new System.EventHandler(this.RbtnAsIsClick);
			// 
			// rbtnUpper
			// 
			this.rbtnUpper.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnUpper.Location = new System.Drawing.Point(214, 16);
			this.rbtnUpper.Name = "rbtnUpper";
			this.rbtnUpper.Size = new System.Drawing.Size(139, 18);
			this.rbtnUpper.TabIndex = 2;
			this.rbtnUpper.Text = "ПРОПИСНЫЕ БУКВЫ";
			this.rbtnUpper.UseVisualStyleBackColor = true;
			this.rbtnUpper.Click += new System.EventHandler(this.RbtnAsIsClick);
			// 
			// rbtnLower
			// 
			this.rbtnLower.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnLower.Location = new System.Drawing.Point(83, 16);
			this.rbtnLower.Name = "rbtnLower";
			this.rbtnLower.Size = new System.Drawing.Size(127, 18);
			this.rbtnLower.TabIndex = 1;
			this.rbtnLower.Text = "строчные буквы";
			this.rbtnLower.UseVisualStyleBackColor = true;
			this.rbtnLower.Click += new System.EventHandler(this.RbtnAsIsClick);
			// 
			// rbtnAsIs
			// 
			this.rbtnAsIs.Checked = true;
			this.rbtnAsIs.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnAsIs.Location = new System.Drawing.Point(3, 16);
			this.rbtnAsIs.Name = "rbtnAsIs";
			this.rbtnAsIs.Size = new System.Drawing.Size(78, 18);
			this.rbtnAsIs.TabIndex = 0;
			this.rbtnAsIs.TabStop = true;
			this.rbtnAsIs.Text = "Как есть";
			this.rbtnAsIs.UseVisualStyleBackColor = true;
			this.rbtnAsIs.Click += new System.EventHandler(this.RbtnAsIsClick);
			// 
			// tpFMNoTagsText
			// 
			this.tpFMNoTagsText.Controls.Add(this.tcDesc);
			this.tpFMNoTagsText.Location = new System.Drawing.Point(4, 22);
			this.tpFMNoTagsText.Name = "tpFMNoTagsText";
			this.tpFMNoTagsText.Size = new System.Drawing.Size(619, 525);
			this.tpFMNoTagsText.TabIndex = 2;
			this.tpFMNoTagsText.Text = " Папки шаблонного тэга без данных ";
			this.tpFMNoTagsText.UseVisualStyleBackColor = true;
			// 
			// tcDesc
			// 
			this.tcDesc.Alignment = System.Windows.Forms.TabAlignment.Left;
			this.tcDesc.Controls.Add(this.tpBookInfo);
			this.tcDesc.Controls.Add(this.tpPublishInfo);
			this.tcDesc.Controls.Add(this.tpFB2Info);
			this.tcDesc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcDesc.Location = new System.Drawing.Point(0, 0);
			this.tcDesc.Multiline = true;
			this.tcDesc.Name = "tcDesc";
			this.tcDesc.SelectedIndex = 0;
			this.tcDesc.Size = new System.Drawing.Size(619, 525);
			this.tcDesc.TabIndex = 0;
			// 
			// tpBookInfo
			// 
			this.tpBookInfo.Controls.Add(this.gBoxFMBINoTags);
			this.tpBookInfo.Location = new System.Drawing.Point(24, 4);
			this.tpBookInfo.Name = "tpBookInfo";
			this.tpBookInfo.Padding = new System.Windows.Forms.Padding(3);
			this.tpBookInfo.Size = new System.Drawing.Size(591, 517);
			this.tpBookInfo.TabIndex = 0;
			this.tpBookInfo.Text = " Книга ";
			this.tpBookInfo.UseVisualStyleBackColor = true;
			// 
			// gBoxFMBINoTags
			// 
			this.gBoxFMBINoTags.Controls.Add(this.panel30);
			this.gBoxFMBINoTags.Controls.Add(this.panel29);
			this.gBoxFMBINoTags.Controls.Add(this.panel12);
			this.gBoxFMBINoTags.Controls.Add(this.panel11);
			this.gBoxFMBINoTags.Controls.Add(this.panel10);
			this.gBoxFMBINoTags.Controls.Add(this.panel9);
			this.gBoxFMBINoTags.Controls.Add(this.panel8);
			this.gBoxFMBINoTags.Controls.Add(this.panel7);
			this.gBoxFMBINoTags.Controls.Add(this.panel6);
			this.gBoxFMBINoTags.Controls.Add(this.panel5);
			this.gBoxFMBINoTags.Controls.Add(this.panel4);
			this.gBoxFMBINoTags.Controls.Add(this.panel13);
			this.gBoxFMBINoTags.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gBoxFMBINoTags.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gBoxFMBINoTags.ForeColor = System.Drawing.Color.Maroon;
			this.gBoxFMBINoTags.Location = new System.Drawing.Point(3, 3);
			this.gBoxFMBINoTags.Name = "gBoxFMBINoTags";
			this.gBoxFMBINoTags.Size = new System.Drawing.Size(585, 511);
			this.gBoxFMBINoTags.TabIndex = 1;
			this.gBoxFMBINoTags.TabStop = false;
			this.gBoxFMBINoTags.Text = " Для отсутствующих данных тэгов ";
			// 
			// panel30
			// 
			this.panel30.Controls.Add(this.txtBoxFMNoDateValue);
			this.panel30.Controls.Add(this.label31);
			this.panel30.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel30.Location = new System.Drawing.Point(3, 368);
			this.panel30.Name = "panel30";
			this.panel30.Size = new System.Drawing.Size(579, 32);
			this.panel30.TabIndex = 11;
			// 
			// txtBoxFMNoDateValue
			// 
			this.txtBoxFMNoDateValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoDateValue.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoDateValue.Name = "txtBoxFMNoDateValue";
			this.txtBoxFMNoDateValue.Size = new System.Drawing.Size(369, 20);
			this.txtBoxFMNoDateValue.TabIndex = 1;
			this.txtBoxFMNoDateValue.TextChanged += new System.EventHandler(this.TxtBoxFMNoGenreGroupTextChanged);
			// 
			// label31
			// 
			this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label31.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label31.Location = new System.Drawing.Point(3, 8);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(186, 18);
			this.label31.TabIndex = 0;
			this.label31.Text = "Даты написания (знач.) Нет:";
			this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel29
			// 
			this.panel29.Controls.Add(this.txtBoxFMNoDateText);
			this.panel29.Controls.Add(this.label30);
			this.panel29.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel29.Location = new System.Drawing.Point(3, 336);
			this.panel29.Name = "panel29";
			this.panel29.Size = new System.Drawing.Size(579, 32);
			this.panel29.TabIndex = 10;
			// 
			// txtBoxFMNoDateText
			// 
			this.txtBoxFMNoDateText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoDateText.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoDateText.Name = "txtBoxFMNoDateText";
			this.txtBoxFMNoDateText.Size = new System.Drawing.Size(369, 20);
			this.txtBoxFMNoDateText.TabIndex = 1;
			this.txtBoxFMNoDateText.TextChanged += new System.EventHandler(this.TxtBoxFMNoGenreGroupTextChanged);
			// 
			// label30
			// 
			this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label30.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label30.Location = new System.Drawing.Point(3, 8);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(186, 18);
			this.label30.TabIndex = 0;
			this.label30.Text = "Даты написания (текст) Нет:";
			this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel12
			// 
			this.panel12.Controls.Add(this.txtBoxFMNoNSequence);
			this.panel12.Controls.Add(this.label10);
			this.panel12.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel12.Location = new System.Drawing.Point(3, 304);
			this.panel12.Name = "panel12";
			this.panel12.Size = new System.Drawing.Size(579, 32);
			this.panel12.TabIndex = 9;
			// 
			// txtBoxFMNoNSequence
			// 
			this.txtBoxFMNoNSequence.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoNSequence.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoNSequence.Name = "txtBoxFMNoNSequence";
			this.txtBoxFMNoNSequence.Size = new System.Drawing.Size(369, 20);
			this.txtBoxFMNoNSequence.TabIndex = 1;
			this.txtBoxFMNoNSequence.TextChanged += new System.EventHandler(this.TxtBoxFMNoGenreGroupTextChanged);
			// 
			// label10
			// 
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label10.Location = new System.Drawing.Point(3, 8);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(186, 18);
			this.label10.TabIndex = 0;
			this.label10.Text = "Номера Серии Нет:";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel11
			// 
			this.panel11.Controls.Add(this.txtBoxFMNoSequence);
			this.panel11.Controls.Add(this.label9);
			this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel11.Location = new System.Drawing.Point(3, 272);
			this.panel11.Name = "panel11";
			this.panel11.Size = new System.Drawing.Size(579, 32);
			this.panel11.TabIndex = 8;
			// 
			// txtBoxFMNoSequence
			// 
			this.txtBoxFMNoSequence.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoSequence.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoSequence.Name = "txtBoxFMNoSequence";
			this.txtBoxFMNoSequence.Size = new System.Drawing.Size(369, 20);
			this.txtBoxFMNoSequence.TabIndex = 1;
			this.txtBoxFMNoSequence.TextChanged += new System.EventHandler(this.TxtBoxFMNoGenreGroupTextChanged);
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label9.Location = new System.Drawing.Point(3, 8);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(186, 18);
			this.label9.TabIndex = 0;
			this.label9.Text = "Серии Нет:";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel10
			// 
			this.panel10.Controls.Add(this.txtBoxFMNoBookTitle);
			this.panel10.Controls.Add(this.label8);
			this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel10.Location = new System.Drawing.Point(3, 240);
			this.panel10.Name = "panel10";
			this.panel10.Size = new System.Drawing.Size(579, 32);
			this.panel10.TabIndex = 7;
			// 
			// txtBoxFMNoBookTitle
			// 
			this.txtBoxFMNoBookTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoBookTitle.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoBookTitle.Name = "txtBoxFMNoBookTitle";
			this.txtBoxFMNoBookTitle.Size = new System.Drawing.Size(369, 20);
			this.txtBoxFMNoBookTitle.TabIndex = 1;
			this.txtBoxFMNoBookTitle.TextChanged += new System.EventHandler(this.TxtBoxFMNoGenreGroupTextChanged);
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label8.Location = new System.Drawing.Point(3, 8);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(186, 18);
			this.label8.TabIndex = 0;
			this.label8.Text = "Названия Книги Нет:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel9
			// 
			this.panel9.Controls.Add(this.txtBoxFMNoNickName);
			this.panel9.Controls.Add(this.label7);
			this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel9.Location = new System.Drawing.Point(3, 208);
			this.panel9.Name = "panel9";
			this.panel9.Size = new System.Drawing.Size(579, 32);
			this.panel9.TabIndex = 6;
			// 
			// txtBoxFMNoNickName
			// 
			this.txtBoxFMNoNickName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoNickName.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoNickName.Name = "txtBoxFMNoNickName";
			this.txtBoxFMNoNickName.Size = new System.Drawing.Size(369, 20);
			this.txtBoxFMNoNickName.TabIndex = 1;
			this.txtBoxFMNoNickName.TextChanged += new System.EventHandler(this.TxtBoxFMNoGenreGroupTextChanged);
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label7.Location = new System.Drawing.Point(3, 8);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(186, 18);
			this.label7.TabIndex = 0;
			this.label7.Text = "Ника Автора Нет:";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel8
			// 
			this.panel8.Controls.Add(this.txtBoxFMNoLastName);
			this.panel8.Controls.Add(this.label6);
			this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel8.Location = new System.Drawing.Point(3, 176);
			this.panel8.Name = "panel8";
			this.panel8.Size = new System.Drawing.Size(579, 32);
			this.panel8.TabIndex = 5;
			// 
			// txtBoxFMNoLastName
			// 
			this.txtBoxFMNoLastName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoLastName.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoLastName.Name = "txtBoxFMNoLastName";
			this.txtBoxFMNoLastName.Size = new System.Drawing.Size(369, 20);
			this.txtBoxFMNoLastName.TabIndex = 1;
			this.txtBoxFMNoLastName.TextChanged += new System.EventHandler(this.TxtBoxFMNoGenreGroupTextChanged);
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label6.Location = new System.Drawing.Point(3, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(186, 18);
			this.label6.TabIndex = 0;
			this.label6.Text = "Фамилия Автора Нет:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel7
			// 
			this.panel7.Controls.Add(this.txtBoxFMNoMiddleName);
			this.panel7.Controls.Add(this.label5);
			this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel7.Location = new System.Drawing.Point(3, 144);
			this.panel7.Name = "panel7";
			this.panel7.Size = new System.Drawing.Size(579, 32);
			this.panel7.TabIndex = 4;
			// 
			// txtBoxFMNoMiddleName
			// 
			this.txtBoxFMNoMiddleName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoMiddleName.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoMiddleName.Name = "txtBoxFMNoMiddleName";
			this.txtBoxFMNoMiddleName.Size = new System.Drawing.Size(369, 20);
			this.txtBoxFMNoMiddleName.TabIndex = 1;
			this.txtBoxFMNoMiddleName.TextChanged += new System.EventHandler(this.TxtBoxFMNoGenreGroupTextChanged);
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label5.Location = new System.Drawing.Point(3, 8);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(186, 18);
			this.label5.TabIndex = 0;
			this.label5.Text = "Отчества Автора Нет:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel6
			// 
			this.panel6.Controls.Add(this.txtBoxFMNoFirstName);
			this.panel6.Controls.Add(this.label4);
			this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel6.Location = new System.Drawing.Point(3, 112);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(579, 32);
			this.panel6.TabIndex = 3;
			// 
			// txtBoxFMNoFirstName
			// 
			this.txtBoxFMNoFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoFirstName.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoFirstName.Name = "txtBoxFMNoFirstName";
			this.txtBoxFMNoFirstName.Size = new System.Drawing.Size(369, 20);
			this.txtBoxFMNoFirstName.TabIndex = 1;
			this.txtBoxFMNoFirstName.TextChanged += new System.EventHandler(this.TxtBoxFMNoGenreGroupTextChanged);
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label4.Location = new System.Drawing.Point(3, 8);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(186, 18);
			this.label4.TabIndex = 0;
			this.label4.Text = "Имени Автора Нет:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this.txtBoxFMNoLang);
			this.panel5.Controls.Add(this.label3);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel5.Location = new System.Drawing.Point(3, 80);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(579, 32);
			this.panel5.TabIndex = 2;
			// 
			// txtBoxFMNoLang
			// 
			this.txtBoxFMNoLang.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoLang.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoLang.Name = "txtBoxFMNoLang";
			this.txtBoxFMNoLang.Size = new System.Drawing.Size(369, 20);
			this.txtBoxFMNoLang.TabIndex = 1;
			this.txtBoxFMNoLang.TextChanged += new System.EventHandler(this.TxtBoxFMNoGenreGroupTextChanged);
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label3.Location = new System.Drawing.Point(3, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(186, 18);
			this.label3.TabIndex = 0;
			this.label3.Text = "Языка Книги Нет:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.txtBoxFMNoGenre);
			this.panel4.Controls.Add(this.label2);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel4.Location = new System.Drawing.Point(3, 48);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(579, 32);
			this.panel4.TabIndex = 1;
			// 
			// txtBoxFMNoGenre
			// 
			this.txtBoxFMNoGenre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoGenre.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoGenre.Name = "txtBoxFMNoGenre";
			this.txtBoxFMNoGenre.Size = new System.Drawing.Size(369, 20);
			this.txtBoxFMNoGenre.TabIndex = 1;
			this.txtBoxFMNoGenre.TextChanged += new System.EventHandler(this.TxtBoxFMNoGenreGroupTextChanged);
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label2.Location = new System.Drawing.Point(3, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(186, 18);
			this.label2.TabIndex = 0;
			this.label2.Text = "Жанра Книги Нет:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel13
			// 
			this.panel13.Controls.Add(this.txtBoxFMNoGenreGroup);
			this.panel13.Controls.Add(this.label1);
			this.panel13.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel13.Location = new System.Drawing.Point(3, 16);
			this.panel13.Name = "panel13";
			this.panel13.Size = new System.Drawing.Size(579, 32);
			this.panel13.TabIndex = 0;
			// 
			// txtBoxFMNoGenreGroup
			// 
			this.txtBoxFMNoGenreGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoGenreGroup.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoGenreGroup.Name = "txtBoxFMNoGenreGroup";
			this.txtBoxFMNoGenreGroup.Size = new System.Drawing.Size(369, 20);
			this.txtBoxFMNoGenreGroup.TabIndex = 1;
			this.txtBoxFMNoGenreGroup.TextChanged += new System.EventHandler(this.TxtBoxFMNoGenreGroupTextChanged);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label1.Location = new System.Drawing.Point(3, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(186, 18);
			this.label1.TabIndex = 0;
			this.label1.Text = "Неизвестная Группа Жанров:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tpPublishInfo
			// 
			this.tpPublishInfo.Controls.Add(this.gBoxFMPINoTags);
			this.tpPublishInfo.Location = new System.Drawing.Point(24, 4);
			this.tpPublishInfo.Name = "tpPublishInfo";
			this.tpPublishInfo.Padding = new System.Windows.Forms.Padding(3);
			this.tpPublishInfo.Size = new System.Drawing.Size(591, 517);
			this.tpPublishInfo.TabIndex = 1;
			this.tpPublishInfo.Text = " Издательство ";
			this.tpPublishInfo.UseVisualStyleBackColor = true;
			// 
			// gBoxFMPINoTags
			// 
			this.gBoxFMPINoTags.Controls.Add(this.panel33);
			this.gBoxFMPINoTags.Controls.Add(this.panel32);
			this.gBoxFMPINoTags.Controls.Add(this.panel31);
			this.gBoxFMPINoTags.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gBoxFMPINoTags.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gBoxFMPINoTags.ForeColor = System.Drawing.Color.Maroon;
			this.gBoxFMPINoTags.Location = new System.Drawing.Point(3, 3);
			this.gBoxFMPINoTags.Name = "gBoxFMPINoTags";
			this.gBoxFMPINoTags.Size = new System.Drawing.Size(585, 511);
			this.gBoxFMPINoTags.TabIndex = 0;
			this.gBoxFMPINoTags.TabStop = false;
			this.gBoxFMPINoTags.Text = " Для отсутствующих данных тэгов ";
			// 
			// panel33
			// 
			this.panel33.Controls.Add(this.txtBoxFMNoCity);
			this.panel33.Controls.Add(this.label34);
			this.panel33.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel33.Location = new System.Drawing.Point(3, 80);
			this.panel33.Name = "panel33";
			this.panel33.Size = new System.Drawing.Size(579, 32);
			this.panel33.TabIndex = 15;
			// 
			// txtBoxFMNoCity
			// 
			this.txtBoxFMNoCity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoCity.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoCity.Name = "txtBoxFMNoCity";
			this.txtBoxFMNoCity.Size = new System.Drawing.Size(369, 20);
			this.txtBoxFMNoCity.TabIndex = 1;
			this.txtBoxFMNoCity.TextChanged += new System.EventHandler(this.TxtBoxFMNoYearTextChanged);
			// 
			// label34
			// 
			this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label34.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label34.Location = new System.Drawing.Point(3, 8);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(186, 18);
			this.label34.TabIndex = 0;
			this.label34.Text = "Города Издательства Нет:";
			this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel32
			// 
			this.panel32.Controls.Add(this.txtBoxFMNoPublisher);
			this.panel32.Controls.Add(this.label33);
			this.panel32.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel32.Location = new System.Drawing.Point(3, 48);
			this.panel32.Name = "panel32";
			this.panel32.Size = new System.Drawing.Size(579, 32);
			this.panel32.TabIndex = 14;
			// 
			// txtBoxFMNoPublisher
			// 
			this.txtBoxFMNoPublisher.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoPublisher.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoPublisher.Name = "txtBoxFMNoPublisher";
			this.txtBoxFMNoPublisher.Size = new System.Drawing.Size(369, 20);
			this.txtBoxFMNoPublisher.TabIndex = 1;
			this.txtBoxFMNoPublisher.TextChanged += new System.EventHandler(this.TxtBoxFMNoYearTextChanged);
			// 
			// label33
			// 
			this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label33.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label33.Location = new System.Drawing.Point(3, 8);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(186, 18);
			this.label33.TabIndex = 0;
			this.label33.Text = "Издательства Нет:";
			this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel31
			// 
			this.panel31.Controls.Add(this.txtBoxFMNoYear);
			this.panel31.Controls.Add(this.label32);
			this.panel31.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel31.Location = new System.Drawing.Point(3, 16);
			this.panel31.Name = "panel31";
			this.panel31.Size = new System.Drawing.Size(579, 32);
			this.panel31.TabIndex = 13;
			// 
			// txtBoxFMNoYear
			// 
			this.txtBoxFMNoYear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoYear.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoYear.Name = "txtBoxFMNoYear";
			this.txtBoxFMNoYear.Size = new System.Drawing.Size(369, 20);
			this.txtBoxFMNoYear.TabIndex = 1;
			this.txtBoxFMNoYear.TextChanged += new System.EventHandler(this.TxtBoxFMNoYearTextChanged);
			// 
			// label32
			// 
			this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label32.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label32.Location = new System.Drawing.Point(3, 8);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(186, 18);
			this.label32.TabIndex = 0;
			this.label32.Text = "Года издания Книги Нет:";
			this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tpFB2Info
			// 
			this.tpFB2Info.Controls.Add(this.gBoxFMFB2INoTags);
			this.tpFB2Info.Location = new System.Drawing.Point(24, 4);
			this.tpFB2Info.Name = "tpFB2Info";
			this.tpFB2Info.Padding = new System.Windows.Forms.Padding(3);
			this.tpFB2Info.Size = new System.Drawing.Size(591, 517);
			this.tpFB2Info.TabIndex = 2;
			this.tpFB2Info.Text = " FB2-файл ";
			this.tpFB2Info.UseVisualStyleBackColor = true;
			// 
			// gBoxFMFB2INoTags
			// 
			this.gBoxFMFB2INoTags.Controls.Add(this.panel34);
			this.gBoxFMFB2INoTags.Controls.Add(this.panel35);
			this.gBoxFMFB2INoTags.Controls.Add(this.panel36);
			this.gBoxFMFB2INoTags.Controls.Add(this.panel37);
			this.gBoxFMFB2INoTags.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gBoxFMFB2INoTags.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gBoxFMFB2INoTags.ForeColor = System.Drawing.Color.Maroon;
			this.gBoxFMFB2INoTags.Location = new System.Drawing.Point(3, 3);
			this.gBoxFMFB2INoTags.Name = "gBoxFMFB2INoTags";
			this.gBoxFMFB2INoTags.Size = new System.Drawing.Size(585, 511);
			this.gBoxFMFB2INoTags.TabIndex = 1;
			this.gBoxFMFB2INoTags.TabStop = false;
			this.gBoxFMFB2INoTags.Text = " Для отсутствующих данных тэгов ";
			// 
			// panel34
			// 
			this.panel34.Controls.Add(this.txtBoxFMNoFB2NickName);
			this.panel34.Controls.Add(this.label35);
			this.panel34.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel34.Location = new System.Drawing.Point(3, 112);
			this.panel34.Name = "panel34";
			this.panel34.Size = new System.Drawing.Size(579, 32);
			this.panel34.TabIndex = 14;
			// 
			// txtBoxFMNoFB2NickName
			// 
			this.txtBoxFMNoFB2NickName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoFB2NickName.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoFB2NickName.Name = "txtBoxFMNoFB2NickName";
			this.txtBoxFMNoFB2NickName.Size = new System.Drawing.Size(369, 20);
			this.txtBoxFMNoFB2NickName.TabIndex = 1;
			this.txtBoxFMNoFB2NickName.TextChanged += new System.EventHandler(this.TxtBoxFMNoFB2FirstNameTextChanged);
			// 
			// label35
			// 
			this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label35.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label35.Location = new System.Drawing.Point(3, 8);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(186, 18);
			this.label35.TabIndex = 0;
			this.label35.Text = "Ника fb2-создателя Нет:";
			this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel35
			// 
			this.panel35.Controls.Add(this.txtBoxFMNoFB2LastName);
			this.panel35.Controls.Add(this.label36);
			this.panel35.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel35.Location = new System.Drawing.Point(3, 80);
			this.panel35.Name = "panel35";
			this.panel35.Size = new System.Drawing.Size(579, 32);
			this.panel35.TabIndex = 13;
			// 
			// txtBoxFMNoFB2LastName
			// 
			this.txtBoxFMNoFB2LastName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoFB2LastName.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoFB2LastName.Name = "txtBoxFMNoFB2LastName";
			this.txtBoxFMNoFB2LastName.Size = new System.Drawing.Size(369, 20);
			this.txtBoxFMNoFB2LastName.TabIndex = 1;
			this.txtBoxFMNoFB2LastName.TextChanged += new System.EventHandler(this.TxtBoxFMNoFB2FirstNameTextChanged);
			// 
			// label36
			// 
			this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label36.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label36.Location = new System.Drawing.Point(3, 8);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(186, 18);
			this.label36.TabIndex = 0;
			this.label36.Text = "Фамилия fb2-создателя Нет:";
			this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel36
			// 
			this.panel36.Controls.Add(this.txtBoxFMNoFB2MiddleName);
			this.panel36.Controls.Add(this.label37);
			this.panel36.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel36.Location = new System.Drawing.Point(3, 48);
			this.panel36.Name = "panel36";
			this.panel36.Size = new System.Drawing.Size(579, 32);
			this.panel36.TabIndex = 12;
			// 
			// txtBoxFMNoFB2MiddleName
			// 
			this.txtBoxFMNoFB2MiddleName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoFB2MiddleName.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoFB2MiddleName.Name = "txtBoxFMNoFB2MiddleName";
			this.txtBoxFMNoFB2MiddleName.Size = new System.Drawing.Size(369, 20);
			this.txtBoxFMNoFB2MiddleName.TabIndex = 1;
			this.txtBoxFMNoFB2MiddleName.TextChanged += new System.EventHandler(this.TxtBoxFMNoFB2FirstNameTextChanged);
			// 
			// label37
			// 
			this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label37.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label37.Location = new System.Drawing.Point(3, 8);
			this.label37.Name = "label37";
			this.label37.Size = new System.Drawing.Size(186, 18);
			this.label37.TabIndex = 0;
			this.label37.Text = "Отчества fb2-создателя Нет:";
			this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel37
			// 
			this.panel37.Controls.Add(this.txtBoxFMNoFB2FirstName);
			this.panel37.Controls.Add(this.label38);
			this.panel37.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel37.Location = new System.Drawing.Point(3, 16);
			this.panel37.Name = "panel37";
			this.panel37.Size = new System.Drawing.Size(579, 32);
			this.panel37.TabIndex = 11;
			// 
			// txtBoxFMNoFB2FirstName
			// 
			this.txtBoxFMNoFB2FirstName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoFB2FirstName.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoFB2FirstName.Name = "txtBoxFMNoFB2FirstName";
			this.txtBoxFMNoFB2FirstName.Size = new System.Drawing.Size(369, 20);
			this.txtBoxFMNoFB2FirstName.TabIndex = 1;
			this.txtBoxFMNoFB2FirstName.TextChanged += new System.EventHandler(this.TxtBoxFMNoFB2FirstNameTextChanged);
			// 
			// label38
			// 
			this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label38.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label38.Location = new System.Drawing.Point(3, 8);
			this.label38.Name = "label38";
			this.label38.Size = new System.Drawing.Size(186, 18);
			this.label38.TabIndex = 0;
			this.label38.Text = "Имени fb2-создателя Нет:";
			this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tpFMGenreGroups
			// 
			this.tpFMGenreGroups.Controls.Add(this.panel41);
			this.tpFMGenreGroups.Controls.Add(this.panel40);
			this.tpFMGenreGroups.Controls.Add(this.panel39);
			this.tpFMGenreGroups.Controls.Add(this.panel38);
			this.tpFMGenreGroups.Controls.Add(this.panel28);
			this.tpFMGenreGroups.Controls.Add(this.panel27);
			this.tpFMGenreGroups.Controls.Add(this.panel26);
			this.tpFMGenreGroups.Controls.Add(this.panel25);
			this.tpFMGenreGroups.Controls.Add(this.panel24);
			this.tpFMGenreGroups.Controls.Add(this.panel23);
			this.tpFMGenreGroups.Controls.Add(this.panel22);
			this.tpFMGenreGroups.Controls.Add(this.panel21);
			this.tpFMGenreGroups.Controls.Add(this.panel20);
			this.tpFMGenreGroups.Controls.Add(this.panel19);
			this.tpFMGenreGroups.Controls.Add(this.panel18);
			this.tpFMGenreGroups.Controls.Add(this.panel17);
			this.tpFMGenreGroups.Controls.Add(this.panel16);
			this.tpFMGenreGroups.Controls.Add(this.panel15);
			this.tpFMGenreGroups.Controls.Add(this.panel14);
			this.tpFMGenreGroups.Controls.Add(this.panel42);
			this.tpFMGenreGroups.Location = new System.Drawing.Point(4, 22);
			this.tpFMGenreGroups.Name = "tpFMGenreGroups";
			this.tpFMGenreGroups.Padding = new System.Windows.Forms.Padding(3);
			this.tpFMGenreGroups.Size = new System.Drawing.Size(619, 525);
			this.tpFMGenreGroups.TabIndex = 3;
			this.tpFMGenreGroups.Text = " Группы Жанров ";
			this.tpFMGenreGroups.UseVisualStyleBackColor = true;
			// 
			// panel41
			// 
			this.panel41.Controls.Add(this.txtboxFMother);
			this.panel41.Controls.Add(this.label40);
			this.panel41.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel41.Location = new System.Drawing.Point(3, 497);
			this.panel41.Name = "panel41";
			this.panel41.Size = new System.Drawing.Size(613, 26);
			this.panel41.TabIndex = 20;
			// 
			// txtboxFMother
			// 
			this.txtboxFMother.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtboxFMother.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMother.Location = new System.Drawing.Point(139, 3);
			this.txtboxFMother.Name = "txtboxFMother";
			this.txtboxFMother.Size = new System.Drawing.Size(458, 20);
			this.txtboxFMother.TabIndex = 7;
			this.txtboxFMother.TextChanged += new System.EventHandler(this.TxtboxFMsfTextChanged);
			// 
			// label40
			// 
			this.label40.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label40.ForeColor = System.Drawing.Color.Blue;
			this.label40.Location = new System.Drawing.Point(55, 6);
			this.label40.Name = "label40";
			this.label40.Size = new System.Drawing.Size(80, 16);
			this.label40.TabIndex = 6;
			this.label40.Text = "Прочее:";
			this.label40.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel40
			// 
			this.panel40.Controls.Add(this.txtboxFMfolklore);
			this.panel40.Controls.Add(this.label39);
			this.panel40.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel40.Location = new System.Drawing.Point(3, 471);
			this.panel40.Name = "panel40";
			this.panel40.Size = new System.Drawing.Size(613, 26);
			this.panel40.TabIndex = 19;
			// 
			// txtboxFMfolklore
			// 
			this.txtboxFMfolklore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtboxFMfolklore.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMfolklore.Location = new System.Drawing.Point(139, 3);
			this.txtboxFMfolklore.Name = "txtboxFMfolklore";
			this.txtboxFMfolklore.Size = new System.Drawing.Size(458, 20);
			this.txtboxFMfolklore.TabIndex = 7;
			this.txtboxFMfolklore.TextChanged += new System.EventHandler(this.TxtboxFMsfTextChanged);
			// 
			// label39
			// 
			this.label39.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label39.ForeColor = System.Drawing.Color.Blue;
			this.label39.Location = new System.Drawing.Point(55, 6);
			this.label39.Name = "label39";
			this.label39.Size = new System.Drawing.Size(80, 16);
			this.label39.TabIndex = 6;
			this.label39.Text = "Фольклор:";
			this.label39.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel39
			// 
			this.panel39.Controls.Add(this.txtboxFMmilitary);
			this.panel39.Controls.Add(this.label13);
			this.panel39.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel39.Location = new System.Drawing.Point(3, 445);
			this.panel39.Name = "panel39";
			this.panel39.Size = new System.Drawing.Size(613, 26);
			this.panel39.TabIndex = 18;
			// 
			// txtboxFMmilitary
			// 
			this.txtboxFMmilitary.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtboxFMmilitary.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMmilitary.Location = new System.Drawing.Point(139, 3);
			this.txtboxFMmilitary.Name = "txtboxFMmilitary";
			this.txtboxFMmilitary.Size = new System.Drawing.Size(458, 20);
			this.txtboxFMmilitary.TabIndex = 7;
			this.txtboxFMmilitary.TextChanged += new System.EventHandler(this.TxtboxFMsfTextChanged);
			// 
			// label13
			// 
			this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label13.ForeColor = System.Drawing.Color.Blue;
			this.label13.Location = new System.Drawing.Point(55, 6);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(80, 16);
			this.label13.TabIndex = 6;
			this.label13.Text = "Военное дело:";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel38
			// 
			this.panel38.Controls.Add(this.txtboxFMtech);
			this.panel38.Controls.Add(this.label12);
			this.panel38.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel38.Location = new System.Drawing.Point(3, 419);
			this.panel38.Name = "panel38";
			this.panel38.Size = new System.Drawing.Size(613, 26);
			this.panel38.TabIndex = 17;
			// 
			// txtboxFMtech
			// 
			this.txtboxFMtech.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtboxFMtech.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMtech.Location = new System.Drawing.Point(139, 3);
			this.txtboxFMtech.Name = "txtboxFMtech";
			this.txtboxFMtech.Size = new System.Drawing.Size(458, 20);
			this.txtboxFMtech.TabIndex = 5;
			this.txtboxFMtech.TextChanged += new System.EventHandler(this.TxtboxFMsfTextChanged);
			// 
			// label12
			// 
			this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label12.ForeColor = System.Drawing.Color.Blue;
			this.label12.Location = new System.Drawing.Point(55, 6);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(80, 16);
			this.label12.TabIndex = 4;
			this.label12.Text = "Техника:";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel28
			// 
			this.panel28.Controls.Add(this.txtboxFMbusiness);
			this.panel28.Controls.Add(this.label29);
			this.panel28.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel28.Location = new System.Drawing.Point(3, 393);
			this.panel28.Name = "panel28";
			this.panel28.Size = new System.Drawing.Size(613, 26);
			this.panel28.TabIndex = 16;
			// 
			// txtboxFMbusiness
			// 
			this.txtboxFMbusiness.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtboxFMbusiness.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMbusiness.Location = new System.Drawing.Point(139, 2);
			this.txtboxFMbusiness.Name = "txtboxFMbusiness";
			this.txtboxFMbusiness.Size = new System.Drawing.Size(458, 20);
			this.txtboxFMbusiness.TabIndex = 1;
			this.txtboxFMbusiness.TextChanged += new System.EventHandler(this.TxtboxFMsfTextChanged);
			// 
			// label29
			// 
			this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label29.ForeColor = System.Drawing.Color.Blue;
			this.label29.Location = new System.Drawing.Point(3, 5);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(130, 16);
			this.label29.TabIndex = 0;
			this.label29.Text = "Бизнес:";
			this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel27
			// 
			this.panel27.Controls.Add(this.txtboxFMhome);
			this.panel27.Controls.Add(this.label28);
			this.panel27.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel27.Location = new System.Drawing.Point(3, 367);
			this.panel27.Name = "panel27";
			this.panel27.Size = new System.Drawing.Size(613, 26);
			this.panel27.TabIndex = 15;
			// 
			// txtboxFMhome
			// 
			this.txtboxFMhome.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtboxFMhome.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMhome.Location = new System.Drawing.Point(139, 2);
			this.txtboxFMhome.Name = "txtboxFMhome";
			this.txtboxFMhome.Size = new System.Drawing.Size(458, 20);
			this.txtboxFMhome.TabIndex = 1;
			this.txtboxFMhome.TextChanged += new System.EventHandler(this.TxtboxFMsfTextChanged);
			// 
			// label28
			// 
			this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label28.ForeColor = System.Drawing.Color.Blue;
			this.label28.Location = new System.Drawing.Point(3, 5);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(130, 16);
			this.label28.TabIndex = 0;
			this.label28.Text = "Дом, Семья:";
			this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel26
			// 
			this.panel26.Controls.Add(this.txtboxFMhumor);
			this.panel26.Controls.Add(this.label27);
			this.panel26.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel26.Location = new System.Drawing.Point(3, 341);
			this.panel26.Name = "panel26";
			this.panel26.Size = new System.Drawing.Size(613, 26);
			this.panel26.TabIndex = 14;
			// 
			// txtboxFMhumor
			// 
			this.txtboxFMhumor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtboxFMhumor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMhumor.Location = new System.Drawing.Point(139, 2);
			this.txtboxFMhumor.Name = "txtboxFMhumor";
			this.txtboxFMhumor.Size = new System.Drawing.Size(458, 20);
			this.txtboxFMhumor.TabIndex = 1;
			this.txtboxFMhumor.TextChanged += new System.EventHandler(this.TxtboxFMsfTextChanged);
			// 
			// label27
			// 
			this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label27.ForeColor = System.Drawing.Color.Blue;
			this.label27.Location = new System.Drawing.Point(3, 5);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(130, 16);
			this.label27.TabIndex = 0;
			this.label27.Text = "Юмор:";
			this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel25
			// 
			this.panel25.Controls.Add(this.txtboxFMreligion);
			this.panel25.Controls.Add(this.label26);
			this.panel25.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel25.Location = new System.Drawing.Point(3, 315);
			this.panel25.Name = "panel25";
			this.panel25.Size = new System.Drawing.Size(613, 26);
			this.panel25.TabIndex = 13;
			// 
			// txtboxFMreligion
			// 
			this.txtboxFMreligion.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtboxFMreligion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMreligion.Location = new System.Drawing.Point(139, 2);
			this.txtboxFMreligion.Name = "txtboxFMreligion";
			this.txtboxFMreligion.Size = new System.Drawing.Size(458, 20);
			this.txtboxFMreligion.TabIndex = 1;
			this.txtboxFMreligion.TextChanged += new System.EventHandler(this.TxtboxFMsfTextChanged);
			// 
			// label26
			// 
			this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label26.ForeColor = System.Drawing.Color.Blue;
			this.label26.Location = new System.Drawing.Point(3, 5);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(130, 16);
			this.label26.TabIndex = 0;
			this.label26.Text = "Религия:";
			this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel24
			// 
			this.panel24.Controls.Add(this.txtboxFMnonfiction);
			this.panel24.Controls.Add(this.label25);
			this.panel24.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel24.Location = new System.Drawing.Point(3, 289);
			this.panel24.Name = "panel24";
			this.panel24.Size = new System.Drawing.Size(613, 26);
			this.panel24.TabIndex = 12;
			// 
			// txtboxFMnonfiction
			// 
			this.txtboxFMnonfiction.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtboxFMnonfiction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMnonfiction.Location = new System.Drawing.Point(139, 2);
			this.txtboxFMnonfiction.Name = "txtboxFMnonfiction";
			this.txtboxFMnonfiction.Size = new System.Drawing.Size(458, 20);
			this.txtboxFMnonfiction.TabIndex = 1;
			this.txtboxFMnonfiction.TextChanged += new System.EventHandler(this.TxtboxFMsfTextChanged);
			// 
			// label25
			// 
			this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label25.ForeColor = System.Drawing.Color.Blue;
			this.label25.Location = new System.Drawing.Point(3, 5);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(130, 16);
			this.label25.TabIndex = 0;
			this.label25.Text = "Документальное:";
			this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel23
			// 
			this.panel23.Controls.Add(this.txtboxFMreference);
			this.panel23.Controls.Add(this.label24);
			this.panel23.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel23.Location = new System.Drawing.Point(3, 263);
			this.panel23.Name = "panel23";
			this.panel23.Size = new System.Drawing.Size(613, 26);
			this.panel23.TabIndex = 11;
			// 
			// txtboxFMreference
			// 
			this.txtboxFMreference.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtboxFMreference.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMreference.Location = new System.Drawing.Point(139, 2);
			this.txtboxFMreference.Name = "txtboxFMreference";
			this.txtboxFMreference.Size = new System.Drawing.Size(458, 20);
			this.txtboxFMreference.TabIndex = 1;
			this.txtboxFMreference.TextChanged += new System.EventHandler(this.TxtboxFMsfTextChanged);
			// 
			// label24
			// 
			this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label24.ForeColor = System.Drawing.Color.Blue;
			this.label24.Location = new System.Drawing.Point(3, 5);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(130, 16);
			this.label24.TabIndex = 0;
			this.label24.Text = "Справочники:";
			this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel22
			// 
			this.panel22.Controls.Add(this.txtboxFMcomputers);
			this.panel22.Controls.Add(this.label23);
			this.panel22.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel22.Location = new System.Drawing.Point(3, 237);
			this.panel22.Name = "panel22";
			this.panel22.Size = new System.Drawing.Size(613, 26);
			this.panel22.TabIndex = 10;
			// 
			// txtboxFMcomputers
			// 
			this.txtboxFMcomputers.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtboxFMcomputers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMcomputers.Location = new System.Drawing.Point(139, 2);
			this.txtboxFMcomputers.Name = "txtboxFMcomputers";
			this.txtboxFMcomputers.Size = new System.Drawing.Size(458, 20);
			this.txtboxFMcomputers.TabIndex = 1;
			this.txtboxFMcomputers.TextChanged += new System.EventHandler(this.TxtboxFMsfTextChanged);
			// 
			// label23
			// 
			this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label23.ForeColor = System.Drawing.Color.Blue;
			this.label23.Location = new System.Drawing.Point(3, 5);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(130, 16);
			this.label23.TabIndex = 0;
			this.label23.Text = "Компьютеры:";
			this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel21
			// 
			this.panel21.Controls.Add(this.txtboxFMscience);
			this.panel21.Controls.Add(this.label22);
			this.panel21.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel21.Location = new System.Drawing.Point(3, 211);
			this.panel21.Name = "panel21";
			this.panel21.Size = new System.Drawing.Size(613, 26);
			this.panel21.TabIndex = 9;
			// 
			// txtboxFMscience
			// 
			this.txtboxFMscience.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtboxFMscience.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMscience.Location = new System.Drawing.Point(139, 2);
			this.txtboxFMscience.Name = "txtboxFMscience";
			this.txtboxFMscience.Size = new System.Drawing.Size(458, 20);
			this.txtboxFMscience.TabIndex = 1;
			this.txtboxFMscience.TextChanged += new System.EventHandler(this.TxtboxFMsfTextChanged);
			// 
			// label22
			// 
			this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label22.ForeColor = System.Drawing.Color.Blue;
			this.label22.Location = new System.Drawing.Point(3, 5);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(130, 16);
			this.label22.TabIndex = 0;
			this.label22.Text = "Наука, Образование:";
			this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel20
			// 
			this.panel20.Controls.Add(this.txtboxFMantique);
			this.panel20.Controls.Add(this.label21);
			this.panel20.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel20.Location = new System.Drawing.Point(3, 185);
			this.panel20.Name = "panel20";
			this.panel20.Size = new System.Drawing.Size(613, 26);
			this.panel20.TabIndex = 8;
			// 
			// txtboxFMantique
			// 
			this.txtboxFMantique.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtboxFMantique.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMantique.Location = new System.Drawing.Point(139, 2);
			this.txtboxFMantique.Name = "txtboxFMantique";
			this.txtboxFMantique.Size = new System.Drawing.Size(458, 20);
			this.txtboxFMantique.TabIndex = 1;
			this.txtboxFMantique.TextChanged += new System.EventHandler(this.TxtboxFMsfTextChanged);
			// 
			// label21
			// 
			this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label21.ForeColor = System.Drawing.Color.Blue;
			this.label21.Location = new System.Drawing.Point(3, 5);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(130, 16);
			this.label21.TabIndex = 0;
			this.label21.Text = "Старинное:";
			this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel19
			// 
			this.panel19.Controls.Add(this.txtboxFMpoetry);
			this.panel19.Controls.Add(this.label20);
			this.panel19.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel19.Location = new System.Drawing.Point(3, 159);
			this.panel19.Name = "panel19";
			this.panel19.Size = new System.Drawing.Size(613, 26);
			this.panel19.TabIndex = 7;
			// 
			// txtboxFMpoetry
			// 
			this.txtboxFMpoetry.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtboxFMpoetry.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMpoetry.Location = new System.Drawing.Point(139, 2);
			this.txtboxFMpoetry.Name = "txtboxFMpoetry";
			this.txtboxFMpoetry.Size = new System.Drawing.Size(458, 20);
			this.txtboxFMpoetry.TabIndex = 1;
			this.txtboxFMpoetry.TextChanged += new System.EventHandler(this.TxtboxFMsfTextChanged);
			// 
			// label20
			// 
			this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label20.ForeColor = System.Drawing.Color.Blue;
			this.label20.Location = new System.Drawing.Point(3, 5);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(130, 16);
			this.label20.TabIndex = 0;
			this.label20.Text = "Поэзия, Драматургия:";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel18
			// 
			this.panel18.Controls.Add(this.txtboxFMchildren);
			this.panel18.Controls.Add(this.label19);
			this.panel18.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel18.Location = new System.Drawing.Point(3, 133);
			this.panel18.Name = "panel18";
			this.panel18.Size = new System.Drawing.Size(613, 26);
			this.panel18.TabIndex = 6;
			// 
			// txtboxFMchildren
			// 
			this.txtboxFMchildren.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtboxFMchildren.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMchildren.Location = new System.Drawing.Point(139, 2);
			this.txtboxFMchildren.Name = "txtboxFMchildren";
			this.txtboxFMchildren.Size = new System.Drawing.Size(458, 20);
			this.txtboxFMchildren.TabIndex = 1;
			this.txtboxFMchildren.TextChanged += new System.EventHandler(this.TxtboxFMsfTextChanged);
			// 
			// label19
			// 
			this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label19.ForeColor = System.Drawing.Color.Blue;
			this.label19.Location = new System.Drawing.Point(3, 5);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(130, 16);
			this.label19.TabIndex = 0;
			this.label19.Text = "Детское:";
			this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel17
			// 
			this.panel17.Controls.Add(this.txtboxFMadventure);
			this.panel17.Controls.Add(this.label18);
			this.panel17.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel17.Location = new System.Drawing.Point(3, 107);
			this.panel17.Name = "panel17";
			this.panel17.Size = new System.Drawing.Size(613, 26);
			this.panel17.TabIndex = 5;
			// 
			// txtboxFMadventure
			// 
			this.txtboxFMadventure.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtboxFMadventure.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMadventure.Location = new System.Drawing.Point(139, 2);
			this.txtboxFMadventure.Name = "txtboxFMadventure";
			this.txtboxFMadventure.Size = new System.Drawing.Size(458, 20);
			this.txtboxFMadventure.TabIndex = 1;
			this.txtboxFMadventure.TextChanged += new System.EventHandler(this.TxtboxFMsfTextChanged);
			// 
			// label18
			// 
			this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label18.ForeColor = System.Drawing.Color.Blue;
			this.label18.Location = new System.Drawing.Point(3, 5);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(130, 16);
			this.label18.TabIndex = 0;
			this.label18.Text = "Приключения:";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel16
			// 
			this.panel16.Controls.Add(this.txtboxFMlove);
			this.panel16.Controls.Add(this.label17);
			this.panel16.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel16.Location = new System.Drawing.Point(3, 81);
			this.panel16.Name = "panel16";
			this.panel16.Size = new System.Drawing.Size(613, 26);
			this.panel16.TabIndex = 4;
			// 
			// txtboxFMlove
			// 
			this.txtboxFMlove.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtboxFMlove.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMlove.Location = new System.Drawing.Point(139, 2);
			this.txtboxFMlove.Name = "txtboxFMlove";
			this.txtboxFMlove.Size = new System.Drawing.Size(458, 20);
			this.txtboxFMlove.TabIndex = 1;
			this.txtboxFMlove.TextChanged += new System.EventHandler(this.TxtboxFMsfTextChanged);
			// 
			// label17
			// 
			this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label17.ForeColor = System.Drawing.Color.Blue;
			this.label17.Location = new System.Drawing.Point(3, 5);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(130, 16);
			this.label17.TabIndex = 0;
			this.label17.Text = "Любовные романы:";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel15
			// 
			this.panel15.Controls.Add(this.txtboxFMprose);
			this.panel15.Controls.Add(this.label16);
			this.panel15.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel15.Location = new System.Drawing.Point(3, 55);
			this.panel15.Name = "panel15";
			this.panel15.Size = new System.Drawing.Size(613, 26);
			this.panel15.TabIndex = 3;
			// 
			// txtboxFMprose
			// 
			this.txtboxFMprose.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtboxFMprose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMprose.Location = new System.Drawing.Point(139, 2);
			this.txtboxFMprose.Name = "txtboxFMprose";
			this.txtboxFMprose.Size = new System.Drawing.Size(458, 20);
			this.txtboxFMprose.TabIndex = 1;
			this.txtboxFMprose.TextChanged += new System.EventHandler(this.TxtboxFMsfTextChanged);
			// 
			// label16
			// 
			this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label16.ForeColor = System.Drawing.Color.Blue;
			this.label16.Location = new System.Drawing.Point(3, 5);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(130, 16);
			this.label16.TabIndex = 0;
			this.label16.Text = "Проза:";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel14
			// 
			this.panel14.Controls.Add(this.txtboxFMdetective);
			this.panel14.Controls.Add(this.label15);
			this.panel14.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel14.Location = new System.Drawing.Point(3, 29);
			this.panel14.Name = "panel14";
			this.panel14.Size = new System.Drawing.Size(613, 26);
			this.panel14.TabIndex = 2;
			// 
			// txtboxFMdetective
			// 
			this.txtboxFMdetective.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtboxFMdetective.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMdetective.Location = new System.Drawing.Point(139, 2);
			this.txtboxFMdetective.Name = "txtboxFMdetective";
			this.txtboxFMdetective.Size = new System.Drawing.Size(458, 20);
			this.txtboxFMdetective.TabIndex = 1;
			this.txtboxFMdetective.TextChanged += new System.EventHandler(this.TxtboxFMsfTextChanged);
			// 
			// label15
			// 
			this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label15.ForeColor = System.Drawing.Color.Blue;
			this.label15.Location = new System.Drawing.Point(3, 5);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(130, 16);
			this.label15.TabIndex = 0;
			this.label15.Text = "Детективы, Боевики:";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel42
			// 
			this.panel42.Controls.Add(this.txtboxFMsf);
			this.panel42.Controls.Add(this.label14);
			this.panel42.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel42.Location = new System.Drawing.Point(3, 3);
			this.panel42.Name = "panel42";
			this.panel42.Size = new System.Drawing.Size(613, 26);
			this.panel42.TabIndex = 1;
			// 
			// txtboxFMsf
			// 
			this.txtboxFMsf.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtboxFMsf.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMsf.Location = new System.Drawing.Point(139, 2);
			this.txtboxFMsf.Name = "txtboxFMsf";
			this.txtboxFMsf.Size = new System.Drawing.Size(458, 20);
			this.txtboxFMsf.TabIndex = 1;
			this.txtboxFMsf.TextChanged += new System.EventHandler(this.TxtboxFMsfTextChanged);
			// 
			// label14
			// 
			this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label14.ForeColor = System.Drawing.Color.Blue;
			this.label14.Location = new System.Drawing.Point(3, 5);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(130, 16);
			this.label14.TabIndex = 0;
			this.label14.Text = "Фантастика, Фэнтэзи:";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnDefRestore);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(3, 554);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(627, 44);
			this.panel1.TabIndex = 0;
			// 
			// btnDefRestore
			// 
			this.btnDefRestore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.btnDefRestore.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
			this.btnDefRestore.ForeColor = System.Drawing.Color.Navy;
			this.btnDefRestore.Location = new System.Drawing.Point(188, 7);
			this.btnDefRestore.Name = "btnDefRestore";
			this.btnDefRestore.Size = new System.Drawing.Size(267, 36);
			this.btnDefRestore.TabIndex = 5;
			this.btnDefRestore.Text = "Восстановить первоначальные значения (для каждой вкладки отдельно)";
			this.btnDefRestore.UseVisualStyleBackColor = true;
			this.btnDefRestore.Click += new System.EventHandler(this.BtnDefRestoreClick);
			// 
			// sfdSaveXMLFile
			// 
			this.sfdSaveXMLFile.RestoreDirectory = true;
			this.sfdSaveXMLFile.Title = "Сохранение Данных для Избранной Сортировки в файл";
			// 
			// sfdOpenXMLFile
			// 
			this.sfdOpenXMLFile.RestoreDirectory = true;
			this.sfdOpenXMLFile.Title = "Загрузка Данных для Избранной Сортировки из файла";
			// 
			// sfdLoadList
			// 
			this.sfdLoadList.RestoreDirectory = true;
			this.sfdLoadList.Title = "Загрузка Списка Сортировщика книг";
			// 
			// panelProgress
			// 
			this.panelProgress.Controls.Add(this.lvFilesCount);
			this.panelProgress.Controls.Add(this.chBoxViewProgress);
			this.panelProgress.Dock = System.Windows.Forms.DockStyle.Right;
			this.panelProgress.Location = new System.Drawing.Point(925, 0);
			this.panelProgress.Name = "panelProgress";
			this.panelProgress.Size = new System.Drawing.Size(331, 628);
			this.panelProgress.TabIndex = 32;
			// 
			// lvFilesCount
			// 
			this.lvFilesCount.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.columnHeader6,
									this.columnHeader7});
			this.lvFilesCount.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvFilesCount.FullRowSelect = true;
			this.lvFilesCount.GridLines = true;
			this.lvFilesCount.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
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
									listViewItem36});
			this.lvFilesCount.Location = new System.Drawing.Point(0, 24);
			this.lvFilesCount.Name = "lvFilesCount";
			this.lvFilesCount.Size = new System.Drawing.Size(331, 604);
			this.lvFilesCount.TabIndex = 27;
			this.lvFilesCount.UseCompatibleStateImageBehavior = false;
			this.lvFilesCount.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Папки и файлы";
			this.columnHeader6.Width = 238;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Количество";
			this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader7.Width = 72;
			// 
			// chBoxViewProgress
			// 
			this.chBoxViewProgress.Dock = System.Windows.Forms.DockStyle.Top;
			this.chBoxViewProgress.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxViewProgress.Location = new System.Drawing.Point(0, 0);
			this.chBoxViewProgress.Name = "chBoxViewProgress";
			this.chBoxViewProgress.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
			this.chBoxViewProgress.Size = new System.Drawing.Size(331, 24);
			this.chBoxViewProgress.TabIndex = 26;
			this.chBoxViewProgress.Text = "Отображать изменение хода работы";
			this.chBoxViewProgress.UseVisualStyleBackColor = true;
			this.chBoxViewProgress.Click += new System.EventHandler(this.ChBoxViewProgressClick);
			// 
			// labelTarget
			// 
			this.labelTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelTarget.Location = new System.Drawing.Point(3, 31);
			this.labelTarget.Name = "labelTarget";
			this.labelTarget.Size = new System.Drawing.Size(115, 19);
			this.labelTarget.TabIndex = 8;
			this.labelTarget.Text = "Папка-приемник:";
			this.labelTarget.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelTargetPath
			// 
			this.labelTargetPath.ForeColor = System.Drawing.SystemColors.ControlText;
			this.labelTargetPath.Location = new System.Drawing.Point(121, 33);
			this.labelTargetPath.Name = "labelTargetPath";
			this.labelTargetPath.Size = new System.Drawing.Size(629, 17);
			this.labelTargetPath.TabIndex = 9;
			this.labelTargetPath.Text = "labelTargetPath";
			// 
			// SFBTpFileManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panelProgress);
			this.Controls.Add(this.tcSort);
			this.Controls.Add(this.ssProgress);
			this.Name = "SFBTpFileManager";
			this.Size = new System.Drawing.Size(1256, 650);
			this.tcSort.ResumeLayout(false);
			this.tpFullSort.ResumeLayout(false);
			this.cmsItems.ResumeLayout(false);
			this.panelExplorer.ResumeLayout(false);
			this.panelAddress.ResumeLayout(false);
			this.panelAddress.PerformLayout();
			this.panelTemplate.ResumeLayout(false);
			this.gBoxFullSortOptions.ResumeLayout(false);
			this.gBoxFullSortRenameTemplates.ResumeLayout(false);
			this.gBoxFullSortRenameTemplates.PerformLayout();
			this.tpSelectedSort.ResumeLayout(false);
			this.panelLV.ResumeLayout(false);
			this.pSSData.ResumeLayout(false);
			this.pSelectedSortDirs.ResumeLayout(false);
			this.pSelectedSortDirs.PerformLayout();
			this.pSSTemplate.ResumeLayout(false);
			this.gBoxSelectedlSortOptions.ResumeLayout(false);
			this.gBoxSelectedlSortRenameTemplates.ResumeLayout(false);
			this.gBoxSelectedlSortRenameTemplates.PerformLayout();
			this.tcTemplates.ResumeLayout(false);
			this.tpSettings.ResumeLayout(false);
			this.tcFM.ResumeLayout(false);
			this.tpFMGeneral.ResumeLayout(false);
			this.gboxApportionment.ResumeLayout(false);
			this.gBoxGenres.ResumeLayout(false);
			this.gBoxGenresType.ResumeLayout(false);
			this.gBoxGenresCount.ResumeLayout(false);
			this.gBoxAuthors.ResumeLayout(false);
			this.gboxFMGeneral.ResumeLayout(false);
			this.pSortFB2.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.gboxRegister.ResumeLayout(false);
			this.tpFMNoTagsText.ResumeLayout(false);
			this.tcDesc.ResumeLayout(false);
			this.tpBookInfo.ResumeLayout(false);
			this.gBoxFMBINoTags.ResumeLayout(false);
			this.panel30.ResumeLayout(false);
			this.panel30.PerformLayout();
			this.panel29.ResumeLayout(false);
			this.panel29.PerformLayout();
			this.panel12.ResumeLayout(false);
			this.panel12.PerformLayout();
			this.panel11.ResumeLayout(false);
			this.panel11.PerformLayout();
			this.panel10.ResumeLayout(false);
			this.panel10.PerformLayout();
			this.panel9.ResumeLayout(false);
			this.panel9.PerformLayout();
			this.panel8.ResumeLayout(false);
			this.panel8.PerformLayout();
			this.panel7.ResumeLayout(false);
			this.panel7.PerformLayout();
			this.panel6.ResumeLayout(false);
			this.panel6.PerformLayout();
			this.panel5.ResumeLayout(false);
			this.panel5.PerformLayout();
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.panel13.ResumeLayout(false);
			this.panel13.PerformLayout();
			this.tpPublishInfo.ResumeLayout(false);
			this.gBoxFMPINoTags.ResumeLayout(false);
			this.panel33.ResumeLayout(false);
			this.panel33.PerformLayout();
			this.panel32.ResumeLayout(false);
			this.panel32.PerformLayout();
			this.panel31.ResumeLayout(false);
			this.panel31.PerformLayout();
			this.tpFB2Info.ResumeLayout(false);
			this.gBoxFMFB2INoTags.ResumeLayout(false);
			this.panel34.ResumeLayout(false);
			this.panel34.PerformLayout();
			this.panel35.ResumeLayout(false);
			this.panel35.PerformLayout();
			this.panel36.ResumeLayout(false);
			this.panel36.PerformLayout();
			this.panel37.ResumeLayout(false);
			this.panel37.PerformLayout();
			this.tpFMGenreGroups.ResumeLayout(false);
			this.panel41.ResumeLayout(false);
			this.panel41.PerformLayout();
			this.panel40.ResumeLayout(false);
			this.panel40.PerformLayout();
			this.panel39.ResumeLayout(false);
			this.panel39.PerformLayout();
			this.panel38.ResumeLayout(false);
			this.panel38.PerformLayout();
			this.panel28.ResumeLayout(false);
			this.panel28.PerformLayout();
			this.panel27.ResumeLayout(false);
			this.panel27.PerformLayout();
			this.panel26.ResumeLayout(false);
			this.panel26.PerformLayout();
			this.panel25.ResumeLayout(false);
			this.panel25.PerformLayout();
			this.panel24.ResumeLayout(false);
			this.panel24.PerformLayout();
			this.panel23.ResumeLayout(false);
			this.panel23.PerformLayout();
			this.panel22.ResumeLayout(false);
			this.panel22.PerformLayout();
			this.panel21.ResumeLayout(false);
			this.panel21.PerformLayout();
			this.panel20.ResumeLayout(false);
			this.panel20.PerformLayout();
			this.panel19.ResumeLayout(false);
			this.panel19.PerformLayout();
			this.panel18.ResumeLayout(false);
			this.panel18.PerformLayout();
			this.panel17.ResumeLayout(false);
			this.panel17.PerformLayout();
			this.panel16.ResumeLayout(false);
			this.panel16.PerformLayout();
			this.panel15.ResumeLayout(false);
			this.panel15.PerformLayout();
			this.panel14.ResumeLayout(false);
			this.panel14.PerformLayout();
			this.panel42.ResumeLayout(false);
			this.panel42.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panelProgress.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Label labelTarget;
		private System.Windows.Forms.Label labelTargetPath;
		private System.Windows.Forms.TabPage tcTemplates;
		private System.Windows.Forms.Panel panelProgress;
		private System.Windows.Forms.OpenFileDialog sfdLoadList;
		private System.Windows.Forms.Button buttonSSortRenew;
		private System.Windows.Forms.Button buttonFullSortRenew;
		private System.Windows.Forms.Button btnDefRestore;
		private System.Windows.Forms.Panel panel42;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox txtboxFMsf;
		private System.Windows.Forms.Panel panel13;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox txtboxFMdetective;
		private System.Windows.Forms.Panel panel14;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.TextBox txtboxFMprose;
		private System.Windows.Forms.Panel panel15;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.TextBox txtboxFMlove;
		private System.Windows.Forms.Panel panel16;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.TextBox txtboxFMadventure;
		private System.Windows.Forms.Panel panel17;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.TextBox txtboxFMchildren;
		private System.Windows.Forms.Panel panel18;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.TextBox txtboxFMpoetry;
		private System.Windows.Forms.Panel panel19;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.TextBox txtboxFMantique;
		private System.Windows.Forms.Panel panel20;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.TextBox txtboxFMscience;
		private System.Windows.Forms.Panel panel21;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.TextBox txtboxFMcomputers;
		private System.Windows.Forms.Panel panel22;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.TextBox txtboxFMreference;
		private System.Windows.Forms.Panel panel23;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.TextBox txtboxFMnonfiction;
		private System.Windows.Forms.Panel panel24;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.TextBox txtboxFMreligion;
		private System.Windows.Forms.Panel panel25;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.TextBox txtboxFMhumor;
		private System.Windows.Forms.Panel panel26;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.TextBox txtboxFMhome;
		private System.Windows.Forms.Panel panel27;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.TextBox txtboxFMbusiness;
		private System.Windows.Forms.Panel panel28;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox txtboxFMtech;
		private System.Windows.Forms.Panel panel38;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.TextBox txtboxFMmilitary;
		private System.Windows.Forms.Panel panel39;
		private System.Windows.Forms.Label label39;
		private System.Windows.Forms.TextBox txtboxFMfolklore;
		private System.Windows.Forms.Panel panel40;
		private System.Windows.Forms.Label label40;
		private System.Windows.Forms.TextBox txtboxFMother;
		private System.Windows.Forms.Panel panel41;
		private System.Windows.Forms.TabPage tpFMGenreGroups;
		private System.Windows.Forms.Label label38;
		private System.Windows.Forms.TextBox txtBoxFMNoFB2FirstName;
		private System.Windows.Forms.Panel panel37;
		private System.Windows.Forms.Label label37;
		private System.Windows.Forms.TextBox txtBoxFMNoFB2MiddleName;
		private System.Windows.Forms.Panel panel36;
		private System.Windows.Forms.Label label36;
		private System.Windows.Forms.TextBox txtBoxFMNoFB2LastName;
		private System.Windows.Forms.Panel panel35;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.TextBox txtBoxFMNoFB2NickName;
		private System.Windows.Forms.Panel panel34;
		private System.Windows.Forms.GroupBox gBoxFMFB2INoTags;
		private System.Windows.Forms.TabPage tpFB2Info;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.TextBox txtBoxFMNoYear;
		private System.Windows.Forms.Panel panel31;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.TextBox txtBoxFMNoPublisher;
		private System.Windows.Forms.Panel panel32;
		private System.Windows.Forms.Label label34;
		private System.Windows.Forms.TextBox txtBoxFMNoCity;
		private System.Windows.Forms.Panel panel33;
		private System.Windows.Forms.GroupBox gBoxFMPINoTags;
		private System.Windows.Forms.TabPage tpPublishInfo;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtBoxFMNoGenreGroup;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtBoxFMNoGenre;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtBoxFMNoLang;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtBoxFMNoFirstName;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtBoxFMNoMiddleName;
		private System.Windows.Forms.Panel panel7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtBoxFMNoLastName;
		private System.Windows.Forms.Panel panel8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtBoxFMNoNickName;
		private System.Windows.Forms.Panel panel9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox txtBoxFMNoBookTitle;
		private System.Windows.Forms.Panel panel10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox txtBoxFMNoSequence;
		private System.Windows.Forms.Panel panel11;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox txtBoxFMNoNSequence;
		private System.Windows.Forms.Panel panel12;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.TextBox txtBoxFMNoDateText;
		private System.Windows.Forms.Panel panel29;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.TextBox txtBoxFMNoDateValue;
		private System.Windows.Forms.Panel panel30;
		private System.Windows.Forms.GroupBox gBoxFMBINoTags;
		private System.Windows.Forms.TabPage tpBookInfo;
		private System.Windows.Forms.TabControl tcDesc;
		private System.Windows.Forms.TabPage tpFMNoTagsText;
		private System.Windows.Forms.RadioButton rbtnAsIs;
		private System.Windows.Forms.RadioButton rbtnLower;
		private System.Windows.Forms.RadioButton rbtnUpper;
		private System.Windows.Forms.RadioButton rbtnAsSentence;
		private System.Windows.Forms.GroupBox gboxRegister;
		private System.Windows.Forms.CheckBox chBoxTranslit;
		private System.Windows.Forms.CheckBox chBoxStrict;
		private System.Windows.Forms.Label lblSpace;
		private System.Windows.Forms.ComboBox cboxSpace;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lbFilelExist;
		private System.Windows.Forms.ComboBox cboxFileExist;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.RadioButton rbtnFMAllFB2;
		private System.Windows.Forms.RadioButton rbtnFMOnlyValidFB2;
		private System.Windows.Forms.Panel pSortFB2;
		private System.Windows.Forms.GroupBox gboxFMGeneral;
		private System.Windows.Forms.RadioButton rbtnAuthorOne;
		private System.Windows.Forms.RadioButton rbtnAuthorAll;
		private System.Windows.Forms.GroupBox gBoxAuthors;
		private System.Windows.Forms.RadioButton rbtnGenreOne;
		private System.Windows.Forms.RadioButton rbtnGenreAll;
		private System.Windows.Forms.GroupBox gBoxGenresCount;
		private System.Windows.Forms.RadioButton rbtnGenreSchema;
		private System.Windows.Forms.RadioButton rbtnGenreText;
		private System.Windows.Forms.GroupBox gBoxGenresType;
		private System.Windows.Forms.GroupBox gBoxGenres;
		private System.Windows.Forms.GroupBox gboxApportionment;
		private System.Windows.Forms.TabPage tpFMGeneral;
		private System.Windows.Forms.TabControl tcFM;
		private System.Windows.Forms.TabPage tpSettings;
		private System.Windows.Forms.Button btnSSGroup;
		private System.Windows.Forms.Button btnGroup;
		private System.Windows.Forms.ColumnHeader cHeaderGenreScheme;
		private System.Windows.Forms.Label lblFMFSGenres;
		private System.Windows.Forms.RadioButton rbtnFMFSFB2Librusec;
		private System.Windows.Forms.RadioButton rbtnFMFSFB22;
		private System.Windows.Forms.CheckBox chBoxStartExplorerColumnsAutoReize;
		private System.Windows.Forms.Panel panelLV;
		private System.Windows.Forms.GroupBox gBoxSelectedlSortOptions;
		private System.Windows.Forms.Panel pSSTemplate;
		private System.Windows.Forms.Button btnSSLetterFamily;
		private System.Windows.Forms.Button btnSSDir;
		private System.Windows.Forms.Button btnSSName;
		private System.Windows.Forms.Button btnSSPatronimic;
		private System.Windows.Forms.Button btnSSSequence;
		private System.Windows.Forms.Button btnSSSequenceNumber;
		private System.Windows.Forms.Button btnSSGenre;
		private System.Windows.Forms.Button btnSSLeftBracket;
		private System.Windows.Forms.Button btnSSFamily;
		private System.Windows.Forms.Button btnSSBook;
		private System.Windows.Forms.Button btnSSRightBracket;
		private System.Windows.Forms.Button btnSSLang;
		private System.Windows.Forms.Button btnSSGroupGenre;
		private System.Windows.Forms.Button btnSSOpenDir;
		private System.Windows.Forms.Button btnSSTargetDir;
		private System.Windows.Forms.Button buttonFullSortFilesTo;
		private System.Windows.Forms.Button buttonSSortFilesTo;
		private System.Windows.Forms.RichTextBox rtboxTemplatesList;
		private System.Windows.Forms.Button btnLeftBracket;
		private System.Windows.Forms.Button btnRightBracket;
		private System.Windows.Forms.Button btnGroupGenre;
		private System.Windows.Forms.CheckBox chBoxSSNotDelFB2Files;
		private System.Windows.Forms.CheckBox chBoxFSNotDelFB2Files;
		private System.Windows.Forms.Button btnLang;
		private System.Windows.Forms.Button btnBook;
		private System.Windows.Forms.Button btnSequence;
		private System.Windows.Forms.Button btnSequenceNumber;
		private System.Windows.Forms.Button btnGenre;
		private System.Windows.Forms.Button btnFamily;
		private System.Windows.Forms.Button btnName;
		private System.Windows.Forms.Button btnPatronimic;
		private System.Windows.Forms.Button btnLetterFamily;
		private System.Windows.Forms.Button btnDir;
		private System.Windows.Forms.Panel panelExplorer;
		private System.Windows.Forms.GroupBox gBoxFullSortOptions;
		private System.Windows.Forms.Panel panelTemplate;
		private System.Windows.Forms.CheckBox chBoxSSToZip;
		private System.Windows.Forms.CheckBox chBoxFSToZip;
		private System.Windows.Forms.CheckBox checkBoxTagsView;
		private System.Windows.Forms.ToolStripMenuItem tsmiColumnsExplorerAutoReize;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
		private System.Windows.Forms.ColumnHeader colHeaderEncoding;
		private System.Windows.Forms.CheckBox chBoxScanSubDir;
		private System.Windows.Forms.ColumnHeader colHeaderFileName;
		private System.Windows.Forms.ColumnHeader colHeaderBookName;
		private System.Windows.Forms.ColumnHeader colHeaderLang;
		private System.Windows.Forms.ColumnHeader colHeaderGenre;
		private System.Windows.Forms.ColumnHeader colHeaderFIOBookAuthor;
		private System.Windows.Forms.ColumnHeader colHeaderSequence;
		private System.Windows.Forms.ToolStripMenuItem tsmiUnCheckedAllSelected;
		private System.Windows.Forms.ToolStripMenuItem tsmiCheckedAllSelected;
		private System.Windows.Forms.ToolStripMenuItem tsmiZipCheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiZipUnCheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiFB2UnCheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiFB2CheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiDirCheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiDirUnCheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiFilesUnCheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiFilesCheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiUnCheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiCheckedAll;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ContextMenuStrip cmsItems;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripSeparator tsmi3;
		private System.Windows.Forms.ImageList imageListItems;
		private System.Windows.Forms.Button buttonOpenSourceDir;
		private System.Windows.Forms.Label labelAddress;
		private System.Windows.Forms.TextBox textBoxAddress;
		private System.Windows.Forms.Button buttonGo;
		private System.Windows.Forms.Panel panelAddress;
		private System.Windows.Forms.ListView listViewSource;
		private System.Windows.Forms.CheckBox chBoxViewProgress;
		private System.Windows.Forms.Panel pSSData;
		private System.Windows.Forms.OpenFileDialog sfdOpenXMLFile;
		private System.Windows.Forms.Button btnSSDataListLoad;
		private System.Windows.Forms.SaveFileDialog sfdSaveXMLFile;
		private System.Windows.Forms.Button btnSSDataListSave;
		private System.Windows.Forms.ColumnHeader cHeaderBookTitle;
		private System.Windows.Forms.ColumnHeader cHeaderExactFit;
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
		private System.Windows.Forms.Label lbSSlDir;
		private System.Windows.Forms.Label lblSSTargetDir;
		private System.Windows.Forms.Panel pSelectedSortDirs;
		private System.Windows.Forms.GroupBox gBoxFullSortRenameTemplates;
		private System.Windows.Forms.TabPage tpSelectedSort;
		private System.Windows.Forms.TabPage tpFullSort;
		private System.Windows.Forms.TabControl tcSort;
		private System.Windows.Forms.Button btnInsertTemplates;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ListView lvFilesCount;
		private System.Windows.Forms.TextBox txtBoxTemplatesFromLine;
		private System.Windows.Forms.FolderBrowserDialog fbdScanDir;
		private System.Windows.Forms.StatusStrip ssProgress;
	}
}
