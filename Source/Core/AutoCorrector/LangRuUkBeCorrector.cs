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
		private string _XmlDescription = string.Empty;
		private readonly string _XmlBody = string.Empty;
		
		/// <summary>
		/// Конструктор класса LangRuUkBeCorrector
		/// </summary>
		/// <param name="XmlDescription">xml строка description для корректировки</param>
		/// <param name="XmlBody">xml текст body</param>
		public LangRuUkBeCorrector( ref string XmlDescription, string XmlBody)
		{
			_XmlDescription = XmlDescription;
			_XmlBody = XmlBody;
		}
		
		/// <summary>
		/// Корректировка признака языка книги на русском, украинском или белоруском языках
		/// </summary>
		/// <param name="IsCorrected">Ссылка на признак, была ли произведена корректировка языка</param>
		/// <returns>Откорректированную строку типа string</returns>
		public string correct( ref bool IsCorrected ) {
			IsCorrected = false;
			// замена <lang> для русских книг на ru для fb2 без <src-title-info>
			if ( _XmlDescription.IndexOf( "<src-title-info>", StringComparison.CurrentCulture ) == -1 ) {
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
				if ( Regex.Match( _XmlBody, "[Ъъ]", RegexOptions.None ).Success ) {
					// это книга только на русском языке
					_XmlDescription = Regex.Replace(
						_XmlDescription, BookLang,
						"ru", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
					IsCorrected = true;
				} else {
					if ( Regex.Match( _XmlBody, "[ЯяЭэЮюЁёЬьЫыЖжЧчЩщ]" ).Success ) {
						// могут быть как книга на русском, так и на украинском или белорусском языках
						if ( Regex.Match( _XmlBody, "[Ўў]", RegexOptions.None ).Success ) {
							// это книга только на белорусском языке
							_XmlDescription = Regex.Replace(
								_XmlDescription, BookLang,
								"be", RegexOptions.IgnoreCase | RegexOptions.Multiline
							);
							IsCorrected = true;
						} else if ( Regex.Match( _XmlBody, "[ҐґЇїЄє]", RegexOptions.None ).Success ) {
							// это книга только на украинском языке
							_XmlDescription = Regex.Replace(
								_XmlDescription, BookLang,
								"uk", RegexOptions.IgnoreCase | RegexOptions.Multiline
							);
							IsCorrected = true;
						} else {
							// это книга только на русском языке
							_XmlDescription = Regex.Replace(
								_XmlDescription, BookLang,
								"ru", RegexOptions.IgnoreCase | RegexOptions.Multiline
							);
							IsCorrected = true;
						}
					}
				}
			}
			return _XmlDescription;
		}
	}
}
