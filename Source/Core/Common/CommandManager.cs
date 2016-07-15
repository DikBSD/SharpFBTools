/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 08.08.2009
 * Time: 17:06
 * 
 * License: GPL 2.1
 */

using System;
using System.Diagnostics;

namespace Core.Common
{

	// ********************************************************************* //
	public class Priority {
		public Priority() {
		}
		
		public static ProcessPriorityClass GetPriority( string sPriority ) {
			switch( sPriority ) {
				case "Низкий (Idle)" :
					return ProcessPriorityClass.Idle;
				case "Ниже Среднего" :
					return ProcessPriorityClass.BelowNormal;
				case "Средний" :
					return ProcessPriorityClass.Normal;
				case "Выше Среднего" :
					return ProcessPriorityClass.AboveNormal;
				case "Высокий" :
					return ProcessPriorityClass.High;
				case "Реального времени (RealTime)" :
					return ProcessPriorityClass.RealTime;
					default :
						return ProcessPriorityClass.AboveNormal;
			}
		}
	}
	
	// ************************************************************************************* //
	
	public class CommandManager {
		private Process	m_oProc;

		public CommandManager() {

		}

		/// <summary>
		/// Запуск процесса в синхронном режиме - т.е. управление возвращается к программе только после завершения процесса
		/// </summary>
		/// <param name="RunSync">true - Запуск процесса в синхронном режиме - т.е. управление возвращается к программе только после завершения процесса. false - Запуск процесса в асинхронном режиме - т.е. управление возвращается к программе после запуска процесса.</param>
		/// <param name="sStartProgPath">Путь к запускаемой программе</param>
		/// <param name="sArgs">Аргументы коммандной строки для запускаемой программы</param>
		/// <param name="pwStyle">Каким образом должно выглядеть новое окно при запуске процесса системой</param>
		/// <param name="ppcPriorityClass">Приоритет, который система связывает с процессом.</param>
		/// <returns>Признак завершения процесса - Empty - </returns>
		public string Run( bool RunSync, string sStartProgPath, string sArgs, ProcessWindowStyle pwStyle, ProcessPriorityClass  ppcPriorityClass ) {
			m_oProc = new Process();
			
			ProcessStartInfo oInfo = string.IsNullOrWhiteSpace( sArgs )
				? new ProcessStartInfo( sStartProgPath )
				: new ProcessStartInfo( sStartProgPath, sArgs );
			
			oInfo.UseShellExecute	= true;
			oInfo.CreateNoWindow	= true;
			oInfo.WindowStyle		= pwStyle;

			m_oProc.StartInfo = oInfo;

			try {
				m_oProc.Start();
				Process.GetProcessById(m_oProc.Id).PriorityClass = ppcPriorityClass;
				if ( RunSync )
					m_oProc.WaitForExit();
				m_oProc.Close();
				m_oProc.Dispose();
				return string.Empty;
			} catch( System.Exception e ) {
				if( !m_oProc.HasExited ) {
					m_oProc.Kill();
					m_oProc.Close();
					m_oProc.Dispose();
					return "Error: Hung process terminated ...\r\n"+e.Message+"\r\n";;
				}
			}
			return string.Empty;
		}	
	}
}
