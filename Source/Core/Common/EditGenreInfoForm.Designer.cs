/*
 * Сделано в SharpDevelop.
 * Пользователь: Вадим Кузнецов (DikBSD)
 * Дата: 21.07.2015
 * Время: 8:37
 * 
 */
namespace Core.Common
{
	partial class EditGenreInfoForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditGenreInfoForm));
			this.GenrePanel = new System.Windows.Forms.Panel();
			this.GenresListView = new System.Windows.Forms.ListView();
			this.columnHeaderGenre = new System.Windows.Forms.ColumnHeader();
			this.columnHeaderMath = new System.Windows.Forms.ColumnHeader();
			this.GenreWorkPanel = new System.Windows.Forms.Panel();
			this.GenreUpButton = new System.Windows.Forms.Button();
			this.GenreDownButton = new System.Windows.Forms.Button();
			this.GenreDeleteAllButton = new System.Windows.Forms.Button();
			this.GenreDeleteButton = new System.Windows.Forms.Button();
			this.GenresSchemePanel = new System.Windows.Forms.Panel();
			this.GroupComboBox = new System.Windows.Forms.ComboBox();
			this.GroupLabel = new System.Windows.Forms.Label();
			this.GenreAddButton = new System.Windows.Forms.Button();
			this.MatchMaskedTextBox = new System.Windows.Forms.MaskedTextBox();
			this.MathLabel = new System.Windows.Forms.Label();
			this.GenresComboBox = new System.Windows.Forms.ComboBox();
			this.GenreLabel = new System.Windows.Forms.Label();
			this.ControlPanel = new System.Windows.Forms.Panel();
			this.CancelBtn = new System.Windows.Forms.Button();
			this.ApplyBtn = new System.Windows.Forms.Button();
			this.GenresPanel = new System.Windows.Forms.Panel();
			this.ProgressPanel = new System.Windows.Forms.Panel();
			this.ProgressBar = new System.Windows.Forms.ProgressBar();
			this.GenrePanel.SuspendLayout();
			this.GenreWorkPanel.SuspendLayout();
			this.GenresSchemePanel.SuspendLayout();
			this.ControlPanel.SuspendLayout();
			this.GenresPanel.SuspendLayout();
			this.ProgressPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// GenrePanel
			// 
			this.GenrePanel.Controls.Add(this.GenresListView);
			this.GenrePanel.Controls.Add(this.GenreWorkPanel);
			this.GenrePanel.Controls.Add(this.GenresSchemePanel);
			this.GenrePanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GenrePanel.Location = new System.Drawing.Point(0, 0);
			this.GenrePanel.Name = "GenrePanel";
			this.GenrePanel.Size = new System.Drawing.Size(789, 303);
			this.GenrePanel.TabIndex = 5;
			// 
			// GenresListView
			// 
			this.GenresListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeaderGenre,
			this.columnHeaderMath});
			this.GenresListView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.GenresListView.FullRowSelect = true;
			this.GenresListView.GridLines = true;
			this.GenresListView.HideSelection = false;
			this.GenresListView.Location = new System.Drawing.Point(0, 77);
			this.GenresListView.Name = "GenresListView";
			this.GenresListView.Size = new System.Drawing.Size(714, 226);
			this.GenresListView.TabIndex = 3;
			this.GenresListView.UseCompatibleStateImageBehavior = false;
			this.GenresListView.View = System.Windows.Forms.View.Details;
			this.GenresListView.SelectedIndexChanged += new System.EventHandler(this.GenresListViewSelectedIndexChanged);
			// 
			// columnHeaderGenre
			// 
			this.columnHeaderGenre.Text = "Жанр";
			this.columnHeaderGenre.Width = 500;
			// 
			// columnHeaderMath
			// 
			this.columnHeaderMath.Text = "Соответствие, %";
			this.columnHeaderMath.Width = 129;
			// 
			// GenreWorkPanel
			// 
			this.GenreWorkPanel.Controls.Add(this.GenreUpButton);
			this.GenreWorkPanel.Controls.Add(this.GenreDownButton);
			this.GenreWorkPanel.Controls.Add(this.GenreDeleteAllButton);
			this.GenreWorkPanel.Controls.Add(this.GenreDeleteButton);
			this.GenreWorkPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.GenreWorkPanel.Enabled = false;
			this.GenreWorkPanel.Location = new System.Drawing.Point(714, 77);
			this.GenreWorkPanel.Name = "GenreWorkPanel";
			this.GenreWorkPanel.Size = new System.Drawing.Size(75, 226);
			this.GenreWorkPanel.TabIndex = 84;
			// 
			// GenreUpButton
			// 
			this.GenreUpButton.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.GenreUpButton.Image = ((System.Drawing.Image)(resources.GetObject("GenreUpButton.Image")));
			this.GenreUpButton.Location = new System.Drawing.Point(0, 155);
			this.GenreUpButton.Name = "GenreUpButton";
			this.GenreUpButton.Size = new System.Drawing.Size(75, 36);
			this.GenreUpButton.TabIndex = 6;
			this.GenreUpButton.UseVisualStyleBackColor = true;
			this.GenreUpButton.Click += new System.EventHandler(this.GenreUpButtonClick);
			// 
			// GenreDownButton
			// 
			this.GenreDownButton.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.GenreDownButton.Image = ((System.Drawing.Image)(resources.GetObject("GenreDownButton.Image")));
			this.GenreDownButton.Location = new System.Drawing.Point(0, 191);
			this.GenreDownButton.Name = "GenreDownButton";
			this.GenreDownButton.Size = new System.Drawing.Size(75, 35);
			this.GenreDownButton.TabIndex = 7;
			this.GenreDownButton.UseVisualStyleBackColor = true;
			this.GenreDownButton.Click += new System.EventHandler(this.GenreDownButtonClick);
			// 
			// GenreDeleteAllButton
			// 
			this.GenreDeleteAllButton.Dock = System.Windows.Forms.DockStyle.Top;
			this.GenreDeleteAllButton.Image = ((System.Drawing.Image)(resources.GetObject("GenreDeleteAllButton.Image")));
			this.GenreDeleteAllButton.Location = new System.Drawing.Point(0, 35);
			this.GenreDeleteAllButton.Name = "GenreDeleteAllButton";
			this.GenreDeleteAllButton.Size = new System.Drawing.Size(75, 35);
			this.GenreDeleteAllButton.TabIndex = 5;
			this.GenreDeleteAllButton.UseVisualStyleBackColor = true;
			this.GenreDeleteAllButton.Click += new System.EventHandler(this.GenreDeleteAllButtonClick);
			// 
			// GenreDeleteButton
			// 
			this.GenreDeleteButton.Dock = System.Windows.Forms.DockStyle.Top;
			this.GenreDeleteButton.Image = ((System.Drawing.Image)(resources.GetObject("GenreDeleteButton.Image")));
			this.GenreDeleteButton.Location = new System.Drawing.Point(0, 0);
			this.GenreDeleteButton.Name = "GenreDeleteButton";
			this.GenreDeleteButton.Size = new System.Drawing.Size(75, 35);
			this.GenreDeleteButton.TabIndex = 4;
			this.GenreDeleteButton.UseVisualStyleBackColor = true;
			this.GenreDeleteButton.Click += new System.EventHandler(this.GenreDeleteButtonClick);
			// 
			// GenresSchemePanel
			// 
			this.GenresSchemePanel.Controls.Add(this.GroupComboBox);
			this.GenresSchemePanel.Controls.Add(this.GroupLabel);
			this.GenresSchemePanel.Controls.Add(this.GenreAddButton);
			this.GenresSchemePanel.Controls.Add(this.MatchMaskedTextBox);
			this.GenresSchemePanel.Controls.Add(this.MathLabel);
			this.GenresSchemePanel.Controls.Add(this.GenresComboBox);
			this.GenresSchemePanel.Controls.Add(this.GenreLabel);
			this.GenresSchemePanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.GenresSchemePanel.Enabled = false;
			this.GenresSchemePanel.Location = new System.Drawing.Point(0, 0);
			this.GenresSchemePanel.Margin = new System.Windows.Forms.Padding(4);
			this.GenresSchemePanel.Name = "GenresSchemePanel";
			this.GenresSchemePanel.Size = new System.Drawing.Size(789, 77);
			this.GenresSchemePanel.TabIndex = 77;
			// 
			// GroupComboBox
			// 
			this.GroupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.GroupComboBox.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.GroupComboBox.FormattingEnabled = true;
			this.GroupComboBox.Location = new System.Drawing.Point(68, 11);
			this.GroupComboBox.Margin = new System.Windows.Forms.Padding(4);
			this.GroupComboBox.Name = "GroupComboBox";
			this.GroupComboBox.Size = new System.Drawing.Size(447, 24);
			this.GroupComboBox.Sorted = true;
			this.GroupComboBox.TabIndex = 0;
			this.GroupComboBox.SelectedIndexChanged += new System.EventHandler(this.GroupComboBoxSelectedIndexChanged);
			// 
			// GroupLabel
			// 
			this.GroupLabel.ForeColor = System.Drawing.SystemColors.ControlText;
			this.GroupLabel.Location = new System.Drawing.Point(2, 12);
			this.GroupLabel.Name = "GroupLabel";
			this.GroupLabel.Size = new System.Drawing.Size(59, 23);
			this.GroupLabel.TabIndex = 84;
			this.GroupLabel.Text = "Группа:";
			// 
			// GenreAddButton
			// 
			this.GenreAddButton.Image = ((System.Drawing.Image)(resources.GetObject("GenreAddButton.Image")));
			this.GenreAddButton.Location = new System.Drawing.Point(712, 33);
			this.GenreAddButton.Name = "GenreAddButton";
			this.GenreAddButton.Size = new System.Drawing.Size(75, 35);
			this.GenreAddButton.TabIndex = 83;
			this.GenreAddButton.UseVisualStyleBackColor = true;
			this.GenreAddButton.Click += new System.EventHandler(this.GenreAddButtonClick);
			// 
			// MatchMaskedTextBox
			// 
			this.MatchMaskedTextBox.Location = new System.Drawing.Point(644, 44);
			this.MatchMaskedTextBox.Mask = "000";
			this.MatchMaskedTextBox.Name = "MatchMaskedTextBox";
			this.MatchMaskedTextBox.Size = new System.Drawing.Size(39, 22);
			this.MatchMaskedTextBox.TabIndex = 2;
			// 
			// MathLabel
			// 
			this.MathLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
			this.MathLabel.Location = new System.Drawing.Point(522, 47);
			this.MathLabel.Name = "MathLabel";
			this.MathLabel.Size = new System.Drawing.Size(121, 23);
			this.MathLabel.TabIndex = 80;
			this.MathLabel.Text = "Соответствие %:";
			// 
			// GenresComboBox
			// 
			this.GenresComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.GenresComboBox.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.GenresComboBox.FormattingEnabled = true;
			this.GenresComboBox.Location = new System.Drawing.Point(68, 43);
			this.GenresComboBox.Margin = new System.Windows.Forms.Padding(4);
			this.GenresComboBox.Name = "GenresComboBox";
			this.GenresComboBox.Size = new System.Drawing.Size(447, 24);
			this.GenresComboBox.Sorted = true;
			this.GenresComboBox.TabIndex = 1;
			// 
			// GenreLabel
			// 
			this.GenreLabel.ForeColor = System.Drawing.Color.Red;
			this.GenreLabel.Location = new System.Drawing.Point(2, 44);
			this.GenreLabel.Name = "GenreLabel";
			this.GenreLabel.Size = new System.Drawing.Size(59, 23);
			this.GenreLabel.TabIndex = 78;
			this.GenreLabel.Text = "Жанр:";
			// 
			// ControlPanel
			// 
			this.ControlPanel.BackColor = System.Drawing.Color.DarkGray;
			this.ControlPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.ControlPanel.Controls.Add(this.CancelBtn);
			this.ControlPanel.Controls.Add(this.ApplyBtn);
			this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.ControlPanel.Enabled = false;
			this.ControlPanel.Location = new System.Drawing.Point(789, 0);
			this.ControlPanel.Name = "ControlPanel";
			this.ControlPanel.Size = new System.Drawing.Size(127, 354);
			this.ControlPanel.TabIndex = 24;
			// 
			// CancelBtn
			// 
			this.CancelBtn.Dock = System.Windows.Forms.DockStyle.Top;
			this.CancelBtn.Image = ((System.Drawing.Image)(resources.GetObject("CancelBtn.Image")));
			this.CancelBtn.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.CancelBtn.Location = new System.Drawing.Point(0, 52);
			this.CancelBtn.Name = "CancelBtn";
			this.CancelBtn.Size = new System.Drawing.Size(125, 49);
			this.CancelBtn.TabIndex = 9;
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
			this.ApplyBtn.TabIndex = 8;
			this.ApplyBtn.Text = "Принять";
			this.ApplyBtn.UseVisualStyleBackColor = true;
			this.ApplyBtn.Click += new System.EventHandler(this.ApplyBtnClick);
			// 
			// GenresPanel
			// 
			this.GenresPanel.Controls.Add(this.GenrePanel);
			this.GenresPanel.Dock = System.Windows.Forms.DockStyle.Top;
			this.GenresPanel.Location = new System.Drawing.Point(0, 0);
			this.GenresPanel.Name = "GenresPanel";
			this.GenresPanel.Size = new System.Drawing.Size(789, 303);
			this.GenresPanel.TabIndex = 25;
			// 
			// ProgressPanel
			// 
			this.ProgressPanel.Controls.Add(this.ProgressBar);
			this.ProgressPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ProgressPanel.Location = new System.Drawing.Point(0, 310);
			this.ProgressPanel.Name = "ProgressPanel";
			this.ProgressPanel.Size = new System.Drawing.Size(789, 44);
			this.ProgressPanel.TabIndex = 26;
			// 
			// ProgressBar
			// 
			this.ProgressBar.Location = new System.Drawing.Point(10, 10);
			this.ProgressBar.Name = "ProgressBar";
			this.ProgressBar.Size = new System.Drawing.Size(771, 23);
			this.ProgressBar.TabIndex = 8;
			// 
			// EditGenreInfoForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(916, 354);
			this.ControlBox = false;
			this.Controls.Add(this.ProgressPanel);
			this.Controls.Add(this.GenresPanel);
			this.Controls.Add(this.ControlPanel);
			this.Name = "EditGenreInfoForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Правка Жанров";
			this.GenrePanel.ResumeLayout(false);
			this.GenreWorkPanel.ResumeLayout(false);
			this.GenresSchemePanel.ResumeLayout(false);
			this.GenresSchemePanel.PerformLayout();
			this.ControlPanel.ResumeLayout(false);
			this.GenresPanel.ResumeLayout(false);
			this.ProgressPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		private System.Windows.Forms.ComboBox GroupComboBox;
		private System.Windows.Forms.Label GroupLabel;
		private System.Windows.Forms.Panel ProgressPanel;
		private System.Windows.Forms.ProgressBar ProgressBar;
		private System.Windows.Forms.Button GenreUpButton;
		private System.Windows.Forms.Button GenreDownButton;
		private System.Windows.Forms.Panel GenresPanel;
		private System.Windows.Forms.Panel GenrePanel;
		private System.Windows.Forms.ListView GenresListView;
		private System.Windows.Forms.ColumnHeader columnHeaderGenre;
		private System.Windows.Forms.ColumnHeader columnHeaderMath;
		private System.Windows.Forms.Panel GenreWorkPanel;
		private System.Windows.Forms.Button GenreDeleteAllButton;
		private System.Windows.Forms.Button GenreDeleteButton;
		private System.Windows.Forms.Panel GenresSchemePanel;
		private System.Windows.Forms.Button GenreAddButton;
		private System.Windows.Forms.MaskedTextBox MatchMaskedTextBox;
		private System.Windows.Forms.Label MathLabel;
		private System.Windows.Forms.ComboBox GenresComboBox;
		private System.Windows.Forms.Label GenreLabel;
		private System.Windows.Forms.Panel ControlPanel;
		private System.Windows.Forms.Button CancelBtn;
		private System.Windows.Forms.Button ApplyBtn;
	}
}
