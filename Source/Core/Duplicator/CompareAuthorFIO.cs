/*
 * User: Кузнецов Вадим (DikBSD)
 * Date: 15.08.2019
 * Time: 14:26
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
using Core.FB2.Description.Common;

using FilesWorker = Core.Common.FilesWorker;
using SharpZipLibWorker = Core.Common.SharpZipLibWorker;

namespace Core.Duplicator
{
    /// <summary>
    /// Авторы с одинаковыми Фамилиями и инициалами (могут быть найдены и разные книги разных Авторов, но с одинаковыми Фамилиями и инициалами)
    /// </summary>
    class CompareAuthorFIO
    {
        private readonly SharpZipLibWorker _sharpZipLib = new SharpZipLibWorker();
        private List<string> _nonOpenedFileList = new List<string>();
        private CompareCommon _compComm = new CompareCommon();

        /// <summary>
        /// Хеширование по Авторам с одинаковыми Фамилиями и инициалами
        /// </summary
        /// <param name="bw">Экземплар фонового обработчика класса BackgroundWorker</param>
        /// <param name="e">Экземпляр класса DoWorkEventArgs</param>
        /// <param name="FilesList">Список файлов для сканирования</param>
        /// <param name="htFB2ForAuthorFIO">Хеш Таблица для сбора одинаковых Авторов книг</param>
        /// <param name="WithMiddleName">Учитывать ли отчество Авторов (true) или нет (false) при поиске</param>
        public void FilesHashForAuthorFIOParser(ref BackgroundWorker bw, ref DoWorkEventArgs e,
                                                Label StatusLabel, ProgressBar ProgressBar, string TempDir,
                                                ref List<string> FilesList, ref Hashtable htFB2ForAuthorFIO, bool WithMiddleName)
        {
            StatusLabel.Text += "Хэширование fb2-файлов в контексте Авторов с одинаковой Фамилией и инициалами...\r";
            ProgressBar.Maximum = FilesList.Count;
            ProgressBar.Value = 0;

            List<string> FinishedFilesList = new List<string>();
            for (int i = 0; i != FilesList.Count; ++i) {
                if (bw.CancellationPending) {
                    // удаление из списка всех файлов обработанные книги (файлы)
                    WorksWithBooks.removeFinishedFilesInFilesList(ref FilesList, ref FinishedFilesList);
                    e.Cancel = true;
                    return;
                }
                if (FilesWorker.isFB2File(FilesList[i])) {
                    // заполнение хеш таблицы данными о fb2-книгах в контексте Авторов с одинаковой Фамилией и инициалами
                    MakeFB2AuthorFIOHashTable(null, FilesList[i], ref htFB2ForAuthorFIO, WithMiddleName);
                    // обработанные файлы
                    FinishedFilesList.Add(FilesList[i]);
                } else {
                    if (FilesWorker.isFB2Archive(FilesList[i])) {
                        try {
                            if (_sharpZipLib.UnZipFB2Files(FilesList[i], TempDir) != -1) {
                                string[] files = Directory.GetFiles(TempDir);
                                if (files.Length > 0) {
                                    if (FilesWorker.isFB2File(files[0])) {
                                        // заполнение хеш таблицы данными о fb2-книгах в контексте Авторов с одинаковой Фамилией и инициалами
                                        MakeFB2AuthorFIOHashTable(FilesList[i], files[0], ref htFB2ForAuthorFIO, WithMiddleName);
                                        // обработанные файлы
                                        FinishedFilesList.Add(FilesList[i]);
                                    }
                                }
                            }
                        } catch (Exception ex) {
                            Debug.DebugMessage(
                                FilesList[i], ex, "Дубликатор.CompareForm.FilesHashForAuthorFIOParser(): Хеширование файлов в контексте Авторов с одинаковой Фамилией и инициалами."
                            );
                        }
                        FilesWorker.RemoveDir(TempDir);
                    }
                }
                bw.ReportProgress(i); // отобразим данные в контролах
            }
            // удаление элементов таблицы, value (списки) которых состоят из 1-го элемента
            _compComm.removeNotCopiesEntryInHashTable(ref htFB2ForAuthorFIO);
            // удаление из списка всех файлов обработанные книги (файлы)
            WorksWithBooks.removeFinishedFilesInFilesList(ref FilesList, ref FinishedFilesList);
        }

        /// <summary>
        /// Заполнение хеш таблицы данными о fb2-книгах в контексте Авторов с одинаковой Фамилией и инициалами
        /// </summary>
        /// <param name="ZipPath">путь к zip-архиву. Если книга - не запакована в zip, то ZipPath = null</param>
        /// <param name="SrcPath">путь к fb2-файлу</param>
        /// <param name="htFB2ForAuthorFIO">Хеш Таблица с книгами одинаковых Авторов</param>
        /// <param name="WithMiddleName">Учитывать ли отчество Авторов (true) или нет (false) при поиске</param>
        private void MakeFB2AuthorFIOHashTable(string ZipPath, string SrcPath, ref Hashtable htFB2ForAuthorFIO, bool WithMiddleName)
        {
            FictionBook fb2 = null;
            try {
                fb2 = new FictionBook(SrcPath);
            } catch (Exception ex) {
                Debug.DebugMessage(
                    SrcPath, ex, "Дубликатор.CompareForm.MakeFB2AuthorFIOHashTable(): Заполнение хеш таблицы данными о fb2-книгах в контексте Авторов с одинаковой Фамилией и инициалами."
                );
                _nonOpenedFileList = _compComm.collectBadFB2(!string.IsNullOrEmpty(ZipPath) ? ZipPath : SrcPath);
                return;
            }

            string Encoding = fb2.getEncoding();
            if (string.IsNullOrWhiteSpace(Encoding))
                Encoding = "?";

            IList<Author> AuthorsList = fb2.TIAuthors;
            string sAuthor = "<Автор книги отсутствует>";
            if (AuthorsList != null) {
                foreach (Author a in AuthorsList) {
                    if (a.LastName != null && !string.IsNullOrEmpty(a.LastName.Value))
                        sAuthor = a.LastName.Value;
                    if (a.FirstName != null && !string.IsNullOrEmpty(a.FirstName.Value))
                        sAuthor += " " + a.FirstName.Value.Substring(0, 1);
                    if (WithMiddleName) {
                        if (a.MiddleName != null && !string.IsNullOrEmpty(a.MiddleName.Value))
                            sAuthor += " " + a.MiddleName.Value.Substring(0, 1);
                    }
                    if (a.NickName != null && !string.IsNullOrWhiteSpace(a.NickName.Value))
                        sAuthor += a.NickName.Value;
                    sAuthor = sAuthor.Trim();
                    // Заполнение хеш таблицы данными о fb2-книгах в контексте Авторов
                    FB2AuthorFIOSetHashTable(fb2, ZipPath, SrcPath, Encoding, sAuthor, ref htFB2ForAuthorFIO);
                }
            } else {
                // Заполнение хеш таблицы данными о fb2-книгах в контексте Авторов
                FB2AuthorFIOSetHashTable(fb2, ZipPath, SrcPath, Encoding, sAuthor, ref htFB2ForAuthorFIO);
            }
        }

        /// <summary>
		/// Заполнение хеш таблицы данными о fb2-книгах в контексте Авторов
		/// </summary>
		/// <param name="fb2">объект класса FictionBook</param>
		/// <param name="ZipPath">путь к zip-архиву. Если книга - не запакована в zip, то ZipPath = null</param>
		/// <param name="SrcPath">путь к fb2-файлу</param>
		/// <param name="Encoding">кодировка текщего файла в fb2</param>
		/// <param name="sAuthor">Фамилия и 1-я буква Имени текущего автора</param>
		/// <param name="htFB2ForAuthorFIO">Хеш Таблица с книгами одинаковых Авторов</param>
		private void FB2AuthorFIOSetHashTable(FictionBook fb2, string ZipPath, string SrcPath, string Encoding,
                                              string sAuthor, ref Hashtable htFB2ForAuthorFIO)
        {
            // данные о книге
            BookData fb2BookData = new BookData(
                fb2.TIBookTitle, fb2.TIAuthors, fb2.TIGenres, fb2.TILang, fb2.DIID, fb2.DIVersion, fb2.DIAuthors, SrcPath, Encoding
            );

            if (ZipPath != null)
                fb2BookData.Path = ZipPath;

            if (!htFB2ForAuthorFIO.ContainsKey(sAuthor)) {
                // этого Автора sAuthor в Группе еще нет
                FB2FilesDataInGroup fb2f = new FB2FilesDataInGroup(fb2BookData, sAuthor);
                fb2f.Group = sAuthor;
                htFB2ForAuthorFIO.Add(sAuthor, fb2f);
            } else {
                // этот Автор sAuthor в Группе уже есть
                FB2FilesDataInGroup fb2f = (FB2FilesDataInGroup)htFB2ForAuthorFIO[sAuthor];
                fb2f.Add(fb2BookData);
                //htFB2ForBT[sAuthor] = fb2f; //ИЗБЫТОЧНЫЙ КОД
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
