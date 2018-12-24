/*
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

namespace Settings
{
	/// <summary>
	/// Класс по работе с общими настройками всех инструментов
	/// </summary>
	public static class Settings
	{
		#region Закрытые статические члены-данные класса
		private readonly static XmlDocument m_xmlDoc = new XmlDocument();
		
		#region Общее Настройки
		private 		 static string m_sProgDir 	= Environment.CurrentDirectory;
		private readonly static string m_sTempDir 	= Path.Combine(Path.GetTempPath(),"Temp"); // Путь к временной папке текущего пользователя
		private readonly static string m_settings 			= Path.Combine(ProgDir, "settings.xml");
		private readonly static string m_sLicensePath		= Path.Combine(ProgDir, "License GPL 2.1.rtf");
		private readonly static string m_sChangeFilePath	= Path.Combine(ProgDir, "Change.rtf");
		private readonly static string m_SchemePath			= Path.Combine(ProgDir, "FictionBook.xsd");
		private readonly static string m_sTFB2Path			= @"c:\Windows\Notepad.exe";
		private readonly static string m_sFBEPath			= @"c:\Program Files\FictionBook Editor\FBE.exe";
		private readonly static string m_sFBReaderPath		= @"c:\Program Files\AlReader 2\AlReader2.exe";
		private 		 static string m_sDiffPath			= string.Empty;
		private readonly static string m_sReady				= "Готово.";
		private readonly static bool m_ConfirmationForExit	= true;
		private readonly static bool m_ShowDebugMessage		= true;
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
		
		#region Общее Настройки
		public static string ProgDir {
			get { return m_sProgDir; }
			set { m_sProgDir = value; }
		}
		
		// возвращает временную папку
		public static string TempDirPath {
			get {
				if ( File.Exists( m_settings ) ) {
					m_xmlDoc.Load( m_settings );
					XmlNode node = m_xmlDoc.SelectSingleNode("Settings/General/TempDirPath");
					if ( node != null )
						return node.InnerText;
				}
				return m_sTempDir;
			}
		}
		
		public static string TextEditorPath {
			get {
				if ( File.Exists( m_settings ) ) {
					m_xmlDoc.Load( m_settings );
					XmlNode node = m_xmlDoc.SelectSingleNode("Settings/General/TextFB2EPath");
					if ( node != null )
						return node.InnerText;
				}
				return m_sTFB2Path;
			}
		}

		public static string FB2EditorPath {
			get {
				if ( File.Exists( m_settings ) ) {
					m_xmlDoc.Load( m_settings );
					XmlNode node = m_xmlDoc.SelectSingleNode("Settings/General/FBEPath");
					if ( node != null )
						return node.InnerText;
				}
				return m_sFBEPath;
			}
		}

		public static string FBReaderPath {
			get {
				if ( File.Exists( m_settings ) ) {
					m_xmlDoc.Load( m_settings );
					XmlNode node = m_xmlDoc.SelectSingleNode("Settings/General/FBReaderPath");
					if ( node != null )
						return node.InnerText;
				}
				return m_sFBReaderPath;
			}
		}
		
		public static string DiffToolPath {
			get {
				if ( File.Exists( m_settings ) ) {
					m_xmlDoc.Load( m_settings );
					XmlNode node = m_xmlDoc.SelectSingleNode("Settings/General/DiffPath");
					if ( node != null )
						return node.InnerText;
				}
				return m_sDiffPath;
			}
		}
		
		public static bool ConfirmationForExit {
			get {
				if ( File.Exists( m_settings ) ) {
					m_xmlDoc.Load( m_settings );
					XmlNode node = m_xmlDoc.SelectSingleNode("Settings/General/ConfirmationForAppExit");
					if ( node != null )
						return Convert.ToBoolean( node.InnerText );
				}
				return m_ConfirmationForExit;
			}
		}
		
		public static bool ShowDebugMessage {
			get {
				if ( File.Exists( m_settings ) ) {
					m_xmlDoc.Load( m_settings );
					XmlNode node = m_xmlDoc.SelectSingleNode("Settings/General/ShowDebugMessage");
					if ( node != null )
						return Convert.ToBoolean( node.InnerText );
				}
				return m_ShowDebugMessage;
			}
		}
		
		// возвращает путь к объединенной схеме
		public static string SchemePath {
			get { return m_SchemePath; }
		}
		
		public static string SettingsPath {
			get { return m_settings; }
		}
		
		public static string LicensePath {
			get { return m_sLicensePath; }
		}

		public static string ChangeFilePath {
			get { return m_sChangeFilePath; }
		}
		
		#endregion
		
		#region Общие Сообщения
		public static string GetReady() {
			return m_sReady;
		}
		#endregion
		
		#region Стили ToolButtons
		// задание для кнопок ToolStrip стиля и положения текста и картинки
		public static void SetToolButtonsSettings( string sNodeName, string sAttrDS, string sAttrTIR, ToolStrip ts ) {
			// чтение настроек для ToolButtons из xml-файла
			if ( File.Exists( SettingsPath ) ) {
				m_xmlDoc.Load( SettingsPath );
				XmlNode node = m_xmlDoc.SelectSingleNode(sNodeName);
				if ( node != null ) {
					string sDS = string.Empty, sTIR = string.Empty;
					XmlAttributeCollection attrs = node.Attributes;
					sDS = attrs.GetNamedItem(sAttrDS).InnerText;
					sTIR = attrs.GetNamedItem(sAttrTIR).InnerText;
					if ( sDS.Length != 0 ) {
						for ( int i = 0; i != ts.Items.Count; ++i ) {
							ts.Items[i].DisplayStyle		= (ToolStripItemDisplayStyle)GetToolButtonDisplayStyle( sDS );
							ts.Items[i].TextImageRelation	= (TextImageRelation)GetToolButtonTextImageRelation( sTIR );
						}
					}
				} /*else
					MessageBox.Show( "Поврежден файл настроек: \""+SettingsPath+"\".\nУдалите его, он создастся автоматически при сохранении настроек", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );*/
			}
		}

		#endregion
		
		#endregion

	}
}