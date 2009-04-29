/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 29.04.2009
 * Time: 14:09
 * 
 * License: GPL 2.1
 */
using System;
using System.Collections.Generic;

namespace FB2.Description.Common
{
	/// <summary>
	/// Description of IAuthorType.
	/// </summary>
	public interface IAuthorType
	{
		string FirstName { get; set; }
        string MiddleName { set; get; }
        string LastName { get; set; }
        string NickName { set; get; }
        IList<string> HomePages { set; get; }
        IList<string> Emails { set; get; }
        string ID { set; get; }
	}
}
