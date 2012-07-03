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
			this.cboxTIRFileManager = new System.Windows.Forms.ComboBox();
			this.cboxDSFileManager = new System.Windows.Forms.ComboBox();
			this.lblFileManager = new System.Windows.Forms.Label();
			this.cboxTIRValidator = new System.Windows.Forms.ComboBox();
			this.cboxDSValidator = new System.Windows.Forms.ComboBox();
			this.lblValidator = new System.Windows.Forms.Label();
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
			this.gboxFMGeneral = new System.Windows.Forms.GroupBox();
			this.pSortFB2 = new System.Windows.Forms.Panel();
			this.rbtnFMOnleValidFB2 = new System.Windows.Forms.RadioButton();
			this.rbtnFMAllFB2 = new System.Windows.Forms.RadioButton();
			this.label11 = new System.Windows.Forms.Label();
			this.pFMGenres = new System.Windows.Forms.Panel();
			this.rbtnFMFB22 = new System.Windows.Forms.RadioButton();
			this.rbtnFMFB21 = new System.Windows.Forms.RadioButton();
			this.lblFMGenres = new System.Windows.Forms.Label();
			this.chBoxAddToFileNameBookID = new System.Windows.Forms.CheckBox();
			this.panel2 = new System.Windows.Forms.Panel();
			this.cboxFileExist = new System.Windows.Forms.ComboBox();
			this.lbFilelExist = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.cboxSpace = new System.Windows.Forms.ComboBox();
			this.lblSpace = new System.Windows.Forms.Label();
			this.chBoxStrict = new System.Windows.Forms.CheckBox();
			this.chBoxTranslit = new System.Windows.Forms.CheckBox();
			this.gboxRegister = new System.Windows.Forms.GroupBox();
			this.rbtnAsSentence = new System.Windows.Forms.RadioButton();
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
			this.tpFMNoTagsText = new System.Windows.Forms.TabPage();
			this.tcDesc = new System.Windows.Forms.TabControl();
			this.tpBookInfo = new System.Windows.Forms.TabPage();
			this.gBoxFMBINoTags = new System.Windows.Forms.GroupBox();
			this.panel30 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoDateValue = new System.Windows.Forms.TextBox();
			this.label31 = new System.Windows.Forms.Label();
			this.panel29 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoDateText = new System.Windows.Forms.TextBox();
			this.label30 = new System.Windows.Forms.Label();
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
			this.tpPublishInfo = new System.Windows.Forms.TabPage();
			this.gBoxFMPINoTags = new System.Windows.Forms.GroupBox();
			this.panel33 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoCity = new System.Windows.Forms.TextBox();
			this.label34 = new System.Windows.Forms.Label();
			this.panel32 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoPublisher = new System.Windows.Forms.TextBox();
			this.label33 = new System.Windows.Forms.Label();
			this.panel31 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoYear = new System.Windows.Forms.TextBox();
			this.label32 = new System.Windows.Forms.Label();
			this.tpFB2Info = new System.Windows.Forms.TabPage();
			this.gBoxFMFB2INoTags = new System.Windows.Forms.GroupBox();
			this.panel34 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoFB2NickName = new System.Windows.Forms.TextBox();
			this.label35 = new System.Windows.Forms.Label();
			this.panel35 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoFB2LastName = new System.Windows.Forms.TextBox();
			this.label36 = new System.Windows.Forms.Label();
			this.panel36 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoFB2MiddleName = new System.Windows.Forms.TextBox();
			this.label37 = new System.Windows.Forms.Label();
			this.panel37 = new System.Windows.Forms.Panel();
			this.txtBoxFMNoFB2FirstName = new System.Windows.Forms.TextBox();
			this.label38 = new System.Windows.Forms.Label();
			this.tpFMGenreGroups = new System.Windows.Forms.TabPage();
			this.panel28 = new System.Windows.Forms.Panel();
			this.txtboxFMbusiness = new System.Windows.Forms.TextBox();
			this.label29 = new System.Windows.Forms.Label();
			this.panel27 = new System.Windows.Forms.Panel();
			this.txtboxFMhome = new System.Windows.Forms.TextBox();
			this.label28 = new System.Windows.Forms.Label();
			this.panel26 = new System.Windows.Forms.Panel();
			this.txtboxFMhumor = new System.Windows.Forms.TextBox();
			this.label27 = new System.Windows.Forms.Label();
			this.panel25 = new System.Windows.Forms.Panel();
			this.txtboxFMreligion = new System.Windows.Forms.TextBox();
			this.label26 = new System.Windows.Forms.Label();
			this.panel24 = new System.Windows.Forms.Panel();
			this.txtboxFMnonfiction = new System.Windows.Forms.TextBox();
			this.label25 = new System.Windows.Forms.Label();
			this.panel23 = new System.Windows.Forms.Panel();
			this.txtboxFMreference = new System.Windows.Forms.TextBox();
			this.label24 = new System.Windows.Forms.Label();
			this.panel22 = new System.Windows.Forms.Panel();
			this.txtboxFMcomputers = new System.Windows.Forms.TextBox();
			this.label23 = new System.Windows.Forms.Label();
			this.panel21 = new System.Windows.Forms.Panel();
			this.txtboxFMscience = new System.Windows.Forms.TextBox();
			this.label22 = new System.Windows.Forms.Label();
			this.panel20 = new System.Windows.Forms.Panel();
			this.txtboxFMantique = new System.Windows.Forms.TextBox();
			this.label21 = new System.Windows.Forms.Label();
			this.panel19 = new System.Windows.Forms.Panel();
			this.txtboxFMpoetry = new System.Windows.Forms.TextBox();
			this.label20 = new System.Windows.Forms.Label();
			this.panel18 = new System.Windows.Forms.Panel();
			this.txtboxFMchildren = new System.Windows.Forms.TextBox();
			this.label19 = new System.Windows.Forms.Label();
			this.panel17 = new System.Windows.Forms.Panel();
			this.txtboxFMadventure = new System.Windows.Forms.TextBox();
			this.label18 = new System.Windows.Forms.Label();
			this.panel16 = new System.Windows.Forms.Panel();
			this.txtboxFMlove = new System.Windows.Forms.TextBox();
			this.label17 = new System.Windows.Forms.Label();
			this.panel15 = new System.Windows.Forms.Panel();
			this.txtboxFMprose = new System.Windows.Forms.TextBox();
			this.label16 = new System.Windows.Forms.Label();
			this.panel14 = new System.Windows.Forms.Panel();
			this.txtboxFMdetective = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.panel13 = new System.Windows.Forms.Panel();
			this.txtboxFMsf = new System.Windows.Forms.TextBox();
			this.label14 = new System.Windows.Forms.Label();
			this.fbdDir = new System.Windows.Forms.FolderBrowserDialog();
			this.pBtn.SuspendLayout();
			this.tcOptions.SuspendLayout();
			this.tpGeneral.SuspendLayout();
			this.gboxButtons.SuspendLayout();
			this.gboxDiff.SuspendLayout();
			this.gboxReader.SuspendLayout();
			this.gboxEditors.SuspendLayout();
			this.gboxRar.SuspendLayout();
			this.tpValidator.SuspendLayout();
			this.gboxValidatorPE.SuspendLayout();
			this.gboxValidatorDoubleClick.SuspendLayout();
			this.tpFileManager.SuspendLayout();
			this.tcFM.SuspendLayout();
			this.tpFMGeneral.SuspendLayout();
			this.gboxFMGeneral.SuspendLayout();
			this.pSortFB2.SuspendLayout();
			this.pFMGenres.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			this.gboxRegister.SuspendLayout();
			this.gboxApportionment.SuspendLayout();
			this.gBoxGenres.SuspendLayout();
			this.gBoxGenresType.SuspendLayout();
			this.gBoxGenresCount.SuspendLayout();
			this.gBoxAuthors.SuspendLayout();
			this.tpFMNoTagsText.SuspendLayout();
			this.tcDesc.SuspendLayout();
			this.tpBookInfo.SuspendLayout();
			this.gBoxFMBINoTags.SuspendLayout();
			this.panel30.SuspendLayout();
			this.panel29.SuspendLayout();
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
			this.tpPublishInfo.SuspendLayout();
			this.gBoxFMPINoTags.SuspendLayout();
			this.panel33.SuspendLayout();
			this.panel32.SuspendLayout();
			this.panel31.SuspendLayout();
			this.tpFB2Info.SuspendLayout();
			this.gBoxFMFB2INoTags.SuspendLayout();
			this.panel34.SuspendLayout();
			this.panel35.SuspendLayout();
			this.panel36.SuspendLayout();
			this.panel37.SuspendLayout();
			this.tpFMGenreGroups.SuspendLayout();
			this.panel28.SuspendLayout();
			this.panel27.SuspendLayout();
			this.panel26.SuspendLayout();
			this.panel25.SuspendLayout();
			this.panel24.SuspendLayout();
			this.panel23.SuspendLayout();
			this.panel22.SuspendLayout();
			this.panel21.SuspendLayout();
			this.panel20.SuspendLayout();
			this.panel19.SuspendLayout();
			this.panel18.SuspendLayout();
			this.panel17.SuspendLayout();
			this.panel16.SuspendLayout();
			this.panel15.SuspendLayout();
			this.panel14.SuspendLayout();
			this.panel13.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.btnOK.Location = new System.Drawing.Point(511, 6);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(99, 26);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			this.btnOK.Click += new System.EventHandler(this.BtnOKClick);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.btnCancel.Location = new System.Drawing.Point(405, 6);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(93, 26);
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
			this.pBtn.Location = new System.Drawing.Point(0, 496);
			this.pBtn.Name = "pBtn";
			this.pBtn.Size = new System.Drawing.Size(617, 42);
			this.pBtn.TabIndex = 2;
			// 
			// btnDefRestore
			// 
			this.btnDefRestore.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.btnDefRestore.ForeColor = System.Drawing.Color.Navy;
			this.btnDefRestore.Location = new System.Drawing.Point(8, 6);
			this.btnDefRestore.Name = "btnDefRestore";
			this.btnDefRestore.Size = new System.Drawing.Size(386, 26);
			this.btnDefRestore.TabIndex = 2;
			this.btnDefRestore.Text = "Восстановить по-умолчанию (для каждой вкладки отдельно)";
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
			this.tcOptions.Size = new System.Drawing.Size(617, 496);
			this.tcOptions.TabIndex = 3;
			// 
			// tpGeneral
			// 
			this.tpGeneral.Controls.Add(this.chBoxConfirmationForExit);
			this.tpGeneral.Controls.Add(this.gboxButtons);
			this.tpGeneral.Controls.Add(this.gboxDiff);
			this.tpGeneral.Controls.Add(this.gboxReader);
			this.tpGeneral.Controls.Add(this.gboxEditors);
			this.tpGeneral.Controls.Add(this.gboxRar);
			this.tpGeneral.Location = new System.Drawing.Point(4, 22);
			this.tpGeneral.Name = "tpGeneral";
			this.tpGeneral.Padding = new System.Windows.Forms.Padding(3);
			this.tpGeneral.Size = new System.Drawing.Size(609, 470);
			this.tpGeneral.TabIndex = 0;
			this.tpGeneral.Text = " Основные ";
			this.tpGeneral.UseVisualStyleBackColor = true;
			// 
			// chBoxConfirmationForExit
			// 
			this.chBoxConfirmationForExit.Checked = true;
			this.chBoxConfirmationForExit.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chBoxConfirmationForExit.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.chBoxConfirmationForExit.Location = new System.Drawing.Point(10, 424);
			this.chBoxConfirmationForExit.Name = "chBoxConfirmationForExit";
			this.chBoxConfirmationForExit.Size = new System.Drawing.Size(338, 24);
			this.chBoxConfirmationForExit.TabIndex = 18;
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
			this.gboxButtons.Controls.Add(this.cboxTIRFileManager);
			this.gboxButtons.Controls.Add(this.cboxDSFileManager);
			this.gboxButtons.Controls.Add(this.lblFileManager);
			this.gboxButtons.Controls.Add(this.cboxTIRValidator);
			this.gboxButtons.Controls.Add(this.cboxDSValidator);
			this.gboxButtons.Controls.Add(this.lblValidator);
			this.gboxButtons.Dock = System.Windows.Forms.DockStyle.Top;
			this.gboxButtons.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gboxButtons.ForeColor = System.Drawing.Color.Maroon;
			this.gboxButtons.Location = new System.Drawing.Point(3, 294);
			this.gboxButtons.Name = "gboxButtons";
			this.gboxButtons.Size = new System.Drawing.Size(603, 121);
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
			this.cboxTIRFB2Dup.Location = new System.Drawing.Point(282, 92);
			this.cboxTIRFB2Dup.Name = "cboxTIRFB2Dup";
			this.cboxTIRFB2Dup.Size = new System.Drawing.Size(130, 21);
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
			this.cboxDSFB2Dup.Location = new System.Drawing.Point(147, 92);
			this.cboxDSFB2Dup.Name = "cboxDSFB2Dup";
			this.cboxDSFB2Dup.Size = new System.Drawing.Size(130, 21);
			this.cboxDSFB2Dup.TabIndex = 26;
			this.cboxDSFB2Dup.SelectedIndexChanged += new System.EventHandler(this.CboxDSFB2DupSelectedIndexChanged);
			// 
			// lblFB2Dup
			// 
			this.lblFB2Dup.AutoSize = true;
			this.lblFB2Dup.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblFB2Dup.Location = new System.Drawing.Point(7, 95);
			this.lblFB2Dup.Name = "lblFB2Dup";
			this.lblFB2Dup.Size = new System.Drawing.Size(128, 13);
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
			this.cboxTIRArchiveManager.Location = new System.Drawing.Point(282, 66);
			this.cboxTIRArchiveManager.Name = "cboxTIRArchiveManager";
			this.cboxTIRArchiveManager.Size = new System.Drawing.Size(130, 21);
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
			this.cboxDSArchiveManager.Location = new System.Drawing.Point(147, 66);
			this.cboxDSArchiveManager.Name = "cboxDSArchiveManager";
			this.cboxDSArchiveManager.Size = new System.Drawing.Size(130, 21);
			this.cboxDSArchiveManager.TabIndex = 23;
			this.cboxDSArchiveManager.SelectedIndexChanged += new System.EventHandler(this.CboxDSArchiveManagerSelectedIndexChanged);
			// 
			// lblArchiveManager
			// 
			this.lblArchiveManager.AutoSize = true;
			this.lblArchiveManager.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblArchiveManager.Location = new System.Drawing.Point(7, 69);
			this.lblArchiveManager.Name = "lblArchiveManager";
			this.lblArchiveManager.Size = new System.Drawing.Size(127, 13);
			this.lblArchiveManager.TabIndex = 22;
			this.lblArchiveManager.Text = "Менеджер Архивов:";
			// 
			// cboxTIRFileManager
			// 
			this.cboxTIRFileManager.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxTIRFileManager.FormattingEnabled = true;
			this.cboxTIRFileManager.Items.AddRange(new object[] {
									"Overlay",
									"ImageAboveText",
									"TextAboveImage",
									"ImageBeforeText",
									"TextBeforeImage"});
			this.cboxTIRFileManager.Location = new System.Drawing.Point(282, 41);
			this.cboxTIRFileManager.Name = "cboxTIRFileManager";
			this.cboxTIRFileManager.Size = new System.Drawing.Size(130, 21);
			this.cboxTIRFileManager.TabIndex = 21;
			// 
			// cboxDSFileManager
			// 
			this.cboxDSFileManager.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxDSFileManager.FormattingEnabled = true;
			this.cboxDSFileManager.Items.AddRange(new object[] {
									"Text",
									"Image",
									"ImageAndText"});
			this.cboxDSFileManager.Location = new System.Drawing.Point(147, 41);
			this.cboxDSFileManager.Name = "cboxDSFileManager";
			this.cboxDSFileManager.Size = new System.Drawing.Size(130, 21);
			this.cboxDSFileManager.TabIndex = 20;
			this.cboxDSFileManager.SelectedIndexChanged += new System.EventHandler(this.CboxDSFileManagerSelectedIndexChanged);
			// 
			// lblFileManager
			// 
			this.lblFileManager.AutoSize = true;
			this.lblFileManager.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblFileManager.Location = new System.Drawing.Point(7, 44);
			this.lblFileManager.Name = "lblFileManager";
			this.lblFileManager.Size = new System.Drawing.Size(134, 13);
			this.lblFileManager.TabIndex = 19;
			this.lblFileManager.Text = "Сортировщик Файлов:";
			// 
			// cboxTIRValidator
			// 
			this.cboxTIRValidator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxTIRValidator.FormattingEnabled = true;
			this.cboxTIRValidator.Items.AddRange(new object[] {
									"Overlay",
									"ImageAboveText",
									"TextAboveImage",
									"ImageBeforeText",
									"TextBeforeImage"});
			this.cboxTIRValidator.Location = new System.Drawing.Point(282, 16);
			this.cboxTIRValidator.Name = "cboxTIRValidator";
			this.cboxTIRValidator.Size = new System.Drawing.Size(130, 21);
			this.cboxTIRValidator.TabIndex = 18;
			// 
			// cboxDSValidator
			// 
			this.cboxDSValidator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxDSValidator.FormattingEnabled = true;
			this.cboxDSValidator.Items.AddRange(new object[] {
									"Text",
									"Image",
									"ImageAndText"});
			this.cboxDSValidator.Location = new System.Drawing.Point(147, 16);
			this.cboxDSValidator.Name = "cboxDSValidator";
			this.cboxDSValidator.Size = new System.Drawing.Size(130, 21);
			this.cboxDSValidator.TabIndex = 17;
			this.cboxDSValidator.SelectedIndexChanged += new System.EventHandler(this.CboxDSValidatorSelectedIndexChanged);
			// 
			// lblValidator
			// 
			this.lblValidator.AutoSize = true;
			this.lblValidator.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblValidator.Location = new System.Drawing.Point(7, 19);
			this.lblValidator.Name = "lblValidator";
			this.lblValidator.Size = new System.Drawing.Size(74, 13);
			this.lblValidator.TabIndex = 16;
			this.lblValidator.Text = "Валидатор:";
			// 
			// gboxDiff
			// 
			this.gboxDiff.Controls.Add(this.lblDiffPath);
			this.gboxDiff.Controls.Add(this.tboxDiffPath);
			this.gboxDiff.Controls.Add(this.btnDiffPath);
			this.gboxDiff.Dock = System.Windows.Forms.DockStyle.Top;
			this.gboxDiff.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gboxDiff.ForeColor = System.Drawing.Color.Maroon;
			this.gboxDiff.Location = new System.Drawing.Point(3, 249);
			this.gboxDiff.Name = "gboxDiff";
			this.gboxDiff.Size = new System.Drawing.Size(603, 45);
			this.gboxDiff.TabIndex = 16;
			this.gboxDiff.TabStop = false;
			this.gboxDiff.Text = " Diff-программа визуального сравнения ";
			// 
			// lblDiffPath
			// 
			this.lblDiffPath.AutoSize = true;
			this.lblDiffPath.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblDiffPath.Location = new System.Drawing.Point(7, 19);
			this.lblDiffPath.Name = "lblDiffPath";
			this.lblDiffPath.Size = new System.Drawing.Size(139, 13);
			this.lblDiffPath.TabIndex = 16;
			this.lblDiffPath.Text = "Путь к diff-программе:";
			// 
			// tboxDiffPath
			// 
			this.tboxDiffPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxDiffPath.Location = new System.Drawing.Point(184, 16);
			this.tboxDiffPath.Name = "tboxDiffPath";
			this.tboxDiffPath.ReadOnly = true;
			this.tboxDiffPath.Size = new System.Drawing.Size(368, 20);
			this.tboxDiffPath.TabIndex = 14;
			// 
			// btnDiffPath
			// 
			this.btnDiffPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDiffPath.Image = ((System.Drawing.Image)(resources.GetObject("btnDiffPath.Image")));
			this.btnDiffPath.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnDiffPath.Location = new System.Drawing.Point(558, 13);
			this.btnDiffPath.Name = "btnDiffPath";
			this.btnDiffPath.Size = new System.Drawing.Size(37, 24);
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
			this.tboxReaderPath.Location = new System.Drawing.Point(184, 15);
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
			this.btnReaderPath.Location = new System.Drawing.Point(558, 13);
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
			this.tpValidator.Size = new System.Drawing.Size(609, 470);
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
			this.tpFileManager.Size = new System.Drawing.Size(609, 470);
			this.tpFileManager.TabIndex = 2;
			this.tpFileManager.Text = "Сортировщик";
			this.tpFileManager.UseVisualStyleBackColor = true;
			// 
			// tcFM
			// 
			this.tcFM.Controls.Add(this.tpFMGeneral);
			this.tcFM.Controls.Add(this.tpFMNoTagsText);
			this.tcFM.Controls.Add(this.tpFMGenreGroups);
			this.tcFM.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcFM.Location = new System.Drawing.Point(3, 3);
			this.tcFM.Name = "tcFM";
			this.tcFM.SelectedIndex = 0;
			this.tcFM.Size = new System.Drawing.Size(603, 464);
			this.tcFM.TabIndex = 36;
			// 
			// tpFMGeneral
			// 
			this.tpFMGeneral.Controls.Add(this.gboxFMGeneral);
			this.tpFMGeneral.Controls.Add(this.gboxApportionment);
			this.tpFMGeneral.Location = new System.Drawing.Point(4, 22);
			this.tpFMGeneral.Name = "tpFMGeneral";
			this.tpFMGeneral.Padding = new System.Windows.Forms.Padding(3);
			this.tpFMGeneral.Size = new System.Drawing.Size(595, 438);
			this.tpFMGeneral.TabIndex = 0;
			this.tpFMGeneral.Text = " Основные ";
			this.tpFMGeneral.UseVisualStyleBackColor = true;
			// 
			// gboxFMGeneral
			// 
			this.gboxFMGeneral.Controls.Add(this.pSortFB2);
			this.gboxFMGeneral.Controls.Add(this.pFMGenres);
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
			this.gboxFMGeneral.Size = new System.Drawing.Size(589, 243);
			this.gboxFMGeneral.TabIndex = 28;
			this.gboxFMGeneral.TabStop = false;
			this.gboxFMGeneral.Text = " Основные настройки ";
			// 
			// pSortFB2
			// 
			this.pSortFB2.Controls.Add(this.rbtnFMOnleValidFB2);
			this.pSortFB2.Controls.Add(this.rbtnFMAllFB2);
			this.pSortFB2.Controls.Add(this.label11);
			this.pSortFB2.Dock = System.Windows.Forms.DockStyle.Top;
			this.pSortFB2.Location = new System.Drawing.Point(3, 186);
			this.pSortFB2.Name = "pSortFB2";
			this.pSortFB2.Size = new System.Drawing.Size(583, 23);
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
			// pFMGenres
			// 
			this.pFMGenres.Controls.Add(this.rbtnFMFB22);
			this.pFMGenres.Controls.Add(this.rbtnFMFB21);
			this.pFMGenres.Controls.Add(this.lblFMGenres);
			this.pFMGenres.Dock = System.Windows.Forms.DockStyle.Top;
			this.pFMGenres.Location = new System.Drawing.Point(3, 163);
			this.pFMGenres.Name = "pFMGenres";
			this.pFMGenres.Size = new System.Drawing.Size(583, 23);
			this.pFMGenres.TabIndex = 26;
			// 
			// rbtnFMFB22
			// 
			this.rbtnFMFB22.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnFMFB22.Location = new System.Drawing.Point(220, 2);
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
			this.rbtnFMFB21.Location = new System.Drawing.Point(141, 2);
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
			this.cboxFileExist.Size = new System.Drawing.Size(425, 21);
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
			this.panel1.Controls.Add(this.cboxSpace);
			this.panel1.Controls.Add(this.lblSpace);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(3, 91);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(583, 29);
			this.panel1.TabIndex = 14;
			// 
			// cboxSpace
			// 
			this.cboxSpace.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxSpace.FormattingEnabled = true;
			this.cboxSpace.Items.AddRange(new object[] {
									"Оставить",
									"Удалить",
									"Заменить на  _",
									"Заменить на  -",
									"Заменить на  +",
									"Заменить на  ~"});
			this.cboxSpace.Location = new System.Drawing.Point(151, 5);
			this.cboxSpace.Name = "cboxSpace";
			this.cboxSpace.Size = new System.Drawing.Size(123, 21);
			this.cboxSpace.TabIndex = 24;
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
			this.gboxRegister.Controls.Add(this.rbtnAsSentence);
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
			// rbtnAsSentence
			// 
			this.rbtnAsSentence.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnAsSentence.Location = new System.Drawing.Point(359, 16);
			this.rbtnAsSentence.Name = "rbtnAsSentence";
			this.rbtnAsSentence.Size = new System.Drawing.Size(217, 18);
			this.rbtnAsSentence.TabIndex = 3;
			this.rbtnAsSentence.Text = "Каждое Слово С Большой Буквы";
			this.rbtnAsSentence.UseVisualStyleBackColor = true;
			// 
			// rbtnUpper
			// 
			this.rbtnUpper.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnUpper.Location = new System.Drawing.Point(214, 16);
			this.rbtnUpper.Name = "rbtnUpper";
			this.rbtnUpper.Size = new System.Drawing.Size(139, 18);
			this.rbtnUpper.TabIndex = 2;
			this.rbtnUpper.Text = "ПРОПИСНЫЕ БУКВЫ";
			this.rbtnUpper.UseVisualStyleBackColor = true;
			// 
			// rbtnLower
			// 
			this.rbtnLower.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnLower.Location = new System.Drawing.Point(83, 16);
			this.rbtnLower.Name = "rbtnLower";
			this.rbtnLower.Size = new System.Drawing.Size(127, 18);
			this.rbtnLower.TabIndex = 1;
			this.rbtnLower.Text = "строчные буквы";
			this.rbtnLower.UseVisualStyleBackColor = true;
			// 
			// rbtnAsIs
			// 
			this.rbtnAsIs.Checked = true;
			this.rbtnAsIs.ForeColor = System.Drawing.SystemColors.ControlText;
			this.rbtnAsIs.Location = new System.Drawing.Point(3, 16);
			this.rbtnAsIs.Name = "rbtnAsIs";
			this.rbtnAsIs.Size = new System.Drawing.Size(78, 18);
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
			this.gboxApportionment.Location = new System.Drawing.Point(3, 246);
			this.gboxApportionment.Name = "gboxApportionment";
			this.gboxApportionment.Size = new System.Drawing.Size(589, 189);
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
			this.gBoxGenres.Size = new System.Drawing.Size(583, 91);
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
			// tpFMNoTagsText
			// 
			this.tpFMNoTagsText.Controls.Add(this.tcDesc);
			this.tpFMNoTagsText.Location = new System.Drawing.Point(4, 22);
			this.tpFMNoTagsText.Name = "tpFMNoTagsText";
			this.tpFMNoTagsText.Size = new System.Drawing.Size(595, 438);
			this.tpFMNoTagsText.TabIndex = 2;
			this.tpFMNoTagsText.Text = " Папки шаблонного тэга без данных ";
			this.tpFMNoTagsText.UseVisualStyleBackColor = true;
			// 
			// tcDesc
			// 
			this.tcDesc.Alignment = System.Windows.Forms.TabAlignment.Bottom;
			this.tcDesc.Controls.Add(this.tpBookInfo);
			this.tcDesc.Controls.Add(this.tpPublishInfo);
			this.tcDesc.Controls.Add(this.tpFB2Info);
			this.tcDesc.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcDesc.Location = new System.Drawing.Point(0, 0);
			this.tcDesc.Name = "tcDesc";
			this.tcDesc.SelectedIndex = 0;
			this.tcDesc.Size = new System.Drawing.Size(595, 438);
			this.tcDesc.TabIndex = 0;
			// 
			// tpBookInfo
			// 
			this.tpBookInfo.Controls.Add(this.gBoxFMBINoTags);
			this.tpBookInfo.Location = new System.Drawing.Point(4, 4);
			this.tpBookInfo.Name = "tpBookInfo";
			this.tpBookInfo.Padding = new System.Windows.Forms.Padding(3);
			this.tpBookInfo.Size = new System.Drawing.Size(587, 412);
			this.tpBookInfo.TabIndex = 0;
			this.tpBookInfo.Text = " Книга ";
			this.tpBookInfo.UseVisualStyleBackColor = true;
			// 
			// gBoxFMBINoTags
			// 
			this.gBoxFMBINoTags.Controls.Add(this.panel30);
			this.gBoxFMBINoTags.Controls.Add(this.panel29);
			this.gBoxFMBINoTags.Controls.Add(this.panel12);
			this.gBoxFMBINoTags.Controls.Add(this.panel11);
			this.gBoxFMBINoTags.Controls.Add(this.panel10);
			this.gBoxFMBINoTags.Controls.Add(this.panel9);
			this.gBoxFMBINoTags.Controls.Add(this.panel8);
			this.gBoxFMBINoTags.Controls.Add(this.panel7);
			this.gBoxFMBINoTags.Controls.Add(this.panel6);
			this.gBoxFMBINoTags.Controls.Add(this.panel5);
			this.gBoxFMBINoTags.Controls.Add(this.panel4);
			this.gBoxFMBINoTags.Controls.Add(this.panel3);
			this.gBoxFMBINoTags.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gBoxFMBINoTags.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.gBoxFMBINoTags.ForeColor = System.Drawing.Color.Maroon;
			this.gBoxFMBINoTags.Location = new System.Drawing.Point(3, 3);
			this.gBoxFMBINoTags.Name = "gBoxFMBINoTags";
			this.gBoxFMBINoTags.Size = new System.Drawing.Size(581, 406);
			this.gBoxFMBINoTags.TabIndex = 1;
			this.gBoxFMBINoTags.TabStop = false;
			this.gBoxFMBINoTags.Text = " Для отсутствующих данных тэгов ";
			// 
			// panel30
			// 
			this.panel30.Controls.Add(this.txtBoxFMNoDateValue);
			this.panel30.Controls.Add(this.label31);
			this.panel30.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel30.Location = new System.Drawing.Point(3, 368);
			this.panel30.Name = "panel30";
			this.panel30.Size = new System.Drawing.Size(575, 32);
			this.panel30.TabIndex = 11;
			// 
			// txtBoxFMNoDateValue
			// 
			this.txtBoxFMNoDateValue.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoDateValue.Name = "txtBoxFMNoDateValue";
			this.txtBoxFMNoDateValue.Size = new System.Drawing.Size(390, 20);
			this.txtBoxFMNoDateValue.TabIndex = 1;
			// 
			// label31
			// 
			this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label31.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label31.Location = new System.Drawing.Point(3, 8);
			this.label31.Name = "label31";
			this.label31.Size = new System.Drawing.Size(186, 18);
			this.label31.TabIndex = 0;
			this.label31.Text = "Даты написания (знач.) Нет:";
			this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel29
			// 
			this.panel29.Controls.Add(this.txtBoxFMNoDateText);
			this.panel29.Controls.Add(this.label30);
			this.panel29.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel29.Location = new System.Drawing.Point(3, 336);
			this.panel29.Name = "panel29";
			this.panel29.Size = new System.Drawing.Size(575, 32);
			this.panel29.TabIndex = 10;
			// 
			// txtBoxFMNoDateText
			// 
			this.txtBoxFMNoDateText.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoDateText.Name = "txtBoxFMNoDateText";
			this.txtBoxFMNoDateText.Size = new System.Drawing.Size(390, 20);
			this.txtBoxFMNoDateText.TabIndex = 1;
			// 
			// label30
			// 
			this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label30.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label30.Location = new System.Drawing.Point(3, 8);
			this.label30.Name = "label30";
			this.label30.Size = new System.Drawing.Size(186, 18);
			this.label30.TabIndex = 0;
			this.label30.Text = "Даты написания (текст) Нет:";
			this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel12
			// 
			this.panel12.Controls.Add(this.txtBoxFMNoNSequence);
			this.panel12.Controls.Add(this.label10);
			this.panel12.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel12.Location = new System.Drawing.Point(3, 304);
			this.panel12.Name = "panel12";
			this.panel12.Size = new System.Drawing.Size(575, 32);
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
			this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label10.Location = new System.Drawing.Point(3, 8);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(186, 18);
			this.label10.TabIndex = 0;
			this.label10.Text = "Номера Серии Нет:";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel11
			// 
			this.panel11.Controls.Add(this.txtBoxFMNoSequence);
			this.panel11.Controls.Add(this.label9);
			this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel11.Location = new System.Drawing.Point(3, 272);
			this.panel11.Name = "panel11";
			this.panel11.Size = new System.Drawing.Size(575, 32);
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
			this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label9.Location = new System.Drawing.Point(3, 8);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(186, 18);
			this.label9.TabIndex = 0;
			this.label9.Text = "Серии Нет:";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel10
			// 
			this.panel10.Controls.Add(this.txtBoxFMNoBookTitle);
			this.panel10.Controls.Add(this.label8);
			this.panel10.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel10.Location = new System.Drawing.Point(3, 240);
			this.panel10.Name = "panel10";
			this.panel10.Size = new System.Drawing.Size(575, 32);
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
			this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label8.Location = new System.Drawing.Point(3, 8);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(186, 18);
			this.label8.TabIndex = 0;
			this.label8.Text = "Названия Книги Нет:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel9
			// 
			this.panel9.Controls.Add(this.txtBoxFMNoNickName);
			this.panel9.Controls.Add(this.label7);
			this.panel9.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel9.Location = new System.Drawing.Point(3, 208);
			this.panel9.Name = "panel9";
			this.panel9.Size = new System.Drawing.Size(575, 32);
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
			this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label7.Location = new System.Drawing.Point(3, 8);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(186, 18);
			this.label7.TabIndex = 0;
			this.label7.Text = "Ника Автора Нет:";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel8
			// 
			this.panel8.Controls.Add(this.txtBoxFMNoLastName);
			this.panel8.Controls.Add(this.label6);
			this.panel8.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel8.Location = new System.Drawing.Point(3, 176);
			this.panel8.Name = "panel8";
			this.panel8.Size = new System.Drawing.Size(575, 32);
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
			this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label6.Location = new System.Drawing.Point(3, 8);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(186, 18);
			this.label6.TabIndex = 0;
			this.label6.Text = "Фамилия Автора Нет:";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel7
			// 
			this.panel7.Controls.Add(this.txtBoxFMNoMiddleName);
			this.panel7.Controls.Add(this.label5);
			this.panel7.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel7.Location = new System.Drawing.Point(3, 144);
			this.panel7.Name = "panel7";
			this.panel7.Size = new System.Drawing.Size(575, 32);
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
			this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label5.Location = new System.Drawing.Point(3, 8);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(186, 18);
			this.label5.TabIndex = 0;
			this.label5.Text = "Отчества Автора Нет:";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel6
			// 
			this.panel6.Controls.Add(this.txtBoxFMNoFirstName);
			this.panel6.Controls.Add(this.label4);
			this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel6.Location = new System.Drawing.Point(3, 112);
			this.panel6.Name = "panel6";
			this.panel6.Size = new System.Drawing.Size(575, 32);
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
			this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label4.Location = new System.Drawing.Point(3, 8);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(186, 18);
			this.label4.TabIndex = 0;
			this.label4.Text = "Имени Автора Нет:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel5
			// 
			this.panel5.Controls.Add(this.txtBoxFMNoLang);
			this.panel5.Controls.Add(this.label3);
			this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel5.Location = new System.Drawing.Point(3, 80);
			this.panel5.Name = "panel5";
			this.panel5.Size = new System.Drawing.Size(575, 32);
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
			this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label3.Location = new System.Drawing.Point(3, 8);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(186, 18);
			this.label3.TabIndex = 0;
			this.label3.Text = "Языка Книги Нет:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel4
			// 
			this.panel4.Controls.Add(this.txtBoxFMNoGenre);
			this.panel4.Controls.Add(this.label2);
			this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel4.Location = new System.Drawing.Point(3, 48);
			this.panel4.Name = "panel4";
			this.panel4.Size = new System.Drawing.Size(575, 32);
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
			this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label2.Location = new System.Drawing.Point(3, 8);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(186, 18);
			this.label2.TabIndex = 0;
			this.label2.Text = "Жанра Книги Нет:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel3
			// 
			this.panel3.Controls.Add(this.txtBoxFMNoGenreGroup);
			this.panel3.Controls.Add(this.label1);
			this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel3.Location = new System.Drawing.Point(3, 16);
			this.panel3.Name = "panel3";
			this.panel3.Size = new System.Drawing.Size(575, 32);
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
			this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label1.Location = new System.Drawing.Point(3, 8);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(186, 18);
			this.label1.TabIndex = 0;
			this.label1.Text = "Неизвестная Группа Жанров:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tpPublishInfo
			// 
			this.tpPublishInfo.Controls.Add(this.gBoxFMPINoTags);
			this.tpPublishInfo.Location = new System.Drawing.Point(4, 4);
			this.tpPublishInfo.Name = "tpPublishInfo";
			this.tpPublishInfo.Padding = new System.Windows.Forms.Padding(3);
			this.tpPublishInfo.Size = new System.Drawing.Size(587, 412);
			this.tpPublishInfo.TabIndex = 1;
			this.tpPublishInfo.Text = " Издательство ";
			this.tpPublishInfo.UseVisualStyleBackColor = true;
			// 
			// gBoxFMPINoTags
			// 
			this.gBoxFMPINoTags.Controls.Add(this.panel33);
			this.gBoxFMPINoTags.Controls.Add(this.panel32);
			this.gBoxFMPINoTags.Controls.Add(this.panel31);
			this.gBoxFMPINoTags.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gBoxFMPINoTags.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gBoxFMPINoTags.ForeColor = System.Drawing.Color.Maroon;
			this.gBoxFMPINoTags.Location = new System.Drawing.Point(3, 3);
			this.gBoxFMPINoTags.Name = "gBoxFMPINoTags";
			this.gBoxFMPINoTags.Size = new System.Drawing.Size(581, 406);
			this.gBoxFMPINoTags.TabIndex = 0;
			this.gBoxFMPINoTags.TabStop = false;
			this.gBoxFMPINoTags.Text = " Для отсутствующих данных тэгов ";
			// 
			// panel33
			// 
			this.panel33.Controls.Add(this.txtBoxFMNoCity);
			this.panel33.Controls.Add(this.label34);
			this.panel33.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel33.Location = new System.Drawing.Point(3, 80);
			this.panel33.Name = "panel33";
			this.panel33.Size = new System.Drawing.Size(575, 32);
			this.panel33.TabIndex = 15;
			// 
			// txtBoxFMNoCity
			// 
			this.txtBoxFMNoCity.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoCity.Name = "txtBoxFMNoCity";
			this.txtBoxFMNoCity.Size = new System.Drawing.Size(390, 20);
			this.txtBoxFMNoCity.TabIndex = 1;
			// 
			// label34
			// 
			this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label34.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label34.Location = new System.Drawing.Point(3, 8);
			this.label34.Name = "label34";
			this.label34.Size = new System.Drawing.Size(186, 18);
			this.label34.TabIndex = 0;
			this.label34.Text = "Города Издательства Нет:";
			this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel32
			// 
			this.panel32.Controls.Add(this.txtBoxFMNoPublisher);
			this.panel32.Controls.Add(this.label33);
			this.panel32.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel32.Location = new System.Drawing.Point(3, 48);
			this.panel32.Name = "panel32";
			this.panel32.Size = new System.Drawing.Size(575, 32);
			this.panel32.TabIndex = 14;
			// 
			// txtBoxFMNoPublisher
			// 
			this.txtBoxFMNoPublisher.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoPublisher.Name = "txtBoxFMNoPublisher";
			this.txtBoxFMNoPublisher.Size = new System.Drawing.Size(390, 20);
			this.txtBoxFMNoPublisher.TabIndex = 1;
			// 
			// label33
			// 
			this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label33.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label33.Location = new System.Drawing.Point(3, 8);
			this.label33.Name = "label33";
			this.label33.Size = new System.Drawing.Size(186, 18);
			this.label33.TabIndex = 0;
			this.label33.Text = "Издательства Нет:";
			this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel31
			// 
			this.panel31.Controls.Add(this.txtBoxFMNoYear);
			this.panel31.Controls.Add(this.label32);
			this.panel31.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel31.Location = new System.Drawing.Point(3, 16);
			this.panel31.Name = "panel31";
			this.panel31.Size = new System.Drawing.Size(575, 32);
			this.panel31.TabIndex = 13;
			// 
			// txtBoxFMNoYear
			// 
			this.txtBoxFMNoYear.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoYear.Name = "txtBoxFMNoYear";
			this.txtBoxFMNoYear.Size = new System.Drawing.Size(390, 20);
			this.txtBoxFMNoYear.TabIndex = 1;
			// 
			// label32
			// 
			this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label32.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label32.Location = new System.Drawing.Point(3, 8);
			this.label32.Name = "label32";
			this.label32.Size = new System.Drawing.Size(186, 18);
			this.label32.TabIndex = 0;
			this.label32.Text = "Года издания Книги Нет:";
			this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tpFB2Info
			// 
			this.tpFB2Info.Controls.Add(this.gBoxFMFB2INoTags);
			this.tpFB2Info.Location = new System.Drawing.Point(4, 4);
			this.tpFB2Info.Name = "tpFB2Info";
			this.tpFB2Info.Padding = new System.Windows.Forms.Padding(3);
			this.tpFB2Info.Size = new System.Drawing.Size(587, 412);
			this.tpFB2Info.TabIndex = 2;
			this.tpFB2Info.Text = " FB2-файл ";
			this.tpFB2Info.UseVisualStyleBackColor = true;
			// 
			// gBoxFMFB2INoTags
			// 
			this.gBoxFMFB2INoTags.Controls.Add(this.panel34);
			this.gBoxFMFB2INoTags.Controls.Add(this.panel35);
			this.gBoxFMFB2INoTags.Controls.Add(this.panel36);
			this.gBoxFMFB2INoTags.Controls.Add(this.panel37);
			this.gBoxFMFB2INoTags.Dock = System.Windows.Forms.DockStyle.Fill;
			this.gBoxFMFB2INoTags.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gBoxFMFB2INoTags.ForeColor = System.Drawing.Color.Maroon;
			this.gBoxFMFB2INoTags.Location = new System.Drawing.Point(3, 3);
			this.gBoxFMFB2INoTags.Name = "gBoxFMFB2INoTags";
			this.gBoxFMFB2INoTags.Size = new System.Drawing.Size(581, 406);
			this.gBoxFMFB2INoTags.TabIndex = 1;
			this.gBoxFMFB2INoTags.TabStop = false;
			this.gBoxFMFB2INoTags.Text = " Для отсутствующих данных тэгов ";
			// 
			// panel34
			// 
			this.panel34.Controls.Add(this.txtBoxFMNoFB2NickName);
			this.panel34.Controls.Add(this.label35);
			this.panel34.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel34.Location = new System.Drawing.Point(3, 112);
			this.panel34.Name = "panel34";
			this.panel34.Size = new System.Drawing.Size(575, 32);
			this.panel34.TabIndex = 14;
			// 
			// txtBoxFMNoFB2NickName
			// 
			this.txtBoxFMNoFB2NickName.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoFB2NickName.Name = "txtBoxFMNoFB2NickName";
			this.txtBoxFMNoFB2NickName.Size = new System.Drawing.Size(390, 20);
			this.txtBoxFMNoFB2NickName.TabIndex = 1;
			// 
			// label35
			// 
			this.label35.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label35.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label35.Location = new System.Drawing.Point(3, 8);
			this.label35.Name = "label35";
			this.label35.Size = new System.Drawing.Size(186, 18);
			this.label35.TabIndex = 0;
			this.label35.Text = "Ника fb2-создателя Нет:";
			this.label35.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel35
			// 
			this.panel35.Controls.Add(this.txtBoxFMNoFB2LastName);
			this.panel35.Controls.Add(this.label36);
			this.panel35.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel35.Location = new System.Drawing.Point(3, 80);
			this.panel35.Name = "panel35";
			this.panel35.Size = new System.Drawing.Size(575, 32);
			this.panel35.TabIndex = 13;
			// 
			// txtBoxFMNoFB2LastName
			// 
			this.txtBoxFMNoFB2LastName.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoFB2LastName.Name = "txtBoxFMNoFB2LastName";
			this.txtBoxFMNoFB2LastName.Size = new System.Drawing.Size(390, 20);
			this.txtBoxFMNoFB2LastName.TabIndex = 1;
			// 
			// label36
			// 
			this.label36.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label36.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label36.Location = new System.Drawing.Point(3, 8);
			this.label36.Name = "label36";
			this.label36.Size = new System.Drawing.Size(186, 18);
			this.label36.TabIndex = 0;
			this.label36.Text = "Фамилия fb2-создателя Нет:";
			this.label36.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel36
			// 
			this.panel36.Controls.Add(this.txtBoxFMNoFB2MiddleName);
			this.panel36.Controls.Add(this.label37);
			this.panel36.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel36.Location = new System.Drawing.Point(3, 48);
			this.panel36.Name = "panel36";
			this.panel36.Size = new System.Drawing.Size(575, 32);
			this.panel36.TabIndex = 12;
			// 
			// txtBoxFMNoFB2MiddleName
			// 
			this.txtBoxFMNoFB2MiddleName.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoFB2MiddleName.Name = "txtBoxFMNoFB2MiddleName";
			this.txtBoxFMNoFB2MiddleName.Size = new System.Drawing.Size(390, 20);
			this.txtBoxFMNoFB2MiddleName.TabIndex = 1;
			// 
			// label37
			// 
			this.label37.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label37.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label37.Location = new System.Drawing.Point(3, 8);
			this.label37.Name = "label37";
			this.label37.Size = new System.Drawing.Size(186, 18);
			this.label37.TabIndex = 0;
			this.label37.Text = "Отчества fb2-создателя Нет:";
			this.label37.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel37
			// 
			this.panel37.Controls.Add(this.txtBoxFMNoFB2FirstName);
			this.panel37.Controls.Add(this.label38);
			this.panel37.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel37.Location = new System.Drawing.Point(3, 16);
			this.panel37.Name = "panel37";
			this.panel37.Size = new System.Drawing.Size(575, 32);
			this.panel37.TabIndex = 11;
			// 
			// txtBoxFMNoFB2FirstName
			// 
			this.txtBoxFMNoFB2FirstName.Location = new System.Drawing.Point(193, 6);
			this.txtBoxFMNoFB2FirstName.Name = "txtBoxFMNoFB2FirstName";
			this.txtBoxFMNoFB2FirstName.Size = new System.Drawing.Size(390, 20);
			this.txtBoxFMNoFB2FirstName.TabIndex = 1;
			// 
			// label38
			// 
			this.label38.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label38.ForeColor = System.Drawing.SystemColors.ControlText;
			this.label38.Location = new System.Drawing.Point(3, 8);
			this.label38.Name = "label38";
			this.label38.Size = new System.Drawing.Size(186, 18);
			this.label38.TabIndex = 0;
			this.label38.Text = "Имени fb2-создателя Нет:";
			this.label38.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tpFMGenreGroups
			// 
			this.tpFMGenreGroups.Controls.Add(this.panel28);
			this.tpFMGenreGroups.Controls.Add(this.panel27);
			this.tpFMGenreGroups.Controls.Add(this.panel26);
			this.tpFMGenreGroups.Controls.Add(this.panel25);
			this.tpFMGenreGroups.Controls.Add(this.panel24);
			this.tpFMGenreGroups.Controls.Add(this.panel23);
			this.tpFMGenreGroups.Controls.Add(this.panel22);
			this.tpFMGenreGroups.Controls.Add(this.panel21);
			this.tpFMGenreGroups.Controls.Add(this.panel20);
			this.tpFMGenreGroups.Controls.Add(this.panel19);
			this.tpFMGenreGroups.Controls.Add(this.panel18);
			this.tpFMGenreGroups.Controls.Add(this.panel17);
			this.tpFMGenreGroups.Controls.Add(this.panel16);
			this.tpFMGenreGroups.Controls.Add(this.panel15);
			this.tpFMGenreGroups.Controls.Add(this.panel14);
			this.tpFMGenreGroups.Controls.Add(this.panel13);
			this.tpFMGenreGroups.Location = new System.Drawing.Point(4, 22);
			this.tpFMGenreGroups.Name = "tpFMGenreGroups";
			this.tpFMGenreGroups.Padding = new System.Windows.Forms.Padding(3);
			this.tpFMGenreGroups.Size = new System.Drawing.Size(595, 438);
			this.tpFMGenreGroups.TabIndex = 3;
			this.tpFMGenreGroups.Text = " Группы Жанров ";
			this.tpFMGenreGroups.UseVisualStyleBackColor = true;
			// 
			// panel28
			// 
			this.panel28.Controls.Add(this.txtboxFMbusiness);
			this.panel28.Controls.Add(this.label29);
			this.panel28.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel28.Location = new System.Drawing.Point(3, 393);
			this.panel28.Name = "panel28";
			this.panel28.Size = new System.Drawing.Size(589, 26);
			this.panel28.TabIndex = 16;
			// 
			// txtboxFMbusiness
			// 
			this.txtboxFMbusiness.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMbusiness.Location = new System.Drawing.Point(159, 2);
			this.txtboxFMbusiness.Name = "txtboxFMbusiness";
			this.txtboxFMbusiness.Size = new System.Drawing.Size(424, 20);
			this.txtboxFMbusiness.TabIndex = 1;
			// 
			// label29
			// 
			this.label29.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label29.Location = new System.Drawing.Point(3, 5);
			this.label29.Name = "label29";
			this.label29.Size = new System.Drawing.Size(150, 16);
			this.label29.TabIndex = 0;
			this.label29.Text = "Бизнес:";
			this.label29.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel27
			// 
			this.panel27.Controls.Add(this.txtboxFMhome);
			this.panel27.Controls.Add(this.label28);
			this.panel27.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel27.Location = new System.Drawing.Point(3, 367);
			this.panel27.Name = "panel27";
			this.panel27.Size = new System.Drawing.Size(589, 26);
			this.panel27.TabIndex = 15;
			// 
			// txtboxFMhome
			// 
			this.txtboxFMhome.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMhome.Location = new System.Drawing.Point(159, 2);
			this.txtboxFMhome.Name = "txtboxFMhome";
			this.txtboxFMhome.Size = new System.Drawing.Size(424, 20);
			this.txtboxFMhome.TabIndex = 1;
			// 
			// label28
			// 
			this.label28.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label28.Location = new System.Drawing.Point(3, 5);
			this.label28.Name = "label28";
			this.label28.Size = new System.Drawing.Size(150, 16);
			this.label28.TabIndex = 0;
			this.label28.Text = "Дом, Семья:";
			this.label28.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel26
			// 
			this.panel26.Controls.Add(this.txtboxFMhumor);
			this.panel26.Controls.Add(this.label27);
			this.panel26.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel26.Location = new System.Drawing.Point(3, 341);
			this.panel26.Name = "panel26";
			this.panel26.Size = new System.Drawing.Size(589, 26);
			this.panel26.TabIndex = 14;
			// 
			// txtboxFMhumor
			// 
			this.txtboxFMhumor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMhumor.Location = new System.Drawing.Point(159, 2);
			this.txtboxFMhumor.Name = "txtboxFMhumor";
			this.txtboxFMhumor.Size = new System.Drawing.Size(424, 20);
			this.txtboxFMhumor.TabIndex = 1;
			// 
			// label27
			// 
			this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label27.Location = new System.Drawing.Point(3, 5);
			this.label27.Name = "label27";
			this.label27.Size = new System.Drawing.Size(150, 16);
			this.label27.TabIndex = 0;
			this.label27.Text = "Юмор:";
			this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel25
			// 
			this.panel25.Controls.Add(this.txtboxFMreligion);
			this.panel25.Controls.Add(this.label26);
			this.panel25.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel25.Location = new System.Drawing.Point(3, 315);
			this.panel25.Name = "panel25";
			this.panel25.Size = new System.Drawing.Size(589, 26);
			this.panel25.TabIndex = 13;
			// 
			// txtboxFMreligion
			// 
			this.txtboxFMreligion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMreligion.Location = new System.Drawing.Point(159, 2);
			this.txtboxFMreligion.Name = "txtboxFMreligion";
			this.txtboxFMreligion.Size = new System.Drawing.Size(424, 20);
			this.txtboxFMreligion.TabIndex = 1;
			// 
			// label26
			// 
			this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label26.Location = new System.Drawing.Point(3, 5);
			this.label26.Name = "label26";
			this.label26.Size = new System.Drawing.Size(150, 16);
			this.label26.TabIndex = 0;
			this.label26.Text = "Религия:";
			this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel24
			// 
			this.panel24.Controls.Add(this.txtboxFMnonfiction);
			this.panel24.Controls.Add(this.label25);
			this.panel24.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel24.Location = new System.Drawing.Point(3, 289);
			this.panel24.Name = "panel24";
			this.panel24.Size = new System.Drawing.Size(589, 26);
			this.panel24.TabIndex = 12;
			// 
			// txtboxFMnonfiction
			// 
			this.txtboxFMnonfiction.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMnonfiction.Location = new System.Drawing.Point(159, 2);
			this.txtboxFMnonfiction.Name = "txtboxFMnonfiction";
			this.txtboxFMnonfiction.Size = new System.Drawing.Size(424, 20);
			this.txtboxFMnonfiction.TabIndex = 1;
			// 
			// label25
			// 
			this.label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label25.Location = new System.Drawing.Point(3, 5);
			this.label25.Name = "label25";
			this.label25.Size = new System.Drawing.Size(150, 16);
			this.label25.TabIndex = 0;
			this.label25.Text = "Документальное:";
			this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel23
			// 
			this.panel23.Controls.Add(this.txtboxFMreference);
			this.panel23.Controls.Add(this.label24);
			this.panel23.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel23.Location = new System.Drawing.Point(3, 263);
			this.panel23.Name = "panel23";
			this.panel23.Size = new System.Drawing.Size(589, 26);
			this.panel23.TabIndex = 11;
			// 
			// txtboxFMreference
			// 
			this.txtboxFMreference.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMreference.Location = new System.Drawing.Point(159, 2);
			this.txtboxFMreference.Name = "txtboxFMreference";
			this.txtboxFMreference.Size = new System.Drawing.Size(424, 20);
			this.txtboxFMreference.TabIndex = 1;
			// 
			// label24
			// 
			this.label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label24.Location = new System.Drawing.Point(3, 5);
			this.label24.Name = "label24";
			this.label24.Size = new System.Drawing.Size(150, 16);
			this.label24.TabIndex = 0;
			this.label24.Text = "Справочники:";
			this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel22
			// 
			this.panel22.Controls.Add(this.txtboxFMcomputers);
			this.panel22.Controls.Add(this.label23);
			this.panel22.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel22.Location = new System.Drawing.Point(3, 237);
			this.panel22.Name = "panel22";
			this.panel22.Size = new System.Drawing.Size(589, 26);
			this.panel22.TabIndex = 10;
			// 
			// txtboxFMcomputers
			// 
			this.txtboxFMcomputers.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMcomputers.Location = new System.Drawing.Point(159, 2);
			this.txtboxFMcomputers.Name = "txtboxFMcomputers";
			this.txtboxFMcomputers.Size = new System.Drawing.Size(424, 20);
			this.txtboxFMcomputers.TabIndex = 1;
			// 
			// label23
			// 
			this.label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label23.Location = new System.Drawing.Point(3, 5);
			this.label23.Name = "label23";
			this.label23.Size = new System.Drawing.Size(150, 16);
			this.label23.TabIndex = 0;
			this.label23.Text = "Компьютеры:";
			this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel21
			// 
			this.panel21.Controls.Add(this.txtboxFMscience);
			this.panel21.Controls.Add(this.label22);
			this.panel21.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel21.Location = new System.Drawing.Point(3, 211);
			this.panel21.Name = "panel21";
			this.panel21.Size = new System.Drawing.Size(589, 26);
			this.panel21.TabIndex = 9;
			// 
			// txtboxFMscience
			// 
			this.txtboxFMscience.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMscience.Location = new System.Drawing.Point(159, 2);
			this.txtboxFMscience.Name = "txtboxFMscience";
			this.txtboxFMscience.Size = new System.Drawing.Size(424, 20);
			this.txtboxFMscience.TabIndex = 1;
			// 
			// label22
			// 
			this.label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label22.Location = new System.Drawing.Point(3, 5);
			this.label22.Name = "label22";
			this.label22.Size = new System.Drawing.Size(150, 16);
			this.label22.TabIndex = 0;
			this.label22.Text = "Наука, Образование:";
			this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel20
			// 
			this.panel20.Controls.Add(this.txtboxFMantique);
			this.panel20.Controls.Add(this.label21);
			this.panel20.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel20.Location = new System.Drawing.Point(3, 185);
			this.panel20.Name = "panel20";
			this.panel20.Size = new System.Drawing.Size(589, 26);
			this.panel20.TabIndex = 8;
			// 
			// txtboxFMantique
			// 
			this.txtboxFMantique.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMantique.Location = new System.Drawing.Point(159, 2);
			this.txtboxFMantique.Name = "txtboxFMantique";
			this.txtboxFMantique.Size = new System.Drawing.Size(424, 20);
			this.txtboxFMantique.TabIndex = 1;
			// 
			// label21
			// 
			this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label21.Location = new System.Drawing.Point(3, 5);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(150, 16);
			this.label21.TabIndex = 0;
			this.label21.Text = "Старинное:";
			this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel19
			// 
			this.panel19.Controls.Add(this.txtboxFMpoetry);
			this.panel19.Controls.Add(this.label20);
			this.panel19.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel19.Location = new System.Drawing.Point(3, 159);
			this.panel19.Name = "panel19";
			this.panel19.Size = new System.Drawing.Size(589, 26);
			this.panel19.TabIndex = 7;
			// 
			// txtboxFMpoetry
			// 
			this.txtboxFMpoetry.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMpoetry.Location = new System.Drawing.Point(159, 2);
			this.txtboxFMpoetry.Name = "txtboxFMpoetry";
			this.txtboxFMpoetry.Size = new System.Drawing.Size(424, 20);
			this.txtboxFMpoetry.TabIndex = 1;
			// 
			// label20
			// 
			this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label20.Location = new System.Drawing.Point(3, 5);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(150, 16);
			this.label20.TabIndex = 0;
			this.label20.Text = "Поэзия, Драматургия:";
			this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel18
			// 
			this.panel18.Controls.Add(this.txtboxFMchildren);
			this.panel18.Controls.Add(this.label19);
			this.panel18.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel18.Location = new System.Drawing.Point(3, 133);
			this.panel18.Name = "panel18";
			this.panel18.Size = new System.Drawing.Size(589, 26);
			this.panel18.TabIndex = 6;
			// 
			// txtboxFMchildren
			// 
			this.txtboxFMchildren.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMchildren.Location = new System.Drawing.Point(159, 2);
			this.txtboxFMchildren.Name = "txtboxFMchildren";
			this.txtboxFMchildren.Size = new System.Drawing.Size(424, 20);
			this.txtboxFMchildren.TabIndex = 1;
			// 
			// label19
			// 
			this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label19.Location = new System.Drawing.Point(3, 5);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(150, 16);
			this.label19.TabIndex = 0;
			this.label19.Text = "Детское:";
			this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel17
			// 
			this.panel17.Controls.Add(this.txtboxFMadventure);
			this.panel17.Controls.Add(this.label18);
			this.panel17.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel17.Location = new System.Drawing.Point(3, 107);
			this.panel17.Name = "panel17";
			this.panel17.Size = new System.Drawing.Size(589, 26);
			this.panel17.TabIndex = 5;
			// 
			// txtboxFMadventure
			// 
			this.txtboxFMadventure.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMadventure.Location = new System.Drawing.Point(159, 2);
			this.txtboxFMadventure.Name = "txtboxFMadventure";
			this.txtboxFMadventure.Size = new System.Drawing.Size(424, 20);
			this.txtboxFMadventure.TabIndex = 1;
			// 
			// label18
			// 
			this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label18.Location = new System.Drawing.Point(3, 5);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(150, 16);
			this.label18.TabIndex = 0;
			this.label18.Text = "Приключения:";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel16
			// 
			this.panel16.Controls.Add(this.txtboxFMlove);
			this.panel16.Controls.Add(this.label17);
			this.panel16.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel16.Location = new System.Drawing.Point(3, 81);
			this.panel16.Name = "panel16";
			this.panel16.Size = new System.Drawing.Size(589, 26);
			this.panel16.TabIndex = 4;
			// 
			// txtboxFMlove
			// 
			this.txtboxFMlove.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMlove.Location = new System.Drawing.Point(159, 2);
			this.txtboxFMlove.Name = "txtboxFMlove";
			this.txtboxFMlove.Size = new System.Drawing.Size(424, 20);
			this.txtboxFMlove.TabIndex = 1;
			// 
			// label17
			// 
			this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label17.Location = new System.Drawing.Point(3, 5);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(150, 16);
			this.label17.TabIndex = 0;
			this.label17.Text = "Любовные романы:";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel15
			// 
			this.panel15.Controls.Add(this.txtboxFMprose);
			this.panel15.Controls.Add(this.label16);
			this.panel15.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel15.Location = new System.Drawing.Point(3, 55);
			this.panel15.Name = "panel15";
			this.panel15.Size = new System.Drawing.Size(589, 26);
			this.panel15.TabIndex = 3;
			// 
			// txtboxFMprose
			// 
			this.txtboxFMprose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMprose.Location = new System.Drawing.Point(159, 2);
			this.txtboxFMprose.Name = "txtboxFMprose";
			this.txtboxFMprose.Size = new System.Drawing.Size(424, 20);
			this.txtboxFMprose.TabIndex = 1;
			// 
			// label16
			// 
			this.label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label16.Location = new System.Drawing.Point(3, 5);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(150, 16);
			this.label16.TabIndex = 0;
			this.label16.Text = "Проза:";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel14
			// 
			this.panel14.Controls.Add(this.txtboxFMdetective);
			this.panel14.Controls.Add(this.label15);
			this.panel14.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel14.Location = new System.Drawing.Point(3, 29);
			this.panel14.Name = "panel14";
			this.panel14.Size = new System.Drawing.Size(589, 26);
			this.panel14.TabIndex = 2;
			// 
			// txtboxFMdetective
			// 
			this.txtboxFMdetective.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMdetective.Location = new System.Drawing.Point(159, 2);
			this.txtboxFMdetective.Name = "txtboxFMdetective";
			this.txtboxFMdetective.Size = new System.Drawing.Size(424, 20);
			this.txtboxFMdetective.TabIndex = 1;
			// 
			// label15
			// 
			this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label15.Location = new System.Drawing.Point(3, 5);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(150, 16);
			this.label15.TabIndex = 0;
			this.label15.Text = "Детективы, Боевики:";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// panel13
			// 
			this.panel13.Controls.Add(this.txtboxFMsf);
			this.panel13.Controls.Add(this.label14);
			this.panel13.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel13.Location = new System.Drawing.Point(3, 3);
			this.panel13.Name = "panel13";
			this.panel13.Size = new System.Drawing.Size(589, 26);
			this.panel13.TabIndex = 1;
			// 
			// txtboxFMsf
			// 
			this.txtboxFMsf.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.txtboxFMsf.Location = new System.Drawing.Point(159, 2);
			this.txtboxFMsf.Name = "txtboxFMsf";
			this.txtboxFMsf.Size = new System.Drawing.Size(424, 20);
			this.txtboxFMsf.TabIndex = 1;
			// 
			// label14
			// 
			this.label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label14.Location = new System.Drawing.Point(3, 5);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(150, 16);
			this.label14.TabIndex = 0;
			this.label14.Text = "Фантастика, Фэнтэзи:";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// OptionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(617, 538);
			this.Controls.Add(this.tcOptions);
			this.Controls.Add(this.pBtn);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
			this.gboxRar.ResumeLayout(false);
			this.gboxRar.PerformLayout();
			this.tpValidator.ResumeLayout(false);
			this.gboxValidatorPE.ResumeLayout(false);
			this.gboxValidatorDoubleClick.ResumeLayout(false);
			this.tpFileManager.ResumeLayout(false);
			this.tcFM.ResumeLayout(false);
			this.tpFMGeneral.ResumeLayout(false);
			this.gboxFMGeneral.ResumeLayout(false);
			this.pSortFB2.ResumeLayout(false);
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
			this.tpFMNoTagsText.ResumeLayout(false);
			this.tcDesc.ResumeLayout(false);
			this.tpBookInfo.ResumeLayout(false);
			this.gBoxFMBINoTags.ResumeLayout(false);
			this.panel30.ResumeLayout(false);
			this.panel30.PerformLayout();
			this.panel29.ResumeLayout(false);
			this.panel29.PerformLayout();
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
			this.tpPublishInfo.ResumeLayout(false);
			this.gBoxFMPINoTags.ResumeLayout(false);
			this.panel33.ResumeLayout(false);
			this.panel33.PerformLayout();
			this.panel32.ResumeLayout(false);
			this.panel32.PerformLayout();
			this.panel31.ResumeLayout(false);
			this.panel31.PerformLayout();
			this.tpFB2Info.ResumeLayout(false);
			this.gBoxFMFB2INoTags.ResumeLayout(false);
			this.panel34.ResumeLayout(false);
			this.panel34.PerformLayout();
			this.panel35.ResumeLayout(false);
			this.panel35.PerformLayout();
			this.panel36.ResumeLayout(false);
			this.panel36.PerformLayout();
			this.panel37.ResumeLayout(false);
			this.panel37.PerformLayout();
			this.tpFMGenreGroups.ResumeLayout(false);
			this.panel28.ResumeLayout(false);
			this.panel28.PerformLayout();
			this.panel27.ResumeLayout(false);
			this.panel27.PerformLayout();
			this.panel26.ResumeLayout(false);
			this.panel26.PerformLayout();
			this.panel25.ResumeLayout(false);
			this.panel25.PerformLayout();
			this.panel24.ResumeLayout(false);
			this.panel24.PerformLayout();
			this.panel23.ResumeLayout(false);
			this.panel23.PerformLayout();
			this.panel22.ResumeLayout(false);
			this.panel22.PerformLayout();
			this.panel21.ResumeLayout(false);
			this.panel21.PerformLayout();
			this.panel20.ResumeLayout(false);
			this.panel20.PerformLayout();
			this.panel19.ResumeLayout(false);
			this.panel19.PerformLayout();
			this.panel18.ResumeLayout(false);
			this.panel18.PerformLayout();
			this.panel17.ResumeLayout(false);
			this.panel17.PerformLayout();
			this.panel16.ResumeLayout(false);
			this.panel16.PerformLayout();
			this.panel15.ResumeLayout(false);
			this.panel15.PerformLayout();
			this.panel14.ResumeLayout(false);
			this.panel14.PerformLayout();
			this.panel13.ResumeLayout(false);
			this.panel13.PerformLayout();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label label38;
		private System.Windows.Forms.TextBox txtBoxFMNoFB2FirstName;
		private System.Windows.Forms.Panel panel37;
		private System.Windows.Forms.Label label37;
		private System.Windows.Forms.TextBox txtBoxFMNoFB2MiddleName;
		private System.Windows.Forms.Panel panel36;
		private System.Windows.Forms.Label label36;
		private System.Windows.Forms.TextBox txtBoxFMNoFB2LastName;
		private System.Windows.Forms.Panel panel35;
		private System.Windows.Forms.Label label35;
		private System.Windows.Forms.TextBox txtBoxFMNoFB2NickName;
		private System.Windows.Forms.Panel panel34;
		private System.Windows.Forms.Label label34;
		private System.Windows.Forms.TextBox txtBoxFMNoCity;
		private System.Windows.Forms.Panel panel33;
		private System.Windows.Forms.Label label33;
		private System.Windows.Forms.TextBox txtBoxFMNoPublisher;
		private System.Windows.Forms.Panel panel32;
		private System.Windows.Forms.Label label32;
		private System.Windows.Forms.TextBox txtBoxFMNoYear;
		private System.Windows.Forms.Panel panel31;
		private System.Windows.Forms.Label label30;
		private System.Windows.Forms.TextBox txtBoxFMNoDateText;
		private System.Windows.Forms.Panel panel29;
		private System.Windows.Forms.Label label31;
		private System.Windows.Forms.TextBox txtBoxFMNoDateValue;
		private System.Windows.Forms.Panel panel30;
		private System.Windows.Forms.GroupBox gBoxFMBINoTags;
		private System.Windows.Forms.GroupBox gBoxFMFB2INoTags;
		private System.Windows.Forms.GroupBox gBoxFMPINoTags;
		private System.Windows.Forms.TabPage tpFB2Info;
		private System.Windows.Forms.TabPage tpPublishInfo;
		private System.Windows.Forms.TabPage tpBookInfo;
		private System.Windows.Forms.TabControl tcDesc;
		private System.Windows.Forms.CheckBox chBoxConfirmationForExit;
		private System.Windows.Forms.Label lblFB2Dup;
		private System.Windows.Forms.ComboBox cboxDSFB2Dup;
		private System.Windows.Forms.ComboBox cboxTIRFB2Dup;
		private System.Windows.Forms.ComboBox cboxTIRArchiveManager;
		private System.Windows.Forms.Label lblArchiveManager;
		private System.Windows.Forms.ComboBox cboxDSArchiveManager;
		private System.Windows.Forms.Label lblFileManager;
		private System.Windows.Forms.ComboBox cboxDSFileManager;
		private System.Windows.Forms.ComboBox cboxTIRFileManager;
		private System.Windows.Forms.ComboBox cboxTIRValidator;
		private System.Windows.Forms.ComboBox cboxDSValidator;
		private System.Windows.Forms.Label lblValidator;
		private System.Windows.Forms.GroupBox gboxButtons;
		private System.Windows.Forms.Button btnDiffPath;
		private System.Windows.Forms.TextBox tboxDiffPath;
		private System.Windows.Forms.Label lblDiffPath;
		private System.Windows.Forms.GroupBox gboxDiff;
		private System.Windows.Forms.RadioButton rbtnAsSentence;
		private System.Windows.Forms.TextBox txtboxFMhome;
		private System.Windows.Forms.TextBox txtboxFMbusiness;
		private System.Windows.Forms.TextBox txtboxFMlove;
		private System.Windows.Forms.TextBox txtboxFMadventure;
		private System.Windows.Forms.TextBox txtboxFMchildren;
		private System.Windows.Forms.TextBox txtboxFMpoetry;
		private System.Windows.Forms.TextBox txtboxFMantique;
		private System.Windows.Forms.TextBox txtboxFMscience;
		private System.Windows.Forms.TextBox txtboxFMcomputers;
		private System.Windows.Forms.TextBox txtboxFMreference;
		private System.Windows.Forms.TextBox txtboxFMnonfiction;
		private System.Windows.Forms.TextBox txtboxFMreligion;
		private System.Windows.Forms.TextBox txtboxFMhumor;
		private System.Windows.Forms.TextBox txtboxFMprose;
		private System.Windows.Forms.TextBox txtboxFMdetective;
		private System.Windows.Forms.Label label28;
		private System.Windows.Forms.Panel panel27;
		private System.Windows.Forms.Label label29;
		private System.Windows.Forms.Panel panel28;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Panel panel16;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Panel panel17;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Panel panel18;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Panel panel19;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.Panel panel20;
		private System.Windows.Forms.Label label22;
		private System.Windows.Forms.Panel panel21;
		private System.Windows.Forms.Label label23;
		private System.Windows.Forms.Panel panel22;
		private System.Windows.Forms.Label label24;
		private System.Windows.Forms.Panel panel23;
		private System.Windows.Forms.Label label25;
		private System.Windows.Forms.Panel panel24;
		private System.Windows.Forms.Label label26;
		private System.Windows.Forms.Panel panel25;
		private System.Windows.Forms.Label label27;
		private System.Windows.Forms.Panel panel26;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Panel panel14;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Panel panel15;
		private System.Windows.Forms.TextBox txtboxFMsf;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Panel panel13;
		private System.Windows.Forms.TabPage tpFMGenreGroups;
		private System.Windows.Forms.Button btnDefRestore;
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
		private System.Windows.Forms.RadioButton rbtnFMFB21;
		private System.Windows.Forms.RadioButton rbtnFMFB22;
		private System.Windows.Forms.Label lblFMGenres;
		private System.Windows.Forms.Panel pFMGenres;
		private System.Windows.Forms.TabPage tpFMGeneral;
		private System.Windows.Forms.TabControl tcFM;
		private System.Windows.Forms.FolderBrowserDialog fbdDir;
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
		private System.Windows.Forms.ComboBox cboxSpace;
		private System.Windows.Forms.ComboBox cboxFileExist;
		private System.Windows.Forms.Label lbFilelExist;
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
