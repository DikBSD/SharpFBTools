/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 11:44
 * 
 * License: GPL 2.1
 */
using System;

namespace Core.FB2.Description.Common
{
	/// <summary>
	/// Description of Date.
	/// </summary>
	public class Date : IDateType
	{
		#region Закрытые данные класса
		private string m_sText	= null;
		private string m_sValue	= null;
        private string m_sLang	= null;
        #endregion
        
        #region Конструкторы класса
		public Date()
		{
		}
		public Date( string sText, string sValue, string sLang )
        {
			m_sText		= !string.IsNullOrEmpty(sText) ? sText.Trim() : null;
			m_sValue	= !string.IsNullOrEmpty(sValue) ? sValue.Trim() : null;
            m_sLang		= !string.IsNullOrEmpty(sLang) ? sLang.Trim() : null;
        }
		public Date( string sText, string sValue )
        {
            m_sText		= !string.IsNullOrEmpty(sText) ? sText.Trim() : null;
            m_sValue	= !string.IsNullOrEmpty(sValue) ? sValue.Trim() : null;
        }
		public Date( string sText )
        {
            m_sText		= !string.IsNullOrEmpty(sText) ? sText.Trim() : null;
        }
		#endregion
		
		#region Открытые свойства класса - атрибуты fb2-элементов
		public virtual string Value {
			get { return !string.IsNullOrEmpty(m_sValue) ? m_sValue.Trim() : null; }
			set { m_sValue = !string.IsNullOrEmpty(value) ? value.Trim() : value; }
        }

        public virtual string Lang {
            get { return !string.IsNullOrEmpty(m_sLang) ? m_sLang.Trim() : null; }
            set { m_sLang = !string.IsNullOrEmpty(value) ? value.Trim() : value; }
        }
        #endregion
        
		#region Открытые свойства класса - элементы fb2-элементов
        public virtual string Text {
            get { return !string.IsNullOrEmpty(m_sText) ? m_sText.Trim() : null; }
            set { m_sText = !string.IsNullOrEmpty(value) ? value.Trim() : value; }
        }
		#endregion
	}
}
