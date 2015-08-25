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
		#endregion
		
		public FB2Validator()
		{
		}

		private string validate( string sFB2Path, string SchemePath ) {
			XmlDocument xmlDoc = new XmlDocument();
			try {
				xmlDoc.Load( sFB2Path );
			} catch ( System.Exception e ) {
				return "Файл: " + sFB2Path + "\r\n" + e.Message;
			}
			string fb2FileNamespaceURI = xmlDoc.DocumentElement.NamespaceURI;
			using (Stream xmlSchemeFile = new FileStream( SchemePath, FileMode.Open ) )
			{
				XmlSchemaSet sc = new XmlSchemaSet();
				if( fb2FileNamespaceURI.Equals( m_aFB21Namespace ) )
					sc.Add( m_aFB21Namespace, XmlReader.Create( xmlSchemeFile ) );
				else
					sc.Add( m_aFB20Namespace, XmlReader.Create( xmlSchemeFile ) );
				
				XmlReaderSettings settings = new XmlReaderSettings();
				settings.ValidationType = ValidationType.Schema;
				settings.Schemas = sc;
				XmlReader reader = XmlReader.Create( sFB2Path, settings );

				try {
					while ( reader.Read() ) {;}
					reader.Close();
					return string.Empty;
				} catch (System.Xml.Schema.XmlSchemaException e) {
					reader.Close();
					return "Файл: " + sFB2Path + "\r\n" + e.Message + "\r\nСтрока: " + e.LineNumber + "; Позиция: " + e.LinePosition;
				} catch ( System.Exception e ) {
					reader.Close();
					return "Файл: " + sFB2Path + "\r\n" + e.Message;
				}
			}
		}
		public string ValidatingFB22File( string sFB2Path ) {
			return validate( sFB2Path, Settings.Settings.FB22SchemePath );
		}
		
		public string ValidatingFB2LibrusecFile( string sFB2Path ) {
			return validate( sFB2Path, Settings.Settings.FB2LibrusecSchemePath );
		}
		
	}
}
