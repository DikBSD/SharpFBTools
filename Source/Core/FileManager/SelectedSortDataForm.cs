/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 23.06.2009
 * Time: 8:50
 * 
 * License: GPL 2.1
 */
using System;
using System.Windows.Forms;

using Settings;
using Core.FB2.Genres;
using Core.Common;

namespace Core.FileManager
{
	/// <summary>
	/// SelectedSortData: форма сбора информации по критериям избранной сортировки
	/// </summary>
	public partial class SelectedSortDataForm : Form
	{
		#region Designer
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectedSortDataForm));
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
			this.cmbBoxSSLang.Location = new System.Drawing.Point(132, 97);
			this.cmbBoxSSLang.Margin = new System.Windows.Forms.Padding(4);
			this.cmbBoxSSLang.Name = "cmbBoxSSLang";
			this.cmbBoxSSLang.Size = new System.Drawing.Size(341, 24);
			this.cmbBoxSSLang.TabIndex = 36;
			// 
			// chBoxSSLang
			// 
			this.chBoxSSLang.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxSSLang.Location = new System.Drawing.Point(16, 97);
			this.chBoxSSLang.Margin = new System.Windows.Forms.Padding(4);
			this.chBoxSSLang.Name = "chBoxSSLang";
			this.chBoxSSLang.Size = new System.Drawing.Size(87, 30);
			this.chBoxSSLang.TabIndex = 35;
			this.chBoxSSLang.Text = "Язык:";
			this.chBoxSSLang.UseVisualStyleBackColor = true;
			this.chBoxSSLang.CheckedChanged += new System.EventHandler(this.ChBoxSSLangCheckedChanged);
			// 
			// txtBoxSSSequence
			// 
			this.txtBoxSSSequence.Enabled = false;
			this.txtBoxSSSequence.Location = new System.Drawing.Point(132, 319);
			this.txtBoxSSSequence.Margin = new System.Windows.Forms.Padding(4);
			this.txtBoxSSSequence.Name = "txtBoxSSSequence";
			this.txtBoxSSSequence.Size = new System.Drawing.Size(827, 22);
			this.txtBoxSSSequence.TabIndex = 46;
			// 
			// chBoxSSSequence
			// 
			this.chBoxSSSequence.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxSSSequence.Location = new System.Drawing.Point(16, 317);
			this.chBoxSSSequence.Margin = new System.Windows.Forms.Padding(4);
			this.chBoxSSSequence.Name = "chBoxSSSequence";
			this.chBoxSSSequence.Size = new System.Drawing.Size(87, 30);
			this.chBoxSSSequence.TabIndex = 45;
			this.chBoxSSSequence.Text = "Серия:";
			this.chBoxSSSequence.UseVisualStyleBackColor = true;
			this.chBoxSSSequence.CheckedChanged += new System.EventHandler(this.ChBoxSSSequenceCheckedChanged);
			this.chBoxSSSequence.Click += new System.EventHandler(this.ChBoxSSSequenceClick);
			// 
			// lblSSNick
			// 
			this.lblSSNick.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblSSNick.ForeColor = System.Drawing.Color.Navy;
			this.lblSSNick.Location = new System.Drawing.Point(483, 158);
			this.lblSSNick.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblSSNick.Name = "lblSSNick";
			this.lblSSNick.Size = new System.Drawing.Size(44, 30);
			this.lblSSNick.TabIndex = 57;
			this.lblSSNick.Text = "Ник";
			this.lblSSNick.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBoxSSNick
			// 
			this.textBoxSSNick.Enabled = false;
			this.textBoxSSNick.Location = new System.Drawing.Point(531, 164);
			this.textBoxSSNick.Margin = new System.Windows.Forms.Padding(4);
			this.textBoxSSNick.Name = "textBoxSSNick";
			this.textBoxSSNick.Size = new System.Drawing.Size(255, 22);
			this.textBoxSSNick.TabIndex = 56;
			// 
			// lblSSMiddle
			// 
			this.lblSSMiddle.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblSSMiddle.ForeColor = System.Drawing.Color.Navy;
			this.lblSSMiddle.Location = new System.Drawing.Point(132, 158);
			this.lblSSMiddle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblSSMiddle.Name = "lblSSMiddle";
			this.lblSSMiddle.Size = new System.Drawing.Size(85, 30);
			this.lblSSMiddle.TabIndex = 55;
			this.lblSSMiddle.Text = "Отчество";
			this.lblSSMiddle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBoxSSMiddle
			// 
			this.textBoxSSMiddle.Enabled = false;
			this.textBoxSSMiddle.Location = new System.Drawing.Point(219, 164);
			this.textBoxSSMiddle.Margin = new System.Windows.Forms.Padding(4);
			this.textBoxSSMiddle.Name = "textBoxSSMiddle";
			this.textBoxSSMiddle.Size = new System.Drawing.Size(255, 22);
			this.textBoxSSMiddle.TabIndex = 54;
			// 
			// lblSSFirst
			// 
			this.lblSSFirst.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblSSFirst.ForeColor = System.Drawing.Color.Navy;
			this.lblSSFirst.Location = new System.Drawing.Point(483, 130);
			this.lblSSFirst.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblSSFirst.Name = "lblSSFirst";
			this.lblSSFirst.Size = new System.Drawing.Size(47, 30);
			this.lblSSFirst.TabIndex = 53;
			this.lblSSFirst.Text = "Имя";
			this.lblSSFirst.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// chBoxAuthor
			// 
			this.chBoxAuthor.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxAuthor.Location = new System.Drawing.Point(16, 132);
			this.chBoxAuthor.Margin = new System.Windows.Forms.Padding(4);
			this.chBoxAuthor.Name = "chBoxAuthor";
			this.chBoxAuthor.Size = new System.Drawing.Size(89, 30);
			this.chBoxAuthor.TabIndex = 49;
			this.chBoxAuthor.Text = "Автор:";
			this.chBoxAuthor.UseVisualStyleBackColor = true;
			this.chBoxAuthor.CheckedChanged += new System.EventHandler(this.ChBoxAuthorCheckedChanged);
			this.chBoxAuthor.Click += new System.EventHandler(this.ChBoxAuthorClick);
			// 
			// textBoxSSLast
			// 
			this.textBoxSSLast.Enabled = false;
			this.textBoxSSLast.Location = new System.Drawing.Point(219, 134);
			this.textBoxSSLast.Margin = new System.Windows.Forms.Padding(4);
			this.textBoxSSLast.Name = "textBoxSSLast";
			this.textBoxSSLast.Size = new System.Drawing.Size(255, 22);
			this.textBoxSSLast.TabIndex = 50;
			// 
			// textBoxSSFirst
			// 
			this.textBoxSSFirst.Enabled = false;
			this.textBoxSSFirst.Location = new System.Drawing.Point(531, 134);
			this.textBoxSSFirst.Margin = new System.Windows.Forms.Padding(4);
			this.textBoxSSFirst.Name = "textBoxSSFirst";
			this.textBoxSSFirst.Size = new System.Drawing.Size(255, 22);
			this.textBoxSSFirst.TabIndex = 52;
			// 
			// lblSSLast
			// 
			this.lblSSLast.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblSSLast.ForeColor = System.Drawing.Color.Navy;
			this.lblSSLast.Location = new System.Drawing.Point(132, 130);
			this.lblSSLast.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblSSLast.Name = "lblSSLast";
			this.lblSSLast.Size = new System.Drawing.Size(79, 30);
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
			this.gBoxGenre.Location = new System.Drawing.Point(132, 223);
			this.gBoxGenre.Margin = new System.Windows.Forms.Padding(4);
			this.gBoxGenre.Name = "gBoxGenre";
			this.gBoxGenre.Padding = new System.Windows.Forms.Padding(4);
			this.gBoxGenre.Size = new System.Drawing.Size(655, 87);
			this.gBoxGenre.TabIndex = 58;
			this.gBoxGenre.TabStop = false;
			// 
			// cmbBoxSSGenres
			// 
			this.cmbBoxSSGenres.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbBoxSSGenres.Enabled = false;
			this.cmbBoxSSGenres.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.cmbBoxSSGenres.FormattingEnabled = true;
			this.cmbBoxSSGenres.Location = new System.Drawing.Point(168, 50);
			this.cmbBoxSSGenres.Margin = new System.Windows.Forms.Padding(4);
			this.cmbBoxSSGenres.Name = "cmbBoxSSGenres";
			this.cmbBoxSSGenres.Size = new System.Drawing.Size(472, 24);
			this.cmbBoxSSGenres.Sorted = true;
			this.cmbBoxSSGenres.TabIndex = 38;
			// 
			// cmbBoxSSGenresGroup
			// 
			this.cmbBoxSSGenresGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbBoxSSGenresGroup.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.cmbBoxSSGenresGroup.FormattingEnabled = true;
			this.cmbBoxSSGenresGroup.Location = new System.Drawing.Point(168, 18);
			this.cmbBoxSSGenresGroup.Margin = new System.Windows.Forms.Padding(4);
			this.cmbBoxSSGenresGroup.Name = "cmbBoxSSGenresGroup";
			this.cmbBoxSSGenresGroup.Size = new System.Drawing.Size(472, 24);
			this.cmbBoxSSGenresGroup.Sorted = true;
			this.cmbBoxSSGenresGroup.TabIndex = 37;
			// 
			// rbtnSSGenres
			// 
			this.rbtnSSGenres.ForeColor = System.Drawing.Color.Navy;
			this.rbtnSSGenres.Location = new System.Drawing.Point(11, 47);
			this.rbtnSSGenres.Margin = new System.Windows.Forms.Padding(4);
			this.rbtnSSGenres.Name = "rbtnSSGenres";
			this.rbtnSSGenres.Size = new System.Drawing.Size(77, 30);
			this.rbtnSSGenres.TabIndex = 3;
			this.rbtnSSGenres.Text = "Жанр";
			this.rbtnSSGenres.UseVisualStyleBackColor = true;
			this.rbtnSSGenres.CheckedChanged += new System.EventHandler(this.RbtnSSGenresCheckedChanged);
			// 
			// rbtnSSGenresGroup
			// 
			this.rbtnSSGenresGroup.Checked = true;
			this.rbtnSSGenresGroup.ForeColor = System.Drawing.Color.Navy;
			this.rbtnSSGenresGroup.Location = new System.Drawing.Point(11, 18);
			this.rbtnSSGenresGroup.Margin = new System.Windows.Forms.Padding(4);
			this.rbtnSSGenresGroup.Name = "rbtnSSGenresGroup";
			this.rbtnSSGenresGroup.Size = new System.Drawing.Size(156, 30);
			this.rbtnSSGenresGroup.TabIndex = 2;
			this.rbtnSSGenresGroup.TabStop = true;
			this.rbtnSSGenresGroup.Text = "Группа Жанров";
			this.rbtnSSGenresGroup.UseVisualStyleBackColor = true;
			this.rbtnSSGenresGroup.CheckedChanged += new System.EventHandler(this.RbtnSSGenresGroupCheckedChanged);
			// 
			// chkBoxGenre
			// 
			this.chkBoxGenre.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chkBoxGenre.Location = new System.Drawing.Point(16, 238);
			this.chkBoxGenre.Margin = new System.Windows.Forms.Padding(4);
			this.chkBoxGenre.Name = "chkBoxGenre";
			this.chkBoxGenre.Size = new System.Drawing.Size(87, 27);
			this.chkBoxGenre.TabIndex = 65;
			this.chkBoxGenre.Text = "Жанр:";
			this.chkBoxGenre.UseVisualStyleBackColor = true;
			this.chkBoxGenre.CheckedChanged += new System.EventHandler(this.ChkBoxGenreCheckedChanged);
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
			this.lvSSData.Location = new System.Drawing.Point(16, 349);
			this.lvSSData.Margin = new System.Windows.Forms.Padding(4);
			this.lvSSData.Name = "lvSSData";
			this.lvSSData.Size = new System.Drawing.Size(943, 180);
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
			this.btnCancel.Location = new System.Drawing.Point(609, 543);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(147, 32);
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
			this.btnOK.Location = new System.Drawing.Point(813, 543);
			this.btnOK.Margin = new System.Windows.Forms.Padding(4);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(147, 32);
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
			this.btnDelete.Location = new System.Drawing.Point(813, 192);
			this.btnDelete.Margin = new System.Windows.Forms.Padding(4);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(147, 32);
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
			this.btnAdd.Location = new System.Drawing.Point(813, 133);
			this.btnAdd.Margin = new System.Windows.Forms.Padding(4);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(147, 32);
			this.btnAdd.TabIndex = 63;
			this.btnAdd.Text = "Добавть";
			this.btnAdd.UseVisualStyleBackColor = true;
			this.btnAdd.Click += new System.EventHandler(this.BtnAddClick);
			// 
			// txtBoxInfo
			// 
			this.txtBoxInfo.Dock = System.Windows.Forms.DockStyle.Top;
			this.txtBoxInfo.Location = new System.Drawing.Point(0, 0);
			this.txtBoxInfo.Margin = new System.Windows.Forms.Padding(4);
			this.txtBoxInfo.Multiline = true;
			this.txtBoxInfo.Name = "txtBoxInfo";
			this.txtBoxInfo.ReadOnly = true;
			this.txtBoxInfo.Size = new System.Drawing.Size(979, 89);
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
			this.chBoxExactFit.Location = new System.Drawing.Point(531, 97);
			this.chBoxExactFit.Margin = new System.Windows.Forms.Padding(4);
			this.chBoxExactFit.Name = "chBoxExactFit";
			this.chBoxExactFit.Size = new System.Drawing.Size(207, 30);
			this.chBoxExactFit.TabIndex = 67;
			this.chBoxExactFit.Text = "Точное соответствие";
			this.chBoxExactFit.UseVisualStyleBackColor = true;
			// 
			// txtBoxSSBookTitle
			// 
			this.txtBoxSSBookTitle.Enabled = false;
			this.txtBoxSSBookTitle.Location = new System.Drawing.Point(132, 196);
			this.txtBoxSSBookTitle.Margin = new System.Windows.Forms.Padding(4);
			this.txtBoxSSBookTitle.Name = "txtBoxSSBookTitle";
			this.txtBoxSSBookTitle.Size = new System.Drawing.Size(653, 22);
			this.txtBoxSSBookTitle.TabIndex = 69;
			// 
			// chkBoxBookTitle
			// 
			this.chkBoxBookTitle.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chkBoxBookTitle.Location = new System.Drawing.Point(16, 193);
			this.chkBoxBookTitle.Margin = new System.Windows.Forms.Padding(4);
			this.chkBoxBookTitle.Name = "chkBoxBookTitle";
			this.chkBoxBookTitle.Size = new System.Drawing.Size(116, 30);
			this.chkBoxBookTitle.TabIndex = 68;
			this.chkBoxBookTitle.Text = "Название:";
			this.chkBoxBookTitle.UseVisualStyleBackColor = true;
			this.chkBoxBookTitle.CheckedChanged += new System.EventHandler(this.ChkBoxBookTitleCheckedChanged);
			this.chkBoxBookTitle.Click += new System.EventHandler(this.ChkBoxBookTitleClick);
			// 
			// lblInfo
			// 
			this.lblInfo.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.lblInfo.Location = new System.Drawing.Point(16, 543);
			this.lblInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblInfo.Name = "lblInfo";
			this.lblInfo.Size = new System.Drawing.Size(247, 28);
			this.lblInfo.TabIndex = 70;
			this.lblInfo.Text = "Число записей условий поиска:";
			// 
			// lblCount
			// 
			this.lblCount.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblCount.ForeColor = System.Drawing.Color.Navy;
			this.lblCount.Location = new System.Drawing.Point(261, 543);
			this.lblCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblCount.Name = "lblCount";
			this.lblCount.Size = new System.Drawing.Size(83, 28);
			this.lblCount.TabIndex = 71;
			this.lblCount.Text = "0";
			// 
			// btnDeleteAll
			// 
			this.btnDeleteAll.Enabled = false;
			this.btnDeleteAll.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.btnDeleteAll.Image = ((System.Drawing.Image)(resources.GetObject("btnDeleteAll.Image")));
			this.btnDeleteAll.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnDeleteAll.Location = new System.Drawing.Point(813, 242);
			this.btnDeleteAll.Margin = new System.Windows.Forms.Padding(4);
			this.btnDeleteAll.Name = "btnDeleteAll";
			this.btnDeleteAll.Size = new System.Drawing.Size(147, 32);
			this.btnDeleteAll.TabIndex = 72;
			this.btnDeleteAll.Text = "Удалить все";
			this.btnDeleteAll.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnDeleteAll.UseVisualStyleBackColor = true;
			this.btnDeleteAll.Click += new System.EventHandler(this.BtnDeleteAllClick);
			// 
			// SelectedSortDataForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(979, 586);
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
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SelectedSortDataForm";
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
		#endregion
		
		#region Закрытые данные класса
		private const string m_sTitle	= "SharpFBTools - Избранная Сортировка";
		private bool m_bOKClicked		= false;
		#endregion

		public SelectedSortDataForm()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			
			// формирование Списка Языков
			MakeListFMLangs();
			// Смена схемы Жанров
			GenresSchemeChange();
		}
		
		#region Закрытые вспомогательные методы класса
		// формирование Списка Языков
		private void MakeListFMLangs() {
			cmbBoxSSLang.Items.AddRange( Core.Common.LangList.LangsList );
			cmbBoxSSLang.SelectedIndex = 0;
		}
		
		// Смена схемы Жанров
		private void GenresSchemeChange() {
			// формирование Списка Групп Жанров
			WorksWithBooks.makeListGenresGroups( cmbBoxSSGenresGroup );
			// формирование Списка Жанров
			WorksWithBooks.makeListAllFullGenres( cmbBoxSSGenres );
		}
		
		// есть ли такая запись в списке
		private bool IsRecordExist() {
			if( lvSSData.Items.Count == 0 )
				return false;

			string sLang	= cmbBoxSSLang.Text;
			string sGG		= string.Empty;
			string sGenre	= string.Empty;
			if( chkBoxGenre.Checked ) {
				if( rbtnSSGenresGroup.Checked )
					sGG = cmbBoxSSGenresGroup.Text.Trim();
				else
					sGenre = cmbBoxSSGenres.Text.Trim();
			}
			string sLast	= !string.IsNullOrWhiteSpace(textBoxSSLast.Text) ? textBoxSSLast.Text.Trim()	: string.Empty;
			string sFirst	= !string.IsNullOrWhiteSpace(textBoxSSFirst.Text) ? textBoxSSFirst.Text.Trim() : string.Empty;
			string sMiddle	= !string.IsNullOrWhiteSpace(textBoxSSMiddle.Text) ? textBoxSSMiddle.Text.Trim() : string.Empty;
			string sNick	= !string.IsNullOrWhiteSpace(textBoxSSNick.Text) ? textBoxSSNick.Text.Trim()	: string.Empty;
			string sSeq		= !string.IsNullOrWhiteSpace(txtBoxSSSequence.Text) ? txtBoxSSSequence.Text.Trim() : string.Empty;
			string sBTitle	= !string.IsNullOrWhiteSpace(txtBoxSSBookTitle.Text)  ? txtBoxSSBookTitle.Text.Trim() : string.Empty;
			
			// перебираем все записи в списке
			for( int i = 0; i != lvSSData.Items.Count; ++i ) {
				if( lvSSData.Items[i].Text == sLang				&& 
				  lvSSData.Items[i].SubItems[1].Text == sGG		&& 
				  lvSSData.Items[i].SubItems[2].Text == sGenre	&&
				  lvSSData.Items[i].SubItems[3].Text == sLast	&&
				  lvSSData.Items[i].SubItems[4].Text == sFirst	&&
				  lvSSData.Items[i].SubItems[5].Text == sMiddle	&&
				  lvSSData.Items[i].SubItems[6].Text == sNick	&&
				  lvSSData.Items[i].SubItems[7].Text == sSeq	&&
				  lvSSData.Items[i].SubItems[8].Text == sBTitle ) {
					return true;
				}
			}
			return false;
		}
		#endregion
		
		#region Открытые методы
		public bool IsOKClicked() {
			return m_bOKClicked;
		}
		
		#endregion
		
		#region Обработчики событий
		void ChBoxSSLangCheckedChanged(object sender, EventArgs e)
		{
			cmbBoxSSLang.Enabled = chBoxSSLang.Checked;
		}
		
		void ChBoxAuthorCheckedChanged(object sender, EventArgs e)
		{
			textBoxSSLast.Enabled = textBoxSSFirst.Enabled = textBoxSSMiddle.Enabled = textBoxSSNick.Enabled = chBoxAuthor.Checked;
		}
		
		void ChBoxSSSequenceCheckedChanged(object sender, EventArgs e)
		{
			txtBoxSSSequence.Enabled = chBoxSSSequence.Checked;
		}
		
		void ChkBoxGenreCheckedChanged(object sender, EventArgs e)
		{
			gBoxGenre.Enabled = chkBoxGenre.Checked;
		}
		
		void RbtnSSGenresGroupCheckedChanged(object sender, EventArgs e)
		{
			cmbBoxSSGenresGroup.Enabled = rbtnSSGenresGroup.Checked;
		}
		
		void RbtnSSGenresCheckedChanged(object sender, EventArgs e)
		{
			cmbBoxSSGenres.Enabled = rbtnSSGenres.Checked;
		}

		void ChkBoxBookTitleCheckedChanged(object sender, EventArgs e)
		{
			txtBoxSSBookTitle.Enabled = chkBoxBookTitle.Checked;
		}
		
		void RbtnFMSSFB2LibrusecClick(object sender, EventArgs e)
		{
			GenresSchemeChange();
		}
		
		void RbtnFMSSFB22Click(object sender, EventArgs e)
		{
			GenresSchemeChange();
		}
		
		// Добавить данные сортировки в список
		void BtnAddClick(object sender, EventArgs e)
		{
			// проверка, выбранали хоть одна опция сортировки
			if( !chBoxSSLang.Checked && !chBoxAuthor.Checked &&
			   	!chkBoxGenre.Checked && !chBoxSSSequence.Checked &&
			    !chkBoxBookTitle.Checked ) {
				MessageBox.Show( "Выберите хоть одну опцию поиска для сортировки (чекбоксы)!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}

			// если выбран ТОЛЬКО Автор и (или) Серия и (или) Название Книги
			if( !chBoxSSLang.Checked && !chkBoxGenre.Checked) {
				if( chBoxAuthor.Checked ) {
					// выбран Автор - не пустые ли все его поля
					if( string.IsNullOrWhiteSpace( textBoxSSLast.Text.Trim() ) &&
					   string.IsNullOrWhiteSpace( textBoxSSFirst.Text.Trim() ) &&
					   string.IsNullOrWhiteSpace( textBoxSSMiddle.Text.Trim() ) &&
					   string.IsNullOrWhiteSpace( textBoxSSNick.Text.Trim() ) ) {
						MessageBox.Show( "Заполните хоть одно поле для Автора Книг!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
						return;
					}
				}
				if( chkBoxBookTitle.Checked ) {
					// выбрано Название Книги - не пустое ли ее поле
					if( string.IsNullOrWhiteSpace( txtBoxSSBookTitle.Text.Trim() ) ) {
						MessageBox.Show( "Заполните поле для Названия Книги!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
						return;
					}
				}
				if( chBoxSSSequence.Checked ) {
					// выбрана Серия - не пустое ли ее поле
					if( string.IsNullOrWhiteSpace( txtBoxSSSequence.Text.Trim() ) ) {
						MessageBox.Show( "Заполните поле для Серии Книги!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
						return;
					}
				}
			}
			
			// проверка, есть ли вводимые данные в списке
			if( IsRecordExist() ) {
				MessageBox.Show( "Вводимые данные уже есть в списке!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			
			ListViewItem lvi = null;
			// Язык Книги
			if( chBoxSSLang.Checked )
				lvi = new ListViewItem( cmbBoxSSLang.Text );
			else
				lvi = new ListViewItem( string.Empty );
			
			// Жанр Книги
			if( chkBoxGenre.Checked ) {
				if( rbtnSSGenresGroup.Checked ) {
					lvi.SubItems.Add( cmbBoxSSGenresGroup.Text.Trim() );
					lvi.SubItems.Add( string.Empty );
				} else {
					lvi.SubItems.Add( string.Empty );
					lvi.SubItems.Add( cmbBoxSSGenres.Text.Trim() );
				}
			} else {
				for( int i=0; i!=2; ++i )
					lvi.SubItems.Add( string.Empty );
			}
			
			// Автор Книги
			if( chBoxAuthor.Checked ) {
				if( textBoxSSLast.Text.Trim().Length!=0 )
					lvi.SubItems.Add( textBoxSSLast.Text.Trim() );
				else
					lvi.SubItems.Add( string.Empty );
				
				if( textBoxSSFirst.Text.Trim().Length!=0 )
					lvi.SubItems.Add( textBoxSSFirst.Text.Trim() );
				else
					lvi.SubItems.Add( string.Empty );
				
				if( textBoxSSMiddle.Text.Trim().Length!=0 )
					lvi.SubItems.Add( textBoxSSMiddle.Text.Trim() );
				else
					lvi.SubItems.Add( string.Empty );
				
				if( textBoxSSNick.Text.Trim().Length!=0 )
					lvi.SubItems.Add( textBoxSSNick.Text.Trim() );
				else
					lvi.SubItems.Add( string.Empty );
			} else {
				for( int i=0; i!=4; ++i )
					lvi.SubItems.Add( string.Empty );
			}

			// Серия Книги
			if( chBoxSSSequence.Checked ) {
				if( txtBoxSSSequence.Text.Trim().Length!=0 )
					lvi.SubItems.Add( txtBoxSSSequence.Text.Trim() );
				else
					lvi.SubItems.Add( string.Empty );
			} else
				lvi.SubItems.Add( string.Empty );
			
			// Название Книги
			if( chkBoxBookTitle.Checked ) {
				if( txtBoxSSBookTitle.Text.Trim().Length!=0 )
					lvi.SubItems.Add( txtBoxSSBookTitle.Text.Trim() );
				else
					lvi.SubItems.Add( string.Empty );
			} else
				lvi.SubItems.Add( string.Empty );
			
			// Точное соответствие
			if( chBoxExactFit.Checked )
				lvi.SubItems.Add( "Да" );
			else
				lvi.SubItems.Add( "Нет" );
			
			// добавление записи в список
			lvSSData.Items.Add( lvi );
			for( int i=0; i!=lvSSData.Items.Count; ++i )
				lvSSData.Items[ i ].Selected = false;

			lvSSData.Items[ lvSSData.Items.Count-1 ].Selected	= true;
			lvSSData.Items[ lvSSData.Items.Count-1 ].Focused	= true;
			
			// очищаем поля ввода
			textBoxSSLast.Text = textBoxSSFirst.Text = textBoxSSMiddle.Text = textBoxSSNick.Text = 
								txtBoxSSSequence.Text = txtBoxSSBookTitle.Text = string.Empty;
			lblCount.Text = Convert.ToString( lvSSData.Items.Count );
			btnOK.Enabled = true;
		}
		
		void LvSSDataSelectedIndexChanged(object sender, EventArgs e)
		{
			if( lvSSData.SelectedItems.Count == 0 )
				btnDelete.Enabled		= false;
			else {
				btnDelete.Enabled		= true;
				btnDeleteAll.Enabled	= true;
			}
		}
		
		// удаление данных сортировки из списка
		void BtnDeleteClick(object sender, EventArgs e)
		{
			const string sMess = "Вы действительно хотите удалить выбранные данные из списка?";
			const MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			DialogResult result;
			result = MessageBox.Show( sMess, m_sTitle, buttons, MessageBoxIcon.Question );
	        if(result == DialogResult.No)
	            return;

			lvSSData.Items.Remove( lvSSData.SelectedItems[0] );
			
			if( lvSSData.SelectedItems.Count == 0 )
				btnOK.Enabled = false;
			else
				btnOK.Enabled = true;
			
			lblCount.Text = Convert.ToString( lvSSData.Items.Count );
			
			if( lvSSData.Items.Count > 0 )
				btnOK.Enabled			= true;
			else {
				btnDelete.Enabled 		= false;
				btnDeleteAll.Enabled	= false;
			}
		}
		
		// удаление всех данных сортировки из списка
		void BtnDeleteAllClick(object sender, EventArgs e)
		{
			const string sMess = "Вы действительно хотите удалить ВСЕ данные из списка?";
			const MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			DialogResult result;
			result = MessageBox.Show( sMess, m_sTitle, buttons, MessageBoxIcon.Question );
	        if(result == DialogResult.No)
	            return;
			
			lvSSData.Items.Clear();
			
			btnDelete.Enabled 		= false;
			btnDeleteAll.Enabled	= false;
			//btnOK.Enabled 			= false;
			
			lblCount.Text = Convert.ToString( lvSSData.Items.Count );
		}
		
		// принять данные
		void BtnOKClick(object sender, EventArgs e)
		{
			m_bOKClicked = true;
			this.Close();
		}
		
		void ChBoxAuthorClick(object sender, EventArgs e)
		{
			textBoxSSLast.Focus();
		}
		
		void ChBoxSSSequenceClick(object sender, EventArgs e)
		{
			txtBoxSSSequence.Focus();
		}
		
		void ChkBoxBookTitleClick(object sender, EventArgs e)
		{
			txtBoxSSBookTitle.Focus();
		}
		
		void SelectedSortDataShown(object sender, EventArgs e)
		{
			if( lvSSData.Items.Count > 0 )
				btnDeleteAll.Enabled = true;
		}
		#endregion
	}
}
