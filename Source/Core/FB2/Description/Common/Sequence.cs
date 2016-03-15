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
			m_sName		= sName.Trim();
			m_sNumber	= sNumber.Trim();
			m_sLang		= sLang.Trim();
        }
        public Sequence( string sName )
        {
            m_sName 	= sName.Trim();
			m_sLang		= null;
			m_sNumber	= null;
        }
		#endregion
		
		#region Открытые свойства класса - атрибуты fb2-элементов
		public virtual string Lang {
            get { return m_sLang.Trim(); }
            set { m_sLang = value.Trim(); }
        }
		#endregion
		
		#region Открытые свойства класса - fb2-элементы
		public virtual string Name {
            // Название Серии
			get { return m_sName.Trim(); }
            set { m_sName = value.Trim(); }
        }

        public virtual string Number {
			// Номер Серии
            get { return m_sNumber.Trim(); }
            set { m_sNumber = value.Trim(); }
        }
		#endregion
	}
}
