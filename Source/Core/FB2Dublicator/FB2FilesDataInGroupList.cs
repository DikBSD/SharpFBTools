/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 07.10.2009
 * Time: 9:57
 * 
 * License: GPL 2.1
 */
using System;
using System.Text;
using System.Collections.Generic;

//using System.Windows.Forms;

namespace Core.FB2Dublicator
{
	/// <summary>
	/// класс для хранения информации по одинаковым книгам в одной группе
	/// </summary>
	public class FB2FilesDataInGroup : List<BookData> {
		
		#region Закрытые данные класса
		private string m_sGroup = null;
		#endregion
		
		#region конструкторы
		public FB2FilesDataInGroup() {
		}
		public FB2FilesDataInGroup( BookData abt ) {
			this.Add( abt );
		}
		public FB2FilesDataInGroup( BookData abt, string Group ) {
			this.Add( abt );
			m_sGroup = Group;
		}
		public FB2FilesDataInGroup( string Group ) {
			m_sGroup = Group;
		}
		#endregion
		
		#region Открытые методы класса
		public void AddBookData( BookData abt ) {
			this.Add( abt );
		}
		
		public bool isBookExists( string BookPath ) {
			foreach( BookData bd in this ) {
				if( bd.Path.Trim() == BookPath.Trim() )
					return true;
			}
			return false;
		}
		
		// формирование списка строк из ФИО всех Авторов книги
		public string makeAutorsString(bool WithMiddleName) {
			if( this==null )
				return "Тег <authors> в книге отсутствует";
			
			if (this.Count > 0) {
				List<string> list = new List<string>();
				foreach( BookData bd in this ) {
					if( bd.Authors == null )
						return "Тег <authors> в книге отсутствует";
					List<string> fioList = bd.makeListFOIAuthors(bd.Authors, WithMiddleName);
					foreach( string fio in fioList ) {
						if (!list.Contains(fio)) {
							list.Add(fio);
							list.Add("; ");
						}
					}
				}
				
				StringBuilder sb = new StringBuilder(list.Count);
				foreach( string s in list )
					sb.Append(s);
				
				string sA = sb.ToString().Trim();
				return sA.Substring( 0, sA.LastIndexOf( ";" ) ).Trim();
			} else
				return "";
		}
		
		// сравнение только по названию группы (если они не одинаковые, то и содержимое групп - разное)
		public bool isSameGroup(FB2FilesDataInGroup RightValue) {
			if ( this==null && RightValue==null )
				return true;
			if ( ( this==null && RightValue!=null ) || ( this!=null && RightValue==null ) )
				return false;
			
			return this.Group == RightValue.Group;
		}
		#endregion
		
		#region Свойства класса
		public virtual string Group {
			get { return m_sGroup; }
			set { m_sGroup = value; }
		}
		#endregion
	}
}
