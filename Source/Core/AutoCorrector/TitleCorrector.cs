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
			if ( _xmlText.IndexOf( _startTag, StringComparison.CurrentCulture ) == -1 )
				return _xmlText;
			
			// преобработка (удаление стартовых пробелов ДО тегов и удаление завершающих пробелов ПОСЛЕ тегов и символов переноса строк)
			if ( _preProcess )
				_xmlText = FB2CleanCode.preProcessing( _xmlText );
			
			/***********************************
			 * Предварительная обработка title *
			 ***********************************/
//			// Обработка </section> между </title> и <section> (Заголовок Книги игнорируется): </section><section><title><p><strong>Название</strong></p><p>главы</p></title></section><section>
//			try {
//				_xmlText = Regex.Replace(
//					_xmlText, @"(?'sect_before_text'</section>\s*?<section>\s*?<title>\s*?)(?'text_end_title'(?:<p>\s*?(?:<(?'tag'strong|emphasis)\b>)?\s*?(?:[^<]+)?(?:</\k'tag'>)?\s*?</p>\s*?){1,}\s*?</title>\s*?)(?'end_sect'</section>\s*?)(?=<section>)",
//					"${sect_before_text}${text_end_title}<empty-line/>\n${end_sect}", RegexOptions.IgnoreCase | RegexOptions.Multiline
//				);
//			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			// удаление обрамления <section> ... </section> у Названия книги: <body><section><title><p>Автор книги</p><empty-line /><p>Название</p><empty-line /><p>(рассказы)</p></title></section><section> => <body><title><p>Автор книги</p><empty-line /><p>Название</p><empty-line /><p>(рассказы)</p></title><section> 
			try {
				_xmlText = Regex.Replace(
					_xmlText, @"(?<=<body>)(?:\s*?<section>\s*?)(?'title'<title>\s*?)(?'texts'(?:(?:<empty-line ?/>\s*?)?(?:<p>\s*?(?:<(?'tag'strong|emphasis)\b>)?\s*?(?:[^<]+)?(?:</\k'tag'>)?\s*?</p>\s*?)(?:<empty-line ?/>\s*?)?){1,})(?'_title'\s*?</title>\s*?)(?:</section>\s*?)(?=<section>)",
					"${title}${texts}${_title}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			// Обработка Заголовка и ее </section> в конце книги перед </body>: <section><title><p><strong>Конец</strong></p><p>романа</p></title></section></body>
			try {
				_xmlText = Regex.Replace(
					_xmlText, @"(?'base_struct'<section>\s*?<title>\s*?(?:<p>\s*?(?:<(?'tag'strong|emphasis)\b>)?\s*?(?:[^<]+)?(?:</\k'tag'>)?\s*?</p>\s*?){1,}</title>\s*?)(?'end_sect'</section>\s*?)(?=</body>)",
					"${base_struct}\n<empty-line/>${end_sect}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			// внесение текста Подзаголовка в Заголовок в начале книги: <body><title><p>Заголовок</p></title><subtitle>Подзаголовок</subtitle> => <body><title><p>Заголовок</p><p>Подзаголовок</p></title>
			try {
				_xmlText = Regex.Replace(
					_xmlText, @"(?<=<body>)\s*?(?'text_title'<title>\s*?(?:<p>\s*?(?:<(?'tag'strong|emphasis)\b>)?\s*?(?:[^<]+)?(?:</\k'tag'>)?\s*?</p>\s*?){1,}\s*?)(?'_title'</title>\s*?)<subtitle>\s*?(?'text_subtitle'(?:<(?'tag_s'strong|emphasis)>)?\s*?(?:[^<]+)?(?:</\k'tag_s'>)?\s*?)</subtitle>",
					"${text_title}<p>${text_subtitle}</p>${_title}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			// Обрамление текста Заголовка тегами <p> ... </p>: <title>Текст</title> => <title><p>Текст</p></title>
			try {
				_xmlText = Regex.Replace(
					_xmlText, @"(?<=<title>)\s*?(?'text_title'(?:<(?'tag_s'strong|emphasis)>)?\s*?(?:[^<]+)?(?:</\k'tag_s'>)?\s*?)\s*?(?=</title>)",
					"<p>${text_title}</p>", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			// Удаление <empty-line/> между </title> и <section>: </title><empty-line /><section>
			try {
				_xmlText = Regex.Replace(
					_xmlText, @"(?<=</title>)?\s*?<empty-line ?/>\s*?(?=<section>)",
					"", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			// Обрамление тегами <section> ... </section> текста в тегах <p>, находящегося между </title> и <section>
			try {
				_xmlText = Regex.Replace(
					_xmlText, @"(?<=</title>)\s*?(?'pp'(?'p'<p>\s*?(?:<(?'tag'strong|emphasis)\b>)?\s*?(?:[^<]+)?(?:</\k'tag'>)?\s*?</p>\s*?){1,})\s*?(?=<section>)",
					"<section>${p}</section>", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			
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
					try {
						NewTag = Regex.Replace(
							NewTag, @"(?'title_text_end_title'<title>\s*?(?:<p>\s*?(?:<(?'tag'strong|emphasis)\b>)?\s*?(?:[^<]+)?(?:</\k'tag'>)?\s*?</p>\s*?){1,}\s*?</title>)",
							"${title_text_end_title}<empty-line/>", RegexOptions.IgnoreCase | RegexOptions.Multiline
						);
					} catch ( RegexMatchTimeoutException /*ex*/ ) {}
				} else if ( tagPair.PreviousTag.Equals("</cite>") && tagPair.NextTag.Equals("<p>") ) {
					// вставка тегов </section><section>: </cite><title><p>Текст</p><p><strong>Текст</strong></p></title><p>Текст</p> => </cite></section><section><title><p>Текст</p><p><strong>Текст</strong></p></title><p>Текст</p>
					NewTag = "</section><section>" + tagPair.PairTag;
				} else if ( tagPair.PreviousTag.Equals("<body>") && tagPair.NextTag.Equals("<section>") ) {
					// замена <v> ... </v> в Заголовке книги на <p> ... </p>
					NewTag = NewTag.Replace("<v>", "<p>").Replace("</v>", "</p>");
				} else {
					// замена <subtitle>Text</subtitle> внутри тегов <title на  <p>Text</p>
					NewTag = NewTag.Replace("<subtitle>", "<p>").Replace("</subtitle>", "</p>");
				}
				
				Index = XmlText.IndexOf( tagPair.PairTag, tagPair.StartTagPosition, StringComparison.CurrentCulture ) + NewTag.Length;
				XmlText = XmlText.Substring( 0, tagPair.StartTagPosition ) /* ДО обрабатываемого текста */
					+ NewTag
					+ XmlText.Substring( tagPair.EndTagPosition ); /* ПОСЛЕ обрабатываемого текста */
			}
		}
		
	}
}
