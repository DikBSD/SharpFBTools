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
using System.Collections.Generic;

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
		public static void IncListViewStatus( ListView lv, int nItem ) {
			lv.Items[nItem].SubItems[1].Text =
				Convert.ToString( 1+Convert.ToInt32( lv.Items[nItem].SubItems[1].Text ) );
		}
		
		// занесение в нужный item определеного значения
		public static void ListViewStatus( ListView lv, int nItem, int nValue ) {
			lv.Items[nItem].SubItems[1].Text = Convert.ToString( nValue );
		}
		
		// занесение в нужный item определеного значения
		public static void ListViewStatus( ListView lv, int nItem, string sValue ) {
			lv.Items[nItem].SubItems[1].Text = sValue;
		}
		#endregion
		
		#region Пометить / Снять отметки
		// отметить все итемы (снять все отметки)
		public static void CheckAllListViewItems( ListView lv, bool bCheck ) {
			if( lv.Items.Count > 0  ) {
				for( int i=0; i!=lv.Items.Count; ++i ) {
					lv.Items[i].Checked = bCheck;
				}
			}
		}
		// снять отметки с помеченных итемов
		public static void UnCheckAllListViewItems( System.Windows.Forms.ListView.CheckedListViewItemCollection checkedItems ) {
			foreach( ListViewItem lvi in checkedItems ) {
				lvi.Checked = false;
			}
		}
		
		// пометить/снять пометку все выделенные элементы
		public static void ChekAllSelectedItems(ListView lv, bool bCheck) {
			System.Windows.Forms.ListView.SelectedListViewItemCollection selectedItems = lv.SelectedItems;
			foreach( ListViewItem lvi in selectedItems ) {
				lvi.Checked = bCheck;
			}
		}
		
		// пометить/снять отметки с  итемов в выбранной группе
		public static void CheckAllListViewItemsInGroup( ListViewGroup Group, bool bCheck ) {
			foreach( ListViewItem lvi in Group.Items ) {
				lvi.Checked = bCheck;
			}
		}
		
		// пометить все файлы определенного типа
		public static void CheckTypeAllFiles(ListView lv, string sType, bool bCheck) {
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
		public static void UnCheckTypeAllFiles(ListView lv, string sType) {
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
		public static void CheckAllFiles(ListView lv, bool bCheck) {
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
		public static void UnCheckAllFiles(ListView lv) {
			foreach( ListViewItem lvi in lv.CheckedItems ) {
				ListViewItemType it = (ListViewItemType)lvi.Tag;
				if(it.Type == "f") {
					lvi.Checked = false;
				}
			}
		}
		
		// отметить все папки
		public static void CheckAllDirs(ListView lv, bool bCheck) {
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
		public static void UnCheckAllDirs(ListView lv) {
			foreach( ListViewItem lvi in lv.CheckedItems ) {
				ListViewItemType it = (ListViewItemType)lvi.Tag;
				if(it.Type == "d") {
					lvi.Checked = false;
				}
			}
		}
		
		#endregion

		#region Добавление, удаление, перемещение итемов, проверка на наличие итема в списке...
		// есть ли в списке итем с текстом CompareItemText
		public static bool isExistListViewItem( ListView lv, string CompareItemText ) {
			ListView.ListViewItemCollection lvicol = lv.Items;
			foreach( ListViewItem item in lvicol ) {
				if( CompareItemText.Equals( item.Text ) )
					return true;
			}
			return false;
		}
		
		// удаление всех итемов
		public static bool deleteAllItems( ListView lv, string MessageBoxTitle, string LestName ) {
			if( lv.Items.Count > 0 ) {
				string sMess = "Вы действительно хотите удалить ВЕСЬ список " + LestName + "?";
				const MessageBoxButtons buttons = MessageBoxButtons.YesNo;
				DialogResult result = MessageBox.Show( sMess, MessageBoxTitle, buttons, MessageBoxIcon.Question );
				if( result == DialogResult.Yes ) {
					lv.Items.Clear();
					return true;
				}
			}
			return false;
		}
		
		// удаление выделенного итема
		public static bool deleteSelectedItem( ListView lv, string MessageBoxTitle, string LestName ) {
			if( lv.SelectedItems.Count > 0 ) {
				string sMess = "Вы действительно хотите удалить выбранный элемент из списка " + LestName + "?";
				const MessageBoxButtons buttons = MessageBoxButtons.YesNo;
				DialogResult result = MessageBox.Show( sMess, MessageBoxTitle, buttons, MessageBoxIcon.Question );
				if( result == DialogResult.Yes ) {
					lv.Items.Remove( lv.SelectedItems[0] );
					return true;
				}
			}
			return false;
		}
		#endregion
		
		#region Разное
		// авторазмер колонок Списка ListView
		public static void AutoResizeColumns( ListView listView ) {
			listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
//			for(int i=0; i!=listView.Columns.Count; ++i)
//				listView.Columns[i].Width = listView.Columns[i].Width + 2;
		}
		// переход на указанный итем
		public static void SelectedItemEnsureVisible( ListView listView, int Index ) {
			listView.Select();
			listView.Items[Index].Selected = true;
			listView.EnsureVisible(Index);
		}
		// число помеченных итемов в группе
		public static int countCheckedItemsInGroup( ListViewGroup Group ) {
			int i = 0;
			foreach( ListViewItem lvi in Group.Items ) {
				if( lvi.Checked )
					++i;
			}
			return i;
		}
		// помеченные итемы в группе
		public static IList<ListViewItem> checkedItemsInGroup( ListViewGroup Group ) {
			IList<ListViewItem> ChekedItems = new List<ListViewItem>();
			ListView.ListViewItemCollection glvic = Group.Items;
			foreach( ListViewItem lvi in glvic ) {
				if( lvi.Checked )
					ChekedItems.Add( lvi );
			}
			return ChekedItems;
		}
		#endregion
	}
}
