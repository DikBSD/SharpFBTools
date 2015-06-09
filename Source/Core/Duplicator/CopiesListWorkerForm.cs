/*
 * Сделано в SharpDevelop.
 * Пользователь: VadimK
 * Дата: 27.08.2014
 * Время: 12:23
 * 
 */
using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;

using Core.Misc;

using resultViewCollumn = Core.Misc.Enums.ResultViewCollumn;

// enums
using EndWorkModeEnum	= Core.Misc.Enums.EndWorkModeEnum;
using DuplWorkMode		= Core.Misc.Enums.DuplWorkMode;

namespace Core.Duplicator
{
	/// <summary>
	/// FileWorkerForm: форма прогреса обработки файлов (загрузка/сохранение списков, копирование/перемещение файлов)
	/// </summary>
	public partial class CopiesListWorkerForm : Form
	{
		#region Закрытые данные класса
		private readonly DuplWorkMode	m_WorkMode; // режим обработки книг
		private readonly string			m_FilePath				= string.Empty;
		private readonly bool			m_viewProgressStatus	= false;
		
		private readonly ComboBox		m_cboxMode			= new ComboBox();
		private readonly TextBox		m_tboxSourceDir		= new TextBox();
		private readonly CheckBox		m_chBoxScanSubDir	= new CheckBox();
		private readonly CheckBox 		m_chBoxIsValid		= new CheckBox();
		private readonly RadioButton	m_rbtnFB2Librusec	= new RadioButton();
		private readonly ListView		m_lvResult			= new ListView();
		private readonly ListView		m_lvFilesCount		= new ListView();
		private readonly MiscListView	m_mscLV				= new MiscListView(); // класс по работе с ListView
		private readonly StatusView		m_sv				= new StatusView();
		private readonly EndWorkMode	m_EndMode			= new EndWorkMode();
		
		private readonly int			m_LastSelectedItem	= -1; // выделенный итем, на котором закончилась обработка списка...
		
		private readonly DateTime		m_dtStart;
		private BackgroundWorker		m_bw = null; // фоновый обработчик
		#endregion

		public CopiesListWorkerForm( DuplWorkMode WorkMode, string FromFilePath, ComboBox cboxMode,
		                            ListView lvResult, ListView lvFilesCount, TextBox tboxSourceDir,
		                            CheckBox chBoxScanSubDir, CheckBox chBoxIsValid, RadioButton rbtnFB2Librusec,
		                            bool viewProgressStatus, int LastSelectedItem
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
			m_viewProgressStatus = viewProgressStatus;
			m_WorkMode			= WorkMode;
			
			m_LastSelectedItem = LastSelectedItem;
			
			InitializeBackgroundWorker();
			m_dtStart = DateTime.Now;
			
			if( !m_bw.IsBusy )
				m_bw.RunWorkerAsync(); //если не занят, то запустить процесс
		}
		
		// =============================================================================================
		// 								ОТКРЫТЫЕ СВОЙСТВА
		// =============================================================================================
		#region Открытые свойства
		public virtual Core.Misc.EndWorkMode EndMode {
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
			#region Код
			ProgressBar.Value = 0;
			switch( m_WorkMode) {
				case DuplWorkMode.SaveFB2CopiesList:
					this.Text = "Сохранение списка копий fb2 книг";
					saveCopiesListToXml( ref m_bw, ref e, m_FilePath, m_cboxMode.SelectedIndex, m_cboxMode.Text.Trim() );
					break;
				case DuplWorkMode.LoadFB2CopiesList:
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
			#endregion
		}
		
		// Отображение результата
		private void bw_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
			if( m_viewProgressStatus )
				ViewDupProgressData();
			++ProgressBar.Value;
		}
		
		// Проверяем - это отмена, ошибка, или конец задачи и сообщить
		private void bw_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {
			#region Код
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
			#endregion
		}
		#endregion
		
		// =============================================================================================
		// 				СОХРАНЕНИЕ В XML И ЗАГРУЗКА ИЗ XML СПИСКА КОПИЙ FB2 КНИГ
		// =============================================================================================
		#region Сохранение в xml и Загрузка из xml списка копий fb2 книг
		
		#region Сохранение в xml списка копий fb2 книг
		// сохранение списка копий книг в xml-файл
		private void saveCopiesListToXml( ref BackgroundWorker bw, ref DoWorkEventArgs e,
		                                 string ToFileName, int CompareMode, string CompareModeName ) {
			#region Код
			int GroupsCount = Convert.ToInt16( m_lvFilesCount.Items[5].SubItems[1].Text );
			int AllBookInAllGroups = Convert.ToInt16( m_lvFilesCount.Items[6].SubItems[1].Text );
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
				                          new XElement("AllDirs", m_lvFilesCount.Items[0].SubItems[1].Text),
				                          new XElement("AllFiles", m_lvFilesCount.Items[1].SubItems[1].Text),
				                          new XElement("FB2Files", m_lvFilesCount.Items[2].SubItems[1].Text),
				                          new XElement("Zip", m_lvFilesCount.Items[3].SubItems[1].Text),
				                          new XElement("Other", m_lvFilesCount.Items[4].SubItems[1].Text),
				                          new XElement("Groups", GroupsCount.ToString()),
				                          new XElement("AllFB2InGroups", AllBookInAllGroups.ToString())
				                         ),
				             new XComment("Копии fb2 книг по группам"),
				             new XElement("Groups",
				                          new XAttribute("count", GroupsCount.ToString()),
				                          new XAttribute("books", AllBookInAllGroups.ToString())
				                         ),
				             new XComment("Выделенный элемент списка, на котором завершили обработку книг"),
				             new XElement("SelectedItem", m_lvResult.SelectedItems[0].Index.ToString() )
				            )
			);
			
			// копии fb2 книг по группам
			if ( m_lvResult.Groups.Count > 0 ) {
				XElement xeGroup = null;
				int GroupNumber = 0;
				int FileNumber = 0;
				ProgressBar.Maximum	= m_lvResult.Items.Count;
				int i = 0;
				foreach (ListViewGroup lvGroup in m_lvResult.Groups ) {
					if( ( bw.CancellationPending ) )  {
						e.Cancel = true;
						return;
					}
					doc.Root.Element("Groups").Add(
						xeGroup = new XElement("Group", new XAttribute("number", 0),
						                       new XAttribute("count", lvGroup.Items.Count),
						                       new XAttribute("name", lvGroup.Header)
						                      )
					);
					int BookCountInGroup = 0;
					bool IsGroupRealBook = false;
					foreach ( ListViewItem lvi in lvGroup.Items ) {
						// только для "реальных" книг в списке (вигнорируем итемы в списке тех книг, которые были удалены с диска в режиме быстрого удаления)
						// а также только для Групп, в которых больше 1 книги.
						if( lvi.Group.Items.Count > 1 ) {
							// подсчет реальных книг в списке. Если больше одной, то не выводим в xml
							int RealBookInGroup = 0;
							foreach( ListViewItem item in lvGroup.Items ) {
								if( !item.Font.Strikeout )
									++RealBookInGroup;
							}
							if( RealBookInGroup > 1 ) {
								if( !lvi.Font.Strikeout ) {
									xeGroup.Add(new XElement("Book", new XAttribute("number", ++FileNumber),
									                         new XElement("Group", lvi.Group.Header),
									                         new XElement("Path", lvi.SubItems[(int)resultViewCollumn.Path].Text),
									                         new XElement("BookTitle", lvi.SubItems[(int)resultViewCollumn.BookTitle].Text),
									                         new XElement("Authors", lvi.SubItems[(int)resultViewCollumn.Authors].Text),
									                         new XElement("Genres", lvi.SubItems[(int)resultViewCollumn.Genres].Text),
									                         new XElement("BookID", lvi.SubItems[(int)resultViewCollumn.BookID].Text),
									                         new XElement("Version", lvi.SubItems[(int)resultViewCollumn.Version].Text),
									                         new XElement("Encoding", lvi.SubItems[(int)resultViewCollumn.Encoding].Text),
									                         new XElement("Validation", lvi.SubItems[(int)resultViewCollumn.Validate].Text),
									                         new XElement("FileLength", lvi.SubItems[(int)resultViewCollumn.FileLength].Text),
									                         new XElement("FileCreationTime", lvi.SubItems[(int)resultViewCollumn.CreationTime].Text),
									                         new XElement("FileLastWriteTime", lvi.SubItems[(int)resultViewCollumn.LastWriteTime].Text),
									                         new XElement("IsChecked", lvi.Checked)
									                        )
									           );
									xeGroup.SetAttributeValue( "count", ++BookCountInGroup );
									IsGroupRealBook = true;
								}
							}
						}
						bw.ReportProgress( ++i );
					}
					
					if( IsGroupRealBook )
						xeGroup.SetAttributeValue( "number", ++GroupNumber );
					
					if( !xeGroup.HasElements )
						xeGroup.Remove();
				}
			}
			doc.Save(ToFileName);
			#endregion
		}
		#endregion
		
		#region Загрузка из xml списка копий fb2 книг
		// загрузка из xml-файла в хэш таблицу данных о копиях книг
		private void loadCopiesListFromXML( ref BackgroundWorker bw, ref DoWorkEventArgs e, string FromXML ) {
			#region Код
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
			m_sv.AllFiles = Convert.ToInt32( compareData.Element("AllFiles").Value );
			m_sv.FB2 = Convert.ToInt32( compareData.Element("FB2Files").Value );
			m_sv.Zip = Convert.ToInt32( compareData.Element("Zip").Value );
			m_sv.Other = Convert.ToInt32( compareData.Element("Other").Value );
			m_sv.Group = Convert.ToInt32( compareData.Element("Groups").Value );
			m_sv.AllFB2InGroups = Convert.ToInt32( compareData.Element("AllFB2InGroups").Value );
			m_lvFilesCount.Items[0].SubItems[1].Text = compareData.Element("AllDirs").Value;
			
			ViewDupProgressData();

			// данные поиска
			Hashtable htBookGroups = new Hashtable(); // хеш-таблица групп одинаковых книг
			ListViewGroup	lvg = null; // группа одинаковых книг
			ListViewItem	lvi = null;
			ProgressBar.Maximum	= Convert.ToInt32( xmlTree.Element("Groups").Attribute("books").Value );
			IEnumerable<XElement> group = xmlTree.Element("Groups").Elements("Group");
			// перебор всех групп копий
			int i = 0;
			foreach( XElement g in group ) {
				if( ( bw.CancellationPending ) )  {
					e.Cancel = true;
					return;
				}
				string GroupName = g.Attribute("name").Value;
				// перебор всех книг в группе
				IEnumerable<XElement> books = g.Elements("Book");
				foreach( XElement book in books ) {
					lvg = new ListViewGroup( GroupName );
					lvi = new ListViewItem( book.Element("Path").Value );
					lvi.SubItems.Add( book.Element("BookTitle").Value );
					lvi.SubItems.Add( book.Element("Authors").Value );
					lvi.SubItems.Add( book.Element("Genres").Value );
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
					bw.ReportProgress( ++i );
				}
			}
			m_mscLV.SelectedItemEnsureVisible(m_lvResult, Convert.ToInt16( xmlTree.Element("SelectedItem").Value ) );
			#endregion
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
			m_mscLV.ListViewStatus( m_lvFilesCount, 1, m_sv.AllFiles );
			m_mscLV.ListViewStatus( m_lvFilesCount, 2, m_sv.FB2 );
			m_mscLV.ListViewStatus( m_lvFilesCount, 3, m_sv.Zip );
			m_mscLV.ListViewStatus( m_lvFilesCount, 4, m_sv.Other );
			m_mscLV.ListViewStatus( m_lvFilesCount, 5, m_sv.Group );
			m_mscLV.ListViewStatus( m_lvFilesCount, 6, m_sv.AllFB2InGroups );
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
