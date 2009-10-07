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
	/// класс для хранения информации по одинаковым книгам (контекст ID Книги )
	/// </summary>
	public class FB2FilesDataIDList : List<BookData> {
		
		#region Закрытые данные класса
		private string	m_sId = null;
		#endregion
		
		#region конструкторы
		public FB2FilesDataIDList() {

		}
		#endregion
		
		#region Открытые методы класса
		public void AddBookData( BookData abt ) {
			this.Add( abt );
        }
		#endregion
		
		#region Свойства класса
		public virtual string Id {
			get { return m_sId; }
			set { m_sId = value; }
        }
		#endregion
	}
}
