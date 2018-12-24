/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 16.03.2009
 * Time: 11:02
 * 
 * License: GPL 2.1
 */

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

using SharpFBTools.Tools;

namespace Main
{
	/// <summary>
	/// MainImpl
	/// </summary>
	public class MainImpl
	{
		public MainImpl()
		{
		}
		
		#region Закрытые члены-методы класса
		private static void MakeGroupToggleButtonsList(
			List<ToolStripButton> l,
			ToolStripButton tsbtnDescEditor, ToolStripButton tsbtnFB2Dublicator,
			ToolStripButton tsbtnFileManager, ToolStripButton tsbtnArchiveManager,
			ToolStripButton tsbtnAbout
		) {
			// список кнопок-переключателей панели инструментов
			l.Add( tsbtnDescEditor );
			l.Add( tsbtnFB2Dublicator );
			l.Add( tsbtnFileManager );
			l.Add( tsbtnArchiveManager );
			l.Add( tsbtnAbout );
		}
		
		private static void MakeImplPanelsList(
			List<UserControl> l,
			SFBTpFB2Corrector sfbTpFB2Corrector, SFBTpFB2Dublicator sfbTpFB2Dublicator,
			SFBTpFB2Sorter sfbTpFileManager, SFBTpArchiveManager sfbTpArchiveManager,
			SFBTpAbout sfbTpAbout
		) {
			// список список имплантируемых панелей-режимов работы
			l.Add( sfbTpFB2Corrector );
			l.Add( sfbTpFB2Dublicator );
			l.Add( sfbTpFileManager );
			l.Add( sfbTpArchiveManager );
			l.Add( sfbTpAbout );
		}
		#endregion
		
		#region Открытые члены-методы класса
		public static void MakeGroupToggleLists(
			List<ToolStripButton> tsbl,
			ToolStripButton tsbtnDescEditor, ToolStripButton tsbtnFB2Dublicator,
			ToolStripButton tsbtnFileManager, ToolStripButton tsbtnArchiveManager,
			ToolStripButton tsbtnAbout,
			List<UserControl> ucl,
			SFBTpFB2Corrector sfbTpFB2Corrector, SFBTpFB2Dublicator sfbTpFB2Dublicator,
			SFBTpFB2Sorter sfbTpFileManager, SFBTpArchiveManager sfbTpArchiveManager,
			SFBTpAbout sfbTpAbout
		) {
			// список кнопок-переключателей панели инструментов
			MakeGroupToggleButtonsList( tsbl, tsbtnDescEditor,  tsbtnFB2Dublicator,
			                           tsbtnFileManager, tsbtnArchiveManager,
			                           tsbtnAbout );
			// список имплантируемых панелей-режимов работы
			MakeImplPanelsList( ucl, sfbTpFB2Corrector, sfbTpFB2Dublicator, sfbTpFileManager,
			                   sfbTpArchiveManager, sfbTpAbout );
		}
		
		public static void ToggleMode( List<ToolStripButton> tsbl, List<UserControl> ucl,
		                              ToolStripButton Btn, UserControl Uc, ToolStripContainer tscMain ) {
			// переключение состояния кнопки btn
			if ( !Btn.Checked ) {
				Btn.Checked = true;
				
				MainForm.getSFBTpFB2Corrector().ConnectListsEventHandlers( false );
				MainForm.getSFBTpFB2Dublicator().ConnectListsEventHandlers( false );
				MainForm.getSFBTpFileManager().ConnectListsEventHandlers( false );
				
				// отключаем все кнопки, кроме btn
				foreach ( ToolStripButton btn in tsbl ) {
					if ( btn != Btn ) {
						btn.Checked = false;
					}
				}
				// отключаем видимость всех имплантированных панелей, кроме uc
				foreach ( UserControl uc in ucl ) {
					if ( uc != Uc )
						uc.Visible = false;
				}
				
				// панель Uc делаем видимой
				tscMain.ContentPanel.Controls.Add( Uc );
				Uc.Dock = DockStyle.Fill;
				Uc.Visible = true;
				
				MainForm.getSFBTpFB2Corrector().ConnectListsEventHandlers( true );
				MainForm.getSFBTpFB2Dublicator().ConnectListsEventHandlers( true );
				MainForm.getSFBTpFileManager().ConnectListsEventHandlers( true );
			}
		}
		#endregion
	}
}
