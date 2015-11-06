/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 04.08.2009
 * Time: 17:07
 * 
 * License: GPL 2.1
 */
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using System.Windows.Forms;

using Core.FB2.Description.TitleInfo;
using Core.FB2.Description.Common;
using Core.FB2.FB2Parsers;
using Core.FB2.Genres;
using Core.Common;

namespace Core.FileManager
{
	/// <summary>
	/// проверка файла по критериям Избранной сортировки
	/// </summary>
	public class FB2SelectedSorting
	{
		public FB2SelectedSorting()
		{
		}
		
		#region Открытые методы
		// заполняем список критериев поиска для Избранной Сортировки
		public static List<SortQueryCriteria> makeSelectedSortQuerysList( ref SortQueryCriteria criteria ) {
			List<string> lsGenres = null; // временный список Жанров по конкретной Группе Жанров
			// если есть Группа Жанров, то преобразуем ее в список "ее" Жанров
			if( !string.IsNullOrEmpty( criteria.GenresGroup ) ) {
				FB2UnionGenres fb2g = new FB2UnionGenres();
				lsGenres = fb2g.GetFBGenreCodesListForGroup( criteria.GenresGroup );
			}
			// формируем список критериев поиска в зависимости от наличия Групп Жанров
			List<SortQueryCriteria> lSSQCList = new List<SortQueryCriteria>();
			if( lsGenres == null ) {
				lSSQCList.Add(
					new SortQueryCriteria(
						criteria.Lang, criteria.GenresGroup, criteria.Genre,
						criteria.LastName, criteria.FirstName, criteria.MiddleName, criteria.NickName,
						criteria.Sequence, criteria.BookTitle, criteria.ExactFit
					)
				);
			} else {
				foreach( string sGenre in lsGenres ) {
					lSSQCList.Add(
						new SortQueryCriteria(
							criteria.Lang, string.Empty, sGenre,
							criteria.LastName, criteria.FirstName, criteria.MiddleName, criteria.NickName,
							criteria.Sequence, criteria.BookTitle, criteria.ExactFit
						)
					);
				}
			}
			return lSSQCList;
		}

		// проверка, соответствует ли текущий файл критерия поиска для Избранной Сортировки
		// возвращает null, если книга не соответствует ни одному критерию поиска; или найденный критерий SelectedSortQueryCriteria
		public static SortQueryCriteria isConformity( string sFromFilePath, List<SortQueryCriteria> lSSQCList, out bool IsNotRead ) {
			SortQueryCriteria currentCriteria = null;
			IsNotRead			= false;
			FictionBook fb2 	= null;
			TitleInfo	ti		= null;
			try {
				fb2	= new FictionBook( sFromFilePath );
				ti	= fb2.getTitleInfo();
				if( ti == null )
					return null;
			} catch {
				IsNotRead = true;
				return null;
			}
			
			string			sFB2Lang		= ti.Lang;
			BookTitle		FB2BookTitle	= ti.BookTitle;
			IList<Genre>	lFB2Genres		= ti.Genres;
			IList<Author>	lFB2Authors		= ti.Authors;
			IList<Sequence>	lFB2Sequences	= ti.Sequences;
			string sLang, sFirstName, sGenre, sMiddleName, sLastName, sNickName, sSequence, sBookTitle;
			bool bExactFit;
			Regex re = null;
			foreach( SortQueryCriteria ssqc in lSSQCList ) {
				// "вычленяем" язык книги
				sLang = ssqc.Lang;
				if( !string.IsNullOrWhiteSpace( sLang ) ) {
					sLang = sLang.Substring( sLang.IndexOf( "(" ) + 1 );
					sLang = sLang.Remove( sLang.IndexOf( ")" ) );
				}
				// если есть Жанр, то "вычленяем" его из строки
				sGenre	= ssqc.Genre;
				if( !string.IsNullOrWhiteSpace( sGenre ) ) {
					if( sGenre.IndexOf( "(" ) >= 0 )
						sGenre = sGenre.Substring( sGenre.IndexOf( "(" ) + 1 );
					if( sGenre.IndexOf( ")" ) >= 0 )
						sGenre = sGenre.Remove( sGenre.IndexOf( ")" ) );
				}
				sFirstName	= ssqc.FirstName;
				sMiddleName	= ssqc.MiddleName;
				sLastName	= ssqc.LastName;
				sNickName	= ssqc.NickName;
				sSequence	= ssqc.Sequence;
				sBookTitle	= ssqc.BookTitle;
				bExactFit	= ssqc.ExactFit;
				
				/* Проверка условиям критерия */
				// проверка языка книги
				if( !string.IsNullOrWhiteSpace( sFB2Lang ) ) {
					if( !string.IsNullOrWhiteSpace( sLang ) ) {
						if( sFB2Lang != sLang )
							continue;
					}
				} else {
					// в книге тега языка нет
					if( !string.IsNullOrWhiteSpace( sLang ) )
						continue;
				}
				// проверка жанра книги
				bool b = false;
				if( lFB2Genres != null ) {
					if( !string.IsNullOrWhiteSpace( sGenre ) ) {
						foreach( Genre gfb2 in lFB2Genres ) {
							if( !string.IsNullOrWhiteSpace( gfb2.Name ) ) {
								if( gfb2.Name == sGenre ) {
									b = true; break;
								}
							} else
								continue;
						}
						if( !b )
							continue;
					}
				} else {
					// в книге тега жанра нет
					if( !string.IsNullOrWhiteSpace( sGenre ) )
						continue;
				}
				// проверка серии книги
				b = false;
				if( lFB2Sequences != null ) {
					if( !string.IsNullOrWhiteSpace( sSequence ) ) {
						foreach( Sequence sfb2 in lFB2Sequences ) {
							if( !string.IsNullOrWhiteSpace( sfb2.Name ) ) {
								if( bExactFit ) {
									// точное соответствие
									if( sfb2.Name == sSequence ) {
										b = true; break;
									}
								} else {
									re = new Regex( sSequence, RegexOptions.IgnoreCase );
									if( re.IsMatch( sfb2.Name ) ) {
										b = true; break;
									}
								}
							} else
								continue;
						}
						if( !b )
							continue;
					}
				} else {
					// в книге тега серии нет
					if( !string.IsNullOrWhiteSpace( sSequence ) )
						continue;
				}
				// проверка автора книги
				if( lFB2Authors != null ) {
					b = false;
					if( sFirstName.Length != 0 ) {
						foreach( Author afb2 in lFB2Authors ) {
							if( afb2.FirstName != null ) {
								if( bExactFit ) {
									// точное соответствие
									if( afb2.FirstName.Value == sFirstName ) {
										b = true; break;
									}
								} else {
									re = new Regex( sFirstName, RegexOptions.IgnoreCase );
									if( re.IsMatch( afb2.FirstName.Value ) ) {
										b = true; break;
									}
								}
							} else
								continue;
						}
						if( !b )
							continue;
					}
					b = false;
					if( sMiddleName.Length != 0 ) {
						foreach( Author afb2 in lFB2Authors ) {
							if( afb2.MiddleName != null ) {
								if( bExactFit ) {
									// точное соответствие
									if( afb2.MiddleName.Value == sMiddleName ) {
										b = true; break;
									}
								} else {
									re = new Regex( sMiddleName, RegexOptions.IgnoreCase );
									if( re.IsMatch( afb2.MiddleName.Value ) ) {
										b = true; break;
									}
								}
							} else
								continue;
						}
						if( !b )
							continue;
					}
					b = false;
					if( sLastName.Length != 0 ) {
						foreach( Author afb2 in lFB2Authors ) {
							if( afb2.LastName != null ) {
								if( bExactFit ) {
									// точное соответствие
									if( afb2.LastName.Value == sLastName ) {
										b = true; break;
									}
								} else {
									re = new Regex( sLastName, RegexOptions.IgnoreCase );
									if( re.IsMatch( afb2.LastName.Value ) ) {
										b = true; break;
									}
								}
							} else
								continue;
						}
						if( !b )
							continue;
					}
					b = false;
					if( sNickName.Length != 0 ) {
						foreach( Author afb2 in lFB2Authors ) {
							if( afb2.NickName != null ) {
								if( bExactFit ) {
									// точное соответствие
									if( afb2.NickName.Value == sNickName ) {
										b = true; break;
									}
								} else {
									re = new Regex( sNickName, RegexOptions.IgnoreCase );
									if( re.IsMatch( afb2.NickName.Value ) ) {
										b = true; break;
									}
								}
							} else
								continue;
						}
						if( !b )
							continue;
					}
				} else {
					// в книге тегов автора нет
					if( !string.IsNullOrWhiteSpace( sFirstName ) || !string.IsNullOrWhiteSpace( sMiddleName )||
					   !string.IsNullOrWhiteSpace( sNickName ) || !string.IsNullOrWhiteSpace( sNickName ) )
						continue;
				}
				// проверка названия книги
				if( FB2BookTitle != null ) {
					if( !string.IsNullOrWhiteSpace( sBookTitle ) ) {
						if( !string.IsNullOrWhiteSpace( FB2BookTitle.Value ) ) {
							if( bExactFit ) {
								// точное соответствие
								if( FB2BookTitle.Value != sBookTitle )
									continue;
							} else {
								re = new Regex( sBookTitle, RegexOptions.IgnoreCase );
								if( !re.IsMatch( FB2BookTitle.Value ) )
									continue;
							}
						} else
							continue; // пустой тэг <book-title>
					}
				} else {
					// в книге тега названия нет
					if( !string.IsNullOrWhiteSpace( sBookTitle ) )
						continue;
				}
				currentCriteria = ssqc;
				break;
			}
			return currentCriteria;
		}
		
		#endregion
	}
}
