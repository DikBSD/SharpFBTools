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
        private uint	m_unNumber	= 0;
        private string	m_sLang		= null;
		#endregion
		
		#region Конструкторы класса
		public Sequence()
		{
			m_sName		= null;
			m_sLang		= null;
		}
		public Sequence( string sName, uint unNumber, string sLang )
        {
			m_sName		= sName;
			m_unNumber	= unNumber;
			m_sLang		= sLang;
        }
        public Sequence( string sName )
        {
            m_sName 	= sName;
			m_sLang		= null;
        }
		#endregion
		
		#region Открытые методы класса
		public int CompareTo( object s ) {
			if ( s.GetType() == typeof( Sequence ) ) {
				if( ( Name == ( ( Sequence )s ).Name ) && ( Number == ( ( Sequence )s ).Number ) ) {
					return 0;
				} else {
					return -1;
				}
			} else {
				throw new ArgumentException("Объект сравнения не явялется Серией.");
			}
        }
		
		public virtual bool Equals( Sequence s )
        {
			bool bThisIsNull = ( m_sName == null && m_sLang == null );
			if( bThisIsNull || s == null ) {
				return true;
			} else if( !bThisIsNull && s != null ) {
				return Name.Equals( s.Name ) &&
            			Number.Equals( s.Number ) &&
            			Lang.Equals( s.Lang );
			}
			return false;
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

        public virtual uint Number {
			// Номер Серии
            get { return m_unNumber; }
            set { m_unNumber = value; }
        }
		#endregion
	}
}
