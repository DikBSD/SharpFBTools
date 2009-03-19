/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 16.03.2009
 * Time: 9:55
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
	/// Description of SFBTpArchiveManager.
	/// </summary>
	public partial class SFBTpArchiveManager : UserControl
	{
		public SFBTpArchiveManager()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			// путь к папке с Rar`м
			tboxRarDir.Text = "c:\\Program Files\\WinRAR";
		}
		
		void TsbtnOpenDirClick(object sender, EventArgs e)
		{
			// задание папки с fb2-файлами для сканирования
			fbdDir.Description = "Укажите папку для архивирования fb2-файлов";
			DialogResult result = fbdDir.ShowDialog();
			if (result == DialogResult.OK) {
                string openFolderName = fbdDir.SelectedPath;
                tboxSourceDir.Text = openFolderName;
            }
		}
		
		void RbtnRarCheckedChanged(object sender, EventArgs e)
		{
			cboxAddRestoreInfo.Enabled = rbtnRar.Checked;
			gboxRar.Visible = rbtnRar.Checked;
		}
		
		void RbtnToAnotherDirCheckedChanged(object sender, EventArgs e)
		{
			btnToAnotherDir.Enabled = rbtnToAnotherDir.Checked;
		}
		
		void BtnToAnotherDirClick(object sender, EventArgs e)
		{
			// задание папки для копирования запакованных fb2-файлов
			fbdDir.Description = "Укажите папку для копирования упакованных fb2-файлов";
			DialogResult result = fbdDir.ShowDialog();
			if (result == DialogResult.OK) {
                string openFolderName = fbdDir.SelectedPath;
                tboxToAnotherDir.Text = openFolderName;
            }
		}
		
		void BtnRarDirClick(object sender, EventArgs e)
		{
			// задание папки для установленного в системе WinRar`a
			fbdDir.Description = "Укажите папку, где установлен Rar-архиватор";
			DialogResult result = fbdDir.ShowDialog();
			if (result == DialogResult.OK) {
                string openFolderName = fbdDir.SelectedPath;
                tboxRarDir.Text = openFolderName;
            }
		}
		
		void TsbtnArchiveClick(object sender, EventArgs e)
		{
			// Запаковка fb2-файлов
			if( tboxSourceDir.Text == "" ) {
				MessageBox.Show( "Выберите папку для сканирования!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			DirectoryInfo diFolder = new DirectoryInfo(tboxSourceDir.Text);
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка не найдена:" + tboxSourceDir.Text, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
		}
	}
}
