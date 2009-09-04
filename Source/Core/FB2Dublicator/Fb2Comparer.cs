/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 03.09.2009
 * Time: 16:23
 * 
 * License: GPL 2.1
 */
using System;
using System.Collections.Generic;

using Core.FB2.Description.Common;
using Core.FB2.Description;
using Core.FB2.Description.TitleInfo;

namespace Core.FB2Dublicator
{
	/// <summary>
	/// Description of Fb2Comparer.
	/// </summary>
	public class Fb2Comparer
	{
		#region Закрытые данные класса
		private Description m_DescFB2File1 = null; // 1-й fb2-файл для сравнения
		private Description m_DescFB2File2 = null; // 2-й fb2-файл для сравнения
		#endregion
		
		public Fb2Comparer( Description DescFB2File1, Description DescFB2File2 )
		{
			m_DescFB2File1 = DescFB2File1;
			m_DescFB2File2 = DescFB2File2;
		}
		
		#region Открытые методы класса
		// возвращается true, если файлы имеют одинаковое Id
		public bool IsIdEquality() {
			return m_DescFB2File1.DocumentInfo.ID == m_DescFB2File2.DocumentInfo.ID;
        }
		
		// возвращается true, если файлы имеют одинаковое BookTitle (название книги)
		public bool IsBookTitleEquality() {
			BookTitle bt1 = m_DescFB2File1.TitleInfo.BookTitle;
			BookTitle bt2 = m_DescFB2File2.TitleInfo.BookTitle;
			
			if( bt1 == null && bt2 == null )
				return true;
			else if( ( bt1 == null && bt2 != null ) || ( bt1 != null && bt2 == null ) )
				return false;
			
			if( bt1.Value == null && bt2.Value == null )
				return true;
			else if( ( bt1.Value == null && bt2.Value != null ) || ( bt1.Value != null && bt2.Value == null ) )
				return false;
			
			return bt1.Value == bt2.Value;
        }
		
		// 1. если bAllAuthors=true - полное соответствие всех авторов в обоих книгах
		// возвращается true, если файлы имеют одинаковое число Авторов и соответственно, одинаковых Авторов
		// 2. если bAllAuthors=false - какой-то автор из книги 1 есть в списке авторов книге 2
		// возвращается true, если какой-то автор из книги 1 есть в списке авторов книге 2
		// сравнение - только по тегам: Имя, Фамилия, Отчество и Ник Авторов (e-mail и web игнорируются)
		public bool IsBookAuthorEquality( bool bAllAuthors  ) {
			IList<Author> lFB2Authors1 = m_DescFB2File1.TitleInfo.Authors;
			IList<Author> lFB2Authors2 = m_DescFB2File2.TitleInfo.Authors;
			
			if( lFB2Authors1 == null && lFB2Authors2 == null )
				return true;
			else if( ( lFB2Authors1 == null && lFB2Authors2 != null ) || ( lFB2Authors1 != null && lFB2Authors2 == null ) )
				return false;
			
			if( bAllAuthors ) {
				bool bFull = false; // флаг равенста данных Авторов
				// полное соответствие всех авторов в обоих книгах
				foreach( Author afb2_1 in lFB2Authors1 ) {
					foreach( Author afb2_2 in lFB2Authors2 ) {
						if( ( afb2_1.FirstName != null && afb2_1.FirstName == null ) ||
						   ( afb2_1.FirstName == null && afb2_1.FirstName != null ) ) {
							bFull = false; break;
						}
						if( ( afb2_1.MiddleName != null && afb2_1.MiddleName == null ) ||
						   ( afb2_1.MiddleName == null && afb2_1.MiddleName != null ) ) {
							bFull = false; break;
						}
						if( ( afb2_1.LastName != null && afb2_1.LastName == null ) ||
						   ( afb2_1.LastName == null && afb2_1.LastName != null ) ) {
							bFull = false; break;
						}
						if( ( afb2_1.NickName != null && afb2_1.NickName == null ) ||
						   ( afb2_1.NickName == null && afb2_1.NickName != null ) ) {
							bFull = false; break;
						}
					}
				}
			} else {
				// какой-то автор из книги 1 есть в списке авторов книге 2
			}
				
			return false;
		}

		#endregion
	}
}
