/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 16.03.2009
 * Time: 10:04
 * 
 * License: GPL 2.1
 */

using System;
using System.IO;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace SharpFBTools.Controls.Panels
{
	/// <summary>
	/// Description of SFBTpAbout.
	/// </summary>
	public partial class SFBTpAbout : UserControl
	{
		public SFBTpAbout()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			
			// загрузка файла Лицензии
			if( File.Exists( "License GPL 2.1.rtf" ) ) {
				rtboxLicense.LoadFile( "License GPL 2.1.rtf" );
			} else {
				rtboxLicense.Text = "Не найден файл лицензии: \"License GPL 2.1.rtf\"";
			}
		}
	}
}
