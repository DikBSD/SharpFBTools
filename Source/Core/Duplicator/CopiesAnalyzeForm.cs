/*
 * Сделано в SharpDevelop.
 * Пользователь: VadimK
 * Дата: 28.08.2014
 * Время: 13:15
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Windows.Forms;
using System.ComponentModel;

using Core.Misc;

using CompareMode		= Core.Misc.Enums.CompareMode;
using EndWorkModeEnum	= Core.Misc.Enums.EndWorkModeEnum;
using ResultViewCollumn = Core.Misc.Enums.ResultViewCollumn;
using GroupAnalyzeMode	= Core.Misc.Enums.GroupAnalyzeMode;
using MiscListView		= Core.Misc.MiscListView;

namespace Core.Duplicator
{
	/// <summary>
	/// CopiesAnalyzeForm: форма прогреса анализа корпий файлов
	/// </summary>
	public partial class CopiesAnalyzeForm : Form
	{
		#region Закрытые данные класса
		private readonly CompareMode		m_AnalyzeMode; 		// метод анализа книг
		private readonly GroupAnalyzeMode	m_GroupAnalyzeMode; // метод анализа книг (в группе или во всех группах)
		private readonly ListView			m_lvResult	= new ListView();
		private readonly MiscListView		m_mscLV		= new MiscListView(); // класс по работе с ListView
		private readonly StatusView			m_sv		= new StatusView();
		private readonly EndWorkMode		m_EndMode	= new EndWorkMode();

		private readonly DateTime	m_dtStart;
		private BackgroundWorker	m_bw = null; // фоновый обработчик
		
		/// <summary>
		/// Данные о самой "новой" книге в группе
		/// </summary>
		private class FB2BookInfo {
			private int m_IndexVersion = 0;
			private int m_IndexCreationTime= 0;
			private int m_IndexLastWriteTime = 0;
			private string m_Version			= string.Empty;
			private DateTime  m_CreationTime	= Convert.ToDateTime("01/01/1900");
			private DateTime  m_LastWriteTime	= Convert.ToDateTime("01/01/1900");
			
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
			public virtual DateTime CreationTime {
				get { return m_CreationTime; }
				set { m_CreationTime = value; }
			}
			public virtual DateTime LastWriteTime {
				get { return m_LastWriteTime; }
				set { m_LastWriteTime = value; }
			}
		}
		#endregion
		
		public CopiesAnalyzeForm( GroupAnalyzeMode ToGroupAnalyzeMode, CompareMode AnalyzeMode, ListView lvResult )
		{
			InitializeComponent();
			
			m_AnalyzeMode		= AnalyzeMode;
			m_GroupAnalyzeMode	= ToGroupAnalyzeMode;
			m_lvResult			= lvResult;
			
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
		//					BACKGROUNDWORKER: АНАЛИЗ ФАЙЛОВ
		// =============================================================================================
		#region BackgroundWorker: Анализ файлов
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
			switch( m_GroupAnalyzeMode) {
				case GroupAnalyzeMode.AllGroup:
					this.Text += ": Во всех Группах копий книг";
					switch( m_AnalyzeMode) {
						case CompareMode.Version:
							// пометить в каждой группе все "старые" книги (по тэгу version)
							this.Text += " (по версии)";
							CheckAllOldBooksInAllGroups(CompareMode.Version, ref m_bw, ref e);
							break;
						case CompareMode.CreationTime:
							// пометить в каждой группе все "старые" книги (по времени создания файла)
							this.Text += " (по времени создания)";
							CheckAllOldBooksInAllGroups(CompareMode.CreationTime, ref m_bw, ref e);
							break;
						case CompareMode.LastWriteTime:
							// пометить в каждой группе все "старые" книги (по времени последнего изменения файла)
							this.Text += " (по времени последнего изменения)";
							CheckAllOldBooksInAllGroups(CompareMode.LastWriteTime, ref m_bw, ref e);
							break;
						default:
							return;
					}
					break;
				case GroupAnalyzeMode.Group:
					this.Text += ": В выбранной Группе копий книг";
					switch( m_AnalyzeMode) {
						case CompareMode.Version:
							// пометить в выбранной группе все "старые" книги (по тэгу version)
							this.Text += " (по версии)";
							CheckAllOldBooksInGroup(CompareMode.Version, ref m_bw, ref e);
							break;
						case CompareMode.CreationTime:
							// пометить в выбранной группе все "старые" книги (по времени создания файла)
							this.Text += " (по времени создания)";
							CheckAllOldBooksInGroup(CompareMode.CreationTime, ref m_bw, ref e);
							break;
						case CompareMode.LastWriteTime:
							// пометить в выбранной группе все "старые" книги (по времени последнего изменения файла)
							this.Text += " (по времени последнего изменения)";
							CheckAllOldBooksInGroup(CompareMode.LastWriteTime, ref m_bw, ref e);
							break;
						default:
							return;
					}
					break;
				default:
					return;
			}

			if( ( m_bw.CancellationPending ) ) {
				e.Cancel = true;
				return;
			}
			
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
				m_EndMode.Message = "Анализ книг прерван и не выполнен до конца!\nЗатрачено времени: "+sTime;
			} else if( e.Error != null ) {
				m_EndMode.EndMode = EndWorkModeEnum.Error;
				m_EndMode.Message = "Ошибка:\n" + e.Error.Message + "\n" + e.Error.StackTrace + "\nЗатрачено времени: "+sTime;
			} else {
				m_EndMode.EndMode = EndWorkModeEnum.Done;
				m_EndMode.Message = "Анализ fb2-файлов завершен!\nЗатрачено времени: "+sTime;
			}
			this.Close();
			#endregion
		}
		#endregion
		
		// =============================================================================================
		// 										Анализатор копий книг
		// =============================================================================================
		#region Анализатор копий книг
		// пометить в выбранной группе все "старые" книги
		private void CheckAllOldBooksInGroup(CompareMode mode, ref BackgroundWorker bw, ref DoWorkEventArgs e)
		{
			ListView.SelectedListViewItemCollection si = m_lvResult.SelectedItems;
			if (si.Count > 0) {
				// группа для выделенной книги
				ListViewGroup lvg = si[0].Group;
				MiscListView.CheckAllListViewItemsInGroup( lvg, false );
				_CheckAllOldBooksInGroup(mode, lvg, false, ref bw, ref e);
			}
		}
		
		// пометить в каждой группе все "старые" книги
		private void CheckAllOldBooksInAllGroups(CompareMode mode, ref BackgroundWorker bw, ref DoWorkEventArgs e)
		{
			int iter = 0;
			ProgressBar.Maximum	= m_lvResult.Groups.Count;
			MiscListView.UnCheckAllListViewItems( m_lvResult.CheckedItems );
			// перебор всех групп
			foreach( ListViewGroup lvg in m_lvResult.Groups ) {
				if( ( bw.CancellationPending ) )  {
					e.Cancel = true;
					return;
				}
				// перебор всех книг в выбранной группе
				_CheckAllOldBooksInGroup(mode, lvg, true, ref bw, ref e);
				bw.ReportProgress( ++iter );
			}
		}
		
		// пометка более "старых" книг
		private void _CheckAllOldBooksInGroup(CompareMode mode, ListViewGroup lvGroup, bool InAllGroups,
		                                      ref BackgroundWorker bw, ref DoWorkEventArgs e)
		{
			#region Код
			int iter = 0;
			if( !InAllGroups )
				ProgressBar.Maximum	= 2 * lvGroup.Items.Count;
			// перебор всех книг в выбранной группе
			FB2BookInfo bookInfo = new FB2BookInfo();
			DateTime dt;
			foreach( ListViewItem lvi in lvGroup.Items ) {
				if( ( bw.CancellationPending ) )  {
					e.Cancel = true;
					return;
				}
				if (lvi.SubItems[(int)ResultViewCollumn.Version].Text != string.Empty) {
					switch( mode) {
						case CompareMode.Version:
							// у какой книги версия более поздняя
							if ( bookInfo.Version.Replace('.', ',').CompareTo(lvi.SubItems[(int)ResultViewCollumn.Version].Text.Replace('.', ',')) < 0 ) {
								// если текущая книга более новая
								bookInfo.Version = lvi.SubItems[(int)ResultViewCollumn.Version].Text;
								bookInfo.IndexVersion = lvi.Index;
							}
							break;
						case CompareMode.CreationTime:
							// какой файл позднее создан
							dt = Convert.ToDateTime(lvi.SubItems[(int)ResultViewCollumn.CreationTime].Text);
							if ( bookInfo.CreationTime.CompareTo(dt) < 0 ) {
								bookInfo.CreationTime = dt;
								bookInfo.IndexCreationTime = lvi.Index;
							}
							break;
						case CompareMode.LastWriteTime:
							// какой файл позднее правился
							dt = Convert.ToDateTime(lvi.SubItems[(int)ResultViewCollumn.LastWriteTime].Text);
							if ( bookInfo.LastWriteTime.CompareTo(dt) < 0 ) {
								bookInfo.LastWriteTime = dt;
								bookInfo.IndexLastWriteTime = lvi.Index;
							}
							break;
					}
				}
				if( !InAllGroups )
					bw.ReportProgress( ++iter );
			}
			
			// помечаем все книги в группе, кроме самой "новой"
			foreach (ListViewItem item in lvGroup.Items ) {
				if( ( bw.CancellationPending ) )  {
					e.Cancel = true;
					return;
				}
				switch( mode) {
					case CompareMode.Version:
						if (item.Index != bookInfo.IndexVersion)
							m_lvResult.Items[item.Index].Checked = true;
						break;
					case CompareMode.CreationTime:
						if (item.Index != bookInfo.IndexCreationTime)
							m_lvResult.Items[item.Index].Checked = true;
						break;
					case CompareMode.LastWriteTime:
						if (item.Index != bookInfo.IndexLastWriteTime)
							m_lvResult.Items[item.Index].Checked = true;
						break;
				}
				if( !InAllGroups )
					bw.ReportProgress( ++iter );
			}
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
