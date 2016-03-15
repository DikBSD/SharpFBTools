/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 07.09.2009
 * Time: 13:25
 * 
 * License: GPL 2.1
 */
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;

using FilesCountViewDupCollumn	= Core.Common.Enums.FilesCountViewDupCollumn;

namespace Core.Common
{
	/// <summary>
	/// Работа с ListView
	/// </summary>
	public class MiscListView
	{
		#region Класс для сравнения данных колонок ListView
		public class FilemanagerColumnSorter : IComparer
		{
			/// <summary>
			/// Specifies the column to be sorted
			/// </summary>
			private int ColumnToSort;
			/// <summary>
			/// Specifies the order in which to sort (i.e. 'Ascending').
			/// </summary>
			private SortOrder OrderOfSort;
			/// <summary>
			/// Case insensitive comparer object
			/// </summary>
			private CaseInsensitiveComparer ObjectCompare;

			/// <summary>
			/// Class constructor.  Initializes various elements
			/// </summary>
			public FilemanagerColumnSorter()
			{
				// Initialize the column to '0'
				ColumnToSort = 0;

				// Initialize the sort order to 'none'
				OrderOfSort = SortOrder.None;

				// Initialize the CaseInsensitiveComparer object
				ObjectCompare = new CaseInsensitiveComparer();
			}

			/// <summary>
			/// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
			/// </summary>
			/// <param name="x">First object to be compared</param>
			/// <param name="y">Second object to be compared</param>
			/// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
			public int Compare(object x, object y) {
				int compareResult;
				ListViewItem listviewX = (ListViewItem)x;
				ListViewItem listviewY = (ListViewItem)y;
				ListViewItemType it1 = (ListViewItemType)listviewX.Tag;
				ListViewItemType it2 = (ListViewItemType)listviewY.Tag;
				if( it1.Type == "f" && it2.Type == "f" ) {
					compareResult = ObjectCompare.Compare(
						listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text
					);
					// Calculate correct return value based on object comparison
					if( OrderOfSort == SortOrder.Ascending ) {
						// Ascending sort is selected, return normal result of compare operation
						return compareResult;
					} else if( OrderOfSort == SortOrder.Descending ) {
						// Descending sort is selected, return negative result of compare operation
						return (-compareResult);
					}
					else
						return 0; // Return '0' to indicate they are equal
				}
				return 0; // Return '0' to indicate they are equal
			}
			
			/// <summary>
			/// Номер столбца для выполнения сортировки (По-умолчанию '0').
			/// </summary>
			public int SortColumn {
				set {
					ColumnToSort = value;
				}
				get {
					return ColumnToSort;
				}
			}

			/// <summary>
			/// Порядок сортировки ('Ascending' или 'Descending')
			/// </summary>
			public SortOrder Order {
				set {
					OrderOfSort = value;
				}
				get {
					return OrderOfSort;
				}
			}
		}
		#endregion
		
		#region Класс для сравнения данных колонок ListView
		public class ListViewColumnSorter : IComparer
		{
			/// <summary>
			/// Specifies the column to be sorted
			/// </summary>
			private int ColumnToSort;
			/// <summary>
			/// Specifies the order in which to sort (i.e. 'Ascending').
			/// </summary>
			private SortOrder OrderOfSort;
			/// <summary>
			/// Case insensitive comparer object
			/// </summary>
			private CaseInsensitiveComparer ObjectCompare;

			/// <summary>
			/// Class constructor.  Initializes various elements
			/// </summary>
			public ListViewColumnSorter()
			{
				// Initialize the column to '0'
				ColumnToSort = 0;

				// Initialize the sort order to 'none'
				OrderOfSort = SortOrder.None;

				// Initialize the CaseInsensitiveComparer object
				ObjectCompare = new CaseInsensitiveComparer();
			}

			/// <summary>
			/// This method is inherited from the IComparer interface.  It compares the two objects passed using a case insensitive comparison.
			/// </summary>
			/// <param name="x">First object to be compared</param>
			/// <param name="y">Second object to be compared</param>
			/// <returns>The result of the comparison. "0" if equal, negative if 'x' is less than 'y' and positive if 'x' is greater than 'y'</returns>
			public int Compare(object x, object y) {
				int compareResult;
				ListViewItem listviewX = (ListViewItem)x;
				ListViewItem listviewY = (ListViewItem)y;
				compareResult = ObjectCompare.Compare(
					listviewX.SubItems[ColumnToSort].Text, listviewY.SubItems[ColumnToSort].Text
				);
				// Calculate correct return value based on object comparison
				if( OrderOfSort == SortOrder.Ascending ) {
					// Ascending sort is selected, return normal result of compare operation
					return compareResult;
				} else if( OrderOfSort == SortOrder.Descending ) {
					// Descending sort is selected, return negative result of compare operation
					return (-compareResult);
				}
				else
					return 0; // Return '0' to indicate they are equal
			}
			
			/// <summary>
			/// Номер столбца для выполнения сортировки (По-умолчанию '0').
			/// </summary>
			public int SortColumn {
				set {
					ColumnToSort = value;
				}
				get {
					return ColumnToSort;
				}
			}

			/// <summary>
			/// Порядок сортировки ('Ascending' или 'Descending')
			/// </summary>
			public SortOrder Order {
				set {
					OrderOfSort = value;
				}
				get {
					return OrderOfSort;
				}
			}
		}
		#endregion
		
		public MiscListView()
		{
		}
		
		#region Работа с отдельными итемами ListView
		// увеличение значения 2-й колонки ListView на 1
		public static void IncListViewStatus( ListView lv, int nItem ) {
			lv.Items[nItem].SubItems[1].Text =
				Convert.ToString( 1 + Convert.ToInt32( lv.Items[nItem].SubItems[1].Text ) );
		}
		// уменьшение значения 2-й колонки ListView на 1
		public static void DecListViewStatus( ListView lv, int nItem ) {
			lv.Items[nItem].SubItems[1].Text =
				Convert.ToString( Convert.ToInt32( lv.Items[nItem].SubItems[1].Text ) - 1 );
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
				for( int i=0; i!=lv.Items.Count; ++i )
					lv.Items[i].Checked = bCheck;
			}
		}
		// снять отметки с помеченных итемов
		public static void UnCheckAllListViewItems( ListView.CheckedListViewItemCollection checkedItems ) {
			foreach( ListViewItem lvi in checkedItems )
				lvi.Checked = false;
		}
		
		// пометить/снять пометку все выделенные элементы
		public static void ChekAllSelectedItems(ListView lv, bool bCheck) {
			System.Windows.Forms.ListView.SelectedListViewItemCollection selectedItems = lv.SelectedItems;
			foreach( ListViewItem lvi in selectedItems )
				lvi.Checked = bCheck;
		}
		
		// пометить/снять отметки с  итемов в выбранной группе
		public static void CheckAllListViewItemsInGroup( ListViewGroup Group, bool bCheck ) {
			foreach( ListViewItem lvi in Group.Items )
				lvi.Checked = bCheck;
		}
		
		// пометить все файлы определенного типа
		public static void CheckTypeAllFiles(ListView lv, string sType, bool bCheck) {
			if( lv.Items.Count > 0  ) {
				DirectoryInfo di = null;
				for( int i = 0; i != lv.Items.Count; ++i ) {
					ListViewItemType it = (ListViewItemType)lv.Items[i].Tag;
					if( it.Type == "f" ) {
						di = new DirectoryInfo(it.Value);
						if( di.Extension.ToLower() == "." + sType.ToLower() )
							lv.Items[i].Checked = bCheck;
					}
				}
			}
		}
		
		// снять пометку со всех файлов пределенного типа
		public static void UnCheckTypeAllFiles(ListView lv, string sType) {
			DirectoryInfo di = null;
			foreach( ListViewItem lvi in lv.CheckedItems ) {
				ListViewItemType it = (ListViewItemType)lvi.Tag;
				if( it.Type == "f" ) {
					di = new DirectoryInfo(it.Value);
					if( di.Extension.ToLower() == "." +sType.ToLower() )
						lvi.Checked = false;
				}
			}
		}
		
		// пометить все файлы
		public static void CheckAllFiles(ListView lv, bool bCheck) {
			if( lv.Items.Count > 0  ) {
				for( int i = 0; i != lv.Items.Count; ++i ) {
					ListViewItemType it = (ListViewItemType)lv.Items[i].Tag;
					if( it.Type == "f" )
						lv.Items[i].Checked = bCheck;
				}
			}
		}
		
		// снять пометку со всех файлов
		public static void UnCheckAllFiles(ListView lv) {
			foreach( ListViewItem lvi in lv.CheckedItems ) {
				ListViewItemType it = (ListViewItemType)lvi.Tag;
				if( it.Type == "f" )
					lvi.Checked = false;
			}
		}
		
		// отметить все папки
		public static void CheckAllDirs(ListView lv, bool bCheck) {
			if( lv.Items.Count > 0  ) {
				for( int i=0; i!=lv.Items.Count; ++i ) {
					ListViewItemType it = (ListViewItemType)lv.Items[i].Tag;
					if( it.Type == "d" )
						lv.Items[i].Checked = bCheck;
				}
			}
		}
		
		// снять пометку со всех папок
		public static void UnCheckAllDirs(ListView lv) {
			foreach( ListViewItem lvi in lv.CheckedItems ) {
				ListViewItemType it = (ListViewItemType)lvi.Tag;
				if( it.Type == "d" )
					lvi.Checked = false;
			}
		}
		
		#endregion

		#region Добавление, удаление итемов
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
		public static bool deleteSelectedItem( ListView lv, string MessageBoxTitle, string ItemName ) {
			if( lv.SelectedItems.Count > 0 ) {
				string sMess = "Вы действительно хотите удалить выбранные элементы из списка " + ItemName + "?";
				const MessageBoxButtons buttons = MessageBoxButtons.YesNo;
				DialogResult result = MessageBox.Show( sMess, MessageBoxTitle, buttons, MessageBoxIcon.Question );
				if( result == DialogResult.Yes ) {
					int SelectedItemsCount = lv.SelectedItems.Count;
					for( int i = 0; i != SelectedItemsCount; ++i )
						lv.Items.Remove( lv.SelectedItems[0] );
					return true;
				}
			}
			return false;
		}
		
		// удаление всех помеченных элементов Списка (их файлы на жестком диске не удаляются) для Корректора
		public static bool removeChechedItemsNotDeleteFiles( ListView listViewFB2Files ) {
			bool Result = false;
			foreach( ListViewItem lvi in listViewFB2Files.CheckedItems ) {
				listViewFB2Files.Items.Remove( lvi );
				Result = true;
			}
			return Result;
		}
		
		// удаление всех элементов Списка, для которых отсутствуют файлы на жестком диске для Корректора
		public static bool removeAllItemForNonExistFile( string SourсeDir, ListView listViewFB2Files ) {
			bool Result = false;
			listViewFB2Files.BeginUpdate();
			foreach( ListViewItem lvi in listViewFB2Files.Items ) {
				if( ((ListViewItemType)lvi.Tag).Type == "f" ) {
					if ( !File.Exists( Path.Combine( SourсeDir, lvi.Text ) ) ) {
						listViewFB2Files.Items.Remove( lvi );
						Result = true;
					}
				}
			}
			listViewFB2Files.EndUpdate();
			return Result;
		}
		
		// удаление всех элементов Списка, для которых отсутствуют файлы на жестком диске для Дубликатора
		public static void deleteAllItemForNonExistFileWithCounter( ListView listView, ListViewItem RemoveListViewItem,
		                                                           bool RemoveFast, ref int AllFiles ) {
			if( !RemoveFast ) {
				if( listView.Items.Count > 0 ) {
					ListViewGroup lvg = RemoveListViewItem.Group;
					listView.Items.Remove( RemoveListViewItem );
					if( lvg != null && lvg.Items.Count <= 1 ) {
						if( lvg.Items.Count == 1 ) {
							listView.Items[lvg.Items[0].Index].Remove();
							--AllFiles;
						}
						listView.Groups.Remove( lvg );
					}
				}
			} else {
				// пометка цветом и зачеркиванием удаленных книг с диска, но не из списка (быстрый режим удаления)
				WorksWithBooks.markRemoverFileInCopyesList( RemoveListViewItem );
			}
		}
		
		// удаление всех элементов Списка, для которых отсутствуют файлы на жестком диске для Дубликатора
		public static bool deleteAllItemForNonExistFile( ListView listViewFB2Files ) {
			bool Result = false;
			foreach( ListViewItem lvi in listViewFB2Files.Items ) {
				if ( !File.Exists( lvi.Text ) ) {
					ListViewGroup lvg = lvi.Group;
					listViewFB2Files.Items.Remove( lvi );
					// удаление Групп с 1 элементом (и сам элемент)
					if( lvg != null && lvg.Items.Count <= 1 ) {
						if( lvg.Items.Count == 1 )
							listViewFB2Files.Items[lvg.Items[0].Index].Remove();
						listViewFB2Files.Groups.Remove( lvg );
					}
					Result = true;
				}
			}
			return Result;
		}
		
		// удаление всех помеченных элементов Списка (их файлы на жестком диске не удаляются) для Дубликатора
		public static bool deleteChechedItemsNotDeleteFiles( ListView listViewFB2Files, ListView lvFilesCount ) {
			bool Result = false;
			listViewFB2Files.BeginUpdate();
			int RemoveGroupCount = 0;
			int RemoveItemCount = 0;
			foreach( ListViewItem lvi in listViewFB2Files.CheckedItems ) {
				ListViewGroup lvg = lvi.Group;
				listViewFB2Files.Items.Remove( lvi );
				++RemoveItemCount;
				// удаление Групп с 1 элементом (и сам элемент)
				if( lvg != null && lvg.Items.Count <= 1 ) {
					if( lvg.Items.Count == 1 ) {
						listViewFB2Files.Items[lvg.Items[0].Index].Remove();
						++RemoveItemCount;
					}
					listViewFB2Files.Groups.Remove( lvg );
					++RemoveGroupCount;
				}
				Result = true;
			}
			// реальное число Групп и книг в них
			lvFilesCount.Items[(int)FilesCountViewDupCollumn.AllGroups].SubItems[1].Text =
				(Convert.ToInt16(lvFilesCount.Items[(int)FilesCountViewDupCollumn.AllGroups].SubItems[1].Text) - RemoveGroupCount).ToString();
			lvFilesCount.Items[(int)FilesCountViewDupCollumn.AllBoolsInAllGroups].SubItems[1].Text =
				(Convert.ToInt16(lvFilesCount.Items[(int)FilesCountViewDupCollumn.AllBoolsInAllGroups].SubItems[1].Text) - RemoveItemCount).ToString();
			
			listViewFB2Files.EndUpdate();
			return Result;
		}
		
		
		#endregion
		
		#region Перемещение итемов в списке...
		// перемещение выделенного итема вверх
		public static bool moveUpSelectedItem( ListView listView ) {
			if( listView.Items.Count > 0 && listView.SelectedItems.Count > 0 ) {
				if( listView.SelectedItems.Count == 1 ) {
					if( listView.SelectedItems[0].Index > 0 ) {
						ListViewItem SelItem = listView.SelectedItems[0];
						int SelItemIndex = SelItem.Index;
						int UpItemIndex = SelItemIndex - 1;
						ListViewItem UpItem = listView.Items[UpItemIndex];
						ListViewItem TempItem = (ListViewItem)UpItem.Clone();
						listView.Items.Remove( UpItem );
						listView.Items.Insert( SelItemIndex, TempItem );
						unSelectAllItems( listView );
						listView.Items[UpItemIndex].Selected = true;
						listView.Items[UpItemIndex].Focused = true;
						listView.Select();
						return true;
					}
				}
				listView.Select();
			}
			return false;
		}
		
		// перемещение выделенного итема вниз
		public static bool moveDownSelectedItem( ListView listView ) {
			if( listView.Items.Count > 0 && listView.SelectedItems.Count > 0 ) {
				if( listView.SelectedItems.Count == 1 ) {
					if( listView.SelectedItems[0].Index < listView.Items.Count - 1 ) {
						ListViewItem SelItem = listView.SelectedItems[0];
						int SelItemIndex = SelItem.Index;
						int DownItemIndex = SelItemIndex + 1;
						ListViewItem DownItem = listView.Items[DownItemIndex];
						ListViewItem TempItem = (ListViewItem)DownItem.Clone();
						listView.Items.Remove( DownItem );
						listView.Items.Insert( SelItemIndex, TempItem );
						unSelectAllItems( listView );
						listView.Items[DownItemIndex].Selected = true;
						listView.Items[DownItemIndex].Focused = true;
						listView.Select();
						return true;
					}
				}
				listView.Select();
			}
			return false;
		}
		#endregion
		
		#region Разное
		// сортировка списка по нажаьтю на колонку
		public static void SortColumnClick( ListView listView, ListViewColumnSorter listViewColumnSorter, ColumnClickEventArgs e ) {
			if ( e.Column == listViewColumnSorter.SortColumn ) {
				// Изменить сортировку на обратную для выбранного столбца
				if ( listViewColumnSorter.Order == SortOrder.Ascending )
					listViewColumnSorter.Order = SortOrder.Descending;
				else
					listViewColumnSorter.Order = SortOrder.Ascending;
			} else {
				// Задать номер столбца для сортировки (по-умолчанию Ascending)
				listViewColumnSorter.SortColumn = e.Column;
				listViewColumnSorter.Order = SortOrder.Ascending;
			}
			listView.ListViewItemSorter = listViewColumnSorter; // перед listView.Sort(); иначе - "тормоза"
			listView.Sort();
		}
		// авторазмер колонок Списка ListView
		public static void AutoResizeColumns( ListView listView ) {
			listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
//			for(int i=0; i!=listView.Columns.Count; ++i)
//				listView.Columns[i].Width = listView.Columns[i].Width + 2;
		}
		// переход на указанный итем
		public static void SelectedItemEnsureVisible( ListView listView, int Index ) {
			if ( listView.Items.Count > 0 ) {
				listView.Select();
				listView.Items[Index].Selected = true;
				listView.EnsureVisible(Index);
			}
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
		// снять выделение со всех итемов
		public static void unSelectAllItems( ListView listView ) {
			foreach( ListViewItem item in listView.Items )
				item.Selected = false;
		}
		#endregion
	}
}
