/*
 * Создано в SharpDevelop.
 * Пользователь: Вадим Кузнецов (DikBSD)
 * Дата: 06.07.2016
 * Время: 8:26
 * 
 * License: GPL 2.1
 */
using System;
using System.Drawing;
using System.Windows.Forms;

using EndWorkMode = Core.Common.EndWorkMode;

namespace Core.Sorter
{
	/// <summary>
	/// Вывод данных о сортировке
	/// </summary>
	public partial class SortResultForm : Form
	{
		public SortResultForm( string TitleSortType, string endWorkMode, StatusView statusView )
		{
			InitializeComponent();
			// Отображение результата сортировки
			sortingProgressData( TitleSortType, endWorkMode, statusView );
		}
	
		// Отображнгтн результата сортировки
		private void sortingProgressData( string TitleSortType, string endWorkMode, StatusView statusView ) {
			this.Text += " ( " + TitleSortType + " )";
			labelInfo.Text = endWorkMode;
			
			lvFilesCount.Items[0].SubItems[1].Text = Convert.ToString( statusView.AllDirs );
			lvFilesCount.Items[1].SubItems[1].Text = Convert.ToString( statusView.AllFiles );
			lvFilesCount.Items[2].SubItems[1].Text = Convert.ToString( statusView.SourceFB2 );
			lvFilesCount.Items[3].SubItems[1].Text = Convert.ToString( statusView.Zip );
			lvFilesCount.Items[4].SubItems[1].Text = Convert.ToString( statusView.FB2FromZips );
			lvFilesCount.Items[5].SubItems[1].Text = Convert.ToString( statusView.Other );
			lvFilesCount.Items[6].SubItems[1].Text = Convert.ToString( statusView.CreateInTarget );
			lvFilesCount.Items[7].SubItems[1].Text = Convert.ToString( statusView.NotRead );
			lvFilesCount.Items[8].SubItems[1].Text = Convert.ToString( statusView.NotValidFB2 );
			lvFilesCount.Items[9].SubItems[1].Text = Convert.ToString( statusView.BadZip );
			lvFilesCount.Items[10].SubItems[1].Text = Convert.ToString( statusView.LongPath );
			lvFilesCount.Items[11].SubItems[1].Text = Convert.ToString( statusView.NotSort );
			lvFilesCount.AutoResizeColumns( ColumnHeaderAutoResizeStyle.HeaderSize );
		}
	}
}
