/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 07.10.2009
 * Time: 8:44
 * 
 * License: GPL 2.1
 */
using System;

namespace Core.Duplicator
{
	/// <summary>
	/// класс для хранения данных для отображения прогресса Дубликатора
	/// </summary>
	public class StatusView {
		
		#region Закрытые данные класса
		private int m_nAllFiles			= 0;
		private int m_nGroup			= 0;
		private int m_nAllFB2InGroups	= 0;
		#endregion
		
		public StatusView() {

		}
		
		#region Открытые методы класса
		public void Clear() {
			// сброс всех данных
			m_nAllFiles			= 0;
			m_nGroup			= 0;
			m_nAllFB2InGroups	= 0;
		}
		#endregion
		
		#region Свойства класса
		public virtual int AllFiles {
			get { return m_nAllFiles; }
			set { m_nAllFiles = value; }
        }
		
		public virtual int Group {
			get { return m_nGroup; }
			set { m_nGroup = value; }
        }
		
		public virtual int AllFB2InGroups {
			get { return m_nAllFB2InGroups; }
			set { m_nAllFB2InGroups = value; }
        }
		#endregion
	}
}
