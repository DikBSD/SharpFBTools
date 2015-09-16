/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 20.07.2015
 * Время: 8:26
 * 
 */
using System.Windows.Forms;

using Core.FB2.FB2Parsers;

namespace Core.Common
{
	/// <summary>
	/// Вспомогательные классы для правки метаданных fb2 файлов
	/// </summary>

	#region AuthorInfo
	/// <summary>
	/// AuthorInfo: Класс хранения данных редактируемого Автора/Переводчика, для передачи в форму редактирования метаданных
	/// </summary>
	public class AuthorInfo {
		#region Закрытые данные класса
		private readonly bool m_IsCreate = true;
		private readonly Enums.AuthorEnum m_AuthorEnumType;
		private string m_LastName = string.Empty;
		private string m_FirstName = string.Empty;
		private string m_MiddleName = string.Empty;
		private string m_NickName = string.Empty;
		private string m_HomePage = string.Empty;
		private string m_Email = string.Empty;
		private string m_ID = string.Empty;
		#endregion
		
		public AuthorInfo( Enums.AuthorEnum AuthorEnumType, bool IsCreate,
		                  string LastName = "", string FirstName = "", string MiddleName = "",
		                  string NickName = "", string HomePage = "", string Email = "", string ID = "" ) {
			m_IsCreate = IsCreate;
			m_AuthorEnumType = AuthorEnumType;
			
			m_LastName = LastName;
			m_FirstName = FirstName;
			m_MiddleName = MiddleName;
			m_NickName = NickName;
			m_HomePage = HomePage;
			m_Email = Email;
			m_ID = ID;
		}
		
		#region Открытые свойства
		public virtual bool IsCreate {
			get {
				return m_IsCreate;
			}
		}
		public virtual Enums.AuthorEnum AuthorType {
			get {
				return m_AuthorEnumType;
			}
		}
		public virtual string LastName {
			get {
				return m_LastName;
			}
			set {
				m_LastName = value;
			}
		}
		public virtual string FirstName {
			get {
				return m_FirstName;
			}
			set {
				m_FirstName = value;
			}
		}
		public virtual string MiddleName {
			get {
				return m_MiddleName;
			}
			set {
				m_MiddleName = value;
			}
		}
		public virtual string NickName {
			get {
				return m_NickName;
			}
			set {
				m_NickName = value;
			}
		}
		public virtual string HomePage {
			get {
				return m_HomePage;
			}
			set {
				m_HomePage = value;
			}
		}
		public virtual string Email {
			get {
				return m_Email;
			}
			set {
				m_Email = value;
			}
		}
		public virtual string ID {
			get {
				return m_ID;
			}
			set {
				m_ID = value;
			}
		}
		#endregion
	}
	#endregion
	
	#region SequenceInfo
	/// <summary>
	/// SequenceInfo: Класс хранения данных редактируемой Серии, для передачи в форму редактирования метаданных
	/// </summary>
	public class SequenceInfo {
		#region Закрытые данные класса
		private readonly bool m_IsCreate = true;
		private readonly Enums.SequenceEnum m_SequenceEnumType;
		private string m_Name = string.Empty;
		private string m_Number = string.Empty;
		#endregion
		public SequenceInfo( Enums.SequenceEnum SequenceEnumType, bool IsCreate,
		                    string Name = "", string Number = "" ) {
			m_IsCreate = IsCreate;
			m_SequenceEnumType = SequenceEnumType;
			m_Name = Name;
			m_Number = Number;
		}
		
		#region Открытые свойства
		public virtual bool IsCreate {
			get {
				return m_IsCreate;
			}
		}
		public virtual Enums.SequenceEnum SequenceType {
			get {
				return m_SequenceEnumType;
			}
		}
		public virtual string Name {
			get {
				return m_Name;
			}
			set {
				m_Name = value;
			}
		}
		public virtual string Number {
			get {
				return m_Number;
			}
			set {
				m_Number = value;
			}
		}
		#endregion
	}
	#endregion
	
	#region CustomInfoInfo
	/// <summary>
	/// CustomInfoInfo: Класс хранения данных редактируемой Дополнительной информации, для передачи в форму редактирования метаданных
	/// </summary>
	public class CustomInfoInfo {
		#region Закрытые данные класса
		private readonly bool m_IsCreate = true;
		private string m_Type = string.Empty;
		private string m_Value = string.Empty;
		#endregion
		public CustomInfoInfo( bool IsCreate, string Type = "", string Value = "" ) {
			m_IsCreate = IsCreate;
			m_Type = Type;
			m_Value = Value;
		}
		
		#region Открытые свойства
		public virtual bool IsCreate {
			get {
				return m_IsCreate;
			}
		}
		public virtual string Type {
			get {
				return m_Type;
			}
			set {
				m_Type = value;
			}
		}
		public virtual string Value {
			get {
				return m_Value;
			}
			set {
				m_Value = value;
			}
		}
		#endregion
	}
	#endregion

	#region FB2ItemInfo
	/// <summary>
	/// FB2ItemInfo: Класс хранения данных редактируемого Автора / Жанра / Языка
	/// </summary>
	public class FB2ItemInfo {
		#region Закрытые данные класса
		private readonly bool m_IsFromArhive = false;
		private readonly ListViewItem m_FB2ListViewItem = null;
		private readonly string m_FilePathSource = null;
		private readonly string m_FilePathIfFromZip = null;
		private readonly FictionBook m_fb2 = null;
		#endregion
		
		public FB2ItemInfo( ListViewItem FB2ListViewItem, string FilePathSource, string FilePathIfFromZip, bool IsFromArhive ) {
			m_FB2ListViewItem = FB2ListViewItem;
			m_FilePathSource = FilePathSource.Replace( @"\\", @"\" );
			m_FilePathIfFromZip = FilePathIfFromZip.Replace( @"\\", @"\" );
			m_IsFromArhive = IsFromArhive;
			if( IsFromArhive )
				m_fb2 = new FictionBook( FilePathIfFromZip );
			else
				m_fb2 = new FictionBook( FilePathSource );
		}
		
		#region Открытые свойства
		public virtual string FilePathSource {
			get {
				return m_FilePathSource;
			}
		}
		public virtual string FilePathIfFromZip {
			get {
				return m_FilePathIfFromZip;
			}
		}
		public virtual bool IsFromArhive {
			get {
				return m_IsFromArhive;
			}
		}
		public virtual ListViewItem FB2ListViewItem {
			get {
				return m_FB2ListViewItem;
			}
		}
		public virtual FictionBook FictionBook {
			get {
				return m_fb2;
			}
		}
		public virtual bool IsDirListViewItem {
			get {
				return ((ListViewItemType)m_FB2ListViewItem.Tag).Type == "d" ? true : false;
			}
		}
		public virtual bool IsFileListViewItem {
			get {
				return ((ListViewItemType)m_FB2ListViewItem.Tag).Type == "f" ? true : false;
			}
		}
		#endregion
	}
	#endregion
	
	#region ItemInfo
	/// <summary>
	/// Класс хранения данных для выбранного итема Списка
	/// </summary>
	public class ListViewItemInfo {
		#region Закрытые данные класса
		private readonly ListViewItem m_ListViewItem = null;
		private readonly string m_FilePathSource = null;
		#endregion
		
		public ListViewItemInfo( ListViewItem ListViewItem, string FilePathSource ) {
			m_ListViewItem = ListViewItem;
			m_FilePathSource = FilePathSource.Replace( @"\\", @"\" );
		}
		
		#region Открытые свойства
		public virtual string FilePathSource {
			get {
				return m_FilePathSource;
			}
		}
		public virtual ListViewItem ListViewItem {
			get {
				return m_ListViewItem;
			}
		}
		public virtual bool IsDirListViewItem {
			get {
				return ((ListViewItemType)m_ListViewItem.Tag).Type == "d" ? true : false;
			}
		}
		public virtual bool IsFileListViewItem {
			get {
				return ((ListViewItemType)m_ListViewItem.Tag).Type == "f" ? true : false;
			}
		}
		#endregion
	}
	#endregion
}