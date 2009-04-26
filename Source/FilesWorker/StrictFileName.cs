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
			const string sStrictLetters = "абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯabcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789 [](){}-_";
			string sStrict = "";
			for( int i=0; i!=s.Length; ++i ) {
				int nInd = sStrictLetters.IndexOf( s[i] );
				if( nInd!=-1 ) {
					sStrict += s[i];
				}
			}
			return sStrict;
		}
		#endregion
	}
}
