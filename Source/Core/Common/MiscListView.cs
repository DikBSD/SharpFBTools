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
using System.Globalization;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

using FilesCountViewDupCollumnEnum	= Core.Common.Enums.FilesCountViewDupCollumnEnum;

namespace Core.Common
{
	/// <summary>
	/// Работа с ListView
	/// </summary>
	public class MiscListView
	{
		#region Класс для сравнения данных колонок ListView
		/// <summary>
		/// Класс для сравнения данных колонок ListView
		/// </summary>
		public class ListViewColumnSorter : IComparer
		{
			/// <summary>
			/// Столбец для сортировки
			/// </summary>
			private int _ColumnToSort;
			/// <summary>
			/// Порядок сортировки (наприм. «По возрастанию» ('Ascending'))
			/// </summary>
			private SortOrder _OrderOfSort;
			/// <summary>
			/// Объект сравнения без учета регистра
			/// </summary>
			private CaseInsensitiveComparer _ObjectCompare;
			/// <summary>
			/// Номер столбца для размеров файлов
			/// </summary>
			private readonly int _FileSizeColumnNumber;

			/// <summary>
			/// Конструктор класса. Инициализация элементов
			/// </summary>
			/// <param name="FileSizeColumnNumber">Номер столбца для размеров файлов</param>
			public ListViewColumnSorter(int FileSizeColumnNumber)
			{
				_FileSizeColumnNumber = FileSizeColumnNumber;
				_ColumnToSort = 0;
				_OrderOfSort = SortOrder.None;
				_ObjectCompare = new CaseInsensitiveComparer();
			}
			
			/// <summary>
			/// Сравнение двух объектов. Этот метод унаследован от интерфейса IComparer. Он сравнивает два переданных объекта, используя сравнение без учета регистра.
			/// </summary>
			/// <param name="x">Первый объект для сравнения</param>
			/// <param name="y">Второй объект для сравнения</param>
			/// <returns>Результат сравнения. «0», если «x» равен «y», отрицательный, если «x» меньше, чем «y», и положительный, если «x» больше, чем «y»</returns>
			public int Compare(object x, object y) {
				int compareResult = 0;
				ListViewItem listviewX = (ListViewItem)x;
				ListViewItem listviewY = (ListViewItem)y;
				ListViewItemType itX = (ListViewItemType)listviewX.Tag;
				ListViewItemType itY = (ListViewItemType)listviewY.Tag;
				if ( itX != null && itY != null ) {
					// Для ListView в качестве файлового менеджера
					if ( itX.Type == "f" && itY.Type == "f" ) {
						// Пропускаем переход на уровень выше
						compareResult = Compare(listviewX, listviewY, ref compareResult);
					}
				} else {
					// Для ListView, которые не являются файловым менеджером
					compareResult = Compare(listviewX, listviewY, ref compareResult);
				}
				
				// Определяем, является ли порядок сортировки по возрастанию
				if (_OrderOfSort == SortOrder.Ascending)
					compareResult *= -1; // Инвертируем значение, возвращаемое String.Compare
				
				return compareResult;
			}
			
			/// <summary>
			/// Номер столбца для выполнения сортировки (по-умолчанию '0').
			/// </summary>
			public int SortColumn {
				set {
					_ColumnToSort = value;
				}
				get {
					return _ColumnToSort;
				}
			}

			/// <summary>
			/// Порядок сортировки ('Ascending' или 'Descending')
			/// </summary>
			public SortOrder Order {
				set {
					_OrderOfSort = value;
				}
				get {
					return _OrderOfSort;
				}
			}
			
			/// <summary>
			/// Алгоритм сравнения
			/// </summary>
			private int Compare(ListViewItem listviewX, ListViewItem listviewY, ref int compareResult) {
				// Определяем, является ли сравниваемый тип типом даты
				try {
					// Рассматриваем два объекта, переданных в качестве параметра, как DateTime
					DateTime firstDate =
						DateTime.Parse(listviewX.SubItems[_ColumnToSort].Text);
					DateTime secondDate =
						DateTime.Parse(listviewY.SubItems[_ColumnToSort].Text);
					// Сравниваем две даты.
					compareResult = DateTime.Compare(firstDate, secondDate);
				} catch {
					// Если ни один из сравниваемых объектов не имеет допустимого формата даты
					if ( _ColumnToSort == _FileSizeColumnNumber) {
						// Рассматриваем оба объекта, как размеры файлов (_ColumnToSort = 9)
						double X = 0, Y = 0;
						// Отделяем размер файла от символьного значения Кб, Мб
						string [] sX = listviewX.SubItems[_ColumnToSort].Text.Split(' ');
						string [] sY = listviewY.SubItems[_ColumnToSort].Text.Split(' ');
						
						// Перевод размера первого файла в биты
						switch (sX[1]) {
							case "Кб":
								X = Convert.ToDouble(sX[0]);
								X *= 1024;
								break;
							case "Мб":
								X = Convert.ToDouble(sX[0]);
								X *= (1024*1024);
								break;
						}
						
						// Перевод размера первого файла в биты
						switch (sY[1]) {
							case "Кб":
								Y = Convert.ToDouble(sY[0]);
								Y *= 1024;
								break;
							case "Мб":
								Y = Convert.ToDouble(sY[0]);
								Y *= (1024*1024);
								break;
						}
						
						// Сравнение двух размеров файлов
						if ( X > Y )
							compareResult = 1;
						else if ( X < Y )
							compareResult = -1;
						else
							compareResult = 0;
					} else {
						// Сравниваем два объекта, как строки
						compareResult = String.Compare(
							listviewX.SubItems[_ColumnToSort].Text,
							listviewY.SubItems[_ColumnToSort].Text,
							CultureInfo.CurrentCulture, System.Globalization.CompareOptions.StringSort
						);
					}
				}
				return compareResult;
			}
		}
		#endregion
		
		public MiscListView()
		{
		}
		
		#region Работа с отдельными итемами ListView
		// увеличение значения 2-й колонки ListView на 1
		public static void IncListViewStatus( ListView lv, int nItem ) {
			if ( lv.Items.Count > 0 ) {
				if ( nItem > -1 ) {
					lv.Items[nItem].SubItems[1].Text =
						Convert.ToString( 1 + Convert.ToInt32( lv.Items[nItem].SubItems[1].Text ) );
				}
			}
		}
		// уменьшение значения 2-й колонки ListView на 1
		public static void DecListViewStatus( ListView lv, int nItem ) {
			if ( lv.Items.Count > 0 ) {
				if ( nItem > -1 ) {
					lv.Items[nItem].SubItems[1].Text =
						Convert.ToString( Convert.ToInt32( lv.Items[nItem].SubItems[1].Text ) - 1 );
				}
			}
		}
		// занесение в нужный item определеного значения
		public static void ListViewStatus( ListView lv, int nItem, int nValue ) {
			if ( lv.Items.Count > 0 ) {
				if ( nItem > -1 ) {
					lv.Items[nItem].SubItems[1].Text = Convert.ToString( nValue );
				}
			}
		}
		
		// занесение в нужный item определеного значения
		public static void ListViewStatus( ListView lv, int nItem, string sValue ) {
			if ( lv.Items.Count > 0 ) {
				if ( nItem > -1 ) {
					lv.Items[nItem].SubItems[1].Text = string.IsNullOrWhiteSpace(sValue)
						? string.Empty : sValue.Trim();
				}
			}
		}
		#endregion
		
		#region Пометить / Снять отметки
		// отметить все итемы (снять все отметки)
		public static void CheckAllListViewItems( ListView lv, bool bCheck ) {
			if ( lv.Items.Count > 0  ) {
				for ( int i=0; i!=lv.Items.Count; ++i )
					lv.Items[i].Checked = bCheck;
			}
		}
		// снять отметки с помеченных итемов
		public static void UnCheckAllListViewItems( ListView.CheckedListViewItemCollection checkedItems ) {
			foreach ( ListViewItem lvi in checkedItems )
				lvi.Checked = false;
		}
		
		// пометить/снять пометку все выделенные элементы
		public static void ChekAllSelectedItems(ListView lv, bool bCheck) {
			System.Windows.Forms.ListView.SelectedListViewItemCollection selectedItems = lv.SelectedItems;
			foreach ( ListViewItem lvi in selectedItems )
				lvi.Checked = bCheck;
		}
		
		// пометить/снять отметки с  итемов в выбранной группе
		public static void CheckAllListViewItemsInGroup( ListViewGroup Group, bool bCheck ) {
			foreach ( ListViewItem lvi in Group.Items )
				lvi.Checked = bCheck;
		}
		
		// пометить все файлы определенного типа
		public static void CheckTypeAllFiles(ListView lv, string sType, bool bCheck) {
			if ( lv.Items.Count > 0  ) {
				DirectoryInfo di = null;
				for ( int i = 0; i != lv.Items.Count; ++i ) {
					ListViewItemType it = (ListViewItemType)lv.Items[i].Tag;
					if ( it.Type == "f" ) {
						di = new DirectoryInfo(it.Value);
						if ( di.Extension.ToLower() == "." + sType.ToLower() )
							lv.Items[i].Checked = bCheck;
					}
				}
			}
		}
		
		// снять пометку со всех файлов пределенного типа
		public static void UnCheckTypeAllFiles(ListView lv, string sType) {
			DirectoryInfo di = null;
			foreach ( ListViewItem lvi in lv.CheckedItems ) {
				ListViewItemType it = (ListViewItemType)lvi.Tag;
				if ( it.Type == "f" ) {
					di = new DirectoryInfo(it.Value);
					if ( di.Extension.ToLower() == "." +sType.ToLower() )
						lvi.Checked = false;
				}
			}
		}
		
		// пометить все файлы
		public static void CheckAllFiles(ListView lv, bool bCheck) {
			if ( lv.Items.Count > 0  ) {
				for ( int i = 0; i != lv.Items.Count; ++i ) {
					ListViewItemType it = (ListViewItemType)lv.Items[i].Tag;
					if ( it.Type == "f" )
						lv.Items[i].Checked = bCheck;
				}
			}
		}
		
		// снять пометку со всех файлов
		public static void UnCheckAllFiles(ListView lv) {
			foreach ( ListViewItem lvi in lv.CheckedItems ) {
				ListViewItemType it = (ListViewItemType)lvi.Tag;
				if ( it.Type == "f" )
					lvi.Checked = false;
			}
		}
		
		// отметить все папки
		public static void CheckAllDirs(ListView lv, bool bCheck) {
			if ( lv.Items.Count > 0  ) {
				for ( int i = 0; i != lv.Items.Count; ++i ) {
					ListViewItemType it = (ListViewItemType)lv.Items[i].Tag;
					if ( it.Type == "d" )
						lv.Items[i].Checked = bCheck;
				}
			}
		}
		
		// снять пометку со всех папок
		public static void UnCheckAllDirs(ListView lv) {
			if ( lv.Items.Count > 0  ) {
				foreach ( ListViewItem lvi in lv.CheckedItems ) {
					ListViewItemType it = (ListViewItemType)lvi.Tag;
					if ( it.Type == "d" )
						lvi.Checked = false;
				}
			}
		}
		
		#endregion

		#region Добавление, удаление итемов
		// есть ли в списке итем с текстом CompareItemText
		public static bool isExistListViewItem( ListView lv, string CompareItemText ) {
			if( lv.Items.Count > 0  ) {
				ListView.ListViewItemCollection lvicol = lv.Items;
				foreach ( ListViewItem item in lvicol ) {
					if ( CompareItemText.Equals( item.Text ) )
						return true;
				}
			}
			return false;
		}
		
		// удаление всех итемов
		public static bool deleteAllItems( ListView lv, string MessageBoxTitle, string LestName ) {
			if ( lv.Items.Count > 0 ) {
				string sMess = "Вы действительно хотите удалить ВЕСЬ список " + LestName + "?";
				const MessageBoxButtons buttons = MessageBoxButtons.YesNo;
				DialogResult result = MessageBox.Show( sMess, MessageBoxTitle, buttons, MessageBoxIcon.Question );
				if ( result == DialogResult.Yes ) {
					lv.Items.Clear();
					return true;
				}
			}
			return false;
		}
		
		// удаление выделенного итема
		public static bool deleteSelectedItem( ListView lv, string MessageBoxTitle, string ItemName ) {
			if ( lv.SelectedItems.Count > 0 ) {
				string sMess = "Вы действительно хотите удалить выбранные элементы из списка " + ItemName + "?";
				const MessageBoxButtons buttons = MessageBoxButtons.YesNo;
				DialogResult result = MessageBox.Show( sMess, MessageBoxTitle, buttons, MessageBoxIcon.Question );
				if ( result == DialogResult.Yes ) {
					int SelectedItemsCount = lv.SelectedItems.Count;
					for ( int i = 0; i != SelectedItemsCount; ++i )
						lv.Items.Remove( lv.SelectedItems[0] );
					return true;
				}
			}
			return false;
		}
		
		// удаление всех помеченных элементов Списка (их файлы на жестком диске не удаляются) для Корректора
		public static bool removeChechedItemsNotDeleteFiles( ListView listViewFB2Files ) {
			bool Result = false;
			if ( listViewFB2Files.Items.Count > 0  ) {
				foreach ( ListViewItem lvi in listViewFB2Files.CheckedItems ) {
					listViewFB2Files.Items.Remove( lvi );
					Result = true;
				}
			}
			return Result;
		}
		
		// удаление всех элементов Списка, для которых отсутствуют файлы на жестком диске для Корректора
		public static bool removeAllItemForNonExistFile( string SourсeDir, ListView listViewFB2Files ) {
			bool Result = false;
			listViewFB2Files.BeginUpdate();
			foreach ( ListViewItem lvi in listViewFB2Files.Items ) {
				if ( ((ListViewItemType)lvi.Tag).Type == "f" ) {
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
		public static void deleteAllItemForNonExistFile( ListView listViewFB2Files ) {
			foreach ( ListViewItem lvi in listViewFB2Files.Items ) {
				if ( !File.Exists( lvi.Text ) ) {
					ListViewGroup lvg = lvi.Group;
					listViewFB2Files.Items.Remove( lvi );
				}
			}
		}

		// удаление всех Групп, у которых не больше 1 итема
		public static bool deleteAllGroupsWithOneItem(ListView listViewFB2Files) {
			bool Result = false;
			foreach (ListViewItem lvi in listViewFB2Files.Items) {
				ListViewGroup lvg = lvi.Group;
				if (lvg != null && lvg.Items.Count <= 1) {
					if (lvg.Items.Count == 1)
						listViewFB2Files.Items[lvg.Items[0].Index].Remove();
					listViewFB2Files.Groups.Remove(lvg);
					Result = true;
				}
			}
			return Result;
		}

		// удаление всех помеченных элементов Списка (их файлы на жестком диске не удаляются) для Дубликатора
		public static bool deleteChechedItemsNotDeleteFiles( ListView listViewFB2Files, ListView lvFilesCount ) {
			bool Result = false;
			listViewFB2Files.BeginUpdate();
			foreach ( ListViewItem lvi in listViewFB2Files.CheckedItems ) {
				ListViewGroup lvg = lvi.Group;
				listViewFB2Files.Items.Remove( lvi );
				// удаление Групп с 1 элементом (и сам элемент)
				if ( lvg != null && lvg.Items.Count <= 1 ) {
					if ( lvg.Items.Count == 1 ) {
						listViewFB2Files.Items[lvg.Items[0].Index].Remove();
					}
					listViewFB2Files.Groups.Remove( lvg );
					Result = true;
				}
			}

            listViewFB2Files.EndUpdate();
			return Result;
		}

		// Чистка списка Групп копий книг Дубликатора от пустых итемов
		public static void cleanGroupList(ListView listViewFB2Files)
		{
			// удаление всех элементов Списка, для которых отсутствуют файлы на жестком диске для Дубликатора
			deleteAllItemForNonExistFile(listViewFB2Files);
			// удаление всех Групп, у которых не больше 1 итема
			deleteAllGroupsWithOneItem(listViewFB2Files);
		}

		#endregion

		#region Разное
		// реальное значение всех Групп и всех копий книг в этих Группах
		public static void RealGroupsAndBooks(ListView lvResult, ListView lvFilesCount)
		{
			int AllGroups = 0;
			int AllBooks = 0;
			foreach (ListViewGroup lvGroup in lvResult.Groups) {
				int RealBookInGroup = 0;
				if (lvGroup.Items.Count > 1) {
					foreach (ListViewItem lvi in lvGroup.Items) {
						if (!lvi.Font.Strikeout)
							++RealBookInGroup;
					}
				}
				if (RealBookInGroup > 1) {
					AllBooks += RealBookInGroup;
					++AllGroups;
				}
			}
			// реальное число групп копий
			ListViewStatus(lvFilesCount, (int)FilesCountViewDupCollumnEnum.AllGroups, AllGroups.ToString());
			// реальное число копий книг во всех группах
			ListViewStatus(lvFilesCount, (int)FilesCountViewDupCollumnEnum.AllBooksInAllGroups, AllBooks.ToString());
		}
		#endregion

		#region Перемещение итемов в списке...
		// перемещение выделенного итема вверх
		public static bool moveUpSelectedItem( ListView listView ) {
			if ( listView.Items.Count > 0 && listView.SelectedItems.Count > 0 ) {
				if ( listView.SelectedItems.Count == 1 ) {
					if ( listView.SelectedItems[0].Index > 0 ) {
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
			if ( listView.Items.Count > 0 && listView.SelectedItems.Count > 0 ) {
				if ( listView.SelectedItems.Count == 1 ) {
					if ( listView.SelectedItems[0].Index < listView.Items.Count - 1 ) {
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
		// сортировка списка по нажатию на колонку
		public static void SortColumnClick( ListView listView, ListViewColumnSorter listViewColumnSorter, 
		                                   ColumnClickEventArgs e ) {
			if ( e.Column == listViewColumnSorter.SortColumn ) {
				// Изменить сортировку на обратную для выбранного столбца
				listViewColumnSorter.Order = listViewColumnSorter.Order == SortOrder.Ascending
					? SortOrder.Descending : SortOrder.Ascending;
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
			foreach ( ListViewItem lvi in Group.Items ) {
				if ( lvi.Checked )
					++i;
			}
			return i;
		}
		
		// помеченные итемы в группе
		public static IList<ListViewItem> checkedItemsInGroup( ListViewGroup Group ) {
			IList<ListViewItem> ChekedItems = new List<ListViewItem>();
			ListView.ListViewItemCollection glvic = Group.Items;
			foreach ( ListViewItem lvi in glvic ) {
				if ( lvi.Checked )
					ChekedItems.Add( lvi );
			}
			return ChekedItems;
		}
		
		// снять выделение со всех итемов
		public static void unSelectAllItems( ListView listView ) {
			foreach ( ListViewItem item in listView.Items )
				item.Selected = false;
		}
		
		/// <summary>
		/// Геренация ListViewItem с пустыми значениями SubItems
		/// </summary>
		/// <param name="SubItemsCount">Число пустых SubItems</param>
		/// <returns>Сформированный Item класса ListViewItem с пустыми SubItems</returns>
		public static ListViewItem makeEmptyListViewItem( int SubItemsCount ) {
			ListViewItem lvi = new ListViewItem( string.Empty );
			for ( int i = 0; i != SubItemsCount; ++i )
				lvi.SubItems.Add( string.Empty );
			return lvi;
		}
		
		/// <summary>
		/// Добавление в дерево XDocument настроек данных о колонказ ListView
		/// </summary>
		/// <param name="xColumns">Ссылка на тег "Columns" в дереве настроек</param>
		/// <param name="listView">Ссылка на ListView</param>
		/// <returns>true, если данные добавлены; false - если doc и/или listView не существуют (данные не добавлены)</returns>
		public static bool addColumnsToXDocument( ref XElement xColumns, ref ListView listView ) {
			if ( xColumns != null && listView != null ) {
				// сбор данных по заголовкам колонок для сортировки по DisplayIndex
				for ( int i = 0; i != listView.Columns.Count; ++i ) {
					// упорядочивание сохранения заголовков по DisplayIndex
					for ( int j = 0; j != listView.Columns.Count; ++j ) {
						if ( listView.Columns[j].DisplayIndex == i ) {
							xColumns.Add(
								new XElement(
									"Column",
									new XAttribute("displayIndex", listView.Columns[j].DisplayIndex),	/* 0 */
									new XAttribute("columnName", listView.Columns[j].Tag),				/* 1 */
									new XAttribute("width", listView.Columns[j].Width)					/* 2 */
								)
							);
							break;
						}
					}
				}

				// вариант без упорядочивания сохранения заголовков по DisplayIndex
//				foreach ( ColumnHeader ch in listView.Columns ) {
//					xColumns.Add(
//						new XElement(
//							"Column",
//							new XAttribute("displayIndex", ch.DisplayIndex),	/* 0 */
//							new XAttribute("columnName", ch.Tag),				/* 1 */
//							new XAttribute("width", ch.Width)					/* 2 */
//						)
//					);
//				}
				return true;
			}
			return false;
		}
		
		/// <summary>
		/// Добавление в дерево XDocument настроек данных о колонказ ListView
		/// </summary>
		/// <param name="xColumns">Ссылка на тег "Columns" в дереве настроек</param>
		/// <param name="listView">Ссылка на ListView</param>
		/// <returns>true, если данные добавлены; false - если xmlTree и/или listView не существуют (данные не добавлены)</returns>
		public static bool setColumnHeradersDataToListView( ref XElement xColumns, ref ListView listView ) {
			if ( xColumns != null && listView != null ) {
				for ( int i = 0; i != listView.Columns.Count; ++i ) {
					IEnumerable<XElement> Columns = xColumns.Elements("Column");
					foreach ( XElement element in Columns ) {
						List<XAttribute> attrs = element.Attributes().ToList<XAttribute>();
						if ( listView.Columns[i].Tag.ToString().Contains(attrs[1].Value) ) {
							listView.Columns[i].DisplayIndex = Convert.ToInt16( attrs[0].Value );
							listView.Columns[i].Width = Convert.ToInt16( attrs[2].Value );
						}
					}
				}
				return true;
			}
			return false;
		}
		
		
		#endregion
	}
}
