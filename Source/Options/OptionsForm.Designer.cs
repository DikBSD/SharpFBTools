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
			this.tpValidator = new System.Windows.Forms.TabPage();
			this.pBtn.SuspendLayout();
			this.tcOptions.SuspendLayout();
			this.tpGeneral.SuspendLayout();
			this.gboxEditors.SuspendLayout();
			this.gboxRar.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.Location = new System.Drawing.Point(418, 3);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
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
			this.btnCancel.Location = new System.Drawing.Point(524, 3);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Отмена";
			this.btnCancel.UseVisualStyleBackColor = true;
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
			this.tcOptions.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcOptions.Location = new System.Drawing.Point(0, 0);
			this.tcOptions.Name = "tcOptions";
			this.tcOptions.SelectedIndex = 0;
			this.tcOptions.Size = new System.Drawing.Size(617, 443);
			this.tcOptions.TabIndex = 3;
			// 
			// tpGeneral
			// 
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
			this.gboxEditors.Location = new System.Drawing.Point(3, 59);
			this.gboxEditors.Name = "gboxEditors";
			this.gboxEditors.Size = new System.Drawing.Size(603, 78);
			this.gboxEditors.TabIndex = 14;
			this.gboxEditors.TabStop = false;
			this.gboxEditors.Text = " Fb2-Редакторы ";
			// 
			// lblTextEPath
			// 
			this.lblTextEPath.AutoSize = true;
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
			this.gboxRar.Dock = System.Windows.Forms.DockStyle.Top;
			this.gboxRar.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.gboxRar.Location = new System.Drawing.Point(3, 3);
			this.gboxRar.Name = "gboxRar";
			this.gboxRar.Size = new System.Drawing.Size(603, 56);
			this.gboxRar.TabIndex = 13;
			this.gboxRar.TabStop = false;
			this.gboxRar.Text = " Настройки для Rar-архиватора ";
			// 
			// lblRarPath
			// 
			this.lblRarPath.AutoSize = true;
			this.lblRarPath.Location = new System.Drawing.Point(7, 25);
			this.lblRarPath.Name = "lblRarPath";
			this.lblRarPath.Size = new System.Drawing.Size(93, 13);
			this.lblRarPath.TabIndex = 10;
			this.lblRarPath.Text = "Путь к WinRar:";
			// 
			// tboxRarPath
			// 
			this.tboxRarPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxRarPath.Location = new System.Drawing.Point(171, 21);
			this.tboxRarPath.Name = "tboxRarPath";
			this.tboxRarPath.ReadOnly = true;
			this.tboxRarPath.Size = new System.Drawing.Size(378, 20);
			this.tboxRarPath.TabIndex = 8;
			// 
			// btnRarPath
			// 
			this.btnRarPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnRarPath.Image = ((System.Drawing.Image)(resources.GetObject("btnRarPath.Image")));
			this.btnRarPath.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnRarPath.Location = new System.Drawing.Point(555, 19);
			this.btnRarPath.Name = "btnRarPath";
			this.btnRarPath.Size = new System.Drawing.Size(37, 24);
			this.btnRarPath.TabIndex = 9;
			this.btnRarPath.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.btnRarPath.UseVisualStyleBackColor = true;
			this.btnRarPath.Click += new System.EventHandler(this.BtnRarPathClick);
			// 
			// tpValidator
			// 
			this.tpValidator.Location = new System.Drawing.Point(4, 22);
			this.tpValidator.Name = "tpValidator";
			this.tpValidator.Padding = new System.Windows.Forms.Padding(3);
			this.tpValidator.Size = new System.Drawing.Size(609, 417);
			this.tpValidator.TabIndex = 1;
			this.tpValidator.Text = " Валидатор ";
			this.tpValidator.UseVisualStyleBackColor = true;
			// 
			// OptionsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(617, 472);
			this.Controls.Add(this.tcOptions);
			this.Controls.Add(this.pBtn);
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
			this.gboxEditors.ResumeLayout(false);
			this.gboxEditors.PerformLayout();
			this.gboxRar.ResumeLayout(false);
			this.gboxRar.PerformLayout();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button btnTextEPath;
		private System.Windows.Forms.TextBox tboxTextEPath;
		private System.Windows.Forms.Label lblTextEPath;
		private System.Windows.Forms.Button btnFBEPath;
		private System.Windows.Forms.TextBox tboxFBEPath;
		private System.Windows.Forms.Label lblFBEPath;
		private System.Windows.Forms.GroupBox gboxEditors;
		private System.Windows.Forms.TextBox tboxRarPath;
		private System.Windows.Forms.Button btnRarPath;
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
