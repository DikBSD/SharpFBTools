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
			this.gboxApportionment = new System.Windows.Forms.GroupBox();
			this.gBoxGenres = new System.Windows.Forms.GroupBox();
			this.rbtnGenreAll = new System.Windows.Forms.RadioButton();
			this.rbtnGenreOne = new System.Windows.Forms.RadioButton();
			this.gBoxAuthors = new System.Windows.Forms.GroupBox();
			this.rbtnAuthorAll = new System.Windows.Forms.RadioButton();
			this.rbtnAuthorOne = new System.Windows.Forms.RadioButton();
			this.gboxFMGeneral = new System.Windows.Forms.GroupBox();
			this.chBoxDelFB2Files = new System.Windows.Forms.CheckBox();
			this.panel3 = new System.Windows.Forms.Panel();
			this.cboxFileExist = new System.Windows.Forms.ComboBox();
			this.lbFilelExist = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.cboxArchiveType = new System.Windows.Forms.ComboBox();
			this.chBoxToArchive = new System.Windows.Forms.CheckBox();
			this.panel1 = new System.Windows.Forms.Panel();
			this.nudMaxFileNameLenght = new System.Windows.Forms.NumericUpDown();
			this.cboxSpace = new System.Windows.Forms.ComboBox();
			this.chBoxFileNameLenght = new System.Windows.Forms.CheckBox();
			this.lblSpace = new System.Windows.Forms.Label();
			this.chBoxStrict = new System.Windows.Forms.CheckBox();
			this.chBoxTranslit = new System.Windows.Forms.CheckBox();
			this.gboxRegistr = new System.Windows.Forms.GroupBox();
			this.rbtnUpper = new System.Windows.Forms.RadioButton();
			this.rbtnLower = new System.Windows.Forms.RadioButton();
			this.rbtnAsIs = new System.Windows.Forms.RadioButton();
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
			this.gboxApportionment.SuspendLayout();
			this.gBoxGenres.SuspendLayout();
			this.gBoxAuthors.SuspendLayout();
			this.gboxFMGeneral.SuspendLayout();
			this.panel3.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudMaxFileNameLenght)).BeginInit();
			this.gboxRegistr.SuspendLayout();
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
			this.pBtn.Controls.Add(this.btnOK);
			this.pBtn.Controls.Add(this.btnCancel);
			this.pBtn.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pBtn.Location = new System.Drawing.Point(0, 443);
			this.pBtn.Name = "pBtn";
			this.pBtn.Size = new System.Drawing.Size(617, 29);
			this.pBtn.TabIndex = 2;
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
			this.gboxReader.Location = new System.Drawing.Point(3, 152);
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
			this.tboxReaderPath.Location = new System.Drawing.Point(171, 12);
			this.tboxReaderPath.Name = "tboxReaderPath";
			this.tboxReaderPath.ReadOnly = true;
			this.tboxReaderPath.Size = new System.Drawing.Size(381, 20);
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
			this.gboxEditors.Location = new System.Drawing.Point(3, 78);
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
			this.tboxTextEPath.Location = new System.Drawing.Point(171, 42);
			this.tboxTextEPath.Name = "tboxTextEPath";
			this.tboxTextEPath.ReadOnly = true;
			this.tboxTextEPath.Size = new System.Drawing.Size(381, 20);
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
			this.tboxFBEPath.Location = new System.Drawing.Point(171, 16);
			this.tboxFBEPath.Name = "tboxFBEPath";
			this.tboxFBEPath.ReadOnly = true;
			this.tboxFBEPath.Size = new System.Drawing.Size(381, 20);
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
			this.gboxRar.Size = new System.Drawing.Size(603, 75);
			this.gboxRar.TabIndex = 13;
			this.gboxRar.TabStop = false;
			this.gboxRar.Text = " Настройки для Rar-архиватора ";
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
			this.tboxRarPath.Location = new System.Drawing.Point(171, 47);
			this.tboxRarPath.Name = "tboxRarPath";
			this.tboxRarPath.ReadOnly = true;
			this.tboxRarPath.Size = new System.Drawing.Size(378, 20);
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
			this.tboxWinRarPath.Location = new System.Drawing.Point(171, 21);
			this.tboxWinRarPath.Name = "tboxWinRarPath";
			this.tboxWinRarPath.ReadOnly = true;
			this.tboxWinRarPath.Size = new System.Drawing.Size(378, 20);
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
			this.tpFileManager.Controls.Add(this.gboxApportionment);
			this.tpFileManager.Controls.Add(this.gboxFMGeneral);
			this.tpFileManager.Location = new System.Drawing.Point(4, 22);
			this.tpFileManager.Name = "tpFileManager";
			this.tpFileManager.Padding = new System.Windows.Forms.Padding(3);
			this.tpFileManager.Size = new System.Drawing.Size(609, 417);
			this.tpFileManager.TabIndex = 2;
			this.tpFileManager.Text = " Менеджер файлов ";
			this.tpFileManager.UseVisualStyleBackColor = true;
			// 
			// gboxApportionment
			// 
			this.gboxApportionment.Controls.Add(this.gBoxGenres);
			this.gboxApportionment.Controls.Add(this.gBoxAuthors);
			this.gboxApportionment.Dock = System.Windows.Forms.DockStyle.Top;
			this.gboxApportionment.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gboxApportionment.ForeColor = System.Drawing.Color.Maroon;
			this.gboxApportionment.Location = new System.Drawing.Point(3, 251);
			this.gboxApportionment.Name = "gboxApportionment";
			this.gboxApportionment.Size = new System.Drawing.Size(603, 87);
			this.gboxApportionment.TabIndex = 29;
			this.gboxApportionment.TabStop = false;
			this.gboxApportionment.Text = " Раскладка файлов по папкам ";
			// 
			// gBoxGenres
			// 
			this.gBoxGenres.Controls.Add(this.rbtnGenreAll);
			this.gBoxGenres.Controls.Add(this.rbtnGenreOne);
			this.gBoxGenres.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gBoxGenres.ForeColor = System.Drawing.Color.Navy;
			this.gBoxGenres.Location = new System.Drawing.Point(6, 19);
			this.gBoxGenres.Name = "gBoxGenres";
			this.gBoxGenres.Size = new System.Drawing.Size(295, 57);
			this.gBoxGenres.TabIndex = 26;
			this.gBoxGenres.TabStop = false;
			this.gBoxGenres.Text = " Раскладка файлов по жанрам ";
			// 
			// rbtnGenreAll
			// 
			this.rbtnGenreAll.Dock = System.Windows.Forms.DockStyle.Top;
			this.rbtnGenreAll.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnGenreAll.Location = new System.Drawing.Point(3, 34);
			this.rbtnGenreAll.Name = "rbtnGenreAll";
			this.rbtnGenreAll.Size = new System.Drawing.Size(289, 18);
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
			this.rbtnGenreOne.Size = new System.Drawing.Size(289, 18);
			this.rbtnGenreOne.TabIndex = 0;
			this.rbtnGenreOne.TabStop = true;
			this.rbtnGenreOne.Text = "По первому жанру";
			this.rbtnGenreOne.UseVisualStyleBackColor = true;
			// 
			// gBoxAuthors
			// 
			this.gBoxAuthors.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.gBoxAuthors.Controls.Add(this.rbtnAuthorAll);
			this.gBoxAuthors.Controls.Add(this.rbtnAuthorOne);
			this.gBoxAuthors.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gBoxAuthors.ForeColor = System.Drawing.Color.Navy;
			this.gBoxAuthors.Location = new System.Drawing.Point(307, 19);
			this.gBoxAuthors.Name = "gBoxAuthors";
			this.gBoxAuthors.Size = new System.Drawing.Size(291, 57);
			this.gBoxAuthors.TabIndex = 27;
			this.gBoxAuthors.TabStop = false;
			this.gBoxAuthors.Text = " Раскладка файлов по авторам ";
			// 
			// rbtnAuthorAll
			// 
			this.rbtnAuthorAll.Dock = System.Windows.Forms.DockStyle.Top;
			this.rbtnAuthorAll.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnAuthorAll.Location = new System.Drawing.Point(3, 34);
			this.rbtnAuthorAll.Name = "rbtnAuthorAll";
			this.rbtnAuthorAll.Size = new System.Drawing.Size(285, 18);
			this.rbtnAuthorAll.TabIndex = 1;
			this.rbtnAuthorAll.Text = "По всем авторам";
			this.rbtnAuthorAll.UseVisualStyleBackColor = true;
			// 
			// rbtnAuthorOne
			// 
			this.rbtnAuthorOne.Checked = true;
			this.rbtnAuthorOne.Dock = System.Windows.Forms.DockStyle.Top;
			this.rbtnAuthorOne.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnAuthorOne.Location = new System.Drawing.Point(3, 16);
			this.rbtnAuthorOne.Name = "rbtnAuthorOne";
			this.rbtnAuthorOne.Size = new System.Drawing.Size(285, 18);
			this.rbtnAuthorOne.TabIndex = 0;
			this.rbtnAuthorOne.TabStop = true;
			this.rbtnAuthorOne.Text = "По первому автору";
			this.rbtnAuthorOne.UseVisualStyleBackColor = true;
			// 
			// gboxFMGeneral
			// 
			this.gboxFMGeneral.Controls.Add(this.chBoxDelFB2Files);
			this.gboxFMGeneral.Controls.Add(this.panel3);
			this.gboxFMGeneral.Controls.Add(this.panel2);
			this.gboxFMGeneral.Controls.Add(this.panel1);
			this.gboxFMGeneral.Controls.Add(this.chBoxStrict);
			this.gboxFMGeneral.Controls.Add(this.chBoxTranslit);
			this.gboxFMGeneral.Controls.Add(this.gboxRegistr);
			this.gboxFMGeneral.Dock = System.Windows.Forms.DockStyle.Top;
			this.gboxFMGeneral.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gboxFMGeneral.ForeColor = System.Drawing.Color.Maroon;
			this.gboxFMGeneral.Location = new System.Drawing.Point(3, 3);
			this.gboxFMGeneral.Name = "gboxFMGeneral";
			this.gboxFMGeneral.Size = new System.Drawing.Size(603, 248);
			this.gboxFMGeneral.TabIndex = 28;
			this.gboxFMGeneral.TabStop = false;
			this.gboxFMGeneral.Text = " Основные настройки ";
			// 
			// chBoxDelFB2Files
			// 
			this.chBoxDelFB2Files.Dock = System.Windows.Forms.DockStyle.Top;
			this.chBoxDelFB2Files.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxDelFB2Files.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chBoxDelFB2Files.Location = new System.Drawing.Point(3, 138);
			this.chBoxDelFB2Files.Name = "chBoxDelFB2Files";
			this.chBoxDelFB2Files.Size = new System.Drawing.Size(597, 18);
			this.chBoxDelFB2Files.TabIndex = 24;
			this.chBoxDelFB2Files.Text = " Удалить исходные файлы после сортировки";
			this.chBoxDelFB2Files.UseVisualStyleBackColor = true;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.cboxFileExist);
			this.panel3.Controls.Add(this.lbFilelExist);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(3, 113);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(597, 25);
			this.panel3.TabIndex = 16;
			// 
			// cboxFileExist
			// 
			this.cboxFileExist.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxFileExist.FormattingEnabled = true;
			this.cboxFileExist.Items.AddRange(new object[] {
									"Заменить существующий файл новым",
									"Добавить к создаваемому файлу дату и время"});
			this.cboxFileExist.Location = new System.Drawing.Point(151, 2);
			this.cboxFileExist.Name = "cboxFileExist";
			this.cboxFileExist.Size = new System.Drawing.Size(433, 21);
			this.cboxFileExist.TabIndex = 20;
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
			// panel2
			// 
			this.panel2.Controls.Add(this.cboxArchiveType);
			this.panel2.Controls.Add(this.chBoxToArchive);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel2.Location = new System.Drawing.Point(3, 81);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(597, 32);
			this.panel2.TabIndex = 15;
			// 
			// cboxArchiveType
			// 
			this.cboxArchiveType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxArchiveType.FormattingEnabled = true;
			this.cboxArchiveType.Items.AddRange(new object[] {
									"Rar",
									"Zip",
									"7z",
									"BZip2",
									"GZip",
									"Tar"});
			this.cboxArchiveType.Location = new System.Drawing.Point(151, 5);
			this.cboxArchiveType.Name = "cboxArchiveType";
			this.cboxArchiveType.Size = new System.Drawing.Size(123, 21);
			this.cboxArchiveType.TabIndex = 17;
			// 
			// chBoxToArchive
			// 
			this.chBoxToArchive.Checked = true;
			this.chBoxToArchive.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chBoxToArchive.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxToArchive.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chBoxToArchive.Location = new System.Drawing.Point(0, 6);
			this.chBoxToArchive.Name = "chBoxToArchive";
			this.chBoxToArchive.Size = new System.Drawing.Size(153, 18);
			this.chBoxToArchive.TabIndex = 16;
			this.chBoxToArchive.Text = "Упаковывать файлы:";
			this.chBoxToArchive.UseVisualStyleBackColor = true;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.nudMaxFileNameLenght);
			this.panel1.Controls.Add(this.cboxSpace);
			this.panel1.Controls.Add(this.chBoxFileNameLenght);
			this.panel1.Controls.Add(this.lblSpace);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(3, 52);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(597, 29);
			this.panel1.TabIndex = 14;
			// 
			// nudMaxFileNameLenght
			// 
			this.nudMaxFileNameLenght.Enabled = false;
			this.nudMaxFileNameLenght.Location = new System.Drawing.Point(536, 4);
			this.nudMaxFileNameLenght.Maximum = new decimal(new int[] {
									8,
									0,
									0,
									0});
			this.nudMaxFileNameLenght.Minimum = new decimal(new int[] {
									1,
									0,
									0,
									0});
			this.nudMaxFileNameLenght.Name = "nudMaxFileNameLenght";
			this.nudMaxFileNameLenght.Size = new System.Drawing.Size(48, 20);
			this.nudMaxFileNameLenght.TabIndex = 22;
			this.nudMaxFileNameLenght.Value = new decimal(new int[] {
									8,
									0,
									0,
									0});
			// 
			// cboxSpace
			// 
			this.cboxSpace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxSpace.FormattingEnabled = true;
			this.cboxSpace.Items.AddRange(new object[] {
									"Оставить",
									"Удалить",
									"Заменит на: _",
									"Заменит на: -",
									"Заменит на: +",
									"Заменит на: *",
									"Заменит на: ="});
			this.cboxSpace.Location = new System.Drawing.Point(151, 5);
			this.cboxSpace.Name = "cboxSpace";
			this.cboxSpace.Size = new System.Drawing.Size(123, 21);
			this.cboxSpace.TabIndex = 24;
			// 
			// chBoxFileNameLenght
			// 
			this.chBoxFileNameLenght.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxFileNameLenght.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chBoxFileNameLenght.Location = new System.Drawing.Point(302, 6);
			this.chBoxFileNameLenght.Name = "chBoxFileNameLenght";
			this.chBoxFileNameLenght.Size = new System.Drawing.Size(240, 18);
			this.chBoxFileNameLenght.TabIndex = 21;
			this.chBoxFileNameLenght.Text = "Ограничить длину имени файла до:";
			this.chBoxFileNameLenght.UseVisualStyleBackColor = true;
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
			this.chBoxStrict.Checked = true;
			this.chBoxStrict.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chBoxStrict.Dock = System.Windows.Forms.DockStyle.Top;
			this.chBoxStrict.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxStrict.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chBoxStrict.Location = new System.Drawing.Point(3, 34);
			this.chBoxStrict.Name = "chBoxStrict";
			this.chBoxStrict.Size = new System.Drawing.Size(597, 18);
			this.chBoxStrict.TabIndex = 13;
			this.chBoxStrict.Text = "\"Строгие\" имена файлов: алфавитно-цифровые символы и []()<>-+=_*";
			this.chBoxStrict.UseVisualStyleBackColor = true;
			// 
			// chBoxTranslit
			// 
			this.chBoxTranslit.Checked = true;
			this.chBoxTranslit.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chBoxTranslit.Dock = System.Windows.Forms.DockStyle.Top;
			this.chBoxTranslit.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxTranslit.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chBoxTranslit.Location = new System.Drawing.Point(3, 16);
			this.chBoxTranslit.Name = "chBoxTranslit";
			this.chBoxTranslit.Size = new System.Drawing.Size(597, 18);
			this.chBoxTranslit.TabIndex = 12;
			this.chBoxTranslit.Text = "Транслитерация имен файлов";
			this.chBoxTranslit.UseVisualStyleBackColor = true;
			// 
			// gboxRegistr
			// 
			this.gboxRegistr.Controls.Add(this.rbtnUpper);
			this.gboxRegistr.Controls.Add(this.rbtnLower);
			this.gboxRegistr.Controls.Add(this.rbtnAsIs);
			this.gboxRegistr.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gboxRegistr.ForeColor = System.Drawing.Color.Navy;
			this.gboxRegistr.Location = new System.Drawing.Point(3, 162);
			this.gboxRegistr.Name = "gboxRegistr";
			this.gboxRegistr.Size = new System.Drawing.Size(160, 77);
			this.gboxRegistr.TabIndex = 10;
			this.gboxRegistr.TabStop = false;
			this.gboxRegistr.Text = " Регистр имени файла ";
			// 
			// rbtnUpper
			// 
			this.rbtnUpper.Dock = System.Windows.Forms.DockStyle.Top;
			this.rbtnUpper.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnUpper.Location = new System.Drawing.Point(3, 52);
			this.rbtnUpper.Name = "rbtnUpper";
			this.rbtnUpper.Size = new System.Drawing.Size(154, 18);
			this.rbtnUpper.TabIndex = 2;
			this.rbtnUpper.Text = "Верхний регистр";
			this.rbtnUpper.UseVisualStyleBackColor = true;
			// 
			// rbtnLower
			// 
			this.rbtnLower.Dock = System.Windows.Forms.DockStyle.Top;
			this.rbtnLower.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnLower.Location = new System.Drawing.Point(3, 34);
			this.rbtnLower.Name = "rbtnLower";
			this.rbtnLower.Size = new System.Drawing.Size(154, 18);
			this.rbtnLower.TabIndex = 1;
			this.rbtnLower.Text = "Нижний регистр";
			this.rbtnLower.UseVisualStyleBackColor = true;
			// 
			// rbtnAsIs
			// 
			this.rbtnAsIs.Checked = true;
			this.rbtnAsIs.Dock = System.Windows.Forms.DockStyle.Top;
			this.rbtnAsIs.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnAsIs.Location = new System.Drawing.Point(3, 16);
			this.rbtnAsIs.Name = "rbtnAsIs";
			this.rbtnAsIs.Size = new System.Drawing.Size(154, 18);
			this.rbtnAsIs.TabIndex = 0;
			this.rbtnAsIs.TabStop = true;
			this.rbtnAsIs.Text = "Как есть";
			this.rbtnAsIs.UseVisualStyleBackColor = true;
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
			this.gboxApportionment.ResumeLayout(false);
			this.gBoxGenres.ResumeLayout(false);
			this.gBoxAuthors.ResumeLayout(false);
			this.gboxFMGeneral.ResumeLayout(false);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.nudMaxFileNameLenght)).EndInit();
			this.gboxRegistr.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.CheckBox chBoxDelFB2Files;
		private System.Windows.Forms.CheckBox chBoxToArchive;
		private System.Windows.Forms.ComboBox cboxSpace;
		private System.Windows.Forms.ComboBox cboxFileExist;
		private System.Windows.Forms.Label lbFilelExist;
		private System.Windows.Forms.ComboBox cboxArchiveType;
		private System.Windows.Forms.CheckBox chBoxFileNameLenght;
		private System.Windows.Forms.NumericUpDown nudMaxFileNameLenght;
		private System.Windows.Forms.Label lblSpace;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel3;
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
		private System.Windows.Forms.GroupBox gboxRegistr;
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
