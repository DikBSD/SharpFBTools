/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 24.02.2014
 * Time: 8:27
 * 
 * License: GPL 2.1
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;
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

namespace Core.Duplicator
{
	/// <summary>
	/// Форма прогресса поиска копий fb2 книг
	/// </summary>
	public partial class ComrareForm : Form
	{
		#region Закрытые данные класса
		private string m_sMessTitle	= "SharpFBTools - Поиск одинаковых fb2 файлов";
		private string m_sSource	= string.Empty;
		private bool m_bScanSubDirs	= false;
		private bool m_bCheckValid	= false;
		private bool m_viewProgressStatus	= false;
		private string m_fromXmlPath		= null;		// null - полное сканирование; Путь - возобновление сравнения их xml
		private bool m_StopToSave			= false;	// true, если остановка с сохранением необработанного списка книг в файл.
		private StatusView	m_sv 			= new StatusView();
		private List<string> m_FilesList	= new List<string>();
		private bool m_FB2Librusec			= true; // список Жанров Либрусек
		
		private System.Windows.Forms.TextBox m_tboxSourceDir	= new System.Windows.Forms.TextBox();
		private System.Windows.Forms.CheckBox m_chBoxScanSubDir	= new System.Windows.Forms.CheckBox();
		private System.Windows.Forms.CheckBox m_chBoxIsValid	= new System.Windows.Forms.CheckBox();
		private System.Windows.Forms.ComboBox m_cboxMode		= new System.Windows.Forms.ComboBox();
		private System.Windows.Forms.ListView m_lvFilesCount	= new System.Windows.Forms.ListView();
		private System.Windows.Forms.ListView m_lvResult		= new System.Windows.Forms.ListView();
		private MiscListView m_mscLV							= new MiscListView(); // класс по работе с ListView
		
		private Hashtable m_htWorkingBook		= new Hashtable();  // таблица обработанные файлов - копии.
		private Hashtable m_htBookTitleAuthors	= new Hashtable();  // таблица для обработанных данных копий для режима группировки по Авторам.
		
		private BackgroundWorker m_bw		= null; // фоновый обработчик для Непрерывного сравнения
		private BackgroundWorker m_bwRenew	= null; // фоновый обработчик для Прерывания сравнения
		
		private DateTime m_dtStart;
		
		/// <summary>
		/// режимы сравнения книг
		/// </summary>
		private enum SearchCompareMode {
			Md5					= 0, // 0. Абсолютно одинаковые книги (md5)
			BookID				= 1, // 1. Одинаковый Id Книги (копии и/или разные версии правки одной и той же книги)
			BookTitle			= 2, // 2. Название Книги (могут быть найдены и разные книги разных Авторов, но с одинаковым Названием)
			AuthorAndTitle		= 3, // 3. Автор(ы) и Название Книги (одна и та же книга, сделанная разными людьми - разные Id, но Автор и Название - одинаковые)
		}
		#endregion
		
		public ComrareForm( string fromXmlPath, bool CheckValid, bool FB2Librusec,
		                   System.Windows.Forms.TextBox tboxSourceDir, System.Windows.Forms.CheckBox chBoxScanSubDir,
		                   System.Windows.Forms.CheckBox chBoxIsValid, System.Windows.Forms.ComboBox cboxMode,
		                   System.Windows.Forms.ListView lvFilesCount, System.Windows.Forms.ListView lvResult,
		                   bool viewProgressStatus )
		{
			InitializeComponent();
			m_tboxSourceDir		= tboxSourceDir;
			m_chBoxScanSubDir	= chBoxScanSubDir;
			m_chBoxIsValid		= chBoxIsValid;
			m_cboxMode			= cboxMode;
			m_lvFilesCount		= lvFilesCount;
			m_lvResult			= lvResult;
			m_bCheckValid		= CheckValid;
			m_FB2Librusec		= FB2Librusec;
			m_viewProgressStatus = viewProgressStatus;
			
			m_sSource		= m_tboxSourceDir.Text.Trim();
			m_bScanSubDirs	= m_chBoxScanSubDir.Checked;
			
			InitializeBackgroundWorker();
			InitializeRenewBackgroundWorker();
			
			m_dtStart = DateTime.Now;
			
			// Запуск процесса DoWork от RunWorker
			if( fromXmlPath == null ) {
				if( m_bw.IsBusy != true )
					m_bw.RunWorkerAsync(); //если не занят, то запустить процесс
			} else {
				m_fromXmlPath = fromXmlPath; // путь к xml файлу для возобновления поиска копий fb2 книг
				if( m_bwRenew.IsBusy != true )
					m_bwRenew.RunWorkerAsync(); //если не занят. то запустить процесс
			}
		}
		
		#region Открытые методы
		public bool IsStopToXmlClicked() {
			return m_StopToSave;
		}
		
		public string getSourceDirFromRenew() {
			return m_sSource;
		}
		#endregion
		
		#region Обработчики событий контролов
		void BtnStopClick(object sender, EventArgs e)
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
		
		void BtnSaveToXmlClick(object sender, EventArgs e)
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
			ControlPanel.Enabled = false;
			StatusTextBox.AppendText("Создание списка файлов для поиска копий fb2 книг...");
			StatusTextBox.AppendText(Environment.NewLine);
			List<string> lDirList = new List<string>();
			m_FilesList.Clear();
			if( !m_bScanSubDirs ) {
				// сканировать только указанную папку
				filesWorker.makeFilesListFromDir( m_sSource, ref m_FilesList, true );
				m_lvFilesCount.Items[0].SubItems[1].Text = "1";
			} else {
				// сканировать и все подпапки
				m_lvFilesCount.Items[0].SubItems[1].Text =
					filesWorker.recursionDirsSearch( m_sSource, ref lDirList, true ).ToString();
				m_sv.AllFiles = filesWorker.makeFilesListFromDirs( ref m_bw, ref e, ref lDirList, ref m_FilesList, true );
				m_lvFilesCount.Items[1].SubItems[1].Text = m_sv.AllFiles.ToString();
			}
			lDirList.Clear();
			m_bw.ReportProgress( 0 ); // отобразим данные в контролах
			
			ControlPanel.Enabled = true;

			if( ( m_bw.CancellationPending ) ) {
				e.Cancel = true;
				return;
			}
			
			// проверка, есть ли хоть один файл в папке для сканирования
			if( m_sv.AllFiles == 0 ) {
				MessageBox.Show( "В папке сканирования не найдено ни одного файла!\nРабота прекращена.", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			
			// очистка контролов вывода данных по книге по ее выбору
//			ClearDataFields();
			
			// Сравнение fb2-файлов
			m_htWorkingBook.Clear();
			m_htBookTitleAuthors.Clear();
			// Создание списка копий fb2-книг по Группам
			makeBookCopiesGroups( ref m_bw, ref e, (SearchCompareMode) m_cboxMode.SelectedIndex,
			                     ref m_FilesList, ref m_htWorkingBook, ref m_htBookTitleAuthors );
			m_lvResult.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
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
			ViewDupProgressData(); // отображение данных прогресса
			filesWorker.RemoveDir( Settings.Settings.GetTempDir() );

			string sTime = dtEnd.Subtract( m_dtStart ).ToString() + " (час.:мин.:сек.)";
			string sMessDone = "Поиск одинаковых fb2-файлов завершен!\nЗатрачено времени: "+sTime;
			string sMessError = string.Empty;
			string sMessCanceled = string.Empty;
			
			if (m_StopToSave) {
				// остановка поиска копий с сохранением списка необработанных книг в файл
				m_StopToSave = false;
				// сохранение в xml-файл списка данных о копиях и необработанных книг
				sfdList.Title = "Укажите файл для возобновления поиска копий книг:";
				sfdList.Filter = "SharpFBTools Файлы хода работы Дубликатора (*.dup_break)|*.dup_break";
				sMessCanceled = "Поиск одинаковых fb2 файлов прерван!\nДанные поиска и список оставшихся для обработки книг будут сохранены в xml-файл:\n"+sfdList.FileName+"\nЗатрачено времени: "+sTime;
				sfdList.FileName = string.Empty;
				sfdList.InitialDirectory = Settings.Settings.GetProgDir();
				DialogResult result = sfdList.ShowDialog();
				if( result == DialogResult.OK )
					saveSearchDataToXmlFile( sfdList.FileName, m_cboxMode.SelectedIndex, m_cboxMode.Text, ref m_FilesList);
			} else {
				// просто остановка поиска
				sMessCanceled = "Поиск одинаковых fb2-файлов остановлен!\nСписок Групп копий fb2-файлов не сформирован полностью!\nЗатрачено времени: "+sTime;
			}
			
			if ( m_lvResult.Items.Count == 0 )
				sMessDone += "\n\nНе найдено НИ ОДНОЙ копии книг!";
			
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
			this.Close();
			#endregion
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
					makeTreeOfBookCopies( ref bw, ref e, ref htWorkingBook );
					break;
				case SearchCompareMode.BookID:
					// 1. Одинаковый Id Книги (копии и/или разные версии правки одной и той же книги)
					// Хэширование fb2-файлов
					FilesHashForIDParser( ref bw, ref e, ref FilesList, ref htWorkingBook );
					// Создание списка копий для режима "1. Одинаковый Id Книги (копии и/или разные версии правки одной и той же книги)"
					makeTreeOfBookCopies( ref bw, ref e, ref htWorkingBook );
					break;
				case SearchCompareMode.BookTitle:
					// 2. Название Книги (могут быть найдены и разные книги разных Авторов, но с одинаковым Названием)
					// Хэширование fb2-файлов
					FilesHashForBTParser( ref bw, ref e, ref FilesList, ref htWorkingBook );
					// Создание списка копий для режима "2. Название Книги (могут быть найдены и разные книги разных Авторов, но с одинаковым Названием)"
					makeTreeOfBookCopies( ref bw, ref e, ref htWorkingBook );
					break;
				case SearchCompareMode.AuthorAndTitle:
					// 3. Автор(ы) и Название Книги (одна и та же книга, сделанная разными людьми - разные Id, но Автор и Название - одинаковые)
					// Хэширование fb2-файлов
					FilesHashForBTParser( ref bw, ref e, ref FilesList, ref htWorkingBook );
					// Хэширование по одинаковым Авторам в пределах сгенерированных групп книг по одинаковым названиям
					FilesHashForAuthorsParser( ref bw, ref e, ref htWorkingBook, ref htBookTitleAuthors );
					// Создание списка копий для режима "3. Автор(ы) и Название Книги (одна и та же книга, сделанная разными людьми - разные Id, но Автор и Название - одинаковые)"
					makeTreeOfBookCopies( ref bw, ref e, ref htBookTitleAuthors );
					break;
			}
		}
		
		// Создание дерева списка копий для всех режимов сравнения
		private void makeTreeOfBookCopies( ref BackgroundWorker bw, ref DoWorkEventArgs e, ref Hashtable ht ) {
			StatusTextBox.AppendText("Создание списка одинаковых fb2-файлов...");
			StatusTextBox.AppendText(Environment.NewLine);
			ProgressBar.Maximum	= ht.Values.Count;
			ProgressBar.Value	= 0;
			// сортировка ключей (групп)
			List<string> keyList = makeSortedKeysForGroups( ref ht );
			foreach( string key in keyList ) {
				if( ( bw.CancellationPending ) ) {
					e.Cancel = true;
					return;
				}
				++m_sv.Group; // число групп одинаковых книг
				// формирование представления Групп с их книгами
				makeBookCopiesView( (FB2FilesDataInGroup)ht[key] );
				bw.ReportProgress( 0 );
			}
		}
		
		
		// Хэширование по одинаковым Авторам в пределах сгенерированных групп книг по одинаковым названиям
		private void FilesHashForAuthorsParser( ref BackgroundWorker bw, ref DoWorkEventArgs e,
		                                       ref Hashtable htFB2ForABT, ref Hashtable htBookTitleAuthors ) {
			StatusTextBox.AppendText("Хеширование по Авторам...");
			StatusTextBox.AppendText(Environment.NewLine);
			ProgressBar.Maximum	= htFB2ForABT.Values.Count;
			ProgressBar.Value	= 0;
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
			StatusTextBox.AppendText("Хэширование fb2-файлов...");
			StatusTextBox.AppendText(Environment.NewLine);
			ProgressBar.Maximum	= FilesList.Count;
			ProgressBar.Value	= 0;
			
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
			// TODO В catch для всех парсеров добавить код сохранения списка проблемных файлов
		}
		
		// хеширование файлов в контексте Id книг:
		// 1. Одинаковый Id Книги (копии и/или разные версии правки одной и той же книги)
		// параметры: FilesList - список файлов для сканирования
		private void FilesHashForIDParser( ref BackgroundWorker bw, ref DoWorkEventArgs e, ref List<string> FilesList, ref Hashtable htFB2ForID ) {
			StatusTextBox.AppendText("Хэширование fb2-файлов...");
			StatusTextBox.AppendText(Environment.NewLine);
			ProgressBar.Maximum	= FilesList.Count;
			ProgressBar.Value	= 0;
			
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
			StatusTextBox.AppendText("Хэширование fb2-файлов...");
			StatusTextBox.AppendText(Environment.NewLine);
			ProgressBar.Maximum	= FilesList.Count;
			ProgressBar.Value	= 0;
			
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
			StatusTextBox.AppendText("Возобновление поиска копий fb2 книг из xml файла "+m_fromXmlPath+" :");
			StatusTextBox.AppendText(Environment.NewLine);
			StatusTextBox.AppendText("Загрузка данных поиска из xml файла...");
			StatusTextBox.AppendText(Environment.NewLine);
			XElement xmlTree = XElement.Load( m_fromXmlPath );
			// выставляем режим сравнения
			int CompareMode = Convert.ToInt16( xmlTree.Element("CompareMode").Attribute("index").Value );
			m_cboxMode.SelectedIndex = CompareMode;

			// устанавливаем данные настройки поиска-сравнения
			m_sSource = m_tboxSourceDir.Text = xmlTree.Element("SourceDir").Value;
			m_bScanSubDirs = m_chBoxScanSubDir.Checked = Convert.ToBoolean( xmlTree.Element("Settings").Element("ScanSubDirs").Value );
			m_bCheckValid = m_chBoxIsValid.Checked = Convert.ToBoolean( xmlTree.Element("Settings").Element("CheckValidate").Value );

			//загрузка данных о ходе сравнения
			XElement compareData = xmlTree.Element("CompareData");
			m_sv.AllFiles = Convert.ToInt32( compareData.Element("AllFiles").Value );
			m_sv.FB2 = Convert.ToInt32( compareData.Element("FB2Files").Value );
			m_lvFilesCount.Items[0].SubItems[1].Text = compareData.Element("AllDirs").Value;

			ViewDupProgressData();

			// заполнение списка необработанных файлов
			m_FilesList.Clear();
			IEnumerable<XElement> files = xmlTree.Element("NotWorkingFiles").Elements("File");
			foreach (XElement element in files)
				m_FilesList.Add(element.Value);

			// заполнение хэш-таблицы
			m_htWorkingBook.Clear();
			m_htBookTitleAuthors.Clear();

			// загрузка из xml-файла в хэш таблицу данных о копиях книг
			loadFromXMLToHashtable( ref m_bwRenew, (SearchCompareMode) CompareMode, ref xmlTree, ref m_htWorkingBook );
			// Создание списка копий fb2-книг по Группам
			makeBookCopiesGroups( ref m_bwRenew, ref e, (SearchCompareMode) CompareMode,
			                     ref m_FilesList, ref m_htWorkingBook, ref m_htBookTitleAuthors );
			m_lvResult.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
		}
		
		// загрузка из xml-файла в хэш таблицу данных о копиях книг для всех режимов
		private void loadFromXMLToHashtable( ref BackgroundWorker bw, SearchCompareMode CompareMode, ref XElement xmlTree, ref Hashtable ht ) {
			StatusTextBox.AppendText("Загрузка в хэш данных обработанных книг...");
			StatusTextBox.AppendText(Environment.NewLine);
			ProgressBar.Maximum	= Convert.ToUInt16( xmlTree.Element("Groups").Attribute("count").Value );
			if( CompareMode == SearchCompareMode.AuthorAndTitle )
				ProgressBar.Maximum += Convert.ToUInt16( xmlTree.Element("BookTitleGroup").Attribute("count").Value );
			ProgressBar.Value	= 0;
			
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
				                          new XElement("ScanSubDirs", m_chBoxScanSubDir.Checked),
				                          new XElement("CheckValidate", m_chBoxIsValid.Checked)),
				             new XComment("Режим поиска-сравнения"),
				             new XElement("CompareMode",
				                          new XAttribute("index", CompareMode),
				                          new XElement("Name", CompareModeName)),
				             new XComment("Данные о ходе сравнения"),
				             new XElement("CompareData",
				                          new XElement("AllDirs", m_lvFilesCount.Items[0].SubItems[1].Text),
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
		// 							ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ И АЛГОРИТМЫ КЛАССА
		// =============================================================================================
		#region Вспомогательные методы и алгоритмы класса

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
				m_lvResult.Groups.Add( (ListViewGroup)htBookGroups[fb2BookList.Group] );
				lvi.Group = (ListViewGroup)htBookGroups[fb2BookList.Group];
				m_lvResult.Items.Add( lvi );
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
		
		// Отображение результата поиска сравнения
		private void ViewDupProgressData() {
			m_mscLV.ListViewStatus( m_lvFilesCount, 1, m_sv.AllFiles );
			m_mscLV.ListViewStatus( m_lvFilesCount, 2, m_sv.FB2 );
			m_mscLV.ListViewStatus( m_lvFilesCount, 3, m_sv.Zip );
			m_mscLV.ListViewStatus( m_lvFilesCount, 4, m_sv.Other );
			m_mscLV.ListViewStatus( m_lvFilesCount, 5, m_sv.Group );
			m_mscLV.ListViewStatus( m_lvFilesCount, 6, m_sv.AllFB2InGroups );
		}
		
//		// очистка контролов вывода данных по книге по ее выбору
//		private void ClearDataFields() {
//			for( int i=0; i!=lvTitleInfo.Items.Count; ++i ) {
//				lvTitleInfo.Items[i].SubItems[1].Text		= string.Empty;
//				lvSourceTitleInfo.Items[i].SubItems[1].Text	= string.Empty;
//			}
//			for( int i=0; i!=lvDocumentInfo.Items.Count; ++i )
//				lvDocumentInfo.Items[i].SubItems[1].Text = string.Empty;
//
//			for( int i=0; i!=lvPublishInfo.Items.Count; ++i )
//				lvPublishInfo.Items[i].SubItems[1].Text = string.Empty;
//
//			lvCustomInfo.Items.Clear();
//			rtbHistory.Clear();
//			rtbAnnotation.Clear();
//		}
		
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
			string sResult = m_FB2Librusec ? fv2Validator.ValidatingFB2LibrusecFile( sFilePath ) : fv2Validator.ValidatingFB22File( sFilePath );
			return sResult == string.Empty ? "Да" : "Нет";
		}
		
		#endregion
	}
}
