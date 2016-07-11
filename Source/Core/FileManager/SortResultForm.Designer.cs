/*
 * Создано в SharpDevelop.
 * Пользователь: VadimK
 * Дата: 06.07.2016
 * Время: 8:26
 * 
 * Для изменения этого шаблона используйте меню "Инструменты | Параметры | Кодирование | Стандартные заголовки".
 */
namespace Core.FileManager
{
	partial class SortResultForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.ListView lvFilesCount;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.Panel panelInfo;
		private System.Windows.Forms.Label labelInfo;
		
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
			System.Windows.Forms.ListViewItem listViewItem13 = new System.Windows.Forms.ListViewItem(new string[] {
			"Всего папок",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem14 = new System.Windows.Forms.ListViewItem(new string[] {
			"Всего файлов",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem15 = new System.Windows.Forms.ListViewItem(new string[] {
			"Исходные fb2-файлы",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem16 = new System.Windows.Forms.ListViewItem(new string[] {
			"Исходные  Zip-архивы с fb2",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem17 = new System.Windows.Forms.ListViewItem(new string[] {
			"Исходные fb2-файлы из архивов",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem18 = new System.Windows.Forms.ListViewItem(new string[] {
			"Другие файлы",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem19 = new System.Windows.Forms.ListViewItem(new string[] {
			"Создано в папке-приемнике",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem20 = new System.Windows.Forms.ListViewItem(new string[] {
			"Нечитаемые fb2-файлы (архивы)",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem21 = new System.Windows.Forms.ListViewItem(new string[] {
			"Не валидные fb2-файлы (при вкл. опции)",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem22 = new System.Windows.Forms.ListViewItem(new string[] {
			"Битые архивы (не открылись)",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem23 = new System.Windows.Forms.ListViewItem(new string[] {
			"Длинный путь к создаваемому файлу",
			"0"}, 0);
			System.Windows.Forms.ListViewItem listViewItem24 = new System.Windows.Forms.ListViewItem(new string[] {
			"Не удовлетворяющие условиям сортировки",
			"0"}, 0);
			this.panelInfo = new System.Windows.Forms.Panel();
			this.labelInfo = new System.Windows.Forms.Label();
			this.lvFilesCount = new System.Windows.Forms.ListView();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.panelInfo.SuspendLayout();
			this.SuspendLayout();
			// 
			// panelInfo
			// 
			this.panelInfo.Controls.Add(this.labelInfo);
			this.panelInfo.Dock = System.Windows.Forms.DockStyle.Top;
			this.panelInfo.Location = new System.Drawing.Point(0, 0);
			this.panelInfo.Name = "panelInfo";
			this.panelInfo.Size = new System.Drawing.Size(485, 48);
			this.panelInfo.TabIndex = 29;
			// 
			// labelInfo
			// 
			this.labelInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.labelInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
			this.labelInfo.Location = new System.Drawing.Point(0, 0);
			this.labelInfo.Name = "labelInfo";
			this.labelInfo.Size = new System.Drawing.Size(485, 48);
			this.labelInfo.TabIndex = 0;
			this.labelInfo.Text = "labelInfo";
			this.labelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lvFilesCount
			// 
			this.lvFilesCount.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
			this.columnHeader6,
			this.columnHeader7});
			this.lvFilesCount.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvFilesCount.FullRowSelect = true;
			this.lvFilesCount.GridLines = true;
			this.lvFilesCount.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
			listViewItem13,
			listViewItem14,
			listViewItem15,
			listViewItem16,
			listViewItem17,
			listViewItem18,
			listViewItem19,
			listViewItem20,
			listViewItem21,
			listViewItem22,
			listViewItem23,
			listViewItem24});
			this.lvFilesCount.Location = new System.Drawing.Point(0, 48);
			this.lvFilesCount.Margin = new System.Windows.Forms.Padding(4);
			this.lvFilesCount.Name = "lvFilesCount";
			this.lvFilesCount.Size = new System.Drawing.Size(485, 285);
			this.lvFilesCount.TabIndex = 30;
			this.lvFilesCount.UseCompatibleStateImageBehavior = false;
			this.lvFilesCount.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "Папки и файлы";
			this.columnHeader6.Width = 327;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "Количество";
			this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.columnHeader7.Width = 116;
			// 
			// SortResultForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(485, 333);
			this.Controls.Add(this.lvFilesCount);
			this.Controls.Add(this.panelInfo);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "SortResultForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Данные сортировки";
			this.panelInfo.ResumeLayout(false);
			this.ResumeLayout(false);

		}
	}
}
