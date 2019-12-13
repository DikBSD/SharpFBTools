/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 16.03.2009
 * Time: 10:03
 * 
 * License: GPL 2.1
 */

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using System.Collections.Generic;

using Core.FB2.Description.CustomInfo;
using Core.FB2.Binary;
using Core.FB2.Genres;
using Core.Common;
using Core.Duplicator;
using Core.FB2.FB2Parsers;

using FB2Validator		= Core.FB2Parser.FB2Validator;
using FilesWorker		= Core.Common.FilesWorker;
using SharpZipLibWorker = Core.Common.SharpZipLibWorker;
using ImageWorker		= Core.Common.ImageWorker;
using StatusView 		= Core.Duplicator.StatusView;
using MiscListView		= Core.Common.MiscListView;
using WorksWithBooks	= Core.Common.WorksWithBooks;

// enums
using CompareModeEnum				= Core.Common.Enums.CompareModeEnum;
using BooksWorkModeEnum				= Core.Common.Enums.BooksWorkModeEnum;
using GroupAnalyzeModeEnum			= Core.Common.Enums.GroupAnalyzeModeEnum;
using ResultViewDupCollumnEnum		= Core.Common.Enums.ResultViewDupCollumnEnum;
using FilesCountViewDupCollumnEnum	= Core.Common.Enums.FilesCountViewDupCollumnEnum;
using BooksValidateModeEnum			= Core.Common.Enums.BooksValidateModeEnum;
using BooksAutoCorrectModeEnum		= Core.Common.Enums.BooksAutoCorrectModeEnum;
using TitleInfoEnum					= Core.Common.Enums.TitleInfoEnum;

namespace SharpFBTools.Tools
{
	/// <summary>
	/// Поиск копий fb2-файлов (различные критерии поиска)
	/// </summary>
	public partial class SFBTpFB2Dublicator : UserControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SFBTpFB2Dublicator));
            System.Windows.Forms.ListViewItem listViewItem103 = new System.Windows.Forms.ListViewItem(new string[] {
            "Название Книги",
            ""}, 0, System.Drawing.Color.Red, System.Drawing.Color.Empty, new System.Drawing.Font("Tahoma", 8F));
            System.Windows.Forms.ListViewItem listViewItem104 = new System.Windows.Forms.ListViewItem(new string[] {
            "Жанр(ы) Книги",
            ""}, 0, System.Drawing.Color.Red, System.Drawing.Color.Empty, new System.Drawing.Font("Tahoma", 8F));
            System.Windows.Forms.ListViewItem listViewItem105 = new System.Windows.Forms.ListViewItem(new string[] {
            "Язык",
            ""}, 0, System.Drawing.Color.Red, System.Drawing.Color.Empty, new System.Drawing.Font("Tahoma", 8F));
            System.Windows.Forms.ListViewItem listViewItem106 = new System.Windows.Forms.ListViewItem(new string[] {
            "Язык оригинала",
            ""}, 0);
            System.Windows.Forms.ListViewItem listViewItem107 = new System.Windows.Forms.ListViewItem(new string[] {
            "Автор(ы) Книги",
            ""}, 0, System.Drawing.Color.Navy, System.Drawing.Color.Empty, null);
            System.Windows.Forms.ListViewItem listViewItem108 = new System.Windows.Forms.ListViewItem(new string[] {
            "Дата написания",
            ""}, 0);
            System.Windows.Forms.ListViewItem listViewItem109 = new System.Windows.Forms.ListViewItem(new string[] {
            "Ключевые слова",
            ""}, 0);
            System.Windows.Forms.ListViewItem listViewItem110 = new System.Windows.Forms.ListViewItem(new string[] {
            "Переводчик(и)",
            ""}, 0);
            System.Windows.Forms.ListViewItem listViewItem111 = new System.Windows.Forms.ListViewItem(new string[] {
            "Серия(и) (Номер)",
            ""}, 0, System.Drawing.Color.Green, System.Drawing.Color.Empty, null);
            System.Windows.Forms.ListViewItem listViewItem112 = new System.Windows.Forms.ListViewItem(new string[] {
            "Название Книги",
            ""}, 0, System.Drawing.Color.Red, System.Drawing.Color.Empty, new System.Drawing.Font("Tahoma", 8F));
            System.Windows.Forms.ListViewItem listViewItem113 = new System.Windows.Forms.ListViewItem(new string[] {
            "Жанр(ы) Книги",
            ""}, 0, System.Drawing.Color.Red, System.Drawing.Color.Empty, new System.Drawing.Font("Tahoma", 8F));
            System.Windows.Forms.ListViewItem listViewItem114 = new System.Windows.Forms.ListViewItem(new string[] {
            "Язык",
            ""}, 0, System.Drawing.Color.Red, System.Drawing.Color.Empty, new System.Drawing.Font("Tahoma", 8F));
            System.Windows.Forms.ListViewItem listViewItem115 = new System.Windows.Forms.ListViewItem(new string[] {
            "Язык оригинала",
            ""}, 0);
            System.Windows.Forms.ListViewItem listViewItem116 = new System.Windows.Forms.ListViewItem(new string[] {
            "Автор(ы) Книги",
            ""}, 0, System.Drawing.Color.Navy, System.Drawing.Color.Empty, null);
            System.Windows.Forms.ListViewItem listViewItem117 = new System.Windows.Forms.ListViewItem(new string[] {
            "Дата написания",
            ""}, 0);
            System.Windows.Forms.ListViewItem listViewItem118 = new System.Windows.Forms.ListViewItem(new string[] {
            "Ключевые слова",
            ""}, 0);
            System.Windows.Forms.ListViewItem listViewItem119 = new System.Windows.Forms.ListViewItem(new string[] {
            "Переводчик(и)",
            ""}, 0);
            System.Windows.Forms.ListViewItem listViewItem120 = new System.Windows.Forms.ListViewItem(new string[] {
            "Серия(и) (Номер)",
            ""}, 0, System.Drawing.Color.Green, System.Drawing.Color.Empty, null);
            System.Windows.Forms.ListViewItem listViewItem121 = new System.Windows.Forms.ListViewItem(new string[] {
            "Число Обложек",
            ""}, 0);
            System.Windows.Forms.ListViewItem listViewItem122 = new System.Windows.Forms.ListViewItem(new string[] {
            "ID Книги",
            ""}, 0, System.Drawing.Color.Red, System.Drawing.Color.Empty, new System.Drawing.Font("Tahoma", 8F));
            System.Windows.Forms.ListViewItem listViewItem123 = new System.Windows.Forms.ListViewItem(new string[] {
            "Версия fb2-файла",
            ""}, 0, System.Drawing.Color.Red, System.Drawing.Color.Empty, new System.Drawing.Font("Tahoma", 8F));
            System.Windows.Forms.ListViewItem listViewItem124 = new System.Windows.Forms.ListViewItem(new string[] {
            "Дата создания: Текст (Значение)",
            ""}, 0);
            System.Windows.Forms.ListViewItem listViewItem125 = new System.Windows.Forms.ListViewItem(new string[] {
            "Программы",
            ""}, 0);
            System.Windows.Forms.ListViewItem listViewItem126 = new System.Windows.Forms.ListViewItem(new string[] {
            "Источник OCR",
            ""}, 0);
            System.Windows.Forms.ListViewItem listViewItem127 = new System.Windows.Forms.ListViewItem(new string[] {
            "Источник URL",
            ""}, 0);
            System.Windows.Forms.ListViewItem listViewItem128 = new System.Windows.Forms.ListViewItem(new string[] {
            "Автор fb2-файла",
            ""}, 0);
            System.Windows.Forms.ListViewItem listViewItem129 = new System.Windows.Forms.ListViewItem(new string[] {
            "Заголовок Книги",
            ""}, 0, System.Drawing.SystemColors.WindowText, System.Drawing.Color.Empty, null);
            System.Windows.Forms.ListViewItem listViewItem130 = new System.Windows.Forms.ListViewItem(new string[] {
            "Издатель",
            ""}, 0);
            System.Windows.Forms.ListViewItem listViewItem131 = new System.Windows.Forms.ListViewItem(new string[] {
            "Город",
            ""}, 0);
            System.Windows.Forms.ListViewItem listViewItem132 = new System.Windows.Forms.ListViewItem(new string[] {
            "Год издания",
            ""}, 0);
            System.Windows.Forms.ListViewItem listViewItem133 = new System.Windows.Forms.ListViewItem(new string[] {
            "ISBN",
            ""}, 0);
            System.Windows.Forms.ListViewItem listViewItem134 = new System.Windows.Forms.ListViewItem(new string[] {
            "Серия(и) (Номер)",
            ""}, 0, System.Drawing.Color.Green, System.Drawing.Color.Empty, null);
            System.Windows.Forms.ListViewItem listViewItem135 = new System.Windows.Forms.ListViewItem(new string[] {
            "Всего групп одинаковых книг",
            "0"}, 0);
            System.Windows.Forms.ListViewItem listViewItem136 = new System.Windows.Forms.ListViewItem(new string[] {
            "Книг во всех группах",
            "0"}, 0);
            this.ssProgress = new System.Windows.Forms.StatusStrip();
            this.tsslblProgress = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.cmsFB2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAnalyzeForSelectedGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAnalyzeVersionValidateInGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAllOldBooksCreationTimeInGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAllOldBooksLastWriteTimeInGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAllNonValidateBooks = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAnalyzeForAllGroups = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAllOldBooksVersionValidateForAllGroups = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAllOldBooksCreationTimeForAllGroups = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAllOldBooksLastWriteTimeForAllGroups = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAllNonValidateBooksForAllGroups = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiValidate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFileReValidate = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAllFilesInGroupReValidate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAllGroupsReValidate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiAutoCorrectorForAllSelectedBooks = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemAutoCorrectorForAllCheckedBooks = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAutoCorrectorForAllBooksFromGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiAutoCorrectorForAllBooksAllGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditAuthors = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSetAuthorsForSelectedBooks = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSetAuthorsForCheckedBooks = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditGenres = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSetGenresForSelectedBooks = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSetGenresForCheckedBooks = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiNewID = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSetNewIDForAllSelectedBooks = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSetNewIDForAllCheckedBooks = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiSetNewIDForAllBooksFromGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiSetNewIDForAllBooksAllGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditBookName = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditDescription = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiEditInTextEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiEditInFB2Editor = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDiffFB2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiViewInReader = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmi2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCopyCheckedFb2To = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMoveCheckedFb2To = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMoveCheckedFb2ToView = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiMoveCheckedFb2ToFast = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteCheckedFb2 = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteCheckedFb2View = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteCheckedFb2Fast = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenFileDir = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiDeleteAllItemForNonExistFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteChechedItemsNotDeleteFiles = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDeleteGroupNotFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiCheckedAllInGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiCheckedAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiUnCheckedAll = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiSaveAllCheckedItemToFile = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsmiColumnsResultAutoReize = new System.Windows.Forms.ToolStripMenuItem();
            this.fbdScanDir = new System.Windows.Forms.FolderBrowserDialog();
            this.sfdLoadList = new System.Windows.Forms.OpenFileDialog();
            this.imageListDup = new System.Windows.Forms.ImageList(this.components);
            this.fbdSaveList = new System.Windows.Forms.FolderBrowserDialog();
            this.sfdList = new System.Windows.Forms.SaveFileDialog();
            this.tcDuplicator = new System.Windows.Forms.TabControl();
            this.tpDuplicator = new System.Windows.Forms.TabPage();
            this.listViewFB2Files = new System.Windows.Forms.ListView();
            this.chPath = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chBookTitle = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chAuthors = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chGenres = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chLang = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chVersion = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chEncodimg = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chValidate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFileLenght = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFileCreationTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.chFileLastWriteTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pInfo = new System.Windows.Forms.Panel();
            this.FB2InfoPanel = new System.Windows.Forms.Panel();
            this.tcViewFB2Desc = new System.Windows.Forms.TabControl();
            this.tpTitleInfo = new System.Windows.Forms.TabPage();
            this.lvTitleInfo = new System.Windows.Forms.ListView();
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpSourceTitleInfo = new System.Windows.Forms.TabPage();
            this.lvSourceTitleInfo = new System.Windows.Forms.ListView();
            this.columnHeader17 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader18 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpDocumentInfo = new System.Windows.Forms.TabPage();
            this.lvDocumentInfo = new System.Windows.Forms.ListView();
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpPublishInfo = new System.Windows.Forms.TabPage();
            this.lvPublishInfo = new System.Windows.Forms.ListView();
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpCustomInfo = new System.Windows.Forms.TabPage();
            this.lvCustomInfo = new System.Windows.Forms.ListView();
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tpHistory = new System.Windows.Forms.TabPage();
            this.rtbHistory = new System.Windows.Forms.RichTextBox();
            this.tpTIAnnotation = new System.Windows.Forms.TabPage();
            this.rtbTIAnnotation = new System.Windows.Forms.RichTextBox();
            this.tpSTIAnnotation = new System.Windows.Forms.TabPage();
            this.rtbSTIAnnotation = new System.Windows.Forms.RichTextBox();
            this.tpValidate = new System.Windows.Forms.TabPage();
            this.tbValidate = new System.Windows.Forms.TextBox();
            this.tcCovers = new System.Windows.Forms.TabControl();
            this.tpTI = new System.Windows.Forms.TabPage();
            this.TICoverPanel = new System.Windows.Forms.Panel();
            this.picBoxTICover = new System.Windows.Forms.PictureBox();
            this.TICoverInfoPanel = new System.Windows.Forms.Panel();
            this.TICoverLenghtLabel = new System.Windows.Forms.Label();
            this.TICoverPixelsLabel = new System.Windows.Forms.Label();
            this.TICoverDPILabel = new System.Windows.Forms.Label();
            this.TICoverListViewPanel = new System.Windows.Forms.Panel();
            this.TICoversListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.TICoverListViewButtonPanel = new System.Windows.Forms.Panel();
            this.TISaveSelectedCoverButton = new System.Windows.Forms.Button();
            this.tpSTI = new System.Windows.Forms.TabPage();
            this.STICoverPanel = new System.Windows.Forms.Panel();
            this.picBoxSTICover = new System.Windows.Forms.PictureBox();
            this.STICoverInfoPanel = new System.Windows.Forms.Panel();
            this.STICoverLenghtLabel = new System.Windows.Forms.Label();
            this.STICoverPixelsLabel = new System.Windows.Forms.Label();
            this.STICoverDPILabel = new System.Windows.Forms.Label();
            this.STICoverListViewPanel = new System.Windows.Forms.Panel();
            this.STICoversListView = new System.Windows.Forms.ListView();
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.STICoverListViewButtonPanel = new System.Windows.Forms.Panel();
            this.STISaveSelectedCoverButton = new System.Windows.Forms.Button();
            this.pFilesCount = new System.Windows.Forms.Panel();
            this.BadZipLabel1 = new System.Windows.Forms.Label();
            this.ZipLabel = new System.Windows.Forms.Label();
            this.NotValidLabel = new System.Windows.Forms.Label();
            this.RazdelitLabel1 = new System.Windows.Forms.Label();
            this.LegengCaptionLabel = new System.Windows.Forms.Label();
            this.lvFilesCount = new System.Windows.Forms.ListView();
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.CommandToolStrip = new System.Windows.Forms.ToolStrip();
            this.toolStripButtonEditInTextEditor = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonEditInFB2Editor = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonDiffFB2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButtonViewInReader = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonCopyCheckedFb2To = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButtonMoveCheckedFb2To = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripMenuItemMoveCheckedFb2ToView = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemMoveCheckedFb2ToFast = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButtonDeleteCheckedFb2 = new System.Windows.Forms.ToolStripDropDownButton();
            this.ToolStripMenuItemDeleteCheckedFb2View = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemDeleteCheckedFb2Fast = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButtonEditDescription = new System.Windows.Forms.ToolStripButton();
            this.toolStripDropDownButtonNewID = new System.Windows.Forms.ToolStripDropDownButton();
            this.ToolStripMenuItemSetNewIDForAllSelectedBooks = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSetNewIDForAllCheckedBooks = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemSetNewIDForAllBooksFromGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
            this.ToolStripMenuItemSetNewIDForAllBooksAllGroup = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButtonEditAuthors = new System.Windows.Forms.ToolStripDropDownButton();
            this.ToolStripMenuItemSetAuthorsForSelectedBooks = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSetAuthorsForCheckedBooks = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripDropDownButtonEditGenres = new System.Windows.Forms.ToolStripDropDownButton();
            this.ToolStripMenuItemSetGenresForSelectedBooks = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripMenuItemSetGenresForCheckedBooks = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripButtonEditBookName = new System.Windows.Forms.ToolStripButton();
            this.pMode = new System.Windows.Forms.Panel();
            this.checkBoxSaveGroupsToXml = new System.Windows.Forms.CheckBox();
            this.cboxMode = new System.Windows.Forms.ComboBox();
            this.lblMode = new System.Windows.Forms.Label();
            this.pSearchFBDup2Dirs = new System.Windows.Forms.Panel();
            this.btnOpenDir = new System.Windows.Forms.Button();
            this.chBoxScanSubDir = new System.Windows.Forms.CheckBox();
            this.tboxSourceDir = new System.Windows.Forms.TextBox();
            this.lblScanDir = new System.Windows.Forms.Label();
            this.tsDup = new System.Windows.Forms.ToolStrip();
            this.tsbtnSearchDubls = new System.Windows.Forms.ToolStripButton();
            this.tsbtnSearchFb2DupRenew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tslGroupCountForList = new System.Windows.Forms.ToolStripLabel();
            this.tscbGroupCountForList = new System.Windows.Forms.ToolStripComboBox();
            this.tsbtnDupSaveList = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnDupOpenList = new System.Windows.Forms.ToolStripButton();
            this.tsbtnDupCurrentSaveList = new System.Windows.Forms.ToolStripButton();
            this.tpOptions = new System.Windows.Forms.TabPage();
            this.cboxDblClickForFB2 = new System.Windows.Forms.ComboBox();
            this.lblValidatorForFB2 = new System.Windows.Forms.Label();
            this.cboxPressEnterForFB2 = new System.Windows.Forms.ComboBox();
            this.lblValidatorForFB2PE = new System.Windows.Forms.Label();
            this.gboxCopyMoveOptions = new System.Windows.Forms.GroupBox();
            this.cboxExistFile = new System.Windows.Forms.ComboBox();
            this.lblExistFile = new System.Windows.Forms.Label();
            this.ssProgress.SuspendLayout();
            this.cmsFB2.SuspendLayout();
            this.tcDuplicator.SuspendLayout();
            this.tpDuplicator.SuspendLayout();
            this.pInfo.SuspendLayout();
            this.FB2InfoPanel.SuspendLayout();
            this.tcViewFB2Desc.SuspendLayout();
            this.tpTitleInfo.SuspendLayout();
            this.tpSourceTitleInfo.SuspendLayout();
            this.tpDocumentInfo.SuspendLayout();
            this.tpPublishInfo.SuspendLayout();
            this.tpCustomInfo.SuspendLayout();
            this.tpHistory.SuspendLayout();
            this.tpTIAnnotation.SuspendLayout();
            this.tpSTIAnnotation.SuspendLayout();
            this.tpValidate.SuspendLayout();
            this.tcCovers.SuspendLayout();
            this.tpTI.SuspendLayout();
            this.TICoverPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxTICover)).BeginInit();
            this.TICoverInfoPanel.SuspendLayout();
            this.TICoverListViewPanel.SuspendLayout();
            this.TICoverListViewButtonPanel.SuspendLayout();
            this.tpSTI.SuspendLayout();
            this.STICoverPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picBoxSTICover)).BeginInit();
            this.STICoverInfoPanel.SuspendLayout();
            this.STICoverListViewPanel.SuspendLayout();
            this.STICoverListViewButtonPanel.SuspendLayout();
            this.pFilesCount.SuspendLayout();
            this.CommandToolStrip.SuspendLayout();
            this.pMode.SuspendLayout();
            this.pSearchFBDup2Dirs.SuspendLayout();
            this.tsDup.SuspendLayout();
            this.tpOptions.SuspendLayout();
            this.gboxCopyMoveOptions.SuspendLayout();
            this.SuspendLayout();
            // 
            // ssProgress
            // 
            this.ssProgress.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ssProgress.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslblProgress,
            this.tsProgressBar});
            this.ssProgress.Location = new System.Drawing.Point(0, 536);
            this.ssProgress.Name = "ssProgress";
            this.ssProgress.Size = new System.Drawing.Size(1123, 24);
            this.ssProgress.TabIndex = 19;
            this.ssProgress.Text = "statusStrip1";
            // 
            // tsslblProgress
            // 
            this.tsslblProgress.Name = "tsslblProgress";
            this.tsslblProgress.Size = new System.Drawing.Size(23, 19);
            this.tsslblProgress.Text = "=>";
            // 
            // tsProgressBar
            // 
            this.tsProgressBar.Name = "tsProgressBar";
            this.tsProgressBar.Size = new System.Drawing.Size(300, 18);
            // 
            // cmsFB2
            // 
            this.cmsFB2.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cmsFB2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAnalyzeForSelectedGroup,
            this.tsmiAnalyzeForAllGroups,
            this.tsmi3,
            this.tsmiValidate,
            this.toolStripMenuItem11,
            this.tsmiEditAuthors,
            this.tsmiEditGenres,
            this.tsmiNewID,
            this.tsmiEditBookName,
            this.tsmiEditDescription,
            this.toolStripMenuItem1,
            this.tsmiEditInTextEditor,
            this.tsmiEditInFB2Editor,
            this.tsmiDiffFB2,
            this.tsmi1,
            this.tsmiViewInReader,
            this.tsmi2,
            this.tsmiCopyCheckedFb2To,
            this.tsmiMoveCheckedFb2To,
            this.tsmiDeleteCheckedFb2,
            this.tsmiOpenFileDir,
            this.toolStripSeparator1,
            this.tsmiDeleteAllItemForNonExistFile,
            this.tsmiDeleteChechedItemsNotDeleteFiles,
            this.tsmiDeleteGroupNotFile,
            this.toolStripSeparator4,
            this.tsmiCheckedAllInGroup,
            this.tsmiCheckedAll,
            this.tsmiUnCheckedAll,
            this.toolStripMenuItem3,
            this.tsmiSaveAllCheckedItemToFile,
            this.toolStripSeparator5,
            this.tsmiColumnsResultAutoReize});
            this.cmsFB2.Name = "cmsValidator";
            this.cmsFB2.Size = new System.Drawing.Size(504, 702);
            // 
            // tsmiAnalyzeForSelectedGroup
            // 
            this.tsmiAnalyzeForSelectedGroup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAnalyzeVersionValidateInGroup,
            this.toolStripMenuItem14,
            this.tsmiAllOldBooksCreationTimeInGroup,
            this.tsmiAllOldBooksLastWriteTimeInGroup,
            this.toolStripMenuItem5,
            this.tsmiAllNonValidateBooks});
            this.tsmiAnalyzeForSelectedGroup.Name = "tsmiAnalyzeForSelectedGroup";
            this.tsmiAnalyzeForSelectedGroup.Size = new System.Drawing.Size(503, 26);
            this.tsmiAnalyzeForSelectedGroup.Text = "Анализ для выбранной группы";
            // 
            // tsmiAnalyzeVersionValidateInGroup
            // 
            this.tsmiAnalyzeVersionValidateInGroup.Name = "tsmiAnalyzeVersionValidateInGroup";
            this.tsmiAnalyzeVersionValidateInGroup.Size = new System.Drawing.Size(396, 22);
            this.tsmiAnalyzeVersionValidateInGroup.Text = "Все \"старые\" fb2 (по версии книги и невалидности)";
            this.tsmiAnalyzeVersionValidateInGroup.Click += new System.EventHandler(this.TsmiAnalyzeVersionValidateInGroupClick);
            // 
            // toolStripMenuItem14
            // 
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            this.toolStripMenuItem14.Size = new System.Drawing.Size(393, 6);
            // 
            // tsmiAllOldBooksCreationTimeInGroup
            // 
            this.tsmiAllOldBooksCreationTimeInGroup.Name = "tsmiAllOldBooksCreationTimeInGroup";
            this.tsmiAllOldBooksCreationTimeInGroup.Size = new System.Drawing.Size(396, 22);
            this.tsmiAllOldBooksCreationTimeInGroup.Text = "Все \"старые\" fb2 (по времени создания файла)";
            this.tsmiAllOldBooksCreationTimeInGroup.Click += new System.EventHandler(this.TsmiAllOldBooksCreationTimeInGroupClick);
            // 
            // tsmiAllOldBooksLastWriteTimeInGroup
            // 
            this.tsmiAllOldBooksLastWriteTimeInGroup.Name = "tsmiAllOldBooksLastWriteTimeInGroup";
            this.tsmiAllOldBooksLastWriteTimeInGroup.Size = new System.Drawing.Size(396, 22);
            this.tsmiAllOldBooksLastWriteTimeInGroup.Text = "Все \"старые\" fb2 (по времени последнего измения файла)";
            this.tsmiAllOldBooksLastWriteTimeInGroup.Click += new System.EventHandler(this.TsmiAllOldBooksLastWriteTimeInGroupClick);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(393, 6);
            // 
            // tsmiAllNonValidateBooks
            // 
            this.tsmiAllNonValidateBooks.Name = "tsmiAllNonValidateBooks";
            this.tsmiAllNonValidateBooks.Size = new System.Drawing.Size(396, 22);
            this.tsmiAllNonValidateBooks.Text = "Все невалидные fb2";
            this.tsmiAllNonValidateBooks.Click += new System.EventHandler(this.TsmiAllNonValidateBooksClick);
            // 
            // tsmiAnalyzeForAllGroups
            // 
            this.tsmiAnalyzeForAllGroups.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAllOldBooksVersionValidateForAllGroups,
            this.toolStripMenuItem15,
            this.tsmiAllOldBooksCreationTimeForAllGroups,
            this.tsmiAllOldBooksLastWriteTimeForAllGroups,
            this.toolStripMenuItem6,
            this.tsmiAllNonValidateBooksForAllGroups});
            this.tsmiAnalyzeForAllGroups.Name = "tsmiAnalyzeForAllGroups";
            this.tsmiAnalyzeForAllGroups.Size = new System.Drawing.Size(503, 26);
            this.tsmiAnalyzeForAllGroups.Text = "Анализ для всех групп";
            // 
            // tsmiAllOldBooksVersionValidateForAllGroups
            // 
            this.tsmiAllOldBooksVersionValidateForAllGroups.Name = "tsmiAllOldBooksVersionValidateForAllGroups";
            this.tsmiAllOldBooksVersionValidateForAllGroups.Size = new System.Drawing.Size(396, 22);
            this.tsmiAllOldBooksVersionValidateForAllGroups.Text = "Все \"старые\" fb2 (по версии книги и невалидности)";
            this.tsmiAllOldBooksVersionValidateForAllGroups.Click += new System.EventHandler(this.TsmiAllOldBooksVersionValidateForAllGroupsClick);
            // 
            // toolStripMenuItem15
            // 
            this.toolStripMenuItem15.Name = "toolStripMenuItem15";
            this.toolStripMenuItem15.Size = new System.Drawing.Size(393, 6);
            // 
            // tsmiAllOldBooksCreationTimeForAllGroups
            // 
            this.tsmiAllOldBooksCreationTimeForAllGroups.Name = "tsmiAllOldBooksCreationTimeForAllGroups";
            this.tsmiAllOldBooksCreationTimeForAllGroups.Size = new System.Drawing.Size(396, 22);
            this.tsmiAllOldBooksCreationTimeForAllGroups.Text = "Все \"старые\" fb2 (по времени создания файла)";
            this.tsmiAllOldBooksCreationTimeForAllGroups.Click += new System.EventHandler(this.TsmiAllOldBooksCreationTimeForAllGroupsClick);
            // 
            // tsmiAllOldBooksLastWriteTimeForAllGroups
            // 
            this.tsmiAllOldBooksLastWriteTimeForAllGroups.Name = "tsmiAllOldBooksLastWriteTimeForAllGroups";
            this.tsmiAllOldBooksLastWriteTimeForAllGroups.Size = new System.Drawing.Size(396, 22);
            this.tsmiAllOldBooksLastWriteTimeForAllGroups.Text = "Все \"старые\" fb2 (по времени последнего измения файла)";
            this.tsmiAllOldBooksLastWriteTimeForAllGroups.Click += new System.EventHandler(this.TsmiAllOldBooksLastWriteTimeForAllGroupsClick);
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(393, 6);
            // 
            // tsmiAllNonValidateBooksForAllGroups
            // 
            this.tsmiAllNonValidateBooksForAllGroups.Name = "tsmiAllNonValidateBooksForAllGroups";
            this.tsmiAllNonValidateBooksForAllGroups.Size = new System.Drawing.Size(396, 22);
            this.tsmiAllNonValidateBooksForAllGroups.Text = "Все невалидные fb2";
            this.tsmiAllNonValidateBooksForAllGroups.Click += new System.EventHandler(this.TsmiAllNonValidateBooksForAllGroupsClick);
            // 
            // tsmi3
            // 
            this.tsmi3.Name = "tsmi3";
            this.tsmi3.Size = new System.Drawing.Size(500, 6);
            // 
            // tsmiValidate
            // 
            this.tsmiValidate.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFileReValidate,
            this.tsmiAllFilesInGroupReValidate,
            this.toolStripMenuItem7,
            this.tsmiAllGroupsReValidate});
            this.tsmiValidate.Image = ((System.Drawing.Image)(resources.GetObject("tsmiValidate.Image")));
            this.tsmiValidate.Name = "tsmiValidate";
            this.tsmiValidate.Size = new System.Drawing.Size(503, 26);
            this.tsmiValidate.Text = "Валидация";
            // 
            // tsmiFileReValidate
            // 
            this.tsmiFileReValidate.Name = "tsmiFileReValidate";
            this.tsmiFileReValidate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.tsmiFileReValidate.Size = new System.Drawing.Size(425, 22);
            this.tsmiFileReValidate.Text = "Проверить выделенную книгу на валидность";
            this.tsmiFileReValidate.Click += new System.EventHandler(this.TsmiFileReValidateClick);
            // 
            // tsmiAllFilesInGroupReValidate
            // 
            this.tsmiAllFilesInGroupReValidate.Name = "tsmiAllFilesInGroupReValidate";
            this.tsmiAllFilesInGroupReValidate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.V)));
            this.tsmiAllFilesInGroupReValidate.Size = new System.Drawing.Size(425, 22);
            this.tsmiAllFilesInGroupReValidate.Text = "Проверить все книги выделенной Группы на валидность";
            this.tsmiAllFilesInGroupReValidate.Click += new System.EventHandler(this.TsmiAllFilesInGroupReValidateClick);
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(422, 6);
            // 
            // tsmiAllGroupsReValidate
            // 
            this.tsmiAllGroupsReValidate.Name = "tsmiAllGroupsReValidate";
            this.tsmiAllGroupsReValidate.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.V)));
            this.tsmiAllGroupsReValidate.Size = new System.Drawing.Size(425, 22);
            this.tsmiAllGroupsReValidate.Text = "Проверить все книги всех Групп на валидность";
            this.tsmiAllGroupsReValidate.Click += new System.EventHandler(this.TsmiAllGroupsReValidateClick);
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAutoCorrectorForAllSelectedBooks,
            this.toolStripMenuItemAutoCorrectorForAllCheckedBooks,
            this.toolStripMenuItem12,
            this.tsmiAutoCorrectorForAllBooksFromGroup,
            this.toolStripMenuItem13,
            this.tsmiAutoCorrectorForAllBooksAllGroup});
            this.toolStripMenuItem11.Image = ((System.Drawing.Image)(resources.GetObject("toolStripMenuItem11.Image")));
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(503, 26);
            this.toolStripMenuItem11.Text = "Автокорректировка книг";
            // 
            // tsmiAutoCorrectorForAllSelectedBooks
            // 
            this.tsmiAutoCorrectorForAllSelectedBooks.Name = "tsmiAutoCorrectorForAllSelectedBooks";
            this.tsmiAutoCorrectorForAllSelectedBooks.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.tsmiAutoCorrectorForAllSelectedBooks.Size = new System.Drawing.Size(284, 22);
            this.tsmiAutoCorrectorForAllSelectedBooks.Text = "Для всех выделенных книг...";
            this.tsmiAutoCorrectorForAllSelectedBooks.Click += new System.EventHandler(this.TsmiAutoCorrectorForAllSelectedBooksClick);
            // 
            // toolStripMenuItemAutoCorrectorForAllCheckedBooks
            // 
            this.toolStripMenuItemAutoCorrectorForAllCheckedBooks.Name = "toolStripMenuItemAutoCorrectorForAllCheckedBooks";
            this.toolStripMenuItemAutoCorrectorForAllCheckedBooks.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.R)));
            this.toolStripMenuItemAutoCorrectorForAllCheckedBooks.Size = new System.Drawing.Size(284, 22);
            this.toolStripMenuItemAutoCorrectorForAllCheckedBooks.Text = "Для всех помеченных книг...";
            this.toolStripMenuItemAutoCorrectorForAllCheckedBooks.Click += new System.EventHandler(this.ToolStripMenuItemAutoCorrectorForAllCheckedBooksClick);
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(281, 6);
            // 
            // tsmiAutoCorrectorForAllBooksFromGroup
            // 
            this.tsmiAutoCorrectorForAllBooksFromGroup.Name = "tsmiAutoCorrectorForAllBooksFromGroup";
            this.tsmiAutoCorrectorForAllBooksFromGroup.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.R)));
            this.tsmiAutoCorrectorForAllBooksFromGroup.Size = new System.Drawing.Size(284, 22);
            this.tsmiAutoCorrectorForAllBooksFromGroup.Text = "Для всех книг Группы...";
            this.tsmiAutoCorrectorForAllBooksFromGroup.Click += new System.EventHandler(this.TsmiAutoCorrectorForAllBooksFromGroupClick);
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(281, 6);
            // 
            // tsmiAutoCorrectorForAllBooksAllGroup
            // 
            this.tsmiAutoCorrectorForAllBooksAllGroup.Name = "tsmiAutoCorrectorForAllBooksAllGroup";
            this.tsmiAutoCorrectorForAllBooksAllGroup.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.R)));
            this.tsmiAutoCorrectorForAllBooksAllGroup.Size = new System.Drawing.Size(284, 22);
            this.tsmiAutoCorrectorForAllBooksAllGroup.Text = "Для всех книг всех Групп...";
            this.tsmiAutoCorrectorForAllBooksAllGroup.Click += new System.EventHandler(this.TsmiAutoCorrectorForAllBooksAllGroupClick);
            // 
            // tsmiEditAuthors
            // 
            this.tsmiEditAuthors.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSetAuthorsForSelectedBooks,
            this.tsmiSetAuthorsForCheckedBooks});
            this.tsmiEditAuthors.Image = ((System.Drawing.Image)(resources.GetObject("tsmiEditAuthors.Image")));
            this.tsmiEditAuthors.Name = "tsmiEditAuthors";
            this.tsmiEditAuthors.Size = new System.Drawing.Size(503, 26);
            this.tsmiEditAuthors.Text = "Правка метаданных Авторов";
            // 
            // tsmiSetAuthorsForSelectedBooks
            // 
            this.tsmiSetAuthorsForSelectedBooks.Name = "tsmiSetAuthorsForSelectedBooks";
            this.tsmiSetAuthorsForSelectedBooks.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.tsmiSetAuthorsForSelectedBooks.Size = new System.Drawing.Size(395, 22);
            this.tsmiSetAuthorsForSelectedBooks.Text = "Правка метаданных Авторов для выделенных книг";
            this.tsmiSetAuthorsForSelectedBooks.Click += new System.EventHandler(this.TsmiSetAuthorsForSelectedBooksClick);
            // 
            // tsmiSetAuthorsForCheckedBooks
            // 
            this.tsmiSetAuthorsForCheckedBooks.Name = "tsmiSetAuthorsForCheckedBooks";
            this.tsmiSetAuthorsForCheckedBooks.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.A)));
            this.tsmiSetAuthorsForCheckedBooks.Size = new System.Drawing.Size(395, 22);
            this.tsmiSetAuthorsForCheckedBooks.Text = "Правка метаданных Авторов для помеченных книг";
            this.tsmiSetAuthorsForCheckedBooks.Click += new System.EventHandler(this.TsmiSetAuthorsClick);
            // 
            // tsmiEditGenres
            // 
            this.tsmiEditGenres.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSetGenresForSelectedBooks,
            this.tsmiSetGenresForCheckedBooks});
            this.tsmiEditGenres.Image = ((System.Drawing.Image)(resources.GetObject("tsmiEditGenres.Image")));
            this.tsmiEditGenres.Name = "tsmiEditGenres";
            this.tsmiEditGenres.Size = new System.Drawing.Size(503, 26);
            this.tsmiEditGenres.Text = "Правка Жанров";
            // 
            // tsmiSetGenresForSelectedBooks
            // 
            this.tsmiSetGenresForSelectedBooks.Name = "tsmiSetGenresForSelectedBooks";
            this.tsmiSetGenresForSelectedBooks.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
            this.tsmiSetGenresForSelectedBooks.Size = new System.Drawing.Size(324, 22);
            this.tsmiSetGenresForSelectedBooks.Text = "Правка Жанров для выделенных книг";
            this.tsmiSetGenresForSelectedBooks.Click += new System.EventHandler(this.TsmiSetGenresForSelectedBooksClick);
            // 
            // tsmiSetGenresForCheckedBooks
            // 
            this.tsmiSetGenresForCheckedBooks.Name = "tsmiSetGenresForCheckedBooks";
            this.tsmiSetGenresForCheckedBooks.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.G)));
            this.tsmiSetGenresForCheckedBooks.Size = new System.Drawing.Size(324, 22);
            this.tsmiSetGenresForCheckedBooks.Text = "Правка Жанров для помеченных книг";
            this.tsmiSetGenresForCheckedBooks.Click += new System.EventHandler(this.TsmiSetGenresClick);
            // 
            // tsmiNewID
            // 
            this.tsmiNewID.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiSetNewIDForAllSelectedBooks,
            this.tsmiSetNewIDForAllCheckedBooks,
            this.toolStripMenuItem4,
            this.tsmiSetNewIDForAllBooksFromGroup,
            this.toolStripMenuItem8,
            this.tsmiSetNewIDForAllBooksAllGroup});
            this.tsmiNewID.Name = "tsmiNewID";
            this.tsmiNewID.Size = new System.Drawing.Size(503, 26);
            this.tsmiNewID.Text = "Новый id книг(и)";
            // 
            // tsmiSetNewIDForAllSelectedBooks
            // 
            this.tsmiSetNewIDForAllSelectedBooks.Name = "tsmiSetNewIDForAllSelectedBooks";
            this.tsmiSetNewIDForAllSelectedBooks.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.tsmiSetNewIDForAllSelectedBooks.Size = new System.Drawing.Size(385, 22);
            this.tsmiSetNewIDForAllSelectedBooks.Text = "Новые Id для выделенных книг...";
            this.tsmiSetNewIDForAllSelectedBooks.Click += new System.EventHandler(this.TsmiSetNewIDForAllSelectedBooksClick);
            // 
            // tsmiSetNewIDForAllCheckedBooks
            // 
            this.tsmiSetNewIDForAllCheckedBooks.Name = "tsmiSetNewIDForAllCheckedBooks";
            this.tsmiSetNewIDForAllCheckedBooks.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.I)));
            this.tsmiSetNewIDForAllCheckedBooks.Size = new System.Drawing.Size(385, 22);
            this.tsmiSetNewIDForAllCheckedBooks.Text = "Новые Id для помеченных книг...";
            this.tsmiSetNewIDForAllCheckedBooks.Click += new System.EventHandler(this.TsmiSetNewIDForAllCheckedBooksClick);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(382, 6);
            // 
            // tsmiSetNewIDForAllBooksFromGroup
            // 
            this.tsmiSetNewIDForAllBooksFromGroup.Name = "tsmiSetNewIDForAllBooksFromGroup";
            this.tsmiSetNewIDForAllBooksFromGroup.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Alt) 
            | System.Windows.Forms.Keys.I)));
            this.tsmiSetNewIDForAllBooksFromGroup.Size = new System.Drawing.Size(385, 22);
            this.tsmiSetNewIDForAllBooksFromGroup.Text = "Новые id для всех книг выделенной Группы...";
            this.tsmiSetNewIDForAllBooksFromGroup.Click += new System.EventHandler(this.TsmiSetNewIDForAllBooksFromGroupClick);
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(382, 6);
            // 
            // tsmiSetNewIDForAllBooksAllGroup
            // 
            this.tsmiSetNewIDForAllBooksAllGroup.Name = "tsmiSetNewIDForAllBooksAllGroup";
            this.tsmiSetNewIDForAllBooksAllGroup.Size = new System.Drawing.Size(385, 22);
            this.tsmiSetNewIDForAllBooksAllGroup.Text = "Новые id для всех книг всех Групп...";
            this.tsmiSetNewIDForAllBooksAllGroup.Click += new System.EventHandler(this.TsmiSetNewIDForAllBooksAllGroupClick);
            // 
            // tsmiEditBookName
            // 
            this.tsmiEditBookName.Name = "tsmiEditBookName";
            this.tsmiEditBookName.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.tsmiEditBookName.Size = new System.Drawing.Size(503, 26);
            this.tsmiEditBookName.Text = "Правка Названия выделенной книги";
            this.tsmiEditBookName.Click += new System.EventHandler(this.TsmiEditBookNameClick);
            // 
            // tsmiEditDescription
            // 
            this.tsmiEditDescription.Image = ((System.Drawing.Image)(resources.GetObject("tsmiEditDescription.Image")));
            this.tsmiEditDescription.Name = "tsmiEditDescription";
            this.tsmiEditDescription.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
            this.tsmiEditDescription.Size = new System.Drawing.Size(503, 26);
            this.tsmiEditDescription.Text = "Правка Метаданных описания книги";
            this.tsmiEditDescription.Click += new System.EventHandler(this.TsmiEditDescriptionClick);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(500, 6);
            // 
            // tsmiEditInTextEditor
            // 
            this.tsmiEditInTextEditor.Image = ((System.Drawing.Image)(resources.GetObject("tsmiEditInTextEditor.Image")));
            this.tsmiEditInTextEditor.Name = "tsmiEditInTextEditor";
            this.tsmiEditInTextEditor.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.tsmiEditInTextEditor.Size = new System.Drawing.Size(503, 26);
            this.tsmiEditInTextEditor.Text = "Редактировать в текстовом редакторе";
            this.tsmiEditInTextEditor.Click += new System.EventHandler(this.TsmiEditInTextEditorClick);
            // 
            // tsmiEditInFB2Editor
            // 
            this.tsmiEditInFB2Editor.Image = ((System.Drawing.Image)(resources.GetObject("tsmiEditInFB2Editor.Image")));
            this.tsmiEditInFB2Editor.Name = "tsmiEditInFB2Editor";
            this.tsmiEditInFB2Editor.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.tsmiEditInFB2Editor.Size = new System.Drawing.Size(503, 26);
            this.tsmiEditInFB2Editor.Text = "Редактировать в fb2-редакторе";
            this.tsmiEditInFB2Editor.Click += new System.EventHandler(this.TsmiEditInFB2EditorClick);
            // 
            // tsmiDiffFB2
            // 
            this.tsmiDiffFB2.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDiffFB2.Image")));
            this.tsmiDiffFB2.Name = "tsmiDiffFB2";
            this.tsmiDiffFB2.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.F1)));
            this.tsmiDiffFB2.Size = new System.Drawing.Size(503, 26);
            this.tsmiDiffFB2.Text = "diff два помеченных (checked) файла в Группе";
            this.tsmiDiffFB2.Click += new System.EventHandler(this.TsmiDiffFB2Click);
            // 
            // tsmi1
            // 
            this.tsmi1.Name = "tsmi1";
            this.tsmi1.Size = new System.Drawing.Size(500, 6);
            // 
            // tsmiViewInReader
            // 
            this.tsmiViewInReader.Image = ((System.Drawing.Image)(resources.GetObject("tsmiViewInReader.Image")));
            this.tsmiViewInReader.Name = "tsmiViewInReader";
            this.tsmiViewInReader.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.tsmiViewInReader.Size = new System.Drawing.Size(503, 26);
            this.tsmiViewInReader.Text = "Запустить в fb2-читалке (Просмотр)";
            this.tsmiViewInReader.Click += new System.EventHandler(this.TsmiViewInReaderClick);
            // 
            // tsmi2
            // 
            this.tsmi2.Name = "tsmi2";
            this.tsmi2.Size = new System.Drawing.Size(500, 6);
            // 
            // tsmiCopyCheckedFb2To
            // 
            this.tsmiCopyCheckedFb2To.Image = ((System.Drawing.Image)(resources.GetObject("tsmiCopyCheckedFb2To.Image")));
            this.tsmiCopyCheckedFb2To.Name = "tsmiCopyCheckedFb2To";
            this.tsmiCopyCheckedFb2To.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.tsmiCopyCheckedFb2To.Size = new System.Drawing.Size(503, 26);
            this.tsmiCopyCheckedFb2To.Text = "Копировать помеченные книги...";
            this.tsmiCopyCheckedFb2To.Click += new System.EventHandler(this.TsmiCopyCheckedFb2ToClick);
            // 
            // tsmiMoveCheckedFb2To
            // 
            this.tsmiMoveCheckedFb2To.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiMoveCheckedFb2ToView,
            this.tsmiMoveCheckedFb2ToFast});
            this.tsmiMoveCheckedFb2To.Image = ((System.Drawing.Image)(resources.GetObject("tsmiMoveCheckedFb2To.Image")));
            this.tsmiMoveCheckedFb2To.Name = "tsmiMoveCheckedFb2To";
            this.tsmiMoveCheckedFb2To.Size = new System.Drawing.Size(503, 26);
            this.tsmiMoveCheckedFb2To.Text = "Переместить помеченные книги";
            // 
            // tsmiMoveCheckedFb2ToView
            // 
            this.tsmiMoveCheckedFb2ToView.Name = "tsmiMoveCheckedFb2ToView";
            this.tsmiMoveCheckedFb2ToView.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.tsmiMoveCheckedFb2ToView.Size = new System.Drawing.Size(371, 22);
            this.tsmiMoveCheckedFb2ToView.Text = "Отображая изменения в списке копий (медленно)";
            this.tsmiMoveCheckedFb2ToView.Click += new System.EventHandler(this.TsmiMoveCheckedFb2ToClick);
            // 
            // tsmiMoveCheckedFb2ToFast
            // 
            this.tsmiMoveCheckedFb2ToFast.Name = "tsmiMoveCheckedFb2ToFast";
            this.tsmiMoveCheckedFb2ToFast.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F6)));
            this.tsmiMoveCheckedFb2ToFast.Size = new System.Drawing.Size(371, 22);
            this.tsmiMoveCheckedFb2ToFast.Text = "Без отображения изменений (быстро)";
            this.tsmiMoveCheckedFb2ToFast.Click += new System.EventHandler(this.TsmiMoveCheckedFb2ToFastClick);
            // 
            // tsmiDeleteCheckedFb2
            // 
            this.tsmiDeleteCheckedFb2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiDeleteCheckedFb2View,
            this.tsmiDeleteCheckedFb2Fast});
            this.tsmiDeleteCheckedFb2.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDeleteCheckedFb2.Image")));
            this.tsmiDeleteCheckedFb2.Name = "tsmiDeleteCheckedFb2";
            this.tsmiDeleteCheckedFb2.Size = new System.Drawing.Size(503, 26);
            this.tsmiDeleteCheckedFb2.Text = "Удалить помеченные книги";
            // 
            // tsmiDeleteCheckedFb2View
            // 
            this.tsmiDeleteCheckedFb2View.Name = "tsmiDeleteCheckedFb2View";
            this.tsmiDeleteCheckedFb2View.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)));
            this.tsmiDeleteCheckedFb2View.Size = new System.Drawing.Size(403, 22);
            this.tsmiDeleteCheckedFb2View.Text = "Отображая изменения в списке копий (медленно)";
            this.tsmiDeleteCheckedFb2View.Click += new System.EventHandler(this.TsmiDeleteCheckedFb2Click);
            // 
            // tsmiDeleteCheckedFb2Fast
            // 
            this.tsmiDeleteCheckedFb2Fast.Name = "tsmiDeleteCheckedFb2Fast";
            this.tsmiDeleteCheckedFb2Fast.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Delete)));
            this.tsmiDeleteCheckedFb2Fast.Size = new System.Drawing.Size(403, 22);
            this.tsmiDeleteCheckedFb2Fast.Text = "Без отображения изменений (быстро)";
            this.tsmiDeleteCheckedFb2Fast.Click += new System.EventHandler(this.TsmiDeleteCheckedFb2FastClick);
            // 
            // tsmiOpenFileDir
            // 
            this.tsmiOpenFileDir.Image = ((System.Drawing.Image)(resources.GetObject("tsmiOpenFileDir.Image")));
            this.tsmiOpenFileDir.Name = "tsmiOpenFileDir";
            this.tsmiOpenFileDir.Size = new System.Drawing.Size(503, 26);
            this.tsmiOpenFileDir.Text = "Открыть папку для выделенного файла";
            this.tsmiOpenFileDir.Click += new System.EventHandler(this.TsmiOpenFileDirClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(500, 6);
            // 
            // tsmiDeleteAllItemForNonExistFile
            // 
            this.tsmiDeleteAllItemForNonExistFile.Name = "tsmiDeleteAllItemForNonExistFile";
            this.tsmiDeleteAllItemForNonExistFile.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.tsmiDeleteAllItemForNonExistFile.Size = new System.Drawing.Size(503, 26);
            this.tsmiDeleteAllItemForNonExistFile.Text = "Удалить все элементы Списка \"без файлов\" на диске...";
            this.tsmiDeleteAllItemForNonExistFile.Click += new System.EventHandler(this.TsmiDeleteAllItemForNonExistFileClick);
            // 
            // tsmiDeleteChechedItemsNotDeleteFiles
            // 
            this.tsmiDeleteChechedItemsNotDeleteFiles.Name = "tsmiDeleteChechedItemsNotDeleteFiles";
            this.tsmiDeleteChechedItemsNotDeleteFiles.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.tsmiDeleteChechedItemsNotDeleteFiles.Size = new System.Drawing.Size(503, 26);
            this.tsmiDeleteChechedItemsNotDeleteFiles.Text = "Удалить помеченные элементы Списка (файлы на диске НЕ удаляются)...";
            this.tsmiDeleteChechedItemsNotDeleteFiles.Click += new System.EventHandler(this.TsmiDeleteChechedItemsNotDeleteFilesClick);
            // 
            // tsmiDeleteGroupNotFile
            // 
            this.tsmiDeleteGroupNotFile.Name = "tsmiDeleteGroupNotFile";
            this.tsmiDeleteGroupNotFile.ShortcutKeys = System.Windows.Forms.Keys.F10;
            this.tsmiDeleteGroupNotFile.Size = new System.Drawing.Size(503, 26);
            this.tsmiDeleteGroupNotFile.Text = "Удалить помеченные Группы из Списка (на диске НЕ удаляются)...";
            this.tsmiDeleteGroupNotFile.Click += new System.EventHandler(this.TsmiDeleteGroupNotFileClick);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(500, 6);
            // 
            // tsmiCheckedAllInGroup
            // 
            this.tsmiCheckedAllInGroup.Image = ((System.Drawing.Image)(resources.GetObject("tsmiCheckedAllInGroup.Image")));
            this.tsmiCheckedAllInGroup.Name = "tsmiCheckedAllInGroup";
            this.tsmiCheckedAllInGroup.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.tsmiCheckedAllInGroup.Size = new System.Drawing.Size(503, 26);
            this.tsmiCheckedAllInGroup.Text = "Пометить все книги выбранной Группы...";
            this.tsmiCheckedAllInGroup.Click += new System.EventHandler(this.TsmiCheckedAllInGroupClick);
            // 
            // tsmiCheckedAll
            // 
            this.tsmiCheckedAll.Image = ((System.Drawing.Image)(resources.GetObject("tsmiCheckedAll.Image")));
            this.tsmiCheckedAll.Name = "tsmiCheckedAll";
            this.tsmiCheckedAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.tsmiCheckedAll.Size = new System.Drawing.Size(503, 26);
            this.tsmiCheckedAll.Text = "Пометить все книги всех Групп";
            this.tsmiCheckedAll.Click += new System.EventHandler(this.TsmiCheckedAllClick);
            // 
            // tsmiUnCheckedAll
            // 
            this.tsmiUnCheckedAll.Image = ((System.Drawing.Image)(resources.GetObject("tsmiUnCheckedAll.Image")));
            this.tsmiUnCheckedAll.Name = "tsmiUnCheckedAll";
            this.tsmiUnCheckedAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.U)));
            this.tsmiUnCheckedAll.Size = new System.Drawing.Size(503, 26);
            this.tsmiUnCheckedAll.Text = "Снять все отметки";
            this.tsmiUnCheckedAll.Click += new System.EventHandler(this.TsmiUnCheckedAllClick);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(500, 6);
            // 
            // tsmiSaveAllCheckedItemToFile
            // 
            this.tsmiSaveAllCheckedItemToFile.Image = ((System.Drawing.Image)(resources.GetObject("tsmiSaveAllCheckedItemToFile.Image")));
            this.tsmiSaveAllCheckedItemToFile.Name = "tsmiSaveAllCheckedItemToFile";
            this.tsmiSaveAllCheckedItemToFile.Size = new System.Drawing.Size(503, 26);
            this.tsmiSaveAllCheckedItemToFile.Text = "Сохранить в файл список путей ко всем помеченным книгам...";
            this.tsmiSaveAllCheckedItemToFile.Click += new System.EventHandler(this.TsmiSaveAllCheckedItemToFileClick);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(500, 6);
            // 
            // tsmiColumnsResultAutoReize
            // 
            this.tsmiColumnsResultAutoReize.Name = "tsmiColumnsResultAutoReize";
            this.tsmiColumnsResultAutoReize.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
            this.tsmiColumnsResultAutoReize.Size = new System.Drawing.Size(503, 26);
            this.tsmiColumnsResultAutoReize.Text = "Обновить авторазмер колонок списка копий";
            this.tsmiColumnsResultAutoReize.Click += new System.EventHandler(this.TsmiColumnsResultAutoReizeClick);
            // 
            // fbdScanDir
            // 
            this.fbdScanDir.Description = "Укажите папку для сканирования с fb2-файлами:";
            // 
            // sfdLoadList
            // 
            this.sfdLoadList.RestoreDirectory = true;
            this.sfdLoadList.Title = "Загрузка Списка копий книг";
            // 
            // imageListDup
            // 
            this.imageListDup.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListDup.ImageStream")));
            this.imageListDup.TransparentColor = System.Drawing.Color.Transparent;
            this.imageListDup.Images.SetKeyName(0, "cover_no.png");
            // 
            // fbdSaveList
            // 
            this.fbdSaveList.Description = "Укажите папку для файлов копий книг:";
            // 
            // sfdList
            // 
            this.sfdList.RestoreDirectory = true;
            this.sfdList.Title = "Укажите название файла копий";
            // 
            // tcDuplicator
            // 
            this.tcDuplicator.Controls.Add(this.tpDuplicator);
            this.tcDuplicator.Controls.Add(this.tpOptions);
            this.tcDuplicator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcDuplicator.Location = new System.Drawing.Point(0, 0);
            this.tcDuplicator.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tcDuplicator.Name = "tcDuplicator";
            this.tcDuplicator.SelectedIndex = 0;
            this.tcDuplicator.Size = new System.Drawing.Size(1123, 536);
            this.tcDuplicator.TabIndex = 20;
            // 
            // tpDuplicator
            // 
            this.tpDuplicator.Controls.Add(this.listViewFB2Files);
            this.tpDuplicator.Controls.Add(this.pInfo);
            this.tpDuplicator.Controls.Add(this.pMode);
            this.tpDuplicator.Controls.Add(this.pSearchFBDup2Dirs);
            this.tpDuplicator.Controls.Add(this.tsDup);
            this.tpDuplicator.Location = new System.Drawing.Point(4, 22);
            this.tpDuplicator.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tpDuplicator.Name = "tpDuplicator";
            this.tpDuplicator.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tpDuplicator.Size = new System.Drawing.Size(1115, 510);
            this.tpDuplicator.TabIndex = 0;
            this.tpDuplicator.Text = "Поиск копий книг";
            this.tpDuplicator.UseVisualStyleBackColor = true;
            // 
            // listViewFB2Files
            // 
            this.listViewFB2Files.AllowColumnReorder = true;
            this.listViewFB2Files.CheckBoxes = true;
            this.listViewFB2Files.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chPath,
            this.chBookTitle,
            this.chAuthors,
            this.chGenres,
            this.chLang,
            this.chID,
            this.chVersion,
            this.chEncodimg,
            this.chValidate,
            this.chFileLenght,
            this.chFileCreationTime,
            this.chFileLastWriteTime});
            this.listViewFB2Files.ContextMenuStrip = this.cmsFB2;
            this.listViewFB2Files.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewFB2Files.FullRowSelect = true;
            this.listViewFB2Files.HideSelection = false;
            this.listViewFB2Files.Location = new System.Drawing.Point(2, 92);
            this.listViewFB2Files.Name = "listViewFB2Files";
            this.listViewFB2Files.ShowItemToolTips = true;
            this.listViewFB2Files.Size = new System.Drawing.Size(1111, 196);
            this.listViewFB2Files.TabIndex = 51;
            this.listViewFB2Files.UseCompatibleStateImageBehavior = false;
            this.listViewFB2Files.View = System.Windows.Forms.View.Details;
            this.listViewFB2Files.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.LvResultColumnClick);
            this.listViewFB2Files.SelectedIndexChanged += new System.EventHandler(this.LvResultSelectedIndexChanged);
            this.listViewFB2Files.DoubleClick += new System.EventHandler(this.LvResultDoubleClick);
            this.listViewFB2Files.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.LvResultKeyPress);
            // 
            // chPath
            // 
            this.chPath.Tag = "Путь к книге";
            this.chPath.Text = "Путь к книге";
            this.chPath.Width = 300;
            // 
            // chBookTitle
            // 
            this.chBookTitle.Tag = "Название";
            this.chBookTitle.Text = "Название";
            this.chBookTitle.Width = 180;
            // 
            // chAuthors
            // 
            this.chAuthors.Tag = "Автор(ы)";
            this.chAuthors.Text = "Автор(ы)";
            this.chAuthors.Width = 180;
            // 
            // chGenres
            // 
            this.chGenres.Tag = "Жанр(ы)";
            this.chGenres.Text = "Жанр(ы)";
            this.chGenres.Width = 180;
            // 
            // chLang
            // 
            this.chLang.Tag = "Язык";
            this.chLang.Text = "Язык";
            this.chLang.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chLang.Width = 50;
            // 
            // chID
            // 
            this.chID.Tag = "ID";
            this.chID.Text = "ID";
            this.chID.Width = 200;
            // 
            // chVersion
            // 
            this.chVersion.Tag = "Версия";
            this.chVersion.Text = "Версия";
            this.chVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chVersion.Width = 50;
            // 
            // chEncodimg
            // 
            this.chEncodimg.Tag = "Кодировка";
            this.chEncodimg.Text = "Кодировка";
            this.chEncodimg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chEncodimg.Width = 90;
            // 
            // chValidate
            // 
            this.chValidate.Tag = "Валидность";
            this.chValidate.Text = "Валидность";
            this.chValidate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.chValidate.Width = 50;
            // 
            // chFileLenght
            // 
            this.chFileLenght.Tag = "Размер";
            this.chFileLenght.Text = "Размер";
            this.chFileLenght.Width = 90;
            // 
            // chFileCreationTime
            // 
            this.chFileCreationTime.Tag = "Дата создания";
            this.chFileCreationTime.Text = "Дата создания";
            this.chFileCreationTime.Width = 120;
            // 
            // chFileLastWriteTime
            // 
            this.chFileLastWriteTime.Tag = "Последнее изменение";
            this.chFileLastWriteTime.Text = "Последнее изменение";
            this.chFileLastWriteTime.Width = 120;
            // 
            // pInfo
            // 
            this.pInfo.Controls.Add(this.FB2InfoPanel);
            this.pInfo.Controls.Add(this.pFilesCount);
            this.pInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pInfo.Location = new System.Drawing.Point(2, 288);
            this.pInfo.Name = "pInfo";
            this.pInfo.Size = new System.Drawing.Size(1111, 220);
            this.pInfo.TabIndex = 50;
            // 
            // FB2InfoPanel
            // 
            this.FB2InfoPanel.Controls.Add(this.tcViewFB2Desc);
            this.FB2InfoPanel.Controls.Add(this.tcCovers);
            this.FB2InfoPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FB2InfoPanel.Location = new System.Drawing.Point(284, 0);
            this.FB2InfoPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.FB2InfoPanel.Name = "FB2InfoPanel";
            this.FB2InfoPanel.Size = new System.Drawing.Size(827, 220);
            this.FB2InfoPanel.TabIndex = 47;
            // 
            // tcViewFB2Desc
            // 
            this.tcViewFB2Desc.Controls.Add(this.tpTitleInfo);
            this.tcViewFB2Desc.Controls.Add(this.tpSourceTitleInfo);
            this.tcViewFB2Desc.Controls.Add(this.tpDocumentInfo);
            this.tcViewFB2Desc.Controls.Add(this.tpPublishInfo);
            this.tcViewFB2Desc.Controls.Add(this.tpCustomInfo);
            this.tcViewFB2Desc.Controls.Add(this.tpHistory);
            this.tcViewFB2Desc.Controls.Add(this.tpTIAnnotation);
            this.tcViewFB2Desc.Controls.Add(this.tpSTIAnnotation);
            this.tcViewFB2Desc.Controls.Add(this.tpValidate);
            this.tcViewFB2Desc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcViewFB2Desc.Location = new System.Drawing.Point(249, 0);
            this.tcViewFB2Desc.Name = "tcViewFB2Desc";
            this.tcViewFB2Desc.SelectedIndex = 0;
            this.tcViewFB2Desc.Size = new System.Drawing.Size(578, 220);
            this.tcViewFB2Desc.TabIndex = 48;
            this.tcViewFB2Desc.DoubleClick += new System.EventHandler(this.TcViewFB2DescDoubleClick);
            // 
            // tpTitleInfo
            // 
            this.tpTitleInfo.Controls.Add(this.lvTitleInfo);
            this.tpTitleInfo.Location = new System.Drawing.Point(4, 22);
            this.tpTitleInfo.Name = "tpTitleInfo";
            this.tpTitleInfo.Size = new System.Drawing.Size(570, 194);
            this.tpTitleInfo.TabIndex = 0;
            this.tpTitleInfo.Text = "Книга";
            this.tpTitleInfo.UseVisualStyleBackColor = true;
            // 
            // lvTitleInfo
            // 
            this.lvTitleInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader10});
            this.lvTitleInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvTitleInfo.FullRowSelect = true;
            this.lvTitleInfo.GridLines = true;
            this.lvTitleInfo.HideSelection = false;
            this.lvTitleInfo.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem103,
            listViewItem104,
            listViewItem105,
            listViewItem106,
            listViewItem107,
            listViewItem108,
            listViewItem109,
            listViewItem110,
            listViewItem111});
            this.lvTitleInfo.Location = new System.Drawing.Point(0, 0);
            this.lvTitleInfo.Name = "lvTitleInfo";
            this.lvTitleInfo.ShowItemToolTips = true;
            this.lvTitleInfo.Size = new System.Drawing.Size(570, 194);
            this.lvTitleInfo.TabIndex = 11;
            this.lvTitleInfo.UseCompatibleStateImageBehavior = false;
            this.lvTitleInfo.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "Тэги";
            this.columnHeader9.Width = 130;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "Значение";
            this.columnHeader10.Width = 600;
            // 
            // tpSourceTitleInfo
            // 
            this.tpSourceTitleInfo.Controls.Add(this.lvSourceTitleInfo);
            this.tpSourceTitleInfo.Location = new System.Drawing.Point(4, 22);
            this.tpSourceTitleInfo.Name = "tpSourceTitleInfo";
            this.tpSourceTitleInfo.Size = new System.Drawing.Size(571, 194);
            this.tpSourceTitleInfo.TabIndex = 1;
            this.tpSourceTitleInfo.Text = "Оригинал";
            this.tpSourceTitleInfo.UseVisualStyleBackColor = true;
            // 
            // lvSourceTitleInfo
            // 
            this.lvSourceTitleInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader17,
            this.columnHeader18});
            this.lvSourceTitleInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvSourceTitleInfo.FullRowSelect = true;
            this.lvSourceTitleInfo.GridLines = true;
            this.lvSourceTitleInfo.HideSelection = false;
            this.lvSourceTitleInfo.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem112,
            listViewItem113,
            listViewItem114,
            listViewItem115,
            listViewItem116,
            listViewItem117,
            listViewItem118,
            listViewItem119,
            listViewItem120,
            listViewItem121});
            this.lvSourceTitleInfo.Location = new System.Drawing.Point(0, 0);
            this.lvSourceTitleInfo.Name = "lvSourceTitleInfo";
            this.lvSourceTitleInfo.ShowItemToolTips = true;
            this.lvSourceTitleInfo.Size = new System.Drawing.Size(571, 194);
            this.lvSourceTitleInfo.TabIndex = 12;
            this.lvSourceTitleInfo.UseCompatibleStateImageBehavior = false;
            this.lvSourceTitleInfo.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader17
            // 
            this.columnHeader17.Text = "Тэги";
            this.columnHeader17.Width = 130;
            // 
            // columnHeader18
            // 
            this.columnHeader18.Text = "Значение";
            this.columnHeader18.Width = 600;
            // 
            // tpDocumentInfo
            // 
            this.tpDocumentInfo.Controls.Add(this.lvDocumentInfo);
            this.tpDocumentInfo.Location = new System.Drawing.Point(4, 22);
            this.tpDocumentInfo.Name = "tpDocumentInfo";
            this.tpDocumentInfo.Size = new System.Drawing.Size(571, 194);
            this.tpDocumentInfo.TabIndex = 2;
            this.tpDocumentInfo.Text = "FB2 документ";
            this.tpDocumentInfo.UseVisualStyleBackColor = true;
            // 
            // lvDocumentInfo
            // 
            this.lvDocumentInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader15,
            this.columnHeader16});
            this.lvDocumentInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvDocumentInfo.FullRowSelect = true;
            this.lvDocumentInfo.GridLines = true;
            this.lvDocumentInfo.HideSelection = false;
            this.lvDocumentInfo.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem122,
            listViewItem123,
            listViewItem124,
            listViewItem125,
            listViewItem126,
            listViewItem127,
            listViewItem128});
            this.lvDocumentInfo.Location = new System.Drawing.Point(0, 0);
            this.lvDocumentInfo.Name = "lvDocumentInfo";
            this.lvDocumentInfo.ShowItemToolTips = true;
            this.lvDocumentInfo.Size = new System.Drawing.Size(571, 194);
            this.lvDocumentInfo.TabIndex = 12;
            this.lvDocumentInfo.UseCompatibleStateImageBehavior = false;
            this.lvDocumentInfo.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "Тэги";
            this.columnHeader15.Width = 240;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "Значение";
            this.columnHeader16.Width = 340;
            // 
            // tpPublishInfo
            // 
            this.tpPublishInfo.Controls.Add(this.lvPublishInfo);
            this.tpPublishInfo.Location = new System.Drawing.Point(4, 22);
            this.tpPublishInfo.Name = "tpPublishInfo";
            this.tpPublishInfo.Size = new System.Drawing.Size(571, 194);
            this.tpPublishInfo.TabIndex = 3;
            this.tpPublishInfo.Text = "Бумажная книга";
            this.tpPublishInfo.UseVisualStyleBackColor = true;
            // 
            // lvPublishInfo
            // 
            this.lvPublishInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader13,
            this.columnHeader14});
            this.lvPublishInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvPublishInfo.FullRowSelect = true;
            this.lvPublishInfo.GridLines = true;
            this.lvPublishInfo.HideSelection = false;
            this.lvPublishInfo.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem129,
            listViewItem130,
            listViewItem131,
            listViewItem132,
            listViewItem133,
            listViewItem134});
            this.lvPublishInfo.Location = new System.Drawing.Point(0, 0);
            this.lvPublishInfo.Name = "lvPublishInfo";
            this.lvPublishInfo.ShowItemToolTips = true;
            this.lvPublishInfo.Size = new System.Drawing.Size(571, 194);
            this.lvPublishInfo.TabIndex = 12;
            this.lvPublishInfo.UseCompatibleStateImageBehavior = false;
            this.lvPublishInfo.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "Тэги";
            this.columnHeader13.Width = 120;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "Значение";
            this.columnHeader14.Width = 430;
            // 
            // tpCustomInfo
            // 
            this.tpCustomInfo.Controls.Add(this.lvCustomInfo);
            this.tpCustomInfo.Location = new System.Drawing.Point(4, 22);
            this.tpCustomInfo.Name = "tpCustomInfo";
            this.tpCustomInfo.Size = new System.Drawing.Size(571, 194);
            this.tpCustomInfo.TabIndex = 4;
            this.tpCustomInfo.Text = "Дополнительные данные";
            this.tpCustomInfo.UseVisualStyleBackColor = true;
            // 
            // lvCustomInfo
            // 
            this.lvCustomInfo.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader11,
            this.columnHeader12});
            this.lvCustomInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvCustomInfo.FullRowSelect = true;
            this.lvCustomInfo.GridLines = true;
            this.lvCustomInfo.HideSelection = false;
            this.lvCustomInfo.Location = new System.Drawing.Point(0, 0);
            this.lvCustomInfo.Name = "lvCustomInfo";
            this.lvCustomInfo.ShowItemToolTips = true;
            this.lvCustomInfo.Size = new System.Drawing.Size(571, 194);
            this.lvCustomInfo.TabIndex = 12;
            this.lvCustomInfo.UseCompatibleStateImageBehavior = false;
            this.lvCustomInfo.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "Тип";
            this.columnHeader11.Width = 150;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "Значение";
            this.columnHeader12.Width = 380;
            // 
            // tpHistory
            // 
            this.tpHistory.Controls.Add(this.rtbHistory);
            this.tpHistory.Location = new System.Drawing.Point(4, 22);
            this.tpHistory.Name = "tpHistory";
            this.tpHistory.Size = new System.Drawing.Size(571, 194);
            this.tpHistory.TabIndex = 5;
            this.tpHistory.Text = "История fb2 файла";
            this.tpHistory.UseVisualStyleBackColor = true;
            // 
            // rtbHistory
            // 
            this.rtbHistory.BackColor = System.Drawing.SystemColors.Window;
            this.rtbHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbHistory.Location = new System.Drawing.Point(0, 0);
            this.rtbHistory.Name = "rtbHistory";
            this.rtbHistory.ReadOnly = true;
            this.rtbHistory.Size = new System.Drawing.Size(571, 194);
            this.rtbHistory.TabIndex = 0;
            this.rtbHistory.Text = "";
            // 
            // tpTIAnnotation
            // 
            this.tpTIAnnotation.Controls.Add(this.rtbTIAnnotation);
            this.tpTIAnnotation.Location = new System.Drawing.Point(4, 22);
            this.tpTIAnnotation.Name = "tpTIAnnotation";
            this.tpTIAnnotation.Size = new System.Drawing.Size(571, 194);
            this.tpTIAnnotation.TabIndex = 6;
            this.tpTIAnnotation.Text = "Аннотация на книгу";
            this.tpTIAnnotation.UseVisualStyleBackColor = true;
            // 
            // rtbTIAnnotation
            // 
            this.rtbTIAnnotation.BackColor = System.Drawing.SystemColors.Window;
            this.rtbTIAnnotation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbTIAnnotation.Location = new System.Drawing.Point(0, 0);
            this.rtbTIAnnotation.Name = "rtbTIAnnotation";
            this.rtbTIAnnotation.ReadOnly = true;
            this.rtbTIAnnotation.Size = new System.Drawing.Size(571, 194);
            this.rtbTIAnnotation.TabIndex = 0;
            this.rtbTIAnnotation.Text = "";
            // 
            // tpSTIAnnotation
            // 
            this.tpSTIAnnotation.Controls.Add(this.rtbSTIAnnotation);
            this.tpSTIAnnotation.Location = new System.Drawing.Point(4, 22);
            this.tpSTIAnnotation.Name = "tpSTIAnnotation";
            this.tpSTIAnnotation.Size = new System.Drawing.Size(571, 194);
            this.tpSTIAnnotation.TabIndex = 8;
            this.tpSTIAnnotation.Text = "Аннотация оригинала";
            this.tpSTIAnnotation.UseVisualStyleBackColor = true;
            // 
            // rtbSTIAnnotation
            // 
            this.rtbSTIAnnotation.BackColor = System.Drawing.SystemColors.Window;
            this.rtbSTIAnnotation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbSTIAnnotation.Location = new System.Drawing.Point(0, 0);
            this.rtbSTIAnnotation.Name = "rtbSTIAnnotation";
            this.rtbSTIAnnotation.ReadOnly = true;
            this.rtbSTIAnnotation.Size = new System.Drawing.Size(571, 194);
            this.rtbSTIAnnotation.TabIndex = 1;
            this.rtbSTIAnnotation.Text = "";
            // 
            // tpValidate
            // 
            this.tpValidate.Controls.Add(this.tbValidate);
            this.tpValidate.Location = new System.Drawing.Point(4, 22);
            this.tpValidate.Name = "tpValidate";
            this.tpValidate.Size = new System.Drawing.Size(571, 194);
            this.tpValidate.TabIndex = 7;
            this.tpValidate.Text = "Валидность";
            this.tpValidate.UseVisualStyleBackColor = true;
            // 
            // tbValidate
            // 
            this.tbValidate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbValidate.Location = new System.Drawing.Point(0, 0);
            this.tbValidate.Multiline = true;
            this.tbValidate.Name = "tbValidate";
            this.tbValidate.ReadOnly = true;
            this.tbValidate.Size = new System.Drawing.Size(571, 194);
            this.tbValidate.TabIndex = 0;
            // 
            // tcCovers
            // 
            this.tcCovers.Controls.Add(this.tpTI);
            this.tcCovers.Controls.Add(this.tpSTI);
            this.tcCovers.Dock = System.Windows.Forms.DockStyle.Left;
            this.tcCovers.Location = new System.Drawing.Point(0, 0);
            this.tcCovers.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tcCovers.Name = "tcCovers";
            this.tcCovers.SelectedIndex = 0;
            this.tcCovers.Size = new System.Drawing.Size(249, 220);
            this.tcCovers.TabIndex = 47;
            this.tcCovers.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.TcCoversMouseDoubleClick);
            // 
            // tpTI
            // 
            this.tpTI.Controls.Add(this.TICoverPanel);
            this.tpTI.Controls.Add(this.TICoverListViewPanel);
            this.tpTI.Location = new System.Drawing.Point(4, 22);
            this.tpTI.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tpTI.Name = "tpTI";
            this.tpTI.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tpTI.Size = new System.Drawing.Size(241, 194);
            this.tpTI.TabIndex = 0;
            this.tpTI.Text = "Обложки Книги";
            this.tpTI.UseVisualStyleBackColor = true;
            // 
            // TICoverPanel
            // 
            this.TICoverPanel.Controls.Add(this.picBoxTICover);
            this.TICoverPanel.Controls.Add(this.TICoverInfoPanel);
            this.TICoverPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TICoverPanel.Location = new System.Drawing.Point(118, 2);
            this.TICoverPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TICoverPanel.Name = "TICoverPanel";
            this.TICoverPanel.Size = new System.Drawing.Size(121, 190);
            this.TICoverPanel.TabIndex = 99;
            // 
            // picBoxTICover
            // 
            this.picBoxTICover.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picBoxTICover.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBoxTICover.ErrorImage = ((System.Drawing.Image)(resources.GetObject("picBoxTICover.ErrorImage")));
            this.picBoxTICover.Location = new System.Drawing.Point(0, 52);
            this.picBoxTICover.Name = "picBoxTICover";
            this.picBoxTICover.Size = new System.Drawing.Size(121, 138);
            this.picBoxTICover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBoxTICover.TabIndex = 98;
            this.picBoxTICover.TabStop = false;
            // 
            // TICoverInfoPanel
            // 
            this.TICoverInfoPanel.Controls.Add(this.TICoverLenghtLabel);
            this.TICoverInfoPanel.Controls.Add(this.TICoverPixelsLabel);
            this.TICoverInfoPanel.Controls.Add(this.TICoverDPILabel);
            this.TICoverInfoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TICoverInfoPanel.Location = new System.Drawing.Point(0, 0);
            this.TICoverInfoPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TICoverInfoPanel.Name = "TICoverInfoPanel";
            this.TICoverInfoPanel.Size = new System.Drawing.Size(121, 52);
            this.TICoverInfoPanel.TabIndex = 97;
            // 
            // TICoverLenghtLabel
            // 
            this.TICoverLenghtLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TICoverLenghtLabel.Location = new System.Drawing.Point(0, 32);
            this.TICoverLenghtLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TICoverLenghtLabel.Name = "TICoverLenghtLabel";
            this.TICoverLenghtLabel.Size = new System.Drawing.Size(121, 16);
            this.TICoverLenghtLabel.TabIndex = 4;
            this.TICoverLenghtLabel.Text = "Размер";
            // 
            // TICoverPixelsLabel
            // 
            this.TICoverPixelsLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TICoverPixelsLabel.Location = new System.Drawing.Point(0, 16);
            this.TICoverPixelsLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TICoverPixelsLabel.Name = "TICoverPixelsLabel";
            this.TICoverPixelsLabel.Size = new System.Drawing.Size(121, 16);
            this.TICoverPixelsLabel.TabIndex = 3;
            this.TICoverPixelsLabel.Text = "В пикселах";
            // 
            // TICoverDPILabel
            // 
            this.TICoverDPILabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.TICoverDPILabel.Location = new System.Drawing.Point(0, 0);
            this.TICoverDPILabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.TICoverDPILabel.Name = "TICoverDPILabel";
            this.TICoverDPILabel.Size = new System.Drawing.Size(121, 16);
            this.TICoverDPILabel.TabIndex = 2;
            this.TICoverDPILabel.Text = "DPI";
            // 
            // TICoverListViewPanel
            // 
            this.TICoverListViewPanel.Controls.Add(this.TICoversListView);
            this.TICoverListViewPanel.Controls.Add(this.TICoverListViewButtonPanel);
            this.TICoverListViewPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.TICoverListViewPanel.Location = new System.Drawing.Point(2, 2);
            this.TICoverListViewPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TICoverListViewPanel.Name = "TICoverListViewPanel";
            this.TICoverListViewPanel.Size = new System.Drawing.Size(116, 190);
            this.TICoverListViewPanel.TabIndex = 96;
            // 
            // TICoversListView
            // 
            this.TICoversListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.TICoversListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TICoversListView.GridLines = true;
            this.TICoversListView.HideSelection = false;
            this.TICoversListView.Location = new System.Drawing.Point(0, 0);
            this.TICoversListView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TICoversListView.Name = "TICoversListView";
            this.TICoversListView.Size = new System.Drawing.Size(116, 159);
            this.TICoversListView.TabIndex = 99;
            this.TICoversListView.UseCompatibleStateImageBehavior = false;
            this.TICoversListView.View = System.Windows.Forms.View.Details;
            this.TICoversListView.SelectedIndexChanged += new System.EventHandler(this.TICoversListViewSelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Обложки";
            this.columnHeader1.Width = 100;
            // 
            // TICoverListViewButtonPanel
            // 
            this.TICoverListViewButtonPanel.Controls.Add(this.TISaveSelectedCoverButton);
            this.TICoverListViewButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.TICoverListViewButtonPanel.Enabled = false;
            this.TICoverListViewButtonPanel.Location = new System.Drawing.Point(0, 159);
            this.TICoverListViewButtonPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TICoverListViewButtonPanel.Name = "TICoverListViewButtonPanel";
            this.TICoverListViewButtonPanel.Size = new System.Drawing.Size(116, 31);
            this.TICoverListViewButtonPanel.TabIndex = 98;
            // 
            // TISaveSelectedCoverButton
            // 
            this.TISaveSelectedCoverButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.TISaveSelectedCoverButton.Image = ((System.Drawing.Image)(resources.GetObject("TISaveSelectedCoverButton.Image")));
            this.TISaveSelectedCoverButton.Location = new System.Drawing.Point(86, 0);
            this.TISaveSelectedCoverButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.TISaveSelectedCoverButton.Name = "TISaveSelectedCoverButton";
            this.TISaveSelectedCoverButton.Size = new System.Drawing.Size(30, 31);
            this.TISaveSelectedCoverButton.TabIndex = 0;
            this.TISaveSelectedCoverButton.UseVisualStyleBackColor = true;
            this.TISaveSelectedCoverButton.Click += new System.EventHandler(this.TISaveSelectedCoverButtonClick);
            // 
            // tpSTI
            // 
            this.tpSTI.Controls.Add(this.STICoverPanel);
            this.tpSTI.Controls.Add(this.STICoverListViewPanel);
            this.tpSTI.Location = new System.Drawing.Point(4, 22);
            this.tpSTI.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tpSTI.Name = "tpSTI";
            this.tpSTI.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tpSTI.Size = new System.Drawing.Size(241, 194);
            this.tpSTI.TabIndex = 1;
            this.tpSTI.Text = "Обложки Оригинала";
            this.tpSTI.UseVisualStyleBackColor = true;
            // 
            // STICoverPanel
            // 
            this.STICoverPanel.Controls.Add(this.picBoxSTICover);
            this.STICoverPanel.Controls.Add(this.STICoverInfoPanel);
            this.STICoverPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.STICoverPanel.Location = new System.Drawing.Point(118, 2);
            this.STICoverPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.STICoverPanel.Name = "STICoverPanel";
            this.STICoverPanel.Size = new System.Drawing.Size(121, 190);
            this.STICoverPanel.TabIndex = 100;
            // 
            // picBoxSTICover
            // 
            this.picBoxSTICover.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picBoxSTICover.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picBoxSTICover.ErrorImage = ((System.Drawing.Image)(resources.GetObject("picBoxSTICover.ErrorImage")));
            this.picBoxSTICover.Location = new System.Drawing.Point(0, 52);
            this.picBoxSTICover.Name = "picBoxSTICover";
            this.picBoxSTICover.Size = new System.Drawing.Size(121, 138);
            this.picBoxSTICover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picBoxSTICover.TabIndex = 100;
            this.picBoxSTICover.TabStop = false;
            // 
            // STICoverInfoPanel
            // 
            this.STICoverInfoPanel.Controls.Add(this.STICoverLenghtLabel);
            this.STICoverInfoPanel.Controls.Add(this.STICoverPixelsLabel);
            this.STICoverInfoPanel.Controls.Add(this.STICoverDPILabel);
            this.STICoverInfoPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.STICoverInfoPanel.Location = new System.Drawing.Point(0, 0);
            this.STICoverInfoPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.STICoverInfoPanel.Name = "STICoverInfoPanel";
            this.STICoverInfoPanel.Size = new System.Drawing.Size(121, 52);
            this.STICoverInfoPanel.TabIndex = 99;
            // 
            // STICoverLenghtLabel
            // 
            this.STICoverLenghtLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.STICoverLenghtLabel.Location = new System.Drawing.Point(0, 32);
            this.STICoverLenghtLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.STICoverLenghtLabel.Name = "STICoverLenghtLabel";
            this.STICoverLenghtLabel.Size = new System.Drawing.Size(121, 16);
            this.STICoverLenghtLabel.TabIndex = 7;
            this.STICoverLenghtLabel.Text = "Размер";
            // 
            // STICoverPixelsLabel
            // 
            this.STICoverPixelsLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.STICoverPixelsLabel.Location = new System.Drawing.Point(0, 16);
            this.STICoverPixelsLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.STICoverPixelsLabel.Name = "STICoverPixelsLabel";
            this.STICoverPixelsLabel.Size = new System.Drawing.Size(121, 16);
            this.STICoverPixelsLabel.TabIndex = 6;
            this.STICoverPixelsLabel.Text = "В пикселах";
            // 
            // STICoverDPILabel
            // 
            this.STICoverDPILabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.STICoverDPILabel.Location = new System.Drawing.Point(0, 0);
            this.STICoverDPILabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.STICoverDPILabel.Name = "STICoverDPILabel";
            this.STICoverDPILabel.Size = new System.Drawing.Size(121, 16);
            this.STICoverDPILabel.TabIndex = 5;
            this.STICoverDPILabel.Text = "DPI";
            // 
            // STICoverListViewPanel
            // 
            this.STICoverListViewPanel.Controls.Add(this.STICoversListView);
            this.STICoverListViewPanel.Controls.Add(this.STICoverListViewButtonPanel);
            this.STICoverListViewPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.STICoverListViewPanel.Location = new System.Drawing.Point(2, 2);
            this.STICoverListViewPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.STICoverListViewPanel.Name = "STICoverListViewPanel";
            this.STICoverListViewPanel.Size = new System.Drawing.Size(116, 190);
            this.STICoverListViewPanel.TabIndex = 97;
            // 
            // STICoversListView
            // 
            this.STICoversListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3});
            this.STICoversListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.STICoversListView.GridLines = true;
            this.STICoversListView.HideSelection = false;
            this.STICoversListView.Location = new System.Drawing.Point(0, 0);
            this.STICoversListView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.STICoversListView.Name = "STICoversListView";
            this.STICoversListView.Size = new System.Drawing.Size(116, 159);
            this.STICoversListView.TabIndex = 99;
            this.STICoversListView.UseCompatibleStateImageBehavior = false;
            this.STICoversListView.View = System.Windows.Forms.View.Details;
            this.STICoversListView.SelectedIndexChanged += new System.EventHandler(this.STICoversListViewSelectedIndexChanged);
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Обложки";
            this.columnHeader3.Width = 100;
            // 
            // STICoverListViewButtonPanel
            // 
            this.STICoverListViewButtonPanel.Controls.Add(this.STISaveSelectedCoverButton);
            this.STICoverListViewButtonPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.STICoverListViewButtonPanel.Enabled = false;
            this.STICoverListViewButtonPanel.Location = new System.Drawing.Point(0, 159);
            this.STICoverListViewButtonPanel.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.STICoverListViewButtonPanel.Name = "STICoverListViewButtonPanel";
            this.STICoverListViewButtonPanel.Size = new System.Drawing.Size(116, 31);
            this.STICoverListViewButtonPanel.TabIndex = 98;
            // 
            // STISaveSelectedCoverButton
            // 
            this.STISaveSelectedCoverButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.STISaveSelectedCoverButton.Image = ((System.Drawing.Image)(resources.GetObject("STISaveSelectedCoverButton.Image")));
            this.STISaveSelectedCoverButton.Location = new System.Drawing.Point(86, 0);
            this.STISaveSelectedCoverButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.STISaveSelectedCoverButton.Name = "STISaveSelectedCoverButton";
            this.STISaveSelectedCoverButton.Size = new System.Drawing.Size(30, 31);
            this.STISaveSelectedCoverButton.TabIndex = 0;
            this.STISaveSelectedCoverButton.UseVisualStyleBackColor = true;
            this.STISaveSelectedCoverButton.Click += new System.EventHandler(this.STISaveSelectedCoverButtonClick);
            // 
            // pFilesCount
            // 
            this.pFilesCount.Controls.Add(this.BadZipLabel1);
            this.pFilesCount.Controls.Add(this.ZipLabel);
            this.pFilesCount.Controls.Add(this.NotValidLabel);
            this.pFilesCount.Controls.Add(this.RazdelitLabel1);
            this.pFilesCount.Controls.Add(this.LegengCaptionLabel);
            this.pFilesCount.Controls.Add(this.lvFilesCount);
            this.pFilesCount.Controls.Add(this.CommandToolStrip);
            this.pFilesCount.Dock = System.Windows.Forms.DockStyle.Left;
            this.pFilesCount.Location = new System.Drawing.Point(0, 0);
            this.pFilesCount.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pFilesCount.Name = "pFilesCount";
            this.pFilesCount.Size = new System.Drawing.Size(284, 220);
            this.pFilesCount.TabIndex = 0;
            // 
            // BadZipLabel1
            // 
            this.BadZipLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.BadZipLabel1.ForeColor = System.Drawing.Color.Purple;
            this.BadZipLabel1.Location = new System.Drawing.Point(0, 192);
            this.BadZipLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.BadZipLabel1.Name = "BadZipLabel1";
            this.BadZipLabel1.Size = new System.Drawing.Size(284, 27);
            this.BadZipLabel1.TabIndex = 47;
            this.BadZipLabel1.Text = "Файлы, которые fb2 парсер не смог открыть";
            // 
            // ZipLabel
            // 
            this.ZipLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.ZipLabel.ForeColor = System.Drawing.Color.Green;
            this.ZipLabel.Location = new System.Drawing.Point(0, 173);
            this.ZipLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ZipLabel.Name = "ZipLabel";
            this.ZipLabel.Size = new System.Drawing.Size(284, 19);
            this.ZipLabel.TabIndex = 46;
            this.ZipLabel.Text = "Архивы книг (fb2.zip, fbz)";
            // 
            // NotValidLabel
            // 
            this.NotValidLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.NotValidLabel.ForeColor = System.Drawing.Color.Blue;
            this.NotValidLabel.Location = new System.Drawing.Point(0, 154);
            this.NotValidLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.NotValidLabel.Name = "NotValidLabel";
            this.NotValidLabel.Size = new System.Drawing.Size(284, 19);
            this.NotValidLabel.TabIndex = 45;
            this.NotValidLabel.Text = "Не валидные fb2, fb2.zip, fbz";
            // 
            // RazdelitLabel1
            // 
            this.RazdelitLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.RazdelitLabel1.Location = new System.Drawing.Point(0, 146);
            this.RazdelitLabel1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.RazdelitLabel1.Name = "RazdelitLabel1";
            this.RazdelitLabel1.Size = new System.Drawing.Size(284, 8);
            this.RazdelitLabel1.TabIndex = 44;
            this.RazdelitLabel1.Text = "   ";
            // 
            // LegengCaptionLabel
            // 
            this.LegengCaptionLabel.BackColor = System.Drawing.Color.Silver;
            this.LegengCaptionLabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LegengCaptionLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.LegengCaptionLabel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.LegengCaptionLabel.ForeColor = System.Drawing.Color.Red;
            this.LegengCaptionLabel.Location = new System.Drawing.Point(0, 127);
            this.LegengCaptionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LegengCaptionLabel.Name = "LegengCaptionLabel";
            this.LegengCaptionLabel.Size = new System.Drawing.Size(284, 19);
            this.LegengCaptionLabel.TabIndex = 43;
            this.LegengCaptionLabel.Text = "Легенда";
            this.LegengCaptionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lvFilesCount
            // 
            this.lvFilesCount.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader6,
            this.columnHeader7});
            this.lvFilesCount.Dock = System.Windows.Forms.DockStyle.Top;
            this.lvFilesCount.FullRowSelect = true;
            this.lvFilesCount.GridLines = true;
            this.lvFilesCount.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem135,
            listViewItem136});
            this.lvFilesCount.Location = new System.Drawing.Point(0, 27);
            this.lvFilesCount.Name = "lvFilesCount";
            this.lvFilesCount.Size = new System.Drawing.Size(284, 100);
            this.lvFilesCount.TabIndex = 42;
            this.lvFilesCount.UseCompatibleStateImageBehavior = false;
            this.lvFilesCount.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Группы и книги";
            this.columnHeader6.Width = 200;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "Кол-во";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader7.Width = 90;
            // 
            // CommandToolStrip
            // 
            this.CommandToolStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.CommandToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButtonEditInTextEditor,
            this.toolStripButtonEditInFB2Editor,
            this.toolStripButtonDiffFB2,
            this.toolStripButtonViewInReader,
            this.toolStripSeparator6,
            this.toolStripButtonCopyCheckedFb2To,
            this.toolStripDropDownButtonMoveCheckedFb2To,
            this.toolStripDropDownButtonDeleteCheckedFb2,
            this.toolStripSeparator7,
            this.toolStripButtonEditDescription,
            this.toolStripDropDownButtonNewID,
            this.toolStripDropDownButtonEditAuthors,
            this.toolStripDropDownButtonEditGenres,
            this.toolStripButtonEditBookName});
            this.CommandToolStrip.Location = new System.Drawing.Point(0, 0);
            this.CommandToolStrip.Name = "CommandToolStrip";
            this.CommandToolStrip.ShowItemToolTips = false;
            this.CommandToolStrip.Size = new System.Drawing.Size(284, 27);
            this.CommandToolStrip.TabIndex = 48;
            this.CommandToolStrip.Text = "toolStrip1";
            // 
            // toolStripButtonEditInTextEditor
            // 
            this.toolStripButtonEditInTextEditor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonEditInTextEditor.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonEditInTextEditor.Image")));
            this.toolStripButtonEditInTextEditor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEditInTextEditor.Name = "toolStripButtonEditInTextEditor";
            this.toolStripButtonEditInTextEditor.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonEditInTextEditor.Text = "Редактировать в текстовом редакторе";
            this.toolStripButtonEditInTextEditor.ToolTipText = "Редактировать в текстовом редакторе";
            this.toolStripButtonEditInTextEditor.Click += new System.EventHandler(this.TsmiEditInTextEditorClick);
            // 
            // toolStripButtonEditInFB2Editor
            // 
            this.toolStripButtonEditInFB2Editor.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonEditInFB2Editor.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonEditInFB2Editor.Image")));
            this.toolStripButtonEditInFB2Editor.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEditInFB2Editor.Name = "toolStripButtonEditInFB2Editor";
            this.toolStripButtonEditInFB2Editor.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonEditInFB2Editor.Text = "Редактировать в fb2-редакторе";
            this.toolStripButtonEditInFB2Editor.Click += new System.EventHandler(this.TsmiEditInFB2EditorClick);
            // 
            // toolStripButtonDiffFB2
            // 
            this.toolStripButtonDiffFB2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonDiffFB2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonDiffFB2.Image")));
            this.toolStripButtonDiffFB2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonDiffFB2.Name = "toolStripButtonDiffFB2";
            this.toolStripButtonDiffFB2.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonDiffFB2.Text = "diff два помеченных (checked) файла в Группе";
            this.toolStripButtonDiffFB2.Click += new System.EventHandler(this.TsmiDiffFB2Click);
            // 
            // toolStripButtonViewInReader
            // 
            this.toolStripButtonViewInReader.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonViewInReader.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonViewInReader.Image")));
            this.toolStripButtonViewInReader.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonViewInReader.Name = "toolStripButtonViewInReader";
            this.toolStripButtonViewInReader.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonViewInReader.Text = "Запустить в fb2-читалке (Просмотр)";
            this.toolStripButtonViewInReader.Click += new System.EventHandler(this.TsmiViewInReaderClick);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButtonCopyCheckedFb2To
            // 
            this.toolStripButtonCopyCheckedFb2To.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonCopyCheckedFb2To.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonCopyCheckedFb2To.Image")));
            this.toolStripButtonCopyCheckedFb2To.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonCopyCheckedFb2To.Name = "toolStripButtonCopyCheckedFb2To";
            this.toolStripButtonCopyCheckedFb2To.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonCopyCheckedFb2To.Text = "Копировать помеченные книги...";
            this.toolStripButtonCopyCheckedFb2To.Click += new System.EventHandler(this.TsmiCopyCheckedFb2ToClick);
            // 
            // toolStripDropDownButtonMoveCheckedFb2To
            // 
            this.toolStripDropDownButtonMoveCheckedFb2To.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButtonMoveCheckedFb2To.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItemMoveCheckedFb2ToView,
            this.toolStripMenuItemMoveCheckedFb2ToFast});
            this.toolStripDropDownButtonMoveCheckedFb2To.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonMoveCheckedFb2To.Image")));
            this.toolStripDropDownButtonMoveCheckedFb2To.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonMoveCheckedFb2To.Name = "toolStripDropDownButtonMoveCheckedFb2To";
            this.toolStripDropDownButtonMoveCheckedFb2To.Size = new System.Drawing.Size(33, 24);
            this.toolStripDropDownButtonMoveCheckedFb2To.Text = "Переместить помеченные книги";
            // 
            // toolStripMenuItemMoveCheckedFb2ToView
            // 
            this.toolStripMenuItemMoveCheckedFb2ToView.Name = "toolStripMenuItemMoveCheckedFb2ToView";
            this.toolStripMenuItemMoveCheckedFb2ToView.Size = new System.Drawing.Size(352, 22);
            this.toolStripMenuItemMoveCheckedFb2ToView.Text = "Отображая изменения в списке копий (медленно)";
            this.toolStripMenuItemMoveCheckedFb2ToView.ToolTipText = "Отображая изменения в списке копий (медленно)";
            this.toolStripMenuItemMoveCheckedFb2ToView.Click += new System.EventHandler(this.TsmiMoveCheckedFb2ToClick);
            // 
            // toolStripMenuItemMoveCheckedFb2ToFast
            // 
            this.toolStripMenuItemMoveCheckedFb2ToFast.Name = "toolStripMenuItemMoveCheckedFb2ToFast";
            this.toolStripMenuItemMoveCheckedFb2ToFast.Size = new System.Drawing.Size(352, 22);
            this.toolStripMenuItemMoveCheckedFb2ToFast.Text = "Без отображения изменений (быстро)";
            this.toolStripMenuItemMoveCheckedFb2ToFast.Click += new System.EventHandler(this.TsmiMoveCheckedFb2ToFastClick);
            // 
            // toolStripDropDownButtonDeleteCheckedFb2
            // 
            this.toolStripDropDownButtonDeleteCheckedFb2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButtonDeleteCheckedFb2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemDeleteCheckedFb2View,
            this.ToolStripMenuItemDeleteCheckedFb2Fast});
            this.toolStripDropDownButtonDeleteCheckedFb2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonDeleteCheckedFb2.Image")));
            this.toolStripDropDownButtonDeleteCheckedFb2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonDeleteCheckedFb2.Name = "toolStripDropDownButtonDeleteCheckedFb2";
            this.toolStripDropDownButtonDeleteCheckedFb2.Size = new System.Drawing.Size(33, 24);
            this.toolStripDropDownButtonDeleteCheckedFb2.Text = "Удалить помеченные книги";
            // 
            // ToolStripMenuItemDeleteCheckedFb2View
            // 
            this.ToolStripMenuItemDeleteCheckedFb2View.Name = "ToolStripMenuItemDeleteCheckedFb2View";
            this.ToolStripMenuItemDeleteCheckedFb2View.Size = new System.Drawing.Size(352, 22);
            this.ToolStripMenuItemDeleteCheckedFb2View.Text = "Отображая изменения в списке копий (медленно)";
            this.ToolStripMenuItemDeleteCheckedFb2View.Click += new System.EventHandler(this.TsmiDeleteCheckedFb2Click);
            // 
            // ToolStripMenuItemDeleteCheckedFb2Fast
            // 
            this.ToolStripMenuItemDeleteCheckedFb2Fast.Name = "ToolStripMenuItemDeleteCheckedFb2Fast";
            this.ToolStripMenuItemDeleteCheckedFb2Fast.Size = new System.Drawing.Size(352, 22);
            this.ToolStripMenuItemDeleteCheckedFb2Fast.Text = "Без отображения изменений (быстро)";
            this.ToolStripMenuItemDeleteCheckedFb2Fast.Click += new System.EventHandler(this.TsmiDeleteCheckedFb2FastClick);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 27);
            // 
            // toolStripButtonEditDescription
            // 
            this.toolStripButtonEditDescription.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButtonEditDescription.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonEditDescription.Image")));
            this.toolStripButtonEditDescription.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEditDescription.Name = "toolStripButtonEditDescription";
            this.toolStripButtonEditDescription.Size = new System.Drawing.Size(24, 24);
            this.toolStripButtonEditDescription.Text = "Правка метаданных описания книги";
            this.toolStripButtonEditDescription.Click += new System.EventHandler(this.TsmiEditDescriptionClick);
            // 
            // toolStripDropDownButtonNewID
            // 
            this.toolStripDropDownButtonNewID.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButtonNewID.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemSetNewIDForAllSelectedBooks,
            this.ToolStripMenuItemSetNewIDForAllCheckedBooks,
            this.toolStripMenuItem9,
            this.ToolStripMenuItemSetNewIDForAllBooksFromGroup,
            this.toolStripMenuItem10,
            this.ToolStripMenuItemSetNewIDForAllBooksAllGroup});
            this.toolStripDropDownButtonNewID.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripDropDownButtonNewID.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonNewID.Image")));
            this.toolStripDropDownButtonNewID.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonNewID.Name = "toolStripDropDownButtonNewID";
            this.toolStripDropDownButtonNewID.Size = new System.Drawing.Size(33, 24);
            this.toolStripDropDownButtonNewID.Text = "ID";
            this.toolStripDropDownButtonNewID.ToolTipText = "Новый id книг(и)";
            // 
            // ToolStripMenuItemSetNewIDForAllSelectedBooks
            // 
            this.ToolStripMenuItemSetNewIDForAllSelectedBooks.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ToolStripMenuItemSetNewIDForAllSelectedBooks.Name = "ToolStripMenuItemSetNewIDForAllSelectedBooks";
            this.ToolStripMenuItemSetNewIDForAllSelectedBooks.Size = new System.Drawing.Size(325, 22);
            this.ToolStripMenuItemSetNewIDForAllSelectedBooks.Text = "Новые Id для выделенных книг...";
            this.ToolStripMenuItemSetNewIDForAllSelectedBooks.Click += new System.EventHandler(this.TsmiSetNewIDForAllSelectedBooksClick);
            // 
            // ToolStripMenuItemSetNewIDForAllCheckedBooks
            // 
            this.ToolStripMenuItemSetNewIDForAllCheckedBooks.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ToolStripMenuItemSetNewIDForAllCheckedBooks.Name = "ToolStripMenuItemSetNewIDForAllCheckedBooks";
            this.ToolStripMenuItemSetNewIDForAllCheckedBooks.Size = new System.Drawing.Size(325, 22);
            this.ToolStripMenuItemSetNewIDForAllCheckedBooks.Text = "Новые Id для помеченных книг...";
            this.ToolStripMenuItemSetNewIDForAllCheckedBooks.Click += new System.EventHandler(this.TsmiSetNewIDForAllCheckedBooksClick);
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(322, 6);
            // 
            // ToolStripMenuItemSetNewIDForAllBooksFromGroup
            // 
            this.ToolStripMenuItemSetNewIDForAllBooksFromGroup.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ToolStripMenuItemSetNewIDForAllBooksFromGroup.Name = "ToolStripMenuItemSetNewIDForAllBooksFromGroup";
            this.ToolStripMenuItemSetNewIDForAllBooksFromGroup.Size = new System.Drawing.Size(325, 22);
            this.ToolStripMenuItemSetNewIDForAllBooksFromGroup.Text = "Новые id для всех книг выделенной Группы...";
            this.ToolStripMenuItemSetNewIDForAllBooksFromGroup.Click += new System.EventHandler(this.TsmiSetNewIDForAllBooksFromGroupClick);
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(322, 6);
            // 
            // ToolStripMenuItemSetNewIDForAllBooksAllGroup
            // 
            this.ToolStripMenuItemSetNewIDForAllBooksAllGroup.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ToolStripMenuItemSetNewIDForAllBooksAllGroup.Name = "ToolStripMenuItemSetNewIDForAllBooksAllGroup";
            this.ToolStripMenuItemSetNewIDForAllBooksAllGroup.Size = new System.Drawing.Size(325, 22);
            this.ToolStripMenuItemSetNewIDForAllBooksAllGroup.Text = "Новые id для всех книг всех Групп...";
            this.ToolStripMenuItemSetNewIDForAllBooksAllGroup.Click += new System.EventHandler(this.TsmiSetNewIDForAllBooksAllGroupClick);
            // 
            // toolStripDropDownButtonEditAuthors
            // 
            this.toolStripDropDownButtonEditAuthors.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButtonEditAuthors.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemSetAuthorsForSelectedBooks,
            this.ToolStripMenuItemSetAuthorsForCheckedBooks});
            this.toolStripDropDownButtonEditAuthors.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonEditAuthors.Image")));
            this.toolStripDropDownButtonEditAuthors.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonEditAuthors.Name = "toolStripDropDownButtonEditAuthors";
            this.toolStripDropDownButtonEditAuthors.Size = new System.Drawing.Size(33, 24);
            this.toolStripDropDownButtonEditAuthors.Text = "Правка метаданных Авторов";
            // 
            // ToolStripMenuItemSetAuthorsForSelectedBooks
            // 
            this.ToolStripMenuItemSetAuthorsForSelectedBooks.Name = "ToolStripMenuItemSetAuthorsForSelectedBooks";
            this.ToolStripMenuItemSetAuthorsForSelectedBooks.Size = new System.Drawing.Size(355, 22);
            this.ToolStripMenuItemSetAuthorsForSelectedBooks.Text = "Правка метаданных Авторов для выделенных книг";
            this.ToolStripMenuItemSetAuthorsForSelectedBooks.Click += new System.EventHandler(this.TsmiSetAuthorsForSelectedBooksClick);
            // 
            // ToolStripMenuItemSetAuthorsForCheckedBooks
            // 
            this.ToolStripMenuItemSetAuthorsForCheckedBooks.Name = "ToolStripMenuItemSetAuthorsForCheckedBooks";
            this.ToolStripMenuItemSetAuthorsForCheckedBooks.Size = new System.Drawing.Size(355, 22);
            this.ToolStripMenuItemSetAuthorsForCheckedBooks.Text = "Правка метаданных Авторов для помеченных книг";
            this.ToolStripMenuItemSetAuthorsForCheckedBooks.Click += new System.EventHandler(this.TsmiSetAuthorsClick);
            // 
            // toolStripDropDownButtonEditGenres
            // 
            this.toolStripDropDownButtonEditGenres.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButtonEditGenres.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripMenuItemSetGenresForSelectedBooks,
            this.ToolStripMenuItemSetGenresForCheckedBooks});
            this.toolStripDropDownButtonEditGenres.Image = ((System.Drawing.Image)(resources.GetObject("toolStripDropDownButtonEditGenres.Image")));
            this.toolStripDropDownButtonEditGenres.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButtonEditGenres.Name = "toolStripDropDownButtonEditGenres";
            this.toolStripDropDownButtonEditGenres.Size = new System.Drawing.Size(33, 24);
            this.toolStripDropDownButtonEditGenres.Text = "Правка Жанров";
            // 
            // ToolStripMenuItemSetGenresForSelectedBooks
            // 
            this.ToolStripMenuItemSetGenresForSelectedBooks.Name = "ToolStripMenuItemSetGenresForSelectedBooks";
            this.ToolStripMenuItemSetGenresForSelectedBooks.Size = new System.Drawing.Size(284, 22);
            this.ToolStripMenuItemSetGenresForSelectedBooks.Text = "Правка Жанров для выделенных книг";
            this.ToolStripMenuItemSetGenresForSelectedBooks.Click += new System.EventHandler(this.TsmiSetGenresForSelectedBooksClick);
            // 
            // ToolStripMenuItemSetGenresForCheckedBooks
            // 
            this.ToolStripMenuItemSetGenresForCheckedBooks.Name = "ToolStripMenuItemSetGenresForCheckedBooks";
            this.ToolStripMenuItemSetGenresForCheckedBooks.Size = new System.Drawing.Size(284, 22);
            this.ToolStripMenuItemSetGenresForCheckedBooks.Text = "Правка Жанров для помеченных книг";
            this.ToolStripMenuItemSetGenresForCheckedBooks.Click += new System.EventHandler(this.TsmiSetGenresClick);
            // 
            // toolStripButtonEditBookName
            // 
            this.toolStripButtonEditBookName.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripButtonEditBookName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripButtonEditBookName.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButtonEditBookName.Image")));
            this.toolStripButtonEditBookName.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButtonEditBookName.Name = "toolStripButtonEditBookName";
            this.toolStripButtonEditBookName.Size = new System.Drawing.Size(26, 24);
            this.toolStripButtonEditBookName.Text = "BT";
            this.toolStripButtonEditBookName.Click += new System.EventHandler(this.TsmiEditBookNameClick);
            // 
            // pMode
            // 
            this.pMode.Controls.Add(this.checkBoxSaveGroupsToXml);
            this.pMode.Controls.Add(this.cboxMode);
            this.pMode.Controls.Add(this.lblMode);
            this.pMode.Dock = System.Windows.Forms.DockStyle.Top;
            this.pMode.Location = new System.Drawing.Point(2, 66);
            this.pMode.Name = "pMode";
            this.pMode.Size = new System.Drawing.Size(1111, 26);
            this.pMode.TabIndex = 48;
            // 
            // checkBoxSaveGroupsToXml
            // 
            this.checkBoxSaveGroupsToXml.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkBoxSaveGroupsToXml.Checked = true;
            this.checkBoxSaveGroupsToXml.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxSaveGroupsToXml.ForeColor = System.Drawing.Color.Blue;
            this.checkBoxSaveGroupsToXml.Location = new System.Drawing.Point(802, 4);
            this.checkBoxSaveGroupsToXml.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.checkBoxSaveGroupsToXml.Name = "checkBoxSaveGroupsToXml";
            this.checkBoxSaveGroupsToXml.Size = new System.Drawing.Size(295, 20);
            this.checkBoxSaveGroupsToXml.TabIndex = 18;
            this.checkBoxSaveGroupsToXml.Text = "Результат - в xml без построения дерева";
            this.checkBoxSaveGroupsToXml.UseVisualStyleBackColor = true;
            this.checkBoxSaveGroupsToXml.CheckStateChanged += new System.EventHandler(this.CheckBoxSaveGroupsToXmlCheckStateChanged);
            // 
            // cboxMode
            // 
            this.cboxMode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboxMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxMode.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.cboxMode.FormattingEnabled = true;
            this.cboxMode.Items.AddRange(new object[] {
            "0. Абсолютно одинаковые книги (md5)",
            "1. Одинаковый Id Книги (копии и/или разные версии правки одной и той же книги)",
            "2. Название Книги (могут быть найдены и разные книги разных Авторов, но с одинако" +
                "вым Названием)",
            "3. Название Книги и Автор(ы)  (одна и та же книга, сделанная разными людьми - раз" +
                "ные Id, но Автор и Название - одинаковые)",
            "4. Авторы с одинаковой Фамилией и инициалами",
            "5. Название Книги, Автор(ы) и Одинаковый Id Книги (разделять по разным группам ра" +
                "зные издания книг)",
            "6. Название Книги и Id Книги (Авторы книги могут быть разными)",
            "7. Название Книги, Автор(ы) и Автор fb2 файла (одна и та же книга, сделанная разн" +
                "ыми людьми)",
            "8. Название Книги, Автор(ы), Id Книги и Автор fb2 файла",
            "9. Название Книги, Id Книги и Автор fb2 файла",
            "10. Автор(ы), Id Книги и Автор fb2 файла"});
            this.cboxMode.Location = new System.Drawing.Point(158, 1);
            this.cboxMode.Name = "cboxMode";
            this.cboxMode.Size = new System.Drawing.Size(639, 21);
            this.cboxMode.TabIndex = 17;
            this.cboxMode.SelectedIndexChanged += new System.EventHandler(this.CboxModeSelectedIndexChanged);
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
            // pSearchFBDup2Dirs
            // 
            this.pSearchFBDup2Dirs.AutoSize = true;
            this.pSearchFBDup2Dirs.Controls.Add(this.btnOpenDir);
            this.pSearchFBDup2Dirs.Controls.Add(this.chBoxScanSubDir);
            this.pSearchFBDup2Dirs.Controls.Add(this.tboxSourceDir);
            this.pSearchFBDup2Dirs.Controls.Add(this.lblScanDir);
            this.pSearchFBDup2Dirs.Dock = System.Windows.Forms.DockStyle.Top;
            this.pSearchFBDup2Dirs.Location = new System.Drawing.Point(2, 33);
            this.pSearchFBDup2Dirs.Name = "pSearchFBDup2Dirs";
            this.pSearchFBDup2Dirs.Size = new System.Drawing.Size(1111, 33);
            this.pSearchFBDup2Dirs.TabIndex = 47;
            // 
            // btnOpenDir
            // 
            this.btnOpenDir.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenDir.Image")));
            this.btnOpenDir.Location = new System.Drawing.Point(158, 3);
            this.btnOpenDir.Name = "btnOpenDir";
            this.btnOpenDir.Size = new System.Drawing.Size(37, 27);
            this.btnOpenDir.TabIndex = 21;
            this.btnOpenDir.UseVisualStyleBackColor = true;
            this.btnOpenDir.Click += new System.EventHandler(this.BtnOpenDirClick);
            // 
            // chBoxScanSubDir
            // 
            this.chBoxScanSubDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chBoxScanSubDir.Checked = true;
            this.chBoxScanSubDir.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chBoxScanSubDir.Font = new System.Drawing.Font("Tahoma", 8F);
            this.chBoxScanSubDir.ForeColor = System.Drawing.Color.Navy;
            this.chBoxScanSubDir.Location = new System.Drawing.Point(802, 4);
            this.chBoxScanSubDir.Name = "chBoxScanSubDir";
            this.chBoxScanSubDir.Size = new System.Drawing.Size(295, 24);
            this.chBoxScanSubDir.TabIndex = 2;
            this.chBoxScanSubDir.Text = "Сканировать подпапки";
            this.chBoxScanSubDir.UseVisualStyleBackColor = true;
            this.chBoxScanSubDir.CheckStateChanged += new System.EventHandler(this.ChBoxScanSubDirCheckStateChanged);
            // 
            // tboxSourceDir
            // 
            this.tboxSourceDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tboxSourceDir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tboxSourceDir.Location = new System.Drawing.Point(200, 6);
            this.tboxSourceDir.Name = "tboxSourceDir";
            this.tboxSourceDir.Size = new System.Drawing.Size(597, 21);
            this.tboxSourceDir.TabIndex = 1;
            this.tboxSourceDir.TextChanged += new System.EventHandler(this.TboxSourceDirTextChanged);
            this.tboxSourceDir.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TboxSourceDirKeyPress);
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
            // tsDup
            // 
            this.tsDup.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.tsDup.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnSearchDubls,
            this.tsbtnSearchFb2DupRenew,
            this.toolStripSeparator3,
            this.tslGroupCountForList,
            this.tscbGroupCountForList,
            this.tsbtnDupSaveList,
            this.toolStripSeparator2,
            this.tsbtnDupOpenList,
            this.tsbtnDupCurrentSaveList});
            this.tsDup.Location = new System.Drawing.Point(2, 2);
            this.tsDup.Name = "tsDup";
            this.tsDup.Size = new System.Drawing.Size(1111, 31);
            this.tsDup.TabIndex = 46;
            // 
            // tsbtnSearchDubls
            // 
            this.tsbtnSearchDubls.AutoToolTip = false;
            this.tsbtnSearchDubls.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSearchDubls.Image")));
            this.tsbtnSearchDubls.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSearchDubls.Name = "tsbtnSearchDubls";
            this.tsbtnSearchDubls.Size = new System.Drawing.Size(107, 28);
            this.tsbtnSearchDubls.Text = "Поиск копий";
            this.tsbtnSearchDubls.Click += new System.EventHandler(this.TsbtnSearchDublsClick);
            // 
            // tsbtnSearchFb2DupRenew
            // 
            this.tsbtnSearchFb2DupRenew.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSearchFb2DupRenew.Image")));
            this.tsbtnSearchFb2DupRenew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnSearchFb2DupRenew.Name = "tsbtnSearchFb2DupRenew";
            this.tsbtnSearchFb2DupRenew.Size = new System.Drawing.Size(204, 28);
            this.tsbtnSearchFb2DupRenew.Text = "Возобновить поиск из файла...";
            this.tsbtnSearchFb2DupRenew.Click += new System.EventHandler(this.TsbtnSearchFb2DupRenewClick);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
            // 
            // tslGroupCountForList
            // 
            this.tslGroupCountForList.Name = "tslGroupCountForList";
            this.tslGroupCountForList.Size = new System.Drawing.Size(165, 28);
            this.tslGroupCountForList.Text = "Число Групп копий в файле:";
            // 
            // tscbGroupCountForList
            // 
            this.tscbGroupCountForList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.tscbGroupCountForList.Items.AddRange(new object[] {
            "5",
            "10",
            "50",
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
            "5000",
            "7500",
            "10000"});
            this.tscbGroupCountForList.MaxDropDownItems = 10;
            this.tscbGroupCountForList.Name = "tscbGroupCountForList";
            this.tscbGroupCountForList.Size = new System.Drawing.Size(75, 31);
            this.tscbGroupCountForList.SelectedIndexChanged += new System.EventHandler(this.TscbGroupCountForListSelectedIndexChanged);
            // 
            // tsbtnDupSaveList
            // 
            this.tsbtnDupSaveList.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnDupSaveList.Image")));
            this.tsbtnDupSaveList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDupSaveList.Name = "tsbtnDupSaveList";
            this.tsbtnDupSaveList.Size = new System.Drawing.Size(197, 28);
            this.tsbtnDupSaveList.Text = "Сохранить Группы в файлы...";
            this.tsbtnDupSaveList.ToolTipText = "Сохранить список копий книг в файлы";
            this.tsbtnDupSaveList.Click += new System.EventHandler(this.TsbtnDupSaveListClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
            // 
            // tsbtnDupOpenList
            // 
            this.tsbtnDupOpenList.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnDupOpenList.Image")));
            this.tsbtnDupOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDupOpenList.Name = "tsbtnDupOpenList";
            this.tsbtnDupOpenList.Size = new System.Drawing.Size(143, 28);
            this.tsbtnDupOpenList.Text = "Загрузить Группы...";
            this.tsbtnDupOpenList.ToolTipText = "Загрузить список копий книг из файла";
            this.tsbtnDupOpenList.Click += new System.EventHandler(this.TsbtnDupOpenListClick);
            // 
            // tsbtnDupCurrentSaveList
            // 
            this.tsbtnDupCurrentSaveList.Enabled = false;
            this.tsbtnDupCurrentSaveList.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnDupCurrentSaveList.Image")));
            this.tsbtnDupCurrentSaveList.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnDupCurrentSaveList.Name = "tsbtnDupCurrentSaveList";
            this.tsbtnDupCurrentSaveList.Size = new System.Drawing.Size(93, 28);
            this.tsbtnDupCurrentSaveList.Text = "Сохранить";
            this.tsbtnDupCurrentSaveList.ToolTipText = "Сохранить текущий обрабатываемый список копий";
            this.tsbtnDupCurrentSaveList.Click += new System.EventHandler(this.TsbtnDupCurrentSaveListClick);
            // 
            // tpOptions
            // 
            this.tpOptions.Controls.Add(this.cboxDblClickForFB2);
            this.tpOptions.Controls.Add(this.lblValidatorForFB2);
            this.tpOptions.Controls.Add(this.cboxPressEnterForFB2);
            this.tpOptions.Controls.Add(this.lblValidatorForFB2PE);
            this.tpOptions.Controls.Add(this.gboxCopyMoveOptions);
            this.tpOptions.Location = new System.Drawing.Point(4, 22);
            this.tpOptions.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tpOptions.Name = "tpOptions";
            this.tpOptions.Padding = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.tpOptions.Size = new System.Drawing.Size(1115, 511);
            this.tpOptions.TabIndex = 1;
            this.tpOptions.Text = "Настройки";
            this.tpOptions.UseVisualStyleBackColor = true;
            // 
            // cboxDblClickForFB2
            // 
            this.cboxDblClickForFB2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxDblClickForFB2.FormattingEnabled = true;
            this.cboxDblClickForFB2.Items.AddRange(new object[] {
            "Проверить файл на валидность",
            "Редактировать в текстовом редакторе",
            "Редактировать в fb2-редакторе",
            "Запустить в fb2-читалке (Просмотр)",
            "Правка метаданных описания книги"});
            this.cboxDblClickForFB2.Location = new System.Drawing.Point(266, 57);
            this.cboxDblClickForFB2.Name = "cboxDblClickForFB2";
            this.cboxDblClickForFB2.Size = new System.Drawing.Size(337, 21);
            this.cboxDblClickForFB2.TabIndex = 27;
            this.cboxDblClickForFB2.SelectedIndexChanged += new System.EventHandler(this.CboxDblClickForFB2SelectedIndexChanged);
            // 
            // lblValidatorForFB2
            // 
            this.lblValidatorForFB2.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lblValidatorForFB2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblValidatorForFB2.Location = new System.Drawing.Point(14, 59);
            this.lblValidatorForFB2.Name = "lblValidatorForFB2";
            this.lblValidatorForFB2.Size = new System.Drawing.Size(260, 18);
            this.lblValidatorForFB2.TabIndex = 25;
            this.lblValidatorForFB2.Text = "Действие по двойному щелчку мышки на Списке:";
            // 
            // cboxPressEnterForFB2
            // 
            this.cboxPressEnterForFB2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxPressEnterForFB2.FormattingEnabled = true;
            this.cboxPressEnterForFB2.Items.AddRange(new object[] {
            "Проверить файл на валидность",
            "Редактировать в текстовом редакторе",
            "Редактировать в fb2-редакторе",
            "Запустить в fb2-читалке (Просмотр)",
            "Правка метаданных описания книги"});
            this.cboxPressEnterForFB2.Location = new System.Drawing.Point(266, 86);
            this.cboxPressEnterForFB2.Name = "cboxPressEnterForFB2";
            this.cboxPressEnterForFB2.Size = new System.Drawing.Size(337, 21);
            this.cboxPressEnterForFB2.TabIndex = 28;
            this.cboxPressEnterForFB2.SelectedIndexChanged += new System.EventHandler(this.CboxPressEnterForFB2SelectedIndexChanged);
            // 
            // lblValidatorForFB2PE
            // 
            this.lblValidatorForFB2PE.Font = new System.Drawing.Font("Tahoma", 8F);
            this.lblValidatorForFB2PE.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblValidatorForFB2PE.Location = new System.Drawing.Point(14, 86);
            this.lblValidatorForFB2PE.Name = "lblValidatorForFB2PE";
            this.lblValidatorForFB2PE.Size = new System.Drawing.Size(250, 18);
            this.lblValidatorForFB2PE.TabIndex = 26;
            this.lblValidatorForFB2PE.Text = "Действие по нажатию клавиши Enter на Списке:";
            // 
            // gboxCopyMoveOptions
            // 
            this.gboxCopyMoveOptions.Controls.Add(this.cboxExistFile);
            this.gboxCopyMoveOptions.Controls.Add(this.lblExistFile);
            this.gboxCopyMoveOptions.Dock = System.Windows.Forms.DockStyle.Top;
            this.gboxCopyMoveOptions.Font = new System.Drawing.Font("Tahoma", 8F);
            this.gboxCopyMoveOptions.ForeColor = System.Drawing.Color.Maroon;
            this.gboxCopyMoveOptions.Location = new System.Drawing.Point(2, 2);
            this.gboxCopyMoveOptions.Name = "gboxCopyMoveOptions";
            this.gboxCopyMoveOptions.Size = new System.Drawing.Size(1111, 53);
            this.gboxCopyMoveOptions.TabIndex = 29;
            this.gboxCopyMoveOptions.TabStop = false;
            this.gboxCopyMoveOptions.Text = " Настройки для Копирования / Перемещения файлов ";
            // 
            // cboxExistFile
            // 
            this.cboxExistFile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxExistFile.Font = new System.Drawing.Font("Tahoma", 8F);
            this.cboxExistFile.FormattingEnabled = true;
            this.cboxExistFile.Items.AddRange(new object[] {
            "Заменить существующий файл новым",
            "Добавить к новому файлу очередной номер",
            "Добавить к новому файлу дату и время"});
            this.cboxExistFile.Location = new System.Drawing.Point(265, 21);
            this.cboxExistFile.Name = "cboxExistFile";
            this.cboxExistFile.Size = new System.Drawing.Size(337, 21);
            this.cboxExistFile.TabIndex = 18;
            this.cboxExistFile.SelectedIndexChanged += new System.EventHandler(this.CboxExistFileSelectedIndexChanged);
            // 
            // lblExistFile
            // 
            this.lblExistFile.AutoSize = true;
            this.lblExistFile.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblExistFile.Location = new System.Drawing.Point(14, 25);
            this.lblExistFile.Name = "lblExistFile";
            this.lblExistFile.Size = new System.Drawing.Size(213, 13);
            this.lblExistFile.TabIndex = 17;
            this.lblExistFile.Text = "Одинаковые файлы в папке-приемнике:";
            // 
            // SFBTpFB2Dublicator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tcDuplicator);
            this.Controls.Add(this.ssProgress);
            this.Name = "SFBTpFB2Dublicator";
            this.Size = new System.Drawing.Size(1123, 560);
            this.ssProgress.ResumeLayout(false);
            this.ssProgress.PerformLayout();
            this.cmsFB2.ResumeLayout(false);
            this.tcDuplicator.ResumeLayout(false);
            this.tpDuplicator.ResumeLayout(false);
            this.tpDuplicator.PerformLayout();
            this.pInfo.ResumeLayout(false);
            this.FB2InfoPanel.ResumeLayout(false);
            this.tcViewFB2Desc.ResumeLayout(false);
            this.tpTitleInfo.ResumeLayout(false);
            this.tpSourceTitleInfo.ResumeLayout(false);
            this.tpDocumentInfo.ResumeLayout(false);
            this.tpPublishInfo.ResumeLayout(false);
            this.tpCustomInfo.ResumeLayout(false);
            this.tpHistory.ResumeLayout(false);
            this.tpTIAnnotation.ResumeLayout(false);
            this.tpSTIAnnotation.ResumeLayout(false);
            this.tpValidate.ResumeLayout(false);
            this.tpValidate.PerformLayout();
            this.tcCovers.ResumeLayout(false);
            this.tpTI.ResumeLayout(false);
            this.TICoverPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxTICover)).EndInit();
            this.TICoverInfoPanel.ResumeLayout(false);
            this.TICoverListViewPanel.ResumeLayout(false);
            this.TICoverListViewButtonPanel.ResumeLayout(false);
            this.tpSTI.ResumeLayout(false);
            this.STICoverPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picBoxSTICover)).EndInit();
            this.STICoverInfoPanel.ResumeLayout(false);
            this.STICoverListViewPanel.ResumeLayout(false);
            this.STICoverListViewButtonPanel.ResumeLayout(false);
            this.pFilesCount.ResumeLayout(false);
            this.pFilesCount.PerformLayout();
            this.CommandToolStrip.ResumeLayout(false);
            this.CommandToolStrip.PerformLayout();
            this.pMode.ResumeLayout(false);
            this.pSearchFBDup2Dirs.ResumeLayout(false);
            this.pSearchFBDup2Dirs.PerformLayout();
            this.tsDup.ResumeLayout(false);
            this.tsDup.PerformLayout();
            this.tpOptions.ResumeLayout(false);
            this.gboxCopyMoveOptions.ResumeLayout(false);
            this.gboxCopyMoveOptions.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		private System.Windows.Forms.ToolStripButton toolStripButtonEditBookName;
		private System.Windows.Forms.ToolStripMenuItem tsmiEditBookName;
		private System.Windows.Forms.ToolStripMenuItem tsmiAllOldBooksVersionValidateForAllGroups;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem15;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem14;
		private System.Windows.Forms.ToolStripMenuItem tsmiAnalyzeVersionValidateInGroup;
		private System.Windows.Forms.CheckBox checkBoxSaveGroupsToXml;
		private System.Windows.Forms.ToolStripProgressBar tsProgressBar;
		private System.Windows.Forms.ColumnHeader chFileLastWriteTime;
		private System.Windows.Forms.ColumnHeader chFileCreationTime;
		private System.Windows.Forms.ColumnHeader chFileLenght;
		private System.Windows.Forms.ColumnHeader chValidate;
		private System.Windows.Forms.ColumnHeader chEncodimg;
		private System.Windows.Forms.ColumnHeader chVersion;
		private System.Windows.Forms.ColumnHeader chID;
		private System.Windows.Forms.ColumnHeader chLang;
		private System.Windows.Forms.ColumnHeader chGenres;
		private System.Windows.Forms.ColumnHeader chAuthors;
		private System.Windows.Forms.ColumnHeader chBookTitle;
		private System.Windows.Forms.ColumnHeader chPath;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem12;
		private System.Windows.Forms.ToolStripMenuItem tsmiAutoCorrectorForAllBooksFromGroup;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem13;
		private System.Windows.Forms.ToolStripMenuItem tsmiAutoCorrectorForAllBooksAllGroup;
		private System.Windows.Forms.ToolStripMenuItem tsmiAutoCorrectorForAllSelectedBooks;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemAutoCorrectorForAllCheckedBooks;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSetNewIDForAllSelectedBooks;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSetNewIDForAllCheckedBooks;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSetNewIDForAllBooksFromGroup;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSetNewIDForAllBooksAllGroup;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSetGenresForSelectedBooks;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSetGenresForCheckedBooks;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSetAuthorsForSelectedBooks;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemSetAuthorsForCheckedBooks;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonEditGenres;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonEditAuthors;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonNewID;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripButton toolStripButtonEditDescription;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDeleteCheckedFb2View;
		private System.Windows.Forms.ToolStripMenuItem ToolStripMenuItemDeleteCheckedFb2Fast;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonDeleteCheckedFb2;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMoveCheckedFb2ToView;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemMoveCheckedFb2ToFast;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripButton toolStripButtonCopyCheckedFb2To;
		private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButtonMoveCheckedFb2To;
		private System.Windows.Forms.ToolStripButton toolStripButtonViewInReader;
		private System.Windows.Forms.ToolStripButton toolStripButtonDiffFB2;
		private System.Windows.Forms.ToolStripButton toolStripButtonEditInFB2Editor;
		private System.Windows.Forms.ToolStripButton toolStripButtonEditInTextEditor;
		private System.Windows.Forms.ToolStrip CommandToolStrip;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
		private System.Windows.Forms.ToolStripMenuItem tsmiSetNewIDForAllBooksAllGroup;
		private System.Windows.Forms.Panel STICoverPanel;
		private System.Windows.Forms.PictureBox picBoxSTICover;
		private System.Windows.Forms.Panel STICoverInfoPanel;
		private System.Windows.Forms.Label STICoverLenghtLabel;
		private System.Windows.Forms.Label STICoverPixelsLabel;
		private System.Windows.Forms.Label STICoverDPILabel;
		private System.Windows.Forms.Panel STICoverListViewPanel;
		private System.Windows.Forms.Panel STICoverListViewButtonPanel;
		private System.Windows.Forms.Button STISaveSelectedCoverButton;
		private System.Windows.Forms.ListView STICoversListView;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.Panel TICoverPanel;
		private System.Windows.Forms.PictureBox picBoxTICover;
		private System.Windows.Forms.Panel TICoverInfoPanel;
		private System.Windows.Forms.Label TICoverLenghtLabel;
		private System.Windows.Forms.Label TICoverPixelsLabel;
		private System.Windows.Forms.Label TICoverDPILabel;
		private System.Windows.Forms.Panel TICoverListViewPanel;
		private System.Windows.Forms.Panel TICoverListViewButtonPanel;
		private System.Windows.Forms.Button TISaveSelectedCoverButton;
		private System.Windows.Forms.ListView TICoversListView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ToolStripButton tsbtnDupCurrentSaveList;
		private System.Windows.Forms.ToolStripMenuItem tsmiDeleteChechedItemsNotDeleteFiles;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
		private System.Windows.Forms.ComboBox cboxDblClickForFB2;
		private System.Windows.Forms.Label lblValidatorForFB2;
		private System.Windows.Forms.ComboBox cboxPressEnterForFB2;
		private System.Windows.Forms.Label lblValidatorForFB2PE;
		private System.Windows.Forms.GroupBox gboxCopyMoveOptions;
		private System.Windows.Forms.ComboBox cboxExistFile;
		private System.Windows.Forms.Label lblExistFile;
		private System.Windows.Forms.TabControl tcDuplicator;
		private System.Windows.Forms.TabPage tpDuplicator;
		private System.Windows.Forms.TabPage tpOptions;
		private System.Windows.Forms.Label RazdelitLabel1;
		private System.Windows.Forms.Label BadZipLabel1;
		private System.Windows.Forms.Label ZipLabel;
		private System.Windows.Forms.Label NotValidLabel;
		private System.Windows.Forms.Label LegengCaptionLabel;
		private System.Windows.Forms.Panel pFilesCount;
		private System.Windows.Forms.ToolStripMenuItem tsmiDeleteAllItemForNonExistFile;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
		private System.Windows.Forms.ToolStripMenuItem tsmiAllNonValidateBooksForAllGroups;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripMenuItem tsmiColumnsResultAutoReize;
		private System.Windows.Forms.ToolStripMenuItem tsmiAllNonValidateBooks;
		private System.Windows.Forms.ToolStripLabel tslGroupCountForList;
		private System.Windows.Forms.ToolStripComboBox tscbGroupCountForList;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.SaveFileDialog sfdList;
		private System.Windows.Forms.FolderBrowserDialog fbdSaveList;
		private System.Windows.Forms.ToolStripStatusLabel tsslblProgress;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
		private System.Windows.Forms.ToolStripMenuItem tsmiSetNewIDForAllBooksFromGroup;
		private System.Windows.Forms.ToolStripMenuItem tsmiEditGenres;
		private System.Windows.Forms.ToolStripMenuItem tsmiSetGenresForSelectedBooks;
		private System.Windows.Forms.ToolStripMenuItem tsmiSetGenresForCheckedBooks;
		private System.Windows.Forms.ToolStripMenuItem tsmiSetAuthorsForSelectedBooks;
		private System.Windows.Forms.ToolStripMenuItem tsmiEditAuthors;
		private System.Windows.Forms.ToolStripMenuItem tsmiSetAuthorsForCheckedBooks;
		private System.Windows.Forms.ToolStripMenuItem tsmiAllFilesInGroupReValidate;
		private System.Windows.Forms.ToolStripMenuItem tsmiAllGroupsReValidate;
		private System.Windows.Forms.ToolStripMenuItem tsmiFileReValidate;
		private System.Windows.Forms.ToolStripMenuItem tsmiValidate;
		private System.Windows.Forms.ToolStripMenuItem tsmiSetNewIDForAllCheckedBooks;
		private System.Windows.Forms.ToolStripMenuItem tsmiNewID;
		private System.Windows.Forms.ToolStripMenuItem tsmiSetNewIDForAllSelectedBooks;
		private System.Windows.Forms.TabControl tcCovers;
		private System.Windows.Forms.TabPage tpTI;
		private System.Windows.Forms.TabPage tpSTI;
		private System.Windows.Forms.RichTextBox rtbSTIAnnotation;
		private System.Windows.Forms.TabPage tpSTIAnnotation;
		private System.Windows.Forms.ToolStripMenuItem tsmiEditDescription;
		private System.Windows.Forms.Panel FB2InfoPanel;
		private System.Windows.Forms.ToolStripMenuItem tsmiDeleteGroupNotFile;
		private System.Windows.Forms.ToolStripMenuItem tsmiMoveCheckedFb2ToView;
		private System.Windows.Forms.ToolStripMenuItem tsmiMoveCheckedFb2ToFast;
		private System.Windows.Forms.ToolStripMenuItem tsmiDeleteCheckedFb2View;
		private System.Windows.Forms.ToolStripMenuItem tsmiDeleteCheckedFb2Fast;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem tsmiSaveAllCheckedItemToFile;
		private System.Windows.Forms.TextBox tbValidate;
		private System.Windows.Forms.TabPage tpValidate;
		private System.Windows.Forms.ToolStripMenuItem tsmiCheckedAllInGroup;
		private System.Windows.Forms.ToolStripMenuItem tsmiDeleteCheckedFb2;
		private System.Windows.Forms.ToolStripMenuItem tsmiMoveCheckedFb2To;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyCheckedFb2To;
		private System.Windows.Forms.ToolStripButton tsbtnSearchFb2DupRenew;
		private System.Windows.Forms.Button btnOpenDir;
		private System.Windows.Forms.ToolStripMenuItem tsmiAnalyzeForAllGroups;
		private System.Windows.Forms.ToolStripMenuItem tsmiAllOldBooksLastWriteTimeInGroup;
		private System.Windows.Forms.ToolStripMenuItem tsmiAllOldBooksCreationTimeInGroup;
		private System.Windows.Forms.ToolStripMenuItem tsmiAllOldBooksLastWriteTimeForAllGroups;
		private System.Windows.Forms.ToolStripMenuItem tsmiAllOldBooksCreationTimeForAllGroups;
		private System.Windows.Forms.ToolStripMenuItem tsmiAnalyzeForSelectedGroup;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ImageList imageListDup;
		private System.Windows.Forms.ToolStrip tsDup;
		private System.Windows.Forms.Panel pInfo;
		private System.Windows.Forms.OpenFileDialog sfdLoadList;
		private System.Windows.Forms.ToolStripButton tsbtnDupOpenList;
		private System.Windows.Forms.ToolStripButton tsbtnDupSaveList;
		private System.Windows.Forms.ToolStripMenuItem tsmiUnCheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiCheckedAll;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem tsmiDiffFB2;
		private System.Windows.Forms.ToolStripMenuItem tsmiOpenFileDir;
		private System.Windows.Forms.ToolStripSeparator tsmi2;
		private System.Windows.Forms.ToolStripMenuItem tsmiViewInReader;
		private System.Windows.Forms.ToolStripSeparator tsmi1;
		private System.Windows.Forms.ToolStripMenuItem tsmiEditInFB2Editor;
		private System.Windows.Forms.ToolStripMenuItem tsmiEditInTextEditor;
		private System.Windows.Forms.ToolStripSeparator tsmi3;
		private System.Windows.Forms.ContextMenuStrip cmsFB2;
		private System.Windows.Forms.ListView lvPublishInfo;
		private System.Windows.Forms.ListView lvTitleInfo;
		private System.Windows.Forms.ListView lvSourceTitleInfo;
		private System.Windows.Forms.ListView listViewFB2Files;
		private System.Windows.Forms.Panel pSearchFBDup2Dirs;
		private System.Windows.Forms.FolderBrowserDialog fbdScanDir;
		private System.Windows.Forms.ColumnHeader columnHeader18;
		private System.Windows.Forms.ColumnHeader columnHeader17;
		private System.Windows.Forms.ColumnHeader columnHeader16;
		private System.Windows.Forms.ColumnHeader columnHeader15;
		private System.Windows.Forms.ListView lvDocumentInfo;
		private System.Windows.Forms.ColumnHeader columnHeader14;
		private System.Windows.Forms.ColumnHeader columnHeader13;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.ListView lvCustomInfo;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.RichTextBox rtbTIAnnotation;
		private System.Windows.Forms.RichTextBox rtbHistory;
		private System.Windows.Forms.TabPage tpTIAnnotation;
		private System.Windows.Forms.TabPage tpHistory;
		private System.Windows.Forms.TabPage tpCustomInfo;
		private System.Windows.Forms.TabPage tpPublishInfo;
		private System.Windows.Forms.TabPage tpDocumentInfo;
		private System.Windows.Forms.TabPage tpSourceTitleInfo;
		private System.Windows.Forms.TabPage tpTitleInfo;
		private System.Windows.Forms.TabControl tcViewFB2Desc;
		private System.Windows.Forms.ComboBox cboxMode;
		private System.Windows.Forms.Label lblMode;
		private System.Windows.Forms.Panel pMode;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ListView lvFilesCount;
		private System.Windows.Forms.Label lblScanDir;
		private System.Windows.Forms.TextBox tboxSourceDir;
		private System.Windows.Forms.CheckBox chBoxScanSubDir;
		private System.Windows.Forms.ToolStripButton tsbtnSearchDubls;
		private System.Windows.Forms.StatusStrip ssProgress;
		#endregion
		
		#region Закрытые данные класса
		private readonly MiscListView		m_mscLV			= new MiscListView(); // класс по работе с ListView
		private readonly FB2Validator		m_fv2Validator	= new FB2Validator();
		private readonly SharpZipLibWorker	m_sharpZipLib	= new SharpZipLibWorker();
		private readonly string				m_TempDir		= Settings.Settings.TempDirPath;
		
		private bool m_isSettingsLoaded		= false; // Только при true все изменения настроек сохраняются в файл.
		private string	m_TargetDir			= string.Empty; // Папка для Copy / Move помеченных книг
		private string	m_sMessTitle		= string.Empty;
		private string	m_DirForSavedCover	= string.Empty;	// папка для сохранения обложек
		private int		m_CurrentResultItem	= -1;
        private readonly MiscListView.ListViewColumnSorter m_lvwColumnSorter =
			new MiscListView.ListViewColumnSorter(9);
		#endregion
		
		public SFBTpFB2Dublicator()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			// задание для кнопок ToolStrip стиля и положения текста и картинки
			SetToolButtonsSettings();
			init();

			ConnectListsEventHandlers( false );
			
			cboxMode.SelectedIndex				= 0; // Условия для Сравнения fb2-файлов: md5 книги
			cboxExistFile.SelectedIndex			= 1;
			cboxDblClickForFB2.SelectedIndex	= 1;
			cboxPressEnterForFB2.SelectedIndex	= 0;
			tscbGroupCountForList.SelectedIndex = 11;
			
			// задание Tag для Listview
			foreach ( ColumnHeader ch in listViewFB2Files.Columns )
				ch.Tag = ch.Text;
			
			// загрузка настроек из xml
			readSettingsFromXML();

            ConnectListsEventHandlers( true );
			m_isSettingsLoaded = true;
		}
		// =============================================================================================
		// 									ОТКРЫТЫЕ МЕТОДЫ КЛАССА
		// =============================================================================================
		#region Открытые методы класса
		// задание для кнопок ToolStrip стиля и положения текста и картинки
		public void SetToolButtonsSettings() {
			Settings.FB2DublicatorSettings.SetToolButtonsSettings( tsDup );
		}
		
		// сохранение настроек в xml-файл
		public void saveSettingsToXml() {
			// защита от "затирания" настроек в файле, когда в некоторые контролы данные еще не загрузились
			if ( m_isSettingsLoaded ) {
                XDocument doc = new XDocument(
                    new XDeclaration("1.0", "utf-8", "yes"),
                    new XElement("Settings", new XAttribute("type", "dup_settings"),
                                 new XComment("xml файл настроек Дубликатора"),
                                 new XComment("Папка для поиска копий fb2 книг"),
                    new XElement("SourceDir", tboxSourceDir.Text.Trim()),
                                 new XComment("Папка для копирования/перемещения копий fb2 книг"),
                    new XElement("TargetDir", m_TargetDir),
                                 new XComment("Настройки поиска-сравнения fb2 книг"),
                    new XElement("Options",
                                 new XComment("Режим поиска-сравнения fb2 книг"),
                                 new XElement("CompareMode",
                                             new XAttribute("index", cboxMode.SelectedIndex),
                                             new XAttribute("name", cboxMode.Text)
                                 ),
                                 new XComment("Сканировать подпапки"),
                                 new XElement("ScanSubDirs", chBoxScanSubDir.Checked),
                                 new XComment("Операции с одинаковыми fb2-файлами при копировании/перемещении"),
                                 new XElement("FB2ExsistMode",
                                             new XAttribute("index", cboxExistFile.SelectedIndex),
                                             new XAttribute("name", cboxExistFile.Text)
                                 ),
                                 new XComment("Действие по двойному щелчку мышки на Списке"),
                                 new XElement("DblClickForFB2", cboxDblClickForFB2.SelectedIndex),
                                 new XComment("Действие по нажатию клавиши Enter на Списке"),
                                 new XElement("PressEnterForFB2", cboxPressEnterForFB2.SelectedIndex),
                                 new XComment("число Групп для сохранения в список"),
                                 new XElement("GroupCountForList", tscbGroupCountForList.SelectedIndex),
                                 new XComment("Сохранять результат (Группы) сразу в файлы без построения дерева"),
                                 new XElement("SaveGroupToXMLWithoutTree", checkBoxSaveGroupsToXml.Checked),
                                 new XComment("Ширина колонок списка копий"),
                                 new XElement("Columns", new XAttribute("count", listViewFB2Files.Columns.Count))
                                 )
                    )
                );

                // сохранение расположения и ширины колонок
                XElement xColumns = doc.Root.Element("Options").Element("Columns");
				MiscListView.addColumnsToXDocument( ref xColumns, ref listViewFB2Files );

				doc.Save( Settings.FB2DublicatorSettings.DublicatorPath );
			}
		}
		#endregion
		
		// =============================================================================================
		// 							ВСПОМОГАТЕЛЬНЫЕ ЗАКРЫТЫЕ МЕТОДЫ И АЛГОРИТМЫ КЛАССА
		// =============================================================================================
		#region Вспомогательные методы и алгоритмы класса
		// инициализация контролов и переменных
		private void init() {
			ConnectListsEventHandlers( false );
			for( int i = 0; i != lvFilesCount.Items.Count; ++i )
				lvFilesCount.Items[i].SubItems[1].Text	= "0";

			// очистка временной папки
			FilesWorker.RemoveDir( Settings.Settings.TempDirPath );
			listViewFB2Files.Items.Clear();
			listViewFB2Files.Groups.Clear();
			ConnectListsEventHandlers( true );
		}
		
		// загрузка настроек из xml-файла
		private void readSettingsFromXML() {
			if ( File.Exists( Settings.FB2DublicatorSettings.DublicatorPath ) ) {
				XElement xmlTree = XElement.Load( Settings.FB2DublicatorSettings.DublicatorPath );
				// Папка для поиска копий fb2 книг
				if ( xmlTree.Element("SourceDir") != null )
					tboxSourceDir.Text = xmlTree.Element("SourceDir").Value;
				// Папка для копирования/перемещения копий fb2 книг
				if ( xmlTree.Element("TargetDir") != null )
					m_TargetDir = xmlTree.Element("TargetDir").Value;
				
				// установки сравнения
				if ( xmlTree.Element("Options") != null ) {
					XElement xmlOptions = xmlTree.Element("Options");
					// Режим поиска-сравнения fb2 книг
					if ( xmlOptions.Element("CompareMode") != null ) {
						if ( xmlOptions.Element("CompareMode").Attribute("index") != null )
							cboxMode.SelectedIndex = Convert.ToInt16( xmlOptions.Element("CompareMode").Attribute("index").Value );
					}
                    // Сканирование подпапок
                    if ( xmlOptions.Element("ScanSubDirs") != null )
						chBoxScanSubDir.Checked	= Convert.ToBoolean( xmlOptions.Element("ScanSubDirs").Value );
                    // Операции с одинаковыми fb2-файлами при копировании/перемещении
                    if ( xmlOptions.Element("FB2ExsistMode") != null ) {
						if ( xmlOptions.Element("FB2ExsistMode").Attribute("index") != null )
							cboxExistFile.SelectedIndex = Convert.ToInt16( xmlOptions.Element("FB2ExsistMode").Attribute("index").Value );
					}
					// Действие по двойному щелчку мышки на Списке
					if ( xmlOptions.Element("DblClickForFB2") != null )
						cboxDblClickForFB2.SelectedIndex = Convert.ToInt16( xmlOptions.Element("DblClickForFB2").Value );
					// Действие по нажатию клавиши Enter на Списке
					if ( xmlOptions.Element("PressEnterForFB2") != null )
						cboxPressEnterForFB2.SelectedIndex = Convert.ToInt16( xmlOptions.Element("PressEnterForFB2").Value );
					// число Групп для сохранения в список
					if ( xmlOptions.Element("GroupCountForList") != null )
						tscbGroupCountForList.SelectedIndex = Convert.ToInt16( xmlOptions.Element("GroupCountForList").Value );
					if ( xmlOptions.Element("SaveGroupToXMLWithoutTree") != null )
						checkBoxSaveGroupsToXml.Checked	= Convert.ToBoolean( xmlOptions.Element("SaveGroupToXMLWithoutTree").Value );
					
					// ширина и расположение колонок
					if ( xmlOptions.Element("Columns") != null ) {
						XElement xColumns = xmlOptions.Element("Columns");
						MiscListView.setColumnHeradersDataToListView( ref xColumns, ref listViewFB2Files );
					}
				}
			}
		}
		// отключение/включение обработчиков событий (убираем "тормоза")
		public void ConnectListsEventHandlers( bool bConnect ) {
			if ( bConnect ) {
				this.listViewFB2Files.SelectedIndexChanged +=
					new System.EventHandler(this.LvResultSelectedIndexChanged);
				this.listViewFB2Files.ColumnClick +=
					new System.Windows.Forms.ColumnClickEventHandler(this.LvResultColumnClick);
				this.listViewFB2Files.DoubleClick += new System.EventHandler(this.LvResultDoubleClick);
				this.listViewFB2Files.KeyPress +=
					new System.Windows.Forms.KeyPressEventHandler(this.LvResultKeyPress);
				this.tscbGroupCountForList.SelectedIndexChanged +=
					new System.EventHandler(this.TscbGroupCountForListSelectedIndexChanged);
				this.tboxSourceDir.TextChanged += new System.EventHandler(this.TboxSourceDirTextChanged);
				this.cboxMode.SelectedIndexChanged += new System.EventHandler(this.CboxModeSelectedIndexChanged);
				this.cboxExistFile.SelectedIndexChanged +=
					new System.EventHandler(this.CboxExistFileSelectedIndexChanged);
				this.cboxDblClickForFB2.SelectedIndexChanged +=
					new System.EventHandler(this.CboxDblClickForFB2SelectedIndexChanged);
				this.cboxPressEnterForFB2.SelectedIndexChanged +=
					new System.EventHandler(this.CboxPressEnterForFB2SelectedIndexChanged);
				this.chBoxScanSubDir.CheckStateChanged +=
					new System.EventHandler(this.ChBoxScanSubDirCheckStateChanged);
				this.toolStripButtonCopyCheckedFb2To.Click +=
					new System.EventHandler(this.TsmiCopyCheckedFb2ToClick);
				this.tsmiCopyCheckedFb2To.Click +=
					new System.EventHandler(this.TsmiCopyCheckedFb2ToClick);
				this.checkBoxSaveGroupsToXml.CheckStateChanged +=
					new System.EventHandler(this.CheckBoxSaveGroupsToXmlCheckStateChanged);
			} else {
				// отключаем обработчики событий (убираем "тормоза")
				this.listViewFB2Files.SelectedIndexChanged -=
					new System.EventHandler(this.LvResultSelectedIndexChanged);
				this.listViewFB2Files.ColumnClick -=
					new System.Windows.Forms.ColumnClickEventHandler(this.LvResultColumnClick);
				this.listViewFB2Files.DoubleClick -= new System.EventHandler(this.LvResultDoubleClick);
				this.listViewFB2Files.KeyPress -=
					new System.Windows.Forms.KeyPressEventHandler(this.LvResultKeyPress);
				this.tscbGroupCountForList.SelectedIndexChanged -=
					new System.EventHandler(this.TscbGroupCountForListSelectedIndexChanged);
				this.tboxSourceDir.TextChanged -= new System.EventHandler(this.TboxSourceDirTextChanged);
				this.cboxMode.SelectedIndexChanged -= new System.EventHandler(this.CboxModeSelectedIndexChanged);
				this.cboxExistFile.SelectedIndexChanged -=
					new System.EventHandler(this.CboxExistFileSelectedIndexChanged);
				this.cboxDblClickForFB2.SelectedIndexChanged -=
					new System.EventHandler(this.CboxDblClickForFB2SelectedIndexChanged);
				this.cboxPressEnterForFB2.SelectedIndexChanged -=
					new System.EventHandler(this.CboxPressEnterForFB2SelectedIndexChanged);
				this.chBoxScanSubDir.CheckStateChanged -=
					new System.EventHandler(this.ChBoxScanSubDirCheckStateChanged);
				this.toolStripButtonCopyCheckedFb2To.Click -=
					new System.EventHandler(this.TsmiCopyCheckedFb2ToClick);
				this.tsmiCopyCheckedFb2To.Click -=
					new System.EventHandler(this.TsmiCopyCheckedFb2ToClick);
				this.checkBoxSaveGroupsToXml.CheckStateChanged -=
					new System.EventHandler(this.CheckBoxSaveGroupsToXmlCheckStateChanged);
			}
		}

		// проверка на наличие папки сканирования копий книг
		private bool isScanFolderDataCorrect( TextBox tbSource, string MessTitle ) {
			// проверка на корректность данных папок источника
			string sSource	= FilesWorker.WorkingDirPath( tbSource.Text.Trim() );
			tbSource.Text	= sSource;
			
			// проверки на корректность папок источника
			if ( sSource.Length == 0 ) {
				MessageBox.Show( "Выберите папку для сканирования!", MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			DirectoryInfo diFolder = new DirectoryInfo( sSource );
			if ( !diFolder.Exists ) {
				MessageBox.Show( "Папка не найдена: " + sSource, MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			return true;
		}
		// запуск выбранного обработчика событий
		private void goHandlerWorker( ComboBox comboBox, object sender, EventArgs e ) {
			switch ( comboBox.SelectedIndex ) {
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
					TsmiViewInReaderClick( sender, e );
					break;
				case 5: // Правка метаданных описания книги
					TsmiEditDescriptionClick( sender, e );
					break;
			}
		}
		
		// пометка всех "старых" книг в Группе/Группах по выбранному методу анализа
		private void checkOldBook( GroupAnalyzeModeEnum ToGroupAnalyzeMode, CompareModeEnum AnalyzeMode ) {
			if ( listViewFB2Files.Items.Count > 0 ) {
				ConnectListsEventHandlers( false );
				Core.Duplicator.CopiesAnalyzeForm copiesAnalyzeForm = new Core.Duplicator.CopiesAnalyzeForm(
					ToGroupAnalyzeMode, AnalyzeMode, listViewFB2Files
				);
				copiesAnalyzeForm.ShowDialog();
				EndWorkMode EndWorkMode = copiesAnalyzeForm.EndMode;
				copiesAnalyzeForm.Dispose();
				ConnectListsEventHandlers( true );
				MessageBox.Show( EndWorkMode.Message, "Анализ копий fb2 книг", MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
		}
		
		// переместить помеченные файлы в папку-приемник
		// Fast = false: с удалением элементов списка копий - медленно. Fast = true: без удаления элементов списка копий - быстро
		private void moveCheckedFb2To( bool Fast ) {
			if ( listViewFB2Files.Items.Count > 0 && listViewFB2Files.CheckedItems.Count > 0 ) {
				string sTarget = FilesWorker.OpenDirDlg( m_TargetDir, fbdScanDir, "Укажите папку-приемник для размешения копий книг:" );
				if( sTarget == null )
					return;

				saveSettingsToXml();
				
				const string MessTitle = "SharpFBTools - Перемещение копий книг";
				if ( tboxSourceDir.Text.Trim() == sTarget ) {
					MessageBox.Show( "Папка-приемник файлов совпадает с папкой сканирования!\nРабота прекращена.",
					                MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return;
				}
				
				listViewFB2Files.BeginUpdate();
				ConnectListsEventHandlers( false );
				Core.Duplicator.CopyMoveDeleteForm copyMoveDeleteForm = new Core.Duplicator.CopyMoveDeleteForm(
					Fast, BooksWorkModeEnum.MoveCheckedBooks, tboxSourceDir.Text.Trim(), sTarget,
					cboxExistFile.SelectedIndex, lvFilesCount, listViewFB2Files
				);
				copyMoveDeleteForm.ShowDialog();
				EndWorkMode EndWorkMode = copyMoveDeleteForm.EndMode;
				copyMoveDeleteForm.Dispose();
				ConnectListsEventHandlers( true );
				listViewFB2Files.EndUpdate();
				MessageBox.Show( EndWorkMode.Message, MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
		}
		// удалить помеченные файлы (с удалением элементов списка копий - медленно)
		// Fast = false: с удалением элементов списка копий - медленно. Fast = true: без удаления элементов списка копий - быстро
		private void deleteCheckedFb2( bool Fast ) {
			if ( listViewFB2Files.Items.Count > 0 && listViewFB2Files.CheckedItems.Count > 0 ) {
				const string sMessTitle = "SharpFBTools - Удаление копий книг";
				int nCount = listViewFB2Files.CheckedItems.Count;
				string sMess = "Вы действительно хотите удалить " + nCount.ToString() + " помеченных копии книг?";
				const MessageBoxButtons buttons = MessageBoxButtons.YesNo;
				if ( MessageBox.Show( sMess, sMessTitle, buttons, MessageBoxIcon.Question ) != DialogResult.No ) {
					listViewFB2Files.BeginUpdate();
					ConnectListsEventHandlers( false );
					Core.Duplicator.CopyMoveDeleteForm copyMoveDeleteForm = new Core.Duplicator.CopyMoveDeleteForm(
						Fast, BooksWorkModeEnum.DeleteCheckedBooks, tboxSourceDir.Text.Trim(), null,
						cboxExistFile.SelectedIndex, lvFilesCount, listViewFB2Files
					);
					copyMoveDeleteForm.ShowDialog();
					EndWorkMode EndWorkMode = copyMoveDeleteForm.EndMode;
					copyMoveDeleteForm.Dispose();
					ConnectListsEventHandlers( true );
					listViewFB2Files.EndUpdate();
					MessageBox.Show( EndWorkMode.Message, sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				}
			}
		}
		// Удалить всю Группу из Списка БЕЗ удаления книг с жесткого диска...
		private void deleteGroupNotRemoveFiles() {
			if ( listViewFB2Files.Items.Count > 0 && listViewFB2Files.CheckedItems.Count > 0 ) {
				const string MessTitle = "SharpFBTools - Удаление отмеченных Групп из списка";
				const string sMess = "Вы действительно хотите удалить все отмеченные Группы из списка копий (файлы с жесткого диска НЕ удаляются)?";
				const MessageBoxButtons buttons = MessageBoxButtons.YesNo;
				if ( MessageBox.Show( sMess, MessTitle, buttons, MessageBoxIcon.Question ) != DialogResult.No ) {
					listViewFB2Files.BeginUpdate();
					ConnectListsEventHandlers( false );
					
					foreach ( ListViewItem lvi in listViewFB2Files.CheckedItems ) {
						MiscListView.CheckAllListViewItemsInGroup(lvi.Group, true );
					}
					foreach ( ListViewItem lvi in listViewFB2Files.CheckedItems ) {
						MiscListView.deleteChechedItemsNotDeleteFiles( listViewFB2Files, lvFilesCount );
					}
					MiscListView.UnCheckAllListViewItems( listViewFB2Files.CheckedItems );
					
					ConnectListsEventHandlers( true );
					listViewFB2Files.EndUpdate();
					MessageBox.Show( "Удаление Групп из списка завершено.", MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				}
			}
		}
		// удаление оставшейся книги в группе и самой группы с контрола отображения (1 книга - это уже не копия)
		private void workingGroupItemAfterBookDelete( ListView listView, ListView FilesCount, ListViewGroup lvg ) {
			if ( lvg.Items.Count <= 1 ) {
				if ( lvg.Items.Count == 1 )
					listView.Items[lvg.Items[0].Index].Remove();
				listView.Groups.Remove( lvg );
			}
		}
		
		// очистка контролов вывода данных по книге по ее выбору
		private void clearDataFields() {
			for( int i = 0; i != lvTitleInfo.Items.Count; ++i ) {
				lvTitleInfo.Items[i].SubItems[1].Text		= string.Empty;
				lvSourceTitleInfo.Items[i].SubItems[1].Text	= string.Empty;
			}
			for( int i = 0; i != lvDocumentInfo.Items.Count; ++i )
				lvDocumentInfo.Items[i].SubItems[1].Text = string.Empty;

			for( int i = 0; i != lvPublishInfo.Items.Count; ++i )
				lvPublishInfo.Items[i].SubItems[1].Text = string.Empty;

			lvCustomInfo.Items.Clear();
			rtbHistory.Clear();
			rtbTIAnnotation.Clear();
			rtbSTIAnnotation.Clear();
			tbValidate.Clear();
			TICoversListView.Items.Clear();
			STICoversListView.Items.Clear();
			
			picBoxTICover.Image = imageListDup.Images[0];
			picBoxSTICover.Image = imageListDup.Images[0];
		}
		// отобразить метаданные данные после автокорректировки
		private string viewMetaData( string SrcFilePath, ListViewItem listViewItem, int BooksCount ) {
//			FilesWorker.RemoveDir(m_TempDir);
			string RetValid = string.Empty;
			if ( File.Exists( SrcFilePath ) && !listViewItem.Font.Strikeout ) {
				string FilePath = SrcFilePath;
				if ( FilesWorker.isFB2Archive( SrcFilePath ) )
					FilePath = ZipFB2Worker.getFileFromZipFBZ( SrcFilePath, m_TempDir );
				try {
					if ( FilesWorker.isFB2File( FilePath ) ) {
						FB2BookDescription fb2Desc = new FB2BookDescription( FilePath );
						RetValid = viewBookMetaDataLocal( ref SrcFilePath, ref fb2Desc, listViewItem );
						if ( BooksCount == 1 )
							viewBookMetaDataFull( ref fb2Desc, listViewItem );
					} else {
						WorksWithBooks.hideMetaDataLocalForDup( listViewItem );
						clearDataFields();
						FilesWorker.RemoveDir( m_TempDir );
					}
				} catch ( Exception ex ) {
					Debug.DebugMessage(
						SrcFilePath, ex, "Дубликатор.viewMetaData(): Отобразить метаданные данные после автокорректировки."
					);
					RetValid = "Нет";
					WorksWithBooks.hideMetaDataLocalForDup( listViewItem );
					clearDataFields();
				} finally {
					FilesWorker.RemoveDir( m_TempDir );
				}
			} else {
				WorksWithBooks.hideMetaDataLocalForDup( listViewItem );
				clearDataFields();
			}
			return RetValid;
		}
		// занесение данных в выделенный итем (метаданные книги после правки)
		private string viewBookMetaDataLocal( ref string SrcFilePath, ref FB2BookDescription bd, ListViewItem lvi ) {
			string RetValid = string.Empty;
			if( lvi != null ) {
				if( bd != null ) {
					lvi.SubItems[(int)ResultViewDupCollumnEnum.BookTitle].Text = bd.TIBookTitle;
					lvi.SubItems[(int)ResultViewDupCollumnEnum.Authors].Text = bd.TIAuthors;
					lvi.SubItems[(int)ResultViewDupCollumnEnum.Genres].Text = bd.TIGenres;
					lvi.SubItems[(int)ResultViewDupCollumnEnum.BookLang].Text = bd.TILang;
					lvi.SubItems[(int)ResultViewDupCollumnEnum.BookID].Text = bd.DIID;
					lvi.SubItems[(int)ResultViewDupCollumnEnum.Version].Text = bd.DIVersion;
					lvi.SubItems[(int)ResultViewDupCollumnEnum.Encoding].Text = bd.Encoding;

					string valid = bd.IsValidFB2Union;
					RetValid = valid;
					if ( !string.IsNullOrEmpty( valid ) ) {
						lvi.SubItems[(int)ResultViewDupCollumnEnum.Validate].Text = "Нет";
						lvi.ForeColor = Colors.FB2NotValidForeColor;
					} else {
						lvi.SubItems[(int)ResultViewDupCollumnEnum.Validate].Text = "Да";
						lvi.ForeColor = FilesWorker.isFB2File( SrcFilePath )
							? Colors.FB2ForeColor
							: Colors.ZipFB2ForeColor;
					}

					lvi.SubItems[(int)ResultViewDupCollumnEnum.FileLength].Text = bd.FileLength;
					lvi.SubItems[(int)ResultViewDupCollumnEnum.CreationTime].Text = bd.FileCreationTime;
					lvi.SubItems[(int)ResultViewDupCollumnEnum.LastWriteTime].Text = bd.FileLastWriteTime;
				}
			}
			return RetValid;
		}
		// занесение данных книги в контролы для просмотра
		private void viewBookMetaDataFull( ref FB2BookDescription fb2Desc, ListViewItem SelectedItem ) {
			if ( File.Exists( fb2Desc.FilePath ) && !SelectedItem.Font.Strikeout ) {
				// очистка контролов вывода данных по книге по ее выбору
				clearDataFields();
				try {
					if( fb2Desc != null ) {
						// загрузка обложек книги
						IList<BinaryBase64> Covers = fb2Desc.TICoversBase64;
						ImageWorker.makeListViewCoverNameItems( TICoversListView, ref Covers );
						if ( TICoversListView.Items.Count > 0 ) {
							TICoversListView.Items[0].Selected = true;
							TICoverListViewButtonPanel.Enabled = true;
						} else {
							picBoxTICover.Image = imageListDup.Images[0];
							TICoverListViewButtonPanel.Enabled = false;
						}
						
						// загруxзка обложек оригинала книги
						Covers = fb2Desc.STICoversBase64;
						ImageWorker.makeListViewCoverNameItems( STICoversListView, ref Covers );
						if ( STICoversListView.Items.Count > 0 ) {
							STICoversListView.Items[0].Selected = true;
							STICoverListViewButtonPanel.Enabled = true;
						} else {
							picBoxSTICover.Image = imageListDup.Images[0];
							STICoverListViewButtonPanel.Enabled = false;
						}
						
						// спиок жанров, в зависимости от схемы Жанров
						FB2UnionGenres fb2g = new FB2UnionGenres();
						
						// считываем данные TitleInfo
						MiscListView.ListViewStatus( lvTitleInfo, 0, fb2Desc.TIBookTitle );
						MiscListView.ListViewStatus( lvTitleInfo, 1, GenresWorker.cyrillicGenreNameAndCode( fb2Desc.TIGenres, ref fb2g ) );
						MiscListView.ListViewStatus( lvTitleInfo, 2, fb2Desc.TILang );
						MiscListView.ListViewStatus( lvTitleInfo, 3, fb2Desc.TISrcLang );
						MiscListView.ListViewStatus( lvTitleInfo, 4, fb2Desc.TIAuthors );
						MiscListView.ListViewStatus( lvTitleInfo, 5, fb2Desc.TIDate );
						MiscListView.ListViewStatus( lvTitleInfo, 6, fb2Desc.TIKeywords );
						MiscListView.ListViewStatus( lvTitleInfo, 7, fb2Desc.TITranslators );
						MiscListView.ListViewStatus( lvTitleInfo, 8, fb2Desc.TISequences );
						MiscListView.AutoResizeColumns( lvTitleInfo );
						// считываем данные SourceTitleInfo
						MiscListView.ListViewStatus( lvSourceTitleInfo, 0, fb2Desc.STIBookTitle );
						MiscListView.ListViewStatus( lvSourceTitleInfo, 1, GenresWorker.cyrillicGenreNameAndCode( fb2Desc.STIGenres, ref fb2g ) );
						MiscListView.ListViewStatus( lvSourceTitleInfo, 2, fb2Desc.STILang );
						MiscListView.ListViewStatus( lvSourceTitleInfo, 3, fb2Desc.STISrcLang );
						MiscListView.ListViewStatus( lvSourceTitleInfo, 4, fb2Desc.STIAuthors );
						MiscListView.ListViewStatus( lvSourceTitleInfo, 5, fb2Desc.STIDate );
						MiscListView.ListViewStatus( lvSourceTitleInfo, 6, fb2Desc.STIKeywords );
						MiscListView.ListViewStatus( lvSourceTitleInfo, 7, fb2Desc.STITranslators );
						MiscListView.ListViewStatus( lvSourceTitleInfo, 8, fb2Desc.STISequences );
						MiscListView.AutoResizeColumns( lvSourceTitleInfo );
						// считываем данные DocumentInfo
						MiscListView.ListViewStatus( lvDocumentInfo, 0, fb2Desc.DIID );
						MiscListView.ListViewStatus( lvDocumentInfo, 1, fb2Desc.DIVersion );
						MiscListView.ListViewStatus( lvDocumentInfo, 2, fb2Desc.DIFB2Date );
						MiscListView.ListViewStatus( lvDocumentInfo, 3, fb2Desc.DIProgramUsed );
						MiscListView.ListViewStatus( lvDocumentInfo, 4, fb2Desc.DISrcOcr );
						MiscListView.ListViewStatus( lvDocumentInfo, 5, fb2Desc.DISrcUrls );
						MiscListView.ListViewStatus( lvDocumentInfo, 6, fb2Desc.DIFB2Authors );
						MiscListView.AutoResizeColumns( lvDocumentInfo );
						// считываем данные PublishInfo
						MiscListView.ListViewStatus( lvPublishInfo, 0, fb2Desc.PIBookName );
						MiscListView.ListViewStatus( lvPublishInfo, 1, fb2Desc.PIPublisher );
						MiscListView.ListViewStatus( lvPublishInfo, 2, fb2Desc.PICity );
						MiscListView.ListViewStatus( lvPublishInfo, 3, fb2Desc.PIYear );
						MiscListView.ListViewStatus( lvPublishInfo, 4, fb2Desc.PIISBN );
						MiscListView.ListViewStatus( lvPublishInfo, 5, fb2Desc.PISequences );
						MiscListView.AutoResizeColumns( lvPublishInfo );
						// считываем данные CustomInfo
						lvCustomInfo.Items.Clear();
						IList<CustomInfo> lcu = fb2Desc.CICustomInfo;
						if ( lcu != null ) {
							foreach ( CustomInfo ci in lcu ) {
								ListViewItem lvi = new ListViewItem( ci.InfoType );
								lvi.SubItems.Add( ci.Value );
								lvCustomInfo.Items.Add( lvi );
							}
							MiscListView.AutoResizeColumns( lvCustomInfo );
						}
						// считываем данные History
						rtbHistory.Clear();
						rtbHistory.Text = StringProcessing.getDeleteAllTags( fb2Desc.DIHistory );
						// считываем данные Annotation
						rtbTIAnnotation.Clear();
						rtbTIAnnotation.Text = StringProcessing.getDeleteAllTags( fb2Desc.TIAnnotation );
						rtbSTIAnnotation.Clear();
						rtbSTIAnnotation.Text = StringProcessing.getDeleteAllTags( fb2Desc.STIAnnotation );
						// Валидность файла
						tbValidate.Clear();
						string Valid = WorksWithBooks.isValidate( fb2Desc.FilePath, tbValidate );
						if ( !string.IsNullOrEmpty( Valid ) )
							SelectedItem.SubItems[(int)ResultViewDupCollumnEnum.Validate].Text = "Нет";
						else
							SelectedItem.SubItems[(int)ResultViewDupCollumnEnum.Validate].Text = "Да";
						FilesWorker.RemoveDir( m_TempDir );
//						MiscListView.AutoResizeColumns(listViewFB2Files);
					}
				} catch ( Exception ex ) {
					Debug.DebugMessage(
						fb2Desc.FilePath, ex, "Дубликатор.viewBookMetaDataFull(): Занесение данных книги в контролы для просмотра."
					);
					MessageBox.Show(
						"Ошибка при отображении метаданных книги " + fb2Desc.FilePath + "\n" + ex.Message, "Отображение метаданных книги", MessageBoxButtons.OK, MessageBoxIcon.Error
					);
				}
			} else
				clearDataFields();
		}
		
		// отобразить метаданные данные после массовой обработки книг
		private string viewMetaDataAfterWorkManyBooks( IList<ListViewItemInfo> ListViewItemInfoList,  BooksValidateModeEnum BooksValidateType ) {
			string Valid = string.Empty;
			int BooksCount = 0;
			if ( BooksValidateType == BooksValidateModeEnum.SelectedBooks )
				BooksCount = listViewFB2Files.SelectedItems.Count;
			else if ( BooksValidateType == BooksValidateModeEnum.CheckedBooks )
				BooksCount = listViewFB2Files.CheckedItems.Count;
			else /* BooksValidateType == BooksValidateMode.AllBooks */
				BooksCount = listViewFB2Files.Items.Count;
			
			tsProgressBar.Maximum = BooksCount;
			tsProgressBar.Value = 0;
			foreach( ListViewItemInfo Info in ListViewItemInfoList ) {
				Valid = viewMetaData( Info.FilePathSource, Info.ListViewItem, BooksCount );
				++tsProgressBar.Value;
			}
			tsProgressBar.Value = 0;
			return Valid;
		}
		// отобразить метаданные данные после массовой обработки книг (обработка в диалоге)
		private void viewMetaDataAfterDialogWorkManyBooks( IList<FB2ItemInfo> ListViewItemInfoList,  BooksValidateModeEnum BooksValidateType ) {
			int BooksCount = 0;
			if ( BooksValidateType == BooksValidateModeEnum.SelectedBooks )
				BooksCount = listViewFB2Files.SelectedItems.Count;
			else if ( BooksValidateType == BooksValidateModeEnum.CheckedBooks )
				BooksCount = listViewFB2Files.CheckedItems.Count;
			else /* BooksValidateType == BooksValidateMode.AllBooks */
				BooksCount = listViewFB2Files.Items.Count;
			
			tsProgressBar.Maximum = BooksCount;
			tsProgressBar.Value = 0;
			foreach( FB2ItemInfo Info in ListViewItemInfoList ) {
				viewMetaData( Info.FilePathSource, Info.FB2ListViewItem, BooksCount );
				++tsProgressBar.Value;
			}
			tsProgressBar.Value = 0;
		}
		
		// правка книги в текстовом редакторе / FBE
		private void editFB2InProgram( string ProgPath, string FilePath, string Title ) {
			if( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count != 0 ) {
				ListViewItem SelectedItem = listViewFB2Files.SelectedItems[0];
				if( !File.Exists( FilePath ) ) {
					MessageBox.Show( "Файл: " + FilePath + "\" не найден!", Title, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				} else {
					// правка fb2 и перепаковка fb2 из zip, fbz
					ZipFB2Worker.StartFB2_FBZForEdit( FilePath, ProgPath );
					Cursor.Current = Cursors.WaitCursor;
					// занесение данных в выделенный итем (метаданные книги после правки)
					viewMetaData( FilePath, SelectedItem, 1 );
					Cursor.Current = Cursors.Default;
				}
			}
		}
		// генерация нового id для выделенной книги
		// BooksCount > 1 - обработка для нескольких книг в цикле вызывающего кода
		private void setNewBookID( ListViewItem SelectedItem, int BooksCount ) {
			string SourceFilePath = SelectedItem.Text;
			string FilePath = SourceFilePath;
			bool IsFromZip = false;
			if ( FilesWorker.isFB2Archive( SourceFilePath ) ) {
				IsFromZip = true;
				FilePath = ZipFB2Worker.getFileFromZipFBZ( SourceFilePath, m_TempDir );
			}
			if( File.Exists( FilePath ) ) {
				FictionBook fb2 = null;
				try {
					fb2 = new FictionBook( FilePath );
				} catch ( FileLoadException ex ) {
					Debug.DebugMessage(
						SourceFilePath, ex, "Дубликатор.setNewBookID(): Генерация нового id для выделенной книги. Ошибка открытия файла."
					);
					if ( BooksCount == 1 )
						MessageBox.Show(
							ex.Message, "Генерация нового id", MessageBoxButtons.OK, MessageBoxIcon.Error
						);
					return;
				}
				if( fb2 != null ) {
					// восстанавление раздела description до структуры с необходимыми элементами для валидности
					FB2DescriptionCorrector fB2Corrector = new FB2DescriptionCorrector( fb2 );
					WorksWithBooks.recoveryFB2Structure( ref fB2Corrector, SelectedItem, SourceFilePath );
					fB2Corrector.setNewID();
					fb2.saveToFB2File( FilePath, false );
					if ( IsFromZip )
						WorksWithBooks.zipMoveTempFB2FileTo( m_sharpZipLib, FilePath, SourceFilePath );
					if ( IsFromZip && File.Exists( FilePath ) )
						File.Delete( FilePath );
					// отображение нового id в строке списка
					viewMetaData( SourceFilePath, SelectedItem, BooksCount );
				}
			}
		}
		// правка Авторов выделенных/помеченных книг
		private void editAuthors( BooksValidateModeEnum BooksValidateType ) {
			// создание списка FB2ItemInfo данных для выбранных книг
			Cursor.Current = Cursors.WaitCursor;
			IList<FB2ItemInfo> AuthorFB2InfoList = WorksWithBooks.makeFB2InfoListForDup( listViewFB2Files, BooksValidateType );
			Cursor.Current = Cursors.Default;
			
			EditAuthorInfoForm editAuthorInfoForm = new EditAuthorInfoForm( ref AuthorFB2InfoList );
			editAuthorInfoForm.ShowDialog();
			
			Cursor.Current = Cursors.WaitCursor;
			if( editAuthorInfoForm.isApplyData() )
				viewMetaDataAfterDialogWorkManyBooks( AuthorFB2InfoList,  BooksValidateType );
			editAuthorInfoForm.Dispose();
			FilesWorker.RemoveDir( m_TempDir );
			Cursor.Current = Cursors.Default;
		}
		// правка Жанров выделенных/помеченных книг
		private void editGenres( BooksValidateModeEnum BooksValidateType ) {
			// создание списка FB2ItemInfo данных для выбранных книг
			Cursor.Current = Cursors.WaitCursor;
			IList<FB2ItemInfo> GenreFB2InfoList = WorksWithBooks.makeFB2InfoListForDup( listViewFB2Files, BooksValidateType );
			Cursor.Current = Cursors.Default;
			
			EditGenreInfoForm editGenreInfoForm =
				new EditGenreInfoForm( ref GenreFB2InfoList );
			editGenreInfoForm.ShowDialog();

			Cursor.Current = Cursors.WaitCursor;
			if( editGenreInfoForm.isApplyData() )
				viewMetaDataAfterDialogWorkManyBooks( GenreFB2InfoList,  BooksValidateType );
			editGenreInfoForm.Dispose();
			FilesWorker.RemoveDir( m_TempDir );
			Cursor.Current = Cursors.Default;
		}
		#endregion
		
		// =============================================================================================
		// 									ОБРАБОТЧИКИ событий контролов
		// =============================================================================================
		#region Обработчики событий контролов
		void TboxSourceDirTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		void BtnOpenDirClick(object sender, EventArgs e)
		{
			FilesWorker.OpenDirDlg( tboxSourceDir, fbdScanDir, "Укажите папку для сканирования с fb2 файлами:" );
		}
		// Поиск одинаковых fb2-файлов
		void TsbtnSearchDublsClick(object sender, EventArgs e)
		{
			const string sMessTitle = "SharpFBTools - Поиск одинаковых fb2 файлов";
			// проверка на корректность данных папок источника и приемника файлов
			if( !isScanFolderDataCorrect( tboxSourceDir, sMessTitle ) )
				return;
			
			// инициализация контролов
			init();
			
			// удаляем log файл, если режим добавления в log
			if ( ! Settings.Settings.AppendToLog )
				if ( File.Exists( Debug.LogFilePath ) )
					File.Delete( Debug.LogFilePath );
			
			ConnectListsEventHandlers( false );
			listViewFB2Files.BeginUpdate();
			tsslblProgress.Text = "=> Поиск копий: ";
			Core.Duplicator.CompareForm comrareForm = new Core.Duplicator.CompareForm(
				null,
				ref tboxSourceDir, ref chBoxScanSubDir, ref cboxMode,
				ref checkBoxSaveGroupsToXml, ref tscbGroupCountForList,
				ref lvFilesCount, ref listViewFB2Files, false
			);
			comrareForm.ShowDialog();
			EndWorkMode EndWorkMode = comrareForm.EndMode;
			comrareForm.Dispose();
			listViewFB2Files.EndUpdate();
			ConnectListsEventHandlers( true );
			MessageBox.Show( EndWorkMode.Message, sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			tsbtnDupCurrentSaveList.Enabled = false; // кнопка сохранения текущего списка без подтверждения
		}
		// ВОЗОБНОВТЬ сравнение и поиск копий по данным из XML-файла, созданного после прерывания обработки
		void TsbtnSearchFb2DupRenewClick(object sender, EventArgs e)
		{
			// загрузка данных из xml
			sfdLoadList.InitialDirectory = Settings.Settings.ProgDir;
			sfdLoadList.Title		= "Укажите файл для возобновления поиска копий книг:";
			sfdLoadList.Filter		= "SharpFBTools Файлы хода работы Дубликатора (*.dup_break)|*.dup_break";
			sfdLoadList.FileName	= string.Empty;
			DialogResult result		= sfdLoadList.ShowDialog();
			if ( result == DialogResult.OK ) {
				Cursor.Current = Cursors.WaitCursor;
				tsslblProgress.Text = string.Format("=> {0} : ", sfdLoadList.FileName);
			} else
				return;
			
			// инициализация контролов
			init();
			ConnectListsEventHandlers( false );
			
			// удаляем log файл, если режим добавления в log
			if ( ! Settings.Settings.AppendToLog )
				if ( File.Exists( Debug.LogFilePath ) )
					File.Delete( Debug.LogFilePath );
			
			listViewFB2Files.BeginUpdate();
			Core.Duplicator.CompareForm comrareForm = new Core.Duplicator.CompareForm(
				sfdLoadList.FileName,
				ref tboxSourceDir, ref chBoxScanSubDir, ref cboxMode,
				ref checkBoxSaveGroupsToXml, ref tscbGroupCountForList,
				ref lvFilesCount, ref listViewFB2Files, false
			);
			Cursor.Current = Cursors.Default;
			comrareForm.ShowDialog();
			tboxSourceDir.Text = comrareForm.getSourceDirFromRenew();
			EndWorkMode EndWorkMode = comrareForm.EndMode;
			comrareForm.Dispose();
			listViewFB2Files.EndUpdate();
			ConnectListsEventHandlers( true );
			tsslblProgress.Text = "=>";
			MessageBox.Show( EndWorkMode.Message, "SharpFBTools - Поиск одинаковых fb2 файлов", MessageBoxButtons.OK, MessageBoxIcon.Information );
			tsbtnDupCurrentSaveList.Enabled = false; // кнопка сохранения текущего списка без подтверждения
		}
		void LvResultKeyPress(object sender, KeyPressEventArgs e)
		{
			if ( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count != 0 ) {
				if ( e.KeyChar == (char)Keys.Return ) {
					if ( listViewFB2Files.SelectedItems.Count == 1 )
						goHandlerWorker( cboxPressEnterForFB2, sender, e );
				}
			}
			e.Handled = true;
		}
		void LvResultDoubleClick(object sender, EventArgs e)
		{
			if ( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count != 0 ) {
				if ( listViewFB2Files.SelectedItems.Count == 1 )
					goHandlerWorker( cboxDblClickForFB2, sender, e );
			}
		}
		
		// занесение данных книги в контролы для просмотра
		void LvResultSelectedIndexChanged(object sender, EventArgs e)
		{
			// пропускаем ситуацию, когда курсор переходит от одной строки к другой - нет выбранного item'а
			if ( listViewFB2Files.SelectedItems.Count > 0 ) {
				ListViewItem SelectedItem = listViewFB2Files.SelectedItems[0];
				if ( SelectedItem != null ) {
					TICoverDPILabel.Text = STICoverDPILabel.Text = "DPI";
					TICoverPixelsLabel.Text = STICoverPixelsLabel.Text = "В пикселах";
					TICoverLenghtLabel.Text = STICoverLenghtLabel.Text = "Размер";
					
					// защита от двойного срабатывания
					if( m_CurrentResultItem != SelectedItem.Index & listViewFB2Files.SelectedItems.Count == 1 ) {
						m_CurrentResultItem = SelectedItem.Index;
						// отображение новых метаданных в строке списка и в детализации
						string SrcFilePath = SelectedItem.Text;
						string FilePath = SrcFilePath;
						if ( File.Exists( SrcFilePath ) && !SelectedItem.Font.Strikeout ) {
							if ( FilesWorker.isFB2Archive( SrcFilePath ) )
								FilePath = ZipFB2Worker.getFileFromZipFBZ( SrcFilePath, m_TempDir );
							try {
								if ( FilesWorker.isFB2File( FilePath ) ) {
									FB2BookDescription fb2Desc = new FB2BookDescription( FilePath );
									viewBookMetaDataFull( ref fb2Desc, SelectedItem );
								} else {
									WorksWithBooks.hideMetaDataLocalForDup( SelectedItem );
									clearDataFields();
								}
							} catch ( Exception ex ) {
								Debug.DebugMessage(
									SrcFilePath, ex, "Дубликатор.LvResultSelectedIndexChanged(): Занесение данных книги в контролы для просмотра."
								);
								WorksWithBooks.hideMetaDataLocalForDup( SelectedItem );
								clearDataFields();
								// Занесение данных о валидации в поле детализации
								WorksWithBooks.isValidate( SrcFilePath, tbValidate );
							}
							FilesWorker.RemoveDir( m_TempDir );
						} else {
							WorksWithBooks.hideMetaDataLocalForDup( SelectedItem );
							clearDataFields();
						}
					}
				}
			}
		}

		// запуск сканирования по нажатию Enter на поле ввода папки для сканирования
		void TboxSourceDirKeyPress(object sender, KeyPressEventArgs e)
		{
			if ( e.KeyChar == (char)Keys.Return )
				TsbtnSearchDublsClick( sender, e );
		}
		// сохранение списка найденных копий
		void TsbtnDupSaveListClick(object sender, EventArgs e)
		{
			const string MessTitle = "SharpFBTools - Сохранение списка копий fb2 книг";
			if ( listViewFB2Files.Items.Count == 0 ) {
				MessageBox.Show( "Нет ни одной копии fb2 книг!", MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			
			DialogResult result = fbdSaveList.ShowDialog();
			if (result == DialogResult.OK) {
				listViewFB2Files.BeginUpdate();
				ConnectListsEventHandlers( false );
				// удаление всех элементов Списка, для которых отсутствуют файлы на жестком диске (защита от сохранения пустых Групп)
				MiscListView.deleteAllItemForNonExistFile( listViewFB2Files );
				Environment.CurrentDirectory = Settings.Settings.ProgDir;
				Core.Duplicator.CopiesListWorkerForm fileWorkerForm = new Core.Duplicator.CopiesListWorkerForm(
					BooksWorkModeEnum.SaveFB2List, fbdSaveList.SelectedPath, cboxMode,
					listViewFB2Files, lvFilesCount, tboxSourceDir,
					chBoxScanSubDir,
					listViewFB2Files.SelectedItems.Count >= 1 ? listViewFB2Files.SelectedItems[0].Index : -1,
					Convert.ToInt16( tscbGroupCountForList.Text )
				);
				fileWorkerForm.ShowDialog();
				EndWorkMode EndWorkMode = fileWorkerForm.EndMode;
				fileWorkerForm.Dispose();
				ConnectListsEventHandlers( true );
				listViewFB2Files.EndUpdate();
				MessageBox.Show( EndWorkMode.Message, MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
		}
		// сохранение текущего обрабатываемого списка без запроса на подтверждение и пути сохранения
		void TsbtnDupCurrentSaveListClick(object sender, EventArgs e)
		{
			string FilePath = tsslblProgress.Text.Substring( tsslblProgress.Text.IndexOf(':') + 1 );
			listViewFB2Files.BeginUpdate();
			ConnectListsEventHandlers( false );
			// удаление всех элементов Списка, для которых отсутствуют файлы на жестком диске (защита от сохранения пустых Групп)
			MiscListView.deleteAllItemForNonExistFile( listViewFB2Files );
			Environment.CurrentDirectory = Settings.Settings.ProgDir;
			Core.Duplicator.CopiesListWorkerForm fileWorkerForm = new Core.Duplicator.CopiesListWorkerForm(
				BooksWorkModeEnum.SaveWorkingFB2List, FilePath.Trim(), cboxMode,
				listViewFB2Files, lvFilesCount, tboxSourceDir,
				chBoxScanSubDir,
				listViewFB2Files.SelectedItems.Count >= 1 ? listViewFB2Files.SelectedItems[0].Index : -1,
				Convert.ToInt16( tscbGroupCountForList.Text )
			);
			fileWorkerForm.ShowDialog();
			EndWorkMode EndWorkMode = fileWorkerForm.EndMode;
			fileWorkerForm.Dispose();
			ConnectListsEventHandlers( true );
			listViewFB2Files.EndUpdate();
			MessageBox.Show( EndWorkMode.Message, "SharpFBTools - Сохранение обрабатываемого списка копий", MessageBoxButtons.OK, MessageBoxIcon.Information );
		}
		// загрузка списка копий
		void TsbtnDupOpenListClick(object sender, EventArgs e)
		{
			sfdLoadList.InitialDirectory = Settings.Settings.ProgDir;
			sfdLoadList.Title 		= "Загрузка Списка копий книг:";
			sfdLoadList.Filter		= "SharpFBTools Файлы копий книг (*.dup_lbc)|*.dup_lbc";
			sfdLoadList.FileName	= string.Empty;
			string FromXML = string.Empty;
			DialogResult result = sfdLoadList.ShowDialog();
			if( result == DialogResult.OK ) {
				const string MessTitle = "SharpFBTools - Загрузка Списка копий книг";
				FromXML = sfdLoadList.FileName;
				// инициализация контролов
				init();
				// установка режима поиска
				if( !File.Exists( FromXML ) ) {
					MessageBox.Show(
						"Не найден файл списка копий fb2 книг: \""+FromXML+"\"!", MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning
					);
					return;
				}
				try {
					// отключаем обработчики событий для listViewFB2Files (убираем "тормоза")
					ConnectListsEventHandlers( false );
					listViewFB2Files.BeginUpdate();
					
					// удаляем log файл, если режим добавления в log
					if ( ! Settings.Settings.AppendToLog )
						if ( File.Exists( Debug.LogFilePath ) )
							File.Delete( Debug.LogFilePath );
					
					Environment.CurrentDirectory = Settings.Settings.ProgDir;
					tsslblProgress.Text = "=> Файл копий книг: " + FromXML;
					Core.Duplicator.CopiesListWorkerForm fileWorkerForm = new Core.Duplicator.CopiesListWorkerForm(
						BooksWorkModeEnum.LoadFB2List, FromXML, cboxMode, listViewFB2Files, lvFilesCount,
						tboxSourceDir, chBoxScanSubDir, -1, 0
					);
					fileWorkerForm.ShowDialog();
					EndWorkMode EndWorkMode = fileWorkerForm.EndMode;
					int SelectedItem = fileWorkerForm.LastSelectedItem > -1 ? fileWorkerForm.LastSelectedItem : 0;
					fileWorkerForm.Dispose();
					listViewFB2Files.EndUpdate();
					ConnectListsEventHandlers( true );
					// отображение метаданных
					if ( listViewFB2Files.Items.Count > 0 )
						viewMetaData( listViewFB2Files.Items[SelectedItem].Text.Trim(), listViewFB2Files.Items[SelectedItem], 1 );
					MessageBox.Show( EndWorkMode.Message, MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					tsbtnDupCurrentSaveList.Enabled = true; // кнопка сохранения текущего списка без подтверждения
					listViewFB2Files.Focus();
				} catch ( Exception ex ) {
					Debug.DebugMessage(
						null, ex, "Дубликатор.TsbtnDupOpenListClick(): Загрузка списка копий."
					);
					listViewFB2Files.EndUpdate();
					ConnectListsEventHandlers( true );
					MessageBox.Show( "Возможно, поврежден файл списка копий fb2 книг: \""+FromXML+"\"!\n"+ex.Message, MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Error );
				}
			}
		}
		void CboxModeSelectedIndexChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		void CboxExistFileSelectedIndexChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		void CboxDblClickForFB2SelectedIndexChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		void CboxPressEnterForFB2SelectedIndexChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		void ChBoxScanSubDirCheckStateChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
        void LvResultColumnClick(object sender, ColumnClickEventArgs e)
		{
			if ( e.Column == m_lvwColumnSorter.SortColumn ) {
				// Изменить сортировку на обратную для выбранного столбца
				m_lvwColumnSorter.Order = ( m_lvwColumnSorter.Order == SortOrder.Ascending )
					? SortOrder.Descending : SortOrder.Ascending;
			} else {
				// Задать номер столбца для сортировки (по-умолчанию Ascending)
				m_lvwColumnSorter.SortColumn = e.Column;
				m_lvwColumnSorter.Order = SortOrder.Ascending;
			}
			listViewFB2Files.ListViewItemSorter = m_lvwColumnSorter; // перед listViewFB2Files.Sort(); иначе - "тормоза"
			listViewFB2Files.Sort();
		}
		// отображение обложек книги
		void TICoversListViewSelectedIndexChanged(object sender, EventArgs e)
		{
			WorksWithBooks.viewCover( TICoversListView, picBoxTICover, TICoverDPILabel, TICoverPixelsLabel, TICoverLenghtLabel );
		}
		// сохранение выделенных обложек на диск
		void TISaveSelectedCoverButtonClick(object sender, EventArgs e)
		{
			ImageWorker.saveSelectedCovers( TICoversListView, ref m_DirForSavedCover, "Сохранение обложек на диск", fbdScanDir );
		}
		// отображение обложек Оригинала
		void STICoversListViewSelectedIndexChanged(object sender, EventArgs e)
		{
			WorksWithBooks.viewCover( STICoversListView, picBoxSTICover, STICoverDPILabel, STICoverPixelsLabel, STICoverLenghtLabel );
		}
		void STISaveSelectedCoverButtonClick(object sender, EventArgs e)
		{
			// сохранение выделенных обложек на диск
			ImageWorker.saveSelectedCovers( STICoversListView, ref m_DirForSavedCover, "Сохранение обложек на диск", fbdScanDir );
		}
		#endregion
		
		// =============================================================================================
		// 								ОБРАБОТЧИКИ для контекстного меню
		// =============================================================================================
		#region Обработчики для контекстного меню
		// Открыть папку для выделенного файла
		void TsmiOpenFileDirClick(object sender, EventArgs e)
		{
			if ( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = listViewFB2Files.SelectedItems;
				FileInfo fi = new FileInfo( si[0].SubItems[0].Text.Split('/')[0] );
				string sDir = fi.Directory.ToString();
				if ( !Directory.Exists( sDir ) ) {
					MessageBox.Show( "Папка: \""+sDir+"\" не найдена!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				FilesWorker.ShowDir( true, sDir );
			}
		}
		
		// запустить файл в fb2-читалке (Просмотр)
		void TsmiViewInReaderClick(object sender, EventArgs e)
		{
			if ( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count != 0 ) {
				// читаем путь к читалке из настроек
				string sFBReaderPath = Settings.Settings.FBReaderPath;
				const string sTitle = "SharpFBTools - Открытие папки для файла";
				if ( !File.Exists( sFBReaderPath ) ) {
					MessageBox.Show( "Не могу найти Читалку \""+sFBReaderPath+"\"!\nПроверьте, правильно ли задан путь в Настройках.", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				ListView.SelectedListViewItemCollection si = listViewFB2Files.SelectedItems;
				string sFilePath = si[0].SubItems[0].Text.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: \""+sFilePath+"\" не найден!", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				FilesWorker.StartFile( true, sFBReaderPath, sFilePath );
			}
		}
		// Правка Названия Книги
		void TsmiEditBookNameClick(object sender, EventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 ) {
				if( listViewFB2Files.SelectedItems.Count == 1 ) {
					string SourceFilePath = listViewFB2Files.SelectedItems[0].Text;
					string FilePath = SourceFilePath;
					bool IsFromZip = false;
					if ( FilesWorker.isFB2Archive( SourceFilePath ) ) {
						IsFromZip = true;
						FilePath = ZipFB2Worker.getFileFromZipFBZ( SourceFilePath, m_TempDir );
					}
					if( File.Exists( FilePath ) ) {
						FictionBook fb2 = null;
						try {
							fb2 = new FictionBook( FilePath );
						} catch ( FileLoadException ex ) {
							Debug.DebugMessage(
								null, ex, "Дубликатор.TsmiEditBookNameClick(): Правка Названия Книги. Ошибка открытия книги."
							);
							MessageBox.Show(
								ex.Message, "Правка Названия книги", MessageBoxButtons.OK, MessageBoxIcon.Error
							);
							return;
						}
						if( fb2 != null ) {
							string BookTitleNew = fb2.TIBookTitle != null ? fb2.TIBookTitle.Value : "Новое название книги";
							if ( WorksWithBooks.InputBox( "Правка названия книги", "Новое название книги:", ref BookTitleNew ) == DialogResult.OK) {
								// восстанавление раздела description до структуры с необходимыми элементами для валидности
								FB2DescriptionCorrector fB2Corrector = new FB2DescriptionCorrector( fb2 );
								WorksWithBooks.recoveryFB2Structure( ref fB2Corrector, listViewFB2Files.SelectedItems[0], SourceFilePath );
								fB2Corrector.setNewBookTitle( BookTitleNew );
								fb2.saveToFB2File( FilePath, false );
								if ( IsFromZip )
									WorksWithBooks.zipMoveTempFB2FileTo( m_sharpZipLib, FilePath, SourceFilePath );
								if ( IsFromZip && File.Exists( FilePath ) )
									File.Delete( FilePath );
								// отображение нового названия книги в строке списка
								viewMetaData( SourceFilePath, listViewFB2Files.SelectedItems[0], 1 );
							}
						}
					}
				} else {
					MessageBox.Show( "Выделите только одну книгу для изменения ее Названия",
					                "Задание нового Названия книги", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				}
			}
		}
		// комплексное редактирование метаданных в специальном диалоге
		void TsmiEditDescriptionClick(object sender, EventArgs e)
		{
			if ( listViewFB2Files.Items.Count > 0 ) {
				if ( listViewFB2Files.SelectedItems.Count == 1 ) {
					ListViewItem SelectedItem = listViewFB2Files.SelectedItems[0];
					if ( SelectedItem != null ) {
						string SourceFilePath = SelectedItem.Text;
						string FilePath = SourceFilePath;
						bool IsFromZip = false;
						if ( FilesWorker.isFB2Archive( SourceFilePath ) ) {
							IsFromZip = true;
							FilePath = ZipFB2Worker.getFileFromZipFBZ( SourceFilePath, m_TempDir );
						}
						if ( File.Exists( FilePath ) ) {
							FictionBook fb2 = null;
							try {
								fb2 = new FictionBook( FilePath );
							} catch ( FileLoadException ex ) {
								Debug.DebugMessage(
									SourceFilePath, ex, "Дубликатор.TsmiEditDescriptionClick(): Комплексное редактирование метаданных в специальном диалоге. Ошибка открытия книги."
								);
								MessageBox.Show(
									ex.Message, "Комплексная правка метаданных", MessageBoxButtons.OK, MessageBoxIcon.Error
								);
								return;
							}
							EditDescriptionForm editDescriptionForm = new EditDescriptionForm( fb2 );
							editDescriptionForm.ShowDialog();
							Cursor.Current = Cursors.WaitCursor;
							if ( editDescriptionForm.isApplyData() ) {
								editDescriptionForm.getFB2().saveToFB2File( FilePath, false );
								if ( IsFromZip )
									WorksWithBooks.zipMoveTempFB2FileTo( m_sharpZipLib, FilePath, SourceFilePath );
								if ( IsFromZip && File.Exists( FilePath ) )
									File.Delete( FilePath );
								// отображение нового названия книги в строке списка
								viewMetaData( SourceFilePath, SelectedItem, 1 );
							}
							editDescriptionForm.Dispose();
							Cursor.Current = Cursors.Default;
						}
					}
				} else {
					MessageBox.Show( "Выделите только одну книгу для изменения всех ее метаданных.",
					                "Комплексная правка метаданных", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				}
			}
		}
		
		// редактировать выделенный файл в fb2-редакторе
		void TsmiEditInFB2EditorClick(object sender, EventArgs e)
		{
			if ( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count != 0 ) {
				// читаем путь к FBE из настроек
				string FBEPath = Settings.Settings.FB2EditorPath;
				const string Title = "SharpFBTools - Открытие файла в fb2-редакторе";
				if ( !File.Exists( FBEPath ) ) {
					MessageBox.Show( "Не могу найти fb2 редактор \""+FBEPath+"\"!\nПроверьте, правильно ли задан путь в Настройках.", Title, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				editFB2InProgram( FBEPath, listViewFB2Files.SelectedItems[0].Text.Trim(), Title );
			}
		}
		
		// редактировать выделенный файл в текстовом редакторе
		void TsmiEditInTextEditorClick(object sender, EventArgs e)
		{
			if ( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count != 0 ) {
				// читаем путь к текстовому редактору из настроек
				string TextEditorPath = Settings.Settings.TextEditorPath;
				const string Title = "SharpFBTools - Открытие файла в текстовом редакторе";
				if ( !File.Exists( TextEditorPath ) ) {
					MessageBox.Show( "Не могу найти текстовый редактор \""+TextEditorPath+"\"!\nПроверьте, правильно ли задан путь в Настройках.", Title, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				editFB2InProgram( TextEditorPath, listViewFB2Files.SelectedItems[0].Text.Trim(), Title );
			}
		}
		
		// Повторная Проверка выбранного fb2-файла (Валидация)
		void TsmiFileReValidateClick(object sender, EventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count != 0 ) {
				DateTime dtStart = DateTime.Now;
				ListViewItem SelectedItem = listViewFB2Files.SelectedItems[0];
				string sSelectedItemText = SelectedItem.SubItems[(int)ResultViewDupCollumnEnum.Path].Text;
				string sFilePath = sSelectedItemText.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: \""+sFilePath+"\" не найден!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				MessageBoxIcon mbi = MessageBoxIcon.Information;
				string Msg		= string.Empty;
				string ErrorMsg	= "СООБЩЕНИЕ ОБ ОШИБКЕ:";
				string OkMsg	= "ОШИБОК НЕТ - ФАЙЛ ВАЛИДЕН";
				
				Msg = m_fv2Validator.ValidatingFB2File( sFilePath );

				// отображение метаданных после правки книги
				viewMetaData( sFilePath, SelectedItem, 1 );
				
				if ( Msg == string.Empty ) {
					// файл валидный
					mbi = MessageBoxIcon.Information;
					ErrorMsg = OkMsg;
					SelectedItem.SubItems[(int)ResultViewDupCollumnEnum.Validate].Text = "Да";
					SelectedItem.ForeColor = FilesWorker.isFB2File( sFilePath )
						? Colors.FB2ForeColor
						: Colors.ZipFB2ForeColor;
					tbValidate.Text = "Все в порядке - файл валидный!";
				} else {
					// файл не валидный
					mbi = MessageBoxIcon.Error;
					SelectedItem.SubItems[(int)ResultViewDupCollumnEnum.Validate].Text = "Нет";
					SelectedItem.ForeColor = Colors.FB2NotValidForeColor;
					tbValidate.Text = "Файл невалидный. Ошибка:";
					tbValidate.AppendText( Environment.NewLine );
					tbValidate.AppendText( Environment.NewLine );
					tbValidate.AppendText( Msg );
				}
				
				DateTime dtEnd = DateTime.Now;
				string sTime = dtEnd.Subtract( dtStart ).ToString() + " (час.:мин.:сек.)";
				FilesWorker.RemoveDir( m_TempDir );
				MessageBox.Show( "Проверка выделенного файла на соответствие FictionBook.xsd схеме завершена.\nЗатрачено времени: "+sTime+"\n\nФайл: \""+sFilePath+"\"\n\n"+ErrorMsg+"\n"+Msg, "SharpFBTools - "+ErrorMsg, MessageBoxButtons.OK, mbi );
			}
		}
		// Повторная Проверка всех fb2-файлов одной Группы (Валидация)
		void TsmiAllFilesInGroupReValidateClick(object sender, EventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 ) {
				Core.Duplicator.ValidatorForm validatorForm = new Core.Duplicator.ValidatorForm(
					GroupAnalyzeModeEnum.Group, listViewFB2Files
				);
				validatorForm.ShowDialog();
				EndWorkMode EndWorkMode = validatorForm.EndMode;
				validatorForm.Dispose();
				
				Cursor.Current = Cursors.WaitCursor;
				ListViewGroup lvg = listViewFB2Files.SelectedItems[0].Group;
				tsProgressBar.Maximum = lvg.Items.Count;
				tsProgressBar.Value = 0;
				foreach( ListViewItem Item in lvg.Items ) {
					viewMetaData( Item.Text.Trim(), Item, lvg.Items.Count );
					tsProgressBar.Value++;
				}
				tsProgressBar.Value = 0;
				
//				MiscListView.AutoResizeColumns( listViewFB2Files );
				Cursor.Current = Cursors.Default;
				MessageBox.Show( EndWorkMode.Message, "SharpFBTools - Валидация всех книг выбранной Группы", MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
		}
		// Повторная Проверка всех fb2-файлов всех Групп (Валидация)
		void TsmiAllGroupsReValidateClick(object sender, EventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 ) {
				Core.Duplicator.ValidatorForm validatorForm = new Core.Duplicator.ValidatorForm(
					GroupAnalyzeModeEnum.AllGroup, listViewFB2Files
				);
				validatorForm.ShowDialog();
				EndWorkMode EndWorkMode = validatorForm.EndMode;
				validatorForm.Dispose();
				
				Cursor.Current = Cursors.WaitCursor;
				tsProgressBar.Maximum = listViewFB2Files.Items.Count;
				tsProgressBar.Value = 0;
				foreach( ListViewItem Item in listViewFB2Files.Items ) {
					viewMetaData( Item.Text.Trim(), Item, listViewFB2Files.Items.Count );
					tsProgressBar.Value++;
				}
				tsProgressBar.Value = 0;

//				MiscListView.AutoResizeColumns( listViewFB2Files );
				Cursor.Current = Cursors.Default;
				MessageBox.Show( EndWorkMode.Message, "SharpFBTools - Валидация всех книг всех Групп", MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
		}
		
		// diff - две помеченные fb2-книги
		void TsmiDiffFB2Click(object sender, EventArgs e)
		{
			if ( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count != 0 ) {
				ListViewItem SelectedItem = listViewFB2Files.SelectedItems[0];
				// проверка на наличие diff-программы
				const string sDiffTitle = "SharpFBTools - diff";
				string sDiffPath = Settings.Settings.DiffToolPath;
				
				if ( sDiffPath.Trim().Length == 0 ) {
					MessageBox.Show( "В Настройках не указан путь к установленной diff-программе визуального сравнения файлов!\nУкажите путь к ней в Настройках.\nРабота остановлена!",
					                sDiffTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return;
				} else {
					if ( !File.Exists( sDiffPath ) ) {
						MessageBox.Show( "Не найден файл diff-программы визуального сравнения файлов \""+sDiffPath+"\"!\nУкажите путь к ней в Настройках.\nРабота остановлена!",
						                sDiffTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
						return;
					}
				}

				// группа для выделенной книги
				ListViewGroup Group = SelectedItem.Group;
				if ( MiscListView.countCheckedItemsInGroup( Group ) != 2) {
					MessageBox.Show( "Сравнивать можно только 2 помеченных книгу в одной группе!",
					                sDiffTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return;
				}
				// книги выбранной группы
				IList<ListViewItem> ChekedItems = MiscListView.checkedItemsInGroup( Group );
				
				// запускаем инструмент сравнения
				string sFilesNotExists = string.Empty;
				if ( !File.Exists( ChekedItems[0].Text ) ) {
					sFilesNotExists += ChekedItems[0].Text; sFilesNotExists += "\n";
				}
				if ( !File.Exists( ChekedItems[1].Text ) ) {
					sFilesNotExists += ChekedItems[1].Text; sFilesNotExists += "\n";
				}

				if ( sFilesNotExists != string.Empty )
					MessageBox.Show( "Не найден(ы) файл(ы) для сравнения:\n" + sFilesNotExists + "\nРабота остановлена!",
					                sDiffTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				else {
					ZipFB2Worker.DiffFB2( sDiffPath, ChekedItems[0].Text, ChekedItems[1].Text, m_TempDir + "\\1", m_TempDir + "\\2" );
					Cursor.Current = Cursors.WaitCursor;
					// занесение данных в выделенный итем (метаданные книги после правки)
					viewMetaData( ChekedItems[0].Text , ChekedItems[0], 1 );
					viewMetaData( ChekedItems[1].Text , ChekedItems[1], 1 );
					Cursor.Current = Cursors.Default;
				}
			}
		}
		
		// отметить все книги выбранной Группы
		void TsmiCheckedAllInGroupClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			ListView.SelectedListViewItemCollection si = listViewFB2Files.SelectedItems;
			if (si.Count > 0) {
				// группа для выделенной книги
				ListViewGroup lvg = si[0].Group;
				MiscListView.CheckAllListViewItemsInGroup( lvg, true );
			}
			ConnectListsEventHandlers( true );
		}
		
		// отметить все книги всех Групп
		void TsmiCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			MiscListView.CheckAllListViewItems( listViewFB2Files, true );
			ConnectListsEventHandlers( true );
		}
		
		// снять отметки со всех книг
		void TsmiUnCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			MiscListView.UnCheckAllListViewItems( listViewFB2Files.CheckedItems );
			ConnectListsEventHandlers( true );
		}
		
		// записать путь ко всем помеченных книгам в файл
		void TsmiSaveAllCheckedItemToFileClick(object sender, EventArgs e)
		{
			// проверка, есть ли хоть один помеченный файл
			const string MessTitle = "SharpFBTools - сохранение путей помеченных книг";
			if( listViewFB2Files.CheckedItems.Count == 0 ) {
				MessageBox.Show( "Нет ни одной помеченной книги.\nРабота прекращена.",
				                MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			
			sfdList.Title = "Укажите название файла путей помеченных книг:";
			sfdList.Filter = "XML файлы (*.xml)|*.xml";
			sfdList.FileName = string.Empty;
			sfdList.InitialDirectory = Settings.Settings.ProgDir;
			DialogResult result = sfdList.ShowDialog();
			if( result == DialogResult.OK ) {
				Environment.CurrentDirectory = Settings.Settings.ProgDir;
				Core.Duplicator.CheckedBooksPathToXMLForm filesForm =
					new Core.Duplicator.CheckedBooksPathToXMLForm(
						tboxSourceDir.Text.Trim() , sfdList.FileName, listViewFB2Files
					);
				filesForm.ShowDialog();
				EndWorkMode EndWorkMode = filesForm.EndMode;
				filesForm.Dispose();
				MessageBox.Show( EndWorkMode.Message, MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
		}
		
		// пометить в выбранной группе все невалидные книги
		void TsmiAllNonValidateBooksForAllGroupsClick(object sender, EventArgs e)
		{
			checkOldBook( GroupAnalyzeModeEnum.AllGroup, CompareModeEnum.Validate );
		}
		
		// пометить в каждой группе все "старые" книги (по тэгу version и невалидности)
		void TsmiAllOldBooksVersionValidateForAllGroupsClick(object sender, EventArgs e)
		{
			checkOldBook( GroupAnalyzeModeEnum.AllGroup, CompareModeEnum.VersionValidate );
		}
		
		// пометить в каждой группе все "старые" книги (по времени создания файла)
		void TsmiAllOldBooksCreationTimeForAllGroupsClick(object sender, EventArgs e)
		{
			checkOldBook( GroupAnalyzeModeEnum.AllGroup, CompareModeEnum.CreationTime );
		}
		
		// пометить в каждой группе все "старые" книги (по времени последнего изменения файла)
		void TsmiAllOldBooksLastWriteTimeForAllGroupsClick(object sender, EventArgs e)
		{
			checkOldBook( GroupAnalyzeModeEnum.AllGroup, CompareModeEnum.LastWriteTime );
		}
		
		// пометить в выбранной группе все невалидные книги
		void TsmiAllNonValidateBooksClick(object sender, EventArgs e)
		{
			checkOldBook( GroupAnalyzeModeEnum.Group, CompareModeEnum.Validate );
		}
		
		// пометить в выбранной группе все "старые" книги (по тэгу version и невалидности)
		void TsmiAnalyzeVersionValidateInGroupClick(object sender, EventArgs e)
		{
			checkOldBook( GroupAnalyzeModeEnum.Group, CompareModeEnum.VersionValidate );
		}
		
		// пометить в выбранной группе все "старые" книги (по времени создания файла)
		void TsmiAllOldBooksCreationTimeInGroupClick(object sender, EventArgs e)
		{
			checkOldBook( GroupAnalyzeModeEnum.Group, CompareModeEnum.CreationTime );
		}
		
		// пометить в выбранной группе все "старые" книги (по времени последнего изменения файла)
		void TsmiAllOldBooksLastWriteTimeInGroupClick(object sender, EventArgs e)
		{
			checkOldBook( GroupAnalyzeModeEnum.Group, CompareModeEnum.LastWriteTime );
		}
		
		// копировать помеченные файлы в папку-приемник
		void TsmiCopyCheckedFb2ToClick(object sender, EventArgs e)
		{
			if( listViewFB2Files.Items.Count > 0 && listViewFB2Files.CheckedItems.Count > 0 ) {
				string sTarget = FilesWorker.OpenDirDlg( m_TargetDir, fbdScanDir, "Укажите папку-приемник для размешения копий книг:" );
				if( sTarget == null )
					return;

				saveSettingsToXml();

				const string MessTitle = "SharpFBTools - Копирование копий книг";
				if( tboxSourceDir.Text.Trim() == sTarget ) {
					MessageBox.Show( "Папка-приемник файлов совпадает с папкой исходных книг!\nРабота прекращена.",
					                MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return;
				}
				
				Core.Duplicator.CopyMoveDeleteForm copyMoveDeleteForm = new Core.Duplicator.CopyMoveDeleteForm(
					false, BooksWorkModeEnum.CopyCheckedBooks, tboxSourceDir.Text.Trim(), sTarget,
					cboxExistFile.SelectedIndex, lvFilesCount, listViewFB2Files
				);
				copyMoveDeleteForm.ShowDialog();
				EndWorkMode EndWorkMode = copyMoveDeleteForm.EndMode;
				copyMoveDeleteForm.Dispose();
				MessageBox.Show( EndWorkMode.Message, MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
		}
		
		// переместить помеченные файлы в папку-приемник (с удалением элементов списка копий - медленно)
		void TsmiMoveCheckedFb2ToClick(object sender, EventArgs e)
		{
			moveCheckedFb2To( false );
		}
		// переместить помеченные файлы в папку-приемник (без удаления элементов списка копий - быстро)
		void TsmiMoveCheckedFb2ToFastClick(object sender, EventArgs e)
		{
			moveCheckedFb2To( true );
		}
		
		// удалить помеченные файлы (с удалением элементов списка копий - медленно)
		void TsmiDeleteCheckedFb2Click(object sender, EventArgs e)
		{
			deleteCheckedFb2( false );
		}
		// удалить помеченные файлы (без удаления элементов списка копий - быстро)
		void TsmiDeleteCheckedFb2FastClick(object sender, EventArgs e)
		{
			deleteCheckedFb2( true );
		}
		// Удалить всю Группу из Списка БЕЗ удаления книг с жесткого диска...
		void TsmiDeleteGroupNotFileClick(object sender, EventArgs e)
		{
			deleteGroupNotRemoveFiles();
		}
		// Удаление элементов Списка, файлы которых были удалены с жесткого диска
		void TsmiDeleteAllItemForNonExistFileClick(object sender, EventArgs e)
		{
			if ( listViewFB2Files.Items.Count > 0 ) {
				const string MessTitle = "SharpFBTools - Удаление элементов Списка \"без файлов\"";
				const string sMess = "Вы действительно хотите удалить все элементы Списка, для которых отсутствуют файлы на жестком диске?";
				const MessageBoxButtons buttons = MessageBoxButtons.YesNo;
				if ( MessageBox.Show( sMess, MessTitle, buttons, MessageBoxIcon.Question ) != DialogResult.No ) {
					listViewFB2Files.BeginUpdate();
					ConnectListsEventHandlers( false );
					MiscListView.deleteAllItemForNonExistFile( listViewFB2Files );
					ConnectListsEventHandlers( true );
					listViewFB2Files.EndUpdate();
					MessageBox.Show( "Удаление элементов Списка \"без файловэ\" завершено.", MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				}
			}
		}
		// удаление всех помеченных элементов Списка (их файлы на жестком диске не удаляются)
		void TsmiDeleteChechedItemsNotDeleteFilesClick(object sender, EventArgs e)
		{
			if ( listViewFB2Files.Items.Count > 0 ) {
				const string MessTitle = "SharpFBTools - Удаление помеченных элементов Списка";
				const string sMess = "Вы действительно хотите удалить все помеченные элементы Списка (их файлы на жестком диске НЕ удаляются)?";
				const MessageBoxButtons buttons = MessageBoxButtons.YesNo;
				if ( MessageBox.Show( sMess, MessTitle, buttons, MessageBoxIcon.Question ) != DialogResult.No ) {
					listViewFB2Files.BeginUpdate();
					ConnectListsEventHandlers( false );
					MiscListView.deleteChechedItemsNotDeleteFiles( listViewFB2Files, lvFilesCount );
					ConnectListsEventHandlers( true );
					listViewFB2Files.EndUpdate();
					MessageBox.Show( "Удаление помеченных элементов Списка завершено.", MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				}
			}
		}
		// правка Авторов помеченных книг
		void TsmiSetAuthorsClick(object sender, EventArgs e)
		{
			if ( listViewFB2Files.Items.Count > 0 && listViewFB2Files.CheckedItems.Count > 0 ) {
				editAuthors( BooksValidateModeEnum.CheckedBooks );
//				MiscListView.AutoResizeColumns( listViewFB2Files );
			}
		}
		// правка Авторов выделенных книг
		void TsmiSetAuthorsForSelectedBooksClick(object sender, EventArgs e)
		{
			if ( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count > 0 ) {
				editAuthors( BooksValidateModeEnum.SelectedBooks );
//				MiscListView.AutoResizeColumns( listViewFB2Files );
			}
		}
		
		// правка Жанров помеченных книг
		void TsmiSetGenresClick(object sender, EventArgs e)
		{
			if ( listViewFB2Files.Items.Count > 0 && listViewFB2Files.CheckedItems.Count > 0 ) {
				editGenres( BooksValidateModeEnum.CheckedBooks );
//				MiscListView.AutoResizeColumns( listViewFB2Files );
			}
		}
		// правка Жанров выделенных книг
		void TsmiSetGenresForSelectedBooksClick(object sender, EventArgs e)
		{
			if ( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count > 0 ) {
				editGenres( BooksValidateModeEnum.SelectedBooks );
//				MiscListView.AutoResizeColumns( listViewFB2Files );
			}
		}

		// автокорректировка для выделенных книг
		void TsmiAutoCorrectorForAllSelectedBooksClick(object sender, EventArgs e)
		{
			if ( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count > 0 ) {
				ListView.SelectedListViewItemCollection SelectedItems = listViewFB2Files.SelectedItems;
				const string Message = "Вы действительно хотите запустить автокорректировку всех выделенных книг?";
				const string MessTitle = "SharpFBTools - Автокорректировка для выделенных книг";
				MessageBoxButtons Buttons = MessageBoxButtons.YesNo;
				DialogResult Result = MessageBox.Show( Message, MessTitle, Buttons);
				if( Result == DialogResult.Yes ) {
					Cursor.Current = Cursors.WaitCursor;
					// создание списка ListViewItemInfo данных для выбранных книг
					IList<ListViewItemInfo> ListViewItemInfoList = WorksWithBooks.makeListViewItemInfoListForDup(
						listViewFB2Files, BooksValidateModeEnum.SelectedBooks, tsProgressBar
					);
					Cursor.Current = Cursors.Default;
					
					AutoCorrectorForm autoCorrectorForm = new AutoCorrectorForm(
						BooksAutoCorrectModeEnum.SelectedBooks, listViewFB2Files
					);
					autoCorrectorForm.ShowDialog();
					EndWorkMode EndWorkMode = autoCorrectorForm.EndMode;
					autoCorrectorForm.Dispose();
					
					// отобразить метаданные данные после автокорректировки
					Cursor.Current = Cursors.WaitCursor;
					string valid = viewMetaDataAfterWorkManyBooks( ListViewItemInfoList, BooksValidateModeEnum.SelectedBooks );
					string mess = (SelectedItems.Count == 1)
						? ( !string.IsNullOrWhiteSpace( valid ) ? ("\n\nФайл невалидный:\r\n" + valid) : "\n\nФайл валидный!" )
						: string.Empty;
//					MiscListView.AutoResizeColumns( listViewFB2Files );
					Cursor.Current = Cursors.Default;
					MessageBox.Show(
						EndWorkMode.Message + mess,
						"Автокорректировка книг", MessageBoxButtons.OK,
						string.IsNullOrWhiteSpace( valid ) ? MessageBoxIcon.Information : MessageBoxIcon.Error
					);
				}
			}
		}
		// автокорректировка для помеченных книг
		void ToolStripMenuItemAutoCorrectorForAllCheckedBooksClick(object sender, EventArgs e)
		{
			if ( listViewFB2Files.Items.Count > 0 && listViewFB2Files.CheckedItems.Count > 0 ) {
				ListView.CheckedListViewItemCollection CheckedItems = listViewFB2Files.CheckedItems;
				const string Message = "Вы действительно хотите запустить автокорректировку всех помеченных книг?";
				const string MessTitle = "SharpFBTools - Автокорректировка для помеченных книг";
				MessageBoxButtons Buttons = MessageBoxButtons.YesNo;
				DialogResult Result = MessageBox.Show( Message, MessTitle, Buttons);
				if( Result == DialogResult.Yes ) {
					Cursor.Current = Cursors.WaitCursor;
					// создание списка ListViewItemInfo данных для помеченных книг
					IList<ListViewItemInfo> ListViewItemInfoList = WorksWithBooks.makeListViewItemInfoListForDup(
						listViewFB2Files, BooksValidateModeEnum.CheckedBooks, tsProgressBar
					);
					Cursor.Current = Cursors.Default;
					
					AutoCorrectorForm autoCorrectorForm = new AutoCorrectorForm(
						BooksAutoCorrectModeEnum.CheckedBooks, listViewFB2Files
					);
					autoCorrectorForm.ShowDialog();
					EndWorkMode EndWorkMode = autoCorrectorForm.EndMode;
					autoCorrectorForm.Dispose();
					
					// отобразить метаданные данные после автокорректировки
					Cursor.Current = Cursors.WaitCursor;
					string valid = viewMetaDataAfterWorkManyBooks( ListViewItemInfoList, BooksValidateModeEnum.CheckedBooks );
					string mess = (CheckedItems.Count == 1)
						? ( !string.IsNullOrWhiteSpace( valid ) ? ("\n\nФайл невалидный:\r\n" + valid) : "\n\nФайл валидный!" )
						: string.Empty;
//					MiscListView.AutoResizeColumns( listViewFB2Files );
					Cursor.Current = Cursors.Default;
					MessageBox.Show(
						EndWorkMode.Message + mess,
						"Автокорректировка книг", MessageBoxButtons.OK,
						string.IsNullOrWhiteSpace( valid ) ? MessageBoxIcon.Information : MessageBoxIcon.Error
					);
				}
			}
		}
		// автокорректировка для всех книг выбранной Группы
		void TsmiAutoCorrectorForAllBooksFromGroupClick(object sender, EventArgs e)
		{
			if ( listViewFB2Files.Items.Count > 0 ) {
				const string MessTitle = "SharpFBTools - Автокорректировка всех книг Группы";
				if ( listViewFB2Files.SelectedItems.Count == 1) {
					ListView.CheckedListViewItemCollection CheckedItems = listViewFB2Files.CheckedItems;
					const string Message = "Вы действительно хотите запустить автокорректировку всех книг выделенной Группы?";
					MessageBoxButtons Buttons = MessageBoxButtons.YesNo;
					DialogResult Result = MessageBox.Show( Message, MessTitle, Buttons);
					if ( Result == DialogResult.Yes ) {
						Cursor.Current = Cursors.WaitCursor;
						// создание списка ListViewItemInfo данных для выбранных книг
						IList<ListViewItemInfo> ListViewItemInfoList = WorksWithBooks.makeListViewItemInfoListForDup(
							listViewFB2Files, BooksValidateModeEnum.SelectedBooks, tsProgressBar
						);
						Cursor.Current = Cursors.Default;
						
						MiscListView.UnCheckAllListViewItems( listViewFB2Files.CheckedItems );
						AutoCorrectorForm autoCorrectorForm = new AutoCorrectorForm(
							BooksAutoCorrectModeEnum.BooksInGroup, listViewFB2Files
						);
						autoCorrectorForm.ShowDialog();
						EndWorkMode EndWorkMode = autoCorrectorForm.EndMode;
						autoCorrectorForm.Dispose();
						
						// отобразить метаданные данные после автокорректировки
						Cursor.Current = Cursors.WaitCursor;
						viewMetaDataAfterWorkManyBooks( ListViewItemInfoList, BooksValidateModeEnum.SelectedBooks );

//						MiscListView.AutoResizeColumns( listViewFB2Files );
						Cursor.Current = Cursors.Default;
						MessageBox.Show( EndWorkMode.Message, "Автокорректировка книг", MessageBoxButtons.OK, MessageBoxIcon.Information );
					}
				} else {
					MessageBox.Show( "Выделите только одну книгу в Группе, для книг которой вы хотите провести автокорректировку", MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				}
			}
		}
		// автокорректировка для всех книг всех Групп
		void TsmiAutoCorrectorForAllBooksAllGroupClick(object sender, EventArgs e)
		{
			if ( listViewFB2Files.Items.Count > 0 ) {
				ListView.CheckedListViewItemCollection CheckedItems = listViewFB2Files.CheckedItems;
				const string Message = "Вы действительно хотите запустить автокорректировку всех книг во всех Группах";
				const string MessTitle = "SharpFBTools - Автокорректировка всех книг";
				MessageBoxButtons Buttons = MessageBoxButtons.YesNo;
				DialogResult Result = MessageBox.Show( Message, MessTitle, Buttons);
				if( Result == DialogResult.Yes ) {
					Cursor.Current = Cursors.WaitCursor;
					// создание списка ListViewItemInfo данных для выбранных книг
					IList<ListViewItemInfo> ListViewItemInfoList = WorksWithBooks.makeListViewItemInfoListForDup(
						listViewFB2Files, BooksValidateModeEnum.AllBooks, tsProgressBar
					);
					Cursor.Current = Cursors.Default;
					
					MiscListView.UnCheckAllListViewItems( listViewFB2Files.CheckedItems );
					AutoCorrectorForm autoCorrectorForm = new AutoCorrectorForm(
						BooksAutoCorrectModeEnum.BooksInAllGroup, listViewFB2Files
					);
					autoCorrectorForm.ShowDialog();
					EndWorkMode EndWorkMode = autoCorrectorForm.EndMode;
					autoCorrectorForm.Dispose();
					
					// отобразить метаданные данные после автокорректировки
					Cursor.Current = Cursors.WaitCursor;
					viewMetaDataAfterWorkManyBooks( ListViewItemInfoList, BooksValidateModeEnum.AllBooks );

//					MiscListView.AutoResizeColumns( listViewFB2Files );
					Cursor.Current = Cursors.Default;
					MessageBox.Show( EndWorkMode.Message, "Автокорректировка книг", MessageBoxButtons.OK, MessageBoxIcon.Information );
				}
			}
		}
		
		// генерация новых id для выделенных книг
		void TsmiSetNewIDForAllSelectedBooksClick(object sender, EventArgs e)
		{
			if ( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count > 0 ) {
				ListView.SelectedListViewItemCollection SelectedItems = listViewFB2Files.SelectedItems;
				const string Message = "Вы действительно хотите измменить id всех выделенных книг на новые?";
				const string MessTitle = "SharpFBTools - Генерация новых ID";
				MessageBoxButtons Buttons = MessageBoxButtons.YesNo;
				DialogResult Result = MessageBox.Show( Message, MessTitle, Buttons);
				if( Result == DialogResult.Yes ) {
					Cursor.Current = Cursors.WaitCursor;
					tsProgressBar.Maximum = SelectedItems.Count;
					tsProgressBar.Value = 0;
					foreach( ListViewItem SelectedItem in SelectedItems ) {
						setNewBookID( SelectedItem, SelectedItems.Count );
						tsProgressBar.Value++;
					}
					tsProgressBar.Value = 0;
					Cursor.Current = Cursors.Default;
//					MiscListView.AutoResizeColumns( listViewFB2Files );
				}
			}
		}
		// генерация новых id для помеченных книг
		void TsmiSetNewIDForAllCheckedBooksClick(object sender, EventArgs e)
		{
			if ( listViewFB2Files.Items.Count > 0 && listViewFB2Files.SelectedItems.Count > 0 ) {
				ListView.CheckedListViewItemCollection CheckedItems = listViewFB2Files.CheckedItems;
				const string Message = "Вы действительно хотите измменить id всех помеченных книг на новые?";
				const string MessTitle = "SharpFBTools - Генерация новых ID";
				MessageBoxButtons Buttons = MessageBoxButtons.YesNo;
				DialogResult Result = MessageBox.Show( Message, MessTitle, Buttons);
				if ( Result == DialogResult.Yes ) {
					Cursor.Current = Cursors.WaitCursor;
					tsProgressBar.Maximum = CheckedItems.Count;
					tsProgressBar.Value = 0;
					foreach( ListViewItem CheckedItem in CheckedItems ) {
						setNewBookID( CheckedItem, CheckedItems.Count );
						tsProgressBar.Value++;
					}
					tsProgressBar.Value = 0;
//					MiscListView.AutoResizeColumns( listViewFB2Files );
					Cursor.Current = Cursors.Default;
				}
			}
		}
		// генерация новых id для всех книг выделенной Группы
		void TsmiSetNewIDForAllBooksFromGroupClick(object sender, EventArgs e)
		{
			if ( listViewFB2Files.Items.Count > 0 ) {
				const string MessTitle = "SharpFBTools - Генерация новых ID для всех книг Группы";
				if ( listViewFB2Files.SelectedItems.Count == 1) {
					const string Message = "Вы действительно хотите измменить id всех книг выделенной Группы на новые?";
					MessageBoxButtons Buttons = MessageBoxButtons.YesNo;
					DialogResult Result = MessageBox.Show( Message, MessTitle, Buttons);
					if ( Result == DialogResult.Yes ) {
						ConnectListsEventHandlers( false );
						MiscListView.UnCheckAllListViewItems( listViewFB2Files.CheckedItems );
						ConnectListsEventHandlers( true );
						
						Cursor.Current = Cursors.WaitCursor;
						ListViewGroup lvg = listViewFB2Files.SelectedItems[0].Group;
						tsProgressBar.Maximum = lvg.Items.Count;
						tsProgressBar.Value = 0;
						// перебор всех книг в выбранной группе
						foreach( ListViewItem Item in lvg.Items ) {
							setNewBookID( Item, lvg.Items.Count );
							tsProgressBar.Value++;
						}
						tsProgressBar.Value = 0;
//						MiscListView.AutoResizeColumns( listViewFB2Files );
						Cursor.Current = Cursors.Default;
					}
				} else {
					MessageBox.Show(
						"Выделите только одну книгу в Группе, для книг которой вы хотите изменить ID", MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information
					);
				}
			}
		}
		// генерация новых id для всех книг всех Групп
		void TsmiSetNewIDForAllBooksAllGroupClick(object sender, EventArgs e)
		{
			if ( listViewFB2Files.Items.Count > 0 ) {
				ListView.CheckedListViewItemCollection CheckedItems = listViewFB2Files.CheckedItems;
				const string Message = "Вы действительно хотите измменить id всех книг во всех Группах на новые?";
				const string MessTitle = "SharpFBTools - Генерация новых ID";
				MessageBoxButtons Buttons = MessageBoxButtons.YesNo;
				DialogResult Result = MessageBox.Show( Message, MessTitle, Buttons);
				if ( Result == DialogResult.Yes ) {
					ConnectListsEventHandlers( false );
					MiscListView.UnCheckAllListViewItems( listViewFB2Files.CheckedItems );
					ConnectListsEventHandlers( true );
					Cursor.Current = Cursors.WaitCursor;
					tsProgressBar.Maximum = listViewFB2Files.Groups.Count;
					tsProgressBar.Value = 0;
					// перебор всех групп
					foreach( ListViewGroup lvg in listViewFB2Files.Groups ) {
						// перебор всех книг в выбранной группе
						foreach( ListViewItem Item in lvg.Items )
							setNewBookID( Item, listViewFB2Files.Items.Count );
						tsProgressBar.Value++;
					}
					tsProgressBar.Value = 0;
//					MiscListView.AutoResizeColumns( listViewFB2Files );
					Cursor.Current = Cursors.Default;
				}
			}
		}

		// авторазмер ширины колонок Списка корпий
		void TsmiColumnsResultAutoReizeClick(object sender, EventArgs e)
		{
			MiscListView.AutoResizeColumns( listViewFB2Files );
		}
		void CheckBoxSaveGroupsToXmlCheckStateChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		void TcCoversMouseDoubleClick(object sender, MouseEventArgs e)
		{
			pFilesCount.Visible = ! pFilesCount.Visible;
		}
		void TcViewFB2DescDoubleClick(object sender, EventArgs e)
		{
			pFilesCount.Visible = ! pFilesCount.Visible;
		}
		void TscbGroupCountForListSelectedIndexChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
        #endregion

    }
}
