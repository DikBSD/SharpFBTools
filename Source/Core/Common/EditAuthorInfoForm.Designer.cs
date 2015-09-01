/*
 * Сделано в SharpDevelop.
 * Пользователь: Вадим Кузнецов (DikBSD)
 * Дата: 17.07.2015
 * Время: 7:46
 * 
 */
namespace Core.Common
{
	partial class EditAuthorInfoForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditAuthorInfoForm));
			this.ControlPanel = new System.Windows.Forms.Panel();
			this.CancelBtn = new System.Windows.Forms.Button();
			this.ApplyBtn = new System.Windows.Forms.Button();
			this.DataPanel = new System.Windows.Forms.Panel();
			this.ProgressPanel = new System.Windows.Forms.Panel();
			this.ProgressBar = new System.Windows.Forms.ProgressBar();
			this.AuthorsPanel = new System.Windows.Forms.Panel();
			this.AuthorsListView = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.AuthorsWorkPanel = new System.Windows.Forms.Panel();
			this.AuthorUpButton = new System.Windows.Forms.Button();
			this.AuthorDownButton = new System.Windows.Forms.Button();
			this.AuthorDeleteAllButton = new System.Windows.Forms.Button();
			this.AuthorDeleteButton = new System.Windows.Forms.Button();
			this.AuthorEditButton = new System.Windows.Forms.Button();
			this.AuthorsAddPanel = new System.Windows.Forms.Panel();
			this.AuthorsLabel = new System.Windows.Forms.Label();
			this.AuthorDataPanel = new System.Windows.Forms.Panel();
			this.AuthorAddButton = new System.Windows.Forms.Button();
			this.HelpLabel = new System.Windows.Forms.Label();
			this.NewIDButton = new System.Windows.Forms.Button();
			this.IDTextBox = new System.Windows.Forms.TextBox();
			this.IDLabel = new System.Windows.Forms.Label();
			this.EmailTextBox = new System.Windows.Forms.TextBox();
			this.NickNameTextBox = new System.Windows.Forms.TextBox();
			this.EmailLabel = new System.Windows.Forms.Label();
			this.NickNameLabel = new System.Windows.Forms.Label();
			this.HomePageTextBox = new System.Windows.Forms.TextBox();
			this.MiddleNameTextBox = new System.Windows.Forms.TextBox();
			this.HomePageLabel = new System.Windows.Forms.Label();
			this.MiddleNameLabel = new System.Windows.Forms.Label();
			this.FirstNameTextBox = new System.Windows.Forms.TextBox();
			this.FirstNameLabel = new System.Windows.Forms.Label();
			this.LastNameLabel = new System.Windows.Forms.Label();
			this.LastNameTextBox = new System.Windows.Forms.TextBox();
			this.AuthorBreakEditButton = new System.Windows.Forms.Button();
			this.ControlPanel.SuspendLayout();
			this.DataPanel.SuspendLayout();
			this.ProgressPanel.SuspendLayout();
			this.AuthorsPanel.SuspendLayout();
			this.AuthorsWorkPanel.SuspendLayout();
			this.AuthorsAddPanel.SuspendLayout();
			this.AuthorDataPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// ControlPanel
			// 
			this.ControlPanel.BackColor = System.Drawing.Color.DarkGray;
			this.ControlPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ControlPanel.Controls.Add(this.CancelBtn);
			this.ControlPanel.Controls.Add(this.ApplyBtn);
			this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.ControlPanel.Enabled = false;
			this.ControlPanel.Location = new System.Drawing.Point(1005, 0);
			this.ControlPanel.Name = "ControlPanel";
			this.ControlPanel.Size = new System.Drawing.Size(127, 487);
			this.ControlPanel.TabIndex = 23;
			// 
			// CancelBtn
			// 
			this.CancelBtn.Dock = System.Windows.Forms.DockStyle.Top;
			this.CancelBtn.Image = ((System.Drawing.Image)(resources.GetObject("CancelBtn.Image")));
			this.CancelBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.CancelBtn.Location = new System.Drawing.Point(0, 52);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(125, 49);
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
			this.ApplyBtn.Size = new System.Drawing.Size(125, 52);
			this.ApplyBtn.TabIndex = 0;
			this.ApplyBtn.Text = "Принять";
			this.ApplyBtn.UseVisualStyleBackColor = true;
			this.ApplyBtn.Click += new System.EventHandler(this.ApplyBtnClick);
			// 
			// DataPanel
			// 
			this.DataPanel.Controls.Add(this.ProgressPanel);
			this.DataPanel.Controls.Add(this.AuthorsPanel);
			this.DataPanel.Controls.Add(this.AuthorDataPanel);
			this.DataPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DataPanel.Location = new System.Drawing.Point(0, 0);
			this.DataPanel.Name = "DataPanel";
			this.DataPanel.Size = new System.Drawing.Size(1005, 487);
			this.DataPanel.TabIndex = 24;
			// 
			// ProgressPanel
			// 
			this.ProgressPanel.Controls.Add(this.ProgressBar);
			this.ProgressPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ProgressPanel.Location = new System.Drawing.Point(0, 443);
			this.ProgressPanel.Name = "ProgressPanel";
			this.ProgressPanel.Size = new System.Drawing.Size(1005, 44);
			this.ProgressPanel.TabIndex = 27;
			// 
			// ProgressBar
			// 
			this.ProgressBar.Location = new System.Drawing.Point(10, 10);
			this.ProgressBar.Name = "ProgressBar";
			this.ProgressBar.Size = new System.Drawing.Size(983, 23);
			this.ProgressBar.TabIndex = 8;
			// 
			// AuthorsPanel
			// 
			this.AuthorsPanel.Controls.Add(this.AuthorsListView);
			this.AuthorsPanel.Controls.Add(this.AuthorsWorkPanel);
			this.AuthorsPanel.Controls.Add(this.AuthorsAddPanel);
			this.AuthorsPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.AuthorsPanel.Location = new System.Drawing.Point(0, 133);
			this.AuthorsPanel.Name = "AuthorsPanel";
			this.AuthorsPanel.Size = new System.Drawing.Size(1005, 304);
			this.AuthorsPanel.TabIndex = 8;
			// 
			// AuthorsListView
			// 
			this.AuthorsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader1,
			this.columnHeader2,
			this.columnHeader3,
			this.columnHeader4,
			this.columnHeader5,
			this.columnHeader6,
			this.columnHeader7});
			this.AuthorsListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.AuthorsListView.FullRowSelect = true;
			this.AuthorsListView.GridLines = true;
			this.AuthorsListView.HideSelection = false;
			this.AuthorsListView.Location = new System.Drawing.Point(0, 25);
			this.AuthorsListView.Name = "AuthorsListView";
			this.AuthorsListView.Size = new System.Drawing.Size(930, 279);
			this.AuthorsListView.TabIndex = 87;
			this.AuthorsListView.UseCompatibleStateImageBehavior = false;
			this.AuthorsListView.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "Фамилия";
			this.columnHeader1.Width = 200;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Имя";
			this.columnHeader2.Width = 150;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Отчество";
			this.columnHeader3.Width = 200;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "Ник";
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "Web";
			this.columnHeader5.Width = 80;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "email";
			this.columnHeader6.Width = 80;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "id";
			this.columnHeader7.Width = 150;
			// 
			// AuthorsWorkPanel
			// 
			this.AuthorsWorkPanel.Controls.Add(this.AuthorUpButton);
			this.AuthorsWorkPanel.Controls.Add(this.AuthorDownButton);
			this.AuthorsWorkPanel.Controls.Add(this.AuthorDeleteAllButton);
			this.AuthorsWorkPanel.Controls.Add(this.AuthorDeleteButton);
			this.AuthorsWorkPanel.Controls.Add(this.AuthorEditButton);
			this.AuthorsWorkPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.AuthorsWorkPanel.Enabled = false;
			this.AuthorsWorkPanel.Location = new System.Drawing.Point(930, 25);
			this.AuthorsWorkPanel.Name = "AuthorsWorkPanel";
			this.AuthorsWorkPanel.Size = new System.Drawing.Size(75, 279);
			this.AuthorsWorkPanel.TabIndex = 86;
			// 
			// AuthorUpButton
			// 
			this.AuthorUpButton.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.AuthorUpButton.Image = ((System.Drawing.Image)(resources.GetObject("AuthorUpButton.Image")));
			this.AuthorUpButton.Location = new System.Drawing.Point(0, 208);
			this.AuthorUpButton.Name = "AuthorUpButton";
			this.AuthorUpButton.Size = new System.Drawing.Size(75, 36);
			this.AuthorUpButton.TabIndex = 5;
			this.AuthorUpButton.UseVisualStyleBackColor = true;
			this.AuthorUpButton.Click += new System.EventHandler(this.AuthorUpButtonClick);
			// 
			// AuthorDownButton
			// 
			this.AuthorDownButton.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.AuthorDownButton.Image = ((System.Drawing.Image)(resources.GetObject("AuthorDownButton.Image")));
			this.AuthorDownButton.Location = new System.Drawing.Point(0, 244);
			this.AuthorDownButton.Name = "AuthorDownButton";
			this.AuthorDownButton.Size = new System.Drawing.Size(75, 35);
			this.AuthorDownButton.TabIndex = 4;
			this.AuthorDownButton.UseVisualStyleBackColor = true;
			this.AuthorDownButton.Click += new System.EventHandler(this.AuthorDownButtonClick);
			// 
			// AuthorDeleteAllButton
			// 
			this.AuthorDeleteAllButton.Dock = System.Windows.Forms.DockStyle.Top;
			this.AuthorDeleteAllButton.Image = ((System.Drawing.Image)(resources.GetObject("AuthorDeleteAllButton.Image")));
			this.AuthorDeleteAllButton.Location = new System.Drawing.Point(0, 82);
			this.AuthorDeleteAllButton.Name = "AuthorDeleteAllButton";
			this.AuthorDeleteAllButton.Size = new System.Drawing.Size(75, 36);
			this.AuthorDeleteAllButton.TabIndex = 3;
			this.AuthorDeleteAllButton.UseVisualStyleBackColor = true;
			this.AuthorDeleteAllButton.Click += new System.EventHandler(this.AuthorDeleteAllButtonClick);
			// 
			// AuthorDeleteButton
			// 
			this.AuthorDeleteButton.Dock = System.Windows.Forms.DockStyle.Top;
			this.AuthorDeleteButton.Image = ((System.Drawing.Image)(resources.GetObject("AuthorDeleteButton.Image")));
			this.AuthorDeleteButton.Location = new System.Drawing.Point(0, 47);
			this.AuthorDeleteButton.Name = "AuthorDeleteButton";
			this.AuthorDeleteButton.Size = new System.Drawing.Size(75, 35);
			this.AuthorDeleteButton.TabIndex = 2;
			this.AuthorDeleteButton.UseVisualStyleBackColor = true;
			this.AuthorDeleteButton.Click += new System.EventHandler(this.AuthorDeleteButtonClick);
			// 
			// AuthorEditButton
			// 
			this.AuthorEditButton.Dock = System.Windows.Forms.DockStyle.Top;
			this.AuthorEditButton.Image = ((System.Drawing.Image)(resources.GetObject("AuthorEditButton.Image")));
			this.AuthorEditButton.Location = new System.Drawing.Point(0, 0);
			this.AuthorEditButton.Name = "AuthorEditButton";
			this.AuthorEditButton.Size = new System.Drawing.Size(75, 47);
			this.AuthorEditButton.TabIndex = 0;
			this.AuthorEditButton.UseVisualStyleBackColor = true;
			this.AuthorEditButton.Click += new System.EventHandler(this.AuthorEditButtonClick);
			// 
			// AuthorsAddPanel
			// 
			this.AuthorsAddPanel.Controls.Add(this.AuthorsLabel);
			this.AuthorsAddPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.AuthorsAddPanel.Location = new System.Drawing.Point(0, 0);
			this.AuthorsAddPanel.Margin = new System.Windows.Forms.Padding(4);
			this.AuthorsAddPanel.Name = "AuthorsAddPanel";
			this.AuthorsAddPanel.Size = new System.Drawing.Size(1005, 25);
			this.AuthorsAddPanel.TabIndex = 80;
			// 
			// AuthorsLabel
			// 
			this.AuthorsLabel.BackColor = System.Drawing.Color.LightGray;
			this.AuthorsLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.AuthorsLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.AuthorsLabel.Location = new System.Drawing.Point(0, 0);
			this.AuthorsLabel.Name = "AuthorsLabel";
			this.AuthorsLabel.Size = new System.Drawing.Size(1005, 25);
			this.AuthorsLabel.TabIndex = 79;
			this.AuthorsLabel.Text = "Авторы книг:";
			this.AuthorsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// AuthorDataPanel
			// 
			this.AuthorDataPanel.Controls.Add(this.AuthorBreakEditButton);
			this.AuthorDataPanel.Controls.Add(this.AuthorAddButton);
			this.AuthorDataPanel.Controls.Add(this.HelpLabel);
			this.AuthorDataPanel.Controls.Add(this.NewIDButton);
			this.AuthorDataPanel.Controls.Add(this.IDTextBox);
			this.AuthorDataPanel.Controls.Add(this.IDLabel);
			this.AuthorDataPanel.Controls.Add(this.EmailTextBox);
			this.AuthorDataPanel.Controls.Add(this.NickNameTextBox);
			this.AuthorDataPanel.Controls.Add(this.EmailLabel);
			this.AuthorDataPanel.Controls.Add(this.NickNameLabel);
			this.AuthorDataPanel.Controls.Add(this.HomePageTextBox);
			this.AuthorDataPanel.Controls.Add(this.MiddleNameTextBox);
			this.AuthorDataPanel.Controls.Add(this.HomePageLabel);
			this.AuthorDataPanel.Controls.Add(this.MiddleNameLabel);
			this.AuthorDataPanel.Controls.Add(this.FirstNameTextBox);
			this.AuthorDataPanel.Controls.Add(this.FirstNameLabel);
			this.AuthorDataPanel.Controls.Add(this.LastNameLabel);
			this.AuthorDataPanel.Controls.Add(this.LastNameTextBox);
			this.AuthorDataPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.AuthorDataPanel.Enabled = false;
			this.AuthorDataPanel.Location = new System.Drawing.Point(0, 0);
			this.AuthorDataPanel.Name = "AuthorDataPanel";
			this.AuthorDataPanel.Size = new System.Drawing.Size(1005, 133);
			this.AuthorDataPanel.TabIndex = 1;
			// 
			// AuthorAddButton
			// 
			this.AuthorAddButton.Image = ((System.Drawing.Image)(resources.GetObject("AuthorAddButton.Image")));
			this.AuthorAddButton.Location = new System.Drawing.Point(833, 3);
			this.AuthorAddButton.Name = "AuthorAddButton";
			this.AuthorAddButton.Size = new System.Drawing.Size(163, 50);
			this.AuthorAddButton.TabIndex = 84;
			this.AuthorAddButton.UseVisualStyleBackColor = true;
			this.AuthorAddButton.Click += new System.EventHandler(this.AuthorAddButtonClick);
			// 
			// HelpLabel
			// 
			this.HelpLabel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.HelpLabel.Location = new System.Drawing.Point(0, 110);
			this.HelpLabel.Name = "HelpLabel";
			this.HelpLabel.Size = new System.Drawing.Size(1005, 23);
			this.HelpLabel.TabIndex = 39;
			this.HelpLabel.Text = "Подсказка: Если сайтов (email) несколько, то они перечисляются через запятую ( , " +
	") или точку с запятой ( ; )";
			// 
			// NewIDButton
			// 
			this.NewIDButton.Location = new System.Drawing.Point(733, 29);
			this.NewIDButton.Name = "NewIDButton";
			this.NewIDButton.Size = new System.Drawing.Size(87, 25);
			this.NewIDButton.TabIndex = 29;
			this.NewIDButton.Text = "Новый ID";
			this.NewIDButton.UseVisualStyleBackColor = true;
			this.NewIDButton.Click += new System.EventHandler(this.NewIDButtonClick);
			// 
			// IDTextBox
			// 
			this.IDTextBox.Location = new System.Drawing.Point(322, 30);
			this.IDTextBox.Name = "IDTextBox";
			this.IDTextBox.Size = new System.Drawing.Size(405, 22);
			this.IDTextBox.TabIndex = 28;
			this.IDTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxKeyDown);
			// 
			// IDLabel
			// 
			this.IDLabel.Location = new System.Drawing.Point(295, 29);
			this.IDLabel.Name = "IDLabel";
			this.IDLabel.Size = new System.Drawing.Size(25, 23);
			this.IDLabel.TabIndex = 38;
			this.IDLabel.Text = "ID:";
			this.IDLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// EmailTextBox
			// 
			this.EmailTextBox.Location = new System.Drawing.Point(81, 85);
			this.EmailTextBox.Name = "EmailTextBox";
			this.EmailTextBox.Size = new System.Drawing.Size(739, 22);
			this.EmailTextBox.TabIndex = 32;
			this.EmailTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxKeyDown);
			// 
			// NickNameTextBox
			// 
			this.NickNameTextBox.Location = new System.Drawing.Point(83, 30);
			this.NickNameTextBox.Name = "NickNameTextBox";
			this.NickNameTextBox.Size = new System.Drawing.Size(203, 22);
			this.NickNameTextBox.TabIndex = 27;
			this.NickNameTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxKeyDown);
			// 
			// EmailLabel
			// 
			this.EmailLabel.Location = new System.Drawing.Point(26, 86);
			this.EmailLabel.Name = "EmailLabel";
			this.EmailLabel.Size = new System.Drawing.Size(48, 23);
			this.EmailLabel.TabIndex = 33;
			this.EmailLabel.Text = "Email:";
			this.EmailLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// NickNameLabel
			// 
			this.NickNameLabel.Location = new System.Drawing.Point(34, 29);
			this.NickNameLabel.Name = "NickNameLabel";
			this.NickNameLabel.Size = new System.Drawing.Size(38, 23);
			this.NickNameLabel.TabIndex = 37;
			this.NickNameLabel.Text = "Ник:";
			this.NickNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// HomePageTextBox
			// 
			this.HomePageTextBox.Location = new System.Drawing.Point(81, 59);
			this.HomePageTextBox.Name = "HomePageTextBox";
			this.HomePageTextBox.Size = new System.Drawing.Size(739, 22);
			this.HomePageTextBox.TabIndex = 31;
			this.HomePageTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxKeyDown);
			// 
			// MiddleNameTextBox
			// 
			this.MiddleNameTextBox.Location = new System.Drawing.Point(610, 5);
			this.MiddleNameTextBox.Name = "MiddleNameTextBox";
			this.MiddleNameTextBox.Size = new System.Drawing.Size(210, 22);
			this.MiddleNameTextBox.TabIndex = 26;
			this.MiddleNameTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxKeyDown);
			// 
			// HomePageLabel
			// 
			this.HomePageLabel.Location = new System.Drawing.Point(26, 60);
			this.HomePageLabel.Name = "HomePageLabel";
			this.HomePageLabel.Size = new System.Drawing.Size(48, 23);
			this.HomePageLabel.TabIndex = 30;
			this.HomePageLabel.Text = "Web:";
			this.HomePageLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
			// 
			// MiddleNameLabel
			// 
			this.MiddleNameLabel.Location = new System.Drawing.Point(536, 5);
			this.MiddleNameLabel.Name = "MiddleNameLabel";
			this.MiddleNameLabel.Size = new System.Drawing.Size(75, 23);
			this.MiddleNameLabel.TabIndex = 36;
			this.MiddleNameLabel.Text = "Отчество:";
			this.MiddleNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// FirstNameTextBox
			// 
			this.FirstNameTextBox.Location = new System.Drawing.Point(322, 5);
			this.FirstNameTextBox.Name = "FirstNameTextBox";
			this.FirstNameTextBox.Size = new System.Drawing.Size(203, 22);
			this.FirstNameTextBox.TabIndex = 25;
			this.FirstNameTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxKeyDown);
			// 
			// FirstNameLabel
			// 
			this.FirstNameLabel.Location = new System.Drawing.Point(284, 5);
			this.FirstNameLabel.Name = "FirstNameLabel";
			this.FirstNameLabel.Size = new System.Drawing.Size(42, 23);
			this.FirstNameLabel.TabIndex = 35;
			this.FirstNameLabel.Text = "Имя:";
			this.FirstNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// LastNameLabel
			// 
			this.LastNameLabel.ForeColor = System.Drawing.Color.Red;
			this.LastNameLabel.Location = new System.Drawing.Point(0, 5);
			this.LastNameLabel.Name = "LastNameLabel";
			this.LastNameLabel.Size = new System.Drawing.Size(80, 23);
			this.LastNameLabel.TabIndex = 34;
			this.LastNameLabel.Text = "Фамилия:";
			this.LastNameLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// LastNameTextBox
			// 
			this.LastNameTextBox.Location = new System.Drawing.Point(83, 5);
			this.LastNameTextBox.Name = "LastNameTextBox";
			this.LastNameTextBox.Size = new System.Drawing.Size(203, 22);
			this.LastNameTextBox.TabIndex = 24;
			this.LastNameTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBoxKeyDown);
			// 
			// AuthorBreakEditButton
			// 
			this.AuthorBreakEditButton.Location = new System.Drawing.Point(833, 59);
			this.AuthorBreakEditButton.Name = "AuthorBreakEditButton";
			this.AuthorBreakEditButton.Size = new System.Drawing.Size(163, 48);
			this.AuthorBreakEditButton.TabIndex = 85;
			this.AuthorBreakEditButton.Text = "Прервать правку Автора";
			this.AuthorBreakEditButton.UseVisualStyleBackColor = true;
			this.AuthorBreakEditButton.Visible = false;
			this.AuthorBreakEditButton.Click += new System.EventHandler(this.AuthorBreakEditButtonClick);
			// 
			// EditAuthorInfoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1132, 487);
			this.ControlBox = false;
			this.Controls.Add(this.DataPanel);
			this.Controls.Add(this.ControlPanel);
			this.Name = "EditAuthorInfoForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Правка данных Авторов";
			this.ControlPanel.ResumeLayout(false);
			this.DataPanel.ResumeLayout(false);
			this.ProgressPanel.ResumeLayout(false);
			this.AuthorsPanel.ResumeLayout(false);
			this.AuthorsWorkPanel.ResumeLayout(false);
			this.AuthorsAddPanel.ResumeLayout(false);
			this.AuthorDataPanel.ResumeLayout(false);
			this.AuthorDataPanel.PerformLayout();
			this.ResumeLayout(false);

		}
		private System.Windows.Forms.Button AuthorBreakEditButton;
		private System.Windows.Forms.Panel ProgressPanel;
		private System.Windows.Forms.ProgressBar ProgressBar;
		private System.Windows.Forms.Button AuthorUpButton;
		private System.Windows.Forms.Button AuthorDownButton;
		private System.Windows.Forms.Panel AuthorsPanel;
		private System.Windows.Forms.ListView AuthorsListView;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.Panel AuthorsWorkPanel;
		private System.Windows.Forms.Button AuthorDeleteAllButton;
		private System.Windows.Forms.Button AuthorDeleteButton;
		private System.Windows.Forms.Button AuthorEditButton;
		private System.Windows.Forms.Panel AuthorsAddPanel;
		private System.Windows.Forms.Button AuthorAddButton;
		private System.Windows.Forms.Label AuthorsLabel;
		private System.Windows.Forms.Label HelpLabel;
		private System.Windows.Forms.Button NewIDButton;
		private System.Windows.Forms.TextBox IDTextBox;
		private System.Windows.Forms.Label IDLabel;
		private System.Windows.Forms.TextBox EmailTextBox;
		private System.Windows.Forms.TextBox NickNameTextBox;
		private System.Windows.Forms.Label EmailLabel;
		private System.Windows.Forms.Label NickNameLabel;
		private System.Windows.Forms.TextBox HomePageTextBox;
		private System.Windows.Forms.TextBox MiddleNameTextBox;
		private System.Windows.Forms.Label HomePageLabel;
		private System.Windows.Forms.Label MiddleNameLabel;
		private System.Windows.Forms.TextBox FirstNameTextBox;
		private System.Windows.Forms.Label FirstNameLabel;
		private System.Windows.Forms.Label LastNameLabel;
		private System.Windows.Forms.TextBox LastNameTextBox;
		private System.Windows.Forms.Panel ControlPanel;
		private System.Windows.Forms.Button CancelBtn;
		private System.Windows.Forms.Button ApplyBtn;
		private System.Windows.Forms.Panel DataPanel;
		private System.Windows.Forms.Panel AuthorDataPanel;
	}
}
