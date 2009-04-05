/*
 * Created by SharpDevelop.
 * User: DikBSD
 * Date: 05.04.2009
 * Time: 14:31
 * 
 * License: GPL 2.1
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Options
{
	/// <summary>
	/// Description of OptionsForm.
	/// </summary>
	public partial class OptionsForm : Form
	{
		#region Закрытые члены-данные класса
		private string m_sRarDir = "c:\\Program Files\\WinRAR\\WinRAR.exe";
		private string m_sFBEDir = "c:\\Program Files\\FictionBook Editor\\FBE.exe";
		private string m_sTFB2Dir = "c:\\WINDOWS\\NOTEPAD.EXE";
		#endregion
		
		public OptionsForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			// по-умолчанию
			tboxRarPath.Text = m_sRarDir;
			tboxFBEPath.Text = m_sFBEDir;
			tboxTextEPath.Text = m_sTFB2Dir;
		}
		
		void BtnRarPathClick(object sender, EventArgs e)
		{
			// указание пути к WinRar
			ofDlg.Title = "Укажите путь к WinRar:";
			ofDlg.FileName = "";
			ofDlg.Filter = "WinRAR.exe|*.exe|Все файлы (*.*)|*.*";
			DialogResult result = ofDlg.ShowDialog();
			if (result == DialogResult.OK) {
                tboxRarPath.Text = ofDlg.FileName;
            }
		}
		
		void BtnFBEPathClick(object sender, EventArgs e)
		{
			// указание пути к fb2-редактору
			ofDlg.Title = "Укажите путь к FB2-Редактору:";
			ofDlg.FileName = "";
			ofDlg.Filter = "Программы (*.exe)|*.exe|Все файлы (*.*)|*.*";
			DialogResult result = ofDlg.ShowDialog();
			if (result == DialogResult.OK) {
                tboxFBEPath.Text = ofDlg.FileName;
            }
		}
		
		void BtnTextEPathClick(object sender, EventArgs e)
		{
			// указание пути к Текстовому Редактору fb2-файлов
			ofDlg.Title = "Укажите путь к Текстовому Редактору fb2-файлов:";
			ofDlg.FileName = "";
			ofDlg.Filter = "Программы (*.exe)|*.exe|Все файлы (*.*)|*.*";
			DialogResult result = ofDlg.ShowDialog();
			if (result == DialogResult.OK) {
                tboxTextEPath.Text = ofDlg.FileName;
            }
		}
		
		void BtnOKClick(object sender, EventArgs e)
		{
			// сохранение настроек в ini
		}
	}
}
