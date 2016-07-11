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

using Core.Common;

using CriteriasViewCollumnEnum = Core.Common.Enums.CriteriasViewCollumnEnum;

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
			this.cmbBoxLang = new System.Windows.Forms.ComboBox();
			this.chBoxLang = new System.Windows.Forms.CheckBox();
			this.txtBoxSequence = new System.Windows.Forms.TextBox();
			this.chBoxSequence = new System.Windows.Forms.CheckBox();
			this.lblNick = new System.Windows.Forms.Label();
			this.textBoxNick = new System.Windows.Forms.TextBox();
			this.lblMiddle = new System.Windows.Forms.Label();
			this.textBoxMiddle = new System.Windows.Forms.TextBox();
			this.lblFirst = new System.Windows.Forms.Label();
			this.chBoxAuthor = new System.Windows.Forms.CheckBox();
			this.textBoxLast = new System.Windows.Forms.TextBox();
			this.textBoxFirst = new System.Windows.Forms.TextBox();
			this.lblLast = new System.Windows.Forms.Label();
			this.gBoxGenre = new System.Windows.Forms.GroupBox();
			this.cmbBoxGenres = new System.Windows.Forms.ComboBox();
			this.cmbBoxGenresGroup = new System.Windows.Forms.ComboBox();
			this.rbtnGenres = new System.Windows.Forms.RadioButton();
			this.rbtnGenresGroup = new System.Windows.Forms.RadioButton();
			this.chkBoxGenre = new System.Windows.Forms.CheckBox();
			this.lvData = new System.Windows.Forms.ListView();
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
			this.txtBoxBookTitle = new System.Windows.Forms.TextBox();
			this.chkBoxBookTitle = new System.Windows.Forms.CheckBox();
			this.lblInfo = new System.Windows.Forms.Label();
			this.lblCount = new System.Windows.Forms.Label();
			this.btnDeleteAll = new System.Windows.Forms.Button();
			this.gBoxGenre.SuspendLayout();
			this.SuspendLayout();
			// 
			// cmbBoxLang
			// 
			this.cmbBoxLang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbBoxLang.Enabled = false;
			this.cmbBoxLang.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.cmbBoxLang.FormattingEnabled = true;
			this.cmbBoxLang.Location = new System.Drawing.Point(132, 97);
			this.cmbBoxLang.Margin = new System.Windows.Forms.Padding(4);
			this.cmbBoxLang.Name = "cmbBoxLang";
			this.cmbBoxLang.Size = new System.Drawing.Size(341, 24);
			this.cmbBoxLang.TabIndex = 36;
			// 
			// chBoxLang
			// 
			this.chBoxLang.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxLang.Location = new System.Drawing.Point(16, 97);
			this.chBoxLang.Margin = new System.Windows.Forms.Padding(4);
			this.chBoxLang.Name = "chBoxLang";
			this.chBoxLang.Size = new System.Drawing.Size(87, 30);
			this.chBoxLang.TabIndex = 35;
			this.chBoxLang.Text = "Язык:";
			this.chBoxLang.UseVisualStyleBackColor = true;
			this.chBoxLang.CheckedChanged += new System.EventHandler(this.ChBoxSSLangCheckedChanged);
			// 
			// txtBoxSequence
			// 
			this.txtBoxSequence.Enabled = false;
			this.txtBoxSequence.Location = new System.Drawing.Point(132, 343);
			this.txtBoxSequence.Margin = new System.Windows.Forms.Padding(4);
			this.txtBoxSequence.Name = "txtBoxSequence";
			this.txtBoxSequence.Size = new System.Drawing.Size(827, 22);
			this.txtBoxSequence.TabIndex = 46;
			// 
			// chBoxSequence
			// 
			this.chBoxSequence.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxSequence.Location = new System.Drawing.Point(16, 341);
			this.chBoxSequence.Margin = new System.Windows.Forms.Padding(4);
			this.chBoxSequence.Name = "chBoxSequence";
			this.chBoxSequence.Size = new System.Drawing.Size(87, 30);
			this.chBoxSequence.TabIndex = 45;
			this.chBoxSequence.Text = "Серия:";
			this.chBoxSequence.UseVisualStyleBackColor = true;
			this.chBoxSequence.CheckedChanged += new System.EventHandler(this.ChBoxSSSequenceCheckedChanged);
			this.chBoxSequence.Click += new System.EventHandler(this.ChBoxSSSequenceClick);
			// 
			// lblNick
			// 
			this.lblNick.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblNick.ForeColor = System.Drawing.Color.Navy;
			this.lblNick.Location = new System.Drawing.Point(483, 158);
			this.lblNick.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblNick.Name = "lblNick";
			this.lblNick.Size = new System.Drawing.Size(44, 30);
			this.lblNick.TabIndex = 57;
			this.lblNick.Text = "Ник";
			this.lblNick.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBoxNick
			// 
			this.textBoxNick.Enabled = false;
			this.textBoxNick.Location = new System.Drawing.Point(531, 164);
			this.textBoxNick.Margin = new System.Windows.Forms.Padding(4);
			this.textBoxNick.Name = "textBoxNick";
			this.textBoxNick.Size = new System.Drawing.Size(255, 22);
			this.textBoxNick.TabIndex = 56;
			// 
			// lblMiddle
			// 
			this.lblMiddle.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblMiddle.ForeColor = System.Drawing.Color.Navy;
			this.lblMiddle.Location = new System.Drawing.Point(132, 158);
			this.lblMiddle.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblMiddle.Name = "lblMiddle";
			this.lblMiddle.Size = new System.Drawing.Size(85, 30);
			this.lblMiddle.TabIndex = 55;
			this.lblMiddle.Text = "Отчество";
			this.lblMiddle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textBoxMiddle
			// 
			this.textBoxMiddle.Enabled = false;
			this.textBoxMiddle.Location = new System.Drawing.Point(219, 164);
			this.textBoxMiddle.Margin = new System.Windows.Forms.Padding(4);
			this.textBoxMiddle.Name = "textBoxMiddle";
			this.textBoxMiddle.Size = new System.Drawing.Size(255, 22);
			this.textBoxMiddle.TabIndex = 54;
			// 
			// lblFirst
			// 
			this.lblFirst.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblFirst.ForeColor = System.Drawing.Color.Navy;
			this.lblFirst.Location = new System.Drawing.Point(483, 130);
			this.lblFirst.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblFirst.Name = "lblFirst";
			this.lblFirst.Size = new System.Drawing.Size(47, 30);
			this.lblFirst.TabIndex = 53;
			this.lblFirst.Text = "Имя";
			this.lblFirst.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
			// textBoxLast
			// 
			this.textBoxLast.Enabled = false;
			this.textBoxLast.Location = new System.Drawing.Point(219, 134);
			this.textBoxLast.Margin = new System.Windows.Forms.Padding(4);
			this.textBoxLast.Name = "textBoxLast";
			this.textBoxLast.Size = new System.Drawing.Size(255, 22);
			this.textBoxLast.TabIndex = 50;
			// 
			// textBoxFirst
			// 
			this.textBoxFirst.Enabled = false;
			this.textBoxFirst.Location = new System.Drawing.Point(531, 134);
			this.textBoxFirst.Margin = new System.Windows.Forms.Padding(4);
			this.textBoxFirst.Name = "textBoxFirst";
			this.textBoxFirst.Size = new System.Drawing.Size(255, 22);
			this.textBoxFirst.TabIndex = 52;
			// 
			// lblLast
			// 
			this.lblLast.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblLast.ForeColor = System.Drawing.Color.Navy;
			this.lblLast.Location = new System.Drawing.Point(132, 130);
			this.lblLast.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblLast.Name = "lblLast";
			this.lblLast.Size = new System.Drawing.Size(79, 30);
			this.lblLast.TabIndex = 51;
			this.lblLast.Text = "Фамилия";
			this.lblLast.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// gBoxGenre
			// 
			this.gBoxGenre.Controls.Add(this.cmbBoxGenres);
			this.gBoxGenre.Controls.Add(this.cmbBoxGenresGroup);
			this.gBoxGenre.Controls.Add(this.rbtnGenres);
			this.gBoxGenre.Controls.Add(this.rbtnGenresGroup);
			this.gBoxGenre.Enabled = false;
			this.gBoxGenre.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gBoxGenre.Location = new System.Drawing.Point(132, 223);
			this.gBoxGenre.Margin = new System.Windows.Forms.Padding(4);
			this.gBoxGenre.Name = "gBoxGenre";
			this.gBoxGenre.Padding = new System.Windows.Forms.Padding(4);
			this.gBoxGenre.Size = new System.Drawing.Size(655, 111);
			this.gBoxGenre.TabIndex = 58;
			this.gBoxGenre.TabStop = false;
			// 
			// cmbBoxGenres
			// 
			this.cmbBoxGenres.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbBoxGenres.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.cmbBoxGenres.FormattingEnabled = true;
			this.cmbBoxGenres.Location = new System.Drawing.Point(11, 51);
			this.cmbBoxGenres.Margin = new System.Windows.Forms.Padding(4);
			this.cmbBoxGenres.Name = "cmbBoxGenres";
			this.cmbBoxGenres.Size = new System.Drawing.Size(472, 24);
			this.cmbBoxGenres.Sorted = true;
			this.cmbBoxGenres.TabIndex = 3;
			// 
			// cmbBoxGenresGroup
			// 
			this.cmbBoxGenresGroup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbBoxGenresGroup.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.cmbBoxGenresGroup.FormattingEnabled = true;
			this.cmbBoxGenresGroup.Location = new System.Drawing.Point(11, 19);
			this.cmbBoxGenresGroup.Margin = new System.Windows.Forms.Padding(4);
			this.cmbBoxGenresGroup.Name = "cmbBoxGenresGroup";
			this.cmbBoxGenresGroup.Size = new System.Drawing.Size(472, 24);
			this.cmbBoxGenresGroup.Sorted = true;
			this.cmbBoxGenresGroup.TabIndex = 2;
			this.cmbBoxGenresGroup.SelectedIndexChanged += new System.EventHandler(this.CmbBoxSSGenresGroupSelectedIndexChanged);
			// 
			// rbtnGenres
			// 
			this.rbtnGenres.ForeColor = System.Drawing.Color.Navy;
			this.rbtnGenres.Location = new System.Drawing.Point(233, 78);
			this.rbtnGenres.Margin = new System.Windows.Forms.Padding(4);
			this.rbtnGenres.Name = "rbtnGenres";
			this.rbtnGenres.Size = new System.Drawing.Size(223, 30);
			this.rbtnGenres.TabIndex = 5;
			this.rbtnGenres.Text = "Только выбранный Жанр";
			this.rbtnGenres.UseVisualStyleBackColor = true;
			// 
			// rbtnGenresGroup
			// 
			this.rbtnGenresGroup.Checked = true;
			this.rbtnGenresGroup.ForeColor = System.Drawing.Color.Navy;
			this.rbtnGenresGroup.Location = new System.Drawing.Point(11, 78);
			this.rbtnGenresGroup.Margin = new System.Windows.Forms.Padding(4);
			this.rbtnGenresGroup.Name = "rbtnGenresGroup";
			this.rbtnGenresGroup.Size = new System.Drawing.Size(183, 30);
			this.rbtnGenresGroup.TabIndex = 4;
			this.rbtnGenresGroup.TabStop = true;
			this.rbtnGenresGroup.Text = "Жанры всей Группы";
			this.rbtnGenresGroup.UseVisualStyleBackColor = true;
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
			// lvData
			// 
			this.lvData.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
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
			this.lvData.FullRowSelect = true;
			this.lvData.GridLines = true;
			this.lvData.HideSelection = false;
			this.lvData.Location = new System.Drawing.Point(16, 379);
			this.lvData.Margin = new System.Windows.Forms.Padding(4);
			this.lvData.Name = "lvData";
			this.lvData.Size = new System.Drawing.Size(943, 163);
			this.lvData.TabIndex = 60;
			this.lvData.UseCompatibleStateImageBehavior = false;
			this.lvData.View = System.Windows.Forms.View.Details;
			this.lvData.SelectedIndexChanged += new System.EventHandler(this.LvSSDataSelectedIndexChanged);
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
			this.btnCancel.Location = new System.Drawing.Point(609, 549);
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
			this.btnOK.Location = new System.Drawing.Point(813, 549);
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
			// txtBoxBookTitle
			// 
			this.txtBoxBookTitle.Enabled = false;
			this.txtBoxBookTitle.Location = new System.Drawing.Point(132, 196);
			this.txtBoxBookTitle.Margin = new System.Windows.Forms.Padding(4);
			this.txtBoxBookTitle.Name = "txtBoxBookTitle";
			this.txtBoxBookTitle.Size = new System.Drawing.Size(653, 22);
			this.txtBoxBookTitle.TabIndex = 69;
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
			this.lblInfo.Location = new System.Drawing.Point(16, 553);
			this.lblInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblInfo.Name = "lblInfo";
			this.lblInfo.Size = new System.Drawing.Size(247, 28);
			this.lblInfo.TabIndex = 70;
			this.lblInfo.Text = "Число записей условий поиска:";
			this.lblInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblCount
			// 
			this.lblCount.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblCount.ForeColor = System.Drawing.Color.Navy;
			this.lblCount.Location = new System.Drawing.Point(261, 553);
			this.lblCount.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblCount.Name = "lblCount";
			this.lblCount.Size = new System.Drawing.Size(83, 28);
			this.lblCount.TabIndex = 71;
			this.lblCount.Text = "0";
			this.lblCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
			this.Controls.Add(this.txtBoxBookTitle);
			this.Controls.Add(this.chkBoxBookTitle);
			this.Controls.Add(this.chBoxExactFit);
			this.Controls.Add(this.txtBoxInfo);
			this.Controls.Add(this.chkBoxGenre);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.lvData);
			this.Controls.Add(this.gBoxGenre);
			this.Controls.Add(this.lblNick);
			this.Controls.Add(this.textBoxNick);
			this.Controls.Add(this.lblMiddle);
			this.Controls.Add(this.textBoxMiddle);
			this.Controls.Add(this.lblFirst);
			this.Controls.Add(this.chBoxAuthor);
			this.Controls.Add(this.textBoxLast);
			this.Controls.Add(this.textBoxFirst);
			this.Controls.Add(this.lblLast);
			this.Controls.Add(this.txtBoxSequence);
			this.Controls.Add(this.chBoxSequence);
			this.Controls.Add(this.cmbBoxLang);
			this.Controls.Add(this.chBoxLang);
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
		private System.Windows.Forms.TextBox txtBoxBookTitle;
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
		public System.Windows.Forms.ListView lvData;
		private System.Windows.Forms.ComboBox cmbBoxGenresGroup;
		private System.Windows.Forms.ComboBox cmbBoxGenres;
		private System.Windows.Forms.RadioButton rbtnGenresGroup;
		private System.Windows.Forms.RadioButton rbtnGenres;
		private System.Windows.Forms.GroupBox gBoxGenre;
		private System.Windows.Forms.Label lblLast;
		private System.Windows.Forms.TextBox textBoxFirst;
		private System.Windows.Forms.TextBox textBoxLast;
		private System.Windows.Forms.CheckBox chBoxAuthor;
		private System.Windows.Forms.Label lblFirst;
		private System.Windows.Forms.TextBox textBoxMiddle;
		private System.Windows.Forms.Label lblMiddle;
		private System.Windows.Forms.TextBox textBoxNick;
		private System.Windows.Forms.Label lblNick;
		private System.Windows.Forms.CheckBox chBoxSequence;
		private System.Windows.Forms.TextBox txtBoxSequence;
		private System.Windows.Forms.CheckBox chBoxLang;
		private System.Windows.Forms.ComboBox cmbBoxLang;
		#endregion
		
		#region Закрытые данные класса
		private const string m_sTitle	= "SharpFBTools - Избранная Сортировка";
		private bool m_bOKClicked		= false;
		#endregion

		public SelectedSortDataForm()
		{
			InitializeComponent();
			
			// формирование Списка Языков
			MakeListFMLangs();
			// формирование Списка Групп Жанров
			WorksWithBooks.makeListGenresGroups( cmbBoxGenresGroup );
		}
		
		#region Закрытые вспомогательные методы класса
		/// <summary>
		/// формирование Списка Языков
		/// </summary>
		private void MakeListFMLangs() {
			cmbBoxLang.Items.AddRange( Core.Common.LangList.LangsList );
			cmbBoxLang.SelectedIndex = 0;
		}
		
		/// <summary>
		/// Извлечение кода языка
		/// </summary>
		/// <returns>Код Языка типа string</returns>
		private string getLangCode( string sLang ) {
			return (sLang.Substring(
				sLang.IndexOf('(')+1, sLang.IndexOf(')') - sLang.IndexOf('(') - 1).Trim()
			       );
		}
		/// <summary>
		/// Извлечение кода жанра
		/// </summary>
		/// <returns>Код Жанра типа string</returns>
		private string getGenreCode( string sGenre ) {
			return (sGenre.Substring(
				sGenre.IndexOf('(')+1, sGenre.IndexOf(')') - sGenre.IndexOf('(') - 1).Trim()
			       );
		}
		
		/// <summary>
		/// Есть ли такая запись в списке (при добавлении только одного Жанра)
		/// </summary>
		/// <param name="GenreGroup">Название Группы Жанров</param>
		/// <param name="GenreCode">Код Жанра</param>
		/// <returns>true - если добавляемая запись уже есть в списке; false - если нет</returns>
		private bool isRecordExistForGenre( string GenreGroup, string GenreCode ) {
			if( lvData.Items.Count == 0 )
				return false;

			string sLang		= chBoxLang.Checked ? getLangCode( cmbBoxLang.Text.Trim() ) : string.Empty;
			string sGenreGroup	= chkBoxGenre.Checked ? GenreGroup : string.Empty;
			string sGenre		= chkBoxGenre.Checked ? GenreCode : string.Empty;
			string sLast	= !string.IsNullOrWhiteSpace(textBoxLast.Text) ? textBoxLast.Text.Trim()	: string.Empty;
			string sFirst	= !string.IsNullOrWhiteSpace(textBoxFirst.Text) ? textBoxFirst.Text.Trim() : string.Empty;
			string sMiddle	= !string.IsNullOrWhiteSpace(textBoxMiddle.Text) ? textBoxMiddle.Text.Trim() : string.Empty;
			string sNick	= !string.IsNullOrWhiteSpace(textBoxNick.Text) ? textBoxNick.Text.Trim()	: string.Empty;
			string sSeq		= !string.IsNullOrWhiteSpace(txtBoxSequence.Text) ? txtBoxSequence.Text.Trim() : string.Empty;
			string sBTitle	= !string.IsNullOrWhiteSpace(txtBoxBookTitle.Text) ? txtBoxBookTitle.Text.Trim() : string.Empty;
			string ExactFit = chBoxExactFit.Checked ? "true" : "false";
			
			// перебираем все записи в списке
			for ( int i = 0; i != lvData.Items.Count; ++i ) {
				if (lvData.Items[i].SubItems[ (int)CriteriasViewCollumnEnum.Lang ].Text == sLang	&&
				    lvData.Items[i].SubItems[ (int)CriteriasViewCollumnEnum.GenresGroup ].Text == sGenreGroup	&&
				    lvData.Items[i].SubItems[ (int)CriteriasViewCollumnEnum.Genre ].Text == sGenre	&&
				    lvData.Items[i].SubItems[ (int)CriteriasViewCollumnEnum.Last ].Text == sLast	&&
				    lvData.Items[i].SubItems[ (int)CriteriasViewCollumnEnum.First ].Text == sFirst	&&
				    lvData.Items[i].SubItems[ (int)CriteriasViewCollumnEnum.Middle ].Text == sMiddle	&&
				    lvData.Items[i].SubItems[ (int)CriteriasViewCollumnEnum.Nick ].Text == sNick	&&
				    lvData.Items[i].SubItems[ (int)CriteriasViewCollumnEnum.Sequence ].Text == sSeq	&&
				    lvData.Items[i].SubItems[ (int)CriteriasViewCollumnEnum.BookTitle ].Text == sBTitle &&
				    lvData.Items[i].SubItems[ (int)CriteriasViewCollumnEnum.ExactFit ].Text == ExactFit) {
					return true;
				}
			}
			return false;
		}
		
		/// <summary>
		/// Формирование subItems для отдельной записи критерия поиска
		/// </summary>
		/// <param name="lvi">Item класса ListViewItem с пустыми SubTems</param>
		/// <param name="GenreGroup">Название Группы Жанров</param>
		/// <param name="GenreCode">Код Жанра</param>
		/// <returns>Сформированный Item класса ListViewItem</returns>
		private ListViewItem makeCriteriaSubItem( ListViewItem lvi, string GenreGroup, string GenreCode ) {
			// Язык Книги
			if ( chBoxLang.Checked )
				lvi.SubItems[ (int)CriteriasViewCollumnEnum.Lang ].Text = getLangCode( cmbBoxLang.Text );
			
			// Жанр Книги
			if ( chkBoxGenre.Checked ) {
				lvi.SubItems[ (int)CriteriasViewCollumnEnum.GenresGroup ].Text = GenreGroup;
				lvi.SubItems[ (int)CriteriasViewCollumnEnum.Genre ].Text = GenreCode;
			}
			
			// Автор Книги
			if ( chBoxAuthor.Checked ) {
				if ( ! string.IsNullOrWhiteSpace( textBoxLast.Text.Trim() ) )
					lvi.SubItems[ (int)CriteriasViewCollumnEnum.Last ].Text = textBoxLast.Text.Trim();
				
				if ( ! string.IsNullOrWhiteSpace( textBoxFirst.Text.Trim() ) )
					lvi.SubItems[ (int)CriteriasViewCollumnEnum.First ].Text = textBoxFirst.Text.Trim();
				
				if ( ! string.IsNullOrWhiteSpace( textBoxMiddle.Text.Trim() ) )
					lvi.SubItems[ (int)CriteriasViewCollumnEnum.Middle ].Text = textBoxMiddle.Text.Trim();
				
				if ( ! string.IsNullOrWhiteSpace( textBoxNick.Text.Trim() ) )
					lvi.SubItems[ (int)CriteriasViewCollumnEnum.Nick ].Text = textBoxNick.Text.Trim();
			}

			// Серия Книги
			if ( chBoxSequence.Checked ) {
				if ( ! string.IsNullOrWhiteSpace( txtBoxSequence.Text.Trim() ) )
					lvi.SubItems[ (int)CriteriasViewCollumnEnum.Sequence ].Text = txtBoxSequence.Text.Trim();
			}
			
			// Название Книги
			if ( chkBoxBookTitle.Checked ) {
				if ( ! string.IsNullOrWhiteSpace( txtBoxBookTitle.Text.Trim() ) )
					lvi.SubItems[ (int)CriteriasViewCollumnEnum.BookTitle ].Text = txtBoxBookTitle.Text.Trim();
			}
			
			// Точное соответствие
			lvi.SubItems[ (int)CriteriasViewCollumnEnum.ExactFit ].Text = chBoxExactFit.Checked ? "true" : "false";
			return lvi;
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
			cmbBoxLang.Enabled = chBoxLang.Checked;
		}
		
		void ChBoxAuthorCheckedChanged(object sender, EventArgs e)
		{
			textBoxLast.Enabled = textBoxFirst.Enabled = textBoxMiddle.Enabled = textBoxNick.Enabled = chBoxAuthor.Checked;
		}
		
		void ChBoxSSSequenceCheckedChanged(object sender, EventArgs e)
		{
			txtBoxSequence.Enabled = chBoxSequence.Checked;
		}
		
		void ChkBoxGenreCheckedChanged(object sender, EventArgs e)
		{
			gBoxGenre.Enabled = chkBoxGenre.Checked;
		}

		void ChkBoxBookTitleCheckedChanged(object sender, EventArgs e)
		{
			txtBoxBookTitle.Enabled = chkBoxBookTitle.Checked;
		}

		// Добавить данные сортировки в список
		void BtnAddClick(object sender, EventArgs e)
		{
			// проверка, выбранали хоть одна опция сортировки
			if ( !chBoxLang.Checked && !chBoxAuthor.Checked &&
			    !chkBoxGenre.Checked && !chBoxSequence.Checked &&
			    !chkBoxBookTitle.Checked ) {
				MessageBox.Show( "Выберите хоть одну опцию поиска для сортировки (чекбоксы)!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}

			// если выбран ТОЛЬКО Автор и (или) Серия и (или) Название Книги
			if ( !chBoxLang.Checked && !chkBoxGenre.Checked) {
				if ( chBoxAuthor.Checked ) {
					// выбран Автор - не пустые ли все его поля
					if ( string.IsNullOrWhiteSpace( textBoxLast.Text.Trim() ) &&
					    string.IsNullOrWhiteSpace( textBoxFirst.Text.Trim() ) &&
					    string.IsNullOrWhiteSpace( textBoxMiddle.Text.Trim() ) &&
					    string.IsNullOrWhiteSpace( textBoxNick.Text.Trim() ) ) {
						MessageBox.Show( "Заполните хоть одно поле для Автора Книг!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
						return;
					}
				}
				if ( chkBoxBookTitle.Checked ) {
					// выбрано Название Книги - не пустое ли ее поле
					if ( string.IsNullOrWhiteSpace( txtBoxBookTitle.Text.Trim() ) ) {
						MessageBox.Show( "Заполните поле для Названия Книги!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
						return;
					}
				}
				if ( chBoxSequence.Checked ) {
					// выбрана Серия - не пустое ли ее поле
					if ( string.IsNullOrWhiteSpace( txtBoxSequence.Text.Trim() ) ) {
						MessageBox.Show( "Заполните поле для Серии Книги!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
						return;
					}
				}
			}
			
			int SubItemsCount = lvData.Columns.Count;
			// Добавление записи критерия поиска в список, в зависимости от всей Группы или отдельного Жанра Книги
			if ( chkBoxGenre.Checked ) {
				// Жанры участвуют в критерии поиска
				if ( rbtnGenresGroup.Checked ) {
					for ( int i = 0; i != cmbBoxGenres.Items.Count; ++i ) {
						string GenreCode = getGenreCode ( cmbBoxGenres.Items[i].ToString().Trim() ); // Код Жанра
						// добавление записи в список критериев
						if ( ! isRecordExistForGenre( cmbBoxGenresGroup.Text.Trim(), GenreCode ) ) {
							lvData.Items.Add(
								makeCriteriaSubItem(
									MiscListView.makeEmptyListViewItem( SubItemsCount ), // Геренация пустой записи критерия поиска
									cmbBoxGenresGroup.Text.Trim(),			// Название Группы Жанра
									GenreCode								// Код Жанра
								) // Формирование subItems для отдельной записи критерия поиска
							);
						}
					}
				} else {
					// проверка, есть ли вводимые данные в списке (при добавлении только одного Жанра)
					if ( isRecordExistForGenre(
						cmbBoxGenresGroup.Text.Trim(),
						getGenreCode ( cmbBoxGenres.Text.Trim() )
					) ) {
						MessageBox.Show( "Вводимые данные уже есть в списке критериев!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
						return;
					}
					// добавление записи в список критериев
					lvData.Items.Add(
						makeCriteriaSubItem(
							MiscListView.makeEmptyListViewItem( SubItemsCount ), // Геренация пустой записи критерия поиска
							cmbBoxGenresGroup.Text.Trim(), 				// Название Группы Жанра
							getGenreCode ( cmbBoxGenres.Text.Trim() )	// Код Жанра
						) // Формирование subItems для отдельной записи критерия поиска
					);
				}
			} else {
				// Жанры не участвуют в критерии поиска
				// проверка, есть ли вводимые данные в списке (при добавлении только одного Жанра)
				if ( isRecordExistForGenre( string.Empty, string.Empty ) ) {
					MessageBox.Show( "Вводимые данные уже есть в списке критериев!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return;
				}
				// добавление записи в список критериев
				lvData.Items.Add(
					makeCriteriaSubItem(
						MiscListView.makeEmptyListViewItem( SubItemsCount ), // Геренация пустой записи критерия поиска
						string.Empty, 	// Название Группы Жанра
						string.Empty	// Код Жанра
					) // Формирование subItems для отдельной записи критерия поиска
				);
			}
			
			// Снимаем выделения со всех итемов, если они есть
			for ( int i = 0; i != lvData.Items.Count; ++i )
				lvData.Items[ i ].Selected = false;
			// Выделяем последний итем в списке
			lvData.Items[ lvData.Items.Count-1 ].Selected	= true;
			lvData.Items[ lvData.Items.Count-1 ].Focused	= true;
			// очищаем поля ввода
			textBoxLast.Text = textBoxFirst.Text = textBoxMiddle.Text = textBoxNick.Text =
				txtBoxSequence.Text = txtBoxBookTitle.Text = string.Empty;
			// выводим число записей в списке
			lblCount.Text = Convert.ToString( lvData.Items.Count );
			// Разблокировка кнопки OK
			btnOK.Enabled = true;
		}
		
		void LvSSDataSelectedIndexChanged(object sender, EventArgs e)
		{
			if( lvData.SelectedItems.Count == 0 )
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
			if (result == DialogResult.No)
				return;

			lvData.Items.Remove( lvData.SelectedItems[0] );
			
			btnOK.Enabled = lvData.SelectedItems.Count == 0 ? false : true;
			
			lblCount.Text = Convert.ToString( lvData.Items.Count );
			
			if ( lvData.Items.Count > 0 )
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
			if (result == DialogResult.No)
				return;
			
			lvData.Items.Clear();
			
			btnDelete.Enabled 		= false;
			btnDeleteAll.Enabled	= false;
			//btnOK.Enabled 			= false;
			
			lblCount.Text = Convert.ToString( lvData.Items.Count );
		}
		
		// принять данные
		void BtnOKClick(object sender, EventArgs e)
		{
			m_bOKClicked = true;
			this.Close();
		}
		
		void ChBoxAuthorClick(object sender, EventArgs e)
		{
			textBoxLast.Focus();
		}
		
		void ChBoxSSSequenceClick(object sender, EventArgs e)
		{
			txtBoxSequence.Focus();
		}
		
		void ChkBoxBookTitleClick(object sender, EventArgs e)
		{
			txtBoxBookTitle.Focus();
		}
		
		void SelectedSortDataShown(object sender, EventArgs e)
		{
			if ( lvData.Items.Count > 0 )
				btnDeleteAll.Enabled = true;
		}
		
		void CmbBoxSSGenresGroupSelectedIndexChanged(object sender, EventArgs e)
		{
			// формирование Списка Жанров в контролы, в зависимости от Группы
			WorksWithBooks.makeListGenres( cmbBoxGenres, cmbBoxGenresGroup.Text );
		}
		#endregion
	}
}
