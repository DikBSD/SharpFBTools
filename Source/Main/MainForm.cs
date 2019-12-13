﻿/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 13.03.2009
 * Time: 13:41
 * 
 * License: GPL 2.1
 */

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

using Options;
using SharpFBTools;
using SharpFBTools.AssemblyInfo;
using SharpFBTools.Tools;

using filesWorker = Core.Common.FilesWorker;

namespace Main
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		#region Designer
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.tscMain = new System.Windows.Forms.ToolStripContainer();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbtnFB2Corrector = new System.Windows.Forms.ToolStripButton();
            this.tsSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnFB2Dublicator = new System.Windows.Forms.ToolStripButton();
            this.tsSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnFileManager = new System.Windows.Forms.ToolStripButton();
            this.tsSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnArchiveManager = new System.Windows.Forms.ToolStripButton();
            this.tsSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnAbout = new System.Windows.Forms.ToolStripButton();
            this.tsSep5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSep6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnOptions = new System.Windows.Forms.ToolStripButton();
            this.tsSep7 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbtnExit = new System.Windows.Forms.ToolStripButton();
            this.tscMain.TopToolStripPanel.SuspendLayout();
            this.tscMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tscMain
            // 
            // 
            // tscMain.ContentPanel
            // 
            this.tscMain.ContentPanel.Size = new System.Drawing.Size(828, 555);
            this.tscMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tscMain.Location = new System.Drawing.Point(0, 0);
            this.tscMain.Name = "tscMain";
            this.tscMain.Size = new System.Drawing.Size(828, 609);
            this.tscMain.TabIndex = 0;
            this.tscMain.Text = "toolStripContainer1";
            // 
            // tscMain.TopToolStripPanel
            // 
            this.tscMain.TopToolStripPanel.BackColor = System.Drawing.SystemColors.Control;
            this.tscMain.TopToolStripPanel.Controls.Add(this.tsMain);
            // 
            // tsMain
            // 
            this.tsMain.Dock = System.Windows.Forms.DockStyle.None;
            this.tsMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbtnFB2Corrector,
            this.tsSep1,
            this.tsbtnFB2Dublicator,
            this.tsSep2,
            this.tsbtnFileManager,
            this.tsSep3,
            this.tsbtnArchiveManager,
            this.tsSep4,
            this.tsbtnOptions,
            this.tsSep5,
            this.tsbtnAbout,
            this.tsSep6,
            this.tsSep7,
            this.tsbtnExit});
            this.tsMain.Location = new System.Drawing.Point(3, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(587, 54);
            this.tsMain.TabIndex = 0;
            // 
            // tsbtnFB2Corrector
            // 
            this.tsbtnFB2Corrector.BackColor = System.Drawing.Color.Gainsboro;
            this.tsbtnFB2Corrector.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnFB2Corrector.Image")));
            this.tsbtnFB2Corrector.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnFB2Corrector.Name = "tsbtnFB2Corrector";
            this.tsbtnFB2Corrector.Size = new System.Drawing.Size(70, 51);
            this.tsbtnFB2Corrector.Tag = "tsbtnFB2Dublicator";
            this.tsbtnFB2Corrector.Text = "Корректор";
            this.tsbtnFB2Corrector.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnFB2Corrector.ToolTipText = "Корректор структуры книги";
            this.tsbtnFB2Corrector.Click += new System.EventHandler(this.TsbtnDescEditorClick);
            // 
            // tsSep1
            // 
            this.tsSep1.BackColor = System.Drawing.Color.LightGray;
            this.tsSep1.Name = "tsSep1";
            this.tsSep1.Size = new System.Drawing.Size(6, 54);
            // 
            // tsbtnFB2Dublicator
            // 
            this.tsbtnFB2Dublicator.BackColor = System.Drawing.Color.Gainsboro;
            this.tsbtnFB2Dublicator.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnFB2Dublicator.Image")));
            this.tsbtnFB2Dublicator.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnFB2Dublicator.Name = "tsbtnFB2Dublicator";
            this.tsbtnFB2Dublicator.Size = new System.Drawing.Size(77, 51);
            this.tsbtnFB2Dublicator.Tag = "tsbtnFB2Dublicator";
            this.tsbtnFB2Dublicator.Text = "Дубликатор";
            this.tsbtnFB2Dublicator.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnFB2Dublicator.ToolTipText = "Поиск одинаковых книг";
            this.tsbtnFB2Dublicator.Click += new System.EventHandler(this.TsbtnFB2DublicatorClick);
            // 
            // tsSep2
            // 
            this.tsSep2.BackColor = System.Drawing.Color.LightGray;
            this.tsSep2.Name = "tsSep2";
            this.tsSep2.Size = new System.Drawing.Size(6, 54);
            // 
            // tsbtnFileManager
            // 
            this.tsbtnFileManager.BackColor = System.Drawing.Color.Gainsboro;
            this.tsbtnFileManager.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnFileManager.Image")));
            this.tsbtnFileManager.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnFileManager.Name = "tsbtnFileManager";
            this.tsbtnFileManager.Size = new System.Drawing.Size(89, 51);
            this.tsbtnFileManager.Tag = "tsbtnFileManager";
            this.tsbtnFileManager.Text = "Сортировщик";
            this.tsbtnFileManager.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnFileManager.Click += new System.EventHandler(this.TsbtnFileManagerClick);
            // 
            // tsSep3
            // 
            this.tsSep3.BackColor = System.Drawing.Color.LightGray;
            this.tsSep3.Name = "tsSep3";
            this.tsSep3.Size = new System.Drawing.Size(6, 54);
            // 
            // tsbtnArchiveManager
            // 
            this.tsbtnArchiveManager.BackColor = System.Drawing.Color.Gainsboro;
            this.tsbtnArchiveManager.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnArchiveManager.Image")));
            this.tsbtnArchiveManager.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnArchiveManager.Name = "tsbtnArchiveManager";
            this.tsbtnArchiveManager.Size = new System.Drawing.Size(69, 51);
            this.tsbtnArchiveManager.Tag = "tsbtnArchiveManager";
            this.tsbtnArchiveManager.Text = "Архиватор";
            this.tsbtnArchiveManager.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnArchiveManager.Click += new System.EventHandler(this.TsbtnArchiveManagerClick);
            // 
            // tsSep4
            // 
            this.tsSep4.BackColor = System.Drawing.Color.LightGray;
            this.tsSep4.Name = "tsSep4";
            this.tsSep4.Size = new System.Drawing.Size(6, 54);
            // 
            // tsbtnAbout
            // 
            this.tsbtnAbout.BackColor = System.Drawing.Color.Gainsboro;
            this.tsbtnAbout.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAbout.Image")));
            this.tsbtnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnAbout.Name = "tsbtnAbout";
            this.tsbtnAbout.Size = new System.Drawing.Size(66, 51);
            this.tsbtnAbout.Tag = "tsbtnAbout";
            this.tsbtnAbout.Text = " Помощь ";
            this.tsbtnAbout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnAbout.ToolTipText = "Помощь...";
            this.tsbtnAbout.Click += new System.EventHandler(this.TsbtnAboutClick);
            // 
            // tsSep5
            // 
            this.tsSep5.BackColor = System.Drawing.Color.LightGray;
            this.tsSep5.Name = "tsSep5";
            this.tsSep5.Size = new System.Drawing.Size(6, 54);
            // 
            // tsSep6
            // 
            this.tsSep6.BackColor = System.Drawing.Color.LightGray;
            this.tsSep6.Name = "tsSep6";
            this.tsSep6.Size = new System.Drawing.Size(6, 54);
            // 
            // tsbtnOptions
            // 
            this.tsbtnOptions.BackColor = System.Drawing.Color.LightGray;
            this.tsbtnOptions.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnOptions.Image")));
            this.tsbtnOptions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnOptions.Name = "tsbtnOptions";
            this.tsbtnOptions.Size = new System.Drawing.Size(80, 51);
            this.tsbtnOptions.Text = "Настройки...";
            this.tsbtnOptions.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnOptions.ToolTipText = "Настройки...";
            this.tsbtnOptions.Click += new System.EventHandler(this.TsbtnOptionsClick);
            // 
            // tsSep7
            // 
            this.tsSep7.BackColor = System.Drawing.Color.LightGray;
            this.tsSep7.Name = "tsSep7";
            this.tsSep7.Size = new System.Drawing.Size(6, 54);
            // 
            // tsbtnExit
            // 
            this.tsbtnExit.BackColor = System.Drawing.Color.LightGray;
            this.tsbtnExit.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnExit.Image")));
            this.tsbtnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbtnExit.Name = "tsbtnExit";
            this.tsbtnExit.Size = new System.Drawing.Size(51, 51);
            this.tsbtnExit.Text = " Выход ";
            this.tsbtnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbtnExit.ToolTipText = "Выход из программы";
            this.tsbtnExit.Click += new System.EventHandler(this.TsbtnExitClick);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(828, 609);
            this.Controls.Add(this.tscMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SharpFBTools";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFormFormClosed);
            this.tscMain.TopToolStripPanel.ResumeLayout(false);
            this.tscMain.TopToolStripPanel.PerformLayout();
            this.tscMain.ResumeLayout(false);
            this.tscMain.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.ResumeLayout(false);

		}
		private System.Windows.Forms.ToolStripSeparator tsSep5;
		private System.Windows.Forms.ToolStripButton tsbtnFB2Corrector;
		private System.Windows.Forms.ToolStripSeparator tsSep7;
		private System.Windows.Forms.ToolStripButton tsbtnOptions;
		private System.Windows.Forms.ToolStripButton tsbtnAbout;
		private System.Windows.Forms.ToolStripSeparator tsSep6;
		private System.Windows.Forms.ToolStripButton tsbtnFB2Dublicator;
		private System.Windows.Forms.ToolStripButton tsbtnFileManager;
		private System.Windows.Forms.ToolStripButton tsbtnArchiveManager;
		private System.Windows.Forms.ToolStripButton tsbtnExit;
		private System.Windows.Forms.ToolStripSeparator tsSep4;
		private System.Windows.Forms.ToolStripSeparator tsSep3;
		private System.Windows.Forms.ToolStripSeparator tsSep2;
		private System.Windows.Forms.ToolStripSeparator tsSep1;
		private System.Windows.Forms.ToolStrip tsMain;
		private System.Windows.Forms.ToolStripContainer tscMain;
		#endregion
		
		#region Закрытые члены класса
		private List<ToolStripButton>	m_listToggleBtns = new List<ToolStripButton>();	// список кнопок-переключателей панели инструментов
		private List<UserControl>		m_listImplPanels = new List<UserControl>();		// список имплантируемых панелей-режимов работы

		private readonly static SFBTpFB2Corrector	sfbTpFB2Corrector	= new SFBTpFB2Corrector();	// панель Корректора структуры книги
		private readonly static SFBTpFB2Sorter		sfbTpFB2Sorter		= new SFBTpFB2Sorter();		// панель Сортировщика
		private readonly static SFBTpArchiveManager	sfbTpArchiveManager	= new SFBTpArchiveManager();// панель Менеджера архивов
		private readonly static SFBTpFB2Dublicator	sfbTpFB2Dublicator	= new SFBTpFB2Dublicator();	// панель Дубликатора файлов
		private readonly static SFBTpAbout			sfbTpAbout			= new SFBTpAbout();			// панель О программе
		#endregion
		
		public MainForm()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			// Запоминаем папку программы
			Settings.Settings.ProgDir = Environment.CurrentDirectory;
			// задаем данные о программе Assembly
			SharpFBTools_AssemblyInfo.SetAssemblyTitle( AppAssembly.AssemblyTitle );
			SharpFBTools_AssemblyInfo.SetAssemblyProduct( AppAssembly.AssemblyProduct );
			SharpFBTools_AssemblyInfo.SetAssemblyVersion( AppAssembly.AssemblyVersion );
			SharpFBTools_AssemblyInfo.SetAssemblyCopyright( AppAssembly.AssemblyCopyright );
			SharpFBTools_AssemblyInfo.SetAssemblyCompany( AppAssembly.AssemblyCompany );
			SharpFBTools_AssemblyInfo.SetAssemblyDescription( AppAssembly.AssemblyDescription );
			// список кнопок-переключателей панели инструментов и список список имплантируемых панелей-режимов работы
			MainImpl.MakeGroupToggleLists(
				m_listToggleBtns,
				tsbtnFB2Corrector, tsbtnFB2Dublicator, tsbtnFileManager, tsbtnArchiveManager, tsbtnAbout,
				m_listImplPanels,
				sfbTpFB2Corrector, sfbTpFB2Dublicator,
				sfbTpFB2Sorter, sfbTpArchiveManager, sfbTpAbout
			);
			// первоначальное задание режима работы - панель Валидатора
			tsbtnFB2Corrector.Checked = true;
			this.tscMain.ContentPanel.Controls.Add( sfbTpFB2Corrector );
			sfbTpFB2Corrector.Dock = System.Windows.Forms.DockStyle.Fill;
			
			AppDomain.CurrentDomain.SetData("REGEX_DEFAULT_MATCH_TIMEOUT", TimeSpan.FromMilliseconds(10000));
		}
		
		public static SFBTpFB2Corrector getSFBTpFB2Corrector() {
			return sfbTpFB2Corrector;
		}
		
		public static SFBTpFB2Dublicator getSFBTpFB2Dublicator() {
			return sfbTpFB2Dublicator;
		}
		
		public static SFBTpFB2Sorter getSFBTpFileManager() {
			return sfbTpFB2Sorter;
		}
		
		#region Обработчики событий
		void TsbtnExitClick(object sender, EventArgs e)
		{
			// выход из программы
			string TempDir = Settings.Settings.TempDirPath;
			if ( ! Settings.Settings.ConfirmationForExit ) {
				// очистка временной папки
				filesWorker.RemoveDir( TempDir );
				this.Close();
			} else {
				DialogResult result = MessageBox.Show(
					"Вы действительно хотите выйти из программы?", "SharpFBTools",
					MessageBoxButtons.YesNo, MessageBoxIcon.Question
				);
				if ( result == DialogResult.Yes ) {
					// очистка временной папки
					filesWorker.RemoveDir( TempDir );
					this.Close();
				}
				return;
			}
		}
		
		void TsbtnFileManagerClick(object sender, EventArgs e)
		{
			// переключение состояния кнопки Менеджера файлов
			MainImpl.ToggleMode( m_listToggleBtns, m_listImplPanels,
			                    tsbtnFileManager, sfbTpFB2Sorter,
			                    tscMain );
		}
		
		void TsbtnArchiveManagerClick(object sender, EventArgs e)
		{
			// переключение состояния кнопки Менеджера архивов
			MainImpl.ToggleMode( m_listToggleBtns, m_listImplPanels,
			                    tsbtnArchiveManager, sfbTpArchiveManager,
			                    tscMain );
		}
		
		void TsbtnFB2DublicatorClick(object sender, EventArgs e)
		{
			// переключение состояния кнопки Дубликатора файлов
			MainImpl.ToggleMode( m_listToggleBtns, m_listImplPanels,
			                    tsbtnFB2Dublicator, sfbTpFB2Dublicator,
			                    tscMain );
		}
		
		void TsbtnDescEditorClick(object sender, EventArgs e)
		{
			// переключение состояния кнопки Редактора описания книги файлов
			MainImpl.ToggleMode( m_listToggleBtns, m_listImplPanels,
			                    tsbtnFB2Corrector, sfbTpFB2Corrector,
			                    tscMain );
		}
		
		void TsbtnAboutClick(object sender, EventArgs e)
		{
			// переключение состояния кнопки О программе
			MainImpl.ToggleMode( m_listToggleBtns, m_listImplPanels,
			                    tsbtnAbout, sfbTpAbout,
			                    tscMain );
		}
		
		void TsbtnOptionsClick(object sender, EventArgs e)
		{
			// запуск диалога Настроек
			OptionsForm ofrm = new OptionsForm();
			ofrm.ShowDialog();
			// задание для кнопок ToolStrip стиля и положения текста и картинки
			sfbTpArchiveManager.SetToolButtonsSettings();
			sfbTpFB2Dublicator.SetToolButtonsSettings();
			ofrm.Dispose();
		}
		// сохранение настроек инструментов
		void MainFormFormClosed(object sender, FormClosedEventArgs e)
		{
			sfbTpFB2Corrector.saveSettingsToXml();
			sfbTpFB2Dublicator.saveSettingsToXml();
			sfbTpFB2Sorter.saveSettingsToXml();
		}
		#endregion
	}
}
