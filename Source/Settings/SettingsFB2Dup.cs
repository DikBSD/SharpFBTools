/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 04.09.2009
 * Time: 17:21
 * 
 * License: GPL 2.1
 */
using System;

namespace Settings
{
	/// <summary>
	/// Description of SettingsFB2Dup.
	/// </summary>
	public class SettingsFB2Dup
	{
		#region Закрытые статические данные класса
		// рабочие папки и данные для Полной Сортировки
		private static string m_sFB2DupScanDir = "";
		#endregion
		
		public SettingsFB2Dup()
		{
		}
			
		#region Открытые статические свойства класса
		public static string FMDataScanDir {
			get { return m_sFB2DupScanDir; }
			set { m_sFB2DupScanDir = value; }
		}
		#endregion
	}
}
