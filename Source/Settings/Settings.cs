/*
 * Created by SharpDevelop.
 * User: DikBSD
 * Date: 07.04.2009
 * Time: 22:18
 * 
 * License: GPL 2.1
 */
using System;
using System.Collections.Generic;
using System.Xml;
using System.IO;

namespace Settings
{
	/// <summary>
	/// Description of Settings.
	/// </summary>
	public class Settings
	{
		#region Закрытые статические члены-данные класса
		private static string m_sProgDir = Environment.CurrentDirectory;
		private static string m_settings = GetProgDir()+"\\settings.xml";
		private static string m_sTFB2Path = "c:\\WINDOWS\\NOTEPAD.EXE";
		private static string m_sWinRarPath = "c:\\Program Files\\WinRAR\\WinRAR.exe";
		private static string m_sRarPath = "c:\\Program Files\\WinRAR\\Rar.exe";
		private static string m_sFBEPath = "c:\\Program Files\\FictionBook Editor\\FBE.exe";
		private static string m_sFBReaderPath = "c:\\Program Files\\AlReader 2\\AlReader2.exe";
		#endregion
		
		#region Открытые статические члены-данные класса
		public static void SetProgDir( string sProgDir ) {
			m_sProgDir = sProgDir;
		}
		
		public static string GetProgDir() {
			return m_sProgDir;
		}
		
		public static string GetSettingsPath() {
			return m_settings;
		}
		
		public static string GetDefTFB2Path() {
			return m_sTFB2Path;
		}
		
		public static string GetDefWinRARPath() {
			return m_sWinRarPath;
		}
		
		public static string GetDefRarPath() {
			return m_sRarPath;
		}
		
		public static string GetDefFBEPath() {
			return m_sFBEPath;
		}
		
		public static string GetDefFBReaderPath() {
			return m_sFBReaderPath;
		}
		
		public static string ReadTextFB2EPath() {
			// читаем путь к текстовому редактору из настроек
			string sTFB2Path = GetDefTFB2Path();
			if( File.Exists( GetSettingsPath() ) ) {
				XmlReaderSettings settings = new XmlReaderSettings();
				settings.IgnoreWhitespace = true;
				using ( XmlReader reader = XmlReader.Create( GetSettingsPath(), settings ) ) {
					reader.ReadToFollowing("Editors");
					sTFB2Path = reader.GetAttribute("TextFB2EPath");
					reader.Close();
				}
			}
			return sTFB2Path;
		}
		
		public static string ReadFBEPath() {
			// читаем путь к FBE из настроек
			string sFBEPath = GetDefFBEPath();
			if( File.Exists( GetSettingsPath() ) ) {
				XmlReaderSettings settings = new XmlReaderSettings();
				settings.IgnoreWhitespace = true;
				using ( XmlReader reader = XmlReader.Create( GetSettingsPath(), settings ) ) {
					reader.ReadToFollowing("Editors");
					sFBEPath = reader.GetAttribute("FBEPath");
					reader.Close();
				}
			}
			return sFBEPath;
		}
		
		public static string ReadFBReaderPath() {
			// читаем путь к читалке из настроек
			string sFBReaderPath = GetDefFBReaderPath();
			if( File.Exists( GetSettingsPath() ) ) {
				XmlReaderSettings settings = new XmlReaderSettings();
				settings.IgnoreWhitespace = true;
				using ( XmlReader reader = XmlReader.Create( GetSettingsPath(), settings ) ) {
					reader.ReadToFollowing("Reader");
					sFBReaderPath = reader.GetAttribute("FBReaderPath");
					reader.Close();
				}
			}
			return sFBReaderPath;
		}
		
		public static string ReadWinRARPath() {
			// читаем путь к WinRar из настроек
			string sWinRarPath = GetDefWinRARPath();
			if( File.Exists( GetSettingsPath() ) ) {
				XmlReaderSettings settings = new XmlReaderSettings();
				settings.IgnoreWhitespace = true;
				using ( XmlReader reader = XmlReader.Create( GetSettingsPath(), settings ) ) {
					reader.ReadToFollowing("WinRar");
					sWinRarPath = reader.GetAttribute("WinRarPath");
					reader.Close();
				}
			}
			return sWinRarPath;
		}
		
		public static string ReadRarPath() {
			// читаем путь к консольному Rar из настроек
			string sRarPath = GetDefRarPath();
			if( File.Exists( GetSettingsPath() ) ) {
				XmlReaderSettings settings = new XmlReaderSettings();
				settings.IgnoreWhitespace = true;
				using ( XmlReader reader = XmlReader.Create( GetSettingsPath(), settings ) ) {
					reader.ReadToFollowing("WinRar");
					sRarPath = reader.GetAttribute("RarPath");
					reader.Close();
				}
			}
			return sRarPath;
		}
		#endregion
	}
}