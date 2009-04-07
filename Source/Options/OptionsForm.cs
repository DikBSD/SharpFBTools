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
using System.Xml;
using System.IO;

namespace Options
{
	/// <summary>
	/// Description of OptionsForm.
	/// </summary>
	public partial class OptionsForm : Form
	{
		#region �������� �����-������ ������
		private static string m_prog_path = Environment.CurrentDirectory;
		private static string m_settings = "settings.xml";
		private static string m_sWinRarPath = "c:\\Program Files\\WinRAR\\WinRAR.exe";
		private static string m_sRarPath = "c:\\Program Files\\WinRAR\\Rar.exe";
		private static string m_sFBEPath = "c:\\Program Files\\FictionBook Editor\\FBE.exe";
		private static string m_sTFB2Path = "c:\\WINDOWS\\NOTEPAD.EXE";
		private static string m_sFBReaderPath = "c:\\Program Files\\AlReader 2\\AlReader2.exe";
		#endregion
		
		public OptionsForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			// ��-���������
			tboxWinRarPath.Text	= m_sWinRarPath;
			tboxRarPath.Text	= m_sRarPath;
			tboxFBEPath.Text	= m_sFBEPath;
			tboxTextEPath.Text	= m_sTFB2Path;
			tboxReaderPath.Text = m_sFBReaderPath;
			// ������ ����������� ���������, ���� ��� ����
			ReadSettings();
		}
		
		public static string GetProgDir() {
			return m_prog_path;
		}
		
		public static string GetSettingsPath() {
			return m_settings;
		}
		
		public static string GetDefRarPath() {
			return m_sRarPath;
		}
		
		public static string GetDefFBEPath() {
			return m_sFBEPath;
		}
		
		public static string GetDefTFB2Path() {
			return m_sTFB2Path;
		}
		
		public static string GetDefFBReaderPath() {
			return m_sFBReaderPath;
		}
		
		void ReadSettings() {
			// ������ �������� �� xml-�����
			string sSettings = GetProgDir()+"\\"+m_settings;
			if( !File.Exists( sSettings ) ) return;
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.IgnoreWhitespace = true;
			using (XmlReader reader = XmlReader.Create( sSettings, settings ) ) {
				reader.ReadToFollowing("WinRar");
				tboxWinRarPath.Text = reader.GetAttribute("WinRarPath");
				tboxRarPath.Text = reader.GetAttribute("RarPath");
				reader.ReadToFollowing("Editors");
				tboxFBEPath.Text = reader.GetAttribute("FBEPath");
				tboxTextEPath.Text = reader.GetAttribute("TextFB2EPath");
				reader.ReadToFollowing("Reader");
				tboxReaderPath.Text = reader.GetAttribute("FBReaderPath");
				reader.Close();
			}
		}
		
		#region �����������
		void BtnWinRarPathClick(object sender, EventArgs e)
		{
			// �������� ���� � WinRar
			ofDlg.Title = "������� ���� � WinRar:";
			ofDlg.FileName = "";
			ofDlg.Filter = "WinRAR.exe|*.exe|��� ����� (*.*)|*.*";
			DialogResult result = ofDlg.ShowDialog();
			if (result == DialogResult.OK) {
                tboxWinRarPath.Text = ofDlg.FileName;
            }
		}

		void BtnRarPathClick(object sender, EventArgs e)
		{
			// �������� ���� � Rar (�����������)
			ofDlg.Title = "������� ���� � Rar (�����������):";
			ofDlg.FileName = "";
			ofDlg.Filter = "Rar.exe|*.exe|��� ����� (*.*)|*.*";
			DialogResult result = ofDlg.ShowDialog();
			if (result == DialogResult.OK) {
                tboxRarPath.Text = ofDlg.FileName;
            }
		}
		
		void BtnFBEPathClick(object sender, EventArgs e)
		{
			// �������� ���� � fb2-���������
			ofDlg.Title = "������� ���� � FB2-���������:";
			ofDlg.FileName = "";
			ofDlg.Filter = "��������� (*.exe)|*.exe|��� ����� (*.*)|*.*";
			DialogResult result = ofDlg.ShowDialog();
			if (result == DialogResult.OK) {
                tboxFBEPath.Text = ofDlg.FileName;
            }
		}
		
		void BtnTextEPathClick(object sender, EventArgs e)
		{
			// �������� ���� � ���������� ��������� fb2-������
			ofDlg.Title = "������� ���� � ���������� ��������� fb2-������:";
			ofDlg.FileName = "";
			ofDlg.Filter = "��������� (*.exe)|*.exe|��� ����� (*.*)|*.*";
			DialogResult result = ofDlg.ShowDialog();
			if (result == DialogResult.OK) {
                tboxTextEPath.Text = ofDlg.FileName;
            }
		}
		
		void BtnOKClick(object sender, EventArgs e)
		{
			// ���������� �������� � ini
			XmlWriter writer = null;
			try {
				XmlWriterSettings settings = new XmlWriterSettings();
				settings.Indent = true;
				settings.IndentChars = ("\t");
				settings.OmitXmlDeclaration = true;
				
				writer = XmlWriter.Create( GetProgDir()+"\\"+m_settings, settings );
				writer.WriteStartElement( "General" );
					writer.WriteStartElement( "WinRar" );
						writer.WriteAttributeString( "WinRarPath", tboxWinRarPath.Text );
						writer.WriteAttributeString( "RarPath", tboxRarPath.Text );
						writer.WriteFullEndElement();
					writer.WriteStartElement( "Editors" );
						writer.WriteAttributeString( "FBEPath", tboxFBEPath.Text );
						writer.WriteAttributeString( "TextFB2EPath", tboxTextEPath.Text );
					writer.WriteFullEndElement();
					writer.WriteStartElement( "Reader" );
						writer.WriteAttributeString( "FBReaderPath", tboxReaderPath.Text );
					writer.WriteFullEndElement();
				writer.WriteEndElement();
				writer.Flush();
				this.Close();
			}  finally  {
				if (writer != null)
				writer.Close();
			}
		}
		
		void BtnReaderPathClick(object sender, EventArgs e)
		{
			// �������� ���� � ������� fb2-������
			ofDlg.Title = "������� ���� � ������� fb2-������:";
			ofDlg.FileName = "";
			ofDlg.Filter = "��������� (*.exe)|*.exe|��� ����� (*.*)|*.*";
			DialogResult result = ofDlg.ShowDialog();
			if (result == DialogResult.OK) {
                tboxReaderPath.Text = ofDlg.FileName;
            }
		}
		#endregion		
	}
}
