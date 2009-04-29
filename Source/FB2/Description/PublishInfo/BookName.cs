/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 14:52
 * 
 * License: GPL 2.1
 */
using System;
using FB2.Common;

namespace FB2.Description.PublishInfo
{
	/// <summary>
	/// Description of BookName.
	/// </summary>
	public class BookName : IAttrLang
	{
		#region Закрытые данные класса
		private string m_sText	= "";
		private string m_sLang	= "";
		#endregion
		
		#region Конструкторы класса
		public BookName()
		{
			m_sText	= "";
        	m_sLang	= "";
		}
		public BookName( string sText, string sLang )
        {
            m_sText	= sText;
        	m_sLang	= sLang;
        }
        public BookName( string sText )
        {
            m_sText	= sText;
        	m_sLang	= "";
        }
		#endregion
		
		#region Открытые свойства класса - атрибуты fb2-элементов
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
