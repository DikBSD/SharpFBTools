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
			m_id = null;
			m_contentType = null;
			m_base64String = null;
		}
		public BinaryBase64( string id, string contentType, string base64String )
		{
			m_id			= id;
			m_contentType	= contentType;
			m_base64String	= base64String;
		}
		#endregion
		
		#region Открытые свойства класса - fb2-элементы
		public virtual string id {
			get { return m_id; }
			set { m_id = value; }
		}
		public virtual string contentType {
			get { return m_contentType; }
			set { m_contentType = value; }
		}
		public virtual string base64String {
			get { return m_base64String; }
			set { m_base64String = value; }
		}
		#endregion
	}
}
