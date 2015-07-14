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
using System.Text.RegularExpressions;
using System.IO;

using Core.Misc;

using EndWorkMode = Core.Misc.EndWorkMode;
using filesWorker = Core.Misc.FilesWorker;

// enums
using EndWorkModeEnum	= Core.Misc.Enums.EndWorkModeEnum;
using DuplWorkMode		= Core.Misc.Enums.DuplWorkMode;
using MiscListView		= Core.Misc.MiscListView;

namespace Core.Duplicator
{
	/// <summary>
	/// Обработка помеченных fb2-файлов: Copy / Move / Delete
	/// </summary>
	public partial class CopyMoveDeleteForm : Form
	{
		#region Designer
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CopyMoveDeleteForm));
			this.ControlPanel = new System.Windows.Forms.Panel();
			this.btnStop = new System.Windows.Forms.Button();
			this.ProgressPanel = new System.Windows.Forms.Panel();
			this.ProgressBar = new System.Windows.Forms.ProgressBar();
			this.ControlPanel.SuspendLayout();
			this.ProgressPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// ControlPanel
			// 
			this.ControlPanel.Controls.Add(this.btnStop);
			this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.ControlPanel.Location = new System.Drawing.Point(684, 0);
			this.ControlPanel.Margin = new System.Windows.Forms.Padding(4);
			this.ControlPanel.Name = "ControlPanel";
			this.ControlPanel.Size = new System.Drawing.Size(208, 73);
			this.ControlPanel.TabIndex = 3;
			// 
			// btnStop
			// 
			this.btnStop.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
			this.btnStop.Location = new System.Drawing.Point(0, 0);
			this.btnStop.Margin = new System.Windows.Forms.Padding(4);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(208, 71);
			this.btnStop.TabIndex = 0;
			this.btnStop.Text = "Прервать";
			this.btnStop.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.BtnStopClick);
			// 
			// ProgressPanel
			// 
			this.ProgressPanel.Controls.Add(this.ProgressBar);
			this.ProgressPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.ProgressPanel.Location = new System.Drawing.Point(0, 0);
			this.ProgressPanel.Margin = new System.Windows.Forms.Padding(4);
			this.ProgressPanel.Name = "ProgressPanel";
			this.ProgressPanel.Size = new System.Drawing.Size(688, 73);
			this.ProgressPanel.TabIndex = 2;
			// 
			// ProgressBar
			// 
			this.ProgressBar.Location = new System.Drawing.Point(16, 25);
			this.ProgressBar.Margin = new System.Windows.Forms.Padding(4);
			this.ProgressBar.Name = "ProgressBar";
			this.ProgressBar.Size = new System.Drawing.Size(653, 28);
			this.ProgressBar.TabIndex = 0;
			// 
			// CopyMoveDeleteForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(892, 73);
			this.ControlBox = false;
			this.Controls.Add(this.ControlPanel);
			this.Controls.Add(this.ProgressPanel);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(910, 118);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(910, 118);
			this.Name = "CopyMoveDeleteForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Работа с помеченными копиями книг";
			this.ControlPanel.ResumeLayout(false);
			this.ProgressPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		private System.Windows.Forms.ProgressBar ProgressBar;
		private System.Windows.Forms.Panel ProgressPanel;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Panel ControlPanel;
		#endregion
		
		#region Закрытые данные класса
		private readonly bool m_Fast				= true; // false - с удаление итемов списка копий (медленно). true - без удаления (быстро)
		private readonly ListView m_lvFilesCount	= new ListView();
		private readonly ListView m_lvResult		= new ListView();
		
		
		private int	m_AllFiles		= 0;
		private int	m_AllFB2Files	= 0;
		private int	m_AllArchives	= 0;
		
		private readonly string	m_SourceDir			= string.Empty;
		private readonly string	m_TargetDir			= string.Empty;
		private readonly bool	m_viewProgressStatus = false;
		private readonly int	m_FileExistMode		= 1; // добавить к создаваемому fb2-файлу очередной номер
		private readonly MiscListView	m_mscLV		= new MiscListView(); // класс по работе с ListView
		private readonly EndWorkMode	m_EndMode	= new EndWorkMode();
		private readonly DuplWorkMode	m_WorkMode;  // режим обработки книг
		private bool m_bFilesWorked			= false; // флаг = true, если хоть один файл был на диске и был обработан (copy, move или delete)
		private BackgroundWorker m_bwcmd	= null;  // фоновый обработчик
		#endregion
		
		public CopyMoveDeleteForm( bool Fast, DuplWorkMode WorkMode, string Source, string TargetDir, int FileExistMode,
		                          ListView lvFilesCount, ListView lvResult, bool viewProgressStatus )
		{
			InitializeComponent();
			
			m_Fast			= Fast;
			m_lvResult		= lvResult;
			m_lvFilesCount	= lvFilesCount;
			m_SourceDir		= Source;
			m_TargetDir		= TargetDir;
			m_FileExistMode = FileExistMode;
			m_WorkMode		= WorkMode;
			m_viewProgressStatus = viewProgressStatus;
			
			m_AllFiles = Convert.ToInt16( m_lvFilesCount.Items[1].SubItems[1].Text );
			m_AllFB2Files = Convert.ToInt16( m_lvFilesCount.Items[2].SubItems[1].Text );
			m_AllArchives = Convert.ToInt16( m_lvFilesCount.Items[3].SubItems[1].Text );
			
			ProgressBar.Maximum = m_lvResult.CheckedItems.Count;
			ProgressBar.Value	= 0;
			
			InitializeCopyMovedeleteWorkerBackgroundWorker();
			if( !m_bwcmd.IsBusy )
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
			switch( m_WorkMode ) {
				case DuplWorkMode.CopyCheckedBooks:
					this.Text = "Копирование помеченных копий книг в папку " + m_TargetDir;
					CopyOrMoveCheckedFilesTo( ref m_bwcmd, ref e, true,
					                         m_SourceDir, m_TargetDir, m_lvResult,
					                         m_FileExistMode );
					break;
				case DuplWorkMode.MoveCheckedBooks:
					this.Text = "Перемещение помеченных копий книг в папку " + m_TargetDir;
					CopyOrMoveCheckedFilesTo( ref m_bwcmd, ref e, false,
					                         m_SourceDir, m_TargetDir, m_lvResult,
					                         m_FileExistMode );
					break;
				case DuplWorkMode.DeleteCheckedBooks:
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
			if( m_viewProgressStatus )
				newGroupItemsCount( m_lvResult, m_lvFilesCount );
			++ProgressBar.Value;
		}
		
		// Завершение работы Обработчика Файлов
		private void bwcmd_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {
			string sMessCanceled, sMessError, sMessDone;
			sMessCanceled = sMessError = sMessDone = string.Empty;
			switch( m_WorkMode ) {
				case DuplWorkMode.CopyCheckedBooks:
					sMessDone 		= "Копирование файлов в указанную папку завершено!";
					sMessCanceled	= "Копирование файлов в указанную папку остановлено!";
					break;
				case DuplWorkMode.MoveCheckedBooks:
					sMessDone 		= "Перемещение файлов в указанную папку завершено!";
					sMessCanceled	= "Перемещение файлов в указанную папку остановлено!";
					break;
				case DuplWorkMode.DeleteCheckedBooks:
					sMessDone 		= "Удаление файлов из папки-источника завершено!";
					sMessCanceled	= "Удаление файлов из папки-источника остановлено!";
					break;
			}
			
			if( !m_bFilesWorked ) {
				const string s = "На диске не найдено ни одного файла из помеченных!\n";
				switch( m_WorkMode ) {
					case DuplWorkMode.CopyCheckedBooks:
						sMessDone = s + "Копирование файлов в указанную папку не произведено!";
						break;
					case DuplWorkMode.MoveCheckedBooks:
						sMessDone = s + "Перемещение файлов в указанную папку не произведено!";
						break;
					case DuplWorkMode.DeleteCheckedBooks:
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
			// реальное значение всех Групп и всех копий книг в этих Группах
			RealGroupsAndBooks( m_lvResult, m_lvFilesCount );
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
			#region Код
			string Ext = string.Empty;
			int i = 0;
			ListView.CheckedListViewItemCollection checkedItems = lvResult.CheckedItems;
			foreach( ListViewItem lvi in checkedItems ) {
				// Проверить флаг на остановку процесса
				if( ( bw.CancellationPending ) ) {
					e.Cancel = true; // Выставить окончание - по отмене, сработает событие bwcmd_RunWorkerCompleted
					return;
				} else {
					string FilePath = lvi.Text;
					// есть ли такая книга на диске? Если нет - то смотрим следующую
					Regex rx = new Regex( @"\\+" );
					FilePath = rx.Replace( FilePath, "\\" );
					if( File.Exists( FilePath ) ) {
						string NewPath = TargetDir + FilePath.Remove( 0, SourceDir.Length );
						FileInfo fi = new FileInfo( NewPath );
						if( !fi.Directory.Exists )
							Directory.CreateDirectory( fi.Directory.ToString() );

						if( File.Exists( NewPath ) ) {
							if( nFileExistMode == 0 )
								File.Delete( NewPath );
							else
								NewPath = filesWorker.createFilePathWithSufix( NewPath, nFileExistMode );
						}

						if( IsCopy )
							File.Copy( FilePath, NewPath );
						else {
							File.Move( FilePath, NewPath );
							
							// для отображения числа файлов на диске
							Ext = Path.GetExtension( FilePath ).ToLower();
							if( Ext == ".fb2" )
								--m_AllFB2Files;
							else if( Ext == ".zip" || Ext == ".fbz" )
								--m_AllArchives;
							--m_AllFiles;
							
							if( !m_Fast ) 
								lvResult.Items.Remove( lvi );
							else {
								// пометка цветом и зачеркиванием удаленных книг с диска, но не из списка (быстрый режим удаления)
								MarkRemoverFileInCopyesList( lvi );
							}
						}
						m_bFilesWorked |= true;
						bw.ReportProgress( ++i ); // отобразим данные в контролах
					}
				}
			}
			#endregion
		}
		
		// удалить помеченные файлы...
		public void DeleteCheckedFiles( ref BackgroundWorker bw, ref DoWorkEventArgs e, ListView lvResult ) {
			#region Код
			string Ext = string.Empty;
			int i = 0;
			ListView.CheckedListViewItemCollection checkedItems = lvResult.CheckedItems;
			foreach( ListViewItem lvi in checkedItems ) {
				// Проверить флаг на остановку процесса
				if( ( bw.CancellationPending ) ) {
					e.Cancel = true; // Выставить окончание - по отмене, сработает событие bwcmd_RunWorkerCompleted
					return;
				} else {
					string sFilePath = lvi.Text;
					if( File.Exists( sFilePath) ) {
						File.Delete( sFilePath );
						
						// для отображения числа файлов на диске
						Ext = Path.GetExtension( sFilePath ).ToLower();
						if( Ext == ".fb2" )
							--m_AllFB2Files;
						else if( Ext == ".zip" || Ext == ".fbz" )
							--m_AllArchives;
						--m_AllFiles;
						
						if( !m_Fast ) {
							if( lvResult.Items.Count > 0 )
								lvResult.Items.Remove( lvi );
						} else {
							// пометка цветом и зачеркиванием удаленных книг с диска, но не из списка (быстрый режим удаления)
							MarkRemoverFileInCopyesList( lvi );
						}
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
		// обновление числа групп и книг во всех группах
		private void newGroupItemsCount( ListView lvResult, ListView lvFilesCount ) {
			// новое число групп
			MiscListView.ListViewStatus( lvFilesCount, 5, lvResult.Groups.Count.ToString() );
			// число книг во всех группах
			MiscListView.ListViewStatus( lvFilesCount, 6, lvResult.Items.Count.ToString() );
			// реальное число всех файлов
			MiscListView.ListViewStatus( lvFilesCount, 1, m_AllFiles.ToString() );
			// реальное число всех fb2 файлов
			MiscListView.ListViewStatus( lvFilesCount, 2, m_AllFB2Files.ToString() );
			// реальное число всех архивов
			MiscListView.ListViewStatus( lvFilesCount, 3, m_AllArchives.ToString() );
		}
		// реальное значение всех Групп и всех копий книг в этих Группах
		private void RealGroupsAndBooks( ListView lvResult, ListView lvFilesCount ) {
			int AllGroups = 0;
			int AllBooks = 0;
			foreach (ListViewGroup lvGroup in lvResult.Groups ) {
				int RealBookInGroup = 0;
				if( lvGroup.Items.Count > 1 ) {
					foreach( ListViewItem lvi in lvGroup.Items ) {
						if( !lvi.Font.Strikeout )
							++RealBookInGroup;
					}
				}
				if( RealBookInGroup > 1 ) {
					AllBooks += RealBookInGroup;
					++AllGroups;
				}
			}
			// реальное число групп копий
			MiscListView.ListViewStatus( lvFilesCount, 5, AllGroups.ToString() );
			// реальное число копий книг во всех группах
			MiscListView.ListViewStatus( lvFilesCount, 6, AllBooks.ToString() );
			// реальное число всех файлов
			MiscListView.ListViewStatus( lvFilesCount, 1, m_AllFiles.ToString() );
			// реальное число всех fb2 файлов
			MiscListView.ListViewStatus( lvFilesCount, 2, m_AllFB2Files.ToString() );
			// реальное число всех архивов
			MiscListView.ListViewStatus( lvFilesCount, 3, m_AllArchives.ToString() );
		}
		
		// пометка цветом и зачеркиванием удаленных книг с диска, но не из списка (быстрый режим удаления)
		private void MarkRemoverFileInCopyesList( ListViewItem lvi ) {
			lvi.BackColor = Color.LightGray;
			lvi.Font = new Font(lvi.Font.Name, lvi.Font.Size, FontStyle.Strikeout);
			lvi.Checked = false;
		}
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
