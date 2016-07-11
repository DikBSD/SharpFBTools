/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 22.05.2014
 * Time: 14:38
 * 
 * License: GPL 2.1
 */
using System;

namespace Core.FileManager
{
	/// <summary>
	/// ISortOptions: интерфейс для всех сортировщиков обоих режимов (беспрерывная сортировка и возобновление сортировки)
	/// </summary>
	public interface ISortOptions
	{
		// =====================================================================================================
		//						Основные настройки для всех режимов Сортировки
		// =====================================================================================================
		#region Обработка файлов
		// Обрабатывать подкаталоги
		bool ScanSubDirs { get; set; }
		// Архивировать в zip
		bool ToZip { get; set; }
		// Сохранять оригиналы
		bool NotDelOriginalFiles { get; set; }
		// Папка исходных fb2-файлов
		string SourceDir { get; set; }
		// Папка-приемник fb2-файлов
		string TargetDir { get; set; }
		// Шаблон подстановки
		string Template { get; set; }
		#endregion
		
		#region Обработка имен файлов
		// Регистр имени файла
		bool RegisterAsIs { get; set; }
		bool RegisterLower { get; set; }
		bool RegisterUpper { get; set; }
		bool RegisterAsSentence { get; set; }
		// транслитерация
		bool Translit	 { get; set; }
		// 'Строги' имена файлов
		bool Strict { get; set; }
		// Обработка пробелов в именах файлов
		int Space { get; set; }
		// что делать с уже имеющимися файлами в папке-приемнике
		int FileExistMode { get; set; }
		
		// Сортировка файлов
		bool SortTypeAllFB2 { get; set; }
		bool SortTypeOnlyValidFB2 { get; set; }
		
		// Раскладка файлов по папкам
		bool AuthorsToDirsAuthorOne { get; set; }
		bool AuthorsToDirsAuthorAll { get; set; }
		bool GenresToDirsGenreOne { get; set; }
		bool GenresToDirsGenreAll { get; set; }
		bool GenresTypeGenreSchema { get; set; }
		bool GenresTypeGenreText { get; set; }
		#endregion
	}
}
