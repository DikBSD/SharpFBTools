/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 14:49
 * 
 * License: GPL 2.1
 */
using System;
using FB2.Common;

namespace FB2.Description.PublishInfo
{
	/// <summary>
	/// Description of Publisher.
	/// </summary>
	public class Publisher : ITextFieldType
	{
		#region Закрытые данные класса
		private string m_sValue	= "";
		private string m_sLang	= "";
		#endregion
		
		#region Конструкторы класса
		public Publisher()
		{
			m_sValue	= "";
        	m_sLang		= "";
		}
		public Publisher( string sValue, string sLang )
        {
            m_sValue	= sValue;
        	m_sLang		= sLang;
        }
        public Publisher( string sValue )
        {
            m_sValue	= sValue;
        	m_sLang		= "";
        }
		#endregion
		
		#region Открытые свойства класса - атрибуты fb2-элементов
		public virtual string Lang {
            get { return m_sLang; }
            set { m_sLang = value; }
        }
		#endregion
		
		#region Открытые свойства класса - элементы fb2-элементов
        public virtual string Value {
            get { return m_sValue; }
            set { m_sValue = value; }
        }
        #endregion
	}
}
