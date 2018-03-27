/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 18.11.2015
 * Время: 13:41
 * 
 * License: GPL 2.1
 */
using System;
using System.Text.RegularExpressions;

namespace Core.AutoCorrector
{
	/// <summary>
	/// Обработка дат
	/// </summary>
	public class DateCorrector
	{
		private string _xmlText = string.Empty;
		
		private bool _preProcess = false;
		private bool _postProcess = false;
		
		/// <summary>
		/// Конструктор класса DateCorrector
		/// </summary>
		/// <param name="xmlText">Строка для корректировки</param>
		/// <param name="preProcess">Удаление стартовых пробелов и перевода строки => всю книгу - в одну строку</param>
		/// <param name="postProcess">Вставка разрыва абзаца между смежными тегами</param>
		public DateCorrector( ref string xmlText, bool preProcess, bool postProcess )
		{
			_xmlText = xmlText;
			_preProcess = preProcess;
			_postProcess = postProcess;
		}
		
		/// <summary>
		/// Корректировка тегов date
		/// </summary>
		/// <returns>Откорректированную строку типа string </returns>
		public string correct() {
			if ( _xmlText.IndexOf( "<date", StringComparison.CurrentCulture ) == -1 )
				return _xmlText;
			
			// преобработка (удаление стартовых пробелов ДО тегов и удаление завершающих пробелов ПОСЛЕ тегов и символов переноса строк)
			if ( _preProcess )
				_xmlText = FB2CleanCode.preProcessing( _xmlText );
			
			/******************
			 * Обработка date *
			 ******************/
			// обработка атрибута в датах типа <date value="16.01.2006"></date> => <date value="2006-01-16"></date>
			// или <date value="16.01.2006">Текст</date> => <date value="2006-01-16">Текст</date>
			_xmlText = Regex.Replace(
				_xmlText, "(?'start'<date +?value=\")(?'d'\\d\\d)\\.(?'m'\\d\\d)\\.(?'y'\\d\\d\\d\\d)(?'end'\">(?:\\s*?</date>))",
				"${start}${y}-${m}-${d}${end}", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка атрибута в датах типа <date value="16.01.2006">16.01.2006</date> => <date value="2006-01-16">16.01.2006</date>
			_xmlText = Regex.Replace(
				_xmlText, "(?'start'<date +?value=\")(?'d'\\d\\d)\\.(?'m'\\d\\d)\\.(?'y'\\d\\d\\d\\d)(?'end'\">(?:\\d\\d\\.\\d\\d\\.\\d\\d\\d\\d</date>))",
				"${start}${y}-${m}-${d}${end}", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка атрибута в датах типа <date value="2008.04.30">2008.04.30</date> => <date value="2008-04-30">2008.04.30</date>
			_xmlText = Regex.Replace(
				_xmlText, "(?'start'<date +?value=\")(?'y'\\d\\d\\d\\d)\\.(?'m'\\d\\d)\\.(?'d'\\d\\d)(?'end'\">(?:[^<]+?)?(?:\\s*?</date>))",
				"${start}${y}-${m}-${d}${end}", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка атрибута в датах типа <date value="2015-08-24 15:22:32">24 авг 2015</date> - удаление времени
			_xmlText = Regex.Replace(
				_xmlText, "(?<=<date value=\"\\d\\d\\d\\d-\\d\\d-\\d\\d)(?: [^<]+?)(?=(?:\">[^<]+?)</date>)",
				"", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// обработка атрибута в датах типа <date value="2015-08-24 15:22:32"></date> - удаление времени
			_xmlText = Regex.Replace(
				_xmlText, "(?<=<date value=\"\\d\\d\\d\\d-\\d\\d-\\d\\d)(?: [^<]+?)(?=(?:\">)</date>)",
				"", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// удаление атрибута в датах типа <date value="05-06-21">21.06.05</date>
			_xmlText = Regex.Replace(
				_xmlText, "(?<=<date) value=\"\\d\\d[-.]\\d\\d[-.]\\d\\d\">\\s*?(?=(?:[^<]+?)?\\s*?</date>)",
				">", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// удаление атрибута в датах типа <date value="2006">2006</date>
			_xmlText = Regex.Replace(
				_xmlText, "(?<=<date) value=\"\\d\\d\\d\\d\">\\s*?(?=(?:[^<]+?)?\\s*?</date>)",
				">", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			
			// обработка атрибута в датах типа <date value="20.09.2017" /> => <date value="2017-09-20" />
			_xmlText = Regex.Replace(
				_xmlText, "(?'start'<date +?value=\")(?'d'\\d\\d)\\.(?'m'\\d\\d)\\.(?'y'\\d\\d\\d\\d)(?'end'\" ?/>)",
				"${start}${y}-${m}-${d}${end}", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			
			// постобработка (разбиение на теги (смежные теги) )
			if ( _postProcess )
				_xmlText = FB2CleanCode.postProcessing( _xmlText );
			
			return _xmlText;
		}
	}
}
