/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 04.12.2015
 * Время: 13:10
 * 
 * License: GPL 2.1
 */
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Core.AutoCorrector
{
	/// <summary>
	/// Корректировка тега FictionBook
	/// </summary>
	public class FictionBookTagCorrector
	{
		private const string _FictionBookTag = "<FictionBook xmlns=\"http://www.gribuser.ru/xml/fictionbook/2.0\" xmlns:l=\"http://www.w3.org/1999/xlink\" xmlns:xlink=\"http://www.w3.org/1999/xlink\">";
		private const string _MessageTitle = "Автокорректор";
		
		public FictionBookTagCorrector()
		{
		}
		
		/// <summary>
		/// Универсальный и корректный тег FictionBook
		/// </summary>
		public virtual string NewFictionBookTag {
			get {
				return _FictionBookTag;
			}
		}
		
		/// <summary>
		/// Замена тега FictionBook универсальным и корректным тегом
		/// </summary>
		/// <param name="XmlDescription">Обрабатываемая xml строка тега FictionBook типа string</param>
		/// <returns>Корректный тег FictionBook типа string </returns>
		public string StartTagCorrect( string XmlDescription ) {
			Regex regex  = new Regex( "<FictionBook [^>]+>", RegexOptions.None );
			Match m = regex.Match( XmlDescription );
			if ( m.Success ) {
				string FBookTag = m.Value;
				try {
					XmlDescription = Regex.Replace(
						XmlDescription, "<FictionBook [^>]+>", _FictionBookTag, RegexOptions.None
					);
				} catch ( RegexMatchTimeoutException /*ex*/ ) {}
				catch ( Exception ex ) {
					if ( Settings.Settings.ShowDebugMessage ) {
						// Показывать сообщения об ошибках при падении работы алгоритмов
						MessageBox.Show(
							string.Format("Обработка раздела <description> FictionBookTagCorrector:\r\nЗамена тега FictionBook универсальным и корректным тегом.\r\nОшибка:\r\n{0}", ex.Message), _MessageTitle, MessageBoxButtons.OK, MessageBoxIcon.Error
						);
					}
				}
			}
			return XmlDescription;
		}
	}
}
