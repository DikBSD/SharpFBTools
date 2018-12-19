/*
 * Сделано в SharpDevelop.
 * Пользователь: Вадим Кузнецов (dikbsd)
 * Дата: 04.08.2015
 * Время: 15:23
 * 
 * License: GPL 2.1
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml;
using System.IO;

using Core.FB2.FB2Parsers;

using TitleInfoEnum = Core.Common.Enums.TitleInfoEnum;

namespace Core.Common
{
	/// <summary>
	/// EditLangForm: Форма для правки языка fb2 книг
	/// </summary>
	public partial class EditLangForm : Form
	{
		#region Закрытые данные класса
		private readonly string	m_TempDir = Settings.Settings.TempDirPath;
		private bool m_ApplyData = false;
		private readonly IList<FB2ItemInfo> m_LangFB2InfoList = null;
		private readonly SharpZipLibWorker m_sharpZipLib = new SharpZipLibWorker();
		private BackgroundWorker m_bw = null;
		#endregion
		
		public EditLangForm( ref IList<FB2ItemInfo> LangFB2InfoList )
		{
			InitializeComponent();
			initializeBackgroundWorker();
			
			this.Text += " : " + LangFB2InfoList.Count.ToString() + " книг";
			m_LangFB2InfoList = LangFB2InfoList;
			// создание списков языков
			LangComboBox.Items.AddRange( LangList.LangsList );
			ControlPanel.Enabled = true;
			LangsPanel.Enabled = true;
			LangComboBox.SelectedIndex = 0;
			ProgressBar.Maximum = LangFB2InfoList.Count;
		}
		
		#region BackgroundWorker
		// Инициализация перед использование BackgroundWorker
		private void initializeBackgroundWorker() {
			m_bw = new BackgroundWorker();
			m_bw.WorkerReportsProgress		= true; // Позволить выводить прогресс процесса
			m_bw.WorkerSupportsCancellation	= true; // Позволить отменить выполнение работы процесса
			m_bw.DoWork 			+= new DoWorkEventHandler( bw_DoWork );
			m_bw.ProgressChanged 	+= new ProgressChangedEventHandler( bw_ProgressChanged );
			m_bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler( bw_RunWorkerCompleted );
		}
		private void bw_DoWork( object sender, DoWorkEventArgs e ) {
			Cursor.Current = Cursors.WaitCursor;
			FB2DescriptionCorrector fB2Corrector = null;
			foreach ( FB2ItemInfo Info in m_LangFB2InfoList ) {
				FictionBook fb2 = Info.FictionBook;
				if ( fb2 != null ) {
					fB2Corrector = new FB2DescriptionCorrector( fb2 );
					fB2Corrector.recoveryDescriptionNode();
					
					XmlNode xmlTI = fb2.getTitleInfoNode( TitleInfoEnum.TitleInfo );
					if ( xmlTI != null ) {
						xmlTI.ReplaceChild(
							fB2Corrector.makeLangNode( LangComboBox.Text.Substring( LangComboBox.Text.IndexOf('(')+1, 2 ) ),
							fb2.getLangNode( TitleInfoEnum.TitleInfo )
						);
						
						// сохранение fb2 файла
						if ( !Directory.Exists( m_TempDir ) )
							Directory.CreateDirectory( m_TempDir );
						string NewPath = Info.IsFromZip ? Info.FilePathIfFromZip : Info.FilePathSource;
						fb2.saveToFB2File( NewPath, false );
						if ( Info.IsFromZip )
							WorksWithBooks.zipMoveTempFB2FileTo( m_sharpZipLib, NewPath, Info.FilePathSource );
						if ( Info.IsFromZip && File.Exists( NewPath ) )
							File.Delete( NewPath );
					}
				}
				m_bw.ReportProgress( 1 );
				ProgressBar.Update();
			}
			Cursor.Current = Cursors.Default;
		}
		private void bw_ProgressChanged( object sender, ProgressChangedEventArgs e ) {
			++ProgressBar.Value;
		}
		private void bw_RunWorkerCompleted( object sender, RunWorkerCompletedEventArgs e ) {
			Close();
		}
		#endregion
		
		#region Открытые методы
		public bool isApplyData() {
			return m_ApplyData;
		}
		#endregion
		
		#region Обработчики событий
		void CancelBtnClick(object sender, EventArgs e)
		{
			Close();
		}
		void ApplyBtnClick(object sender, EventArgs e)
		{
			if ( LangComboBox.SelectedIndex == -1 ) {
				MessageBox.Show(
					"Выберите язык из списка.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning
				);
				LangComboBox.Focus();
			} else {
				m_ApplyData = true;
				ControlPanel.Enabled = false;
				LangsPanel.Enabled = false;
				if ( !m_bw.IsBusy )
					m_bw.RunWorkerAsync();
			}
		}
		#endregion
	}
}
