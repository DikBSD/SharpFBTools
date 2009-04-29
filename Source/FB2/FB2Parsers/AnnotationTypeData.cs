/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 30.04.2009
 * Time: 0:00
 * 
 * * License: GPL 2.1
 */
using System;
using System.Xml;
using FB2.Description.TitleInfo;

namespace FB2.FB2Parsers
{
	/// <summary>
	/// Description of AnnotationTypeData.
	/// </summary>
	public class AnnotationTypeData
	{
		public AnnotationTypeData()
		{
		}
		
		public T AnnotationType<T>( XmlNode xmlNode ) where T : IAnnotationType, new()
        {
            T annotation = default(T);
            if( xmlNode != null ) {
                annotation = new T();
                annotation.Value = xmlNode.InnerXml;
                if( xmlNode.Attributes["id"] != null ) {
                    annotation.Id = xmlNode.Attributes["id"].Value;
                }
                if( xmlNode.Attributes["lang"] != null ) {
                    annotation.Lang = xmlNode.Attributes["lang"].Value;
                }
            }
            return annotation;
        }
	}
}
