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

using ResultViewDupCollumn = Core.Common.Enums.ResultViewDupCollumn;
using EndWorkMode			= Core.Common.EndWorkMode;
using BooksWorkMode			= Core.Common.Enums.BooksWorkMode;
using WorksWithBooks		= Core.Common.WorksWithBooks;
using MiscListView			= Core.Common.MiscListView;

// enums
using EndWorkModeEnum			= Core.Common.Enums.EndWorkModeEnum;
using FilesCountViewDupCollumn	= Core.Common.Enums.FilesCountViewDupCollumn;

namespace Core.Duplicator
{
	/// <summary>
	/// FileWorkerForm: форма прогреса обработки файлов (загрузка/сохранение списков, копирование/перемещение файлов)
	/// </summary>
	public partial class CopiesListWorkerForm : Form
	{
		#region Закрытые данные класса
		private readonly BooksWorkMode	m_WorkMode; // режим обработки книг
		private readonly string			m_FilePath				= string.Empty;
		
		private readonly ComboBox		m_cboxMode			= new ComboBox();
		private readonly TextBox		m_tboxSourceDir		= new TextBox();
		private readonly CheckBox		m_chBoxScanSubDir	= new CheckBox();
		private readonly CheckBox 		m_chBoxIsValid		= new CheckBox();
		private readonly RadioButton	m_rbtnFB2Librusec	= new RadioButton();
		private readonly ListView		m_lvResult			= new ListView();
		private readonly ListView		m_lvFilesCount		= new ListView();
		private readonly StatusView		m_StatusView		= new StatusView();
		private readonly EndWorkMode	m_EndMode			= new EndWorkMode();
		
		private readonly int			m_LastSelectedItem	= -1;	// выделенный итем, на котором закончилась обработка списка...
		private readonly int			m_GroupCountForList	= 500;	// ограничитель числа групп для кажого файла
		
		private readonly DateTime		m_dtStart;
		private BackgroundWorker		m_bw = null; // фоновый обработчик
		#endregion

		public CopiesListWorkerForm( BooksWorkMode WorkMode, string FromFilePath, ComboBox cboxMode,
		                            ListView lvResult, ListView lvFilesCount, TextBox tboxSourceDir,
		                            CheckBox chBoxScanSubDir, CheckBox chBoxIsValid, RadioButton rbtnFB2Librusec,
		                            int LastSelectedItem, int GroupCountForList
		                           )
		{
			InitializeComponent();
			m_FilePath			= FromFilePath;
			m_cboxMode			= cboxMode;
			m_tboxSourceDir		= tboxSourceDir;
			m_chBoxScanSubDir	= chBoxScanSubDir;
			m_chBoxIsValid		= chBoxIsValid;
			m_rbtnFB2Librusec	= rbtnFB2Librusec;
			m_lvResult			= lvResult;
			m_lvFilesCount		= lvFilesCount;
			m_WorkMode			= WorkMode;
			m_LastSelectedItem	= LastSelectedItem;
			
			m_StatusView.AllFiles		= Convert.ToInt32( m_lvFilesCount.Items[(int)FilesCountViewDupCollumn.AllBooks].SubItems[1].Text );
			m_StatusView.Group			= Convert.ToInt32( m_lvFilesCount.Items[(int)FilesCountViewDupCollumn.AllGroups].SubItems[1].Text );
			m_StatusView.AllFB2InGroups = Convert.ToInt32( m_lvFilesCount.Items[(int)FilesCountViewDupCollumn.AllBoolsInAllGroups].SubItems[1].Text );

			m_GroupCountForList = GroupCountForList;
			
			InitializeBackgroundWorker();
			m_dtStart = DateTime.Now;
			
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
			ProgressBar.Value = 0;
			switch( m_WorkMode ) {
				case BooksWorkMode.SaveFB2List:
					this.Text = "Сохранение списка копий fb2 книг";
					// удаление всех элементов Списка, для которых отсутствуют файлы на жестком диске (защита от сохранения пустых Групп)
					MiscListView.deleteAllItemForNonExistFile( m_lvResult );
					saveCopiesListToXml( ref m_bw, ref e, m_GroupCountForList, m_FilePath,
					                    m_cboxMode.SelectedIndex, m_cboxMode.Text.Trim() );
					break;
				case BooksWorkMode.LoadFB2List:
					this.Text = "Загрузка списка копий fb2 книг";
					loadCopiesListFromXML( ref m_bw, ref e, m_FilePath );
					break;
				default:
					return;
			}

			if( ( m_bw.CancellationPending ) ) {
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
		// формирование строки из номера по Шаблону 00X
		private string makeNNNStringOfNumber( int Number ) {
			// число, смотрим, сколько цифр и добавляем слева нужное число 0.
			if( Number > 0 && Number <= 9 )
				return "00" + Number.ToString();
			else if( Number >= 10 && Number <= 99)
				return "0" + Number.ToString();
			else
				return Number.ToString(); // число символов >= 3
		}
		// заполнение данными ноды для генерируемых файлов списка копий
		private void setDataForNode( ref XDocument doc, int GroupCountInGroups, int BookInGroups ) {
			doc.Root.Element("CompareData").SetElementValue("Groups", GroupCountInGroups);
			doc.Root.Element("CompareData").SetElementValue("AllFB2InGroups", BookInGroups);
			// заполнение аттрибутов
			doc.Root.Element("Groups").SetAttributeValue("count", GroupCountInGroups);
			doc.Root.Element("Groups").SetAttributeValue("books", BookInGroups);
			IEnumerable<XElement> Groups = doc.Root.Element("Groups").Elements("Group");
			int i = 0;
			foreach( XElement Group in Groups )
				Group.SetAttributeValue( "number", ++i );
		}
		// сохранение списка копий книг в xml-файл
		private void saveCopiesListToXml( ref BackgroundWorker bw, ref DoWorkEventArgs e, int GroupCountForList,
		                                 string ToFileName, int CompareMode, string CompareModeName ) {
			if( !Directory.Exists( m_FilePath ) )
				Directory.CreateDirectory( m_FilePath );
			int ThroughGroupCounterForXML = 0;	// "скыозной"счетчик числа групп для каждого создаваемого xml файла копий
			int GroupCounterForXML = 0;			// счетчик (в границых CompareModeName) числа групп для каждого создаваемого xml файла копий
			int XmlFileNumber = 0;				// номер файла - для формирования имени создаваемого xml файла копий
			
			// копии fb2 книг по группам
			if ( m_lvResult.Groups.Count > 0 ) {
				ProgressBar.Maximum	= m_lvResult.Items.Count;
				XDocument doc = new XDocument(
					new XDeclaration("1.0", "utf-8", "yes"),
					new XComment("Файл копий fb2 книг, сохраненный после полного окончания работы Дубликатора"),
					new XElement("Files", new XAttribute("type", "dup_endwork"),
					             new XComment("Папка для поиска копий fb2 книг"),
					             new XElement("SourceDir", m_tboxSourceDir.Text.Trim()),
					             new XComment("Настройки поиска-сравнения fb2 книг"),
					             new XElement("Settings",
					                          new XElement("ScanSubDirs", m_chBoxScanSubDir.Checked),
					                          new XElement("CheckValidate", m_chBoxIsValid.Checked),
					                          new XElement("GenresFB2Librusec", m_rbtnFB2Librusec.Checked)),
					             new XComment("Режим поиска-сравнения fb2 книг"),
					             new XElement("CompareMode",
					                          new XAttribute("index", CompareMode),
					                          new XElement("Name", CompareModeName)),
					             new XComment("Данные о ходе сравнения fb2 книг"),
					             new XElement("CompareData",
					                          new XElement("AllDirs", m_lvFilesCount.Items[(int)FilesCountViewDupCollumn.AllDirs].SubItems[1].Text),
					                          new XElement("AllFiles", m_lvFilesCount.Items[(int)FilesCountViewDupCollumn.AllBooks].SubItems[1].Text),
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
				
				int BookInGroups = 0; 		// число книг (books) в Группах (Groups)
				int GroupCountInGroups = 0; // число Групп (Group count) в Группах (Groups)
				foreach (ListViewGroup lvGroup in m_lvResult.Groups ) {
					if( ( bw.CancellationPending ) )  {
						e.Cancel = true;
						return;
					}
					BookInGroups += lvGroup.Items.Count;
					XElement xeGroup = null;
					int BookNumber = 0;	// номер Книги (Book number) В Группе (Group)
					int i = 0; 			// прогресс
					doc.Root.Element("Groups").Add(
						xeGroup = new XElement("Group", new XAttribute("number", 0),
						                       new XAttribute("count", lvGroup.Items.Count),
						                       new XAttribute("name", lvGroup.Header)
						                      )
					);
					
					int BookCountInGroup = 0; // число Книг (Group count) в Группе (Group)
					foreach ( ListViewItem lvi in lvGroup.Items ) {
						// только для "реальных" книг в списке (игнорируем итемы в списке тех книг, которые были удалены с диска в режиме быстрого удаления)
						// а также только для Групп, в которых больше 1 книги.
						if( lvi.Group.Items.Count > 1 ) {
							xeGroup.Add( new XElement("Book", new XAttribute("number", ++BookNumber),
							                          new XElement("Group", lvi.Group.Header),
							                          new XElement("Path", lvi.SubItems[(int)ResultViewDupCollumn.Path].Text),
							                          new XElement("BookTitle", lvi.SubItems[(int)ResultViewDupCollumn.BookTitle].Text),
							                          new XElement("Authors", lvi.SubItems[(int)ResultViewDupCollumn.Authors].Text),
							                          new XElement("Genres", lvi.SubItems[(int)ResultViewDupCollumn.Genres].Text),
							                          new XElement("BookLang", lvi.SubItems[(int)ResultViewDupCollumn.BookLang].Text),
							                          new XElement("BookID", lvi.SubItems[(int)ResultViewDupCollumn.BookID].Text),
							                          new XElement("Version", lvi.SubItems[(int)ResultViewDupCollumn.Version].Text),
							                          new XElement("Encoding", lvi.SubItems[(int)ResultViewDupCollumn.Encoding].Text),
							                          new XElement("Validation", lvi.SubItems[(int)ResultViewDupCollumn.Validate].Text),
							                          new XElement("FileLength", lvi.SubItems[(int)ResultViewDupCollumn.FileLength].Text),
							                          new XElement("FileCreationTime", lvi.SubItems[(int)ResultViewDupCollumn.CreationTime].Text),
							                          new XElement("FileLastWriteTime", lvi.SubItems[(int)ResultViewDupCollumn.LastWriteTime].Text),
							                          new XElement("ForeColor", lvi.ForeColor.Name),
							                          new XElement("BackColor", lvi.BackColor.Name),
							                          new XElement("IsChecked", lvi.Checked)
							                         )
							           );
							xeGroup.SetAttributeValue( "count", ++BookCountInGroup );
							if( !File.Exists( lvi.SubItems[(int)ResultViewDupCollumn.Path].Text ) ) {
								// пометка цветом и зачеркиванием удаленных книг с диска, но не из списка (быстрый режим удаления)
								WorksWithBooks.MarkRemoverFileInCopyesList( lvi );
							}
						}
						if( !xeGroup.HasElements ) {
							xeGroup.Remove();
						}
						bw.ReportProgress( ++i );
					} // по всем книгам Группы
					
					++GroupCountInGroups;
					++GroupCounterForXML;
					++ThroughGroupCounterForXML;
					doc.Root.Element("SelectedItem").SetValue(
						( m_LastSelectedItem <= GroupCountForList && ThroughGroupCounterForXML <= GroupCountForList )
						? m_LastSelectedItem.ToString()
						: "0"
					);
					if( GroupCountForList <= m_lvResult.Groups.Count ) {
						if( GroupCounterForXML >= GroupCountForList ) {
							setDataForNode( ref doc, GroupCountInGroups, BookInGroups );
							doc.Save( Path.Combine( m_FilePath, makeNNNStringOfNumber( ++XmlFileNumber ) + ".dup_lbc" ) );
							doc.Root.Element("Groups").Elements().Remove();
							GroupCountInGroups = 0;
							GroupCounterForXML = 0;
							BookInGroups = 0;
						} else {
							// последний диаппазон Групп
							if( ThroughGroupCounterForXML == m_lvResult.Groups.Count ) {
								setDataForNode( ref doc, GroupCountInGroups, BookInGroups );
								doc.Save( Path.Combine( m_FilePath, makeNNNStringOfNumber( ++XmlFileNumber ) + ".dup_lbc" ) );
							}
						}
					} else {
						setDataForNode( ref doc, GroupCountInGroups, BookInGroups );
						doc.Save( Path.Combine( m_FilePath, "001.dup_lbc" ) );
					}
				} // по всем Группам
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
			m_chBoxIsValid.Checked = Convert.ToBoolean( xmlTree.Element("Settings").Element("CheckValidate").Value );
			m_rbtnFB2Librusec.Checked = Convert.ToBoolean( xmlTree.Element("Settings").Element("GenresFB2Librusec").Value );
			
			//загрузка данных о ходе сравнения
			XElement compareData = xmlTree.Element("CompareData");
			m_StatusView.AllFiles = Convert.ToInt32( compareData.Element("AllFiles").Value );
			m_StatusView.Group = Convert.ToInt32( compareData.Element("Groups").Value );
			m_StatusView.AllFB2InGroups = Convert.ToInt32( compareData.Element("AllFB2InGroups").Value );
			m_lvFilesCount.Items[(int)FilesCountViewDupCollumn.AllDirs].SubItems[1].Text = compareData.Element("AllDirs").Value;
			
			ViewDupProgressData();

			// данные поиска
			Hashtable htBookGroups = new Hashtable(); // хеш-таблица групп одинаковых книг
			ListViewGroup	lvg = null; // группа одинаковых книг
			ListViewItem	lvi = null;
			ProgressBar.Maximum	= Convert.ToInt32( xmlTree.Element("Groups").Attribute("books").Value );
			IEnumerable<XElement> Groups = xmlTree.Element("Groups").Elements("Group");
			// перебор всех групп копий
			int i = 0;
			foreach( XElement Group in Groups ) {
				if( ( bw.CancellationPending ) )  {
					e.Cancel = true;
					return;
				}
				string GroupName = Group.Attribute("name").Value;
				// перебор всех книг в группе
				IEnumerable<XElement> books = Group.Elements("Book");
				foreach( XElement book in books ) {
					// в список - только существующие на диске книги
					if ( File.Exists( book.Element("Path").Value ) ) {
						string ForeColor = book.Element("ForeColor").Value;
						string BackColor = book.Element("BackColor").Value;
						lvg = new ListViewGroup( GroupName );
						lvi = new ListViewItem( book.Element("Path").Value );
						lvi.ForeColor = Color.FromName( ForeColor );
						lvi.BackColor = Color.FromName( BackColor );
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
						m_lvResult.Groups.Add( (ListViewGroup)htBookGroups[GroupName] );
						lvi.Group = (ListViewGroup)htBookGroups[GroupName];
						lvi.Checked = Convert.ToBoolean( book.Element("IsChecked").Value );
						m_lvResult.Items.Add( lvi );
					} else {
						--m_StatusView.AllFiles;
						--m_StatusView.AllFB2InGroups;
					}
					bw.ReportProgress( ++i );
				}
			}
			ViewDupProgressData();
			int SelectedItem = Convert.ToInt32( xmlTree.Element("SelectedItem").Value );
			MiscListView.SelectedItemEnsureVisible(m_lvResult, SelectedItem == -1 ? 0 : SelectedItem );
		}
		
		// создание хеш-таблицы для групп одинаковых книг
		private bool AddBookGroupInHashTable( ref Hashtable groups, ref ListViewGroup lvg ) {
			if( groups != null ){
				if( !groups.Contains( lvg.Header ) ) {
					groups.Add( lvg.Header, lvg );
					return true;
				}
			}
			return false;
		}
		#endregion
		
		// Отображение результата поиска сравнения
		private void ViewDupProgressData() {
			MiscListView.ListViewStatus( m_lvFilesCount, (int)FilesCountViewDupCollumn.AllBooks, m_StatusView.AllFiles );
			MiscListView.ListViewStatus( m_lvFilesCount, (int)FilesCountViewDupCollumn.AllGroups, m_StatusView.Group );
			MiscListView.ListViewStatus( m_lvFilesCount, (int)FilesCountViewDupCollumn.AllBoolsInAllGroups, m_StatusView.AllFB2InGroups );
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
