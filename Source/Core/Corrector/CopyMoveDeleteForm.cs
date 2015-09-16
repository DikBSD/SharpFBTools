/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 30.07.2015
 * Время: 7:07
 * 
 * License: GPL 2.1
 */
using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;

using EndWorkMode		= Core.Common.EndWorkMode;
using FilesWorker		= Core.Common.FilesWorker;
using WorksWithBooks	= Core.Common.WorksWithBooks;
using MiscListView		= Core.Common.MiscListView;

// enums
using EndWorkModeEnum	= Core.Common.Enums.EndWorkModeEnum;
using BooksWorkMode		= Core.Common.Enums.BooksWorkMode;

namespace Core.Corrector
{
	/// <summary>
	/// Обработка помеченных fb2-файлов: Copy / Move / Delete для Редактора метаданных
	/// </summary>
	public partial class CopyMoveDeleteForm : Form
	{
		#region Закрытые данные класса
		private readonly bool m_Fast				 = true; // false - с удаление итемов списка копий (медленно). true - без удаления (быстро)
		private readonly ListView m_listViewFB2Files = new ListView();
		private readonly string	m_SourceDir			= string.Empty;
		private readonly string	m_TargetDir			= string.Empty;
		private readonly int	m_FileExistMode		= 1; // добавить к создаваемому fb2-файлу очередной номер
		private readonly EndWorkMode	m_EndMode	= new EndWorkMode();
		private readonly BooksWorkMode	m_WorkMode;  // режим обработки книг
		private bool m_bFilesWorked			= false; // флаг = true, если хоть один файл был на диске и был обработан (copy, move или delete)
		private BackgroundWorker m_bwcmd	= null;  // фоновый обработчик
		#endregion
		
		public CopyMoveDeleteForm( bool Fast, BooksWorkMode WorkMode, string Source, string TargetDir,
		                          int FileExistMode, ListView listViewFB2Files )
		{

			InitializeComponent();

			m_Fast				= Fast;
			m_listViewFB2Files	= listViewFB2Files;
			m_SourceDir			= Source;
			m_TargetDir			= TargetDir;
			m_WorkMode			= WorkMode;
			m_FileExistMode		= FileExistMode;
			ProgressBar.Maximum = m_listViewFB2Files.CheckedItems.Count;
			ProgressBar.Value	= 0;
			
			InitializeCopyMovedeleteWorkerBackgroundWorker();
			if( !m_bwcmd.IsBusy )
				m_bwcmd.RunWorkerAsync(); //если не занят, то запустить процесс
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
			if( m_Fast )
				m_listViewFB2Files.BeginUpdate();
			
			this.Text += ": " + m_listViewFB2Files.CheckedItems.Count.ToString() + " книг";
			// удаление всех элементов Списка, для которых отсутствуют файлы на жестком диске для Корректора
			MiscListView.removeAllItemForNonExistFile( m_SourceDir.Trim(), m_listViewFB2Files );
			switch( m_WorkMode ) {
				case BooksWorkMode.CopyCheckedBooks:
					this.Text = "Копирование помеченных книг в папку " + m_TargetDir;
					this.Text += String.Format( ": Файлов: {0}", m_listViewFB2Files.CheckedItems.Count );
					CopyOrMoveCheckedFilesTo( ref m_bwcmd, ref e, true,
					                         m_SourceDir, m_TargetDir, m_listViewFB2Files,
					                         m_FileExistMode );
					break;
				case BooksWorkMode.MoveCheckedBooks:
					this.Text = "Перемещение помеченных книг в папку " + m_TargetDir;
					this.Text += String.Format( ": Файлов: {0}", m_listViewFB2Files.CheckedItems.Count );
					CopyOrMoveCheckedFilesTo( ref m_bwcmd, ref e, false,
					                         m_SourceDir, m_TargetDir, m_listViewFB2Files,
					                         m_FileExistMode );
					break;
				case BooksWorkMode.DeleteCheckedBooks:
					this.Text = "Удаление помеченных книг";
					this.Text += String.Format( ": Файлов: {0}", m_listViewFB2Files.CheckedItems.Count );
					DeleteCheckedFiles( ref m_bwcmd, ref e, m_listViewFB2Files );
					break;
				default:
					return;
			}
			if( m_Fast )
				m_listViewFB2Files.EndUpdate();
		}
		
		// Отобразим результат Копирования / Перемещения / Удаление
		private void bwcmd_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
			++ProgressBar.Value;
		}
		
		// Завершение работы Обработчика Файлов
		private void bwcmd_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {
			string sMessCanceled, sMessError, sMessDone;
			sMessCanceled = sMessError = sMessDone = string.Empty;
			switch( m_WorkMode ) {
				case BooksWorkMode.CopyCheckedBooks:
					sMessDone 		= "Копирование файлов в указанную папку завершено!";
					sMessCanceled	= "Копирование файлов в указанную папку остановлено!";
					break;
				case BooksWorkMode.MoveCheckedBooks:
					sMessDone 		= "Перемещение файлов в указанную папку завершено!";
					sMessCanceled	= "Перемещение файлов в указанную папку остановлено!";
					break;
				case BooksWorkMode.DeleteCheckedBooks:
					sMessDone 		= "Удаление файлов из папки-источника завершено!";
					sMessCanceled	= "Удаление файлов из папки-источника остановлено!";
					break;
			}
			
			if( !m_bFilesWorked ) {
				const string s = "На диске не найдено ни одного файла из помеченных!\n";
				switch( m_WorkMode ) {
					case BooksWorkMode.CopyCheckedBooks:
						sMessDone = s + "Копирование файлов в указанную папку не произведено!";
						break;
					case BooksWorkMode.MoveCheckedBooks:
						sMessDone = s + "Перемещение файлов в указанную папку не произведено!";
						break;
					case BooksWorkMode.DeleteCheckedBooks:
						sMessDone = s + "Удаление файлов из папки-источника не произведено!";
						break;
				}
			}

			// Проверяем это отмена, ошибка, или конец задачи и сообщить
			if( ( e.Cancelled ) ) {
				m_EndMode.EndMode = EndWorkModeEnum.Cancelled;
				m_EndMode.Message = sMessCanceled;
			} else if( e.Error != null ) {
				m_EndMode.EndMode = EndWorkModeEnum.Error;
				m_EndMode.Message = "Ошибка:\n" + e.Error.Message + "\n" + e.Error.StackTrace;
			} else {
				m_EndMode.EndMode = EndWorkModeEnum.Done;
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
		                                     ListView lvResult, int nFileExistMode ) {
			int i = 0;
			ListView.CheckedListViewItemCollection checkedItems = lvResult.CheckedItems;
			foreach( ListViewItem lvi in checkedItems ) {
				// Проверить флаг на остановку процесса
				if( ( bw.CancellationPending ) ) {
					e.Cancel = true; // Выставить окончание - по отмене, сработает событие bwcmd_RunWorkerCompleted
					return;
				} else {
					string FilePath = Path.Combine( SourceDir, lvi.Text);
					// есть ли такая книга на диске? Если нет - то смотрим следующую
					if( File.Exists( FilePath ) ) {
						string NewPath = Path.Combine( TargetDir, Path.GetFileName( FilePath ) );
						FileInfo fi = new FileInfo( NewPath );
						if( !fi.Directory.Exists )
							Directory.CreateDirectory( fi.Directory.ToString() );

						if( File.Exists( NewPath ) ) {
							if( nFileExistMode == 0 )
								File.Delete( NewPath );
							else
								NewPath = FilesWorker.createFilePathWithSufix( NewPath, nFileExistMode );
						}
						if( IsCopy )
							File.Copy( FilePath, NewPath );
						else {
							File.Move( FilePath, NewPath );
							if( !m_Fast )
								lvResult.Items.Remove( lvi );
							else {
								// пометка цветом и зачеркиванием удаленных книг с диска, но не из списка (быстрый режим удаления)
								WorksWithBooks.markRemoverFileInCopyesList( lvi );
							}
						}
						m_bFilesWorked |= true;
						bw.ReportProgress( ++i ); 
					}
				}
			}
		}
		
		// удалить помеченные файлы...
		public void DeleteCheckedFiles( ref BackgroundWorker bw, ref DoWorkEventArgs e, ListView lvResult ) {
			int i = 0;
			ListView.CheckedListViewItemCollection checkedItems = lvResult.CheckedItems;
			foreach( ListViewItem lvi in checkedItems ) {
				// Проверить флаг на остановку процесса
				if( ( bw.CancellationPending ) ) {
					e.Cancel = true; // Выставить окончание - по отмене, сработает событие bwcmd_RunWorkerCompleted
					return;
				} else {
					string FilePath = Path.Combine( m_SourceDir, lvi.Text);
					if( File.Exists( FilePath) ) {
						File.Delete( FilePath );
						if( !m_Fast ) {
							if( lvResult.Items.Count > 0 )
								lvResult.Items.Remove( lvi );
						} else {
							// пометка цветом и зачеркиванием удаленных книг с диска, но не из списка (быстрый режим удаления)
							WorksWithBooks.markRemoverFileInCopyesList( lvi );
						}
						m_bFilesWorked |= true;
					}
					bw.ReportProgress( ++i );
				}
			}
		}
		#endregion
		
		#endregion

		// =============================================================================================
		// 								ОБРАБОТЧИКИ СОБЫТИЙ
		// =============================================================================================
		#region Обработчики событий
		// нажатие кнопки прерывания работы
		void BtnStopClick(object sender, EventArgs e)
		{
			if( m_bwcmd.WorkerSupportsCancellation )
				m_bwcmd.CancelAsync();
		}
		#endregion
	}
}