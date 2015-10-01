/*
 * Created by SharpDevelop.
 * User: DikBSD
 * Date: 07.03.2009
 * Time: 21:34
 * 
 * License: GPL 2.1
 */

using System;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Windows.Forms;

using Settings;
using SharpZipLibWorker = Core.Common.SharpZipLibWorker;

namespace Core.FB2Parser
{
	/// <summary>
	/// Description of FB2Validator.
	/// </summary>
	public class FB2Validator
	{
		#region Закрытые данные класса
		private const string m_aFB20Namespace	= "http://www.gribuser.ru/xml/fictionbook/2.0";
		private const string m_aFB21Namespace	= "http://www.gribuser.ru/xml/fictionbook/2.1";
		private readonly static SharpZipLibWorker	m_sharpZipLib	= new SharpZipLibWorker();
		private readonly static string				m_TempDir		= Settings.Settings.TempDir;
		#endregion
		
		public FB2Validator()
		{
		}

		private string validate( string FilePath, string SchemePath ) {
			string Ext = Path.GetExtension( FilePath ).ToLower();
			string [] files = null;
			if ( Ext == ".zip" || Ext == ".fbz" ) {
				m_sharpZipLib.UnZipFiles(FilePath, m_TempDir, 0, false, null, 4096);
				files = Directory.GetFiles( m_TempDir );
			}
			string FB2Path = files != null ? files[0] : FilePath;
			XmlDocument xmlDoc = new XmlDocument();
			try {
				xmlDoc.Load( FB2Path );
			} catch ( System.Exception e ) {
				return  e.Message + "\r\n\r\nФайл: " + FilePath;
			}
			
			Cursor.Current = Cursors.WaitCursor;
			string fb2FileNamespaceURI = xmlDoc.DocumentElement.NamespaceURI;
			using (Stream xmlSchemeFile = new FileStream( SchemePath, FileMode.Open ) ) {
				XmlSchemaSet sc = new XmlSchemaSet();
				if( fb2FileNamespaceURI.Equals( m_aFB21Namespace ) )
					sc.Add( m_aFB21Namespace, XmlReader.Create( xmlSchemeFile ) );
				else
					sc.Add( m_aFB20Namespace, XmlReader.Create( xmlSchemeFile ) );
				
				XmlReaderSettings settings = new XmlReaderSettings();
				settings.ValidationType = ValidationType.Schema;
				settings.Schemas = sc;
				XmlReader reader = XmlReader.Create( FB2Path, settings );

				try {
					while ( reader.Read() ) {;}
					reader.Close();
					Cursor.Current = Cursors.Default;
					return string.Empty;
				} catch (System.Xml.Schema.XmlSchemaException e) {
					reader.Close();
					Cursor.Current = Cursors.Default;
					return "Файл: " + FilePath + "\r\n" + e.Message + "\r\nСтрока: " + e.LineNumber + "; Позиция: " + e.LinePosition;
				} catch ( System.Exception e ) {
					reader.Close();
					Cursor.Current = Cursors.Default;
					return "Файл: " + FilePath + "\r\n" + e.Message;
				}
			}
		}
		
		public string ValidatingFB2File( string FilePath ) {
			return validate( FilePath, Settings.Settings.FB2LibrusecSchemePath );
		}
		
	}
}
