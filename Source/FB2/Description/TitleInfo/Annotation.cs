/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 11:14
 * 
 * License: GPL 2.1
 */
using System;
using FB2.Common;

namespace FB2.Description.TitleInfo
{
	/// <summary>
	/// Description of Annotation.
	/// </summary>
	public class Annotation : IAnnotation
	{
		#region Закрытые данные класса
        private string m_sText	= "";
		private string m_sId	= "";
        private string m_sLang	= "";
        #endregion
		
		#region Конструкторы класса
        public Annotation()
		{
		}
		public Annotation( string sText, string sId, string sLang )
        {
            m_sText	= sText;
			m_sId	= sId;
            m_sLang	= sLang;
        }
		public Annotation( string sText, string sId )
        {
            m_sText	= sText;
			m_sId	= sId;
			m_sLang	= "";
        }
        #endregion
		
        #region Открытые свойства класса - атрибуты fb2-элементов
		public virtual string Id {
            get { return m_sId; }
            set { m_sId = value; }
        }

        public virtual string Lang {
            get { return m_sLang; }
            set { m_sLang = value; }
        }
        #endregion
        
        #region Открытые свойства класса - элементы fb2-элементов
        public virtual string Value {
            get { return m_sText; }
            set { m_sText = value; }
        }
        #endregion
	}
}
