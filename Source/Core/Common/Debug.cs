/*
 * Создано в SharpDevelop.
 * Пользователь: Вадим Кузнецов (DikBSD)
 * Дата: 14.12.2018
 * Время: 10:28
 * 
 * License: GPL 3.0
 */
using System;
using System.Windows.Forms;

namespace Core.Common
{
	/// <summary>
	/// Отладка кода
	/// </summary>
	public class Debug
	{
		public Debug()
		{
		}
		
		/// <summary>
		/// Отладочное сообщение
		/// </summary>
		/// <param name="ex">Ошибки, происходящие во время выполнения приложения</param>
		/// <param name="prevMessage">Сообщение, которое нужно показать до отладочного</param>
		/// <param name="postMessage">Сообщение, которое нужно показать после отладочного</param>
		public static void DebugMessage( Exception ex, string prevMessage = null, string postMessage = null ) {
			if ( Settings.Settings.ShowDebugMessage ) {
				// Показывать сообщения об ошибках при падении работы алгоритмов
				string s = "Ошибка:\r\n"+ex.Message+
					"\r\n\r\nStackTrace:\r\n"+ex.StackTrace+
					"\r\n\r\nМетод, создавший исключение:\t"+ex.TargetSite.Name;
				
				if ( string.IsNullOrEmpty(prevMessage) )
					s = prevMessage + "\r\n" + s;
				
				if ( string.IsNullOrEmpty(postMessage) )
					s += "\r\n\r\n" + prevMessage;
				MessageBox.Show( s, "Отладка", MessageBoxButtons.OK, MessageBoxIcon.Error );
			}
		}
	}
}
