/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 14:49
 * 
 * License: GPL 2.1
 */
using System;

using Core.FB2.Description.Common;

namespace Core.FB2.Description.PublishInfo
{
	/// <summary>
	/// Description of Publisher.
	/// </summary>
	public class Publisher : TextFieldType
	{
		#region Конструкторы класса
		public Publisher()
		{
		}
		public Publisher( string sValue, string sLang ) :
			base( sValue.Trim(), sLang.Trim() )
        {
        }
		public Publisher( string sValue ) :
			base( sValue.Trim() )
        {
        }
		#endregion
	}
}
