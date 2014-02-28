/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 24.02.2014
 * Time: 8:27
 * 
 * License: GPL 2.1
 */
namespace Core.Duplicator
{
	partial class ComrareForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ComrareForm));
			this.ProgressPanel = new System.Windows.Forms.Panel();
			this.StatusTextBox = new System.Windows.Forms.TextBox();
			this.ProgressBar = new System.Windows.Forms.ProgressBar();
			this.ControlPanel = new System.Windows.Forms.Panel();
			this.btnSaveToXml = new System.Windows.Forms.Button();
			this.btnStop = new System.Windows.Forms.Button();
			this.sfdList = new System.Windows.Forms.SaveFileDialog();
			this.ProgressPanel.SuspendLayout();
			this.ControlPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// ProgressPanel
			// 
			this.ProgressPanel.Controls.Add(this.StatusTextBox);
			this.ProgressPanel.Controls.Add(this.ProgressBar);
			this.ProgressPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.ProgressPanel.Location = new System.Drawing.Point(0, 0);
			this.ProgressPanel.Name = "ProgressPanel";
			this.ProgressPanel.Size = new System.Drawing.Size(516, 178);
			this.ProgressPanel.TabIndex = 0;
			// 
			// StatusTextBox
			// 
			this.StatusTextBox.Location = new System.Drawing.Point(11, 42);
			this.StatusTextBox.Multiline = true;
			this.StatusTextBox.Name = "StatusTextBox";
			this.StatusTextBox.ReadOnly = true;
			this.StatusTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.StatusTextBox.Size = new System.Drawing.Size(491, 124);
			this.StatusTextBox.TabIndex = 1;
			// 
			// ProgressBar
			// 
			this.ProgressBar.Location = new System.Drawing.Point(12, 13);
			this.ProgressBar.Name = "ProgressBar";
			this.ProgressBar.Size = new System.Drawing.Size(490, 23);
			this.ProgressBar.TabIndex = 0;
			// 
			// ControlPanel
			// 
			this.ControlPanel.Controls.Add(this.btnSaveToXml);
			this.ControlPanel.Controls.Add(this.btnStop);
			this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.ControlPanel.Location = new System.Drawing.Point(513, 0);
			this.ControlPanel.Name = "ControlPanel";
			this.ControlPanel.Size = new System.Drawing.Size(156, 178);
			this.ControlPanel.TabIndex = 1;
			// 
			// btnSaveToXml
			// 
			this.btnSaveToXml.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnSaveToXml.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnSaveToXml.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveToXml.Image")));
			this.btnSaveToXml.Location = new System.Drawing.Point(0, 58);
			this.btnSaveToXml.Name = "btnSaveToXml";
			this.btnSaveToXml.Size = new System.Drawing.Size(156, 58);
			this.btnSaveToXml.TabIndex = 1;
			this.btnSaveToXml.Text = "Прервать в xml...";
			this.btnSaveToXml.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnSaveToXml.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnSaveToXml.UseVisualStyleBackColor = true;
			this.btnSaveToXml.Click += new System.EventHandler(this.BtnSaveToXmlClick);
			// 
			// btnStop
			// 
			this.btnStop.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
			this.btnStop.Location = new System.Drawing.Point(0, 0);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(156, 58);
			this.btnStop.TabIndex = 0;
			this.btnStop.Text = "Прервать";
			this.btnStop.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.BtnStopClick);
			// 
			// sfdList
			// 
			this.sfdList.RestoreDirectory = true;
			this.sfdList.Title = "Укажите название файла копий";
			// 
			// ComrareForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(669, 178);
			this.ControlBox = false;
			this.Controls.Add(this.ControlPanel);
			this.Controls.Add(this.ProgressPanel);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ComrareForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Поиск копий fb2 книг";
			this.ProgressPanel.ResumeLayout(false);
			this.ProgressPanel.PerformLayout();
			this.ControlPanel.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.SaveFileDialog sfdList;
		private System.Windows.Forms.TextBox StatusTextBox;
		private System.Windows.Forms.ProgressBar ProgressBar;
		private System.Windows.Forms.Button btnSaveToXml;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Panel ControlPanel;
		private System.Windows.Forms.Panel ProgressPanel;
	}
}
