/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 9:53
 * 
 * License: GPL 2.1
 */
using System;

namespace FB2.Description.TitleInfo
{
	/// <summary>
	/// Description of Genre.
	/// </summary>
	public class Genre : IComparable
	{
		#region Закрытые данные класса
		private uint m_unMath;
        private Genres m_eName;
        #endregion
        
		#region Конструкторы класса
		public Genre()
        {
            m_eName = Genres.prose_classic;
			m_unMath = 100;
        }
		public Genre( Genres eName, uint unMath )
        {
            m_eName = eName;
            if( unMath < 0 || unMath > 100 ) {
                throw new ArgumentOutOfRangeException("math", "Атрибут Math должен иметь значение от 0 до 100");
            }
            m_unMath = unMath;
        }
		public Genre( Genres eName )
        {
            m_eName = eName;
            m_unMath = 100;
        }
        #endregion
        
        #region Открытые Вспомогательные методы класса
        public int CompareTo( object g )
        {
            if( g.GetType() == typeof( Genre) ) {
                if( ( Name == ( ( Genre )g ).Name ) && ( Math == ( ( Genre )g ).Math ) ) {
					return 0;
				} else {
					return -1;
				}
        	} else {
        		throw new ArgumentException("Объект сравнения не является Жанром.");
        	}
        }
        #endregion
        
        #region Открытые свойства класса - fb2-элементы
        public virtual Genres Name {
            get { return m_eName; }
            set { m_eName = value; }
        }

        public virtual uint Math {
            get { return m_unMath; }
            set { m_unMath = value; }
        }
        #endregion
	}
}
