/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 07.09.2009
 * Time: 13:25
 * 
 * License: GPL 2.1
 */
using System;
using System.Windows.Forms;


namespace Core.Misc
{
	/// <summary>
	/// Description of Misc.
	/// </summary>
	public class Misc
	{
		public Misc()
		{
		}
		
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
	}
}
