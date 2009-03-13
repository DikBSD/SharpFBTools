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
		private SFBTpValidator sfbTpValidator1 = new SFBTpValidator();
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			// имплантируемые контролы
			this.tscMain.ContentPanel.Controls.Add(this.sfbTpValidator1);
			this.sfbTpValidator1.Dock = System.Windows.Forms.DockStyle.Fill;
		}
	}
}
