/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 01.10.2013
 * Time: 13:54
 * 
 * License: GPL 2.1
 */
using System;
using System.Xml;
using System.IO;
using System.Windows.Forms;

namespace Settings
{
	/// <summary>
	/// ValidatorSettings: класс для работы с сохранением в xml и чтением настроек Валидатора
	/// </summary>
	public class ValidatorSettings
	{
		#region Закрытые статические члены-данные класса
		private static string m_FileSettingsPath		= Settings.GetProgDir()+@"\ValidatorSettings.xml";
		private static string m_sFB2ValidatorHelpPath	= Settings.GetProgDir()+@"\Help\FB2ValidatorHelp.rtf";
		// вид ToolButtons инструмента
		private static string m_cboxDSValidatorText		= "ImageAndText";
		private static string m_cboxTIRValidatorText	= "ImageBeforeText";
		// пакпки
		private static string m_sSourceDir 				= "";
		private static string m_sFB2NotValidDirCopyTo	= "";
		private static string m_sFB2NotValidDirMoveTo	= "";
		private static string m_sFB2ValidDirCopyTo		= "";
		private static string m_sFB2ValidDirMoveTo		= "";
		private static string m_sNotFB2DirCopyTo		= "";
		private static string m_sNotFB2DirMoveTo		= "";
		// опции
		private static bool m_ScanSubDir			= true;
		private static bool m_FB2LibrusecGenres		= true;
		private static bool m_FB22Genres			= false;
		private static int m_ExistFileWorker = 1;
		// настройки обработчиков мыши и клавиши Enter
		private static int m_MouseDoubleClickForFB2Mode		= 1;
		private static int m_MouseDoubleClickForZipMode		= 1;
		private static int m_EnterPressForFB2Mode			= 0;
		private static int m_EnterPressForZipMode			= 0;
		// путь к Архиватору
		private static string m_ArchivatorPath	= "";
		//
		private static XmlDocument m_xmlDoc = new XmlDocument();
		#endregion
		
		public ValidatorSettings()
		{
		}
		
		#region Открытые статические общие свойства класса
		public static string ValidatorSettingsPath {
			get { return m_FileSettingsPath; }
		}
		#endregion
		
		#region Открытые статические методы
		public static void WriteValidatorSettings() {
			#region Сохранение настроек в zml-файл
			XmlWriter writer = null;
			try {
				XmlWriterSettings settings = new XmlWriterSettings();
				settings.Indent = true;
				settings.IndentChars = ("\t");
				settings.OmitXmlDeclaration = true;
				
				writer = XmlWriter.Create( m_FileSettingsPath, settings );
				writer.WriteStartElement( "FB2Validator" );
				
				// Общие основные настройки
				writer.WriteElementString( "SourceDir", SourceDir );
				writer.WriteStartElement( "NotValidFB2Files" );
					writer.WriteElementString( "CopyTo", FB2NotValidDirCopyTo );
					writer.WriteElementString( "MoveTo", FB2NotValidDirMoveTo );
				writer.WriteFullEndElement();
				writer.WriteStartElement( "ValidFB2Files" );
					writer.WriteElementString( "CopyTo", FB2ValidDirCopyTo );
					writer.WriteElementString( "MoveTo", FB2ValidDirMoveTo );
				writer.WriteFullEndElement();
				writer.WriteStartElement( "NotFB2Files" );
					writer.WriteElementString( "CopyTo", NotFB2DirCopyTo );
					writer.WriteElementString( "MoveTo", NotFB2DirMoveTo );
				writer.WriteFullEndElement();
				writer.WriteElementString( "ScanSubDir", Convert.ToString(ScanSubDir) );
				writer.WriteElementString( "ExistFileWorker", Convert.ToString(ExistFileWorker) );
				writer.WriteStartElement( "GenreSchema" );
					writer.WriteElementString( "FB2LibrusecGenres", Convert.ToString(FB2LibrusecGenres) );
					writer.WriteElementString( "FB22Genres", Convert.ToString(FB22Genres) );
				writer.WriteFullEndElement();
				writer.WriteStartElement( "EventHandlers" );
					writer.WriteElementString( "MouseDoubleClickForFB2Mode", Convert.ToString(MouseDoubleClickForFB2Mode) );
					writer.WriteElementString( "MouseDoubleClickForZipMode", Convert.ToString(MouseDoubleClickForZipMode) );
					writer.WriteElementString( "EnterPressForFB2Mode", Convert.ToString(EnterPressForFB2Mode) );
					writer.WriteElementString( "EnterPressForZipMode", Convert.ToString(EnterPressForZipMode) );
				writer.WriteFullEndElement();
				writer.WriteElementString( "ArchivatorPath", ArchivatorPath );
				
				writer.WriteEndElement();
				writer.Flush();
			}  finally  {
				if (writer != null)
					writer.Close();
			}
			#endregion
		}
		
		// задание для кнопок ToolStrip стиля и положения текста и картинки
		public static string GetDefValidatorcboxDSValidatorText() {
			return m_cboxDSValidatorText;
		}
		public static string GetDefValidatorcboxTIRValidatorText() {
			return m_cboxTIRValidatorText;
		}
		
		public static void SetToolButtonsSettings( ToolStrip ts ) {
			Settings.SetToolButtonsSettings( "ValidatorToolButtons", "cboxDSValidatorText", "cboxTIRValidatorText", ts );
		}
		#endregion

		#region Открытые статические свойства класса
		// папка для сканирования
		public static string SourceDir {
			get { return m_sSourceDir; }
			set { m_sSourceDir = value; }
		}

		// папка для копирования не валидных fb2-файлов
		public static string FB2NotValidDirCopyTo {
			get { return m_sFB2NotValidDirCopyTo; }
			set { m_sFB2NotValidDirCopyTo = value; }
		}
				// папка для перемещения не валидных fb2-файлов
		public static string FB2NotValidDirMoveTo {
			get { return m_sFB2NotValidDirMoveTo; }
			set { m_sFB2NotValidDirMoveTo = value; }
		}
		
		// папка для копирования валидных fb2-файлов
		public static string FB2ValidDirCopyTo {
			get { return m_sFB2ValidDirCopyTo; }
			set { m_sFB2ValidDirCopyTo = value; }
		}
				// папка для перемещения валидных fb2-файлов
		public static string FB2ValidDirMoveTo {
			get { return m_sFB2ValidDirMoveTo; }
			set { m_sFB2ValidDirMoveTo = value; }
		}
		
		// папка для копирования не fb2-файлов
		public static string NotFB2DirCopyTo {
			get { return m_sNotFB2DirCopyTo; }
			set { m_sNotFB2DirCopyTo = value; }
		}
		// папка для перемещения не fb2-файлов
		public static string NotFB2DirMoveTo {
			get { return m_sNotFB2DirMoveTo; }
			set { m_sNotFB2DirMoveTo = value; }
		}
		
		// обрабатывать подкаталоги
		public static bool ScanSubDir {
			get { return m_ScanSubDir; }
			set { m_ScanSubDir = value; }
		}
		
		// схема жанров
		public static bool FB2LibrusecGenres {
			get { return m_FB2LibrusecGenres; }
			set { m_FB2LibrusecGenres = value; }
		}
		public static bool FB22Genres {
			get { return m_FB22Genres; }
			set { m_FB22Genres = value; }
		}
		
		// обработка имени файла, если такой уже есть
		public static int ExistFileWorker {
			get { return m_ExistFileWorker; }
			set { m_ExistFileWorker = value; }
		}
		
		// обработка двойного клика мышки на fb2 файле
		public static int MouseDoubleClickForFB2Mode {
			get { return m_MouseDoubleClickForFB2Mode; }
			set { m_MouseDoubleClickForFB2Mode = value; }
		}
		// обработка двойного клика мышки на zip архиве
		public static int MouseDoubleClickForZipMode {
			get { return m_MouseDoubleClickForZipMode; }
			set { m_MouseDoubleClickForZipMode = value; }
		}
		// обработка нажатия клавиши Enter на fb2 файле
		public static int EnterPressForFB2Mode {
			get { return m_EnterPressForFB2Mode; }
			set { m_EnterPressForFB2Mode = value; }
		}
		// обработка нажатия клавиши Enter на zip архиве
		public static int EnterPressForZipMode {
			get { return m_EnterPressForZipMode; }
			set { m_EnterPressForZipMode = value; }
		}
		
		// задание пути к GUI Архиватора
		public static string ArchivatorPath {
			get { return m_ArchivatorPath; }
			set { m_ArchivatorPath = value; }
		}
		
		#endregion

		#region Открытые статические методы класса для чтения из xml настроек
		public static string GetFB2ValidatorHelpPath() {
			return m_sFB2ValidatorHelpPath;
		}
		
		// чтение SourceDir из xml-файла
		public static string ReadXmlSourceDir() {
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FB2Validator/SourceDir");
				if(node != null)
					return SourceDir = node.InnerText.Trim();
			}
			return SourceDir;
		}
		
		// чтение FB2NotValidDirCopyTo из xml-файла
		public static string ReadXmlFB2NotValidDirCopyTo() {
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FB2Validator/NotValidFB2Files/CopyTo");
				if(node != null)
					return FB2NotValidDirCopyTo = node.InnerText.Trim();
			}
			return FB2NotValidDirCopyTo;
		}
		// чтение FB2NotValidDirMoveTo из xml-файла
		public static string ReadXmlFB2NotValidDirMoveTo() {
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FB2Validator/NotValidFB2Files/MoveTo");
				if(node != null)
					return FB2NotValidDirMoveTo = node.InnerText.Trim();
			}
			return FB2NotValidDirMoveTo;
		}
		
		// чтение FB2ValidDirCopyTo из xml-файла
		public static string ReadXmlFB2ValidDirCopyTo() {
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FB2Validator/ValidFB2Files/CopyTo");
				if(node != null)
					return FB2ValidDirCopyTo = node.InnerText.Trim();
			}
			return FB2ValidDirCopyTo;
		}
		// чтение FB2NotValidDirMoveTo из xml-файла
		public static string ReadXmlFB2ValidDirMoveTo() {
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FB2Validator/ValidFB2Files/MoveTo");
				if(node != null)
					return FB2ValidDirMoveTo = node.InnerText.Trim();
			}
			return FB2ValidDirMoveTo;
		}
		
		// чтение NotFB2DirCopyTo из xml-файла
		public static string ReadXmlNotFB2DirCopyTo() {
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FB2Validator/NotFB2Files/CopyTo");
				if(node != null)
					return NotFB2DirCopyTo = node.InnerText.Trim();
			}
			return NotFB2DirCopyTo;
		}
		// чтение NotFB2DirMoveTo из xml-файла
		public static string ReadXmlNotFB2DirMoveTo() {
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FB2Validator/NotFB2Files/MoveTo");
				if(node != null)
					return NotFB2DirMoveTo = node.InnerText.Trim();
			}
			return NotFB2DirMoveTo;
		}
		
		// чтение ScanSubDir из xml-файла
		public static bool ReadXmlScanSubDir() {
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FB2Validator/ScanSubDir");
				if(node != null)
					return ScanSubDir = Convert.ToBoolean(node.InnerText);
			}
			return ScanSubDir;
		}
		
		// чтение FB2LibrusecGenres из xml-файла
		public static bool ReadXmlFB2Librusec() {
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FB2Validator/GenreSchema/FB2LibrusecGenres");
				if(node != null)
					return FB2LibrusecGenres = Convert.ToBoolean(node.InnerText);
			}
			return FB2LibrusecGenres;
		}
		// чтение FB22Genres из xml-файла
		public static bool ReadXmlFB22() {
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FB2Validator/GenreSchema/FB22Genres");
				if(node != null)
					return FB22Genres = Convert.ToBoolean(node.InnerText);
			}
			return FB22Genres;
		}
		
		// чтение ExistFileWorker из xml-файла
		public static int ReadXmlExistFileWorker() {
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FB2Validator/ExistFileWorker");
				if(node != null)
					return ExistFileWorker = (int)Convert.ToInt16(node.InnerText);
			}
			return ExistFileWorker;
		}
		
		// чтение MouseDoubleClickForFB2Mode из xml-файла
		public static int ReadXmlMouseDoubleClickForFB2Mode() {
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FB2Validator/EventHandlers/MouseDoubleClickForFB2Mode");
				if(node != null)
					return MouseDoubleClickForFB2Mode = (int)Convert.ToInt16(node.InnerText);
			}
			return MouseDoubleClickForFB2Mode;
		}
		// чтение MouseDoubleClickForZipMode из xml-файла
		public static int ReadXmlMouseDoubleClickForZipMode() {
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FB2Validator/EventHandlers/MouseDoubleClickForZipMode");
				if(node != null)
					return MouseDoubleClickForZipMode = (int)Convert.ToInt16(node.InnerText);
			}
			return MouseDoubleClickForZipMode;
		}
		// чтение EnterPressForFB2Mode из xml-файла
		public static int ReadXmlEnterPressForFB2Mode() {
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FB2Validator/EventHandlers/EnterPressForFB2Mode");
				if(node != null)
					return EnterPressForFB2Mode = (int)Convert.ToInt16(node.InnerText);
			}
			return EnterPressForFB2Mode;
		}
		// чтение EnterPressForZipMode из xml-файла
		public static int ReadXmlEnterPressForZipMode() {
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FB2Validator/EventHandlers/EnterPressForZipMode");
				if(node != null)
					return EnterPressForZipMode = (int)Convert.ToInt16(node.InnerText);
			}
			return EnterPressForZipMode;
		}
		
		// чтение ArchivatorPath из xml-файла
		public static string ReadXmlArchivatorPath() {
			if(File.Exists(m_FileSettingsPath)) {
				m_xmlDoc.Load(m_FileSettingsPath);
				XmlNode node = m_xmlDoc.SelectSingleNode("FB2Validator/ArchivatorPath");
				if(node != null)
					return ArchivatorPath = node.InnerText;
			}
			return ArchivatorPath;
		}
		
		#endregion
	}
}
