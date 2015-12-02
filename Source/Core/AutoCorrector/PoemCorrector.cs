/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 18.11.2015
 * Время: 8:29
 * 
 * License: GPL 2.1
 */
using System;
using System.Text.RegularExpressions;

namespace Core.AutoCorrector
{
	/// <summary>
	/// Обработка Poem
	/// </summary>
	public class PoemCorrector
	{
		private const string _startTag = "<poem>";
		private const string _endTag = "</poem>";
		private string _xmlText = string.Empty;
		
		private bool _preProcess = false;
		private bool _postProcess = false;
		
		/// <summary>
		/// Конструктор класса PoemCorrector
		/// </summary>
		/// <param name="xmlText">Строка для корректировки</param>
		/// <param name="preProcess">Удаление стартовых пробелов и перевода строки => всю книгу - в одну строку</param>
		/// <param name="postProcess">Вставка разрыва абзаца между смежными тегами</param>
		public PoemCorrector( ref string xmlText, bool preProcess, bool postProcess )
		{
			_xmlText = xmlText;
			_preProcess = preProcess;
			_postProcess = postProcess;
		}
		
		/// <summary>
		/// Корректировка парных тегов Стихов
		/// </summary>
		/// <returns>Откорректированную строку типа string </returns>
		public string correct() {
			if ( _xmlText.IndexOf( _startTag ) == -1 )
				return _xmlText;
			
			// преобработка (удаление стартовых пробелов ДО тегов и удаление завершающих пробелов ПОСЛЕ тегов и символов переноса строк)
			if ( _preProcess )
				_xmlText = FB2CleanCode.preProcessing( _xmlText );
			
			/**********************************
			 * Предварительная обработка poem *
			 **********************************/
			// Удаление структур <poem><stanza /></poem>
			try {
				_xmlText = Regex.Replace(
					_xmlText, @"<poem>\s*?<stanza ?/>\s*?</poem>",
					"", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			// Удаление структур <poem><stanza><empty-line /></stanza></poem>
			try {
				_xmlText = Regex.Replace(
					_xmlText, @"<poem>\s*?<stanza>\s*?<empty-line ?/>\s*?</stanza>\s*?</poem>",
					"", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			// вставка <text-author> внутрь <poem> </poem>
			try {
				_xmlText = Regex.Replace(
					_xmlText, @"(?'_poem'</poem>)(?'ws'\s*)(?'textauthor'<text-author>\s*.+?\s*</text-author>)",
					"${textauthor}${ws}${_poem}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			
			// постобработка (разбиение на теги (смежные теги) )
			if ( _postProcess )
				_xmlText = FB2CleanCode.postProcessing( _xmlText );
			
			return _xmlText;
		}
		
	}
}
