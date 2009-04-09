/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
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
		private static string m_sTempDir = GetProgDir()+"\\Temp"; // временный каталог
		private static string m_sFB21SchemePath = GetProgDir()+"\\FictionBook.xsd";
		private static string m_settings = GetProgDir()+"\\settings.xml";
		private static string m_sTFB2Path = "c:\\WINDOWS\\NOTEPAD.EXE";
		private static string m_sWinRarPath = "c:\\Program Files\\WinRAR\\WinRAR.exe";
		private static string m_sRarPath = "c:\\Program Files\\WinRAR\\Rar.exe";
		private static string m_sFBEPath = "c:\\Program Files\\FictionBook Editor\\FBE.exe";
		private static string m_sFBReaderPath = "c:\\Program Files\\AlReader 2\\AlReader2.exe";
		private static string m_sLicensePath = GetProgDir()+"\\License GPL 2.1.rtf";
		private static string m_s7zaPath = GetProgDir()+"\\7za.exe";
		private static string m_sUnRARPath = GetProgDir()+"\\UnRAR.exe";
		private static string m_sChangeFilePath = GetProgDir()+"\\Change.txt";
		private static string m_sFB2ValidatorHelpPath = GetProgDir()+"\\Help\\FB2ValidatorHelp.rtf";
		private static Int16 m_nValidatorForFB2SelectedIndex = 1;
		private static Int16 m_nValidatorForFB2ArchiveSelectedIndex = 1;
		#endregion
		
		#region Открытые статические члены-данные класса
		public static void SetProgDir( string sProgDir ) {
			m_sProgDir = sProgDir;
		}
		
		public static string GetProgDir() {
			return m_sProgDir;
		}
		
		public static string GetTempDir() {
			// возвращает временную папку
			return m_sTempDir;
		}
		
		public static string GetFB21SchemePath() {
			// возвращает путь к схеме fb2.1
			return m_sFB21SchemePath;
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
		
		public static string GetLicensePath() {
			return m_sLicensePath;
		}
		
		public static string Get7zaPath() {
			return m_s7zaPath;
		}
		
		public static string GetUnRARPath() {
			return m_sUnRARPath;
		}
		
		public static string GetChangeFilePath() {
			return m_sChangeFilePath;
		}
		
		public static string GetFB2ValidatorHelpPath() {
			return m_sFB2ValidatorHelpPath;
		}
		
		public static Int16 GetDefValidatorFB2SelectedIndex() {
			return m_nValidatorForFB2SelectedIndex;
		}
		
		public static Int16 GetDefValidatorFB2ArchiveSelectedIndex() {
			return m_nValidatorForFB2ArchiveSelectedIndex;
		}
		
		////// Чтение из файла настроек данных по конкретному параметру
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
		
		public static Int16 ReadValidatorFB2SelectedIndex() {
			// читаем номер выделенного итема для комбобокса cboxValidatorForFB2 из настроек
			string sSelectedIndex = GetDefValidatorFB2SelectedIndex().ToString();
			if( File.Exists( GetSettingsPath() ) ) {
				XmlReaderSettings settings = new XmlReaderSettings();
				settings.IgnoreWhitespace = true;
				using ( XmlReader reader = XmlReader.Create( GetSettingsPath(), settings ) ) {
					reader.ReadToFollowing("ValidatorDoubleClick");
					sSelectedIndex = reader.GetAttribute("cboxValidatorForFB2SelectedIndex");
					reader.Close();
				}
			}
			return Convert.ToInt16( sSelectedIndex );
		}
		
		public static Int16 ReadValidatorFB2ArchiveSelectedIndex() {
			// читаем номер выделенного итема для комбобокса cboxValidatorForFB2Archive из настроек
			string sSelectedIndex = GetDefValidatorFB2ArchiveSelectedIndex().ToString();
			if( File.Exists( GetSettingsPath() ) ) {
				XmlReaderSettings settings = new XmlReaderSettings();
				settings.IgnoreWhitespace = true;
				using ( XmlReader reader = XmlReader.Create( GetSettingsPath(), settings ) ) {
					reader.ReadToFollowing("ValidatorDoubleClick");
					sSelectedIndex = reader.GetAttribute("cboxValidatorForFB2ArchiveSelectedIndex");
					reader.Close();
				}
			}
			return Convert.ToInt16( sSelectedIndex );
		}
		#endregion
	}
}