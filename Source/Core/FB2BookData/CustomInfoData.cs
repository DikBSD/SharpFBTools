/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 09.09.2009
 * Time: 12:36
 * 
 * License: GPL 2.1
 */
using System;

using Core.FB2.Description.CustomInfo;
	
namespace Core.FB2BookData
{
	/// <summary>
	/// Description of CustomInfoData.
	/// </summary>
	public class CustomInfoData
	{
		#region Закрытые данные класса
		private string m_sInfoType	= null;
		private string m_sValue		= null;
		private string m_sLang		= null;
		#endregion
		
		public CustomInfoData( string sInfoType, string sValue, string sLang )
		{
			m_sInfoType	= sInfoType;
			m_sValue	= sValue;
			m_sLang		= sLang;
		}
		
		public CustomInfoData( CustomInfo ci )
		{
			m_sInfoType	= ci.InfoType;
			m_sValue	= ci.Value;
			m_sLang		= ci.Lang;
		}
		
		#region Свойства класса
		public virtual string InfoType {
			get {
				return m_sInfoType;
			}
        }
		
		public virtual string Value {
			get {
				return m_sValue;
			}
        }
		
		public virtual string Lang {
			get {
				return m_sLang;
			}
        }
		#endregion
	}
}
