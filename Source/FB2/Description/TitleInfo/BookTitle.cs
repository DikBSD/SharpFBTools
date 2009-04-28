/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 11:36
 * 
 * License: GPL 2.1
 */
using System;
using System.Globalization;
using FB2.Common;

namespace FB2.Description.TitleInfo
{
	/// <summary>
	/// Description of BookTitle.
	/// </summary>
	public class BookTitle : IAttrLang
	{
		#region Закрытые данные класса
        private string m_sText;
		private CultureInfo m_ciLang = null;
        #endregion
        
		#region Конструкторы класса
        public BookTitle()
        {
            m_sText		= "";
        	m_ciLang	= null;
        }
        public BookTitle( string sText, CultureInfo ciLang )
        {
            m_sText		= sText;
        	m_ciLang	= ciLang;
        }
        public BookTitle( CultureInfo ciLang )
        {
        	m_ciLang	= ciLang;
        }
        #endregion
        
        #region Открытые свойства класса - атрибуты fb2-элементов
        public virtual CultureInfo AttrLang {
            get { return m_ciLang; }
            set { m_ciLang = value; }
        }
        #endregion
        
        #region Открытые свойства класса - элементы fb2-элементов
        public virtual string Text {
            get { return m_sText; }
            set { m_sText = value; }
        }
        #endregion
	}
}
