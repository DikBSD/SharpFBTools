/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 21.10.2013
 * Time: 13:00
 * 
 * License: GPL 2.1
 */
using System;

namespace Core.FB2.Binary
{
	/// <summary>
	/// Description of BinaryBase64.
	/// </summary>
	public class BinaryBase64
	{
		#region Закрытые данные класса
		private string m_id				= null;
		private string m_contentType	= null;
		private string m_base64String	= null;
		#endregion
		
		#region Конструкторы класса
		public BinaryBase64()
		{
			m_id			= null;
			m_contentType	= null;
			m_base64String	= null;
		}
		public BinaryBase64( string id, string contentType, string base64String )
		{
			m_id			= !string.IsNullOrEmpty(id) ? id.Trim() : null;
			m_contentType	= !string.IsNullOrEmpty(contentType) ? contentType.Trim() : null;
			m_base64String	= !string.IsNullOrEmpty(base64String) ? base64String.Trim() : null;
		}
		#endregion
		
		#region Открытые свойства класса - fb2-элементы
		public virtual string id {
			get { return !string.IsNullOrEmpty(m_id) ? m_id.Trim() : null; }
			set { m_id = !string.IsNullOrEmpty(value) ? value.Trim() : value; }
		}
		public virtual string contentType {
			get { return !string.IsNullOrEmpty(m_contentType) ? m_contentType.Trim() : null; }
			set { m_contentType = !string.IsNullOrEmpty(value) ? value.Trim() : value; }
		}
		public virtual string base64String {
			get { return !string.IsNullOrEmpty(m_base64String) ? m_base64String.Trim() : null; }
			set { m_base64String = !string.IsNullOrEmpty(value) ? value.Trim() : value; }
		}
		#endregion
	}
}
