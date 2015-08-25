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
	/// Настройка опций всех инструментов
	/// </summary>
	public partial class OptionsForm : Form
	{
		public OptionsForm()
		{
			#region Код Конструктора
			InitializeComponent();
			/* по-умолчанию */
			// общие
			DefGeneral();
			/* читаем сохраненные настройки, если они есть */
			readSettingsFromXML();
			#endregion
		}
		
		#region Закрытые Вспомогательные методы
		private void DefGeneral() {
			// общие настройки
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
		
		// загрузка настроек из xml-файла
		private void readSettingsFromXML() {
			#region Код
			if( File.Exists( Settings.Settings.SettingsPath ) ) {
				XElement xmlTree = XElement.Load( Settings.Settings.SettingsPath );
				/* Основные настройки для всех инструментов */
				if( xmlTree.Element("General") != null ) {
					XElement xmlGeneral = xmlTree.Element("General");
					// FBE Редактор
					if( xmlGeneral.Element("FBEPath") != null )
						tboxFBEPath.Text = xmlGeneral.Element("FBEPath").Value;
					// Text Редактор
					if( xmlGeneral.Element("TextFB2EPath") != null )
						tboxTextEPath.Text = xmlGeneral.Element("TextFB2EPath").Value;
					// FB2 Reader
					if( xmlGeneral.Element("FBReaderPath") != null )
						tboxReaderPath.Text = xmlGeneral.Element("FBReaderPath").Value;
					// Diff инструмент
					if( xmlGeneral.Element("DiffPath") != null )
						tboxDiffPath.Text = xmlGeneral.Element("DiffPath").Value;
					// Подтверждение выхода из программы
					if( xmlGeneral.Element("ConfirmationForAppExit") != null )
						chBoxConfirmationForExit.Checked = Convert.ToBoolean( xmlGeneral.Element("ConfirmationForAppExit").Value );
					// Стиль кнопок инструментов
					if( xmlGeneral.Element("ToolButtons") != null ) {
						XElement xmlToolButtons = xmlGeneral.Element("ToolButtons");
						// Менеджер Архивов
						if( xmlToolButtons.Element("ArchiveManagerToolButtons") != null ) {
							XElement xmlArchiveManagerToolButtons = xmlToolButtons.Element("ArchiveManagerToolButtons");
							if( xmlArchiveManagerToolButtons.Attribute("DSArchiveManagerText") != null )
								cboxDSArchiveManager.Text = xmlArchiveManagerToolButtons.Attribute("DSArchiveManagerText").Value;
							if( xmlArchiveManagerToolButtons.Attribute("TIRArchiveManagerText") != null )
								cboxTIRArchiveManager.Text = xmlArchiveManagerToolButtons.Attribute("TIRArchiveManagerText").Value;
						}
						// Дубликатор
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
		
		// сохранение настроек в xml-файл
		private void saveSettingsToXml() {
			#region Код
			// защита от "затирания" настроек в файле, когда в некоторые контролы данные еще не загрузились
			XDocument doc = new XDocument(
				new XDeclaration("1.0", "utf-8", "yes"),
				new XElement("Settings",
				             new XComment("Основные настройки для всех инструментов"),
				             new XElement("General",
				                          new XComment("FBE Редактор"),
				                          new XElement("FBEPath", tboxFBEPath.Text),
				                          new XComment("Text Редактор"),
				                          new XElement("TextFB2EPath", tboxTextEPath.Text),
				                          new XComment("FB2 Reader"),
				                          new XElement("FBReaderPath", tboxReaderPath.Text),
				                          new XComment("Diff инструмент"),
				                          new XElement("DiffPath", tboxDiffPath.Text),
				                          new XComment("Подтверждение выхода из программы"),
				                          new XElement("ConfirmationForAppExit", chBoxConfirmationForExit.Checked),
				                          new XComment("Стиль кнопок инструментов"),
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
		
		#region Обработчики
		void BtnOKClick(object sender, EventArgs e)
		{
			// сохранение настроек в xml
			saveSettingsToXml();
			this.Close();
		}
		
		#region Общее
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
		
		void BtnReaderPathClick(object sender, EventArgs e)
		{
			// указание пути к Читалке fb2-файлов
			ofDlg.Title = "Укажите путь к Читалке fb2-файлов:";
			ofDlg.FileName = "";
			ofDlg.Filter = "Программы (*.exe)|*.exe|Все файлы (*.*)|*.*";
			DialogResult result = ofDlg.ShowDialog();
			if (result == DialogResult.OK) {
				tboxReaderPath.Text = ofDlg.FileName;
			}
		}
		
		void BtnDiffPathClick(object sender, EventArgs e)
		{
			// указание пути к diff-программе
			ofDlg.Title = "Укажите путь к diff-программе визуального сравнения файлов:";
			ofDlg.FileName = "";
			ofDlg.Filter = "Программы (*.exe)|*.exe|Все файлы (*.*)|*.*";
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
		
		#region Восстановление по-умолчанию
		void BtnDefRestoreClick(object sender, EventArgs e) {
			DefGeneral();
		}
		#endregion
		
		#endregion
	}
}
