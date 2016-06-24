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
		private long m_nAllDirs			= 0;
		private long m_nAllFiles		= 0;
		private long m_nSourceFB2		= 0;
		private long m_nZip				= 0;
		private long m_nFB2FromZips		= 0;
		private long m_nOther			= 0;
		private long m_nCreateInTarget	= 0;
		private long m_nNotRead			= 0;
		private long m_nNotValidFB2		= 0;
		private long m_nBadZip			= 0;
		private long m_nNotSort			= 0;
		private long m_nLongPath		= 0;
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
		public virtual long AllDirs {
			get { return m_nAllDirs; }
			set { m_nAllDirs = value; }
        }
		public virtual long AllFiles {
			get { return m_nAllFiles; }
			set { m_nAllFiles = value; }
        }
		public virtual long SourceFB2 {
			get { return m_nSourceFB2; }
			set { m_nSourceFB2 = value; }
        }
		public virtual long Zip {
			get { return m_nZip; }
			set { m_nZip = value; }
        }
		public virtual long FB2FromZips {
			get { return m_nFB2FromZips; }
			set { m_nFB2FromZips = value; }
        }
		public virtual long Other {
			get { return m_nOther; }
			set { m_nOther = value; }
        }
		public virtual long CreateInTarget {
			get { return m_nCreateInTarget; }
			set { m_nCreateInTarget = value; }
        }
		public virtual long NotRead {
			get { return m_nNotRead; }
			set { m_nNotRead = value; }
        }
		public virtual long NotValidFB2 {
			get { return m_nNotValidFB2; }
			set { m_nNotValidFB2 = value; }
        }
		public virtual long BadZip {
			get { return m_nBadZip; }
			set { m_nBadZip = value; }
        }
		public virtual long NotSort {
			get { return m_nNotSort; }
			set { m_nNotSort = value; }
		}
		public virtual long LongPath {
			get { return m_nLongPath; }
			set { m_nLongPath = value; }
		}
		#endregion
	}
}
