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
		// Разное
		private string m_sFromFilePath	= "";	// путь к анализируемой книге
		private string m_sValid			= "";	// Валидность Книги
		private string m_sFileLength	= "";	// Размер fb2-файла
		// TitleInfo
		private string m_sBookTitle		= "";	// Название Книги
		private IList<Genre> m_Genres	= null;	// Список Жанров Книги
		private string m_sLang			= "";	// Язык
		private string m_sSrcLang		= "";	// Язык оригинала
		private IList<Author> m_Authors	= null;	// Список Авторов Книги
		private string m_sDateText		= "";	// Дата написания Книги: Текст
		private string m_sDateValue		= "";	// Дата написания Книги: Значение
		private string m_sKeywords		= null;	// Ключевые слова
		private string m_sCoverpage		= "";	// Обложки
		// DocumentInfo
		private string m_sID			= "";	// ID Книги
		private string m_sVersion		= "";	// Версия fb2-файла
		private string m_sFB2DateText	= "";	// Дата создания fb2: Текст
		private string m_sFB2DateValue	= "";	// Дата создания fb2: Значение
		private string m_sProgramms		= "";	// Использованные программы
		private string m_sOCR			= "";	// OCR
		private IList<string> m_URLs	= null;	// URLs
		private IList<Author> m_FB2Authors	= null;	// Список Авторов fb2-файла
		#endregion
		
		public BookData( string sFromFilePath )
		{
			m_sFromFilePath = sFromFilePath;
			m_fb2 = new fB2Parser( sFromFilePath );
		}
		
		#region Свойства класса
		#region Разное
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
		#endregion
		
		#region TitleInfo
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
		
		public virtual string Lang {
			get {
				if( m_fb2.GetTitleInfo().Lang != null  ) {
					m_sLang = m_fb2.GetTitleInfo().Lang;
				} else {
					m_sLang = "Отсутствует";
				}
				return m_sLang;
			}
        }
		
		public virtual string SrcLang {
			get {
				if( m_fb2.GetTitleInfo().SrcLang != null  ) {
					m_sSrcLang = m_fb2.GetTitleInfo().SrcLang;
				} else {
					m_sSrcLang = "Отсутствует";
				}
				return m_sSrcLang;
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
		
		public virtual string DateText {
			get {
				if( m_fb2.GetTitleInfo().Date != null && m_fb2.GetTitleInfo().Date.Text != null ) {
					m_sDateText = m_fb2.GetTitleInfo().Date.Text;
				} else {
					m_sDateText = "Отсутствует";
				}
				return m_sDateText;
			}
        }
		
		public virtual string DateValue {
			get {
				if( m_fb2.GetTitleInfo().Date != null && m_fb2.GetTitleInfo().Date.Value != null ) {
					m_sDateValue = m_fb2.GetTitleInfo().Date.Value;
				} else {
					m_sDateValue = "Отсутствует";
				}
				return m_sDateValue;
			}
        }
		
		public virtual string Keywords {
			get {
				if( m_fb2.GetTitleInfo().Keywords != null && m_fb2.GetTitleInfo().Keywords.Value != null ) {
					m_sKeywords = m_fb2.GetTitleInfo().Keywords.Value;
				} else {
					m_sKeywords = "Отсутствуют";
				}
				return m_sKeywords;
			}
        }
		
		public virtual string Coverpage {
			get {
				if( m_fb2.GetTitleInfo().Coverpage != null && m_fb2.GetTitleInfo().Coverpage.Value != null ) {
					m_sCoverpage = m_fb2.GetTitleInfo().Coverpage.Value;
				} else {
					m_sCoverpage = "Отсутствует";
				}
				return m_sCoverpage;
			}
        }
		#endregion
		
		#region DocumentInfo
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
		
		public virtual string FB2DateText {
			get {
				if( m_fb2.GetDocumentInfo().Date != null && m_fb2.GetDocumentInfo().Date.Text != null ) {
					m_sFB2DateText = m_fb2.GetDocumentInfo().Date.Text;
				} else {
					m_sFB2DateText = "Отсутствует";
				}
				return m_sFB2DateText;
			}
        }
		
		public virtual string FB2DateValue {
			get {
				if( m_fb2.GetDocumentInfo().Date != null && m_fb2.GetDocumentInfo().Date.Value != null ) {
					m_sFB2DateValue = m_fb2.GetDocumentInfo().Date.Value;
				} else {
					m_sFB2DateValue = "Отсутствует";
				}
				return m_sFB2DateValue;
			}
        }
		
		public virtual string Programms {
			get {
				if( m_fb2.GetDocumentInfo().ProgramUsed != null && m_fb2.GetDocumentInfo().ProgramUsed.Value != null ) {
					m_sProgramms = m_fb2.GetDocumentInfo().ProgramUsed.Value;
				} else {
					m_sProgramms = "Отсутствует";
				}
				return m_sProgramms;
			}
        }
		
		public virtual string OCR {
			get {
				if( m_fb2.GetDocumentInfo().SrcOcr != null && m_fb2.GetDocumentInfo().SrcOcr.Value != null ) {
					m_sOCR = m_fb2.GetDocumentInfo().SrcOcr.Value;
				} else {
					m_sOCR = "Отсутствует";
				}
				return m_sOCR;
			}
        }

		public virtual IList<string> URLs {
			get {
				if( m_fb2.GetDocumentInfo().SrcUrls != null ) {
					m_URLs = m_fb2.GetDocumentInfo().SrcUrls;
				} else {
					m_URLs = null;
				}
				return m_URLs;
			}
        }
		
		public virtual IList<Author> FB2Authors {
			get {
				if( m_fb2.GetDocumentInfo().Authors != null ) {
					m_FB2Authors = m_fb2.GetDocumentInfo().Authors;
				} else {
					m_FB2Authors = null;
				}
				return m_FB2Authors;
			}
        }
		#endregion
		
		#endregion
	}
}
