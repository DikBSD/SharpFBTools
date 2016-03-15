/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 9:28
 * 
 * License: GPL 2.1
 */
using System;

namespace Core.FB2.Description.Common
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
			m_sName		= !string.IsNullOrEmpty(sName) ? sName.Trim() : null;
			m_sNumber	= !string.IsNullOrEmpty(sNumber) ? sNumber.Trim() : null;
			m_sLang		= !string.IsNullOrEmpty(sLang) ? sLang.Trim() : null;
        }
        public Sequence( string sName )
        {
            m_sName		= !string.IsNullOrEmpty(sName) ? sName.Trim() : null;
			m_sLang		= null;
			m_sNumber	= null;
        }
		#endregion
		
		#region Открытые свойства класса - атрибуты fb2-элементов
		public virtual string Lang {
            get { return !string.IsNullOrEmpty(m_sLang) ? m_sLang.Trim() : null; }
			set { m_sLang = !string.IsNullOrEmpty(value) ? value.Trim() : value; }
        }
		#endregion
		
		#region Открытые свойства класса - fb2-элементы
		public virtual string Name {
            // Название Серии
            get { return !string.IsNullOrEmpty(m_sName) ? m_sName.Trim() : null; }
			set { m_sName = !string.IsNullOrEmpty(value) ? value.Trim() : value; }
        }

        public virtual string Number {
			// Номер Серии
            get { return !string.IsNullOrEmpty(m_sNumber) ? m_sNumber.Trim() : null; }
			set { m_sNumber = !string.IsNullOrEmpty(value) ? value.Trim() : value; }
        }
		#endregion
	}
}
