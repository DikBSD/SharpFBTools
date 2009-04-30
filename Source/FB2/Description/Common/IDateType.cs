/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 29.04.2009
 * Time: 11:59
 * 
 * License: GPL 2.1
 */
using System;

namespace FB2.Description.Common
{
	/// <summary>
	/// Description of IDateType.
	/// </summary>
	public interface IDateType : IComparable
	{
		string Text { set; get; } 	// значение типа
		string Value { get; set; }	// атрибут типа
        string Lang { set; get; } 	// атрибут типа
	}
}
