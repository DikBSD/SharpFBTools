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
		private string m_sType = null;
		private string m_sHref = null;
		private string m_sAlt = null;
		#endregion
		
		#region Конструкторы класса
		public InlineImage()
		{
		}
		public InlineImage( string sType, string sHref, string sAlt )
        {
            m_sType		= !string.IsNullOrEmpty(sType) ? sType.Trim() : null;
            m_sHref		= !string.IsNullOrEmpty(sHref) ? sHref.Trim() : null;
            m_sAlt		= !string.IsNullOrEmpty(sAlt) ? sAlt.Trim() : null;
        }
		public InlineImage( string sType, string sHref )
        {
            m_sType		= !string.IsNullOrEmpty(sType) ? sType.Trim() : null;
        	m_sHref		= !string.IsNullOrEmpty(sHref) ? sHref.Trim() : null;
        }
		public InlineImage( string sHref )
        {
        	m_sHref		= !string.IsNullOrEmpty(sHref) ? sHref.Trim() : null;
        }
		#endregion
		
		#region Открытые свойства класса - атрибуты fb2-элементов
		public virtual string Type {
            get { return !string.IsNullOrEmpty(m_sType) ? m_sType.Trim() : null; }
			set { m_sType = !string.IsNullOrEmpty(value) ? value.Trim() : value; }
        }
		public virtual string Href {
            get { return !string.IsNullOrEmpty(m_sHref) ? m_sHref.Trim() : null; }
			set { m_sHref = !string.IsNullOrEmpty(value) ? value.Trim() : value; }
        }
		public virtual string Alt {
            get { return !string.IsNullOrEmpty(m_sAlt) ? m_sAlt.Trim() : null; }
			set { m_sAlt = !string.IsNullOrEmpty(value) ? value.Trim() : value; }
        }
		#endregion
	}
}
