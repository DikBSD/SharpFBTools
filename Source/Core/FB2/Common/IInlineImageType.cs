/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 29.04.2009
 * Time: 10:48
 * 
 * License: GPL 2.1
 */
using System;

namespace Core.FB2.Common
{
	/// <summary>
	/// Description of IInlineImageType.
	/// </summary>
	public interface IInlineImageType
	{
        string Type { set; get; } 	// атрибут типа
        string Href { set; get; } 	// атрибут типа
        string Alt { set; get; } 	// атрибут типа
	}
}
