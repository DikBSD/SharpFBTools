/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 30.07.2015
 * Время: 10:53
 * 
 * License: GPL 2.1
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;

using Core.Common;

using FB2Validator		= Core.FB2Parser.FB2Validator;
using SharpZipLibWorker = Core.Common.SharpZipLibWorker;
using EndWorkMode 		= Core.Common.EndWorkMode;
using MiscListView		= Core.Common.MiscListView;
using Colors			= Core.Common.Colors;

// enums
using EndWorkModeEnum		= Core.Common.Enums.EndWorkModeEnum;
using ResultViewCollumnEnum	= Core.Common.Enums.ResultViewCollumnEnum;
using BooksValidateModeEnum = Core.Common.Enums.BooksValidateModeEnum;

namespace Core.Corrector
{
	/// <summary>
	/// Групповая валидация fb2-файлов Корректора Метаданных
	/// </summary>
	public partial class ValidatorForm : Form
	{
		#region Закрытые данные класса
		private readonly BooksValidateModeEnum m_booksValidateMode = BooksValidateModeEnum.AllBooks;
		private readonly string				m_SourceDir		= string.Empty;
		
		private readonly ListView			m_listView		= new ListView();
		private readonly string				m_TempDir		= Settings.Settings.TempDirPath;
		private readonly SharpZipLibWorker	m_sharpZipLib	= new SharpZipLibWorker();
		private readonly EndWorkMode		m_EndMode 		= new EndWorkMode();
		
		private BackgroundWorker m_bw = null;
		private DateTime m_dtStart = DateTime.Now;
		#endregion
		
		public ValidatorForm( BooksValidateModeEnum booksValidateMode, ListView listView, string SourceDir )
		{
			InitializeComponent();

			m_SourceDir			= SourceDir;
			m_booksValidateMode	= booksValidateMode;
			m_listView			= listView;
			
			InitializeBackgroundWorker();
			if ( !m_bw.IsBusy )
				m_bw.RunWorkerAsync(); //если не занят. то запустить процесс
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
		// 								BACKGROUNDWORKER: ГРУППОВАЯ ВАЛИДАЦИЯ
		// =============================================================================================
		#region BackgroundWorker: Групповая валидация
		
		// =============================================================================================
		//			BackgroundWorker: Групповая валидация
		// =============================================================================================
		#region BackgroundWorker: Групповая валидация
		private void InitializeBackgroundWorker() {
			// Инициализация перед использование BackgroundWorker Групповая валидация
			m_bw = new BackgroundWorker();
			m_bw.WorkerReportsProgress		= true; // Позволить выводить прогресс процесса
			m_bw.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
			m_bw.DoWork				+= new DoWorkEventHandler( bw_DoWork );
			m_bw.ProgressChanged 	+= new ProgressChangedEventHandler( bw_ProgressChanged );
			m_bw.RunWorkerCompleted	+= new RunWorkerCompletedEventHandler( bw_RunWorkerCompleted );
		}
		
		// Обработка файлов
		private void bw_DoWork( object sender, DoWorkEventArgs e ) {
			m_dtStart = DateTime.Now;
			FB2Validator fv2Validator = new FB2Validator();
			m_listView.BeginUpdate();
			if ( m_booksValidateMode == BooksValidateModeEnum.CheckedBooks ) {
				// для помеченных книг
				ListView.CheckedListViewItemCollection checkedItems = m_listView.CheckedItems;
				if ( checkedItems.Count > 0 ) {
					this.Text += ": Помеченные книги";
					ProgressBar.Maximum = checkedItems.Count;
					ProgressBar.Value	= 0;
					int i = 0;
					foreach ( ListViewItem lvi in checkedItems ) {
						if ( ( m_bw.CancellationPending ) ) {
							m_listView.EndUpdate();
							e.Cancel = true;
							return;
						}
						validateFile( lvi, ref fv2Validator );
						m_bw.ReportProgress( ++i );
					}
				}
			} else if ( m_booksValidateMode == BooksValidateModeEnum.SelectedBooks ) {
				// для выделенных книг
				ListView.SelectedListViewItemCollection selItems = m_listView.SelectedItems;
				this.Text += ": Выделенные книги";
				if ( selItems.Count > 0 ) {
					// группа для выделенной книги
					ProgressBar.Maximum = selItems.Count;
					ProgressBar.Value	= 0;
					int i = 0;
					foreach ( ListViewItem lvi in selItems ) {
						if ( ( m_bw.CancellationPending ) ) {
							m_listView.EndUpdate();
							e.Cancel = true;
							return;
						}
						validateFile( lvi, ref fv2Validator );
						m_bw.ReportProgress( ++i );
					}
				}
			} else {
				// для всех книг
				this.Text += ": Выделенные книги";
				ListView.ListViewItemCollection lvItemColl = m_listView.Items;
				if ( lvItemColl.Count > 0 ) {
					// группа для выделенной книги
					ProgressBar.Maximum = lvItemColl.Count;
					ProgressBar.Value	= 0;
					int i = 0;
					foreach ( ListViewItem lvi in lvItemColl ) {
						if ( ( m_bw.CancellationPending ) ) {
							m_listView.EndUpdate();
							e.Cancel = true;
							return;
						}
						validateFile( lvi, ref fv2Validator );
						m_bw.ReportProgress( ++i );
					}
				}
			}
			m_listView.EndUpdate();
		}
		
		// Отобразим результат Валидации
		private void bw_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
			++ProgressBar.Value;
		}
		
		// Проверяем - это отмена, ошибка, или конец задачи и сообщить
		private void bw_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {
			// Сбор данных о причине завершения работы
			DateTime dtEnd = DateTime.Now;
			string sTime = dtEnd.Subtract( m_dtStart ).ToString() + " (час.:мин.:сек.)";
			if ( ( e.Cancelled ) ) {
				m_EndMode.EndMode = EndWorkModeEnum.Cancelled;
				m_EndMode.Message = "Валидация файлов остановлена!\nЗатрачено времени: " + sTime;
			}
			else if ( e.Error != null ) {
				m_EndMode.EndMode = EndWorkModeEnum.Error;
				m_EndMode.Message = "Ошибка:\n" + e.Error.Message + "\n" + e.Error.StackTrace;
			} else {
				m_EndMode.EndMode = EndWorkModeEnum.Done;
				m_EndMode.Message = "Валидация файлов завершена!\nЗатрачено времени: " + sTime;
			}
			this.Close();
		}
		
		#endregion

		// =============================================================================================
		// 							ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ ДЛЯ BACKGROUNDWORKER
		// =============================================================================================
		#region BackgroundWorker: Вспомогательные методы
		private void validateFile( ListViewItem lvi, ref FB2Validator fv2Validator ) {
			if ( WorksWithBooks.isFileItem( lvi ) ) {
				string FilePath = Path.Combine( m_SourceDir.Trim(), lvi.SubItems[0].Text );
				if ( File.Exists( FilePath ) ) {
					string Msg = fv2Validator.ValidatingFB2File( FilePath );
					lvi.SubItems[(int)ResultViewCollumnEnum.Validate].Text = Msg == string.Empty ? "Да" : "Нет";
					if ( !string.IsNullOrEmpty( Msg ) )
						lvi.ForeColor = Colors.FB2NotValidForeColor;
					else
						lvi.ForeColor = FilesWorker.isFB2File( FilePath )
							? Color.FromName( "WindowText" ) : Colors.ZipFB2ForeColor;
				}
			}
		}
		#endregion
		
		#endregion
		
		// =============================================================================================
		// 								ОБРАБОТЧИКИ для контекстного меню
		// =============================================================================================
		#region Обработчики для контекстного меню
		void BtnStopClick(object sender, EventArgs e)
		{
			if ( m_bw.WorkerSupportsCancellation )
				m_bw.CancelAsync();
		}
		#endregion
	}
}
