/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 07.10.2009
 * Time: 9:57
 * 
 * License: GPL 2.1
 */
using System;
using System.Collections.Generic;

namespace Core.FB2Dublicator
{
	/// <summary>
	/// класс для хранения информации по одинаковым книгам (контекст Авторы и Название )
	/// </summary>
	public class FB2FilesDataABTList : List<BookData> {
		
		#region Закрытые данные класса
		private string	m_sBookTitleForKey	= null;
		#endregion
		
		#region конструкторы
		public FB2FilesDataABTList() {

		}
		#endregion
		
		#region Открытые методы класса
		public void AddBookData( BookData abt ) {
			this.Add( abt );
        }
		public BookData GetBookData( string sPath ) {
			foreach( BookData a in this ) {
				if( a.Path == sPath ) return a;
			}
			return null;
        }
		#endregion
		
		#region Свойства класса
		public virtual string BookTitleForKey {
			get { return m_sBookTitleForKey; }
			set { m_sBookTitleForKey = value; }
        }
		#endregion
	}
}
