/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 24.04.2009
 * Time: 13:11
 * 
 * License: GPL 2.1
 */
using System;

namespace FilesWorker
{
	/// <summary>
	/// Description of StrictFileName.
	/// </summary>
	public class StrictFileName
	{
		#region Закрытые данные класса
		private static string m_sStrictLetters = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 [](){}-_";
		#endregion
		
		public StrictFileName()
		{
		}
		
		#region Открытые методы класса
		public static string StrictString( string sString ) {
			// "строгое" значение строки
			string s = sString;
			if( sString.Trim()=="" ) {
				return sString;
			}
			string sStrict = "";
			for( int i=0; i!=s.Length; ++i ) {
				char ci = s[i];
				for( int j=0; j!=m_sStrictLetters.Length; ++j ) {
					char cj = m_sStrictLetters[j];
					if( ci==cj ) {
						sStrict += ci;
						break;
					}
				}
			}
			return sStrict;
		}
		#endregion
	}
}
