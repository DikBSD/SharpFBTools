/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 15:02
 * 
 * License: GPL 2.1
 */
using System;
using System.Globalization;
using FB2.Common;

namespace FB2.Description.CustomInfo
{
	/// <summary>
	/// Description of CustomInfo.
	/// </summary>
	public class CustomInfo : IAttrLang
	{
		#region Закрытые данные класса
		private string m_sValue;
		private string m_sInfoType;
		private CultureInfo m_ciLang;
		#endregion
		
		#region Конструкторы класса
		public CustomInfo()
		{
			m_sValue	= "";
			m_sInfoType	= "";
        	m_ciLang	= null;
		}
		public CustomInfo( string sValue, string sInfoType, CultureInfo ciLang )
        {
            m_sValue	= sValue;
			m_sInfoType	= sInfoType;
        	m_ciLang	= ciLang;
        }
        public CustomInfo(string sValue, string sInfoType)
        {
            m_sValue	= sValue;
            m_sInfoType = sInfoType;
        }
		public CustomInfo( string sInfoType )
        {
            m_sInfoType	= sInfoType;
        	m_ciLang	= null;
        }
		#endregion
		
		#region Открытые свойства класса - атрибуты fb2-элементов
		public virtual CultureInfo AttrLang {
            get { return m_ciLang; }
            set { m_ciLang = value; }
        }
		#endregion
		
		#region Открытые свойства класса - элементы fb2-элементов
        public virtual string sInfoType {
            get { return m_sInfoType; }
            set { m_sInfoType = value; }
        }
        #endregion
	}
}
