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
using FB2.Common;

namespace FB2.Description.Common
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
		public Author( TextFieldType tFirstName, TextFieldType tMiddleName, TextFieldType tLastName, TextFieldType tNickName,
		              IList<string> ilsHomePages, IList<string> ilsEmails, string sID )
        {
            // все данные об Авторе
			m_tFirstName	= tFirstName;
            m_tMiddleName	= tMiddleName;
            m_tLastName		= tLastName;
            m_tNickName		= tNickName;
            m_ilsHomePages	= ilsHomePages;
            m_ilsEmails		= ilsEmails;
            m_sID			= sID;
        }
		public Author( TextFieldType tFirstName, TextFieldType tMiddleName, TextFieldType tLastName, TextFieldType tNickName,
		              IList<string> ilsHomePages, IList<string> ilsEmails )
        {
            // все данные об Авторе без его id
			m_tFirstName	= tFirstName;
            m_tMiddleName	= tMiddleName;
            m_tLastName		= tLastName;
            m_tNickName		= tNickName;
            m_ilsHomePages	= ilsHomePages;
            m_ilsEmails		= ilsEmails;
        }
		public Author( TextFieldType tFirstName, TextFieldType tMiddleName, TextFieldType tLastName, string sID )
        {
            // только ФИО Автора
			m_tFirstName	= tFirstName;
            m_tMiddleName	= tMiddleName;
            m_tLastName		= tLastName;
        }
		public Author( TextFieldType tFirstName, TextFieldType tLastName )
        {
            // только Имя и Фамилия Автора
			m_tFirstName	= tFirstName;
            m_tLastName		= tLastName;
        }
		public Author( TextFieldType tNickName )
        {
            // только Ник Автора
			m_tNickName = tNickName;
        }
		#endregion
				
		#region Открытые методы класса
		public int CompareTo( object a ) {
            if( a.GetType() == typeof( Author ) ) {
                if( ID == ( (Author)a ).ID ) {
					return 0;
				} else {
					return -1;
				}
			} else {
				throw new ArgumentException("Объект сравнения не явялется Автором.");
			}
        }
		
		public int CompareFull( object a ) {
            if( a.GetType() == typeof( Author ) ) {
                if( FirstName == ( (Author)a ).FirstName &&
                	MiddleName == ( (Author)a ).MiddleName &&
                	LastName == ( (Author)a ).LastName &&
                	NickName == ( (Author)a ).NickName &&
                	HomePages == ( (Author)a ).HomePages &&
                	Emails == ( (Author)a ).Emails &&
                	ID == ( (Author)a ).ID ) {
					return 0;
				} else {
					return -1;
				}
			} else {
				throw new ArgumentException("Объект сравнения не явялется Автором.");
			}
        }
		
		public virtual bool Equals( Author a )
        {
			bool bThisIsNull = ( m_tFirstName == null && m_tMiddleName == null && m_tLastName == null && 
			                  	m_tNickName == null && m_ilsHomePages == null && m_ilsEmails == null &&
			                 	m_sID == null );
			if( bThisIsNull || a == null ) {
				return true;
			} else if( !bThisIsNull && a != null ) {
				return FirstName.Equals( a.FirstName ) &&
            			MiddleName.Equals( a.MiddleName ) &&
            			LastName.Equals( a.LastName ) &&
						NickName.Equals( a.NickName ) &&
          		  		HomePages.Equals( a.HomePages ) &&
           		 		Emails.Equals( a.Emails ) &&
						ID.Equals( a.ID );
			}
			return false;
        }
		
		public virtual bool EqualsFromID( Author a )
        {
			if( m_sID != null && a != null) {
				return ID.Equals( a.ID );
			}
			return false;
        }
		#endregion
		
		#region Открытые свойства-fb2-элементы класса
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
                if( m_sID == null ) {
                    m_sID = value;
                }
            }
        }
		#endregion
	}
}
