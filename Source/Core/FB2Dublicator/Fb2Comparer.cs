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

using Core.FB2.Description;
using Core.FB2.Description.TitleInfo;
using Core.FB2.Genres;

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
		public bool IsIdEquality() {
			// true, если файлы имеют одинаковое Id
			return m_DescFB2File1.DocumentInfo.ID == m_DescFB2File2.DocumentInfo.ID;
        }
		
		public bool IsBookTitleEquality() {
			// true, если файлы имеют одинаковое BookTitle (название книги)
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
		
		public bool IsBookAuthorEquality() {
			// true, если файлы имеют одинаковое число Авторов и соответственно, одинаковых Авторов
			return false;
		}
		
		public bool IsGenreEquality() {
			// true, если файлы имеют одинаковое число Жанров и соответственно, одинаковые Жанры
			IList<Genre> lGenres1 = m_DescFB2File1.TitleInfo.Genres;
			IList<Genre> lGenres2 = m_DescFB2File2.TitleInfo.Genres;
			
			return false;
		}
		#endregion
	}
}
