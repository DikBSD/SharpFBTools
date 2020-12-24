/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 04.05.2009
 * Time: 11:30
 * 
 * License: GPL 2.1
 */
using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Core.Common;
using Core.FB2.Description.TitleInfo;
using Core.FB2.Description.PublishInfo;
using Core.FB2.Description.Common;
using Core.FB2.Genres;
using Core.FB2.FB2Parsers;
using Core.AutoCorrector;

using System.Windows.Forms;

namespace Core.Sorter.Templates {
	/// <summary>
	/// класс для формирования имени (пути) файла, согласно заданным шаблонам подстановки
	/// </summary>
	public class TemplatesParser : Core.Sorter.Templates.Lexems.AllTemplates
	{
		#region Закрытые данные
		private readonly FB2UnionGenres m_FB2UnionGenres = new FB2UnionGenres();
		private readonly char _cSeparator = '↑';
		#endregion
		
		public TemplatesParser()
		{
		}
		
		#region Открытые методы
		/* получение простых лексем из шаблонной строки */
		public List<Core.Sorter.Templates.Lexems.TPSimple> GemSimpleLexems( string sLine ) {
			// разбиваем строку относительно [ и ]
			string [] sTemp = InsertSeparatorToSquareBracket( sLine ).Split( new char[] { _cSeparator }, StringSplitOptions.RemoveEmptyEntries );
			// разбиваем строки sLexems, где нет [] относительно *
			List<string> lsLexems = new List<string>();
			foreach ( string sStr in sTemp ) {
				if ( sStr.IndexOf( '[' )!=-1 || sStr.IndexOf( '*' ) == -1 )
					lsLexems.Add( sStr );
				else
					lsLexems.AddRange( InsertSeparatorToAsterik( sStr ).Split( new char[] { _cSeparator }, StringSplitOptions.RemoveEmptyEntries ) );
			}
			// задаем лексемам их тип
			List<Lexems.TPSimple> lexems = new List<Lexems.TPSimple>();
			foreach ( string s in lsLexems ) {
				if ( !IsTemplateExsist( s ) ) {
					// постоянные символы
					lexems.Add( new Lexems.TPSimple( s, Lexems.SimpleType.const_text ) );
				} else {
					if ( s.IndexOf( '[' ) == -1 ) {
						// постоянный шаблон
						lexems.Add( new Lexems.TPSimple( s, Lexems.SimpleType.const_template ) );
					} else {
						// удаляем шаблон из строки и смотрим, есть ли там еще что, помимо него
						string st = GetTemplate( s );
						string sRem = s.Remove( s.IndexOf( st, StringComparison.CurrentCulture ), st.Length ).Remove( 0, 1 );
						sRem = sRem.Remove( (sRem.Length - 1), 1 );
						if ( sRem == string.Empty ) {
							// условный шаблон
							lexems.Add( new Lexems.TPSimple( s, Lexems.SimpleType.conditional_template ) );
						} else {
							// условная группа
							lexems.Add( new Lexems.TPSimple( s, Lexems.SimpleType.conditional_group ) );
						}
					}
				}
			}
			return lexems;
		}
		
		// формирование имени файла на основе данных Description и шаблонов подстановки
		// GenreGroupFromSelectedSort = null или Empty для Полной Сортировки (Группа берется исходя из fb2 жанра). Для Избранной - Группа Жанров = GenreGroupFromSelectedSort
		public string generateNewPath(string sFB2FilePath, List<Lexems.TPSimple> lSLexems,
		                              int nGenreIndex, int nAuthorIndex, int RegisterMode,
		                              int SpaceProcessMode, bool StrictMode, bool TranslitMode,
		                              ref SortingOptions sortOptions, ref long lCounter,
		                              int MaxBookTitleLenght, int MaxSequenceLenght, int MaxPublisherLenght,
									  string GenreGroupFromSelectedSort ) {
			LatinToRus latinToRus = new LatinToRus();
			string sFileName			= string.Empty;
			FictionBook fb2				= new FictionBook(sFB2FilePath);
			string sLang				= fb2.TILang;
			IList<Genre> lGenres		= fb2.TIGenres;
			// Замена 1-го латинского символа в ФИО Авторов на соответствующий кирилический
			IList<Author> lAuthors		= latinToRus.replaceFirstCharLatinToRusForAuthors(fb2.TIAuthors);
			BookTitle btBookTitle		= fb2.TIBookTitle;
			IList<Sequence> lSequences	= fb2.TISequences;
			Date dBookDate				= fb2.TIDate;
			string sYear		= fb2.PIYear;
			Publisher pubPub	= fb2.PIPublisher;
			City cCity			= fb2.PICity;
			IList<Author> lfb2Authors = fb2.DIAuthors;

			foreach ( Lexems.TPSimple lexem in lSLexems ) {
				switch ( lexem.Type ) {
					case Lexems.SimpleType.const_text:
						// постоянные символы
						sFileName += lexem.Lexem;
						break;
					case Lexems.SimpleType.const_template:
						// постоянный шаблон
						switch ( lexem.Lexem ) {
							case "*L*": // Язык Книги
								sFileName += string.IsNullOrWhiteSpace( sLang )
									? sortOptions.BookInfoNoLang
									: sLang.Trim();
								break;
							case "*GROUP*": // Группа Жанров
								string sNoGroup = sortOptions.BookInfoNoGenreGroup;
								if ( lGenres == null ) {
									sFileName += sNoGroup;
								} else {
									if ( lGenres[nGenreIndex] == null ) {
										sFileName += sNoGroup;
									} else {
										if ( string.IsNullOrWhiteSpace(lGenres[nGenreIndex].Name) ) {
											sFileName += sNoGroup;
										} else {
											// жанр в fb2 файле есть
											if ( string.IsNullOrEmpty( GenreGroupFromSelectedSort ) ) {
												// Полная Сортировка (GenreGroupFromSelectedSort=null)
												string sgg = m_FB2UnionGenres.GetFBGenreGroup( lGenres[nGenreIndex].Name.Trim() ); // группа жанров по жанру из описания файла
												// sgg = null или Empty для жанра, не соответствующего схеме
												sFileName += ( string.IsNullOrWhiteSpace( sgg ) ? sNoGroup : sgg );
											} else {
												// Для Избранной Сортировки
												sFileName += GenreGroupFromSelectedSort;
											}
										}
									}
								}
								break;
							case "*GG*": // Группа Жанров\Жанр Книги
								sNoGroup = sortOptions.BookInfoNoGenreGroup;
								string sNoGG = sNoGroup + "\\" + sortOptions.BookInfoNoGenre; // такого жанра нет в схеме
								if ( lGenres == null ) {
									sFileName += sNoGG;
								} else {
									if ( lGenres[nGenreIndex] == null ) {
										sFileName += sNoGG;
									} else {
										if ( string.IsNullOrWhiteSpace(lGenres[nGenreIndex].Name) ) {
											sFileName += sNoGG;
										} else {
											// жанр в fb2 файле есть
											string sGenreCode	= lGenres[nGenreIndex].Name.Trim();
											if ( string.IsNullOrEmpty( GenreGroupFromSelectedSort ) ) {
												// Полная Сортировка (GenreGroupFromSelectedSort=null)
												string sgg			= m_FB2UnionGenres.GetFBGenreGroup( sGenreCode );// группа жанров
												// sgg = null или Empty для жанра, не соответствующего схеме
												sFileName += sortOptions.GenresTypeGenreSchema
													? ( string.IsNullOrWhiteSpace( sgg ) ? sNoGroup + "\\" + sGenreCode : sgg + "\\" + sGenreCode ) /* как в схеме */
													: ( string.IsNullOrWhiteSpace( sgg ) ? sNoGroup + "\\" + sGenreCode : sgg + "\\" + m_FB2UnionGenres.GetFBGenreName( sGenreCode ) ); /* жанр расшифровано */
											} else {
												// Для Избранной Сортировки
												sFileName += sortOptions.GenresTypeGenreSchema
													? ( GenreGroupFromSelectedSort + "\\" + sGenreCode ) /* как в схеме */
													: ( GenreGroupFromSelectedSort + "\\" + m_FB2UnionGenres.GetFBGenreName( sGenreCode ) ); /* жанр расшифровано */
											}
										}
									}
								}
								break;
							case "*G*": // Жанр Книги
								if ( lGenres == null ) {
									sFileName += sortOptions.BookInfoNoGenre;
								} else {
									if ( lGenres[nGenreIndex] == null ) {
										sFileName += sortOptions.BookInfoNoGenre;
									} else {
										if ( string.IsNullOrWhiteSpace(lGenres[nGenreIndex].Name) ) {
											sFileName += sortOptions.BookInfoNoGenre;
										} else {
											// жанр есть
											if ( sortOptions.GenresTypeGenreSchema ) {
												// как в схеме
												sFileName += lGenres[nGenreIndex].Name.Trim();
											} else {
												// жанр расшифровано
												string sg = m_FB2UnionGenres.GetFBGenreName( lGenres[nGenreIndex].Name.Trim() );
												// sg.Length==0 для жанра, не соответствующего схеме
												sFileName += ( sg.Length==0 ? lGenres[nGenreIndex].Name.Trim() : sg );
											}
										}
									}
								}
								break;
							case "*BAF*": // Имя Автора Книги
								if ( lAuthors == null ) {
									sFileName += sortOptions.BookInfoNoFirstName;
								} else {
									if ( lAuthors[nAuthorIndex] == null ) {
										sFileName += sortOptions.BookInfoNoFirstName;
									} else {
										if ( lAuthors[nAuthorIndex].FirstName == null ) {
											sFileName += sortOptions.BookInfoNoFirstName;
										} else {
											sFileName += string.IsNullOrWhiteSpace(lAuthors[nAuthorIndex].FirstName.Value)
												? sortOptions.BookInfoNoFirstName
												: lAuthors[nAuthorIndex].FirstName.Value.Trim();
										}
									}
								}
								break;
							case "*LF*": // 1-я Буква Имя Автора Книги
								if ( lAuthors == null ) {
									sFileName += sortOptions.BookInfoNoFirstName[0];
								} else {
									if ( lAuthors[nAuthorIndex] == null ) {
										sFileName += sortOptions.BookInfoNoFirstName[0];
									} else {
										if ( lAuthors[nAuthorIndex].FirstName == null ) {
											sFileName += sortOptions.BookInfoNoFirstName[0];
										} else {
											if ( string.IsNullOrWhiteSpace(lAuthors[nAuthorIndex].FirstName.Value) ) {
												sFileName += sortOptions.BookInfoNoFirstName[0];
											} else {
												string sExsist = lAuthors[nAuthorIndex].FirstName.Value.Trim();
												sFileName += firstLetter( sExsist ).ToString().ToUpper();
											}
										}
									}
								}
								break;
							case "*BAM*": // Отчество Автора Книги
								if ( lAuthors == null ) {
									sFileName += sortOptions.BookInfoNoMiddleName;
								} else {
									if ( lAuthors[nAuthorIndex] == null ) {
										sFileName += sortOptions.BookInfoNoMiddleName;
									} else {
										if ( lAuthors[nAuthorIndex].MiddleName == null ) {
											sFileName += sortOptions.BookInfoNoMiddleName;
										} else {
											sFileName += string.IsNullOrWhiteSpace(lAuthors[nAuthorIndex].MiddleName.Value)
												? sortOptions.BookInfoNoMiddleName
												: lAuthors[nAuthorIndex].MiddleName.Value.Trim();
										}
									}
								}
								break;
							case "*LM*": // 1-я Буква Отчество Автора Книги
								if ( lAuthors == null ) {
									sFileName += sortOptions.BookInfoNoMiddleName[0];
								} else {
									if ( lAuthors[nAuthorIndex] == null ) {
										sFileName += sortOptions.BookInfoNoMiddleName[0];
									} else {
										if ( lAuthors[nAuthorIndex].MiddleName == null ) {
											sFileName += sortOptions.BookInfoNoMiddleName[0];
										} else {
											if ( string.IsNullOrWhiteSpace(lAuthors[nAuthorIndex].MiddleName.Value) ) {
												sFileName += sortOptions.BookInfoNoMiddleName[0];
											} else {
												string sExsist = lAuthors[nAuthorIndex].MiddleName.Value.Trim();
												sFileName += firstLetter( sExsist ).ToString().ToUpper();
											}
										}
									}
								}
								break;
							case "*BAL*": // Фамилия Автора Книги
								if ( lAuthors == null ) {
									sFileName += sortOptions.BookInfoNoLastName;
								} else {
									if ( lAuthors[nAuthorIndex] == null ) {
										sFileName += sortOptions.BookInfoNoLastName;
									} else {
										if ( lAuthors[nAuthorIndex].LastName == null ) {
											sFileName += sortOptions.BookInfoNoLastName;
										} else {
											sFileName += string.IsNullOrWhiteSpace(lAuthors[nAuthorIndex].LastName.Value)
												? sortOptions.BookInfoNoLastName
												: lAuthors[nAuthorIndex].LastName.Value.Trim();
										}
									}
								}
								break;
							case "*LL*": // 1-я Буква Фамилия Автора Книги
								if ( lAuthors == null ) {
									sFileName += sortOptions.BookInfoNoLastName[0];
								} else {
									if ( lAuthors[nAuthorIndex] == null ) {
										sFileName += sortOptions.BookInfoNoLastName[0];
									} else {
										if ( lAuthors[nAuthorIndex].LastName == null ) {
											sFileName += sortOptions.BookInfoNoLastName[0];
										} else {
											if ( string.IsNullOrWhiteSpace(lAuthors[nAuthorIndex].LastName.Value) ) {
												sFileName += sortOptions.BookInfoNoLastName[0];
											} else {
												string sExsist = lAuthors[nAuthorIndex].LastName.Value.Trim();
												sFileName += firstLetter( sExsist ).ToString().ToUpper();
											}
										}
									}
								}
								break;
							case "*LBAL*": // 1-я Буква Фамилия Автора Книги \ Фамилия Автора Книги
								string sNoLN = sortOptions.BookInfoNoLastName;
								string sNoLLN = sNoLN[0] + "\\" + sNoLN;
								if ( lAuthors == null ) {
									sFileName += sNoLN;
								} else {
									if ( lAuthors[nAuthorIndex] == null ) {
										sFileName += sNoLN;
									} else {
										if ( lAuthors[nAuthorIndex].LastName == null ) {
											sFileName += sNoLLN;
										} else {
											if ( string.IsNullOrWhiteSpace(lAuthors[nAuthorIndex].LastName.Value) ) {
												sFileName += sNoLLN;
											} else {
												string sExsist = lAuthors[nAuthorIndex].LastName.Value.Trim();
												sFileName += firstLetter( sExsist ).ToString().ToUpper() + "\\" + sExsist;
											}
										}
									}
								}
								break;
							case "*LBAL_OR_LBAN*": // 1-я Буква Фамилия или Ника Автора Книги\Фамилия или Ник Автора Книги
								sNoLN = "Без Автора";
								string sNoAuthor = sNoLN[0] + "\\" + sNoLN;
								if ( lAuthors == null ) {
									sFileName += sNoLN;
								} else {
									if ( lAuthors[nAuthorIndex] == null ) {
										sFileName += sNoLN;
									} else {
										if ( lAuthors[nAuthorIndex].LastName == null ) {
											sFileName += makeFilenameForLBAL_OR_LBAN( lAuthors, nAuthorIndex, sNoAuthor );
										} else {
											if ( string.IsNullOrWhiteSpace(lAuthors[nAuthorIndex].LastName.Value) ) {
												sFileName += makeFilenameForLBAL_OR_LBAN( lAuthors, nAuthorIndex, sNoAuthor );
											} else {
												string sExsist = lAuthors[nAuthorIndex].LastName.Value.Trim();
												sFileName += firstLetter( sExsist ).ToString().ToUpper() + "\\" + sExsist;
											}
										}
									}
								}
								break;
							case "*LL_OR_LN*": // 1-я Буква Фамилия или Ника Автора Книги
								sNoLN = "Без Автора";
								if ( lAuthors == null ) {
									sFileName += sNoLN;
								} else {
									if ( lAuthors[nAuthorIndex] == null ) {
										sFileName += sNoLN;
									} else {
										if ( lAuthors[nAuthorIndex].LastName == null ) {
											sFileName += makeFilenameForLL_OR_LN( lAuthors, nAuthorIndex, sNoLN[0] );
										} else {
											if ( string.IsNullOrWhiteSpace(lAuthors[nAuthorIndex].LastName.Value) ) {
												sFileName += makeFilenameForLL_OR_LN( lAuthors, nAuthorIndex, sNoLN[0] );
											} else {
												string sExsist = lAuthors[nAuthorIndex].LastName.Value.Trim();
												sFileName += firstLetter( sExsist ).ToString().ToUpper();
											}
										}
									}
								}
								break;
							case "*BAN*": // Ник Автора Книги
								if ( lAuthors == null ) {
									sFileName += sortOptions.BookInfoNoNickName;
								} else {
									if ( lAuthors[nAuthorIndex] == null ) {
										sFileName += sortOptions.BookInfoNoNickName;
									} else {
										if ( lAuthors[nAuthorIndex].NickName == null ) {
											sFileName += sortOptions.BookInfoNoNickName;
										} else {
											sFileName += string.IsNullOrWhiteSpace(lAuthors[nAuthorIndex].NickName.Value)
												? sortOptions.BookInfoNoNickName
												: lAuthors[nAuthorIndex].NickName.Value.Trim();
										}
									}
								}
								break;
							case "*LN*": //1-я Буква Ника Автора Книги
								if ( lAuthors == null ) {
									sFileName += sortOptions.BookInfoNoNickName;
								} else {
									if ( lAuthors[nAuthorIndex] == null ) {
										sFileName += sortOptions.BookInfoNoNickName;
									} else {
										if ( lAuthors[nAuthorIndex].NickName == null ) {
											sFileName += sortOptions.BookInfoNoNickName;
										} else {
											if ( string.IsNullOrWhiteSpace(lAuthors[nAuthorIndex].NickName.Value) ) {
												sFileName += sortOptions.BookInfoNoNickName;
											} else {
												string sExsist = lAuthors[nAuthorIndex].NickName.Value.Trim();
												sFileName += firstLetter( sExsist ).ToString().ToUpper();
											}
										}
									}
								}
								break;
							case "*BT*": // Название Книги
								if ( btBookTitle == null ) {
									sFileName += sortOptions.BookInfoNoBookTitle;
								} else {
									sFileName += string.IsNullOrWhiteSpace(btBookTitle.Value)
										? sortOptions.BookInfoNoBookTitle
										: StringProcessing.makeString( btBookTitle.Value.Trim(), MaxBookTitleLenght );
								}
								break;
							case "*SN*": // Серия Книги
								if ( lSequences == null ) {
									sFileName += sortOptions.BookInfoNoSequence;
								} else {
									if ( lSequences[0] == null )
										sFileName += sortOptions.BookInfoNoNSequence;
									else
										sFileName += string.IsNullOrWhiteSpace(lSequences[0].Name)
											? sortOptions.BookInfoNoSequence
											: StringProcessing.makeString( lSequences[0].Name.Trim(), MaxSequenceLenght );
								}
								break;
							case "*SI*": // Номер Серии Книги
								if ( lSequences == null ) {
									sFileName += sortOptions.BookInfoNoNSequence;
								} else {
									if ( lSequences[0] == null )
										sFileName += sortOptions.BookInfoNoNSequence;
									else
										sFileName += string.IsNullOrWhiteSpace(lSequences[0].Number)
											? sortOptions.BookInfoNoNSequence
											: lSequences[0].Number;
								}
								break;
							case "*SII*": // Номер Серии Книги 0X
								if ( lSequences == null ) {
									sFileName += sortOptions.BookInfoNoNSequence;
								} else {
									if ( lSequences[0] == null )
										sFileName += sortOptions.BookInfoNoNSequence;
									else
										sFileName += string.IsNullOrWhiteSpace(lSequences[0].Number)
											? sortOptions.BookInfoNoNSequence
											: MakeSII( lSequences[0].Number );
								}
								break;
							case "*SIII*": // Номер Серии Книги 00X
								if ( lSequences == null ) {
									sFileName += sortOptions.BookInfoNoNSequence;
								} else {
									if ( lSequences[0] == null )
										sFileName += sortOptions.BookInfoNoNSequence;
									else
										sFileName += string.IsNullOrWhiteSpace(lSequences[0].Number)
											? sortOptions.BookInfoNoNSequence
											: MakeSIII( lSequences[0].Number );
								}
								break;
							case "*DT*": // Дата написания Книги (текст)
								if ( dBookDate == null )
									sFileName += sortOptions.BookInfoNoDateText;
								else
									sFileName += string.IsNullOrWhiteSpace(dBookDate.Text)
										? sortOptions.BookInfoNoDateText
										: dBookDate.Text.Trim();
								break;
							case "*DV*": // Дата написания Книги (значение)
								if ( dBookDate == null )
									sFileName += sortOptions.BookInfoNoDateText;
								else
									sFileName += string.IsNullOrWhiteSpace(dBookDate.Value)
										? sortOptions.BookInfoNoDateValue
										: dBookDate.Value.Trim();
								break;
							case "*YEAR*": // Год Издания Книги
								sFileName += string.IsNullOrWhiteSpace( sYear )
									? sortOptions.PublishInfoNoYear
									: sYear.Trim();
								break;
							case "*PUB*": // Издательство
								if ( pubPub == null )
									sFileName += sortOptions.PublishInfoNoPublisher;
								else
									sFileName += string.IsNullOrWhiteSpace(pubPub.Value)
										? sortOptions.PublishInfoNoPublisher
										: StringProcessing.makeString(pubPub.Value.Trim(), MaxPublisherLenght);
								break;
							case "*CITY*": // Город Издательства
								if ( cCity == null )
									sFileName += sortOptions.PublishInfoNoCity;
								else
									sFileName += string.IsNullOrWhiteSpace(cCity.Value)
										? sortOptions.PublishInfoNoCity
										: cCity.Value.Trim();
								break;
							case "*FB2AF*": // Имя сооздателя fb2-файла
								if ( lfb2Authors == null ) {
									sFileName += sortOptions.FB2InfoNoFB2FirstName;
								} else {
									if ( lfb2Authors[nAuthorIndex] == null ) {
										sFileName += sortOptions.FB2InfoNoFB2FirstName;
									} else {
										if ( lfb2Authors[nAuthorIndex].FirstName == null ) {
											sFileName += sortOptions.FB2InfoNoFB2FirstName;
										} else {
											sFileName +=string.IsNullOrWhiteSpace(lfb2Authors[nAuthorIndex].FirstName.Value)
												? sortOptions.FB2InfoNoFB2FirstName
												: lfb2Authors[nAuthorIndex].FirstName.Value.Trim();
										}
									}
								}
								break;
							case "*FB2AM*": // Отчество сооздателя fb2-файла
								if ( lfb2Authors == null ) {
									sFileName += sortOptions.FB2InfoNoFB2MiddleName;
								} else {
									if ( lfb2Authors[nAuthorIndex] == null ) {
										sFileName += sortOptions.FB2InfoNoFB2MiddleName;
									} else {
										if ( lfb2Authors[nAuthorIndex].MiddleName == null ) {
											sFileName += sortOptions.FB2InfoNoFB2MiddleName;
										} else {
											sFileName += string.IsNullOrWhiteSpace(lfb2Authors[nAuthorIndex].MiddleName.Value)
												? sortOptions.FB2InfoNoFB2MiddleName
												: lfb2Authors[nAuthorIndex].MiddleName.Value.Trim();
										}
									}
								}
								break;
							case "*FB2AL*": // Фамилия сооздателя fb2-файла
								if ( lfb2Authors == null ) {
									sFileName += sortOptions.FB2InfoNoFB2LastName;
								} else {
									if ( lfb2Authors[nAuthorIndex] == null ) {
										sFileName += sortOptions.FB2InfoNoFB2LastName;
									} else {
										if ( lfb2Authors[nAuthorIndex].LastName == null ) {
											sFileName += sortOptions.FB2InfoNoFB2LastName;
										} else {
											sFileName += string.IsNullOrWhiteSpace(lfb2Authors[nAuthorIndex].LastName.Value)
												? sortOptions.FB2InfoNoFB2LastName
												: lfb2Authors[nAuthorIndex].LastName.Value.Trim();
										}
									}
								}
								break;
							case "*FB2AN*": // Ник сооздателя fb2-файла
								if ( lfb2Authors == null ) {
									sFileName += sortOptions.FB2InfoNoFB2NickName;
								} else {
									if ( lfb2Authors[nAuthorIndex] == null ) {
										sFileName += sortOptions.FB2InfoNoFB2NickName;
									} else {
										if ( lfb2Authors[nAuthorIndex].NickName == null ) {
											sFileName += sortOptions.FB2InfoNoFB2NickName;
										} else {
											sFileName += string.IsNullOrWhiteSpace(lfb2Authors[nAuthorIndex].NickName.Value)
												? sortOptions.FB2InfoNoFB2NickName
												: lfb2Authors[nAuthorIndex].NickName.Value.Trim();
										}
									}
								}
								break;
							case "*FILENAME*": // Название файла
								sFileName += Path.GetFileNameWithoutExtension( sFB2FilePath );
								break;
							case "*COUNTER*": // Счетчик файлов
								sFileName += (++lCounter).ToString();
								break;
								default :
									sFileName += string.Empty;
								break;
						}
						break;
					case Lexems.SimpleType.conditional_template:
						// условный шаблон
						switch ( lexem.Lexem ) {
							case "[*L*]": // Язык Книги
								if ( !string.IsNullOrWhiteSpace(sLang) ) {
									sFileName += sLang.Trim();
								}
								break;
							case "[*GG*]": // Группа Жанров\Жанр Книги
								string sNoGenreGroup = sortOptions.BookInfoNoGenreGroup; // такого жанра (группы) нет в схеме
								if ( lGenres != null ) {
									if ( lGenres[nGenreIndex] != null ) {
										if ( !string.IsNullOrWhiteSpace(lGenres[nGenreIndex].Name) ) {
											// жанр в fb2 файле есть
											string sGenreCode	= lGenres[nGenreIndex].Name.Trim();
											if ( string.IsNullOrEmpty( GenreGroupFromSelectedSort ) ) {
												// Полная Сортировка (GenreGroupFromSelectedSort=null)
												string sgg = m_FB2UnionGenres.GetFBGenreGroup( sGenreCode );// группа жанров
												// sgg = null или Empty для жанра, не соответствующего схеме
												sFileName += sortOptions.GenresTypeGenreSchema
													? ( string.IsNullOrWhiteSpace( sgg ) ? sNoGenreGroup + "\\" + sGenreCode : sgg + "\\" + sGenreCode ) /* как в схеме */
													: ( string.IsNullOrWhiteSpace( sgg ) ? sNoGenreGroup + "\\" + sGenreCode : sgg + "\\" + m_FB2UnionGenres.GetFBGenreName( sGenreCode ) ); /* жанр расшифровано */
											} else {
												// Для Избранной Сортировки
												sFileName += sortOptions.GenresTypeGenreSchema
													? ( GenreGroupFromSelectedSort + "\\" + sGenreCode ) /* как в схеме */
													: ( GenreGroupFromSelectedSort + "\\" + m_FB2UnionGenres.GetFBGenreName( sGenreCode ) ); /* жанр расшифровано */
											}
										}
									}
								}
								break;
							case "[*G*]": // Жанр Книги
								if ( lGenres != null ) {
									if ( lGenres[nGenreIndex] != null ) {
										if ( !string.IsNullOrWhiteSpace(lGenres[nGenreIndex].Name) ) {
											if ( sortOptions.GenresTypeGenreSchema ) {
												// как в схеме
												sFileName += lGenres[nGenreIndex].Name.Trim();
											} else {
												// жанр расшифровано
												string sg = m_FB2UnionGenres.GetFBGenreName( lGenres[nGenreIndex].Name.Trim() );
												// sg.Length==0 для жанра, не соответствующего схеме
												sFileName += ( sg.Length == 0 ? lGenres[nGenreIndex].Name.Trim() : sg );
											}
										}
									}
								}
								break;
							case "[*BAF*]": // Имя Автора Книги
								if ( lAuthors != null ) {
									if ( lAuthors[nAuthorIndex] != null ) {
										if ( lAuthors[nAuthorIndex].FirstName != null ) {
											if ( !string.IsNullOrWhiteSpace(lAuthors[nAuthorIndex].FirstName.Value) )
												sFileName += lAuthors[nAuthorIndex].FirstName.Value.Trim();
										}
									}
								}
								break;
							case "[*LF*]": // Имя Автора Книги - 1-я Буква
								if ( lAuthors != null ) {
									if ( lAuthors[nAuthorIndex] != null ) {
										if ( lAuthors[nAuthorIndex].FirstName != null ) {
											if ( !string.IsNullOrWhiteSpace(lAuthors[nAuthorIndex].FirstName.Value) ) {
												string sExsist = lAuthors[nAuthorIndex].FirstName.Value.Trim();
												sFileName += firstLetter( sExsist ).ToString().ToUpper();
											}
										}
									}
								}
								break;
							case "[*BAM*]": // Отчество Автора Книги
								if ( lAuthors != null ) {
									if ( lAuthors[nAuthorIndex] != null ) {
										if ( lAuthors[nAuthorIndex].MiddleName != null ) {
											if ( !string.IsNullOrWhiteSpace(lAuthors[nAuthorIndex].MiddleName.Value) )
												sFileName += lAuthors[nAuthorIndex].MiddleName.Value.Trim();
										}
									}
								}
								break;
							case "[*LM*]": // Отчество Автора Книги - 1-я Буква
								if ( lAuthors != null ) {
									if ( lAuthors[nAuthorIndex] != null ) {
										if ( lAuthors[nAuthorIndex].MiddleName != null ) {
											if ( !string.IsNullOrWhiteSpace(lAuthors[nAuthorIndex].MiddleName.Value) ) {
												string sExsist = lAuthors[nAuthorIndex].MiddleName.Value.Trim();
												sFileName += firstLetter( sExsist ).ToString().ToUpper();
											}
										}
									}
								}
								break;
							case "[*BAL*]": // Фамилия Автора Книги
								if ( lAuthors != null ) {
									if ( lAuthors[nAuthorIndex] != null ) {
										if ( lAuthors[nAuthorIndex].LastName != null ) {
											if ( !string.IsNullOrWhiteSpace(lAuthors[nAuthorIndex].LastName.Value) )
												sFileName += lAuthors[nAuthorIndex].LastName.Value.Trim();
										}
									}
								}
								break;
							case "[*LL*]": // Фамилия Автора Книги - 1-я Буква
								if ( lAuthors != null ) {
									if ( lAuthors[nAuthorIndex] != null ) {
										if ( lAuthors[nAuthorIndex].LastName != null ) {
											if ( !string.IsNullOrWhiteSpace(lAuthors[nAuthorIndex].LastName.Value) ) {
												string sExsist = lAuthors[nAuthorIndex].LastName.Value.Trim();
												sFileName += firstLetter( sExsist ).ToString().ToUpper();
											}
										}
									}
								}
								break;
							case "[*LBAL*]": // 1-я Буква Фамилия Автора Книги \ Фамилия Автора Книги
								if ( lAuthors != null ) {
									if ( lAuthors[nAuthorIndex] != null ) {
										if ( lAuthors[nAuthorIndex].LastName != null ) {
											if ( !string.IsNullOrWhiteSpace(lAuthors[nAuthorIndex].LastName.Value) ) {
												string sExsist = lAuthors[nAuthorIndex].LastName.Value.Trim();
												sFileName += firstLetter( sExsist ).ToString().ToUpper() + "\\" + sExsist;
											}
										}
									}
								}
								break;
							case "[*BAN*]": // Ник Автора Книги
								if ( lAuthors != null ) {
									if ( lAuthors[nAuthorIndex] != null ) {
										if ( lAuthors[nAuthorIndex].NickName != null ) {
											if ( !string.IsNullOrWhiteSpace(lAuthors[nAuthorIndex].NickName.Value) )
												sFileName += lAuthors[nAuthorIndex].NickName.Value.Trim();
										}
									}
								}
								break;
							case "[*LN*]": // Ник Автора Книги- 1-я Буква
								if ( lAuthors != null ) {
									if ( lAuthors[nAuthorIndex] != null ) {
										if ( lAuthors[nAuthorIndex].NickName != null ) {
											if ( !string.IsNullOrWhiteSpace(lAuthors[nAuthorIndex].NickName.Value) ) {
												string sExsist = lAuthors[nAuthorIndex].NickName.Value.Trim();
												sFileName += firstLetter( sExsist ).ToString().ToUpper();
											}
										}
									}
								}
								break;
							case "[*LBAL_OR_LBAN*]": // 1-я Буква Фамилия или Ника Автора Книги\Фамилия или Ник Автора Книги
								if ( lAuthors != null ) {
									if ( lAuthors[nAuthorIndex] != null ) {
										if ( lAuthors[nAuthorIndex].LastName != null ) {
											if ( !string.IsNullOrWhiteSpace(lAuthors[nAuthorIndex].LastName.Value) ) {
												string sExsist = lAuthors[nAuthorIndex].LastName.Value.Trim();
												sFileName += firstLetter( sExsist ).ToString().ToUpper() + "\\" + sExsist;
											} else {
												sFileName += makeFilenameForIF_LBAL_OR_LBAN( lAuthors, nAuthorIndex );
											}
										} else {
											sFileName += makeFilenameForIF_LBAL_OR_LBAN( lAuthors, nAuthorIndex );
										}
									}
								}
								break;
							case "[*LL_OR_LN*]": // 1-я Буква Фамилия или Ника Автора Книги
								if ( lAuthors != null ) {
									if ( lAuthors[nAuthorIndex] != null ) {
										if ( lAuthors[nAuthorIndex].LastName != null ) {
											if ( !string.IsNullOrWhiteSpace(lAuthors[nAuthorIndex].LastName.Value) ) {
												string sExsist = lAuthors[nAuthorIndex].LastName.Value.Trim();
												sFileName += firstLetter( sExsist ).ToString().ToUpper();
											} else {
												sFileName += makeFilenameForIF_LL_OR_LN( lAuthors, nAuthorIndex );
											}
										} else {
											sFileName += makeFilenameForIF_LL_OR_LN( lAuthors, nAuthorIndex );
										}
									}
								}
								break;
							case "[*BT*]": // Название Книги
								if ( btBookTitle != null ) {
									if ( !string.IsNullOrWhiteSpace(btBookTitle.Value) )
										sFileName += StringProcessing.makeString( btBookTitle.Value.Trim(), MaxBookTitleLenght );
								}
								break;
							case "[*SN*]": // Серия Книги
								if ( lSequences != null ) {
									if ( lSequences[0] != null ) {
										if ( !string.IsNullOrWhiteSpace(lSequences[0].Name) )
											sFileName += StringProcessing.makeString( lSequences[0].Name.Trim(), MaxSequenceLenght );
									}
								}
								break;
							case "[*SI*]": // Номер Серии Книги
								if ( lSequences != null ) {
									if ( lSequences[0] != null ) {
										if ( !string.IsNullOrWhiteSpace(lSequences[0].Number) )
											sFileName += lSequences[0].Number;
									}
								}
								break;
							case "[*SII*]": // Номер Серии Книги 0X
								if ( lSequences != null ) {
									if ( lSequences[0] != null ) {
										if ( !string.IsNullOrWhiteSpace(lSequences[0].Number) )
											sFileName += MakeSII( lSequences[0].Number );
									}
								}
								break;
							case "[*SIII*]": // Номер Серии Книги 00X
								if ( lSequences != null ) {
									if ( lSequences[0] != null ) {
										if ( !string.IsNullOrWhiteSpace(lSequences[0].Number) )
											sFileName += MakeSIII( lSequences[0].Number );
									}
								}
								break;
							case "[*DT*]": // Дата написания Книги (текст)
								if ( dBookDate != null ) {
									if ( !string.IsNullOrWhiteSpace(dBookDate.Text) )
										sFileName += dBookDate.Text.Trim();
								}
								break;
							case "[*DV*]": // Дата написания Книги (значение)
								if ( dBookDate != null ) {
									if ( !string.IsNullOrWhiteSpace(dBookDate.Value) )
										sFileName += dBookDate.Value.Trim();
								}
								break;
							case "[*YEAR*]": // Год Издания Книги
								if ( !string.IsNullOrWhiteSpace(sYear) )
									sFileName += sYear.Trim();
								break;
							case "[*PUB*]": // Издательство
								if ( pubPub != null ) {
									if ( !string.IsNullOrWhiteSpace(pubPub.Value) )
										sFileName += StringProcessing.makeString(pubPub.Value.Trim(), MaxPublisherLenght);
								}
								break;
							case "[*CITY*]": // Город Издательства
								if ( cCity != null ) {
									if ( !string.IsNullOrWhiteSpace(cCity.Value) )
										sFileName += cCity.Value.Trim();
								}
								break;
							case "[*FB2AF*]": // Имя создателя fb2-файла
								if ( lfb2Authors != null ) {
									if ( lfb2Authors[nAuthorIndex] != null ) {
										if ( lfb2Authors[nAuthorIndex].FirstName != null ) {
											if ( !string.IsNullOrWhiteSpace(lfb2Authors[nAuthorIndex].FirstName.Value) )
												sFileName += lfb2Authors[nAuthorIndex].FirstName.Value.Trim();
										}
									}
								}
								break;
							case "[*FB2AM*]": // Отчество создателя fb2-файла
								if ( lfb2Authors != null ) {
									if ( lfb2Authors[nAuthorIndex] != null ) {
										if ( lfb2Authors[nAuthorIndex].MiddleName != null ) {
											if ( !string.IsNullOrWhiteSpace(lfb2Authors[nAuthorIndex].MiddleName.Value) )
												sFileName += lfb2Authors[nAuthorIndex].MiddleName.Value.Trim();
										}
									}
								}
								break;
							case "[*FB2AL*]": // Фамилия создателя fb2-файла
								if ( lfb2Authors != null ) {
									if ( lfb2Authors[nAuthorIndex] != null ) {
										if ( lfb2Authors[nAuthorIndex].LastName != null ) {
											if ( !string.IsNullOrWhiteSpace(lfb2Authors[nAuthorIndex].LastName.Value) )
												sFileName += lfb2Authors[nAuthorIndex].LastName.Value.Trim();
										}
									}
								}
								break;
							case "[*FB2AN*]": // Ник создателя fb2-файла
								if ( lfb2Authors != null ) {
									if ( lfb2Authors[nAuthorIndex] != null ) {
										if ( lfb2Authors[nAuthorIndex].NickName != null ) {
											if ( !string.IsNullOrWhiteSpace(lfb2Authors[nAuthorIndex].NickName.Value) )
												sFileName += lfb2Authors[nAuthorIndex].NickName.Value.Trim();
										}
									}
								}
								break;
								default :
									//sFileName += string.Empty;
									break;
						}
						break;
					case Lexems.SimpleType.conditional_group:
						// условная группа
						sFileName += AnalyzeComplexGroup( lexem.Lexem, sLang, lGenres, lAuthors, btBookTitle,
						                                 lSequences, dBookDate, sYear, pubPub, cCity,
						                                 lfb2Authors,
						                                 ref sortOptions, nGenreIndex, nAuthorIndex,
						                                 MaxBookTitleLenght, MaxSequenceLenght, MaxPublisherLenght,
														 GenreGroupFromSelectedSort);
						break;
						default :
							// постоянные символы
							sFileName += lexem.Lexem;
						break;
				}
			}
			// если с начала или в конце строки есть один или несколько \ - то убираем их
			Regex rx = new Regex( @"^\\+" );
			sFileName = rx.Replace( sFileName, string.Empty );
			rx = new Regex( @"\\+$" );
			sFileName = rx.Replace( sFileName, string.Empty );
			rx = new Regex( @"\\+" );
			sFileName = rx.Replace( sFileName, "\\" );
			// если перед \ есть пробелы - убираем их (иначе архиваторы не архивируют файл)
			rx = new Regex( @" +\\" );
			sFileName = rx.Replace( sFileName, "\\" );
			// убираем многоточие в начале строки
			rx = new Regex( @"^\.\.\.\s*" );
			sFileName = rx.Replace( sFileName, string.Empty );
			rx = new Regex( @"(\\)\.\.\.\s*" );
			sFileName = rx.Replace( sFileName, "\\" );
			
			string Ret = StringProcessing.MakeGeneralWorkedPath( sFileName, RegisterMode, SpaceProcessMode, StrictMode, TranslitMode);
			
			// Добавить к создаваемому файлу суффикс из {Переводчик}[Издательство](FB2 Автор)
			string Sufix = FilesWorker.GetTranslatorPublisherFB2AuthorExt(fb2, MaxPublisherLenght);
			Sufix = StringProcessing.MakeGeneralWorkedPath(Sufix, RegisterMode, SpaceProcessMode, StrictMode, TranslitMode);
			
			return !string.IsNullOrEmpty(Sufix) ? (Ret + Sufix) : Ret;
		}

		#endregion

		#region Закрытые Вспомогательные методы
		private string makeFilenameForIF_LBAL_OR_LBAN( IList<Author> lAuthors, int nAuthorIndex ) {
			string sFileName = string.Empty;
			if ( lAuthors[nAuthorIndex].NickName != null ) {
				if ( ! string.IsNullOrWhiteSpace(lAuthors[nAuthorIndex].NickName.Value) ) {
					if ( lAuthors[nAuthorIndex].NickName.Value.Trim().Length != 0 ) {
						string sExsist = lAuthors[nAuthorIndex].NickName.Value.Trim();
						sFileName += firstLetter( sExsist ).ToString().ToUpper() + "\\" + sExsist;
					}
				}
			}
			return sFileName;
		}
		
		private string makeFilenameForLBAL_OR_LBAN( IList<Author> lAuthors, int nAuthorIndex, string sNoAuthor ) {
			string sFileName = sNoAuthor;
			if ( lAuthors[nAuthorIndex].NickName != null ) {
				if ( ! string.IsNullOrWhiteSpace(lAuthors[nAuthorIndex].NickName.Value) ) {
					string sExsist = lAuthors[nAuthorIndex].NickName.Value.Trim();
					sFileName = firstLetter( sExsist ).ToString().ToUpper() + "\\" + sExsist;
				}
			}
			return sFileName;
		}
		
		private string makeLexemForComplexLBAL_OR_LBAN( IList<Author> lAuthors, int nAuthorIndex ) {
			string sLexem = string.Empty;
			if ( lAuthors[nAuthorIndex].NickName != null ) {
				if ( ! string.IsNullOrWhiteSpace(lAuthors[nAuthorIndex].NickName.Value) ) {
					string sExsist = lAuthors[nAuthorIndex].NickName.Value.Trim();
					sLexem = firstLetter( sExsist ).ToString().ToUpper() + "\\" + sExsist;
				}
			}
			return sLexem;
		}
		
		private string makeFilenameForIF_LL_OR_LN( IList<Author> lAuthors, int nAuthorIndex ) {
			string sFileName = string.Empty;
			if ( lAuthors[nAuthorIndex].NickName != null ) {
				if ( !string.IsNullOrWhiteSpace(lAuthors[nAuthorIndex].NickName.Value) ) {
					if ( lAuthors[nAuthorIndex].NickName.Value.Trim().Length != 0 ) {
						string sExsist = lAuthors[nAuthorIndex].NickName.Value.Trim();
						sFileName += firstLetter( sExsist ).ToString().ToUpper();
					}
				}
			}
			return sFileName;
		}
		
		private string makeFilenameForLL_OR_LN( IList<Author> lAuthors, int nAuthorIndex, char cNoAuthorLetter ) {
			string sFileName = cNoAuthorLetter.ToString();
			if ( lAuthors[nAuthorIndex].NickName != null ) {
				if ( ! string.IsNullOrWhiteSpace(lAuthors[nAuthorIndex].NickName.Value) ) {
					string sExsist = lAuthors[nAuthorIndex].NickName.Value.Trim();
					sFileName = firstLetter( sExsist ).ToString().ToUpper();
				}
			}
			return sFileName;
		}
		
		private string makeLexemForComplexLL_OR_LN( IList<Author> lAuthors, int nAuthorIndex ) {
			string sLexem = string.Empty;
			if ( lAuthors[nAuthorIndex].NickName != null ) {
				if ( ! string.IsNullOrWhiteSpace(lAuthors[nAuthorIndex].NickName.Value) ) {
					string sExsist = lAuthors[nAuthorIndex].NickName.Value.Trim();
					sLexem = firstLetter( sExsist ).ToString().ToUpper();
				}
			}
			return sLexem;
		}
		
		
		// возвращает 1-ю букву или цифру (отбрасываются кавычки, скобки, пунктуация...). Если первые 6 символов - не буква или цифра, то возвращается 1-м символ value
		private char firstLetter( string value ) {
			if ( value == "." || value == ".." )
				return '_';
			int iterCharTopBound = value.Length > 6 ? 6 : value.Length; // на случай, если число символов в value < 6
			for ( int i = 0; i != iterCharTopBound; ++i ) {
				char ch = value[i];
				if ( Char.IsLetter(ch) || Char.IsNumber(ch) )
					return ch;
			}
			return value[0];
		}

		// вставка разделителя слева от открывающей * и справа от закрывающей *
		private string InsertSeparatorToAsterik( string sLine ) {
			if ( string.IsNullOrEmpty( sLine ) )
				return sLine;
			if ( sLine.IndexOf( '*' ) == -1 )
				return sLine;
			string sTemp = string.Empty;
			int nCount = 0; // счетчик * - для определения их четности
			for ( int i=0; i!=sLine.Length; ++i ) {
				if ( sLine[i] == '*' ) {
					++nCount;
					if ( nCount % 2 == 0 ) {
						// закрывающая * (четная)
						sTemp += sLine[i];
						sTemp += _cSeparator;
					} else {
						// открывающая * (не четная)
						sTemp += _cSeparator;
						sTemp += sLine[i];
					}
				} else
					sTemp += sLine[i];
			}
			return sTemp;
		}

		// вставка разделителя слева от [ и справа от ]
		private string InsertSeparatorToSquareBracket( string sLine ) {
			if ( string.IsNullOrEmpty( sLine ) )
				return sLine;
			string sTemp = string.Empty;
			for ( int i = 0; i != sLine.Length; ++i ) {
				if ( sLine[i] == '[' ) {
					sTemp += _cSeparator;
					sTemp += sLine[i];
				} else {
					sTemp += sLine[i];
					if ( sLine[i] == ']' )
						sTemp += _cSeparator;
				}
			}
			return sTemp;
		}

		// подсчет числа элементов cChar в строке sLine
		private int CountElement( string sLine, char cChar ) {
			int nCount = 0;
			for ( int i = 0; i != sLine.Length; ++i ) {
				if ( sLine[i] == cChar )
					++nCount;
			}
			return  nCount;
		}

		// проверка, есть ли шаблоны в строке
		private bool IsTemplateExsist( string sLine ) {
			foreach ( string t in m_sAllTemplates ) {
				if ( sLine.IndexOf( t, StringComparison.CurrentCulture ) != -1 )
					return true;
			}
			return false;
		}

		// возвращает 1-й шаблон в строке, если он есть, или ""
		private string GetTemplate( string sLine ) {
			foreach ( string t in m_sAllTemplates ) {
				if ( sLine.IndexOf( t, StringComparison.CurrentCulture ) != -1 )
					return t;
			}
			return string.Empty;
		}

		/* получение лексем из сложной группы */
		private List<Core.Sorter.Templates.Lexems.TPComplex> GemComplexLexems( string sLine ) {
			// разбиваем строку относительно *
			string str = sLine.Remove( 0, 1 );
			str = str.Remove( (str.Length - 1), 1 );
			string [] sLexems = InsertSeparatorToAsterik( str ).Split( new char[] { _cSeparator }, StringSplitOptions.RemoveEmptyEntries );
			// задаем лексемам их тип
			List<Lexems.TPComplex> lexems = new List<Lexems.TPComplex>();
			foreach ( string s in sLexems ) {
				if ( !IsTemplateExsist( s ) )
					lexems.Add( new Lexems.TPComplex( s, Lexems.ComplexType.text ) ); // символы
				else
					lexems.Add( new Lexems.TPComplex( s, Lexems.ComplexType.template ) ); // шаблон
			}
			return lexems;
		}

		// формирование номера Серии Книги по Шаблону 0X
		private string MakeSII( string sSequence ) {
			// проверка, число ли это
			if ( !StringProcessing.IsNumberInString( sSequence ) )
				return sSequence; // не число
			else {
				// число, смотрим, сколько цифр и добавляем слева нужное число 0.
				if ( sSequence.Length == 1 )
					return "0" + sSequence;
				else
					return sSequence; // число символов >= 2
			}
		}

		// формирование номера Серии Книги по Шаблону 00X
		private string MakeSIII( string sSequence ) {
			// проверка, число ли это
			if ( !StringProcessing.IsNumberInString( sSequence ) )
				return sSequence; // не число
			else {
				// число, смотрим, сколько цифр и добавляем слева нужное число 0.
				if ( sSequence.Length == 1 )
					return "00" + sSequence;
				else if ( sSequence.Length == 2 )
					return "0" + sSequence;
				else
					return sSequence; // число символов >= 3
			}
		}
		
		// парсинг сложных условных групп
		// GenreGroupFromSelectedSort = null или Empty для Полной Сортировки (Группа берется исходя из fb2 жанра). Для Избранной - Группа Жанров = GenreGroupFromSelectedSort
		private string AnalyzeComplexGroup( string sLine, string sLang, IList<Genre> lGenres, IList<Author> lAuthors,
		                                   BookTitle btBookTitle, IList<Sequence> lSequences, Date dBookDate,
		                                   string sYear, Publisher pubPub, City cCity,
		                                   IList<Author> lfb2Authors,
		                                   ref SortingOptions sortOptions, int nGenreIndex, int nAuthorIndex,
		                                   int MaxBookTitleLenght, int MaxSequenceLenght, int MaxPublisherLenght,
										   string GenreGroupFromSelectedSort ) {
			string sFileName = string.Empty;
			List<Lexems.TPComplex> lCLexems = GemComplexLexems( sLine );
			foreach ( Lexems.TPComplex lexem in lCLexems ) {
				switch ( lexem.Type ) {
					case Lexems.ComplexType.text:
						// символы
						break;
					case Lexems.ComplexType.template:
						// шаблоны
						switch( lexem.Lexem ) {
							case "*L*": // Язык Книги
								lexem.Lexem = string.IsNullOrEmpty( sLang )
									? lexem.Lexem = string.Empty
									: lexem.Lexem = sLang.Trim();
								break;
							case "*GG*": // Группа Жанров\Жанр Книги
								string sNoGG = sortOptions.BookInfoNoGenreGroup;// такого жанра (группы) нет в схеме
								if ( lGenres == null ) {
									lexem.Lexem = string.Empty;
								} else {
									if ( lGenres[nGenreIndex] == null ) {
										lexem.Lexem = string.Empty;
									} else {
										if ( string.IsNullOrWhiteSpace(lGenres[nGenreIndex].Name) ) {
											lexem.Lexem = string.Empty;
										} else {
											// жанр есть
											string sGenreCode	= lGenres[nGenreIndex].Name.Trim();
											if ( string.IsNullOrEmpty( GenreGroupFromSelectedSort ) ) {
												// Полная Сортировка
												string sgg = m_FB2UnionGenres.GetFBGenreGroup( sGenreCode );// группа жанров
												// sgg = null или Empty для жанра, не соответствующего схеме
												lexem.Lexem = sortOptions.GenresTypeGenreSchema
													? lexem.Lexem = ( string.IsNullOrWhiteSpace( sgg ) ? sNoGG + "\\" + sGenreCode : sgg + "\\" + sGenreCode ) /* как в схеме */
													: lexem.Lexem = ( string.IsNullOrWhiteSpace( sgg ) ? sNoGG + "\\" + sGenreCode : sgg + "\\" + m_FB2UnionGenres.GetFBGenreName( sGenreCode ) ); /* жанр расшифровано */
											} else {
												// Для Избранной Сортировки
												lexem.Lexem = sortOptions.GenresTypeGenreSchema
													? ( GenreGroupFromSelectedSort + "\\" + sGenreCode ) /* как в схеме */
													: ( GenreGroupFromSelectedSort + "\\" + m_FB2UnionGenres.GetFBGenreName( sGenreCode ) ); /* жанр расшифровано */
											}
										}
									}
								}
								break;
							case "*G*": // Жанр Книги
								if ( lGenres == null ) {
									lexem.Lexem = string.Empty;
								} else {
									if ( lGenres[nGenreIndex] == null ) {
										lexem.Lexem = string.Empty;
									} else {
										if ( string.IsNullOrWhiteSpace(lGenres[nGenreIndex].Name) ) {
											lexem.Lexem = string.Empty;
										} else {
											// жанр есть
											if ( sortOptions.GenresTypeGenreSchema ) {
												// как в схеме
												lexem.Lexem = lGenres[nGenreIndex].Name.Trim();
											} else {
												// жанр расшифровано
												string sg = m_FB2UnionGenres.GetFBGenreName( lGenres[nGenreIndex].Name.Trim() );
												// sg.Length == 0 для жанра, не соответствующего схеме
												lexem.Lexem = ( string.IsNullOrWhiteSpace( sg ) ? lGenres[nGenreIndex].Name.Trim() : sg );
											}
										}
									}
								}
								break;
							case "*BAF*": // Имя Автора Книги
								if ( lAuthors == null ) {
									lexem.Lexem = string.Empty;
								} else {
									if ( lAuthors[nAuthorIndex] == null ) {
										lexem.Lexem = string.Empty;
									} else {
										if ( lAuthors[nAuthorIndex].FirstName == null ) {
											lexem.Lexem = string.Empty;
										} else {
											lexem.Lexem = string.IsNullOrEmpty(lAuthors[nAuthorIndex].FirstName.Value)
												? string.Empty
												: lAuthors[nAuthorIndex].FirstName.Value.Trim();
										}
									}
								}
								break;
							case "*LF*": // Имя Автора Книги - 1-я Буква
								if ( lAuthors == null ) {
									lexem.Lexem = string.Empty;
								} else {
									if ( lAuthors[nAuthorIndex] == null ) {
										lexem.Lexem = string.Empty;
									} else {
										if ( lAuthors[nAuthorIndex].FirstName == null ) {
											lexem.Lexem = string.Empty;
										} else {
											if ( string.IsNullOrEmpty(lAuthors[nAuthorIndex].FirstName.Value) ) {
												lexem.Lexem = string.Empty;
											} else {
												string sExsist = lAuthors[nAuthorIndex].FirstName.Value.Trim();
												lexem.Lexem = firstLetter( sExsist ).ToString().ToUpper();
											}
										}
									}
								}
								break;
							case "*BAM*": // Отчество Автора Книги
								if ( lAuthors == null ) {
									lexem.Lexem = string.Empty;
								} else {
									if ( lAuthors[nAuthorIndex] == null ) {
										lexem.Lexem = string.Empty;
									} else {
										if ( lAuthors[nAuthorIndex].MiddleName == null ) {
											lexem.Lexem = string.Empty;
										} else {
											lexem.Lexem = string.IsNullOrEmpty(lAuthors[nAuthorIndex].MiddleName.Value)
												? string.Empty
												: lAuthors[nAuthorIndex].MiddleName.Value.Trim();
										}
									}
								}
								break;
							case "*LM*": // Отчество Автора Книги - 1-я Буква
								if ( lAuthors == null ) {
									lexem.Lexem = string.Empty;
								} else {
									if ( lAuthors[nAuthorIndex] == null ) {
										lexem.Lexem = string.Empty;
									} else {
										if ( lAuthors[nAuthorIndex].MiddleName == null ) {
											lexem.Lexem = string.Empty;
										} else {
											if ( string.IsNullOrEmpty(lAuthors[nAuthorIndex].MiddleName.Value) ) {
												lexem.Lexem = string.Empty;
											} else {
												string sExsist = lAuthors[nAuthorIndex].MiddleName.Value.Trim();
												lexem.Lexem = firstLetter( sExsist ).ToString().ToUpper();
											}
										}
									}
								}
								break;
							case "*BAL*": // Фамилия Автора Книги
								if ( lAuthors == null ) {
									lexem.Lexem = string.Empty;
								} else {
									if ( lAuthors[nAuthorIndex] == null ) {
										lexem.Lexem = string.Empty;
									} else {
										if ( lAuthors[nAuthorIndex].LastName == null ) {
											lexem.Lexem = string.Empty;
										} else {
											lexem.Lexem = string.IsNullOrEmpty(lAuthors[nAuthorIndex].LastName.Value)
												? string.Empty
												: lAuthors[nAuthorIndex].LastName.Value.Trim();
										}
									}
								}
								break;
							case "*LL*": // Фамилия Автора Книги - 1-я Буква
								if ( lAuthors == null ) {
									lexem.Lexem = string.Empty;
								} else {
									if ( lAuthors[nAuthorIndex] == null ) {
										lexem.Lexem = string.Empty;
									} else {
										if ( lAuthors[nAuthorIndex].LastName == null ) {
											lexem.Lexem = string.Empty;
										} else {
											if ( string.IsNullOrEmpty(lAuthors[nAuthorIndex].LastName.Value) ) {
												lexem.Lexem = string.Empty;
											} else {
												string sExsist = lAuthors[nAuthorIndex].LastName.Value.Trim();
												lexem.Lexem = firstLetter( sExsist ).ToString().ToUpper();
											}
										}
									}
								}
								break;
							case "*LBAL*": // 1-я Буква Фамилия Автора Книги\Фамилия Автора Книги
								if ( lAuthors == null ) {
									lexem.Lexem = string.Empty;
								} else {
									if ( lAuthors[nAuthorIndex] == null ) {
										lexem.Lexem = string.Empty;
									} else {
										if ( lAuthors[nAuthorIndex].LastName == null ) {
											lexem.Lexem = string.Empty;
										} else {
											if ( string.IsNullOrEmpty(lAuthors[nAuthorIndex].LastName.Value) ) {
												lexem.Lexem = string.Empty;
											} else {
												string sExsist = lAuthors[nAuthorIndex].LastName.Value.Trim();
												lexem.Lexem = firstLetter( sExsist ).ToString().ToUpper() + "\\" + sExsist;
											}
										}
									}
								}
								break;
							case "*BAN*": // Ник Автора Книги
								if ( lAuthors == null ) {
									lexem.Lexem = string.Empty;
								} else {
									if ( lAuthors[nAuthorIndex] == null ) {
										lexem.Lexem = string.Empty;
									} else {
										if ( lAuthors[nAuthorIndex].NickName == null ) {
											lexem.Lexem = string.Empty;
										} else {
											lexem.Lexem = string.IsNullOrEmpty(lAuthors[nAuthorIndex].NickName.Value)
												? string.Empty
												: lAuthors[nAuthorIndex].NickName.Value.Trim();
										}
									}
								}
								break;
							case "*LN*": // Ник Автора Книги - 1-я Буква
								if ( lAuthors == null ) {
									lexem.Lexem = string.Empty;
								} else {
									if ( lAuthors[nAuthorIndex] == null ) {
										lexem.Lexem = string.Empty;
									} else {
										if ( lAuthors[nAuthorIndex].NickName == null ) {
											lexem.Lexem = string.Empty;
										} else {
											if ( string.IsNullOrEmpty(lAuthors[nAuthorIndex].NickName.Value) ) {
												lexem.Lexem = string.Empty;
											} else {
												string sExsist = lAuthors[nAuthorIndex].NickName.Value.Trim();
												lexem.Lexem = firstLetter( sExsist ).ToString().ToUpper();
											}
										}
									}
								}
								break;
							case "*LBAL_OR_LBAN*": // 1-я Буква Фамилия или Ника Автора Книги\Фамилия или Ник Автора Книги
								if ( lAuthors == null ) {
									lexem.Lexem = string.Empty;
								} else {
									if ( lAuthors[nAuthorIndex] == null ) {
										lexem.Lexem = string.Empty;
									} else {
										if ( lAuthors[nAuthorIndex].LastName == null ) {
											lexem.Lexem = makeLexemForComplexLBAL_OR_LBAN( lAuthors, nAuthorIndex );
										} else {
											if ( string.IsNullOrWhiteSpace(lAuthors[nAuthorIndex].LastName.Value) ) {
												lexem.Lexem = makeLexemForComplexLBAL_OR_LBAN( lAuthors, nAuthorIndex );
											} else {
												string sExsist = lAuthors[nAuthorIndex].LastName.Value.Trim();
												lexem.Lexem = firstLetter( sExsist ).ToString().ToUpper() + "\\" + sExsist;
											}
										}
									}
								}
								break;
							case "*LL_OR_LN*": // 1-я Буква Фамилия или Ника Автора Книги
								if ( lAuthors == null ) {
									lexem.Lexem = string.Empty;
								} else {
									if ( lAuthors[nAuthorIndex] == null ) {
										lexem.Lexem = string.Empty;
									} else {
										if ( lAuthors[nAuthorIndex].LastName == null ) {
											lexem.Lexem = makeLexemForComplexLL_OR_LN( lAuthors, nAuthorIndex );
										} else {
											if ( string.IsNullOrWhiteSpace(lAuthors[nAuthorIndex].LastName.Value) ) {
												lexem.Lexem = makeLexemForComplexLL_OR_LN( lAuthors, nAuthorIndex );
											} else {
												string sExsist = lAuthors[nAuthorIndex].LastName.Value.Trim();
												lexem.Lexem = firstLetter( sExsist ).ToString().ToUpper();
											}
										}
									}
								}
								break;
							case "*BT*": // Название Книги
								if ( btBookTitle == null ) {
									lexem.Lexem = string.Empty;
								} else {
									lexem.Lexem = string.IsNullOrWhiteSpace(btBookTitle.Value)
										? string.Empty : StringProcessing.makeString( btBookTitle.Value.Trim(), MaxBookTitleLenght );
								}
								break;
							case "*SN*": // Серия Книги
								if ( lSequences == null ) {
									lexem.Lexem = string.Empty;
								} else {
									if ( lSequences[0] == null )
										lexem.Lexem = string.Empty;
									else
										lexem.Lexem = string.IsNullOrWhiteSpace(lSequences[0].Name)
											? string.Empty : StringProcessing.makeString( lSequences[0].Name.Trim(), MaxSequenceLenght );
								}
								break;
							case "*SI*": // Номер Серии Книги X
								if ( lSequences == null ) {
									lexem.Lexem = string.Empty;
								} else {
									if ( lSequences[0] == null )
										lexem.Lexem = string.Empty;
									else
										lexem.Lexem = string.IsNullOrEmpty(lSequences[0].Number)
											? string.Empty : lSequences[0].Number;
								}
								break;
							case "*SII*": // Номер Серии Книги 0X
								if ( lSequences == null ) {
									lexem.Lexem = string.Empty;
								} else {
									if ( lSequences[0] == null )
										lexem.Lexem = string.Empty;
									else
										lexem.Lexem = string.IsNullOrEmpty(lSequences[0].Number)
											? string.Empty
											: MakeSII( lSequences[0].Number );
								}
								break;
							case "*SIII*": // Номер Серии Книги 00X
								if ( lSequences == null ) {
									lexem.Lexem = string.Empty;
								} else {
									if ( lSequences[0] == null )
										lexem.Lexem = string.Empty;
									else
										lexem.Lexem = string.IsNullOrEmpty(lSequences[0].Number)
											? string.Empty
											: MakeSIII( lSequences[0].Number );
								}
								break;
							case "*DT*": // Дата написания Книги (текст)
								if ( dBookDate == null ) {
									lexem.Lexem = string.Empty;
								} else {
									lexem.Lexem = string.IsNullOrWhiteSpace(dBookDate.Text)
										? string.Empty : dBookDate.Text.Trim();
								}
								break;
							case "*DV*": // Дата написания Книги (значение)
								if ( dBookDate == null ) {
									lexem.Lexem = string.Empty;
								} else {
									lexem.Lexem = string.IsNullOrWhiteSpace(dBookDate.Value)
										? string.Empty : dBookDate.Value.Trim();
								}
								break;
							case "*YEAR*": // Год Издания Книги
								lexem.Lexem = string.IsNullOrWhiteSpace( sYear )
									? string.Empty
									: sYear.Trim();
								break;
							case "*PUB*": // Издательство
								if ( pubPub == null ) {
									lexem.Lexem = string.Empty;
								} else {
									lexem.Lexem = string.IsNullOrWhiteSpace(pubPub.Value)
										? string.Empty : StringProcessing.makeString(pubPub.Value.Trim(), MaxPublisherLenght);
								}
								break;
							case "*CITY*": // Город Издательства
								if ( cCity == null ) {
									lexem.Lexem = string.Empty;
								} else {
									lexem.Lexem = string.IsNullOrWhiteSpace(cCity.Value)
										? string.Empty : cCity.Value.Trim();
								}
								break;
							case "*FB2AF*": // Имя создателя fb2-файла
								if ( lfb2Authors == null ) {
									lexem.Lexem = string.Empty;
								} else {
									if ( lfb2Authors[nAuthorIndex] == null ) {
										lexem.Lexem = string.Empty;
									} else {
										if ( lfb2Authors[nAuthorIndex].FirstName == null ) {
											lexem.Lexem = string.Empty;
										} else {
											lexem.Lexem = string.IsNullOrWhiteSpace(lfb2Authors[nAuthorIndex].FirstName.Value)
												? string.Empty
												: lfb2Authors[nAuthorIndex].FirstName.Value.Trim();
										}
									}
								}
								break;
							case "*FB2AM*": // Отчество создателя fb2-файла
								if( lfb2Authors == null ) {
									lexem.Lexem = string.Empty;
								} else {
									if ( lfb2Authors[nAuthorIndex] == null ) {
										lexem.Lexem = string.Empty;
									} else {
										if ( lfb2Authors[nAuthorIndex].MiddleName == null ) {
											lexem.Lexem = string.Empty;
										} else {
											lexem.Lexem = string.IsNullOrWhiteSpace(lfb2Authors[nAuthorIndex].MiddleName.Value)
												? string.Empty
												: lfb2Authors[nAuthorIndex].MiddleName.Value.Trim();
										}
									}
								}
								break;
							case "*FB2AL*": // Фамилия создателя fb2-файла
								if ( lfb2Authors == null ) {
									lexem.Lexem = string.Empty;
								} else {
									if ( lfb2Authors[nAuthorIndex] == null ) {
										lexem.Lexem = string.Empty;
									} else {
										if ( lfb2Authors[nAuthorIndex].LastName == null ) {
											lexem.Lexem = string.Empty;
										} else {
											lexem.Lexem = string.IsNullOrWhiteSpace(lfb2Authors[nAuthorIndex].LastName.Value)
												? string.Empty
												: lfb2Authors[nAuthorIndex].LastName.Value.Trim();
										}
									}
								}
								break;
							case "*FB2AN*": // Ник создателя fb2-файла
								if ( lfb2Authors == null ) {
									lexem.Lexem = string.Empty;
								} else {
									if ( lfb2Authors[nAuthorIndex] == null ) {
										lexem.Lexem = string.Empty;
									} else {
										if ( lfb2Authors[nAuthorIndex].NickName == null ) {
											lexem.Lexem = string.Empty;
										} else {
											lexem.Lexem = string.IsNullOrWhiteSpace(lfb2Authors[nAuthorIndex].NickName.Value)
												? string.Empty
												: lfb2Authors[nAuthorIndex].NickName.Value.Trim();
										}
									}
								}
								break;
								default :
									lexem.Lexem = string.Empty;
								break;
						}
						break;
				}
			}

			// определение, какой текст, если он есть в группе, будет отображаться вместе с данными "его" шаблона
			for ( int i = 0; i != lCLexems.Count; ++i ) {
				// "пустой" шаблон "ликвидирует" текст справа от себя, а 1-й "пустой" шаблон - еще и слева.
				if ( lCLexems[i].Type == Lexems.ComplexType.template ) {
					if ( lCLexems[i].Lexem == string.Empty ) {
						// не 1-й ли это элемент списка
						if ( i < (lCLexems.Count-1) && lCLexems[i+1].Type == Lexems.ComplexType.text )
							lCLexems[i+1].Lexem = string.Empty;
						// если этот шаблон - самый первый из группы шаблонов, а до него есть текст, то еще и "ликвидируем" текст слева от него
						if ( i == 1 && lCLexems[0].Type == Lexems.ComplexType.text )
							lCLexems[0].Lexem = string.Empty;
					}
				}
			}
			// формирование строки
			foreach ( Lexems.TPComplex lex in lCLexems ) {
				// проверку ни в коем случае не заменять на ( !string.IsNullOrWhiteSpace( lex.Lexem ) ) - пропадут пробелы межуд шаблонами!!!
				if ( !string.IsNullOrEmpty( lex.Lexem ) )
					sFileName += lex.Lexem;
			}
			return sFileName;
		}
		
		#endregion
	}
}
