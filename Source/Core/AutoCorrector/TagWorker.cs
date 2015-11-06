/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 05.11.2015
 * Время: 13:42
 * 
 * License: GPL 2.1
 */
using System;
using System.Collections.Generic;

namespace Core.AutoCorrector
{
	/// <summary>
	/// Обработчик для разных видов парных тэгов, вызывающий конкретный обработчик конкретных тэгов (IWorker)
	/// </summary>
	public class TagWorker
	{
		private string _xmlText = string.Empty;
		private readonly string _startTag = string.Empty;
		private readonly string _endTag = string.Empty;
		private readonly IWorker _worker = null;
		
		/// <summary>
		/// Конструктор класса TagWorker
		/// </summary>
		/// <param name="XmlText">Текст строки для корректировки в xml представлении</param>
		/// <param name="StartTag">Открывающий тэг</param>
		/// <param name="EndTag">Закрывающий тэг</param>
		/// <param name="Worker">Экземпляр обработчика для парных тегов конкретного обработчика конкретных парных тэгов</param>
		public TagWorker( ref string XmlText, string StartTag, string EndTag, ref IWorker Worker )
		{
			_xmlText = XmlText;
			_startTag = StartTag;
			_endTag = EndTag;
			_worker = Worker;
		}
		
		/// <summary>
		/// Метод-обработчик парных тэгов, вызывающий конкретный обработчик для конкретных тэгов (IWorker Worker)
		/// </summary>
		/// <returns>Откорректированную строку типа string </returns>
		public string Work() {
			TagPair tagPair = new TagPair();
			List<int> PairTagIndexes = new List<int>();
			int indexOfTag = _xmlText.IndexOf( _startTag );
			do {
				tagPair.clear();
				++tagPair.StartTagCount;
				tagPair.StartTagPosition = indexOfTag;
				
				if ( !PairTagIndexes.Contains( tagPair.StartTagPosition ) )
					PairTagIndexes.Add( tagPair.StartTagPosition );
				else
					break;

				tagPair.getPairTextAndTags( _xmlText, _startTag, _endTag );
				int Index = 0;
				_worker.DoWork( ref tagPair, ref _xmlText, ref Index );
				if ( tagPair.EndTagPosition < _xmlText.Length )
					indexOfTag = _xmlText.IndexOf( _startTag, Index );
				else
					indexOfTag = -1;
			} while( indexOfTag != -1 );
			
			return _xmlText;
		}
	}
}
