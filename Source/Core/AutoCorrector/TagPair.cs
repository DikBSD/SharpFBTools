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

namespace Core.AutoCorrector
{
	/// <summary>
	/// Поиск парных тегов с вложенными тегами любой сложности вложения
	/// </summary>
	public class TagPair {
		private int _startTagCount = 0;
		private int _endTagCount = 0;
		private int _startTagPosition = 0;
		private int _endTagPosition = 0;
		private string _previousTag = string.Empty;
		private string _nextTag = string.Empty;
		
		private string _tagPair = string.Empty;
		
		public TagPair ()
		{
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
		///текст найденного парного тега со всеми вложениями
		/// </summary>
		public virtual string PairTag {
			get { return _tagPair; }
			set { _tagPair = value; }
		}
		
		/// <summary>
		/// Сбор всех полей - очистка
		/// </summary>
		public void clear() {
			_startTagCount = 0;
			_endTagCount = 0;
			_startTagPosition = 0;
			_endTagPosition = 0;
			_previousTag = string.Empty;
			_nextTag = string.Empty;
			_tagPair = string.Empty;
		}
		
		/// <summary>
		/// Поиск текста для заданного парного тега, а также
		/// Поиск следующего тэга, идущего за закрывающим искомым тэгом и
		/// Поиск предыдущего тэга, идущего перед открывающим искомым тэгом
		/// </summary>
		/// <param name="xmlText">Строка для корректировки</param>
		/// <param name="startTag">Открывающий тэг</param>
		/// <param name="endTag">Закрывающий тэг</param>
		/// <returns>true - найдено; false - не найдено </returns>
		public void getPairTextAndTags( string xmlText, string startTag, string endTag ) {
			findPreviousTag( xmlText, _startTagPosition );
			getPairTagText( xmlText, startTag, endTag );
			findNextTag( xmlText, _endTagPosition );
		}
		
		/// <summary>
		/// Поиск текст для заданного парного тега 
		/// </summary>
		/// <param name="xmlText">Строка для корректировки</param>
		/// <param name="startTag">Открывающий тэг</param>
		/// <param name="endTag">Закрывающий тэг</param>
		/// <returns>true - найдено; false - не найдено </returns>
		public bool getPairTagText( string xmlText, string startTag, string endTag ) {
			// АЛГОРИТМ
//				2. Ищем <epigraph> или </epigraph> - что будет найдено вперед {
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
				start: int indexOf_Epigraph = xmlText.IndexOf( endTag, cursor + startTag.Length );
				if ( indexOf_Epigraph == -1 )
					return false; // нет зарывающего тега для найденного открывающего
				else {
					// нашли закрывающий тег </tag>
					int indexOfEpigraph = xmlText.IndexOf( startTag, cursor + startTag.Length );
					if ( indexOfEpigraph != -1 ) {
						// нашли открывающий тег <tag>
						if ( indexOf_Epigraph < indexOfEpigraph ) {
							// закрывающий тег </tag> находится ДО нового открывающего тега <tag>
							++_endTagCount;
							if ( _endTagCount == _startTagCount ) {
								_endTagPosition = indexOf_Epigraph + endTag.Length;
								_tagPair = xmlText.Substring( _startTagPosition, _endTagPosition - _startTagPosition );
								return true;
							} else {
								// закрывающий тег </tag> находится ПОСЛЕ нового открывающего тега <tag>
								cursor = indexOf_Epigraph;
								goto start;
							}
						} else {
							// новый открывающий тег <tag> находится ДО закрывающего тега </tag>
							++_startTagCount;
							cursor = indexOfEpigraph;
							goto start;
						}
					} else {
						// нет открывающего тега <tag>
						++_endTagCount;
						if ( _endTagCount == _startTagCount ) {
							_endTagPosition = indexOf_Epigraph + endTag.Length;
							_tagPair = xmlText.Substring( _startTagPosition, _endTagPosition - _startTagPosition );
							return true;
						} else {
							// закрывающий тег </tag> находится ПОСЛЕ нового открывающего тега <tag>
							cursor = indexOf_Epigraph;
							goto start;
						}
					}
				}
			} catch (Exception m) {
				MessageBox.Show("getPairTagText:\n" + m);
			}
			
			return false;
		}
		
		/// <summary>
		/// Поиск следующего тэга, идущего за закрывающим искомым тэгом
		/// </summary>
		/// <param name="xmlText">Строка для корректировки</param>
		/// <param name="StartPosition">Позиция начала поиска</param>
		/// <returns>true - найдено; false - не найдено </returns>
		public bool findNextTag( string xmlText, int StartPosition ) {
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
			} catch (Exception m) {
				MessageBox.Show("findNextTag:\n" + m);
			}
			
			return !string.IsNullOrEmpty( _nextTag );
		}
		
		/// <summary>
		/// Поиск предыдущего тэга, идущего перед открывающим искомым тэгом
		/// </summary>
		/// <param name="xmlText">Строка для корректировки</param>
		/// <param name="StartPosition">Позиция начала поиска</param>
		/// <returns>true - найдено; false - не найдено </returns>
		public bool findPreviousTag( string xmlText, int StartPosition ) {
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
			} catch (Exception m) {
				MessageBox.Show("_findPreviousTag:\n" + m);
			}
			
			return !string.IsNullOrEmpty( _previousTag );
		}
		
	}
}
