﻿/*
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
	/// Форма прогресса поиска копий fb2 книг
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
		private BackgroundWorker _bw		= null; // фоновый обработчик для Непрерывного сравнения
		private BackgroundWorker _bwRenew	= null; // фоновый обработчик для Возобновления сравнения

        private CompareCommon       _compComm           = new CompareCommon();      // Общие методы для различных режимов сравнение книг
        private CompareMd5          _compareMd5         = new CompareMd5();         // Сравнение книг по Md5
        private CompareBookID       _compareBookID      = new CompareBookID();      // Сравнение книг по ID Книги
        private CompareBookTitle    _сompareBookTitle   = new CompareBookTitle();   // Сравнение книг по Названию Книги
        private CompareAuthorFIO    _compareAuthorFIO   = new CompareAuthorFIO();   // Авторы с одинаковыми Фамилиями и инициалами
        private CompareAuthorBookTitle _compareAuthorBookTitle = new CompareAuthorBookTitle();   // Одинаковые Автор(ы) и Название Книги (одна и та же книга, сделанная разными людьми - разные Id, но Автор и Название - одинаковые)
        private CompareAuthorBookTitleBookID    _compareAuthorBookTitleBookID = new CompareAuthorBookTitleBookID(); // Одинаковые Автор(ы), Название и Одинаковый Id Книги (разделять по разным группам разные издания книг)
        private CompareBookTitleBookID          _compareBookTitleBookID = new CompareBookTitleBookID();   // Одинаковые Название Книги и Id Книги (Авторы книги могут быть разными)
        private CompareFB2Author _compareFB2Author = new CompareFB2Author(); // Одинаковые Автор(ы), Название Книги и Автор fb2 файла(одна и та же книга, сделанная разными людьми)
        private CompareAuthorFromFB2 _compareAuthorFromFB2 = new CompareAuthorFromFB2(); // Сравнение книг по одинаковым Авторам
        private CompareAuthorBookID _compareAuthorBookID = new CompareAuthorBookID(); // Сравнение книг по одинаковым Авторам Книг и ID книг

        private readonly string _TempDir		= Settings.Settings.TempDirPath;
		private const string	_sMessTitle		= "SharpFBTools - Поиск одинаковых fb2 файлов";
		
		private string	_Source			= string.Empty;
		private bool 	_ScanSubDirs	= false;
		private int		_CompareMode	= 0; 			// режим сравнения книг на определение копий
		private string	_CompareModeName= string.Empty; // название режима сравнения книг на определение копий
		private readonly bool	_autoResizeColumns	= false;
		private readonly string _fromXmlPath		= null;	// null - полное сканирование; Путь - возобновление сравнения их xml
		
		private readonly StatusView			_sv 			= new StatusView();
		private readonly EndWorkMode		_EndMode		= new EndWorkMode();
		private readonly SharpZipLibWorker	_sharpZipLib	= new SharpZipLibWorker();
		
		// таблица для обработанных файлов - копий
		private Hashtable _htWorkingBook		= new Hashtable( new FB2CultureComparer() );
		// таблица для обработанных данных копий (режим группировки по Авторам и по Названиям Книг)
		private Hashtable _htBookTitleAuthors	= new Hashtable( new FB2CultureComparer() );
        // таблица для обработанных данных копий (режим группировки по Название Книги и Id Книги)
        private Hashtable _htBookTitleBookID    = new Hashtable(new FB2CultureComparer());
        // таблица для обработанных данных копий (режим группировки по Автор(ы), Название и Одинаковый Id Книги)
        private Hashtable _htBookTitleAuthorsBookID = new Hashtable(new FB2CultureComparer());
        // таблица для обработанных данных копий (режим группировки по Авторам)
        private Hashtable _htAuthors = new Hashtable(new FB2CultureComparer());
        // таблица для обработанных данных копий (режим группировки по Авторам, ID Книги)
        private Hashtable _htAuthorsBookID = new Hashtable(new FB2CultureComparer());

        private List<string> _FilesList				= new List<string>();
		private List<string> _nonOpenedFile			= new List<string>();
		private const string _nonOpenedFB2FilePath	= "_DuplicatorNonOpenedFile.xml";
		
		private readonly TextBox  _tboxSourceDir;
		private readonly CheckBox _chBoxScanSubDir;
        private readonly ComboBox _cboxMode;
		private readonly CheckBox _checkBoxSaveGroupsToXml;
		private readonly ToolStripComboBox _tscbGroupCountForList;
		private readonly ListView	_lvFilesCount		= new ListView();
		private readonly ListView	_listViewFB2Files	= new ListView();

        // Учитывать ли отчество Авторов (true) или нет (false) при сравнении
        private readonly bool _WithMiddleName = false;

        // true, если остановка с сохранением необработанного списка книг в файл.
        private bool _StopToSave = false;
		private DateTime _dtStart = DateTime.Now;
		#endregion
		
		/// <summary>
		/// Форма прогресса поиска копий fb2 книг
		/// </summary>
		/// <param name="fromXmlPath">null, если Новое сканирование книг; иначе - Путь к xml-файлу для Возобновления сканирования книг</param>
		/// <param name="tbSourceDir">Папка с книгами (экземпляр класса TextBox)</param>
		/// <param name="cbScanSubDir">Сканировать ли и подкаталоги (экземпляр класса CheckBox)</param>
		/// <param name="cboxMode">Режим  сравнения книг (экземпляр класса ComboBox)</param>
		/// <param name="cbSaveGroupsToXml">Сохранять ли результат сразу в XML-файл, без построения визуального дерева (экземпляр класса CheckBox)</param>
		/// <param name="tscboxGroupCountForList">Число групп, сохраняемых в XML-файл (экземпляр класса ToolStripComboBox)</param>
		/// <param name="listViewFilesCount">Экземпляр класса ListView для отображения статистики</param>
		/// <param name="listViewFB2Files">Экземпляр класса ListView для отображения данных на книги</param>
		/// <param name="AutoResizeColumns">true - Автоподстройка размера столбцов; false - нет.</param>
		public CompareForm(
			string fromXmlPath,
			ref TextBox tbSourceDir, ref CheckBox cbScanSubDir, ref ComboBox cboxMode,
			ref CheckBox cbSaveGroupsToXml, ref ToolStripComboBox tscboxGroupCountForList,
			ref ListView listViewFilesCount, ref ListView listViewFB2Files, bool AutoResizeColumns
		)
		{
			InitializeComponent();
			
			_tboxSourceDir				= tbSourceDir;
			_chBoxScanSubDir			= cbScanSubDir;
            _cboxMode					= cboxMode;
			_checkBoxSaveGroupsToXml	= cbSaveGroupsToXml;
			_tscbGroupCountForList		= tscboxGroupCountForList;
			_lvFilesCount				= listViewFilesCount;
			_listViewFB2Files			= listViewFB2Files;
			
			_Source				= _tboxSourceDir.Text.Trim();
			_ScanSubDirs		= _chBoxScanSubDir.Checked;
			_CompareMode		= _cboxMode.SelectedIndex;
			_CompareModeName	= _cboxMode.Text;
			
			_autoResizeColumns	= AutoResizeColumns;
			
			InitializeBackgroundWorker();
			InitializeRenewBackgroundWorker();
			
			// Запуск процесса DoWork от RunWorker
			if ( fromXmlPath == null ) {
				if ( !_bw.IsBusy )
					_bw.RunWorkerAsync(); // если не занят, то запустить процесс
			} else {
				_fromXmlPath = fromXmlPath; // Путь к xml для возобновления поиска копий fb2 книг
				if ( !_bwRenew.IsBusy )
					_bwRenew.RunWorkerAsync(); // если не занят. то запустить процесс
			}
		}

		// =============================================================================================
		// 								ОТКРЫТЫЕ СВОЙСТВА
		// =============================================================================================
		#region Открытые свойства
		public virtual EndWorkMode EndMode {
			get { return _EndMode; }
		}
		#endregion
		
		// =============================================================================================
		// 								ОТКРЫТЫЕ МЕТОДЫ
		// =============================================================================================
		#region Открытые методы
		public bool IsStopToXmlClicked() {
			return _StopToSave;
		}
		
		public string getSourceDirFromRenew() {
			return _Source;
		}
		#endregion
		
		// =============================================================================================
		// 				BACKGROUNDWORKER ДЛЯ НЕПРЕРЫВНОГО СРАВНЕНИЯ и ПРЕРЫВАНИЯ / ВОЗОБНОВЛЕНИЯ
		// =============================================================================================
		#region BackgroundWorker для Непрерывного сравнения и прерывания / возобновления
		
		// =============================================================================================
		//			            BackgroundWorker: Непрерывное Сравнение
		// =============================================================================================
		#region BackgroundWorker: Непрерывное Сравнение
		private void InitializeBackgroundWorker() {
			// Инициализация перед использование BackgroundWorker
			_bw = new BackgroundWorker();
			_bw.WorkerReportsProgress		= true; // Позволить выводить прогресс процесса
			_bw.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
			_bw.DoWork 				+= new DoWorkEventHandler( bw_DoWork );
			_bw.ProgressChanged 	+= new ProgressChangedEventHandler( bw_ProgressChanged );
			_bw.RunWorkerCompleted	+= new RunWorkerCompletedEventHandler( bw_RunWorkerCompleted );
		}
		
		// поиск одинаковых fb2-файлов
		private void bw_DoWork( object sender, DoWorkEventArgs e ) {
			_dtStart = DateTime.Now;
			cbGroupCountForList.SelectedIndex	= _tscbGroupCountForList.SelectedIndex;
			checkBoxSaveGroupsToXml.Checked		= _checkBoxSaveGroupsToXml.Checked;
			ControlPanel.Enabled = false;
			
			StatusLabel.Text += "Создание списка файлов для поиска копий fb2 книг...\r";
			List<string> lDirList = new List<string>();
			_FilesList.Clear();
			if ( !_ScanSubDirs )
				// сканировать только указанную папку
				_sv.AllFiles = FilesWorker.makeFilesListFromDir( _Source, ref _FilesList, true );
			else {
				// сканировать и все подпапки
				FilesWorker.recursionDirsSearch( _Source, ref lDirList, true ).ToString();
				_sv.AllFiles = FilesWorker.makeFilesListFromDirs( ref _bw, ref e, ref lDirList, ref _FilesList, true );
			}

			_bw.ReportProgress( 0 ); // отобразим данные в контролах

			if ( _bw.CancellationPending ) {
				e.Cancel = true;
				return;
			}
			
			// проверка, есть ли хоть один файл в папке для сканирования
			if ( _sv.AllFiles == 0 ) {
				MessageBox.Show(
					"В папке сканирования не найдено ни одного файла!\nРабота прекращена.",
					_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information
				);
				return;
			}
			
			this.Text += String.Format(
				": Всего {0} каталогов; {1} файлов", lDirList.Count, _sv.AllFiles
			);
			lDirList.Clear();

            // Очистка всех хеш таблиц
            ClearAllHashtables();

            // Сравнение fb2-файлов
            ControlPanel.Enabled = true;
			// Создание списка копий fb2-книг по Группам
			makeBookCopiesGroups(
				ref _bw, ref e, (SearchCompareModeEnum) _CompareMode, ref _FilesList
			);
			if ( _autoResizeColumns )
				MiscListView.AutoResizeColumns( _listViewFB2Files );
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

			string sTime = dtEnd.Subtract( _dtStart ).ToString().Substring( 0, 8 ) + " (час.:мин.:сек.)";
            if ( e.Cancelled ) {
                _EndMode.EndMode = EndWorkModeEnum.Cancelled;
				_EndMode.Message = "Поиск одинаковых fb2-файлов остановлен!\nСписок (псевдодерево) Групп копий fb2-файлов не сформирован полностью!\nЗатрачено времени: " + sTime;
				if ( _StopToSave ) {
					// остановка поиска копий с сохранением списка необработанных книг в файл
					_StopToSave = false;
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
						saveSearchedDataToXmlFile(
							sfdList.FileName, _CompareMode, _Source, _ScanSubDirs, _htWorkingBook, _FilesList
						);
						_EndMode.Message = "Поиск одинаковых fb2 файлов прерван!\nДанные поиска и список оставшихся для обработки книг сохранены в xml-файл:\n\n"+sfdList.FileName+"\n\nЗатрачено времени: "+sTime;
					}
				}
			} else if ( e.Error != null ) {
				_EndMode.EndMode = EndWorkModeEnum.Error;
				_EndMode.Message = "Ошибка:\n" + e.Error.Message + "\n" + e.Error.StackTrace + "\nЗатрачено времени: "+sTime;
			} else {
				_EndMode.EndMode = EndWorkModeEnum.Done;
				_EndMode.Message = "Поиск одинаковых fb2-файлов завершен!\nЗатрачено времени: "+sTime;
				if ( checkBoxSaveGroupsToXml.Checked ) {
					_EndMode.Message += "\n\nРезультат поиска (Группы копий) сохранен в папку '_Copies'";
				} else {
					if ( _listViewFB2Files.Items.Count == 0 )
						_EndMode.Message += "\n\nНе найдено НИ ОДНОЙ копии книг!";
				}
			}
			
			if ( _nonOpenedFile.Count > 0 ) {
				_EndMode.Message += string.Format(
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
			
			_sv.Clear();
			_FilesList.Clear();
            _nonOpenedFile.Clear();

            // Очистка всех хеш таблиц
            ClearAllHashtables();

            this.Close();
		}
        #endregion

        // =====================================================================================================
        //	Общие методы для Полного и Прерванного сканирования Алгоритмы создания списков копий книг по Группам
        // =====================================================================================================
        #region Общие для Полного и Прерванного сканирования Алгоритмы создания списков копий книг по Группам
        /// <summary>
        /// Создание списка копий fb2-книг по Группам
        /// </summary
        /// <param name="bw">Экземплар фонового обработчика класса BackgroundWorker</param>
        /// <param name="e">Экземпляр класса DoWorkEventArgs</param>
        /// <param name="CompareMode">Режим сравнения книг</param>
        /// <param name="FilesList">Список файлов для сканирования</param>
        private void makeBookCopiesGroups( ref BackgroundWorker bw, ref DoWorkEventArgs e,
		                                  SearchCompareModeEnum CompareMode, ref List<string> FilesList ) {
            switch (CompareMode) {
                case SearchCompareModeEnum.Md5:
                    // 0. Абсолютно одинаковые книги (md5)
                    // Хэширование fb2-файлов по Md5
                    _compareMd5.FilesHashForMd5Parser(
                        ref bw, ref e, StatusLabel, ProgressBar,
                        _TempDir, ref FilesList, ref _htWorkingBook
                    );
                    _nonOpenedFile = _compareMd5.NonOpenedFileList;
                    // формирование дерева списка копий
                    if (!checkBoxSaveGroupsToXml.Checked) {
                        // Создание списка копий
                        _compComm.makeTreeOfBookCopies(
                            ref bw, ref e, StatusLabel, ProgressBar, checkBoxSaveGroupsToXml,
                            lblGroupCountForList, cbGroupCountForList,
                            _listViewFB2Files, _sv, ref _htWorkingBook
                        );
                    } else {
                        // Сохранение Групп сразу в файлы без построения дерева
                        ControlPanel.Enabled = false;
                        _compComm.saveCopiesListToXml(
                            ref bw, ref e, Convert.ToInt32(cbGroupCountForList.Text), _CompareMode,
                            _CompareModeName, StatusLabel, ProgressBar, _sv, _Source, _ScanSubDirs,
                            cbGroupCountForList, checkBoxSaveGroupsToXml, ref _htWorkingBook
                        );
                    }
                    break;
                case SearchCompareModeEnum.BookID:
                    // 1. Одинаковый Id Книги (копии и/или разные версии правки одной и той же книги)
                    // Хэширование fb2-файлов по ID книги
                    _compareBookID.FilesHashForIDParser(
                        ref bw, ref e, StatusLabel, ProgressBar,
                        _TempDir, ref FilesList, ref _htWorkingBook
                    );
                    _nonOpenedFile = _compareBookID.NonOpenedFileList;
                    // формирование дерева списка копий
                    if (!checkBoxSaveGroupsToXml.Checked) {
                        // Создание списка копий
                        _compComm.makeTreeOfBookCopies(
                            ref bw, ref e, StatusLabel, ProgressBar, checkBoxSaveGroupsToXml,
                            lblGroupCountForList, cbGroupCountForList,
                            _listViewFB2Files, _sv, ref _htWorkingBook
                        );
                    } else {
                        // Сохранение Групп сразу в файлы без построения дерева
                        ControlPanel.Enabled = false;
                        _compComm.saveCopiesListToXml(
                            ref bw, ref e, Convert.ToInt32(cbGroupCountForList.Text), _CompareMode,
                            _CompareModeName, StatusLabel, ProgressBar, _sv, _Source, _ScanSubDirs,
                            cbGroupCountForList, checkBoxSaveGroupsToXml, ref _htWorkingBook
                        );
                    }
                    break;
                case SearchCompareModeEnum.BookTitle:
                    // 2. Название Книги (могут быть найдены и разные книги разных Авторов, но с одинаковым Названием)
                    // Хэширование fb2-файлов по Названию книги
                    _сompareBookTitle.FilesHashForBTParser(
                        ref bw, ref e, StatusLabel, ProgressBar,
                        _TempDir, ref FilesList, ref _htWorkingBook
                    );
                    _nonOpenedFile = _сompareBookTitle.NonOpenedFileList;
                    // формирование дерева списка копий
                    if (!checkBoxSaveGroupsToXml.Checked) {
                        // Создание списка копий
                        _compComm.makeTreeOfBookCopies(
                            ref bw, ref e, StatusLabel, ProgressBar, checkBoxSaveGroupsToXml,
                            lblGroupCountForList, cbGroupCountForList,
                            _listViewFB2Files, _sv, ref _htWorkingBook
                        );
                    } else {
                        // Сохранение Групп сразу в файлы без построения дерева
                        ControlPanel.Enabled = false;
                        _compComm.saveCopiesListToXml(
                            ref bw, ref e, Convert.ToInt32(cbGroupCountForList.Text), _CompareMode,
                            _CompareModeName, StatusLabel, ProgressBar, _sv, _Source, _ScanSubDirs,
                            cbGroupCountForList, checkBoxSaveGroupsToXml, ref _htWorkingBook
                        );
                    }
                    break;
                case SearchCompareModeEnum.AuthorBookTitle:
                    // 3. Автор(ы) и Название Книги (одна и та же книга, сделанная разными людьми - разные Id, но Автор и Название - одинаковые)
                    // Хэширование fb2-файлов по Названию книги
                    _сompareBookTitle.FilesHashForBTParser(
                        ref bw, ref e, StatusLabel, ProgressBar,
                        _TempDir, ref FilesList, ref _htWorkingBook
                    );
                    _nonOpenedFile = _сompareBookTitle.NonOpenedFileList;
                    // Хэширование по одинаковым Авторам в пределах сгенерированных групп книг по одинаковым Названиям
                    _compareAuthorBookTitle.FilesHashForAuthorsParser(
                        ref bw, ref e, StatusLabel, ProgressBar,
                        ref _htWorkingBook, ref _htBookTitleAuthors, _WithMiddleName
                    );
                    // формирование дерева списка копий
                    if (!checkBoxSaveGroupsToXml.Checked) {
                        // Создание списка копий
                        _compComm.makeTreeOfBookCopies(
                            ref bw, ref e, StatusLabel, ProgressBar, checkBoxSaveGroupsToXml,
                            lblGroupCountForList, cbGroupCountForList,
                            _listViewFB2Files, _sv, ref _htBookTitleAuthors
                        );
                    } else {
                        // Сохранение Групп сразу в файлы без построения дерева
                        ControlPanel.Enabled = false;
                        _compComm.saveCopiesListToXml(
                            ref bw, ref e, Convert.ToInt32(cbGroupCountForList.Text), _CompareMode,
                            _CompareModeName, StatusLabel, ProgressBar, _sv, _Source, _ScanSubDirs,
                            cbGroupCountForList, checkBoxSaveGroupsToXml, ref _htBookTitleAuthors
                        );
                    }
                    break;
                case SearchCompareModeEnum.AuthorFIO:
                    // 4. Авторы с одинаковыми Фамилиями и инициалами  (могут быть найдены и разные книги разных Авторов, но с одинаковыми Фамилиями и инициалами)
                    // Хэширование fb2-файлов по FIO Авторов
                    _compareAuthorFIO.FilesHashForAuthorFIOParser(
                        ref bw, ref e, StatusLabel, ProgressBar,
                        _TempDir, ref FilesList, ref _htWorkingBook, _WithMiddleName
                    );
                    _nonOpenedFile = _compareAuthorFIO.NonOpenedFileList;

                    // формирование дерева списка копий
                    if (!checkBoxSaveGroupsToXml.Checked) {
                        // Создание списка копий
                        _compComm.makeTreeOfBookCopies(
                            ref bw, ref e, StatusLabel, ProgressBar, checkBoxSaveGroupsToXml,
                            lblGroupCountForList, cbGroupCountForList,
                            _listViewFB2Files, _sv, ref _htWorkingBook
                        );
                    } else {
                        // Сохранение Групп сразу в файлы без построения дерева
                        ControlPanel.Enabled = false;
                        _compComm.saveCopiesListToXml(
                            ref bw, ref e, Convert.ToInt32(cbGroupCountForList.Text), _CompareMode,
                            _CompareModeName, StatusLabel, ProgressBar, _sv, _Source, _ScanSubDirs,
                            cbGroupCountForList, checkBoxSaveGroupsToXml, ref _htWorkingBook
                        );
                    }
                    break;
                case SearchCompareModeEnum.AuthorBookTitleBookID:
                    // 5. Автор(ы), Название Книги и Одинаковый Id Книги (разделять по разным группам разные издания книг)
                    // Хэширование fb2-файлов по Названию книги
                    _сompareBookTitle.FilesHashForBTParser(
                        ref bw, ref e, StatusLabel, ProgressBar,
                        _TempDir, ref FilesList, ref _htWorkingBook
                    );
                    _nonOpenedFile = _сompareBookTitle.NonOpenedFileList;
                    // Хэширование по одинаковым Авторам в пределах сгенерированных групп книг по одинаковым названиям
                    _compareAuthorBookTitle.FilesHashForAuthorsParser(
                        ref bw, ref e, StatusLabel, ProgressBar,
                        ref _htWorkingBook, ref _htBookTitleAuthors, _WithMiddleName
                    );
                    // Хэширование fb2-файлов по ID книги в пределах одинаковых Авторов и Названий книги
                    _compareAuthorBookTitleBookID.FilesHashForBTAuthorsBookIDParser(
                        ref bw, ref e, StatusLabel, ProgressBar,
                        ref _htBookTitleAuthors, ref _htBookTitleAuthorsBookID
                    );
                    // формирование дерева списка копий
                    if (!checkBoxSaveGroupsToXml.Checked) {
                        // Создание списка копий
                        _compComm.makeTreeOfBookCopies(
                            ref bw, ref e, StatusLabel, ProgressBar, checkBoxSaveGroupsToXml,
                            lblGroupCountForList, cbGroupCountForList,
                            _listViewFB2Files, _sv, ref _htBookTitleAuthorsBookID
                        );
                    } else {
                        // Сохранение Групп сразу в файлы без построения дерева
                        ControlPanel.Enabled = false;
                        _compComm.saveCopiesListToXml(
                            ref bw, ref e, Convert.ToInt32(cbGroupCountForList.Text),
                            _CompareMode, _CompareModeName, StatusLabel, ProgressBar, _sv,
                            _Source, _ScanSubDirs, cbGroupCountForList, checkBoxSaveGroupsToXml,
                            ref _htBookTitleAuthorsBookID
                        );
                    }
                    break;
                case SearchCompareModeEnum.BookTitleBookID:
                    // 6. Название Книги и Id Книги (Авторы книги могут быть разными)
                    // Хэширование fb2-файлов по Названию книги
                    _сompareBookTitle.FilesHashForBTParser(
                        ref bw, ref e, StatusLabel, ProgressBar,
                        _TempDir, ref FilesList, ref _htWorkingBook
                    );
                    _nonOpenedFile = _сompareBookTitle.NonOpenedFileList;
                    // Хэширование fb2-файлов по ID книги в пределах одинакового Названия книги
                    _htBookTitleBookID = _compareBookTitleBookID.FilesHashForBTBookIDParser(
                        ref bw, ref e, StatusLabel, ProgressBar, _TempDir, ref _htWorkingBook
                    );
                    // формирование дерева списка копий
                    if (!checkBoxSaveGroupsToXml.Checked) {
                        // Создание списка копий
                        _compComm.makeTreeOfBookCopies(
                            ref bw, ref e, StatusLabel, ProgressBar, checkBoxSaveGroupsToXml,
                            lblGroupCountForList, cbGroupCountForList,
                            _listViewFB2Files, _sv, ref _htBookTitleBookID
                        );
                    } else {
                        // Сохранение Групп сразу в файлы без построения дерева
                        ControlPanel.Enabled = false;
                        _compComm.saveCopiesListToXml(
                            ref bw, ref e, Convert.ToInt32(cbGroupCountForList.Text), _CompareMode,
                            _CompareModeName, StatusLabel, ProgressBar, _sv, _Source, _ScanSubDirs,
                            cbGroupCountForList, checkBoxSaveGroupsToXml, ref _htBookTitleBookID
                        );
                    }
                    break;
                case SearchCompareModeEnum.AuthorBookTitleFB2Author:
                    // 7. Автор(ы), Название Книги и Автор fb2 файла (одна и та же книга, сделанная разными людьми)
                    _сompareBookTitle.FilesHashForBTParser(
                        ref bw, ref e, StatusLabel, ProgressBar,
                        _TempDir, ref FilesList, ref _htWorkingBook
                    );
                    _nonOpenedFile = _сompareBookTitle.NonOpenedFileList;
                    // Хэширование по одинаковым Авторам в пределах сгенерированных групп книг по одинаковым Названиям
                    _compareAuthorBookTitle.FilesHashForAuthorsParser(
                        ref bw, ref e, StatusLabel, ProgressBar,
                        ref _htWorkingBook, ref _htBookTitleAuthors, _WithMiddleName
                    );
                    // Хэширование по одинаковым Авторам fb2 файлов
                    // в пределах сгенерированных групп книг по одинаковым Названиям и Авторам книг
                    _compareFB2Author.FilesHashForFB2AuthorsParser(
                        ref bw, ref e, StatusLabel, ProgressBar,
                        ref _htBookTitleAuthors, ref _htWorkingBook, _WithMiddleName
                    );
                    // формирование дерева списка копий
                    if (!checkBoxSaveGroupsToXml.Checked) {
                        // Создание списка копий
                        _compComm.makeTreeOfBookCopies(
                            ref bw, ref e, StatusLabel, ProgressBar, checkBoxSaveGroupsToXml,
                            lblGroupCountForList, cbGroupCountForList,
                            _listViewFB2Files, _sv, ref _htWorkingBook
                        );
                    } else {
                        // Сохранение Групп сразу в файлы без построения дерева
                        ControlPanel.Enabled = false;
                        _compComm.saveCopiesListToXml(
                            ref bw, ref e, Convert.ToInt32(cbGroupCountForList.Text),
                            _CompareMode, _CompareModeName, StatusLabel, ProgressBar, _sv,
                            _Source, _ScanSubDirs, cbGroupCountForList, checkBoxSaveGroupsToXml,
                            ref _htWorkingBook
                        );
                    }
                    break;
                case SearchCompareModeEnum.AuthorBookTitleBookIDFB2Author:
                    // 8. Автор(ы), Название Книги, Id Книги и Автор fb2 файла
                    // Хэширование fb2-файлов по Названию книги
                    _сompareBookTitle.FilesHashForBTParser(
                        ref bw, ref e, StatusLabel, ProgressBar,
                        _TempDir, ref FilesList, ref _htWorkingBook
                    );
                    _nonOpenedFile = _сompareBookTitle.NonOpenedFileList;
                    // Хэширование по одинаковым Авторам в пределах сгенерированных групп книг по одинаковым названиям
                    _compareAuthorBookTitle.FilesHashForAuthorsParser(
                        ref bw, ref e, StatusLabel, ProgressBar,
                        ref _htWorkingBook, ref _htBookTitleAuthors, _WithMiddleName
                    );
                    // Хэширование fb2-файлов по ID книги в пределах одинаковых Авторов и Названий книги
                    _compareAuthorBookTitleBookID.FilesHashForBTAuthorsBookIDParser(
                        ref bw, ref e, StatusLabel, ProgressBar,
                        ref _htBookTitleAuthors, ref _htBookTitleAuthorsBookID
                    );
                    // Хэширование по одинаковым Авторам fb2 файлов
                    // в пределах сгенерированных групп книг по одинаковым Названиям, Авторам книг и ID книг
                    _compareFB2Author.FilesHashForFB2AuthorsParser(
                        ref bw, ref e, StatusLabel, ProgressBar,
                        ref _htBookTitleAuthorsBookID, ref _htWorkingBook, _WithMiddleName
                    );
                    // формирование дерева списка копий
                    if (!checkBoxSaveGroupsToXml.Checked) {
                        // Создание списка копий
                        _compComm.makeTreeOfBookCopies(
                            ref bw, ref e, StatusLabel, ProgressBar, checkBoxSaveGroupsToXml, lblGroupCountForList,
                            cbGroupCountForList, _listViewFB2Files, _sv, ref _htWorkingBook
                        );
                    } else {
                        // Сохранение Групп сразу в файлы без построения дерева
                        ControlPanel.Enabled = false;
                        _compComm.saveCopiesListToXml(
                            ref bw, ref e, Convert.ToInt32(cbGroupCountForList.Text),
                            _CompareMode, _CompareModeName, StatusLabel, ProgressBar, _sv,
                            _Source, _ScanSubDirs, cbGroupCountForList, checkBoxSaveGroupsToXml,
                            ref _htWorkingBook
                       );
                    }
                    break;
                case SearchCompareModeEnum.BookTitleBookIDFB2Author:
                    // 9. Название Книги, Id Книги и Автор fb2 файла
                    // Хэширование fb2-файлов по Названию книги
                    _сompareBookTitle.FilesHashForBTParser(
                        ref bw, ref e, StatusLabel, ProgressBar,
                        _TempDir, ref FilesList, ref _htWorkingBook
                    );
                    _nonOpenedFile = _сompareBookTitle.NonOpenedFileList;
                    // Хэширование fb2-файлов по ID книги в пределах одинакового Названия книги
                    _htBookTitleBookID = _compareBookTitleBookID.FilesHashForBTBookIDParser(
                        ref bw, ref e, StatusLabel, ProgressBar, _TempDir, ref _htWorkingBook
                    );
                    // Хэширование по одинаковым Авторам fb2 файлов
                    // в пределах сгенерированных групп книг по одинаковым Названиям и ID книг
                    _compareFB2Author.FilesHashForFB2AuthorsParser(
                        ref bw, ref e, StatusLabel, ProgressBar,
                        ref _htBookTitleBookID, ref _htWorkingBook, _WithMiddleName
                    );
                    // формирование дерева списка копий
                    if (!checkBoxSaveGroupsToXml.Checked)
                    {
                        // Создание списка копий
                        _compComm.makeTreeOfBookCopies(
                            ref bw, ref e, StatusLabel, ProgressBar, checkBoxSaveGroupsToXml,
                            lblGroupCountForList, cbGroupCountForList,
                            _listViewFB2Files, _sv, ref _htWorkingBook
                        );
                    }
                    else
                    {
                        // Сохранение Групп сразу в файлы без построения дерева
                        ControlPanel.Enabled = false;
                        _compComm.saveCopiesListToXml(
                            ref bw, ref e, Convert.ToInt32(cbGroupCountForList.Text), _CompareMode,
                            _CompareModeName, StatusLabel, ProgressBar, _sv, _Source, _ScanSubDirs,
                            cbGroupCountForList, checkBoxSaveGroupsToXml, ref _htWorkingBook
                        );
                    }
                    break;
                case SearchCompareModeEnum.AuthorBookIDFB2Author:
                    // 10. Автор(ы), Id Книги и Автор fb2 файла
                    // Хэширование fb2-файлов по Авторам книги
                    _compareAuthorFromFB2.FilesHashForAuthorParser(
                        ref bw, ref e, StatusLabel, ProgressBar,
                        _TempDir, ref FilesList, ref _htAuthors, _WithMiddleName
                    );
                    _nonOpenedFile = _compareAuthorFIO.NonOpenedFileList;

                    // Хэширование fb2-файлов по ID книги
                    _compareAuthorBookID.FilesHashForAuthorsBookIDParser(ref bw, ref e, StatusLabel, ProgressBar,
                                                                         ref _htAuthors, ref _htAuthorsBookID);
                    // Хэширование по одинаковым Авторам fb2 файлов
                    // в пределах сгенерированных групп книг по одинаковым Авторам книг и ID книг
                    _compareFB2Author.FilesHashForFB2AuthorsParser(
                        ref bw, ref e, StatusLabel, ProgressBar,
                        ref _htAuthorsBookID, ref _htWorkingBook, _WithMiddleName
                    );

                    /*// Хэширование fb2-файлов по ID книги
                    _compareBookID.FilesHashForIDParser(
                        ref bw, ref e, StatusLabel, ProgressBar,
                        _TempDir, ref FilesList, ref _htWorkingBook
                    );
                    _nonOpenedFile = _compareBookID.NonOpenedFileList;
                    // Хэширование по одинаковым Авторам в пределах сгенерированных групп книг по ID Книг
                    _compareAuthorBookTitle.FilesHashForAuthorsParser(
                        ref bw, ref e, StatusLabel, ProgressBar,
                        ref _htWorkingBook, ref _htBookTitleAuthors, _WithMiddleName
                    );
                    // Хэширование по одинаковым Авторам fb2 файлов
                    // в пределах сгенерированных групп книг по одинаковым Авторам книг и ID книг
                    _compareFB2Author.FilesHashForFB2AuthorsParser(
                        ref bw, ref e, StatusLabel, ProgressBar, _TempDir,
                        ref _htBookTitleAuthors, ref _htWorkingBook, _WithMiddleName
                    );*/
                    // формирование дерева списка копий
                    if (!checkBoxSaveGroupsToXml.Checked)
                    {
                        // Создание списка копий
                        _compComm.makeTreeOfBookCopies(
                            ref bw, ref e, StatusLabel, ProgressBar, checkBoxSaveGroupsToXml,
                            lblGroupCountForList, cbGroupCountForList,
                            _listViewFB2Files, _sv, ref _htWorkingBook
                        );
                    }
                    else
                    {
                        // Сохранение Групп сразу в файлы без построения дерева
                        ControlPanel.Enabled = false;
                        _compComm.saveCopiesListToXml(
                            ref bw, ref e, Convert.ToInt32(cbGroupCountForList.Text), _CompareMode,
                            _CompareModeName, StatusLabel, ProgressBar, _sv, _Source, _ScanSubDirs,
                            cbGroupCountForList, checkBoxSaveGroupsToXml, ref _htWorkingBook
                        );
                    }
                    break;
            }
        }
                
        #endregion

        // =============================================================================================
        //					BackgroundWorker: Возобновление поиска копий книг
        // =============================================================================================
        #region BackgroundWorker: Возобновление поиска копий книг
        private void InitializeRenewBackgroundWorker() {
			_bwRenew = new BackgroundWorker();
			_bwRenew.WorkerReportsProgress		= true; // Позволить выводить прогресс процесса
			_bwRenew.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
			_bwRenew.DoWork 				+= new DoWorkEventHandler( m_bwRenew_renewSearchDataFromFile_DoWork );
			_bwRenew.ProgressChanged 		+= new ProgressChangedEventHandler( bw_ProgressChanged );
			_bwRenew.RunWorkerCompleted		+= new RunWorkerCompletedEventHandler( bw_RunWorkerCompleted );
		}
		
		// возобновление проверки - загрузка отчета о найденных копиях и о необработанных книгах из xml-файла
		private void m_bwRenew_renewSearchDataFromFile_DoWork( object sender, DoWorkEventArgs e ) {
			_dtStart = DateTime.Now;
			ControlPanel.Enabled = false;
			// загрузка данных из xml
			StatusLabel.Text += "Возобновление поиска копий fb2 книг из xml файла:\r";
			StatusLabel.Text += _fromXmlPath + "\r";
			StatusLabel.Text += "Загрузка данных поиска из xml файла...\r";
			XElement xTree = XElement.Load( _fromXmlPath );
			if ( xTree != null ) {
				// загрузка данных о ходе сравнения
				XElement xCompareData = xTree.Element("CompareData");
				_sv.AllFiles = Convert.ToInt32( xCompareData.Element("AllFiles").Value );
				// режим сравнения
				_CompareMode = Convert.ToInt16( xTree.Element("CompareMode").Attribute("index").Value );
				_cboxMode.SelectedIndex = _CompareMode;
				_CompareModeName		= _cboxMode.Text;
				// настройки поиска и сравнения
				XElement xmlSettings = xTree.Element("Settings");
				if ( xmlSettings != null ) {
					// данные настройки поиска-сравнения
					_Source = xTree.Element("SourceDir").Value;
					_tboxSourceDir.Text = xTree.Element("SourceDir").Value;
					if ( xmlSettings.Element("ScanSubDirs") != null ) {
						_ScanSubDirs = Convert.ToBoolean( xmlSettings.Element("ScanSubDirs").Value );
						_chBoxScanSubDir.Checked = _ScanSubDirs;
					}
					// сохранять ли Группы сразу в mxl-файлы, минуя построение дерева-результата?
					if ( xmlSettings.Element("SaveGroupToXMLWithoutTree") != null ) {
						_checkBoxSaveGroupsToXml.Checked	= Convert.ToBoolean( xTree.Element("Settings").Element("SaveGroupToXMLWithoutTree").Value );
						checkBoxSaveGroupsToXml.Checked		= _checkBoxSaveGroupsToXml.Checked;
					}
					// число Групп для сохранения в список
					if ( xmlSettings.Element("GroupCountForList") != null ) {
						_tscbGroupCountForList.SelectedIndex = Convert.ToInt16( xTree.Element("Settings").Element("GroupCountForList").Value );
						cbGroupCountForList.SelectedIndex	= _tscbGroupCountForList.SelectedIndex;
					}
				}
			}
			
			ViewDupProgressData();
			
			// заполнение списка необработанных файлов
			_FilesList.Clear();
			IEnumerable<XElement> NotWorkingFiles = xTree.Element("NotWorkingFiles").Elements("File");
			int i = 0;
			ProgressBar.Maximum = NotWorkingFiles.ToList().Count;
			ProgressBar.Value = 0;
			foreach ( XElement element in NotWorkingFiles ) {
				_FilesList.Add( element.Value );
				_bwRenew.ReportProgress( ++i );
			}
            
            // загрузка из xml-файла в хэш таблицу данных о копиях книг
            loadFromXMLToHashtable( ref _bwRenew, (SearchCompareModeEnum)_CompareMode, ref xTree );
			// загрузка списка неоткрываемых книг
			if ( File.Exists( _nonOpenedFB2FilePath ) ) {
				XElement xmlNonOpenedFilesTree = XElement.Load( _nonOpenedFB2FilePath );
				IEnumerable<XElement> nof_files = xmlNonOpenedFilesTree.Elements("File");
				foreach ( XElement element in nof_files )
					_nonOpenedFile.Add( element.Value );
			}
			ControlPanel.Enabled = true;

			// Создание списка копий fb2-книг по Группам
			makeBookCopiesGroups( ref _bwRenew, ref e, (SearchCompareModeEnum)_CompareMode, ref _FilesList );
			if ( _autoResizeColumns )
				MiscListView.AutoResizeColumns( _listViewFB2Files );
			
		}
		
		/// <summary>
		/// Загрузка из xml-файла в хэш таблицу данных о копиях книг для всех режимов
		/// </summary>
		private void loadFromXMLToHashtable( ref BackgroundWorker bw, SearchCompareModeEnum CompareMode, ref XElement xmlTree ) {
			StatusLabel.Text += "Загрузка в хэш данных обработанных книг...\r";
			ProgressBar.Maximum	= Convert.ToInt32( xmlTree.Element("Groups").Attribute("count").Value );
			if ( CompareMode == SearchCompareModeEnum.AuthorBookTitle )
				ProgressBar.Maximum += Convert.ToInt32( xmlTree.Element("BookTitleGroup").Attribute("count").Value );
			ProgressBar.Value = 0;
			
			// заполнение хэш-таблиц
            ClearAllHashtables();
            //TODO Для других режимов (_htBookTitleAuthorsBookID для 5-го)
            if (CompareMode == SearchCompareModeEnum.AuthorBookTitle) {
                _loadFromXMLToHashtable(ref bw, ref xmlTree, "BookTitleGroup", ref _htWorkingBook);
                _loadFromXMLToHashtable(ref bw, ref xmlTree, "Groups", ref _htBookTitleAuthors);
            } else if (CompareMode == SearchCompareModeEnum.AuthorBookTitleBookID) {
                //TODO
                _loadFromXMLToHashtable(ref bw, ref xmlTree, "BookTitleGroup", ref _htWorkingBook);
                _loadFromXMLToHashtable(ref bw, ref xmlTree, "BookTitleAuthorsGroup", ref _htBookTitleAuthors);
                _loadFromXMLToHashtable(ref bw, ref xmlTree, "Groups", ref _htBookTitleAuthorsBookID);
            } else {
                _loadFromXMLToHashtable(ref bw, ref xmlTree, "Groups", ref _htWorkingBook);
            }
		}
		
		/// <summary>
		/// Загрузка из xml-файла в хэш таблицу данных о копиях книг для всех режимов
		/// </summary>
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
							Author author = new Author(
								xeFirstName != null ? transformToTextFieldType(xeFirstName.Value) : null,
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

                    // загрузка авторов fb2 файла
                    IList<Author> fb2Authors = null;
                    XElement xeFB2Authors = book.Element("FB2Authors");
                    if (xeFB2Authors != null) {
                        fb2Authors = new List<Author>();
                        IEnumerable<XElement> iexeFB2Authors = from el in book.Descendants("Author") select el;
                        foreach (XElement a in iexeFB2Authors) {
                            XElement xeFB2FirstName = a.Element("FirstName");
                            XElement xeFB2MiddleName = a.Element("MiddleName");
                            XElement xeFB2LastName = a.Element("LastName");
                            XElement xeFB2NickName = a.Element("NickName");
                            Author author = new Author(
                                xeFB2FirstName != null ? transformToTextFieldType(xeFB2FirstName.Value) : null,
                                xeFB2MiddleName != null ? transformToTextFieldType(xeFB2MiddleName.Value) : null,
                                xeFB2LastName != null ? transformToTextFieldType(xeFB2LastName.Value) : null,
                                xeFB2NickName != null ? transformToTextFieldType(xeFB2NickName.Value) : null
                            );
                            fb2Authors.Add(author);
                        }
                    }

                    // данные о книге
                    BookData fb2BookData = new BookData(
						bookTitle, authors, genres,
						book.Element("BookLang").Value, book.Element("BookID").Value,
						book.Element("Version").Value, fb2Authors,
                        book.Element("Path").Value, book.Element("Encoding").Value
					);
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
            //TODO Для других режимов
            int groupsCount = 0;
            switch ((SearchCompareModeEnum)CompareMode) {
                case SearchCompareModeEnum.AuthorBookTitle:
                    groupsCount = _htBookTitleAuthors.Count;
                    break;
                case SearchCompareModeEnum.AuthorBookTitleBookID:
                    groupsCount = _htBookTitleAuthorsBookID.Count;
                    break;
                default:
                    groupsCount = htWorkingBook.Count;
                    break;
            }
            
            XElement xNonWorkedFiles = null;
            //TODO Для других режимов
            switch ((SearchCompareModeEnum)CompareMode) {
                case SearchCompareModeEnum.AuthorBookTitle:
                    xNonWorkedFiles = new XElement("BookTitleGroup", new XAttribute("count", htWorkingBook.Count));
                    break;
                case SearchCompareModeEnum.AuthorBookTitleBookID:
                    xNonWorkedFiles = new XElement("BookTitleGroup", new XAttribute("count", _htBookTitleAuthorsBookID.Count));
                    break;
                default:
                    xNonWorkedFiles = null;
                    break;
            }

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
				                          new XElement("AllFiles", _sv.AllFiles),
				                          new XElement("Groups", _sv.Group),
				                          new XElement("AllFB2InGroups", _sv.AllFB2InGroups)
				                         ),
				             new XComment("Обработанные файлы"),
				             new XElement("Groups", new XAttribute("count", groupsCount)),
				             new XComment("Не обработанные файлы"), xNonWorkedFiles,
				             new XElement("NotWorkingFiles", new XAttribute("count", FilesList.Count))
				            )
			);

            // обработанные книги
            List<string> keyList;
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
				case SearchCompareModeEnum.AuthorBookTitle:
					// 3. Автор(ы) и Название Книги (одна и та же книга, сделанная разными людьми - разные Id, но Автор и Название - одинаковые)
					// список обработанных файлов по группам
					keyList = sortKeys( ref htWorkingBook );
					makeXmlFB2FilesInGroup( ref htWorkingBook, ref keyList, ref groupNumber, ref fileNumber, ref doc, "BookTitleGroup", FilesList.Count );
                    // список Групп с копиями (Одинаковые Авторы в пределах сгенерированных групп книг по одинаковым Названием)
                    groupNumber = 0;
					keyList = sortKeys( ref _htBookTitleAuthors );
					makeXmlFB2FilesInGroup( ref _htBookTitleAuthors, ref keyList, ref groupNumber, ref fileNumber, ref doc, "Groups", FilesList.Count );
					break;
				case SearchCompareModeEnum.AuthorFIO:
					// 4. Авторы с одинаковой Фамилией и инициалами
					keyList = sortKeys( ref htWorkingBook );
					makeXmlFB2FilesInGroup( ref htWorkingBook, ref keyList, ref groupNumber, ref fileNumber, ref doc, "Groups", FilesList.Count );
					break;

                case SearchCompareModeEnum.AuthorBookTitleBookID:
                    // 5. Автор(ы), Название и Одинаковый Id Книги (разделять по разным группам разные издания книг)
                    //TODO Режим
                    keyList = sortKeys(ref htWorkingBook);
                    makeXmlFB2FilesInGroup(ref htWorkingBook, ref keyList, ref groupNumber, ref fileNumber, ref doc, "BookTitleGroup", FilesList.Count);
                    // список одинаковых Авторов в пределах сгенерированных групп книг по одинаковым Названиям
                    groupNumber = 0;
                    keyList = sortKeys(ref _htBookTitleAuthors);
                    makeXmlFB2FilesInGroup(ref _htBookTitleAuthors, ref keyList, ref groupNumber, ref fileNumber, ref doc, "BookTitleAuthorsGroup", FilesList.Count);
                    // список Групп с копиями (Одинаковые ID книги в пределах одинаковых Авторов и Названий книги)
                    groupNumber = 0;
                    keyList = sortKeys(ref _htBookTitleAuthorsBookID);
                    makeXmlFB2FilesInGroup(ref _htBookTitleAuthorsBookID, ref keyList, ref groupNumber, ref fileNumber, ref doc, "Groups", FilesList.Count);
                    break;

                case SearchCompareModeEnum.BookTitleBookID:
                    // 6. Название Книги и Id Книги (Авторы книги могут быть разными)
                    // список обработанных файлов по группам
                    keyList = sortKeys(ref htWorkingBook);
                    makeXmlFB2FilesInGroup(ref htWorkingBook, ref keyList, ref groupNumber, ref fileNumber, ref doc, "Groups", FilesList.Count);
                    // список Групп с копиями (Одинаковые  ID книги в пределах одинакового Названия книги)
                    groupNumber = 0;// TODO В файле прерывания нумерация после 9 идет опять с 0
                    keyList = sortKeys(ref _htBookTitleBookID);
                    makeXmlFB2FilesInGroup(ref _htBookTitleBookID, ref keyList, ref groupNumber, ref fileNumber, ref doc, "Groups", FilesList.Count);
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
                    if ( _bw.IsBusy )
						_bw.ReportProgress( ++i );
					else if ( _bwRenew.IsBusy )
						_bwRenew.ReportProgress( ++i );
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
					if ( _bw.IsBusy )
						_bw.ReportProgress( ++i );
					else if ( _bwRenew.IsBusy )
						_bwRenew.ReportProgress( ++i );
					else
						++ProgressBar.Value;
				}
			}
		}

		// формирование xml данных о книге в xDocument
		private void makeXmlBookData( ref XElement xeGroup, FB2FilesDataInGroup fb2f, ref int fileNumber ) {
			XElement xeAuthors;
			XElement xeGenres;
            XElement xeFB2Authors;
            foreach ( BookData bd in fb2f ) {
				xeGroup.Add(new XElement("Book", new XAttribute("number", fileNumber++),
				                         new XElement("Group", fb2f.Group),
				                         new XElement("Path", bd.Path),
				                         new XElement("BookLang", bd.Lang),
				                         new XElement("BookID", bd.Id),
				                         new XElement("Encoding", bd.Encoding),
				                         new XElement("Version", bd.Version),
				                         new XElement("BookTitle", _compComm.MakeBookTitleString( bd.BookTitle )),
				                         xeAuthors = new XElement("Authors"),
				                         xeGenres = new XElement("Genres"),
                                         xeFB2Authors = new XElement("FB2Authors")
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
                // сохранение данных об авторах fb2 файла в xml-файл
                if (bd.FB2Authors != null) {
                    XElement xeFB2Author = null;
                    foreach (Author fb2a in bd.FB2Authors) {
                        xeFB2Authors.Add(xeFB2Author = new XElement("FB2Author"));
                        if (fb2a.LastName != null && fb2a.LastName.Value != null)
                            xeFB2Author.Add(new XElement("LastName", fb2a.LastName.Value));
                        if (fb2a.FirstName != null && fb2a.FirstName.Value != null)
                            xeFB2Author.Add(new XElement("FirstName", fb2a.FirstName.Value));
                        if (fb2a.MiddleName != null && fb2a.MiddleName.Value != null)
                            xeFB2Author.Add(new XElement("MiddleName", fb2a.MiddleName.Value));
                        if (fb2a.NickName != null && fb2a.NickName.Value != null)
                            xeFB2Author.Add(new XElement("NickName", fb2a.NickName.Value));
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
		
		// Отображение результата поиска сравнения
		private void ViewDupProgressData() {
			MiscListView.ListViewStatus( _lvFilesCount, (int)FilesCountViewDupCollumnEnum.AllGroups, _sv.Group );
			MiscListView.ListViewStatus( _lvFilesCount, (int)FilesCountViewDupCollumnEnum.AllBoolsInAllGroups, _sv.AllFB2InGroups );
		}
		
		// остановка поиска / возобновления поиска из xml
		// StopForSaveToXml: false - остановка поиска; true - остановка возобновления поиска
		private void StopCompare( bool StopForSaveToXml )
		{
			_StopToSave = StopForSaveToXml;
			if ( _bw.IsBusy ) {
				if( _bw.WorkerSupportsCancellation )
					_bw.CancelAsync();
			} else {
				if ( _bwRenew.WorkerSupportsCancellation )
					_bwRenew.CancelAsync();
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

        /// <summary>
        /// Очистка всех хеш таблиц
        /// </summary>
        private void ClearAllHashtables()
        {
            _htWorkingBook.Clear();
            _htBookTitleAuthors.Clear();
            _htBookTitleBookID.Clear();
            _htBookTitleAuthorsBookID.Clear();
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
