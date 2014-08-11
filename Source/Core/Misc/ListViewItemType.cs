/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 22.06.2012
 * Time: 9:28
 * 
 * License: GPL 2.1
 */
using System;

namespace Core.Misc
{
	/// <summary>
	/// Данные ListViewItemType.
	/// </summary>
	public class ListViewItemType
	{
		private string m_sItemType		= string.Empty;
		private string m_sItemTypeValue	= string.Empty;
		
		public ListViewItemType(string Type, string Value)
		{
			m_sItemType			= Type;
			m_sItemTypeValue	= Value;
		}
		
		public virtual string Type {
			get { return m_sItemType; }
			set { m_sItemType = value; }
        }
		public virtual string Value {
			get { return m_sItemTypeValue; }
			set { m_sItemTypeValue = value; }
        }
	}
}
