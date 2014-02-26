/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 16.03.2009
 * Time: 10:03
 * 
 * License: GPL 2.1
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;

using Core.FB2.Common;
using Core.FB2.Description;
using Core.FB2.Description.Common;
using Core.FB2.Description.CustomInfo;
using Core.FB2.Description.DocumentInfo;
using Core.FB2.Description.TitleInfo;
using Core.FB2Dublicator;
using Core.Misc;

using FB2Validator = Core.FB2Parser.FB2Validator;
using filesWorker = Core.FilesWorker.FilesWorker;

namespace SharpFBTools.Tools
{

	/// <summary>
	/// Поиск копий fb2-файлов (различные критерии поиска)
	/// </summary>
	public partial class SFBTpFB2Dublicator : UserControl
	{
		#region Закрытые данные класса
		private StatusView	m_sv 		= new StatusView();
		private string	m_sSource		= string.Empty;
		private string	m_TargetDir		= string.Empty;
		private string	m_sMessTitle	= string.Empty;
		private MiscListView m_mscLV	= new MiscListView(); // класс по работе с ListView
		FB2Validator m_fv2Validator		= new FB2Validator();

		/// <summary>
		/// Номера колонок контрола просмотра групп одинаковых книг
		/// </summary>
		private enum ResultViewCollumn {
			Path			= 0,	// Путь к книге
			BookTitle		= 1,	// Название книги
			Authors			= 2,	// Автор(ы)
			Genres			= 3,	// Жанр(ы)
			BookID			= 4,	// ID книги
			Version			= 5,	// Версия файла
			Encoding		= 6,	// Кодировка
			Validate		= 7,	// Валидность
			FileLength		= 8, 	// Размер файла
			CreationTime	= 9, 	// Время создания файла
			LastWriteTime	= 10, 	// Последнее изменение файла
		}
		
		/// <summary>
		/// Варианты сравнения книг в группах
		/// </summary>
		private enum CompareMode {
			Version,		// Версия файла
			Encoding,		// Кодировка
			Validate,		// Валидность
			FileLength, 	// Размер файла
			CreationTime, 	// Время создания файла
			LastWriteTime, 	// Последнее изменение файла
		}
		
		/// <summary>
		/// Данные о самой "новой" книге в группе
		/// </summary>
		private class FB2BookInfo {
			private int m_IndexVersion = 0;
			private int m_IndexCreationTime= 0;
			private int m_IndexLastWriteTime = 0;
			private string m_Version = "0";
			private string m_CreationTime = "0";
			private string m_LastWriteTime = "0";
			
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
			public virtual string CreationTime {
				get { return m_CreationTime; }
				set { m_CreationTime = value; }
			}
			public virtual string LastWriteTime {
				get { return m_LastWriteTime; }
				set { m_LastWriteTime = value; }
			}
		}
		#endregion
		
		public SFBTpFB2Dublicator()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			// задание для кнопок ToolStrip стиля и положения текста и картинки
			SetToolButtonsSettings();
			// создание колонок просмотрщика найденных книг
			MakeColumns();
			Init();
			// читаем сохраненные пути к папкам Поиска одинаковых fb2-файлов, если они есть
			ReadFB2DupTempData();
			cboxMode.SelectedIndex			= 0; // Условия для Сравнения fb2-файлов: md5 книги
			cboxDupExistFile.SelectedIndex	= 1; // добавление к создаваемому fb2-файлу очередного номера
		}
		// =============================================================================================
		// 									ОТКРЫТЫЕ МЕТОДЫ КЛАССА
		// =============================================================================================
		#region Открытые методы класса
		// задание для кнопок ToolStrip стиля и положения текста и картинки
		public void SetToolButtonsSettings() {
			Settings.SettingsFB2Dup.SetToolButtonsSettings( tsDup );
		}
		#endregion
		
		// =============================================================================================
		// 							ВСПОМОГАТЕЛЬНЫЕ МЕТОДЫ И АЛГОРИТМЫ КЛАССА
		// =============================================================================================
		#region Вспомогательные методы и алгоритмы класса
		
		// =============================================================================================
		// 			Сохранение в xml и Загрузка из xml списка копий fb2 книг
		// =============================================================================================
		#region Сохранение в xml и Загрузка из xml списка копий fb2 книг
		// сохранение списка копий книг в xml-файл
		private void saveCopiesListToXml(string ToFileName, int CompareMode, string CompareModeName) {
			#region Код
			XDocument doc = new XDocument(
				new XDeclaration("1.0", "utf-8", "yes"),
				new XElement("Files",
				             new XComment("Папка для поиска копий fb2 книг"),
				             new XElement("SourceDir", m_sSource),
				             new XComment("Настройки поиска-сравнения fb2 книг"),
				             new XElement("Settings",
				                          new XElement("ScanSubDirs", chBoxScanSubDir.Checked),
				                          new XElement("CheckValidate", chBoxIsValid.Checked)),
				             new XComment("Режим поиска-сравнения fb2 книг"),
				             new XElement("CompareMode",
				                          new XAttribute("index", CompareMode),
				                          new XElement("Name", CompareModeName)),
				             new XComment("Данные о ходе сравнения fb2 книг"),
				             new XElement("CompareData",
				                          new XElement("AllDirs", lvFilesCount.Items[0].SubItems[1].Text),
				                          new XElement("AllFiles", lvFilesCount.Items[1].SubItems[1].Text),
				                          new XElement("FB2Files", lvFilesCount.Items[2].SubItems[1].Text),
				                          new XElement("Zip", lvFilesCount.Items[3].SubItems[1].Text),
				                          new XElement("Other", lvFilesCount.Items[4].SubItems[1].Text),
				                          new XElement("Groups", lvFilesCount.Items[5].SubItems[1].Text),
				                          new XElement("AllFB2InGroups", lvFilesCount.Items[6].SubItems[1].Text)
				                         ),
				             new XComment("Копии fb2 книг по группам"),
				             new XElement("Groups", new XAttribute("count", lvResult.Groups.Count.ToString()))
				            )
			);
			
			// копии fb2 книг по группам
			if ( lvResult.Groups.Count > 0 ) {
				XElement xeGroup = null;
				int groupNumber = 0;
				int fileNumber = 0;
				foreach (ListViewGroup lvGroup in lvResult.Groups ) {
					doc.Root.Element("Groups").Add(
						xeGroup = new XElement("Group", new XAttribute("number", groupNumber++),
						                       new XAttribute("count", lvGroup.Items.Count),
						                       new XAttribute("name", lvGroup.Header)
						                      )
					);
					foreach ( ListViewItem lvi in lvGroup.Items ) {
						xeGroup.Add(new XElement("Book", new XAttribute("number", fileNumber++),
						                         new XElement("Group", lvi.Group.Header),
						                         new XElement("Path", lvi.SubItems[(int)ResultViewCollumn.Path].Text),
						                         new XElement("BookTitle", lvi.SubItems[(int)ResultViewCollumn.BookTitle].Text),
						                         new XElement("Authors", lvi.SubItems[(int)ResultViewCollumn.Authors].Text),
						                         new XElement("Genres", lvi.SubItems[(int)ResultViewCollumn.Genres].Text),
						                         new XElement("BookID", lvi.SubItems[(int)ResultViewCollumn.BookID].Text),
						                         new XElement("Version", lvi.SubItems[(int)ResultViewCollumn.Version].Text),
						                         new XElement("Encoding", lvi.SubItems[(int)ResultViewCollumn.Encoding].Text),
						                         new XElement("Validation", lvi.SubItems[(int)ResultViewCollumn.Validate].Text),
						                         new XElement("FileLength", lvi.SubItems[(int)ResultViewCollumn.FileLength].Text),
						                         new XElement("FileCreationTime", lvi.SubItems[(int)ResultViewCollumn.CreationTime].Text),
						                         new XElement("FileLastWriteTime", lvi.SubItems[(int)ResultViewCollumn.LastWriteTime].Text)
						                        )
						           );
					}
				}
			}
			doc.Save(ToFileName);
			#endregion
		}
		
		// загрузка из xml-файла в хэш таблицу данных о копиях книг
		private void loadCopiesListFromXML( string FromXML ) {
			#region Код
			XElement xmlTree = XElement.Load( FromXML );
			
			// выставляем режим сравнения
			int CompareMode = Convert.ToInt16( xmlTree.Element("CompareMode").Attribute("index").Value );
			cboxMode.SelectedIndex = CompareMode;
			
			// устанавливаем данные настройки поиска-сравнения
			m_sSource = tboxSourceDir.Text = xmlTree.Element("SourceDir").Value;
			chBoxScanSubDir.Checked = Convert.ToBoolean( xmlTree.Element("Settings").Element("ScanSubDirs").Value );
			chBoxIsValid.Checked = Convert.ToBoolean( xmlTree.Element("Settings").Element("CheckValidate").Value );
			
			//загрузка данных о ходе сравнения
			XElement compareData = xmlTree.Element("CompareData");
			m_sv.AllFiles = Convert.ToInt32( compareData.Element("AllFiles").Value );
			m_sv.FB2 = Convert.ToInt32( compareData.Element("FB2Files").Value );
			m_sv.Zip = Convert.ToInt32( compareData.Element("Zip").Value );
			m_sv.Other = Convert.ToInt32( compareData.Element("Other").Value );
			m_sv.Group = Convert.ToInt32( compareData.Element("Groups").Value );
			m_sv.AllFB2InGroups = Convert.ToInt32( compareData.Element("AllFB2InGroups").Value );
			lvFilesCount.Items[0].SubItems[1].Text = compareData.Element("AllDirs").Value;
			
			ViewDupProgressData();

			// данные поиска
			Hashtable htBookGroups = new Hashtable(); // хеш-таблица групп одинаковых книг
			ListViewGroup	lvg = null; // группа одинаковых книг
			ListViewItem	lvi = null;
			IEnumerable<XElement> groups = xmlTree.Element("Groups").Elements("Group");
			// перебор всех групп копий
			foreach( XElement group in groups ) {
				string GroupName = group.Attribute("name").Value;
				// перебор всех книг в группе
				IEnumerable<XElement> books = group.Elements("Book");
				foreach( XElement book in books ) {
					lvg = new ListViewGroup( GroupName );
					lvi = new ListViewItem( book.Element("Path").Value );
					lvi.SubItems.Add( book.Element("BookTitle").Value );
					lvi.SubItems.Add( book.Element("Authors").Value );
					lvi.SubItems.Add( book.Element("Genres").Value );
					lvi.SubItems.Add( book.Element("BookID").Value );
					lvi.SubItems.Add( book.Element("Version").Value );
					lvi.SubItems.Add( book.Element("Encoding").Value );
					lvi.SubItems.Add( book.Element("Validation").Value );
					lvi.SubItems.Add( book.Element("FileLength").Value );
					lvi.SubItems.Add( book.Element("FileCreationTime").Value );
					lvi.SubItems.Add( book.Element("FileLastWriteTime").Value );
					// заносим группу в хеш, если она там отсутствует
					AddBookGroupInHashTable( ref htBookGroups, ref lvg );
					// присваиваем группу книге
					lvResult.Groups.Add( (ListViewGroup)htBookGroups[GroupName] );
					lvi.Group = (ListViewGroup)htBookGroups[GroupName];
					lvResult.Items.Add( lvi );
				}
			}
			#endregion
		}
		
		// создание хеш-таблицы для групп одинаковых книг
		private bool AddBookGroupInHashTable( ref Hashtable groups, ref ListViewGroup lvg ) {
			if( groups != null ){
				if( !groups.Contains( lvg.Header ) ) {
					groups.Add( lvg.Header, lvg );
					return true;
				}
			}
			return false;
		}
		#endregion

		// =============================================================================================
		// 										Анализатор копий книг
		// =============================================================================================
		#region Анализатор копий книг
		// пометка "старых" книг
		private void _CheckAllOldBooksInGroup(CompareMode mode, ListViewGroup lvGroup)
		{
			#region Код
			// перебор всех книг в выбранной группе
			FB2BookInfo bookInfo = new FB2BookInfo();
			foreach( ListViewItem lvi in lvGroup.Items ) {
				if (lvi.SubItems[(int)ResultViewCollumn.Version].Text != string.Empty) {
					switch( mode) {
						case CompareMode.Version:
							if ( bookInfo.Version.Replace('.', ',').CompareTo(lvi.SubItems[(int)ResultViewCollumn.Version].Text.Replace('.', ',')) < 0 ) {
								// если текущая книга более новая
								bookInfo.Version = lvi.SubItems[(int)ResultViewCollumn.Version].Text;
								bookInfo.IndexVersion = lvi.Index;
							}
							break;
						case CompareMode.CreationTime:
							// какой файл позднее создан
							if ( bookInfo.CreationTime.CompareTo(lvi.SubItems[(int)ResultViewCollumn.CreationTime].Text) < 0 ) {
								bookInfo.CreationTime = lvi.SubItems[(int)ResultViewCollumn.CreationTime].Text;
								bookInfo.IndexCreationTime = lvi.Index;
							}
							break;
						case CompareMode.LastWriteTime:
							// какой файл позднее правился
							if ( bookInfo.LastWriteTime.CompareTo(lvi.SubItems[(int)ResultViewCollumn.LastWriteTime].Text) < 0 ) {
								bookInfo.LastWriteTime = lvi.SubItems[(int)ResultViewCollumn.LastWriteTime].Text;
								bookInfo.IndexLastWriteTime = lvi.Index;
							}
							break;
					}
				}
			}
			// помечаем все книги в группе, кроме самой "новой"
			foreach (ListViewItem item in lvGroup.Items ) {
				switch( mode) {
					case CompareMode.Version:
						if (item.Index != bookInfo.IndexVersion)
							lvResult.Items[item.Index].Checked = true;
						break;
					case CompareMode.CreationTime:
						if (item.Index != bookInfo.IndexCreationTime)
							lvResult.Items[item.Index].Checked = true;
						break;
					case CompareMode.LastWriteTime:
						if (item.Index != bookInfo.IndexLastWriteTime)
							lvResult.Items[item.Index].Checked = true;
						break;
				}
			}
			#endregion
		}
		
		// пометить в выбранной группе все "старые" книги
		private void CheckAllOldBooksInGroup(CompareMode mode)
		{
			ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
			if (si.Count > 0) {
				// группа для выделенной книги
				ListViewGroup lvg = si[0].Group;
				m_mscLV.CheckAllListViewItemsInGroup( lvg, false );
				_CheckAllOldBooksInGroup(mode, lvg);
			}
		}
		
		// пометить в каждой группе все "старые" книги
		private void CheckAllOldBooksInAllGroups(CompareMode mode)
		{
			m_mscLV.UnCheckAllListViewItems( lvResult.CheckedItems );
			// перебор всех групп
			foreach( ListViewGroup lvg in lvResult.Groups ) {
				// перебор всех книг в выбранной группе
				_CheckAllOldBooksInGroup(mode, lvg);
			}
		}
		#endregion
		
		// =============================================================================================
		// 											Разное
		// =============================================================================================
		#region Разное
		// удаление оставшейся книги в группе и самой группы с контрола отображения (1 книга - это уже не копия)
		private void workingGroupItemAfterBookDelete( System.Windows.Forms.ListView listView, ListViewGroup lvg ) {
			if( lvg.Items.Count <= 1 ) {
				if( lvg.Items.Count == 1 )
					listView.Items[lvg.Items[0].Index].Remove();
				listView.Groups.Remove( lvg );
			}
		}
		
		// обновление числа групп и книг во всех группах
		private void newGroupItemsCount( System.Windows.Forms.ListView lvResult, System.Windows.Forms.ListView lvFilesCount ) {
			// новое число групп
			m_mscLV.ListViewStatus( lvFilesCount, 5, lvResult.Groups.Count.ToString() );
			// число книг во всех группах
			m_mscLV.ListViewStatus( lvFilesCount, 6, lvResult.Items.Count.ToString() );
		}
		
		// создание колонок просмотрщика найденных книг
		private void MakeColumns() {
			lvResult.Columns.Add( "Путь к книге", 300 );
			lvResult.Columns.Add( "Название", 180 );
			lvResult.Columns.Add( "Автор(ы)", 180 );
			lvResult.Columns.Add( "Жанр(ы)", 180 );
			lvResult.Columns.Add( "ID", 200 );
			lvResult.Columns.Add( "Версия", 50 );
			lvResult.Columns.Add( "Кодировка", 90, HorizontalAlignment.Center );
			lvResult.Columns.Add( "Валидность", 50, HorizontalAlignment.Center );
			lvResult.Columns.Add( "Размер", 90, HorizontalAlignment.Center );
			
			lvResult.Columns.Add( "Дата создания", 120 );
			lvResult.Columns.Add( "Последнее изменение", 120 );
		}

		// Получение картинки из base64
		private Image Base64ToImage(string base64String) {
			// Convert Base64 String to byte[]
			byte[] imageBytes = Convert.FromBase64String(base64String);
			MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length);

			// Convert byte[] to Image
			ms.Write(imageBytes, 0, imageBytes.Length);
			Image image = Image.FromStream(ms, true);
			return image;
		}
		
		// отключение/включение обработчиков событий для lvResult (убираем "тормоза")
		private void ConnectListViewResultEventHandlers( bool bConnect ) {
			if( !bConnect ) {
				// отключаем обработчики событий для lvResult (убираем "тормоза")
				lvResult.BeginUpdate();
				this.lvResult.ItemChecked -= new System.Windows.Forms.ItemCheckedEventHandler(this.LvResultItemChecked);
				this.lvResult.SelectedIndexChanged -= new System.EventHandler(this.LvResultSelectedIndexChanged);
			} else {
				lvResult.EndUpdate();
				this.lvResult.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.LvResultItemChecked);
				this.lvResult.SelectedIndexChanged += new System.EventHandler(this.LvResultSelectedIndexChanged);
			}
		}
		
		// Отображение результата поиска сравнения
		private void ViewDupProgressData() {
			m_mscLV.ListViewStatus( lvFilesCount, 1, m_sv.AllFiles );
			m_mscLV.ListViewStatus( lvFilesCount, 2, m_sv.FB2 );
			m_mscLV.ListViewStatus( lvFilesCount, 3, m_sv.Zip );
			m_mscLV.ListViewStatus( lvFilesCount, 4, m_sv.Other );
			m_mscLV.ListViewStatus( lvFilesCount, 5, m_sv.Group );
			m_mscLV.ListViewStatus( lvFilesCount, 6, m_sv.AllFB2InGroups );
		}
		
		// инициализация контролов и переменных
		private void Init() {
			ConnectListViewResultEventHandlers( false );
			for( int i=0; i!=lvFilesCount.Items.Count; ++i )
				lvFilesCount.Items[i].SubItems[1].Text	= "0";

			// очистка временной папки
			filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
			m_sv.Clear(); // сброс данных класса для отображения прогресса
			lvResult.Items.Clear();
			lvResult.Groups.Clear();
			ConnectListViewResultEventHandlers( true );
		}
		
		// очистка контролов вывода данных по книге по ее выбору
		private void ClearDataFields() {
			for( int i=0; i!=lvTitleInfo.Items.Count; ++i ) {
				lvTitleInfo.Items[i].SubItems[1].Text		= string.Empty;
				lvSourceTitleInfo.Items[i].SubItems[1].Text	= string.Empty;
			}
			for( int i=0; i!=lvDocumentInfo.Items.Count; ++i )
				lvDocumentInfo.Items[i].SubItems[1].Text = string.Empty;

			for( int i=0; i!=lvPublishInfo.Items.Count; ++i )
				lvPublishInfo.Items[i].SubItems[1].Text = string.Empty;

			lvCustomInfo.Items.Clear();
			rtbHistory.Clear();
			rtbAnnotation.Clear();
		}
		
		// чтение путей к данным поиска одинаковых fb2-файлов из xml-файла
		private void ReadFB2DupTempData() {
			string sSettings = Settings.Settings.WorksDataSettingsPath;
			if( !File.Exists( sSettings ) )
				return;
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.IgnoreWhitespace = true;
			using ( XmlReader reader = XmlReader.Create( sSettings, settings ) ) {
				// Полная Сортировка
				reader.ReadToFollowing("FB2DupScanDir");
				if (reader.HasAttributes ) {
					tboxSourceDir.Text = reader.GetAttribute("SourceDir");
					Settings.SettingsFB2Dup.DupScanDir = tboxSourceDir.Text.Trim();
				}
				reader.ReadToFollowing("FB2DupToDir");
				if (reader.HasAttributes ) {
					m_TargetDir = reader.GetAttribute("TargetDir");
					Settings.SettingsFB2Dup.DupToDir = m_TargetDir;
				}
				reader.Close();
			}
		}
		
		// доступность / недоступность кнопок групповой обработки помеченных книг
		private void groupWorkingChekedItemsEnabled( int checkedItemsCount ) {
			if( checkedItemsCount > 0 ) {
				tsmiCopyCheckedFb2To.Enabled	= true;
				tsmiMoveCheckedFb2To.Enabled	= true;
				tsmiDeleteCheckedFb2.Enabled	= true;
			} else {
				tsmiCopyCheckedFb2To.Enabled	= false;
				tsmiMoveCheckedFb2To.Enabled	= false;
				tsmiDeleteCheckedFb2.Enabled	= false;
			}
		}
		
		// проверка на наличие папки сканирования копий книг
		private bool IsScanFolderDataCorrect( TextBox tbSource, ref string MessTitle ) {
			// проверка на корректность данных папок источника
			string sSource	= filesWorker.WorkingDirPath( tbSource.Text.Trim() );
			tbSource.Text	= sSource;
			
			// проверки на корректность папок источника
			if( sSource.Length == 0 ) {
				MessageBox.Show( "Выберите папку для сканирования!", MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			DirectoryInfo diFolder = new DirectoryInfo( sSource );
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка не найдена: " + sSource, MessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			return true;
		}
		
		private string GetSubtring( string sP, string sStart, string sEnd ) {
			string s = string.Empty;
			int nStart = sP.IndexOf( sStart );
			int nEnd = -1;
			if( nStart!=-1 ) {
				nEnd = sP.IndexOf( sEnd );
				if( nEnd!=-1 )
					nEnd += sEnd.Length;

				s = sP.Substring( nStart, nEnd-nStart );
			}
			return s;
		}
		
		// извлечение информации из текста тэга <p>, убираем инлайн-теги для простоты
		private string GetDataFromTagP( string sP ) {
			sP = sP.Replace( "</p>","\r\n" ); sP = sP.Replace( "</P>","\r\n" );
			string s = GetSubtring( sP, "<p ", ">" );
			if( s.Length!=0 ) sP = sP.Replace( s, "" );
			s = GetSubtring( sP, "<P ", ">" );
			if( s.Length!=0 ) sP = sP.Replace( s, "" );
			string[] sIT = { "<strong>", "<STRONG>", "</strong>", "</STRONG>",
				"<emphasis>", "<EMPHASIS>", "</emphasis>", "</EMPHASIS>",
				"<sup>", "<SUP>", "</sup>", "</SUP>",
				"<sub>", "<SUB>", "</sub>", "</SUB>",
				"<code>", "<CODE>", "</code>", "</CODE>",
				"<strikethrough>", "<STRIKETHROUGH>", "</strikethrough>", "</STRIKETHROUGH>" };
			foreach( string sT in sIT ) {
				sP = sP.Replace( sT, "" );
			}
			return sP;
		}
		#endregion
		
		#endregion

		// =============================================================================================
		// 									ОБРАБОТЧИКИ событий контролов
		// =============================================================================================
		#region Обработчики событий контролов
		void TboxSourceDirTextChanged(object sender, EventArgs e)
		{
			Settings.SettingsFB2Dup.DupScanDir = tboxSourceDir.Text;
		}
		
		void BtnOpenDirClick(object sender, EventArgs e)
		{
			filesWorker.OpenDirDlg( tboxSourceDir, fbdScanDir, "Укажите папку для сканирования с fb2 файлами:" );
		}
		
		// Поиск одинаковых fb2-файлов
		void TsbtnSearchDublsClick(object sender, EventArgs e)
		{
			string sMessTitle = "SharpFBTools - Поиск одинаковых fb2 файлов";
			// проверка на корректность данных папок источника и приемника файлов
			if( !IsScanFolderDataCorrect( tboxSourceDir, ref sMessTitle ) )
				return;
			
			// инициализация контролов
			Init();
			ConnectListViewResultEventHandlers( false );
			Core.Duplicator.ComrareForm comrareForm = new Core.Duplicator.ComrareForm(
				null, chBoxIsValid.Checked, rbtnFB2Librusec.Checked,
				tboxSourceDir, chBoxScanSubDir, chBoxIsValid, cboxMode,
				lvFilesCount, lvResult, chBoxViewProgress.Checked
			);
			comrareForm.ShowDialog();
			comrareForm.Dispose();
			ConnectListViewResultEventHandlers( true );
		}
		
		// возобновить сравнение и поиск копий по данным из файла, созданного после прерывания обработки
		void TsbtnSearchFb2DupRenewClick(object sender, EventArgs e)
		{
			// загрузка данных из xml
			sfdLoadList.InitialDirectory = Settings.Settings.GetProgDir();
			sfdLoadList.Title		= "Укажите файл для возобновления поиска копий книг:";
			sfdLoadList.Filter		= "SharpFBTools Файлы хода работы Дубликатора (*.dup_break)|*.dup_break";
			sfdLoadList.FileName	= string.Empty;
			string FromXML			= string.Empty;
			DialogResult result = sfdLoadList.ShowDialog();
			XElement xmlTree = null;
			if( result == DialogResult.OK )
				xmlTree = XElement.Load( sfdLoadList.FileName );
			else
				return;
			
			// инициализация контролов
			Init();
			ConnectListViewResultEventHandlers( false );
			Core.Duplicator.ComrareForm comrareForm = new Core.Duplicator.ComrareForm(
				sfdLoadList.FileName, chBoxIsValid.Checked, rbtnFB2Librusec.Checked,
				tboxSourceDir, chBoxScanSubDir, chBoxIsValid, cboxMode,
				lvFilesCount, lvResult, chBoxViewProgress.Checked
			);
			comrareForm.ShowDialog();
			m_sSource = comrareForm.getSourceDirFromRenew();
			comrareForm.Dispose();
			ConnectListViewResultEventHandlers( true );
		}
		
		// занесение данных книги в контролы для просмотра
		void LvResultSelectedIndexChanged(object sender, EventArgs e)
		{
			#region Код
			ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
			// пропускаем ситуацию, когда курсор переходит от одной строки к другой - нет выбранного item'а
			if( si.Count > 0 ) {
				if( File.Exists( si[0].Text ) ) {
					FB2BookDescription	bd	= new FB2BookDescription( si[0].Text );
					// считываем данные TitleInfo
					m_mscLV.ListViewStatus( lvTitleInfo, 0, bd.TIBookTitle );
					m_mscLV.ListViewStatus( lvTitleInfo, 1, bd.TIGenres );
					m_mscLV.ListViewStatus( lvTitleInfo, 2, bd.TILang );
					m_mscLV.ListViewStatus( lvTitleInfo, 3, bd.TISrcLang );
					m_mscLV.ListViewStatus( lvTitleInfo, 4, bd.TIAuthors );
					m_mscLV.ListViewStatus( lvTitleInfo, 5, bd.TIDate );
					m_mscLV.ListViewStatus( lvTitleInfo, 6, bd.TIKeywords );
					m_mscLV.ListViewStatus( lvTitleInfo, 7, bd.TITranslators );
					m_mscLV.ListViewStatus( lvTitleInfo, 8, bd.TISequences );
					m_mscLV.ListViewStatus( lvTitleInfo, 9, bd.TICoverpagesCount.ToString() );
					// считываем данные SourceTitleInfo
					m_mscLV.ListViewStatus( lvSourceTitleInfo, 0, bd.STIBookTitle );
					m_mscLV.ListViewStatus( lvSourceTitleInfo, 1, bd.STIGenres );
					m_mscLV.ListViewStatus( lvSourceTitleInfo, 2, bd.STILang );
					m_mscLV.ListViewStatus( lvSourceTitleInfo, 3, bd.STISrcLang );
					m_mscLV.ListViewStatus( lvSourceTitleInfo, 4, bd.STIAuthors );
					m_mscLV.ListViewStatus( lvSourceTitleInfo, 5, bd.STIDate );
					m_mscLV.ListViewStatus( lvSourceTitleInfo, 6, bd.STIKeywords );
					m_mscLV.ListViewStatus( lvSourceTitleInfo, 7, bd.STITranslators );
					m_mscLV.ListViewStatus( lvSourceTitleInfo, 8, bd.STISequences );
					m_mscLV.ListViewStatus( lvSourceTitleInfo, 9, bd.STICoverpagesCount.ToString() );
					// считываем данные DocumentInfo
					m_mscLV.ListViewStatus( lvDocumentInfo, 0, bd.DIID );
					m_mscLV.ListViewStatus( lvDocumentInfo, 1, bd.DIVersion );
					m_mscLV.ListViewStatus( lvDocumentInfo, 2, bd.DIFB2Date );
					m_mscLV.ListViewStatus( lvDocumentInfo, 3, bd.DIProgramUsed );
					m_mscLV.ListViewStatus( lvDocumentInfo, 4, bd.DISrcOcr );
					m_mscLV.ListViewStatus( lvDocumentInfo, 5, bd.DISrcUrls );
					m_mscLV.ListViewStatus( lvDocumentInfo, 6, bd.DIFB2Authors );
					// считываем данные PublishInfo
					m_mscLV.ListViewStatus( lvPublishInfo, 0, bd.PIBookName );
					m_mscLV.ListViewStatus( lvPublishInfo, 1, bd.PIPublisher );
					m_mscLV.ListViewStatus( lvPublishInfo, 2, bd.PIYear );
					m_mscLV.ListViewStatus( lvPublishInfo, 3, bd.PICity );
					m_mscLV.ListViewStatus( lvPublishInfo, 4, bd.PIISBN );
					m_mscLV.ListViewStatus( lvPublishInfo, 5, bd.PISequences );
					// считываем данные CustomInfo
					lvCustomInfo.Items.Clear();
					IList<CustomInfo> lcu = bd.CICustomInfo;
					if( lcu != null ) {
						foreach( CustomInfo ci in lcu ) {
							ListViewItem lvi = new ListViewItem( ci.InfoType );
							lvi.SubItems.Add( ci.Value );
							lvCustomInfo.Items.Add( lvi );
						}
					}
					// считываем данные History
					rtbHistory.Clear(); rtbHistory.Text = GetDataFromTagP( bd.DIHistory );
					// считываем данные Annotation
					rtbAnnotation.Clear(); rtbAnnotation.Text = GetDataFromTagP( bd.TIAnnotation );
					// загрузка обложки
					if (bd.CoversBase64 != null) {
						picBoxCover.Image = Base64ToImage(bd.CoversBase64[0].base64String);
					} else {
						picBoxCover.Image = imageListDup.Images[0];
					}
					// Валидность файла
					tbValidate.Clear();
					if( si[0].SubItems[7].Text == "Нет" ) {
						string sResult	= rbtnFB2Librusec.Checked
							? m_fv2Validator.ValidatingFB2LibrusecFile( si[0].Text )
							: m_fv2Validator.ValidatingFB22File( si[0].Text );
						tbValidate.Text = "Файл невалидный. Ошибка:";
						tbValidate.AppendText( Environment.NewLine );
						tbValidate.AppendText( Environment.NewLine );
						tbValidate.AppendText( sResult );
					} else if( si[0].SubItems[7].Text == "Да" ) {
						tbValidate.Text = "Все в порядке - файл валидный!";
					} else {
						tbValidate.Text = "Валидация файла не производилась.";
					}
				}
			}
			#endregion
		}
		
		// запуск сканирования по нажатию Enter на поле ввода папки для сканирования
		void TboxSourceDirKeyPress(object sender, KeyPressEventArgs e)
		{
			if ( e.KeyChar == (char)Keys.Return )
				TsbtnSearchDublsClick( sender, e );
		}
		
		// (раз)блокировка кнопок групповой обработки помеченных книг
		void LvResultItemChecked(object sender, ItemCheckedEventArgs e)
		{
			groupWorkingChekedItemsEnabled( lvResult.CheckedItems.Count );
		}
		
		// сохранение списка найденных копий
		void TsbtnDupSaveListClick(object sender, EventArgs e)
		{
			#region Код
			if( lvResult.Items.Count==0 ) {
				MessageBox.Show( "Нет ни одной копии fb2 книг!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			sfdList.Title = "Укажите название файла копий:";
			sfdList.Filter = "SharpFBTools Файлы копий книг (*.dup_lbc)|*.dup_lbc";
			sfdList.FileName = string.Empty;
			sfdList.InitialDirectory = Settings.Settings.GetProgDir();
			DialogResult result = sfdList.ShowDialog();
			if( result == DialogResult.OK ) {
				Environment.CurrentDirectory = Settings.Settings.GetProgDir();
				saveCopiesListToXml( sfdList.FileName, cboxMode.SelectedIndex, cboxMode.Text);
				MessageBox.Show( "Сохранение списка копий fb2 книг завершено!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
			}
			
			#endregion
		}
		
		// загрузка списка копий
		void TsbtnDupOpenListClick(object sender, EventArgs e)
		{
			#region Код
			sfdLoadList.InitialDirectory = Settings.Settings.GetProgDir();
			sfdLoadList.Title 		= "Загрузка Списка копий книг:";
			sfdLoadList.Filter		= "SharpFBTools Файлы копий книг (*.dup_lbc)|*.dup_lbc";
			sfdLoadList.FileName	= string.Empty;
			string FromXML = string.Empty;
			DialogResult result = sfdLoadList.ShowDialog();
			if( result == DialogResult.OK ) {
				FromXML = sfdLoadList.FileName;
				// инициализация контролов
				Init();
				// установка режима поиска
				if( !File.Exists( FromXML ) ) {
					MessageBox.Show( "Не найден файл списка копий fb2 книг: \""+FromXML+"\"!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return;
				}
				XmlReaderSettings data = new XmlReaderSettings();
				data.IgnoreWhitespace = true;
				// отключаем обработчики событий для lvResult (убираем "тормоза")
				ConnectListViewResultEventHandlers( false );
				bool Result = false;
				try {
					lvResult.BeginUpdate();
					loadCopiesListFromXML( FromXML );
					Result = true;
					// Отобразим результат в индикаторе прогресса
					m_mscLV.ListViewStatus( lvFilesCount, 5, lvResult.Groups.Count );
					m_mscLV.ListViewStatus( lvFilesCount, 6, lvResult.Items.Count );
				} catch {
					Result = false;
				} finally {
					lvResult.EndUpdate();
					ConnectListViewResultEventHandlers( true );
				}
				
				if(Result)
					MessageBox.Show( "Список копий fb2 книг загружен.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				else
					MessageBox.Show( "Поврежден файл списка копий fb2 книг: \""+FromXML+"\"!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				
			}
			#endregion
		}
		#endregion
		
		// =============================================================================================
		// 								ОБРАБОТЧИКИ для контекстного меню
		// =============================================================================================
		#region Обработчики для контекстного меню
		// удаление выделенного файла с диска
		void TsmiDeleteFileFromDiskClick(object sender, EventArgs e)
		{
			#region Код
			ConnectListViewResultEventHandlers( false );
			if( lvResult.Items.Count > 0 && lvResult.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
				ListViewGroup	lvg			= si[0].Group;
				string			sFilePath	= si[0].SubItems[0].Text.Split('/')[0];
				string sTitle = "SharpFBTools - Удаление файла с диска";
				if( !File.Exists( sFilePath ) ) {
					if( MessageBox.Show( "Файл: \""+sFilePath+"\" не найден!\nУдалить путь к этому файлу из списка?",
					                    sTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes ) {
						lvResult.Items[ lvResult.SelectedItems[0].Index ].Remove();
					}
				} else {
					if( MessageBox.Show( "Вы действительно хотите удалить файл: \""+sFilePath+"\" с диска?", sTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes ) {
						File.Delete( sFilePath );
						lvResult.Items[ lvResult.SelectedItems[0].Index ].Remove();
					} else
						return;
				}
				
				// удаление оставшейся книги в группе и самой группы с контрола отображения (1 книга - это уже не копия)
				workingGroupItemAfterBookDelete( lvResult, lvg );
				// обновление числа групп и книг во всех группах
				newGroupItemsCount( lvResult, lvFilesCount );
			}
			ConnectListViewResultEventHandlers( true );
			#endregion
		}
		
		// Открыть папку для выделенного файла
		void TsmiOpenFileDirClick(object sender, EventArgs e)
		{
			#region Код
			if( lvResult.Items.Count > 0 && lvResult.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
				FileInfo fi = new FileInfo( si[0].SubItems[0].Text.Split('/')[0] );
				string sDir = fi.Directory.ToString();
				if( !Directory.Exists( sDir ) ) {
					MessageBox.Show( "Папка: \""+sDir+"\" не найдена!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				filesWorker.ShowAsyncDir( sDir );
			}
			#endregion
		}
		
		// запустить файл в fb2-читалке (Просмотр)
		void TsmiViewInReaderClick(object sender, EventArgs e)
		{
			#region Код
			// читаем путь к читалке из настроек
			string sFBReaderPath = Settings.Settings.ReadFBReaderPath();
			string sTitle = "SharpFBTools - Открытие папки для файла";
			if( !File.Exists( sFBReaderPath ) ) {
				MessageBox.Show( "Не могу найти Читалку \""+sFBReaderPath+"\"!\nПроверьте, правильно ли задан путь в Настройках.", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			if( lvResult.Items.Count > 0 && lvResult.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
				string sFilePath = si[0].SubItems[0].Text.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: \""+sFilePath+"\" не найден!", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				filesWorker.StartAsyncFile( sFBReaderPath, sFilePath );
			}
			#endregion
		}
		
		// редактировать выделенный файл в fb2-редакторе
		void TsmiEditInFB2EditorClick(object sender, EventArgs e)
		{
			#region Код
			// читаем путь к FBE из настроек
			string sFBEPath = Settings.Settings.ReadFBEPath();
			string sTitle = "SharpFBTools - Открытие файла в fb2-редакторе";
			if( !File.Exists( sFBEPath ) ) {
				MessageBox.Show( "Не могу найти fb2 редактор \""+sFBEPath+"\"!\nПроверьте, правильно ли задан путь в Настройках.", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			if( lvResult.Items.Count > 0 && lvResult.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
				string sFilePath = si[0].SubItems[0].Text.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: \""+sFilePath+"\" не найден!", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				filesWorker.StartAsyncFile( sFBEPath, sFilePath );
			}
			#endregion
		}
		
		// редактировать выделенный файл в текстовом редакторе
		void TsmiEditInTextEditorClick(object sender, EventArgs e)
		{
			#region Код
			// читаем путь к текстовому редактору из настроек
			string sTFB2Path = Settings.Settings.ReadTextFB2EPath();
			string sTitle = "SharpFBTools - Открытие файла в текстовом редакторе";
			if( !File.Exists( sTFB2Path ) ) {
				MessageBox.Show( "Не могу найти текстовый редактор \""+sTFB2Path+"\"!\nПроверьте, правильно ли задан путь в Настройках.", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			if( lvResult.Items.Count > 0 && lvResult.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
				string sFilePath = si[0].SubItems[0].Text.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: \""+sFilePath+"\" не найден!", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				filesWorker.StartAsyncFile( sTFB2Path, sFilePath );
			}
			#endregion
		}
		
		// Повторная Проверка выбранного fb2-файла (Валидация)
		void TsmiFileReValidateClick(object sender, EventArgs e)
		{
			#region Код
			if( lvResult.Items.Count > 0 && lvResult.SelectedItems.Count != 0 ) {
				DateTime dtStart = DateTime.Now;
				ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
				string sSelectedItemText = si[0].SubItems[(int)ResultViewCollumn.Path].Text;
				string sFilePath = sSelectedItemText.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: \""+sFilePath+"\" не найден!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				MessageBoxIcon mbi = MessageBoxIcon.Information;
				string sMsg			= string.Empty;
				string sErrorMsg	= "СООБЩЕНИЕ ОБ ОШИБКЕ:";
				string sOkMsg		= "ОШИБОК НЕТ - ФАЙЛ ВАЛИДЕН";
				// для несжатого fb2-файла
				sMsg = rbtnFB2Librusec.Checked ? m_fv2Validator.ValidatingFB2LibrusecFile( sFilePath ) : m_fv2Validator.ValidatingFB22File( sFilePath );
				if ( sMsg == string.Empty ) {
					// файл валидный
					mbi = MessageBoxIcon.Information;
					sErrorMsg = sOkMsg;
					si[0].SubItems[(int)ResultViewCollumn.Validate].Text = "Да";
				} else {
					// файл не валидный
					mbi = MessageBoxIcon.Error;
					si[0].SubItems[(int)ResultViewCollumn.Validate].Text = "Нет";
				}
				DateTime dtEnd = DateTime.Now;
				string sTime = dtEnd.Subtract( dtStart ).ToString() + " (час.:мин.:сек.)";
				MessageBox.Show( "Проверка выделенного файла на соответствие FictionBook.xsd схеме завершена.\nЗатрачено времени: "+sTime+"\n\nФайл: \""+sFilePath+"\"\n\n"+sErrorMsg+"\n"+sMsg, "SharpFBTools - "+sErrorMsg, MessageBoxButtons.OK, mbi );
			}
			#endregion
		}
		
		// Повторная Проверка всех fb2-файлов одной Группы (Валидация)
		void TsmiAllFilesInGroupReValidateClick(object sender, EventArgs e)
		{
			if( lvResult.Items.Count > 0 ) {
				ConnectListViewResultEventHandlers( false );
				Core.Duplicator.ValidatorForm validatorForm = new Core.Duplicator.ValidatorForm(
					false, rbtnFB2Librusec.Checked, lvResult
				);
				validatorForm.ShowDialog();
				validatorForm.Dispose();
				ConnectListViewResultEventHandlers( true );
			}
		}
		
		// Повторная Проверка всех fb2-файлов всех Групп (Валидация)
		void TsmiAllGroupsReValidateClick(object sender, EventArgs e)
		{
			if( lvResult.Items.Count > 0 ) {
				ConnectListViewResultEventHandlers( false );
				Core.Duplicator.ValidatorForm validatorForm = new Core.Duplicator.ValidatorForm(
					true, rbtnFB2Librusec.Checked, lvResult
				);
				validatorForm.ShowDialog();
				validatorForm.Dispose();
				ConnectListViewResultEventHandlers( true );
			}
		}
		
		// diff - две помеченные fb2-книги
		void TsmiDiffFB2Click(object sender, EventArgs e)
		{
			#region Код
			ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
			if( lvResult.Items.Count > 0 && lvResult.SelectedItems.Count != 0 ) {
				// проверка на наличие diff-программы
				string sDiffTitle = "SharpFBTools - diff";
				string sDiffPath = Settings.Settings.ReadDiffPath();
				
				if( sDiffPath.Trim().Length==0 ) {
					MessageBox.Show( "В Настройках не указан путь к установленной diff-программе визуального сравнения файлов!\nУкажите путь к ней в Настройках.\nРабота остановлена!",
					                sDiffTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return;
				} else {
					if( !File.Exists( sDiffPath ) ) {
						MessageBox.Show( "Не найден файл diff-программы визуального сравнения файлов \""+sDiffPath+"\"!\nУкажите путь к ней в Настройках.\nРабота остановлена!",
						                sDiffTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
						return;
					}
				}
				// группа для выделенной книги
				ListViewGroup lvg = si[0].Group;
				// книги выбранной группы
				ListView.ListViewItemCollection glvic = lvg.Items;
				List<string> l = new List<string>();
				foreach( ListViewItem lvi in glvic ) {
					if( lvi.Checked )
						l.Add( lvi.Text );
					if( l.Count==2 )
						break;
				}
				// запускаем инструмент сравнения
				if( l.Count==2 ) {
					string sFilesNotExists = string.Empty;
					if( !File.Exists( l[0] ) )
						sFilesNotExists += l[0]; sFilesNotExists += "\n";

					if( !File.Exists( l[1] ) )
						sFilesNotExists += l[1]; sFilesNotExists += "\n";

					if( sFilesNotExists.Length!=0 )
						MessageBox.Show( "Не найден(ы) файл(ы) для сравнения:\n"+sFilesNotExists+"\nРабота остановлена!",
						                sDiffTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					else
						filesWorker.StartAsyncDiff( sDiffPath, l[0], l[1] );
				}
			}
			#endregion
		}
		
		// отметить все книги выбранной Группы
		void TsmiCheckedAllInGroupClick(object sender, EventArgs e)
		{
			ConnectListViewResultEventHandlers( false );
			ListView.SelectedListViewItemCollection si = lvResult.SelectedItems;
			if (si.Count > 0) {
				// группа для выделенной книги
				ListViewGroup lvg = si[0].Group;
				m_mscLV.CheckAllListViewItemsInGroup( lvg, true );
			}
			ConnectListViewResultEventHandlers( true );
			groupWorkingChekedItemsEnabled( lvResult.CheckedItems.Count );
		}
		
		// отметить все книги всех Групп
		void TsmiCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListViewResultEventHandlers( false );
			m_mscLV.CheckAllListViewItems( lvResult, true );
			ConnectListViewResultEventHandlers( true );
			groupWorkingChekedItemsEnabled( lvResult.CheckedItems.Count );
		}
		
		// снять отметки со всех книг
		void TsmiUnCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListViewResultEventHandlers( false );
			m_mscLV.UnCheckAllListViewItems( lvResult.CheckedItems );
			ConnectListViewResultEventHandlers( true );
			groupWorkingChekedItemsEnabled( lvResult.CheckedItems.Count );
		}
		
		// пометить в каждой группе все "старые" книги (по тэгу version)
		void TsmiAllOldBooksForAllGroupsClick(object sender, EventArgs e)
		{
			ConnectListViewResultEventHandlers( false );
			CheckAllOldBooksInAllGroups(CompareMode.Version);
			ConnectListViewResultEventHandlers( true );
		}
		
		// пометить в каждой группе все "старые" книги (по времени создания файла)
		void TsmiAllOldBooksCreationTimeForAllGroupsClick(object sender, EventArgs e)
		{
			ConnectListViewResultEventHandlers( false );
			CheckAllOldBooksInAllGroups(CompareMode.CreationTime);
			ConnectListViewResultEventHandlers( true );
		}
		
		// пометить в каждой группе все "старые" книги (по времени последнего изменения файла)
		void TsmiAllOldBooksLastWriteTimeForAllGroupsClick(object sender, EventArgs e)
		{
			ConnectListViewResultEventHandlers( false );
			CheckAllOldBooksInAllGroups(CompareMode.LastWriteTime);
			ConnectListViewResultEventHandlers( true );
		}
		
		// пометить в выбранной группе все "старые" книги (по тэгу version)
		void TsmiAnalyzeInGroupClick(object sender, EventArgs e)
		{
			ConnectListViewResultEventHandlers( false );
			CheckAllOldBooksInGroup(CompareMode.Version);
			ConnectListViewResultEventHandlers( true );
		}
		
		// пометить в выбранной группе все "старые" книги (по времени создания файла)
		void TsmiAllOldBooksCreationTimeInGroupClick(object sender, EventArgs e)
		{
			ConnectListViewResultEventHandlers( false );
			CheckAllOldBooksInGroup(CompareMode.CreationTime);
			ConnectListViewResultEventHandlers( true );
		}
		
		// пометить в выбранной группе все "старые" книги (по времени последнего изменения файла)
		void TsmiAllOldBooksLastWriteTimeInGroupClick(object sender, EventArgs e)
		{
			ConnectListViewResultEventHandlers( false );
			CheckAllOldBooksInGroup(CompareMode.LastWriteTime);
			ConnectListViewResultEventHandlers( true );
		}
		
		// копировать помеченные файлы в папку-приемник
		void TsmiCopyCheckedFb2ToClick(object sender, EventArgs e)
		{
			string sTarget = filesWorker.OpenDirDlg( m_TargetDir, fbdScanDir, "Укажите папку-приемник для размешения копий книг:" );
			if( sTarget == null )
				return;
			else
				Settings.SettingsFB2Dup.DupToDir = m_TargetDir = sTarget;

			if( m_sSource == sTarget ) {
				MessageBox.Show( "Папка-приемник файлов совпадает с папкой сканирования!\nРабота прекращена.",
				                "SharpFBTools - Копирование копий книг", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			
			ConnectListViewResultEventHandlers( false );
			Core.Duplicator.CopyMoveDeleteForm comrareForm = new Core.Duplicator.CopyMoveDeleteForm(
				"Copy", m_sSource, sTarget, cboxDupExistFile.SelectedIndex, lvFilesCount, lvResult
			);
			comrareForm.ShowDialog();
			comrareForm.Dispose();
			ConnectListViewResultEventHandlers( true );
		}
		
		// переместить помеченные файлы в папку-приемник
		void TsmiMoveCheckedFb2ToClick(object sender, EventArgs e)
		{
			string sTarget = filesWorker.OpenDirDlg( m_TargetDir, fbdScanDir, "Укажите папку-приемник для размешения копий книг:" );
			if( sTarget == null )
				return;
			else
				Settings.SettingsFB2Dup.DupToDir = m_TargetDir = sTarget;
			
			if( m_sSource == sTarget ) {
				MessageBox.Show( "Папка-приемник файлов совпадает с папкой сканирования!\nРабота прекращена.",
				                "SharpFBTools - Перемещение копий книг", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			
			ConnectListViewResultEventHandlers( false );
			Core.Duplicator.CopyMoveDeleteForm comrareForm = new Core.Duplicator.CopyMoveDeleteForm(
				"Move", m_sSource, sTarget, cboxDupExistFile.SelectedIndex, lvFilesCount, lvResult
			);
			comrareForm.ShowDialog();
			comrareForm.Dispose();
			ConnectListViewResultEventHandlers( true );
		}
		
		// удалить помеченные файлы
		void TsmiDeleteCheckedFb2Click(object sender, EventArgs e)
		{
			string sMessTitle = "SharpFBTools - Удаление копий книг";
			int nCount = lvResult.CheckedItems.Count;
			string sMess = "Вы действительно хотите удалить "+nCount.ToString()+" помеченных копии книг?";
			MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			if( MessageBox.Show( sMess, sMessTitle, buttons, MessageBoxIcon.Question ) != DialogResult.No ) {
				ConnectListViewResultEventHandlers( false );
				Core.Duplicator.CopyMoveDeleteForm comrareForm = new Core.Duplicator.CopyMoveDeleteForm(
					"Delete", m_sSource, null, cboxDupExistFile.SelectedIndex, lvFilesCount, lvResult
				);
				comrareForm.ShowDialog();
				comrareForm.Dispose();
				ConnectListViewResultEventHandlers( true );
			}
		}
		#endregion
	}
}
