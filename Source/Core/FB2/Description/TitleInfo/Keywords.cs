/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 11:26
 * 
 * License: GPL 2.1
 */
using System;

using Core.FB2.Description.Common;

namespace Core.FB2.Description.TitleInfo
{
	/// <summary>
	/// Description of Keywords.
	/// </summary>
	public class Keywords : TextFieldType
	{
		#region Конструкторы класса
		public Keywords()
		{
		}
		public Keywords( string sValue, string sLang ) :
			base( sValue, sLang )
        {
        }
		public Keywords( string sValue ) :
			base( sValue )
        {
        }
		#endregion
	}
}
