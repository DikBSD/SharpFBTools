/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 03.10.2013
 * Time: 8:14
 * 
 * License: GPL 2.1
 */
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;

using System.Windows.Forms;

using FilesWorker = Core.Common.FilesWorker;

using ICSharpCode.SharpZipLib.Zip;

namespace Core.Common
{
	/// <summary>
	/// упаковка / распаковка файлов и папок
	/// </summary>
	public class SharpZipLibWorker
	{
		public SharpZipLibWorker()
		{
		}
		
		#region Открык методы класса

		/* ======================================================================================== */
		/* 									Упаковка файлов	в архив									*/
		/* ======================================================================================== */
		#region Упаковка файлов	в архив
		/// <summary>
		/// Упаковка файла в zip
		/// </summary>
		/// <param name="SourceFile">путь к исходному файлу</param>
		/// <param name="DestinationZipFile">путь к создаваемому zip-архиву</param>
		/// <param name="CompressionMethod">BZip2, Deflated, Stored, WinZipAES</param>
		/// <param name="CompressLevel">0-9, 9 being the highest level of compression</param>
		/// <param name="BufferSize">буфер, обычно 4096</param>
		public void ZipFile(string SourceFile, string DestinationZipFile, int CompressLevel,
		                    ICSharpCode.SharpZipLib.Zip.CompressionMethod CompressionMethod, int BufferSize)
		{
			FileStream fileStreamIn = new FileStream(SourceFile, FileMode.Open, FileAccess.Read);
			FileStream fileStreamOut = new FileStream(DestinationZipFile, FileMode.Create, FileAccess.Write);
			ZipOutputStream zipOutStream = new ZipOutputStream(fileStreamOut);
			zipOutStream.SetLevel(CompressLevel);

			ZipEntry zipEntry = new ZipEntry(Path.GetFileName(SourceFile));
			zipEntry.CompressionMethod = CompressionMethod;
			zipOutStream.PutNextEntry(zipEntry);
			
			CopyStream(fileStreamIn, zipOutStream, BufferSize);

			zipOutStream.Finish();
			zipOutStream.Close();
			fileStreamOut.Close();
			fileStreamIn.Close();
		}
		
		/// <summary>
		/// Упаковка списка файлов в zip
		/// </summary>
		/// <param name="FilesForZipList">список всех файлов для упаковки</param>
		/// <param name="DestinationZipFile">путь к результирующему zip-архиву</param>
		/// <param name="CompressLevel">0-9, 9 being the highest level of compression</param>
		/// <param name="Password">пароль архива. Если его нет, то задаем null</param>
		/// <param name="BufferSize">буфер, обычно 4096</param>
		public void ZipFiles(ref List<string> FilesForZipList, string DestinationZipFile, int CompressLevel, string Password, /*bool IsVisible,*/ int BufferSize)
		{
			//TODO полные пути/папки (пока все файлы помещает в корень)
			if (FilesForZipList.Count > 0) {
				FileStream outputFileStream = new FileStream(DestinationZipFile, FileMode.Create);
				ZipOutputStream zipOutStream = new ZipOutputStream(outputFileStream);
				zipOutStream.SetLevel(CompressLevel);
				bool IsCrypted = false;
				if ( ! string.IsNullOrEmpty( Password ) ) {
					zipOutStream.Password = Password;
					IsCrypted = true;
				}

				foreach (string file in FilesForZipList) {
					Stream inputStream = new FileStream(file, FileMode.Open);
					ZipEntry zipEntry = new ZipEntry(Path.GetFileName(file));

					//zipEntry.IsVisible = IsVisible;
					zipEntry.IsCrypted = IsCrypted;
					zipEntry.CompressionMethod = ICSharpCode.SharpZipLib.Zip.CompressionMethod.Deflated;
					zipOutStream.PutNextEntry(zipEntry);
					CopyStream(inputStream, zipOutStream, BufferSize);
					inputStream.Close();
					zipOutStream.CloseEntry();
				}
				zipOutStream.Finish();
				zipOutStream.Close();
			}
		}
		
		
		/*		/// <summary>
		/// Упаковка папки
		/// </summary>
		/// <param name="SourceFolder">путь к исходной папке</param>
		/// <param name="DestinationZipFile">путь к результирующему zip-архиву</param>
		/// <param name="CompressLevel">0-9, 9 being the highest level of compression</param>
		/// <param name="Password">пароль архива. Если его нет, то задаем null</param>
		/// <param name="BufferSize">буфер, обычно 4096</param>
		 */
//		public void ZipFolder(string SourceFolder, string DestinationZipFile, int CompressLevel, string Password, int BufferSize)
//		{
//			List<string> DirList = new List<string>();
//			DirsParser( SourceFolder, ref DirList, false );
//
//			FileStream outputFileStream = new FileStream(DestinationZipFile, FileMode.Create);
//			ZipOutputStream zipOutStream = new ZipOutputStream(outputFileStream);
//			zipOutStream.SetLevel(CompressLevel);
//
//			bool IsCrypted = false;
//			if (Password != null && Password.Length > 0) {
//				zipOutStream.Password = Password;
//				IsCrypted = true;
//			}
//
//			foreach( string dir in DirList ) {
//				string[] FilesForZipList = Directory.GetFiles( dir );
//				if (FilesForZipList.Length == 0) {
//					// файлов нет. Создаем папку dir
//
//				}
//
//				foreach (string file in FilesForZipList) {
//					Stream inputStream = new FileStream(file, FileMode.Open);
//					ZipEntry zipEntry = new ZipEntry(Path.GetFileName(file));
//
//					//zipEntry.IsVisible = IsVisible;
//					zipEntry.IsCrypted = IsCrypted;
//					zipEntry.CompressionMethod = ICSharpCode.SharpZipLib.Zip.CompressionMethod.Deflated;
//					zipOutStream.PutNextEntry(zipEntry);
//					CopyStream(inputStream, zipOutStream, BufferSize);
//					inputStream.Close();
//					zipOutStream.CloseEntry();
//				}
//				zipOutStream.Finish();
//				zipOutStream.Close();
//			}
//		}
		
		//        public void AddFilesToZip(ref List<string> FilesForZipList, string DestinationZipFile, string Password, bool IsVisible, int BufferSize)
//		{
//			if (FilesForZipList.Count > 0){
		//        		FileStream outputFileStream = new FileStream(DestinationZipFile, FileMode.Create);
		//        		ZipOutputStream zipOutStream = new ZipOutputStream(outputFileStream);
		//        		ZipFile zipFile = null;
//				// there may be files to copy from annother archive
//				zipFile = new ZipFile(DestinationZipFile);
//
//				bool IsCrypted = false;
//				if (Password != null && Password.Length > 0) {
//					zipFile.Password = Password;
//					// encrypt the zip file, if Password != null
//					zipOutStream.Password = Password;
//					IsCrypted = true;
//				}
//
//				foreach (string file in FilesForZipList) {
//					ZipEntry zipEntry = viewItem.Tag as ZipEntry;
//					Stream inputStream;
//					if (zipEntry == null) {
//						inputStream = new FileStream(file, FileMode.Open);
//						zipEntry = new ZipEntry(Path.GetFileName(file));
//					} else {
//						inputStream = zipFile.GetInputStream(zipEntry);
//					}
//					zipEntry.IsVisible = IsVisible;
//					zipEntry.IsCrypted = IsCrypted;
//					zipEntry.CompressionMethod = ICSharpCode.SharpZipLib.Zip.CompressionMethod.Deflated;
//					zipOutStream.PutNextEntry(zipEntry);
//					CopyStream(inputStream, zipOutStream, BufferSize);
//					inputStream.Close();
//					zipOutStream.CloseEntry();
//				}
//
//				if (zipFile != null) {
//					zipFile.Close();
//				}
//
//				zipOutStream.Finish();
//				zipOutStream.Close();
//			}
//		}
		#endregion

		/* ======================================================================================== */
		/* 									Распаковка файлов										*/
		/* ======================================================================================== */
		#region Распаковка файлов
		
		/// <summary>
		/// Распаковка конкретного файла FileName из zip архива ZipPath
		/// Возвращает: Строку string с данными распакованного файла
		/// </summary>
		/// <param name="ZipPath">Путь к исходному zip-файлу</param>
		/// <param name="FileName">Имя файла, который нужно распаковать</param>
		public string UnZipFileToString( string ZipPath, string FileName ) {
			MemoryStream ms = new MemoryStream();
			ICSharpCode.SharpZipLib.Zip.ZipInputStream zis = new ICSharpCode.SharpZipLib.Zip.ZipInputStream(
				new FileStream(ZipPath, FileMode.Open)
			);
			ICSharpCode.SharpZipLib.Zip.ZipEntry entry = null;
			while ( (entry = zis.GetNextEntry()) != null) {
				if (entry.Name.ToLower() == FileName) {
					int size = 2048;
					byte[] data = new byte[2048];
					while (true) {
						size = zis.Read(data, 0, data.Length);
						if (size > 0) {
							ms.Write(data, 0, size);
						} else {
							break;
						}
					}
				}
			}
			if ( ms.Length > 0 ) {
				byte[] bdata = ms.ToArray();
				return Encoding.GetEncoding( getEncoding( bdata ) ).GetString(bdata);
			}
			return string.Empty;
		}
		
		/// <summary>
		/// Распаковка 1-го файла из zip архива ZipPath
		/// Возвращает: Строку string с данными распакованного файла, если была распаковка. Иначе - пустую строку
		/// </summary>
		/// <param name="ZipPath">Путь к исходному zip-файлу</param>
		public string UnZipFB2FileToString( string ZipPath ) {
			if ( FilesWorker.isFB2Archive( ZipPath )  ) {
				MemoryStream ms = new MemoryStream();
				ICSharpCode.SharpZipLib.Zip.ZipInputStream zis = new ICSharpCode.SharpZipLib.Zip.ZipInputStream(
					new FileStream(ZipPath, FileMode.Open)
				);
				if ( zis.GetNextEntry() != null ) {
					int size = 2048;
					byte[] data = new byte[2048];
					while (true) {
						size = zis.Read( data, 0, data.Length );
						if ( size > 0 ) {
							ms.Write(data, 0, size);
						} else {
							break;
						}
					}
					byte[] bdata = ms.ToArray();
					return Encoding.GetEncoding( getEncoding( bdata ) ).GetString(bdata);
				}
			}
			return string.Empty;
		}
		
		/// <summary>
		/// Распаковка файла из zip архива: с учетом вложенных папок, относительно исходной, и наличия копий в папке распаковки
		/// Наличие нескольких файлов и/или папок не проверяется (будет ошибка)
		/// Возвращает: false, если архив 'битый' (не смог открыть).
		/// </summary>
		/// <param name="SourceZipFile">Путь к исходному zip-файлу</param>
		/// <param name="DestinationDir">папка для распакованного фвйла</param>
		/// <param name="IsFileExistsMode">Режим для суффикса: 0 - замена; 1 - новый номер; 2 - дата</param>
		/// <param name="BufferSize">буфер, обычно 4096</param>
		/// <returns>true - если архив удалось распаковать; false - если не удалось распаковать</returns>
		public bool UnZipFile(string SourceZipFile, string DestinationDir, int IsFileExistsMode, int BufferSize)
		{
			FileStream fileStreamIn = new FileStream(SourceZipFile, FileMode.Open, FileAccess.Read);
			ZipInputStream zipInStream = null;
			try {
				zipInStream = new ZipInputStream(fileStreamIn);
			} catch ( Exception ex ) {
				Debug.DebugMessage(
					SourceZipFile, ex, "SharpZipLibWorker.UnZipFile()."
				);
				return false;
			}
			
			if ( !Directory.Exists( DestinationDir ) )
				Directory.CreateDirectory( DestinationDir );
			
			ZipEntry zipEntry = zipInStream.GetNextEntry();
			string path = Path.Combine(DestinationDir, zipEntry.Name.Replace('/', '\\'));
			string dir = Path.GetDirectoryName(path);
			if ( !Directory.Exists( dir ) )
				Directory.CreateDirectory( dir );
			if ( File.Exists( path ) )
				path = FilesWorker.createFilePathWithSufix(path, IsFileExistsMode);
			
			try {
				// на случай, если в имени файла есть нечитаемые символы
				using ( FileStream fileStreamOut = new FileStream(path, FileMode.Create, FileAccess.Write) ) {
					CopyStream(zipInStream, fileStreamOut, BufferSize);
					zipInStream.Close();
					fileStreamOut.Close();
					fileStreamIn.Close();
				}
				if ( File.Exists(path) ) {
					ZipFile zipFile = new ZipFile(SourceZipFile);
					if (zipFile[0].IsFile) {
						File.SetCreationTime(path, File.GetCreationTime( SourceZipFile ));
						File.SetLastAccessTime(path, File.GetLastAccessTime( SourceZipFile ));
						File.SetLastWriteTime(path, File.GetLastWriteTime( SourceZipFile ));
					}
				}
			} catch ( Exception ex ) {
				Debug.DebugMessage(
					SourceZipFile, ex, "SharpZipLibWorker.UnZipFile()."
				);
				fileStreamIn.Close();
				return false;
			}
			return true;
		}
		
		/// <summary>
		/// Распаковка файла из zip архива: с учетом вложенных папок, относительно исходной, и наличия копий в папке распаковки
		/// Не воспринимает символы « и » в имени архива - вылетает
		/// </summary>
		/// <param name="SourceZipFile">Путь к исходному zip-файлу</param>
		/// <param name="DestinationDir">папка для распаковываемых фвйлов</param>
		/// <param name="IsFileExistsMode">Режим для суффикса: 0 - замена; 1 - новый номер; 2 - дата</param>
		/// <param name="FB2Only">true - распаковка только fb2 файлов</param>
		/// <param name="Password">пароль архива. Если его нет, то задаем null</param>
		/// <param name="BufferSize">буфер, обычно 4096</param>
		/// <returns>Число распакованных файлов, или -1, если архив 'битый' (не смог открыть)</returns>
		public long UnZipFiles(string SourceZipFile, string DestinationDir,
		                       int IsFileExistsMode, bool FB2Only, string Password, int BufferSize)
		{
			long count = 0;
			if (File.Exists(SourceZipFile)) {
				ZipFile zipFile = null;
				try {
					zipFile = new ZipFile(SourceZipFile);
				} catch {
					return -1;
				}
				
				if (Password != null)
					zipFile.Password = Password;

				if( !Directory.Exists( DestinationDir ) )
					Directory.CreateDirectory( DestinationDir );
				
				for (int i = 0; i != zipFile.Count; ++i) {
					if (zipFile[i] != null) {
						if (zipFile[i].IsFile) {
							string zipFileName = zipFile[i].Name.Replace('<', '«').Replace('>', '»').Replace(':', '_');
							try {
								if ( FB2Only && Path.GetExtension(zipFileName.ToLower()) != ".fb2" )
									continue;
							} catch ( ArgumentException ex ) {
								Debug.DebugMessage(
									SourceZipFile, ex, "SharpZipLibWorker.UnZipFiles(). Исключение ArgumentException."
								);
								continue;
							}
							
							Stream inputStream = zipFile.GetInputStream(zipFile[i]);
							string path = Path.Combine(DestinationDir, zipFileName.Replace('/', '\\'));
							string dir = Path.GetDirectoryName(path);
							if ( !Directory.Exists( dir ) )
								Directory.CreateDirectory( dir );
							if ( File.Exists( path ) )
								path = FilesWorker.createFilePathWithSufix(path, IsFileExistsMode);

							try {
								// на случай, если в имени файла есть нечитаемые символы
								using ( FileStream fileStream = new FileStream(path, FileMode.Create) ) {
									CopyStream(inputStream, fileStream, BufferSize);
									fileStream.Close();
									inputStream.Close();
								}
								if( File.Exists(path) ) {
									DateTime dtFile = zipFile[i].DateTime;
									File.SetCreationTime(path, dtFile);
									File.SetLastAccessTime(path, dtFile);
									File.SetLastWriteTime(path, dtFile);
								}
								++count;
							} catch ( Exception ex ) {
								Debug.DebugMessage(
									SourceZipFile, ex, "SharpZipLibWorker.UnZipFiles(). Исключение Exception."
								);
								inputStream.Close();
							}
						}
					}
				}
				zipFile.Close();
			}
			return count;
		}
		
		/// <summary>
		/// Распаковка только fb2 файлов из zip архива: с учетом вложенных папок, относительно исходной, и наличия копий в папке распаковки
		/// Не воспринимает символы « и » в имени архива - вылетает
		/// </summary>
		/// <param name="SourceZipPath">Путь к исходному zip-файлу</param>
		/// <param name="DestinationDir">Папка для распаковываемых фвйлов</param>
		/// <param name="IsFileExistsMode">Режим для суффикса: 0 - замена; 1 - новый номер; 2 - дата</param>
		/// <returns>Число распакованных файлов, или -1, если архив 'битый' (не смог открыть)</returns>
		public long UnZipFB2Files(string SourceZipPath, string DestinationDir, int IsFileExistsMode = 1)
		{
			long count = 0;
			bool fileExists = false;
			try {
				// на случай, если в имени файла есть нечитаемые символы
				fileExists = File.Exists(SourceZipPath);
			} catch ( Exception ex ) {
				Debug.DebugMessage(
					SourceZipPath, ex, "SharpZipLibWorker.UnZipFiles(). Не возможно открыть архив (в имени файла есть нечитаемые символы)."
				);
				return -1;
			}
			
			if ( fileExists ) {
				ZipFile zipFile = null;
				try {
					zipFile = new ZipFile(SourceZipPath);
				} catch ( Exception ex ) {
					Debug.DebugMessage(
						SourceZipPath, ex, "SharpZipLibWorker.UnZipFiles(). Не возможно открыть архив."
					);
					return -1;
				}

				if ( !Directory.Exists( DestinationDir ) )
					Directory.CreateDirectory( DestinationDir );
				
				for ( int i = 0; i != zipFile.Count; ++i ) {
					if ( zipFile[i] != null ) {
						if ( zipFile[i].IsFile ) {
							// только корректные символы для имен файлов
							string zipFileName = StringProcessing.OnlyCorrectSymbolsForString(
								zipFile[i].Name.Replace('<', '«').Replace('>', '»').Replace(':', '_')
								);
							try {
								if ( Path.GetExtension(zipFileName.ToLower()) != ".fb2" )
									continue;
							} catch ( ArgumentException ex ) {
								Debug.DebugMessage(
									SourceZipPath, ex, "SharpZipLibWorker.UnZipFiles(). Исключение ArgumentException."
								);
								continue;
							}

							try {
								Stream inputStream = zipFile.GetInputStream(zipFile[i]);
								string path = Path.Combine(DestinationDir, zipFileName.Replace('/', '\\'));
								string dir = Path.GetDirectoryName(path);
								if (!Directory.Exists(dir))
									Directory.CreateDirectory(dir);
								if (File.Exists(path))
									path = FilesWorker.createFilePathWithSufix(path, IsFileExistsMode);


								//using (FileStream fileStream = new FileStream(path, FileMode.Create)) {
								//    inputStream.CopyTo(fileStream);
								//    inputStream.Close();
								//    fileStream.Close();
								//}
								//if (File.Exists(path)) {
								//    DateTime dtFile = zipFile[i].DateTime;
								//    File.SetCreationTime(path, dtFile);
								//    File.SetLastAccessTime(path, dtFile);
								//    File.SetLastWriteTime(path, dtFile);
								//}
								// на случай, если в имени файла есть нечитаемые символы
								using (FileStream fileStream = new FileStream(path, FileMode.Create)) {
									CopyStream(inputStream, fileStream, 4096);
									fileStream.Close();
									inputStream.Close();
								}
								if (File.Exists(path)) {
									DateTime dtFile = zipFile[i].DateTime;
									File.SetCreationTime(path, dtFile);
									File.SetLastAccessTime(path, dtFile);
									File.SetLastWriteTime(path, dtFile);
								}
								++count;
							}
							catch (Exception ex) {
								Debug.DebugMessage(
									SourceZipPath, ex, "SharpZipLibWorker.UnZipFiles(). Исключение Exception."
								);
								//inputStream.Close();
							}
						}
					}
				}
				zipFile.Close();
			}
			return count;
		}
		
		/// <summary>
		/// Распаковка файла из zip архива: с учетом вложенных папок, относительно исходной, и наличия копий в папке распаковки
		/// Возвращает: false, если архив 'битый' (не смог открыть).
		/// </summary>
		/// <param name="SourceZipFile">Путь к исходному zip-файлу</param>
		/// <param name="FileFromZip">Файл для распаковки</param>
		/// <param name="DestinationDir">папка для распаковываемого фвйла</param>
		/// <param name="IsFileExistsMode">Режим для суффикса: 0 - замена; 1 - новый номер; 2 - дата</param>
		/// <param name="Password">пароль архива. Если его нет, то задаем null</param>
		/// <param name="BufferSize">буфер, обычно 4096</param>
		public bool UnZipSelectedFile(string SourceZipFile, string FileFromZip, string DestinationDir,
		                              int IsFileExistsMode, string Password, int BufferSize)
		{
			bool b_fileExists = false;
			try {
				// на случай, если в имени файла есть нечитаемые символы
				b_fileExists = File.Exists(SourceZipFile);
			} catch ( Exception ex ) {
				Debug.DebugMessage(
					SourceZipFile, ex, "SharpZipLibWorker.UnZipSelectedFile(). Не возможно открыть архив (в имени файла есть нечитаемые символы)."
				);
				return false;
			}
			
			if ( b_fileExists ) {
				ZipFile zipFile = null;
				try {
					zipFile = new ZipFile(SourceZipFile);
				} catch ( Exception ex ) {
					Debug.DebugMessage(
						SourceZipFile, ex, "SharpZipLibWorker.UnZipSelectedFile(). Не возможно открыть архив."
					);
					return false;
				}
				
				if (Password != null)
					zipFile.Password = Password;

				string path	= string.Empty;
				int fileExists = zipFile.FindEntry(FileFromZip, false);
				if ( fileExists != -1) {
					if (zipFile[fileExists].IsFile) {
						if ( !Directory.Exists( DestinationDir ) )
							Directory.CreateDirectory( DestinationDir );
						Stream inputStream = zipFile.GetInputStream(zipFile[fileExists]);
						path = Path.Combine(DestinationDir, FileFromZip.Replace('/', '\\'));
						string dir = Path.GetDirectoryName(path);
						if ( !Directory.Exists( dir ) )
							Directory.CreateDirectory( dir );
						if ( File.Exists( path ) )
							path = FilesWorker.createFilePathWithSufix(path, IsFileExistsMode);

						try {
							// на случай, если в имени файла есть нечитаемые символы
							using ( FileStream fileStream = new FileStream(path, FileMode.Create) ) {
								CopyStream(inputStream, fileStream, BufferSize);
								fileStream.Close();
								inputStream.Close();
							}
							if ( File.Exists(path) ) {
								DateTime dtFile = zipFile[fileExists].DateTime;
								File.SetCreationTime(path, dtFile);
								File.SetLastAccessTime(path, dtFile);
								File.SetLastWriteTime(path, dtFile);
							}
						} catch ( Exception ex ) {
							Debug.DebugMessage(
								SourceZipFile, ex, "SharpZipLibWorker.UnZipSelectedFile(). Не возможно открыть архив (скорее всего в имени файла есть нечитаемые символы)."
							);
							inputStream.Close();
						}
					}
				}
				zipFile.Close();
				return true;
			}
			return false;
		}
		
		/// <summary>
		/// Распаковка выбранных файлов из zip архива: с учетом вложенных папок, относительно исходной, и наличия копий в папке распаковки
		/// Возвращает: Число распакованных файлов, или -1, если архив 'битый' (не смог открыть).
		/// </summary>
		/// <param name="SourceZipFile">Путь к исходному zip-файлу</param>
		/// <param name="FilesFromZipList">Путь к списку из string для списка распаковываемых файлов из zip</param>
		/// <param name="DestinationDir">папка для распаковываемых фвйлов</param>
		/// <param name="IsFileExistsMode">Режим для суффикса: 0 - замена; 1 - новый номер; 2 - дата</param>
		/// <param name="FB2Only">true - распаковка только fb2 файлов</param>
		/// <param name="Password">пароль архива. Если его нет, то задаем null</param>
		/// <param name="BufferSize">буфер, обычно 4096</param>
		public long UnZipSelectedFiles(string SourceZipFile, ref List<string> FilesFromZipList, string DestinationDir,
		                               int IsFileExistsMode, bool FB2Only, string Password, int BufferSize)
		{
			long count = 0;
			bool b_fileExists = false;
			try {
				// на случай, если в имени файла есть нечитаемые символы
				b_fileExists = File.Exists(SourceZipFile);
			} catch ( Exception ex ) {
				Debug.DebugMessage(
					SourceZipFile, ex, "SharpZipLibWorker.UnZipSelectedFiles(). Не возможно открыть архив (в имени файла есть нечитаемые символы)."
				);
				return -1;
			}
			
			if ( b_fileExists ) {
				ZipFile zipFile = null;
				try {
					zipFile = new ZipFile(SourceZipFile);
				} catch ( Exception ex ) {
					Debug.DebugMessage(
						SourceZipFile, ex, "SharpZipLibWorker.UnZipSelectedFiles(). Не возможно открыть архив."
					);
					return -1;
				}
				
				if (Password != null)
					zipFile.Password = Password;

				if( !Directory.Exists( DestinationDir ) )
					Directory.CreateDirectory( DestinationDir );
				
				string path	= string.Empty;
				string dir	= string.Empty;
				foreach (string file in FilesFromZipList) {
					int fileExists = zipFile.FindEntry(file, false);
					if ( fileExists != -1) {
						if (zipFile[fileExists].IsFile) {
							try {
								if ( FB2Only && Path.GetExtension(zipFile[fileExists].Name.ToLower()) != ".fb2" )
									continue;
							} catch ( ArgumentException ex ) {
								Debug.DebugMessage(
									SourceZipFile, ex, "SharpZipLibWorker.UnZipSelectedFiles().  Исключение ArgumentException."
								);
								continue;
							}
							
							Stream inputStream = zipFile.GetInputStream(zipFile[fileExists]);
							path = Path.Combine(DestinationDir, file.Replace('/', '\\'));
							dir = Path.GetDirectoryName(path);
							if ( !Directory.Exists( dir ) )
								Directory.CreateDirectory( dir );
							if ( File.Exists( path ) )
								path = FilesWorker.createFilePathWithSufix(path, IsFileExistsMode);

							try {
								// на случай, если в имени файла есть нечитаемые символы
								using ( FileStream fileStream = new FileStream(path, FileMode.Create) ) {
									CopyStream(inputStream, fileStream, BufferSize);
									fileStream.Close();
									inputStream.Close();
								}
								if ( File.Exists(path) ) {
									DateTime dtFile = zipFile[fileExists].DateTime;
									File.SetCreationTime(path, dtFile);
									File.SetLastAccessTime(path, dtFile);
									File.SetLastWriteTime(path, dtFile);
								}
								++count;
							} catch ( Exception ex ) {
								Debug.DebugMessage(
									SourceZipFile, ex, "SharpZipLibWorker.UnZipSelectedFiles(). Не возможно открыть архив (скорее всего в имени файла есть нечитаемые символы). Исключение Exception."
								);
								inputStream.Close();
							}
						}
					}
				}
				zipFile.Close();
			}
			return count;
		}

		#endregion
		
		/* ======================================================================================== */
		/* 								Удаление файлов их архива									*/
		/* ======================================================================================== */
		#region Удаление файлов их архива
		/// <summary>
		/// Удаление файла из zip архива
		/// Возвращает null, если архив 'битый' (не смог открыть), или файл в нем не удалось удалить
		/// </summary>
		/// <param name="SourceZipFile">Путь к исходному zip-файлу</param>
		/// <param name="FileFromZip">Файл для удаления</param>
		/// <param name="Password">пароль архива. Если его нет, то задаем null</param>
		public bool DeleteSelectedFile(string SourceZipFile, string FileFromZip, string Password)
		{
			// список файлов в архиве
			bool ret = false;
			if (File.Exists(SourceZipFile)) {
				ZipFile zipFile = null;
				try {
					zipFile = new ZipFile(SourceZipFile);
				} catch ( Exception ex ) {
					Debug.DebugMessage(
						SourceZipFile, ex, "SharpZipLibWorker.DeleteSelectedFile(). Не возможно открыть архив."
					);
					return false;
				}
				if (Password != null)
					zipFile.Password = Password;

				int fileExists = zipFile.FindEntry(FileFromZip, false);
				if ( fileExists != -1) {
					if (zipFile[fileExists].IsFile) {
						zipFile.BeginUpdate();
						ret = zipFile.Delete(FileFromZip);
						zipFile.CommitUpdate();
					}
				}
				zipFile.Close();
			}
			return ret;
		}
		
		/// <summary>
		/// Удаление файлов из zip архива
		/// Возвращает false, если архив 'битый' (не смог открыть), или не все файлы в нем удалось удалить
		/// </summary>
		/// <param name="SourceZipFile">Путь к исходному zip-файлу</param>
		/// <param name="FilesFromZipList">Файл для удаления</param>
		/// <param name="Password">пароль архива. Если его нет, то задаем null</param>
		public bool DeleteSelectedFiles(string SourceZipFile, ref List<string> FilesFromZipList, string Password)
		{
			// список файлов в архиве
			bool ret = false;
			if (File.Exists(SourceZipFile)) {
				ZipFile zipFile = null;
				try {
					zipFile = new ZipFile(SourceZipFile);
				} catch ( Exception ex ) {
					Debug.DebugMessage(
						SourceZipFile, ex, "SharpZipLibWorker.DeleteSelectedFiles(). Не возможно открыть архив."
					);
					return false;
				}
				if (Password != null)
					zipFile.Password = Password;

				zipFile.BeginUpdate();
				foreach (string file in FilesFromZipList) {
					int fileExists = zipFile.FindEntry(file, false);
					if ( fileExists != -1) {
						if (zipFile[fileExists].IsFile)
							ret &= zipFile.Delete(file);
					}
				}
				zipFile.CommitUpdate();
				zipFile.Close();
			}
			return ret;
		}
		#endregion
		
		/* ======================================================================================== */
		/* 											Разное											*/
		/* ======================================================================================== */
		#region Разное
		/// <summary>
		/// Список файлов в zip
		/// Возвращает null, если архив 'битый' (не смог открыть), или в нем нет fb2-файлов при заданном FB2Only=true
		/// </summary>
		/// <param name="SourceZipFile">Путь к исходному zip-файлу</param>
		/// <param name="ToSort">true - сортировать список;</param>
		///	<param name="FB2Only">true - список только fb2-файлов</param>
		public List<string> FilesListFromZip( string SourceZipFile, bool ToSort, bool FB2Only  )
		{
			List<string> FilesFromZipList = null;
			if (SourceZipFile.Length > 0) {
				ZipEntry zipEntry;
				ZipFile zipFile = null;
				try {
					zipFile = new ZipFile(SourceZipFile);
				} catch ( Exception ex ) {
					Debug.DebugMessage(
						SourceZipFile, ex, "SharpZipLibWorker.FilesListFromZip(). Не возможно открыть архив."
					);
					return null;
				}
				FilesFromZipList = new List<string>();
				int nFB2 = 0;
				for (int i = 0; i != zipFile.Count; ++i) {
					zipEntry = zipFile[i];
					if ( FB2Only ) {
						if ( Path.GetExtension( Path.GetFileName( zipEntry.Name ) ).ToLower()==".fb2" ) {
							FilesFromZipList.Add(zipEntry.Name);
							++nFB2;
						}
					} else {
						FilesFromZipList.Add(zipEntry.Name);
					}
				}
				zipFile.Close();
				
				if ( FB2Only && nFB2 == 0 )
					return null;
				if ( ToSort ) {
					FilesFromZipList.Sort();
				}
			}
			return FilesFromZipList;
		}
		#endregion
		
		#endregion
		
		#region Закрытые вспомогательные методы класса
		private void CopyStream(Stream source, Stream destination, int BufferSize )
		{
			byte[] buffer = new byte[BufferSize];
			int countBytesRead;
			while ( (countBytesRead = source.Read(buffer, 0, buffer.Length) ) > 0 )
				destination.Write(buffer, 0, countBytesRead);
		}
		
		// определение кодировки распакованного fb2 файла
		private string getEncoding( byte[] bdata ) {
			string FB2Encoding = "UTF-8";
			string str = Encoding.GetEncoding("utf-8").GetString(bdata);
			str = str.Substring( 0, str.Length > 100 ? 100 : str.Length );
			Match match = Regex.Match( str, "(?<=encoding=\").+?(?=\")", RegexOptions.IgnoreCase);
			if ( match.Success )
				FB2Encoding = match.Value;
			if ( FB2Encoding.ToLower() == "wutf-8" || FB2Encoding.ToLower() == "utf8" )
				FB2Encoding = "utf-8";
			return FB2Encoding;
		}
		
		// создание списка всех вложенных папок в заданной папке
		// параметры:	StartDir - папка для сканирования;
		//				AllDirsList - заполняемый список папок в папке сканирования и ее подпапках
		//				DoSort = true - сортировать созданный список папок
		// возвращает: число всех файлов в папке для сканирования и ее подпапках
//		public int DirsParser( string StartDir, ref List<string> AllDirsList, bool DoSort ) {
//			int AllFilesCount = 0;
//			// рабочий список папок - по нему парсим вложенные папки и из него удаляем отработанные
//			List<string> WorkDirList = new List<string>();
//			// начальное заполнение списков
//			AllFilesCount = Directory.GetFiles( StartDir ).Length;
//			AllFilesCount += DirListMaker( StartDir, ref WorkDirList );
//			AllDirsList.Add( StartDir );
//			AllDirsList.AddRange( WorkDirList );
//			while( WorkDirList.Count != 0 ) {
//				// перебор папок в указанной папке
//				int WorkListCount = WorkDirList.Count;
//				for( int i=0; i!=WorkListCount; ++i ) {
//					// l - список найденных папок в указанной папке sWD
//					List<string> l = new List<string>();
//					AllFilesCount += DirListMaker( WorkDirList[i], ref l );
//					// заносим найденные папки в рабочий и полный список папок
//					WorkDirList.AddRange( l );
//					AllDirsList.AddRange( l );
//				}
//				// удаляем из рабочего списка обработанные папки
//				WorkDirList.RemoveRange( 0, WorkListCount );
//			}
//			if( DoSort ) {
//				AllDirsList.Sort();
//			}
//			return AllFilesCount;
//		}
		
		// создание списка подпапок в заданной папке
		// параметры:	StartDir - папка для сканирования;
		//				lDirList - заполняемый список папок в текущей папке
		// возвращает: число файлов в текущем каталоге
//		private int DirListMaker( string StartDir, ref List<string> lDirList ) {
//			int nFilesCount = 0;
//			// папки в текущей папке
//			try {
//				string[] dirs = Directory.GetDirectories( StartDir );
//				foreach( string dir in dirs ) {
//					try {
//						nFilesCount += Directory.GetFiles( dir ).Length;
//						lDirList.Add( dir );
//					} catch { continue; }
//				}
//			} catch { lDirList.Remove( StartDir ); }
//			return nFilesCount;
//		}
		#endregion
	}
}
