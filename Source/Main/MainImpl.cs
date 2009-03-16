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
using SharpFBTools.Controls.Panels;

namespace Main
{
	/// <summary>
	/// Description of MainImpl.
	/// </summary>
	public class MainImpl
	{
		public MainImpl()
		{
		}
		
		#region Закрытые члены-методы класса
		private static void MakeGroupToggleButtonsList( List<ToolStripButton> l,
		    ToolStripButton tsbtnFB2Validator, ToolStripButton tsbtnFileManager,
			ToolStripButton tsbtnArchiveManager, ToolStripButton tsbtnFB2Corrector,
			ToolStripButton tsbtnFB2Dublicator, ToolStripButton tsbtnAbout ) {
			// список кнопок-переключателей панели инструментов
			l.Add( tsbtnFB2Validator );
			l.Add( tsbtnFileManager );
			l.Add( tsbtnArchiveManager );
			l.Add( tsbtnFB2Corrector );
			l.Add( tsbtnFB2Dublicator );
			l.Add( tsbtnAbout );
		}		
		
		private static void MakeImplPanelsList( List<UserControl> l,
		    SFBTpFB2Validator sfbTpFB2Validator, SFBTpFileManager sfbTpFileManager,
			SFBTpArchiveManager sfbTpArchiveManager, SFBTpFB2Corrector sfbTpFB2Corrector,
			SFBTpFB2Dublicator sfbTpFB2Dublicator, SFBTpAbout sfbTpAbout ) {
			// список список имплантируемых панелей-режимов работы
			l.Add( sfbTpFB2Validator );
			l.Add( sfbTpFileManager );
			l.Add( sfbTpArchiveManager );
			l.Add( sfbTpFB2Corrector );
			l.Add( sfbTpFB2Dublicator );
			l.Add( sfbTpAbout );
		}	
		#endregion
		
		#region Jnrhsnst члены-методы класса
		public static void MakeGroupToggleLists( List<ToolStripButton> tsbl,
		    ToolStripButton tsbtnFB2Validator, ToolStripButton tsbtnFileManager,
			ToolStripButton tsbtnArchiveManager, ToolStripButton tsbtnFB2Corrector,
			ToolStripButton tsbtnFB2Dublicator, ToolStripButton tsbtnAbout,
			List<UserControl> ucl,
		    SFBTpFB2Validator sfbTpFB2Validator, SFBTpFileManager sfbTpFileManager,
			SFBTpArchiveManager sfbTpArchiveManager, SFBTpFB2Corrector sfbTpFB2Corrector,
			SFBTpFB2Dublicator sfbTpFB2Dublicator, SFBTpAbout sfbTpAbout ) {
			// список кнопок-переключателей панели инструментов и список список имплантируемых панелей-режимов работы
			MakeGroupToggleButtonsList( tsbl,
		    							tsbtnFB2Validator, tsbtnFileManager,
										tsbtnArchiveManager, tsbtnFB2Corrector,
										tsbtnFB2Dublicator, tsbtnAbout );
			// список кнопок-переключателей панели инструментов
			MakeImplPanelsList( ucl,
		    					sfbTpFB2Validator, sfbTpFileManager,
								sfbTpArchiveManager, sfbTpFB2Corrector,
								sfbTpFB2Dublicator, sfbTpAbout ) ;
		}
		
		public static void ToggleMode( List<ToolStripButton> tsbl, List<UserControl> ucl,
				ToolStripButton Btn, UserControl Uc, ToolStripContainer tscMain ) {
			// переключение состояния кнопки btn
			if( Btn.Checked!=true ) {
				Btn.Checked = true;
				// отключаем все кнопки, кроме btn
				foreach( ToolStripButton btn in tsbl ) {
					if( btn!=Btn ) {
						btn.Checked = false;
					}
				}
				// отключаем видимость всех имплантированных панелей, кроме uc
				foreach( UserControl uc in ucl ) {
					if( uc!=Uc ) {
						uc.Visible = false;
					}
				}
				// панель Uc делаем видимой
				tscMain.ContentPanel.Controls.Add( Uc );
				Uc.Dock = System.Windows.Forms.DockStyle.Fill;
				Uc.Visible = true;
			}
		}
		#endregion
	}
}
