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

//using System.Windows.Forms;

using Core.FB2.Description.TitleInfo;
using Core.FB2.Description.Common;
using Core.FB2.FB2Parsers;
using Core.FB2.Genres;
using Core.Common;

namespace Core.Sorter
{
	/// <summary>
	/// �������� ����� �� ��������� ��������� ����������
	/// </summary>
	public class FB2SelectedSorting
	{
		public FB2SelectedSorting()
		{
		}
		
		#region �������� ������
		/// <summary>
		/// ��������, ������������� �� ������� ���� �������� ������ ��� ��������� ����������
		/// </summary>
		/// <param name="sFromFilePath">��������������� ���� �� ������� ������������ ��������� ����������</param>
		/// <param name="lSSQCList">������ ����������� ������  SortQueryCriteria</param>
		/// <param name="IsNotRead">out ��������: true - ����� �� �����������; false - ����� ������� �������</param>
		/// <returns>null, ���� ����� �� ������������� �� ������ �������� ������; ��� ��������� �������� SelectedSortQueryCriteria</returns>
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
			} catch ( Exception ex ) {
				Debug.DebugMessage(
					sFromFilePath, ex, "�����������.SortQueryCriteria: ��������, ������������� �� ������� ���� �������� ������ ��� ��������� ����������."
				);
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
			foreach ( SortQueryCriteria ssqc in lSSQCList ) {
				sLang 		= ssqc.Lang;
				sGenre		= ssqc.Genre;
				sFirstName	= ssqc.FirstName;
				sMiddleName	= ssqc.MiddleName;
				sLastName	= ssqc.LastName;
				sNickName	= ssqc.NickName;
				sSequence	= ssqc.Sequence;
				sBookTitle	= ssqc.BookTitle;
				bExactFit	= ssqc.ExactFit;
				
				/* �������� �������� �������� */
				// �������� ����� �����
				if ( !string.IsNullOrWhiteSpace( sFB2Lang ) ) {
					if ( !string.IsNullOrWhiteSpace( sLang ) ) {
						if ( sFB2Lang != sLang )
							continue;
					}
				} else {
					// � ����� ���� ����� ���
					if ( !string.IsNullOrWhiteSpace( sLang ) )
						continue;
				}
				// �������� ����� �����
				bool b = false;
				if ( lFB2Genres != null ) {
					if ( !string.IsNullOrWhiteSpace( sGenre ) ) {
						foreach ( Genre gfb2 in lFB2Genres ) {
							if ( !string.IsNullOrWhiteSpace( gfb2.Name ) ) {
								if ( gfb2.Name == sGenre ) {
									b = true; break;
								}
							} else
								continue;
						}
						if ( !b )
							continue;
					}
				} else {
					// � ����� ���� ����� ���
					if ( !string.IsNullOrWhiteSpace( sGenre ) )
						continue;
				}
				// �������� ����� �����
				b = false;
				if ( lFB2Sequences != null ) {
					if ( !string.IsNullOrWhiteSpace( sSequence ) ) {
						foreach ( Sequence sfb2 in lFB2Sequences ) {
							if ( !string.IsNullOrWhiteSpace( sfb2.Name ) ) {
								if ( bExactFit ) {
									// ������ ������������
									if ( sfb2.Name == sSequence ) {
										b = true; break;
									}
								} else {
									re = new Regex( sSequence, RegexOptions.IgnoreCase );
									try {
										if ( re.IsMatch( sfb2.Name ) ) {
											b = true; break;
										}
									} catch ( Exception ex ) {
										Debug.DebugMessage(
											sFromFilePath, ex, "�����������.SortQueryCriteria: �������� ����� �����."
										);
//										MessageBox.Show("sfb2.Name  \r\n"+e.Message+"\r\n\r\n"+sFromFilePath);
									}
								}
							} else
								continue;
						}
						if ( !b )
							continue;
					}
				} else {
					// � ����� ���� ����� ���
					if ( !string.IsNullOrWhiteSpace( sSequence ) )
						continue;
				}
				// �������� ������ �����
				if ( lFB2Authors != null ) {
					b = false;
					if ( sFirstName.Length != 0 ) {
						foreach ( Author afb2 in lFB2Authors ) {
							if ( afb2.FirstName != null ) {
								if ( bExactFit ) {
									// ������ ������������
									if ( afb2.FirstName.Value == sFirstName ) {
										b = true; break;
									}
								} else {
									re = new Regex( sFirstName, RegexOptions.IgnoreCase );
									try {
										if ( re.IsMatch( afb2.FirstName.Value ) ) {
											b = true; break;
										}
									} catch ( Exception ex ) {
										Debug.DebugMessage(
											sFromFilePath, ex, "�����������.SortQueryCriteria: �������� ������ ����� (FirstName)."
										);
//										MessageBox.Show("afb2.FirstName.Value \r\n"+e.Message+"\r\n\r\n"+sFromFilePath);
									}
								}
							} else
								continue;
						}
						if ( !b )
							continue;
					}
					b = false;
					if ( sMiddleName.Length != 0 ) {
						foreach ( Author afb2 in lFB2Authors ) {
							if ( afb2.MiddleName != null ) {
								if ( bExactFit ) {
									// ������ ������������
									if ( afb2.MiddleName.Value == sMiddleName ) {
										b = true; break;
									}
								} else {
									re = new Regex( sMiddleName, RegexOptions.IgnoreCase );
									try {
										if ( re.IsMatch( afb2.MiddleName.Value ) ) {
											b = true; break;
										}
									} catch ( Exception ex ) {
										Debug.DebugMessage(
											sFromFilePath, ex, "�����������.SortQueryCriteria: �������� ������ ����� (MiddleName)."
										);
//										MessageBox.Show("afb2.MiddleName.Value \r\n"+e.Message+"\r\n\r\n"+sFromFilePath);
									}
								}
							} else
								continue;
						}
						if ( !b )
							continue;
					}
					b = false;
					if ( sLastName.Length != 0 ) {
						foreach ( Author afb2 in lFB2Authors ) {
							if ( afb2.LastName != null ) {
								if ( bExactFit ) {
									// ������ ������������
									if ( afb2.LastName.Value == sLastName ) {
										b = true; break;
									}
								} else {
									re = new Regex( sLastName, RegexOptions.IgnoreCase );
									try {
										if ( re.IsMatch( afb2.LastName.Value ) ) {
											b = true; break;
										}
									} catch ( Exception ex ) {
										Debug.DebugMessage(
											sFromFilePath, ex, "�����������.SortQueryCriteria: �������� ������ ����� (LastName)."
										);
//										MessageBox.Show("afb2.LastName.Value \r\n"+e.Message+"\r\n\r\n"+sFromFilePath);
									}
								}
							} else
								continue;
						}
						if ( !b )
							continue;
					}
					b = false;
					if ( sNickName.Length != 0 ) {
						foreach ( Author afb2 in lFB2Authors ) {
							if ( afb2.NickName != null ) {
								if ( bExactFit ) {
									// ������ ������������
									if ( afb2.NickName.Value == sNickName ) {
										b = true; break;
									}
								} else {
									re = new Regex( sNickName, RegexOptions.IgnoreCase );
									try {
										if ( re.IsMatch( afb2.NickName.Value ) ) {
											b = true; break;
										}
									} catch ( Exception ex ) {
										Debug.DebugMessage(
											sFromFilePath, ex, "�����������.SortQueryCriteria: �������� ������ ����� (NickName)."
										);
//										MessageBox.Show("afb2.NickName.Value \r\n"+e.Message+"\r\n\r\n"+sFromFilePath);
									}
								}
							} else
								continue;
						}
						if ( !b )
							continue;
					}
				} else {
					// � ����� ����� ������ ���
					if ( !string.IsNullOrWhiteSpace( sFirstName ) || !string.IsNullOrWhiteSpace( sMiddleName )||
					    !string.IsNullOrWhiteSpace( sNickName ) || !string.IsNullOrWhiteSpace( sNickName ) )
						continue;
				}
				// �������� �������� �����
				if ( FB2BookTitle != null ) {
					if ( !string.IsNullOrWhiteSpace( sBookTitle ) ) {
						if ( !string.IsNullOrWhiteSpace( FB2BookTitle.Value ) ) {
							if ( bExactFit ) {
								// ������ ������������
								if ( FB2BookTitle.Value != sBookTitle )
									continue;
							} else {
								re = new Regex( sBookTitle, RegexOptions.IgnoreCase );
								try {
									if ( !re.IsMatch( FB2BookTitle.Value ) )
										continue;
								} catch ( Exception ex ) {
									Debug.DebugMessage(
										sFromFilePath, ex, "�����������.SortQueryCriteria: �������� �������� �����."
									);
//									MessageBox.Show("FB2BookTitle.Value \r\n"+e.Message+"\r\n\r\n"+sFromFilePath);
								}
							}
						} else
							continue; // ������ ��� <book-title>
					}
				} else {
					// � ����� ���� �������� ���
					if ( !string.IsNullOrWhiteSpace( sBookTitle ) )
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
