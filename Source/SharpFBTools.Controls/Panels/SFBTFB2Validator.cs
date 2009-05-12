/*
 * Created by SharpDevelop.
 * User: Вадим Кузнецов (DikBSD)
 * Date: 13.03.2009
 * Time: 14:34
 * 
 * License: GPL 2.1
 */

using System;
using System.IO;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml;

using Settings;
using FB2.FB2Parsers;
using FB2.Description.DocumentInfo;

namespace SharpFBTools.Controls.Panels
{
	/// <summary>
	/// Description of SFBTpValidator.
	/// </summary>
	public partial class SFBTpFB2Validator : UserControl
	{
		public SFBTpFB2Validator()
		{
			// The InitializeComponent() call is required for Windows Forms designer support.
			InitializeComponent();
			cboxExistFile.SelectedIndex = 1;
			// инициализация контролов
			Init();
			// читаем сохраненные пути к папкам Валидатора, если они есть
			ReadValidatorDirs();
		}
		
		#region Закрытые данные класса
		// Color
		private Color	m_FB2ValidFontColor			= Color.Black;		// цвет для несжатых валидных fb2
		private Color	m_FB2NotValidFontColor		= Color.Black;		// цвет для несжатых не валидных fb2
		private Color	m_ZipFB2ValidFontColor		= Color.Blue;		// цвет для валидных fb2 в zip
		private Color	m_ZipFB2NotValidFontColor	= Color.Blue;		// цвет для не валидных fb2 в zip
		private Color	m_RarFB2ValidFontColor		= Color.DarkGreen;	// цвет для валидных fb2 в rar
		private Color	m_RarFB2NotValidFontColor	= Color.DarkGreen;	// цвет для не валидных fb2 в rar
		private Color	m_ZipFontColor				= Color.BlueViolet;	// цвет для zip не fb2
		private Color	m_RarFontColor				= Color.DarkCyan; 	// цвет для rar не fb2
		private Color	m_NotFB2FontColor			= Color.Black;		// цвет для всех остальных файлов
		//
		private long	m_lFB2Valid		= 0; // число валидных файлов
		private long	m_lFB2NotValid	= 0; // число не валидных файлов
		private long	m_lFB2Files		= 0; // число fb2 файлов (не сжатых)
		private long	m_lFB2ZipFiles	= 0; // число fb2.zip файлов
		private long	m_lFB2RarFiles	= 0; // число fb2.rar файлов
		private long	m_lNotFB2Files	= 0; // число других (не fb2) файлов
		//
		private string	m_sNotValid		= " Не валидные fb2-файлы ";
		private	string	m_sValid		= " Валидные fb2-файлы ";
		private string	m_sNotFB2Files	= " Не fb2-файлы ";
		// Report
		private string	m_FB2NotValidReportEmpty	= "Список не валидных fb2-файлов пуст!\nОтчет не сохранен.";
		private string	m_FB2ValidReportEmpty		= "Список валидных fb2-файлов пуст!\nОтчет не сохранен.";
		private string	m_NotFB2FileReportEmpty		= "Список не fb2-файлов пуст!\nОтчет не сохранен.";
		private string	m_FB2NotValidFilesListReport = "Список не валидных fb2-файлов";
		private string	m_FB2ValidFilesListReport 	= "Список валидных fb2-файлов";
		private string	m_NotFB2FilesListReport 	= "Список не fb2-файлов";
		private string	m_GeneratingReport			= "Генерация отчета";
		private string	m_ReportSaveOk = "Отчет сохранен в файл:\n";
		private string	m_HTMLFilter 	= "HTML файлы (*.html)|*.html|Все файлы (*.*)|*.*";
		private string	m_FB2Filter 	= "fb2 файлы (*.fb2)|*.fb2|Все файлы (*.*)|*.*";
		private string	m_CSV_csv_Filter = "CVS файлы (*.csv)|*.csv|Все файлы (*.*)|*.*";
		private string	m_TXTFilter 	= "TXT файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";
		#endregion
		
		#region Закрытые вспомогательные методы класса
		private void Init() {
			// инициализация контролов и переменных
			for( int i=0; i!=lvFilesCount.Items.Count; ++i ) {
				lvFilesCount.Items[i].SubItems[1].Text	= "0";
			}
			listViewNotValid.Items.Clear();
			listViewValid.Items.Clear();
			listViewNotFB2.Items.Clear();
			rеboxNotValid.Clear();
			tpNotValid.Text		= m_sNotValid;
			tpValid.Text		= m_sValid;
			tpNotFB2Files.Text	= m_sNotFB2Files;
			tsProgressBar.Value	= 1;
			tsslblProgress.Text		= Settings.Settings.GetReady();
			tsProgressBar.Visible	= false;
			m_lFB2Valid	= m_lFB2NotValid = m_lFB2Files = m_lFB2ZipFiles = m_lFB2RarFiles = m_lNotFB2Files = 0;
			// очистка временной папки
			FilesWorker.FilesWorker.RemoveDir( Settings.Settings.GetTempDir() );
		}
		
		private ListView GetCurrentListWiew()
		{
			// возвращает текущий ListView в зависимости от выбранной вкладки
			ListView l = null;
			switch( tcResult.SelectedIndex ) {
				case 0:
					// не валидные fb2-файлы
					l = listViewNotValid;
					break;
				case 1:
					// валидные fb2-файлы
					l = listViewValid;
					break;
				case 2:
					// не fb2-файлы
					l = listViewNotFB2;
					break;
			}
			return l;
		}
		
		private void ReadValidatorDirs() {
			// чтение путей к папкам Валидатора из xml-файла
			string sSettings = Settings.Settings.GetValidatorDirsSettingsPath();
			if( !File.Exists( sSettings ) ) return;
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.IgnoreWhitespace = true;
			using ( XmlReader reader = XmlReader.Create( sSettings, settings ) ) {
				reader.ReadToFollowing("ScanDir");
				if (reader.HasAttributes ) {
					tboxSourceDir.Text = reader.GetAttribute("tboxSourceDir");
					Settings.Settings.SetScanDir( tboxSourceDir.Text );
				}
				reader.ReadToFollowing("NotValidFB2Files");
				if (reader.HasAttributes ) {
					tboxFB2NotValidDirCopyTo.Text = reader.GetAttribute("tboxFB2NotValidDirCopyTo");
					Settings.Settings.SetFB2NotValidDirCopyTo( tboxFB2NotValidDirCopyTo.Text );
					tboxFB2NotValidDirMoveTo.Text = reader.GetAttribute("tboxFB2NotValidDirMoveTo");
					Settings.Settings.SetFB2NotValidDirMoveTo( tboxFB2NotValidDirMoveTo.Text );
				}
				reader.ReadToFollowing("ValidFB2Files");
				if (reader.HasAttributes ) {
					tboxFB2ValidDirCopyTo.Text = reader.GetAttribute("tboxFB2ValidDirCopyTo");
					Settings.Settings.SetFB2ValidDirCopyTo( tboxFB2ValidDirCopyTo.Text );
					tboxFB2ValidDirMoveTo.Text = reader.GetAttribute("tboxFB2ValidDirMoveTo");
					Settings.Settings.SetFB2ValidDirMoveTo( tboxFB2ValidDirMoveTo.Text );
				}
				reader.ReadToFollowing("NotFB2Files");
				if (reader.HasAttributes ) {
					tboxNotFB2DirCopyTo.Text = reader.GetAttribute("tboxNotFB2DirCopyTo");
					Settings.Settings.SetNotFB2DirCopyTo( tboxNotFB2DirCopyTo.Text );
					tboxNotFB2DirMoveTo.Text = reader.GetAttribute("tboxNotFB2DirMoveTo");
					Settings.Settings.SetNotFB2DirMoveTo( tboxNotFB2DirMoveTo.Text );
				}
				reader.Close();
			}
		}
		#endregion
				
		#region Парсеры файлов и архивов
		private void ParseFB2File( string sFile, FB2Parser.FB2Validator fv2Validator ) {
			// парсер несжатого fb2-файла
			string sMsg = fv2Validator.ValidatingFB2File( sFile );
			if ( sMsg == "" ) {
           		// файл валидный
           		++this.m_lFB2Valid;
				//listViewValid.Items.Add( sFile );
				ListViewItem item = new ListViewItem( sFile, 0 );
   				item.ForeColor = m_FB2ValidFontColor;
				FileInfo fi = new FileInfo( sFile );
   				item.SubItems.Add( FilesWorker.FilesWorker.FormatFileLenght( fi.Length ) );
				listViewValid.Items.AddRange( new ListViewItem[]{ item } );
				tpValid.Text = m_sValid + "( " + m_lFB2Valid.ToString() + " ) " ;
           	} else {
           		// файл не валидный
           		++this.m_lFB2NotValid;
				ListViewItem item = new ListViewItem( sFile, 0 );
   				item.ForeColor = m_FB2NotValidFontColor;
				item.SubItems.Add( sMsg );
   				FileInfo fi = new FileInfo( sFile );
   				item.SubItems.Add( FilesWorker.FilesWorker.FormatFileLenght( fi.Length ) );
				listViewNotValid.Items.AddRange( new ListViewItem[]{ item } );
				tpNotValid.Text = m_sNotValid + "( " + m_lFB2NotValid.ToString() + " ) " ;
           	}
			++tsProgressBar.Value;
		}
		
		private void ParseArchiveFile( string sArchiveFile, FB2Parser.FB2Validator fv2Validator, string sTempDir ) {
			// парсер архива
			string sExt = Path.GetExtension( sArchiveFile );
			if( sExt.ToLower() == ".zip" ) {
				Archiver.Archiver.unzip( Settings.Settings.Read7zaPath(), sArchiveFile, sTempDir );
			} else if( sExt.ToLower() == ".rar" ) {
				Archiver.Archiver.unrar( Settings.Settings.ReadUnRarPath(), sArchiveFile, sTempDir );
			}
			string [] files = Directory.GetFiles( sTempDir );
			if( files.Length <= 0 ) return;
				
			string sMsg = fv2Validator.ValidatingFB2File( files[0] );
			string sFileName = Path.GetFileName( files[0] );

			if ( sMsg == "" ) {
           		// файл валидный - это fb2
           		++this.m_lFB2Valid;
				//listViewValid.Items.Add( sArchiveFile + "/" + sFileName );
				ListViewItem item = new ListViewItem( sArchiveFile + "/" + sFileName , 0 );
				if( sExt.ToLower() == ".zip" ) {
					item.ForeColor = m_ZipFB2ValidFontColor;
					++this.m_lFB2ZipFiles;
				} else if( sExt.ToLower() == ".rar" ) {
					item.ForeColor = m_RarFB2ValidFontColor;
					++this.m_lFB2RarFiles;
				}
				FileInfo fi = new FileInfo( sArchiveFile );
   				string s = FilesWorker.FilesWorker.FormatFileLenght( fi.Length );
   				fi = new FileInfo( sTempDir+"\\"+sFileName );
   				s += " / "+FilesWorker.FilesWorker.FormatFileLenght( fi.Length );
   				item.SubItems.Add( s );
				listViewValid.Items.AddRange( new ListViewItem[]{ item } );
				tpValid.Text = m_sValid + "( " + this.m_lFB2Valid.ToString() + " ) " ;
           	} else {
           		// архив не валидный - посмотрим, что это
        		if( sExt.ToLower() == ".zip" ) {
					// определяем тип разархивированного файла
           			sExt = Path.GetExtension( sFileName );
					if( sExt.ToLower() != ".fb2" ) {
           				sMsg = "Тип файла: " + sExt;
           				++this.m_lNotFB2Files;
           				ListViewItem item = new ListViewItem( sArchiveFile + "/" + sFileName, 0 );
   						item.ForeColor = m_ZipFontColor;
           				item.SubItems.Add( Path.GetExtension( sArchiveFile + "/" + sFileName ) );
   						FileInfo fi = new FileInfo( sArchiveFile );
   						string s = FilesWorker.FilesWorker.FormatFileLenght( fi.Length );
   						fi = new FileInfo( sTempDir+"\\"+sFileName );
   						s += " / "+FilesWorker.FilesWorker.FormatFileLenght( fi.Length );
   						item.SubItems.Add( s );
   						listViewNotFB2.Items.AddRange( new ListViewItem[]{ item } );
						tpNotFB2Files.Text = this.m_sNotFB2Files + "( " + this.m_lNotFB2Files.ToString() + " ) ";
					} else {
						++this.m_lFB2ZipFiles;
						++this.m_lFB2NotValid;
						ListViewItem item = new ListViewItem( sArchiveFile + "/" + sFileName, 0 );
   						item.ForeColor = m_ZipFB2NotValidFontColor;
						item.SubItems.Add( sMsg );
   						FileInfo fi = new FileInfo( sArchiveFile );
   						string s = FilesWorker.FilesWorker.FormatFileLenght( fi.Length );
   						fi = new FileInfo( sTempDir+"\\"+sFileName );
   						s += " / "+FilesWorker.FilesWorker.FormatFileLenght( fi.Length );
   						item.SubItems.Add( s );
						listViewNotValid.Items.AddRange( new ListViewItem[]{ item } );
						tpNotValid.Text = this.m_sNotValid + "( " + this.m_lFB2NotValid.ToString() + " ) ";
					}
				} else if( sExt.ToLower() == ".rar" ) {
					// определяем тип разархивированного файла
           			sExt = Path.GetExtension( sFileName );
					if( sExt.ToLower() != ".fb2" ) {
        				sMsg = "Тип файла: " + sExt;
          				++this.m_lNotFB2Files;
          				ListViewItem item = new ListViewItem( sArchiveFile + "/" + sFileName, 0 );
   						item.ForeColor = m_RarFontColor;
          				item.SubItems.Add( Path.GetExtension( sArchiveFile + "/" + sFileName ) );
   						FileInfo fi = new FileInfo( sArchiveFile );
   						string s = FilesWorker.FilesWorker.FormatFileLenght( fi.Length );
   						fi = new FileInfo( sTempDir+"\\"+sFileName );
   						s += " / "+FilesWorker.FilesWorker.FormatFileLenght( fi.Length );
   						item.SubItems.Add( s );
   						listViewNotFB2.Items.AddRange( new ListViewItem[]{ item } );
						tpNotFB2Files.Text = this.m_sNotFB2Files + "( " + this.m_lNotFB2Files.ToString() + " ) ";
					} else {
						++this.m_lFB2RarFiles;
						++this.m_lFB2NotValid;
						ListViewItem item = new ListViewItem( sArchiveFile + "/" + sFileName, 0 );
   						item.ForeColor = m_RarFB2NotValidFontColor;
						item.SubItems.Add( sMsg );
   						FileInfo fi = new FileInfo( sArchiveFile );
   						string s = FilesWorker.FilesWorker.FormatFileLenght( fi.Length );
   						fi = new FileInfo( sTempDir+"\\"+sFileName );
   						s += " / "+FilesWorker.FilesWorker.FormatFileLenght( fi.Length );
   						item.SubItems.Add( s );
						listViewNotValid.Items.AddRange( new ListViewItem[]{ item } );
						tpNotValid.Text = this.m_sNotValid + "( " + this.m_lFB2NotValid.ToString() + " ) ";
					}
				}
            }
			++tsProgressBar.Value;
		}
		#endregion
		
		#region Копирование, перемещение файлов
		void CopyOrMoveFilesTo( bool bCopy, string sSource, string sTarget,
		                       ListView lw, TabPage tp, string sFileType1, string sFileType,
		                       string sProgressText, string sTabPageDefText ) {
			// копировать или переместить файлы в...
			#region Код
			int nCount = lw.Items.Count;
			if( nCount == 0) {
				MessageBox.Show( "Список "+sFileType1+" пуст!\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			if( sTarget == "") {
				MessageBox.Show( "Не указана папка-приемник файлов!\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			if( sSource == sTarget ) {
				MessageBox.Show( "Папка-приемник файлов совпадает с папкой сканирования!\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			DirectoryInfo diFolder = new DirectoryInfo( sTarget );
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка не найдена: " + sTarget + "\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			string sMess = "";
			if( bCopy ) {
				sMess = "Вы действительно хотите скопировать "+sFileType+" в указанную папку?";
			} else {
				sMess = "Вы действительно хотите переместить "+sFileType+" в указанную папку?";
			}
			MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			DialogResult result;
			result = MessageBox.Show( sMess, "SharpFBTools", buttons, MessageBoxIcon.Question );
	        if(result == DialogResult.No) {
	            return;
			}
			tsslblProgress.Text = sProgressText;
			tsProgressBar.Visible = true;
			tsProgressBar.Maximum = nCount+1;
			tsProgressBar.Value = 1;
			for( int i=0; i!=nCount; ++i ) {
				string sItemText = lw.Items[i].SubItems[0].Text;
				string sFilePath = sItemText.Split('/')[0];
				string sNewPath = sTarget + sFilePath.Remove( 0, sSource.Length );
				FileInfo fi = new FileInfo( sNewPath );
				if( !fi.Directory.Exists ) {
					Directory.CreateDirectory( fi.Directory.ToString() );
				}
				string sSufix = "";
				if( File.Exists( sNewPath ) ) {
					if( cboxExistFile.SelectedIndex==0 ) {
						File.Delete( sNewPath );
					} else {
						if( chBoxAddBookID.Checked ) {
							string sExtTemp = Path.GetExtension( sFilePath ).ToLower();
							if( sExtTemp != ".fb2" ) {
								string sTempDir = Settings.Settings.GetTempDir();
								FilesWorker.FilesWorker.RemoveDir( sTempDir );
								Directory.CreateDirectory( sTempDir );
								if( sExtTemp.ToLower() == ".rar" ) {
									Archiver.Archiver.unrar( Settings.Settings.ReadUnRarPath(), sFilePath, sTempDir );
								} else {
									Archiver.Archiver.unzip( Settings.Settings.Read7zaPath(), sFilePath, sTempDir );
								}
								if( Directory.Exists( sTempDir ) ) {
									string [] files = Directory.GetFiles( sTempDir );
									try {
										sSufix = FilesWorker.StringProcessing.GetBookID( files[0] );
									} catch { }
									FilesWorker.FilesWorker.RemoveDir( sTempDir );
								}
							} else {
								try {
									sSufix = FilesWorker.StringProcessing.GetBookID( sFilePath );
								} catch { }
							}
							
						}
						if( cboxExistFile.SelectedIndex == 1 ) {
							// Добавить к создаваемому файлу очередной номер
							sSufix += "_" + FilesWorker.StringProcessing.GetFileNewNumber( sNewPath ).ToString();
						} else {
							// Добавить к создаваемому файлу дату и время
							sSufix += FilesWorker.StringProcessing.GetDateTimeExt();
						}
						
						string sFB2File = sNewPath.ToLower();
						if( sFB2File.IndexOf( ".fb2" )!=1 ) {
							sFB2File = sFB2File.Substring( 0, sFB2File.IndexOf( ".fb2" )+4 );
						}
						string sExt = sNewPath.Remove( 0, sFB2File.Length );
						if( sExt.Length == 0 ) {
							sExt = Path.GetExtension( sNewPath );
							sNewPath = sNewPath.Remove( sNewPath.Length - sExt.Length ) + sSufix + sExt;
						} else {
							sExt = Path.GetExtension( sFB2File ) + Path.GetExtension( sNewPath );
							sNewPath = sNewPath.Remove( sNewPath.Length - sExt.Length ) + sSufix + sExt;
						}
					}
				}
				
				Regex rx = new Regex( @"\\+" );
				sFilePath = rx.Replace( sFilePath, "\\" );
				if( File.Exists( sFilePath ) ) {
					if( bCopy ) {
						File.Copy( sFilePath, sNewPath );
					} else {
						File.Move( sFilePath, sNewPath );
					}
				}
				++tsProgressBar.Value;
				ssProgress.Refresh();
			}
			if( !bCopy ) {
				// только для перемещения файлов
				lw.Items.Clear();
				tp.Text	= sTabPageDefText;
				sMess = "Перемещение файлов в указанную папку завершено!";
			} else {
				sMess = "Копирование файлов в указанную папку завершено!";
			}
			lvFilesCount.Items[1].SubItems[1].Text = ( listViewNotValid.Items.Count + listViewValid.Items.Count +
														listViewNotFB2.Items.Count ).ToString();
			MessageBox.Show( sMess, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
			tsslblProgress.Text = Settings.Settings.GetReady();
			tsProgressBar.Visible = false;
			#endregion
		}
		
		void DeleteFiles( ListView lw, TabPage tp, string sFileType, string sFileType1,
		                  string sProgressText, string sTabPageDefText ) {
			// удалить файлы...
			#region Код
			int nCount = lw.Items.Count;
			if( nCount == 0) {
				MessageBox.Show( "Список "+sFileType+" пуст!\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			string sMess = "Вы действительно хотите удалить "+sFileType1+"?";
			MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			DialogResult result;
			result = MessageBox.Show( sMess, "SharpFBTools", buttons, MessageBoxIcon.Question );
	        if(result == DialogResult.No) {
	            return;
			}
			tsslblProgress.Text = sProgressText;
			tsProgressBar.Visible = true;
			tsProgressBar.Maximum = nCount+1;
			tsProgressBar.Value = 1;
			for( int i=0; i!=nCount; ++i ) {
				string sFilePath = lw.Items[i].SubItems[0].Text.Split('/')[0];
				if( File.Exists( sFilePath) ) {
					File.Delete( sFilePath );
				}
				++tsProgressBar.Value;
				ssProgress.Refresh();
			}
			lw.Items.Clear();
			tp.Text	= sTabPageDefText;
			lvFilesCount.Items[1].SubItems[1].Text = ( listViewNotValid.Items.Count + listViewValid.Items.Count +
			        			       				   listViewNotFB2.Items.Count ).ToString();
			sMess = "Удаление файлов завершено!";
			MessageBox.Show( sMess, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
			tsslblProgress.Text = Settings.Settings.GetReady();
			tsProgressBar.Visible = false;
			#endregion
		}
		#endregion
		
		#region Генерация отчетов
		void MakeReport( int nModeReport ) {
			// создание отчета заданного через nModeReport вида для разных вкладок (видов найденных файлов)
			// 0 - html; 1 - fb2; 3 - csv(csv); 4 - csv(txt)
			switch( tcResult.SelectedIndex ) {
				case 0:
					// не валидные fb2-файлы
					MakeReport( listViewNotValid, m_FB2NotValidReportEmpty, m_FB2NotValidFilesListReport, nModeReport );
					break;
				case 1:
					// валидные fb2-файлы
					MakeReport( listViewValid, m_FB2ValidReportEmpty, m_FB2ValidFilesListReport, nModeReport );
					break;
				case 2:
					// не fb2-файлы
					MakeReport( listViewNotFB2, m_NotFB2FileReportEmpty, m_NotFB2FilesListReport, nModeReport );
					break;
			}
		}
		
		private bool MakeReport( ListView lw, string sReportListEmpty, string sReportTitle, int nModeReport ) {
			// создание отчета
			string sDelem = ";";
			switch( nModeReport ) {
				case 0: // Как html-файл
					// сохранение списка не валидных файлов как html-файла
					if( lw.Items.Count < 1 ) {
						MessageBox.Show( sReportListEmpty, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
						return false;
					} else {
						sfdReport.Filter = m_HTMLFilter;
						sfdReport.FileName = "";
						DialogResult result = sfdReport.ShowDialog();
						if (result == DialogResult.OK) {
    	          			tsslblProgress.Text = m_GeneratingReport;
    	          			tsProgressBar.Visible = true;
							ReportGenerator.ReportGenerator.MakeHTMLReport( lw, sfdReport.FileName, sReportTitle, tsProgressBar, ssProgress  );
							MessageBox.Show( m_ReportSaveOk+sfdReport.FileName, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
							tsProgressBar.Visible = false;
							tsslblProgress.Text = Settings.Settings.GetReady();
	          			}
					}
					break;
				case 1: // Как fb2-файл
					// сохранение списка не валидных файлов как fb2-файла
					if( lw.Items.Count < 1 ) {
						MessageBox.Show( sReportListEmpty, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
						return false;
					} else {
						sfdReport.Filter = m_FB2Filter;
						sfdReport.FileName = "";
						DialogResult result = sfdReport.ShowDialog();
						if (result == DialogResult.OK) {
    	          			tsslblProgress.Text = m_GeneratingReport;
    	          			tsProgressBar.Visible = true;
							ReportGenerator.ReportGenerator.MakeFB2Report( lw, sfdReport.FileName, sReportTitle, tsProgressBar, ssProgress );
							MessageBox.Show( m_ReportSaveOk+sfdReport.FileName, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
							tsProgressBar.Visible = false;
							tsslblProgress.Text = Settings.Settings.GetReady();
	          			}
					}
					break;
				case 2: // Как csv-файл (.csv)
					// сохранение списка не валидных файлов как csv-файла
					if( lw.Items.Count < 1 ) {
						MessageBox.Show( sReportListEmpty, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
						return false;
					} else {
						sfdReport.Filter = m_CSV_csv_Filter;
						sfdReport.FileName = "";
						DialogResult result = sfdReport.ShowDialog();
						if (result == DialogResult.OK) {
							tsslblProgress.Text = m_GeneratingReport;
    	          			tsProgressBar.Visible = true;
    	       				ReportGenerator.ReportGenerator.MakeCSVReport( lw, sfdReport.FileName, sDelem, tsProgressBar, ssProgress );
							MessageBox.Show( m_ReportSaveOk+sfdReport.FileName, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
							tsProgressBar.Visible = false;
							tsslblProgress.Text = Settings.Settings.GetReady();
	        			}
					}
					break;
			}
			return true;
		}
		#endregion
		
		#region Обработчики событий
		void ListViewNotValidSelectedIndexChanged(object sender, EventArgs e)
		{
			// занесение ошибки валидации в бокс
			ListView.SelectedListViewItemCollection si = listViewNotValid.SelectedItems;
			foreach ( ListViewItem item in si ) {
				rеboxNotValid.Text = item.SubItems[1].Text;
			}
			if( listViewNotValid.Items.Count > 0 && listViewNotValid.SelectedItems.Count != 0 ) {
				// путь к выделенному файлу
				string s = si[0].SubItems[0].Text.Split('/')[0];
				// отределяем его расширение
				string sExt = Path.GetExtension( s );
				if( sExt.ToLower() == ".fb2" ) {
					listViewNotValid.ContextMenuStrip = cmsFB2;
				} else {
					listViewNotValid.ContextMenuStrip = cmsArchive;
				}
			} else {
				rеboxNotValid.Clear();
			}
		}
		void ListViewValidSelectedIndexChanged(object sender, EventArgs e)
		{
			ListView.SelectedListViewItemCollection si = listViewValid.SelectedItems;
			if( listViewValid.Items.Count > 0 && listViewValid.SelectedItems.Count != 0 ) {
				// путь к выделенному файлу
				string s = si[0].SubItems[0].Text.Split('/')[0];
				// отределяем его расширение
				string sExt = Path.GetExtension( s );
				if( sExt.ToLower() == ".fb2" ) {
					listViewValid.ContextMenuStrip = cmsFB2;
				} else {
					listViewValid.ContextMenuStrip = cmsArchive;
				}
			}
		}
		
		void BtnFB2NotValidCopyToClick(object sender, EventArgs e)
		{
			// задание папки для копирования невалидных fb2-файлов
			FilesWorker.FilesWorker.OpenDirDlg( tboxFB2NotValidDirCopyTo, fbdDir, "Укажите папку для не валидных fb2-файлов" );
		}
		
		void BtnFB2NotValidMoveToClick(object sender, EventArgs e)
		{
			// задание папки для перемещения невалидных fb2-файлов
			FilesWorker.FilesWorker.OpenDirDlg( tboxFB2NotValidDirMoveTo, fbdDir, "Укажите папку для не валидных fb2-файлов" );
		}
		
		void BtnFB2ValidCopyToClick(object sender, EventArgs e)
		{
			// задание папки для валидных fb2-файлов
			FilesWorker.FilesWorker.OpenDirDlg( tboxFB2ValidDirCopyTo, fbdDir, "Укажите папку для валидных fb2-файлов" );
		}
		
		void BtnFB2ValidMoveToClick(object sender, EventArgs e)
		{
			// задание папки для перемещения валидных fb2-файлов
			FilesWorker.FilesWorker.OpenDirDlg( tboxFB2ValidDirMoveTo, fbdDir, "Укажите папку для валидных fb2-файлов" );
		}
		
		void BtnNotFB2CopyToClick(object sender, EventArgs e)
		{
			// задание папки для не fb2-файлов
			FilesWorker.FilesWorker.OpenDirDlg( tboxNotFB2DirCopyTo, fbdDir, "Укажите папку для не fb2-файлов" );
		}
		
		void BtnNotFB2MoveToClick(object sender, EventArgs e)
		{
			// задание папки для перемещения не fb2-файлов
			FilesWorker.FilesWorker.OpenDirDlg( tboxNotFB2DirMoveTo, fbdDir, "Укажите папку для не fb2-файлов" );
		}
		
		void TsbtnOpenDirClick(object sender, EventArgs e)
		{
			// задание папки с fb2-файлами для сканирования
			FilesWorker.FilesWorker.OpenDirDlg( tboxSourceDir, fbdDir, "Укажите папку для проверки fb2-файлов" );
		}
		
		void TSBValidateClick(object sender, EventArgs e)
		{
			// Ввлидация fb2-файлов в выбранной папке
			tlCentral.Refresh(); // обновление контролов на форме
			string sSource = tboxSourceDir.Text.Trim();
			if( sSource == "" ) {
				MessageBox.Show( "Выберите папку для сканирования!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			DirectoryInfo diFolder = new DirectoryInfo( sSource );
			if( !diFolder.Exists ) {
				MessageBox.Show( "Папка не найдена: " + sSource, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				return;
			}
			
			DateTime dtStart = DateTime.Now;
			// инициализация контролов
			Init();
			tsProgressBar.Visible = true;
			// сортированный список всех вложенных папок
			List<string> lDirList = new List<string>();
			if( !cboxScanSubDir.Checked ) {
				// сканировать только указанную папку
				lDirList.Add( diFolder.FullName );
				lvFilesCount.Items[0].SubItems[1].Text = "1";
				lvFilesCount.Refresh();
			} else {
				// сканировать и все подпапки
				lDirList = FilesWorker.FilesWorker.DirsParser( diFolder.FullName, lvFilesCount );
				lDirList.Sort();
			}
			// сортированный список всех файлов
			tsslblProgress.Text = "Создание списка файлов:";
			tlCentral.Refresh(); // обновление контролов на форме
			List<string> lFilesList = FilesWorker.FilesWorker.AllFilesParser( lDirList, ssProgress, lvFilesCount, tsProgressBar );
			lFilesList.Sort();
			
			if( lFilesList.Count == 0 ) {
				MessageBox.Show( "Не найдено ни одного файла!\nРабота прекращена.", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				Init();
				return;
			}
			
			// проверка файлов
			tsslblProgress.Text = "Проверка найденных файлов на соответствие схеме (валидация):";
			tsProgressBar.Maximum = lFilesList.Count+1;
			tsProgressBar.Value = 1;
			ssProgress.Refresh();
			tlCentral.Refresh(); // обновление контролов на форме
			FB2Parser.FB2Validator fv2V = new FB2Parser.FB2Validator();
			string sTempDir = Settings.Settings.GetTempDir();
			foreach( string sFile in lFilesList ) {
				string sExt = Path.GetExtension( sFile );
				if( sExt.ToLower() == ".fb2" ) {
					++this.m_lFB2Files;
					ParseFB2File( sFile, fv2V );
				} else if( sExt.ToLower() == ".zip" || sExt.ToLower() == ".rar" ) {
					// очистка временной папки
					FilesWorker.FilesWorker.RemoveDir( sTempDir );
					Directory.CreateDirectory( sTempDir );
					ParseArchiveFile( sFile, fv2V, sTempDir );
				} else {
					// разные файлы
					++this.m_lNotFB2Files;
					ListViewItem item = new ListViewItem( sFile, 0 );
   					item.ForeColor = m_NotFB2FontColor;
					item.SubItems.Add( Path.GetExtension( sFile ) );
   					FileInfo fi = new FileInfo( sFile );
   					item.SubItems.Add( FilesWorker.FilesWorker.FormatFileLenght( fi.Length ) );
					listViewNotFB2.Items.AddRange( new ListViewItem[]{ item } );
					tpNotFB2Files.Text = m_sNotFB2Files + "( " + m_lNotFB2Files.ToString() + " ) " ;
					++tsProgressBar.Value;
				}
			}
			lvFilesCount.Items[2].SubItems[1].Text = m_lFB2Files.ToString();
			lvFilesCount.Items[3].SubItems[1].Text = m_lFB2ZipFiles.ToString();
			lvFilesCount.Items[4].SubItems[1].Text = m_lFB2RarFiles.ToString();
			lvFilesCount.Items[5].SubItems[1].Text = m_lNotFB2Files.ToString();
			
			DateTime dtEnd = DateTime.Now;
			string sTime = dtEnd.Subtract( dtStart ).ToString() + " (час.:мин.:сек.)";
			MessageBox.Show( "Проверка файлов на соответствие FictionBook.xsd схеме завершена!\nЗатрачено времени: "+sTime, "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
			tsslblProgress.Text = Settings.Settings.GetReady();
			tsProgressBar.Visible = false;
			// очистка временной папки
			FilesWorker.FilesWorker.RemoveDir( sTempDir );
		}
		
		void TsbtnCopyFilesToClick(object sender, EventArgs e)
		{
			// копирование файлов в зависимости от выбранной вкладки
			switch( tcResult.SelectedIndex ) {
				case 0:
					// не валидные fb2-файлы
					CopyOrMoveFilesTo( true, tboxSourceDir.Text.Trim(), tboxFB2NotValidDirCopyTo.Text.Trim(),
		                       listViewNotValid, tpNotValid, "не валидных fb2-файлов", "не валидные fb2-файлы",
		                       "Копирование не валидных fb2-файлов:", m_sNotValid );
					break;
				case 1:
					// валидные fb2-файлы
					CopyOrMoveFilesTo( true, tboxSourceDir.Text.Trim(), tboxFB2ValidDirCopyTo.Text.Trim(),
		                       listViewValid, tpValid, "валидных fb2-файлов", "валидные fb2-файлы",
		                       "Копирование валидных fb2-файлов:", m_sValid );
					break;
				case 2:
					// не fb2-файлы
					CopyOrMoveFilesTo( true, tboxSourceDir.Text.Trim(), tboxNotFB2DirCopyTo.Text.Trim(),
		                       listViewNotFB2, tpNotFB2Files, "не fb2-файлов", "не fb2-файлы",
		                       "Копирование не fb2-файлов:", m_sNotFB2Files );
					break;
			}
		}
		
		void TsbtnMoveFilesToClick(object sender, EventArgs e)
		{
			// перемещение файлов в зависимости от выбранной вкладки
			switch( tcResult.SelectedIndex ) {
				case 0:
					// не валидные fb2-файлы
					CopyOrMoveFilesTo( false, tboxSourceDir.Text.Trim(), tboxFB2NotValidDirMoveTo.Text.Trim(),
		                       listViewNotValid, tpNotValid, "не валидных fb2-файлов", "не валидные fb2-файлы",
		                       "Перемещение не валидных fb2-файлов:", m_sNotValid );
					break;
				case 1:
					// валидные fb2-файлы
					CopyOrMoveFilesTo( false, tboxSourceDir.Text.Trim(), tboxFB2ValidDirMoveTo.Text.Trim(),
		                       listViewValid, tpValid, "валидных fb2-файлов", "валидные fb2-файлы",
		                       "Перемещение валидных fb2-файлов:", m_sValid );
					break;
				case 2:
					// не fb2-файлы
					CopyOrMoveFilesTo( false, tboxSourceDir.Text.Trim(), tboxNotFB2DirMoveTo.Text.Trim(),
		                       listViewNotFB2, tpNotFB2Files, "не fb2-файлов", "не fb2-файлы",
		                       "Перемещение не fb2-файлов:", m_sNotFB2Files );
					break;
			}
		}
		
		void TsbtnDeleteFilesClick(object sender, EventArgs e)
		{
			// удаление файлов в зависимости от выбранной вкладки
			switch( tcResult.SelectedIndex ) {
				case 0:
					// не валидные fb2-файлы
					DeleteFiles( listViewNotValid, tpNotValid, "не валидных fb2-файлов", "не валидные fb2-файлы",
		                       "Удаление не валидных fb2-файлов:", m_sNotValid );
					break;
				case 1:
					// валидные fb2-файлы
					DeleteFiles( listViewValid, tpValid, "валидных fb2-файлов", "валидные fb2-файлы",
		                       "Удаление валидных fb2-файлов:", m_sValid );
					break;
				case 2:
					// не fb2-файлы
					DeleteFiles( listViewNotFB2, tpNotFB2Files, "не fb2-файлов", "не fb2-файлы",
		                       "Удаление не fb2-файлов:", m_sNotFB2Files );
					break;
			}
		}
		
		void TsmiReportAsHTMLClick(object sender, EventArgs e)
		{
			// отчет в виде html-файла
			MakeReport( 0 );
		}
		
		void TsmiReportAsFB2Click(object sender, EventArgs e)
		{
			// отчет в виде fb2-файла
			MakeReport( 1 );
		}
		
		void TsmiReportAsCSV_CSVClick(object sender, EventArgs e)
		{
			// отчет в виде cvs-файла
			MakeReport( 2 );
		}

		void TsmiEditInTextEditorClick(object sender, EventArgs e)
		{
			// редактировать выделенный файл в текстовом редакторе
			// читаем путь к текстовому редактору из настроек
			string sTFB2Path = Settings.Settings.ReadTextFB2EPath();
			string sTitle = "SharpFBTools - Открытие файла в текстовом редакторе";
			if( !File.Exists( sTFB2Path ) ) {
				MessageBox.Show( "Не могу найти текстовый редактор \""+sTFB2Path+"\"!\nПроверьте, правильно ли задан путь в Настройках.", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 && l.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = l.SelectedItems;
				string sFilePath = si[0].SubItems[0].Text.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: "+sFilePath+"\" не найден!", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				FilesWorker.FilesWorker.StartFile( "\""+sTFB2Path+"\"" + " " + "\""+sFilePath+"\"" );
			}
		}
		
		void TsmiEditInFB2EditorClick(object sender, EventArgs e)
		{
			// редактировать выделенный файл в fb2-редакторе
			// читаем путь к FBE из настроек
			string sFBEPath = Settings.Settings.ReadFBEPath();
			string sTitle = "SharpFBTools - Открытие файла в fb2-редакторе";
			if( !File.Exists( sFBEPath ) ) {
				MessageBox.Show( "Не могу найти fb2-редактор \""+sFBEPath+"\"!\nПроверьте, правильно ли задан путь в Настройках.", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 && l.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = l.SelectedItems;
				string sFilePath = si[0].SubItems[0].Text.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: "+sFilePath+"\" не найден!", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				FilesWorker.FilesWorker.StartFile( "\""+sFBEPath+"\"" + " " + "\""+sFilePath+"\"" );
			}
		}
		
		void TsmiVienInReaderClick(object sender, EventArgs e)
		{
			// запустить файл в fb2-читалке (Просмотр)
			// читаем путь к читалке из настроек
			string sFBReaderPath = Settings.Settings.ReadFBReaderPath();
			string sTitle = "SharpFBTools - Открытие папки для файла";
			if( !File.Exists( sFBReaderPath ) ) {
				MessageBox.Show( "Не могу найти Читалку \""+sFBReaderPath+"\"!\nПроверьте, правильно ли задан путь в Настройках.", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 && l.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = l.SelectedItems;
				string sFilePath = si[0].SubItems[0].Text.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: "+sFilePath+"\" не найден!", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				FilesWorker.FilesWorker.StartFile( "\""+sFBReaderPath+"\"" + " " + "\""+sFilePath+"\"" );
			}
		}
		
		void TsmiOpenFileDirClick(object sender, EventArgs e)
		{
			// Открыть папку для выделенного файла
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 && l.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = l.SelectedItems;
				FileInfo fi = new FileInfo( si[0].SubItems[0].Text.Split('/')[0] );
				string sDir = fi.Directory.ToString();
				if( !Directory.Exists( sDir ) ) {
					MessageBox.Show( "Папка: "+sDir+"\" не найдена!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				FilesWorker.FilesWorker.ShowDir( sDir );
			}
		}
		
		void TsmiOpenFileInArchivatorClick(object sender, EventArgs e)
		{
			// Запустить выделенный файл в архиваторе
			// читаем путь к архиватору из настроек
			string sWinRarPath = Settings.Settings.ReadWinRARPath();
			string sTitle = "SharpFBTools - Запуск файла в Архиваторе";
			if( !File.Exists( sWinRarPath ) ) {
				MessageBox.Show( "Не могу найти WinRAR \""+sWinRarPath+"\"!\nПроверьте, правильно ли задан путь в Настройках.", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
				return;
			}
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 && l.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = l.SelectedItems;
				string sFilePath = si[0].SubItems[0].Text.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: "+sFilePath+"\" не найден!", sTitle, MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				FilesWorker.FilesWorker.StartFile( "\""+sWinRarPath+"\"" + " " + "\""+sFilePath+"\"" );
			}
		}
		
		void TsmiFileReValidateClick(object sender, EventArgs e)
		{
			// Повторная Проверка выбранного fb2-файла или архива (Валидация)
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 && l.SelectedItems.Count != 0 ) {
				DateTime dtStart = DateTime.Now;
				string sTempDir = Settings.Settings.GetTempDir();
				ListView.SelectedListViewItemCollection si = l.SelectedItems;
				string sSelectedItemText = si[0].SubItems[0].Text;
				string sFilePath = sSelectedItemText.Split('/')[0];
				if( !File.Exists( sFilePath ) ) {
					MessageBox.Show( "Файл: "+sFilePath+"\" не найден!", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Information );
					return;
				}
				MessageBoxIcon mbi = MessageBoxIcon.Information;
				string sExt = Path.GetExtension( sFilePath );
				string sMsg = "";
				string sErrorMsg = "СООБЩЕНИЕ ОБ ОШИБКЕ:";
				string sOkMsg = "ОШИБОК НЕТ - ФАЙЛ ВАЛИДЕН";
				string sMoveNotValToVal = "Путь к этому файлу перенесен из Списка не валидных fb2-файлов в Список валидных.";
				string sMoveValToNotVal = "Путь к этому файлу перенесен из Списка валидных fb2-файлов в Список не валидных.";
				FB2Parser.FB2Validator fv2V = new FB2Parser.FB2Validator();
				if( sExt.ToLower() == ".fb2" ) {
					// для несжатого fb2-файла
					sMsg = fv2V.ValidatingFB2File( sFilePath );
					if ( sMsg == "" ) {
           				// файл валидный
           				mbi = MessageBoxIcon.Information;
           				if( l.Name == "listViewNotValid" ) {
           					sErrorMsg = sOkMsg + ":";
           					sMsg = sMoveNotValToVal;
							listViewNotValid.Items[ l.SelectedItems[0].Index ].Remove();
							rеboxNotValid.Text = "";
							tpNotValid.Text = m_sNotValid + "( " + listViewNotValid.Items.Count.ToString() + " ) ";
							ListViewItem item = new ListViewItem( sSelectedItemText, 0 );
	   						item.ForeColor = m_FB2ValidFontColor;
							FileInfo fi = new FileInfo( sSelectedItemText );
   							item.SubItems.Add( FilesWorker.FilesWorker.FormatFileLenght( fi.Length ) );
							listViewValid.Items.AddRange( new ListViewItem[]{ item } );
							tpValid.Text = m_sValid + "( " + listViewValid.Items.Count.ToString() + " ) ";
           				} else {
           					sErrorMsg = sOkMsg;
           				}
					} else {
						// файл не валидный
						mbi = MessageBoxIcon.Error;
						if( l.Name == "listViewNotValid" ) {
							l.Items[ l.SelectedItems[0].Index ].SubItems[1].Text = sMsg;
							rеboxNotValid.Text = sMsg;
						} else if( l.Name == "listViewValid" ) {
							// валидный файл был как-то "испорчен"
							listViewValid.Items[ l.SelectedItems[0].Index ].Remove();
							tpValid.Text = m_sValid + "( " + listViewValid.Items.Count.ToString() + " ) ";
							ListViewItem item = new ListViewItem( sSelectedItemText, 0 );
   							item.ForeColor = m_FB2NotValidFontColor;
   							item.SubItems.Add( sMsg );
							FileInfo fi = new FileInfo( sSelectedItemText );
			   				item.SubItems.Add( FilesWorker.FilesWorker.FormatFileLenght( fi.Length ) );
							listViewNotValid.Items.AddRange( new ListViewItem[]{ item } );
							tpNotValid.Text = m_sNotValid + "( " + listViewNotValid.Items.Count.ToString() + " ) ";
							sMsg += "\n\n" + sMoveValToNotVal;
						}
					}
				} else if( sExt.ToLower() == ".zip" || sExt.ToLower() == ".rar" ) {
					// очистка временной папки
					FilesWorker.FilesWorker.RemoveDir( sTempDir );
					Directory.CreateDirectory( sTempDir );
					if( sExt.ToLower() == ".zip" ) {
						Archiver.Archiver.unzip( Settings.Settings.Read7zaPath(), sFilePath, sTempDir );
					} else if( sExt.ToLower() == ".rar" ) {
						Archiver.Archiver.unrar( Settings.Settings.ReadUnRarPath(), sFilePath, sTempDir );
					}
					string [] files = Directory.GetFiles( sTempDir );
					if( files.Length > 0 ) {
						sMsg = fv2V.ValidatingFB2File( files[0] );
						if ( sMsg == "" ) {
							mbi = MessageBoxIcon.Information;
        			   		//  Запакованный fb2-файл валидный
        			   		if( l.Name == "listViewNotValid" ) {
        			   			sErrorMsg = sOkMsg + ":";
           						sMsg = sMoveNotValToVal;
								listViewNotValid.Items[ l.SelectedItems[0].Index ].Remove();
								rеboxNotValid.Text = "";
								tpNotValid.Text = m_sNotValid + "( " + listViewNotValid.Items.Count.ToString() + " ) ";
								string sFB2FileName = sSelectedItemText.Split('/')[1];
								ListViewItem item = new ListViewItem( sSelectedItemText , 0 );
								if( sExt.ToLower() == ".zip" ) {
									item.ForeColor = m_ZipFB2ValidFontColor;
								} else if( sExt.ToLower() == ".rar" ) {
									item.ForeColor = m_RarFB2ValidFontColor;
								}
								FileInfo fi = new FileInfo( sFilePath );
				   				string s = FilesWorker.FilesWorker.FormatFileLenght( fi.Length );
				   				fi = new FileInfo( sTempDir+"\\"+sFB2FileName );
				   				s += " / "+FilesWorker.FilesWorker.FormatFileLenght( fi.Length );
				   				item.SubItems.Add( s );
								listViewValid.Items.AddRange( new ListViewItem[]{ item } );
								tpValid.Text = m_sValid + "( " + listViewValid.Items.Count.ToString() + " ) ";
							} else {
           						sErrorMsg = sOkMsg;
           					}
           				} else {
							mbi = MessageBoxIcon.Error;
							// Запакованный fb2-файл невалиден
							if( l.Name == "listViewNotValid" ) {
								l.Items[ l.SelectedItems[0].Index ].SubItems[1].Text = sMsg;
								rеboxNotValid.Text = sMsg;
							} else if( l.Name == "listViewValid" ) {
								// валидный файл в архиве был как-то "испорчен"
								listViewValid.Items[ l.SelectedItems[0].Index ].Remove();
								tpValid.Text = m_sValid + "( " + listViewValid.Items.Count.ToString() + " ) ";
								string sFB2FileName = sSelectedItemText.Split('/')[1];
								ListViewItem item = new ListViewItem( sSelectedItemText , 0 );
								if( sExt.ToLower() == ".zip" ) {
									item.ForeColor = m_ZipFB2NotValidFontColor;
								} else if( sExt.ToLower() == ".rar" ) {
									item.ForeColor = m_RarFB2NotValidFontColor;
								}
								item.SubItems.Add( sMsg );
								FileInfo fi = new FileInfo( sFilePath );
				   				string s = FilesWorker.FilesWorker.FormatFileLenght( fi.Length );
				   				fi = new FileInfo( sTempDir+"\\"+sFB2FileName );
				   				s += " / "+FilesWorker.FilesWorker.FormatFileLenght( fi.Length );
				   				item.SubItems.Add( s );
								listViewNotValid.Items.AddRange( new ListViewItem[]{ item } );
								tpNotValid.Text = m_sNotValid + "( " + listViewNotValid.Items.Count.ToString() + " ) ";
								sMsg += "\n\n" + sMoveValToNotVal;
							}
						}
					}
				}
				DateTime dtEnd = DateTime.Now;
				string sTime = dtEnd.Subtract( dtStart ).ToString() + " (час.:мин.:сек.)";
				// очистка временной папки
				FilesWorker.FilesWorker.RemoveDir( sTempDir );
				MessageBox.Show( "Повторная проверка выделенного файла на соответствие FictionBook.xsd схеме завершена.\nЗатрачено времени: "+sTime+"\n\nФайл: \""+sFilePath+"\"\n\n"+sErrorMsg+"\n"+sMsg, "SharpFBTools - "+sErrorMsg, MessageBoxButtons.OK, mbi );
			}
		}
		
		void TsmiDeleteFileFromDiskClick(object sender, EventArgs e)
		{
			// удаление выделенного файла с диска
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 && l.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = l.SelectedItems;
				string sFilePath = si[0].SubItems[0].Text.Split('/')[0];
				string sTitle = "SharpFBTools - Удаление файла с диска";
				if( !File.Exists( sFilePath ) ) {
					if( MessageBox.Show( "Файл: "+sFilePath+"\" не найден!\nУдалить путь к этому файлу из списка?",
					                    sTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes ) {
						l.Items[ l.SelectedItems[0].Index ].Remove();
					}
				} else {
					if( MessageBox.Show( "Вы действительно хотите удалить файл: "+sFilePath+"\" с диска?", sTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question ) == DialogResult.Yes ) {
						File.Delete( sFilePath );
						l.Items[ l.SelectedItems[0].Index ].Remove();
					}
				}
				switch( tcResult.SelectedIndex ) {
					case 0:
						// не валидные fb2-файлы
						tpNotValid.Text = m_sNotValid + "( " + l.Items.Count.ToString() + " ) ";
						break;
					case 1:
						// валидные fb2-файлы
						tpValid.Text = m_sValid + "( " + l.Items.Count.ToString() + " ) ";
						break;
					case 2:
						// не fb2-файлы
						tpNotFB2Files.Text = m_sNotFB2Files + "( " + l.Items.Count.ToString() + " ) ";
						break;
				}
			}
		}
		
		void ListViewNotValidDoubleClick(object sender, EventArgs e)
		{
			// выбор варианта работы по двойному щелчку на списках
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 && l.SelectedItems.Count != 0 ) {
				ListView.SelectedListViewItemCollection si = l.SelectedItems;
				string sSelectedItemText = si[0].SubItems[0].Text;
				string sFilePath = sSelectedItemText.Split('/')[0];
				string sExt = Path.GetExtension( sFilePath );
				if( sExt.ToLower() == ".fb2" ) {
					switch ( Settings.Settings.ReadValidatorFB2SelectedIndex() ) {
						case 0: // Повторная Валидация
							TsmiFileReValidateClick( sender, e );
							break;
						case 1: // Править в текстовом редакторе
							TsmiEditInTextEditorClick( sender, e );
							break;
						case 2: // Править в fb2-редакторе
							TsmiEditInFB2EditorClick( sender, e );
							break;
						case 3: // Просмотр в Читалке
							TsmiVienInReaderClick( sender, e );
							break;
						case 4: // Открыть папку файла
							TsmiOpenFileDirClick( sender, e );
							break;
					}
				} else if( sExt.ToLower() == ".zip" || sExt.ToLower() == ".rar" ) {
					switch ( Settings.Settings.ReadValidatorFB2ArchiveSelectedIndex() ) {
						case 0: // Повторная Валидация
							TsmiFileReValidateClick( sender, e );
							break;
						case 1: // Запустить в Архиваторе
							TsmiOpenFileInArchivatorClick( sender, e );
							break;
						case 2: // Открыть папку файла
							TsmiOpenFileDirClick( sender, e );
							break;
					}
				}
			}
		}
		
		void ListViewNotValidKeyPress(object sender, KeyPressEventArgs e)
		{
			// выбор варианта работы по нажатию Enter на списках
			ListView l = GetCurrentListWiew();
			if( l.Items.Count > 0 && l.SelectedItems.Count != 0 ) {
				if ( e.KeyChar == (char)Keys.Return ) {
            		e.Handled = true;
            		ListView.SelectedListViewItemCollection si = l.SelectedItems;
					string sSelectedItemText = si[0].SubItems[0].Text;
					string sFilePath = sSelectedItemText.Split('/')[0];
					string sExt = Path.GetExtension( sFilePath );
					if( sExt.ToLower() == ".fb2" ) {
						switch ( Settings.Settings.ReadValidatorFB2SelectedIndexPE() ) {
							case 0: // Повторная Валидация
								TsmiFileReValidateClick( sender, e );
								break;
							case 1: // Править в текстовом редакторе
								TsmiEditInTextEditorClick( sender, e );
								break;
							case 2: // Править в fb2-редакторе
								TsmiEditInFB2EditorClick( sender, e );
								break;
							case 3: // Просмотр в Читалке
								TsmiVienInReaderClick( sender, e );
								break;
							case 4: // Открыть папку файла
								TsmiOpenFileDirClick( sender, e );
								break;
						}
					} else if( sExt.ToLower() == ".zip" || sExt.ToLower() == ".rar" ) {
						switch ( Settings.Settings.ReadValidatorFB2ArchiveSelectedIndexPE() ) {
							case 0: // Повторная Валидация
								TsmiFileReValidateClick( sender, e );
								break;
							case 1: // Запустить в Архиваторе
								TsmiOpenFileInArchivatorClick( sender, e );
								break;
							case 2: // Открыть папку файла
								TsmiOpenFileDirClick( sender, e );
								break;
						}
					}
        		}
			}
		}

		void TsmiMakeNotValidFileListClick(object sender, EventArgs e)
		{
			// сохранение списка Не валидных файлов
			ReportGenerator.ReportGenerator.SaveFilesList( listViewNotValid, sfdReport, m_TXTFilter,
			              ssProgress,  tsslblProgress, tsProgressBar, m_FB2NotValidFilesListReport,
			              "Нет ни одного Не валидного файла!", "Создание списка Не валидных файлов завершено.", Settings.Settings.GetReady() );
		}
		
		void TsmiMakeValidFileListClick(object sender, EventArgs e)
		{
			// сохранение списка Валидных файлов
			ReportGenerator.ReportGenerator.SaveFilesList( listViewValid, sfdReport, m_TXTFilter, ssProgress,
			              tsslblProgress, tsProgressBar, m_FB2ValidFilesListReport,
			              "Нет ни одного Валидного файла!", "Создание списка Валидных файлов завершено.", Settings.Settings.GetReady() );
		}
		
		void CboxExistFileSelectedIndexChanged(object sender, EventArgs e)
		{
			chBoxAddBookID.Enabled = ( cboxExistFile.SelectedIndex != 0 );
		}	
		
		void TboxSourceDirTextChanged(object sender, EventArgs e)
		{
			Settings.Settings.SetScanDir( tboxSourceDir.Text );
		}
		
		void TboxFB2NotValidDirCopyToTextChanged(object sender, EventArgs e)
		{
			Settings.Settings.SetFB2NotValidDirCopyTo( tboxFB2NotValidDirCopyTo.Text );
		}
		
		void TboxFB2NotValidDirMoveToTextChanged(object sender, EventArgs e)
		{
			Settings.Settings.SetFB2NotValidDirMoveTo( tboxFB2NotValidDirMoveTo.Text );
		}
		
		void TboxFB2ValidDirCopyToTextChanged(object sender, EventArgs e)
		{
			Settings.Settings.SetFB2ValidDirCopyTo( tboxFB2ValidDirCopyTo.Text );
		}
		
		void TboxFB2ValidDirMoveToTextChanged(object sender, EventArgs e)
		{
			Settings.Settings.SetFB2ValidDirMoveTo( tboxFB2ValidDirMoveTo.Text );
		}
		
		void TboxNotFB2DirCopyToTextChanged(object sender, EventArgs e)
		{
			Settings.Settings.SetNotFB2DirCopyTo( tboxNotFB2DirCopyTo.Text );
		}
		
		void TboxNotFB2DirMoveToTextChanged(object sender, EventArgs e)
		{
			Settings.Settings.SetNotFB2DirMoveTo( tboxNotFB2DirMoveTo.Text );
		}
		#endregion
	}
}
