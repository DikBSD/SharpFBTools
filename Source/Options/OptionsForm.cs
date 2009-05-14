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
			#region ��� ������������
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
			rbtnFMFB21.Checked = Settings.Settings.GetDefFMrbtnGenreFB21Cheked();
			rbtnFMFB22.Checked = Settings.Settings.GetDefFMrbtnGenreFB22Cheked();
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
			rbtnGenreAll.Checked = Settings.Settings.GetDefFMrbtnGenreAllCheked();
			rbtnAuthorOne.Checked = Settings.Settings.GetDefFMrbtnAuthorOneCheked();
			rbtnAuthorAll.Checked = Settings.Settings.GetDefFMrbtnAuthorAllCheked();
			rbtnGenreSchema.Checked = Settings.Settings.GetDefFMrbtnGenreSchemaCheked();
			rbtnGenreText.Checked = Settings.Settings.GetDefFMrbtnGenreTextCheked();
			chBoxAddToFileNameBookID.Checked = Settings.Settings.GetDefFMchBoxAddToFileNameBookIDChecked();
			txtBoxFB2NotReadDir.Text = Settings.Settings.GetDefFMFB2NotReadDir();
			txtBoxFB2LongPathDir.Text = Settings.Settings.GetDefFMFB2LongPathDir();
			// ������ ����������� ���������, ���� ��� ����
			ReadSettings();
			#endregion
		}
		
		#region ��������������� ������
		void ReadSettings() {
			// ������ �������� �� xml-�����
			#region ���
			string sSettings = Settings.Settings.GetSettingsPath();
			if( !File.Exists( sSettings ) ) return;
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.IgnoreWhitespace = true;
			using ( XmlReader reader = XmlReader.Create( sSettings, settings ) ) {
				// ����� 
				try {
					reader.ReadToFollowing("WinRar");
					if (reader.HasAttributes ) {
						tboxWinRarPath.Text = reader.GetAttribute("WinRarPath");
						tboxRarPath.Text = reader.GetAttribute("RarPath");
						tboxUnRarPath.Text = reader.GetAttribute("UnRarPath");
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
					// ���������
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
					// �������� ������
					reader.ReadToFollowing("Register");
					if (reader.HasAttributes ) {
						rbtnAsIs.Checked = Convert.ToBoolean( reader.GetAttribute("rbtnAsIsChecked") );
						rbtnLower.Checked = Convert.ToBoolean( reader.GetAttribute("rbtnLowerChecked") );
						rbtnUpper.Checked = Convert.ToBoolean( reader.GetAttribute("rbtnUpperChecked") );
					}
					reader.ReadToFollowing("Translit");
					if (reader.HasAttributes ) {
						chBoxTranslit.Checked = Convert.ToBoolean( reader.GetAttribute("chBoxTranslitChecked") );
					}
					reader.ReadToFollowing("Strict");
					if (reader.HasAttributes ) {
						chBoxStrict.Checked = Convert.ToBoolean( reader.GetAttribute("chBoxStrictChecked") );
					}
					reader.ReadToFollowing("Space");
					if (reader.HasAttributes ) {
						cboxSpace.SelectedIndex = Convert.ToInt16( reader.GetAttribute("cboxSpaceSelectedIndex") );
					}
					reader.ReadToFollowing("Archive");
					if (reader.HasAttributes ) {
						chBoxToArchive.Checked = Convert.ToBoolean( reader.GetAttribute("chBoxToArchiveChecked") );
						cboxArchiveType.SelectedIndex = Convert.ToInt16( reader.GetAttribute("cboxArchiveTypeSelectedIndex") );
					}
					reader.ReadToFollowing("IsFileExist");
					if (reader.HasAttributes ) {
						cboxFileExist.SelectedIndex = Convert.ToInt16( reader.GetAttribute("cboxFileExistSelectedIndex") );
					}
					reader.ReadToFollowing("AddToFileNameBookID");
					if (reader.HasAttributes ) {
						chBoxAddToFileNameBookID.Checked = Convert.ToBoolean( reader.GetAttribute("chBoxAddToFileNameBookIDChecked") );
					}
					reader.ReadToFollowing("FileDelete");
					if (reader.HasAttributes ) {
						chBoxDelFB2Files.Checked = Convert.ToBoolean( reader.GetAttribute("chBoxDelFB2FilesChecked") );
					}
					reader.ReadToFollowing("AuthorsToDirs");
					if (reader.HasAttributes ) {
						rbtnAuthorOne.Checked = Convert.ToBoolean( reader.GetAttribute("rbtnAuthorOneChecked") );
						rbtnAuthorAll.Checked = Convert.ToBoolean( reader.GetAttribute("rbtnAuthorAllChecked") );
					}
					reader.ReadToFollowing("GenresToDirs");
					if (reader.HasAttributes ) {
						rbtnGenreOne.Checked = Convert.ToBoolean( reader.GetAttribute("rbtnGenreOneChecked") );
						rbtnGenreAll.Checked = Convert.ToBoolean( reader.GetAttribute("rbtnGenreAllChecked") );
					}
					reader.ReadToFollowing("GenresType");
					if (reader.HasAttributes ) {
						rbtnGenreSchema.Checked = Convert.ToBoolean( reader.GetAttribute("rbtnGenreSchemaChecked") );
						rbtnGenreText.Checked = Convert.ToBoolean( reader.GetAttribute("rbtnGenreTextChecked") );
					}
					reader.ReadToFollowing("FMGenresScheme");
					if (reader.HasAttributes ) {
						rbtnFMFB21.Checked = Convert.ToBoolean( reader.GetAttribute("rbtnFMFB21Checked") );
						rbtnFMFB22.Checked = Convert.ToBoolean( reader.GetAttribute("rbtnFMFB22Checked") );
					}
					reader.ReadToFollowing("FB2NotReadDir");
					if (reader.HasAttributes ) {
						txtBoxFB2NotReadDir.Text = reader.GetAttribute("txtBoxFB2NotReadDir");
					}
					reader.ReadToFollowing("FB2LongPathDir");
					if (reader.HasAttributes ) {
						txtBoxFB2LongPathDir.Text = reader.GetAttribute("txtBoxFB2LongPathDir");
					}
				} catch {
					MessageBox.Show( "��������� ���� ��������: \""+Settings.Settings.GetSettingsPath()+"\".\n������� ���, �� ��������� ������������� ��� ���������� ��������", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				} finally {
					reader.Close();
				}
			}
			#endregion
		}
		
		#endregion
		
		#region �����������
				
		void BtnOKClick(object sender, EventArgs e)
		{
			// ���������� �������� � ini
			#region ���
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
					// �����
					writer.WriteStartElement( "General" );
						writer.WriteStartElement( "WinRar" );
							writer.WriteAttributeString( "WinRarPath", tboxWinRarPath.Text );
							writer.WriteAttributeString( "RarPath", tboxRarPath.Text );
							writer.WriteAttributeString( "UnRarPath", tboxUnRarPath.Text );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "A7za" );
							writer.WriteAttributeString( "A7zaPath", tbox7zaPath.Text );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "Editors" );
							writer.WriteAttributeString( "FBEPath", tboxFBEPath.Text );
							writer.WriteAttributeString( "TextFB2EPath", tboxTextEPath.Text );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "Reader" );
							writer.WriteAttributeString( "FBReaderPath", tboxReaderPath.Text );
						writer.WriteFullEndElement();
					writer.WriteEndElement();
					
					// ���������
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
					
					// �������� ������
					writer.WriteStartElement( "FileManager" );
						writer.WriteStartElement( "Register" );
							writer.WriteAttributeString( "rbtnAsIsChecked", rbtnAsIs.Checked.ToString() );
							writer.WriteAttributeString( "rbtnLowerChecked", rbtnLower.Checked.ToString() );
							writer.WriteAttributeString( "rbtnUpperChecked", rbtnUpper.Checked.ToString() );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "Translit" );
							writer.WriteAttributeString( "chBoxTranslitChecked", chBoxTranslit.Checked.ToString() );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "Strict" );
							writer.WriteAttributeString( "chBoxStrictChecked", chBoxStrict.Checked.ToString() );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "Space" );
							writer.WriteAttributeString( "cboxSpaceSelectedIndex", cboxSpace.SelectedIndex.ToString() );
							writer.WriteAttributeString( "cboxSpaceText", cboxSpace.Text.ToString() );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "Archive" );
							writer.WriteAttributeString( "chBoxToArchiveChecked", chBoxToArchive.Checked.ToString() );
							writer.WriteAttributeString( "cboxArchiveTypeSelectedIndex", cboxArchiveType.SelectedIndex.ToString() );
							writer.WriteAttributeString( "cboxArchiveTypeText", cboxArchiveType.Text.ToString() );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "IsFileExist" );
							writer.WriteAttributeString( "cboxFileExistSelectedIndex", cboxFileExist.SelectedIndex.ToString() );
							writer.WriteAttributeString( "cboxFileExistText", cboxFileExist.Text.ToString() );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "AddToFileNameBookID" );
							writer.WriteAttributeString( "chBoxAddToFileNameBookIDChecked", chBoxAddToFileNameBookID.Checked.ToString() );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "FileDelete" );
							writer.WriteAttributeString( "chBoxDelFB2FilesChecked", chBoxDelFB2Files.Checked.ToString() );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "AuthorsToDirs" );
							writer.WriteAttributeString( "rbtnAuthorOneChecked", rbtnAuthorOne.Checked.ToString() );
							writer.WriteAttributeString( "rbtnAuthorAllChecked", rbtnAuthorAll.Checked.ToString() );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "GenresToDirs" );
							writer.WriteAttributeString( "rbtnGenreOneChecked", rbtnGenreOne.Checked.ToString() );
							writer.WriteAttributeString( "rbtnGenreAllChecked", rbtnGenreAll.Checked.ToString() );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "GenresType" );
							writer.WriteAttributeString( "rbtnGenreSchemaChecked", rbtnGenreSchema.Checked.ToString() );
							writer.WriteAttributeString( "rbtnGenreTextChecked", rbtnGenreText.Checked.ToString() );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "FMGenresScheme" );
							writer.WriteAttributeString( "rbtnFMFB21Checked", rbtnFMFB21.Checked.ToString() );
							writer.WriteAttributeString( "rbtnFMFB22Checked", rbtnFMFB22.Checked.ToString() );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "FB2NotReadDir" );
							writer.WriteAttributeString( "txtBoxFB2NotReadDir", txtBoxFB2NotReadDir.Text );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "FB2LongPathDir" );
							writer.WriteAttributeString( "txtBoxFB2LongPathDir", txtBoxFB2LongPathDir.Text );
						writer.WriteFullEndElement();
						
					writer.WriteEndElement();
					
				writer.WriteEndElement();
				writer.Flush();
			}  finally  {
				if (writer != null)
				writer.Close();
				this.Close();
			}
			#endregion
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
			chBoxAddToFileNameBookID.Visible = cboxFileExist.SelectedIndex != 0;
			if( cboxFileExist.SelectedIndex == 0 ) {
				chBoxAddToFileNameBookID.Checked = false;
			}
		}
		
		void ChBoxToArchiveCheckedChanged(object sender, EventArgs e)
		{
			cboxArchiveType.Enabled = chBoxToArchive.Checked;
		}
		
		void BtnFB2NotReadDirClick(object sender, EventArgs e)
		{
			// �������� ���� � ����� ��� ���������� fb2-������
			FilesWorker.FilesWorker.OpenDirDlg( txtBoxFB2NotReadDir, fbdDir, "������� ����� ��� ���������� fb2-������:" );
		}
		
		void BtnFB2LongPathDirClick(object sender, EventArgs e)
		{
			// �������� ���� � ����� ��� fb2-������ � ���������������� �������� �������
			FilesWorker.FilesWorker.OpenDirDlg( txtBoxFB2LongPathDir, fbdDir, "������� ����� ��� fb2-������, � ������� ����� ��������� ���� ���� ���������� ������� �������:" );
		}
		#endregion
		
		#endregion
		
	}
}
