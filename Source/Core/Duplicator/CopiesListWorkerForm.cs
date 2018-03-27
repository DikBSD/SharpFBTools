/*
 * Сделано в SharpDevelop.
 * Пользователь: Вадим Кузнецов (DikBSD)
 * Дата: 27.08.2014
 * Время: 12:23
 * 
 */
using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using System.IO;

using Core.Common;

using EndWorkMode		= Core.Common.EndWorkMode;
using WorksWithBooks	= Core.Common.WorksWithBooks;
using MiscListView		= Core.Common.MiscListView;

// enums
using ResultViewDupCollumnEnum		= Core.Common.Enums.ResultViewDupCollumnEnum;
using FilesCountViewDupCollumnEnum	= Core.Common.Enums.FilesCountViewDupCollumnEnum;
using EndWorkModeEnum				= Core.Common.Enums.EndWorkModeEnum;
using BooksWorkModeEnum				= Core.Common.Enums.BooksWorkModeEnum;

namespace Core.Duplicator
{
	/// <summary>
	/// FileWorkerForm: форма прогресса обработки файлов (загрузка/сохранение списков, копирование/перемещение файлов)
	/// </summary>
	public partial class CopiesListWorkerForm : Form
	{
		#region Закрытые данные класса
		private readonly BooksWorkModeEnum	m_WorkMode; // режим обработки книг
		private readonly string			m_DirOrFileName		= string.Empty;
		
		private readonly ComboBox		m_cboxMode			= new ComboBox();
		private readonly TextBox		m_tboxSourceDir		= new TextBox();
		private readonly CheckBox		m_chBoxScanSubDir	= new CheckBox();
		private readonly ListView		m_listViewFB2Files	= new ListView();
		private readonly ListView		m_lvFilesCount		= new ListView();
		private readonly StatusView		m_StatusView		= new StatusView();
		private readonly EndWorkMode	m_EndMode			= new EndWorkMode();
		
		private 		 int			m_LastSelectedItem	= -1;	// выделенный итем, на котором закончилась обработка списка...
		private readonly int			m_GroupCountForList	= 500;	// ограничитель числа групп для кажого файла
		
		private DateTime m_dtStart;
		private BackgroundWorker		m_bw = null; // фоновый обработчик
		#endregion

		public CopiesListWorkerForm( BooksWorkModeEnum WorkMode, string DirOrFileName, ComboBox cboxMode,
		                            ListView listViewFB2Files, ListView lvFilesCount, TextBox tboxSourceDir,
		                            CheckBox chBoxScanSubDir, int LastSelectedItem, int GroupCountForList )
		{
			InitializeComponent();
			m_DirOrFileName		= DirOrFileName;
			m_cboxMode			= cboxMode;
			m_tboxSourceDir		= tboxSourceDir;
			m_chBoxScanSubDir	= chBoxScanSubDir;
			m_listViewFB2Files	= listViewFB2Files;
			m_lvFilesCount		= lvFilesCount;
			m_WorkMode			= WorkMode;
			m_LastSelectedItem	= LastSelectedItem;
			
			m_StatusView.Group			= Convert.ToInt32( m_lvFilesCount.Items[(int)FilesCountViewDupCollumnEnum.AllGroups].SubItems[1].Text );
			m_StatusView.AllFB2InGroups = Convert.ToInt32( m_lvFilesCount.Items[(int)FilesCountViewDupCollumnEnum.AllBoolsInAllGroups].SubItems[1].Text );

			m_GroupCountForList = GroupCountForList;
			
			InitializeBackgroundWorker();
			
			if( !m_bw.IsBusy )
				m_bw.RunWorkerAsync(); //если не занят, то запустить процесс
		}
		
		// =============================================================================================
		// 								ОТКРЫТЫЕ СВОЙСТВА
		// =============================================================================================
		#region Открытые свойства
		public virtual EndWorkMode EndMode {
			get { return m_EndMode; }
		}
		public virtual int LastSelectedItem {
			get { return m_LastSelectedItem; }
		}
		#endregion
		
		// =============================================================================================
		//			BACKGROUNDWORKER: ОБРАБОТКА ФАЙЛОВ
		// =============================================================================================
		#region BackgroundWorker: Обработка файлов
		private void InitializeBackgroundWorker() {
			// Инициализация перед использование BackgroundWorker
			m_bw = new BackgroundWorker();
			m_bw.WorkerReportsProgress		= true; // Позволить выводить прогресс процесса
			m_bw.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
			m_bw.DoWork 			+= new DoWorkEventHandler( bw_DoWork );
			m_bw.ProgressChanged 	+= new ProgressChangedEventHandler( bw_ProgressChanged );
			m_bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler( bw_RunWorkerCompleted );
		}
		
		// Обработка файлов
		private void bw_DoWork( object sender, DoWorkEventArgs e ) {
			m_dtStart = DateTime.Now;
			ProgressBar.Value = 0;
			switch ( m_WorkMode ) {
				case BooksWorkModeEnum.SaveFB2List:
					// Сохранение списка копий книг
					this.Text = "Сохранение списка копий fb2 книг";
					saveCopiesListToXml( ref m_bw, ref e, m_GroupCountForList, m_DirOrFileName,
					                    m_cboxMode.SelectedIndex, m_cboxMode.Text.Trim() );
					break;
				case BooksWorkModeEnum.LoadFB2List:
					// Загрузка списка копий книг
					this.Text = "Загрузка списка копий fb2 книг";
					loadCopiesListFromXML( ref m_bw, ref e, m_DirOrFileName );
					break;
				case BooksWorkModeEnum.SaveWorkingFB2List:
					// Сохранение текущего обрабатываемого списка копий книг без запроса пути
					this.Text = "Сохранение списка копий fb2 книг";
					saveWorkingListToXml( ref m_bw, ref e, m_GroupCountForList, m_DirOrFileName,
					                     m_cboxMode.SelectedIndex, m_cboxMode.Text.Trim() );
					break;
				default:
					return;
			}

			if ( ( m_bw.CancellationPending ) ) {
				e.Cancel = true;
				return;
			}
//			m_lvResult.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
		}
		
		// Отображение результата
		private void bw_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
			++ProgressBar.Value;
		}
		
		// Проверяем - это отмена, ошибка, или конец задачи и сообщить
		private void bw_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {
			DateTime dtEnd = DateTime.Now;
			string sTime = dtEnd.Subtract( m_dtStart ).ToString() + " (час.:мин.:сек.)";
			if ( e.Cancelled ) {
				m_EndMode.EndMode = EndWorkModeEnum.Cancelled;
				m_EndMode.Message = "Работа прервана и не выполнена до конца!\nЗатрачено времени: "+sTime;
			} else if( e.Error != null ) {
				m_EndMode.EndMode = EndWorkModeEnum.Error;
				m_EndMode.Message = "Ошибка:\n" + e.Error.Message + "\n" + e.Error.StackTrace + "\nЗатрачено времени: "+sTime;
			} else {
				m_EndMode.EndMode = EndWorkModeEnum.Done;
				m_EndMode.Message = "Обработка fb2-файлов завершена!\nЗатрачено времени: "+sTime;
//				if ( m_lvResult.Items.Count == 0 )
//					m_EndMode.Message += "\n\nНе найдено НИ ОДНОЙ копии книг!";
			}
			this.Close();
		}
		#endregion
		
		// =============================================================================================
		// 				СОХРАНЕНИЕ В XML И ЗАГРУЗКА ИЗ XML СПИСКА КОПИЙ FB2 КНИГ
		// =============================================================================================
		#region Сохранение в xml и Загрузка из xml списка копий fb2 книг
		
		#region Сохранение в xml списка копий fb2 книг
		// заполнение данными ноды для генерируемых файлов списка копий
		private void setDataForNode( ref XDocument doc, int GroupCountInGroups, int BookInGroups ) {
			XElement xCompareData = doc.Root.Element("CompareData");
			if ( xCompareData != null ) {
				xCompareData.SetElementValue("Groups", GroupCountInGroups);
				xCompareData.SetElementValue("AllFB2InGroups", BookInGroups);
			}
			// заполнение аттрибутов
			XElement xGroups = doc.Root.Element("Groups");
			if ( xGroups != null ) {
				xGroups.SetAttributeValue("count", GroupCountInGroups);
				xGroups.SetAttributeValue("books", BookInGroups);
				IEnumerable<XElement> Groups = xGroups.Elements("Group");
				int i = 0;
				foreach( XElement Group in Groups )
					Group.SetAttributeValue( "number", ++i );
			}
		}
		private XDocument createXMLStructure( int CompareMode, string CompareModeName ) {
			return new XDocument(
				new XDeclaration("1.0", "utf-8", "yes"),
				new XComment("Файл копий fb2 книг, сохраненный после полного окончания работы Дубликатора"),
				new XElement("Files", new XAttribute("type", "dup_endwork"),
				             new XComment("Папка для поиска копий fb2 книг"),
				             new XElement("SourceDir", m_tboxSourceDir.Text.Trim()),
				             new XComment("Настройки поиска-сравнения fb2 книг"),
				             new XElement("Settings",
				                          new XElement("ScanSubDirs", m_chBoxScanSubDir.Checked)),
				             new XComment("Режим поиска-сравнения fb2 книг"),
				             new XElement("CompareMode",
				                          new XAttribute("index", CompareMode),
				                          new XElement("Name", CompareModeName)),
				             new XComment("Данные о ходе сравнения fb2 книг"),
				             new XElement("CompareData",
				                          new XElement("Groups", "0"),
				                          new XElement("AllFB2InGroups", "0")
				                         ),
				             new XComment("Копии fb2 книг по группам"),
				             new XElement("Groups",
				                          new XAttribute("count", "0"),
				                          new XAttribute("books", "0")
				                         ),
				             new XComment("Выделенный элемент списка, на котором завершили обработку книг"),
				             new XElement("SelectedItem", "-1" )
				            )
			);
		}
		private void addGroupInGroups( ref XDocument doc, ref XElement xeGroup, ListViewGroup listViewGroup ) {
			doc.Root.Element("Groups").Add(
				xeGroup = new XElement(
					"Group", new XAttribute("number", 0),
					new XAttribute("count", listViewGroup.Items.Count),
					new XAttribute("name", listViewGroup.Header)
				)
			);
		}
		private void addBookInGroup( ref XElement xeGroup, ListViewItem listViewItem, ref int BookNumber ) {
			xeGroup.Add(
				new XElement("Book", new XAttribute("number", ++BookNumber),
				             new XElement("Group", listViewItem.Group.Header),
				             new XElement("Path", listViewItem.SubItems[(int)ResultViewDupCollumnEnum.Path].Text),
				             new XElement("BookTitle", listViewItem.SubItems[(int)ResultViewDupCollumnEnum.BookTitle].Text),
				             new XElement("Authors", listViewItem.SubItems[(int)ResultViewDupCollumnEnum.Authors].Text),
				             new XElement("Genres", listViewItem.SubItems[(int)ResultViewDupCollumnEnum.Genres].Text),
				             new XElement("BookLang", listViewItem.SubItems[(int)ResultViewDupCollumnEnum.BookLang].Text),
				             new XElement("BookID", listViewItem.SubItems[(int)ResultViewDupCollumnEnum.BookID].Text),
				             new XElement("Version", listViewItem.SubItems[(int)ResultViewDupCollumnEnum.Version].Text),
				             new XElement("Encoding", listViewItem.SubItems[(int)ResultViewDupCollumnEnum.Encoding].Text),
				             new XElement("Validation", listViewItem.SubItems[(int)ResultViewDupCollumnEnum.Validate].Text),
				             new XElement("FileLength", listViewItem.SubItems[(int)ResultViewDupCollumnEnum.FileLength].Text),
				             new XElement("FileCreationTime", listViewItem.SubItems[(int)ResultViewDupCollumnEnum.CreationTime].Text),
				             new XElement("FileLastWriteTime", listViewItem.SubItems[(int)ResultViewDupCollumnEnum.LastWriteTime].Text),
				             new XElement("ForeColor", listViewItem.ForeColor.Name),
				             new XElement("BackColor", listViewItem.BackColor.Name),
				             new XElement("IsChecked", listViewItem.Checked)
				            )
			);
		}
		private void addAllBookInGroup( ref BackgroundWorker bw, ref DoWorkEventArgs e,
		                               ref XDocument doc, ListViewGroup listViewGroup,
		                               ref int BookInGroups, ref int GroupCountInGroups ) {
			BookInGroups += listViewGroup.Items.Count;
			
			XElement xeGroup = null;
			addGroupInGroups( ref doc, ref xeGroup, listViewGroup );
			
			int BookNumber = 0;			// номер Книги (Book number) В Группе (Group)
			int BookCountInGroup = 0;	// число Книг (Group count) в Группе (Group)
			int i = 0; 					// прогресс
			foreach ( ListViewItem lvi in listViewGroup.Items ) {
				// только для "реальных" книг в списке (игнорируем итемы в списке тех книг, которые были удалены с диска в режиме быстрого удаления)
				// а также только для Групп, в которых больше 1 книги.
				if ( lvi.Group.Items.Count > 1 ) {
					addBookInGroup( ref xeGroup, lvi, ref BookNumber );
					xeGroup.SetAttributeValue( "count", ++BookCountInGroup );
					if ( !File.Exists( lvi.SubItems[(int)ResultViewDupCollumnEnum.Path].Text ) ) {
						// пометка цветом и зачеркиванием удаленных книг с диска, но не из списка (быстрый режим удаления)
						WorksWithBooks.markRemoverFileInCopyesList( lvi );
					}
				}
				if ( !xeGroup.HasElements ) {
					xeGroup.Remove();
				}
				bw.ReportProgress( ++i );
			} // по всем книгам Группы
			++GroupCountInGroups;
		}
		// сохранение списка копий книг в xml-файл
		private void saveCopiesListToXml( ref BackgroundWorker bw, ref DoWorkEventArgs e, int GroupCountForList,
		                                 string ToDirName, int CompareMode, string CompareModeName ) {
			if ( !Directory.Exists( ToDirName ) )
				Directory.CreateDirectory( ToDirName );
			int ThroughGroupCounterForXML = 0;	// "сквозной" счетчик числа групп для каждого создаваемого xml файла копий
			int GroupCounterForXML = 0;			// счетчик (в границых CompareModeName) числа групп для каждого создаваемого xml файла копий
			int XmlFileNumber = 0;				// номер файла - для формирования имени создаваемого xml файла копий
			
			// копии fb2 книг по группам
			if ( m_listViewFB2Files.Groups.Count > 0 ) {
				ProgressBar.Maximum	= m_listViewFB2Files.Items.Count;
				XDocument doc = createXMLStructure( CompareMode, CompareModeName );
				
				int BookInGroups = 0; 		// число книг (books) в Группах (Groups)
				int GroupCountInGroups = 0; // число Групп (Group count) в Группах (Groups)
				bool one = false;
				foreach ( ListViewGroup lvGroup in m_listViewFB2Files.Groups ) {
					if ( ( bw.CancellationPending ) )  {
						e.Cancel = true;
						return;
					}
					
					addAllBookInGroup( ref bw, ref e, ref doc, lvGroup, ref BookInGroups, ref GroupCountInGroups );

					++GroupCounterForXML;
					++ThroughGroupCounterForXML;
					doc.Root.Element("SelectedItem").SetValue(
						( m_LastSelectedItem <= GroupCountForList && ThroughGroupCounterForXML <= GroupCountForList )
						? m_LastSelectedItem.ToString()
						: "0"
					);
					if ( GroupCountForList <= m_listViewFB2Files.Groups.Count ) {
						if ( GroupCounterForXML >= GroupCountForList ) {
							setDataForNode( ref doc, GroupCountInGroups, BookInGroups );
							doc.Save(
								Path.Combine( ToDirName, StringProcessing.makeNNNStringOfNumber( ++XmlFileNumber ) + ".dup_lbc" )
							);
							doc.Root.Element("Groups").Elements().Remove();
							GroupCountInGroups = 0;
							GroupCounterForXML = 0;
							BookInGroups = 0;
						} else {
							// последний диаппазон Групп
							if( ThroughGroupCounterForXML == m_listViewFB2Files.Groups.Count ) {
								setDataForNode( ref doc, GroupCountInGroups, BookInGroups );
								doc.Save(
									Path.Combine( ToDirName, StringProcessing.makeNNNStringOfNumber( ++XmlFileNumber ) + ".dup_lbc" )
								);
							}
						}
					} else {
						setDataForNode( ref doc, GroupCountInGroups, BookInGroups );
						one = true;
					}
				} // по всем Группам
				if ( one )
					doc.Save( Path.Combine( ToDirName, "001.dup_lbc" ) );
			}
		}
		// сохранение текущего обрабатываемого списка без запроса на подтверждение и пути сохранения
		private void saveWorkingListToXml( ref BackgroundWorker bw, ref DoWorkEventArgs e, int GroupCountForList,
		                                  string ToFileName, int CompareMode, string CompareModeName ) {
			// копии fb2 книг по группам
			if ( m_listViewFB2Files.Groups.Count > 0 ) {
				ProgressBar.Maximum	= m_listViewFB2Files.Items.Count;
				XDocument doc = createXMLStructure( CompareMode, CompareModeName );
				
				int BookInGroups = 0; 		// число книг (books) в Группах (Groups)
				int GroupCountInGroups = 0; // число Групп (Group count) в Группах (Groups)
				foreach ( ListViewGroup lvGroup in m_listViewFB2Files.Groups ) {
					if ( ( bw.CancellationPending ) )  {
						e.Cancel = true;
						return;
					}
					
					addAllBookInGroup( ref bw, ref e, ref doc, lvGroup, ref BookInGroups, ref GroupCountInGroups );
					
					doc.Root.Element("SelectedItem").SetValue( m_LastSelectedItem.ToString() );
					setDataForNode( ref doc, GroupCountInGroups, BookInGroups );
				} // по всем Группам
				doc.Save( ToFileName );
			}
		}
		#endregion
		
		#region Загрузка из xml списка копий fb2 книг
		// загрузка из xml-файла в хэш таблицу данных о копиях книг
		private void loadCopiesListFromXML( ref BackgroundWorker bw, ref DoWorkEventArgs e, string FromXML ) {
			XElement xmlTree = XElement.Load( FromXML );
			
			// выставляем режим сравнения
			m_cboxMode.SelectedIndex = Convert.ToInt16( xmlTree.Element("CompareMode").Attribute("index").Value );
			
			// устанавливаем данные настройки поиска-сравнения
			m_tboxSourceDir.Text = xmlTree.Element("SourceDir").Value;
			m_chBoxScanSubDir.Checked = Convert.ToBoolean( xmlTree.Element("Settings").Element("ScanSubDirs").Value );
			
			//загрузка данных о ходе сравнения
			XElement compareData = xmlTree.Element("CompareData");
			m_StatusView.Group = Convert.ToInt32( compareData.Element("Groups").Value );
			m_StatusView.AllFB2InGroups = Convert.ToInt32( compareData.Element("AllFB2InGroups").Value );
			
			ViewDupProgressData();

			// данные поиска
			Hashtable htBookGroups = new Hashtable(); // хеш-таблица групп одинаковых книг
			ListViewGroup	lvg = null; // группа одинаковых книг
			ListViewItem	lvi = null;
			ProgressBar.Maximum	= Convert.ToInt32( xmlTree.Element("Groups").Attribute("books").Value );
			IEnumerable<XElement> Groups = xmlTree.Element("Groups").Elements("Group");
			// перебор всех групп копий
			int i = 0;
			foreach ( XElement Group in Groups ) {
				if ( ( bw.CancellationPending ) )  {
					e.Cancel = true;
					return;
				}
				string GroupName = Group.Attribute("name").Value;
				// перебор всех книг в группе
				IEnumerable<XElement> books = Group.Elements("Book");
				foreach ( XElement book in books ) {
					// в список - только существующие на диске книги
					if ( File.Exists( book.Element("Path").Value ) ) {
						string lviForeColor = book.Element("ForeColor").Value;
						string lviBackColor = book.Element("BackColor").Value;
						lvg = new ListViewGroup( GroupName );
						lvi = new ListViewItem( book.Element("Path").Value );
						lvi.ForeColor = Color.FromName( lviForeColor );
						lvi.BackColor = Color.FromName( lviBackColor );
						lvi.SubItems.Add( book.Element("BookTitle").Value );
						lvi.SubItems.Add( book.Element("Authors").Value );
						lvi.SubItems.Add( book.Element("Genres").Value );
						lvi.SubItems.Add( book.Element("BookLang").Value );
						lvi.SubItems.Add( book.Element("BookID").Value );
						lvi.SubItems.Add( book.Element("Version").Value );
						lvi.SubItems.Add( book.Element("Encoding").Value );
						lvi.SubItems.Add( book.Element("Validation").Value );
						lvi.SubItems.Add( book.Element("FileLength").Value );
						lvi.SubItems.Add( book.Element("FileCreationTime").Value );
						lvi.SubItems.Add( book.Element("FileLastWriteTime").Value );
						// заносим группу в хеш, если она там отсутствует
						AddBookGroupInHashTable( ref htBookGroups, ref lvg );
						// присваиваем группу книге
						m_listViewFB2Files.Groups.Add( (ListViewGroup)htBookGroups[GroupName] );
						lvi.Group = (ListViewGroup)htBookGroups[GroupName];
						lvi.Checked = Convert.ToBoolean( book.Element("IsChecked").Value );
						m_listViewFB2Files.Items.Add( lvi );
					} else {
						--m_StatusView.AllFB2InGroups;
					}
					bw.ReportProgress( ++i );
				}
			}
			ViewDupProgressData();
			m_LastSelectedItem = Convert.ToInt32( xmlTree.Element("SelectedItem").Value );
			MiscListView.SelectedItemEnsureVisible( m_listViewFB2Files, m_LastSelectedItem == -1 ? 0 : m_LastSelectedItem );
		}
		
		// создание хеш-таблицы для групп одинаковых книг
		private bool AddBookGroupInHashTable( ref Hashtable groups, ref ListViewGroup lvg ) {
			if ( groups != null ){
				if ( !groups.Contains( lvg.Header ) ) {
					groups.Add( lvg.Header, lvg );
					return true;
				}
			}
			return false;
		}
		#endregion
		
		// Отображение результата поиска сравнения
		private void ViewDupProgressData() {
			MiscListView.ListViewStatus( m_lvFilesCount, (int)FilesCountViewDupCollumnEnum.AllGroups, m_StatusView.Group );
			MiscListView.ListViewStatus( m_lvFilesCount, (int)FilesCountViewDupCollumnEnum.AllBoolsInAllGroups, m_StatusView.AllFB2InGroups );
		}
		#endregion
		
		// =============================================================================================
		// 								ОБРАБОТЧИКИ СОБЫТИЙ
		// =============================================================================================
		#region Обработчики событий
		// нажатие кнопки прерывания работы
		void BtnStopClick(object sender, EventArgs e)
		{
			if( m_bw.WorkerSupportsCancellation )
				m_bw.CancelAsync();
		}
		#endregion
		
	}
}
