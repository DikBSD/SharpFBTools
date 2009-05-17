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
			this.gboxRar = new System.Windows.Forms.GroupBox();
			this.lbl7zaPath = new System.Windows.Forms.Label();
			this.tbox7zaPath = new System.Windows.Forms.TextBox();
			this.btn7zaPath = new System.Windows.Forms.Button();
			this.lblUnRarPath = new System.Windows.Forms.Label();
			this.tboxUnRarPath = new System.Windows.Forms.TextBox();
			this.btnUnRarPath = new System.Windows.Forms.Button();
			this.lblRarPath = new System.Windows.Forms.Label();
			this.tboxRarPath = new System.Windows.Forms.TextBox();
			this.btnRarPath = new System.Windows.Forms.Button();
			this.lblWinRarPath = new System.Windows.Forms.Label();
			this.tboxWinRarPath = new System.Windows.Forms.TextBox();
			this.btnWinRarPath = new System.Windows.Forms.Button();
			this.tpValidator = new System.Windows.Forms.TabPage();
			this.gboxValidatorPE = new System.Windows.Forms.GroupBox();
			this.cboxValidatorForFB2ArchivePE = new System.Windows.Forms.ComboBox();
			this.cboxValidatorForFB2PE = new System.Windows.Forms.ComboBox();
			this.lblValidatorForFB2ArchivePE = new System.Windows.Forms.Label();
			this.lblValidatorForFB2PE = new System.Windows.Forms.Label();
			this.gboxValidatorDoubleClick = new System.Windows.Forms.GroupBox();
			this.cboxValidatorForFB2Archive = new System.Windows.Forms.ComboBox();
			this.cboxValidatorForFB2 = new System.Windows.Forms.ComboBox();
			this.lblValidatorForFB2Archive = new System.Windows.Forms.Label();
			this.lblValidatorForFB2 = new System.Windows.Forms.Label();
			this.tpFileManager = new System.Windows.Forms.TabPage();
			this.tcFM = new System.Windows.Forms.TabControl();
			this.tpFMGeneral = new System.Windows.Forms.TabPage();
			this.pSortFB2 = new System.Windows.Forms.Panel();
			this.rbtnFMOnleValidFB2 = new System.Windows.Forms.RadioButton();
			this.rbtnFMAllFB2 = new System.Windows.Forms.RadioButton();
			this.label11 = new System.Windows.Forms.Label();
			this.gboxFMGeneral = new System.Windows.Forms.GroupBox();
			this.pFMGenres = new System.Windows.Forms.Panel();
			this.rbtnFMFB22 = new System.Windows.Forms.RadioButton();
			this.rbtnFMFB21 = new System.Windows.Forms.RadioButton();
			this.lblFMGenres = new System.Windows.Forms.Label();
			this.chBoxDelFB2Files = new System.Windows.Forms.CheckBox();
			this.chBoxAddToFileNameBookID = new System.Windows.Forms.CheckBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.cboxFileExist = new System.Windows.Forms.ComboBox();
			this.lbFilelExist = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.cboxArchiveType = new System.Windows.Forms.ComboBox();
			this.cboxSpace = new System.Windows.Forms.ComboBox();
			this.chBoxToArchive = new System.Windows.Forms.CheckBox();
			this.lblSpace = new System.Windows.Forms.Label();
			this.chBoxStrict = new System.Windows.Forms.CheckBox();
			this.chBoxTranslit = new System.Windows.Forms.CheckBox();
			this.gboxRegister = new System.Windows.Forms.GroupBox();
			this.rbtnUpper = new System.Windows.Forms.RadioButton();
			this.rbtnLower = new System.Windows.Forms.RadioButton();
			this.rbtnAsIs = new System.Windows.Forms.RadioButton();
			this.gboxApportionment = new System.Windows.Forms.GroupBox();
			this.gBoxGenres = new System.Windows.Forms.GroupBox();
			this.gBoxGenresType = new System.Windows.Forms.GroupBox();
			this.rbtnGenreText = new System.Windows.Forms.RadioButton();
			this.rbtnGenreSchema = new System.Windows.Forms.RadioButton();
			this.gBoxGenresCount = new System.Windows.Forms.GroupBox();
			this.rbtnGenreAll = new System.Windows.Forms.RadioButton();
			this.rbtnGenreOne = new System.Windows.Forms.RadioButton();
			this.gBoxAuthors = new System.Windows.Forms.GroupBox();
			this.rbtnAuthorAll = new System.Windows.Forms.RadioButton();
			this.rbtnAuthorOne = new System.Windows.Forms.RadioButton();
			this.tpFMDirs = new System.Windows.Forms.TabPage();
			this.label12 = new System.Windows.Forms.Label();
			this.txtBoxFB2NotValidDir = new System.Windows.Forms.TextBox();
			this.btnFB2NotValidDir = new System.Windows.Forms.Button();
			this.lblFB2NotReadDir = new System.Windows.Forms.Label();
			this.lblLongPath = new System.Windows.Forms.Label();
			this.btnFB2NotReadDir = new System.Windows.Forms.Button();
			this.txtBoxFB2LongPathDir = new System.Windows.Forms.TextBox();
			this.txtBoxFB2NotReadDir = new System.Windows.Forms.TextBox();
			this.btnFB2LongPathDir = new System.Windows.Forms.Button();
			this.tpFMNoTagsText = new System.Windows.Forms.TabPage();
			this.gBoxFMNoTags = new System.Windows.Forms.GroupBox();
			this.panel12 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoNSequence = new System.Windows.Forms.TextBox();
			this.label10 = new System.Windows.Forms.Label();
			this.panel11 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoSequence = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.panel10 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoBookTitle = new System.Windows.Forms.TextBox();
			this.label8 = new System.Windows.Forms.Label();
			this.panel9 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoNickName = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.panel8 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoLastName = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.panel7 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoMiddleName = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.panel6 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoFirstName = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.panel5 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoLang = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.panel4 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoGenre = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.panel3 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoGenreGroup = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.fbdDir = new System.Windows.Forms.FolderBrowserDialog();
			this.pBtn.SuspendLayout();
			this.tcOptions.SuspendLayout();
			this.tpGeneral.SuspendLayout();
			this.gboxReader.SuspendLayout();
			this.gboxEditors.SuspendLayout();
			this.gboxRar.SuspendLayout();
			this.tpValidator.SuspendLayout();
			this.gboxValidatorPE.SuspendLayout();
			this.gboxValidatorDoubleClick.SuspendLayout();
			this.tpFileManager.SuspendLayout();
			this.tcFM.SuspendLayout();
			this.tpFMGeneral.SuspendLayout();
			this.pSortFB2.SuspendLayout();
			this.gboxFMGeneral.SuspendLayout();
			this.pFMGenres.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.gboxRegister.SuspendLayout();
			this.gboxApportionment.SuspendLayout();
			this.gBoxGenres.SuspendLayout();
			this.gBoxGenresType.SuspendLayout();
			this.gBoxGenresCount.SuspendLayout();
			this.gBoxAuthors.SuspendLayout();
			this.tpFMDirs.SuspendLayout();
			this.tpFMNoTagsText.SuspendLayout();
			this.gBoxFMNoTags.SuspendLayout();
			this.panel12.SuspendLayout();
			this.panel11.SuspendLayout();
			this.panel10.SuspendLayout();
			this.panel9.SuspendLayout();
			this.panel8.SuspendLayout();
			this.panel7.SuspendLayout();
			this.panel6.SuspendLayout();
			this.panel5.SuspendLayout();
			this.panel4.SuspendLayout();
			this.panel3.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(365, 3);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(112, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.BtnOKClick);
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(499, 3);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(100, 23);
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
			this.pBtn.Controls.Add(this.btnDefRestore);
			this.pBtn.Controls.Add(this.btnOK);
			this.pBtn.Controls.Add(this.btnCancel);
			this.pBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pBtn.Location = new System.Drawing.Point(0, 443);
			this.pBtn.Name = "pBtn";
			this.pBtn.Size = new System.Drawing.Size(617, 29);
			this.pBtn.TabIndex = 2;
			// 
			// btnDefRestore
			// 
			this.btnDefRestore.Location = new System.Drawing.Point(0, 3);
			this.btnDefRestore.Name = "btnDefRestore";
			this.btnDefRestore.Size = new System.Drawing.Size(336, 23);
			this.btnDefRestore.TabIndex = 2;
			this.btnDefRestore.Text = "Восстановить по-умолчанию (для каждой вкоадки отдельно)";
			this.btnDefRestore.UseVisualStyleBackColor = true;
			this.btnDefRestore.Click += new System.EventHandler(this.BtnDefRestoreClick);
			// 
			// tcOptions
			// 
			this.tcOptions.Controls.Add(this.tpGeneral);
			this.tcOptions.Controls.Add(this.tpValidator);
			this.tcOptions.Controls.Add(this.tpFileManager);
			this.tcOptions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcOptions.Location = new System.Drawing.Point(0, 0);
			this.tcOptions.Name = "tcOptions";
			this.tcOptions.SelectedIndex = 0;
			this.tcOptions.Size = new System.Drawing.Size(617, 443);
			this.tcOptions.TabIndex = 3;
			// 
			// tpGeneral
			// 
			this.tpGeneral.Controls.Add(this.gboxReader);
			this.tpGeneral.Controls.Add(this.gboxEditors);
			this.tpGeneral.Controls.Add(this.gboxRar);
			this.tpGeneral.Location = new System.Drawing.Point(4, 22);
			this.tpGeneral.Name = "tpGeneral";
			this.tpGeneral.Padding = new System.Windows.Forms.Padding(3);
			this.tpGeneral.Size = new System.Drawing.Size(609, 417);
			this.tpGeneral.TabIndex = 0;
			this.tpGeneral.Text = " Основные ";
			this.tpGeneral.UseVisualStyleBackColor = true;
			// 
			// gboxReader
			// 
			this.gboxReader.Controls.Add(this.lblFBReaderPath);
			this.gboxReader.Controls.Add(this.tboxReaderPath);
			this.gboxReader.Controls.Add(this.btnReaderPath);
			this.gboxReader.Dock = System.Windows.Forms.DockStyle.Top;
			this.gboxReader.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gboxReader.ForeColor = System.Drawing.Color.Maroon;
			this.gboxReader.Location = new System.Drawing.Point(3, 204);
			this.gboxReader.Name = "gboxReader";
			this.gboxReader.Size = new System.Drawing.Size(603, 45);
			this.gboxReader.TabIndex = 15;
			this.gboxReader.TabStop = false;
			this.gboxReader.Text = " Читалка fb2-файлов ";
			// 
			// lblFBReaderPath
			// 
			this.lblFBReaderPath.AutoSize = true;
			this.lblFBReaderPath.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblFBReaderPath.Location = new System.Drawing.Point(7, 16);
			this.lblFBReaderPath.Name = "lblFBReaderPath";
			this.lblFBReaderPath.Size = new System.Drawing.Size(123, 13);
			this.lblFBReaderPath.TabIndex = 16;
			this.lblFBReaderPath.Text = "Путь к fb2-читалке:";
			// 
			// tboxReaderPath
			// 
			this.tboxReaderPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxReaderPath.Location = new System.Drawing.Point(184, 12);
			this.tboxReaderPath.Name = "tboxReaderPath";
			this.tboxReaderPath.ReadOnly = true;
			this.tboxReaderPath.Size = new System.Drawing.Size(368, 20);
			this.tboxReaderPath.TabIndex = 14;
			// 
			// btnReaderPath
			// 
			this.btnReaderPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnReaderPath.Image = ((System.Drawing.Image)(resources.GetObject("btnReaderPath.Image")));
			this.btnReaderPath.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnReaderPath.Location = new System.Drawing.Point(558, 10);
			this.btnReaderPath.Name = "btnReaderPath";
			this.btnReaderPath.Size = new System.Drawing.Size(37, 24);
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
			this.gboxEditors.Location = new System.Drawing.Point(3, 130);
			this.gboxEditors.Name = "gboxEditors";
			this.gboxEditors.Size = new System.Drawing.Size(603, 74);
			this.gboxEditors.TabIndex = 14;
			this.gboxEditors.TabStop = false;
			this.gboxEditors.Text = "fb2-Редакторы ";
			// 
			// lblTextEPath
			// 
			this.lblTextEPath.AutoSize = true;
			this.lblTextEPath.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblTextEPath.Location = new System.Drawing.Point(7, 46);
			this.lblTextEPath.Name = "lblTextEPath";
			this.lblTextEPath.Size = new System.Drawing.Size(158, 13);
			this.lblTextEPath.TabIndex = 16;
			this.lblTextEPath.Text = "Текстовый Редактор fb2:";
			// 
			// tboxTextEPath
			// 
			this.tboxTextEPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxTextEPath.Location = new System.Drawing.Point(184, 42);
			this.tboxTextEPath.Name = "tboxTextEPath";
			this.tboxTextEPath.ReadOnly = true;
			this.tboxTextEPath.Size = new System.Drawing.Size(368, 20);
			this.tboxTextEPath.TabIndex = 14;
			// 
			// btnTextEPath
			// 
			this.btnTextEPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnTextEPath.Image = ((System.Drawing.Image)(resources.GetObject("btnTextEPath.Image")));
			this.btnTextEPath.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnTextEPath.Location = new System.Drawing.Point(558, 40);
			this.btnTextEPath.Name = "btnTextEPath";
			this.btnTextEPath.Size = new System.Drawing.Size(37, 24);
			this.btnTextEPath.TabIndex = 15;
			this.btnTextEPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnTextEPath.UseVisualStyleBackColor = true;
			this.btnTextEPath.Click += new System.EventHandler(this.BtnTextEPathClick);
			// 
			// lblFBEPath
			// 
			this.lblFBEPath.AutoSize = true;
			this.lblFBEPath.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblFBEPath.Location = new System.Drawing.Point(7, 20);
			this.lblFBEPath.Name = "lblFBEPath";
			this.lblFBEPath.Size = new System.Drawing.Size(137, 13);
			this.lblFBEPath.TabIndex = 13;
			this.lblFBEPath.Text = "Редактор fb2-файлов:";
			// 
			// tboxFBEPath
			// 
			this.tboxFBEPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxFBEPath.Location = new System.Drawing.Point(184, 16);
			this.tboxFBEPath.Name = "tboxFBEPath";
			this.tboxFBEPath.ReadOnly = true;
			this.tboxFBEPath.Size = new System.Drawing.Size(368, 20);
			this.tboxFBEPath.TabIndex = 11;
			// 
			// btnFBEPath
			// 
			this.btnFBEPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFBEPath.Image = ((System.Drawing.Image)(resources.GetObject("btnFBEPath.Image")));
			this.btnFBEPath.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnFBEPath.Location = new System.Drawing.Point(558, 14);
			this.btnFBEPath.Name = "btnFBEPath";
			this.btnFBEPath.Size = new System.Drawing.Size(37, 24);
			this.btnFBEPath.TabIndex = 12;
			this.btnFBEPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnFBEPath.UseVisualStyleBackColor = true;
			this.btnFBEPath.Click += new System.EventHandler(this.BtnFBEPathClick);
			// 
			// gboxRar
			// 
			this.gboxRar.Controls.Add(this.lbl7zaPath);
			this.gboxRar.Controls.Add(this.tbox7zaPath);
			this.gboxRar.Controls.Add(this.btn7zaPath);
			this.gboxRar.Controls.Add(this.lblUnRarPath);
			this.gboxRar.Controls.Add(this.tboxUnRarPath);
			this.gboxRar.Controls.Add(this.btnUnRarPath);
			this.gboxRar.Controls.Add(this.lblRarPath);
			this.gboxRar.Controls.Add(this.tboxRarPath);
			this.gboxRar.Controls.Add(this.btnRarPath);
			this.gboxRar.Controls.Add(this.lblWinRarPath);
			this.gboxRar.Controls.Add(this.tboxWinRarPath);
			this.gboxRar.Controls.Add(this.btnWinRarPath);
			this.gboxRar.Dock = System.Windows.Forms.DockStyle.Top;
			this.gboxRar.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gboxRar.ForeColor = System.Drawing.Color.Maroon;
			this.gboxRar.Location = new System.Drawing.Point(3, 3);
			this.gboxRar.Name = "gboxRar";
			this.gboxRar.Size = new System.Drawing.Size(603, 127);
			this.gboxRar.TabIndex = 13;
			this.gboxRar.TabStop = false;
			this.gboxRar.Text = " Настройки для архиваторов ";
			// 
			// lbl7zaPath
			// 
			this.lbl7zaPath.AutoSize = true;
			this.lbl7zaPath.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lbl7zaPath.Location = new System.Drawing.Point(7, 103);
			this.lbl7zaPath.Name = "lbl7zaPath";
			this.lbl7zaPath.Size = new System.Drawing.Size(166, 13);
			this.lbl7zaPath.TabIndex = 19;
			this.lbl7zaPath.Text = "Путь к 7z(a) (консольный):";
			// 
			// tbox7zaPath
			// 
			this.tbox7zaPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tbox7zaPath.Location = new System.Drawing.Point(184, 99);
			this.tbox7zaPath.Name = "tbox7zaPath";
			this.tbox7zaPath.ReadOnly = true;
			this.tbox7zaPath.Size = new System.Drawing.Size(365, 20);
			this.tbox7zaPath.TabIndex = 17;
			// 
			// btn7zaPath
			// 
			this.btn7zaPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn7zaPath.Image = ((System.Drawing.Image)(resources.GetObject("btn7zaPath.Image")));
			this.btn7zaPath.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btn7zaPath.Location = new System.Drawing.Point(555, 97);
			this.btn7zaPath.Name = "btn7zaPath";
			this.btn7zaPath.Size = new System.Drawing.Size(37, 24);
			this.btn7zaPath.TabIndex = 18;
			this.btn7zaPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btn7zaPath.UseVisualStyleBackColor = true;
			this.btn7zaPath.Click += new System.EventHandler(this.Btn7zaPathClick);
			// 
			// lblUnRarPath
			// 
			this.lblUnRarPath.AutoSize = true;
			this.lblUnRarPath.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblUnRarPath.Location = new System.Drawing.Point(7, 77);
			this.lblUnRarPath.Name = "lblUnRarPath";
			this.lblUnRarPath.Size = new System.Drawing.Size(171, 13);
			this.lblUnRarPath.TabIndex = 16;
			this.lblUnRarPath.Text = "Путь к UnRar (консольный):";
			// 
			// tboxUnRarPath
			// 
			this.tboxUnRarPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxUnRarPath.Location = new System.Drawing.Point(184, 73);
			this.tboxUnRarPath.Name = "tboxUnRarPath";
			this.tboxUnRarPath.ReadOnly = true;
			this.tboxUnRarPath.Size = new System.Drawing.Size(365, 20);
			this.tboxUnRarPath.TabIndex = 14;
			// 
			// btnUnRarPath
			// 
			this.btnUnRarPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnUnRarPath.Image = ((System.Drawing.Image)(resources.GetObject("btnUnRarPath.Image")));
			this.btnUnRarPath.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnUnRarPath.Location = new System.Drawing.Point(555, 71);
			this.btnUnRarPath.Name = "btnUnRarPath";
			this.btnUnRarPath.Size = new System.Drawing.Size(37, 24);
			this.btnUnRarPath.TabIndex = 15;
			this.btnUnRarPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnUnRarPath.UseVisualStyleBackColor = true;
			this.btnUnRarPath.Click += new System.EventHandler(this.BtnUnRarPathClick);
			// 
			// lblRarPath
			// 
			this.lblRarPath.AutoSize = true;
			this.lblRarPath.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblRarPath.Location = new System.Drawing.Point(7, 51);
			this.lblRarPath.Name = "lblRarPath";
			this.lblRarPath.Size = new System.Drawing.Size(156, 13);
			this.lblRarPath.TabIndex = 13;
			this.lblRarPath.Text = "Путь к Rar (консольный):";
			// 
			// tboxRarPath
			// 
			this.tboxRarPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxRarPath.Location = new System.Drawing.Point(184, 47);
			this.tboxRarPath.Name = "tboxRarPath";
			this.tboxRarPath.ReadOnly = true;
			this.tboxRarPath.Size = new System.Drawing.Size(365, 20);
			this.tboxRarPath.TabIndex = 11;
			// 
			// btnRarPath
			// 
			this.btnRarPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRarPath.Image = ((System.Drawing.Image)(resources.GetObject("btnRarPath.Image")));
			this.btnRarPath.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnRarPath.Location = new System.Drawing.Point(555, 45);
			this.btnRarPath.Name = "btnRarPath";
			this.btnRarPath.Size = new System.Drawing.Size(37, 24);
			this.btnRarPath.TabIndex = 12;
			this.btnRarPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnRarPath.UseVisualStyleBackColor = true;
			this.btnRarPath.Click += new System.EventHandler(this.BtnRarPathClick);
			// 
			// lblWinRarPath
			// 
			this.lblWinRarPath.AutoSize = true;
			this.lblWinRarPath.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblWinRarPath.Location = new System.Drawing.Point(7, 25);
			this.lblWinRarPath.Name = "lblWinRarPath";
			this.lblWinRarPath.Size = new System.Drawing.Size(93, 13);
			this.lblWinRarPath.TabIndex = 10;
			this.lblWinRarPath.Text = "Путь к WinRar:";
			// 
			// tboxWinRarPath
			// 
			this.tboxWinRarPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxWinRarPath.Location = new System.Drawing.Point(184, 21);
			this.tboxWinRarPath.Name = "tboxWinRarPath";
			this.tboxWinRarPath.ReadOnly = true;
			this.tboxWinRarPath.Size = new System.Drawing.Size(365, 20);
			this.tboxWinRarPath.TabIndex = 8;
			// 
			// btnWinRarPath
			// 
			this.btnWinRarPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnWinRarPath.Image = ((System.Drawing.Image)(resources.GetObject("btnWinRarPath.Image")));
			this.btnWinRarPath.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnWinRarPath.Location = new System.Drawing.Point(555, 19);
			this.btnWinRarPath.Name = "btnWinRarPath";
			this.btnWinRarPath.Size = new System.Drawing.Size(37, 24);
			this.btnWinRarPath.TabIndex = 9;
			this.btnWinRarPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnWinRarPath.UseVisualStyleBackColor = true;
			this.btnWinRarPath.Click += new System.EventHandler(this.BtnWinRarPathClick);
			// 
			// tpValidator
			// 
			this.tpValidator.Controls.Add(this.gboxValidatorPE);
			this.tpValidator.Controls.Add(this.gboxValidatorDoubleClick);
			this.tpValidator.Location = new System.Drawing.Point(4, 22);
			this.tpValidator.Name = "tpValidator";
			this.tpValidator.Padding = new System.Windows.Forms.Padding(3);
			this.tpValidator.Size = new System.Drawing.Size(609, 417);
			this.tpValidator.TabIndex = 1;
			this.tpValidator.Text = " Валидатор ";
			this.tpValidator.UseVisualStyleBackColor = true;
			// 
			// gboxValidatorPE
			// 
			this.gboxValidatorPE.Controls.Add(this.cboxValidatorForFB2ArchivePE);
			this.gboxValidatorPE.Controls.Add(this.cboxValidatorForFB2PE);
			this.gboxValidatorPE.Controls.Add(this.lblValidatorForFB2ArchivePE);
			this.gboxValidatorPE.Controls.Add(this.lblValidatorForFB2PE);
			this.gboxValidatorPE.Dock = System.Windows.Forms.DockStyle.Top;
			this.gboxValidatorPE.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gboxValidatorPE.ForeColor = System.Drawing.Color.Maroon;
			this.gboxValidatorPE.Location = new System.Drawing.Point(3, 78);
			this.gboxValidatorPE.Name = "gboxValidatorPE";
			this.gboxValidatorPE.Size = new System.Drawing.Size(603, 75);
			this.gboxValidatorPE.TabIndex = 1;
			this.gboxValidatorPE.TabStop = false;
			this.gboxValidatorPE.Text = " Действие по нажатию клавиши Enter на Списках ";
			// 
			// cboxValidatorForFB2ArchivePE
			// 
			this.cboxValidatorForFB2ArchivePE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxValidatorForFB2ArchivePE.FormattingEnabled = true;
			this.cboxValidatorForFB2ArchivePE.Items.AddRange(new object[] {
									"Проверить файл заново (валидация)",
									"Открыть файл в архиваторе",
									"Открыть папку для выделенного файла"});
			this.cboxValidatorForFB2ArchivePE.Location = new System.Drawing.Point(173, 43);
			this.cboxValidatorForFB2ArchivePE.Name = "cboxValidatorForFB2ArchivePE";
			this.cboxValidatorForFB2ArchivePE.Size = new System.Drawing.Size(337, 21);
			this.cboxValidatorForFB2ArchivePE.TabIndex = 3;
			// 
			// cboxValidatorForFB2PE
			// 
			this.cboxValidatorForFB2PE.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxValidatorForFB2PE.FormattingEnabled = true;
			this.cboxValidatorForFB2PE.Items.AddRange(new object[] {
									"Проверить файл заново (валидация)",
									"Редактировать в текстовом редакторе",
									"Редактировать в fb2-редакторе",
									"Запустить в fb2-читалке (Просмотр)",
									"Открыть папку для выделенного файла"});
			this.cboxValidatorForFB2PE.Location = new System.Drawing.Point(173, 20);
			this.cboxValidatorForFB2PE.Name = "cboxValidatorForFB2PE";
			this.cboxValidatorForFB2PE.Size = new System.Drawing.Size(337, 21);
			this.cboxValidatorForFB2PE.TabIndex = 2;
			// 
			// lblValidatorForFB2ArchivePE
			// 
			this.lblValidatorForFB2ArchivePE.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblValidatorForFB2ArchivePE.Location = new System.Drawing.Point(10, 46);
			this.lblValidatorForFB2ArchivePE.Name = "lblValidatorForFB2ArchivePE";
			this.lblValidatorForFB2ArchivePE.Size = new System.Drawing.Size(157, 18);
			this.lblValidatorForFB2ArchivePE.TabIndex = 1;
			this.lblValidatorForFB2ArchivePE.Text = "Для запакованных fb2:";
			// 
			// lblValidatorForFB2PE
			// 
			this.lblValidatorForFB2PE.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblValidatorForFB2PE.Location = new System.Drawing.Point(10, 24);
			this.lblValidatorForFB2PE.Name = "lblValidatorForFB2PE";
			this.lblValidatorForFB2PE.Size = new System.Drawing.Size(162, 18);
			this.lblValidatorForFB2PE.TabIndex = 0;
			this.lblValidatorForFB2PE.Text = "Для незапакованных fb2:";
			// 
			// gboxValidatorDoubleClick
			// 
			this.gboxValidatorDoubleClick.Controls.Add(this.cboxValidatorForFB2Archive);
			this.gboxValidatorDoubleClick.Controls.Add(this.cboxValidatorForFB2);
			this.gboxValidatorDoubleClick.Controls.Add(this.lblValidatorForFB2Archive);
			this.gboxValidatorDoubleClick.Controls.Add(this.lblValidatorForFB2);
			this.gboxValidatorDoubleClick.Dock = System.Windows.Forms.DockStyle.Top;
			this.gboxValidatorDoubleClick.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gboxValidatorDoubleClick.ForeColor = System.Drawing.Color.Maroon;
			this.gboxValidatorDoubleClick.Location = new System.Drawing.Point(3, 3);
			this.gboxValidatorDoubleClick.Name = "gboxValidatorDoubleClick";
			this.gboxValidatorDoubleClick.Size = new System.Drawing.Size(603, 75);
			this.gboxValidatorDoubleClick.TabIndex = 0;
			this.gboxValidatorDoubleClick.TabStop = false;
			this.gboxValidatorDoubleClick.Text = " Действие по двойному щелчку мышки на Списках ";
			// 
			// cboxValidatorForFB2Archive
			// 
			this.cboxValidatorForFB2Archive.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxValidatorForFB2Archive.FormattingEnabled = true;
			this.cboxValidatorForFB2Archive.Items.AddRange(new object[] {
									"Проверить файл заново (валидация)",
									"Открыть файл в архиваторе",
									"Открыть папку для выделенного файла"});
			this.cboxValidatorForFB2Archive.Location = new System.Drawing.Point(173, 43);
			this.cboxValidatorForFB2Archive.Name = "cboxValidatorForFB2Archive";
			this.cboxValidatorForFB2Archive.Size = new System.Drawing.Size(337, 21);
			this.cboxValidatorForFB2Archive.TabIndex = 3;
			// 
			// cboxValidatorForFB2
			// 
			this.cboxValidatorForFB2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxValidatorForFB2.FormattingEnabled = true;
			this.cboxValidatorForFB2.Items.AddRange(new object[] {
									"Проверить файл заново (валидация)",
									"Редактировать в текстовом редакторе",
									"Редактировать в fb2-редакторе",
									"Запустить в fb2-читалке (Просмотр)",
									"Открыть папку для выделенного файла"});
			this.cboxValidatorForFB2.Location = new System.Drawing.Point(173, 20);
			this.cboxValidatorForFB2.Name = "cboxValidatorForFB2";
			this.cboxValidatorForFB2.Size = new System.Drawing.Size(337, 21);
			this.cboxValidatorForFB2.TabIndex = 2;
			// 
			// lblValidatorForFB2Archive
			// 
			this.lblValidatorForFB2Archive.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblValidatorForFB2Archive.Location = new System.Drawing.Point(10, 46);
			this.lblValidatorForFB2Archive.Name = "lblValidatorForFB2Archive";
			this.lblValidatorForFB2Archive.Size = new System.Drawing.Size(157, 18);
			this.lblValidatorForFB2Archive.TabIndex = 1;
			this.lblValidatorForFB2Archive.Text = "Для запакованных fb2:";
			// 
			// lblValidatorForFB2
			// 
			this.lblValidatorForFB2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblValidatorForFB2.Location = new System.Drawing.Point(10, 24);
			this.lblValidatorForFB2.Name = "lblValidatorForFB2";
			this.lblValidatorForFB2.Size = new System.Drawing.Size(162, 18);
			this.lblValidatorForFB2.TabIndex = 0;
			this.lblValidatorForFB2.Text = "Для незапакованных fb2:";
			// 
			// tpFileManager
			// 
			this.tpFileManager.Controls.Add(this.tcFM);
			this.tpFileManager.Location = new System.Drawing.Point(4, 22);
			this.tpFileManager.Name = "tpFileManager";
			this.tpFileManager.Padding = new System.Windows.Forms.Padding(3);
			this.tpFileManager.Size = new System.Drawing.Size(609, 417);
			this.tpFileManager.TabIndex = 2;
			this.tpFileManager.Text = " Менеджер файлов ";
			this.tpFileManager.UseVisualStyleBackColor = true;
			// 
			// tcFM
			// 
			this.tcFM.Controls.Add(this.tpFMGeneral);
			this.tcFM.Controls.Add(this.tpFMDirs);
			this.tcFM.Controls.Add(this.tpFMNoTagsText);
			this.tcFM.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcFM.Location = new System.Drawing.Point(3, 3);
			this.tcFM.Name = "tcFM";
			this.tcFM.SelectedIndex = 0;
			this.tcFM.Size = new System.Drawing.Size(603, 411);
			this.tcFM.TabIndex = 36;
			// 
			// tpFMGeneral
			// 
			this.tpFMGeneral.Controls.Add(this.pSortFB2);
			this.tpFMGeneral.Controls.Add(this.gboxFMGeneral);
			this.tpFMGeneral.Controls.Add(this.gboxApportionment);
			this.tpFMGeneral.Location = new System.Drawing.Point(4, 22);
			this.tpFMGeneral.Name = "tpFMGeneral";
			this.tpFMGeneral.Padding = new System.Windows.Forms.Padding(3);
			this.tpFMGeneral.Size = new System.Drawing.Size(595, 385);
			this.tpFMGeneral.TabIndex = 0;
			this.tpFMGeneral.Text = " Основные ";
			this.tpFMGeneral.UseVisualStyleBackColor = true;
			// 
			// pSortFB2
			// 
			this.pSortFB2.Controls.Add(this.rbtnFMOnleValidFB2);
			this.pSortFB2.Controls.Add(this.rbtnFMAllFB2);
			this.pSortFB2.Controls.Add(this.label11);
			this.pSortFB2.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pSortFB2.Location = new System.Drawing.Point(3, 209);
			this.pSortFB2.Name = "pSortFB2";
			this.pSortFB2.Size = new System.Drawing.Size(589, 23);
			this.pSortFB2.TabIndex = 30;
			// 
			// rbtnFMOnleValidFB2
			// 
			this.rbtnFMOnleValidFB2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.rbtnFMOnleValidFB2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnFMOnleValidFB2.Location = new System.Drawing.Point(292, 1);
			this.rbtnFMOnleValidFB2.Name = "rbtnFMOnleValidFB2";
			this.rbtnFMOnleValidFB2.Size = new System.Drawing.Size(182, 17);
			this.rbtnFMOnleValidFB2.TabIndex = 2;
			this.rbtnFMOnleValidFB2.Text = "Только Валидные файлы";
			this.rbtnFMOnleValidFB2.UseVisualStyleBackColor = true;
			// 
			// rbtnFMAllFB2
			// 
			this.rbtnFMAllFB2.Checked = true;
			this.rbtnFMAllFB2.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.rbtnFMAllFB2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnFMAllFB2.Location = new System.Drawing.Point(140, 2);
			this.rbtnFMAllFB2.Name = "rbtnFMAllFB2";
			this.rbtnFMAllFB2.Size = new System.Drawing.Size(145, 17);
			this.rbtnFMAllFB2.TabIndex = 1;
			this.rbtnFMAllFB2.TabStop = true;
			this.rbtnFMAllFB2.Text = "Любые fb2-файлы";
			this.rbtnFMAllFB2.UseVisualStyleBackColor = true;
			// 
			// label11
			// 
			this.label11.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.label11.ForeColor = System.Drawing.Color.Navy;
			this.label11.Location = new System.Drawing.Point(5, 3);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(133, 16);
			this.label11.TabIndex = 0;
			this.label11.Text = "Сортировка файлов:";
			// 
			// gboxFMGeneral
			// 
			this.gboxFMGeneral.Controls.Add(this.pFMGenres);
			this.gboxFMGeneral.Controls.Add(this.chBoxDelFB2Files);
			this.gboxFMGeneral.Controls.Add(this.chBoxAddToFileNameBookID);
			this.gboxFMGeneral.Controls.Add(this.panel2);
			this.gboxFMGeneral.Controls.Add(this.panel1);
			this.gboxFMGeneral.Controls.Add(this.chBoxStrict);
			this.gboxFMGeneral.Controls.Add(this.chBoxTranslit);
			this.gboxFMGeneral.Controls.Add(this.gboxRegister);
			this.gboxFMGeneral.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gboxFMGeneral.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gboxFMGeneral.ForeColor = System.Drawing.Color.Maroon;
			this.gboxFMGeneral.Location = new System.Drawing.Point(3, 3);
			this.gboxFMGeneral.Name = "gboxFMGeneral";
			this.gboxFMGeneral.Size = new System.Drawing.Size(589, 229);
			this.gboxFMGeneral.TabIndex = 28;
			this.gboxFMGeneral.TabStop = false;
			this.gboxFMGeneral.Text = " Основные настройки ";
			// 
			// pFMGenres
			// 
			this.pFMGenres.Controls.Add(this.rbtnFMFB22);
			this.pFMGenres.Controls.Add(this.rbtnFMFB21);
			this.pFMGenres.Controls.Add(this.lblFMGenres);
			this.pFMGenres.Dock = System.Windows.Forms.DockStyle.Top;
			this.pFMGenres.Location = new System.Drawing.Point(3, 181);
			this.pFMGenres.Name = "pFMGenres";
			this.pFMGenres.Size = new System.Drawing.Size(583, 23);
			this.pFMGenres.TabIndex = 26;
			// 
			// rbtnFMFB22
			// 
			this.rbtnFMFB22.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnFMFB22.Location = new System.Drawing.Point(217, 2);
			this.rbtnFMFB22.Name = "rbtnFMFB22";
			this.rbtnFMFB22.Size = new System.Drawing.Size(63, 17);
			this.rbtnFMFB22.TabIndex = 2;
			this.rbtnFMFB22.Text = "fb2.2";
			this.rbtnFMFB22.UseVisualStyleBackColor = true;
			// 
			// rbtnFMFB21
			// 
			this.rbtnFMFB21.Checked = true;
			this.rbtnFMFB21.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnFMFB21.Location = new System.Drawing.Point(138, 2);
			this.rbtnFMFB21.Name = "rbtnFMFB21";
			this.rbtnFMFB21.Size = new System.Drawing.Size(62, 17);
			this.rbtnFMFB21.TabIndex = 1;
			this.rbtnFMFB21.TabStop = true;
			this.rbtnFMFB21.Text = "fb2.1";
			this.rbtnFMFB21.UseVisualStyleBackColor = true;
			// 
			// lblFMGenres
			// 
			this.lblFMGenres.ForeColor = System.Drawing.Color.Navy;
			this.lblFMGenres.Location = new System.Drawing.Point(5, 3);
			this.lblFMGenres.Name = "lblFMGenres";
			this.lblFMGenres.Size = new System.Drawing.Size(108, 16);
			this.lblFMGenres.TabIndex = 0;
			this.lblFMGenres.Text = "Схема Жанров:";
			// 
			// chBoxDelFB2Files
			// 
			this.chBoxDelFB2Files.Dock = System.Windows.Forms.DockStyle.Top;
			this.chBoxDelFB2Files.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxDelFB2Files.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chBoxDelFB2Files.Location = new System.Drawing.Point(3, 163);
			this.chBoxDelFB2Files.Name = "chBoxDelFB2Files";
			this.chBoxDelFB2Files.Size = new System.Drawing.Size(583, 18);
			this.chBoxDelFB2Files.TabIndex = 25;
			this.chBoxDelFB2Files.Text = " Удалить исходные файлы после сортировки";
			this.chBoxDelFB2Files.UseVisualStyleBackColor = true;
			// 
			// chBoxAddToFileNameBookID
			// 
			this.chBoxAddToFileNameBookID.Dock = System.Windows.Forms.DockStyle.Top;
			this.chBoxAddToFileNameBookID.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxAddToFileNameBookID.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chBoxAddToFileNameBookID.Location = new System.Drawing.Point(3, 145);
			this.chBoxAddToFileNameBookID.Name = "chBoxAddToFileNameBookID";
			this.chBoxAddToFileNameBookID.Size = new System.Drawing.Size(583, 18);
			this.chBoxAddToFileNameBookID.TabIndex = 24;
			this.chBoxAddToFileNameBookID.Text = " Добавить к имени файла ID книги";
			this.chBoxAddToFileNameBookID.UseVisualStyleBackColor = true;
			// 
			// panel2
			// 
			this.panel2.Controls.Add(this.cboxFileExist);
			this.panel2.Controls.Add(this.lbFilelExist);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(3, 120);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(583, 25);
			this.panel2.TabIndex = 16;
			// 
			// cboxFileExist
			// 
			this.cboxFileExist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxFileExist.FormattingEnabled = true;
			this.cboxFileExist.Items.AddRange(new object[] {
									"Заменить существующий файл новым",
									"Добавить к создаваемому файлу очередной номер",
									"Добавить к создаваемому файлу дату и время"});
			this.cboxFileExist.Location = new System.Drawing.Point(151, 2);
			this.cboxFileExist.Name = "cboxFileExist";
			this.cboxFileExist.Size = new System.Drawing.Size(433, 21);
			this.cboxFileExist.TabIndex = 20;
			this.cboxFileExist.SelectedIndexChanged += new System.EventHandler(this.CboxFileExistSelectedIndexChanged);
			// 
			// lbFilelExist
			// 
			this.lbFilelExist.AutoSize = true;
			this.lbFilelExist.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lbFilelExist.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lbFilelExist.Location = new System.Drawing.Point(2, 5);
			this.lbFilelExist.Name = "lbFilelExist";
			this.lbFilelExist.Size = new System.Drawing.Size(127, 13);
			this.lbFilelExist.TabIndex = 19;
			this.lbFilelExist.Text = "Одинаковые файлы:";
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.cboxArchiveType);
			this.panel1.Controls.Add(this.cboxSpace);
			this.panel1.Controls.Add(this.chBoxToArchive);
			this.panel1.Controls.Add(this.lblSpace);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(3, 91);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(583, 29);
			this.panel1.TabIndex = 14;
			// 
			// cboxArchiveType
			// 
			this.cboxArchiveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxArchiveType.Enabled = false;
			this.cboxArchiveType.FormattingEnabled = true;
			this.cboxArchiveType.Items.AddRange(new object[] {
									"Rar",
									"Zip",
									"7z",
									"BZip2",
									"GZip",
									"Tar"});
			this.cboxArchiveType.Location = new System.Drawing.Point(461, 4);
			this.cboxArchiveType.Name = "cboxArchiveType";
			this.cboxArchiveType.Size = new System.Drawing.Size(123, 21);
			this.cboxArchiveType.TabIndex = 17;
			// 
			// cboxSpace
			// 
			this.cboxSpace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxSpace.FormattingEnabled = true;
			this.cboxSpace.Items.AddRange(new object[] {
									"Оставить",
									"Удалить",
									"Заменит на  _",
									"Заменит на  -",
									"Заменит на  +",
									"Заменит на  ~"});
			this.cboxSpace.Location = new System.Drawing.Point(151, 5);
			this.cboxSpace.Name = "cboxSpace";
			this.cboxSpace.Size = new System.Drawing.Size(123, 21);
			this.cboxSpace.TabIndex = 24;
			// 
			// chBoxToArchive
			// 
			this.chBoxToArchive.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxToArchive.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chBoxToArchive.Location = new System.Drawing.Point(307, 6);
			this.chBoxToArchive.Name = "chBoxToArchive";
			this.chBoxToArchive.Size = new System.Drawing.Size(153, 18);
			this.chBoxToArchive.TabIndex = 16;
			this.chBoxToArchive.Text = "Упаковывать файлы:";
			this.chBoxToArchive.UseVisualStyleBackColor = true;
			this.chBoxToArchive.CheckedChanged += new System.EventHandler(this.ChBoxToArchiveCheckedChanged);
			// 
			// lblSpace
			// 
			this.lblSpace.AutoSize = true;
			this.lblSpace.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblSpace.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblSpace.Location = new System.Drawing.Point(2, 8);
			this.lblSpace.Name = "lblSpace";
			this.lblSpace.Size = new System.Drawing.Size(133, 13);
			this.lblSpace.TabIndex = 23;
			this.lblSpace.Text = "Обработка пробелов:";
			// 
			// chBoxStrict
			// 
			this.chBoxStrict.Dock = System.Windows.Forms.DockStyle.Top;
			this.chBoxStrict.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxStrict.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chBoxStrict.Location = new System.Drawing.Point(3, 73);
			this.chBoxStrict.Name = "chBoxStrict";
			this.chBoxStrict.Size = new System.Drawing.Size(583, 18);
			this.chBoxStrict.TabIndex = 13;
			this.chBoxStrict.Text = "\"Строгие\" имена файлов: алфавитно-цифровые символы, а так же [](){}-_";
			this.chBoxStrict.UseVisualStyleBackColor = true;
			// 
			// chBoxTranslit
			// 
			this.chBoxTranslit.Dock = System.Windows.Forms.DockStyle.Top;
			this.chBoxTranslit.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxTranslit.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chBoxTranslit.Location = new System.Drawing.Point(3, 55);
			this.chBoxTranslit.Name = "chBoxTranslit";
			this.chBoxTranslit.Size = new System.Drawing.Size(583, 18);
			this.chBoxTranslit.TabIndex = 12;
			this.chBoxTranslit.Text = "Транслитерация имен файлов";
			this.chBoxTranslit.UseVisualStyleBackColor = true;
			// 
			// gboxRegister
			// 
			this.gboxRegister.Controls.Add(this.rbtnUpper);
			this.gboxRegister.Controls.Add(this.rbtnLower);
			this.gboxRegister.Controls.Add(this.rbtnAsIs);
			this.gboxRegister.Dock = System.Windows.Forms.DockStyle.Top;
			this.gboxRegister.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gboxRegister.ForeColor = System.Drawing.Color.Navy;
			this.gboxRegister.Location = new System.Drawing.Point(3, 16);
			this.gboxRegister.Name = "gboxRegister";
			this.gboxRegister.Size = new System.Drawing.Size(583, 39);
			this.gboxRegister.TabIndex = 10;
			this.gboxRegister.TabStop = false;
			this.gboxRegister.Text = " Регистр имени файла ";
			// 
			// rbtnUpper
			// 
			this.rbtnUpper.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnUpper.Location = new System.Drawing.Point(105, 16);
			this.rbtnUpper.Name = "rbtnUpper";
			this.rbtnUpper.Size = new System.Drawing.Size(132, 18);
			this.rbtnUpper.TabIndex = 2;
			this.rbtnUpper.Text = "Верхний регистр";
			this.rbtnUpper.UseVisualStyleBackColor = true;
			// 
			// rbtnLower
			// 
			this.rbtnLower.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnLower.Location = new System.Drawing.Point(238, 16);
			this.rbtnLower.Name = "rbtnLower";
			this.rbtnLower.Size = new System.Drawing.Size(132, 18);
			this.rbtnLower.TabIndex = 1;
			this.rbtnLower.Text = "Нижний регистр";
			this.rbtnLower.UseVisualStyleBackColor = true;
			// 
			// rbtnAsIs
			// 
			this.rbtnAsIs.Checked = true;
			this.rbtnAsIs.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnAsIs.Location = new System.Drawing.Point(3, 16);
			this.rbtnAsIs.Name = "rbtnAsIs";
			this.rbtnAsIs.Size = new System.Drawing.Size(96, 18);
			this.rbtnAsIs.TabIndex = 0;
			this.rbtnAsIs.TabStop = true;
			this.rbtnAsIs.Text = "Как есть";
			this.rbtnAsIs.UseVisualStyleBackColor = true;
			// 
			// gboxApportionment
			// 
			this.gboxApportionment.Controls.Add(this.gBoxGenres);
			this.gboxApportionment.Controls.Add(this.gBoxAuthors);
			this.gboxApportionment.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.gboxApportionment.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gboxApportionment.ForeColor = System.Drawing.Color.Maroon;
			this.gboxApportionment.Location = new System.Drawing.Point(3, 232);
			this.gboxApportionment.Name = "gboxApportionment";
			this.gboxApportionment.Size = new System.Drawing.Size(589, 150);
			this.gboxApportionment.TabIndex = 29;
			this.gboxApportionment.TabStop = false;
			this.gboxApportionment.Text = " Раскладка файлов по папкам ";
			// 
			// gBoxGenres
			// 
			this.gBoxGenres.Controls.Add(this.gBoxGenresType);
			this.gBoxGenres.Controls.Add(this.gBoxGenresCount);
			this.gBoxGenres.Dock = System.Windows.Forms.DockStyle.Top;
			this.gBoxGenres.ForeColor = System.Drawing.Color.Navy;
			this.gBoxGenres.Location = new System.Drawing.Point(3, 56);
			this.gBoxGenres.Name = "gBoxGenres";
			this.gBoxGenres.Size = new System.Drawing.Size(583, 82);
			this.gBoxGenres.TabIndex = 28;
			this.gBoxGenres.TabStop = false;
			this.gBoxGenres.Text = " Жанры ";
			// 
			// gBoxGenresType
			// 
			this.gBoxGenresType.Controls.Add(this.rbtnGenreText);
			this.gBoxGenresType.Controls.Add(this.rbtnGenreSchema);
			this.gBoxGenresType.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gBoxGenresType.ForeColor = System.Drawing.Color.Purple;
			this.gBoxGenresType.Location = new System.Drawing.Point(260, 19);
			this.gBoxGenresType.Name = "gBoxGenresType";
			this.gBoxGenresType.Size = new System.Drawing.Size(331, 57);
			this.gBoxGenresType.TabIndex = 27;
			this.gBoxGenresType.TabStop = false;
			this.gBoxGenresType.Text = " Вид папки - жанра ";
			// 
			// rbtnGenreText
			// 
			this.rbtnGenreText.Dock = System.Windows.Forms.DockStyle.Top;
			this.rbtnGenreText.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnGenreText.Location = new System.Drawing.Point(3, 34);
			this.rbtnGenreText.Name = "rbtnGenreText";
			this.rbtnGenreText.Size = new System.Drawing.Size(325, 18);
			this.rbtnGenreText.TabIndex = 1;
			this.rbtnGenreText.Text = "Расшифровано (например: Русская классика)";
			this.rbtnGenreText.UseVisualStyleBackColor = true;
			// 
			// rbtnGenreSchema
			// 
			this.rbtnGenreSchema.Checked = true;
			this.rbtnGenreSchema.Dock = System.Windows.Forms.DockStyle.Top;
			this.rbtnGenreSchema.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnGenreSchema.Location = new System.Drawing.Point(3, 16);
			this.rbtnGenreSchema.Name = "rbtnGenreSchema";
			this.rbtnGenreSchema.Size = new System.Drawing.Size(325, 18);
			this.rbtnGenreSchema.TabIndex = 0;
			this.rbtnGenreSchema.TabStop = true;
			this.rbtnGenreSchema.Text = "Как в схеме (например: prose_rus_classic)";
			this.rbtnGenreSchema.UseVisualStyleBackColor = true;
			// 
			// gBoxGenresCount
			// 
			this.gBoxGenresCount.Controls.Add(this.rbtnGenreAll);
			this.gBoxGenresCount.Controls.Add(this.rbtnGenreOne);
			this.gBoxGenresCount.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gBoxGenresCount.ForeColor = System.Drawing.Color.Purple;
			this.gBoxGenresCount.Location = new System.Drawing.Point(6, 19);
			this.gBoxGenresCount.Name = "gBoxGenresCount";
			this.gBoxGenresCount.Size = new System.Drawing.Size(248, 57);
			this.gBoxGenresCount.TabIndex = 26;
			this.gBoxGenresCount.TabStop = false;
			this.gBoxGenresCount.Text = " Раскладка файлов по жанрам ";
			// 
			// rbtnGenreAll
			// 
			this.rbtnGenreAll.Dock = System.Windows.Forms.DockStyle.Top;
			this.rbtnGenreAll.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnGenreAll.Location = new System.Drawing.Point(3, 34);
			this.rbtnGenreAll.Name = "rbtnGenreAll";
			this.rbtnGenreAll.Size = new System.Drawing.Size(242, 18);
			this.rbtnGenreAll.TabIndex = 1;
			this.rbtnGenreAll.Text = "По всем жанрам";
			this.rbtnGenreAll.UseVisualStyleBackColor = true;
			// 
			// rbtnGenreOne
			// 
			this.rbtnGenreOne.Checked = true;
			this.rbtnGenreOne.Dock = System.Windows.Forms.DockStyle.Top;
			this.rbtnGenreOne.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnGenreOne.Location = new System.Drawing.Point(3, 16);
			this.rbtnGenreOne.Name = "rbtnGenreOne";
			this.rbtnGenreOne.Size = new System.Drawing.Size(242, 18);
			this.rbtnGenreOne.TabIndex = 0;
			this.rbtnGenreOne.TabStop = true;
			this.rbtnGenreOne.Text = "По первому жанру";
			this.rbtnGenreOne.UseVisualStyleBackColor = true;
			// 
			// gBoxAuthors
			// 
			this.gBoxAuthors.Controls.Add(this.rbtnAuthorAll);
			this.gBoxAuthors.Controls.Add(this.rbtnAuthorOne);
			this.gBoxAuthors.Dock = System.Windows.Forms.DockStyle.Top;
			this.gBoxAuthors.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gBoxAuthors.ForeColor = System.Drawing.Color.Navy;
			this.gBoxAuthors.Location = new System.Drawing.Point(3, 16);
			this.gBoxAuthors.Name = "gBoxAuthors";
			this.gBoxAuthors.Size = new System.Drawing.Size(583, 40);
			this.gBoxAuthors.TabIndex = 27;
			this.gBoxAuthors.TabStop = false;
			this.gBoxAuthors.Text = " Авторы ";
			// 
			// rbtnAuthorAll
			// 
			this.rbtnAuthorAll.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnAuthorAll.Location = new System.Drawing.Point(154, 16);
			this.rbtnAuthorAll.Name = "rbtnAuthorAll";
			this.rbtnAuthorAll.Size = new System.Drawing.Size(132, 18);
			this.rbtnAuthorAll.TabIndex = 1;
			this.rbtnAuthorAll.Text = "По всем авторам";
			this.rbtnAuthorAll.UseVisualStyleBackColor = true;
			// 
			// rbtnAuthorOne
			// 
			this.rbtnAuthorOne.Checked = true;
			this.rbtnAuthorOne.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnAuthorOne.Location = new System.Drawing.Point(3, 16);
			this.rbtnAuthorOne.Name = "rbtnAuthorOne";
			this.rbtnAuthorOne.Size = new System.Drawing.Size(145, 18);
			this.rbtnAuthorOne.TabIndex = 0;
			this.rbtnAuthorOne.TabStop = true;
			this.rbtnAuthorOne.Text = "По первому автору";
			this.rbtnAuthorOne.UseVisualStyleBackColor = true;
			// 
			// tpFMDirs
			// 
			this.tpFMDirs.Controls.Add(this.label12);
			this.tpFMDirs.Controls.Add(this.txtBoxFB2NotValidDir);
			this.tpFMDirs.Controls.Add(this.btnFB2NotValidDir);
			this.tpFMDirs.Controls.Add(this.lblFB2NotReadDir);
			this.tpFMDirs.Controls.Add(this.lblLongPath);
			this.tpFMDirs.Controls.Add(this.btnFB2NotReadDir);
			this.tpFMDirs.Controls.Add(this.txtBoxFB2LongPathDir);
			this.tpFMDirs.Controls.Add(this.txtBoxFB2NotReadDir);
			this.tpFMDirs.Controls.Add(this.btnFB2LongPathDir);
			this.tpFMDirs.Location = new System.Drawing.Point(4, 22);
			this.tpFMDirs.Name = "tpFMDirs";
			this.tpFMDirs.Padding = new System.Windows.Forms.Padding(3);
			this.tpFMDirs.Size = new System.Drawing.Size(595, 385);
			this.tpFMDirs.TabIndex = 1;
			this.tpFMDirs.Text = " Папки для \"проблемных\" файлов ";
			this.tpFMDirs.UseVisualStyleBackColor = true;
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.label12.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label12.Location = new System.Drawing.Point(3, 67);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(218, 13);
			this.label12.TabIndex = 38;
			this.label12.Text = "Папка для невалидных fb2-файлов:";
			// 
			// txtBoxFB2NotValidDir
			// 
			this.txtBoxFB2NotValidDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFB2NotValidDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtBoxFB2NotValidDir.Location = new System.Drawing.Point(226, 63);
			this.txtBoxFB2NotValidDir.Name = "txtBoxFB2NotValidDir";
			this.txtBoxFB2NotValidDir.ReadOnly = true;
			this.txtBoxFB2NotValidDir.Size = new System.Drawing.Size(309, 20);
			this.txtBoxFB2NotValidDir.TabIndex = 36;
			// 
			// btnFB2NotValidDir
			// 
			this.btnFB2NotValidDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFB2NotValidDir.Image = ((System.Drawing.Image)(resources.GetObject("btnFB2NotValidDir.Image")));
			this.btnFB2NotValidDir.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnFB2NotValidDir.Location = new System.Drawing.Point(541, 61);
			this.btnFB2NotValidDir.Name = "btnFB2NotValidDir";
			this.btnFB2NotValidDir.Size = new System.Drawing.Size(37, 24);
			this.btnFB2NotValidDir.TabIndex = 37;
			this.btnFB2NotValidDir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnFB2NotValidDir.UseVisualStyleBackColor = true;
			this.btnFB2NotValidDir.Click += new System.EventHandler(this.BtnFB2NotValidDirClick);
			// 
			// lblFB2NotReadDir
			// 
			this.lblFB2NotReadDir.AutoSize = true;
			this.lblFB2NotReadDir.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblFB2NotReadDir.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblFB2NotReadDir.Location = new System.Drawing.Point(3, 14);
			this.lblFB2NotReadDir.Name = "lblFB2NotReadDir";
			this.lblFB2NotReadDir.Size = new System.Drawing.Size(217, 13);
			this.lblFB2NotReadDir.TabIndex = 32;
			this.lblFB2NotReadDir.Text = "Папка для нечитаемых fb2-файлов:";
			// 
			// lblLongPath
			// 
			this.lblLongPath.AutoSize = true;
			this.lblLongPath.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblLongPath.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblLongPath.Location = new System.Drawing.Point(3, 40);
			this.lblLongPath.Name = "lblLongPath";
			this.lblLongPath.Size = new System.Drawing.Size(217, 13);
			this.lblLongPath.TabIndex = 35;
			this.lblLongPath.Text = "Папка для fb2 с длинными именами:";
			// 
			// btnFB2NotReadDir
			// 
			this.btnFB2NotReadDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFB2NotReadDir.Image = ((System.Drawing.Image)(resources.GetObject("btnFB2NotReadDir.Image")));
			this.btnFB2NotReadDir.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnFB2NotReadDir.Location = new System.Drawing.Point(541, 8);
			this.btnFB2NotReadDir.Name = "btnFB2NotReadDir";
			this.btnFB2NotReadDir.Size = new System.Drawing.Size(37, 24);
			this.btnFB2NotReadDir.TabIndex = 31;
			this.btnFB2NotReadDir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnFB2NotReadDir.UseVisualStyleBackColor = true;
			this.btnFB2NotReadDir.Click += new System.EventHandler(this.BtnFB2NotReadDirClick);
			// 
			// txtBoxFB2LongPathDir
			// 
			this.txtBoxFB2LongPathDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFB2LongPathDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtBoxFB2LongPathDir.Location = new System.Drawing.Point(226, 36);
			this.txtBoxFB2LongPathDir.Name = "txtBoxFB2LongPathDir";
			this.txtBoxFB2LongPathDir.ReadOnly = true;
			this.txtBoxFB2LongPathDir.Size = new System.Drawing.Size(309, 20);
			this.txtBoxFB2LongPathDir.TabIndex = 33;
			// 
			// txtBoxFB2NotReadDir
			// 
			this.txtBoxFB2NotReadDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.txtBoxFB2NotReadDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtBoxFB2NotReadDir.Location = new System.Drawing.Point(226, 10);
			this.txtBoxFB2NotReadDir.Name = "txtBoxFB2NotReadDir";
			this.txtBoxFB2NotReadDir.ReadOnly = true;
			this.txtBoxFB2NotReadDir.Size = new System.Drawing.Size(309, 20);
			this.txtBoxFB2NotReadDir.TabIndex = 30;
			// 
			// btnFB2LongPathDir
			// 
			this.btnFB2LongPathDir.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFB2LongPathDir.Image = ((System.Drawing.Image)(resources.GetObject("btnFB2LongPathDir.Image")));
			this.btnFB2LongPathDir.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnFB2LongPathDir.Location = new System.Drawing.Point(541, 34);
			this.btnFB2LongPathDir.Name = "btnFB2LongPathDir";
			this.btnFB2LongPathDir.Size = new System.Drawing.Size(37, 24);
			this.btnFB2LongPathDir.TabIndex = 34;
			this.btnFB2LongPathDir.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnFB2LongPathDir.UseVisualStyleBackColor = true;
			this.btnFB2LongPathDir.Click += new System.EventHandler(this.BtnFB2LongPathDirClick);
			// 
			// tpFMNoTagsText
			// 
			this.tpFMNoTagsText.Controls.Add(this.gBoxFMNoTags);
			this.tpFMNoTagsText.Location = new System.Drawing.Point(4, 22);
			this.tpFMNoTagsText.Name = "tpFMNoTagsText";
			this.tpFMNoTagsText.Size = new System.Drawing.Size(595, 385);
			this.tpFMNoTagsText.TabIndex = 2;
			this.tpFMNoTagsText.Text = " Название папки шаблонного тэга без данных ";
			this.tpFMNoTagsText.UseVisualStyleBackColor = true;
			// 
			// gBoxFMNoTags
			// 
			this.gBoxFMNoTags.Controls.Add(this.panel12);
			this.gBoxFMNoTags.Controls.Add(this.panel11);
			this.gBoxFMNoTags.Controls.Add(this.panel10);
			this.gBoxFMNoTags.Controls.Add(this.panel9);
			this.gBoxFMNoTags.Controls.Add(this.panel8);
			this.gBoxFMNoTags.Controls.Add(this.panel7);
			this.gBoxFMNoTags.Controls.Add(this.panel6);
			this.gBoxFMNoTags.Controls.Add(this.panel5);
			this.gBoxFMNoTags.Controls.Add(this.panel4);
			this.gBoxFMNoTags.Controls.Add(this.panel3);
			this.gBoxFMNoTags.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gBoxFMNoTags.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gBoxFMNoTags.Location = new System.Drawing.Point(0, 0);
			this.gBoxFMNoTags.Name = "gBoxFMNoTags";
			this.gBoxFMNoTags.Size = new System.Drawing.Size(595, 385);
			this.gBoxFMNoTags.TabIndex = 0;
			this.gBoxFMNoTags.TabStop = false;
			this.gBoxFMNoTags.Text = " Для отсутствующих данных тэгов ";
			// 
			// panel12
			// 
			this.panel12.Controls.Add(this.txtBoxFMNoNSequence);
			this.panel12.Controls.Add(this.label10);
			this.panel12.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel12.Location = new System.Drawing.Point(3, 304);
			this.panel12.Name = "panel12";
			this.panel12.Size = new System.Drawing.Size(589, 32);
			this.panel12.TabIndex = 9;
			// 
			// txtBoxFMNoNSequence
			// 
			this.txtBoxFMNoNSequence.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoNSequence.Name = "txtBoxFMNoNSequence";
			this.txtBoxFMNoNSequence.Size = new System.Drawing.Size(390, 20);
			this.txtBoxFMNoNSequence.TabIndex = 1;
			// 
			// label10
			// 
			this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label10.Location = new System.Drawing.Point(3, 8);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(186, 18);
			this.label10.TabIndex = 0;
			this.label10.Text = "Номера Серии Нет";
			// 
			// panel11
			// 
			this.panel11.Controls.Add(this.txtBoxFMNoSequence);
			this.panel11.Controls.Add(this.label9);
			this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel11.Location = new System.Drawing.Point(3, 272);
			this.panel11.Name = "panel11";
			this.panel11.Size = new System.Drawing.Size(589, 32);
			this.panel11.TabIndex = 8;
			// 
			// txtBoxFMNoSequence
			// 
			this.txtBoxFMNoSequence.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoSequence.Name = "txtBoxFMNoSequence";
			this.txtBoxFMNoSequence.Size = new System.Drawing.Size(390, 20);
			this.txtBoxFMNoSequence.TabIndex = 1;
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label9.Location = new System.Drawing.Point(3, 8);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(186, 18);
			this.label9.TabIndex = 0;
			this.label9.Text = "Серии Нет";
			// 
			// panel10
			// 
			this.panel10.Controls.Add(this.txtBoxFMNoBookTitle);
			this.panel10.Controls.Add(this.label8);
			this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel10.Location = new System.Drawing.Point(3, 240);
			this.panel10.Name = "panel10";
			this.panel10.Size = new System.Drawing.Size(589, 32);
			this.panel10.TabIndex = 7;
			// 
			// txtBoxFMNoBookTitle
			// 
			this.txtBoxFMNoBookTitle.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoBookTitle.Name = "txtBoxFMNoBookTitle";
			this.txtBoxFMNoBookTitle.Size = new System.Drawing.Size(390, 20);
			this.txtBoxFMNoBookTitle.TabIndex = 1;
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label8.Location = new System.Drawing.Point(3, 8);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(186, 18);
			this.label8.TabIndex = 0;
			this.label8.Text = "Названия Книги Нет";
			// 
			// panel9
			// 
			this.panel9.Controls.Add(this.txtBoxFMNoNickName);
			this.panel9.Controls.Add(this.label7);
			this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel9.Location = new System.Drawing.Point(3, 208);
			this.panel9.Name = "panel9";
			this.panel9.Size = new System.Drawing.Size(589, 32);
			this.panel9.TabIndex = 6;
			// 
			// txtBoxFMNoNickName
			// 
			this.txtBoxFMNoNickName.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoNickName.Name = "txtBoxFMNoNickName";
			this.txtBoxFMNoNickName.Size = new System.Drawing.Size(390, 20);
			this.txtBoxFMNoNickName.TabIndex = 1;
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label7.Location = new System.Drawing.Point(3, 8);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(186, 18);
			this.label7.TabIndex = 0;
			this.label7.Text = "Ника Автора Нет";
			// 
			// panel8
			// 
			this.panel8.Controls.Add(this.txtBoxFMNoLastName);
			this.panel8.Controls.Add(this.label6);
			this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel8.Location = new System.Drawing.Point(3, 176);
			this.panel8.Name = "panel8";
			this.panel8.Size = new System.Drawing.Size(589, 32);
			this.panel8.TabIndex = 5;
			// 
			// txtBoxFMNoLastName
			// 
			this.txtBoxFMNoLastName.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoLastName.Name = "txtBoxFMNoLastName";
			this.txtBoxFMNoLastName.Size = new System.Drawing.Size(390, 20);
			this.txtBoxFMNoLastName.TabIndex = 1;
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label6.Location = new System.Drawing.Point(3, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(186, 18);
			this.label6.TabIndex = 0;
			this.label6.Text = "Фамилия Автора Нет";
			// 
			// panel7
			// 
			this.panel7.Controls.Add(this.txtBoxFMNoMiddleName);
			this.panel7.Controls.Add(this.label5);
			this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel7.Location = new System.Drawing.Point(3, 144);
			this.panel7.Name = "panel7";
			this.panel7.Size = new System.Drawing.Size(589, 32);
			this.panel7.TabIndex = 4;
			// 
			// txtBoxFMNoMiddleName
			// 
			this.txtBoxFMNoMiddleName.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoMiddleName.Name = "txtBoxFMNoMiddleName";
			this.txtBoxFMNoMiddleName.Size = new System.Drawing.Size(390, 20);
			this.txtBoxFMNoMiddleName.TabIndex = 1;
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label5.Location = new System.Drawing.Point(3, 8);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(186, 18);
			this.label5.TabIndex = 0;
			this.label5.Text = "Отчества Автора Нет";
			// 
			// panel6
			// 
			this.panel6.Controls.Add(this.txtBoxFMNoFirstName);
			this.panel6.Controls.Add(this.label4);
			this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel6.Location = new System.Drawing.Point(3, 112);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(589, 32);
			this.panel6.TabIndex = 3;
			// 
			// txtBoxFMNoFirstName
			// 
			this.txtBoxFMNoFirstName.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoFirstName.Name = "txtBoxFMNoFirstName";
			this.txtBoxFMNoFirstName.Size = new System.Drawing.Size(390, 20);
			this.txtBoxFMNoFirstName.TabIndex = 1;
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label4.Location = new System.Drawing.Point(3, 8);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(186, 18);
			this.label4.TabIndex = 0;
			this.label4.Text = "Имени Автора Нет";
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this.txtBoxFMNoLang);
			this.panel5.Controls.Add(this.label3);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel5.Location = new System.Drawing.Point(3, 80);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(589, 32);
			this.panel5.TabIndex = 2;
			// 
			// txtBoxFMNoLang
			// 
			this.txtBoxFMNoLang.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoLang.Name = "txtBoxFMNoLang";
			this.txtBoxFMNoLang.Size = new System.Drawing.Size(390, 20);
			this.txtBoxFMNoLang.TabIndex = 1;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label3.Location = new System.Drawing.Point(3, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(186, 18);
			this.label3.TabIndex = 0;
			this.label3.Text = "Языка Книги Нет";
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.txtBoxFMNoGenre);
			this.panel4.Controls.Add(this.label2);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel4.Location = new System.Drawing.Point(3, 48);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(589, 32);
			this.panel4.TabIndex = 1;
			// 
			// txtBoxFMNoGenre
			// 
			this.txtBoxFMNoGenre.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoGenre.Name = "txtBoxFMNoGenre";
			this.txtBoxFMNoGenre.Size = new System.Drawing.Size(390, 20);
			this.txtBoxFMNoGenre.TabIndex = 1;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label2.Location = new System.Drawing.Point(3, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(186, 18);
			this.label2.TabIndex = 0;
			this.label2.Text = "Жанра Книги Нет";
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.txtBoxFMNoGenreGroup);
			this.panel3.Controls.Add(this.label1);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(3, 16);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(589, 32);
			this.panel3.TabIndex = 0;
			// 
			// txtBoxFMNoGenreGroup
			// 
			this.txtBoxFMNoGenreGroup.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoGenreGroup.Name = "txtBoxFMNoGenreGroup";
			this.txtBoxFMNoGenreGroup.Size = new System.Drawing.Size(390, 20);
			this.txtBoxFMNoGenreGroup.TabIndex = 1;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.Location = new System.Drawing.Point(3, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(186, 18);
			this.label1.TabIndex = 0;
			this.label1.Text = "Неизвестная Группа Жанров:";
			// 
			// OptionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(617, 472);
			this.Controls.Add(this.tcOptions);
			this.Controls.Add(this.pBtn);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "OptionsForm";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Настройки";
			this.TopMost = true;
			this.pBtn.ResumeLayout(false);
			this.tcOptions.ResumeLayout(false);
			this.tpGeneral.ResumeLayout(false);
			this.gboxReader.ResumeLayout(false);
			this.gboxReader.PerformLayout();
			this.gboxEditors.ResumeLayout(false);
			this.gboxEditors.PerformLayout();
			this.gboxRar.ResumeLayout(false);
			this.gboxRar.PerformLayout();
			this.tpValidator.ResumeLayout(false);
			this.gboxValidatorPE.ResumeLayout(false);
			this.gboxValidatorDoubleClick.ResumeLayout(false);
			this.tpFileManager.ResumeLayout(false);
			this.tcFM.ResumeLayout(false);
			this.tpFMGeneral.ResumeLayout(false);
			this.pSortFB2.ResumeLayout(false);
			this.gboxFMGeneral.ResumeLayout(false);
			this.pFMGenres.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.gboxRegister.ResumeLayout(false);
			this.gboxApportionment.ResumeLayout(false);
			this.gBoxGenres.ResumeLayout(false);
			this.gBoxGenresType.ResumeLayout(false);
			this.gBoxGenresCount.ResumeLayout(false);
			this.gBoxAuthors.ResumeLayout(false);
			this.tpFMDirs.ResumeLayout(false);
			this.tpFMDirs.PerformLayout();
			this.tpFMNoTagsText.ResumeLayout(false);
			this.gBoxFMNoTags.ResumeLayout(false);
			this.panel12.ResumeLayout(false);
			this.panel12.PerformLayout();
			this.panel11.ResumeLayout(false);
			this.panel11.PerformLayout();
			this.panel10.ResumeLayout(false);
			this.panel10.PerformLayout();
			this.panel9.ResumeLayout(false);
			this.panel9.PerformLayout();
			this.panel8.ResumeLayout(false);
			this.panel8.PerformLayout();
			this.panel7.ResumeLayout(false);
			this.panel7.PerformLayout();
			this.panel6.ResumeLayout(false);
			this.panel6.PerformLayout();
			this.panel5.ResumeLayout(false);
			this.panel5.PerformLayout();
			this.panel4.ResumeLayout(false);
			this.panel4.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button btnDefRestore;
		private System.Windows.Forms.Button btnFB2NotValidDir;
		private System.Windows.Forms.TextBox txtBoxFB2NotValidDir;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.RadioButton rbtnFMOnleValidFB2;
		private System.Windows.Forms.RadioButton rbtnFMAllFB2;
		private System.Windows.Forms.Panel pSortFB2;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TabPage tpFMNoTagsText;
		private System.Windows.Forms.TextBox txtBoxFMNoNSequence;
		private System.Windows.Forms.TextBox txtBoxFMNoSequence;
		private System.Windows.Forms.TextBox txtBoxFMNoBookTitle;
		private System.Windows.Forms.TextBox txtBoxFMNoNickName;
		private System.Windows.Forms.TextBox txtBoxFMNoLastName;
		private System.Windows.Forms.TextBox txtBoxFMNoMiddleName;
		private System.Windows.Forms.TextBox txtBoxFMNoFirstName;
		private System.Windows.Forms.TextBox txtBoxFMNoLang;
		private System.Windows.Forms.TextBox txtBoxFMNoGenre;
		private System.Windows.Forms.TextBox txtBoxFMNoGenreGroup;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Panel panel5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Panel panel6;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Panel panel7;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Panel panel8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Panel panel9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Panel panel10;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Panel panel11;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Panel panel12;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Panel panel4;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel3;
		private System.Windows.Forms.GroupBox gBoxFMNoTags;
		private System.Windows.Forms.RadioButton rbtnFMFB21;
		private System.Windows.Forms.RadioButton rbtnFMFB22;
		private System.Windows.Forms.Label lblFMGenres;
		private System.Windows.Forms.Panel pFMGenres;
		private System.Windows.Forms.TabPage tpFMDirs;
		private System.Windows.Forms.TabPage tpFMGeneral;
		private System.Windows.Forms.TabControl tcFM;
		private System.Windows.Forms.Button btnFB2LongPathDir;
		private System.Windows.Forms.TextBox txtBoxFB2LongPathDir;
		private System.Windows.Forms.Label lblLongPath;
		private System.Windows.Forms.FolderBrowserDialog fbdDir;
		private System.Windows.Forms.Button btnFB2NotReadDir;
		private System.Windows.Forms.TextBox txtBoxFB2NotReadDir;
		private System.Windows.Forms.Label lblFB2NotReadDir;
		private System.Windows.Forms.GroupBox gboxRegister;
		private System.Windows.Forms.RadioButton rbtnGenreSchema;
		private System.Windows.Forms.RadioButton rbtnGenreText;
		private System.Windows.Forms.GroupBox gBoxGenresType;
		private System.Windows.Forms.GroupBox gBoxGenresCount;
		private System.Windows.Forms.Button btn7zaPath;
		private System.Windows.Forms.TextBox tbox7zaPath;
		private System.Windows.Forms.Label lbl7zaPath;
		private System.Windows.Forms.Button btnUnRarPath;
		private System.Windows.Forms.TextBox tboxUnRarPath;
		private System.Windows.Forms.Label lblUnRarPath;
		private System.Windows.Forms.CheckBox chBoxAddToFileNameBookID;
		private System.Windows.Forms.CheckBox chBoxDelFB2Files;
		private System.Windows.Forms.CheckBox chBoxToArchive;
		private System.Windows.Forms.ComboBox cboxSpace;
		private System.Windows.Forms.ComboBox cboxFileExist;
		private System.Windows.Forms.Label lbFilelExist;
		private System.Windows.Forms.ComboBox cboxArchiveType;
		private System.Windows.Forms.Label lblSpace;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.CheckBox chBoxTranslit;
		private System.Windows.Forms.CheckBox chBoxStrict;
		private System.Windows.Forms.GroupBox gboxFMGeneral;
		private System.Windows.Forms.GroupBox gboxApportionment;
		private System.Windows.Forms.RadioButton rbtnGenreOne;
		private System.Windows.Forms.RadioButton rbtnGenreAll;
		private System.Windows.Forms.GroupBox gBoxGenres;
		private System.Windows.Forms.RadioButton rbtnAuthorOne;
		private System.Windows.Forms.RadioButton rbtnAuthorAll;
		private System.Windows.Forms.GroupBox gBoxAuthors;
		private System.Windows.Forms.RadioButton rbtnAsIs;
		private System.Windows.Forms.RadioButton rbtnLower;
		private System.Windows.Forms.RadioButton rbtnUpper;
		private System.Windows.Forms.TabPage tpFileManager;
		private System.Windows.Forms.Label lblValidatorForFB2PE;
		private System.Windows.Forms.Label lblValidatorForFB2ArchivePE;
		private System.Windows.Forms.ComboBox cboxValidatorForFB2PE;
		private System.Windows.Forms.ComboBox cboxValidatorForFB2ArchivePE;
		private System.Windows.Forms.GroupBox gboxValidatorPE;
		private System.Windows.Forms.ComboBox cboxValidatorForFB2;
		private System.Windows.Forms.ComboBox cboxValidatorForFB2Archive;
		private System.Windows.Forms.Label lblValidatorForFB2;
		private System.Windows.Forms.Label lblValidatorForFB2Archive;
		private System.Windows.Forms.GroupBox gboxValidatorDoubleClick;
		private System.Windows.Forms.Button btnReaderPath;
		private System.Windows.Forms.TextBox tboxReaderPath;
		private System.Windows.Forms.Label lblFBReaderPath;
		private System.Windows.Forms.GroupBox gboxReader;
		private System.Windows.Forms.TextBox tboxRarPath;
		private System.Windows.Forms.Button btnRarPath;
		private System.Windows.Forms.Label lblWinRarPath;
		private System.Windows.Forms.Button btnWinRarPath;
		private System.Windows.Forms.TextBox tboxWinRarPath;
		private System.Windows.Forms.Button btnTextEPath;
		private System.Windows.Forms.TextBox tboxTextEPath;
		private System.Windows.Forms.Label lblTextEPath;
		private System.Windows.Forms.Button btnFBEPath;
		private System.Windows.Forms.TextBox tboxFBEPath;
		private System.Windows.Forms.Label lblFBEPath;
		private System.Windows.Forms.GroupBox gboxEditors;
		private System.Windows.Forms.Label lblRarPath;
		private System.Windows.Forms.GroupBox gboxRar;
		private System.Windows.Forms.TabPage tpValidator;
		private System.Windows.Forms.TabPage tpGeneral;
		private System.Windows.Forms.TabControl tcOptions;
		private System.Windows.Forms.Panel pBtn;
		private System.Windows.Forms.OpenFileDialog ofDlg;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
	}
}
