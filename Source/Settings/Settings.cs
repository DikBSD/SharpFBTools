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
	public class Settings
	{
		#region Закрытые статические члены-данные класса
		private readonly static XmlDocument m_xmlDoc = new XmlDocument();
		
		#region Общее Настройки
		private 		 static string m_sProgDir 				= Environment.CurrentDirectory;
		private readonly static string m_sTempDir 				= ProgDir + "\\Temp"; // временный каталог
		private readonly static string m_settings 				= ProgDir + "\\settings.xml";
		private readonly static string m_sLicensePath			= ProgDir + "\\License GPL 2.1.rtf";
		private readonly static string m_sChangeFilePath		= ProgDir + "\\Change.rtf";
		private readonly static string m_SchemePath				= ProgDir + "\\FictionBook.xsd";
		private readonly static string m_sTFB2Path				= "c:\\WINDOWS\\NOTEPAD.EXE";
		private readonly static string m_sFBEPath				= "c:\\Program Files\\FictionBook Editor\\FBE.exe";
		private readonly static string m_sFBReaderPath			= "c:\\Program Files\\AlReader 2\\AlReader2.exe";
		private 		 static string m_sDiffPath				= string.Empty;
		#endregion
		
		#region Общие Сообщения
		private static string m_sReady	= "Готово.";
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
		public static string TempDir {
			get { return m_sTempDir; }
		}
		
		// возвращает путь к объединенной схеме
		public static string SchemePath {
			get { return m_SchemePath; }
		}
		
		public static string SettingsPath {
			get { return m_settings; }
		}
		
		public static string DefTFB2Path {
			get { return m_sTFB2Path; }
		}

		public static string DefFBEPath {
			get { return m_sFBEPath; }
		}

		public static string DefFBReaderPath {
			get { return m_sFBReaderPath; }
		}
		
		public static string DiffPath {
			get { return m_sDiffPath; }
		}
		
		public static string LicensePath {
			get { return m_sLicensePath; }
		}

		public static string ChangeFilePath {
			get { return m_sChangeFilePath; }
		}
		
		// =============================================================================================
		// 				Чтение из файла настроек данных по конкретному параметру
		// =============================================================================================
		public static string ReadFBEPath() {
			if( File.Exists( m_settings ) ) {
				m_xmlDoc.Load( m_settings );
				XmlNode node = m_xmlDoc.SelectSingleNode("Settings/General/FBEPath");
				if(node != null)
					return node.InnerText;
			}
			return DefFBEPath;
		}
		
		public static string ReadTextFB2EPath() {
			if( File.Exists( m_settings ) ) {
				m_xmlDoc.Load( m_settings );
				XmlNode node = m_xmlDoc.SelectSingleNode("Settings/General/TextFB2EPath");
				if(node != null)
					return node.InnerText;
			}
			return DefTFB2Path;
		}

		public static string ReadFBReaderPath() {
			if( File.Exists( m_settings ) ) {
				m_xmlDoc.Load( m_settings );
				XmlNode node = m_xmlDoc.SelectSingleNode("Settings/General/FBReaderPath");
				if(node != null)
					return node.InnerText;
			}
			return DefFBReaderPath;
		}

		public static string ReadDiffPath() {
			if( File.Exists( m_settings ) ) {
				m_xmlDoc.Load( m_settings );
				XmlNode node = m_xmlDoc.SelectSingleNode("Settings/General/DiffPath");
				if(node != null)
					return node.InnerText;
			}
			return DiffPath;
		}

		public static bool ReadConfirmationForExit() {
			if( File.Exists( m_settings ) ) {
				m_xmlDoc.Load( m_settings );
				XmlNode node = m_xmlDoc.SelectSingleNode("Settings/General/ConfirmationForAppExit");
				if(node != null)
					return Convert.ToBoolean( node.InnerText );
			}
			return true;
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
			if( File.Exists( SettingsPath ) ) {
				m_xmlDoc.Load( SettingsPath );
				XmlNode node = m_xmlDoc.SelectSingleNode(sNodeName);
				if(node != null) {
					string sDS = string.Empty, sTIR = string.Empty;
					XmlAttributeCollection attrs = node.Attributes;
					sDS = attrs.GetNamedItem(sAttrDS).InnerText;
					sTIR = attrs.GetNamedItem(sAttrTIR).InnerText;
					if( sDS.Length!=0 ) {
						for( int i=0; i!=ts.Items.Count; ++i ) {
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