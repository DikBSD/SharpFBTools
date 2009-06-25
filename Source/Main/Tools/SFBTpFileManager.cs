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
using System.Xml;

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
using FilesWorker;
using Settings;
using FB2.Genres;

using fB2Parser = FB2.FB2Parsers.FB2Parser;

namespace SharpFBTools.Tools
{
	/// <summary>
	/// Description of SFBTpFileManager.
	/// </summary>
	public partial class SFBTpFileManager : UserControl
	{
		private FB2Parser.FB2Validator fv2V = new FB2Parser.FB2Validator();
		private List<SelectedSortQueryCriteria> m_lSSQCList = null; // список критериев поиска для Избранной Сортировки
		
		public ListView GetSettingsInfoListView()
		{
			return lvSettings;
		}
		
		public SFBTpFileManager()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			Init();
			// читаем сохраненные пути к папкам и шаблон Менеджера Файлов, если они есть
			ReadFMTempData();
			//
			string sTDPath = Settings.SettingsFM.GetDefFMDescTemplatePath();
			if( File.Exists( sTDPath ) ) {
				richTxtBoxDescTemplates.LoadFile( sTDPath );
			} else {
				richTxtBoxDescTemplates.Text = "Не найден файл описания Шаблонов подстановки: \""+sTDPath+"\"";
			}

			// загружаем в ListView-индикатор настроек данные 
			Settings.SettingsFM.SetInfoSettings( lvSettings );
		}
		
		#region Закрытые вспомогательные методы класса
		private void Init() {
			// инициализация контролов и переменных
			for( int i=0; i!=lvFilesCount.Items.Count; ++i ) {
				lvFilesCount.Items[i].SubItems[1].Text	= "0";
			}
			tsProgressBar.Value		= 1;
			tsslblProgress.Text		= Settings.Settings.GetReady();
			tsProgressBar.Visible	= false;
			// очистка временной папки
			FilesWorker.FilesWorker.RemoveDir( Settings.Settings.GetTempDir() );
		}
		
		private bool IsFolderdataCorrect( TextBox tbSource, TextBox tbTarget, string sMessTitle )
		{
			// проверка на корректность данных папок источника и приемника файлов
			string sSource = tbSource.Text.Trim();
			string sTarget = tbTarget.Text.Trim();
			Regex rx = new Regex( @"\\+$" );
			sSource = rx.Replace( sSource, "" );
			tbSource.Text = sSource;
			rx = new Regex( @"\\+$" );
			sTarget = rx.Replace( sTarget, "" );
			tbTarget.Text = sTarget;
			
			// проверки на корректность папок источника и приемника
			if( sSource.Length == 0 ) {
				MessageBox.Show( "Выберите папку для сканирования!", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			DirectoryInfo diFolder = new DirectoryInfo( sSource );
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка не найдена: " + sSource, sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			if( sTarget.Length == 0 ) {
				MessageBox.Show( "Не указана папка-приемник файлов!\nРабота прекращена.", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			diFolder = new DirectoryInfo( sTarget );
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка не найдена: " + sTarget + "\nРабота прекращена.", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			if( sSource == sTarget ) {
				MessageBox.Show( "Папка-приемник файлов совпадает с папкой сканирования!\nРабота прекращена.", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			return true;
		}
		
		private void ReadFMTempData() {
			// чтение путей к данным Менеджера Файлов из xml-файла
			string sSettings = Settings.Settings.WorksDataSettingsPath;
			if( !File.Exists( sSettings ) ) return;
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.IgnoreWhitespace = true;
			using ( XmlReader reader = XmlReader.Create( sSettings, settings ) ) {
				reader.ReadToFollowing("FMScanDir");
				if (reader.HasAttributes ) {
					tboxSourceDir.Text = reader.GetAttribute("tboxSourceDir");
					Settings.SettingsFM.FMDataScanDir = tboxSourceDir.Text.Trim();
				}
				reader.ReadToFollowing("FMTargetDir");
				if (reader.HasAttributes ) {
					tboxSortAllToDir.Text = reader.GetAttribute("tboxSortAllToDir");
					Settings.SettingsFM.FMDataTargetDir = tboxSortAllToDir.Text.Trim();
				}
				reader.ReadToFollowing("FMTemplate");
				if (reader.HasAttributes ) {
					txtBoxTemplatesFromLine.Text = reader.GetAttribute("txtBoxTemplatesFromLine");
					Settings.SettingsFM.FMDataTemplate =  txtBoxTemplatesFromLine.Text.Trim();
				}
				reader.Close();
			}
		}
		
		private bool IsArchive( string sExt )
		{
			return ( sExt==".zip" || sExt==".rar" || sExt==".7z" || sExt==".bz2" || sExt==".gz" || sExt==".tar" );
		}
		
		private List<string> GetFileListFromArchive( string sFromFile, Settings.DataFM dfm ) {
			// Распаковать архив во временную папку
			string sTempDir 	= Settings.Settings.GetTempDir();
			string sExt			= Path.GetExtension( sFromFile ).ToLower();
			FilesWorker.FilesWorker.RemoveDir( sTempDir );
			switch( sExt ) {
				case ".rar":
					FilesWorker.Archiver.unrar( dfm.UnRarPath, sFromFile, sTempDir );
					IncStatus( 4, true );
					break;
				case ".zip":
					FilesWorker.Archiver.unzip( dfm.A7zaPath, sFromFile, sTempDir );
					IncStatus( 3, true );
					break;
				case ".7z":
					FilesWorker.Archiver.unzip( dfm.A7zaPath, sFromFile, sTempDir );
					IncStatus( 5, true );
					break;
				case ".bz2":
					FilesWorker.Archiver.unzip( dfm.A7zaPath, sFromFile, sTempDir );
					IncStatus( 6, true );
					break;
				case ".gz":
					FilesWorker.Archiver.unzip( dfm.A7zaPath, sFromFile, sTempDir );
					IncStatus( 7, true );
					break;
				case ".tar":
					FilesWorker.Archiver.unzip( dfm.A7zaPath, sFromFile, sTempDir );
					IncStatus( 8, true );
					break;
			}
			// составляем список файлов (или одного) из архива
			return MakeFileListFromDir( sTempDir, false, true );
		}
		
		private List<string> MakeFileListFromDir( string sFromDir, bool bSort, bool bFB2Only ) {
			// составляем список файлов (или одного) из архива
			List<string> lFilesList = null;
			if( Directory.Exists( sFromDir ) ) {
				string [] files = Directory.GetFiles( sFromDir );
				if( files.Length != 0 ) {
					lFilesList = new List<string>();
					foreach( string sFile in files ) {
						if( bFB2Only ) {
							if( Path.GetExtension( Path.GetFileName( sFile ) )==".fb2" ) {
								lFilesList.Add( sFile );
							}
						} else {
							lFilesList.Add( sFile );
						}
					}
					if( bSort ) {
						lFilesList.Sort();
					}
				}
			}
			return lFilesList;
		}
		
		private void CreateFileTo( string sFromFilePath, string sToFilePath, int nFileExistMode, bool bAddToFileNameBookIDMode,
		                         Settings.DataFM dfm ) {
			// создание нового файла или архива
			try {
				if( !dfm.ToArchiveMode ) {
					CopyFileToTargetDir( sFromFilePath, sToFilePath, false, nFileExistMode, bAddToFileNameBookIDMode );
				} else {
					// упаковка в архив
					string sArchType = StringProcessing.StringProcessing.GetArchiveExt( dfm.ArchiveTypeText );
					CopyFileToArchive( dfm.A7zaPath, dfm.RarPath, sArchType, sFromFilePath, sToFilePath+"."+sArchType, 
					                  nFileExistMode, bAddToFileNameBookIDMode );
				}
			} catch ( System.IO.PathTooLongException ) {
				string sFileLongPathDir = dfm.FileLongPathDir;
				Directory.CreateDirectory( sFileLongPathDir );
				sToFilePath = sFileLongPathDir+"\\"+Path.GetFileName( sFromFilePath );
				CopyFileToTargetDir( sFromFilePath, sToFilePath, true, nFileExistMode, false );	
			}
		}
		
		private void CopyFileToArchive( string s7zaPath, string sRarPath, string sArchType,
		                               string sFromFilePath, string sToFilePath,
		                               int nFileExistMode, bool bAddToFileNameBookIDMode ) {
			// архивирование файла с сформированным именем (путь)
			// обработка уже существующих файлов в папке
			Regex rx = new Regex( @"\\+" );
			sFromFilePath = rx.Replace( sFromFilePath, "\\" );
			sToFilePath = rx.Replace( sToFilePath, "\\" );
			
			sToFilePath = FileExsistWorker( sFromFilePath, sToFilePath, nFileExistMode, bAddToFileNameBookIDMode, sArchType );
			if( sArchType == "rar" ) {
				FilesWorker.Archiver.rar( sRarPath, sFromFilePath, sToFilePath, true );
			} else {
				FilesWorker.Archiver.zip( s7zaPath, sArchType, sFromFilePath, sToFilePath );
			}
			IncStatus( 11, true ); // всего создано
		}
		
		private void CopyFileToTargetDir( string sFromFilePath, string sToFilePath, bool bBad,
		                                 int nFileExistMode, bool bAddToFileNameBookIDMode )
		{
			// копирование файла с сформированным именем (путь)
			Regex rx = new Regex( @"\\+" );
			sFromFilePath = rx.Replace( sFromFilePath, "\\" );
			sToFilePath = rx.Replace( sToFilePath, "\\" );
			// обработка уже существующих файлов в папке
			sToFilePath = FileExsistWorker( sFromFilePath, sToFilePath, nFileExistMode, bAddToFileNameBookIDMode, "" );
			if( File.Exists( sFromFilePath ) ) {
				File.Copy( sFromFilePath, sToFilePath );
				if( !bBad ) {
					if( File.Exists( sToFilePath ) ) {
					   	IncStatus( 11, true ); // всего создано
					}
				}
			}
		}
		
		private void CopyBadArchiveToBadDir( string sFromFilePath, string sSource, string sToDir, int nFileExistMode )
		{
			// копирование "битого" с сформированным именем (путь)
			string sToFilePath = sToDir+"\\"+sFromFilePath.Remove( 0, sSource.Length );
			Regex rx = new Regex( @"\\+" );
			sFromFilePath = rx.Replace( sFromFilePath, "\\" );
			sToFilePath = rx.Replace( sToFilePath, "\\" );
			string sSufix = "";
			FileInfo fi = new FileInfo( sToFilePath );
			if( !fi.Directory.Exists ) {
				Directory.CreateDirectory( fi.Directory.ToString() );
			}
			// обработка уже существующих файлов в папке
			if( File.Exists( sToFilePath ) ) {
				if( nFileExistMode == 0 ) {
					File.Delete( sToFilePath );
				} else {
					if( nFileExistMode == 1 ) {
						// Добавить к создаваемому архиву очередной номер
						sSufix += "_" + StringProcessing.StringProcessing.GetFileNewNumber( sToFilePath ).ToString();
					} else {
						// Добавить к создаваемому архиву дату и время
						sSufix += "_" + StringProcessing.StringProcessing.GetDateTimeExt();
					}
					sToFilePath = sToFilePath.Remove( sToFilePath.Length-4 ) + sSufix + Path.GetExtension( sToFilePath );
				}
			}
			if( File.Exists( sFromFilePath ) ) {
				File.Copy( sFromFilePath, sToFilePath );
			}
			IncStatus( 14, true ); // "битые" архивы - не открылись
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
		                                      List<Templates.Lexems.TPSimple> lSLexems, Settings.DataFM dfm ) {
			// создаем файл по новому пути для первого Жанра и для первого Автора Книги
			string sExt = Path.GetExtension( sFromFilePath ).ToLower();
			if( sExt==".fb2" ) {
				MakeFileFor1Genre1AuthorWorker( sExt, sFromFilePath, sSource, sTarget, lSLexems, dfm, false );
				IncStatus( 2, true ); // исходные fb2-файлы
			} else {
				// это архив?
				if( IsArchive( sExt ) ) {
					List<string> lFilesListFromArchive = GetFileListFromArchive( sFromFilePath, dfm );
					if( lFilesListFromArchive==null ) {
						CopyBadArchiveToBadDir( sFromFilePath, sSource, dfm.NotOpenArchDir, dfm.FileExistMode );
						return; // не получилось открыть архив - "битый"
					}
					foreach( string sFB2FromArchPath in lFilesListFromArchive ) {
						MakeFileFor1Genre1AuthorWorker( Path.GetExtension( sFB2FromArchPath ).ToLower(), sFB2FromArchPath,
						                               sSource, sTarget, lSLexems, dfm, true );
						IncStatus( 9, true ); // Исходные fb2-файлы из архивов
					}
				}  else {
					// пропускаем не fb2-файлы и архивы
					IncStatus( 10, true ); // другие файлы
				}
			}
		}
		private void MakeFileFor1Genre1AuthorWorker( string sExt, string sFromFilePath, string sSource, string sTarget,
		                                      List<Templates.Lexems.TPSimple> lSLexems, Settings.DataFM dfm, bool bFromArchive ) {
			try {
				MakeFB2File( sFromFilePath, sSource, sTarget, lSLexems, dfm, bFromArchive, 0, 0 );
			} catch {
				if( sExt==".fb2" ) {
					CopyBadFileToDir( sFromFilePath, sSource, bFromArchive, dfm.NotReadFB2Dir, dfm.FileExistMode );
				}
			}
		}
		
		private void MakeFileForAllGenre1Author( string sFromFilePath, string sSource, string sTarget,
		                                      List<Templates.Lexems.TPSimple> lSLexems, Settings.DataFM dfm ) {
			// создаем файл по новому пути для всех Жанров и для первого Автора Книги
			string sExt = Path.GetExtension( sFromFilePath ).ToLower();
			if( sExt==".fb2" ) {
				MakeFileForAllGenre1AuthorWorker( sExt, sFromFilePath, sSource, sTarget, lSLexems, dfm, false ) ;
				IncStatus( 2, true ); // исходные fb2-файлы
			} else {
				// это архив?
				if( IsArchive( sExt ) ) {
					List<string> lFilesListFromArchive = GetFileListFromArchive( sFromFilePath, dfm );
					if( lFilesListFromArchive==null ) {
						CopyBadArchiveToBadDir( sFromFilePath, sSource, dfm.NotOpenArchDir, dfm.FileExistMode );
						return; // не получилось открыть архив - "битый"
					}
					foreach( string sFB2FromArchPath in lFilesListFromArchive ) {
						MakeFileForAllGenre1AuthorWorker( Path.GetExtension( sFB2FromArchPath ).ToLower(), sFB2FromArchPath,
						                                 sSource, sTarget, lSLexems, dfm, true ) ;
						IncStatus( 9, true ); // Исходные fb2-файлы из архивов
					}
				}  else {
					// пропускаем не fb2-файлы и архивы
					IncStatus( 10, true ); // другие файлы
				}
			}
		}
		private void MakeFileForAllGenre1AuthorWorker( string sExt, string sFromFilePath, string sSource, string sTarget,
		                                      List<Templates.Lexems.TPSimple> lSLexems, Settings.DataFM dfm, bool bFromArchive ) {
			try {
				fB2Parser fb2 = new fB2Parser( sFromFilePath );
				TitleInfo ti = fb2.GetTitleInfo();
				IList<Genre> lGenres = ti.Genres;
				IList<Author> lAuthors = ti.Authors;
				for( int i=0; i!=lGenres.Count; ++i ) {
					MakeFB2File( sFromFilePath, sSource, sTarget, lSLexems, dfm, bFromArchive, i, 0 );
				}
			} catch {
				if( sExt==".fb2" ) {
					CopyBadFileToDir( sFromFilePath, sSource, bFromArchive, dfm.NotReadFB2Dir, dfm.FileExistMode );
				}
			}
		}

		private void MakeFileFor1GenreAllAuthor( string sFromFilePath, string sSource, string sTarget,
		                                      	List<Templates.Lexems.TPSimple> lSLexems, Settings.DataFM dfm ) {
			// создаем файл по новому пути для первого Жанра и для всех Авторов Книги
			string sExt = Path.GetExtension( sFromFilePath ).ToLower();
			if( sExt==".fb2" ) {
				MakeFileFor1GenreAllAuthorWorker( sExt, sFromFilePath, sSource, sTarget, lSLexems, dfm, false );
				IncStatus( 2, true ); // исходные fb2-файлы
			} else {
				// это архив?
				if( IsArchive( sExt ) ) {
					List<string> lFilesListFromArchive = GetFileListFromArchive( sFromFilePath, dfm );
					if( lFilesListFromArchive==null ) {
						CopyBadArchiveToBadDir( sFromFilePath, sSource, dfm.NotOpenArchDir, dfm.FileExistMode );
						return; // не получилось открыть архив - "битый"
					}
					foreach( string sFB2FromArchPath in lFilesListFromArchive ) {
						MakeFileFor1GenreAllAuthorWorker( Path.GetExtension( sFB2FromArchPath ).ToLower(), sFB2FromArchPath,
						                                 sSource, sTarget, lSLexems, dfm, true );
						IncStatus( 9, true ); // Исходные fb2-файлы из архивов
					}
				} else {
					// пропускаем не fb2-файлы и архивы
					IncStatus( 10, true ); // другие файлы
				}
			}
		}
		private void MakeFileFor1GenreAllAuthorWorker( string sExt, string sFromFilePath, string sSource, string sTarget,
		                                      List<Templates.Lexems.TPSimple> lSLexems, Settings.DataFM dfm, bool bFromArchive ) {
			try {
				fB2Parser fb2 = new fB2Parser( sFromFilePath );
				TitleInfo ti = fb2.GetTitleInfo();
				IList<Genre> lGenres = ti.Genres;
				IList<Author> lAuthors = ti.Authors;
				for( int i=0; i!=lAuthors.Count; ++i ) {
					MakeFB2File( sFromFilePath, sSource, sTarget, lSLexems, dfm, bFromArchive, 0, i );
				}
			} catch {
				if( sExt==".fb2" ) {
					CopyBadFileToDir( sFromFilePath, sSource, bFromArchive, dfm.NotReadFB2Dir, dfm.FileExistMode );
				}
			}
		}
		
		private void MakeFileForAllGenreAllAuthor( string sFromFilePath, string sSource, string sTarget,
		                                      		List<Templates.Lexems.TPSimple> lSLexems, Settings.DataFM dfm) {
			// создаем файл по новому пути для всех Жанров и для всех Авторов Книги
			string sExt = Path.GetExtension( sFromFilePath ).ToLower();
			if( sExt==".fb2" ) {
				MakeFileForAllGenreAllAuthorWorker( sExt, sFromFilePath, sSource, sTarget, lSLexems, dfm, false );
				IncStatus( 2, true ); // исходные fb2-файлы
			} else {
				// это архив?
				if( IsArchive( sExt ) ) {
					List<string> lFilesListFromArchive = GetFileListFromArchive( sFromFilePath, dfm );
					if( lFilesListFromArchive==null ) {
						CopyBadArchiveToBadDir( sFromFilePath, sSource, dfm.NotOpenArchDir, dfm.FileExistMode );
						return; // не получилось открыть архив - "битый"
					}
					foreach( string sFB2FromArchPath in lFilesListFromArchive ) {
						MakeFileForAllGenreAllAuthorWorker( Path.GetExtension( sFB2FromArchPath ).ToLower(), sFB2FromArchPath,
						                                   sSource, sTarget, lSLexems, dfm, true );
						IncStatus( 9, true ); // Исходные fb2-файлы из архивов
					}
				}  else {
					// пропускаем не fb2-файлы и архивы
					IncStatus( 10, true ); // другие файлы
				}
			}
		}
		private void MakeFileForAllGenreAllAuthorWorker( string sExt, string sFromFilePath, string sSource, string sTarget,
		                                      List<Templates.Lexems.TPSimple> lSLexems, Settings.DataFM dfm, bool bFromArchive ) {
			try {
				fB2Parser fb2 = new fB2Parser( sFromFilePath );
				TitleInfo ti = fb2.GetTitleInfo();
				IList<Genre> lGenres = ti.Genres;
				IList<Author> lAuthors = ti.Authors;
				for( int i=0; i!= lGenres.Count; ++i ) {
					for( int j=0; j!=lAuthors.Count; ++j ) {
						MakeFB2File( sFromFilePath, sSource, sTarget, lSLexems, dfm, bFromArchive, i, j );
					}
				}
			} catch {
				if( sExt==".fb2" ) {
					CopyBadFileToDir( sFromFilePath, sSource, bFromArchive, dfm.NotReadFB2Dir, dfm.FileExistMode );
				}
			}
		}
		
		private bool IsValid( string sFromFilePath, string sSource, Settings.DataFM dfm,
		                     bool bFromArchive, int nGenreIndex, int nAuthorIndex ) {
			// если режим сортировки - только валидные - то проверка и копирование невалидных в папку
			string sResult = fv2V.ValidatingFB2File( sFromFilePath );
			if ( sResult.Length != 0 ) {
				// защита от многократного копирования невалимдного файла в папку для невалидных
				if( nGenreIndex==0 && nAuthorIndex==0 ) {
					// помещаем его в папку для невалидных файлов
					CopyBadFileToDir( sFromFilePath, sSource, bFromArchive, dfm.NotValidFB2Dir, dfm.FileExistMode );
					IncStatus( 13, true ); // не валидные fb2-файлы
					return false; // файл невалидный - пропускаем его, сортируем дальше
				} else {
					return false; // файл уже скопирован - пропускаем его, сортируем дальше
				}
			}
			return true;
		}
		
		private void CopyBadFileToDir( string sFromFilePath, string sSource, bool bFromArchive,
		                              string sBadDir, int nFileExistMode ) {
			// нечитаемый fb2-файл или архив - копируем его в папку Bad
			Directory.CreateDirectory( sBadDir );
			string sFrom = ( !bFromArchive ? sSource : Settings.Settings.GetTempDir() );
			string sToFilePath = sBadDir+"\\"+sFromFilePath.Remove( 0, sFrom.Length );
			CopyFileToTargetDir( sFromFilePath, sToFilePath, true, nFileExistMode, false );
			IncStatus( 12, true ); // нечитаемые fb2-файлы или архивы
		}
			
		private void MakeFB2File( string sFromFilePath, string sSource, string sTarget,
		                      List<Templates.Lexems.TPSimple> lSLexems, Settings.DataFM dfm,
		                      bool bFromArchive, int nGenreIndex, int nAuthorIndex ) {
			// создаем файл по новому пути
			string sTempDir = Settings.Settings.GetTempDir();
			// смотрим, что это за файл
			string sExt = Path.GetExtension( sFromFilePath ).ToLower();
			if( sExt == ".fb2" ) {
				// обработка fb2-файла
				// тип сортировки
				if( !dfm.SortValidType  ) {
					if( !IsValid( sFromFilePath, sSource, dfm, bFromArchive, nGenreIndex, nAuthorIndex ) ) {
						return;
					}
				}
				try {
					string sToFilePath = sTarget + "\\" +
							Templates.TemplatesParser.Parse( sFromFilePath, lSLexems, dfm, nGenreIndex, nAuthorIndex ) + ".fb2";
					CreateFileTo( sFromFilePath, sToFilePath, dfm.FileExistMode, dfm.AddToFileNameBookIDMode, dfm );
				} catch /*( System.IO.FileLoadException )*/ {
					// нечитаемый fb2-файл - копируем его в папку Bad
					CopyBadFileToDir( sFromFilePath, sSource, bFromArchive, dfm.NotReadFB2Dir, dfm.FileExistMode );
				}
				
				if( dfm.DelFB2FilesMode ) {
					// удаляем исходный fb2-файл
					if( File.Exists( sFromFilePath ) ) {
						File.Delete( sFromFilePath );
					}
				}
			}
		}
		
		private void IncStatus( int nItem, bool bRefresh ) {
			lvFilesCount.Items[nItem].SubItems[1].Text	=
					Convert.ToString( 1+Convert.ToInt32( lvFilesCount.Items[nItem].SubItems[1].Text ) );
			if( bRefresh  ) {
				lvFilesCount.Refresh();
			}
		}

		private bool IsArchivatorsExist( string sMessTitle ) {
			// проверка на наличие архиваторов
			string s7zPath	= Settings.Settings.Read7zaPath();
			string sRarPath	= Settings.Settings.ReadRarPath();
			if( Settings.SettingsFM.ReadToArchiveMode() ) {
				if( Settings.SettingsFM.ReadArchiveTypeText().ToLower()=="rar" ) {
					if( sRarPath.Trim().Length==0 ) {
						MessageBox.Show( "В Настройках выбрана rar-архивация отсортированных файлов.\nПри этом не указана папка с установленным консольным Rar-архиватором!\nУкажите путь к нему в Настройках.\nРабота остановлена!", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
						return false;
					} else {
						// проверка на наличие архиваторов
						if( !File.Exists( sRarPath ) ) {
							MessageBox.Show( "В Настройках выбрана rar-архивация отсортированных файлов.\nПри этом не найден файл консольного Rar-архиватора "+sRarPath+"!\nУкажите путь к нему в Настройках.\nРабота остановлена!", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
							return false;
						}
					}
				}
			}
			
			if( s7zPath.Trim().Length==0 ) {
				MessageBox.Show( "В Настройках не указана папка с установленным консольным 7Zip-архиватором!\nУкажите путь к нему в Настройках.\nРабота остановлена!", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			} else {
				if( !File.Exists( s7zPath ) ) {
					MessageBox.Show( "Не найден файл Zip-архиватора \""+s7zPath+"\"!\nУкажите путь к нему в Настройках.\nРабота остановлена!", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return false;
				}
			}
			return true;
		}
		
		private bool IsLineTemplateCorrect( string sLineTemplate, string sMessTitle ) {
			// проверки на корректность шаблонных строк
			// проверка "пустоту" строки с шаблонами
			if( sLineTemplate.Length == 0 ) {
				MessageBox.Show( "Строка шаблонов не может быть пустой!\nРабота прекращена.", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на корректность строки с шаблонами
			if( !Templates.TemplatesVerify.IsLineTemplatesCorrect( sLineTemplate ) ) {
				MessageBox.Show( "Строка содержит или недопустимые шаблоны,\nили недопустимые символы */|?<>\"&\\t\\r\\n между шаблонами!\nРабота прекращена.", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на четность * в строке с шаблонами
			if( !Templates.TemplatesVerify.IsEvenElements( sLineTemplate, '*' ) ) {
				MessageBox.Show( "Строка с шаблонами подстановки содержит нечетное число *!\nРабота прекращена.", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка, не стоит ли ] перед [
			if( sLineTemplate.IndexOf('[')!=-1 && sLineTemplate.IndexOf(']')!=-1 ) {
				if( sLineTemplate.IndexOf('[') > sLineTemplate.IndexOf(']') ) {
					MessageBox.Show( "В строке с шаблонами закрывающая скобка ] не может стоять перед открывающей [ !\nРабота прекращена.", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return false;
				}
			}
			// проверка на соответствие [ ] в строке с шаблонами
			if( !Templates.TemplatesVerify.IsBracketsCorrect( sLineTemplate, '[', ']' ) ) {
				MessageBox.Show( "В строке с шаблонами переименования нет соответствия между открывающим и закрывающими скобками [ ]!\nРабота прекращена.", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на соответствие ( ) в строке с шаблонами
			if( !Templates.TemplatesVerify.IsBracketsCorrect( sLineTemplate, '(', ')' ) ) {
				MessageBox.Show( "В строке с шаблонами нет соответствия между открывающим и закрывающими скобками ( )!\nРабота прекращена.", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на \ в начале строки с шаблонами
			if( sLineTemplate[0]=='\\' ) {
				MessageBox.Show( "Строка с шаблонами не может начинаться с '\\'!\nРабота прекращена.", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на \ в конце строки с шаблонами
			if( sLineTemplate[sLineTemplate.Length-1]=='\\' ) {
				MessageBox.Show( "Строка с шаблонами не может заканчиваться на '\\' !\nРабота прекращена.", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка условных шаблонов на наличие в них вспом. символов без самих шаблонов
			if( !Templates.TemplatesVerify.IsConditionalPatternCorrect( sLineTemplate ) ) {
				MessageBox.Show( "Условные шаблоны [] в строке с шаблонами не могут содержать вспомогательных символов БЕЗ самих шаблонов!\nРабота прекращена.", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			// проверка на множественность символа папки \ в строке с шаблонами
			if( sLineTemplate.IndexOf( "\\\\" )!=-1 ) {
				MessageBox.Show( "Строка с шаблонами не может содержать несколько идущих подряд символов папки '\\' !\nРабота прекращена.", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return false;
			}
			return true;
		}
		
		private void SortFb2Files( string sSource, string sTarget, string sLineTemplate ) {
			// полная сортировка файлов
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
				tsslblProgress.Text = "Создание списка папок:";
				lDirList = FilesWorker.FilesWorker.DirsParser( sSource, lvFilesCount, false );
				lvFilesCount.Refresh();
			}
			// сортированный список всех файлов
			tsslblProgress.Text = "Создание списка файлов:";
			ssProgress.Refresh();
			List<string> lFilesList = FilesWorker.FilesWorker.AllFilesParser( lDirList, ssProgress, lvFilesCount,
			                                                                 tsProgressBar, false, false );

			// проверка, есть ли хоть один файл в папке для сканирования
			lvFilesCount.Refresh();
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
			ssProgress.Refresh();
	
			// данные настроек для сортировки по шаблонам
			Settings.DataFM dfm = new Settings.DataFM();
			string sTempDir = Settings.Settings.GetTempDir();
			
			// формируем лексемы шаблонной строки
			List<Templates.Lexems.TPSimple> lSLexems = Templates.TemplatesParser.GemSimpleLexems( sLineTemplate );
			// сортировка
			foreach( string sFromFilePath in lFilesList ) {
				// создаем файл по новому пути
				if( dfm.GenreOneMode && dfm.AuthorOneMode ) {
					// по первому Жанру и первому Автору Книги
					MakeFileFor1Genre1Author( sFromFilePath, sSource, sTarget, lSLexems, dfm );
				} else if( dfm.GenreOneMode && !dfm.AuthorOneMode ) {
					// по первому Жанру и всем Авторам Книги
					MakeFileFor1GenreAllAuthor( sFromFilePath, sSource, sTarget, lSLexems, dfm );
				} else if( !dfm.GenreOneMode && dfm.AuthorOneMode ) {
					// по всем Жанрам и первому Автору Книги
					MakeFileForAllGenre1Author( sFromFilePath, sSource, sTarget, lSLexems, dfm );
				} else {
					// по всем Жанрам и всем Авторам Книги
					MakeFileForAllGenreAllAuthor( sFromFilePath, sSource, sTarget, lSLexems, dfm );
				}
				++tsProgressBar.Value;
				ssProgress.Refresh();
			}
			// завершение работы
			FilesWorker.FilesWorker.RemoveDir( sTempDir );
			DateTime dtEnd = DateTime.Now;
			string sTime = dtEnd.Subtract( dtStart ).ToString() + " (час.:мин.:сек.)";
			string sMess = "Сортировка файлов в указанную папку завершена!\nЗатрачено времени: "+sTime;
			MessageBox.Show( sMess, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
			tsslblProgress.Text = Settings.Settings.GetReady();
			tsProgressBar.Visible = false;
		}

		void SelectedSortFb2Files( string sSource, string sTarget, string sLineTemplate ) {
			// Избранная Сортировка файлов
			DateTime dtStart = DateTime.Now;
			// инициализация контролов
			Init();
			tsProgressBar.Visible = true;
			// сортированный список всех вложенных папок
			List<string> lDirList = new List<string>();
			if( !chBoxSSScanSubDir.Checked ) {
				// сканировать только указанную папку
				lDirList.Add( sSource );
				lvFilesCount.Items[0].SubItems[1].Text = "1";
				lvFilesCount.Refresh();
			} else {
				// сканировать и все подпапки
				tsslblProgress.Text = "Создание списка папок:";
				lDirList = FilesWorker.FilesWorker.DirsParser( sSource, lvFilesCount, false );
				lvFilesCount.Refresh();
			}
			// сортированный список всех файлов
			tsslblProgress.Text = "Создание списка файлов:";
			ssProgress.Refresh();
			List<string> lFilesList = FilesWorker.FilesWorker.AllFilesParser( lDirList, ssProgress, lvFilesCount,
			                                                                 tsProgressBar, false, false );
			
			// проверка, есть ли хоть один файл в папке для сканирования
			lvFilesCount.Refresh();
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
			ssProgress.Refresh();
	
			// данные настроек для сортировки по шаблонам
			Settings.DataFM dfm = new Settings.DataFM();
			string sTempDir = Settings.Settings.GetTempDir();
			// формируем лексемы шаблонной строки
			List<Templates.Lexems.TPSimple> lSLexems = Templates.TemplatesParser.GemSimpleLexems( sLineTemplate );
			// сортировка
			string sExt = "";
			foreach( string sFromFilePath in lFilesList ) {
				/* создаем файл по новому пути */
				sExt = Path.GetExtension( sFromFilePath ).ToLower();
				if( sExt==".fb2" ) {
					// проверка, соответствует ли текущий файл критерия поиска для Избранной Сортировки
					if( IsConformity( sFromFilePath ) ) {
						if( dfm.GenreOneMode && dfm.AuthorOneMode ) {
							// по первому Жанру и первому Автору Книги
							MakeFileFor1Genre1Author( sFromFilePath, sSource, sTarget, lSLexems, dfm );
						} else if( dfm.GenreOneMode && !dfm.AuthorOneMode ) {
							// по первому Жанру и всем Авторам Книги
							MakeFileFor1GenreAllAuthor( sFromFilePath, sSource, sTarget, lSLexems, dfm );
						} else if( !dfm.GenreOneMode && dfm.AuthorOneMode ) {
							// по всем Жанрам и первому Автору Книги
							MakeFileForAllGenre1Author( sFromFilePath, sSource, sTarget, lSLexems, dfm );
						} else {
							// по всем Жанрам и всем Авторам Книги
							MakeFileForAllGenreAllAuthor( sFromFilePath, sSource, sTarget, lSLexems, dfm );
						}
					}
				} else {
					// это архив?
					if( IsArchive( sExt ) ) {
						List<string> lFilesListFromArchive = GetFileListFromArchive( sFromFilePath, dfm );
						if( lFilesListFromArchive!=null ) {
							foreach( string sFB2FromArchPath in lFilesListFromArchive ) {
								// проверка, соответствует ли текущий файл критерия поиска для Избранной Сортировки
								if( IsConformity( sFB2FromArchPath ) ) {
									if( dfm.GenreOneMode && dfm.AuthorOneMode ) {
										// по первому Жанру и первому Автору Книги
										MakeFileFor1Genre1Author( sFB2FromArchPath, sSource, sTarget, lSLexems, dfm );
									} else if( dfm.GenreOneMode && !dfm.AuthorOneMode ) {
										// по первому Жанру и всем Авторам Книги
										MakeFileFor1GenreAllAuthor( sFB2FromArchPath, sSource, sTarget, lSLexems, dfm );
									} else if( !dfm.GenreOneMode && dfm.AuthorOneMode ) {
										// по всем Жанрам и первому Автору Книги
										MakeFileForAllGenre1Author( sFB2FromArchPath, sSource, sTarget, lSLexems, dfm );
									} else {
										// по всем Жанрам и всем Авторам Книги
										MakeFileForAllGenreAllAuthor( sFB2FromArchPath, sSource, sTarget, lSLexems, dfm );
									}
								}
							}
						}
					}
				}
				++tsProgressBar.Value;
				ssProgress.Refresh();
			}
			// завершение работы
			FilesWorker.FilesWorker.RemoveDir( sTempDir );
			DateTime dtEnd = DateTime.Now;
			string sTime = dtEnd.Subtract( dtStart ).ToString() + " (час.:мин.:сек.)";
			string sMess = "Сортировка файлов в указанную папку завершена!\nЗатрачено времени: "+sTime;
			MessageBox.Show( sMess, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
			tsslblProgress.Text = Settings.Settings.GetReady();
			tsProgressBar.Visible = false;
		}
		
		private bool IsConformity( string sFromFilePath ) {
			// проверка, соответствует ли текущий файл критерия поиска для Избранной Сортировки
			fB2Parser fb2	= null;
			TitleInfo ti	= null;
			try {
				fb2	= new fB2Parser( sFromFilePath );
				ti	= fb2.GetTitleInfo();
				if( ti==null ) return false;
			} catch {
				return false;
			}
			bool bRet = true; // флаг, нашли ли соответствие
			string			sFB2Lang		= ti.Lang;
			IList<Genre>	lFB2Genres		= ti.Genres;
			IList<Author>	lFB2Authors		= ti.Authors;
			IList<Sequence>	lFB2Sequences	= ti.Sequences;
			string sLang, sFirstName, sGenre, sMiddleName, sLastName, sNickName, sSequence;
			foreach( SelectedSortQueryCriteria ssqc in m_lSSQCList ) {
				sLang		= ssqc.Lang;
				sGenre		= ssqc.Genre;
				sFirstName	= ssqc.FirstName;
				sMiddleName	= ssqc.MiddleName;
				sLastName	= ssqc.LastName;
				sNickName	= ssqc.NickName;
				sSequence	= ssqc.Sequence;
				// проверка языка книги
				if( sFB2Lang != null ) {
					if( sLang.Length != 0 ) {
						if( sLang != sFB2Lang ) {
							bRet = false; continue;
						}
					}
				} else {
					// в книге тега языка нет
					if( sLang.Length != 0 ) {
						bRet = false; continue;
					}
				}
				// проверка жанра книги
				bool b = false;
				if( lFB2Genres != null ) {
					if( sGenre.Length != 0 ) {
						foreach( Genre gfb2 in lFB2Genres ) {
							if( gfb2.Name == sGenre ) {
								b = true; break;
							}
						}
						if( !b ) {
							bRet = false; continue;
						}
					}
				} else {
					// в книге тега жанра нет
					if( sGenre.Length != 0 ) {
						bRet = false; continue;
					}
				}
				// проверка серии книги
				b = false;
				if( lFB2Sequences != null ) {
					if( sSequence.Length != 0 ) {
						foreach( Sequence sfb2 in lFB2Sequences ) {
							if( sfb2.Name == sSequence ) {
								b = true; break;
							}
						}
						if( !b ) {
							bRet = false; continue;
						}
					}
				} else {
					// в книге тега серии нет
					if( sSequence.Length != 0 ) {
						bRet = false; continue;
					}
				}
				if( lFB2Authors != null ) {
					b = false;
					if( sFirstName.Length != 0 ) {
						foreach( Author afb2 in lFB2Authors ) {
							if( afb2.FirstName.Value == sFirstName ) {
								b = true; break;
							}
						}
						if( !b ) {
							bRet = false; continue;
						}
					}
					b = false;
					if( sMiddleName.Length != 0 ) {
						foreach( Author afb2 in lFB2Authors ) {
							if( afb2.MiddleName.Value == sMiddleName ) {
								b = true; break;
							}
						}
						if( !b ) {
							bRet = false; continue;
						}
					}
					b = false;
					if( sLastName.Length != 0 ) {
						foreach( Author afb2 in lFB2Authors ) {
							if( afb2.LastName.Value == sLastName ) {
								b = true; break;
							}
						}
						if( !b ) {
							bRet = false; continue;
						}
					}
					b = false;
					if( sNickName.Length != 0 ) {
						foreach( Author afb2 in lFB2Authors ) {
							if( afb2.NickName.Value == sNickName ) {
								b = true; break;
							}
						}
						if( !b ) {
							bRet = false; continue;
						}
					}
				} else {
					// в книге тегов автора нет
					if( sFirstName.Length != 0 || sMiddleName.Length != 0 ||
					  	sNickName.Length != 0 || sNickName.Length != 0 ) continue;
				}
				bRet = true; break;
			}
			return bRet;
		}
		
		#endregion
		
		#region Обработчики событий
		void TsbtnOpenDirClick(object sender, EventArgs e)
		{
			// задание папки с fb2-файлами и архивами для сканирования
			FilesWorker.FilesWorker.OpenDirDlg( tboxSourceDir, fbdScanDir, "Укажите папку для сканирования с fb2-файлами и архивами:" );
		}
		
		void TsbtnSortFilesToClick(object sender, EventArgs e)
		{
			// Полная сортировка
			string sMessTitle = "SharpFBTools - Полная Сортировка";
			// проверка на корректность данных папок источника и приемника файлов
			if( !IsFolderdataCorrect( tboxSourceDir, tboxSortAllToDir, sMessTitle ) ) {
				return;
			}
			// проверка на наличие архиваторов
			if( !IsArchivatorsExist( sMessTitle ) ) {
				return;
			}
			// проверки на корректность шаблонных строк
			if( !IsLineTemplateCorrect( txtBoxTemplatesFromLine.Text.Trim(), sMessTitle ) ) {
				return;
			}

			SortFb2Files( tboxSourceDir.Text, tboxSortAllToDir.Text, txtBoxTemplatesFromLine.Text.Trim() );
		}
		
		void BtnSortAllToDirClick(object sender, EventArgs e)
		{
			// задание папки-приемника для размешения отсортированных файлов
			FilesWorker.FilesWorker.OpenDirDlg( tboxSortAllToDir, fbdScanDir, "Укажите папку-приемник для размешения отсортированных файлов:" );
		}
		
		void TboxSourceDirTextChanged(object sender, EventArgs e)
		{
			Settings.SettingsFM.FMDataScanDir = tboxSourceDir.Text;
		}
		
		void TboxSortAllToDirTextChanged(object sender, EventArgs e)
		{
			Settings.SettingsFM.FMDataTargetDir = tboxSortAllToDir.Text;
		}
		
		void TxtBoxTemplatesFromLineTextChanged(object sender, EventArgs e)
		{
			Settings.SettingsFM.FMDataTemplate = txtBoxTemplatesFromLine.Text;
		}
		
		void BtnInsertTemplatesClick(object sender, EventArgs e)
		{
			// запуск диалога Вставки готовых шаблонов
			BasiclTemplates btfrm = new BasiclTemplates();
			btfrm.ShowDialog();
			if( btfrm.GetTemplateLine()!=null ) {
				txtBoxTemplatesFromLine.Text = btfrm.GetTemplateLine();
			}
			btfrm.Dispose();
		}
		
		void BtnSSGetDataClick(object sender, EventArgs e)
		{
			// запуск диалога Сбора данных для Избранной Сортировки
			#region Код
			SelectedSortData ssdfrm = new SelectedSortData();
			// если в основном списке критериев поиска уже есть записи, то копируем их в форму сбора данных
			if( lvSSData.Items.Count > 0 ) {
				for( int i=0; i!=lvSSData.Items.Count; ++i ) {
					ListViewItem lvi = new ListViewItem( lvSSData.Items[i].Text );
								lvi.SubItems.Add( lvSSData.Items[i].SubItems[1].Text );
								lvi.SubItems.Add( lvSSData.Items[i].SubItems[2].Text );
								lvi.SubItems.Add( lvSSData.Items[i].SubItems[3].Text );
								lvi.SubItems.Add( lvSSData.Items[i].SubItems[4].Text );
								lvi.SubItems.Add( lvSSData.Items[i].SubItems[5].Text );
								lvi.SubItems.Add( lvSSData.Items[i].SubItems[6].Text );
								lvi.SubItems.Add( lvSSData.Items[i].SubItems[7].Text );
					ssdfrm.lvSSData.Items.Add( lvi );
				}
			}

			ssdfrm.ShowDialog();
			if( ssdfrm.IsOKClicked() ) {
				/* обрабатываем собранные данные */
				List<string> lsGenres = null; // временный список Жанров по конкретной Группе Жанров
				if( ssdfrm.lvSSData.Items.Count > 0 ) {
					// удаляем записи в списке, если они есть
					if( lvSSData.Items.Count > 0 ) {
						lvSSData.Items.Clear();
					}
					m_lSSQCList = new List<SelectedSortQueryCriteria>();
					string sLang, sLast, sFirst, sMiddle, sNick, sGGroup, sGenre, sSequence;
					for( int i=0; i!=ssdfrm.lvSSData.Items.Count; ++i ) {
						sLang	= ssdfrm.lvSSData.Items[i].Text;
						sGGroup	= ssdfrm.lvSSData.Items[i].SubItems[1].Text;
						sGenre	= ssdfrm.lvSSData.Items[i].SubItems[2].Text;
						sLast	= ssdfrm.lvSSData.Items[i].SubItems[3].Text;
						sFirst	= ssdfrm.lvSSData.Items[i].SubItems[4].Text;
						sMiddle	= ssdfrm.lvSSData.Items[i].SubItems[5].Text;
						sNick	= ssdfrm.lvSSData.Items[i].SubItems[6].Text;
						sSequence	= ssdfrm.lvSSData.Items[i].SubItems[7].Text;
						ListViewItem lvi = new ListViewItem( sLang );
									lvi.SubItems.Add( sGGroup );
									lvi.SubItems.Add( sGenre );
									lvi.SubItems.Add( sLast );
									lvi.SubItems.Add( sFirst );
									lvi.SubItems.Add( sMiddle );
									lvi.SubItems.Add( sNick );
									lvi.SubItems.Add( sSequence );
						// добавление записи в список
						lvSSData.Items.Add( lvi );
						/* заполняем список критериев поиска для Избранной Сортировки */
						// "вычленяем" язык книги
						if( sLang.Length!=0 ) {
							sLang = sLang.Substring( sLang.IndexOf( "(" )+1 );
							sLang = sLang.Remove( sLang.IndexOf( ")" ) );
						}
						// если есть Жанр, то "вычленяем" его из строки
						if( sGenre.Length!=0 ) {
							sGenre = sGenre.Substring( sGenre.IndexOf( "(" )+1 );
							sGenre = sGenre.Remove( sGenre.IndexOf( ")" ) );
						}
						// если есть Группа Жанров, то преобразуем ее в список "ее" Жанров
						if( sGGroup.Length!=0 ) {
							DataFM dfm = new DataFM();
							IFBGenres fb2g = null;
							if( dfm.GenresFB21Scheme ) {
								fb2g = new FB21Genres();
							} else {
								fb2g = new FB22Genres();
							}
							lsGenres = fb2g.GetFBGenresForGroup( sGGroup );
						}
						// формируем список критериев поиска в зависимости от наличия Групп Жанров
						if( lsGenres==null ) {
							m_lSSQCList.Add( new SelectedSortQueryCriteria(
									sLang,sGGroup,sGenre,sLast,sFirst,sMiddle,sNick,sSequence ) );
						} else {
							foreach( string sG in lsGenres ) {
								m_lSSQCList.Add( new SelectedSortQueryCriteria(
										sLang,"",sG,sLast,sFirst,sMiddle,sNick,sSequence ) );
							}
						}
					}
				}
			}
			
			ssdfrm.Dispose();
			#endregion
		}
		
		void BtnSSInsertTemplatesClick(object sender, EventArgs e)
		{
			// запуск диалога Вставки готовых шаблонов
			BasiclTemplates btfrm = new BasiclTemplates();
			btfrm.ShowDialog();
			if( btfrm.GetTemplateLine()!=null ) {
				txtBoxSSTemplatesFromLine.Text = btfrm.GetTemplateLine();
			}
			btfrm.Dispose();
		}
		
		void TsbtnSSOpenDirClick(object sender, EventArgs e)
		{
			// задание папки с fb2-файлами и архивами для сканирования (Избранная Сортировка)
			FilesWorker.FilesWorker.OpenDirDlg( tboxSSSourceDir, fbdScanDir, "Укажите папку для сканирования с fb2-файлами и архивами (Избранная Сортировка):" );
		}
		
		void TsbtnSSTargetDirClick(object sender, EventArgs e)
		{
			// задание папки-приемника для размешения отсортированных файлов (Избранная Сортировка)
			FilesWorker.FilesWorker.OpenDirDlg( tboxSSToDir, fbdScanDir, "Укажите папку-приемник для размешения отсортированных файлов (Избранная Сортировка):" );
		}
	
		void TsbtnSSSortFilesToClick(object sender, EventArgs e)
		{
			// Избранная Сортировка
			string sMessTitle = "SharpFBTools - Избранная Сортировка";
			// проверка на корректность данных папок источника и приемника файлов
			if( !IsFolderdataCorrect( tboxSSSourceDir, tboxSSToDir, sMessTitle ) ) {
				return;
			}
			// проверка на наличие критериев поиска для Избранной Сортировки
			if( lvSSData.Items.Count == 0 ) {
				MessageBox.Show( "Задайте хоть один критерий для Избранной Сортировки (кнопка \"Собрать данные для Избранной Сортировки\")!", sMessTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				btnSSGetData.Focus();
				return;
			}
			// проверка на наличие архиваторов
			if( !IsArchivatorsExist( sMessTitle ) ) {
				return;
			}
			// проверки на корректность шаблонных строк
			if( !IsLineTemplateCorrect( txtBoxSSTemplatesFromLine.Text.Trim(), sMessTitle ) ) {
				return;
			}
			SelectedSortFb2Files( tboxSSSourceDir.Text, tboxSSToDir.Text, txtBoxSSTemplatesFromLine.Text.Trim() );
		}
		#endregion
	}
}
