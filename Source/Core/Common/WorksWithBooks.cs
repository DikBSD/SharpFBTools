﻿/*
 * Сделано в SharpDevelop.
 * Пользователь: Вадим Кузнецов (DikBSD)
 * Дата: 30.07.2015
 * Время: 8:50
 * 
 * License: GPL 2.1
 */
using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;

using Core.FB2.Genres;
using Core.FB2.Description.TitleInfo;
using Core.FB2.Description.Common;
using Core.FB2.FB2Parsers;

// enums
using ResultViewCollumn		= Core.Common.Enums.ResultViewCollumn;
using ResultViewDupCollumn	= Core.Common.Enums.ResultViewDupCollumn;
using BooksValidateMode		= Core.Common.Enums.BooksValidateMode;

namespace Core.Common
{
	/// <summary>
	/// Общий класс работы с помеченными книгами для всех инструментов
	/// </summary>
	public class WorksWithBooks
	{
		public WorksWithBooks()
		{
		}
		
		// создание списка FB2ItemInfo данных для помеченных книг Дубликатора
		public static IList<FB2ItemInfo> makeFB2InfoListForDup( ListView listView, BooksValidateMode BooksValidateType ) {
			IList<FB2ItemInfo> FB2InfoList = new List<FB2ItemInfo>();
			if( listView.Items.Count > 0 ) {
				string	TempDir = Settings.Settings.TempDir;
				IList Items = null;
				if( BooksValidateType == BooksValidateMode.SelectedBooks )
					Items = listView.SelectedItems;
				else if (BooksValidateType == BooksValidateMode.CheckedBooks)
					Items = listView.CheckedItems;
				else /* BooksValidateType == BooksValidateMode.AllBooks */
					Items = listView.Items;
				
				if( Items.Count > 0 ) {
					foreach( ListViewItem item in Items ) {
						string FilePathSource = item.Text.Trim();
						string FilePathIfFromZip = FilePathSource;
						if( File.Exists( FilePathSource ) ) {
							bool IsFromArhive = ZipFB2Worker.getFileFromFB2_FB2Z( ref FilePathIfFromZip, TempDir );
							if( File.Exists( FilePathIfFromZip ) ) {
								FB2InfoList.Add(
									new FB2ItemInfo( item, FilePathSource, FilePathIfFromZip, IsFromArhive )
								);
								FilesWorker.RemoveDir( TempDir );
							}
						}
					}
				}
			}
			return FB2InfoList;
		}
		
		// создание списка ListViewItemInfo данных для помеченных / выделенных книг и каталогов Корректора в корневой папке
		public static IList<ListViewItemInfo> makeListViewItemInfoListForDup( ListView listView,
		                                                                     BooksValidateMode BooksValidateType,
		                                                                     ToolStripProgressBar ProgressBar ) {
			IList<ListViewItemInfo> ListViewItemInfoList = new List<ListViewItemInfo>();
			if( listView.Items.Count > 0 ) {
				string	TempDir = Settings.Settings.TempDir;
				IList Items = null;
				if( BooksValidateType == BooksValidateMode.SelectedBooks )
					Items = listView.SelectedItems;
				else if (BooksValidateType == BooksValidateMode.CheckedBooks)
					Items = listView.CheckedItems;
				else /* BooksValidateType == BooksValidateMode.AllBooks */
					Items = listView.Items;
				
				if( Items.Count > 0 ) {
					ProgressBar.Value = 0;
					ProgressBar.Maximum = Items.Count;
					foreach( ListViewItem item in Items ) {
						if( File.Exists( item.Text.Trim() ) )
							ListViewItemInfoList.Add( new ListViewItemInfo( item, item.Text.Trim() ) );
						ProgressBar.Value++;
					}
				}
			}
			ProgressBar.Value = 0;
			return ListViewItemInfoList;
		}
		
		// создание списка FB2ItemInfo данных для помеченных / выделенных книг Корректора
		public static IList<FB2ItemInfo> makeFB2InfoList( ListView listView, string FB2SourceDir,
		                                                 BooksValidateMode BooksValidateType,
		                                                 ToolStripProgressBar ProgressBar ) {
			IList<FB2ItemInfo> FB2InfoList = new List<FB2ItemInfo>();
			if( listView.Items.Count > 0 ) {
				string	TempDir = Settings.Settings.TempDir;
				IList Items = null;
				if( BooksValidateType == BooksValidateMode.SelectedBooks )
					Items = listView.SelectedItems;
				else if (BooksValidateType == BooksValidateMode.CheckedBooks)
					Items = listView.CheckedItems;
				else /* BooksValidateType == BooksValidateMode.AllBooks */
					Items = listView.Items;
				
				if( Items.Count > 0 ) {
					ProgressBar.Maximum = Items.Count;
					foreach( ListViewItem item in Items ) {
						string FilePathSource = Path.Combine( FB2SourceDir.Trim(), item.Text.Trim() );
						string FilePathIfFromZip = FilePathSource;
						if( File.Exists( FilePathSource ) ) {
							bool IsFromArhive = ZipFB2Worker.getFileFromFB2_FB2Z( ref FilePathIfFromZip, TempDir );
							if( File.Exists( FilePathIfFromZip ) ) {
								try {
									FB2InfoList.Add(
										new FB2ItemInfo( item, FilePathSource, FilePathIfFromZip, IsFromArhive )
									);
								} catch {
									// пропускаем нечитаемые файлы
								} finally {
									FilesWorker.RemoveDir( TempDir );
								}
							}
						}
						++ProgressBar.Value;
					}
				}
			}
			ProgressBar.Value = 0;
			return FB2InfoList;
		}
		
		// создание списка ListViewItemInfo данных для помеченных / выделенных книг и каталогов Корректора в корневой папке
		public static IList<ListViewItemInfo> makeListViewItemInfoList( ListView listView, string FB2SourceRootDir,
		                                                               BooksValidateMode BooksValidateType,
		                                                               ToolStripProgressBar ProgressBar ) {
			IList<ListViewItemInfo> ListViewItemInfoList = new List<ListViewItemInfo>();
			if( listView.Items.Count > 0 ) {
				string	TempDir = Settings.Settings.TempDir;
				IList Items = null;
				if( BooksValidateType == BooksValidateMode.SelectedBooks )
					Items = listView.SelectedItems;
				else if (BooksValidateType == BooksValidateMode.CheckedBooks)
					Items = listView.CheckedItems;
				else /* BooksValidateType == BooksValidateMode.AllBooks */
					Items = listView.Items;
				
				if( Items.Count > 0 ) {
					ProgressBar.Value = 0;
					ProgressBar.Maximum = Items.Count;
					foreach( ListViewItem item in Items ) {
						string PathSource = Path.Combine( FB2SourceRootDir.Trim(), item.Text );
						string ItemType = ((ListViewItemType)item.Tag).Type;
						if( ItemType == "d" ) {
							if ( Directory.Exists( PathSource ) )
								ListViewItemInfoList.Add(
									new ListViewItemInfo( item, PathSource )
								);
						} else if( ItemType == "f" ) {
							if( File.Exists( PathSource ) ) {
								ListViewItemInfoList.Add(
									new ListViewItemInfo( item, PathSource )
								);
							}
						}
						ProgressBar.Value++;
					}
				}
			}
			ProgressBar.Value = 0;
			return ListViewItemInfoList;
		}
		
		// если ли среди помеченных/выделенных итемов папки?
		public static bool checkedDirExsist( IList Items ) {
			if( Items != null ) {
				foreach( ListViewItem item in Items ) {
					if( ((ListViewItemType)item.Tag).Type != "f" )
						return true;
				}
			}
			return false;
		}
		
		// есть ли в списке заданный жанр
		public static bool genreIsExist( ListView listView, Genre g, IFBGenres fb2g ) {
			string Name = !string.IsNullOrEmpty( g.Name )
				? fb2g.GetFBGenreName( g.Name ) + " (" + g.Name + ")"
				: string.Empty;
			string Match = !string.IsNullOrEmpty( g.Math.ToString() ) ? g.Math.ToString() : string.Empty;
			string Genre = Name + Match;
			if ( string.IsNullOrWhiteSpace( Genre ) )
				return true; // не заносим Жанр вообще без данных
			
			foreach ( ListViewItem Item in listView.Items ) {
				string ItemGenre = Item.Text + Item.SubItems[1].Text;
				if ( ItemGenre.Contains( Genre ) )
					return true;
			}
			return false;
		}
		
		// есть ли в списке заданный автор
		public static bool authorIsExist( ListView listView, Author a ) {
			string LastName = string.Empty;
			string FirstName = string.Empty;
			string MiddleName = string.Empty;
			string NickName = string.Empty;
			string HomePages = string.Empty;
			string Emails = string.Empty;
			string ID = string.Empty;
			if( a.LastName != null )
				LastName = !string.IsNullOrEmpty( a.LastName.Value ) ? a.LastName.Value : string.Empty;
			if( a.FirstName != null )
				FirstName = !string.IsNullOrEmpty( a.FirstName.Value ) ? a.FirstName.Value : string.Empty;
			if( a.MiddleName != null )
				MiddleName = !string.IsNullOrEmpty( a.MiddleName.Value ) ? a.MiddleName.Value : string.Empty;
			if( a.NickName != null )
				NickName = !string.IsNullOrEmpty( a.NickName.Value ) ? a.NickName.Value : string.Empty;
			if( a.HomePages != null )
				HomePages = StringProcessing.makeStringFromListItems( a.HomePages );
			if( a.Emails != null )
				Emails = StringProcessing.makeStringFromListItems( a.Emails );
			ID = !string.IsNullOrEmpty( a.ID ) ? a.ID : string.Empty;

			string Author = LastName + FirstName + MiddleName + NickName + HomePages + Emails + ID;
			if ( string.IsNullOrWhiteSpace( Author ) )
				return true; // не заносим Автора вообще без данных
			
			foreach ( ListViewItem Item in listView.Items ) {
				string ItemAuthor = Item.Text + Item.SubItems[1].Text + Item.SubItems[2].Text + Item.SubItems[3].Text + Item.SubItems[4].Text + Item.SubItems[5].Text + Item.SubItems[6].Text;
				if ( ItemAuthor.Contains( Author ) )
					return true;
			}
			return false;
		}

		// итем - файл?
		public static bool isFileItem( ListViewItem listViewItem ) {
			return ((ListViewItemType)listViewItem.Tag).Type == "f" ? true : false;
		}
		
		// пометка цветом и зачеркиванием удаленных книг с диска, но не из списка (быстрый режим удаления)
		public static void markRemoverFileInCopyesList( ListViewItem lvi ) {
			lvi.BackColor = Color.LightGray;
			lvi.Font = new Font(lvi.Font.Name, lvi.Font.Size, FontStyle.Strikeout);
			lvi.Checked = false;
		}
		
		
		// создание пустых subitems для Сортировщика и Корректора
		public static ListViewItem.ListViewSubItem[] createEmptySubItemsForItem( ListViewItem Item ) {
			return new ListViewItem.ListViewSubItem[] {
				new ListViewItem.ListViewSubItem(Item, string.Empty),
				new ListViewItem.ListViewSubItem(Item, string.Empty),
				new ListViewItem.ListViewSubItem(Item, string.Empty),
				new ListViewItem.ListViewSubItem(Item, string.Empty),
				new ListViewItem.ListViewSubItem(Item, string.Empty),
				new ListViewItem.ListViewSubItem(Item, string.Empty),
				new ListViewItem.ListViewSubItem(Item, string.Empty),
				new ListViewItem.ListViewSubItem(Item, string.Empty),
				new ListViewItem.ListViewSubItem(Item, string.Empty),
				new ListViewItem.ListViewSubItem(Item, string.Empty),
				new ListViewItem.ListViewSubItem(Item, string.Empty),
				new ListViewItem.ListViewSubItem(Item, string.Empty),
				new ListViewItem.ListViewSubItem(Item, string.Empty)
			};
		}
		
		// создание заполненных subitems для Сортировщика и Корректора
		public static ListViewItem.ListViewSubItem[]
			createSubItemsWithMetaData( string FilePath, string SourceFileExt,
			                           ListViewItem Item, ref IFBGenres FB2FullSortGenres,
			                           bool IsLibrusecGenres, bool NeedValidate ) {
			FB2BookDescription bd = null;
			try {
				bd = new FB2BookDescription( FilePath );
			} catch ( System.Exception ) {
			}
			
			string valid = "?";
			if(	NeedValidate ) {
				if ( bd != null )
					valid = IsLibrusecGenres ? bd.IsValidFB2Librusec : bd.IsValidFB22;
				else
					valid = ZipFB2Worker.IsValid( FilePath, IsLibrusecGenres );
				if ( !string.IsNullOrEmpty( valid ) ) {
					valid = "Нет";
					Item.ForeColor = Colors.FB2NotValidForeColor;
				} else {
					valid = "Да";
					Item.ForeColor = SourceFileExt == ".fb2" ? Color.FromName( "WindowText" ) : Colors.ZipFB2ForeColor;
				}
			}
			
			return new ListViewItem.ListViewSubItem[] {
				new ListViewItem.ListViewSubItem(Item, bd != null ? bd.TIBookTitle : string.Empty),
				new ListViewItem.ListViewSubItem(Item, bd != null ? bd.TIAuthors : string.Empty),
				new ListViewItem.ListViewSubItem(Item, bd != null ? GenresWorker.cyrillicGenreName( bd.TIGenres, ref FB2FullSortGenres ) : string.Empty),
				new ListViewItem.ListViewSubItem(Item, bd != null ? bd.TISequences : string.Empty),
				new ListViewItem.ListViewSubItem(Item, bd != null ? bd.TILang : string.Empty),
				new ListViewItem.ListViewSubItem(Item, bd != null ? bd.DIID : string.Empty),
				new ListViewItem.ListViewSubItem(Item, bd != null ? bd.DIVersion : string.Empty),
				new ListViewItem.ListViewSubItem(Item, bd != null ? bd.Encoding : string.Empty),
				new ListViewItem.ListViewSubItem(Item, valid),
				new ListViewItem.ListViewSubItem(Item, SourceFileExt),
				new ListViewItem.ListViewSubItem(Item, bd != null ? bd.FileLength : string.Empty),
				new ListViewItem.ListViewSubItem(Item, bd != null ? bd.FileCreationTime : string.Empty),
				new ListViewItem.ListViewSubItem(Item, bd != null ? bd.FileLastWriteTime : string.Empty)
			};
		}
		
		// занесение данных в выделенный итем (метаданные книги после правки) для Корректора
		public static bool viewBookMetaDataLocal( ref FB2BookDescription fb2Desc, ListViewItem lvi,
		                                         bool IsNeedValid, bool IsFB2Librusec, ref IFBGenres FB2Genres ) {
			if( lvi != null ) {
				if( fb2Desc != null ) {
					string fileExtention = Path.GetExtension(fb2Desc.FilePath).ToLower();
					string valid = "?";
					if(	IsNeedValid ) {
						valid = IsFB2Librusec ? fb2Desc.IsValidFB2Librusec : fb2Desc.IsValidFB22;
						if ( string.IsNullOrEmpty( valid )  ) {
							valid = "Да";
							lvi.ForeColor = fileExtention == ".fb2"
								? Color.FromName( "WindowText" )
								: Colors.ZipFB2ForeColor;
						} else {
							valid = "Нет";
							lvi.ForeColor = Colors.FB2NotValidForeColor;
						}
					}
					lvi.SubItems[(int)ResultViewCollumn.BookTitle].Text = fb2Desc.TIBookTitle;
					lvi.SubItems[(int)ResultViewCollumn.Authors].Text = fb2Desc.TIAuthors;
					lvi.SubItems[(int)ResultViewCollumn.Genres].Text = GenresWorker.cyrillicGenreName( fb2Desc.TIGenres, ref FB2Genres );
					lvi.SubItems[(int)ResultViewCollumn.Sequences].Text = fb2Desc.TISequences;
					lvi.SubItems[(int)ResultViewCollumn.Lang].Text = fb2Desc.TILang;
					lvi.SubItems[(int)ResultViewCollumn.BookID].Text = fb2Desc.DIID;
					lvi.SubItems[(int)ResultViewCollumn.Version].Text = fb2Desc.DIVersion;
					lvi.SubItems[(int)ResultViewCollumn.Encoding].Text = fb2Desc.Encoding;
					lvi.SubItems[(int)ResultViewCollumn.Validate].Text = valid;
					lvi.SubItems[(int)ResultViewCollumn.Format].Text = fileExtention;
					lvi.SubItems[(int)ResultViewCollumn.FileLength].Text = fb2Desc.FileLength;
					lvi.SubItems[(int)ResultViewCollumn.CreationTime].Text = fb2Desc.FileCreationTime;
					lvi.SubItems[(int)ResultViewCollumn.LastWriteTime].Text = fb2Desc.FileLastWriteTime;
					
					return true;
				}
			}
			return false;
		}
		// скрыть метаданные итема в Списке для Корректора
		public static void hideMetaDataLocal( ListViewItem Item, bool IsNeedvalidate ) {
			for( int i = 1; i != Item.SubItems.Count; ++i )
				Item.SubItems[i].Text = string.Empty;
			Item.ForeColor = Colors.FB2NotValidForeColor;
			Item.SubItems[(int)ResultViewCollumn.Validate].Text = IsNeedvalidate ? "Нет" : "?";
			Item.SubItems[(int)ResultViewCollumn.Format].Text = Item.Text.Substring(Item.Text.LastIndexOf('.'));
		}
		// скрыть метаданные итема в Списке для Дубликатора
		public static void hideMetaDataLocalForDup( ListViewItem Item, bool IsNeedvalidate ) {
			for( int i = 1; i != Item.SubItems.Count; ++i )
				Item.SubItems[i].Text = string.Empty;
			Item.ForeColor = Colors.FB2NotValidForeColor;
			Item.SubItems[(int)ResultViewDupCollumn.Validate].Text = IsNeedvalidate ? "Нет" : "?";
		}
		
		// показать данные книги для Сортировщика
		public static void viewBookMetaDataLocal( string FilePath, ListView listViewSource, IFBGenres FB2FullSortGenres,
		                                         int ItemNumber, string TempDir, bool IsLibrusecGenres, bool NeedValidate ) {
			string valid = "?";
			FB2BookDescription bd = null;
			string FileExtension = Path.GetExtension( FilePath ).ToLower();
			try {
				if( FileExtension == ".fb2" ) {
					// показать данные fb2 файлов
					bd = new FB2BookDescription( FilePath );
					if(	NeedValidate ) {
						valid = IsLibrusecGenres ? bd.IsValidFB2Librusec : bd.IsValidFB22;
						if ( !string.IsNullOrEmpty( valid ) ) {
							valid = "Нет";
							listViewSource.Items[ItemNumber].ForeColor = Colors.FB2NotValidForeColor;
						} else {
							valid = "Да";
							listViewSource.ForeColor = FileExtension == ".fb2"
								? Color.FromName( "WindowText" )
								: Colors.ZipFB2ForeColor;
						}
					}
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumn.BookTitle].Text = bd.TIBookTitle;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumn.Authors].Text = bd.TIAuthors;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumn.Genres].Text = GenresWorker.cyrillicGenreName(
						bd.TIGenres, ref FB2FullSortGenres
					);
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumn.Sequences].Text = bd.TISequences;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumn.Lang].Text = bd.TILang;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumn.BookID].Text = bd.DIID;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumn.Version].Text = bd.DIVersion;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumn.Encoding].Text = bd.Encoding;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumn.Validate].Text = valid;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumn.Format].Text = FileExtension;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumn.FileLength].Text = bd.FileLength;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumn.CreationTime].Text = bd.FileCreationTime;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumn.LastWriteTime].Text = bd.FileLastWriteTime;
				} else if( FilesWorker.isFB2Archive( FilePath ) ) {
					// показать данные архивов
					FilesWorker.RemoveDir( TempDir );
					SharpZipLibWorker sharpZipLib = new SharpZipLibWorker();
					sharpZipLib.UnZipFiles( FilePath, TempDir, 0, true, null, 4096 );
					string [] files = Directory.GetFiles( TempDir );
					bd = new FB2BookDescription( files[0] );
					if(	NeedValidate ) {
						valid = IsLibrusecGenres ? bd.IsValidFB2Librusec : bd.IsValidFB22;
						if ( !string.IsNullOrEmpty( valid ) ) {
							valid = "Нет";
							listViewSource.Items[ItemNumber].ForeColor = Colors.FB2NotValidForeColor;
						} else {
							valid = "Да";
							listViewSource.Items[ItemNumber].ForeColor = FileExtension == ".fb2"
								? Color.FromName( "WindowText" )
								: Colors.ZipFB2ForeColor;
						}
					}
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumn.BookTitle].Text = bd.TIBookTitle;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumn.Authors].Text = bd.TIAuthors;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumn.Genres].Text = GenresWorker.cyrillicGenreName(
						bd.TIGenres, ref FB2FullSortGenres
					);
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumn.Sequences].Text = bd.TISequences;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumn.Lang].Text = bd.TILang;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumn.BookID].Text = bd.DIID;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumn.Version].Text = bd.DIVersion;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumn.Encoding].Text = bd.Encoding;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumn.Validate].Text = valid;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumn.Format].Text = FileExtension;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumn.FileLength].Text = bd.FileLength;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumn.CreationTime].Text = bd.FileCreationTime;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumn.LastWriteTime].Text = bd.FileLastWriteTime;
				}
			} catch( System.Exception ) {
				listViewSource.Items[ItemNumber].ForeColor = Colors.BadZipForeColor;
			} finally {
				FilesWorker.RemoveDir( TempDir );
			}
		}
		
		/// <summary>
		/// генерация списка файлов - создание итемов listViewSource
		/// </summary>
		/// <param name="listView">ListView для формирования списка items</param>
		/// <param name="dirPath">Исходная папка сканирования книг для генерации списка их данных</param>
		/// <param name="fb2g">Список Жанров IFBGenres</param>
		/// <param name="IsLibrusecGenres">true - Схема Жанров от Либрусек</param>
		/// <param name="isTagsView">Нужно ли отображатьметаданные книг в списке</param>
		/// <param name="itemChecked">Нужно ли ставить пометку на созданный ListLiewItem</param>
		/// <param name="NeedValidate">Нужно ли проводить валидацию книги</param>
		/// <param name="AutoResizeColumns">Нужно ли проводить авторазмер колонок списка</param>
		/// <param name="form">Форма прогреса Windows.Form (null, если не используется)</param>
		/// <param name="ProgressBar">Прогресc Windows.ProgressBar.ProgressBar (null, если не используется)</param>
		/// <param name="bw">Фоновый обработчик BackgroundWorker (null, если не используется)</param>
		/// <param name="e">DoWorkEventArgs (null, если не используется)</param>
		/// <returns>true - список сформирован; false - прерывание генерации списка</returns>
		public static bool generateBooksListWithMetaData( ListView listView, string dirPath, ref IFBGenres fb2g,
		                                                 bool IsLibrusecGenres, bool isTagsView, bool itemChecked,
		                                                 bool NeedValidate, bool AutoResizeColumns,
		                                                 Form form = null, ProgressBar ProgressBar = null,
		                                                 BackgroundWorker bw = null, DoWorkEventArgs e = null ) {
			if ( !Directory.Exists( dirPath ) )
				return false;
			
			Cursor.Current = Cursors.WaitCursor;
			listView.BeginUpdate();
			listView.Items.Clear();
			string TempDir = Settings.Settings.TempDir;
			try {
				DirectoryInfo dirInfo = new DirectoryInfo( dirPath );
				ListViewItem.ListViewSubItem[] subItems;
				ListViewItem item = null;
				if( dirInfo.Exists ) {
					if( dirInfo.Parent != null ) {
						item = new ListViewItem( "..", 3) ;
						item.Tag = new ListViewItemType( "dUp", dirInfo.Parent.FullName );
						subItems = createEmptySubItemsForItem( item );
						item.SubItems.AddRange( subItems );
						listView.Items.Add( item );
					}
					int nItemCount = 0;
					
					if ( form != null ) {
						form.Text += String.Format(
							": {0} каталогов; {1} файлов", dirInfo.GetDirectories().Length, dirInfo.GetFiles().Length
						);
						ProgressBar.Maximum	= ( dirInfo.GetDirectories().Length + dirInfo.GetFiles().Length ) + 1;
					}
					
					foreach( DirectoryInfo dir in dirInfo.GetDirectories() ) {
						if ( bw != null ) {
							if( ( bw.CancellationPending ) ) {
								e.Cancel = true;
								listView.EndUpdate();
								Cursor.Current = Cursors.Default;
								return false;
							}
						}
						item = new ListViewItem( dir.Name, 0);
						item.Checked = itemChecked;
						item.Tag = new ListViewItemType("d", dir.FullName);
						item.BackColor = Colors.DirBackColor;
						
						subItems = createEmptySubItemsForItem( item );
						item.SubItems.AddRange( subItems );
						listView.Items.Add( item );
						if ( bw != null ) {
							bw.ReportProgress( nItemCount );
							++nItemCount;
						}
					}
					
					SharpZipLibWorker sharpZipLib = new SharpZipLibWorker();
					foreach( FileInfo file in dirInfo.GetFiles() ) {
						if ( bw != null ) {
							if( ( bw.CancellationPending ) ) {
								e.Cancel = true;
								listView.EndUpdate();
								Cursor.Current = Cursors.Default;
								return false;
							}
						}
						string Ext = file.Extension.ToLower();
						if( Ext == ".fb2" || Ext == ".zip" || Ext == ".fbz" ) {
							item = new ListViewItem( file.Name, Ext == ".fb2" ? 1 : 2 );
							try {
								if( Ext == ".fb2" ) {
									if( isTagsView ) {
										subItems = createSubItemsWithMetaData(
											file.FullName, Ext, item, ref fb2g, IsLibrusecGenres,
											NeedValidate
										);
									} else
										subItems = createEmptySubItemsForItem( item );
								} else {
									// для zip-архивов
									if( isTagsView ) {
										FilesWorker.RemoveDir( TempDir );
										sharpZipLib.UnZipFiles( file.FullName, TempDir, 0, false, null, 4096 );
										string [] files = Directory.GetFiles( TempDir );
										Ext = Path.GetExtension( files[0] ).ToLower();
										if ( Ext == ".fb2") {
											subItems = createSubItemsWithMetaData(
												files[0], Ext, item, ref fb2g, IsLibrusecGenres,
												NeedValidate
											);
											item.ForeColor = Colors.ZipFB2ForeColor;
										} else {
											subItems = createEmptySubItemsForItem( item );
											item.ForeColor = Colors.BadZipForeColor;
										}
									} else
										subItems = createEmptySubItemsForItem( item );
								}
								item.SubItems.AddRange(subItems);
							} catch(System.Exception) {
								subItems = createEmptySubItemsForItem( item );
								item.SubItems.AddRange(subItems);
								item.ForeColor = Colors.BadZipForeColor;
							}
							
							item.Checked = itemChecked;
							item.Tag = new ListViewItemType( "f", file.FullName );
							item.BackColor = Colors.FileBackColor;

							listView.Items.Add(item);
							if ( bw != null ) {
								bw.ReportProgress( nItemCount );
								++nItemCount;
							}
						}
					}
					if ( AutoResizeColumns ) {
						// авторазмер колонок Списка Проводника
						MiscListView.AutoResizeColumns( listView );
					}
				}
			} catch (System.Exception) {
			} finally {
				listView.EndUpdate();
				FilesWorker.RemoveDir( TempDir );
				Cursor.Current = Cursors.Default;
			}
			return true;
		}
		
		/// <summary>
		/// отображение/скрытие метаданных данных книг в Списке Сортировщика
		/// </summary>
		/// <param name="listViewFB2Files">ListView для формирования списка items</param>
		/// <param name="fb2g">Список Жанров IFBGenres</param>
		/// <param name="IsLibrusecGenres">true - Схема Жанров от Либрусек</param>
		/// <param name="isTagsView">Нужно ли отображатьметаданные книг в списке</param>
		/// <param name="NeedValidate">Нужно ли проводить валидацию книги</param>
		/// <param name="bw">Фоновый обработчик BackgroundWorker (null, если не используется)</param>
		/// <param name="e">DoWorkEventArgs (null, если не используется)</param>
		/// <returns>true - список сформирован; false - прерывание генерации списка</returns>
		public static bool viewOrHideBookMetaDataLocal( ListView listViewFB2Files, ref IFBGenres fb2g,
		                                               bool IsLibrusecGenres, bool isTagsView, bool NeedValidate,
		                                               BackgroundWorker bw = null, DoWorkEventArgs e = null ) {
			ListViewItemType it	= null;
			string TempDir = Settings.Settings.TempDir;
			listViewFB2Files.BeginUpdate();
			for( int i = 0; i != listViewFB2Files.Items.Count; ++i ) {
				if ( bw != null ) {
					if( ( bw.CancellationPending ) ) {
						e.Cancel = true;
						return false;
					}
				}
				it = (ListViewItemType)listViewFB2Files.Items[i].Tag;
				if( it.Type == "f" ) {
					if( isTagsView ) {
						// показать данные книг
						viewBookMetaDataLocal(
							it.Value, listViewFB2Files, fb2g, i, TempDir, IsLibrusecGenres, NeedValidate
						);
					} else {
						// скрыть данные книг
						for( int j = 1; j != listViewFB2Files.Items[i].SubItems.Count; ++j ) {
							if ( bw != null ) {
								if( ( bw.CancellationPending ) ) {
									e.Cancel = true;
									return false;
								}
							}
							listViewFB2Files.Items[i].SubItems[j].Text = string.Empty;
						}
					}
				}
				if ( bw != null ) {
					bw.ReportProgress( i );
				}
			}
			listViewFB2Files.EndUpdate();
			return true;
		}
		
		// форма ввода строки данных
		public static DialogResult InputBox(string title, string promptText, ref string value) {
			Form form = new Form();
			Label label = new Label();
			TextBox textBox = new TextBox();
			Button buttonOk = new Button();
			Button buttonCancel = new Button();

			form.Text = title;
			label.Text = promptText;
			textBox.Text = value;

			buttonOk.Text = "OK";
			buttonCancel.Text = "Cancel";
			buttonOk.DialogResult = DialogResult.OK;
			buttonCancel.DialogResult = DialogResult.Cancel;

			label.SetBounds(9, 20, 372, 13);
			textBox.SetBounds(12, 36, 372, 20);
			buttonOk.SetBounds(228, 72, 75, 23);
			buttonCancel.SetBounds(309, 72, 75, 23);

			label.AutoSize = true;
			textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
			buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
			buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

			form.ClientSize = new Size(396, 107);
			form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
			form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
			form.FormBorderStyle = FormBorderStyle.FixedDialog;
			form.StartPosition = FormStartPosition.CenterScreen;
			form.MinimizeBox = false;
			form.MaximizeBox = false;
			form.AcceptButton = buttonOk;
			form.CancelButton = buttonCancel;

			DialogResult dialogResult = form.ShowDialog();
			value = textBox.Text;
			return dialogResult;
		}

		// восстановление структуры description fb2 файла
		public static bool recoveryFB2Structure( ref FB2Corrector fB2Corrector, ListViewItem lvi ) {
			if (fB2Corrector.recoveryDescriptionNode() ) {
				lvi.ForeColor = Color.FromName( "WindowText" );
				return true;
			}
			return false;
		}

		// формирование Списка Групп Жанров
		public static void makeListGenresGroups( ComboBox GenresGroupComboBox, bool IsFB2Libruser ) {
			IGenresGroup GenresGroup = new GenresGroup();
			GenresGroupComboBox.Items.Clear();
			GenresGroupComboBox.Items.Add( GenresGroup.GenresGroupSf );
			GenresGroupComboBox.Items.Add( GenresGroup.GenresGroupDetective );
			GenresGroupComboBox.Items.Add( GenresGroup.GenresGroupProse );
			GenresGroupComboBox.Items.Add( GenresGroup.GenresGroupLove );
			GenresGroupComboBox.Items.Add( GenresGroup.GenresGroupAdventure );
			GenresGroupComboBox.Items.Add( GenresGroup.GenresGroupChildren );
			GenresGroupComboBox.Items.Add( GenresGroup.GenresGroupPoetry );
			GenresGroupComboBox.Items.Add( GenresGroup.GenresGroupAntique );
			GenresGroupComboBox.Items.Add( GenresGroup.GenresGroupScience );
			GenresGroupComboBox.Items.Add( GenresGroup.GenresGroupComputers );
			GenresGroupComboBox.Items.Add( GenresGroup.GenresGroupReference );
			GenresGroupComboBox.Items.Add( GenresGroup.GenresGroupNonfiction );
			GenresGroupComboBox.Items.Add( GenresGroup.GenresGroupReligion );
			GenresGroupComboBox.Items.Add( GenresGroup.GenresGroupHumor );
			GenresGroupComboBox.Items.Add( GenresGroup.GenresGroupHome );
			GenresGroupComboBox.Items.Add( GenresGroup.GenresGroupBusiness );
			if( IsFB2Libruser ) {
				GenresGroupComboBox.Items.Add( GenresGroup.GenresGroupTech );
				GenresGroupComboBox.Items.Add( GenresGroup.GenresGroupMilitary );
				GenresGroupComboBox.Items.Add( GenresGroup.GenresGroupFolklore );
				GenresGroupComboBox.Items.Add( GenresGroup.GenresGroupOther );
			}
			
			GenresGroupComboBox.SelectedIndex = 0;
		}
		
		// формирование Списка Жанров в контролы, в зависимости от Группы (только коды Жанров)
		public static void makeListCodeGenres( ComboBox GenresComboBox, bool IsFB2Libruser, string GenreGroup ) {
			GenresComboBox.Items.Clear();
			IGenresGroup GenresGroup = new GenresGroup();
			IFBGenres fb2g = GenresWorker.genresListOfGenreSheme( IsFB2Libruser, ref GenresGroup );
			GenresComboBox.Items.AddRange( fb2g.GetFBGenreCodesArrayForGroup( GenreGroup ) );
			GenresComboBox.SelectedIndex = 0;
		}
		
		// формирование Списка Жанров в контролы, в зависимости от Группы (и коды и расшифровка Жанров)
		public static void makeListGenres( ComboBox GenresComboBox, bool IsFB2Libruser, string GenreGroup ) {
			GenresComboBox.Items.Clear();
			IGenresGroup GenresGroup = new GenresGroup();
			IFBGenres fb2g = GenresWorker.genresListOfGenreSheme( IsFB2Libruser, ref GenresGroup );
			string[] Codes = fb2g.GetFBGenreCodesArrayForGroup( GenreGroup );
			for( int i = 0; i != Codes.Length; ++i )
				GenresComboBox.Items.Add( fb2g.GetFBGenreName( Codes[i] ) + " (" + Codes[i] + ")" );

			GenresComboBox.SelectedIndex = 0;
		}
		
		// формирование Списка всех Жанров Схемы в контролы
		public static void makeListAllGenres( ComboBox GenresComboBox, bool IsFB2Libruser ) {
			GenresComboBox.Items.Clear();
			IGenresGroup GenresGroup = new GenresGroup();
			IFBGenres fb2g = GenresWorker.genresListOfGenreSheme( IsFB2Libruser, ref GenresGroup );
			string[] sGenresNames	= fb2g.GetFBGenreNamesArray();
			string[] sCodes			= fb2g.GetFBGenreCodesArray();
			
			for( int i = 0; i != sGenresNames.Length; ++i )
				GenresComboBox.Items.Add( sGenresNames[i] + " (" + sCodes[i] + ")" );
			GenresComboBox.SelectedIndex = 0;
		}
		
		// отображение обложки
		public static void viewCover( ListView CoversListView, PictureBox picBox,
		                             Label CoverDPILabel, Label CoverPixelsLabel, Label CoverLenghtLabel ) {
			if( CoversListView.SelectedItems.Count > 0 ) {
				try {
					picBox.Image = ImageWorker.base64ToImage(
						CoversListView.SelectedItems[0].Tag.ToString()
					);
					CoverDPILabel.Text = CoversListView.SelectedItems[0].SubItems[2].Text;
					CoverPixelsLabel.Text = CoversListView.SelectedItems[0].SubItems[3].Text;
					CoverLenghtLabel.Text = CoversListView.SelectedItems[0].SubItems[4].Text;
				} catch ( System.Exception error ) {
					MessageBox.Show(
						error.Message, "Ошибка отображения картинки", MessageBoxButtons.OK, MessageBoxIcon.Error
					);
				}
			}
		}
		
		// автокорректировка всех выделеннеых/помеченных книг для Корректора и Дубликатора
		// OneBook = false - обработка для нескольких книг в цикле вызывающего кода
		public static void autoCorrect( ListViewItem Item, string SrcFilePath,
		                               bool IsFB2Librusec, bool IsNeedValid, bool OneBook, SharpZipLibWorker sharpZipLib ) {
			string SourceFilePath = SrcFilePath;
			string FilePath = SourceFilePath;
			bool IsFromZip = ZipFB2Worker.getFileFromFB2_FB2Z( ref FilePath, Settings.Settings.TempDir );
			
			FB2Corrector.autoCorrector( SourceFilePath );
			
			FictionBook fb2 = null;
			try {
				fb2 = new FictionBook( FilePath );
			} catch {
				if ( OneBook )
					MessageBox.Show( "Файл \""+FilePath+"\" невозможно открыть для извлечения fb2 метаданных!",
					                "Автокорректировка", MessageBoxButtons.OK, MessageBoxIcon.Error );
				return;
			}
			
			if( fb2 != null ) {
				// восстанавление раздела description до структуры с необходимыми элементами для валидности
				FB2Corrector fB2Corrector = new FB2Corrector( ref fb2 );
				WorksWithBooks.recoveryFB2Structure( ref fB2Corrector, Item );
				fB2Corrector.saveToFB2File( FilePath );
				
				if( IsFromZip ) {
					// обработка исправленного файла-архива
					string ArchFile = FilePath + ".zip";
					sharpZipLib.ZipFile( FilePath, ArchFile, 9, ICSharpCode.SharpZipLib.Zip.CompressionMethod.Deflated, 4096 );
					if( File.Exists( SourceFilePath ) )
						File.Delete( SourceFilePath );
					File.Move( ArchFile, SourceFilePath );
				}

				// отображение новых данных в строке списка
				if( IsFromZip )
					ZipFB2Worker.getFileFromFB2_FB2Z( ref SourceFilePath, Settings.Settings.TempDir );
				// спиок жанров, в зависимости от схемы Жанров
				IGenresGroup GenresGroup = new GenresGroup();
				IFBGenres fb2g = GenresWorker.genresListOfGenreSheme( IsFB2Librusec, ref GenresGroup );
				try {
					FB2BookDescription fb2Corrected = new FB2BookDescription( SourceFilePath );
					viewBookMetaDataLocal(
						ref fb2Corrected, Item, IsNeedValid, IsFB2Librusec, ref fb2g
					);
					FilesWorker.RemoveDir( Settings.Settings.TempDir );
				} catch ( System.Exception ) {
					FilesWorker.RemoveDir( Settings.Settings.TempDir );
				}
			}
		}
		
		
		// автокорректировка всех  книг для Корректора
		public static void autoCorrect( string SrcFilePath, SharpZipLibWorker sharpZipLib ) {
			string SourceFilePath = SrcFilePath;
			string FilePath = SourceFilePath;
			bool IsFromZip = ZipFB2Worker.getFileFromFB2_FB2Z( ref FilePath, Settings.Settings.TempDir );

			FB2Corrector.autoCorrector( FilePath );
			
			FictionBook fb2 = null;
			try {
				fb2 = new FictionBook( FilePath );
			} catch {
				return;
			}
			
			if( fb2 != null ) {
				// восстанавление раздела description до структуры с необходимыми элементами для валидности
				FB2Corrector fB2Corrector = new FB2Corrector( ref fb2 );
				fB2Corrector.recoveryDescriptionNode();
				fB2Corrector.saveToFB2File( FilePath );
				
				if( IsFromZip ) {
					// обработка исправленного файла-архива
					string ArchFile = FilePath + ".zip";
					sharpZipLib.ZipFile( FilePath, ArchFile, 9, ICSharpCode.SharpZipLib.Zip.CompressionMethod.Deflated, 4096 );
					if( File.Exists( SourceFilePath ) )
						File.Delete( SourceFilePath );
					File.Move( ArchFile, SourceFilePath );
				}

			}
		}
		
	}
}
