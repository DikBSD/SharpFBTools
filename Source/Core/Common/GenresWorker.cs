/*
 * Сделано в SharpDevelop.
 * Пользователь: Вадим Кузнецов (DikBSD)
 * Дата: 22.07.2015
 * Время: 7:37
 * 
 */
using System;

using Core.FB2.Genres;

namespace Core.Common
{
	/// <summary>
	/// GenresWorker: работа с Жанрами
	/// </summary>
	public class GenresWorker
	{
		public GenresWorker()
		{
		}
		
		// название жанра по его коду
		public static string cyrillicGenreName( string GenreCodes, ref IFBGenres fb2g ) {
			if( GenreCodes.IndexOf(';') != -1 ) {
				string ret = string.Empty;
				string[] Codes = GenreCodes.Split(';');
				foreach( string code in Codes ) {
					string  Name = fb2g.GetFBGenreName( code.Trim() );
					ret += ( string.IsNullOrEmpty( Name ) ? "?" : Name ) + "; ";
					ret.Trim();
				}
				string ReturnValue = ret.Substring( 0, ret.LastIndexOf( ";" ) ).Trim();
				return ReturnValue.IndexOf( ";" ) != 0 ? ReturnValue : GenreCodes;
			}
			string RetValue = fb2g.GetFBGenreName( GenreCodes );
			return !string.IsNullOrEmpty( RetValue ) ? RetValue : GenreCodes;
		}
		
		// название жанра (код)
		public static string cyrillicGenreNameAndCode( string GenreCodes, ref IFBGenres fb2g ) {
			if( GenreCodes.IndexOf(';') != -1 ) {
				string ret = string.Empty;
				string[] Codes = GenreCodes.Split(';');
				foreach( string code in Codes ) {
					string  Name = fb2g.GetFBGenreName( code.Trim() );
					ret += ( string.IsNullOrEmpty( Name ) ? "?" : Name ) + " (" + code.Trim() + ")" + "; ";
					ret.Trim();
				}
				return ret.Substring( 0, ret.LastIndexOf( ";" ) ).Trim();
			}
			return fb2g.GetFBGenreName( GenreCodes ) + " (" + GenreCodes + ")";
		}
		
		// спиок жанров, в зависимости от схемы Жанров
		public static IFBGenres genresListOfGenreSheme( bool IsFB2Librusec, ref IGenresGroup GenresGroup ) {
			IFBGenres fb2g = null;
			if( IsFB2Librusec )
				fb2g = new FB2LibrusecGenres( ref GenresGroup );
			else
				fb2g = new FB22Genres( ref GenresGroup );
			return fb2g;
		}
	}
}
