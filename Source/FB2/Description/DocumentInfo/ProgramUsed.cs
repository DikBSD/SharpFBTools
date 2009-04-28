﻿/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 14:58
 * 
 * License: GPL 2.1
 */
using System;
using System.Globalization;
using FB2.Common;

namespace FB2.Description.DocumentInfo
{
	/// <summary>
	/// Description of ProgramUsed.
	/// </summary>
	public class ProgramUsed : IAttrLang
	{
		#region Закрытые данные класса
		private string m_sText;
		private CultureInfo m_ciLang;
		#endregion
		
		#region Конструкторы класса
		public ProgramUsed()
		{
			m_sText		= "";
        	m_ciLang	= null;
		}
		public ProgramUsed( string sText, CultureInfo ciLang )
        {
            m_sText		= sText;
        	m_ciLang	= ciLang;
        }
        public ProgramUsed( string sText )
        {
            m_sText		= sText;
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
        public virtual string Text {
            get { return m_sText; }
            set { m_sText = value; }
        }
        #endregion
	}
}
