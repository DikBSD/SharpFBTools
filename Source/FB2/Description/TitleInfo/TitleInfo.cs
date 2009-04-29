/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 12:07
 * 
 * License: GPL 2.1
 */
using System;
using System.Collections.Generic;
using FB2.Description.Common;

namespace FB2.Description.TitleInfo
{
	/// <summary>
	/// Description of TitleInfo.
	/// </summary>
	public class TitleInfo : ITitleInfoType
	{
		#region Закрытые данные класса
        private IList<Genre>	m_Genres;
		private IList<Author>	m_Authors;
        private BookTitle		m_BookTitle;
		private Annotation		m_Annotation;
        private Keywords		m_Keywords;
		private Date			m_Date;
		private Coverpage		m_Coverpage;
        private string			m_Lang;
        private string			m_ScrLang;
        private IList<Author>	m_Translators;
        private IList<Sequence>	m_Sequences;
        #endregion
        
		#region Конструкторы класса
        public TitleInfo()
		{
		}
		public TitleInfo( IList<Genre> genres, IList<Author> authors, BookTitle bookTitle, Annotation annotation,
                         Keywords keywords, Date date, Coverpage coverpage, string lang, string scrLang,
                         IList<Author> translators, IList<Sequence> sequences )
        {
            m_Genres = genres;
            m_Authors = authors;
            m_BookTitle = bookTitle;
            m_Annotation = annotation;
            m_Keywords = keywords;
            m_Date = date;
            m_Coverpage	= coverpage;
            m_Lang = lang;
            m_ScrLang = scrLang;
            m_Translators = translators;
            m_Sequences = sequences;
        }
        #endregion
        
        #region Открытые свойства класса - fb2-элементы
         public virtual IList<Genre> Genres
        {
            get { return m_Genres; }
            set { m_Genres = value; }
        }

        public virtual IList<Author> Authors
        {
            get { return m_Authors; }
            set { m_Authors = value; }
        }

        public virtual BookTitle BookTitle
        {
            get { return m_BookTitle; }
            set { m_BookTitle = value; }
        }

        public virtual Annotation Annotation
        {
            get { return m_Annotation; }
            set { m_Annotation = value; }
        }

        public virtual Keywords Keywords
        {
            get { return m_Keywords; }
            set { m_Keywords = value; }
        }

        public virtual Date Date
        {
            get { return m_Date; }
            set { m_Date = value; }
        }

        public virtual Coverpage Coverpage
        {
            get { return m_Coverpage; }
            set { m_Coverpage = value; }
        }
         
        public virtual string Lang
        {
            get { return m_Lang; }
            set { m_Lang = value; }
        }

        public virtual string ScrLang
        {
            get { return m_ScrLang; }
            set { m_ScrLang = value; }
        }

        public virtual IList<Author> Translators
        {
            get { return m_Translators; }
            set { m_Translators = value; }
        }

        public virtual IList<Sequence> Sequences
        {
            get { return m_Sequences; }
            set { m_Sequences = value; }
        }
        #endregion
	}
}
