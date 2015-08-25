/*
 * Сделано в SharpDevelop.
 * Пользователь: Вадим Кузнецов (dikbsd)
 * Дата: 04.08.2015
 * Время: 15:23
 * 
 */
namespace Core.Common
{
	partial class EditLangForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditLangForm));
			this.ControlPanel = new System.Windows.Forms.Panel();
			this.CancelBtn = new System.Windows.Forms.Button();
			this.ApplyBtn = new System.Windows.Forms.Button();
			this.LangsPanel = new System.Windows.Forms.Panel();
			this.LangComboBox = new System.Windows.Forms.ComboBox();
			this.TILangLabel = new System.Windows.Forms.Label();
			this.ProgressBar = new System.Windows.Forms.ProgressBar();
			this.ControlPanel.SuspendLayout();
			this.LangsPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// ControlPanel
			// 
			this.ControlPanel.BackColor = System.Drawing.Color.DarkGray;
			this.ControlPanel.Controls.Add(this.CancelBtn);
			this.ControlPanel.Controls.Add(this.ApplyBtn);
			this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.ControlPanel.Enabled = false;
			this.ControlPanel.Location = new System.Drawing.Point(312, 0);
			this.ControlPanel.Name = "ControlPanel";
			this.ControlPanel.Size = new System.Drawing.Size(127, 101);
			this.ControlPanel.TabIndex = 5;
			// 
			// CancelBtn
			// 
			this.CancelBtn.Dock = System.Windows.Forms.DockStyle.Top;
			this.CancelBtn.Image = ((System.Drawing.Image)(resources.GetObject("CancelBtn.Image")));
			this.CancelBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.CancelBtn.Location = new System.Drawing.Point(0, 50);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(127, 48);
			this.CancelBtn.TabIndex = 1;
			this.CancelBtn.Text = "Отмена";
			this.CancelBtn.UseVisualStyleBackColor = true;
			this.CancelBtn.Click += new System.EventHandler(this.CancelBtnClick);
			// 
			// ApplyBtn
			// 
			this.ApplyBtn.Dock = System.Windows.Forms.DockStyle.Top;
			this.ApplyBtn.Image = ((System.Drawing.Image)(resources.GetObject("ApplyBtn.Image")));
			this.ApplyBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.ApplyBtn.Location = new System.Drawing.Point(0, 0);
			this.ApplyBtn.Name = "ApplyBtn";
			this.ApplyBtn.Size = new System.Drawing.Size(127, 50);
			this.ApplyBtn.TabIndex = 0;
			this.ApplyBtn.Text = "Принять";
			this.ApplyBtn.UseVisualStyleBackColor = true;
			this.ApplyBtn.Click += new System.EventHandler(this.ApplyBtnClick);
			// 
			// LangsPanel
			// 
			this.LangsPanel.Controls.Add(this.LangComboBox);
			this.LangsPanel.Controls.Add(this.TILangLabel);
			this.LangsPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.LangsPanel.Location = new System.Drawing.Point(0, 0);
			this.LangsPanel.Name = "LangsPanel";
			this.LangsPanel.Size = new System.Drawing.Size(312, 38);
			this.LangsPanel.TabIndex = 6;
			// 
			// LangComboBox
			// 
			this.LangComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.LangComboBox.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.LangComboBox.FormattingEnabled = true;
			this.LangComboBox.Location = new System.Drawing.Point(70, 7);
			this.LangComboBox.Margin = new System.Windows.Forms.Padding(4);
			this.LangComboBox.Name = "LangComboBox";
			this.LangComboBox.Size = new System.Drawing.Size(224, 24);
			this.LangComboBox.TabIndex = 37;
			// 
			// TILangLabel
			// 
			this.TILangLabel.ForeColor = System.Drawing.Color.Red;
			this.TILangLabel.Location = new System.Drawing.Point(3, 8);
			this.TILangLabel.Name = "TILangLabel";
			this.TILangLabel.Size = new System.Drawing.Size(58, 23);
			this.TILangLabel.TabIndex = 0;
			this.TILangLabel.Text = "Язык:";
			// 
			// ProgressBar
			// 
			this.ProgressBar.Location = new System.Drawing.Point(3, 73);
			this.ProgressBar.Name = "ProgressBar";
			this.ProgressBar.Size = new System.Drawing.Size(291, 23);
			this.ProgressBar.TabIndex = 7;
			// 
			// EditLangForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(439, 101);
			this.ControlBox = false;
			this.Controls.Add(this.ProgressBar);
			this.Controls.Add(this.LangsPanel);
			this.Controls.Add(this.ControlPanel);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(457, 146);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(457, 146);
			this.Name = "EditLangForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Правка Языка";
			this.ControlPanel.ResumeLayout(false);
			this.LangsPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		private System.Windows.Forms.ProgressBar ProgressBar;
		private System.Windows.Forms.Panel ControlPanel;
		private System.Windows.Forms.Button CancelBtn;
		private System.Windows.Forms.Button ApplyBtn;
		private System.Windows.Forms.Panel LangsPanel;
		private System.Windows.Forms.ComboBox LangComboBox;
		private System.Windows.Forms.Label TILangLabel;
	}
}
