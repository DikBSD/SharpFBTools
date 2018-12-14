/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 05.11.2015
 * Время: 13:28
 * 
 * License: GPL 2.1
 */
using System;

namespace Core.AutoCorrector
{
	/// <summary>
	/// Интерфейс обработчика для парных тегов
	/// </summary>
	/// <param name="tagPair">Экземпляр класса поиска парных тегов с вложенными тегами любой сложности вложения</param>
	/// <param name="XmlText">Текст строки для корректировки в xml представлении</param>
	/// <param name="Index">Индекс начала поиска открывающего тэга</param>
	public interface IWorker
	{
		/// <summary>
		/// Обработчик найденных парных тегов poem
		/// </summary>
		/// <param name="tagPair">Экземпляр класса поиска парных тегов с вложенными тегами любой сложности вложения</param>
		/// <param name="XmlText">Текст строки для корректировки в xml представлении</param>
		/// <param name="Index">Индекс начала поиска открывающего тэга</param>
		void DoWork( ref TagPair tagPair, ref string XmlText, ref int Index );
	}
}
