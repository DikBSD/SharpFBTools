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

using Core.Common;

using SharpZipLibWorker = Core.Common.SharpZipLibWorker;
using FilesWorker		= Core.Common.FilesWorker;

namespace Core.FB2Parser
{
	/// <summary>
	/// Валидатор fb2, fb2.zip ли fbz файлов по пути FilePath
	/// </summary>
	public class FB2Validator
	{
		#region Закрытые данные класса
		private const string _aFB20Namespace	= "http://www.gribuser.ru/xml/fictionbook/2.0";
		private const string _aFB21Namespace	= "http://www.gribuser.ru/xml/fictionbook/2.1";
		private readonly static SharpZipLibWorker	_sharpZipLib	= new SharpZipLibWorker();
		private readonly static string				_TempDir		= Settings.Settings.TempDirPath;
		#endregion
		
		public FB2Validator()
		{
		}

		/// <summary>
		/// Валидация fb2, fb2.zip ли fbz файлов по пути FilePath
		/// </summary>
		/// <returns>Пустая строка, файл валиден; Строка с сообщением, если файл невалиден</returns>
		public string ValidatingFB2File( string FilePath ) {
			bool IsZip = false;
			string Result = validate( FilePath, Settings.Settings.SchemePath, ref IsZip );
			if ( IsZip ) //  удаляем временные файлы только, если проверяли архив
				FilesWorker.RemoveDir( _TempDir );
			return Result;
		}
		
		/// <summary>
		/// Валидация fb2, fb2.zip ли fbz файлов по пути FilePath, согласно схеме SchemePath
		/// </summary>
		/// <param name="FilePath">Путь к проверяемому файлу</param>
		/// <param name="SchemePath">Путь к схеме fb2у</param>
		/// <param name="IsZip">Ссылка, проверяемый файл - архив (true)?</param>
		/// <returns>Пустая строка, файл валиден; Строка с сообщением, если файл невалиден</returns>
		private string validate( string FilePath, string SchemePath, ref bool IsZip ) {
			string [] files = null;
			if ( FilesWorker.isFB2Archive( FilePath ) ) {
				IsZip = true;
				_sharpZipLib.UnZipFB2Files(FilePath, _TempDir);
				files = Directory.GetFiles( _TempDir );
			}
			string FB2Path = ( files != null && files.Length > 0 ) ? files[0] : FilePath;
			XmlDocument xmlDoc = new XmlDocument();
			try {
				xmlDoc.Load( FB2Path );
				// Проверка наличия атрибутов корневого тега <FictionBook
				int FictionBookTagIndex = xmlDoc.InnerXml.IndexOf( "<FictionBook", StringComparison.CurrentCulture );
				if ( FictionBookTagIndex != -1 ) {
					Regex regex  = new Regex( "<FictionBook [^>]+>", RegexOptions.None );
					Match m = regex.Match( xmlDoc.InnerXml );
					if ( m.Success ) {
						string FBookTag = m.Value;
						int xmlnsIndex = FBookTag.IndexOf( "xmlns=\"http://www.gribuser.ru/xml/fictionbook/2.", StringComparison.CurrentCulture  );
						int xmlnsLinkIndex = FBookTag.IndexOf( "=\"http://www.w3.org/1999/xlink\"", StringComparison.CurrentCulture  );
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
					} else {
						regDesc = new Regex( "(?:<description[^/]+?(?:\"[^\"]*\"|'[^']*')?>)", RegexOptions.Multiline | RegexOptions.Singleline );
						mDesc = regDesc.Match( xmlDoc.InnerXml );
						return mDesc.Success ?
							string.Format( "Файл: {0}\r\nВ теге <description> присутствует(ет) аттрибут(ы), что недопустимо по стандарту FictionBook", FilePath )
							:
							string.Format( "Файл: {0}\r\nОтсутствует раздел описания книги <description>", FilePath );
					}
				} catch ( RegexMatchTimeoutException ex ) {
					Debug.DebugMessage(
						FilePath, ex,
						"FB2Validator.validate(): Проверка обязательных разделов fb2 структуры. Исключение RegexMatchTimeoutException."
					);
				} catch ( Exception ex ) {
					Debug.DebugMessage(
						FilePath, ex,
						"FB2Validator.validate(): Проверка обязательных разделов fb2 структуры. Исключение Exception."
					);
				}
				
				if ( !string.IsNullOrEmpty( TI ) ) {
					// Проверка <genre>
					if ( TI.IndexOf("<genre", StringComparison.CurrentCulture ) == -1 )
						return string.Format( "Файл: {0}\r\nОтсутствует тег <genre> книги", FilePath );
					else {
						Regex regGenre = new Regex( @"(<genre ?/>)|(<genre></genre>)", RegexOptions.None );
						Match mGenre = regGenre.Match( TI );
						if ( mGenre.Success )
							return string.Format( "Файл: {0}\r\nТег <genre> книги 'пустой'", FilePath );
					}
					
					// Проверка <lang>
					if ( TI.IndexOf("<lang", StringComparison.CurrentCulture ) == -1 )
						return string.Format( "Файл: {0}\r\nОтсутствует тег <lang> книги", FilePath );
					else {
						Regex regLang = new Regex( @"(<lang ?/>)|(<lang></lang>)", RegexOptions.None );
						Match mLang = regLang.Match( TI );
						if ( mLang.Success )
							return string.Format( "Файл: {0}\r\nТег <lang> книги 'пустой'", FilePath );
						else {
							regLang = new Regex( @"(?<=<lang>)[^<]+(?=</lang>)", RegexOptions.None );
							mLang = regLang.Match( TI );
							if ( mLang.Success ) {
								if ( mLang.Value.Length > 2 )
									return string.Format( "Файл: {0}\r\nТег Название языка книги (тег <lang>) не может быть более 2 символов.\r\nЗначение тега <lang>: '{1}'", FilePath, mLang.Value );
							}
						}
					}
					
					// Проверка <author>
					if ( TI.IndexOf("<author", StringComparison.CurrentCulture ) == -1 )
						return string.Format( "Файл: {0}\r\nОтсутствует тег <author> Автора книги", FilePath );
					else {
						Regex regAuthor = new Regex( @"(<author ?/>)|(<author></author>)", RegexOptions.None );
						Match mAuthor = regAuthor.Match( TI );
						if ( mAuthor.Success )
							return string.Format( "Файл: {0}\r\nТег <author> Автора книги 'пустой'", FilePath );
					}
					
					// Проверка <book-title>
					if ( TI.IndexOf("<book-title", StringComparison.CurrentCulture ) == -1 )
						return string.Format( "Файл: {0}\r\nОтсутствует тег <book-title> Названия книги", FilePath );
					else {
						Regex regBT = new Regex( @"(<book-title ?/>)|(<book-title></book-title>)", RegexOptions.None );
						Match mBT = regBT.Match( TI );
						if ( mBT.Success )
							return string.Format( "Файл: {0}\r\nТег <book-title> Названия книги 'пустой'", FilePath );
					}
				} else {
					return string.Format( "Файл: {0}\r\nОтсутствует раздел описания книги <title-info>", FilePath );
				}
				
				if ( !string.IsNullOrEmpty( DI ) ) {
					// Проверка id книги
					if ( DI.IndexOf("<id", StringComparison.CurrentCulture ) == -1 )
						return "Файл: " + FilePath + "\r\nОтсутствует идентификатор книги тег <id>";
					else {
						Regex regID = new Regex( @"(<id ?/>)|(<id></id>)", RegexOptions.None );
						Match mID = regID.Match( DI );
						if ( mID.Success )
							return string.Format( "Файл: {0}\r\nИдентификатор книги тег <id> 'пустой'", FilePath );
					}
				} else {
					return string.Format( "Файл: {0}\r\nОтсутствует раздел описания книги <document-info>", FilePath );
				}
				
			} catch ( Exception ex ) {
				return string.Format( "{0}\r\n\r\nФайл: {1}", ex.Message, FilePath );
			}
			
			Cursor.Current = Cursors.WaitCursor;
			string fb2FileNamespaceURI = xmlDoc.DocumentElement.NamespaceURI;
			using (Stream xmlSchemeFile = new FileStream( SchemePath, FileMode.Open ) ) {
				XmlSchemaSet sc = new XmlSchemaSet();
				try {
					if( fb2FileNamespaceURI.Equals( _aFB21Namespace ) )
						sc.Add( _aFB21Namespace, XmlReader.Create( xmlSchemeFile ) );
					else
						sc.Add( _aFB20Namespace, XmlReader.Create( xmlSchemeFile ) );
				} catch ( System.Xml.Schema.XmlSchemaException ex ) {
					return string.Format(
						"Файл: {0}\r\n{1}\r\nСтрока: {2}; Позиция: {3}", FilePath, ex.Message,  ex.LineNumber, ex.LinePosition
					);
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
				} catch ( System.Xml.Schema.XmlSchemaException ex ) {
					reader.Close();
					Cursor.Current = Cursors.Default;
					return string.Format(
						"Файл: {0}\r\n{1}\r\nСтрока: {2}; Позиция: {3}", FilePath, ex.Message,  ex.LineNumber, ex.LinePosition
					);
				} catch ( Exception ex ) {
					reader.Close();
					Cursor.Current = Cursors.Default;
					return string.Format( "Файл: {0}\r\n{1}", FilePath, ex.Message );
				}
			}
		}
	}
}
