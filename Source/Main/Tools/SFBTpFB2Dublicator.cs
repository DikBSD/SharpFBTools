/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 16.03.2009
 * Time: 10:03
 * 
 * License: GPL 2.1
 */

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using Core.FB2.Description.CustomInfo;
using Core.Misc;

using FB2Validator		= Core.FB2Parser.FB2Validator;
using SharpZipLibWorker = Core.Misc.SharpZipLibWorker;
using StatusView 		= Core.Duplicator.StatusView;
using filesWorker		= Core.Misc.FilesWorker;

// enums
using ResultViewCollumn = Core.Misc.Enums.ResultViewCollumn;
using CompareMode		= Core.Misc.Enums.CompareMode;
using DuplWorkMode		= Core.Misc.Enums.DuplWorkMode;
using GroupAnalyzeMode	= Core.Misc.Enums.GroupAnalyzeMode;

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
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
			"Название Книги",
			""}, 0, System.Drawing.Color.Red, System.Drawing.Color.Empty, new System.Drawing.Font("Tahoma", 8F));
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
			"Жанр(ы) Книги (Math %)",
			""}, 0, System.Drawing.Color.Red, System.Drawing.Color.Empty, new System.Drawing.Font("Tahoma", 8F));
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
			"Язык",
			""}, 0, System.Drawing.Color.Red, System.Drawing.Color.Empty, new System.Drawing.Font("Tahoma", 8F));
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
			"Язык оригинала",
			""}, 0);
			System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
			"Автор(ы) Книги",
			""}, 0, System.Drawing.Color.Navy, System.Drawing.Color.Empty, null);
			System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
			"Дата написания: Текст (Значение)",
			""}, 0);
			System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
			"Ключевые слова",
			""}, 0);
			System.Windows.Forms.ListViewItem listViewItem8 = new System.Windows.Forms.ListViewItem(new string[] {
			"Переводчик(и)",
			""}, 0);
			System.Windows.Forms.ListViewItem listViewItem9 = new System.Windows.Forms.ListViewItem(new string[] {
			"Серия(и) (Номер)",
			""}, 0, System.Drawing.Color.Green, System.Drawing.Color.Empty, null);
			System.Windows.Forms.ListViewItem listViewItem10 = new System.Windows.Forms.ListViewItem(new string[] {
			"Число Обложек",
			""}, 0);
			System.Windows.Forms.ListViewItem listViewItem11 = new System.Windows.Forms.ListViewItem(new string[] {
			"Название Книги",
			""}, 0, System.Drawing.Color.Red, System.Drawing.Color.Empty, new System.Drawing.Font("Tahoma", 8F));
			System.Windows.Forms.ListViewItem listViewItem12 = new System.Windows.Forms.ListViewItem(new string[] {
			"Жанр(ы) Книги (Math %)",
			""}, 0, System.Drawing.Color.Red, System.Drawing.Color.Empty, new System.Drawing.Font("Tahoma", 8F));
			System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem(new string[] {
			"Язык",
			""}, 0, System.Drawing.Color.Red, System.Drawing.Color.Empty, new System.Drawing.Font("Tahoma", 8F));
			System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem(new string[] {
			"Язык оригинала",
			""}, 0);
			System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem(new string[] {
			"Автор(ы) Книги",
			""}, 0, System.Drawing.Color.Navy, System.Drawing.Color.Empty, null);
			System.Windows.Forms.ListViewItem listViewItem16 = new System.Windows.Forms.ListViewItem(new string[] {
			"Дата написания: Текст (Значение)",
			""}, 0);
			System.Windows.Forms.ListViewItem listViewItem17 = new System.Windows.Forms.ListViewItem(new string[] {
			"Ключевые слова",
			""}, 0);
			System.Windows.Forms.ListViewItem listViewItem18 = new System.Windows.Forms.ListViewItem(new string[] {
			"Переводчик(и)",
			""}, 0);
			System.Windows.Forms.ListViewItem listViewItem19 = new System.Windows.Forms.ListViewItem(new string[] {
			"Серия(и) (Номер)",
			""}, 0, System.Drawing.Color.Green, System.Drawing.Color.Empty, null);
			System.Windows.Forms.ListViewItem listViewItem20 = new System.Windows.Forms.ListViewItem(new string[] {
			"Число Обложек",
			""}, 0);
			System.Windows.Forms.ListViewItem listViewItem21 = new System.Windows.Forms.ListViewItem(new string[] {
			"ID Книги",
			""}, 0, System.Drawing.Color.Red, System.Drawing.Color.Empty, new System.Drawing.Font("Tahoma", 8F));
			System.Windows.Forms.ListViewItem listViewItem22 = new System.Windows.Forms.ListViewItem(new string[] {
			"Версия fb2-файла",
			""}, 0, System.Drawing.Color.Red, System.Drawing.Color.Empty, new System.Drawing.Font("Tahoma", 8F));
			System.Windows.Forms.ListViewItem listViewItem23 = new System.Windows.Forms.ListViewItem(new string[] {
			"Дата создания: Текст (Значение)",
			""}, 0);
			System.Windows.Forms.ListViewItem listViewItem24 = new System.Windows.Forms.ListViewItem(new string[] {
			"Программы",
			""}, 0);
			System.Windows.Forms.ListViewItem listViewItem25 = new System.Windows.Forms.ListViewItem(new string[] {
			"Источник OCR",
			""}, 0);
			System.Windows.Forms.ListViewItem listViewItem26 = new System.Windows.Forms.ListViewItem(new string[] {
			"Источник URL",
			""}, 0);
			System.Windows.Forms.ListViewItem listViewItem27 = new System.Windows.Forms.ListViewItem(new string[] {
			"Автор fb2-файла",
			""}, 0);
			System.Windows.Forms.ListViewItem listViewItem28 = new System.Windows.Forms.ListViewItem(new string[] {
			"Заголовок Книги",
			""}, 0, System.Drawing.SystemColors.WindowText, System.Drawing.Color.Empty, null);
			System.Windows.Forms.ListViewItem listViewItem29 = new System.Windows.Forms.ListViewItem(new string[] {
			"Издатель",
			""}, 0);
			System.Windows.Forms.ListViewItem listViewItem30 = new System.Windows.Forms.ListViewItem(new string[] {
			"Город",
			""}, 0);
			System.Windows.Forms.ListViewItem listViewItem31 = new System.Windows.Forms.ListViewItem(new string[] {
			"Год издания",
			""}, 0);
			System.Windows.Forms.ListViewItem listViewItem32 = new System.Windows.Forms.ListViewItem(new string[] {
			"ISBN",
			""}, 0);
			System.Windows.Forms.ListViewItem listViewItem33 = new System.Windows.Forms.ListViewItem(new string[] {
			"Серия(и) (Номер)",
			""}, 0, System.Drawing.Color.Green, System.Drawing.Color.Empty, null);
			System.Windows.Forms.ListViewItem listViewItem34 = new System.Windows.Forms.ListViewItem(new string[] {
			"Всего папок",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem35 = new System.Windows.Forms.ListViewItem(new string[] {
			"Всего файлов",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem36 = new System.Windows.Forms.ListViewItem(new string[] {
			"Всего fb2 файлов (.fb2)",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem37 = new System.Windows.Forms.ListViewItem(new string[] {
			"Архивы (.fb2.zip; .fbz; .zip)",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem38 = new System.Windows.Forms.ListViewItem(new string[] {
			"Другие файлы",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem39 = new System.Windows.Forms.ListViewItem(new string[] {
			"Всего групп одинаковых книг",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem40 = new System.Windows.Forms.ListViewItem(new string[] {
			"Книг во всех группах одинаковых книг",
			"0"}, 0);
			this.ssProgress = new System.Windows.Forms.StatusStrip();
			this.pSearchFBDup2Dirs = new System.Windows.Forms.Panel();
			this.btnOpenDir = new System.Windows.Forms.Button();
			this.chBoxScanSubDir = new System.Windows.Forms.CheckBox();
			this.tboxSourceDir = new System.Windows.Forms.TextBox();
			this.lblScanDir = new System.Windows.Forms.Label();
			this.tsDup = new System.Windows.Forms.ToolStrip();
			this.tsbtnSearchDubls = new System.Windows.Forms.ToolStripButton();
			this.tsbtnSearchFb2DupRenew = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnDupSaveList = new System.Windows.Forms.ToolStripButton();
			this.tsbtnDupOpenList = new System.Windows.Forms.ToolStripButton();
			this.cmsFB2 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.tsmiAnalyzeForSelectedGroup = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAnalyzeInGroup = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAllOldBooksCreationTimeInGroup = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAllOldBooksLastWriteTimeInGroup = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAnalyzeForAllGroups = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAllOldBooksForAllGroups = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAllOldBooksCreationTimeForAllGroups = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAllOldBooksLastWriteTimeForAllGroups = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiDeleteGroupNotFile = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiDiffFB2 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiFileReValidate = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAllFilesInGroupReValidate = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiAllGroupsReValidate = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmi3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiEditInTextEditor = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiEditInFB2Editor = new System.Windows.Forms.ToolStripMenuItem();
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
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiCheckedAllInGroup = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiCheckedAll = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiUnCheckedAll = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiSaveAllCheckedItemToFile = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsmiOpenFileDir = new System.Windows.Forms.ToolStripMenuItem();
			this.tsmiDeleteFileFromDisk = new System.Windows.Forms.ToolStripMenuItem();
			this.pMode = new System.Windows.Forms.Panel();
			this.chBoxIsValid = new System.Windows.Forms.CheckBox();
			this.cboxMode = new System.Windows.Forms.ComboBox();
			this.lblMode = new System.Windows.Forms.Label();
			this.fbdScanDir = new System.Windows.Forms.FolderBrowserDialog();
			this.pExistFile = new System.Windows.Forms.Panel();
			this.rbtnFB22 = new System.Windows.Forms.RadioButton();
			this.rbtnFB2Librusec = new System.Windows.Forms.RadioButton();
			this.lblFMFSGenres = new System.Windows.Forms.Label();
			this.cboxDupExistFile = new System.Windows.Forms.ComboBox();
			this.lblDupExistFile = new System.Windows.Forms.Label();
			this.sfdList = new System.Windows.Forms.SaveFileDialog();
			this.sfdLoadList = new System.Windows.Forms.OpenFileDialog();
			this.pInfo = new System.Windows.Forms.Panel();
			this.tcViewFB2Desc = new System.Windows.Forms.TabControl();
			this.tpTitleInfo = new System.Windows.Forms.TabPage();
			this.lvTitleInfo = new System.Windows.Forms.ListView();
			this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
			this.tpSourceTitleInfo = new System.Windows.Forms.TabPage();
			this.lvSourceTitleInfo = new System.Windows.Forms.ListView();
			this.columnHeader17 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader18 = new System.Windows.Forms.ColumnHeader();
			this.tpDocumentInfo = new System.Windows.Forms.TabPage();
			this.lvDocumentInfo = new System.Windows.Forms.ListView();
			this.columnHeader15 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader16 = new System.Windows.Forms.ColumnHeader();
			this.tpPublishInfo = new System.Windows.Forms.TabPage();
			this.lvPublishInfo = new System.Windows.Forms.ListView();
			this.columnHeader13 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader14 = new System.Windows.Forms.ColumnHeader();
			this.tpCustomInfo = new System.Windows.Forms.TabPage();
			this.lvCustomInfo = new System.Windows.Forms.ListView();
			this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
			this.tpHistory = new System.Windows.Forms.TabPage();
			this.rtbHistory = new System.Windows.Forms.RichTextBox();
			this.tpAnnotation = new System.Windows.Forms.TabPage();
			this.rtbAnnotation = new System.Windows.Forms.RichTextBox();
			this.tpValidate = new System.Windows.Forms.TabPage();
			this.tbValidate = new System.Windows.Forms.TextBox();
			this.picBoxCover = new System.Windows.Forms.PictureBox();
			this.lvFilesCount = new System.Windows.Forms.ListView();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.chBoxViewProgress = new System.Windows.Forms.CheckBox();
			this.lvResult = new System.Windows.Forms.ListView();
			this.imageListDup = new System.Windows.Forms.ImageList(this.components);
			this.pSearchFBDup2Dirs.SuspendLayout();
			this.tsDup.SuspendLayout();
			this.cmsFB2.SuspendLayout();
			this.pMode.SuspendLayout();
			this.pExistFile.SuspendLayout();
			this.pInfo.SuspendLayout();
			this.tcViewFB2Desc.SuspendLayout();
			this.tpTitleInfo.SuspendLayout();
			this.tpSourceTitleInfo.SuspendLayout();
			this.tpDocumentInfo.SuspendLayout();
			this.tpPublishInfo.SuspendLayout();
			this.tpCustomInfo.SuspendLayout();
			this.tpHistory.SuspendLayout();
			this.tpAnnotation.SuspendLayout();
			this.tpValidate.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picBoxCover)).BeginInit();
			this.SuspendLayout();
			// 
			// ssProgress
			// 
			this.ssProgress.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.ssProgress.Location = new System.Drawing.Point(0, 667);
			this.ssProgress.Name = "ssProgress";
			this.ssProgress.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
			this.ssProgress.Size = new System.Drawing.Size(1497, 22);
			this.ssProgress.TabIndex = 19;
			this.ssProgress.Text = "statusStrip1";
			// 
			// pSearchFBDup2Dirs
			// 
			this.pSearchFBDup2Dirs.AutoSize = true;
			this.pSearchFBDup2Dirs.Controls.Add(this.btnOpenDir);
			this.pSearchFBDup2Dirs.Controls.Add(this.chBoxScanSubDir);
			this.pSearchFBDup2Dirs.Controls.Add(this.tboxSourceDir);
			this.pSearchFBDup2Dirs.Controls.Add(this.lblScanDir);
			this.pSearchFBDup2Dirs.Dock = System.Windows.Forms.DockStyle.Top;
			this.pSearchFBDup2Dirs.Location = new System.Drawing.Point(0, 31);
			this.pSearchFBDup2Dirs.Margin = new System.Windows.Forms.Padding(4);
			this.pSearchFBDup2Dirs.Name = "pSearchFBDup2Dirs";
			this.pSearchFBDup2Dirs.Size = new System.Drawing.Size(1497, 41);
			this.pSearchFBDup2Dirs.TabIndex = 36;
			// 
			// btnOpenDir
			// 
			this.btnOpenDir.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenDir.Image")));
			this.btnOpenDir.Location = new System.Drawing.Point(211, 4);
			this.btnOpenDir.Margin = new System.Windows.Forms.Padding(4);
			this.btnOpenDir.Name = "btnOpenDir";
			this.btnOpenDir.Size = new System.Drawing.Size(49, 33);
			this.btnOpenDir.TabIndex = 21;
			this.btnOpenDir.UseVisualStyleBackColor = true;
			this.btnOpenDir.Click += new System.EventHandler(this.BtnOpenDirClick);
			// 
			// chBoxScanSubDir
			// 
			this.chBoxScanSubDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chBoxScanSubDir.Checked = true;
			this.chBoxScanSubDir.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chBoxScanSubDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxScanSubDir.ForeColor = System.Drawing.Color.Navy;
			this.chBoxScanSubDir.Location = new System.Drawing.Point(1268, 5);
			this.chBoxScanSubDir.Margin = new System.Windows.Forms.Padding(4);
			this.chBoxScanSubDir.Name = "chBoxScanSubDir";
			this.chBoxScanSubDir.Size = new System.Drawing.Size(229, 30);
			this.chBoxScanSubDir.TabIndex = 2;
			this.chBoxScanSubDir.Text = "Сканировать и подпапки";
			this.chBoxScanSubDir.UseVisualStyleBackColor = true;
			this.chBoxScanSubDir.CheckStateChanged += new System.EventHandler(this.ChBoxScanSubDirCheckStateChanged);
			// 
			// tboxSourceDir
			// 
			this.tboxSourceDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxSourceDir.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tboxSourceDir.Location = new System.Drawing.Point(267, 7);
			this.tboxSourceDir.Margin = new System.Windows.Forms.Padding(4);
			this.tboxSourceDir.Name = "tboxSourceDir";
			this.tboxSourceDir.Size = new System.Drawing.Size(992, 24);
			this.tboxSourceDir.TabIndex = 1;
			this.tboxSourceDir.TextChanged += new System.EventHandler(this.TboxSourceDirTextChanged);
			this.tboxSourceDir.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TboxSourceDirKeyPress);
			// 
			// lblScanDir
			// 
			this.lblScanDir.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.lblScanDir.Location = new System.Drawing.Point(4, 10);
			this.lblScanDir.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblScanDir.Name = "lblScanDir";
			this.lblScanDir.Size = new System.Drawing.Size(216, 23);
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
			this.tsbtnDupSaveList,
			this.tsbtnDupOpenList});
			this.tsDup.Location = new System.Drawing.Point(0, 0);
			this.tsDup.Name = "tsDup";
			this.tsDup.Size = new System.Drawing.Size(1497, 31);
			this.tsDup.TabIndex = 35;
			// 
			// tsbtnSearchDubls
			// 
			this.tsbtnSearchDubls.AutoToolTip = false;
			this.tsbtnSearchDubls.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSearchDubls.Image")));
			this.tsbtnSearchDubls.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnSearchDubls.Name = "tsbtnSearchDubls";
			this.tsbtnSearchDubls.Size = new System.Drawing.Size(127, 28);
			this.tsbtnSearchDubls.Text = "Поиск копий";
			this.tsbtnSearchDubls.Click += new System.EventHandler(this.TsbtnSearchDublsClick);
			// 
			// tsbtnSearchFb2DupRenew
			// 
			this.tsbtnSearchFb2DupRenew.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnSearchFb2DupRenew.Image")));
			this.tsbtnSearchFb2DupRenew.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnSearchFb2DupRenew.Name = "tsbtnSearchFb2DupRenew";
			this.tsbtnSearchFb2DupRenew.Size = new System.Drawing.Size(205, 28);
			this.tsbtnSearchFb2DupRenew.Text = "Возобновить из файла...";
			this.tsbtnSearchFb2DupRenew.Click += new System.EventHandler(this.TsbtnSearchFb2DupRenewClick);
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(6, 31);
			// 
			// tsbtnDupSaveList
			// 
			this.tsbtnDupSaveList.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnDupSaveList.Image")));
			this.tsbtnDupSaveList.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnDupSaveList.Name = "tsbtnDupSaveList";
			this.tsbtnDupSaveList.Size = new System.Drawing.Size(120, 28);
			this.tsbtnDupSaveList.Text = "Сохранить...";
			this.tsbtnDupSaveList.ToolTipText = "Сохранить список копий книг в файл";
			this.tsbtnDupSaveList.Click += new System.EventHandler(this.TsbtnDupSaveListClick);
			// 
			// tsbtnDupOpenList
			// 
			this.tsbtnDupOpenList.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnDupOpenList.Image")));
			this.tsbtnDupOpenList.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnDupOpenList.Name = "tsbtnDupOpenList";
			this.tsbtnDupOpenList.Size = new System.Drawing.Size(114, 28);
			this.tsbtnDupOpenList.Text = "Загрузить...";
			this.tsbtnDupOpenList.ToolTipText = "Загрузить список копий книг из файла";
			this.tsbtnDupOpenList.Click += new System.EventHandler(this.TsbtnDupOpenListClick);
			// 
			// cmsFB2
			// 
			this.cmsFB2.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.cmsFB2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.tsmiAnalyzeForSelectedGroup,
			this.tsmiAnalyzeForAllGroups,
			this.tsmiDeleteGroupNotFile,
			this.toolStripMenuItem1,
			this.tsmiDiffFB2,
			this.toolStripSeparator1,
			this.tsmiFileReValidate,
			this.tsmiAllFilesInGroupReValidate,
			this.tsmiAllGroupsReValidate,
			this.tsmi3,
			this.tsmiEditInTextEditor,
			this.tsmiEditInFB2Editor,
			this.tsmi1,
			this.tsmiViewInReader,
			this.tsmi2,
			this.tsmiCopyCheckedFb2To,
			this.tsmiMoveCheckedFb2To,
			this.tsmiDeleteCheckedFb2,
			this.toolStripSeparator4,
			this.tsmiCheckedAllInGroup,
			this.tsmiCheckedAll,
			this.tsmiUnCheckedAll,
			this.toolStripMenuItem3,
			this.tsmiSaveAllCheckedItemToFile,
			this.toolStripMenuItem2,
			this.tsmiOpenFileDir,
			this.tsmiDeleteFileFromDisk});
			this.cmsFB2.Name = "cmsValidator";
			this.cmsFB2.Size = new System.Drawing.Size(616, 574);
			// 
			// tsmiAnalyzeForSelectedGroup
			// 
			this.tsmiAnalyzeForSelectedGroup.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.tsmiAnalyzeInGroup,
			this.tsmiAllOldBooksCreationTimeInGroup,
			this.tsmiAllOldBooksLastWriteTimeInGroup});
			this.tsmiAnalyzeForSelectedGroup.Name = "tsmiAnalyzeForSelectedGroup";
			this.tsmiAnalyzeForSelectedGroup.Size = new System.Drawing.Size(615, 26);
			this.tsmiAnalyzeForSelectedGroup.Text = "Анализ для выбранной группы";
			// 
			// tsmiAnalyzeInGroup
			// 
			this.tsmiAnalyzeInGroup.Name = "tsmiAnalyzeInGroup";
			this.tsmiAnalyzeInGroup.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.I)));
			this.tsmiAnalyzeInGroup.Size = new System.Drawing.Size(533, 24);
			this.tsmiAnalyzeInGroup.Text = "Все \"старые\" fb2 (по версии книги)";
			this.tsmiAnalyzeInGroup.Click += new System.EventHandler(this.TsmiAnalyzeInGroupClick);
			// 
			// tsmiAllOldBooksCreationTimeInGroup
			// 
			this.tsmiAllOldBooksCreationTimeInGroup.Name = "tsmiAllOldBooksCreationTimeInGroup";
			this.tsmiAllOldBooksCreationTimeInGroup.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.T)));
			this.tsmiAllOldBooksCreationTimeInGroup.Size = new System.Drawing.Size(533, 24);
			this.tsmiAllOldBooksCreationTimeInGroup.Text = "Все \"старые\" fb2 (по времени создания файла)";
			this.tsmiAllOldBooksCreationTimeInGroup.Click += new System.EventHandler(this.TsmiAllOldBooksCreationTimeInGroupClick);
			// 
			// tsmiAllOldBooksLastWriteTimeInGroup
			// 
			this.tsmiAllOldBooksLastWriteTimeInGroup.Name = "tsmiAllOldBooksLastWriteTimeInGroup";
			this.tsmiAllOldBooksLastWriteTimeInGroup.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.L)));
			this.tsmiAllOldBooksLastWriteTimeInGroup.Size = new System.Drawing.Size(533, 24);
			this.tsmiAllOldBooksLastWriteTimeInGroup.Text = "Все \"старые\" fb2 (по времени последнего измения файла)";
			this.tsmiAllOldBooksLastWriteTimeInGroup.Click += new System.EventHandler(this.TsmiAllOldBooksLastWriteTimeInGroupClick);
			// 
			// tsmiAnalyzeForAllGroups
			// 
			this.tsmiAnalyzeForAllGroups.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.tsmiAllOldBooksForAllGroups,
			this.tsmiAllOldBooksCreationTimeForAllGroups,
			this.tsmiAllOldBooksLastWriteTimeForAllGroups});
			this.tsmiAnalyzeForAllGroups.Name = "tsmiAnalyzeForAllGroups";
			this.tsmiAnalyzeForAllGroups.Size = new System.Drawing.Size(615, 26);
			this.tsmiAnalyzeForAllGroups.Text = "Анализ для всех групп";
			// 
			// tsmiAllOldBooksForAllGroups
			// 
			this.tsmiAllOldBooksForAllGroups.Name = "tsmiAllOldBooksForAllGroups";
			this.tsmiAllOldBooksForAllGroups.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
			this.tsmiAllOldBooksForAllGroups.Size = new System.Drawing.Size(537, 24);
			this.tsmiAllOldBooksForAllGroups.Text = "Все \"старые\" fb2 (по версии книги)";
			this.tsmiAllOldBooksForAllGroups.Click += new System.EventHandler(this.TsmiAllOldBooksForAllGroupsClick);
			// 
			// tsmiAllOldBooksCreationTimeForAllGroups
			// 
			this.tsmiAllOldBooksCreationTimeForAllGroups.Name = "tsmiAllOldBooksCreationTimeForAllGroups";
			this.tsmiAllOldBooksCreationTimeForAllGroups.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
			this.tsmiAllOldBooksCreationTimeForAllGroups.Size = new System.Drawing.Size(537, 24);
			this.tsmiAllOldBooksCreationTimeForAllGroups.Text = "Все \"старые\" fb2 (по времени создания файла)";
			this.tsmiAllOldBooksCreationTimeForAllGroups.Click += new System.EventHandler(this.TsmiAllOldBooksCreationTimeForAllGroupsClick);
			// 
			// tsmiAllOldBooksLastWriteTimeForAllGroups
			// 
			this.tsmiAllOldBooksLastWriteTimeForAllGroups.Name = "tsmiAllOldBooksLastWriteTimeForAllGroups";
			this.tsmiAllOldBooksLastWriteTimeForAllGroups.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.L)));
			this.tsmiAllOldBooksLastWriteTimeForAllGroups.Size = new System.Drawing.Size(537, 24);
			this.tsmiAllOldBooksLastWriteTimeForAllGroups.Text = "Все \"старые\" fb2 (по времени последнего измения файла)";
			this.tsmiAllOldBooksLastWriteTimeForAllGroups.Click += new System.EventHandler(this.TsmiAllOldBooksLastWriteTimeForAllGroupsClick);
			// 
			// tsmiDeleteGroupNotFile
			// 
			this.tsmiDeleteGroupNotFile.Name = "tsmiDeleteGroupNotFile";
			this.tsmiDeleteGroupNotFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Shift | System.Windows.Forms.Keys.Delete)));
			this.tsmiDeleteGroupNotFile.Size = new System.Drawing.Size(615, 26);
			this.tsmiDeleteGroupNotFile.Text = "Удалить помеченные Группы из Списка (на диске НЕ удаляются)...";
			this.tsmiDeleteGroupNotFile.Click += new System.EventHandler(this.TsmiDeleteGroupNotFileClick);
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(612, 6);
			// 
			// tsmiDiffFB2
			// 
			this.tsmiDiffFB2.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDiffFB2.Image")));
			this.tsmiDiffFB2.Name = "tsmiDiffFB2";
			this.tsmiDiffFB2.Size = new System.Drawing.Size(615, 26);
			this.tsmiDiffFB2.Text = "diff два помеченных (checked) файла в Группе";
			this.tsmiDiffFB2.Click += new System.EventHandler(this.TsmiDiffFB2Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(612, 6);
			// 
			// tsmiFileReValidate
			// 
			this.tsmiFileReValidate.Image = ((System.Drawing.Image)(resources.GetObject("tsmiFileReValidate.Image")));
			this.tsmiFileReValidate.Name = "tsmiFileReValidate";
			this.tsmiFileReValidate.Size = new System.Drawing.Size(615, 26);
			this.tsmiFileReValidate.Text = "Проверить выделенную книгу на валидность";
			this.tsmiFileReValidate.Click += new System.EventHandler(this.TsmiFileReValidateClick);
			// 
			// tsmiAllFilesInGroupReValidate
			// 
			this.tsmiAllFilesInGroupReValidate.Image = ((System.Drawing.Image)(resources.GetObject("tsmiAllFilesInGroupReValidate.Image")));
			this.tsmiAllFilesInGroupReValidate.Name = "tsmiAllFilesInGroupReValidate";
			this.tsmiAllFilesInGroupReValidate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.V)));
			this.tsmiAllFilesInGroupReValidate.Size = new System.Drawing.Size(615, 26);
			this.tsmiAllFilesInGroupReValidate.Text = "Проверить все книги Группы на валидность";
			this.tsmiAllFilesInGroupReValidate.Click += new System.EventHandler(this.TsmiAllFilesInGroupReValidateClick);
			// 
			// tsmiAllGroupsReValidate
			// 
			this.tsmiAllGroupsReValidate.Image = ((System.Drawing.Image)(resources.GetObject("tsmiAllGroupsReValidate.Image")));
			this.tsmiAllGroupsReValidate.Name = "tsmiAllGroupsReValidate";
			this.tsmiAllGroupsReValidate.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.tsmiAllGroupsReValidate.Size = new System.Drawing.Size(615, 26);
			this.tsmiAllGroupsReValidate.Text = "Проверить все книги всех Группы на валидность";
			this.tsmiAllGroupsReValidate.Click += new System.EventHandler(this.TsmiAllGroupsReValidateClick);
			// 
			// tsmi3
			// 
			this.tsmi3.Name = "tsmi3";
			this.tsmi3.Size = new System.Drawing.Size(612, 6);
			// 
			// tsmiEditInTextEditor
			// 
			this.tsmiEditInTextEditor.Image = ((System.Drawing.Image)(resources.GetObject("tsmiEditInTextEditor.Image")));
			this.tsmiEditInTextEditor.Name = "tsmiEditInTextEditor";
			this.tsmiEditInTextEditor.Size = new System.Drawing.Size(615, 26);
			this.tsmiEditInTextEditor.Text = "Редактировать в текстовом редакторе";
			this.tsmiEditInTextEditor.Click += new System.EventHandler(this.TsmiEditInTextEditorClick);
			// 
			// tsmiEditInFB2Editor
			// 
			this.tsmiEditInFB2Editor.Image = ((System.Drawing.Image)(resources.GetObject("tsmiEditInFB2Editor.Image")));
			this.tsmiEditInFB2Editor.Name = "tsmiEditInFB2Editor";
			this.tsmiEditInFB2Editor.Size = new System.Drawing.Size(615, 26);
			this.tsmiEditInFB2Editor.Text = "Редактировать в fb2-редакторе";
			this.tsmiEditInFB2Editor.Click += new System.EventHandler(this.TsmiEditInFB2EditorClick);
			// 
			// tsmi1
			// 
			this.tsmi1.Name = "tsmi1";
			this.tsmi1.Size = new System.Drawing.Size(612, 6);
			// 
			// tsmiViewInReader
			// 
			this.tsmiViewInReader.Image = ((System.Drawing.Image)(resources.GetObject("tsmiViewInReader.Image")));
			this.tsmiViewInReader.Name = "tsmiViewInReader";
			this.tsmiViewInReader.Size = new System.Drawing.Size(615, 26);
			this.tsmiViewInReader.Text = "Запустить в fb2-читалке (Просмотр)";
			this.tsmiViewInReader.Click += new System.EventHandler(this.TsmiViewInReaderClick);
			// 
			// tsmi2
			// 
			this.tsmi2.Name = "tsmi2";
			this.tsmi2.Size = new System.Drawing.Size(612, 6);
			// 
			// tsmiCopyCheckedFb2To
			// 
			this.tsmiCopyCheckedFb2To.Image = ((System.Drawing.Image)(resources.GetObject("tsmiCopyCheckedFb2To.Image")));
			this.tsmiCopyCheckedFb2To.Name = "tsmiCopyCheckedFb2To";
			this.tsmiCopyCheckedFb2To.Size = new System.Drawing.Size(615, 26);
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
			this.tsmiMoveCheckedFb2To.Size = new System.Drawing.Size(615, 26);
			this.tsmiMoveCheckedFb2To.Text = "Переместить помеченные книги...";
			// 
			// tsmiMoveCheckedFb2ToView
			// 
			this.tsmiMoveCheckedFb2ToView.Name = "tsmiMoveCheckedFb2ToView";
			this.tsmiMoveCheckedFb2ToView.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.M)));
			this.tsmiMoveCheckedFb2ToView.Size = new System.Drawing.Size(488, 24);
			this.tsmiMoveCheckedFb2ToView.Text = "Отображая изменения в списке копий (медленно)";
			this.tsmiMoveCheckedFb2ToView.Click += new System.EventHandler(this.TsmiMoveCheckedFb2ToClick);
			// 
			// tsmiMoveCheckedFb2ToFast
			// 
			this.tsmiMoveCheckedFb2ToFast.Name = "tsmiMoveCheckedFb2ToFast";
			this.tsmiMoveCheckedFb2ToFast.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.M)));
			this.tsmiMoveCheckedFb2ToFast.Size = new System.Drawing.Size(488, 24);
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
			this.tsmiDeleteCheckedFb2.Size = new System.Drawing.Size(615, 26);
			this.tsmiDeleteCheckedFb2.Text = "Удалить помеченные книги...";
			// 
			// tsmiDeleteCheckedFb2View
			// 
			this.tsmiDeleteCheckedFb2View.Name = "tsmiDeleteCheckedFb2View";
			this.tsmiDeleteCheckedFb2View.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Delete)));
			this.tsmiDeleteCheckedFb2View.Size = new System.Drawing.Size(498, 24);
			this.tsmiDeleteCheckedFb2View.Text = "Отображая изменения в списке копий (медленно)";
			this.tsmiDeleteCheckedFb2View.Click += new System.EventHandler(this.TsmiDeleteCheckedFb2Click);
			// 
			// tsmiDeleteCheckedFb2Fast
			// 
			this.tsmiDeleteCheckedFb2Fast.Name = "tsmiDeleteCheckedFb2Fast";
			this.tsmiDeleteCheckedFb2Fast.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.Delete)));
			this.tsmiDeleteCheckedFb2Fast.Size = new System.Drawing.Size(498, 24);
			this.tsmiDeleteCheckedFb2Fast.Text = "Без отображения изменений (быстро)";
			this.tsmiDeleteCheckedFb2Fast.Click += new System.EventHandler(this.TsmiDeleteCheckedFb2FastClick);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(612, 6);
			// 
			// tsmiCheckedAllInGroup
			// 
			this.tsmiCheckedAllInGroup.Image = ((System.Drawing.Image)(resources.GetObject("tsmiCheckedAllInGroup.Image")));
			this.tsmiCheckedAllInGroup.Name = "tsmiCheckedAllInGroup";
			this.tsmiCheckedAllInGroup.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
			this.tsmiCheckedAllInGroup.Size = new System.Drawing.Size(615, 26);
			this.tsmiCheckedAllInGroup.Text = "Пометить все книги выбранной Группы";
			this.tsmiCheckedAllInGroup.Click += new System.EventHandler(this.TsmiCheckedAllInGroupClick);
			// 
			// tsmiCheckedAll
			// 
			this.tsmiCheckedAll.Image = ((System.Drawing.Image)(resources.GetObject("tsmiCheckedAll.Image")));
			this.tsmiCheckedAll.Name = "tsmiCheckedAll";
			this.tsmiCheckedAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
			| System.Windows.Forms.Keys.C)));
			this.tsmiCheckedAll.Size = new System.Drawing.Size(615, 26);
			this.tsmiCheckedAll.Text = "Пометить все книги всех Групп";
			this.tsmiCheckedAll.Click += new System.EventHandler(this.TsmiCheckedAllClick);
			// 
			// tsmiUnCheckedAll
			// 
			this.tsmiUnCheckedAll.Image = ((System.Drawing.Image)(resources.GetObject("tsmiUnCheckedAll.Image")));
			this.tsmiUnCheckedAll.Name = "tsmiUnCheckedAll";
			this.tsmiUnCheckedAll.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
			| System.Windows.Forms.Keys.U)));
			this.tsmiUnCheckedAll.Size = new System.Drawing.Size(615, 26);
			this.tsmiUnCheckedAll.Text = "Снять все отметки";
			this.tsmiUnCheckedAll.Click += new System.EventHandler(this.TsmiUnCheckedAllClick);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(612, 6);
			// 
			// tsmiSaveAllCheckedItemToFile
			// 
			this.tsmiSaveAllCheckedItemToFile.Image = ((System.Drawing.Image)(resources.GetObject("tsmiSaveAllCheckedItemToFile.Image")));
			this.tsmiSaveAllCheckedItemToFile.Name = "tsmiSaveAllCheckedItemToFile";
			this.tsmiSaveAllCheckedItemToFile.Size = new System.Drawing.Size(615, 26);
			this.tsmiSaveAllCheckedItemToFile.Text = "Сохранить в файл список путей ко всем помеченным книгам";
			this.tsmiSaveAllCheckedItemToFile.Click += new System.EventHandler(this.TsmiSaveAllCheckedItemToFileClick);
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(612, 6);
			// 
			// tsmiOpenFileDir
			// 
			this.tsmiOpenFileDir.Image = ((System.Drawing.Image)(resources.GetObject("tsmiOpenFileDir.Image")));
			this.tsmiOpenFileDir.Name = "tsmiOpenFileDir";
			this.tsmiOpenFileDir.Size = new System.Drawing.Size(615, 26);
			this.tsmiOpenFileDir.Text = "Открыть папку для выделенного файла";
			this.tsmiOpenFileDir.Click += new System.EventHandler(this.TsmiOpenFileDirClick);
			// 
			// tsmiDeleteFileFromDisk
			// 
			this.tsmiDeleteFileFromDisk.Image = ((System.Drawing.Image)(resources.GetObject("tsmiDeleteFileFromDisk.Image")));
			this.tsmiDeleteFileFromDisk.Name = "tsmiDeleteFileFromDisk";
			this.tsmiDeleteFileFromDisk.ShortcutKeys = System.Windows.Forms.Keys.Delete;
			this.tsmiDeleteFileFromDisk.Size = new System.Drawing.Size(615, 26);
			this.tsmiDeleteFileFromDisk.Text = "Удалить выделенный файл с диска";
			this.tsmiDeleteFileFromDisk.Click += new System.EventHandler(this.TsmiDeleteFileFromDiskClick);
			// 
			// pMode
			// 
			this.pMode.Controls.Add(this.chBoxIsValid);
			this.pMode.Controls.Add(this.cboxMode);
			this.pMode.Controls.Add(this.lblMode);
			this.pMode.Dock = System.Windows.Forms.DockStyle.Top;
			this.pMode.Location = new System.Drawing.Point(0, 72);
			this.pMode.Margin = new System.Windows.Forms.Padding(4);
			this.pMode.Name = "pMode";
			this.pMode.Size = new System.Drawing.Size(1497, 32);
			this.pMode.TabIndex = 37;
			// 
			// chBoxIsValid
			// 
			this.chBoxIsValid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chBoxIsValid.Font = new System.Drawing.Font("Tahoma", 8F);
			this.chBoxIsValid.ForeColor = System.Drawing.Color.Navy;
			this.chBoxIsValid.Location = new System.Drawing.Point(1268, 1);
			this.chBoxIsValid.Margin = new System.Windows.Forms.Padding(4);
			this.chBoxIsValid.Name = "chBoxIsValid";
			this.chBoxIsValid.Size = new System.Drawing.Size(224, 30);
			this.chBoxIsValid.TabIndex = 18;
			this.chBoxIsValid.Text = "Проверять на валидность";
			this.chBoxIsValid.UseVisualStyleBackColor = true;
			this.chBoxIsValid.CheckStateChanged += new System.EventHandler(this.ChBoxIsValidCheckStateChanged);
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
			"3. Автор(ы) и Название Книги (одна и та же книга, сделанная разными людьми - разн" +
				"ые Id, но Автор и Название - одинаковые)"});
			this.cboxMode.Location = new System.Drawing.Point(211, 1);
			this.cboxMode.Margin = new System.Windows.Forms.Padding(4);
			this.cboxMode.Name = "cboxMode";
			this.cboxMode.Size = new System.Drawing.Size(1048, 24);
			this.cboxMode.TabIndex = 17;
			this.cboxMode.SelectedIndexChanged += new System.EventHandler(this.CboxModeSelectedIndexChanged);
			// 
			// lblMode
			// 
			this.lblMode.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblMode.Location = new System.Drawing.Point(5, 5);
			this.lblMode.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblMode.Name = "lblMode";
			this.lblMode.Size = new System.Drawing.Size(215, 22);
			this.lblMode.TabIndex = 0;
			this.lblMode.Text = "Данные для Сравнения:";
			// 
			// fbdScanDir
			// 
			this.fbdScanDir.Description = "Укажите папку для сканирования с fb2-файлами:";
			// 
			// pExistFile
			// 
			this.pExistFile.Controls.Add(this.rbtnFB22);
			this.pExistFile.Controls.Add(this.rbtnFB2Librusec);
			this.pExistFile.Controls.Add(this.lblFMFSGenres);
			this.pExistFile.Controls.Add(this.cboxDupExistFile);
			this.pExistFile.Controls.Add(this.lblDupExistFile);
			this.pExistFile.Dock = System.Windows.Forms.DockStyle.Top;
			this.pExistFile.Location = new System.Drawing.Point(0, 104);
			this.pExistFile.Margin = new System.Windows.Forms.Padding(4);
			this.pExistFile.Name = "pExistFile";
			this.pExistFile.Size = new System.Drawing.Size(1497, 34);
			this.pExistFile.TabIndex = 39;
			// 
			// rbtnFB22
			// 
			this.rbtnFB22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbtnFB22.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnFB22.Location = new System.Drawing.Point(1007, 5);
			this.rbtnFB22.Margin = new System.Windows.Forms.Padding(4);
			this.rbtnFB22.Name = "rbtnFB22";
			this.rbtnFB22.Size = new System.Drawing.Size(72, 26);
			this.rbtnFB22.TabIndex = 31;
			this.rbtnFB22.Text = "fb2.2";
			this.rbtnFB22.UseVisualStyleBackColor = true;
			this.rbtnFB22.Click += new System.EventHandler(this.RbtnFB22Click);
			// 
			// rbtnFB2Librusec
			// 
			this.rbtnFB2Librusec.Checked = true;
			this.rbtnFB2Librusec.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.rbtnFB2Librusec.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnFB2Librusec.Location = new System.Drawing.Point(868, 5);
			this.rbtnFB2Librusec.Margin = new System.Windows.Forms.Padding(4);
			this.rbtnFB2Librusec.Name = "rbtnFB2Librusec";
			this.rbtnFB2Librusec.Size = new System.Drawing.Size(127, 26);
			this.rbtnFB2Librusec.TabIndex = 32;
			this.rbtnFB2Librusec.TabStop = true;
			this.rbtnFB2Librusec.Text = "fb2 Либрусек";
			this.rbtnFB2Librusec.UseVisualStyleBackColor = true;
			this.rbtnFB2Librusec.Click += new System.EventHandler(this.RbtnFB2LibrusecClick);
			// 
			// lblFMFSGenres
			// 
			this.lblFMFSGenres.ForeColor = System.Drawing.Color.Navy;
			this.lblFMFSGenres.Location = new System.Drawing.Point(732, 9);
			this.lblFMFSGenres.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblFMFSGenres.Name = "lblFMFSGenres";
			this.lblFMFSGenres.Size = new System.Drawing.Size(128, 20);
			this.lblFMFSGenres.TabIndex = 30;
			this.lblFMFSGenres.Text = "Схема Жанров:";
			// 
			// cboxDupExistFile
			// 
			this.cboxDupExistFile.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxDupExistFile.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.cboxDupExistFile.FormattingEnabled = true;
			this.cboxDupExistFile.Items.AddRange(new object[] {
			"Заменить существующий fb2-файл создаваемым",
			"Добавить к создаваемому fb2-файлу очередной номер",
			"Добавить к создаваемому fb2-файлу дату и время"});
			this.cboxDupExistFile.Location = new System.Drawing.Point(211, 4);
			this.cboxDupExistFile.Margin = new System.Windows.Forms.Padding(4);
			this.cboxDupExistFile.Name = "cboxDupExistFile";
			this.cboxDupExistFile.Size = new System.Drawing.Size(501, 24);
			this.cboxDupExistFile.TabIndex = 16;
			this.cboxDupExistFile.SelectedIndexChanged += new System.EventHandler(this.CboxDupExistFileSelectedIndexChanged);
			// 
			// lblDupExistFile
			// 
			this.lblDupExistFile.AutoSize = true;
			this.lblDupExistFile.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblDupExistFile.Location = new System.Drawing.Point(5, 9);
			this.lblDupExistFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDupExistFile.Name = "lblDupExistFile";
			this.lblDupExistFile.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblDupExistFile.Size = new System.Drawing.Size(190, 17);
			this.lblDupExistFile.TabIndex = 15;
			this.lblDupExistFile.Text = "Одинаковые fb2 файлы:";
			this.lblDupExistFile.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// sfdList
			// 
			this.sfdList.RestoreDirectory = true;
			this.sfdList.Title = "Укажите название файла копий";
			// 
			// sfdLoadList
			// 
			this.sfdLoadList.RestoreDirectory = true;
			this.sfdLoadList.Title = "Загрузка Списка копий книг";
			// 
			// pInfo
			// 
			this.pInfo.Controls.Add(this.tcViewFB2Desc);
			this.pInfo.Controls.Add(this.picBoxCover);
			this.pInfo.Controls.Add(this.lvFilesCount);
			this.pInfo.Controls.Add(this.chBoxViewProgress);
			this.pInfo.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pInfo.Location = new System.Drawing.Point(0, 374);
			this.pInfo.Margin = new System.Windows.Forms.Padding(4);
			this.pInfo.Name = "pInfo";
			this.pInfo.Size = new System.Drawing.Size(1497, 293);
			this.pInfo.TabIndex = 44;
			// 
			// tcViewFB2Desc
			// 
			this.tcViewFB2Desc.Controls.Add(this.tpTitleInfo);
			this.tcViewFB2Desc.Controls.Add(this.tpSourceTitleInfo);
			this.tcViewFB2Desc.Controls.Add(this.tpDocumentInfo);
			this.tcViewFB2Desc.Controls.Add(this.tpPublishInfo);
			this.tcViewFB2Desc.Controls.Add(this.tpCustomInfo);
			this.tcViewFB2Desc.Controls.Add(this.tpHistory);
			this.tcViewFB2Desc.Controls.Add(this.tpAnnotation);
			this.tcViewFB2Desc.Controls.Add(this.tpValidate);
			this.tcViewFB2Desc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcViewFB2Desc.Location = new System.Drawing.Point(607, 30);
			this.tcViewFB2Desc.Margin = new System.Windows.Forms.Padding(4);
			this.tcViewFB2Desc.Name = "tcViewFB2Desc";
			this.tcViewFB2Desc.SelectedIndex = 0;
			this.tcViewFB2Desc.Size = new System.Drawing.Size(890, 263);
			this.tcViewFB2Desc.TabIndex = 43;
			// 
			// tpTitleInfo
			// 
			this.tpTitleInfo.Controls.Add(this.lvTitleInfo);
			this.tpTitleInfo.Location = new System.Drawing.Point(4, 25);
			this.tpTitleInfo.Margin = new System.Windows.Forms.Padding(4);
			this.tpTitleInfo.Name = "tpTitleInfo";
			this.tpTitleInfo.Size = new System.Drawing.Size(882, 234);
			this.tpTitleInfo.TabIndex = 0;
			this.tpTitleInfo.Text = "Title Info";
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
			this.lvTitleInfo.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
			listViewItem1,
			listViewItem2,
			listViewItem3,
			listViewItem4,
			listViewItem5,
			listViewItem6,
			listViewItem7,
			listViewItem8,
			listViewItem9,
			listViewItem10});
			this.lvTitleInfo.Location = new System.Drawing.Point(0, 0);
			this.lvTitleInfo.Margin = new System.Windows.Forms.Padding(4);
			this.lvTitleInfo.Name = "lvTitleInfo";
			this.lvTitleInfo.ShowItemToolTips = true;
			this.lvTitleInfo.Size = new System.Drawing.Size(882, 234);
			this.lvTitleInfo.TabIndex = 11;
			this.lvTitleInfo.UseCompatibleStateImageBehavior = false;
			this.lvTitleInfo.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "Тэги";
			this.columnHeader9.Width = 240;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "Значение";
			this.columnHeader10.Width = 420;
			// 
			// tpSourceTitleInfo
			// 
			this.tpSourceTitleInfo.Controls.Add(this.lvSourceTitleInfo);
			this.tpSourceTitleInfo.Location = new System.Drawing.Point(4, 25);
			this.tpSourceTitleInfo.Margin = new System.Windows.Forms.Padding(4);
			this.tpSourceTitleInfo.Name = "tpSourceTitleInfo";
			this.tpSourceTitleInfo.Size = new System.Drawing.Size(882, 234);
			this.tpSourceTitleInfo.TabIndex = 1;
			this.tpSourceTitleInfo.Text = "Source Title Info";
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
			this.lvSourceTitleInfo.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
			listViewItem11,
			listViewItem12,
			listViewItem13,
			listViewItem14,
			listViewItem15,
			listViewItem16,
			listViewItem17,
			listViewItem18,
			listViewItem19,
			listViewItem20});
			this.lvSourceTitleInfo.Location = new System.Drawing.Point(0, 0);
			this.lvSourceTitleInfo.Margin = new System.Windows.Forms.Padding(4);
			this.lvSourceTitleInfo.Name = "lvSourceTitleInfo";
			this.lvSourceTitleInfo.ShowItemToolTips = true;
			this.lvSourceTitleInfo.Size = new System.Drawing.Size(882, 234);
			this.lvSourceTitleInfo.TabIndex = 12;
			this.lvSourceTitleInfo.UseCompatibleStateImageBehavior = false;
			this.lvSourceTitleInfo.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader17
			// 
			this.columnHeader17.Text = "Тэги";
			this.columnHeader17.Width = 240;
			// 
			// columnHeader18
			// 
			this.columnHeader18.Text = "Значение";
			this.columnHeader18.Width = 420;
			// 
			// tpDocumentInfo
			// 
			this.tpDocumentInfo.Controls.Add(this.lvDocumentInfo);
			this.tpDocumentInfo.Location = new System.Drawing.Point(4, 25);
			this.tpDocumentInfo.Margin = new System.Windows.Forms.Padding(4);
			this.tpDocumentInfo.Name = "tpDocumentInfo";
			this.tpDocumentInfo.Size = new System.Drawing.Size(882, 234);
			this.tpDocumentInfo.TabIndex = 2;
			this.tpDocumentInfo.Text = "Document Info";
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
			this.lvDocumentInfo.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
			listViewItem21,
			listViewItem22,
			listViewItem23,
			listViewItem24,
			listViewItem25,
			listViewItem26,
			listViewItem27});
			this.lvDocumentInfo.Location = new System.Drawing.Point(0, 0);
			this.lvDocumentInfo.Margin = new System.Windows.Forms.Padding(4);
			this.lvDocumentInfo.Name = "lvDocumentInfo";
			this.lvDocumentInfo.ShowItemToolTips = true;
			this.lvDocumentInfo.Size = new System.Drawing.Size(882, 234);
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
			this.tpPublishInfo.Location = new System.Drawing.Point(4, 25);
			this.tpPublishInfo.Margin = new System.Windows.Forms.Padding(4);
			this.tpPublishInfo.Name = "tpPublishInfo";
			this.tpPublishInfo.Size = new System.Drawing.Size(882, 234);
			this.tpPublishInfo.TabIndex = 3;
			this.tpPublishInfo.Text = "Publish Info";
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
			this.lvPublishInfo.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
			listViewItem28,
			listViewItem29,
			listViewItem30,
			listViewItem31,
			listViewItem32,
			listViewItem33});
			this.lvPublishInfo.Location = new System.Drawing.Point(0, 0);
			this.lvPublishInfo.Margin = new System.Windows.Forms.Padding(4);
			this.lvPublishInfo.Name = "lvPublishInfo";
			this.lvPublishInfo.ShowItemToolTips = true;
			this.lvPublishInfo.Size = new System.Drawing.Size(882, 234);
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
			this.tpCustomInfo.Location = new System.Drawing.Point(4, 25);
			this.tpCustomInfo.Margin = new System.Windows.Forms.Padding(4);
			this.tpCustomInfo.Name = "tpCustomInfo";
			this.tpCustomInfo.Size = new System.Drawing.Size(882, 234);
			this.tpCustomInfo.TabIndex = 4;
			this.tpCustomInfo.Text = "Custom Info";
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
			this.lvCustomInfo.Location = new System.Drawing.Point(0, 0);
			this.lvCustomInfo.Margin = new System.Windows.Forms.Padding(4);
			this.lvCustomInfo.Name = "lvCustomInfo";
			this.lvCustomInfo.ShowItemToolTips = true;
			this.lvCustomInfo.Size = new System.Drawing.Size(882, 234);
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
			this.tpHistory.Location = new System.Drawing.Point(4, 25);
			this.tpHistory.Margin = new System.Windows.Forms.Padding(4);
			this.tpHistory.Name = "tpHistory";
			this.tpHistory.Size = new System.Drawing.Size(882, 234);
			this.tpHistory.TabIndex = 5;
			this.tpHistory.Text = "History";
			this.tpHistory.UseVisualStyleBackColor = true;
			// 
			// rtbHistory
			// 
			this.rtbHistory.BackColor = System.Drawing.SystemColors.Window;
			this.rtbHistory.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbHistory.Location = new System.Drawing.Point(0, 0);
			this.rtbHistory.Margin = new System.Windows.Forms.Padding(4);
			this.rtbHistory.Name = "rtbHistory";
			this.rtbHistory.ReadOnly = true;
			this.rtbHistory.Size = new System.Drawing.Size(882, 234);
			this.rtbHistory.TabIndex = 0;
			this.rtbHistory.Text = "";
			// 
			// tpAnnotation
			// 
			this.tpAnnotation.Controls.Add(this.rtbAnnotation);
			this.tpAnnotation.Location = new System.Drawing.Point(4, 25);
			this.tpAnnotation.Margin = new System.Windows.Forms.Padding(4);
			this.tpAnnotation.Name = "tpAnnotation";
			this.tpAnnotation.Size = new System.Drawing.Size(882, 234);
			this.tpAnnotation.TabIndex = 6;
			this.tpAnnotation.Text = "Annotation";
			this.tpAnnotation.UseVisualStyleBackColor = true;
			// 
			// rtbAnnotation
			// 
			this.rtbAnnotation.BackColor = System.Drawing.SystemColors.Window;
			this.rtbAnnotation.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtbAnnotation.Location = new System.Drawing.Point(0, 0);
			this.rtbAnnotation.Margin = new System.Windows.Forms.Padding(4);
			this.rtbAnnotation.Name = "rtbAnnotation";
			this.rtbAnnotation.ReadOnly = true;
			this.rtbAnnotation.Size = new System.Drawing.Size(882, 234);
			this.rtbAnnotation.TabIndex = 0;
			this.rtbAnnotation.Text = "";
			// 
			// tpValidate
			// 
			this.tpValidate.Controls.Add(this.tbValidate);
			this.tpValidate.Location = new System.Drawing.Point(4, 25);
			this.tpValidate.Margin = new System.Windows.Forms.Padding(4);
			this.tpValidate.Name = "tpValidate";
			this.tpValidate.Size = new System.Drawing.Size(882, 234);
			this.tpValidate.TabIndex = 7;
			this.tpValidate.Text = "Валидность";
			this.tpValidate.UseVisualStyleBackColor = true;
			// 
			// tbValidate
			// 
			this.tbValidate.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbValidate.Location = new System.Drawing.Point(0, 0);
			this.tbValidate.Margin = new System.Windows.Forms.Padding(4);
			this.tbValidate.Multiline = true;
			this.tbValidate.Name = "tbValidate";
			this.tbValidate.ReadOnly = true;
			this.tbValidate.Size = new System.Drawing.Size(882, 234);
			this.tbValidate.TabIndex = 0;
			// 
			// picBoxCover
			// 
			this.picBoxCover.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.picBoxCover.Dock = System.Windows.Forms.DockStyle.Left;
			this.picBoxCover.ErrorImage = ((System.Drawing.Image)(resources.GetObject("picBoxCover.ErrorImage")));
			this.picBoxCover.Location = new System.Drawing.Point(368, 30);
			this.picBoxCover.Margin = new System.Windows.Forms.Padding(4);
			this.picBoxCover.Name = "picBoxCover";
			this.picBoxCover.Size = new System.Drawing.Size(239, 263);
			this.picBoxCover.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
			this.picBoxCover.TabIndex = 44;
			this.picBoxCover.TabStop = false;
			// 
			// lvFilesCount
			// 
			this.lvFilesCount.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader6,
			this.columnHeader7});
			this.lvFilesCount.Dock = System.Windows.Forms.DockStyle.Left;
			this.lvFilesCount.FullRowSelect = true;
			this.lvFilesCount.GridLines = true;
			this.lvFilesCount.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
			listViewItem34,
			listViewItem35,
			listViewItem36,
			listViewItem37,
			listViewItem38,
			listViewItem39,
			listViewItem40});
			this.lvFilesCount.Location = new System.Drawing.Point(0, 30);
			this.lvFilesCount.Margin = new System.Windows.Forms.Padding(4);
			this.lvFilesCount.Name = "lvFilesCount";
			this.lvFilesCount.Size = new System.Drawing.Size(368, 263);
			this.lvFilesCount.TabIndex = 42;
			this.lvFilesCount.UseCompatibleStateImageBehavior = false;
			this.lvFilesCount.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Папки и файлы";
			this.columnHeader6.Width = 260;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Кол-во";
			this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader7.Width = 90;
			// 
			// chBoxViewProgress
			// 
			this.chBoxViewProgress.Dock = System.Windows.Forms.DockStyle.Top;
			this.chBoxViewProgress.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxViewProgress.Location = new System.Drawing.Point(0, 0);
			this.chBoxViewProgress.Margin = new System.Windows.Forms.Padding(4);
			this.chBoxViewProgress.Name = "chBoxViewProgress";
			this.chBoxViewProgress.Padding = new System.Windows.Forms.Padding(7, 0, 0, 0);
			this.chBoxViewProgress.Size = new System.Drawing.Size(1497, 30);
			this.chBoxViewProgress.TabIndex = 41;
			this.chBoxViewProgress.Text = "Отображать изменение хода работы";
			this.chBoxViewProgress.UseVisualStyleBackColor = true;
			this.chBoxViewProgress.CheckedChanged += new System.EventHandler(this.ChBoxViewProgressCheckedChanged);
			// 
			// lvResult
			// 
			this.lvResult.CheckBoxes = true;
			this.lvResult.ContextMenuStrip = this.cmsFB2;
			this.lvResult.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvResult.FullRowSelect = true;
			this.lvResult.HideSelection = false;
			this.lvResult.Location = new System.Drawing.Point(0, 138);
			this.lvResult.Margin = new System.Windows.Forms.Padding(4);
			this.lvResult.MultiSelect = false;
			this.lvResult.Name = "lvResult";
			this.lvResult.ShowItemToolTips = true;
			this.lvResult.Size = new System.Drawing.Size(1497, 236);
			this.lvResult.TabIndex = 45;
			this.lvResult.UseCompatibleStateImageBehavior = false;
			this.lvResult.View = System.Windows.Forms.View.Details;
			this.lvResult.SelectedIndexChanged += new System.EventHandler(this.LvResultSelectedIndexChanged);
			// 
			// imageListDup
			// 
			this.imageListDup.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListDup.ImageStream")));
			this.imageListDup.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListDup.Images.SetKeyName(0, "cover_no.png");
			// 
			// SFBTpFB2Dublicator
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.lvResult);
			this.Controls.Add(this.pInfo);
			this.Controls.Add(this.pExistFile);
			this.Controls.Add(this.pMode);
			this.Controls.Add(this.pSearchFBDup2Dirs);
			this.Controls.Add(this.tsDup);
			this.Controls.Add(this.ssProgress);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "SFBTpFB2Dublicator";
			this.Size = new System.Drawing.Size(1497, 689);
			this.pSearchFBDup2Dirs.ResumeLayout(false);
			this.pSearchFBDup2Dirs.PerformLayout();
			this.tsDup.ResumeLayout(false);
			this.tsDup.PerformLayout();
			this.cmsFB2.ResumeLayout(false);
			this.pMode.ResumeLayout(false);
			this.pExistFile.ResumeLayout(false);
			this.pExistFile.PerformLayout();
			this.pInfo.ResumeLayout(false);
			this.tcViewFB2Desc.ResumeLayout(false);
			this.tpTitleInfo.ResumeLayout(false);
			this.tpSourceTitleInfo.ResumeLayout(false);
			this.tpDocumentInfo.ResumeLayout(false);
			this.tpPublishInfo.ResumeLayout(false);
			this.tpCustomInfo.ResumeLayout(false);
			this.tpHistory.ResumeLayout(false);
			this.tpAnnotation.ResumeLayout(false);
			this.tpValidate.ResumeLayout(false);
			this.tpValidate.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picBoxCover)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
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
		private System.Windows.Forms.ToolStripMenuItem tsmiAllGroupsReValidate;
		private System.Windows.Forms.ToolStripMenuItem tsmiAllFilesInGroupReValidate;
		private System.Windows.Forms.Label lblFMFSGenres;
		private System.Windows.Forms.RadioButton rbtnFB2Librusec;
		private System.Windows.Forms.RadioButton rbtnFB22;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem tsmiDeleteCheckedFb2;
		private System.Windows.Forms.ToolStripMenuItem tsmiMoveCheckedFb2To;
		private System.Windows.Forms.ToolStripMenuItem tsmiCopyCheckedFb2To;
		private System.Windows.Forms.ToolStripButton tsbtnSearchFb2DupRenew;
		private System.Windows.Forms.Button btnOpenDir;
		private System.Windows.Forms.ToolStripMenuItem tsmiAnalyzeForAllGroups;
		private System.Windows.Forms.ToolStripMenuItem tsmiAllOldBooksLastWriteTimeInGroup;
		private System.Windows.Forms.ToolStripMenuItem tsmiAllOldBooksCreationTimeInGroup;
		private System.Windows.Forms.ToolStripMenuItem tsmiAnalyzeInGroup;
		private System.Windows.Forms.ToolStripMenuItem tsmiAllOldBooksLastWriteTimeForAllGroups;
		private System.Windows.Forms.ToolStripMenuItem tsmiAllOldBooksCreationTimeForAllGroups;
		private System.Windows.Forms.ToolStripMenuItem tsmiAllOldBooksForAllGroups;
		private System.Windows.Forms.ToolStripMenuItem tsmiAnalyzeForSelectedGroup;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ImageList imageListDup;
		private System.Windows.Forms.PictureBox picBoxCover;
		private System.Windows.Forms.ToolStrip tsDup;
		private System.Windows.Forms.CheckBox chBoxViewProgress;
		private System.Windows.Forms.Panel pInfo;
		private System.Windows.Forms.OpenFileDialog sfdLoadList;
		private System.Windows.Forms.SaveFileDialog sfdList;
		private System.Windows.Forms.ToolStripButton tsbtnDupOpenList;
		private System.Windows.Forms.ToolStripButton tsbtnDupSaveList;
		private System.Windows.Forms.ToolStripMenuItem tsmiUnCheckedAll;
		private System.Windows.Forms.ToolStripMenuItem tsmiCheckedAll;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.Panel pExistFile;
		private System.Windows.Forms.ComboBox cboxDupExistFile;
		private System.Windows.Forms.Label lblDupExistFile;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem tsmiDiffFB2;
		private System.Windows.Forms.ToolStripMenuItem tsmiDeleteFileFromDisk;
		private System.Windows.Forms.ToolStripMenuItem tsmiOpenFileDir;
		private System.Windows.Forms.ToolStripSeparator tsmi2;
		private System.Windows.Forms.ToolStripMenuItem tsmiViewInReader;
		private System.Windows.Forms.ToolStripSeparator tsmi1;
		private System.Windows.Forms.ToolStripMenuItem tsmiEditInFB2Editor;
		private System.Windows.Forms.ToolStripMenuItem tsmiEditInTextEditor;
		private System.Windows.Forms.ToolStripSeparator tsmi3;
		private System.Windows.Forms.ToolStripMenuItem tsmiFileReValidate;
		private System.Windows.Forms.ContextMenuStrip cmsFB2;
		private System.Windows.Forms.CheckBox chBoxIsValid;
		private System.Windows.Forms.ListView lvPublishInfo;
		private System.Windows.Forms.ListView lvTitleInfo;
		private System.Windows.Forms.ListView lvSourceTitleInfo;
		private System.Windows.Forms.ListView lvResult;
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
		private System.Windows.Forms.RichTextBox rtbAnnotation;
		private System.Windows.Forms.RichTextBox rtbHistory;
		private System.Windows.Forms.TabPage tpAnnotation;
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
		private readonly string	m_FileSettingsPath	= Settings.Settings.ProgDir + @"\DuplicatorSettings.xml";
		private readonly StatusView			m_sv			= new StatusView();
		private readonly MiscListView		m_mscLV			= new MiscListView(); // класс по работе с ListView
		private readonly FB2Validator		m_fv2Validator	= new FB2Validator();
		private readonly SharpZipLibWorker	m_sharpZipLib	= new SharpZipLibWorker();
		private readonly string				m_TempDir		= Settings.Settings.TempDir;
		
		private bool	m_isSettingsLoaded	= false; // Только при true все изменения настроек сохраняются в файл.
		private string	m_TargetDir			= string.Empty;
		private string	m_sMessTitle		= string.Empty;
		private int		CurrentResultItem	= -1;

		/// <summary>
		/// Данные о самой "новой" книге в группе
		/// </summary>
		private class FB2BookInfo {
			private int m_IndexVersion = 0;
			private int m_IndexCreationTime= 0;
			private int m_IndexLastWriteTime = 0;
			private string m_Version			= string.Empty;
			private DateTime  m_CreationTime	= Convert.ToDateTime("01/01/1900");
			private DateTime  m_LastWriteTime	= Convert.ToDateTime("01/01/1900");
			
			public virtual int IndexVersion {
				get { return m_IndexVersion; }
				set { m_IndexVersion = value; }
			}
			public virtual int IndexCreationTime {
				get { return m_IndexCreationTime; }
				set { m_IndexCreationTime = value; }
			}
			public virtual int IndexLastWriteTime {
				get { return m_IndexLastWriteTime; }
				set { m_IndexLastWriteTime = value; }
			}
			
			public virtual string Version {
				get { return m_Version; }
				set { m_Version = value; }
			}
			public virtual DateTime CreationTime {
				get { return m_CreationTime; }
				set { m_CreationTime = value; }
			}
			public virtual DateTime LastWriteTime {
				get { return m_LastWriteTime; }
				set { m_LastWriteTime = value; }
			}
		}
		#endregion
		
		public SFBTpFB2Dublicator()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			// задание для кнопок ToolStrip стиля и положения текста и картинки
			SetToolButtonsSettings();
			// создание колонок просмотрщика найденных книг
			MakeColumns();
			Init();

			cboxMode.SelectedIndex			= 0; // Условия для Сравнения fb2-файлов: md5 книги
			cboxDupExistFile.SelectedIndex	= 1; // добавление к создаваемому fb2-файлу очередного номера
			// загрузка настроек из xml
			readSettingsFromXML();
			m_isSettingsLoaded = true; // все настройки запгружены
		}
		// =============================================================================================
		// 									ОТКРЫТЫЕ МЕТОДЫ КЛАССА
		// =============================================================================================
		#region Открытые методы класса
		// задание для кнопок ToolStrip стиля и положения текста и картинки
		public void SetToolButtonsSettings() {
			Settings.FB2DublicatorSettings.SetToolButtonsSettings( tsDup );
		}
		#endregion
		
		// =============================================================================================
		// 							ВСПОМОГАТЕЛЬНЫЕ ЗАКРЫТЫЕ МЕТОДЫ И АЛГОРИТМЫ КЛАССА
		// =============================================================================================
		#region Вспомогательные методы и алгоритмы класса

		// удаление оставшейся книги в группе и самой группы с контрола отображения (1 книга - это уже не копия)
		private void workingGroupItemAfterBookDelete( System.Windows.Forms.ListView listView, ListViewGroup lvg ) {
			if( lvg.Items.Count <= 1 ) {
				if( lvg.Items.Count == 1 )
					listView.Items[lvg.Items[0].Index].Remove();
				listView.Groups.Remove( lvg );
			}
		}
		
		// обновление числа групп и книг во всех группах
		private void newGroupItemsCount( System.Windows.Forms.ListView lvResult, System.Windows.Forms.ListView lvFilesCount ) {
			// новое число групп
			m_mscLV.ListViewStatus( lvFilesCount, 5, lvResult.Groups.Count.ToString() );
			// число книг во всех группах
			m_mscLV.ListViewStatus( lvFilesCount, 6, lvResult.Items.Count.ToString() );
		}
		
		// создание колонок просмотрщика найденных книг
		private void MakeColumns() {
			lvResult.Columns.Add( "Путь к книге", 300 );
			lvResult.Columns.Add( "Название", 180 );
			lvResult.Columns.Add( "Автор(ы)", 180 );
			lvResult.Columns.Add( "Жанр(ы)", 180 );
			lvResult.Columns.Add( "ID", 200 );
			lvResult.Columns.Add( "Версия", 50 );
			lvResult.Columns.Add( "Кодировка", 90, HorizontalAlignment.Center );
			lvResult.Columns.Add( "Валидность", 50, HorizontalAlignment.Center );
			lvResult.Columns.Add( "Размер", 90, HorizontalAlignment.Center );
			
			lvResult.Columns.Add( "Дата создания", 120 );
			lvResult.Columns.Add( "Последнее изменение", 120 );
		}

		// Получение картинки из base64
		private Image Base64ToImage(string base64String) {
			// Convert Base64 String to byte[]
			byte[] imageBytes = Convert.FromBase64String(base64String);
			MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

			// Convert byte[] to Image
			ms.Write(imageBytes, 0, imageBytes.Length);
			Image image = Image.FromStream(ms, true);
			return image;
		}
		
		// отключение/включение обработчиков событий для lvResult (убираем "тормоза")
		private void ConnectListViewResultEventHandlers( bool bConnect ) {
			if( !bConnect ) {
				// отключаем обработчики событий для lvResult (убираем "тормоза")
				lvResult.BeginUpdate();
				this.lvResult.SelectedIndexChanged -= new System.EventHandler(this.LvResultSelectedIndexChanged);
			} else {
				this.lvResult.SelectedIndexChanged += new System.EventHandler(this.LvResultSelectedIndexChanged);
				lvResult.EndUpdate();
			}
		}
		
		// инициализация контролов и переменных
		private void Init() {
			ConnectListViewResultEventHandlers( false );
			for( int i=0; i!=lvFilesCount.Items.Count; ++i )
				lvFilesCount.Items[i].SubItems[1].Text	= "0";

			// очистка временной папки
			filesWorker.RemoveDir( Settings.Settings.TempDir );
			m_sv.Clear(); // сброс данных класса для отображения прогресса
			lvResult.Items.Clear();
			lvResult.Groups.Clear();
			ConnectListViewResultEventHandlers( true );
		}
		
		// очистка контролов вывода данных по книге по ее выбору
		private void ClearDataFields() {
			for( int i=0; i!=lvTitleInfo.Items.Count; ++i ) {
				lvTitleInfo.Items[i].SubItems[1].Text		= string.Empty;
				lvSourceTitleInfo.Items[i].SubItems[1].Text	= string.Empty;
			}
			for( int i=0; i!=lvDocumentInfo.Items.Count; ++i )
				lvDocumentInfo.Items[i].SubItems[1].Text = string.Empty;

			for( int i=0; i!=lvPublishInfo.Items.Count; ++i )
				lvPublishInfo.Items[i].SubItems[1].Text = string.Empty;

			lvCustomInfo.Items.Clear();
			rtbHistory.Clear();
			rtbAnnotation.Clear();
		}
		
		// сохранение настроек в xml-файл
		private void saveSettingsToXml() {
			#region Код
			if( m_isSettingsLoaded ) {
				// защита от "затирания" настроек в файле, когда в некоторые контролы данные еще не загрузились
				XDocument doc = new XDocument(
					new XDeclaration("1.0", "utf-8", "yes"),
					new XElement("Settings", new XAttribute("type", "dup_settings"),
					             new XComment("xml файл настроек Дубликатора"),
					             new XComment("Папка для поиска копий fb2 книг"),
					             new XElement("SourceDir", tboxSourceDir.Text.Trim()),
					             new XComment("Режим поиска-сравнения fb2 книг"),
					             new XElement("CompareMode",
					                          new XAttribute("index", cboxMode.SelectedIndex),
					                          new XAttribute("name", cboxMode.Text)
					                         ),
					             new XComment("Операции с одинаковыми fb2-файлами при копировании/перемещении"),
					             new XElement("FB2ExsistMode",
					                          new XAttribute("index", cboxDupExistFile.SelectedIndex),
					                          new XAttribute("name", cboxDupExistFile.Text)
					                         ),
					             new XComment("Активная Схема Жанров"),
					             new XElement("FB2Genres",
					                          new XAttribute("Librusec", rbtnFB2Librusec.Checked),
					                          new XAttribute("FB22", rbtnFB22.Checked)
					                         ),
					             new XComment("Настройки поиска-сравнения fb2 книг"),
					             new XElement("Options",
					                          new XElement("ScanSubDirs", chBoxScanSubDir.Checked),
					                          new XElement("CheckValidate", chBoxIsValid.Checked),
					                          new XElement("GenresFB2Librusec", rbtnFB2Librusec.Checked)
					                         ),
					             new XComment("Отображать изменения хода работы"),
					             new XElement("Progress", chBoxViewProgress.Checked),
					             new XComment("Папка для копирования/перемещения копий fb2 книг"),
					             new XElement("TargetDir", m_TargetDir)
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
				// Папка для поиска копий fb2 книг
				if( xmlTree.Element("SourceDir") != null )
					tboxSourceDir.Text = xmlTree.Element("SourceDir").Value;
				// Режим поиска-сравнения fb2 книг
				if( xmlTree.Element("SourceDir") != null ) {
					if( xmlTree.Element("SourceDir").Attribute("index") != null )
						cboxMode.SelectedIndex = Convert.ToInt16( xmlTree.Element("CompareMode").Attribute("index").Value );
				}
				// Режим поиска-сравнения fb2 книг
				if( xmlTree.Element("FB2ExsistMode") != null ) {
					if( xmlTree.Element("FB2ExsistMode").Attribute("index") != null )
						cboxDupExistFile.SelectedIndex = Convert.ToInt16( xmlTree.Element("FB2ExsistMode").Attribute("index").Value );
				}
				// Активная Схема Жанров
				if( xmlTree.Element("FB2Genres") != null ) {
					XElement xmlFB2Genres = xmlTree.Element("FB2Genres");
					if( xmlFB2Genres.Attribute("Librusec") != null )
						rbtnFB2Librusec.Checked = Convert.ToBoolean( xmlFB2Genres.Attribute("Librusec").Value );
					if( xmlFB2Genres.Attribute("FB22") != null )
						rbtnFB22.Checked = Convert.ToBoolean( xmlFB2Genres.Attribute("FB22").Value );
				}
				// установки сравнения
				if( xmlTree.Element("Options") != null ) {
					XElement xmlOptions = xmlTree.Element("Options");
					if( xmlOptions.Element("ScanSubDirs") != null )
						chBoxScanSubDir.Checked	= Convert.ToBoolean( xmlOptions.Element("ScanSubDirs").Value );
					if( xmlOptions.Element("CheckValidate") != null )
						chBoxIsValid.Checked	= Convert.ToBoolean( xmlOptions.Element("CheckValidate").Value );
					if( xmlOptions.Element("GenresFB2Librusec") != null )
						rbtnFB2Librusec.Checked	= Convert.ToBoolean( xmlOptions.Element("GenresFB2Librusec").Value );
				}
				// Отображать изменения хода работы
				if( xmlTree.Element("Progress") != null )
					chBoxViewProgress.Checked = Convert.ToBoolean( xmlTree.Element("Progress").Value );
				// Папка для копирования/перемещения копий fb2 книг
				if( xmlTree.Element("TargetDir") != null )
					m_TargetDir = xmlTree.Element("TargetDir").Value;
			}
			#endregion
		}
		
		// проверка на наличие папки сканирования копий книг
		private bool IsScanFolderDataCorrect( TextBox tbSource, ref string MessTitle ) {
			// проверка на корректность данных папок источника
			string sSource	= filesWorker.WorkingDirPath( tbSource.Text.Trim() );
			tbSource.Text	= sSource;
			
			// проверки на корректность папок источника
			if( sSource.Length == 0 ) {
				MessageBox.Show( "Выберите папку для сканирования!", MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			DirectoryInfo diFolder = new DirectoryInfo( sSource );
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка не найдена: " + sSource, MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			return true;
		}
		
		// извлечение информации из текста тэга <p>, убираем инлайн-теги для простоты
		private string getDataFromTagP( string sP ) {
			Regex rx = new Regex( "<empty-line[^>]*>" );
			string s = rx.Replace( sP, "\r\n" );
			rx = new Regex( "</p>" );
			s = rx.Replace( s, "</p>\r\n" );
			rx = new Regex( "<[^>]*>" );
			s = rx.Replace( s, string.Empty );
			return s;
		}
		
		// пометка всех "старых" книг в Группе/Группах по выбранному методу анализа
		private void checkOldBook( GroupAnalyzeMode ToGroupAnalyzeMode, CompareMode AnalyzeMode ) {
			ConnectListViewResultEventHandlers( false );
			Core.Duplicator.CopiesAnalyzeForm copiesAnalyzeForm = new Core.Duplicator.CopiesAnalyzeForm(
				ToGroupAnalyzeMode, AnalyzeMode, lvResult
			);
			copiesAnalyzeForm.ShowDialog();
			Core.Misc.EndWorkMode EndWorkMode = copiesAnalyzeForm.EndMode;
			copiesAnalyzeForm.Dispose();
			ConnectListViewResultEventHandlers( true );
			MessageBox.Show( EndWorkMode.Message, "Анализ копий fb2 книг", MessageBoxButtons.OK, MessageBoxIcon.Information );
		}
		
		// переместить помеченные файлы в папку-приемник
		// Fast = false: с удалением элементов списка копий - медленно. Fast = true: без удаления элементов списка копий - быстро
		void MoveCheckedFb2To( bool Fast ) {
			string sTarget = filesWorker.OpenDirDlg( m_TargetDir, fbdScanDir, "Укажите папку-приемник для размешения копий книг:" );
			if( sTarget == null )
				return;

			saveSettingsToXml();
			
			const string MessTitle = "SharpFBTools - Перемещение копий книг";
			if( tboxSourceDir.Text.Trim() == sTarget ) {
				MessageBox.Show( "Папка-приемник файлов совпадает с папкой сканирования!\nРабота прекращена.",
				                MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			
			ConnectListViewResultEventHandlers( false );
			Core.Duplicator.CopyMoveDeleteForm comrareForm = new Core.Duplicator.CopyMoveDeleteForm(
				Fast, DuplWorkMode.MoveCheckedBooks, tboxSourceDir.Text.Trim(), sTarget,
				cboxDupExistFile.SelectedIndex, lvFilesCount, lvResult, chBoxViewProgress.Checked
			);
			comrareForm.ShowDialog();
			Core.Misc.EndWorkMode EndWorkMode = comrareForm.EndMode;
			comrareForm.Dispose();
			ConnectListViewResultEventHandlers( true );
			MessageBox.Show( EndWorkMode.Message, MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
		}
		
		// удалить помеченные файлы (с удалением элементов списка копий - медленно)
		// Fast = false: с удалением элементов списка копий - медленно. Fast = true: без удаления элементов списка копий - быстро
		void DeleteCheckedFb2( bool Fast ) {
			const string sMessTitle = "SharpFBTools - Удаление копий книг";
			int nCount = lvResult.CheckedItems.Count;
			string sMess = "Вы действительно хотите удалить " + nCount.ToString() + " помеченных копии книг?";
			const MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			if( MessageBox.Show( sMess, sMessTitle, buttons, MessageBoxIcon.Question ) != DialogResult.No ) {
				ConnectListViewResultEventHandlers( false );
				Core.Duplicator.CopyMoveDeleteForm comrareForm = new Core.Duplicator.CopyMoveDeleteForm(
					Fast, DuplWorkMode.DeleteCheckedBooks, tboxSourceDir.Text.Trim(), null,
					cboxDupExistFile.SelectedIndex, lvFilesCount, lvResult, chBoxViewProgress.Checked
				);
				comrareForm.ShowDialog();
				Core.Misc.EndWorkMode EndWorkMode = comrareForm.EndMode;
				comrareForm.Dispose();
				ConnectListViewResultEventHandlers( true );
				MessageBox.Show( EndWorkMode.Message, sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
		}
		
		// Удалить всю Группу из Списка БЕЗ удаления книг с жесткого диска...
		void DeleteGroupNotFile() {
			const string MessTitle = "SharpFBTools - Удаление отмеченных Групп из списка";
			string sMess = "Вы действительно хотите удалить все отмеченные Группы из списка копий (файлы с жесткого диска НЕ удаляются)?";
			const MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			if( MessageBox.Show( sMess, MessTitle, buttons, MessageBoxIcon.Question ) != DialogResult.No ) {
				ConnectListViewResultEventHandlers( false );
				ListView.CheckedListViewItemCollection checkedItems = lvResult.CheckedItems;
				int RemoveGroupCount = 0;
				int RemoveItemCount = 0;
				foreach( ListViewItem lvi in checkedItems ) {
					ListViewGroup lvg = lvi.Group;
					RemoveItemCount += lvg.Items.Count;
					++RemoveGroupCount;
					lvResult.Groups.Remove(lvg);
				}
				
				//удаляем все верхние итемы по числу всех итемов удаленных групп
				for( int i = 0; i != RemoveItemCount; ++i )
					lvResult.Items.RemoveAt(0);
				
				// реальное число Групп и книг в них
				lvFilesCount.Items[5].SubItems[1].Text = (Convert.ToInt16(lvFilesCount.Items[5].SubItems[1].Text) - RemoveGroupCount).ToString();
				lvFilesCount.Items[6].SubItems[1].Text = (Convert.ToInt16(lvFilesCount.Items[6].SubItems[1].Text) - RemoveItemCount).ToString();
				
				ConnectListViewResultEventHandlers( true );
				MessageBox.Show( "Удаление Групп из списка завершено.", MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
		}
		
		#endregion
		
		// =============================================================================================
		// 									ОБРАБОТЧИКИ событий контролов
		// =============================================================================================
		#region Обработчики событий контролов
		void RbtnFB2LibrusecClick(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void RbtnFB22Click(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void TboxSourceDirTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void BtnOpenDirClick(object sender, EventArgs e)
		{
			filesWorker.OpenDirDlg( tboxSourceDir, fbdScanDir, "Укажите папку для сканирования с fb2 файлами:" );
		}
		
		// Поиск одинаковых fb2-файлов
		void TsbtnSearchDublsClick(object sender, EventArgs e)
		{
			#region Код
			string sMessTitle = "SharpFBTools - Поиск одинаковых fb2 файлов";
			// проверка на корректность данных папок источника и приемника файлов
			if( !IsScanFolderDataCorrect( tboxSourceDir, ref sMessTitle ) )
				return;
			
			// инициализация контролов
			Init();
			ConnectListViewResultEventHandlers( false );
			Core.Duplicator.CompareForm comrareForm = new Core.Duplicator.CompareForm(
				null, tboxSourceDir.Text.Trim(), cboxMode.SelectedIndex,
				chBoxScanSubDir.Checked, chBoxIsValid.Checked, rbtnFB2Librusec.Checked,
				lvFilesCount, lvResult, chBoxViewProgress.Checked
			);
			comrareForm.ShowDialog();
			Core.Misc.EndWorkMode EndWorkMode = comrareForm.EndMode;
			comrareForm.Dispose();
			ConnectListViewResultEventHandlers( true );
			MessageBox.Show( EndWorkMode.Message, sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			#endregion
		}
		
		// возобновить сравнение и поиск копий по данным из файла, созданного после прерывания обработки
		void TsbtnSearchFb2DupRenewClick(object sender, EventArgs e)
		{
			#region Код
			// загрузка данных из xml
			sfdLoadList.InitialDirectory = Settings.Settings.ProgDir;
			sfdLoadList.Title		= "Укажите файл для возобновления поиска копий книг:";
			sfdLoadList.Filter		= "SharpFBTools Файлы хода работы Дубликатора (*.dup_break)|*.dup_break";
			sfdLoadList.FileName	= string.Empty;
			DialogResult result		= sfdLoadList.ShowDialog();
			XElement xmlTree = null;
			if( result == DialogResult.OK )
				xmlTree = XElement.Load( sfdLoadList.FileName );
			else
				return;
			
			// инициализация контролов
			Init();
			if( xmlTree != null ) {
				// выставляем режим сравнения
				cboxMode.SelectedIndex = Convert.ToInt16( xmlTree.Element("CompareMode").Attribute("index").Value );
				// устанавливаем данные настройки поиска-сравнения
				tboxSourceDir.Text = xmlTree.Element("SourceDir").Value;
				chBoxScanSubDir.Checked = Convert.ToBoolean( xmlTree.Element("Settings").Element("ScanSubDirs").Value );
				chBoxIsValid.Checked = Convert.ToBoolean( xmlTree.Element("Settings").Element("CheckValidate").Value );
				rbtnFB2Librusec.Checked = Convert.ToBoolean( xmlTree.Element("Settings").Element("GenresFB2Librusec").Value );
			}
			ConnectListViewResultEventHandlers( false );
			Core.Duplicator.CompareForm comrareForm = new Core.Duplicator.CompareForm(
				sfdLoadList.FileName, tboxSourceDir.Text.Trim(), cboxMode.SelectedIndex,
				chBoxScanSubDir.Checked, chBoxIsValid.Checked, rbtnFB2Librusec.Checked,
				lvFilesCount, lvResult, chBoxViewProgress.Checked
			);
			comrareForm.ShowDialog();
			tboxSourceDir.Text = comrareForm.getSourceDirFromRenew();
			Core.Misc.EndWorkMode EndWorkMode = comrareForm.EndMode;
			comrareForm.Dispose();
			ConnectListViewResultEventHandlers( true );
			MessageBox.Show( EndWorkMode.Message, "SharpFBTools - Поиск одинаковых fb2 файлов", MessageBoxButtons.OK, MessageBoxIcon.Information );
			#endregion
		}
		
		// занесение данных книги в контролы для просмотра
		void LvResultSelectedIndexChanged(object sender, EventArgs e)
		{
			#region Код
			ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
			// пропускаем ситуацию, когда курсор переходит от одной строки к другой - нет выбранного item'а
			if( si.Count > 0 ) {
				// защита от двойного срабатывания
				if( CurrentResultItem != si[0].Index ) {
					// очистка контролов вывода данных по книге по ее выбору
					ClearDataFields();
					CurrentResultItem = si[0].Index;
					if( File.Exists( si[0].Text ) ) {
						string FilePath = si[0].Text;
						string Ext = Path.GetExtension( FilePath ).ToLower();
						if( Ext == ".zip" || Ext == ".fbz" ) {
							m_sharpZipLib.UnZipFiles(FilePath, m_TempDir, 0, false, null, 4096);
							string [] files = Directory.GetFiles( m_TempDir );
							FilePath = files[0];
						}
						
						FB2BookDescription	bd = new FB2BookDescription( FilePath );
						// считываем данные TitleInfo
						m_mscLV.ListViewStatus( lvTitleInfo, 0, bd.TIBookTitle );
						m_mscLV.ListViewStatus( lvTitleInfo, 1, bd.TIGenres );
						m_mscLV.ListViewStatus( lvTitleInfo, 2, bd.TILang );
						m_mscLV.ListViewStatus( lvTitleInfo, 3, bd.TISrcLang );
						m_mscLV.ListViewStatus( lvTitleInfo, 4, bd.TIAuthors );
						m_mscLV.ListViewStatus( lvTitleInfo, 5, bd.TIDate );
						m_mscLV.ListViewStatus( lvTitleInfo, 6, bd.TIKeywords );
						m_mscLV.ListViewStatus( lvTitleInfo, 7, bd.TITranslators );
						m_mscLV.ListViewStatus( lvTitleInfo, 8, bd.TISequences );
						m_mscLV.ListViewStatus( lvTitleInfo, 9, bd.TICoverpagesCount.ToString() );
						// считываем данные SourceTitleInfo
						m_mscLV.ListViewStatus( lvSourceTitleInfo, 0, bd.STIBookTitle );
						m_mscLV.ListViewStatus( lvSourceTitleInfo, 1, bd.STIGenres );
						m_mscLV.ListViewStatus( lvSourceTitleInfo, 2, bd.STILang );
						m_mscLV.ListViewStatus( lvSourceTitleInfo, 3, bd.STISrcLang );
						m_mscLV.ListViewStatus( lvSourceTitleInfo, 4, bd.STIAuthors );
						m_mscLV.ListViewStatus( lvSourceTitleInfo, 5, bd.STIDate );
						m_mscLV.ListViewStatus( lvSourceTitleInfo, 6, bd.STIKeywords );
						m_mscLV.ListViewStatus( lvSourceTitleInfo, 7, bd.STITranslators );
						m_mscLV.ListViewStatus( lvSourceTitleInfo, 8, bd.STISequences );
						m_mscLV.ListViewStatus( lvSourceTitleInfo, 9, bd.STICoverpagesCount.ToString() );
						// считываем данные DocumentInfo
						m_mscLV.ListViewStatus( lvDocumentInfo, 0, bd.DIID );
						m_mscLV.ListViewStatus( lvDocumentInfo, 1, bd.DIVersion );
						m_mscLV.ListViewStatus( lvDocumentInfo, 2, bd.DIFB2Date );
						m_mscLV.ListViewStatus( lvDocumentInfo, 3, bd.DIProgramUsed );
						m_mscLV.ListViewStatus( lvDocumentInfo, 4, bd.DISrcOcr );
						m_mscLV.ListViewStatus( lvDocumentInfo, 5, bd.DISrcUrls );
						m_mscLV.ListViewStatus( lvDocumentInfo, 6, bd.DIFB2Authors );
						// считываем данные PublishInfo
						m_mscLV.ListViewStatus( lvPublishInfo, 0, bd.PIBookName );
						m_mscLV.ListViewStatus( lvPublishInfo, 1, bd.PIPublisher );
						m_mscLV.ListViewStatus( lvPublishInfo, 2, bd.PIYear );
						m_mscLV.ListViewStatus( lvPublishInfo, 3, bd.PICity );
						m_mscLV.ListViewStatus( lvPublishInfo, 4, bd.PIISBN );
						m_mscLV.ListViewStatus( lvPublishInfo, 5, bd.PISequences );
						// считываем данные CustomInfo
						lvCustomInfo.Items.Clear();
						IList<CustomInfo> lcu = bd.CICustomInfo;
						if( lcu != null ) {
							foreach( CustomInfo ci in lcu ) {
								ListViewItem lvi = new ListViewItem( ci.InfoType );
								lvi.SubItems.Add( ci.Value );
								lvCustomInfo.Items.Add( lvi );
							}
						}
						// считываем данные History
						rtbHistory.Clear(); rtbHistory.Text = getDataFromTagP( bd.DIHistory );
						// считываем данные Annotation
						rtbAnnotation.Clear(); rtbAnnotation.Text = getDataFromTagP( bd.TIAnnotation );
						// загрузка обложки
						if (bd.CoversBase64 != null) {
							picBoxCover.Image = Base64ToImage(bd.CoversBase64[0].base64String);
						} else {
							picBoxCover.Image = imageListDup.Images[0];
						}
						// Валидность файла
						tbValidate.Clear();
						if( si[0].SubItems[7].Text == "Нет" ) {
							string sResult	= rbtnFB2Librusec.Checked
								? m_fv2Validator.ValidatingFB2LibrusecFile( si[0].Text )
								: m_fv2Validator.ValidatingFB22File( si[0].Text );
							tbValidate.Text = "Файл невалидный. Ошибка:";
							tbValidate.AppendText( Environment.NewLine );
							tbValidate.AppendText( Environment.NewLine );
							tbValidate.AppendText( sResult );
						} else if( si[0].SubItems[7].Text == "Да" ) {
							tbValidate.Text = "Все в порядке - файл валидный!";
						} else {
							tbValidate.Text = "Валидация файла не производилась.";
						}
						filesWorker.RemoveDir( m_TempDir );
					}
				}
			}
			#endregion
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
			#region Код
			const string MessTitle = "SharpFBTools - Сохранение списка копий fb2 книг";
			if( lvResult.Items.Count == 0 ) {
				MessageBox.Show( "Нет ни одной копии fb2 книг!", MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			sfdList.Title = "Укажите название файла копий:";
			sfdList.Filter = "SharpFBTools Файлы копий книг (*.dup_lbc)|*.dup_lbc";
			sfdList.FileName = string.Empty;
			sfdList.InitialDirectory = Settings.Settings.ProgDir;
			DialogResult result = sfdList.ShowDialog();
			if( result == DialogResult.OK ) {
				ConnectListViewResultEventHandlers( false );
				Environment.CurrentDirectory = Settings.Settings.ProgDir;
				Core.Duplicator.CopiesListWorkerForm fileWorkerForm = new Core.Duplicator.CopiesListWorkerForm(
					DuplWorkMode.SaveFB2CopiesList, sfdList.FileName, cboxMode,
					lvResult, lvFilesCount, tboxSourceDir,
					chBoxScanSubDir, chBoxIsValid, rbtnFB2Librusec, chBoxViewProgress.Checked,
					lvResult.SelectedItems[0].Index
				);
				fileWorkerForm.ShowDialog();
				Core.Misc.EndWorkMode EndWorkMode = fileWorkerForm.EndMode;
				fileWorkerForm.Dispose();
				ConnectListViewResultEventHandlers( true );
				MessageBox.Show( EndWorkMode.Message, MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
			
			#endregion
		}
		
		// загрузка списка копий
		void TsbtnDupOpenListClick(object sender, EventArgs e)
		{
			#region Код
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
				Init();
				// установка режима поиска
				if( !File.Exists( FromXML ) ) {
					MessageBox.Show( "Не найден файл списка копий fb2 книг: \""+FromXML+"\"!", MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return;
				}
				XmlReaderSettings data = new XmlReaderSettings();
				data.IgnoreWhitespace = true;
				try {
					// отключаем обработчики событий для lvResult (убираем "тормоза")
					ConnectListViewResultEventHandlers( false );
					Environment.CurrentDirectory = Settings.Settings.ProgDir;
					Core.Duplicator.CopiesListWorkerForm fileWorkerForm = new Core.Duplicator.CopiesListWorkerForm(
						DuplWorkMode.LoadFB2CopiesList, FromXML, cboxMode, lvResult, lvFilesCount,
						tboxSourceDir, chBoxScanSubDir, chBoxIsValid, rbtnFB2Librusec, chBoxViewProgress.Checked, -1
					);
					fileWorkerForm.ShowDialog();
					Core.Misc.EndWorkMode EndWorkMode = fileWorkerForm.EndMode;
					fileWorkerForm.Dispose();
					
					// Отобразим результат в индикаторе прогресса
					m_mscLV.ListViewStatus( lvFilesCount, 5, lvResult.Groups.Count );
					m_mscLV.ListViewStatus( lvFilesCount, 6, lvResult.Items.Count );
					ConnectListViewResultEventHandlers( true );
					MessageBox.Show( EndWorkMode.Message, MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				} catch {
					ConnectListViewResultEventHandlers( true );
					MessageBox.Show( "Поврежден файл списка копий fb2 книг: \""+FromXML+"\"!", MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				}
			}
			#endregion
		}
		
		void CboxModeSelectedIndexChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void CboxDupExistFileSelectedIndexChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void ChBoxScanSubDirCheckStateChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void ChBoxIsValidCheckStateChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void ChBoxViewProgressCheckedChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		#endregion
		
		// =============================================================================================
		// 								ОБРАБОТЧИКИ для контекстного меню
		// =============================================================================================
		#region Обработчики для контекстного меню
		// удаление выделенного файла с диска
		void TsmiDeleteFileFromDiskClick(object sender, EventArgs e)
		{
			#region Код
			ConnectListViewResultEventHandlers( false );
			if( lvResult.Items.Count > 0 && lvResult.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
				ListViewGroup	lvg			= si[0].Group;
				string			sFilePath	= si[0].SubItems[0].Text.Split('/')[0];
				string sTitle = "SharpFBTools - Удаление файла с диска";
				if( !File.Exists( sFilePath ) ) {
					if( MessageBox.Show( "Файл: \""+sFilePath+"\" не найден!\nУдалить путь к этому файлу из списка?",
					                    sTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes ) {
						lvResult.Items[ lvResult.SelectedItems[0].Index ].Remove();
					}
				} else {
					if( MessageBox.Show( "Вы действительно хотите удалить файл: \""+sFilePath+"\" с диска?", sTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes ) {
						File.Delete( sFilePath );
						lvResult.Items[ lvResult.SelectedItems[0].Index ].Remove();
					} else
						return;
				}
				
				// удаление оставшейся книги в группе и самой группы с контрола отображения (1 книга - это уже не копия)
				workingGroupItemAfterBookDelete( lvResult, lvg );
				// обновление числа групп и книг во всех группах
				newGroupItemsCount( lvResult, lvFilesCount );
			}
			ConnectListViewResultEventHandlers( true );
			#endregion
		}
		
		// Открыть папку для выделенного файла
		void TsmiOpenFileDirClick(object sender, EventArgs e)
		{
			#region Код
			if( lvResult.Items.Count > 0 && lvResult.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
				FileInfo fi = new FileInfo( si[0].SubItems[0].Text.Split('/')[0] );
				string sDir = fi.Directory.ToString();
				if( !Directory.Exists( sDir ) ) {
					MessageBox.Show( "Папка: \""+sDir+"\" не найдена!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				filesWorker.ShowAsyncDir( sDir );
			}
			#endregion
		}
		
		// запустить файл в fb2-читалке (Просмотр)
		void TsmiViewInReaderClick(object sender, EventArgs e)
		{
			#region Код
			// читаем путь к читалке из настроек
			string sFBReaderPath = Settings.Settings.ReadFBReaderPath();
			const string sTitle = "SharpFBTools - Открытие папки для файла";
			if( !File.Exists( sFBReaderPath ) ) {
				MessageBox.Show( "Не могу найти Читалку \""+sFBReaderPath+"\"!\nПроверьте, правильно ли задан путь в Настройках.", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			if( lvResult.Items.Count > 0 && lvResult.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
				string sFilePath = si[0].SubItems[0].Text.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: \""+sFilePath+"\" не найден!", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				filesWorker.StartAsyncFile( sFBReaderPath, sFilePath );
			}
			#endregion
		}
		
		// редактировать выделенный файл в fb2-редакторе
		void TsmiEditInFB2EditorClick(object sender, EventArgs e)
		{
			#region Код
			// читаем путь к FBE из настроек
			string sFBEPath = Settings.Settings.ReadFBEPath();
			const string sTitle = "SharpFBTools - Открытие файла в fb2-редакторе";
			if( !File.Exists( sFBEPath ) ) {
				MessageBox.Show( "Не могу найти fb2 редактор \""+sFBEPath+"\"!\nПроверьте, правильно ли задан путь в Настройках.", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			if( lvResult.Items.Count > 0 && lvResult.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
				string sFilePath = si[0].SubItems[0].Text.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: \""+sFilePath+"\" не найден!", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				} else if( Path.GetExtension( sFilePath ).ToLower() == ".fb2" ) {
					// только для fb2 (не для zip)
					filesWorker.StartAsyncFile( sFBEPath, sFilePath );
				}
			}
			#endregion
		}
		
		// редактировать выделенный файл в текстовом редакторе
		void TsmiEditInTextEditorClick(object sender, EventArgs e)
		{
			#region Код
			// читаем путь к текстовому редактору из настроек
			string sTFB2Path = Settings.Settings.ReadTextFB2EPath();
			const string sTitle = "SharpFBTools - Открытие файла в текстовом редакторе";
			if( !File.Exists( sTFB2Path ) ) {
				MessageBox.Show( "Не могу найти текстовый редактор \""+sTFB2Path+"\"!\nПроверьте, правильно ли задан путь в Настройках.", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			if( lvResult.Items.Count > 0 && lvResult.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
				string sFilePath = si[0].SubItems[0].Text.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: \""+sFilePath+"\" не найден!", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				} else if( Path.GetExtension( sFilePath ).ToLower() == ".fb2" ) {
					// только для fb2 (не для zip)
					filesWorker.StartAsyncFile( sTFB2Path, sFilePath );
				}
			}
			#endregion
		}
		
		// Повторная Проверка выбранного fb2-файла (Валидация)
		void TsmiFileReValidateClick(object sender, EventArgs e)
		{
			#region Код
			if( lvResult.Items.Count > 0 && lvResult.SelectedItems.Count != 0 ) {
				DateTime dtStart = DateTime.Now;
				ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
				string sSelectedItemText = si[0].SubItems[(int)ResultViewCollumn.Path].Text;
				string sFilePath = sSelectedItemText.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: \""+sFilePath+"\" не найден!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				MessageBoxIcon mbi = MessageBoxIcon.Information;
				string Msg		= string.Empty;
				string ErrorMsg	= "СООБЩЕНИЕ ОБ ОШИБКЕ:";
				string OkMsg	= "ОШИБОК НЕТ - ФАЙЛ ВАЛИДЕН";
				
				Msg = Core.Misc.ZipFB2Validation.IsValid( sFilePath, rbtnFB2Librusec.Checked );
				
				if ( Msg == string.Empty ) {
					// файл валидный
					mbi = MessageBoxIcon.Information;
					ErrorMsg = OkMsg;
					si[0].SubItems[(int)ResultViewCollumn.Validate].Text = "Да";
				} else {
					// файл не валидный
					mbi = MessageBoxIcon.Error;
					si[0].SubItems[(int)ResultViewCollumn.Validate].Text = "Нет";
				}
				DateTime dtEnd = DateTime.Now;
				string sTime = dtEnd.Subtract( dtStart ).ToString() + " (час.:мин.:сек.)";
				filesWorker.RemoveDir( m_TempDir );
				MessageBox.Show( "Проверка выделенного файла на соответствие FictionBook.xsd схеме завершена.\nЗатрачено времени: "+sTime+"\n\nФайл: \""+sFilePath+"\"\n\n"+ErrorMsg+"\n"+Msg, "SharpFBTools - "+ErrorMsg, MessageBoxButtons.OK, mbi );
			}
			#endregion
		}
		
		// Повторная Проверка всех fb2-файлов одной Группы (Валидация)
		void TsmiAllFilesInGroupReValidateClick(object sender, EventArgs e)
		{
			#region Код
			if( lvResult.Items.Count > 0 ) {
				ConnectListViewResultEventHandlers( false );
				Core.Duplicator.ValidatorForm validatorForm = new Core.Duplicator.ValidatorForm(
					false, rbtnFB2Librusec.Checked, lvResult
				);
				validatorForm.ShowDialog();

				Core.Misc.EndWorkMode EndWorkMode = validatorForm.EndMode;
				validatorForm.Dispose();
				ConnectListViewResultEventHandlers( true );
				
				MessageBox.Show( EndWorkMode.Message, "SharpFBTools - Валидация всех книг выбранной Группы", MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
			#endregion
		}
		
		// Повторная Проверка всех fb2-файлов всех Групп (Валидация)
		void TsmiAllGroupsReValidateClick(object sender, EventArgs e)
		{
			#region Код
			if( lvResult.Items.Count > 0 ) {
				ConnectListViewResultEventHandlers( false );
				Core.Duplicator.ValidatorForm validatorForm = new Core.Duplicator.ValidatorForm(
					true, rbtnFB2Librusec.Checked, lvResult
				);
				validatorForm.ShowDialog();
				
				Core.Misc.EndWorkMode EndWorkMode = validatorForm.EndMode;
				validatorForm.Dispose();
				ConnectListViewResultEventHandlers( true );

				MessageBox.Show( EndWorkMode.Message, "SharpFBTools - Валидация всех книг всех Групп", MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
			#endregion
		}
		
		// diff - две помеченные fb2-книги
		void TsmiDiffFB2Click(object sender, EventArgs e)
		{
			#region Код
			ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
			if( lvResult.Items.Count > 0 && lvResult.SelectedItems.Count != 0 ) {
				// проверка на наличие diff-программы
				const string sDiffTitle = "SharpFBTools - diff";
				string sDiffPath = Settings.Settings.ReadDiffPath();
				
				if( sDiffPath.Trim().Length==0 ) {
					MessageBox.Show( "В Настройках не указан путь к установленной diff-программе визуального сравнения файлов!\nУкажите путь к ней в Настройках.\nРабота остановлена!",
					                sDiffTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return;
				} else {
					if( !File.Exists( sDiffPath ) ) {
						MessageBox.Show( "Не найден файл diff-программы визуального сравнения файлов \""+sDiffPath+"\"!\nУкажите путь к ней в Настройках.\nРабота остановлена!",
						                sDiffTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
						return;
					}
				}
				// группа для выделенной книги
				ListViewGroup lvg = si[0].Group;
				// книги выбранной группы
				ListView.ListViewItemCollection glvic = lvg.Items;
				List<string> l = new List<string>();
				foreach( ListViewItem lvi in glvic ) {
					if( lvi.Checked )
						l.Add( lvi.Text );
					if( l.Count==2 )
						break;
				}
				// запускаем инструмент сравнения
				if( l.Count==2 ) {
					string sFilesNotExists = string.Empty;
					if( !File.Exists( l[0] ) ) {
						sFilesNotExists += l[0]; sFilesNotExists += "\n";
					}
					
					if( !File.Exists( l[1] ) ) {
						sFilesNotExists += l[1]; sFilesNotExists += "\n";
					}

					if( sFilesNotExists != string.Empty )
						MessageBox.Show( "Не найден(ы) файл(ы) для сравнения:\n"+sFilesNotExists+"\nРабота остановлена!",
						                sDiffTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					else {
						if( Path.GetExtension( l[0] ).ToLower() == ".fb2" && Path.GetExtension( l[1] ).ToLower() == ".fb2" ) {
							filesWorker.StartAsyncDiff( sDiffPath, l[0], l[1] );
						}
					}
				}
			}
			#endregion
		}
		
		// отметить все книги выбранной Группы
		void TsmiCheckedAllInGroupClick(object sender, EventArgs e)
		{
			ConnectListViewResultEventHandlers( false );
			ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
			if (si.Count > 0) {
				// группа для выделенной книги
				ListViewGroup lvg = si[0].Group;
				m_mscLV.CheckAllListViewItemsInGroup( lvg, true );
			}
			ConnectListViewResultEventHandlers( true );
		}
		
		// отметить все книги всех Групп
		void TsmiCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListViewResultEventHandlers( false );
			m_mscLV.CheckAllListViewItems( lvResult, true );
			ConnectListViewResultEventHandlers( true );
		}
		
		// снять отметки со всех книг
		void TsmiUnCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListViewResultEventHandlers( false );
			m_mscLV.UnCheckAllListViewItems( lvResult.CheckedItems );
			ConnectListViewResultEventHandlers( true );
		}
		
		// записать путь ко всем помеченных книгам в файл
		void TsmiSaveAllCheckedItemToFileClick(object sender, EventArgs e)
		{
			#region Код
			// проверка, есть ли хоть один помеченный файл
			const string MessTitle = "SharpFBTools - сохранение путей помеченных книг";
			if( lvResult.CheckedItems.Count == 0 ) {
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
//				ConnectListViewResultEventHandlers( false );
				Environment.CurrentDirectory = Settings.Settings.ProgDir;
				Core.Duplicator.CheckedBooksPathToXMLForm filesForm =
					new Core.Duplicator.CheckedBooksPathToXMLForm(
						tboxSourceDir.Text.Trim() , sfdList.FileName, lvResult
					);
				filesForm.ShowDialog();
				Core.Misc.EndWorkMode EndWorkMode = filesForm.EndMode;
				filesForm.Dispose();
//				ConnectListViewResultEventHandlers( true );
				MessageBox.Show( EndWorkMode.Message, MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
			#endregion
		}
		
		// пометить в каждой группе все "старые" книги (по тэгу version)
		void TsmiAllOldBooksForAllGroupsClick(object sender, EventArgs e)
		{
			checkOldBook( GroupAnalyzeMode.AllGroup, CompareMode.Version );
		}
		
		// пометить в каждой группе все "старые" книги (по времени создания файла)
		void TsmiAllOldBooksCreationTimeForAllGroupsClick(object sender, EventArgs e)
		{
			checkOldBook( GroupAnalyzeMode.AllGroup, CompareMode.CreationTime );
		}
		
		// пометить в каждой группе все "старые" книги (по времени последнего изменения файла)
		void TsmiAllOldBooksLastWriteTimeForAllGroupsClick(object sender, EventArgs e)
		{
			checkOldBook( GroupAnalyzeMode.AllGroup, CompareMode.LastWriteTime );
		}
		
		// пометить в выбранной группе все "старые" книги (по тэгу version)
		void TsmiAnalyzeInGroupClick(object sender, EventArgs e)
		{
			checkOldBook( GroupAnalyzeMode.Group, CompareMode.Version );
		}
		
		// пометить в выбранной группе все "старые" книги (по времени создания файла)
		void TsmiAllOldBooksCreationTimeInGroupClick(object sender, EventArgs e)
		{
			checkOldBook( GroupAnalyzeMode.Group, CompareMode.CreationTime );
		}
		
		// пометить в выбранной группе все "старые" книги (по времени последнего изменения файла)
		void TsmiAllOldBooksLastWriteTimeInGroupClick(object sender, EventArgs e)
		{
			checkOldBook( GroupAnalyzeMode.Group, CompareMode.LastWriteTime );
		}
		
		// копировать помеченные файлы в папку-приемник
		void TsmiCopyCheckedFb2ToClick(object sender, EventArgs e)
		{
			#region Код
			string sTarget = filesWorker.OpenDirDlg( m_TargetDir, fbdScanDir, "Укажите папку-приемник для размешения копий книг:" );
			if( sTarget == null )
				return;

			saveSettingsToXml();

			const string MessTitle = "SharpFBTools - Копирование копий книг";
			if( tboxSourceDir.Text.Trim() == sTarget ) {
				MessageBox.Show( "Папка-приемник файлов совпадает с папкой сканирования!\nРабота прекращена.",
				                MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			
			ConnectListViewResultEventHandlers( false );
			Core.Duplicator.CopyMoveDeleteForm comrareForm = new Core.Duplicator.CopyMoveDeleteForm(
				false, DuplWorkMode.CopyCheckedBooks, tboxSourceDir.Text.Trim(), sTarget,
				cboxDupExistFile.SelectedIndex, lvFilesCount, lvResult, chBoxViewProgress.Checked
			);
			comrareForm.ShowDialog();
			Core.Misc.EndWorkMode EndWorkMode = comrareForm.EndMode;
			comrareForm.Dispose();
			ConnectListViewResultEventHandlers( true );
			MessageBox.Show( EndWorkMode.Message, MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			#endregion
		}
		
		// переместить помеченные файлы в папку-приемник (с удалением элементов списка копий - медленно)
		void TsmiMoveCheckedFb2ToClick(object sender, EventArgs e)
		{
			MoveCheckedFb2To( false );
		}
		// переместить помеченные файлы в папку-приемник (без удаления элементов списка копий - быстро)
		void TsmiMoveCheckedFb2ToFastClick(object sender, EventArgs e)
		{
			MoveCheckedFb2To( true );
		}
		
		// удалить помеченные файлы (с удалением элементов списка копий - медленно)
		void TsmiDeleteCheckedFb2Click(object sender, EventArgs e)
		{
			DeleteCheckedFb2( false );
		}
		// удалить помеченные файлы (без удаления элементов списка копий - быстро)
		void TsmiDeleteCheckedFb2FastClick(object sender, EventArgs e)
		{
			DeleteCheckedFb2( true );
		}
		void TsmiDeleteGroupNotFileClick(object sender, EventArgs e)
		{
			DeleteGroupNotFile();
		}

		#endregion
		
	}
}
