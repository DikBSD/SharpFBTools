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
using FB2.Description.Common;

namespace FB2.Description.TitleInfo
{
	/// <summary>
	/// Description of Annotation.
	/// </summary>
	public class Annotation : IAnnotationType
	{
		#region Закрытые данные класса
        private string m_sValue	= null;
		private string m_sId	= null;
        private string m_sLang	= null;
        #endregion
		
		#region Конструкторы класса
        public Annotation()
		{
		}
		public Annotation( string sValue, string sId, string sLang )
        {
            m_sValue	= sValue;
			m_sId		= sId;
            m_sLang		= sLang;
        }
		public Annotation( string sValue, string sId )
        {
            m_sValue	= sValue;
			m_sId		= sId;
			m_sLang		= null;
        }
        #endregion
		
        #region Открытые Вспомогательные методы класса
		public virtual bool Equals( Annotation a ) {
			if ( a.GetType() == typeof( Annotation ) ) {
				if( ( Value == ( ( Annotation )a ).Value ) &&
        		   	( Id == ( ( Annotation )a ).Id ) &&
        		  	( Lang == ( ( Annotation )a ).Lang ) ) {
					return true;
				} else {
					return false;
				}
			} else {
				return false;
			}
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
            get { return m_sValue; }
            set { m_sValue = value; }
        }
        #endregion
	}
}
