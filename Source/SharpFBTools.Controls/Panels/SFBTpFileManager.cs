/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 16.03.2009
 * Time: 9:03
 * 
 * License: GPL 2.1
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace SharpFBTools.Controls.Panels
{
	/// <summary>
	/// Description of SFBTpFileManager.
	/// </summary>
	public partial class SFBTpFileManager : UserControl
	{
		public SFBTpFileManager()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			cboxTemplatesPrepared.SelectedIndex = 0;
			
			string sTDPath = Settings.Settings.GetDefFMDescTemplatePath();
			if( File.Exists( sTDPath ) ) {
				richTxtBoxDescTemplates.LoadFile( sTDPath );
			} else {
				richTxtBoxDescTemplates.Text = "Не найден файл описания Шаблонов переименования: \""+sTDPath+"\"";
			}
		}
		
		#region Обработчики событий
		void TsbtnOpenDirClick(object sender, EventArgs e)
		{
			// задание папки с fb2-файлами и архивами для сканирования
			if( tboxSourceDir.Text !="" ) {
				fbdScanDir.SelectedPath = tboxSourceDir.Text;
			}
			DialogResult result = fbdScanDir.ShowDialog();
			if (result == DialogResult.OK) {
				string openFolderName = fbdScanDir.SelectedPath;
                tboxSourceDir.Text = openFolderName;
            }
		}
		
		void RBtnTemplatesFromLineCheckedChanged(object sender, EventArgs e)
		{
			txtBoxTemplatesFromLine.Enabled = rBtnTemplatesFromLine.Checked;
		}
		
		void RBtnTemplatesPreparedCheckedChanged(object sender, EventArgs e)
		{
			cboxTemplatesPrepared.Enabled = rBtnTemplatesPrepared.Checked;
		}
		#endregion
	}
}
