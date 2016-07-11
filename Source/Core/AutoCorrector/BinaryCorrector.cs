/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 04.12.2015
 * Время: 13:47
 * 
 * License: GPL 2.1
 */
using System;
using System.Text.RegularExpressions;

namespace Core.AutoCorrector
{
	/// <summary>
	/// Корректировка атрибутов тега binary
	/// </summary>
	public class BinaryCorrector
	{
		private string _xmlText = string.Empty;
		
		/// <summary>
		/// Конструктор класса BinaryCorrector
		/// </summary>
		/// <param name="xmlText">Строка для корректировки</param>
		public BinaryCorrector( string xmlText )
		{
			_xmlText = xmlText;
		}
		
		/// <summary>
		/// Корректировка атрибутов тегов binary
		/// </summary>
		/// <returns>Откорректированную строку типа string </returns>
		public string correct() {
			try {
				Match m = Regex.Match(
					_xmlText, "<binary (?:(?:content-type=\"image/\\w{3,4}\" id=\"\\w+\\.\\w{3,4}\")|(?:id=\"\\w+\\.\\w{3,4}\" content-type=\"image/\\w{3,4}\")) ?>",
					RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				while (m.Success) {
					string sourceRinaryTag = m.Value;
					// обработка ссылок
					LinksCorrector linksCorrector = new LinksCorrector( sourceRinaryTag );
					string resultBinaryTag = linksCorrector.correct();
					if ( resultBinaryTag != sourceRinaryTag )
						_xmlText = _xmlText.Replace( sourceRinaryTag, resultBinaryTag );
					m = m.NextMatch();
				}
			} catch ( RegexMatchTimeoutException /*exp*/ ) {}
			return _xmlText;
		}
	}
}
