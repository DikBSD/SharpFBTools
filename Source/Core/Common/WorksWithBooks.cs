/*
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
using ResultViewCollumn = Core.Common.Enums.ResultViewCollumn;

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
		public static IList<FB2ItemInfo> makeFB2InfoListForDup( ListView listView, bool IsSelectedItems ) {
			IList<FB2ItemInfo> FB2InfoList = new List<FB2ItemInfo>();
			if( listView.Items.Count > 0 ) {
				string	TempDir = Settings.Settings.TempDir;
				IList Items = null;
				if( IsSelectedItems )
					Items = listView.SelectedItems;
				else
					Items = listView.CheckedItems;
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
		
		// создание списка FB2ItemInfo данных для помеченных книг Корректора
		public static IList<FB2ItemInfo> makeFB2InfoList( ListView listView, string FB2SourceDir, bool IsSelectedItems,
		                                                 ToolStripProgressBar ProgressBar ) {
			IList<FB2ItemInfo> FB2InfoList = new List<FB2ItemInfo>();
			if( listView.Items.Count > 0 ) {
				string	TempDir = Settings.Settings.TempDir;
				IList Items = null;
				if( IsSelectedItems )
					Items = listView.SelectedItems;
				else
					Items = listView.CheckedItems;
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
		
		// если ли среди помеченных/выделенных итемов папки?
		public static bool chechedDirExsist( IList Items ) {
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
		public static void MarkRemoverFileInCopyesList( ListViewItem lvi ) {
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
		public static bool viewBookMetaDataLocal( ref FB2BookDescription bd, ListViewItem lvi,
		                                         bool IsNeedValid, bool IsFB2Librusec, ref IFBGenres FB2Genres ) {
			if( lvi != null ) {
				if( bd != null ) {
					string valid = "?";
					if(	IsNeedValid ) {
						valid = IsFB2Librusec ? bd.IsValidFB2Librusec : bd.IsValidFB22;
						if ( string.IsNullOrEmpty( valid )  ) {
							valid = "Да";
						} else {
							valid = "Нет";
							lvi.ForeColor = Colors.FB2NotValidForeColor;
						}
					}
					lvi.SubItems[(int)ResultViewCollumn.BookTitle].Text = bd.TIBookTitle;
					lvi.SubItems[(int)ResultViewCollumn.Authors].Text = bd.TIAuthors;
					lvi.SubItems[(int)ResultViewCollumn.Genres].Text = GenresWorker.cyrillicGenreName( bd.TIGenres, ref FB2Genres );
					lvi.SubItems[(int)ResultViewCollumn.Sequences].Text = bd.TISequences;
					lvi.SubItems[(int)ResultViewCollumn.Lang].Text = bd.TILang;
					lvi.SubItems[(int)ResultViewCollumn.BookID].Text = bd.DIID;
					lvi.SubItems[(int)ResultViewCollumn.Version].Text = bd.DIVersion;
					lvi.SubItems[(int)ResultViewCollumn.Encoding].Text = bd.Encoding;
					lvi.SubItems[(int)ResultViewCollumn.Validate].Text = valid;
					// Format не изменился :) - не отображаем его заново
					lvi.SubItems[(int)ResultViewCollumn.FileLength].Text = bd.FileLength;
					lvi.SubItems[(int)ResultViewCollumn.CreationTime].Text = bd.FileCreationTime;
					lvi.SubItems[(int)ResultViewCollumn.LastWriteTime].Text = bd.FileLastWriteTime;
					
					return true;
				}
			}
			return false;
		}
		
		// показать данные книги для Сортировщика
		public static void viewBookMetaDataLocal( string FilePath, string FileExtension,
		                                         ListView listViewSource, IFBGenres FB2FullSortGenres,
		                                         int ItemNumber, string TempDir, bool IsLibrusecGenres, bool NeedValidate ) {
			string valid = "?";
			FB2BookDescription bd = null;
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
				} else if( FileExtension == ".zip" || FileExtension == ".fbz" ) {
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
						subItems = WorksWithBooks.createEmptySubItemsForItem( item );
						item.SubItems.AddRange( subItems );
						listView.Items.Add( item );
					}
					int nItemCount = 0;
					
					if ( form != null ) {
						form.Text += ": " + dirInfo.GetFiles().Length.ToString() + " файлов";
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
						
						subItems = WorksWithBooks.createEmptySubItemsForItem( item );
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
										subItems = WorksWithBooks.createSubItemsWithMetaData(
											file.FullName, Ext, item, ref fb2g, IsLibrusecGenres,
											NeedValidate
										);
									} else
										subItems = WorksWithBooks.createEmptySubItemsForItem( item );
								} else {
									// для zip-архивов
									if( isTagsView ) {
										FilesWorker.RemoveDir( TempDir );
										sharpZipLib.UnZipFiles( file.FullName, TempDir, 0, false, null, 4096 );
										string [] files = Directory.GetFiles( TempDir );
										subItems = WorksWithBooks.createSubItemsWithMetaData(
											files[0], Ext, item, ref fb2g, IsLibrusecGenres,
											NeedValidate
										);
										item.ForeColor = Colors.ZipFB2ForeColor;
									} else
										subItems = WorksWithBooks.createEmptySubItemsForItem( item );
								}
								item.SubItems.AddRange(subItems);
							} catch(System.Exception) {
								subItems = WorksWithBooks.createEmptySubItemsForItem( item );
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
			DirectoryInfo di	= null;
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
					di = new DirectoryInfo( it.Value );
					if( isTagsView ) {
						// показать данные книг
						WorksWithBooks.viewBookMetaDataLocal(
							it.Value, di.Extension, listViewFB2Files,
							fb2g, i, TempDir, IsLibrusecGenres, NeedValidate
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
		public static bool recoveryFB2Structure( ref FictionBook fb2, ListViewItem lvi ) {
			if (fb2.recoveryDescriptionNode() ) {
				lvi.ForeColor = Color.FromName( "WindowText" );
				return true;
			}
			return false;
		}

		// формирование Списка Групп Жанров
		public static void makeListFMGenresGroups( ComboBox comboBox, bool IsFB2Libruser ) {
			comboBox.Items.Clear();
			comboBox.Items.Add( "ФАНТАСТИКА, ФЭНТЕЗИ" );
			comboBox.Items.Add( "ДЕТЕКТИВЫ, БОЕВИКИ" );
			comboBox.Items.Add( "ПРОЗА" );
			comboBox.Items.Add( "ЛЮБОВНЫЕ РОМАНЫ" );
			comboBox.Items.Add( "ПРИКЛЮЧЕНИЯ" );
			comboBox.Items.Add( "ДЕТСКОЕ" );
			comboBox.Items.Add( "ПОЭЗИЯ, ДРАМАТУРГИЯ" );
			comboBox.Items.Add( "СТАРИННОЕ" );
			comboBox.Items.Add( "НАУКА, ОБРАЗОВАНИЕ" );
			comboBox.Items.Add( "КОМПЬЮТЕРЫ" );
			comboBox.Items.Add( "СПРАВОЧНИКИ" );
			comboBox.Items.Add( "ДОКУМЕНТАЛЬНОЕ" );
			comboBox.Items.Add( "РЕЛИГИЯ" );
			comboBox.Items.Add( "ЮМОР" );
			comboBox.Items.Add( "ДОМ, СЕМЬЯ" );
			comboBox.Items.Add( "БИЗНЕС" );
			if( IsFB2Libruser ) {
				comboBox.Items.Add( "ТЕХНИКА" );
				comboBox.Items.Add( "ВОЕННОЕ ДЕЛО" );
				comboBox.Items.Add( "ФОЛЬКЛЕР" );
				comboBox.Items.Add( "ПРОЧЕЕ" );
			}
			
			comboBox.SelectedIndex = 0;
		}
		
	}
}
