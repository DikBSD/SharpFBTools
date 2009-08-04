/*
 * Created by SharpDevelop.
 * User: ����� �������� (DikBSD)
 * Date: 04.08.2009
 * Time: 17:07
 * 
 * License: GPL 2.1
 */
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Core.FB2.FB2Parsers;
using Core.FB2.Description.TitleInfo;
using Core.FB2.Description.Common;
using Core.FB2.Genres;
//using FB2.Common;
//using FB2.Description;
//using FB2.Description.DocumentInfo;
//using FB2.Description.PublishInfo;
//using FB2.Description.CustomInfo;
//using Templates.Lexems;
//using Templates;
//using StringProcessing;
//using FilesWorker;


using fB2Parser = Core.FB2.FB2Parsers.FB2Parser;

namespace Core.BookSorting
{
	/// <summary>
	/// Description of FB2SelectedSorting.
	/// </summary>
	public class FB2SelectedSorting
	{
		public FB2SelectedSorting()
		{
		}
		#region �������� ������
		public List<SelectedSortQueryCriteria> MakeSelectedSortQuerysList(
								string sLang, string sLast, string sFirst, string sMiddle, string sNick,
								string sGGroup, string sGenre, string sSequence, string sBTitle, string sExactFit,
								bool bGenresFB21Scheme ) {
			// ��������� ������ ��������� ������ ��� ��������� ����������
			List<SelectedSortQueryCriteria> lSSQCList = new List<SelectedSortQueryCriteria>();
			List<string> lsGenres = null; // ��������� ������ ������ �� ���������� ������ ������
			// "���������" ���� �����
			if( sLang.Length!=0 ) {
				sLang = sLang.Substring( sLang.IndexOf( "(" )+1 );
				sLang = sLang.Remove( sLang.IndexOf( ")" ) );
			}
			// ���� ���� ����, �� "���������" ��� �� ������
			if( sGenre.Length!=0 ) {
				sGenre = sGenre.Substring( sGenre.IndexOf( "(" )+1 );
				sGenre = sGenre.Remove( sGenre.IndexOf( ")" ) );
			}
			// ���� ���� ������ ������, �� ����������� �� � ������ "��" ������
			if( sGGroup.Length!=0 ) {
				IFBGenres fb2g = null;
				if( bGenresFB21Scheme ) {
					fb2g = new FB21Genres();
				} else {
					fb2g = new FB22Genres();
				}
				lsGenres = fb2g.GetFBGenresForGroup( sGGroup );
			}
			// ��������� ������ ��������� ������ � ����������� �� ������� ����� ������
			if( lsGenres==null ) {
				lSSQCList.Add( new SelectedSortQueryCriteria(
				sLang,sGGroup,sGenre,sLast,sFirst,sMiddle,sNick,sSequence,sBTitle,sExactFit=="��"?true:false ) );
			} else {
				foreach( string sG in lsGenres ) {
					lSSQCList.Add( new SelectedSortQueryCriteria(
							sLang,"",sG,sLast,sFirst,sMiddle,sNick,sSequence,sBTitle,sExactFit=="��"?true:false ) );
				}
			}
			return lSSQCList;
		}
		
		public bool IsConformity( string sFromFilePath, List<SelectedSortQueryCriteria> lSSQCList ) {
			// ��������, ������������� �� ������� ���� �������� ������ ��� ��������� ����������
			fB2Parser fb2	= null;
			TitleInfo ti	= null;
			try {
				fb2	= new fB2Parser( sFromFilePath );
				ti	= fb2.GetTitleInfo();
				if( ti==null ) return false;
			} catch {
				return false;
			}
			bool bRet = true; // ����, ����� �� ������������
			string			sFB2Lang		= ti.Lang;
			BookTitle		sFB2BookTitle	= ti.BookTitle;
			IList<Genre>	lFB2Genres		= ti.Genres;
			IList<Author>	lFB2Authors		= ti.Authors;
			IList<Sequence>	lFB2Sequences	= ti.Sequences;
			string sLang, sFirstName, sGenre, sMiddleName, sLastName, sNickName, sSequence, sBookTitle;
			bool bExactFit;
			Regex re = null;
			foreach( SelectedSortQueryCriteria ssqc in lSSQCList ) {
				sLang		= ssqc.Lang;
				sGenre		= ssqc.Genre;
				sFirstName	= ssqc.FirstName;
				sMiddleName	= ssqc.MiddleName;
				sLastName	= ssqc.LastName;
				sNickName	= ssqc.NickName;
				sSequence	= ssqc.Sequence;
				sBookTitle	= ssqc.BookTitle;
				bExactFit	= ssqc.ExactFit;
				// �������� ����� �����
				if( sFB2Lang != null ) {
					if( sLang.Length != 0 ) {
						if( sFB2Lang != sLang ) {
							bRet = false; continue;
						}
					}
				} else {
					// � ����� ���� ����� ���
					if( sLang.Length != 0 ) {
						bRet = false; continue;
					}
				}
				// �������� ����� �����
				bool b = false;
				if( lFB2Genres != null ) {
					if( sGenre.Length != 0 ) {
						foreach( Genre gfb2 in lFB2Genres ) {
							if( gfb2.Name != null ) {
								if( gfb2.Name == sGenre ) {
									b = true; break;
								}
							} else {
								bRet = false; continue;
							}
						}
						if( !b ) {
							bRet = false; continue;
						}
					}
				} else {
					// � ����� ���� ����� ���
					if( sGenre.Length != 0 ) {
						bRet = false; continue;
					}
				}
				// �������� ����� �����
				b = false;
				if( lFB2Sequences != null ) {
					if( sSequence.Length != 0 ) {
						foreach( Sequence sfb2 in lFB2Sequences ) {
							if( sfb2.Name != null ) {
								if( bExactFit ) {
									// ������ ������������
									if( sfb2.Name == sSequence ) {
										b = true; break;
									}
								} else {
									re = new Regex( sSequence, RegexOptions.IgnoreCase );
									if( re.IsMatch( sfb2.Name ) ) {
										b = true; break;
									}
								}
							} else {
								bRet = false; continue;
							}
						}
						if( !b ) {
							bRet = false; continue;
						}
					}
				} else {
					// � ����� ���� ����� ���
					if( sSequence.Length != 0 ) {
						bRet = false; continue;
					}
				}
				// �������� ������ �����
				if( lFB2Authors != null ) {
					b = false;
					if( sFirstName.Length != 0 ) {
						foreach( Author afb2 in lFB2Authors ) {
							if( afb2.FirstName != null ) {
								if( bExactFit ) {
									// ������ ������������
									if( afb2.FirstName.Value == sFirstName ) {
										b = true; break;
									}
								} else {
									re = new Regex( sFirstName, RegexOptions.IgnoreCase );
									if( re.IsMatch( afb2.FirstName.Value ) ) {
										b = true; break;
									}
								}
							} else {
								bRet = false; continue;
							}
						}
						if( !b ) {
							bRet = false; continue;
						}
					}
					b = false;
					if( sMiddleName.Length != 0 ) {
						foreach( Author afb2 in lFB2Authors ) {
							if( afb2.MiddleName != null ) {
								if( bExactFit ) {
									// ������ ������������
									if( afb2.MiddleName.Value == sMiddleName ) {
										b = true; break;
									}
								} else {
									re = new Regex( sMiddleName, RegexOptions.IgnoreCase );
									if( re.IsMatch( afb2.MiddleName.Value ) ) {
										b = true; break;
									}
								}
							} else {
								bRet = false; continue;
							}
						}
						if( !b ) {
							bRet = false; continue;
						}
					}
					b = false;
					if( sLastName.Length != 0 ) {
						foreach( Author afb2 in lFB2Authors ) {
							if( afb2.LastName != null ) {
								if( bExactFit ) {
									// ������ ������������
									if( afb2.LastName.Value == sLastName ) {
										b = true; break;
									}
								} else {
									re = new Regex( sLastName, RegexOptions.IgnoreCase );
									if( re.IsMatch( afb2.LastName.Value ) ) {
										b = true; break;
									}
								}
							} else {
								bRet = false; continue;
							}
						}
						if( !b ) {
							bRet = false; continue;
						}
					}
					b = false;
					if( sNickName.Length != 0 ) {
						foreach( Author afb2 in lFB2Authors ) {
							if( afb2.NickName != null ) {
								if( bExactFit ) {
									// ������ ������������
									if( afb2.NickName.Value == sNickName ) {
										b = true; break;
									}
								} else {
									re = new Regex( sNickName, RegexOptions.IgnoreCase );
									if( re.IsMatch( afb2.NickName.Value ) ) {
										b = true; break;
									}
								}
							} else {
								bRet = false; continue;
							}
						}
						if( !b ) {
							bRet = false; continue;
						}
					}
				} else {
					// � ����� ����� ������ ���
					if( sFirstName.Length != 0 || sMiddleName.Length != 0 ||
					  	sNickName.Length != 0 || sNickName.Length != 0 ) continue;
				}
				// �������� �������� �����				
				if( sFB2BookTitle != null ) {
					if( sBookTitle.Length != 0 ) {
						if( sFB2BookTitle.Value != null ) {
							if( bExactFit ) {
								// ������ ������������
								if( sFB2BookTitle.Value != sBookTitle ) {
									bRet = false; continue;
								}
							} else {
								re = new Regex( sBookTitle, RegexOptions.IgnoreCase );
								if( !re.IsMatch( sFB2BookTitle.Value ) ) {
									bRet = false; continue;
								}
							}
						} else {
							// ������ ��� <book-title>
							bRet = false; continue;
						}
					}
				} else {
					// � ����� ���� �������� ���
					if( sBookTitle.Length != 0 ) {
						bRet = false; continue;
					}
				}
				
				bRet = true; break;
			}
			return bRet;
		}
		#endregion
	}
}
