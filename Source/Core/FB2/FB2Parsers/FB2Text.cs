/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 05.10.2015
 * Время: 7:06
 * License: GPL 2.1
 */
using System;
using System.Text.RegularExpressions;
using System.Text;
using System.IO;
using System.Windows.Forms;

using Core.AutoCorrector;

namespace Core.FB2.FB2Parsers
{
	/// <summary>
	/// fb2 Файл в виде реальных текстовых частей
	/// В случае невозможности чтения файла "по частям", генерируется исключение
	/// </summary>
	public class FB2Text
	{
		private readonly string _FilePath = string.Empty;
		private string _Encoding = string.Empty;
		private string _StartTags = string.Empty;
		private string _Description = string.Empty;
		private string _Bodies = string.Empty;
		private string _Binaries = string.Empty;
		private const string _BodysProxy = "\r\n<body><section><empty-line/></section></body>\r\n";
		private bool _ProxyMode = false;
		
		public FB2Text( string FilePath, bool onlyDescription = false )
		{
			_FilePath = FilePath;
			_Encoding = getEncoding();
			if ( !onlyDescription )
				loadFromFile();
			else
				loadDescriptionOnlyFromFile();
			
			try {
				_StartTags = _Description.Substring( 0, _Description.IndexOf("<description") );
			} catch ( Exception ex ) {
				throw new Exception( string.Format("Файл: {0}\r\nСтруктура раздела <description> сильно искажена.\r\n{1}\r\n", _FilePath, ex.Message)  );
			}
			
			// предварительная обязательная обработка
			preWork();
		}
		
		public virtual bool ProxyMode {
			get { return _ProxyMode; }
			set { _ProxyMode = value; }
		}
		
		public virtual string FB2Encoding  {
			get { return _Encoding; }
		}
		
		public virtual string FB2FilePath  {
			get { return _FilePath; }
		}

		public virtual bool DescriptionExists {
			get { return string.IsNullOrWhiteSpace( _Description ); }
		}

		public virtual string StartTags  {
			get { return _StartTags; }
		}
		
		public virtual string Description {
			get { return _Description; }
			set { _Description = value; }
		}
		
		public virtual bool BodiesExists {
			get { return !string.IsNullOrWhiteSpace( _Bodies ); }
		}
		
		public virtual string Bodies {
			get {
				return  _ProxyMode ? _BodysProxy : _Bodies;
			}
			set { _Bodies = value; }
		}

		public virtual bool BinariesExists {
			get { return !string.IsNullOrWhiteSpace( _Binaries ); }
		}
		
		public virtual string Binaries {
			get { return _Binaries; }
			set { _Binaries = value; }
		}
		
		public virtual string FictionBoocEndTag {
			get { return "</FictionBook>"; }
		}
		
		public string toXML() {
			return _Description + ( _ProxyMode ? _BodysProxy : _Bodies ) + _Binaries + FictionBoocEndTag;
		}
		
		public void saveFile() {
			saveFile( _FilePath );
		}
		public void saveToFile( string FilePath ) {
			saveFile( FilePath );
		}
		
		#region Закрытые вспомогательные методы и свойства
		private void saveFile( string FilePath ) {
			using (
				StreamWriter writer = new StreamWriter(
					FilePath, false, Encoding.GetEncoding( _Encoding ) )
			) {
				writer.Write( toXML() );
			}
		}
		
		private void loadFromFile() {
			string InputString = string.Empty;
			using (StreamReader reader = new StreamReader( File.OpenRead (_FilePath), Encoding.GetEncoding(_Encoding) ) ) {
				InputString = reader.ReadToEnd();
			}
			makeFB2Part( ref InputString );
		}
		
		private void loadDescriptionOnlyFromFile() {
			StringBuilder sb = new StringBuilder();
			using ( StreamReader sr = new StreamReader( File.OpenRead(_FilePath), Encoding.GetEncoding(_Encoding) ) ) {
				string input = string.Empty;
				string DescEndTag = "</description>";
				while (sr.Peek() >= 0) {
					input = sr.ReadLine();
					int index = input.IndexOf( DescEndTag );
					if ( index > -1 ) {
						sb.Append( input.Substring( 0, index ) );
						sb.Append( input.Substring( index, DescEndTag.Length ) );
						break;
					}
					sb.Append( input );
				}
			}
			_Description = sb.ToString();
			string InputString = _Description;
			makeFB2Part( ref InputString );
			ProxyMode = true;
		}
		
		private void makeFB2Part( ref string InputString ) {
			// реконструкция кодировки, если ее нет
			if ( InputString.IndexOf( "<?xml version=" ) == -1 )
				InputString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + InputString;
			
			//  правка тега <FictionBook
			int FictionBookTagIndex = InputString.IndexOf( "<FictionBook" );
			string FictionBookTag = "<FictionBook xmlns=\"http://www.gribuser.ru/xml/fictionbook/2.0\" xmlns:l=\"http://www.w3.org/1999/xlink\">";
			if ( FictionBookTagIndex == -1 ) {
				// нет тега <FictionBook
				int index = InputString.LastIndexOf( '>' );
				string left = InputString.Substring( 0, index );
				string right = InputString.Substring( index );
				InputString = left + " " + FictionBookTag + right;
			} else {
				// тег <FictionBook есть
				Regex regex  = new Regex( "<FictionBook [^>]+>", RegexOptions.None );
				Match m = regex.Match( InputString );
				if ( m.Success ) {
					string FBookTag = m.Value;
					int xmlnsIndex = FBookTag.IndexOf( "xmlns=\"http://www.gribuser.ru/xml/fictionbook/2." );
					int xmlnsLinkIndex = FBookTag.IndexOf( "=\"http://www.w3.org/1999/xlink\"" );
					if ( xmlnsIndex == -1 || xmlnsLinkIndex == -1 ) {
						try {
							InputString = Regex.Replace(
								InputString, "<FictionBook [^>]+>", FictionBookTag, RegexOptions.None
							);
						} catch ( RegexMatchTimeoutException /*ex*/ ) {}
					}
				}
			}
			
			string DescCloseTag = "</description>";
			int IndexDescriptionEnd = InputString.IndexOf( DescCloseTag ) + DescCloseTag.Length;
			int IndexFirstBody = InputString.IndexOf( "<body" );
			int IndexFirstBinary = InputString.IndexOf( "<binary " );
			int IndexFictionBookEndTag = InputString.IndexOf( "</FictionBook>" );
			if ( IndexFictionBookEndTag == -1 )
				IndexFictionBookEndTag = InputString.Length;
			
			if ( IndexDescriptionEnd != -1 ) {
				_Description = InputString.Substring( 0, IndexDescriptionEnd );
				if ( _Encoding.Equals( "windows-1251" ) && isUnicodeCharExists() ) {
					_Encoding = "UTF-8";
					_Description = Regex.Replace(
						_Description, "(?<=encoding=\").+?(?=\")",
						"UTF-8", RegexOptions.IgnoreCase
					);
				}
				
				try {
					if ( IndexFirstBody != -1 ) {
						if ( IndexFirstBinary != -1 )
							_Bodies = InputString.Substring( IndexFirstBody, IndexFirstBinary - IndexFirstBody );
						else
							_Bodies = InputString.Substring( IndexFirstBody, IndexFictionBookEndTag - IndexFirstBody );
					}
					
					if ( IndexFirstBinary != -1 ) {
						_Binaries = InputString.Substring( IndexFirstBinary, IndexFictionBookEndTag - IndexFirstBinary );
					}
					Match mBody = Regex.Match( _Binaries, "<body +?name=\"notes\">", RegexOptions.IgnoreCase );
					int indexBody = mBody.Index;
					string BodyNotes = string.Empty;
					if ( indexBody > 0 ) {
						Match mEndBody = Regex.Match( _Binaries, "</body>", RegexOptions.IgnoreCase );
						int indexEndBody = mEndBody.Index;
						if ( indexEndBody > 0 ) {
							BodyNotes = _Binaries.Substring( indexBody, indexEndBody - indexBody + 7 );
							_Bodies += BodyNotes;
							_Binaries = _Binaries.Remove( indexBody, indexEndBody - indexBody + 7 );
						}
					}

					if ( indexBody == 0 ) {
						if ( IndexFirstBody != -1 ) {
							if ( _Bodies.IndexOf( "</body>", _Bodies.Length - 50 ) == -1 )
								_Bodies += "</body>";
						}
						if ( IndexFirstBinary != -1 ) {
							if ( _Binaries.IndexOf( "</body>", _Binaries.Length - 50 ) != -1 )
								_Binaries = _Binaries.Replace( "</body>", string.Empty );
						}
					}
				} catch {
					throw new Exception( string.Format("Структура файла {0} сильно искажена.", _FilePath) );
				}

			}
			InputString = string.Empty;
		}
		
		private string getEncoding() {
			string encoding = "UTF-8";
			string str = string.Empty;
			using ( StreamReader reader = File.OpenText( _FilePath ) ) {
				str = reader.ReadLine();
			}
			
			if ( string.IsNullOrWhiteSpace( str ) || str.Length == 0 )
				return encoding;
			
			Match match = Regex.Match( str, "(?<=encoding=\").+?(?=\")", RegexOptions.IgnoreCase);
			if ( match.Success )
				encoding = match.Value;
			if ( encoding.ToLower() == "wutf-8" || encoding.ToLower() == "utf8" )
				encoding = "utf-8";
			return encoding;
		}
		
		// есть ли в тексте Юникодные символы
		private bool isUnicodeCharExists() {
			string template = "(&#(x([0-9]|[A-F]){1,4})|([0-9]){1,3});";
			bool res = Regex.IsMatch( _Description, template );
			if ( !_ProxyMode )
				res |= Regex.IsMatch( _Bodies, template );
			return res;
		}
		
		// предварительная обязательная обработка
		private void preWork() {
			Regex regex = new Regex( FB2CleanCode.getRegAmpString() ); // пропускае юникод, символы в десятичной кодировке и меняем уголки
			/* удаление недопустимых символов */
			_Description = FB2CleanCode.deleteIllegalCharacters(
				/* обработка & */
				regex.Replace( _Description, "&amp;" )
			);
			_Bodies = FB2CleanCode.deleteIllegalCharacters(
				/* обработка & */
				regex.Replace( _Bodies, "&amp;" )
			);
			// обработка неверного значения кодировки файла
			regex = new Regex( "(?<=encoding=\")(?:(?:wutf-8)|(?:utf8))(?=\")", RegexOptions.IgnoreCase );
			_Description = regex.Replace( _Description, "utf-8" );
			
			// обработка NameSpace
			int FictionBookTagIndex = _StartTags.IndexOf( "<FictionBook" );
			string xmlns = "xmlns:l";
			regex = new Regex( "(?:xmlns:xlink)", RegexOptions.IgnoreCase );
			Match m = regex.Match( _Description );
			if ( m.Success )
				xmlns = m.Value;
			else {
				m = regex.Match( _Bodies );
				if ( m.Success )
					xmlns = m.Value;
				else {
					regex = new Regex( "(?:xmlns:l)", RegexOptions.IgnoreCase );
					if ( m.Success )
						xmlns = m.Value;
					else {
						m = regex.Match( _Bodies );
						if ( m.Success )
							xmlns = m.Value;
					}
				}
			}
			string xmlnsLink_q = xmlns + "=\"http://www.w3.org/1999/xlink\"";
			if ( _StartTags.IndexOf( "='http://www.w3.org/1999/xlink'" ) == -1 ) {
				if ( _StartTags.IndexOf( xmlnsLink_q ) == -1 ) {
					if ( FictionBookTagIndex != -1) {
						int index = _StartTags.LastIndexOf( '>' );
						string left = _StartTags.Substring( 0, index );
						string right = _StartTags.Substring( index );
						string newStartTags = left + " " + xmlnsLink_q + right;
						_Description = _Description.Replace( _StartTags, newStartTags );
						_StartTags = newStartTags;
					}
				}
			}
		}
		#endregion
	}
}
