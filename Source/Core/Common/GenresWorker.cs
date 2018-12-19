/*
 * Сделано в SharpDevelop.
 * Пользователь: Вадим Кузнецов (DikBSD)
 * Дата: 22.07.2015
 * Время: 7:37
 * 
 */
using System;
using System.IO;
using System.Xml.Linq;

using Core.FB2.Genres;

namespace Core.Common
{
	/// <summary>
	/// Работа с Жанрами
	/// </summary>
	public class GenresWorker
	{
		public GenresWorker()
		{
		}
		
		// название жанра по его коду
		public static string cyrillicGenreName( string GenreCodes, ref FB2UnionGenres fb2g ) {
			if( GenreCodes.IndexOf(';') != -1 ) {
				string ret = string.Empty;
				string[] Codes = GenreCodes.Split(';');
				foreach( string code in Codes ) {
					string  Name = fb2g.GetFBGenreName( code.Trim() );
					ret += ( string.IsNullOrEmpty( Name ) ? "?" : Name ) + "; ";
					ret.Trim();
				}
				string ReturnValue = ret.Substring(
					0, ret.LastIndexOf( ";", StringComparison.CurrentCultureIgnoreCase )
				).Trim();
				
				return ReturnValue.IndexOf(
					";", StringComparison.CurrentCultureIgnoreCase
				) != 0 ? ReturnValue : GenreCodes;
			}
			string RetValue = fb2g.GetFBGenreName( GenreCodes );
			return !string.IsNullOrEmpty( RetValue ) ? RetValue : GenreCodes;
		}
		
		// название жанра (код)
		public static string cyrillicGenreNameAndCode( string GenreCodes, ref FB2UnionGenres fb2g ) {
			if( GenreCodes.IndexOf(';') != -1 ) {
				string ret = string.Empty;
				string[] Codes = GenreCodes.Split(';');
				foreach( string code in Codes ) {
					string  Name = fb2g.GetFBGenreName( code.Trim() );
					ret += ( string.IsNullOrEmpty( Name ) ? "?" : Name ) + " (" + code.Trim() + ")" + "; ";
					ret.Trim();
				}
				return ret.Substring(
					0, ret.LastIndexOf( ";", StringComparison.CurrentCultureIgnoreCase )
				).Trim();
			}
			return fb2g.GetFBGenreName( GenreCodes ) + " (" + GenreCodes + ")";
		}
		
	}
}
