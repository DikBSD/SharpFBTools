/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 07.10.2009
 * Time: 9:53
 * 
 * License: GPL 2.1
 */
using System;
using System.Collections.Generic;

namespace Core.FB2Dublicator
{
	/// <summary>
	/// класс для хранения информации по одинаковым книгам (контекст md5 Книги )
	/// </summary>
	public class FB2FilesDataMd5List : List<BookData> {
		
		#region Закрытые данные класса
		private string	m_sMd5 = null;
		#endregion
		
		#region конструкторы
		public FB2FilesDataMd5List() {

		}
		#endregion
		
		#region Открытые методы класса
		public void AddBookData( BookData abt ) {
			this.Add( abt );
        }
		#endregion
		
		#region Свойства класса
		public virtual string Md5 {
			get { return m_sMd5; }
			set { m_sMd5 = value; }
        }
		#endregion
	}
}
