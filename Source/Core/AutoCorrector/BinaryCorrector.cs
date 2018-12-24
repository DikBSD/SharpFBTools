/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 04.12.2015
 * Время: 13:47
 * 
 * License: GPL 2.1
 */
using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;

using System.Linq;
using System.Windows.Forms;
using System.IO;
using System.Text;

using Core.Common;

namespace Core.AutoCorrector
{
	/// <summary>
	/// Корректировка атрибутов тега binary
	/// </summary>
	public class BinaryCorrector
	{
		#region Вспомогательные классы
		/// <summary>
		/// Закрытый класс данных на картинку: свойства Название картинки и число данной картинки в блоках binary
		/// </summary>
		private class ImageData {
			private string _ImageName = null;
			private int _Count = 0;
			
			/// <summary>
			/// Конструктор по-умолчанию
			/// </summary>
			public ImageData()
			{
			}
			
			/// <summary>
			/// Конструктор с заданными параметрами
			/// </summary>
			/// <param name="ImageName">Название картинки</param>
			/// <param name="Count">Количество данных картинок</param>
			public ImageData( string ImageName, int Count )
			{
				_ImageName = ImageName;
				_Count = Count;
			}
			
			public virtual string ImageName {
				get { return _ImageName; }
				set { _ImageName = value; }
			}
			public virtual int Count {
				get { return _Count; }
				set { _Count = value; }
			}
		}
		#endregion
		
		private const string _MessageTitle = "Автокорректор";
		
		private string _xmlBinaries = string.Empty;
		private const string _regStringForBinaryTag = "<binary +?(?:(?:content-type=\"image/\\w{3,4}\" id=\"(?:(?:\\w+?\\W?\\w+?)+)+\")|(?:id=\"(?:(\\w+?\\W?\\w+?)+)+\" content-type=\"image/\\w{3,4}\")) ?>";
		//"<binary (?:(?:content-type=\"image/\\w{3,4}\" id=\"\\w+\\.\\w{3,4}\")|(?:id=\"\\w+\\.\\w{3,4}\" content-type=\"image/\\w{3,4}\")) ?>"
		private readonly string _FilePath = string.Empty; // Путь к обрабатываемому файлу
		
		/// <summary>
		/// Конструктор класса BinaryCorrector
		/// </summary>
		/// <param name="FilePath">Путь к обрабатываемому файлу</param>
		/// <param name="xmlBinaries">Строка всех рвзделов Binary в Fb2 формате для корректировки</param>
		public BinaryCorrector( string FilePath, string xmlBinaries )
		{
			_FilePath = FilePath;
			_xmlBinaries = xmlBinaries;
		}
		
		/// <summary>
		/// Корректировка атрибутов тегов binary
		/// </summary>
		/// <returns>Откорректированная строка типа string</returns>
		public string correct() {
			/* ********************************
			 *    Предварительная обработка   *
			 * ********************************/
			// Поиск одноименных обложек и их переименовывание
			BinaryCopiesWorker();
			
			/* *************************
			 *    Основная обработка   *
			 * *************************/
			// обработка binary, в которых отсутствует аттрибут content-type
			try {
				Match m = Regex.Match(
					_xmlBinaries, "(?'binary'<binary +?)(?'id'id=\"(?:(?:\\w+?\\W?\\w+?)+)+\\.\\w{3,4}\">)",
					RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				while ( m.Success ) {
					string BinaryText = m.Value;
					Match mExt = Regex.Match(
						_xmlBinaries, "(?<=\\.)(\\w{3,4})",
						RegexOptions.IgnoreCase | RegexOptions.Multiline
					);
					string ContentType = string.Empty;
					if ( mExt.Success ) {
						switch ( mExt.Value ) {
							case "png" :
								ContentType = "content-type=\"image/png\"";
								break;
							default:
								ContentType = "content-type=\"image/jpeg\"";
								break;
						}
					}
					if ( !string.IsNullOrWhiteSpace( ContentType ) ) {
						string resultBinaryTag = string.Format(
							"{0}{1} {2}",
							m.Groups[1].Captures[0].Value, ContentType, m.Groups[2].Captures[0].Value
						);
						if ( resultBinaryTag != BinaryText )
							_xmlBinaries = _xmlBinaries.Replace( BinaryText, resultBinaryTag );
					}
					m = m.NextMatch();
				}
			} catch ( RegexMatchTimeoutException /*exp*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "BinaryCorrector:\r\nОбработка <binary>, в которых отсутствует аттрибут content-type."
				);
			}
			
			// обработка ссылок в данных тега binary
			try {
				Match m = Regex.Match(
					_xmlBinaries, _regStringForBinaryTag,
					RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				while ( m.Success ) {
					string sourceRinaryTag = m.Value;
					// обработка ссылок
					LinksCorrector linksCorrector = new LinksCorrector( _FilePath, sourceRinaryTag );
					string resultBinaryTag = linksCorrector.correct();
					if ( resultBinaryTag != sourceRinaryTag )
						_xmlBinaries = _xmlBinaries.Replace( sourceRinaryTag, resultBinaryTag );
					m = m.NextMatch();
				}
			} catch ( RegexMatchTimeoutException /*exp*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "BinaryCorrector:\r\nОбработка ссылок в данных тега <binary>."
				);
			}
			
			return _xmlBinaries;
		}
		
		// =============================================================================================
		// 								ВСПОМОГАТЕЛЬНЫЕ ПРИВАТНЫЕ МЕТОДЫ
		// =============================================================================================
		#region Вспомогательные методы
		/// <summary>
		/// Поиск одноименных обложек и их переименовывание
		/// </summary>
		private void BinaryCopiesWorker() {
			// Формируем список картинок разделов binary
			List<string> BinaryList = MakeImageList();
			// Есть ли копии картинок?
			List<ImageData> ImageIDCopiesList = getCopiesList( BinaryList );
			if ( ImageIDCopiesList != null ) {
				int counter = 0;
				int CurrentIndex = -1; // Текущая позиция-индекс при поиске картинки
				foreach ( ImageData image in ImageIDCopiesList ) {
					if ( image.Count > 1 ) {
						try {
							Match m = Regex.Match(
								_xmlBinaries, image.ImageName,
								RegexOptions.IgnoreCase | RegexOptions.Multiline
							);
							if ( m.Success ) {
								// Пропускае 1-ю картинку из списка - работаем с ее копией, если она есть
								m = m.NextMatch();
								if ( m.Success ) {
									CurrentIndex = m.Index; // Текущая позиция курсора
									counter = 0; // обнуление счетчика одинаковых картинок
									while ( CurrentIndex > -1 ) {
										CurrentIndex = _xmlBinaries.IndexOf( image.ImageName, CurrentIndex, StringComparison.CurrentCulture );
										if ( CurrentIndex > -1 ) {
											int TempIndex = _xmlBinaries.IndexOf('.', CurrentIndex);
											if ( TempIndex > -1 )
												CurrentIndex = TempIndex;
											if ( CurrentIndex > -1 ) {
												string s = string.Format( "__copy{0}", ++counter );
												_xmlBinaries = _xmlBinaries.Insert( CurrentIndex, s );
												CurrentIndex += s.Length;
											}
										} else // CurrentIndex = -1
											break;
									} // while
								}
							}
						} catch ( RegexMatchTimeoutException /*exp*/ ) {}
						catch ( Exception ex ) {
							Debug.DebugMessage(
								_FilePath, ex, "BinaryCorrector:\r\nПоиск одноименных обложек и их переименовывание."
							);
						}
					}
				} // foreach
			}
		}
		
		/// <summary>
		/// Формирование списка картинок из раздела всех binary
		/// </summary>
		/// <returns>Список имен картинок с расширением типа List&lt;string&gt; или null, если картинки не найдены</returns>
		private List<string> MakeImageList() {
			List<string> BinaryList = null;
			try {
				// Формируем список картинок
				IList<string> BinaryListTemp = new List<string>();
				Match m = Regex.Match(
					_xmlBinaries, _regStringForBinaryTag,
					RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				while ( m.Success ) {
					BinaryListTemp.Add(m.Value);
					m = m.NextMatch();
				}
				if ( BinaryListTemp.Count > 0 ) {
					BinaryList = new List<string>();
					foreach ( string s in BinaryListTemp ) {
						m = Regex.Match(
							s, "id=\"(?:(\\w+?\\W?\\w+?)+)+\\.\\w{3,4}\"",
							RegexOptions.IgnoreCase | RegexOptions.Multiline
						);
						if ( m.Success ) {
							string sID = m.Value;
							int FirstIndex = sID.IndexOf('"');
							int LastIndex = sID.LastIndexOf('"');
							string ImageName = sID.Substring( FirstIndex+1, LastIndex-1 - FirstIndex );
							BinaryList.Add( ImageName );
							ImageData img = new ImageData( ImageName, FirstIndex+1 );
						}
					}
					BinaryListTemp.Clear();
				}
			} catch ( RegexMatchTimeoutException /*exp*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "BinaryCorrector:\r\nФормирование списка картинок из раздела всех <binary>."
				);
			}
			return BinaryList;
		}
		
		/// <summary>
		/// Формирование списка копий строк в List&lt;string&gt;
		/// </summary>
		/// <param name="BinaryList">Исследуемый список строк List&lt;string&gt;</param>
		/// <returns>Список копий элементов типа List&lt;ImageData&gt; или null, если исследуемый список имеет уникальные элементы</returns>
		private List<ImageData> getCopiesList( List<string> BinaryList ) {
			if ( BinaryList == null )
				return null;
			else if ( BinaryList.Count == 0 )
				return null;
			
			List<string> ImageNameList =
				BinaryList.GroupBy(x => x).Where(g => g.Count() > 1).Select(g => g.Key).ToList();
			List<int> CountList =
				BinaryList.GroupBy(x => x).Where(g => g.Count() > 1).Select(g => g.Count()).ToList();
			List<ImageData> CopiesList = new List<ImageData>();
			for( int i = 0; i != ImageNameList.Count; ++i ) {
				ImageData id = new ImageData( ImageNameList[i], CountList[i] );
				CopiesList.Add( id );
			}
			return CopiesList.Count > 0 ? CopiesList : null;
		}
		
		#endregion
	}
}
