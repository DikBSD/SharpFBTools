/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 05.11.2015
 * Время: 8:13
 * 
 * License: GPL 2.1
 */
using System;
using System.Text.RegularExpressions;

namespace Core.AutoCorrector
{
	/// <summary>
	/// Обработка ссылок
	/// </summary>
	public class LinksCorrector
	{
		private string _xmlText = string.Empty;
		
		// некорректное id ссылки (начинается с цифры)
		private readonly static string _UnCorrectID_Numb_Query = "id=\"(?'linknumber'\\d[^\"]*)\"";
		private readonly static string _UnCorrectID_Numb_Repl = "id=\"_${linknumber}\"";
		
		// некорректное id ссылки (символ @)
		private readonly static string _UnCorrectID_O_Query = "(?'start'id=\"[_\\w\\d']+)(?'del'@)(?'end'[^\"]*\")";
		private readonly static string _UnCorrectID_O_Repl = "${start}${end}";
		
		// некорректное id ссылки (символ ')
		private readonly static string _UnCorrectID_Apost_Query = "(?'start'id=\"[_\\w\\d@]+)(?'del'')(?'end'[^\"]*\")";
		private readonly static string _UnCorrectID_Apost_Repl = "${start}${end}";
		
		// обработка Либрусековских id
		private readonly static string _UnCorrectLibrusecID_Numb_Query = "=\"#(?'linknumber'\\d[^\"]*)\"";
		private readonly static string _UnCorrectLibrusecID_Numb_Repl = "=\"#_${linknumber}\"";
		
		// обработка Либрусековских id (символ @)
		private readonly static string _UnCorrectLibrusecID_O_Query = "(?'start'=\"#[_\\w\\d']+)(?'del'@)(?'end'[^\"]*\")";
		private readonly static string _UnCorrectLibrusecID_O_Repl = "${start}${end}";
		
		// обработка Либрусековских id (символ ')
		private readonly static string _UnCorrectLibrusecID_Apost_Query = "(?'start'=\"#[_\\w\\d@]+)(?'del'')(?'end'[^\"]*\")";
		private readonly static string _UnCorrectLibrusecID_Apost_Repl = "${start}${end}";
		
		/// <summary>
		/// Конструктор класса LinksCorrector
		/// </summary>
		/// <param name="xmlText">Строка для корректировки</param>
		public LinksCorrector( string xmlText )
		{
			_xmlText = xmlText;
		}
		
		/// <summary>
		/// Корректировка ссылок
		/// </summary>
		/// <returns>Откорректированную строку типа string </returns>
		public string correct() {
			// некорректное id ссылки (начинается с цифры)
			_xmlText = Regex.Replace(
				_xmlText, _UnCorrectID_Numb_Query,
				_UnCorrectID_Numb_Repl, RegexOptions.None
			);
			// некорректное id ссылки (символ @)
			_xmlText = Regex.Replace(
				_xmlText, _UnCorrectID_O_Query,
				_UnCorrectID_O_Repl, RegexOptions.None
			);
			// некорректное id ссылки (символ ')
			_xmlText = Regex.Replace(
				_xmlText, _UnCorrectID_Apost_Query,
				_UnCorrectID_Apost_Repl, RegexOptions.None
			);
			
			// обработка Либрусековских id
			_xmlText = Regex.Replace(
				_xmlText, _UnCorrectLibrusecID_Numb_Query,
				_UnCorrectLibrusecID_Numb_Repl, RegexOptions.None
			);
			// обработка Либрусековских id (символ @)
			_xmlText = Regex.Replace(
				_xmlText, _UnCorrectLibrusecID_O_Query,
				_UnCorrectLibrusecID_O_Repl, RegexOptions.None
			);
			// обработка Либрусековских id (символ ')
			_xmlText = Regex.Replace(
				_xmlText, _UnCorrectLibrusecID_Apost_Query,
				_UnCorrectLibrusecID_Apost_Repl, RegexOptions.None
			);
			
			return _xmlText;
		}
	}
}
