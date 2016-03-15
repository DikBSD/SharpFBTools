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

using Core.FB2.Description.Common;

namespace Core.FB2.Description.DocumentInfo
{
	/// <summary>
	/// Description of DocumentInfo.
	/// </summary>
	public class DocumentInfo
	{
		#region Закрытые данные класса
        private IList<Author>	m_Authors		= null;
        private ProgramUsed		m_ProgramUsed	= null;
        private Date			m_Date			= null;
        private IList<string>	m_SrcUrls		= null;
        private SrcOCR			m_sSrcOCR		= null;
        private string			m_sID			= null;
        private string			m_sVersion		= null;
        private History 		m_History		= null;
        #endregion
        
		#region Конструкторы класса
        public DocumentInfo()
        {
        }
        public DocumentInfo( IList<Author> authors, ProgramUsed programUsed, Date date, IList<string> srcUrls, SrcOCR srcOcr,
                            string sID, string sVersion, History history )
        {
            m_Authors		= authors;
            m_ProgramUsed	= programUsed;
            m_Date			= date;
            m_SrcUrls		= srcUrls;
            m_sSrcOCR		= srcOcr;
            m_sID			= !string.IsNullOrEmpty(sID) ? sID.Trim() : null;
            m_sVersion		= !string.IsNullOrEmpty(sVersion) ? sVersion.Trim() : null;
            m_History		= history;
        }
        public DocumentInfo(IList<Author> authors, Date date, string sID, string sVersion)
        {
            m_Authors	= authors;
            m_Date		= date;
            m_sID		= !string.IsNullOrEmpty(sID) ? sID.Trim() : null;
            m_sVersion	= !string.IsNullOrEmpty(sVersion) ? sVersion.Trim() : null;
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
            get { return !string.IsNullOrEmpty(m_sID) ? m_sID.Trim() : null; }
			set { m_sID = !string.IsNullOrEmpty(value) ? value.Trim() : value; }
        }

        public virtual string Version
        {
            get { return !string.IsNullOrEmpty(m_sVersion) ? m_sVersion.Trim() : null; }
			set { m_sVersion = !string.IsNullOrEmpty(value) ? value.Trim() : value; }
        }

        public virtual History History
        {
            get { return m_History; }
            set { m_History = value; }
        }
        
        public virtual void SetNewID()
        {
            m_sID = Guid.NewGuid().ToString();
        }
        #endregion
	}
}
