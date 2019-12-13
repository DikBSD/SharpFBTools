/*
 * User: Кузнецов Вадим (DikBSD)
 * Date: 16.08.2019
 * Time: 14:43
 * 
 * License: GPL 2.1
 */
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;

using Core.Common;

namespace Core.Duplicator
{
    /// <summary>
    /// Одинаковые Название Книги и Автор(ы) (одна и та же книга, сделанная разными людьми - разные Id, но Автор и Название - одинаковые)
    /// </summary>
    class CompareAuthorBookTitle
    {
        private CompareCommon _compComm = new CompareCommon();

        /// <summary>
        /// Хэширование по одинаковым Авторам в пределах сгенерированных групп книг по одинаковым названиям
        /// </summary
        /// <param name="bw">Экземплар фонового обработчика класса BackgroundWorker</param>
        /// <param name="e">Экземпляр класса DoWorkEventArgs</param>
        /// <param name="htFB2ForBT">Заполненная хеш-таблица списками книг по критерию одинакового Названия книг</param>
        /// <param name="htBookTitleAuthors">Заполняемая хеш-таблица списками книг по критерию ( Название книги (Авторы) )</param>
        /// <param name="WithMiddleName">Учитывать ли отчество Авторов (true) или нет (false) при поиске</param>
        /// <returns>Признак непрерывности обработки файлов</returns>
        public bool FilesHashForAuthorsParser(BackgroundWorker bw, DoWorkEventArgs e,
                                              Label StatusLabel, ProgressBar ProgressBar,
                                              HashtableClass htFB2ForBT, HashtableClass htBookTitleAuthors,
                                              bool WithMiddleName)
        {
            StatusLabel.Text += "Хеширование по Авторам книг...\r";
            ProgressBar.Maximum = htFB2ForBT.Values.Count;
            ProgressBar.Value = 0;
            // генерация списка ключей хеш-таблицы (для удаления обработанного элемента таблицы)
            List<string> keyList = _compComm.makeSortedKeysForGroups(htFB2ForBT);
            // группировка книг по одинаковым Авторам в пределах сгенерированных Групп книг по одинаковым Названиям
            int i = 0;
            foreach (string key in keyList) {
                // разбивка на группы для одинакового Названия по Авторам
                Hashtable htGroupAuthors = FindDupForAuthors((FB2FilesDataInGroup)htFB2ForBT[key], WithMiddleName);
                foreach (FB2FilesDataInGroup fb2List in htGroupAuthors.Values) {
                    if (!htBookTitleAuthors.ContainsKey(fb2List.Group))
                        htBookTitleAuthors.Add(fb2List.Group, fb2List);
                    else {
                        FB2FilesDataInGroup fb2ListInGroup = (FB2FilesDataInGroup)htBookTitleAuthors[fb2List.Group];
                        fb2ListInGroup.AddRange(fb2List);
                    }
                }

                // удаление обработанной группы книг, сгруппированных по одинаковому названию
                htFB2ForBT.Remove(key);
                bw.ReportProgress(++i);

                if (bw.CancellationPending) {
                    e.Cancel = true;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Cоздание групп копий по Авторам, относительно найденного Названия Книги
        /// </summary
        /// <param name="fb2Group">Экземпляр класса для хранения информации по одинаковым книгам в одной группе</param>
        /// <param name="WithMiddleName">Учитывать ли отчество Авторов (true) или нет (false) при поиске</param>
        private Hashtable FindDupForAuthors(FB2FilesDataInGroup fb2Group, bool WithMiddleName)
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
                    if (bd1.isSameBook(bd2, WithMiddleName, false)) {
                        if (!fb2NewGroup.isBookExists(bd2.Path))
                            fb2NewGroup.Add(bd2);
                    }
                }
                if (fb2NewGroup.Count >= 1) {
                    // только для копий, а не для единичных книг
                    fb2NewGroup.Group = fb2Group.Group + " ( " + fb2NewGroup.makeAuthorsString(WithMiddleName, false) + " )";
                    fb2NewGroup.Insert(0, bd1);
                    if (!ht.ContainsKey(fb2NewGroup.Group))
                        ht.Add(fb2NewGroup.Group, fb2NewGroup);
                }
            }
            return ht;
        }
    }
}
