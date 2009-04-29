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
	public class Date : IAttrValue, IAttrLang
	{
		#region Закрытые данные класса
		private string m_sText;
		private string m_sValue;
        private string m_sLang;
        #endregion
        
        #region Конструкторы класса
		public Date()
		{
		}
		public Date( string sValue, string sText, string sLang )
        {
            m_sValue	= sValue;
            m_sText		= sText;
            m_sLang	= sLang;
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
