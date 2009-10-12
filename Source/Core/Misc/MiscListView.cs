/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 07.09.2009
 * Time: 13:25
 * 
 * License: GPL 2.1
 */
using System;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;

using filesWorker		= Core.FilesWorker.FilesWorker;
using archivesWorker	= Core.FilesWorker.Archiver;
using stringProcessing	= Core.StringProcessing.StringProcessing;

namespace Core.Misc
{
	/// <summary>
	/// Description of Misc.
	/// </summary>
	public class MiscListView
	{
		public MiscListView()
		{
		}
		
		#region Работа с отдельными итемами ListView
		// увеличение значения 2-й колонки ListView на 1
		public void IncListViewStatus( ListView lv, int nItem ) {
			lv.Items[nItem].SubItems[1].Text =
				Convert.ToString( 1+Convert.ToInt32( lv.Items[nItem].SubItems[1].Text ) );
		}
		
		// занесение в нужный item определеного значения
		public void ListViewStatus( ListView lv, int nItem, int nValue ) {
			lv.Items[nItem].SubItems[1].Text = Convert.ToString( nValue );
		}
		
		// занесение в нужный item определеного значения
		public void ListViewStatus( ListView lv, int nItem, string sValue ) {
			lv.Items[nItem].SubItems[1].Text = sValue;
		}
		#endregion
		
		#region Пометить / Снять отметки
		// отметить все итемы (снять все отметки)
		public void CheckdAllListViewItems( ListView lv, bool bCheck ) {
			if( lv.Items.Count > 0  ) {
				for( int i=0; i!=lv.Items.Count; ++i ) {
					lv.Items[i].Checked = bCheck;
				}
			}
		}
		// снять отметки с помеченных итемов
		public void UnCheckdAllListViewItems( System.Windows.Forms.ListView.CheckedListViewItemCollection checkedItems) {
			foreach( ListViewItem lvi in checkedItems ) {
				lvi.Checked = false;
			}
		}
		#endregion

	}
}
