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
            m_sText		= sText.Trim();
			m_sValue	= sValue.Trim();
            m_sLang		= sLang.Trim();
        }
		public Date( string sText, string sValue )
        {
            m_sText		= sText.Trim();
            m_sValue	= sValue.Trim();
        }
		public Date( string sText )
        {
            m_sText	= sText.Trim();
        }
		#endregion
		
		#region Открытые свойства класса - атрибуты fb2-элементов
		public virtual string Value {
            get { return m_sValue.Trim(); }
            set { m_sValue = value.Trim(); }
        }

        public virtual string Lang {
            get { return m_sLang.Trim(); }
            set { m_sLang = value.Trim(); }
        }
        #endregion
        
		#region Открытые свойства класса - элементы fb2-элементов
        public virtual string Text {
            get { return m_sText.Trim(); }
            set { m_sText = value.Trim(); }
        }
		#endregion
	}
}
