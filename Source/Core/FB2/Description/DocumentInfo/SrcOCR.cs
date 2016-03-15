/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 14:55
 * 
 * License: GPL 2.1
 */
using System;

using Core.FB2.Description.Common;

namespace Core.FB2.Description.DocumentInfo
{
	/// <summary>
	/// Description of SrcOCR.
	/// </summary>
	public class SrcOCR : TextFieldType
	{
		#region Конструкторы класса
		public SrcOCR()
		{
		}
		public SrcOCR( string sValue, string sLang ) :
			base( sValue.Trim(), sLang.Trim() )
        {
        }
		public SrcOCR( string sValue ) :
			base( sValue.Trim() )
        {
        }
		#endregion
	}
}