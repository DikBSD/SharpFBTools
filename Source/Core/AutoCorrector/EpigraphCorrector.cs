/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 28.10.2015
 * Время: 7:31
 * 
 * License: GPL 2.1
 */
using System;
using System.Text.RegularExpressions;

namespace Core.AutoCorrector
{
	/// <summary>
	/// Обработка Эпиграфов
	/// </summary>
	public class EpigraphCorrector
	{
		private const string _startTag = "<epigraph>";
		private const string _endTag = "</epigraph>";
		private string _xmlText = string.Empty;
		
		private bool _preProcess = false;
		private bool _postProcess = false;
		
		/// <summary>
		/// Конструктор класса EpigraphCorrector
		/// </summary>
		/// <param name="xmlText">Строка для корректировки</param>
		/// <param name="preProcess">Удаление стартовых пробелов и перевода строки => всю книгу - в одну строку</param>
		/// <param name="postProcess">Вставка разрыва абзаца между смежными тегами</param>
		public EpigraphCorrector( ref string xmlText, bool preProcess, bool postProcess )
		{
			_xmlText = xmlText;
			_preProcess = preProcess;
			_postProcess = postProcess;
		}
		
		/// <summary>
		/// Корректировка парных тегов Эпиграфа
		/// </summary>
		/// <returns>Откорректированную строку типа string </returns>
		public string correct() {
			if ( _xmlText.IndexOf( _startTag ) == -1 )
				return _xmlText;
			
			// преобработка (удаление стартовых пробелов ДО тегов и удаление завершающих пробелов ПОСЛЕ тегов и символов переноса строк)
			if ( _preProcess )
				_xmlText = FB2CleanCode.preProcessing( _xmlText );
			
			/******************************************************
			 * Предварительная обработка epigraph и <empty-line/> *
			 ******************************************************/
			// Вставка между </epigraph> и </section> недостающего тега <empty-line/>
			try {
				_xmlText = Regex.Replace(
					_xmlText, @"(?'tag'(?:</epigraph>))(?:\s*)(?'_sect'</section>)",
					"${tag}<empty-line/>${_sect}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			// удаление <empty-line /> между </title> и <epigraph> </title><empty-line/><epigraph><p>Эпиграф</p></epigraph> => </title><epigraph><p>Эпиграф</p></epigraph>
			try {
				_xmlText = Regex.Replace(
					_xmlText, @"(?'_title'</title>)(?:\s*?)(?:<empty-line ?/>)(?:\s*?)(?'epigraph'<epigraph>)",
					"${_title}${epigraph}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			// удаление <empty-line /> между </epigraph> и <epigraph>: </epigraph><empty-line /><epigraph> => </epigraph>\n<epigraph>
			try {
				_xmlText = Regex.Replace(
					_xmlText, @"(?<=</epigraph>)(?:\s*<empty-line ?/>\s*)(?=<epigraph>)",
					"", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			// удаление <empty-line /> между </epigraph> и <section>: </epigraph><empty-line /><section> => </epigraph>\n<section>
			try {
				_xmlText = Regex.Replace(
					_xmlText, @"(?<=</epigraph>)(?:\s*<empty-line ?/>\s*)(?=<section>)",
					"", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			// Перестановка местами </epigraph> и тег <text-author> с его данными
			try {
				_xmlText = Regex.Replace(
					_xmlText, @"(?'_epigraph'</epigraph>)\s*?(?'text_a'(?:<text-author>\s*?(?:<(?'tag'strong|emphasis)\b>)?[^<]+?(?:</\k'tag'>)?\s*?</text-author>\s*?){1,})",
					"${text_a}${_epigraph}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			
			// обработка найденных парных тэгов
			IWorker worker = new EpigraphCorrectorWorker();
			TagWorker tagWorker = new TagWorker( ref _xmlText, _startTag, _endTag, ref worker );
			_xmlText = tagWorker.Work();
			
			// постобработка (разбиение на теги (смежные теги) )
			if ( _postProcess )
				_xmlText = FB2CleanCode.postProcessing( _xmlText );
			
			return _xmlText;
		}
		
		public class EpigraphCorrectorWorker : IWorker
		{
			/// <summary>
			/// Обработчик найденных парных тегов Эпиграфа
			/// </summary>
			/// <param name="tagPair">Экземпляр класса поиска парных тегов с вложенными тегами любой сложности вложения</param>
			/// <param name="XmlText">Текст строки для корректировки в xml представлении</param>
			/// <param name="Index">Индекс начала поиска открывающего тэга</param>
			public void DoWork( ref TagPair tagPair, ref string XmlText, ref int Index ) {
				if ( string.IsNullOrWhiteSpace( tagPair.PairTag ) )
					return;
				
				string NewTag = tagPair.PairTag;
				if ( tagPair.PreviousTag.Equals("</p>") ) {
					if ( tagPair.NextTag.Equals("<p>") ) {
						// Преобразование вложенных друг в друга тегов epigraph в Автора: <epigraph><epigraph><p>Иванов</p></epigraph></epigraph> => <cite><text-author><p>Иванов</p></text-author></cite>
						try {
							NewTag = Regex.Replace(
								NewTag, @"(?:(?:<(?'tag'epigraph)\b>\s*?){2})(?:<p>\s*?)(?'text'(?:<(?'tag1'strong|emphasis)\b>)?\s*?(?:[^<]+)(?:</\k'tag1'>)?)(?:\s*?</p>)(?:\s*?</\k'tag'>){2}",
								"<${tag}><text-author>${text}</text-author></${tag}>", RegexOptions.IgnoreCase | RegexOptions.Multiline
							);
						} catch ( RegexMatchTimeoutException /*ex*/ ) {}
					}
					// замена epigraph на cite: <p>Text</p><epigraph><p><strong>Text</strong></p></epigraph> => <p>Text</p><cite><p><strong>Text</strong></p></cite>
					NewTag = NewTag.Replace("<epigraph>", "<cite>").Replace("</epigraph>", "</cite>");
				} else {
					if ( tagPair.StartTagCount == 1  ) {
						if ( tagPair.PreviousTag.Equals("</subtitle>") || tagPair.PreviousTag.Equals("</subtitle><empty-line/>") || tagPair.PreviousTag.Equals("</subtitle><empty-line />") ) {
							// обработка Эпиграфа с вложенным Эпиграфом вместо Автора эпиграфа: <subtitle>...</subtitle><epigraph><emphasis><p><emphasis>Текст</emphasis></p><emphasis><p>Достоевский</p></emphasis></emphasis></epigraph>
							try {
								NewTag = Regex.Replace(
									NewTag, @"(?:<epigraph>\s*?)(?:<(?'tag1'strong|emphasis)\b>\s*?)(?'texts'(?:<p>\s*?(?:<(?'tag'strong|emphasis)\b>)?[^<]+?(?:</\k'tag'>)?\s*?</p>\s*?){1,}\s*?)(?:<(?'tag2'emphasis|strong)\b>)\s*?(?'p'<p>)(?'author'[^<]+?)(?'_p'</p>\s*?)</\k'tag2'>\s*?</\k'tag1'>(?:\s*?</epigraph>)",
									"<cite>${texts}<text-author><${tag2}>${author}</${tag2}></text-author></cite>", RegexOptions.IgnoreCase | RegexOptions.Multiline
								);
							} catch ( RegexMatchTimeoutException /*ex*/ ) {}
							// Преобразование Эпиграфа в Цитату в случае <subtitle>Текст</subtitle><empty-line/><epigraph><p>Текст</p></epigraph>
							// Преобразование Эпиграфа в Цитату в случае <subtitle>Текст</subtitle><epigraph><p>Текст</p></epigraph>
							// @"(?:<epigraph>\s*?)(?'texts'(<p>\s*?(?:<(?'tag1'strong|emphasis)\b>)?[^<]+?(?:</\k'tag1'>)?\s*?</p>\s*?){1,})\s*?(?:\s*?</epigraph>)"
							try {
								NewTag = Regex.Replace(
									NewTag, @"(?:<epigraph>\s*?)(?'texts'(?:<p>\s*?.+?\s*?</p>\s*?){1,})(?:\s*?</epigraph>)",
									"<cite>${texts}</cite>", RegexOptions.IgnoreCase | RegexOptions.Multiline
								);
							} catch ( RegexMatchTimeoutException /*ex*/ ) {}
							// Преобразование Эпиграфа в Цитату в случае <subtitle>Текст</subtitle><empty-line/><epigraph><emphasis><p>Текст</p></emphasis></epigraph>
							// Преобразование Эпиграфа в Цитату в случае <subtitle>Текст</subtitle><epigraph><emphasis><p>Текст</p></emphasis></epigraph>
							// @"(?:<epigraph>\s*?)(?:<(?'tag'strong|emphasis)\b>\s*?)(?'texts'(<p>\s*?(?:<(?'tag1'strong|emphasis)\b>)?[^<]+?(?:</\k'tag1'>)?\s*?</p>\s*?){1,})\s*?</\k'tag'>(?:\s*?</epigraph>)"
							try {
								NewTag = Regex.Replace(
									NewTag, @"(?:<epigraph>\s*?)(?:<(?'tag'strong|emphasis)\b>\s*?)(?'texts'(?:<p>\s*?.+?\s*?</p>\s*?){1,})\s*?</\k'tag'>(?:\s*?</epigraph>)",
									"<cite>${texts}</cite>", RegexOptions.IgnoreCase | RegexOptions.Multiline
								);
							} catch ( RegexMatchTimeoutException /*ex*/ ) {}
							// Преобразование Эпиграфа в Цитату в случае </subtitle><epigraph><p>Текст</p><text-author>Автор</text-author></epigraph>
							try {
								NewTag = Regex.Replace(
									NewTag, @"(?:<epigraph>\s*?)(?'texts'(?:<p>\s*?.+?\s*?</p>\s*?){1,}\s*?(?:<text-author>\s*?.+?\s*?</text-author>\s*?))(?:\s*?</epigraph>)",
									"<cite>${texts}</cite>", RegexOptions.IgnoreCase | RegexOptions.Multiline
								);
							} catch ( RegexMatchTimeoutException /*ex*/ ) {}
						} else if ( tagPair.PreviousTag.Equals("</section>") && tagPair.NextTag.Equals("<section>") ) {
							// обрамление Эпиграфа тегами <section> ... <empty-line /></section> в случае если до Эпиграфа стоит тег </section>, а после Эпиграфа - тег <section> : </section><epigraph><p>Текст</p><p>Текст</p></epigraph><section> => </section><section><epigraph><p>Текст</p><p>Текст</p></epigraph><empty-line/></section><section>
							NewTag = "<section>" + NewTag + "<empty-line /></section>";
						} else if ( tagPair.PreviousTag.Equals("</title>") || tagPair.PreviousTag.Equals("<section>") ) {
							// Обработка Эпиграфа с Аннотацией в случае: </title><epigraph><annotation><p>Текст.</p><p>Текст.</p><empty-line /><p>Текст</p><p><emphasis>Текст</emphasis></p><p>Текст</p></annotation></epigraph>
							try {
								NewTag = Regex.Replace(
									NewTag, @"(?'epigraph'<epigraph>)(?:\s*?)(?:<annotation>\s*)(?'text'\s*?.+?\s*?)(?:</annotation>\s*?)(?'_epigraph'</epigraph>)",
									"${epigraph}${text}${_epigraph}", RegexOptions.IgnoreCase | RegexOptions.Multiline | RegexOptions.Singleline
								);
							} catch ( RegexMatchTimeoutException /*ex*/ ) {}
						} else if ( tagPair.PreviousTag.IndexOf("<empty-line />") != -1 || tagPair.PreviousTag.IndexOf("<empty-line/>") != -1) {
							// замена epigraph на cite в случае, если перед простым Эпиграфом находится </p><empty-line />
							NewTag = NewTag.Replace( "<epigraph>", "<cite>" ).Replace( "</epigraph>", "</cite>" );
						} else if ( tagPair.PreviousTag.Equals("</cite>") || tagPair.PreviousTag.Equals("</poem>") ) {
							// замена epigraph на cite в случае, если перед простым Эпиграфом находится </p><empty-line />
							NewTag = NewTag.Replace( "<epigraph>", "<cite>" ).Replace( "</epigraph>", "</cite>" );
						} else if ( tagPair.PreviousTag.Equals("</annotation>") ) {
							// обрамление Эпиграфа тегами <section> ... <empty-line /></section>
							NewTag = "<section>" + NewTag + "<empty-line /></section>";
						}
					} else if ( tagPair.StartTagCount == 2 ) {
						if ( tagPair.PreviousTag.Equals("</subtitle>") || tagPair.PreviousTag.Equals("</subtitle><empty-line/>") || tagPair.PreviousTag.Equals("</subtitle><empty-line />") ) {
							// Преобразование Эпиграфа в Цитату в случае <subtitle>...</subtitle><epigraph><emphasis><p><emphasis>Этот стих звучит так:</emphasis></p><epigraph><p><emphasis>И счастья баловень безродный</emphasis></p><p><emphasis>Полудержавный властелин.</emphasis></p></epigraph></emphasis></epigraph>
							try {
								NewTag = Regex.Replace(
									NewTag, @"(?:<epigraph>\s*?)(?:<(?'tag'strong|emphasis)\b>\s*?)(?'texts1'(<p>\s*?(?:<(?'tag1'strong|emphasis)\b>)?[^<]+?(?:</\k'tag1'>)?\s*?</p>\s*?){1,})\s*?(<epigraph>\s*?)(?'texts2'(<p>\s*?(?:<(?'tag2'strong|emphasis)\b>)?[^<]+?(?:</\k'tag2'>)?\s*?</p>\s*?){1,})\s*?(?:</epigraph>\s*?)</\k'tag'>\s*?(?:</epigraph>)",
									"<cite>${texts1}${texts2}</cite>", RegexOptions.IgnoreCase | RegexOptions.Multiline
								);
							} catch ( RegexMatchTimeoutException /*ex*/ ) {}
						} else if ( tagPair.PreviousTag.Equals("</title>") || tagPair.PreviousTag.Equals("<section>") || tagPair.PreviousTag.Equals("</epigraph>")  ) {
							// обработка Эпиграфа с вложенным Эпиграфом вместо Автора эпиграфа: <epigraph><p>Текст</p><p>Текст</p><epigraph><emphasis><p>Достоевский</p></emphasis></epigraph></epigraph><epigraph>
							try {
								NewTag = Regex.Replace(
									NewTag, @"(?'epigraph'<epigraph>\s*?)(?'texts'(?:<p>\s*?(?:<(?'tag'strong|emphasis)\b>)?[^<]+?(?:</\k'tag'>)?\s*?</p>\s*?){1,})\s*?<epigraph>\s*?<(?'tag1'emphasis|strong)\b>(?:\s*?<p>)(?'author'[^<]+?)(?:</p>\s*?)</\k'tag1'>\s*?</epigraph>\s*?(?'_epigraph'</epigraph>)",
									"${epigraph}${texts}<text-author><${tag1}>${author}</${tag1}></text-author>\n${_epigraph}", RegexOptions.IgnoreCase | RegexOptions.Multiline
								);
							} catch ( RegexMatchTimeoutException /*ex*/ ) {}
							// обработка Эпиграфа с вложенным Эпиграфом вместо Автора эпиграфа: <epigraph><p>Текст</p><p>Текст</p><epigraph><emphasis><emphasis>Достоевский</emphasis></emphasis></epigraph></epigraph><epigraph>
							try {
								NewTag = Regex.Replace(
									NewTag, @"(?'epigraph'<epigraph>\s*?)(?'texts'(?:<p>\s*?(?:<(?'tag'strong|emphasis)\b>)?[^<]+?(?:</\k'tag'>)?\s*?</p>\s*?){1,})\s*?<epigraph>\s*?<(?'tag1'emphasis|strong)\b>(\s*?<(?'tag_a'emphasis|strong)>)(?'author'[^<]+?)(?:</\k'tag_a'>\s*?)</\k'tag1'>\s*?</epigraph>\s*?(?'_epigraph'</epigraph>)",
									"${epigraph}${texts}<text-author><${tag1}>${author}</${tag1}></text-author>\n${_epigraph}", RegexOptions.IgnoreCase | RegexOptions.Multiline
								);
							} catch ( RegexMatchTimeoutException /*ex*/ ) {}
							// обработка Эпиграфа с текстом и Эпиграфом вместо Автора эпиграфа: <epigraph><p><emphasis>Текст</emphasis></p><p>Текст</p><epigraph><p><emphasis>Достоевский</emphasis></p></epigraph></epigraph><epigraph>
							try {
								NewTag = Regex.Replace(
									NewTag, @"(?'epigraph'<epigraph>\s*)(?'texts'(?:<p>\s*?(?:<(?'tag'strong|emphasis)\b>)?[^<]+?(?:</\k'tag'>)?\s*?</p>\s*?){1,}\s*?)<epigraph>\s*<p>\s*(?:<(?'tag1'strong|emphasis)\b>\s*?)(?'author'[^<]+?)</\k'tag1'>\s*</p>\s*?</epigraph>\s*?(?'_epigraph'</epigraph>)",
									"${epigraph}${texts}<text-author><${tag1}>${author}</${tag1}></text-author>${_epigraph}", RegexOptions.IgnoreCase | RegexOptions.Multiline
								); //
							} catch ( RegexMatchTimeoutException /*ex*/ ) {}
						}
						
						// Обработка Эпиграфа вида: <epigraph><empty-line/><p>Текст</p><p>Текст.</p><epigraph><empty-line/><p><emphasis>Автор</emphasis> </p></epigraph></epigraph> => <epigraph><empty-line/><p>Текст</p><p>Текст.</p><text-author><emphasis>Автор</emphasis></text-author></epigraph>
						try {
							NewTag = Regex.Replace(
								NewTag, @"(?<=<epigraph>)\s*(?'p'(?:<empty-line ?/>)?\s*(?:<p>\s*?(?:<(?'tag'strong|emphasis)\b>)?[^<]+?(?:</\k'tag'>)?\s*?</p>\s*?){1,}\s*(?:<empty-line ?/>)?\s*)(?:<epigraph>\s*(?:<empty-line ?/>)?\s*<p>)(?'author'<(?'tag1'strong|emphasis)>[^<]+</\k'tag1'>)(?: ?</p>\s*(?:<empty-line ?/>)?\s*</epigraph>\s*)(?=</epigraph>)",
								"${p}<text-author>${author}</text-author>", RegexOptions.IgnoreCase | RegexOptions.Multiline
							);
						} catch ( RegexMatchTimeoutException /*ex*/ ) {}
						
					} else if ( tagPair.StartTagCount == 3 ) {
						// обработка Эпиграфа с вложенным Эпиграфом и Эпиграфом вместо Автора эпиграфа: <epigraph><epigraph><p><strong>Текст</strong></p><p>Текст</p></epigraph><epigraph><emphasis><p>Автор</p></emphasis></epigraph></epigraph>
						try {
							NewTag = Regex.Replace(
								NewTag, @"(?'epigraph'<epigraph>\s*?)<epigraph>\s*?(?'texts'(<p>\s*?(?:<(?'tag'strong|emphasis)\b>)?[^<]+?(?:</\k'tag'>)?\s*?</p>\s*?){1,})\s*?</epigraph>\s*?<epigraph>\s*?<(?'tag1'emphasis|strong)\b>(\s*?<p>)(?'author'[^<]+?)(</p>\s*?)</\k'tag1'>\s*?</epigraph>\s*?(?'_epigraph'</epigraph>)",
								"${epigraph}${texts}<text-author><${tag1}>${author}</${tag1}></text-author>\n${_epigraph}", RegexOptions.IgnoreCase | RegexOptions.Multiline
							);
						} catch ( RegexMatchTimeoutException /*ex*/ ) {}
					}
				}
				Index = XmlText.IndexOf( tagPair.PairTag, tagPair.StartTagPosition ) + NewTag.Length;
				XmlText = XmlText.Substring( 0, tagPair.StartTagPosition ) /* ДО обрабатываемого текста */
					+ NewTag
					+ XmlText.Substring( tagPair.EndTagPosition ); /* ПОСЛЕ обрабатываемого текста */
			}
		}
		
	}
}
