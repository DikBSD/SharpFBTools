/*
 * Сделано в SharpDevelop.
 * Пользователь: DikBSD
 * Дата: 27.08.2014
 * Время: 14:19
 * 
 */
 
using System;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Core.Misc
{
	/// <summary>
	/// класс для работы с перечислениями
	/// </summary>
	public class Enums
	{
		/// <summary>
		/// Вид TitleInfo: TitleInfo или SourceTitleInfo  
		/// </summary>
		public enum TitleInfoEnum {
			TitleInfo,		
			SourceTitleInfo
		}
		
		/// <summary>
		/// Обрабатываемый author: Автор книги, Автор fb2-файла или Переводчик  
		/// </summary>
		public enum AuthorEnum {
			AuthorOfBook,		
			AuthorOfFB2,
			Translator
		}
		
		/// <summary>
		/// Обрабатываемый sequence: Серия Электронной или Бумажной книги  
		/// </summary>
		public enum SequenceEnum {
			Ebook,		
			PaperBook
		}
		
		/// <summary>
		/// Варианты завершения групповой обработки
		/// </summary>
		public enum EndWorkModeEnum {
			Done,		// Благополучное завершение
			Cancelled,	// Прерывание
			Error,		// Ошибка
		}
		
		/// <summary>
		/// Номера колонок контрола просмотра групп одинаковых книг
		/// </summary>
		public enum ResultViewCollumn {
			Path			= 0,	// Путь к книге
			BookTitle		= 1,	// Название книги
			Authors			= 2,	// Автор(ы)
			Genres			= 3,	// Жанр(ы)
			BookID			= 4,	// ID книги
			Version			= 5,	// Версия файла
			Encoding		= 6,	// Кодировка
			Validate		= 7,	// Валидность
			FileLength		= 8, 	// Размер файла
			CreationTime	= 9, 	// Время создания файла
			LastWriteTime	= 10, 	// Последнее изменение файла
		}
		
		/// <summary>
		/// Варианты сравнения книг в группах
		/// </summary>
		public enum CompareMode {
			Version,		// Версия файла
			Encoding,		// Кодировка
			Validate,		// Валидность
			FileLength, 	// Размер файла
			CreationTime, 	// Время создания файла
			LastWriteTime, 	// Последнее изменение файла
		}
		
		/// <summary>
		/// Варианты сравнения книг: в текущей группе или во всех группах
		/// </summary>
		public enum GroupAnalyzeMode {
			Group,		// В текущей Группе
			AllGroup,	// Во всех Группах
		}
		
		/// <summary>
		/// режимы сравнения книг
		/// </summary>
		public enum SearchCompareMode {
			Md5					= 0, // 0. Абсолютно одинаковые книги (md5)
			BookID				= 1, // 1. Одинаковый Id Книги (копии и/или разные версии правки одной и той же книги)
			BookTitle			= 2, // 2. Название Книги (могут быть найдены и разные книги разных Авторов, но с одинаковым Названием)
			AuthorAndTitle		= 3, // 3. Автор(ы) и Название Книги (одна и та же книга, сделанная разными людьми - разные Id, но Автор и Название - одинаковые)
		}
		
		/// <summary>
		/// Режимы обработки Дубликатором копий книг
		/// </summary>
		public enum DuplWorkMode {
			SaveFB2CopiesList,	// Сохранение списка копий книг
			LoadFB2CopiesList,	// Загрузка списка копий книг
			CopyCheckedBooks,	// Копирование помеченных книг в папку
			MoveCheckedBooks,	// Перемещение помеченных книг в папку
			DeleteCheckedBooks,	// Удаление помеченных книг
		}
		
		/// <summary>
		/// Режим сортировки книг - по числу Авторов и Жанров
		/// </summary>
//		public enum SortModeType {
//			_1Genre1Author,			// по первому Жанру и первому Автору Книги
//			_1GenreAllAuthor, 		// по первому Жанру и всем Авторам Книги
//			_AllGenre1Author,		// по всем Жанрам и первому Автору Книги
//			_AllGenreAllAuthor, 	// по всем Жанрам и всем Авторам Книги
//		}
		public Enums()
		{
		}
		
	}
}