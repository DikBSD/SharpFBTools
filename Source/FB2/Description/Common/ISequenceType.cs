/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 29.04.2009
 * Time: 11:08
 * 
 * License: GPL 2.1
 */
using System;

namespace FB2.Description.Common
{
	/// <summary>
	/// Description of ISequenceType.
	/// </summary>
	public interface ISequenceType : IComparable
	{
		string Name { get; set; } // атрибут типа
        uint Number { set; get; } // атрибут типа
        string Lang { set; get; } // атрибут типа
	}
}
