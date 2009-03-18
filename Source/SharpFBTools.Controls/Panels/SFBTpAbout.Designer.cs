/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 16.03.2009
 * Time: 10:04
 * 
 * License: GPL 2.1
 */
namespace SharpFBTools.Controls.Panels
{
	partial class SFBTpAbout
	{
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
			this.tpLicense = new System.Windows.Forms.TabPage();
			this.rtboxLicense = new System.Windows.Forms.RichTextBox();
			this.tpHelp = new System.Windows.Forms.TabPage();
			this.pHelp = new System.Windows.Forms.Panel();
			this.rtboxHelp = new System.Windows.Forms.RichTextBox();
			this.pMode = new System.Windows.Forms.Panel();
			this.lblType = new System.Windows.Forms.Label();
			this.cboxInstrument = new System.Windows.Forms.ComboBox();
			this.tcAbout.SuspendLayout();
			this.tpLicense.SuspendLayout();
			this.tpHelp.SuspendLayout();
			this.pHelp.SuspendLayout();
			this.pMode.SuspendLayout();
			this.SuspendLayout();
			// 
			// tcAbout
			// 
			this.tcAbout.Controls.Add(this.tpAbout);
			this.tcAbout.Controls.Add(this.tpLicense);
			this.tcAbout.Controls.Add(this.tpHelp);
			this.tcAbout.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcAbout.Location = new System.Drawing.Point(0, 0);
			this.tcAbout.Name = "tcAbout";
			this.tcAbout.SelectedIndex = 0;
			this.tcAbout.Size = new System.Drawing.Size(711, 555);
			this.tcAbout.TabIndex = 4;
			// 
			// tpAbout
			// 
			this.tpAbout.Location = new System.Drawing.Point(4, 22);
			this.tpAbout.Name = "tpAbout";
			this.tpAbout.Padding = new System.Windows.Forms.Padding(3);
			this.tpAbout.Size = new System.Drawing.Size(703, 529);
			this.tpAbout.TabIndex = 0;
			this.tpAbout.Text = "О программе";
			this.tpAbout.UseVisualStyleBackColor = true;
			// 
			// tpLicense
			// 
			this.tpLicense.Controls.Add(this.rtboxLicense);
			this.tpLicense.Location = new System.Drawing.Point(4, 22);
			this.tpLicense.Name = "tpLicense";
			this.tpLicense.Size = new System.Drawing.Size(703, 529);
			this.tpLicense.TabIndex = 2;
			this.tpLicense.Text = "Лицензия";
			this.tpLicense.UseVisualStyleBackColor = true;
			// 
			// rtboxLicense
			// 
			this.rtboxLicense.BackColor = System.Drawing.SystemColors.Window;
			this.rtboxLicense.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtboxLicense.Location = new System.Drawing.Point(0, 0);
			this.rtboxLicense.Name = "rtboxLicense";
			this.rtboxLicense.ReadOnly = true;
			this.rtboxLicense.Size = new System.Drawing.Size(703, 529);
			this.rtboxLicense.TabIndex = 0;
			this.rtboxLicense.Text = "";
			// 
			// tpHelp
			// 
			this.tpHelp.Controls.Add(this.pHelp);
			this.tpHelp.Controls.Add(this.pMode);
			this.tpHelp.Location = new System.Drawing.Point(4, 22);
			this.tpHelp.Name = "tpHelp";
			this.tpHelp.Padding = new System.Windows.Forms.Padding(3);
			this.tpHelp.Size = new System.Drawing.Size(703, 529);
			this.tpHelp.TabIndex = 1;
			this.tpHelp.Text = "Справка";
			this.tpHelp.UseVisualStyleBackColor = true;
			// 
			// pHelp
			// 
			this.pHelp.Controls.Add(this.rtboxHelp);
			this.pHelp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pHelp.Location = new System.Drawing.Point(3, 32);
			this.pHelp.Name = "pHelp";
			this.pHelp.Size = new System.Drawing.Size(697, 494);
			this.pHelp.TabIndex = 3;
			// 
			// rtboxHelp
			// 
			this.rtboxHelp.BackColor = System.Drawing.SystemColors.Window;
			this.rtboxHelp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtboxHelp.ForeColor = System.Drawing.SystemColors.WindowText;
			this.rtboxHelp.Location = new System.Drawing.Point(0, 0);
			this.rtboxHelp.Name = "rtboxHelp";
			this.rtboxHelp.ReadOnly = true;
			this.rtboxHelp.Size = new System.Drawing.Size(697, 494);
			this.rtboxHelp.TabIndex = 0;
			this.rtboxHelp.Text = "";
			// 
			// pMode
			// 
			this.pMode.Controls.Add(this.lblType);
			this.pMode.Controls.Add(this.cboxInstrument);
			this.pMode.Dock = System.Windows.Forms.DockStyle.Top;
			this.pMode.Location = new System.Drawing.Point(3, 3);
			this.pMode.Name = "pMode";
			this.pMode.Size = new System.Drawing.Size(697, 29);
			this.pMode.TabIndex = 2;
			// 
			// lblType
			// 
			this.lblType.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.lblType.ForeColor = System.Drawing.Color.Navy;
			this.lblType.Location = new System.Drawing.Point(3, 5);
			this.lblType.Name = "lblType";
			this.lblType.Size = new System.Drawing.Size(86, 18);
			this.lblType.TabIndex = 2;
			this.lblType.Text = "Инструмент:";
			// 
			// cboxInstrument
			// 
			this.cboxInstrument.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboxInstrument.FormattingEnabled = true;
			this.cboxInstrument.Items.AddRange(new object[] {
									"Валидатор fb2-файлов",
									"Менеджер файлов",
									"Менеджер архивов",
									"FB2 Корректор",
									"Дубликатор файлов"});
			this.cboxInstrument.Location = new System.Drawing.Point(89, 3);
			this.cboxInstrument.Name = "cboxInstrument";
			this.cboxInstrument.Size = new System.Drawing.Size(608, 21);
			this.cboxInstrument.TabIndex = 1;
			// 
			// SFBTpAbout
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tcAbout);
			this.Name = "SFBTpAbout";
			this.Size = new System.Drawing.Size(711, 555);
			this.tcAbout.ResumeLayout(false);
			this.tpLicense.ResumeLayout(false);
			this.tpHelp.ResumeLayout(false);
			this.pHelp.ResumeLayout(false);
			this.pMode.ResumeLayout(false);
			this.ResumeLayout(false);
		}
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
	}
}
