/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 26.02.2014
 * Time: 9:05
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
using FilesWorker		= Core.Common.FilesWorker;
using SharpZipLibWorker = Core.Common.SharpZipLibWorker;
using EndWorkMode 		= Core.Common.EndWorkMode;
using Colors			= Core.Common.Colors;

// enums
using GroupAnalyzeModeEnum		= Core.Common.Enums.GroupAnalyzeModeEnum;
using EndWorkModeEnum			= Core.Common.Enums.EndWorkModeEnum;
using ResultViewDupCollumnEnum	= Core.Common.Enums.ResultViewDupCollumnEnum;

namespace Core.Duplicator
{
	/// <summary>
	/// Групповая валидация fb2-файлов Дубликатора
	/// </summary>
	public partial class ValidatorForm : Form
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ValidatorForm));
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
			this.ControlPanel.TabIndex = 5;
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
			this.ProgressPanel.TabIndex = 4;
			// 
			// ProgressBar
			// 
			this.ProgressBar.Location = new System.Drawing.Point(16, 18);
			this.ProgressBar.Margin = new System.Windows.Forms.Padding(4);
			this.ProgressBar.Name = "ProgressBar";
			this.ProgressBar.Size = new System.Drawing.Size(653, 42);
			this.ProgressBar.TabIndex = 0;
			// 
			// ValidatorForm
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
			this.Name = "ValidatorForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Проверка fb2-файлов на валидность";
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
		private readonly GroupAnalyzeModeEnum	m_GroupAnalyzeMode = GroupAnalyzeModeEnum.Group;
		private readonly ListView			m_lvResult		= new ListView();
		private readonly DateTime			m_dtStart		= DateTime.Now;
		private readonly string				m_TempDir		= Settings.Settings.TempDirPath;
		private readonly SharpZipLibWorker	m_sharpZipLib	= new SharpZipLibWorker();
		private readonly EndWorkMode		m_EndMode 		= new EndWorkMode();
		
		private BackgroundWorker m_bw	= null;
		#endregion
		
		public ValidatorForm( GroupAnalyzeModeEnum groupAnalyzeMode, ListView lvResult )
		{
			InitializeComponent();
			
			m_GroupAnalyzeMode	= groupAnalyzeMode;
			m_lvResult			= lvResult;
			
			InitializeBackgroundWorker();
			if( !m_bw.IsBusy )
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
			FB2Validator fv2Validator = new FB2Validator();
			m_lvResult.BeginUpdate();
			if( m_GroupAnalyzeMode == GroupAnalyzeModeEnum.AllGroup ) {
				// все книги всех Групп
				this.Text += ": Все книги всех Групп";
				ProgressBar.Maximum = m_lvResult.Items.Count;
				ProgressBar.Value	= 0;
				int i = 0;
				foreach ( ListViewItem lvi in m_lvResult.Items ) {
					if ( ( m_bw.CancellationPending ) ) {
						e.Cancel = true;
						return;
					}
					validateFile( lvi, ref fv2Validator );
					m_bw.ReportProgress( ++i );
					ProgressBar.Update();
				}
			} else {
				// все книги выбранной Группы
				ListView.SelectedListViewItemCollection si = m_lvResult.SelectedItems;
				this.Text += ": Все книги Группы '" + si[0].Group.Header + "'";
				if (si.Count > 0) {
					// группа для выделенной книги
					ListViewGroup lvg = si[0].Group;
					ProgressBar.Maximum = lvg.Items.Count;
					ProgressBar.Value	= 0;
					int i = 0;
					foreach ( ListViewItem lvi in lvg.Items ) {
						if( ( m_bw.CancellationPending ) ) {
							e.Cancel = true;
							return;
						}
						validateFile( lvi, ref fv2Validator );
						m_bw.ReportProgress( ++i );
						ProgressBar.Update();
					}
				}
			}
			m_lvResult.EndUpdate();
		}
		
		// Отобразим результат Валидации
		private void bw_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
			++ProgressBar.Value;
		}
		
		// Завершение работы Обработчика Файлов
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
			string FilePath = lvi.SubItems[0].Text;
			if ( File.Exists( FilePath ) ) {
				string Msg = fv2Validator.ValidatingFB2File( FilePath );
				lvi.SubItems[(int)ResultViewDupCollumnEnum.Validate].Text = Msg == string.Empty ? "Да" : "Нет";
				if ( !string.IsNullOrEmpty( Msg ) )
					lvi.ForeColor = Colors.FB2NotValidForeColor;
				else
					lvi.ForeColor = FilesWorker.isFB2File( FilePath )
						? Color.FromName( "WindowText" ) : Colors.ZipFB2ForeColor;
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
