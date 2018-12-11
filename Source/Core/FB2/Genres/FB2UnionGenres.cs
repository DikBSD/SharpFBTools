/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 23.09.2013
 * Time: 13:15
 * 
 * License: GPL 2.1
 */
using System;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Xml.Linq;

using Core.Common;

using stringProcessing = Core.Common.StringProcessing;

namespace Core.FB2.Genres
{
	/// <summary>
	/// Объединенные Жанры fb2.1 + Либрусек + Флибуста
	/// </summary>
	public class FB2UnionGenres
	{
		private readonly MultiDictionary<KeyValuePair<string, string>> _mdFB2GenresWithGroups = new MultiDictionary<KeyValuePair<string, string>>();
		
		public FB2UnionGenres() {
			const string xml = "union_genres_ru.xml";
			if ( !File.Exists( xml ) ) {
				string Message = string.Format( "Не найден файл Жанров '{0}'.", xml );
				MessageBox.Show( Message );
				throw new Exception( Message );
			}
			
			XElement xmlTree = null;
			try {
				xmlTree = XElement.Load( xml );
			} catch {
				string Message = string.Format( "Не могу прочитать файл Жанров  '{0}'.", xml );
				MessageBox.Show( Message );
				throw new Exception( Message );
			}
			
			if( xmlTree != null ) {
				IEnumerable<XElement> Groups = xmlTree.Elements("group");
				foreach( XElement Group in Groups ) {
					string GroupName = Group.Attribute("name").Value;
					IEnumerable<XElement> GenreList = Group.Elements("genre");
					foreach (XElement Genre in GenreList)
						_mdFB2GenresWithGroups.Add(
							GroupName,
							new KeyValuePair<string, string>(Genre.Attribute("name").Value, Genre.Attribute("code").Value)
						);
				}
			}
		}
		
		#region Открытые методы класса
		// возвращает список всех Групп Жанров
		public List<string> GetListAllGenresGroupsList() {
			List<string> list = new List<string>();
			foreach (string key in _mdFB2GenresWithGroups.Keys)
				list.Add( key );
			return list;
		}
		
		// возвращает массив всех Групп Жанров
		public string[] GetListAllGenresGroupsArray() {
			return GetListAllGenresGroupsList().ToArray();
		}
		
		// возвращает список всех Имен и Кодов (в скобках) Жанров
		public List<string> GetListAllFullGenresList() {
			List<string> listFullGenres = new List<string>();
			foreach (string key in _mdFB2GenresWithGroups.Keys) {
				foreach (KeyValuePair<string, string> kvp in _mdFB2GenresWithGroups[key]) {
					string s = kvp.Key + " (" + kvp.Value + ")";
					if( !listFullGenres.Contains( s ) )
						listFullGenres.Add( s );
				}
			}
			return listFullGenres;
		}
		// возвращает массив всех Имен и Кодов (в скобках) Жанров
		public string[] GetListAllFullGenreArray() {
			return GetListAllFullGenresList().ToArray();
		}
		
		// возвращает список всех Имен и Кодов (в скобках) Жанров для конкретной Группы
		public List<string> GetListAllFullGenresListForGroup( string Group ) {
			List<string> listFullGenres = new List<string>();
			List<KeyValuePair<string, string>> list = _mdFB2GenresWithGroups[Group];
			foreach ( KeyValuePair<string, string> kvp in list ) {
				string s = kvp.Key + " (" + kvp.Value + ")";
				if( !listFullGenres.Contains( s ) )
					listFullGenres.Add( s );
			}
			return listFullGenres;
		}
		// возвращает массив всех Имен и Кодов (в скобках) Жанров для конкретной Группы
		public string[] GetListAllFullGenreArrayForGroup( string Group )  {
			return GetListAllFullGenresListForGroup( Group ).ToArray();
		}
		
		// возвращает список Кодов Жанров для конкретной Группы
		public List<string> GetFBGenreCodesListForGroup( string Group ) {
			List<string> listCode = new List<string>();
			List<KeyValuePair<string, string>> list = _mdFB2GenresWithGroups[Group];
			foreach ( KeyValuePair<string, string> kvp in list )
				listCode.Add( kvp.Value );
//			listCode.Sort();
			return listCode.Count != 0 ? listCode : null;
		}
		// возвращает массив Кодов Жанров для конкретной Группы
		public string[] GetFBGenreCodesArrayForGroup( string Group ) {
			List<string> lsGenresForGroup = GetFBGenreCodesListForGroup( Group );
			return lsGenresForGroup != null ? lsGenresForGroup.ToArray() : null;
		}

		// МЕДЛЕННЫЙ - ВАЖНЫЙ АЛГОРИТМ
		// возвращает расшифрованное значение (название) Жанра
		public string GetFBGenreName( string GenreCode ) {
			foreach ( string key in _mdFB2GenresWithGroups.Keys ) {
				foreach ( KeyValuePair<string, string> kvp in _mdFB2GenresWithGroups[key] )
					if ( GenreCode.Equals(kvp.Value) )
						return stringProcessing.OnlyCorrectSymbolsForString( kvp.Key );
			}
			return string.Empty;
		}
		
		// МЕДЛЕННЫЙ - ВАЖНЫЙ АЛГОРИТМ
		// возвращает Группу для указанного Жанра
		public string GetFBGenreGroup( string GenreCode ) {
			foreach (string key in _mdFB2GenresWithGroups.Keys) {
				foreach ( KeyValuePair<string, string> kvp in _mdFB2GenresWithGroups[key] ) {
					if ( GenreCode.Equals( kvp.Value ) )
						return stringProcessing.OnlyCorrectSymbolsForString( key );
				}
			}
			return string.Empty;
		}
		#endregion
	}
}
