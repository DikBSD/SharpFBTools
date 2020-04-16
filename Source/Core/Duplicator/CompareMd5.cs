/*
 * User: Кузнецов Вадим (DikBSD)
 * Date: 14.08.2019
 * Time: 11:12
 * 
 * License: GPL 2.1
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;
using System.Security.Cryptography;
using System.IO;

using Core.Common;
using Core.FB2.FB2Parsers;

using FilesWorker = Core.Common.FilesWorker;
using SharpZipLibWorker = Core.Common.SharpZipLibWorker;

namespace Core.Duplicator
{
    /// <summary>
	/// Сравнение книг по Md5
	/// </summary>
    class CompareMd5
    {
        private readonly SharpZipLibWorker _sharpZipLib = new SharpZipLibWorker();
        private List<string> _nonOpenedFileList = new List<string>();
        private CompareCommon _compComm = new CompareCommon();

        /// <summary>
        /// Хеширование файлов в контексте значений Md5 книг:
        /// Абсолютно одинаковые книги (Md5)
        /// </summary>
        /// <param name="FilesList">Список файлов для сканирования</param>
        /// <param name="htFB2ForMd5">Хеш Таблица с книгами с одинаковыми значениями Md5</param>
        /// <returns>Признак непрерывности обработки файлов</returns>
        public bool FilesHashForMd5Parser(BackgroundWorker bw, DoWorkEventArgs e,
                                          Label StatusLabel, ProgressBar ProgressBar, string TempDir,
                                          List<string> FilesList, HashtableClass htFB2ForMd5)
        { 
            StatusLabel.Text += "Хэширование по Md5 ...\r";
            ProgressBar.Maximum = FilesList.Count;
            ProgressBar.Value = 0;

            List<string> FinishedFilesList = new List<string>();
            for (int i = 0; i != FilesList.Count; ++i) {
                if (FilesWorker.isFB2File(FilesList[i])) {
                    // заполнение хеш таблицы данными о fb2-книгах в контексте их md5
                    MakeFB2Md5HashTable(null, FilesList[i], ref htFB2ForMd5);
                    // обработанные файлы
                    FinishedFilesList.Add(FilesList[i]);
                } else {
                    if (FilesWorker.isFB2Archive(FilesList[i])) {
                        try {
                            if (_sharpZipLib.UnZipFB2Files(FilesList[i], TempDir) != -1) {
                                string[] files = Directory.GetFiles(TempDir);
                                if (files.Length > 0) {
                                    if (FilesWorker.isFB2File(files[0])) {
                                        // заполнение хеш таблицы данными о fb2-книгах в контексте их md5
                                        MakeFB2Md5HashTable(FilesList[i], files[0], ref htFB2ForMd5);
                                        // обработанные файлы
                                        FinishedFilesList.Add(FilesList[i]);
                                    }
                                }
                            }
                        } catch (Exception ex) {
                            Debug.DebugMessage(
                                FilesList[i], ex, "Дубликатор.CompareForm.FilesHashForMd5Parser(): Хеширование файлов в контексте Md5 книг."
                            );
                            // обработанные файлы
                            FinishedFilesList.Add(FilesList[i]);
                        }
                        FilesWorker.RemoveDir(TempDir);
                    }
                }
                bw.ReportProgress(i); // отобразим данные в контролах

                if (bw.CancellationPending) {
                    // удаление из списка всех файлов обработанные книги (файлы)
                    WorksWithBooks.removeFinishedFilesInFilesList(ref FilesList, ref FinishedFilesList);
                    e.Cancel = true;
                    return false;
                }
            }
            // удаление элементов таблицы, value (списки) которых состоят из 1-го элемента (это не копии)
            _compComm.removeNotCopiesEntryInHashTable(htFB2ForMd5);
            // удаление из списка всех файлов обработанные книги (файлы)
            WorksWithBooks.removeFinishedFilesInFilesList(ref FilesList, ref FinishedFilesList);

            return true;
        }

        /// <summary>
		/// Заполнение хеш таблицы данными о fb2-книгах в контексте их md5
		/// </summary>
		/// <param name="ZipPath">путь к zip-архиву. Если книга - не запакована в zip, то ZipPath = null</param>
		/// <param name="SrcPath">путь к fb2-файлу;</param>
		/// <param name="htFB2ForMd5">Хеш Таблица с книгами с одинаковыми значениями Md5</param>
		private void MakeFB2Md5HashTable(string ZipPath, string SrcPath, ref HashtableClass htFB2ForMd5)
        {
            string md5 = ComputeMD5Checksum(SrcPath);

            FictionBook fb2 = null;
            try {
                fb2 = new FictionBook(SrcPath);
            } catch (Exception ex) {
                Debug.DebugMessage(
                    SrcPath, ex, "Дубликатор.CompareForm.MakeFB2Md5HashTable(): Заполнение хеш таблицы данными о fb2-книгах в контексте их md5."
                );
                _nonOpenedFileList = _compComm.collectBadFB2(!string.IsNullOrEmpty(ZipPath) ? ZipPath : SrcPath);
                return;
            }

            string Encoding = fb2.getEncoding();
            if (string.IsNullOrWhiteSpace(Encoding))
                Encoding = "?";
            string ID = fb2.DIID;
            if (ID == null)
                return;

            if (ID.Trim().Length == 0)
                ID = "Тег <id> в этих книгах \"пустой\"";

            // данные о книге
            BookData fb2BookData = new BookData(
                fb2.TIBookTitle, fb2.TIAuthors, fb2.TIGenres, fb2.TILang, ID, fb2.DIVersion, fb2.DIAuthors, SrcPath, Encoding
            );
            if (ZipPath != null)
                fb2BookData.Path = ZipPath;

            if (!htFB2ForMd5.ContainsKey(md5)) {
                // такой книги в числе дублей еще нет
                FB2FilesDataInGroup fb2f = new FB2FilesDataInGroup(fb2BookData, md5);
                htFB2ForMd5.Add(md5, fb2f);
            } else {
                // такая книга в числе дублей уже есть
                FB2FilesDataInGroup fb2f = (FB2FilesDataInGroup)htFB2ForMd5[md5];
                fb2f.Add(fb2BookData);
                //htFB2ForMd5[md5] = fb2f; //ИЗБЫТОЧНЫЙ КОД
            }
        }

        /// <summary>
		/// Вычисление MD5 файла
		/// </summary>
        private string ComputeMD5Checksum(string path)
        {
            using (FileStream fs = System.IO.File.OpenRead(path))
            {
                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] fileData = new byte[fs.Length];
                fs.Read(fileData, 0, (int)fs.Length);
                byte[] checkSum = md5.ComputeHash(fileData);
                string result = BitConverter.ToString(checkSum).Replace("-", String.Empty);
                return result;
            }
        }

        #region Свойства класса
        /// <summary>
        /// Список неоткрываемых файлов
        /// </summary>
        public virtual List<string> NonOpenedFileList
        {
            get { return _nonOpenedFileList; }
            set { _nonOpenedFileList = value; }
        }
        #endregion

    }
}
