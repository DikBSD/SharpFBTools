/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 14.05.2009
 * Time: 16:26
 * 
 * License: GPL 2.1
 */
using System;

namespace Core.FileManager.Templates.Lexems {
	/// <summary>
	/// AllTemplates: Все допустимые шаблоны
	/// </summary>
	public class AllTemplates {
		protected static readonly string[] m_sAllTemplates = new string[] {
								"*LBAL*","*L*","*G*","*BAF*","*BAM*","*BAL*","*BAN*",
								"*BT*","*SN*","*SI*","*GG*","*SII*","*SIII*","*DT*","DV",
								"*LF*","*LM*","*LL*","*LN*",
								"*YEAR*","PUB","*CITY*",
								"*FB2AF*","*FB2AM*","*FB2AL*","*FB2AN*",
								"*GROUP*","*FILENAME*","*COUNTER*","*LBAL_OR_LBAN*", "*LL_OR_LN*"
								};
		public AllTemplates() {
		}
		public static string[] Templates {
            get { return m_sAllTemplates; }
        }
	}
	
	/// <summary>
	/// Description of Templates
	/// </summary>
	public class Templates : AllTemplates {
		public Templates() {
			
		}
		// постоянные шаблоны
		public static string LBAL {
            get { return m_sAllTemplates[0]; }
        }
		public static string L {
			get { return m_sAllTemplates[1]; }
        }
		public static string G {
            get { return m_sAllTemplates[2]; }
        }
		public static string BAF {
            get { return m_sAllTemplates[3]; }
        }
		public static string BAM {
            get { return m_sAllTemplates[4]; }
        }
		public static string BAL {
            get { return m_sAllTemplates[5]; }
        }
		public static string BAN {
            get { return m_sAllTemplates[6]; }
        }
		public static string BT {
            get { return m_sAllTemplates[7]; }
        }
		public static string SN {
            get { return m_sAllTemplates[8]; }
        }
		public static string SI {
            get { return m_sAllTemplates[9]; }
        }
		public static string GG {
            get { return m_sAllTemplates[10]; }
        }
		public static string SII {
            get { return m_sAllTemplates[11]; }
        }
		public static string SIII {
            get { return m_sAllTemplates[12]; }
        }
		public static string DT {
            get { return m_sAllTemplates[13]; }
        }
		public static string DV {
            get { return m_sAllTemplates[14]; }
        }
		public static string LF {
            get { return m_sAllTemplates[15]; }
        }
		public static string LM {
            get { return m_sAllTemplates[16]; }
        }
		public static string LL {
            get { return m_sAllTemplates[17]; }
        }
		public static string LN {
            get { return m_sAllTemplates[18]; }
        }
		public static string YEAR {
            get { return m_sAllTemplates[19]; }
        }
		public static string PUB {
            get { return m_sAllTemplates[20]; }
        }
		public static string CITY {
            get { return m_sAllTemplates[21]; }
        }
		public static string FB2AF {
            get { return m_sAllTemplates[22]; }
        }
		public static string FB2AM {
            get { return m_sAllTemplates[23]; }
        }
		public static string FB2AL {
            get { return m_sAllTemplates[24]; }
        }
		public static string FB2AN {
            get { return m_sAllTemplates[25]; }
        }
		public static string GROUP {
            get { return m_sAllTemplates[26]; }
        }
		public static string FILENAME {
            get { return m_sAllTemplates[27]; }
        }
		public static string COUNTER {
            get { return m_sAllTemplates[28]; }
        }
		public static string LBAL_OR_LBAN {
            get { return m_sAllTemplates[29]; }
        }
		public static string LL_OR_LN {
            get { return m_sAllTemplates[30]; }
        }
	}
	
	/// <summary>
	/// SimpleType
	/// </summary>
	public enum SimpleType {
		const_template,			// постоянный шаблон *BT*, *L*
		const_text, 			// постоянные символы (\, -, #)
		conditional_template,	// условный шаблон [*BT*]
		conditional_group, 		// условная группа [ *BAF*], [*LL* - ]
	}
	
	/// <summary>
	/// ComplexType
	/// </summary>
	public enum ComplexType {
		template,	// постоянный шаблон
		text, 		// постоянные символы
	}
	
	/// <summary>
	/// TPSimple
	/// </summary>
	public class TPSimple {
		private string		m_sLexem	= string.Empty;
		private SimpleType	m_Type		= SimpleType.const_template;
		public TPSimple( string sLexem, SimpleType Type )
		{
			m_sLexem	= sLexem;
			m_Type		= Type;
		}
		public virtual string Lexem {
            get { return m_sLexem; }
        }
		public virtual SimpleType Type {
            get { return m_Type; }
        }
	}
	
	/// <summary>
	/// TPComplex
	/// </summary>
	public class TPComplex {
		private string		m_sLexem	= string.Empty;
		private ComplexType	m_bType		= ComplexType.template;
		public TPComplex( string sLexem, ComplexType Type )
		{
			m_sLexem	= sLexem;
			m_bType		= Type;
		}
		public virtual string Lexem {
            get { return m_sLexem; }
            set { m_sLexem = value; }
        }
		public virtual ComplexType Type {
            get { return m_bType; }
        }
	}
}
