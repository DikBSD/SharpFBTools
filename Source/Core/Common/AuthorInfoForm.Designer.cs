/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 30.06.2015
 * Время: 12:29
 * 
 */
namespace Core.Common
{
	partial class AuthorInfoForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AuthorInfoForm));
			this.IDTextBox = new System.Windows.Forms.TextBox();
			this.IDLabel = new System.Windows.Forms.Label();
			this.NickNameTextBox = new System.Windows.Forms.TextBox();
			this.NickNameLabel = new System.Windows.Forms.Label();
			this.MiddleNameTextBox = new System.Windows.Forms.TextBox();
			this.MiddleNameLabel = new System.Windows.Forms.Label();
			this.FirstNameTextBox = new System.Windows.Forms.TextBox();
			this.FirstNameLabel = new System.Windows.Forms.Label();
			this.LastNameTextBox = new System.Windows.Forms.TextBox();
			this.LastNameLabel = new System.Windows.Forms.Label();
			this.HomePageLabel = new System.Windows.Forms.Label();
			this.HomePageTextBox = new System.Windows.Forms.TextBox();
			this.EmailTextBox = new System.Windows.Forms.TextBox();
			this.EmailLabel = new System.Windows.Forms.Label();
			this.NewIDButton = new System.Windows.Forms.Button();
			this.ControlPanel = new System.Windows.Forms.Panel();
			this.CancelBtn = new System.Windows.Forms.Button();
			this.ApplyBtn = new System.Windows.Forms.Button();
			this.HelpLabel = new System.Windows.Forms.Label();
			this.ControlPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// IDTextBox
			// 
			this.IDTextBox.Location = new System.Drawing.Point(322, 34);
			this.IDTextBox.Name = "IDTextBox";
			this.IDTextBox.Size = new System.Drawing.Size(405, 22);
			this.IDTextBox.TabIndex = 4;
			// 
			// IDLabel
			// 
			this.IDLabel.Location = new System.Drawing.Point(295, 33);
			this.IDLabel.Name = "IDLabel";
			this.IDLabel.Size = new System.Drawing.Size(25, 23);
			this.IDLabel.TabIndex = 21;
			this.IDLabel.Text = "ID:";
			this.IDLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// NickNameTextBox
			// 
			this.NickNameTextBox.Location = new System.Drawing.Point(83, 34);
			this.NickNameTextBox.Name = "NickNameTextBox";
			this.NickNameTextBox.Size = new System.Drawing.Size(203, 22);
			this.NickNameTextBox.TabIndex = 3;
			// 
			// NickNameLabel
			// 
			this.NickNameLabel.Location = new System.Drawing.Point(34, 33);
			this.NickNameLabel.Name = "NickNameLabel";
			this.NickNameLabel.Size = new System.Drawing.Size(38, 23);
			this.NickNameLabel.TabIndex = 20;
			this.NickNameLabel.Text = "Ник:";
			this.NickNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// MiddleNameTextBox
			// 
			this.MiddleNameTextBox.Location = new System.Drawing.Point(610, 9);
			this.MiddleNameTextBox.Name = "MiddleNameTextBox";
			this.MiddleNameTextBox.Size = new System.Drawing.Size(210, 22);
			this.MiddleNameTextBox.TabIndex = 2;
			// 
			// MiddleNameLabel
			// 
			this.MiddleNameLabel.Location = new System.Drawing.Point(536, 9);
			this.MiddleNameLabel.Name = "MiddleNameLabel";
			this.MiddleNameLabel.Size = new System.Drawing.Size(75, 23);
			this.MiddleNameLabel.TabIndex = 19;
			this.MiddleNameLabel.Text = "Отчество:";
			this.MiddleNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FirstNameTextBox
			// 
			this.FirstNameTextBox.Location = new System.Drawing.Point(322, 9);
			this.FirstNameTextBox.Name = "FirstNameTextBox";
			this.FirstNameTextBox.Size = new System.Drawing.Size(203, 22);
			this.FirstNameTextBox.TabIndex = 1;
			// 
			// FirstNameLabel
			// 
			this.FirstNameLabel.Location = new System.Drawing.Point(284, 9);
			this.FirstNameLabel.Name = "FirstNameLabel";
			this.FirstNameLabel.Size = new System.Drawing.Size(42, 23);
			this.FirstNameLabel.TabIndex = 18;
			this.FirstNameLabel.Text = "Имя:";
			this.FirstNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// LastNameTextBox
			// 
			this.LastNameTextBox.Location = new System.Drawing.Point(83, 9);
			this.LastNameTextBox.Name = "LastNameTextBox";
			this.LastNameTextBox.Size = new System.Drawing.Size(203, 22);
			this.LastNameTextBox.TabIndex = 0;
			// 
			// LastNameLabel
			// 
			this.LastNameLabel.ForeColor = System.Drawing.Color.Red;
			this.LastNameLabel.Location = new System.Drawing.Point(0, 9);
			this.LastNameLabel.Name = "LastNameLabel";
			this.LastNameLabel.Size = new System.Drawing.Size(80, 23);
			this.LastNameLabel.TabIndex = 16;
			this.LastNameLabel.Text = "Фамилия:";
			this.LastNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// HomePageLabel
			// 
			this.HomePageLabel.Location = new System.Drawing.Point(26, 64);
			this.HomePageLabel.Name = "HomePageLabel";
			this.HomePageLabel.Size = new System.Drawing.Size(48, 23);
			this.HomePageLabel.TabIndex = 5;
			this.HomePageLabel.Text = "Web:";
			this.HomePageLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// HomePageTextBox
			// 
			this.HomePageTextBox.Location = new System.Drawing.Point(81, 63);
			this.HomePageTextBox.Name = "HomePageTextBox";
			this.HomePageTextBox.Size = new System.Drawing.Size(739, 22);
			this.HomePageTextBox.TabIndex = 6;
			// 
			// EmailTextBox
			// 
			this.EmailTextBox.Location = new System.Drawing.Point(81, 89);
			this.EmailTextBox.Name = "EmailTextBox";
			this.EmailTextBox.Size = new System.Drawing.Size(739, 22);
			this.EmailTextBox.TabIndex = 7;
			// 
			// EmailLabel
			// 
			this.EmailLabel.Location = new System.Drawing.Point(26, 90);
			this.EmailLabel.Name = "EmailLabel";
			this.EmailLabel.Size = new System.Drawing.Size(48, 23);
			this.EmailLabel.TabIndex = 7;
			this.EmailLabel.Text = "Email:";
			this.EmailLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// NewIDButton
			// 
			this.NewIDButton.Location = new System.Drawing.Point(733, 33);
			this.NewIDButton.Name = "NewIDButton";
			this.NewIDButton.Size = new System.Drawing.Size(87, 25);
			this.NewIDButton.TabIndex = 5;
			this.NewIDButton.Text = "Новый ID";
			this.NewIDButton.UseVisualStyleBackColor = true;
			this.NewIDButton.Click += new System.EventHandler(this.NewIDButtonClick);
			// 
			// ControlPanel
			// 
			this.ControlPanel.Controls.Add(this.CancelBtn);
			this.ControlPanel.Controls.Add(this.ApplyBtn);
			this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.ControlPanel.Location = new System.Drawing.Point(828, 0);
			this.ControlPanel.Name = "ControlPanel";
			this.ControlPanel.Size = new System.Drawing.Size(127, 138);
			this.ControlPanel.TabIndex = 22;
			// 
			// CancelBtn
			// 
			this.CancelBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.CancelBtn.Image = ((System.Drawing.Image)(resources.GetObject("CancelBtn.Image")));
			this.CancelBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.CancelBtn.Location = new System.Drawing.Point(0, 89);
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
			// HelpLabel
			// 
			this.HelpLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.HelpLabel.Location = new System.Drawing.Point(0, 115);
			this.HelpLabel.Name = "HelpLabel";
			this.HelpLabel.Size = new System.Drawing.Size(828, 23);
			this.HelpLabel.TabIndex = 23;
			this.HelpLabel.Text = "Подсказка: Если сайтов (email) несколько, то они перечисляются через запятую ( , " +
	") или точку с запятой ( ; )";
			// 
			// AuthorInfoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(955, 138);
			this.ControlBox = false;
			this.Controls.Add(this.HelpLabel);
			this.Controls.Add(this.ControlPanel);
			this.Controls.Add(this.NewIDButton);
			this.Controls.Add(this.IDTextBox);
			this.Controls.Add(this.IDLabel);
			this.Controls.Add(this.EmailTextBox);
			this.Controls.Add(this.NickNameTextBox);
			this.Controls.Add(this.EmailLabel);
			this.Controls.Add(this.NickNameLabel);
			this.Controls.Add(this.HomePageTextBox);
			this.Controls.Add(this.MiddleNameTextBox);
			this.Controls.Add(this.HomePageLabel);
			this.Controls.Add(this.MiddleNameLabel);
			this.Controls.Add(this.FirstNameTextBox);
			this.Controls.Add(this.FirstNameLabel);
			this.Controls.Add(this.LastNameLabel);
			this.Controls.Add(this.LastNameTextBox);
			this.Name = "AuthorInfoForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Автор";
			this.ControlPanel.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		private System.Windows.Forms.Button NewIDButton;
		private System.Windows.Forms.TextBox EmailTextBox;
		private System.Windows.Forms.Label EmailLabel;
		private System.Windows.Forms.Label HelpLabel;
		private System.Windows.Forms.Label HomePageLabel;
		private System.Windows.Forms.TextBox HomePageTextBox;
		private System.Windows.Forms.TextBox IDTextBox;
		private System.Windows.Forms.Label IDLabel;
		private System.Windows.Forms.TextBox NickNameTextBox;
		private System.Windows.Forms.Label NickNameLabel;
		private System.Windows.Forms.TextBox MiddleNameTextBox;
		private System.Windows.Forms.Label MiddleNameLabel;
		private System.Windows.Forms.Label LastNameLabel;
		private System.Windows.Forms.TextBox LastNameTextBox;
		private System.Windows.Forms.Label FirstNameLabel;
		private System.Windows.Forms.TextBox FirstNameTextBox;
		private System.Windows.Forms.Panel ControlPanel;
		private System.Windows.Forms.Button CancelBtn;
		private System.Windows.Forms.Button ApplyBtn;
	}
}
