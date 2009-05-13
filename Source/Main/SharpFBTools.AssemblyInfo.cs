/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 09.04.2009
 * Time: 14:13
 * 
 * License: GPL 2.1
 */
using System;

namespace SharpFBTools.AssemblyInfo
{
	/// <summary>
	/// Description of SharpFBTools_AssemblyInfo.
	/// </summary>
	public class SharpFBTools_AssemblyInfo
	{
		public SharpFBTools_AssemblyInfo()
		{
		}
		
		private static string m_sAssemblyTitle			= "";
		private static string m_sAssemblyProduct		= "";
		private static string m_sAssemblyVersion		= "";
		private static string m_sAssemblyCopyright		= "";
		private static string m_sAssemblyCompany		= "";
		private static string m_sAssemblyDescription	= "";
		
		public static void SetAssemblyTitle( string sAssemblyTitle ) {
			m_sAssemblyTitle = sAssemblyTitle;
		}
		
		public static void SetAssemblyProduct( string sAssemblyProduct ) {
			m_sAssemblyProduct = sAssemblyProduct;
		}
		
		public static void SetAssemblyVersion( string sAssemblyVersion ) {
			m_sAssemblyVersion = sAssemblyVersion;
		}
		
		public static void SetAssemblyCopyright( string sAssemblyCopyright ) {
			m_sAssemblyCopyright = sAssemblyCopyright;
		}
		
		public static void SetAssemblyCompany( string sAssemblyCompany ) {
			m_sAssemblyCompany = sAssemblyCompany;
		}
		
		public static void SetAssemblyDescription( string sAssemblyDescription ) {
			m_sAssemblyDescription = sAssemblyDescription;
		}
		
		public static string GetAssemblyTitle() {
			return m_sAssemblyTitle;
		}
		
		public static string GetAssemblyProduct() {
			return m_sAssemblyProduct;
		}
		
		public static string GetAssemblyVersion() {
			return m_sAssemblyVersion;
		}
		
		public static string GetAssemblyCopyright() {
			return m_sAssemblyCopyright;
		}
		
		public static string GetAssemblyCompany() {
			return m_sAssemblyCompany;
		}
		
		public static string GetAssemblyDescription() {
			return m_sAssemblyDescription;
		}
		
	}
}
