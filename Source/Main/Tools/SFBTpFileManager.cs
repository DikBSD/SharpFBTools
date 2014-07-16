/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 16.03.2009
 * Time: 9:03
 * 
 * License: GPL 2.1
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Linq;
using System.Xml.Linq;
using System.Threading;
using System.Diagnostics;

using Core.FB2.FB2Parsers;
using Core.FB2.Description.TitleInfo;
using Core.FB2.Description.Common;
using Core.FB2.Genres;
using Core.BookSorting;
using Core.FileManager;
using Core.Misc;
using Settings;

using Core.Templates.Lexems;

using fB2Parser					= Core.FB2.FB2Parsers.FB2Parser;
using filesWorker				= Core.FilesWorker.FilesWorker;
using fb2Validator				= Core.FB2Parser.FB2Validator;
using templatesParser			= Core.Templates.TemplatesParser;
using templatesVerify			= Core.Templates.TemplatesVerify;
using templatesLexemsSimple		= Core.Templates.Lexems.TPSimple;
using selectedSortQueryCriteria	= Core.BookSorting.SortQueryCriteria;

namespace SharpFBTools.Tools
{
	/// <summary>
	/// Режим сортировки книг - по числу Авторов и Жанров
	/// </summary>
	public enum SortModeType {
		_1Genre1Author,			// по первому Жанру и первому Автору Книги
		_1GenreAllAuthor, 		// по первому Жанру и всем Авторам Книги
		_AllGenre1Author,		// по всем Жанрам и первому Автору Книги
		_AllGenreAllAuthor, 	// по всем Жанрам и всем Авторам Книги
	}
	
	/// <summary>
	/// Сортировщик fb2 и fb2.zip файлов
	/// </summary>
	public partial class SFBTpFileManager : UserControl
	{
		#region Закрытые данные класса
		private bool m_isSettingsLoaded			= false; // Только при true все изменения настроек сохраняются в файл.
		private bool m_ViewMessageForLongTime	= true; // показывать предупреждение о том, что вкл. опции отображения метаданных потребует много времени...
		private fb2Validator fv2V				= new fb2Validator();
		private SortingOptions	 				m_sortOptions	= null; // индивидуальные настройки обоих Сортировщиков, взависимости от режима (непрерывная сортировка или возобновление сортировки)
		
		private string m_sMessTitle		= string.Empty;
		private bool m_bFullSort		= true;
		private bool m_SSFB2Librusec	= true; // схема Жанров для Избранной сортировки - Либрусек
		private Core.FileManager.StatusView m_sv	= new Core.FileManager.StatusView();
		private MiscListView m_mscLV				= new MiscListView(); // класс по работе с ListView
		private const string m_space				= " "; // для задания отступов данных от границ колонов в Списке
		private Core.FilesWorker.SharpZipLibWorker sharpZipLib = new Core.FilesWorker.SharpZipLibWorker();
		private FullNameTemplates m_fnt = new FullNameTemplates();
		private string m_TempDir = Settings.Settings.TempDir;
		
		private IFBGenres m_fb2FullSortGenres = null; // спиок жанров для Полной Сортировки для режима отображения метаданных для файлов Проводника
		#endregion
		
		public SFBTpFileManager()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			Init();

			/* Настройки Менеджера Файлов по-умолчанию*/
			// основные настройки
			DefFMGeneral();
			// название папки шаблонного тэга без данных
			DefFMDirNameForTagNotData();
			// название для данных Издательства из Description когда нет данных тэга
			DefFMDirNameForPublisherTagNotData();
			// название для данных fb2-файла из Description когда нет данных тэга
			DefFMDirNameForFB2TagNotData();
			// название Групп Жанров
			DefFMGenresGroups();
			
			/* читаем сохраненные пути к папкам и шаблон Менеджера Файлов, если они есть */
			readSettingsFromXML();
			m_isSettingsLoaded = true;
			lvFilesCount.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
			rtboxTemplatesList.Clear();
			
			// спиок жанров для Полной Сортировки для режима отображения метаданных для файлов Проводника
			SortingFB2Tags	sortTags = new SortingFB2Tags(null);
			if( rbtnFMFSFB2Librusec.Checked )
				m_fb2FullSortGenres = new FB2LibrusecGenres( ref sortTags );
			else
				m_fb2FullSortGenres = new FB22Genres( ref sortTags );
			
			string sTDPath = Settings.FileManagerSettings.DefFMDescTemplatePath;
			if( File.Exists( sTDPath ) )
				rtboxTemplatesList.LoadFile( sTDPath );
			else
				rtboxTemplatesList.Text = "Не найден файл описания Шаблонов подстановки: \""+sTDPath+"\"";
		}
		
		#region Настройки по-умолчанию для вкладок
		private void DefFMGeneral() {
			// основные для Менеджера Файлов
			chBoxTranslit.Checked = Settings.FileManagerSettings.DefTranslit;
			chBoxStrict.Checked = Settings.FileManagerSettings.DefStrict;
			cboxSpace.SelectedIndex = Settings.FileManagerSettings.DefSpace;
			cboxFileExist.SelectedIndex = Settings.FileManagerSettings.DefFileExist;
			rbtnAsIs.Checked = Settings.FileManagerSettings.DefRegisterAsIs;
			rbtnAsSentence.Checked = Settings.FileManagerSettings.DefRegisterAsSentence;
			rbtnLower.Checked = Settings.FileManagerSettings.DefRegisterLower;
			rbtnUpper.Checked = Settings.FileManagerSettings.DefRegisterUpper;
			rbtnGenreOne.Checked = Settings.FileManagerSettings.DefGenreOne;
			rbtnGenreAll.Checked = Settings.FileManagerSettings.DefGenreAll;
			rbtnAuthorOne.Checked = Settings.FileManagerSettings.DefAuthorOne;
			rbtnAuthorAll.Checked = Settings.FileManagerSettings.DefAuthorAll;
			rbtnGenreSchema.Checked = Settings.FileManagerSettings.DefGenreSchema;
			rbtnGenreText.Checked = Settings.FileManagerSettings.DefGenreText;
			rbtnFMAllFB2.Checked		= Settings.FileManagerSettings.DefAllFB2;
			rbtnFMOnlyValidFB2.Checked	= Settings.FileManagerSettings.DefOnlyValidFB2;
		}
		private void DefFMDirNameForTagNotData() {
			// название папки шаблонного тэга без данных
			txtBoxFMNoGenreGroup.Text	= Settings.FileManagerSettings.DefNoGenreGroup;
			txtBoxFMNoGenre.Text		= Settings.FileManagerSettings.DefNoGenre;
			txtBoxFMNoLang.Text			= Settings.FileManagerSettings.DefNoLang;
			txtBoxFMNoFirstName.Text	= Settings.FileManagerSettings.DefNoFirstName;
			txtBoxFMNoMiddleName.Text	= Settings.FileManagerSettings.DefNoMiddleName;
			txtBoxFMNoLastName.Text		= Settings.FileManagerSettings.DefNoLastName;
			txtBoxFMNoNickName.Text		= Settings.FileManagerSettings.DefNoNickName;
			txtBoxFMNoBookTitle.Text	= Settings.FileManagerSettings.DefNoBookTitle;
			txtBoxFMNoSequence.Text		= Settings.FileManagerSettings.DefNoSequence;
			txtBoxFMNoNSequence.Text	= Settings.FileManagerSettings.DefNoNSequence;
			txtBoxFMNoDateText.Text		= Settings.FileManagerSettings.DefNoDateText;
			txtBoxFMNoDateValue.Text	= Settings.FileManagerSettings.DefNoDateValue;
		}
		private void DefFMDirNameForPublisherTagNotData() {
			// название для данных Издательства из Description когда нет данных тэга
			txtBoxFMNoYear.Text			= Settings.FileManagerSettings.DefNoYear;
			txtBoxFMNoPublisher.Text	= Settings.FileManagerSettings.DefNoPublisher;
			txtBoxFMNoCity.Text			= Settings.FileManagerSettings.DefNoCity;
		}
		private void DefFMDirNameForFB2TagNotData() {
			// название для данных fb2-файла из Description когда нет данных тэга
			txtBoxFMNoFB2FirstName.Text		= Settings.FileManagerSettings.DefNoFB2FirstName;
			txtBoxFMNoFB2MiddleName.Text	= Settings.FileManagerSettings.DefNoFB2MiddleName;
			txtBoxFMNoFB2LastName.Text		= Settings.FileManagerSettings.DefNoFB2LastName;
			txtBoxFMNoFB2NickName.Text		= Settings.FileManagerSettings.DefNoFB2NickName;
		}
		
		private void DefFMGenresGroups() {
			// название Групп Жанров
			txtboxFMsf.Text			= Settings.FileManagerSettings.DefGenresGroupSf;
			txtboxFMdetective.Text	= Settings.FileManagerSettings.DefGenresGroupDetective;
			txtboxFMprose.Text		= Settings.FileManagerSettings.DefGenresGroupProse;
			txtboxFMlove.Text		= Settings.FileManagerSettings.DefGenresGroupLove;
			txtboxFMadventure.Text	= Settings.FileManagerSettings.DefGenresGroupAdventure;
			txtboxFMchildren.Text	= Settings.FileManagerSettings.DefGenresGroupChildren;
			txtboxFMpoetry.Text		= Settings.FileManagerSettings.DefGenresGroupPoetry;
			txtboxFMantique.Text	= Settings.FileManagerSettings.DefGenresGroupAntique;
			txtboxFMscience.Text	= Settings.FileManagerSettings.DefGenresGroupScience;
			txtboxFMcomputers.Text	= Settings.FileManagerSettings.DefGenresGroupComputers;
			txtboxFMreference.Text	= Settings.FileManagerSettings.DefGenresGroupReference;
			txtboxFMnonfiction.Text	= Settings.FileManagerSettings.DefGenresGroupNonfiction;
			txtboxFMreligion.Text	= Settings.FileManagerSettings.DefGenresGroupReligion;
			txtboxFMhumor.Text		= Settings.FileManagerSettings.DefGenresGroupHumor;
			txtboxFMhome.Text		= Settings.FileManagerSettings.DefGenresGroupHome;
			txtboxFMbusiness.Text	= Settings.FileManagerSettings.DefGenresGroupBusiness;
			txtboxFMtech.Text		= Settings.FileManagerSettings.DefGenresGroupTech;
			txtboxFMmilitary.Text	= Settings.FileManagerSettings.DefGenresGroupMilitary;
			txtboxFMfolklore.Text	= Settings.FileManagerSettings.DefGenresGroupFolklore;
			txtboxFMother.Text		= Settings.FileManagerSettings.DefGenresGroupOther;
		}
		#endregion
		
		#region Закрытые вспомогательные методы класса
		private void ConnectListsEventHandlers( bool bConnect ) {
			if( !bConnect ) {
				// отключаем обработчики событий для Списка (убираем "тормоза")
				this.listViewSource.DoubleClick -= new System.EventHandler(this.ListViewSourceDoubleClick);
				this.listViewSource.ItemChecked -= new System.Windows.Forms.ItemCheckedEventHandler(this.ListViewSourceItemChecked);
				this.listViewSource.ItemCheck -= new System.Windows.Forms.ItemCheckEventHandler(this.ListViewSourceItemCheck);
				this.listViewSource.KeyPress -= new System.Windows.Forms.KeyPressEventHandler(this.ListViewSourceKeyPress);
			} else {
				// подключаем обработчики событий для Списка
				this.listViewSource.DoubleClick += new System.EventHandler(this.ListViewSourceDoubleClick);
				this.listViewSource.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.ListViewSourceItemChecked);
				this.listViewSource.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.ListViewSourceItemCheck);
				this.listViewSource.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ListViewSourceKeyPress);
			}
		}

		// инициализация контролов и переменных
		private void Init() {
			for( int i=0; i!=lvFilesCount.Items.Count; ++i )
				lvFilesCount.Items[i].SubItems[1].Text	= "0";

			// очистка временной папки
			filesWorker.RemoveDir( m_TempDir );
			m_sv.Clear();
		}
		
		// Полная Сортировка: проверка на корректность данных папок источника и приемника файлов
		private bool IsSourceDirDataCorrect(  string SourseDir, string TargetDir )
		{
			// проверки на корректность папок источника и приемника
			if( SourseDir.Length == 0 ) {
				MessageBox.Show( "Выберите папку для сканирования!", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			DirectoryInfo diFolder = new DirectoryInfo( SourseDir );
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка не найдена: " + SourseDir, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка папки-приемника и создание ее, если нужно
			if( !filesWorker.CreateDirIfNeed( TargetDir, m_sMessTitle ) )
				return false;

			return true;
		}
		
		// Селективная Сортировка: проверка на корректность данных папок источника и приемника файлов
		private bool IsFoldersDataCorrect( string SourseDir, string TargetDir )
		{
			// проверки на корректность папок источника и приемника
			if( SourseDir.Length == 0 ) {
				MessageBox.Show( "Выберите папку для сканирования!", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			DirectoryInfo diFolder = new DirectoryInfo( SourseDir );
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка не найдена: " + SourseDir, m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			if( TargetDir.Length == 0 ) {
				MessageBox.Show( "Не указана папка-приемник файлов!\nРабота прекращена.", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			if( SourseDir == TargetDir ) {
				MessageBox.Show( "Папка-приемник файлов совпадает с папкой сканирования!\nРабота прекращена.", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка папки-приемника и создание ее, если нужно
			if( !filesWorker.CreateDirIfNeed( TargetDir, m_sMessTitle ) )
				return false;

			return true;
		}
		
		// сохранение настроек в xml-файл
		private void saveSettingsToXml() {
			#region Код
			// защита от "затирания" настроек в файле, когда в некоторые контролы данные еще не загрузились
			if( m_isSettingsLoaded ) {
				XDocument doc = new XDocument(
					new XDeclaration("1.0", "utf-8", "yes"),
					new XElement("Settings", new XAttribute("type", "sort_settings"),
					             new XComment("xml файл настроек Сортировщика"),
					             new XComment("Полная Сортировка"),
					             new XElement("FullSorting",
					                          new XComment("Папка исходных fb2-файлов"),
					                          new XElement("SourceDir", textBoxAddress.Text.Trim()),
					                          new XComment("Папка-приемник fb2-файлов"),
					                          new XElement("TargetDir", labelTargetPath.Text),
					                          new XComment("Шаблон подстановки"),
					                          new XElement("Template", txtBoxTemplatesFromLine.Text.Trim()),
					                          new XComment("Активная Схема Жанров"),
					                          new XElement("FB2Genres",
					                                       new XAttribute("Librusec", rbtnFMFSFB2Librusec.Checked),
					                                       new XAttribute("FB22", rbtnFMFSFB22.Checked)
					                                      ),
					                          new XComment("Настройки Полной Сортировки"),
					                          new XElement("Options",
					                                       new XComment("Обрабатывать подкаталоги"),
					                                       new XElement("ScanSubDirs", chBoxScanSubDir.Checked),
					                                       new XComment("Архивировать в zip"),
					                                       new XElement("ToZip", chBoxFSToZip.Checked),
					                                       new XComment("Сохранять оригиналы"),
					                                       new XElement("NotDelFB2Files", chBoxFSNotDelFB2Files.Checked),
					                                       new XComment("Авторазмер колонок Проводника"),
					                                       new XElement("StartExplorerColumnsAutoReize", chBoxStartExplorerColumnsAutoReize.Checked),
					                                       new XComment("Показывать описание книг в Проводнике"),
					                                       new XElement("BooksTagsView", checkBoxTagsView.Checked),
					                                       new XComment("Показывать сообщение о том, что требуется много времени на генерацию метаданных"),
					                                       new XElement("ViewMessageForLongTime", m_ViewMessageForLongTime)
					                                      )
					                         ),
					             new XComment("Избранная Сортировка"),
					             new XElement("SelectedSorting",
					                          new XComment("Папка исходных fb2-файлов"),
					                          new XElement("SourceDir", tboxSSSourceDir.Text.Trim()),
					                          new XComment("Папка-приемник fb2-файлов"),
					                          new XElement("TargetDir", tboxSSToDir.Text.Trim()),
					                          new XComment("Шаблон подстановки"),
					                          new XElement("Template", txtBoxSSTemplatesFromLine.Text.Trim()),
					                          new XComment("Активная Схема Жанров"),
					                          new XElement("FB2Genres",
					                                       new XAttribute("Librusec", m_SSFB2Librusec),
					                                       new XAttribute("FB22", !m_SSFB2Librusec)
					                                      ),
					                          new XComment("Настройки Избранной Сортировки"),
					                          new XElement("Options",
					                                       new XComment("Обрабатывать подкаталоги"),
					                                       new XElement("ScanSubDirs", chBoxSSScanSubDir.Checked),
					                                       new XComment("Архивировать в zip"),
					                                       new XElement("ToZip", chBoxSSToZip.Checked),
					                                       new XComment("Сохранять оригиналы"),
					                                       new XElement("NotDelFB2Files", chBoxSSNotDelFB2Files.Checked)
					                                      )
					                         ),
					             new XElement("Log",
					                          new XComment("Отображать изменения хода работы"),
					                          new XElement("Progress", chBoxViewProgress.Checked)
					                         ),
					             new XComment("Общие настройки для обоих режимов Сортировки"),
					             new XElement("CommonOptions",
					                          new XComment("Основные настройки"),
					                          new XElement("General",
					                                       new XComment("Регистр имени файла"),
					                                       new XElement("Register",
					                                                    new XAttribute("AsIs", rbtnAsIs.Checked),
					                                                    new XAttribute("Lower", rbtnLower.Checked),
					                                                    new XAttribute("Upper", rbtnUpper.Checked),
					                                                    new XAttribute("AsSentence", rbtnAsSentence.Checked)
					                                                   ),
					                                       new XComment("Транслитерация имен файлов"),
					                                       new XElement("Translit", chBoxTranslit.Checked),
					                                       new XComment("'Строгие' имена файлов: алфавитно-цифровые символы, а так же [](){}-_"),
					                                       new XElement("Strict", chBoxStrict.Checked),
					                                       new XComment("Обработка пробелов"),
					                                       new XElement("Space",
					                                                    new XAttribute("index", cboxSpace.SelectedIndex),
					                                                    new XAttribute("Name", cboxSpace.Text)
					                                                   ),
					                                       new XComment("Одинаковые файлы"),
					                                       new XElement("FileExistMode",
					                                                    new XAttribute("index", cboxFileExist.SelectedIndex),
					                                                    new XAttribute("Name", cboxFileExist.Text)
					                                                   ),
					                                       new XComment("Сортировка файлов"),
					                                       new XElement("SortType",
					                                                    new XAttribute("AllFB2", rbtnFMAllFB2.Checked),
					                                                    new XAttribute("OnlyValidFB2", rbtnFMOnlyValidFB2.Checked)
					                                                   ),
					                                       new XComment("Раскладка файлов по папкам"),
					                                       new XElement("FilesToDirs",
					                                                    new XComment("По Авторам"),
					                                                    new XElement("AuthorsToDirs",
					                                                                 new XAttribute("AuthorOne", rbtnAuthorOne.Checked),
					                                                                 new XAttribute("AuthorAll", rbtnAuthorAll.Checked)
					                                                                ),
					                                                    new XComment("По Жанрам"),
					                                                    new XElement("GenresToDirs",
					                                                                 new XAttribute("GenreOne", rbtnGenreOne.Checked),
					                                                                 new XAttribute("GenreAll", rbtnGenreAll.Checked)
					                                                                ),
					                                                    new XComment("Вид папки-Жанра"),
					                                                    new XElement("GenresType",
					                                                                 new XAttribute("GenreSchema", rbtnGenreSchema.Checked),
					                                                                 new XAttribute("GenreText", rbtnGenreText.Checked)
					                                                                )
					                                                   )
					                                      ),
					                          new XComment("Папки шаблонного тэга без данных"),
					                          new XElement("NoTags",
					                                       new XComment("Описание Книги"),
					                                       new XElement("BookInfo",
					                                                    new XElement("NoGenreGroup", txtBoxFMNoGenreGroup.Text.Trim()),
					                                                    new XElement("NoGenre", txtBoxFMNoGenre.Text.Trim()),
					                                                    new XElement("NoLang", txtBoxFMNoLang.Text.Trim()),
					                                                    new XElement("NoFirstName", txtBoxFMNoFirstName.Text.Trim()),
					                                                    new XElement("NoMiddleName", txtBoxFMNoMiddleName.Text.Trim()),
					                                                    new XElement("NoLastName", txtBoxFMNoLastName.Text.Trim()),
					                                                    new XElement("NoNickName", txtBoxFMNoNickName.Text.Trim()),
					                                                    new XElement("NoBookTitle", txtBoxFMNoBookTitle.Text.Trim()),
					                                                    new XElement("NoSequence", txtBoxFMNoSequence.Text.Trim()),
					                                                    new XElement("NoNSequence", txtBoxFMNoNSequence.Text.Trim()),
					                                                    new XElement("NoDateText", txtBoxFMNoDateText.Text.Trim()),
					                                                    new XElement("NoDateValue", txtBoxFMNoDateValue.Text.Trim())
					                                                   ),
					                                       new XComment("Издательство"),
					                                       new XElement("PublishInfo",
					                                                    new XElement("NoPublisher", txtBoxFMNoPublisher.Text.Trim()),
					                                                    new XElement("NoYear", txtBoxFMNoYear.Text.Trim()),
					                                                    new XElement("NoCity", txtBoxFMNoCity.Text.Trim())
					                                                   ),
					                                       new XComment("Данные о создателе fb2 файла"),
					                                       new XElement("FB2Info",
					                                                    new XElement("NoFB2FirstName", txtBoxFMNoFB2FirstName.Text.Trim()),
					                                                    new XElement("NoFB2MiddleName", txtBoxFMNoFB2MiddleName.Text.Trim()),
					                                                    new XElement("NoFB2LastName", txtBoxFMNoFB2LastName.Text.Trim()),
					                                                    new XElement("NoFB2NickName", txtBoxFMNoFB2NickName.Text.Trim())
					                                                   )
					                                      ),
					                          new XComment("Названия Групп Жанров"),
					                          new XElement("GenreGroups",
					                                       new XElement("sf", txtboxFMsf.Text.Trim()),
					                                       new XElement("detective", txtboxFMdetective.Text.Trim()),
					                                       new XElement("prose", txtboxFMprose.Text.Trim()),
					                                       new XElement("love", txtboxFMlove.Text.Trim()),
					                                       new XElement("adventure", txtboxFMadventure.Text.Trim()),
					                                       new XElement("children", txtboxFMchildren.Text.Trim()),
					                                       new XElement("poetry", txtboxFMpoetry.Text.Trim()),
					                                       new XElement("antique", txtboxFMantique.Text.Trim()),
					                                       new XElement("science", txtboxFMscience.Text.Trim()),
					                                       new XElement("computers", txtboxFMcomputers.Text.Trim()),
					                                       new XElement("reference", txtboxFMreference.Text.Trim()),
					                                       new XElement("nonfiction", txtboxFMnonfiction.Text.Trim()),
					                                       new XElement("religion", txtboxFMreligion.Text.Trim()),
					                                       new XElement("humor", txtboxFMhumor.Text.Trim()),
					                                       new XElement("home", txtboxFMhome.Text.Trim()),
					                                       new XElement("business", txtboxFMbusiness.Text.Trim()),
					                                       new XElement("tech", txtboxFMtech.Text.Trim()),
					                                       new XElement("military", txtboxFMmilitary.Text.Trim()),
					                                       new XElement("folklore", txtboxFMfolklore.Text.Trim()),
					                                       new XElement("other", txtboxFMother.Text.Trim())
					                                      )
					                         )
					            )
				);
				doc.Save( Settings.FileManagerSettings.FileManagerSettingsPath );
			}
			#endregion
		}
		
		// загрузка настроек из xml-файла
		private void readSettingsFromXML() {
			#region Код
			if( File.Exists( Settings.FileManagerSettings.FileManagerSettingsPath ) ) {
				XElement xmlTree = XElement.Load( Settings.FileManagerSettings.FileManagerSettingsPath );
				/* FullSorting */
				if( xmlTree.Element("FullSorting") != null ) {
					XElement xmlFullSorting = xmlTree.Element("FullSorting");
					// Папка исходных fb2-файлов
					if( xmlFullSorting.Element("SourceDir") != null )
						textBoxAddress.Text = xmlFullSorting.Element("SourceDir").Value;
					// Папка приемник fb2-файлов
					if( xmlFullSorting.Element("TargetDir") != null )
						labelTargetPath.Text = xmlFullSorting.Element("TargetDir").Value;
					// Шаблон подстановки
					if( xmlFullSorting.Element("Template") != null )
						txtBoxTemplatesFromLine.Text = xmlFullSorting.Element("Template").Value;
					// Активная Схема Жанров
					if( xmlFullSorting.Element("FB2Genres") != null ) {
						XElement xmlFB2Genres = xmlFullSorting.Element("FB2Genres");
						if( xmlFB2Genres.Attribute("Librusec") != null )
							rbtnFMFSFB2Librusec.Checked = Convert.ToBoolean( xmlFB2Genres.Attribute("Librusec").Value );
						if( xmlFB2Genres.Attribute("FB22") != null )
							rbtnFMFSFB22.Checked = Convert.ToBoolean( xmlFB2Genres.Attribute("FB22").Value );
					}
					// Настройки Полной Сортировки:
					if( xmlFullSorting.Element("Options") != null ) {
						XElement xmlOptions = xmlFullSorting.Element("Options");
						// Обрабатывать подкаталоги
						if( xmlOptions.Element("ScanSubDirs") != null )
							chBoxScanSubDir.Checked	= Convert.ToBoolean( xmlOptions.Element("ScanSubDirs").Value );
						// Архивировать в zip
						if( xmlOptions.Element("ToZip") != null )
							chBoxFSToZip.Checked = Convert.ToBoolean( xmlOptions.Element("ToZip").Value );
						// Сохранять оригиналы
						if( xmlOptions.Element("NotDelFB2Files") != null )
							chBoxFSNotDelFB2Files.Checked = Convert.ToBoolean( xmlOptions.Element("NotDelFB2Files").Value );
						// Авторазмер колонок Проводника
						if( xmlOptions.Element("StartExplorerColumnsAutoReize") != null )
							chBoxStartExplorerColumnsAutoReize.Checked	= Convert.ToBoolean( xmlOptions.Element("StartExplorerColumnsAutoReize").Value );
						// Показывать описание книг в Проводнике
						if( xmlOptions.Element("BooksTagsView") != null ) {
							this.checkBoxTagsView.Click -= new System.EventHandler(this.CheckBoxTagsViewClick);
							checkBoxTagsView.Checked = Convert.ToBoolean( xmlOptions.Element("BooksTagsView").Value );
							this.checkBoxTagsView.Click += new System.EventHandler(this.CheckBoxTagsViewClick);
						}
						// Показывать сообщение о том, что требуется много времени на генерацию метаданных"
						if( xmlOptions.Element("ViewMessageForLongTime") != null )
							m_ViewMessageForLongTime = Convert.ToBoolean( xmlOptions.Element("ViewMessageForLongTime").Value );
					}
				}
				
				/* SelectedSorting */
				if( xmlTree.Element("SelectedSorting") != null ) {
					XElement xmlSelectedSorting = xmlTree.Element("SelectedSorting");
					// Папка исходных fb2-файлов
					if( xmlSelectedSorting.Element("SourceDir") != null )
						tboxSSSourceDir.Text = xmlSelectedSorting.Element("SourceDir").Value;
					// Папка-приемник fb2-файлов
					if( xmlSelectedSorting.Element("TargetDir") != null )
						tboxSSToDir.Text = xmlSelectedSorting.Element("TargetDir").Value;
					// Шаблон подстановки
					if( xmlSelectedSorting.Element("Template") != null )
						txtBoxSSTemplatesFromLine.Text = xmlSelectedSorting.Element("Template").Value;
					// Активная Схема Жанров
					if( xmlSelectedSorting.Element("FB2Genres") != null ) {
						XElement xmlFB2Genres = xmlSelectedSorting.Element("FB2Genres");
						if( xmlFB2Genres.Attribute("Librusec") != null )
							m_SSFB2Librusec = Convert.ToBoolean( xmlFB2Genres.Attribute("Librusec").Value );
					}
					// Настройки Избранной Сортировки:
					if( xmlSelectedSorting.Element("Options") != null ) {
						XElement xmlOptions = xmlSelectedSorting.Element("Options");
						// Обрабатывать подкаталоги
						if( xmlOptions.Element("ScanSubDirs") != null )
							chBoxSSScanSubDir.Checked = Convert.ToBoolean( xmlOptions.Element("ScanSubDirs").Value );
						// Архивировать в zip
						if( xmlOptions.Element("ToZip") != null )
							chBoxSSToZip.Checked = Convert.ToBoolean( xmlOptions.Element("ToZip").Value );
						// Сохранять оригиналы
						if( xmlOptions.Element("NotDelFB2Files") != null )
							chBoxSSNotDelFB2Files.Checked = Convert.ToBoolean( xmlOptions.Element("NotDelFB2Files").Value );
					}
				}
				
				/* Log */
				if( xmlTree.Element("Log") != null ) {
					XElement xmlLog = xmlTree.Element("Log");
					// Отображать изменения хода работы
					if( xmlLog.Element("Progress") != null )
						chBoxViewProgress.Checked = Convert.ToBoolean( xmlLog.Element("Progress").Value );
				}
				
				/* Общие настройки для обоих режимов Сортировки */
				if( xmlTree.Element("CommonOptions") != null ) {
					XElement xmlCommonOptions = xmlTree.Element("CommonOptions");
					if( xmlCommonOptions.Element("General") != null ) {
						// Основные настройки
						XElement xmlGeneral = xmlCommonOptions.Element("General");
						// Регистр имени файла
						if( xmlGeneral.Element("Register") != null ) {
							XElement xmlRegister = xmlGeneral.Element("Register");
							if( xmlRegister.Attribute("AsIs") != null )
								rbtnAsIs.Checked = Convert.ToBoolean( xmlRegister.Attribute("AsIs").Value );
							if( xmlRegister.Attribute("Lower") != null )
								rbtnLower.Checked = Convert.ToBoolean( xmlRegister.Attribute("Lower").Value );
							if( xmlRegister.Attribute("Upper") != null )
								rbtnUpper.Checked = Convert.ToBoolean( xmlRegister.Attribute("Upper").Value );
							if( xmlRegister.Attribute("AsSentence") != null )
								rbtnAsSentence.Checked = Convert.ToBoolean( xmlRegister.Attribute("AsSentence").Value );
						}
						// Транслитерация имен файлов
						if( xmlGeneral.Element("Translit") != null )
							chBoxTranslit.Checked = Convert.ToBoolean( xmlGeneral.Element("Translit").Value );
						// 'Строгие' имена файлов: алфавитно-цифровые символы, а так же [](){}-_
						if( xmlGeneral.Element("Strict") != null )
							chBoxStrict.Checked = Convert.ToBoolean( xmlGeneral.Element("Strict").Value );
						// Обработка пробелов
						if( xmlGeneral.Element("Space") != null ) {
							if( xmlGeneral.Element("Space").Attribute("index") != null )
								cboxSpace.SelectedIndex = Convert.ToInt16( xmlGeneral.Element("Space").Attribute("index").Value );
						}
						// Одинаковые файлы
						if( xmlGeneral.Element("FileExistMode") != null ) {
							if( xmlGeneral.Element("FileExistMode").Attribute("index") != null )
								cboxFileExist.SelectedIndex = Convert.ToInt16( xmlGeneral.Element("FileExistMode").Attribute("index").Value );
						}
						// Сортировка файлов
						if( xmlGeneral.Element("SortType") != null ) {
							XElement xmlSortType = xmlGeneral.Element("SortType");
							if( xmlSortType.Attribute("AllFB2") != null )
								rbtnFMAllFB2.Checked = Convert.ToBoolean( xmlSortType.Attribute("AllFB2").Value );
							if( xmlSortType.Attribute("OnlyValidFB2") != null )
								rbtnFMOnlyValidFB2.Checked = Convert.ToBoolean( xmlSortType.Attribute("OnlyValidFB2").Value );
						}
						// Раскладка файлов по папкам
						if( xmlGeneral.Element("FilesToDirs") != null ) {
							XElement xmlFilesToDirs = xmlGeneral.Element("FilesToDirs");
							// По Авторам
							if( xmlFilesToDirs.Element("AuthorsToDirs") != null ) {
								XElement xmlAuthorsToDirs = xmlFilesToDirs.Element("AuthorsToDirs");
								if( xmlAuthorsToDirs.Attribute("AuthorOne") != null )
									rbtnAuthorOne.Checked = Convert.ToBoolean( xmlAuthorsToDirs.Attribute("AuthorOne").Value );
								if( xmlAuthorsToDirs.Attribute("AuthorAll") != null )
									rbtnAuthorAll.Checked = Convert.ToBoolean( xmlAuthorsToDirs.Attribute("AuthorAll").Value );
							}
							// По Жанрам
							if( xmlFilesToDirs.Element("GenresToDirs") != null ) {
								XElement xmlGenresToDirs = xmlFilesToDirs.Element("GenresToDirs");
								if( xmlGenresToDirs.Attribute("GenreOne") != null )
									rbtnGenreOne.Checked = Convert.ToBoolean( xmlGenresToDirs.Attribute("GenreOne").Value );
								if( xmlGenresToDirs.Attribute("GenreAll") != null )
									rbtnGenreAll.Checked = Convert.ToBoolean( xmlGenresToDirs.Attribute("GenreAll").Value );
							}
							// Вид папки-Жанра
							if( xmlFilesToDirs.Element("GenresType") != null ) {
								XElement xmlGenresType = xmlFilesToDirs.Element("GenresType");
								if( xmlGenresType.Attribute("GenreSchema") != null )
									rbtnGenreSchema.Checked = Convert.ToBoolean( xmlGenresType.Attribute("GenreSchema").Value );
								if( xmlGenresType.Attribute("GenreText") != null )
									rbtnGenreText.Checked = Convert.ToBoolean( xmlGenresType.Attribute("GenreText").Value );
							}
						}
					}
					/* Папки шаблонного тэга без данных */
					if( xmlCommonOptions.Element("NoTags") != null ) {
						XElement xmlNoTags = xmlCommonOptions.Element("NoTags");
						// Описание Книги
						if( xmlNoTags.Element("BookInfo") != null ) {
							XElement xmlBookInfo = xmlNoTags.Element("BookInfo");
							if( xmlBookInfo.Element("NoGenreGroup") != null )
								txtBoxFMNoGenreGroup.Text = xmlBookInfo.Element("NoGenreGroup").Value;
							if( xmlBookInfo.Element("NoGenre") != null )
								txtBoxFMNoGenre.Text = xmlBookInfo.Element("NoGenre").Value;
							if( xmlBookInfo.Element("NoLang") != null )
								txtBoxFMNoLang.Text = xmlBookInfo.Element("NoLang").Value;
							if( xmlBookInfo.Element("NoFirstName") != null )
								txtBoxFMNoFirstName.Text = xmlBookInfo.Element("NoFirstName").Value;
							if( xmlBookInfo.Element("NoMiddleName") != null )
								txtBoxFMNoMiddleName.Text = xmlBookInfo.Element("NoMiddleName").Value;
							if( xmlBookInfo.Element("NoLastName") != null )
								txtBoxFMNoLastName.Text = xmlBookInfo.Element("NoLastName").Value;
							if( xmlBookInfo.Element("NoNickName") != null )
								txtBoxFMNoNickName.Text = xmlBookInfo.Element("NoNickName").Value;
							if( xmlBookInfo.Element("NoBookTitle") != null )
								txtBoxFMNoBookTitle.Text = xmlBookInfo.Element("NoBookTitle").Value;
							if( xmlBookInfo.Element("NoSequence") != null )
								txtBoxFMNoSequence.Text = xmlBookInfo.Element("NoSequence").Value;
							if( xmlBookInfo.Element("NoNSequence") != null )
								txtBoxFMNoNSequence.Text = xmlBookInfo.Element("NoNSequence").Value;
							if( xmlBookInfo.Element("NoDateText") != null )
								txtBoxFMNoDateText.Text = xmlBookInfo.Element("NoDateText").Value;
							if( xmlBookInfo.Element("NoDateValue") != null )
								txtBoxFMNoDateValue.Text = xmlBookInfo.Element("NoDateValue").Value;
						}
						// Издательство
						if( xmlNoTags.Element("PublishInfo") != null ) {
							XElement xmlPublishInfo = xmlNoTags.Element("PublishInfo");
							if( xmlPublishInfo.Element("NoPublisher") != null )
								txtBoxFMNoPublisher.Text = xmlPublishInfo.Element("NoPublisher").Value;
							if( xmlPublishInfo.Element("NoYear") != null )
								txtBoxFMNoYear.Text = xmlPublishInfo.Element("NoYear").Value;
							if( xmlPublishInfo.Element("NoCity") != null )
								txtBoxFMNoCity.Text = xmlPublishInfo.Element("NoCity").Value;
						}
						// Данные о создателе fb2 файла
						if( xmlNoTags.Element("FB2Info") != null ) {
							XElement xmlBookInfo = xmlNoTags.Element("FB2Info");
							if( xmlBookInfo.Element("NoFB2FirstName") != null )
								txtBoxFMNoFB2FirstName.Text = xmlBookInfo.Element("NoFB2FirstName").Value;
							if( xmlBookInfo.Element("NoFB2MiddleName") != null )
								txtBoxFMNoFB2MiddleName.Text = xmlBookInfo.Element("NoFB2MiddleName").Value;
							if( xmlBookInfo.Element("NoFB2LastName") != null )
								txtBoxFMNoFB2LastName.Text = xmlBookInfo.Element("NoFB2LastName").Value;
							if( xmlBookInfo.Element("NoFB2NickName") != null )
								txtBoxFMNoFB2NickName.Text = xmlBookInfo.Element("NoFB2NickName").Value;
						}
					}
					
					// Названия Групп Жанров
					if( xmlCommonOptions.Element("GenreGroups") != null ) {
						XElement xmlGenreGroups = xmlCommonOptions.Element("GenreGroups");
						if( xmlGenreGroups.Element("sf") != null )
							txtboxFMsf.Text = xmlGenreGroups.Element("sf").Value;
						if( xmlGenreGroups.Element("detective") != null )
							txtboxFMdetective.Text = xmlGenreGroups.Element("detective").Value;
						if( xmlGenreGroups.Element("prose") != null )
							txtboxFMprose.Text = xmlGenreGroups.Element("prose").Value;
						if( xmlGenreGroups.Element("love") != null )
							txtboxFMlove.Text = xmlGenreGroups.Element("love").Value;
						if( xmlGenreGroups.Element("adventure") != null )
							txtboxFMadventure.Text = xmlGenreGroups.Element("adventure").Value;
						if( xmlGenreGroups.Element("children") != null )
							txtboxFMchildren.Text = xmlGenreGroups.Element("children").Value;
						if( xmlGenreGroups.Element("poetry") != null )
							txtboxFMpoetry.Text = xmlGenreGroups.Element("poetry").Value;
						if( xmlGenreGroups.Element("antique") != null )
							txtboxFMantique.Text = xmlGenreGroups.Element("antique").Value;
						if( xmlGenreGroups.Element("science") != null )
							txtboxFMscience.Text = xmlGenreGroups.Element("science").Value;
						if( xmlGenreGroups.Element("computers") != null )
							txtboxFMcomputers.Text = xmlGenreGroups.Element("computers").Value;
						if( xmlGenreGroups.Element("reference") != null )
							txtboxFMreference.Text = xmlGenreGroups.Element("reference").Value;
						if( xmlGenreGroups.Element("nonfiction") != null )
							txtboxFMnonfiction.Text = xmlGenreGroups.Element("nonfiction").Value;
						if( xmlGenreGroups.Element("religion") != null )
							txtboxFMreligion.Text = xmlGenreGroups.Element("religion").Value;
						if( xmlGenreGroups.Element("humor") != null )
							txtboxFMhumor.Text = xmlGenreGroups.Element("humor").Value;
						if( xmlGenreGroups.Element("home") != null )
							txtboxFMhome.Text = xmlGenreGroups.Element("home").Value;
						if( xmlGenreGroups.Element("business") != null )
							txtboxFMbusiness.Text = xmlGenreGroups.Element("business").Value;
						if( xmlGenreGroups.Element("tech") != null )
							txtboxFMtech.Text = xmlGenreGroups.Element("tech").Value;
						if( xmlGenreGroups.Element("military") != null )
							txtboxFMmilitary.Text = xmlGenreGroups.Element("military").Value;
						if( xmlGenreGroups.Element("folklore") != null )
							txtboxFMfolklore.Text = xmlGenreGroups.Element("folklore").Value;
						if( xmlGenreGroups.Element("other") != null )
							txtboxFMother.Text = xmlGenreGroups.Element("other").Value;
					}
				}
			}
			#endregion
		}
		
		//------------------------------------------------------------------------------------------
		
		// проверки на корректность шаблонных строк
		private bool IsLineTemplateCorrect( string sLineTemplate ) {
			#region Код
			// проверка "пустоту" строки с шаблонами
			if( sLineTemplate.Length == 0 ) {
				MessageBox.Show( "Строка шаблонов не может быть пустой!\nРабота прекращена.", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на наличие недопустимого условного шаблона [*GROUP*]
			if( sLineTemplate.IndexOf("[*GROUP*]")!=-1 ) {
				MessageBox.Show( "Шаблон для Группы Жанров *GROUP* не миожет буть условным [*GROUP*]!\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на корректность строки с шаблонами
			if( !templatesVerify.IsLineTemplatesCorrect( sLineTemplate ) ) {
				MessageBox.Show( "Строка содержит или недопустимые шаблоны,\nили недопустимые символы */|?<>\"&\\t\\r\\n между шаблонами!\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на четность * в строке с шаблонами
			if( !templatesVerify.IsEvenElements( sLineTemplate, '*' ) ) {
				MessageBox.Show( "Строка с шаблонами подстановки содержит нечетное число *!\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка, не стоит ли ] перед [
			if( sLineTemplate.IndexOf('[')!=-1 && sLineTemplate.IndexOf(']')!=-1 ) {
				if( sLineTemplate.IndexOf('[') > sLineTemplate.IndexOf(']') ) {
					MessageBox.Show( "В строке с шаблонами закрывающая скобка ] не может стоять перед открывающей [ !\nРабота прекращена.",
					                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return false;
				}
			}
			// проверка на соответствие [ ] в строке с шаблонами
			if( !templatesVerify.IsBracketsCorrect( sLineTemplate, '[', ']' ) ) {
				MessageBox.Show( "В строке с шаблонами переименования нет соответствия между открывающим и закрывающими скобками [ ]!\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на соответствие ( ) в строке с шаблонами
			if( !templatesVerify.IsBracketsCorrect( sLineTemplate, '(', ')' ) ) {
				MessageBox.Show( "В строке с шаблонами нет соответствия между открывающим и закрывающими скобками ( )!\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на \ в начале строки с шаблонами
			if( sLineTemplate[0]=='\\' ) {
				MessageBox.Show( "Строка с шаблонами не может начинаться с '\\'!\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на \ в конце строки с шаблонами
			if( sLineTemplate[sLineTemplate.Length-1]=='\\' ) {
				MessageBox.Show( "Строка с шаблонами не может заканчиваться на '\\' !\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка условных шаблонов на наличие в них вспом. символов без самих шаблонов
			if( !templatesVerify.IsConditionalPatternCorrect( sLineTemplate ) ) {
				MessageBox.Show( "Условные шаблоны [] в строке с шаблонами не могут содержать вспомогательных символов БЕЗ самих шаблонов!\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на множественность символа папки \ в строке с шаблонами
			if( sLineTemplate.IndexOf( "\\\\" )!=-1 ) {
				MessageBox.Show( "Строка с шаблонами не может содержать несколько идущих подряд символов папки '\\' !\nРабота прекращена.",
				                m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			return true;
			#endregion
		}
		
		private void SetTemplateInInputControl(System.Windows.Forms.TextBox textBox, string Template) {
			int CursorPosition = textBox.SelectionStart;
			int NewPosition = CursorPosition + Template.Length;
			string TextBeforeCursor = textBox.Text.Substring(0, CursorPosition);
			string TextAfterCursor = textBox.Text.Substring(CursorPosition, textBox.TextLength-CursorPosition);
			textBox.Text = TextBeforeCursor + Template + TextAfterCursor;
			textBox.Focus();
			textBox.Select(NewPosition, 0);
		}
		
		// название жанра по его коду
		private string CyrillicGenreName( bool IsFB2LibrusecGenres, string GenreCode, ref IFBGenres fb2g ) {
			if( GenreCode.IndexOf(';') != -1 ) {
				string ret = string.Empty;
				string[] sG = GenreCode.Split(';');
				foreach(string s in sG) {
					ret += fb2g.GetFBGenreName( s.Trim() ) + "; ";
					ret.Trim();
				}
				return ret.Substring( 0, ret.LastIndexOf( ";" ) ).Trim();
			}
			return fb2g.GetFBGenreName( GenreCode );
		}
		
		// ========================================================================================================================
		// 													Для Полной Сортировки
		// ========================================================================================================================
		// заполнение списка данными указанной папки
		private void FuulSortingGenerateSourceList( string dirPath ) {
			FullSortingGenerateSourceList(
				dirPath, listViewSource, true, rbtnFMFSFB2Librusec.Checked,
				checkBoxTagsView.Checked, chBoxStartExplorerColumnsAutoReize.Checked, ref m_fb2FullSortGenres
			);
		}
		
		// заполнение списка данными указанной папки (Полная Сортировка)
		private void FullSortingGenerateSourceList( string dirPath, ListView listView, bool itemChecked,
		                                           bool IsFB2LibrusecGenres, bool isTagsView, bool isColumnsAutoReize, ref IFBGenres fb2g ) {
			#region Код
			Cursor.Current = Cursors.WaitCursor;
			listView.BeginUpdate();
			listView.Items.Clear();
			string TempDir = Settings.Settings.TempDir;
			try {
				DirectoryInfo dirInfo = new DirectoryInfo(dirPath);
				ListViewItem.ListViewSubItem[] subItems;
				ListViewItem item = null;
				if (dirInfo.Exists) {
					if(dirInfo.Parent != null) {
						item = new ListViewItem("..", 3);
						item.Tag = new ListViewItemType("dUp", dirInfo.Parent.FullName);
						listView.Items.Add(item);
					}
					int nItemCount = 0;
					foreach (DirectoryInfo dir in dirInfo.GetDirectories()) {
						item = new ListViewItem( m_space + dir.Name + m_space, 0);
						item.Checked = itemChecked;
						item.Tag = new ListViewItemType("d", dir.FullName);
						if(nItemCount%2 == 0)  // четное
							item.BackColor = Color.Beige;

						listView.Items.Add(item);
						++nItemCount;
					}
					FB2BookDescription bd = null;
					Core.FilesWorker.SharpZipLibWorker sharpZipLib = new Core.FilesWorker.SharpZipLibWorker();
					foreach (FileInfo file in dirInfo.GetFiles()) {
						if(file.Extension.ToLower() == ".fb2" || file.Extension.ToLower() == ".zip") {
							item = new ListViewItem(" "+file.Name+" ", file.Extension.ToLower() == ".fb2" ? 1 : 2);
							try {
								if(file.Extension.ToLower() == ".fb2") {
									if(isTagsView) {
										bd = new FB2BookDescription( file.FullName );
										subItems = new ListViewItem.ListViewSubItem[] {
											new ListViewItem.ListViewSubItem(item, m_space + bd.TIBookTitle + m_space),
											new ListViewItem.ListViewSubItem(item, m_space + bd.TISequences + m_space),
											new ListViewItem.ListViewSubItem(item, m_space + bd.TIAuthors + m_space),
											new ListViewItem.ListViewSubItem(item, m_space + CyrillicGenreName( IsFB2LibrusecGenres, bd.TIGenres, ref fb2g ) + m_space),
											new ListViewItem.ListViewSubItem(item, m_space + bd.TILang + m_space),
											new ListViewItem.ListViewSubItem(item, m_space + bd.Encoding + m_space)
										};
									} else {
										subItems = new ListViewItem.ListViewSubItem[] {
											new ListViewItem.ListViewSubItem(item, string.Empty),
											new ListViewItem.ListViewSubItem(item, string.Empty),
											new ListViewItem.ListViewSubItem(item, string.Empty),
											new ListViewItem.ListViewSubItem(item, string.Empty),
											new ListViewItem.ListViewSubItem(item, string.Empty),
											new ListViewItem.ListViewSubItem(item, string.Empty)
										};
									}
								} else {
									// для zip-архивов
									if(isTagsView) {
										filesWorker.RemoveDir( TempDir );
										sharpZipLib.UnZipFiles( file.FullName, TempDir, 0, false, null, 4096 );
										string [] files = Directory.GetFiles( TempDir );
										bd = new FB2BookDescription( files[0] );
										subItems = new ListViewItem.ListViewSubItem[] {
											new ListViewItem.ListViewSubItem(item, m_space + bd.TIBookTitle + m_space),
											new ListViewItem.ListViewSubItem(item, m_space + bd.TISequences + m_space),
											new ListViewItem.ListViewSubItem(item, m_space + bd.TIAuthors + m_space),
											new ListViewItem.ListViewSubItem(item, m_space + CyrillicGenreName( IsFB2LibrusecGenres, bd.TIGenres, ref fb2g ) + m_space),
											new ListViewItem.ListViewSubItem(item, m_space + bd.TILang + m_space),
											new ListViewItem.ListViewSubItem(item, m_space + bd.Encoding + m_space)
										};
									} else {
										subItems = new ListViewItem.ListViewSubItem[] {
											new ListViewItem.ListViewSubItem(item, string.Empty),
											new ListViewItem.ListViewSubItem(item, string.Empty),
											new ListViewItem.ListViewSubItem(item, string.Empty),
											new ListViewItem.ListViewSubItem(item, string.Empty),
											new ListViewItem.ListViewSubItem(item, string.Empty),
											new ListViewItem.ListViewSubItem(item, string.Empty)
										};
									}
								}
								item.SubItems.AddRange(subItems);
							} catch(System.Exception) {
								item.ForeColor = Color.Blue;
							}
							
							item.Checked = itemChecked;
							item.Tag = new ListViewItemType("f", file.FullName);
							if(nItemCount%2 == 0) { // четное
								item.BackColor = Color.AliceBlue;
							}
							listView.Items.Add(item);
							++nItemCount;
						}
					}
					// авторазмер колонок Списка Проводника
					if(isColumnsAutoReize)
						m_mscLV.AutoResizeColumns(listView);
				}
				
			} catch (System.Exception) {
			} finally {
				listView.EndUpdate();
				Cursor.Current = Cursors.Default;
			}
			#endregion
		}
		
		// ========================================================================================================================
		// 													Для Полной Сортировки
		// ========================================================================================================================
		// заполнение списка критериев поиска для Избранной Сортировки
		List<selectedSortQueryCriteria> makeCriteriasList()
		{
			#region Код
			List<selectedSortQueryCriteria> list = new List<SortQueryCriteria>();
			if( lvSSData.Items.Count > 0 ) {
				string sLang, sLast, sFirst, sMiddle, sNick, sGGroup, sGenre, sSequence, sBTitle, sExactFit, sFB2Genres;
				FB2SelectedSorting fb2ss = new FB2SelectedSorting();
				SortingFB2Tags sortTags = new SortingFB2Tags( null );
				for( int i=0; i!=lvSSData.Items.Count; ++i ) {
					sLang = lvSSData.Items[i].Text;
					sGGroup = lvSSData.Items[i].SubItems[1].Text;
					sGenre = lvSSData.Items[i].SubItems[2].Text;
					sLast = lvSSData.Items[i].SubItems[3].Text;
					sFirst = lvSSData.Items[i].SubItems[4].Text;
					sMiddle = lvSSData.Items[i].SubItems[5].Text;
					sNick = lvSSData.Items[i].SubItems[6].Text;
					sSequence = lvSSData.Items[i].SubItems[7].Text;
					sBTitle = lvSSData.Items[i].SubItems[8].Text;
					sExactFit = lvSSData.Items[i].SubItems[9].Text;
					sFB2Genres = lvSSData.Items[i].SubItems[10].Text;
					// заполняем список критериев поиска для Избранной Сортировки
					SortQueryCriteria SelSortQuery = new selectedSortQueryCriteria( sLang, sGGroup, sGenre,
					                                                               sLast, sFirst, sMiddle, sNick, sSequence, sBTitle,
					                                                               sExactFit=="Да"?true:false,
					                                                               sFB2Genres == "Либрусек"?true:false);
					list.AddRange( fb2ss.MakeSelectedSortQuerysList( ref sortTags, ref SelSortQuery ) );
				}
			}
			return list;
			#endregion
		}
		#endregion
		
		#region Обработчики событий
		void BtnDefRestoreClick(object sender, EventArgs e)
		{
			switch( tcFM.SelectedIndex ) {
				case 0: // основные - для Менеджера Файлов
					DefFMGeneral();
					break;
				case 1: // название папки шаблонного тэга без данных
					switch( tcDesc.SelectedIndex ) {
						case 0: // название для данных Книги из Description когда нет данных тэга
							DefFMDirNameForTagNotData();
							break;
						case 1: // название для данных Издательства из Description когда нет данных тэга
							DefFMDirNameForPublisherTagNotData();
							break;
						case 2: // название для данных fb2-файла из Description когда нет данных тэга
							DefFMDirNameForFB2TagNotData();
							break;
					}
					break;
				case 2: // название Групп Жанров
					DefFMGenresGroups();
					break;
			}
		}
		void BtnGroupClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.Group);
		}
		
		void BtnLetterFamilyClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.LetterFamily);
		}
		
		void BtnGroupGenreClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.GroupGenre);
		}
		
		void BtnLangClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.Language);
		}
		
		void BtnFamilyClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.Family);
		}
		
		void BtnNameClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.Name);
		}
		
		void BtnPatronimicClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.Patronimic);
		}
		
		void BtnGenreClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.Genre);
		}
		
		void BtnBookClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.BookTitle);
		}
		
		void BtnSequenceClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.Series);
		}
		
		void BtnSequenceNumberClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, m_fnt.SeriesNumber);
		}
		
		void BtnDirClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, @"\");
		}
		
		void BtnLeftBracketClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, "[");
		}
		
		void BtnRightBracketClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxTemplatesFromLine, "]");
		}
		
		void BtnSSLetterFamilyClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.LetterFamily);
		}
		
		void BtnSSGroupGenreClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.GroupGenre);
		}
		
		void BtnSSGroupClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.Group);
		}
		
		void BtnSSLangClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.Language);
		}
		
		void BtnSSFamilyClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.Family);
		}
		
		void BtnSSNameClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.Name);
		}
		
		void BtnSSPatronimicClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.Patronimic);
		}
		
		void BtnSSGenreClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.Genre);
		}
		
		void BtnSSBookClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.BookTitle);
		}
		
		void BtnSSSequenceClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.Series);
		}
		
		void BtnSSSequenceNumberClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, m_fnt.SeriesNumber);
		}

		void BtnSSDirClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, @"\");
		}
		
		void BtnSSLeftBracketClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, "[");
		}
		
		void BtnSSRightBracketClick(object sender, EventArgs e)
		{
			SetTemplateInInputControl(txtBoxSSTemplatesFromLine, "]");
		}
		
		void TsmiColumnsExplorerAutoReizeClick(object sender, EventArgs e)
		{
			m_mscLV.AutoResizeColumns(listViewSource);
		}
		
		// Отображать/скрывать описание книг
		void CheckBoxTagsViewClick(object sender, EventArgs e)
		{
			#region Код
			if(checkBoxTagsView.Checked) {
				if(m_ViewMessageForLongTime) {
					string Mess = "При включении этой опции для создания списка книг с их описанием может потребоваться очень много времени!\nБольше не показывать это сообщение?";
					DialogResult result = MessageBox.Show( Mess, "Отображение описания книг", MessageBoxButtons.YesNo, MessageBoxIcon.Question );
					m_ViewMessageForLongTime = (result == DialogResult.Yes) ? false : true;
				}
			}
			saveSettingsToXml();
			
			if( listViewSource.Items.Count > 0 ) {
				Cursor.Current = Cursors.WaitCursor;
				listViewSource.BeginUpdate();
				DirectoryInfo di		= null;
				FB2BookDescription bd	= null;
				for(int i=0; i!= listViewSource.Items.Count; ++i) {
					ListViewItemType it = (ListViewItemType)listViewSource.Items[i].Tag;
					if(it.Type=="f") {
						di = new DirectoryInfo(it.Value);
						if(checkBoxTagsView.Checked) {
							// показать данные книг
							try {
								if(di.Extension.ToLower() == ".fb2") {
									// показать данные fb2 файлов
									bd = new FB2BookDescription( it.Value );
									listViewSource.Items[i].SubItems[1].Text = m_space+bd.TIBookTitle+m_space;
									listViewSource.Items[i].SubItems[2].Text = m_space+bd.TISequences+m_space;
									listViewSource.Items[i].SubItems[3].Text = m_space+bd.TIAuthors+m_space;
									listViewSource.Items[i].SubItems[4].Text = m_space + CyrillicGenreName(
										rbtnFMFSFB2Librusec.Checked, bd.TIGenres, ref m_fb2FullSortGenres
									) + m_space;
									listViewSource.Items[i].SubItems[5].Text = m_space+bd.TILang+m_space;
									listViewSource.Items[i].SubItems[6].Text = m_space+bd.Encoding+m_space;
								} else if(di.Extension.ToLower() == ".zip") {
									if( checkBoxTagsView.Checked ) {
										// показать данные архивов
										filesWorker.RemoveDir( m_TempDir );
										sharpZipLib.UnZipFiles(it.Value, m_TempDir, 0, true, null, 4096);
										string [] files = Directory.GetFiles( m_TempDir );
										bd = new FB2BookDescription( files[0] );
										listViewSource.Items[i].SubItems[1].Text = m_space+bd.TIBookTitle+m_space;
										listViewSource.Items[i].SubItems[2].Text = m_space+bd.TISequences+m_space;
										listViewSource.Items[i].SubItems[3].Text = m_space+bd.TIAuthors+m_space;
										listViewSource.Items[i].SubItems[4].Text = CyrillicGenreName(
											rbtnFMFSFB2Librusec.Checked, bd.TIGenres, ref m_fb2FullSortGenres
										) + m_space;
										listViewSource.Items[i].SubItems[5].Text = m_space+bd.TILang+m_space;
										listViewSource.Items[i].SubItems[6].Text = m_space+bd.Encoding+m_space;
									}
								}
							} catch( System.Exception ) {
								listViewSource.Items[i].ForeColor = Color.Blue;
							}
						} else {
							// скрыть данные книг
							for( int j=1; j!=listViewSource.Items[i].SubItems.Count; ++j )
								listViewSource.Items[i].SubItems[j].Text = string.Empty;
						}
					}
				}
				// очистка временной папки
				filesWorker.RemoveDir( m_TempDir );
				// авторазмер колонок Списка
				if(chBoxStartExplorerColumnsAutoReize.Checked)
					m_mscLV.AutoResizeColumns(listViewSource);

				listViewSource.EndUpdate();
				Cursor.Current = Cursors.Default;
			}
			#endregion
		}
		
		// задание папки с fb2-файлами и архивами для сканирования
		void ButtonOpenSourceDirClick(object sender, EventArgs e)
		{
			if(filesWorker.OpenDirDlg( textBoxAddress, fbdScanDir, "Укажите папку для сканирования с fb2-файлами и архивами:" ))
				ButtonGoClick(sender, e);
		}
		
		// переход на заданную папку-источник fb2-файлов
		void ButtonGoClick(object sender, EventArgs e)
		{
			string s = textBoxAddress.Text.Trim();
			if(s != string.Empty) {
				DirectoryInfo info = new DirectoryInfo(s);
				if(info.Exists)
					FuulSortingGenerateSourceList( info.FullName );
				else
					MessageBox.Show( "Не удается найти папку " + textBoxAddress.Text + ".\nПроверьте правильность пути.", "Переход по выбранному адресу", MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
		}
		
		// обработка нажатия клавиш в поле ввода пути к папке-источнику
		void TextBoxAddressKeyPress(object sender, KeyPressEventArgs e)
		{
			if ( e.KeyChar == (char)Keys.Return ) {
				// отображение папок и/или фалов в заданной папке
				ButtonGoClick( sender, e );
			} else if ( e.KeyChar == '/' || e.KeyChar == '*' || e.KeyChar == '<' || e.KeyChar == '>' || e.KeyChar == '?' || e.KeyChar == '"') {
				e.Handled = true;
			}
		}
		
		// переход в выбранную папку
		void ListViewSourceDoubleClick(object sender, EventArgs e)
		{
			if( listViewSource.Items.Count > 0 && listViewSource.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = listViewSource.SelectedItems;
				ListViewItemType it = (ListViewItemType)si[0].Tag;
				if(it.Type=="d" || it.Type=="dUp") {
					textBoxAddress.Text = it.Value;
					FuulSortingGenerateSourceList( it.Value );
				}
			}
		}
		
		// обработка нажатия клавиш на списке папок и файлов
		void ListViewSourceKeyPress(object sender, KeyPressEventArgs e)
		{
			if ( e.KeyChar == (char)Keys.Return ) {
				// переход в выбранную папку
				ListViewSourceDoubleClick(sender, e);
			} else if ( e.KeyChar == (char)Keys.Back ) {
				// переход на каталог выше
				ListViewItemType it = (ListViewItemType)listViewSource.Items[0].Tag;
				textBoxAddress.Text = it.Value;
				FuulSortingGenerateSourceList( it.Value );
			}
		}
		
		// Пометить все файлы и папки
		void TsmiCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			m_mscLV.CheckAllListViewItems( listViewSource, true );
			if(listViewSource.Items.Count > 0) {
				listViewSource.Items[0].Checked = false;
			}
			ConnectListsEventHandlers( true );
		}
		
		// Снять отметки со всех файлов и папок
		void TsmiUnCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			m_mscLV.UnCheckAllListViewItems( listViewSource.CheckedItems );
			ConnectListsEventHandlers( true );
		}
		
		// Пометить все файлы
		void TsmiFilesCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			m_mscLV.CheckAllFiles(listViewSource, true);
			ConnectListsEventHandlers( true );
		}
		
		// Снять пометки со всех файлов
		void TsmiFilesUnCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			m_mscLV.UnCheckAllFiles(listViewSource);
			ConnectListsEventHandlers( true );
		}
		
		// Пометить все папки
		void TsmiDirCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			m_mscLV.CheckAllDirs(listViewSource, true);
			ConnectListsEventHandlers( true );
		}
		
		// Снять пометки со всех папок
		void TsmiDirUnCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			m_mscLV.UnCheckAllDirs(listViewSource);
			ConnectListsEventHandlers( true );
		}
		
		// Пометить все fb2 файлы
		void TsmiFB2CheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			m_mscLV.CheckTypeAllFiles(listViewSource, "fb2", true);
			ConnectListsEventHandlers( true );
		}
		
		// Снять пометки со всех fb2 файлов
		void TsmiFB2UnCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			m_mscLV.UnCheckTypeAllFiles(listViewSource, "fb2");
			ConnectListsEventHandlers( true );
		}
		
		// Пометить все zip файлы
		void TsmiZipCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			m_mscLV.CheckTypeAllFiles(listViewSource, "zip", true);
			ConnectListsEventHandlers( true );
		}
		
		// Снять пометки со всех zip файлов
		void TsmiZipUnCheckedAllClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			m_mscLV.UnCheckTypeAllFiles(listViewSource, "zip");
			ConnectListsEventHandlers( true );
		}
		
		// Пометить всё выделенное
		void TsmiCheckedAllSelectedClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			m_mscLV.ChekAllSelectedItems(listViewSource, true);
			ConnectListsEventHandlers( true );
			listViewSource.Focus();
		}
		
		// Снять пометки со всего выделенного
		void TsmiUnCheckedAllSelectedClick(object sender, EventArgs e)
		{
			ConnectListsEventHandlers( false );
			m_mscLV.ChekAllSelectedItems(listViewSource, false);
			ConnectListsEventHandlers( true );
			listViewSource.Focus();
		}
		
		void ListViewSourceItemCheck(object sender, ItemCheckEventArgs e)
		{
			if( listViewSource.Items.Count > 0 && listViewSource.SelectedItems.Count != 0 ) {
				// при двойном клике на папке ".." пометку не ставим
				if(e.Index == 0) // ".."
					e.NewValue = CheckState.Unchecked;
			}
		}
		
		// пометка/снятие пометки по check на 0-й item - папка ".."
		void ListViewSourceItemChecked(object sender, ItemCheckedEventArgs e)
		{
			if( listViewSource.Items.Count > 0 ) {
				ListViewItemType it = (ListViewItemType)e.Item.Tag;
				if(it.Type=="dUp") {
					ConnectListsEventHandlers( false );
					if(e.Item.Checked)
						m_mscLV.CheckAllListViewItems( listViewSource, true );
					else
						m_mscLV.UnCheckAllListViewItems( listViewSource.CheckedItems );;
					ConnectListsEventHandlers( true );
				}
			}
		}
		
		// =====================================================================================================================
		// 													Полная сортировка
		// =====================================================================================================================
		void ButtonSortFilesToClick(object sender, EventArgs e)
		{
			// отображение списка файлов в указанной папке
			ButtonGoClick(sender, e);
			
			// обработка заданных каталога
			m_bFullSort = true;
			m_sMessTitle = "SharpFBTools - Полная Сортировка";
			// загрузка всех настроек Сортировки
			m_sortOptions = new SortingOptions( true, null );
			// проверка на корректность данных папок источника и приемника файлов
			if( !IsSourceDirDataCorrect( m_sortOptions.SourceDir, m_sortOptions.TargetDir ) )
				return;

			// приведение к одному виду шаблонов
			m_sortOptions.Template = templatesVerify.ToOneTemplateType( @m_sortOptions.Template );
			// проверки на корректность шаблонных строк
			if( !IsLineTemplateCorrect( m_sortOptions.Template ) )
				return;
			
			// инициализация контролов
			Init();
			SortingForm sortingForm = new SortingForm( ref m_sortOptions, listViewSource, lvFilesCount );
			sortingForm.ShowDialog();
			Core.Misc.EndWorkMode EndWorkMode = sortingForm.EndMode;
			sortingForm.Dispose();
			
			MessageBox.Show( EndWorkMode.Message, "SharpFBTools - Полная Сортировка", MessageBoxButtons.OK, MessageBoxIcon.Information );
			
			if( !m_sortOptions.NotDelOriginalFiles )
				FuulSortingGenerateSourceList( m_sortOptions.SourceDir );
			m_sortOptions = null;
		}
		
		// Возобновление Полной сортировки из xml
		void ButtonFullSortRenewClick(object sender, EventArgs e)
		{
			// загрузка данных из xml
			sfdLoadList.InitialDirectory = Settings.Settings.ProgDir;
			sfdLoadList.Title		= "Укажите файл для возобновления Полной Сортировки книг:";
			sfdLoadList.Filter		= "SharpFBTools Файлы хода работы Сортировщика (*.fullsort_break)|*.fullsort_break";
			sfdLoadList.FileName	= string.Empty;
			DialogResult result		= sfdLoadList.ShowDialog();

			if( result != DialogResult.OK )
				return;
			
			// инициализация контролов
			Init();
			m_sortOptions = new SortingOptions( true, sfdLoadList.FileName );
			
			// устанавливаем данные настройки поиска-сравнения
			textBoxAddress.Text = m_sortOptions.SourceDir;
			txtBoxTemplatesFromLine.Text = m_sortOptions.Template;
			chBoxScanSubDir.Checked = m_sortOptions.ScanSubDirs;
			chBoxFSToZip.Checked = m_sortOptions.ToZip;
			chBoxFSNotDelFB2Files.Checked = m_sortOptions.NotDelOriginalFiles;
			List<selectedSortQueryCriteria> cl = m_sortOptions.getCriterias();
			rbtnFMFSFB2Librusec.Checked = cl[0].GenresFB2Librusec;
			SortingForm sortingForm = new SortingForm( ref m_sortOptions, listViewSource, lvFilesCount );
			sortingForm.ShowDialog();
			//TODO Доделать вывод инфы и в контролы????
			Core.Misc.EndWorkMode EndWorkMode = sortingForm.EndMode;
			sortingForm.Dispose();
			MessageBox.Show( EndWorkMode.Message, "SharpFBTools - Полная Сортировка", MessageBoxButtons.OK, MessageBoxIcon.Information );
			
			if( !m_sortOptions.NotDelOriginalFiles )
				FuulSortingGenerateSourceList( m_sortOptions.SourceDir );
			m_sortOptions = null;
		}
		
		void ChBoxFSToZipClick(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void ChBoxFSNotDelFB2FilesClick(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void RbtnFMFSFB2LibrusecClick(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void RbtnFMFSFB22Click(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void ChBoxSSToZipClick(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void ChBoxSSNotDelFB2FilesClick(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void ChBoxScanSubDirClick(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}

		void ChBoxSSScanSubDirClick(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void TextBoxAddressTextChanged(object sender, EventArgs e)
		{
			if( textBoxAddress.Text.Substring(textBoxAddress.Text.Length-2, 2) == "\\\\" ) {
				textBoxAddress.Text = textBoxAddress.Text.Remove(textBoxAddress.Text.Length-1, 1);
				textBoxAddress.SelectionStart = textBoxAddress.Text.Length;
			} else if( textBoxAddress.Text.Substring(textBoxAddress.Text.Length-2, 2) == "\\." ) {
				textBoxAddress.Text = textBoxAddress.Text.Remove(textBoxAddress.Text.Length-1, 1);
				textBoxAddress.SelectionStart = textBoxAddress.Text.Length;
			} else if( textBoxAddress.Text.Substring( textBoxAddress.Text.Length-3, 3) == "\\.." ) {
				textBoxAddress.Text = textBoxAddress.Text.Remove( textBoxAddress.Text.Length-1, 1 );
				textBoxAddress.SelectionStart = textBoxAddress.Text.Length;
			}
			
			if( textBoxAddress.Text[textBoxAddress.Text.Length-1] == '\\' )
				labelTargetPath.Text = textBoxAddress.Text.Remove(textBoxAddress.Text.Length-1, 1);
			else
				labelTargetPath.Text = textBoxAddress.Text;
			labelTargetPath.Text += " - OUT";
			saveSettingsToXml();
		}
		
		void TxtBoxTemplatesFromLineTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void TboxSSSourceDirTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void TboxSSToDirTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void TxtBoxSSTemplatesFromLineTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		// запуск диалога Вставки готовых шаблонов
		void BtnInsertTemplatesClick(object sender, EventArgs e)
		{
			Core.BookSorting.BasicTemplates btfrm = new Core.BookSorting.BasicTemplates();
			btfrm.ShowDialog();
			if( btfrm.GetTemplateLine()!=null )
				txtBoxTemplatesFromLine.Text = btfrm.GetTemplateLine();

			btfrm.Dispose();
		}
		
		// запуск диалога Сбора данных для Избранной Сортировки
		void BtnSSGetDataClick(object sender, EventArgs e)
		{
			#region Код
			SelectedSortDataForm ssdfrm = new SelectedSortDataForm( m_SSFB2Librusec );
			// если в основном списке критериев поиска уже есть записи, то копируем их в форму сбора данных
			if( lvSSData.Items.Count > 0 ) {
				for( int i=0; i!=lvSSData.Items.Count; ++i ) {
					ListViewItem lvi = new ListViewItem( lvSSData.Items[i].Text );
					lvi.SubItems.Add( lvSSData.Items[i].SubItems[1].Text );
					lvi.SubItems.Add( lvSSData.Items[i].SubItems[2].Text );
					lvi.SubItems.Add( lvSSData.Items[i].SubItems[3].Text );
					lvi.SubItems.Add( lvSSData.Items[i].SubItems[4].Text );
					lvi.SubItems.Add( lvSSData.Items[i].SubItems[5].Text );
					lvi.SubItems.Add( lvSSData.Items[i].SubItems[6].Text );
					lvi.SubItems.Add( lvSSData.Items[i].SubItems[7].Text );
					lvi.SubItems.Add( lvSSData.Items[i].SubItems[8].Text );
					lvi.SubItems.Add( lvSSData.Items[i].SubItems[9].Text );
					lvi.SubItems.Add( lvSSData.Items[i].SubItems[10].Text );
					ssdfrm.lvSSData.Items.Add( lvi );
				}
				ssdfrm.lblCount.Text = Convert.ToString( lvSSData.Items.Count );
			}
			
			ssdfrm.ShowDialog();
			
			if( ssdfrm.IsOKClicked() ) {
				/* сохраняем настройки */
				m_SSFB2Librusec = ssdfrm.isFB2Librusec();
				saveSettingsToXml();
				/* обрабатываем собранные данные */
				if( ssdfrm.lvSSData.Items.Count > 0 ) {
					// удаляем записи в списке, если они есть
					lvSSData.Items.Clear();
					string sLang, sLast, sFirst, sMiddle, sNick, sGGroup, sGenre, sSequence, sBTitle, sExactFit, sFB2Genres;

					for( int i=0; i!=ssdfrm.lvSSData.Items.Count; ++i ) {
						sLang = ssdfrm.lvSSData.Items[i].Text;
						sGGroup = ssdfrm.lvSSData.Items[i].SubItems[1].Text;
						sGenre = ssdfrm.lvSSData.Items[i].SubItems[2].Text;
						sLast = ssdfrm.lvSSData.Items[i].SubItems[3].Text;
						sFirst = ssdfrm.lvSSData.Items[i].SubItems[4].Text;
						sMiddle = ssdfrm.lvSSData.Items[i].SubItems[5].Text;
						sNick = ssdfrm.lvSSData.Items[i].SubItems[6].Text;
						sSequence = ssdfrm.lvSSData.Items[i].SubItems[7].Text;
						sBTitle = ssdfrm.lvSSData.Items[i].SubItems[8].Text;
						sExactFit = ssdfrm.lvSSData.Items[i].SubItems[9].Text;
						sFB2Genres = ssdfrm.lvSSData.Items[i].SubItems[10].Text;
						ListViewItem lvi = new ListViewItem( sLang );
						lvi.SubItems.Add( sGGroup );
						lvi.SubItems.Add( sGenre );
						lvi.SubItems.Add( sLast );
						lvi.SubItems.Add( sFirst );
						lvi.SubItems.Add( sMiddle );
						lvi.SubItems.Add( sNick );
						lvi.SubItems.Add( sSequence );
						lvi.SubItems.Add( sBTitle );
						lvi.SubItems.Add( sExactFit );
						lvi.SubItems.Add( sFB2Genres );
						// добавление записи в список
						lvSSData.Items.Add( lvi );
					}
				}
			}
			
			ssdfrm.Dispose();
			#endregion
		}
		
		// запуск диалога Вставки готовых шаблонов
		void BtnSSInsertTemplatesClick(object sender, EventArgs e)
		{
			Core.BookSorting.BasicTemplates btfrm = new Core.BookSorting.BasicTemplates();
			btfrm.ShowDialog();
			if( btfrm.GetTemplateLine()!= null )
				txtBoxSSTemplatesFromLine.Text = btfrm.GetTemplateLine();

			btfrm.Dispose();
		}
		
		// задание папки с fb2-файлами и архивами для сканирования (Избранная Сортировка)
		void BtnSSOpenDirClick(object sender, EventArgs e)
		{
			filesWorker.OpenDirDlg( tboxSSSourceDir, fbdScanDir, "Укажите папку для сканирования с fb2-файлами и архивами (Избранная Сортировка):" );
		}
		
		// задание папки-приемника для размешения отсортированных файлов (Избранная Сортировка)
		void BtnSSTargetDirClick(object sender, EventArgs e)
		{
			filesWorker.OpenDirDlg( tboxSSToDir, fbdScanDir, "Укажите папку-приемник для размешения отсортированных файлов (Избранная Сортировка):" );
		}
		
		// =====================================================================================================================
		// 													Избранная Сортировка
		// =====================================================================================================================
		void ButtonSSortFilesToClick(object sender, EventArgs e)
		{
			m_bFullSort = false;
			m_sMessTitle = "SharpFBTools - Избранная Сортировка";
			// загрузка всех настроек Сортировки
			m_sortOptions = new SortingOptions( false, null );
			//задаем критерии Избранной Сортировки в класс m_sortOptions
			m_sortOptions.setCriterias( makeCriteriasList() );
			
			// проверка на корректность данных папок источника и приемника файлов
			if( !IsFoldersDataCorrect( m_sortOptions.SourceDir, m_sortOptions.TargetDir ) ) {
				return;
			}
			// проверка на наличие критериев поиска для Избранной Сортировки
			if( lvSSData.Items.Count == 0 ) {
				MessageBox.Show( "Задайте хоть один критерий для Избранной Сортировки (кнопка \"Собрать данные для Избранной Сортировки\")!", m_sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				btnSSGetData.Focus();
				return;
			}

			// приведение к одному виду шаблонов
			m_sortOptions.Template = templatesVerify.ToOneTemplateType( @m_sortOptions.Template );
			// проверки на корректность шаблонных строк
			if( !IsLineTemplateCorrect( m_sortOptions.Template ) )
				return;
			
			// инициализация контролов
			Init();
			Core.BookSorting.SortingForm sortingForm = new Core.BookSorting.SortingForm( ref m_sortOptions, lvFilesCount );
			sortingForm.ShowDialog();
			Core.Misc.EndWorkMode EndWorkMode = sortingForm.EndMode;
			sortingForm.Dispose();
			m_sortOptions = null;
			MessageBox.Show( EndWorkMode.Message, "SharpFBTools - Избранная Сортировка", MessageBoxButtons.OK, MessageBoxIcon.Information );
		}
		
		// Возобновление Избранной сортировки из xml
		void ButtonSSortRenewClick(object sender, EventArgs e)
		{
			// загрузка данных из xml
			sfdLoadList.InitialDirectory = Settings.Settings.ProgDir;
			sfdLoadList.Title		= "Укажите файл для возобновления Избранной Сортировки книг:";
			sfdLoadList.Filter		= "SharpFBTools Файлы хода работы Сортировщика (*.selsort_break)|*.selsort_break";
			sfdLoadList.FileName	= string.Empty;
			DialogResult result		= sfdLoadList.ShowDialog();

			if( result != DialogResult.OK )
				return;
			
			// инициализация контролов
			Init();
			m_sortOptions = new SortingOptions( false, sfdLoadList.FileName );
			
			// устанавливаем данные настройки поиска-сравнения
			tboxSSSourceDir.Text = m_sortOptions.SourceDir;
			tboxSSToDir.Text = m_sortOptions.TargetDir;
			txtBoxSSTemplatesFromLine.Text = m_sortOptions.Template;
			chBoxSSScanSubDir.Checked = m_sortOptions.ScanSubDirs;
			chBoxSSToZip.Checked = m_sortOptions.ToZip;
			chBoxSSNotDelFB2Files.Checked = m_sortOptions.NotDelOriginalFiles;
			
			// загрузка критериев для Избранной сортировки
			// удаляем записи в списке, если они есть
			lvSSData.Items.Clear();
			List<selectedSortQueryCriteria> lSSQCList = m_sortOptions.getCriterias();
			foreach( selectedSortQueryCriteria c in lSSQCList ) {
				ListViewItem lvi = new ListViewItem( c.Lang );
				lvi.SubItems.Add( c.GenresGroup );
				lvi.SubItems.Add( c.Genre );
				lvi.SubItems.Add( c.LastName );
				lvi.SubItems.Add( c.FirstName );
				lvi.SubItems.Add( c.MiddleName );
				lvi.SubItems.Add( c.NickName );
				lvi.SubItems.Add( c.Sequence );
				lvi.SubItems.Add( c.BookTitle );
				lvi.SubItems.Add( c.ExactFit ? "Да" : "Нет" );
				lvi.SubItems.Add( c.GenresFB2Librusec ? "Либрусек" : "FB2.2" );
				// добавление записи в список
				lvSSData.Items.Add( lvi );
			}

			SortingForm sortingForm = new SortingForm( ref m_sortOptions, lvFilesCount );
			sortingForm.ShowDialog();
			//TODO Доделать вывод инфы и в контролы????
			Core.Misc.EndWorkMode EndWorkMode = sortingForm.EndMode;
			sortingForm.Dispose();
			m_sortOptions = null;
			MessageBox.Show( EndWorkMode.Message, "SharpFBTools - Избранная Сортировка", MessageBoxButtons.OK, MessageBoxIcon.Information );
		}
		
		// сохранить список критериев Избранной Сортировки в файл
		void BtnSSDataListSaveClick(object sender, EventArgs e)
		{
			#region Код
			string sMessTitle = "SharpFBTools - Избранная Сортировка";
			if( lvSSData.Items.Count == 0 ) {
				MessageBox.Show( "Список данных для Избранной Сортировки пуст.\nЗадайте хоть один критерий Сортировки (кнопка 'Собрать данные для Избранной Сортировки').",
				                sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			sfdSaveXMLFile.Filter = "SharpFBTools файлы (*.sort_criterias)|*.sort_criterias";;
			sfdSaveXMLFile.FileName = string.Empty;
			DialogResult result = sfdSaveXMLFile.ShowDialog();
			if( result == DialogResult.OK ) {
				XDocument doc = new XDocument(
					new XDeclaration("1.0", "utf-8", "yes"),
					new XComment("Файл критериев сортировки fb2-книг"),
					new XElement("Criterias", new XAttribute("count", lvSSData.Items.Count.ToString()), new XAttribute("type", "sort_criterias"),
					             new XComment("Критерии сортировки")
					            )
				);
				if ( lvSSData.Items.Count > 0 ) {
					int critNumber = 0;
					foreach (ListViewItem item in lvSSData.Items ) {
						doc.Root.Add( new XElement("Criteria", new XAttribute("number", critNumber++),
						                           new XElement("Lang", item.SubItems[0].Text),
						                           new XElement("GGroup", item.SubItems[1].Text),
						                           new XElement("Genre", item.SubItems[2].Text),
						                           new XElement("LastName", item.SubItems[3].Text),
						                           new XElement("FirstName", item.SubItems[4].Text),
						                           new XElement("MiddleName", item.SubItems[5].Text),
						                           new XElement("NickName", item.SubItems[6].Text),
						                           new XElement("Sequence", item.SubItems[7].Text),
						                           new XElement("BookTitle", item.SubItems[8].Text),
						                           new XElement("ExactFit", item.SubItems[9].Text),
						                           new XElement("FB2Genres", item.SubItems[10].Text)
						                          )
						            );
					}
					doc.Save( sfdSaveXMLFile.FileName ) ;
					MessageBox.Show( "Список данных для Избранной Сортировки сохранен в файл!", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				}
			}
			#endregion
		}
		
		// загрузить список критериев Избранной Сортировки из файла
		void BtnSSDataListLoadClick(object sender, EventArgs e)
		{
			#region Код
			sfdOpenXMLFile.Filter = "SharpFBTools файлы (*.sort_criterias)|*.sort_criterias";
			sfdOpenXMLFile.FileName = string.Empty;
			DialogResult result = sfdOpenXMLFile.ShowDialog();
			if( result == DialogResult.OK ) {
				lvSSData.Items.Clear();
				XElement xmlTree = XElement.Load( sfdOpenXMLFile.FileName );
				ListViewItem lvi = null;
				IEnumerable<XElement> criterias = xmlTree.Elements("Criteria");
				foreach( XElement crit in criterias ) {
					lvi = new ListViewItem( crit.Element("Lang").Value );
					lvi.SubItems.Add( crit.Element("GGroup").Value );
					lvi.SubItems.Add( crit.Element("Genre").Value );
					lvi.SubItems.Add( crit.Element("LastName").Value );
					lvi.SubItems.Add( crit.Element("FirstName").Value );
					lvi.SubItems.Add( crit.Element("MiddleName").Value );
					lvi.SubItems.Add( crit.Element("NickName").Value );
					lvi.SubItems.Add( crit.Element("Sequence").Value );
					lvi.SubItems.Add( crit.Element("BookTitle").Value );
					lvi.SubItems.Add( crit.Element("ExactFit").Value );
					lvi.SubItems.Add( crit.Element("FB2Genres").Value );
					lvSSData.Items.Add( lvi );
				}
			}
			#endregion
		}
		
		void ChBoxStartExplorerColumnsAutoReizeCheckedChanged(object sender, EventArgs e)
		{
			if(chBoxStartExplorerColumnsAutoReize.Checked)
				m_mscLV.AutoResizeColumns(listViewSource);
			saveSettingsToXml();
		}
		
		void ChBoxViewProgressClick(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		// =====================================================================================================================
		//													Настройки
		// =====================================================================================================================
		void RbtnAsIsClick(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void ChBoxTranslitClick(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void ChBoxStrictClick(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void CboxSpaceSelectedIndexChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void CboxFileExistSelectedIndexChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void RbtnFMAllFB2Click(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void RbtnAuthorOneClick(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void RbtnGenreOneClick(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void RbtnGenreSchemaClick(object sender, EventArgs e)
		{
			saveSettingsToXml();
		}
		
		void TxtBoxFMNoGenreGroupTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
			if( ((TextBox)sender).TextLength < 1 )
				((TextBox)sender).BackColor = Color.LightPink;
			else
				((TextBox)sender).BackColor = Color.White;
		}
		
		void TxtBoxFMNoYearTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
			if( ((TextBox)sender).TextLength < 1 )
				((TextBox)sender).BackColor = Color.LightPink;
			else
				((TextBox)sender).BackColor = Color.White;
		}
		
		void TxtBoxFMNoFB2FirstNameTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
			if( ((TextBox)sender).TextLength < 1 )
				((TextBox)sender).BackColor = Color.LightPink;
			else
				((TextBox)sender).BackColor = Color.White;
		}
		
		void TxtboxFMsfTextChanged(object sender, EventArgs e)
		{
			saveSettingsToXml();
			if( ((TextBox)sender).TextLength < 1 )
				((TextBox)sender).BackColor = Color.LightPink;
			else
				((TextBox)sender).BackColor = Color.White;
		}
		#endregion
	}
}
