/*
 * User: Кузнецов Вадим (DikBSD)
 * Date: 09.09.2019
 * Time: 15:35
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
    /// Одинаковые Авторы Книги из fb2 файлов
    /// </summary>
    class CompareAuthorFromFB2
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
        /// <returns>Признак непрерывности обработки файлов</returns>
        public bool FilesHashForAuthorParser(ref BackgroundWorker bw, ref DoWorkEventArgs e,
                                                Label StatusLabel, ProgressBar ProgressBar, string TempDir,
                                                ref List<string> FilesList, ref HashtableClass htFB2ForAuthorFIO, bool WithMiddleName)
        {
            StatusLabel.Text += "Хэширование в контексте Авторов с одинаковой Фамилией...\r";
            ProgressBar.Maximum = FilesList.Count;
            ProgressBar.Value = 0;

            List<string> FinishedFilesList = new List<string>();
            for (int i = 0; i != FilesList.Count; ++i) {
                if (FilesWorker.isFB2File(FilesList[i])) {
                    // заполнение хеш таблицы данными о fb2-книгах в контексте Авторов с одинаковой Фамилией и инициалами
                    MakeFB2AuthorHashTable(null, FilesList[i], ref htFB2ForAuthorFIO, WithMiddleName);
                    // обработанные файлы
                    FinishedFilesList.Add(FilesList[i]);
                } else {
                    if (FilesWorker.isFB2Archive(FilesList[i])) {
                        try {
                            if (_sharpZipLib.UnZipFB2Files(FilesList[i], TempDir) != -1) {
                                string[] files = Directory.GetFiles(TempDir);
                                if (files.Length > 0) {
                                    if (FilesWorker.isFB2File(files[0])) {
                                        // заполнение хеш таблицы данными о fb2-книгах в контексте одинаковых Авторов
                                        MakeFB2AuthorHashTable(FilesList[i], files[0], ref htFB2ForAuthorFIO, WithMiddleName);
                                        // обработанные файлы
                                        FinishedFilesList.Add(FilesList[i]);
                                    }
                                }
                            }
                        } catch (Exception ex) {
                            Debug.DebugMessage(
                                FilesList[i], ex, "Дубликатор.CompareForm.FilesHashForAuthorParser(): Хеширование файлов в контексте одинаковых Авторов Книг."
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
            // удаление элементов таблицы, value (списки) которых состоят из 1-го элемента
            _compComm.removeNotCopiesEntryInHashTable(htFB2ForAuthorFIO);
            // удаление из списка всех файлов обработанные книги (файлы)
            WorksWithBooks.removeFinishedFilesInFilesList(ref FilesList, ref FinishedFilesList);

            return true;
        }

        /// <summary>
        /// Заполнение хеш таблицы данными о fb2-книгах в контексте одинаковых Авторов
        /// </summary>
        /// <param name="ZipPath">путь к zip-архиву. Если книга - не запакована в zip, то ZipPath = null</param>
        /// <param name="SrcPath">путь к fb2-файлу</param>
        /// <param name="htFB2ForAuthorFIO">Хеш Таблица с книгами одинаковых Авторов</param>
        /// <param name="WithMiddleName">Учитывать ли отчество Авторов (true) или нет (false) при поиске</param>
        private void MakeFB2AuthorHashTable(string ZipPath, string SrcPath, ref HashtableClass htFB2ForAuthorFIO, bool WithMiddleName)
        {
            FictionBook fb2 = null;
            try {
                fb2 = new FictionBook(SrcPath);
            } catch (Exception ex) {
                Debug.DebugMessage(
                    SrcPath, ex, "Дубликатор.CompareForm.MakeFB2AuthorHashTable(): Заполнение хеш таблицы данными о fb2-книгах в контексте одинаковых Авторов."
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
                        sAuthor += " " + a.FirstName.Value;
                    if (WithMiddleName) {
                        if (a.MiddleName != null && !string.IsNullOrEmpty(a.MiddleName.Value))
                            sAuthor += " " + a.MiddleName.Value;
                    }
                    if (a.NickName != null && !string.IsNullOrWhiteSpace(a.NickName.Value))
                        sAuthor += a.NickName.Value;
                    sAuthor = sAuthor.Trim();
                    // Заполнение хеш таблицы данными о fb2-книгах в контексте одинаковых Авторов
                    FB2AuthorSetHashTable(fb2, ZipPath, SrcPath, Encoding, sAuthor, ref htFB2ForAuthorFIO);
                }
            } else {
                // Заполнение хеш таблицы данными о fb2-книгах в контексте одинаковых Авторов
                FB2AuthorSetHashTable(fb2, ZipPath, SrcPath, Encoding, sAuthor, ref htFB2ForAuthorFIO);
            }
        }

        /// <summary>
		/// Заполнение хеш таблицы данными о fb2-книгах в контексте одинаковых Авторов
		/// </summary>
		/// <param name="fb2">объект класса FictionBook</param>
		/// <param name="ZipPath">путь к zip-архиву. Если книга - не запакована в zip, то ZipPath = null</param>
		/// <param name="SrcPath">путь к fb2-файлу</param>
		/// <param name="Encoding">кодировка текщего файла в fb2</param>
		/// <param name="sAuthor">Фамилия и 1-я буква Имени текущего автора</param>
		/// <param name="htFB2ForAuthor">Хеш Таблица с книгами одинаковых Авторов</param>
		private void FB2AuthorSetHashTable(FictionBook fb2, string ZipPath, string SrcPath, string Encoding,
                                           string sAuthor, ref HashtableClass htFB2ForAuthor)
        {
            // данные о книге
            BookData fb2BookData = new BookData(
                fb2.TIBookTitle, fb2.TIAuthors, fb2.TIGenres, fb2.TILang, fb2.DIID, fb2.DIVersion, fb2.DIAuthors, SrcPath, Encoding
            );

            if (ZipPath != null)
                fb2BookData.Path = ZipPath;

            if (!htFB2ForAuthor.ContainsKey(sAuthor)) {
                // этого Автора sAuthor в Группе еще нет
                FB2FilesDataInGroup fb2f = new FB2FilesDataInGroup(fb2BookData, sAuthor);
                fb2f.Group = sAuthor;
                htFB2ForAuthor.Add(sAuthor, fb2f);
            } else {
                // этот Автор sAuthor в Группе уже есть
                FB2FilesDataInGroup fb2f = (FB2FilesDataInGroup)htFB2ForAuthor[sAuthor];
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
