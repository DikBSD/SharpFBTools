/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 9:28
 * 
 * License: GPL 2.1
 */
using System;

namespace FB2.Description.Common
{
	/// <summary>
	/// Description of Sequence.
	/// </summary>
	public class Sequence : IComparable
	{
		#region Закрытые данные класса
        private string m_sName;
        private uint m_unNumber;
		#endregion
		
		#region Конструкторы класса
		public Sequence()
		{
		}
		public Sequence( string sName, uint unNumber )
        {
			m_sName		= sName;
			m_unNumber	= unNumber;
        }
        public Sequence( string sName )
        {
            m_sName = sName;
        }
		#endregion
		
		#region Открытые Вспомогательные методы класса
		public int CompareTo( object o ) {
            if ( o.GetType() != typeof( Sequence ) ) {
                throw new ArgumentException("the object type is not Sequence.");
            }
            return
                ( Name == ( ( Sequence )o ).Name ) &&
                ( Number == ( ( Sequence )o ).Number ) ? 0 : -1;
        }
		#endregion
		
		#region Открытые свойства-fb2-элементы класса
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
