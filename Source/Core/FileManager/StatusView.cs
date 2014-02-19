/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 09.10.2009
 * Time: 14:14
 * 
 * License: GPL 2.1
 */
using System;

namespace Core.FileManager
{
	/// <summary>
	/// класс для хранения данных для отображения прогресса Менеджера Файлов
	/// </summary>
	public class StatusView {
		
		#region Закрытые данные класса
		private int m_nAllDirs			= 0;
		private int m_nAllFiles			= 0;
		private int m_nSourceFB2		= 0;
		private int m_nZip				= 0;
		private int m_nFB2FromZips		= 0;
		private int m_nOther			= 0;
		private int m_nCreateInTarget	= 0;
		private int m_nNotRead			= 0;
		private int m_nNotValidFB2		= 0;
		private int m_nBadZip			= 0;
		private int m_nNotSort			= 0;
		private int m_nLongPath			= 0;
		#endregion
		
		public StatusView() {

		}
		
		#region Открытые методы класса
		public void Clear() {
			// сброс всех данных
			m_nAllDirs			= 0;
			m_nAllFiles			= 0;
			m_nSourceFB2		= 0;
			m_nZip				= 0;
			m_nFB2FromZips		= 0;
			m_nOther			= 0;
			m_nCreateInTarget	= 0;
			m_nNotRead			= 0;
			m_nNotValidFB2		= 0;
			m_nBadZip			= 0;
			m_nNotSort			= 0;
			m_nLongPath			= 0;
		}
		#endregion
		
		#region Свойства класса
		public virtual int AllDirs {
			get { return m_nAllDirs; }
			set { m_nAllDirs = value; }
        }
		public virtual int AllFiles {
			get { return m_nAllFiles; }
			set { m_nAllFiles = value; }
        }
		public virtual int SourceFB2 {
			get { return m_nSourceFB2; }
			set { m_nSourceFB2 = value; }
        }
		public virtual int Zip {
			get { return m_nZip; }
			set { m_nZip = value; }
        }
		public virtual int FB2FromZips {
			get { return m_nFB2FromZips; }
			set { m_nFB2FromZips = value; }
        }
		public virtual int Other {
			get { return m_nOther; }
			set { m_nOther = value; }
        }
		public virtual int CreateInTarget {
			get { return m_nCreateInTarget; }
			set { m_nCreateInTarget = value; }
        }
		public virtual int NotRead {
			get { return m_nNotRead; }
			set { m_nNotRead = value; }
        }
		public virtual int NotValidFB2 {
			get { return m_nNotValidFB2; }
			set { m_nNotValidFB2 = value; }
        }
		public virtual int BadZip {
			get { return m_nBadZip; }
			set { m_nBadZip = value; }
        }
		public virtual int NotSort {
			get { return m_nNotSort; }
			set { m_nNotSort = value; }
		}
		public virtual int LongPath {
			get { return m_nLongPath; }
			set { m_nLongPath = value; }
		}
		#endregion
	}
}
