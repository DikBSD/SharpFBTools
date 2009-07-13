/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 23.06.2009
 * Time: 8:50
 * 
 * License: GPL 2.1
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using Settings;

using FB2.Genres;


namespace SharpFBTools.Tools
{
	/// <summary>
	/// Description of SelectedSortData.
	/// </summary>
	public partial class SelectedSortData : Form
	{
		#region Закрытые данные класса
		private string m_sTitle		= "SharpFBTools - Избранная Сортировка";
		private bool m_bOKClicked	= false;
		#endregion

		public SelectedSortData()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			
			// формирование Списка Языков
			MakeListFMLangs();
			// формирование Списка Групп Жанров
			MakeListFMGenresGroups();
			// формирование Списка Жанров
			MakeListFMGenres();
		}
		
		#region Закрытые вспомогательные методы класса
				private void MakeListFMLangs() {
			// формирование Списка Языков
			string[] m_sLang = {
				"Russian (ru)","English (en)",
				"Abkhazian (ab)","Afar (aa)","Afrikaans (af)","Albanian (sq)","Amharic (am)","Arabic (ar)","Armenian (hy)","Assamese (as)","Aymara (ay)","Azerbaijani (az)", 
				"Bashkir (ba)","Basque (eu)","Bengali (bn)","Bhutani (dz)","Bihari (bh)","Bislama (bi)","Breton (br)","Bulgarian (bg)","Burmese (my)","Byelorussian (be)",
				"Cambodian (km)","Catalan (ca)","Chinese (zh)","Corsican (co)","Croatian (hr)","Czech (cs)",
				"Danish (da)","Dutch (nl)",
				"Esperanto (eo)","Estonian (et)",
				"Faroese (fo)","Fiji (fj)","Finnish (fi)","French (fr)","Frisian (fy)",
				"Galician (gl)","Georgian (ka)","German (de)","Greek (el)","Greenlandic (kl)","Guarani (gn)","Gujarati (gu)",
				"Hausa (ha)","Hebrew (he)","Hindi (hi)","Hungarian (hu)",
				"Icelandic (is)","Indonesian (in)","Interlingua (ia)","Inuktitut (iu)","Inupiak (ik)","Irish (ga)","Italian (it)",
				"Japanese (ja)","Javanese (jw)",
				"Kannada (kn)","Kashmiri (ks)","Kazakh (kk)","Kirghiz (ky)","Kirundi (rn)","Kiyarwanda (rw)","Korean (ko)","Kurdish (ku)",
				"Laotian (lo)","Latin (la)","Latvian (lv)","Lingala (ln)","Lithuanian (lt)",
				"Macedonian (mk)","Malagasy (mg)","Malay (ms)","Malayalam (ml)","Maltese (mt)","Maori (mi)","Marathi (mr)","Moldavian (mo)","Mongolian (mn)",
				"Nauru (na)","Nauru (na)","Nepali (ne)","Norwegian (no)",
				"Occitan (oc)","Oriya (or)","Oromo (om)",
				"Pashto (ps)","Persian (fa)","Polish (pl)","Portuguese (pt)","Pundjabi (pa)",
				"Quechua (qu)",
				"Rhaeto-Romance (rm)","Romanian (ro)",
				"Samoan (sm)","Sangho (sg)","Sanskrit (sa)","Scots Gaelic (gd)","Serbian (sr)","Serbo-Croatian (sh)","Sesotho (st)","Setswana (tn)","Shona (sn)","Sindhi (sd)","Singhalese (si)","Siswati (ss)","Slovak (sk)","Slovenian (sl)","Somali (so)","Spanish (es)","Sudanese (su)","Swahili (sw)","Swedish (sv)",
				"Tagalog (tl)","Tajik (tg)","Tamil (ta)","Tatar (tt)","Telugu (te)","Thai (th)","Tibetan (bo)","Tigrinya (ti)","Tonga (to)","Tsonga (ts)","Turkish (tr)","Turkman (tk)","Twi (tw)",
				"Uigur (ug)","Ukrainian (uk)","Urdu (ur)","Uzbek (uz)",
				"Vietnamese (vi)","Volapuk (vo)",
				"Welsh (cy)","Wolof (wo)",
				"Xhosa (xh)",
				"Yiddish (yi)","Yorouba (yo)",
				"Zhuang (za)","Zulu (zu)"
			};
			cmbBoxSSLang.Items.AddRange( m_sLang );
			cmbBoxSSLang.SelectedIndex = 0;
		}
		
		private void MakeListFMGenresGroups() {
			// формирование Списка Групп Жанров
			cmbBoxSSGenresGroup.Items.Add( SettingsFM.ReadFMSf() );
			cmbBoxSSGenresGroup.Items.Add( SettingsFM.ReadFMDetective() );
			cmbBoxSSGenresGroup.Items.Add( SettingsFM.ReadFMProse() );
			cmbBoxSSGenresGroup.Items.Add( SettingsFM.ReadFMLove() );
			cmbBoxSSGenresGroup.Items.Add( SettingsFM.ReadFMAdventure() );
			cmbBoxSSGenresGroup.Items.Add( SettingsFM.ReadFMChildren() );
			cmbBoxSSGenresGroup.Items.Add( SettingsFM.ReadFMPoetry() );
			cmbBoxSSGenresGroup.Items.Add( SettingsFM.ReadFMAntique() );
			cmbBoxSSGenresGroup.Items.Add( SettingsFM.ReadFMScience() );
			cmbBoxSSGenresGroup.Items.Add( SettingsFM.ReadFMComputers() );
			cmbBoxSSGenresGroup.Items.Add( SettingsFM.ReadFMReference() );
			cmbBoxSSGenresGroup.Items.Add( SettingsFM.ReadFMNonfiction() );
			cmbBoxSSGenresGroup.Items.Add( SettingsFM.ReadFMReligion() );
			cmbBoxSSGenresGroup.Items.Add( SettingsFM.ReadFMHumor() );
			cmbBoxSSGenresGroup.Items.Add( SettingsFM.ReadFMHome() );
			cmbBoxSSGenresGroup.Items.Add( SettingsFM.ReadFMBusiness() );
			
			cmbBoxSSGenresGroup.SelectedIndex = 0;
		}
		
		private void MakeListFMGenres() {
			// формирование Списка Жанров
			DataFM dfm = new DataFM();
			IFBGenres fb2g = null;
			if( dfm.GenresFB21Scheme ) {
				fb2g = new FB21Genres();
			} else {
				fb2g = new FB22Genres();
			}
			string[] m_sGenresNames	= fb2g.GetFBGenreNamesArray();
			string[] m_sCodes		= fb2g.GetFBGenreCodesArray();
			for( int i=0; i!=m_sGenresNames.Length; ++i ) {
				cmbBoxSSGenres.Items.Add( m_sGenresNames[i] + " (" + m_sCodes[i] + ")" );
			}
			cmbBoxSSGenres.SelectedIndex = 0;
		}
		
		private bool IsRecordExist() {
			// есть ли такая запись в списке
			if( lvSSData.Items.Count == 0 ) {
				return false;
			}
			string sLang	= cmbBoxSSLang.Text;
			string sGG		= "";
			string sGenre	= "";
			if( chkBoxGenre.Checked ) {
				if( rbtnSSGenresGroup.Checked ) {
					sGG = cmbBoxSSGenresGroup.Text.Trim();
				} else {
					sGenre = cmbBoxSSGenres.Text.Trim();
				}
			}
			string sLast	= textBoxSSLast.Text.Trim()		!= "" ? textBoxSSLast.Text.Trim()	: "";
			string sFirst	= textBoxSSFirst.Text.Trim()	!= "" ? textBoxSSFirst.Text.Trim()	: "";
			string sMiddle	= textBoxSSMiddle.Text.Trim()	!= "" ? textBoxSSMiddle.Text.Trim() : "";
			string sNick	= textBoxSSNick.Text.Trim()		!= "" ? textBoxSSNick.Text.Trim()	: "";
			string sSeq		= txtBoxSSSequence.Text.Trim()	!= "" ? txtBoxSSSequence.Text.Trim(): "";
			string sBTitle	= txtBoxSSBookTitle.Text.Trim()	!= "" ? txtBoxSSBookTitle.Text.Trim(): "";
			
			// перебираем все записи в списке
			for( int i=0; i!=lvSSData.Items.Count; ++i ) {
				if( lvSSData.Items[i].Text==sLang && 
				  lvSSData.Items[i].SubItems[1].Text==sGG		&& 
				  lvSSData.Items[i].SubItems[2].Text==sGenre	&&
				  lvSSData.Items[i].SubItems[3].Text==sLast		&&
				  lvSSData.Items[i].SubItems[4].Text==sFirst	&&
				  lvSSData.Items[i].SubItems[5].Text==sMiddle	&&
				  lvSSData.Items[i].SubItems[6].Text==sNick		&&
				  lvSSData.Items[i].SubItems[7].Text==sSeq		&&
				  lvSSData.Items[i].SubItems[8].Text==sBTitle ) {
					return true;
				}
			}
			return false;
		}
		#endregion
		
		#region Открытые методы
		public bool IsOKClicked() {
			return m_bOKClicked;
		}
		#endregion
		
		#region Обработчики событий
		void ChBoxSSLangCheckedChanged(object sender, EventArgs e)
		{
			cmbBoxSSLang.Enabled = chBoxSSLang.Checked;
		}
		
		void ChBoxAuthorCheckedChanged(object sender, EventArgs e)
		{
			textBoxSSLast.Enabled	= chBoxAuthor.Checked;
			textBoxSSFirst.Enabled	= chBoxAuthor.Checked;
			textBoxSSMiddle.Enabled	= chBoxAuthor.Checked;
			textBoxSSNick.Enabled	= chBoxAuthor.Checked;
		}
		
		void ChBoxSSSequenceCheckedChanged(object sender, EventArgs e)
		{
			txtBoxSSSequence.Enabled = chBoxSSSequence.Checked;
		}
		
		void ChkBoxGanreCheckedChanged(object sender, EventArgs e)
		{
			gBoxGenre.Enabled = chkBoxGenre.Checked;
		}
		
		void RbtnSSGenresGroupCheckedChanged(object sender, EventArgs e)
		{
			cmbBoxSSGenresGroup.Enabled = rbtnSSGenresGroup.Checked;
		}
		
		void RbtnSSGenresCheckedChanged(object sender, EventArgs e)
		{
			cmbBoxSSGenres.Enabled = rbtnSSGenres.Checked;
		}

		void ChkBoxBookTitleCheckedChanged(object sender, EventArgs e)
		{
			txtBoxSSBookTitle.Enabled = chkBoxBookTitle.Checked;
		}
		
		void BtnAddClick(object sender, EventArgs e)
		{
			// Добавить данные сортировки в список
			// проверка, выбранали хоть одна опция сортировки
			if( !chBoxSSLang.Checked && !chBoxAuthor.Checked &&
			   	!chkBoxGenre.Checked && !chBoxSSSequence.Checked &&
			    !chkBoxBookTitle.Checked ) {
				MessageBox.Show( "Выберите хоть одну опцию поиска для сортировки (чекбоксы)!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}

			// если выбран ТОЛЬКО Автор и (или) Серия и (или) Название Книги
			if( !chBoxSSLang.Checked && !chkBoxGenre.Checked) {
				if( chBoxAuthor.Checked ) {
					// выбран Автор - не пустые ли все его поля
					if( textBoxSSLast.Text.Trim().Length==0 && textBoxSSFirst.Text.Trim().Length==0 &&
					  textBoxSSMiddle.Text.Trim().Length==0 && textBoxSSNick.Text.Trim().Length==0 ) {
						MessageBox.Show( "Заполните хоть одно поле для Автора Книг!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
						return;
					}
				}
				if( chkBoxBookTitle.Checked ) {
					// выбрано Название Книги - не пустое ли ее поле
					if( txtBoxSSBookTitle.Text.Trim().Length==0 ) {
						MessageBox.Show( "Заполните поле для Названия Книги!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
						return;
					}
				}
				if( chBoxSSSequence.Checked ) {
					// выбрана Серия - не пустое ли ее поле
					if( txtBoxSSSequence.Text.Trim().Length==0 ) {
						MessageBox.Show( "Заполните поле для Серии Книги!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
						return;
					}
				}
			}
			
			// проверка, есть ли вводимые данные в списке
			if( IsRecordExist() ) {
				MessageBox.Show( "Вводимые данные уже есть в списке!", m_sTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			
			ListViewItem lvi = null;
			// Язык Книги
			if( chBoxSSLang.Checked ) {
				lvi = new ListViewItem( cmbBoxSSLang.Text );
			} else {
				lvi = new ListViewItem( "" );
			}
			
			// Жанр Книги
			if( chkBoxGenre.Checked ) {
				if( rbtnSSGenresGroup.Checked ) {
					lvi.SubItems.Add( cmbBoxSSGenresGroup.Text.Trim() );
					lvi.SubItems.Add( "" );
				} else {
					lvi.SubItems.Add( "" );
					lvi.SubItems.Add( cmbBoxSSGenres.Text.Trim() );
				}
			} else {
				for( int i=0; i!=2; ++i ) {
					lvi.SubItems.Add( "" );
				}
			}
			
			// Автор Книги
			if( chBoxAuthor.Checked ) {
				if( textBoxSSLast.Text.Trim().Length!=0 ) {
					lvi.SubItems.Add( textBoxSSLast.Text.Trim() );
				} else lvi.SubItems.Add( "" );
				if( textBoxSSFirst.Text.Trim().Length!=0 ) {
					lvi.SubItems.Add( textBoxSSFirst.Text.Trim() );
				} else lvi.SubItems.Add( "" );
				if( textBoxSSMiddle.Text.Trim().Length!=0 ) {
					lvi.SubItems.Add( textBoxSSMiddle.Text.Trim() );
				} else lvi.SubItems.Add( "" );
				if( textBoxSSNick.Text.Trim().Length!=0 ) {
					lvi.SubItems.Add( textBoxSSNick.Text.Trim() );
				} else lvi.SubItems.Add( "" );
			} else {
				for( int i=0; i!=4; ++i ) {
					lvi.SubItems.Add( "" );
				}
			}

			// Серия Книги
			if( chBoxSSSequence.Checked ) {
				if( txtBoxSSSequence.Text.Trim().Length!=0 ) {
					lvi.SubItems.Add( txtBoxSSSequence.Text.Trim() );
				} else lvi.SubItems.Add( "" );
			} else {
				lvi.SubItems.Add( "" );
			}
			
			// Название Книги
			if( chkBoxBookTitle.Checked ) {
				if( txtBoxSSBookTitle.Text.Trim().Length!=0 ) {
					lvi.SubItems.Add( txtBoxSSBookTitle.Text.Trim() );
				} else lvi.SubItems.Add( "" );
			} else {
				lvi.SubItems.Add( "" );
			}
			
			// Точное соответствие
			if( chBoxExactFit.Checked ) {
				lvi.SubItems.Add( "Да" );
			} else {
				lvi.SubItems.Add( "Нет" );
			}
			
			// добавление записи в список
			lvSSData.Items.Add( lvi );
			for( int i=0; i!=lvSSData.Items.Count; ++i ) {
				lvSSData.Items[ i ].Selected = false;
			}
			lvSSData.Items[ lvSSData.Items.Count-1 ].Selected	= true;
			lvSSData.Items[ lvSSData.Items.Count-1 ].Focused	= true;
			
			// очищаем поля ввода
			textBoxSSLast.Text		= "";
			textBoxSSFirst.Text		= "";
			textBoxSSMiddle.Text	= "";
			textBoxSSNick.Text		= "";
			txtBoxSSSequence.Text	= "";
			txtBoxSSBookTitle.Text	= "";
			
			lblCount.Text = Convert.ToString( lvSSData.Items.Count );

			btnOK.Enabled = true;
		}
		
		void LvSSDataSelectedIndexChanged(object sender, EventArgs e)
		{
			if( lvSSData.SelectedItems.Count == 0 ) {
				btnDelete.Enabled = false;
			} else {
				btnDelete.Enabled 		= true;
				btnDeleteAll.Enabled 	= true;
			}
		}
		
		void BtnDeleteClick(object sender, EventArgs e)
		{
			// удаление данных сортировки из списка
			string sMess = "Вы действительно хотите удалить выбранные данные из списка?";
			MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			DialogResult result;
			result = MessageBox.Show( sMess, m_sTitle, buttons, MessageBoxIcon.Question );
	        if(result == DialogResult.No) {
	            return;
			}
			lvSSData.Items.Remove( lvSSData.SelectedItems[0] );
			
			if( lvSSData.SelectedItems.Count == 0 ) {
				btnOK.Enabled = false;
			} else {
				btnOK.Enabled = true;
			}
			
			lblCount.Text = Convert.ToString( lvSSData.Items.Count );
			
			if( lvSSData.Items.Count > 0 ) {
				btnOK.Enabled = true;
			} else {
				btnDelete.Enabled 		= false;
				btnDeleteAll.Enabled	= false;
			}
		}
		
		void BtnDeleteAllClick(object sender, EventArgs e)
		{
			// удаление всех данных сортировки из списка
			string sMess = "Вы действительно хотите удалить ВСЕ данные из списка?";
			MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			DialogResult result;
			result = MessageBox.Show( sMess, m_sTitle, buttons, MessageBoxIcon.Question );
	        if(result == DialogResult.No) {
	            return;
			}
			
			lvSSData.Items.Clear();
			
			btnDelete.Enabled 		= false;
			btnDeleteAll.Enabled	= false;
			//btnOK.Enabled 			= false;
			
			lblCount.Text = Convert.ToString( lvSSData.Items.Count );
		}
		
		void BtnOKClick(object sender, EventArgs e)
		{
			// принять данные
			m_bOKClicked = true;
			this.Close();
		}
		
		void ChBoxAuthorClick(object sender, EventArgs e)
		{
			textBoxSSLast.Focus();
		}
		
		void ChBoxSSSequenceClick(object sender, EventArgs e)
		{
			txtBoxSSSequence.Focus();
		}
		
		void ChkBoxBookTitleClick(object sender, EventArgs e)
		{
			txtBoxSSBookTitle.Focus();
		}
		
		void SelectedSortDataShown(object sender, EventArgs e)
		{
			if( lvSSData.Items.Count > 0 )  btnDeleteAll.Enabled = true;
		}
		#endregion

	}
}
