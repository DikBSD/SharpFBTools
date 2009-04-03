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

namespace FB2Parser
{
	/// <summary>
	/// Description of FB2Validator.
	/// </summary>
	public class FB2Validator
	{
		public FB2Validator()
		{
		}

		public string ValidatingFB2File( string path )
        {
            using (Stream xmlSchemeFile = new FileStream("FictionBook.xsd", FileMode.Open))
            {
                // Create the XmlSchemaSet class.
                XmlSchemaSet sc = new XmlSchemaSet();

                // Add the schema to the collection.
                sc.Add("http://www.gribuser.ru/xml/fictionbook/2.0",
                       XmlReader.Create(xmlSchemeFile));

                // Set the validation settings.
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;
                settings.Schemas = sc;

                // Create the XmlReader object.
                XmlReader reader = XmlReader.Create(path, settings);

                try {
                	// Parse the file.
                	while (reader.Read());
                	reader.Close();
                	return "";
                } catch (System.Xml.Schema.XmlSchemaException e) {
            		reader.Close();
            		return e.Message + "\r\nСтрока: " + e.LineNumber + "; Позиция: " + e.LinePosition;
                } catch (System.Exception e) {
                	reader.Close();
                	return e.Message;
                }
            }
        }
	}
}
