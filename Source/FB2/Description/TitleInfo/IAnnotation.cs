﻿/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 22:10
 * 
 * * License: GPL 2.1
 */
using System;
using FB2.Common;

namespace FB2.Description.TitleInfo
{
	/// <summary>
	/// Description of IAnnotation.
	/// </summary>
	public interface IAnnotation : ITextField
    {
        string Id { set; get; }
    }
}