/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 16.03.2009
 * Time: 9:03
 * 
 * License: GPL 2.1
 */

using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

using Core.FB2.Genres;
using Core.FileManager;
using Core.Common;
using Core.FileManager.Templates.Lexems;

using TemplatesVerify			= Core.FileManager.Templates.TemplatesVerify;
using SharpZipLibWorker 		= Core.Common.SharpZipLibWorker;
using StatusView 				= Core.FileManager.StatusView;
using filesWorker				= Core.Common.FilesWorker;
using MiscListView				= Core.Common.MiscListView;
using GenresWorker				= Core.Common.GenresWorker;
using SelectedSortQueryCriteria	= Core.FileManager.SortQueryCriteria;
using WorksWithBooks			= Core.Common.WorksWithBooks;
using Colors					= Core.Common.Colors;

// enums
using EndWorkModeEnum	= Core.Common.Enums.EndWorkModeEnum;
using ResultViewCollumn	= Core.Common.Enums.ResultViewCollumn;

namespace SharpFBTools.Tools
{
	/// <summary>
	/// Сортировщик fb2, fb2.zip и fbz файлов
	/// </summary>
	public partial class SFBTpFileManager : UserControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SFBTpFileManager));
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
			"Всего папок",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
			"Всего файлов",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
			"Исходные fb2-файлы",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
			"Исходные  Zip-архивы с fb2",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
			"Исходные fb2-файлы из архивов",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
			"Другие файлы",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
			"Создано в папке-приемнике",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem(new string[] {
			"Нечитаемые fb2-файлы (архивы)",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem(new string[] {
			"Не валидные fb2-файлы (при вкл. опции)",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem(new string[] {
			"Битые архивы (не открылись)",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem(new string[] {
			"Длинный путь к создаваемому файлу",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem(new string[] {
			"Не удовлетворяющие условиям сортировки",
			"0"}, 0);
			this.ssProgress = new System.Windows.Forms.StatusStrip();
			this.fbdScanDir = new System.Windows.Forms.FolderBrowserDialog();
			this.btnInsertTemplates = new System.Windows.Forms.Button();
			this.txtBoxTemplatesFromLine = new System.Windows.Forms.TextBox();
			this.tcSort = new System.Windows.Forms.TabControl();
			this.tpFullSort = new System.Windows.Forms.TabPage();
			this.listViewFB2Files = new System.Windows.Forms.ListView();
			this.colHeaderFileName = new System.Windows.Forms.ColumnHeader();
			this.colHeaderBookName = new System.Windows.Forms.ColumnHeader();
			this.colHeaderFIOBookAuthor = new System.Windows.Forms.ColumnHeader();
			this.colHeaderGenre = new System.Windows.Forms.ColumnHeader();
			this.colHeaderSequence = new System.Windows.Forms.ColumnHeader();
			this.colHeaderLang = new System.Windows.Forms.ColumnHeader();
			this.colHeaderID = new System.Windows.Forms.ColumnHeader();
			this.colHeaderVersion = new System.Windows.Forms.ColumnHeader();
			this.colHeaderEncoding = new System.Windows.Forms.ColumnHeader();
			this.colHeaderIValidate = new System.Windows.Forms.ColumnHeader();
			this.colHeaderIFormat = new System.Windows.Forms.ColumnHeader();
			this.colHeaderFileSize = new System.Windows.Forms.ColumnHeader();
			this.colHeaderFileCreate = new System.Windows.Forms.ColumnHeader();
			this.colHeaderFileModify = new System.Windows.Forms.ColumnHeader();
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
			this.tsmi1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiViewInReader = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiColumnsExplorerAutoReize = new System.Windows.Forms.ToolStripMenuItem();
			this.imageListItems = new System.Windows.Forms.ImageList(this.components);
			this.panelExplorer = new System.Windows.Forms.Panel();
			this.panelAddress = new System.Windows.Forms.Panel();
			this.labelTargetPath = new System.Windows.Forms.Label();
			this.labelTarget = new System.Windows.Forms.Label();
			this.buttonGo = new System.Windows.Forms.Button();
			this.textBoxAddress = new System.Windows.Forms.TextBox();
			this.labelAddress = new System.Windows.Forms.Label();
			this.buttonOpenSourceDir = new System.Windows.Forms.Button();
			this.panelTemplate = new System.Windows.Forms.Panel();
			this.buttonFullSortRenew = new System.Windows.Forms.Button();
			this.gBoxFullSortOptions = new System.Windows.Forms.GroupBox();
			this.comboBoxMaxFileForProgress = new System.Windows.Forms.ComboBox();
			this.lblMaxFileForProgress = new System.Windows.Forms.Label();
			this.checkBoxTagsView = new System.Windows.Forms.CheckBox();
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
			this.gBoxOptions = new System.Windows.Forms.GroupBox();
			this.comboBoxMaxSequenceLenght = new System.Windows.Forms.ComboBox();
			this.lblMaxSequenceLenght = new System.Windows.Forms.Label();
			this.comboBoxMaxBTLenght = new System.Windows.Forms.ComboBox();
			this.lblMaxBTLenght = new System.Windows.Forms.Label();
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
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnDefRestore = new System.Windows.Forms.Button();
			this.sfdSaveXMLFile = new System.Windows.Forms.SaveFileDialog();
			this.sfdOpenXMLFile = new System.Windows.Forms.OpenFileDialog();
			this.sfdLoadList = new System.Windows.Forms.OpenFileDialog();
			this.panelProgress = new System.Windows.Forms.Panel();
			this.lvFilesCount = new System.Windows.Forms.ListView();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
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
			this.gBoxOptions.SuspendLayout();
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
			this.panel1.SuspendLayout();
			this.panelProgress.SuspendLayout();
			this.SuspendLayout();
			// 
			// ssProgress
			// 
			this.ssProgress.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.ssProgress.Location = new System.Drawing.Point(0, 778);
			this.ssProgress.Name = "ssProgress";
			this.ssProgress.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
			this.ssProgress.Size = new System.Drawing.Size(1675, 22);
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
			this.btnInsertTemplates.Location = new System.Drawing.Point(637, 21);
			this.btnInsertTemplates.Margin = new System.Windows.Forms.Padding(4);
			this.btnInsertTemplates.Name = "btnInsertTemplates";
			this.btnInsertTemplates.Size = new System.Drawing.Size(160, 34);
			this.btnInsertTemplates.TabIndex = 9;
			this.btnInsertTemplates.Text = "Вставить готовый";
			this.btnInsertTemplates.UseVisualStyleBackColor = true;
			this.btnInsertTemplates.Click += new System.EventHandler(this.BtnInsertTemplatesClick);
			// 
			// txtBoxTemplatesFromLine
			// 
			this.txtBoxTemplatesFromLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxTemplatesFromLine.Location = new System.Drawing.Point(8, 25);
			this.txtBoxTemplatesFromLine.Margin = new System.Windows.Forms.Padding(4);
			this.txtBoxTemplatesFromLine.Name = "txtBoxTemplatesFromLine";
			this.txtBoxTemplatesFromLine.Size = new System.Drawing.Size(621, 24);
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
			this.tcSort.Margin = new System.Windows.Forms.Padding(4);
			this.tcSort.Name = "tcSort";
			this.tcSort.SelectedIndex = 0;
			this.tcSort.Size = new System.Drawing.Size(1225, 773);
			this.tcSort.TabIndex = 31;
			// 
			// tpFullSort
			// 
			this.tpFullSort.Controls.Add(this.listViewFB2Files);
			this.tpFullSort.Controls.Add(this.panelExplorer);
			this.tpFullSort.Controls.Add(this.panelTemplate);
			this.tpFullSort.Font = new System.Drawing.Font("Tahoma", 8F);
			this.tpFullSort.Location = new System.Drawing.Point(4, 25);
			this.tpFullSort.Margin = new System.Windows.Forms.Padding(4);
			this.tpFullSort.Name = "tpFullSort";
			this.tpFullSort.Padding = new System.Windows.Forms.Padding(4);
			this.tpFullSort.Size = new System.Drawing.Size(1217, 744);
			this.tpFullSort.TabIndex = 0;
			this.tpFullSort.Text = " Полная Сортировка ";
			this.tpFullSort.UseVisualStyleBackColor = true;
			// 
			// listViewFB2Files
			// 
			this.listViewFB2Files.AllowColumnReorder = true;
			this.listViewFB2Files.CheckBoxes = true;
			this.listViewFB2Files.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.colHeaderFileName,
			this.colHeaderBookName,
			this.colHeaderFIOBookAuthor,
			this.colHeaderGenre,
			this.colHeaderSequence,
			this.colHeaderLang,
			this.colHeaderID,
			this.colHeaderVersion,
			this.colHeaderEncoding,
			this.colHeaderIValidate,
			this.colHeaderIFormat,
			this.colHeaderFileSize,
			this.colHeaderFileCreate,
			this.colHeaderFileModify});
			this.listViewFB2Files.ContextMenuStrip = this.cmsItems;
			this.listViewFB2Files.Dock = System.Windows.Forms.DockStyle.Fill;
			this.listViewFB2Files.FullRowSelect = true;
			this.listViewFB2Files.GridLines = true;
			this.listViewFB2Files.Location = new System.Drawing.Point(4, 292);
			this.listViewFB2Files.Margin = new System.Windows.Forms.Padding(4);
			this.listViewFB2Files.Name = "listViewFB2Files";
			this.listViewFB2Files.ShowItemToolTips = true;
			this.listViewFB2Files.Size = new System.Drawing.Size(1209, 448);
			this.listViewFB2Files.SmallImageList = this.imageListItems;
			this.listViewFB2Files.TabIndex = 35;
			this.listViewFB2Files.UseCompatibleStateImageBehavior = false;
			this.listViewFB2Files.View = System.Windows.Forms.View.Details;
			this.listViewFB2Files.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListViewSourceColumnClick);
			this.listViewFB2Files.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ListViewSourceItemCheck);
			this.listViewFB2Files.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.ListViewSourceItemChecked);
			this.listViewFB2Files.DoubleClick += new System.EventHandler(this.ListViewSourceDoubleClick);
			this.listViewFB2Files.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ListViewSourceKeyPress);
			// 
			// colHeaderFileName
			// 
			this.colHeaderFileName.Text = "Имя файла";
			// 
			// colHeaderBookName
			// 
			this.colHeaderBookName.Text = "Книга";
			// 
			// colHeaderFIOBookAuthor
			// 
			this.colHeaderFIOBookAuthor.Text = "Автор(ы)";
			// 
			// colHeaderGenre
			// 
			this.colHeaderGenre.Text = "Жанр(ы)";
			// 
			// colHeaderSequence
			// 
			this.colHeaderSequence.Text = "Серия (№)";
			// 
			// colHeaderLang
			// 
			this.colHeaderLang.Text = "Язык";
			this.colHeaderLang.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// colHeaderID
			// 
			this.colHeaderID.Text = "ID";
			// 
			// colHeaderVersion
			// 
			this.colHeaderVersion.Text = "Версия";
			this.colHeaderVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// colHeaderEncoding
			// 
			this.colHeaderEncoding.Text = "Кодировка";
			this.colHeaderEncoding.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// colHeaderIValidate
			// 
			this.colHeaderIValidate.Text = "Валидность";
			this.colHeaderIValidate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.colHeaderIValidate.Width = 50;
			// 
			// colHeaderIFormat
			// 
			this.colHeaderIFormat.Text = "Формат";
			// 
			// colHeaderFileSize
			// 
			this.colHeaderFileSize.Text = "Размер";
			this.colHeaderFileSize.Width = 90;
			// 
			// colHeaderFileCreate
			// 
			this.colHeaderFileCreate.Text = "Дата создания";
			this.colHeaderFileCreate.Width = 120;
			// 
			// colHeaderFileModify
			// 
			this.colHeaderFileModify.Text = "Последнее изменение";
			this.colHeaderFileModify.Width = 120;
			// 
			// cmsItems
			// 
			this.cmsItems.ImageScalingSize = new System.Drawing.Size(20, 20);
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
			this.tsmi1,
			this.tsmiViewInReader,
			this.toolStripMenuItem7,
			this.tsmiColumnsExplorerAutoReize});
			this.cmsItems.Name = "cmsValidator";
			this.cmsItems.Size = new System.Drawing.Size(449, 416);
			// 
			// tsmi3
			// 
			this.tsmi3.Name = "tsmi3";
			this.tsmi3.Size = new System.Drawing.Size(445, 6);
			// 
			// tsmiCheckedAll
			// 
			this.tsmiCheckedAll.Image = ((System.Drawing.Image)(resources.GetObject("tsmiCheckedAll.Image")));
			this.tsmiCheckedAll.Name = "tsmiCheckedAll";
			this.tsmiCheckedAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
			| System.Windows.Forms.Keys.C)));
			this.tsmiCheckedAll.Size = new System.Drawing.Size(448, 26);
			this.tsmiCheckedAll.Text = "Пометить все файлы и папки";
			this.tsmiCheckedAll.Click += new System.EventHandler(this.TsmiCheckedAllClick);
			// 
			// tsmiUnCheckedAll
			// 
			this.tsmiUnCheckedAll.Image = ((System.Drawing.Image)(resources.GetObject("tsmiUnCheckedAll.Image")));
			this.tsmiUnCheckedAll.Name = "tsmiUnCheckedAll";
			this.tsmiUnCheckedAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
			| System.Windows.Forms.Keys.U)));
			this.tsmiUnCheckedAll.Size = new System.Drawing.Size(448, 26);
			this.tsmiUnCheckedAll.Text = "Снять пометки со всех файлов и папок";
			this.tsmiUnCheckedAll.Click += new System.EventHandler(this.TsmiUnCheckedAllClick);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(445, 6);
			// 
			// tsmiFilesCheckedAll
			// 
			this.tsmiFilesCheckedAll.Name = "tsmiFilesCheckedAll";
			this.tsmiFilesCheckedAll.Size = new System.Drawing.Size(448, 26);
			this.tsmiFilesCheckedAll.Text = "Пометить все файлы";
			this.tsmiFilesCheckedAll.Click += new System.EventHandler(this.TsmiFilesCheckedAllClick);
			// 
			// tsmiFilesUnCheckedAll
			// 
			this.tsmiFilesUnCheckedAll.Name = "tsmiFilesUnCheckedAll";
			this.tsmiFilesUnCheckedAll.Size = new System.Drawing.Size(448, 26);
			this.tsmiFilesUnCheckedAll.Text = "Снять пометки со всех файлов";
			this.tsmiFilesUnCheckedAll.Click += new System.EventHandler(this.TsmiFilesUnCheckedAllClick);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(445, 6);
			// 
			// tsmiDirCheckedAll
			// 
			this.tsmiDirCheckedAll.Name = "tsmiDirCheckedAll";
			this.tsmiDirCheckedAll.Size = new System.Drawing.Size(448, 26);
			this.tsmiDirCheckedAll.Text = "Пометить все папки";
			this.tsmiDirCheckedAll.Click += new System.EventHandler(this.TsmiDirCheckedAllClick);
			// 
			// tsmiDirUnCheckedAll
			// 
			this.tsmiDirUnCheckedAll.Name = "tsmiDirUnCheckedAll";
			this.tsmiDirUnCheckedAll.Size = new System.Drawing.Size(448, 26);
			this.tsmiDirUnCheckedAll.Text = "Снять пометки со всех папок";
			this.tsmiDirUnCheckedAll.Click += new System.EventHandler(this.TsmiDirUnCheckedAllClick);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(445, 6);
			// 
			// tsmiFB2CheckedAll
			// 
			this.tsmiFB2CheckedAll.Name = "tsmiFB2CheckedAll";
			this.tsmiFB2CheckedAll.Size = new System.Drawing.Size(448, 26);
			this.tsmiFB2CheckedAll.Text = "Пометить все fb2 файлы";
			this.tsmiFB2CheckedAll.Click += new System.EventHandler(this.TsmiFB2CheckedAllClick);
			// 
			// tsmiFB2UnCheckedAll
			// 
			this.tsmiFB2UnCheckedAll.Name = "tsmiFB2UnCheckedAll";
			this.tsmiFB2UnCheckedAll.Size = new System.Drawing.Size(448, 26);
			this.tsmiFB2UnCheckedAll.Text = "Снять пометки со всех fb2 файлов";
			this.tsmiFB2UnCheckedAll.Click += new System.EventHandler(this.TsmiFB2UnCheckedAllClick);
			// 
			// toolStripMenuItem4
			// 
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new System.Drawing.Size(445, 6);
			// 
			// tsmiZipCheckedAll
			// 
			this.tsmiZipCheckedAll.Name = "tsmiZipCheckedAll";
			this.tsmiZipCheckedAll.Size = new System.Drawing.Size(448, 26);
			this.tsmiZipCheckedAll.Text = "Пометить все zip файлы";
			this.tsmiZipCheckedAll.Click += new System.EventHandler(this.TsmiZipCheckedAllClick);
			// 
			// tsmiZipUnCheckedAll
			// 
			this.tsmiZipUnCheckedAll.Name = "tsmiZipUnCheckedAll";
			this.tsmiZipUnCheckedAll.Size = new System.Drawing.Size(448, 26);
			this.tsmiZipUnCheckedAll.Text = "Снять пометки со всех zip файлов";
			this.tsmiZipUnCheckedAll.Click += new System.EventHandler(this.TsmiZipUnCheckedAllClick);
			// 
			// toolStripMenuItem5
			// 
			this.toolStripMenuItem5.Name = "toolStripMenuItem5";
			this.toolStripMenuItem5.Size = new System.Drawing.Size(445, 6);
			// 
			// tsmiCheckedAllSelected
			// 
			this.tsmiCheckedAllSelected.Name = "tsmiCheckedAllSelected";
			this.tsmiCheckedAllSelected.Size = new System.Drawing.Size(448, 26);
			this.tsmiCheckedAllSelected.Text = "Пометить всё выделенное";
			this.tsmiCheckedAllSelected.Click += new System.EventHandler(this.TsmiCheckedAllSelectedClick);
			// 
			// tsmiUnCheckedAllSelected
			// 
			this.tsmiUnCheckedAllSelected.Name = "tsmiUnCheckedAllSelected";
			this.tsmiUnCheckedAllSelected.Size = new System.Drawing.Size(448, 26);
			this.tsmiUnCheckedAllSelected.Text = "Снять пометки со всего выделенного";
			this.tsmiUnCheckedAllSelected.Click += new System.EventHandler(this.TsmiUnCheckedAllSelectedClick);
			// 
			// tsmi1
			// 
			this.tsmi1.Name = "tsmi1";
			this.tsmi1.Size = new System.Drawing.Size(445, 6);
			// 
			// tsmiViewInReader
			// 
			this.tsmiViewInReader.Image = ((System.Drawing.Image)(resources.GetObject("tsmiViewInReader.Image")));
			this.tsmiViewInReader.Name = "tsmiViewInReader";
			this.tsmiViewInReader.ShortcutKeys = System.Windows.Forms.Keys.F3;
			this.tsmiViewInReader.Size = new System.Drawing.Size(448, 26);
			this.tsmiViewInReader.Text = "Запустить в fb2-читалке (Просмотр)";
			this.tsmiViewInReader.Click += new System.EventHandler(this.TsmiViewInReaderClick);
			// 
			// toolStripMenuItem7
			// 
			this.toolStripMenuItem7.Name = "toolStripMenuItem7";
			this.toolStripMenuItem7.Size = new System.Drawing.Size(445, 6);
			// 
			// tsmiColumnsExplorerAutoReize
			// 
			this.tsmiColumnsExplorerAutoReize.Name = "tsmiColumnsExplorerAutoReize";
			this.tsmiColumnsExplorerAutoReize.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
			this.tsmiColumnsExplorerAutoReize.Size = new System.Drawing.Size(448, 26);
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
			this.panelExplorer.Location = new System.Drawing.Point(4, 226);
			this.panelExplorer.Margin = new System.Windows.Forms.Padding(4);
			this.panelExplorer.Name = "panelExplorer";
			this.panelExplorer.Size = new System.Drawing.Size(1209, 66);
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
			this.panelAddress.Margin = new System.Windows.Forms.Padding(4);
			this.panelAddress.Name = "panelAddress";
			this.panelAddress.Size = new System.Drawing.Size(1209, 64);
			this.panelAddress.TabIndex = 38;
			// 
			// labelTargetPath
			// 
			this.labelTargetPath.ForeColor = System.Drawing.SystemColors.ControlText;
			this.labelTargetPath.Location = new System.Drawing.Point(161, 41);
			this.labelTargetPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelTargetPath.Name = "labelTargetPath";
			this.labelTargetPath.Size = new System.Drawing.Size(839, 21);
			this.labelTargetPath.TabIndex = 9;
			this.labelTargetPath.Text = "labelTargetPath";
			// 
			// labelTarget
			// 
			this.labelTarget.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelTarget.Location = new System.Drawing.Point(4, 38);
			this.labelTarget.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelTarget.Name = "labelTarget";
			this.labelTarget.Size = new System.Drawing.Size(153, 23);
			this.labelTarget.TabIndex = 8;
			this.labelTarget.Text = "Папка-приемник:";
			this.labelTarget.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// buttonGo
			// 
			this.buttonGo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.buttonGo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.buttonGo.Location = new System.Drawing.Point(1010, 5);
			this.buttonGo.Margin = new System.Windows.Forms.Padding(4);
			this.buttonGo.Name = "buttonGo";
			this.buttonGo.Size = new System.Drawing.Size(189, 53);
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
			this.textBoxAddress.Location = new System.Drawing.Point(161, 6);
			this.textBoxAddress.Margin = new System.Windows.Forms.Padding(4);
			this.textBoxAddress.Name = "textBoxAddress";
			this.textBoxAddress.Size = new System.Drawing.Size(838, 24);
			this.textBoxAddress.TabIndex = 5;
			this.textBoxAddress.TextChanged += new System.EventHandler(this.TextBoxAddressTextChanged);
			this.textBoxAddress.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TextBoxAddressKeyPress);
			// 
			// labelAddress
			// 
			this.labelAddress.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.labelAddress.Location = new System.Drawing.Point(4, 9);
			this.labelAddress.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.labelAddress.Name = "labelAddress";
			this.labelAddress.Size = new System.Drawing.Size(63, 23);
			this.labelAddress.TabIndex = 4;
			this.labelAddress.Text = "Адрес:";
			this.labelAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// buttonOpenSourceDir
			// 
			this.buttonOpenSourceDir.Image = ((System.Drawing.Image)(resources.GetObject("buttonOpenSourceDir.Image")));
			this.buttonOpenSourceDir.Location = new System.Drawing.Point(107, 4);
			this.buttonOpenSourceDir.Margin = new System.Windows.Forms.Padding(4);
			this.buttonOpenSourceDir.Name = "buttonOpenSourceDir";
			this.buttonOpenSourceDir.Size = new System.Drawing.Size(41, 33);
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
			this.panelTemplate.Location = new System.Drawing.Point(4, 4);
			this.panelTemplate.Margin = new System.Windows.Forms.Padding(4);
			this.panelTemplate.Name = "panelTemplate";
			this.panelTemplate.Size = new System.Drawing.Size(1209, 222);
			this.panelTemplate.TabIndex = 34;
			// 
			// buttonFullSortRenew
			// 
			this.buttonFullSortRenew.Font = new System.Drawing.Font("Tahoma", 11F);
			this.buttonFullSortRenew.Image = ((System.Drawing.Image)(resources.GetObject("buttonFullSortRenew.Image")));
			this.buttonFullSortRenew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonFullSortRenew.Location = new System.Drawing.Point(227, 6);
			this.buttonFullSortRenew.Margin = new System.Windows.Forms.Padding(4);
			this.buttonFullSortRenew.Name = "buttonFullSortRenew";
			this.buttonFullSortRenew.Size = new System.Drawing.Size(305, 54);
			this.buttonFullSortRenew.TabIndex = 34;
			this.buttonFullSortRenew.Text = "Возобновить из файла...";
			this.buttonFullSortRenew.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.buttonFullSortRenew.UseVisualStyleBackColor = true;
			this.buttonFullSortRenew.Click += new System.EventHandler(this.ButtonFullSortRenewClick);
			// 
			// gBoxFullSortOptions
			// 
			this.gBoxFullSortOptions.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.gBoxFullSortOptions.Controls.Add(this.comboBoxMaxFileForProgress);
			this.gBoxFullSortOptions.Controls.Add(this.lblMaxFileForProgress);
			this.gBoxFullSortOptions.Controls.Add(this.checkBoxTagsView);
			this.gBoxFullSortOptions.Controls.Add(this.chBoxScanSubDir);
			this.gBoxFullSortOptions.Controls.Add(this.chBoxFSNotDelFB2Files);
			this.gBoxFullSortOptions.Controls.Add(this.chBoxFSToZip);
			this.gBoxFullSortOptions.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gBoxFullSortOptions.Location = new System.Drawing.Point(826, 6);
			this.gBoxFullSortOptions.Margin = new System.Windows.Forms.Padding(4);
			this.gBoxFullSortOptions.Name = "gBoxFullSortOptions";
			this.gBoxFullSortOptions.Padding = new System.Windows.Forms.Padding(4);
			this.gBoxFullSortOptions.Size = new System.Drawing.Size(373, 206);
			this.gBoxFullSortOptions.TabIndex = 33;
			this.gBoxFullSortOptions.TabStop = false;
			this.gBoxFullSortOptions.Text = " Настройки ";
			// 
			// comboBoxMaxFileForProgress
			// 
			this.comboBoxMaxFileForProgress.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxMaxFileForProgress.FormattingEnabled = true;
			this.comboBoxMaxFileForProgress.Items.AddRange(new object[] {
			"100",
			"150",
			"200",
			"250",
			"300",
			"350",
			"400",
			"450",
			"500",
			"550",
			"600",
			"650",
			"700",
			"750",
			"800",
			"850",
			"900",
			"950",
			"1000",
			"1500",
			"2000",
			"2500",
			"3000",
			"3500",
			"4000",
			"4500",
			"5000"});
			this.comboBoxMaxFileForProgress.Location = new System.Drawing.Point(260, 141);
			this.comboBoxMaxFileForProgress.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.comboBoxMaxFileForProgress.Name = "comboBoxMaxFileForProgress";
			this.comboBoxMaxFileForProgress.Size = new System.Drawing.Size(105, 24);
			this.comboBoxMaxFileForProgress.TabIndex = 11;
			this.comboBoxMaxFileForProgress.SelectedIndexChanged += new System.EventHandler(this.ComboBoxMaxFileForProgressSelectedIndexChanged);
			// 
			// lblMaxFileForProgress
			// 
			this.lblMaxFileForProgress.Font = new System.Drawing.Font("Tahoma", 8F);
			this.lblMaxFileForProgress.Location = new System.Drawing.Point(4, 143);
			this.lblMaxFileForProgress.Name = "lblMaxFileForProgress";
			this.lblMaxFileForProgress.Size = new System.Drawing.Size(251, 23);
			this.lblMaxFileForProgress.TabIndex = 10;
			this.lblMaxFileForProgress.Text = "Прогресс, начиная с числа файлов:";
			// 
			// checkBoxTagsView
			// 
			this.checkBoxTagsView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.checkBoxTagsView.Font = new System.Drawing.Font("Tahoma", 8F);
			this.checkBoxTagsView.ForeColor = System.Drawing.Color.Navy;
			this.checkBoxTagsView.Location = new System.Drawing.Point(8, 89);
			this.checkBoxTagsView.Margin = new System.Windows.Forms.Padding(4);
			this.checkBoxTagsView.Name = "checkBoxTagsView";
			this.checkBoxTagsView.Size = new System.Drawing.Size(357, 30);
			this.checkBoxTagsView.TabIndex = 8;
			this.checkBoxTagsView.Text = "Показывать описание книг в Проводнике";
			this.checkBoxTagsView.UseVisualStyleBackColor = true;
			this.checkBoxTagsView.Click += new System.EventHandler(this.CheckBoxTagsViewClick);
			// 
			// chBoxScanSubDir
			// 
			this.chBoxScanSubDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chBoxScanSubDir.Checked = true;
			this.chBoxScanSubDir.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chBoxScanSubDir.Font = new System.Drawing.Font("Tahoma", 8F);
			this.chBoxScanSubDir.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chBoxScanSubDir.Location = new System.Drawing.Point(8, 20);
			this.chBoxScanSubDir.Margin = new System.Windows.Forms.Padding(4);
			this.chBoxScanSubDir.Name = "chBoxScanSubDir";
			this.chBoxScanSubDir.Size = new System.Drawing.Size(357, 30);
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
			this.chBoxFSNotDelFB2Files.Location = new System.Drawing.Point(8, 66);
			this.chBoxFSNotDelFB2Files.Margin = new System.Windows.Forms.Padding(4);
			this.chBoxFSNotDelFB2Files.Name = "chBoxFSNotDelFB2Files";
			this.chBoxFSNotDelFB2Files.Size = new System.Drawing.Size(359, 30);
			this.chBoxFSNotDelFB2Files.TabIndex = 6;
			this.chBoxFSNotDelFB2Files.Text = "Сохранять оригиналы";
			this.chBoxFSNotDelFB2Files.UseVisualStyleBackColor = true;
			this.chBoxFSNotDelFB2Files.Click += new System.EventHandler(this.ChBoxFSNotDelFB2FilesClick);
			// 
			// chBoxFSToZip
			// 
			this.chBoxFSToZip.Font = new System.Drawing.Font("Tahoma", 8F);
			this.chBoxFSToZip.Location = new System.Drawing.Point(8, 43);
			this.chBoxFSToZip.Margin = new System.Windows.Forms.Padding(4);
			this.chBoxFSToZip.Name = "chBoxFSToZip";
			this.chBoxFSToZip.Size = new System.Drawing.Size(357, 30);
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
			this.gBoxFullSortRenameTemplates.Location = new System.Drawing.Point(4, 68);
			this.gBoxFullSortRenameTemplates.Margin = new System.Windows.Forms.Padding(4);
			this.gBoxFullSortRenameTemplates.Name = "gBoxFullSortRenameTemplates";
			this.gBoxFullSortRenameTemplates.Padding = new System.Windows.Forms.Padding(4);
			this.gBoxFullSortRenameTemplates.Size = new System.Drawing.Size(810, 144);
			this.gBoxFullSortRenameTemplates.TabIndex = 32;
			this.gBoxFullSortRenameTemplates.TabStop = false;
			this.gBoxFullSortRenameTemplates.Text = " Шаблоны подстановки ";
			// 
			// btnGroup
			// 
			this.btnGroup.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnGroup.Location = new System.Drawing.Point(9, 101);
			this.btnGroup.Margin = new System.Windows.Forms.Padding(4);
			this.btnGroup.Name = "btnGroup";
			this.btnGroup.Size = new System.Drawing.Size(107, 28);
			this.btnGroup.TabIndex = 23;
			this.btnGroup.Text = "Группа";
			this.btnGroup.UseVisualStyleBackColor = true;
			this.btnGroup.Click += new System.EventHandler(this.BtnGroupClick);
			// 
			// btnGroupGenre
			// 
			this.btnGroupGenre.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnGroupGenre.Location = new System.Drawing.Point(124, 101);
			this.btnGroupGenre.Margin = new System.Windows.Forms.Padding(4);
			this.btnGroupGenre.Name = "btnGroupGenre";
			this.btnGroupGenre.Size = new System.Drawing.Size(128, 28);
			this.btnGroupGenre.TabIndex = 22;
			this.btnGroupGenre.Text = "Группа\\Жанр";
			this.btnGroupGenre.UseVisualStyleBackColor = true;
			this.btnGroupGenre.Click += new System.EventHandler(this.BtnGroupGenreClick);
			// 
			// btnLang
			// 
			this.btnLang.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnLang.Location = new System.Drawing.Point(9, 66);
			this.btnLang.Margin = new System.Windows.Forms.Padding(4);
			this.btnLang.Name = "btnLang";
			this.btnLang.Size = new System.Drawing.Size(107, 28);
			this.btnLang.TabIndex = 21;
			this.btnLang.Text = "Язык";
			this.btnLang.UseVisualStyleBackColor = true;
			this.btnLang.Click += new System.EventHandler(this.BtnLangClick);
			// 
			// btnRightBracket
			// 
			this.btnRightBracket.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnRightBracket.Location = new System.Drawing.Point(677, 66);
			this.btnRightBracket.Margin = new System.Windows.Forms.Padding(4);
			this.btnRightBracket.Name = "btnRightBracket";
			this.btnRightBracket.Size = new System.Drawing.Size(31, 28);
			this.btnRightBracket.TabIndex = 20;
			this.btnRightBracket.Text = "]";
			this.btnRightBracket.UseVisualStyleBackColor = true;
			this.btnRightBracket.Click += new System.EventHandler(this.BtnRightBracketClick);
			// 
			// btnBook
			// 
			this.btnBook.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnBook.Location = new System.Drawing.Point(372, 101);
			this.btnBook.Margin = new System.Windows.Forms.Padding(4);
			this.btnBook.Name = "btnBook";
			this.btnBook.Size = new System.Drawing.Size(107, 28);
			this.btnBook.TabIndex = 15;
			this.btnBook.Text = "Книга";
			this.btnBook.UseVisualStyleBackColor = true;
			this.btnBook.Click += new System.EventHandler(this.BtnBookClick);
			// 
			// btnFamily
			// 
			this.btnFamily.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnFamily.Location = new System.Drawing.Point(260, 66);
			this.btnFamily.Margin = new System.Windows.Forms.Padding(4);
			this.btnFamily.Name = "btnFamily";
			this.btnFamily.Size = new System.Drawing.Size(107, 28);
			this.btnFamily.TabIndex = 12;
			this.btnFamily.Text = "Фамилия";
			this.btnFamily.UseVisualStyleBackColor = true;
			this.btnFamily.Click += new System.EventHandler(this.BtnFamilyClick);
			// 
			// btnLeftBracket
			// 
			this.btnLeftBracket.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnLeftBracket.Location = new System.Drawing.Point(643, 66);
			this.btnLeftBracket.Margin = new System.Windows.Forms.Padding(4);
			this.btnLeftBracket.Name = "btnLeftBracket";
			this.btnLeftBracket.Size = new System.Drawing.Size(31, 28);
			this.btnLeftBracket.TabIndex = 19;
			this.btnLeftBracket.Text = "[";
			this.btnLeftBracket.UseVisualStyleBackColor = true;
			this.btnLeftBracket.Click += new System.EventHandler(this.BtnLeftBracketClick);
			// 
			// btnGenre
			// 
			this.btnGenre.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnGenre.Location = new System.Drawing.Point(257, 101);
			this.btnGenre.Margin = new System.Windows.Forms.Padding(4);
			this.btnGenre.Name = "btnGenre";
			this.btnGenre.Size = new System.Drawing.Size(107, 28);
			this.btnGenre.TabIndex = 18;
			this.btnGenre.Text = "Жанр";
			this.btnGenre.UseVisualStyleBackColor = true;
			this.btnGenre.Click += new System.EventHandler(this.BtnGenreClick);
			// 
			// btnSequenceNumber
			// 
			this.btnSequenceNumber.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSequenceNumber.Location = new System.Drawing.Point(600, 101);
			this.btnSequenceNumber.Margin = new System.Windows.Forms.Padding(4);
			this.btnSequenceNumber.Name = "btnSequenceNumber";
			this.btnSequenceNumber.Size = new System.Drawing.Size(107, 28);
			this.btnSequenceNumber.TabIndex = 17;
			this.btnSequenceNumber.Text = "№ Серии";
			this.btnSequenceNumber.UseVisualStyleBackColor = true;
			this.btnSequenceNumber.Click += new System.EventHandler(this.BtnSequenceNumberClick);
			// 
			// btnSequence
			// 
			this.btnSequence.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSequence.Location = new System.Drawing.Point(487, 101);
			this.btnSequence.Margin = new System.Windows.Forms.Padding(4);
			this.btnSequence.Name = "btnSequence";
			this.btnSequence.Size = new System.Drawing.Size(107, 28);
			this.btnSequence.TabIndex = 16;
			this.btnSequence.Text = "Серия";
			this.btnSequence.UseVisualStyleBackColor = true;
			this.btnSequence.Click += new System.EventHandler(this.BtnSequenceClick);
			// 
			// btnPatronimic
			// 
			this.btnPatronimic.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnPatronimic.Location = new System.Drawing.Point(488, 66);
			this.btnPatronimic.Margin = new System.Windows.Forms.Padding(4);
			this.btnPatronimic.Name = "btnPatronimic";
			this.btnPatronimic.Size = new System.Drawing.Size(107, 28);
			this.btnPatronimic.TabIndex = 14;
			this.btnPatronimic.Text = "Отчество";
			this.btnPatronimic.UseVisualStyleBackColor = true;
			this.btnPatronimic.Click += new System.EventHandler(this.BtnPatronimicClick);
			// 
			// btnName
			// 
			this.btnName.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnName.Location = new System.Drawing.Point(375, 66);
			this.btnName.Margin = new System.Windows.Forms.Padding(4);
			this.btnName.Name = "btnName";
			this.btnName.Size = new System.Drawing.Size(107, 28);
			this.btnName.TabIndex = 13;
			this.btnName.Text = "Имя";
			this.btnName.UseVisualStyleBackColor = true;
			this.btnName.Click += new System.EventHandler(this.BtnNameClick);
			// 
			// btnDir
			// 
			this.btnDir.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnDir.Location = new System.Drawing.Point(604, 66);
			this.btnDir.Margin = new System.Windows.Forms.Padding(4);
			this.btnDir.Name = "btnDir";
			this.btnDir.Size = new System.Drawing.Size(31, 28);
			this.btnDir.TabIndex = 11;
			this.btnDir.Text = "\\";
			this.btnDir.UseVisualStyleBackColor = true;
			this.btnDir.Click += new System.EventHandler(this.BtnDirClick);
			// 
			// btnLetterFamily
			// 
			this.btnLetterFamily.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnLetterFamily.Location = new System.Drawing.Point(124, 66);
			this.btnLetterFamily.Margin = new System.Windows.Forms.Padding(4);
			this.btnLetterFamily.Name = "btnLetterFamily";
			this.btnLetterFamily.Size = new System.Drawing.Size(128, 28);
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
			this.buttonFullSortFilesTo.Location = new System.Drawing.Point(4, 6);
			this.buttonFullSortFilesTo.Margin = new System.Windows.Forms.Padding(4);
			this.buttonFullSortFilesTo.Name = "buttonFullSortFilesTo";
			this.buttonFullSortFilesTo.Size = new System.Drawing.Size(211, 54);
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
			this.tpSelectedSort.Location = new System.Drawing.Point(4, 25);
			this.tpSelectedSort.Margin = new System.Windows.Forms.Padding(4);
			this.tpSelectedSort.Name = "tpSelectedSort";
			this.tpSelectedSort.Padding = new System.Windows.Forms.Padding(4);
			this.tpSelectedSort.Size = new System.Drawing.Size(1217, 744);
			this.tpSelectedSort.TabIndex = 1;
			this.tpSelectedSort.Text = " Избранная Сортировка ";
			this.tpSelectedSort.UseVisualStyleBackColor = true;
			// 
			// panelLV
			// 
			this.panelLV.Controls.Add(this.lvSSData);
			this.panelLV.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelLV.Location = new System.Drawing.Point(4, 379);
			this.panelLV.Margin = new System.Windows.Forms.Padding(4);
			this.panelLV.Name = "panelLV";
			this.panelLV.Size = new System.Drawing.Size(1209, 361);
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
			this.cHeaderExactFit});
			this.lvSSData.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvSSData.FullRowSelect = true;
			this.lvSSData.GridLines = true;
			this.lvSSData.Location = new System.Drawing.Point(0, 0);
			this.lvSSData.Margin = new System.Windows.Forms.Padding(4);
			this.lvSSData.Name = "lvSSData";
			this.lvSSData.Size = new System.Drawing.Size(1209, 361);
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
			// pSSData
			// 
			this.pSSData.Controls.Add(this.btnSSDataListLoad);
			this.pSSData.Controls.Add(this.btnSSDataListSave);
			this.pSSData.Controls.Add(this.btnSSGetData);
			this.pSSData.Dock = System.Windows.Forms.DockStyle.Top;
			this.pSSData.Location = new System.Drawing.Point(4, 315);
			this.pSSData.Margin = new System.Windows.Forms.Padding(4);
			this.pSSData.Name = "pSSData";
			this.pSSData.Size = new System.Drawing.Size(1209, 64);
			this.pSSData.TabIndex = 62;
			// 
			// btnSSDataListLoad
			// 
			this.btnSSDataListLoad.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSSDataListLoad.Image = ((System.Drawing.Image)(resources.GetObject("btnSSDataListLoad.Image")));
			this.btnSSDataListLoad.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnSSDataListLoad.Location = new System.Drawing.Point(1002, 6);
			this.btnSSDataListLoad.Margin = new System.Windows.Forms.Padding(4);
			this.btnSSDataListLoad.Name = "btnSSDataListLoad";
			this.btnSSDataListLoad.Size = new System.Drawing.Size(189, 49);
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
			this.btnSSDataListSave.Location = new System.Drawing.Point(798, 6);
			this.btnSSDataListSave.Margin = new System.Windows.Forms.Padding(4);
			this.btnSSDataListSave.Name = "btnSSDataListSave";
			this.btnSSDataListSave.Size = new System.Drawing.Size(188, 49);
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
			this.btnSSGetData.Location = new System.Drawing.Point(11, 6);
			this.btnSSGetData.Margin = new System.Windows.Forms.Padding(4);
			this.btnSSGetData.Name = "btnSSGetData";
			this.btnSSGetData.Size = new System.Drawing.Size(423, 49);
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
			this.pSelectedSortDirs.Location = new System.Drawing.Point(4, 226);
			this.pSelectedSortDirs.Margin = new System.Windows.Forms.Padding(4);
			this.pSelectedSortDirs.Name = "pSelectedSortDirs";
			this.pSelectedSortDirs.Size = new System.Drawing.Size(1209, 89);
			this.pSelectedSortDirs.TabIndex = 65;
			// 
			// btnSSTargetDir
			// 
			this.btnSSTargetDir.Image = ((System.Drawing.Image)(resources.GetObject("btnSSTargetDir.Image")));
			this.btnSSTargetDir.Location = new System.Drawing.Point(219, 47);
			this.btnSSTargetDir.Margin = new System.Windows.Forms.Padding(4);
			this.btnSSTargetDir.Name = "btnSSTargetDir";
			this.btnSSTargetDir.Size = new System.Drawing.Size(41, 33);
			this.btnSSTargetDir.TabIndex = 4;
			this.btnSSTargetDir.UseVisualStyleBackColor = true;
			this.btnSSTargetDir.Click += new System.EventHandler(this.BtnSSTargetDirClick);
			// 
			// btnSSOpenDir
			// 
			this.btnSSOpenDir.Image = ((System.Drawing.Image)(resources.GetObject("btnSSOpenDir.Image")));
			this.btnSSOpenDir.Location = new System.Drawing.Point(219, 7);
			this.btnSSOpenDir.Margin = new System.Windows.Forms.Padding(4);
			this.btnSSOpenDir.Name = "btnSSOpenDir";
			this.btnSSOpenDir.Size = new System.Drawing.Size(41, 33);
			this.btnSSOpenDir.TabIndex = 2;
			this.btnSSOpenDir.UseVisualStyleBackColor = true;
			this.btnSSOpenDir.Click += new System.EventHandler(this.BtnSSOpenDirClick);
			// 
			// lblSSTargetDir
			// 
			this.lblSSTargetDir.AutoSize = true;
			this.lblSSTargetDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblSSTargetDir.Location = new System.Drawing.Point(4, 53);
			this.lblSSTargetDir.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblSSTargetDir.Name = "lblSSTargetDir";
			this.lblSSTargetDir.Size = new System.Drawing.Size(197, 17);
			this.lblSSTargetDir.TabIndex = 18;
			this.lblSSTargetDir.Text = "Папка-приемник файлов:";
			// 
			// tboxSSToDir
			// 
			this.tboxSSToDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxSSToDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.tboxSSToDir.Location = new System.Drawing.Point(269, 50);
			this.tboxSSToDir.Margin = new System.Windows.Forms.Padding(4);
			this.tboxSSToDir.Name = "tboxSSToDir";
			this.tboxSSToDir.Size = new System.Drawing.Size(910, 24);
			this.tboxSSToDir.TabIndex = 3;
			this.tboxSSToDir.TextChanged += new System.EventHandler(this.TboxSSToDirTextChanged);
			// 
			// tboxSSSourceDir
			// 
			this.tboxSSSourceDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxSSSourceDir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tboxSSSourceDir.Location = new System.Drawing.Point(269, 9);
			this.tboxSSSourceDir.Margin = new System.Windows.Forms.Padding(4);
			this.tboxSSSourceDir.Name = "tboxSSSourceDir";
			this.tboxSSSourceDir.Size = new System.Drawing.Size(910, 24);
			this.tboxSSSourceDir.TabIndex = 1;
			this.tboxSSSourceDir.TextChanged += new System.EventHandler(this.TboxSSSourceDirTextChanged);
			// 
			// lbSSlDir
			// 
			this.lbSSlDir.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.lbSSlDir.Location = new System.Drawing.Point(4, 12);
			this.lbSSlDir.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbSSlDir.Name = "lbSSlDir";
			this.lbSSlDir.Size = new System.Drawing.Size(216, 23);
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
			this.pSSTemplate.Location = new System.Drawing.Point(4, 4);
			this.pSSTemplate.Margin = new System.Windows.Forms.Padding(4);
			this.pSSTemplate.Name = "pSSTemplate";
			this.pSSTemplate.Size = new System.Drawing.Size(1209, 222);
			this.pSSTemplate.TabIndex = 64;
			// 
			// buttonSSortRenew
			// 
			this.buttonSSortRenew.Font = new System.Drawing.Font("Tahoma", 11F);
			this.buttonSSortRenew.Image = ((System.Drawing.Image)(resources.GetObject("buttonSSortRenew.Image")));
			this.buttonSSortRenew.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.buttonSSortRenew.Location = new System.Drawing.Point(227, 6);
			this.buttonSSortRenew.Margin = new System.Windows.Forms.Padding(4);
			this.buttonSSortRenew.Name = "buttonSSortRenew";
			this.buttonSSortRenew.Size = new System.Drawing.Size(305, 54);
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
			this.buttonSSortFilesTo.Location = new System.Drawing.Point(4, 6);
			this.buttonSSortFilesTo.Margin = new System.Windows.Forms.Padding(4);
			this.buttonSSortFilesTo.Name = "buttonSSortFilesTo";
			this.buttonSSortFilesTo.Size = new System.Drawing.Size(211, 54);
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
			this.gBoxSelectedlSortOptions.Location = new System.Drawing.Point(826, 6);
			this.gBoxSelectedlSortOptions.Margin = new System.Windows.Forms.Padding(4);
			this.gBoxSelectedlSortOptions.Name = "gBoxSelectedlSortOptions";
			this.gBoxSelectedlSortOptions.Padding = new System.Windows.Forms.Padding(4);
			this.gBoxSelectedlSortOptions.Size = new System.Drawing.Size(373, 206);
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
			this.chBoxSSScanSubDir.Location = new System.Drawing.Point(8, 20);
			this.chBoxSSScanSubDir.Margin = new System.Windows.Forms.Padding(4);
			this.chBoxSSScanSubDir.Name = "chBoxSSScanSubDir";
			this.chBoxSSScanSubDir.Size = new System.Drawing.Size(231, 30);
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
			this.chBoxSSNotDelFB2Files.Location = new System.Drawing.Point(8, 66);
			this.chBoxSSNotDelFB2Files.Margin = new System.Windows.Forms.Padding(4);
			this.chBoxSSNotDelFB2Files.Name = "chBoxSSNotDelFB2Files";
			this.chBoxSSNotDelFB2Files.Size = new System.Drawing.Size(199, 30);
			this.chBoxSSNotDelFB2Files.TabIndex = 19;
			this.chBoxSSNotDelFB2Files.Text = "Сохранять оригиналы";
			this.chBoxSSNotDelFB2Files.UseVisualStyleBackColor = true;
			this.chBoxSSNotDelFB2Files.Click += new System.EventHandler(this.ChBoxSSNotDelFB2FilesClick);
			// 
			// chBoxSSToZip
			// 
			this.chBoxSSToZip.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chBoxSSToZip.Font = new System.Drawing.Font("Tahoma", 8F);
			this.chBoxSSToZip.Location = new System.Drawing.Point(8, 43);
			this.chBoxSSToZip.Margin = new System.Windows.Forms.Padding(4);
			this.chBoxSSToZip.Name = "chBoxSSToZip";
			this.chBoxSSToZip.Size = new System.Drawing.Size(173, 30);
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
			this.gBoxSelectedlSortRenameTemplates.Location = new System.Drawing.Point(4, 68);
			this.gBoxSelectedlSortRenameTemplates.Margin = new System.Windows.Forms.Padding(4);
			this.gBoxSelectedlSortRenameTemplates.Name = "gBoxSelectedlSortRenameTemplates";
			this.gBoxSelectedlSortRenameTemplates.Padding = new System.Windows.Forms.Padding(4);
			this.gBoxSelectedlSortRenameTemplates.Size = new System.Drawing.Size(810, 144);
			this.gBoxSelectedlSortRenameTemplates.TabIndex = 63;
			this.gBoxSelectedlSortRenameTemplates.TabStop = false;
			this.gBoxSelectedlSortRenameTemplates.Text = " Шаблоны подстановки ";
			// 
			// btnSSGroup
			// 
			this.btnSSGroup.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSSGroup.Location = new System.Drawing.Point(9, 101);
			this.btnSSGroup.Margin = new System.Windows.Forms.Padding(4);
			this.btnSSGroup.Name = "btnSSGroup";
			this.btnSSGroup.Size = new System.Drawing.Size(107, 28);
			this.btnSSGroup.TabIndex = 36;
			this.btnSSGroup.Text = "Группа";
			this.btnSSGroup.UseVisualStyleBackColor = true;
			this.btnSSGroup.Click += new System.EventHandler(this.BtnSSGroupClick);
			// 
			// btnSSGroupGenre
			// 
			this.btnSSGroupGenre.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSSGroupGenre.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSGroupGenre.Location = new System.Drawing.Point(124, 101);
			this.btnSSGroupGenre.Margin = new System.Windows.Forms.Padding(4);
			this.btnSSGroupGenre.Name = "btnSSGroupGenre";
			this.btnSSGroupGenre.Size = new System.Drawing.Size(128, 28);
			this.btnSSGroupGenre.TabIndex = 35;
			this.btnSSGroupGenre.Text = "Группа\\Жанр";
			this.btnSSGroupGenre.UseVisualStyleBackColor = true;
			this.btnSSGroupGenre.Click += new System.EventHandler(this.BtnSSGroupGenreClick);
			// 
			// btnSSLang
			// 
			this.btnSSLang.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSSLang.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSLang.Location = new System.Drawing.Point(9, 66);
			this.btnSSLang.Margin = new System.Windows.Forms.Padding(4);
			this.btnSSLang.Name = "btnSSLang";
			this.btnSSLang.Size = new System.Drawing.Size(107, 28);
			this.btnSSLang.TabIndex = 34;
			this.btnSSLang.Text = "Язык";
			this.btnSSLang.UseVisualStyleBackColor = true;
			this.btnSSLang.Click += new System.EventHandler(this.BtnSSLangClick);
			// 
			// btnSSRightBracket
			// 
			this.btnSSRightBracket.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSSRightBracket.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSRightBracket.Location = new System.Drawing.Point(677, 66);
			this.btnSSRightBracket.Margin = new System.Windows.Forms.Padding(4);
			this.btnSSRightBracket.Name = "btnSSRightBracket";
			this.btnSSRightBracket.Size = new System.Drawing.Size(31, 28);
			this.btnSSRightBracket.TabIndex = 33;
			this.btnSSRightBracket.Text = "]";
			this.btnSSRightBracket.UseVisualStyleBackColor = true;
			this.btnSSRightBracket.Click += new System.EventHandler(this.BtnSSRightBracketClick);
			// 
			// btnSSBook
			// 
			this.btnSSBook.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSSBook.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSBook.Location = new System.Drawing.Point(372, 101);
			this.btnSSBook.Margin = new System.Windows.Forms.Padding(4);
			this.btnSSBook.Name = "btnSSBook";
			this.btnSSBook.Size = new System.Drawing.Size(107, 28);
			this.btnSSBook.TabIndex = 28;
			this.btnSSBook.Text = "Книга";
			this.btnSSBook.UseVisualStyleBackColor = true;
			this.btnSSBook.Click += new System.EventHandler(this.BtnSSBookClick);
			// 
			// btnSSFamily
			// 
			this.btnSSFamily.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSSFamily.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSFamily.Location = new System.Drawing.Point(260, 66);
			this.btnSSFamily.Margin = new System.Windows.Forms.Padding(4);
			this.btnSSFamily.Name = "btnSSFamily";
			this.btnSSFamily.Size = new System.Drawing.Size(107, 28);
			this.btnSSFamily.TabIndex = 25;
			this.btnSSFamily.Text = "Фамилия";
			this.btnSSFamily.UseVisualStyleBackColor = true;
			this.btnSSFamily.Click += new System.EventHandler(this.BtnSSFamilyClick);
			// 
			// btnSSLeftBracket
			// 
			this.btnSSLeftBracket.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSSLeftBracket.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSLeftBracket.Location = new System.Drawing.Point(643, 66);
			this.btnSSLeftBracket.Margin = new System.Windows.Forms.Padding(4);
			this.btnSSLeftBracket.Name = "btnSSLeftBracket";
			this.btnSSLeftBracket.Size = new System.Drawing.Size(31, 28);
			this.btnSSLeftBracket.TabIndex = 32;
			this.btnSSLeftBracket.Text = "[";
			this.btnSSLeftBracket.UseVisualStyleBackColor = true;
			this.btnSSLeftBracket.Click += new System.EventHandler(this.BtnSSLeftBracketClick);
			// 
			// btnSSGenre
			// 
			this.btnSSGenre.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSSGenre.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSGenre.Location = new System.Drawing.Point(257, 101);
			this.btnSSGenre.Margin = new System.Windows.Forms.Padding(4);
			this.btnSSGenre.Name = "btnSSGenre";
			this.btnSSGenre.Size = new System.Drawing.Size(107, 28);
			this.btnSSGenre.TabIndex = 31;
			this.btnSSGenre.Text = "Жанр";
			this.btnSSGenre.UseVisualStyleBackColor = true;
			this.btnSSGenre.Click += new System.EventHandler(this.BtnSSGenreClick);
			// 
			// btnSSSequenceNumber
			// 
			this.btnSSSequenceNumber.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSSSequenceNumber.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSSequenceNumber.Location = new System.Drawing.Point(600, 101);
			this.btnSSSequenceNumber.Margin = new System.Windows.Forms.Padding(4);
			this.btnSSSequenceNumber.Name = "btnSSSequenceNumber";
			this.btnSSSequenceNumber.Size = new System.Drawing.Size(107, 28);
			this.btnSSSequenceNumber.TabIndex = 30;
			this.btnSSSequenceNumber.Text = "№ Серии";
			this.btnSSSequenceNumber.UseVisualStyleBackColor = true;
			this.btnSSSequenceNumber.Click += new System.EventHandler(this.BtnSSSequenceNumberClick);
			// 
			// btnSSSequence
			// 
			this.btnSSSequence.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSSSequence.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSSequence.Location = new System.Drawing.Point(487, 101);
			this.btnSSSequence.Margin = new System.Windows.Forms.Padding(4);
			this.btnSSSequence.Name = "btnSSSequence";
			this.btnSSSequence.Size = new System.Drawing.Size(107, 28);
			this.btnSSSequence.TabIndex = 29;
			this.btnSSSequence.Text = "Серия";
			this.btnSSSequence.UseVisualStyleBackColor = true;
			this.btnSSSequence.Click += new System.EventHandler(this.BtnSSSequenceClick);
			// 
			// btnSSPatronimic
			// 
			this.btnSSPatronimic.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSSPatronimic.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSPatronimic.Location = new System.Drawing.Point(488, 66);
			this.btnSSPatronimic.Margin = new System.Windows.Forms.Padding(4);
			this.btnSSPatronimic.Name = "btnSSPatronimic";
			this.btnSSPatronimic.Size = new System.Drawing.Size(107, 28);
			this.btnSSPatronimic.TabIndex = 27;
			this.btnSSPatronimic.Text = "Отчество";
			this.btnSSPatronimic.UseVisualStyleBackColor = true;
			this.btnSSPatronimic.Click += new System.EventHandler(this.BtnSSPatronimicClick);
			// 
			// btnSSName
			// 
			this.btnSSName.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSSName.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSName.Location = new System.Drawing.Point(375, 66);
			this.btnSSName.Margin = new System.Windows.Forms.Padding(4);
			this.btnSSName.Name = "btnSSName";
			this.btnSSName.Size = new System.Drawing.Size(107, 28);
			this.btnSSName.TabIndex = 26;
			this.btnSSName.Text = "Имя";
			this.btnSSName.UseVisualStyleBackColor = true;
			this.btnSSName.Click += new System.EventHandler(this.BtnSSNameClick);
			// 
			// btnSSDir
			// 
			this.btnSSDir.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSSDir.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSDir.Location = new System.Drawing.Point(604, 66);
			this.btnSSDir.Margin = new System.Windows.Forms.Padding(4);
			this.btnSSDir.Name = "btnSSDir";
			this.btnSSDir.Size = new System.Drawing.Size(31, 28);
			this.btnSSDir.TabIndex = 24;
			this.btnSSDir.Text = "\\";
			this.btnSSDir.UseVisualStyleBackColor = true;
			this.btnSSDir.Click += new System.EventHandler(this.BtnSSDirClick);
			// 
			// btnSSLetterFamily
			// 
			this.btnSSLetterFamily.Font = new System.Drawing.Font("Tahoma", 8F);
			this.btnSSLetterFamily.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnSSLetterFamily.Location = new System.Drawing.Point(124, 66);
			this.btnSSLetterFamily.Margin = new System.Windows.Forms.Padding(4);
			this.btnSSLetterFamily.Name = "btnSSLetterFamily";
			this.btnSSLetterFamily.Size = new System.Drawing.Size(128, 28);
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
			this.btnSSInsertTemplates.Location = new System.Drawing.Point(637, 21);
			this.btnSSInsertTemplates.Margin = new System.Windows.Forms.Padding(4);
			this.btnSSInsertTemplates.Name = "btnSSInsertTemplates";
			this.btnSSInsertTemplates.Size = new System.Drawing.Size(160, 34);
			this.btnSSInsertTemplates.TabIndex = 9;
			this.btnSSInsertTemplates.Text = "Вставить готовый";
			this.btnSSInsertTemplates.UseVisualStyleBackColor = true;
			this.btnSSInsertTemplates.Click += new System.EventHandler(this.BtnSSInsertTemplatesClick);
			// 
			// txtBoxSSTemplatesFromLine
			// 
			this.txtBoxSSTemplatesFromLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxSSTemplatesFromLine.Location = new System.Drawing.Point(8, 25);
			this.txtBoxSSTemplatesFromLine.Margin = new System.Windows.Forms.Padding(4);
			this.txtBoxSSTemplatesFromLine.Name = "txtBoxSSTemplatesFromLine";
			this.txtBoxSSTemplatesFromLine.Size = new System.Drawing.Size(621, 24);
			this.txtBoxSSTemplatesFromLine.TabIndex = 8;
			this.txtBoxSSTemplatesFromLine.TextChanged += new System.EventHandler(this.TxtBoxSSTemplatesFromLineTextChanged);
			// 
			// tcTemplates
			// 
			this.tcTemplates.Controls.Add(this.rtboxTemplatesList);
			this.tcTemplates.Location = new System.Drawing.Point(4, 25);
			this.tcTemplates.Margin = new System.Windows.Forms.Padding(4);
			this.tcTemplates.Name = "tcTemplates";
			this.tcTemplates.Padding = new System.Windows.Forms.Padding(4);
			this.tcTemplates.Size = new System.Drawing.Size(1217, 744);
			this.tcTemplates.TabIndex = 2;
			this.tcTemplates.Text = "Шаблоны подстановки";
			this.tcTemplates.UseVisualStyleBackColor = true;
			// 
			// rtboxTemplatesList
			// 
			this.rtboxTemplatesList.BackColor = System.Drawing.SystemColors.Window;
			this.rtboxTemplatesList.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtboxTemplatesList.ForeColor = System.Drawing.SystemColors.WindowText;
			this.rtboxTemplatesList.Location = new System.Drawing.Point(4, 4);
			this.rtboxTemplatesList.Margin = new System.Windows.Forms.Padding(4);
			this.rtboxTemplatesList.Name = "rtboxTemplatesList";
			this.rtboxTemplatesList.ReadOnly = true;
			this.rtboxTemplatesList.Size = new System.Drawing.Size(1209, 736);
			this.rtboxTemplatesList.TabIndex = 35;
			this.rtboxTemplatesList.Text = "";
			// 
			// tpSettings
			// 
			this.tpSettings.Controls.Add(this.tcFM);
			this.tpSettings.Controls.Add(this.panel1);
			this.tpSettings.Location = new System.Drawing.Point(4, 25);
			this.tpSettings.Margin = new System.Windows.Forms.Padding(4);
			this.tpSettings.Name = "tpSettings";
			this.tpSettings.Padding = new System.Windows.Forms.Padding(4);
			this.tpSettings.Size = new System.Drawing.Size(1217, 744);
			this.tpSettings.TabIndex = 3;
			this.tpSettings.Text = "Настройки";
			this.tpSettings.UseVisualStyleBackColor = true;
			// 
			// tcFM
			// 
			this.tcFM.Controls.Add(this.tpFMGeneral);
			this.tcFM.Controls.Add(this.tpFMNoTagsText);
			this.tcFM.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcFM.Location = new System.Drawing.Point(4, 4);
			this.tcFM.Margin = new System.Windows.Forms.Padding(4);
			this.tcFM.Name = "tcFM";
			this.tcFM.SelectedIndex = 0;
			this.tcFM.Size = new System.Drawing.Size(1209, 682);
			this.tcFM.TabIndex = 38;
			// 
			// tpFMGeneral
			// 
			this.tpFMGeneral.Controls.Add(this.gBoxOptions);
			this.tpFMGeneral.Controls.Add(this.gboxApportionment);
			this.tpFMGeneral.Controls.Add(this.gboxFMGeneral);
			this.tpFMGeneral.Location = new System.Drawing.Point(4, 25);
			this.tpFMGeneral.Margin = new System.Windows.Forms.Padding(4);
			this.tpFMGeneral.Name = "tpFMGeneral";
			this.tpFMGeneral.Padding = new System.Windows.Forms.Padding(4);
			this.tpFMGeneral.Size = new System.Drawing.Size(1201, 653);
			this.tpFMGeneral.TabIndex = 0;
			this.tpFMGeneral.Text = " Основные ";
			this.tpFMGeneral.UseVisualStyleBackColor = true;
			// 
			// gBoxOptions
			// 
			this.gBoxOptions.Controls.Add(this.comboBoxMaxSequenceLenght);
			this.gBoxOptions.Controls.Add(this.lblMaxSequenceLenght);
			this.gBoxOptions.Controls.Add(this.comboBoxMaxBTLenght);
			this.gBoxOptions.Controls.Add(this.lblMaxBTLenght);
			this.gBoxOptions.Dock = System.Windows.Forms.DockStyle.Top;
			this.gBoxOptions.ForeColor = System.Drawing.Color.Maroon;
			this.gBoxOptions.Location = new System.Drawing.Point(4, 409);
			this.gBoxOptions.Name = "gBoxOptions";
			this.gBoxOptions.Size = new System.Drawing.Size(1193, 105);
			this.gBoxOptions.TabIndex = 31;
			this.gBoxOptions.TabStop = false;
			this.gBoxOptions.Text = " Опции ";
			// 
			// comboBoxMaxSequenceLenght
			// 
			this.comboBoxMaxSequenceLenght.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxMaxSequenceLenght.FormattingEnabled = true;
			this.comboBoxMaxSequenceLenght.Items.AddRange(new object[] {
			"10",
			"15",
			"20",
			"25",
			"30",
			"35",
			"40",
			"45",
			"50",
			"55",
			"60",
			"65",
			"70",
			"75",
			"80",
			"85",
			"90",
			"95",
			"100"});
			this.comboBoxMaxSequenceLenght.Location = new System.Drawing.Point(480, 59);
			this.comboBoxMaxSequenceLenght.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.comboBoxMaxSequenceLenght.Name = "comboBoxMaxSequenceLenght";
			this.comboBoxMaxSequenceLenght.Size = new System.Drawing.Size(105, 24);
			this.comboBoxMaxSequenceLenght.TabIndex = 19;
			this.comboBoxMaxSequenceLenght.SelectedIndexChanged += new System.EventHandler(this.ComboBoxMaxSequenceLenghtSelectedIndexChanged);
			// 
			// lblMaxSequenceLenght
			// 
			this.lblMaxSequenceLenght.Font = new System.Drawing.Font("Tahoma", 8F);
			this.lblMaxSequenceLenght.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblMaxSequenceLenght.Location = new System.Drawing.Point(12, 61);
			this.lblMaxSequenceLenght.Name = "lblMaxSequenceLenght";
			this.lblMaxSequenceLenght.Size = new System.Drawing.Size(462, 23);
			this.lblMaxSequenceLenght.TabIndex = 18;
			this.lblMaxSequenceLenght.Text = "Шаблон подстановки *SN*: максимальная длина названия серии:";
			// 
			// comboBoxMaxBTLenght
			// 
			this.comboBoxMaxBTLenght.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxMaxBTLenght.FormattingEnabled = true;
			this.comboBoxMaxBTLenght.Items.AddRange(new object[] {
			"10",
			"15",
			"20",
			"25",
			"30",
			"35",
			"40",
			"45",
			"50",
			"55",
			"60",
			"65",
			"70",
			"75",
			"80",
			"85",
			"90",
			"95",
			"100"});
			this.comboBoxMaxBTLenght.Location = new System.Drawing.Point(480, 29);
			this.comboBoxMaxBTLenght.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.comboBoxMaxBTLenght.Name = "comboBoxMaxBTLenght";
			this.comboBoxMaxBTLenght.Size = new System.Drawing.Size(105, 24);
			this.comboBoxMaxBTLenght.TabIndex = 17;
			this.comboBoxMaxBTLenght.SelectedIndexChanged += new System.EventHandler(this.ComboBoxMaxBTLenghtSelectedIndexChanged);
			// 
			// lblMaxBTLenght
			// 
			this.lblMaxBTLenght.Font = new System.Drawing.Font("Tahoma", 8F);
			this.lblMaxBTLenght.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblMaxBTLenght.Location = new System.Drawing.Point(12, 31);
			this.lblMaxBTLenght.Name = "lblMaxBTLenght";
			this.lblMaxBTLenght.Size = new System.Drawing.Size(462, 23);
			this.lblMaxBTLenght.TabIndex = 16;
			this.lblMaxBTLenght.Text = "Шаблон подстановки *BT*: максимальная длина названия книги:";
			// 
			// gboxApportionment
			// 
			this.gboxApportionment.Controls.Add(this.gBoxGenres);
			this.gboxApportionment.Controls.Add(this.gBoxAuthors);
			this.gboxApportionment.Dock = System.Windows.Forms.DockStyle.Top;
			this.gboxApportionment.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gboxApportionment.ForeColor = System.Drawing.Color.Maroon;
			this.gboxApportionment.Location = new System.Drawing.Point(4, 217);
			this.gboxApportionment.Margin = new System.Windows.Forms.Padding(4);
			this.gboxApportionment.Name = "gboxApportionment";
			this.gboxApportionment.Padding = new System.Windows.Forms.Padding(4);
			this.gboxApportionment.Size = new System.Drawing.Size(1193, 192);
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
			this.gBoxGenres.Location = new System.Drawing.Point(4, 70);
			this.gBoxGenres.Margin = new System.Windows.Forms.Padding(4);
			this.gBoxGenres.Name = "gBoxGenres";
			this.gBoxGenres.Padding = new System.Windows.Forms.Padding(4);
			this.gBoxGenres.Size = new System.Drawing.Size(1185, 112);
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
			this.gBoxGenresType.Location = new System.Drawing.Point(347, 23);
			this.gBoxGenresType.Margin = new System.Windows.Forms.Padding(4);
			this.gBoxGenresType.Name = "gBoxGenresType";
			this.gBoxGenresType.Padding = new System.Windows.Forms.Padding(4);
			this.gBoxGenresType.Size = new System.Drawing.Size(441, 70);
			this.gBoxGenresType.TabIndex = 27;
			this.gBoxGenresType.TabStop = false;
			this.gBoxGenresType.Text = " Вид папки - жанра ";
			// 
			// rbtnGenreText
			// 
			this.rbtnGenreText.Dock = System.Windows.Forms.DockStyle.Top;
			this.rbtnGenreText.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnGenreText.Location = new System.Drawing.Point(4, 43);
			this.rbtnGenreText.Margin = new System.Windows.Forms.Padding(4);
			this.rbtnGenreText.Name = "rbtnGenreText";
			this.rbtnGenreText.Size = new System.Drawing.Size(433, 22);
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
			this.rbtnGenreSchema.Location = new System.Drawing.Point(4, 21);
			this.rbtnGenreSchema.Margin = new System.Windows.Forms.Padding(4);
			this.rbtnGenreSchema.Name = "rbtnGenreSchema";
			this.rbtnGenreSchema.Size = new System.Drawing.Size(433, 22);
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
			this.gBoxGenresCount.Location = new System.Drawing.Point(8, 23);
			this.gBoxGenresCount.Margin = new System.Windows.Forms.Padding(4);
			this.gBoxGenresCount.Name = "gBoxGenresCount";
			this.gBoxGenresCount.Padding = new System.Windows.Forms.Padding(4);
			this.gBoxGenresCount.Size = new System.Drawing.Size(331, 70);
			this.gBoxGenresCount.TabIndex = 26;
			this.gBoxGenresCount.TabStop = false;
			this.gBoxGenresCount.Text = " Раскладка файлов по жанрам ";
			// 
			// rbtnGenreAll
			// 
			this.rbtnGenreAll.Dock = System.Windows.Forms.DockStyle.Top;
			this.rbtnGenreAll.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnGenreAll.Location = new System.Drawing.Point(4, 43);
			this.rbtnGenreAll.Margin = new System.Windows.Forms.Padding(4);
			this.rbtnGenreAll.Name = "rbtnGenreAll";
			this.rbtnGenreAll.Size = new System.Drawing.Size(323, 22);
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
			this.rbtnGenreOne.Location = new System.Drawing.Point(4, 21);
			this.rbtnGenreOne.Margin = new System.Windows.Forms.Padding(4);
			this.rbtnGenreOne.Name = "rbtnGenreOne";
			this.rbtnGenreOne.Size = new System.Drawing.Size(323, 22);
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
			this.gBoxAuthors.Location = new System.Drawing.Point(4, 21);
			this.gBoxAuthors.Margin = new System.Windows.Forms.Padding(4);
			this.gBoxAuthors.Name = "gBoxAuthors";
			this.gBoxAuthors.Padding = new System.Windows.Forms.Padding(4);
			this.gBoxAuthors.Size = new System.Drawing.Size(1185, 49);
			this.gBoxAuthors.TabIndex = 27;
			this.gBoxAuthors.TabStop = false;
			this.gBoxAuthors.Text = " Авторы ";
			// 
			// rbtnAuthorAll
			// 
			this.rbtnAuthorAll.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnAuthorAll.Location = new System.Drawing.Point(205, 20);
			this.rbtnAuthorAll.Margin = new System.Windows.Forms.Padding(4);
			this.rbtnAuthorAll.Name = "rbtnAuthorAll";
			this.rbtnAuthorAll.Size = new System.Drawing.Size(176, 22);
			this.rbtnAuthorAll.TabIndex = 1;
			this.rbtnAuthorAll.Text = "По всем авторам";
			this.rbtnAuthorAll.UseVisualStyleBackColor = true;
			this.rbtnAuthorAll.Click += new System.EventHandler(this.RbtnAuthorOneClick);
			// 
			// rbtnAuthorOne
			// 
			this.rbtnAuthorOne.Checked = true;
			this.rbtnAuthorOne.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnAuthorOne.Location = new System.Drawing.Point(4, 20);
			this.rbtnAuthorOne.Margin = new System.Windows.Forms.Padding(4);
			this.rbtnAuthorOne.Name = "rbtnAuthorOne";
			this.rbtnAuthorOne.Size = new System.Drawing.Size(193, 22);
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
			this.gboxFMGeneral.Location = new System.Drawing.Point(4, 4);
			this.gboxFMGeneral.Margin = new System.Windows.Forms.Padding(4);
			this.gboxFMGeneral.Name = "gboxFMGeneral";
			this.gboxFMGeneral.Padding = new System.Windows.Forms.Padding(4);
			this.gboxFMGeneral.Size = new System.Drawing.Size(1193, 213);
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
			this.pSortFB2.Location = new System.Drawing.Point(4, 180);
			this.pSortFB2.Margin = new System.Windows.Forms.Padding(4);
			this.pSortFB2.Name = "pSortFB2";
			this.pSortFB2.Size = new System.Drawing.Size(1185, 28);
			this.pSortFB2.TabIndex = 30;
			// 
			// rbtnFMOnlyValidFB2
			// 
			this.rbtnFMOnlyValidFB2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.rbtnFMOnlyValidFB2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnFMOnlyValidFB2.Location = new System.Drawing.Point(389, 1);
			this.rbtnFMOnlyValidFB2.Margin = new System.Windows.Forms.Padding(4);
			this.rbtnFMOnlyValidFB2.Name = "rbtnFMOnlyValidFB2";
			this.rbtnFMOnlyValidFB2.Size = new System.Drawing.Size(243, 21);
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
			this.rbtnFMAllFB2.Location = new System.Drawing.Point(187, 2);
			this.rbtnFMAllFB2.Margin = new System.Windows.Forms.Padding(4);
			this.rbtnFMAllFB2.Name = "rbtnFMAllFB2";
			this.rbtnFMAllFB2.Size = new System.Drawing.Size(193, 21);
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
			this.label11.Location = new System.Drawing.Point(7, 4);
			this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(177, 20);
			this.label11.TabIndex = 0;
			this.label11.Text = "Сортировка файлов:";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.cboxFileExist);
			this.panel2.Controls.Add(this.lbFilelExist);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(4, 149);
			this.panel2.Margin = new System.Windows.Forms.Padding(4);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(1185, 31);
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
			this.cboxFileExist.Location = new System.Drawing.Point(201, 2);
			this.cboxFileExist.Margin = new System.Windows.Forms.Padding(4);
			this.cboxFileExist.Name = "cboxFileExist";
			this.cboxFileExist.Size = new System.Drawing.Size(565, 24);
			this.cboxFileExist.TabIndex = 20;
			this.cboxFileExist.SelectedIndexChanged += new System.EventHandler(this.CboxFileExistSelectedIndexChanged);
			// 
			// lbFilelExist
			// 
			this.lbFilelExist.AutoSize = true;
			this.lbFilelExist.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lbFilelExist.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lbFilelExist.Location = new System.Drawing.Point(3, 6);
			this.lbFilelExist.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lbFilelExist.Name = "lbFilelExist";
			this.lbFilelExist.Size = new System.Drawing.Size(163, 17);
			this.lbFilelExist.TabIndex = 19;
			this.lbFilelExist.Text = "Одинаковые файлы:";
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.cboxSpace);
			this.panel3.Controls.Add(this.lblSpace);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(4, 113);
			this.panel3.Margin = new System.Windows.Forms.Padding(4);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(1185, 36);
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
			this.cboxSpace.Location = new System.Drawing.Point(201, 6);
			this.cboxSpace.Margin = new System.Windows.Forms.Padding(4);
			this.cboxSpace.Name = "cboxSpace";
			this.cboxSpace.Size = new System.Drawing.Size(163, 24);
			this.cboxSpace.TabIndex = 24;
			this.cboxSpace.SelectedIndexChanged += new System.EventHandler(this.CboxSpaceSelectedIndexChanged);
			// 
			// lblSpace
			// 
			this.lblSpace.AutoSize = true;
			this.lblSpace.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblSpace.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblSpace.Location = new System.Drawing.Point(3, 10);
			this.lblSpace.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblSpace.Name = "lblSpace";
			this.lblSpace.Size = new System.Drawing.Size(167, 17);
			this.lblSpace.TabIndex = 23;
			this.lblSpace.Text = "Обработка пробелов:";
			// 
			// chBoxStrict
			// 
			this.chBoxStrict.Dock = System.Windows.Forms.DockStyle.Top;
			this.chBoxStrict.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxStrict.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chBoxStrict.Location = new System.Drawing.Point(4, 91);
			this.chBoxStrict.Margin = new System.Windows.Forms.Padding(4);
			this.chBoxStrict.Name = "chBoxStrict";
			this.chBoxStrict.Size = new System.Drawing.Size(1185, 22);
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
			this.chBoxTranslit.Location = new System.Drawing.Point(4, 69);
			this.chBoxTranslit.Margin = new System.Windows.Forms.Padding(4);
			this.chBoxTranslit.Name = "chBoxTranslit";
			this.chBoxTranslit.Size = new System.Drawing.Size(1185, 22);
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
			this.gboxRegister.Location = new System.Drawing.Point(4, 21);
			this.gboxRegister.Margin = new System.Windows.Forms.Padding(4);
			this.gboxRegister.Name = "gboxRegister";
			this.gboxRegister.Padding = new System.Windows.Forms.Padding(4);
			this.gboxRegister.Size = new System.Drawing.Size(1185, 48);
			this.gboxRegister.TabIndex = 10;
			this.gboxRegister.TabStop = false;
			this.gboxRegister.Text = " Регистр имени файла ";
			// 
			// rbtnAsSentence
			// 
			this.rbtnAsSentence.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnAsSentence.Location = new System.Drawing.Point(479, 20);
			this.rbtnAsSentence.Margin = new System.Windows.Forms.Padding(4);
			this.rbtnAsSentence.Name = "rbtnAsSentence";
			this.rbtnAsSentence.Size = new System.Drawing.Size(289, 22);
			this.rbtnAsSentence.TabIndex = 3;
			this.rbtnAsSentence.Text = "Каждое Слово С Большой Буквы";
			this.rbtnAsSentence.UseVisualStyleBackColor = true;
			this.rbtnAsSentence.Click += new System.EventHandler(this.RbtnAsIsClick);
			// 
			// rbtnUpper
			// 
			this.rbtnUpper.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnUpper.Location = new System.Drawing.Point(285, 20);
			this.rbtnUpper.Margin = new System.Windows.Forms.Padding(4);
			this.rbtnUpper.Name = "rbtnUpper";
			this.rbtnUpper.Size = new System.Drawing.Size(185, 22);
			this.rbtnUpper.TabIndex = 2;
			this.rbtnUpper.Text = "ПРОПИСНЫЕ БУКВЫ";
			this.rbtnUpper.UseVisualStyleBackColor = true;
			this.rbtnUpper.Click += new System.EventHandler(this.RbtnAsIsClick);
			// 
			// rbtnLower
			// 
			this.rbtnLower.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnLower.Location = new System.Drawing.Point(111, 20);
			this.rbtnLower.Margin = new System.Windows.Forms.Padding(4);
			this.rbtnLower.Name = "rbtnLower";
			this.rbtnLower.Size = new System.Drawing.Size(169, 22);
			this.rbtnLower.TabIndex = 1;
			this.rbtnLower.Text = "строчные буквы";
			this.rbtnLower.UseVisualStyleBackColor = true;
			this.rbtnLower.Click += new System.EventHandler(this.RbtnAsIsClick);
			// 
			// rbtnAsIs
			// 
			this.rbtnAsIs.Checked = true;
			this.rbtnAsIs.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnAsIs.Location = new System.Drawing.Point(4, 20);
			this.rbtnAsIs.Margin = new System.Windows.Forms.Padding(4);
			this.rbtnAsIs.Name = "rbtnAsIs";
			this.rbtnAsIs.Size = new System.Drawing.Size(104, 22);
			this.rbtnAsIs.TabIndex = 0;
			this.rbtnAsIs.TabStop = true;
			this.rbtnAsIs.Text = "Как есть";
			this.rbtnAsIs.UseVisualStyleBackColor = true;
			this.rbtnAsIs.Click += new System.EventHandler(this.RbtnAsIsClick);
			// 
			// tpFMNoTagsText
			// 
			this.tpFMNoTagsText.Controls.Add(this.tcDesc);
			this.tpFMNoTagsText.Location = new System.Drawing.Point(4, 25);
			this.tpFMNoTagsText.Margin = new System.Windows.Forms.Padding(4);
			this.tpFMNoTagsText.Name = "tpFMNoTagsText";
			this.tpFMNoTagsText.Size = new System.Drawing.Size(1201, 653);
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
			this.tcDesc.Margin = new System.Windows.Forms.Padding(4);
			this.tcDesc.Multiline = true;
			this.tcDesc.Name = "tcDesc";
			this.tcDesc.SelectedIndex = 0;
			this.tcDesc.Size = new System.Drawing.Size(1201, 653);
			this.tcDesc.TabIndex = 0;
			// 
			// tpBookInfo
			// 
			this.tpBookInfo.Controls.Add(this.gBoxFMBINoTags);
			this.tpBookInfo.Location = new System.Drawing.Point(26, 4);
			this.tpBookInfo.Margin = new System.Windows.Forms.Padding(4);
			this.tpBookInfo.Name = "tpBookInfo";
			this.tpBookInfo.Padding = new System.Windows.Forms.Padding(4);
			this.tpBookInfo.Size = new System.Drawing.Size(1171, 645);
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
			this.gBoxFMBINoTags.Location = new System.Drawing.Point(4, 4);
			this.gBoxFMBINoTags.Margin = new System.Windows.Forms.Padding(4);
			this.gBoxFMBINoTags.Name = "gBoxFMBINoTags";
			this.gBoxFMBINoTags.Padding = new System.Windows.Forms.Padding(4);
			this.gBoxFMBINoTags.Size = new System.Drawing.Size(1163, 637);
			this.gBoxFMBINoTags.TabIndex = 1;
			this.gBoxFMBINoTags.TabStop = false;
			this.gBoxFMBINoTags.Text = " Для отсутствующих данных тэгов ";
			// 
			// panel30
			// 
			this.panel30.Controls.Add(this.txtBoxFMNoDateValue);
			this.panel30.Controls.Add(this.label31);
			this.panel30.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel30.Location = new System.Drawing.Point(4, 449);
			this.panel30.Margin = new System.Windows.Forms.Padding(4);
			this.panel30.Name = "panel30";
			this.panel30.Size = new System.Drawing.Size(1155, 39);
			this.panel30.TabIndex = 11;
			// 
			// txtBoxFMNoDateValue
			// 
			this.txtBoxFMNoDateValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoDateValue.Location = new System.Drawing.Point(257, 7);
			this.txtBoxFMNoDateValue.Margin = new System.Windows.Forms.Padding(4);
			this.txtBoxFMNoDateValue.Name = "txtBoxFMNoDateValue";
			this.txtBoxFMNoDateValue.Size = new System.Drawing.Size(874, 23);
			this.txtBoxFMNoDateValue.TabIndex = 1;
			this.txtBoxFMNoDateValue.TextChanged += new System.EventHandler(this.TxtBoxFMNoGenreGroupTextChanged);
			// 
			// label31
			// 
			this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label31.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label31.Location = new System.Drawing.Point(4, 10);
			this.label31.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(248, 22);
			this.label31.TabIndex = 0;
			this.label31.Text = "Даты написания (знач.) Нет:";
			this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel29
			// 
			this.panel29.Controls.Add(this.txtBoxFMNoDateText);
			this.panel29.Controls.Add(this.label30);
			this.panel29.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel29.Location = new System.Drawing.Point(4, 410);
			this.panel29.Margin = new System.Windows.Forms.Padding(4);
			this.panel29.Name = "panel29";
			this.panel29.Size = new System.Drawing.Size(1155, 39);
			this.panel29.TabIndex = 10;
			// 
			// txtBoxFMNoDateText
			// 
			this.txtBoxFMNoDateText.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoDateText.Location = new System.Drawing.Point(257, 7);
			this.txtBoxFMNoDateText.Margin = new System.Windows.Forms.Padding(4);
			this.txtBoxFMNoDateText.Name = "txtBoxFMNoDateText";
			this.txtBoxFMNoDateText.Size = new System.Drawing.Size(874, 23);
			this.txtBoxFMNoDateText.TabIndex = 1;
			this.txtBoxFMNoDateText.TextChanged += new System.EventHandler(this.TxtBoxFMNoGenreGroupTextChanged);
			// 
			// label30
			// 
			this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label30.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label30.Location = new System.Drawing.Point(4, 10);
			this.label30.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(248, 22);
			this.label30.TabIndex = 0;
			this.label30.Text = "Даты написания (текст) Нет:";
			this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel12
			// 
			this.panel12.Controls.Add(this.txtBoxFMNoNSequence);
			this.panel12.Controls.Add(this.label10);
			this.panel12.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel12.Location = new System.Drawing.Point(4, 371);
			this.panel12.Margin = new System.Windows.Forms.Padding(4);
			this.panel12.Name = "panel12";
			this.panel12.Size = new System.Drawing.Size(1155, 39);
			this.panel12.TabIndex = 9;
			// 
			// txtBoxFMNoNSequence
			// 
			this.txtBoxFMNoNSequence.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoNSequence.Location = new System.Drawing.Point(257, 7);
			this.txtBoxFMNoNSequence.Margin = new System.Windows.Forms.Padding(4);
			this.txtBoxFMNoNSequence.Name = "txtBoxFMNoNSequence";
			this.txtBoxFMNoNSequence.Size = new System.Drawing.Size(874, 23);
			this.txtBoxFMNoNSequence.TabIndex = 1;
			this.txtBoxFMNoNSequence.TextChanged += new System.EventHandler(this.TxtBoxFMNoGenreGroupTextChanged);
			// 
			// label10
			// 
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label10.Location = new System.Drawing.Point(4, 10);
			this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(248, 22);
			this.label10.TabIndex = 0;
			this.label10.Text = "Номера Серии Нет:";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel11
			// 
			this.panel11.Controls.Add(this.txtBoxFMNoSequence);
			this.panel11.Controls.Add(this.label9);
			this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel11.Location = new System.Drawing.Point(4, 332);
			this.panel11.Margin = new System.Windows.Forms.Padding(4);
			this.panel11.Name = "panel11";
			this.panel11.Size = new System.Drawing.Size(1155, 39);
			this.panel11.TabIndex = 8;
			// 
			// txtBoxFMNoSequence
			// 
			this.txtBoxFMNoSequence.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoSequence.Location = new System.Drawing.Point(257, 7);
			this.txtBoxFMNoSequence.Margin = new System.Windows.Forms.Padding(4);
			this.txtBoxFMNoSequence.Name = "txtBoxFMNoSequence";
			this.txtBoxFMNoSequence.Size = new System.Drawing.Size(874, 23);
			this.txtBoxFMNoSequence.TabIndex = 1;
			this.txtBoxFMNoSequence.TextChanged += new System.EventHandler(this.TxtBoxFMNoGenreGroupTextChanged);
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label9.Location = new System.Drawing.Point(4, 10);
			this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(248, 22);
			this.label9.TabIndex = 0;
			this.label9.Text = "Серии Нет:";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel10
			// 
			this.panel10.Controls.Add(this.txtBoxFMNoBookTitle);
			this.panel10.Controls.Add(this.label8);
			this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel10.Location = new System.Drawing.Point(4, 293);
			this.panel10.Margin = new System.Windows.Forms.Padding(4);
			this.panel10.Name = "panel10";
			this.panel10.Size = new System.Drawing.Size(1155, 39);
			this.panel10.TabIndex = 7;
			// 
			// txtBoxFMNoBookTitle
			// 
			this.txtBoxFMNoBookTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoBookTitle.Location = new System.Drawing.Point(257, 7);
			this.txtBoxFMNoBookTitle.Margin = new System.Windows.Forms.Padding(4);
			this.txtBoxFMNoBookTitle.Name = "txtBoxFMNoBookTitle";
			this.txtBoxFMNoBookTitle.Size = new System.Drawing.Size(874, 23);
			this.txtBoxFMNoBookTitle.TabIndex = 1;
			this.txtBoxFMNoBookTitle.TextChanged += new System.EventHandler(this.TxtBoxFMNoGenreGroupTextChanged);
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label8.Location = new System.Drawing.Point(4, 10);
			this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(248, 22);
			this.label8.TabIndex = 0;
			this.label8.Text = "Названия Книги Нет:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel9
			// 
			this.panel9.Controls.Add(this.txtBoxFMNoNickName);
			this.panel9.Controls.Add(this.label7);
			this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel9.Location = new System.Drawing.Point(4, 254);
			this.panel9.Margin = new System.Windows.Forms.Padding(4);
			this.panel9.Name = "panel9";
			this.panel9.Size = new System.Drawing.Size(1155, 39);
			this.panel9.TabIndex = 6;
			// 
			// txtBoxFMNoNickName
			// 
			this.txtBoxFMNoNickName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoNickName.Location = new System.Drawing.Point(257, 7);
			this.txtBoxFMNoNickName.Margin = new System.Windows.Forms.Padding(4);
			this.txtBoxFMNoNickName.Name = "txtBoxFMNoNickName";
			this.txtBoxFMNoNickName.Size = new System.Drawing.Size(874, 23);
			this.txtBoxFMNoNickName.TabIndex = 1;
			this.txtBoxFMNoNickName.TextChanged += new System.EventHandler(this.TxtBoxFMNoGenreGroupTextChanged);
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label7.Location = new System.Drawing.Point(4, 10);
			this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(248, 22);
			this.label7.TabIndex = 0;
			this.label7.Text = "Ника Автора Нет:";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel8
			// 
			this.panel8.Controls.Add(this.txtBoxFMNoLastName);
			this.panel8.Controls.Add(this.label6);
			this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel8.Location = new System.Drawing.Point(4, 215);
			this.panel8.Margin = new System.Windows.Forms.Padding(4);
			this.panel8.Name = "panel8";
			this.panel8.Size = new System.Drawing.Size(1155, 39);
			this.panel8.TabIndex = 5;
			// 
			// txtBoxFMNoLastName
			// 
			this.txtBoxFMNoLastName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoLastName.Location = new System.Drawing.Point(257, 7);
			this.txtBoxFMNoLastName.Margin = new System.Windows.Forms.Padding(4);
			this.txtBoxFMNoLastName.Name = "txtBoxFMNoLastName";
			this.txtBoxFMNoLastName.Size = new System.Drawing.Size(874, 23);
			this.txtBoxFMNoLastName.TabIndex = 1;
			this.txtBoxFMNoLastName.TextChanged += new System.EventHandler(this.TxtBoxFMNoGenreGroupTextChanged);
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label6.Location = new System.Drawing.Point(4, 10);
			this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(248, 22);
			this.label6.TabIndex = 0;
			this.label6.Text = "Фамилия Автора Нет:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel7
			// 
			this.panel7.Controls.Add(this.txtBoxFMNoMiddleName);
			this.panel7.Controls.Add(this.label5);
			this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel7.Location = new System.Drawing.Point(4, 176);
			this.panel7.Margin = new System.Windows.Forms.Padding(4);
			this.panel7.Name = "panel7";
			this.panel7.Size = new System.Drawing.Size(1155, 39);
			this.panel7.TabIndex = 4;
			// 
			// txtBoxFMNoMiddleName
			// 
			this.txtBoxFMNoMiddleName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoMiddleName.Location = new System.Drawing.Point(257, 7);
			this.txtBoxFMNoMiddleName.Margin = new System.Windows.Forms.Padding(4);
			this.txtBoxFMNoMiddleName.Name = "txtBoxFMNoMiddleName";
			this.txtBoxFMNoMiddleName.Size = new System.Drawing.Size(874, 23);
			this.txtBoxFMNoMiddleName.TabIndex = 1;
			this.txtBoxFMNoMiddleName.TextChanged += new System.EventHandler(this.TxtBoxFMNoGenreGroupTextChanged);
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label5.Location = new System.Drawing.Point(4, 10);
			this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(248, 22);
			this.label5.TabIndex = 0;
			this.label5.Text = "Отчества Автора Нет:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel6
			// 
			this.panel6.Controls.Add(this.txtBoxFMNoFirstName);
			this.panel6.Controls.Add(this.label4);
			this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel6.Location = new System.Drawing.Point(4, 137);
			this.panel6.Margin = new System.Windows.Forms.Padding(4);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(1155, 39);
			this.panel6.TabIndex = 3;
			// 
			// txtBoxFMNoFirstName
			// 
			this.txtBoxFMNoFirstName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoFirstName.Location = new System.Drawing.Point(257, 7);
			this.txtBoxFMNoFirstName.Margin = new System.Windows.Forms.Padding(4);
			this.txtBoxFMNoFirstName.Name = "txtBoxFMNoFirstName";
			this.txtBoxFMNoFirstName.Size = new System.Drawing.Size(874, 23);
			this.txtBoxFMNoFirstName.TabIndex = 1;
			this.txtBoxFMNoFirstName.TextChanged += new System.EventHandler(this.TxtBoxFMNoGenreGroupTextChanged);
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label4.Location = new System.Drawing.Point(4, 10);
			this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(248, 22);
			this.label4.TabIndex = 0;
			this.label4.Text = "Имени Автора Нет:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this.txtBoxFMNoLang);
			this.panel5.Controls.Add(this.label3);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel5.Location = new System.Drawing.Point(4, 98);
			this.panel5.Margin = new System.Windows.Forms.Padding(4);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(1155, 39);
			this.panel5.TabIndex = 2;
			// 
			// txtBoxFMNoLang
			// 
			this.txtBoxFMNoLang.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoLang.Location = new System.Drawing.Point(257, 7);
			this.txtBoxFMNoLang.Margin = new System.Windows.Forms.Padding(4);
			this.txtBoxFMNoLang.Name = "txtBoxFMNoLang";
			this.txtBoxFMNoLang.Size = new System.Drawing.Size(874, 23);
			this.txtBoxFMNoLang.TabIndex = 1;
			this.txtBoxFMNoLang.TextChanged += new System.EventHandler(this.TxtBoxFMNoGenreGroupTextChanged);
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label3.Location = new System.Drawing.Point(4, 10);
			this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(248, 22);
			this.label3.TabIndex = 0;
			this.label3.Text = "Языка Книги Нет:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.txtBoxFMNoGenre);
			this.panel4.Controls.Add(this.label2);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel4.Location = new System.Drawing.Point(4, 59);
			this.panel4.Margin = new System.Windows.Forms.Padding(4);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(1155, 39);
			this.panel4.TabIndex = 1;
			// 
			// txtBoxFMNoGenre
			// 
			this.txtBoxFMNoGenre.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoGenre.Location = new System.Drawing.Point(257, 7);
			this.txtBoxFMNoGenre.Margin = new System.Windows.Forms.Padding(4);
			this.txtBoxFMNoGenre.Name = "txtBoxFMNoGenre";
			this.txtBoxFMNoGenre.Size = new System.Drawing.Size(874, 23);
			this.txtBoxFMNoGenre.TabIndex = 1;
			this.txtBoxFMNoGenre.TextChanged += new System.EventHandler(this.TxtBoxFMNoGenreGroupTextChanged);
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label2.Location = new System.Drawing.Point(4, 10);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(248, 22);
			this.label2.TabIndex = 0;
			this.label2.Text = "Жанра Книги Нет:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel13
			// 
			this.panel13.Controls.Add(this.txtBoxFMNoGenreGroup);
			this.panel13.Controls.Add(this.label1);
			this.panel13.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel13.Location = new System.Drawing.Point(4, 20);
			this.panel13.Margin = new System.Windows.Forms.Padding(4);
			this.panel13.Name = "panel13";
			this.panel13.Size = new System.Drawing.Size(1155, 39);
			this.panel13.TabIndex = 0;
			// 
			// txtBoxFMNoGenreGroup
			// 
			this.txtBoxFMNoGenreGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoGenreGroup.Location = new System.Drawing.Point(257, 7);
			this.txtBoxFMNoGenreGroup.Margin = new System.Windows.Forms.Padding(4);
			this.txtBoxFMNoGenreGroup.Name = "txtBoxFMNoGenreGroup";
			this.txtBoxFMNoGenreGroup.Size = new System.Drawing.Size(874, 23);
			this.txtBoxFMNoGenreGroup.TabIndex = 1;
			this.txtBoxFMNoGenreGroup.TextChanged += new System.EventHandler(this.TxtBoxFMNoGenreGroupTextChanged);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label1.Location = new System.Drawing.Point(4, 10);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(248, 22);
			this.label1.TabIndex = 0;
			this.label1.Text = "Неизвестная Группа Жанров:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tpPublishInfo
			// 
			this.tpPublishInfo.Controls.Add(this.gBoxFMPINoTags);
			this.tpPublishInfo.Location = new System.Drawing.Point(26, 4);
			this.tpPublishInfo.Margin = new System.Windows.Forms.Padding(4);
			this.tpPublishInfo.Name = "tpPublishInfo";
			this.tpPublishInfo.Padding = new System.Windows.Forms.Padding(4);
			this.tpPublishInfo.Size = new System.Drawing.Size(1171, 645);
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
			this.gBoxFMPINoTags.Location = new System.Drawing.Point(4, 4);
			this.gBoxFMPINoTags.Margin = new System.Windows.Forms.Padding(4);
			this.gBoxFMPINoTags.Name = "gBoxFMPINoTags";
			this.gBoxFMPINoTags.Padding = new System.Windows.Forms.Padding(4);
			this.gBoxFMPINoTags.Size = new System.Drawing.Size(1163, 637);
			this.gBoxFMPINoTags.TabIndex = 0;
			this.gBoxFMPINoTags.TabStop = false;
			this.gBoxFMPINoTags.Text = " Для отсутствующих данных тэгов ";
			// 
			// panel33
			// 
			this.panel33.Controls.Add(this.txtBoxFMNoCity);
			this.panel33.Controls.Add(this.label34);
			this.panel33.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel33.Location = new System.Drawing.Point(4, 99);
			this.panel33.Margin = new System.Windows.Forms.Padding(4);
			this.panel33.Name = "panel33";
			this.panel33.Size = new System.Drawing.Size(1155, 39);
			this.panel33.TabIndex = 15;
			// 
			// txtBoxFMNoCity
			// 
			this.txtBoxFMNoCity.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoCity.Location = new System.Drawing.Point(257, 7);
			this.txtBoxFMNoCity.Margin = new System.Windows.Forms.Padding(4);
			this.txtBoxFMNoCity.Name = "txtBoxFMNoCity";
			this.txtBoxFMNoCity.Size = new System.Drawing.Size(874, 24);
			this.txtBoxFMNoCity.TabIndex = 1;
			this.txtBoxFMNoCity.TextChanged += new System.EventHandler(this.TxtBoxFMNoYearTextChanged);
			// 
			// label34
			// 
			this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label34.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label34.Location = new System.Drawing.Point(4, 10);
			this.label34.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(248, 22);
			this.label34.TabIndex = 0;
			this.label34.Text = "Города Издательства Нет:";
			this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel32
			// 
			this.panel32.Controls.Add(this.txtBoxFMNoPublisher);
			this.panel32.Controls.Add(this.label33);
			this.panel32.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel32.Location = new System.Drawing.Point(4, 60);
			this.panel32.Margin = new System.Windows.Forms.Padding(4);
			this.panel32.Name = "panel32";
			this.panel32.Size = new System.Drawing.Size(1155, 39);
			this.panel32.TabIndex = 14;
			// 
			// txtBoxFMNoPublisher
			// 
			this.txtBoxFMNoPublisher.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoPublisher.Location = new System.Drawing.Point(257, 7);
			this.txtBoxFMNoPublisher.Margin = new System.Windows.Forms.Padding(4);
			this.txtBoxFMNoPublisher.Name = "txtBoxFMNoPublisher";
			this.txtBoxFMNoPublisher.Size = new System.Drawing.Size(874, 24);
			this.txtBoxFMNoPublisher.TabIndex = 1;
			this.txtBoxFMNoPublisher.TextChanged += new System.EventHandler(this.TxtBoxFMNoYearTextChanged);
			// 
			// label33
			// 
			this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label33.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label33.Location = new System.Drawing.Point(4, 10);
			this.label33.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(248, 22);
			this.label33.TabIndex = 0;
			this.label33.Text = "Издательства Нет:";
			this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel31
			// 
			this.panel31.Controls.Add(this.txtBoxFMNoYear);
			this.panel31.Controls.Add(this.label32);
			this.panel31.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel31.Location = new System.Drawing.Point(4, 21);
			this.panel31.Margin = new System.Windows.Forms.Padding(4);
			this.panel31.Name = "panel31";
			this.panel31.Size = new System.Drawing.Size(1155, 39);
			this.panel31.TabIndex = 13;
			// 
			// txtBoxFMNoYear
			// 
			this.txtBoxFMNoYear.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoYear.Location = new System.Drawing.Point(257, 7);
			this.txtBoxFMNoYear.Margin = new System.Windows.Forms.Padding(4);
			this.txtBoxFMNoYear.Name = "txtBoxFMNoYear";
			this.txtBoxFMNoYear.Size = new System.Drawing.Size(874, 24);
			this.txtBoxFMNoYear.TabIndex = 1;
			this.txtBoxFMNoYear.TextChanged += new System.EventHandler(this.TxtBoxFMNoYearTextChanged);
			// 
			// label32
			// 
			this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label32.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label32.Location = new System.Drawing.Point(4, 10);
			this.label32.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(248, 22);
			this.label32.TabIndex = 0;
			this.label32.Text = "Года издания Книги Нет:";
			this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tpFB2Info
			// 
			this.tpFB2Info.Controls.Add(this.gBoxFMFB2INoTags);
			this.tpFB2Info.Location = new System.Drawing.Point(26, 4);
			this.tpFB2Info.Margin = new System.Windows.Forms.Padding(4);
			this.tpFB2Info.Name = "tpFB2Info";
			this.tpFB2Info.Padding = new System.Windows.Forms.Padding(4);
			this.tpFB2Info.Size = new System.Drawing.Size(1171, 645);
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
			this.gBoxFMFB2INoTags.Location = new System.Drawing.Point(4, 4);
			this.gBoxFMFB2INoTags.Margin = new System.Windows.Forms.Padding(4);
			this.gBoxFMFB2INoTags.Name = "gBoxFMFB2INoTags";
			this.gBoxFMFB2INoTags.Padding = new System.Windows.Forms.Padding(4);
			this.gBoxFMFB2INoTags.Size = new System.Drawing.Size(1163, 637);
			this.gBoxFMFB2INoTags.TabIndex = 1;
			this.gBoxFMFB2INoTags.TabStop = false;
			this.gBoxFMFB2INoTags.Text = " Для отсутствующих данных тэгов ";
			// 
			// panel34
			// 
			this.panel34.Controls.Add(this.txtBoxFMNoFB2NickName);
			this.panel34.Controls.Add(this.label35);
			this.panel34.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel34.Location = new System.Drawing.Point(4, 138);
			this.panel34.Margin = new System.Windows.Forms.Padding(4);
			this.panel34.Name = "panel34";
			this.panel34.Size = new System.Drawing.Size(1155, 39);
			this.panel34.TabIndex = 14;
			// 
			// txtBoxFMNoFB2NickName
			// 
			this.txtBoxFMNoFB2NickName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoFB2NickName.Location = new System.Drawing.Point(257, 7);
			this.txtBoxFMNoFB2NickName.Margin = new System.Windows.Forms.Padding(4);
			this.txtBoxFMNoFB2NickName.Name = "txtBoxFMNoFB2NickName";
			this.txtBoxFMNoFB2NickName.Size = new System.Drawing.Size(874, 24);
			this.txtBoxFMNoFB2NickName.TabIndex = 1;
			this.txtBoxFMNoFB2NickName.TextChanged += new System.EventHandler(this.TxtBoxFMNoFB2FirstNameTextChanged);
			// 
			// label35
			// 
			this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label35.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label35.Location = new System.Drawing.Point(4, 10);
			this.label35.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(248, 22);
			this.label35.TabIndex = 0;
			this.label35.Text = "Ника fb2-создателя Нет:";
			this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel35
			// 
			this.panel35.Controls.Add(this.txtBoxFMNoFB2LastName);
			this.panel35.Controls.Add(this.label36);
			this.panel35.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel35.Location = new System.Drawing.Point(4, 99);
			this.panel35.Margin = new System.Windows.Forms.Padding(4);
			this.panel35.Name = "panel35";
			this.panel35.Size = new System.Drawing.Size(1155, 39);
			this.panel35.TabIndex = 13;
			// 
			// txtBoxFMNoFB2LastName
			// 
			this.txtBoxFMNoFB2LastName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoFB2LastName.Location = new System.Drawing.Point(257, 7);
			this.txtBoxFMNoFB2LastName.Margin = new System.Windows.Forms.Padding(4);
			this.txtBoxFMNoFB2LastName.Name = "txtBoxFMNoFB2LastName";
			this.txtBoxFMNoFB2LastName.Size = new System.Drawing.Size(874, 24);
			this.txtBoxFMNoFB2LastName.TabIndex = 1;
			this.txtBoxFMNoFB2LastName.TextChanged += new System.EventHandler(this.TxtBoxFMNoFB2FirstNameTextChanged);
			// 
			// label36
			// 
			this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label36.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label36.Location = new System.Drawing.Point(4, 10);
			this.label36.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(248, 22);
			this.label36.TabIndex = 0;
			this.label36.Text = "Фамилия fb2-создателя Нет:";
			this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel36
			// 
			this.panel36.Controls.Add(this.txtBoxFMNoFB2MiddleName);
			this.panel36.Controls.Add(this.label37);
			this.panel36.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel36.Location = new System.Drawing.Point(4, 60);
			this.panel36.Margin = new System.Windows.Forms.Padding(4);
			this.panel36.Name = "panel36";
			this.panel36.Size = new System.Drawing.Size(1155, 39);
			this.panel36.TabIndex = 12;
			// 
			// txtBoxFMNoFB2MiddleName
			// 
			this.txtBoxFMNoFB2MiddleName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoFB2MiddleName.Location = new System.Drawing.Point(257, 7);
			this.txtBoxFMNoFB2MiddleName.Margin = new System.Windows.Forms.Padding(4);
			this.txtBoxFMNoFB2MiddleName.Name = "txtBoxFMNoFB2MiddleName";
			this.txtBoxFMNoFB2MiddleName.Size = new System.Drawing.Size(874, 24);
			this.txtBoxFMNoFB2MiddleName.TabIndex = 1;
			this.txtBoxFMNoFB2MiddleName.TextChanged += new System.EventHandler(this.TxtBoxFMNoFB2FirstNameTextChanged);
			// 
			// label37
			// 
			this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label37.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label37.Location = new System.Drawing.Point(4, 10);
			this.label37.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label37.Name = "label37";
			this.label37.Size = new System.Drawing.Size(248, 22);
			this.label37.TabIndex = 0;
			this.label37.Text = "Отчества fb2-создателя Нет:";
			this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel37
			// 
			this.panel37.Controls.Add(this.txtBoxFMNoFB2FirstName);
			this.panel37.Controls.Add(this.label38);
			this.panel37.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel37.Location = new System.Drawing.Point(4, 21);
			this.panel37.Margin = new System.Windows.Forms.Padding(4);
			this.panel37.Name = "panel37";
			this.panel37.Size = new System.Drawing.Size(1155, 39);
			this.panel37.TabIndex = 11;
			// 
			// txtBoxFMNoFB2FirstName
			// 
			this.txtBoxFMNoFB2FirstName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFMNoFB2FirstName.Location = new System.Drawing.Point(257, 7);
			this.txtBoxFMNoFB2FirstName.Margin = new System.Windows.Forms.Padding(4);
			this.txtBoxFMNoFB2FirstName.Name = "txtBoxFMNoFB2FirstName";
			this.txtBoxFMNoFB2FirstName.Size = new System.Drawing.Size(874, 24);
			this.txtBoxFMNoFB2FirstName.TabIndex = 1;
			this.txtBoxFMNoFB2FirstName.TextChanged += new System.EventHandler(this.TxtBoxFMNoFB2FirstNameTextChanged);
			// 
			// label38
			// 
			this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label38.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label38.Location = new System.Drawing.Point(4, 10);
			this.label38.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label38.Name = "label38";
			this.label38.Size = new System.Drawing.Size(248, 22);
			this.label38.TabIndex = 0;
			this.label38.Text = "Имени fb2-создателя Нет:";
			this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.btnDefRestore);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.panel1.Location = new System.Drawing.Point(4, 686);
			this.panel1.Margin = new System.Windows.Forms.Padding(4);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1209, 54);
			this.panel1.TabIndex = 0;
			// 
			// btnDefRestore
			// 
			this.btnDefRestore.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.btnDefRestore.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
			this.btnDefRestore.ForeColor = System.Drawing.Color.Navy;
			this.btnDefRestore.Location = new System.Drawing.Point(251, 9);
			this.btnDefRestore.Margin = new System.Windows.Forms.Padding(4);
			this.btnDefRestore.Name = "btnDefRestore";
			this.btnDefRestore.Size = new System.Drawing.Size(729, 44);
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
			this.panelProgress.Dock = System.Windows.Forms.DockStyle.Right;
			this.panelProgress.Location = new System.Drawing.Point(1234, 0);
			this.panelProgress.Margin = new System.Windows.Forms.Padding(4);
			this.panelProgress.Name = "panelProgress";
			this.panelProgress.Size = new System.Drawing.Size(441, 778);
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
			listViewItem12});
			this.lvFilesCount.Location = new System.Drawing.Point(0, 0);
			this.lvFilesCount.Margin = new System.Windows.Forms.Padding(4);
			this.lvFilesCount.Name = "lvFilesCount";
			this.lvFilesCount.Size = new System.Drawing.Size(441, 778);
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
			// SFBTpFileManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.panelProgress);
			this.Controls.Add(this.tcSort);
			this.Controls.Add(this.ssProgress);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "SFBTpFileManager";
			this.Size = new System.Drawing.Size(1675, 800);
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
			this.gBoxOptions.ResumeLayout(false);
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
			this.panel1.ResumeLayout(false);
			this.panelProgress.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		private System.Windows.Forms.GroupBox gBoxOptions;
		private System.Windows.Forms.ComboBox comboBoxMaxSequenceLenght;
		private System.Windows.Forms.Label lblMaxSequenceLenght;
		private System.Windows.Forms.ComboBox comboBoxMaxBTLenght;
		private System.Windows.Forms.Label lblMaxBTLenght;
		private System.Windows.Forms.ComboBox comboBoxMaxFileForProgress;
		private System.Windows.Forms.Label lblMaxFileForProgress;
		private System.Windows.Forms.ColumnHeader colHeaderIFormat;
		private System.Windows.Forms.ColumnHeader colHeaderVersion;
		private System.Windows.Forms.ColumnHeader colHeaderEncoding;
		private System.Windows.Forms.ColumnHeader colHeaderIValidate;
		private System.Windows.Forms.ColumnHeader colHeaderFileSize;
		private System.Windows.Forms.ColumnHeader colHeaderFileCreate;
		private System.Windows.Forms.ColumnHeader colHeaderFileModify;
		private System.Windows.Forms.ToolStripSeparator tsmi1;
		private System.Windows.Forms.ToolStripMenuItem tsmiViewInReader;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
		private System.Windows.Forms.Label labelTarget;
		private System.Windows.Forms.Label labelTargetPath;
		private System.Windows.Forms.TabPage tcTemplates;
		private System.Windows.Forms.Panel panelProgress;
		private System.Windows.Forms.OpenFileDialog sfdLoadList;
		private System.Windows.Forms.Button buttonSSortRenew;
		private System.Windows.Forms.Button buttonFullSortRenew;
		private System.Windows.Forms.Button btnDefRestore;
		private System.Windows.Forms.Panel panel13;
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
		private System.Windows.Forms.ColumnHeader colHeaderID;
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
		private System.Windows.Forms.ListView listViewFB2Files;
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
		#endregion
		
		#region Закрытые данные класса
		private string						m_sMessTitle	= string.Empty;
		private readonly StatusView			m_sv			= new StatusView();
		private readonly MiscListView		m_mscLV			= new MiscListView(); // класс по работе с ListView
		private readonly SharpZipLibWorker	m_sharpZipLib	= new SharpZipLibWorker();
		private readonly FullNameTemplates	m_fnt			= new FullNameTemplates();
		private readonly string				m_TempDir		= Settings.Settings.TempDir;
		
		private bool m_isSettingsLoaded			= false; // Только при true все изменения настроек сохраняются в файл.
		private bool m_ViewMessageForLongTime	= true; // показывать предупреждение о том, что вкл. опции отображения метаданных потребует много времени...
		private SortingOptions m_sortOptions	= null; // индивидуальные настройки обоих Сортировщиков, взависимости от режима (непрерывная сортировка или возобновление сортировки)
		private readonly MiscListView.FilemanagerColumnSorter m_lvwColumnSorter = new MiscListView.FilemanagerColumnSorter();
		#endregion
		
		public SFBTpFileManager()
		{
			InitializeComponent();
			Init();
			
			comboBoxMaxFileForProgress.SelectedIndex = 10;
			comboBoxMaxBTLenght.SelectedIndex = 8;
			comboBoxMaxSequenceLenght.SelectedIndex = 8;

			/* Настройки Менеджера Файлов по-умолчанию*/
			// основные настройки
			DefFMGeneral();
			// название папки шаблонного тэга без данных
			DefFMDirNameForTagNotData();
			// название для данных Издательства из Description когда нет данных тэга
			DefFMDirNameForPublisherTagNotData();
			// название для данных fb2-файла из Description когда нет данных тэга
			DefFMDirNameForFB2TagNotData();
			
			/* читаем сохраненные пути к папкам и шаблон Менеджера Файлов, если они есть */
			readSettingsFromXML();
			m_isSettingsLoaded = true;
			lvFilesCount.AutoResizeColumns( ColumnHeaderAutoResizeStyle.HeaderSize );
			rtboxTemplatesList.Clear();

			string sTDPath = Settings.FileManagerSettings.DefFMDescTemplatePath;
			if( File.Exists( sTDPath ) )
				rtboxTemplatesList.LoadFile( sTDPath );
			else
				rtboxTemplatesList.Text = "Не найден файл описания Шаблонов подстановки: \"" + sTDPath + "\"";
		}
		
		#region Настройки по-умолчанию для вкладок
		private void DefFMGeneral() {
			// основные для Менеджера Файлов
			chBoxTranslit.Checked = Settings.FileManagerSettings.DefTranslit;
			chBoxStrict.Checked = Settings.FileManagerSettings.DefStrict;
			cboxSpace.SelectedIndex = Settings.FileManagerSettings.DefSpace;
			cboxFileExist.SelectedIndex = Settings.FileManagerSettings.DefFileExist;
			rbtnAsIs.Checked = Settings.FileManagerSettings.DefRegisterAsIs;
			rbtnAsSentence.Checked = Settings.FileManagerSettings.DefRegisterAsSentence;
			rbtnLower.Checked = Settings.FileManagerSettings.DefRegisterLower;
			rbtnUpper.Checked = Settings.FileManagerSettings.DefRegisterUpper;
			rbtnGenreOne.Checked = Settings.FileManagerSettings.DefGenreOne;
			rbtnGenreAll.Checked = Settings.FileManagerSettings.DefGenreAll;
			rbtnAuthorOne.Checked = Settings.FileManagerSettings.DefAuthorOne;
			rbtnAuthorAll.Checked = Settings.FileManagerSettings.DefAuthorAll;
			rbtnGenreSchema.Checked = Settings.FileManagerSettings.DefGenreSchema;
			rbtnGenreText.Checked = Settings.FileManagerSettings.DefGenreText;
			rbtnFMAllFB2.Checked		= Settings.FileManagerSettings.DefAllFB2;
			rbtnFMOnlyValidFB2.Checked	= Settings.FileManagerSettings.DefOnlyValidFB2;
		}
		private void DefFMDirNameForTagNotData() {
			// название папки шаблонного тэга без данных
			txtBoxFMNoGenreGroup.Text	= Settings.FileManagerSettings.DefNoGenreGroup;
			txtBoxFMNoGenre.Text		= Settings.FileManagerSettings.DefNoGenre;
			txtBoxFMNoLang.Text			= Settings.FileManagerSettings.DefNoLang;
			txtBoxFMNoFirstName.Text	= Settings.FileManagerSettings.DefNoFirstName;
			txtBoxFMNoMiddleName.Text	= Settings.FileManagerSettings.DefNoMiddleName;
			txtBoxFMNoLastName.Text		= Settings.FileManagerSettings.DefNoLastName;
			txtBoxFMNoNickName.Text		= Settings.FileManagerSettings.DefNoNickName;
			txtBoxFMNoBookTitle.Text	= Settings.FileManagerSettings.DefNoBookTitle;
			txtBoxFMNoSequence.Text		= Settings.FileManagerSettings.DefNoSequence;
			txtBoxFMNoNSequence.Text	= Settings.FileManagerSettings.DefNoNSequence;
			txtBoxFMNoDateText.Text		= Settings.FileManagerSettings.DefNoDateText;
			txtBoxFMNoDateValue.Text	= Settings.FileManagerSettings.DefNoDateValue;
		}
		private void DefFMDirNameForPublisherTagNotData() {
			// название для данных Издательства из Description когда нет данных тэга
			txtBoxFMNoYear.Text			= Settings.FileManagerSettings.DefNoYear;
			txtBoxFMNoPublisher.Text	= Settings.FileManagerSettings.DefNoPublisher;
			txtBoxFMNoCity.Text			= Settings.FileManagerSettings.DefNoCity;
		}
		private void DefFMDirNameForFB2TagNotData() {
			// название для данных fb2-файла из Description когда нет данных тэга
			txtBoxFMNoFB2FirstName.Text		= Settings.FileManagerSettings.DefNoFB2FirstName;
			txtBoxFMNoFB2MiddleName.Text	= Settings.FileManagerSettings.DefNoFB2MiddleName;
			txtBoxFMNoFB2LastName.Text		= Settings.FileManagerSettings.DefNoFB2LastName;
			txtBoxFMNoFB2NickName.Text		= Settings.FileManagerSettings.DefNoFB2NickName;
		}
		#endregion
		
		#region Закрытые вспомогательные методы класса
		private void ConnectListsEventHandlers( bool bConnect ) {
			if( !bConnect ) {
				// отключаем обработчики событий для Списка (убираем "тормоза")
				this.listViewFB2Files.DoubleClick -= new System.EventHandler(this.ListViewSourceDoubleClick);
				this.listViewFB2Files.ItemChecked -= new System.Windows.Forms.ItemCheckedEventHandler(this.ListViewSourceItemChecked);
				this.listViewFB2Files.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.ListViewSourceItemCheck);
				this.listViewFB2Files.KeyPress -= new System.Windows.Forms.KeyPressEventHandler(this.ListViewSourceKeyPress);
				this.listViewFB2Files.ColumnClick -= new System.Windows.Forms.ColumnClickEventHandler(this.ListViewSourceColumnClick);
			} else {
				// подключаем обработчики событий для Списка
				this.listViewFB2Files.DoubleClick += new System.EventHandler(this.ListViewSourceDoubleClick);
				this.listViewFB2Files.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.ListViewSourceItemChecked);
				this.listViewFB2Files.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ListViewSourceItemCheck);
				this.listViewFB2Files.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ListViewSourceKeyPress);
				this.listViewFB2Files.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.ListViewSourceColumnClick);
			}
		}

		// инициализация контролов и переменных
		private void Init() {
			for( int i = 0; i != lvFilesCount.Items.Count; ++i )
				lvFilesCount.Items[i].SubItems[1].Text	= "0";

			// очистка временной папки
			filesWorker.RemoveDir( m_TempDir );
			m_sv.Clear();
		}
		
		// Полная Сортировка: проверка на корректность данных папок источника и приемника файлов
		private bool IsSourceDirDataCorrect(  string SourseDir, string TargetDir )
		{
			// проверки на корректность папок источника и приемника
			if( SourseDir.Length == 0 ) {
				MessageBox.Show( "Выберите папку для сканирования!", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			DirectoryInfo diFolder = new DirectoryInfo( SourseDir );
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка не найдена: " + SourseDir, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка папки-приемника и создание ее, если нужно
			if( !filesWorker.CreateDirIfNeed( TargetDir, m_sMessTitle ) )
				return false;

			return true;
		}
		
		// Селективная Сортировка: проверка на корректность данных папок источника и приемника файлов
		private bool IsFoldersDataCorrect( string SourseDir, string TargetDir )
		{
			// проверки на корректность папок источника и приемника
			if( SourseDir.Length == 0 ) {
				MessageBox.Show( "Выберите папку для сканирования!", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			DirectoryInfo diFolder = new DirectoryInfo( SourseDir );
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка не найдена: " + SourseDir, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			if( TargetDir.Length == 0 ) {
				MessageBox.Show( "Не указана папка-приемник файлов!\nРабота прекращена.", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			if( SourseDir == TargetDir ) {
				MessageBox.Show( "Папка-приемник файлов совпадает с папкой сканирования!\nРабота прекращена.", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка папки-приемника и создание ее, если нужно
			if( !filesWorker.CreateDirIfNeed( TargetDir, m_sMessTitle ) )
				return false;

			return true;
		}
		
		// сохранение настроек в xml-файл
		private void saveSettingsToXml() {
			// защита от "затирания" настроек в файле, когда в некоторые контролы данные еще не загрузились
			if( m_isSettingsLoaded ) {
				XDocument doc = new XDocument(
					new XDeclaration("1.0", "utf-8", "yes"),
					new XElement("Settings", new XAttribute("type", "sort_settings"),
					             new XComment("xml файл настроек Сортировщика"),
					             new XComment("Полная Сортировка"),
					             new XElement("FullSorting",
					                          new XComment("Папка исходных fb2-файлов"),
					                          new XElement("SourceDir", textBoxAddress.Text.Trim()),
					                          new XComment("Папка-приемник fb2-файлов"),
					                          new XElement("TargetDir", labelTargetPath.Text),
					                          new XComment("Шаблон подстановки"),
					                          new XElement("Template", txtBoxTemplatesFromLine.Text.Trim()),
					                          new XComment("Настройки Полной Сортировки"),
					                          new XElement("Options",
					                                       new XComment("Обрабатывать подкаталоги"),
					                                       new XElement("ScanSubDirs", chBoxScanSubDir.Checked),
					                                       new XComment("Архивировать в zip"),
					                                       new XElement("ToZip", chBoxFSToZip.Checked),
					                                       new XComment("Сохранять оригиналы"),
					                                       new XElement("NotDelFB2Files", chBoxFSNotDelFB2Files.Checked),
					                                       new XComment("Показывать описание книг в Проводнике"),
					                                       new XElement("BooksTagsView", checkBoxTagsView.Checked),
					                                       new XComment("Показывать сообщение о том, что требуется много времени на генерацию метаданных"),
					                                       new XElement("ViewMessageForLongTime", m_ViewMessageForLongTime),
					                                       new XElement("MaxFileForProgressIndex", comboBoxMaxFileForProgress.SelectedIndex),
					                                       new XComment("Максимальная длина имени книги и/или серии"),
					                                       new XElement("MaxBTLenght", comboBoxMaxBTLenght.SelectedIndex),
					                                       new XElement("MaxSequenceLenght", comboBoxMaxSequenceLenght.SelectedIndex)
					                                      )
					                         ),
					             new XComment("Избранная Сортировка"),
					             new XElement("SelectedSorting",
					                          new XComment("Папка исходных fb2-файлов"),
					                          new XElement("SourceDir", tboxSSSourceDir.Text.Trim()),
					                          new XComment("Папка-приемник fb2-файлов"),
					                          new XElement("TargetDir", tboxSSToDir.Text.Trim()),
					                          new XComment("Шаблон подстановки"),
					                          new XElement("Template", txtBoxSSTemplatesFromLine.Text.Trim()),
					                          new XComment("Настройки Избранной Сортировки"),
					                          new XElement("Options",
					                                       new XComment("Обрабатывать подкаталоги"),
					                                       new XElement("ScanSubDirs", chBoxSSScanSubDir.Checked),
					                                       new XComment("Архивировать в zip"),
					                                       new XElement("ToZip", chBoxSSToZip.Checked),
					                                       new XComment("Сохранять оригиналы"),
					                                       new XElement("NotDelFB2Files", chBoxSSNotDelFB2Files.Checked)
					                                      )
					                         ),
					             new XComment("Общие настройки для обоих режимов Сортировки"),
					             new XElement("CommonOptions",
					                          new XComment("Основные настройки"),
					                          new XElement("General",
					                                       new XComment("Регистр имени файла"),
					                                       new XElement("Register",
					                                                    new XAttribute("AsIs", rbtnAsIs.Checked),
					                                                    new XAttribute("Lower", rbtnLower.Checked),
					                                                    new XAttribute("Upper", rbtnUpper.Checked),
					                                                    new XAttribute("AsSentence", rbtnAsSentence.Checked)
					                                                   ),
					                                       new XComment("Транслитерация имен файлов"),
					                                       new XElement("Translit", chBoxTranslit.Checked),
					                                       new XComment("'Строгие' имена файлов: алфавитно-цифровые символы, а так же [](){}-_"),
					                                       new XElement("Strict", chBoxStrict.Checked),
					                                       new XComment("Обработка пробелов"),
					                                       new XElement("Space",
					                                                    new XAttribute("index", cboxSpace.SelectedIndex),
					                                                    new XAttribute("Name", cboxSpace.Text)
					                                                   ),
					                                       new XComment("Одинаковые файлы"),
					                                       new XElement("FileExistMode",
					                                                    new XAttribute("index", cboxFileExist.SelectedIndex),
					                                                    new XAttribute("Name", cboxFileExist.Text)
					                                                   ),
					                                       new XComment("Сортировка файлов"),
					                                       new XElement("SortType",
					                                                    new XAttribute("AllFB2", rbtnFMAllFB2.Checked),
					                                                    new XAttribute("OnlyValidFB2", rbtnFMOnlyValidFB2.Checked)
					                                                   ),
					                                       new XComment("Раскладка файлов по папкам"),
					                                       new XElement("FilesToDirs",
					                                                    new XComment("По Авторам"),
					                                                    new XElement("AuthorsToDirs",
					                                                                 new XAttribute("AuthorOne", rbtnAuthorOne.Checked),
					                                                                 new XAttribute("AuthorAll", rbtnAuthorAll.Checked)
					                                                                ),
					                                                    new XComment("По Жанрам"),
					                                                    new XElement("GenresToDirs",
					                                                                 new XAttribute("GenreOne", rbtnGenreOne.Checked),
					                                                                 new XAttribute("GenreAll", rbtnGenreAll.Checked)
					                                                                ),
					                                                    new XComment("Вид папки-Жанра"),
					                                                    new XElement("GenresType",
					                                                                 new XAttribute("GenreSchema", rbtnGenreSchema.Checked),
					                                                                 new XAttribute("GenreText", rbtnGenreText.Checked)
					                                                                )
					                                                   )
					                                      ),
					                          new XComment("Папки шаблонного тэга без данных"),
					                          new XElement("NoTags",
					                                       new XComment("Описание Книги"),
					                                       new XElement("BookInfo",
					                                                    new XElement("NoGenreGroup", txtBoxFMNoGenreGroup.Text.Trim()),
					                                                    new XElement("NoGenre", txtBoxFMNoGenre.Text.Trim()),
					                                                    new XElement("NoLang", txtBoxFMNoLang.Text.Trim()),
					                                                    new XElement("NoFirstName", txtBoxFMNoFirstName.Text.Trim()),
					                                                    new XElement("NoMiddleName", txtBoxFMNoMiddleName.Text.Trim()),
					                                                    new XElement("NoLastName", txtBoxFMNoLastName.Text.Trim()),
					                                                    new XElement("NoNickName", txtBoxFMNoNickName.Text.Trim()),
					                                                    new XElement("NoBookTitle", txtBoxFMNoBookTitle.Text.Trim()),
					                                                    new XElement("NoSequence", txtBoxFMNoSequence.Text.Trim()),
					                                                    new XElement("NoNSequence", txtBoxFMNoNSequence.Text.Trim()),
					                                                    new XElement("NoDateText", txtBoxFMNoDateText.Text.Trim()),
					                                                    new XElement("NoDateValue", txtBoxFMNoDateValue.Text.Trim())
					                                                   ),
					                                       new XComment("Издательство"),
					                                       new XElement("PublishInfo",
					                                                    new XElement("NoPublisher", txtBoxFMNoPublisher.Text.Trim()),
					                                                    new XElement("NoYear", txtBoxFMNoYear.Text.Trim()),
					                                                    new XElement("NoCity", txtBoxFMNoCity.Text.Trim())
					                                                   ),
					                                       new XComment("Данные о создателе fb2 файла"),
					                                       new XElement("FB2Info",
					                                                    new XElement("NoFB2FirstName", txtBoxFMNoFB2FirstName.Text.Trim()),
					                                                    new XElement("NoFB2MiddleName", txtBoxFMNoFB2MiddleName.Text.Trim()),
					                                                    new XElement("NoFB2LastName", txtBoxFMNoFB2LastName.Text.Trim()),
					                                                    new XElement("NoFB2NickName", txtBoxFMNoFB2NickName.Text.Trim())
					                                                   )
					                                      )
					                         )
					            )
				);
				doc.Save( Settings.FileManagerSettings.FileManagerSettingsPath );
			}
		}
		
		// загрузка настроек из xml-файла
		private void readSettingsFromXML() {
			if( File.Exists( Settings.FileManagerSettings.FileManagerSettingsPath ) ) {
				XElement xmlTree = XElement.Load( Settings.FileManagerSettings.FileManagerSettingsPath );
				/* FullSorting */
				if( xmlTree.Element("FullSorting") != null ) {
					XElement xmlFullSorting = xmlTree.Element("FullSorting");
					// Папка исходных fb2-файлов
					if( xmlFullSorting.Element("SourceDir") != null )
						textBoxAddress.Text = xmlFullSorting.Element("SourceDir").Value;
					// Папка приемник fb2-файлов
					if( xmlFullSorting.Element("TargetDir") != null )
						labelTargetPath.Text = xmlFullSorting.Element("TargetDir").Value;
					// Шаблон подстановки
					if( xmlFullSorting.Element("Template") != null )
						txtBoxTemplatesFromLine.Text = xmlFullSorting.Element("Template").Value;

					// Настройки Полной Сортировки:
					if( xmlFullSorting.Element("Options") != null ) {
						XElement xmlOptions = xmlFullSorting.Element("Options");
						// Обрабатывать подкаталоги
						if( xmlOptions.Element("ScanSubDirs") != null )
							chBoxScanSubDir.Checked	= Convert.ToBoolean( xmlOptions.Element("ScanSubDirs").Value );
						// Архивировать в zip
						if( xmlOptions.Element("ToZip") != null )
							chBoxFSToZip.Checked = Convert.ToBoolean( xmlOptions.Element("ToZip").Value );
						// Сохранять оригиналы
						if( xmlOptions.Element("NotDelFB2Files") != null )
							chBoxFSNotDelFB2Files.Checked = Convert.ToBoolean( xmlOptions.Element("NotDelFB2Files").Value );
						// Показывать описание книг в Проводнике
						if( xmlOptions.Element("BooksTagsView") != null ) {
							this.checkBoxTagsView.Click -= new System.EventHandler(this.CheckBoxTagsViewClick);
							checkBoxTagsView.Checked = Convert.ToBoolean( xmlOptions.Element("BooksTagsView").Value );
							this.checkBoxTagsView.Click += new System.EventHandler(this.CheckBoxTagsViewClick);
						}
						// Показывать сообщение о том, что требуется много времени на генерацию метаданных"
						if( xmlOptions.Element("ViewMessageForLongTime") != null )
							m_ViewMessageForLongTime = Convert.ToBoolean( xmlOptions.Element("ViewMessageForLongTime").Value );
						
						// Если число файлов в сканируемом каталоге превышает заданное, то появляется панель прогресса
						if( xmlOptions.Element("MaxFileForProgressIndex") != null )
							comboBoxMaxFileForProgress.SelectedIndex = Convert.ToInt16( xmlOptions.Element("MaxFileForProgressIndex").Value );
						
						// Максимальная длина имени книги и/или серии
						if( xmlOptions.Element("MaxBTLenght") != null )
							comboBoxMaxBTLenght.SelectedIndex = Convert.ToInt16( xmlOptions.Element("MaxBTLenght").Value );
						if( xmlOptions.Element("MaxSequenceLenght") != null )
							comboBoxMaxSequenceLenght.SelectedIndex = Convert.ToInt16( xmlOptions.Element("MaxSequenceLenght").Value );
					}
				}
				
				/* SelectedSorting */
				if( xmlTree.Element("SelectedSorting") != null ) {
					XElement xmlSelectedSorting = xmlTree.Element("SelectedSorting");
					// Папка исходных fb2-файлов
					if( xmlSelectedSorting.Element("SourceDir") != null )
						tboxSSSourceDir.Text = xmlSelectedSorting.Element("SourceDir").Value;
					// Папка-приемник fb2-файлов
					if( xmlSelectedSorting.Element("TargetDir") != null )
						tboxSSToDir.Text = xmlSelectedSorting.Element("TargetDir").Value;
					// Шаблон подстановки
					if( xmlSelectedSorting.Element("Template") != null )
						txtBoxSSTemplatesFromLine.Text = xmlSelectedSorting.Element("Template").Value;
					// Настройки Избранной Сортировки:
					if( xmlSelectedSorting.Element("Options") != null ) {
						XElement xmlOptions = xmlSelectedSorting.Element("Options");
						// Обрабатывать подкаталоги
						if( xmlOptions.Element("ScanSubDirs") != null )
							chBoxSSScanSubDir.Checked = Convert.ToBoolean( xmlOptions.Element("ScanSubDirs").Value );
						// Архивировать в zip
						if( xmlOptions.Element("ToZip") != null )
							chBoxSSToZip.Checked = Convert.ToBoolean( xmlOptions.Element("ToZip").Value );
						// Сохранять оригиналы
						if( xmlOptions.Element("NotDelFB2Files") != null )
							chBoxSSNotDelFB2Files.Checked = Convert.ToBoolean( xmlOptions.Element("NotDelFB2Files").Value );
					}
				}
				
				/* Общие настройки для обоих режимов Сортировки */
				if( xmlTree.Element("CommonOptions") != null ) {
					XElement xmlCommonOptions = xmlTree.Element("CommonOptions");
					if( xmlCommonOptions.Element("General") != null ) {
						// Основные настройки
						XElement xmlGeneral = xmlCommonOptions.Element("General");
						// Регистр имени файла
						if( xmlGeneral.Element("Register") != null ) {
							XElement xmlRegister = xmlGeneral.Element("Register");
							if( xmlRegister.Attribute("AsIs") != null )
								rbtnAsIs.Checked = Convert.ToBoolean( xmlRegister.Attribute("AsIs").Value );
							if( xmlRegister.Attribute("Lower") != null )
								rbtnLower.Checked = Convert.ToBoolean( xmlRegister.Attribute("Lower").Value );
							if( xmlRegister.Attribute("Upper") != null )
								rbtnUpper.Checked = Convert.ToBoolean( xmlRegister.Attribute("Upper").Value );
							if( xmlRegister.Attribute("AsSentence") != null )
								rbtnAsSentence.Checked = Convert.ToBoolean( xmlRegister.Attribute("AsSentence").Value );
						}
						// Транслитерация имен файлов
						if( xmlGeneral.Element("Translit") != null )
							chBoxTranslit.Checked = Convert.ToBoolean( xmlGeneral.Element("Translit").Value );
						// 'Строгие' имена файлов: алфавитно-цифровые символы, а так же [](){}-_
						if( xmlGeneral.Element("Strict") != null )
							chBoxStrict.Checked = Convert.ToBoolean( xmlGeneral.Element("Strict").Value );
						// Обработка пробелов
						if( xmlGeneral.Element("Space") != null ) {
							if( xmlGeneral.Element("Space").Attribute("index") != null )
								cboxSpace.SelectedIndex = Convert.ToInt16( xmlGeneral.Element("Space").Attribute("index").Value );
						}
						// Одинаковые файлы
						if( xmlGeneral.Element("FileExistMode") != null ) {
							if( xmlGeneral.Element("FileExistMode").Attribute("index") != null )
								cboxFileExist.SelectedIndex = Convert.ToInt16( xmlGeneral.Element("FileExistMode").Attribute("index").Value );
						}
						// Сортировка файлов
						if( xmlGeneral.Element("SortType") != null ) {
							XElement xmlSortType = xmlGeneral.Element("SortType");
							if( xmlSortType.Attribute("AllFB2") != null )
								rbtnFMAllFB2.Checked = Convert.ToBoolean( xmlSortType.Attribute("AllFB2").Value );
							if( xmlSortType.Attribute("OnlyValidFB2") != null )
								rbtnFMOnlyValidFB2.Checked = Convert.ToBoolean( xmlSortType.Attribute("OnlyValidFB2").Value );
						}
						// Раскладка файлов по папкам
						if( xmlGeneral.Element("FilesToDirs") != null ) {
							XElement xmlFilesToDirs = xmlGeneral.Element("FilesToDirs");
							// По Авторам
							if( xmlFilesToDirs.Element("AuthorsToDirs") != null ) {
								XElement xmlAuthorsToDirs = xmlFilesToDirs.Element("AuthorsToDirs");
								if( xmlAuthorsToDirs.Attribute("AuthorOne") != null )
									rbtnAuthorOne.Checked = Convert.ToBoolean( xmlAuthorsToDirs.Attribute("AuthorOne").Value );
								if( xmlAuthorsToDirs.Attribute("AuthorAll") != null )
									rbtnAuthorAll.Checked = Convert.ToBoolean( xmlAuthorsToDirs.Attribute("AuthorAll").Value );
							}
							// По Жанрам
							if( xmlFilesToDirs.Element("GenresToDirs") != null ) {
								XElement xmlGenresToDirs = xmlFilesToDirs.Element("GenresToDirs");
								if( xmlGenresToDirs.Attribute("GenreOne") != null )
									rbtnGenreOne.Checked = Convert.ToBoolean( xmlGenresToDirs.Attribute("GenreOne").Value );
								if( xmlGenresToDirs.Attribute("GenreAll") != null )
									rbtnGenreAll.Checked = Convert.ToBoolean( xmlGenresToDirs.Attribute("GenreAll").Value );
							}
							// Вид папки-Жанра
							if( xmlFilesToDirs.Element("GenresType") != null ) {
								XElement xmlGenresType = xmlFilesToDirs.Element("GenresType");
								if( xmlGenresType.Attribute("GenreSchema") != null )
									rbtnGenreSchema.Checked = Convert.ToBoolean( xmlGenresType.Attribute("GenreSchema").Value );
								if( xmlGenresType.Attribute("GenreText") != null )
									rbtnGenreText.Checked = Convert.ToBoolean( xmlGenresType.Attribute("GenreText").Value );
							}
						}
					}
					/* Папки шаблонного тэга без данных */
					if( xmlCommonOptions.Element("NoTags") != null ) {
						XElement xmlNoTags = xmlCommonOptions.Element("NoTags");
						// Описание Книги
						if( xmlNoTags.Element("BookInfo") != null ) {
							XElement xmlBookInfo = xmlNoTags.Element("BookInfo");
							if( xmlBookInfo.Element("NoGenreGroup") != null )
								txtBoxFMNoGenreGroup.Text = xmlBookInfo.Element("NoGenreGroup").Value;
							if( xmlBookInfo.Element("NoGenre") != null )
								txtBoxFMNoGenre.Text = xmlBookInfo.Element("NoGenre").Value;
							if( xmlBookInfo.Element("NoLang") != null )
								txtBoxFMNoLang.Text = xmlBookInfo.Element("NoLang").Value;
							if( xmlBookInfo.Element("NoFirstName") != null )
								txtBoxFMNoFirstName.Text = xmlBookInfo.Element("NoFirstName").Value;
							if( xmlBookInfo.Element("NoMiddleName") != null )
								txtBoxFMNoMiddleName.Text = xmlBookInfo.Element("NoMiddleName").Value;
							if( xmlBookInfo.Element("NoLastName") != null )
								txtBoxFMNoLastName.Text = xmlBookInfo.Element("NoLastName").Value;
							if( xmlBookInfo.Element("NoNickName") != null )
								txtBoxFMNoNickName.Text = xmlBookInfo.Element("NoNickName").Value;
							if( xmlBookInfo.Element("NoBookTitle") != null )
								txtBoxFMNoBookTitle.Text = xmlBookInfo.Element("NoBookTitle").Value;
							if( xmlBookInfo.Element("NoSequence") != null )
								txtBoxFMNoSequence.Text = xmlBookInfo.Element("NoSequence").Value;
							if( xmlBookInfo.Element("NoNSequence") != null )
								txtBoxFMNoNSequence.Text = xmlBookInfo.Element("NoNSequence").Value;
							if( xmlBookInfo.Element("NoDateText") != null )
								txtBoxFMNoDateText.Text = xmlBookInfo.Element("NoDateText").Value;
							if( xmlBookInfo.Element("NoDateValue") != null )
								txtBoxFMNoDateValue.Text = xmlBookInfo.Element("NoDateValue").Value;
						}
						// Издательство
						if( xmlNoTags.Element("PublishInfo") != null ) {
							XElement xmlPublishInfo = xmlNoTags.Element("PublishInfo");
							if( xmlPublishInfo.Element("NoPublisher") != null )
								txtBoxFMNoPublisher.Text = xmlPublishInfo.Element("NoPublisher").Value;
							if( xmlPublishInfo.Element("NoYear") != null )
								txtBoxFMNoYear.Text = xmlPublishInfo.Element("NoYear").Value;
							if( xmlPublishInfo.Element("NoCity") != null )
								txtBoxFMNoCity.Text = xmlPublishInfo.Element("NoCity").Value;
						}
						// Данные о создателе fb2 файла
						if( xmlNoTags.Element("FB2Info") != null ) {
							XElement xmlBookInfo = xmlNoTags.Element("FB2Info");
							if( xmlBookInfo.Element("NoFB2FirstName") != null )
								txtBoxFMNoFB2FirstName.Text = xmlBookInfo.Element("NoFB2FirstName").Value;
							if( xmlBookInfo.Element("NoFB2MiddleName") != null )
								txtBoxFMNoFB2MiddleName.Text = xmlBookInfo.Element("NoFB2MiddleName").Value;
							if( xmlBookInfo.Element("NoFB2LastName") != null )
								txtBoxFMNoFB2LastName.Text = xmlBookInfo.Element("NoFB2LastName").Value;
							if( xmlBookInfo.Element("NoFB2NickName") != null )
								txtBoxFMNoFB2NickName.Text = xmlBookInfo.Element("NoFB2NickName").Value;
						}
					}
				}
			}
		}

		//------------------------------------------------------------------------------------------
		
		// проверки на корректность шаблонных строк
		private bool IsLineTemplateCorrect( string sLineTemplate ) {
			// проверка "пустоту" строки с шаблонами
			if( sLineTemplate.Length == 0 ) {
				MessageBox.Show( "Строка шаблонов не может быть пустой!\nРабота прекращена.", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на наличие недопустимого условного шаблона [*GROUP*]
			if( sLineTemplate.IndexOf("[*GROUP*]") != -1 ) {
				MessageBox.Show( "Шаблон для Группы Жанров *GROUP* не миожет буть условным [*GROUP*]!\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на наличие недопустимого условного шаблона [*FILENAME*]
			if( sLineTemplate.IndexOf("[*FILENAME*]") != -1 ) {
				MessageBox.Show( "Шаблон для имени файла *FILENAME* не миожет буть условным [*FILENAME*]!\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на наличие недопустимого условного шаблона [*COUNTER*]
			if( sLineTemplate.IndexOf("[*COUNTER*]") != -1 ) {
				MessageBox.Show( "Шаблон для счетчика *COUNTER* не миожет буть условным [*COUNTER*]!\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на корректность строки с шаблонами
			if( !TemplatesVerify.IsLineTemplatesCorrect( sLineTemplate ) ) {
				MessageBox.Show( "Строка содержит или недопустимые шаблоны,\nили недопустимые символы */|?<>\"&\\t\\r\\n между шаблонами!\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на четность * в строке с шаблонами
			if( !TemplatesVerify.IsEvenElements( sLineTemplate, '*' ) ) {
				MessageBox.Show( "Строка с шаблонами подстановки содержит нечетное число *!\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка, не стоит ли ] перед [
			if( sLineTemplate.IndexOf('[')!=-1 && sLineTemplate.IndexOf(']') != -1 ) {
				if( sLineTemplate.IndexOf('[') > sLineTemplate.IndexOf(']') ) {
					MessageBox.Show( "В строке с шаблонами закрывающая скобка ] не может стоять перед открывающей [ !\nРабота прекращена.",
					                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return false;
				}
			}
			// проверка на соответствие [ ] в строке с шаблонами
			if( !TemplatesVerify.IsBracketsCorrect( sLineTemplate, '[', ']' ) ) {
				MessageBox.Show( "В строке с шаблонами переименования нет соответствия между открывающим и закрывающими скобками [ ]!\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на соответствие ( ) в строке с шаблонами
			if( !TemplatesVerify.IsBracketsCorrect( sLineTemplate, '(', ')' ) ) {
				MessageBox.Show( "В строке с шаблонами нет соответствия между открывающим и закрывающими скобками ( )!\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на \ в начале строки с шаблонами
			if( sLineTemplate[0]=='\\' ) {
				MessageBox.Show( "Строка с шаблонами не может начинаться с '\\'!\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на \ в конце строки с шаблонами
			if( sLineTemplate[sLineTemplate.Length-1]=='\\' ) {
				MessageBox.Show( "Строка с шаблонами не может заканчиваться на '\\' !\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка условных шаблонов на наличие в них вспом. символов без самих шаблонов
			if( !TemplatesVerify.IsConditionalPatternCorrect( sLineTemplate ) ) {
				MessageBox.Show( "Условные шаблоны [] в строке с шаблонами не могут содержать вспомогательных символов БЕЗ самих шаблонов!\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на множественность символа папки \ в строке с шаблонами
			if( sLineTemplate.IndexOf( "\\\\" )!=-1 ) {
				MessageBox.Show( "Строка с шаблонами не может содержать несколько идущих подряд символов папки '\\' !\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			return true;
		}
		
		private void SetTemplateInInputControl( TextBox textBox, string Template ) {
			int CursorPosition = textBox.SelectionStart;
			int NewPosition = CursorPosition + Template.Length;
			string TextBeforeCursor = textBox.Text.Substring(0, CursorPosition);
			string TextAfterCursor = textBox.Text.Substring(CursorPosition, textBox.TextLength-CursorPosition);
			textBox.Text = TextBeforeCursor + Template + TextAfterCursor;
			textBox.Focus();
			textBox.Select(NewPosition, 0);
		}
		
		// ========================================================================================================================
		// 													Для Полной Сортировки
		// ========================================================================================================================
		// заполнение списка данными указанной папки
		private void FullSortingGenerateSourceList( string dirPath ) {
			// спиок жанров для Полной Сортировки для режима отображения метаданных для файлов Проводника
			FB2UnionGenres fb2Genres = new FB2UnionGenres();
			if( checkBoxTagsView.Checked ) {
				// отображение метаданных книг
				DirectoryInfo dirInfo = new DirectoryInfo( dirPath );
				if( ( dirInfo.GetDirectories().Length + dirInfo.GetFiles().Length ) > Convert.ToInt16( comboBoxMaxFileForProgress.Text ) ) {
					// запуск формы прогресса отображения метаданных книг
					Cursor.Current = Cursors.WaitCursor;
					listViewFB2Files.BeginUpdate();
					FB2TagsViewForm fb2TagsViewForm = new FB2TagsViewForm(
						true, checkBoxTagsView.Checked, listViewFB2Files, dirPath
					);
					fb2TagsViewForm.ShowDialog();
					EndWorkMode EndWorkMode = fb2TagsViewForm.EndMode;
					fb2TagsViewForm.Dispose();
					if( EndWorkMode.EndMode != EndWorkModeEnum.Done )
						MessageBox.Show( EndWorkMode.Message, "Отображение метаданных книг", MessageBoxButtons.OK, MessageBoxIcon.Information );
					listViewFB2Files.EndUpdate();
					Cursor.Current = Cursors.Default;
				} else {
					// просто отображаем метаданные книг без прогресса
					WorksWithBooks.generateBooksListWithMetaData(
						listViewFB2Files, dirPath, ref fb2Genres, checkBoxTagsView.Checked, true,
						true, null, null, null, null
					);
				}
			} else {
				// просто отображаем метаданные книг без прогресса
				WorksWithBooks.generateBooksListWithMetaData(
					listViewFB2Files, dirPath, ref fb2Genres, checkBoxTagsView.Checked, true,
					true, null, null, null, null
				);
			}
		}
		
		// ========================================================================================================================
		// 													Для Избранной Сортировки
		// ========================================================================================================================
		// заполнение списка критериев поиска для Избранной Сортировки
		List<SelectedSortQueryCriteria> makeCriteriasList() {
			List<SelectedSortQueryCriteria> list = new List<SortQueryCriteria>();
			if( lvSSData.Items.Count > 0 ) {
				string sLang, sLast, sFirst, sMiddle, sNick, sGGroup, sGenre, sSequence, sBTitle, sExactFit;
				for( int i = 0; i != lvSSData.Items.Count; ++i ) {
					sLang = lvSSData.Items[i].Text;
					sGGroup = lvSSData.Items[i].SubItems[1].Text;
					sGenre = lvSSData.Items[i].SubItems[2].Text;
					sLast = lvSSData.Items[i].SubItems[3].Text;
					sFirst = lvSSData.Items[i].SubItems[4].Text;
					sMiddle = lvSSData.Items[i].SubItems[5].Text;
					sNick = lvSSData.Items[i].SubItems[6].Text;
					sSequence = lvSSData.Items[i].SubItems[7].Text;
					sBTitle = lvSSData.Items[i].SubItems[8].Text;
					sExactFit = lvSSData.Items[i].SubItems[9].Text;
					// заполняем список критериев поиска для Избранной Сортировки
					SortQueryCriteria SelSortQuery = new SelectedSortQueryCriteria(
						sLang, sGGroup, sGenre,
						sLast, sFirst, sMiddle, sNick, sSequence, sBTitle,
						sExactFit == "Да" ? true : false
					);
					list.AddRange(
						FB2SelectedSorting.makeSelectedSortQuerysList( ref SelSortQuery )
					);
				}
			}
			return list;
		}
		#endregion
		
		#region Обработчики событий
		void BtnDefRestoreClick(object sender, EventArgs e)
		{
			switch( tcFM.SelectedIndex ) {
				case 0: // основные - для Менеджера Файлов
					DefFMGeneral();
					break;
				case 1: // название папки шаблонного тэга без данных
					switch( tcDesc.SelectedIndex ) {
						case 0: // название для данных Книги из Description когда нет данных тэга
							DefFMDirNameForTagNotData();
							break;
						case 1: // название для данных Издательства из Description когда нет данных тэга
							DefFMDirNameForPublisherTagNotData();
							break;
						case 2: // название для данных fb2-файла из Description когда нет данных тэга
							DefFMDirNameForFB2TagNotData();
							break;
					}
					break;
			}
		}
		
		void BtnGroupClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.Group);
		}
		
		void BtnLetterFamilyClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.LetterFamily);
		}
		
		void BtnGroupGenreClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.GroupGenre);
		}
		
		void BtnLangClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.Language);
		}
		
		void BtnFamilyClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.Family);
		}
		
		void BtnNameClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.Name);
		}
		
		void BtnPatronimicClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.Patronimic);
		}
		
		void BtnGenreClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.Genre);
		}
		
		void BtnBookClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.BookTitle);
		}
		
		void BtnSequenceClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.Series);
		}
		
		void BtnSequenceNumberClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.SeriesNumber);
		}
		
		void BtnDirClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, @"\");
		}
		
		void BtnLeftBracketClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, "[");
		}
		
		void BtnRightBracketClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, "]");
		}
		
		void BtnSSLetterFamilyClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.LetterFamily);
		}
		
		void BtnSSGroupGenreClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.GroupGenre);
		}
		
		void BtnSSGroupClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.Group);
		}
		
		void BtnSSLangClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.Language);
		}
		
		void BtnSSFamilyClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.Family);
		}
		
		void BtnSSNameClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.Name);
		}
		
		void BtnSSPatronimicClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.Patronimic);
		}
		
		void BtnSSGenreClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.Genre);
		}
		
		void BtnSSBookClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.BookTitle);
		}
		
		void BtnSSSequenceClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.Series);
		}
		
		void BtnSSSequenceNumberClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.SeriesNumber);
		}

		void BtnSSDirClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, @"\");
		}
		
		void BtnSSLeftBracketClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, "[");
		}
		
		void BtnSSRightBracketClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, "]");
		}
		
		void TsmiColumnsExplorerAutoReizeClick(object sender, EventArgs e)
		{
			MiscListView.AutoResizeColumns( listViewFB2Files );
		}
		
		// Отображать/скрывать описание книг
		void CheckBoxTagsViewClick(object sender, EventArgs e)
		{
			if( checkBoxTagsView.Checked ) {
				if( m_ViewMessageForLongTime ) {
					string Mess = "При включении этой опции для создания списка книг с их описанием может потребоваться очень много времени!\nБольше не показывать это сообщение?";
					DialogResult result = MessageBox.Show(
						Mess, "Отображение описания книг", MessageBoxButtons.YesNo, MessageBoxIcon.Question
					);
					m_ViewMessageForLongTime = (result == DialogResult.Yes) ? false : true;
				}
			}
			saveSettingsToXml();
			
			if( listViewFB2Files.Items.Count > 0 ) {
				Cursor.Current = Cursors.WaitCursor;
				listViewFB2Files.BeginUpdate();
				DirectoryInfo dirInfo = new DirectoryInfo( textBoxAddress.Text.Trim() );
				// спиок жанров для Полной Сортировки для режима отображения метаданных для файлов Проводника
				FB2UnionGenres fb2Genres = new FB2UnionGenres();
				if( ( dirInfo.GetDirectories().Length + dirInfo.GetFiles().Length ) > Convert.ToInt16( comboBoxMaxFileForProgress.Text ) ) {
					// запуск прогресса отображения метаданных книг
					FB2TagsViewForm fb2TagsViewForm = new FB2TagsViewForm(
						false, checkBoxTagsView.Checked, listViewFB2Files, null
					);
					fb2TagsViewForm.ShowDialog();
					EndWorkMode EndWorkMode = fb2TagsViewForm.EndMode;
					fb2TagsViewForm.Dispose();
					if( EndWorkMode.EndMode != EndWorkModeEnum.Done )
						MessageBox.Show( EndWorkMode.Message, "Отображение метаданных книг", MessageBoxButtons.OK, MessageBoxIcon.Information );
				} else {
					// просто отображаем данные без прогресса
					WorksWithBooks.viewOrHideBookMetaDataLocal(
						listViewFB2Files, ref fb2Genres, checkBoxTagsView.Checked, null, null
					);
				}
				MiscListView.AutoResizeColumns( listViewFB2Files );
				listViewFB2Files.EndUpdate();
				Cursor.Current = Cursors.Default;
			}
		}
		
		// задание папки с fb2-файлами и архивами для сканирования
		void ButtonOpenSourceDirClick(object sender, EventArgs e)
		{
			if(filesWorker.OpenDirDlg( textBoxAddress, fbdScanDir, "Укажите папку для сканирования с fb2-файлами и архивами:" ))
				ButtonGoClick( sender, e );
		}
		
		// переход на заданную папку-источник fb2-файлов
		void ButtonGoClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			string s = textBoxAddress.Text.Trim();
			if( s != string.Empty ) {
				if ( s.Substring(s.Length-1, 1) != "\\" )
					s = textBoxAddress.Text = textBoxAddress.Text + "\\";
				DirectoryInfo info = new DirectoryInfo(s);
				if( info.Exists )
					FullSortingGenerateSourceList( info.FullName );
				else
					MessageBox.Show( "Не удается найти папку " + textBoxAddress.Text + ".\nПроверьте правильность пути.", "Переход по выбранному адресу", MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
			ConnectListsEventHandlers( true );
		}
		
		// обработка нажатия клавиш в поле ввода пути к папке-источнику
		void TextBoxAddressKeyPress(object sender, KeyPressEventArgs e)
		{
			if ( e.KeyChar == (char)Keys.Return ) {
				// отображение папок и/или фалов в заданной папке
				ButtonGoClick( sender, e );
			} else if ( e.KeyChar == '/' || e.KeyChar == '*' || e.KeyChar == '<' || e.KeyChar == '>' || e.KeyChar == '?' || e.KeyChar == '"') {
				e.Handled = true;
			}
		}
		
		// переход в выбранную папку
		void ListViewSourceDoubleClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			if( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = listViewFB2Files.SelectedItems;
				ListViewItemType it = (ListViewItemType)si[0].Tag;
				if( it.Type == "d" || it.Type == "dUp" ) {
					textBoxAddress.Text = it.Value;
					FullSortingGenerateSourceList( it.Value );
				}
			}
			ConnectListsEventHandlers( true );
		}
		void ListViewSourceItemCheck(object sender, ItemCheckEventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count != 0 ) {
				// при двойном клике на папке ".." пометку не ставим
				ConnectListsEventHandlers( false );
				if( e.Index == 0 ) // ".."
					e.NewValue = CheckState.Unchecked;
				ConnectListsEventHandlers( true );
			}
		}
		
		// пометка/снятие пометки по check на 0-й item - папка ".."
		void ListViewSourceItemChecked(object sender, ItemCheckedEventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 ) {
				ListViewItemType it = (ListViewItemType)e.Item.Tag;
				if( it.Type == "dUp" ) {
					ConnectListsEventHandlers( false );
					if( e.Item.Checked )
						MiscListView.CheckAllListViewItems( listViewFB2Files, true );
					else
						MiscListView.UnCheckAllListViewItems( listViewFB2Files.CheckedItems );;
					ConnectListsEventHandlers( true );
				}
			}
		}
		
		// обработка нажатия клавиш на списке папок и файлов
		void ListViewSourceKeyPress(object sender, KeyPressEventArgs e)
		{
			if ( e.KeyChar == (char)Keys.Return ) {
				// переход в выбранную папку
				ListViewSourceDoubleClick(sender, e);
			} else if ( e.KeyChar == (char)Keys.Back ) {
				// переход на каталог выше
				ConnectListsEventHandlers( false );
				
				string address = textBoxAddress.Text.Trim();
				int index = address.LastIndexOf('\\');
				string oldAddress = string.Empty;
				if ( index < address.Length )
					oldAddress = address.Substring(index+1);
				
				ListViewItemType it = (ListViewItemType)listViewFB2Files.Items[0].Tag;
				textBoxAddress.Text = it.Value;
				FullSortingGenerateSourceList( it.Value );
				
				if ( !string.IsNullOrEmpty( oldAddress ) ) {
					ListViewItem Item = listViewFB2Files.FindItemWithText(oldAddress);
					if ( Item != null ) {
						Item.Selected = true;
						Item.Focused = true;
					}
				}
				
				ConnectListsEventHandlers( true );
			}
		}
		
		// Пометить все файлы и папки
		void TsmiCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			MiscListView.CheckAllListViewItems( listViewFB2Files, true );
			if(listViewFB2Files.Items.Count > 0) {
				listViewFB2Files.Items[0].Checked = false;
			}
			ConnectListsEventHandlers( true );
		}
		
		// Снять отметки со всех файлов и папок
		void TsmiUnCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			MiscListView.UnCheckAllListViewItems( listViewFB2Files.CheckedItems );
			ConnectListsEventHandlers( true );
		}
		
		// Пометить все файлы
		void TsmiFilesCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			MiscListView.CheckAllFiles(listViewFB2Files, true);
			ConnectListsEventHandlers( true );
		}
		
		// Снять пометки со всех файлов
		void TsmiFilesUnCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			MiscListView.UnCheckAllFiles(listViewFB2Files);
			ConnectListsEventHandlers( true );
		}
		
		// Пометить все папки
		void TsmiDirCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			MiscListView.CheckAllDirs(listViewFB2Files, true);
			ConnectListsEventHandlers( true );
		}
		
		// Снять пометки со всех папок
		void TsmiDirUnCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			MiscListView.UnCheckAllDirs(listViewFB2Files);
			ConnectListsEventHandlers( true );
		}
		
		// Пометить все fb2 файлы
		void TsmiFB2CheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			MiscListView.CheckTypeAllFiles(listViewFB2Files, "fb2", true);
			ConnectListsEventHandlers( true );
		}
		
		// Снять пометки со всех fb2 файлов
		void TsmiFB2UnCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			MiscListView.UnCheckTypeAllFiles(listViewFB2Files, "fb2");
			ConnectListsEventHandlers( true );
		}
		
		// Пометить все zip файлы
		void TsmiZipCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			MiscListView.CheckTypeAllFiles(listViewFB2Files, "zip", true);
			ConnectListsEventHandlers( true );
		}
		
		// Снять пометки со всех zip файлов
		void TsmiZipUnCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			MiscListView.UnCheckTypeAllFiles(listViewFB2Files, "zip");
			ConnectListsEventHandlers( true );
		}
		
		// Пометить всё выделенное
		void TsmiCheckedAllSelectedClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			MiscListView.ChekAllSelectedItems(listViewFB2Files, true);
			ConnectListsEventHandlers( true );
			listViewFB2Files.Focus();
		}
		
		// Снять пометки со всего выделенного
		void TsmiUnCheckedAllSelectedClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			MiscListView.ChekAllSelectedItems(listViewFB2Files, false);
			ConnectListsEventHandlers( true );
			listViewFB2Files.Focus();
		}
		
		// =====================================================================================================================
		// 													Полная сортировка
		// =====================================================================================================================
		void ButtonSortFilesToClick(object sender, EventArgs e)
		{
			// отображение списка файлов в указанной папке
			if ( listViewFB2Files.Items.Count == 0 )
				ButtonGoClick(sender, e);
			
			// обработка заданных каталога
			m_sMessTitle = "SharpFBTools - Полная Сортировка";
			// загрузка всех настроек Сортировки
			m_sortOptions = new SortingOptions( true, null );
			// проверка на корректность данных папок источника и приемника файлов
			if( !IsSourceDirDataCorrect( m_sortOptions.SourceDir, m_sortOptions.TargetDir ) )
				return;

			// приведение к одному виду шаблонов
			m_sortOptions.Template = TemplatesVerify.ToOneTemplateType( @m_sortOptions.Template );
			// проверки на корректность шаблонных строк
			if( !IsLineTemplateCorrect( m_sortOptions.Template ) )
				return;
			
			// инициализация контролов
			Init();

			SortingForm sortingForm = new SortingForm(
				ref m_sortOptions, listViewFB2Files, lvFilesCount,
				Convert.ToInt16( comboBoxMaxBTLenght.Text ), Convert.ToInt16( comboBoxMaxSequenceLenght.Text )
			);
			sortingForm.ShowDialog();
			EndWorkMode EndWorkMode = sortingForm.EndMode;
			sortingForm.Dispose();
			
			MessageBox.Show( EndWorkMode.Message, "SharpFBTools - Полная Сортировка", MessageBoxButtons.OK, MessageBoxIcon.Information );
			
			if( !m_sortOptions.NotDelOriginalFiles )
				FullSortingGenerateSourceList( m_sortOptions.SourceDir );
			m_sortOptions = null;
		}
		
		// Возобновление Полной сортировки из xml
		void ButtonFullSortRenewClick(object sender, EventArgs e)
		{
			// загрузка данных из xml
			sfdLoadList.InitialDirectory = Settings.Settings.ProgDir;
			sfdLoadList.Title		= "Укажите файл для возобновления Полной Сортировки книг:";
			sfdLoadList.Filter		= "SharpFBTools Файлы хода работы Сортировщика (*.fullsort_break)|*.fullsort_break";
			sfdLoadList.FileName	= string.Empty;
			DialogResult result		= sfdLoadList.ShowDialog();

			if( result != DialogResult.OK )
				return;
			
			// инициализация контролов
			Init();
			m_sortOptions = new SortingOptions( true, sfdLoadList.FileName );
			
			// устанавливаем данные настройки поиска-сравнения
			textBoxAddress.Text = m_sortOptions.SourceDir;
			txtBoxTemplatesFromLine.Text = m_sortOptions.Template;
			chBoxScanSubDir.Checked = m_sortOptions.ScanSubDirs;
			chBoxFSToZip.Checked = m_sortOptions.ToZip;
			chBoxFSNotDelFB2Files.Checked = m_sortOptions.NotDelOriginalFiles;
			SortingForm sortingForm = new SortingForm(
				ref m_sortOptions, listViewFB2Files, lvFilesCount,
				Convert.ToInt16( comboBoxMaxBTLenght.Text ), Convert.ToInt16( comboBoxMaxSequenceLenght.Text )
			);
			sortingForm.ShowDialog();
			//TODO Доделать вывод инфы и в контролы????
			EndWorkMode EndWorkMode = sortingForm.EndMode;
			sortingForm.Dispose();
			MessageBox.Show( EndWorkMode.Message, "SharpFBTools - Полная Сортировка", MessageBoxButtons.OK, MessageBoxIcon.Information );
			
			if( !m_sortOptions.NotDelOriginalFiles )
				FullSortingGenerateSourceList( m_sortOptions.SourceDir );
			m_sortOptions = null;
		}
		
		void ChBoxFSToZipClick(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void ChBoxFSNotDelFB2FilesClick(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void ComboBoxMaxFileForProgressSelectedIndexChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void ChBoxSSToZipClick(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void ChBoxSSNotDelFB2FilesClick(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void ChBoxScanSubDirClick(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}

		void ChBoxSSScanSubDirClick(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void TextBoxAddressTextChanged(object sender, EventArgs e)
		{
			if ( !string.IsNullOrEmpty( textBoxAddress.Text ) ) {
				if( textBoxAddress.Text.Substring(textBoxAddress.Text.Length - 2, 2) == "\\\\" ) {
					textBoxAddress.Text = textBoxAddress.Text.Remove(textBoxAddress.Text.Length - 1, 1);
					textBoxAddress.SelectionStart = textBoxAddress.Text.Length;
				} else if( textBoxAddress.Text.Substring(textBoxAddress.Text.Length - 2, 2) == "\\." ) {
					textBoxAddress.Text = textBoxAddress.Text.Remove(textBoxAddress.Text.Length-1, 1);
					textBoxAddress.SelectionStart = textBoxAddress.Text.Length;
				} else if( textBoxAddress.Text.Substring( textBoxAddress.Text.Length - 3, 3) == "\\.." ) {
					textBoxAddress.Text = textBoxAddress.Text.Remove( textBoxAddress.Text.Length - 1, 1 );
					textBoxAddress.SelectionStart = textBoxAddress.Text.Length;
				}
				
				if( textBoxAddress.Text[textBoxAddress.Text.Length - 1] == '\\' )
					labelTargetPath.Text = textBoxAddress.Text.Remove(textBoxAddress.Text.Length - 1, 1);
				else
					labelTargetPath.Text = textBoxAddress.Text;
				labelTargetPath.Text += "-OUT";
			}
			saveSettingsToXml();
		}
		
		void TxtBoxTemplatesFromLineTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void TboxSSSourceDirTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void TboxSSToDirTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void TxtBoxSSTemplatesFromLineTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		// запуск диалога Вставки готовых шаблонов
		void BtnInsertTemplatesClick(object sender, EventArgs e)
		{
			Core.FileManager.BasicTemplates btfrm = new Core.FileManager.BasicTemplates();
			btfrm.ShowDialog();
			if( btfrm.GetTemplateLine()!=null )
				txtBoxTemplatesFromLine.Text = btfrm.GetTemplateLine();

			btfrm.Dispose();
		}
		
		// запуск диалога Сбора данных для Избранной Сортировки
		void BtnSSGetDataClick(object sender, EventArgs e)
		{
			SelectedSortDataForm ssdfrm = new SelectedSortDataForm();
			// если в основном списке критериев поиска уже есть записи, то копируем их в форму сбора данных
			if( lvSSData.Items.Count > 0 ) {
				for( int i=0; i!=lvSSData.Items.Count; ++i ) {
					ListViewItem lvi = new ListViewItem( lvSSData.Items[i].Text );
					lvi.SubItems.Add( lvSSData.Items[i].SubItems[1].Text );
					lvi.SubItems.Add( lvSSData.Items[i].SubItems[2].Text );
					lvi.SubItems.Add( lvSSData.Items[i].SubItems[3].Text );
					lvi.SubItems.Add( lvSSData.Items[i].SubItems[4].Text );
					lvi.SubItems.Add( lvSSData.Items[i].SubItems[5].Text );
					lvi.SubItems.Add( lvSSData.Items[i].SubItems[6].Text );
					lvi.SubItems.Add( lvSSData.Items[i].SubItems[7].Text );
					lvi.SubItems.Add( lvSSData.Items[i].SubItems[8].Text );
					lvi.SubItems.Add( lvSSData.Items[i].SubItems[9].Text );
					ssdfrm.lvSSData.Items.Add( lvi );
				}
				ssdfrm.lblCount.Text = Convert.ToString( lvSSData.Items.Count );
			}
			
			ssdfrm.ShowDialog();
			
			if( ssdfrm.IsOKClicked() ) {
				/* сохраняем настройки */
				saveSettingsToXml();
				/* обрабатываем собранные данные */
				if( ssdfrm.lvSSData.Items.Count > 0 ) {
					// удаляем записи в списке, если они есть
					lvSSData.Items.Clear();
					for( int i=0; i!=ssdfrm.lvSSData.Items.Count; ++i ) {
						ListViewItem lvi = new ListViewItem( ssdfrm.lvSSData.Items[i].Text );
						lvi.SubItems.Add( ssdfrm.lvSSData.Items[i].SubItems[1].Text );
						lvi.SubItems.Add( ssdfrm.lvSSData.Items[i].SubItems[2].Text );
						lvi.SubItems.Add( ssdfrm.lvSSData.Items[i].SubItems[3].Text );
						lvi.SubItems.Add( ssdfrm.lvSSData.Items[i].SubItems[4].Text );
						lvi.SubItems.Add( ssdfrm.lvSSData.Items[i].SubItems[5].Text );
						lvi.SubItems.Add( ssdfrm.lvSSData.Items[i].SubItems[6].Text );
						lvi.SubItems.Add( ssdfrm.lvSSData.Items[i].SubItems[7].Text );
						lvi.SubItems.Add( ssdfrm.lvSSData.Items[i].SubItems[8].Text );
						lvi.SubItems.Add( ssdfrm.lvSSData.Items[i].SubItems[9].Text );
						// добавление записи в список
						lvSSData.Items.Add( lvi );
					}
				}
				MiscListView.AutoResizeColumns( lvSSData );
			}
			
			ssdfrm.Dispose();
		}
		
		// запуск диалога Вставки готовых шаблонов
		void BtnSSInsertTemplatesClick(object sender, EventArgs e)
		{
			Core.FileManager.BasicTemplates btfrm = new Core.FileManager.BasicTemplates();
			btfrm.ShowDialog();
			if( btfrm.GetTemplateLine()!= null )
				txtBoxSSTemplatesFromLine.Text = btfrm.GetTemplateLine();

			btfrm.Dispose();
		}
		
		// задание папки с fb2-файлами и архивами для сканирования (Избранная Сортировка)
		void BtnSSOpenDirClick(object sender, EventArgs e)
		{
			filesWorker.OpenDirDlg( tboxSSSourceDir, fbdScanDir, "Укажите папку для сканирования с fb2-файлами и архивами (Избранная Сортировка):" );
		}
		
		// задание папки-приемника для размешения отсортированных файлов (Избранная Сортировка)
		void BtnSSTargetDirClick(object sender, EventArgs e)
		{
			filesWorker.OpenDirDlg( tboxSSToDir, fbdScanDir, "Укажите папку-приемник для размешения отсортированных файлов (Избранная Сортировка):" );
		}
		
		void TsmiViewInReaderClick(object sender, EventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count != 0 ) {
				// читаем путь к читалке из настроек
				string sFBReaderPath = Settings.Settings.ReadFBReaderPath();
				const string sTitle = "SharpFBTools - Открытие папки для файла";
				if( !File.Exists( sFBReaderPath ) ) {
					MessageBox.Show( "Не могу найти Читалку \""+sFBReaderPath+"\"!\nПроверьте, правильно ли задан путь в Настройках.", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				ListView.SelectedListViewItemCollection si = listViewFB2Files.SelectedItems;
				string sFilePath = Path.Combine( textBoxAddress.Text.Trim(), si[0].SubItems[0].Text.Split('/')[0].Trim() );
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: \""+sFilePath+"\" не найден!", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				filesWorker.StartFile( sFBReaderPath, sFilePath );
			}
		}
		
		// =====================================================================================================================
		// 													Избранная Сортировка
		// =====================================================================================================================
		void ButtonSSortFilesToClick(object sender, EventArgs e)
		{
			m_sMessTitle = "SharpFBTools - Избранная Сортировка";
			// загрузка всех настроек Сортировки
			m_sortOptions = new SortingOptions( false, null );
			//задаем критерии Избранной Сортировки в класс m_sortOptions
			m_sortOptions.setCriterias( makeCriteriasList() );
			
			// проверка на корректность данных папок источника и приемника файлов
			if( !IsFoldersDataCorrect( m_sortOptions.SourceDir, m_sortOptions.TargetDir ) ) {
				return;
			}
			// проверка на наличие критериев поиска для Избранной Сортировки
			if( lvSSData.Items.Count == 0 ) {
				MessageBox.Show(
					"Задайте хоть один критерий для Избранной Сортировки (кнопка \"Собрать данные для Избранной Сортировки\")!",
					m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information
				);
				btnSSGetData.Focus();
				return;
			}

			// приведение к одному виду шаблонов
			m_sortOptions.Template = TemplatesVerify.ToOneTemplateType( @m_sortOptions.Template );
			// проверки на корректность шаблонных строк
			if( !IsLineTemplateCorrect( m_sortOptions.Template ) )
				return;
			
			// инициализация контролов
			Init();
			SortingForm sortingForm = new SortingForm(
				ref m_sortOptions, lvFilesCount,
				Convert.ToInt16( comboBoxMaxBTLenght.Text ), Convert.ToInt16( comboBoxMaxSequenceLenght.Text )
			);
			sortingForm.ShowDialog();
			EndWorkMode EndWorkMode = sortingForm.EndMode;
			sortingForm.Dispose();
			m_sortOptions = null;
			MessageBox.Show(
				EndWorkMode.Message, "SharpFBTools - Избранная Сортировка",
				MessageBoxButtons.OK, MessageBoxIcon.Information
			);
		}
		
		// Возобновление Избранной сортировки из xml
		void ButtonSSortRenewClick(object sender, EventArgs e)
		{
			// загрузка данных из xml
			sfdLoadList.InitialDirectory = Settings.Settings.ProgDir;
			sfdLoadList.Title		= "Укажите файл для возобновления Избранной Сортировки книг:";
			sfdLoadList.Filter		= "SharpFBTools Файлы хода работы Сортировщика (*.selsort_break)|*.selsort_break";
			sfdLoadList.FileName	= string.Empty;
			DialogResult result		= sfdLoadList.ShowDialog();

			if( result != DialogResult.OK )
				return;
			
			// инициализация контролов
			Init();
			m_sortOptions = new SortingOptions( false, sfdLoadList.FileName );
			
			// устанавливаем данные настройки поиска-сравнения
			tboxSSSourceDir.Text = m_sortOptions.SourceDir;
			tboxSSToDir.Text = m_sortOptions.TargetDir;
			txtBoxSSTemplatesFromLine.Text = m_sortOptions.Template;
			chBoxSSScanSubDir.Checked = m_sortOptions.ScanSubDirs;
			chBoxSSToZip.Checked = m_sortOptions.ToZip;
			chBoxSSNotDelFB2Files.Checked = m_sortOptions.NotDelOriginalFiles;
			
			// загрузка критериев для Избранной сортировки
			// удаляем записи в списке, если они есть
			lvSSData.Items.Clear();
			List<SelectedSortQueryCriteria> lSSQCList = m_sortOptions.getCriterias();
			foreach( SelectedSortQueryCriteria c in lSSQCList ) {
				ListViewItem lvi = new ListViewItem( c.Lang );
				lvi.SubItems.Add( c.GenresGroup );
				lvi.SubItems.Add( c.Genre );
				lvi.SubItems.Add( c.LastName );
				lvi.SubItems.Add( c.FirstName );
				lvi.SubItems.Add( c.MiddleName );
				lvi.SubItems.Add( c.NickName );
				lvi.SubItems.Add( c.Sequence );
				lvi.SubItems.Add( c.BookTitle );
				lvi.SubItems.Add( c.ExactFit ? "Да" : "Нет" );
				// добавление записи в список
				lvSSData.Items.Add( lvi );
			}

			SortingForm sortingForm = new SortingForm(
				ref m_sortOptions, lvFilesCount,
				Convert.ToInt16( comboBoxMaxBTLenght.Text ), Convert.ToInt16( comboBoxMaxSequenceLenght.Text )
			);
			sortingForm.ShowDialog();
			//TODO Доделать вывод инфы и в контролы????
			EndWorkMode EndWorkMode = sortingForm.EndMode;
			sortingForm.Dispose();
			m_sortOptions = null;
			MessageBox.Show( EndWorkMode.Message, "SharpFBTools - Избранная Сортировка", MessageBoxButtons.OK, MessageBoxIcon.Information );
		}
		
		// сохранить список критериев Избранной Сортировки в файл
		void BtnSSDataListSaveClick(object sender, EventArgs e)
		{
			const string sMessTitle = "SharpFBTools - Избранная Сортировка";
			if( lvSSData.Items.Count == 0 ) {
				MessageBox.Show( "Список данных для Избранной Сортировки пуст.\nЗадайте хоть один критерий Сортировки (кнопка 'Собрать данные для Избранной Сортировки').",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			sfdSaveXMLFile.Filter = "SharpFBTools файлы (*.sort_criterias)|*.sort_criterias";;
			sfdSaveXMLFile.FileName = string.Empty;
			DialogResult result = sfdSaveXMLFile.ShowDialog();
			if( result == DialogResult.OK ) {
				XDocument doc = new XDocument(
					new XDeclaration("1.0", "utf-8", "yes"),
					new XComment("Файл критериев сортировки fb2-книг"),
					new XElement("Criterias", new XAttribute("count", lvSSData.Items.Count.ToString()), new XAttribute("type", "sort_criterias"),
					             new XComment("Критерии сортировки")
					            )
				);
				if ( lvSSData.Items.Count > 0 ) {
					int critNumber = 0;
					foreach (ListViewItem item in lvSSData.Items ) {
						doc.Root.Add( new XElement("Criteria", new XAttribute("number", critNumber++),
						                           new XElement("Lang", item.SubItems[0].Text),
						                           new XElement("GGroup", item.SubItems[1].Text),
						                           new XElement("Genre", item.SubItems[2].Text),
						                           new XElement("LastName", item.SubItems[3].Text),
						                           new XElement("FirstName", item.SubItems[4].Text),
						                           new XElement("MiddleName", item.SubItems[5].Text),
						                           new XElement("NickName", item.SubItems[6].Text),
						                           new XElement("Sequence", item.SubItems[7].Text),
						                           new XElement("BookTitle", item.SubItems[8].Text),
						                           new XElement("ExactFit", item.SubItems[9].Text)
						                          )
						            );
					}
					doc.Save( sfdSaveXMLFile.FileName ) ;
					MessageBox.Show( "Список данных для Избранной Сортировки сохранен в файл!", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				}
			}
		}
		
		// загрузить список критериев Избранной Сортировки из файла
		void BtnSSDataListLoadClick(object sender, EventArgs e)
		{
			sfdOpenXMLFile.Filter = "SharpFBTools файлы (*.sort_criterias)|*.sort_criterias";
			sfdOpenXMLFile.FileName = string.Empty;
			DialogResult result = sfdOpenXMLFile.ShowDialog();
			if( result == DialogResult.OK ) {
				lvSSData.Items.Clear();
				XElement xmlTree = XElement.Load( sfdOpenXMLFile.FileName );
				ListViewItem lvi = null;
				IEnumerable<XElement> criterias = xmlTree.Elements("Criteria");
				foreach( XElement crit in criterias ) {
					lvi = new ListViewItem( crit.Element("Lang").Value );
					lvi.SubItems.Add( crit.Element("GGroup").Value );
					lvi.SubItems.Add( crit.Element("Genre").Value );
					lvi.SubItems.Add( crit.Element("LastName").Value );
					lvi.SubItems.Add( crit.Element("FirstName").Value );
					lvi.SubItems.Add( crit.Element("MiddleName").Value );
					lvi.SubItems.Add( crit.Element("NickName").Value );
					lvi.SubItems.Add( crit.Element("Sequence").Value );
					lvi.SubItems.Add( crit.Element("BookTitle").Value );
					lvi.SubItems.Add( crit.Element("ExactFit").Value );
					lvSSData.Items.Add( lvi );
				}
				MiscListView.AutoResizeColumns(lvSSData);
			}
		}
		
		// =====================================================================================================================
		//													Настройки
		// =====================================================================================================================
		void RbtnAsIsClick(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void ChBoxTranslitClick(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void ChBoxStrictClick(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void CboxSpaceSelectedIndexChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void CboxFileExistSelectedIndexChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void RbtnFMAllFB2Click(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void RbtnAuthorOneClick(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void RbtnGenreOneClick(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void RbtnGenreSchemaClick(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		void ComboBoxMaxBTLenghtSelectedIndexChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		void ComboBoxMaxSequenceLenghtSelectedIndexChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		void TxtBoxFMNoGenreGroupTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
			if( ((TextBox)sender).TextLength < 1 )
				((TextBox)sender).BackColor = Color.LightPink;
			else
				((TextBox)sender).BackColor = Color.White;
		}
		
		void TxtBoxFMNoYearTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
			if( ((TextBox)sender).TextLength < 1 )
				((TextBox)sender).BackColor = Color.LightPink;
			else
				((TextBox)sender).BackColor = Color.White;
		}
		
		void TxtBoxFMNoFB2FirstNameTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
			if( ((TextBox)sender).TextLength < 1 )
				((TextBox)sender).BackColor = Color.LightPink;
			else
				((TextBox)sender).BackColor = Color.White;
		}
		
		void ListViewSourceColumnClick(object sender, ColumnClickEventArgs e)
		{
			if ( e.Column == m_lvwColumnSorter.SortColumn ) {
				// Изменить сортировку на обратную для выбранного столбца
				if( m_lvwColumnSorter.Order == SortOrder.Ascending )
					m_lvwColumnSorter.Order = SortOrder.Descending;
				else
					m_lvwColumnSorter.Order = SortOrder.Ascending;
			} else {
				// Задать номер столбца для сортировки (по-умолчанию Ascending)
				m_lvwColumnSorter.SortColumn = e.Column;
				m_lvwColumnSorter.Order = SortOrder.Ascending;
			}
			listViewFB2Files.ListViewItemSorter = m_lvwColumnSorter; // перед listViewSource.Sort(); иначе - "тормоза"
			listViewFB2Files.Sort();
		}
		
		#endregion
	}
	
}
