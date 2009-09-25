/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 18.09.2009
 * Time: 10:55
 * 
 * License: GPL 2.1
 */
using System;
using System.Collections.Generic;

using Core.FB2.Description.Common;
using Core.FB2.Description.TitleInfo;

namespace Core.FB2Dublicator
{
	/// <summary>
	/// Класс, хранящий данные по книге
	/// </summary>
	public class BookData
	{
		#region Закрытые члены класса
		private BookTitle		m_BookTitle	= null;	// Название Книги
		private IList<Author> 	m_Authors	= null;	// список Авторов Книги
		private IList<Genre> 	m_Genres	= null;	// список Жанров Книги
		private string 			m_Id		= null;	// Id Книги
		private string 			m_Version	= null;	// Версия Книги
		private string			m_Path		= null;	// путь к fb2-файлу Книги
		private string 			m_Encoding	= null; // кодировка книги
		#endregion
		
		public BookData( BookTitle BookTitle, IList<Author> Authors, IList<Genre> Genres, string Id, string Version, string Path, string Encoding )
		{
			m_BookTitle	= BookTitle;
			m_Authors	= Authors;
			m_Genres	= Genres;
			m_Id		= Id;
			m_Version	= Version;
			m_Path		= Path;
			m_Encoding	= Encoding;
		}
		
		#region Свойства класса
		public virtual BookTitle BookTitle {
			get { return m_BookTitle; } 
			set { m_BookTitle = value; }
        }
		
		public virtual IList<Author> Authors {
			get { return m_Authors; }
			set { m_Authors = value; }
        }
		
		public virtual IList<Genre> Genres {
			get { return m_Genres; }
			set { m_Genres = value; }
        }
		
		public virtual string Id {
			get { return m_Id; }
			set { m_Id = value; }
        }
		
		public virtual string Version {
			get { return m_Version; }
			set { m_Version = value; }
        }
		
		public virtual string Path {
			get { return m_Path; }
			set { m_Path = value; }
        }
		
		public virtual string Encoding {
			get { return m_Encoding; }
			set { m_Encoding = value; }
        }
		#endregion
	}
}
