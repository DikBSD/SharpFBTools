/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 01.09.2014
 * Время: 10:37
 * 
 * License: GPL 2.1
 */
using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Xml.Linq;

using EndWorkMode = Core.Common.EndWorkMode;

// enums
using EndWorkModeEnum = Core.Common.Enums.EndWorkModeEnum;

namespace Core.Duplicator
{
	/// <summary>
	/// CheckedBooksPathToXMLForm: сохранение списка помеченных книг (путей) в xml файл
	/// </summary>
	public partial class CheckedBooksPathToXMLForm : Form
	{
		#region Закрытые данные класса
		private readonly string		m_Source	= string.Empty;
		private readonly string		m_XMLPath	= string.Empty;
		private readonly ListView	m_lvResult	= new ListView();
		private readonly EndWorkMode m_EndMode	= new EndWorkMode();
		private BackgroundWorker	m_bw		= null; // фоновый обработчик
		private readonly DateTime	m_dtStart 	= DateTime.Now;
		#endregion
		
		public CheckedBooksPathToXMLForm( string Source, string XMLPath, ListView lvResult )
		{
			InitializeComponent();
			
			m_Source	= Source;
			m_XMLPath	= XMLPath;
			m_lvResult	= lvResult;
			
			InitializeBackgroundWorker();
			
			ProgressBar.Maximum	= m_lvResult.CheckedItems.Count;
			ProgressBar.Value	= 0;
			
			if( !m_bw.IsBusy )
				m_bw.RunWorkerAsync(); //если не занят, то запустить процесс
		}
		
		// =============================================================================================
		// 								ОТКРЫТЫЕ СВОЙСТВА
		// =============================================================================================
		#region Открытые свойства
		public virtual Core.Common.EndWorkMode EndMode {
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
		
		// Сохранение списка помеченных книг (пути) в xml
		private void bw_DoWork( object sender, DoWorkEventArgs e ) {
			#region Код
			ListView.CheckedListViewItemCollection checkedItems = m_lvResult.CheckedItems;
			XDocument doc = new XDocument(
				new XDeclaration("1.0", "utf-8", "yes"),
				new XComment("Файл путей всех помеченных fb2 книг, сохраненный после анализа копий для дальнейшей их обработки (copy/move/delete) сторонними утилитами"),
				new XElement("Files",
				             new XAttribute("count", checkedItems.Count),
				             new XAttribute("source", m_Source)
				            )
			);
			
			int i = 0;
			foreach( ListViewItem lvi in checkedItems ) {
				if( ( m_bw.CancellationPending ) ) {
					e.Cancel = true;
					return;
				}
				doc.Root.Add(
					new XElement("Path", new XAttribute("number", i++), lvi.Text)
				);
				m_bw.ReportProgress( i );
				ProgressBar.Update();
			}
			
			doc.Save(m_XMLPath);
			#endregion
		}
		
		// Отображение результата
		private void bw_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
			++ProgressBar.Value;
		}
		
		// Проверяем - это отмена, ошибка, или конец задачи и сообщить
		private void bw_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {
			#region Код
			DateTime dtEnd = DateTime.Now;
			string sTime = dtEnd.Subtract( m_dtStart ).ToString() + " (час.:мин.:сек.)";
			if ( e.Cancelled ) {
				m_EndMode.EndMode = EndWorkModeEnum.Cancelled;
				m_EndMode.Message = "Сохранение путей помеченных книг в файл прервано!\nЗатрачено времени: " + sTime;
			} else if( e.Error != null ) {
				m_EndMode.EndMode = EndWorkModeEnum.Error;
				m_EndMode.Message = "Ошибка:\n" + e.Error.Message + "\n" + e.Error.StackTrace + "\nЗатрачено времени: " + sTime;
			} else {
				m_EndMode.EndMode = EndWorkModeEnum.Done;
				m_EndMode.Message = "Сохранение путей помеченных книг в файл завершено!\nЗатрачено времени: " + sTime;
			}
			this.Close();
			#endregion
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
