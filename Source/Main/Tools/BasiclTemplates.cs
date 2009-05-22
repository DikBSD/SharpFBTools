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
		// выбранная строка с шаблонами подстановки
		private string m_sLine = null;
		
		public BasiclTemplates()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();

		}
		#region Открытые методы
		public string GetTemplateLine() {
			return m_sLine;
		}
		#endregion
		
		#region Обработчики событий
		void TvBasicTemplatesAfterSelect(object sender, TreeViewEventArgs e)
		{
			// кнопка Вставить доступна только в случае выбора шаблона
			btnInsert.Enabled = ( tvBasicTemplates.SelectedNode.GetNodeCount( true ) == 0 );
		}
		
		void BtnInsertClick(object sender, EventArgs e)
		{
			// вставка выбранного шаблона в поле шаблонов
			m_sLine = tvBasicTemplates.SelectedNode.Text;
			this.Close();
		}
		
		void BtnCollapseAllClick(object sender, EventArgs e)
		{
			tvBasicTemplates.CollapseAll();
		}
		
		void BtnExpandAllClick(object sender, EventArgs e)
		{
			tvBasicTemplates.ExpandAll();
		}
		#endregion
	}
}
