/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 29.04.2009
 * Time: 23:50
 * 
 * * License: GPL 2.1
 */
using System;
using System.Xml;
using FB2.Common;

namespace FB2.FB2Parsers
{
	/// <summary>
	/// Description of TextFieldTypeData.
	/// </summary>
	public class TextFieldTypeData
	{
		public TextFieldTypeData()
		{
		}
		
		public T TextFieldType<T>( XmlNode xmlNode ) where T : ITextFieldType, new()
        {
            if( xmlNode == null ) {
                return default(T);
            }

            T textField = new T();
            textField.Value = xmlNode.InnerText;
            if( xmlNode.Attributes["lang"] != null ) {
                textField.Lang = xmlNode.Attributes["lang"].Value;
            }
            return textField;
        }
	}
}
