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
		public OptionsForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			// ��-���������
			// �����
			tboxWinRarPath.Text	= Settings.Settings.GetDefWinRARPath();
			tboxRarPath.Text	= Settings.Settings.GetDefRarPath();
			tboxUnRarPath.Text	= Settings.Settings.GetDefUnRARPath();
			tbox7zaPath.Text	= Settings.Settings.GetDef7zaPath();
			tboxFBEPath.Text	= Settings.Settings.GetDefFBEPath();
			tboxTextEPath.Text	= Settings.Settings.GetDefTFB2Path();
			tboxReaderPath.Text = Settings.Settings.GetDefFBReaderPath();
			// ���������
			cboxValidatorForFB2.SelectedIndex = Settings.Settings.GetDefValidatorFB2SelectedIndex();
			cboxValidatorForFB2Archive.SelectedIndex = Settings.Settings.GetDefValidatorFB2ArchiveSelectedIndex();
			cboxValidatorForFB2PE.SelectedIndex = Settings.Settings.GetDefValidatorFB2SelectedIndexPE();
			cboxValidatorForFB2ArchivePE.SelectedIndex = Settings.Settings.GetDefValidatorFB2ArchiveSelectedIndexPE();
			// �������� ������
			chBoxTranslit.Checked = Settings.Settings.GetDefFMchBoxTranslitCheked();
			chBoxStrict.Checked = Settings.Settings.GetDefFMchBoxStrictCheked();
			cboxSpace.SelectedIndex = Settings.Settings.GetDefFMcboxSpaceSelectedIndex();
			chBoxToArchive.Checked = Settings.Settings.GetDefFMchBoxToArchiveCheked();
			cboxArchiveType.SelectedIndex = Settings.Settings.GetDefFMcboxArchiveTypeSelectedIndex();
			cboxFileExist.SelectedIndex = Settings.Settings.GetDefFMcboxFileExistSelectedIndex();
			chBoxDelFB2Files.Checked = Settings.Settings.GetDefFMchBoxDelFB2FilesCheked();
			rbtnAsIs.Checked = Settings.Settings.GetDefFMrbtnAsIsCheked();
			rbtnLower.Checked = Settings.Settings.GetDefFMrbtnLowerCheked();
			rbtnUpper.Checked = Settings.Settings.GetDefFMrbtnUpperCheked();
			rbtnGenreOne.Checked = Settings.Settings.GetDefFMrbtnGenreOneCheked();
			rbtnAuthorOne.Checked = Settings.Settings.GetDefFMrbtnAuthorOneCheked();
			// ������ ����������� ���������, ���� ��� ����
			ReadSettings();
		}
		
		void ReadSettings() {
			// ������ �������� �� xml-�����
			#region ���
			string sSettings = Settings.Settings.GetSettingsPath();
			if( !File.Exists( sSettings ) ) return;
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.IgnoreWhitespace = true;
			using ( XmlReader reader = XmlReader.Create( sSettings, settings ) ) {
				reader.ReadToFollowing("WinRar");
				if (reader.HasAttributes ) {
					tboxWinRarPath.Text = reader.GetAttribute("WinRarPath");
					tboxRarPath.Text = reader.GetAttribute("RarPath");
				}
				reader.ReadToFollowing("A7za");
				if (reader.HasAttributes ) {
					tbox7zaPath.Text = reader.GetAttribute("A7zaPath");
				}
				reader.ReadToFollowing("Editors");
				if (reader.HasAttributes ) {
					tboxFBEPath.Text = reader.GetAttribute("FBEPath");
					tboxTextEPath.Text = reader.GetAttribute("TextFB2EPath");
				}
				reader.ReadToFollowing("Reader");
				if (reader.HasAttributes ) {
					tboxReaderPath.Text = reader.GetAttribute("FBReaderPath");
				}
				reader.ReadToFollowing("ValidatorDoubleClick");
				if (reader.HasAttributes ) {
					cboxValidatorForFB2.SelectedIndex = Convert.ToInt16( reader.GetAttribute("cboxValidatorForFB2SelectedIndex") );
					cboxValidatorForFB2Archive.SelectedIndex = Convert.ToInt16( reader.GetAttribute("cboxValidatorForFB2ArchiveSelectedIndex") );
				}
				reader.ReadToFollowing("ValidatorPressEnter");
				if (reader.HasAttributes ) {
					cboxValidatorForFB2PE.SelectedIndex = Convert.ToInt16( reader.GetAttribute("cboxValidatorForFB2SelectedIndexPE") );
					cboxValidatorForFB2ArchivePE.SelectedIndex = Convert.ToInt16( reader.GetAttribute("cboxValidatorForFB2ArchiveSelectedIndexPE") );
				}
				reader.Close();
			}
			#endregion
		}
		
		#region �����������
				
		void BtnOKClick(object sender, EventArgs e)
		{
			// ���������� �������� � ini
			// ������������� ������� ����� - ����� ���������
			Environment.CurrentDirectory = Settings.Settings.GetProgDir();
			XmlWriter writer = null;
			try {
				XmlWriterSettings settings = new XmlWriterSettings();
				settings.Indent = true;
				settings.IndentChars = ("\t");
				settings.OmitXmlDeclaration = true;
				
				writer = XmlWriter.Create( Settings.Settings.GetSettingsPath(), settings );
				writer.WriteStartElement( "SharpFBTools" );
					writer.WriteStartElement( "General" );
						writer.WriteStartElement( "WinRar" );
							writer.WriteAttributeString( "WinRarPath", tboxWinRarPath.Text );
							writer.WriteAttributeString( "RarPath", tboxRarPath.Text );
						writer.WriteFullEndElement();
						writer.WriteStartElement( "7za" );
							writer.WriteAttributeString( "7zaPath", tbox7zaPath.Text );
						writer.WriteFullEndElement();
						writer.WriteStartElement( "Editors" );
							writer.WriteAttributeString( "FBEPath", tboxFBEPath.Text );
							writer.WriteAttributeString( "TextFB2EPath", tboxTextEPath.Text );
						writer.WriteFullEndElement();
						writer.WriteStartElement( "Reader" );
							writer.WriteAttributeString( "FBReaderPath", tboxReaderPath.Text );
						writer.WriteFullEndElement();
					writer.WriteEndElement();
					writer.WriteStartElement( "FB2Validator" );
						writer.WriteStartElement( "ValidatorDoubleClick" );
							writer.WriteAttributeString( "cboxValidatorForFB2SelectedIndex", cboxValidatorForFB2.SelectedIndex.ToString() );
							writer.WriteAttributeString( "cboxValidatorForFB2ArchiveSelectedIndex", cboxValidatorForFB2Archive.SelectedIndex.ToString() );
						writer.WriteFullEndElement();
						writer.WriteStartElement( "ValidatorPressEnter" );
							writer.WriteAttributeString( "cboxValidatorForFB2SelectedIndexPE", cboxValidatorForFB2PE.SelectedIndex.ToString() );
							writer.WriteAttributeString( "cboxValidatorForFB2ArchiveSelectedIndexPE", cboxValidatorForFB2ArchivePE.SelectedIndex.ToString() );
						writer.WriteFullEndElement();
					writer.WriteEndElement();
				writer.WriteEndElement();
				writer.Flush();
			}  finally  {
				if (writer != null)
				writer.Close();
				this.Close();
			}
		}
		#region �����
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
		
		void BtnUnRarPathClick(object sender, EventArgs e)
		{
			// �������� ���� � UnRar (�����������)
			ofDlg.Title = "������� ���� � UnRar (�����������):";
			ofDlg.FileName = "";
			ofDlg.Filter = "UnRar.exe|*.exe|��� ����� (*.*)|*.*";
			DialogResult result = ofDlg.ShowDialog();
			if (result == DialogResult.OK) {
                tboxUnRarPath.Text = ofDlg.FileName;
            }
		}
		
		void Btn7zaPathClick(object sender, EventArgs e)
		{
			// �������� ���� � 7za (�����������)
			ofDlg.Title = "������� ���� � 7za (�����������):";
			ofDlg.FileName = "";
			ofDlg.Filter = "7za.exe|*.exe|��� ����� (*.*)|*.*";
			DialogResult result = ofDlg.ShowDialog();
			if (result == DialogResult.OK) {
                tbox7zaPath.Text = ofDlg.FileName;
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

		#region �������� ������
		void CboxFileExistSelectedIndexChanged(object sender, EventArgs e)
		{
			chBoxAddToFileNameBookID.Visible = cboxFileExist.SelectedIndex == 1;
		}
		
		void ChBoxToArchiveCheckedChanged(object sender, EventArgs e)
		{
			cboxArchiveType.Enabled = chBoxToArchive.Checked;
		}		
		#endregion
		
		#endregion
	}
}
