/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 07.09.2009
 * Time: 15:29
 * 
 * License: GPL 2.1
 */
using System;
using System.IO;
using System.Collections.Generic;

using Core.FB2.Description.Common;
using Core.FB2.Description.TitleInfo;

using fB2Parser 	= Core.FB2.FB2Parsers.FB2Parser;
using FB2Validator	= Core.FB2Parser.FB2Validator;
using filesWorker	= Core.FilesWorker.FilesWorker;

namespace Core.FB2Dublicator
{
	/// <summary>
	/// Description of BookData.
	/// </summary>
	public class BookData
	{
		#region Закрытые данные класса
		private fB2Parser m_fb2			= null; // fb2-парсер
		private string m_sFromFilePath	= "";	// путь к анализируемой книге
		private string m_sValid			= "";	// Валидность Книги
		private string m_sFileLength	= "";	// размер fb2-файла
		private string m_sBookTitle		= "";	// Название Книги
		private string m_sID			= "";	// ID Книги
		private string m_sVersion		= "";	// Версия fb2-файла
		private IList<Author> m_Authors	= null;	// список Авторов Книги
		private IList<Genre> m_Genres	= null;	// список Жанров Книги
		#endregion
		
		public BookData( string sFromFilePath )
		{
			m_sFromFilePath = sFromFilePath;
			m_fb2 = new fB2Parser( sFromFilePath );
		}
		
		#region Свойства класса
		public virtual string FileLength {
			get {
				FileInfo fi = new FileInfo( m_sFromFilePath );
				m_sFileLength = filesWorker.FormatFileLength( fi.Length );
				return m_sFileLength;
			}
        }
		
		public virtual string IsValid {
			get {
				FB2Validator fv2Validator = new FB2Validator();
				m_sValid = fv2Validator.ValidatingFB2File( m_sFromFilePath );
				return m_sValid;
			}
        }
		
		public virtual string ID {
			get {
				if( m_fb2.GetDocumentInfo().ID != null ) {
					m_sID = m_fb2.GetDocumentInfo().ID;
				} else {
					m_sID = "ID Книги отсутствует";
				}
				return m_sID;
			}
        }
		
		public virtual string Version {
			get {
				if( m_fb2.GetDocumentInfo().Version != null ) {
					m_sVersion = m_fb2.GetDocumentInfo().Version;
				} else {
					m_sVersion = "Отсутствует";
				}
				return m_sVersion;
			}
        }
		
		public virtual string BookTitle {
			get {
				if( m_fb2.GetTitleInfo().BookTitle != null && m_fb2.GetTitleInfo().BookTitle.Value != null ) {
					m_sBookTitle = m_fb2.GetTitleInfo().BookTitle.Value;
				} else {
					m_sBookTitle = "Название Книги отсутствует";
				}
				return m_sBookTitle;
			}
        }
		
		public virtual IList<Author> Authors {
			get {
				if( m_fb2.GetTitleInfo().Authors != null ) {
					m_Authors = m_fb2.GetTitleInfo().Authors;
				} else {
					m_Authors = null;
				}
				return m_Authors;
			}
        }
		
		public virtual IList<Genre> Genres {
			get {
				if( m_fb2.GetTitleInfo().Genres != null ) {
					m_Genres = m_fb2.GetTitleInfo().Genres;
				} else {
					m_Genres = null;
				}
				return m_Genres;
			}
        }
		#endregion
	}
}
