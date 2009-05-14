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

using FB2.FB2Parsers;
using FB2.Common;
using FB2.Description;
using FB2.Description.TitleInfo;
using FB2.Description.DocumentInfo;
using FB2.Description.PublishInfo;
using FB2.Description.CustomInfo;
using FB2.Description.Common;
using FB2.Genres;
using StringProcessing;

using fB2Parser = FB2.FB2Parsers.FB2Parser;

namespace Templates {
	/// <summary>
	/// Description of TemplatesParser.
	/// </summary>
	public class TemplatesParser : Lexems.AllTemplates
	{
		#region Закрытые данные
		private static char cSeparator = '↑';
		#endregion
		
		public TemplatesParser()
		{
		}
		
		#region Закрытые Вспомогательные методы
		private static string InsertSeparatorToAsterik( string sLine ) {
			// вставка разделителя слева от открывающей * и справа от закрывающей *
			if( sLine==null || sLine.Length==0 ) return sLine;
			if( sLine.IndexOf( '*' )==-1 ) return sLine;
			string sTemp = "";
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
				} else {
					sTemp += sLine[i];
				}
			}
			return sTemp;
		}
		
		private static string InsertSeparatorToSquareBracket( string sLine ) {
			// вставка разделителя слева от [ и справа от ]
			if( sLine==null || sLine.Length==0 ) return sLine;
			string sTemp = "";
			for( int i=0; i!=sLine.Length; ++i ) {
				if( sLine[i]=='[' ) {
					sTemp += cSeparator;
					sTemp += sLine[i];
				} else {
					sTemp += sLine[i];
					if( sLine[i]==']' ) {
						sTemp += cSeparator;
					}
				}
			}
			return sTemp;
		}
		
		private static int CountElement( string sLine, char cChar ) {
			// подсчет числа элементов cChar в строке sLine
			int nCount = 0;
			for( int i=0; i!=sLine.Length; ++i ) {
				if( sLine[i]== cChar ) {
					++nCount;
				}
			}
			return  nCount;
		}
		
		private static bool IsTemplateExsist( string sLine ) {
			// проверка, есть ли шаблоны в строке
			foreach( string t in m_sAllTemplates ) {
				if( sLine.IndexOf( t )!=-1 ) {
					return true;
				}
			}
			return false;
		}
		
		private static string GetTemplate( string sLine ) {
			// возвращает 1-й шаблон в строке, если он есть, или ""
			foreach( string t in m_sAllTemplates ) {
				if( sLine.IndexOf( t )!=-1 ) {
					return t;
				}
			}
			return "";
		}
		
		private static List<Lexems.TPComplex> GemComplexLexems( string sLine ) {
			/* получение лексем из сложной группы */
			// разбиваем строку относительно *
			string str = sLine.Remove( 0, 1 );
			str = str.Remove( (str.Length-1), 1 );
			string [] sLexems = InsertSeparatorToAsterik( str ).Split( new char[] { cSeparator }, StringSplitOptions.RemoveEmptyEntries );
			// задаем лексемам их тип
			List<Lexems.TPComplex> lexems = new List<Lexems.TPComplex>();
			foreach( string s in sLexems ) {
				if( !IsTemplateExsist( s ) ) {
					// символы
					lexems.Add( new Lexems.TPComplex( s, Lexems.ComplexType.text ) );
				} else {
					// шаблон
					lexems.Add( new Lexems.TPComplex( s, Lexems.ComplexType.template ) );
				}
			}
			return lexems;
		}
		
		private static string ParseComplexGroup( string sLine, string sLang, IList<Genre> lGenres, IList<Author> lAuthors, 
												BookTitle btBookTitle, IList<Sequence> lSequences, IFBGenres fb2g,
												int nGenreIndex, int nAuthorIndex ) {
			// парсинг сложных условных групп
			#region Код
			string sFileName = "";
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
							case "*GG*": // Группа Жанров\Жанр Книги
								string sNoGG = Settings.SettingsFM.GetFMNoGenreGroup();// такого жанра (группы) нет в схеме
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
										if( Settings.SettingsFM.ReadGenreTypeMode() ) {
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
										if( Settings.SettingsFM.ReadGenreTypeMode() ) {
											// как в схеме
											lexem.Lexem = lGenres[nGenreIndex].Name.Trim();
										} else {
											// жанр расшифровано
											string sg = fb2g.GetFBGenreName( lGenres[nGenreIndex].Name.Trim() );
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
							case "*LBAL*": // 1-я Буква Фамилия Автора Книги \ Фамилия Автора Книги
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
											lexem.Lexem = sExsist[0] + "\\" + sExsist;
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
							case "*SI*": // Номер Серии Книги
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
		public static List<Lexems.TPSimple> GemSimpleLexems( string sLine ) {
			/* получение простых лексем из шаблонной строки */
			// разбиваем строку относительно [ и ]
			string [] sTemp = InsertSeparatorToSquareBracket( sLine ).Split( new char[] { cSeparator }, StringSplitOptions.RemoveEmptyEntries );
			// разбиваем строки sLexems, где нет [] относительно *
			List<string> lsLexems = new List<string>();
			foreach( string sStr in sTemp ) {
				if( sStr.IndexOf( '[' )!=-1 || sStr.IndexOf( '*' )==-1 ) {
					lsLexems.Add( sStr );
				} else {
					lsLexems.AddRange( InsertSeparatorToAsterik( sStr ).Split( new char[] { cSeparator }, StringSplitOptions.RemoveEmptyEntries ) );
				}
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
		
		public static string Parse( string sFB2FilePath, List<Lexems.TPSimple> lSLexems, bool bFB21, int nGenreIndex, int nAuthorIndex ) {
			// формирование имени файла на основе данных Description и шаблонов подстановки
			string sFileName = "";
			fB2Parser fb2 = new fB2Parser( sFB2FilePath );
			TitleInfo ti = fb2.GetTitleInfo();
			string sLang = ti.Lang;
			IList<Genre> lGenres = ti.Genres;
			IList<Author> lAuthors = ti.Authors;
			BookTitle btBookTitle = ti.BookTitle;
			IList<Sequence> lSequences = ti.Sequences;
			IFBGenres fb2g = null;
			if( bFB21 ) {
				fb2g = new FB21Genres();
			} else {
				fb2g = new FB22Genres();
			}

			foreach( Lexems.TPSimple lexem in lSLexems ) {
				switch( lexem.Type ) {
					case Lexems.SimpleType.const_text:
						// постоянные символы
						sFileName += lexem.Lexem.Trim();
						break;
					case Lexems.SimpleType.const_template:
						// постоянный шаблон
						switch( lexem.Lexem ) {
							case "*L*": // Язык Книги
								if( sLang == null || sLang.Length==0 ) {
									sFileName += Settings.SettingsFM.GetFMNoLang();
								} else {
								sFileName += sLang.Trim();
								}
								break;
							case "*GG*": // Группа Жанров\Жанр Книги
								string sNoGG = Settings.SettingsFM.GetFMNoGenreGroup();
								string sNo = sNoGG+"\\"+Settings.SettingsFM.GetFMNoGenre();// такого жанра нет в схеме
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
										if( Settings.SettingsFM.ReadGenreTypeMode() ) {
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
									sFileName += Settings.SettingsFM.GetFMNoGenre();
								} else {
									if( lGenres[nGenreIndex].Name==null || lGenres[nGenreIndex].Name.Trim().Length==0 ) {
										sFileName += Settings.SettingsFM.GetFMNoGenre();
									} else {
										// жанр есть
										if( Settings.SettingsFM.ReadGenreTypeMode() ) {
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
									sFileName += Settings.SettingsFM.GetFMNoFirstName();
								} else {
									if( lAuthors[nAuthorIndex].FirstName==null ) {
										sFileName += Settings.SettingsFM.GetFMNoFirstName();
									} else {
										if( lAuthors[nAuthorIndex].FirstName.Value.Trim().Length==0 ) {
											sFileName += Settings.SettingsFM.GetFMNoFirstName();
										} else {
											sFileName += lAuthors[nAuthorIndex].FirstName.Value.Trim();
										}
									}
								}
								break;
							case "*BAM*": // Отчество Автора Книги
								if( lAuthors == null ) {
									sFileName += Settings.SettingsFM.GetFMNoMiddleName();
								} else {
									if( lAuthors[nAuthorIndex].MiddleName==null ) {
										sFileName += Settings.SettingsFM.GetFMNoMiddleName();
									} else {
										if( lAuthors[nAuthorIndex].MiddleName.Value.Trim().Length==0 ) {
											sFileName += Settings.SettingsFM.GetFMNoMiddleName();
										} else {
											sFileName += lAuthors[nAuthorIndex].MiddleName.Value.Trim();
										}
									}
								}
								break;
							case "*BAL*": // Фамилия Автора Книги
								if( lAuthors == null ) {
									sFileName += Settings.SettingsFM.GetFMNoLastName();
								} else {
									if( lAuthors[nAuthorIndex].LastName==null ) {
										sFileName += Settings.SettingsFM.GetFMNoLastName();
									} else {
										if( lAuthors[nAuthorIndex].LastName.Value.Trim().Length==0 ) {
											sFileName += Settings.SettingsFM.GetFMNoLastName();
										} else {
											sFileName += lAuthors[nAuthorIndex].LastName.Value.Trim();
										}
									}
								}
								break;
							case "*LBAL*": // 1-я Буква Фамилия Автора Книги \ Фамилия Автора Книги
								string sNoL = Settings.SettingsFM.GetFMNoLastName();
								sNo = sNoL[0] + "\\" + sNoL;
								if( lAuthors == null ) {
									sFileName += sNoL;
								} else {
									if( lAuthors[nAuthorIndex].LastName==null ) {
										sFileName += sNoL;
									} else {
										if( lAuthors[nAuthorIndex].LastName.Value.Trim().Length==0 ) {
											sFileName += sNoL;
										} else {
											string sExsist = lAuthors[nAuthorIndex].LastName.Value.Trim();
											sFileName += sExsist[0] + "\\" + sExsist;
										}
									}
								}
								break;
							case "*BAN*": // Ник Автора Книги
								if( lAuthors == null ) {
									sFileName += Settings.SettingsFM.GetFMNoNickName();
								} else {
									if( lAuthors[nAuthorIndex].NickName==null ) {
										sFileName += Settings.SettingsFM.GetFMNoNickName();
									} else {
										if( lAuthors[nAuthorIndex].NickName.Value.Trim().Length==0 ) {
											sFileName += Settings.SettingsFM.GetFMNoNickName();
										} else {
											sFileName += lAuthors[nAuthorIndex].NickName.Value.Trim();
										}
									}
								}
								break;
							case "*BT*": // Название Книги
								if( btBookTitle == null ) {
									sFileName += Settings.SettingsFM.GetFMNoBookTitle();
								} else {
									if( btBookTitle.Value==null || btBookTitle.Value.Trim().Length==0 ) {
										sFileName += Settings.SettingsFM.GetFMNoBookTitle();
									} else {
										sFileName += btBookTitle.Value.Trim();
									}
								}
								break;
							case "*SN*": // Серия Книги
								if( lSequences == null ) {
									sFileName += Settings.SettingsFM.GetFMNoSequence();
								} else {
									if( lSequences[0].Name==null || lSequences[0].Name.Trim().Length==0 ) {
										sFileName += Settings.SettingsFM.GetFMNoSequence();
									} else {
										sFileName += lSequences[0].Name.Trim();
									}
								}
								break;
							case "*SI*": // Номер Серии Книги
								if( lSequences == null ) {
									sFileName += Settings.SettingsFM.GetFMNoNSequence();
								} else {
									if( lSequences[0].Number==null ) {
										sFileName += Settings.SettingsFM.GetFMNoNSequence();
									} else {
										sFileName += lSequences[0].Number;
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
								if( sLang == null || sLang.Length==0 ) {
									sFileName += "";
								} else {
									sFileName += sLang.Trim();
								}
								break;
							case "[*GG*]": // Группа Жанров\Жанр Книги
								string sNoGG = Settings.SettingsFM.GetFMNoGenreGroup();// такого жанра (группы) нет в схеме
								if( lGenres != null ) {
									if( lGenres[nGenreIndex].Name!=null || lGenres[nGenreIndex].Name.Trim().Length!=0 ) {
										// жанр есть
										string sGenre	= lGenres[nGenreIndex].Name.Trim();
										string sgg		= fb2g.GetFBGenreGroup( sGenre );// группа жанров
										// sgg.Length==0 для жанра, не соответствующего схеме
										if( Settings.SettingsFM.ReadGenreTypeMode() ) {
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
										if( Settings.SettingsFM.ReadGenreTypeMode() ) {
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
							case "[*BAM*]": // Отчество Автора Книги
								if( lAuthors != null ) {
									if( lAuthors[nAuthorIndex].MiddleName != null ) {
										if( lAuthors[nAuthorIndex].MiddleName.Value.Trim().Length!=0 ) {
											sFileName += lAuthors[nAuthorIndex].MiddleName.Value.Trim();
										}
									}
								}
								break;
							case "[*BAL*]": // Фамилия Автора Книги
								if( lAuthors != null ) {
									if( lAuthors[nAuthorIndex].LastName != null ) {
										if( lAuthors[nAuthorIndex].LastName.Value.Trim() != "" ) {
											sFileName += lAuthors[nAuthorIndex].LastName.Value.Trim();
										}
									}
								}
								break;
							case "[*LBAL*]": // 1-я Буква Фамилия Автора Книги \ Фамилия Автора Книги
								if( lAuthors != null ) {
									if( lAuthors[nAuthorIndex].LastName != null ) {
										if( lAuthors[nAuthorIndex].LastName.Value.Trim().Length!=0 ) {
											string sExsist = lAuthors[nAuthorIndex].LastName.Value.Trim();
											sFileName += sExsist[0] + "\\" + sExsist;
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
							default :
								//sFileName += "";
								break;
						}
						break;
					case Lexems.SimpleType.conditional_group:
						// условная группа
						sFileName += ParseComplexGroup( lexem.Lexem, sLang, lGenres, lAuthors, btBookTitle,
						                               lSequences, fb2g, nGenreIndex, nAuthorIndex );
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
			return StringProcessing.StringProcessing.GetGeneralWorkedPath( sFileName );
		}
			
		#endregion
	}
}
