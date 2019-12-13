/*
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
        private IList<Author>   m_FB2Authors = null; // список Авторов fb2 файла
        private string 			m_Encoding	= null; // кодировка книги
		private string			m_Path		= null;	// путь к fb2-файлу Книги
		
		private const string	m_AuthorNotExist      = "<Автор книги отсутствует>"; // Когда либо нет тега <authors>, либо все его подтеги - пустые
        private const string    m_FB2AuthorNotExist   = "<Автор fb2 файла отсутствует>"; // Когда либо нет тега <authors>, либо все его подтеги - пустые
        #endregion

        public BookData( BookTitle BookTitle, IList<Author> Authors, IList<Genre> Genres, string Lang, string Id, string Version, IList<Author> FB2Authors, string Path, string Encoding )
		{
			m_BookTitle	    = BookTitle;
			m_Authors   	= Authors;
			m_Genres    	= Genres;
			m_Lang	    	= Lang;
			m_Id	    	= Id;
			m_Version   	= Version;
            m_FB2Authors    = FB2Authors;
            m_Encoding      = Encoding;
            m_Path		    = Path;
		}

        #region Открытые методы класса
        /// <summary>
        /// Проверка - та же самая ли это книга:
        /// сравнение проводится только по Авторам (только по Авторам fb2 файлов): число авторов в книгах может быть разным;
        /// без учета Path (книги могут быть абсолютно одинаковые, но размещаться в разных папках);
        /// без учета жанров (книги могут быть одинаковые, но разные книгоделатели присвоили им разные жанры);
        /// без учета ID, версии, кодировки...
        /// </summary>
        /// <param name="RightValue">Данные книги для сравнения</param>
        /// <param name="WithMiddleName">учитывать Отчество Автора (true)</param>
        /// <param name="IsFB2Author">Автор книги (false) или Автора fb2 файла (true)</param>
        public bool isSameBook(BookData RightValue, bool WithMiddleName, bool IsFB2Author) {
            if (!IsFB2Author) {
                // Для Авторов книги
                return RightValue != null
				    ? isSameAuthors(this.Authors, RightValue.Authors, WithMiddleName, IsFB2Author)
				    : false;
            } else {
                // Для Авторов fb2 файла
                return RightValue != null
                    ? isSameAuthors(this.FB2Authors, RightValue.FB2Authors, WithMiddleName, IsFB2Author)
                    : false;
            }
		}

        /// <summary>
        /// Одни и те же ли Авторы в обоих книги (их число и ФИО) true, если авторы одинаковые (но могут идти в разном порядке)
        /// формирование списка строк с ФИО Авторов, которые есть в обоих списках Авторов (Intersection)
        /// </summary>
        /// <param name="authors1">1-й Список Авторов книги для сравнения</param>
        /// <param name="authors2">2-й Список Авторов книги для сравнения</param>
        /// <param name="WithMiddleName">учитывать Отчество Автора (true)</param>
        /// <param name="IsFB2Author">Автор книги (false) или Автора fb2 файла (true)</param>
        public bool isSameAuthors(IList<Author> authors1, IList<Author> authors2, bool WithMiddleName, bool IsFB2Author) {
			if (authors1 != null && authors2 != null)
				if (authors1.Count != authors2.Count)
					return false;
			// если авторы одинаковые (но могут идти в разном порядке), то возвращается true
			return makeListFOIAuthors(authors1, WithMiddleName,  IsFB2Author).Except(
				makeListFOIAuthors(authors2, WithMiddleName, IsFB2Author), new FB2EqualityComparer()
			).ToList().Count == 0;
		}

        /// <summary>
        /// Формирование списка из строк ФИО каждого Автора из Authors
        /// </summary>
        /// <param name="authors">Авторы книги для сравнения</param>
        /// <param name="WithMiddleName">Учитывать Отчество Автора (true)</param>
        /// <param name="IsFB2Author">Автор книги (false) или Автора fb2 файла (true)</param>
        public List<string> makeListFOIAuthors(IList<Author> authors, bool WithMiddleName, bool IsFB2Author) {
			List<string> list = new List<string>();
			if (authors == null) {
                list.Add(IsFB2Author ? m_FB2AuthorNotExist : m_AuthorNotExist);
				return list;
			}
            for (int i = 0; i != authors.Count; ++i) {
                bool AuthorExist = true;
				StringBuilder fio = new StringBuilder();
				if (authors[i].LastName != null && !string.IsNullOrWhiteSpace(authors[i].LastName.Value))
					fio.Append(authors[i].LastName.Value.Trim());
				if (authors[i].FirstName != null && !string.IsNullOrWhiteSpace(authors[i].FirstName.Value)) {
					fio.Append(" ");
					fio.Append(authors[i].FirstName.Value.Trim());
				}
				if (WithMiddleName) {
					if (authors[i].MiddleName != null && !string.IsNullOrWhiteSpace(authors[i].MiddleName.Value)) {
						fio.Append(" ");
						fio.Append(authors[i].MiddleName.Value.Trim());
					}
				}
                if (authors[i].NickName != null && !string.IsNullOrWhiteSpace(authors[i].NickName.Value)){
                    fio.Append(" ");
                    fio.Append(authors[i].NickName.Value.Trim());
                }

                bool MiddleNameExist = false;
				if (WithMiddleName) {
					if (authors[i].MiddleName != null && !string.IsNullOrWhiteSpace(authors[i].MiddleName.Value))
						MiddleNameExist = true;
				}
				bool LastNameExist = false;
				if (authors[i].LastName != null && !string.IsNullOrWhiteSpace(authors[i].LastName.Value))
					LastNameExist = true;
				bool FirstNameExist = false;
				if (authors[i].FirstName != null && !string.IsNullOrWhiteSpace(authors[i].FirstName.Value))
					FirstNameExist = true;
                bool NickNameExist = false;
                if (authors[i].NickName != null && !string.IsNullOrWhiteSpace(authors[i].NickName.Value))
                    NickNameExist = true;

                if (WithMiddleName) {
					if (!LastNameExist && !FirstNameExist && !MiddleNameExist && !NickNameExist)
						AuthorExist = false;
				} else {
					if (!LastNameExist && !FirstNameExist && !NickNameExist)
						AuthorExist = false;
				}

				string s = fio.ToString();
				if (!AuthorExist) {
					if (IsFB2Author)
						s = m_FB2AuthorNotExist;
					else
						s = m_AuthorNotExist;
				}

				if (!IsFB2Author) {
					if (s != m_AuthorNotExist) {
						if (!list.Contains(s))
							list.Add(s);
					} else
						list.Add(s + (i > 0 ? i.ToString() : string.Empty));
				} else {
					if (s != m_FB2AuthorNotExist) {
						if (!list.Contains(s))
							list.Add(s);
					} else
						list.Add(s + (i > 0 ? i.ToString() : string.Empty));
				}
			}
			list.Sort(); // чтобы учесть одинаковые книги, где Авторы переставлены местами
			return list;
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

        public virtual IList<Author> FB2Authors
        {
            get { return m_FB2Authors; }
            set { m_FB2Authors = value; }
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
