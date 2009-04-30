/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 15:44
 * 
 * License: GPL 2.1
 */
using System;
using FB2.Description.TitleInfo;
using FB2.Description.DocumentInfo;
using FB2.Description.PublishInfo;
using FB2.Description.CustomInfo;

namespace FB2.Description
{
	/// <summary>
	/// Description of Description.
	/// </summary>
	public class Description
	{
		#region Закрытые данные класса
        private TitleInfo.TitleInfo			m_TitleInfo		= null;
        private TitleInfo.TitleInfo			m_ScrTitleInfo	= null;
        private DocumentInfo.DocumentInfo	m_DocumentInfo	= null;
        private PublishInfo.PublishInfo		m_PublishInfo	= null;
        private CustomInfo.CustomInfo		m_CustomInfo	= null;
        #endregion
        
		#region Конструкторы класса
		public Description()
		{
		}
		public Description( TitleInfo.TitleInfo titleInfo, TitleInfo.TitleInfo scrTitleInfo,
		                   DocumentInfo.DocumentInfo documentInfo, PublishInfo.PublishInfo publishInfo,
		                   CustomInfo.CustomInfo customInfo )
        {
            m_TitleInfo		= titleInfo;
            m_ScrTitleInfo	= scrTitleInfo;
            m_DocumentInfo	= documentInfo;
            m_PublishInfo	= publishInfo;
            m_CustomInfo	= customInfo;
        }
		#endregion
		
		#region Открытые Вспомогательные методы класса
		public virtual bool Equals( Description d )
        {
			if( d.GetType() == typeof( Description) ) {
                if( ( TitleInfo == ( ( Description )d ).TitleInfo ) && 
				   	( ScrTitleInfo == ( ( Description )d ).ScrTitleInfo ) &&
				   	( PublishInfo == ( ( Description )d ).PublishInfo ) &&
				   	( CustomInfo == ( ( Description )d ).CustomInfo ) ) {
					return true;
				} else {
					return false;
				}
        	} else {
        		return false;
        	}
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

        public virtual CustomInfo.CustomInfo CustomInfo {
            get { return m_CustomInfo; }
            set { m_CustomInfo = value; }
        }
		#endregion
	}
}
