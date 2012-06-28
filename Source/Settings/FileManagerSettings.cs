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

namespace Settings
{
	/// <summary>
	/// FileManagerSettings: класс для работы с сохранением в xml и чтением настроек Сортировщика
	/// </summary>
	public class FileManagerSettings
	{
		#region Закрытые статические данные класса
		private static string m_FileSettingsPath = Settings.GetProgDir()+@"\FileManagerSettings.xml";
		// Общие
		private static bool m_BooksTagsView	= false;
		// рабочие папки и данные для Полной Сортировки
		private static string m_FullSortingSourceDir	= "";
		private static string m_FullSortingTemplate		= "";
		private static bool m_FullSortingInSubDir		= true;
		private static bool m_ViewMessageForLongTime	= true;
		
		// рабочие папки и данные для Избранной Сортировки
		private static string m_SelectedSortingSourceDir	= "";
		private static string m_SelectedSortingTargetDir	= "";
		private static string m_SelectedSortingTemplate		= "";
		private static bool m_SelectedSortingInSubDir		= true;
		#endregion
		
		public FileManagerSettings()
		{
		}
		
		#region Открытые статические общие свойства класса
		public static string FileManagerSettingsPath {
			get { return m_FileSettingsPath; }
		}

		public static void WriteFileManagerSettings() {
			XmlWriter writer = null;
			try {
				XmlWriterSettings settings = new XmlWriterSettings();
				settings.Indent = true;
				settings.IndentChars = ("\t");
				settings.OmitXmlDeclaration = true;
				
				writer = XmlWriter.Create( m_FileSettingsPath, settings );
				writer.WriteStartElement( "FileManager" );
					// Общие основные настройки
					writer.WriteStartElement( "General" );
						writer.WriteElementString( "BooksTagsView", Convert.ToString(BooksTagsView) );
					writer.WriteEndElement();
					
					// Полная Сортировка
					writer.WriteStartElement( "FullSorting" );
						writer.WriteElementString( "SourceDir", FullSortingSourceDir );
						writer.WriteElementString( "Template", FullSortingTemplate );
						writer.WriteElementString( "SortingInSubDir", Convert.ToString(FullSortingInSubDir) );
						writer.WriteElementString( "ViewMessageForLongTime", Convert.ToString(ViewMessageForLongTime) );
					writer.WriteEndElement();

					// Избранная Сортировка
					writer.WriteStartElement( "SelectedSorting" );
						writer.WriteElementString( "SourceDir", SelectedSortingSourceDir );
						writer.WriteElementString( "TargetDir", SelectedSortingTargetDir );
						writer.WriteElementString( "Template", SelectedSortingTemplate );
						writer.WriteElementString( "SortingInSubDir", Convert.ToString(SelectedSortingInSubDir) );
					writer.WriteEndElement();
					
				writer.WriteEndElement();
				writer.Flush();
			}  finally  {
				if (writer != null)
				writer.Close();
			}
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
		#endregion
	}
}
