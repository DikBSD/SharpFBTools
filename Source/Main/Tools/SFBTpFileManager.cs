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

using fB2Parser = FB2.FB2Parsers.FB2Parser;

namespace SharpFBTools.Tools
{
	/// <summary>
	/// Description of SFBTpFileManager.
	/// </summary>
	public partial class SFBTpFileManager : UserControl
	{
		private FB2Parser.FB2Validator fv2V = new FB2Parser.FB2Validator();
		
		public ListView GetSettingsInfoListView()
		{
			return lvSettings;
		}
		
		public SFBTpFileManager()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			cboxTemplatesPrepared.SelectedIndex = 0;
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
					IncStatus( 4 );
					break;
				case ".zip":
					FilesWorker.Archiver.unzip( dfm.A7zaPath, sFromFile, sTempDir );
					IncStatus( 3 );
					break;
				case ".7z":
					FilesWorker.Archiver.unzip( dfm.A7zaPath, sFromFile, sTempDir );
					IncStatus( 5 );
					break;
				case ".bz2":
					FilesWorker.Archiver.unzip( dfm.A7zaPath, sFromFile, sTempDir );
					IncStatus( 6 );
					break;
				case ".gz":
					FilesWorker.Archiver.unzip( dfm.A7zaPath, sFromFile, sTempDir );
					IncStatus( 7 );
					break;
				case ".tar":
					FilesWorker.Archiver.unzip( dfm.A7zaPath, sFromFile, sTempDir );
					IncStatus( 8 );
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
			IncStatus( 11 ); // всего создано
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
			}
			if( !bBad ) {
				IncStatus( 11 ); // всего создано
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
			IncStatus( 14 ); // "битые" архивы - не открылись
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
				IncStatus( 2 ); // исходные fb2-файлы
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
						IncStatus( 9 ); // Исходные fb2-файлы из архивов
					}
				}  else {
					// пропускаем не fb2-файлы и архивы
					IncStatus( 10 ); // другие файлы
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
				IncStatus( 2 ); // исходные fb2-файлы
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
						IncStatus( 9 ); // Исходные fb2-файлы из архивов
					}
				}  else {
					// пропускаем не fb2-файлы и архивы
					IncStatus( 10 ); // другие файлы
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
				IncStatus( 2 ); // исходные fb2-файлы
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
						IncStatus( 9 ); // Исходные fb2-файлы из архивов
					}
				} else {
					// пропускаем не fb2-файлы и архивы
					IncStatus( 10 ); // другие файлы
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
				IncStatus( 2 ); // исходные fb2-файлы
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
						IncStatus( 9 ); // Исходные fb2-файлы из архивов
					}
				}  else {
					// пропускаем не fb2-файлы и архивы
					IncStatus( 10 ); // другие файлы
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
					IncStatus( 13 ); // не валидные fb2-файлы
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
			IncStatus( 12 ); // нечитаемые fb2-файлы или архивы
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
			
			// проверка на наличие архиваторов
			string s7zPath	= Settings.Settings.Read7zaPath();
			string sRarPath	= Settings.Settings.ReadRarPath();
			if( Settings.SettingsFM.ReadArchiveTypeText().ToLower()=="rar" && sRarPath.Trim().Length==0 ) {
				MessageBox.Show( "В Настройках выбрана rar-архивация отсортированных файлов.\nПри этом не указана папка с установленным консольным Rar-архиватором!\nУкажите путь к нему в Настройках.\nРабота остановлена!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			} else {
				// проверка на наличие архиваторов
				if( !File.Exists( sRarPath ) ) {
					MessageBox.Show( "В Настройках выбрана rar-архивация отсортированных файлов.\nПри этом не найден файл консольного Rar-архиватора "+sRarPath+"!\nУкажите путь к нему в Настройках.\nРабота остановлена!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return;
				}
			}
			if( s7zPath.Trim().Length==0 ) {
				MessageBox.Show( "В Настройках не указана папка с установленным консольным 7Zip-архиватором!\nУкажите путь к нему в Настройках.\nРабота остановлена!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			} else {
				if( !File.Exists( s7zPath ) ) {
					MessageBox.Show( "Не найден файл Zip-архиватора \""+s7zPath+"\"!\nУкажите путь к нему в Настройках.\nРабота остановлена!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
					return;
				}
			}
			// проверки на корректность папок источника и приемника
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
			// проверки на корректность шаблонных строк
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
				tsslblProgress.Text = "Создание списка папок:";
				lDirList = FilesWorker.FilesWorker.DirsParser( sSource, lvFilesCount, false );
			}
			// сортированный список всех файлов
			tsslblProgress.Text = "Создание списка файлов:";
			ssProgress.Refresh();
			List<string> lFilesList = FilesWorker.FilesWorker.AllFilesParser( lDirList, ssProgress, lvFilesCount,
			                                                                 tsProgressBar, false );

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
			// запуск диалога Вставки готовый шаблонов
			BasiclTemplates btfrm = new BasiclTemplates();
			btfrm.ShowDialog();
		}
		#endregion
	}
}
