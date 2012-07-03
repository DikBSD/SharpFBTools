/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 16.03.2009
 * Time: 9:03
 * 
 * License: GPL 2.1
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Threading;
using System.Diagnostics;

using Core.FB2.FB2Parsers;
using Core.FB2.Description.TitleInfo;
using Core.FB2.Description.Common;
using Core.FB2.Genres;
using Core.BookSorting;
using Core.FileManager;
using Core.Misc;
using Settings;

using fB2Parser					= Core.FB2.FB2Parsers.FB2Parser;
using filesWorker				= Core.FilesWorker.FilesWorker;
using archivesWorker			= Core.FilesWorker.Archiver;
using fb2Validator				= Core.FB2Parser.FB2Validator;
using stringProcessing			= Core.StringProcessing.StringProcessing;
using templatesParser			= Core.Templates.TemplatesParser;
using templatesVerify			= Core.Templates.TemplatesVerify;
using templatesLexemsSimple		= Core.Templates.Lexems.TPSimple;
using selectedSortQueryCriteria	= Core.BookSorting.SelectedSortQueryCriteria;

using Core.FB2Dublicator;

namespace SharpFBTools.Tools
{
	/// <summary>
	/// Description of SFBTpFileManager.
	/// </summary>
	public partial class SFBTpFileManager : UserControl
	{
		#region Закрытые данные класса
		private string m_CurrentDir = "";
		private fb2Validator fv2V = new fb2Validator();
		private List<selectedSortQueryCriteria> m_lSSQCList = null; // список критериев поиска для Избранной Сортировки
        private DateTime m_dtStart;
        private BackgroundWorker m_bw = null;
		private string m_sSource		= "";
		private string m_sTarget		= "";
		private string m_sLineTemplate	= "";
		private string m_sMessTitle		= "";
        private bool m_bFullSort		= true;
        private bool m_bScanSubDirs		= true;
        private Core.FileManager.StatusView m_sv	= new Core.FileManager.StatusView();
        private MiscListView m_mscLV				= new MiscListView(); // класс по работе с ListView
        private const string space		= " "; // для задания отступов данных от границ колонов в Списке
        #endregion
		
		public SFBTpFileManager()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			// задание для кнопок ToolStrip стиля и положения текста и картинки
			SetToolButtonsSettings();
			
			InitializeBackgroundWorker();
			
			Init();
			// читаем сохраненные пути к папкам и шаблон Менеджера Файлов, если они есть
			ReadFMTempData();
			//
			string sTDPath = Settings.SettingsFM.GetDefFMDescTemplatePath();
			if( File.Exists( sTDPath ) ) {
				richTxtBoxDescTemplates.LoadFile( sTDPath );
			} else {
				richTxtBoxDescTemplates.Text = "Не найден файл описания Шаблонов подстановки: \""+sTDPath+"\"";
			}

		}
		
		#region Открытые методы класса
		// задание для кнопок ToolStrip стиля и положения текста и картинки
		public void SetToolButtonsSettings() {
			Settings.SettingsFM.SetToolButtonsSettings( tsSelectedSort );
		}
		#endregion
		
		#region Закрытые методы реализации BackgroundWorker
		private void InitializeBackgroundWorker() {
			// Инициализация перед использование BackgroundWorker 
            m_bw = new BackgroundWorker();
            m_bw.WorkerReportsProgress		= true; // Позволить выводить прогресс процесса
            m_bw.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
            m_bw.DoWork 			+= new DoWorkEventHandler( bw_DoWork );
            m_bw.ProgressChanged 	+= new ProgressChangedEventHandler( bw_ProgressChanged );
            m_bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler( bw_RunWorkerCompleted );
		}
		
		private void bw_DoWork( object sender, DoWorkEventArgs e ) {
			// сортировка файлов по папкам, согласно шаблонам подстановки
			List<string> lDirList = new List<string>();
			List<string> lCheckedDirList = new List<string>();
			List<string> lCheckedFileList = new List<string>();
			if(m_bFullSort) {
				/* Полная Сортировка */
				// формируем список помеченных папок
				System.Windows.Forms.ListView.CheckedListViewItemCollection checkedItems = listViewSource.CheckedItems;
				foreach( ListViewItem lvi in checkedItems ) {
					ListViewItemType it = (ListViewItemType)lvi.Tag;
					if(it.Type == "d") {
						lCheckedDirList.Add(it.Value.Trim());
					} else if(it.Type == "f") {
						lCheckedFileList.Add(it.Value.Trim());
					}
				}
				if( !m_bScanSubDirs ) {
					// сканировать только указанную папку
					lDirList.AddRange(lCheckedDirList);// Add( m_sSource );
					lvFilesCount.Items[0].SubItems[1].Text = lDirList.Count.ToString();
				} else {
					// сканировать и все подпапки
					foreach(string sIn in lCheckedDirList) {
						m_sv.AllFiles += filesWorker.DirsParser( m_bw, e, sIn, lvFilesCount, ref lDirList, false );
					}
					m_sv.AllFiles += lCheckedFileList.Count;
					m_sv.AllDirs = lDirList.Count;
				}
			} else {
				// Избранная Сортировка
				if( !m_bScanSubDirs ) {
					// сканировать только указанную папку
					lDirList.Add( m_sSource );
					lvFilesCount.Items[0].SubItems[1].Text = "1";
				} else {
					// сканировать и все подпапки
					m_sv.AllFiles	= filesWorker.DirsParser( m_bw, e, m_sSource, lvFilesCount, ref lDirList, false );
					m_sv.AllDirs	= lDirList.Count;
				}
			}

			// отобразим число всех файлов в папке сканирования
			lvFilesCount.Items[1].SubItems[1].Text = m_sv.AllFiles.ToString();
			
			// Проверить флаг на остановку процесса 
			if( ( m_bw.CancellationPending == true ) ) {
				e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
				return;
			}
			
			// проверка, есть ли хоть один файл в папке для сканирования
			if( m_sv.AllFiles == 0 ) {
				MessageBox.Show( "В папке сканирования не найдено ни одного файла!\nРабота прекращена.", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				tsslblProgress.Text = Settings.Settings.GetReady();
				SetSelectedSortingStartEnabled( true );
				return;
			}
			
			tsslblProgress.Text		= "Сортировка файлов:";
			tsProgressBar.Maximum	= m_sv.AllFiles;
			tsProgressBar.Value		= 0;
			
			// данные настроек для сортировки по шаблонам
			Settings.DataFM dfm = new Settings.DataFM();
			
			// папки для проблемных файлов
			dfm.NotReadFB2Dir	= m_sTarget + "\\" + dfm.NotReadFB2Dir;
			dfm.FileLongPathDir	= m_sTarget + "\\" + dfm.FileLongPathDir;
			dfm.NotValidFB2Dir	= m_sTarget + "\\" + dfm.NotValidFB2Dir;
			dfm.NotOpenArchDir	= m_sTarget + "\\" + dfm.NotOpenArchDir;
				
			
			// формируем лексемы шаблонной строки
			List<templatesLexemsSimple> lSLexems = templatesParser.GemSimpleLexems( m_sLineTemplate );
			// сортировка
			if( m_bFullSort ) {
				// Полная Сортировка
				// для помеченных файлов
				foreach(string filePath in lCheckedFileList) {
					// Проверить флаг на остановку процесса
					if( ( m_bw.CancellationPending == true ) ) {
						e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
						return;
					}
					// создаем файл по заданному пути
					if( dfm.GenreOneMode && dfm.AuthorOneMode ) {
						// по первому Жанру и первому Автору Книги
						MakeFileFor1Genre1Author( filePath, m_sSource, m_sTarget, lSLexems, dfm );
					} else if( dfm.GenreOneMode && !dfm.AuthorOneMode ) {
						// по первому Жанру и всем Авторам Книги
						MakeFileFor1GenreAllAuthor( filePath, m_sSource, m_sTarget, lSLexems, dfm );
					} else if( !dfm.GenreOneMode && dfm.AuthorOneMode ) {
						// по всем Жанрам и первому Автору Книги
						MakeFileForAllGenre1Author( filePath, m_sSource, m_sTarget, lSLexems, dfm );
					} else {
						// по всем Жанрам и всем Авторам Книги
						MakeFileForAllGenreAllAuthor( filePath, m_sSource, m_sTarget, lSLexems, dfm );
					}

					m_bw.ReportProgress( 0 ); // отобразим данные в контролах
				}
				// для помеченных папок
				string sFromFilePath = "";
				foreach( string s in lDirList ) {
					DirectoryInfo diFolder = new DirectoryInfo( s );
					foreach( FileInfo fiNextFile in diFolder.GetFiles() ) {
						// Проверить флаг на остановку процесса 
						if( ( m_bw.CancellationPending == true ) ) {
							e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
							return;
						} 
						
						sFromFilePath = s + "\\" + fiNextFile.Name;
						// создаем файл по новому пути
						if( dfm.GenreOneMode && dfm.AuthorOneMode ) {
							// по первому Жанру и первому Автору Книги
							MakeFileFor1Genre1Author( sFromFilePath, m_sSource, m_sTarget, lSLexems, dfm );
						} else if( dfm.GenreOneMode && !dfm.AuthorOneMode ) {
							// по первому Жанру и всем Авторам Книги
							MakeFileFor1GenreAllAuthor( sFromFilePath, m_sSource, m_sTarget, lSLexems, dfm );
						} else if( !dfm.GenreOneMode && dfm.AuthorOneMode ) {
							// по всем Жанрам и первому Автору Книги
							MakeFileForAllGenre1Author( sFromFilePath, m_sSource, m_sTarget, lSLexems, dfm );
						} else {
							// по всем Жанрам и всем Авторам Книги
							MakeFileForAllGenreAllAuthor( sFromFilePath, m_sSource, m_sTarget, lSLexems, dfm );
						}

						m_bw.ReportProgress( 0 ); // отобразим данные в контролах
					}
				}
				lCheckedDirList.Clear();
				lCheckedFileList.Clear();
			} else {
				// Избранная Сортировка
				string sFromFilePath = "", sExt = "";
				foreach( string s in lDirList ) {
					DirectoryInfo diFolder = new DirectoryInfo( s );
					foreach( FileInfo fiNextFile in diFolder.GetFiles() ) {
						// Проверить флаг на остановку процесса 
						if( ( m_bw.CancellationPending == true ) ) {
							e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
							return;
						}
						
						sFromFilePath = s + "\\" + fiNextFile.Name;
						sExt = Path.GetExtension( sFromFilePath ).ToLower();
						// создаем файл по новому пути
						if( sExt==".fb2" ) {
							// Создание файла по критериям Избранной сортировки
							MakeFileForSelectedSortingWorker( sFromFilePath, m_sSource, m_sTarget, lSLexems, dfm );
						} else {
							// это архив?
							if( archivesWorker.IsArchive( sExt ) ) {
								List<string> lFilesListFromArchive = archivesWorker.GetFileListFromArchive( sFromFilePath, Settings.Settings.GetTempDir(), dfm.A7zaPath, dfm.UnRarPath );
								IncArchiveInfo( sExt ); // Увеличить число определенного файла-архива на 1
								if( lFilesListFromArchive!=null ) {
									foreach( string sFB2FromArchPath in lFilesListFromArchive ) {
										// Создание файла по критериям Избранной сортировки
										MakeFileForSelectedSortingWorker( sFB2FromArchPath, m_sSource, m_sTarget, lSLexems, dfm );
									}
								}
								filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
							}
						}
						m_bw.ReportProgress( 0 ); // отобразим данные в контролах
					}
				}
			}
			lDirList.Clear();
			if(m_bFullSort && Settings.FileManagerSettings.FullSortingDelFB2Files) {
				GenerateSourceList(m_sSource);
			}
        }

		private void bw_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
            // Отобразим результат сортировки
            if( chBoxViewProgress.Checked ) SortingProgressData();
            ++tsProgressBar.Value;
        }
		
        private void bw_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {   
            // Проверяем это отмена, ошибка, или конец задачи и сообщить
            SortingProgressData(); // Отобразим результат сортировки
            DateTime dtEnd = DateTime.Now;
            filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
            
            string sTime = dtEnd.Subtract( m_dtStart ).ToString() + " (час.:мин.:сек.)";
			string sMessCanceled	= "Сортировка остановлена!\nЗатрачено времени: "+sTime;
			string sMessError		= "";
			string sMessDone		= "Сортировка файлов в указанную папку завершена!\nЗатрачено времени: "+sTime;
           
			if( ( e.Cancelled == true ) ) {
                MessageBox.Show( sMessCanceled, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
            } else if( e.Error != null ) {
                sMessError = "Error!\n" + e.Error.Message + "\n" + e.Error.StackTrace + "\nЗатрачено времени: "+sTime;
            	MessageBox.Show( sMessError, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
            } else {
            	MessageBox.Show( sMessDone, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
            }
			
			tsslblProgress.Text = Settings.Settings.GetReady();

			if( m_bFullSort ) {
				SetFullSortingStartEnabled( true );
			} else {
				SetSelectedSortingStartEnabled( true );
			}
        }
		#endregion
		
		#region Закрытые вспомогательные методы класса
		private void ConnectListsEventHandlers( bool bConnect ) {
			if( !bConnect ) {
				// отключаем обработчики событий для Списка (убираем "тормоза")
				this.listViewSource.DoubleClick -= new System.EventHandler(this.ListViewSourceDoubleClick);
				this.listViewSource.ItemChecked -= new System.Windows.Forms.ItemCheckedEventHandler(this.ListViewSourceItemChecked);
				this.listViewSource.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.ListViewSourceItemCheck);
				this.listViewSource.KeyPress -= new System.Windows.Forms.KeyPressEventHandler(this.ListViewSourceKeyPress);
			} else {
				// подключаем обработчики событий для Списка
				this.listViewSource.DoubleClick += new System.EventHandler(this.ListViewSourceDoubleClick);
				this.listViewSource.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.ListViewSourceItemChecked);
				this.listViewSource.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ListViewSourceItemCheck);
				this.listViewSource.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ListViewSourceKeyPress);
			}
		}
		
		private void AutoResizeColumns() {
			// авторазмер колонок Списка
			listViewSource.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
//			for(int i=0; i!=listViewSource.Columns.Count; ++i) {
//				listViewSource.Columns[i].Width = listViewSource.Columns[i].Width + 2;
//			}
		}
		
		private string CyrillicGenreName(string GenreCode) {
			Settings.DataFM dfm = new Settings.DataFM();
			IFBGenres fb2g = null;
			if( dfm.GenresFB21Scheme ) {
				fb2g = new FB21Genres();
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
		
		private void GenerateSourceList(string dirPath) {
        	// заполнение списка данными указанной папки
        	m_CurrentDir = dirPath;
        	Cursor.Current = Cursors.WaitCursor;
        	listViewSource.BeginUpdate();
        	listViewSource.Items.Clear();
        	try {
        		Settings.DataFM dfm = new Settings.DataFM();
        		DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
        		ListViewItem.ListViewSubItem[] subItems;
        		ListViewItem item = null;
        		if (dirInfo.Exists) {
        			if(dirInfo.Parent != null) {
        				item = new ListViewItem("..", 3);
        				item.Tag = new ListViewItemType("dUp", dirInfo.Parent.FullName);
        				listViewSource.Items.Add(item);
        			}
        			int nItemCount = 0;
        			foreach (DirectoryInfo dir in dirInfo.GetDirectories()) {
        				item = new ListViewItem(space+dir.Name+space, 0);
        				item.Checked = true;
        				item.Tag = new ListViewItemType("d", dir.FullName);
        				if(nItemCount%2 == 0) { // четное
        					item.BackColor = Color.Beige;
        				}
        				listViewSource.Items.Add(item);
        				++nItemCount;
        			}
        			FB2BookDataForDup bd = null;
        			foreach (FileInfo file in dirInfo.GetFiles()) {
        				if(file.Extension.ToLower()==".fb2" || file.Extension.ToLower()==".zip") {
        					item = new ListViewItem(" "+file.Name+" ", file.Extension.ToLower()==".fb2" ? 1 : 2);
        					try {
        						if(file.Extension.ToLower()==".fb2") {
        							if(checkBoxTagsView.Checked) {
        								bd = new FB2BookDataForDup( file.FullName );
        								subItems = new ListViewItem.ListViewSubItem[] {
        									new ListViewItem.ListViewSubItem(item, space+bd.TIBookTitle+space),
        									new ListViewItem.ListViewSubItem(item, space+bd.TISequences+space),
        									new ListViewItem.ListViewSubItem(item, space+bd.TIAuthors+space),
        									new ListViewItem.ListViewSubItem(item, space+CyrillicGenreName(bd.TIGenres)+space),
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
        							if(checkBoxTagsView.Checked) {
        								filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
        								archivesWorker.unzip(dfm.A7zaPath, file.FullName, Settings.Settings.GetTempDir(), ProcessPriorityClass.AboveNormal );
        								string [] files = Directory.GetFiles( Settings.Settings.GetTempDir() );
        								bd = new FB2BookDataForDup( files[0] );
        								subItems = new ListViewItem.ListViewSubItem[] {
        									new ListViewItem.ListViewSubItem(item, space+bd.TIBookTitle+space),
        									new ListViewItem.ListViewSubItem(item, space+bd.TISequences+space),
        									new ListViewItem.ListViewSubItem(item, space+bd.TIAuthors+space),
        									new ListViewItem.ListViewSubItem(item, space+CyrillicGenreName(bd.TIGenres)+space),
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
        					
        					item.Checked = true;
        					item.Tag = new ListViewItemType("f", file.FullName);
        					if(nItemCount%2 == 0) { // четное
        						item.BackColor = Color.AliceBlue;
        					}
        					listViewSource.Items.Add(item);
        					++nItemCount;
        				}
        			}
        			// авторазмер колонок Списка Проводника
        			if(tsmiStartExplorerColumnsAutoReize.Checked) {
        				AutoResizeColumns();
        			}
        		}
        		
        	} catch (System.Exception) {
        	} finally {
        		listViewSource.EndUpdate();
        		Cursor.Current = Cursors.Default;
        	}
        }
		
		// отметить все итемы ListView
		private void CheckAll() {
			m_mscLV.CheckdAllListViewItems( listViewSource, true );
		}
		
		// снять отметки со всех итемов ListView
		private void UnCheckAll() {
			m_mscLV.UnCheckdAllListViewItems( listViewSource.CheckedItems );
		}
		
		// пометить все файлы определенного типа
		private void CheckTypeAllFiles(string sType, bool bCheck) {
			if( listViewSource.Items.Count > 0  ) {
				DirectoryInfo di = null;
				for( int i=0; i!=listViewSource.Items.Count; ++i ) {
					ListViewItemType it = (ListViewItemType)listViewSource.Items[i].Tag;
					if(it.Type == "f") {
						di = new DirectoryInfo(it.Value);
						if(di.Extension.ToLower()=="."+sType.ToLower()) {
							listViewSource.Items[i].Checked = bCheck;
						}
					}
				}
			}
		}
		
		// снять пометку со всех файлов пределенного типа
		private void UnCheckTypeAllFiles(string sType) {
			DirectoryInfo di = null;
			foreach( ListViewItem lvi in listViewSource.CheckedItems ) {
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
		private void CheckAllFiles(bool bCheck) {
			if( listViewSource.Items.Count > 0  ) {
				for( int i=0; i!=listViewSource.Items.Count; ++i ) {
					ListViewItemType it = (ListViewItemType)listViewSource.Items[i].Tag;
					if(it.Type == "f") {
						listViewSource.Items[i].Checked = bCheck;
					}
				}
			}
		}
		
		// снять пометку со всех файлов
		private void UnCheckAllFiles() {
			foreach( ListViewItem lvi in listViewSource.CheckedItems ) {
				ListViewItemType it = (ListViewItemType)lvi.Tag;
				if(it.Type == "f") {
					lvi.Checked = false;
				}
			}
		}
	
		// отметить все папки
		private void CheckAllDirs(bool bCheck) {
			if( listViewSource.Items.Count > 0  ) {
				for( int i=0; i!=listViewSource.Items.Count; ++i ) {
					ListViewItemType it = (ListViewItemType)listViewSource.Items[i].Tag;
					if(it.Type == "d") {
						listViewSource.Items[i].Checked = bCheck;
					}
				}
			}
		}
		
		// снять пометку со всех папок
		private void UnCheckAllDirs() {
			foreach( ListViewItem lvi in listViewSource.CheckedItems ) {
				ListViewItemType it = (ListViewItemType)lvi.Tag;
				if(it.Type == "d") {
					lvi.Checked = false;
				}
			}
		}
		
		private void SortingProgressData() {
            // Отобразим результат сортировки
            /*lvFilesCount.Items[0].SubItems[1].Text = Convert.ToString( m_sv.AllDirs );
            lvFilesCount.Items[1].SubItems[1].Text = Convert.ToString( m_sv.AllFiles );*/
            lvFilesCount.Items[2].SubItems[1].Text = Convert.ToString( m_sv.SourceFB2 );
            lvFilesCount.Items[3].SubItems[1].Text = Convert.ToString( m_sv.Zip );
            lvFilesCount.Items[4].SubItems[1].Text = Convert.ToString( m_sv.Rar );
            lvFilesCount.Items[5].SubItems[1].Text = Convert.ToString( m_sv.A7Zip );
            lvFilesCount.Items[6].SubItems[1].Text = Convert.ToString( m_sv.BZip2 );
            lvFilesCount.Items[7].SubItems[1].Text = Convert.ToString( m_sv.Gzip );
            lvFilesCount.Items[8].SubItems[1].Text = Convert.ToString( m_sv.Tar );
            lvFilesCount.Items[9].SubItems[1].Text = Convert.ToString( m_sv.FB2FromArchives );
            lvFilesCount.Items[10].SubItems[1].Text = Convert.ToString( m_sv.Other );
            lvFilesCount.Items[11].SubItems[1].Text = Convert.ToString( m_sv.CreateInTarget );
            lvFilesCount.Items[12].SubItems[1].Text = Convert.ToString( m_sv.NotRead );
            lvFilesCount.Items[13].SubItems[1].Text = Convert.ToString( m_sv.NotValidFB2 );
            lvFilesCount.Items[14].SubItems[1].Text = Convert.ToString( m_sv.BadArchive );
        }
		
		private void Init() {
			// инициализация контролов и переменных
			for( int i=0; i!=lvFilesCount.Items.Count; ++i ) {
				lvFilesCount.Items[i].SubItems[1].Text	= "0";
			}
			tsProgressBar.Value		= 0;
			tsslblProgress.Text		= Settings.Settings.GetReady();
			tsProgressBar.Visible	= false;
			// очистка временной папки
			filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
			m_sv.Clear();
		}
		
		private void SetFullSortingStartEnabled( bool bEnabled ) {
			// доступность контролов при Полной Сортировки
			tpSelectedSort.Enabled		= bEnabled;
			tpSettings.Enabled			= bEnabled;
			panelAddress.Enabled		= bEnabled;
			listViewSource.Enabled		= bEnabled;
			checkBoxTagsView.Enabled	= bEnabled;
			chBoxScanSubDir.Enabled		= bEnabled;
			buttonSortFilesTo.Enabled	= bEnabled;
			chBoxFSToZip.Enabled		= bEnabled;
			chBoxFSDelFB2Files.Enabled	= bEnabled;
			gBoxFullSortRenameTemplates.Enabled	= bEnabled;
			buttonFullSortStop.Enabled	= !bEnabled;
			tsProgressBar.Visible		= !bEnabled;
			tcSort.Refresh();
			ssProgress.Refresh();
		}
		
		private void SetSelectedSortingStartEnabled( bool bEnabled ) {
			// доступность контролов при Избранной Сортировки
			tpFullSort.Enabled			= bEnabled;
			tpSettings.Enabled			= bEnabled;
			tsbtnSSOpenDir.Enabled		= bEnabled;
			tsbtnSSTargetDir.Enabled	= bEnabled;
			tsbtnSSSortFilesTo.Enabled	= bEnabled;
			pSelectedSortDirs.Enabled	= bEnabled;
			gBoxSelectedlSortRenameTemplates.Enabled	= bEnabled;
			pSSData.Enabled				= bEnabled;
			chBoxSSToZip.Enabled		= bEnabled;
			chBoxSSDelFB2Files.Enabled	= bEnabled;
			tsbtnSSSortStop.Enabled		= !bEnabled;
			tsProgressBar.Visible		= !bEnabled;
			tcSort.Refresh();
			ssProgress.Refresh();
		}
		
		private bool IsSourceDirDataCorrect()
		{
			// Полная Сортировка: проверка на корректность данных папок источника и приемника файлов
			// проверки на корректность папок источника и приемника
			if( m_sSource.Length == 0 ) {
				MessageBox.Show( "Выберите папку для сканирования!", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			DirectoryInfo diFolder = new DirectoryInfo( m_sSource );
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка не найдена: " + m_sSource, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка папки-приемника и создание ее, если нужно
			if( !filesWorker.CreateDirIfNeed( m_sTarget, m_sMessTitle ) ) {
				return false;
			}

			return true;
		}
		
		private bool IsFoldersDataCorrect()
		{
			// Селективная Сортировка: проверка на корректность данных папок источника и приемника файлов
			// проверки на корректность папок источника и приемника
			if( m_sSource.Length == 0 ) {
				MessageBox.Show( "Выберите папку для сканирования!", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			DirectoryInfo diFolder = new DirectoryInfo( m_sSource );
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка не найдена: " + m_sSource, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			if( m_sTarget.Length == 0 ) {
				MessageBox.Show( "Не указана папка-приемник файлов!\nРабота прекращена.", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			if( m_sSource == m_sTarget ) {
				MessageBox.Show( "Папка-приемник файлов совпадает с папкой сканирования!\nРабота прекращена.", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка папки-приемника и создание ее, если нужно
			if( !filesWorker.CreateDirIfNeed( m_sTarget, m_sMessTitle ) ) {
				return false;
			}
			return true;
		}
		
		private void ReadFMTempData() {
			// чтение данных Полной Сортировки из xml-файла
			this.checkBoxTagsView.Click -= new System.EventHandler(this.CheckBoxTagsViewClick);
			checkBoxTagsView.Checked = Settings.FileManagerSettings.ReadXmlFullSortingBooksTagsView();
			this.checkBoxTagsView.Click += new System.EventHandler(this.CheckBoxTagsViewClick);
			
			textBoxAddress.Text = Settings.FileManagerSettings.ReadXmlFullSortingSourceDir();
			txtBoxTemplatesFromLine.Text = Settings.FileManagerSettings.ReadXmlFullSortingTemplate();
			chBoxScanSubDir.Checked = Settings.FileManagerSettings.ReadXmlFullSortingInSubDir();
			tsmiStartExplorerColumnsAutoReize.Checked = Settings.FileManagerSettings.ReadXmlFullSortingStartExplorerColumnsAutoReize();
			chBoxFSToZip.Checked = Settings.FileManagerSettings.ReadXmlFullSortingToZip();
			chBoxFSDelFB2Files.Checked = Settings.FileManagerSettings.ReadXmlFullSortingDelFB2Files();
			
			// чтение данных Избранной Сортировки из xml-файла
			tboxSSSourceDir.Text = Settings.FileManagerSettings.ReadXmlSelectedSortingSourceDir();
			tboxSSToDir.Text = Settings.FileManagerSettings.ReadXmlSelectedSortingTargetDir();
			txtBoxSSTemplatesFromLine.Text = Settings.FileManagerSettings.ReadXmlSelectedSortingTemplate();
			chBoxSSScanSubDir.Checked = Settings.FileManagerSettings.ReadXmlSelectedSortingInSubDir();
			chBoxSSToZip.Checked = Settings.FileManagerSettings.ReadXmlSelectedSortingToZip();
			chBoxSSDelFB2Files.Checked = Settings.FileManagerSettings.ReadXmlSelectedSortingDelFB2Files();

			if(File.Exists(Settings.FileManagerSettings.FileManagerSettingsPath)) {
				GenerateSourceList(Settings.FileManagerSettings.FullSortingSourceDir);
			}
		}
		
		private void IncArchiveInfo( string sExt ) {
			// Увеличить число определенного файла-архива на 1
			switch( sExt ) {
				case ".rar":
					//IncStatus( 4 );
					++m_sv.Rar;
					break;
				case ".zip":
					//IncStatus( 3 );
					++m_sv.Zip;
					break;
				case ".7z":
					//IncStatus( 5 );
					++m_sv.A7Zip;
					break;
				case ".bz2":
					//IncStatus( 6 );
					++m_sv.BZip2;
					break;
				case ".gz":
					//IncStatus( 7 );
					++m_sv.Gzip;
					break;
				case ".tar":
					//IncStatus( 8 );
					++m_sv.Tar;
					break;
			}
		}
		
		private void CreateFileTo( string sFromFilePath, string sToFilePath, int nFileExistMode,
		                          	bool bAddToFileNameBookIDMode, Settings.DataFM dfm ) {
			// создание нового файла или архива
			bool ToZip = m_bFullSort ? Settings.FileManagerSettings.FullSortingToZip
					            	 : Settings.FileManagerSettings.SelectedSortingToZip;
			try {
				if( !ToZip ) {
					CopyFileToTargetDir( sFromFilePath, sToFilePath, false, nFileExistMode, bAddToFileNameBookIDMode );
				} else {
					// упаковка в архив
					CopyFileToArchive( dfm.A7zaPath, sFromFilePath, sToFilePath+".zip", nFileExistMode, bAddToFileNameBookIDMode );
				}
			} catch ( System.IO.PathTooLongException ) {
				string sFileLongPathDir = dfm.FileLongPathDir;
				Directory.CreateDirectory( sFileLongPathDir );
				sToFilePath = sFileLongPathDir+"\\"+Path.GetFileName( sFromFilePath );
				CopyFileToTargetDir( sFromFilePath, sToFilePath, true, nFileExistMode, false );	
			}
		}
		
		private void CopyFileToArchive( string s7zaPath, string sFromFilePath, string sToFilePath,
		                               int nFileExistMode, bool bAddToFileNameBookIDMode ) {
			// архивирование файла с сформированным именем (путь)
			// обработка уже существующих файлов в папке
			Regex rx = new Regex( @"\\+" );
			sFromFilePath = rx.Replace( sFromFilePath, "\\" );
			sToFilePath = rx.Replace( sToFilePath, "\\" );
			
			sToFilePath = FileExsistWorker( sFromFilePath, sToFilePath, nFileExistMode, bAddToFileNameBookIDMode, "zip" );
			archivesWorker.zip( s7zaPath, "zip", sFromFilePath, sToFilePath, ProcessPriorityClass.AboveNormal );
			//IncStatus( 11 ); // всего создано
			++m_sv.CreateInTarget;
		}
		
		private void CopyFileToTargetDir( string sFromFilePath, string sToFilePath, bool bBad,
		                                 int nFileExistMode, bool bAddToFileNameBookIDMode )
		{
			// копирование файла с сформированным именем (путь)
			Regex rx = new Regex( @"\\+" );
			sFromFilePath = rx.Replace( sFromFilePath, "\\" );
			sToFilePath = rx.Replace( sToFilePath, "\\" );
			// обработка уже существующих файлов в папке
			sToFilePath = FileExsistWorker( sFromFilePath, sToFilePath, nFileExistMode, bAddToFileNameBookIDMode, "" );
			if( File.Exists( sFromFilePath ) ) {
				File.Copy( sFromFilePath, sToFilePath );
				if( !bBad ) {
					if( File.Exists( sToFilePath ) ) {
					   	//IncStatus( 11 ); // всего создано
					   	++m_sv.CreateInTarget;
					}
				}
			}
		}
		
		private void CopyBadArchiveToBadDir( string sFromFilePath, string sSource, string sToDir, int nFileExistMode, bool bDeleteOriginal )
		{
			// копирование "битого" с сформированным именем (путь)
			string sToFilePath = sToDir+"\\"+sFromFilePath.Remove( 0, sSource.Length );
			Regex rx = new Regex( @"\\+" );
			sFromFilePath = rx.Replace( sFromFilePath, "\\" );
			sToFilePath = rx.Replace( sToFilePath, "\\" );
			string sSufix = "";
			FileInfo fi = new FileInfo( sToFilePath );
			if( !fi.Directory.Exists ) {
				Directory.CreateDirectory( fi.Directory.ToString() );
			}
			// обработка уже существующих файлов в папке
			if( File.Exists( sToFilePath ) ) {
				if( nFileExistMode == 0 ) {
					File.Delete( sToFilePath );
				} else {
					if( nFileExistMode == 1 ) {
						// Добавить к создаваемому архиву очередной номер
						sSufix += "_" + stringProcessing.GetFileNewNumber( sToFilePath ).ToString();
					} else {
						// Добавить к создаваемому архиву дату и время
						sSufix += "_" + stringProcessing.GetDateTimeExt();
					}
					sToFilePath = sToFilePath.Remove( sToFilePath.Length-4 ) + sSufix + Path.GetExtension( sToFilePath );
				}
			}
			if( File.Exists( sFromFilePath ) ) {
				if( !bDeleteOriginal ) {
					File.Copy( sFromFilePath, sToFilePath );
				} else {
					File.Move( sFromFilePath, sToFilePath );
				}
			}
			//IncStatus( 14 ); // "битые" архивы - не открылись
			++m_sv.BadArchive;
		}
		
		private string FileExsistWorker( string sFromFilePath, string sToFilePath, int nFileExistMode,
		                                bool bAddToFileNameBookIDMode, string sArchType )
		{
			// обработка уже существующих файлов в папке
			string sSufix = "";
			FileInfo fi = new FileInfo( sToFilePath );
			if( !fi.Directory.Exists ) {
				Directory.CreateDirectory( fi.Directory.ToString() );
			}
			if( File.Exists( sToFilePath ) ) {
				if( nFileExistMode == 0 ) {
					File.Delete( sToFilePath );
				} else {
					if( bAddToFileNameBookIDMode ) {
						sSufix = "_" + stringProcessing.GetFMBookID( sFromFilePath );
					}
					if( nFileExistMode == 1 ) {
						// Добавить к создаваемому файлу очередной номер
						sSufix += "_" + stringProcessing.GetFileNewNumber( sToFilePath ).ToString();
					} else {
						// Добавить к создаваемому файлу дату и время
						sSufix += "_" + stringProcessing.GetDateTimeExt();
					}
					if( sArchType.Length==0 ) {
						sToFilePath = sToFilePath.Remove( sToFilePath.Length-4 ) + sSufix + ".fb2";
					} else {
						sToFilePath = sToFilePath.Remove( sToFilePath.Length - (sArchType.Length+5) ) + sSufix + ".fb2." + sArchType;
					}
				}
			}
			return sToFilePath;
		}
		
		private void MakeFileFor1Genre1Author( string sFromFilePath, string sSource, string sTarget,
		                                      List<templatesLexemsSimple> lSLexems, Settings.DataFM dfm ) {
			// создаем файл по новому пути для первого Жанра и для первого Автора Книги
			string sExt = Path.GetExtension( sFromFilePath ).ToLower();
			if( sExt==".fb2" ) {
				MakeFileFor1Genre1AuthorWorker( sExt, sFromFilePath, sSource, sTarget, lSLexems, dfm, false );
				//IncStatus( 2 ); // исходные fb2-файлы
				++m_sv.SourceFB2;
			} else {
				// это архив?
				if( archivesWorker.IsArchive( sExt ) ) {
					bool DelFB2Files = m_bFullSort ? Settings.FileManagerSettings.FullSortingDelFB2Files
					            	 				: Settings.FileManagerSettings.SelectedSortingDelFB2Files;
					List<string> lFilesListFromArchive = archivesWorker.GetFileListFromArchive( sFromFilePath, Settings.Settings.GetTempDir(), dfm.A7zaPath, dfm.UnRarPath );
					IncArchiveInfo( sExt ); // Увеличить число определенного файла-архива на 1
					if( lFilesListFromArchive==null ) {
						CopyBadArchiveToBadDir( sFromFilePath, sSource, dfm.NotOpenArchDir, dfm.FileExistMode, DelFB2Files );
						return; // не получилось открыть архив - "битый"
					}
					foreach( string sFB2FromArchPath in lFilesListFromArchive ) {
						MakeFileFor1Genre1AuthorWorker( Path.GetExtension( sFB2FromArchPath ).ToLower(), sFB2FromArchPath,
						                               sSource, sTarget, lSLexems, dfm, true );
						//IncStatus( 9 ); // Исходные fb2-файлы из архивов
						++m_sv.FB2FromArchives;
					}
					filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
					if( DelFB2Files ) {
						// удаляем исходный fb2-файл
						if( File.Exists( sFromFilePath ) ) {
							File.Delete( sFromFilePath );
						}
					}
				}  else {
					// пропускаем не fb2-файлы и архивы
					//IncStatus( 10 ); // другие файлы
					++m_sv.Other;
				}
			}
		}
		private void MakeFileFor1Genre1AuthorWorker( string sExt, string sFromFilePath, string sSource, string sTarget,
		                                      List<templatesLexemsSimple> lSLexems, Settings.DataFM dfm, bool bFromArchive ) {
			try {
				MakeFB2File( sFromFilePath, sSource, sTarget, lSLexems, dfm, bFromArchive, 0, 0 );
			} catch {
				if( sExt==".fb2" ) {
					CopyBadFileToDir( sFromFilePath, sSource, bFromArchive, dfm.NotReadFB2Dir, dfm.FileExistMode );
				}
			}
		}
		
		private void MakeFileForAllGenre1Author( string sFromFilePath, string sSource, string sTarget,
		                                      List<templatesLexemsSimple> lSLexems, Settings.DataFM dfm ) {
			// создаем файл по новому пути для всех Жанров и для первого Автора Книги
			string sExt = Path.GetExtension( sFromFilePath ).ToLower();
			if( sExt==".fb2" ) {
				MakeFileForAllGenre1AuthorWorker( sExt, sFromFilePath, sSource, sTarget, lSLexems, dfm, false ) ;
				//IncStatus( 2 ); // исходные fb2-файлы
				++m_sv.SourceFB2;
			} else {
				// это архив?
				if( archivesWorker.IsArchive( sExt ) ) {
					bool DelFB2Files = m_bFullSort ? Settings.FileManagerSettings.FullSortingDelFB2Files
					            	 				: Settings.FileManagerSettings.SelectedSortingDelFB2Files;
					List<string> lFilesListFromArchive = archivesWorker.GetFileListFromArchive( sFromFilePath, Settings.Settings.GetTempDir(), dfm.A7zaPath, dfm.UnRarPath );
					IncArchiveInfo( sExt ); // Увеличить число определенного файла-архива на 1
					if( lFilesListFromArchive==null ) {
						CopyBadArchiveToBadDir( sFromFilePath, sSource, dfm.NotOpenArchDir, dfm.FileExistMode, DelFB2Files);
						return; // не получилось открыть архив - "битый"
					}
					foreach( string sFB2FromArchPath in lFilesListFromArchive ) {
						MakeFileForAllGenre1AuthorWorker( Path.GetExtension( sFB2FromArchPath ).ToLower(), sFB2FromArchPath,
						                                 sSource, sTarget, lSLexems, dfm, true ) ;
						//IncStatus( 9 ); // Исходные fb2-файлы из архивов
						++m_sv.FB2FromArchives;
					}
					filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
					if( DelFB2Files ) {
						// удаляем исходный fb2-файл
						if( File.Exists( sFromFilePath ) ) {
							File.Delete( sFromFilePath );
						}
					}
				}  else {
					// пропускаем не fb2-файлы и архивы
					//IncStatus( 10 ); // другие файлы
					++m_sv.Other;
				}
			}
		}
		private void MakeFileForAllGenre1AuthorWorker( string sExt, string sFromFilePath, string sSource, string sTarget,
		                                      List<templatesLexemsSimple> lSLexems, Settings.DataFM dfm, bool bFromArchive ) {
			try {
				fB2Parser fb2 = new fB2Parser( sFromFilePath );
				TitleInfo ti = fb2.GetTitleInfo();
				IList<Genre> lGenres = ti.Genres;
				IList<Author> lAuthors = ti.Authors;
				for( int i=0; i!=lGenres.Count; ++i ) {
					MakeFB2File( sFromFilePath, sSource, sTarget, lSLexems, dfm, bFromArchive, i, 0 );
				}
			} catch {
				if( sExt==".fb2" ) {
					CopyBadFileToDir( sFromFilePath, sSource, bFromArchive, dfm.NotReadFB2Dir, dfm.FileExistMode );
				}
			}
		}

		private void MakeFileFor1GenreAllAuthor( string sFromFilePath, string sSource, string sTarget,
		                                      	List<templatesLexemsSimple> lSLexems, Settings.DataFM dfm ) {
			// создаем файл по новому пути для первого Жанра и для всех Авторов Книги
			string sExt = Path.GetExtension( sFromFilePath ).ToLower();
			if( sExt==".fb2" ) {
				MakeFileFor1GenreAllAuthorWorker( sExt, sFromFilePath, sSource, sTarget, lSLexems, dfm, false );
				//IncStatus( 2 ); // исходные fb2-файлы
				++m_sv.SourceFB2;
			} else {
				// это архив?
				if( archivesWorker.IsArchive( sExt ) ) {
					bool DelFB2Files = m_bFullSort ? Settings.FileManagerSettings.FullSortingDelFB2Files
					            	 				: Settings.FileManagerSettings.SelectedSortingDelFB2Files;
					List<string> lFilesListFromArchive = archivesWorker.GetFileListFromArchive( sFromFilePath, Settings.Settings.GetTempDir(), dfm.A7zaPath, dfm.UnRarPath );
					IncArchiveInfo( sExt ); // Увеличить число определенного файла-архива на 1
					if( lFilesListFromArchive==null ) {
						CopyBadArchiveToBadDir( sFromFilePath, sSource, dfm.NotOpenArchDir, dfm.FileExistMode, DelFB2Files );
						return; // не получилось открыть архив - "битый"
					}
					foreach( string sFB2FromArchPath in lFilesListFromArchive ) {
						MakeFileFor1GenreAllAuthorWorker( Path.GetExtension( sFB2FromArchPath ).ToLower(), sFB2FromArchPath,
						                                 sSource, sTarget, lSLexems, dfm, true );
						//IncStatus( 9 ); // Исходные fb2-файлы из архивов
						++m_sv.FB2FromArchives;
					}
					filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
					if( DelFB2Files ) {
						// удаляем исходный fb2-файл
						if( File.Exists( sFromFilePath ) ) {
							File.Delete( sFromFilePath );
						}
					}
				} else {
					// пропускаем не fb2-файлы и архивы
					//IncStatus( 10 ); // другие файлы
					++m_sv.Other;
				}
			}
		}
		private void MakeFileFor1GenreAllAuthorWorker( string sExt, string sFromFilePath, string sSource, string sTarget,
		                                      List<templatesLexemsSimple> lSLexems, Settings.DataFM dfm, bool bFromArchive ) {
			try {
				fB2Parser fb2 = new fB2Parser( sFromFilePath );
				TitleInfo ti = fb2.GetTitleInfo();
				IList<Genre> lGenres = ti.Genres;
				IList<Author> lAuthors = ti.Authors;
				for( int i=0; i!=lAuthors.Count; ++i ) {
					MakeFB2File( sFromFilePath, sSource, sTarget, lSLexems, dfm, bFromArchive, 0, i );
				}
			} catch {
				if( sExt==".fb2" ) {
					CopyBadFileToDir( sFromFilePath, sSource, bFromArchive, dfm.NotReadFB2Dir, dfm.FileExistMode );
				}
			}
		}
		
		private void MakeFileForAllGenreAllAuthor( string sFromFilePath, string sSource, string sTarget,
		                                      		List<templatesLexemsSimple> lSLexems, Settings.DataFM dfm) {
			// создаем файл по новому пути для всех Жанров и для всех Авторов Книги
			string sExt = Path.GetExtension( sFromFilePath ).ToLower();
			if( sExt==".fb2" ) {
				MakeFileForAllGenreAllAuthorWorker( sExt, sFromFilePath, sSource, sTarget, lSLexems, dfm, false );
				//IncStatus( 2 ); // исходные fb2-файлы
				++m_sv.SourceFB2;
			} else {
				// это архив?
				if( archivesWorker.IsArchive( sExt ) ) {
					bool DelFB2Files = m_bFullSort ? Settings.FileManagerSettings.FullSortingDelFB2Files
					            	 				: Settings.FileManagerSettings.SelectedSortingDelFB2Files;
					List<string> lFilesListFromArchive = archivesWorker.GetFileListFromArchive( sFromFilePath, Settings.Settings.GetTempDir(), dfm.A7zaPath, dfm.UnRarPath );
					IncArchiveInfo( sExt ); // Увеличить число определенного файла-архива на 1
					if( lFilesListFromArchive==null ) {
						CopyBadArchiveToBadDir( sFromFilePath, sSource, dfm.NotOpenArchDir, dfm.FileExistMode, DelFB2Files );
						return; // не получилось открыть архив - "битый"
					}
					foreach( string sFB2FromArchPath in lFilesListFromArchive ) {
						MakeFileForAllGenreAllAuthorWorker( Path.GetExtension( sFB2FromArchPath ).ToLower(), sFB2FromArchPath,
						                                   sSource, sTarget, lSLexems, dfm, true );
						//IncStatus( 9 ); // Исходные fb2-файлы из архивов
						++m_sv.FB2FromArchives;
					}
					filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
					if( DelFB2Files ) {
						// удаляем исходный fb2-файл
						if( File.Exists( sFromFilePath ) ) {
							File.Delete( sFromFilePath );
						}
					}
				}  else {
					// пропускаем не fb2-файлы и архивы
					//IncStatus( 10 ); // другие файлы
					++m_sv.Other;
				}
			}
		}
		private void MakeFileForAllGenreAllAuthorWorker( string sExt, string sFromFilePath, string sSource, string sTarget,
		                                      List<templatesLexemsSimple> lSLexems, Settings.DataFM dfm, bool bFromArchive ) {
			try {
				fB2Parser fb2 = new fB2Parser( sFromFilePath );
				TitleInfo ti = fb2.GetTitleInfo();
				IList<Genre> lGenres = ti.Genres;
				IList<Author> lAuthors = ti.Authors;
				for( int i=0; i!= lGenres.Count; ++i ) {
					for( int j=0; j!=lAuthors.Count; ++j ) {
						MakeFB2File( sFromFilePath, sSource, sTarget, lSLexems, dfm, bFromArchive, i, j );
					}
				}
			} catch {
				if( sExt==".fb2" ) {
					CopyBadFileToDir( sFromFilePath, sSource, bFromArchive, dfm.NotReadFB2Dir, dfm.FileExistMode );
				}
			}
		}
		
		private bool IsValid( string sFromFilePath, string sSource, Settings.DataFM dfm,
		                     bool bFromArchive, int nGenreIndex, int nAuthorIndex ) {
			// если режим сортировки - только валидные - то проверка и копирование невалидных в папку
			string sResult = fv2V.ValidatingFB2File( sFromFilePath );
			if ( sResult.Length != 0 ) {
				// защита от многократного копирования невалимдного файла в папку для невалидных
				if( nGenreIndex==0 && nAuthorIndex==0 ) {
					// помещаем его в папку для невалидных файлов
					CopyBadFileToDir( sFromFilePath, sSource, bFromArchive, dfm.NotValidFB2Dir, dfm.FileExistMode );
					//IncStatus( 13 ); // не валидные fb2-файлы
					++m_sv.NotValidFB2;
					return false; // файл невалидный - пропускаем его, сортируем дальше
				} else {
					return false; // файл уже скопирован - пропускаем его, сортируем дальше
				}
			}
			return true;
		}
		
		private void CopyBadFileToDir( string sFromFilePath, string sSource, bool bFromArchive,
		                              string sBadDir, int nFileExistMode ) {
			// нечитаемый fb2-файл или архив - копируем его в папку Bad
			Directory.CreateDirectory( sBadDir );
			string sFrom = ( !bFromArchive ? sSource : Settings.Settings.GetTempDir() );
			string sToFilePath = sBadDir+"\\"+sFromFilePath.Remove( 0, sFrom.Length );
			CopyFileToTargetDir( sFromFilePath, sToFilePath, true, nFileExistMode, false );
			//IncStatus( 12 ); // нечитаемые fb2-файлы или архивы
			++m_sv.NotRead;
		}
			
		private void MakeFB2File( string sFromFilePath, string sSource, string sTarget,
		                      List<templatesLexemsSimple> lSLexems, Settings.DataFM dfm,
		                      bool bFromArchive, int nGenreIndex, int nAuthorIndex ) {
			// создаем файл по новому пути
			string sTempDir = Settings.Settings.GetTempDir();
			// смотрим, что это за файл
			string sExt = Path.GetExtension( sFromFilePath ).ToLower();
			if( sExt == ".fb2" ) {
				// обработка fb2-файла
				// тип сортировки
				if( !dfm.SortValidType  ) {
					if( !IsValid( sFromFilePath, sSource, dfm, bFromArchive, nGenreIndex, nAuthorIndex ) ) {
						return;
					}
				}
				try {
					string sToFilePath = sTarget + "\\" +
							templatesParser.Parse( sFromFilePath, lSLexems, dfm, nGenreIndex, nAuthorIndex ) + ".fb2";
					CreateFileTo( sFromFilePath, sToFilePath, dfm.FileExistMode, dfm.AddToFileNameBookIDMode, dfm );
				} catch /*( System.IO.FileLoadException )*/ {
					// нечитаемый fb2-файл - копируем его в папку Bad
					CopyBadFileToDir( sFromFilePath, sSource, bFromArchive, dfm.NotReadFB2Dir, dfm.FileExistMode );
				}

				if( m_bFullSort ? Settings.FileManagerSettings.FullSortingDelFB2Files
					            : Settings.FileManagerSettings.SelectedSortingDelFB2Files ) {
					// удаляем исходный fb2-файл
					if( File.Exists( sFromFilePath ) ) {
						File.Delete( sFromFilePath );
					}
				}
			}
		}
		
		private void IncStatus( int nItem ) {
			lvFilesCount.Items[nItem].SubItems[1].Text	=
					Convert.ToString( 1+Convert.ToInt32( lvFilesCount.Items[nItem].SubItems[1].Text ) );
		}

		private bool IsArchivatorsExist() {
			// проверка на наличие архиваторов
			string s7zPath	= Settings.Settings.Read7zaPath();
			string sRarPath	= Settings.Settings.ReadRarPath();
			bool ToZip = m_bFullSort ? Settings.FileManagerSettings.FullSortingToZip
					            	 : Settings.FileManagerSettings.SelectedSortingToZip;
			if( ToZip ) {
				if( s7zPath.Trim().Length==0 ) {
					MessageBox.Show( "В Настройках не указана папка с установленным консольным 7Zip-архиватором!\nУкажите путь к нему в Настройках.\nРабота остановлена!",
					                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return false;
				} else {
					if( !File.Exists( s7zPath ) ) {
						MessageBox.Show( "Не найден файл Zip-архиватора \""+s7zPath+"\"!\nУкажите путь к нему в Настройках.\nРабота остановлена!",
						                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
						return false;
					}
				}
			}
			return true;
		}
		
		private bool IsLineTemplateCorrect( string sLineTemplate ) {
			// проверки на корректность шаблонных строк
			// проверка "пустоту" строки с шаблонами
			if( sLineTemplate.Length == 0 ) {
				MessageBox.Show( "Строка шаблонов не может быть пустой!\nРабота прекращена.", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на корректность строки с шаблонами
			if( !templatesVerify.IsLineTemplatesCorrect( sLineTemplate ) ) {
				MessageBox.Show( "Строка содержит или недопустимые шаблоны,\nили недопустимые символы */|?<>\"&\\t\\r\\n между шаблонами!\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на четность * в строке с шаблонами
			if( !templatesVerify.IsEvenElements( sLineTemplate, '*' ) ) {
				MessageBox.Show( "Строка с шаблонами подстановки содержит нечетное число *!\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка, не стоит ли ] перед [
			if( sLineTemplate.IndexOf('[')!=-1 && sLineTemplate.IndexOf(']')!=-1 ) {
				if( sLineTemplate.IndexOf('[') > sLineTemplate.IndexOf(']') ) {
					MessageBox.Show( "В строке с шаблонами закрывающая скобка ] не может стоять перед открывающей [ !\nРабота прекращена.",
					                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return false;
				}
			}
			// проверка на соответствие [ ] в строке с шаблонами
			if( !templatesVerify.IsBracketsCorrect( sLineTemplate, '[', ']' ) ) {
				MessageBox.Show( "В строке с шаблонами переименования нет соответствия между открывающим и закрывающими скобками [ ]!\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на соответствие ( ) в строке с шаблонами
			if( !templatesVerify.IsBracketsCorrect( sLineTemplate, '(', ')' ) ) {
				MessageBox.Show( "В строке с шаблонами нет соответствия между открывающим и закрывающими скобками ( )!\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на \ в начале строки с шаблонами
			if( sLineTemplate[0]=='\\' ) {
				MessageBox.Show( "Строка с шаблонами не может начинаться с '\\'!\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на \ в конце строки с шаблонами
			if( sLineTemplate[sLineTemplate.Length-1]=='\\' ) {
				MessageBox.Show( "Строка с шаблонами не может заканчиваться на '\\' !\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка условных шаблонов на наличие в них вспом. символов без самих шаблонов
			if( !templatesVerify.IsConditionalPatternCorrect( sLineTemplate ) ) {
				MessageBox.Show( "Условные шаблоны [] в строке с шаблонами не могут содержать вспомогательных символов БЕЗ самих шаблонов!\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на множественность символа папки \ в строке с шаблонами
			if( sLineTemplate.IndexOf( "\\\\" )!=-1 ) {
				MessageBox.Show( "Строка с шаблонами не может содержать несколько идущих подряд символов папки '\\' !\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			return true;
		}
		
		private void MakeFileForSelectedSortingWorker( string sFromFilePath, string sSource, string sTarget,
		                                             List<templatesLexemsSimple> lSLexems, Settings.DataFM dfm ) {
			// Создание файла по критериям Избранной сортировки
			// проверка, соответствует ли текущий файл критерия поиска для Избранной Сортировки
			FB2SelectedSorting fb2ss = new FB2SelectedSorting();
			if( fb2ss.IsConformity( sFromFilePath, m_lSSQCList ) ) {
				if( dfm.GenreOneMode && dfm.AuthorOneMode ) {
					// по первому Жанру и первому Автору Книги
					MakeFileFor1Genre1Author( sFromFilePath, sSource, sTarget, lSLexems, dfm );
				} else if( dfm.GenreOneMode && !dfm.AuthorOneMode ) {
					// по первому Жанру и всем Авторам Книги
					MakeFileFor1GenreAllAuthor( sFromFilePath, sSource, sTarget, lSLexems, dfm );
				} else if( !dfm.GenreOneMode && dfm.AuthorOneMode ) {
					// по всем Жанрам и первому Автору Книги
					MakeFileForAllGenre1Author( sFromFilePath, sSource, sTarget, lSLexems, dfm );
				} else {
					// по всем Жанрам и всем Авторам Книги
					MakeFileForAllGenreAllAuthor( sFromFilePath, sSource, sTarget, lSLexems, dfm );
				}
			}
		}
		#endregion
		
		#region Обработчики событий
		void TsmiColumnsExplorerAutoReizeClick(object sender, EventArgs e)
		{
			AutoResizeColumns();
		}
		
		void TsmiStartExplorerColumnsAutoReizeCheckedChanged(object sender, EventArgs e)
		{
			Settings.FileManagerSettings.StartExplorerColumnsAutoReize = tsmiStartExplorerColumnsAutoReize.Checked;
			if(tsmiStartExplorerColumnsAutoReize.Checked) {
				AutoResizeColumns();
			}
		}
		
		void CheckBoxTagsViewClick(object sender, EventArgs e)
		{
			// Отображать/скрывать описание книг
			if(checkBoxTagsView.Checked) {
				if(File.Exists(Settings.FileManagerSettings.FileManagerSettingsPath)) {
					if(Settings.FileManagerSettings.ReadXmlFullSortingViewMessageForLongTime()) {
						string sMess = "При включении этой опции для создания списка книг с их описанием может потребоваться очень много времени!\nБольше не показывать это сообщение?";
						DialogResult result = MessageBox.Show( sMess, "Отображение описания книг", MessageBoxButtons.YesNo, MessageBoxIcon.Question );
						Settings.FileManagerSettings.ViewMessageForLongTime = (result == DialogResult.Yes) ? false : true;
					}
				}
			}
			
			Settings.FileManagerSettings.BooksTagsView = checkBoxTagsView.Checked;
			Settings.FileManagerSettings.WriteFileManagerSettings();
			if( listViewSource.Items.Count > 0 ) {
				Cursor.Current = Cursors.WaitCursor;
				listViewSource.BeginUpdate();
				DirectoryInfo di = null;
				FB2BookDataForDup bd = null;
				Settings.DataFM dfm = new Settings.DataFM();
				for(int i=0; i!= listViewSource.Items.Count; ++i) {
					ListViewItemType it = (ListViewItemType)listViewSource.Items[i].Tag;
					if(it.Type=="f") {
						di = new DirectoryInfo(it.Value);
						if(checkBoxTagsView.Checked) {
							// показать данные книг
							try {
								if(di.Extension.ToLower()==".fb2") {
									// показать данные fb2 файлов
									bd = new FB2BookDataForDup( it.Value );
									listViewSource.Items[i].SubItems[1].Text = space+bd.TIBookTitle+space;
									listViewSource.Items[i].SubItems[2].Text = space+bd.TISequences+space;
									listViewSource.Items[i].SubItems[3].Text = space+bd.TIAuthors+space;
									listViewSource.Items[i].SubItems[4].Text = space+CyrillicGenreName(bd.TIGenres)+space;
									listViewSource.Items[i].SubItems[5].Text = space+bd.TILang+space;
									listViewSource.Items[i].SubItems[6].Text = space+bd.Encoding+space;
								} else if(di.Extension.ToLower()==".zip") {
									if(checkBoxTagsView.Checked) {
										// показать данные архивов
										filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
										archivesWorker.unzip(dfm.A7zaPath, it.Value, Settings.Settings.GetTempDir(), ProcessPriorityClass.AboveNormal );
										string [] files = Directory.GetFiles( Settings.Settings.GetTempDir() );
										bd = new FB2BookDataForDup( files[0] );
										listViewSource.Items[i].SubItems[1].Text = space+bd.TIBookTitle+space;
										listViewSource.Items[i].SubItems[2].Text = space+bd.TISequences+space;
										listViewSource.Items[i].SubItems[3].Text = space+bd.TIAuthors+space;
										listViewSource.Items[i].SubItems[4].Text = space+CyrillicGenreName(bd.TIGenres)+space;
										listViewSource.Items[i].SubItems[5].Text = space+bd.TILang+space;
										listViewSource.Items[i].SubItems[6].Text = space+bd.Encoding+space;
									}
								}
							} catch(System.Exception) {
								listViewSource.Items[i].ForeColor = Color.Blue;
							}
						} else {
							// скрыть данные книг
							for(int j=1; j!=listViewSource.Items[i].SubItems.Count; ++j) {
								listViewSource.Items[i].SubItems[j].Text = "";
							}
						}
					}
				}
				// авторазмер колонок Списка
				if(tsmiStartExplorerColumnsAutoReize.Checked) {
					AutoResizeColumns();
				}
				listViewSource.EndUpdate();
				Cursor.Current = Cursors.Default;
			}
		}
		
		void ButtonOpenSourceDirClick(object sender, EventArgs e)
		{
			// задание папки с fb2-файлами и архивами для сканирования
			if(filesWorker.OpenDirDlg( textBoxAddress, fbdScanDir, "Укажите папку для сканирования с fb2-файлами и архивами:" )) {
				ButtonGoClick(sender, e);
			}
		}
		
		void ButtonGoClick(object sender, EventArgs e)
		{
			// переход на заданную папку-источник fb2-файлов
			string s = textBoxAddress.Text.Trim();
			if(s != "") {
				DirectoryInfo info = new DirectoryInfo(s);
				if(info.Exists) {
					GenerateSourceList(info.FullName);
				} else {
					MessageBox.Show( "Не удается найти папку " + textBoxAddress.Text + ".\nПроверьте правильность пути.", "Переход по выбранному адресу", MessageBoxButtons.OK, MessageBoxIcon.Error );
				}
			}
		}
		
		void TextBoxAddressKeyPress(object sender, KeyPressEventArgs e)
		{
			// обработка нажатия клавиш в поле ввода пути к папке-источнику
			if ( e.KeyChar == (char)Keys.Return ) {
				// отображение папок и/или фалов в заданной папке
				ButtonGoClick( sender, e );
			}
		}
		
		void ListViewSourceDoubleClick(object sender, EventArgs e)
		{
			// переход в выбранную папку
			if( listViewSource.Items.Count > 0 && listViewSource.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = listViewSource.SelectedItems;
				ListViewItemType it = (ListViewItemType)si[0].Tag;
				if(it.Type=="d" || it.Type=="dUp") {
					textBoxAddress.Text = it.Value;
					GenerateSourceList(it.Value);
				}
			}
		}
		
		void ListViewSourceKeyPress(object sender, KeyPressEventArgs e)
		{
			// обработка нажатия клавиш на списке папок и файлов
			if ( e.KeyChar == (char)Keys.Return ) {
				// переход в выбранную папку
				ListViewSourceDoubleClick(sender, e);
			} else if ( e.KeyChar == (char)Keys.Back ) {
				// переход на каталог выше
				ListViewItemType it = (ListViewItemType)listViewSource.Items[0].Tag;
				textBoxAddress.Text = it.Value;
				GenerateSourceList(it.Value);
			}
		}
		
		void TsmiCheckedAllClick(object sender, EventArgs e)
		{
			// Пометить все файлы и папки
			ConnectListsEventHandlers( false );
			CheckAll();
			if(listViewSource.Items.Count > 0) {
				listViewSource.Items[0].Checked = false;
			}
			ConnectListsEventHandlers( true );
		}
		
		void TsmiUnCheckedAllClick(object sender, EventArgs e)
		{
			// Снять отметки со всех файлов и папок
			ConnectListsEventHandlers( false );
			UnCheckAll();
			ConnectListsEventHandlers( true );
		}
		
		void TsmiFilesCheckedAllClick(object sender, EventArgs e)
		{
			// Пометить все файлы
			ConnectListsEventHandlers( false );
			CheckAllFiles(true);
			ConnectListsEventHandlers( true );
		}
		
		void TsmiFilesUnCheckedAllClick(object sender, EventArgs e)
		{
			// Снять пометки со всех файлов
			ConnectListsEventHandlers( false );
			UnCheckAllFiles();
			ConnectListsEventHandlers( true );
		}
		
		void TsmiDirCheckedAllClick(object sender, EventArgs e)
		{
			// Пометить все папки
			ConnectListsEventHandlers( false );
			CheckAllDirs(true);
			ConnectListsEventHandlers( true );
		}
		
		void TsmiDirUnCheckedAllClick(object sender, EventArgs e)
		{
			// Снять пометки со всех папок
			ConnectListsEventHandlers( false );
			UnCheckAllDirs();
			ConnectListsEventHandlers( true );
		}
		
		void TsmiFB2CheckedAllClick(object sender, EventArgs e)
		{
			// Пометить все fb2 файлы
			ConnectListsEventHandlers( false );
			CheckTypeAllFiles("fb2", true);
			ConnectListsEventHandlers( true );
		}
		
		void TsmiFB2UnCheckedAllClick(object sender, EventArgs e)
		{
			// Снять пометки со всех fb2 файлов
			ConnectListsEventHandlers( false );
			UnCheckTypeAllFiles("fb2");
			ConnectListsEventHandlers( true );
		}
		
		void TsmiZipCheckedAllClick(object sender, EventArgs e)
		{
			// Пометить все zip файлы
			ConnectListsEventHandlers( false );
			CheckTypeAllFiles("zip", true);
			ConnectListsEventHandlers( true );
		}
		
		void TsmiZipUnCheckedAllClick(object sender, EventArgs e)
		{
			// Снять пометки со всех zip файлов
			ConnectListsEventHandlers( false );
			UnCheckTypeAllFiles("zip");
			ConnectListsEventHandlers( true );
		}
		
		void TsmiCheckedAllSelectedClick(object sender, EventArgs e)
		{
			// Пометить всё выделенное
			ConnectListsEventHandlers( false );
			m_mscLV.ChekAllSelectedItems(listViewSource, true);
			ConnectListsEventHandlers( true );
			listViewSource.Focus();
		}
		
		void TsmiUnCheckedAllSelectedClick(object sender, EventArgs e)
		{
			// Снять пометки со всего выделенного
			ConnectListsEventHandlers( false );
			m_mscLV.ChekAllSelectedItems(listViewSource, false);
			ConnectListsEventHandlers( true );
			listViewSource.Focus();
		}
		
		void ListViewSourceItemCheck(object sender, ItemCheckEventArgs e)
		{
			if( listViewSource.Items.Count > 0 && listViewSource.SelectedItems.Count != 0 ) {
				// при двойном клике на папке ".." пометку не ставим
				if(e.Index == 0) { // ".."
					e.NewValue = CheckState.Unchecked;
				}
			}
		}
		
		void ListViewSourceItemChecked(object sender, ItemCheckedEventArgs e)
		{
			// пометка/снятие пометки по check на 0-й item - папка ".."
			if( listViewSource.Items.Count > 0 ) {
				ListViewItemType it = (ListViewItemType)e.Item.Tag;
				if(it.Type=="dUp") {
					ConnectListsEventHandlers( false );
					if(e.Item.Checked) {
						CheckAll();
					} else {
						UnCheckAll();
					}
					ConnectListsEventHandlers( true );
				}
			}
		}
		
		void ButtonSortFilesToClick(object sender, EventArgs e)
		{
			// ********* Полная сортировка *************
			// обработка заданных каталого
			m_sSource = Settings.FileManagerSettings.FullSortingSourceDir = filesWorker.WorkingDirPath( textBoxAddress.Text.Trim() );
			textBoxAddress.Text = m_sSource;
			m_sTarget = m_sSource + " - OUT"; // папка вывода out - рядом с  исходой
			
			m_bFullSort = true;
			m_bScanSubDirs = chBoxScanSubDir.Checked ? true : false;
			m_sLineTemplate	= txtBoxTemplatesFromLine.Text.Trim();
			m_sMessTitle	= "SharpFBTools - Полная Сортировка";
			// проверка на корректность данных папок источника и приемника файлов
			if( !IsSourceDirDataCorrect() ) {
				return;
			}
			// проверка на наличие архиваторов
			if( !IsArchivatorsExist() ) {
				return;
			}
			// проверки на корректность шаблонных строк
			if( !IsLineTemplateCorrect( m_sLineTemplate ) ) {
				return;
			}
			
			// инициализация контролов
			Init();
			SetFullSortingStartEnabled( false );

			m_dtStart = DateTime.Now;
			tsslblProgress.Text = "Создание списка файлов:";
			
			// Запуск процесса DoWork от Бекграунд Воркера
			if( m_bw.IsBusy != true ) {
				//если не занят то запустить процесс
            	m_bw.RunWorkerAsync();
			}
		}
		
		void ChBoxFSToZipClick(object sender, EventArgs e)
		{
			Settings.FileManagerSettings.FullSortingToZip = chBoxFSToZip.Checked;
			Settings.FileManagerSettings.WriteFileManagerSettings();
		}
		
		void ChBoxFSDelFB2FilesClick(object sender, EventArgs e)
		{
			Settings.FileManagerSettings.FullSortingDelFB2Files = chBoxFSDelFB2Files.Checked;
			Settings.FileManagerSettings.WriteFileManagerSettings();
		}
		
		void ChBoxSSToZipClick(object sender, EventArgs e)
		{
			Settings.FileManagerSettings.SelectedSortingToZip = chBoxSSToZip.Checked;
			Settings.FileManagerSettings.WriteFileManagerSettings();
		}
		
		void ChBoxSSDelFB2FilesClick(object sender, EventArgs e)
		{
			Settings.FileManagerSettings.SelectedSortingDelFB2Files = chBoxSSDelFB2Files.Checked;
			Settings.FileManagerSettings.WriteFileManagerSettings();
		}
		
		void ChBoxScanSubDirClick(object sender, EventArgs e)
		{
			Settings.FileManagerSettings.FullSortingInSubDir = chBoxScanSubDir.Checked;
			Settings.FileManagerSettings.WriteFileManagerSettings();
		}

		void ChBoxSSScanSubDirClick(object sender, EventArgs e)
		{
			Settings.FileManagerSettings.SelectedSortingInSubDir = chBoxSSScanSubDir.Checked;
			Settings.FileManagerSettings.WriteFileManagerSettings();
		}
		
		void TextBoxAddressTextChanged(object sender, EventArgs e)
		{
			Settings.FileManagerSettings.FullSortingSourceDir = textBoxAddress.Text;
		}
		
		void TxtBoxTemplatesFromLineTextChanged(object sender, EventArgs e)
		{
			Settings.FileManagerSettings.FullSortingTemplate = txtBoxTemplatesFromLine.Text;
		}
		
		void TboxSSSourceDirTextChanged(object sender, EventArgs e)
		{
			Settings.FileManagerSettings.SelectedSortingSourceDir = tboxSSSourceDir.Text;
		}
		
		void TboxSSToDirTextChanged(object sender, EventArgs e)
		{
			Settings.FileManagerSettings.SelectedSortingTargetDir = tboxSSToDir.Text;
		}
		
		void TxtBoxSSTemplatesFromLineTextChanged(object sender, EventArgs e)
		{
			Settings.FileManagerSettings.SelectedSortingTemplate = txtBoxSSTemplatesFromLine.Text;
		}
		
		void BtnInsertTemplatesClick(object sender, EventArgs e)
		{
			// запуск диалога Вставки готовых шаблонов
			Core.BookSorting.BasiclTemplates btfrm = new Core.BookSorting.BasiclTemplates();
			btfrm.ShowDialog();
			if( btfrm.GetTemplateLine()!=null ) {
				txtBoxTemplatesFromLine.Text = btfrm.GetTemplateLine();
			}
			btfrm.Dispose();
		}
		
		void BtnSSGetDataClick(object sender, EventArgs e)
		{
			// запуск диалога Сбора данных для Избранной Сортировки
			#region Код
			Core.BookSorting.SelectedSortData ssdfrm = new Core.BookSorting.SelectedSortData();
			// если в основном списке критериев поиска уже есть записи, то копируем их в форму сбора данных
			if( lvSSData.Items.Count > 0 ) {
				for( int i=0; i!=lvSSData.Items.Count; ++i ) {
					ListViewItem lvi = new ListViewItem( lvSSData.Items[i].Text );
								lvi.SubItems.Add( lvSSData.Items[i].SubItems[1].Text );
								lvi.SubItems.Add( lvSSData.Items[i].SubItems[2].Text );
								lvi.SubItems.Add( lvSSData.Items[i].SubItems[3].Text );
								lvi.SubItems.Add( lvSSData.Items[i].SubItems[4].Text );
								lvi.SubItems.Add( lvSSData.Items[i].SubItems[5].Text );
								lvi.SubItems.Add( lvSSData.Items[i].SubItems[6].Text );
								lvi.SubItems.Add( lvSSData.Items[i].SubItems[7].Text );
								lvi.SubItems.Add( lvSSData.Items[i].SubItems[8].Text );
								lvi.SubItems.Add( lvSSData.Items[i].SubItems[9].Text );
					ssdfrm.lvSSData.Items.Add( lvi );
				}
				ssdfrm.lblCount.Text = Convert.ToString( lvSSData.Items.Count );
			}

			ssdfrm.ShowDialog();
			if( ssdfrm.IsOKClicked() ) {
				/* обрабатываем собранные данные */
				if( ssdfrm.lvSSData.Items.Count > 0 ) {
					// удаляем записи в списке, если они есть
					lvSSData.Items.Clear();
					m_lSSQCList = new List<selectedSortQueryCriteria>();
					string sLang, sLast, sFirst, sMiddle, sNick, sGGroup, sGenre, sSequence, sBTitle, sExactFit;
					DataFM dfm = new DataFM();
					FB2SelectedSorting fb2ss = new FB2SelectedSorting();
					for( int i=0; i!=ssdfrm.lvSSData.Items.Count; ++i ) {
						sLang	= ssdfrm.lvSSData.Items[i].Text;
						sGGroup	= ssdfrm.lvSSData.Items[i].SubItems[1].Text;
						sGenre	= ssdfrm.lvSSData.Items[i].SubItems[2].Text;
						sLast	= ssdfrm.lvSSData.Items[i].SubItems[3].Text;
						sFirst	= ssdfrm.lvSSData.Items[i].SubItems[4].Text;
						sMiddle	= ssdfrm.lvSSData.Items[i].SubItems[5].Text;
						sNick	= ssdfrm.lvSSData.Items[i].SubItems[6].Text;
						sSequence	= ssdfrm.lvSSData.Items[i].SubItems[7].Text;
						sBTitle		= ssdfrm.lvSSData.Items[i].SubItems[8].Text;
						sExactFit	= ssdfrm.lvSSData.Items[i].SubItems[9].Text;
						ListViewItem lvi = new ListViewItem( sLang );
									lvi.SubItems.Add( sGGroup );
									lvi.SubItems.Add( sGenre );
									lvi.SubItems.Add( sLast );
									lvi.SubItems.Add( sFirst );
									lvi.SubItems.Add( sMiddle );
									lvi.SubItems.Add( sNick );
									lvi.SubItems.Add( sSequence );
									lvi.SubItems.Add( sBTitle );
									lvi.SubItems.Add( sExactFit );
						// добавление записи в список
						lvSSData.Items.Add( lvi );
						// заполняем список критериев поиска для Избранной Сортировки
						m_lSSQCList.AddRange( fb2ss.MakeSelectedSortQuerysList( sLang, sLast, sFirst, sMiddle, sNick,
																			sGGroup, sGenre, sSequence, sBTitle,
																			sExactFit, dfm.GenresFB21Scheme ) );
					}
				}
			}
			
			ssdfrm.Dispose();
			#endregion
		}
		
		void BtnSSInsertTemplatesClick(object sender, EventArgs e)
		{
			// запуск диалога Вставки готовых шаблонов
			Core.BookSorting.BasiclTemplates btfrm = new Core.BookSorting.BasiclTemplates();
			btfrm.ShowDialog();
			if( btfrm.GetTemplateLine()!= null ) {
				txtBoxSSTemplatesFromLine.Text = btfrm.GetTemplateLine();
			}
			btfrm.Dispose();
		}
		
		void TsbtnSSOpenDirClick(object sender, EventArgs e)
		{
			// задание папки с fb2-файлами и архивами для сканирования (Избранная Сортировка)
			filesWorker.OpenDirDlg( tboxSSSourceDir, fbdScanDir, "Укажите папку для сканирования с fb2-файлами и архивами (Избранная Сортировка):" );
		}
		
		void TsbtnSSTargetDirClick(object sender, EventArgs e)
		{
			// задание папки-приемника для размешения отсортированных файлов (Избранная Сортировка)
			filesWorker.OpenDirDlg( tboxSSToDir, fbdScanDir, "Укажите папку-приемник для размешения отсортированных файлов (Избранная Сортировка):" );
		}
	
		void ButtonFullSortStopClick(object sender, EventArgs e)
		{
			// Остановка выполнения процесса Полной Сортировки
			if( m_bw.WorkerSupportsCancellation == true ) {
				m_bw.CancelAsync();
			}
		}
		
		void TsbtnSSSortFilesToClick(object sender, EventArgs e)
		{
			// ********* Избранная Сортировка ***********
			m_bFullSort = false;
			
			// обработка заданных каталогов
			m_sSource = Settings.FileManagerSettings.SelectedSortingSourceDir = filesWorker.WorkingDirPath( tboxSSSourceDir.Text.Trim() );
			tboxSSSourceDir.Text	= m_sSource;
			m_sTarget = Settings.FileManagerSettings.SelectedSortingTargetDir = filesWorker.WorkingDirPath( tboxSSToDir.Text.Trim() );
			tboxSSToDir.Text		= m_sTarget;
			
			m_bScanSubDirs = chBoxSSScanSubDir.Checked ? true : false;
			m_sLineTemplate = txtBoxSSTemplatesFromLine.Text.Trim();
			m_sMessTitle = "SharpFBTools - Избранная Сортировка";
			// проверка на корректность данных папок источника и приемника файлов
			if( !IsFoldersDataCorrect() ) {
				return;
			}
			// проверка на наличие критериев поиска для Избранной Сортировки
			if( lvSSData.Items.Count == 0 ) {
				MessageBox.Show( "Задайте хоть один критерий для Избранной Сортировки (кнопка \"Собрать данные для Избранной Сортировки\")!", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				btnSSGetData.Focus();
				return;
			}
			// проверка на наличие архиваторов
			if( !IsArchivatorsExist() ) {
				return;
			}
			// проверки на корректность шаблонных строк
			if( !IsLineTemplateCorrect( m_sLineTemplate ) ) {
				return;
			}
			
			// инициализация контролов
			Init();
			SetSelectedSortingStartEnabled( false );

			m_dtStart = DateTime.Now;
			tsslblProgress.Text = "Создание списка файлов:";
			
			// Запуск процесса DoWork от Бекграунд Воркера
			if( m_bw.IsBusy != true ) {
				//если не занят то запустить процесс
            	m_bw.RunWorkerAsync();
			}
		}
		
		void TsbtnSSSortStopClick(object sender, EventArgs e)
		{
			// Остановка выполнения процесса Избранной Сортировки
			if( m_bw.WorkerSupportsCancellation == true ) {
				m_bw.CancelAsync();
			}
		}
		
		void BtnSSDataListSaveClick(object sender, EventArgs e)
		{
			// сохранить список критериев Избранной Сортировки в файл
			string sMessTitle = "SharpFBTools - Избранная Сортировка";
			if( lvSSData.Items.Count == 0 ) {
				MessageBox.Show( "Список данных для Избранной Сортировки пуст.\nЗадайте хоть один критерий Сортировки (кнопка 'Собрать данные для Избранной Сортировки').",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			sfdSaveXMLFile.Filter = "SharpFBTools файлы (*.qss)|*.qss|Все файлы (*.*)|*.*";;
			sfdSaveXMLFile.FileName = "";
			DialogResult result = sfdSaveXMLFile.ShowDialog();
			if( result == DialogResult.OK ) {
				XmlWriter writer = null;
				try {
					XmlWriterSettings settings = new XmlWriterSettings();
					settings.Indent = true;
					settings.IndentChars = ("\t");
					settings.OmitXmlDeclaration = true;
					writer = XmlWriter.Create( sfdSaveXMLFile.FileName, settings );
					writer.WriteStartElement( "SelectedSortingData" );
					for( int i=0; i!=lvSSData.Items.Count; ++i ) {
						writer.WriteStartElement( "Item" );
							writer.WriteAttributeString( "Lang", lvSSData.Items[i].Text );
							writer.WriteAttributeString( "GGroup", lvSSData.Items[i].SubItems[1].Text );
							writer.WriteAttributeString( "Genre", lvSSData.Items[i].SubItems[2].Text );
							writer.WriteAttributeString( "Last", lvSSData.Items[i].SubItems[3].Text );
							writer.WriteAttributeString( "First", lvSSData.Items[i].SubItems[4].Text );
							writer.WriteAttributeString( "Middle", lvSSData.Items[i].SubItems[5].Text );
							writer.WriteAttributeString( "Nick", lvSSData.Items[i].SubItems[6].Text );
							writer.WriteAttributeString( "Sequence", lvSSData.Items[i].SubItems[7].Text );
							writer.WriteAttributeString( "BookTitle", lvSSData.Items[i].SubItems[8].Text );
							writer.WriteAttributeString( "ExactFit", lvSSData.Items[i].SubItems[9].Text );
						writer.WriteEndElement();
					}
					writer.WriteEndElement();
					writer.Flush();
					MessageBox.Show( "Список данных для Избранной Сортировки сохранен в файл!", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				}  finally  {
					if (writer != null)
					writer.Close();
				}
	         }
		}
		
		void BtnSSDataListLoadClick(object sender, EventArgs e)
		{
			// загрузить список критериев Избранной Сортировки из файла
			sfdOpenXMLFile.Filter = "SharpFBTools файлы (*.qss)|*.qss|Все файлы (*.*)|*.*";
			sfdOpenXMLFile.FileName = "";
			DialogResult result = sfdOpenXMLFile.ShowDialog();
			if( result == DialogResult.OK ) {
				XmlReaderSettings xml = new XmlReaderSettings();
				xml.IgnoreWhitespace = true;
				using ( XmlReader reader = XmlReader.Create( sfdOpenXMLFile.FileName, xml ) ) {
					try {
						reader.ReadToFollowing("Item");
						if( reader.HasAttributes ) {
							// удаляем записи в списке, если они есть
							lvSSData.Items.Clear();
							DataFM dfm = new DataFM();
							FB2SelectedSorting fb2ss = new FB2SelectedSorting();
							m_lSSQCList = new List<selectedSortQueryCriteria>();
							string sLang, sLast, sFirst, sMiddle, sNick, sGGroup, sGenre, sSequence, sBTitle, sExactFit;
							do {
       							sLang		= reader.GetAttribute("Lang");
       							sLast		= reader.GetAttribute("Last");
       							sFirst		= reader.GetAttribute("First");
       							sMiddle		= reader.GetAttribute("Middle");
       							sNick		= reader.GetAttribute("Nick");
       							sGGroup		= reader.GetAttribute("GGroup");
       							sGenre		= reader.GetAttribute("Genre");
       							sSequence	= reader.GetAttribute("Sequence");
       							sBTitle		= reader.GetAttribute("BookTitle");
       							sExactFit	= reader.GetAttribute("ExactFit");
       							
       							ListViewItem lvi = new ListViewItem( sLang );
											lvi.SubItems.Add( sGGroup );
											lvi.SubItems.Add( sGenre );
											lvi.SubItems.Add( sLast );
											lvi.SubItems.Add( sFirst );
											lvi.SubItems.Add( sMiddle );
											lvi.SubItems.Add( sNick );
											lvi.SubItems.Add( sSequence );
											lvi.SubItems.Add( sBTitle );
											lvi.SubItems.Add( sExactFit );
								// добавление записи в список
								lvSSData.Items.Add( lvi );
								// заполняем список критериев поиска для Избранной Сортировки
								m_lSSQCList.AddRange( fb2ss.MakeSelectedSortQuerysList( sLang, sLast, sFirst, sMiddle, sNick,
																					sGGroup, sGenre, sSequence, sBTitle,
																					sExactFit, dfm.GenresFB21Scheme ) );
    						} while( reader.ReadToNextSibling("Item") );
						}
					} catch {
						MessageBox.Show( "Поврежден списка данных для Избранной Сортировки:\n\""+sfdOpenXMLFile.FileName+"\".", "SharpFBTools - Избранная Сортировка", MessageBoxButtons.OK, MessageBoxIcon.Warning );
					} finally {
						reader.Close();
					}
				}
			}
		}
		#endregion

	}
}
