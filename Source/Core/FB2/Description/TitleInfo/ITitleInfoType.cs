/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 29.04.2009
 * Time: 12:43
 * 
 * License: GPL 2.1
 */
using System;
using System.Collections.Generic;

using Core.FB2.Description.Common;

namespace Core.FB2.Description.TitleInfo
{
	/// <summary>
	/// Description of ITitleInfoType.
	/// </summary>
	public interface ITitleInfoType
	{
		IList<Genre> Genres { get; set; }
        IList<Author> Authors { get; set; }
        BookTitle BookTitle { get; set; }
        Annotation Annotation { get; set; }
        Keywords Keywords { get; set; }
        Date Date { get; set; }
        Coverpage Coverpage { get; set; }
        string Lang { get; set; }
        string SrcLang { get; set; }
        IList<Author> Translators { get; set; }
        IList<Sequence> Sequences { get; set; }
	}
}
