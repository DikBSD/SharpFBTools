/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 22.07.2015
 * Время: 13:13
 * 
 */
using System;

namespace Core.Common
{
	/// <summary>
	/// Названия Групп Жанров
	/// </summary>
	public class GenresGroup : IGenresGroup
	{
		#region закрытые данные класса
		// название Групп Жанров
		private string m_GenreGroupsSf			= "ФАНТАСТИКА, ФЭНТЕЗИ";
		private string m_GenreGroupsDetective	= "ДЕТЕКТИВЫ, БОЕВИКИ";
		private string m_GenreGroupsProse		= "ПРОЗА";
		private string m_GenreGroupsLove		= "ЛЮБОВНЫЕ РОМАНЫ";
		private string m_GenreGroupsAdventure	= "ПРИКЛЮЧЕНИЯ";
		private string m_GenreGroupsChildren	= "ДЕТСКОЕ";
		private string m_GenreGroupsPoetry		= "ПОЭЗИЯ, ДРАМАТУРГИЯ";
		private string m_GenreGroupsAntique		= "СТАРИННОЕ";
		private string m_GenreGroupsScience		= "НАУКА, ОБРАЗОВАНИЕ";
		private string m_GenreGroupsComputers	= "КОМПЬЮТЕРЫ";
		private string m_GenreGroupsReference	= "СПРАВОЧНИКИ";
		private string m_GenreGroupsNonfiction	= "ДОКУМЕНТАЛЬНОЕ";
		private string m_GenreGroupsReligion	= "РЕЛИГИЯ";
		private string m_GenreGroupsHumor		= "ЮМОР";
		private string m_GenreGroupsHome		= "ДОМ, СЕМЬЯ";
		private string m_GenreGroupsBusiness	= "БИЗНЕС";
		private string m_GenreGroupsTech		= "ТЕХНИКА";
		private string m_GenreGroupsMilitary	= "ВОЕННОЕ ДЕЛО";
		private string m_GenreGroupsFolklore	= "ФОЛЬКЛЕР";
		private string m_GenreGroupsOther		= "ПРОЧЕЕ";
		#endregion
		
		public GenresGroup()
		{
		}
		
		#region Открытые свойства
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
	}
}
