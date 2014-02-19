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

using Settings;

namespace Core.FB2Parser
{
	/// <summary>
	/// Description of FB2Validator.
	/// </summary>
	public class FB2Validator
	{
		public FB2Validator()
		{
		}

		public string ValidatingFB22File( string sFB2Path )
        {
			#region Код
			using (Stream xmlSchemeFile = new FileStream( Settings.Settings.GetFB22SchemePath(), FileMode.Open ) )
            {
				XmlSchemaSet sc = new XmlSchemaSet();

                sc.Add( "http://www.gribuser.ru/xml/fictionbook/2.0",
                       XmlReader.Create( xmlSchemeFile ) );

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;
                settings.Schemas = sc;
                XmlReader reader = XmlReader.Create( sFB2Path, settings );

                try {
                	// Parse the file.
                	while ( reader.Read() );
                	reader.Close();
                	return string.Empty;
                } catch (System.Xml.Schema.XmlSchemaException e) {
            		reader.Close();
            		return e.Message + "\r\nСтрока: " + e.LineNumber + "; Позиция: " + e.LinePosition;
                } catch ( System.Exception e ) {
                	reader.Close();
                	return e.Message;
                }
            }
			#endregion
        }
		
		public string ValidatingFB2LibrusecFile( string sFB2Path )
        {
			#region Код
			using (Stream xmlSchemeFile = new FileStream( Settings.Settings.GetFB2LibrusecSchemePath(), FileMode.Open ) )
            {
				XmlSchemaSet sc = new XmlSchemaSet();

                sc.Add( "http://www.gribuser.ru/xml/fictionbook/2.0",
                       XmlReader.Create( xmlSchemeFile ) );

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;
                settings.Schemas = sc;
                XmlReader reader = XmlReader.Create( sFB2Path, settings );

                try {
                	// Parse the file.
                	while ( reader.Read() );
                	reader.Close();
                	return string.Empty;
                } catch (System.Xml.Schema.XmlSchemaException e) {
            		reader.Close();
            		return e.Message + "\r\nСтрока: " + e.LineNumber + "; Позиция: " + e.LinePosition;
                } catch ( System.Exception e ) {
                	reader.Close();
                	return e.Message;
                }
            }
			#endregion
        }
		
	}
}
