/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 17:23
 * 
 * License: GPL 2.1
 */
using System;
using System.Xml;

namespace FB2.FB2Parsers
{
	/// <summary>
	/// Description of IFB2Parser.
	/// </summary>
	public interface IFB2Parser
    {
        XmlDocument RawData { get; }
        //IFBDocument Parse( string sFB2Path );
    }
}
