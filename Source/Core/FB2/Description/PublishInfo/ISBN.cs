/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 14:47
 * 
 * License: GPL 2.1
 */
using System;

using Core.FB2.Description.Common;

namespace Core.FB2.Description.PublishInfo
{
	/// <summary>
	/// Description of ISBN.
	/// </summary>
	public class ISBN : TextFieldType
	{
		#region Конструкторы класса
		public ISBN()
		{
		}
		public ISBN( string sValue, string sLang ) :
			base( sValue.Trim(), sLang.Trim() )
        {
        }
		public ISBN( string sValue ) :
			base( sValue.Trim() )
        {
        }
		#endregion
	}
}
