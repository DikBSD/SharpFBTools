/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 14.05.2009
 * Time: 12:46
 * 
 * License: GPL 2.1
 */
using System;
using System.Collections.Generic;

namespace Core.FB2.Genres
{
	/// <summary>
	/// Description of IFBGenres.
	/// </summary>
	public interface IFBGenres
	{
		string GetFBGenreName( string sGenreCode );
		string GetFBGenreGroup( string sGenreCode );
		string[] GetFBGenreNamesArray();
		string[] GetFBGenreCodesArray();
		List<string> GetFBGenresForGroup( string sGGroup );
	}
}
