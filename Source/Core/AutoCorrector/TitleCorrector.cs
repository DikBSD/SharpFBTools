/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 30.10.2015
 * Время: 7:21
 * 
 * License: GPL 2.1
 */
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace Core.AutoCorrector
{
	/// <summary>
	/// Обработка Заголовков
	/// </summary>
	public class TitleCorrector
	{
		private const string _startTag = "<title>";
		private const string _endTag = "</title>";
		private string _xmlText = string.Empty;
		
		private bool _preProcess = false;
		private bool _postProcess = false;
		
		/// <summary>
		/// Конструктор класса TitleCorrector
		/// </summary>
		/// <param name="xmlText">Строка для корректировки</param>
		/// <param name="preProcess">Удаление стартовых пробелов и перевода строки => всю книгу - в одну строку</param>
		/// <param name="postProcess">Вставка разрыва абзаца между смежными тегами</param>
		public TitleCorrector( ref string xmlText, bool preProcess, bool postProcess )
		{
			_xmlText = xmlText;
			_preProcess = preProcess;
			_postProcess = postProcess;
		}
		
		/// <summary>
		/// Корректировка парных тегов Заголовка
		/// </summary>
		/// <returns>Откорректированную строку типа string </returns>
		public string correct() {
			if ( _xmlText.IndexOf( _startTag ) == -1 )
				return _xmlText;
			
			// преобработка (удаление стартовых пробелов ДО тегов и удаление завершающих пробелов ПОСЛЕ тегов и символов переноса строк)
			if ( _preProcess )
				_xmlText = FB2CleanCode.preProcessing( _xmlText );
			
			/***********************************
			 * Предварительная обработка title *
			 ***********************************/
//			// Обработка </section> между </title> и <section> (Заголовок Книги игнорируется): </section><section><title><p><strong>Название</strong></p><p>главы</p></title></section><section>
//			_xmlText = Regex.Replace(
//				_xmlText, @"(?'sect_before_text'</section>\s*?<section>\s*?<title>\s*?)(?'text_end_title'(?:<p>\s*?(?:<(?'tag'strong|emphasis)\b>)?\s*?(?:[^<]+)?(?:</\k'tag'>)?\s*?</p>\s*?){1,}\s*?</title>\s*?)(?'end_sect'</section>\s*?)(?=<section>)",
//				"${sect_before_text}${text_end_title}<empty-line/>\n${end_sect}", RegexOptions.IgnoreCase | RegexOptions.Multiline
//			);
			// удаление <section> </section> вокруг Заголовка Книги: <body><section><title><p><strong>Название</strong></p><p>главы</p></title></section><section>
			_xmlText = Regex.Replace(
				_xmlText, @"(?<=<body>)(?:\s*?<section>\s*?)(?'title'<title>\s*?)(?'text_title'(?:<p>\s*?(?:<(?'tag'strong|emphasis)\b>)?\s*?(?:[^<]+)?(?:</\k'tag'>)?\s*?</p>\s*?){1,}\s*?</title>\s*?)(?:</section>\s*?)(?=<section>)",
				"${title}${text_title}", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// Обработка Заголовка и ее </section> в конце книги перед </body>: <section><title><p><strong>Конец</strong></p><p>романа</p></title></section></body>
			_xmlText = Regex.Replace(
				_xmlText, @"(?'base_struct'<section>\s*?<title>\s*?(?:<p>\s*?(?:<(?'tag'strong|emphasis)\b>)?\s*?(?:[^<]+)?(?:</\k'tag'>)?\s*?</p>\s*?){1,}</title>\s*?)(?'end_sect'</section>\s*?)(?=</body>)",
				"${base_struct}\n<empty-line/>${end_sect}", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			
			// обработка найденных парных тэгов
			IWorker worker = new TitleCorrectorWorker();
			TagWorker tagWorker = new TagWorker( ref _xmlText, _startTag, _endTag, ref worker );
			_xmlText = tagWorker.Work();
			
			// постобработка (разбиение на теги (смежные теги) )
			if ( _postProcess )
				_xmlText = FB2CleanCode.postProcessing( _xmlText );
			
			return _xmlText;
		}
		
		public class TitleCorrectorWorker : IWorker
		{
			/// <summary>
			/// Обработчик найденных парных тегов Заголовка
			/// </summary>
			/// <param name="tagPair">Экземпляр класса поиска парных тегов с вложенными тегами любой сложности вложения</param>
			/// <param name="XmlText">Текст строки для корректировки в xml представлении</param>
			/// <param name="Index">Индекс начала поиска открывающего тэга</param>
			public void DoWork( ref TagPair tagPair, ref string XmlText, ref int Index ) {
				if ( string.IsNullOrWhiteSpace( tagPair.PairTag ) )
					return;
				string NewTag = tagPair.PairTag;
				if ( tagPair.PreviousTag.Equals("</p>") && tagPair.NextTag.Equals("<p>") ) {
					// вставка тегов </section><section>: <p>Text</p><title><p><strong>Text</strong></p></title><p> => <p>Text</p></section><section><title><p><strong>Text</strong></p></title><p>
					NewTag = "</section><section>" + tagPair.PairTag;
				} else if ( tagPair.PreviousTag.Equals("<section>") && tagPair.NextTag.Equals("</section>") ) {
					// Вставка <empty-line/> между </title> и <section> (Заголовок Книги игнорируется): <section><title><p><strong>Название</strong></p><p>главы</p></title></section>
					NewTag = Regex.Replace(
						NewTag, @"(?'title_text_end_title'<title>\s*?(?:<p>\s*?(?:<(?'tag'strong|emphasis)\b>)?\s*?(?:[^<]+)?(?:</\k'tag'>)?\s*?</p>\s*?){1,}\s*?</title>)",
						"${title_text_end_title}<empty-line/>", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} else if ( tagPair.PreviousTag.Equals("</cite>") && tagPair.NextTag.Equals("<p>") ) {
					// вставка тегов </section><section>: </cite><title><p>Текст</p><p><strong>Текст</strong></p></title><p>Текст</p> => </cite></section><section><title><p>Текст</p><p><strong>Текст</strong></p></title><p>Текст</p>
					NewTag = "</section><section>" + tagPair.PairTag;
				} else {
					// замена <subtitle>Text</subtitle> внутри тегов <title на  <p>Text</p>
					NewTag = NewTag.Replace("<subtitle>", "<p>").Replace("</subtitle>", "</p>");
				}
				
				Index = XmlText.IndexOf( tagPair.PairTag, tagPair.StartTagPosition ) + NewTag.Length;
				XmlText = XmlText.Substring( 0, tagPair.StartTagPosition ) /* ДО обрабатываемого текста */
					+ NewTag
					+ XmlText.Substring( tagPair.EndTagPosition ); /* ПОСЛЕ обрабатываемого текста */
			}
		}
		
	}
}
