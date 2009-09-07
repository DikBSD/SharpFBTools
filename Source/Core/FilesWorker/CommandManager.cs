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
using System.Threading;

namespace Core.FilesWorker
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
        private string	m_sResult;

        public CommandManager() {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sStartProgPath"></param>
        /// <param name="sArgs"></param>
        /// <param name="ppcPriorityClass"></param>
        /// <returns></returns>
        // запуск процесса в синхронном режиме - т.е. управление возвращается к программе только после завершения процесса
        public string Run( string sStartProgPath, string sArgs, ProcessWindowStyle processWindowStyle, ProcessPriorityClass  ppcPriorityClass )  {
            m_sResult = "";
            m_oProc = new Process();
            
            ProcessStartInfo oInfo;
            if( sArgs == null || sArgs == "" )
            	oInfo = new ProcessStartInfo( sStartProgPath );
            else oInfo = new ProcessStartInfo( sStartProgPath, sArgs );
            
			oInfo.UseShellExecute	= true;
			oInfo.CreateNoWindow	= true;
			oInfo.WindowStyle		= processWindowStyle;

			m_oProc.StartInfo = oInfo;

			try {
                m_oProc.Start();
                m_oProc.PriorityClass = ppcPriorityClass;
                m_oProc.WaitForExit();
                return m_sResult;
            } catch( System.Exception e ) {
                if( m_oProc.HasExited == false ) {
                    m_oProc.Kill();
                    m_sResult = "Error: Hung process terminated ...\r\n"+e.Message+"\r\n";
                    return m_sResult;
                }
            } finally {
                m_oProc.Close();
                m_oProc.Dispose();                
            }

            return m_sResult;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sStartProgPath"></param>
        /// <param name="sArgs"></param>
        /// <param name="ppcPriorityClass"></param>
        /// <returns></returns>
        // запуск процесса в асинхронном режиме - т.е. управление возвращается к программе после запуска процесса
        public string RunAsync( string sStartProgPath, string sArgs, ProcessWindowStyle processWindowStyle, ProcessPriorityClass  ppcPriorityClass )  {
            m_sResult = "";
            m_oProc = new Process();
            
            ProcessStartInfo oInfo;
            if( sArgs == null || sArgs == "" )
            	oInfo = new ProcessStartInfo( sStartProgPath );
            else oInfo = new ProcessStartInfo( sStartProgPath, sArgs );
            
			oInfo.UseShellExecute	= true;
			oInfo.CreateNoWindow	= true;
			oInfo.WindowStyle		= processWindowStyle;

			m_oProc.StartInfo = oInfo;

			try {
                m_oProc.Start();
                m_oProc.PriorityClass = ppcPriorityClass;
                return m_sResult;
            } catch( System.Exception e ) {
                if( m_oProc.HasExited == false ) {
                    m_oProc.Kill();
                    m_sResult = "Error: Hung process terminated ...\r\n"+e.Message+"\r\n";
                    return m_sResult;
                }
            } finally {
                m_oProc.Close();
                m_oProc.Dispose();                
            }

            return m_sResult;
        }
    }
}
