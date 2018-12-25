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
using System.IO;

namespace Core.Common
{
	/// <summary>
	/// Отладка кода
	/// </summary>
	public class Debug
	{
		private readonly static string _logPath = "_DebugLogFile.txt";
		
		public Debug()
		{
		}
	
		/// <summary>
		/// Путь к Log файлу
		/// </summary>
		public static string LogFilePath {
			get { return _logPath; }
		}
		
		
		/// <summary>
		/// Отладочное сообщение
		/// </summary>
		/// <param name="FilePath">Путь к файлу, при обработке которого произошло исключение</param>
		/// <param name="ex">Ошибки, происходящие во время выполнения приложения</param>
		/// <param name="prevMessage">Сообщение, которое нужно показать до отладочного</param>
		/// <param name="postMessage">Сообщение, которое нужно показать после отладочного</param>
		public static void DebugMessage( string FilePath, Exception ex,
		                                string prevMessage = null, string postMessage = null ) {
			if ( Settings.Settings.SaveDebugMessage ) {
				string Message = gebugMessageString( FilePath, ex, prevMessage, postMessage );
				// Записывать сообщения об ошибках при падении работы алгоритмов в Log файл
				debugLog( Message, true );
			}
		}
		
		/// <summary>
		/// Отладочное сообщение в Log файл
		/// </summary>
		/// <param name="Message">Сгенарированное сообщение для отладки</param>
		/// <param name="append">true - добавление к файлу; false - замена файла.</param>
		private static void debugLog( string Message, bool append = true ) {
			using (StreamWriter sw = new StreamWriter( _logPath, append, System.Text.Encoding.UTF8 ) )
			{
				sw.WriteLine( Message );
			}
		}
		
		/// <summary>
		/// Генарация сообщения для отладки
		/// </summary>
		/// <param name="FilePath">Путь к файлу, при обработке которого произошло исключение</param>
		/// <param name="ex">Ошибки, происходящие во время выполнения приложения</param>
		/// <param name="prevMessage">Сообщение, которое нужно показать до отладочного</param>
		/// <param name="postMessage">Сообщение, которое нужно показать после отладочного</param>
		/// <returns>Стройка отладочного сообщения</returns>
		private static string gebugMessageString( string FilePath, Exception ex,
		                                         string prevMessage = null, string postMessage = null ) {
			string s = string.Format( "{0}\r\n", DateTime.Now );
			
			if ( !string.IsNullOrEmpty(prevMessage) )
				s += prevMessage + "\r\n";
			
			s += string.IsNullOrEmpty(FilePath)
				? string.Empty
				: string.Format( "Файл, где произошло падение алгоритма:\r\n\t{0}\r\n", FilePath );
			
			if ( ex != null ) {
				s += "Ошибка:\r\n\t" + ex.Message +
				"\r\n\r\nStackTrace:\r\n" + ex.StackTrace +
				"\r\n\r\nМетод, создавший исключение:\r\n\t" + ex.TargetSite.Name;
			}
			
			if ( !string.IsNullOrEmpty(postMessage) )
				s += "\r\n\r\n" + postMessage;
			
			s += "\r\n===================================================";
			return  s;
		}
		
		/// <summary>
		/// Запись в Log файл - добавление
		/// </summary>
		/// <param name="Text">Записываемый текст</param>
		/// <param name="IsStartSave">true - Если впервые создаем Log; false - Добавление к уже существующему Log</param>
		/// <param name="LogPath">Путь к log-файлу. Если LogPath = null или Empty, то сохранение происходит в папке исполняемой программы</param>
		public static void appendTextInLog( string Text, bool IsStartSave = false, string LogPath = null ) {
			DirectoryInfo di = new DirectoryInfo( Application.StartupPath );
			string Log = string.IsNullOrEmpty( LogPath )
				?  string.Format(
					"{0}\\{1}", di.FullName.Substring(0, di.FullName.Length - di.Extension.Length), _logPath
				)
				: LogPath;
			if ( IsStartSave ) {
				// создаем Log
				if ( File.Exists(Log) ) {
					File.Delete(Log);
				}
				using ( System.IO.StreamWriter file =
				       new System.IO.StreamWriter( Log ) )
				{
					file.WriteLine("======= Log выполнения работы =======\r\n");
				}
				using ( System.IO.StreamWriter file =
				       new System.IO.StreamWriter(Log, true, System.Text.Encoding.UTF8) )
				{
					file.WriteLine( string.Format( "{0}", Text ) );
				}
			} else {
				// добавляем в Log
				using ( System.IO.StreamWriter file =
				       new System.IO.StreamWriter(Log, true, System.Text.Encoding.UTF8) )
				{
					file.WriteLine( string.Format( "{0}", Text ) );
				}
			}
		}
		
	}
}
