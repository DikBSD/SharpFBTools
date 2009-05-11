/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 9:28
 * 
 * License: GPL 2.1
 */
using System;
using FB2.Common;

namespace FB2.Description.Common
{
	/// <summary>
	/// Description of Sequence.
	/// </summary>
	public class Sequence : ISequenceType
	{
		#region Закрытые данные класса
        private string	m_sName		= null;
        private string	m_sNumber	= null;
        private string	m_sLang		= null;
		#endregion
		
		#region Конструкторы класса
		public Sequence()
		{
			m_sName		= null;
			m_sLang		= null;
			m_sNumber	= null;
		}
		public Sequence( string sName, string sNumber, string sLang )
        {
			m_sName		= sName;
			m_sNumber	= sNumber;
			m_sLang		= sLang;
        }
        public Sequence( string sName )
        {
            m_sName 	= sName;
			m_sLang		= null;
			m_sNumber	= null;
        }
		#endregion
		
		#region Открытые свойства класса - атрибуты fb2-элементов
		public virtual string Lang {
            get { return m_sLang; }
            set { m_sLang = value; }
        }
		#endregion
		
		#region Открытые свойства класса - fb2-элементы
		public virtual string Name {
            // Название Серии
			get { return m_sName; }
            set { m_sName = value; }
        }

        public virtual string Number {
			// Номер Серии
            get { return m_sNumber; }
            set { m_sNumber = value; }
        }
		#endregion
	}
}
