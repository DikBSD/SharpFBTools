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
using System.Windows.Forms;

using Core.Common;

namespace Core.AutoCorrector
{
	/// <summary>
	/// Обработка Poem
	/// </summary>
	public class PoemCorrector
	{
		private const string _MessageTitle = "Автокорректор";
		private readonly string _FilePath = string.Empty; // Путь к обрабатываемому файлу
		
		private const string _startTag = "<poem>";
		private const string _endTag = "</poem>";
		private string _xmlText = string.Empty;
		
		private bool _preProcess = false;
		private bool _postProcess = false;
		
		/// <summary>
		/// Конструктор класса PoemCorrector
		/// </summary>
		/// <param name="FilePath">Путь к обрабатываемому файлу</param>
		/// <param name="xmlText">Строка для корректировки</param>
		/// <param name="preProcess">Удаление стартовых пробелов и перевода строки => всю книгу - в одну строку</param>
		/// <param name="postProcess">Вставка разрыва абзаца между смежными тегами</param>
		public PoemCorrector( string FilePath, ref string xmlText, bool preProcess, bool postProcess )
		{
			_FilePath = FilePath;
			_xmlText = xmlText;
			_preProcess = preProcess;
			_postProcess = postProcess;
		}
		
		/// <summary>
		/// Корректировка парных тегов Стихов
		/// </summary>
		/// <returns>Откорректированную строку типа string </returns>
		public string correct() {
			if ( _xmlText.IndexOf( _startTag, StringComparison.CurrentCulture ) == -1 )
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
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "PoemCorrector:\r\nУдаление структур <poem><stanza /></poem>."
				);
			}
			
			// Удаление структур <poem><stanza><empty-line /></stanza></poem>
			try {
				_xmlText = Regex.Replace(
					_xmlText, @"<poem>\s*?<stanza>\s*?<empty-line ?/>\s*?</stanza>\s*?</poem>",
					"", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "PoemCorrector:\r\nУдаление структур <poem><stanza><empty-line /></stanza></poem>."
				);
			}

			// вставка <text-author> внутрь <poem> </poem>
			try {
				_xmlText = Regex.Replace(
					_xmlText, @"(?'_poem'</poem>)(?'ws'\s*)(?'textauthor'<text-author>\s*.+?\s*</text-author>)",
					"${textauthor}${ws}${_poem}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "PoemCorrector:\r\nВставка <text-author> внутрь <poem> </poem>."
				);
			}
			
			// обработка найденных парных тэгов
			IWorker worker = new PoemCorrectorWorker();
			TagWorker tagWorker = new TagWorker( _FilePath, ref _xmlText, _startTag, _endTag, ref worker );
			_xmlText = tagWorker.Work();
			
			// постобработка (разбиение на теги (смежные теги) )
			if ( _postProcess )
				_xmlText = FB2CleanCode.postProcessing( _xmlText );
			
			return _xmlText;
		}
		
		public class PoemCorrectorWorker : IWorker
		{
			/// <summary>
			/// Обработчик найденных парных тегов poem
			/// </summary>
			/// <param name="tagPair">Экземпляр класса поиска парных тегов с вложенными тегами любой сложности вложения</param>
			/// <param name="XmlText">Текст строки для корректировки в xml представлении</param>
			/// <param name="Index">Индекс начала поиска открывающего тэга</param>
			public void DoWork( ref TagPair tagPair, ref string XmlText, ref int Index ) {
				if ( string.IsNullOrWhiteSpace( tagPair.PairTag ) )
					return;
				
				string NewTag = tagPair.PairTag;
				
				// Преобразование <poem><p>...</p><p>...</p></poem> в <poem><stanza><v>...</v><v>...</v></stanza></poem>
				try {
					Match m = Regex.Match(
						NewTag, "(?:<poem>)\\s*?(?:(?:<p>\\s*?(?'text'(?:(?:[=~\"'\\`«»\\\\^\\$-\\+\\-\\—\\–.,~!?:;&#@№%\\*\\(\\)\\{\\}\\[\\]/…\\w\\d]\\s*?){1,}))</p>){1,}\\s*?){1,}\\s*?(?:</poem>)",
						RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
					if ( m.Success ) {
						string sSource = m.Value;
						string sResult = m.Value;
						sResult = sResult.Replace("<p>", "<v>").Replace("</p>", "</v>");
						sResult = sResult.Replace("<poem>", "<poem><stanza>").Replace("</poem>", "</stanza></poem>");
						if ( sResult != sSource )
							NewTag = NewTag.Replace( sSource, sResult );
					}
				} catch ( RegexMatchTimeoutException /*ex*/ ) {}
				catch ( Exception ex ) {
					Debug.DebugMessage(
						tagPair.FilePath, ex, "PoemCorrector:\r\nПреобразование <poem><p>...</p><p>...</p></poem> в <poem><stanza><v>...</v><v>...</v></stanza></poem>."
					);
				}
				
				Index = XmlText.IndexOf( tagPair.PairTag, tagPair.StartTagPosition, StringComparison.CurrentCulture ) + NewTag.Length;
				XmlText = XmlText.Substring( 0, tagPair.StartTagPosition ) /* ДО обрабатываемого текста */
					+ NewTag
					+ XmlText.Substring( tagPair.EndTagPosition ); /* ПОСЛЕ обрабатываемого текста */
			}
		}
		
	}
}
