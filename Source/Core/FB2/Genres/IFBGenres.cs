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
	/// IFBGenres: интерфейс для разных видов жанров
	/// </summary>
	public interface IFBGenres
	{
		string GetFBGenreName( string GenreCode );
		string GetFBGenreGroup( string GenreCode );
		string[] GetFBGenreNamesArray();
		string[] GetFBGenreCodesArray();
		List<string> GetFBGenresForGroup( string GenreGroup );
	}
}
