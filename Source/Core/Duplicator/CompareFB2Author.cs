/*
 * User: Кузнецов Вадим (DikBSD)
 * Date: 22.08.2019
 * Time: 09:01
 * 
 * License: GPL 2.1
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Core.Common;

namespace Core.Duplicator
{
    /// <summary>
    /// Одинаковые Название Книги, Автор(ы) и Автор fb2 файла (одна и та же книга, сделанная разными людьми)
    /// </summary>
    class CompareFB2Author
    {
        private CompareCommon _compComm = new CompareCommon();

        /// <summary>
        /// Хэширование по одинаковым Авторам fb2 файлов
        /// в пределах сгенерированных групп книг по одинаковым Названиям и Авторам книг
        /// </summary
        /// <param name="bw">Экземплар фонового обработчика класса BackgroundWorker</param>
        /// <param name="e">Экземпляр класса DoWorkEventArgs</param>
        /// <returns>Признак непрерывности обработки файлов</returns>
        public bool FilesHashForFB2AuthorsParser(BackgroundWorker bw, DoWorkEventArgs e,
                                                 Label StatusLabel, ProgressBar ProgressBar,
                                                 HashtableClass htBookTitleAuthors, HashtableClass htWorkingBook,
                                                 bool WithMiddleName)
        {
            StatusLabel.Text += "Хэширование по Авторам fb2-файлов...\r";
            ProgressBar.Maximum = htBookTitleAuthors.Count;
            ProgressBar.Value = 0;
            // генерация списка ключей хеш-таблицы (для удаления обработанного элемента таблицы)
            List<string> keyList = _compComm.makeSortedKeysForGroups(htBookTitleAuthors);
            // группировка книг по одинаковым Авторам fb2 файлов в пределах сгенерированных Групп книг одинаковых Авторов и по одинаковым Названиям
            int i = 0;
            foreach (string key in keyList) {
                // разбивка на группы для одинакового Авторам fb2 файла по Названию и по Авторам
                Hashtable AuthorsBookTitleFB2Authors = FindDupForAuthorsBookTitleFB2Authors((FB2FilesDataInGroup)htBookTitleAuthors[key], WithMiddleName);
                foreach (FB2FilesDataInGroup fb2List in AuthorsBookTitleFB2Authors.Values) {
                    if (!htBookTitleAuthors.ContainsKey(fb2List.Group))
                        htWorkingBook.Add(fb2List.Group, fb2List);
                    else {
                        FB2FilesDataInGroup fb2ListInGroup = (FB2FilesDataInGroup)htBookTitleAuthors[fb2List.Group];
                        fb2ListInGroup.AddRange(fb2List);
                    }
                }
                // удаление обработанной группы книг, сгруппированных по одинаковому названию
                htBookTitleAuthors.Remove(key);
                bw.ReportProgress(++i);

                if (bw.CancellationPending) {
                    e.Cancel = true;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Cоздание групп копий по Авторам fb2  файла, относительно найденного Названия и Автора книги
        /// </summary
        /// <param name="fb2Group">Экземпляр класса для хранения информации по одинаковым книгам в одной группе</param>
        /// <param name="WithMiddleName">Учитывать ли отчество Авторов (true) или нет (false) при поиске</param>
        private Hashtable FindDupForAuthorsBookTitleFB2Authors(FB2FilesDataInGroup fb2Group, bool WithMiddleName)
        {
            // в fb2Group.Group - название группы (название книги у всех книг одинаковое, а пути - разные )
            // внутри fb2Group в BookData - данные на каждую книгу группы
            Hashtable ht = new Hashtable(new FB2CultureComparer());
            // 2 итератора для перебора всех книг группы. 1-й - только на текущий элемент группы, 2-й - скользящий на все последующие. т.е. iter2 = iter1+1
            for (int iter1 = 0; iter1 != fb2Group.Count; ++iter1) {
                BookData bd1 = fb2Group[iter1]; // текущая книга
                FB2FilesDataInGroup fb2NewGroup = new FB2FilesDataInGroup();
                // перебор всех книг в группе, за исключением текущей
                for (int iter2 = iter1 + 1; iter2 != fb2Group.Count; ++iter2) {
                    // сравнение текущей книги со всеми последующими
                    BookData bd2 = fb2Group[iter2];
                    if (bd1.isSameBook(bd2, WithMiddleName, true)) {
                        if (!fb2NewGroup.isBookExists(bd2.Path))
                            fb2NewGroup.Add(bd2);
                    }
                }
                if (fb2NewGroup.Count >= 1) {
                    // только для копий, а не для единичных книг
                    fb2NewGroup.Group = fb2Group.Group + " [ " + fb2NewGroup.makeAuthorsString(WithMiddleName, true) + " ]";
                    fb2NewGroup.Insert(0, bd1);
                    if (!ht.ContainsKey(fb2NewGroup.Group))
                        ht.Add(fb2NewGroup.Group, fb2NewGroup);
                }
            }
            return ht;
        }

    }
}
