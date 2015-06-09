/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 16.03.2009
 * Time: 9:55
 * 
 * License: GPL 2.1
 */

using System;
using System.IO;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

using SharpZipLibWorker	= Core.Misc.SharpZipLibWorker;
using filesWorker 		= Core.Misc.FilesWorker;

namespace SharpFBTools.Tools
{
	/// <summary>
	/// FBTpArchiveManager: работа с архивами
	/// </summary>
	public partial class SFBTpArchiveManager : UserControl
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SFBTpArchiveManager));
			System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
			"Всего папок",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem(new string[] {
			"Всего файлов",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem(new string[] {
			"fb2-файлов",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem(new string[] {
			"Всего папок",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem(new string[] {
			"Всего файлов",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem(new string[] {
			"Распаковано архивов",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem7 = new System.Windows.Forms.ListViewItem(new string[] {
			"fb2-файлы из этих архивов",
			"0"}, 0);
			this.tsArchiver = new System.Windows.Forms.ToolStrip();
			this.tsbtnArchive = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnArchiveStop = new System.Windows.Forms.ToolStripButton();
			this.ssProgress = new System.Windows.Forms.StatusStrip();
			this.tsslblProgress = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.pScanDir = new System.Windows.Forms.Panel();
			this.btnOpenDir = new System.Windows.Forms.Button();
			this.cboxToSomeDir = new System.Windows.Forms.CheckBox();
			this.cboxScanSubDirToArchive = new System.Windows.Forms.CheckBox();
			this.tboxSourceDir = new System.Windows.Forms.TextBox();
			this.lblDir = new System.Windows.Forms.Label();
			this.pCentral = new System.Windows.Forms.Panel();
			this.tcArchiver = new System.Windows.Forms.TabControl();
			this.tpArchive = new System.Windows.Forms.TabPage();
			this.gboxCount = new System.Windows.Forms.GroupBox();
			this.lvGeneralCount = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.chBoxViewProgressA = new System.Windows.Forms.CheckBox();
			this.pOptions = new System.Windows.Forms.Panel();
			this.cboxDelFB2Files = new System.Windows.Forms.CheckBox();
			this.pType = new System.Windows.Forms.Panel();
			this.cboxExistArchive = new System.Windows.Forms.ComboBox();
			this.lblExistArchive = new System.Windows.Forms.Label();
			this.pToAnotherDir = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.tboxToAnotherDir = new System.Windows.Forms.TextBox();
			this.btnToAnotherDir = new System.Windows.Forms.Button();
			this.tpUnArchive = new System.Windows.Forms.TabPage();
			this.gboxUACount = new System.Windows.Forms.GroupBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.lvUAGeneralCount = new System.Windows.Forms.ListView();
			this.cHeaderDirsFiles = new System.Windows.Forms.ColumnHeader();
			this.cHeaderCount = new System.Windows.Forms.ColumnHeader();
			this.chBoxViewProgressU = new System.Windows.Forms.CheckBox();
			this.pUAOptions = new System.Windows.Forms.Panel();
			this.cboxUADelFB2Files = new System.Windows.Forms.CheckBox();
			this.pUAType = new System.Windows.Forms.Panel();
			this.cboxUAExistArchive = new System.Windows.Forms.ComboBox();
			this.lblUAExistArchive = new System.Windows.Forms.Label();
			this.pUAToAnotherDir = new System.Windows.Forms.Panel();
			this.label2 = new System.Windows.Forms.Label();
			this.btnUAToAnotherDir = new System.Windows.Forms.Button();
			this.tboxUAToAnotherDir = new System.Windows.Forms.TextBox();
			this.pUAScanDir = new System.Windows.Forms.Panel();
			this.cboxUAToSomeDir = new System.Windows.Forms.CheckBox();
			this.btnUAOpenDir = new System.Windows.Forms.Button();
			this.cboxScanSubDirToUnArchive = new System.Windows.Forms.CheckBox();
			this.tboxUASourceDir = new System.Windows.Forms.TextBox();
			this.lblUAScanDir = new System.Windows.Forms.Label();
			this.tsUnArchiver = new System.Windows.Forms.ToolStrip();
			this.tsbtnUnArchive = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnUnArchiveStop = new System.Windows.Forms.ToolStripButton();
			this.imgl16 = new System.Windows.Forms.ImageList(this.components);
			this.fbdDir = new System.Windows.Forms.FolderBrowserDialog();
			this.tsArchiver.SuspendLayout();
			this.ssProgress.SuspendLayout();
			this.pScanDir.SuspendLayout();
			this.pCentral.SuspendLayout();
			this.tcArchiver.SuspendLayout();
			this.tpArchive.SuspendLayout();
			this.gboxCount.SuspendLayout();
			this.pOptions.SuspendLayout();
			this.pType.SuspendLayout();
			this.pToAnotherDir.SuspendLayout();
			this.tpUnArchive.SuspendLayout();
			this.gboxUACount.SuspendLayout();
			this.panel2.SuspendLayout();
			this.pUAOptions.SuspendLayout();
			this.pUAType.SuspendLayout();
			this.pUAToAnotherDir.SuspendLayout();
			this.pUAScanDir.SuspendLayout();
			this.tsUnArchiver.SuspendLayout();
			this.SuspendLayout();
			// 
			// tsArchiver
			// 
			this.tsArchiver.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tsArchiver.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.tsbtnArchive,
			this.toolStripSeparator1,
			this.tsbtnArchiveStop});
			this.tsArchiver.Location = new System.Drawing.Point(4, 4);
			this.tsArchiver.Name = "tsArchiver";
			this.tsArchiver.Size = new System.Drawing.Size(1081, 31);
			this.tsArchiver.TabIndex = 2;
			// 
			// tsbtnArchive
			// 
			this.tsbtnArchive.AutoToolTip = false;
			this.tsbtnArchive.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnArchive.Image")));
			this.tsbtnArchive.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnArchive.Name = "tsbtnArchive";
			this.tsbtnArchive.Size = new System.Drawing.Size(109, 28);
			this.tsbtnArchive.Text = "Упаковать";
			this.tsbtnArchive.Click += new System.EventHandler(this.TsbtnArchiveClick);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 31);
			// 
			// tsbtnArchiveStop
			// 
			this.tsbtnArchiveStop.AutoToolTip = false;
			this.tsbtnArchiveStop.Enabled = false;
			this.tsbtnArchiveStop.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnArchiveStop.Image")));
			this.tsbtnArchiveStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnArchiveStop.Name = "tsbtnArchiveStop";
			this.tsbtnArchiveStop.Size = new System.Drawing.Size(118, 28);
			this.tsbtnArchiveStop.Text = "Остановить";
			this.tsbtnArchiveStop.Click += new System.EventHandler(this.TsbtnArchiveStopClick);
			// 
			// ssProgress
			// 
			this.ssProgress.ImageScalingSize = new System.Drawing.Size(20, 20);
			this.ssProgress.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.tsslblProgress,
			this.tsProgressBar});
			this.ssProgress.Location = new System.Drawing.Point(0, 650);
			this.ssProgress.Name = "ssProgress";
			this.ssProgress.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
			this.ssProgress.Size = new System.Drawing.Size(1097, 26);
			this.ssProgress.TabIndex = 18;
			this.ssProgress.Text = "statusStrip1";
			// 
			// tsslblProgress
			// 
			this.tsslblProgress.Name = "tsslblProgress";
			this.tsslblProgress.Size = new System.Drawing.Size(60, 21);
			this.tsslblProgress.Text = "Готово.";
			// 
			// tsProgressBar
			// 
			this.tsProgressBar.Name = "tsProgressBar";
			this.tsProgressBar.Size = new System.Drawing.Size(933, 20);
			// 
			// pScanDir
			// 
			this.pScanDir.AutoSize = true;
			this.pScanDir.Controls.Add(this.btnOpenDir);
			this.pScanDir.Controls.Add(this.cboxToSomeDir);
			this.pScanDir.Controls.Add(this.cboxScanSubDirToArchive);
			this.pScanDir.Controls.Add(this.tboxSourceDir);
			this.pScanDir.Controls.Add(this.lblDir);
			this.pScanDir.Dock = System.Windows.Forms.DockStyle.Top;
			this.pScanDir.Location = new System.Drawing.Point(4, 35);
			this.pScanDir.Margin = new System.Windows.Forms.Padding(0);
			this.pScanDir.Name = "pScanDir";
			this.pScanDir.Size = new System.Drawing.Size(1081, 94);
			this.pScanDir.TabIndex = 22;
			// 
			// btnOpenDir
			// 
			this.btnOpenDir.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenDir.Image")));
			this.btnOpenDir.Location = new System.Drawing.Point(220, 4);
			this.btnOpenDir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnOpenDir.Name = "btnOpenDir";
			this.btnOpenDir.Size = new System.Drawing.Size(49, 33);
			this.btnOpenDir.TabIndex = 10;
			this.btnOpenDir.UseVisualStyleBackColor = true;
			this.btnOpenDir.Click += new System.EventHandler(this.BtnOpenDirClick);
			// 
			// cboxToSomeDir
			// 
			this.cboxToSomeDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.cboxToSomeDir.ForeColor = System.Drawing.Color.Navy;
			this.cboxToSomeDir.Location = new System.Drawing.Point(277, 60);
			this.cboxToSomeDir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.cboxToSomeDir.Name = "cboxToSomeDir";
			this.cboxToSomeDir.Size = new System.Drawing.Size(796, 30);
			this.cboxToSomeDir.TabIndex = 9;
			this.cboxToSomeDir.Text = "Поместить zip-архив в ту же папку, где находится исходный fb2-файл";
			this.cboxToSomeDir.UseVisualStyleBackColor = true;
			this.cboxToSomeDir.CheckedChanged += new System.EventHandler(this.CboxToAnotherDirCheckedChanged);
			// 
			// cboxScanSubDirToArchive
			// 
			this.cboxScanSubDirToArchive.Checked = true;
			this.cboxScanSubDirToArchive.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cboxScanSubDirToArchive.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.cboxScanSubDirToArchive.ForeColor = System.Drawing.Color.Navy;
			this.cboxScanSubDirToArchive.Location = new System.Drawing.Point(277, 36);
			this.cboxScanSubDirToArchive.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.cboxScanSubDirToArchive.Name = "cboxScanSubDirToArchive";
			this.cboxScanSubDirToArchive.Size = new System.Drawing.Size(796, 30);
			this.cboxScanSubDirToArchive.TabIndex = 6;
			this.cboxScanSubDirToArchive.Text = "Сканировать и подпапки";
			this.cboxScanSubDirToArchive.UseVisualStyleBackColor = true;
			this.cboxScanSubDirToArchive.CheckStateChanged += new System.EventHandler(this.CboxScanSubDirToArchiveCheckStateChanged);
			// 
			// tboxSourceDir
			// 
			this.tboxSourceDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxSourceDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tboxSourceDir.Location = new System.Drawing.Point(277, 6);
			this.tboxSourceDir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tboxSourceDir.Name = "tboxSourceDir";
			this.tboxSourceDir.Size = new System.Drawing.Size(797, 23);
			this.tboxSourceDir.TabIndex = 4;
			this.tboxSourceDir.TextChanged += new System.EventHandler(this.TboxSourceDirTextChanged);
			// 
			// lblDir
			// 
			this.lblDir.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.lblDir.Location = new System.Drawing.Point(3, 10);
			this.lblDir.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDir.Name = "lblDir";
			this.lblDir.Size = new System.Drawing.Size(216, 23);
			this.lblDir.TabIndex = 3;
			this.lblDir.Text = "Папка для сканирования:";
			// 
			// pCentral
			// 
			this.pCentral.Controls.Add(this.tcArchiver);
			this.pCentral.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pCentral.Location = new System.Drawing.Point(0, 0);
			this.pCentral.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.pCentral.Name = "pCentral";
			this.pCentral.Size = new System.Drawing.Size(1097, 650);
			this.pCentral.TabIndex = 23;
			// 
			// tcArchiver
			// 
			this.tcArchiver.Controls.Add(this.tpArchive);
			this.tcArchiver.Controls.Add(this.tpUnArchive);
			this.tcArchiver.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcArchiver.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.tcArchiver.ImageList = this.imgl16;
			this.tcArchiver.Location = new System.Drawing.Point(0, 0);
			this.tcArchiver.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tcArchiver.Name = "tcArchiver";
			this.tcArchiver.SelectedIndex = 0;
			this.tcArchiver.Size = new System.Drawing.Size(1097, 650);
			this.tcArchiver.TabIndex = 0;
			// 
			// tpArchive
			// 
			this.tpArchive.Controls.Add(this.gboxCount);
			this.tpArchive.Controls.Add(this.pOptions);
			this.tpArchive.Controls.Add(this.pScanDir);
			this.tpArchive.Controls.Add(this.tsArchiver);
			this.tpArchive.ImageIndex = 0;
			this.tpArchive.Location = new System.Drawing.Point(4, 25);
			this.tpArchive.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tpArchive.Name = "tpArchive";
			this.tpArchive.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tpArchive.Size = new System.Drawing.Size(1089, 621);
			this.tpArchive.TabIndex = 0;
			this.tpArchive.Text = " Упаковать (Zip)";
			this.tpArchive.UseVisualStyleBackColor = true;
			// 
			// gboxCount
			// 
			this.gboxCount.Controls.Add(this.lvGeneralCount);
			this.gboxCount.Controls.Add(this.chBoxViewProgressA);
			this.gboxCount.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gboxCount.Location = new System.Drawing.Point(4, 229);
			this.gboxCount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.gboxCount.Name = "gboxCount";
			this.gboxCount.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.gboxCount.Size = new System.Drawing.Size(1081, 388);
			this.gboxCount.TabIndex = 27;
			this.gboxCount.TabStop = false;
			this.gboxCount.Text = " Ход работы ";
			// 
			// lvGeneralCount
			// 
			this.lvGeneralCount.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader1,
			this.columnHeader2});
			this.lvGeneralCount.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvGeneralCount.FullRowSelect = true;
			this.lvGeneralCount.GridLines = true;
			this.lvGeneralCount.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
			listViewItem1,
			listViewItem2,
			listViewItem3});
			this.lvGeneralCount.Location = new System.Drawing.Point(4, 51);
			this.lvGeneralCount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.lvGeneralCount.Name = "lvGeneralCount";
			this.lvGeneralCount.Size = new System.Drawing.Size(1073, 333);
			this.lvGeneralCount.TabIndex = 25;
			this.lvGeneralCount.UseCompatibleStateImageBehavior = false;
			this.lvGeneralCount.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Папки и файлы";
			this.columnHeader1.Width = 200;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Количество";
			this.columnHeader2.Width = 80;
			// 
			// chBoxViewProgressA
			// 
			this.chBoxViewProgressA.Dock = System.Windows.Forms.DockStyle.Top;
			this.chBoxViewProgressA.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxViewProgressA.Location = new System.Drawing.Point(4, 21);
			this.chBoxViewProgressA.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.chBoxViewProgressA.Name = "chBoxViewProgressA";
			this.chBoxViewProgressA.Size = new System.Drawing.Size(1073, 30);
			this.chBoxViewProgressA.TabIndex = 24;
			this.chBoxViewProgressA.Text = "Отображать изменение хода работы";
			this.chBoxViewProgressA.UseVisualStyleBackColor = true;
			this.chBoxViewProgressA.CheckStateChanged += new System.EventHandler(this.ChBoxViewProgressACheckStateChanged);
			// 
			// pOptions
			// 
			this.pOptions.AutoSize = true;
			this.pOptions.Controls.Add(this.cboxDelFB2Files);
			this.pOptions.Controls.Add(this.pType);
			this.pOptions.Controls.Add(this.pToAnotherDir);
			this.pOptions.Dock = System.Windows.Forms.DockStyle.Top;
			this.pOptions.Location = new System.Drawing.Point(4, 129);
			this.pOptions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.pOptions.Name = "pOptions";
			this.pOptions.Size = new System.Drawing.Size(1081, 100);
			this.pOptions.TabIndex = 26;
			// 
			// cboxDelFB2Files
			// 
			this.cboxDelFB2Files.Dock = System.Windows.Forms.DockStyle.Top;
			this.cboxDelFB2Files.Location = new System.Drawing.Point(0, 70);
			this.cboxDelFB2Files.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.cboxDelFB2Files.Name = "cboxDelFB2Files";
			this.cboxDelFB2Files.Size = new System.Drawing.Size(1081, 30);
			this.cboxDelFB2Files.TabIndex = 12;
			this.cboxDelFB2Files.Text = " Удалить fb2-файлы после упаковки";
			this.cboxDelFB2Files.UseVisualStyleBackColor = true;
			this.cboxDelFB2Files.CheckStateChanged += new System.EventHandler(this.CboxDelFB2FilesCheckStateChanged);
			// 
			// pType
			// 
			this.pType.AutoSize = true;
			this.pType.Controls.Add(this.cboxExistArchive);
			this.pType.Controls.Add(this.lblExistArchive);
			this.pType.Dock = System.Windows.Forms.DockStyle.Top;
			this.pType.Location = new System.Drawing.Point(0, 38);
			this.pType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.pType.Name = "pType";
			this.pType.Size = new System.Drawing.Size(1081, 32);
			this.pType.TabIndex = 11;
			// 
			// cboxExistArchive
			// 
			this.cboxExistArchive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxExistArchive.FormattingEnabled = true;
			this.cboxExistArchive.Items.AddRange(new object[] {
			"Заменить существующий fb2-архив создаваемым",
			"Добавить к создаваемому fb2-архиву очередной номер",
			"Добавить к создаваемому fb2-архиву дату и время"});
			this.cboxExistArchive.Location = new System.Drawing.Point(277, 4);
			this.cboxExistArchive.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.cboxExistArchive.Name = "cboxExistArchive";
			this.cboxExistArchive.Size = new System.Drawing.Size(539, 24);
			this.cboxExistArchive.TabIndex = 16;
			this.cboxExistArchive.SelectedIndexChanged += new System.EventHandler(this.CboxExistArchiveSelectedIndexChanged);
			// 
			// lblExistArchive
			// 
			this.lblExistArchive.AutoSize = true;
			this.lblExistArchive.Location = new System.Drawing.Point(4, 7);
			this.lblExistArchive.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblExistArchive.Name = "lblExistArchive";
			this.lblExistArchive.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblExistArchive.Size = new System.Drawing.Size(195, 17);
			this.lblExistArchive.TabIndex = 15;
			this.lblExistArchive.Text = "Одинаковые fb2-архивы:";
			this.lblExistArchive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pToAnotherDir
			// 
			this.pToAnotherDir.AutoSize = true;
			this.pToAnotherDir.Controls.Add(this.label1);
			this.pToAnotherDir.Controls.Add(this.tboxToAnotherDir);
			this.pToAnotherDir.Controls.Add(this.btnToAnotherDir);
			this.pToAnotherDir.Dock = System.Windows.Forms.DockStyle.Top;
			this.pToAnotherDir.Location = new System.Drawing.Point(0, 0);
			this.pToAnotherDir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.pToAnotherDir.Name = "pToAnotherDir";
			this.pToAnotherDir.Size = new System.Drawing.Size(1081, 38);
			this.pToAnotherDir.TabIndex = 5;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.label1.Location = new System.Drawing.Point(4, 10);
			this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(216, 23);
			this.label1.TabIndex = 8;
			this.label1.Text = "Где размещать архивы";
			// 
			// tboxToAnotherDir
			// 
			this.tboxToAnotherDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxToAnotherDir.Location = new System.Drawing.Point(277, 7);
			this.tboxToAnotherDir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tboxToAnotherDir.Name = "tboxToAnotherDir";
			this.tboxToAnotherDir.Size = new System.Drawing.Size(797, 24);
			this.tboxToAnotherDir.TabIndex = 6;
			this.tboxToAnotherDir.TextChanged += new System.EventHandler(this.TboxToAnotherDirTextChanged);
			// 
			// btnToAnotherDir
			// 
			this.btnToAnotherDir.Image = ((System.Drawing.Image)(resources.GetObject("btnToAnotherDir.Image")));
			this.btnToAnotherDir.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnToAnotherDir.Location = new System.Drawing.Point(220, 4);
			this.btnToAnotherDir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnToAnotherDir.Name = "btnToAnotherDir";
			this.btnToAnotherDir.Size = new System.Drawing.Size(49, 30);
			this.btnToAnotherDir.TabIndex = 7;
			this.btnToAnotherDir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnToAnotherDir.UseVisualStyleBackColor = true;
			this.btnToAnotherDir.Click += new System.EventHandler(this.BtnToAnotherDirClick);
			// 
			// tpUnArchive
			// 
			this.tpUnArchive.Controls.Add(this.gboxUACount);
			this.tpUnArchive.Controls.Add(this.pUAOptions);
			this.tpUnArchive.Controls.Add(this.pUAScanDir);
			this.tpUnArchive.Controls.Add(this.tsUnArchiver);
			this.tpUnArchive.ImageIndex = 1;
			this.tpUnArchive.Location = new System.Drawing.Point(4, 25);
			this.tpUnArchive.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tpUnArchive.Name = "tpUnArchive";
			this.tpUnArchive.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tpUnArchive.Size = new System.Drawing.Size(1089, 621);
			this.tpUnArchive.TabIndex = 1;
			this.tpUnArchive.Text = " Распаковать (UnZip)";
			this.tpUnArchive.UseVisualStyleBackColor = true;
			// 
			// gboxUACount
			// 
			this.gboxUACount.Controls.Add(this.panel2);
			this.gboxUACount.Controls.Add(this.chBoxViewProgressU);
			this.gboxUACount.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gboxUACount.Location = new System.Drawing.Point(4, 229);
			this.gboxUACount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.gboxUACount.Name = "gboxUACount";
			this.gboxUACount.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.gboxUACount.Size = new System.Drawing.Size(1081, 388);
			this.gboxUACount.TabIndex = 29;
			this.gboxUACount.TabStop = false;
			this.gboxUACount.Text = " Ход работы ";
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.lvUAGeneralCount);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.Location = new System.Drawing.Point(4, 51);
			this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(1073, 333);
			this.panel2.TabIndex = 7;
			// 
			// lvUAGeneralCount
			// 
			this.lvUAGeneralCount.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.cHeaderDirsFiles,
			this.cHeaderCount});
			this.lvUAGeneralCount.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvUAGeneralCount.FullRowSelect = true;
			this.lvUAGeneralCount.GridLines = true;
			this.lvUAGeneralCount.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
			listViewItem4,
			listViewItem5,
			listViewItem6,
			listViewItem7});
			this.lvUAGeneralCount.Location = new System.Drawing.Point(0, 0);
			this.lvUAGeneralCount.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.lvUAGeneralCount.Name = "lvUAGeneralCount";
			this.lvUAGeneralCount.Size = new System.Drawing.Size(1073, 333);
			this.lvUAGeneralCount.TabIndex = 2;
			this.lvUAGeneralCount.UseCompatibleStateImageBehavior = false;
			this.lvUAGeneralCount.View = System.Windows.Forms.View.Details;
			// 
			// cHeaderDirsFiles
			// 
			this.cHeaderDirsFiles.Text = "Папки и файлы";
			this.cHeaderDirsFiles.Width = 250;
			// 
			// cHeaderCount
			// 
			this.cHeaderCount.Text = "Количество";
			this.cHeaderCount.Width = 100;
			// 
			// chBoxViewProgressU
			// 
			this.chBoxViewProgressU.Dock = System.Windows.Forms.DockStyle.Top;
			this.chBoxViewProgressU.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxViewProgressU.Location = new System.Drawing.Point(4, 21);
			this.chBoxViewProgressU.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.chBoxViewProgressU.Name = "chBoxViewProgressU";
			this.chBoxViewProgressU.Size = new System.Drawing.Size(1073, 30);
			this.chBoxViewProgressU.TabIndex = 25;
			this.chBoxViewProgressU.Text = "Отображать изменение хода работы";
			this.chBoxViewProgressU.UseVisualStyleBackColor = true;
			this.chBoxViewProgressU.CheckStateChanged += new System.EventHandler(this.ChBoxViewProgressUCheckStateChanged);
			// 
			// pUAOptions
			// 
			this.pUAOptions.AutoSize = true;
			this.pUAOptions.Controls.Add(this.cboxUADelFB2Files);
			this.pUAOptions.Controls.Add(this.pUAType);
			this.pUAOptions.Controls.Add(this.pUAToAnotherDir);
			this.pUAOptions.Dock = System.Windows.Forms.DockStyle.Top;
			this.pUAOptions.Location = new System.Drawing.Point(4, 129);
			this.pUAOptions.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.pUAOptions.Name = "pUAOptions";
			this.pUAOptions.Size = new System.Drawing.Size(1081, 100);
			this.pUAOptions.TabIndex = 28;
			// 
			// cboxUADelFB2Files
			// 
			this.cboxUADelFB2Files.Dock = System.Windows.Forms.DockStyle.Top;
			this.cboxUADelFB2Files.Location = new System.Drawing.Point(0, 70);
			this.cboxUADelFB2Files.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.cboxUADelFB2Files.Name = "cboxUADelFB2Files";
			this.cboxUADelFB2Files.Size = new System.Drawing.Size(1081, 30);
			this.cboxUADelFB2Files.TabIndex = 30;
			this.cboxUADelFB2Files.Text = " Удалить архивы после распаковки";
			this.cboxUADelFB2Files.UseVisualStyleBackColor = true;
			this.cboxUADelFB2Files.CheckStateChanged += new System.EventHandler(this.CboxUADelFB2FilesCheckStateChanged);
			// 
			// pUAType
			// 
			this.pUAType.AutoSize = true;
			this.pUAType.Controls.Add(this.cboxUAExistArchive);
			this.pUAType.Controls.Add(this.lblUAExistArchive);
			this.pUAType.Dock = System.Windows.Forms.DockStyle.Top;
			this.pUAType.Location = new System.Drawing.Point(0, 38);
			this.pUAType.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.pUAType.Name = "pUAType";
			this.pUAType.Size = new System.Drawing.Size(1081, 32);
			this.pUAType.TabIndex = 27;
			// 
			// cboxUAExistArchive
			// 
			this.cboxUAExistArchive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxUAExistArchive.FormattingEnabled = true;
			this.cboxUAExistArchive.Items.AddRange(new object[] {
			"Заменить существующий fb2-файл создаваемым",
			"Добавить к создаваемому fb2-файлу очередной номер",
			"Добавить к создаваемому fb2-файлу дату и время"});
			this.cboxUAExistArchive.Location = new System.Drawing.Point(277, 4);
			this.cboxUAExistArchive.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.cboxUAExistArchive.Name = "cboxUAExistArchive";
			this.cboxUAExistArchive.Size = new System.Drawing.Size(539, 24);
			this.cboxUAExistArchive.TabIndex = 16;
			this.cboxUAExistArchive.SelectedIndexChanged += new System.EventHandler(this.CboxUAExistArchiveSelectedIndexChanged);
			// 
			// lblUAExistArchive
			// 
			this.lblUAExistArchive.AutoSize = true;
			this.lblUAExistArchive.Location = new System.Drawing.Point(4, 7);
			this.lblUAExistArchive.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblUAExistArchive.Name = "lblUAExistArchive";
			this.lblUAExistArchive.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.lblUAExistArchive.Size = new System.Drawing.Size(192, 17);
			this.lblUAExistArchive.TabIndex = 15;
			this.lblUAExistArchive.Text = "Одинаковые fb2-файлы:";
			this.lblUAExistArchive.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// pUAToAnotherDir
			// 
			this.pUAToAnotherDir.AutoSize = true;
			this.pUAToAnotherDir.Controls.Add(this.label2);
			this.pUAToAnotherDir.Controls.Add(this.btnUAToAnotherDir);
			this.pUAToAnotherDir.Controls.Add(this.tboxUAToAnotherDir);
			this.pUAToAnotherDir.Dock = System.Windows.Forms.DockStyle.Top;
			this.pUAToAnotherDir.Location = new System.Drawing.Point(0, 0);
			this.pUAToAnotherDir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.pUAToAnotherDir.Name = "pUAToAnotherDir";
			this.pUAToAnotherDir.Size = new System.Drawing.Size(1081, 38);
			this.pUAToAnotherDir.TabIndex = 9;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.label2.Location = new System.Drawing.Point(4, 10);
			this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(216, 23);
			this.label2.TabIndex = 8;
			this.label2.Text = "Где размещать fb2:";
			// 
			// btnUAToAnotherDir
			// 
			this.btnUAToAnotherDir.Image = ((System.Drawing.Image)(resources.GetObject("btnUAToAnotherDir.Image")));
			this.btnUAToAnotherDir.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnUAToAnotherDir.Location = new System.Drawing.Point(220, 4);
			this.btnUAToAnotherDir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnUAToAnotherDir.Name = "btnUAToAnotherDir";
			this.btnUAToAnotherDir.Size = new System.Drawing.Size(49, 30);
			this.btnUAToAnotherDir.TabIndex = 7;
			this.btnUAToAnotherDir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnUAToAnotherDir.UseVisualStyleBackColor = true;
			this.btnUAToAnotherDir.Click += new System.EventHandler(this.BtnUAToAnotherDirClick);
			// 
			// tboxUAToAnotherDir
			// 
			this.tboxUAToAnotherDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxUAToAnotherDir.Location = new System.Drawing.Point(277, 7);
			this.tboxUAToAnotherDir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tboxUAToAnotherDir.Name = "tboxUAToAnotherDir";
			this.tboxUAToAnotherDir.Size = new System.Drawing.Size(797, 24);
			this.tboxUAToAnotherDir.TabIndex = 6;
			this.tboxUAToAnotherDir.TextChanged += new System.EventHandler(this.TboxUAToAnotherDirTextChanged);
			// 
			// pUAScanDir
			// 
			this.pUAScanDir.AutoSize = true;
			this.pUAScanDir.Controls.Add(this.cboxUAToSomeDir);
			this.pUAScanDir.Controls.Add(this.btnUAOpenDir);
			this.pUAScanDir.Controls.Add(this.cboxScanSubDirToUnArchive);
			this.pUAScanDir.Controls.Add(this.tboxUASourceDir);
			this.pUAScanDir.Controls.Add(this.lblUAScanDir);
			this.pUAScanDir.Dock = System.Windows.Forms.DockStyle.Top;
			this.pUAScanDir.Location = new System.Drawing.Point(4, 35);
			this.pUAScanDir.Margin = new System.Windows.Forms.Padding(0);
			this.pUAScanDir.Name = "pUAScanDir";
			this.pUAScanDir.Size = new System.Drawing.Size(1081, 94);
			this.pUAScanDir.TabIndex = 23;
			// 
			// cboxUAToSomeDir
			// 
			this.cboxUAToSomeDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.cboxUAToSomeDir.ForeColor = System.Drawing.Color.Navy;
			this.cboxUAToSomeDir.Location = new System.Drawing.Point(277, 60);
			this.cboxUAToSomeDir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.cboxUAToSomeDir.Name = "cboxUAToSomeDir";
			this.cboxUAToSomeDir.Size = new System.Drawing.Size(796, 30);
			this.cboxUAToSomeDir.TabIndex = 12;
			this.cboxUAToSomeDir.Text = "Поместить fb2-файл в ту же папку, где находится исходный архив";
			this.cboxUAToSomeDir.UseVisualStyleBackColor = true;
			this.cboxUAToSomeDir.CheckedChanged += new System.EventHandler(this.CboxUAToSomeDirCheckedChanged);
			// 
			// btnUAOpenDir
			// 
			this.btnUAOpenDir.Image = ((System.Drawing.Image)(resources.GetObject("btnUAOpenDir.Image")));
			this.btnUAOpenDir.Location = new System.Drawing.Point(220, 4);
			this.btnUAOpenDir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.btnUAOpenDir.Name = "btnUAOpenDir";
			this.btnUAOpenDir.Size = new System.Drawing.Size(49, 33);
			this.btnUAOpenDir.TabIndex = 11;
			this.btnUAOpenDir.UseVisualStyleBackColor = true;
			this.btnUAOpenDir.Click += new System.EventHandler(this.BtnUAOpenDirClick);
			// 
			// cboxScanSubDirToUnArchive
			// 
			this.cboxScanSubDirToUnArchive.Checked = true;
			this.cboxScanSubDirToUnArchive.CheckState = System.Windows.Forms.CheckState.Checked;
			this.cboxScanSubDirToUnArchive.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.cboxScanSubDirToUnArchive.ForeColor = System.Drawing.Color.Navy;
			this.cboxScanSubDirToUnArchive.Location = new System.Drawing.Point(277, 36);
			this.cboxScanSubDirToUnArchive.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.cboxScanSubDirToUnArchive.Name = "cboxScanSubDirToUnArchive";
			this.cboxScanSubDirToUnArchive.Size = new System.Drawing.Size(797, 30);
			this.cboxScanSubDirToUnArchive.TabIndex = 7;
			this.cboxScanSubDirToUnArchive.Text = "Сканировать и подпапки";
			this.cboxScanSubDirToUnArchive.UseVisualStyleBackColor = true;
			this.cboxScanSubDirToUnArchive.CheckStateChanged += new System.EventHandler(this.CboxScanSubDirToUnArchiveCheckStateChanged);
			// 
			// tboxUASourceDir
			// 
			this.tboxUASourceDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxUASourceDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tboxUASourceDir.Location = new System.Drawing.Point(277, 6);
			this.tboxUASourceDir.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.tboxUASourceDir.Name = "tboxUASourceDir";
			this.tboxUASourceDir.Size = new System.Drawing.Size(797, 23);
			this.tboxUASourceDir.TabIndex = 4;
			this.tboxUASourceDir.TextChanged += new System.EventHandler(this.TboxUASourceDirTextChanged);
			// 
			// lblUAScanDir
			// 
			this.lblUAScanDir.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.lblUAScanDir.Location = new System.Drawing.Point(3, 10);
			this.lblUAScanDir.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblUAScanDir.Name = "lblUAScanDir";
			this.lblUAScanDir.Size = new System.Drawing.Size(216, 23);
			this.lblUAScanDir.TabIndex = 3;
			this.lblUAScanDir.Text = "Папка для сканирования:";
			// 
			// tsUnArchiver
			// 
			this.tsUnArchiver.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tsUnArchiver.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.tsbtnUnArchive,
			this.toolStripSeparator2,
			this.tsbtnUnArchiveStop});
			this.tsUnArchiver.Location = new System.Drawing.Point(4, 4);
			this.tsUnArchiver.Name = "tsUnArchiver";
			this.tsUnArchiver.Size = new System.Drawing.Size(1081, 31);
			this.tsUnArchiver.TabIndex = 3;
			// 
			// tsbtnUnArchive
			// 
			this.tsbtnUnArchive.AutoToolTip = false;
			this.tsbtnUnArchive.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnUnArchive.Image")));
			this.tsbtnUnArchive.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnUnArchive.Name = "tsbtnUnArchive";
			this.tsbtnUnArchive.Size = new System.Drawing.Size(123, 28);
			this.tsbtnUnArchive.Text = "Распаковать";
			this.tsbtnUnArchive.Click += new System.EventHandler(this.TsbtnUnArchiveClick);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(6, 31);
			// 
			// tsbtnUnArchiveStop
			// 
			this.tsbtnUnArchiveStop.AutoToolTip = false;
			this.tsbtnUnArchiveStop.Enabled = false;
			this.tsbtnUnArchiveStop.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnUnArchiveStop.Image")));
			this.tsbtnUnArchiveStop.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnUnArchiveStop.Name = "tsbtnUnArchiveStop";
			this.tsbtnUnArchiveStop.Size = new System.Drawing.Size(118, 28);
			this.tsbtnUnArchiveStop.Text = "Остановить";
			this.tsbtnUnArchiveStop.Click += new System.EventHandler(this.TsbtnUnArchiveStopClick);
			// 
			// imgl16
			// 
			this.imgl16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgl16.ImageStream")));
			this.imgl16.TransparentColor = System.Drawing.Color.Transparent;
			this.imgl16.Images.SetKeyName(0, "Archive1.png");
			this.imgl16.Images.SetKeyName(1, "UnArchive1.png");
			// 
			// SFBTpArchiveManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pCentral);
			this.Controls.Add(this.ssProgress);
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "SFBTpArchiveManager";
			this.Size = new System.Drawing.Size(1097, 676);
			this.tsArchiver.ResumeLayout(false);
			this.tsArchiver.PerformLayout();
			this.ssProgress.ResumeLayout(false);
			this.ssProgress.PerformLayout();
			this.pScanDir.ResumeLayout(false);
			this.pScanDir.PerformLayout();
			this.pCentral.ResumeLayout(false);
			this.tcArchiver.ResumeLayout(false);
			this.tpArchive.ResumeLayout(false);
			this.tpArchive.PerformLayout();
			this.gboxCount.ResumeLayout(false);
			this.pOptions.ResumeLayout(false);
			this.pOptions.PerformLayout();
			this.pType.ResumeLayout(false);
			this.pType.PerformLayout();
			this.pToAnotherDir.ResumeLayout(false);
			this.pToAnotherDir.PerformLayout();
			this.tpUnArchive.ResumeLayout(false);
			this.tpUnArchive.PerformLayout();
			this.gboxUACount.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.pUAOptions.ResumeLayout(false);
			this.pUAOptions.PerformLayout();
			this.pUAType.ResumeLayout(false);
			this.pUAType.PerformLayout();
			this.pUAToAnotherDir.ResumeLayout(false);
			this.pUAToAnotherDir.PerformLayout();
			this.pUAScanDir.ResumeLayout(false);
			this.pUAScanDir.PerformLayout();
			this.tsUnArchiver.ResumeLayout(false);
			this.tsUnArchiver.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		private System.Windows.Forms.CheckBox cboxUAToSomeDir;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.CheckBox cboxToSomeDir;
		private System.Windows.Forms.Button btnUAOpenDir;
		private System.Windows.Forms.Button btnOpenDir;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.CheckBox chBoxViewProgressU;
		private System.Windows.Forms.CheckBox chBoxViewProgressA;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.ToolStripButton tsbtnUnArchiveStop;
		private System.Windows.Forms.ToolStripButton tsbtnArchiveStop;
		private System.Windows.Forms.CheckBox cboxScanSubDirToUnArchive;
		private System.Windows.Forms.CheckBox cboxScanSubDirToArchive;
		private System.Windows.Forms.ImageList imgl16;
		private System.Windows.Forms.GroupBox gboxCount;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ListView lvGeneralCount;
		private System.Windows.Forms.ColumnHeader cHeaderCount;
		private System.Windows.Forms.ColumnHeader cHeaderDirsFiles;
		private System.Windows.Forms.ListView lvUAGeneralCount;
		private System.Windows.Forms.GroupBox gboxUACount;
		private System.Windows.Forms.TextBox tboxUAToAnotherDir;
		private System.Windows.Forms.Button btnUAToAnotherDir;
		private System.Windows.Forms.Panel pUAToAnotherDir;
		private System.Windows.Forms.CheckBox cboxUADelFB2Files;
		private System.Windows.Forms.Panel pUAOptions;
		private System.Windows.Forms.Label lblUAExistArchive;
		private System.Windows.Forms.ComboBox cboxUAExistArchive;
		private System.Windows.Forms.Panel pUAType;
		private System.Windows.Forms.Label lblUAScanDir;
		private System.Windows.Forms.TextBox tboxUASourceDir;
		private System.Windows.Forms.Panel pUAScanDir;
		private System.Windows.Forms.ToolStripButton tsbtnUnArchive;
		private System.Windows.Forms.ToolStrip tsUnArchiver;
		private System.Windows.Forms.ToolStrip tsArchiver;
		private System.Windows.Forms.ComboBox cboxExistArchive;
		private System.Windows.Forms.Label lblExistArchive;
		private System.Windows.Forms.FolderBrowserDialog fbdDir;
		private System.Windows.Forms.ToolStripButton tsbtnArchive;
		private System.Windows.Forms.CheckBox cboxDelFB2Files;
		private System.Windows.Forms.TextBox tboxToAnotherDir;
		private System.Windows.Forms.Button btnToAnotherDir;
		private System.Windows.Forms.Panel pToAnotherDir;
		private System.Windows.Forms.Panel pOptions;
		private System.Windows.Forms.Panel pType;
		private System.Windows.Forms.TabPage tpUnArchive;
		private System.Windows.Forms.TabPage tpArchive;
		private System.Windows.Forms.TabControl tcArchiver;
		private System.Windows.Forms.Panel pCentral;
		private System.Windows.Forms.Label lblDir;
		private System.Windows.Forms.TextBox tboxSourceDir;
		private System.Windows.Forms.Panel pScanDir;
		private System.Windows.Forms.ToolStripProgressBar tsProgressBar;
		private System.Windows.Forms.ToolStripStatusLabel tsslblProgress;
		private System.Windows.Forms.StatusStrip ssProgress;
		#endregion
		
		#region Закрытые члены-данные класса
		private readonly string		m_FileSettingsPath	= Settings.Settings.ProgDir + @"\ArchiveManagerSettings.xml";
		private bool				m_isSettingsLoaded	= false; // Только при true все изменения настроек сохраняются в файл.
		private SharpZipLibWorker	m_sharpZipLib		= new SharpZipLibWorker();
		private string				m_TempDir			= Settings.Settings.TempDir;
		// Общие
		private DateTime	m_dtStart;
		private string		m_sMessTitle = string.Empty;
		// Для Упаковки
		private BackgroundWorker m_bwa	= null;
		private int	m_nFB2A				= 0;
		// Для Распаковки
		private BackgroundWorker m_bwu	= null;
		private int	m_nUnpackCount		= 0;
		private int	m_nCountU 			= 0;
		private long m_nFB2U 			= 0;
		private int	m_nZipU 			= 0;
		#endregion
		
		public SFBTpArchiveManager()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			// задание для кнопок ToolStrip стиля и положения текста и картинки
			SetToolButtonsSettings();
			
			InitializeArchiveBackgroundWorker();
			InitializeUnPackBackgroundWorker();
			InitA();	// инициализация контролов (Упаковка)
			InitUA();	// инициализация контролов (Распаковка

			cboxExistArchive.SelectedIndex		= 1; // добавление к создаваемому fb2-архиву очередного номера
			cboxUAExistArchive.SelectedIndex	= 1; // добавление к создаваемому fb2-файлу очередного номера
			// чтение настроек из xml-файла
			readSettingsFromXML();
			m_isSettingsLoaded = true; // все настройки запгружены
		}
		
		#region Открытые методы класса
		// задание для кнопок ToolStrip стиля и положения текста и картинки
		public void SetToolButtonsSettings() {
			Settings.ArchiveManagerSettings.SetToolButtonsSettings( tsArchiver );
			Settings.ArchiveManagerSettings.SetToolButtonsSettings( tsUnArchiver );
		}
		#endregion
		
		#region Закрытые Общие Вспомогательны методы класса
		// удаляем исходный файл
		private bool DeleteFileIsNeeds( string sFile ) {
			if( File.Exists( sFile ) ) {
				File.Delete( sFile );
				return true;
			}
			return false;
		}
		
		// сохранение настроек в xml-файл
		private void saveSettingsToXml() {
			#region Код
			if( m_isSettingsLoaded ) {
				// защита от "затирания" настроек в файле, когда в некоторые контролы данные еще не загрузились
				XDocument doc = new XDocument(
					new XDeclaration("1.0", "utf-8", "yes"),
					new XElement("Settings",
					             new XElement("Zip",
					                          new XComment("Папка исходных fb2-файлов"),
					                          new XElement("SourceDir", tboxSourceDir.Text.Trim()),
					                          new XComment("Папка для размещения архивов"),
					                          new XElement("TargetDir", tboxToAnotherDir.Text.Trim()),
					                          new XComment("Операции с одинаковыми zip-архивами"),
					                          new XElement("ZipExsistMode",
					                                       new XAttribute("Index", cboxExistArchive.SelectedIndex),
					                                       new XAttribute("Name", cboxExistArchive.Text)
					                                      ),
					                          new XComment("Настройки архивации"),
					                          new XElement("Options",
					                                       new XComment("Сканировать и подпапки"),
					                                       new XElement("ScanSubDirs", cboxScanSubDirToArchive.Checked),
					                                       new XComment("Поместить zip-архив в ту же папку, где находится исходный fb2-файл"),
					                                       new XElement("ToSomeDir", cboxToSomeDir.Checked),
					                                       new XComment("Удалить fb2-файлы после упаковки"),
					                                       new XElement("DeleteFB2Files", cboxDelFB2Files.Checked)
					                                      ),
					                          new XComment("Отображать изменения хода работы"),
					                          new XElement("Progress", chBoxViewProgressA.Checked)
					                         ),
					             new XElement("UnZip",
					                          new XComment("Папка для исходных zip-архивов"),
					                          new XElement("SourceDir", tboxUASourceDir.Text.Trim()),
					                          new XComment("Папка для размещения распакованных fb2-файлов"),
					                          new XElement("TargetDir", tboxUAToAnotherDir.Text.Trim()),
					                          new XComment("Операции с одинаковыми fb2-файлами"),
					                          new XElement("FB2ExsistMode",
					                                       new XAttribute("Index", cboxUAExistArchive.SelectedIndex),
					                                       new XAttribute("Name", cboxUAExistArchive.Text)
					                                      ),
					                          new XComment("Настройки распаковки"),
					                          new XElement("Options",
					                                       new XComment("Сканировать и подпапки"),
					                                       new XElement("ScanSubDirs", cboxScanSubDirToUnArchive.Checked),
					                                       new XComment("Поместить fb2-файл в ту же папку, где находится исходный архив"),
					                                       new XElement("ToSomeDir", cboxUAToSomeDir.Checked),
					                                       new XComment("Удалить zip-архивы после распаковки"),
					                                       new XElement("DeleteZip", cboxUADelFB2Files.Checked)
					                                      ),
					                          new XComment("Отображать изменения хода работы"),
					                          new XElement("Progress", chBoxViewProgressU.Checked)
					                         )
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
				/* Zip */
				if( xmlTree.Element("Zip") != null ) {
					XElement xmlZip = xmlTree.Element("Zip");
					// Папка исходных fb2-файлов
					if( xmlZip.Element("SourceDir") != null )
						tboxSourceDir.Text = xmlZip.Element("SourceDir").Value;
					// Папка для размещения архивов
					if( xmlZip.Element("TargetDir") != null )
						tboxToAnotherDir.Text = xmlZip.Element("TargetDir").Value;
					// Операции с одинаковыми zip-архивами
					if( xmlZip.Element("ZipExsistMode") != null ) {
						if( xmlZip.Element("ZipExsistMode").Attribute("Index") != null )
							cboxExistArchive.SelectedIndex = Convert.ToInt16( xmlZip.Element("ZipExsistMode").Attribute("Index").Value );
					}
					// Настройки архивации:
					if( xmlZip.Element("Options") != null ) {
						XElement xmlOptions = xmlZip.Element("Options");
						// Сканировать и подпапки
						if( xmlOptions.Element("ScanSubDirs") != null )
							cboxScanSubDirToArchive.Checked	= Convert.ToBoolean( xmlOptions.Element("ScanSubDirs").Value );
						// Поместить zip-архив в ту же папку, где находится исходный fb2-файл"
						if( xmlOptions.Element("ToSomeDir") != null )
							cboxToSomeDir.Checked	= Convert.ToBoolean( xmlOptions.Element("ToSomeDir").Value );
						// Удалить fb2-файлы после упаковки"
						if( xmlOptions.Element("DeleteFB2Files") != null )
							cboxDelFB2Files.Checked	= Convert.ToBoolean( xmlOptions.Element("DeleteFB2Files").Value );
					}
					// Отображать изменения хода работы
					if( xmlZip.Element("Progress") != null )
						chBoxViewProgressA.Checked = Convert.ToBoolean( xmlZip.Element("Progress").Value );
				}
				
				/* UnZip */
				if( xmlTree.Element("UnZip") != null ) {
					XElement xmlUnZip = xmlTree.Element("UnZip");
					// Папка исходных zip-файлов
					if( xmlUnZip.Element("SourceDir") != null )
						tboxUASourceDir.Text = xmlUnZip.Element("SourceDir").Value;
					// Папка для размещения архивов
					if( xmlUnZip.Element("TargetDir") != null )
						tboxUAToAnotherDir.Text = xmlUnZip.Element("TargetDir").Value;
					// Операции с одинаковыми fb2-архивами
					if( xmlUnZip.Element("FB2ExsistMode") != null ) {
						if( xmlUnZip.Element("FB2ExsistMode").Attribute("Index") != null )
							cboxUAExistArchive.SelectedIndex = Convert.ToInt16( xmlUnZip.Element("FB2ExsistMode").Attribute("Index").Value );
					}
					// Настройки распаковки:
					if( xmlUnZip.Element("Options") != null ) {
						XElement xmlOptions = xmlUnZip.Element("Options");
						// Сканировать и подпапки
						if( xmlOptions.Element("ScanSubDirs") != null )
							cboxScanSubDirToUnArchive.Checked = Convert.ToBoolean( xmlOptions.Element("ScanSubDirs").Value );
						// Поместить fb2-архив в ту же папку, где находится исходный архив"
						if( xmlOptions.Element("ToSomeDir") != null )
							cboxUAToSomeDir.Checked	= Convert.ToBoolean( xmlOptions.Element("ToSomeDir").Value );
						// Удалить zip-архивы после упаковки"
						if( xmlOptions.Element("DeleteZip") != null )
							cboxUADelFB2Files.Checked	= Convert.ToBoolean( xmlOptions.Element("DeleteZip").Value );
					}
					// Отображать изменения хода работы
					if( xmlUnZip.Element("Progress") != null )
						chBoxViewProgressU.Checked = Convert.ToBoolean( xmlUnZip.Element("Progress").Value );
				}
			}
			#endregion
		}
		#endregion
		
		#region Архивация
		// Инициализация перед использование BackgroundWorker Упаковщика
		private void InitializeArchiveBackgroundWorker() {
			m_bwa = new BackgroundWorker();
			m_bwa.WorkerReportsProgress			= true; // Позволить выводить прогресс процесса
			m_bwa.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
			m_bwa.DoWork 				+= new DoWorkEventHandler( bwa_DoWork );
			m_bwa.ProgressChanged 		+= new ProgressChangedEventHandler( bwa_ProgressChanged );
			m_bwa.RunWorkerCompleted 	+= new RunWorkerCompletedEventHandler( bwa_RunWorkerCompleted );
		}
		
		
		// упаковка файлов в архивы
		private void bwa_DoWork( object sender, DoWorkEventArgs e ) {
			int nAllFiles = 0;
			List<string> lDirList = new List<string>();
			string SourceDir = getSourceDirForZip();
			if( !getScanSubDirsForZip() ) {
				// сканировать только указанную папку
				lDirList.Add( SourceDir );
				lvGeneralCount.Items[0].SubItems[1].Text = "1";
			} else {
				// сканировать и все подпапки
				nAllFiles = filesWorker.DirsParser( m_bwa, e, SourceDir, ref lvGeneralCount, ref lDirList, false );
			}
			
			// отобразим число всех файлов в папке сканирования
			lvGeneralCount.Items[1].SubItems[1].Text = nAllFiles.ToString();

			// проверка остановки процесса
			if( ( m_bwa.CancellationPending == true ) )  {
				e.Cancel = true; // Выставить окончание - по отмене, сработает событие bwa_RunWorkerCompleted
				return;
			}
			
			// проверка, есть ли хоть один файл в папке для сканирования
			if( nAllFiles == 0 ) {
				MessageBox.Show( "Не найдено ни одного файла!\nРабота прекращена.", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				InitA();
				return;
			}

			tsslblProgress.Text = "Упаковка найденных файлов в zip:";
			tsProgressBar.Maximum	= nAllFiles;
			tsProgressBar.Value		= 0;
			m_nFB2A = 0;
			string sFile = string.Empty;
			string TargetDir = getTargetDirForZip();
			int n = 0;
			foreach( string s in lDirList ) {
				DirectoryInfo diFolder = new DirectoryInfo( s );
				foreach( FileInfo fiNextFile in diFolder.GetFiles() ) {
					if( ( m_bwa.CancellationPending == true ) )  {
						e.Cancel = true; // Выставить окончание - по отмене, сработает событие bwa_RunWorkerCompleted
						return;
					}
					sFile = s + "\\" + fiNextFile.Name;
					FileToZip( sFile, SourceDir, TargetDir ); // zip
					m_bwa.ReportProgress( ++n ); // отобразим данные в контролах
				}
			}
			lDirList.Clear();
		}
		
		// Отобразим результат Упаковки
		private void bwa_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
			if( chBoxViewProgressA.Checked )
				ArchiveProgressData();
			tsProgressBar.Value	= e.ProgressPercentage;
		}
		
		// Проверяем это отмена, ошибка, или конец задачи и сообщить
		private void bwa_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {
			ArchiveProgressData(); // Отобразим результат Упаковки
			DateTime dtEnd = DateTime.Now;
			filesWorker.RemoveDir( m_TempDir );
			
			tsslblProgress.Text = Settings.Settings.GetReady();
			SetPackingStartEnabled( true );
			
			string sTime = dtEnd.Subtract( m_dtStart ).ToString() + " (час.:мин.:сек.)";
			string sMessCanceled	= "Упаковка fb2-файлов остановлена!\nЗатрачено времени: "+sTime;
			string sMessError		= string.Empty;
			string sMessDone		= "Упаковка fb2-файлов завершена!\nЗатрачено времени: "+sTime;
			
			if( ( e.Cancelled == true ) )
				MessageBox.Show( sMessCanceled, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			else if( e.Error != null ) {
				sMessError = "Ошибка:\n" + e.Error.Message + "\n" + e.Error.StackTrace + "\nЗатрачено времени: "+sTime;
				MessageBox.Show( sMessError, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			} else {
				MessageBox.Show( sMessDone, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
		}
		
		// создание уникального пути создаваемого архива, помещаемого в ту же папку, где лежит и исхлжный файл
		private string MakeNewArchivePathToSomeDirWithSufix( string FilePath ) {
			string FileExt = Path.GetExtension( FilePath ).ToLower();
			string ArchiveFile = FilePath + ".zip";
			if( File.Exists( ArchiveFile ) ) {
				if( cboxExistArchive.SelectedIndex == 0 )
					File.Delete( ArchiveFile );
				else {
					// или FilePath вместо sArchiveFile
					ArchiveFile = FilePath.Remove( FilePath.Length-4 )
						+ filesWorker.createSufix( ArchiveFile, cboxExistArchive.SelectedIndex )//Sufix
						+ FileExt + ".zip";
				}
			}
			return ArchiveFile;
		}
		
		// создание уникального пути создаваемого архива, помещаемого в другую папку
		private string MakeNewArchivePathToAnotherDirWithSufix( string SourceDir, string TargetDir, string FilePath ) {
			string FileExt = Path.GetExtension( FilePath ).ToLower();
			string NewFilePath = FilePath.Remove( 0, SourceDir.Length );
			string ArchiveFile = TargetDir + NewFilePath + ".zip";
			FileInfo fi = new FileInfo( ArchiveFile );
			if( !fi.Directory.Exists )
				Directory.CreateDirectory( fi.Directory.ToString() );

			if( File.Exists( ArchiveFile ) ) {
				if( cboxExistArchive.SelectedIndex == 0 )
					File.Delete( ArchiveFile );
				else {
					ArchiveFile = TargetDir + NewFilePath.Remove( NewFilePath.Length-4 )
						+ filesWorker.createSufix( ArchiveFile, cboxExistArchive.SelectedIndex )//Sufix
						+ FileExt + ".zip";
				}
			}
			return ArchiveFile;
		}
		
		// упаковка fb2-файлов в .fb2.zip
		private void FileToZip( string FilePath, string SourceDir, string TargetDir ) {
			// упаковываем только fb2-файлы
			if( Path.GetExtension( FilePath ).ToLower() == ".fb2" ) {
				++m_nFB2A;
				string ArchiveFile = string.Empty;
				if( cboxToSomeDir.Checked ) {
					// создаем архив в тот же папке, где и исходный fb2-файл
					ArchiveFile = MakeNewArchivePathToSomeDirWithSufix( FilePath );
				} else {
					// создаем архив в другой папке
					ArchiveFile = MakeNewArchivePathToAnotherDirWithSufix( SourceDir, TargetDir, FilePath );
				}
				m_sharpZipLib.ZipFile( FilePath, ArchiveFile, 9, ICSharpCode.SharpZipLib.Zip.CompressionMethod.Deflated, 4096 );
				
				// удаляем исходный файл, если задана опция
				if ( cboxDelFB2Files.Checked )
					DeleteFileIsNeeds( FilePath );
			}
		}
		
		// доступность контролов при Упаковке
		private void SetPackingStartEnabled( bool bEnabled ) {
			tsbtnArchive.Enabled	= bEnabled;
			pScanDir.Enabled		= bEnabled;
			pType.Enabled			= bEnabled;
			pToAnotherDir.Enabled	= bEnabled;
			cboxToSomeDir.Enabled	= bEnabled;
			tpUnArchive.Enabled		= bEnabled;

			tsbtnArchiveStop.Enabled	= !bEnabled;
			tsProgressBar.Visible		= !bEnabled;
			tcArchiver.Refresh();
			ssProgress.Refresh();
		}
		
		// сканировать ли подпапки для режима упаковки
		private bool getScanSubDirsForZip() {
			return cboxScanSubDirToArchive.Checked;
		}
		
		// получение source папки для режима упаковки
		private string getSourceDirForZip() {
			return filesWorker.WorkingDirPath( tboxSourceDir.Text.Trim() );
		}
		
		// получение target папки для режима упаковки
		private string getTargetDirForZip() {
			if (cboxToSomeDir.Checked)
				return filesWorker.WorkingDirPath( tboxSourceDir.Text.Trim() );
			else
				return filesWorker.WorkingDirPath( tboxToAnotherDir.Text.Trim() );
		}
		
		// Отобразим результат Упаковки
		private void ArchiveProgressData() {
			lvGeneralCount.Items[2].SubItems[1].Text = (m_nFB2A).ToString();
		}
		
		// инициализация контролов и переменных (Упаковка)
		private void InitA() {
			for( int i=0; i!=lvGeneralCount.Items.Count; ++i ) {
				lvGeneralCount.Items[i].SubItems[1].Text = "0";
			}
			tsProgressBar.Value		= 0;
			tsslblProgress.Text		= Settings.Settings.GetReady();
			tsProgressBar.Visible	= false;
		}
		
		// проверки папки для сканирования
		private bool IsSourceDirCorrect( string SourceDir, string sMessTitle ) {
			DirectoryInfo diFolder = new DirectoryInfo( SourceDir );
			if( SourceDir.Length == 0 ) {
				MessageBox.Show( "Выберите папку для сканирования!", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка для сканирования не найдена: " + SourceDir, sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			return true;
		}
		
		#region Обработчики событий
		void CboxToAnotherDirCheckedChanged(object sender, EventArgs e)
		{
			pToAnotherDir.Enabled = !cboxToSomeDir.Checked;
			if( !cboxToSomeDir.Checked )
				tboxToAnotherDir.Focus();
			saveSettingsToXml();
		}
		
		// задание папки для копирования запакованных fb2-файлов
		void BtnToAnotherDirClick(object sender, EventArgs e)
		{
			filesWorker.OpenDirDlg( tboxToAnotherDir, fbdDir, "Укажите папку для размещения упакованных fb2-файлов" );
		}
		
		// задание папки с fb2-файлами для сканирования (Архивация)
		void BtnOpenDirClick(object sender, EventArgs e)
		{
			if( filesWorker.OpenDirDlg( tboxSourceDir, fbdDir, "Укажите папку с fb2-файлами для Упаковки" ) )
				InitA();
		}
		
		// Упаковка fb2-файлов
		void TsbtnArchiveClick(object sender, EventArgs e)
		{
			m_sMessTitle = "SharpFBTools - Упаковка в архивы";

			// проверки папки для сканирования
			if( !IsSourceDirCorrect( getSourceDirForZip(), m_sMessTitle ) )
				return;
			
			// проверки папки-приемника
			if( !IsTargetDirCorrect( getTargetDirForZip(), cboxToSomeDir.Checked, m_sMessTitle ) )
				return;

			// инициализация контролов
			InitA();
			SetPackingStartEnabled( false );
			
			m_dtStart = DateTime.Now;
			tsslblProgress.Text = "Создание списка папок:";
			// Запуск процесса DoWork от Бекграунд Воркера
			if( m_bwa.IsBusy != true ) {
				//если не занят то запустить процесс
				m_bwa.RunWorkerAsync();
			}
		}
		
		void TboxSourceDirTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void TboxToAnotherDirTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void CboxScanSubDirToArchiveCheckStateChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void CboxExistArchiveSelectedIndexChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void CboxDelFB2FilesCheckStateChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void ChBoxViewProgressACheckStateChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		// Остановка выполнения процесса Архивации
		void TsbtnArchiveStopClick(object sender, EventArgs e)
		{
			if( m_bwa.WorkerSupportsCancellation == true )
				m_bwa.CancelAsync();
		}
		
		#endregion
		
		#endregion
		
		#region Распаковка
		// Инициализация перед использование BackgroundWorker Распаковщика
		private void InitializeUnPackBackgroundWorker() {
			m_bwu = new BackgroundWorker();
			m_bwu.WorkerReportsProgress			= true; // Позволить выводить прогресс процесса
			m_bwu.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
			m_bwu.DoWork 				+= new DoWorkEventHandler( bwu_DoWork );
			m_bwu.ProgressChanged 		+= new ProgressChangedEventHandler( bwu_ProgressChanged );
			m_bwu.RunWorkerCompleted 	+= new RunWorkerCompletedEventHandler( bwu_RunWorkerCompleted );
		}
		
		// распаковка архивов в файлы
		private void bwu_DoWork( object sender, DoWorkEventArgs e ) {
			int nAllFiles = 0;
			List<string> lDirList = new List<string>();
			string SourceDir = getSourceDirForUnZip();
			if( !getScanSubDirsForUnZip() ) {
				// сканировать только указанную папку
				lDirList.Add( SourceDir );
				lvUAGeneralCount.Items[0].SubItems[1].Text = "1";
			} else {
				// сканировать и все подпапки
				nAllFiles = filesWorker.DirsParser( m_bwu, e, SourceDir, ref lvUAGeneralCount, ref lDirList, false );
			}
			
			// отобразим число всех файлов в папке сканирования
			lvUAGeneralCount.Items[1].SubItems[1].Text = nAllFiles.ToString();

			// проверка остановки процесса
			if( ( m_bwu.CancellationPending == true ) )  {
				e.Cancel = true; // Выставить окончание - по отмене, сработает событие bwu_RunWorkerCompleted
				return;
			}
			
			// проверка, есть ли хоть один файл в папке для сканирования
			if( nAllFiles == 0 ) {
				MessageBox.Show( "В указанной папке не найдено ни одного файла!", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				InitUA();
				return;
			}
			
			tsslblProgress.Text 	= "Распаковка архивов:";
			tsProgressBar.Maximum 	= nAllFiles;
			tsProgressBar.Value 	= 0;
			m_nCountU = m_nZipU		= 0;
			m_nFB2U = 0;

			filesWorker.RemoveDir( m_TempDir );
			
			BackgroundWorker bw = sender as BackgroundWorker;
			m_nUnpackCount = UnZipToFile( bw, e, SourceDir, lDirList, getTargetDirForUnZip() );
			lDirList.Clear();
		}
		
		// Отобразим результат Распаковки
		private void bwu_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
			if( chBoxViewProgressU.Checked ) UnArchiveProgressData();
			tsProgressBar.Value	= e.ProgressPercentage;
		}
		
		// Проверяем это отмена, ошибка, или конец задачи и сообщить
		private void bwu_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {
			UnArchiveProgressData(); // Отобразим результат Распаковки
			DateTime dtEnd = DateTime.Now;
			filesWorker.RemoveDir( m_TempDir );
			
			tsslblProgress.Text = Settings.Settings.GetReady();
			SetUnPackingStartEnabled( true );
			
			string sTime = dtEnd.Subtract( m_dtStart ).ToString() + " (час.:мин.:сек.)";
			string sMessCanceled	= "Распаковка архивов в fb2-файлы остановлена!\nЗатрачено времени: "+sTime;
			string sMessError		= string.Empty;
			string sMessDone		= string.Empty;
			if( m_nUnpackCount > 0 ) {
				sMessDone	= "Распаковка архивов в fb2-файлы завершена!\nЗатрачено времени: "+sTime;
			} else {
				sMessDone	= "В папке для сканирования не найдено ни одного архива указанного типа!\nРаспаковка не произведена."+sTime;
			}
			
			if( ( e.Cancelled == true ) ) {
				MessageBox.Show( sMessCanceled, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			} else if( e.Error != null ) {
				sMessError = "Ошибка:\n" + e.Error.Message + "\n" + e.Error.StackTrace + "\nЗатрачено времени: "+sTime;
				MessageBox.Show( sMessError, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			} else {
				MessageBox.Show( sMessDone, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
		}
		
		// Распаковать архива
		private int UnZipToFile( BackgroundWorker bw, DoWorkEventArgs e, string FileSourceDir, List<string> lDirList, string TargetDir ) {
			int nCount = 0;
			int n = 0;
			foreach( string dir in lDirList ) {
				DirectoryInfo diFolder = new DirectoryInfo( dir );
				foreach( FileInfo fiNextFile in diFolder.GetFiles() ) {
					if( ( bw.CancellationPending == true ) ) {
						e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
						return nCount;
					}
					string sFile = dir + "\\" + fiNextFile.Name;
					if( Path.GetExtension( sFile.ToLower() ) == ".zip" || Path.GetExtension( sFile.ToLower() ) == ".fbz" ) {
						//string sNewDir = Path.GetDirectoryName( sMoveToDir+"\\"+sFile.Remove( 0, sFileSourceDir.Length ) );
						m_nFB2U += m_sharpZipLib.UnZipFiles(sFile, filesWorker.buildTargetDir(sFile, FileSourceDir, TargetDir),
						                                    cboxUAExistArchive.SelectedIndex, true, null, 4096);
						++m_nZipU; ++m_nCountU; ++nCount;
						// удаление исходного архива, если включена опция
						if ( cboxUADelFB2Files.Checked )
							DeleteFileIsNeeds( sFile );
					}
					bw.ReportProgress( ++n ); // отобразим данные в контролах
				}
			}
			return nCount;
		}
		
		// инициализация контролов и переменных (Распаковка)
		private void InitUA() {
			for( int i=0; i!=lvUAGeneralCount.Items.Count; ++i ) {
				lvUAGeneralCount.Items[i].SubItems[1].Text = "0";
			}
			tsProgressBar.Value		= 0;
			tsslblProgress.Text		= Settings.Settings.GetReady();
			tsProgressBar.Visible	= false;
			m_nUnpackCount			= 0;
		}
		
		// проверки папки-приемника
		private bool IsTargetDirCorrect( string sTarget, bool bToAnotherDir, string sMessTitle ) {
			if( bToAnotherDir ) {
				// папка-приемник - отличная от источника
				if( sTarget.Length == 0 ) {
					MessageBox.Show( "Не задана папка-приемник архивов!", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return false;
				} else {
					// проверка папки-приемника и создание ее, если нужно
					if( !filesWorker.CreateDirIfNeed( sTarget, sMessTitle ) )
						return false;
				}
			}
			return true;
		}
		
		// доступность контролов при Распаковке
		private void SetUnPackingStartEnabled( bool bEnabled ) {
			tsbtnUnArchive.Enabled		= bEnabled;
			pUAScanDir.Enabled			= bEnabled;
			pUAType.Enabled				= bEnabled;
			pUAToAnotherDir.Enabled		= bEnabled;
			cboxUADelFB2Files.Enabled	= bEnabled;
			tpArchive.Enabled			= bEnabled;
			
			tsbtnUnArchiveStop.Enabled	= !bEnabled;
			tsProgressBar.Visible		= !bEnabled;
			tcArchiver.Refresh();
			ssProgress.Refresh();
		}
		
		// сканировать ли подпапки для режима распаковки
		private bool getScanSubDirsForUnZip() {
			return cboxScanSubDirToUnArchive.Checked;
		}
		
		// получение source папки для режима распаковки
		private string getSourceDirForUnZip() {
			return filesWorker.WorkingDirPath( tboxUASourceDir.Text.Trim() );
		}
		
		// получение target папки для режима распаковки
		private string getTargetDirForUnZip() {
			if (cboxUAToSomeDir.Checked)
				return filesWorker.WorkingDirPath( tboxUASourceDir.Text.Trim() );
			else
				return filesWorker.WorkingDirPath( tboxUAToAnotherDir.Text.Trim() );
		}
		
		// Отобразим результат Распаковки
		private void UnArchiveProgressData() {
			lvUAGeneralCount.Items[2].SubItems[1].Text = (m_nCountU).ToString();
			lvUAGeneralCount.Items[3].SubItems[1].Text = (m_nFB2U).ToString();
		}
		
		#region Обработчики событий
		// задание папки с fb2-архивами для сканирования (Распаковка)
		void BtnUAOpenDirClick(object sender, EventArgs e)
		{
			if( filesWorker.OpenDirDlg( tboxUASourceDir, fbdDir, "Укажите папку с fb2-архивами для Распаковки" ) )
				InitUA();
		}
		
		// задание папки для копирования распакованных файлов
		void BtnUAToAnotherDirClick(object sender, EventArgs e)
		{
			filesWorker.OpenDirDlg( tboxUAToAnotherDir, fbdDir, "Укажите папку для размещения распакованных файлов" );
		}
		
		void CboxUAToSomeDirCheckedChanged(object sender, EventArgs e)
		{
			pUAToAnotherDir.Enabled = !cboxUAToSomeDir.Checked;
			if( !cboxUAToSomeDir.Checked )
				tboxUAToAnotherDir.Focus();
			saveSettingsToXml();
		}
		
		// Распаковка архивов
		void TsbtnUnArchiveClick(object sender, EventArgs e)
		{
			m_sMessTitle = "SharpFBTools - Распаковка архивов";

			// проверки папки для сканирования
			if( !IsSourceDirCorrect( getSourceDirForUnZip(), m_sMessTitle ) )
				return;

			// проверки папки-приемника
			if( !IsTargetDirCorrect( getTargetDirForUnZip(), cboxUAToSomeDir.Checked, m_sMessTitle ) )
				return;

			// инициализация контролов
			InitUA();
			SetUnPackingStartEnabled( false );
			
			m_dtStart = DateTime.Now;
			tsslblProgress.Text = "Создание списка папок:";

			// Запуск процесса DoWork от Бекграунд Воркера
			if( m_bwu.IsBusy != true ) {
				//если не занят то запустить процесс
				m_bwu.RunWorkerAsync();
			}
		}
		
		void TboxUASourceDirTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void TboxUAToAnotherDirTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void CboxScanSubDirToUnArchiveCheckStateChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void CboxUAExistArchiveSelectedIndexChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void CboxUADelFB2FilesCheckStateChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void ChBoxViewProgressUCheckStateChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		// Остановка выполнения процесса Распаковки
		void TsbtnUnArchiveStopClick(object sender, EventArgs e)
		{
			if( m_bwu.WorkerSupportsCancellation == true )
				m_bwu.CancelAsync();
		}
		#endregion
		
		#endregion

	}
}
