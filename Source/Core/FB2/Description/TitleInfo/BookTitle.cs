﻿/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 11:36
 * 
 * License: GPL 2.1
 */
using System;
using Core.FB2.Description.Common;

namespace Core.FB2.Description.TitleInfo
{
	/// <summary>
	/// Description of BookTitle.
	/// </summary>
	public class BookTitle : TextFieldType
	{
		#region Конструкторы класса
		public BookTitle()
		{
		}
		public BookTitle( string sValue, string sLang ) :
			base(
				!string.IsNullOrEmpty(sValue) ? sValue.Trim() : null,
				!string.IsNullOrEmpty(sLang) ? sLang.Trim() : null
			)
        {
        }
		public BookTitle( string sValue ) :
			base( !string.IsNullOrEmpty(sValue) ? sValue.Trim() : null )
        {
        }
		#endregion
	}
}
