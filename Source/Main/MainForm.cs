/*
 * Created by SharpDevelop.
 * User: DikBSD
 * Date: 13.03.2009
 * Time: 13:41
 * 
 * License: GPL 2.1
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SharpFBTools.Controls.Panels;

namespace Main
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		private List<ToolStripItem> m_listToggleBtn = new List<ToolStripItem>();
		private SFBTpValidator sfbTpValidator = new SFBTpValidator();
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			// имплантируемые контролы
			// валидатор //
			this.tscMain.ContentPanel.Controls.Add(this.sfbTpValidator);
			this.sfbTpValidator.Dock = System.Windows.Forms.DockStyle.Fill;
		}
	}
	
	private MakeToolBarGroupButtonList() {
		for( int i=0; i!= tsMain.Items.Count; ++i ) {
			if( tsMain.Items[i].Tag=="group" ) {
				m_listToggleBtn.Add( tsMain.Items[i] );
			}
		}
	}
	
}
