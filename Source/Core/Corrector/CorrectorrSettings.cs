/*
 * Сделано в SharpDevelop.
 * Пользователь: Вадим Кузнецов (DikBSD)
 * Дата: 28.07.2015
 * Время: 16:49
 * 
 * License: GPL 2.1
 */
using System;

namespace Settings
{
	/// <summary>
	/// Настройки Корректора fb2 файлов
	/// </summary>
	public class CorrectorSettings
	{
		#region Закрытые статические данные класса ПО-УМОЛЧАНИЮ
		// пути к файлам-справкам
		private readonly static string m_CorrectorHelpPath	= Settings.ProgDir + @"\Help\CorrectorSettings.rtf";
		private readonly static string m_CorrectorPath		= Settings.ProgDir + @"\CorrectorSettings.xml";
		#endregion
		
		public CorrectorSettings()
		{
		}
		
		#region Открытые статические общие свойства класса
		public static string CorrectorPath {
			get { return m_CorrectorPath; }
		}
		public static string CorrectorHelpPath {
			get { return m_CorrectorHelpPath; }
		}
		#endregion
	}
}
