/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 25.02.2014
 * Time: 13:47
 * 
 * License: GPL 2.1
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;

using Core.Misc;

using filesWorker = Core.FilesWorker.FilesWorker;

namespace Core.Duplicator
{
	/// <summary>
	/// Обработка помеченных fb2-файлов: Copy / Move / Delete
	/// </summary>
	public partial class CopyMoveDeleteForm : Form
	{
		#region Закрытые данные класса
		private string	m_Source			= string.Empty;
		private string	m_TargetDir			= string.Empty;
		private string	m_FileWorkerMode	= string.Empty;
		private int m_FileExistMode			= 1; // добавить к созхдаваемому fb2-файлу очередной номер
		private MiscListView m_mscLV		= new MiscListView(); // класс по работе с ListView
		
		private BackgroundWorker m_bwcmd	= null;
		private bool m_bFilesWorked			= false; // флаг = true, если хоть один файл был на диске и был обработан (copy, move или delete)
		
		private System.Windows.Forms.ListView m_lvFilesCount = new System.Windows.Forms.ListView();
		private System.Windows.Forms.ListView m_lvResult	 = new System.Windows.Forms.ListView();
		
		private Core.Misc.EndWorkMode m_EndMode = new Core.Misc.EndWorkMode();
		#endregion
		
		public CopyMoveDeleteForm( string FileWorkerMode, string Source, string TargetDir, int FileExistMode,
		                          System.Windows.Forms.ListView lvFilesCount, System.Windows.Forms.ListView lvResult )
		{
			InitializeComponent();
			
			m_FileWorkerMode = FileWorkerMode;
			m_lvResult		= lvResult;
			m_lvFilesCount	= lvFilesCount;
			m_Source		= Source;
			m_TargetDir		= TargetDir;
			m_FileExistMode = FileExistMode;
			
			ProgressBar.Maximum = m_lvResult.CheckedItems.Count;
			ProgressBar.Value	= 0;
			
			InitializeCopyMovedeleteWorkerBackgroundWorker();
			if( m_bwcmd.IsBusy != true )
				m_bwcmd.RunWorkerAsync(); //если не занят, то запустить процесс
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
		// 						BACKGROUNDWORKER: КОПИРОВАНИЕ / ПЕРЕМИЕЩЕНИЕ / УДАЛЕНИЕ
		// =============================================================================================
		#region BackgroundWorker: Копирование / Перемещение / Удаление
		
		// =============================================================================================
		//			BackgroundWorker: Копирование / Перемещение / Удаление
		// =============================================================================================
		#region BackgroundWorker: Копирование / Перемещение / Удаление
		private void InitializeCopyMovedeleteWorkerBackgroundWorker() {
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
			switch( m_FileWorkerMode ) {
				case "Copy":
					this.Text = "Копирование помеченных копий книг в папку "+m_TargetDir;
					CopyOrMoveCheckedFilesTo( ref m_bwcmd, ref e, true,
					                         m_Source, m_TargetDir, m_lvResult,
					                         m_FileExistMode );
					break;
				case "Move":
					this.Text = "Перемещение помеченных копий книг в папку "+m_TargetDir;
					CopyOrMoveCheckedFilesTo( ref m_bwcmd, ref e, false,
					                         m_Source, m_TargetDir, m_lvResult,
					                         m_FileExistMode );
					break;
				case "Delete":
					this.Text = "Удаление помеченных копий книг";
					DeleteCheckedFiles( ref m_bwcmd, ref e, m_lvResult );
					break;
				default:
					return;
			}
		}
		
		// Отобразим результат Копирования / Перемещения / Удаление
		private void bwcmd_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
			// обновление числа групп и книг во всех группах
			newGroupItemsCount( m_lvResult, m_lvFilesCount );
			++ProgressBar.Value;
		}
		
		// Завершение работы Обработчика Файлов
		private void bwcmd_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {
			string sMessCanceled, sMessError, sMessDone, sTabPageDefText;
			sMessCanceled = sMessError = sMessDone = sTabPageDefText = string.Empty;
			switch( m_FileWorkerMode ) {
				case "Copy":
					sMessDone 		= "Копирование файлов в указанную папку завершено!";
					sMessCanceled	= "Копирование файлов в указанную папку остановлено!";
					break;
				case "Move":
					sMessDone 		= "Перемещение файлов в указанную папку завершено!";
					sMessCanceled	= "Перемещение файлов в указанную папку остановлено!";
					break;
				case "Delete":
					sMessDone 		= "Удаление файлов из папки-источника завершено!";
					sMessCanceled	= "Удаление файлов из папки-источника остановлено!";
					break;
			}
			
			if( !m_bFilesWorked ) {
				string s = "На диске не найдено ни одного файла из помеченных!\n";
				switch( m_FileWorkerMode ) {
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

			// Проверяем это отмена, ошибка, или конец задачи и сообщить
			if( ( e.Cancelled ) ) {
				m_EndMode.EndMode = Core.Misc.EndWorkMode.EndWorkModeEnum.Cancelled;
				m_EndMode.Message = sMessCanceled;
			} else if( e.Error != null ) {
				m_EndMode.EndMode = Core.Misc.EndWorkMode.EndWorkModeEnum.Error;
				m_EndMode.Message = "Ошибка:\n" + e.Error.Message + "\n" + e.Error.StackTrace;
			} else {
				m_EndMode.EndMode = Core.Misc.EndWorkMode.EndWorkModeEnum.Done;
				m_EndMode.Message = sMessDone;
			}
			this.Close();
		}
		#endregion

		// =============================================================================================
		// 					Реализация Copy/Move/Delete помеченных книг
		// =============================================================================================
		#region Реализация Copy/Move/Delete помеченных книг
		// копировать или переместить файлы в...
		public void CopyOrMoveCheckedFilesTo( ref BackgroundWorker bw, ref DoWorkEventArgs e,
		                                     bool IsCopy, string SourceDir, string TargetDir,
		                                     ListView m_lvResult, int nFileExistMode ) {
			#region Код
			List<ListViewGroup> listLVG = new List<ListViewGroup>();
			System.Windows.Forms.ListView.CheckedListViewItemCollection checkedItems = m_lvResult.CheckedItems;
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
							m_lvResult.Items.Remove( lvi );
							listLVG.Add( lvg );
						}
						m_bFilesWorked |= true;
					}
					bw.ReportProgress( ++i ); // отобразим данные в контролах
				}
			}
			
			// Если в обрабатываемой группе осталась только 1 книга, то ее и группу удаляем
			if( !IsCopy ) {
				// только для перемещения книг
				foreach( ListViewGroup g in listLVG ) {
					// удаление оставшейся книги в группе и самой группы с контрола отображения (1 книга - это уже не копия)
					workingGroupItemAfterBookDelete( m_lvResult, g );
					// обновление числа групп и книг во всех группах
					newGroupItemsCount( m_lvResult, m_lvFilesCount );
				}
			}
			#endregion
		}
		
		// удалить помеченные файлы...
		public void DeleteCheckedFiles( ref BackgroundWorker bw, ref DoWorkEventArgs e,
		                               ListView m_lvResult ) {
			#region Код
			List<ListViewGroup> listLVG = new List<ListViewGroup>();
			System.Windows.Forms.ListView.CheckedListViewItemCollection checkedItems = m_lvResult.CheckedItems;
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
						m_lvResult.Items.Remove( lvi );
						listLVG.Add( lvg );
						m_bFilesWorked |= true;
					}
					bw.ReportProgress( ++i ); // отобразим данные в контролах
				}
			}
			// Если в обрабатываемой группе осталась только 1 книга, то ее и группу удаляем
			foreach( ListViewGroup g in listLVG ) {
				// удаление оставшейся книги в группе и самой группы с контрола отображения (1 книга - это уже не копия)
				workingGroupItemAfterBookDelete( m_lvResult, g );
				// обновление числа групп и книг во всех группах
				newGroupItemsCount( m_lvResult, m_lvFilesCount );
			}
			#endregion
		}
		#endregion
		
		#endregion

		// =============================================================================================
		// 							ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ И АЛГОРИТМЫ КЛАССА
		// =============================================================================================
		#region Вспомогательные методы и алгоритмы класса
		// обновление числа групп и книг во всех группах
		private void newGroupItemsCount( System.Windows.Forms.ListView lvResult, System.Windows.Forms.ListView lvFilesCount ) {
			// новое число групп
			m_mscLV.ListViewStatus( lvFilesCount, 5, lvResult.Groups.Count.ToString() );
			// число книг во всех группах
			m_mscLV.ListViewStatus( lvFilesCount, 6, lvResult.Items.Count.ToString() );
		}
		
		// удаление оставшейся книги в группе и самой группы с контрола отображения (1 книга - это уже не копия)
		private void workingGroupItemAfterBookDelete( System.Windows.Forms.ListView listView, ListViewGroup lvg ) {
			if( lvg.Items.Count <= 1 ) {
				if( lvg.Items.Count == 1 )
					listView.Items[lvg.Items[0].Index].Remove();
				listView.Groups.Remove( lvg );
			}
		}
		#endregion
		
		// =============================================================================================
		// 								ОБРАБОТЧИКИ для контекстного меню
		// =============================================================================================
		#region Обработчики для контекстного меню
		void BtnStopClick(object sender, EventArgs e) 
		{
			if( m_bwcmd.WorkerSupportsCancellation )
				m_bwcmd.CancelAsync();
		}
		#endregion
	}
}
