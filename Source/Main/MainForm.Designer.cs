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
			this.stMainStatus = new System.Windows.Forms.StatusStrip();
			this.tsMain = new System.Windows.Forms.ToolStrip();
			this.tsbtnValidator = new System.Windows.Forms.ToolStripButton();
			this.tsSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnFileManager = new System.Windows.Forms.ToolStripButton();
			this.tsSep2 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnArchiveManager = new System.Windows.Forms.ToolStripButton();
			this.tsSep3 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnFB2Corrector = new System.Windows.Forms.ToolStripButton();
			this.tsSep5 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnAbout = new System.Windows.Forms.ToolStripButton();
			this.tsSep4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbtnExit = new System.Windows.Forms.ToolStripButton();
			this.tscMain.BottomToolStripPanel.SuspendLayout();
			this.tscMain.TopToolStripPanel.SuspendLayout();
			this.tscMain.SuspendLayout();
			this.tsMain.SuspendLayout();
			this.SuspendLayout();
			// 
			// tscMain
			// 
			// 
			// tscMain.BottomToolStripPanel
			// 
			this.tscMain.BottomToolStripPanel.Controls.Add(this.stMainStatus);
			// 
			// tscMain.ContentPanel
			// 
			this.tscMain.ContentPanel.Size = new System.Drawing.Size(764, 702);
			this.tscMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tscMain.Location = new System.Drawing.Point(0, 0);
			this.tscMain.Name = "tscMain";
			this.tscMain.Size = new System.Drawing.Size(764, 776);
			this.tscMain.TabIndex = 0;
			this.tscMain.Text = "toolStripContainer1";
			// 
			// tscMain.TopToolStripPanel
			// 
			this.tscMain.TopToolStripPanel.Controls.Add(this.tsMain);
			// 
			// stMainStatus
			// 
			this.stMainStatus.Dock = System.Windows.Forms.DockStyle.None;
			this.stMainStatus.Location = new System.Drawing.Point(0, 0);
			this.stMainStatus.Name = "stMainStatus";
			this.stMainStatus.Size = new System.Drawing.Size(764, 22);
			this.stMainStatus.TabIndex = 0;
			// 
			// tsMain
			// 
			this.tsMain.Dock = System.Windows.Forms.DockStyle.None;
			this.tsMain.ImageScalingSize = new System.Drawing.Size(32, 32);
			this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsbtnValidator,
									this.tsSep1,
									this.tsbtnFileManager,
									this.tsSep2,
									this.tsbtnArchiveManager,
									this.tsSep3,
									this.tsbtnFB2Corrector,
									this.tsSep5,
									this.tsbtnAbout,
									this.tsSep4,
									this.tsbtnExit});
			this.tsMain.Location = new System.Drawing.Point(3, 0);
			this.tsMain.Name = "tsMain";
			this.tsMain.Size = new System.Drawing.Size(557, 52);
			this.tsMain.TabIndex = 0;
			// 
			// tsbtnValidator
			// 
			this.tsbtnValidator.Checked = true;
			this.tsbtnValidator.CheckState = System.Windows.Forms.CheckState.Checked;
			this.tsbtnValidator.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnValidator.Image")));
			this.tsbtnValidator.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnValidator.Name = "tsbtnValidator";
			this.tsbtnValidator.Size = new System.Drawing.Size(66, 49);
			this.tsbtnValidator.Tag = "group";
			this.tsbtnValidator.Text = "Валидатор";
			this.tsbtnValidator.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.tsbtnValidator.ToolTipText = "Валидатор";
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
			this.tsbtnFileManager.Tag = "group";
			this.tsbtnFileManager.Text = "Менеджер файлов";
			this.tsbtnFileManager.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
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
			this.tsbtnArchiveManager.Tag = "group";
			this.tsbtnArchiveManager.Text = "Менеджер архивов";
			this.tsbtnArchiveManager.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
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
			this.tsbtnFB2Corrector.Tag = "group";
			this.tsbtnFB2Corrector.Text = "FB2 Корректор";
			this.tsbtnFB2Corrector.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.tsbtnFB2Corrector.ToolTipText = "FB2 Корректор";
			// 
			// tsSep5
			// 
			this.tsSep5.Name = "tsSep5";
			this.tsSep5.Size = new System.Drawing.Size(6, 52);
			// 
			// tsbtnAbout
			// 
			this.tsbtnAbout.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnAbout.Image")));
			this.tsbtnAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnAbout.Name = "tsbtnAbout";
			this.tsbtnAbout.Size = new System.Drawing.Size(75, 49);
			this.tsbtnAbout.Tag = "about";
			this.tsbtnAbout.Text = "О программе";
			this.tsbtnAbout.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.tsbtnAbout.ToolTipText = "О программе...";
			// 
			// tsSep4
			// 
			this.tsSep4.Name = "tsSep4";
			this.tsSep4.Size = new System.Drawing.Size(6, 52);
			// 
			// tsbtnExit
			// 
			this.tsbtnExit.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnExit.Image")));
			this.tsbtnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnExit.Name = "tsbtnExit";
			this.tsbtnExit.Size = new System.Drawing.Size(44, 49);
			this.tsbtnExit.Text = "Выход";
			this.tsbtnExit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.tsbtnExit.ToolTipText = "Выход из программы";
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(764, 776);
			this.Controls.Add(this.tscMain);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "MainForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "SharpFBTools";
			this.tscMain.BottomToolStripPanel.ResumeLayout(false);
			this.tscMain.BottomToolStripPanel.PerformLayout();
			this.tscMain.TopToolStripPanel.ResumeLayout(false);
			this.tscMain.TopToolStripPanel.PerformLayout();
			this.tscMain.ResumeLayout(false);
			this.tscMain.PerformLayout();
			this.tsMain.ResumeLayout(false);
			this.tsMain.PerformLayout();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.StatusStrip stMainStatus;
		private System.Windows.Forms.ToolStripSeparator tsSep5;
		private System.Windows.Forms.ToolStripButton tsbtnFB2Corrector;
		private System.Windows.Forms.ToolStripButton tsbtnFileManager;
		private System.Windows.Forms.ToolStripButton tsbtnArchiveManager;
		private System.Windows.Forms.ToolStripButton tsbtnExit;
		private System.Windows.Forms.ToolStripSeparator tsSep4;
		private System.Windows.Forms.ToolStripButton tsbtnAbout;
		private System.Windows.Forms.ToolStripSeparator tsSep3;
		private System.Windows.Forms.ToolStripSeparator tsSep2;
		private System.Windows.Forms.ToolStripSeparator tsSep1;
		private System.Windows.Forms.ToolStripButton tsbtnValidator;
		private System.Windows.Forms.ToolStrip tsMain;
		private System.Windows.Forms.ToolStripContainer tscMain;
	}
}
