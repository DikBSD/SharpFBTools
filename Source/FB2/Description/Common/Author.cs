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
	public class Author : IAuthorType, IComparable
	{
		#region Закрытые данные класса
        private TextFieldType	m_sFirstName	= null;
        private TextFieldType	m_sMiddleName	= null;
        private TextFieldType	m_sLastName		= null;
        private TextFieldType	m_sNickName		= null;
        private IList<string> m_lisHomePages = null;
        private IList<string> m_lisEmails	 = null;
        private string	m_sID			 = "";
		#endregion
		
		#region Конструкторы класса
		public Author()
		{
		}
		public Author( TextFieldType sFirstName, TextFieldType sMiddleName, TextFieldType sLastName, TextFieldType sNickName,
		              IList<string> lisHomePages, IList<string> lisEmails, string sID )
        {
            // все данные об Авторе
			m_sFirstName	= sFirstName;
            m_sMiddleName	= sMiddleName;
            m_sLastName		= sLastName;
            m_sNickName		= sNickName;
            m_lisHomePages	= lisHomePages;
            m_lisEmails		= lisEmails;
            m_sID			= sID;
        }
		public Author( TextFieldType sFirstName, TextFieldType sMiddleName, TextFieldType sLastName, TextFieldType sNickName,
		              IList<string> lisHomePages, IList<string> lisEmails )
        {
            // все данные об Авторе без его id
			m_sFirstName	= sFirstName;
            m_sMiddleName	= sMiddleName;
            m_sLastName		= sLastName;
            m_sNickName		= sNickName;
            m_lisHomePages	= lisHomePages;
            m_lisEmails		= lisEmails;
        }
		public Author( TextFieldType sFirstName, TextFieldType sMiddleName, TextFieldType sLastName, string sID )
        {
            // только ФИО Автора
			m_sFirstName	= sFirstName;
            m_sMiddleName	= sMiddleName;
            m_sLastName		= sLastName;
        }
		public Author( TextFieldType sFirstName, TextFieldType sLastName )
        {
            // только Имя и Фамилия Автора
			m_sFirstName	= sFirstName;
            m_sLastName		= sLastName;
        }
		public Author( TextFieldType sNickName )
        {
            // только Ник Автора
			m_sNickName = sNickName;
        }
		#endregion
		
		#region Закрытые Вспомогательные методы класса
		private string CalculateMD5Hash( string sString ) {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] hash = md5.ComputeHash( Encoding.UTF8.GetBytes( sString ) );
            StringBuilder sb = new StringBuilder();
            for( int i=0; i!=hash.Length; ++i) {
                sb.Append( hash[i].ToString( "x2" ) );
            }
            return sb.ToString();
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
		
		#endregion
		
		#region Открытые свойства-fb2-элементы класса
		public virtual TextFieldType FirstName {
            get { return m_sFirstName; }
            set { m_sFirstName = value; }
        }

        public virtual TextFieldType MiddleName {
            get { return m_sMiddleName; }
            set { m_sMiddleName = value; }
        }

        public virtual TextFieldType LastName {
            get { return m_sLastName; }
            set { m_sLastName = value; }
        }

        public virtual TextFieldType NickName {
            get { return m_sNickName; }
            set { m_sNickName = value; }
        }

        public virtual IList<string> HomePages {
            get { return m_lisHomePages; }
            set { m_lisHomePages = value; }
        }

        public virtual IList<string> Emails {
            get { return m_lisEmails; }
            set { m_lisEmails = value; }
        }

        public virtual string ID {
            get {
                if( m_sID == null ) {
                    m_sID = CalculateMD5Hash( String.Format("{0} {1} {2} {3}", FirstName, MiddleName, LastName, NickName ).ToLower() );
                }
                return m_sID;
            } set {
                if( m_sID == null ) {
                    m_sID = value;
                }
            }
        }
		#endregion
	}
}
