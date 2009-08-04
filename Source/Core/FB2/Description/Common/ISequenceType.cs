/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 29.04.2009
 * Time: 11:08
 * 
 * License: GPL 2.1
 */
using System;

namespace Core.FB2.Description.Common
{
	/// <summary>
	/// Description of ISequenceType.
	/// </summary>
	public interface ISequenceType
	{
		string Name { get; set; }	// атрибут типа
        string Number { set; get; } // атрибут типа
        string Lang { set; get; }	// атрибут типа
	}
}
