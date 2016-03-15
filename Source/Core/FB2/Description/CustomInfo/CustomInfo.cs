/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 15:02
 * 
 * License: GPL 2.1
 */
using System;

using Core.FB2.Description.Common;

namespace Core.FB2.Description.CustomInfo
{
	/// <summary>
	/// Description of CustomInfo.
	/// </summary>
	public class CustomInfo : TextFieldType
	{
		#region Закрытые данные класса
		private string m_sInfoType	= null;
		#endregion
		
		#region Конструкторы класса
		public CustomInfo()
		{
		}
		public CustomInfo( string sValue, string sInfoType, string sLang ) :
			base( sValue, sLang )
        {
			m_sInfoType	= sInfoType.Trim();
        }
		public CustomInfo(string sValue, string sInfoType) :
			base( sValue )
        {
            m_sInfoType = sInfoType.Trim();
        }
		public CustomInfo( string sInfoType ) :
			base( "", "" )
        {
			m_sInfoType	= sInfoType.Trim();
        }
		#endregion
		
		#region Открытые свойства класса - атрибуты fb2-элементов
		public virtual string InfoType {
            get { return m_sInfoType.Trim(); }
            set { m_sInfoType = value.Trim(); }
        }
		#endregion
	}
}
