/*
 * Сделано в SharpDevelop.
 * Пользователь: DikBSD
 * Дата: 27.08.2014
 * Время: 14:19
 * 
 */
 
using System;

namespace Core.Common
{
	/// <summary>
	/// класс для работы с перечислениями
	/// </summary>
	public class Enums
	{
		/// <summary>
		/// Вид TitleInfo: TitleInfo или SourceTitleInfo  
		/// </summary>
		public enum TitleInfoEnum : byte {
			TitleInfo,		
			SourceTitleInfo
		}
		
		/// <summary>
		/// Обрабатываемый author: Автор книги, Автор fb2-файла или Переводчик  
		/// </summary>
		public enum AuthorEnum : byte {
			AuthorOfBook,		
			AuthorOfFB2,
			Translator
		}
		
		/// <summary>
		/// Обрабатываемый sequence: Серия Электронной или Бумажной книги  
		/// </summary>
		public enum SequenceEnum : byte {
			Ebook,		
			PaperBook
		}
		
		/// <summary>
		/// Варианты завершения групповой обработки
		/// </summary>
		public enum EndWorkModeEnum : byte {
			Done,		// Благополучное завершение
			Cancelled,	// Прерывание
			Error,		// Ошибка
		}
		
		/// <summary>
		/// Номера колонок контрола просмотра групп одинаковых книг Корректора и Сортировщика
		/// </summary>
		public enum ResultViewCollumn : byte {
			Path			= 0,	// Путь к книге
			BookTitle		= 1,	// Название книги
			Authors			= 2,	// Автор(ы)
			Genres			= 3,	// Жанр(ы)
			Sequences		= 4,	// Серия(и)
			Lang			= 5,	// Язык
			BookID			= 6,	// ID книги
			Version			= 7,	// Версия файла
			Encoding		= 8,	// Кодировка
			Validate		= 9,	// Валидность
			Format			= 10,	// Формат файла
			FileLength		= 11, 	// Размер файла
			CreationTime	= 12, 	// Время создания файла
			LastWriteTime	= 13, 	// Последнее изменение файла
		}
		
		/// <summary>
		/// Номера колонок контрола просмотра групп одинаковых книг для Дубликатора
		/// </summary>
		public enum ResultViewDupCollumn : byte {
			Path			= 0,	// Путь к книге
			BookTitle		= 1,	// Название книги
			Authors			= 2,	// Автор(ы)
			Genres			= 3,	// Жанр(ы)
			BookLang		= 4,	// Язык книги
			BookID			= 5,	// ID книги
			Version			= 6,	// Версия файла
			Encoding		= 7,	// Кодировка
			Validate		= 8,	// Валидность
			FileLength		= 9, 	// Размер файла
			CreationTime	= 10, 	// Время создания файла
			LastWriteTime	= 11, 	// Последнее изменение файла
		}
		
		/// <summary>
		/// Варианты сравнения книг в группах Дубликатора
		/// </summary>
		public enum CompareMode : byte {
			Version,		// Версия файла
			Encoding,		// Кодировка
			Validate,		// Валидность
			FileLength, 	// Размер файла
			CreationTime, 	// Время создания файла
			LastWriteTime, 	// Последнее изменение файла
		}
		
		/// <summary>
		/// Варианты сравнения книг: в текущей группе или во всех группах Дубликатора
		/// </summary>
		public enum GroupAnalyzeMode : byte {
			Group,		// В текущей Группе
			AllGroup,	// Во всех Группах
		}
		
		/// <summary>
		/// режимы сравнения книг Дубликатора
		/// </summary>
		public enum SearchCompareMode : byte {
			Md5					= 0, // 0. Абсолютно одинаковые книги (md5)
			BookID				= 1, // 1. Одинаковый Id Книги (копии и/или разные версии правки одной и той же книги)
			BookTitle			= 2, // 2. Название Книги (могут быть найдены и разные книги разных Авторов, но с одинаковым Названием)
			AuthorAndTitle		= 3, // 3. Автор(ы) и Название Книги (одна и та же книга, сделанная разными людьми - разные Id, но Автор и Название - одинаковые)
		}
		
		/// <summary>
		/// Номера колонок контрола отображения информации по файлам и Группам Дубликатора
		/// </summary>
		public enum FilesCountViewDupCollumn : byte {
			AllDirs				= 0,	// всего каталогов
			AllBooks			= 1,	// всего книг
			AllGroups			= 2,	// всего Групп одинаковых книг
			AllBoolsInAllGroups	= 3,	// всего всех книг во всех Группах 
		}
		
		/// <summary>
		/// Режимы обработки помеченных книг
		/// </summary>
		public enum BooksWorkMode : byte {
			SaveFB2List,		// Сохранение списка копий книг
			SaveWorkingFB2List,	// Сохранение текущего обрабатываемого списка копий книг без запроса пути
			LoadFB2List,		// Загрузка списка копий книг
			CopyCheckedBooks,	// Копирование помеченных книг в папку
			MoveCheckedBooks,	// Перемещение помеченных книг в папку
			DeleteCheckedBooks,	// Удаление помеченных книг
		}
		
		/// <summary>
		/// Режимы валидации книг
		/// </summary>
		public enum BooksValidateMode : byte {
			AllBooks,		// все книги
			CheckedBooks,	// помеченные книги
			SelectedBooks,	// выделенные книги
		}
		
		/// <summary>
		/// Режимы автокорректировки книг
		/// </summary>
		public enum BooksAutoCorrectMode : byte {
			CheckedBooks,		// помеченные книги
			SelectedBooks,		// выделенные книги
			BooksInGroup,		// В текущей Группе
			BooksInAllGroup,	// Во всех Группах
		}
		
		/// <summary>
		/// Режим сортировки книг - по числу Авторов и Жанров
		/// </summary>
//		public enum SortModeType : byte {
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