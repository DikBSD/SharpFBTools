/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 28.04.2009
 * Time: 17:21
 * 
 * License: GPL 2.1
 */
using System;
using System.Xml;
using FB2.Description;

namespace FB2.FB2Parsers
{
	/// <summary>
	/// Description of FB2DescriptionParser.
	/// </summary>
	public class FB2DescriptionParser
	{
		#region Закрытые данные класса
		private XmlNamespaceManager m_NsManager;
        private XmlDocument			m_RawData;
        #endregion
        
		#region Конструкторы класса
        public FB2DescriptionParser()
        {
            m_RawData = new XmlDocument();
        }
        #endregion
        
        #region Открытые свойства класса
        public XmlDocument RawData
        {
            get { return m_RawData; }
        }
        #endregion
        
        #region Открытые методы класса
        public Description.Description Parse( string sFB2Path )
        {
        	/// TODO доделать
        	return new Description.Description();
        }
        #endregion
	}
}
