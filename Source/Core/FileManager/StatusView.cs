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
		private int m_nRar				= 0;
		private int m_n7Zip				= 0;
		private int m_nBZip2			= 0;
		private int m_nGzip				= 0;
		private int m_nTar				= 0;
		private int m_nFB2FromArchives	= 0;
		private int m_nOther			= 0;
		private int m_nCreateInTarget	= 0;
		private int m_nNotRead			= 0;
		private int m_nNotValidFB2		= 0;
		private int m_nBadArchive		= 0;
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
			m_nRar				= 0;
			m_n7Zip				= 0;
			m_nBZip2			= 0;
			m_nGzip				= 0;
			m_nTar				= 0;
			m_nFB2FromArchives	= 0;
			m_nOther			= 0;
			m_nCreateInTarget	= 0;
			m_nNotRead			= 0;
			m_nNotValidFB2		= 0;
			m_nBadArchive		= 0;
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
		public virtual int Rar {
			get { return m_nRar; }
			set { m_nRar = value; }
        }
		public virtual int A7Zip {
			get { return m_n7Zip; }
			set { m_n7Zip = value; }
        }
		public virtual int BZip2 {
			get { return m_nBZip2; }
			set { m_nBZip2 = value; }
        }
		public virtual int Gzip {
			get { return m_nGzip; }
			set { m_nGzip = value; }
        }
		public virtual int Tar {
			get { return m_nTar; }
			set { m_nTar = value; }
        }
		public virtual int FB2FromArchives {
			get { return m_nFB2FromArchives; }
			set { m_nFB2FromArchives = value; }
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
		public virtual int BadArchive {
			get { return m_nBadArchive; }
			set { m_nBadArchive = value; }
        }
		#endregion
	}
}
