/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 22.07.2015
 * Время: 14:03
 * 
 */
using System;
using System.IO;
using System.Linq;
using System.Xml.Linq;

using Core.Common;
using Settings;

namespace Core.FileManager
{
	/// <summary>
	/// GenresGroupForSorting: Названия Групп Жанров для Сортировщиков
	/// </summary>
	public class GenresGroupForSorting : IGenresGroup
	{
		#region закрытые данные класса
		private string m_FromXmlFile = null; // null - сортировка сначала/непрерывная
		private readonly XElement m_xmlTree	= null; // дерево настроек из xml файла m_FromXmlFile
		
		// название Групп Жанров
		private string m_GenreGroupsSf			= string.Empty;
		private string m_GenreGroupsDetective	= string.Empty;
		private string m_GenreGroupsProse		= string.Empty;
		private string m_GenreGroupsLove		= string.Empty;
		private string m_GenreGroupsAdventure	= string.Empty;
		private string m_GenreGroupsChildren	= string.Empty;
		private string m_GenreGroupsPoetry		= string.Empty;
		private string m_GenreGroupsAntique		= string.Empty;
		private string m_GenreGroupsScience		= string.Empty;
		private string m_GenreGroupsComputers	= string.Empty;
		private string m_GenreGroupsReference	= string.Empty;
		private string m_GenreGroupsNonfiction	= string.Empty;
		private string m_GenreGroupsReligion	= string.Empty;
		private string m_GenreGroupsHumor		= string.Empty;
		private string m_GenreGroupsHome		= string.Empty;
		private string m_GenreGroupsBusiness	= string.Empty;
		private string m_GenreGroupsTech		= string.Empty;
		private string m_GenreGroupsMilitary	= string.Empty;
		private string m_GenreGroupsFolklore	= string.Empty;
		private string m_GenreGroupsOther		= string.Empty;
		#endregion
		
		public GenresGroupForSorting( string FromXmlFile )
		{
			m_FromXmlFile = FromXmlFile;
			if( m_FromXmlFile != null ) {
				// Возобновление сортировки
				if( File.Exists( m_FromXmlFile ) ) {
					try {
						m_xmlTree = XElement.Load( m_FromXmlFile );
						// загрузка общих данных из xml-файла
						loadFromXMLGenreGroup();	 // загрузка Названий Групп Жанров
					} catch {
						return;
					}
				} else
					return;
			} else {
				// непрерывная сортировка
				readDefSettinsg(); // загрузка общих основных данных по-умолчанию из xml-файла настроек Сортировщиков
			}
		}
		
		#region Названия Групп Жанров
		// Фантастика, Фэнтэзи
		public virtual string GenresGroupSf {
			get { return m_GenreGroupsSf; }
			set { m_GenreGroupsSf = value; }
        }
		// Детективы, Боевики
		public virtual string GenresGroupDetective {
			get { return m_GenreGroupsDetective; }
			set { m_GenreGroupsDetective = value; }
        }
		// Проза
		public virtual string GenresGroupProse {
			get { return m_GenreGroupsProse; }
			set { m_GenreGroupsProse = value; }
        }
		// Любовные романы
		public virtual string GenresGroupLove {
			get { return m_GenreGroupsLove; }
			set { m_GenreGroupsLove = value; }
        }
		// Приключения
		public virtual string GenresGroupAdventure {
			get { return m_GenreGroupsAdventure; }
			set { m_GenreGroupsAdventure = value; }
        }
		// Детское
		public virtual string GenresGroupChildren {
			get { return m_GenreGroupsChildren; }
			set { m_GenreGroupsChildren = value; }
        }
		// Поэзия, Драматургия
		public virtual string GenresGroupPoetry {
			get { return m_GenreGroupsPoetry; }
			set { m_GenreGroupsPoetry = value; }
        }
		// Старинное
		public virtual string GenresGroupAntique {
			get { return m_GenreGroupsAntique; }
			set { m_GenreGroupsAntique = value; }
        }
		// Наука, Образование
		public virtual string GenresGroupScience {
			get { return m_GenreGroupsScience; }
			set { m_GenreGroupsScience = value; }
        }
		// Компьютеры
		public virtual string GenresGroupComputers {
			get { return m_GenreGroupsComputers; }
			set { m_GenreGroupsComputers = value; }
        }
		// Справочники
		public virtual string GenresGroupReference {
			get { return m_GenreGroupsReference; }
			set { m_GenreGroupsReference = value; }
        }
		// Документальное
		public virtual string GenresGroupNonfiction {
			get { return m_GenreGroupsNonfiction; }
			set { m_GenreGroupsNonfiction = value; }
        }
		// Религия
		public virtual string GenresGroupReligion {
			get { return m_GenreGroupsReligion; }
			set { m_GenreGroupsReligion = value; }
        }
		// Юмор
		public virtual string GenresGroupHumor {
			get { return m_GenreGroupsHumor; }
			set { m_GenreGroupsHumor = value; }
        }
		// Дом, Семья
		public virtual string GenresGroupHome {
			get { return m_GenreGroupsHome; }
			set { m_GenreGroupsHome = value; }
        }
		// Бизнес
		public virtual string GenresGroupBusiness {
			get { return m_GenreGroupsBusiness; }
			set { m_GenreGroupsBusiness = value; }
        }
		// Техника
		public virtual string GenresGroupTech {
			get { return m_GenreGroupsTech; }
			set { m_GenreGroupsTech = value; }
        }
		// Военное дело
		public virtual string GenresGroupMilitary {
			get { return m_GenreGroupsMilitary; }
			set { m_GenreGroupsMilitary = value; }
        }
		// Фольклор
		public virtual string GenresGroupFolklore {
			get { return m_GenreGroupsFolklore; }
			set { m_GenreGroupsFolklore = value; }
        }
		// Прочее
		public virtual string GenresGroupOther {
			get { return m_GenreGroupsOther; }
			set { m_GenreGroupsOther = value; }
        }
		#endregion
		
		
		private void readDefSettinsg() {
			// название Групп Жанров
			m_GenreGroupsSf			= FileManagerSettings.ReadFMSf();
			m_GenreGroupsDetective	= FileManagerSettings.ReadFMDetective();
			m_GenreGroupsProse		= FileManagerSettings.ReadFMProse();
			m_GenreGroupsLove		= FileManagerSettings.ReadFMLove();
			m_GenreGroupsAdventure	= FileManagerSettings.ReadFMAdventure();
			m_GenreGroupsChildren	= FileManagerSettings.ReadFMChildren();
			m_GenreGroupsPoetry		= FileManagerSettings.ReadFMPoetry();
			m_GenreGroupsAntique	= FileManagerSettings.ReadFMAntique();
			m_GenreGroupsScience	= FileManagerSettings.ReadFMScience();
			m_GenreGroupsComputers	= FileManagerSettings.ReadFMComputers();
			m_GenreGroupsReference	= FileManagerSettings.ReadFMReference();
			m_GenreGroupsNonfiction	= FileManagerSettings.ReadFMNonfiction();
			m_GenreGroupsReligion	= FileManagerSettings.ReadFMReligion();
			m_GenreGroupsHumor		= FileManagerSettings.ReadFMHumor();
			m_GenreGroupsHome		= FileManagerSettings.ReadFMHome();
			m_GenreGroupsBusiness	= FileManagerSettings.ReadFMBusiness();
			m_GenreGroupsTech		= FileManagerSettings.ReadFMTech();
			m_GenreGroupsMilitary	= FileManagerSettings.ReadFMMilitary();
			m_GenreGroupsFolklore	= FileManagerSettings.ReadFMFolklore();
			m_GenreGroupsOther		= FileManagerSettings.ReadFMOther();
		}
		
		// загрузка Названий Групп Жанров
		private void loadFromXMLGenreGroup() {
			if( m_xmlTree.Element("Options") != null ) {
				XElement xmlOptions = m_xmlTree.Element("Options");
				if( xmlOptions.Element("GenreGroups") != null ) {
					XElement xmlGenreGroups = xmlOptions.Element("GenreGroups");
					if( xmlGenreGroups.Element("sf") != null )
						m_GenreGroupsSf = xmlGenreGroups.Element("sf").Value;
					if( xmlGenreGroups.Element("detective") != null )
						m_GenreGroupsDetective = xmlGenreGroups.Element("detective").Value;
					if( xmlGenreGroups.Element("prose") != null )
						m_GenreGroupsProse = xmlGenreGroups.Element("prose").Value;
					if( xmlGenreGroups.Element("love") != null )
						m_GenreGroupsLove = xmlGenreGroups.Element("love").Value;
					if( xmlGenreGroups.Element("adventure") != null )
						m_GenreGroupsAdventure = xmlGenreGroups.Element("adventure").Value;
					if( xmlGenreGroups.Element("children") != null )
						m_GenreGroupsChildren = xmlGenreGroups.Element("children").Value;
					if( xmlGenreGroups.Element("poetry") != null )
						m_GenreGroupsPoetry = xmlGenreGroups.Element("poetry").Value;
					if( xmlGenreGroups.Element("antique") != null )
						m_GenreGroupsAntique = xmlGenreGroups.Element("antique").Value;
					if( xmlGenreGroups.Element("science") != null )
						m_GenreGroupsScience = xmlGenreGroups.Element("science").Value;
					if( xmlGenreGroups.Element("computers") != null )
						m_GenreGroupsComputers = xmlGenreGroups.Element("computers").Value;
					if( xmlGenreGroups.Element("reference") != null )
						m_GenreGroupsReference = xmlGenreGroups.Element("reference").Value;
					if( xmlGenreGroups.Element("nonfiction") != null )
						m_GenreGroupsNonfiction = xmlGenreGroups.Element("nonfiction").Value;
					if( xmlGenreGroups.Element("religion") != null )
						m_GenreGroupsReligion = xmlGenreGroups.Element("religion").Value;
					if( xmlGenreGroups.Element("humor") != null )
						m_GenreGroupsHumor = xmlGenreGroups.Element("humor").Value;
					if( xmlGenreGroups.Element("home") != null )
						m_GenreGroupsHome = xmlGenreGroups.Element("home").Value;
					if( xmlGenreGroups.Element("business") != null )
						m_GenreGroupsBusiness = xmlGenreGroups.Element("business").Value;
					if( xmlGenreGroups.Element("tech") != null )
						m_GenreGroupsTech = xmlGenreGroups.Element("tech").Value;
					if( xmlGenreGroups.Element("military") != null )
						m_GenreGroupsMilitary = xmlGenreGroups.Element("military").Value;
					if( xmlGenreGroups.Element("folklore") != null )
						m_GenreGroupsFolklore = xmlGenreGroups.Element("folklore").Value;
					if( xmlGenreGroups.Element("other") != null )
						m_GenreGroupsOther = xmlGenreGroups.Element("other").Value;
				}
			}
		}

	}
}
