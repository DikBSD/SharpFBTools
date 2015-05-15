/*
 * Сделано в SharpDevelop.
 * Пользователь: VadimK
 * Дата: 01.09.2014
 * Время: 10:37
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
namespace Core.Duplicator
{
	partial class CheckedBooksPathToXMLForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CheckedBooksPathToXMLForm));
			this.ControlPanel = new System.Windows.Forms.Panel();
			this.btnStop = new System.Windows.Forms.Button();
			this.ProgressBar = new System.Windows.Forms.ProgressBar();
			this.ControlPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// ControlPanel
			// 
			this.ControlPanel.Controls.Add(this.btnStop);
			this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.ControlPanel.Location = new System.Drawing.Point(672, 0);
			this.ControlPanel.Margin = new System.Windows.Forms.Padding(4);
			this.ControlPanel.Name = "ControlPanel";
			this.ControlPanel.Size = new System.Drawing.Size(208, 72);
			this.ControlPanel.TabIndex = 7;
			// 
			// btnStop
			// 
			this.btnStop.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
			this.btnStop.Location = new System.Drawing.Point(0, 0);
			this.btnStop.Margin = new System.Windows.Forms.Padding(4);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(208, 71);
			this.btnStop.TabIndex = 1;
			this.btnStop.Text = "Прервать";
			this.btnStop.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.BtnStopClick);
			// 
			// ProgressBar
			// 
			this.ProgressBar.Location = new System.Drawing.Point(13, 17);
			this.ProgressBar.Margin = new System.Windows.Forms.Padding(4);
			this.ProgressBar.Name = "ProgressBar";
			this.ProgressBar.Size = new System.Drawing.Size(642, 41);
			this.ProgressBar.TabIndex = 6;
			// 
			// CheckedBooksPathToXMLForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(880, 72);
			this.ControlBox = false;
			this.Controls.Add(this.ControlPanel);
			this.Controls.Add(this.ProgressBar);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(898, 117);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(898, 117);
			this.Name = "CheckedBooksPathToXMLForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "CheckedBooksPathToXMLForm";
			this.ControlPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		private System.Windows.Forms.Panel ControlPanel;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.ProgressBar ProgressBar;
	}
}
