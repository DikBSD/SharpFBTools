/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 16.03.2009
 * Time: 10:03
 * 
 * License: GPL 2.1
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

using Core.FB2.Common;
using Core.FB2.Description;
using Core.FB2.Description.Common;
using Core.FB2.Description.CustomInfo;
using Core.FB2.Description.DocumentInfo;
using Core.FB2.Description.TitleInfo;
using Core.FB2Dublicator;
using Core.Misc;

using fB2Parser = Core.FB2Dublicator.FB2ParserForDup;
using FB2Validator = Core.FB2Parser.FB2Validator;
using filesWorker = Core.FilesWorker.FilesWorker;

//using fB2Parser 		= Core.FB2.FB2Parsers.FB2Parser;

namespace SharpFBTools.Tools
{

	/// <summary>
	/// Поиск копий fb2-файлов (различные критерии поиска)
	/// </summary>
	public partial class SFBTpFB2Dublicator : UserControl
	{
		#region Закрытые данные класса
		private StatusView	m_sv 			= new StatusView();
		private DateTime	m_dtStart;
		private BackgroundWorker m_bw		= null;
		private BackgroundWorker m_bwcmd	= null;
		private BackgroundWorker m_bwRenew	= null;
		private string	m_sSource			= string.Empty;
		private bool	m_bScanSubDirs		= true;
		private string	m_sMessTitle		= string.Empty;
		private bool	m_bCheckValid		= false;	// проверять или нет fb2-файл на валидность
		private string	m_sFileWorkerMode	= string.Empty;
		private MiscListView m_mscLV		= new MiscListView(); // класс по работе с ListView
		private List<string> m_FilesList	= new List<string>();
		private bool	m_bFilesWorked		= false; // флаг = true, если хоть один файл был на диске и был обработан (copy, move или delete)
		private bool	m_StopToSave		= false; // флаг = true, если остановка с сохранением необработанного списка книг в файл.
		private Hashtable m_htWorkingBook		= new Hashtable();  // таблица обработанные файлов - копии.
		private Hashtable m_htBookTitleAuthors	= new Hashtable();  // таблица для обработанных данных копий для режима группировки по Авторам.
		
		/// <summary>
		/// режимы сравнения книг
		/// </summary>
		private enum SearchCompareMode {
			Md5					= 0, // 0. Абсолютно одинаковые книги (md5)
			BookID				= 1, // 1. Одинаковый Id Книги (копии и/или разные версии правки одной и той же книги)
			BookTitle			= 2, // 2. Название Книги (могут быть найдены и разные книги разных Авторов, но с одинаковым Названием)
			AuthorAndTitle		= 3, // 3. Автор(ы) и Название Книги (одна и та же книга, сделанная разными людьми - разные Id, но Автор и Название - одинаковые)
		}
		
		/// <summary>
		/// Номера колонок контрола просмотра групп одинаковых книг
		/// </summary>
		private enum ResultViewCollumn {
			Path			= 0,	// Путь к книге
			BookTitle		= 1,	// Название книги
			Authors			= 2,	// Автор(ы)
			Genres			= 3,	// Жанр(ы)
			BookID			= 4,	// ID книги
			Version			= 5,	// Версия файла
			Encoding		= 6,	// Кодировка
			Validate		= 7,	// Валидность
			FileLength		= 8, 	// Размер файла
			CreationTime	= 9, 	// Время создания файла
			LastWriteTime	= 10, 	// Последнее изменение файла
		}
		
		/// <summary>
		/// Варианты сравнения книг в группах
		/// </summary>
		private enum CompareMode {
			Version,		// Версия файла
			Encoding,		// Кодировка
			Validate,		// Валидность
			FileLength, 	// Размер файла
			CreationTime, 	// Время создания файла
			LastWriteTime, 	// Последнее изменение файла
		}
		
		/// <summary>
		/// Данные о самой "новой" книге в группе
		/// </summary>
		private class FB2BookInfo {
			private int m_IndexVersion = 0;
			private int m_IndexCreationTime= 0;
			private int m_IndexLastWriteTime = 0;
			private string m_Version = "0";
			private string m_CreationTime = "0";
			private string m_LastWriteTime = "0";
			
			public virtual int IndexVersion {
				get { return m_IndexVersion; }
				set { m_IndexVersion = value; }
			}
			public virtual int IndexCreationTime {
				get { return m_IndexCreationTime; }
				set { m_IndexCreationTime = value; }
			}
			public virtual int IndexLastWriteTime {
				get { return m_IndexLastWriteTime; }
				set { m_IndexLastWriteTime = value; }
			}
			
			public virtual string Version {
				get { return m_Version; }
				set { m_Version = value; }
			}
			public virtual string CreationTime {
				get { return m_CreationTime; }
				set { m_CreationTime = value; }
			}
			public virtual string LastWriteTime {
				get { return m_LastWriteTime; }
				set { m_LastWriteTime = value; }
			}
		}
		#endregion
		
		public SFBTpFB2Dublicator()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			// задание для кнопок ToolStrip стиля и положения текста и картинки
			SetToolButtonsSettings();
			
			InitializeBackgroundWorker();
			InitializeFilesWorkerBackgroundWorker();
			InitializeRenewBackgroundWorker();
			
			// создание колонок просмотрщика найденных книг
			MakeColumns();
			Init();
			// читаем сохраненные пути к папкам Поиска одинаковых fb2-файлов, если они есть
			ReadFB2DupTempData();
			cboxMode.SelectedIndex			= (int)SearchCompareMode.Md5; // Условия для Сравнения fb2-файлов: md5 книги
			cboxDupExistFile.SelectedIndex	= 1; // добавление к создаваемому fb2-файлу очередного номера
		}
		// =============================================================================================
		// 									ОТКРЫТЫЕ МЕТОДЫ КЛАССА
		// =============================================================================================
		#region Открытые методы класса
		// задание для кнопок ToolStrip стиля и положения текста и картинки
		public void SetToolButtonsSettings() {
			Settings.SettingsFB2Dup.SetToolButtonsSettings( tsDup );
		}
		#endregion
		
		// =============================================================================================
		// 				BACKGROUNDWORKER ДЛЯ НЕПРЕРЫВНОГО СРАВНЕНИЯ и ПРЕРЫВАНИЯ / ВОЗОБНОВЛЕНИЯ
		// =============================================================================================
		#region BackgroundWorker для Непрерывного сравнения и прерывания / возобновления
		
		// =============================================================================================
		//			BackgroundWorker: Непрерывное Сравнение
		// =============================================================================================
		#region BackgroundWorker: Непрерывное Сравнение
		private void InitializeBackgroundWorker() {
			// Инициализация перед использование BackgroundWorker
			m_bw = new BackgroundWorker();
			m_bw.WorkerReportsProgress		= true; // Позволить выводить прогресс процесса
			m_bw.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
			m_bw.DoWork 			+= new DoWorkEventHandler( bw_DoWork );
			m_bw.ProgressChanged 	+= new ProgressChangedEventHandler( bw_ProgressChanged );
			m_bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler( bw_RunWorkerCompleted );
		}
		
		// поиск одинаковых fb2-файлов
		private void bw_DoWork( object sender, DoWorkEventArgs e ) {
			#region Код
			List<string> lDirList = new List<string>();
			m_FilesList.Clear();
			if( !m_bScanSubDirs ) {
				// сканировать только указанную папку
				filesWorker.makeFilesListFromDir( m_sSource, ref m_FilesList, true );
				lvFilesCount.Items[0].SubItems[1].Text = "1";
			} else {
				// сканировать и все подпапки
				lvFilesCount.Items[0].SubItems[1].Text =
					filesWorker.recursionDirsSearch( m_sSource, ref lDirList, true ).ToString();
				m_sv.AllFiles = filesWorker.makeFilesListFromDirs( ref m_bw, ref e, ref lDirList, ref m_FilesList, true );
				lvFilesCount.Items[1].SubItems[1].Text = m_sv.AllFiles.ToString();
			}
			lDirList.Clear();
			m_bw.ReportProgress( 0 ); // отобразим данные в контролах

			if( ( m_bw.CancellationPending ) ) {
				e.Cancel = true;
				return;
			}
			
			// проверка, есть ли хоть один файл в папке для сканирования
			if( m_sv.AllFiles == 0 ) {
				MessageBox.Show( "В папке сканирования не найдено ни одного файла!\nРабота прекращена.", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				tsslblProgress.Text = Settings.Settings.GetReady();
				setSearchFB2DupStartEnabled( true );
				return;
			}
			
			// очистка контролов вывода данных по книге по ее выбору
			ClearDataFields();
			
			// Сравнение fb2-файлов
			ConnectListViewResultEventHandlers( false );
			m_htWorkingBook.Clear();
			m_htBookTitleAuthors.Clear();
			
			// Создание списка копий fb2-книг по Группам
			makeBookCopiesGroups( ref m_bw, ref e, (SearchCompareMode) cboxMode.SelectedIndex,
			                     ref m_FilesList, ref m_htWorkingBook, ref m_htBookTitleAuthors );
			lvResult.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
			#endregion
		}
		
		// Отображение результата
		private void bw_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
			if( chBoxViewProgress.Checked )
				ViewDupProgressData();
			++tsProgressBar.Value;
		}
		
		// Проверяем - это отмена, ошибка, или конец задачи и сообщить
		private void bw_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {
			DateTime dtEnd = DateTime.Now;
			ViewDupProgressData(); // отображение данных прогресса
			ConnectListViewResultEventHandlers( true );
			filesWorker.RemoveDir( Settings.Settings.GetTempDir() );

			string sTime = dtEnd.Subtract( m_dtStart ).ToString() + " (час.:мин.:сек.)";
			string sMessDone = "Поиск одинаковых fb2-файлов завершен!\nЗатрачено времени: "+sTime;
			string sMessError = string.Empty;
			string sMessCanceled = string.Empty;
			
			if (m_StopToSave) {
				// остановка поиска копий с сохранением списка необработанных книг в файл
				// сохранение в xml-файл списка данных о копиях и необработанных книг
				sfdList.Title = "Укажите файл для возобновления поиска копий книг:";
				sfdList.Filter = "SharpFBTools Файлы хода работы Дубликатора (*.dup_break)|*.dup_break";
				sMessCanceled = "Поиск одинаковых fb2 файлов прерван!\nДанные поиска и список оставшихся для обработки книг будут сохранены в xml-файл:\n"+sfdList.FileName+"\nЗатрачено времени: "+sTime;
				sfdList.FileName = string.Empty;
				sfdList.InitialDirectory = Settings.Settings.GetProgDir();
				DialogResult result = sfdList.ShowDialog();
				if( result == DialogResult.OK )
					saveSearchDataToXmlFile( sfdList.FileName, cboxMode.SelectedIndex, cboxMode.Text, ref m_FilesList);
			} else {
				// просто остановка поиска
				sMessCanceled = "Поиск одинаковых fb2-файлов остановлен!\nСписок Групп копий fb2-файлов не сформирован полностью!\nЗатрачено времени: "+sTime;
			}
			
			if ( lvResult.Items.Count == 0 )
				sMessDone += "\n\nНе найдено НИ ОДНОЙ копии книг!";
			
			tsslblProgress.Text = Settings.Settings.GetReady();
			setSearchFB2DupStartEnabled( true );
			
			if ( ( e.Cancelled ) ) {
				MessageBox.Show( sMessCanceled, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			} else if( e.Error != null ) {
				sMessError = "Error!\n" + e.Error.Message + "\n" + e.Error.StackTrace + "\nЗатрачено времени: "+sTime;
				MessageBox.Show( sMessError, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			} else {
				MessageBox.Show( sMessDone, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
			m_sv.Clear();
			m_FilesList.Clear();
			m_htWorkingBook.Clear();
			m_htBookTitleAuthors.Clear();
		}
		#endregion

		// =============================================================================================
		// 			Сравнение завершено: Сохранение в xml и Загрузка из xml списка копий fb2 книг
		// =============================================================================================
		#region Сравнение завершено: Сохранение в xml и Загрузка из xml списка копий fb2 книг
		// сохранение списка копий книг в xml-файл
		private void saveCopiesListToXml(string ToFileName, int CompareMode, string CompareModeName) {
			#region Код
			XDocument doc = new XDocument(
				new XDeclaration("1.0", "utf-8", "yes"),
				new XElement("Files",
				             new XComment("Папка для поиска копий fb2 книг"),
				             new XElement("SourceDir", m_sSource),
				             new XComment("Настройки поиска-сравнения fb2 книг"),
				             new XElement("Settings",
				                          new XElement("ScanSubDirs", chBoxScanSubDir.Checked),
				                          new XElement("CheckValidate", chBoxIsValid.Checked)),
				             new XComment("Режим поиска-сравнения fb2 книг"),
				             new XElement("CompareMode",
				                          new XAttribute("index", CompareMode),
				                          new XElement("Name", CompareModeName)),
				             new XComment("Данные о ходе сравнения fb2 книг"),
				             new XElement("CompareData",
				                          new XElement("AllDirs", lvFilesCount.Items[0].SubItems[1].Text),
				                          new XElement("AllFiles", lvFilesCount.Items[1].SubItems[1].Text),
				                          new XElement("FB2Files", lvFilesCount.Items[2].SubItems[1].Text),
				                          new XElement("Zip", lvFilesCount.Items[3].SubItems[1].Text),
				                          new XElement("Other", lvFilesCount.Items[4].SubItems[1].Text),
				                          new XElement("Groups", lvFilesCount.Items[5].SubItems[1].Text),
				                          new XElement("AllFB2InGroups", lvFilesCount.Items[6].SubItems[1].Text)
				                         ),
				             new XComment("Копии fb2 книг по группам"),
				             new XElement("Groups", new XAttribute("count", lvResult.Groups.Count.ToString()))
				            )
			);
			
			// копии fb2 книг по группам
			if ( lvResult.Groups.Count > 0 ) {
				XElement xeGroup = null;
				int groupNumber = 0;
				int fileNumber = 0;
				foreach (ListViewGroup lvGroup in lvResult.Groups ) {
					doc.Root.Element("Groups").Add(
						xeGroup = new XElement("Group", new XAttribute("number", groupNumber++),
						                       new XAttribute("count", lvGroup.Items.Count),
						                       new XAttribute("name", lvGroup.Header)
						                      )
					);
					foreach ( ListViewItem lvi in lvGroup.Items ) {
						xeGroup.Add(new XElement("Book", new XAttribute("number", fileNumber++),
						                         new XElement("Group", lvi.Group.Header),
						                         new XElement("Path", lvi.SubItems[(int)ResultViewCollumn.Path].Text),
						                         new XElement("BookTitle", lvi.SubItems[(int)ResultViewCollumn.BookTitle].Text),
						                         new XElement("Authors", lvi.SubItems[(int)ResultViewCollumn.Authors].Text),
						                         new XElement("Genres", lvi.SubItems[(int)ResultViewCollumn.Genres].Text),
						                         new XElement("BookID", lvi.SubItems[(int)ResultViewCollumn.BookID].Text),
						                         new XElement("Version", lvi.SubItems[(int)ResultViewCollumn.Version].Text),
						                         new XElement("Encoding", lvi.SubItems[(int)ResultViewCollumn.Encoding].Text),
						                         new XElement("Validation", lvi.SubItems[(int)ResultViewCollumn.Validate].Text),
						                         new XElement("FileLength", lvi.SubItems[(int)ResultViewCollumn.FileLength].Text),
						                         new XElement("FileCreationTime", lvi.SubItems[(int)ResultViewCollumn.CreationTime].Text),
						                         new XElement("FileLastWriteTime", lvi.SubItems[(int)ResultViewCollumn.LastWriteTime].Text)
						                        )
						           );
					}
				}
			}
			doc.Save(ToFileName);
			#endregion
		}
		
		// загрузка из xml-файла в хэш таблицу данных о копиях книг
		private void loadCopiesListFromXML( string FromXML ) {
			XElement xmlTree = XElement.Load( FromXML );
			
			// выставляем режим сравнения
			int CompareMode = Convert.ToInt16( xmlTree.Element("CompareMode").Attribute("index").Value );
			cboxMode.SelectedIndex = CompareMode;
			
			// устанавливаем данные настройки поиска-сравнения
			m_sSource = tboxSourceDir.Text = xmlTree.Element("SourceDir").Value;
			m_bScanSubDirs = chBoxScanSubDir.Checked = Convert.ToBoolean( xmlTree.Element("Settings").Element("ScanSubDirs").Value );
			m_bCheckValid = chBoxIsValid.Checked = Convert.ToBoolean( xmlTree.Element("Settings").Element("CheckValidate").Value );
			
			//загрузка данных о ходе сравнения
			XElement compareData = xmlTree.Element("CompareData");
			m_sv.AllFiles = Convert.ToInt32( compareData.Element("AllFiles").Value );
			m_sv.FB2 = Convert.ToInt32( compareData.Element("FB2Files").Value );
			m_sv.Zip = Convert.ToInt32( compareData.Element("Zip").Value );
			m_sv.Other = Convert.ToInt32( compareData.Element("Other").Value );
			m_sv.Group = Convert.ToInt32( compareData.Element("Groups").Value );
			m_sv.AllFB2InGroups = Convert.ToInt32( compareData.Element("AllFB2InGroups").Value );
			lvFilesCount.Items[0].SubItems[1].Text = compareData.Element("AllDirs").Value;
			
			ViewDupProgressData();

			// данные поиска
			Hashtable htBookGroups = new Hashtable(); // хеш-таблица групп одинаковых книг
			ListViewGroup	lvg = null; // группа одинаковых книг
			ListViewItem	lvi = null;
			IEnumerable<XElement> groups = xmlTree.Element("Groups").Elements("Group");
			// перебор всех групп копий
			foreach( XElement group in groups ) {
				string GroupName = group.Attribute("name").Value;
				// перебор всех книг в группе
				IEnumerable<XElement> books = group.Elements("Book");
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
					lvResult.Groups.Add( (ListViewGroup)htBookGroups[GroupName] );
					lvi.Group = (ListViewGroup)htBookGroups[GroupName];
					lvResult.Items.Add( lvi );
				}
			}
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
		
		// =============================================================================================
		//	Общие для Полного и Прерванного сканирования Алгоритмы создания списков копий книг по Группам
		// =============================================================================================
		#region Общие для Полного и Прерванного сканирования Алгоритмы создания списков копий книг по Группам
		
		// Создание списка копий fb2-книг по Группам
		private void makeBookCopiesGroups( ref BackgroundWorker bw, ref DoWorkEventArgs e,
		                                  SearchCompareMode CompareMode, ref List<string> FilesList,
		                                  ref Hashtable htWorkingBook, ref Hashtable htBookTitleAuthors ) {
			switch ( CompareMode ) {
				case SearchCompareMode.Md5:
					// 0. Абсолютно одинаковые книги (md5)
					// Хэширование fb2-файлов
					FilesHashForMd5Parser( ref bw, ref e, ref FilesList, ref htWorkingBook );
					// Создание списка копий для режима "0. Абсолютно одинаковые книги (md5)"
					makeBookCopiesForMd5( ref bw, ref e, ref htWorkingBook );
					break;
				case SearchCompareMode.BookID:
					// 1. Одинаковый Id Книги (копии и/или разные версии правки одной и той же книги)
					// Хэширование fb2-файлов
					FilesHashForIDParser( ref bw, ref e, ref FilesList, ref htWorkingBook );
					// Создание списка копий для режима "1. Одинаковый Id Книги (копии и/или разные версии правки одной и той же книги)"
					makeBookCopiesForID( ref bw, ref e, ref htWorkingBook );
					break;
				case SearchCompareMode.BookTitle:
					// 2. Название Книги (могут быть найдены и разные книги разных Авторов, но с одинаковым Названием)
					// Хэширование fb2-файлов
					FilesHashForBTParser( ref bw, ref e, ref FilesList, ref htWorkingBook );
					// Создание списка копий для режима "2. Название Книги (могут быть найдены и разные книги разных Авторов, но с одинаковым Названием)"
					makeBookCopiesForBT( ref bw, ref e, ref htWorkingBook );
					break;
				case SearchCompareMode.AuthorAndTitle:
					// 3. Автор(ы) и Название Книги (одна и та же книга, сделанная разными людьми - разные Id, но Автор и Название - одинаковые)
					// Хэширование fb2-файлов
					FilesHashForBTParser( ref bw, ref e, ref FilesList, ref htWorkingBook );
					// Хэширование по одинаковым Авторам в пределах сгенерированных групп книг по одинаковым названиям
					FilesHashForAuthorsParser( ref bw, ref e, ref htWorkingBook, ref htBookTitleAuthors );
					// Создание списка копий для режима "3. Автор(ы) и Название Книги (одна и та же книга, сделанная разными людьми - разные Id, но Автор и Название - одинаковые)"
					makeBookCopiesForABT3( ref bw, ref e, ref htBookTitleAuthors );
					break;
			}
		}
		
		// Создание списка копий для режима "0. Абсолютно одинаковые книги (md5)"
		private void makeBookCopiesForMd5( ref BackgroundWorker bw, ref DoWorkEventArgs e, ref Hashtable htFB2ForMd5 ) {
			tsslblProgress.Text		= "Создание списка одинаковых fb2-файлов:";
			tsProgressBar.Maximum	= htFB2ForMd5.Values.Count;
			tsProgressBar.Value		= 0;
			// сортировка ключей (групп)
			List<string> keyList = makeSortedKeysForGroups( ref htFB2ForMd5 );
			
			foreach( string key in keyList ) {
				if( ( bw.CancellationPending ) ) {
					e.Cancel = true;
					return;
				}
				++m_sv.Group; // число групп одинаковых книг
				// формирование представления Групп с их книгами
				makeBookCopiesView( (FB2FilesDataInGroup)htFB2ForMd5[key] );
				bw.ReportProgress( 0 );
			}
		}
		
		// Создание списка копий для режима "1. Одинаковый Id Книги (копии и/или разные версии правки одной и той же книги)"
		private void makeBookCopiesForID( ref BackgroundWorker bw, ref DoWorkEventArgs e, ref Hashtable htFB2ForID ) {
			tsslblProgress.Text		= "Создание списка одинаковых fb2-файлов:";
			tsProgressBar.Maximum	= htFB2ForID.Values.Count;
			tsProgressBar.Value		= 0;
			
			// сортировка ключей (групп)
			List<string> keyList = makeSortedKeysForGroups( ref htFB2ForID );
			
			foreach( string key in keyList ) {
				if( ( bw.CancellationPending ) ) {
					e.Cancel = true;
					return;
				}
				++m_sv.Group; // число групп одинаковых книг
				// формирование представления Групп с их книгами
				makeBookCopiesView( (FB2FilesDataInGroup)htFB2ForID[key] );
				bw.ReportProgress( 0 );
			}
		}
		
		// Создание списка копий для режима "2. Название Книги (могут быть найдены и разные книги разных Авторов, но с одинаковым Названием)"
		private void makeBookCopiesForBT( ref BackgroundWorker bw, ref DoWorkEventArgs e, ref Hashtable htFB2ForBT ) {
			tsslblProgress.Text		= "Создание списка одинаковых fb2-файлов:";
			tsProgressBar.Maximum	= htFB2ForBT.Values.Count;
			tsProgressBar.Value		= 0;
			// сортировка ключей (групп)
			List<string> keyList = makeSortedKeysForGroups( ref htFB2ForBT );
			foreach( string key in keyList ) {
				if( ( bw.CancellationPending ) ) {
					e.Cancel = true;
					return;
				}
				//TODO ???// разбивка на группы для одинакового Названия по Авторам
				++m_sv.Group; // число групп одинаковых книг
				// формирование представления Групп с их книгами
				makeBookCopiesView( (FB2FilesDataInGroup)htFB2ForBT[key] );
				bw.ReportProgress( 0 );
			}
		}
		
		// Хэширование по одинаковым Авторам в пределах сгенерированных групп книг по одинаковым названиям
		private void FilesHashForAuthorsParser( ref BackgroundWorker bw, ref DoWorkEventArgs e,
		                                       ref Hashtable htFB2ForABT, ref Hashtable htBookTitleAuthors ) {
			tsslblProgress.Text		= "Хеширование по Авторам:";
			tsProgressBar.Maximum	= htFB2ForABT.Values.Count;
			tsProgressBar.Value		= 0;
			// генерация списка ключей хеш-таблицы (для удаления обработанного элемента таблицы)
			List<string> keyList = makeSortedKeysForGroups( ref htFB2ForABT );
			// группировка книг по одинаковым Авторам в пределах сгенерированных Групп книг по одинаковым Названиям
			foreach( string key in keyList ) {
				// разбивка на группы для одинакового Названия по Авторам
				Hashtable htGroupAuthors = FindDupForAuthors( ref bw, ref e, (FB2FilesDataInGroup)htFB2ForABT[key], false );
				if( ( bw.CancellationPending ) ) {
					e.Cancel = true;
					return;
				}
				foreach( FB2FilesDataInGroup fb2List in htGroupAuthors.Values )
					htBookTitleAuthors.Add(fb2List.Group, fb2List);
				// удаление обработанной группы книг, сгруппированных по одинаковому названию
				htFB2ForABT.Remove(key);
				bw.ReportProgress( 0 );
			}
		}
		
		// Создание списка копий для режима "3. Автор(ы) и Название Книги (одна и та же книга, сделанная разными людьми - разные Id, но Автор и Название - одинаковые)"
		private void makeBookCopiesForABT3( ref BackgroundWorker bw, ref DoWorkEventArgs e, ref Hashtable htBookTitleAuthors ) {
			tsslblProgress.Text		= "Создание списка одинаковых fb2-файлов:";
			tsProgressBar.Maximum	= htBookTitleAuthors.Values.Count;
			tsProgressBar.Value		= 0;
			// отображение данных копий книг по Группам
			// сортировка ключей (групп)
			List<string> keyList = makeSortedKeysForGroups( ref htBookTitleAuthors );
			foreach( string key in keyList ) {
				if( ( bw.CancellationPending ) ) {
					e.Cancel = true;
					return;
				}
				++m_sv.Group; // число групп одинаковых книг
				// формирование представления Групп с их книгами
				makeBookCopiesView( (FB2FilesDataInGroup)htBookTitleAuthors[key] );
				bw.ReportProgress( 0 );
			}
		}
		
		// удаление элементов таблицы, value (списки) которых состоят из 1-го элемента (это не копии)
		private void removeNotCopiesEntryInHashTable( ref Hashtable ht ) {
			List<DictionaryEntry> notCopies = new List<DictionaryEntry>();
			foreach (DictionaryEntry entry in ht) {
				FB2FilesDataInGroup fb2f = (FB2FilesDataInGroup)entry.Value;
				if (fb2f.Count==1)
					notCopies.Add(entry);
			}
			foreach (var ent in notCopies)
				ht.Remove(ent.Key);
		}
		
		// удаление из списка всех файлов обработанные книги (файлы)
		private void removeFinishedFilesInFilesList( ref List<string> FilesList, ref List<string> FinishedFilesList) {
			List<string> FilesToWorkingList = new List<string>();
			foreach (var file in FilesList.Except(FinishedFilesList))
				FilesToWorkingList.Add(file);
			
			FilesList.Clear();
			FilesList.AddRange(FilesToWorkingList);
		}
		
		// хеширование файлов в контексте Md5 книг:
		// 0. Абсолютно одинаковые книги (md5)
		// параметры: FilesList - список файлов для сканирования
		private void FilesHashForMd5Parser( ref BackgroundWorker bw, ref DoWorkEventArgs e, ref List<string> FilesList, ref Hashtable htFB2ForMd5 ) {
			tsslblProgress.Text		= "Хэширование fb2-файлов:";
			tsProgressBar.Maximum	= FilesList.Count;
			tsProgressBar.Value		= 0;
			
			List<string> FinishedFilesList = new List<string>();
			for( int i=0; i!=FilesList.Count; ++i ) {
				if( ( bw.CancellationPending ) )  {
					// удаление из списка всех файлов обработанные книги (файлы)
					removeFinishedFilesInFilesList( ref FilesList, ref FinishedFilesList);
					e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
					return;
				}
				string sExt = Path.GetExtension( FilesList[i] ).ToLower();
				if( sExt==".fb2" ) {
					++m_sv.FB2;
					// заполнение хеш таблицы данными о fb2-книгах в контексте их md5
					MakeFB2Md5HashTable( FilesList[i], ref htFB2ForMd5 );
					// обработанные файлы
					FinishedFilesList.Add(FilesList[i]);
				}  else {
					if( sExt==".zip" )
						++m_sv.Zip;		// пропускаем архивы
					else
						++m_sv.Other;	// пропускаем не fb2-файлы
				}
				bw.ReportProgress( 0 ); // отобразим данные в контролах
			}
			// удаление элементов таблицы, value (списки) которых состоят из 1-го элемента (это не копии)
			removeNotCopiesEntryInHashTable( ref htFB2ForMd5 );
			// удаление из списка всех файлов обработанные книги (файлы)
			removeFinishedFilesInFilesList( ref FilesList, ref FinishedFilesList);
		}
		
		// заполнение хеш таблицы данными о fb2-книгах в контексте их md5
		// параметры: Path - путь к fb2-файлу; htFB2ForMd5 - хеш-таблица
		private void MakeFB2Md5HashTable( string Path, ref Hashtable htFB2ForMd5 ) {
			try {
				string md5 = ComputeMD5Checksum( Path );
				fB2Parser fb2 = new fB2Parser( Path );
				string Encoding = filesWorker.GetFileEncoding( fb2.XmlDoc.InnerXml.Split('>')[0] );
				if( Encoding == null )
					Encoding = "?";
				string ID = fb2.Id;
				if( ID == null )
					return;

				if( ID.Length == 0 )
					ID = "Тег <id> в этих книгах \"пустой\"";
				
				// данные о книге
				BookData fb2BookData = new BookData( fb2.BookTitle, fb2.Authors, fb2.Genres, ID, fb2.Version, Path, Encoding );
				
				if( !htFB2ForMd5.ContainsKey( md5 ) ) {
					// такой книги в числе дублей еще нет
					FB2FilesDataInGroup fb2f = new FB2FilesDataInGroup( fb2BookData, md5 );
					htFB2ForMd5.Add( md5, fb2f );
				} else {
					// такая книга в числе дублей уже есть
					FB2FilesDataInGroup fb2f = (FB2FilesDataInGroup)htFB2ForMd5[md5] ;
					fb2f.AddBookData( fb2BookData );
					//htFB2ForMd5[md5] = fb2f; //ИЗБЫТОЧНЫЙ КОД
				}
			} catch {} // пропускаем проблемные файлы
		}
		
		// хеширование файлов в контексте Id книг:
		// 1. Одинаковый Id Книги (копии и/или разные версии правки одной и той же книги)
		// параметры: FilesList - список файлов для сканирования
		private void FilesHashForIDParser( ref BackgroundWorker bw, ref DoWorkEventArgs e, ref List<string> FilesList, ref Hashtable htFB2ForID ) {
			tsslblProgress.Text		= "Хэширование fb2-файлов:";
			tsProgressBar.Maximum	= FilesList.Count;
			tsProgressBar.Value		= 0;
			
			List<string> FinishedFilesList = new List<string>();
			for( int i=0; i!=FilesList.Count; ++i ) {
				if( ( bw.CancellationPending ) )  {
					// удаление из списка всех файлов обработанные книги (файлы)
					removeFinishedFilesInFilesList( ref FilesList, ref FinishedFilesList);
					e.Cancel = true;
					return;
				}
				string Ext = Path.GetExtension( FilesList[i] ).ToLower();
				if( Ext==".fb2" ) {
					++m_sv.FB2;
					// заполнение хеш таблицы данными о fb2-книгах в контексте их ID
					MakeFB2IDHashTable( FilesList[i], ref htFB2ForID );
					// обработанные файлы
					FinishedFilesList.Add(FilesList[i]);
				}  else {
					if( Ext==".zip" )
						++m_sv.Zip;	// пропускаем архивы
					else
						++m_sv.Other;	// пропускаем не fb2-файлы
				}
				bw.ReportProgress( 0 ); // отобразим данные в контролах
			}
			// удаление элементов таблицы, value (списки) которых состоят из 1-го элемента (это не копии)
			removeNotCopiesEntryInHashTable( ref htFB2ForID );
			// удаление из списка всех файлов обработанные книги (файлы)
			removeFinishedFilesInFilesList( ref FilesList, ref FinishedFilesList);
		}
		
		// заполнение хеш таблицы данными о fb2-книгах в контексте их ID
		// параметры: Path - путь к fb2-файлу; htFB2ForID - хеш-таблица
		private void MakeFB2IDHashTable( string Path, ref Hashtable htFB2ForID ) {
			try {
				fB2Parser fb2 = new fB2Parser( Path );
				string Encoding = filesWorker.GetFileEncoding( fb2.XmlDoc.InnerXml.Split('>')[0] );
				if( Encoding == null )
					Encoding = "?";
				string ID = fb2.Id;
				if( ID==null )
					return;

				if( ID.Length==0 )
					ID = "Тег <id> в этих книгах \"пустой\"";
				
				// данные о книге
				BookData fb2BookData = new BookData( fb2.BookTitle, fb2.Authors, fb2.Genres, ID, fb2.Version, Path, Encoding );
				
				if( !htFB2ForID.ContainsKey( ID ) ) {
					// такой книги в числе дублей еще нет
					FB2FilesDataInGroup fb2f = new FB2FilesDataInGroup( fb2BookData, ID );
					htFB2ForID.Add( ID, fb2f );
				} else {
					// такая книга в числе дублей уже есть
					FB2FilesDataInGroup fb2f = (FB2FilesDataInGroup)htFB2ForID[ID] ;
					fb2f.AddBookData( fb2BookData );
					//htFB2ForID[sID] = fb2f; //ИЗБЫТОЧНЫЙ КОД
				}
			} catch {} // пропускаем проблемные файлы
		}
		
		// хеширование файлов в контексте Авторов и Названия книг:
		// 2. Название Книги (могут быть найдены и разные книги разных Авторов, но с одинаковым Названием)
		// 3. Автор(ы) и Название Книги (одна и та же книга, сделанная разными людьми - разные Id, но Автор и Название - одинаковые)
		// параметры: FilesList - список файлов для сканирования
		private void FilesHashForBTParser( ref BackgroundWorker bw, ref DoWorkEventArgs e, ref List<string> FilesList, ref Hashtable htFB2ForBT ) {
			tsslblProgress.Text		= "Хэширование fb2-файлов:";
			tsProgressBar.Maximum	= FilesList.Count;
			tsProgressBar.Value		= 0;
			
			List<string> FinishedFilesList = new List<string>();
			for( int i=0; i!=FilesList.Count; ++i ) {
				if( ( bw.CancellationPending ) )  {
					// удаление из списка всех файлов обработанные книги (файлы)
					removeFinishedFilesInFilesList( ref FilesList, ref FinishedFilesList);
					e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
					return;
				}
				string Ext = Path.GetExtension( FilesList[i] ).ToLower();
				if( Ext==".fb2" ) {
					++m_sv.FB2;
					// заполнение хеш таблицы данными о fb2-книгах в контексте их Авторов и Названия
					MakeFB2BTHashTable( FilesList[i], ref htFB2ForBT );
					// обработанные файлы
					FinishedFilesList.Add(FilesList[i]);
				} else {
					if( Ext==".zip" )
						++m_sv.Zip;	// пропускаем архивы
					else
						++m_sv.Other;	// пропускаем не fb2-файлы
				}
				bw.ReportProgress( 0 ); // отобразим данные в контролах
			}
			// удаление элементов таблицы, value (списки) которых состоят из 1-го элемента (это не копии)
			removeNotCopiesEntryInHashTable( ref htFB2ForBT );
			// удаление из списка всех файлов обработанные книги (файлы)
			removeFinishedFilesInFilesList( ref FilesList, ref FinishedFilesList);
		}
		
		// заполнение хеш таблицы данными о fb2-книгах в контексте их Названия
		// параметры: Path - путь к fb2-файлу; htFB2ForABT - хеш-таблица
		private void MakeFB2BTHashTable( string Path, ref Hashtable htFB2ForBT ) {
			try {
				fB2Parser fb2 = new fB2Parser( Path );
				BookTitle bookTitle	= fb2.BookTitle;
				if( bookTitle!=null && bookTitle.Value!=null ) {
					string Encoding = filesWorker.GetFileEncoding( fb2.XmlDoc.InnerXml.Split('>')[0] );
					if( Encoding == null )
						Encoding = "?";
					// данные о книге
					BookData fb2BookData = new BookData( bookTitle, fb2.Authors, fb2.Genres, fb2.Id, fb2.Version, Path, Encoding );
					string BT = bookTitle.Value.Trim();
					if( !htFB2ForBT.ContainsKey( BT ) ) {
						// такой книги в числе дублей еще нет
						FB2FilesDataInGroup fb2f = new FB2FilesDataInGroup( fb2BookData, BT );
						htFB2ForBT.Add( BT, fb2f );
					} else {
						// такая книга в числе дублей уже есть
						FB2FilesDataInGroup fb2f = (FB2FilesDataInGroup)htFB2ForBT[BT];
						fb2f.AddBookData( fb2BookData );
						//htFB2ForID[sBT] = fb2f; //ИЗБЫТОЧНЫЙ КОД
					}
				}
			} catch {} // пропускаем проблемные файлы
		}

		// создание групп копий по Авторам, относительно найденного Названия Книги
		private Hashtable FindDupForAuthors( ref BackgroundWorker bw, ref DoWorkEventArgs e, FB2FilesDataInGroup fb2Group, bool WithMiddleName ) {
			// в fb2Group.Group - название группы (название книги у всех книг одинаковое, а пути - разные )
			// внутри fb2Group в BookData - данные на каждую книгу группы
			Hashtable ht = new Hashtable();
			// 2 итератора для перебора всех книг группы. 1-й - только на текущий элемент группы, 2-й - скользящий на все последующие. т.е. iter2 = iter1+1
			for( int iter1=0; iter1!=fb2Group.Count; ++iter1 ) {
				if( ( bw.CancellationPending ) )  {
					e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
					return null;
				}
				BookData bd1 = fb2Group[iter1]; // текущая книга
				FB2FilesDataInGroup fb2NewGroup = new FB2FilesDataInGroup();
				// перебор всех книг в группе, за исключением текущей
				for( int iter2=iter1+1; iter2!=fb2Group.Count; ++iter2 ) {
					// сравнение текущей книги со всеми последующими
					BookData bd2 = fb2Group[iter2];
					if ( bd1.isSameBook(bd2, WithMiddleName) ) {
						if (!fb2NewGroup.isBookExists(bd2.Path))
							fb2NewGroup.Add( bd2 );
					}
				}
				if( fb2NewGroup.Count >= 1 ) {
					// только для копий, а не для единичных книг
					fb2NewGroup.Group = bd1.BookTitle.Value + " ( " + fb2NewGroup.makeAutorsString(WithMiddleName) + " )";
					fb2NewGroup.Insert( 0, bd1 );
					if ( !ht.ContainsKey(fb2NewGroup.Group) )
						ht.Add( fb2NewGroup.Group, fb2NewGroup );
				}
			}
			return ht;
		}
		#endregion

		// =============================================================================================
		//					BackgroundWorker: Возобновления поиска копий книг
		// =============================================================================================
		#region BackgroundWorker: Возобновления поиска копий книг
		private void InitializeRenewBackgroundWorker() {
			m_bwRenew = new BackgroundWorker();
			m_bwRenew.WorkerReportsProgress			= true; // Позволить выводить прогресс процесса
			m_bwRenew.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
			m_bwRenew.DoWork 			+= new DoWorkEventHandler( m_bwRenew_renewSearchDataFromFile_DoWork );
			m_bwRenew.ProgressChanged 	+= new ProgressChangedEventHandler( bw_ProgressChanged );
			m_bwRenew.RunWorkerCompleted += new RunWorkerCompletedEventHandler( bw_RunWorkerCompleted );
		}
		
		// возобновление проверки - загрузка отчета о найденных копиях и о необработанных книгах из xml-файла
		private void m_bwRenew_renewSearchDataFromFile_DoWork( object sender, DoWorkEventArgs e ) {
			// загрузка данных из xml
			sfdLoadList.InitialDirectory = Settings.Settings.GetProgDir();
			sfdLoadList.Title		= "Укажите файл для возобновления поиска копий книг:";
			sfdLoadList.Filter		= "SharpFBTools Файлы хода работы Дубликатора (*.dup_break)|*.dup_break";
			sfdLoadList.FileName	= string.Empty;
			string FromXML = string.Empty;
			DialogResult result = sfdLoadList.ShowDialog();
			XElement xmlTree = null;
			if( result == DialogResult.OK )
				xmlTree = XElement.Load( sfdLoadList.FileName );
			else {
				tsslblProgress.Text = Settings.Settings.GetReady();
				setSearchFB2DupStartEnabled( true );
				return;
			}
			
			// выставляем режим сравнения
			int CompareMode = Convert.ToInt16( xmlTree.Element("CompareMode").Attribute("index").Value );
			cboxMode.SelectedIndex = CompareMode;
			
			// устанавливаем данные настройки поиска-сравнения
			m_sSource = tboxSourceDir.Text = xmlTree.Element("SourceDir").Value;
			m_bScanSubDirs = chBoxScanSubDir.Checked = Convert.ToBoolean( xmlTree.Element("Settings").Element("ScanSubDirs").Value );
			m_bCheckValid = chBoxIsValid.Checked = Convert.ToBoolean( xmlTree.Element("Settings").Element("CheckValidate").Value );
			
			//загрузка данных о ходе сравнения
			XElement compareData = xmlTree.Element("CompareData");
			m_sv.AllFiles = Convert.ToInt32( compareData.Element("AllFiles").Value );
			m_sv.FB2 = Convert.ToInt32( compareData.Element("FB2Files").Value );
			lvFilesCount.Items[0].SubItems[1].Text = compareData.Element("AllDirs").Value;
			
			ViewDupProgressData();
			
			// заполнение списка необработанных файлов
			m_FilesList.Clear();
			IEnumerable<XElement> files = xmlTree.Element("NotWorkingFiles").Elements("File");
			foreach (XElement element in files)
				m_FilesList.Add(element.Value);
			
			// заполнение хэш-таблицы
			m_htWorkingBook.Clear();
			m_htBookTitleAuthors.Clear();
			
			ConnectListViewResultEventHandlers( false );

			// загрузка из xml-файла в хэш таблицу данных о копиях книг
			loadFromXMLToHashtable( ref m_bwRenew, (SearchCompareMode) CompareMode, ref xmlTree, ref m_htWorkingBook );
			// Создание списка копий fb2-книг по Группам
			makeBookCopiesGroups( ref m_bwRenew, ref e, (SearchCompareMode) CompareMode,
			                     ref m_FilesList, ref m_htWorkingBook, ref m_htBookTitleAuthors );
			lvResult.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
		}
		
		// загрузка из xml-файла в хэш таблицу данных о копиях книг для всех режимов
		private void loadFromXMLToHashtable( ref BackgroundWorker bw, SearchCompareMode CompareMode, ref XElement xmlTree, ref Hashtable ht ) {
			tsslblProgress.Text = "Загрузка групп fb2-файлов:";
			tsProgressBar.Maximum	= Convert.ToUInt16( xmlTree.Element("Groups").Attribute("count").Value );
			if( CompareMode == SearchCompareMode.AuthorAndTitle )
				tsProgressBar.Maximum += Convert.ToUInt16( xmlTree.Element("BookTitleGroup").Attribute("count").Value );
			tsProgressBar.Value	= 0;
			
			_loadFromXMLToHashtable( ref bw, ref xmlTree, "Groups", ref ht );
			if( CompareMode == SearchCompareMode.AuthorAndTitle )
				_loadFromXMLToHashtable( ref bw, ref xmlTree, "BookTitleGroup", ref ht );
		}
		
		// загрузка из xml-файла в хэш таблицу данных о копиях книг для всех режимов
		private void _loadFromXMLToHashtable( ref BackgroundWorker bw, ref XElement xmlTree, string Element, ref Hashtable ht ) {
			IEnumerable<XElement> groups = xmlTree.Element(Element).Elements("Group");
			// перебор всех групп копий
			foreach( XElement group in groups ) {
				// перебор всех книг в группе
				IEnumerable<XElement> books = group.Elements("Book");
				foreach( XElement book in books ) {
					string Group = book.Element("Group").Value;
					BookTitle bookTitle = new BookTitle(book.Element("BookTitle").Value);
					// загрузка авторов книги
					IList<Author> authors = null;
					XElement xeAuthors = book.Element("Authors");
					if (xeAuthors != null) {
						authors = new List<Author>();
						IEnumerable<XElement> iexeAuthors = from el in book.Descendants("Author") select el;
						foreach (XElement a in iexeAuthors) {
							XElement xeFirstName = a.Element("FirstName");
							XElement xeMiddleName = a.Element("MiddleName");
							XElement xeLastName = a.Element("LastName");
							XElement xeNickName = a.Element("NickName");
							Author author = new Author( xeFirstName!= null ? transformToTextFieldType(xeFirstName.Value) : null,
							                           xeMiddleName != null ? transformToTextFieldType(xeMiddleName.Value) : null,
							                           xeLastName != null ? transformToTextFieldType(xeLastName.Value) : null,
							                           xeNickName != null ? transformToTextFieldType(xeNickName.Value) : null
							                          );
							authors.Add(author);
						}
					}
					// загрузка жанров книги
					IList<Genre> genres = null;
					XElement xeGenres = book.Element("Genres");
					if (xeGenres != null) {
						genres = new List<Genre>();
						IEnumerable<XElement> iexeGenre = from el in book.Descendants("Genre") select el;
						foreach( XElement g in iexeGenre ) {
							Genre genre = new Genre( g.Value );
							genre.Math = Convert.ToUInt32( g.Attribute("match").Value );
							genres.Add( genre );
						}
					}
					// данные о книге
					BookData fb2BookData = new BookData( bookTitle, authors, genres, book.Element("BookID").Value,
					                                    book.Element("Version").Value, book.Element("Path").Value,
					                                    book.Element("Encoding").Value );
					//заполнение хеш таблицы данными о fb2-книгах
					if( !ht.ContainsKey( Group ) ) {
						// такой книги в числе дублей еще нет
						FB2FilesDataInGroup fb2f = new FB2FilesDataInGroup( fb2BookData, Group );
						ht.Add( Group, fb2f );
					} else {
						// такая книга в числе дублей уже есть
						FB2FilesDataInGroup fb2f = (FB2FilesDataInGroup)ht[Group] ;
						fb2f.AddBookData( fb2BookData );
						//ht[Group] = fb2f; //ИЗБЫТОЧНЫЙ КОД
					}
				}
				bw.ReportProgress( 0 );
			}
		}
		#endregion
		
		// =============================================================================================
		// 						Прерывание сравнения: Сохранение данных в xml
		// =============================================================================================
		#region Прерывание сравнения: Сохранение данных в xml
		// сохранение данных о найденных копиях и о необработанных книгах при прерывании проверки для записи
		private void saveSearchDataToXmlFile(string ToFileName, int CompareMode, string CompareModeName, ref List<string> FilesList) {
			int fileNumber = 0;
			int groupNumber = 0;
			XDocument doc = new XDocument(
				new XDeclaration("1.0", "utf-8", "yes"),
				new XElement("Files",
				             new XComment("Папка для поиска копий книг"),
				             new XElement("SourceDir", m_sSource),
				             new XComment("Настройки поиска-сравнения"),
				             new XElement("Settings",
				                          new XElement("ScanSubDirs", chBoxScanSubDir.Checked),
				                          new XElement("CheckValidate", chBoxIsValid.Checked)),
				             new XComment("Режим поиска-сравнения"),
				             new XElement("CompareMode",
				                          new XAttribute("index", CompareMode),
				                          new XElement("Name", CompareModeName)),
				             new XComment("Данные о ходе сравнения"),
				             new XElement("CompareData",
				                          new XElement("AllDirs", lvFilesCount.Items[0].SubItems[1].Text),
				                          new XElement("AllFiles", m_sv.AllFiles),
				                          new XElement("FB2Files", m_sv.FB2),
				                          new XElement("Zip", m_sv.Zip),
				                          new XElement("Other", m_sv.Other),
				                          new XElement("Groups", m_sv.Group),
				                          new XElement("AllFB2InGroups", m_sv.AllFB2InGroups)
				                         ),
				             new XComment("Обработанные файлы"),
				             new XElement("Groups", new XAttribute("count",
				                                                   (SearchCompareMode)CompareMode == SearchCompareMode.AuthorAndTitle
				                                                   ? m_htBookTitleAuthors.Count : m_htWorkingBook.Count)),
				             new XComment("Не обработанные файлы"),
				             (SearchCompareMode)CompareMode == SearchCompareMode.AuthorAndTitle
				             ? new XElement("BookTitleGroup", new XAttribute("count", m_htWorkingBook.Count))
				             : null,
				             new XElement("NotWorkingFiles", new XAttribute("count", m_FilesList.Count))
				            )
			);
			
			// обработанные книги
			List<string> keyList = null;
			switch ( (SearchCompareMode)CompareMode ) {
				case SearchCompareMode.Md5:
					// 0. Абсолютно одинаковые книги (md5)
					keyList = sortKeys( ref m_htWorkingBook );
					saveFB2FilesInGroupToXML( ref m_htWorkingBook, ref keyList, ref groupNumber, ref fileNumber, ref doc, "Groups" );
					break;
				case SearchCompareMode.BookID:
					// 1. Одинаковый Id Книги (копии и/или разные версии правки одной и той же книги)
					keyList = sortKeys( ref m_htWorkingBook );
					saveFB2FilesInGroupToXML( ref m_htWorkingBook, ref keyList, ref groupNumber, ref fileNumber, ref doc, "Groups" );
					break;
				case SearchCompareMode.BookTitle:
					// 2. Название Книги (могут быть найдены и разные книги разных Авторов, но с одинаковым Названием)
					keyList = sortKeys( ref m_htWorkingBook );
					saveFB2FilesInGroupToXML( ref m_htWorkingBook, ref keyList, ref groupNumber, ref fileNumber, ref doc, "Groups" );
					break;
				case SearchCompareMode.AuthorAndTitle:
					// 3. Автор(ы) и Название Книги (одна и та же книга, сделанная разными людьми - разные Id, но Автор и Название - одинаковые)
					// список необработанных файлов по группам
					keyList = sortKeys( ref m_htWorkingBook );
					saveFB2FilesInGroupToXML( ref m_htWorkingBook, ref keyList, ref groupNumber, ref fileNumber, ref doc, "BookTitleGroup" );
					// список Групп с копиями
					groupNumber = 0;
					keyList = sortKeys( ref m_htBookTitleAuthors );
					saveFB2FilesInGroupToXML( ref m_htBookTitleAuthors, ref keyList, ref groupNumber, ref fileNumber, ref doc, "Groups" );
					break;
			}
			
			// необработанные книги
			if ( FilesList.Count > 0 ) {
				fileNumber = 0;
				for (int i=0; i!=FilesList.Count; ++i)
					doc.Root.Element("NotWorkingFiles").Add(
						new XElement("File", new XAttribute("number", fileNumber++),
						             new XElement("Path", FilesList[i])
						            )
					);
			}
			doc.Save(ToFileName);
		}
		
		// сохранение групп копий fb2 файлов в xml файл
		private void saveFB2FilesInGroupToXML( ref Hashtable htFB2InGroup, ref List<string> keyList, ref int groupNumber, ref int fileNumber,
		                                      ref XDocument doc, string ToElement ) {
			if ( keyList.Count > 0 ) {
				XElement xeGroup = null;
				foreach( string key in keyList ) {
					FB2FilesDataInGroup fb2f = (FB2FilesDataInGroup)htFB2InGroup[key];
					doc.Root.Element(ToElement).Add(
						xeGroup = new XElement("Group",
						                       new XAttribute("number", groupNumber++),
						                       new XAttribute("count", fb2f.Count),
						                       new XAttribute("name", fb2f.Group)
						                      )
					);
					xmlSaveBookData( ref xeGroup, fb2f, ref fileNumber );
				}
			}
		}
		
		// сортировка ключей (названия групп)
		private List<string> sortKeys( ref Hashtable hash ) {
			List<string> keyList = new List<string>();
			foreach( string key in hash.Keys )
				keyList.Add(key);
			keyList.Sort();
			return keyList;
		}
		
		// сохранение данных о книге в xml-файл
		private void xmlSaveBookData( ref XElement xeGroup, FB2FilesDataInGroup fb2f, ref int fileNumber ) {
			XElement xeAuthors;
			XElement xeGenres;
			foreach ( BookData bd in fb2f ) {
				xeGroup.Add(new XElement("Book", new XAttribute("number", fileNumber++),
				                         new XElement("Group", fb2f.Group),
				                         new XElement("Path", bd.Path),
				                         new XElement("BookID", bd.Id),
				                         new XElement("Encoding", bd.Encoding),
				                         new XElement("Version", bd.Version),
				                         new XElement("BookTitle", MakeBookTitleString( bd.BookTitle )),
				                         xeAuthors = new XElement("Authors"),
				                         xeGenres = new XElement("Genres")
				                        )
				           );
				// сохранение данных об авторах конкретной книги в xml-файл
				if( bd.Authors!=null ) {
					xeAuthors.Add(new XElement("Author"));
					XElement xeAuthor = xeAuthors.Element("Author");
					foreach( Author a in bd.Authors ) {
						if( a.LastName!=null && a.LastName.Value!=null )
							xeAuthor.Add(new XElement("LastName", a.LastName.Value));
						if( a.FirstName!=null && a.FirstName.Value!=null )
							xeAuthor.Add(new XElement("FirstName", a.FirstName.Value));
						if( a.MiddleName!=null && a.MiddleName.Value!=null )
							xeAuthor.Add(new XElement("MiddleName", a.MiddleName.Value));
						if( a.NickName!=null && a.NickName.Value!=null )
							xeAuthor.Add(new XElement("NickName", a.NickName.Value));
					}
				}
				// сохранение данных о жанрах конкретной книги в xml-файл
				if( bd.Genres==null )
					xeGenres.Add(new XElement("Genre", "?"));
				else {
					foreach( Genre g in bd.Genres )
						if( g.Name!=null )
						xeGenres.Add( new XElement("Genre", new XAttribute("match", g.Math), g.Name) );
				}
			}
		}

		private TextFieldType transformToTextFieldType(string Value) {
			TextFieldType textField = new TextFieldType();
			textField.Value = Value;
			return textField;
		}
		#endregion

		#endregion
		
		
		// =============================================================================================
		// 						BACKGROUNDWORKER: КОПИРОВАНИЕ / ПЕРЕМИЕЩЕНИЕ / УДАЛЕНИЕ
		// =============================================================================================
		#region BackgroundWorker: Копирование / Перемещение / Удаление
		
		// =============================================================================================
		//			BackgroundWorker: Копирование / Перемещение / Удаление
		// =============================================================================================
		#region BackgroundWorker: Копирование / Перемещение / Удаление
		private void InitializeFilesWorkerBackgroundWorker() {
			// Инициализация перед использование BackgroundWorker Копирование / Перемещение / Удаление
			m_bwcmd = new BackgroundWorker();
			m_bwcmd.WorkerReportsProgress		= true; // Позволить выводить прогресс процесса
			m_bwcmd.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
			m_bwcmd.DoWork				+= new DoWorkEventHandler( bwcmd_DoWork );
			m_bwcmd.ProgressChanged 	+= new ProgressChangedEventHandler( bwcmd_ProgressChanged );
			m_bwcmd.RunWorkerCompleted	+= new RunWorkerCompletedEventHandler( bwcmd_RunWorkerCompleted );
		}
		
		// Обработка файлов
		private void bwcmd_DoWork( object sender, DoWorkEventArgs e ) {
			m_bFilesWorked = false;
			ConnectListViewResultEventHandlers( false );
			switch( m_sFileWorkerMode ) {
				case "Copy":
					CopyOrMoveFilesTo( m_bwcmd, e, true,
					                  m_sSource, tboxDupToDir.Text.Trim(), lvResult,
					                  cboxDupExistFile.SelectedIndex,
					                  tsslblProgress, "Копирование помеченных копий книг:" );
					break;
				case "Move":
					CopyOrMoveFilesTo( m_bwcmd, e, false,
					                  m_sSource, tboxDupToDir.Text.Trim(), lvResult,
					                  cboxDupExistFile.SelectedIndex,
					                  tsslblProgress, "Перемещение помеченных копий книг:" );
					break;
				case "Delete":
					DeleteFiles( m_bwcmd, e, lvResult, tsslblProgress, "Удаление помеченных копий книг:" );
					break;
				default:
					return;
			}
		}
		
		// Отобразим результат Копирования / Перемещения / Удаление
		private void bwcmd_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
			// новое число групп и книг во всех группах
			m_mscLV.ListViewStatus( lvFilesCount, 5, lvResult.Groups.Count.ToString() );
			// число книг во всех группах
			m_mscLV.ListViewStatus( lvFilesCount, 6, lvResult.Items.Count.ToString() );
			++tsProgressBar.Value;
		}
		
		// Завершение работы Обработчика Файлов
		private void bwcmd_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {
			ConnectListViewResultEventHandlers( true );
			string sMessCanceled, sMessError, sMessDone, sTabPageDefText, sMessTitle;
			sMessCanceled = sMessError = sMessDone = sTabPageDefText = sMessTitle = string.Empty;
			switch( m_sFileWorkerMode ) {
				case "Copy":
					sMessTitle		= "SharpFBTools - Копирование копий книг";
					sMessDone 		= "Копирование файлов в указанную папку завершено!";
					sMessCanceled	= "Копирование файлов в указанную папку остановлено!";
					break;
				case "Move":
					sMessTitle		= "SharpFBTools - Перемещение копий книг";
					sMessDone 		= "Перемещение файлов в указанную папку завершено!";
					sMessCanceled	= "Перемещение файлов в указанную папку остановлено!";
					break;
				case "Delete":
					sMessTitle		= "SharpFBTools - Удаление копий книг";
					sMessDone 		= "Удаление файлов из папки-источника завершено!";
					sMessCanceled	= "Удаление файлов из папки-источника остановлено!";
					break;
			}
			
			if( !m_bFilesWorked ) {
				string s = "На диске не найдено ни одного файла из помеченных!\n";
				switch( m_sFileWorkerMode ) {
					case "Copy":
						sMessDone = s + "Копирование файлов в указанную папку не произведено!";
						break;
					case "Move":
						sMessDone = s + "Перемещение файлов в указанную папку не произведено!";
						break;
					case "Delete":
						sMessDone = s + "Удаление файлов из папки-источника не произведено!";
						break;
				}
			}

			tsslblProgress.Text = Settings.Settings.GetReady();
			setFilesWorkerStartEnabled( true );
			tsProgressBar.Visible = false;
			
			// Проверяем это отмена, ошибка, или конец задачи и сообщить
			if( ( e.Cancelled ) ) {
				MessageBox.Show( sMessCanceled, sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			} else if( e.Error != null ) {
				sMessError = "Ошибка:\n" + e.Error.Message + "\n" + e.Error.StackTrace;
				MessageBox.Show( sMessError, sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			} else {
				MessageBox.Show( sMessDone, sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
		}
		
		#endregion

		// =============================================================================================
		// 					Реализация Copy/Move/Delete помеченных книг
		// =============================================================================================
		#region Реализация Copy/Move/Delete помеченных книг
		// Копирование, перемещение, удаление файлов
		public void CopyOrMoveFilesTo( BackgroundWorker bw, DoWorkEventArgs e,
		                              bool IsCopy, string SourceDir, string TargetDir,
		                              ListView lvResult, int nFileExistMode,
		                              ToolStripStatusLabel tsslblProgress, string ProgressText ) {
			// копировать или переместить файлы в...
			#region Код
			tsslblProgress.Text = ProgressText;
			System.Windows.Forms.ListView.CheckedListViewItemCollection checkedItems = lvResult.CheckedItems;
			int i=0;
			foreach( ListViewItem lvi in checkedItems ) {
				// Проверить флаг на остановку процесса
				if( ( bw.CancellationPending ) ) {
					e.Cancel = true; // Выставить окончание - по отмене, сработает событие bwcmd_RunWorkerCompleted
					return;
				} else {
					string FilePath = lvi.Text;
					// есть ли такая книга на диске? Если нет - то смотрим следующую
					if( !File.Exists( FilePath ) ) {
						bw.ReportProgress( ++i ); // отобразим данные в контролах
						break;
					}
					string NewPath = TargetDir + FilePath.Remove( 0, SourceDir.Length );
					FileInfo fi = new FileInfo( NewPath );
					if( !fi.Directory.Exists )
						Directory.CreateDirectory( fi.Directory.ToString() );

					if( File.Exists( NewPath ) ) {
						if( nFileExistMode==0 )
							File.Delete( NewPath );
						else
							NewPath = filesWorker.createFilePathWithSufix( NewPath, nFileExistMode );
					}
					
					Regex rx = new Regex( @"\\+" );
					FilePath = rx.Replace( FilePath, "\\" );
					if( File.Exists( FilePath ) ) {
						if( IsCopy )
							File.Copy( FilePath, NewPath );
						else {
							File.Move( FilePath, NewPath );
							ListViewGroup lvg = lvi.Group;
							lvResult.Items.Remove( lvi );
							if( lvg.Items.Count == 0 )
								lvResult.Groups.Remove( lvg );
						}
						m_bFilesWorked |= true;
					}
					bw.ReportProgress( ++i ); // отобразим данные в контролах
				}
			}
			#endregion
		}
		
		public void DeleteFiles( BackgroundWorker bw, DoWorkEventArgs e,
		                        ListView lvResult,
		                        ToolStripStatusLabel tsslblProgress, string sProgressText ) {
			// удалить помеченные файлы...
			#region Код
			tsslblProgress.Text = sProgressText;
			System.Windows.Forms.ListView.CheckedListViewItemCollection checkedItems = lvResult.CheckedItems;
			int i=0;
			foreach( ListViewItem lvi in checkedItems ) {
				// Проверить флаг на остановку процесса
				if( ( bw.CancellationPending ) ) {
					e.Cancel = true; // Выставить окончание - по отмене, сработает событие bwcmd_RunWorkerCompleted
					return;
				} else {
					string sFilePath = lvi.Text;
					if( File.Exists( sFilePath) ) {
						File.Delete( sFilePath );
						ListViewGroup lvg = lvi.Group;
						lvResult.Items.Remove( lvi );
						if( lvg.Items.Count == 0 ) lvResult.Groups.Remove( lvg );
						m_bFilesWorked |= true;
					}
					bw.ReportProgress( ++i ); // отобразим данные в контролах
				}
			}
			#endregion
		}
		#endregion
		
		#endregion

		
		// =============================================================================================
		// 							ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ И АЛГОРИТМЫ КЛАССА
		// =============================================================================================
		#region Вспомогательные методы и алгоритмы класса

		// =============================================================================================
		// 										Анализатор копий книг
		// =============================================================================================
		#region Анализатор копий книг
		// пометка "старых" книг
		private void _CheckAllOldBooksInGroup(CompareMode mode, ListViewGroup lvGroup)
		{
			#region Код
			// перебор всех книг в выбранной группе
			FB2BookInfo bookInfo = new FB2BookInfo();
			foreach( ListViewItem lvi in lvGroup.Items ) {
				if (lvi.SubItems[(int)ResultViewCollumn.Version].Text != string.Empty) {
					switch( mode) {
						case CompareMode.Version:
							if ( bookInfo.Version.Replace('.', ',').CompareTo(lvi.SubItems[(int)ResultViewCollumn.Version].Text.Replace('.', ',')) < 0 ) {
								// если текущая книга более новая
								bookInfo.Version = lvi.SubItems[(int)ResultViewCollumn.Version].Text;
								bookInfo.IndexVersion = lvi.Index;
							}
							break;
						case CompareMode.CreationTime:
							// какой файл позднее создан
							if ( bookInfo.CreationTime.CompareTo(lvi.SubItems[(int)ResultViewCollumn.CreationTime].Text) < 0 ) {
								bookInfo.CreationTime = lvi.SubItems[(int)ResultViewCollumn.CreationTime].Text;
								bookInfo.IndexCreationTime = lvi.Index;
							}
							break;
						case CompareMode.LastWriteTime:
							// какой файл позднее правился
							if ( bookInfo.LastWriteTime.CompareTo(lvi.SubItems[(int)ResultViewCollumn.LastWriteTime].Text) < 0 ) {
								bookInfo.LastWriteTime = lvi.SubItems[(int)ResultViewCollumn.LastWriteTime].Text;
								bookInfo.IndexLastWriteTime = lvi.Index;
							}
							break;
					}
				}
			}
			// помечаем все книги в группе, кроме самой "новой"
			foreach (ListViewItem item in lvGroup.Items ) {
				switch( mode) {
					case CompareMode.Version:
						if (item.Index != bookInfo.IndexVersion)
							lvResult.Items[item.Index].Checked = true;
						break;
					case CompareMode.CreationTime:
						if (item.Index != bookInfo.IndexCreationTime)
							lvResult.Items[item.Index].Checked = true;
						break;
					case CompareMode.LastWriteTime:
						if (item.Index != bookInfo.IndexLastWriteTime)
							lvResult.Items[item.Index].Checked = true;
						break;
				}
			}
			#endregion
		}
		
		// пометить в выбранной группе все "старые" книги
		private void CheckAllOldBooksInGroup(CompareMode mode)
		{
			ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
			if (si.Count > 0) {
				// группа для выделенной книги
				ListViewGroup lvg = si[0].Group;
				m_mscLV.CheckAllListViewItemsInGroup( lvg, false );
				_CheckAllOldBooksInGroup(mode, lvg);
			}
		}
		
		// пометить в каждой группе все "старые" книги
		private void CheckAllOldBooksInAllGroups(CompareMode mode)
		{
			m_mscLV.UnCheckAllListViewItems( lvResult.CheckedItems );
			// перебор всех групп
			foreach( ListViewGroup lvg in lvResult.Groups ) {
				// перебор всех книг в выбранной группе
				_CheckAllOldBooksInGroup(mode, lvg);
			}
		}
		#endregion
		
		// =============================================================================================
		// 											Разное
		// =============================================================================================
		#region Разное
		// создание колонок просмотрщика найденных книг
		private void MakeColumns() {
			lvResult.Columns.Add( "Путь к книге", 300 );
			lvResult.Columns.Add( "Название", 180 );
			lvResult.Columns.Add( "Автор(ы)", 180 );
			lvResult.Columns.Add( "Жанр(ы)", 180 );
			lvResult.Columns.Add( "ID", 200 );
			lvResult.Columns.Add( "Версия", 50 );
			lvResult.Columns.Add( "Кодировка", 90, HorizontalAlignment.Center );
			lvResult.Columns.Add( "Валидность", 50, HorizontalAlignment.Center );
			lvResult.Columns.Add( "Размер", 90, HorizontalAlignment.Center );
			
			lvResult.Columns.Add( "Дата создания", 120 );
			lvResult.Columns.Add( "Последнее изменение", 120 );
		}
		
		// формирование представления Групп с их книгами
		private void makeBookCopiesView( FB2FilesDataInGroup fb2BookList ) {
			Hashtable htBookGroups = new Hashtable(); // хеш-таблица групп одинаковых книг
			ListViewGroup lvGroup = null; // группа одинаковых книг
			foreach( BookData bd in fb2BookList ) {
				++m_sv.AllFB2InGroups; // число книг во всех группах одинаковых книг
				lvGroup = new ListViewGroup( fb2BookList.Group );
				ListViewItem lvi = new ListViewItem( bd.Path );
				lvi.SubItems.Add( MakeBookTitleString( bd.BookTitle ) );
				lvi.SubItems.Add( MakeAutorsString( bd.Authors, false ) );
				lvi.SubItems.Add( MakeGenresString( bd.Genres, false ) );
				lvi.SubItems.Add( bd.Id );
				lvi.SubItems.Add( bd.Version );
				lvi.SubItems.Add( bd.Encoding );
				lvi.SubItems.Add( m_bCheckValid ? IsValid( bd.Path ) : "?" );
				lvi.SubItems.Add( GetFileLength( bd.Path ) );
				lvi.SubItems.Add( GetFileCreationTime( bd.Path ) );
				lvi.SubItems.Add( FileLastWriteTime( bd.Path ) );
				// заносим группу в хеш, если она там отсутствует
				AddBookGroupInHashTable( ref htBookGroups, ref lvGroup );
				// присваиваем группу книге
				lvResult.Groups.Add( (ListViewGroup)htBookGroups[fb2BookList.Group] );
				lvi.Group = (ListViewGroup)htBookGroups[fb2BookList.Group];
				lvResult.Items.Add( lvi );
			}
		}
		
		// формирование строки с Названием Книги
		private string MakeBookTitleString( BookTitle bookTitle ) {
			if ( bookTitle==null )
				return "?";
			return bookTitle.Value!=null ? bookTitle.Value : "?";
		}
		
		// формирование строки с Авторами Книги из списка всех Авторов ЭТОЙ Книги
		private string MakeAutorsString( IList<Author> Authors, bool bNumber ) {
			if( Authors==null )
				return "Тег <authors> в книге отсутствует";
			string sA = string.Empty; int n = 0;
			foreach( Author a in Authors ) {
				++n;
				if( a.LastName!=null && a.LastName.Value!=null )
					sA += a.LastName.Value+" ";
				if( a.FirstName!=null && a.FirstName.Value!=null )
					sA += a.FirstName.Value+" ";
				if( a.MiddleName!=null && a.MiddleName.Value!=null )
					sA += a.MiddleName.Value+" ";
				if( a.NickName!=null && a.NickName.Value!=null )
					sA += a.NickName.Value;
				sA = sA.Trim();
				sA += "; ";
			}
			if( bNumber )
				sA = Convert.ToString(n)+": " + sA;
			return sA.Substring( 0, sA.LastIndexOf( ";" ) ).Trim();
		}
		
		// формирование строки с Жанрами Книги из списка всех Жанров ЭТОЙ Книги
		private string MakeGenresString( IList<Genre> Genres, bool bNumber ) {
			if( Genres==null )
				return "?";
			string sG = string.Empty; int n = 0;
			foreach( Genre g in Genres ) {
				++n;
				if( g.Name!=null )
					sG += g.Name;
				sG += "; ";
			}
			sG = sG.Trim();
			if( bNumber )
				sG = Convert.ToString(n)+": " + sG;
			return sG.Substring( 0, sG.LastIndexOf( ";" ) ).Trim();
		}
		
		// сортировка ключей (групп)
		private List<string> makeSortedKeysForGroups( ref Hashtable htBookGroups ) {
			List<string> keyList = new List<string>();
			foreach( string key in htBookGroups.Keys )
				keyList.Add(key);
			keyList.Sort();
			return keyList;
		}
		
		// Преобразование картинки в base64
		private string ImageToBase64(Image image, System.Drawing.Imaging.ImageFormat format) {
			using (MemoryStream ms = new MemoryStream())
			{
				// Convert Image to byte[]
				image.Save(ms, format);
				byte[] imageBytes = ms.ToArray();

				// Convert byte[] to Base64 String
				string base64String = Convert.ToBase64String(imageBytes);
				return base64String;
			}
		}
		
		// Получение картинки из base64
		private Image Base64ToImage(string base64String) {
			// Convert Base64 String to byte[]
			byte[] imageBytes = Convert.FromBase64String(base64String);
			MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

			// Convert byte[] to Image
			ms.Write(imageBytes, 0, imageBytes.Length);
			Image image = Image.FromStream(ms, true);
			return image;
		}
		
		// Вычисление SHA1 файла
		private string ComputeSHA1Checksum(string path) {
			using (FileStream fs = System.IO.File.OpenRead(path))
			{
				SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
				byte[] fileData = new byte[fs.Length];
				fs.Read(fileData, 0, (int)fs.Length);
				byte[] checkSum = sha1.ComputeHash(fileData);
				string hash = BitConverter.ToString(checkSum).Replace("-", String.Empty);
				return hash;
			}
		}
		
		// Вычисление MD5 файла
		private string ComputeMD5Checksum(string path) {
			using (FileStream fs = System.IO.File.OpenRead(path))
			{
				MD5 md5 = new MD5CryptoServiceProvider();
				byte[] fileData = new byte[fs.Length];
				fs.Read(fileData, 0, (int)fs.Length);
				byte[] checkSum = md5.ComputeHash(fileData);
				string result = BitConverter.ToString(checkSum).Replace("-", String.Empty);
				return result;
			}
		}
		
		// отключение/включение обработчиков событий для lvResult (убираем "тормоза")
		private void ConnectListViewResultEventHandlers( bool bConnect ) {
			if( !bConnect ) {
				// отключаем обработчики событий для lvResult (убираем "тормоза")
				lvResult.BeginUpdate();
				this.lvResult.ItemChecked -= new System.Windows.Forms.ItemCheckedEventHandler(this.LvResultItemChecked);
				this.lvResult.SelectedIndexChanged -= new System.EventHandler(this.LvResultSelectedIndexChanged);
			} else {
				lvResult.EndUpdate();
				this.lvResult.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.LvResultItemChecked);
				this.lvResult.SelectedIndexChanged += new System.EventHandler(this.LvResultSelectedIndexChanged);
			}
		}
		
		// Отображение результата поиска сравнения
		private void ViewDupProgressData() {
			m_mscLV.ListViewStatus( lvFilesCount, 1, m_sv.AllFiles );
			m_mscLV.ListViewStatus( lvFilesCount, 2, m_sv.FB2 );
			m_mscLV.ListViewStatus( lvFilesCount, 3, m_sv.Zip );
			m_mscLV.ListViewStatus( lvFilesCount, 4, m_sv.Other );
			m_mscLV.ListViewStatus( lvFilesCount, 5, m_sv.Group );
			m_mscLV.ListViewStatus( lvFilesCount, 6, m_sv.AllFB2InGroups );
		}
		
		// инициализация контролов и переменных
		private void Init() {
			ConnectListViewResultEventHandlers( false );
			for( int i=0; i!=lvFilesCount.Items.Count; ++i )
				lvFilesCount.Items[i].SubItems[1].Text	= "0";

			tsProgressBar.Value		= 0;
			tsslblProgress.Text		= Settings.Settings.GetReady();
			tsProgressBar.Visible	= false;
			// очистка временной папки
			filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
			m_sv.Clear(); // сброс данных класса для отображения прогресса
			lvResult.Items.Clear();
			lvResult.Groups.Clear();
			ConnectListViewResultEventHandlers( true );
			m_StopToSave = false;
			
			m_htWorkingBook.Clear();
			m_htBookTitleAuthors.Clear();
		}
		
		// очистка контролов вывода данных по книге по ее выбору
		private void ClearDataFields() {
			for( int i=0; i!=lvTitleInfo.Items.Count; ++i ) {
				lvTitleInfo.Items[i].SubItems[1].Text		= string.Empty;
				lvSourceTitleInfo.Items[i].SubItems[1].Text	= string.Empty;
			}
			for( int i=0; i!=lvDocumentInfo.Items.Count; ++i )
				lvDocumentInfo.Items[i].SubItems[1].Text = string.Empty;

			for( int i=0; i!=lvPublishInfo.Items.Count; ++i )
				lvPublishInfo.Items[i].SubItems[1].Text = string.Empty;

			lvCustomInfo.Items.Clear();
			rtbHistory.Clear();
			rtbAnnotation.Clear();
		}
		
		// чтение путей к данным поиска одинаковых fb2-файлов из xml-файла
		private void ReadFB2DupTempData() {
			string sSettings = Settings.Settings.WorksDataSettingsPath;
			if( !File.Exists( sSettings ) ) return;
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.IgnoreWhitespace = true;
			using ( XmlReader reader = XmlReader.Create( sSettings, settings ) ) {
				// Полная Сортировка
				reader.ReadToFollowing("FB2DupScanDir");
				if (reader.HasAttributes ) {
					tboxSourceDir.Text = reader.GetAttribute("tboxSourceDir");
					Settings.SettingsFB2Dup.DupScanDir = tboxSourceDir.Text.Trim();
				}
				reader.ReadToFollowing("FB2DupToDir");
				if (reader.HasAttributes ) {
					tboxDupToDir.Text = reader.GetAttribute("tboxDupToDir");
					Settings.SettingsFB2Dup.DupToDir = tboxDupToDir.Text.Trim();
				}
				reader.Close();
			}
		}

		// доступность контролов при Поиске (Возобновлении Поиска) одинаковых fb2-файлов
		private void setSearchFB2DupStartEnabled( bool bEnabled ) {
			lvResult.Enabled				= bEnabled;
			pSearchFBDup2Dirs.Enabled		= bEnabled;
			pMode.Enabled					= bEnabled;
			pExistFile.Enabled				= bEnabled;
			tsbtnSearchDubls.Enabled		= bEnabled;
			tsbtnSearchFb2DupRenew.Enabled	= bEnabled;
			tsbtnDupSaveList.Enabled		= bEnabled;
			tsbtnDupOpenList.Enabled		= bEnabled;
			tsbtnSearchFb2DupStop.Enabled		= !bEnabled;
			tsbtnSearchFb2DupStopSave.Enabled	= !bEnabled;
			tsProgressBar.Visible				= !bEnabled;
			ssProgress.Refresh();
		}
		
		// доступность контролов при Обработке файлов
		private void setFilesWorkerStartEnabled( bool bEnabled ) {
			lvResult.Enabled			= bEnabled;
			tsbtnSearchDubls.Enabled	= bEnabled;
			if( lvResult.Items.Count==0 ) {
				tsbtnDupCopy.Enabled	= !bEnabled;
				tsbtnDupMove.Enabled	= !bEnabled;
				tsbtnDupDelete.Enabled	= !bEnabled;
			} else {
				tsbtnDupCopy.Enabled	= bEnabled;
				tsbtnDupMove.Enabled	= bEnabled;
				tsbtnDupDelete.Enabled	= bEnabled;
			}
			pSearchFBDup2Dirs.Enabled	= bEnabled;
			pMode.Enabled				= bEnabled;
			pExistFile.Enabled			= bEnabled;
			tsbtnDupWorkStop.Enabled	= !bEnabled;
			tsProgressBar.Visible		= !bEnabled;
			ssProgress.Refresh();
		}
		
		// проверка на наличие папки сканирования копий книг
		private bool IsScanFolderDataCorrect( TextBox tbSource ) {
			// проверка на корректность данных папок источника
			string sSource	= filesWorker.WorkingDirPath( tbSource.Text.Trim() );
			tbSource.Text	= sSource;
			
			// проверки на корректность папок источника
			if( sSource.Length == 0 ) {
				MessageBox.Show( "Выберите папку для сканирования!", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			DirectoryInfo diFolder = new DirectoryInfo( sSource );
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка не найдена: " + sSource, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			return true;
		}
		
		private string GetFileLength( string sFilePath ) {
			FileInfo fi = new FileInfo( sFilePath );
			return filesWorker.FormatFileLength( fi.Length );
		}
		
		// время создания файла
		private  string GetFileCreationTime( string sFilePath ) {
			FileInfo fi = new FileInfo( sFilePath );
			return fi.CreationTime.ToString();
		}
		
		// время последней записи в файл
		private  string FileLastWriteTime( string sFilePath ) {
			FileInfo fi = new FileInfo( sFilePath );
			return fi.LastWriteTime.ToString();
		}
		
		// TODO Librusec
		private string IsValid( string sFilePath ) {
			FB2Validator fv2Validator = new FB2Validator();
			return fv2Validator.ValidatingFB22File( sFilePath ) == string.Empty ? "Да" : "Нет";
		}
		
		private string GetSubtring( string sP, string sStart, string sEnd ) {
			string s = string.Empty;
			int nStart = sP.IndexOf( sStart );
			int nEnd = -1;
			if( nStart!=-1 ) {
				nEnd = sP.IndexOf( sEnd );
				if( nEnd!=-1 )
					nEnd += sEnd.Length;

				s = sP.Substring( nStart, nEnd-nStart );
			}
			return s;
		}
		
		// извлечение информации из текста тэга <p>, убираем инлайн-теги для простоты
		private string GetDataFromTagP( string sP ) {
			sP = sP.Replace( "</p>","\r\n" ); sP = sP.Replace( "</P>","\r\n" );
			string s = GetSubtring( sP, "<p ", ">" );
			if( s.Length!=0 ) sP = sP.Replace( s, "" );
			s = GetSubtring( sP, "<P ", ">" );
			if( s.Length!=0 ) sP = sP.Replace( s, "" );
			string[] sIT = { "<strong>", "<STRONG>", "</strong>", "</STRONG>",
				"<emphasis>", "<EMPHASIS>", "</emphasis>", "</EMPHASIS>",
				"<sup>", "<SUP>", "</sup>", "</SUP>",
				"<sub>", "<SUB>", "</sub>", "</SUB>",
				"<code>", "<CODE>", "</code>", "</CODE>",
				"<strikethrough>", "<STRIKETHROUGH>", "</strikethrough>", "</STRIKETHROUGH>" };
			foreach( string sT in sIT ) {
				sP = sP.Replace( sT, "" );
			}
			return sP;
		}
		#endregion
		
		#endregion

		// =============================================================================================
		// 									ОБРАБОТЧИКИ событий контролов
		// =============================================================================================
		#region Обработчики событий контролов
		void TboxSourceDirTextChanged(object sender, EventArgs e)
		{
			Settings.SettingsFB2Dup.DupScanDir = tboxSourceDir.Text;
		}
		
		void TboxDupToDirTextChanged(object sender, EventArgs e)
		{
			Settings.SettingsFB2Dup.DupToDir = tboxDupToDir.Text;
		}
		
		void BtnOpenDirClick(object sender, EventArgs e)
		{
			filesWorker.OpenDirDlg( tboxSourceDir, fbdScanDir, "Укажите папку для сканирования с fb2 файлами:" );
		}
		
		void BtnToAnotherDirClick(object sender, EventArgs e)
		{
			filesWorker.OpenDirDlg( tboxDupToDir, fbdScanDir, "Укажите папку-приемник для размешения копий книг:" );
		}
		
		// Остановка выполнения процесса Поиска (Возобновления Поиска) одинаковых fb2-файлов
		void TsbtnSearchFb2DupStopClick(object sender, EventArgs e)
		{
			m_StopToSave = false;
			// TODO вынести в отдельную ф-ю вместе с  таким же кодом из TsbtnSearchFb2DupStopSaveClick
			if ( m_bw.IsBusy ) {
				if( m_bw.WorkerSupportsCancellation )
					m_bw.CancelAsync();
			} else {
				if( m_bwRenew.WorkerSupportsCancellation )
					m_bwRenew.CancelAsync();
			}
		}
		
		// остановка Поиска (Возобновления Поиска) и сохранение списка необработанных книг в файл
		void TsbtnSearchFb2DupStopSaveClick(object sender, EventArgs e)
		{
			m_StopToSave = true;
			if ( m_bw.IsBusy ) {
				if( m_bw.WorkerSupportsCancellation )
					m_bw.CancelAsync();
			} else {
				if( m_bwRenew.WorkerSupportsCancellation )
					m_bwRenew.CancelAsync();
			}
		}
		
		// Поиск одинаковых fb2-файлов
		void TsbtnSearchDublsClick(object sender, EventArgs e)
		{
			m_sMessTitle = "SharpFBTools - Поиск одинаковых fb2 файлов";
			// проверка на корректность данных папок источника и приемника файлов
			if( !IsScanFolderDataCorrect( tboxSourceDir ) )
				return;
			
			m_bScanSubDirs = chBoxScanSubDir.Checked ? true : false;
			m_sSource = tboxSourceDir.Text;
			// инициализация контролов
			Init();
			setSearchFB2DupStartEnabled( false );

			m_bCheckValid = chBoxIsValid.Checked;
			m_dtStart = DateTime.Now;
			tsslblProgress.Text = "Создание списка файлов:";
			
			// Запуск процесса DoWork от RunWorker
			if( m_bw.IsBusy != true )
				m_bw.RunWorkerAsync(); //если не занят то запустить процесс
		}
		
		// возобновить сравнение и поиск копий по данным из файла, созданного после прерывания обработки
		void TsbtnSearchFb2DupRenewClick(object sender, EventArgs e)
		{
			m_sMessTitle = "SharpFBTools - Поиск одинаковых fb2 файлов";
			// проверка на корректность данных папок источника и приемника файлов
			if( !IsScanFolderDataCorrect( tboxSourceDir ) )
				return;
			
			// инициализация контролов
			Init();
			setSearchFB2DupStartEnabled( false );
			tsslblProgress.Text = "Возобновление поиска копий fb2 книг:";
			m_dtStart = DateTime.Now;
			
			// Запуск процесса DoWork от RunWorker
			if( m_bwRenew.IsBusy != true )
				m_bwRenew.RunWorkerAsync(); //если не занят то запустить процесс
		}
		
		// занесение данных книги в контролы для просмотра
		void LvResultSelectedIndexChanged(object sender, EventArgs e)
		{
			#region Код
			ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
			// пропускаем ситуацию, когда курсор переходит от одной строки к другой - нет выбранного item'а
			if( si.Count > 0 ) {
				if( File.Exists( si[0].Text ) ) {
					FB2BookDescription	bd	= new FB2BookDescription( si[0].Text );
					// считываем данные TitleInfo
					m_mscLV.ListViewStatus( lvTitleInfo, 0, bd.TIBookTitle );
					m_mscLV.ListViewStatus( lvTitleInfo, 1, bd.TIGenres );
					m_mscLV.ListViewStatus( lvTitleInfo, 2, bd.TILang );
					m_mscLV.ListViewStatus( lvTitleInfo, 3, bd.TISrcLang );
					m_mscLV.ListViewStatus( lvTitleInfo, 4, bd.TIAuthors );
					m_mscLV.ListViewStatus( lvTitleInfo, 5, bd.TIDate );
					m_mscLV.ListViewStatus( lvTitleInfo, 6, bd.TIKeywords );
					m_mscLV.ListViewStatus( lvTitleInfo, 7, bd.TITranslators );
					m_mscLV.ListViewStatus( lvTitleInfo, 8, bd.TISequences );
					m_mscLV.ListViewStatus( lvTitleInfo, 9, bd.TICoverpagesCount.ToString() );
					// считываем данные SourceTitleInfo
					m_mscLV.ListViewStatus( lvSourceTitleInfo, 0, bd.STIBookTitle );
					m_mscLV.ListViewStatus( lvSourceTitleInfo, 1, bd.STIGenres );
					m_mscLV.ListViewStatus( lvSourceTitleInfo, 2, bd.STILang );
					m_mscLV.ListViewStatus( lvSourceTitleInfo, 3, bd.STISrcLang );
					m_mscLV.ListViewStatus( lvSourceTitleInfo, 4, bd.STIAuthors );
					m_mscLV.ListViewStatus( lvSourceTitleInfo, 5, bd.STIDate );
					m_mscLV.ListViewStatus( lvSourceTitleInfo, 6, bd.STIKeywords );
					m_mscLV.ListViewStatus( lvSourceTitleInfo, 7, bd.STITranslators );
					m_mscLV.ListViewStatus( lvSourceTitleInfo, 8, bd.STISequences );
					m_mscLV.ListViewStatus( lvSourceTitleInfo, 9, bd.STICoverpagesCount.ToString() );
					// считываем данные DocumentInfo
					m_mscLV.ListViewStatus( lvDocumentInfo, 0, bd.DIID );
					m_mscLV.ListViewStatus( lvDocumentInfo, 1, bd.DIVersion );
					m_mscLV.ListViewStatus( lvDocumentInfo, 2, bd.DIFB2Date );
					m_mscLV.ListViewStatus( lvDocumentInfo, 3, bd.DIProgramUsed );
					m_mscLV.ListViewStatus( lvDocumentInfo, 4, bd.DISrcOcr );
					m_mscLV.ListViewStatus( lvDocumentInfo, 5, bd.DISrcUrls );
					m_mscLV.ListViewStatus( lvDocumentInfo, 6, bd.DIFB2Authors );
					// считываем данные PublishInfo
					m_mscLV.ListViewStatus( lvPublishInfo, 0, bd.PIBookName );
					m_mscLV.ListViewStatus( lvPublishInfo, 1, bd.PIPublisher );
					m_mscLV.ListViewStatus( lvPublishInfo, 2, bd.PIYear );
					m_mscLV.ListViewStatus( lvPublishInfo, 3, bd.PICity );
					m_mscLV.ListViewStatus( lvPublishInfo, 4, bd.PIISBN );
					m_mscLV.ListViewStatus( lvPublishInfo, 5, bd.PISequences );
					// считываем данные CustomInfo
					lvCustomInfo.Items.Clear();
					IList<CustomInfo> lcu = bd.CICustomInfo;
					if( lcu != null ) {
						foreach( CustomInfo ci in lcu ) {
							ListViewItem lvi = new ListViewItem( ci.InfoType );
							lvi.SubItems.Add( ci.Value );
							lvCustomInfo.Items.Add( lvi );
						}
					}
					// считываем данные History
					rtbHistory.Clear(); rtbHistory.Text = GetDataFromTagP( bd.DIHistory );
					// считываем данные Annotation
					rtbAnnotation.Clear(); rtbAnnotation.Text = GetDataFromTagP( bd.TIAnnotation );
					// загрузка обложки
					if (bd.CoversBase64 != null) {
						picBoxCover.Image = Base64ToImage(bd.CoversBase64[0].base64String);
					} else {
						picBoxCover.Image = imageListDup.Images[0];
					}
				}
			}
			#endregion
		}
		
		// запуск сканирования по нажатию Enter на поле ввода папки для сканирования
		void TboxSourceDirKeyPress(object sender, KeyPressEventArgs e)
		{
			if ( e.KeyChar == (char)Keys.Return )
				TsbtnSearchDublsClick( sender, e );
		}
		
		// (раз)блокировка кнопок групповой обработки помеченных книг
		void LvResultItemChecked(object sender, ItemCheckedEventArgs e)
		{
			if( lvResult.CheckedItems.Count > 0 ) {
				tsbtnDupCopy.Enabled	= true;
				tsbtnDupMove.Enabled	= true;
				tsbtnDupDelete.Enabled	= true;
			} else {
				tsbtnDupCopy.Enabled	= false;
				tsbtnDupMove.Enabled	= false;
				tsbtnDupDelete.Enabled	= false;
			}
		}
		
		// копировать помеченные файлы в папку-приемник
		void TsbtnDupCopyClick(object sender, EventArgs e)
		{
			string sMessTitle	= "SharpFBTools - Копирование копий книг";
			string sTarget		= filesWorker.WorkingDirPath( tboxDupToDir.Text.Trim() );
			tboxDupToDir.Text	= sTarget;
			// проверки корректности путей к папкам
			if( sTarget.Length == 0 ) {
				MessageBox.Show( "Не указана папка-приемник файлов!\nРабота прекращена.",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			if( m_sSource == sTarget ) {
				MessageBox.Show( "Папка-приемник файлов совпадает с папкой сканирования!\nРабота прекращена.",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			// проверка папки-приемника и создание ее, если нужно
			if( !filesWorker.CreateDirIfNeed( sTarget, sMessTitle ) ) {
				return;
			}
			
			int nCount = lvResult.CheckedItems.Count;
			string sMess = "Вы действительно хотите скопировать "+nCount.ToString()+" отмеченные книги в папку \""+sTarget+"\"?";
			DialogResult result = MessageBox.Show( sMess, sMessTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question );
			if(result == DialogResult.No) {
				tsslblProgress.Text = Settings.Settings.GetReady();
				setFilesWorkerStartEnabled( true );
				return;
			}
			
			m_sFileWorkerMode = "Copy";
			// инициализация контролов
			tsProgressBar.Maximum 	= nCount;
			tsProgressBar.Value		= 0;
			setFilesWorkerStartEnabled( false );
			
			// Запуск процесса DoWork
			if( m_bwcmd.IsBusy != true ) {
				// если не занят то запустить процесс
				m_bwcmd.RunWorkerAsync();
			}
		}
		
		// переместить помеченные файлы в папку-приемник
		void TsbtnDupMoveClick(object sender, EventArgs e)
		{
			string sMessTitle = "SharpFBTools - Перемещение копий книг";
			string sTarget		= filesWorker.WorkingDirPath( tboxDupToDir.Text.Trim() );
			tboxDupToDir.Text	= sTarget;
			// проверки корректности путей к папкам
			if( sTarget.Length == 0 ) {
				MessageBox.Show( "Не указана папка-приемник файлов!\nРабота прекращена.",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			if( m_sSource == sTarget ) {
				MessageBox.Show( "Папка-приемник файлов совпадает с папкой сканирования!\nРабота прекращена.",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			// проверка папки-приемника и создание ее, если нужно
			if( !filesWorker.CreateDirIfNeed( sTarget, sMessTitle ) ) {
				return;
			}
			
			int nCount = lvResult.CheckedItems.Count;
			string sMess = "Вы действительно хотите переместить "+nCount.ToString()+" отмеченные книги в папку \""+sTarget+"\"?";
			MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			DialogResult result;
			result = MessageBox.Show( sMess, sMessTitle, buttons, MessageBoxIcon.Question );
			if(result == DialogResult.No) {
				tsslblProgress.Text = Settings.Settings.GetReady();
				setFilesWorkerStartEnabled( true );
				return;
			}
			
			m_sFileWorkerMode = "Move";
			// инициализация контролов
			tsProgressBar.Maximum 	= nCount;
			tsProgressBar.Value		= 0;
			setFilesWorkerStartEnabled( false );
			
			// Запуск процесса DoWork
			if( m_bwcmd.IsBusy != true )
				m_bwcmd.RunWorkerAsync(); // если не занят то запустить процесс
		}
		
		// удалить помеченные файлы
		void TsbtnDupDeleteClick(object sender, EventArgs e)
		{
			string sMessTitle = "SharpFBTools - Удаление копий книг";
			int nCount = lvResult.CheckedItems.Count;
			string sMess = "Вы действительно хотите удалить "+nCount.ToString()+" помеченные копии книг?";
			MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			DialogResult result;
			result = MessageBox.Show( sMess, sMessTitle, buttons, MessageBoxIcon.Question );
			if( result == DialogResult.No ) {
				return;
			}
			
			m_sFileWorkerMode = "Delete";
			
			// инициализация контролов
			tsProgressBar.Maximum 	= nCount;
			tsProgressBar.Value		= 0;
			setFilesWorkerStartEnabled( false );
			
			// Запуск процесса DoWork
			if( m_bwcmd.IsBusy != true )
				m_bwcmd.RunWorkerAsync(); // если не занят то запустить процесс
		}
		
		// Остановка выполнения процесса копирования(перемещения, удаления) fb2-файлов
		void TsbtnDupWorkStopClick(object sender, EventArgs e)
		{
			if( m_bwcmd.WorkerSupportsCancellation )
				m_bwcmd.CancelAsync();
		}
		
		// сохранение списка найденных копий
		void TsbtnDupSaveListClick(object sender, EventArgs e)
		{
			#region Код
			if( lvResult.Items.Count==0 ) {
				MessageBox.Show( "Нет ни одной копии fb2 книг!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			sfdList.Title = "Укажите название файла копий:";
			sfdList.Filter = "SharpFBTools Файлы копий книг (*.dup_lbc)|*.dup_lbc";
			sfdList.FileName = string.Empty;
			sfdList.InitialDirectory = Settings.Settings.GetProgDir();
			DialogResult result = sfdList.ShowDialog();
			if( result == DialogResult.OK ) {
				Environment.CurrentDirectory = Settings.Settings.GetProgDir();
				saveCopiesListToXml( sfdList.FileName, cboxMode.SelectedIndex, cboxMode.Text);
				MessageBox.Show( "Сохранение списка копий fb2 книг завершено!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
			
			#endregion
		}
		
		// загрузка списка копий
		void TsbtnDupOpenListClick(object sender, EventArgs e)
		{
			#region Код
			sfdLoadList.InitialDirectory = Settings.Settings.GetProgDir();
			sfdLoadList.Title 		= "Загрузка Списка копий книг:";
			sfdLoadList.Filter		= "SharpFBTools Файлы копий книг (*.dup_lbc)|*.dup_lbc";
			sfdLoadList.FileName	= string.Empty;
			string FromXML = string.Empty;
			DialogResult result = sfdLoadList.ShowDialog();
			if( result == DialogResult.OK ) {
				FromXML = sfdLoadList.FileName;
				// инициализация контролов
				Init();
				// установка режима поиска
				if( !File.Exists( FromXML ) ) {
					MessageBox.Show( "Не найден файл списка копий fb2 книг: \""+FromXML+"\"!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return;
				}
				XmlReaderSettings data = new XmlReaderSettings();
				data.IgnoreWhitespace = true;
				// отключаем обработчики событий для lvResult (убираем "тормоза")
				ConnectListViewResultEventHandlers( false );
				bool Result = false;
				try {
					lvResult.BeginUpdate();
					loadCopiesListFromXML( FromXML );
					Result = true;
					// Отобразим результат в индикаторе прогресса
					m_mscLV.ListViewStatus( lvFilesCount, 5, lvResult.Groups.Count );
					m_mscLV.ListViewStatus( lvFilesCount, 6, lvResult.Items.Count );
				} catch {
					Result = false;
				} finally {
					lvResult.EndUpdate();
					ConnectListViewResultEventHandlers( true );
				}
				
				if(Result)
					MessageBox.Show( "Список копий fb2 книг загружен.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				else
					MessageBox.Show( "Поврежден файл списка копий fb2 книг: \""+FromXML+"\"!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				
			}
			#endregion
		}
		#endregion
		
		// =============================================================================================
		// 								ОБРАБОТЧИКИ для контекстного меню
		// =============================================================================================
		#region Обработчики для контекстного меню
		void TsmiDeleteFileFromDiskClick(object sender, EventArgs e)
		{
			// удаление выделенного файла с диска
			#region Код
			if( lvResult.Items.Count > 0 && lvResult.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
				ListViewGroup	lvg			= si[0].Group;
				string			sFilePath	= si[0].SubItems[0].Text.Split('/')[0];
				string sTitle = "SharpFBTools - Удаление файла с диска";
				if( !File.Exists( sFilePath ) ) {
					if( MessageBox.Show( "Файл: \""+sFilePath+"\" не найден!\nУдалить путь к этому файлу из списка?",
					                    sTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes ) {
						lvResult.Items[ lvResult.SelectedItems[0].Index ].Remove();
					}
				} else {
					if( MessageBox.Show( "Вы действительно хотите удалить файл: \""+sFilePath+"\" с диска?", sTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes ) {
						File.Delete( sFilePath );
						lvResult.Items[ lvResult.SelectedItems[0].Index ].Remove();
					} else
						return;
				}
				
				// новое число групп и книг во всех группах
				// число групп
				if( lvg.Items.Count == 0 )
					lvResult.Groups.Remove( lvg );
				m_mscLV.ListViewStatus( lvFilesCount, 5, lvResult.Groups.Count.ToString() );
				// число книг во всех группах
				m_mscLV.ListViewStatus( lvFilesCount, 6, lvResult.Items.Count.ToString() );
			}
			#endregion
		}
		
		void TsmiOpenFileDirClick(object sender, EventArgs e)
		{
			// Открыть папку для выделенного файла
			#region Код
			if( lvResult.Items.Count > 0 && lvResult.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
				FileInfo fi = new FileInfo( si[0].SubItems[0].Text.Split('/')[0] );
				string sDir = fi.Directory.ToString();
				if( !Directory.Exists( sDir ) ) {
					MessageBox.Show( "Папка: \""+sDir+"\" не найдена!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				filesWorker.ShowAsyncDir( sDir );
			}
			#endregion
		}
		
		void TsmiViewInReaderClick(object sender, EventArgs e)
		{
			// запустить файл в fb2-читалке (Просмотр)
			#region Код
			// читаем путь к читалке из настроек
			string sFBReaderPath = Settings.Settings.ReadFBReaderPath();
			string sTitle = "SharpFBTools - Открытие папки для файла";
			if( !File.Exists( sFBReaderPath ) ) {
				MessageBox.Show( "Не могу найти Читалку \""+sFBReaderPath+"\"!\nПроверьте, правильно ли задан путь в Настройках.", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			if( lvResult.Items.Count > 0 && lvResult.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
				string sFilePath = si[0].SubItems[0].Text.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: \""+sFilePath+"\" не найден!", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				filesWorker.StartAsyncFile( sFBReaderPath, sFilePath );
			}
			#endregion
		}
		
		// редактировать выделенный файл в fb2-редакторе
		void TsmiEditInFB2EditorClick(object sender, EventArgs e)
		{
			#region Код
			// читаем путь к FBE из настроек
			string sFBEPath = Settings.Settings.ReadFBEPath();
			string sTitle = "SharpFBTools - Открытие файла в fb2-редакторе";
			if( !File.Exists( sFBEPath ) ) {
				MessageBox.Show( "Не могу найти fb2 редактор \""+sFBEPath+"\"!\nПроверьте, правильно ли задан путь в Настройках.", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			if( lvResult.Items.Count > 0 && lvResult.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
				string sFilePath = si[0].SubItems[0].Text.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: \""+sFilePath+"\" не найден!", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				filesWorker.StartAsyncFile( sFBEPath, sFilePath );
			}
			#endregion
		}
		
		// редактировать выделенный файл в текстовом редакторе
		void TsmiEditInTextEditorClick(object sender, EventArgs e)
		{
			#region Код
			// читаем путь к текстовому редактору из настроек
			string sTFB2Path = Settings.Settings.ReadTextFB2EPath();
			string sTitle = "SharpFBTools - Открытие файла в текстовом редакторе";
			if( !File.Exists( sTFB2Path ) ) {
				MessageBox.Show( "Не могу найти текстовый редактор \""+sTFB2Path+"\"!\nПроверьте, правильно ли задан путь в Настройках.", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			if( lvResult.Items.Count > 0 && lvResult.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
				string sFilePath = si[0].SubItems[0].Text.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: \""+sFilePath+"\" не найден!", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				filesWorker.StartAsyncFile( sTFB2Path, sFilePath );
			}
			#endregion
		}
		
		void TsmiFileReValidateClick(object sender, EventArgs e)
		{
			// Повторная Проверка выбранного fb2-файла или архива (Валидация)
			#region Код
			if( lvResult.Items.Count > 0 && lvResult.SelectedItems.Count != 0 ) {
				DateTime dtStart = DateTime.Now;
				string sTempDir = Settings.Settings.GetTempDir();
				ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
				string sSelectedItemText = si[0].SubItems[0].Text;
				string sFilePath = sSelectedItemText.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: \""+sFilePath+"\" не найден!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				MessageBoxIcon mbi = MessageBoxIcon.Information;
				string sMsg			= string.Empty;
				string sErrorMsg	= "СООБЩЕНИЕ ОБ ОШИБКЕ:";
				string sOkMsg		= "ОШИБОК НЕТ - ФАЙЛ ВАЛИДЕН";
				FB2Validator fv2V = new FB2Validator();
				// для несжатого fb2-файла
				sMsg = fv2V.ValidatingFB22File( sFilePath );
				if ( sMsg == string.Empty ) {
					// файл валидный
					mbi = MessageBoxIcon.Information;
					sErrorMsg = sOkMsg;
					si[0].SubItems[6].Text = "Да";
				} else {
					// файл не валидный
					mbi = MessageBoxIcon.Error;
					si[0].SubItems[6].Text = "Нет";
				}
				DateTime dtEnd = DateTime.Now;
				string sTime = dtEnd.Subtract( dtStart ).ToString() + " (час.:мин.:сек.)";
				MessageBox.Show( "Проверка выделенного файла на соответствие FictionBook.xsd схеме завершена.\nЗатрачено времени: "+sTime+"\n\nФайл: \""+sFilePath+"\"\n\n"+sErrorMsg+"\n"+sMsg, "SharpFBTools - "+sErrorMsg, MessageBoxButtons.OK, mbi );
			}
			#endregion
		}
		
		void TsmiDiffFB2Click(object sender, EventArgs e)
		{
			// diff - две помеченные fb2-книги
			#region Код
			ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
			if( lvResult.Items.Count > 0 && lvResult.SelectedItems.Count != 0 ) {
				// проверка на наличие diff-программы
				string sDiffTitle = "SharpFBTools - diff";
				string sDiffPath = Settings.Settings.ReadDiffPath();
				
				if( sDiffPath.Trim().Length==0 ) {
					MessageBox.Show( "В Настройках не указан путь к установленной diff-программе визуального сравнения файлов!\nУкажите путь к ней в Настройках.\nРабота остановлена!",
					                sDiffTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return;
				} else {
					if( !File.Exists( sDiffPath ) ) {
						MessageBox.Show( "Не найден файл diff-программы визуального сравнения файлов \""+sDiffPath+"\"!\nУкажите путь к ней в Настройках.\nРабота остановлена!",
						                sDiffTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
						return;
					}
				}
				// группа для выделенной книги
				ListViewGroup lvg = si[0].Group;
				// книги выбранной группы
				ListView.ListViewItemCollection glvic = lvg.Items;
				List<string> l = new List<string>();
				foreach( ListViewItem lvi in glvic ) {
					if( lvi.Checked )
						l.Add( lvi.Text );
					if( l.Count==2 )
						break;
				}
				// запускаем инструмент сравнения
				if( l.Count==2 ) {
					string sFilesNotExists = string.Empty;
					if( !File.Exists( l[0] ) )
						sFilesNotExists += l[0]; sFilesNotExists += "\n";

					if( !File.Exists( l[1] ) )
						sFilesNotExists += l[1]; sFilesNotExists += "\n";

					if( sFilesNotExists.Length!=0 )
						MessageBox.Show( "Не найден(ы) файл(ы) для сравнения:\n"+sFilesNotExists+"\nРабота остановлена!",
						                sDiffTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					else
						filesWorker.StartAsyncDiff( sDiffPath, l[0], l[1] );
				}
			}
			#endregion
		}
		
		void TsmiCheckedAllClick(object sender, EventArgs e)
		{
			// отметить все книги
			ConnectListViewResultEventHandlers( false );
			m_mscLV.CheckAllListViewItems( lvResult, true );
			ConnectListViewResultEventHandlers( true );
		}
		
		void TsmiUnCheckedAllClick(object sender, EventArgs e)
		{
			// снять отметки со всех книг
			ConnectListViewResultEventHandlers( false );
			m_mscLV.UnCheckAllListViewItems( lvResult.CheckedItems );
			ConnectListViewResultEventHandlers( true );
		}
		
		// пометить в каждой группе все "старые" книги (по тэгу version)
		void TsmiAllOldBooksForAllGroupsClick(object sender, EventArgs e)
		{
			ConnectListViewResultEventHandlers( false );
			CheckAllOldBooksInAllGroups(CompareMode.Version);
			ConnectListViewResultEventHandlers( true );
		}
		
		// пометить в каждой группе все "старые" книги (по времени создания файла)
		void TsmiAllOldBooksCreationTimeForAllGroupsClick(object sender, EventArgs e)
		{
			ConnectListViewResultEventHandlers( false );
			CheckAllOldBooksInAllGroups(CompareMode.CreationTime);
			ConnectListViewResultEventHandlers( true );
		}
		
		// пометить в каждой группе все "старые" книги (по времени последнего изменения файла)
		void TsmiAllOldBooksLastWriteTimeForAllGroupsClick(object sender, EventArgs e)
		{
			ConnectListViewResultEventHandlers( false );
			CheckAllOldBooksInAllGroups(CompareMode.LastWriteTime);
			ConnectListViewResultEventHandlers( true );
		}
		
		// пометить в выбранной группе все "старые" книги (по тэгу version)
		void TsmiAnalyzeInGroupClick(object sender, EventArgs e)
		{
			ConnectListViewResultEventHandlers( false );
			CheckAllOldBooksInGroup(CompareMode.Version);
			ConnectListViewResultEventHandlers( true );
		}
		
		// пометить в выбранной группе все "старые" книги (по времени создания файла)
		void TsmiAllOldBooksCreationTimeInGroupClick(object sender, EventArgs e)
		{
			ConnectListViewResultEventHandlers( false );
			CheckAllOldBooksInGroup(CompareMode.CreationTime);
			ConnectListViewResultEventHandlers( true );
		}
		
		// пометить в выбранной группе все "старые" книги (по времени последнего изменения файла)
		void TsmiAllOldBooksLastWriteTimeInGroupClick(object sender, EventArgs e)
		{
			ConnectListViewResultEventHandlers( false );
			CheckAllOldBooksInGroup(CompareMode.LastWriteTime);
			ConnectListViewResultEventHandlers( true );
		}
		#endregion

	}
}
