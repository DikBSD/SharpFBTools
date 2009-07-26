using System;
using System.Diagnostics;
using System.Threading;

namespace FilesWorker
{
   
    public class CommandManager
    {
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
        public string Run( string sStartProgPath, string sArgs, ProcessPriorityClass  ppcPriorityClass )  {
            m_sResult = "";
            m_oProc = new Process();
            
            ProcessStartInfo oInfo;
            if( sArgs == null || sArgs == "" )
            	oInfo = new ProcessStartInfo( sStartProgPath );
            else oInfo = new ProcessStartInfo( sStartProgPath, sArgs );
            
			oInfo.UseShellExecute	= true;
			oInfo.CreateNoWindow	= true;
			oInfo.WindowStyle		= ProcessWindowStyle.Hidden;			

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
    }
}
