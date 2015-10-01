/*
 * Сделано в SharpDevelop.
 * Пользователь: Вадим Кузнецов (DikBSD)
 * Дата: 30.09.2015
 * Время: 9:46
 * 
 */
using System;
using System.Collections.Generic;

namespace Core.Common
{
	/// <summary>
	/// Словарь, поддерживающий множество одинаковых ключей
	/// </summary>
	public class MultiDictionary<Type>
	{
		Dictionary<string, List<Type>> _dictionary = new Dictionary<string, List<Type>>();

		public void Add(string key, Type value) {
			List<Type> list;
			if (_dictionary.TryGetValue(key, out list)) {
				list.Add(value);
			} else {
				list = new List<Type>();
				list.Add(value);
				_dictionary[key] = list;
			}
		}

		public IEnumerable<string> Keys {
			get {
				return _dictionary.Keys;
			}
		}
		
		public Dictionary<string, List<Type>> Dictionary {
			get {
				return _dictionary;
			}
		}
		
		public List<Type> this[string key] {
			get {
				List<Type> list;
				if ( !this._dictionary.TryGetValue(key, out list) ) {
					list = new List<Type>();
					_dictionary[key] = list;
				}
				return list;
			}
		}

	}
}
