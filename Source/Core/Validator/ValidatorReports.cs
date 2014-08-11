/*
 * Created by SharpDevelop.
 * User: DikBSD
 * Date: 10.03.2009
 * Time: 9:18
 * 
 * License: GPL 2.1
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Text;

namespace Core.Validator
{
	/// <summary>
	/// Отчеты о валидации
	/// </summary>
	public class ValidatorReports
	{
		public ValidatorReports()
		{
		}
			
		#region Открытые методы класса
		public static void MakeFB2Report( System.Windows.Forms.ListView lw, string sFilePath, string sTitle, 
		                                 ToolStripProgressBar tsProgressBar, StatusStrip ssProgress ) {
			#region Код
			if( lw.Items.Count < 1 ) return;
			// генерация отчета
			tsProgressBar.Maximum = lw.Items.Count+2;
			tsProgressBar.Value = 1;
			ssProgress.Refresh();
			List<string> lFB2Text = new List<string>();
			lFB2Text.Add( "<?xml version=\"1.0\" encoding=\"utf-8\" ?>" );
			lFB2Text.Add( "<FictionBook xmlns=\"http://www.gribuser.ru/xml/fictionbook/2.0\" xmlns:l=\"http://www.w3.org/1999/xlink\">" );
			lFB2Text.Add( "<description>" );
			lFB2Text.Add( "<title-info>" );
			lFB2Text.Add( "<genre>comp_programming</genre>" );
			lFB2Text.Add( "<author><first-name/><middle-name/><last-name/></author>" );
			lFB2Text.Add( "<book-title>" + sTitle + "</book-title>" );
			lFB2Text.Add( "<lang>ru</lang>" );
			lFB2Text.Add( "</title-info>" );
			lFB2Text.Add( "<document-info>" );
			lFB2Text.Add( "<author><first-name>Вадим</first-name><middle-name></middle-name><last-name>Кузнецов</last-name><nickname>DikBSD</nickname></author>" );
			lFB2Text.Add( "<program-used>FB2SharpValidator</program-used>" );
			lFB2Text.Add( "<date></date>" );
			lFB2Text.Add( "<id>FB2SharpValidator</id>" );
			lFB2Text.Add( "<version>1.0</version>" );
			lFB2Text.Add( "</document-info>" );
			lFB2Text.Add( "</description>" );
			lFB2Text.Add( "<body>" );
			lFB2Text.Add( "<title><p>" + sTitle + "</p></title>" );
			++tsProgressBar.Value;
			ssProgress.Refresh();
			int n=0;
			for( int i=0; i!=lw.Items.Count; ++i ) {
				++n;
				lFB2Text.Add( "<section><title><p>"+n+"</p></title>" );
				for( int j=0; j!=lw.Columns.Count; ++j ) {
					lFB2Text.Add( "<p><strong>"+lw.Columns[j].Text+":</strong></p>" );
					lFB2Text.Add( "<p>"+lw.Items[i].SubItems[j].Text+"</p>" );
				}
				lFB2Text.Add( "</section>" );
				++tsProgressBar.Value;
				ssProgress.Refresh();
			}
			lFB2Text.Add( "</body>" );
			lFB2Text.Add( "</FictionBook>" );
			
			// сохранение отчета в файл
			StreamWriter sw = new StreamWriter( @sFilePath, false, Encoding.UTF8 );
			tsProgressBar.Maximum = lFB2Text.Count+1;
			tsProgressBar.Value = 1;
			ssProgress.Refresh();
			foreach( string sLine in lFB2Text ) {
				sw.WriteLine( sLine );
				++tsProgressBar.Value;
				ssProgress.Refresh();
			}
			sw.Close();
			#endregion
		}
		
		public static void MakeHTMLReport( System.Windows.Forms.ListView lw, string sFilePath, string sTitle,
		                                 ToolStripProgressBar tsProgressBar, StatusStrip ssProgress) {
			#region Код
			if( lw.Items.Count < 1 ) return;
			// генерация отчета
			tsProgressBar.Maximum = lw.Items.Count+2;
			tsProgressBar.Value = 1;
			ssProgress.Refresh();
			List<string> lHTMLText = new List<string>();
			lHTMLText.Add( "<html>" );
			lHTMLText.Add( "<meta http-equiv=\"content-type\" content=\"text/html; charset=utf-8\">" );
			lHTMLText.Add( "<body>" );
			lHTMLText.Add( "<font color=\"#FF0000\"><h1 ALIGN=CENTER>"+sTitle+"</h1></font>" );
			lHTMLText.Add( "<table border=\"1\" width=100%>" );
			// формируем заголовок
			lHTMLText.Add( "<tr>" );
			lHTMLText.Add( "<th><b>№</b></th>" );
			for( int j=0; j!=lw.Columns.Count; ++j ) {
				lHTMLText.Add( "<th><b>"+lw.Columns[j].Text+"</b></th>" );
			}
			++tsProgressBar.Value;
			ssProgress.Refresh();
			// формируем строки данных
			int n=0;
			for( int i=0; i!=lw.Items.Count; ++i ) {
				++n;
				lHTMLText.Add( "<tr>" );
				lHTMLText.Add( "<td>"+n+"</td>" );
				for( int j=0; j!=lw.Columns.Count; ++j ) {
					lHTMLText.Add( "<td>"+lw.Items[i].SubItems[j].Text+"</td>" );
				}
				lHTMLText.Add( "</tr>" );
				++tsProgressBar.Value;
				ssProgress.Refresh();
			}
			lHTMLText.Add( "</table>" );
			lHTMLText.Add( "</html>" );
			
			// сохранение отчета в файл
			StreamWriter sw = new StreamWriter( @sFilePath, false, Encoding.UTF8 );
			tsProgressBar.Maximum = lHTMLText.Count+1;
			tsProgressBar.Value = 1;
			ssProgress.Refresh();
			foreach( string sLine in lHTMLText ) {
				sw.WriteLine( sLine );
				++tsProgressBar.Value;
				ssProgress.Refresh();
			}
			sw.Close();
			#endregion
		}
		
		// 
		public static bool SaveFilesList( ListView lw, SaveFileDialog sfd, string sTXTFilter,
		                  	 StatusStrip ssProgress, ToolStripLabel tsslblProgress, ToolStripProgressBar tsProgressBar, 
		                  	 string tsslblProgressText, string sNoFilesMess, string sEndMess, string sReady ) {
			// сохранение списка файлов из ListView
			#region
			if( lw.Items.Count < 1 ) {
				MessageBox.Show( sNoFilesMess, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
				return false;
			} else {
				sfd.Filter = sTXTFilter;
				sfd.FileName = "";
				DialogResult result = sfd.ShowDialog();
				if (result == DialogResult.OK) {
    	       		tsslblProgress.Text = tsslblProgressText;
    	    		tsProgressBar.Visible = true;
					tsProgressBar.Maximum = lw.Items.Count+1;
					tsProgressBar.Value = 1;
					ssProgress.Refresh();
					// сохранение списка в файл
					string sFilePath = sfd.FileName;
					StreamWriter sw = new StreamWriter( @sFilePath, false, Encoding.UTF8 );
					for( int i=0; i!=lw.Items.Count; ++i ) {
						string s = lw.Items[i].SubItems[0].Text.Split('/')[0];
						if( File.Exists( s ) ) {
							sw.WriteLine( s );
						}
						++tsProgressBar.Value;
						ssProgress.Refresh();
					}
					sw.Close();
					MessageBox.Show( sEndMess, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
					tsProgressBar.Visible = false;
					tsslblProgress.Text = sReady;
	          	}
			}
			return true;
			#endregion
		}
		#endregion
	}
}
