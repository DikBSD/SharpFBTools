/*
 * Сделано в SharpDevelop.
 * Пользователь: VadimK
 * Дата: 01.07.2015
 * Время: 7:09
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
namespace Core.Common
{
	partial class SequenceInfoForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SequenceInfoForm));
			this.SequenceNumberTextBox = new System.Windows.Forms.TextBox();
			this.SequenceNumberLabel = new System.Windows.Forms.Label();
			this.SequenceTextBox = new System.Windows.Forms.TextBox();
			this.SequenceLabel = new System.Windows.Forms.Label();
			this.ControlPanel = new System.Windows.Forms.Panel();
			this.CancelBtn = new System.Windows.Forms.Button();
			this.ApplyBtn = new System.Windows.Forms.Button();
			this.SequenceAddPanel = new System.Windows.Forms.Panel();
			this.ControlPanel.SuspendLayout();
			this.SequenceAddPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// SequenceNumberTextBox
			// 
			this.SequenceNumberTextBox.Location = new System.Drawing.Point(331, 3);
			this.SequenceNumberTextBox.Name = "SequenceNumberTextBox";
			this.SequenceNumberTextBox.Size = new System.Drawing.Size(210, 22);
			this.SequenceNumberTextBox.TabIndex = 1;
			// 
			// SequenceNumberLabel
			// 
			this.SequenceNumberLabel.Location = new System.Drawing.Point(269, 3);
			this.SequenceNumberLabel.Name = "SequenceNumberLabel";
			this.SequenceNumberLabel.Size = new System.Drawing.Size(63, 23);
			this.SequenceNumberLabel.TabIndex = 23;
			this.SequenceNumberLabel.Text = "Номер:";
			this.SequenceNumberLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// SequenceTextBox
			// 
			this.SequenceTextBox.Location = new System.Drawing.Point(57, 3);
			this.SequenceTextBox.Name = "SequenceTextBox";
			this.SequenceTextBox.Size = new System.Drawing.Size(203, 22);
			this.SequenceTextBox.TabIndex = 0;
			// 
			// SequenceLabel
			// 
			this.SequenceLabel.Location = new System.Drawing.Point(1, 3);
			this.SequenceLabel.Name = "SequenceLabel";
			this.SequenceLabel.Size = new System.Drawing.Size(60, 23);
			this.SequenceLabel.TabIndex = 22;
			this.SequenceLabel.Text = "Серия:";
			this.SequenceLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// ControlPanel
			// 
			this.ControlPanel.Controls.Add(this.CancelBtn);
			this.ControlPanel.Controls.Add(this.ApplyBtn);
			this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.ControlPanel.Location = new System.Drawing.Point(554, 0);
			this.ControlPanel.Name = "ControlPanel";
			this.ControlPanel.Size = new System.Drawing.Size(127, 108);
			this.ControlPanel.TabIndex = 24;
			// 
			// CancelBtn
			// 
			this.CancelBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.CancelBtn.Image = ((System.Drawing.Image)(resources.GetObject("CancelBtn.Image")));
			this.CancelBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.CancelBtn.Location = new System.Drawing.Point(0, 59);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(127, 49);
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
			this.ApplyBtn.Size = new System.Drawing.Size(127, 52);
			this.ApplyBtn.TabIndex = 0;
			this.ApplyBtn.Text = "Принять";
			this.ApplyBtn.UseVisualStyleBackColor = true;
			this.ApplyBtn.Click += new System.EventHandler(this.ApplyBtnClick);
			// 
			// SequenceAddPanel
			// 
			this.SequenceAddPanel.Controls.Add(this.SequenceTextBox);
			this.SequenceAddPanel.Controls.Add(this.SequenceLabel);
			this.SequenceAddPanel.Controls.Add(this.SequenceNumberTextBox);
			this.SequenceAddPanel.Controls.Add(this.SequenceNumberLabel);
			this.SequenceAddPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.SequenceAddPanel.Location = new System.Drawing.Point(0, 0);
			this.SequenceAddPanel.Name = "SequenceAddPanel";
			this.SequenceAddPanel.Size = new System.Drawing.Size(554, 32);
			this.SequenceAddPanel.TabIndex = 0;
			// 
			// SequenceInfoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(681, 108);
			this.ControlBox = false;
			this.Controls.Add(this.SequenceAddPanel);
			this.Controls.Add(this.ControlPanel);
			this.Name = "SequenceInfoForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Серия";
			this.ControlPanel.ResumeLayout(false);
			this.SequenceAddPanel.ResumeLayout(false);
			this.SequenceAddPanel.PerformLayout();
			this.ResumeLayout(false);

		}
		private System.Windows.Forms.Panel SequenceAddPanel;
		private System.Windows.Forms.Panel ControlPanel;
		private System.Windows.Forms.Button CancelBtn;
		private System.Windows.Forms.Button ApplyBtn;
		private System.Windows.Forms.TextBox SequenceNumberTextBox;
		private System.Windows.Forms.Label SequenceNumberLabel;
		private System.Windows.Forms.TextBox SequenceTextBox;
		private System.Windows.Forms.Label SequenceLabel;
	}
}
