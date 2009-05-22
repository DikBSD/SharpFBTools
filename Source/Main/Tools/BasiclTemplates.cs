/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 21.05.2009
 * Time: 13:36
 * 
 * License: GPL 2.1
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace SharpFBTools.Tools
{
	/// <summary>
	/// Description of BasiclTemplates.
	/// </summary>
	public partial class BasiclTemplates : Form
	{
		private string m_sLine = "";
		
		public BasiclTemplates()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();

		}
		
		void TvBasicTemplatesAfterSelect(object sender, TreeViewEventArgs e)
		{
			// кнопка Вставить доступна только в случае выбора шаблона
			btnInsert.Enabled = ( tvBasicTemplates.SelectedNode.GetNodeCount( true ) == 0 );
		}
		
		void BtnInsertClick(object sender, EventArgs e)
		{
			// вставка выбранного шаблона в поле шаблонов
			m_sLine = tvBasicTemplates.SelectedNode.Text;
		}
	}
}
