/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 9:53
 * 
 * License: GPL 2.1
 */
using System;

namespace Core.FB2.Description.TitleInfo
{
	/// <summary>
	/// Description of Genre.
	/// </summary>
	public class Genre
	{
		#region Закрытые данные класса
		private string 	m_sName		= null;
		private uint 	m_unMath	= 100;
		#endregion
		
		#region Конструкторы класса
		public Genre()
		{
			m_sName 	= null;
			m_unMath 	= 100;
		}
		public Genre( string sName, uint unMath )
		{
			m_sName	= !string.IsNullOrEmpty(sName) ? sName.Trim() : null;
			if( unMath < 0 ) {
				m_unMath = 0;
			} else if( unMath > 100 ) {
				m_unMath = 100;
			} else {
				m_unMath = unMath;
			}
		}
		public Genre( string sName )
		{
			m_sName		= !string.IsNullOrEmpty(sName) ? sName.Trim() : null;
			m_unMath 	= 100;
		}
		#endregion
		
		#region Открытые методы класса
		public bool isSameGenre(Genre RightValue) {
			if ( this==null && RightValue==null )
				return true;
			if ( ( this==null && RightValue!=null ) || ( this!=null && RightValue==null ) )
				return false;

			return this.Name == RightValue.Name
				&& this.Math == RightValue.Math;
		}
		#endregion
		
		#region Открытые свойства класса - fb2-элементы
		public virtual string Name {
			get { return !string.IsNullOrEmpty(m_sName) ? m_sName.Trim() : null; }
			set { m_sName = !string.IsNullOrEmpty(value) ? value.Trim() : value; }
		}

		public virtual uint Math {
			get { return m_unMath; }
			set { m_unMath = value; }
		}
		#endregion
	}
}
