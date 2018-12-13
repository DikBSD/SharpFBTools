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
using System.Xml;
using System.Linq;
using System.Xml.Linq;

using Core.FB2.Genres;
using Core.FB2.Description.TitleInfo;
using Core.FB2.Description.Common;
using Core.FB2.FB2Parsers;
using Core.AutoCorrector;

using FB2Validator = Core.FB2Parser.FB2Validator;

// enums
using ResultViewCollumnEnum					= Core.Common.Enums.ResultViewCollumnEnum;
using ResultViewDupCollumnEnum				= Core.Common.Enums.ResultViewDupCollumnEnum;
using BooksValidateModeEnum					= Core.Common.Enums.BooksValidateModeEnum;
using BooksAutoCorrectProcessingModeEnum	= Core.Common.Enums.BooksAutoCorrectProcessingModeEnum;

namespace Core.Common
{
	/// <summary>
	/// Общий класс работы с помеченными книгами для всех инструментов
	/// </summary>
	public class WorksWithBooks
	{
		private readonly static FB2Validator m_fv2Validator	= new FB2Validator();
		
		public WorksWithBooks()
		{
		}
		
		/// <summary>
		/// Создание списка FB2ItemInfo данных для помеченных книг Дубликатора
		/// </summary>
		/// <param name="listView">Список книг класса ListView</param>
		/// <param name="BooksValidateType">Режим валидации книг</param>
		/// <returns>Список элементов, типа FB2ItemInfo</returns>
		public static IList<FB2ItemInfo> makeFB2InfoListForDup( ListView listView,
		                                                       BooksValidateModeEnum BooksValidateType ) {
			IList<FB2ItemInfo> FB2InfoList = new List<FB2ItemInfo>();
			if ( listView.Items.Count > 0 ) {
				string TempDir = Settings.Settings.TempDirPath;
				IList Items = null;
				if ( BooksValidateType == BooksValidateModeEnum.SelectedBooks )
					Items = listView.SelectedItems;
				else if ( BooksValidateType == BooksValidateModeEnum.CheckedBooks )
					Items = listView.CheckedItems;
				else /* BooksValidateType == BooksValidateMode.AllBooks */
					Items = listView.Items;
				
				if ( Items.Count > 0 ) {
					foreach ( ListViewItem item in Items ) {
						string SourceFilePath = item.Text.Trim();
						string FilePathIfFromZip = SourceFilePath;
						if ( File.Exists( SourceFilePath ) ) {
							bool IsFromZip = false;
							if ( FilesWorker.isFB2Archive( SourceFilePath ) ) {
								IsFromZip = true;
								FilePathIfFromZip = ZipFB2Worker.getFileFromZipFBZ( SourceFilePath, TempDir );
							}
							if ( File.Exists( FilePathIfFromZip ) ) {
								FB2InfoList.Add(
									new FB2ItemInfo( item, SourceFilePath, FilePathIfFromZip, IsFromZip )
								);
								FilesWorker.RemoveDir( TempDir );
							}
						}
					}
				}
			}
			return FB2InfoList;
		}
		
		/// <summary>
		/// Создание списка ListViewItemInfo данных для помеченных / выделенных книг и каталогов Корректора в корневой папке
		/// </summary>
		/// <param name="listView">Список книг класса ListView</param>
		/// <param name="BooksValidateType">Режим валидации книг</param>
		/// <param name="ProgressBar">Прогресс класса ToolStripProgressBar</param>
		/// <returns>Список элементов, типа ListViewItemInfo</returns>
		public static IList<ListViewItemInfo> makeListViewItemInfoListForDup( ListView listView,
		                                                                     BooksValidateModeEnum BooksValidateType,
		                                                                     ToolStripProgressBar ProgressBar ) {
			IList<ListViewItemInfo> ListViewItemInfoList = new List<ListViewItemInfo>();
			if( listView.Items.Count > 0 ) {
				string	TempDir = Settings.Settings.TempDirPath;
				IList Items = null;
				if( BooksValidateType == BooksValidateModeEnum.SelectedBooks )
					Items = listView.SelectedItems;
				else if (BooksValidateType == BooksValidateModeEnum.CheckedBooks)
					Items = listView.CheckedItems;
				else /* BooksValidateType == BooksValidateMode.AllBooks */
					Items = listView.Items;
				
				if( Items.Count > 0 ) {
					ProgressBar.Value = 0;
					ProgressBar.Maximum = Items.Count;
					foreach( ListViewItem item in Items ) {
						if( File.Exists( item.Text.Trim() ) )
							ListViewItemInfoList.Add(
								new ListViewItemInfo( item, item.Text.Trim() )
							);
						ProgressBar.Value++;
					}
				}
			}
			ProgressBar.Value = 0;
			return ListViewItemInfoList;
		}
		
		/// <summary>
		/// Создание списка FB2ItemInfo данных для помеченных / выделенных книг Корректора
		/// </summary>
		/// <param name="listView">Список книг класса ListView</param>
		/// <param name="FB2SourceDir">Папка с книгами</param>
		/// <param name="BooksValidateType">Режим валидации книг</param>
		/// <param name="ProgressBar">Прогресс класса ToolStripProgressBar</param>
		/// <returns>Список элементов, типа FB2ItemInfo</returns>
		public static IList<FB2ItemInfo> makeFB2InfoList( ListView listView, string FB2SourceDir,
		                                                 BooksValidateModeEnum BooksValidateType,
		                                                 ToolStripProgressBar ProgressBar ) {
			IList<FB2ItemInfo> FB2InfoList = new List<FB2ItemInfo>();
			if ( listView.Items.Count > 0 ) {
				string	TempDir = Settings.Settings.TempDirPath;
				IList Items = null;
				if ( BooksValidateType == BooksValidateModeEnum.SelectedBooks )
					Items = listView.SelectedItems;
				else if (BooksValidateType == BooksValidateModeEnum.CheckedBooks)
					Items = listView.CheckedItems;
				else /* BooksValidateType == BooksValidateMode.AllBooks */
					Items = listView.Items;
				
				if ( Items.Count > 0 ) {
					ProgressBar.Maximum = Items.Count;
					foreach ( ListViewItem item in Items ) {
						string SourceFilePath = Path.Combine( FB2SourceDir.Trim(), item.Text.Trim() );
						string FilePathIfFromZip = SourceFilePath;
						if ( File.Exists( SourceFilePath ) ) {
							bool IsFromZip = false;
							if ( FilesWorker.isFB2Archive( SourceFilePath ) ) {
								IsFromZip = true;
								FilePathIfFromZip = ZipFB2Worker.getFileFromZipFBZ( SourceFilePath, TempDir );
							}
							if( File.Exists( FilePathIfFromZip ) ) {
								try {
									FB2InfoList.Add(
										new FB2ItemInfo( item, SourceFilePath, FilePathIfFromZip, IsFromZip )
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
		
		/// <summary>
		/// Создание списка ListViewItemInfo данных для помеченных / выделенных книг и каталогов Корректора в корневой папке
		/// </summary>
		/// <param name="listView">Список книг класса ListView</param>
		/// <param name="FB2SourceRootDir">Папка с книгами</param>
		/// <param name="BooksValidateType">Режим валидации книг</param>
		/// <returns>Список элементов, типа ListViewItemInfo</returns>
		/// <param name="ProgressBar">Прогресс класса ToolStripProgressBar</param>
		public static IList<ListViewItemInfo> makeListViewItemInfoList( ListView listView, string FB2SourceRootDir,
		                                                               BooksValidateModeEnum BooksValidateType,
		                                                               ToolStripProgressBar ProgressBar ) {
			IList<ListViewItemInfo> ListViewItemInfoList = new List<ListViewItemInfo>();
			if( listView.Items.Count > 0 ) {
				string	TempDir = Settings.Settings.TempDirPath;
				IList Items = null;
				if( BooksValidateType == BooksValidateModeEnum.SelectedBooks )
					Items = listView.SelectedItems;
				else if (BooksValidateType == BooksValidateModeEnum.CheckedBooks)
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
		
		/// <summary>
		/// Определение, есть ли среди помеченных/выделенных итемов папки?
		/// </summary>
		/// <param name="Items">IList список типа ListViewItem</param>
		/// <returns>true, если найдена(ы) папка(и)</returns>
		public static bool checkedDirExsist( IList Items ) {
			if( Items != null ) {
				foreach( ListViewItem item in Items ) {
					if( ((ListViewItemType)item.Tag).Type != "f" )
						return true;
				}
			}
			return false;
		}
		
		/// <summary>
		/// Определение, есть ли в списке заданный жанр
		/// </summary>
		/// <returns>true, если в списке жанров найден указанный жанр</returns>
		public static bool genreIsExist( ListView listView, Genre g, FB2UnionGenres fb2g ) {
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
		
		/// <summary>
		/// Есть ли в списке книг listView заданный автор
		/// </summary>
		/// <returns>true, если в списке книг найдена любая книга с указанным автором</returns>
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

		/// <summary>
		/// Определение, listViewItem - это файл?
		/// </summary>
		/// <returns>true, если listViewItem - это файл</returns>
		public static bool isFileItem( ListViewItem listViewItem ) {
			return ((ListViewItemType)listViewItem.Tag).Type == "f" ? true : false;
		}
		
		/// <summary>
		/// Пометка цветом и зачеркиванием удаленных книг с диска, но не из списка (быстрый режим удаления)
		/// </summary>
		public static void markRemoverFileInCopyesList( ListViewItem lvi ) {
			lvi.BackColor = Color.LightGray;
			lvi.Font = new Font(lvi.Font.Name, lvi.Font.Size, FontStyle.Strikeout);
			lvi.Checked = false;
		}
		
		/// <summary>
		/// Создание пустых subitems для Сортировщика и Корректора
		/// </summary>
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
		
		/// <summary>
		/// Создание заполненных subitems для Сортировщика и Корректора
		/// </summary>
		public static ListViewItem.ListViewSubItem[]
			createSubItemsWithMetaData(
				string FilePath, string SourceFilePath,
				ListViewItem Item, ref FB2UnionGenres FB2FullSortGenres
			) {
			FB2BookDescription bd = null;
			try {
				bd = new FB2BookDescription( FilePath );
			} catch ( System.Exception ) {
			}
			
			string valid = "?";
			valid = bd != null ? bd.IsValidFB2Union : m_fv2Validator.ValidatingFB2File( FilePath );
			if ( !string.IsNullOrEmpty( valid ) ) {
				valid = "Нет";
				Item.ForeColor = Colors.FB2NotValidForeColor;
			} else {
				valid = "Да";
				Item.ForeColor = FilesWorker.isFB2File( SourceFilePath )
					? Colors.FB2ForeColor : Colors.ZipFB2ForeColor;
			}
			
			return new ListViewItem.ListViewSubItem[] {
				new ListViewItem.ListViewSubItem(Item, bd != null ? bd.TIBookTitle : string.Empty),
				new ListViewItem.ListViewSubItem(Item, bd != null ? bd.TIAuthors : string.Empty),
				new ListViewItem.ListViewSubItem(Item, bd != null ? GenresWorker.cyrillicGenreName(
					bd.TIGenres, ref FB2FullSortGenres
				) : string.Empty),
				new ListViewItem.ListViewSubItem(Item, bd != null ? bd.TISequences : string.Empty),
				new ListViewItem.ListViewSubItem(Item, bd != null ? bd.TILang : string.Empty),
				new ListViewItem.ListViewSubItem(Item, bd != null ? bd.DIID : string.Empty),
				new ListViewItem.ListViewSubItem(Item, bd != null ? bd.DIVersion : string.Empty),
				new ListViewItem.ListViewSubItem(Item, bd != null ? bd.Encoding : string.Empty),
				new ListViewItem.ListViewSubItem(Item, valid),
				new ListViewItem.ListViewSubItem(Item, Path.GetExtension(SourceFilePath).ToLower()),
				new ListViewItem.ListViewSubItem(Item, bd != null ? bd.FileLength : string.Empty),
				new ListViewItem.ListViewSubItem(Item, bd != null ? bd.FileCreationTime : string.Empty),
				new ListViewItem.ListViewSubItem(Item, bd != null ? bd.FileLastWriteTime : string.Empty)
			};
		}

		/// <summary>
		/// Занесение данных в выделенный итем (метаданные книги после правки) для Корректора
		/// </summary>
		/// <returns>Пустая строка, файл валиден; Строка с сообщением или ?, если файл невалиден</returns>
		public static string viewBookMetaDataLocal( ref string SrcFilePath, ref FB2BookDescription fb2Desc, ListViewItem lvi, ref FB2UnionGenres FB2Genres ) {
			string RetValid = "?";
			if ( lvi != null ) {
				if ( fb2Desc != null ) {
					string valid = fb2Desc.IsValidFB2Union;
					RetValid = valid;
					if ( string.IsNullOrEmpty( valid ) ) {
						valid = "Да";
						lvi.ForeColor = FilesWorker.isFB2File( SrcFilePath )
							? Colors.FB2ForeColor
							: Colors.ZipFB2ForeColor;
					} else {
						valid = "Нет";
						lvi.ForeColor = Colors.FB2NotValidForeColor;
					}

					lvi.SubItems[(int)ResultViewCollumnEnum.BookTitle].Text = fb2Desc.TIBookTitle;
					lvi.SubItems[(int)ResultViewCollumnEnum.Authors].Text = fb2Desc.TIAuthors;
					lvi.SubItems[(int)ResultViewCollumnEnum.Genres].Text = GenresWorker.cyrillicGenreName( fb2Desc.TIGenres, ref FB2Genres );
					lvi.SubItems[(int)ResultViewCollumnEnum.Sequences].Text = fb2Desc.TISequences;
					lvi.SubItems[(int)ResultViewCollumnEnum.Lang].Text = fb2Desc.TILang;
					lvi.SubItems[(int)ResultViewCollumnEnum.BookID].Text = fb2Desc.DIID;
					lvi.SubItems[(int)ResultViewCollumnEnum.Version].Text = fb2Desc.DIVersion;
					lvi.SubItems[(int)ResultViewCollumnEnum.Encoding].Text = fb2Desc.Encoding;
					lvi.SubItems[(int)ResultViewCollumnEnum.Validate].Text = valid;
					lvi.SubItems[(int)ResultViewCollumnEnum.Format].Text = Path.GetExtension( SrcFilePath ).ToLower();
					lvi.SubItems[(int)ResultViewCollumnEnum.FileLength].Text = fb2Desc.FileLength;
					lvi.SubItems[(int)ResultViewCollumnEnum.CreationTime].Text = fb2Desc.FileCreationTime;
					lvi.SubItems[(int)ResultViewCollumnEnum.LastWriteTime].Text = fb2Desc.FileLastWriteTime;
				}
			}
			return RetValid;
		}

		/// <summary>
		/// Скрыть метаданные итема в Списке для Корректора
		/// </summary>
		public static void hideMetaDataLocal( ListViewItem Item ) {
			for( int i = 1; i != Item.SubItems.Count; ++i )
				Item.SubItems[i].Text = string.Empty;
			
			string ItemType = ((ListViewItemType)Item.Tag).Type;
			if( ItemType == "f" ) {
				Item.ForeColor = Colors.FB2NotValidForeColor;
				Item.SubItems[(int)ResultViewCollumnEnum.Validate].Text = "Нет";
				Item.SubItems[(int)ResultViewCollumnEnum.Format].Text = Item.Text.Substring(Item.Text.LastIndexOf('.'));
			}
		}
		/// <summary>
		/// Скрыть метаданные итема в Списке для Дубликатора
		/// </summary>
		public static void hideMetaDataLocalForDup( ListViewItem Item ) {
			for( int i = 1; i != Item.SubItems.Count; ++i )
				Item.SubItems[i].Text = string.Empty;
			Item.ForeColor = Colors.FB2NotValidForeColor;
			Item.SubItems[(int)ResultViewDupCollumnEnum.Validate].Text = "Нет";
		}
		
		/// <summary>
		/// Показать данные книги для Сортировщика
		/// </summary>
		public static void viewBookMetaDataLocal( string FilePath, ListView listViewSource,
		                                         FB2UnionGenres FB2FullSortGenres,
		                                         int ItemNumber, string TempDir ) {
			string valid = "?";
			FB2BookDescription bd = null;
			try {
				if( FilesWorker.isFB2File( FilePath ) ) {
					// показать данные fb2 файлов
					bd = new FB2BookDescription( FilePath );

					valid = bd.IsValidFB2Union;
					if ( !string.IsNullOrEmpty( valid ) ) {
						valid = "Нет";
						listViewSource.Items[ItemNumber].ForeColor = Colors.FB2NotValidForeColor;
					} else {
						valid = "Да";
						listViewSource.ForeColor = FilesWorker.isFB2File( FilePath )
							? Colors.FB2ForeColor
							: Colors.ZipFB2ForeColor;
					}

					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumnEnum.BookTitle].Text = bd.TIBookTitle;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumnEnum.Authors].Text = bd.TIAuthors;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumnEnum.Genres].Text = GenresWorker.cyrillicGenreName(
						bd.TIGenres, ref FB2FullSortGenres
					);
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumnEnum.Sequences].Text = bd.TISequences;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumnEnum.Lang].Text = bd.TILang;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumnEnum.BookID].Text = bd.DIID;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumnEnum.Version].Text = bd.DIVersion;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumnEnum.Encoding].Text = bd.Encoding;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumnEnum.Validate].Text = valid;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumnEnum.Format].Text = Path.GetExtension( FilePath ).ToLower();
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumnEnum.FileLength].Text = bd.FileLength;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumnEnum.CreationTime].Text = bd.FileCreationTime;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumnEnum.LastWriteTime].Text = bd.FileLastWriteTime;
				} else if( FilesWorker.isFB2Archive( FilePath ) ) {
					// показать данные архивов
					FilesWorker.RemoveDir( TempDir );
					SharpZipLibWorker sharpZipLib = new SharpZipLibWorker();
					sharpZipLib.UnZipFB2Files( FilePath, TempDir );
					string [] files = Directory.GetFiles( TempDir );
					bd = new FB2BookDescription( files[0] );

					valid = bd.IsValidFB2Union;
					if ( !string.IsNullOrEmpty( valid ) ) {
						valid = "Нет";
						listViewSource.Items[ItemNumber].ForeColor = Colors.FB2NotValidForeColor;
					} else {
						valid = "Да";
						listViewSource.Items[ItemNumber].ForeColor = FilesWorker.isFB2File( FilePath )
							? Colors.FB2ForeColor
							: Colors.ZipFB2ForeColor;
					}

					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumnEnum.BookTitle].Text = bd.TIBookTitle;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumnEnum.Authors].Text = bd.TIAuthors;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumnEnum.Genres].Text = GenresWorker.cyrillicGenreName(
						bd.TIGenres, ref FB2FullSortGenres
					);
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumnEnum.Sequences].Text = bd.TISequences;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumnEnum.Lang].Text = bd.TILang;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumnEnum.BookID].Text = bd.DIID;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumnEnum.Version].Text = bd.DIVersion;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumnEnum.Encoding].Text = bd.Encoding;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumnEnum.Validate].Text = valid;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumnEnum.Format].Text = Path.GetExtension( FilePath ).ToLower();
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumnEnum.FileLength].Text = bd.FileLength;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumnEnum.CreationTime].Text = bd.FileCreationTime;
					listViewSource.Items[ItemNumber].SubItems[(int)ResultViewCollumnEnum.LastWriteTime].Text = bd.FileLastWriteTime;
				}
			} catch( System.Exception ) {
				listViewSource.Items[ItemNumber].ForeColor = Colors.BadZipForeColor;
			} finally {
				FilesWorker.RemoveDir( TempDir );
			}
		}
		
		/// <summary>
		/// Генерация списка файлов - создание итемов listViewSource
		/// </summary>
		/// <param name="listView">ListView для формирования списка items</param>
		/// <param name="dirPath">Исходная папка сканирования книг для генерации списка их данных</param>
		/// <param name="fb2g">Список Жанров FB2UnionGenres</param>
		/// <param name="isTagsView">Нужно ли отображатьметаданные книг в списке</param>
		/// <param name="itemChecked">Нужно ли ставить пометку на созданный ListLiewItem</param>
		/// <param name="AutoResizeColumns">Нужно ли проводить авторазмер колонок списка</param>
		/// <param name="form">Форма прогреса Windows.Form (null, если не используется)</param>
		/// <param name="ProgressBar">Прогресc Windows.ProgressBar.ProgressBar (null, если не используется)</param>
		/// <param name="bw">Фоновый обработчик BackgroundWorker (null, если не используется)</param>
		/// <param name="e">DoWorkEventArgs (null, если не используется)</param>
		/// <returns>true - список сформирован; false - прерывание генерации списка</returns>
		public static bool generateBooksListWithMetaData( ListView listView, string dirPath, ref FB2UnionGenres fb2g,
		                                                 bool isTagsView, bool itemChecked, bool AutoResizeColumns,
		                                                 Form form = null, ProgressBar ProgressBar = null,
		                                                 BackgroundWorker bw = null, DoWorkEventArgs e = null ) {
			if ( !Directory.Exists( dirPath ) )
				return false;
			
			Cursor.Current = Cursors.WaitCursor;
			listView.BeginUpdate();
			listView.Items.Clear();
			string TempDir = Settings.Settings.TempDirPath;
			try {
				DirectoryInfo dirInfo = new DirectoryInfo( dirPath );
				ListViewItem.ListViewSubItem[] subItems;
				ListViewItem item = null;
				if ( dirInfo.Exists ) {
					if ( dirInfo.Parent != null ) {
						item = new ListViewItem( "..", 3) ;
						item.Tag = new ListViewItemType( "dUp", dirInfo.Parent.FullName );
						subItems = createEmptySubItemsForItem( item );
						item.SubItems.AddRange( subItems );
						listView.Items.Add( item );
					}
					int nItemCount = 0;
					
					if ( form != null ) {
						form.Text += String.Format(
							": всего {0} каталогов, {1} файлов", dirInfo.GetDirectories().Length+1, dirInfo.GetFiles().Length
						);
						ProgressBar.Maximum	= ( dirInfo.GetDirectories().Length + dirInfo.GetFiles().Length ) + 1;
					}
					
					foreach ( DirectoryInfo dir in dirInfo.GetDirectories() ) {
						if ( bw != null ) {
							if ( ( bw.CancellationPending ) ) {
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
					foreach ( FileInfo file in dirInfo.GetFiles() ) {
						if ( bw != null ) {
							if ( ( bw.CancellationPending ) ) {
								e.Cancel = true;
								listView.EndUpdate();
								Cursor.Current = Cursors.Default;
								return false;
							}
						}
						if ( FilesWorker.isFB2File( file.FullName ) || FilesWorker.isFB2Archive( file.FullName ) ) {
							item = new ListViewItem( file.Name, FilesWorker.isFB2File( file.FullName ) ? 1 : 2 );
							try {
								if ( FilesWorker.isFB2File( file.FullName ) ) {
									if ( isTagsView ) {
										item.ForeColor = Colors.FB2ForeColor;
										subItems = createSubItemsWithMetaData(
											file.FullName, file.FullName, item, ref fb2g
										);
									} else
										subItems = createEmptySubItemsForItem( item );
								} else {
									// для zip-архивов
									if( isTagsView ) {
										FilesWorker.RemoveDir( TempDir );
										sharpZipLib.UnZipFB2Files( file.FullName, TempDir );
										string [] files = Directory.GetFiles( TempDir );
										if ( FilesWorker.isFB2File( files[0] ) ) {
											item.ForeColor = Colors.ZipFB2ForeColor;
											subItems = createSubItemsWithMetaData(
												files[0], file.FullName, item, ref fb2g
											);
										} else {
											item.ForeColor = Colors.BadZipForeColor;
											subItems = createEmptySubItemsForItem( item );
										}
									} else
										subItems = createEmptySubItemsForItem( item );
								}
								item.SubItems.AddRange(subItems);
							} catch(System.Exception /*e*/) {
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
		/// <param name="fb2g">Список Жанров FB2UnionGenres</param>
		/// <param name="isTagsView">Нужно ли отображатьметаданные книг в списке</param>
		/// <param name="bw">Фоновый обработчик BackgroundWorker (null, если не используется)</param>
		/// <param name="e">DoWorkEventArgs (null, если не используется)</param>
		/// <returns>true - список сформирован; false - прерывание генерации списка</returns>
		public static bool viewOrHideBookMetaDataLocal( ListView listViewFB2Files, ref FB2UnionGenres fb2g,
		                                               bool isTagsView,
		                                               BackgroundWorker bw = null, DoWorkEventArgs e = null ) {
			ListViewItemType it	= null;
			string TempDir = Settings.Settings.TempDirPath;
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
							it.Value, listViewFB2Files, fb2g, i, TempDir
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
		
		/// <summary>
		/// Форма ввода строки данных
		/// </summary>
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

			label.SetBounds(9, 16, 372, 13);
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

		/// <summary>
		/// Восстановление структуры description fb2 файла
		/// </summary>
		public static bool recoveryFB2Structure( ref FB2DescriptionCorrector fB2Corrector, ListViewItem lvi, string FilePath ) {
			if ( fB2Corrector.recoveryDescriptionNode() ) {
				lvi.ForeColor = FilesWorker.isFB2File( FilePath )
					? Colors.FB2ForeColor : Colors.ZipFB2ForeColor;
				return true;
			}
			return false;
		}

		/// <summary>
		/// Формирование Списка Групп Жанров
		/// </summary>
		public static void makeListGenresGroups( ComboBox GenresGroupComboBox ) {
			GenresGroupComboBox.Items.Clear();
			FB2UnionGenres fb2g = new FB2UnionGenres();
			GenresGroupComboBox.Items.AddRange( fb2g.GetListAllGenresGroupsArray() );
			if ( GenresGroupComboBox.Items.Count > 0 )
				GenresGroupComboBox.SelectedIndex = 0;
		}
		
		/// <summary>
		/// Формирование Списка всех полных Жанров (в скобках) в контролы
		/// </summary>
		public static void makeListAllFullGenres( ComboBox GenresComboBox ) {
			GenresComboBox.Items.Clear();
			FB2UnionGenres fb2g = new FB2UnionGenres();
			GenresComboBox.Items.AddRange( fb2g.GetListAllFullGenreArray() );
			if ( GenresComboBox.Items.Count > 0 )
				GenresComboBox.SelectedIndex = 0;
		}
		
		/// <summary>
		/// Формирование Списка Жанров в контролы, в зависимости от Группы (и коды и расшифровка Жанров)
		/// </summary>
		public static void makeListGenres( ComboBox GenresComboBox, string GenreGroup ) {
			GenresComboBox.Items.Clear();
			FB2UnionGenres fb2g = new FB2UnionGenres();
			GenresComboBox.Items.AddRange( fb2g.GetListAllFullGenreArrayForGroup( GenreGroup ) );
			if ( GenresComboBox.Items.Count > 0 )
				GenresComboBox.SelectedIndex = 0;
		}
		
		/// <summary>
		/// Формирование Списка Жанров в контролы, в зависимости от Группы (только коды Жанров)
		/// </summary>
		public static void makeListCodeGenres( ComboBox GenresComboBox, string GenreGroup ) {
			GenresComboBox.Items.Clear();
			FB2UnionGenres fb2g = new FB2UnionGenres();
			GenresComboBox.Items.AddRange( fb2g.GetFBGenreCodesArrayForGroup( GenreGroup ) );
			if ( GenresComboBox.Items.Count > 0 )
				GenresComboBox.SelectedIndex = 0;
		}
		
		/// <summary>
		/// Отображение обложки
		/// </summary>
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
		
		/// <summary>
		/// Автокорректировка всех выделеннеых/помеченных книг для Дубликатора
		/// </summary>
		/// <param name="AutoCorrectProcessingMode">Режим обработки книг: обработка одной книги или пакетный режим</param>
		/// <param name="Item">Текущий item (ListViewItem) списка книг</param>
		/// <param name="SrcFB2OrZipPath">Исходный путь fb2 или архива</param>
		/// <param name="sharpZipLib">Экземпляр класса по работе с архивами</param>
		/// <param name="fv2Validator">Экземпляр класса FB2Validator - Валидатор</param>
		public static void autoCorrect( BooksAutoCorrectProcessingModeEnum AutoCorrectProcessingMode,
		                               ListViewItem Item, string SrcFB2OrZipPath,
		                               SharpZipLibWorker sharpZipLib, FB2Validator fv2Validator ) {
			string FilePath = SrcFB2OrZipPath;
			string TempDir = Settings.Settings.TempDirPath;
			bool IsFromZip = false;
			if ( FilesWorker.isFB2Archive( SrcFB2OrZipPath ) ) {
				IsFromZip = true;
				FilePath = ZipFB2Worker.getFileFromZipFBZ( SrcFB2OrZipPath, TempDir );
			}
			
			if ( !string.IsNullOrEmpty( fv2Validator.ValidatingFB2File( FilePath ) ) ) {
				// автокорректировка только невалидного файла
				if ( AutoCorrectProcessingMode == BooksAutoCorrectProcessingModeEnum.OneBookProcessing ) {
					// Обработка только одной книги - при генерации исключения ничего не делаем
					FB2AutoCorrector.autoCorrector( FilePath );
				} else {
					// пакетная обработка.
					try {
						FB2AutoCorrector.autoCorrector( FilePath );
					} catch {
						// При сильно битой структуре книги переходим к обработке следующей книги
						FilesWorker.RemoveDir( TempDir );
						return;
					}
				}
				// Постобработка для Дубликатора после Автокорректировки файла
				postWorkForDuplicator(
					AutoCorrectProcessingMode, Item, SrcFB2OrZipPath, IsFromZip, FilePath, sharpZipLib
				);
			} else {
				// замена <lang> для русских книг на ru, украинских на uk, беларуский на be для fb2 без <src-title-info>
				FB2Text fb2Text = new FB2Text( FilePath );
				string XmlText = fb2Text.toXML();
				string InputString = fb2Text.Description;
				LangRuUkBeCorrector langRuUkBeCorrector = new LangRuUkBeCorrector( ref InputString, XmlText );
				bool IsCorrected = false;
				InputString = langRuUkBeCorrector.correct( ref IsCorrected );
				if ( IsCorrected ) {
					fb2Text.Description = InputString;
					try {
						XmlDocument xmlDoc = new XmlDocument();
						xmlDoc.LoadXml( fb2Text.toXML() );
						xmlDoc.Save( FilePath );
					} catch {
						fb2Text.saveFile();
					}
					// Постобработка для Дубликатора после Автокорректировки файла
					postWorkForDuplicator(
						AutoCorrectProcessingMode, Item, SrcFB2OrZipPath, IsFromZip, FilePath, sharpZipLib
					);
				}
			}
			FilesWorker.RemoveDir( TempDir );
		}
		
		/// <summary>
		/// Автокорректировка книги для Корректора
		/// </summary>
		/// <param name="AutoCorrectProcessingMode">Режим обработки книг: обработка одной книги или пакетный режим</param>
		/// <param name="SrcFB2OrZipPath">Путь к обрабатываемой книге (fb2 или zip-архив)</param>
		/// <param name="sharpZipLib">Экземпляр класса SharpZipLibWorker по работе с zip-архивами</param>
		/// <param name="fv2Validator">Экземпляр класса FB2Validator - Валидатор</param>
		public static void autoCorrect( BooksAutoCorrectProcessingModeEnum AutoCorrectProcessingMode,
		                               string SrcFB2OrZipPath, SharpZipLibWorker sharpZipLib,
		                               FB2Validator fv2Validator ) {
			string FilePath = SrcFB2OrZipPath;
			string TempDir = Settings.Settings.TempDirPath;
			bool IsFromZip = false;
			if ( FilesWorker.isFB2Archive( SrcFB2OrZipPath ) ) {
				IsFromZip = true;
				FilePath = ZipFB2Worker.getFileFromZipFBZ( SrcFB2OrZipPath, TempDir );
			}
			
			if ( !string.IsNullOrWhiteSpace( fv2Validator.ValidatingFB2File( FilePath ) ) ) {
				// автокорректировка только невалидного файла
				if ( AutoCorrectProcessingMode == BooksAutoCorrectProcessingModeEnum.OneBookProcessing ) {
					// Обработка только одной книги - при генерации исключения ничего не делаем
					FB2AutoCorrector.autoCorrector( FilePath );
				} else {
					// пакетная обработка.
					try {
						FB2AutoCorrector.autoCorrector( FilePath );
					} catch {
						// При сильно битой структуре книги переходим к обработке следующей книги
						FilesWorker.RemoveDir( TempDir );
						return;
					}
				}
				
				// Сжатие файла FilePath и перемещение архива по пути SrcFB2OrZipPath (если он был из архива)
				if( IsFromZip )
					zipMoveTempFB2FileTo( sharpZipLib, FilePath, SrcFB2OrZipPath );
				// восстанавление раздела description до структуры с необходимыми элементами для валидности
				FictionBook	fb2 = null;
				try {
					fb2 = new FictionBook( FilePath );
					recoverDesc( ref fb2, sharpZipLib, SrcFB2OrZipPath, FilePath );
				} catch { }
			} else {
				// замена <lang> для русских книг на ru, украинских на uk, беларуский на be для fb2 без <src-title-info>
				FB2Text fb2Text = new FB2Text( FilePath );
				string InputString = fb2Text.Description;
				LangRuUkBeCorrector langRuUkBeCorrector = new LangRuUkBeCorrector( ref InputString, fb2Text.Bodies );
				bool IsCorrected = false;
				InputString = langRuUkBeCorrector.correct( ref IsCorrected );
				if ( IsCorrected ) {
					fb2Text.Description = InputString;
					try {
						XmlDocument xmlDoc = new XmlDocument();
						xmlDoc.LoadXml( fb2Text.toXML() );
						xmlDoc.Save( FilePath );
					} catch {
						fb2Text.saveFile();
					}
					
					// Сжатие файла FilePath и перемещение архива по пути SrcFB2OrZipPath (если он был из архива)
					if( IsFromZip )
						zipMoveTempFB2FileTo( sharpZipLib, FilePath, SrcFB2OrZipPath );
					// восстанавление раздела description до структуры с необходимыми элементами для валидности
					FictionBook	fb2 = null;
					try {
						fb2 = new FictionBook( FilePath );
						recoverDesc( ref fb2, sharpZipLib, SrcFB2OrZipPath, FilePath );
					} catch { }
				}
			}

			FilesWorker.RemoveDir( TempDir );
		}
		
		/// <summary>
		/// Восстановление description раздела описания fb2 книги
		/// </summary>
		/// <param name="fb2">Книга в fb2 фармате</param>
		/// <param name="sharpZipLib">Экземпляр класса по работе с архивами</param>
		/// <param name="SrcFB2OrZipPath">Исходный путь fb2 или архива</param>
		/// <param name="FilePath">Временный путь распакованного файла</param>
		/// <returns>true, если fb2 не равен null, и восстановление произошло; false - в противном случае</returns>
		private static bool recoverDesc( ref FictionBook fb2, SharpZipLibWorker sharpZipLib,
		                                string SrcFB2OrZipPath, string FilePath ) {
			if( fb2 != null ) {
				FB2DescriptionCorrector fB2Corrector = new FB2DescriptionCorrector( fb2 );
				fB2Corrector.recoveryDescriptionNode();
				fb2.saveToFB2File( FilePath, false );
				bool IsFromZip = FilesWorker.isFB2Archive( SrcFB2OrZipPath );
				if ( IsFromZip )
					zipMoveTempFB2FileTo( sharpZipLib, FilePath, SrcFB2OrZipPath);
				if ( IsFromZip && File.Exists( FilePath ) )
					File.Delete( FilePath );
				return true;
			}
			return false;
		}
		
		/// <summary>
		/// Сжатие файла FilePath и перемещение архива по пути SrcFilePath
		/// </summary>
		/// <param name="sharpZipLib">Экземпляр класса по работе с архивами</param>
		/// <param name="FilePath">Что: Путь к временному распакованному файлу</param>
		/// <param name="ToZipPath">Куда: Исходный путь архива</param>
		public static void zipMoveTempFB2FileTo( SharpZipLibWorker sharpZipLib, string FilePath, string ToZipPath ) {
			if ( string.IsNullOrWhiteSpace( FilePath ) )
				return;
			if ( string.IsNullOrWhiteSpace( ToZipPath ) )
				return;
			string ArchFile = FilePath + ".zip";
			sharpZipLib.ZipFile( FilePath, ArchFile, 9, ICSharpCode.SharpZipLib.Zip.CompressionMethod.Deflated, 4096 );
			if( File.Exists( ToZipPath ) )
				File.Delete( ToZipPath );
			File.Move( ArchFile, ToZipPath );
		}
		
		/// <summary>
		/// Занесение данных о валидации в поле детализации
		/// </summary>
		/// <returns>Откорректированная строка типа string</returns>
		public static string isValidate( string SrcFilePath, TextBox tbValidate ) {
			string Result = m_fv2Validator.ValidatingFB2File( SrcFilePath );
			if ( string.IsNullOrEmpty( Result ) ) {
				tbValidate.Text = "Все в порядке - файл валидный!";
			} else {
				tbValidate.Text = "Файл невалидный. Ошибка:";
				tbValidate.AppendText( Environment.NewLine );
				tbValidate.AppendText( Environment.NewLine );
				tbValidate.AppendText( Result );
			}
			return Result;
		}

		/// <summary>
		/// Удаление из списка всех обработанных книг (файлы)
		/// </summary>
		/// <param name="filesList">Список обрабатываемых файлов</param>
		/// <param name="finishedFilesList">Список обработанных файлов</param>
		public static void removeFinishedFilesInFilesList( ref List<string> filesList, ref List<string> finishedFilesList ) {
			List<string> FilesToWorkingList = new List<string>();
			foreach ( var file in filesList.Except(finishedFilesList) )
				FilesToWorkingList.Add(file);
			
			filesList.Clear();
			filesList.AddRange(FilesToWorkingList);
		}
		
		/// <summary>
		/// Постобработка для Дубликатора после Автокорректировки файла
		/// </summary>
		/// <param name="AutoCorrectProcessingMode">Режим обработки книг: обработка одной книги или пакетный режим</param>
		/// <param name="Item">Текущий item (ListViewItem) списка книг</param>
		/// <param name="SrcFB2OrZipPath">Исходный путь fb2 или архива</param>
		/// <param name="IsFromZip">true, если обрабатываемый файл был извлечен из архива</param>
		/// <param name="FilePath">Текущий обрабатываемый fb2 файл</param>
		/// <param name="sharpZipLib">Экземпляр класса по работе с архивами</param>
		private static void postWorkForDuplicator( BooksAutoCorrectProcessingModeEnum AutoCorrectProcessingMode,
		                                          ListViewItem Item, string SrcFB2OrZipPath,
		                                          bool IsFromZip, string FilePath,
		                                          SharpZipLibWorker sharpZipLib ) {
			// Сжатие файла FilePath и перемещение архива по пути SrcFB2OrZipPath (если он был из архива)
			if( IsFromZip )
				zipMoveTempFB2FileTo( sharpZipLib, FilePath, SrcFB2OrZipPath );
			
			// восстанавление раздела description до структуры с необходимыми элементами для валидности
			FictionBook fb2 = null;
			try {
				fb2 = new FictionBook( FilePath );
				recoverDesc( ref fb2, sharpZipLib, SrcFB2OrZipPath, FilePath );
			} catch ( FileLoadException e ) {
				if ( AutoCorrectProcessingMode == BooksAutoCorrectProcessingModeEnum.OneBookProcessing )
					MessageBox.Show( e.Message, "Автокорректировка", MessageBoxButtons.OK, MessageBoxIcon.Error );
				return;
			}
			if( fb2 != null ) {
				FB2DescriptionCorrector fB2Corrector = new FB2DescriptionCorrector( fb2 );
				WorksWithBooks.recoveryFB2Structure( ref fB2Corrector, Item, SrcFB2OrZipPath );
				fb2.saveToFB2File( FilePath, false );
				// Сжатие файла FilePath и перемещение архива по пути SrcFB2OrZipPath (если он был из архива)
				if( IsFromZip )
					zipMoveTempFB2FileTo( sharpZipLib, SrcFB2OrZipPath, FilePath );
				if ( IsFromZip && File.Exists( FilePath ) )
					File.Delete( FilePath );
			}
		}
	}
}
