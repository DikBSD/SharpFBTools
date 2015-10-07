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

namespace Core.FB2.FB2Parsers
{
	/// <summary>
	/// fb2 Файл в виде реальных текстовых частей
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
		
		public FB2Text( string FilePath )
		{
			_FilePath = FilePath;
			_Encoding = getEncoding();
			loadFile();
			_StartTags = _Description.Substring( 0, _Description.IndexOf("<description>") );
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
//		
//		public virtual bool DescriptionExists {
//			get { return string.IsNullOrWhiteSpace( _Description ); }
//		}
//		
		public virtual string StartTags  {
			get { return _StartTags; }
		}
		
		public virtual string Description {
			get { return _Description; }
			set { _Description = value; }
		}
		
		public virtual bool BodiesExists {
			get { return string.IsNullOrWhiteSpace( _Bodies ); }
		}
		
		public virtual string Bodies {
			get {
				return  _ProxyMode ? _BodysProxy : _Bodies;
			}
			set { _Bodies = value; }
		}

		public virtual bool BinariesExists {
			get { return string.IsNullOrWhiteSpace( _Binaries ); }
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
		
		private void saveFile( string FilePath ) {
			using (
				StreamWriter writer = new StreamWriter(
					FilePath, false, Encoding.GetEncoding( _Encoding ) )
			) {
				writer.Write( toXML() );
			}
		}
		
		private void loadFile() {
			string InputString = string.Empty;
			using (StreamReader reader = new StreamReader( File.OpenRead (_FilePath), Encoding.GetEncoding(_Encoding) ) ) {
				InputString = reader.ReadToEnd();
			}

			string DescEndTag = "</description>";
			int IndexDescriptionEnd = InputString.IndexOf( DescEndTag ) + DescEndTag.Length;
			int IndexFirstBody = IndexDescriptionEnd;
			int IndexFirstBinary = InputString.IndexOf( "<binary " );
			int IndexFictionBoocEndTag = InputString.IndexOf( "</FictionBook>" );
			if ( IndexDescriptionEnd != -1 ) {
				_Description = InputString.Substring( 0, IndexDescriptionEnd );
				bool UnicodeCharExists = isUnicodeCharExists();
				if ( _Encoding.Equals( "windows-1251" ) && UnicodeCharExists ) {
					_Encoding = "UTF-8";
					_Description = Regex.Replace(
						_Description, "(?<=encoding=\").+?(?=\")",
						"UTF-8", RegexOptions.IgnoreCase
					);
				}
				if ( IndexFirstBody != -1 ) {
					if ( IndexFirstBinary != -1 )
						_Bodies = InputString.Substring( IndexFirstBody, IndexFirstBinary - IndexFirstBody );
					else
						_Bodies = InputString.Substring( IndexFirstBody, IndexFictionBoocEndTag - IndexFirstBody );
				}
				
				if ( IndexFirstBinary != -1 )
					_Binaries = InputString.Substring( IndexFirstBinary, IndexFictionBoocEndTag - IndexFirstBinary );
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
			return encoding;
		}
		
		private bool isUnicodeCharExists() {
			string template = "(&#(x([0-9]|[A-F]){1,4})|([0-9]){1,3});";
			bool res = Regex.IsMatch( _Description, template );
			if ( !_ProxyMode )
				res |= Regex.IsMatch( _Bodies, template );
			return res;
		}
	}
}
