/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 16:56
 * 
 * License: GPL 2.1
 */
using System;
using Core.FB2.Common;

namespace Core.FB2.Description.TitleInfo
{
	/// <summary>
	/// Description of Coverpage.
	/// </summary>
	public class Coverpage
	{
		#region Закрытые данные класса
        private string m_sValue	= null;
        #endregion
        
		#region Конструкторы класса
        public Coverpage()
		{
        	m_sValue = null;
		}
        public Coverpage( string sValue )
		{
        	m_sValue = sValue;
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
