/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 27.04.2009
 * Time: 17:38
 * 
 * License: GPL 2.1
 */
using System;
using System.Text;
using System.Collections.Generic;
using System.Security.Cryptography;

using System.Windows.Forms;

using Core.FB2.Common;

namespace Core.FB2.Description.Common
{
	/// <summary>
	/// Description of Author.
	/// </summary>
	public class Author : IAuthorType
	{
		#region Закрытые данные класса
		private TextFieldType	m_tFirstName	= null;
		private TextFieldType	m_tMiddleName	= null;
		private TextFieldType	m_tLastName		= null;
		private TextFieldType	m_tNickName		= null;
		private IList<string>	m_ilsHomePages	= null;
		private IList<string>	m_ilsEmails		= null;
		private string			m_sID			= null;
		#endregion

		#region Конструкторы класса
		public Author()
		{
		}
		// все данные об Авторе
		public Author( TextFieldType tFirstName, TextFieldType tMiddleName, TextFieldType tLastName, TextFieldType tNickName,
		              IList<string> ilsHomePages, IList<string> ilsEmails, string sID )
		{
			m_tFirstName	= tFirstName;
			m_tMiddleName	= tMiddleName;
			m_tLastName		= tLastName;
			m_tNickName		= tNickName;
			m_ilsHomePages	= ilsHomePages;
			m_ilsEmails		= ilsEmails;
			m_sID			= sID;
		}
		// все данные об Авторе без его id
		public Author( TextFieldType tFirstName, TextFieldType tMiddleName, TextFieldType tLastName, TextFieldType tNickName,
		              IList<string> ilsHomePages, IList<string> ilsEmails )
		{
			m_tFirstName	= tFirstName;
			m_tMiddleName	= tMiddleName;
			m_tLastName		= tLastName;
			m_tNickName		= tNickName;
			m_ilsHomePages	= ilsHomePages;
			m_ilsEmails		= ilsEmails;
		}
		// только ФИО Ник Автора
		public Author( TextFieldType tFirstName, TextFieldType tMiddleName, TextFieldType tLastName, TextFieldType tNickName )
		{
			m_tFirstName	= tFirstName;
			m_tMiddleName	= tMiddleName;
			m_tLastName		= tLastName;
			m_tNickName		= tNickName;
		}
		// только ФИО Автора
		public Author( TextFieldType tFirstName, TextFieldType tMiddleName, TextFieldType tLastName )
		{
			m_tFirstName	= tFirstName;
			m_tMiddleName	= tMiddleName;
			m_tLastName		= tLastName;
		}
		// только Имя и Фамилия Автора
		public Author( TextFieldType tFirstName, TextFieldType tLastName )
		{
			m_tFirstName	= tFirstName;
			m_tLastName		= tLastName;
		}
		// только Ник Автора
		public Author( TextFieldType tNickName )
		{
			m_tNickName = tNickName;
		}
		#endregion
		
		#region Открытые методы класса
		// сравниваются только Имени и Фамилия (во многих fb2 Отчество не указано)
		public bool isSameAuthor(Author RightValue, bool CompareAndMiddleName) {
			if ( this==null && RightValue==null )
				return true;
			if ( ( this==null && RightValue!=null ) || ( this!=null && RightValue==null ) )
				return false;
			
			StringBuilder fioThis = new StringBuilder();
			if (this.LastName != null && this.LastName.Value != null)
				fioThis.Append(this.LastName.Value.Trim());
			if (this.FirstName != null && this.FirstName.Value != null) {
				fioThis.Append(" ");
				fioThis.Append(this.FirstName.Value.Trim());
			}
			if (CompareAndMiddleName) {
				if (this.MiddleName != null && this.MiddleName.Value != null) {
					fioThis.Append(" ");
					fioThis.Append(this.MiddleName.Value.Trim());
				}
			}
			
			StringBuilder fioRight = new StringBuilder();
			if (RightValue.LastName != null && RightValue.LastName.Value != null)
				fioRight.Append(RightValue.LastName.Value.Trim());
			if (RightValue.FirstName != null && RightValue.FirstName.Value != null) {
				fioRight.Append(" ");
				fioRight.Append(RightValue.FirstName.Value.Trim());
			}
			if (CompareAndMiddleName) {
				if (RightValue.MiddleName != null && RightValue.MiddleName.Value != null) {
					fioRight.Append(" ");
					fioRight.Append(RightValue.MiddleName.Value.Trim());
				}
			}
			return fioThis.ToString().Equals(fioRight.ToString());
		}
		#endregion
		
		#region Открытые свойства - fb2-элементы класса
		public virtual TextFieldType FirstName {
			get { return m_tFirstName; }
			set { m_tFirstName = value; }
		}

		public virtual TextFieldType MiddleName {
			get { return m_tMiddleName; }
			set { m_tMiddleName = value; }
		}

		public virtual TextFieldType LastName {
			get { return m_tLastName; }
			set { m_tLastName = value; }
		}

		public virtual TextFieldType NickName {
			get { return m_tNickName; }
			set { m_tNickName = value; }
		}

		public virtual IList<string> HomePages {
			get { return m_ilsHomePages; }
			set { m_ilsHomePages = value; }
		}

		public virtual IList<string> Emails {
			get { return m_ilsEmails; }
			set { m_ilsEmails = value; }
		}

		public virtual string ID {
			get { return m_sID; }
			set {
				if( m_sID == null )
					m_sID = value;
			}
		}
		#endregion
	}
}
