﻿/*
 * Сделано в SharpDevelop.
 * Пользователь: Вадим Кузнецов (DikBSD)
 * Дата: 09.09.2015
 * Время: 14:30
 * 
 */
using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;

using Core.Common;
using Core.FB2.Genres;
using Core.FB2.FB2Parsers;

using EndWorkMode		= Core.Common.EndWorkMode;
using FilesWorker		= Core.Common.FilesWorker;
using WorksWithBooks	= Core.Common.WorksWithBooks;
using MiscListView		= Core.Common.MiscListView;

// enums
using EndWorkModeEnum		= Core.Common.Enums.EndWorkModeEnum;
using BooksAutoCorrectMode	= Core.Common.Enums.BooksAutoCorrectMode;

namespace Core.Duplicator
{
	/// <summary>
	/// прогресс автокорректировки книг Дубликатора
	/// </summary>
	public partial class AutoCorrectorForm : Form
	{
		#region Закрытые данные класса
		private readonly ListView m_listViewFB2Files = new ListView();
		private readonly SharpZipLibWorker	m_sharpZipLib = new SharpZipLibWorker();
		private readonly EndWorkMode m_EndMode		= new EndWorkMode();
		private readonly string m_TempDir			= Settings.Settings.TempDir;
		private readonly BooksAutoCorrectMode m_WorkMode;  // режим обработки книг
		private readonly DateTime m_dtStart;
		private BackgroundWorker m_bw	= null;  // фоновый обработчик
		#endregion
		
		public AutoCorrectorForm( BooksAutoCorrectMode WorkMode, ListView listViewFB2Files )
		{
			InitializeComponent();
			m_listViewFB2Files	= listViewFB2Files;
			m_WorkMode			= WorkMode;
			
			switch( m_WorkMode ) {
				case BooksAutoCorrectMode.SelectedBooks:
					ProgressBar.Maximum = m_listViewFB2Files.SelectedItems.Count;
					break;
				case BooksAutoCorrectMode.CheckedBooks:
					ProgressBar.Maximum = m_listViewFB2Files.CheckedItems.Count;
					break;
				case BooksAutoCorrectMode.BooksInGroup:
					ProgressBar.Maximum = m_listViewFB2Files.SelectedItems[0].Group.Items.Count;
					break;
				case BooksAutoCorrectMode.BooksInAllGroup:
					ProgressBar.Maximum = m_listViewFB2Files.Items.Count;
					break;
				default:
					ProgressBar.Maximum = m_listViewFB2Files.Items.Count;
					break;
			}
			ProgressBar.Value = 0;
			
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
			int i = 0;
			switch( m_WorkMode ) {
				case BooksAutoCorrectMode.SelectedBooks:
					this.Text = string.Format( "Автокорректировка выделенных {0} книг", m_listViewFB2Files.SelectedItems.Count );
					foreach( ListViewItem SelectedItem in m_listViewFB2Files.SelectedItems ) {
						if( ( m_bw.CancellationPending ) ) {
							e.Cancel = true;
							return;
						}
						WorksWithBooks.autoCorrect(
							SelectedItem, SelectedItem.Text,
							m_listViewFB2Files.SelectedItems.Count == 1 ? true : false,
							m_sharpZipLib
						);
						m_bw.ReportProgress( ++i );
					}
					break;
				case BooksAutoCorrectMode.CheckedBooks:
					this.Text = string.Format( "Автокорректировка помеченных {0} книг", m_listViewFB2Files.CheckedItems.Count );
					foreach( ListViewItem CheckedItem in m_listViewFB2Files.CheckedItems ) {
						if( ( m_bw.CancellationPending ) ) {
							e.Cancel = true;
							return;
						}
						WorksWithBooks.autoCorrect(
							CheckedItem, CheckedItem.Text,
							m_listViewFB2Files.CheckedItems.Count == 1 ? true : false,
							m_sharpZipLib
						);
						m_bw.ReportProgress( ++i );
					}
					break;
				case BooksAutoCorrectMode.BooksInGroup:
					this.Text = string.Format( "Автокорректировка {0} книг в Группе", m_listViewFB2Files.SelectedItems[0].Group.Items.Count );
					foreach( ListViewItem Item in m_listViewFB2Files.SelectedItems[0].Group.Items ) {
						if( ( m_bw.CancellationPending ) ) {
							e.Cancel = true;
							return;
						}
						WorksWithBooks.autoCorrect(
							Item, Item.Text,
							false, m_sharpZipLib
						);
						m_bw.ReportProgress( ++i );
					}
					break;
				case BooksAutoCorrectMode.BooksInAllGroup:
					this.Text = string.Format( "Автокорректировка всех {0} книг во всех Группах", m_listViewFB2Files.Items.Count );
					foreach( ListViewItem Item in m_listViewFB2Files.Items ) {
						if( ( m_bw.CancellationPending ) ) {
							e.Cancel = true;
							return;
						}
						WorksWithBooks.autoCorrect(
							Item, Item.Text, false, m_sharpZipLib
						);
						m_bw.ReportProgress( ++i );
					}
					break;
				default:
					return;
			}

			if( ( m_bw.CancellationPending ) ) {
				e.Cancel = true;
				return;
			}
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
			}
			this.Close();
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