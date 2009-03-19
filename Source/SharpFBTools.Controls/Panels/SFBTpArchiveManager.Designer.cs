/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 16.03.2009
 * Time: 9:55
 * 
 * License: GPL 2.1
 */
namespace SharpFBTools.Controls.Panels
{
	partial class SFBTpArchiveManager
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SFBTpArchiveManager));
			this.tsValidator = new System.Windows.Forms.ToolStrip();
			this.tsbtnOpenDir = new System.Windows.Forms.ToolStripButton();
			this.tsSep1 = new System.Windows.Forms.ToolStripSeparator();
			this.ssProgress = new System.Windows.Forms.StatusStrip();
			this.tsslblProgress = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
			this.pCount = new System.Windows.Forms.Panel();
			this.tlpCount = new System.Windows.Forms.TableLayoutPanel();
			this.lblDirs = new System.Windows.Forms.Label();
			this.lblDirsCount = new System.Windows.Forms.Label();
			this.lblFiles = new System.Windows.Forms.Label();
			this.lblFilesCount = new System.Windows.Forms.Label();
			this.lblFB2Files = new System.Windows.Forms.Label();
			this.lblFB2FilesCount = new System.Windows.Forms.Label();
			this.lblFB2ZipFiles = new System.Windows.Forms.Label();
			this.lblFB2ZipFilesCount = new System.Windows.Forms.Label();
			this.lblFB2RarFiles = new System.Windows.Forms.Label();
			this.lblFB2RarFilesCount = new System.Windows.Forms.Label();
			this.pScanDir = new System.Windows.Forms.Panel();
			this.tboxSourceDir = new System.Windows.Forms.TextBox();
			this.lblDir = new System.Windows.Forms.Label();
			this.pCentral = new System.Windows.Forms.Panel();
			this.tcArchiver = new System.Windows.Forms.TabControl();
			this.tpArchive = new System.Windows.Forms.TabPage();
			this.tpUnArchive = new System.Windows.Forms.TabPage();
			this.tpTest = new System.Windows.Forms.TabPage();
			this.tsValidator.SuspendLayout();
			this.ssProgress.SuspendLayout();
			this.pCount.SuspendLayout();
			this.tlpCount.SuspendLayout();
			this.pScanDir.SuspendLayout();
			this.pCentral.SuspendLayout();
			this.tcArchiver.SuspendLayout();
			this.SuspendLayout();
			// 
			// tsValidator
			// 
			this.tsValidator.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.tsValidator.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsbtnOpenDir,
									this.tsSep1});
			this.tsValidator.Location = new System.Drawing.Point(0, 0);
			this.tsValidator.Name = "tsValidator";
			this.tsValidator.Size = new System.Drawing.Size(727, 31);
			this.tsValidator.TabIndex = 2;
			// 
			// tsbtnOpenDir
			// 
			this.tsbtnOpenDir.Image = ((System.Drawing.Image)(resources.GetObject("tsbtnOpenDir.Image")));
			this.tsbtnOpenDir.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbtnOpenDir.Name = "tsbtnOpenDir";
			this.tsbtnOpenDir.Size = new System.Drawing.Size(114, 28);
			this.tsbtnOpenDir.Text = "Открыть папку";
			this.tsbtnOpenDir.ToolTipText = "Открыть папку с fb2, fb2.zip, fb2.rar, zip или rar файлами...";
			// 
			// tsSep1
			// 
			this.tsSep1.Name = "tsSep1";
			this.tsSep1.Size = new System.Drawing.Size(6, 31);
			// 
			// ssProgress
			// 
			this.ssProgress.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
									this.tsslblProgress,
									this.tsProgressBar});
			this.ssProgress.Location = new System.Drawing.Point(0, 527);
			this.ssProgress.Name = "ssProgress";
			this.ssProgress.Size = new System.Drawing.Size(727, 22);
			this.ssProgress.TabIndex = 18;
			this.ssProgress.Text = "statusStrip1";
			// 
			// tsslblProgress
			// 
			this.tsslblProgress.Name = "tsslblProgress";
			this.tsslblProgress.Size = new System.Drawing.Size(47, 17);
			this.tsslblProgress.Text = "Готово.";
			// 
			// tsProgressBar
			// 
			this.tsProgressBar.Name = "tsProgressBar";
			this.tsProgressBar.Size = new System.Drawing.Size(400, 16);
			// 
			// pCount
			// 
			this.pCount.AutoSize = true;
			this.pCount.Controls.Add(this.tlpCount);
			this.pCount.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.pCount.Location = new System.Drawing.Point(0, 514);
			this.pCount.Margin = new System.Windows.Forms.Padding(0);
			this.pCount.Name = "pCount";
			this.pCount.Size = new System.Drawing.Size(727, 13);
			this.pCount.TabIndex = 21;
			// 
			// tlpCount
			// 
			this.tlpCount.AutoSize = true;
			this.tlpCount.ColumnCount = 10;
			this.tlpCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.tlpCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlpCount.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlpCount.Controls.Add(this.lblDirs, 0, 0);
			this.tlpCount.Controls.Add(this.lblDirsCount, 1, 0);
			this.tlpCount.Controls.Add(this.lblFiles, 2, 0);
			this.tlpCount.Controls.Add(this.lblFilesCount, 3, 0);
			this.tlpCount.Controls.Add(this.lblFB2Files, 4, 0);
			this.tlpCount.Controls.Add(this.lblFB2FilesCount, 5, 0);
			this.tlpCount.Controls.Add(this.lblFB2ZipFiles, 6, 0);
			this.tlpCount.Controls.Add(this.lblFB2ZipFilesCount, 7, 0);
			this.tlpCount.Controls.Add(this.lblFB2RarFiles, 8, 0);
			this.tlpCount.Controls.Add(this.lblFB2RarFilesCount, 9, 0);
			this.tlpCount.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpCount.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
			this.tlpCount.Location = new System.Drawing.Point(0, 0);
			this.tlpCount.Margin = new System.Windows.Forms.Padding(0);
			this.tlpCount.Name = "tlpCount";
			this.tlpCount.RowCount = 1;
			this.tlpCount.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpCount.Size = new System.Drawing.Size(727, 13);
			this.tlpCount.TabIndex = 15;
			// 
			// lblDirs
			// 
			this.lblDirs.AutoSize = true;
			this.lblDirs.Location = new System.Drawing.Point(3, 0);
			this.lblDirs.Name = "lblDirs";
			this.lblDirs.Size = new System.Drawing.Size(81, 13);
			this.lblDirs.TabIndex = 0;
			this.lblDirs.Text = "Всего папок:";
			// 
			// lblDirsCount
			// 
			this.lblDirsCount.AutoSize = true;
			this.lblDirsCount.Location = new System.Drawing.Point(90, 0);
			this.lblDirsCount.Name = "lblDirsCount";
			this.lblDirsCount.Size = new System.Drawing.Size(14, 13);
			this.lblDirsCount.TabIndex = 1;
			this.lblDirsCount.Text = "0";
			// 
			// lblFiles
			// 
			this.lblFiles.AutoSize = true;
			this.lblFiles.Location = new System.Drawing.Point(110, 0);
			this.lblFiles.Name = "lblFiles";
			this.lblFiles.Size = new System.Drawing.Size(89, 13);
			this.lblFiles.TabIndex = 2;
			this.lblFiles.Text = "Всего файлов:";
			// 
			// lblFilesCount
			// 
			this.lblFilesCount.AutoSize = true;
			this.lblFilesCount.Location = new System.Drawing.Point(205, 0);
			this.lblFilesCount.Name = "lblFilesCount";
			this.lblFilesCount.Size = new System.Drawing.Size(14, 13);
			this.lblFilesCount.TabIndex = 3;
			this.lblFilesCount.Text = "0";
			// 
			// lblFB2Files
			// 
			this.lblFB2Files.AutoSize = true;
			this.lblFB2Files.Location = new System.Drawing.Point(225, 0);
			this.lblFB2Files.Name = "lblFB2Files";
			this.lblFB2Files.Size = new System.Drawing.Size(79, 13);
			this.lblFB2Files.TabIndex = 4;
			this.lblFB2Files.Text = ".fb2-файлов:";
			// 
			// lblFB2FilesCount
			// 
			this.lblFB2FilesCount.AutoSize = true;
			this.lblFB2FilesCount.Location = new System.Drawing.Point(310, 0);
			this.lblFB2FilesCount.Name = "lblFB2FilesCount";
			this.lblFB2FilesCount.Size = new System.Drawing.Size(14, 13);
			this.lblFB2FilesCount.TabIndex = 5;
			this.lblFB2FilesCount.Text = "0";
			// 
			// lblFB2ZipFiles
			// 
			this.lblFB2ZipFiles.AutoSize = true;
			this.lblFB2ZipFiles.Location = new System.Drawing.Point(330, 0);
			this.lblFB2ZipFiles.Name = "lblFB2ZipFiles";
			this.lblFB2ZipFiles.Size = new System.Drawing.Size(98, 13);
			this.lblFB2ZipFiles.TabIndex = 6;
			this.lblFB2ZipFiles.Text = ".zip-файлов fb2:";
			// 
			// lblFB2ZipFilesCount
			// 
			this.lblFB2ZipFilesCount.AutoSize = true;
			this.lblFB2ZipFilesCount.Location = new System.Drawing.Point(434, 0);
			this.lblFB2ZipFilesCount.Name = "lblFB2ZipFilesCount";
			this.lblFB2ZipFilesCount.Size = new System.Drawing.Size(14, 13);
			this.lblFB2ZipFilesCount.TabIndex = 7;
			this.lblFB2ZipFilesCount.Text = "0";
			// 
			// lblFB2RarFiles
			// 
			this.lblFB2RarFiles.AutoSize = true;
			this.lblFB2RarFiles.Location = new System.Drawing.Point(454, 0);
			this.lblFB2RarFiles.Name = "lblFB2RarFiles";
			this.lblFB2RarFiles.Size = new System.Drawing.Size(99, 13);
			this.lblFB2RarFiles.TabIndex = 8;
			this.lblFB2RarFiles.Text = ".rar-файлов fb2:";
			// 
			// lblFB2RarFilesCount
			// 
			this.lblFB2RarFilesCount.AutoSize = true;
			this.lblFB2RarFilesCount.Location = new System.Drawing.Point(559, 0);
			this.lblFB2RarFilesCount.Name = "lblFB2RarFilesCount";
			this.lblFB2RarFilesCount.Size = new System.Drawing.Size(14, 13);
			this.lblFB2RarFilesCount.TabIndex = 9;
			this.lblFB2RarFilesCount.Text = "0";
			// 
			// pScanDir
			// 
			this.pScanDir.AutoSize = true;
			this.pScanDir.Controls.Add(this.tboxSourceDir);
			this.pScanDir.Controls.Add(this.lblDir);
			this.pScanDir.Dock = System.Windows.Forms.DockStyle.Top;
			this.pScanDir.Location = new System.Drawing.Point(0, 31);
			this.pScanDir.Margin = new System.Windows.Forms.Padding(0);
			this.pScanDir.Name = "pScanDir";
			this.pScanDir.Size = new System.Drawing.Size(727, 28);
			this.pScanDir.TabIndex = 22;
			// 
			// tboxSourceDir
			// 
			this.tboxSourceDir.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.tboxSourceDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.tboxSourceDir.Location = new System.Drawing.Point(162, 5);
			this.tboxSourceDir.Name = "tboxSourceDir";
			this.tboxSourceDir.ReadOnly = true;
			this.tboxSourceDir.Size = new System.Drawing.Size(562, 20);
			this.tboxSourceDir.TabIndex = 4;
			// 
			// lblDir
			// 
			this.lblDir.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.World);
			this.lblDir.Location = new System.Drawing.Point(2, 8);
			this.lblDir.Name = "lblDir";
			this.lblDir.Size = new System.Drawing.Size(162, 19);
			this.lblDir.TabIndex = 3;
			this.lblDir.Text = "Папка для сканирования:";
			// 
			// pCentral
			// 
			this.pCentral.Controls.Add(this.tcArchiver);
			this.pCentral.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pCentral.Location = new System.Drawing.Point(0, 59);
			this.pCentral.Name = "pCentral";
			this.pCentral.Size = new System.Drawing.Size(727, 455);
			this.pCentral.TabIndex = 23;
			// 
			// tcArchiver
			// 
			this.tcArchiver.Controls.Add(this.tpArchive);
			this.tcArchiver.Controls.Add(this.tpTest);
			this.tcArchiver.Controls.Add(this.tpUnArchive);
			this.tcArchiver.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tcArchiver.Location = new System.Drawing.Point(0, 0);
			this.tcArchiver.Name = "tcArchiver";
			this.tcArchiver.SelectedIndex = 0;
			this.tcArchiver.Size = new System.Drawing.Size(727, 455);
			this.tcArchiver.TabIndex = 0;
			// 
			// tpArchive
			// 
			this.tpArchive.Location = new System.Drawing.Point(4, 22);
			this.tpArchive.Name = "tpArchive";
			this.tpArchive.Padding = new System.Windows.Forms.Padding(3);
			this.tpArchive.Size = new System.Drawing.Size(719, 429);
			this.tpArchive.TabIndex = 0;
			this.tpArchive.Text = "Архивировать";
			this.tpArchive.UseVisualStyleBackColor = true;
			// 
			// tpUnArchive
			// 
			this.tpUnArchive.Location = new System.Drawing.Point(4, 22);
			this.tpUnArchive.Name = "tpUnArchive";
			this.tpUnArchive.Padding = new System.Windows.Forms.Padding(3);
			this.tpUnArchive.Size = new System.Drawing.Size(719, 429);
			this.tpUnArchive.TabIndex = 1;
			this.tpUnArchive.Text = "Разархивировать";
			this.tpUnArchive.UseVisualStyleBackColor = true;
			// 
			// tpTest
			// 
			this.tpTest.Location = new System.Drawing.Point(4, 22);
			this.tpTest.Name = "tpTest";
			this.tpTest.Size = new System.Drawing.Size(719, 429);
			this.tpTest.TabIndex = 2;
			this.tpTest.Text = "Тестирование архивов";
			this.tpTest.UseVisualStyleBackColor = true;
			// 
			// SFBTpArchiveManager
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.pCentral);
			this.Controls.Add(this.pScanDir);
			this.Controls.Add(this.pCount);
			this.Controls.Add(this.ssProgress);
			this.Controls.Add(this.tsValidator);
			this.Name = "SFBTpArchiveManager";
			this.Size = new System.Drawing.Size(727, 549);
			this.tsValidator.ResumeLayout(false);
			this.tsValidator.PerformLayout();
			this.ssProgress.ResumeLayout(false);
			this.ssProgress.PerformLayout();
			this.pCount.ResumeLayout(false);
			this.pCount.PerformLayout();
			this.tlpCount.ResumeLayout(false);
			this.tlpCount.PerformLayout();
			this.pScanDir.ResumeLayout(false);
			this.pScanDir.PerformLayout();
			this.pCentral.ResumeLayout(false);
			this.tcArchiver.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.TabPage tpTest;
		private System.Windows.Forms.TabPage tpUnArchive;
		private System.Windows.Forms.TabPage tpArchive;
		private System.Windows.Forms.TabControl tcArchiver;
		private System.Windows.Forms.Panel pCentral;
		private System.Windows.Forms.Label lblDir;
		private System.Windows.Forms.TextBox tboxSourceDir;
		private System.Windows.Forms.Panel pScanDir;
		private System.Windows.Forms.Label lblFB2RarFilesCount;
		private System.Windows.Forms.Label lblFB2RarFiles;
		private System.Windows.Forms.Label lblFB2ZipFilesCount;
		private System.Windows.Forms.Label lblFB2ZipFiles;
		private System.Windows.Forms.Label lblFB2FilesCount;
		private System.Windows.Forms.Label lblFB2Files;
		private System.Windows.Forms.Label lblFilesCount;
		private System.Windows.Forms.Label lblFiles;
		private System.Windows.Forms.Label lblDirsCount;
		private System.Windows.Forms.Label lblDirs;
		private System.Windows.Forms.TableLayoutPanel tlpCount;
		private System.Windows.Forms.Panel pCount;
		private System.Windows.Forms.ToolStripProgressBar tsProgressBar;
		private System.Windows.Forms.ToolStripStatusLabel tsslblProgress;
		private System.Windows.Forms.StatusStrip ssProgress;
		private System.Windows.Forms.ToolStripSeparator tsSep1;
		private System.Windows.Forms.ToolStripButton tsbtnOpenDir;
		private System.Windows.Forms.ToolStrip tsValidator;
	}
}
