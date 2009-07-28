/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 14.05.2009
 * Time: 16:26
 * 
 * License: GPL 2.1
 */
using System;

namespace Templates.Lexems {
	/// <summary>
	/// Description of AllTemplates
	/// </summary>
	public class AllTemplates {
		protected static readonly string[] m_sAllTemplates = new string[] {
								"*LBAL*","*L*","*G*","*BAF*","*BAM*","*BAL*","*BAN*","*BT*","*SN*","*SI*","*GG*","*SII*","*SIII*",
								};
		public AllTemplates() {
			
		}
	}
	
	/// <summary>
	/// Description of Templates
	/// </summary>
	public class Templates : AllTemplates {
		public Templates() {
			
		}
		// постоянные шаблоны
		public string LBAL {
            get { return m_sAllTemplates[0]; }
        }
		public string L {
			get { return m_sAllTemplates[1]; }
        }
		public string G {
            get { return m_sAllTemplates[2]; }
        }
		public string BAF {
            get { return m_sAllTemplates[3]; }
        }
		public string BAM {
            get { return m_sAllTemplates[4]; }
        }
		public string BAL {
            get { return m_sAllTemplates[5]; }
        }
		public string BAN {
            get { return m_sAllTemplates[6]; }
        }
		public string BT {
            get { return m_sAllTemplates[7]; }
        }
		public string SN {
            get { return m_sAllTemplates[8]; }
        }
		public string SI {
            get { return m_sAllTemplates[9]; }
        }
		public string GG {
            get { return m_sAllTemplates[10]; }
        }
		public string SII {
            get { return m_sAllTemplates[11]; }
        }
		public string SIII {
            get { return m_sAllTemplates[12]; }
        }
	}
	
	/// <summary>
	/// Description of SimpleType
	/// </summary>
	public enum SimpleType {
		const_template,			// постоянный шаблон
		const_text, 			// постоянные символы
		conditional_template,	// условный шаблон
		conditional_group, 		// условная группа
	}
	
	/// <summary>
	/// Description of ComplexType
	/// </summary>
	public enum ComplexType {
		template,	// постоянный шаблон
		text, 		// постоянные символы
	}
	
	/// <summary>
	/// Description of TPSimple
	/// </summary>
	public class TPSimple {
		private string		m_sLexem	= "";
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
	/// Description of TPComplex
	/// </summary>
	public class TPComplex {
		private string		m_sLexem	= "";
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
