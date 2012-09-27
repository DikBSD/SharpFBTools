/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 13.03.2009
 * Time: 13:41
 * 
 * License: GPL 2.1
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Options;
using Settings;
using SharpFBTools;
using SharpFBTools.AssemblyInfo;
using SharpFBTools.Tools;

using filesWorker = Core.FilesWorker.FilesWorker;

namespace Main
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		#region Закрытые члены класса
		private List<ToolStripButton>	m_listToggleBtns = new List<ToolStripButton>();	// список кнопок-переключателей панели инструментов
		private List<UserControl>		m_listImplPanels = new List<UserControl>();		// список имплантируемых панелей-режимов работы

		private SFBTpFB2Validator	sfbTpFB2Validator	= new SFBTpFB2Validator();	// панель Валидатора
		private SFBTpFileManager	sfbTpFileManager	= new SFBTpFileManager();	// панель Менеджера файлов
		private SFBTpArchiveManager	sfbTpArchiveManager	= new SFBTpArchiveManager();// панель Менеджера архивов
		private SFBTpFB2Dublicator	sfbTpFB2Dublicator	= new SFBTpFB2Dublicator();	// панель Дубликатора файлов
		private SFBTpAbout			sfbTpAbout			= new SFBTpAbout();			// панель О программе
		#endregion		
		
		public MainForm()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			// Запоминаем папку программы
			Settings.Settings.SetProgDir( Environment.CurrentDirectory );
			// задаем данные о программе Assembly
			SharpFBTools_AssemblyInfo.SetAssemblyTitle( AppAssembly.AssemblyTitle );
			SharpFBTools_AssemblyInfo.SetAssemblyProduct( AppAssembly.AssemblyProduct );
			SharpFBTools_AssemblyInfo.SetAssemblyVersion( AppAssembly.AssemblyVersion );
			SharpFBTools_AssemblyInfo.SetAssemblyCopyright( AppAssembly.AssemblyCopyright );
			SharpFBTools_AssemblyInfo.SetAssemblyCompany( AppAssembly.AssemblyCompany );
			SharpFBTools_AssemblyInfo.SetAssemblyDescription( AppAssembly.AssemblyDescription );
			// список кнопок-переключателей панели инструментов и список список имплантируемых панелей-режимов работы
			MainImpl.MakeGroupToggleLists( m_listToggleBtns,
		    							tsbtnFB2Validator, tsbtnFileManager,
										tsbtnArchiveManager, tsbtnFB2Dublicator, tsbtnAbout,
										m_listImplPanels,
		    							sfbTpFB2Validator, sfbTpFileManager,
										sfbTpArchiveManager, sfbTpFB2Dublicator, sfbTpAbout );
			// первоначальное задание режима работы - панель Валидатора
			tsbtnFB2Validator.Checked = true;
			this.tscMain.ContentPanel.Controls.Add( sfbTpFB2Validator );
			this.sfbTpFB2Validator.Dock = System.Windows.Forms.DockStyle.Fill;
		}
					
		#region Обработчики событий
		void TsbtnExitClick(object sender, EventArgs e)
		{
			// выход из программы
			if( ! Settings.Settings.ReadConfirmationForExit() ) {
				// очистка временной папки
				filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
				this.Close();
			} else {
				DialogResult result = MessageBox.Show( "Вы действительно хотите выйти из программы?", "SharpFBTools",
			                                      MessageBoxButtons.YesNo, MessageBoxIcon.Question );
	      	  	if( result == DialogResult.Yes ) {
					// очистка временной папки
					filesWorker.RemoveDir( Settings.Settings.GetTempDir() );
					this.Close();
				}
				return;
			}
		}
		
		void TsbtnFB2ValidatorClick(object sender, EventArgs e)
		{
			// переключение состояния кнопки Валидатора
			MainImpl.ToggleMode( m_listToggleBtns, m_listImplPanels,
			                    tsbtnFB2Validator, this.sfbTpFB2Validator,
			                    tscMain );
		}
		
		void TsbtnFileManagerClick(object sender, EventArgs e)
		{
			// переключение состояния кнопки Менеджера файлов
			MainImpl.ToggleMode( m_listToggleBtns, m_listImplPanels,
			                    tsbtnFileManager, this.sfbTpFileManager,
			                    tscMain );
		}
		
		void TsbtnArchiveManagerClick(object sender, EventArgs e)
		{
			// переключение состояния кнопки Менеджера архивов
			MainImpl.ToggleMode( m_listToggleBtns, m_listImplPanels,
			                    tsbtnArchiveManager, this.sfbTpArchiveManager,
			                    tscMain );
		}
		
		void TsbtnFB2DublicatorClick(object sender, EventArgs e)
		{
			// переключение состояния кнопки Дубликатора файлов
			MainImpl.ToggleMode( m_listToggleBtns, m_listImplPanels,
			                    tsbtnFB2Dublicator, this.sfbTpFB2Dublicator,
			                    tscMain );
		}
		
		void TsbtnAboutClick(object sender, EventArgs e)
		{
			// переключение состояния кнопки О программе
			MainImpl.ToggleMode( m_listToggleBtns, m_listImplPanels,
			                    tsbtnAbout, this.sfbTpAbout,
			                    tscMain );
		}
		
		void TsbtnOptionsClick(object sender, EventArgs e)
		{
			// запуск диалога Настроек
			OptionsForm ofrm = new OptionsForm();
			ofrm.ShowDialog();
			// задание для кнопок ToolStrip стиля и положения текста и картинки
			sfbTpFB2Validator.SetToolButtonsSettings();
			sfbTpArchiveManager.SetToolButtonsSettings();
			sfbTpFB2Dublicator.SetToolButtonsSettings();
			ofrm.Dispose();
		}
		
		void MainFormFormClosed(object sender, FormClosedEventArgs e)
		{
			// сохраняем пути к папкам и рабочие изменяющиеся данные всех инструменнов
			Settings.Settings.WriteSharpFBToolsWorksData();
			// Для Сортировщика
			Settings.FileManagerSettings.WriteFileManagerSettings();
		}
		#endregion
	}	
}
