/*
 * Created by SharpDevelop.
 * User: DikBSD
 * Date: 05.04.2009
 * Time: 14:31
 * 
 * License: GPL 2.1
 */
using System;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;

using FilesWorker = Core.Common.FilesWorker;

namespace Options
{
	/// <summary>
	/// ��������� ����� ���� ������������
	/// </summary>
	public partial class OptionsForm : Form
	{
		public OptionsForm()
		{
			#region ��� ������������
			InitializeComponent();
			/* ��-��������� */
			// �����
			DefGeneral();
			/* ������ ����������� ���������, ���� ��� ���� */
			readSettingsFromXML();
			#endregion
		}
		
		#region �������� ��������������� ������
		private void DefGeneral() {
			// ����� ���������
			tboxFBEPath.Text			= Settings.Settings.FB2EditorPath;
			tboxTextEPath.Text			= Settings.Settings.TextEditorPath;
			tboxReaderPath.Text 		= Settings.Settings.FBReaderPath;
			tboxDiffPath.Text 			= Settings.Settings.DiffToolPath;
			tboxTempDirPath.Text		= Settings.Settings.TempDirPath;
			cboxDSArchiveManager.Text	= Settings.ArchiveManagerSettings.GetDefAMcboxDSArchiveManagerText();
			cboxTIRArchiveManager.Text	= Settings.ArchiveManagerSettings.GetDefAMcboxTIRArchiveManagerText();
			cboxDSFB2Dup.Text			= Settings.FB2DublicatorSettings.GetDefDupcboxDSFB2DupText();
			cboxTIRFB2Dup.Text			= Settings.FB2DublicatorSettings.GetDefDupcboxTIRFB2DupText();
			chBoxConfirmationForExit.Checked = true;
			chBoxSaveDebugMessage.Checked = false;
			rbDebugMessageNewLog.Checked = false;
			rbDebugMessageLogAppend.Checked = true;
		}
		
		// �������� �������� �� xml-�����
		private void readSettingsFromXML() {
			if ( File.Exists( Settings.Settings.SettingsPath ) ) {
				XElement xmlTree = XElement.Load( Settings.Settings.SettingsPath );
				/* �������� ��������� ��� ���� ������������ */
				if ( xmlTree.Element("General") != null ) {
					XElement xmlGeneral = xmlTree.Element("General");
					// FBE ��������
					if ( xmlGeneral.Element("FBEPath") != null )
						tboxFBEPath.Text = xmlGeneral.Element("FBEPath").Value;
					// Text ��������
					if ( xmlGeneral.Element("TextFB2EPath") != null )
						tboxTextEPath.Text = xmlGeneral.Element("TextFB2EPath").Value;
					// FB2 Reader
					if ( xmlGeneral.Element("FBReaderPath") != null )
						tboxReaderPath.Text = xmlGeneral.Element("FBReaderPath").Value;
					// Diff ����������
					if ( xmlGeneral.Element("DiffPath") != null )
						tboxDiffPath.Text = xmlGeneral.Element("DiffPath").Value;
					// ���� � ��������� �����
					if ( xmlGeneral.Element("TempDirPath") != null )
						tboxTempDirPath.Text = xmlGeneral.Element("TempDirPath").Value;
					// ������������� ������ �� ���������
					if ( xmlGeneral.Element("ConfirmationForAppExit") != null )
						chBoxConfirmationForExit.Checked = Convert.ToBoolean( xmlGeneral.Element("ConfirmationForAppExit").Value );
					// ���������� ��������� �� ������� ��� ������� ������ ����������
					if ( xmlGeneral.Element("ShowDebugMessage") != null )
						chBoxSaveDebugMessage.Checked = Convert.ToBoolean( xmlGeneral.Element("ShowDebugMessage").Value );
					if ( xmlGeneral.Element("NewLog") != null )
						rbDebugMessageNewLog.Checked = Convert.ToBoolean( xmlGeneral.Element("NewLog").Value );
					if ( xmlGeneral.Element("AppendToLog") != null )
						rbDebugMessageLogAppend.Checked =
							Convert.ToBoolean( xmlGeneral.Element("AppendToLog").Value );
					
					// ����� ������ ������������
					if ( xmlGeneral.Element("ToolButtons") != null ) {
						XElement xmlToolButtons = xmlGeneral.Element("ToolButtons");
						// �������� �������
						if ( xmlToolButtons.Element("ArchiveManagerToolButtons") != null ) {
							XElement xmlArchiveManagerToolButtons = xmlToolButtons.Element("ArchiveManagerToolButtons");
							if ( xmlArchiveManagerToolButtons.Attribute("DSArchiveManagerText") != null )
								cboxDSArchiveManager.Text = xmlArchiveManagerToolButtons.Attribute("DSArchiveManagerText").Value;
							if ( xmlArchiveManagerToolButtons.Attribute("TIRArchiveManagerText") != null )
								cboxTIRArchiveManager.Text = xmlArchiveManagerToolButtons.Attribute("TIRArchiveManagerText").Value;
						}
						// ����������
						if ( xmlToolButtons.Element("DupToolButtons") != null ) {
							XElement xmlDupToolButtons = xmlToolButtons.Element("ArchiveManagerToolButtons");
							if ( xmlDupToolButtons.Attribute("DSFB2DupText") != null )
								cboxDSFB2Dup.Text = xmlDupToolButtons.Attribute("DSFB2DupText").Value;
							if ( xmlDupToolButtons.Attribute("TIRFB2DupText") != null )
								cboxTIRFB2Dup.Text = xmlDupToolButtons.Attribute("TIRFB2DupText").Value;
						}
					}
				}
			}
		}
		
		// ���������� �������� � xml-����
		private void saveSettingsToXml() {
			// ������ �� "���������" �������� � �����, ����� � ��������� �������� ������ ��� �� �����������
			XDocument doc = new XDocument(
				new XDeclaration("1.0", "utf-8", "yes"),
				new XElement("Settings",
				             new XComment("�������� ��������� ��� ���� ������������"),
				             new XElement("General",
				                          new XComment("FBE ��������"),
				                          new XElement("FBEPath", tboxFBEPath.Text),
				                          new XComment("Text ��������"),
				                          new XElement("TextFB2EPath", tboxTextEPath.Text),
				                          new XComment("FB2 Reader"),
				                          new XElement("FBReaderPath", tboxReaderPath.Text),
				                          new XComment("Diff ����������"),
				                          new XElement("DiffPath", tboxDiffPath.Text),
				                          new XComment("���� � ��������� �����"),
				                          new XElement("TempDirPath", tboxTempDirPath.Text),
				                          new XComment("������������� ������ �� ���������"),
				                          new XElement("ConfirmationForAppExit", chBoxConfirmationForExit.Checked),
				                          new XComment("���������� ��������� �� ������� ��� ������� ������ ����������"),
				                          new XElement("ShowDebugMessage", chBoxSaveDebugMessage.Checked),
				                          new XElement("NewLog", rbDebugMessageNewLog.Checked),
				                          new XElement("AppendToLog", rbDebugMessageLogAppend.Checked),
				                          new XComment("����� ������ ������������"),
				                          new XElement("ToolButtons",
				                                       new XElement("ArchiveManagerToolButtons",
				                                                    new XAttribute("DSArchiveManagerText", cboxDSArchiveManager.Text),
				                                                    new XAttribute("TIRArchiveManagerText", cboxTIRArchiveManager.Text)
				                                                   ),
				                                       new XElement("DupToolButtons",
				                                                    new XAttribute("DSFB2DupText", cboxDSFB2Dup.Text),
				                                                    new XAttribute("TIRFB2DupText", cboxTIRFB2Dup.Text)
				                                                   )
				                                      )
				                         )
				            )
			);
			doc.Save( Settings.Settings.SettingsPath );
		}
		#endregion
		
		#region �����������
		void BtnOKClick(object sender, EventArgs e)
		{
			// ���������� �������� � xml
			saveSettingsToXml();
			this.Close();
		}
		
		void BtnFBEPathClick(object sender, EventArgs e)
		{
			// �������� ���� � fb2-���������
			ofDlg.Title = "������� ���� � FB2-���������:";
			ofDlg.FileName = "";
			ofDlg.Filter = "��������� (*.exe)|*.exe|��� ����� (*.*)|*.*";
			DialogResult result = ofDlg.ShowDialog();
			if ( result == DialogResult.OK ) {
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
			if ( result == DialogResult.OK ) {
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
			if ( result == DialogResult.OK ) {
				tboxReaderPath.Text = ofDlg.FileName;
			}
		}
		
		void BtnDiffPathClick(object sender, EventArgs e)
		{
			// �������� ���� � diff-���������
			ofDlg.Title = "������� ���� � diff-��������� ����������� ��������� ������:";
			ofDlg.FileName = "";
			ofDlg.Filter = "��������� (*.exe)|*.exe|��� ����� (*.*)|*.*";
			DialogResult result = ofDlg.ShowDialog();
			if ( result == DialogResult.OK ) {
				tboxDiffPath.Text = ofDlg.FileName;
			}
		}
		
		void BtnTempDirPathClick(object sender, EventArgs e)
		{
			// �������� ���� � ��������� �����
			string TempDir = FilesWorker.OpenDirDlg( tboxTempDirPath.Text, fbdDir, "������� ���� � ��������� �����:" );
			if ( ! string.IsNullOrWhiteSpace( TempDir ) )
				tboxTempDirPath.Text = TempDir;
		}
		
		void CboxDSArchiveManagerSelectedIndexChanged(object sender, EventArgs e)
		{
			cboxTIRArchiveManager.Enabled = cboxDSArchiveManager.SelectedIndex == 2;
		}
		
		void CboxDSFB2DupSelectedIndexChanged(object sender, EventArgs e)
		{
			cboxTIRFB2Dup.Enabled = cboxDSFB2Dup.SelectedIndex == 2;
		}
		
		void ChBoxSaveDebugMessageCheckedChanged(object sender, EventArgs e)
		{
			rbDebugMessageNewLog.Enabled = chBoxSaveDebugMessage.Checked;
			rbDebugMessageLogAppend.Enabled = chBoxSaveDebugMessage.Checked;
		}
		#endregion
		
		#region �������������� ��-���������
		void BtnDefRestoreClick(object sender, EventArgs e) {
			DefGeneral();
		}
		#endregion
		
	}
}
