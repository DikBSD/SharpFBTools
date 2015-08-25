/*
 * Сделано в SharpDevelop.
 * Пользователь: Вадим Кузнецов (DikBSD)
 * Дата: 29.06.2015
 * Время: 13:49
 * 
 */
using System;

namespace Core.Common
{
	/// <summary>
	/// LangList: Списков языков написания fb2 книги
	/// </summary>
	public class LangList
	{
		private static readonly string[] m_Langs = {
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
		
		public LangList()
		{
		}
		
		public static string[] LangsList {
			get {
				return m_Langs;
			}
		}
	}
}
