/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.06.2012
 * Time: 9:12
 * 
 * License: GPL 2.1
 */
using System;
using System.Xml;
using System.IO;

namespace Settings
{
	/// <summary>
	/// FileManagerSettings: класс для работы с сохранением в xml и чтением настроек Сортировщика
	/// </summary>
	public class FileManagerSettings
	{
		#region Закрытые статические данные класса
		private static string m_FileSettingsPath = Settings.GetProgDir()+@"\FileManagerSettings.xml";
		private static XmlDocument m_xmlDoc = new XmlDocument();
		// Общие
		
		// рабочие папки и данные для Полной Сортировки
		private static string m_FullSortingSourceDir		= "";
		private static string m_FullSortingTemplate			= "";
		private static bool m_FullSortingInSubDir			= true;
		private static bool m_ViewMessageForLongTime		= true;
		private static bool m_BooksTagsView					= false;
		private static bool m_StartExplorerColumnsAutoReize	= false;
		private static bool m_FullSortingToZip				= false;
		private static bool m_FullSortingDelFB2Files		= false;
		
		// рабочие папки и данные для Избранной Сортировки
		private static string m_SelectedSortingSourceDir	= "";
		private static string m_SelectedSortingTargetDir	= "";
		private static string m_SelectedSortingTemplate		= "";
		private static bool m_SelectedSortingInSubDir		= true;
		private static bool m_SelectedSortingToZip			= false;
		private static bool m_SelectedSortingDelFB2Files		= false;
		#endregion
		
		public FileManagerSettings()
		{
		}
		
		public static void WriteFileManagerSettings() {
			#region Сохранение настроек в zml-файл
			XmlWriter writer = null;
			try {
				XmlWriterSettings settings = new XmlWriterSettings();
				settings.Indent = true;
				settings.IndentChars = ("\t");
				settings.OmitXmlDeclaration = true;
				
				writer = XmlWriter.Create( m_FileSettingsPath, settings );
				writer.WriteStartElement( "FileManager" );
				
				// Общие основные настройки
				
				// Полная Сортировка
				writer.WriteStartElement( "FullSorting" );
				writer.WriteElementString( "SourceDir", FullSortingSourceDir );
				writer.WriteElementString( "Template", FullSortingTemplate );
				writer.WriteElementString( "SortingInSubDir", Convert.ToString(FullSortingInSubDir) );
				writer.WriteElementString( "BooksTagsView", Convert.ToString(BooksTagsView) );
				writer.WriteElementString( "ViewMessageForLongTime", Convert.ToString(ViewMessageForLongTime) );
				writer.WriteElementString( "StartExplorerColumnsAutoReize", Convert.ToString(StartExplorerColumnsAutoReize) );
				writer.WriteElementString( "ToZip", Convert.ToString(FullSortingToZip) );
				writer.WriteElementString( "DelFB2Files", Convert.ToString(FullSortingDelFB2Files) );
				writer.WriteEndElement();

				// Избранная Сортировка
				writer.WriteStartElement( "SelectedSorting" );
				writer.WriteElementString( "SourceDir", SelectedSortingSourceDir );
				writer.WriteElementString( "TargetDir", SelectedSortingTargetDir );
				writer.WriteElementString( "Template", SelectedSortingTemplate );
				writer.WriteElementString( "SortingInSubDir", Convert.ToString(SelectedSortingInSubDir) );
				writer.WriteElementString( "ToZip", Convert.ToString(SelectedSortingToZip) );
				writer.WriteElementString( "DelFB2Files", Convert.ToString(SelectedSortingDelFB2Files) );
				writer.WriteEndElement();
				
				writer.WriteEndElement();
				writer.Flush();
			}  finally  {
				if (writer != null)
					writer.Close();
			}
			#endregion
		}
		
		#region Открытые статические общие свойства класса
		public static string FileManagerSettingsPath {
			get { return m_FileSettingsPath; }
		}
		#endregion
		
		#region Открытые статические свойства класса для данных Сортировок
		// Общие основные настройки
		public static bool BooksTagsView {
			get { return m_BooksTagsView; }
			set { m_BooksTagsView = value; }
		}
		// Полная Сортировка
		public static string FullSortingSourceDir {
			get { return m_FullSortingSourceDir; }
			set { m_FullSortingSourceDir = value; }
		}
		public static string FullSortingTemplate {
			get { return m_FullSortingTemplate; }
			set { m_FullSortingTemplate = value; }
		}
		public static bool FullSortingInSubDir {
			get { return m_FullSortingInSubDir; }
			set { m_FullSortingInSubDir = value; }
		}
		public static bool ViewMessageForLongTime {
			get { return m_ViewMessageForLongTime; }
			set { m_ViewMessageForLongTime = value; }
		}
		
		public static bool StartExplorerColumnsAutoReize {
			get { return m_StartExplorerColumnsAutoReize; }
			set { m_StartExplorerColumnsAutoReize = value; }
		}
		public static bool FullSortingToZip {
			get { return m_FullSortingToZip; }
			set { m_FullSortingToZip = value; }
		}
		public static bool FullSortingDelFB2Files {
			get { return m_FullSortingDelFB2Files; }
			set { m_FullSortingDelFB2Files = value; }
		}
		
		// Избранная Сортировка
		public static string SelectedSortingSourceDir {
			get { return m_SelectedSortingSourceDir; }
			set { m_SelectedSortingSourceDir = value; }
		}
		public static string SelectedSortingTargetDir {
			get { return m_SelectedSortingTargetDir; }
			set { m_SelectedSortingTargetDir = value; }
		}
		public static string SelectedSortingTemplate {
			get { return m_SelectedSortingTemplate; }
			set { m_SelectedSortingTemplate = value; }
		}
		public static bool SelectedSortingInSubDir {
			get { return m_SelectedSortingInSubDir; }
			set { m_SelectedSortingInSubDir = value; }
		}
		public static bool SelectedSortingToZip {
			get { return m_SelectedSortingToZip; }
			set { m_SelectedSortingToZip = value; }
		}
		public static bool SelectedSortingDelFB2Files {
			get { return m_SelectedSortingDelFB2Files; }
			set { m_SelectedSortingDelFB2Files = value; }
		}
		#endregion
		
		#region Открытые статические методы класса для чтения из xml настроек Полной Сортировки
		public static string ReadXmlFullSortingSourceDir() {
			/// <summary>
			/// чтение FullSortingSourceDir из xml-файла
			/// </summary>
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FileManager/FullSorting/SourceDir");
				if(node != null)
					return FullSortingSourceDir = node.InnerText.Trim();
			}
			return FullSortingSourceDir;
		}
		
		public static string ReadXmlFullSortingTemplate() {
			// чтение FullSortingTemplate из xml-файла
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FileManager/FullSorting/Template");
				if(node != null)
					return FullSortingTemplate = node.InnerText.Trim();
			}
			return FullSortingTemplate;
		}
		
		public static bool ReadXmlFullSortingInSubDir() {
			// чтение FullSortingInSubDir из xml-файла
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FileManager/FullSorting/SortingInSubDir");
				if(node != null)
					return FullSortingInSubDir = Convert.ToBoolean(node.InnerText);
			}
			return FullSortingInSubDir;
		}
		
		public static bool ReadXmlFullSortingBooksTagsView() {
			// чтение BooksTagsView из xml-файла
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FileManager/FullSorting/BooksTagsView");
				if(node != null)
					return BooksTagsView = Convert.ToBoolean(node.InnerText);
			}
			return BooksTagsView;
		}
		
		public static bool ReadXmlFullSortingViewMessageForLongTime() {
			// чтение ViewMessageForLongTime из xml-файла
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FileManager/FullSorting/ViewMessageForLongTime");
				if(node != null)
					return ViewMessageForLongTime = Convert.ToBoolean(node.InnerText);
			}
			return ViewMessageForLongTime;
		}
		public static bool ReadXmlFullSortingStartExplorerColumnsAutoReize() {
			// чтение StartExplorrerColumnsAutoResize из xml-файла
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FileManager/FullSorting/StartExplorerColumnsAutoReize");
				if(node != null)
					return StartExplorerColumnsAutoReize = Convert.ToBoolean(node.InnerText);
			}
			return StartExplorerColumnsAutoReize;
		}
		public static bool ReadXmlFullSortingToZip() {
			// чтение FullSortingToZip из xml-файла
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FileManager/FullSorting/ToZip");
				if(node != null)
					return FullSortingToZip = Convert.ToBoolean(node.InnerText);
			}
			return FullSortingToZip;
		}
		public static bool ReadXmlFullSortingDelFB2Files() {
			// чтение FullSortingDelFB2Files из xml-файла
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FileManager/FullSorting/DelFB2Files");
				if(node != null)
					return FullSortingDelFB2Files = Convert.ToBoolean(node.InnerText);
			}
			return FullSortingDelFB2Files;
		}
		#endregion
		
		#region Открытые статические методы класса для чтения из xml настроек Избранной Сортировки
		public static string ReadXmlSelectedSortingSourceDir() {
			// чтение SelectedSortingSourceDir из xml-файла
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FileManager/SelectedSorting/SourceDir");
				if(node != null)
					return SelectedSortingSourceDir = node.InnerText.Trim();
			}
			return SelectedSortingSourceDir;
		}
		
		public static string ReadXmlSelectedSortingTargetDir() {
			// чтение SelectedSortingTargetDir из xml-файла
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FileManager/SelectedSorting/TargetDir");
				if(node != null)
					return SelectedSortingTargetDir = node.InnerText.Trim();
			}
			return SelectedSortingTargetDir;
		}
		
		public static string ReadXmlSelectedSortingTemplate() {
			// чтение SelectedSortingTemplate из xml-файла
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FileManager/SelectedSorting/Template");
				if(node != null)
					return SelectedSortingTemplate = node.InnerText.Trim();
			}
			return SelectedSortingTemplate;
		}
		
		public static bool ReadXmlSelectedSortingInSubDir() {
			// чтение SelectedSortingInSubDir из xml-файла
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FileManager/SelectedSorting/SortingInSubDir");
				if(node != null)
					return SelectedSortingInSubDir = Convert.ToBoolean(node.InnerText);
			}
			return SelectedSortingInSubDir;
		}
		
		public static bool ReadXmlSelectedSortingToZip() {
			// чтение SelectedSortingToZip из xml-файла
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FileManager/SelectedSorting/ToZip");
				if(node != null)
					return SelectedSortingToZip = Convert.ToBoolean(node.InnerText);
			}
			return SelectedSortingToZip;
		}
		public static bool ReadXmlSelectedSortingDelFB2Files() {
			// чтение SelectedSortingDelFB2Files из xml-файла
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FileManager/SelectedSorting/DelFB2Files");
				if(node != null)
					return SelectedSortingDelFB2Files = Convert.ToBoolean(node.InnerText);
			}
			return SelectedSortingDelFB2Files;
		}
		#endregion
	}
}
		