/*
 * Сделано в SharpDevelop.
 * Пользователь: Вадим Кузнецов (DikBSD)
 * Дата: 17.03.2016
 * Время: 11:47
 * 
 * License: GPL 2.1
 */
using System;
using System.Text;
using System.Collections.Generic;

using System.Windows.Forms;

namespace Core.AutoCorrector
{
	/// <summary>
	/// Замена латинских первых букв в ФИО на русские
	/// </summary>
	public class LatinToRus
	{
		private readonly Dictionary<string, string> _dic = new Dictionary<string, string>();
		
		public LatinToRus()
		{
			_dic.Add("A", "А");
			_dic.Add("B", "В");
			_dic.Add("C", "С");
			_dic.Add("E", "Е");
			_dic.Add("H", "Н");
			_dic.Add("K", "К");
			_dic.Add("M", "М");
			_dic.Add("O", "О");
			_dic.Add("P", "Р");
			_dic.Add("T", "Т");
			_dic.Add("X", "Х");
			
			_dic.Add("a", "а");
			_dic.Add("b", "в");
			_dic.Add("c", "с");
			_dic.Add("e", "е");
			_dic.Add("h", "н");
			_dic.Add("k", "к");
			_dic.Add("m", "м");
			_dic.Add("o", "о");
			_dic.Add("p", "р");
			_dic.Add("t", "т");
			_dic.Add("x", "х");
		}
		
		/// <summary>
		/// Замена 1-го латинского символа в строке Text на соответствующий кирилический
		/// </summary>
		/// <param name="Text">Текст для обработки</param>
		/// <returns>троку типа string, если была произведена обработка; null - в противном случае</returns>
		public string replaceFirstCharLatinToRus( string Text ) {
			StringBuilder sb = new StringBuilder( Text.Trim() );
			foreach( KeyValuePair<string, string> kvp in _dic ) {
				if ( sb[0] == kvp.Key.ToCharArray()[0] ) {
					sb[0] = kvp.Value.ToCharArray()[0];
					return sb.ToString();
				}
			}
			return null;
		}
	}
}
