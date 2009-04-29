/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 15:02
 * 
 * License: GPL 2.1
 */
using System;
using FB2.Common;

namespace FB2.Description.CustomInfo
{
	/// <summary>
	/// Description of CustomInfo.
	/// </summary>
	public class CustomInfo : ITextFieldType
	{
		#region Закрытые данные класса
		private string m_sValue		= "";
		private string m_sInfoType	= "";
		private string m_sLang		= "";
		#endregion
		
		#region Конструкторы класса
		public CustomInfo()
		{
			m_sValue	= "";
			m_sInfoType	= "";
        	m_sLang		= "";
		}
		public CustomInfo( string sValue, string sInfoType, string sLang )
        {
            m_sValue	= sValue;
			m_sInfoType	= sInfoType;
        	m_sLang		= sLang;
        }
        public CustomInfo(string sValue, string sInfoType)
        {
            m_sValue	= sValue;
            m_sInfoType = sInfoType;
            m_sLang		= "";
        }
		public CustomInfo( string sInfoType )
        {
            m_sValue	= "";
			m_sInfoType	= sInfoType;
            m_sLang		= "";
        }
		#endregion
		
		#region Открытые свойства класса - атрибуты fb2-элементов
		public virtual string InfoType {
            get { return m_sInfoType; }
            set { m_sInfoType = value; }
        }
		
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
