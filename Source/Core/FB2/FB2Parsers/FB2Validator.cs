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
using System.Text.RegularExpressions;

using System.Windows.Forms;

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

		/// <summary>
		/// Валидация fb2, fb2.zip ли fbz файлов по пути FilePath, согласно схеме SchemePath
		/// </summary>
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
				// Проверка наличия атрибутов корневого тега <FictionBook
				int FictionBookTagIndex = xmlDoc.InnerXml.IndexOf( "<FictionBook" );
				if ( FictionBookTagIndex != -1 ) {
					Regex regex  = new Regex( "<FictionBook [^>]+>", RegexOptions.None );
					Match m = regex.Match( xmlDoc.InnerXml );
					if ( m.Success ) {
						string FBookTag = m.Value;
						int xmlnsIndex = FBookTag.IndexOf( "xmlns=\"http://www.gribuser.ru/xml/fictionbook/2." );
						int xmlnsLinkIndex = FBookTag.IndexOf( "=\"http://www.w3.org/1999/xlink\"" );
						if ( xmlnsIndex == -1 )
							return "Файл: " + FilePath + "\r\nВ корневом теге <FictionBook отсутствует атрибут xmlns=\"http://www.gribuser.ru/xml/fictionbook/2.0\"";
						if ( xmlnsLinkIndex == -1 )
							return "Файл: " + FilePath + "\r\nВ корневом теге <FictionBook отсутствует атрибут xmlns:l=\"http://www.w3.org/1999/xlink\"";
					}
				}
				// Проверка обязательных тегов
				string TI = string.Empty;
				string DI = string.Empty;
				try {
					Regex regDesc = new Regex( @"<description>\s*?.+?\s*?</description>", RegexOptions.Multiline | RegexOptions.Singleline );
					Match mDesc = regDesc.Match( xmlDoc.InnerXml );
					if ( mDesc.Success ) {
						string Desc = mDesc.Value;
						Regex regTitleInfo = new Regex( @"<title-info>\s*?.+?\s*?</title-info>", RegexOptions.Multiline | RegexOptions.Singleline );
						Match mTI = regTitleInfo.Match( Desc );
						if ( mTI.Success )
							TI = mTI.Value;
						Regex regDocumentInfo = new Regex( @"<document-info>\s*?.+?\s*?</document-info>", RegexOptions.Multiline | RegexOptions.Singleline );
						Match mDI = regDocumentInfo.Match( Desc );
						if ( mDI.Success )
							DI = mDI.Value;
					} else
						return "Файл: " + FilePath + "\r\nОтсутствует раздел описания книги <description>";
				} catch ( RegexMatchTimeoutException /*ex*/ ) {}
				if ( string.IsNullOrEmpty( TI ) ) {
					return "Файл: " + FilePath + "\r\nОтсутствует раздел описания книги <title-info>";
				} else {
					// Проверка <genre>
					if ( TI.IndexOf("<genre") == -1 )
						return "Файл: " + FilePath + "\r\nОтсутствует тег <genre> книги";
					else {
						Regex regGenre = new Regex( @"(<genre ?/>)|(<genre></genre>)", RegexOptions.None );
						Match mGenre = regGenre.Match( TI );
						if ( mGenre.Success )
							return "Файл: " + FilePath + "\r\nТег <genre> книги 'пустой'";
					}
					
					// Проверка <lang>
					if ( TI.IndexOf("<lang") == -1 )
						return "Файл: " + FilePath + "\r\nОтсутствует тег <lang> книги";
					else {
						Regex regLang = new Regex( @"(<lang ?/>)|(<lang></lang>)", RegexOptions.None );
						Match mLang = regLang.Match( TI );
						if ( mLang.Success )
							return "Файл: " + FilePath + "\r\nТег <lang> книги 'пустой'";
						else {
							regLang = new Regex( @"(?<=<lang>)[^<]+(?=</lang>)", RegexOptions.None );
							mLang = regLang.Match( TI );
							if ( mLang.Success ) {
								if ( mLang.Value.Length > 2 )
									return "Файл: " + FilePath + "\r\nТег Название языка книги (тег <lang>) не может быть более 2 символов.\r\nЗначение тега <lang>: '" + mLang.Value + "'";
							}
						}
					}
					
					// Проверка <author>
					if ( TI.IndexOf("<author") == -1 )
						return "Файл: " + FilePath + "\r\nОтсутствует тег <author> Автора книги";
					else {
						Regex regAuthor = new Regex( @"(<author ?/>)|(<author></author>)", RegexOptions.None );
						Match mAuthor = regAuthor.Match( TI );
						if ( mAuthor.Success )
							return "Файл: " + FilePath + "\r\nТег <author> Автора книги 'пустой'";
					}
					
					// Проверка <book-title>
					if ( TI.IndexOf("<book-title") == -1 )
						return "Файл: " + FilePath + "\r\nОтсутствует тег <book-title> Названия книги";
					else {
						Regex regBT = new Regex( @"(<book-title ?/>)|(<book-title></book-title>)", RegexOptions.None );
						Match mBT = regBT.Match( TI );
						if ( mBT.Success )
							return "Файл: " + FilePath + "\r\nТег <book-title> Названия книги 'пустой'";
					}
				}
				if ( string.IsNullOrEmpty( DI ) ) {
					return "Файл: " + FilePath + "\r\nОтсутствует раздел описания книги <document-info>";
				} else {
					// Проверка id книги
					if ( DI.IndexOf("<id") == -1 )
						return "Файл: " + FilePath + "\r\nОтсутствует идентификатор книги тег <id>";
					else {
						Regex regID = new Regex( @"(<id ?/>)|(<id></id>)", RegexOptions.None );
						Match mID = regID.Match( DI );
						if ( mID.Success )
							return "Файл: " + FilePath + "\r\nИдентификатор книги тег <id> 'пустой'";
					}
				}
				
			} catch ( System.Exception e ) {
				return  e.Message + "\r\n\r\nФайл: " + FilePath;
			}
			
			Cursor.Current = Cursors.WaitCursor;
			string fb2FileNamespaceURI = xmlDoc.DocumentElement.NamespaceURI;
			using (Stream xmlSchemeFile = new FileStream( SchemePath, FileMode.Open ) ) {
				XmlSchemaSet sc = new XmlSchemaSet();
				try {
					if( fb2FileNamespaceURI.Equals( m_aFB21Namespace ) )
						sc.Add( m_aFB21Namespace, XmlReader.Create( xmlSchemeFile ) );
					else
						sc.Add( m_aFB20Namespace, XmlReader.Create( xmlSchemeFile ) );
				} catch (System.Xml.Schema.XmlSchemaException e) {
					return "Файл: " + FilePath + "\r\n" + e.Message + "\r\nСтрока: " + e.LineNumber + "; Позиция: " + e.LinePosition;
				}
				
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
		
		/// <summary>
		/// Валидация fb2, fb2.zip ли fbz файлов по пути FilePath
		/// </summary>
		public string ValidatingFB2File( string FilePath ) {
			return validate( FilePath, Settings.Settings.SchemePath );
		}
		
	}
}
