/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 11:14
 * 
 * License: GPL 2.1
 */
using System;
using System.Globalization;
using FB2.Common;

namespace FB2.Description.TitleInfo
{
	/// <summary>
	/// Description of Annotation.
	/// </summary>
	public class Annotation : IAttrID, IAttrLang
	{
		#region Закрытые данные класса
		private string m_sId;
        private CultureInfo m_ciLang = null;
        #endregion
		
		#region Конструкторы класса
        public Annotation()
		{
		}
		public Annotation( string sId, CultureInfo ciLang )
        {
            m_sId		= sId;
            m_ciLang	= ciLang;
        }
        #endregion
		
        #region Открытые свойства класса -атрибуты fb2-элементов
		public virtual string AttrID {
            get { return m_sId; }
            set { m_sId = value; }
        }

        public virtual CultureInfo AttrLang {
            get { return m_ciLang; }
            set { m_ciLang = value; }
        }
        #endregion
	}
}
