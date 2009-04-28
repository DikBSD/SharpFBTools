/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 22:06
 * 
 * License: GPL 2.1
 */
using System;
using System.Globalization;

namespace FB2.Common
{
	/// <summary>
	/// Description of ITextField.
	/// </summary>
	public interface ITextField
    {
        string Value { get; set; }
        CultureInfo Lang { set; get; }
    }
}
