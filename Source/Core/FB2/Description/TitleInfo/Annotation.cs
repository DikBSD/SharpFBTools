/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 11:14
 * 
 * License: GPL 2.1
 */
using System;

namespace Core.FB2.Description.TitleInfo
{
	/// <summary>
	/// Description of Annotation.
	/// </summary>
	public class Annotation : IAnnotationType
	{
		#region Закрытые данные класса
        private string m_sValue	= null;
		private string m_sId	= null;
        private string m_sLang	= null;
        #endregion
		
		#region Конструкторы класса
        public Annotation()
		{
		}
		public Annotation( string sValue, string sId, string sLang )
        {
            m_sValue	= !string.IsNullOrEmpty(sValue) ? sValue.Trim() : null;
            m_sId		= !string.IsNullOrEmpty(sId) ? sId.Trim() : null;
            m_sLang		= !string.IsNullOrEmpty(sLang) ? sLang.Trim() : null;
        }
		public Annotation( string sValue, string sId )
        {
            m_sValue	= !string.IsNullOrEmpty(sValue) ? sValue.Trim() : null;
            m_sId		= !string.IsNullOrEmpty(sId) ? sId.Trim() : null;
			m_sLang		= null;
        }
        #endregion
        
        #region Открытые свойства класса - атрибуты fb2-элементов
		public virtual string Id {
            get { return !string.IsNullOrEmpty(m_sId) ? m_sId.Trim() : null; }
            set { m_sId = !string.IsNullOrEmpty(value) ? value.Trim() : value; }
        }

        public virtual string Lang {
            get { return !string.IsNullOrEmpty(m_sLang) ? m_sLang.Trim() : null; }
            set { m_sLang = !string.IsNullOrEmpty(value) ? value.Trim() : value; }
        }
        #endregion
        
        #region Открытые свойства класса - элементы fb2-элементов
        public virtual string Value {
            get { return !string.IsNullOrEmpty(m_sValue) ? m_sValue.Trim() : null; }
            set { m_sValue = !string.IsNullOrEmpty(value) ? value.Trim() : value; }
        }
        #endregion
	}
}
