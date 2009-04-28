/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 11:05
 * 
 * License: GPL 2.1
 */
using System;
using System.Globalization;

namespace FB2.Common
{
	/// <summary>
	/// Description of IAttrLang.
	/// </summary>
	public interface IAttrLang
	{
		/// <summary>
        /// Language.
        /// </summary>
        CultureInfo AttrLang { set; get; }
	}
}
