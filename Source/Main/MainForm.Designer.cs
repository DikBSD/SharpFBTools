/*
 * Created by SharpDevelop.
 * User: DikBSD
 * Date: 13.03.2009
 * Time: 13:41
 * 
 * License: GPL 2.1
 */
namespace Main
{
	partial class MainForm
	{
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
			this.tsbtnFB2Validator = new System.Windows.Forms.ToolStripButton();
			this.tsSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnFileManager = new System.Windows.Forms.ToolStripButton();
			this.tsSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnArchiveManager = new System.Windows.Forms.ToolStripButton();
			this.tsSep3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnFB2Corrector = new System.Windows.Forms.ToolStripButton();
			this.tsSep5 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnFB2Dublicator = new System.Windows.Forms.ToolStripButton();
			this.tsSep4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnAbout = new System.Windows.Forms.ToolStripButton();
			this.tsSep6 = new System.Windows.Forms.ToolStripSeparator();
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
			this.tscMain.ContentPanel.Size = new System.Drawing.Size(764, 557);
			this.tscMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tscMain.Location = new System.Drawing.Point(0, 0);
			this.tscMain.Name = "tscMain";
			this.tscMain.Size = new System.Drawing.Size(764, 609);
			this.tscMain.TabIndex = 0;
			this.tscMain.Text = "toolStripContainer1";
			// 
			// tscMain.TopToolStripPanel
			// 
			this.tscMain.TopToolStripPanel.Controls.Add(this.tsMain);
			// 
			// tsMain
			// 
			this.tsMain.Dock = System.Windows.Forms.DockStyle.None;
			this.tsMain.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsbtnFB2Validator,
									this.tsSep1,
									this.tsbtnFileManager,
									this.tsSep2,
									this.tsbtnArchiveManager,
									this.tsSep3,
									this.tsbtnFB2Corrector,
									this.tsSep5,
									this.tsbtnFB2Dublicator,
									this.tsSep4,
									this.tsbtnAbout,
									this.tsSep6,
									this.tsbtnExit});
			this.tsMain.Location = new System.Drawing.Point(3, 0);
			this.tsMain.Name = "tsMain";
			this.tsMain.Size = new System.Drawing.Size(686, 52);
			this.tsMain.TabIndex = 0;
			// 
			// tsbtnFB2Validator
			// 
			this.tsbtnFB2Validator.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnFB2Validator.Image")));
			this.tsbtnFB2Validator.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnFB2Validator.Name = "tsbtnFB2Validator";
			this.tsbtnFB2Validator.Size = new System.Drawing.Size(87, 49);
			this.tsbtnFB2Validator.Tag = "tsbtnFB2Validator";
			this.tsbtnFB2Validator.Text = "FB2 Валидатор";
			this.tsbtnFB2Validator.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.tsbtnFB2Validator.ToolTipText = "FB2 Валидатор";
			this.tsbtnFB2Validator.Click += new System.EventHandler(this.TsbtnFB2ValidatorClick);
			// 
			// tsSep1
			// 
			this.tsSep1.Name = "tsSep1";
			this.tsSep1.Size = new System.Drawing.Size(6, 52);
			// 
			// tsbtnFileManager
			// 
			this.tsbtnFileManager.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnFileManager.Image")));
			this.tsbtnFileManager.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnFileManager.Name = "tsbtnFileManager";
			this.tsbtnFileManager.Size = new System.Drawing.Size(105, 49);
			this.tsbtnFileManager.Tag = "tsbtnFileManager";
			this.tsbtnFileManager.Text = "Менеджер файлов";
			this.tsbtnFileManager.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.tsbtnFileManager.Click += new System.EventHandler(this.TsbtnFileManagerClick);
			// 
			// tsSep2
			// 
			this.tsSep2.Name = "tsSep2";
			this.tsSep2.Size = new System.Drawing.Size(6, 52);
			// 
			// tsbtnArchiveManager
			// 
			this.tsbtnArchiveManager.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnArchiveManager.Image")));
			this.tsbtnArchiveManager.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnArchiveManager.Name = "tsbtnArchiveManager";
			this.tsbtnArchiveManager.Size = new System.Drawing.Size(109, 49);
			this.tsbtnArchiveManager.Tag = "tsbtnArchiveManager";
			this.tsbtnArchiveManager.Text = "Менеджер архивов";
			this.tsbtnArchiveManager.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.tsbtnArchiveManager.Click += new System.EventHandler(this.TsbtnArchiveManagerClick);
			// 
			// tsSep3
			// 
			this.tsSep3.Name = "tsSep3";
			this.tsSep3.Size = new System.Drawing.Size(6, 52);
			// 
			// tsbtnFB2Corrector
			// 
			this.tsbtnFB2Corrector.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnFB2Corrector.Image")));
			this.tsbtnFB2Corrector.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnFB2Corrector.Name = "tsbtnFB2Corrector";
			this.tsbtnFB2Corrector.Size = new System.Drawing.Size(87, 49);
			this.tsbtnFB2Corrector.Tag = "tsbtnFB2Corrector";
			this.tsbtnFB2Corrector.Text = "FB2 Корректор";
			this.tsbtnFB2Corrector.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.tsbtnFB2Corrector.ToolTipText = "FB2 Корректор";
			this.tsbtnFB2Corrector.Click += new System.EventHandler(this.TsbtnFB2CorrectorClick);
			// 
			// tsSep5
			// 
			this.tsSep5.Name = "tsSep5";
			this.tsSep5.Size = new System.Drawing.Size(6, 52);
			// 
			// tsbtnFB2Dublicator
			// 
			this.tsbtnFB2Dublicator.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnFB2Dublicator.Image")));
			this.tsbtnFB2Dublicator.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnFB2Dublicator.Name = "tsbtnFB2Dublicator";
			this.tsbtnFB2Dublicator.Size = new System.Drawing.Size(114, 49);
			this.tsbtnFB2Dublicator.Tag = "tsbtnFB2Dublicator";
			this.tsbtnFB2Dublicator.Text = "Дубликатор файлов";
			this.tsbtnFB2Dublicator.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.tsbtnFB2Dublicator.ToolTipText = "Поиск одинаковых книг";
			this.tsbtnFB2Dublicator.Click += new System.EventHandler(this.TsbtnFB2DublicatorClick);
			// 
			// tsSep4
			// 
			this.tsSep4.Name = "tsSep4";
			this.tsSep4.Size = new System.Drawing.Size(6, 52);
			// 
			// tsbtnAbout
			// 
			this.tsbtnAbout.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAbout.Image")));
			this.tsbtnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnAbout.Name = "tsbtnAbout";
			this.tsbtnAbout.Size = new System.Drawing.Size(57, 49);
			this.tsbtnAbout.Tag = "tsbtnAbout";
			this.tsbtnAbout.Text = " Помощь ";
			this.tsbtnAbout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.tsbtnAbout.ToolTipText = "Помощь...";
			this.tsbtnAbout.Click += new System.EventHandler(this.TsbtnAboutClick);
			// 
			// tsSep6
			// 
			this.tsSep6.Name = "tsSep6";
			this.tsSep6.Size = new System.Drawing.Size(6, 52);
			// 
			// tsbtnExit
			// 
			this.tsbtnExit.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnExit.Image")));
			this.tsbtnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnExit.Name = "tsbtnExit";
			this.tsbtnExit.Size = new System.Drawing.Size(50, 49);
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
			this.ClientSize = new System.Drawing.Size(764, 609);
			this.Controls.Add(this.tscMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SharpFBTools";
			this.tscMain.TopToolStripPanel.ResumeLayout(false);
			this.tscMain.TopToolStripPanel.PerformLayout();
			this.tscMain.ResumeLayout(false);
			this.tscMain.PerformLayout();
			this.tsMain.ResumeLayout(false);
			this.tsMain.PerformLayout();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.ToolStripButton tsbtnAbout;
		private System.Windows.Forms.ToolStripButton tsbtnFB2Validator;
		private System.Windows.Forms.ToolStripSeparator tsSep6;
		private System.Windows.Forms.ToolStripButton tsbtnFB2Dublicator;
		private System.Windows.Forms.ToolStripSeparator tsSep5;
		private System.Windows.Forms.ToolStripButton tsbtnFB2Corrector;
		private System.Windows.Forms.ToolStripButton tsbtnFileManager;
		private System.Windows.Forms.ToolStripButton tsbtnArchiveManager;
		private System.Windows.Forms.ToolStripButton tsbtnExit;
		private System.Windows.Forms.ToolStripSeparator tsSep4;
		private System.Windows.Forms.ToolStripSeparator tsSep3;
		private System.Windows.Forms.ToolStripSeparator tsSep2;
		private System.Windows.Forms.ToolStripSeparator tsSep1;
		private System.Windows.Forms.ToolStrip tsMain;
		private System.Windows.Forms.ToolStripContainer tscMain;
	}
}
