/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 16.03.2009
 * Time: 10:03
 * 
 * License: GPL 2.1
 */

using System;
using System.IO;
using System.Xml;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.Collections;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Core.Misc;
using Core.FB2Dublicator;
using Core.FB2.Description;
using Core.FB2.Description.Common;
using Core.FB2.Description.TitleInfo;
using Core.FB2.Description.DocumentInfo;
using Core.FB2.Description.CustomInfo;

//using fB2Parser 		= Core.FB2.FB2Parsers.FB2Parser;
using fB2Parser 		= Core.FB2Dublicator.FB2ParserForDup;
using filesWorker		= Core.FilesWorker.FilesWorker;
using archivesWorker	= Core.FilesWorker.Archiver;
using FB2Validator		= Core.FB2Parser.FB2Validator;

namespace SharpFBTools.Tools
{
	/// <summary>
	/// Description of SFBTpFB2Dublicator.
	/// </summary>
	public partial class SFBTpFB2Dublicator : UserControl
	{
		#region Закрытые данные класса
		private StatusView	m_sv 			= null; 
		private DateTime	m_dtStart;
        private BackgroundWorker m_bw		= null;
        private string	m_sSource			= "";
        private bool	m_bScanSubDirs		= true;
        private string	m_sMessTitle		= "";
		private bool	m_bCheckValid		= false;	// проверять или нет fb2-файл на валидность
		#endregion
		
		public SFBTpFB2Dublicator()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			InitializeBackgroundWorker();
			
			Init();
			// читаем сохраненные пути к папкам Поиска одинаковых fb2-файлов, если они есть
			ReadFB2DupTempData();
			m_sv = new StatusView();
			cboxMode.SelectedIndex = 0; // Условия для Сравнения fb2-файлов: Автор(ы) и Название Книги
		}

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
			// поиск одинаковых fb2-файлов
			List<string> lDirList = new List<string>();
			if( !m_bScanSubDirs ) {
				// сканировать только указанную папку
				lDirList.Add( m_sSource );
				lvFilesCount.Items[0].SubItems[1].Text = "1";
			} else {
				// сканировать и все подпапки
				m_sv.AllFiles = filesWorker.DirsParser( m_sSource, lvFilesCount, ref lDirList, false );
			}

			// Проверить флаг на остановку процесса 
			if( ( m_bw.CancellationPending == true ) ) {
				e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
				return;
			}
			
			// проверка, есть ли хоть один файл в папке для сканирования
			if( m_sv.AllFiles == 0 ) {
				MessageBox.Show( "В папке сканирования не найдено ни одного файла!\nРабота прекращена.", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				tsslblProgress.Text = Settings.Settings.GetReady();
				SetSearchFB2DupStartEnabled( true );
				return;
			}
			
			tsslblProgress.Text		= "Хеширование fb2-файлов:";
			tsProgressBar.Maximum	= m_sv.AllFiles;
			tsProgressBar.Value		= 0;
			
			// Сравнение fb2-файлов
			lvResult.BeginUpdate();
			Hashtable htBookGroups = new Hashtable(); // хеш-таблица групп одинаковых книг
			ListViewGroup lvg = null; // группа одинаковых книг
           	if( cboxMode.SelectedIndex == 0 ) {
           		MakeColumns( 0 ); // изменение колонок просмотрщика найденного, взависимости от режима сравнения
           		Hashtable htFB2ForID = FilesHashForIDParser( m_bw, e, lDirList );
           		tsslblProgress.Text		= "Создание Списка одинаковых fb2-файлов:";
           		tsProgressBar.Maximum	= htFB2ForID.Count+1;
				tsProgressBar.Value		= 0;
           		foreach( FB2FilesData fb2f in htFB2ForID.Values ) {
           			// Проверить флаг на остановку процесса 
					if( ( m_bw.CancellationPending == true ) ) {
						e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
						return;
					}
	           		if( fb2f.Count > 1 ) {
     	      			++m_sv.Group; // число групп одинаковых книг
     	      			IList<string> ilPath = fb2f.GetPathList();
        	   			for( int i=0; i!=fb2f.Count; ++i ) {
           					++m_sv.AllFB2InGroups; // число книг во всех группах одинаковых книг
           					lvg = new ListViewGroup( fb2f.Id );
           					ListViewItem lvi = new ListViewItem( ilPath[i] );
           					lvi.SubItems.Add( MakeBookTitleString( fb2f.GetBookData( ilPath[i] ).BookTitle ) );
							lvi.SubItems.Add( MakeAutorsString( fb2f.GetBookData( ilPath[i] ).Authors ) );
							lvi.SubItems.Add( MakeGenresString( fb2f.GetBookData( ilPath[i] ).Genres ) );
							lvi.SubItems.Add( fb2f.GetBookData( ilPath[i] ).Version );
							lvi.SubItems.Add( m_bCheckValid ? IsValid( ilPath[i] ) : "?" );
							lvi.SubItems.Add( GetFileLength( ilPath[i] ) );
							lvi.SubItems.Add( GetFileCreationTime( ilPath[i] ) );
							lvi.SubItems.Add( FileLastWriteTime( ilPath[i] ) );
							// заносим группу в хеш, если она там отсутствует
							AddBookGroupInHashTable( htBookGroups, lvg );
							// присваиваем группу книге
							lvResult.Groups.Add( (ListViewGroup)htBookGroups[fb2f.Id] );
							lvi.Group = (ListViewGroup)htBookGroups[fb2f.Id];
							lvResult.Items.Add( lvi );
    	       			}
        	   		}
           			m_bw.ReportProgress( 0 );
           		}
           	} else {
           		MakeColumns( 1 ); // изменение колонок просмотрщика найденного, взависимости от режима сравнения
           		Hashtable htFB2ForABT = FilesHashForABTParser( m_bw, e, lDirList );
           		tsslblProgress.Text		= "Создание Списка одинаковых fb2-файлов:";
           		tsProgressBar.Maximum	= htFB2ForABT.Count+1;
				tsProgressBar.Value		= 0;
           		foreach( FB2FilesData fb2f in htFB2ForABT.Values ) {
           			// Проверить флаг на остановку процесса 
					if( ( m_bw.CancellationPending == true ) ) {
						e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
						return;
					}
           			if( fb2f.Count > 1 ) {
           				++m_sv.Group; // число групп одинаковых книг
	           			IList<string> ilPath = fb2f.GetPathList();
           				for( int i=0; i!=fb2f.Count; ++i ) {
           					++m_sv.AllFB2InGroups; // число книг во всех группах одинаковых книг
							lvg = new ListViewGroup( fb2f.BookTitleForKey );
							ListViewItem lvi = new ListViewItem( ilPath[i] );
							lvi.SubItems.Add( MakeAutorsString( fb2f.GetBookData( ilPath[i] ).Authors ) );
							lvi.SubItems.Add( MakeGenresString( fb2f.GetBookData( ilPath[i] ).Genres ) );
							lvi.SubItems.Add( fb2f.GetBookData( ilPath[i] ).Id );
							lvi.SubItems.Add( fb2f.GetBookData( ilPath[i] ).Version );
							lvi.SubItems.Add( m_bCheckValid ? IsValid( ilPath[i] ) : "?" );
							lvi.SubItems.Add( GetFileLength( ilPath[i] ) );
							lvi.SubItems.Add( GetFileCreationTime( ilPath[i] ) );
							lvi.SubItems.Add( FileLastWriteTime( ilPath[i] ) );
							// заносим группу в хеш, если она там отсутствует
							AddBookGroupInHashTable( htBookGroups, lvg );
							// присваиваем группу книге
							lvResult.Groups.Add( (ListViewGroup)htBookGroups[fb2f.BookTitleForKey] );
							lvi.Group = (ListViewGroup)htBookGroups[fb2f.BookTitleForKey];
							lvResult.Items.Add( lvi );
           				}
           			}
           			m_bw.ReportProgress( 0 );
           		}
           	}
			lDirList.Clear();
		}
		
		private void bw_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
            // Отобразим результат
			Misc msc = new Misc();
			msc.ListViewStatus( lvFilesCount, 1, m_sv.AllFiles );
			msc.ListViewStatus( lvFilesCount, 2, m_sv.FB2 );
			msc.ListViewStatus( lvFilesCount, 3, m_sv.Archive );
			msc.ListViewStatus( lvFilesCount, 4, m_sv.Other );
			msc.ListViewStatus( lvFilesCount, 5, m_sv.Group );
			msc.ListViewStatus( lvFilesCount, 6, m_sv.AllFB2InGroups );
			++tsProgressBar.Value;
        }
		
		 private void bw_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {   
            // Проверяем - это отмена, ошибка, или конец задачи и сообщить
            lvResult.EndUpdate();
            DateTime dtEnd = DateTime.Now;
            m_sv.Clear();
            filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
            
            string sTime = dtEnd.Subtract( m_dtStart ).ToString() + " (час.:мин.:сек.)";
			string sMessCanceled	= "Поиск одинаковых fb2-файлов остановлен!\nЗатрачено времени: "+sTime;
			string sMessError		= "";
			string sMessDone		= "Поиск одинаковых fb2-файлов завершен!\nЗатрачено времени: "+sTime;
           
			if( ( e.Cancelled == true ) ) {
                MessageBox.Show( sMessCanceled, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
            } else if( e.Error != null ) {
                sMessError = "Error!\n" + e.Error.Message + "\n" + e.Error.StackTrace + "\nЗатрачено времени: "+sTime;
            	MessageBox.Show( sMessError, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
            } else {
            	MessageBox.Show( sMessDone, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
            }
			
			tsslblProgress.Text = Settings.Settings.GetReady();

			SetSearchFB2DupStartEnabled( true );
        }
		#endregion
		
		#region Закрытые вспомогательные методы класса
		// увеличение значения 2-й колонки ListView на 1
		private void Init() {
			// инициализация контролов и переменных
			lvResult.Items.Clear();
			for( int i=0; i!=lvFilesCount.Items.Count; ++i ) {
				lvFilesCount.Items[i].SubItems[1].Text	= "0";
			}
			tsProgressBar.Value		= 0;
			tsslblProgress.Text		= Settings.Settings.GetReady();
			tsProgressBar.Visible	= false;
			// очистка временной папки
			filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
			if( m_sv!=null ) m_sv.Clear(); // сброс данных класса для отображения прогресса
			lvResult.Items.Clear();
			lvResult.Groups.Clear();
		}
		
		private void ReadFB2DupTempData() {
			// чтение путей к данным поиска одинаковых fb2-файлов из xml-файла
			string sSettings = Settings.Settings.WorksDataSettingsPath;
			if( !File.Exists( sSettings ) ) return;
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.IgnoreWhitespace = true;
			using ( XmlReader reader = XmlReader.Create( sSettings, settings ) ) {
				// Полная Сортировка
				reader.ReadToFollowing("FB2DupScanDir");
				if (reader.HasAttributes ) {
					tboxSourceDir.Text = reader.GetAttribute("tboxSourceDir");
					Settings.SettingsFB2Dup.FMDataScanDir = tboxSourceDir.Text.Trim();
				}
				reader.Close();
			}
		}
		
		private void SetSearchFB2DupStartEnabled( bool bEnabled ) {
			// доступность контролов при Поиске одинаковых fb2-файлов
			pSearchFBDup2Dirs.Enabled	= bEnabled;
			pMode.Enabled				= bEnabled;
			tsbtnOpenDir.Enabled		= bEnabled;
			tsbtnSearchDubls.Enabled	= bEnabled;
			tsbtnSearchFb2DupStop.Enabled	= !bEnabled;
			tsProgressBar.Visible			= !bEnabled;
			ssProgress.Refresh();
		}
		
		private bool IsScanFolderDataCorrect( TextBox tbSource ) {
			// проверка на корректность данных папок источника
			string sSource = tbSource.Text.Trim();
			Regex rx = new Regex( @"\\+$" );
			sSource = rx.Replace( sSource, "" );
			tbSource.Text = sSource;
			
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
		
/*		private bool IsArchivatorsExist() {
			// проверка на наличие архиваторов
			string s7zPath	= Settings.Settings.Read7zaPath();
			string sRarPath	= Settings.Settings.ReadRarPath();
			
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
			
			if( sRarPath.Trim().Length==0 ) {
				MessageBox.Show( "В Настройках не указана папка с установленным консольным rar-архиватором!\nУкажите путь к нему в Настройках.\nРабота остановлена!",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			} else {
				if( !File.Exists( sRarPath ) ) {
					MessageBox.Show( "Не найден файл консольного rar-архиватора \""+sRarPath+"\"!\nУкажите путь к нему в Настройках.\nРабота остановлена!",
					                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return false;
				}
			}
			return true;
		}*/
	
		// создание хеш-таблицы для групп одинаковых книг
		private bool AddBookGroupInHashTable( Hashtable groups, ListViewGroup lvg ) {
			if( groups == null ) return false;
			if( !groups.Contains( lvg.Header ) ) {
				groups.Add( lvg.Header, lvg );
				return true;
			}
			return false;
		}
		
		// хеширование файлов в контексте Id книг
		// параметры: lsDirs - список папок для сканирования
		private Hashtable FilesHashForIDParser( BackgroundWorker bw, DoWorkEventArgs e, List<string> lsDirs ) {
			// список всех файлов - по cписку папок - замена рекурсии
			Hashtable htFB2ForID = new Hashtable();
			foreach( string s in lsDirs ) {
				DirectoryInfo diFolder = new DirectoryInfo( s );
				foreach( FileInfo fiNextFile in diFolder.GetFiles() ) {
					if( ( m_bw.CancellationPending == true ) )  {
						e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
						return htFB2ForID;
					}
					
					string sPath	= s + "\\" + fiNextFile.Name;
					string sExt		= Path.GetExtension( sPath ).ToLower();
					if( sExt==".fb2" ) {
						++m_sv.FB2;
						// заполнение хеш таблицы данными о fb2-книгах в контексте их ID
						MakeFB2IDHashTable( sPath, ref htFB2ForID );
					}  else {
						// это архив?
						if( archivesWorker.IsArchive( sExt ) ) {
							++m_sv.Archive;	// пропускаем архивы
						}  else {
							++m_sv.Other;	// пропускаем не fb2-файлы
						}
					}
					bw.ReportProgress( 0 ); // отобразим данные в контролах
				}
			}
			return htFB2ForID;
		}
		
		// заполнение хеш таблицы данными о fb2-книгах в контексте их ID
		// параметры: sPath - путь к fb2-файлу; htFB2ForID - хеш-таблица
		private void MakeFB2IDHashTable( string sPath, ref Hashtable htFB2ForID ) {
			try {
				fB2Parser fb2 = new fB2Parser( sPath );
				string sID = fb2.Id;
				if( sID==null ) return;

				if( sID.Length==0 ) sID = "Тег <id> в этих книгах \"пустой\"";
				string sVersion = fb2.Version;;
				BookTitle 		bookTitle	= fb2.BookTitle;
				IList<Author>	authors		= fb2.Authors;
				
				// данные о книге
				BookData fb2BookData = new BookData( bookTitle, authors, fb2.Genres, sID, sVersion, sPath );
				
				if( !htFB2ForID.ContainsKey( sID ) ) {
					// такой книги в числе дублей еще нет
					FB2FilesData fb2f = new FB2FilesData();
					fb2f.Id = sID;
					fb2f.AddBookData( fb2BookData );
					fb2f.AddPath( sPath );
					htFB2ForID.Add( sID, fb2f );
				} else {
					// такая книга в числе дублей уже есть
					FB2FilesData fb2f = (FB2FilesData)htFB2ForID[sID];
					fb2f.AddBookData( fb2BookData );
					fb2f.AddPath( sPath );
					htFB2ForID[sID] = fb2f;
				}
			} catch {} // пропускаем проблемные файлы
		}
		
		// хеширование файлов в контексте Авторов и Названия книг
		// параметры: lsDirs - список папок для сканирования
		private Hashtable FilesHashForABTParser( BackgroundWorker bw, DoWorkEventArgs e, List<string> lsDirs ) {
			// список всех файлов - по cписку папок - замена рекурсии
			Hashtable htFB2ForABT = new Hashtable();
			foreach( string s in lsDirs ) {
				DirectoryInfo diFolder = new DirectoryInfo( s );
				foreach( FileInfo fiNextFile in diFolder.GetFiles() ) {
					if( ( m_bw.CancellationPending == true ) )  {
						e.Cancel = true; // Выставить окончание - по отмене, сработает событие bw_RunWorkerCompleted
						return htFB2ForABT;
					}
					string sPath	= s + "\\" + fiNextFile.Name;
					string sExt		= Path.GetExtension( sPath ).ToLower();
					if( sExt==".fb2" ) {
						++m_sv.FB2;
						// заполнение хеш таблицы данными о fb2-книгах в контексте их Авторов и Названия
						MakeFB2ABTHashTable( sPath, ref htFB2ForABT );
					}  else {
						// это архив?
						if( archivesWorker.IsArchive( sExt ) ) {
							++m_sv.Archive;	// пропускаем архивы
						}  else {
							++m_sv.Other;	// пропускаем не fb2-файлы
						}
					}
					bw.ReportProgress( 0 ); // отобразим данные в контролах
				}
			}
			return htFB2ForABT;
		}
		
		// заполнение хеш таблицы данными о fb2-книгах в контексте их Авторов и Названия
		// параметры: sPath - путь к fb2-файлу; htFB2ForABT - хеш-таблица
		private void MakeFB2ABTHashTable( string sPath, ref Hashtable htFB2ForABT ) {
			try {
				fB2Parser fb2 = new fB2Parser( sPath );
				string sId		= fb2.Id;
				string sVersion	= fb2.Version;

				BookTitle bookTitle		= fb2.BookTitle;
				IList<Author> authors	= fb2.Authors;
				// ключ
				BookDataABTKey htKey = new BookDataABTKey( authors, bookTitle );
				// данные о книге
				BookData fb2BookData = new BookData( bookTitle, authors, fb2.Genres, sId, sVersion, sPath );
				// ищем в хеше дубли
				DictionaryEntry deDup = IsBookEqualityInHash( ref htFB2ForABT, authors, bookTitle );
				if( deDup.Key==null ) {
					// такой книги еще нет в хэше
					// заносим только те книги, где тэг названия - есть
					if( bookTitle!=null && bookTitle.Value!=null ) {
						FB2FilesData fb2f = new FB2FilesData();
						fb2f.BookTitleForKey = bookTitle.Value;
						fb2f.AddBookData( fb2BookData );
						fb2f.AddPath( sPath );
						htFB2ForABT.Add( htKey, fb2f );
					}
				} else {
					// такая книга уже есть
					// обработка данных value хеша
					BookDataABTKey aFromHash = (BookDataABTKey)deDup.Key;
					// вытаскивает value их хэша по key
					FB2FilesData fb2f = (FB2FilesData)htFB2ForABT[deDup.Key];
					fb2f.AddBookData( fb2BookData );
					// заменяем Название книги в хеше на самое длинное
					bool bKeyNewBT = false; bool bKeyNewA = false;
					if( bookTitle!=null && bookTitle.Value!=null ) {
						if( bookTitle.Value.Length > aFromHash.BookTitle.Value.Length ) {
							// заменяем Название книги в данных хеша на самое длинное
							fb2f.BookTitleForKey = bookTitle.Value;
							// заменяем key в хеше на тот, где название - длиннее
							htKey.BookTitle = bookTitle; bKeyNewBT = true;
						}
					}
					fb2f.AddPath( sPath );
					// обработка key хеша
					if( authors.Count > aFromHash.Authors.Count ) bKeyNewA = true;
					// заменяем key в хеше на тот, где больше число авторов, и название - длиннее
					if( bKeyNewA || bKeyNewBT ) {
						htFB2ForABT.Remove( deDup.Key );
						htFB2ForABT.Add( htKey, fb2f );
					} else {
						htFB2ForABT[deDup.Key] = fb2f;
					}
				}
			} catch {} // пропускаем проблемные файлы
		}
		
		// ищем в хеше дубли
		// возвращаем: путой DictionaryEntry, если не нашли, или найденный ключ
		private DictionaryEntry IsBookEqualityInHash( ref Hashtable htFB2ForABT, IList<Author> Authors, BookTitle bookTitle ) {
			foreach ( DictionaryEntry de in htFB2ForABT ) {
           		BookDataABTKey abtkFromHash = (BookDataABTKey)de.Key;
           		FB2ABTComparer fb2c = new FB2ABTComparer( bookTitle, abtkFromHash.BookTitle, Authors, abtkFromHash.Authors );
           		if( fb2c.IsBookTitleEquality() && fb2c.IsBookAuthorEquality() ) return de;
           	}
			DictionaryEntry d;
			return d;
		}
		
		// формирование строки с Названием Книги
		private string MakeBookTitleString( BookTitle bookTitle ) {
			if( bookTitle==null ) return "?";
			return bookTitle.Value!=null ? bookTitle.Value : "?";
		}
		
		// формирование строки с Авторами Книги из списка всех Авторов ЭТОЙ Книги
		private string MakeAutorsString( IList<Author> Authors ) {
			if( Authors==null ) return "Тег <authors> в книге отсутствует";
			string sA = ""; int n = 0;
			foreach( Author a in Authors ) {
				sA += Convert.ToString(++n)+": ";
				if( a.LastName!=null && a.LastName.Value!=null )
					sA += a.LastName.Value+" ";
				if( a.FirstName!=null && a.FirstName.Value!=null )
					sA += a.FirstName.Value+" ";
				if( a.MiddleName!=null && a.FirstName.Value!=null )
					sA += a.MiddleName.Value+" ";
				if( a.NickName!=null && a.NickName.Value!=null )
					sA += a.NickName.Value;
				sA += "; ";
			}
			return sA;
		}
		
		// формирование строки с Жанрами Книги из списка всех Жанров ЭТОЙ Книги
		private string MakeGenresString( IList<Genre> Genres ) {
			if( Genres==null ) return "?";
			string sG = ""; int n = 0;
			foreach( Genre g in Genres ) {
				sG += Convert.ToString(++n)+": ";
				if( g.Name!=null ) sG += g.Name;
				sG += "; ";
			}
			return sG;
		}
		
		// изменение колонок просмотрщика найденного, взависимости от режима сравнения
		private void MakeColumns( int nMode ) {
			lvResult.Columns.Clear();
			if( nMode == 0 ) {
				// по Id Книги
				lvResult.Columns.Add( "Одинаковые ID (Путь к Книге)", 300 );
				lvResult.Columns.Add( "Название Книги", 180 );
				lvResult.Columns.Add( "Автор(ы) Книги", 180 );
				lvResult.Columns.Add( "Жанр(ы) Книги", 180 );
			} else {
				// по Автору(ам) и Названию Книги
				lvResult.Columns.Add( "Книга (Путь к Книге)", 300 );
				lvResult.Columns.Add( "Автор(ы) Книги", 180 );
				lvResult.Columns.Add( "Жанр(ы) Книги", 180 );
				lvResult.Columns.Add( "ID Книги", 200 );
			}
			lvResult.Columns.Add( "Версия", 50 );
			lvResult.Columns.Add( "Валидность", 50, HorizontalAlignment.Center );
			lvResult.Columns.Add( "Размер", 90, HorizontalAlignment.Center );
			
			lvResult.Columns.Add( "Дата создания", 120 );
			lvResult.Columns.Add( "Последнее изменение", 120 );
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
		
		private string IsValid( string sFilePath ) {
			FB2Validator fv2Validator = new FB2Validator();
			return fv2Validator.ValidatingFB2File( sFilePath ) == "" ? "Да" : "Нет";
        }
		#endregion
		
		#region Обработчики событий
		void TboxSourceDirTextChanged(object sender, EventArgs e)
		{
			Settings.SettingsFB2Dup.FMDataScanDir = tboxSourceDir.Text;
		}
		
		void TsbtnOpenDirClick(object sender, EventArgs e)
		{
			// задание папки с fb2-файлами и архивами для сканирования
			filesWorker.OpenDirDlg( tboxSourceDir, fbdScanDir, "Укажите папку для сканирования с fb2-файлами и архивами:" );
		}
		
		void TsbtnFullSortStopClick(object sender, EventArgs e)
		{
			// Остановка выполнения процесса Поиска одинаковых fb2-файлов
			if( m_bw.WorkerSupportsCancellation == true ) {
				m_bw.CancelAsync();
			}
		}
		
		void TsbtnSearchDublsClick(object sender, EventArgs e)
		{
			// Поиск одинаковых fb2-файлов
			if( chBoxScanSubDir.Checked ) {
				m_bScanSubDirs = true;
			} else {
				m_bScanSubDirs = false;
			}
			m_sSource = tboxSourceDir.Text;
			m_sMessTitle = "SharpFBTools - Поиск одинаковых fb2-файлов";
			// проверка на корректность данных папок источника и приемника файлов
			if( !IsScanFolderDataCorrect( tboxSourceDir ) ) {
				return;
			}
			// проверка на наличие архиваторов
/*			if( !IsArchivatorsExist() ) {
				return;
			}*/
			
			// инициализация контролов
			Init();
			SetSearchFB2DupStartEnabled( false );

			m_bCheckValid = chBoxIsValid.Checked;
			m_dtStart = DateTime.Now;
			tsslblProgress.Text = "Создание списка файлов:";
			
			// Запуск процесса DoWork от Бекграунд Воркера
			if( m_bw.IsBusy != true ) {
				//если не занят то запустить процесс
            	m_bw.RunWorkerAsync();
			}		
		}
		
		void LvResultSelectedIndexChanged(object sender, EventArgs e)
		{
			// занесение данных книги в контролы для просмотра
			#region Код
			ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
			if( si.Count > 0 ) {
				// пропускаем ситуацию, когда курсор переходит от одной строки к другой - нет выбранного item'а
				Misc				msc	= new Misc();
				FB2BookDataForDup	bd	= new FB2BookDataForDup( si[0].Text );
				// считываем данные TitleInfo
				msc.ListViewStatus( lvTitleInfo, 0, bd.TIBookTitle );
				msc.ListViewStatus( lvTitleInfo, 1, bd.TIGenres );
				msc.ListViewStatus( lvTitleInfo, 2, bd.TILang );
				msc.ListViewStatus( lvTitleInfo, 3, bd.TISrcLang );
				msc.ListViewStatus( lvTitleInfo, 4, bd.TIAuthors );
				msc.ListViewStatus( lvTitleInfo, 5, bd.TIDate );
				msc.ListViewStatus( lvTitleInfo, 6, bd.TIKeywords );
				msc.ListViewStatus( lvTitleInfo, 7, bd.TICoverpage );
				msc.ListViewStatus( lvTitleInfo, 8, bd.TITranslators );
				msc.ListViewStatus( lvTitleInfo, 9, bd.TISequences );
				// считываем данные SourceTitleInfo
				msc.ListViewStatus( lvSourceTitleInfo, 0, bd.STIBookTitle );
				msc.ListViewStatus( lvSourceTitleInfo, 1, bd.STIGenres );
				msc.ListViewStatus( lvSourceTitleInfo, 2, bd.STILang );
				msc.ListViewStatus( lvSourceTitleInfo, 3, bd.STISrcLang );
				msc.ListViewStatus( lvSourceTitleInfo, 4, bd.STIAuthors );
				msc.ListViewStatus( lvSourceTitleInfo, 5, bd.STIDate );
				msc.ListViewStatus( lvSourceTitleInfo, 6, bd.STIKeywords );
				msc.ListViewStatus( lvSourceTitleInfo, 7, bd.STICoverpage );
				msc.ListViewStatus( lvSourceTitleInfo, 8, bd.STITranslators );
				msc.ListViewStatus( lvSourceTitleInfo, 9, bd.STISequences );
				// считываем данные DocumentInfo
				msc.ListViewStatus( lvDocumentInfo, 0, bd.DIID );
				msc.ListViewStatus( lvDocumentInfo, 1, bd.DIVersion );
				msc.ListViewStatus( lvDocumentInfo, 2, bd.DIFB2Date );
				msc.ListViewStatus( lvDocumentInfo, 3, bd.DIProgramUsed );
				msc.ListViewStatus( lvDocumentInfo, 4, bd.DISrcOcr );
				msc.ListViewStatus( lvDocumentInfo, 5, bd.DISrcUrls );
				msc.ListViewStatus( lvDocumentInfo, 6, bd.DIFB2Authors );
				// считываем данные PublishInfo
				msc.ListViewStatus( lvPublishInfo, 0, bd.PIBookName );
				msc.ListViewStatus( lvPublishInfo, 1, bd.PIPublisher );
				msc.ListViewStatus( lvPublishInfo, 2, bd.PIYear );
				msc.ListViewStatus( lvPublishInfo, 3, bd.PICity );
				msc.ListViewStatus( lvPublishInfo, 4, bd.PIISBN );
				msc.ListViewStatus( lvPublishInfo, 5, bd.PISequences );
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
				rtbHistory.Clear(); rtbHistory.Text = bd.DIHistory;
				// считываем данные Annotation
				rtbAnnotation.Clear(); rtbAnnotation.Text = bd.TIAnnotation;
			}
			#endregion
		}
		#endregion
		
	}
	
	
	
	
	
	
	
	/// <summary>
	/// класс для хранения данных для отображения прогресса программы
	/// </summary>
	public class StatusView {
		
		#region Закрытые данные класса
		private int m_nAllFiles			= 0;
		private int m_nFB2				= 0;
		private int m_nArchive			= 0;
		private int m_nOther			= 0;
		private int m_nGroup			= 0;
		private int m_nAllFB2InGroups	= 0;
		#endregion
		
		public StatusView() {

		}
		
		#region Открытые методы класса
		public void Clear() {
			// сброс всех данных
			m_nAllFiles			= 0;
			m_nFB2				= 0;
			m_nArchive			= 0;
			m_nOther			= 0;
			m_nGroup			= 0;
			m_nAllFB2InGroups	= 0;
		}
		#endregion
		
		#region Свойства класса
		public virtual int AllFiles {
			get { return m_nAllFiles; }
			set { m_nAllFiles = value; }
        }
		
		public virtual int FB2 {
			get { return m_nFB2; }
			set { m_nFB2 = value; }
        }
		
		public virtual int Archive {
			get { return m_nArchive; }
			set { m_nArchive = value; }
        }
		
		public virtual int Other {
			get { return m_nOther; }
			set { m_nOther = value; }
        }
		
		public virtual int Group {
			get { return m_nGroup; }
			set { m_nGroup = value; }
        }
		
		public virtual int AllFB2InGroups {
			get { return m_nAllFB2InGroups; }
			set { m_nAllFB2InGroups = value; }
        }
		#endregion
	}
	
	/// <summary>
	/// класс для хранения информации по одинаковым книгам (контекст Авторы и Название )
	/// </summary>
	public class FB2FilesData {
		#region Закрытые данные класса
		private string	m_sBookTitleForKey	= null;
		private string	m_sId				= null;
		
		private IList<string>	m_ilPath			= new List<string>();
		private IList<BookData>	m_ilFB2Book			= new List<BookData>();
		#endregion
		
		public FB2FilesData() {

		}
		
		#region Открытые методы класса
		public void AddBookData( BookData abt ) {
			m_ilFB2Book.Add( abt );
        }
		public BookData GetBookData( string sPath ) {
			foreach( BookData a in m_ilFB2Book ) {
				if( a.Path == sPath )
					return a;
			}
			return null;
        }
		public void AddPath( string sFB2Path ) {
			m_ilPath.Add( sFB2Path );
        }
		public IList<string> GetPathList() {
			return m_ilPath;
        }
		#endregion
		
		#region Свойства класса
		public virtual string BookTitleForKey {
			get { return m_sBookTitleForKey; }
			set { m_sBookTitleForKey = value; }
        }
		public virtual string Id {
			get { return m_sId; }
			set { m_sId = value; }
        }
		
		public virtual int Count {
			get { return m_ilPath.Count; }
        }
		#endregion
	}

}
