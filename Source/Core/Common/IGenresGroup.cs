/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 22.07.2015
 * Время: 8:40
 * 
 */
using System;

namespace Core.Common
{
	/// <summary>
	/// Интерфейс названий Групп Жанров
	/// </summary>
	public interface IGenresGroup
	{
		// Фантастика, Фэнтэзи
		string GenresGroupSf { get; set; }
		// Детективы, Боевики
		string GenresGroupDetective { get; set; }
		// Проза
		string GenresGroupProse { get; set; }
		// Любовные романы
		string GenresGroupLove { get; set; }
		// Приключения
		string GenresGroupAdventure { get; set; }
		// Детское
		string GenresGroupChildren { get; set; }
		// Поэзия, Драматургия
		string GenresGroupPoetry { get; set; }
		// Старинное
		string GenresGroupAntique { get; set; }
		// Наука, Образование
		string GenresGroupScience { get; set; }
		// Компьютеры
		string GenresGroupComputers { get; set; }
		// Справочники
		string GenresGroupReference { get; set; }
		// Документальное
		string GenresGroupNonfiction { get; set; }
		// Религия
		string GenresGroupReligion { get; set; }
		// Юмор
		string GenresGroupHumor { get; set; }
		// Дом, Семья
		string GenresGroupHome { get; set; }
		// Бизнес
		string GenresGroupBusiness { get; set; }
		// Техника
		string GenresGroupTech { get; set; }
		// Военное дело
		string GenresGroupMilitary { get; set; }
		// Фольклор
		string GenresGroupFolklore { get; set; }
		// Прочее
		string GenresGroupOther { get; set; }
	}
}
