﻿/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 16.03.2009
 * Time: 10:04
 * 
 * License: GPL 2.1
 */

using System;
using System.IO;
using System.Windows.Forms;

using SharpFBTools.AssemblyInfo;

namespace SharpFBTools.Tools
{
	/// <summary>
	/// Description of SFBTpAbout.
	/// </summary>
	public partial class SFBTpAbout : UserControl
	{
		#region Designer
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the control.
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
            this.tcAbout = new System.Windows.Forms.TabControl();
            this.tpAbout = new System.Windows.Forms.TabPage();
            this.lblAsIs1 = new System.Windows.Forms.Label();
            this.lblAsIs = new System.Windows.Forms.Label();
            this.lblWeb = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.lblDeveloper = new System.Windows.Forms.Label();
            this.lblLicense = new System.Windows.Forms.Label();
            this.lblAbout = new System.Windows.Forms.Label();
            this.lblSharpFBTools = new System.Windows.Forms.Label();
            this.tpLog = new System.Windows.Forms.TabPage();
            this.rtboxLog = new System.Windows.Forms.RichTextBox();
            this.tpLicense = new System.Windows.Forms.TabPage();
            this.rtboxLicense = new System.Windows.Forms.RichTextBox();
            this.tpHelp = new System.Windows.Forms.TabPage();
            this.pHelp = new System.Windows.Forms.Panel();
            this.rtboxHelp = new System.Windows.Forms.RichTextBox();
            this.pMode = new System.Windows.Forms.Panel();
            this.lblType = new System.Windows.Forms.Label();
            this.cboxInstrument = new System.Windows.Forms.ComboBox();
            this.tcAbout.SuspendLayout();
            this.tpAbout.SuspendLayout();
            this.tpLog.SuspendLayout();
            this.tpLicense.SuspendLayout();
            this.tpHelp.SuspendLayout();
            this.pHelp.SuspendLayout();
            this.pMode.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcAbout
            // 
            this.tcAbout.Controls.Add(this.tpAbout);
            this.tcAbout.Controls.Add(this.tpLog);
            this.tcAbout.Controls.Add(this.tpLicense);
            this.tcAbout.Controls.Add(this.tpHelp);
            this.tcAbout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcAbout.Location = new System.Drawing.Point(0, 0);
            this.tcAbout.Margin = new System.Windows.Forms.Padding(4);
            this.tcAbout.Name = "tcAbout";
            this.tcAbout.SelectedIndex = 0;
            this.tcAbout.Size = new System.Drawing.Size(948, 683);
            this.tcAbout.TabIndex = 0;
            // 
            // tpAbout
            // 
            this.tpAbout.Controls.Add(this.lblAsIs1);
            this.tpAbout.Controls.Add(this.lblAsIs);
            this.tpAbout.Controls.Add(this.lblWeb);
            this.tpAbout.Controls.Add(this.label1);
            this.tpAbout.Controls.Add(this.lblCopyright);
            this.tpAbout.Controls.Add(this.lblDeveloper);
            this.tpAbout.Controls.Add(this.lblLicense);
            this.tpAbout.Controls.Add(this.lblAbout);
            this.tpAbout.Controls.Add(this.lblSharpFBTools);
            this.tpAbout.Location = new System.Drawing.Point(4, 25);
            this.tpAbout.Margin = new System.Windows.Forms.Padding(4);
            this.tpAbout.Name = "tpAbout";
            this.tpAbout.Padding = new System.Windows.Forms.Padding(4);
            this.tpAbout.Size = new System.Drawing.Size(940, 654);
            this.tpAbout.TabIndex = 0;
            this.tpAbout.Text = " О программе ";
            this.tpAbout.UseVisualStyleBackColor = true;
            // 
            // lblAsIs1
            // 
            this.lblAsIs1.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAsIs1.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblAsIs1.Location = new System.Drawing.Point(4, 298);
            this.lblAsIs1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAsIs1.Name = "lblAsIs1";
            this.lblAsIs1.Size = new System.Drawing.Size(932, 50);
            this.lblAsIs1.TabIndex = 15;
            this.lblAsIs1.Text = "Разработчик программы не несет никакой ответственности за возможные негативные ре" +
    "зультаты работы программы. Делайте копии файлов.";
            this.lblAsIs1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAsIs
            // 
            this.lblAsIs.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAsIs.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblAsIs.Location = new System.Drawing.Point(4, 270);
            this.lblAsIs.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAsIs.Name = "lblAsIs";
            this.lblAsIs.Size = new System.Drawing.Size(932, 28);
            this.lblAsIs.TabIndex = 14;
            this.lblAsIs.Text = "Соглашение:";
            this.lblAsIs.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblWeb
            // 
            this.lblWeb.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblWeb.Font = new System.Drawing.Font("Tahoma", 12F);
            this.lblWeb.ForeColor = System.Drawing.Color.DarkMagenta;
            this.lblWeb.Location = new System.Drawing.Point(4, 190);
            this.lblWeb.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWeb.Name = "lblWeb";
            this.lblWeb.Size = new System.Drawing.Size(932, 80);
            this.lblWeb.TabIndex = 13;
            this.lblWeb.Text = "Сайт программы:\r\nhttps://sourceforge.net/projects/sharpfbtools\r\nhttps://github.co" +
    "m/DikBSD/SharpFBTools";
            this.lblWeb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Tahoma", 11F);
            this.label1.ForeColor = System.Drawing.Color.Sienna;
            this.label1.Location = new System.Drawing.Point(4, 162);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(932, 28);
            this.label1.TabIndex = 12;
            this.label1.Text = "Пожелания и предложения по изменению приветствуются и принимаются!";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCopyright
            // 
            this.lblCopyright.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCopyright.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblCopyright.Location = new System.Drawing.Point(4, 139);
            this.lblCopyright.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(932, 23);
            this.lblCopyright.TabIndex = 2;
            this.lblCopyright.Text = "Copyright (c) 2009-2024";
            this.lblCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblDeveloper
            // 
            this.lblDeveloper.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDeveloper.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.lblDeveloper.Location = new System.Drawing.Point(4, 101);
            this.lblDeveloper.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDeveloper.Name = "lblDeveloper";
            this.lblDeveloper.Size = new System.Drawing.Size(932, 38);
            this.lblDeveloper.TabIndex = 3;
            this.lblDeveloper.Text = "Разработчик: Вадим Кузнецов (DikBSD)";
            this.lblDeveloper.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLicense
            // 
            this.lblLicense.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLicense.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblLicense.Location = new System.Drawing.Point(4, 73);
            this.lblLicense.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblLicense.Name = "lblLicense";
            this.lblLicense.Size = new System.Drawing.Size(932, 28);
            this.lblLicense.TabIndex = 4;
            this.lblLicense.Text = "Лицензия: LGPL 2.1";
            this.lblLicense.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblAbout
            // 
            this.lblAbout.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblAbout.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            this.lblAbout.ForeColor = System.Drawing.Color.Navy;
            this.lblAbout.Location = new System.Drawing.Point(4, 45);
            this.lblAbout.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblAbout.Name = "lblAbout";
            this.lblAbout.Size = new System.Drawing.Size(932, 28);
            this.lblAbout.TabIndex = 1;
            this.lblAbout.Text = "Open Source набор инструментов по работе с fb2-файлами в пакетном режиме";
            this.lblAbout.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSharpFBTools
            // 
            this.lblSharpFBTools.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSharpFBTools.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.lblSharpFBTools.ForeColor = System.Drawing.Color.Red;
            this.lblSharpFBTools.Location = new System.Drawing.Point(4, 4);
            this.lblSharpFBTools.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSharpFBTools.Name = "lblSharpFBTools";
            this.lblSharpFBTools.Size = new System.Drawing.Size(932, 41);
            this.lblSharpFBTools.TabIndex = 0;
            this.lblSharpFBTools.Text = "SharpFBTools";
            this.lblSharpFBTools.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tpLog
            // 
            this.tpLog.Controls.Add(this.rtboxLog);
            this.tpLog.Location = new System.Drawing.Point(4, 25);
            this.tpLog.Margin = new System.Windows.Forms.Padding(4);
            this.tpLog.Name = "tpLog";
            this.tpLog.Size = new System.Drawing.Size(940, 654);
            this.tpLog.TabIndex = 3;
            this.tpLog.Text = " История развития ";
            this.tpLog.UseVisualStyleBackColor = true;
            // 
            // rtboxLog
            // 
            this.rtboxLog.BackColor = System.Drawing.SystemColors.Window;
            this.rtboxLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtboxLog.ForeColor = System.Drawing.SystemColors.WindowText;
            this.rtboxLog.Location = new System.Drawing.Point(0, 0);
            this.rtboxLog.Margin = new System.Windows.Forms.Padding(4);
            this.rtboxLog.Name = "rtboxLog";
            this.rtboxLog.ReadOnly = true;
            this.rtboxLog.Size = new System.Drawing.Size(940, 654);
            this.rtboxLog.TabIndex = 0;
            this.rtboxLog.Text = "";
            // 
            // tpLicense
            // 
            this.tpLicense.Controls.Add(this.rtboxLicense);
            this.tpLicense.Location = new System.Drawing.Point(4, 25);
            this.tpLicense.Margin = new System.Windows.Forms.Padding(4);
            this.tpLicense.Name = "tpLicense";
            this.tpLicense.Size = new System.Drawing.Size(940, 654);
            this.tpLicense.TabIndex = 2;
            this.tpLicense.Text = " Лицензия ";
            this.tpLicense.UseVisualStyleBackColor = true;
            // 
            // rtboxLicense
            // 
            this.rtboxLicense.BackColor = System.Drawing.SystemColors.Window;
            this.rtboxLicense.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtboxLicense.Location = new System.Drawing.Point(0, 0);
            this.rtboxLicense.Margin = new System.Windows.Forms.Padding(4);
            this.rtboxLicense.Name = "rtboxLicense";
            this.rtboxLicense.ReadOnly = true;
            this.rtboxLicense.Size = new System.Drawing.Size(940, 654);
            this.rtboxLicense.TabIndex = 0;
            this.rtboxLicense.Text = "";
            // 
            // tpHelp
            // 
            this.tpHelp.Controls.Add(this.pHelp);
            this.tpHelp.Controls.Add(this.pMode);
            this.tpHelp.Location = new System.Drawing.Point(4, 25);
            this.tpHelp.Margin = new System.Windows.Forms.Padding(4);
            this.tpHelp.Name = "tpHelp";
            this.tpHelp.Padding = new System.Windows.Forms.Padding(4);
            this.tpHelp.Size = new System.Drawing.Size(940, 654);
            this.tpHelp.TabIndex = 1;
            this.tpHelp.Text = " Справка ";
            this.tpHelp.UseVisualStyleBackColor = true;
            // 
            // pHelp
            // 
            this.pHelp.Controls.Add(this.rtboxHelp);
            this.pHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pHelp.Location = new System.Drawing.Point(4, 40);
            this.pHelp.Margin = new System.Windows.Forms.Padding(4);
            this.pHelp.Name = "pHelp";
            this.pHelp.Size = new System.Drawing.Size(932, 610);
            this.pHelp.TabIndex = 3;
            // 
            // rtboxHelp
            // 
            this.rtboxHelp.BackColor = System.Drawing.SystemColors.Window;
            this.rtboxHelp.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtboxHelp.ForeColor = System.Drawing.SystemColors.WindowText;
            this.rtboxHelp.Location = new System.Drawing.Point(0, 0);
            this.rtboxHelp.Margin = new System.Windows.Forms.Padding(4);
            this.rtboxHelp.Name = "rtboxHelp";
            this.rtboxHelp.ReadOnly = true;
            this.rtboxHelp.Size = new System.Drawing.Size(932, 610);
            this.rtboxHelp.TabIndex = 0;
            this.rtboxHelp.Text = "";
            // 
            // pMode
            // 
            this.pMode.Controls.Add(this.lblType);
            this.pMode.Controls.Add(this.cboxInstrument);
            this.pMode.Dock = System.Windows.Forms.DockStyle.Top;
            this.pMode.Location = new System.Drawing.Point(4, 4);
            this.pMode.Margin = new System.Windows.Forms.Padding(4);
            this.pMode.Name = "pMode";
            this.pMode.Size = new System.Drawing.Size(932, 36);
            this.pMode.TabIndex = 2;
            // 
            // lblType
            // 
            this.lblType.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
            this.lblType.ForeColor = System.Drawing.Color.Navy;
            this.lblType.Location = new System.Drawing.Point(4, 6);
            this.lblType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(115, 22);
            this.lblType.TabIndex = 2;
            this.lblType.Text = "Инструмент:";
            // 
            // cboxInstrument
            // 
            this.cboxInstrument.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboxInstrument.FormattingEnabled = true;
            this.cboxInstrument.Items.AddRange(new object[] {
            "Сортировщик файлов",
            "Менеджер архивов",
            "Дубликатор файлов"});
            this.cboxInstrument.Location = new System.Drawing.Point(119, 4);
            this.cboxInstrument.Margin = new System.Windows.Forms.Padding(4);
            this.cboxInstrument.Name = "cboxInstrument";
            this.cboxInstrument.Size = new System.Drawing.Size(809, 24);
            this.cboxInstrument.TabIndex = 1;
            this.cboxInstrument.SelectedIndexChanged += new System.EventHandler(this.CboxInstrumentSelectedIndexChanged);
            // 
            // SFBTpAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tcAbout);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SFBTpAbout";
            this.Size = new System.Drawing.Size(948, 683);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.SFBTpAboutLayout);
            this.tcAbout.ResumeLayout(false);
            this.tpAbout.ResumeLayout(false);
            this.tpLog.ResumeLayout(false);
            this.tpLicense.ResumeLayout(false);
            this.tpHelp.ResumeLayout(false);
            this.pHelp.ResumeLayout(false);
            this.pMode.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		private System.Windows.Forms.Label lblAsIs1;
		private System.Windows.Forms.Label lblAsIs;
		private System.Windows.Forms.Label lblWeb;
		private System.Windows.Forms.RichTextBox rtboxLog;
		private System.Windows.Forms.TabPage tpLog;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label lblLicense;
		private System.Windows.Forms.Label lblCopyright;
		private System.Windows.Forms.Label lblDeveloper;
		private System.Windows.Forms.Label lblSharpFBTools;
		private System.Windows.Forms.Label lblAbout;
		private System.Windows.Forms.Panel pMode;
		private System.Windows.Forms.Panel pHelp;
		private System.Windows.Forms.ComboBox cboxInstrument;
		private System.Windows.Forms.Label lblType;
		private System.Windows.Forms.RichTextBox rtboxHelp;
		private System.Windows.Forms.RichTextBox rtboxLicense;
		private System.Windows.Forms.TabPage tpLicense;
		private System.Windows.Forms.TabPage tpHelp;
		private System.Windows.Forms.TabPage tpAbout;
		private System.Windows.Forms.TabControl tcAbout;
		#endregion
		
		public SFBTpAbout()
		{
			InitializeComponent();
			// загрузка файла Лицензии
			string sLicensePath = Settings.Settings.LicensePath;
			if( File.Exists( sLicensePath ) ) {
				rtboxLicense.LoadFile( sLicensePath );
			} else {
				rtboxLicense.Text = "Не найден файл лицензии: \""+sLicensePath+"\"";
			}
			string sChangeFilePath = Settings.Settings.ChangeFilePath;
			if( File.Exists( sChangeFilePath ) ) {
				rtboxLog.LoadFile( sChangeFilePath );
			} else {
				rtboxLog.Text = "Не найден файл истории развития программы: \""+sChangeFilePath+"\"";
			}
			// справка
			cboxInstrument.SelectedIndex = 0;
		}
		
		void CboxInstrumentSelectedIndexChanged(object sender, EventArgs e)
		{
			rtboxHelp.Clear();
			string sFileManagerHelpPath		= Settings.SorterSettings.SorterHelpPath;
			string sArchiveManagerHelpPath	= Settings.ArchiveManagerSettings.GetArchiveManagerHelpPath();
			string sDuplicatorHelpPath		= Settings.FB2DublicatorSettings.GetDuplicatorHelpPath();
			switch( cboxInstrument.SelectedIndex ) {
				case 0:
					if( File.Exists( sFileManagerHelpPath ) ) {
						rtboxHelp.LoadFile( sFileManagerHelpPath );
					} else {
						rtboxHelp.Text = "Не найден файл Справки Сортировщика Файлов: \""+sFileManagerHelpPath+"\"";
					}
					break;
				case 1:
					if( File.Exists( sArchiveManagerHelpPath ) ) {
						rtboxHelp.LoadFile( sArchiveManagerHelpPath );
					} else {
						rtboxHelp.Text = "Не найден файл Справки Менеджера Архивов: \""+sArchiveManagerHelpPath+"\"";
					}
					break;
				case 2:
					if( File.Exists( sDuplicatorHelpPath ) ) {
						rtboxHelp.LoadFile( sDuplicatorHelpPath );
					} else {
						rtboxHelp.Text = "Не найден файл Справки Дубликатора fb2-файлов: \""+sDuplicatorHelpPath+"\"";
					}
					break;
			}
		}
		
		void SFBTpAboutLayout(object sender, LayoutEventArgs e)
		{
			// Данные о программе
			lblSharpFBTools.Text = SharpFBTools_AssemblyInfo.GetAssemblyTitle()+"-"+
										SharpFBTools_AssemblyInfo.GetAssemblyVersion();
			lblAbout.Text = SharpFBTools_AssemblyInfo.GetAssemblyDescription();
			lblDeveloper.Text = "Разработчик: "+SharpFBTools_AssemblyInfo.GetAssemblyCompany();
			lblCopyright.Text = SharpFBTools_AssemblyInfo.GetAssemblyCopyright();
		}

	}
}
