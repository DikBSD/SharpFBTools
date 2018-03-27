/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 17.11.2015
 * Время: 12:18
 * 
 * License: GPL 2.1
 */
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Core.AutoCorrector
{
	/// <summary>
	/// Обработка Цитат
	/// </summary>
	public class CiteCorrector
	{
		private const string _MessageTitle = "Автокорректор";
		
		private const string _startTag = "<cite>";
		private const string _endTag = "</cite>";
		private string _xmlText = string.Empty;
		
		private bool _preProcess = false;
		private bool _postProcess = false;
		
		/// <summary>
		/// Конструктор класса CiteCorrector
		/// </summary>
		/// <param name="xmlText">Строка для корректировки</param>
		/// <param name="preProcess">Удаление стартовых пробелов и перевода строки => всю книгу - в одну строку</param>
		/// <param name="postProcess">Вставка разрыва абзаца между смежными тегами</param>
		public CiteCorrector( ref string xmlText, bool preProcess, bool postProcess  )
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
			if ( _xmlText.IndexOf( _startTag, StringComparison.CurrentCulture ) == -1 )
				return _xmlText;
			
			// преобработка (удаление стартовых пробелов ДО тегов и удаление завершающих пробелов ПОСЛЕ тегов и символов переноса строк)
			if ( _preProcess )
				_xmlText = FB2CleanCode.preProcessing( _xmlText );
			
			/**********************************
			 * Предварительная обработка cite *
			 **********************************/
			// Преобразование вложенных друг в друга тегов cite в Автора: <cite><cite><p>Иванов</p></cite></cite> => <cite><text-author><p>Иванов</p></text-author></cite>
			try {
				_xmlText = Regex.Replace(
					_xmlText, @"(?:(?:<(?'tag'cite)\b>\s*?){2})(?:<p>\s*?)(?'text'(?:<(?'tag1'strong|emphasis)\b>)?\s*?(?:[^<]+)(?:</\k'tag1'>)?)(?:\s*?</p>)(?:\s*?</\k'tag'>){2}",
					"<${tag}><text-author>${text}</text-author></${tag}>", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				if ( Settings.Settings.ShowDebugMessage ) {
					// Показывать сообщения об ошибках при падении работы алгоритмов
					MessageBox.Show(
						string.Format("CiteCorrector:\r\nПреобразование вложенных друг в друга тегов cite в Автора: <cite><cite><p>Иванов</p></cite></cite> => <cite><text-author><p>Иванов</p></text-author></cite>.\r\nОшибка:\r\n{0}", ex.Message), _MessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Error
					);
				}
			}
			
			// перестановка местами Текста Цитаты и ее автора: <cite><text-author>Автор</text-author><p>Цитата</p></cite> => <cite><p>Цитата</p><text-author>Автор</text-author></cite>
			try {
				_xmlText = Regex.Replace(
					_xmlText, @"(?<=<cite>)\s*?(?'author'<text-author>\s*?(?:<(?'tag'strong|emphasis)\b>)?[^<]+?(?:</\k'tag'>)?\s*?</text-author>)\s*?(?'texts'(?:<p>\s*?(?:<(?'tagp'strong|emphasis)\b>)?[^<]+?(?:</\k'tagp'>)?\s*?</p>\s*?){1,})\s*?(?=</cite>)",
					"${texts}${author}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				if ( Settings.Settings.ShowDebugMessage ) {
					// Показывать сообщения об ошибках при падении работы алгоритмов
					MessageBox.Show(
						string.Format("CiteCorrector:\r\nПерестановка местами Текста Цитаты и ее автора: <cite><text-author>Автор</text-author><p>Цитата</p></cite> => <cite><p>Цитата</p><text-author>Автор</text-author></cite>.\r\nОшибка:\r\n{0}", ex.Message), _MessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Error
					);
				}
			}
			
			// Удаление <empty-line /> между </text-author> и </cite>: </text-author><empty-line /></cite> => </text-author></cite>
			try {
				_xmlText = Regex.Replace(
					_xmlText, @"(?<=</text-author>)\s*<empty-line */>\s*(?=</cite>)",
					"", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				if ( Settings.Settings.ShowDebugMessage ) {
					// Показывать сообщения об ошибках при падении работы алгоритмов
					MessageBox.Show(
						string.Format("CiteCorrector:\r\nУдаление <empty-line /> между </text-author> и </cite>: </text-author><empty-line /></cite> => </text-author></cite>.\r\nОшибка:\r\n{0}", ex.Message), _MessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Error
					);
				}
			}
			
			// обработка найденных парных тэгов
			IWorker worker = new CiteCorrectorWorker();
			TagWorker tagWorker = new TagWorker( ref _xmlText, _startTag, _endTag, ref worker );
			_xmlText = tagWorker.Work();
			
			// постобработка (разбиение на теги (смежные теги) )
			if ( _postProcess )
				_xmlText = FB2CleanCode.postProcessing( _xmlText );
			
			return _xmlText;
		}
		
		public class CiteCorrectorWorker : IWorker
		{
			/// <summary>
			/// Обработчик найденных парных тегов Цитаты
			/// </summary>
			/// <param name="tagPair">Экземпляр класса поиска парных тегов с вложенными тегами любой сложности вложения</param>
			/// <param name="XmlText">Текст строки для корректировки в xml представлении</param>
			/// <param name="Index">Индекс начала поиска открывающего тэга</param>
			public void DoWork( ref TagPair tagPair, ref string XmlText, ref int Index ) {
				if ( string.IsNullOrWhiteSpace( tagPair.PairTag ) )
					return;
				
				string NewTag = tagPair.PairTag;
				// Обрамление картинки тегами <p> ... </p>
				try {
					NewTag = Regex.Replace(
						NewTag, "(?<!<p>)(?'img'<image[^/]+?(?:\"[^\"]*\"|'[^']*')?/>)(?!</p>)",
						"<p>${img}</p>", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException /*ex*/ ) {}
				catch ( Exception ex ) {
					if ( Settings.Settings.ShowDebugMessage ) {
						// Показывать сообщения об ошибках при падении работы алгоритмов
						MessageBox.Show(
							string.Format("CiteCorrector:\r\nОбрамление картинки тегами <p> ... </p>.\r\nОшибка:\r\n{0}", ex.Message), _MessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Error
						);
					}
				}
				
				if ( tagPair.StartTagCount == 2 ) {
					// Удаление <cite> вокруг <text-author> в Цитате: <cite><p>Текст</p><p>Текст</p><cite><text-author>Автор Цитаты</text-author></cite></cite> => <cite><p>Текст</p><p>Текст</p><text-author>Автор Цитаты</text-author></cite>
					try {
						NewTag = Regex.Replace(
							NewTag, @"(?<=<cite>)\s*?(?'p'(?:<p>\s*?(?:<(?'tag'strong|emphasis)\b>)?[^<]+?(?:</\k'tag'>)?\s*?</p>\s*?){1,})\s*?<cite>\s*?(?'ta'<text-author>\s*?(<(?'tag1'emphasis|strong)\b>)?(?'author'[^<]+)(</\k'tag1'>)?\s*?</text-author>)\s*?</cite>\s*?(?=</cite>)",
							"${p}${ta}", RegexOptions.IgnoreCase | RegexOptions.Multiline
						);
					} catch ( RegexMatchTimeoutException /*ex*/ ) {}
					catch ( Exception ex ) {
						if ( Settings.Settings.ShowDebugMessage ) {
							// Показывать сообщения об ошибках при падении работы алгоритмов
							MessageBox.Show(
								string.Format("CiteCorrector:\r\nУдаление <cite> вокруг <text-author> в Цитате: <cite><p>Текст</p><p>Текст</p><cite><text-author>Автор Цитаты</text-author></cite></cite> => <cite><p>Текст</p><p>Текст</p><text-author>Автор Цитаты</text-author></cite>.\r\nОшибка:\r\n{0}", ex.Message), _MessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Error
							);
						}
					}
				}
				
				Index = XmlText.IndexOf( tagPair.PairTag, tagPair.StartTagPosition, StringComparison.CurrentCulture ) + NewTag.Length;
				XmlText = XmlText.Substring( 0, tagPair.StartTagPosition ) /* ДО обрабатываемого текста */
					+ NewTag
					+ XmlText.Substring( tagPair.EndTagPosition ); /* ПОСЛЕ обрабатываемого текста */
			}
		}
		
	}
}
