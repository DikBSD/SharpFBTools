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

using System.Windows.Forms;

namespace Core.Duplicator
{
	/// <summary>
	/// Класс для хранения информации по одинаковым книгам в одной группе
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
		/// <summary>
		/// Проверка - таже самая ли это книга:
		/// проверка производится по всем книгам в текущей Группе (в списке List)
		/// </summary>
		/// <param name="BookPath">Путь к проверяемой книге</param>
		public bool isBookExists( string BookPath ) {
			foreach ( BookData bd in this ) {
                if ( bd.Path.Trim() == BookPath.Trim() )
					return true;
			}
			return false;
		}
		
		// формирование списка строк из ФИО всех Авторов книги или Авторов fb2 файла
		public string makeAuthorsString(bool WithMiddleName, bool IsFB2Author) {
			if (this.Count > 0) {
				List<string> list = new List<string>();
				foreach ( BookData bd in this ) {
                    List<string> fioList = null;
                    fioList = !IsFB2Author ? bd.makeListFOIAuthors(bd.Authors, WithMiddleName, false)
                                           : bd.makeListFOIAuthors(bd.FB2Authors, WithMiddleName, true);
					foreach ( string fio in fioList ) {
						if (!list.Contains(fio)) {
							list.Add(fio);
							list.Add("; ");
						}
					}
				}
				
				StringBuilder sb = new StringBuilder( list.Count );
				foreach ( string s in list )
					sb.Append(s);
				
				string sA = sb.ToString().Trim();
                return sA.Substring( 0, sA.LastIndexOf( ';' ) ).Trim();
			}
			return string.Empty;
		}

        // сравнение только по названию группы (если они не одинаковые, то и содержимое групп - разное)
        public bool isSameGroup( FB2FilesDataInGroup RightValue ) {
			if ( RightValue == null )
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
