/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 11:44
 * 
 * License: GPL 2.1
 */
using System;
using FB2.Common;

namespace FB2.Description.Common
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
            m_sText		= sText;
			m_sValue	= sValue;
            m_sLang		= sLang;
        }
		public Date( string sText, string sValue )
        {
            m_sText		= sText;
            m_sValue	= sValue;
        }
		public Date( string sText )
        {
            m_sText	= sText;
        }
		#endregion
		
		#region Открытые Вспомогательные методы класса
		public virtual bool Equals( Date d )
        {
			if ( d.GetType() == typeof( Date ) ) {
				if( ( Text == ( ( Date )d ).Text ) &&
				   	( Value == ( ( Date )d ).Value ) &&
				   	( Lang == ( ( Date )d ).Lang ) ) {
					return true;
				} else {
					return false;
				}
			} else {
				return false;
			}
        }
		#endregion
		
		#region Открытые свойства класса - атрибуты fb2-элементов
		public virtual string Value {
            get { return m_sValue; }
            set { m_sValue = value; }
        }

        public virtual string Lang {
            get { return m_sLang; }
            set { m_sLang = value; }
        }
        #endregion
        
		#region Открытые свойства класса - элементы fb2-элементов
        public virtual string Text {
            get { return m_sText; }
            set { m_sText = value; }
        }
		#endregion
	}
}
