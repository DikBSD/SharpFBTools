/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 14:58
 * 
 * License: GPL 2.1
 */
using System;
using FB2.Description.Common;

namespace FB2.Description.DocumentInfo
{
	/// <summary>
	/// Description of ProgramUsed.
	/// </summary>
	public class ProgramUsed : TextFieldType
	{
		#region Конструкторы класса
		public ProgramUsed()
		{
		}
		public ProgramUsed( string sValue, string sLang ) :
			base( sValue, sLang )
        {
        }
		public ProgramUsed( string sValue ) :
			base( sValue )
        {
        }
		#endregion
	}
}
