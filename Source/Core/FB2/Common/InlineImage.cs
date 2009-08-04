/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 16:48
 * 
 * License: GPL 2.1
 */
using System;

namespace Core.FB2.Common
{
	/// <summary>
	/// Description of Image.
	/// </summary>
	public class InlineImage : IInlineImageType
	{
		#region Закрытые данные класса
		private string m_sType;
		private string m_sHref;
		private string m_sAlt;
		#endregion
		
		#region Конструкторы класса
		public InlineImage()
		{
		}
		public InlineImage( string sType, string sHref, string sAlt )
        {
            m_sType		= sType;
        	m_sHref		= sHref;
        	m_sAlt		= sAlt;
        }
		public InlineImage( string sType, string sHref )
        {
            m_sType		= sType;
        	m_sHref		= sHref;
        }
		public InlineImage( string sHref )
        {
        	m_sHref		= sHref;
        }
		#endregion
		
		#region Открытые свойства класса - атрибуты fb2-элементов
		public virtual string Type {
            get { return m_sType; }
            set { m_sType = value; }
        }
		public virtual string Href {
            get { return m_sHref; }
            set { m_sHref = value; }
        }
		public virtual string Alt {
            get { return m_sAlt; }
            set { m_sAlt = value; }
        }
		#endregion
	}
}
