/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 29.04.2009
 * Time: 14:33
 * 
 * License: GPL 2.1
 */
using System;

using Core.FB2.Common;

namespace Core.FB2.Description.Common
{
	/// <summary>
	/// Description of TextFieldType.
	/// </summary>
	public class TextFieldType : ITextFieldType
	{
		#region Закрытые данные класса
		private string m_sValue	= null;
		private string m_sLang	= null;
		#endregion
		
		#region Конструкторы класса
		public TextFieldType()
		{
			m_sValue	= null;
			m_sLang		= null;
		}
		public TextFieldType( string sValue, string sLang )
		{
			m_sValue	= sValue;
			m_sLang		= sLang;
		}
		public TextFieldType( string sValue )
		{
			m_sValue	= sValue;
		}
		#endregion
		
		#region Открытые методы класса
		// атрибут Lang не проверяется - в реальных книгах он не используется (или крайне редко)
		public bool Equals(TextFieldType RightValue) {
			if ( this==null && RightValue==null )
				return true;
			if ( ( this==null && RightValue!=null ) || ( this!=null && RightValue==null ) )
				return false;

			return this.Value==RightValue.Value;
		}
		#endregion
		
		#region Открытые свойства класса
		public virtual string Lang {
			get { return m_sLang; }
			set { m_sLang = value; }
		}
		
		public virtual string Value {
			get { return m_sValue; }
			set { m_sValue = value; }
		}
		#endregion
	}
}
