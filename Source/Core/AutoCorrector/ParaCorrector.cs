/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 05.11.2015
 * Время: 12:11
 * 
 * License: GPL 2.1
 */
using System;
using System.Text.RegularExpressions;

namespace Core.AutoCorrector
{
	/// <summary>
	/// Обработка абзацев
	/// </summary>
	public class ParaCorrector
	{
		private const string _startTag = "<p>";
		private const string _endTag = "</p>";
		private string _xmlText = string.Empty;
		
		private bool _preProcess = false;
		private bool _postProcess = false;
		
		/// <summary>
		/// Конструктор класса ParaCorrector
		/// </summary>
		/// <param name="xmlText">Строка для корректировки</param>
		/// <param name="preProcess">Удаление стартовых пробелов и перевода строки => всю книгу - в одну строку</param>
		/// <param name="postProcess">Вставка разрыва абзаца между смежными тегами</param>
		public ParaCorrector( ref string xmlText, bool preProcess, bool postProcess )
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
			
			// незавершенный тег <p>: <p текст => <p> текст
			_xmlText = Regex.Replace(
				_xmlText, @"(?:\s*?)(?'p'<p)(?=\s+?[^i/>])",
				"${p}>", RegexOptions.Multiline // регист не игнорировать!!!
			);
			
			// восстановление пропущенных </p>: <p><image xlink:href="#cover.jpg"/><p>Текст</p> => <p><image xlink:href="#cover.jpg"/></p>\n<p>Текст</p>
			_xmlText = Regex.Replace(
				_xmlText, @"(?'inline_img'<p>\s*?<image [^<]+?/>)(?=\s*?<p>)",
				"${inline_img}</p>", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// восстановление пропущенных <p>: </p><image xlink:href="#cover.jpg"/></p> => </p>\n<p><image xlink:href="#cover.jpg"/></p>
			_xmlText = Regex.Replace(
				_xmlText, @"(?'_p'</p>)(?:\s*?)(?'img'<image [^<]+?/>)(?=\s*?</p>)",
				"${_p}<p>${img}", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			
			// восстановление пропущенных <p>: </p>Любой текст с тегами</p> => </p><p>Любой текст с тегами</p>
			_xmlText = Regex.Replace(
				_xmlText, @"(?<=</p>)(?: *?)(?'text'(?:(?!(?:(?:<p>)|(?:</[^p])|(?:<empty-line ?/>))).?){1,} *?)(?=</p>)",
				"<p>${text}", RegexOptions.IgnoreCase | RegexOptions.Multiline  | RegexOptions.Singleline
			);
			// восстановление пропущенных </p>: <p>Любой текст с тегами<p> => <p>Любой текст с тегами</p><p>
			_xmlText = Regex.Replace(
				_xmlText, @"(?<=<p>)(?: *?)(?'text'(?:(?!(?:</p>)).?){1,} *?)(?=<p>)",
				"${text}</p>", RegexOptions.IgnoreCase | RegexOptions.Multiline  | RegexOptions.Singleline
			);

			// ********************************************************************************************************
			// восстановление пропущенных <p>: </p> Текст => </p>\n<p> Текст
			_xmlText = Regex.Replace(
				_xmlText, "(?'_p'</p>)\\s*?(?'text'[\\−\\—\\–\\-\\*\\+\\d\\w`'\"«»„“”‘’\\:;\\.,!_=\\?\\(\\)\\{\\}\\[\\]@#$%№\\^~])",
				"${_p}\n<p>${text}", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// восстановление пропущенных <p>: </p><strong>Текст</strong> => </p><p><strong>Текст</strong>
			_xmlText = Regex.Replace(
				_xmlText, @"(?'_p'</p>)(?:\s*?)(?'tag_s'<(?'tag'strong|emphasis|strikethrough|sub|sup|code)\b>\s*?)(?'text'.+?\s*?)(?=</\k'tag'>)",
				"${_p}\n<p>${tag_s}${text}", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			
			// восстановление пропущенных </p>: Текст <p> => Текст </p>\n<p>
			_xmlText = Regex.Replace(
				_xmlText, "(?'text'[\\−\\—\\–\\-\\*\\+\\d\\w`'\"«»„“”‘’\\:;\\.,!_=\\?\\(\\)\\{\\}\\[\\]@#$%№\\^~])\\s*?(?'p'<p>)(?!\\s*?</[^<]+?>)",
				"${text}</p>\n${p}", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// восстановление пропущенных </p>: <p><strong>Текст</strong><p> => <p><strong>Текст</strong></p><p>
			_xmlText = Regex.Replace(
				_xmlText, @"(?'p'<p>)(?:\s*?)(?'tag_s'<(?'tag'strong|emphasis|strikethrough|sub|sup|code)\b>\s*?)(?'text'.+?\s*?)(?'_tag'</\k'tag'>)(?=\s*?<p>)",
				"${p}${tag_s}${text}${_tag}</p>\n", RegexOptions.IgnoreCase | RegexOptions.Multiline
			);
			// ********************************************************************************************************
			
			//TODO
			// удаление "одиночных" тегов <strong> (</strong>) и т.д. из абзаца <p> ... </p>

			// постобработка (разбиение на теги (смежные теги) )
			if ( _postProcess )
				_xmlText = FB2CleanCode.postProcessing( _xmlText );
			
			return _xmlText;
		}

	}
}
