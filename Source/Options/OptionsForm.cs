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
			tboxFBEPath.Text			= Settings.Settings.DefFBEPath;
			tboxTextEPath.Text			= Settings.Settings.DefTFB2Path;
			tboxReaderPath.Text 		= Settings.Settings.DefFBReaderPath;
			tboxDiffPath.Text 			= Settings.Settings.DiffPath;
			cboxDSArchiveManager.Text	= Settings.ArchiveManagerSettings.GetDefAMcboxDSArchiveManagerText();
			cboxTIRArchiveManager.Text	= Settings.ArchiveManagerSettings.GetDefAMcboxTIRArchiveManagerText();
			cboxDSFB2Dup.Text			= Settings.FB2DublicatorSettings.GetDefDupcboxDSFB2DupText();
			cboxTIRFB2Dup.Text			= Settings.FB2DublicatorSettings.GetDefDupcboxTIRFB2DupText();
			chBoxConfirmationForExit.Checked = true;
		}
		
		// �������� �������� �� xml-�����
		private void readSettingsFromXML() {
			#region ���
			if( File.Exists( Settings.Settings.SettingsPath ) ) {
				XElement xmlTree = XElement.Load( Settings.Settings.SettingsPath );
				/* �������� ��������� ��� ���� ������������ */
				if( xmlTree.Element("General") != null ) {
					XElement xmlGeneral = xmlTree.Element("General");
					// FBE ��������
					if( xmlGeneral.Element("FBEPath") != null )
						tboxFBEPath.Text = xmlGeneral.Element("FBEPath").Value;
					// Text ��������
					if( xmlGeneral.Element("TextFB2EPath") != null )
						tboxTextEPath.Text = xmlGeneral.Element("TextFB2EPath").Value;
					// FB2 Reader
					if( xmlGeneral.Element("FBReaderPath") != null )
						tboxReaderPath.Text = xmlGeneral.Element("FBReaderPath").Value;
					// Diff ����������
					if( xmlGeneral.Element("DiffPath") != null )
						tboxDiffPath.Text = xmlGeneral.Element("DiffPath").Value;
					// ������������� ������ �� ���������
					if( xmlGeneral.Element("ConfirmationForAppExit") != null )
						chBoxConfirmationForExit.Checked = Convert.ToBoolean( xmlGeneral.Element("ConfirmationForAppExit").Value );
					// ����� ������ ������������
					if( xmlGeneral.Element("ToolButtons") != null ) {
						XElement xmlToolButtons = xmlGeneral.Element("ToolButtons");
						// �������� �������
						if( xmlToolButtons.Element("ArchiveManagerToolButtons") != null ) {
							XElement xmlArchiveManagerToolButtons = xmlToolButtons.Element("ArchiveManagerToolButtons");
							if( xmlArchiveManagerToolButtons.Attribute("DSArchiveManagerText") != null )
								cboxDSArchiveManager.Text = xmlArchiveManagerToolButtons.Attribute("DSArchiveManagerText").Value;
							if( xmlArchiveManagerToolButtons.Attribute("TIRArchiveManagerText") != null )
								cboxTIRArchiveManager.Text = xmlArchiveManagerToolButtons.Attribute("TIRArchiveManagerText").Value;
						}
						// ����������
						if( xmlToolButtons.Element("DupToolButtons") != null ) {
							XElement xmlDupToolButtons = xmlToolButtons.Element("ArchiveManagerToolButtons");
							if( xmlDupToolButtons.Attribute("DSFB2DupText") != null )
								cboxDSFB2Dup.Text = xmlDupToolButtons.Attribute("DSFB2DupText").Value;
							if( xmlDupToolButtons.Attribute("TIRFB2DupText") != null )
								cboxTIRFB2Dup.Text = xmlDupToolButtons.Attribute("TIRFB2DupText").Value;
						}
					}
				}
			}
			#endregion
		}
		
		// ���������� �������� � xml-����
		private void saveSettingsToXml() {
			#region ���
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
				                          new XComment("������������� ������ �� ���������"),
				                          new XElement("ConfirmationForAppExit", chBoxConfirmationForExit.Checked),
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
			#endregion
		}
		#endregion
		
		#region �����������
		void BtnOKClick(object sender, EventArgs e)
		{
			// ���������� �������� � xml
			saveSettingsToXml();
			this.Close();
		}
		
		#region �����
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
		
		void BtnDiffPathClick(object sender, EventArgs e)
		{
			// �������� ���� � diff-���������
			ofDlg.Title = "������� ���� � diff-��������� ����������� ��������� ������:";
			ofDlg.FileName = "";
			ofDlg.Filter = "��������� (*.exe)|*.exe|��� ����� (*.*)|*.*";
			DialogResult result = ofDlg.ShowDialog();
			if (result == DialogResult.OK) {
				tboxDiffPath.Text = ofDlg.FileName;
			}
		}
		
		void CboxDSArchiveManagerSelectedIndexChanged(object sender, EventArgs e)
		{
			cboxTIRArchiveManager.Enabled = cboxDSArchiveManager.SelectedIndex == 2;
		}
		
		void CboxDSFB2DupSelectedIndexChanged(object sender, EventArgs e)
		{
			cboxTIRFB2Dup.Enabled = cboxDSFB2Dup.SelectedIndex == 2;
		}
		#endregion
		
		#region �������������� ��-���������
		void BtnDefRestoreClick(object sender, EventArgs e) {
			DefGeneral();
		}
		#endregion
		
		#endregion
	}
}
