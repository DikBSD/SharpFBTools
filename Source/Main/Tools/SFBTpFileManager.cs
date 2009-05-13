/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 16.03.2009
 * Time: 9:03
 * 
 * License: GPL 2.1
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

using FB2.FB2Parsers;
using FB2.Common;
using FB2.Description;
using FB2.Description.TitleInfo;
using FB2.Description.DocumentInfo;
using FB2.Description.PublishInfo;
using FB2.Description.CustomInfo;
using FB2.Description.Common;
using Templates.Lexems;
using Templates;
using StringProcessing;

using fB2Parser = FB2.FB2Parsers.FB2Parser;

namespace SharpFBTools.Controls.Panels
{
	/// <summary>
	/// Description of SFBTpFileManager.
	/// </summary>
	public partial class SFBTpFileManager : UserControl
	{
		public ListView GetSettingsInfoListView()
		{
			return lvSettings;
		}
		public SFBTpFileManager()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			cboxTemplatesPrepared.SelectedIndex = 3;
			Init();
			
			string sTDPath = Settings.Settings.GetDefFMDescTemplatePath();
			if( File.Exists( sTDPath ) ) {
				richTxtBoxDescTemplates.LoadFile( sTDPath );
			} else {
				richTxtBoxDescTemplates.Text = "Не найден файл описания Шаблонов подстановки: \""+sTDPath+"\"";
			}

			// загружаем в ListView-индикатор настроек данные 
			Settings.Settings.SetInfoSettings( lvSettings );
		}
		
		#region Закрытые вспомогательные методы класса
		private void Init() {
			// инициализация контролов и переменных
			for( int i=0; i!=lvFilesCount.Items.Count; ++i ) {
				lvFilesCount.Items[i].SubItems[1].Text	= "0";
			}
			tsProgressBar.Value	= 1;
			tsslblProgress.Text		= Settings.Settings.GetReady();
			tsProgressBar.Visible	= false;
			// очистка временной папки
			FilesWorker.FilesWorker.RemoveDir( Settings.Settings.GetTempDir() );
		}
			
		private bool IsArchive( string sExt )
		{
			return ( sExt==".zip" || sExt==".rar" || sExt==".7z" || sExt==".bz2" || sExt==".gz" || sExt==".tar" );
		}
		
		private List<string> GetFileListFromArchive( string sFromFile ) {
			// Распаковать архив во временную папку
			List<string> lFilesList = new List<string>(); // список fb2-файлов из файла-архива sFromFile
			string sTempDir 	= Settings.Settings.GetTempDir();
			string sExt			= Path.GetExtension( sFromFile ).ToLower();
			string s7zaPath		= Settings.Settings.Read7zaPath();
			string sUnRarPath	= Settings.Settings.ReadUnRarPath();
			if( sExt != "" ) {
				FilesWorker.FilesWorker.RemoveDir( sTempDir );
				switch( sExt ) {
					case ".rar":
						if( !Directory.Exists( sTempDir ) ) {
							Directory.CreateDirectory( sTempDir );
						}
						Archiver.Archiver.unrar( sUnRarPath, sFromFile, sTempDir );
						IncStatus( 4 );
						break;
					case ".zip":
						Archiver.Archiver.unzip( s7zaPath, sFromFile, sTempDir );
						IncStatus( 3 );
						break;
					case ".7z":
						Archiver.Archiver.unzip( s7zaPath, sFromFile, sTempDir );
						IncStatus( 5 );
						break;
					case ".bz2":
						Archiver.Archiver.unzip( s7zaPath, sFromFile, sTempDir );
						IncStatus( 6 );
						break;
					case ".gz":
						Archiver.Archiver.unzip( s7zaPath, sFromFile, sTempDir );
						IncStatus( 7 );
						break;
					case ".tar":
						Archiver.Archiver.unzip( s7zaPath, sFromFile, sTempDir );
						IncStatus( 8 );
						break;
				}
				// составляем список файлов (или одного) из архива
				if( Directory.Exists( sTempDir ) ) {
					string [] files = Directory.GetFiles( sTempDir );
					foreach( string sFile in files ) {
						if( Path.GetExtension( Path.GetFileName( sFile ) )==".fb2" ) {
							lFilesList.Add( sFile );
						}
					}
				}
			}
			return lFilesList;
		}
		
		private void CreateFileTo( string sFromFilePath, string sToFilePath, int nFileExistMode, bool bAddToFileNameBookIDMode ) {
			// создание нового файла или архива
			try {
				if( !Settings.Settings.ReadToArchiveMode() ) {
					CopyFileToTargetDir( sFromFilePath, sToFilePath, nFileExistMode, bAddToFileNameBookIDMode );
				} else {
					// упаковка в архив
					string sArchType = StringProcessing.StringProcessing.GetArchiveExt( Settings.Settings.ReadArchiveTypeText() );
					CopyFileToArchive( sArchType, sFromFilePath, sToFilePath+"."+sArchType, nFileExistMode, bAddToFileNameBookIDMode );
				}
			} catch ( System.IO.PathTooLongException ) {
				string sFileLongPathDir = Settings.Settings.ReadFMFB2LongPathDir();
				Directory.CreateDirectory( sFileLongPathDir );
				sToFilePath = sFileLongPathDir+"\\"+Path.GetFileName( sFromFilePath );
				CopyFileToTargetDir( sFromFilePath, sToFilePath, nFileExistMode, false );	
			}
		}
		
		private void CopyFileToArchive( string sArchType, string sFromFilePath, string sToFilePath, int nFileExistMode, bool bAddToFileNameBookIDMode )
		{
			// архивирование файла с сформированным именем (путь)
			string s7zPath = Settings.Settings.Read7zaPath();
			string sRarPath = Settings.Settings.ReadRarPath();
			// обработка уже существующих файлов в папке
			Regex rx = new Regex( @"\\+" );
			sFromFilePath = rx.Replace( sFromFilePath, "\\" );
			sToFilePath = rx.Replace( sToFilePath, "\\" );
			
			sToFilePath = FileExsistWorker( sFromFilePath, sToFilePath, nFileExistMode, bAddToFileNameBookIDMode, sArchType );
			if( sArchType == "rar" ) {
				Archiver.Archiver.rar( sRarPath, sFromFilePath, sToFilePath, true );
			} else {
				Archiver.Archiver.zip( s7zPath, sArchType, sFromFilePath, sToFilePath );
			}
			IncStatus( 10 ); // всего создано
		}
		
		private void CopyFileToTargetDir( string sFromFilePath, string sToFilePath, int nFileExistMode, bool bAddToFileNameBookIDMode )
		{
			// копирование файла с сформированным именем (путь)
			Regex rx = new Regex( @"\\+" );
			sFromFilePath = rx.Replace( sFromFilePath, "\\" );
			sToFilePath = rx.Replace( sToFilePath, "\\" );
			// обработка уже существующих файлов в папке
			sToFilePath = FileExsistWorker( sFromFilePath, sToFilePath, nFileExistMode, bAddToFileNameBookIDMode, "" );
			if( File.Exists( sFromFilePath ) ) {
				File.Copy( sFromFilePath, sToFilePath );
			}
			IncStatus( 10 ); // всего создано
		}
		private string FileExsistWorker( string sFromFilePath, string sToFilePath, int nFileExistMode, bool bAddToFileNameBookIDMode,
		                                string sArchType )
		{
			// обработка уже существующих файлов в папке
			string sSufix = "";
			FileInfo fi = new FileInfo( sToFilePath );
			if( !fi.Directory.Exists ) {
				Directory.CreateDirectory( fi.Directory.ToString() );
			}
			if( File.Exists( sToFilePath ) ) {
				if( nFileExistMode == 0 ) {
					File.Delete( sToFilePath );
				} else {
					if( bAddToFileNameBookIDMode ) {
						sSufix = "_" + StringProcessing.StringProcessing.GetFMBookID( sFromFilePath );
					}
					if( nFileExistMode == 1 ) {
						// Добавить к создаваемому файлу очередной номер
						sSufix += "_" + StringProcessing.StringProcessing.GetFileNewNumber( sToFilePath ).ToString();
					} else {
						// Добавить к создаваемому файлу дату и время
						sSufix += "_" + StringProcessing.StringProcessing.GetDateTimeExt();
					}
					if( sArchType.Length==0 ) {
						sToFilePath = sToFilePath.Remove( sToFilePath.Length-4 ) + sSufix + ".fb2";
					} else {
						sToFilePath = sToFilePath.Remove( sToFilePath.Length - (sArchType.Length+5) ) + sSufix + ".fb2." + sArchType;
					}
				}
			}
			return sToFilePath;
		}
		
		private void MakeFileFor1Genre1Author( string sFromFilePath, string sSource, string sTarget,
		                                      List<Templates.Lexems.TPSimple> lSLexems ) {
			// создаем файл по новому пути для первого Жанра и для первого Автора Книги
			MakeFile( sFromFilePath, sSource, sTarget, lSLexems, 0, 0 );
		}
		private void MakeFileForAllGenre1Author( string sFromFilePath, string sSource, string sTarget,
		                                      List<Templates.Lexems.TPSimple> lSLexems ) {
			// создаем файл по новому пути для всех Жанров и для первого Автора Книги
			fB2Parser fb2 = new fB2Parser( sFromFilePath );
			TitleInfo ti = fb2.GetTitleInfo();
			IList<Genre> lGenres = ti.Genres;
			IList<Author> lAuthors = ti.Authors;
			for( int i=0; i!= lGenres.Count; ++i ) {
				MakeFile( sFromFilePath, sSource, sTarget, lSLexems, i, 0 );
			}
			
		}
		private void MakeFileFor1GenreAllAuthor( string sFromFilePath, string sSource, string sTarget,
		                                      	List<Templates.Lexems.TPSimple> lSLexems ) {
			// создаем файл по новому пути для первого Жанра и для всех Авторов Книги
			fB2Parser fb2 = new fB2Parser( sFromFilePath );
			TitleInfo ti = fb2.GetTitleInfo();
			IList<Genre> lGenres = ti.Genres;
			IList<Author> lAuthors = ti.Authors;
			for( int i=0; i!= lAuthors.Count; ++i ) {
				MakeFile( sFromFilePath, sSource, sTarget, lSLexems, 0, i );
			}
			
		}
		private void MakeFileForAllGenreAllAuthor( string sFromFilePath, string sSource, string sTarget,
		                                      		List<Templates.Lexems.TPSimple> lSLexems ) {
			// создаем файл по новому пути для всех Жанров и для всех Авторов Книги
			fB2Parser fb2 = new fB2Parser( sFromFilePath );
			TitleInfo ti = fb2.GetTitleInfo();
			IList<Genre> lGenres = ti.Genres;
			IList<Author> lAuthors = ti.Authors;
			for( int i=0; i!= lGenres.Count; ++i ) {
				for( int j=0; j!= lAuthors.Count; ++j ) {
					MakeFile( sFromFilePath, sSource, sTarget, lSLexems, i, j );
				}
			}
			
		}
		
		private void MakeFile( string sFromFilePath, string sSource, string sTarget,
		                      List<Templates.Lexems.TPSimple> lSLexems, int nGenreIndex, int nAuthorIndex ) {
			// создаем файл по новому пути
			int nFileExistMode = Settings.Settings.ReadFileExistMode();
			bool bAddToFileNameBookIDMode = Settings.Settings.ReadAddToFileNameBookIDMode();
			bool bDelFB2FilesMode = Settings.Settings.ReadDelFB2FilesMode();
			string sTempDir = Settings.Settings.GetTempDir();
			string sNotReadFB2Dir = Settings.Settings.ReadFMFB2NotReadDir();
			// смотрим, что это за файл
			string sExt = Path.GetExtension( sFromFilePath ).ToLower();
			if( sExt == ".fb2" ) {
				// обработка fb2-файла
				try {
					string sToFilePath = sTarget + "\\" +
							Templates.TemplatesParser.Parse( sFromFilePath, lSLexems, nGenreIndex, nAuthorIndex ) + ".fb2";
					CreateFileTo( sFromFilePath, sToFilePath, nFileExistMode, bAddToFileNameBookIDMode );
					IncStatus( 2 ); // исходные fb2-файлы
				} catch ( System.IO.FileLoadException ){
					// нечитаемый fb2-файл - копируем его в папку Bad
					Directory.CreateDirectory( sNotReadFB2Dir );
					string sToFilePath = sNotReadFB2Dir+"\\"+sFromFilePath.Remove( 0, sSource.Length );
					CopyFileToTargetDir( sFromFilePath, sToFilePath, nFileExistMode, false );
					IncStatus( 11 ); // нечитаемые fb2-файлв или архивы
				}
				
				if( bDelFB2FilesMode ) {
					// удаляем исходный fb2-файл
					if( File.Exists( sFromFilePath ) ) {
						File.Delete( sFromFilePath );
					}
				}
			} else {
				// это архив?
				if( IsArchive( sExt ) ) {
					List<string> lFilesListFromArchive = GetFileListFromArchive( sFromFilePath );
					foreach( string sFromFB2Path in lFilesListFromArchive ) {
						try {
							string sToFilePath = sTarget + "\\" + 
									Templates.TemplatesParser.Parse( sFromFB2Path, lSLexems, 0, nAuthorIndex ) + ".fb2";
							CreateFileTo( sFromFB2Path, sToFilePath, nFileExistMode, bAddToFileNameBookIDMode  );
						} catch ( System.IO.FileLoadException ){
							// нечитаемый fb2-архив - копируем его в папку Bad
							Directory.CreateDirectory( sNotReadFB2Dir );
							string sToFilePath = sNotReadFB2Dir+"\\"+sFromFB2Path.Remove( nGenreIndex, sTempDir.Length );
							CopyFileToTargetDir( sFromFB2Path, sToFilePath, nFileExistMode, false );
							IncStatus( 11 ); // нечитаемые fb2-файлв или архивы
						}
					}
					if( bDelFB2FilesMode ) {
						// удаляем исходный fb2-файл
						if( File.Exists( sFromFilePath ) ) {
							File.Delete( sFromFilePath );
						}
					}
				} else {
					// пропускаем не fb2-файлы и архивы
					IncStatus( 9 ); // другие файлы
				}
			}
		}
		
		private void IncStatus( int nItem ) {
			lvFilesCount.Items[nItem].SubItems[1].Text	=
					Convert.ToString( 1+Convert.ToInt32( lvFilesCount.Items[nItem].SubItems[1].Text ) );
			lvFilesCount.Refresh();
		}

		private void SortFb2Files( string sSource )
		{
			// полная сортировка файлов
			string sTarget = tboxSortAllToDir.Text.Trim();
			Regex rx = new Regex( @"\\+$" );
			sTarget = rx.Replace( sTarget, "" );
			tboxSortAllToDir.Text = sTarget;
			
			if( sTarget == "") {
				MessageBox.Show( "Не указана папка-приемник файлов!\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			if( sSource == sTarget ) {
				MessageBox.Show( "Папка-приемник файлов совпадает с папкой сканирования!\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			DirectoryInfo diFolder = new DirectoryInfo( sTarget );
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка не найдена: " + sTarget + "\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			
			string sLineTemplate = "";
			if( rBtnTemplatesPrepared.Checked ) {
				sLineTemplate = cboxTemplatesPrepared.Text.Trim();
			} else {
				sLineTemplate = txtBoxTemplatesFromLine.Text.Trim();
			}
			// проверка "пустоту" строки с шаблонами
			if( sLineTemplate == "" ) {
				MessageBox.Show( "Строка шаблонов не может быть пустой!\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			// проверка на корректность строки с шаблонами
			if( !Templates.TemplatesVerify.IsLineTemplatesCorrect( sLineTemplate ) ) {
				MessageBox.Show( "Строка содержит или недопустимые шаблоны,\nили недопустимые символы */|?<>\"&\\t\\r\\n между шаблонами!\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			// проверка на четность * в строке с шаблонами
			if( !Templates.TemplatesVerify.IsEvenElements( sLineTemplate, '*' ) ) {
				MessageBox.Show( "Строка с шаблонами подстановки содержит нечетное число *!\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			// проверка, не стоит ли ] перед [
			if( sLineTemplate.IndexOf('[')!=-1 && sLineTemplate.IndexOf(']')!=-1 ) {
				if( sLineTemplate.IndexOf('[') > sLineTemplate.IndexOf(']') ) {
					MessageBox.Show( "В строке с шаблонами закрывающая скобка ] не может стоять перед открывающей [ !\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return;
				}
			}
			
			// проверка на соответствие [ ] в строке с шаблонами
			if( !Templates.TemplatesVerify.IsBracketsCorrect( sLineTemplate, '[', ']' ) ) {
				MessageBox.Show( "В строке с шаблонами переименования нет соответствия между открывающим и закрывающими скобками [ ]!\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			// проверка на соответствие ( ) в строке с шаблонами
			if( !Templates.TemplatesVerify.IsBracketsCorrect( sLineTemplate, '(', ')' ) ) {
				MessageBox.Show( "В строке с шаблонами нет соответствия между открывающим и закрывающими скобками ( )!\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			// проверка на \ в начале строки с шаблонами
			if( sLineTemplate[0]=='\\' ) {
				MessageBox.Show( "Строка с шаблонами не может начинаться с '\\'!\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			// проверка на \ в конце строки с шаблонами
			if( sLineTemplate[sLineTemplate.Length-1]=='\\' ) {
				MessageBox.Show( "Строка с шаблонами не может заканчиваться на '\\' !\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			// проверка условных шаблонов на наличие в них вспом. символов без самих шаблонов
			if( !Templates.TemplatesVerify.IsConditionalPatternCorrect( sLineTemplate ) ) {
				MessageBox.Show( "Условные шаблоны [] в строке с шаблонами не могут содержать вспомогательных символов БЕЗ самих шаблонов!\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			// проверка на множественность символа папки \ в строке с шаблонами
			if( sLineTemplate.IndexOf( "\\\\" )!=-1 ) {
				MessageBox.Show( "Строка с шаблонами не может содержать несколько идущих подряд символов папки '\\' !\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			
			DateTime dtStart = DateTime.Now;
			// инициализация контролов
			Init();
			tsProgressBar.Visible = true;
			// сортированный список всех вложенных папок
			List<string> lDirList = new List<string>();
			if( !chBoxScanSubDir.Checked ) {
				// сканировать только указанную папку
				lDirList.Add( sSource );
				lvFilesCount.Items[0].SubItems[1].Text = "1";
				lvFilesCount.Refresh();
			} else {
				// сканировать и все подпапки
				lDirList = FilesWorker.FilesWorker.DirsParser( sSource, lvFilesCount );
				lDirList.Sort();
			}
			// сортированный список всех файлов
			tsslblProgress.Text = "Создание списка файлов:";
			List<string> lFilesList = FilesWorker.FilesWorker.AllFilesParser( lDirList, ssProgress, lvFilesCount, tsProgressBar );
			//lFilesList.Sort();

			int nFilesCount = lFilesList.Count;
			if( nFilesCount == 0 ) {
				MessageBox.Show( "В папке сканирования не найдено ни одного файла!\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
				tsslblProgress.Text = Settings.Settings.GetReady();
				tsProgressBar.Visible = false;
				return;
			}
			
			// сортировка файлов по папкам, согласно шаблонам подстановки
			tsslblProgress.Text = "Сортировка файлов:";
			tsProgressBar.Visible = true;
			tsProgressBar.Maximum = nFilesCount+1;
			tsProgressBar.Value = 1;
			string sTempDir = Settings.Settings.GetTempDir();
			bool b1Autor = Settings.Settings.ReadAuthorOneMode();
			bool b1Genre = Settings.Settings.ReadGenreOneMode();
			// формируем лексемы шаблонной строки
			List<Templates.Lexems.TPSimple> lSLexems = Templates.TemplatesParser.GemSimpleLexems( sLineTemplate );
			foreach( string sFromFilePath in lFilesList ) {
				// создаем файл по новому пути
				if( b1Genre && b1Autor ) {
					// по первому Жанру и первому Автору Книги
					MakeFileFor1Genre1Author( sFromFilePath, sSource, sTarget, lSLexems );
				} else if( b1Genre && !b1Autor ) {
					// по первому Жанру и всем Авторам Книги
					MakeFileFor1GenreAllAuthor( sFromFilePath, sSource, sTarget, lSLexems );
				} else if( !b1Genre && b1Autor ) {
					// по всем Жанрам и первому Автору Книги
					MakeFileForAllGenre1Author( sFromFilePath, sSource, sTarget, lSLexems );
				} else {
					// по всем Жанрам и всем Авторам Книги
					MakeFileForAllGenreAllAuthor( sFromFilePath, sSource, sTarget, lSLexems );
				}
				++tsProgressBar.Value;
				ssProgress.Refresh();
			}
			FilesWorker.FilesWorker.RemoveDir( sTempDir );
			DateTime dtEnd = DateTime.Now;
			string sTime = dtEnd.Subtract( dtStart ).ToString() + " (час.:мин.:сек.)";
			string sMess = "Сортировка файлов в указанную папку завершена!\nЗатрачено времени: "+sTime;
			MessageBox.Show( sMess, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
			tsslblProgress.Text = Settings.Settings.GetReady();
			tsProgressBar.Visible = false;
		}

		#endregion
		
		#region Обработчики событий
		void TsbtnOpenDirClick(object sender, EventArgs e)
		{
			// задание папки с fb2-файлами и архивами для сканирования
			if( tboxSourceDir.Text !="" ) {
				fbdScanDir.SelectedPath = tboxSourceDir.Text;
			}
			DialogResult result = fbdScanDir.ShowDialog();
			if (result == DialogResult.OK) {
				string openFolderName = fbdScanDir.SelectedPath;
                tboxSourceDir.Text = openFolderName;
            }
		}

		void RBtnTemplatesPreparedCheckedChanged(object sender, EventArgs e)
		{
			cboxTemplatesPrepared.Enabled = rBtnTemplatesPrepared.Checked;
		}
		
		void RBtnTemplatesFromLineCheckedChanged(object sender, EventArgs e)
		{
			txtBoxTemplatesFromLine.Enabled = rBtnTemplatesFromLine.Checked;
			txtBoxTemplatesFromLine.Focus();
		}
		
		void TsbtnSortFilesToClick(object sender, EventArgs e)
		{
			// сортировка
			string sSource = tboxSourceDir.Text.Trim();
			if( sSource == "" ) {
				MessageBox.Show( "Выберите папку для сканирования!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			DirectoryInfo diFolder = new DirectoryInfo( sSource );
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка не найдена: " + sSource, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			
			SortFb2Files( sSource );
		}
		
		void BtnSortAllToDirClick(object sender, EventArgs e)
		{
			// задание папки-приемника для размешения отсортированных файлов
			FilesWorker.FilesWorker.OpenDirDlg( tboxSortAllToDir, fbdScanDir, "Укажите папку-приемник для размешения отсортированных файлов:" );
		}
		#endregion
		
	}
}
