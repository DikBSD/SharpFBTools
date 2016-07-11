/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 27.05.2014
 * Time: 7:47
 * 
 * License: GPL 2.1
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

//using System.Windows.Forms;

using Settings;
using Core.Common;

using SelectedSortQueryCriteria	= Core.FileManager.SortQueryCriteria;

// enums
using SortingTypeEnum  = Core.Common.Enums.SortingTypeEnum;

namespace Core.FileManager
{
	/// <summary>
	/// SortingOptions: индивидуальные настройки обоих Сортировщиков, в зависимости от режима (непрерывная сортировка или возобновление сортировки)
	/// </summary>
	public class SortingOptions : ISortOptions, IFB2TagsNotExsist, /*IGenresGroup,*/ IProblemDirs
	{
		#region закрытые данные класса
		private readonly XElement m_xmlTree		= null; // дерево настроек из xml файла m_FromXmlFile
		private readonly string m_FromXmlFile	= null; // xml файл с сохраненными настройками сортировки при прерывании работы (null - сортировка сначала)
		private SortingTypeEnum _sortingTypeEnum = SortingTypeEnum.FullSort; // Тип Сортировки: Полная или Избранная сортировка
		private List<SelectedSortQueryCriteria> m_lSSQCList = new List<SelectedSortQueryCriteria>(); // список критерие Избранной Сортировки
		
		#region Папки для "проблемных" файлов
		// папки для "проблемных" файлов
		private string m_sNotReadFB2Dir		= FileManagerSettings.DefNotReadDir;
		private string m_sFileLongPathDir	= FileManagerSettings.DefLongPathDir;
		private string m_sNotValidFB2Dir	= FileManagerSettings.DefNotValidDir;
		private string m_sNotOpenArchDir	= FileManagerSettings.DefArchNotOpenDir;
		#endregion
		
		#region Обработка файлов
		// папки: исходная и приемник
		private string m_SourceDir = string.Empty;
		private string m_TargetDir = string.Empty;
		// Настройки сортировки
		private bool m_ScanSubDirs			= true;
		private bool m_ToZip				= false;
		private bool m_NotDelOriginalFiles	= true;
		#endregion
		
		#region Обработка имени файлов
		// Шаблоны подстановк
		private string m_Template = string.Empty;
		// основные настройки
		private bool m_RegisterAsIs				= true;
		private bool m_RegisterLower			= false;
		private bool m_RegisterUpper			= false;
		private bool m_RegisterAsSentence		= false;
		private bool m_Translit					= false;
		private bool m_Strict					= false;
		private int  m_Space					= 0;
		private int	 m_FileExistMode			= 1;
		#endregion
		
		#region Сортировка файлов
		// Сортировка файлов
		private bool m_SortTypeAllFB2			= true;
		private bool m_SortTypeOnlyValidFB2		= false;
		#endregion
		
		#region Раскладка файлов по папкам
		// Раскладка файлов по папкам
		private bool m_AuthorsToDirsAuthorOne	= true;
		private bool m_AuthorsToDirsAuthorAll	= false;
		private bool m_GenresToDirsGenreOne		= true;
		private bool m_GenresToDirsGenreAll		= false;
		private bool m_GenresTypeGenreSchema	= false;
		private bool m_GenresTypeGenreText		= true;
		#endregion
		
		#region Названия папок для шаблонных тэгов, которые не имеют данных
		#region Описание Книги
		// названия папки для тэга, у которого нет данных
		private string m_BookInfoNoGenreGroup	= string.Empty;
		private string m_BookInfoNoGenre		= string.Empty;
		private string m_BookInfoNoLang			= string.Empty;
		private string m_BookInfoNoFirstName	= string.Empty;
		private string m_BookInfoNoMiddleName	= string.Empty;
		private string m_BookInfoNoLastName		= string.Empty;
		private string m_BookInfoNoNickName		= string.Empty;
		private string m_BookInfoNoBookTitle	= string.Empty;
		private string m_BookInfoNoSequence		= string.Empty;
		private string m_BookInfoNoNSequence	= string.Empty;
		private string m_BookInfoNoDateText		= string.Empty;
		private string m_BookInfoNoDateValue	= string.Empty;
		#endregion
		
		#region Издательство
		// Издательство
		private string m_PublishInfoNoPublisher	= string.Empty;
		private string m_PublishInfoNoYear		= string.Empty;
		private string m_PublishInfoNoCity		= string.Empty;
		#endregion
		
		#region Данные о создателе fb2 файла
		// Данные о создателе fb2 файла
		private string m_FB2InfoNoFB2FirstName	= string.Empty;
		private string m_FB2InfoNoFB2MiddleName	= string.Empty;
		private string m_FB2InfoNoFB2LastName	= string.Empty;
		private string m_FB2InfoNoFB2NickName	= string.Empty;
		#endregion
		#endregion

		#endregion
		
		/// <summary>
		/// Конструктор класса SortingOptions: индивидуальные настройки обоих Сортировщиков, в зависимости от режима (непрерывная сортировка или возобновление сортировки)
		/// </summary>
		/// <param name="sortingTypeEnum">Тип сортировки: Полная Сортировка или Избранная Сортировка</param>
		/// <param name="FromXmlFile">null - Непрерывная Сортировка; Путь - Возобновление Сортировки из xml файла</param>
		public SortingOptions( SortingTypeEnum sortingTypeEnum, string FromXmlFile )
		{
			m_FromXmlFile	 = FromXmlFile;
			_sortingTypeEnum = sortingTypeEnum;
			
			if( m_FromXmlFile == null ) {
				// Непрерывная Сортировка
				loadSettingsForNotBreakSort( _sortingTypeEnum );
			} else {
				// Возобновление Сортировки
				if( File.Exists( m_FromXmlFile ) ) {
					m_xmlTree = XElement.Load( m_FromXmlFile );
					if( m_xmlTree != null ) {
						try {
							// загрузка данных из xml-файла восстановления сортировки
							loadSettingsForReNewSort();
							if( _sortingTypeEnum == SortingTypeEnum.SelectedSort ) // загрузка критериев Избранной Сортировки при возобновлении сортировки
								loadCriteriasForSelReNewSort();
						} catch {
							return;
						}
					}
				}
			}
		}
		
		#region Открытые свойства класса
		// =====================================================================================================
		// 									папки для "проблемных" файлов
		// =====================================================================================================
		#region папки для "проблемных" файлов
		public virtual string NotReadFB2Dir {
			get { return m_sNotReadFB2Dir; }
			set { m_sNotReadFB2Dir = value; }
		}
		public virtual string FileLongPathDir {
			get { return m_sFileLongPathDir; }
			set { m_sFileLongPathDir = value; }
		}
		public virtual string NotValidFB2Dir {
			get { return m_sNotValidFB2Dir; }
			set { m_sNotValidFB2Dir = value; }
		}
		public virtual string NotOpenArchDir {
			get { return m_sNotOpenArchDir; }
			set { m_sNotOpenArchDir = value; }
		}
		#endregion

		// =====================================================================================================
		// 										Обработка файлов
		// =====================================================================================================
		#region Обработка файлов
		// папка исходная
		public string SourceDir {
			get { return m_SourceDir; }
			set { m_SourceDir = value; }
		}
		// папка приемник
		public string TargetDir {
			get { return m_TargetDir; }
			set { m_TargetDir = value; }
		}
		// Обрабатывать подкаталоги
		public bool ScanSubDirs {
			get { return m_ScanSubDirs; }
			set { m_ScanSubDirs = value; }
		}
		// Архивировать в zip
		public bool ToZip {
			get { return m_ToZip; }
			set { m_ToZip = value; }
		}
		// Сохранять оригиналы
		public bool NotDelOriginalFiles {
			get { return m_NotDelOriginalFiles; }
			set { m_NotDelOriginalFiles = value; }
		}
		#endregion
		
		// =====================================================================================================
		//											Общие настройки Сортировщиков
		// =====================================================================================================
		#region Общие настройки Сортировщиков
		#region Обработка имени файлов
		// Шаблоны подстановк
		public string Template {
			get { return m_Template; }
			set { m_Template = value; }
		}
		
		public virtual bool RegisterAsIs {
			get { return m_RegisterAsIs; }
			set { m_RegisterAsIs = value; }
		}
		public virtual bool RegisterLower {
			get { return m_RegisterLower; }
			set { m_RegisterLower = value; }
		}
		public virtual bool RegisterUpper {
			get { return m_RegisterUpper; }
			set { m_RegisterUpper = value; }
		}
		public virtual bool RegisterAsSentence {
			get { return m_RegisterAsSentence; }
			set { m_RegisterAsSentence = value; }
		}
		public virtual bool Translit {
			get { return m_Translit; }
			set { m_Translit = value; }
		}
		public virtual bool Strict {
			get { return m_Strict; }
			set { m_Strict = value; }
		}
		public virtual int Space {
			get { return m_Space; }
			set { m_Space = value; }
		}
		public virtual int FileExistMode {
			get { return m_FileExistMode; }
			set { m_FileExistMode = value; }
		}
		#endregion
		
		#region Сортировка файлов
		public virtual bool SortTypeAllFB2 {
			get { return m_SortTypeAllFB2; }
			set { m_SortTypeAllFB2 = value; }
		}
		public virtual bool SortTypeOnlyValidFB2 {
			get { return m_SortTypeOnlyValidFB2; }
			set { m_SortTypeOnlyValidFB2 = value; }
		}
		#endregion
		
		#region Раскладка файлов по папкам
		public virtual bool AuthorsToDirsAuthorOne {
			get { return m_AuthorsToDirsAuthorOne; }
			set { m_SortTypeAllFB2 = value; }
		}
		public virtual bool AuthorsToDirsAuthorAll {
			get { return m_AuthorsToDirsAuthorAll; }
			set { m_AuthorsToDirsAuthorAll = value; }
		}
		public virtual bool GenresToDirsGenreOne {
			get { return m_GenresToDirsGenreOne; }
			set { m_GenresToDirsGenreOne = value; }
		}
		public virtual bool GenresToDirsGenreAll {
			get { return m_GenresToDirsGenreAll; }
			set { m_GenresToDirsGenreAll = value; }
		}
		public virtual bool GenresTypeGenreSchema {
			get { return m_GenresTypeGenreSchema; }
			set { m_GenresTypeGenreSchema = value; }
		}
		public virtual bool GenresTypeGenreText {
			get { return m_GenresTypeGenreText; }
			set { m_GenresTypeGenreText = value; }
		}
		#endregion
		#endregion
		
		// =====================================================================================================
		//										Папки шаблонного тэга без данны
		// =====================================================================================================
		#region названия папок для шаблонных тэгов, которые не имеют данных
		#region Описание Книги
		// _Нестандартные Жанры
		public virtual string BookInfoNoGenreGroup {
			get { return m_BookInfoNoGenreGroup; }
			set { m_BookInfoNoGenreGroup = value; }
		}
		// Жанра Нет
		public virtual string BookInfoNoGenre {
			get { return m_BookInfoNoGenre; }
			set { m_BookInfoNoGenre = value; }
		}
		// Языка Книги Нет
		public virtual string BookInfoNoLang {
			get { return m_BookInfoNoLang; }
			set { m_BookInfoNoLang = value; }
		}
		// Имени Автора Нет
		public virtual string BookInfoNoFirstName {
			get { return m_BookInfoNoFirstName; }
			set { m_BookInfoNoFirstName = value; }
		}
		// Отчества Автора Нет
		public virtual string BookInfoNoMiddleName {
			get { return m_BookInfoNoMiddleName; }
			set { m_BookInfoNoMiddleName = value; }
		}
		// Фамилия Автора Нет
		public virtual string BookInfoNoLastName {
			get { return m_BookInfoNoLastName; }
			set { m_BookInfoNoLastName = value; }
		}
		// Ника Автора Нет
		public virtual string BookInfoNoNickName {
			get { return m_BookInfoNoNickName; }
			set { m_BookInfoNoNickName = value; }
		}
		// Названия Книги Нет
		public virtual string BookInfoNoBookTitle {
			get { return m_BookInfoNoBookTitle; }
			set { m_BookInfoNoBookTitle = value; }
		}
		// Серии Нет
		public virtual string BookInfoNoSequence {
			get { return m_BookInfoNoSequence; }
			set { m_BookInfoNoSequence = value; }
		}
		// Номера Серии Нет
		public virtual string BookInfoNoNSequence {
			get { return m_BookInfoNoNSequence; }
			set { m_BookInfoNoNSequence = value; }
		}
		// Даты (Текст) Нет
		public virtual string BookInfoNoDateText {
			get { return m_BookInfoNoDateText; }
			set { m_BookInfoNoDateText = value; }
		}
		// Даты (Значение) Нет
		public virtual string BookInfoNoDateValue {
			get { return m_BookInfoNoDateValue; }
			set { m_BookInfoNoDateValue = value; }
		}
		#endregion
		
		#region Издательство
		// Издательства Нет
		public virtual string PublishInfoNoPublisher {
			get { return m_PublishInfoNoYear; }
			set { m_PublishInfoNoYear = value; }
		}
		// Года издания Нет
		public virtual string PublishInfoNoYear {
			get { return m_PublishInfoNoPublisher; }
			set { m_PublishInfoNoPublisher = value; }
		}
		// Города Издания Нет
		public virtual string PublishInfoNoCity {
			get { return m_PublishInfoNoCity; }
			set { m_PublishInfoNoCity = value; }
		}
		#endregion
		
		#region Данные о создателе fb2 файла
		// Имени fb2-создателя Нет
		public virtual string FB2InfoNoFB2FirstName {
			get { return m_FB2InfoNoFB2FirstName; }
			set { m_FB2InfoNoFB2FirstName = value; }
		}
		// Отчества fb2-создателя Нет
		public virtual string FB2InfoNoFB2MiddleName {
			get { return m_FB2InfoNoFB2MiddleName; }
			set { m_FB2InfoNoFB2MiddleName = value; }
		}
		// Фамилия fb2-создателя Нет
		public virtual string FB2InfoNoFB2LastName {
			get { return m_FB2InfoNoFB2LastName; }
			set { m_FB2InfoNoFB2LastName = value; }
		}
		// Ника fb2-создателя Нет
		public virtual string FB2InfoNoFB2NickName {
			get { return m_FB2InfoNoFB2NickName; }
			set { m_FB2InfoNoFB2NickName = value; }
		}
		#endregion
		#endregion
		
		// =====================================================================================================
		// 												Разное
		// =====================================================================================================
		#region Разное
		// Путь к xml файлу возобновления сортировки
		public string FromXmlFile {
			get { return m_FromXmlFile; }
		}
		// Вид сортировки: m_FullSort = true - Полная; false - Избранная.
		public bool IsFullSort {
			get {
				return _sortingTypeEnum == SortingTypeEnum.FullSort
					? true : false;
			}
		}
		// Вид сортировки: Полная или Избранная.
		public SortingTypeEnum sortingTypeEnum {
			get {
				return _sortingTypeEnum;
			}
		}
		// Режим сортировки: true - Возобновление; false - Непрерывная
		public bool IsReNewSort {
			get { return m_FromXmlFile == null ? false : true; }
		}
		#endregion
		#endregion
		
		// =====================================================================================================
		//											Открытые методы класса
		// =====================================================================================================
		#region Открытые методы класса
		/// <summary>
		/// Присвоение критериев Сортировки
		/// </summary>
		/// <param name="lSSQCList">Список экземпляров класса SelectedSortQueryCriteria</param>
		public void setCriterias( List<SelectedSortQueryCriteria> lSSQCList ) {
			m_lSSQCList = lSSQCList;
		}
		
		// получение критериев Сортировки
		public List<SelectedSortQueryCriteria> getCriterias() {
			return m_lSSQCList;
		}
		
		// Обработка регистра имени файла
		public int getRegisterMode() {
			if( m_RegisterAsIs )
				return 0;
			else if ( m_RegisterLower )
				return 1;
			else if ( m_RegisterUpper )
				return 2;
			else if ( m_RegisterAsSentence )
				return 3;
			return 0;
		}
		#endregion
		
		// =====================================================================================================
		//											Загрузка индивидуальных настроек сортировки
		// =====================================================================================================
		#region Закрытые методы класса: загрузка данных по-умолчанию, в зависимости от Сортировщика  (Непрерывная Сортировка)
		/// <summary>
		/// Данные из xml-файла Сортировщика для непрерывной сортировки
		/// </summary>
		private void loadSettingsForNotBreakSort( SortingTypeEnum sortingTypeEnum ) {
			if( sortingTypeEnum == SortingTypeEnum.FullSort ) {
				// Полная Сортировка : данные из xml-файла Сортировщика
				#region Обработка файлов
				m_SourceDir = FileManagerSettings.ReadFullSortSourceDir();
				m_TargetDir = FileManagerSettings.ReadFullSortTargetDir();
				m_ScanSubDirs = FileManagerSettings.ReadFullSortScanSubDirs();
				m_ToZip = FileManagerSettings.ReadFullSortToZip();
				m_NotDelOriginalFiles = FileManagerSettings.ReadFullSortNotDelFB2Files();
				#endregion
				m_Template = FileManagerSettings.ReadFullSortTemplate();
			} else {
				// Избранная Сортировка : данные из xml-файла Сортировщика
				#region Обработка файлов
				m_SourceDir = FileManagerSettings.ReadSelSortSourceDir();
				m_TargetDir = FileManagerSettings.ReadSelSortTargetDir();
				m_ScanSubDirs = FileManagerSettings.ReadSelSortScanSubDirs();
				m_ToZip = FileManagerSettings.ReadSelSortToZip();
				m_NotDelOriginalFiles = FileManagerSettings.ReadSelSortNotDelFB2Files();
				#endregion
				m_Template = FileManagerSettings.ReadSelSortTemplate();
			}
			
			#region Обработка имени файлов
			m_RegisterAsIs			= FileManagerSettings.ReadRegisterAsIs();
			m_RegisterLower			= FileManagerSettings.ReadRegisterLower();
			m_RegisterUpper			= FileManagerSettings.ReadRegisterUpper();
			m_RegisterAsSentence	= FileManagerSettings.ReadRegisterAsSentence();
			m_Translit				= FileManagerSettings.ReadTranslit();
			m_Strict				= FileManagerSettings.ReadStrict();
			m_Space					= FileManagerSettings.ReadSpaceProcess();
			m_FileExistMode			= FileManagerSettings.ReadFileExist();
			#endregion
			
			#region Сортировка файлов
			m_SortTypeAllFB2		= FileManagerSettings.ReadSortAllFB2();
			m_SortTypeOnlyValidFB2	= FileManagerSettings.ReadSortOnlyValidFB2();
			#endregion
			
			#region Раскладка файлов по папкам
			m_AuthorsToDirsAuthorOne	= FileManagerSettings.ReadAuthorOne();
			m_AuthorsToDirsAuthorAll	= FileManagerSettings.ReadAuthorAll();
			m_GenresToDirsGenreOne		= FileManagerSettings.ReadGenreOne();
			m_GenresToDirsGenreAll		= FileManagerSettings.ReadGenreAll();
			m_GenresTypeGenreSchema		= FileManagerSettings.ReadGenreSchema();
			m_GenresTypeGenreText		= FileManagerSettings.ReadGenreText();
			#endregion

			#region Названия папок для шаблонных тэгов, которые не имеют данных
			// названия папки для тэга, у которого нет данных
			m_BookInfoNoGenreGroup	= FileManagerSettings.ReadFMNoGenreGroup();
			m_BookInfoNoGenre		= FileManagerSettings.ReadFMNoGenre();
			m_BookInfoNoLang		= FileManagerSettings.ReadFMNoLang();
			m_BookInfoNoFirstName	= FileManagerSettings.ReadFMNoFirstName();
			m_BookInfoNoMiddleName	= FileManagerSettings.ReadFMNoMiddleName();
			m_BookInfoNoLastName	= FileManagerSettings.ReadFMNoLastName();
			m_BookInfoNoNickName	= FileManagerSettings.ReadFMNoNickName();
			m_BookInfoNoBookTitle	= FileManagerSettings.ReadFMNoBookTitle();
			m_BookInfoNoSequence	= FileManagerSettings.ReadFMNoSequence();
			m_BookInfoNoNSequence	= FileManagerSettings.ReadFMNoNSequence();
			m_BookInfoNoDateText	= FileManagerSettings.ReadFMNoDateText();
			m_BookInfoNoDateValue	= FileManagerSettings.ReadFMNoDateValue();
			// Издательство
			m_PublishInfoNoPublisher	= FileManagerSettings.ReadFMNoPublisher();
			m_PublishInfoNoYear			= FileManagerSettings.ReadFMNoYear();
			m_PublishInfoNoCity			= FileManagerSettings.ReadFMNoCity();
			// Данные о создателе fb2 файла
			m_FB2InfoNoFB2FirstName		= FileManagerSettings.ReadFMNoFB2FirstName();
			m_FB2InfoNoFB2MiddleName	= FileManagerSettings.ReadFMNoFB2MiddleName();
			m_FB2InfoNoFB2LastName		= FileManagerSettings.ReadFMNoFB2LastName();
			m_FB2InfoNoFB2NickName		= FileManagerSettings.ReadFMNoFB2NickName();
			#endregion
		}

		/// <summary>
		/// Загрузка данных из xml-файла восстановления сортировки
		/// </summary>
		private void loadSettingsForReNewSort() {
			#region Код
			#region Обработка файлов
			// загрузка папок: исходная и приемник
			if( m_xmlTree.Element("Folders") != null ) {
				XElement xmlFolders = m_xmlTree.Element("Folders");
				if( xmlFolders.Element("SourceDir") != null )
					m_SourceDir = xmlFolders.Element("SourceDir").Value;
				if( xmlFolders.Element("TargetDir") != null )
					m_TargetDir = xmlFolders.Element("TargetDir").Value;
			}
			// загрузка настроек обработки файлов
			if( m_xmlTree.Element("Settings") != null ) {
				XElement xmlSettings = m_xmlTree.Element("Settings");
				if( xmlSettings.Element("ScanSubDirs") != null )
					m_ScanSubDirs = Convert.ToBoolean( xmlSettings.Element("ScanSubDirs").Value );
				if( xmlSettings.Element("ToZip") != null )
					m_ToZip = Convert.ToBoolean( xmlSettings.Element("ToZip").Value );
				if( xmlSettings.Element("NotDelOriginalFiles") != null )
					m_NotDelOriginalFiles = Convert.ToBoolean( xmlSettings.Element("NotDelOriginalFiles").Value );
			}
			#endregion
			
			#region Обработка имени файлов
			// Шаблоны подстановки
			if( m_xmlTree.Element("Template") != null )
				m_Template = m_xmlTree.Element("Template").Value;

			if( m_xmlTree.Element("Options") != null ) {
				XElement xmlOptions = m_xmlTree.Element("Options");
				if( xmlOptions.Element("General") != null ) {
					XElement xmlGeneral = xmlOptions.Element("General");
					// Регистр имени файла
					if( xmlGeneral.Element("Register") != null ) {
						XElement xmlRegister = xmlGeneral.Element("Register");
						if( xmlRegister.Attribute("AsIs") != null )
							m_RegisterAsIs = Convert.ToBoolean( xmlRegister.Attribute("AsIs").Value );
						if( xmlRegister.Attribute("Lower") != null )
							m_RegisterLower = Convert.ToBoolean( xmlRegister.Attribute("Lower").Value );
						if( xmlRegister.Attribute("Upper") != null )
							m_RegisterUpper = Convert.ToBoolean( xmlRegister.Attribute("Upper").Value );
						if( xmlRegister.Attribute("AsSentence") != null )
							m_RegisterAsSentence = Convert.ToBoolean( xmlRegister.Attribute("AsSentence").Value );
					}
					// Транслитерация имен файлов
					if( xmlGeneral.Element("Translit") != null )
						m_Translit = Convert.ToBoolean( xmlGeneral.Element("Translit").Value );
					// 'Строгие' имена файлов
					if( xmlGeneral.Element("Strict") != null )
						m_Strict = Convert.ToBoolean( xmlGeneral.Element("Strict").Value );
					// Обработка пробелов
					if( xmlGeneral.Element("Space") != null ) {
						if( xmlGeneral.Element("Space").Attribute("index") != null )
							m_Space = Convert.ToInt16( xmlGeneral.Element("Space").Attribute("index").Value );
					}
					// Одинаковые файлы
					if( xmlGeneral.Element("FileExistMode") != null ) {
						if( xmlGeneral.Element("FileExistMode").Attribute("index") != null )
							m_FileExistMode = Convert.ToInt16( xmlGeneral.Element("FileExistMode").Attribute("index").Value );
					}
				}
			}
			#endregion
			
			#region Сортировка файлов
			if( m_xmlTree.Element("Options") != null ) {
				XElement xmlOptions = m_xmlTree.Element("Options");
				if( xmlOptions.Element("General") != null ) {
					XElement xmlGeneral = xmlOptions.Element("General");
					// Сортировка файлов
					if( xmlGeneral.Element("SortType") != null ) {
						XElement xmlSortType = xmlGeneral.Element("SortType");
						if( xmlSortType.Attribute("AllFB2") != null )
							m_SortTypeAllFB2 = Convert.ToBoolean( xmlSortType.Attribute("AllFB2").Value );
						if( xmlSortType.Attribute("OnlyValidFB2") != null )
							m_SortTypeOnlyValidFB2 = Convert.ToBoolean( xmlSortType.Attribute("OnlyValidFB2").Value );
					}
				}
			}
			#endregion
			
			#region Раскладка файлов по папкам
			if( m_xmlTree.Element("Options") != null ) {
				XElement xmlOptions = m_xmlTree.Element("Options");
				if( xmlOptions.Element("General") != null ) {
					XElement xmlGeneral = xmlOptions.Element("General");
					// Раскладка файлов по папкам
					if( xmlGeneral.Element("FilesToDirs") != null ) {
						XElement xmlFilesToDirs = xmlGeneral.Element("FilesToDirs");
						// По Авторам
						if( xmlFilesToDirs.Element("AuthorsToDirs") != null ) {
							XElement xmlAuthorsToDirs = xmlFilesToDirs.Element("AuthorsToDirs");
							if( xmlAuthorsToDirs.Attribute("AuthorOne") != null )
								m_AuthorsToDirsAuthorOne = Convert.ToBoolean( xmlAuthorsToDirs.Attribute("AuthorOne").Value );
							if( xmlAuthorsToDirs.Attribute("AuthorAll") != null )
								m_AuthorsToDirsAuthorAll = Convert.ToBoolean( xmlAuthorsToDirs.Attribute("AuthorAll").Value );
						}
						// По Жанрам
						if( xmlFilesToDirs.Element("GenresToDirs") != null ) {
							XElement xmlGenresToDirs = xmlFilesToDirs.Element("GenresToDirs");
							if( xmlGenresToDirs.Attribute("GenreOne") != null )
								m_GenresToDirsGenreOne = Convert.ToBoolean( xmlGenresToDirs.Attribute("GenreOne").Value );
							if( xmlGenresToDirs.Attribute("GenreAll") != null )
								m_GenresToDirsGenreAll = Convert.ToBoolean( xmlGenresToDirs.Attribute("GenreAll").Value );
						}
						// Вид папки-Жанра
						if( xmlFilesToDirs.Element("GenresType") != null ) {
							XElement xmlGenresType = xmlFilesToDirs.Element("GenresType");
							if( xmlGenresType.Attribute("GenreSchema") != null )
								m_GenresTypeGenreSchema = Convert.ToBoolean( xmlGenresType.Attribute("GenreSchema").Value );
							if( xmlGenresType.Attribute("GenreText") != null )
								m_GenresTypeGenreText = Convert.ToBoolean( xmlGenresType.Attribute("GenreText").Value );
						}
					}
				}
			}
			#endregion
			
			#region Названия папок для шаблонных тэгов, которые не имеют данных
			if( m_xmlTree.Element("Options") != null ) {
				XElement xmlOptions = m_xmlTree.Element("Options");
				if( xmlOptions.Element("NoTags") != null ) {
					XElement xmlNoTags = xmlOptions.Element("NoTags");
					if( xmlNoTags.Element("BookInfo") != null ) {
						XElement xmlNoTagsBookInfo = xmlNoTags.Element("BookInfo");
						if( xmlNoTagsBookInfo.Element("NoGenreGroup") != null )
							m_BookInfoNoGenre = xmlNoTagsBookInfo.Element("NoGenreGroup").Value;
						if( xmlNoTagsBookInfo.Element("NoGenre") != null )
							m_BookInfoNoGenre = xmlNoTagsBookInfo.Element("NoGenre").Value;
						if( xmlNoTagsBookInfo.Element("NoLang") != null )
							m_BookInfoNoLang = xmlNoTagsBookInfo.Element("NoLang").Value;
						if( xmlNoTagsBookInfo.Element("NoFirstName") != null )
							m_BookInfoNoFirstName = xmlNoTagsBookInfo.Element("NoFirstName").Value;
						if( xmlNoTagsBookInfo.Element("NoMiddleName") != null )
							m_BookInfoNoMiddleName = xmlNoTagsBookInfo.Element("NoMiddleName").Value;
						if( xmlNoTagsBookInfo.Element("NoLastName") != null )
							m_BookInfoNoLastName = xmlNoTagsBookInfo.Element("NoLastName").Value;
						if( xmlNoTagsBookInfo.Element("NoNickName") != null )
							m_BookInfoNoNickName = xmlNoTagsBookInfo.Element("NoNickName").Value;
						if( xmlNoTagsBookInfo.Element("NoBookTitle") != null )
							m_BookInfoNoBookTitle = xmlNoTagsBookInfo.Element("NoBookTitle").Value;
						if( xmlNoTagsBookInfo.Element("NoSequence") != null )
							m_BookInfoNoSequence = xmlNoTagsBookInfo.Element("NoSequence").Value;
						if( xmlNoTagsBookInfo.Element("NoNSequence") != null )
							m_BookInfoNoNSequence = xmlNoTagsBookInfo.Element("NoNSequence").Value;
						if( xmlNoTagsBookInfo.Element("NoDateText") != null )
							m_BookInfoNoDateText = xmlNoTagsBookInfo.Element("NoDateText").Value;
						if( xmlNoTagsBookInfo.Element("NoDateValue") != null )
							m_BookInfoNoDateValue = xmlNoTagsBookInfo.Element("NoDateValue").Value;
					}
					
					if( xmlNoTags.Element("PublishInfo") != null ) {
						XElement xmlNoTagsPublishInfo = xmlNoTags.Element("PublishInfo");
						if( xmlNoTagsPublishInfo.Element("NoPublisher") != null )
							m_PublishInfoNoPublisher = xmlNoTagsPublishInfo.Element("NoPublisher").Value;
						if( xmlNoTagsPublishInfo.Element("NoYear") != null )
							m_PublishInfoNoYear = xmlNoTagsPublishInfo.Element("NoYear").Value;
						if( xmlNoTagsPublishInfo.Element("NoCity") != null )
							m_PublishInfoNoCity = xmlNoTagsPublishInfo.Element("NoCity").Value;
					}
					
					if( xmlNoTags.Element("FB2Info") != null ) {
						XElement xmlNoTagsFB2Info = xmlNoTags.Element("FB2Info");
						if( xmlNoTagsFB2Info.Element("NoFB2FirstName") != null )
							m_FB2InfoNoFB2FirstName = xmlNoTagsFB2Info.Element("NoFB2FirstName").Value;
						if( xmlNoTagsFB2Info.Element("NoFB2MiddleName") != null )
							m_FB2InfoNoFB2MiddleName = xmlNoTagsFB2Info.Element("NoFB2MiddleName").Value;
						if( xmlNoTagsFB2Info.Element("NoFB2LastName") != null )
							m_FB2InfoNoFB2LastName = xmlNoTagsFB2Info.Element("NoFB2LastName").Value;
						if( xmlNoTagsFB2Info.Element("NoFB2NickName") != null )
							m_FB2InfoNoFB2NickName = xmlNoTagsFB2Info.Element("NoFB2NickName").Value;
					}
				}
			}
			#endregion
			#endregion
		}
		
		/// <summary>
		/// Загрузка критериев Избранной Сортировки при возобновлении сортировки
		/// </summary>
		private void loadCriteriasForSelReNewSort() {
			XElement xeCriterias = m_xmlTree.Element("Criterias");
			IEnumerable<XElement> iexeCriterias  = from el in xeCriterias.Descendants("Criteria") select el;
			
			foreach ( XElement c in iexeCriterias ) {
				XElement xeLang = c.Element("Lang");
				XElement xeGGroup = c.Element("GenresGroup");
				XElement xeGenre = c.Element("Genre");
				XElement xeLast = c.Element("LastName");
				XElement xeFirst = c.Element("FirstName");
				XElement xeMiddle = c.Element("MiddleName");
				XElement xeNick = c.Element("NickName");
				XElement xeSequence = c.Element("Sequence");
				XElement xeBookTitle = c.Element("BookTitle");
				XElement xeExactFit = c.Element("ExactFit");
				XElement xeGenresFB2Librusec = c.Element("GenresFB2Librusec");
				// заполняем список критериев поиска для Избранной Сортировки
				SortQueryCriteria SelSortQuery = new SelectedSortQueryCriteria(
					xeLang != null ? xeLang.Value : null, xeGGroup != null ? xeGGroup.Value : null, xeGenre != null ? xeGenre.Value : null,
					xeLast != null ? xeLast.Value : null, xeFirst != null ? xeFirst.Value : null, xeMiddle != null ? xeMiddle.Value : null, xeNick != null ? xeNick.Value : null,
					xeSequence != null ? xeSequence.Value : null, xeBookTitle != null ? xeBookTitle.Value : null,
					xeExactFit != null ? Convert.ToBoolean( xeExactFit.Value ) : true
				);
				m_lSSQCList.Add( SelSortQuery );
			}
		}
		#endregion
	}
}

