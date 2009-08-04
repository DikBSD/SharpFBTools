/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 14:33
 * 
 * License: GPL 2.1
 */
using System;

using Core.FB2.Description.Common;

namespace Core.FB2.Description.PublishInfo
{
	/// <summary>
	/// Description of City.
	/// </summary>
	public class City : TextFieldType
	{
		#region Конструкторы класса
		public City()
		{
		}
		public City( string sValue, string sLang ) :
			base( sValue, sLang )
        {
        }
		public City( string sValue ) :
			base( sValue )
        {
        }
		#endregion
	}
}
