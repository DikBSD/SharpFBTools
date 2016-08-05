/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 24.02.2014
 * Time: 8:27
 * 
 * License: GPL 2.1
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.IO;
using System.Linq;
using System.Xml.Linq;

using Core.Common;
using Core.FB2.FB2Parsers;
using Core.FB2.Description.Common;
using Core.FB2.Description.TitleInfo;

using FB2Validator		= Core.FB2Parser.FB2Validator;
using FilesWorker		= Core.Common.FilesWorker;
using SharpZipLibWorker = Core.Common.SharpZipLibWorker;
using MiscListView		= Core.Common.MiscListView;
using Colors			= Core.Common.Colors;
using EndWorkMode 		= Core.Common.EndWorkMode;

// enums
using SearchCompareModeEnum			= Core.Common.Enums.SearchCompareModeEnum;
using EndWorkModeEnum				= Core.Common.Enums.EndWorkModeEnum;
using FilesCountViewDupCollumnEnum	= Core.Common.Enums.FilesCountViewDupCollumnEnum;
using ResultViewDupCollumnEnum		= Core.Common.Enums.ResultViewDupCollumnEnum;

namespace Core.Duplicator
{
	/// <summary>
	/// CompareForm: Форма прогресса поиска копий fb2 книг
	/// </summary>
	public partial class CompareForm : Form
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CompareForm));
			this.sfdList = new System.Windows.Forms.SaveFileDialog();
			this.ProgressBar = new System.Windows.Forms.ProgressBar();
			this.ProgressPanel = new System.Windows.Forms.Panel();
			this.StatusLabel = new System.Windows.Forms.Label();
			this.ControlPanel = new System.Windows.Forms.Panel();
			this.btnSaveToXml = new System.Windows.Forms.Button();
			this.btnStop = new System.Windows.Forms.Button();
			this.cbGroupCountForList = new System.Windows.Forms.ComboBox();
			this.lblGroupCountForList = new System.Windows.Forms.Label();
			this.checkBoxSaveGroupsToXml = new System.Windows.Forms.CheckBox();
			this.ProgressPanel.SuspendLayout();
			this.ControlPanel.SuspendLayout();
			this.SuspendLayout();
			// 
			// sfdList
			// 
			this.sfdList.RestoreDirectory = true;
			this.sfdList.Title = "Укажите название файла копий";
			// 
			// ProgressBar
			// 
			this.ProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
			                                                                | System.Windows.Forms.AnchorStyles.Right)));
			this.ProgressBar.Location = new System.Drawing.Point(16, 16);
			this.ProgressBar.Margin = new System.Windows.Forms.Padding(4);
			this.ProgressBar.Name = "ProgressBar";
			this.ProgressBar.Size = new System.Drawing.Size(653, 28);
			this.ProgressBar.TabIndex = 0;
			// 
			// ProgressPanel
			// 
			this.ProgressPanel.Controls.Add(this.StatusLabel);
			this.ProgressPanel.Controls.Add(this.ProgressBar);
			this.ProgressPanel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ProgressPanel.Location = new System.Drawing.Point(0, 0);
			this.ProgressPanel.Margin = new System.Windows.Forms.Padding(4);
			this.ProgressPanel.Name = "ProgressPanel";
			this.ProgressPanel.Size = new System.Drawing.Size(892, 282);
			this.ProgressPanel.TabIndex = 0;
			// 
			// StatusLabel
			// 
			this.StatusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
			                                                                 | System.Windows.Forms.AnchorStyles.Left)
			                                                                | System.Windows.Forms.AnchorStyles.Right)));
			this.StatusLabel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.StatusLabel.Location = new System.Drawing.Point(15, 52);
			this.StatusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			this.StatusLabel.Name = "StatusLabel";
			this.StatusLabel.Size = new System.Drawing.Size(655, 215);
			this.StatusLabel.TabIndex = 1;
			// 
			// ControlPanel
			// 
			this.ControlPanel.BackColor = System.Drawing.Color.DarkGray;
			this.ControlPanel.Controls.Add(this.btnSaveToXml);
			this.ControlPanel.Controls.Add(this.btnStop);
			this.ControlPanel.Controls.Add(this.cbGroupCountForList);
			this.ControlPanel.Controls.Add(this.lblGroupCountForList);
			this.ControlPanel.Controls.Add(this.checkBoxSaveGroupsToXml);
			this.ControlPanel.Dock = System.Windows.Forms.DockStyle.Right;
			this.ControlPanel.Location = new System.Drawing.Point(684, 0);
			this.ControlPanel.Margin = new System.Windows.Forms.Padding(4);
			this.ControlPanel.Name = "ControlPanel";
			this.ControlPanel.Size = new System.Drawing.Size(208, 282);
			this.ControlPanel.TabIndex = 1;
			// 
			// btnSaveToXml
			// 
			this.btnSaveToXml.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.btnSaveToXml.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnSaveToXml.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveToXml.Image")));
			this.btnSaveToXml.Location = new System.Drawing.Point(0, 140);
			this.btnSaveToXml.Margin = new System.Windows.Forms.Padding(4);
			this.btnSaveToXml.Name = "btnSaveToXml";
			this.btnSaveToXml.Size = new System.Drawing.Size(208, 71);
			this.btnSaveToXml.TabIndex = 8;
			this.btnSaveToXml.Text = "Прервать в файл...";
			this.btnSaveToXml.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnSaveToXml.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnSaveToXml.UseVisualStyleBackColor = true;
			this.btnSaveToXml.Click += new System.EventHandler(this.BtnSaveToXmlClick);
			// 
			// btnStop
			// 
			this.btnStop.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.btnStop.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.btnStop.Image = ((System.Drawing.Image)(resources.GetObject("btnStop.Image")));
			this.btnStop.Location = new System.Drawing.Point(0, 211);
			this.btnStop.Margin = new System.Windows.Forms.Padding(4);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(208, 71);
			this.btnStop.TabIndex = 7;
			this.btnStop.Text = "Прервать";
			this.btnStop.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
			this.btnStop.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.BtnStopClick);
			// 
			// cbGroupCountForList
			// 
			this.cbGroupCountForList.Dock = System.Windows.Forms.DockStyle.Top;
			this.cbGroupCountForList.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbGroupCountForList.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.cbGroupCountForList.FormattingEnabled = true;
			this.cbGroupCountForList.Items.AddRange(new object[] {
			                                        	"5",
			                                        	"10",
			                                        	"50",
			                                        	"100",
			                                        	"150",
			                                        	"200",
			                                        	"250",
			                                        	"300",
			                                        	"350",
			                                        	"400",
			                                        	"450",
			                                        	"500",
			                                        	"550",
			                                        	"600",
			                                        	"650",
			                                        	"700",
			                                        	"750",
			                                        	"800",
			                                        	"850",
			                                        	"900",
			                                        	"950",
			                                        	"1000",
			                                        	"1500",
			                                        	"2000",
			                                        	"2500",
			                                        	"3000",
			                                        	"3500",
			                                        	"4000",
			                                        	"4500",
			                                        	"5000",
			                                        	"7500",
			                                        	"10000"});
			this.cbGroupCountForList.Location = new System.Drawing.Point(0, 85);
			this.cbGroupCountForList.Name = "cbGroupCountForList";
			this.cbGroupCountForList.Size = new System.Drawing.Size(208, 24);
			this.cbGroupCountForList.TabIndex = 5;
			// 
			// lblGroupCountForList
			// 
			this.lblGroupCountForList.Dock = System.Windows.Forms.DockStyle.Top;
			this.lblGroupCountForList.ForeColor = System.Drawing.Color.Navy;
			this.lblGroupCountForList.Location = new System.Drawing.Point(0, 62);
			this.lblGroupCountForList.Name = "lblGroupCountForList";
			this.lblGroupCountForList.Size = new System.Drawing.Size(208, 23);
			this.lblGroupCountForList.TabIndex = 4;
			this.lblGroupCountForList.Text = "Число Групп копий в файле:";
			// 
			// checkBoxSaveGroupsToXml
			// 
			this.checkBoxSaveGroupsToXml.Checked = true;
			this.checkBoxSaveGroupsToXml.CheckState = System.Windows.Forms.CheckState.Checked;
			this.checkBoxSaveGroupsToXml.Dock = System.Windows.Forms.DockStyle.Top;
			this.checkBoxSaveGroupsToXml.ForeColor = System.Drawing.Color.Blue;
			this.checkBoxSaveGroupsToXml.Location = new System.Drawing.Point(0, 0);
			this.checkBoxSaveGroupsToXml.Name = "checkBoxSaveGroupsToXml";
			this.checkBoxSaveGroupsToXml.Size = new System.Drawing.Size(208, 62);
			this.checkBoxSaveGroupsToXml.TabIndex = 3;
			this.checkBoxSaveGroupsToXml.Text = "Сохранять результат (Группы) сразу в файлы без построения дерева";
			this.checkBoxSaveGroupsToXml.UseVisualStyleBackColor = true;
			this.checkBoxSaveGroupsToXml.CheckedChanged += new System.EventHandler(this.CheckBoxSaveGroupsToXmlCheckedChanged);
			// 
			// CompareForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(892, 282);
			this.ControlBox = false;
			this.Controls.Add(this.ControlPanel);
			this.Controls.Add(this.ProgressPanel);
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(1200, 600);
			this.MinimizeBox = false;
			this.MinimumSize = new System.Drawing.Size(910, 300);
			this.Name = "CompareForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Поиск копий fb2 книг";
			this.ProgressPanel.ResumeLayout(false);
			this.ControlPanel.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		private System.Windows.Forms.ComboBox cbGroupCountForList;
		private System.Windows.Forms.Label lblGroupCountForList;
		private System.Windows.Forms.CheckBox checkBoxSaveGroupsToXml;
		private System.Windows.Forms.Label StatusLabel;
		private System.Windows.Forms.SaveFileDialog sfdList;
		private System.Windows.Forms.ProgressBar ProgressBar;
		private System.Windows.Forms.Button btnSaveToXml;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.Panel ControlPanel;
		private System.Windows.Forms.Panel ProgressPanel;
		#endregion
		
		#region Закрытые данные класса
		private const string m_sMessTitle		= "SharpFBTools - Поиск одинаковых fb2 файлов";
		private readonly string m_Source		= string.Empty;
		private readonly bool m_ScanSubDirs		= false;
		private int m_CompareMode				= 0; // режим сравнения книг на определение копий
		private string m_CompareModeName		= string.Empty; // название режима сравнения книг на определение копий
		private readonly bool m_autoResizeColumns	= false;
		private readonly string m_fromXmlPath		= null;	// null - полное сканирование; Путь - возобновление сравнения их xml
		private readonly string m_TempDir	= Settings.Settings.TempDirPath;
		private readonly StatusView	m_sv 	= new StatusView();
		private List<string> m_FilesList	= new List<string>();
		private bool m_StopToSave = false;	// true, если остановка с сохранением необработанного списка книг в файл.
		
		private readonly ListView m_lvFilesCount		= new ListView();
		private readonly ListView m_listViewFB2Files	= new ListView();
		private readonly MiscListView m_mscLV			= new MiscListView(); // класс по работе с ListView

		private readonly EndWorkMode		m_EndMode		= new EndWorkMode();
		private readonly SharpZipLibWorker	m_sharpZipLib	= new SharpZipLibWorker();
		private readonly FB2Validator		m_fv2Validator	= new FB2Validator();
		
		private BackgroundWorker m_bw			= null; // фоновый обработчик для Непрерывного сравнения
		private BackgroundWorker m_bwRenew		= null; // фоновый обработчик для Возобновления сравнения
		private Hashtable m_htWorkingBook		= new Hashtable( new FB2CultureComparer() ); // таблица обработанные файлов - копии
		private Hashtable m_htBookTitleAuthors	= new Hashtable( new FB2CultureComparer() ); // таблица для обработанных данных копий (режим группировки по Авторам)
		
		private List<string> _nonOpenedFile			= new List<string>();
		private const string _nonOpenedFB2FilePath	= "_DuplicatorNonOpenedFile.xml";
		private DateTime m_dtStart = DateTime.Now;

		#endregion
		
		public CompareForm( string fromXmlPath, string sSource, int CompareMode, string CompareModeName,
		                   bool ScanSubDirs, int GroupCountForList, bool SaveGroupToXMLWithoutTree,
		                   ListView lvFilesCount, ListView listViewFB2Files, bool AutoResizeColumns )
		{
			InitializeComponent();
			m_Source			= sSource;
			m_ScanSubDirs		= ScanSubDirs;
			m_CompareMode		= CompareMode;
			m_CompareModeName	= CompareModeName;
			m_lvFilesCount		= lvFilesCount;
			m_listViewFB2Files	= listViewFB2Files;
			m_autoResizeColumns	= AutoResizeColumns;
			
			cbGroupCountForList.SelectedIndex = GroupCountForList;
			checkBoxSaveGroupsToXml.Checked = SaveGroupToXMLWithoutTree;
			
			InitializeBackgroundWorker();
			InitializeRenewBackgroundWorker();
			
			// Запуск процесса DoWork от RunWorker
			if ( fromXmlPath == null ) {
				if ( !m_bw.IsBusy )
					m_bw.RunWorkerAsync(); // если не занят, то запустить процесс
			} else {
				m_fromXmlPath = fromXmlPath; // путь к xml файлу для возобновления поиска копий fb2 книг
				if ( !m_bwRenew.IsBusy )
					m_bwRenew.RunWorkerAsync(); // если не занят. то запустить процесс
			}
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
		// 								ОТКРЫТЫЕ МЕТОДЫ
		// =============================================================================================
		#region Открытые методы
		public bool IsStopToXmlClicked() {
			return m_StopToSave;
		}
		
		public string getSourceDirFromRenew() {
			return m_Source;
		}
		#endregion
		
		// =============================================================================================
		// 				BACKGROUNDWORKER ДЛЯ НЕПРЕРЫВНОГО СРАВНЕНИЯ и ПРЕРЫВАНИЯ / ВОЗОБНОВЛЕНИЯ
		// =============================================================================================
		#region BackgroundWorker для Непрерывного сравнения и прерывания / возобновления
		
		// =============================================================================================
		//			BackgroundWorker: Непрерывное Сравнение
		// =============================================================================================
		#region BackgroundWorker: Непрерывное Сравнение
		private void InitializeBackgroundWorker() {
			// Инициализация перед использование BackgroundWorker
			m_bw = new BackgroundWorker();
			m_bw.WorkerReportsProgress		= true; // Позволить выводить прогресс процесса
			m_bw.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
			m_bw.DoWork 			+= new DoWorkEventHandler( bw_DoWork );
			m_bw.ProgressChanged 	+= new ProgressChangedEventHandler( bw_ProgressChanged );
			m_bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler( bw_RunWorkerCompleted );
		}
		
		// поиск одинаковых fb2-файлов
		private void bw_DoWork( object sender, DoWorkEventArgs e ) {
			m_dtStart = DateTime.Now;
			ControlPanel.Enabled = false;
			StatusLabel.Text += "Создание списка файлов для поиска копий fb2 книг...\r";
			List<string> lDirList = new List<string>();
			m_FilesList.Clear();
			if ( !m_ScanSubDirs ) {
				// сканировать только указанную папку
				FilesWorker.makeFilesListFromDir( m_Source, ref m_FilesList, true );
				m_lvFilesCount.Items[(int)FilesCountViewDupCollumnEnum.AllDirs].SubItems[1].Text = "1";
			} else {
				// сканировать и все подпапки
				m_lvFilesCount.Items[(int)FilesCountViewDupCollumnEnum.AllDirs].SubItems[1].Text =
					FilesWorker.recursionDirsSearch( m_Source, ref lDirList, true ).ToString();
				m_sv.AllFiles = FilesWorker.makeFilesListFromDirs( ref m_bw, ref e, ref lDirList, ref m_FilesList, true );
				m_lvFilesCount.Items[(int)FilesCountViewDupCollumnEnum.AllBooks].SubItems[1].Text = m_sv.AllFiles.ToString();
			}
			m_bw.ReportProgress( 0 ); // отобразим данные в контролах

			if ( ( m_bw.CancellationPending ) ) {
				e.Cancel = true;
				return;
			}
			
			// проверка, есть ли хоть один файл в папке для сканирования
			if ( m_sv.AllFiles == 0 ) {
				MessageBox.Show( "В папке сканирования не найдено ни одного файла!\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			
			this.Text += String.Format(
				": Всего {0} каталогов; {1} файлов", lDirList.Count, m_sv.AllFiles
			);
			lDirList.Clear();
			// Сравнение fb2-файлов
			m_htWorkingBook.Clear();
			m_htBookTitleAuthors.Clear();
			ControlPanel.Enabled = true;
			// Создание списка копий fb2-книг по Группам
			makeBookCopiesGroups( ref m_bw, ref e, (SearchCompareModeEnum) m_CompareMode, ref m_FilesList );
			if ( m_autoResizeColumns )
				MiscListView.AutoResizeColumns( m_listViewFB2Files );
		}
		
		// Отображение результата
		private void bw_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
			++ProgressBar.Value;
		}
		
		// Проверяем - это отмена, ошибка, или конец задачи и сообщить
		private void bw_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {
			DateTime dtEnd = DateTime.Now;
			ViewDupProgressData(); // отображение данных прогресса
			FilesWorker.RemoveDir( Settings.Settings.TempDirPath );

			string sTime = dtEnd.Subtract( m_dtStart ).ToString().Substring( 0, 8 ) + " (час.:мин.:сек.)";
			if ( e.Cancelled ) {
				m_EndMode.EndMode = EndWorkModeEnum.Cancelled;
				m_EndMode.Message = "Поиск одинаковых fb2-файлов остановлен!\nСписок (псевдодерево) Групп копий fb2-файлов не сформирован полностью!\nЗатрачено времени: " + sTime;
				if ( m_StopToSave ) {
					// остановка поиска копий с сохранением списка необработанных книг в файл
					m_StopToSave = false;
					// сохранение в xml-файл списка данных о копиях и необработанных книг
					sfdList.Title		= "Укажите файл для будущего возобновления поиска копий книг:";
					sfdList.Filter		= "SharpFBTools Файлы хода работы Дубликатора (*.dup_break)|*.dup_break";
					sfdList.FileName	= string.Empty;
					sfdList.InitialDirectory = Settings.Settings.ProgDir;
					DialogResult result = sfdList.ShowDialog();
					if ( result == DialogResult.OK ) {
						ControlPanel.Enabled = false;
						StatusLabel.Text += "Сохранение данных анализа в файл:\r";
						StatusLabel.Text += sfdList.FileName;
						saveSearchedDataToXmlFile( sfdList.FileName, m_CompareMode, m_Source, m_ScanSubDirs, m_htWorkingBook, m_FilesList );
						m_EndMode.Message = "Поиск одинаковых fb2 файлов прерван!\nДанные поиска и список оставшихся для обработки книг сохранены в xml-файл:\n\n"+sfdList.FileName+"\n\nЗатрачено времени: "+sTime;
					}
				}
			} else if ( e.Error != null ) {
				m_EndMode.EndMode = EndWorkModeEnum.Error;
				m_EndMode.Message = "Ошибка:\n" + e.Error.Message + "\n" + e.Error.StackTrace + "\nЗатрачено времени: "+sTime;
			} else {
				m_EndMode.EndMode = EndWorkModeEnum.Done;
				m_EndMode.Message = "Поиск одинаковых fb2-файлов завершен!\nЗатрачено времени: "+sTime;
				if ( checkBoxSaveGroupsToXml.Checked ) {
					m_EndMode.Message += "\n\nРезультат поиска (Группы копий) сохранен в папку '_Copies'";
				} else {
					if ( m_listViewFB2Files.Items.Count == 0 )
						m_EndMode.Message += "\n\nНе найдено НИ ОДНОЙ копии книг!";
				}
			}
			
			if ( _nonOpenedFile.Count > 0 ) {
				m_EndMode.Message += string.Format(
					"\n\nСписок {0} файлов, которые никак не удалось открыть, и которые не участвовали в сравнении, находится в файле {1}",
					_nonOpenedFile.Count, _nonOpenedFB2FilePath
				);
				XDocument doc = new XDocument(
					new XDeclaration("1.0", "utf-8", "yes"),
					new XComment("Список книг, которые Дубликатор не смог открыть, и которые поэтому не участвовали в сравнении"),
					new XElement("Files", new XAttribute("type", "duplicator") )
				);
				foreach ( string file in _nonOpenedFile )
					doc.Root.Add( new XElement("File", file ) );
				doc.Save( _nonOpenedFB2FilePath );
			}
			
			m_sv.Clear();
			m_FilesList.Clear();
			m_htWorkingBook.Clear();
			m_htBookTitleAuthors.Clear();
			_nonOpenedFile.Clear();
			this.Close();
		}
		#endregion
		
		// =============================================================================================
		//	Общие для Полного и Прерванного сканирования Алгоритмы создания списков копий книг по Группам
		// =============================================================================================
		#region Общие для Полного и Прерванного сканирования Алгоритмы создания списков копий книг по Группам
		
		// Создание списка копий fb2-книг по Группам
		private void makeBookCopiesGroups( ref BackgroundWorker bw, ref DoWorkEventArgs e,
		                                  SearchCompareModeEnum CompareMode, ref List<string> FilesList ) {
			switch ( CompareMode ) {
				case SearchCompareModeEnum.Md5:
					// 0. Абсолютно одинаковые книги (md5)
					// Хэширование fb2-файлов
					FilesHashForMd5Parser( ref bw, ref e, ref FilesList, ref m_htWorkingBook );
					if ( !checkBoxSaveGroupsToXml.Checked ) {
						// Создание списка копий
						makeTreeOfBookCopies( ref bw, ref e, ref m_htWorkingBook );
					} else {
						// Сохранение Групп сразу в файлы без построения дерева
						saveCopiesListToXml( ref bw, ref e, Convert.ToInt32( cbGroupCountForList.Text ),
						                    m_CompareMode, m_CompareModeName, ref m_htWorkingBook );
					}
					break;
				case SearchCompareModeEnum.BookID:
					// 1. Одинаковый Id Книги (копии и/или разные версии правки одной и той же книги)
					// Хэширование fb2-файлов
					FilesHashForIDParser( ref bw, ref e, ref FilesList, ref m_htWorkingBook );
					if ( !checkBoxSaveGroupsToXml.Checked ) {
						// Создание списка копий
						makeTreeOfBookCopies( ref bw, ref e, ref m_htWorkingBook );
					} else {
						// Сохранение Групп сразу в файлы без построения дерева
						saveCopiesListToXml( ref bw, ref e, Convert.ToInt32( cbGroupCountForList.Text ),
						                    m_CompareMode, m_CompareModeName, ref m_htWorkingBook );
					}
					break;
				case SearchCompareModeEnum.BookTitle:
					// 2. Название Книги (могут быть найдены и разные книги разных Авторов, но с одинаковым Названием)
					// Хэширование fb2-файлов
					FilesHashForBTParser( ref bw, ref e, ref FilesList, ref m_htWorkingBook );
					if ( !checkBoxSaveGroupsToXml.Checked ) {
						// Создание списка копий
						makeTreeOfBookCopies( ref bw, ref e, ref m_htWorkingBook );
					} else {
						// Сохранение Групп сразу в файлы без построения дерева
						saveCopiesListToXml( ref bw, ref e, Convert.ToInt32( cbGroupCountForList.Text ),
						                    m_CompareMode, m_CompareModeName, ref m_htWorkingBook );
					}
					break;
				case SearchCompareModeEnum.AuthorAndTitle:
					// 3. Автор(ы) и Название Книги (одна и та же книга, сделанная разными людьми - разные Id, но Автор и Название - одинаковые)
					// Хэширование fb2-файлов
					FilesHashForBTParser( ref bw, ref e, ref FilesList, ref m_htWorkingBook );
					// Хэширование по одинаковым Авторам в пределах сгенерированных групп книг по одинаковым названиям
					FilesHashForAuthorsParser( ref bw, ref e, ref m_htWorkingBook, ref m_htBookTitleAuthors );
					if ( !checkBoxSaveGroupsToXml.Checked ) {
						// Создание списка копий
						makeTreeOfBookCopies( ref bw, ref e, ref m_htBookTitleAuthors );
					} else {
						// Сохранение Групп сразу в файлы без построения дерева
						saveCopiesListToXml( ref bw, ref e, Convert.ToInt32( cbGroupCountForList.Text ),
						                    m_CompareMode, m_CompareModeName, ref m_htBookTitleAuthors );
					}
					break;
				case SearchCompareModeEnum.AuthorFIO:
					// 4. Авторы с одинаковой Фамилией и инициалами
					// Хэширование fb2-файлов
					FilesHashForAuthorFIOParser( ref bw, ref e, ref FilesList, ref m_htWorkingBook );
					if ( !checkBoxSaveGroupsToXml.Checked ) {
						// Создание списка копий
						makeTreeOfBookCopies( ref bw, ref e, ref m_htWorkingBook );
					} else {
						// Сохранение Групп сразу в файлы без построения дерева
						saveCopiesListToXml( ref bw, ref e, Convert.ToInt32( cbGroupCountForList.Text ),
						                    m_CompareMode, m_CompareModeName, ref m_htWorkingBook );
					}
					break;
			}
		}
		
		// Создание дерева списка копий для всех режимов сравнения
		private void makeTreeOfBookCopies( ref BackgroundWorker bw, ref DoWorkEventArgs e, ref Hashtable ht ) {
			// блокировка возможности сразу сохранять результат в xml файлы, минуя построения дерева.
			checkBoxSaveGroupsToXml.Enabled = false;
			lblGroupCountForList.Enabled = false;
			cbGroupCountForList.Enabled = false;
			
			StatusLabel.Text += "Создание списка (псевдодерево) одинаковых fb2-файлов...\r";
			ProgressBar.Maximum	= ht.Values.Count;
			ProgressBar.Value	= 0;
			// сортировка ключей (групп)
			List<string> keyList = makeSortedKeysForGroups( ref ht );
			int i = 0;
			foreach ( string key in keyList ) {
				if ( ( bw.CancellationPending ) ) {
					e.Cancel = true;
					return;
				}
				++m_sv.Group; // число групп одинаковых книг
				// формирование представления Групп с их книгами
				makeBookCopiesView( (FB2FilesDataInGroup)ht[key] );
				bw.ReportProgress( ++i );
			}
		}
		
		// Хэширование по одинаковым Авторам в пределах сгенерированных групп книг по одинаковым названиям
		// htFB2ForBT - заполненная хеш-таблица списками книг по критерию одинакового Названия книг
		// htBookTitleAuthors - заполняемая хеш-таблица списками книг по критерию ( Название книги (Авторы) )
		private void FilesHashForAuthorsParser( ref BackgroundWorker bw, ref DoWorkEventArgs e,
		                                       ref Hashtable htFB2ForBT, ref Hashtable htBookTitleAuthors ) {
			StatusLabel.Text += "Хеширование по Авторам...\r";
			ProgressBar.Maximum	= htFB2ForBT.Values.Count;
			ProgressBar.Value	= 0;
			// генерация списка ключей хеш-таблицы (для удаления обработанного элемента таблицы)
			List<string> keyList = makeSortedKeysForGroups( ref htFB2ForBT );
			// группировка книг по одинаковым Авторам в пределах сгенерированных Групп книг по одинаковым Названиям
			int i = 0;
			foreach ( string key in keyList ) {
				// разбивка на группы для одинакового Названия по Авторам
				Hashtable htGroupAuthors = FindDupForAuthors(
					ref bw, ref e, (FB2FilesDataInGroup)htFB2ForBT[key], false
				);
				if ( ( bw.CancellationPending ) ) {
					e.Cancel = true;
					return;
				}
				foreach ( FB2FilesDataInGroup fb2List in htGroupAuthors.Values ) {
					if ( !htBookTitleAuthors.ContainsKey( fb2List.Group ) )
						htBookTitleAuthors.Add( fb2List.Group, fb2List );
					else {
						FB2FilesDataInGroup fb2ListInGroup = (FB2FilesDataInGroup)htBookTitleAuthors[fb2List.Group];
						fb2ListInGroup.AddRange( fb2List );
					}
				}
				
				// удаление обработанной группы книг, сгруппированных по одинаковому названию
				htFB2ForBT.Remove(key);
				bw.ReportProgress( ++i );
			}
		}
		
		// создание групп копий по Авторам, относительно найденного Названия Книги
		private Hashtable FindDupForAuthors( ref BackgroundWorker bw, ref DoWorkEventArgs e, FB2FilesDataInGroup fb2Group, bool WithMiddleName ) {
			// в fb2Group.Group - название группы (название книги у всех книг одинаковое, а пути - разные )
			// внутри fb2Group в BookData - данные на каждую книгу группы
			Hashtable ht = new Hashtable( new FB2CultureComparer() );
			// 2 итератора для перебора всех книг группы. 1-й - только на текущий элемент группы, 2-й - скользящий на все последующие. т.е. iter2 = iter1+1
			for ( int iter1 = 0; iter1 != fb2Group.Count; ++iter1 ) {
				if( ( bw.CancellationPending ) )  {
					e.Cancel = true;
					return null;
				}
				BookData bd1 = fb2Group[iter1]; // текущая книга
				FB2FilesDataInGroup fb2NewGroup = new FB2FilesDataInGroup();
				// перебор всех книг в группе, за исключением текущей
				for ( int iter2 = iter1 + 1; iter2 != fb2Group.Count; ++iter2 ) {
					// сравнение текущей книги со всеми последующими
					BookData bd2 = fb2Group[iter2];
					if ( bd1.isSameBook(bd2, WithMiddleName) ) {
						if ( !fb2NewGroup.isBookExists(bd2.Path) )
							fb2NewGroup.Add( bd2 );
					}
				}
				if ( fb2NewGroup.Count >= 1 ) {
					// только для копий, а не для единичных книг
					fb2NewGroup.Group = fb2Group.Group + " ( " + fb2NewGroup.makeAuthorsString( WithMiddleName ) + " )";
					fb2NewGroup.Insert( 0, bd1 );
					if ( !ht.ContainsKey( fb2NewGroup.Group ) )
						ht.Add( fb2NewGroup.Group, fb2NewGroup );
				}
			}
			return ht;
		}
		
		// удаление элементов таблицы, value (списки) которых состоят из 1-го элемента (это не копии)
		private void removeNotCopiesEntryInHashTable( ref Hashtable ht ) {
			List<DictionaryEntry> notCopies = new List<DictionaryEntry>();
			foreach (DictionaryEntry entry in ht) {
				FB2FilesDataInGroup fb2f = (FB2FilesDataInGroup)entry.Value;
				if (fb2f.Count == 1)
					notCopies.Add(entry);
			}
			foreach (var ent in notCopies)
				ht.Remove(ent.Key);
		}
		
		// хеширование файлов в контексте Md5 книг:
		// 0. Абсолютно одинаковые книги (md5)
		// параметры: FilesList - список файлов для сканирования
		private void FilesHashForMd5Parser( ref BackgroundWorker bw, ref DoWorkEventArgs e, ref List<string> FilesList, ref Hashtable htFB2ForMd5 ) {
			StatusLabel.Text += "Хэширование fb2-файлов...\r";
			ProgressBar.Maximum	= FilesList.Count;
			ProgressBar.Value	= 0;
			
			List<string> FinishedFilesList = new List<string>();
			for( int i = 0; i != FilesList.Count; ++i ) {
				if( ( bw.CancellationPending ) )  {
					// удаление из списка всех файлов обработанные книги (файлы)
					WorksWithBooks.removeFinishedFilesInFilesList( ref FilesList, ref FinishedFilesList);
					e.Cancel = true;
					return;
				}
				if ( FilesWorker.isFB2File( FilesList[i] ) ) {
					// заполнение хеш таблицы данными о fb2-книгах в контексте их md5
					MakeFB2Md5HashTable( null, FilesList[i], ref htFB2ForMd5 );
					// обработанные файлы
					FinishedFilesList.Add( FilesList[i] );
				}  else {
					if ( FilesWorker.isFB2Archive( FilesList[i] ) ) {
						try {
							m_sharpZipLib.UnZipFB2Files( FilesList[i], m_TempDir );
							string [] files = Directory.GetFiles( m_TempDir );
							if ( files.Length > 0 ) {
								if ( FilesWorker.isFB2File( files[0] ) ) {
									// заполнение хеш таблицы данными о fb2-книгах в контексте их md5
									MakeFB2Md5HashTable( FilesList[i], files[0], ref htFB2ForMd5 );
									// обработанные файлы
									FinishedFilesList.Add( FilesList[i] );
								}
							}
						} catch {
							// обработанные файлы
							FinishedFilesList.Add(FilesList[i]);
						}
						FilesWorker.RemoveDir( m_TempDir );
					}
				}
				bw.ReportProgress( i ); // отобразим данные в контролах
			}
			// удаление элементов таблицы, value (списки) которых состоят из 1-го элемента (это не копии)
			removeNotCopiesEntryInHashTable( ref htFB2ForMd5 );
			// удаление из списка всех файлов обработанные книги (файлы)
			WorksWithBooks.removeFinishedFilesInFilesList( ref FilesList, ref FinishedFilesList );
		}
		
		/// <summary>
		/// Заполнение хеш таблицы данными о fb2-книгах в контексте их md5
		/// </summary>
		/// <param name="ZipPath">путь к zip-архиву. Если книга - не запакована в zip, то ZipPath = null</param>
		/// <param name="SrcPath">путь к fb2-файлу;</param>
		/// <param name="htFB2ForMd5">хеш-таблица</param>
		private void MakeFB2Md5HashTable( string ZipPath, string SrcPath, ref Hashtable htFB2ForMd5 ) {
			string md5 = ComputeMD5Checksum( SrcPath );
			
			FictionBook fb2 = null;
			try {
				fb2 = new FictionBook( SrcPath );
			} catch  {
				collectBadFB2( !string.IsNullOrEmpty(ZipPath) ? ZipPath : SrcPath );
				return;
			}
			
			string Encoding = fb2.getEncoding();
			if( string.IsNullOrWhiteSpace( Encoding ) )
				Encoding = "?";
			string ID = fb2.DIID;
			if( ID == null )
				return;

			if( ID.Trim().Length == 0 )
				ID = "Тег <id> в этих книгах \"пустой\"";
			
			// данные о книге
			BookData fb2BookData = new BookData(
				fb2.TIBookTitle, fb2.TIAuthors, fb2.TIGenres, fb2.TILang, ID, fb2.DIVersion, SrcPath, Encoding
			);
			if (ZipPath != null)
				fb2BookData.Path = ZipPath;
			
			if( !htFB2ForMd5.ContainsKey( md5 ) ) {
				// такой книги в числе дублей еще нет
				FB2FilesDataInGroup fb2f = new FB2FilesDataInGroup( fb2BookData, md5 );
				htFB2ForMd5.Add( md5, fb2f );
			} else {
				// такая книга в числе дублей уже есть
				FB2FilesDataInGroup fb2f = (FB2FilesDataInGroup)htFB2ForMd5[md5];
				fb2f.Add( fb2BookData );
				//htFB2ForMd5[md5] = fb2f; //ИЗБЫТОЧНЫЙ КОД
			}
		}
		
		// хеширование файлов в контексте Id книг:
		// 1. Одинаковый Id Книги (копии и/или разные версии правки одной и той же книги)
		// параметры: FilesList - список файлов для сканирования
		private void FilesHashForIDParser( ref BackgroundWorker bw, ref DoWorkEventArgs e, ref List<string> FilesList, ref Hashtable htFB2ForID ) {
			StatusLabel.Text += "Хэширование fb2-файлов...\r";
			ProgressBar.Maximum	= FilesList.Count;
			ProgressBar.Value	= 0;
			
			List<string> FinishedFilesList = new List<string>();
			for( int i = 0; i != FilesList.Count; ++i ) {
				if( ( bw.CancellationPending ) )  {
					// удаление из списка всех файлов обработанные книги (файлы)
					WorksWithBooks.removeFinishedFilesInFilesList( ref FilesList, ref FinishedFilesList);
					e.Cancel = true;
					return;
				}
				if( FilesWorker.isFB2File( FilesList[i] ) ) {
					// заполнение хеш таблицы данными о fb2-книгах в контексте их ID
					MakeFB2IDHashTable( null, FilesList[i], ref htFB2ForID );
					// обработанные файлы
					FinishedFilesList.Add(FilesList[i]);
				}  else {
					if( FilesWorker.isFB2Archive( FilesList[i] ) ) {
						try {
							m_sharpZipLib.UnZipFB2Files(FilesList[i], m_TempDir);
							string [] files = Directory.GetFiles( m_TempDir );
							if( files.Length > 0 ) {
								if( FilesWorker.isFB2File( files[0] ) ) {
									// заполнение хеш таблицы данными о fb2-книгах в контексте их ID
									MakeFB2IDHashTable( FilesList[i], files[0], ref htFB2ForID );
									// обработанные файлы
									FinishedFilesList.Add(FilesList[i]);
								}
							}
						} catch {
						}
						FilesWorker.RemoveDir( m_TempDir );
					}
				}
				bw.ReportProgress( i ); // отобразим данные в контролах
			}
			// удаление элементов таблицы, value (списки) которых состоят из 1-го элемента (это не копии)
			removeNotCopiesEntryInHashTable( ref htFB2ForID );
			// удаление из списка всех файлов обработанные книги (файлы)
			WorksWithBooks.removeFinishedFilesInFilesList( ref FilesList, ref FinishedFilesList);
		}
		
		/// <summary>
		/// Заполнение хеш таблицы данными о fb2-книгах в контексте их ID
		/// </summary>
		/// <param name="ZipPath">путь к zip-архиву. Если книга - не запакована в zip, то ZipPath = null</param>
		/// <param name="SrcPath">путь к fb2-файлу;</param>
		/// <param name="htFB2ForID">хеш-таблица</param>
		private void MakeFB2IDHashTable( string ZipPath, string SrcPath, ref Hashtable htFB2ForID ) {
			FictionBook fb2 = null;
			try {
				fb2 = new FictionBook( SrcPath );
			} catch  {
				collectBadFB2( !string.IsNullOrEmpty(ZipPath) ? ZipPath : SrcPath );
				return;
			}
			
			string Encoding = fb2.getEncoding();
			if( string.IsNullOrWhiteSpace( Encoding ) )
				Encoding = "?";
			string ID = fb2.DIID;
			if( string.IsNullOrWhiteSpace( ID ) )
				ID = "<ID книги отсутствует>";
			
			// данные о книге
			BookData fb2BookData = new BookData(
				fb2.TIBookTitle, fb2.TIAuthors, fb2.TIGenres, fb2.TILang, ID, fb2.DIVersion, SrcPath, Encoding
			);
			if (ZipPath != null)
				fb2BookData.Path = ZipPath;
			
			if( !htFB2ForID.ContainsKey( ID ) ) {
				// такой книги в числе дублей еще нет
				FB2FilesDataInGroup fb2f = new FB2FilesDataInGroup( fb2BookData, ID );
				htFB2ForID.Add( ID, fb2f );
			} else {
				// такая книга в числе дублей уже есть
				FB2FilesDataInGroup fb2f = (FB2FilesDataInGroup)htFB2ForID[ID];
				fb2f.Add( fb2BookData );
				//htFB2ForID[sID] = fb2f; //ИЗБЫТОЧНЫЙ КОД
			}
		}
		
		// хеширование файлов в контексте Авторов и Названия книг:
		// 2. Название Книги (могут быть найдены и разные книги разных Авторов, но с одинаковым Названием)
		// 3. Автор(ы) и Название Книги (одна и та же книга, сделанная разными людьми - разные Id, но Автор и Название - одинаковые)
		// параметры: FilesList - список файлов для сканирования
		private void FilesHashForBTParser( ref BackgroundWorker bw, ref DoWorkEventArgs e, ref List<string> FilesList, ref Hashtable htFB2ForBT ) {
			StatusLabel.Text += "Хэширование fb2-файлов...\r";
			ProgressBar.Maximum	= FilesList.Count;
			ProgressBar.Value	= 0;
			
			List<string> FinishedFilesList = new List<string>();
			for ( int i = 0; i != FilesList.Count; ++i ) {
				if ( ( bw.CancellationPending ) )  {
					// удаление из списка всех файлов обработанные книги (файлы)
					WorksWithBooks.removeFinishedFilesInFilesList( ref FilesList, ref FinishedFilesList);
					e.Cancel = true;
					return;
				}
				if ( FilesWorker.isFB2File( FilesList[i] ) ) {
					// заполнение хеш таблицы данными о fb2-книгах в контексте их Авторов и Названия
					MakeFB2BTHashTable( null, FilesList[i], ref htFB2ForBT );
					// обработанные файлы
					FinishedFilesList.Add(FilesList[i]);
				} else {
					if ( FilesWorker.isFB2Archive( FilesList[i] ) ) {
						try {
							m_sharpZipLib.UnZipFB2Files(FilesList[i], m_TempDir);
							string [] files = Directory.GetFiles( m_TempDir );
							if ( files.Length > 0 ) {
								if ( FilesWorker.isFB2File( files[0] ) ) {
									// заполнение хеш таблицы данными о fb2-книгах в контексте их Авторов и Названия
									MakeFB2BTHashTable( FilesList[i], files[0], ref htFB2ForBT );
									// обработанные файлы
									FinishedFilesList.Add(FilesList[i]);
								}
							}
						} catch /*(Exception exp)*/ {
						}
						FilesWorker.RemoveDir( m_TempDir );
					}
				}
				bw.ReportProgress( i ); // отобразим данные в контролах
			}
			// удаление элементов таблицы, value (списки) которых состоят из 1-го элемента (это не копии)
			removeNotCopiesEntryInHashTable( ref htFB2ForBT );
			// удаление из списка всех файлов обработанные книги (файлы)
			WorksWithBooks.removeFinishedFilesInFilesList( ref FilesList, ref FinishedFilesList);
		}
		
		/// <summary>
		/// Заполнение хеш таблицы данными о fb2-книгах в контексте их Названия
		/// </summary>
		/// <param name="ZipPath">путь к zip-архиву. Если книга - не запакована в zip, то ZipPath = null</param>
		/// <param name="SrcPath">путь к fb2-файлу;</param>
		/// <param name="htFB2ForBT">хеш-таблица</param>
		private void MakeFB2BTHashTable( string ZipPath, string SrcPath, ref Hashtable htFB2ForBT ) {
			FictionBook fb2 = null;
			try {
				fb2 = new FictionBook( SrcPath );
			} catch  {
				collectBadFB2( !string.IsNullOrEmpty(ZipPath) ? ZipPath : SrcPath );
				return;
			}
			
			string Encoding = fb2.getEncoding();
			if( string.IsNullOrWhiteSpace( Encoding ) )
				Encoding = "?";
			
			BookTitle bookTitle	= fb2.TIBookTitle;
			string BT = "<Название книги отсутствует>";
			if( bookTitle != null && !string.IsNullOrWhiteSpace( bookTitle.Value ) )
				BT = bookTitle.Value.Trim();
			
			// данные о книге
			BookData fb2BookData = new BookData(
				bookTitle, fb2.TIAuthors, fb2.TIGenres, fb2.TILang, fb2.DIID, fb2.DIVersion, SrcPath, Encoding
			);
			
			if (ZipPath != null)
				fb2BookData.Path = ZipPath;
			
			if( !htFB2ForBT.ContainsKey( BT ) ) {
				// такой книги в числе дублей еще нет
				FB2FilesDataInGroup fb2f = new FB2FilesDataInGroup( fb2BookData, BT );
				htFB2ForBT.Add( BT, fb2f );
			} else {
				// такая книга в числе дублей уже есть
				FB2FilesDataInGroup fb2f = (FB2FilesDataInGroup)htFB2ForBT[BT];
				fb2f.Add( fb2BookData );
				//htFB2ForBT[sBT] = fb2f; //ИЗБЫТОЧНЫЙ КОД
			}
		}

		// хеширование файлов в контексте Авторов с одинаковой Фамилией и инициалами:
		// могут быть найдены и разные книги разных Авторов, но с одинаковыми Фамилиями и инициалами
		// параметры: FilesList - список файлов для сканирования
		private void FilesHashForAuthorFIOParser( ref BackgroundWorker bw, ref DoWorkEventArgs e, ref List<string> FilesList, ref Hashtable htFB2ForAuthorFIO ) {
			StatusLabel.Text += "Хэширование fb2-файлов...\r";
			ProgressBar.Maximum	= FilesList.Count;
			ProgressBar.Value	= 0;
			
			List<string> FinishedFilesList = new List<string>();
			for( int i = 0; i != FilesList.Count; ++i ) {
				if( ( bw.CancellationPending ) )  {
					// удаление из списка всех файлов обработанные книги (файлы)
					WorksWithBooks.removeFinishedFilesInFilesList( ref FilesList, ref FinishedFilesList);
					e.Cancel = true;
					return;
				}
				if( FilesWorker.isFB2File( FilesList[i] ) ) {
					// заполнение хеш таблицы данными о fb2-книгах в контексте Авторов с одинаковой Фамилией и инициалами
					MakeFB2AuthorFIOHashTable( null, FilesList[i], ref htFB2ForAuthorFIO );
					// обработанные файлы
					FinishedFilesList.Add(FilesList[i]);
				} else {
					if( FilesWorker.isFB2Archive( FilesList[i] ) ) {
						try {
							m_sharpZipLib.UnZipFB2Files(FilesList[i], m_TempDir);
							string [] files = Directory.GetFiles( m_TempDir );
							if( files.Length > 0 ) {
								if( FilesWorker.isFB2File( files[0] ) ) {
									// заполнение хеш таблицы данными о fb2-книгах в контексте Авторов с одинаковой Фамилией и инициалами
									MakeFB2AuthorFIOHashTable( FilesList[i], files[0], ref htFB2ForAuthorFIO );
									// обработанные файлы
									FinishedFilesList.Add(FilesList[i]);
								}
							}
						} catch {
						}
						FilesWorker.RemoveDir( m_TempDir );
					}
				}
				bw.ReportProgress( i ); // отобразим данные в контролах
			}
			// удаление элементов таблицы, value (списки) которых состоят из 1-го элемента
			removeNotCopiesEntryInHashTable( ref htFB2ForAuthorFIO );
			// удаление из списка всех файлов обработанные книги (файлы)
			WorksWithBooks.removeFinishedFilesInFilesList( ref FilesList, ref FinishedFilesList);
		}
		
		/// <summary>
		/// Заполнение хеш таблицы данными о fb2-книгах в контексте Авторов с одинаковой Фамилией и инициалами
		/// </summary>
		/// <param name="ZipPath">путь к zip-архиву. Если книга - не запакована в zip, то ZipPath = null</param>
		/// <param name="SrcPath">путь к fb2-файлу</param>
		/// <param name="htFB2ForAuthorFIO">хеш-таблица</param>
		private void MakeFB2AuthorFIOHashTable( string ZipPath, string SrcPath, ref Hashtable htFB2ForAuthorFIO ) {
			FictionBook fb2 = null;
			try {
				fb2 = new FictionBook( SrcPath );
			} catch  {
				collectBadFB2( !string.IsNullOrEmpty(ZipPath) ? ZipPath : SrcPath );
				return;
			}
			
			string Encoding = fb2.getEncoding();
			if( string.IsNullOrWhiteSpace( Encoding ) )
				Encoding = "?";
			
			IList<Author> AuthorsList = fb2.TIAuthors;
			string sAuthor = "<Автор книги отсутствует>";
			if( AuthorsList != null  ) {
				foreach( Author a in AuthorsList ) {
					if ( a.LastName != null && !string.IsNullOrEmpty( a.LastName.Value ) )
						sAuthor = a.LastName.Value;
					if ( a.FirstName != null && !string.IsNullOrEmpty( a.FirstName.Value ) )
						sAuthor += " " + a.FirstName.Value.Substring(0,1);
//					if ( a.MiddleName != null && !string.IsNullOrEmpty( a.MiddleName.Value ) )
//						sAuthor += " " + a.MiddleName.Value.Substring(0,1);
					sAuthor = sAuthor.Trim();
					// Заполнение хеш таблицы данными о fb2-книгах в контексте Авторов
					FB2AuthorFIOSetHashTable( fb2, ZipPath, SrcPath, Encoding, sAuthor, ref htFB2ForAuthorFIO );
				}
			} else {
				// Заполнение хеш таблицы данными о fb2-книгах в контексте Авторов
				FB2AuthorFIOSetHashTable( fb2, ZipPath, SrcPath, Encoding, sAuthor, ref htFB2ForAuthorFIO );
			}
		}
		/// <summary>
		/// Заполнение хеш таблицы данными о fb2-книгах в контексте Авторов
		/// </summary>
		/// <param name="fb2">объект класса FictionBook</param>
		/// <param name="ZipPath">путь к zip-архиву. Если книга - не запакована в zip, то ZipPath = null</param>
		/// <param name="SrcPath">путь к fb2-файлу</param>
		/// <param name="Encoding">кодировка текщего файла в fb2</param>
		/// <param name="sAuthor">Фамилия и 1-я буква Имени текущего автора</param>
		/// <param name="htFB2ForAuthorFIO">хеш-таблица</param>
		private void FB2AuthorFIOSetHashTable( FictionBook fb2, string ZipPath, string SrcPath, string Encoding,
		                                      string sAuthor, ref Hashtable htFB2ForAuthorFIO ) {
			// данные о книге
			BookData fb2BookData = new BookData(
				fb2.TIBookTitle, fb2.TIAuthors, fb2.TIGenres, fb2.TILang, fb2.DIID, fb2.DIVersion, SrcPath, Encoding
			);
			
			if (ZipPath != null)
				fb2BookData.Path = ZipPath;
			
			if( !htFB2ForAuthorFIO.ContainsKey( sAuthor ) ) {
				// этого Автор sAuthor в Группе еще нет
				FB2FilesDataInGroup fb2f = new FB2FilesDataInGroup( fb2BookData, sAuthor );
				fb2f.Group = sAuthor;
				htFB2ForAuthorFIO.Add( sAuthor, fb2f );
			} else {
				// этот Автор sAuthor в Группе уже есть
				FB2FilesDataInGroup fb2f = (FB2FilesDataInGroup)htFB2ForAuthorFIO[sAuthor];
				fb2f.Add( fb2BookData );
				//htFB2ForBT[sAuthor] = fb2f; //ИЗБЫТОЧНЫЙ КОД
			}
		}
		#endregion

		// =============================================================================================
		//					BackgroundWorker: Возобновление поиска копий книг
		// =============================================================================================
		#region BackgroundWorker: Возобновление поиска копий книг
		private void InitializeRenewBackgroundWorker() {
			m_bwRenew = new BackgroundWorker();
			m_bwRenew.WorkerReportsProgress			= true; // Позволить выводить прогресс процесса
			m_bwRenew.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
			m_bwRenew.DoWork 				+= new DoWorkEventHandler( m_bwRenew_renewSearchDataFromFile_DoWork );
			m_bwRenew.ProgressChanged 		+= new ProgressChangedEventHandler( bw_ProgressChanged );
			m_bwRenew.RunWorkerCompleted	+= new RunWorkerCompletedEventHandler( bw_RunWorkerCompleted );
		}
		
		// возобновление проверки - загрузка отчета о найденных копиях и о необработанных книгах из xml-файла
		private void m_bwRenew_renewSearchDataFromFile_DoWork( object sender, DoWorkEventArgs e ) {
			m_dtStart = DateTime.Now;
			ControlPanel.Enabled = false;
			// загрузка данных из xml
			StatusLabel.Text += "Возобновление поиска копий fb2 книг из xml файла:\r";
			StatusLabel.Text += m_fromXmlPath + "\r";
			StatusLabel.Text += "Загрузка данных поиска из xml файла...\r";
			XElement xTree = XElement.Load( m_fromXmlPath );

			//режим сравнения
			m_CompareMode = Convert.ToInt32( xTree.Element("CompareMode").Attribute("index").Value );
			//загрузка данных о ходе сравнения
			XElement xCompareData = xTree.Element("CompareData");
			m_sv.AllFiles = Convert.ToInt32( xCompareData.Element("AllFiles").Value );
			m_lvFilesCount.Items[(int)FilesCountViewDupCollumnEnum.AllDirs].SubItems[1].Text = xCompareData.Element("AllDirs").Value;

			ViewDupProgressData();
			
			// заполнение списка необработанных файлов
			m_FilesList.Clear();
			IEnumerable<XElement> files = xTree.Element("NotWorkingFiles").Elements("File");
			int i = 0;
			ProgressBar.Maximum = files.ToList().Count;
			ProgressBar.Value = 0;
			foreach ( XElement element in files ) {
				m_FilesList.Add( element.Value );
				m_bwRenew.ReportProgress( ++i );
			}

			// загрузка из xml-файла в хэш таблицу данных о копиях книг
			loadFromXMLToHashtable( ref m_bwRenew, (SearchCompareModeEnum)m_CompareMode, ref xTree );
			// загрузка списка неоткрываемых книг
			if ( File.Exists( _nonOpenedFB2FilePath ) ) {
				XElement xmlNonOpenedFilesTree = XElement.Load( _nonOpenedFB2FilePath );
				IEnumerable<XElement> nof_files = xmlNonOpenedFilesTree.Elements("File");
				foreach ( XElement element in nof_files )
					_nonOpenedFile.Add( element.Value );
			}
			ControlPanel.Enabled = true;
			
			// Создание списка копий fb2-книг по Группам
			makeBookCopiesGroups( ref m_bwRenew, ref e, (SearchCompareModeEnum)m_CompareMode, ref m_FilesList );
			if ( m_autoResizeColumns )
				MiscListView.AutoResizeColumns( m_listViewFB2Files );
			
		}
		
		// загрузка из xml-файла в хэш таблицу данных о копиях книг для всех режимов
		private void loadFromXMLToHashtable( ref BackgroundWorker bw, SearchCompareModeEnum CompareMode, ref XElement xmlTree ) {
			StatusLabel.Text += "Загрузка в хэш данных обработанных книг...\r";
			ProgressBar.Maximum	= Convert.ToInt32( xmlTree.Element("Groups").Attribute("count").Value );
			if ( CompareMode == SearchCompareModeEnum.AuthorAndTitle )
				ProgressBar.Maximum += Convert.ToInt32( xmlTree.Element("BookTitleGroup").Attribute("count").Value );
			ProgressBar.Value = 0;
			
			// заполнение хэш-таблиц
			m_htWorkingBook.Clear();
			m_htBookTitleAuthors.Clear();
			
			if( CompareMode == SearchCompareModeEnum.AuthorAndTitle ) {
				_loadFromXMLToHashtable( ref bw, ref xmlTree, "Groups", ref m_htBookTitleAuthors );
				_loadFromXMLToHashtable( ref bw, ref xmlTree, "BookTitleGroup", ref m_htWorkingBook );
			} else {
				_loadFromXMLToHashtable( ref bw, ref xmlTree, "Groups", ref m_htWorkingBook );
			}
		}
		
		// загрузка из xml-файла в хэш таблицу данных о копиях книг для всех режимов
		private void _loadFromXMLToHashtable( ref BackgroundWorker bw, ref XElement xmlTree, string Element, ref Hashtable ht ) {
			IEnumerable<XElement> groups = xmlTree.Element(Element).Elements("Group");
			// перебор всех групп копий
			int i = 0;
			foreach ( XElement group in groups ) {
				// перебор всех книг в группе
				IEnumerable<XElement> books = group.Elements("Book");
				foreach ( XElement book in books ) {
					string Group = book.Element("Group").Value;
					BookTitle bookTitle = new BookTitle(book.Element("BookTitle").Value);
					
					// загрузка авторов книги
					IList<Author> authors = null;
					XElement xeAuthors = book.Element("Authors");
					if (xeAuthors != null) {
						authors = new List<Author>();
						IEnumerable<XElement> iexeAuthors = from el in book.Descendants("Author") select el;
						foreach (XElement a in iexeAuthors) {
							XElement xeFirstName = a.Element("FirstName");
							XElement xeMiddleName = a.Element("MiddleName");
							XElement xeLastName = a.Element("LastName");
							XElement xeNickName = a.Element("NickName");
							Author author = new Author( xeFirstName != null ? transformToTextFieldType(xeFirstName.Value) : null,
							                           xeMiddleName != null ? transformToTextFieldType(xeMiddleName.Value) : null,
							                           xeLastName != null ? transformToTextFieldType(xeLastName.Value) : null,
							                           xeNickName != null ? transformToTextFieldType(xeNickName.Value) : null
							                          );
							authors.Add(author);
						}
					}
					
					// загрузка жанров книги
					IList<Genre> genres = null;
					XElement xeGenres = book.Element("Genres");
					if (xeGenres != null) {
						genres = new List<Genre>();
						IEnumerable<XElement> iexeGenre = from el in book.Descendants("Genre") select el;
						foreach ( XElement g in iexeGenre ) {
							if ( g != null ) {
								Genre genre = new Genre( g.Value );
								genre.Math = Convert.ToUInt16( g.Attribute("match").Value );
								genres.Add( genre );
							}
						}
					}

					// данные о книге
					BookData fb2BookData = new BookData( bookTitle, authors, genres,
					                                    book.Element("BookLang").Value, book.Element("BookID").Value,
					                                    book.Element("Version").Value, book.Element("Path").Value,
					                                    book.Element("Encoding").Value );
					//заполнение хеш таблицы данными о fb2-книгах
					if ( !ht.ContainsKey( Group ) ) {
						// такой книги в числе дублей еще нет
						FB2FilesDataInGroup fb2f = new FB2FilesDataInGroup( fb2BookData, Group );
						ht.Add( Group, fb2f );
					} else {
						// такая книга в числе дублей уже есть
						FB2FilesDataInGroup fb2f = (FB2FilesDataInGroup)ht[Group] ;
						fb2f.Add( fb2BookData );
						//ht[Group] = fb2f; //ИЗБЫТОЧНЫЙ КОД
					}
				}
				bw.ReportProgress( ++i );
			}
		}
		#endregion
		
		// =============================================================================================
		// 						Прерывание сравнения: Сохранение данных в xml
		// =============================================================================================
		#region Прерывание сравнения: Сохранение данных в xml
		/// <summary>
		/// Сохранение данных о найденных копиях и о необработанных книгах при прерывании проверки для записи
		/// </summary>
		/// <param name="ToFilePath">Путь к xml-файлу</param>
		/// <param name="CompareMode">Вид сравнения при поиске копий</param>
		/// <param name="SourceDir">Путь к папке сканирования fb2 файлов</param>
		/// <param name="ScanSubDirs">true - сканировать подпапки; false - не сканировать подпапки</param>
		/// <param name="htWorkingBook">Хэш-таблица Групп копий (обработанные файлы)</param>
		/// <param name="FilesList">Список необработанных файлов</param>
		private void saveSearchedDataToXmlFile(string ToFilePath, int CompareMode, string SourceDir, bool ScanSubDirs, Hashtable htWorkingBook, List<string> FilesList) {
			int fileNumber = 0;
			int groupNumber = 0;
			XDocument doc = new XDocument(
				new XDeclaration("1.0", "utf-8", "yes"),
				new XComment("Файл копий fb2 книг, сохраненный после прерывания работы Дубликатора. Используется для возобновления поиска/сравнения"),
				new XElement("Files", new XAttribute("type", "dup_break"),
				             new XComment("Папка для поиска копий книг"),
				             new XElement("SourceDir", SourceDir),
				             new XComment("Настройки поиска-сравнения"),
				             new XElement("Settings",
				                          new XElement("ScanSubDirs", ScanSubDirs),
				                          new XElement("GroupCountForList", cbGroupCountForList.SelectedIndex),
				                          new XElement("SaveGroupToXMLWithoutTree", checkBoxSaveGroupsToXml.Checked)
				                         ),
				             new XComment("Режим поиска-сравнения"),
				             new XElement("CompareMode", new XAttribute("index", CompareMode)),
				             new XComment("Данные о ходе сравнения"),
				             new XElement("CompareData",
				                          new XElement("AllDirs", m_lvFilesCount.Items[(int)FilesCountViewDupCollumnEnum.AllDirs].SubItems[1].Text),
				                          new XElement("AllFiles", m_sv.AllFiles),
				                          new XElement("Groups", m_sv.Group),
				                          new XElement("AllFB2InGroups", m_sv.AllFB2InGroups)
				                         ),
				             new XComment("Обработанные файлы"),
				             new XElement("Groups",
				                          new XAttribute(
				                          	"count",
				                          	(SearchCompareModeEnum)CompareMode == SearchCompareModeEnum.AuthorAndTitle
				                          	? m_htBookTitleAuthors.Count : htWorkingBook.Count)
				                         ),
				             new XComment("Не обработанные файлы"),
				             (SearchCompareModeEnum)CompareMode == SearchCompareModeEnum.AuthorAndTitle
				             ? new XElement("BookTitleGroup", new XAttribute("count", htWorkingBook.Count))
				             : null,
				             new XElement("NotWorkingFiles", new XAttribute("count", FilesList.Count))
				            )
			);
			
			// обработанные книги
			List<string> keyList = null;
			switch ( (SearchCompareModeEnum)CompareMode ) {
				case SearchCompareModeEnum.Md5:
					// 0. Абсолютно одинаковые книги (md5)
					keyList = sortKeys( ref htWorkingBook );
					makeXmlFB2FilesInGroup( ref htWorkingBook, ref keyList, ref groupNumber, ref fileNumber, ref doc, "Groups", FilesList.Count );
					break;
				case SearchCompareModeEnum.BookID:
					// 1. Одинаковый Id Книги (копии и/или разные версии правки одной и той же книги)
					keyList = sortKeys( ref htWorkingBook );
					makeXmlFB2FilesInGroup( ref htWorkingBook, ref keyList, ref groupNumber, ref fileNumber, ref doc, "Groups", FilesList.Count );
					break;
				case SearchCompareModeEnum.BookTitle:
					// 2. Название Книги (могут быть найдены и разные книги разных Авторов, но с одинаковым Названием)
					keyList = sortKeys( ref htWorkingBook );
					makeXmlFB2FilesInGroup( ref htWorkingBook, ref keyList, ref groupNumber, ref fileNumber, ref doc, "Groups", FilesList.Count );
					break;
				case SearchCompareModeEnum.AuthorAndTitle:
					// 3. Автор(ы) и Название Книги (одна и та же книга, сделанная разными людьми - разные Id, но Автор и Название - одинаковые)
					// список обработанных файлов по группам
					keyList = sortKeys( ref htWorkingBook );
					makeXmlFB2FilesInGroup( ref htWorkingBook, ref keyList, ref groupNumber, ref fileNumber, ref doc, "BookTitleGroup", FilesList.Count );
					// список Групп с копиями
					groupNumber = 0;
					keyList = sortKeys( ref m_htBookTitleAuthors );
					makeXmlFB2FilesInGroup( ref m_htBookTitleAuthors, ref keyList, ref groupNumber, ref fileNumber, ref doc, "Groups", FilesList.Count );
					break;
				case SearchCompareModeEnum.AuthorFIO:
					// 4. Авторы с одинаковой Фамилией и инициалами
					keyList = sortKeys( ref htWorkingBook );
					makeXmlFB2FilesInGroup( ref htWorkingBook, ref keyList, ref groupNumber, ref fileNumber, ref doc, "Groups", FilesList.Count );
					break;
			}
			
			// необработанные книги
			if ( FilesList.Count > 0 ) {
				fileNumber = 0;
				for ( int i = 0; i != FilesList.Count; ++i ) {
					doc.Root.Element("NotWorkingFiles").Add(
						new XElement(
							"File", new XAttribute("number", fileNumber++),
							new XElement("Path", FilesList[i])
						)
					);
					if ( m_bw.IsBusy )
						m_bw.ReportProgress( ++i );
					else if ( m_bwRenew.IsBusy )
						m_bwRenew.ReportProgress( ++i );
					else
						++ProgressBar.Value;
				}
			}
			StatusLabel.Text += "\nПодождите, пожалуйста - происходит сохранение данных в файл...";
			doc.Save( ToFilePath );
		}
		
		// сортировка ключей (названия групп)
		private List<string> sortKeys( ref Hashtable hash ) {
			List<string> keyList = new List<string>();
			foreach ( string key in hash.Keys )
				keyList.Add(key);
			keyList.Sort();
			return keyList;
		}
		
		// формирование xml данных групп копий fb2 файлов в xDocument
		private void makeXmlFB2FilesInGroup( ref Hashtable htFB2InGroup, ref List<string> keyList,
		                                    ref int groupNumber, ref int fileNumber,
		                                    ref XDocument doc, string ToElement, int filesListCount ) {
			if ( keyList.Count > 0 ) {
				ProgressBar.Maximum	= keyList.Count + filesListCount;
				ProgressBar.Value	= 0;
				XElement xeGroup	= null;
				int i = 0;
				foreach ( string key in keyList ) {
					FB2FilesDataInGroup fb2f = (FB2FilesDataInGroup)htFB2InGroup[key];
					doc.Root.Element(ToElement).Add(
						xeGroup = new XElement("Group",
						                       new XAttribute("number", groupNumber++),
						                       new XAttribute("count", fb2f.Count),
						                       new XAttribute("name", fb2f.Group)
						                      )
					);
					// формирование xml данных о книге в xDocument
					makeXmlBookData( ref xeGroup, fb2f, ref fileNumber );
					if ( m_bw.IsBusy )
						m_bw.ReportProgress( ++i );
					else if ( m_bwRenew.IsBusy )
						m_bwRenew.ReportProgress( ++i );
					else
						++ProgressBar.Value;
				}
			}
		}

		// формирование xml данных о книге в xDocument
		private void makeXmlBookData( ref XElement xeGroup, FB2FilesDataInGroup fb2f, ref int fileNumber ) {
			XElement xeAuthors;
			XElement xeGenres;
			foreach ( BookData bd in fb2f ) {
				xeGroup.Add(new XElement("Book", new XAttribute("number", fileNumber++),
				                         new XElement("Group", fb2f.Group),
				                         new XElement("Path", bd.Path),
				                         new XElement("BookLang", bd.Lang),
				                         new XElement("BookID", bd.Id),
				                         new XElement("Encoding", bd.Encoding),
				                         new XElement("Version", bd.Version),
				                         new XElement("BookTitle", MakeBookTitleString( bd.BookTitle )),
				                         xeAuthors = new XElement("Authors"),
				                         xeGenres = new XElement("Genres")
				                        )
				           );
				// сохранение данных об авторах конкретной книги в xml-файл
				if ( bd.Authors != null ) {
					XElement xeAuthor = null;
					foreach ( Author a in bd.Authors ) {
						xeAuthors.Add( xeAuthor = new XElement("Author") );
						if ( a.LastName != null && a.LastName.Value != null )
							xeAuthor.Add(new XElement("LastName", a.LastName.Value));
						if ( a.FirstName != null && a.FirstName.Value != null )
							xeAuthor.Add(new XElement("FirstName", a.FirstName.Value));
						if ( a.MiddleName != null && a.MiddleName.Value != null )
							xeAuthor.Add(new XElement("MiddleName", a.MiddleName.Value));
						if ( a.NickName != null && a.NickName.Value != null )
							xeAuthor.Add(new XElement("NickName", a.NickName.Value));
					}
				}
				// сохранение данных о жанрах конкретной книги в xml-файл
				if ( bd.Genres != null ) {
					foreach ( Genre g in bd.Genres ) {
						if ( g.Name != null )
							xeGenres.Add( new XElement("Genre", new XAttribute("match", g.Math), g.Name) );
					}
				}
			}
		}

		private TextFieldType transformToTextFieldType(string Value) {
			TextFieldType textField = new TextFieldType();
			textField.Value = Value;
			return textField;
		}
		#endregion

		#endregion
		
		// =============================================================================================
		// 							ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ И АЛГОРИТМЫ КЛАССА
		// =============================================================================================
		#region Вспомогательные методы и алгоритмы класса
		// создание хеш-таблицы для групп одинаковых книг
		private bool AddBookGroupInHashTable( ref Hashtable groups, ref ListViewGroup lvg ) {
			if ( groups != null ){
				if ( !groups.Contains( lvg.Header ) ) {
					groups.Add( lvg.Header, lvg );
					return true;
				}
			}
			return false;
		}
		
		// формирование представления Групп с их книгами
		private void makeBookCopiesView( FB2FilesDataInGroup fb2BookList ) {
			Hashtable htBookGroups = new Hashtable( new FB2CultureComparer() ); // хеш-таблица групп одинаковых книг
			ListViewGroup lvGroup = null; // группа одинаковых книг
			string Valid = string.Empty;
			foreach ( BookData bd in fb2BookList ) {
				++m_sv.AllFB2InGroups; // число книг во всех группах одинаковых книг
				lvGroup = new ListViewGroup( fb2BookList.Group );
				ListViewItem lvi = new ListViewItem( bd.Path );
				if ( FilesWorker.isFB2Archive( bd.Path ) )
					lvi.ForeColor = Colors.ZipFB2ForeColor;
				lvi.SubItems.Add( MakeBookTitleString( bd.BookTitle ) );
				lvi.SubItems.Add( MakeAuthorsString( bd.Authors ) );
				lvi.SubItems.Add( MakeGenresString( bd.Genres ) );
				lvi.SubItems.Add( bd.Lang );
				lvi.SubItems.Add( bd.Id );
				lvi.SubItems.Add( bd.Version );
				lvi.SubItems.Add( bd.Encoding );

				Valid = m_fv2Validator.ValidatingFB2File( bd.Path );
				if ( string.IsNullOrEmpty( Valid )  ) {
					Valid = "Да";
					lvi.ForeColor = FilesWorker.isFB2File( bd.Path ) ? Color.FromName( "WindowText" ) : Colors.ZipFB2ForeColor;
				} else {
					Valid = "Нет";
					lvi.ForeColor = Colors.FB2NotValidForeColor;
				}

				lvi.SubItems.Add( Valid );
				lvi.SubItems.Add( GetFileLength( bd.Path ) );
				lvi.SubItems.Add( GetFileCreationTime( bd.Path ) );
				lvi.SubItems.Add( FileLastWriteTime( bd.Path ) );
				// заносим группу в хеш, если она там отсутствует
				AddBookGroupInHashTable( ref htBookGroups, ref lvGroup );
				// присваиваем группу книге
				m_listViewFB2Files.Groups.Add( (ListViewGroup)htBookGroups[fb2BookList.Group] );
				lvi.Group = (ListViewGroup)htBookGroups[fb2BookList.Group];
				m_listViewFB2Files.Items.Add( lvi );
			}
		}
		
		// формирование строки с Названием Книги
		private string MakeBookTitleString( BookTitle bookTitle ) {
			if ( bookTitle == null )
				return string.Empty;
			return bookTitle.Value ?? string.Empty; //return bookTitle.Value != null ? bookTitle.Value : string.Empty;
		}
		
		// формирование строки с Авторами Книги из списка всех Авторов ЭТОЙ Книги
		private string MakeAuthorsString( IList<Author> Authors ) {
			if ( Authors != null ) {
				string sA = string.Empty;
				foreach ( Author a in Authors ) {
					if ( a.LastName != null && !string.IsNullOrWhiteSpace( a.LastName.Value ) )
						sA += a.LastName.Value.Trim() + " ";
					if ( a.FirstName != null && !string.IsNullOrWhiteSpace( a.FirstName.Value ) )
						sA += a.FirstName.Value.Trim() + " ";
					if ( a.MiddleName != null && !string.IsNullOrWhiteSpace( a.MiddleName.Value ) )
						sA += a.MiddleName.Value.Trim() + " ";
					if ( a.NickName != null && !string.IsNullOrWhiteSpace( a.NickName.Value ) )
						sA += a.NickName.Value.Trim();
					sA = sA.Trim();
					if ( !string.IsNullOrWhiteSpace( sA ) )
						sA += "; ";
				}
				return sA.LastIndexOf( ';' ) > -1
					? sA.Substring( 0, sA.LastIndexOf( ';' ) ).Trim()
					: sA.Trim();
			}
			return string.Empty;
		}
		
		// формирование строки с Жанрами Книги из списка всех Жанров ЭТОЙ Книги
		private string MakeGenresString( IList<Genre> Genres ) {
			if ( Genres != null ) {
				string sG = string.Empty;
				foreach ( Genre g in Genres ) {
					if ( !string.IsNullOrWhiteSpace( g.Name ) )
						sG += g.Name.Trim();
					sG = sG.Trim();
					if ( !string.IsNullOrWhiteSpace( sG ) )
						sG += "; ";
				}
				sG = sG.Trim();
				return sG.LastIndexOf( ';' ) > -1
					? sG.Substring( 0, sG.LastIndexOf( ';' ) ).Trim()
					: sG.Trim();
			}
			return string.Empty;
		}
		
		/// <summary>
		/// Сортировка ключей (групп)
		/// </summary>
		/// <param name="htBookGroups">Хэш-таблица Групп</param>
		/// <returns>Список List объектов типа string значений отсортированных ключей хэш-таблицы</returns>
		private List<string> makeSortedKeysForGroups( ref Hashtable htBookGroups ) {
			List<string> keyList = new List<string>();
			foreach ( string key in htBookGroups.Keys )
				keyList.Add(key);
			keyList.Sort();
			return keyList;
		}
		
		// Вычисление SHA1 файла
		private string ComputeSHA1Checksum(string path) {
			using (FileStream fs = System.IO.File.OpenRead(path)) {
				SHA1CryptoServiceProvider sha1 = new SHA1CryptoServiceProvider();
				byte[] fileData = new byte[fs.Length];
				fs.Read(fileData, 0, (int)fs.Length);
				byte[] checkSum = sha1.ComputeHash(fileData);
				string hash = BitConverter.ToString(checkSum).Replace("-", String.Empty);
				return hash;
			}
		}
		
		// Вычисление MD5 файла
		private string ComputeMD5Checksum(string path) {
			using (FileStream fs = System.IO.File.OpenRead(path)) {
				MD5 md5 = new MD5CryptoServiceProvider();
				byte[] fileData = new byte[fs.Length];
				fs.Read(fileData, 0, (int)fs.Length);
				byte[] checkSum = md5.ComputeHash(fileData);
				string result = BitConverter.ToString(checkSum).Replace("-", String.Empty);
				return result;
			}
		}
		
		// Отображение результата поиска сравнения
		private void ViewDupProgressData() {
			MiscListView.ListViewStatus( m_lvFilesCount, (int)FilesCountViewDupCollumnEnum.AllBooks, m_sv.AllFiles );
			MiscListView.ListViewStatus( m_lvFilesCount, (int)FilesCountViewDupCollumnEnum.AllGroups, m_sv.Group );
			MiscListView.ListViewStatus( m_lvFilesCount, (int)FilesCountViewDupCollumnEnum.AllBoolsInAllGroups, m_sv.AllFB2InGroups );
		}
		
		private string GetFileLength( string sFilePath ) {
			FileInfo fi = new FileInfo( sFilePath );
			return fi.Exists ? FilesWorker.FormatFileLength( fi.Length ) : string.Empty;
		}
		
		// время создания файла
		private  string GetFileCreationTime( string sFilePath ) {
			FileInfo fi = new FileInfo( sFilePath );
			return fi.Exists ? fi.CreationTime.ToString() : string.Empty;
		}
		
		// время последней записи в файл
		private  string FileLastWriteTime( string sFilePath ) {
			FileInfo fi = new FileInfo( sFilePath );
			return fi.Exists ? fi.LastWriteTime.ToString() : string.Empty;
		}
		
		// остановка поиска / возобновления поиска из xml
		// StopForSaveToXml: false - остановка поиска; true - остановка возобновления поиска
		private void StopCompare( bool StopForSaveToXml )
		{
			m_StopToSave = StopForSaveToXml;
			if ( m_bw.IsBusy ) {
				if( m_bw.WorkerSupportsCancellation )
					m_bw.CancelAsync();
			} else {
				if ( m_bwRenew.WorkerSupportsCancellation )
					m_bwRenew.CancelAsync();
			}
		}
		
		// список файлов, которые не никак удалось открыть
		private void collectBadFB2( string FilePath ) {
			if ( _nonOpenedFile.Count > 0 ) {
				foreach ( string file in _nonOpenedFile ) {
					if ( file != FilePath ) {
						_nonOpenedFile.Add( FilePath );
						break;
					}
				}
			} else {
				_nonOpenedFile.Add( FilePath );
			}
		}
		
//		// создание "пустого" BookData
//		private BookData makeEmptyBookData() {
//			IList<Author> authors = new List<Author>();
//			authors.Add(
//				new Author(
//					new TextFieldType(string.Empty), new TextFieldType(string.Empty), new TextFieldType(string.Empty)
//				)
//			);
//
//			IList<Genre> genres = new List<Genre>();
//			genres.Add( new Genre("other") );
//
//			return new BookData(
//				new BookTitle(string.Empty), authors, genres, "ru", "EMPTY ID", "1.0", string.Empty, "UTF-8"
//			);
//		}

		#endregion
		
		// =============================================================================================
		// 							СОХРАНЕНИЕ РЕЗУЛЬТАТА ПОИСКА КОПИЙ В XML-ФАЙЛЫ
		// =============================================================================================
		#region Сохранение результата поиска копий в xml-файлы
		/// <summary>
		/// сохранение результата сразу в xml-файлы без построения визуального списка
		/// </summary>
		/// <param name="GroupCountForList">Число Групп в Списке Групп</param>
		/// <param name="CompareMode">Вид сравнения при поиске копий</param>
		/// <param name="CompareModeName">Название вида сравнения при поиске копий</param>
		/// <param name="ht">Хэш-таблица данных на Группы (копии fb2 книг по Группам)</param>
		private void saveCopiesListToXml( ref BackgroundWorker bw, ref DoWorkEventArgs e, int GroupCountForList,
		                                 int CompareMode, string CompareModeName, ref Hashtable ht ) {
			// блокировка отмены сохранения результата в файлы
			ControlPanel.Enabled = false;
			
			StatusLabel.Text += "Сохранение результата поиска в xml-файлы (папка '_Copies') без построения дерева копий...\r";
			ProgressBar.Maximum	= ht.Values.Count;
			ProgressBar.Value	= 0;
			
			const string ToDirName = "_Copies";
			if ( !Directory.Exists( ToDirName ) )
				Directory.CreateDirectory( ToDirName );
			
			// "сквозной" счетчик числа групп для каждого создаваемого xml файла копий
			int ThroughGroupCounterForXML = 0;
			// счетчик (в границых CompareModeName) числа групп для каждого создаваемого xml файла копий
			int GroupCounterForXML = 0;
			// номер файла - для формирования имени создаваемого xml файла копий
			int XmlFileNumber = 0;

			// копии fb2 книг по группам
			if ( ht.Values.Count > 0 ) {
				XDocument doc = createXMLStructure( CompareMode, CompareModeName );
				
				int BookInGroups = 0; 		// число книг (books) в Группах (Groups)
				int GroupCountInGroups = 0; // число Групп (Group count) в Группах (Groups)
				int i = 0;					// прогресс
				bool one = false;
				// сортировка ключей (групп)
				List<string> keyList = makeSortedKeysForGroups( ref ht );
				foreach ( string key in keyList ) {
					if ( ( bw.CancellationPending ) )  {
						e.Cancel = true;
						return;
					}
					++m_sv.Group; // число групп одинаковых книг
					// формирование представления Групп с их книгами
					addAllBookInGroup( ref bw, ref e, ref doc, (FB2FilesDataInGroup)ht[key], ref BookInGroups, ref GroupCountInGroups );

					++GroupCounterForXML;
					++ThroughGroupCounterForXML;
					doc.Root.Element("SelectedItem").SetValue("0");
					if ( GroupCountForList <= ht.Values.Count ) {
						if ( GroupCounterForXML >= GroupCountForList ) {
							string FileNumber = StringProcessing.makeNNNStringOfNumber( ++XmlFileNumber ) + ".dup_lbc";
							setDataForNode( ref doc, GroupCountInGroups, BookInGroups );
							doc.Save( Path.Combine( ToDirName, FileNumber ) );
							StatusLabel.Text += "Файл: '_Copies\\" + FileNumber + "' создан...\r";
							doc.Root.Element("Groups").Elements().Remove();
							GroupCountInGroups = 0;
							GroupCounterForXML = 0;
							BookInGroups = 0;
						} else {
							// последний диаппазон Групп
							if ( ThroughGroupCounterForXML == ht.Values.Count ) {
								string FileNumber = StringProcessing.makeNNNStringOfNumber( ++XmlFileNumber ) + ".dup_lbc";
								setDataForNode( ref doc, GroupCountInGroups, BookInGroups );
								doc.Save( Path.Combine( ToDirName, FileNumber ) );
								StatusLabel.Text += "Файл: '_Copies\\" + FileNumber + "' создан...\r";
							}
						}
					} else {
						setDataForNode( ref doc, GroupCountInGroups, BookInGroups );
						one = true;
					}
					bw.ReportProgress( i++ );
				} // по всем Группам
				if ( one ) {
					StatusLabel.Text += "Файл: '_Copies\001.dup_lbc' ...\r";
					doc.Save( Path.Combine( ToDirName, "001.dup_lbc" ) );
				}
			}
		}
		/// <summary>
		/// Добавление Группы в Список Групп
		/// </summary>
		/// <param name="doc">xml документ - объект класса XDocument, в который заносятся данные на книги Групп</param>
		/// <param name="fb2BookList">Список данных fb2 книг для конкретной Группы</param>
		/// <param name="BookInGroups">Число книг в Группе</param>
		/// <param name="GroupCountInGroups">Счетчик числа Групп</param>
		private void addAllBookInGroup( ref BackgroundWorker bw, ref DoWorkEventArgs e,
		                               ref XDocument doc, FB2FilesDataInGroup fb2BookList,
		                               ref int BookInGroups, ref int GroupCountInGroups ) {
			BookInGroups += fb2BookList.Count;
			
			// Добавление Группы в Список Групп
			XElement xeGroup = null;
			doc.Root.Element("Groups").Add(
				xeGroup = new XElement(
					"Group", new XAttribute("number", 0),
					new XAttribute("count", fb2BookList.Count),
					new XAttribute("name", fb2BookList.Group)
				)
			);
			
			int BookNumber = 0;			// номер Книги (Book number) В Группе (Group)
			int BookCountInGroup = 0;	// число Книг (Group count) в Группе (Group)
			
			foreach ( BookData bd in fb2BookList ) {
				++m_sv.AllFB2InGroups; // число книг во всех группах одинаковых книг
				string sForeColor = "WindowText";
				if ( FilesWorker.isFB2Archive( bd.Path ) )
					sForeColor = Colors.ZipFB2ForeColor.Name;
				string Validation = m_fv2Validator.ValidatingFB2File( bd.Path );
				if ( string.IsNullOrEmpty( Validation )  ) {
					Validation = "Да";
					sForeColor = FilesWorker.isFB2File( bd.Path ) ? "WindowText" : Colors.ZipFB2ForeColor.Name;
				} else {
					Validation = "Нет";
					sForeColor = Colors.FB2NotValidForeColor.Name;
				}
				// Добавление Книги в Группу
				xeGroup.Add(
					new XElement("Book", new XAttribute("number", ++BookNumber),
					             new XElement("Group", fb2BookList.Group),
					             new XElement("Path", bd.Path),
					             new XElement("BookTitle", MakeBookTitleString( bd.BookTitle )),
					             new XElement("Authors", MakeAuthorsString( bd.Authors )),
					             new XElement("Genres", MakeGenresString( bd.Genres )),
					             new XElement("BookLang", bd.Lang),
					             new XElement("BookID", bd.Id),
					             new XElement("Version", bd.Version),
					             new XElement("Encoding", bd.Encoding),
					             new XElement("Validation", Validation),
					             new XElement("FileLength", GetFileLength( bd.Path )),
					             new XElement("FileCreationTime", GetFileCreationTime( bd.Path )),
					             new XElement("FileLastWriteTime", FileLastWriteTime( bd.Path )),
					             new XElement("ForeColor", sForeColor),
					             new XElement("BackColor", "Window"),
					             new XElement("IsChecked", false)
					            )
				);
				
				xeGroup.SetAttributeValue( "count", ++BookCountInGroup );
				if ( !xeGroup.HasElements ) {
					xeGroup.Remove();
				}
			} // по всем книгам Группы
			++GroupCountInGroups;
		}
		/// <summary>
		/// Заполнение данными ноды для генерируемых файлов списка копий
		/// </summary>
		/// <param name="doc">xml документ - объект класса XDocument, в который заносятся данные на книги Групп</param>
		/// <param name="GroupCountInGroups">Счетчик числа Групп</param>
		/// <param name="BookInGroups">Число книг в Группе</param>
		private void setDataForNode( ref XDocument doc, int GroupCountInGroups, int BookInGroups ) {
			XElement xCompareData = doc.Root.Element("CompareData");
			if ( xCompareData != null ) {
				xCompareData.SetElementValue("Groups", GroupCountInGroups);
				xCompareData.SetElementValue("AllFB2InGroups", BookInGroups);
			}
			// заполнение аттрибутов
			XElement xGroups = doc.Root.Element("Groups");
			if ( xGroups != null ) {
				xGroups.SetAttributeValue("count", GroupCountInGroups);
				xGroups.SetAttributeValue("books", BookInGroups);
				IEnumerable<XElement> Groups = xGroups.Elements("Group");
				int i = 0;
				foreach ( XElement Group in Groups )
					Group.SetAttributeValue( "number", ++i );
			}
		}
		/// <summary>
		/// Создание основных разделов xml структуры объекта класса XDocument
		/// </summary>
		/// <param name="CompareMode">Вид сравнения при поиске копий</param>
		/// <param name="CompareModeName">Название вида сравнения при поиске копий</param>
		/// <returns>Объект класса XDocument </returns>
		private XDocument createXMLStructure( int CompareMode, string CompareModeName ) {
			return new XDocument(
				new XDeclaration("1.0", "utf-8", "yes"),
				new XComment("Файл копий fb2 книг, сохраненный после полного окончания работы Дубликатора"),
				new XElement("Files", new XAttribute("type", "dup_endwork"),
				             new XComment("Папка для поиска копий fb2 книг"),
				             new XElement("SourceDir", m_Source),
				             new XComment("Настройки поиска-сравнения fb2 книг"),
				             new XElement("Settings",
				                          new XElement("ScanSubDirs", m_ScanSubDirs),
				                          new XElement("GroupCountForList", cbGroupCountForList.SelectedIndex),
				                          new XElement("SaveGroupToXMLWithoutTree", checkBoxSaveGroupsToXml.Checked)),
				             new XComment("Режим поиска-сравнения fb2 книг"),
				             new XElement("CompareMode",
				                          new XAttribute("index", CompareMode),
				                          new XElement("Name", CompareModeName)),
				             new XComment("Данные о ходе сравнения fb2 книг"),
				             new XElement("CompareData",
				                          new XElement("AllDirs", m_lvFilesCount.Items[(int)FilesCountViewDupCollumnEnum.AllDirs].SubItems[1].Text),
				                          new XElement("AllFiles", m_lvFilesCount.Items[(int)FilesCountViewDupCollumnEnum.AllBooks].SubItems[1].Text),
				                          new XElement("Groups", "0"),
				                          new XElement("AllFB2InGroups", "0")
				                         ),
				             new XComment("Копии fb2 книг по группам"),
				             new XElement("Groups",
				                          new XAttribute("count", "0"),
				                          new XAttribute("books", "0")
				                         ),
				             new XComment("Выделенный элемент списка, на котором завершили обработку книг"),
				             new XElement("SelectedItem", "-1" )
				            )
			);
		}
		#endregion
		
		// =============================================================================================
		// 								ОБРАБОТЧИКИ СОБЫТИЙ
		// =============================================================================================
		#region Обработчики событий контролов
		void BtnSaveToXmlClick(object sender, EventArgs e)
		{
			StopCompare( true );
		}
		void BtnStopClick(object sender, EventArgs e)
		{
			StopCompare( false );
		}
		void CheckBoxSaveGroupsToXmlCheckedChanged(object sender, EventArgs e)
		{
			lblGroupCountForList.Enabled = checkBoxSaveGroupsToXml.Checked;
			cbGroupCountForList.Enabled = checkBoxSaveGroupsToXml.Checked;
		}
		#endregion
		
	}
}
