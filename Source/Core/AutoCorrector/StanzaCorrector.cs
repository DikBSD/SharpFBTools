/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 18.11.2015
 * Время: 11:06
 * 
 * License: GPL 2.1
 */
using System;
using System.Text.RegularExpressions;

namespace Core.AutoCorrector
{
	/// <summary>
	/// Обработка Stanza
	/// </summary>
	public class StanzaCorrector
	{
		private const string _startTag = "<stanza>";
		private const string _endTag = "</stanza>";
		private string _xmlText = string.Empty;
		
		private bool _preProcess = false;
		private bool _postProcess = false;
		
		/// <summary>
		/// Конструктор класса StanzaCorrector
		/// </summary>
		/// <param name="xmlText">Строка для корректировки</param>
		/// <param name="preProcess">Удаление стартовых пробелов и перевода строки => всю книгу - в одну строку</param>
		/// <param name="postProcess">Вставка разрыва абзаца между смежными тегами</param>
		public StanzaCorrector( ref string xmlText, bool preProcess, bool postProcess )
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
			if ( _xmlText.IndexOf( _startTag, StringComparison.CurrentCulture ) == -1 )
				return _xmlText;
			
			// преобработка (удаление стартовых пробелов ДО тегов и удаление завершающих пробелов ПОСЛЕ тегов и символов переноса строк)
			if ( _preProcess )
				_xmlText = FB2CleanCode.preProcessing( _xmlText );
			
			/************************************
			 * Предварительная обработка stanza *
			 ************************************/
			// вставка <v /> после </subtitle> внутри <stanza></stanza>: <stanza><subtitle>Текст</subtitle></stanza> => <stanza><subtitle>Текст</subtitle><v/></stanza>
			try {
				_xmlText = Regex.Replace(
					_xmlText, @"(?<=<stanza>)\s*?(?'subtitle'<subtitle>.+?</subtitle>)\s*?(?=</stanza>)",
					"${subtitle}<v/>", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			
			// обработка найденных парных тэгов
			IWorker worker = new StanzaCorrectorWorker();
			TagWorker tagWorker = new TagWorker( ref _xmlText, _startTag, _endTag, ref worker );
			_xmlText = tagWorker.Work();
			
			// постобработка (разбиение на теги (смежные теги) )
			if ( _postProcess )
				_xmlText = FB2CleanCode.postProcessing( _xmlText );
			
			return _xmlText;
		}
		
		public class StanzaCorrectorWorker : IWorker
		{
			/// <summary>
			/// Обработчик найденных парных тегов Стихов
			/// </summary>
			/// <param name="tagPair">Экземпляр класса поиска парных тегов с вложенными тегами любой сложности вложения</param>
			/// <param name="XmlText">Текст строки для корректировки в xml представлении</param>
			/// <param name="Index">Индекс начала поиска открывающего тэга</param>
			public void DoWork( ref TagPair tagPair, ref string XmlText, ref int Index ) {
				if ( string.IsNullOrWhiteSpace( tagPair.PairTag ) )
					return;
				
				string NewTag = tagPair.PairTag;
				// замена тегов <p>, </p> на <v>, </v> в стихах
				NewTag = NewTag.Replace("<p>", "<v>").Replace("</p>", "</v>");
				// преобразование тегов <v> в <title> обратно в <p>
				try {
					Match m = Regex.Match(
						NewTag, @"(?:<title>\s*?.+?\s*?</title>)",
						RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
					if ( m.Success ) {
						string sSource = m.Value;
						string sResult = m.Value;
						sResult = sResult.Replace("<v>", "<p>").Replace("</v>", "</p>");
						if ( sResult != sSource )
							NewTag = NewTag.Replace( sSource, sResult );
					}
				} catch ( RegexMatchTimeoutException /*ex*/ ) {}
				
				
				// обработка <empty-line /> между строфами: <v>Строфа</v><v>Строфа</v><empty-line /><v>Строфа</v><v>Строфа</v> => <v>Строфа</v><v>Строфа</v></stanza><stanza><v>Строфа</v><v>Строфа</v>
				try {
					NewTag = Regex.Replace(
						NewTag, @"(?'v'<v>.+?</v>)\s*?<empty-line ?/>\s*?(?'v1'<v>.+?</v>)",
						"${v}</stanza><stanza>${v1}", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException /*ex*/ ) {}
				
				// обработка строф с эпиграфом: <poem><stanza><epigraph><v><v>Строфа</v></v></epigraph></stanza></poem> => <poem><stanza><v>Строфа</v></stanza></poem>
				try {
					NewTag = Regex.Replace(
						NewTag, @"<epigraph>\s*?<v>\s*?(?'v'<v>[^<]+?</v>)\s*?</v>\s*?</epigraph>",
						"${v}", RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
				} catch ( RegexMatchTimeoutException /*ex*/ ) {}
				
				Index = XmlText.IndexOf( tagPair.PairTag, tagPair.StartTagPosition, StringComparison.CurrentCulture ) + NewTag.Length;
				XmlText = XmlText.Substring( 0, tagPair.StartTagPosition ) /* ДО обрабатываемого текста */
					+ NewTag
					+ XmlText.Substring( tagPair.EndTagPosition ); /* ПОСЛЕ обрабатываемого текста */
			}
		}
		
	}
}
