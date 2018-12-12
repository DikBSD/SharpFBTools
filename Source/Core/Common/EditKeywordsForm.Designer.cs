/*
 * Создано в SharpDevelop.
 * Пользователь: VadimK
 * Дата: 09.09.2016
 * Время: 13:43
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
namespace Core.Common
{
	partial class EditKeywordsForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ProgressBar ProgressBar;
		private System.Windows.Forms.Panel ControlPanel;
		private System.Windows.Forms.Button CancelBtn;
		private System.Windows.Forms.Button ApplyBtn;
		private System.Windows.Forms.Panel KeywordsPanel;
		private System.Windows.Forms.Label TIKeywordsLabel;
		private System.Windows.Forms.TextBox KeywordsTextBox;
		private System.Windows.Forms.RadioButton AddRadioButton;
		private System.Windows.Forms.RadioButton ReplaceRadioButton;
		
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditKeywordsForm));
			this.ProgressBar = new System.Windows.Forms.ProgressBar();
			this.ControlPanel = new System.Windows.Forms.Panel();
			this.CancelBtn = new System.Windows.Forms.Button();
			this.ApplyBtn = new System.Windows.Forms.Button();
			this.KeywordsPanel = new System.Windows.Forms.Panel();
			this.KeywordsTextBox = new System.Windows.Forms.TextBox();
			this.TIKeywordsLabel = new System.Windows.Forms.Label();
			this.AddRadioButton = new System.Windows.Forms.RadioButton();
			this.ReplaceRadioButton = new System.Windows.Forms.RadioButton();
			this.ControlPanel.SuspendLayout();
			this.KeywordsPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// ProgressBar
			// 
			this.ProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ProgressBar.Location = new System.Drawing.Point(0, 112);
			this.ProgressBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.ProgressBar.Name = "ProgressBar";
			this.ProgressBar.Size = new System.Drawing.Size(514, 23);
			this.ProgressBar.TabIndex = 9;
			// 
			// ControlPanel
			// 
			this.ControlPanel.BackColor = System.Drawing.Color.DarkGray;
			this.ControlPanel.Controls.Add(this.CancelBtn);
			this.ControlPanel.Controls.Add(this.ApplyBtn);
			this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.ControlPanel.Enabled = false;
			this.ControlPanel.Location = new System.Drawing.Point(514, 0);
			this.ControlPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.ControlPanel.Name = "ControlPanel";
			this.ControlPanel.Size = new System.Drawing.Size(127, 135);
			this.ControlPanel.TabIndex = 8;
			// 
			// CancelBtn
			// 
			this.CancelBtn.Dock = System.Windows.Forms.DockStyle.Top;
			this.CancelBtn.Image = ((System.Drawing.Image)(resources.GetObject("CancelBtn.Image")));
			this.CancelBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.CancelBtn.Location = new System.Drawing.Point(0, 50);
			this.CancelBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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
			this.ApplyBtn.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.ApplyBtn.Name = "ApplyBtn";
			this.ApplyBtn.Size = new System.Drawing.Size(127, 50);
			this.ApplyBtn.TabIndex = 0;
			this.ApplyBtn.Text = "Принять";
			this.ApplyBtn.UseVisualStyleBackColor = true;
			this.ApplyBtn.Click += new System.EventHandler(this.ApplyBtnClick);
			// 
			// KeywordsPanel
			// 
			this.KeywordsPanel.Controls.Add(this.KeywordsTextBox);
			this.KeywordsPanel.Controls.Add(this.TIKeywordsLabel);
			this.KeywordsPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.KeywordsPanel.Location = new System.Drawing.Point(0, 0);
			this.KeywordsPanel.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.KeywordsPanel.Name = "KeywordsPanel";
			this.KeywordsPanel.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.KeywordsPanel.Size = new System.Drawing.Size(514, 68);
			this.KeywordsPanel.TabIndex = 10;
			// 
			// KeywordsTextBox
			// 
			this.KeywordsTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.KeywordsTextBox.Location = new System.Drawing.Point(4, 27);
			this.KeywordsTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.KeywordsTextBox.Name = "KeywordsTextBox";
			this.KeywordsTextBox.Size = new System.Drawing.Size(506, 22);
			this.KeywordsTextBox.TabIndex = 1;
			// 
			// TIKeywordsLabel
			// 
			this.TIKeywordsLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.TIKeywordsLabel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.TIKeywordsLabel.Location = new System.Drawing.Point(4, 4);
			this.TIKeywordsLabel.Name = "TIKeywordsLabel";
			this.TIKeywordsLabel.Size = new System.Drawing.Size(506, 23);
			this.TIKeywordsLabel.TabIndex = 0;
			this.TIKeywordsLabel.Text = "Ключевые слова:";
			// 
			// AddRadioButton
			// 
			this.AddRadioButton.Checked = true;
			this.AddRadioButton.Location = new System.Drawing.Point(4, 74);
			this.AddRadioButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.AddRadioButton.Name = "AddRadioButton";
			this.AddRadioButton.Size = new System.Drawing.Size(235, 30);
			this.AddRadioButton.TabIndex = 11;
			this.AddRadioButton.TabStop = true;
			this.AddRadioButton.Text = "Добавить к существующим";
			this.AddRadioButton.UseVisualStyleBackColor = true;
			// 
			// ReplaceRadioButton
			// 
			this.ReplaceRadioButton.Location = new System.Drawing.Point(292, 74);
			this.ReplaceRadioButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.ReplaceRadioButton.Name = "ReplaceRadioButton";
			this.ReplaceRadioButton.Size = new System.Drawing.Size(196, 30);
			this.ReplaceRadioButton.TabIndex = 12;
			this.ReplaceRadioButton.Text = "Заменить на новые";
			this.ReplaceRadioButton.UseVisualStyleBackColor = true;
			// 
			// EditKeywordsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(641, 135);
			this.ControlBox = false;
			this.Controls.Add(this.ReplaceRadioButton);
			this.Controls.Add(this.AddRadioButton);
			this.Controls.Add(this.KeywordsPanel);
			this.Controls.Add(this.ProgressBar);
			this.Controls.Add(this.ControlPanel);
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EditKeywordsForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Правка Ключевых слов";
			this.Shown += new System.EventHandler(this.EditKeywordsFormShown);
			this.ControlPanel.ResumeLayout(false);
			this.KeywordsPanel.ResumeLayout(false);
			this.KeywordsPanel.PerformLayout();
			this.ResumeLayout(false);

		}
	}
}
