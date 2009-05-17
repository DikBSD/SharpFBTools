/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 14.05.2009
 * Time: 16:44
 * 
 * License: GPL 2.1
 */
using System;
using System.Xml;

namespace Settings
{
	/// <summary>
	/// Description of SettingsValidator.
	/// </summary>
	public class SettingsValidator
	{
		#region Закрытые статические члены-данные класса
		private static string m_sFB2ValidatorHelpPath = Settings.GetProgDir()+"\\Help\\FB2ValidatorHelp.rtf";
		private static Int16 m_nValidatorForFB2SelectedIndex		= 1;
		private static Int16 m_nValidatorForFB2ArchiveSelectedIndex	= 1;
		private static Int16 m_nValidatorForFB2SelectedIndexPE		= 0;
		private static Int16 m_nValidatorForFB2ArchiveSelectedIndexPE	= 0;
		//
		private static string m_sValidatorDirsSettingsPath = Settings.GetProgDir()+"\\ValidatorDirs.xml";
		private static string m_sScanDir 				= "";
		private static string m_sFB2NotValidDirCopyTo	= "";
		private static string m_sFB2NotValidDirMoveTo	= "";
		private static string m_sFB2ValidDirCopyTo		= "";
		private static string m_sFB2ValidDirMoveTo		= "";
		private static string m_sNotFB2DirCopyTo		= "";
		private static string m_sNotFB2DirMoveTo		= "";
		#endregion
		
		public SettingsValidator()
		{
		}
		
		#region Открытые статические методы
		public static Int16 GetDefValidatorFB2SelectedIndex() {
			return m_nValidatorForFB2SelectedIndex;
		}
		
		public static Int16 GetDefValidatorFB2ArchiveSelectedIndex() {
			return m_nValidatorForFB2ArchiveSelectedIndex;
		}
		
		public static Int16 GetDefValidatorFB2SelectedIndexPE() {
			return m_nValidatorForFB2SelectedIndexPE;
		}
		
		public static Int16 GetDefValidatorFB2ArchiveSelectedIndexPE() {
			return m_nValidatorForFB2ArchiveSelectedIndexPE;
		}
		
		public static string GetFB2ValidatorHelpPath() {
			return m_sFB2ValidatorHelpPath;
		}
		
		public static Int16 ReadValidatorFB2SelectedIndex() {
			// читаем номер выделенного итема для комбобокса cboxValidatorForFB2 из настроек
			return Convert.ToInt16( Settings.ReadAttribute( "ValidatorDoubleClick", "cboxValidatorForFB2SelectedIndex", GetDefValidatorFB2SelectedIndex().ToString() ) );
		}
		
		public static Int16 ReadValidatorFB2ArchiveSelectedIndex() {
			// читаем номер выделенного итема для комбобокса cboxValidatorForFB2Archive из настроек
			return Convert.ToInt16( Settings.ReadAttribute( "ValidatorDoubleClick", "cboxValidatorForFB2ArchiveSelectedIndex", GetDefValidatorFB2ArchiveSelectedIndex().ToString() ) );
		}
		
		public static Int16 ReadValidatorFB2SelectedIndexPE() {
			// читаем номер выделенного итема для комбобокса cboxValidatorForFB2 из настроек для нажатия Enter
			return Convert.ToInt16( Settings.ReadAttribute( "ValidatorPressEnter", "cboxValidatorForFB2SelectedIndexPE", GetDefValidatorFB2SelectedIndexPE().ToString() ) );
		}
		
		public static Int16 ReadValidatorFB2ArchiveSelectedIndexPE() {
			// читаем номер выделенного итема для комбобокса cboxValidatorForFB2Archive из настроек для нажатия Enter
			return Convert.ToInt16( Settings.ReadAttribute( "ValidatorPressEnter", "cboxValidatorForFB2ArchiveSelectedIndexPE", GetDefValidatorFB2ArchiveSelectedIndexPE().ToString() ) );
		}
		
		//
		public static void WriteValidatorDirs() {
			XmlWriter writer = null;
			try {
				XmlWriterSettings settings = new XmlWriterSettings();
				settings.Indent = true;
				settings.IndentChars = ("\t");
				settings.OmitXmlDeclaration = true;
				
				writer = XmlWriter.Create( SettingsValidator.ValidatorDirsSettingsPath, settings );
				writer.WriteStartElement( "SharpFBTools" );
					writer.WriteStartElement( "FB2Validator" );
						writer.WriteStartElement( "ScanDir" );
							writer.WriteAttributeString( "tboxSourceDir", SettingsValidator.ScanDir );
						writer.WriteFullEndElement();
						writer.WriteStartElement( "NotValidFB2Files" );
							writer.WriteAttributeString( "tboxFB2NotValidDirCopyTo", SettingsValidator.FB2NotValidDirCopyTo );
							writer.WriteAttributeString( "tboxFB2NotValidDirMoveTo", SettingsValidator.FB2NotValidDirMoveTo );
						writer.WriteFullEndElement();
						writer.WriteStartElement( "ValidFB2Files" );
							writer.WriteAttributeString( "tboxFB2ValidDirCopyTo", SettingsValidator.FB2ValidDirCopyTo );
							writer.WriteAttributeString( "tboxFB2ValidDirMoveTo", SettingsValidator.FB2ValidDirMoveTo );
						writer.WriteFullEndElement();
						writer.WriteStartElement( "NotFB2Files" );
							writer.WriteAttributeString( "tboxNotFB2DirCopyTo", SettingsValidator.NotFB2DirCopyTo );
							writer.WriteAttributeString( "tboxNotFB2DirMoveTo", SettingsValidator.NotFB2DirMoveTo );
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
		
		#region Открытые статические свойства класса для Папок Валидатора
		public static string ValidatorDirsSettingsPath {
			get { return m_sValidatorDirsSettingsPath; }
		}
		
		public static string ScanDir {
			// папка для сканирования
			get { return m_sScanDir; }
			set { m_sScanDir = value; }
		}

		public static string FB2NotValidDirCopyTo {
			// папка для копирования не валидных fb2-файлов
			get { return m_sFB2NotValidDirCopyTo; }
			set { m_sFB2NotValidDirCopyTo = value; }
		}
		
		public static string FB2NotValidDirMoveTo {
			// папка для перемещения не валидных fb2-файлов
			get { return m_sFB2NotValidDirMoveTo; }
			set { m_sFB2NotValidDirMoveTo = value; }
		}
		
		public static string FB2ValidDirCopyTo {
			// папка для копирования валидных fb2-файлов
			get { return m_sFB2ValidDirCopyTo; }
			set { m_sFB2ValidDirCopyTo = value; }
		}
		
		public static string FB2ValidDirMoveTo {
			// папка для перемещения валидных fb2-файлов
			get { return m_sFB2ValidDirMoveTo; }
			set { m_sFB2ValidDirMoveTo = value; }
		}
		
		public static string NotFB2DirCopyTo {
			// папка для копирования не fb2-файлов
			get { return m_sNotFB2DirCopyTo; }
			set { m_sNotFB2DirCopyTo = value; }
		}
		
		public static string NotFB2DirMoveTo {
			// папка для перемещения не fb2-файлов
			get { return m_sNotFB2DirMoveTo; }
			set { m_sNotFB2DirMoveTo = value; }
		}
		#endregion

	}
}
