/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 23.05.2014
 * Time: 9:23
 * 
 * License: GPL 2.1
 */
using System;

namespace Core.Common
{
	/// <summary>
	/// Интерфейс названий каталогов/имен файлов, для случая, когда нет соответствующих тэгов книг
	/// </summary>
	public interface IFB2TagsNotExsist
	{
		// =====================================================================================================
		//										Папки шаблонного тэга без данны
		// =====================================================================================================
		#region Описание Книги
		// _Нестандартные Жанры
		string BookInfoNoGenreGroup { get; set; }
		// Жанра Нет
		string BookInfoNoGenre { get; set; }
		// Языка Книги Нет
		string BookInfoNoLang { get; set; }
		// Имени Автора Нет
		string BookInfoNoFirstName { get; set; }
		// Отчества Автора Нет
		string BookInfoNoMiddleName { get; set; }
		// Фамилия Автора Нет
		string BookInfoNoLastName { get; set; }
		// Ника Автора Нет
		string BookInfoNoNickName { get; set; }
		// Названия Книги Нет
		string BookInfoNoBookTitle { get; set; }
		// Серии Нет
		string BookInfoNoSequence { get; set; }
		// Номера Серии Нет
		string BookInfoNoNSequence { get; set; }
		// Даты (Текст) Нет
		string BookInfoNoDateText { get; set; }
		// Даты (Значение) Нет
		string BookInfoNoDateValue { get; set; }
		#endregion
		
		#region Издательство
		string PublishInfoNoPublisher { get; set; }
		// Года издания Нет
		string PublishInfoNoYear { get; set; }
		// Города Издания Нет
		string PublishInfoNoCity { get; set; }
		#endregion
		
		#region Данные о создателе fb2 файла
		// Имени fb2-создателя Нет
		string FB2InfoNoFB2FirstName { get; set; }
		// Отчества fb2-создателя Нет
		string FB2InfoNoFB2MiddleName { get; set; }
		// Фамилия fb2-создателя Нет
		string FB2InfoNoFB2LastName { get; set; }
		// Ника fb2-создателя Нет
		string FB2InfoNoFB2NickName { get; set; }
		#endregion
	}
}
