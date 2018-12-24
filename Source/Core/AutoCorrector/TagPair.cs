/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 28.10.2015
 * Время: 6:51
 * 
 * License: GPL 2.1
 */
using System;

using System.Windows.Forms;

using Core.Common;

namespace Core.AutoCorrector
{
	/// <summary>
	/// Поиск парных тегов с вложенными тегами любой сложности вложения
	/// </summary>
	public class TagPair {
		private string _FilePath = string.Empty; // Путь к обрабатываемому файлу
		
		private int _startTagCount = 0;
		private int _endTagCount = 0;
		private int _startTagPosition = 0;
		private int _endTagPosition = 0;
		private string _previousTag = string.Empty;
		private string _nextTag = string.Empty;
		
		private string _tagPair = string.Empty;
		
		private string _startTag = string.Empty;
		private string _endTag = string.Empty;
		
		/// <summary>
		/// Конструктор класса TagPair
		/// </summary>
		/// <param name="FilePath">Путь к обрабатываемому файлу</param>
		public TagPair ( string FilePath )
		{
			_FilePath = FilePath;
		}
		
		/// <summary>
		/// Путь к обрабатываемому файлу
		/// </summary>
		public virtual string FilePath {
			get { return _FilePath; }
		}
		
		/// <summary>
		/// число открывающих тегов
		/// </summary>
		public virtual int StartTagCount {
			get { return _startTagCount; }
			set { _startTagCount = value; }
		}
		/// <summary>
		/// число закрывающих тегов
		/// </summary>
		public virtual int EndTagCount {
			get { return _endTagCount; }
			set { _endTagCount = value; }
		}
		
		/// <summary>
		/// позиция перед открывающим тегом
		/// </summary>
		public virtual int StartTagPosition {
			get { return _startTagPosition; }
			set { _startTagPosition = value; }
		}
		/// <summary>
		/// позиция за последним символом закрывающего тега
		/// </summary>
		public virtual int EndTagPosition {
			get { return _endTagPosition; }
			set { _endTagPosition = value; }
		}
		
		/// <summary>
		///тег ДО искомого парного тега (до первого открывающего тега)
		/// </summary>
		public virtual string PreviousTag {
			get { return _previousTag; }
			set { _previousTag = value; }
		}
		/// <summary>
		///тег ПОСЛЕ искомого парного тега (после последнего закрывающего тега)
		/// </summary>
		public virtual string NextTag {
			get { return _nextTag; }
			set { _nextTag = value; }
		}
		
		/// <summary>
		///Текст найденного парного тега со всеми вложениями
		/// </summary>
		public virtual string PairTag {
			get { return _tagPair; }
			set { _tagPair = value; }
		}
		
		/// <summary>
		/// Сбор всех полей - очистка
		/// </summary>
		public void clear() {
			_FilePath = string.Empty;
			_startTagCount = 0;
			_endTagCount = 0;
			_startTagPosition = 0;
			_endTagPosition = 0;
			_previousTag = string.Empty;
			_nextTag = string.Empty;
			_tagPair = string.Empty;
			_startTag = string.Empty;
			_endTag = string.Empty;
		}
		
		/// <summary>
		/// Поиск текста для заданного парного тега, а также
		/// Поиск следующего тэга, идущего за закрывающим искомым тэгом и
		/// Поиск предыдущего тэга, идущего перед открывающим искомым тэгом
		/// </summary>
		/// <param name="xmlText">Строка для корректировки</param>
		/// <param name="startTag">Открывающий тэг</param>
		/// <param name="endTag">Закрывающий тэг</param>
		public void getPairTextAndTags( string xmlText, string startTag, string endTag ) {
			_startTag = startTag;
			_endTag = endTag;
			findPreviousTag( xmlText, _startTagPosition );
			getPairTagText( xmlText, startTag, endTag );
			findNextTag( xmlText, _endTagPosition );
		}
		
		/// <summary>
		/// Поиск текста для заданного парного тега
		/// </summary>
		/// <param name="xmlText">Строка для корректировки</param>
		/// <param name="startTag">Открывающий тэг</param>
		/// <param name="endTag">Закрывающий тэг</param>
		/// <returns>true - найдено; false - не найдено </returns>
		private bool getPairTagText( string xmlText, string startTag, string endTag ) {
			// АЛГОРИТМ
//				2. Ищем, например, <epigraph> или </epigraph> - что будет найдено вперед {
//					Если найдено </epigraph> то {
//						++endEpigraph.
//							Если startEpigraph == endEpigraph то {
//							нашли парные теги - Выход из метода.
//						} Иначе {
//							Переход на п. 2
//						}
//					} Иначе, если найдено <epigraph> то (
//						++startEpigraph
//						Если startEpigraph == endEpigraph то {
//							нашли парные теги - Выход из метода.
//						} Иначе {
//							Переход на п. 2
//						}
//					} Иначе {
//					Переход на п. 2
//				}
//			}
			int cursor = _startTagPosition;
			try {
				start: int indexOf_Tag = xmlText.IndexOf( endTag, cursor + startTag.Length, StringComparison.CurrentCulture );
				if ( indexOf_Tag == -1 )
					return false; // нет зарывающего тега для найденного открывающего
				else {
					// нашли закрывающий тег </tag>
					int indexOfTag = xmlText.IndexOf( startTag, cursor + startTag.Length, StringComparison.CurrentCulture );
					if ( indexOfTag != -1 ) {
						// нашли открывающий тег <tag>
						if ( indexOf_Tag < indexOfTag ) {
							// закрывающий тег </tag> находится ДО нового открывающего тега <tag>
							++_endTagCount;
							if ( _endTagCount == _startTagCount ) {
								_endTagPosition = indexOf_Tag + endTag.Length;
								_tagPair = xmlText.Substring( _startTagPosition, _endTagPosition - _startTagPosition );
								return true;
							} else {
								// закрывающий тег </tag> находится ПОСЛЕ нового открывающего тега <tag>
								cursor = indexOf_Tag;
								goto start;
							}
						} else {
							// новый открывающий тег <tag> находится ДО закрывающего тега </tag>
							++_startTagCount;
							cursor = indexOfTag;
							goto start;
						}
					} else {
						// нет открывающего тега <tag>
						++_endTagCount;
						if ( _endTagCount == _startTagCount ) {
							_endTagPosition = indexOf_Tag + endTag.Length;
							_tagPair = xmlText.Substring( _startTagPosition, _endTagPosition - _startTagPosition );
							return true;
						} else {
							// закрывающий тег </tag> находится ПОСЛЕ нового открывающего тега <tag>
							cursor = indexOf_Tag;
							goto start;
						}
					}
				}
			} catch (Exception ex) {
				Debug.DebugMessage(
					_FilePath, ex,
					null, string.Format("Искомый тэг: Открывающий тэг: {0}, Закрывающий тэг: {1}\r\n", startTag, endTag )
				);
			}
			
			return false;
		}
		
		/// <summary>
		/// Поиск следующего тэга, идущего за закрывающим искомым тэгом
		/// </summary>
		/// <param name="xmlText">Строка для корректировки</param>
		/// <param name="StartPosition">Позиция начала поиска</param>
		/// <returns>true - найдено; false - не найдено </returns>
		private bool findNextTag( string xmlText, int StartPosition ) {
			int start = StartPosition;
			int end = -1;
			try {
				for ( int i = StartPosition; i != xmlText.Length; ++i ) {
					if (xmlText[i] == '>' ) {
						end = i;
						break;
					}
					if ( xmlText[i] == '<' ) {
						start = i;
						continue;
					}
				}
				_nextTag = xmlText.Substring( start, end-start+1 );
			} catch (Exception ex) {
				Debug.DebugMessage(
					_FilePath, ex,
					null, string.Format("Открывающий тэг: {0}, Закрывающий тэг: {1}\r\n", _startTag, _endTag )
				);
			}
			
			return ! string.IsNullOrEmpty( _nextTag );
		}
		
		/// <summary>
		/// Поиск предыдущего тэга, идущего перед открывающим искомым тэгом
		/// </summary>
		/// <param name="xmlText">Строка для корректировки</param>
		/// <param name="StartPosition">Позиция начала поиска</param>
		/// <returns>true - найдено; false - не найдено </returns>
		private bool findPreviousTag( string xmlText, int StartPosition ) {
			if ( _findPreviousTag(  xmlText, StartPosition ) ) {
				string prevTag = _previousTag;
				if ( _previousTag.Equals( "<empty-line/>" ) || _previousTag.Equals( "<empty-line />" )/* || _previousTag.Equals( "<section>" )*/ ) {
					if ( _findPreviousTag( xmlText, StartPosition - prevTag.Length - 1 ) )
						_previousTag += prevTag;
				}
				return false;
			}
			return false;
		}
		
		/// <summary>
		/// Поиск предыдущего тэга, идущего перед открывающим искомым тэгом
		/// </summary>
		/// <param name="xmlText">Строка для корректировки</param>
		/// <param name="StartPosition">Позиция начала поиска</param>
		/// <returns>true - найдено; false - не найдено </returns>
		private bool _findPreviousTag( string xmlText, int StartPosition ) {
			if ( StartPosition < 0 )
				return false;
			
			int start = 0;
			int end = StartPosition;
			try {
				for ( int i = StartPosition-1; i != 0; --i ) {
					if (xmlText[i] == '<' ) {
						start = i;
						break;
					}
					if ( xmlText[i] == '>' ) {
						end = i;
						continue;
					}
				}
				_previousTag = xmlText.Substring( start, end-start+1 );
			} catch (Exception ex) {
				Debug.DebugMessage(
					_FilePath, ex,
					null, string.Format("Открывающий тэг: {0}, Закрывающий тэг: {1}\r\n", _startTag, _endTag )
				);
			}
			
			return ! string.IsNullOrEmpty( _previousTag );
		}
		
	}
}
