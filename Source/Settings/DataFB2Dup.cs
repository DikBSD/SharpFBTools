/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 07.09.2009
 * Time: 10:55
 * 
 * License: GPL 2.1
 */
using System;

namespace Settings
{
	/// <summary>
	/// Description of DataFB2Dup.
	/// </summary>
	public class DataFB2Dup
	{
		#region Закрытые данные класса
		// пути к архиваторам
		//TODO Удалить
		private string m_s7zaPath	= Settings.Read7zaPath();
		private string m_sTempDir	= Settings.GetTempDir();
		#endregion
		
		public DataFB2Dup()
		{
		}
		
		#region Свойства класса
		// временная папка
		public virtual string TempDir {
			get { return m_sTempDir; }
			set { m_sTempDir = value; }
        }
		// пути к архиваторам
		public virtual string A7zaPath {
			get { return m_s7zaPath; }
			set { m_s7zaPath = value; }
        }
		#endregion
	}
}
