/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 23.06.2009
 * Time: 8:50
 * 
 * License: GPL 2.1
 */
namespace SharpFBTools.Tools
{
	partial class SelectedSortData
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectedSortData));
			this.cmbBoxSSLang = new System.Windows.Forms.ComboBox();
			this.chBoxSSLang = new System.Windows.Forms.CheckBox();
			this.txtBoxSSSequence = new System.Windows.Forms.TextBox();
			this.chBoxSSSequence = new System.Windows.Forms.CheckBox();
			this.lblSSNick = new System.Windows.Forms.Label();
			this.textBoxSSNick = new System.Windows.Forms.TextBox();
			this.lblSSMiddle = new System.Windows.Forms.Label();
			this.textBoxSSMiddle = new System.Windows.Forms.TextBox();
			this.lblSSFirst = new System.Windows.Forms.Label();
			this.chBoxAuthor = new System.Windows.Forms.CheckBox();
			this.textBoxSSLast = new System.Windows.Forms.TextBox();
			this.textBoxSSFirst = new System.Windows.Forms.TextBox();
			this.lblSSLast = new System.Windows.Forms.Label();
			this.gBoxGenre = new System.Windows.Forms.GroupBox();
			this.cmbBoxSSGenres = new System.Windows.Forms.ComboBox();
			this.cmbBoxSSGenresGroup = new System.Windows.Forms.ComboBox();
			this.rbtnSSGenres = new System.Windows.Forms.RadioButton();
			this.rbtnSSGenresGroup = new System.Windows.Forms.RadioButton();
			this.chkBoxGenre = new System.Windows.Forms.CheckBox();
			this.lvSSData = new System.Windows.Forms.ListView();
			this.cHeaderLang = new System.Windows.Forms.ColumnHeader();
			this.cHeaderGenresGroup = new System.Windows.Forms.ColumnHeader();
			this.cHeaderGenre = new System.Windows.Forms.ColumnHeader();
			this.cHeaderLast = new System.Windows.Forms.ColumnHeader();
			this.cHeaderFirst = new System.Windows.Forms.ColumnHeader();
			this.cHeaderMiddle = new System.Windows.Forms.ColumnHeader();
			this.cHeaderNick = new System.Windows.Forms.ColumnHeader();
			this.cHeaderSequence = new System.Windows.Forms.ColumnHeader();
			this.cHeaderBookTitle = new System.Windows.Forms.ColumnHeader();
			this.cHeaderExactFit = new System.Windows.Forms.ColumnHeader();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.txtBoxInfo = new System.Windows.Forms.TextBox();
			this.chBoxExactFit = new System.Windows.Forms.CheckBox();
			this.txtBoxSSBookTitle = new System.Windows.Forms.TextBox();
			this.chkBoxBookTitle = new System.Windows.Forms.CheckBox();
			this.lblInfo = new System.Windows.Forms.Label();
			this.lblCount = new System.Windows.Forms.Label();
			this.btnDeleteAll = new System.Windows.Forms.Button();
			this.gBoxGenre.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmbBoxSSLang
			// 
			this.cmbBoxSSLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbBoxSSLang.Enabled = false;
			this.cmbBoxSSLang.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.cmbBoxSSLang.FormattingEnabled = true;
			this.cmbBoxSSLang.Location = new System.Drawing.Point(99, 79);
			this.cmbBoxSSLang.Name = "cmbBoxSSLang";
			this.cmbBoxSSLang.Size = new System.Drawing.Size(257, 21);
			this.cmbBoxSSLang.TabIndex = 36;
			// 
			// chBoxSSLang
			// 
			this.chBoxSSLang.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxSSLang.Location = new System.Drawing.Point(12, 79);
			this.chBoxSSLang.Name = "chBoxSSLang";
			this.chBoxSSLang.Size = new System.Drawing.Size(65, 24);
			this.chBoxSSLang.TabIndex = 35;
			this.chBoxSSLang.Text = "Язык:";
			this.chBoxSSLang.UseVisualStyleBackColor = true;
			this.chBoxSSLang.CheckedChanged += new System.EventHandler(this.ChBoxSSLangCheckedChanged);
			// 
			// txtBoxSSSequence
			// 
			this.txtBoxSSSequence.Enabled = false;
			this.txtBoxSSSequence.Location = new System.Drawing.Point(99, 258);
			this.txtBoxSSSequence.Name = "txtBoxSSSequence";
			this.txtBoxSSSequence.Size = new System.Drawing.Size(621, 20);
			this.txtBoxSSSequence.TabIndex = 46;
			// 
			// chBoxSSSequence
			// 
			this.chBoxSSSequence.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxSSSequence.Location = new System.Drawing.Point(12, 256);
			this.chBoxSSSequence.Name = "chBoxSSSequence";
			this.chBoxSSSequence.Size = new System.Drawing.Size(65, 24);
			this.chBoxSSSequence.TabIndex = 45;
			this.chBoxSSSequence.Text = "Серия:";
			this.chBoxSSSequence.UseVisualStyleBackColor = true;
			this.chBoxSSSequence.Click += new System.EventHandler(this.ChBoxSSSequenceClick);
			this.chBoxSSSequence.CheckedChanged += new System.EventHandler(this.ChBoxSSSequenceCheckedChanged);
			// 
			// lblSSNick
			// 
			this.lblSSNick.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblSSNick.ForeColor = System.Drawing.Color.Navy;
			this.lblSSNick.Location = new System.Drawing.Point(362, 128);
			this.lblSSNick.Name = "lblSSNick";
			this.lblSSNick.Size = new System.Drawing.Size(33, 24);
			this.lblSSNick.TabIndex = 57;
			this.lblSSNick.Text = "Ник";
			this.lblSSNick.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBoxSSNick
			// 
			this.textBoxSSNick.Enabled = false;
			this.textBoxSSNick.Location = new System.Drawing.Point(398, 133);
			this.textBoxSSNick.Name = "textBoxSSNick";
			this.textBoxSSNick.Size = new System.Drawing.Size(192, 20);
			this.textBoxSSNick.TabIndex = 56;
			// 
			// lblSSMiddle
			// 
			this.lblSSMiddle.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblSSMiddle.ForeColor = System.Drawing.Color.Navy;
			this.lblSSMiddle.Location = new System.Drawing.Point(99, 128);
			this.lblSSMiddle.Name = "lblSSMiddle";
			this.lblSSMiddle.Size = new System.Drawing.Size(64, 24);
			this.lblSSMiddle.TabIndex = 55;
			this.lblSSMiddle.Text = "Отчество";
			this.lblSSMiddle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBoxSSMiddle
			// 
			this.textBoxSSMiddle.Enabled = false;
			this.textBoxSSMiddle.Location = new System.Drawing.Point(164, 133);
			this.textBoxSSMiddle.Name = "textBoxSSMiddle";
			this.textBoxSSMiddle.Size = new System.Drawing.Size(192, 20);
			this.textBoxSSMiddle.TabIndex = 54;
			// 
			// lblSSFirst
			// 
			this.lblSSFirst.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblSSFirst.ForeColor = System.Drawing.Color.Navy;
			this.lblSSFirst.Location = new System.Drawing.Point(362, 106);
			this.lblSSFirst.Name = "lblSSFirst";
			this.lblSSFirst.Size = new System.Drawing.Size(35, 24);
			this.lblSSFirst.TabIndex = 53;
			this.lblSSFirst.Text = "Имя";
			this.lblSSFirst.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// chBoxAuthor
			// 
			this.chBoxAuthor.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxAuthor.Location = new System.Drawing.Point(12, 107);
			this.chBoxAuthor.Name = "chBoxAuthor";
			this.chBoxAuthor.Size = new System.Drawing.Size(67, 24);
			this.chBoxAuthor.TabIndex = 49;
			this.chBoxAuthor.Text = "Автор:";
			this.chBoxAuthor.UseVisualStyleBackColor = true;
			this.chBoxAuthor.Click += new System.EventHandler(this.ChBoxAuthorClick);
			this.chBoxAuthor.CheckedChanged += new System.EventHandler(this.ChBoxAuthorCheckedChanged);
			// 
			// textBoxSSLast
			// 
			this.textBoxSSLast.Enabled = false;
			this.textBoxSSLast.Location = new System.Drawing.Point(164, 109);
			this.textBoxSSLast.Name = "textBoxSSLast";
			this.textBoxSSLast.Size = new System.Drawing.Size(192, 20);
			this.textBoxSSLast.TabIndex = 50;
			// 
			// textBoxSSFirst
			// 
			this.textBoxSSFirst.Enabled = false;
			this.textBoxSSFirst.Location = new System.Drawing.Point(398, 109);
			this.textBoxSSFirst.Name = "textBoxSSFirst";
			this.textBoxSSFirst.Size = new System.Drawing.Size(192, 20);
			this.textBoxSSFirst.TabIndex = 52;
			// 
			// lblSSLast
			// 
			this.lblSSLast.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblSSLast.ForeColor = System.Drawing.Color.Navy;
			this.lblSSLast.Location = new System.Drawing.Point(99, 106);
			this.lblSSLast.Name = "lblSSLast";
			this.lblSSLast.Size = new System.Drawing.Size(59, 24);
			this.lblSSLast.TabIndex = 51;
			this.lblSSLast.Text = "Фамилия";
			this.lblSSLast.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// gBoxGenre
			// 
			this.gBoxGenre.Controls.Add(this.cmbBoxSSGenres);
			this.gBoxGenre.Controls.Add(this.cmbBoxSSGenresGroup);
			this.gBoxGenre.Controls.Add(this.rbtnSSGenres);
			this.gBoxGenre.Controls.Add(this.rbtnSSGenresGroup);
			this.gBoxGenre.Enabled = false;
			this.gBoxGenre.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gBoxGenre.Location = new System.Drawing.Point(99, 182);
			this.gBoxGenre.Name = "gBoxGenre";
			this.gBoxGenre.Size = new System.Drawing.Size(491, 71);
			this.gBoxGenre.TabIndex = 58;
			this.gBoxGenre.TabStop = false;
			// 
			// cmbBoxSSGenres
			// 
			this.cmbBoxSSGenres.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbBoxSSGenres.Enabled = false;
			this.cmbBoxSSGenres.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.cmbBoxSSGenres.FormattingEnabled = true;
			this.cmbBoxSSGenres.Location = new System.Drawing.Point(136, 41);
			this.cmbBoxSSGenres.Name = "cmbBoxSSGenres";
			this.cmbBoxSSGenres.Size = new System.Drawing.Size(336, 21);
			this.cmbBoxSSGenres.TabIndex = 38;
			// 
			// cmbBoxSSGenresGroup
			// 
			this.cmbBoxSSGenresGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbBoxSSGenresGroup.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.cmbBoxSSGenresGroup.FormattingEnabled = true;
			this.cmbBoxSSGenresGroup.Location = new System.Drawing.Point(136, 15);
			this.cmbBoxSSGenresGroup.Name = "cmbBoxSSGenresGroup";
			this.cmbBoxSSGenresGroup.Size = new System.Drawing.Size(336, 21);
			this.cmbBoxSSGenresGroup.Sorted = true;
			this.cmbBoxSSGenresGroup.TabIndex = 37;
			// 
			// rbtnSSGenres
			// 
			this.rbtnSSGenres.ForeColor = System.Drawing.Color.Navy;
			this.rbtnSSGenres.Location = new System.Drawing.Point(13, 38);
			this.rbtnSSGenres.Name = "rbtnSSGenres";
			this.rbtnSSGenres.Size = new System.Drawing.Size(58, 24);
			this.rbtnSSGenres.TabIndex = 3;
			this.rbtnSSGenres.Text = "Жанр";
			this.rbtnSSGenres.UseVisualStyleBackColor = true;
			this.rbtnSSGenres.CheckedChanged += new System.EventHandler(this.RbtnSSGenresCheckedChanged);
			// 
			// rbtnSSGenresGroup
			// 
			this.rbtnSSGenresGroup.Checked = true;
			this.rbtnSSGenresGroup.ForeColor = System.Drawing.Color.Navy;
			this.rbtnSSGenresGroup.Location = new System.Drawing.Point(13, 15);
			this.rbtnSSGenresGroup.Name = "rbtnSSGenresGroup";
			this.rbtnSSGenresGroup.Size = new System.Drawing.Size(117, 24);
			this.rbtnSSGenresGroup.TabIndex = 2;
			this.rbtnSSGenresGroup.TabStop = true;
			this.rbtnSSGenresGroup.Text = "Группа Жанров";
			this.rbtnSSGenresGroup.UseVisualStyleBackColor = true;
			this.rbtnSSGenresGroup.CheckedChanged += new System.EventHandler(this.RbtnSSGenresGroupCheckedChanged);
			// 
			// chkBoxGenre
			// 
			this.chkBoxGenre.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chkBoxGenre.Location = new System.Drawing.Point(12, 182);
			this.chkBoxGenre.Name = "chkBoxGenre";
			this.chkBoxGenre.Size = new System.Drawing.Size(65, 22);
			this.chkBoxGenre.TabIndex = 65;
			this.chkBoxGenre.Text = "Жанр:";
			this.chkBoxGenre.UseVisualStyleBackColor = true;
			this.chkBoxGenre.CheckedChanged += new System.EventHandler(this.ChkBoxGanreCheckedChanged);
			// 
			// lvSSData
			// 
			this.lvSSData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.cHeaderLang,
									this.cHeaderGenresGroup,
									this.cHeaderGenre,
									this.cHeaderLast,
									this.cHeaderFirst,
									this.cHeaderMiddle,
									this.cHeaderNick,
									this.cHeaderSequence,
									this.cHeaderBookTitle,
									this.cHeaderExactFit});
			this.lvSSData.FullRowSelect = true;
			this.lvSSData.GridLines = true;
			this.lvSSData.HideSelection = false;
			this.lvSSData.Location = new System.Drawing.Point(12, 288);
			this.lvSSData.Name = "lvSSData";
			this.lvSSData.Size = new System.Drawing.Size(708, 118);
			this.lvSSData.TabIndex = 60;
			this.lvSSData.UseCompatibleStateImageBehavior = false;
			this.lvSSData.View = System.Windows.Forms.View.Details;
			this.lvSSData.SelectedIndexChanged += new System.EventHandler(this.LvSSDataSelectedIndexChanged);
			// 
			// cHeaderLang
			// 
			this.cHeaderLang.Text = "Язык Книги";
			this.cHeaderLang.Width = 80;
			// 
			// cHeaderGenresGroup
			// 
			this.cHeaderGenresGroup.Text = "Группа Жанров";
			this.cHeaderGenresGroup.Width = 120;
			// 
			// cHeaderGenre
			// 
			this.cHeaderGenre.Text = "Жанр";
			this.cHeaderGenre.Width = 120;
			// 
			// cHeaderLast
			// 
			this.cHeaderLast.Text = "Фамилия";
			this.cHeaderLast.Width = 120;
			// 
			// cHeaderFirst
			// 
			this.cHeaderFirst.Text = "Имя";
			this.cHeaderFirst.Width = 80;
			// 
			// cHeaderMiddle
			// 
			this.cHeaderMiddle.Text = "Отчество";
			this.cHeaderMiddle.Width = 80;
			// 
			// cHeaderNick
			// 
			this.cHeaderNick.Text = "Ник";
			this.cHeaderNick.Width = 50;
			// 
			// cHeaderSequence
			// 
			this.cHeaderSequence.Text = "Серия";
			this.cHeaderSequence.Width = 80;
			// 
			// cHeaderBookTitle
			// 
			this.cHeaderBookTitle.Text = "Название Книги";
			this.cHeaderBookTitle.Width = 110;
			// 
			// cHeaderExactFit
			// 
			this.cHeaderExactFit.Text = "Точное соответствие";
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.btnCancel.Location = new System.Drawing.Point(457, 416);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(110, 26);
			this.btnCancel.TabIndex = 62;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOK
			// 
			this.btnOK.Enabled = false;
			this.btnOK.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.btnOK.Image = ((System.Drawing.Image)(resources.GetObject("btnOK.Image")));
			this.btnOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnOK.Location = new System.Drawing.Point(610, 416);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(110, 26);
			this.btnOK.TabIndex = 61;
			this.btnOK.Text = "Принять";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.BtnOKClick);
			// 
			// btnDelete
			// 
			this.btnDelete.Enabled = false;
			this.btnDelete.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.btnDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.Image")));
			this.btnDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnDelete.Location = new System.Drawing.Point(610, 156);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(110, 26);
			this.btnDelete.TabIndex = 64;
			this.btnDelete.Text = "Удалить";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.BtnDeleteClick);
			// 
			// btnAdd
			// 
			this.btnAdd.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.btnAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnAdd.Image")));
			this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnAdd.Location = new System.Drawing.Point(610, 108);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(110, 26);
			this.btnAdd.TabIndex = 63;
			this.btnAdd.Text = "Добавть";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.BtnAddClick);
			// 
			// txtBoxInfo
			// 
			this.txtBoxInfo.Dock = System.Windows.Forms.DockStyle.Top;
			this.txtBoxInfo.Location = new System.Drawing.Point(0, 0);
			this.txtBoxInfo.Multiline = true;
			this.txtBoxInfo.Name = "txtBoxInfo";
			this.txtBoxInfo.ReadOnly = true;
			this.txtBoxInfo.Size = new System.Drawing.Size(734, 73);
			this.txtBoxInfo.TabIndex = 66;
			this.txtBoxInfo.Text = resources.GetString("txtBoxInfo.Text");
			this.txtBoxInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// chBoxExactFit
			// 
			this.chBoxExactFit.Checked = true;
			this.chBoxExactFit.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chBoxExactFit.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxExactFit.ForeColor = System.Drawing.Color.Purple;
			this.chBoxExactFit.Location = new System.Drawing.Point(398, 79);
			this.chBoxExactFit.Name = "chBoxExactFit";
			this.chBoxExactFit.Size = new System.Drawing.Size(155, 24);
			this.chBoxExactFit.TabIndex = 67;
			this.chBoxExactFit.Text = "Точное соответствие";
			this.chBoxExactFit.UseVisualStyleBackColor = true;
			// 
			// txtBoxSSBookTitle
			// 
			this.txtBoxSSBookTitle.Enabled = false;
			this.txtBoxSSBookTitle.Location = new System.Drawing.Point(99, 159);
			this.txtBoxSSBookTitle.Name = "txtBoxSSBookTitle";
			this.txtBoxSSBookTitle.Size = new System.Drawing.Size(491, 20);
			this.txtBoxSSBookTitle.TabIndex = 69;
			// 
			// chkBoxBookTitle
			// 
			this.chkBoxBookTitle.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chkBoxBookTitle.Location = new System.Drawing.Point(12, 157);
			this.chkBoxBookTitle.Name = "chkBoxBookTitle";
			this.chkBoxBookTitle.Size = new System.Drawing.Size(87, 24);
			this.chkBoxBookTitle.TabIndex = 68;
			this.chkBoxBookTitle.Text = "Название:";
			this.chkBoxBookTitle.UseVisualStyleBackColor = true;
			this.chkBoxBookTitle.Click += new System.EventHandler(this.ChkBoxBookTitleClick);
			this.chkBoxBookTitle.CheckedChanged += new System.EventHandler(this.ChkBoxBookTitleCheckedChanged);
			// 
			// lblInfo
			// 
			this.lblInfo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.lblInfo.Location = new System.Drawing.Point(12, 416);
			this.lblInfo.Name = "lblInfo";
			this.lblInfo.Size = new System.Drawing.Size(185, 23);
			this.lblInfo.TabIndex = 70;
			this.lblInfo.Text = "Число записей условий поиска:";
			// 
			// lblCount
			// 
			this.lblCount.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblCount.ForeColor = System.Drawing.Color.Navy;
			this.lblCount.Location = new System.Drawing.Point(196, 416);
			this.lblCount.Name = "lblCount";
			this.lblCount.Size = new System.Drawing.Size(62, 23);
			this.lblCount.TabIndex = 71;
			this.lblCount.Text = "0";
			// 
			// btnDeleteAll
			// 
			this.btnDeleteAll.Enabled = false;
			this.btnDeleteAll.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.btnDeleteAll.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteAll.Image")));
			this.btnDeleteAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnDeleteAll.Location = new System.Drawing.Point(610, 197);
			this.btnDeleteAll.Name = "btnDeleteAll";
			this.btnDeleteAll.Size = new System.Drawing.Size(110, 26);
			this.btnDeleteAll.TabIndex = 72;
			this.btnDeleteAll.Text = "Удалить все";
			this.btnDeleteAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnDeleteAll.UseVisualStyleBackColor = true;
			this.btnDeleteAll.Click += new System.EventHandler(this.BtnDeleteAllClick);
			// 
			// SelectedSortData
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(734, 455);
			this.Controls.Add(this.btnDeleteAll);
			this.Controls.Add(this.lblCount);
			this.Controls.Add(this.lblInfo);
			this.Controls.Add(this.txtBoxSSBookTitle);
			this.Controls.Add(this.chkBoxBookTitle);
			this.Controls.Add(this.chBoxExactFit);
			this.Controls.Add(this.txtBoxInfo);
			this.Controls.Add(this.chkBoxGenre);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.lvSSData);
			this.Controls.Add(this.gBoxGenre);
			this.Controls.Add(this.lblSSNick);
			this.Controls.Add(this.textBoxSSNick);
			this.Controls.Add(this.lblSSMiddle);
			this.Controls.Add(this.textBoxSSMiddle);
			this.Controls.Add(this.lblSSFirst);
			this.Controls.Add(this.chBoxAuthor);
			this.Controls.Add(this.textBoxSSLast);
			this.Controls.Add(this.textBoxSSFirst);
			this.Controls.Add(this.lblSSLast);
			this.Controls.Add(this.txtBoxSSSequence);
			this.Controls.Add(this.chBoxSSSequence);
			this.Controls.Add(this.cmbBoxSSLang);
			this.Controls.Add(this.chBoxSSLang);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SelectedSortData";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Данные для Избранной Сортировки";
			this.Shown += new System.EventHandler(this.SelectedSortDataShown);
			this.gBoxGenre.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Button btnDeleteAll;
		public System.Windows.Forms.Label lblCount;
		private System.Windows.Forms.Label lblInfo;
		private System.Windows.Forms.CheckBox chkBoxBookTitle;
		private System.Windows.Forms.TextBox txtBoxSSBookTitle;
		private System.Windows.Forms.ColumnHeader cHeaderBookTitle;
		private System.Windows.Forms.ColumnHeader cHeaderExactFit;
		private System.Windows.Forms.CheckBox chBoxExactFit;
		private System.Windows.Forms.TextBox txtBoxInfo;
		private System.Windows.Forms.CheckBox chkBoxGenre;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ColumnHeader cHeaderGenre;
		private System.Windows.Forms.ColumnHeader cHeaderGenresGroup;
		private System.Windows.Forms.ColumnHeader cHeaderSequence;
		private System.Windows.Forms.ColumnHeader cHeaderNick;
		private System.Windows.Forms.ColumnHeader cHeaderMiddle;
		private System.Windows.Forms.ColumnHeader cHeaderFirst;
		private System.Windows.Forms.ColumnHeader cHeaderLast;
		private System.Windows.Forms.ColumnHeader cHeaderLang;
		public System.Windows.Forms.ListView lvSSData;
		private System.Windows.Forms.ComboBox cmbBoxSSGenresGroup;
		private System.Windows.Forms.ComboBox cmbBoxSSGenres;
		private System.Windows.Forms.RadioButton rbtnSSGenresGroup;
		private System.Windows.Forms.RadioButton rbtnSSGenres;
		private System.Windows.Forms.GroupBox gBoxGenre;
		private System.Windows.Forms.Label lblSSLast;
		private System.Windows.Forms.TextBox textBoxSSFirst;
		private System.Windows.Forms.TextBox textBoxSSLast;
		private System.Windows.Forms.CheckBox chBoxAuthor;
		private System.Windows.Forms.Label lblSSFirst;
		private System.Windows.Forms.TextBox textBoxSSMiddle;
		private System.Windows.Forms.Label lblSSMiddle;
		private System.Windows.Forms.TextBox textBoxSSNick;
		private System.Windows.Forms.Label lblSSNick;
		private System.Windows.Forms.CheckBox chBoxSSSequence;
		private System.Windows.Forms.TextBox txtBoxSSSequence;
		private System.Windows.Forms.CheckBox chBoxSSLang;
		private System.Windows.Forms.ComboBox cmbBoxSSLang;
	}
}
