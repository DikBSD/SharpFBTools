/*
 * Сделано в SharpDevelop.
 * Пользователь: VadimK
 * Дата: 19.02.2016
 * Время: 14:38
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
namespace Core.Corrector
{
	partial class FB2NotValidateForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FB2NotValidateForm));
			this.ProgressPanel = new System.Windows.Forms.Panel();
			this.ControlPanel = new System.Windows.Forms.Panel();
			this.btnStop = new System.Windows.Forms.Button();
			this.btnSaveToXml = new System.Windows.Forms.Button();
			this.StatusLabel = new System.Windows.Forms.Label();
			this.ProgressBar = new System.Windows.Forms.ProgressBar();
			this.sfdList = new System.Windows.Forms.SaveFileDialog();
			this.ProgressPanel.SuspendLayout();
			this.ControlPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// ProgressPanel
			// 
			this.ProgressPanel.Controls.Add(this.ControlPanel);
			this.ProgressPanel.Controls.Add(this.StatusLabel);
			this.ProgressPanel.Controls.Add(this.ProgressBar);
			this.ProgressPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ProgressPanel.Location = new System.Drawing.Point(0, 0);
			this.ProgressPanel.Margin = new System.Windows.Forms.Padding(4);
			this.ProgressPanel.Name = "ProgressPanel";
			this.ProgressPanel.Size = new System.Drawing.Size(892, 255);
			this.ProgressPanel.TabIndex = 2;
			// 
			// ControlPanel
			// 
			this.ControlPanel.BackColor = System.Drawing.Color.DarkGray;
			this.ControlPanel.Controls.Add(this.btnStop);
			this.ControlPanel.Controls.Add(this.btnSaveToXml);
			this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.ControlPanel.Location = new System.Drawing.Point(684, 0);
			this.ControlPanel.Margin = new System.Windows.Forms.Padding(4);
			this.ControlPanel.Name = "ControlPanel";
			this.ControlPanel.Size = new System.Drawing.Size(208, 255);
			this.ControlPanel.TabIndex = 2;
			// 
			// btnStop
			// 
			this.btnStop.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
			this.btnStop.Location = new System.Drawing.Point(0, 71);
			this.btnStop.Margin = new System.Windows.Forms.Padding(4);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(208, 71);
			this.btnStop.TabIndex = 9;
			this.btnStop.Text = "Прервать";
			this.btnStop.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.BtnStopClick);
			// 
			// btnSaveToXml
			// 
			this.btnSaveToXml.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnSaveToXml.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnSaveToXml.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveToXml.Image")));
			this.btnSaveToXml.Location = new System.Drawing.Point(0, 0);
			this.btnSaveToXml.Margin = new System.Windows.Forms.Padding(4);
			this.btnSaveToXml.Name = "btnSaveToXml";
			this.btnSaveToXml.Size = new System.Drawing.Size(208, 71);
			this.btnSaveToXml.TabIndex = 8;
			this.btnSaveToXml.Text = "Прервать в файл...";
			this.btnSaveToXml.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnSaveToXml.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnSaveToXml.UseVisualStyleBackColor = true;
			this.btnSaveToXml.Click += new System.EventHandler(this.BtnSaveToXmlClick);
			// 
			// StatusLabel
			// 
			this.StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.StatusLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.StatusLabel.Location = new System.Drawing.Point(15, 52);
			this.StatusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(655, 192);
			this.StatusLabel.TabIndex = 1;
			// 
			// ProgressBar
			// 
			this.ProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.ProgressBar.Location = new System.Drawing.Point(16, 16);
			this.ProgressBar.Margin = new System.Windows.Forms.Padding(4);
			this.ProgressBar.Name = "ProgressBar";
			this.ProgressBar.Size = new System.Drawing.Size(653, 28);
			this.ProgressBar.TabIndex = 0;
			// 
			// sfdList
			// 
			this.sfdList.RestoreDirectory = true;
			this.sfdList.Title = "Укажите название файла невалидных книг";
			// 
			// FB2NotValidateForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(892, 255);
			this.ControlBox = false;
			this.Controls.Add(this.ProgressPanel);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(1200, 600);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(910, 300);
			this.Name = "FB2NotValidateForm";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Поиск всех невалидных файлов в заданной папке";
			this.ProgressPanel.ResumeLayout(false);
			this.ControlPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		private System.Windows.Forms.SaveFileDialog sfdList;
		private System.Windows.Forms.Button btnSaveToXml;
		private System.Windows.Forms.Panel ProgressPanel;
		private System.Windows.Forms.Label StatusLabel;
		private System.Windows.Forms.ProgressBar ProgressBar;
		private System.Windows.Forms.Panel ControlPanel;
		private System.Windows.Forms.Button btnStop;
	}
}
