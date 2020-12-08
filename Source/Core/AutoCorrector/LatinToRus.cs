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
using System.Text.RegularExpressions;

using Core.FB2.Description.Common;

using System.Windows.Forms;

namespace Core.AutoCorrector
{
	/// <summary>
	/// Замена латинских первых букв в ФИО на русские
	/// </summary>
	public class LatinToRus
	{
		private readonly Dictionary<string, string> _dic = new Dictionary<string, string>();
		
		/// <summary>
		/// Конструктор класса LatinToRus
		/// </summary>
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
		/// Проверка, если ли в строке Text хотя бы один русский символ
		/// </summary>
		/// <param name="Text">Текст для обработки</param>
		/// <returns>true, если в строке Text есть хотя бы один русский символ; false - в противном случае</returns>
		public bool isExistCharRussian( string Text ) {
			return Regex.IsMatch( Text, "[А-Яа-яЁё]+" );
		}
		
		/// <summary>
		/// Замена 1-го латинского символа в строке Text на соответствующий кирилический
		/// </summary>
		/// <param name="Text">Текст для обработки</param>
		/// <returns>строку типа string, если была произведена обработка; null - в противном случае</returns>
		public string replaceFirstCharLatinToRus( string Text ) {
			if ( !string.IsNullOrWhiteSpace(Text) ) {
				if ( isExistCharRussian( Text ) ) {
					// пропускаем Text, который состоят только из НЕ русских символов
					StringBuilder sb = new StringBuilder( Text.Trim() );
					foreach( KeyValuePair<string, string> kvp in _dic ) {
						if ( sb[0] == kvp.Key.ToCharArray()[0] ) {
							sb[0] = kvp.Value.ToCharArray()[0];
							return sb.ToString();
						}
					}
				}
			}
			return null;
		}

		/// <summary>
		/// Замена 1-го латинского символа в ФИО Авторов на соответствующий кирилический
		/// </summary>
		/// <param name="Authors">Список fb2 Авторов для обработки</param>
		/// <returns>Откорректированный список fb2 Авторов</returns>
		public IList<Author> replaceFirstCharLatinToRusForAuthors(IList<Author> Authors)
		{
			IList<Author> correctAuthors = new List<Author>();
			foreach (Author author in Authors) {
				string name = replaceFirstCharLatinToRus(author.FirstName.Value);
				if (name != null)
					author.FirstName.Value = name;

				name = replaceFirstCharLatinToRus(author.MiddleName.Value);
				if (name != null)
					author.MiddleName.Value = name;

				name = replaceFirstCharLatinToRus(author.LastName.Value);
				if (name != null)
					author.LastName.Value = name;

				correctAuthors.Add(author);
			}
			return correctAuthors;
		}

	}
}
