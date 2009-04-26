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
using SharpFBTools.AssemblyInfo;

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
			string sLicensePath = Settings.Settings.GetLicensePath();
			if( File.Exists( sLicensePath ) ) {
				rtboxLicense.LoadFile( sLicensePath );
			} else {
				rtboxLicense.Text = "Не найден файл лицензии: \""+sLicensePath+"\"";
			}
			string sChangeFilePath = Settings.Settings.GetChangeFilePath();
			if( File.Exists( sChangeFilePath ) ) {
				rtboxLog.LoadFile( sChangeFilePath );
			} else {
				rtboxLog.Text = "Не найден файл истории развития программы: \""+sChangeFilePath+"\"";
			}
			// справка
			cboxInstrument.SelectedIndex = 0;
		}
		
		void CboxInstrumentSelectedIndexChanged(object sender, EventArgs e)
		{
			rtboxHelp.Clear();
			string sFB2ValidatorHelpPath = Settings.Settings.GetFB2ValidatorHelpPath();
			string sArchiveManagerHelpPath = Settings.Settings.GetArchiveManagerHelpPath();
			switch( cboxInstrument.SelectedIndex ) {
				case 0:
					if( File.Exists( sFB2ValidatorHelpPath ) ) {
						rtboxHelp.LoadFile( sFB2ValidatorHelpPath );
					} else {
						rtboxHelp.Text = "Не найден файл Справки Валидатор: \""+sFB2ValidatorHelpPath+"\"";
					}
					break;
				case 2:
					if( File.Exists( sArchiveManagerHelpPath ) ) {
						rtboxHelp.LoadFile( sArchiveManagerHelpPath );
					} else {
						rtboxHelp.Text = "Не найден файл Справки Менеджера Архивов: \""+sArchiveManagerHelpPath+"\"";
					}
					break;
			}
		}
		
		void SFBTpAboutLayout(object sender, LayoutEventArgs e)
		{
			// Данные о программе
			lblSharpFBTools.Text = SharpFBTools_AssemblyInfo.GetAssemblyTitle()+"-"+
										SharpFBTools_AssemblyInfo.GetAssemblyVersion();
			lblAbout.Text = SharpFBTools_AssemblyInfo.GetAssemblyDescription();
			lblDeveloper.Text = "Разработчик: "+SharpFBTools_AssemblyInfo.GetAssemblyCompany();
			lblCopyright.Text = SharpFBTools_AssemblyInfo.GetAssemblyCopyright();
		}
	}
}
