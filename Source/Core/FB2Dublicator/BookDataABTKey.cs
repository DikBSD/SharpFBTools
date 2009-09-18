/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 17.09.2009
 * Time: 13:30
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
	/// Класс, хранящий всех Авторов Книги и Название этой Книги, как ключ хеш таблицы
	/// </summary>
	public class BookDataABTKey
	{
		#region Закрытые члены класса
		private IList<Author> 	m_Authors	= null;	// список Авторов Книги
		private BookTitle		m_BookTitle	= null;	// Название Книги
		#endregion
		
		public BookDataABTKey( IList<Author> Authors, BookTitle BookTitle )
		{
			m_Authors		= Authors;
			m_BookTitle		= BookTitle;
		}

		#region Свойства класса
		public virtual BookTitle BookTitle {
			get { return m_BookTitle; } 
			set { m_BookTitle = value; }
        }
		
		public virtual IList<Author> Authors {
			get { return m_Authors; }
        }
		#endregion
	}
}
