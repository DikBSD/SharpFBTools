/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 11:44
 * 
 * License: GPL 2.1
 */
using System;
using System.Globalization;
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
        private CultureInfo m_ciLang = null;
        #endregion
        
        #region Конструкторы класса
		public Date()
		{
		}
		public Date( string sValue, string sText, CultureInfo ciLang )
        {
            m_sValue	= sValue;
            m_sText		= sText;
            m_ciLang	= ciLang;
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
		public virtual string AttrValue {
            get { return m_sValue; }
            set { m_sValue = value; }
        }

        public virtual CultureInfo AttrLang {
            get { return m_ciLang; }
            set { m_ciLang = value; }
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
