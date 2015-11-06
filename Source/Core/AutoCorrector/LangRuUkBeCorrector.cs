/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 06.11.2015
 * Время: 7:47
 * 
 * License: GPL 2.1
 */
using System;
using System.Text.RegularExpressions;

namespace Core.AutoCorrector
{
	/// <summary>
	/// Корректировка идентификатора русского, украинского и белорусского языков, если она задана неверно (только для книг, у которых в разделе description отсутствует раздел src-title-info)
	/// </summary>
	public class LangRuUkBeCorrector
	{
		private readonly string _xmlText = string.Empty;
		private string _InputString = string.Empty;
		
		/// <summary>
		/// Конструктор класса LangRuUkBeCorrector
		/// </summary>
		/// <param name="xmlText">xml текст всей книги</param>
		/// <param name="InputString">Строка для корректировки</param>
		public LangRuUkBeCorrector( string xmlText, ref string InputString )
		{
			_xmlText = xmlText;
			_InputString = InputString;
		}
		
		/// <summary>
		/// Корректировка признака языка книги на русском, украинском или белоруском языках
		/// </summary>
		/// <param name="IsCorrected">Ссылка на признак, была ли произведена корректировка языка</param>
		/// <returns>Откорректированную строку типа string</returns>
		public string correct( ref bool IsCorrected ) {
			IsCorrected = false;
			// замена <lang> для русских книг на ru для fb2 без <src-title-info>
			if ( _InputString.IndexOf( "<src-title-info>" ) == -1 ) {
				/* ***************************************************************************
				 * 							Алгоритм:
				 * 1. Ищем Ъъ - это только русский язык.
				 * 2. Если не нашли, то ищем ЯяЭэЮюЁёЬьЫыЖжЧчЩщ
				 * 3. Если не нашли - выход.
				 * 4. Если нашли, то смотрим, есть ли там только белоруская буква Ўў
				 * 5. Если есть - замена языка на белоруский язык (be).
				 * 6. Если нет, то смотрим, есть ли там только украинские буквы ҐґЇїЄє
				 * 7. Если есть, то замена языка на украинский язык (uk)
				 * 8. Если нет, то замена на русский язык (ru)
				 *************************************************************************** */
				const string BookLang = @"(?<=lang>)\s*?[^<]+?\s*?(?=</lang>)";
				if ( Regex.Match( _xmlText, "[Ъъ]" ).Success ) {
					// это книга только на русском языке
					_InputString = Regex.Replace(
						_InputString, BookLang,
						"ru", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
					IsCorrected = true;
				} else {
					if ( Regex.Match( _xmlText, "[ЯяЭэЮюЁёЬьЫыЖжЧчЩщ]" ).Success ) {
						// могут быть как книга на русском, так и на украинском или белорусском языках
						if ( Regex.Match( _xmlText, "[Ўў]" ).Success ) {
							// это книга только на белорусском языке
							_InputString = Regex.Replace(
								_InputString, BookLang,
								"be", RegexOptions.IgnoreCase | RegexOptions.Multiline
							);
							IsCorrected = true;
						} else if ( Regex.Match( _xmlText, "[ҐґЇїЄє]" ).Success ) {
							// это книга только на украинском языке
							_InputString = Regex.Replace(
								_InputString, BookLang,
								"uk", RegexOptions.IgnoreCase | RegexOptions.Multiline
							);
							IsCorrected = true;
						} else {
							// это книга только на русском языке
							_InputString = Regex.Replace(
								_InputString, BookLang,
								"ru", RegexOptions.IgnoreCase | RegexOptions.Multiline
							);
							IsCorrected = true;
						}
					}
				}
			}
			return _InputString;
		}
	}
}
