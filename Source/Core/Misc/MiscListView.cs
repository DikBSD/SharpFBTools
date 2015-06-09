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
using System.Windows.Forms;

namespace Core.Misc
{
	/// <summary>
	/// Работа с ListView
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
		public void CheckAllListViewItems( ListView lv, bool bCheck ) {
			if( lv.Items.Count > 0  ) {
				for( int i=0; i!=lv.Items.Count; ++i ) {
					lv.Items[i].Checked = bCheck;
				}
			}
		}
		// снять отметки с помеченных итемов
		public void UnCheckAllListViewItems( System.Windows.Forms.ListView.CheckedListViewItemCollection checkedItems ) {
			foreach( ListViewItem lvi in checkedItems ) {
				lvi.Checked = false;
			}
		}
		
		// пометить/снять пометку все выделенные элементы
		public void ChekAllSelectedItems(ListView lv, bool bCheck) {
			System.Windows.Forms.ListView.SelectedListViewItemCollection selectedItems = lv.SelectedItems;
			foreach( ListViewItem lvi in selectedItems ) {
				lvi.Checked = bCheck;
			}
		}
		
		// пометить/снять отметки с  итемов в выбранной группе
		public void CheckAllListViewItemsInGroup( ListViewGroup Group, bool bCheck ) {
			foreach( ListViewItem lvi in Group.Items ) {
				lvi.Checked = bCheck;
			}
		}
		
		// пометить все файлы определенного типа
		public void CheckTypeAllFiles(ListView lv, string sType, bool bCheck) {
			if( lv.Items.Count > 0  ) {
				DirectoryInfo di = null;
				for( int i=0; i!=lv.Items.Count; ++i ) {
					ListViewItemType it = (ListViewItemType)lv.Items[i].Tag;
					if(it.Type == "f") {
						di = new DirectoryInfo(it.Value);
						if(di.Extension.ToLower()=="."+sType.ToLower()) {
							lv.Items[i].Checked = bCheck;
						}
					}
				}
			}
		}
		
		// снять пометку со всех файлов пределенного типа
		public void UnCheckTypeAllFiles(ListView lv, string sType) {
			DirectoryInfo di = null;
			foreach( ListViewItem lvi in lv.CheckedItems ) {
				ListViewItemType it = (ListViewItemType)lvi.Tag;
				if(it.Type == "f") {
					di = new DirectoryInfo(it.Value);
					if(di.Extension.ToLower()=="."+sType.ToLower()) {
						lvi.Checked = false;
					}
				}
			}
		}
		
		// пометить все файлы
		public void CheckAllFiles(ListView lv, bool bCheck) {
			if( lv.Items.Count > 0  ) {
				for( int i=0; i!=lv.Items.Count; ++i ) {
					ListViewItemType it = (ListViewItemType)lv.Items[i].Tag;
					if(it.Type == "f") {
						lv.Items[i].Checked = bCheck;
					}
				}
			}
		}
		
		// снять пометку со всех файлов
		public void UnCheckAllFiles(ListView lv) {
			foreach( ListViewItem lvi in lv.CheckedItems ) {
				ListViewItemType it = (ListViewItemType)lvi.Tag;
				if(it.Type == "f") {
					lvi.Checked = false;
				}
			}
		}
	
		// отметить все папки
		public void CheckAllDirs(ListView lv, bool bCheck) {
			if( lv.Items.Count > 0  ) {
				for( int i=0; i!=lv.Items.Count; ++i ) {
					ListViewItemType it = (ListViewItemType)lv.Items[i].Tag;
					if(it.Type == "d") {
						lv.Items[i].Checked = bCheck;
					}
				}
			}
		}
		
		// снять пометку со всех папок
		public void UnCheckAllDirs(ListView lv) {
			foreach( ListViewItem lvi in lv.CheckedItems ) {
				ListViewItemType it = (ListViewItemType)lvi.Tag;
				if(it.Type == "d") {
					lvi.Checked = false;
				}
			}
		}
		
		#endregion

		#region Разное
		// авторазмер колонок Списка ListView
		public void AutoResizeColumns(ListView listView) {
			listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
//			for(int i=0; i!=listView.Columns.Count; ++i)
//				listView.Columns[i].Width = listView.Columns[i].Width + 2;
		}
		// переход на указанный итем
		public void SelectedItemEnsureVisible(ListView listView, int Index) {
			listView.Select();
			listView.Items[Index].Selected = true;
			listView.EnsureVisible(Index);
		}
		#endregion
	}
}
