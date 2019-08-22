/*
 * User: Кузнецов Вадим (DikBSD)
 * Date: 15.08.2019
 * Time: 13:43
 * 
 * License: GPL 2.1
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;
using System.IO;

using Core.Common;
using Core.FB2.FB2Parsers;

using FilesWorker = Core.Common.FilesWorker;
using SharpZipLibWorker = Core.Common.SharpZipLibWorker;

namespace Core.Duplicator
{
    /// <summary>
    /// Одинаковый Id Книги (копии и/или разные версии правки одной и той же книги)
    /// </summary>
    class CompareBookID
    {
        private readonly SharpZipLibWorker _sharpZipLib = new SharpZipLibWorker();
        private List<string> _nonOpenedFileList = new List<string>();
        private CompareCommon _compComm = new CompareCommon();

        /// <summary>
        /// Хеширование файлов в контексте Id книг:
        /// Одинаковый Id Книги (копии и/или разные версии правки одной и той же книги)
        /// </summary>
        /// <param name="FilesList">Список файлов для сканированияl</param>
        /// <param name="htFB2ForID">Хеш Таблица с книгами с одинаковыми ID</param>
        public void FilesHashForIDParser(ref BackgroundWorker bw, ref DoWorkEventArgs e,
                                         Label StatusLabel, ProgressBar ProgressBar, string TempDir,
                                         ref List<string> FilesList, ref Hashtable htFB2ForID)
        {
            StatusLabel.Text += "Хэширование fb2-файлов в контексте Id книг...\r";
            ProgressBar.Maximum = FilesList.Count;
            ProgressBar.Value = 0;

            List<string> FinishedFilesList = new List<string>();
            for (int i = 0; i != FilesList.Count; ++i) {
                if (bw.CancellationPending) {
                    // удаление из списка всех файлов обработанных книг
                    WorksWithBooks.removeFinishedFilesInFilesList(ref FilesList, ref FinishedFilesList);
                    e.Cancel = true;
                    return;
                }
                if (FilesWorker.isFB2File(FilesList[i])) {
                    // заполнение хеш таблицы данными о fb2-книгах в контексте их ID
                    MakeFB2IDHashTable(null, FilesList[i], ref htFB2ForID);
                    // обработанные файлы
                    FinishedFilesList.Add(FilesList[i]);
                } else {
                    if (FilesWorker.isFB2Archive(FilesList[i])) {
                        try {
                            if (_sharpZipLib.UnZipFB2Files(FilesList[i], TempDir) != -1) {
                                string[] files = Directory.GetFiles(TempDir);
                                if (files.Length > 0) {
                                    if (FilesWorker.isFB2File(files[0])) {
                                        // заполнение хеш таблицы данными о fb2-книгах в контексте их ID
                                        MakeFB2IDHashTable(FilesList[i], files[0], ref htFB2ForID);
                                        // обработанные файлы
                                        FinishedFilesList.Add(FilesList[i]);
                                    }
                                }
                            }
                        }
                        catch (Exception ex) {
                            Debug.DebugMessage(
                                FilesList[i], ex, "Дубликатор.CompareForm.FilesHashForIDParser(): Хеширование файлов в контексте Id книг."
                            );
                        }
                        FilesWorker.RemoveDir(TempDir);
                    }
                }
                bw.ReportProgress(i); // отобразим данные в контролах
            }
            // удаление элементов таблицы, value (списки) которых состоят из 1-го элемента (это не копии)
            _compComm.removeNotCopiesEntryInHashTable(ref htFB2ForID);
            // удаление из списка всех файлов обработанных книг
            WorksWithBooks.removeFinishedFilesInFilesList(ref FilesList, ref FinishedFilesList);
        }

        /// <summary>
		/// Заполнение хеш таблицы данными о fb2-книгах в контексте их ID
		/// </summary>
		/// <param name="ZipPath">путь к zip-архиву. Если книга - не запакована в zip, то ZipPath = null</param>
		/// <param name="SrcPath">путь к fb2-файлу;</param>
		/// <param name="htFB2ForID">Хеш Таблица с книгами с одинаковыми ID</param>
		private void MakeFB2IDHashTable(string ZipPath, string SrcPath, ref Hashtable htFB2ForID)
        {
            FictionBook fb2 = null;
            try {
                fb2 = new FictionBook(SrcPath);
            }
            catch (Exception ex) {
                Debug.DebugMessage(
                    SrcPath, ex, "Дубликатор.CompareForm.MakeFB2IDHashTable(): Заполнение хеш таблицы данными о fb2-книгах в контексте их ID."
                );
                _nonOpenedFileList = _compComm.collectBadFB2(!string.IsNullOrEmpty(ZipPath) ? ZipPath : SrcPath);
                return;
            }

            string Encoding = fb2.getEncoding();
            if (string.IsNullOrWhiteSpace(Encoding))
                Encoding = "?";
            string ID = fb2.DIID;
            if (string.IsNullOrEmpty(ID) || string.IsNullOrWhiteSpace(ID))
                ID = _compComm.NoOrEmptyBookIDString;

            // данные о книге
            BookData fb2BookData = new BookData(
                fb2.TIBookTitle, fb2.TIAuthors, fb2.TIGenres, fb2.TILang, ID, fb2.DIVersion, fb2.DIAuthors, SrcPath, Encoding
            );
            if (ZipPath != null)
                fb2BookData.Path = ZipPath;

            if (!htFB2ForID.ContainsKey(ID)) {
                // такой книги в числе дублей еще нет
                FB2FilesDataInGroup fb2f = new FB2FilesDataInGroup(fb2BookData, ID);
                htFB2ForID.Add(ID, fb2f);
            } else {
                // такая книга в числе дублей уже есть
                FB2FilesDataInGroup fb2f = (FB2FilesDataInGroup)htFB2ForID[ID];
                fb2f.Add(fb2BookData);
                //htFB2ForID[sID] = fb2f; //ИЗБЫТОЧНЫЙ КОД
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
