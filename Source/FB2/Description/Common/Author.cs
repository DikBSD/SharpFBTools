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

namespace FB2.Description.Common
{
	/// <summary>
	/// Description of Author.
	/// </summary>
	public class Author : IComparable
	{
		#region Закрытые данные класса
        private string m_sFirstName;
        private string m_sMiddleName;
        private string m_sLastName;
        private string m_sNickName;
        private IList<string> m_lisHomePages;
        private IList<string> m_lisEmails;
        private string m_sID;
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
		
		#region Конструкторы класса
		public Author()
		{
		}
		#endregion
		
		#region Открытые свойства-fb2-элементы класса
		
		#endregion
	}
}
