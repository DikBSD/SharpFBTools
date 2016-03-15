/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 15:29
 * 
 * License: GPL 2.1
 */
using System;
using System.Collections.Generic;

using Core.FB2.Description.Common;

namespace Core.FB2.Description.PublishInfo
{
	/// <summary>
	/// Description of PublishInfo.
	/// </summary>
	public class PublishInfo
	{
		#region Закрытые данные класса
        private	BookName		m_BookName	= null;
        private Publisher		m_Publisher	= null;
        private City			m_City		= null;
        private string			m_sYear		= null;
        private ISBN			m_ISBN		= null;
        private IList<Sequence> m_Sequences	= null;
        #endregion
        
		#region Конструкторы класса
        public PublishInfo()
		{
		}
        public PublishInfo( BookName bookName, Publisher publisher, City city, string sYear, ISBN isbn,
                           IList<Sequence> sequences )
        {
            m_BookName	= bookName;
            m_Publisher = publisher;
            m_City		= city;
            m_sYear		= sYear.Trim();
            m_ISBN		= isbn;
            m_Sequences	= sequences;
        }
        #endregion
        
        #region Открытые свойства класса - fb2-элементы
        public virtual BookName BookName
        {
            get { return m_BookName; }
            set { m_BookName = value; }
        }

        public virtual Publisher Publisher
        {
            get { return m_Publisher; }
            set { m_Publisher = value; }
        }

        public virtual City City
        {
            get { return m_City; }
            set { m_City = value; }
        }

        public virtual string Year
        {
            get { return m_sYear.Trim(); }
            set { m_sYear = value.Trim(); }
        }

        public virtual ISBN ISBN
        {
            get { return m_ISBN; }
            set { m_ISBN = value; }
        }

        public virtual IList<Sequence> Sequences
        {
            get { return m_Sequences; }
            set { m_Sequences = value; }
        }
        #endregion
	}
}
