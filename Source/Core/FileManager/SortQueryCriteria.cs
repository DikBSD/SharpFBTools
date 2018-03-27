/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 24.06.2009
 * Time: 15:53
 * 
 * License: GPL 2.1
 */
using System;

namespace Core.FileManager
{
	/// <summary>
	/// Данные критерия поиска и сравнения для сортировки
	/// </summary>
	public class SortQueryCriteria
	{
		#region Закрытые члены класса
		private string	m_Lang			= string.Empty;
		private string	m_GenresGroup	= string.Empty;
		private string	m_Genre			= string.Empty;
		private string	m_LastName		= string.Empty;
		private string	m_FirstName		= string.Empty;
		private string	m_MiddleName	= string.Empty;
		private string	m_NickName		= string.Empty;
		private string	m_BookTitle		= string.Empty;
		private string	m_Sequence		= string.Empty;
		private bool	m_ExactFit		= true;
		#endregion
		
		public SortQueryCriteria()
		{
		}
		public SortQueryCriteria( string sLang, string sGenresGroup, string sGenre,
		                         string sLastName, string sFirstName, string sMiddleName, string sNickName,
		                         string sSequence, string sBookTitle, bool bExactFit )
		{
			m_Lang			= sLang;
			m_GenresGroup	= sGenresGroup;
			m_Genre			= sGenre;
			m_LastName		= sLastName;
			m_FirstName		= sFirstName;
			m_MiddleName	= sMiddleName;
			m_NickName		= sNickName;
			m_Sequence		= sSequence;
			m_BookTitle		= sBookTitle;
			m_ExactFit		= bExactFit;
		}
		
		#region Свойства класса
		public virtual string Lang {
			get { return m_Lang; }
			set { m_Lang = value; }
		}
		public virtual string GenresGroup {
			get { return m_GenresGroup; }
			set { m_GenresGroup = value; }
		}
		public virtual string Genre {
			get { return m_Genre; }
			set { m_Genre = value; }
		}
		public virtual string LastName {
			get { return m_LastName; }
			set { m_LastName = value; }
		}
		public virtual string FirstName {
			get { return m_FirstName; }
			set { m_FirstName = value; }
		}
		public virtual string MiddleName {
			get { return m_MiddleName; }
			set { m_MiddleName = value; }
		}
		public virtual string NickName {
			get { return m_NickName; }
			set { m_NickName = value; }
		}
		public virtual string Sequence {
			get { return m_Sequence; }
			set { m_Sequence = value; }
		}
		public virtual string BookTitle {
			get { return m_BookTitle; }
			set { m_BookTitle = value; }
		}
		public virtual bool ExactFit {
			get { return m_ExactFit; }
			set { m_ExactFit = value; }
		}
		#endregion
	}
}
