/*
 * User: Кузнецов Вадим (DikBSD)
 * Date: 14.08.2019
 * Time: 11:37
 * 
 * License: GPL 2.1
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Collections;
using System.Security.Cryptography;
using System.IO;
using System.Xml.Linq;

using Core.Common;
using Core.FB2.FB2Parsers;
using Core.FB2.Description.Common;
using Core.FB2.Description.TitleInfo;

using FB2Validator = Core.FB2Parser.FB2Validator;
using FilesWorker = Core.Common.FilesWorker;
using SharpZipLibWorker = Core.Common.SharpZipLibWorker;
using MiscListView = Core.Common.MiscListView;
using Colors = Core.Common.Colors;
using EndWorkMode = Core.Common.EndWorkMode;

// enums
using SearchCompareModeEnum = Core.Common.Enums.SearchCompareModeEnum;
using EndWorkModeEnum = Core.Common.Enums.EndWorkModeEnum;
using FilesCountViewDupCollumnEnum = Core.Common.Enums.FilesCountViewDupCollumnEnum;
using ResultViewDupCollumnEnum = Core.Common.Enums.ResultViewDupCollumnEnum;

namespace Core.Duplicator
{
    /// <summary>
	/// Общие методы для различных режимов сравнение книг
	/// </summary>
    class CompareCommon
    {
        private FB2Validator _fv2Validator = new FB2Validator();
        private string _noOrEmptyBookIDString = "<тега ID либо нет, либо он пустой>";

        /// <summary>
        /// Удаление элементов таблицы, value (списки) которых состоят из 1-го элемента (это не копии)
        /// </summary>
        /// <param name="ht">Хеш Таблица, в которой производится удаление элементов по заданному алгоритму</param>
        public void removeNotCopiesEntryInHashTable(ref Hashtable ht)
        {
            List<DictionaryEntry> notCopies = new List<DictionaryEntry>();
            foreach (DictionaryEntry entry in ht) {
                FB2FilesDataInGroup fb2f = (FB2FilesDataInGroup)entry.Value;
                if (fb2f.Count == 1)
                    notCopies.Add(entry);
            }
            foreach (var ent in notCopies)
                ht.Remove(ent.Key);
        }

        /// <summary>
        /// Список файлов, которые не никак удалось открыть
        /// </summary>
        public List<string> collectBadFB2(string FilePath)
        {
            List<string> nonOpenedFileList = new List<string>();
            if (nonOpenedFileList.Count > 0) {
                foreach (string file in nonOpenedFileList) {
                    if (file != FilePath) {
                        nonOpenedFileList.Add(FilePath);
                        break;
                    }
                }
            } else {
                nonOpenedFileList.Add(FilePath);
            }
            return nonOpenedFileList;
        }

        /// <summary>
        /// Создание дерева списка копий для всех режимов сравнения
        /// </summary
        /// <param name="bw">Экземплар фонового обработчика класса BackgroundWorker</param>
        /// <param name="e">Экземпляр класса DoWorkEventArgs</param>
        /// <param name="ht">Заполненная хеш-таблица списками книг</param>
        public void makeTreeOfBookCopies(ref BackgroundWorker bw, ref DoWorkEventArgs e,
                                          Label StatusLabel, ProgressBar ProgressBar,
                                          CheckBox checkBoxSaveGroupsToXml, Label lblGroupCountForList,
                                          ComboBox cbGroupCountForList, ListView listViewFB2Files,
                                          StatusView sv, ref Hashtable ht)
        {
            // блокировка возможности сразу сохранять результат в xml файлы, минуя построения дерева.
            checkBoxSaveGroupsToXml.Enabled = false;
            lblGroupCountForList.Enabled = false;
            cbGroupCountForList.Enabled = false;

            StatusLabel.Text += "Создание списка (псевдодерево) одинаковых fb2-файлов...\r";
            ProgressBar.Maximum = ht.Values.Count;
            ProgressBar.Value = 0;
            // сортировка ключей (групп)
            List<string> keyList = makeSortedKeysForGroups(ref ht);
            int i = 0;
            foreach (string key in keyList) {
                if (bw.CancellationPending) {
                    e.Cancel = true;
                    return;
                }
                ++sv.Group; // число групп одинаковых книг
                             // формирование представления Групп с их книгами
                makeBookCopiesView((FB2FilesDataInGroup)ht[key], listViewFB2Files, ref sv);
                bw.ReportProgress(++i);
            }
        }

        /// <summary>
		/// Сортировка ключей (групп)
		/// </summary>
		/// <param name="htBookGroups">Хэш-таблица Групп</param>
		/// <returns>Список List объектов типа string значений отсортированных ключей хэш-таблицы</returns>
		public List<string> makeSortedKeysForGroups(ref Hashtable htBookGroups)
        {
            List<string> keyList = new List<string>();
            foreach (string key in htBookGroups.Keys)
                keyList.Add(key);
            keyList.Sort();
            return keyList;
        }

        /// <summary>
		/// Создание хеш-таблицы для групп одинаковых книг
		/// </summary>
		private bool AddBookGroupInHashTable(ref Hashtable groups, ref ListViewGroup lvg)
        {
            if (groups != null) {
                if (!groups.Contains(lvg.Header)) {
                    groups.Add(lvg.Header, lvg);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
		/// Формирование представления Групп с их книгами
		/// </summary>
		private void makeBookCopiesView(FB2FilesDataInGroup fb2BookList, ListView listViewFB2Files, ref StatusView sv)
        {
            Hashtable htBookGroups = new Hashtable(new FB2CultureComparer()); // хеш-таблица групп одинаковых книг
            ListViewGroup lvGroup = null; // группа одинаковых книг
            string Valid = string.Empty;
            foreach (BookData bd in fb2BookList) {
                ++sv.AllFB2InGroups; // число книг во всех группах одинаковых книг
                lvGroup = new ListViewGroup(fb2BookList.Group);
                ListViewItem lvi = new ListViewItem(bd.Path);
                if (FilesWorker.isFB2Archive(bd.Path))
                    lvi.ForeColor = Colors.ZipFB2ForeColor;
                lvi.SubItems.Add(MakeBookTitleString(bd.BookTitle));
                lvi.SubItems.Add(MakeAuthorsString(bd.Authors));
                lvi.SubItems.Add(MakeGenresString(bd.Genres));
                lvi.SubItems.Add(bd.Lang);
                lvi.SubItems.Add(bd.Id);
                lvi.SubItems.Add(bd.Version);
                lvi.SubItems.Add(bd.Encoding);

                Valid = _fv2Validator.ValidatingFB2File(bd.Path);
                if (string.IsNullOrEmpty(Valid)) {
                    Valid = "Да";
                    lvi.ForeColor = FilesWorker.isFB2File(bd.Path) ? Color.FromName("WindowText") : Colors.ZipFB2ForeColor;
                } else {
                    Valid = "Нет";
                    lvi.ForeColor = Colors.FB2NotValidForeColor;
                }

                lvi.SubItems.Add(Valid);
                lvi.SubItems.Add(GetFileLength(bd.Path));
                lvi.SubItems.Add(GetFileCreationTime(bd.Path));
                lvi.SubItems.Add(FileLastWriteTime(bd.Path));
                // заносим группу в хеш, если она там отсутствует
                AddBookGroupInHashTable(ref htBookGroups, ref lvGroup);
                // присваиваем группу книге
                listViewFB2Files.Groups.Add((ListViewGroup)htBookGroups[fb2BookList.Group]);
                lvi.Group = (ListViewGroup)htBookGroups[fb2BookList.Group];
                listViewFB2Files.Items.Add(lvi);
            }
        }

        /// <summary>
		/// Длина файла
		/// </summary>
        public string GetFileLength(string sFilePath)
        {
            FileInfo fi = new FileInfo(sFilePath);
            return fi.Exists ? FilesWorker.FormatFileLength(fi.Length) : string.Empty;
        }

        /// <summary>
		/// Время создания файла
		/// </summary>
        public string GetFileCreationTime(string sFilePath)
        {
            FileInfo fi = new FileInfo(sFilePath);
            return fi.Exists ? fi.CreationTime.ToString() : string.Empty;
        }

        /// <summary>
		/// Время последней записи в файл
		/// </summary>
        public string FileLastWriteTime(string sFilePath)
        {
            FileInfo fi = new FileInfo(sFilePath);
            return fi.Exists ? fi.LastWriteTime.ToString() : string.Empty;
        }

        /// <summary>
		/// Формирование строки с Названием Книги
		/// </summary>
		public string MakeBookTitleString(BookTitle bookTitle)
        {
            if (bookTitle == null)
                return string.Empty;
            return bookTitle.Value ?? string.Empty; //return bookTitle.Value != null ? bookTitle.Value : string.Empty;
        }

        /// <summary>
		/// Формирование строки с Авторами Книги из списка всех Авторов ЭТОЙ Книги
		/// </summary>
		public string MakeAuthorsString(IList<Author> Authors)
        {
            if (Authors != null) {
                string sA = string.Empty;
                foreach (Author a in Authors) {
                    if (a.LastName != null && !string.IsNullOrWhiteSpace(a.LastName.Value))
                        sA += a.LastName.Value.Trim() + " ";
                    if (a.FirstName != null && !string.IsNullOrWhiteSpace(a.FirstName.Value))
                        sA += a.FirstName.Value.Trim() + " ";
                    if (a.MiddleName != null && !string.IsNullOrWhiteSpace(a.MiddleName.Value))
                        sA += a.MiddleName.Value.Trim() + " ";
                    if (a.NickName != null && !string.IsNullOrWhiteSpace(a.NickName.Value))
                        sA += a.NickName.Value.Trim();
                    sA = sA.Trim();
                    if (!string.IsNullOrWhiteSpace(sA))
                        sA += "; ";
                }
                return sA.LastIndexOf(';') > -1
                    ? sA.Substring(0, sA.LastIndexOf(';')).Trim()
                    : sA.Trim();
            }
            return string.Empty;
        }

        /// <summary>
        /// Формирование строки с Жанрами Книги из списка всех Жанров ЭТОЙ Книги
        /// </summary>
        public string MakeGenresString(IList<Genre> Genres)
        {
            if (Genres != null) {
                string sG = string.Empty;
                foreach (Genre g in Genres) {
                    if (!string.IsNullOrWhiteSpace(g.Name))
                        sG += g.Name.Trim();
                    sG = sG.Trim();
                    if (!string.IsNullOrWhiteSpace(sG))
                        sG += "; ";
                }
                sG = sG.Trim();
                return sG.LastIndexOf(';') > -1
                    ? sG.Substring(0, sG.LastIndexOf(';')).Trim()
                    : sG.Trim();
            }
            return string.Empty;
        }

        /// <summary>
        /// Проверка ID книги на наличие и/или пустоту и присвоение ID в этом случае временного определенного значения
        /// </summary
        public void VerifyBookID(BookData bd)
        {
            string bdId = bd.Id;
            if (bdId == null || string.IsNullOrEmpty(bdId) || string.IsNullOrWhiteSpace(bdId))
                bd.Id = _noOrEmptyBookIDString;
        }

        // =============================================================================================
        // 							СОХРАНЕНИЕ РЕЗУЛЬТАТА ПОИСКА КОПИЙ В XML-ФАЙЛЫ
        // =============================================================================================
        #region Сохранение результата поиска копий в xml-файлы
        /// <summary>
        /// сохранение результата сразу в xml-файлы без построения визуального списка
        /// </summary>
        /// <param name="bw">Ссылка на объект класса BackgroundWorker</param>
        /// <param name="e">Ссылка на объект класса DoWorkEventArgs</param>
        /// <param name="GroupCountForList">Число Групп в Списке Групп</param>
        /// <param name="CompareMode">Вид сравнения при поиске копий</param>
        /// <param name="CompareModeName">Название вида сравнения при поиске копий</param>
        /// <param name="ht">Хэш-таблица данных на Группы (копии fb2 книг по Группам)</param>
        public void saveCopiesListToXml(ref BackgroundWorker bw, ref DoWorkEventArgs e, int GroupCountForList,
                                         int CompareMode, string CompareModeName, Label StatusLabel, ProgressBar ProgressBar,
                                         StatusView sv, string Source, bool ScanSubDirs, ComboBox cbGroupCountForList,
                                         CheckBox checkBoxSaveGroupsToXml, ref Hashtable ht)

        {
            // блокировка отмены сохранения результата в файлы
            StatusLabel.Text += "Сохранение результата поиска в xml-файлы (папка '_Copies') без построения дерева копий...\r";
            ProgressBar.Maximum = ht.Values.Count;
            ProgressBar.Value = 0;

            const string ToDirName = "_Copies";
            if (!Directory.Exists(ToDirName))
                Directory.CreateDirectory(ToDirName);

            // "сквозной" счетчик числа групп для каждого создаваемого xml файла копий
            int ThroughGroupCounterForXML = 0;
            // счетчик (в границых CompareModeName) числа групп для каждого создаваемого xml файла копий
            int GroupCounterForXML = 0;
            // номер файла - для формирования имени создаваемого xml файла копий
            int XmlFileNumber = 0;

            // копии fb2 книг по группам
            if (ht.Values.Count > 0) {
                XDocument doc = createXMLStructure(CompareMode, CompareModeName, Source, ScanSubDirs,
                                                   cbGroupCountForList, checkBoxSaveGroupsToXml);

                int BookInGroups = 0;       // число книг (books) в Группах (Groups)
                int GroupCountInGroups = 0; // число Групп (Group count) в Группах (Groups)
                int i = 0;                  // прогресс
                bool one = false;
                // сортировка ключей (групп)
                List<string> keyList = makeSortedKeysForGroups(ref ht);
                foreach (string key in keyList) {
                    if (bw.CancellationPending) {
                        e.Cancel = true;
                        return;
                    }

                    ++sv.Group; // число групп одинаковых книг
                                 // формирование представления Групп с их книгами
                    addAllBookInGroup(ref bw, ref e, ref doc, (FB2FilesDataInGroup)ht[key],
                                        ref BookInGroups, ref GroupCountInGroups, ref sv);

                    ++GroupCounterForXML;
                    ++ThroughGroupCounterForXML;
                    doc.Root.Element("SelectedItem").SetValue("0");
                    if (GroupCountForList <= ht.Values.Count) {
                        if (GroupCounterForXML >= GroupCountForList) {
                            string FileNumber = StringProcessing.makeNNNStringOfNumber(++XmlFileNumber) + ".dup_lbc";
                            setDataForNode(ref doc, GroupCountInGroups, BookInGroups);
                            doc.Save(Path.Combine(ToDirName, FileNumber));
                            StatusLabel.Text += "Файл: '_Copies\\" + FileNumber + "' создан...\r";
                            doc.Root.Element("Groups").Elements().Remove();
                            GroupCountInGroups = 0;
                            GroupCounterForXML = 0;
                            BookInGroups = 0;
                        } else {
                            // последний диаппазон Групп
                            if (ThroughGroupCounterForXML == ht.Values.Count) {
                                string FileNumber = StringProcessing.makeNNNStringOfNumber(++XmlFileNumber) + ".dup_lbc";
                                setDataForNode(ref doc, GroupCountInGroups, BookInGroups);
                                doc.Save(Path.Combine(ToDirName, FileNumber));
                                StatusLabel.Text += "Файл: '_Copies\\" + FileNumber + "' создан...\r";
                            }
                        }
                    } else {
                        setDataForNode(ref doc, GroupCountInGroups, BookInGroups);
                        one = true;
                    }
                    bw.ReportProgress(i++);
                } // по всем Группам
                if (one) {
                    StatusLabel.Text += @"Файл: '_Copies\001.dup_lbc' ...\r";
                    doc.Save(Path.Combine(ToDirName, "001.dup_lbc"));
                }
            }
        }
        /// <summary>
        /// Добавление Группы в Список Групп
        /// </summary>
        /// <param name="bw">BackgroundWorker</param>
        /// <param name="e">DoWorkEventArgs</param>
        /// <param name="doc">xml документ - объект класса XDocument, в который заносятся данные на книги Групп</param>
        /// <param name="fb2BookList">Список данных fb2 книг для конкретной Группы</param>
        /// <param name="BookInGroups">Число книг в Группе</param>
        /// <param name="GroupCountInGroups">Счетчик числа Групп</param>
        private void addAllBookInGroup(ref BackgroundWorker bw, ref DoWorkEventArgs e,
                                       ref XDocument doc, FB2FilesDataInGroup fb2BookList,
                                       ref int BookInGroups, ref int GroupCountInGroups,
                                       ref StatusView sv)
        {
            BookInGroups += fb2BookList.Count;

            // Добавление Группы в Список Групп
            XElement xeGroup = null;
            doc.Root.Element("Groups").Add(
                xeGroup = new XElement(
                    "Group", new XAttribute("number", 0),
                    new XAttribute("count", fb2BookList.Count),
                    new XAttribute("name", fb2BookList.Group)
                )
            );

            int BookNumber = 0;         // номер Книги (Book number) В Группе (Group)
            int BookCountInGroup = 0;   // число Книг (Group count) в Группе (Group)

            foreach (BookData bd in fb2BookList) {
                if (bw.CancellationPending) {
                    e.Cancel = true;
                    return;
                }

                ++sv.AllFB2InGroups; // число книг во всех группах одинаковых книг
                string sForeColor = "WindowText";
                if (FilesWorker.isFB2Archive(bd.Path))
                    sForeColor = Colors.ZipFB2ForeColor.Name;
                string Validation = _fv2Validator.ValidatingFB2File(bd.Path);
                if (string.IsNullOrEmpty(Validation)) {
                    Validation = "Да";
                    sForeColor = FilesWorker.isFB2File(bd.Path) ? "WindowText" : Colors.ZipFB2ForeColor.Name;
                } else {
                    Validation = "Нет";
                    sForeColor = Colors.FB2NotValidForeColor.Name;
                }
                // Добавление Книги в Группу
                xeGroup.Add(
                    new XElement("Book", new XAttribute("number", ++BookNumber),
                                 new XElement("Group", fb2BookList.Group),
                                 new XElement("Path", bd.Path),
                                 new XElement("BookTitle", MakeBookTitleString(bd.BookTitle)),
                                 new XElement("Authors", MakeAuthorsString(bd.Authors)),
                                 new XElement("Genres", MakeGenresString(bd.Genres)),
                                 new XElement("BookLang", bd.Lang),
                                 new XElement("BookID", bd.Id),
                                 new XElement("Version", bd.Version),
                                 new XElement("FB2Authors", MakeAuthorsString(bd.FB2Authors)),
                                 new XElement("Encoding", bd.Encoding),
                                 new XElement("Validation", Validation),
                                 new XElement("FileLength", GetFileLength(bd.Path)),
                                 new XElement("FileCreationTime", GetFileCreationTime(bd.Path)),
                                 new XElement("FileLastWriteTime", FileLastWriteTime(bd.Path)),
                                 new XElement("ForeColor", sForeColor),
                                 new XElement("BackColor", "Window"),
                                 new XElement("IsChecked", false)
                                )
                );

                xeGroup.SetAttributeValue("count", ++BookCountInGroup);
                if (!xeGroup.HasElements) {
                    xeGroup.Remove();
                }
            } // по всем книгам Группы
            ++GroupCountInGroups;
        }
        /// <summary>
        /// Заполнение данными ноды для генерируемых файлов списка копий
        /// </summary>
        /// <param name="doc">xml документ - объект класса XDocument, в который заносятся данные на книги Групп</param>
        /// <param name="GroupCountInGroups">Счетчик числа Групп</param>
        /// <param name="BookInGroups">Число книг в Группе</param>
        private void setDataForNode(ref XDocument doc, int GroupCountInGroups, int BookInGroups)
        {
            XElement xCompareData = doc.Root.Element("CompareData");
            if (xCompareData != null) {
                xCompareData.SetElementValue("Groups", GroupCountInGroups);
                xCompareData.SetElementValue("AllFB2InGroups", BookInGroups);
            }
            // заполнение аттрибутов
            XElement xGroups = doc.Root.Element("Groups");
            if (xGroups != null) {
                xGroups.SetAttributeValue("count", GroupCountInGroups);
                xGroups.SetAttributeValue("books", BookInGroups);
                IEnumerable<XElement> Groups = xGroups.Elements("Group");
                int i = 0;
                foreach (XElement Group in Groups)
                    Group.SetAttributeValue("number", ++i);
            }
        }
        /// <summary>
        /// Создание основных разделов xml структуры объекта класса XDocument
        /// </summary>
        /// <param name="CompareMode">Вид сравнения при поиске копий</param>
        /// <param name="CompareModeName">Название вида сравнения при поиске копий</param>
        /// <returns>Объект класса XDocument </returns>
        private XDocument createXMLStructure(int CompareMode, string CompareModeName, string Source, bool ScanSubDirs,
                                            ComboBox cbGroupCountForList, CheckBox checkBoxSaveGroupsToXml)
        {
            return new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XComment("Файл копий fb2 книг, сохраненный после полного окончания работы Дубликатора"),
                new XElement("Files", new XAttribute("type", "dup_endwork"),
                             new XComment("Папка для поиска копий fb2 книг"),
                             new XElement("SourceDir", Source),
                             new XComment("Настройки поиска-сравнения fb2 книг"),
                             new XElement("Settings",
                                          new XElement("ScanSubDirs", ScanSubDirs),
                                          new XElement("GroupCountForList", cbGroupCountForList.SelectedIndex),
                                          new XElement("SaveGroupToXMLWithoutTree", checkBoxSaveGroupsToXml.Checked)),
                             new XComment("Режим поиска-сравнения fb2 книг"),
                             new XElement("CompareMode",
                                          new XAttribute("index", CompareMode),
                                          new XElement("Name", CompareModeName)),
                             new XComment("Данные о ходе сравнения fb2 книг"),
                             new XElement("CompareData",
                                          new XElement("Groups", "0"),
                                          new XElement("AllFB2InGroups", "0")
                                         ),
                             new XComment("Копии fb2 книг по группам"),
                             new XElement("Groups",
                                          new XAttribute("count", "0"),
                                          new XAttribute("books", "0")
                                         ),
                             new XComment("Выделенный элемент списка, на котором завершили обработку книг"),
                             new XElement("SelectedItem", "-1")
                            )
            );
        }
        #endregion

        #region Свойства класса
        /// <summary>
        /// Строка названия ID книги, если тега id либо нет, либо его значение пусто
        /// </summary>
        public virtual string NoOrEmptyBookIDString
        {
            get { return _noOrEmptyBookIDString; }
            set { _noOrEmptyBookIDString = value; }
        }
        #endregion
        

    }
}
