/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 20.09.2013
 * Time: 14:05
 * 
 * License: GPL 2.1
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using Settings;
using Core.FB2.Genres;
using Core.Misc;

using archivesWorker	= Core.FilesWorker.Archiver;
using filesWorker		= Core.FilesWorker.FilesWorker;

namespace Core.FileManager
{
	/// <summary>
	/// Description of FileManagerWork.
	/// </summary>
	public class FileManagerWork
	{
		#region Закрытые данные класса
		private const string space		= " "; // для задания отступов данных от границ колонов в Списке
		#endregion
		
		public FileManagerWork()
		{
		}
		
		static public string CyrillicGenreName(bool IsFB2LibrusecGenres, string GenreCode) {
			IFBGenres fb2g = null;
			if( IsFB2LibrusecGenres ) {
				fb2g = new FB2LibrusecGenres();
			} else {
				fb2g = new FB22Genres();
			}
			if(GenreCode.IndexOf(';') != -1) {
				string ret = "";
				string[] sG = GenreCode.Split(';');
				foreach(string s in sG) {
					ret += fb2g.GetFBGenreName(s.Trim()) + "; ";
					ret.Trim();
				}
				return ret.Substring( 0, ret.LastIndexOf( ";" ) ).Trim();
			}
			return fb2g.GetFBGenreName(GenreCode);;
		}
		
		static public void AutoResizeColumns(ListView listView) {
			// авторазмер колонок Списка
			listView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
//			for(int i=0; i!=listView.Columns.Count; ++i) {
//				listView.Columns[i].Width = listView.Columns[i].Width + 2;
//			}
		}
		
		static public void GenerateSourceList(string dirPath, ListView listView, bool itemChecked, bool IsFB2LibrusecGenres, bool isTagsView, bool isColumnsAutoReize) {
        	// заполнение списка данными указанной папки
        	Cursor.Current = Cursors.WaitCursor;
        	listView.BeginUpdate();
        	listView.Items.Clear();
        	try {
        		Settings.DataFM dfm = new Settings.DataFM();
        		DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
        		ListViewItem.ListViewSubItem[] subItems;
        		ListViewItem item = null;
        		if (dirInfo.Exists) {
        			if(dirInfo.Parent != null) {
        				item = new ListViewItem("..", 3);
        				item.Tag = new ListViewItemType("dUp", dirInfo.Parent.FullName);
        				listView.Items.Add(item);
        			}
        			int nItemCount = 0;
        			foreach (DirectoryInfo dir in dirInfo.GetDirectories()) {
        				item = new ListViewItem(space+dir.Name+space, 0);
        				item.Checked = itemChecked;
        				item.Tag = new ListViewItemType("d", dir.FullName);
        				if(nItemCount%2 == 0) { // четное
        					item.BackColor = Color.Beige;
        				}
        				listView.Items.Add(item);
        				++nItemCount;
        			}
        			FB2BookDescription bd = null;
        			foreach (FileInfo file in dirInfo.GetFiles()) {
        				if(file.Extension.ToLower()==".fb2" || file.Extension.ToLower()==".zip") {
        					item = new ListViewItem(" "+file.Name+" ", file.Extension.ToLower()==".fb2" ? 1 : 2);
        					try {
        						if(file.Extension.ToLower()==".fb2") {
        							if(isTagsView) {
        								bd = new FB2BookDescription( file.FullName );
        								subItems = new ListViewItem.ListViewSubItem[] {
        									new ListViewItem.ListViewSubItem(item, space+bd.TIBookTitle+space),
        									new ListViewItem.ListViewSubItem(item, space+bd.TISequences+space),
        									new ListViewItem.ListViewSubItem(item, space+bd.TIAuthors+space),
        									new ListViewItem.ListViewSubItem(item, space+CyrillicGenreName(IsFB2LibrusecGenres, bd.TIGenres)+space),
        									new ListViewItem.ListViewSubItem(item, space+bd.TILang+space),
        									new ListViewItem.ListViewSubItem(item, space+bd.Encoding+space)
        								};
        							} else {
        								subItems = new ListViewItem.ListViewSubItem[] {
        									new ListViewItem.ListViewSubItem(item, ""),
        									new ListViewItem.ListViewSubItem(item, ""),
        									new ListViewItem.ListViewSubItem(item, ""),
        									new ListViewItem.ListViewSubItem(item, ""),
        									new ListViewItem.ListViewSubItem(item, ""),
        									new ListViewItem.ListViewSubItem(item, "")
        								};
        							}
        						} else {
        							// для zip-архивов
        							if(isTagsView) {
        								filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
        								archivesWorker.unzip(dfm.A7zaPath, file.FullName, Settings.Settings.GetTempDir(), ProcessPriorityClass.AboveNormal );
        								string [] files = Directory.GetFiles( Settings.Settings.GetTempDir() );
        								bd = new FB2BookDescription( files[0] );
        								subItems = new ListViewItem.ListViewSubItem[] {
        									new ListViewItem.ListViewSubItem(item, space+bd.TIBookTitle+space),
        									new ListViewItem.ListViewSubItem(item, space+bd.TISequences+space),
        									new ListViewItem.ListViewSubItem(item, space+bd.TIAuthors+space),
        									new ListViewItem.ListViewSubItem(item, space+CyrillicGenreName(IsFB2LibrusecGenres, bd.TIGenres)+space),
        									new ListViewItem.ListViewSubItem(item, space+bd.TILang+space),
        									new ListViewItem.ListViewSubItem(item, space+bd.Encoding+space)
        								};
        							} else {
        								subItems = new ListViewItem.ListViewSubItem[] {
        									new ListViewItem.ListViewSubItem(item, ""),
        									new ListViewItem.ListViewSubItem(item, ""),
        									new ListViewItem.ListViewSubItem(item, ""),
        									new ListViewItem.ListViewSubItem(item, ""),
        									new ListViewItem.ListViewSubItem(item, ""),
        									new ListViewItem.ListViewSubItem(item, "")
        								};
        							}
        						}
        						item.SubItems.AddRange(subItems);
        					} catch(System.Exception) {
        						item.ForeColor = Color.Blue;
        					}
        					
        					item.Checked = itemChecked;
        					item.Tag = new ListViewItemType("f", file.FullName);
        					if(nItemCount%2 == 0) { // четное
        						item.BackColor = Color.AliceBlue;
        					}
        					listView.Items.Add(item);
        					++nItemCount;
        				}
        			}
        			// авторазмер колонок Списка Проводника
        			if(isColumnsAutoReize) {
        				AutoResizeColumns(listView);
        			}
        		}
        		
        	} catch (System.Exception) {
        	} finally {
        		listView.EndUpdate();
        		Cursor.Current = Cursors.Default;
        	}
        }

	}
}
