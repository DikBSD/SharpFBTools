/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 15.12.2015
 * Время: 14:05
 * 
 * License: GPL 2.1
 */
using System;
using System.Text.RegularExpressions;

namespace Core.AutoCorrector
{
	/// <summary>
	/// Обработка Таблиц
	/// </summary>
	public class TableCorrector
	{
		private const string _startTag = "<table>";
		private const string _endTag = "</table>";
		private string _xmlText = string.Empty;
		
		private bool _preProcess = false;
		private bool _postProcess = false;
		
		/// <summary>
		/// Конструктор класса TableCorrector
		/// </summary>
		/// <param name="xmlText">Строка для корректировки</param>
		/// <param name="preProcess">Удаление стартовых пробелов и перевода строки => всю книгу - в одну строку</param>
		/// <param name="postProcess">Вставка разрыва абзаца между смежными тегами</param>
		public TableCorrector( ref string xmlText, bool preProcess, bool postProcess )
		{
			_xmlText = xmlText;
			_preProcess = preProcess;
			_postProcess = postProcess;
		}
		
		/// <summary>
		/// Корректировка парных тегов Таблиц
		/// </summary>
		/// <returns>Откорректированную строку типа string </returns>
		public string correct() {
			if ( _xmlText.IndexOf( _startTag ) == -1 )
				return _xmlText;
			
			// преобработка (удаление стартовых пробелов ДО тегов и удаление завершающих пробелов ПОСЛЕ тегов и символов переноса строк)
			if ( _preProcess )
				_xmlText = FB2CleanCode.preProcessing( _xmlText );
			
			/***********************************
			 * Предварительная обработка table *
			 ***********************************/
			// ...
			
			// обработка найденных парных тэгов
			IWorker worker = new TableCorrectorWorker();
			TagWorker tagWorker = new TagWorker( ref _xmlText, _startTag, _endTag, ref worker );
			_xmlText = tagWorker.Work();
			
			// постобработка (разбиение на теги (смежные теги) )
			if ( _postProcess )
				_xmlText = FB2CleanCode.postProcessing( _xmlText );
			
			return _xmlText;
		}
		
		public class TableCorrectorWorker : IWorker
		{
			/// <summary>
			/// Обработчик найденных парных тегов Таблиц
			/// </summary>
			/// <param name="tagPair">Экземпляр класса поиска парных тегов с вложенными тегами любой сложности вложения</param>
			/// <param name="XmlText">Текст строки для корректировки в xml представлении</param>
			/// <param name="Index">Индекс начала поиска открывающего тэга</param>
			public void DoWork( ref TagPair tagPair, ref string XmlText, ref int Index ) {
				if ( string.IsNullOrWhiteSpace( tagPair.PairTag ) )
					return;
				
				string NewTag = tagPair.PairTag;

				// преобразование таблиц: <table><tr><td /><image l:href="#image2.jpg" /><empty-line /></tr><tr><td>Текст</emphasis></td></tr></table> => <table><tr><td><image l:href="#image2.jpg" /></td></tr><tr><td>Текст</td></tr></table>
				try {
					NewTag = Regex.Replace(
						NewTag, "(?'table'<table>\\s*<tr>)\\s*<td ?/>\\s*(?'img'<image[^/]+?(?:\"[^\"]*\"|'[^']*')?/>)\\s*<empty-line ?/>\\s*(?'_tr_table'</tr>\\s*<tr>\\s*<td>.+</td>\\s*</tr>\\s*</table>)",
						"${table}<td>${img}</td>${_tr_table}", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException /*ex*/ ) {}
				
				Index = XmlText.IndexOf( tagPair.PairTag, tagPair.StartTagPosition ) + NewTag.Length;
				XmlText = XmlText.Substring( 0, tagPair.StartTagPosition ) /* ДО обрабатываемого текста */
					+ NewTag
					+ XmlText.Substring( tagPair.EndTagPosition ); /* ПОСЛЕ обрабатываемого текста */
			}
		}
		
	}
}
