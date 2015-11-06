/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 13.05.2014
 * Time: 9:06
 * 
 * License: GPL 2.1
 */
using System;

// enums
using EndWorkModeEnum = Core.Common.Enums.EndWorkModeEnum;

namespace Core.Common
{
	/// <summary>
	/// EndWorkMode: причина завершения работы обработки файлов
	/// </summary>
	public class EndWorkMode
	{
		#region Закрытые данные класса
		private EndWorkModeEnum m_EndMode	= EndWorkModeEnum.Done;
		private string m_Message			= string.Empty;
		#endregion
		
		public EndWorkMode()
		{
		}
		
		#region Открытые свойства класса
		public virtual EndWorkModeEnum EndMode {
			get { return m_EndMode; }
			set { m_EndMode = value; }
		}
		
		public virtual string Message {
			get { return m_Message; }
			set { m_Message = value; }
		}
		#endregion
	}
}
