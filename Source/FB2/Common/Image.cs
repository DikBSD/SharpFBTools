/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 16:48
 * 
 * License: GPL 2.1
 */
using System;

namespace FB2.Common
{
	/// <summary>
	/// Description of Image.
	/// </summary>
	public class Image
	{
		#region Закрытые данные класса
		private string m_sValue;
		private string m_sType;
		private string m_sHref;
		private string m_sAlt;
		#endregion
		
		#region Конструкторы класса
		public Image()
		{
		}
		public Image( string sValue, string sType, string sHref, string sAlt )
        {
            m_sValue	= sValue;
            m_sType		= sType;
        	m_sHref		= sHref;
        	m_sAlt		= sAlt;
        }
		public Image( string sValue, string sType, string sHref )
        {
            m_sValue	= sValue;
            m_sType		= sType;
        	m_sHref		= sHref;
        }
		#endregion
		
		#region Открытые свойства класса - атрибуты fb2-элементов
		public virtual string AttrType {
            get { return m_sType; }
            set { m_sType = value; }
        }
		public virtual string AttrHref {
            get { return m_sHref; }
            set { m_sHref = value; }
        }
		public virtual string AttrAlt {
            get { return m_sAlt; }
            set { m_sAlt = value; }
        }
		#endregion
		
		#region Открытые свойства класса - fb2-элементы
		public virtual string Value {
            get { return m_sValue; }
            set { m_sValue = value; }
        }
		#endregion
	}
}
