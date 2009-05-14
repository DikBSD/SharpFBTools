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
		#endregion
	}
}
