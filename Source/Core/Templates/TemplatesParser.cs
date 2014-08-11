/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 04.05.2009
 * Time: 11:30
 * 
 * License: GPL 2.1
 */
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Core.FB2.FB2Parsers;
using Core.FB2.Common;
using Core.FB2.Description;
using Core.FB2.Description.TitleInfo;
using Core.FB2.Description.DocumentInfo;
using Core.FB2.Description.PublishInfo;
using Core.FB2.Description.CustomInfo;
using Core.FB2.Description.Common;
using Core.FB2.Genres;
using Core.BookSorting;
using Core.Misc;

//using System.Windows.Forms;

using fB2Parser = Core.FB2.FB2Parsers.FB2Parser;

namespace Core.Templates {
	/// <summary>
	/// TemplatesParser: класс для формирования имени (пути) файла, согласно заданным шаблонам подстановки
	/// </summary>
	public class TemplatesParser : Core.Templates.Lexems.AllTemplates
	{
		#region Закрытые данные
		private static char cSeparator = '↑';
		#endregion
		
		public TemplatesParser()
		{
		}
		
		#region Закрытые Вспомогательные методы
		// возвращает 1-ю букву или цифру (отбрасываются кавычки, скобки, пунктуация...). Если первые 6 символов - не буква или цифра, то возвращается 1-м символ value
		private static char firstLetter( string value ) {
			if( value == "." || value == ".." )
				return '_';
			int iterCharTopBound = value.Length > 6 ? 6 : value.Length; // на случай, если число символов в value < 6
			for( int i = 0; i != iterCharTopBound; ++i ) {
				char ch = value[i];
				if( Char.IsLetter(ch) || Char.IsNumber(ch) )
					return ch;
			}
			return value[0];
		}
		private static string InsertSeparatorToAsterik( string sLine ) {
			// вставка разделителя слева от открывающей * и справа от закрывающей *
			if( sLine == null || sLine.Length == 0 ) return sLine;
			if( sLine.IndexOf( '*' ) == -1 ) return sLine;
			string sTemp = string.Empty;
			int nCount = 0; // счетчик * - для определения их четности
			for( int i=0; i!=sLine.Length; ++i ) {
				if( sLine[i]=='*' ) {
					++nCount;
					if( nCount % 2 == 0 ) {
						// закрывающая * (четная)
						sTemp += sLine[i];
						sTemp += cSeparator;
					} else {
						// открывающая * (не четная)
						sTemp += cSeparator;
						sTemp += sLine[i];
					}
				} else
					sTemp += sLine[i];
			}
			return sTemp;
		}
		
		private static string InsertSeparatorToSquareBracket( string sLine ) {
			// вставка разделителя слева от [ и справа от ]
			if( sLine == null || sLine.Length == 0 ) return sLine;
			string sTemp = string.Empty;
			for( int i=0; i!=sLine.Length; ++i ) {
				if( sLine[i]=='[' ) {
					sTemp += cSeparator;
					sTemp += sLine[i];
				} else {
					sTemp += sLine[i];
					if( sLine[i]==']' )
						sTemp += cSeparator;
				}
			}
			return sTemp;
		}
		
		private static int CountElement( string sLine, char cChar ) {
			// подсчет числа элементов cChar в строке sLine
			int nCount = 0;
			for( int i=0; i!=sLine.Length; ++i ) {
				if( sLine[i] == cChar )
					++nCount;
			}
			return  nCount;
		}
		
		private static bool IsTemplateExsist( string sLine ) {
			// проверка, есть ли шаблоны в строке
			foreach( string t in m_sAllTemplates ) {
				if( sLine.IndexOf( t )!=-1 )
					return true;
			}
			return false;
		}
		
		private static string GetTemplate( string sLine ) {
			// возвращает 1-й шаблон в строке, если он есть, или ""
			foreach( string t in m_sAllTemplates ) {
				if( sLine.IndexOf( t )!=-1 )
					return t;
			}
			return "";
		}
		
		private static List<Core.Templates.Lexems.TPComplex> GemComplexLexems( string sLine ) {
			/* получение лексем из сложной группы */
			// разбиваем строку относительно *
			string str = sLine.Remove( 0, 1 );
			str = str.Remove( (str.Length-1), 1 );
			string [] sLexems = InsertSeparatorToAsterik( str ).Split( new char[] { cSeparator }, StringSplitOptions.RemoveEmptyEntries );
			// задаем лексемам их тип
			List<Lexems.TPComplex> lexems = new List<Lexems.TPComplex>();
			foreach( string s in sLexems ) {
				if( !IsTemplateExsist( s ) )
					lexems.Add( new Lexems.TPComplex( s, Lexems.ComplexType.text ) ); // символы
				else
					lexems.Add( new Lexems.TPComplex( s, Lexems.ComplexType.template ) ); // шаблон
			}
			return lexems;
		}
		
		private static string MakeSII( string sSequence ) {
			// формирование номера Серии Книги по Шаблону 0X
			// проверка, число ли это
			if( !StringProcessing.IsNumberInString( sSequence ) )
				return sSequence; // не число
			else {
				// число, смотрим, сколько цифр и добавляем слева нужное число 0.
				if( sSequence.Length==1 )
					return "0" + sSequence;
				else
					return sSequence; // число символов >= 2
			}
		}
		
		private static string MakeSIII( string sSequence ) {
			// формирование номера Серии Книги по Шаблону 00X
			// проверка, число ли это
			if( !StringProcessing.IsNumberInString( sSequence ) )
				return sSequence; // не число
			else {
				// число, смотрим, сколько цифр и добавляем слева нужное число 0.
				if( sSequence.Length==1 )
					return "00"+sSequence;
				else if( sSequence.Length==2 )
					return "0"+sSequence;
				else
					return sSequence; // число символов >= 3
			}
		}
		
		// парсинг сложных условных групп
		private static string ParseComplexGroup( string sLine, string sLang, IList<Genre> lGenres, IList<Author> lAuthors,
		                                        BookTitle btBookTitle, IList<Sequence> lSequences, IFBGenres fb2g, Date dBookDate,
		                                        string sYear, Publisher pubPub, City cCity,
		                                        IList<Author> lfb2Authors,
		                                        ref SortingOptions sortOptions, int nGenreIndex, int nAuthorIndex ) {
			#region Код
			string sFileName = string.Empty;
			List<Lexems.TPComplex> lCLexems = GemComplexLexems( sLine );
			foreach( Lexems.TPComplex lexem in lCLexems ) {
				switch( lexem.Type ) {
					case Lexems.ComplexType.text:
						// символы
						break;
					case Lexems.ComplexType.template:
						// шаблоны
						switch( lexem.Lexem ) {
							case "*L*": // Язык Книги
								if( sLang == null || sLang.Length==0 ) {
									lexem.Lexem = "";
								} else {
									lexem.Lexem = sLang.Trim();
								}
								break;
							case "*GROUP*": // Группа Жанров
								string sNoGroup = sortOptions.BookInfoNoGenreGroup;
								if( lGenres == null ) {
									sFileName += sNoGroup;
								} else {
									if( lGenres[nGenreIndex].Name==null || lGenres[nGenreIndex].Name.Trim().Length==0 ) {
										sFileName += sNoGroup;
									} else {
										// жанр есть
										string sgg = fb2g.GetFBGenreGroup( lGenres[nGenreIndex].Name.Trim() );// группа жанров
										// sgg.Length==0 для жанра, не соответствующего схеме
										sFileName += ( sgg.Length==0 ? sNoGroup : sgg );
									}
								}
								break;
							case "*GG*": // Группа Жанров\Жанр Книги
								string sNoGG = sortOptions.BookInfoNoGenreGroup;// такого жанра (группы) нет в схеме
								if( lGenres == null ) {
									lexem.Lexem = "";
								} else {
									if( lGenres[nGenreIndex].Name==null || lGenres[nGenreIndex].Name.Trim().Length==0 ) {
										lexem.Lexem = "";
									} else {
										// жанр есть
										string sGenre	= lGenres[nGenreIndex].Name.Trim();
										string sgg		= fb2g.GetFBGenreGroup( sGenre );// группа жанров
										// sgg.Length==0 для жанра, не соответствующего схеме
										if( sortOptions.GenresTypeGenreSchema ) {
											// как в схеме
											lexem.Lexem = ( sgg.Length==0 ? sNoGG+"\\"+sGenre : sgg+"\\"+sGenre );
										} else {
											// жанр расшифровано
											lexem.Lexem = ( sgg.Length==0 ? sNoGG+"\\"+sGenre : sgg+"\\"+fb2g.GetFBGenreName( sGenre ) );
										}
									}
								}
								break;
							case "*G*": // Жанр Книги
								if( lGenres == null ) {
									lexem.Lexem = "";
								} else {
									if( lGenres[nGenreIndex].Name==null || lGenres[nGenreIndex].Name.Trim().Length==0 ) {
										lexem.Lexem = "";
									} else {
										// жанр есть
										if( sortOptions.GenresTypeGenreSchema ) {
											// как в схеме
											lexem.Lexem = lGenres[nGenreIndex].Name.Trim();
										} else {
											// жанр расшифровано
											string sg = fb2g.GetFBGenreName( lGenres[nGenreIndex].Name.Trim() );
											// sg.Length==0 для жанра, не соответствующего схеме
											lexem.Lexem = ( sg.Length==0 ? lGenres[nGenreIndex].Name.Trim() : sg );
										}
									}
								}
								break;
							case "*BAF*": // Имя Автора Книги
								if( lAuthors == null ) {
									lexem.Lexem = "";
								} else {
									if( lAuthors[nAuthorIndex].FirstName==null ) {
										lexem.Lexem = "";
									} else {
										if( lAuthors[nAuthorIndex].FirstName.Value.Trim().Length==0 ) {
											lexem.Lexem = "";
										} else {
											lexem.Lexem = lAuthors[nAuthorIndex].FirstName.Value.Trim();
										}
									}
								}
								break;
							case "*LF*": // Имя Автора Книги - 1-я Буква
								if( lAuthors == null ) {
									lexem.Lexem = "";
								} else {
									if( lAuthors[nAuthorIndex].FirstName==null ) {
										lexem.Lexem = "";
									} else {
										if( lAuthors[nAuthorIndex].FirstName.Value.Trim().Length==0 ) {
											lexem.Lexem = "";
										} else {
											string sExsist = lAuthors[nAuthorIndex].FirstName.Value.Trim();
											lexem.Lexem = firstLetter( sExsist ).ToString().ToUpper();
										}
									}
								}
								break;
							case "*BAM*": // Отчество Автора Книги
								if( lAuthors == null ) {
									lexem.Lexem = "";
								} else {
									if( lAuthors[nAuthorIndex].MiddleName==null ) {
										lexem.Lexem = "";
									} else {
										if( lAuthors[nAuthorIndex].MiddleName.Value.Trim().Length==0 ) {
											lexem.Lexem = "";
										} else {
											lexem.Lexem = lAuthors[nAuthorIndex].MiddleName.Value.Trim();
										}
									}
								}
								break;
							case "*LM*": // Отчество Автора Книги - 1-я Буква
								if( lAuthors == null ) {
									lexem.Lexem = "";
								} else {
									if( lAuthors[nAuthorIndex].MiddleName==null ) {
										lexem.Lexem = "";
									} else {
										if( lAuthors[nAuthorIndex].MiddleName.Value.Trim().Length==0 ) {
											lexem.Lexem = "";
										} else {
											string sExsist = lAuthors[nAuthorIndex].MiddleName.Value.Trim();
											lexem.Lexem = firstLetter( sExsist ).ToString().ToUpper();
										}
									}
								}
								break;
							case "*BAL*": // Фамилия Автора Книги
								if( lAuthors == null ) {
									lexem.Lexem = "";
								} else {
									if( lAuthors[nAuthorIndex].LastName==null ) {
										lexem.Lexem = "";
									} else {
										if( lAuthors[nAuthorIndex].LastName.Value.Trim().Length==0 ) {
											lexem.Lexem = "";
										} else {
											lexem.Lexem = lAuthors[nAuthorIndex].LastName.Value.Trim();
										}
									}
								}
								break;
							case "*LL*": // Фамилия Автора Книги - 1-я Буква
								if( lAuthors == null ) {
									lexem.Lexem = "";
								} else {
									if( lAuthors[nAuthorIndex].LastName==null ) {
										lexem.Lexem = "";
									} else {
										if( lAuthors[nAuthorIndex].LastName.Value.Trim().Length==0 ) {
											lexem.Lexem = "";
										} else {
											string sExsist = lAuthors[nAuthorIndex].LastName.Value.Trim();
											lexem.Lexem = firstLetter( sExsist ).ToString().ToUpper();
										}
									}
								}
								break;
							case "*LBAL*": // 1-я Буква Фамилия Автора Книги\Фамилия Автора Книги
								if( lAuthors == null ) {
									lexem.Lexem = "";
								} else {
									if( lAuthors[nAuthorIndex].LastName==null ) {
										lexem.Lexem = "";
									} else {
										if( lAuthors[nAuthorIndex].LastName.Value.Trim().Length==0 ) {
											lexem.Lexem = "";
										} else {
											string sExsist = lAuthors[nAuthorIndex].LastName.Value.Trim();
											lexem.Lexem = firstLetter( sExsist ).ToString().ToUpper() + "\\" + sExsist;
										}
									}
								}
								break;
							case "*BAN*": // Ник Автора Книги
								if( lAuthors == null ) {
									lexem.Lexem = "";
								} else {
									if( lAuthors[nAuthorIndex].NickName==null ) {
										lexem.Lexem = "";
									} else {
										if( lAuthors[nAuthorIndex].NickName.Value.Trim().Length==0 ) {
											lexem.Lexem = "";
										} else {
											lexem.Lexem = lAuthors[nAuthorIndex].NickName.Value.Trim();
										}
									}
								}
								break;
							case "*LN*": // Ник Автора Книги - 1-я Буква
								if( lAuthors == null ) {
									lexem.Lexem = "";
								} else {
									if( lAuthors[nAuthorIndex].NickName==null ) {
										lexem.Lexem = "";
									} else {
										if( lAuthors[nAuthorIndex].NickName.Value.Trim().Length==0 ) {
											lexem.Lexem = "";
										} else {
											string sExsist = lAuthors[nAuthorIndex].NickName.Value.Trim();
											lexem.Lexem = firstLetter( sExsist ).ToString().ToUpper();
										}
									}
								}
								break;
							case "*BT*": // Название Книги
								if( btBookTitle == null ) {
									lexem.Lexem = "";
								} else {
									if( btBookTitle.Value==null || btBookTitle.Value.Trim().Length==0 ) {
										lexem.Lexem = "";
									} else {
										lexem.Lexem = btBookTitle.Value.Trim();
									}
								}
								break;
							case "*SN*": // Серия Книги
								if( lSequences == null ) {
									lexem.Lexem = "";
								} else {
									if( lSequences[0].Name==null || lSequences[0].Name.Trim().Length==0 ) {
										lexem.Lexem = "";
									} else {
										lexem.Lexem = lSequences[0].Name.Trim();
									}
								}
								break;
							case "*SI*": // Номер Серии Книги X
								if( lSequences == null ) {
									lexem.Lexem = "";
								} else {
									if( lSequences[0].Number==null ) {
										lexem.Lexem = "";
									} else {
										lexem.Lexem = lSequences[0].Number;
									}
								}
								break;
							case "*SII*": // Номер Серии Книги 0X
								if( lSequences == null ) {
									lexem.Lexem = "";
								} else {
									if( lSequences[0].Number==null ) {
										lexem.Lexem = "";
									} else {
										lexem.Lexem = MakeSII( lSequences[0].Number );
									}
								}
								break;
							case "*SIII*": // Номер Серии Книги 00X
								if( lSequences == null ) {
									lexem.Lexem = "";
								} else {
									if( lSequences[0].Number==null ) {
										lexem.Lexem = "";
									} else {
										lexem.Lexem = MakeSIII( lSequences[0].Number );
									}
								}
								break;
							case "*DT*": // Дата написания Книги (текст)
								if( dBookDate == null ) {
									lexem.Lexem = "";
								} else {
									if( dBookDate.Text==null || dBookDate.Text.Trim().Length==0 ) {
										lexem.Lexem = "";
									} else {
										lexem.Lexem = dBookDate.Text.Trim();
									}
								}
								break;
							case "*DV*": // Дата написания Книги (значение)
								if( dBookDate == null ) {
									lexem.Lexem = "";
								} else {
									if( dBookDate.Value==null || dBookDate.Value.Trim().Length==0 ) {
										lexem.Lexem = "";
									} else {
										lexem.Lexem = dBookDate.Value.Trim();
									}
								}
								break;
							case "*YEAR*": // Год Издания Книги
								if( sYear == null || sYear.Length==0 ) {
									lexem.Lexem = "";
								} else {
									lexem.Lexem = sYear.Trim();
								}
								break;
							case "*PUB*": // Издательство
								if( pubPub == null ) {
									lexem.Lexem = "";
								} else {
									if( pubPub.Value==null || pubPub.Value.Trim().Length==0 ) {
										lexem.Lexem = "";
									} else {
										lexem.Lexem = pubPub.Value.Trim();
									}
								}
								break;
							case "*CITY*": // Город Издательства
								if( cCity == null ) {
									lexem.Lexem = "";
								} else {
									if( cCity.Value==null || cCity.Value.Trim().Length==0 ) {
										lexem.Lexem = "";
									} else {
										lexem.Lexem = cCity.Value.Trim();
									}
								}
								break;
							case "*FB2AF*": // Имя создателя fb2-файла
								if( lfb2Authors == null ) {
									lexem.Lexem = "";
								} else {
									if( lfb2Authors[nAuthorIndex].FirstName==null ) {
										lexem.Lexem = "";
									} else {
										if( lfb2Authors[nAuthorIndex].FirstName.Value.Trim().Length==0 ) {
											lexem.Lexem = "";
										} else {
											lexem.Lexem = lfb2Authors[nAuthorIndex].FirstName.Value.Trim();
										}
									}
								}
								break;
							case "*FB2AM*": // Отчество создателя fb2-файла
								if( lfb2Authors == null ) {
									lexem.Lexem = "";
								} else {
									if( lfb2Authors[nAuthorIndex].MiddleName==null ) {
										lexem.Lexem = "";
									} else {
										if( lfb2Authors[nAuthorIndex].MiddleName.Value.Trim().Length==0 ) {
											lexem.Lexem = "";
										} else {
											lexem.Lexem = lfb2Authors[nAuthorIndex].MiddleName.Value.Trim();
										}
									}
								}
								break;
							case "*FB2AL*": // Фамилия создателя fb2-файла
								if( lfb2Authors == null ) {
									lexem.Lexem = "";
								} else {
									if( lfb2Authors[nAuthorIndex].LastName==null ) {
										lexem.Lexem = "";
									} else {
										if( lfb2Authors[nAuthorIndex].LastName.Value.Trim().Length==0 ) {
											lexem.Lexem = "";
										} else {
											lexem.Lexem = lfb2Authors[nAuthorIndex].LastName.Value.Trim();
										}
									}
								}
								break;
							case "*FB2AN*": // Ник создателя fb2-файла
								if( lfb2Authors == null ) {
									lexem.Lexem = "";
								} else {
									if( lfb2Authors[nAuthorIndex].NickName==null ) {
										lexem.Lexem = "";
									} else {
										if( lfb2Authors[nAuthorIndex].NickName.Value.Trim().Length==0 ) {
											lexem.Lexem = "";
										} else {
											lexem.Lexem = lfb2Authors[nAuthorIndex].NickName.Value.Trim();
										}
									}
								}
								break;
								default :
									lexem.Lexem = "";
								break;
						}
						break;
				}
			}
			
			// определение, какой текст, если он есть в группе, будет отображаться вместе с данными "его" шаблона
			for( int i=0; i!=lCLexems.Count; ++i ) {
				// "пустой" шаблон "ликвидирует" текст справа от себя, а 1-й "пустой" шаблон - еще и слева.
				if( lCLexems[i].Type == Lexems.ComplexType.template ) {
					if( lCLexems[i].Lexem == "" ) {
						// не 1-й ли это элемент списка
						if( i < lCLexems.Count-1 && lCLexems[i+1].Type == Lexems.ComplexType.text ) {
							lCLexems[i+1].Lexem = "";
						}
						// если этот шаблон - самый первый из группы шаблонов, а до него есть текст, то еще и "ликвидируем" текст слева от него
						if( i == 1 && lCLexems[0].Type == Lexems.ComplexType.text ) {
							lCLexems[0].Lexem = "";
						}
					}
				}
			}
			// формирование строки
			foreach( Lexems.TPComplex lex in lCLexems ) {
				if( lex.Lexem.Length!=0 ) {
					sFileName += lex.Lexem;
				}
			}
			return sFileName;
			#endregion
		}
		
		#endregion
		
		#region Открытые методы
		/* получение простых лексем из шаблонной строки */
		public static List<Core.Templates.Lexems.TPSimple> GemSimpleLexems( string sLine ) {
			// разбиваем строку относительно [ и ]
			string [] sTemp = InsertSeparatorToSquareBracket( sLine ).Split( new char[] { cSeparator }, StringSplitOptions.RemoveEmptyEntries );
			// разбиваем строки sLexems, где нет [] относительно *
			List<string> lsLexems = new List<string>();
			foreach( string sStr in sTemp ) {
				if( sStr.IndexOf( '[' )!=-1 || sStr.IndexOf( '*' )==-1 )
					lsLexems.Add( sStr );
				else
					lsLexems.AddRange( InsertSeparatorToAsterik( sStr ).Split( new char[] { cSeparator }, StringSplitOptions.RemoveEmptyEntries ) );
			}
			// задаем лексемам их тип
			List<Lexems.TPSimple> lexems = new List<Lexems.TPSimple>();
			foreach( string s in lsLexems ) {
				if( !IsTemplateExsist( s ) ) {
					// постоянные символы
					lexems.Add( new Lexems.TPSimple( s, Lexems.SimpleType.const_text ) );
				} else {
					if( s.IndexOf( '[' )==-1 ) {
						// постоянный шаблон
						lexems.Add( new Lexems.TPSimple( s, Lexems.SimpleType.const_template ) );
					} else {
						// удаляем шаблон из строки и смотрим, есть ли там еще что, помимо него
						string st = GetTemplate( s );
						string sRem = s.Remove( s.IndexOf( st ), st.Length ).Remove( 0, 1 );
						sRem = sRem.Remove( (sRem.Length-1), 1 );
						if( sRem == "" ) {
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
		public static string Parse( string sFB2FilePath, List<Core.Templates.Lexems.TPSimple> lSLexems,
		                           ref IFBGenres fb2g, int nGenreIndex, int nAuthorIndex, int RegisterMode,
		                           int SpaceProcessMode, bool StrictMode, bool TranslitMode,
		                           ref SortingOptions sortOptions ) {
			#region Код
			string sFileName			= string.Empty;
			fB2Parser fb2				= new fB2Parser( sFB2FilePath );
			TitleInfo ti				= fb2.GetTitleInfo();
			string sLang				= ti.Lang;
			IList<Genre> lGenres		= ti.Genres;
			IList<Author> lAuthors		= ti.Authors;
			BookTitle btBookTitle		= ti.BookTitle;
			IList<Sequence> lSequences	= ti.Sequences;
			Date dBookDate				= ti.Date;
			PublishInfo pi		= null;
			string sYear		= null;
			Publisher pubPub	= null;
			City cCity			= null;
			if( pi != null ) {
				sYear	= pi.Year;
				pubPub	= pi.Publisher;
				cCity	= pi.City;
			}
			DocumentInfo di				= fb2.GetDocumentInfo();
			IList<Author> lfb2Authors	= di.Authors;

			foreach( Lexems.TPSimple lexem in lSLexems ) {
				switch( lexem.Type ) {
					case Lexems.SimpleType.const_text:
						// постоянные символы
						sFileName += lexem.Lexem;
						break;
					case Lexems.SimpleType.const_template:
						// постоянный шаблон
						switch( lexem.Lexem ) {
							case "*L*": // Язык Книги
								if( sLang == null || sLang.Length == 0 ) {
									sFileName += sortOptions.BookInfoNoLang;
								} else {
									sFileName += sLang.Trim();
								}
								break;
							case "*GROUP*": // Группа Жанров
								string sNoGroup = sortOptions.BookInfoNoGenreGroup;
								if( lGenres == null ) {
									sFileName += sNoGroup;
								} else {
									if( lGenres[nGenreIndex].Name==null || lGenres[nGenreIndex].Name.Trim().Length==0 ) {
										sFileName += sNoGroup;
									} else {
										// жанр есть
										string sgg = fb2g.GetFBGenreGroup( lGenres[nGenreIndex].Name.Trim() );// группа жанров
										// sgg.Length==0 для жанра, не соответствующего схеме
										sFileName += ( sgg.Length==0 ? sNoGroup : sgg );
									}
								}
								break;
							case "*GG*": // Группа Жанров\Жанр Книги
								string sNoGG = sortOptions.BookInfoNoGenreGroup;
								string sNo = sNoGG+"\\"+sortOptions.BookInfoNoGenre;// такого жанра нет в схеме
								if( lGenres == null ) {
									sFileName += sNo;
								} else {
									if( lGenres[nGenreIndex].Name==null || lGenres[nGenreIndex].Name.Trim().Length==0 ) {
										sFileName += sNo;
									} else {
										// жанр есть
										string sGenre	= lGenres[nGenreIndex].Name.Trim();
										string sgg		= fb2g.GetFBGenreGroup( sGenre );// группа жанров
										// sgg.Length==0 для жанра, не соответствующего схеме
										if( sortOptions.GenresTypeGenreSchema ) {
											// как в схеме
											sFileName += ( sgg.Length==0 ? sNoGG+"\\"+sGenre : sgg+"\\"+sGenre );
										} else {
											// жанр расшифровано
											sFileName += ( sgg.Length==0 ? sNoGG+"\\"+sGenre : sgg+"\\"+fb2g.GetFBGenreName( sGenre ) );
										}
									}
								}
								break;
							case "*G*": // Жанр Книги
								if( lGenres == null ) {
									sFileName += sortOptions.BookInfoNoGenre;
								} else {
									if( lGenres[nGenreIndex].Name==null || lGenres[nGenreIndex].Name.Trim().Length==0 ) {
										sFileName += sortOptions.BookInfoNoGenre;
									} else {
										// жанр есть
										if( sortOptions.GenresTypeGenreSchema ) {
											// как в схеме
											sFileName += lGenres[nGenreIndex].Name.Trim();
										} else {
											// жанр расшифровано
											string sg = fb2g.GetFBGenreName( lGenres[nGenreIndex].Name.Trim() );
											// sg.Length==0 для жанра, не соответствующего схеме
											sFileName += ( sg.Length==0 ? lGenres[nGenreIndex].Name.Trim() : sg );
										}
									}
								}
								break;
							case "*BAF*": // Имя Автора Книги
								if( lAuthors == null ) {
									sFileName += sortOptions.BookInfoNoFirstName;
								} else {
									if( lAuthors[nAuthorIndex].FirstName==null ) {
										sFileName += sortOptions.BookInfoNoFirstName;
									} else {
										if( lAuthors[nAuthorIndex].FirstName.Value.Trim().Length==0 ) {
											sFileName += sortOptions.BookInfoNoFirstName;
										} else {
											sFileName += lAuthors[nAuthorIndex].FirstName.Value.Trim();
										}
									}
								}
								break;
							case "*LF*": // Имя Автора Книги - 1-я Буква
								if( lAuthors == null ) {
									sFileName += sortOptions.BookInfoNoFirstName;
								} else {
									if( lAuthors[nAuthorIndex].FirstName == null ) {
										sFileName += sortOptions.BookInfoNoFirstName;
									} else {
										if( lAuthors[nAuthorIndex].FirstName.Value.Trim().Length == 0 ) {
											sFileName += sortOptions.BookInfoNoFirstName;
										} else {
											string sExsist = lAuthors[nAuthorIndex].FirstName.Value.Trim();
											sFileName += firstLetter( sExsist ).ToString().ToUpper();
										}
									}
								}
								break;
							case "*BAM*": // Отчество Автора Книги
								if( lAuthors == null ) {
									sFileName += sortOptions.BookInfoNoMiddleName;
								} else {
									if( lAuthors[nAuthorIndex].MiddleName==null ) {
										sFileName += sortOptions.BookInfoNoMiddleName;
									} else {
										if( lAuthors[nAuthorIndex].MiddleName.Value.Trim().Length==0 ) {
											sFileName += sortOptions.BookInfoNoMiddleName;
										} else {
											sFileName += lAuthors[nAuthorIndex].MiddleName.Value.Trim();
										}
									}
								}
								break;
							case "*LM*": // Отчество Автора Книги - 1-я Буква
								if( lAuthors == null ) {
									sFileName += sortOptions.BookInfoNoMiddleName;
								} else {
									if( lAuthors[nAuthorIndex].MiddleName==null ) {
										sFileName += sortOptions.BookInfoNoMiddleName;
									} else {
										if( lAuthors[nAuthorIndex].MiddleName.Value.Trim().Length==0 ) {
											sFileName += sortOptions.BookInfoNoMiddleName;
										} else {
											string sExsist = lAuthors[nAuthorIndex].MiddleName.Value.Trim();
											sFileName += firstLetter( sExsist ).ToString().ToUpper();
										}
									}
								}
								break;
							case "*BAL*": // Фамилия Автора Книги
								if( lAuthors == null ) {
									sFileName += sortOptions.BookInfoNoLastName;
								} else {
									if( lAuthors[nAuthorIndex].LastName==null ) {
										sFileName += sortOptions.BookInfoNoLastName;
									} else {
										if( lAuthors[nAuthorIndex].LastName.Value.Trim().Length==0 ) {
											sFileName += sortOptions.BookInfoNoLastName;
										} else {
											sFileName += lAuthors[nAuthorIndex].LastName.Value.Trim();
										}
									}
								}
								break;
							case "*LL*": // Фамилия Автора Книги - 1-я Буква
								if( lAuthors == null ) {
									sFileName += sortOptions.BookInfoNoLastName;
								} else {
									if( lAuthors[nAuthorIndex].LastName==null ) {
										sFileName += sortOptions.BookInfoNoLastName;
									} else {
										if( lAuthors[nAuthorIndex].LastName.Value.Trim().Length==0 ) {
											sFileName += sortOptions.BookInfoNoLastName;
										} else {
											string sExsist = lAuthors[nAuthorIndex].LastName.Value.Trim();
											sFileName += firstLetter( sExsist ).ToString().ToUpper();
										}
									}
								}
								break;
							case "*LBAL*": // 1-я Буква Фамилия Автора Книги \ Фамилия Автора Книги
								string sNoLN = sortOptions.BookInfoNoLastName;
								sNo = sNoLN[0] + "\\" + sNoLN;
								if( lAuthors == null ) {
									sFileName += sNoLN;
								} else {
									if( lAuthors[nAuthorIndex].LastName==null ) {
										sFileName += sNoLN + "\\" + sNoLN;
									} else {
										if( lAuthors[nAuthorIndex].LastName.Value.Trim().Length==0 ) {
											sFileName += sNoLN + "\\" + sNoLN;
										} else {
											string sExsist = lAuthors[nAuthorIndex].LastName.Value.Trim();
											sFileName += firstLetter( sExsist ).ToString().ToUpper() + "\\" + sExsist;
										}
									}
								}
								break;
							case "*BAN*": // Ник Автора Книги
								if( lAuthors == null ) {
									sFileName += sortOptions.BookInfoNoNickName;
								} else {
									if( lAuthors[nAuthorIndex].NickName==null ) {
										sFileName += sortOptions.BookInfoNoNickName;
									} else {
										if( lAuthors[nAuthorIndex].NickName.Value.Trim().Length==0 ) {
											sFileName += sortOptions.BookInfoNoNickName;
										} else {
											sFileName += lAuthors[nAuthorIndex].NickName.Value.Trim();
										}
									}
								}
								break;
							case "*LN*": // Ник Автора Книги - 1-я Буква
								if( lAuthors == null ) {
									sFileName += sortOptions.BookInfoNoNickName;
								} else {
									if( lAuthors[nAuthorIndex].NickName==null ) {
										sFileName += sortOptions.BookInfoNoNickName;
									} else {
										if( lAuthors[nAuthorIndex].NickName.Value.Trim().Length==0 ) {
											sFileName += sortOptions.BookInfoNoNickName;
										} else {
											string sExsist = lAuthors[nAuthorIndex].NickName.Value.Trim();
											sFileName += firstLetter( sExsist ).ToString().ToUpper();
										}
									}
								}
								break;
							case "*BT*": // Название Книги
								if( btBookTitle == null ) {
									sFileName += sortOptions.BookInfoNoBookTitle;
								} else {
									if( btBookTitle.Value==null || btBookTitle.Value.Trim().Length==0 ) {
										sFileName += sortOptions.BookInfoNoBookTitle;
									} else {
										sFileName += btBookTitle.Value.Trim();
									}
								}
								break;
							case "*SN*": // Серия Книги
								if( lSequences == null ) {
									sFileName += sortOptions.BookInfoNoSequence;
								} else {
									if( lSequences[0].Name==null || lSequences[0].Name.Trim().Length==0 ) {
										sFileName += sortOptions.BookInfoNoSequence;
									} else {
										sFileName += lSequences[0].Name.Trim();
									}
								}
								break;
							case "*SI*": // Номер Серии Книги
								if( lSequences == null ) {
									sFileName += sortOptions.BookInfoNoNSequence;
								} else {
									if( lSequences[0].Number==null ) {
										sFileName += sortOptions.BookInfoNoNSequence;
									} else {
										sFileName += lSequences[0].Number;
									}
								}
								break;
							case "*SII*": // Номер Серии Книги 0X
								if( lSequences == null ) {
									sFileName += sortOptions.BookInfoNoNSequence;
								} else {
									if( lSequences[0].Number==null ) {
										sFileName += sortOptions.BookInfoNoNSequence;
									} else {
										sFileName += MakeSII( lSequences[0].Number );
									}
								}
								break;
							case "*SIII*": // Номер Серии Книги 00X
								if( lSequences == null ) {
									sFileName += sortOptions.BookInfoNoNSequence;
								} else {
									if( lSequences[0].Number==null ) {
										sFileName += sortOptions.BookInfoNoNSequence;
									} else {
										sFileName += MakeSIII( lSequences[0].Number );
									}
								}
								break;
							case "*DT*": // Дата написания Книги (текст)
								if( dBookDate == null ) {
									sFileName += sortOptions.BookInfoNoDateText;
								} else {
									if( dBookDate.Text==null || dBookDate.Text.Trim().Length==0 ) {
										sFileName += sortOptions.BookInfoNoDateText;
									} else {
										sFileName += dBookDate.Text.Trim();
									}
								}
								break;
							case "*DV*": // Дата написания Книги (значение)
								if( dBookDate == null ) {
									sFileName += sortOptions.BookInfoNoDateText;
								} else {
									if( dBookDate.Value==null || dBookDate.Value.Trim().Length==0 ) {
										sFileName += sortOptions.BookInfoNoDateValue;
									} else {
										sFileName += dBookDate.Value.Trim();
									}
								}
								break;
							case "*YEAR*": // Год Издания Книги
								if( sYear==null || sYear.Length==0 ) {
									sFileName += sortOptions.PublishInfoNoYear;
								} else {
									sFileName += sYear.Trim();
								}
								break;
							case "*PUB*": // Издательство
								if( pubPub == null ) {
									sFileName += sortOptions.PublishInfoNoPublisher;
								} else {
									if( pubPub.Value==null || pubPub.Value.Trim().Length==0 ) {
										sFileName += sortOptions.PublishInfoNoPublisher;
									} else {
										sFileName += pubPub.Value.Trim();
									}
								}
								break;
							case "*CITY*": // Город Издательства
								if( cCity == null ) {
									sFileName += sortOptions.PublishInfoNoCity;
								} else {
									if( cCity.Value==null || cCity.Value.Trim().Length==0 ) {
										sFileName += sortOptions.PublishInfoNoCity;
									} else {
										sFileName += cCity.Value.Trim();
									}
								}
								break;
							case "*FB2AF*": // Имя сооздателя fb2-файла
								if( lfb2Authors == null ) {
									sFileName += sortOptions.FB2InfoNoFB2FirstName;
								} else {
									if( lfb2Authors[nAuthorIndex].FirstName==null ) {
										sFileName += sortOptions.FB2InfoNoFB2FirstName;
									} else {
										if( lfb2Authors[nAuthorIndex].FirstName.Value.Trim().Length==0 ) {
											sFileName += sortOptions.FB2InfoNoFB2FirstName;
										} else {
											sFileName += lfb2Authors[nAuthorIndex].FirstName.Value.Trim();
										}
									}
								}
								break;
							case "*FB2AM*": // Отчество сооздателя fb2-файла
								if( lfb2Authors == null ) {
									sFileName += sortOptions.FB2InfoNoFB2MiddleName;
								} else {
									if( lfb2Authors[nAuthorIndex].MiddleName==null ) {
										sFileName += sortOptions.FB2InfoNoFB2MiddleName;
									} else {
										if( lfb2Authors[nAuthorIndex].MiddleName.Value.Trim().Length==0 ) {
											sFileName += sortOptions.FB2InfoNoFB2MiddleName;
										} else {
											sFileName += lfb2Authors[nAuthorIndex].MiddleName.Value.Trim();
										}
									}
								}
								break;
							case "*FB2AL*": // Фамилия сооздателя fb2-файла
								if( lfb2Authors == null ) {
									sFileName += sortOptions.FB2InfoNoFB2LastName;
								} else {
									if( lfb2Authors[nAuthorIndex].LastName==null ) {
										sFileName += sortOptions.FB2InfoNoFB2LastName;
									} else {
										if( lfb2Authors[nAuthorIndex].LastName.Value.Trim().Length==0 ) {
											sFileName += sortOptions.FB2InfoNoFB2LastName;
										} else {
											sFileName += lfb2Authors[nAuthorIndex].LastName.Value.Trim();
										}
									}
								}
								break;
							case "*FB2AN*": // Ник сооздателя fb2-файла
								if( lfb2Authors == null ) {
									sFileName += sortOptions.FB2InfoNoFB2NickName;
								} else {
									if( lfb2Authors[nAuthorIndex].NickName==null ) {
										sFileName += sortOptions.FB2InfoNoFB2NickName;
									} else {
										if( lfb2Authors[nAuthorIndex].NickName.Value.Trim().Length==0 ) {
											sFileName += sortOptions.FB2InfoNoFB2NickName;
										} else {
											sFileName += lfb2Authors[nAuthorIndex].NickName.Value.Trim();
										}
									}
								}
								break;
								default :
									sFileName += "";
								break;
						}
						break;
					case Lexems.SimpleType.conditional_template:
						// условный шаблон
						switch( lexem.Lexem ) {
							case "[*L*]": // Язык Книги
								if( sLang!=null || sLang.Length!=0 ) {
									sFileName += sLang.Trim();
								}
								break;
							case "[*GG*]": // Группа Жанров\Жанр Книги
								string sNoGG = sortOptions.BookInfoNoGenreGroup;// такого жанра (группы) нет в схеме
								if( lGenres != null ) {
									if( lGenres[nGenreIndex].Name!=null || lGenres[nGenreIndex].Name.Trim().Length!=0 ) {
										// жанр есть
										string sGenre	= lGenres[nGenreIndex].Name.Trim();
										string sgg		= fb2g.GetFBGenreGroup( sGenre );// группа жанров
										// sgg.Length==0 для жанра, не соответствующего схеме
										if( sortOptions.GenresTypeGenreSchema ) {
											// как в схеме
											sFileName += ( sgg.Length==0 ? sNoGG+"\\"+sGenre : sgg+"\\"+sGenre );
										} else {
											// жанр расшифровано
											sFileName += ( sgg.Length==0 ? sNoGG+"\\"+sGenre : sgg+"\\"+fb2g.GetFBGenreName( sGenre ) );
										}
									}
								}
								break;
							case "[*G*]": // Жанр Книги
								if( lGenres != null ) {
									if( lGenres[nGenreIndex].Name!=null || lGenres[nGenreIndex].Name.Trim().Length!=0 ) {
										if( sortOptions.GenresTypeGenreSchema ) {
											// как в схеме
											sFileName += lGenres[nGenreIndex].Name.Trim();
										} else {
											// жанр расшифровано
											string sg = fb2g.GetFBGenreName( lGenres[nGenreIndex].Name.Trim() );
											// sg.Length==0 для жанра, не соответствующего схеме
											sFileName += ( sg.Length==0 ? lGenres[nGenreIndex].Name.Trim() : sg );
										}
									}
								}
								break;
							case "[*BAF*]": // Имя Автора Книги
								if( lAuthors != null ) {
									if( lAuthors[nAuthorIndex].FirstName != null ) {
										if( lAuthors[nAuthorIndex].FirstName.Value.Trim().Length!=0 ) {
											sFileName += lAuthors[nAuthorIndex].FirstName.Value.Trim();
										}
									}
								}
								break;
							case "[*LF*]": // Имя Автора Книги - 1-я Буква
								if( lAuthors != null ) {
									if( lAuthors[nAuthorIndex].FirstName != null ) {
										if( lAuthors[nAuthorIndex].FirstName.Value.Trim().Length!=0 ) {
											string sExsist = lAuthors[nAuthorIndex].FirstName.Value.Trim();
											sFileName += firstLetter( sExsist ).ToString().ToUpper();
										}
									}
								}
								break;
							case "[*BAM*]": // Отчество Автора Книги
								if( lAuthors != null ) {
									if( lAuthors[nAuthorIndex].MiddleName != null ) {
										if( lAuthors[nAuthorIndex].MiddleName.Value.Trim().Length!=0 ) {
											sFileName += lAuthors[nAuthorIndex].MiddleName.Value.Trim();
										}
									}
								}
								break;
							case "[*LM*]": // Отчество Автора Книги - 1-я Буква
								if( lAuthors != null ) {
									if( lAuthors[nAuthorIndex].MiddleName != null ) {
										if( lAuthors[nAuthorIndex].MiddleName.Value.Trim().Length!=0 ) {
											string sExsist = lAuthors[nAuthorIndex].MiddleName.Value.Trim();
											sFileName += firstLetter( sExsist ).ToString().ToUpper();
										}
									}
								}
								break;
							case "[*BAL*]": // Фамилия Автора Книги
								if( lAuthors != null ) {
									if( lAuthors[nAuthorIndex].LastName != null ) {
										if( lAuthors[nAuthorIndex].LastName.Value.Trim().Length!=0 ) {
											sFileName += lAuthors[nAuthorIndex].LastName.Value.Trim();
										}
									}
								}
								break;
							case "[*LL*]": // Фамилия Автора Книги- 1-я Буква
								if( lAuthors != null ) {
									if( lAuthors[nAuthorIndex].LastName != null ) {
										if( lAuthors[nAuthorIndex].LastName.Value.Trim().Length!=0 ) {
											string sExsist = lAuthors[nAuthorIndex].LastName.Value.Trim();
											sFileName += firstLetter( sExsist ).ToString().ToUpper();
										}
									}
								}
								break;
							case "[*LBAL*]": // 1-я Буква Фамилия Автора Книги \ Фамилия Автора Книги
								if( lAuthors != null ) {
									if( lAuthors[nAuthorIndex].LastName != null ) {
										if( lAuthors[nAuthorIndex].LastName.Value.Trim().Length!=0 ) {
											string sExsist = lAuthors[nAuthorIndex].LastName.Value.Trim();
											sFileName += firstLetter( sExsist ).ToString().ToUpper() + "\\" + sExsist;
										}
									}
								}
								break;
							case "[*BAN*]": // Ник Автора Книги
								if( lAuthors != null ) {
									if( lAuthors[nAuthorIndex].NickName != null ) {
										if( lAuthors[nAuthorIndex].NickName.Value.Trim().Length!=0 ) {
											sFileName += lAuthors[nAuthorIndex].NickName.Value.Trim();
										}
									}
								}
								break;
							case "[*LN*]": // Ник Автора Книги- 1-я Буква
								if( lAuthors != null ) {
									if( lAuthors[nAuthorIndex].NickName != null ) {
										if( lAuthors[nAuthorIndex].NickName.Value.Trim().Length!=0 ) {
											string sExsist = lAuthors[nAuthorIndex].NickName.Value.Trim();
											sFileName += firstLetter( sExsist ).ToString().ToUpper();
										}
									}
								}
								break;
							case "[*BT*]": // Название Книги
								if( btBookTitle != null ) {
									if( btBookTitle.Value!=null || btBookTitle.Value.Trim().Length!=0 ) {
										sFileName += btBookTitle.Value.Trim();
									}
								}
								break;
							case "[*SN*]": // Серия Книги
								if( lSequences != null ) {
									if( lSequences[0].Name!=null || lSequences[0].Name.Trim().Length!=0 ) {
										sFileName += lSequences[0].Name.Trim();
									}
								}
								break;
							case "[*SI*]": // Номер Серии Книги
								if( lSequences != null ) {
									if( lSequences[0].Number != null ) {
										sFileName += lSequences[0].Number;
									}
								}
								break;
							case "[*SII*]": // Номер Серии Книги 0X
								if( lSequences != null ) {
									if( lSequences[0].Number != null ) {
										sFileName += MakeSII( lSequences[0].Number );
									}
								}
								break;
							case "[*SIII*]": // Номер Серии Книги 00X
								if( lSequences != null ) {
									if( lSequences[0].Number != null ) {
										sFileName += MakeSIII( lSequences[0].Number );
									}
								}
								break;
							case "[*DT*]": // Дата написания Книги (текст)
								if( dBookDate != null ) {
									if( dBookDate.Text!=null || dBookDate.Text.Trim().Length!=0 ) {
										sFileName += dBookDate.Text.Trim();
									}
								}
								break;
							case "[*DV*]": // Дата написания Книги (значение)
								if( dBookDate != null ) {
									if( dBookDate.Value!=null || dBookDate.Value.Trim().Length!=0 ) {
										sFileName += dBookDate.Value.Trim();
									}
								}
								break;
							case "[*YEAR*]": // Год Издания Книги
								if( sYear != null || sYear.Length != 0 ) {
									sFileName += sYear.Trim();
								}
								break;
							case "[*PUB*]": // Издательство
								if( pubPub != null ) {
									if( pubPub.Value!=null || pubPub.Value.Trim().Length!=0 ) {
										sFileName += pubPub.Value.Trim();
									}
								}
								break;
							case "[*CITY*]": // Город Издательства
								if( cCity != null ) {
									if( cCity.Value!=null || cCity.Value.Trim().Length!=0 ) {
										sFileName += cCity.Value.Trim();
									}
								}
								break;
							case "[*FB2AF*]": // Имя создателя fb2-файла
								if( lfb2Authors != null ) {
									if( lfb2Authors[nAuthorIndex].FirstName != null ) {
										if( lfb2Authors[nAuthorIndex].FirstName.Value.Trim().Length!=0 ) {
											sFileName += lfb2Authors[nAuthorIndex].FirstName.Value.Trim();
										}
									}
								}
								break;
							case "[*FB2AM*]": // Отчество создателя fb2-файла
								if( lfb2Authors != null ) {
									if( lfb2Authors[nAuthorIndex].MiddleName != null ) {
										if( lfb2Authors[nAuthorIndex].MiddleName.Value.Trim().Length!=0 ) {
											sFileName += lfb2Authors[nAuthorIndex].MiddleName.Value.Trim();
										}
									}
								}
								break;
							case "[*FB2AL*]": // Фамилия создателя fb2-файла
								if( lfb2Authors != null ) {
									if( lfb2Authors[nAuthorIndex].LastName != null ) {
										if( lfb2Authors[nAuthorIndex].LastName.Value.Trim().Length!=0 ) {
											sFileName += lfb2Authors[nAuthorIndex].LastName.Value.Trim();
										}
									}
								}
								break;
							case "[*FB2AN*]": // Ник создателя fb2-файла
								if( lfb2Authors != null ) {
									if( lfb2Authors[nAuthorIndex].NickName != null ) {
										if( lfb2Authors[nAuthorIndex].NickName.Value.Trim().Length!=0 ) {
											sFileName += lfb2Authors[nAuthorIndex].NickName.Value.Trim();
										}
									}
								}
								break;
								default :
									//sFileName += "";
									break;
						}
						break;
					case Lexems.SimpleType.conditional_group:
						// условная группа
						sFileName += ParseComplexGroup( lexem.Lexem, sLang, lGenres, lAuthors, btBookTitle,
						                               lSequences, fb2g, dBookDate, sYear, pubPub, cCity,
						                               lfb2Authors,
						                               ref sortOptions, nGenreIndex, nAuthorIndex );
						break;
						default :
							// постоянные символы
							sFileName += lexem.Lexem;
						break;
				}
			}
			// если с начала или в конце строки есть один или несколько \ - то убираем их
			Regex rx = new Regex( @"^\\+" );
			sFileName = rx.Replace( sFileName, "" );
			rx = new Regex( @"\\+$" );
			sFileName = rx.Replace( sFileName, "" );
			rx = new Regex( @"\\+" );
			sFileName = rx.Replace( sFileName, "\\" );
			// если перед \ есть пробелы - убираем их (иначе архиваторы не архивируют файл)
			rx = new Regex( @" +\\" );
			sFileName = rx.Replace( sFileName, "\\" );

			return StringProcessing.MakeGeneralWorkedPath( sFileName, RegisterMode, SpaceProcessMode, StrictMode, TranslitMode );
			#endregion
		}
		
		#endregion
	}
}
