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
using System.Collections.Generic;
using System.Windows.Forms;

using Core.AutoCorrector;
using Core.Common;

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
		
		private const string _MessageTitle = "Автокорректор";
		
		/// <summary>
		/// Конструктор
		/// </summary>
		/// <param name="FilePath">Путь к обрабатываемой книге расширения .fb2</param>
		/// <param name="onlyDescription">Загружать только description</param>
		public FB2Text( string FilePath, bool onlyDescription = false )
		{
			_FilePath = FilePath;
			_Encoding = getEncoding();
			if ( !onlyDescription )
				loadFromFile(); // загрузка всего fb2-файла
			else
				loadDescriptionOnlyFromFile(); // загрузка только раздела description fb2-файла
			
			try {
				_StartTags = _Description.Substring(
					0, _Description.IndexOf("<description", StringComparison.CurrentCulture)
				);
			} catch ( Exception ex ) {
				Debug.DebugMessage(
					null, ex, "FB2Text (Конструктор):\r\nfb2 Файл в виде реальных текстовых частей.\r\nОпределение начала тега <description>"
				);
				throw new Exception(
					string.Format("Файл: {0}\r\nСтруктура раздела <description> сильно искажена.\r\n{1}\r\n", _FilePath, ex.Message)
				);
			}
			
			// предварительная обязательная обработка
			preWork();
		}
		
		/// <summary>
		/// Proxy режим ("пустышка" вместо раздела &lt;body&gt;)
		/// </summary>
		/// <returns>true, если включен Proxy режим; false - если Proxy режим выключен</returns>
		public virtual bool ProxyMode {
			get { return _ProxyMode; }
			set { _ProxyMode = value; }
		}
		
		/// <summary>
		/// Кодировка fb2-файла
		/// </summary>
		/// <returns>Строка типа string, содержащая кодировку fb2-файла, напр., utf-8</returns>
		public virtual string FB2Encoding  {
			get { return _Encoding; }
		}
		
		/// <summary>
		/// Путь к fb2-файлу
		/// </summary>
		/// <returns>Строка типа string, содержащая путь к fb2-файлу</returns>
		public virtual string FB2FilePath  {
			get { return _FilePath; }
		}

		/// <summary>
		/// Есть ли раздел &lt;description&gt;
		/// </summary>
		/// <returns>true, если раздел &lt;description&gt; есть; false - если нет раздела &lt;description&gt;</returns>
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
		
		/// <summary>
		/// Есть ли хоть один раздел &lt;body&gt;
		/// </summary>
		/// <returns>true, если хоть один раздел &lt;body&gt; есть; false - если нет ни одного раздела &lt;body&gt;</returns>
		public virtual bool BodiesExists {
			get { return !string.IsNullOrWhiteSpace( _Bodies ); }
		}
		
		/// <summary>
		/// fb2-структура всех разделов &lt;body&gt; из данных экземплара класса FB2Text, в зависимости от того, включен ли Proxy режим.
		/// Если Proxy режим включен (true), то вместо тела (body) книги подставляется body-"пустышка"
		/// </summary>
		/// <returns>Строка типа string  fb2-структурой раздела &lt;body&gt;</returns>
		public virtual string Bodies {
			get {
				return  _ProxyMode ? _BodysProxy : _Bodies;
			}
			set { _Bodies = value; }
		}

		/// <summary>
		/// Есть ли хоть один раздел &lt;binary&gt;
		/// </summary>
		/// <returns>true, если хоть один раздел &lt;binary&gt; есть; false - если нет ни одного раздела &lt;binary&gt;</returns>
		public virtual bool BinariesExists {
			get { return !string.IsNullOrWhiteSpace( _Binaries ); }
		}
		
		public virtual string Binaries {
			get { return _Binaries; }
			set { _Binaries = value; }
		}
		
		/// <summary>
		/// Нказвание завершающего родительского тега FictionBook
		/// </summary>
		/// <returns>Строка типа string со значением &lt;/FictionBook&gt;</returns>
		public virtual string FictionBoocEndTag {
			get { return "</FictionBook>"; }
		}
		
		/// <summary>
		/// Формирование fb2-структуры из данных экземплара класса FB2Text, в зависимости от того, включен ли Proxy режим.
		/// Если Proxy режим включен (true), то вместо тела (body) книги подставляется body-"пустышка"
		/// </summary>
		/// <returns>Строка типа string со всей fb2-структурой)</returns>
		public string toXML() {
			return _Description + ( _ProxyMode ? _BodysProxy : _Bodies ) + _Binaries + FictionBoocEndTag;
		}
		
		/// <summary>
		/// Сохранение данных экземпляра класса FB2Text в xml структуру fb2 файла
		/// Путь к fb2-файлу - по-умолчанию, прописанный в приватной переменной _FilePath класса FB2Text
		/// </summary>
		public void saveFile() {
			saveFile( _FilePath );
		}
		/// <summary>
		/// Сохранение данных экземпляра класса FB2Text в xml структуру fb2 файла
		/// </summary>
		/// <param name="FilePath">Путь к сохраняемому fb2-файлу</param>
		public void saveToFile( string FilePath ) {
			saveFile( FilePath );
		}
		
		#region Закрытые вспомогательные методы и свойства
		/// <summary>
		/// Предварительная обязательная обработка (&, wutf-8, utf8)
		/// </summary>
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
		}
		
		/// <summary>
		/// Определение кодировки fb2-файла по его заголовку)
		/// </summary>
		/// <returns>Строка типа string со значение кодитровки (напр., utf-8)</returns>
		private string getEncoding() {
			string encoding = "UTF-8";
			string str = string.Empty;
			using ( StreamReader reader = File.OpenText( _FilePath ) ) {
				str = reader.ReadLine();
			}
			
			if ( string.IsNullOrWhiteSpace( str ) || str.Length == 0 )
				return encoding;
			
			Match match = Regex.Match( str, "(?<=encoding=\").+?(?=\")", RegexOptions.IgnoreCase );
			if ( match.Success )
				encoding = match.Value;
			if ( encoding.ToLower() == "wutf-8" || encoding.ToLower() == "utf8" )
				encoding = "utf-8";
			return encoding;
		}
		
		/// <summary>
		/// Определение, есть ли в тексте Юникодные символы
		/// </summary>
		/// <returns>true, если в тексте fb2 файла есть Юникодные символы; false - если их нет</returns>
		private bool isUnicodeCharExists() {
			const string template = "(&#(x([0-9]|[A-F]){1,4})|([0-9]){1,3});";
			bool res = Regex.IsMatch( _Description, template );
			if ( !_ProxyMode )
				res |= Regex.IsMatch( _Bodies, template );
			return res;
		}
		
		/// <summary>
		/// Сохранение данных экземпляра класса FB2Text в xml структуру fb2 файла
		/// </summary>
		/// <param name="FilePath">Путь к сохраняемому fb2-файлу</param>
		private void saveFile( string FilePath ) {
			using (
				StreamWriter writer = new StreamWriter(
					FilePath, false, Encoding.GetEncoding( _Encoding ) )
			) {
				writer.Write( toXML() );
			}
		}
		
		/// <summary>
		/// Загрузка всего текста fb2-файла
		/// </summary>
		private void loadFromFile() {
			string fb2FileString = string.Empty;
			using (StreamReader reader = new StreamReader( File.OpenRead (_FilePath), Encoding.GetEncoding(_Encoding) ) ) {
				fb2FileString = reader.ReadToEnd();
			}
			// Создание текста разделов fb2-файла в переменных класса FB2Text
			makeFB2Part( ref fb2FileString );
		}
		
		/// <summary>
		/// Загрузка текста только раздела description fb2-файла
		/// </summary>
		private void loadDescriptionOnlyFromFile() {
			StringBuilder sb = new StringBuilder();
			using ( StreamReader sr = new StreamReader( File.OpenRead(_FilePath), Encoding.GetEncoding(_Encoding) ) ) {
				string input = string.Empty;
				const string DescEndTag = "</description>";
				while (sr.Peek() >= 0) {
					input = sr.ReadLine();
					int index = input.IndexOf( DescEndTag, StringComparison.CurrentCulture );
					if ( index > -1 ) {
						sb.Append( input.Substring( 0, index ) );
						sb.Append( input.Substring( index, DescEndTag.Length ) );
						break;
					}
					sb.Append( input );
				}
			}
			_Description = sb.ToString();
			string fb2DescriptionString = _Description;
			// Создание текста разделов fb2-файла в переменных класса FB2Text
			makeFB2Part( ref fb2DescriptionString );
			ProxyMode = true;
		}
		
		
		//=======================================================================
		private void _makeFB2Part( ref string fb2FileString ) {
			// реконструкция кодировки, если ее нет
			if ( fb2FileString.IndexOf( "<?xml version=", StringComparison.CurrentCulture ) == -1 )
				fb2FileString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + fb2FileString;
			
			//  правка тега <FictionBook
			int IndexFictionBookEndTag = fb2FileString.IndexOf( "</FictionBook>", StringComparison.CurrentCulture );
			int FictionBookTagIndex = fb2FileString.IndexOf( "<FictionBook", StringComparison.CurrentCulture );
			FictionBookTagCorrector fbtc = new FictionBookTagCorrector( _FilePath );
			if ( FictionBookTagIndex == -1 ) {
				// нет тега <FictionBook
				int index = fb2FileString.LastIndexOf( '>' );
				string left = fb2FileString.Substring( 0, index );
				string right = fb2FileString.Substring( index );
				fb2FileString = left + " " + fbtc.NewFictionBookTag + right;
			} else {
				// тег <FictionBook есть
				// обработка головного тега FictionBook и пространства имен
				fb2FileString = fbtc.StartTagCorrect( fb2FileString );
			}
			
			const string DescCloseTag = "</description>";
			int IndexDescriptionEnd = fb2FileString.IndexOf( DescCloseTag, StringComparison.CurrentCulture ) + DescCloseTag.Length;
			int IndexFirstBodyStart = fb2FileString.IndexOf( "<body", StringComparison.CurrentCulture );
			int IndexFirstBinaryStart = fb2FileString.IndexOf( "<binary ", StringComparison.CurrentCulture );
			
			List<string> BodiesList		= new List<string>(); // список всех <body>
			List<string> BinariesList	= new List<string>(); // список всех <binary>
			
			if ( IndexDescriptionEnd != -1 ) {
				_Description = fb2FileString.Substring( 0, IndexDescriptionEnd );
				if ( _Encoding.Equals( "windows-1251" ) && isUnicodeCharExists() ) {
					_Encoding = "UTF-8";
					try {
						_Description = Regex.Replace(
							_Description, "(?<=encoding=\").+?(?=\")",
							_Encoding, RegexOptions.IgnoreCase
						);
					} catch ( RegexMatchTimeoutException ex ) {
						Debug.DebugMessage(
							_FilePath, ex, "Fb2Text:_makeFB2Part()\r\nОбработка раздела <description>:\r\nОбработка неверного значения кодировки файла. Исключение RegexMatchTimeoutException."
						);
					} catch ( Exception ex ) {
						Debug.DebugMessage(
							_FilePath, ex, "Fb2Text:_makeFB2Part()\r\nОбработка раздела <description>:\r\nОбработка неверного значения кодировки файла. Исключение Exception."
						);
					}
				}
				
				//			Regex regex  = new Regex( "<description>", RegexOptions.IgnoreCase );
//			Match m = regex.Match( XmlDescription );
//			if ( !m.Success ) {
//
//			}
				
				// binary и body могут чередоваться из-за неверно созданной структуры. Поэтому, надо вычленять все binary по порядку, и создавать из них строку. И все body по порядку, и создавать из них строку.
				//1. Берем 1-й тег. Проверка на конец fb2 текста по index тега </FictionBook>
				// теги могут быть открытыми!!! Учитывать это!!!
				//if ( Tag == “<binary ” ) {
				// ищем </binary> и add <binary> текст </binary> в BinariesList.
				// add “\r\n” к BinariesList.
				//} else if ( Tag == “<body ” ) {
				// ищем </body> и add <body> текст </body> в BodiesList.
				// add “\r\n” к BodiesList.
				//} else {
				// что-то иное
				// помещаем это в строку Other и add “\r\n
				//}
				
			}
			
			fb2FileString = string.Empty;
		}
		
		//======================================================================
		
		
		
		/// <summary>
		/// Создание текста разделов fb2-файла в переменных класса FB2Text.
		/// Генерируется Exception, если картинки расположены выше раздела body, или, если есть другое грубое нарушение fb2-структуры.
		/// </summary>
		/// <param name="fb2FileString">Строка текста fb2-файла</param>
		private void makeFB2Part( ref string fb2FileString ) {
			// реконструкция кодировки, если ее нет
			if ( fb2FileString.IndexOf( "<?xml version=", StringComparison.CurrentCulture ) == -1 )
				fb2FileString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + fb2FileString;
			
			//  правка тега <FictionBook
			int FictionBookTagIndex = fb2FileString.IndexOf( "<FictionBook", StringComparison.CurrentCulture );
			FictionBookTagCorrector fbtc = new FictionBookTagCorrector( _FilePath );
			if ( FictionBookTagIndex == -1 ) {
				// нет тега <FictionBook
				int index = fb2FileString.LastIndexOf( '>' );
				string left = fb2FileString.Substring( 0, index );
				string right = fb2FileString.Substring( index );
				fb2FileString = left + " " + fbtc.NewFictionBookTag + right;
			} else {
				// тег <FictionBook есть
				// обработка головного тега FictionBook и пространства имен
				fb2FileString = fbtc.StartTagCorrect( fb2FileString );
			}
			
			const string DescCloseTag = "</description>";
			int IndexDescriptionEnd = fb2FileString.IndexOf( DescCloseTag, StringComparison.CurrentCulture ) + DescCloseTag.Length;
			int IndexFirstBodyStart = fb2FileString.IndexOf( "<body", StringComparison.CurrentCulture );
			int IndexFirstBinaryStart = fb2FileString.IndexOf( "<binary ", StringComparison.CurrentCulture );
			int IndexFictionBookEndTag = fb2FileString.IndexOf( "</FictionBook>", StringComparison.CurrentCulture );
			
			if ( IndexFictionBookEndTag == -1 )
				IndexFictionBookEndTag = fb2FileString.Length;
			
			if ( IndexDescriptionEnd != -1 ) {
				_Description = fb2FileString.Substring( 0, IndexDescriptionEnd );
				if ( _Encoding.Equals( "windows-1251" ) && isUnicodeCharExists() ) {
					_Encoding = "UTF-8";
					try {
						_Description = Regex.Replace(
							_Description, "(?<=encoding=\").+?(?=\")",
							_Encoding, RegexOptions.IgnoreCase
						);
					} catch ( RegexMatchTimeoutException ex ) {
						Debug.DebugMessage(
							_FilePath, ex, "Fb2Text:makeFB2Part()\r\nСоздание текста разделов fb2-файла в переменных класса FB2Text. Исключение RegexMatchTimeoutException."
						);
					} catch ( Exception ex ) {
						Debug.DebugMessage(
							_FilePath, ex, "Fb2Text:makeFB2Part()\r\nСоздание текста разделов fb2-файла в переменных класса FB2Text. Исключение Exception."
						);
					}
				}
				
				try {
					if ( IndexFirstBodyStart != -1 ) {
						// Вычленяем все разделы <body>
						_Bodies = (IndexFirstBinaryStart != -1)
							? fb2FileString.Substring( IndexFirstBodyStart, IndexFirstBinaryStart - IndexFirstBodyStart )
							: fb2FileString.Substring( IndexFirstBodyStart, IndexFictionBookEndTag - IndexFirstBodyStart );
					}

					if ( IndexFirstBinaryStart != -1 ) {
						// Вычленяем все разделы <binary>
						_Binaries = fb2FileString.Substring( IndexFirstBinaryStart, IndexFictionBookEndTag - IndexFirstBinaryStart );
					}
					// Вычленяем все сноски - раздел <body name="notes">
					Match mBody = Regex.Match( _Binaries, "<body +?name=\"notes\">", RegexOptions.IgnoreCase );
					int indexBody = mBody.Index;
					string BodyNotes = string.Empty;
					if ( indexBody > 0 ) {
						// есть раздел сносок
						Match mEndBody = Regex.Match( _Binaries, "</body>", RegexOptions.IgnoreCase );
						int indexEndBody = mEndBody.Index;
						if ( indexEndBody > 0 ) {
							BodyNotes = _Binaries.Substring( indexBody, indexEndBody - indexBody + 7 );
							_Bodies += BodyNotes;
							_Binaries = _Binaries.Remove( indexBody, indexEndBody - indexBody + 7 );
						}
					}

					if ( indexBody == 0 ) {
						// нет раздела сносок
						if ( IndexFirstBodyStart != -1 ) {
							if ( _Bodies.IndexOf( "</body>", _Bodies.Length - 50, StringComparison.CurrentCulture ) == -1 )
								_Bodies += "</body>";
						}
						if ( IndexFirstBinaryStart != -1 ) {
							if ( _Binaries.IndexOf( "</body>", _Binaries.Length - 50, StringComparison.CurrentCulture ) != -1 )
								_Binaries = _Binaries.Replace( "</body>", string.Empty );
						}
					}
				} catch ( Exception ex ) {
					Debug.DebugMessage(
						_FilePath, ex, "Fb2Text:makeFB2Part()\r\nВычленение основных разделов fb2 структуры. Исключение Exception."
					);
					throw new Exception( string.Format("Структура файла {0} сильно искажена.\r\nВозможно, раздел(ы) <binary> картинок расположен(ы) выше раздела тела книги <body>\r\n", _FilePath) );
				}

			}
			fb2FileString = string.Empty;
			
			//TODO Разделы body и notes - отдельно. Новый алгоритмя вычленения разделов
			
			/*// реконструкция кодировки, если ее нет
			if ( fb2FileString.IndexOf( "<?xml version=", StringComparison.CurrentCulture ) == -1 )
				fb2FileString = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>" + fb2FileString;
			
			//  правка тега <FictionBook
			int FictionBookTagIndex = fb2FileString.IndexOf( "<FictionBook", StringComparison.CurrentCulture );
			FictionBookTagCorrector fbtc = new FictionBookTagCorrector();
			if ( FictionBookTagIndex == -1 ) {
				// нет тега <FictionBook
				int index = fb2FileString.LastIndexOf( '>' );
				string left = fb2FileString.Substring( 0, index );
				string right = fb2FileString.Substring( index );
				fb2FileString = left + " " + fbtc.NewFictionBookTag + right;
			} else {
				// тег <FictionBook есть
				// обработка головного тега FictionBook и пространства имен
				fb2FileString = fbtc.StartTagCorrect( fb2FileString );
			}
			
			const string DescCloseTag = "</description>";
			int IndexDescriptionEnd = fb2FileString.IndexOf( DescCloseTag, StringComparison.CurrentCulture ) + DescCloseTag.Length;
			int IndexFirstBodyStart = fb2FileString.IndexOf( "<body", StringComparison.CurrentCulture );
			int IndexFirstBinaryStart = fb2FileString.IndexOf( "<binary ", StringComparison.CurrentCulture );
			int IndexFictionBookEndTag = fb2FileString.IndexOf( "</FictionBook>", StringComparison.CurrentCulture );
			
			if ( IndexFictionBookEndTag == -1 )
				IndexFictionBookEndTag = fb2FileString.Length;
			
			if ( IndexDescriptionEnd != -1 ) {
				// Вычленяем раздел <description>
				_Description = fb2FileString.Substring( 0, IndexDescriptionEnd );
				if ( _Encoding.Equals( "windows-1251" ) && isUnicodeCharExists() ) {
					_Encoding = "UTF-8";
					try {
						_Description = Regex.Replace(
							_Description, "(?<=encoding=\").+?(?=\")",
							"UTF-8", RegexOptions.IgnoreCase
						);
					} catch ( RegexMatchTimeoutException /*ex*/ /*) {}
				}
			}

					          */
					         /*		if ( IndexFirstBinaryStart == -1 ) {
				// Нет ни одного тега <binary>
				// Вычленение разделов ================================================
				try {
					if ( IndexFirstBodyStart != -1 ) {
						// Вычленяем все разделы <body>
						_Bodies = (IndexFirstBinaryStart != -1)
							? fb2FileString.Substring( IndexFirstBodyStart, IndexFirstBinaryStart - IndexFirstBodyStart )
							: fb2FileString.Substring( IndexFirstBodyStart, IndexFictionBookEndTag - IndexFirstBodyStart );
					}

					if ( IndexFirstBinaryStart != -1 ) {
						// Вычленяем все разделы <binary>
						_Binaries = fb2FileString.Substring( IndexFirstBinaryStart, IndexFictionBookEndTag - IndexFirstBinaryStart );
					}

					// Вычленяем все сноски - раздел <body name="notes">
					Match mBody = Regex.Match( _Binaries, "<body +?name=\"notes\">", RegexOptions.IgnoreCase );
					int indexBody = mBody.Index;
					string BodyNotes = string.Empty;
					if ( indexBody > 0 ) {
						// есть раздел сносок
						Match mEndBody = Regex.Match( _Binaries, "</body>", RegexOptions.IgnoreCase );
						int indexEndBody = mEndBody.Index;
						if ( indexEndBody > 0 ) {
							BodyNotes = _Binaries.Substring( indexBody, indexEndBody - indexBody + 7 );
							_Bodies += BodyNotes;
							_Binaries = _Binaries.Remove( indexBody, indexEndBody - indexBody + 7 );
						}
					}
					if ( indexBody == 0 ) {
//						MessageBox.Show(_Bodies);
						// нет раздела сносок
						if ( IndexFirstBodyStart != -1 ) {
							if ( _Bodies.IndexOf( "</body>", _Bodies.Length - 50, StringComparison.CurrentCulture ) == -1 )
								_Bodies += "</body>";
						}
						if ( IndexFirstBinaryStart != -1 ) {
							if ( _Binaries.IndexOf( "</body>", _Binaries.Length - 50, StringComparison.CurrentCulture ) != -1 )
								_Binaries = _Binaries.Replace( "</body>", string.Empty );
						}
					}
				} catch {
					throw new Exception( string.Format("Структура файла {0} сильно искажена.\r\n", _FilePath) );
				}
				// =========================================
			} else {
				// Тег <binary> есть ( IndexFirstBinaryStart > -1 )
				MessageBox.Show("Тег <binary> есть ( IndexFirstBinaryStart > -1 )");
				if ( IndexFirstBodyStart  > IndexFirstBinaryStart ) {
					// Тело книги находится выше 1-й картинки
					// Вычленение разделов ================================================
					try {
						if ( IndexFirstBodyStart != -1 ) {
							// Вычленяем все разделы <body>
							_Bodies = (IndexFirstBinaryStart != -1)
								? fb2FileString.Substring( IndexFirstBodyStart, IndexFirstBinaryStart - IndexFirstBodyStart )
								: fb2FileString.Substring( IndexFirstBodyStart, IndexFictionBookEndTag - IndexFirstBodyStart );
						}

						if ( IndexFirstBinaryStart != -1 ) {
							// Вычленяем все разделы <binary>
							_Binaries = fb2FileString.Substring( IndexFirstBinaryStart, IndexFictionBookEndTag - IndexFirstBinaryStart );
						}

						// Вычленяем все сноски - раздел <body name="notes">
						Match mBody = Regex.Match( _Binaries, "<body +?name=\"notes\">", RegexOptions.IgnoreCase );
						int indexBody = mBody.Index;
						string BodyNotes = string.Empty;
						if ( indexBody > 0 ) {
							// есть раздел сносок
							Match mEndBody = Regex.Match( _Binaries, "</body>", RegexOptions.IgnoreCase );
							int indexEndBody = mEndBody.Index;
							if ( indexEndBody > 0 ) {
								BodyNotes = _Binaries.Substring( indexBody, indexEndBody - indexBody + 7 );
								_Bodies += BodyNotes;
								_Binaries = _Binaries.Remove( indexBody, indexEndBody - indexBody + 7 );
							}
						}
						if ( indexBody == 0 ) {
//							MessageBox.Show(_Bodies);
							// нет раздела сносок
							if ( IndexFirstBodyStart != -1 ) {
								if ( _Bodies.IndexOf( "</body>", _Bodies.Length - 50, StringComparison.CurrentCulture ) == -1 )
									_Bodies += "</body>";
							}
							if ( IndexFirstBinaryStart != -1 ) {
								if ( _Binaries.IndexOf( "</body>", _Binaries.Length - 50, StringComparison.CurrentCulture ) != -1 )
									_Binaries = _Binaries.Replace( "</body>", string.Empty );
							}
						}
					} catch {
						throw new Exception( string.Format("Структура файла {0} сильно искажена.\r\n", _FilePath) );
					}
					// ============================================
				} else {
					MessageBox.Show("IndexFirstBinaryStart="+IndexFirstBinaryStart+"\r\nIndexFirstBody="+IndexFirstBodyStart);
					// IndexFirstBodyStart  < IndexFirstBinaryStart: 1-я Картинка находится выше тела книги (нарушение структуры)
					throw new Exception( string.Format("Грубое нарушение структуры fb2 книги:\r\nКартинки (<binary>) расположены выше раздела тела текста книги <body>\r\nФайл: {0}\r\n", _FilePath) );
					// TODO разделы <body> переставить выше первого <binary>
				}
			}*/
					         
					         /*			if ( IndexFirstBinaryStart == -1 || IndexFirstBodyStart  < IndexFirstBinaryStart ) {
				// Раздел <body> расположен выше Картинкок (<binary>)
				try {
					if ( IndexFirstBodyStart != -1 ) {
						// Вычленяем все разделы <body>
						_Bodies = (IndexFirstBinaryStart != -1)
							? fb2FileString.Substring( IndexFirstBodyStart, IndexFirstBinaryStart - IndexFirstBodyStart )
							: fb2FileString.Substring( IndexFirstBodyStart, IndexFictionBookEndTag - IndexFirstBodyStart );
					}

					if ( IndexFirstBinaryStart != -1 ) {
						// Вычленяем все разделы <binary>
						_Binaries = fb2FileString.Substring( IndexFirstBinaryStart, IndexFictionBookEndTag - IndexFirstBinaryStart );
					}

					// Вычленяем все сноски - раздел <body name="notes">
					Match mBody = Regex.Match( _Binaries, "<body +?name=\"notes\">", RegexOptions.IgnoreCase );
					int indexBody = mBody.Index;
					string BodyNotes = string.Empty;
					if ( indexBody > 0 ) {
						// есть раздел сносок
						Match mEndBody = Regex.Match( _Binaries, "</body>", RegexOptions.IgnoreCase );
						int indexEndBody = mEndBody.Index;
						if ( indexEndBody > 0 ) {
							BodyNotes = _Binaries.Substring( indexBody, indexEndBody - indexBody + 7 );
							_Bodies += BodyNotes;
							_Binaries = _Binaries.Remove( indexBody, indexEndBody - indexBody + 7 );
						}
					}
					if ( indexBody == 0 ) {
						MessageBox.Show(_Bodies);
						// нет раздела сносок
						if ( IndexFirstBodyStart != -1 ) {
							if ( _Bodies.IndexOf( "</body>", _Bodies.Length - 50, StringComparison.CurrentCulture ) == -1 )
								_Bodies += "</body>";
						}
						if ( IndexFirstBinaryStart != -1 ) {
							if ( _Binaries.IndexOf( "</body>", _Binaries.Length - 50, StringComparison.CurrentCulture ) != -1 )
								_Binaries = _Binaries.Replace( "</body>", string.Empty );
						}
					}
				} catch {
					throw new Exception( string.Format("Структура файла {0} сильно искажена.\r\n", _FilePath) );
				}
			} else {
				// IndexFirstBodyStart  > IndexFirstBinaryStart : Картинки (<binary>) расположены выше раздела <body>
				throw new Exception( string.Format("Грубое нарушение структуры fb2 книги:\r\nКартинки (<binary>) расположены выше раздела тела текста книги <body>\r\nФайл: {0}\r\n", _FilePath) );
				// TODO разделы <body> переставить выше первого <binary>
			}*/
//			fb2FileString = string.Empty;
					}
				#endregion
			}
		}
