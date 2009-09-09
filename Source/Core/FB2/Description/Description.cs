/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 15:44
 * 
 * License: GPL 2.1
 */
using System;
using System.Collections.Generic;

using Core.FB2.Description.TitleInfo;
using Core.FB2.Description.DocumentInfo;
using Core.FB2.Description.PublishInfo;
using Core.FB2.Description.CustomInfo;

namespace Core.FB2.Description
{
	/// <summary>
	/// Description of Description.
	/// </summary>
	public class Description
	{
		#region Закрытые данные класса
        private TitleInfo.TitleInfo				m_TitleInfo		= null;
        private TitleInfo.TitleInfo				m_ScrTitleInfo	= null;
        private DocumentInfo.DocumentInfo		m_DocumentInfo	= null;
        private PublishInfo.PublishInfo			m_PublishInfo	= null;
        private IList<CustomInfo.CustomInfo>	m_CustomInfo	= null;
        #endregion
        
		#region Конструкторы класса
		public Description()
		{
		}
		public Description( TitleInfo.TitleInfo titleInfo, TitleInfo.TitleInfo scrTitleInfo,
		                   DocumentInfo.DocumentInfo documentInfo, PublishInfo.PublishInfo publishInfo,
		                   IList<CustomInfo.CustomInfo> customInfo )
        {
            m_TitleInfo		= titleInfo;
            m_ScrTitleInfo	= scrTitleInfo;
            m_DocumentInfo	= documentInfo;
            m_PublishInfo	= publishInfo;
            m_CustomInfo	= customInfo;
        }
		#endregion
		
		#region Открытые свойства класса - fb2-элементы
		public virtual TitleInfo.TitleInfo TitleInfo {
            get { return m_TitleInfo; }
            set { m_TitleInfo = value; }
        }

        public virtual TitleInfo.TitleInfo ScrTitleInfo {
            get { return m_ScrTitleInfo; }
            set { m_ScrTitleInfo = value; }
        }

        public virtual DocumentInfo.DocumentInfo DocumentInfo {
            get { return m_DocumentInfo; }
            set { m_DocumentInfo = value; }
        }

        public virtual PublishInfo.PublishInfo PublishInfo {
            get { return m_PublishInfo; }
            set { m_PublishInfo = value; }
        }

        public virtual IList<CustomInfo.CustomInfo> CustomInfo {
            get { return m_CustomInfo; }
            set { m_CustomInfo = value; }
        }

		#endregion
	}
}
