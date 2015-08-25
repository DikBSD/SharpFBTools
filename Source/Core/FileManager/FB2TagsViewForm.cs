/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 24.07.2015
 * Время: 11:40
 * 
 * License: GPL 2.1
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.IO;

using Core.Common;
using Core.FB2.Genres;

using EndWorkMode	= Core.Common.EndWorkMode;
using FilesWorker	= Core.Common.FilesWorker;
using Colors		= Core.Common.Colors;

// enums
using EndWorkModeEnum = Core.Common.Enums.EndWorkModeEnum;

namespace Core.FileManager
{
	/// <summary>
	/// FB2TagsViewForm: прогресс при формировании тегов fb2 книг для списка файлов Сортировщика
	/// </summary>
	public partial class FB2TagsViewForm : Form
	{
		#region Закрытые данные класса
		private readonly SharpZipLibWorker m_sharpZipLib = new SharpZipLibWorker();
		private BackgroundWorker	m_bw				= null; // фоновый обработчик
		private readonly ListView	m_listViewFB2Files	= new ListView();
		private readonly EndWorkMode m_EndMode			= new EndWorkMode();
		private readonly string		m_TempDir			= Settings.Settings.TempDir;
		private readonly bool 		m_isCreateItems		= true;
		private readonly bool 		m_isTagsView		= false;
		private readonly bool 		m_NeedValidate		= false;
		private readonly bool 		m_IsLibrusecGenres	= true;
		private readonly string		m_dirPath			= null;
		private IFBGenres			m_fb2FullSortGenres	= null;			// спиок жанров для Полной Сортировки для режима отображения метаданных для файлов Проводника
		private readonly DateTime	m_dtStart 			= DateTime.Now;
		#endregion
		
		public FB2TagsViewForm( bool isCreateItems, bool isTagsView, bool NeedValidate, bool IsLibrusecGenres,
		                       IFBGenres fb2FullSortGenres, ListView listViewFB2Files, string dirPath = null )
		{
			InitializeComponent();
			
			InitializeBackgroundWorker();
			
			m_NeedValidate		= NeedValidate;
			m_IsLibrusecGenres	= IsLibrusecGenres;
			m_fb2FullSortGenres = fb2FullSortGenres;
			m_isTagsView		= isTagsView;
			m_isCreateItems		= isCreateItems;
			m_listViewFB2Files	= listViewFB2Files;
			m_dirPath			= dirPath;
			ProgressBar.Value	= 0;
			
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
		//			BackgroundWorker: Сохранение списка помеченных книг (пути) в xml
		// =============================================================================================
		#region BackgroundWorker: Сохранение списка помеченных книг (пути) в xml
		private void InitializeBackgroundWorker() {
			// Инициализация перед использование BackgroundWorker
			m_bw = new BackgroundWorker();
			m_bw.WorkerReportsProgress		= true; // Позволить выводить прогресс процесса
			m_bw.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
			m_bw.DoWork 			+= new DoWorkEventHandler( bw_DoWork );
			m_bw.ProgressChanged 	+= new ProgressChangedEventHandler( bw_ProgressChanged );
			m_bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler( bw_RunWorkerCompleted );
		}
		
		// генерация списка файлов с описанием из метаданных
		private void bw_DoWork( object sender, DoWorkEventArgs e ) {
			if( !m_isCreateItems ) {
				// отображение/скрытие метаданных данных книг в Списке Сортировщика
				if( m_listViewFB2Files.Items.Count > 0 ) {
					ProgressBar.Maximum	= m_listViewFB2Files.Items.Count;
					if (
						!WorksWithBooks.viewOrHideBookMetaDataLocal(
							m_listViewFB2Files, ref m_fb2FullSortGenres,
							m_IsLibrusecGenres, m_isTagsView, m_NeedValidate,
							m_bw, e
						)
					)
						e.Cancel = true;
				}
			} else {
				// генерация списка файлов - создание итемов listViewSource
				if (
					!WorksWithBooks.generateBooksListWithMetaData(
						m_listViewFB2Files, m_dirPath, ref m_fb2FullSortGenres,
						m_IsLibrusecGenres, m_isTagsView, true, m_NeedValidate,
						false, this, ProgressBar, m_bw, e
					)
				)
					e.Cancel = true;
			}
		}
		
		// Отображение результата
		private void bw_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
			++ProgressBar.Value;
		}
		
		// Проверяем - это отмена, ошибка, или конец задачи и сообщить
		private void bw_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {
			// очистка временной папки
			FilesWorker.RemoveDir( m_TempDir );
			// авторазмер колонок Списка
			MiscListView.AutoResizeColumns( m_listViewFB2Files );
			DateTime dtEnd = DateTime.Now;
			string sTime = dtEnd.Subtract( m_dtStart ).ToString() + " (час.:мин.:сек.)";
			if ( e.Cancelled ) {
				m_EndMode.EndMode = EndWorkModeEnum.Cancelled;
				m_EndMode.Message = "Отображение метаданных книг прервано!\nСгенерирован список " + ProgressBar.Value + " каталогов и папок из " + ProgressBar.Maximum + "\nЗатрачено времени: " + sTime;
			} else if( e.Error != null ) {
				m_EndMode.EndMode = EndWorkModeEnum.Error;
				m_EndMode.Message = "Ошибка:\n" + e.Error.Message + "\n" + e.Error.StackTrace + "\nСгенерирован список " + ProgressBar.Value + " каталогов и папок из " + ProgressBar.Maximum + "\nЗатрачено времени: " + sTime;
			} else {
				m_EndMode.EndMode = EndWorkModeEnum.Done;
				m_EndMode.Message = "Отображение метаданных книг завершено!\nЗатрачено времени: " + sTime;
			}
			this.Close();
		}
		#endregion
		
		// =============================================================================================
		// 								ОБРАБОТЧИКИ СОБЫТИЙ
		// =============================================================================================
		#region Обработчики событий
		void BtnStopClick(object sender, EventArgs e)
		{
			if( m_bw.WorkerSupportsCancellation )
				m_bw.CancelAsync();
		}
		#endregion
	}
}
