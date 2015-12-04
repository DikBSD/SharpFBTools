﻿/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 18.09.2009
 * Time: 10:55
 * 
 * License: GPL 2.1
 */
using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

using System.Windows.Forms;

using Core.FB2.Description.Common;
using Core.FB2.Description.TitleInfo;
using Core.Common;

namespace Core.Duplicator
{
	/// <summary>
	/// Класс, хранящий данные по книге для Дубликатора
	/// </summary>
	public class BookData
	{
		#region Закрытые члены класса
		private BookTitle		m_BookTitle	= null;	// Название Книги
		private IList<Author> 	m_Authors	= null;	// список Авторов Книги
		private IList<Genre> 	m_Genres	= null;	// список Жанров Книги
		private string 			m_Lang		= null;	// Язык Книги
		private string 			m_Id		= null;	// Id Книги
		private string 			m_Version	= null;	// Версия Книги
		private string 			m_Encoding	= null; // кодировка книги
		private string			m_Path		= null;	// путь к fb2-файлу Книги
		
		private string			m_NoAuthor	= "<Автор книги отсутствует>"; // Когда либо нет тега <authors>, либо все его подтеги - пустые
		#endregion
		
		public BookData( BookTitle BookTitle, IList<Author> Authors, IList<Genre> Genres, string Lang, string Id, string Version, string Path, string Encoding )
		{
			m_BookTitle	= BookTitle;
			m_Authors	= Authors;
			m_Genres	= Genres;
			m_Lang		= Lang;
			m_Id		= Id;
			m_Version	= Version;
			m_Encoding	= Encoding;
			m_Path		= Path;
		}
		#region Закрытые вспомогательные методы класса
		// формирование списка строк с ФИО Авторов, которые есть в обоих списках Авторов (Intersection)
		// WithMiddleName = true - учитывать Отчество Автора
		private List<string> listIntersection(IList<Author> Authors1, IList<Author> Authors2, bool WithMiddleName) {
			if ( Authors1 != null && Authors2 != null )
				if ( Authors1.Count != Authors2.Count )
					return null;

			List<string> list1 = makeListFOIAuthors(Authors1, WithMiddleName);
			List<string> list2 = makeListFOIAuthors(Authors2, WithMiddleName);
			return list1.Intersect(list2, new FB2EqualityComparer()).ToList();
		}
		#endregion
		
		#region Открытые методы класса
		
		// формирование списка из строк ФИО каждого Автора из Authors
		// WithMiddleName = true - учитывать Отчество Автора
		public List<string> makeListFOIAuthors(IList<Author> Authors, bool WithMiddleName) {
			List<string> list = new List<string>();
			if ( Authors == null ) {
				list.Add(m_NoAuthor);
				return list;
			}
			
			for ( int i = 0; i != Authors.Count; ++i ) {
				bool AuthorExist = true;
				StringBuilder fio = new StringBuilder();
				if ( Authors[i].LastName != null && !string.IsNullOrWhiteSpace(Authors[i].LastName.Value ) )
					fio.Append(Authors[i].LastName.Value.Trim());
				if ( Authors[i].FirstName != null && !string.IsNullOrWhiteSpace( Authors[i].FirstName.Value ) ) {
					fio.Append(" ");
					fio.Append(Authors[i].FirstName.Value.Trim());
				}
				if ( WithMiddleName ) {
					if (Authors[i].MiddleName != null && !string.IsNullOrWhiteSpace( Authors[i].MiddleName.Value ) ) {
						fio.Append(" ");
						fio.Append(Authors[i].MiddleName.Value.Trim());
					}
				}
				
				bool MiddleNameExist = false;
				if ( WithMiddleName ) {
					if ( Authors[i].MiddleName != null && !string.IsNullOrWhiteSpace( Authors[i].MiddleName.Value ) )
						MiddleNameExist = true;
				}
				bool LastNameExist = false;
				if ( Authors[i].LastName != null && !string.IsNullOrWhiteSpace( Authors[i].LastName.Value ) )
					LastNameExist = true;
				bool FirstNameExist = false;
				if ( Authors[i].FirstName != null && !string.IsNullOrWhiteSpace( Authors[i].FirstName.Value ) )
					FirstNameExist = true;
				
				if ( WithMiddleName ) {
					if ( !LastNameExist && !FirstNameExist && !MiddleNameExist )
						AuthorExist = false;
				} else {
					if ( !LastNameExist && !FirstNameExist )
						AuthorExist = false;
				}

				string s = fio.ToString();
				if ( !AuthorExist )
					s = m_NoAuthor;
				if ( s != m_NoAuthor ) {
					if ( !list.Contains(s) )
						list.Add(s);
				} else
					list.Add(s + ( i > 0 ? i.ToString() : string.Empty ) );
			}
			list.Sort(); // чтобы учесть одинаковые книги, где Авторы переставлены местами
			return list;
		}
		// сравнение проводится только по Авторам: число авторов в книгах может быть разным
		// без учета Path (книги могут быть абсолютно одинаковые, но размещаться в раных папках)
		// без учета жанров (книги могут быть одинаковые, но разные книгоделатели присвоили им разные жанры)
		// без учета ID, версии, кодировки...
		// WithMiddleName = true - учитывать Отчество Автора
		public bool isSameBook(BookData RightValue, bool WithMiddleName) {
			if ( RightValue == null )
				return false;
			else {
				// все авторы 2-х сравниваемых книг должны быть одни и те же
				if ( this.Authors != null && RightValue.Authors != null ) {
					if ( this.Authors.Count == RightValue.Authors.Count ) {
						List<string> l = listIntersection( this.Authors, RightValue.Authors, WithMiddleName );
						return l != null
							? (l.Count == this.Authors.Count)
							: false;
					} else
						return false;
				} else if ( this.Authors != null && RightValue.Authors == null ) {
					List<string> l = listIntersection( this.Authors, RightValue.Authors, WithMiddleName );
					return l != null
						? (l.Count == this.Authors.Count)
						: false;
				} else if ( this.Authors == null ) {
					List<string> l = listIntersection( this.Authors, RightValue.Authors, WithMiddleName );
					if ( l == null || l.Count == 0 )
						return false;
					else {
						if ( l.Count == 1 )
							if ( l[0] == m_NoAuthor )
								return true;
							else
								return false;
						else // l.Count > 1
							return false;
					}
				}
			}
			return false;
		}
		#endregion
		
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
		
		public virtual string Lang {
			get { return m_Lang; }
			set { m_Lang = value; }
		}
		
		public virtual string Version {
			get { return m_Version; }
			set { m_Version = value; }
		}
		
		public virtual string Encoding {
			get { return m_Encoding; }
			set { m_Encoding = value; }
		}
		
		public virtual string Path {
			get { return m_Path; }
			set { m_Path = value; }
		}

		#endregion
	}
}