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
		// пути к файлам-справкам
		private static string m_sFileManagerHelpPath	= Settings.GetProgDir()+"\\Help\\FileManagerHelp.rtf";
		private static string m_sDescTemplatePath		= Settings.GetProgDir()+"\\Help\\TemplatesDescription.rtf";
		private static string m_FileSettingsPath		= Settings.GetProgDir()+@"\FileManagerSettings.xml";
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
		private static bool m_FullSortingNotDelFB2Files		= true;
		private static bool m_FullSortingFB2LibrusecGenres	= true;
		private static bool m_FullSortingFB22Genres			= false;
		
		// рабочие папки и данные для Избранной Сортировки
		private static string m_SelectedSortingSourceDir	= "";
		private static string m_SelectedSortingTargetDir	= "";
		private static string m_SelectedSortingTemplate		= "";
		private static bool m_SelectedSortingInSubDir		= true;
		private static bool m_SelectedSortingToZip			= false;
		private static bool m_SelectedSortingNotDelFB2Files	= true;
		private static bool m_SelectedSortingFB2LibrusecGenres	= true;
		private static bool m_SelectedSortingFB22Genres			= false;
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
				writer.WriteElementString( "NotDelFB2Files", Convert.ToString(FullSortingNotDelFB2Files) );
				writer.WriteElementString( "FB2LibrusecGenres", Convert.ToString(FullSortingFB2LibrusecGenres) );
				writer.WriteElementString( "FB22Genres", Convert.ToString(FullSortingFB22Genres) );
				writer.WriteEndElement();

				// Избранная Сортировка
				writer.WriteStartElement( "SelectedSorting" );
				writer.WriteElementString( "SourceDir", SelectedSortingSourceDir );
				writer.WriteElementString( "TargetDir", SelectedSortingTargetDir );
				writer.WriteElementString( "Template", SelectedSortingTemplate );
				writer.WriteElementString( "SortingInSubDir", Convert.ToString(SelectedSortingInSubDir) );
				writer.WriteElementString( "ToZip", Convert.ToString(SelectedSortingToZip) );
				writer.WriteElementString( "NotDelFB2Files", Convert.ToString(SelectedSortingNotDelFB2Files) );
				writer.WriteElementString( "FB2LibrusecGenres", Convert.ToString(SelectedSortingFB2LibrusecGenres) );
				writer.WriteElementString( "FB22Genres", Convert.ToString(SelectedSortingFB22Genres) );
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
		public static bool FullSortingNotDelFB2Files {
			get { return m_FullSortingNotDelFB2Files; }
			set { m_FullSortingNotDelFB2Files = value; }
		}
		public static bool FullSortingFB2LibrusecGenres {
			get { return m_FullSortingFB2LibrusecGenres; }
			set { m_FullSortingFB2LibrusecGenres = value; }
		}
		public static bool FullSortingFB22Genres {
			get { return m_FullSortingFB22Genres; }
			set { m_FullSortingFB22Genres = value; }
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
		public static bool SelectedSortingNotDelFB2Files {
			get { return m_SelectedSortingNotDelFB2Files; }
			set { m_SelectedSortingNotDelFB2Files = value; }
		}
		public static bool SelectedSortingFB2LibrusecGenres {
			get { return m_SelectedSortingFB2LibrusecGenres; }
			set { m_SelectedSortingFB2LibrusecGenres = value; }
		}
		public static bool SelectedSortingFB22Genres {
			get { return m_SelectedSortingFB22Genres; }
			set { m_SelectedSortingFB22Genres = value; }
		}
		#endregion
		
		#region Открытые статические методы класса для чтения из xml настроек Полной Сортировки
		public static string GetFileManagerHelpPath() {
			return m_sFileManagerHelpPath;
		}
		public static string GetDefFMDescTemplatePath() {
			return m_sDescTemplatePath;
		}
		
		public static string ReadXmlFullSortingSourceDir() {
			// чтение FullSortingSourceDir из xml-файла
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
		public static bool ReadXmlFullSortingNotDelFB2Files() {
			// чтение FullSortingNotDelFB2Files из xml-файла
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FileManager/FullSorting/NotDelFB2Files");
				if(node != null)
					return FullSortingNotDelFB2Files = Convert.ToBoolean(node.InnerText);
			}
			return FullSortingNotDelFB2Files;
		}
		public static bool ReadXmlFullSortingFB2Librusec() {
			// чтение FullSortingFB2Librusec из xml-файла
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FileManager/FullSorting/FB2LibrusecGenres");
				if(node != null)
					return FullSortingFB2LibrusecGenres = Convert.ToBoolean(node.InnerText);
			}
			return FullSortingFB2LibrusecGenres;
		}
		public static bool ReadXmlFullSortingFB22() {
			// чтение FullSortingFB22 из xml-файла
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FileManager/FullSorting/FB22Genres");
				if(node != null)
					return FullSortingFB22Genres = Convert.ToBoolean(node.InnerText);
			}
			return FullSortingFB22Genres;
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
		public static bool ReadXmlSelectedSortingNotDelFB2Files() {
			// чтение SelectedSortingNotDelFB2Files из xml-файла
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FileManager/SelectedSorting/NotDelFB2Files");
				if(node != null)
					return SelectedSortingNotDelFB2Files = Convert.ToBoolean(node.InnerText);
			}
			return SelectedSortingNotDelFB2Files;
		}
		public static bool ReadXmlSelectedSortingFB2Librusec() {
			// чтение SelectedSortingFB2Librusec из xml-файла
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FileManager/SelectedSorting/FB2LibrusecGenres");
				if(node != null)
					return SelectedSortingFB2LibrusecGenres = Convert.ToBoolean(node.InnerText);
			}
			return SelectedSortingFB2LibrusecGenres;
		}
		public static bool ReadXmlSelectedSortingFB22() {
			// чтение SelectedSortingFB22 из xml-файла
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FileManager/SelectedSorting/FB22Genres");
				if(node != null)
					return SelectedSortingFB22Genres = Convert.ToBoolean(node.InnerText);
			}
			return SelectedSortingFB22Genres;
		}
		#endregion
	}
}
		