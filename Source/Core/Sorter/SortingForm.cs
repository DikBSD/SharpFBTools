/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 19.05.2014
 * Time: 7:26
 * 
 * License: GPL 2.1
 */
using System;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Xml.Linq;

using Core.FB2.Description.TitleInfo;
using Core.FB2.Description.Common;
using Core.FB2.FB2Parsers;
using Core.Common;
using Core.Sorter.Templates;

using Fb2Validator				= Core.FB2Parser.FB2Validator;
using FilesWorker				= Core.Common.FilesWorker;
using TemplatesLexemsSimple		= Core.Sorter.Templates.Lexems.TPSimple;
using SelectedSortQueryCriteria	= Core.Sorter.SortQueryCriteria;
using StatusView 				= Core.Sorter.StatusView;
using SharpZipLibWorker 		= Core.Common.SharpZipLibWorker;
using EndWorkMode 				= Core.Common.EndWorkMode;

// enums
using EndWorkModeEnum	= Core.Common.Enums.EndWorkModeEnum;

namespace Core.Sorter
{
	/// <summary>
	/// SortingForm: Форма прогресса сортировки fb2 книг
	/// </summary>
	public partial class SortingForm : Form
	{
		#region Design
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SortingForm));
			this.ControlPanel = new System.Windows.Forms.Panel();
			this.btnStop = new System.Windows.Forms.Button();
			this.btnSaveToXml = new System.Windows.Forms.Button();
			this.ProgressPanel = new System.Windows.Forms.Panel();
			this.StatusLabel = new System.Windows.Forms.Label();
			this.ProgressBar = new System.Windows.Forms.ProgressBar();
			this.sfdList = new System.Windows.Forms.SaveFileDialog();
			this.ControlPanel.SuspendLayout();
			this.ProgressPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// ControlPanel
			// 
			this.ControlPanel.BackColor = System.Drawing.Color.DarkGray;
			this.ControlPanel.Controls.Add(this.btnStop);
			this.ControlPanel.Controls.Add(this.btnSaveToXml);
			this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.ControlPanel.Location = new System.Drawing.Point(684, 0);
			this.ControlPanel.Margin = new System.Windows.Forms.Padding(4);
			this.ControlPanel.Name = "ControlPanel";
			this.ControlPanel.Size = new System.Drawing.Size(208, 230);
			this.ControlPanel.TabIndex = 3;
			// 
			// btnStop
			// 
			this.btnStop.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
			this.btnStop.Location = new System.Drawing.Point(0, 71);
			this.btnStop.Margin = new System.Windows.Forms.Padding(4);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(208, 71);
			this.btnStop.TabIndex = 2;
			this.btnStop.Text = "Прервать";
			this.btnStop.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.BtnStopClick);
			// 
			// btnSaveToXml
			// 
			this.btnSaveToXml.Dock = System.Windows.Forms.DockStyle.Top;
			this.btnSaveToXml.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnSaveToXml.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveToXml.Image")));
			this.btnSaveToXml.Location = new System.Drawing.Point(0, 0);
			this.btnSaveToXml.Margin = new System.Windows.Forms.Padding(4);
			this.btnSaveToXml.Name = "btnSaveToXml";
			this.btnSaveToXml.Size = new System.Drawing.Size(208, 71);
			this.btnSaveToXml.TabIndex = 0;
			this.btnSaveToXml.Text = "Прервать в файл...";
			this.btnSaveToXml.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnSaveToXml.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnSaveToXml.UseVisualStyleBackColor = true;
			this.btnSaveToXml.Click += new System.EventHandler(this.BtnSaveToXmlClick);
			// 
			// ProgressPanel
			// 
			this.ProgressPanel.Controls.Add(this.StatusLabel);
			this.ProgressPanel.Controls.Add(this.ProgressBar);
			this.ProgressPanel.Dock = System.Windows.Forms.DockStyle.Left;
			this.ProgressPanel.Location = new System.Drawing.Point(0, 0);
			this.ProgressPanel.Margin = new System.Windows.Forms.Padding(4);
			this.ProgressPanel.Name = "ProgressPanel";
			this.ProgressPanel.Size = new System.Drawing.Size(688, 230);
			this.ProgressPanel.TabIndex = 2;
			// 
			// StatusLabel
			// 
			this.StatusLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.StatusLabel.Location = new System.Drawing.Point(19, 62);
			this.StatusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(649, 148);
			this.StatusLabel.TabIndex = 2;
			// 
			// ProgressBar
			// 
			this.ProgressBar.Location = new System.Drawing.Point(16, 16);
			this.ProgressBar.Margin = new System.Windows.Forms.Padding(4);
			this.ProgressBar.Name = "ProgressBar";
			this.ProgressBar.Size = new System.Drawing.Size(653, 28);
			this.ProgressBar.TabIndex = 0;
			// 
			// sfdList
			// 
			this.sfdList.RestoreDirectory = true;
			this.sfdList.Title = "Укажите название файла копий";
			// 
			// SortingForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(892, 230);
			this.ControlBox = false;
			this.Controls.Add(this.ControlPanel);
			this.Controls.Add(this.ProgressPanel);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(910, 248);
			this.Name = "SortingForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Сортировка fb2, fb2.zip и fbz книг";
			this.ControlPanel.ResumeLayout(false);
			this.ProgressPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		private System.Windows.Forms.Label StatusLabel;
		private System.Windows.Forms.SaveFileDialog sfdList;
		private System.Windows.Forms.ProgressBar ProgressBar;
		private System.Windows.Forms.Panel ProgressPanel;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Button btnSaveToXml;
		private System.Windows.Forms.Panel ControlPanel;
		#endregion
		
		#region Закрытые данные класса
		private List<string> m_FilesList	= new List<string>();
		private BackgroundWorker m_bw		= null; // фоновый обработчик для Непрерывной сортировки
		private BackgroundWorker m_bwRenew	= null; // фоновый обработчик для Прерывания сортировки
		
		private string m_sMessTitle		= string.Empty;
		private string m_fromXmlPath	= null;	// null - полное сканирование; Путь - возобновление сравнения их xml
		
		private long _lCounter = 0; // счетчик текущего обрабатываемого файла (для шаблона *COUNTER*)
		private TemplatesParser _templatesParser = new TemplatesParser();
		private List<SelectedSortQueryCriteria> m_lSSQCList		= null; // список критериев поиска для Избранной Сортировки
		private SortingOptions					m_sortOptions	= null; // индивидуальные настройки обоих Сортировщиков, взависимости от режима (непрерывная сортировка или возобновление сортировки)
		
		private ListView m_listViewFB2Files					= new ListView();
		private readonly Fb2Validator		m_fv2V			= new Fb2Validator();
		private readonly StatusView 		m_sv			= new StatusView();
		private readonly SharpZipLibWorker	m_sharpZipLib	= new SharpZipLibWorker();
		private readonly EndWorkMode 		m_EndMode		= new EndWorkMode();
		
		private readonly string m_TempDir	= Settings.Settings.TempDirPath;
		private static int _MaxBookTitleLenght = 40; // Максимальная длина названия книги
		private static int _MaxSequenceLenght = 40;	 // Максимальная длина названия серии
		private static int _MaxPublisherLenght = 40; // Максимальная длина названия издательства
		private bool m_StopToSave			= false; // true, если остановка с сохранением необработанного списка книг в файл.
		private readonly DateTime m_dtStart = DateTime.Now;
		#endregion
		
		private void init(SortingOptions sortOptions, int MaxBTLenght, int MaxSequenceLenght, int MaxPublisherLenght) {
			InitializeComponent();
			initializeBackgroundWorker();
			initializeRenewBackgroundWorker();
			
			_MaxBookTitleLenght = MaxBTLenght;
			_MaxSequenceLenght = MaxSequenceLenght;
			_MaxPublisherLenght = MaxPublisherLenght;

			m_sortOptions	= sortOptions;
			m_lSSQCList		= m_sortOptions.getCriterias();
			
			m_sMessTitle = m_sortOptions.IsFullSort ? "SharpFBTools - Полная Сортировка" : "SharpFBTools - Избранная Сортировка";
			
			// Запуск процесса DoWork от RunWorker
			if ( m_sortOptions.FromXmlFile == null ) {
				if ( !m_bw.IsBusy )
					m_bw.RunWorkerAsync(); //если не занят, то запустить процесс
			} else {
				m_fromXmlPath = m_sortOptions.FromXmlFile; // путь к xml файлу для возобновления сортировки fb2 книг
				if ( !m_bwRenew.IsBusy )
					m_bwRenew.RunWorkerAsync(); //если не занят. то запустить процесс
			}
		}
		
		// Полная сортировка: Беспрерывная и Возобновление
		public SortingForm(
			SortingOptions sortOptions, ListView listViewFB2Files,
			int MaxBTLenght, int MaxSequenceLenght, int MaxPublisherLenght
		)
		{
			m_listViewFB2Files = listViewFB2Files;
			init(sortOptions, MaxBTLenght, MaxSequenceLenght, MaxPublisherLenght);
		}
		
		// Избранная сортировка: Беспрерывная и Возобновление
		public SortingForm(
			SortingOptions sortOptions,
			int MaxBTLenght, int MaxSequenceLenght, int MaxPublisherLenght
		)
		{
			init(sortOptions, MaxBTLenght, MaxSequenceLenght, MaxPublisherLenght);
		}
		
		// =============================================================================================
		// 										ОТКРЫТЫЕ СВОЙСТВА
		// =============================================================================================
		#region Открытые свойства
		public virtual EndWorkMode EndMode {
			get { return m_EndMode; }
		}
		public virtual StatusView Status {
			get { return m_sv; }
		}
		#endregion
		
		// =============================================================================================
		// 										ОТКРЫТЫЕ МЕТОДЫ
		// =============================================================================================
		#region Открытые методы
		public bool isStopToXmlClicked() {
			return m_StopToSave;
		}
		#endregion
		
		// =============================================================================================
		//							BackgroundWorker: Сортировка книг
		// =============================================================================================
		#region BackgroundWorker: Сортировка книг
		// Инициализация перед использование BackgroundWorker
		private void initializeBackgroundWorker() {
			m_bw = new BackgroundWorker();
			m_bw.WorkerReportsProgress		= true; // Позволить выводить прогресс процесса
			m_bw.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
			m_bw.DoWork 			+= new DoWorkEventHandler( bw_DoWork );
			m_bw.ProgressChanged 	+= new ProgressChangedEventHandler( bw_ProgressChanged );
			m_bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler( bw_RunWorkerCompleted );
		}
		
		// сортировка файлов по папкам, согласно шаблонам подстановки
		private void bw_DoWork( object sender, DoWorkEventArgs e ) {
			ControlPanel.Enabled = false;
			StatusLabel.Text += m_sortOptions.IsFullSort ? "Полная сортировка файлов." : "Избранная сортировка файлов.";
			StatusLabel.Text += "\rСоздание списка файлов для сортировки fb2 книг... " ;
//			m_sv.Clear();
//			m_FilesList.Clear();
			List<string> lDirList = new List<string>();
			
			if ( m_sortOptions.IsFullSort ) {
				// ========================================================================
				//                              Полная Сортировка
				// ========================================================================
				// формируем список помеченных папок
				List<string> lCheckedDirList	= new List<string>();
				List<string> lCheckedFileList	= new List<string>();
				ListView.CheckedListViewItemCollection checkedItems = m_listViewFB2Files.CheckedItems;
				foreach ( ListViewItem lvi in checkedItems ) {
					ListViewItemType it = (ListViewItemType)lvi.Tag;
					if ( it.Type.Trim() == "d" )
						lCheckedDirList.Add(it.Value.Trim());
					else if ( it.Type.Trim() == "f" )
						lCheckedFileList.Add(it.Value.Trim());
				}
				
				m_FilesList.AddRange( lCheckedFileList ); // помеченные файлы
				lDirList.Add( m_sortOptions.SourceDir );
				if ( !m_sortOptions.ScanSubDirs ) {
					// сканировать только указанную папку (ее папки, но не их подпапки)
					lDirList.AddRange( lCheckedDirList );
					foreach ( string dir in lCheckedDirList )
						m_FilesList.AddRange( Directory.GetFiles( dir ) );

				} else {
					// сканировать и все подпапки
					foreach ( string dir in lCheckedDirList )
						FilesWorker.DirsFilesParser( m_bw, e, dir, ref lDirList, ref m_FilesList );
				}
				lCheckedDirList.Clear();
				lCheckedFileList.Clear();
			} else {
				// ========================================================================
				//                            Избранная Сортировка
				// ========================================================================
				if ( !m_sortOptions.ScanSubDirs ) {
					// сканировать только указанную папку
					lDirList.Add( m_sortOptions.SourceDir );
					m_FilesList.AddRange( Directory.GetFiles( m_sortOptions.SourceDir ) );
				} else {
					// сканировать и все подпапки
					FilesWorker.DirsFilesParser( m_bw, e, m_sortOptions.SourceDir, ref lDirList, ref m_FilesList );
				}
			}
			m_sv.AllDirs = lDirList.Count;
			m_sv.AllFiles = m_FilesList.Count;
			ControlPanel.Enabled = true;
			
			// Проверить флаг на остановку процесса
			if ( m_bw.CancellationPending == true ) {
				e.Cancel = true;
				return;
			}
			
			// проверка, есть ли хоть один файл в папке для сканирования
			if ( m_FilesList.Count == 0 ) {
				MessageBox.Show(
					"В папке сканирования не найдено ни одного файла!\nРабота прекращена.", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information
				);
				return;
			}
			
			StatusLabel.Text += "Всего " + m_FilesList.Count.ToString() + " файлов.";
			ProgressBar.Maximum	= m_FilesList.Count;
			ProgressBar.Value	= 0;
			lDirList.Clear();
			
			// сортировка книг, в зависимости от критериев поиска и типа Сортировщика
			StatusLabel.Text += "\rЗапущена сортировка книг... ";
			sortBooks( ref m_bw, ref e );
		}
		
		// Отобразим результат сортировки
		private void bw_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
			++ProgressBar.Value;
		}
		
		// Проверяем это отмена, ошибка, или конец задачи и сообщить
		private void bw_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {
			#region Код
			DateTime dtEnd = DateTime.Now;
			FilesWorker.RemoveDir( m_TempDir );
			
			string sTime = dtEnd.Subtract( m_dtStart ).ToString() + " (час.:мин.:сек.)";
			if ( e.Cancelled ) {
				m_EndMode.EndMode = EndWorkModeEnum.Cancelled;
				m_EndMode.Message = "Сортировка остановлена!\nЗатрачено времени: " + sTime;
				if (m_StopToSave) {
					// остановка сортировки с сохранением списка необработанных книг в файл
					m_StopToSave = false;
					// сохранение в xml-файл списка данных о необработанных книгах
					sfdList.Title		= "Укажите файл для будущего возобновления сортировки книг:";
					string filter		= m_sortOptions.IsFullSort ? "*.fullsort_break" : "*.selsort_break";
					sfdList.Filter		= "SharpFBTools Файлы хода работы Сортировщика (" + filter+")|" + filter;
					sfdList.FileName	= string.Empty;
					sfdList.InitialDirectory = Settings.Settings.ProgDir;
					DialogResult result = sfdList.ShowDialog();
					if ( result == DialogResult.OK ) {
						ControlPanel.Enabled = false;
						StatusLabel.Text += "Сохранение списка неотбработанных книг в файл:\r";
						StatusLabel.Text += sfdList.FileName;
						saveBreakDataToXmlFile( sfdList.FileName, ref m_FilesList);
						m_EndMode.Message = "Сортировка fb2 файлов прервана!\nСписок оставшихся для обработки книг сохранены в xml-файл:\n\n" + sfdList.FileName + "\n\nЗатрачено времени: " + sTime;
					}
				}
			} else if ( e.Error != null ) {
				m_EndMode.EndMode = EndWorkModeEnum.Error;
				m_EndMode.Message = "Ошибка:\n" + e.Error.Message + "\n" + e.Error.StackTrace + "\nЗатрачено времени: " + sTime;
			} else {
				m_EndMode.EndMode = EndWorkModeEnum.Done;
				m_EndMode.Message = "Сортировка файлов в указанную папку завершена!\nЗатрачено времени: " + sTime;
			}
//			m_sv.Clear();
//			m_FilesList.Clear();
			this.Close();
			#endregion
		}
		#endregion
		
		// =============================================================================================
		//						BackgroundWorker: Возобновления сортировки книг
		// =============================================================================================
		#region BackgroundWorker: Возобновления сортировки книг
		private void initializeRenewBackgroundWorker() {
			m_bwRenew = new BackgroundWorker();
			m_bwRenew.WorkerReportsProgress			= true; // Позволить выводить прогресс процесса
			m_bwRenew.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
			m_bwRenew.DoWork 			+= new DoWorkEventHandler( m_bwRenew_renewSearchDataFromFile_DoWork );
			m_bwRenew.ProgressChanged 	+= new ProgressChangedEventHandler( bw_ProgressChanged );
			m_bwRenew.RunWorkerCompleted += new RunWorkerCompletedEventHandler( bw_RunWorkerCompleted );
		}
		
		// возобновление сортировки - загрузка отчета о необработанных книгах из xml-файла
		private void m_bwRenew_renewSearchDataFromFile_DoWork( object sender, DoWorkEventArgs e ) {
			ControlPanel.Enabled = false;
			// загрузка данных из xml
			StatusLabel.Text += "Возобновление " + (m_sortOptions.IsFullSort ? "полной" : "избранной") + " сортировки fb2 книг.\r";
			StatusLabel.Text += "Загрузка данных поиска из xml файла:\r";
			StatusLabel.Text += m_fromXmlPath + " ...\r";
			XElement xmlTree = XElement.Load( m_fromXmlPath );

			//загрузка данных о ходе сравнения
			XElement compareData = xmlTree.Element("SortingData");
			if ( compareData.Element("AllDirs") != null )
				m_sv.AllDirs = Convert.ToInt64( compareData.Element("AllDirs").Value );
			if ( compareData.Element("AllFiles") != null )
				m_sv.AllFiles = Convert.ToInt64( compareData.Element("AllFiles").Value );
			if ( compareData.Element("FB2Files") != null )
				m_sv.SourceFB2 = Convert.ToInt64( compareData.Element("FB2Files").Value );
			if ( compareData.Element("Zip") != null )
				m_sv.Zip = Convert.ToInt64( compareData.Element("Zip").Value );
			if ( compareData.Element("FB2FromZips") != null )
				m_sv.FB2FromZips = Convert.ToInt64( compareData.Element("FB2FromZips").Value );
			if ( compareData.Element("Other") != null )
				m_sv.Other = Convert.ToInt64( compareData.Element("Other").Value );
			if ( compareData.Element("CreateInTarget") != null )
				m_sv.CreateInTarget = Convert.ToInt64( compareData.Element("CreateInTarget").Value );
			if ( compareData.Element("NotRead") != null )
				m_sv.NotRead = Convert.ToInt64( compareData.Element("NotRead").Value );
			if ( compareData.Element("NotValidFB2") != null )
				m_sv.NotValidFB2 = Convert.ToInt64( compareData.Element("NotValidFB2").Value );
			if ( compareData.Element("BadZip") != null )
				m_sv.BadZip = Convert.ToInt64( compareData.Element("BadZip").Value );
			if ( compareData.Element("NotSort") != null )
				m_sv.NotSort = Convert.ToInt64( compareData.Element("NotSort").Value );
			if ( compareData.Element("LongPath") != null )
				m_sv.LongPath = Convert.ToInt64( compareData.Element("LongPath").Value );
			if ( compareData.Element("Counter") != null )
				_lCounter = Convert.ToInt64( compareData.Element("Counter").Value );

			// заполнение списка необработанных файлов
			m_FilesList.Clear();
			IEnumerable<XElement> files = xmlTree.Element("NotWorkingFiles").Elements("File");
			foreach ( XElement element in files )
				m_FilesList.Add(element.Value);

			ProgressBar.Maximum	= m_FilesList.Count;
			ProgressBar.Value	= 0;
			// ортировка книг, в зависимости от критериев поиска и типа Сортировщика
			StatusLabel.Text += "Осталось обработать: " + m_FilesList.Count.ToString() + " файлов.";
			StatusLabel.Text += "\rЗапущена сортировка книг... ";
			
			ControlPanel.Enabled = true;
			sortBooks( ref m_bwRenew, ref e );
		}
		#endregion
		
		// =============================================================================================
		// 						Прерывание сортировки: Сохранение данных в xml
		// =============================================================================================
		#region Прерывание сортировки: Сохранение данных в xml
		// сохранение списка необработанных книг при прерывании проверки для записи
		private void saveBreakDataToXmlFile(string ToFileName, ref List<string> FilesList) {
			string type = m_sortOptions.IsFullSort ? "fullsort_break" : "selsort_break";
			XDocument doc = new XDocument(
				new XDeclaration("1.0", "utf-8", "yes"),
				new XComment("Файл сортировки fb2 книг, сохраненный после прерывания работы Сортировщика. Используется для возобновления сортировки"),
				new XElement("Files", new XAttribute("type", type),
				             new XComment("Режим сортировки"),
				             new XElement("SortMode", m_sortOptions.IsFullSort ? "Полная сортировка файлов" : "Избранная сортировка файлов"),
				             new XComment("Шаблоны подстановки"),
				             new XElement("Template", m_sortOptions.Template),
				             new XComment("Папки с файлами"),
				             new XElement("Folders",
				                          new XComment("Папка с книгами для сортировки"),
				                          new XElement("SourceDir", m_sortOptions.SourceDir),
				                          new XComment("Папка с отсортированными книгами"),
				                          new XElement("TargetDir", m_sortOptions.TargetDir)),
				             new XComment("Настройки сортировки"),
				             new XElement("Settings"),
				             new XComment("Опции Сортировщика"),
				             new XElement("Options"),
				             
				             new XComment("Критерии сортировки"),
				             new XElement("Criterias", new XAttribute("count", m_lSSQCList != null ? m_lSSQCList.Count.ToString() : "1")),
				             
				             new XComment("Данные о ходе сортировки"),
				             new XElement("SortingData",
				                          new XElement("AllDirs", m_sv.AllDirs),
				                          new XElement("AllFiles", m_sv.AllFiles),
				                          new XElement("FB2Files", m_sv.SourceFB2),
				                          new XElement("Zip", m_sv.Zip),
				                          new XElement("FB2FromZips", m_sv.FB2FromZips),
				                          new XElement("Other", m_sv.Other),
				                          new XElement("CreateInTarget", m_sv.CreateInTarget),
				                          new XElement("NotRead", m_sv.NotRead),
				                          new XElement("NotValidFB2", m_sv.NotValidFB2),
				                          new XElement("BadZip", m_sv.BadZip),
				                          new XElement("NotSort", m_sv.NotSort),
				                          new XElement("LongPath", m_sv.LongPath),
				                          new XElement("Counter", _lCounter)
				                         ),
				             
				             new XComment("Не обработанные файлы"),
				             new XElement("NotWorkingFiles", new XAttribute("count", FilesList.Count))
				            )
			);
			
			// настройки сортировки
			doc.Root.Element("Settings").Add(
				new XElement("ScanSubDirs", m_sortOptions.ScanSubDirs),
				new XElement("ToZip", m_sortOptions.ToZip),
				new XElement("NotDelOriginalFiles", m_sortOptions.NotDelOriginalFiles)
			);

			// спиок опций Сортировщика
			XElement xeOptions = doc.Root.Element("Options");
			makeCommonOptions( ref xeOptions );
			
			// список критериев
			if ( !m_sortOptions.IsFullSort ) {
				// для Избранной сортировки
				int criteriaNumber = 0;
				foreach ( SortQueryCriteria crit in m_lSSQCList ) {
					doc.Root.Element("Criterias").Add(
						new XElement("Criteria", new XAttribute("number", criteriaNumber++),
						             new XElement("Lang", crit.Lang),
						             new XElement("GenresGroup", crit.GenresGroup),
						             new XElement("Genre", crit.Genre),
						             new XElement("LastName", crit.LastName),
						             new XElement("FirstName", crit.FirstName),
						             new XElement("MiddleName", crit.MiddleName),
						             new XElement("NickName", crit.NickName),
						             new XElement("Sequence", crit.Sequence),
						             new XElement("BookTitle", crit.BookTitle),
						             new XElement("ExactFit", crit.ExactFit)
						            )
					);
				}
			}
			
			// список необработанных книг
			if ( FilesList.Count > 0 ) {
				ProgressBar.Maximum	= FilesList.Count;
				ProgressBar.Value = 0;
				int fileNumber = 0;
				for (int i=0; i!=FilesList.Count; ++i) {
					doc.Root.Element("NotWorkingFiles").Add(
						new XElement("File", new XAttribute("number", fileNumber++),
						             new XElement("Path", FilesList[i])
						            )
					);
					++ProgressBar.Value;
				}
			}
			doc.Save(ToFileName);
		}
		
		// сохранение настроек в xml-файл
		private void makeCommonOptions( ref XElement xeOptions ) {
			xeOptions.Add(
				new XComment("Основные настройки"),
				new XElement("General",
				             new XComment("Регистр имени файла"),
				             new XElement("Register",
				                          new XAttribute("AsIs", m_sortOptions.RegisterAsIs),
				                          new XAttribute("Lower", m_sortOptions.RegisterLower),
				                          new XAttribute("Upper", m_sortOptions.RegisterUpper),
				                          new XAttribute("AsSentence", m_sortOptions.RegisterAsSentence)
				                         ),
				             new XComment("Транслитерация имен файлов"),
				             new XElement("Translit", m_sortOptions.Translit),
				             new XComment("'Строгие' имена файлов: алфавитно-цифровые символы, а так же [](){}-_"),
				             new XElement("Strict", m_sortOptions.Strict),
				             new XComment("Обработка пробелов"),
				             new XElement("Space", new XAttribute("index",  m_sortOptions.Space)),
				             new XComment("Одинаковые файлы"),
				             new XElement("FileExistMode", new XAttribute("index", m_sortOptions.FileExistMode)),
				             new XComment("Сортировка файлов"),
				             new XElement("SortType",
				                          new XAttribute("AllFB2", m_sortOptions.SortTypeAllFB2),
				                          new XAttribute("OnlyValidFB2", m_sortOptions.SortTypeOnlyValidFB2)
				                         ),
				             new XComment("Раскладка файлов по папкам"),
				             new XElement("FilesToDirs",
				                          new XComment("По Авторам"),
				                          new XElement("AuthorsToDirs",
				                                       new XAttribute("AuthorOne", m_sortOptions.AuthorsToDirsAuthorOne),
				                                       new XAttribute("AuthorAll", m_sortOptions.AuthorsToDirsAuthorAll)
				                                      ),
				                          new XComment("По Жанрам"),
				                          new XElement("GenresToDirs",
				                                       new XAttribute("GenreOne", m_sortOptions.GenresToDirsGenreOne),
				                                       new XAttribute("GenreAll", m_sortOptions.GenresToDirsGenreAll)
				                                      ),
				                          new XComment("Вид папки-Жанра"),
				                          new XElement("GenresType",
				                                       new XAttribute("GenreSchema", m_sortOptions.GenresTypeGenreSchema),
				                                       new XAttribute("GenreText", m_sortOptions.GenresTypeGenreText)
				                                      )
				                         )
				            ),
				new XComment("Папки шаблонного тэга без данных"),
				new XElement("NoTags",
				             new XComment("Описание Книги"),
				             new XElement("BookInfo",
				                          new XElement("NoGenreGroup", m_sortOptions.BookInfoNoGenreGroup),
				                          new XElement("NoGenre", m_sortOptions.BookInfoNoGenre),
				                          new XElement("NoLang", m_sortOptions.BookInfoNoLang),
				                          new XElement("NoFirstName", m_sortOptions.BookInfoNoFirstName),
				                          new XElement("NoMiddleName", m_sortOptions.BookInfoNoMiddleName),
				                          new XElement("NoLastName", m_sortOptions.BookInfoNoLastName),
				                          new XElement("NoNickName", m_sortOptions.BookInfoNoNickName),
				                          new XElement("NoBookTitle", m_sortOptions.BookInfoNoBookTitle),
				                          new XElement("NoSequence", m_sortOptions.BookInfoNoSequence),
				                          new XElement("NoNSequence", m_sortOptions.BookInfoNoNSequence),
				                          new XElement("NoDateText", m_sortOptions.BookInfoNoDateText),
				                          new XElement("NoDateValue", m_sortOptions.BookInfoNoDateValue)
				                         ),
				             new XComment("Издательство"),
				             new XElement("PublishInfo",
				                          new XElement("NoPublisher", m_sortOptions.PublishInfoNoPublisher),
				                          new XElement("NoYear", m_sortOptions.PublishInfoNoYear),
				                          new XElement("NoCity", m_sortOptions.PublishInfoNoCity)
				                         ),
				             new XComment("Данные о создателе fb2 файла"),
				             new XElement("FB2Info",
				                          new XElement("NoFB2FirstName", m_sortOptions.FB2InfoNoFB2FirstName),
				                          new XElement("NoFB2MiddleName", m_sortOptions.FB2InfoNoFB2MiddleName),
				                          new XElement("NoFB2LastName", m_sortOptions.FB2InfoNoFB2LastName),
				                          new XElement("NoFB2NickName", m_sortOptions.FB2InfoNoFB2NickName)
				                         )
				            )
			);
		}
		#endregion
		
		#region Закрытые вспомогательные методы класса
		// сортировка книг, в зависимости от критериев поиска и типа Сортировщика
		private void sortBooks(ref BackgroundWorker bw, ref DoWorkEventArgs e) {
			// формируем лексемы шаблонной строки
			List<TemplatesLexemsSimple> lSLexems = _templatesParser.GemSimpleLexems( m_sortOptions.Template );
			// папки для проблемных файлов
			string TargetDir = m_sortOptions.TargetDir;
			m_sortOptions.NotReadFB2Dir		= Path.Combine( TargetDir, m_sortOptions.NotReadFB2Dir );
			m_sortOptions.FileLongPathDir	= Path.Combine( TargetDir, m_sortOptions.FileLongPathDir );
			m_sortOptions.NotValidFB2Dir	= Path.Combine( TargetDir, m_sortOptions.NotValidFB2Dir );
			m_sortOptions.NotOpenArchDir	= Path.Combine( TargetDir, m_sortOptions.NotOpenArchDir );
			
			List<string> FinishedFilesList = new List<string>();
			
			if (m_sortOptions.IsFullSort) {
				// ========================================================================
				//                              Полная Сортировка
				// ========================================================================
				for (int i = 0; i != m_FilesList.Count; ++i) {
					// Проверить флаг на остановку процесса
					if (bw.CancellationPending == true) {
						// удаление из списка всех файлов обработанные книги (файлы)
						removeFinishedFilesInFilesList( ref m_FilesList, ref FinishedFilesList);
						e.Cancel = true;
						return;
					}
					// создание отсортированного fb2 по Жанру(ам) и Автору(ам)
					fileFullSorting(m_FilesList[i], lSLexems, m_sortOptions);
					
					FinishedFilesList.Add(m_FilesList[i]); // обработанные файлы
					FilesWorker.RemoveDir(m_TempDir);
					bw.ReportProgress(i); // отобразим данные в контролах
				}
				
				// удаление из списка всех файлов обработанные книги (файлы)
				removeFinishedFilesInFilesList( ref m_FilesList, ref FinishedFilesList);
			} else {
				// ========================================================================
				//                            Избранная Сортировка
				// ========================================================================
				for (int i = 0; i != m_FilesList.Count; ++i) {
					// Проверить флаг на остановку процесса
					if (bw.CancellationPending == true) {
						// удаление из списка всех файлов обработанные книги (файлы)
						removeFinishedFilesInFilesList(ref m_FilesList, ref FinishedFilesList);
						e.Cancel = true;
						return;
					}
					// создаем отсортированный fb2 файл по новому пути
					fileSelectedSorting(m_FilesList[i], lSLexems, m_sortOptions);
					
					FinishedFilesList.Add(m_FilesList[i]); // обработанные файлы
					FilesWorker.RemoveDir(m_TempDir);
					bw.ReportProgress(i); // отобразим данные в контролах
				}
				// удаление из списка всех файлов обработанные книги (файлы)
				removeFinishedFilesInFilesList(ref m_FilesList, ref FinishedFilesList);
			}
		}
		
		//==============================================================================================================
		//											Полная Сортировка
		//==============================================================================================================
		private void fileFullSorting(string FromFilePath, List<TemplatesLexemsSimple> lSLexems, SortingOptions m_sortOptions) {
			if (FilesWorker.isFB2File(FromFilePath)) {
				// создание файла по новому пути для Жанра(ов) и Автора(ов) Книги из исходного fb2
				makeFileForGenreAndAuthorFromFB2(
					false, FromFilePath, lSLexems, m_sortOptions,
					null /* Группа будет определяться исходя из Жанра из fb2 */
				);
				++m_sv.SourceFB2;
				
				// удаляем исходный fb2-файл, если включена эта опция
				if (!m_sortOptions.NotDelOriginalFiles) {
					if ( File.Exists(FromFilePath))
						File.Delete(FromFilePath);
				}
			} else if (FilesWorker.isFB2Archive(FromFilePath)) {
				long UnZipCount = m_sharpZipLib.UnZipFB2Files(FromFilePath, m_TempDir);
				List<string> FilesListFromZip = FilesWorker.MakeFileListFromDir(m_TempDir, false, true);
				++m_sv.Zip;

				if (UnZipCount == -1) {
					// не получилось открыть архив - "битый"
					copyBadZipToBadDir(FromFilePath, m_sortOptions.SourceDir, m_sortOptions.NotOpenArchDir, m_sortOptions.FileExistMode);
					++m_sv.BadZip;
					return;
				} else {
					if (FilesListFromZip == null) {
						// в архиве нет fb2-файлов
						copyBadZipToBadDir(FromFilePath, m_sortOptions.SourceDir, m_sortOptions.NotOpenArchDir, m_sortOptions.FileExistMode);
						++m_sv.BadZip;
						return;
					}
					foreach (string FB2FromArchPath in FilesListFromZip) {
						// создание файла по новому пути для Жанра(ов) и Автора(ов) Книги из исходного fb2
						makeFileForGenreAndAuthorFromFB2(
							true, FB2FromArchPath, lSLexems, m_sortOptions,
							null /* Группа будет определяться исходя из Жанра из fb2 */
						);
						++m_sv.FB2FromZips;
					}
					
					// очистка временной папки
					foreach (string FB2FromArchPath in FilesListFromZip) {
						if (File.Exists(FB2FromArchPath))
							File.Delete(FB2FromArchPath);
					}
				}
				
				// удаляем исходный zip-файл, если включена эта опция
				if (!m_sortOptions.NotDelOriginalFiles) {
					if (File.Exists(FromFilePath))
						File.Delete(FromFilePath);
				}

			} else {
				// пропускаем не fb2-файлы и не zip-архивы
				++m_sv.Other;
			}
		}
		
		//============================================================================================================
		// 											Избранная Сортировка
		//============================================================================================================
		private void fileSelectedSorting(string FromFilePath, List<TemplatesLexemsSimple> lSLexems, SortingOptions m_sortOptions) {
			bool IsNotRead = false;
			if (FilesWorker.isFB2File(FromFilePath)) {
				// проверка, соответствует ли текущий файл критерия поиска для Избранной Сортировки
				SortQueryCriteria criteria = FB2SelectedSorting.isConformity(FromFilePath, m_lSSQCList, out IsNotRead);
				if (criteria != null) {
					// создание файла по новому пути для Жанра(ов) и Автора(ов) Книги из исходного fb2
					makeFileForGenreAndAuthorFromFB2(
						false, FromFilePath, lSLexems, m_sortOptions, criteria.GenresGroup
						);
					++m_sv.SourceFB2;
				} else {
					++m_sv.NotSort;
					if (IsNotRead) // к тому же еще и не читается
						copyBadFileToDir(FromFilePath, m_sortOptions.SourceDir, m_sortOptions);
				}
				// удаляем исходный fb2-файл, если включена эта опция
				if (!m_sortOptions.NotDelOriginalFiles) {
					if (File.Exists(FromFilePath))
						File.Delete(FromFilePath);
				}
			} else if (FilesWorker.isFB2Archive(FromFilePath)) {
				long UnZipCount = m_sharpZipLib.UnZipFB2Files(FromFilePath, m_TempDir);
				List<string> FilesListFromZip = FilesWorker.MakeFileListFromDir(m_TempDir, false, false);
				++m_sv.Zip;

				if (UnZipCount == -1) {
					// не получилось открыть архив - "битый"
					copyBadZipToBadDir(
						FromFilePath, m_sortOptions.SourceDir, m_sortOptions.NotOpenArchDir, m_sortOptions.FileExistMode
					);
					++m_sv.BadZip;
					return;
				} else {
					if (FilesListFromZip == null) {
						// в архиве нет fb2-файлов
						copyBadZipToBadDir(
							FromFilePath, m_sortOptions.SourceDir, m_sortOptions.NotOpenArchDir, m_sortOptions.FileExistMode
						);
						++m_sv.BadZip;
						return;
					}
				}
				m_sv.FB2FromZips += FilesListFromZip.Count;
				foreach (string FB2FromZipPath in FilesListFromZip) {
					// проверка, соответствует ли текущий файл критерия поиска для Избранной Сортировки
					SortQueryCriteria criteria = FB2SelectedSorting.isConformity(FB2FromZipPath, m_lSSQCList, out IsNotRead);
					
					if (criteria != null) {
						if (FilesWorker.isFB2File(FB2FromZipPath)) {
							// создание файла по новому пути для Жанра(ов) и Автора(ов) Книги из исходного fb2
							makeFileForGenreAndAuthorFromFB2(
								true, FB2FromZipPath, lSLexems, m_sortOptions, criteria.GenresGroup
								);
							++m_sv.SourceFB2;
							
							// удаляем исходный fb2-файл, если включена эта опция
							if (!m_sortOptions.NotDelOriginalFiles) {
								if ( File.Exists(FromFilePath) )
									File.Delete(FromFilePath);
							}
						} else {
							// пропускаем не fb2-файлы и не zip-архивы
							++m_sv.Other;
						}
					} else {
						// fb2-файл не соответствует критериям сортировки
						++m_sv.NotSort;
						if (IsNotRead) // к тому же еще и не читается
							copyBadFileToDir(FB2FromZipPath, m_TempDir, m_sortOptions);
					}
				}
				// очистка временной папки
				foreach (string FB2FromArchPath in FilesListFromZip) {
					if (File.Exists(FB2FromArchPath))
						File.Delete(FB2FromArchPath);
				}
				
				// удаляем исходный zip-файл, если включена эта опция
				if (!m_sortOptions.NotDelOriginalFiles) {
					if (File.Exists(FromFilePath))
						File.Delete(FromFilePath);
				}
			} else {
				// пропускаем не fb2-файлы и не zip-архивы
				++m_sv.Other;
			}
		}
		
		// создание файла по новому пути для Жанра(ов) и Автора(ов) Книги из исходного fb2
		// GenreGroup = null для Полной Сортировки. Для избранной - передаем значение Группы из критерия поиска
		private void makeFileForGenreAndAuthorFromFB2(
			bool FB2IsFromZip, string FromFilePath, List<TemplatesLexemsSimple> lSLexems,
			SortingOptions sortOptions, string GenreGroup
		) {
			if (sortOptions.GenresToDirsGenreOne && sortOptions.AuthorsToDirsAuthorOne) {
				// по первому Жанру и первому Автору Книги
				makeFileFor1Genre1AuthorWorker(FB2IsFromZip, FromFilePath, lSLexems, sortOptions, GenreGroup);
			} else if (sortOptions.GenresToDirsGenreOne && sortOptions.AuthorsToDirsAuthorAll) {
				// по первому Жанру и всем Авторам Книги
				makeFileFor1GenreAllAuthorWorker(FB2IsFromZip, FromFilePath, lSLexems, sortOptions, GenreGroup);
			} else if (sortOptions.GenresToDirsGenreAll && sortOptions.AuthorsToDirsAuthorOne) {
				// по всем Жанрам и первому Автору Книги
				makeFileForAllGenre1AuthorWorker(FB2IsFromZip, FromFilePath, lSLexems, sortOptions, GenreGroup);
			} else {
				// по всем Жанрам и всем Авторам Книги
				makeFileForAllGenreAllAuthorWorker(FB2IsFromZip, FromFilePath, lSLexems, sortOptions, GenreGroup);
			}
		}
		
		private void makeFileFor1Genre1AuthorWorker(
			bool FB2IsFromZip, string FromFilePath, List<TemplatesLexemsSimple> lSLexems,
			SortingOptions sortOptions, string GenreGroup
		) {
			string SourceDir = FB2IsFromZip ? m_TempDir : sortOptions.SourceDir;
			try {
				makeFB2File(FB2IsFromZip, FromFilePath, SourceDir, lSLexems, sortOptions, 0, 0, GenreGroup);
			} catch (Exception ex) {
				Debug.DebugMessage(
					FromFilePath, ex, "SortingForm.makeFileFor1Genre1AuthorWorker():"
				);
				if (FilesWorker.isFB2File(FromFilePath))
					copyBadFileToDir(FromFilePath, SourceDir, sortOptions);
			}
		}

		private void makeFileForAllGenre1AuthorWorker(
			bool FB2IsFromZip, string FromFilePath, List<TemplatesLexemsSimple> lSLexems,
			SortingOptions sortOptions, string GenreGroup
		) {
			string SourceDir = FB2IsFromZip ? m_TempDir : sortOptions.SourceDir;
			try {
				FictionBook fb2 = new FictionBook(FromFilePath);
				TitleInfo ti = fb2.getTitleInfo();
				IList<Genre> lGenres = ti.Genres;
				for (int i = 0; i != lGenres.Count; ++i)
					makeFB2File(FB2IsFromZip, FromFilePath, SourceDir, lSLexems, sortOptions, i, 0, GenreGroup);
			} catch (Exception ex) {
				Debug.DebugMessage(
					FromFilePath, ex, "SortingForm.makeFileForAllGenre1AuthorWorker():"
				);
				if (FilesWorker.isFB2File(FromFilePath))
					copyBadFileToDir(FromFilePath, SourceDir, sortOptions);
			}
		}

		private void makeFileFor1GenreAllAuthorWorker(
			bool FB2IsFromZip, string FromFilePath, List<TemplatesLexemsSimple> lSLexems,
			SortingOptions sortOptions, string GenreGroup
		) {
			string SourceDir = FB2IsFromZip ? m_TempDir : sortOptions.SourceDir;
			try {
				FictionBook fb2 = new FictionBook(FromFilePath);
				TitleInfo ti = fb2.getTitleInfo();
				IList<Author> lAuthors = ti.Authors;
				for(int i = 0; i != lAuthors.Count; ++i)
					makeFB2File(FB2IsFromZip, FromFilePath, SourceDir, lSLexems, sortOptions, 0, i, GenreGroup);
			} catch (Exception ex) {
				Debug.DebugMessage(
					FromFilePath, ex, "SortingForm.makeFileFor1GenreAllAuthorWorker():"
				);
				if (FilesWorker.isFB2File(FromFilePath))
					copyBadFileToDir(FromFilePath, SourceDir, sortOptions);
			}
		}

		private void makeFileForAllGenreAllAuthorWorker(
			bool FB2IsFromZip, string FromFilePath, List<TemplatesLexemsSimple> lSLexems,
			SortingOptions sortOptions, string GenreGroup
		) {
			string SourceDir = FB2IsFromZip ? m_TempDir : sortOptions.SourceDir;
			try {
				FictionBook fb2 = new FictionBook(FromFilePath);
				TitleInfo ti = fb2.getTitleInfo();
				IList<Genre> lGenres = ti.Genres;
				IList<Author> lAuthors = ti.Authors;
				for(int i = 0; i != lGenres.Count; ++i) {
					for(int j = 0; j != lAuthors.Count; ++j)
						makeFB2File(FB2IsFromZip, FromFilePath, SourceDir, lSLexems, sortOptions, i, j, GenreGroup);
				}
			} catch (Exception ex) {
				Debug.DebugMessage(
					FromFilePath, ex, "SortingForm.makeFileForAllGenreAllAuthorWorker():"
				);
				if (FilesWorker.isFB2File(FromFilePath))
					copyBadFileToDir(FromFilePath, SourceDir, sortOptions);
			}
		}

		// ===================================================================================================
		/// <summary>
		/// Создание файла по новому пути
		/// </summary>
		private void makeFB2File(bool FromZip, string FromFilePath, string SourceDir,
								 List<TemplatesLexemsSimple> lSLexems, SortingOptions sortOptions,
								 int nGenreIndex, int AuthorIndex, string GenreGroup)
		{
			// смотрим, что это за файл
			if (FilesWorker.isFB2File(FromFilePath)) {
				if (sortOptions.SortTypeOnlyValidFB2) {
					// тип сортировки: только валидные
					if (!isValid(FromFilePath, SourceDir, nGenreIndex, AuthorIndex, sortOptions))
						return;
				}

				try {
					string ToFilePath = sortOptions.TargetDir + "\\" +
						_templatesParser.generateNewPath(
							FromFilePath, lSLexems, nGenreIndex, AuthorIndex,
							sortOptions.getRegisterMode(), sortOptions.Space,
							sortOptions.Strict, sortOptions.Translit,
							ref sortOptions, ref _lCounter,
							_MaxBookTitleLenght, _MaxSequenceLenght, _MaxPublisherLenght, GenreGroup
						) + ".fb2";
					createFileTo(FromZip, FromFilePath, ToFilePath, sortOptions);
				} catch (Exception ex) {
					Debug.DebugMessage(
						FromFilePath, ex, "SortingForm.makeFB2File(): Создание файла по новому пути."
					);
					// нечитаемый fb2-файл - копируем его в папку Bad
					copyBadFileToDir(FromFilePath, SourceDir, sortOptions);
				}
			}
		}

		/// <summary>
		/// Создание нового файла или архива
		/// </summary>
		private void createFileTo(
			bool FromZip, string FromFilePath, string ToFilePath, SortingOptions sortOptions
			)
		{
			try {
				if (!m_sortOptions.ToZip)
					copyFileToTargetDir(FromFilePath, ToFilePath, sortOptions);
				else {
					// упаковка в архив: копируем файл в Temp папку с именем из тега назвыания книги
					if (!Directory.Exists(m_TempDir))
						Directory.CreateDirectory(m_TempDir);

					// упаковка в zip по месту назначения
					string NewFileName = Path.GetFileName(ToFilePath);
					if (!FromZip) {
						string NewFromPath = m_TempDir + "\\" + NewFileName;
						File.Copy(FromFilePath, NewFromPath, true);
						copyFileToArchive(NewFromPath, ToFilePath + ".zip", sortOptions);
					} else {
						// если fb2 из zip, то для изменения и его имени внутри будущего zip копируем его во вложенную временную папку
						string temp_dir = m_TempDir + "\\_";
						string NewFromPath = temp_dir + "\\" + NewFileName;
						Directory.CreateDirectory(temp_dir);
						File.Copy(FromFilePath, NewFromPath, true);
						copyFileToArchive(NewFromPath, ToFilePath + ".zip", sortOptions);
						File.Delete(NewFromPath);
						Directory.Delete(temp_dir);
					}
				}

				if (!m_sortOptions.ToZip) {
					if (File.Exists(ToFilePath))
						++m_sv.CreateInTarget;
				} else {
					if (File.Exists(ToFilePath + ".zip"))
						++m_sv.CreateInTarget;
				}

			} catch (Exception ex) {
				Debug.DebugMessage(
					FromFilePath, ex, "SortingForm.createFileTo(): Создание нового файла или архива."
				);
				// файл с длинным путем (название книги слишком длинное...)
				Directory.CreateDirectory(sortOptions.FileLongPathDir);
				ToFilePath = sortOptions.FileLongPathDir + "\\" + Path.GetFileName(FromFilePath);
				copyFileToTargetDir(FromFilePath, ToFilePath, sortOptions);
				++m_sv.LongPath;
			}
		}

		// копирование файла с сформированным именем (путь)
		private void copyFileToTargetDir(string FromFilePath, string ToFilePath, SortingOptions sortOptions)
		{
			// обработка уже существующих файлов в папке
			ToFilePath = fileExistWorker(ToFilePath, sortOptions, false);
			if (File.Exists(FromFilePath))
				File.Copy(FromFilePath, ToFilePath);
		}

		// архивирование файла с сформированным именем (путь)
		private void copyFileToArchive(string FromFilePath, string ToFilePath, SortingOptions sortOptions)
		{
			// обработка уже существующих файлов в папке
			ToFilePath = fileExistWorker(ToFilePath, sortOptions, true);
			m_sharpZipLib.ZipFile(FromFilePath, ToFilePath, 9, ICSharpCode.SharpZipLib.Zip.CompressionMethod.Deflated, 4096);
		}

		// обработка уже существующих файлов в папке
		private string fileExistWorker(string ToFilePath, SortingOptions sortOptions, bool ToZip)
		{
			if (!Directory.Exists(ToFilePath))
				Directory.CreateDirectory(Directory.GetParent(ToFilePath).ToString() );

			if (File.Exists(ToFilePath)) {
				if (sortOptions.FileExistMode == 0)
					File.Delete( ToFilePath );
				else {
					string Sufix = FilesWorker.createSufix(ToFilePath, sortOptions.FileExistMode);
					if (ToZip)
						ToFilePath = ToFilePath.Remove(ToFilePath.Length - 8) + Sufix + ".fb2.zip";
					else
						ToFilePath = ToFilePath.Remove(ToFilePath.Length - 4) + Sufix + ".fb2";
				}
			}
			return ToFilePath;
		}
		
		// нечитаемый fb2-файл или архив - копируем его в папку Bad
		private void copyBadFileToDir(string FromFilePath, string SourceDir, SortingOptions sortOptions) {
			Directory.CreateDirectory(sortOptions.NotReadFB2Dir);
			string FileName = FromFilePath.Remove(0, SourceDir.Length);
			string ToFilePath = sortOptions.NotReadFB2Dir + (FileName.Substring(0,1)=="\\" ? string.Empty : "\\") + FileName;
			copyFileToTargetDir(FromFilePath, ToFilePath, sortOptions);
			++m_sv.NotRead;
		}
		
		// копирование "битого" архива с сформированным именем (путь)
		private void copyBadZipToBadDir(string FromFilePath, string SourceDir, string TargetDir, int FileExistMode)
		{
			string ToFilePath = TargetDir + "\\" + FromFilePath.Remove(0, SourceDir.Length);
			FileInfo fi = new FileInfo(ToFilePath);
			if (!fi.Directory.Exists)
				Directory.CreateDirectory(fi.Directory.ToString());

			// обработка уже существующих файлов в папке
			if (File.Exists(ToFilePath)) {
				if (FileExistMode == 0)
					File.Delete(ToFilePath);
				else {
					ToFilePath = ToFilePath.Remove(ToFilePath.Length - 4)
						+ FilesWorker.createSufix(ToFilePath, FileExistMode) //Sufix
						+ Path.GetExtension(ToFilePath);
				}
			}
			if (File.Exists(FromFilePath)) {
				File.Copy(FromFilePath, ToFilePath);
			}
		}
		
		// если режим сортировки "только валидные" - то проверка и копирование невалидных в папку
		private bool isValid(
			string FromFilePath, string SourceDir, int GenreIndex, int AuthorIndex, SortingOptions sortOptions
			) {
			string ValidateResult = string.Empty;
			ValidateResult = m_fv2V.ValidatingFB2File(FromFilePath);
			if (ValidateResult.Length != 0) {
				// защита от многократного копирования невалимдного файла в папку для невалидных
				if (GenreIndex == 0 && AuthorIndex == 0) {
					// помещаем его в папку для невалидных файлов
					copyBadFileToDir(FromFilePath, SourceDir, sortOptions);
					++m_sv.NotValidFB2;
					return false; // файл невалидный - пропускаем его, сортируем дальше
				} else {
					return false; // файл уже скопирован - пропускаем его, сортируем дальше
				}
			}
			return true;
		}
		
		//------------------------------------------------------------------------------------------
		// удаление из списка всех обработанных книги (файлы)
		private void removeFinishedFilesInFilesList( ref List<string> FilesList, ref List<string> FinishedFilesList) {
			List<string> FilesToWorkingList = new List<string>();
			foreach (var file in FilesList.Except(FinishedFilesList))
				FilesToWorkingList.Add(file);
			
			FilesList.Clear();
			FilesList.AddRange(FilesToWorkingList);
		}

		//------------------------------------------------------------------------------------------
		// остановка поиска / возобновления поиска из xml
		// StopForSaveToXml: false - остановка поиска; true - остановка возобновления поиска
		private void stopSorting( bool StopForSaveToXml )
		{
			m_StopToSave = StopForSaveToXml;
			if ( m_bw.IsBusy ) {
				if( m_bw.WorkerSupportsCancellation )
					m_bw.CancelAsync();
			} else {
				if( m_bwRenew.WorkerSupportsCancellation )
					m_bwRenew.CancelAsync();
			}
		}
		#endregion

		#region Обработчики событий контролов
		void BtnSaveToXmlClick(object sender, EventArgs e)
		{
			stopSorting( true );
		}
		void BtnStopClick(object sender, EventArgs e)
		{
			stopSorting( false );
		}
		#endregion
		
	}
}
