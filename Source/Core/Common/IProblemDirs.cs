/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 26.05.2014
 * Time: 13:31
 * 
 * License: GPL 2.1
 */
using System;

namespace Core.Common
{
	/// <summary>
	/// IProblemDirs: названия папок для "проблемных" файлов
	/// </summary>
	public interface IProblemDirs
	{
		string NotReadFB2Dir { get; set; }
		string FileLongPathDir { get; set; }
		string NotValidFB2Dir { get; set; }
		string NotOpenArchDir { get; set; }
	}
}
