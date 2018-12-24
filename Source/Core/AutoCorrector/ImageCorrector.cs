/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 18.11.2015
 * Время: 12:33
 * 
 * License: GPL 2.1
 */
using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

using Core.Common;

namespace Core.AutoCorrector
{
	/// <summary>
	/// Обработка Image
	/// </summary>
	public class ImageCorrector
	{
		private const string _MessageTitle = "Автокорректор";
		
		private readonly string _FilePath = string.Empty; // Путь к обрабатываемому файлу
		
		private string _xmlText = string.Empty;
		
		private bool _preProcess = false;
		private bool _postProcess = false;
		
		/// <summary>
		/// Конструктор класса StanzaCorrector
		/// </summary>
		/// <param name="FilePath">Путь к обрабатываемому файлу</param>
		/// <param name="xmlText">Строка для корректировки</param>
		/// <param name="preProcess">Удаление стартовых пробелов и перевода строки => всю книгу - в одну строку</param>
		/// <param name="postProcess">Вставка разрыва абзаца между смежными тегами</param>
		public ImageCorrector( string FilePath, ref string xmlText, bool preProcess, bool postProcess )
		{
			_FilePath = FilePath;
			_xmlText = xmlText;
			_preProcess = preProcess;
			_postProcess = postProcess;
		}
		
		/// <summary>
		/// Корректировка тегов Картинок
		/// </summary>
		/// <returns>Откорректированная строка типа string</returns>
		public string correct() {
			if ( _xmlText.IndexOf( "<image", StringComparison.CurrentCulture ) == -1 )
				return _xmlText;
			
			// преобработка (удаление стартовых пробелов ДО тегов и удаление завершающих пробелов ПОСЛЕ тегов и символов переноса строк)
			if ( _preProcess )
				_xmlText = FB2CleanCode.preProcessing( _xmlText );
			
			/****************************
			 * Основная обработка image *
			 ****************************/
			// обработка картинок: <image l:href="#img_0.png"> </image> или <image l:href="#img_0.png">\n</image>
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'img'<image [^<]+?(?:\"[^\"]*\"|'[^']*')?)(?'more'>)(?:\\s*?</image>)",
					"${img} /${more}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "ImageCorrector:\r\nОбработка картинок: <image l:href=\"#img_0.png\"> </image> или <image l:href=\"#img_0.png\">\n</image>."
				);
			}
			
			// обработка картинки между <section> и <title>
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'sect'<section>\\s*)(?'img'<image[^/]+?(?:\"[^\"]*\"|'[^']*')?/>\\s*)(?'title'<title>\\s*)(?'p'(?:(?:<p[^>]*?(?:\"[^\"]*\"|'[^']*')?>\\s*)(?:.*?)\\s*(?:</p>\\s*)){1,})(?'_title'\\s*</title>)",
					"${sect}${title}${p}${_title}${img}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "ImageCorrector:\r\nОбработка картинки между <section> и <title>."
				);
			}
			
			// вставка <empty-line /> до картинки, идущей после тега <section>: <section><image l:href="#index.jpg" /> => <section><empty-line /><image l:href="#index.jpg" />
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?<=<section>)\\s*(?'img'<image[^/]+?(?:\"[^\"]*\"|'[^']*')?/>)",
					"<empty-line />${img}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "ImageCorrector:\r\nВставка <empty-line /> до картинки, идущей после тега <section>: <section><image l:href=\"#index.jpg\" /> => <section><empty-line /><image l:href=\"#index.jpg\"."
				);
			}
			
			// вставка <empty-line /> после картинки, заключенной в тегах <section> ... </section>: <section><image l:href="#index.jpg" /></section> => <section><image l:href="#index.jpg" /><empty-line /></section>
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'sect'<(?'section'section)>\\s*)(?'img'<image[^/]+?(?:\"[^\"]*\"|'[^']*')?/>\\s*)\\s*?(?'_sect'</\\k'section'>)",
					"${sect}${img}<empty-line />${_sect}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "ImageCorrector:\r\nВставка <empty-line /> после картинки, заключенной в тегах <section> ... </section>: <section><image l:href=\"#index.jpg\" /></section> => <section><image l:href=\"#index.jpg\" /><empty-line /></section>."
				);
			}
			
			// Обработка картинки между тегами </title> и <section> : </title><image l:href="#index.jpg" /><section> => </title><section><image l:href="#index.jpg" /><empty-line /></section><section>
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?<=</title>)\\s*(?'img'(?:<image[^/]+?(?:\"[^\"]*\"|'[^']*')?/>\\s*){1,}\\s*?)(?=<section>)",
					"<section>${img}<empty-line /></section>", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "ImageCorrector:\r\nОбработка картинки между тегами </title> и <section> : </title><image l:href=\"#index.jpg\" /><section> => </title><section><image l:href=\"#index.jpg\" /><empty-line /></section><section>."
				);
			}
			
			// Обработка картинки между тегами </title> и <section> : </title><empty-line /><image l:href="#index.jpg" /><empty-line /><section> => </title><section><empty-line /><image l:href="#index.jpg" /><empty-line /></section><section>
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?<=</title>)\\s*?(?'img'(?:(?:<empty-line ?/>)?\\s*?(?:<image[^/]+?(?:\"[^\"]*\"|'[^']*')?/>\\s*){1,}\\s*?(?:<empty-line ?/>)?\\s*?){1,})(?=<section>)",
					"<section>${img}</section>", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "ImageCorrector:\r\nОбработка картинки между тегами </title> и <section> : </title><empty-line /><image l:href=\"#index.jpg\" /><empty-line /><section> => </title><section><empty-line /><image l:href=\"#index.jpg\" /><empty-line /></section><section>."
				);
			}
			
			// Обработка картинки между тегами </title> и <epigraph> : </title><image l:href="#index.jpg" /><epigraph> => <p><image l:href="#index.jpg" /></p></title><epigraph>
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'_title'</title>)\\s*(?'img'<image[^/]+?(?:\"[^\"]*\"|'[^']*')?/>\\s*)\\s*?(?=<epigraph>)",
					"<p>${img}</p>${_title}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "ImageCorrector:\r\nОбработка картинки между тегами </title> и <epigraph> : </title><image l:href=\"#index.jpg\" /><epigraph> => <p><image l:href=\"#index.jpg\" /></p></title><epigraph>."
				);
			}
			
			// Вставка между <image ... /> (<image ... ></image>) и </section> недостающего тега <empty-line/>
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'tag'(?:<image\\s+\\w+:href=\"#[^\"]*\"\\s*?/>)|(?:<image\\s+\\w+:href=\"#[^\"]*\">\\s*?</image>))(?:\\s*?)(?'_sect'</section>)",
					"${tag}<empty-line/>${_sect}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "ImageCorrector:\r\nВставка между <image ... /> (<image ... ></image>) и </section> недостающего тега <empty-line/>."
				);
			}
			
			// Обработка <subtitle><image l:href="#_3.jpg" /></subtitle> => <p><image l:href="#_3.jpg" /></p>
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?:<subtitle>)\\s*?(?'img'(?:<image[^/]+?(?:\"[^\"]*\"|'[^']*')?/>)\\s*?)\\s*?(?:</subtitle>)",
					"<p>${img}</p>", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "ImageCorrector:\r\nОбработка <subtitle><image l:href=\"#_3.jpg\" /></subtitle> => <p><image l:href=\"#_3.jpg\" /></p>."
				);
			}
			
			// замена пробелов и тильды в ссылках на _
			try {
				Match m = Regex.Match(
					_xmlText, "(?:=\"#[^\"]*\\.\\w\\w\\w\")",
					RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				while (m.Success) {
					string sSource = m.Value;
					string sResult = m.Value;
					sResult = sResult.Replace(' ', '_').Replace('~', '_');
					if ( sResult != sSource )
						_xmlText = _xmlText.Replace( sSource, sResult );
					m = m.NextMatch();
				}
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "ImageCorrector:\r\nЗамена пробелов и тильды в ссылках на _."
				);
			}
			
			// обработка картинок: <text-author><p><image l:href="#i_008.png" /></p></text-author> => <text-author><image l:href="#i_008.png" /></text-author>
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?:<text-author>\\s*?<p>\\s*?)(?'img'(?:<image[^/]+?(?:\"[^\"]*\"|'[^']*')?/>))(?:\\s*?</p>\\s*?</text-author>)",
					"<text-author>${img}</text-author>", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "ImageCorrector:\r\nОбработка картинок: <text-author><p><image l:href=\"#i_008.png\" /></p></text-author> => <text-author><image l:href=\"#i_008.png\" /></text-author>."
				);
			}
			
			// обработка картинок: </section><empty-line /><image l:href="#freud.jpg" /><section> => </section><section><empty-line /><image l:href="#freud.jpg" /><empty-line /></section><section>
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'_sect'</section>)\\s*?(?:<empty-line ?/>\\s*?){1,}\\s*?(?'img'(?:<image [^<]+?(?:\"[^\"]*\"|'[^']*')?>\\s*?){1,})\\s*?(?'sect'<section>)",
					"${_sect}<section><empty-line />${img}<empty-line /></section>${sect}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			catch ( Exception ex ) {
				Debug.DebugMessage(
					_FilePath, ex, "ImageCorrector:\r\nОбработка картинок: </section><empty-line /><image l:href=\"#freud.jpg\" /><section> => </section><section><empty-line /><image l:href=\"#freud.jpg\" /><empty-line /></section><section>."
				);
			}
			
			// постобработка (разбиение на теги (смежные теги) )
			if ( _postProcess )
				_xmlText = FB2CleanCode.postProcessing( _xmlText );
			
			return _xmlText;
		}
	}
}
