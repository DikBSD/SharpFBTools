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

namespace Core.AutoCorrector
{
	/// <summary>
	/// Обработка Image
	/// </summary>
	public class ImageCorrector
	{
		private string _xmlText = string.Empty;
		
		private bool _preProcess = false;
		private bool _postProcess = false;
		
		/// <summary>
		/// Конструктор класса StanzaCorrector
		/// </summary>
		/// <param name="xmlText">Строка для корректировки</param>
		/// <param name="preProcess">Удаление стартовых пробелов и перевода строки => всю книгу - в одну строку</param>
		/// <param name="postProcess">Вставка разрыва абзаца между смежными тегами</param>
		public ImageCorrector( ref string xmlText, bool preProcess, bool postProcess )
		{
			_xmlText = xmlText;
			_preProcess = preProcess;
			_postProcess = postProcess;
		}
		
		/// <summary>
		/// Корректировка тегов Картинок
		/// </summary>
		/// <returns>Откорректированную строку типа string </returns>
		public string correct() {
			if ( _xmlText.IndexOf( "<image" ) == -1 )
				return _xmlText;
			
			// преобработка (удаление стартовых пробелов ДО тегов и удаление завершающих пробелов ПОСЛЕ тегов и символов переноса строк)
			if ( _preProcess )
				_xmlText = FB2CleanCode.preProcessing( _xmlText );
			
			/*******************
			 * Обработка image *
			 *******************/
			// обработка картинок: <image l:href="#img_0.png"> </image> или <image l:href="#img_0.png">\n</image>
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'img'<image [^<]+?(?:\"[^\"]*\"|'[^']*')?)(?'more'>)(?:\\s*?</image>)",
					"${img} /${more}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			// обработка картинки между <section> и <title>
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'sect'<section>\\s*)(?'img'<image[^/]+?(?:\"[^\"]*\"|'[^']*')?/>\\s*)(?'title'<title>\\s*)(?'p'(?:(?:<p[^>]*?(?:\"[^\"]*\"|'[^']*')?>\\s*)(?:.*?)\\s*(?:</p>\\s*)){1,})(?'_title'\\s*</title>)",
					"${sect}${title}${p}${_title}${img}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			// вставка <empty-line /> после картинки, заключенной в тегах <section> ... </section>: <section><image l:href="#index.jpg" /></section> => <section><image l:href="#index.jpg" /><empty-line /></section>
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'sect'<(?'section'section)>\\s*)(?'img'<image[^/]+?(?:\"[^\"]*\"|'[^']*')?/>\\s*)\\s*?(?'_sect'</\\k'section'>)",
					"${sect}${img}<empty-line />${_sect}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			// Обработка картинки между тегами </title> и <section> : </title><image l:href="#index.jpg" /><section> => </title><section><image l:href="#index.jpg" /><empty-line /></section><section>
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?<=</title>)\\s*(?'img'(?:<image[^/]+?(?:\"[^\"]*\"|'[^']*')?/>\\s*){1,}\\s*?)(?=<section>)",
					"<section>${img}<empty-line /></section>", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			// Обработка картинки между тегами </title> и <section> : </title><empty-line /><image l:href="#index.jpg" /><empty-line /><section> => </title><section><empty-line /><image l:href="#index.jpg" /><empty-line /></section><section>
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?<=</title>)\\s*?(?'img'(?:(?:<empty-line ?/>)?\\s*?(?:<image[^/]+?(?:\"[^\"]*\"|'[^']*')?/>\\s*){1,}\\s*?(?:<empty-line ?/>)?\\s*?){1,})(?=<section>)",
					"<section>${img}</section>", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			
			// Обработка картинки между тегами </title> и <epigraph> : </title><image l:href="#index.jpg" /><epigraph> => <p><image l:href="#index.jpg" /></p></title><epigraph>
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'_title'</title>)\\s*(?'img'<image[^/]+?(?:\"[^\"]*\"|'[^']*')?/>\\s*)\\s*?(?=<epigraph>)",
					"<p>${img}</p>${_title}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			
			// Вставка между <image ... /> (<image ... ></image>) и </section> недостающего тега <empty-line/>
			try {
				_xmlText = Regex.Replace(
					_xmlText, "(?'tag'(?:<image\\s+\\w+:href=\"#[^\"]*\"\\s*?/>)|(?:<image\\s+\\w+:href=\"#[^\"]*\">\\s*?</image>))(?:\\s*?)(?'_sect'</section>)",
					"${tag}<empty-line/>${_sect}", RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			// замена пробелов в ссылках на _
			try {
				Match m = Regex.Match(
					_xmlText, "(?:=\"#[^\"]*\\.\\w\\w\\w\")",
					RegexOptions.IgnoreCase | RegexOptions.Multiline
				);
				if ( m.Success ) {
					string s = m.Value;
					s = s.Replace(" ", "_");
					_xmlText = _xmlText.Substring( 0, m.Index ) /* ДО обрабатываемого текста */
						+ s
						+ _xmlText.Substring( m.Index + m.Length ); /* ПОСЛЕ обрабатываемого текста */
				}
			} catch ( RegexMatchTimeoutException /*ex*/ ) {}
			
			// постобработка (разбиение на теги (смежные теги) )
			if ( _postProcess )
				_xmlText = FB2CleanCode.postProcessing( _xmlText );
			
			return _xmlText;
		}
	}
}
