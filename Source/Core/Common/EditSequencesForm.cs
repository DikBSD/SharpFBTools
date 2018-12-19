/*
 * Создано в SharpDevelop.
 * Пользователь: VadimK
 * Дата: 13.09.2016
 * Время: 8:22
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
	/// EditSequencesForm: Форма для правки Серий fb2 книги
	/// </summary>
	public partial class EditSequencesForm : Form
	{
		#region Закрытые данные класса
		private readonly string	m_TempDir = Settings.Settings.TempDirPath;
		private bool m_ApplyData = false;
		private readonly IList<FB2ItemInfo> m_SequencesFB2InfoList = null;
		private readonly SharpZipLibWorker m_sharpZipLib = new SharpZipLibWorker();
		private BackgroundWorker m_bw = null;
		#endregion
		
		public EditSequencesForm( ref IList<FB2ItemInfo> SequencesFB2InfoList )
		{
			InitializeComponent();
			initializeBackgroundWorker();
			
			this.Text += " : " + SequencesFB2InfoList.Count.ToString() + " книг";
			m_SequencesFB2InfoList = SequencesFB2InfoList;
			ControlPanel.Enabled = true;
			SequencesPanel.Enabled = true;
			ProgressBar.Maximum = SequencesFB2InfoList.Count;
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
			foreach ( FB2ItemInfo Info in m_SequencesFB2InfoList ) {
				FictionBook fb2 = Info.FictionBook;
				if ( fb2 != null ) {
					fB2Corrector = new FB2DescriptionCorrector( fb2 );
					fB2Corrector.recoveryDescriptionNode();
					
					XmlNode xmlTI = fb2.getTitleInfoNode( TitleInfoEnum.TitleInfo );
					if ( xmlTI != null ) {
						if ( RemoveRadioButton.Checked || ReplaceRadioButton.Checked ) {
							// удаление всех Серий
							foreach ( XmlNode node in fb2.getSequencesNode( TitleInfoEnum.TitleInfo ) )
								xmlTI.RemoveChild( node );
						}
						if ( AddRadioButton.Checked || ReplaceRadioButton.Checked ) {
							// добавление новой Серии
							xmlTI.AppendChild(
								fB2Corrector.makeSequenceNode( SequencesTextBox.Text.Trim(), NumberTextBox.Text.Trim() )
							);
						}
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
		void SequencesTextBoxKeyPress(object sender, KeyPressEventArgs e)
		{
			if ( e.KeyChar == (char)Keys.Return )
				ApplyBtnClick( sender, e );
		}
		void NumberTextBoxKeyPress(object sender, KeyPressEventArgs e)
		{
			if ( e.KeyChar == (char)Keys.Return )
				ApplyBtnClick( sender, e );
		}
		void RemoveRadioButtonCheckedChanged(object sender, EventArgs e)
		{
			SequencesPanel.Enabled = NumberPanel.Enabled = !RemoveRadioButton.Checked;
		}
		void EditSequencesFormShown(object sender, EventArgs e)
		{
			SequencesTextBox.Focus();
			if ( m_SequencesFB2InfoList.Count > 1 ) {
				NumberPanel.Visible = false;
				this.Height -= NumberPanel.Size.Height;
			} else {
				NumberPanel.Visible = true;
			}
		}
		void CancelBtnClick(object sender, EventArgs e)
		{
			Close();
		}
		void ApplyBtnClick(object sender, EventArgs e)
		{
			if ( RemoveRadioButton.Checked ) {
				m_ApplyData = true;
				ControlPanel.Enabled = false;
				SequencesPanel.Enabled = false;
				NumberPanel.Enabled = false;
				ModePanel.Enabled = false;
				if ( !m_bw.IsBusy )
					m_bw.RunWorkerAsync();
			} else {
				if ( string.IsNullOrWhiteSpace( SequencesTextBox.Text ) ) {
					MessageBox.Show(
						"Введите название Серии.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning
					);
					SequencesTextBox.Focus();
				} else {
					if ( !string.IsNullOrWhiteSpace( NumberTextBox.Text ) ) {
						int number = 0;
						if ( !int.TryParse( NumberTextBox.Text, out number ) ) {
							MessageBox.Show(
								"Номер Серии не может символы и/или пробелы! Введите число, или оставьте поле пустым, если у данной книги нет номера серии.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error
							);
							NumberTextBox.Focus();
							return;
						}
					}
					m_ApplyData = true;
					ControlPanel.Enabled = false;
					SequencesPanel.Enabled = false;
					NumberPanel.Enabled = false;
					ModePanel.Enabled = false;
					if ( !m_bw.IsBusy )
						m_bw.RunWorkerAsync();
				}
			}
		}
		
		#endregion
	}
}
