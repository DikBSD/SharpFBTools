/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 17.09.2009
 * Time: 15:41
 * 
 * License: GPL 2.1
 */
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using Core.FB2.Description.Common;
using Core.FB2.Description.TitleInfo;

namespace Core.FB2Dublicator
{
	/// <summary>
	/// класс для сравнения двух книг по Авторам и Названию для Дубликатора
	/// (условие - название или один из Авторов должны быть хотя бы частью Названия или данных Автора соответственно)
	/// </summary>
	public class FB2ABTComparer
	{
		#region Закрытые данные класса
		private BookTitle m_BT1					= null;
		private BookTitle m_BT2					= null;
		private IList<Author> m_lFB2Authors1	= null;
		private IList<Author> m_lFB2Authors2	= null;
		#endregion
		
		public FB2ABTComparer( BookTitle BT1, BookTitle BT2, IList<Author> lFB2Authors1, IList<Author> lFB2Authors2 )
		{
			m_BT1 = BT1;
			m_BT2 = BT2;
			m_lFB2Authors1 = lFB2Authors1;
			m_lFB2Authors2 = lFB2Authors2;
		}
						
		#region Закрытые вспомогательные методы класса
		// сравнение 2-х строк на включение 2-й в 1-ю или 1-й во 2-ю
		private bool IsStringsEquality( string str1, string str2 ) {
			bool b1 = false, b2 = false;
			Regex re = null;
			if( str1.Length != 0 ) {
				re = new Regex( str1, RegexOptions.IgnoreCase );
				b1 = re.IsMatch( str2 );
			}
			if( str2.Length != 0 ) {
				re = new Regex( str2, RegexOptions.IgnoreCase );
				b2 = re.IsMatch( str1 );
			}
			return ( b1 || b2 );
		}
		
		// включается ли Фамилия И Имя (или только Фамилия) 2-го Автора в Фамилия И Имя (или только Фамилия) 1-го и наоборот
		private bool IsLastFirsNameEquality( Author afb2_1, Author afb2_2 ) {
			if( afb2_1.LastName != null ) {
				// У Автора 1-й Книги есть Фамилия
				if( afb2_1.FirstName != null ) {
					// У Автора 1-й Книги есть И Фамилия И Имя. Смотрим, что есть у Автора из 2-й Книги
					if( ( afb2_2.LastName != null ) && ( afb2_2.FirstName != null ) ) {
						// У Автора 2-й Книги есть И Фамилия И Имя : сравниваем по Фамилия И Имя
						if( IsStringsEquality( afb2_1.LastName.Value, afb2_2.LastName.Value ) &&
						   	IsStringsEquality( afb2_1.FirstName.Value, afb2_2.FirstName.Value )	) {
							return true; // нашли соответствие одного Автора, можно завершать сравнение
						}
					}
				} else {
					// У Автора 1-й Книги есть Фамилия, НО нет Имени : сравниваем только по Фамилия
					if( afb2_2.LastName != null ) {
						// У Автора 2-й Книги есть Фамилия
						if( IsStringsEquality( afb2_1.LastName.Value, afb2_2.LastName.Value ) ) {
							return true; // нашли соответствие одного Автора, можно завершать сравнение
						}
					}
				}
			}
			return false;
		}
		
		// включается ли Nick 2-го Автора в Nick 1-го и наоборот
		private bool IsNickEquality( Author afb2_1, Author afb2_2 ) {
			if( afb2_1.NickName != null && afb2_2.NickName != null ) {
				if( IsStringsEquality( afb2_1.NickName.Value, afb2_2.NickName.Value ) ) {
					return true;
				}
			}
			return false;
		}
		#endregion
		
		#region Открытые методы класса
		
		// возвращается true, если файлы имеют одинаковое BookTitle (название книги)
		// или одно содержится в другом
		public bool IsBookTitleEquality() {
			if( m_BT1 == null && m_BT2 == null )
				return true;
			else if( ( m_BT1 == null && m_BT2 != null ) || ( m_BT1 != null && m_BT2 == null ) )
				return false;
			
			if( m_BT1.Value == null && m_BT2.Value == null )
				return true;
			else if( ( m_BT1.Value == null && m_BT2.Value != null ) || ( m_BT1.Value != null && m_BT2.Value == null ) )
				return false;
			
			return IsStringsEquality( m_BT1.Value, m_BT2.Value );
        }
		
		/* I. Алгоритм:
		1. Сравнение - только по тегам: Имя, Фамилия, Отчество и Ник Авторов (e-mail и web игнорируются)
		2. Условия: Если в обоих книгах есть ФИ, то сравнение по ФИ, Иначе
						Если в одной книге есть Ф, а в другой - ФИ или Ф, то сравнение по Ф, Иначе
						Если в обоин книгах есть Ник, то сравнение по Нику, Иначе
						Авторы - разные
		3. Сравнения - не строгое, а на включение данных Автора одной в тегах для другой,
			чтобы отловить такие ситуации: в 1-й книге: Стругацкий, во 2-й - Стругацкий (Витецкий) */
		/* Возвращаемое значение:
			возвращается true, если какой-то автор из книги 1 есть в списке авторов книге 2 */
		public bool IsBookAuthorEquality() {
			if( m_lFB2Authors1 == null && m_lFB2Authors2 == null )
				return true;
			else if( ( m_lFB2Authors1 == null && m_lFB2Authors2 != null ) || ( m_lFB2Authors1 != null && m_lFB2Authors2 == null ) )
				return false;
			
			// соответствие какого-то одного автора в обоих книгах
			foreach( Author afb2_1 in m_lFB2Authors1 ) {
				foreach( Author afb2_2 in m_lFB2Authors2 ) {
					// Сначала Сравниваем Фамилия И Имя (или только Фамилия) Авторов
					if( IsLastFirsNameEquality( afb2_1, afb2_2 ) ) {
						return true; // нашли соответствие одного Автора, можно завершать сравнение
					}
					
					// Теперь Сравниваем Авторов по их Никам
					if( IsNickEquality( afb2_1, afb2_2 ) ) {
						return true; // нашли соответствие одного Автора, можно завершать сравнение
					}
				}
			}
			
			return false;
		}

		#endregion
	}
}
