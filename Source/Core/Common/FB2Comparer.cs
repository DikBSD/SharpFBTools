/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 25.11.2015
 * Время: 8:50
 * 
 * License: GPL 2.1
 */
using System;
using System.Collections;
using System.Globalization;
using System.Collections.Generic;

namespace Core.Common
{
	/// <summary>
	/// Компарер для сравнения строк без учета регистра и/или с учетом языка
	/// </summary>
	public class FB2CultureComparer : IEqualityComparer
	{
		public CaseInsensitiveComparer _Comparer;

		public FB2CultureComparer()
		{
			_Comparer = CaseInsensitiveComparer.DefaultInvariant;
		}

		public FB2CultureComparer(CultureInfo myCulture)
		{
			_Comparer = new CaseInsensitiveComparer(myCulture);
		}

		public new bool Equals(object x, object y)
		{
			if ( _Comparer.Compare(x, y) == 0 ) {
				return true;
			} else {
				return false;
			}
		}

		public int GetHashCode(object x)
		{
			return x.ToString().ToLower().GetHashCode();
		}
	}

	/// <summary>
	/// Компарер для сравнения строк без учета регистра
	/// </summary>
	public class FB2EqualityComparer : IEqualityComparer<string>
	{
		public bool Equals(string x, string y)
		{
			return x.ToLower() == y.ToLower();
		}

		public int GetHashCode(string x)
		{
			return x.ToLower().GetHashCode();
		}
	}
	
}
