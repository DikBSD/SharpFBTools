/*
 * Created by SharpDevelop.
 * User: DikBSD
 * Date: 13.03.2009
 * Time: 13:41
 * 
 * License: GPL 2.1
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SharpFBTools.Controls.Panels;

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
		private SFBTpFB2Corrector	sfbTpFB2Corrector	= new SFBTpFB2Corrector();	// панель FB2 Корректора
		private SFBTpFB2Dublicator	sfbTpFB2Dublicator	= new SFBTpFB2Dublicator();	// панель Дубликатора файлов
		private SFBTpAbout			sfbTpAbout			= new SFBTpAbout();			// панель О программе
		#endregion		
		
		public MainForm()
		{
			//
			// The InitializeComponent() call is required for Windows Forms designer support.
			//
			InitializeComponent();
			//
			// список кнопок-переключателей панели инструментов
			MakeGroupToggleButtonsList();
			// список список имплантируемых панелей-режимов работы
			MakeImplPanelsList();
			// первоначальное задание режима работы - панель Валидатора
			tsbtnFB2Validator.Checked = true;
			this.tscMain.ContentPanel.Controls.Add( sfbTpFB2Validator );
			this.sfbTpFB2Validator.Dock = System.Windows.Forms.DockStyle.Fill;
		}
		
		private void MakeGroupToggleButtonsList() {
			// список кнопок-переключателей панели инструментов
			m_listToggleBtns.Add( tsbtnFB2Validator );
			m_listToggleBtns.Add( tsbtnFileManager );
			m_listToggleBtns.Add( tsbtnArchiveManager );
			m_listToggleBtns.Add( tsbtnFB2Corrector );
			m_listToggleBtns.Add( tsbtnFB2Dublicator );
			m_listToggleBtns.Add( tsbtnAbout );
		}		
		
		private void MakeImplPanelsList() {
			// список список имплантируемых панелей-режимов работы
			m_listImplPanels.Add( sfbTpFB2Validator );
			m_listImplPanels.Add( sfbTpFileManager );
			m_listImplPanels.Add( sfbTpArchiveManager );
			m_listImplPanels.Add( sfbTpFB2Corrector );
			m_listImplPanels.Add( sfbTpFB2Dublicator );
			m_listImplPanels.Add( sfbTpAbout );
		}	
		
		void ToggleMode( ToolStripButton Btn, UserControl Uc )
		{
			// переключение состояния кнопки btn
			if( Btn.Checked!=true ) {
				Btn.Checked = true;
				// отключаем все кнопки, кроме btn
				foreach( ToolStripButton btn in m_listToggleBtns ) {
					if( btn!=Btn ) {
						btn.Checked = false;
					}
				}
				// отключаем видимость всех имплантированных панелей, кроме uc
				foreach( UserControl uc in m_listImplPanels ) {
					if( uc!=Uc ) {
						uc.Visible = false;
					}
				}
				// панель Uc делаем видимой
				this.tscMain.ContentPanel.Controls.Add( Uc );
				Uc.Dock = System.Windows.Forms.DockStyle.Fill;
				Uc.Visible = true;
			}
		}
		#region Обработчики событий
		void TsbtnExitClick(object sender, EventArgs e)
		{
			// выход из программы
			this.Close();
		}
		
		void TsbtnFB2ValidatorClick(object sender, EventArgs e)
		{
			// переключение состояния кнопки Валидатора
			ToggleMode( tsbtnFB2Validator, this.sfbTpFB2Validator );
		}
		
		void TsbtnFileManagerClick(object sender, EventArgs e)
		{
			// переключение состояния кнопки Менеджера файлов
			ToggleMode( tsbtnFileManager, this.sfbTpFileManager );
		}
		#endregion
		
		void TsbtnArchiveManagerClick(object sender, EventArgs e)
		{
			// переключение состояния кнопки Менеджера архивов
			ToggleMode( tsbtnArchiveManager, this.sfbTpArchiveManager );
		}
		
		void TsbtnFB2CorrectorClick(object sender, EventArgs e)
		{
			// переключение состояния кнопки FB2 Корректора
			ToggleMode( tsbtnFB2Corrector, this.sfbTpFB2Corrector );
		}
		
		void TsbtnFB2DublicatorClick(object sender, EventArgs e)
		{
			// переключение состояния кнопки Дубликатора файлов
			ToggleMode( tsbtnFB2Dublicator, this.sfbTpFB2Dublicator );
		}
		
		void TsbtnAboutClick(object sender, EventArgs e)
		{
			// переключение состояния кнопки О программе
			ToggleMode( tsbtnAbout, this.sfbTpAbout );
		}
	}	
}
