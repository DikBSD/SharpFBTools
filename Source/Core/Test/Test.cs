﻿/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 20.12.2013
 * Time: 12:53
 * 
 * License: GPL 2.1
 */
using System;
using System.Text;
using System.IO;
using System.Threading;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Linq;

using Core.FB2.Common;
using Core.FB2.Description;
using Core.FB2.Description.Common;
using Core.FB2.Description.CustomInfo;
using Core.FB2.Description.DocumentInfo;
using Core.FB2.Description.TitleInfo;
using Core.Duplicator;
using Core.FB2.FB2Parsers;
using Core.Common;

namespace Test
{
	/// <summary>
	/// Тестовый стенд
	/// </summary>
	public class Test
	{
		public Test()
		{
		}
		
		public static void gebugTestIsSameBookTitle() {
			BookTitle bt1 = new BookTitle("11");
			BookTitle bt2 = new BookTitle("1");
			
			if (bt1.Equals(bt2))
				MessageBox.Show( "Да", "Test - BookTitle", MessageBoxButtons.OK, MessageBoxIcon.Information );
			else
				MessageBox.Show( "Нет", "Test - BookTitle", MessageBoxButtons.OK, MessageBoxIcon.Information );
		}
		
		public static void gebugTestIsSameGenre() {
			Genre g1 = new Genre(null, 90);
			Genre g2 = new Genre("2", 90);
			
			if (g1.isSameGenre(g2))
				MessageBox.Show( "Да", "Test - Genre", MessageBoxButtons.OK, MessageBoxIcon.Information );
			else
				MessageBox.Show( "Нет", "Test - Genre", MessageBoxButtons.OK, MessageBoxIcon.Information );
		}
		
		public static void gebugTestIsSameAuthor(bool CompareAndMiddleName) {
			TextFieldType FirstName1 = new TextFieldType("Иван");
			TextFieldType MiddleName1 = new TextFieldType("Петрович");
			TextFieldType LastName1 = new TextFieldType("Кузнецов");
			TextFieldType NickName1 = new TextFieldType("NNNnnNNN");
			string			m_sID1 = "HGFYTF7HJGJGJHKJ";
			IList<string>	lsHomePages1 = new List<string>();
			lsHomePages1.Add("111");
			lsHomePages1.Add("222");
			IList<string>	lsEmails1	 = new List<string>();
			lsEmails1.Add("111");
			lsEmails1.Add("222");
			
			TextFieldType FirstName2 = new TextFieldType("Иван");
			TextFieldType MiddleName2 = new TextFieldType("Петрович");
			TextFieldType LastName2 = new TextFieldType("Кузнецов");
			TextFieldType NickName2 = new TextFieldType("NNNnnNNN");
//			string			m_sID2 = "HGFYTF7HJGJGJHKJ";
			IList<string>	lsHomePages2 = new List<string>();
			lsHomePages2.Add("111");
			lsHomePages2.Add("222");
			IList<string>	lsEmails2	 = new List<string>();
			lsEmails2.Add("111");
			lsEmails2.Add("222");
			Author author1 = new Author(FirstName1, MiddleName1, LastName1, NickName1, lsHomePages1, lsEmails1, m_sID1);
			Author author2 = new Author(FirstName2, MiddleName2, LastName2);
			
			if (author1.isSameAuthor(author2, CompareAndMiddleName))
				MessageBox.Show( "Да", "Test - Author", MessageBoxButtons.OK, MessageBoxIcon.Information );
			else
				MessageBox.Show( "Нет", "Test - Author", MessageBoxButtons.OK, MessageBoxIcon.Information );
		}
		
		public static void gebugTestIsSameBookData(bool CompareAndMiddleName) {
            // Данные на 1-го Автора книги
            TextFieldType FirstName1 = new TextFieldType("Иван");
			TextFieldType MiddleName1 = new TextFieldType("Петрович");
			TextFieldType LastName1 = new TextFieldType("Кузнецов");
			TextFieldType NickName1 = new TextFieldType("NNNnnNNN");
			string			m_sID1 = "HGFYTF7HJGJGJHKJ";
			IList<string>	lsHomePages1 = new List<string>();
			lsHomePages1.Add("111");
			lsHomePages1.Add("222");
			IList<string>	lsEmails1	 = new List<string>();
			lsEmails1.Add("111");
			lsEmails1.Add("222");

            // Данные на 2-го Автора книги
            TextFieldType FirstName2 = new TextFieldType("Иван");
			TextFieldType MiddleName2 = new TextFieldType("Петрович");
			TextFieldType LastName2 = new TextFieldType("Кузнецов");
			TextFieldType NickName2 = new TextFieldType("NNNnnNNN");
//			string			m_sID2 = "HGFYTF7HJGJGJHKJ";
			IList<string>	lsHomePages2 = new List<string>();
			lsHomePages2.Add("111");
			lsHomePages2.Add("222");
			IList<string>	lsEmails2	 = new List<string>();
			lsEmails2.Add("111");
			lsEmails2.Add("222");

            // "Сборка" данных Авторов
			Author author1 = new Author(FirstName1, MiddleName1, LastName1, NickName1, lsHomePages1, lsEmails1, m_sID1);
			Author author2 = new Author(FirstName2, MiddleName2, LastName2);
			IList<Author> authors1 = new List<Author>();
			authors1.Add(author1);
			//authors1.Add(author2);
			IList<Author> authors2 = new List<Author>();
			//authors2.Add(author1);
			authors2.Add(author2);
			
            // Данные на Жанры книг
			Genre g1 = new Genre("1", 90);
			Genre g2 = new Genre("2", 90);
			IList<Genre> genres1 = new List<Genre>();
			genres1.Add(g1);
			genres1.Add(g2);
			IList<Genre> genres2 = new List<Genre>();
			genres2.Add(g1);
			genres2.Add(g2);
			
            // Названия книг
			BookTitle bt1 = new BookTitle("1");
			BookTitle bt2 = new BookTitle("1");

            // Данные на 1-го Автора fb2 файла
            TextFieldType FB2FirstName1 = new TextFieldType("Петр");
            TextFieldType FB2MiddleName1 = new TextFieldType("Иванович");
            TextFieldType FB2LastName1 = new TextFieldType("Пелюгин");
            TextFieldType FB2NickName1 = new TextFieldType("JKotNM");
            string m_sFB2ID1 = "H7KF75TDWJGJHKJ";
            IList<string> lsFB2HomePages1 = new List<string>();
            lsFB2HomePages1.Add("111");
            lsFB2HomePages1.Add("222");
            IList<string> lsFB2Emails1 = new List<string>();
            lsFB2Emails1.Add("111");
            lsFB2Emails1.Add("222");

            // Данные на 2-го Автора fb2 файла
            TextFieldType FB2FirstName2 = new TextFieldType("Петр");
            TextFieldType FB2MiddleName2 = new TextFieldType("Иванович");
            TextFieldType FB2LastName2 = new TextFieldType("Пелюгин");
            TextFieldType FB2NickName2 = new TextFieldType("JKotNM");
            //string m_sFB2ID2 = "H7KF75TDWJGJHKJ";
            IList<string> lsFB2HomePages2 = new List<string>();
            lsFB2HomePages2.Add("111");
            lsFB2HomePages2.Add("222");
            IList<string> lsFB2Emails2 = new List<string>();
            lsFB2Emails2.Add("111");
            lsFB2Emails2.Add("222");

            // "Сборка" данных Авторов fb2 файлов
            Author fb2Author1 = new Author(FB2FirstName1, FB2MiddleName1, FB2LastName1, FB2NickName1, lsFB2HomePages1, lsFB2Emails1, m_sFB2ID1);
            Author fb2Author2 = new Author(FB2FirstName2, FB2MiddleName2, FB2LastName2);
            IList<Author> fb2Authors1 = new List<Author>();
            fb2Authors1.Add(fb2Author1);
            //fb2Authors1.Add(fb2Author2);
            IList<Author> fb2Authors2 = new List<Author>();
            //fb2Authors2.Add(fb2Author1);
            fb2Authors2.Add(fb2Author2);

            BookData bd1 = new BookData( bt1, authors1, genres1, "ru", "efewtgerger", "1.0", fb2Authors1, "Path", "UTF-8");
			BookData bd2 = new BookData( bt2, authors2, genres2, "ru", "efewtgerger", "1.0", fb2Authors2, "Path", "UTF-8");
			
			if (bd1.isSameBook(bd2, CompareAndMiddleName, false))
				MessageBox.Show( "Одинаковые:\n"+"bd1: "+bd1.BookTitle.Value+"\n"+"bd2: "+bd2.BookTitle.Value,
				                "Test - BookData", MessageBoxButtons.OK, MessageBoxIcon.Information );
			else
				MessageBox.Show( "Нет:\n"+"bd1: "+bd1.BookTitle.Value+"\n"+"bd2: "+bd2.BookTitle.Value,
				                "Test - BookData", MessageBoxButtons.OK, MessageBoxIcon.Information );
		}
		
		public static void _gebugTestIsSameBookData(bool CompareAndMiddleName) {
			FictionBook fb2_1 = new FictionBook("1.fb2");
			FictionBook fb2_2 = new FictionBook("2.fb2");
			
			BookData bd1 = new BookData(
				fb2_1.TIBookTitle, fb2_1.TIAuthors, fb2_1.TIGenres, fb2_1.TILang, fb2_1.DIID, fb2_1.DIVersion,
                fb2_1.DIAuthors, fb2_1.getFilePath(), fb2_1.getEncoding()
			);
			BookData bd2 = new BookData(
				fb2_2.TIBookTitle, fb2_2.TIAuthors, fb2_2.TIGenres, fb2_2.TILang, fb2_2.DIID, fb2_2.DIVersion,
                fb2_2.DIAuthors, fb2_2.getFilePath(), fb2_2.getEncoding()
			);
			
			if (bd1.isSameBook(bd2, CompareAndMiddleName, false))
				MessageBox.Show( "Одинаковые:\n"+"bd1: "+bd1.BookTitle.Value+"\n"+"bd2: "+bd2.BookTitle.Value,
				                "Test - BookData", MessageBoxButtons.OK, MessageBoxIcon.Information );
			else
				MessageBox.Show( "Нет:\n"+"bd1: "+bd1.BookTitle.Value+"\n"+"bd2: "+bd2.BookTitle.Value,
				                "Test - BookData", MessageBoxButtons.OK, MessageBoxIcon.Information );
		}
		
		public static void debugTestIsSameBookData( ref IList<BookData> BookDataList, bool CompareAndMiddleName) {
			for( int i = 0; i != BookDataList.Count; ++i ) {
				BookData bd1 = BookDataList[i]; // текущая книга
				// перебор всех книг в группе, за исключением текущей
				for( int j = i+1; j != BookDataList.Count; ++j ) {
					// сравнение текущей книги со всеми последующими
					BookData bd2 = BookDataList[j];
					if (bd1.isSameBook(bd2, CompareAndMiddleName, false))
						MessageBox.Show( "Одинаковые:\n"+"bd1: "+bd1.Path+"\n"+"bd2: "+bd2.Path,
						                "Test - BookData", MessageBoxButtons.OK, MessageBoxIcon.Information );
					else
						MessageBox.Show( "Нет:\n"+"bd1: "+bd1.Path+"\n"+"bd2: "+bd2.Path,
						                "Test - BookData", MessageBoxButtons.OK, MessageBoxIcon.Information );
				}
			}
		}
		
		// ===================================================================================================
		public static string isSameBookTitle(BookTitle bt1, BookTitle bt2) {
			if (bt1.Equals(bt2))
				return "Одинаковые:\n"+"BookTitle1: "+bt1.Value+"\n"+"BookTitle2: "+bt2.Value;
			else
				return "Нет:\n"+"BookTitle1"+bt1.Value+"\n"+"BookTitle2"+bt2.Value;
		}
		
		// =====================================================
		public static string isSameGenre(Genre g1, Genre g2) {
			if (g1.isSameGenre(g2))
				return "Одинаковые:\n"+"g1: "+g1.Name+"\t"+g1.Math.ToString()+"\ng2: "+g2.Name+"\t"+g2.Math.ToString();
			else
				return "Нет:\n"+"g1: "+g1.Name+"\t"+g1.Math.ToString()+"\ng2: "+g2.Name+"\t"+g2.Math.ToString();
		}
		
		// =====================================================
		public static string isSameAuthor(Author author1, Author author2, bool CompareAndMiddleName) {
			string fio1 = (author1.FirstName!=null ? author1.FirstName.Value : "") + "\t"
				+ (author1.MiddleName!=null ? author1.MiddleName.Value : "") + "\t"
				+ author1.LastName!=null ? author1.LastName.Value : "";
			string fio2 = (author2.FirstName!=null ? author2.FirstName.Value : "") + "\t"
				+ (author2.MiddleName!=null ? author2.MiddleName.Value : "") + "\t"
				+ author2.LastName!=null ? author2.LastName.Value : "";
			if (author1.isSameAuthor(author2, CompareAndMiddleName))
				return "Одинаковые:\n\n"+"fio1: "+fio1 +"\nfio2: "+fio2;
			else
				return "Нет:\n\n"+"fio1: "+fio1 +"\nfio2: "+fio2;
		}
		
		// =====================================================
		public static string isSameBookData(BookData bd1, BookData bd2, bool CompareAndMiddleName) {
			string fio1 = "";
			foreach (Author a1 in bd1.Authors) {
				fio1 += (a1.FirstName!=null ? a1.FirstName.Value : "") + "\t"
					+ (a1.MiddleName!=null ? a1.MiddleName.Value : "") + "\t"
					+ (a1.LastName!=null ? a1.LastName.Value : "") + "\n";
			}
			string fio2 = "";
			foreach (Author a2 in bd2.Authors) {
				fio2 += (a2.FirstName!=null ? a2.FirstName.Value : "") + "\t"
					+ (a2.MiddleName!=null ? a2.MiddleName.Value : "") + "\t"
					+ (a2.LastName!=null ? a2.LastName.Value : "") + "\n";
			}
			if (bd1.isSameBook(bd2, CompareAndMiddleName, false))
				return "Одинаковые:\n\n"+"bd1: "+bd1.BookTitle.Value+"\nfio1: "+fio1
					+"\n\nbd2: "+bd2.BookTitle.Value+"\nfio2: "+fio2;
			else
				return "Нет:\n\n"+"bd1: "+bd1.BookTitle.Value+"\nfio1: "+fio1
					+"\n\nbd2: "+bd2.BookTitle.Value+"\nfio2: "+fio2;
		}
		
		public static string getBookDataInfo( BookData bd ) {
			IList<Author> authors = bd.Authors;
			string fio = "";
			if (authors!=null) {
				foreach (Author a in authors) {
					fio += (a.FirstName!=null ? a.FirstName.Value : "") + " "
						+ (a.MiddleName!=null ? a.MiddleName.Value : "") + " "
						+ (a.LastName!=null ? a.LastName.Value : "") + "\n";
				}
			}
			IList<Genre> genres = bd.Genres;
			string genre = "";
			if (genres!=null) {
				foreach (Genre g in genres) {
					genre += (g.Name!=null ? g.Name : "") + " "
						+ g.Math.ToString() + "\n";
				}
			}
			return "Название книги: "+bd.BookTitle.Value
				+ "\nId: "+bd.Id
				+ "\nVersion: "+bd.Version
				+ "\nEncoding: "+bd.Encoding
				+ "\nPath: "+bd.Path
				+ "\n\nАвторы: \n"+fio
				+ "\nЖанры: \n"+genre;
		}
		
		// =====================================================
		public static string isSameGroup(FB2FilesDataInGroup group1, FB2FilesDataInGroup group2) {
			if (group1.isSameGroup(group2))
				return "Да";
			else
				return "Нет";
		}
		
		public static string getGroupListInfo( ref FB2FilesDataInGroup fb2Group ) {
			string group = fb2Group.Group;
			string titleList = "";
			string authorsList = "";
			string pathList = "";
			int titleListCount = 0;
			int authorsListCount = 0;
			int pathListCount = 0;
			foreach ( BookData bd in fb2Group ) {
				titleList += bd.BookTitle.Value+"\n";
				titleListCount++;
				authorsList += MakeAutorsString( bd.Authors, false )+"\n";
				authorsListCount++;
				pathList += bd.Path+"\n";
				pathListCount++;
			}
			return "Группа: \n"+group+"\n\nНазвания книг: "+titleListCount.ToString()+"\n"+titleList
				+"\nАвторы: "+authorsListCount.ToString()+"\n"+authorsList
				+"\nПути: "+pathListCount.ToString()+"\n"+pathList;
		}
		public static string getGroupListInfo( FB2FilesDataInGroup fb2Group ) {
			string group = fb2Group.Group;
			string titleList = "";
			string authorsList = "";
			string pathList = "";
			int titleListCount = 0;
			int authorsListCount = 0;
			int pathListCount = 0;
			foreach ( BookData bd in fb2Group ) {
				titleList += bd.BookTitle.Value+"\n";
				titleListCount++;
				authorsList += MakeAutorsString( bd.Authors, false )+"\n";
				authorsListCount++;
				pathList += bd.Path+"\n";
				pathListCount++;
			}
			return "Группа: \n"+group+"\n\nНазвания книг: "+titleListCount.ToString()+"\n"+titleList
				+"\nАвторы: "+authorsListCount.ToString()+"\n"+authorsList
				+"\nПути: "+pathListCount.ToString()+"\n"+pathList;
		}
		
		// ===================================================================================================
		
		// формирование строки с Авторами Книги из списка всех Авторов ЭТОЙ Книги
		public static string MakeAutorsString( IList<Author> Authors, bool bNumber ) {
			if( Authors == null )
				return "Тег <authors> в книге отсутствует";
			string sA = string.Empty; int n = 0;
			foreach( Author a in Authors ) {
				++n;
				if( a.LastName!=null && a.LastName.Value!=null )
					sA += a.LastName.Value+" ";
				if( a.FirstName!=null && a.FirstName.Value!=null )
					sA += a.FirstName.Value+" ";
				if( a.MiddleName!=null && a.MiddleName.Value!=null )
					sA += a.MiddleName.Value+" ";
				if( a.NickName!=null && a.NickName.Value!=null )
					sA += a.NickName.Value;
				sA = sA.Trim();
				sA += "; ";
			}
			if( bNumber )
				sA = Convert.ToString(n)+": " + sA;
			return sA.Substring( 0, sA.LastIndexOf( ";" ) ).Trim();
		}
		
		public static string getBookAuthorsInfo( BookData bd ) {
			IList<Author> authors = bd.Authors;
			string fio = "";
			if (authors!=null) {
				foreach (Author a in authors) {
					fio += (a.FirstName!=null ? a.FirstName.Value : "") + " "
						+ (a.MiddleName!=null ? a.MiddleName.Value : "") + " "
						+ (a.LastName!=null ? a.LastName.Value : "") + "\n";
				}
			}
			return "Авторы: \n"+fio+"\nПуть: "+bd.Path;
		}
		
		// формирование строки с Автором Книги
		public static string MakeAutorString( Author a ) {
			if( a == null )
				return "Тег <author> в книге отсутствует";
			string sA = string.Empty;
			if( a.LastName!=null && a.LastName.Value!=null )
				sA += a.LastName.Value+" ";
			if( a.FirstName!=null && a.FirstName.Value!=null )
				sA += a.FirstName.Value+" ";
			if( a.MiddleName!=null && a.MiddleName.Value!=null )
				sA += a.MiddleName.Value+" ";
			if( a.NickName!=null && a.NickName.Value!=null )
				sA += a.NickName.Value;
			sA = sA.Trim();

			return sA;
		}
		
		// формирование списка строк с ФИО Авторов, которые есть в обоих списках Авторов (Intersection)
		// WithMiddleName = true - учитывать Отчество Автора
		private List<string> listIntersection(IList<Author> Authors1, IList<Author> Authors2, bool WithMiddleName) {
			if ( Authors1 != null && Authors2 != null )
				if ( Authors1.Count != Authors2.Count )
					return null;

			List<string> list1 = makeListFOIAuthors(Authors1, WithMiddleName);
			List<string> list2 = makeListFOIAuthors(Authors2, WithMiddleName);
			return list1.Intersect(list2, new FB2EqualityComparer()).ToList();
		}
		// формирование списка из строк ФИО каждого Автора из Authors
		// WithMiddleName = true - учитывать Отчество Автора
		public static List<string> makeListFOIAuthors(IList<Author> Authors, bool WithMiddleName) {
			List<string> list = new List<string>();
			const string Ret = "<Автор книги отсутствует>";
			if ( Authors == null ) {
				list.Add(Ret);
				return list;
			}
			
			for ( int i = 0; i != Authors.Count; ++i ) {
				bool AuthorExist = true;
				StringBuilder fio = new StringBuilder();
				if ( Authors[i].LastName != null && !string.IsNullOrWhiteSpace(Authors[i].LastName.Value ) )
					fio.Append(Authors[i].LastName.Value.Trim());
				if ( Authors[i].FirstName != null && !string.IsNullOrWhiteSpace( Authors[i].FirstName.Value ) ) {
					fio.Append(" ");
					fio.Append(Authors[i].FirstName.Value.Trim());
				}
				if ( WithMiddleName ) {
					if (Authors[i].MiddleName != null && !string.IsNullOrWhiteSpace( Authors[i].MiddleName.Value ) ) {
						fio.Append(" ");
						fio.Append(Authors[i].MiddleName.Value.Trim());
					}
				}
				
				bool MiddleNameExist = false;
				if ( WithMiddleName ) {
					if ( Authors[i].MiddleName != null && !string.IsNullOrWhiteSpace( Authors[i].MiddleName.Value ) )
						MiddleNameExist = true;
				}
				bool LastNameExist = false;
				if ( Authors[i].LastName != null && !string.IsNullOrWhiteSpace( Authors[i].LastName.Value ) )
					LastNameExist = true;
				bool FirstNameExist = false;
				if ( Authors[i].FirstName != null && !string.IsNullOrWhiteSpace( Authors[i].FirstName.Value ) )
					FirstNameExist = true;
				
				if ( WithMiddleName ) {
					if ( !LastNameExist && !FirstNameExist && !MiddleNameExist )
						AuthorExist = false;
				} else {
					if ( !LastNameExist && !FirstNameExist )
						AuthorExist = false;
				}
				
				string s = fio.ToString();
				if ( !AuthorExist )
					s = Ret;
				if ( s != Ret ) {
					if ( !list.Contains(s) )
						list.Add(s);
				} else
					list.Add(s + ( i > 0 ? i.ToString() : string.Empty ) );
			}
			return list;
		}

	}
}
