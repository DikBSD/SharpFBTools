/*
 * Создано в SharpDevelop.
 * Пользователь: VadimK
 * Дата: 13.09.2016
 * Время: 8:22
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
namespace Core.Common
{
	partial class EditSequencesForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.RadioButton ReplaceRadioButton;
		private System.Windows.Forms.RadioButton AddRadioButton;
		private System.Windows.Forms.Panel SequencesPanel;
		private System.Windows.Forms.TextBox SequencesTextBox;
		private System.Windows.Forms.Label TISequencesLabel;
		private System.Windows.Forms.ProgressBar ProgressBar;
		private System.Windows.Forms.Panel ControlPanel;
		private System.Windows.Forms.Button CancelBtn;
		private System.Windows.Forms.Button ApplyBtn;
		private System.Windows.Forms.TextBox NumberTextBox;
		private System.Windows.Forms.Label TINumberLabel;
		private System.Windows.Forms.RadioButton RemoveRadioButton;
		private System.Windows.Forms.Panel ModePanel;
		private System.Windows.Forms.Panel NumberPanel;
		
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditSequencesForm));
			this.SequencesPanel = new System.Windows.Forms.Panel();
			this.SequencesTextBox = new System.Windows.Forms.TextBox();
			this.TISequencesLabel = new System.Windows.Forms.Label();
			this.ControlPanel = new System.Windows.Forms.Panel();
			this.CancelBtn = new System.Windows.Forms.Button();
			this.ApplyBtn = new System.Windows.Forms.Button();
			this.NumberPanel = new System.Windows.Forms.Panel();
			this.NumberTextBox = new System.Windows.Forms.TextBox();
			this.TINumberLabel = new System.Windows.Forms.Label();
			this.ModePanel = new System.Windows.Forms.Panel();
			this.AddRadioButton = new System.Windows.Forms.RadioButton();
			this.RemoveRadioButton = new System.Windows.Forms.RadioButton();
			this.ReplaceRadioButton = new System.Windows.Forms.RadioButton();
			this.ProgressBar = new System.Windows.Forms.ProgressBar();
			this.SequencesPanel.SuspendLayout();
			this.ControlPanel.SuspendLayout();
			this.NumberPanel.SuspendLayout();
			this.ModePanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// SequencesPanel
			// 
			this.SequencesPanel.Controls.Add(this.SequencesTextBox);
			this.SequencesPanel.Controls.Add(this.TISequencesLabel);
			this.SequencesPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.SequencesPanel.Location = new System.Drawing.Point(0, 0);
			this.SequencesPanel.Margin = new System.Windows.Forms.Padding(2);
			this.SequencesPanel.Name = "SequencesPanel";
			this.SequencesPanel.Padding = new System.Windows.Forms.Padding(3);
			this.SequencesPanel.Size = new System.Drawing.Size(386, 48);
			this.SequencesPanel.TabIndex = 15;
			// 
			// SequencesTextBox
			// 
			this.SequencesTextBox.Dock = System.Windows.Forms.DockStyle.Top;
			this.SequencesTextBox.Location = new System.Drawing.Point(3, 22);
			this.SequencesTextBox.Name = "SequencesTextBox";
			this.SequencesTextBox.Size = new System.Drawing.Size(380, 20);
			this.SequencesTextBox.TabIndex = 1;
			this.SequencesTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.SequencesTextBoxKeyPress);
			// 
			// TISequencesLabel
			// 
			this.TISequencesLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.TISequencesLabel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.TISequencesLabel.Location = new System.Drawing.Point(3, 3);
			this.TISequencesLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.TISequencesLabel.Name = "TISequencesLabel";
			this.TISequencesLabel.Size = new System.Drawing.Size(380, 19);
			this.TISequencesLabel.TabIndex = 0;
			this.TISequencesLabel.Text = "Название Серии:";
			// 
			// ControlPanel
			// 
			this.ControlPanel.BackColor = System.Drawing.Color.DarkGray;
			this.ControlPanel.Controls.Add(this.CancelBtn);
			this.ControlPanel.Controls.Add(this.ApplyBtn);
			this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.ControlPanel.Enabled = false;
			this.ControlPanel.Location = new System.Drawing.Point(386, 0);
			this.ControlPanel.Margin = new System.Windows.Forms.Padding(2);
			this.ControlPanel.Name = "ControlPanel";
			this.ControlPanel.Size = new System.Drawing.Size(95, 166);
			this.ControlPanel.TabIndex = 13;
			// 
			// CancelBtn
			// 
			this.CancelBtn.Dock = System.Windows.Forms.DockStyle.Top;
			this.CancelBtn.Image = ((System.Drawing.Image)(resources.GetObject("CancelBtn.Image")));
			this.CancelBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.CancelBtn.Location = new System.Drawing.Point(0, 41);
			this.CancelBtn.Margin = new System.Windows.Forms.Padding(2);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(95, 39);
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
			this.ApplyBtn.Margin = new System.Windows.Forms.Padding(2);
			this.ApplyBtn.Name = "ApplyBtn";
			this.ApplyBtn.Size = new System.Drawing.Size(95, 41);
			this.ApplyBtn.TabIndex = 0;
			this.ApplyBtn.Text = "Принять";
			this.ApplyBtn.UseVisualStyleBackColor = true;
			this.ApplyBtn.Click += new System.EventHandler(this.ApplyBtnClick);
			// 
			// NumberPanel
			// 
			this.NumberPanel.Controls.Add(this.NumberTextBox);
			this.NumberPanel.Controls.Add(this.TINumberLabel);
			this.NumberPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.NumberPanel.Location = new System.Drawing.Point(0, 48);
			this.NumberPanel.Name = "NumberPanel";
			this.NumberPanel.Size = new System.Drawing.Size(386, 44);
			this.NumberPanel.TabIndex = 21;
			// 
			// NumberTextBox
			// 
			this.NumberTextBox.Dock = System.Windows.Forms.DockStyle.Top;
			this.NumberTextBox.Location = new System.Drawing.Point(0, 19);
			this.NumberTextBox.Name = "NumberTextBox";
			this.NumberTextBox.Size = new System.Drawing.Size(386, 20);
			this.NumberTextBox.TabIndex = 7;
			this.NumberTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.NumberTextBoxKeyPress);
			// 
			// TINumberLabel
			// 
			this.TINumberLabel.Dock = System.Windows.Forms.DockStyle.Top;
			this.TINumberLabel.Location = new System.Drawing.Point(0, 0);
			this.TINumberLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
			this.TINumberLabel.Name = "TINumberLabel";
			this.TINumberLabel.Size = new System.Drawing.Size(386, 19);
			this.TINumberLabel.TabIndex = 6;
			this.TINumberLabel.Text = "Номер Серии";
			// 
			// ModePanel
			// 
			this.ModePanel.Controls.Add(this.AddRadioButton);
			this.ModePanel.Controls.Add(this.RemoveRadioButton);
			this.ModePanel.Controls.Add(this.ReplaceRadioButton);
			this.ModePanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.ModePanel.Location = new System.Drawing.Point(0, 92);
			this.ModePanel.Name = "ModePanel";
			this.ModePanel.Size = new System.Drawing.Size(386, 54);
			this.ModePanel.TabIndex = 22;
			// 
			// AddRadioButton
			// 
			this.AddRadioButton.Checked = true;
			this.AddRadioButton.Location = new System.Drawing.Point(15, 3);
			this.AddRadioButton.Name = "AddRadioButton";
			this.AddRadioButton.Size = new System.Drawing.Size(176, 24);
			this.AddRadioButton.TabIndex = 16;
			this.AddRadioButton.TabStop = true;
			this.AddRadioButton.Text = "Добавить к существующим";
			this.AddRadioButton.UseVisualStyleBackColor = true;
			// 
			// RemoveRadioButton
			// 
			this.RemoveRadioButton.Location = new System.Drawing.Point(15, 28);
			this.RemoveRadioButton.Name = "RemoveRadioButton";
			this.RemoveRadioButton.Size = new System.Drawing.Size(176, 24);
			this.RemoveRadioButton.TabIndex = 18;
			this.RemoveRadioButton.Text = "Удалить все Серии";
			this.RemoveRadioButton.UseVisualStyleBackColor = true;
			this.RemoveRadioButton.CheckedChanged += new System.EventHandler(this.RemoveRadioButtonCheckedChanged);
			// 
			// ReplaceRadioButton
			// 
			this.ReplaceRadioButton.Location = new System.Drawing.Point(197, 3);
			this.ReplaceRadioButton.Name = "ReplaceRadioButton";
			this.ReplaceRadioButton.Size = new System.Drawing.Size(181, 24);
			this.ReplaceRadioButton.TabIndex = 17;
			this.ReplaceRadioButton.Text = "Заменить все Серии на новую";
			this.ReplaceRadioButton.UseVisualStyleBackColor = true;
			// 
			// ProgressBar
			// 
			this.ProgressBar.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ProgressBar.Location = new System.Drawing.Point(0, 147);
			this.ProgressBar.Margin = new System.Windows.Forms.Padding(2);
			this.ProgressBar.Name = "ProgressBar";
			this.ProgressBar.Size = new System.Drawing.Size(386, 19);
			this.ProgressBar.TabIndex = 23;
			// 
			// EditSequencesForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(481, 166);
			this.ControlBox = false;
			this.Controls.Add(this.ProgressBar);
			this.Controls.Add(this.ModePanel);
			this.Controls.Add(this.NumberPanel);
			this.Controls.Add(this.SequencesPanel);
			this.Controls.Add(this.ControlPanel);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "EditSequencesForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Правка Серии";
			this.Shown += new System.EventHandler(this.EditSequencesFormShown);
			this.SequencesPanel.ResumeLayout(false);
			this.SequencesPanel.PerformLayout();
			this.ControlPanel.ResumeLayout(false);
			this.NumberPanel.ResumeLayout(false);
			this.NumberPanel.PerformLayout();
			this.ModePanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
