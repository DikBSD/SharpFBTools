/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 15:09
 * 
 * License: GPL 2.1
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using FB2.Description.Common;

namespace FB2.Description.DocumentInfo
{
	/// <summary>
	/// Description of DocumentInfo.
	/// </summary>
	public class DocumentInfo
	{
		#region Закрытые данные класса
        private IList<Author>	m_Authors;
        private ProgramUsed		m_ProgramUsed;
        private Date			m_Date;
        private IList<string>	m_SrcUrls;
        private SrcOCR			m_sSrcOCR;
        private string			m_sID;
        private string			m_sVersion;
        private History 		m_History;
        #endregion
        
		#region Конструкторы класса
        public DocumentInfo()
        {
            m_sID = Guid.NewGuid().ToString();
        }
        public DocumentInfo( IList<Author> authors, ProgramUsed programUsed, Date date, IList<string> srcUrls, SrcOCR srcOcr,
                            string sID, string sVersion, History history )
        {
            m_Authors		= authors;
            m_ProgramUsed	= programUsed;
            m_Date		= date;
            m_SrcUrls	= srcUrls;
            m_sSrcOCR	= srcOcr;
            m_sID		= sID;
            m_sVersion	= sVersion;
            m_History	= history;
        }
        public DocumentInfo(IList<Author> authors, Date date, string sID, string sVersion)
        {
            m_Authors	= authors;
            m_Date		= date;
            m_sID		= sID;
            m_sVersion	= sVersion;
        }
        #endregion
        
        #region Открытые свойства класса - fb2-элементы
        public virtual IList<Author> Authors {
            get { return m_Authors; }
            set { m_Authors = value; }
        }

        public virtual ProgramUsed ProgramUsed {
            get { return m_ProgramUsed; }
            set { m_ProgramUsed = value; }
        }

        public virtual Date Date {
            get { return m_Date; }
            set { m_Date = value; }
        }

        public virtual IList<string> SrcUrls {
            get { return m_SrcUrls; }
            set { m_SrcUrls = value; }
        }

        public virtual SrcOCR SrcOcr {
            get { return m_sSrcOCR; }
            set { m_sSrcOCR = value; }
        }

        public virtual string ID
        {
            get { return m_sID; }
            set { m_sID = value; }
        }

        public virtual string Version
        {
            get { return m_sVersion; }
            set { m_sVersion = value; }
        }

        public virtual History History
        {
            get { return m_History; }
            set { m_History = value; }
        }
        #endregion
	}
}
