/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 14.05.2009
 * Time: 22:19
 * 
 * * License: GPL 2.1
 */
using System;

namespace Settings
{
	/// <summary>
	/// Description of DataFM.
	/// </summary>
	public class DataFM
	{
		#region Закрытые данные класса
		private string m_sNoGenreGroup	= SettingsFM.ReadFMNoGenreGroup();
		private string m_sNoGenre		= SettingsFM.ReadFMNoGenre();
		private string m_sNoLang		= SettingsFM.ReadFMNoLang();
		private string m_sNoFirstName	= SettingsFM.ReadFMNoFirstName();
		private string m_sNoMiddleName	= SettingsFM.ReadFMNoMiddleName();
		private string m_sNoLastName	= SettingsFM.ReadFMNoLastName();
		private string m_sNoNickName	= SettingsFM.ReadFMNoNickName();
		private string m_sNoBookTitle	= SettingsFM.ReadFMNoBookTitle();
		private string m_sNoSequence	= SettingsFM.ReadFMNoSequence();
		private string m_sNoNSequence	= SettingsFM.ReadFMNoNSequence();
		
		private string m_s7zaPath	= Settings.Read7zaPath();
		private string m_sUnRarPath	= Settings.ReadUnRarPath();
		private string m_sRarPath	= Settings.ReadRarPath();
		
		private string m_sNotReadFB2Dir		= SettingsFM.ReadFMFB2NotReadDir();
		private string m_sFileLongPathDir	= SettingsFM.ReadFMFB2LongPathDir();
		private string m_sNotValidFB2Dir	= SettingsFM.ReadFMFB2NotValidDir();
		private string m_sNotOpenArchDir	= SettingsFM.ReadFMArchNotOpenDir();
		
		private bool m_bGenreTypeMode		= SettingsFM.ReadGenreTypeMode();
		private bool m_bGenresFB21Scheme	= SettingsFM.ReadFMGenresScheme();
		
		private bool m_bAuthorOneMode	= SettingsFM.ReadAuthorOneMode();
		private bool m_bGenreOneMode	= SettingsFM.ReadGenreOneMode();
		
		private bool m_bAllFB2			= SettingsFM.ReadSortValidType();
		
		private bool m_bAddToFileNameBookIDMode	= SettingsFM.ReadAddToFileNameBookIDMode();
		private int	 m_nFileExistMode	= SettingsFM.ReadFileExistMode();
		private bool m_bDelFB2FilesMode	= SettingsFM.ReadDelFB2FilesMode();

		private bool m_bToArchiveMode	= SettingsFM.ReadToArchiveMode();
		private string m_sArchiveTypeText	= SettingsFM.ReadArchiveTypeText();
		#endregion
		
		public DataFM()
		{
		}
		
		#region Свойства класса
		// названия папок для шаблонных тэгов, которые не имеют данных
		public virtual string NoGenreGroup {
			get { return m_sNoGenreGroup; }
			set { m_sNoGenreGroup = value; }
        }
		public virtual string NoGenre {
			get { return m_sNoGenre; }
			set { m_sNoGenre = value; }
        }
		public virtual string NoLang {
			get { return m_sNoLang; }
			set { m_sNoLang = value; }
        }
		public virtual string NoFirstName {
			get { return m_sNoFirstName; }
			set { m_sNoFirstName = value; }
        }
		public virtual string NoMiddleName {
			get { return m_sNoMiddleName; }
			set { m_sNoMiddleName = value; }
        }
		public virtual string NoLastName {
			get { return m_sNoLastName; }
			set { m_sNoLastName = value; }
        }
		public virtual string NoNickName {
			get { return m_sNoNickName; }
			set { m_sNoNickName = value; }
        }
		public virtual string NoBookTitle {
			get { return m_sNoBookTitle; }
			set { m_sNoBookTitle = value; }
        }
		public virtual string NoSequence {
			get { return m_sNoSequence; }
			set { m_sNoSequence = value; }
        }
		public virtual string NoNSequence {
			get { return m_sNoNSequence; }
			set { m_sNoNSequence = value; }
        }
		// режимы работы
		public virtual string A7zaPath {
			get { return m_s7zaPath; }
			set { m_s7zaPath = value; }
        }
		public virtual string UnRarPath {
			get { return m_sUnRarPath; }
			set { m_sUnRarPath = value; }
        }
		public virtual string RarPath {
			get { return m_sRarPath; }
			set { m_sRarPath = value; }
        }
		//
		public virtual string NotReadFB2Dir {
			get { return m_sNotReadFB2Dir; }
			set { m_sNotReadFB2Dir = value; }
        }
		public virtual string FileLongPathDir {
			get { return m_sFileLongPathDir; }
			set { m_sFileLongPathDir = value; }
        }
		public virtual string NotValidFB2Dir {
			get { return m_sNotValidFB2Dir; }
			set { m_sNotValidFB2Dir = value; }
        }
		public virtual string NotOpenArchDir {
			get { return m_sNotOpenArchDir; }
			set { m_sNotOpenArchDir = value; }
        }
		//
		public virtual bool GenreTypeMode {
			get { return m_bGenreTypeMode; }
			set { m_bGenreTypeMode = value; }
        }
		public virtual bool GenresFB21Scheme {
			get { return m_bGenresFB21Scheme; }
			set { m_bGenresFB21Scheme = value; }
        }
		public virtual bool AuthorOneMode {
			get { return m_bAuthorOneMode; }
			set { m_bAuthorOneMode = value; }
        }
		public virtual bool GenreOneMode {
			get { return m_bGenreOneMode; }
			set { m_bGenreOneMode = value; }
        }
		//
		public virtual bool SortValidType {
			get { return m_bAllFB2; }
			set { m_bAllFB2 = value; }
        }
		//
		public virtual int FileExistMode {
			get { return m_nFileExistMode; }
			set { m_nFileExistMode = value; }
        }
		public virtual bool AddToFileNameBookIDMode {
			get { return m_bAddToFileNameBookIDMode; }
			set { m_bAddToFileNameBookIDMode = value; }
        }
		public virtual bool DelFB2FilesMode {
			get { return m_bDelFB2FilesMode; }
			set { m_bDelFB2FilesMode = value; }
        }
		public virtual bool ToArchiveMode {
			get { return m_bToArchiveMode; }
			set { m_bToArchiveMode = value; }
        }
		public virtual string ArchiveTypeText {
			get { return m_sArchiveTypeText; }
			set { m_sArchiveTypeText = value; }
        }
		
		#endregion
	}
}
