/*
 * User: Кузнецов Вадим (DikBSD)
 * Date: 15.08.2019
 * Time: 14:08
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
using Core.FB2.Description.TitleInfo;

using FilesWorker = Core.Common.FilesWorker;
using SharpZipLibWorker = Core.Common.SharpZipLibWorker;

namespace Core.Duplicator
{
    /// <summary>
    /// Одинаковые Названия Книги (могут быть найдены и разные книги разных Авторов, но с одинаковым Названием)
    /// </summary>
    class CompareBookTitle
    {
        private readonly SharpZipLibWorker _sharpZipLib = new SharpZipLibWorker();
        private List<string> _nonOpenedFileList = new List<string>();
        private CompareCommon _compComm = new CompareCommon();

        /// <summary>
        /// Хеширование файлов в контексте Авторов и Названия книг:
        /// 2. Название Книги (могут быть найдены и разные книги разных Авторов, но с одинаковым Названием)
        /// 3. Автор(ы) и Название Книги (одна и та же книга, сделанная разными людьми - разные Id, но Автор и Название - одинаковые)
        /// </summary>
        /// <param name="FilesList">Список файлов для сканирования</param>
        /// <param name="htFB2ForBT">Хеш Таблица с книгами с одинаковыми Названиями</param>
        /// <returns>Признак непрерывности обработки файлов</returns>
        public bool FilesHashForBTParser(BackgroundWorker bw, DoWorkEventArgs e,
                                         Label StatusLabel, ProgressBar ProgressBar, string TempDir,
                                         List<string> FilesList, HashtableClass htFB2ForBT)
        {
            StatusLabel.Text += "Хэширование по Названиям книг...\r";
            ProgressBar.Maximum = FilesList.Count;
            ProgressBar.Value = 0;

            List<string> FinishedFilesList = new List<string>();
            for (int i = 0; i != FilesList.Count; ++i) {
                if (FilesWorker.isFB2File(FilesList[i])) {
                    // заполнение хеш таблицы данными о fb2-книгах в контексте их Авторов и Названия
                    MakeFB2BTHashTable(null, FilesList[i], htFB2ForBT);
                    // обработанные файлы
                    FinishedFilesList.Add(FilesList[i]);
                } else {
                    if (FilesWorker.isFB2Archive(FilesList[i])) {
                        try {
                            if (_sharpZipLib.UnZipFB2Files(FilesList[i], TempDir) != -1) {
                                string[] files = Directory.GetFiles(TempDir);
                                if (files.Length > 0) {
                                    if (FilesWorker.isFB2File(files[0])) {
                                        // заполнение хеш таблицы данными о fb2-книгах в контексте их Авторов и Названия
                                        MakeFB2BTHashTable(FilesList[i], files[0], htFB2ForBT);
                                        // обработанные файлы
                                        FinishedFilesList.Add(FilesList[i]);
                                    }
                                }
                            }
                        }
                        catch (Exception ex) {
                            Debug.DebugMessage(
                                FilesList[i], ex, "Дубликатор.CompareForm.FilesHashForBTParser(): Хеширование файлов в контексте Авторов и Названия книг."
                            );
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
            _compComm.removeNotCopiesEntryInHashTable(htFB2ForBT);
            // удаление из списка всех файлов обработанные книги (файлы)
            WorksWithBooks.removeFinishedFilesInFilesList(ref FilesList, ref FinishedFilesList);
            return true;
        }

        /// <summary>
		/// Заполнение хеш таблицы данными о fb2-книгах в контексте их Названия
		/// </summary>
		/// <param name="ZipPath">путь к zip-архиву. Если книга - не запакована в zip, то ZipPath = null</param>
		/// <param name="SrcPath">путь к fb2-файлу;</param>
		/// <param name="htFB2ForBT">Хеш Таблица с книгами с одинаковыми Названиями</param>
		private void MakeFB2BTHashTable(string ZipPath, string SrcPath, HashtableClass htFB2ForBT)
        {
            FictionBook fb2 = null;
            try {
                fb2 = new FictionBook(SrcPath);
            }
            catch (Exception ex) {
                Debug.DebugMessage(
                    SrcPath, ex, "Дубликатор.CompareForm.MakeFB2BTHashTable(): Заполнение хеш таблицы данными о fb2-книгах в контексте их Названия."
                );
                _nonOpenedFileList = _compComm.collectBadFB2(!string.IsNullOrEmpty(ZipPath) ? ZipPath : SrcPath);
                return;
            }

            string Encoding = fb2.getEncoding();
            if (string.IsNullOrWhiteSpace(Encoding))
                Encoding = "?";

            BookTitle bookTitle = fb2.TIBookTitle;
            string BT = "<Название книги отсутствует>";
            if (bookTitle != null && !string.IsNullOrWhiteSpace(bookTitle.Value))
                BT = bookTitle.Value.Trim();

            // данные о книге
            BookData fb2BookData = new BookData(
                bookTitle, fb2.TIAuthors, fb2.TIGenres, fb2.TILang, fb2.DIID, fb2.DIVersion, fb2.DIAuthors, SrcPath, Encoding
            );

            if (ZipPath != null)
                fb2BookData.Path = ZipPath;

            if (!htFB2ForBT.ContainsKey(BT))             {
                // такой книги в числе дублей еще нет
                FB2FilesDataInGroup fb2f = new FB2FilesDataInGroup(fb2BookData, BT);
                htFB2ForBT.Add(BT, fb2f);
            } else {
                // такая книга в числе дублей уже есть
                FB2FilesDataInGroup fb2f = (FB2FilesDataInGroup)htFB2ForBT[BT];
                fb2f.Add(fb2BookData);
                //htFB2ForBT[sBT] = fb2f; //ИЗБЫТОЧНЫЙ КОД
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
