/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 14:52
 * 
 * License: GPL 2.1
 */
using System;

using Core.FB2.Description.Common;

namespace Core.FB2.Description.PublishInfo
{
	/// <summary>
	/// Description of BookName.
	/// </summary>
	public class BookName : TextFieldType
	{
		#region Конструкторы класса
		public BookName()
		{
		}
		public BookName( string sValue, string sLang ) :
			base(
				!string.IsNullOrEmpty(sValue) ? sValue.Trim() : null,
				!string.IsNullOrEmpty(sLang) ? sLang.Trim() : null
			)
        {
        }
		public BookName( string sValue ) :
			base( !string.IsNullOrEmpty(sValue) ? sValue.Trim() : null )
        {
        }
		#endregion
	}
}
