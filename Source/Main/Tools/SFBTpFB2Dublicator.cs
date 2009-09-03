/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 16.03.2009
 * Time: 10:03
 * 
 * License: GPL 2.1
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

using Core.FB2Dublicator;

using fB2Parser = Core.FB2.FB2Parsers.FB2Parser;

namespace SharpFBTools.Tools
{
	/// <summary>
	/// Description of SFBTpFB2Dublicator.
	/// </summary>
	public partial class SFBTpFB2Dublicator : UserControl
	{
		public SFBTpFB2Dublicator()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			cboxMode.SelectedIndex = 0; // Условия для Сравнения fb2-файлов: Автор(ы) и Название Книги
		}

		
		void TsbtnSearchDublsClick(object sender, EventArgs e)
		{
			try {
				fB2Parser fb2_1 = new fB2Parser( "c:\\Temp\\_Test\\01.fb2" );
				fB2Parser fb2_2 = new fB2Parser( "c:\\Temp\\_Test\\02.fb2" );
				Fb2Comparer fb2c = new Fb2Comparer( fb2_1.GetDescription(), fb2_2.GetDescription() );
				bool bId		= fb2c.IsIdEquality();
				bool bBookTitle	= fb2c.IsBookTitleEquality();
			} catch {
				MessageBox.Show( "catch!!!", "!!!!!!!!!!!!!", MessageBoxButtons.OK, MessageBoxIcon.Warning );
			}
			
		}
	}
}
