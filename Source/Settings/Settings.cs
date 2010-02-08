﻿/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 07.04.2009
 * Time: 22:18
 * 
 * License: GPL 2.1
 */
using System;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Collections.Generic;

namespace Settings
{
	/// <summary>
	/// Description of Settings.
	/// </summary>
	public class Settings
	{
		#region Закрытые статические члены-данные класса
		
		#region Общее Настройки
		private static string m_sProgDir = Environment.CurrentDirectory;
		private static string m_sTempDir = GetProgDir()+"\\Temp"; // временный каталог
		private static string m_settings = GetProgDir()+"\\settings.xml";
		private static string m_sFB21SchemePath = GetProgDir()+"\\FictionBook.xsd";
		private static string m_sTFB2Path	= "c:\\WINDOWS\\NOTEPAD.EXE";
		private static string m_sWinRarPath	= "c:\\Program Files\\WinRAR\\WinRAR.exe";
		private static string m_sRarPath	= "c:\\Program Files\\WinRAR\\Rar.exe";
		private static string m_sUnRARPath	= GetProgDir()+"\\UnRAR.exe";
		private static string m_s7zaPath	= GetProgDir()+"\\7za.exe";		
		private static string m_sFBEPath	= "c:\\Program Files\\FictionBook Editor\\FBE.exe";
		private static string m_sFBReaderPath	= "c:\\Program Files\\AlReader 2\\AlReader2.exe";
		private static string m_sDiffPath		= "";
		
		private static string m_sLicensePath	= GetProgDir()+"\\License GPL 2.1.rtf";
		private static string m_sChangeFilePath	= GetProgDir()+"\\Change.rtf";
		//
		private static string m_sWorksDataSettingsPath = Settings.GetProgDir()+"\\SharpFBToolsWorksData.xml";
		#endregion
		
		#region Общие Сообщения
		private static string m_sReady	= "Готово.";
		private static string m_sNoID	= "Id_Нет";
		#endregion
		
		#region Стили ToolButtons
		// получение стиля ToolButton
		private static ToolStripItemDisplayStyle GetToolButtonDisplayStyle( string DisplayStyle ) {
			switch( DisplayStyle ) {
				case "Image":
					return ToolStripItemDisplayStyle.Image;
				case "Text":
					return ToolStripItemDisplayStyle.Text;
				case "ImageAndText":
					return ToolStripItemDisplayStyle.ImageAndText;
				default:
					return ToolStripItemDisplayStyle.ImageAndText;
			}
		}
		
		// получение TextImageRelation ToolButton
		private static TextImageRelation GetToolButtonTextImageRelation( string sTextImageRelation ) {
			switch( sTextImageRelation ) {
				case "Overlay":
					return TextImageRelation.Overlay;
				case "ImageAboveText":
					return TextImageRelation.ImageAboveText;
				case "TextAboveImage":
					return TextImageRelation.TextAboveImage;
				case "ImageBeforeText":
					return TextImageRelation.ImageBeforeText;
				case "TextBeforeImage":
					return TextImageRelation.TextBeforeImage;
				default:
					return TextImageRelation.ImageBeforeText;
			}
		}
		#endregion
		
		#endregion

		#region Открытые статические методы класса
		
		#region Чтение Атрибутов xml-файла
		public static string ReadAttribute( string sTag, string sAttr, string sAttrDefValue ) {
			// читаем атрибут тега из настроек
			string sAttrValue = sAttrDefValue;
			if( File.Exists( GetSettingsPath() ) ) {
				XmlReaderSettings settings = new XmlReaderSettings();
				settings.IgnoreWhitespace = true;
				using ( XmlReader reader = XmlReader.Create( GetSettingsPath(), settings ) ) {
					try {
						reader.ReadToFollowing(sTag);
						if ( reader.HasAttributes ) {
							string s = reader.GetAttribute( sAttr );
							if( s != null ) {
								sAttrValue = s;
							}
						}
					} catch {
						
					} finally {
						reader.Close();
					}
				}
			}
			return sAttrValue;
		}
		public static Int16 ReadAttribute( string sTag, string sAttr, Int16 nAttrDefValue ) {
			// читаем атрибут тега из настроек
			Int16 nAttrValue = nAttrDefValue;
			if( File.Exists( GetSettingsPath() ) ) {
				XmlReaderSettings settings = new XmlReaderSettings();
				settings.IgnoreWhitespace = true;
				using ( XmlReader reader = XmlReader.Create( GetSettingsPath(), settings ) ) {
					try {
						reader.ReadToFollowing(sTag);
						if ( reader.HasAttributes ) {
							string s = reader.GetAttribute( sAttr );
							if( s != null ) {
								nAttrValue = Convert.ToInt16( s );
							}
						}
					} catch {
						
					} finally {
						reader.Close();
					}
				}
			}
			return nAttrValue;
		}
		public static bool ReadAttribute( string sTag, string sAttr, bool bAttrDefValue ) {
			// читаем атрибут тега из настроек
			bool bAttrValue = bAttrDefValue;
			if( File.Exists( GetSettingsPath() ) ) {
				XmlReaderSettings settings = new XmlReaderSettings();
				settings.IgnoreWhitespace = true;
				using ( XmlReader reader = XmlReader.Create( GetSettingsPath(), settings ) ) {
					try {
						reader.ReadToFollowing(sTag);
						if ( reader.HasAttributes ) {
							string s = reader.GetAttribute( sAttr );
							if( s != null ) {
								bAttrValue = Convert.ToBoolean( s );
							}
						}
					} catch {
						
					} finally {
						reader.Close();
					}
				}
			}
			return bAttrValue;
		}
		#endregion		
		
		#region Общее Настройки
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
		
		public static string GetDefUnRARPath() {
			return m_sUnRARPath;
		}		
		
		public static string GetDef7zaPath() {
			return m_s7zaPath;
		}
		
		public static string GetDefFBEPath() {
			return m_sFBEPath;
		}
		
		public static string GetDefFBReaderPath() {
			return m_sFBReaderPath;
		}
		
		public static string GetDiffPath() {
			return m_sDiffPath;
		}
		
		public static string GetLicensePath() {
			return m_sLicensePath;
		}

		public static string GetChangeFilePath() {
			return m_sChangeFilePath;
		}

		////// Чтение из файла настроек данных по конкретному параметру //
		
		public static string ReadWinRARPath() {
			// читаем путь к WinRar из настроек
			return ReadAttribute( "WinRar", "WinRarPath", GetDefWinRARPath() );
		}
		public static string ReadRarPath() {
			// читаем путь к консольному Rar из настроек
			return ReadAttribute( "WinRar", "RarPath", GetDefRarPath() );
		}
		public static string ReadUnRarPath() {
			// читаем путь к консольному UnRar из настроек
			return ReadAttribute( "WinRar", "UnRarPath", GetDefUnRARPath() );
		}
		public static string Read7zaPath() {
			// читаем путь к консольному 7za из настроек
			return ReadAttribute( "A7za", "A7zaPath", GetDef7zaPath() );
		}
		public static string ReadTextFB2EPath() {
			// читаем путь к текстовому редактору из настроек
			return ReadAttribute( "Editors", "TextFB2EPath", GetDefTFB2Path() );
		}
		public static string ReadFBEPath() {
			// читаем путь к FBE из настроек
			return ReadAttribute( "Editors", "FBEPath", GetDefFBEPath() );
		}
		public static string ReadFBReaderPath() {
			// читаем путь к читалке из настроек
			return ReadAttribute( "Reader", "FBReaderPath", GetDefFBReaderPath() );
		}
		public static string ReadDiffPath() {
			// читаем путь к читалке из настроек
			return ReadAttribute( "Diff", "DiffPath", GetDiffPath() );
		}
		public static bool ReadConfirmationForExit() {
			// читаем путь для подтверждения выхода из программы
			return ReadAttribute( "ConfirmationForExit", "ConfirmationForExit", true );
		}
		#endregion
		
		#region Общие Сообщения
		public static string GetReady() {
			return m_sReady;
		}
		
		public static string GetNoID() {
			return m_sNoID;
		}
		#endregion

		#region Открытые статические общие свойства класса
		public static string WorksDataSettingsPath {
			get { return m_sWorksDataSettingsPath; }
		}
		#endregion
		
		#region Сохранение рабочих настроек (папки...) всех инструментов в xml-файл
		public static void WriteSharpFBToolsWorksData() {
			XmlWriter writer = null;
			try {
				XmlWriterSettings settings = new XmlWriterSettings();
				settings.Indent = true;
				settings.IndentChars = ("\t");
				settings.OmitXmlDeclaration = true;
				
				writer = XmlWriter.Create( WorksDataSettingsPath, settings );
				writer.WriteStartElement( "SharpFBTools" );
					writer.WriteStartElement( "FB2Validator" );
						writer.WriteStartElement( "VScanDir" );
							writer.WriteAttributeString( "tboxSourceDir", SettingsValidator.ScanDir );
						writer.WriteFullEndElement();
						writer.WriteStartElement( "VNotValidFB2Files" );
							writer.WriteAttributeString( "tboxFB2NotValidDirCopyTo", SettingsValidator.FB2NotValidDirCopyTo );
							writer.WriteAttributeString( "tboxFB2NotValidDirMoveTo", SettingsValidator.FB2NotValidDirMoveTo );
						writer.WriteFullEndElement();
						writer.WriteStartElement( "VValidFB2Files" );
							writer.WriteAttributeString( "tboxFB2ValidDirCopyTo", SettingsValidator.FB2ValidDirCopyTo );
							writer.WriteAttributeString( "tboxFB2ValidDirMoveTo", SettingsValidator.FB2ValidDirMoveTo );
						writer.WriteFullEndElement();
						writer.WriteStartElement( "VNotFB2Files" );
							writer.WriteAttributeString( "tboxNotFB2DirCopyTo", SettingsValidator.NotFB2DirCopyTo );
							writer.WriteAttributeString( "tboxNotFB2DirMoveTo", SettingsValidator.NotFB2DirMoveTo );
						writer.WriteFullEndElement();
					writer.WriteEndElement();
					
					writer.WriteStartElement( "ArchiveManager" );
						writer.WriteStartElement( "AMScanDirForArchive" );
							writer.WriteAttributeString( "tboxSourceDir", SettingsAM.AMAScanDir );
						writer.WriteFullEndElement();
						writer.WriteStartElement( "AMTargetDirForArchive" );
							writer.WriteAttributeString( "tboxToAnotherDir", SettingsAM.AMATargetDir );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "AMScanDirForUnArchive" );
							writer.WriteAttributeString( "tboxUASourceDir", SettingsAM.AMUAScanDir );
						writer.WriteFullEndElement();
						writer.WriteStartElement( "AMTargetDirForUnArchive" );
							writer.WriteAttributeString( "tboxUAToAnotherDir", SettingsAM.AMAUATargetDir );
						writer.WriteFullEndElement();
					writer.WriteEndElement();
					
					writer.WriteStartElement( "FileManager" );
						writer.WriteStartElement( "FMScanDir" );
							writer.WriteAttributeString( "tboxSourceDir", SettingsFM.FMDataScanDir );
						writer.WriteFullEndElement();
						writer.WriteStartElement( "FMTargetDir" );
							writer.WriteAttributeString( "tboxSortAllToDir", SettingsFM.FMDataTargetDir );
						writer.WriteFullEndElement();
						writer.WriteStartElement( "FMTemplate" );
							writer.WriteAttributeString( "txtBoxTemplatesFromLine", SettingsFM.FMDataTemplate );
						writer.WriteFullEndElement();

						writer.WriteStartElement( "FMSSScanDir" );
							writer.WriteAttributeString( "tboxSSSourceDir", SettingsFM.FMDataSSScanDir );
						writer.WriteFullEndElement();
						writer.WriteStartElement( "FMSSTargetDir" );
							writer.WriteAttributeString( "tboxSSToDir", SettingsFM.FMDataSSTargetDir );
						writer.WriteFullEndElement();
						writer.WriteStartElement( "FMSSTemplate" );
							writer.WriteAttributeString( "txtBoxSSTemplatesFromLine", SettingsFM.FMDataSSTemplate );
						writer.WriteFullEndElement();
					writer.WriteEndElement();
					
					writer.WriteStartElement( "FB2DuplicatesSearcher" );
						writer.WriteStartElement( "FB2DupScanDir" );
							writer.WriteAttributeString( "tboxSourceDir", SettingsFB2Dup.DupScanDir );
						writer.WriteFullEndElement();
						writer.WriteStartElement( "FB2DupToDir" );
							writer.WriteAttributeString( "tboxDupToDir", SettingsFB2Dup.DupToDir );
						writer.WriteFullEndElement();
					writer.WriteEndElement();
					
				writer.WriteEndElement();
				writer.Flush();
			}  finally  {
				if (writer != null)
				writer.Close();
			}
		}
		#endregion
	
		#region Стили ToolButtons
		// задание для кнопок ToolStrip стиля и положения текста и картинки
		public static void SetToolButtonsSettings( string sNodeName, string sAttrDS, string sAttrTIR, ToolStrip ts ) {
			// чтение настроек для ToolButtons из xml-файла
			string sSettings = GetSettingsPath();
			if( !File.Exists( sSettings ) ) return;
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.IgnoreWhitespace = true;
			string sDS = "", sTIR = "";
			using ( XmlReader reader = XmlReader.Create( sSettings, settings ) ) {
				try {
					reader.ReadToFollowing( sNodeName );
					if (reader.HasAttributes ) {
						sDS = reader.GetAttribute( sAttrDS );
						sTIR = reader.GetAttribute( sAttrTIR );
					}
				} catch {
					MessageBox.Show( "Поврежден файл настроек: \""+GetSettingsPath()+"\".\nУдалите его, он создастся автоматически при сохранении настроек", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				} finally {
					reader.Close();
				}
			}
			if( sDS.Length!=0 ) {
				for( int i=0; i!=ts.Items.Count; ++i ) {
					ts.Items[i].DisplayStyle		= (ToolStripItemDisplayStyle)GetToolButtonDisplayStyle( sDS );
					ts.Items[i].TextImageRelation	= (TextImageRelation)GetToolButtonTextImageRelation( sTIR );
				}
			}
		}

		#endregion
		
		#endregion

	}
}