/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 07.08.2015
 * Время: 10:36
 * 
  */
using System;
using System.Drawing;

namespace Core.Common
{
	/// <summary>
	/// Colors: цвета для шрифтов списков файлов
	/// </summary>
	public class Colors
	{
		private static readonly Color m_DirBackColor			= Color.Beige;			// цвет фона каталогов
		private static readonly Color m_FileBackColor			= Color.LightSteelBlue;	// цвет фона файлов
		private static readonly Color m_FB2ForeColor			= Color.Black; 			// цвет для fb2
		private static readonly Color m_ZipFB2ForeColor			= Color.Green; 			// цвет для fb2 в zip
		private static readonly Color m_BadZipForeColor			= Color.Purple;			// цвет битых архивов
		private static readonly Color m_FB2NotValidForeColor	= Color.Blue;			// цвет невалидных fb2 файлов и архивов
		
		public Colors()
		{
		}
		
		public static Color DirBackColor {
			get {
				return m_DirBackColor;
			}
		}
		public static Color FB2ForeColor {
			get {
				return m_FB2ForeColor;
			}
		}
		public static Color FileBackColor {
			get {
				return m_FileBackColor;
			}
		}
		public static Color ZipFB2ForeColor {
			get {
				return m_ZipFB2ForeColor;
			}
		}
		public static Color BadZipForeColor {
			get {
				return m_BadZipForeColor;
			}
		}
		public static Color FB2NotValidForeColor {
			get {
				return m_FB2NotValidForeColor;
			}
		}
		
		
		
	}
}
