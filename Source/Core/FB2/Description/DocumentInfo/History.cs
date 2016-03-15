/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 12:44
 * 
 * License: GPL 2.1
 */
using System;

using Core.FB2.Description.TitleInfo;

namespace Core.FB2.Description.DocumentInfo
{
	/// <summary>
	/// Description of History.
	/// </summary>
	public class History : Annotation
	{
		#region Конструкторы класса
		public History()
		{
		}
		public History( string sText, string sId, string sLang ) :
			base( sText.Trim(), sId.Trim(), sLang.Trim() )
        {
        }
		public History( string sText, string sId ) :
			base( sText.Trim(), sId.Trim() )
        {
        }
		#endregion
	}
}
