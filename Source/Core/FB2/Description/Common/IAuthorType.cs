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

using Core.FB2.Common;

namespace Core.FB2.Description.Common
{
	/// <summary>
	/// Description of IAuthorType.
	/// </summary>
	public interface IAuthorType
	{
		TextFieldType FirstName { get; set; }
        TextFieldType MiddleName { set; get; }
        TextFieldType LastName { get; set; }
        TextFieldType NickName { set; get; }
        IList<string> HomePages { set; get; }
        IList<string> Emails { set; get; }
        string ID { set; get; }
	}
}
