/*
 * Created by SharpDevelop.
 * User: vadim
 * Date: 05.04.2009
 * Time: 14:31
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace Options
{
	partial class OptionsForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OptionsForm));
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.ofDlg = new System.Windows.Forms.OpenFileDialog();
			this.pBtn = new System.Windows.Forms.Panel();
			this.btnDefRestore = new System.Windows.Forms.Button();
			this.tcOptions = new System.Windows.Forms.TabControl();
			this.tpGeneral = new System.Windows.Forms.TabPage();
			this.chBoxConfirmationForExit = new System.Windows.Forms.CheckBox();
			this.gboxButtons = new System.Windows.Forms.GroupBox();
			this.cboxTIRFB2Dup = new System.Windows.Forms.ComboBox();
			this.cboxDSFB2Dup = new System.Windows.Forms.ComboBox();
			this.lblFB2Dup = new System.Windows.Forms.Label();
			this.cboxTIRArchiveManager = new System.Windows.Forms.ComboBox();
			this.cboxDSArchiveManager = new System.Windows.Forms.ComboBox();
			this.lblArchiveManager = new System.Windows.Forms.Label();
			this.gboxDiff = new System.Windows.Forms.GroupBox();
			this.lblDiffPath = new System.Windows.Forms.Label();
			this.tboxDiffPath = new System.Windows.Forms.TextBox();
			this.btnDiffPath = new System.Windows.Forms.Button();
			this.gboxReader = new System.Windows.Forms.GroupBox();
			this.lblFBReaderPath = new System.Windows.Forms.Label();
			this.tboxReaderPath = new System.Windows.Forms.TextBox();
			this.btnReaderPath = new System.Windows.Forms.Button();
			this.gboxEditors = new System.Windows.Forms.GroupBox();
			this.lblTextEPath = new System.Windows.Forms.Label();
			this.tboxTextEPath = new System.Windows.Forms.TextBox();
			this.btnTextEPath = new System.Windows.Forms.Button();
			this.lblFBEPath = new System.Windows.Forms.Label();
			this.tboxFBEPath = new System.Windows.Forms.TextBox();
			this.btnFBEPath = new System.Windows.Forms.Button();
			this.fbdDir = new System.Windows.Forms.FolderBrowserDialog();
			this.pBtn.SuspendLayout();
			this.tcOptions.SuspendLayout();
			this.tpGeneral.SuspendLayout();
			this.gboxButtons.SuspendLayout();
			this.gboxDiff.SuspendLayout();
			this.gboxReader.SuspendLayout();
			this.gboxEditors.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.btnOK.Location = new System.Drawing.Point(681, 7);
			this.btnOK.Margin = new System.Windows.Forms.Padding(4);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(132, 32);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.BtnOKClick);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.btnCancel.Location = new System.Drawing.Point(540, 7);
			this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(124, 32);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// ofDlg
			// 
			this.ofDlg.RestoreDirectory = true;
			// 
			// pBtn
			// 
			this.pBtn.BackColor = System.Drawing.Color.DarkGray;
			this.pBtn.Controls.Add(this.btnDefRestore);
			this.pBtn.Controls.Add(this.btnOK);
			this.pBtn.Controls.Add(this.btnCancel);
			this.pBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pBtn.Location = new System.Drawing.Point(0, 610);
			this.pBtn.Margin = new System.Windows.Forms.Padding(4);
			this.pBtn.Name = "pBtn";
			this.pBtn.Size = new System.Drawing.Size(820, 52);
			this.pBtn.TabIndex = 2;
			// 
			// btnDefRestore
			// 
			this.btnDefRestore.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.btnDefRestore.ForeColor = System.Drawing.Color.Navy;
			this.btnDefRestore.Location = new System.Drawing.Point(11, 7);
			this.btnDefRestore.Margin = new System.Windows.Forms.Padding(4);
			this.btnDefRestore.Name = "btnDefRestore";
			this.btnDefRestore.Size = new System.Drawing.Size(515, 32);
			this.btnDefRestore.TabIndex = 2;
			this.btnDefRestore.Text = "Восстановить по-умолчанию";
			this.btnDefRestore.UseVisualStyleBackColor = true;
			this.btnDefRestore.Click += new System.EventHandler(this.BtnDefRestoreClick);
			// 
			// tcOptions
			// 
			this.tcOptions.Controls.Add(this.tpGeneral);
			this.tcOptions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcOptions.Location = new System.Drawing.Point(0, 0);
			this.tcOptions.Margin = new System.Windows.Forms.Padding(4);
			this.tcOptions.Name = "tcOptions";
			this.tcOptions.SelectedIndex = 0;
			this.tcOptions.Size = new System.Drawing.Size(820, 610);
			this.tcOptions.TabIndex = 3;
			// 
			// tpGeneral
			// 
			this.tpGeneral.Controls.Add(this.chBoxConfirmationForExit);
			this.tpGeneral.Controls.Add(this.gboxButtons);
			this.tpGeneral.Controls.Add(this.gboxDiff);
			this.tpGeneral.Controls.Add(this.gboxReader);
			this.tpGeneral.Controls.Add(this.gboxEditors);
			this.tpGeneral.Location = new System.Drawing.Point(4, 25);
			this.tpGeneral.Margin = new System.Windows.Forms.Padding(4);
			this.tpGeneral.Name = "tpGeneral";
			this.tpGeneral.Padding = new System.Windows.Forms.Padding(4);
			this.tpGeneral.Size = new System.Drawing.Size(812, 581);
			this.tpGeneral.TabIndex = 0;
			this.tpGeneral.Text = " Основные ";
			this.tpGeneral.UseVisualStyleBackColor = true;
			// 
			// chBoxConfirmationForExit
			// 
			this.chBoxConfirmationForExit.Checked = true;
			this.chBoxConfirmationForExit.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chBoxConfirmationForExit.Dock = System.Windows.Forms.DockStyle.Top;
			this.chBoxConfirmationForExit.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxConfirmationForExit.Location = new System.Drawing.Point(4, 298);
			this.chBoxConfirmationForExit.Margin = new System.Windows.Forms.Padding(4);
			this.chBoxConfirmationForExit.Name = "chBoxConfirmationForExit";
			this.chBoxConfirmationForExit.Size = new System.Drawing.Size(804, 30);
			this.chBoxConfirmationForExit.TabIndex = 36;
			this.chBoxConfirmationForExit.Text = " Подтверждение для выхода из программы";
			this.chBoxConfirmationForExit.UseVisualStyleBackColor = true;
			// 
			// gboxButtons
			// 
			this.gboxButtons.Controls.Add(this.cboxTIRFB2Dup);
			this.gboxButtons.Controls.Add(this.cboxDSFB2Dup);
			this.gboxButtons.Controls.Add(this.lblFB2Dup);
			this.gboxButtons.Controls.Add(this.cboxTIRArchiveManager);
			this.gboxButtons.Controls.Add(this.cboxDSArchiveManager);
			this.gboxButtons.Controls.Add(this.lblArchiveManager);
			this.gboxButtons.Dock = System.Windows.Forms.DockStyle.Top;
			this.gboxButtons.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gboxButtons.ForeColor = System.Drawing.Color.Maroon;
			this.gboxButtons.Location = new System.Drawing.Point(4, 205);
			this.gboxButtons.Margin = new System.Windows.Forms.Padding(4);
			this.gboxButtons.Name = "gboxButtons";
			this.gboxButtons.Padding = new System.Windows.Forms.Padding(4);
			this.gboxButtons.Size = new System.Drawing.Size(804, 93);
			this.gboxButtons.TabIndex = 17;
			this.gboxButtons.TabStop = false;
			this.gboxButtons.Text = " Внешний вид кнопок инструментов (иконка и текст) ";
			// 
			// cboxTIRFB2Dup
			// 
			this.cboxTIRFB2Dup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxTIRFB2Dup.FormattingEnabled = true;
			this.cboxTIRFB2Dup.Items.AddRange(new object[] {
			"Overlay",
			"ImageAboveText",
			"TextAboveImage",
			"ImageBeforeText",
			"TextBeforeImage"});
			this.cboxTIRFB2Dup.Location = new System.Drawing.Point(376, 56);
			this.cboxTIRFB2Dup.Margin = new System.Windows.Forms.Padding(4);
			this.cboxTIRFB2Dup.Name = "cboxTIRFB2Dup";
			this.cboxTIRFB2Dup.Size = new System.Drawing.Size(172, 24);
			this.cboxTIRFB2Dup.TabIndex = 27;
			// 
			// cboxDSFB2Dup
			// 
			this.cboxDSFB2Dup.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxDSFB2Dup.FormattingEnabled = true;
			this.cboxDSFB2Dup.Items.AddRange(new object[] {
			"Text",
			"Image",
			"ImageAndText"});
			this.cboxDSFB2Dup.Location = new System.Drawing.Point(196, 56);
			this.cboxDSFB2Dup.Margin = new System.Windows.Forms.Padding(4);
			this.cboxDSFB2Dup.Name = "cboxDSFB2Dup";
			this.cboxDSFB2Dup.Size = new System.Drawing.Size(172, 24);
			this.cboxDSFB2Dup.TabIndex = 26;
			this.cboxDSFB2Dup.SelectedIndexChanged += new System.EventHandler(this.CboxDSFB2DupSelectedIndexChanged);
			// 
			// lblFB2Dup
			// 
			this.lblFB2Dup.AutoSize = true;
			this.lblFB2Dup.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblFB2Dup.Location = new System.Drawing.Point(9, 59);
			this.lblFB2Dup.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblFB2Dup.Name = "lblFB2Dup";
			this.lblFB2Dup.Size = new System.Drawing.Size(163, 17);
			this.lblFB2Dup.TabIndex = 25;
			this.lblFB2Dup.Text = "Дубликатор файлов:";
			// 
			// cboxTIRArchiveManager
			// 
			this.cboxTIRArchiveManager.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxTIRArchiveManager.FormattingEnabled = true;
			this.cboxTIRArchiveManager.Items.AddRange(new object[] {
			"Overlay",
			"ImageAboveText",
			"TextAboveImage",
			"ImageBeforeText",
			"TextBeforeImage"});
			this.cboxTIRArchiveManager.Location = new System.Drawing.Point(376, 24);
			this.cboxTIRArchiveManager.Margin = new System.Windows.Forms.Padding(4);
			this.cboxTIRArchiveManager.Name = "cboxTIRArchiveManager";
			this.cboxTIRArchiveManager.Size = new System.Drawing.Size(172, 24);
			this.cboxTIRArchiveManager.TabIndex = 24;
			// 
			// cboxDSArchiveManager
			// 
			this.cboxDSArchiveManager.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxDSArchiveManager.FormattingEnabled = true;
			this.cboxDSArchiveManager.Items.AddRange(new object[] {
			"Text",
			"Image",
			"ImageAndText"});
			this.cboxDSArchiveManager.Location = new System.Drawing.Point(196, 24);
			this.cboxDSArchiveManager.Margin = new System.Windows.Forms.Padding(4);
			this.cboxDSArchiveManager.Name = "cboxDSArchiveManager";
			this.cboxDSArchiveManager.Size = new System.Drawing.Size(172, 24);
			this.cboxDSArchiveManager.TabIndex = 23;
			this.cboxDSArchiveManager.SelectedIndexChanged += new System.EventHandler(this.CboxDSArchiveManagerSelectedIndexChanged);
			// 
			// lblArchiveManager
			// 
			this.lblArchiveManager.AutoSize = true;
			this.lblArchiveManager.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblArchiveManager.Location = new System.Drawing.Point(9, 27);
			this.lblArchiveManager.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblArchiveManager.Name = "lblArchiveManager";
			this.lblArchiveManager.Size = new System.Drawing.Size(158, 17);
			this.lblArchiveManager.TabIndex = 22;
			this.lblArchiveManager.Text = "Менеджер Архивов:";
			// 
			// gboxDiff
			// 
			this.gboxDiff.Controls.Add(this.lblDiffPath);
			this.gboxDiff.Controls.Add(this.tboxDiffPath);
			this.gboxDiff.Controls.Add(this.btnDiffPath);
			this.gboxDiff.Dock = System.Windows.Forms.DockStyle.Top;
			this.gboxDiff.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gboxDiff.ForeColor = System.Drawing.Color.Maroon;
			this.gboxDiff.Location = new System.Drawing.Point(4, 150);
			this.gboxDiff.Margin = new System.Windows.Forms.Padding(4);
			this.gboxDiff.Name = "gboxDiff";
			this.gboxDiff.Padding = new System.Windows.Forms.Padding(4);
			this.gboxDiff.Size = new System.Drawing.Size(804, 55);
			this.gboxDiff.TabIndex = 16;
			this.gboxDiff.TabStop = false;
			this.gboxDiff.Text = " Diff-программа визуального сравнения ";
			// 
			// lblDiffPath
			// 
			this.lblDiffPath.AutoSize = true;
			this.lblDiffPath.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblDiffPath.Location = new System.Drawing.Point(9, 23);
			this.lblDiffPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblDiffPath.Name = "lblDiffPath";
			this.lblDiffPath.Size = new System.Drawing.Size(172, 17);
			this.lblDiffPath.TabIndex = 16;
			this.lblDiffPath.Text = "Путь к diff-программе:";
			// 
			// tboxDiffPath
			// 
			this.tboxDiffPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxDiffPath.Location = new System.Drawing.Point(245, 20);
			this.tboxDiffPath.Margin = new System.Windows.Forms.Padding(4);
			this.tboxDiffPath.Name = "tboxDiffPath";
			this.tboxDiffPath.ReadOnly = true;
			this.tboxDiffPath.Size = new System.Drawing.Size(490, 24);
			this.tboxDiffPath.TabIndex = 14;
			// 
			// btnDiffPath
			// 
			this.btnDiffPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDiffPath.Image = ((System.Drawing.Image)(resources.GetObject("btnDiffPath.Image")));
			this.btnDiffPath.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnDiffPath.Location = new System.Drawing.Point(744, 16);
			this.btnDiffPath.Margin = new System.Windows.Forms.Padding(4);
			this.btnDiffPath.Name = "btnDiffPath";
			this.btnDiffPath.Size = new System.Drawing.Size(49, 30);
			this.btnDiffPath.TabIndex = 15;
			this.btnDiffPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnDiffPath.UseVisualStyleBackColor = true;
			this.btnDiffPath.Click += new System.EventHandler(this.BtnDiffPathClick);
			// 
			// gboxReader
			// 
			this.gboxReader.Controls.Add(this.lblFBReaderPath);
			this.gboxReader.Controls.Add(this.tboxReaderPath);
			this.gboxReader.Controls.Add(this.btnReaderPath);
			this.gboxReader.Dock = System.Windows.Forms.DockStyle.Top;
			this.gboxReader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gboxReader.ForeColor = System.Drawing.Color.Maroon;
			this.gboxReader.Location = new System.Drawing.Point(4, 95);
			this.gboxReader.Margin = new System.Windows.Forms.Padding(4);
			this.gboxReader.Name = "gboxReader";
			this.gboxReader.Padding = new System.Windows.Forms.Padding(4);
			this.gboxReader.Size = new System.Drawing.Size(804, 55);
			this.gboxReader.TabIndex = 15;
			this.gboxReader.TabStop = false;
			this.gboxReader.Text = " Читалка fb2-файлов ";
			// 
			// lblFBReaderPath
			// 
			this.lblFBReaderPath.AutoSize = true;
			this.lblFBReaderPath.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblFBReaderPath.Location = new System.Drawing.Point(9, 20);
			this.lblFBReaderPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblFBReaderPath.Name = "lblFBReaderPath";
			this.lblFBReaderPath.Size = new System.Drawing.Size(151, 17);
			this.lblFBReaderPath.TabIndex = 16;
			this.lblFBReaderPath.Text = "Путь к fb2-читалке:";
			// 
			// tboxReaderPath
			// 
			this.tboxReaderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxReaderPath.Location = new System.Drawing.Point(245, 18);
			this.tboxReaderPath.Margin = new System.Windows.Forms.Padding(4);
			this.tboxReaderPath.Name = "tboxReaderPath";
			this.tboxReaderPath.ReadOnly = true;
			this.tboxReaderPath.Size = new System.Drawing.Size(490, 24);
			this.tboxReaderPath.TabIndex = 14;
			// 
			// btnReaderPath
			// 
			this.btnReaderPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnReaderPath.Image = ((System.Drawing.Image)(resources.GetObject("btnReaderPath.Image")));
			this.btnReaderPath.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnReaderPath.Location = new System.Drawing.Point(744, 16);
			this.btnReaderPath.Margin = new System.Windows.Forms.Padding(4);
			this.btnReaderPath.Name = "btnReaderPath";
			this.btnReaderPath.Size = new System.Drawing.Size(49, 30);
			this.btnReaderPath.TabIndex = 15;
			this.btnReaderPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnReaderPath.UseVisualStyleBackColor = true;
			this.btnReaderPath.Click += new System.EventHandler(this.BtnReaderPathClick);
			// 
			// gboxEditors
			// 
			this.gboxEditors.Controls.Add(this.lblTextEPath);
			this.gboxEditors.Controls.Add(this.tboxTextEPath);
			this.gboxEditors.Controls.Add(this.btnTextEPath);
			this.gboxEditors.Controls.Add(this.lblFBEPath);
			this.gboxEditors.Controls.Add(this.tboxFBEPath);
			this.gboxEditors.Controls.Add(this.btnFBEPath);
			this.gboxEditors.Dock = System.Windows.Forms.DockStyle.Top;
			this.gboxEditors.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gboxEditors.ForeColor = System.Drawing.Color.Maroon;
			this.gboxEditors.Location = new System.Drawing.Point(4, 4);
			this.gboxEditors.Margin = new System.Windows.Forms.Padding(4);
			this.gboxEditors.Name = "gboxEditors";
			this.gboxEditors.Padding = new System.Windows.Forms.Padding(4);
			this.gboxEditors.Size = new System.Drawing.Size(804, 91);
			this.gboxEditors.TabIndex = 14;
			this.gboxEditors.TabStop = false;
			this.gboxEditors.Text = "fb2-Редакторы ";
			// 
			// lblTextEPath
			// 
			this.lblTextEPath.AutoSize = true;
			this.lblTextEPath.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblTextEPath.Location = new System.Drawing.Point(9, 57);
			this.lblTextEPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblTextEPath.Name = "lblTextEPath";
			this.lblTextEPath.Size = new System.Drawing.Size(197, 17);
			this.lblTextEPath.TabIndex = 16;
			this.lblTextEPath.Text = "Текстовый Редактор fb2:";
			// 
			// tboxTextEPath
			// 
			this.tboxTextEPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxTextEPath.Location = new System.Drawing.Point(245, 52);
			this.tboxTextEPath.Margin = new System.Windows.Forms.Padding(4);
			this.tboxTextEPath.Name = "tboxTextEPath";
			this.tboxTextEPath.ReadOnly = true;
			this.tboxTextEPath.Size = new System.Drawing.Size(490, 23);
			this.tboxTextEPath.TabIndex = 14;
			// 
			// btnTextEPath
			// 
			this.btnTextEPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnTextEPath.Image = ((System.Drawing.Image)(resources.GetObject("btnTextEPath.Image")));
			this.btnTextEPath.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnTextEPath.Location = new System.Drawing.Point(744, 49);
			this.btnTextEPath.Margin = new System.Windows.Forms.Padding(4);
			this.btnTextEPath.Name = "btnTextEPath";
			this.btnTextEPath.Size = new System.Drawing.Size(49, 30);
			this.btnTextEPath.TabIndex = 15;
			this.btnTextEPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnTextEPath.UseVisualStyleBackColor = true;
			this.btnTextEPath.Click += new System.EventHandler(this.BtnTextEPathClick);
			// 
			// lblFBEPath
			// 
			this.lblFBEPath.AutoSize = true;
			this.lblFBEPath.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblFBEPath.Location = new System.Drawing.Point(9, 25);
			this.lblFBEPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.lblFBEPath.Name = "lblFBEPath";
			this.lblFBEPath.Size = new System.Drawing.Size(174, 17);
			this.lblFBEPath.TabIndex = 13;
			this.lblFBEPath.Text = "Редактор fb2-файлов:";
			// 
			// tboxFBEPath
			// 
			this.tboxFBEPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxFBEPath.Location = new System.Drawing.Point(245, 20);
			this.tboxFBEPath.Margin = new System.Windows.Forms.Padding(4);
			this.tboxFBEPath.Name = "tboxFBEPath";
			this.tboxFBEPath.ReadOnly = true;
			this.tboxFBEPath.Size = new System.Drawing.Size(490, 23);
			this.tboxFBEPath.TabIndex = 11;
			// 
			// btnFBEPath
			// 
			this.btnFBEPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFBEPath.Image = ((System.Drawing.Image)(resources.GetObject("btnFBEPath.Image")));
			this.btnFBEPath.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnFBEPath.Location = new System.Drawing.Point(744, 17);
			this.btnFBEPath.Margin = new System.Windows.Forms.Padding(4);
			this.btnFBEPath.Name = "btnFBEPath";
			this.btnFBEPath.Size = new System.Drawing.Size(49, 30);
			this.btnFBEPath.TabIndex = 12;
			this.btnFBEPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnFBEPath.UseVisualStyleBackColor = true;
			this.btnFBEPath.Click += new System.EventHandler(this.BtnFBEPathClick);
			// 
			// OptionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(820, 662);
			this.Controls.Add(this.tcOptions);
			this.Controls.Add(this.pBtn);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OptionsForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Настройки";
			this.pBtn.ResumeLayout(false);
			this.tcOptions.ResumeLayout(false);
			this.tpGeneral.ResumeLayout(false);
			this.gboxButtons.ResumeLayout(false);
			this.gboxButtons.PerformLayout();
			this.gboxDiff.ResumeLayout(false);
			this.gboxDiff.PerformLayout();
			this.gboxReader.ResumeLayout(false);
			this.gboxReader.PerformLayout();
			this.gboxEditors.ResumeLayout(false);
			this.gboxEditors.PerformLayout();
			this.ResumeLayout(false);

		}
		private System.Windows.Forms.CheckBox chBoxConfirmationForExit;
		private System.Windows.Forms.Label lblFB2Dup;
		private System.Windows.Forms.ComboBox cboxDSFB2Dup;
		private System.Windows.Forms.ComboBox cboxTIRFB2Dup;
		private System.Windows.Forms.ComboBox cboxTIRArchiveManager;
		private System.Windows.Forms.Label lblArchiveManager;
		private System.Windows.Forms.ComboBox cboxDSArchiveManager;
		private System.Windows.Forms.GroupBox gboxButtons;
		private System.Windows.Forms.Button btnDiffPath;
		private System.Windows.Forms.TextBox tboxDiffPath;
		private System.Windows.Forms.Label lblDiffPath;
		private System.Windows.Forms.GroupBox gboxDiff;
		private System.Windows.Forms.Button btnDefRestore;
		private System.Windows.Forms.FolderBrowserDialog fbdDir;
		private System.Windows.Forms.Button btnReaderPath;
		private System.Windows.Forms.TextBox tboxReaderPath;
		private System.Windows.Forms.Label lblFBReaderPath;
		private System.Windows.Forms.GroupBox gboxReader;
		private System.Windows.Forms.Button btnTextEPath;
		private System.Windows.Forms.TextBox tboxTextEPath;
		private System.Windows.Forms.Label lblTextEPath;
		private System.Windows.Forms.Button btnFBEPath;
		private System.Windows.Forms.TextBox tboxFBEPath;
		private System.Windows.Forms.Label lblFBEPath;
		private System.Windows.Forms.GroupBox gboxEditors;
		private System.Windows.Forms.TabPage tpGeneral;
		private System.Windows.Forms.TabControl tcOptions;
		private System.Windows.Forms.Panel pBtn;
		private System.Windows.Forms.OpenFileDialog ofDlg;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
	}
}
