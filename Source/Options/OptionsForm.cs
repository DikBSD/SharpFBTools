/*
 * Created by SharpDevelop.
 * User: DikBSD
 * Date: 05.04.2009
 * Time: 14:31
 * 
 * License: GPL 2.1
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.IO;

using filesWorker = Core.FilesWorker.FilesWorker;

namespace Options
{
	/// <summary>
	/// Description of OptionsForm.
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
			// Валидатор
			DefValidator();
			// Менеджер Файлов
			// основные для Менеджера Файлов
			DefFMGeneral();
			// Папки для "проблемных" файлов - для Менеджера Файлов
			DefFMProblemFilesDir();
			// название папки шаблонного тэга без данных
			DefFMDirNameForTagNotData();
			// название Групп Жанров
			DefFMGenresGroups();
			// читаем сохраненные настройки, если они есть
			ReadSettings();
			#endregion
		}
		
		#region Закрытые Вспомогательные методы
		private void DefGeneral() {
			// общие настройки
			tboxWinRarPath.Text	= Settings.Settings.GetDefWinRARPath();
			tboxRarPath.Text	= Settings.Settings.GetDefRarPath();
			tboxUnRarPath.Text	= Settings.Settings.GetDefUnRARPath();
			tbox7zaPath.Text	= Settings.Settings.GetDef7zaPath();
			tboxFBEPath.Text	= Settings.Settings.GetDefFBEPath();
			tboxTextEPath.Text	= Settings.Settings.GetDefTFB2Path();
			tboxReaderPath.Text = Settings.Settings.GetDefFBReaderPath();
			tboxDiffPath.Text 	= Settings.Settings.GetDiffPath();
		}
		private void DefValidator() {
			// Валидатор
			cboxValidatorForFB2.SelectedIndex			= Settings.SettingsValidator.GetDefValidatorFB2SelectedIndex();
			cboxValidatorForFB2Archive.SelectedIndex	= Settings.SettingsValidator.GetDefValidatorFB2ArchiveSelectedIndex();
			cboxValidatorForFB2PE.SelectedIndex			= Settings.SettingsValidator.GetDefValidatorFB2SelectedIndexPE();
			cboxValidatorForFB2ArchivePE.SelectedIndex	= Settings.SettingsValidator.GetDefValidatorFB2ArchiveSelectedIndexPE();
		}
		private void DefFMGeneral() {
			// основные для Менеджера Файлов
			rbtnFMFB21.Checked = Settings.SettingsFM.GetDefFMrbtnGenreFB21Cheked();
			rbtnFMFB22.Checked = Settings.SettingsFM.GetDefFMrbtnGenreFB22Cheked();
			chBoxTranslit.Checked = Settings.SettingsFM.GetDefFMchBoxTranslitCheked();
			chBoxStrict.Checked = Settings.SettingsFM.GetDefFMchBoxStrictCheked();
			cboxSpace.SelectedIndex = Settings.SettingsFM.GetDefFMcboxSpaceSelectedIndex();
			chBoxToArchive.Checked = Settings.SettingsFM.GetDefFMchBoxToArchiveCheked();
			cboxArchiveType.SelectedIndex = Settings.SettingsFM.GetDefFMcboxArchiveTypeSelectedIndex();
			cboxFileExist.SelectedIndex = Settings.SettingsFM.GetDefFMcboxFileExistSelectedIndex();
			chBoxDelFB2Files.Checked = Settings.SettingsFM.GetDefFMchBoxDelFB2FilesCheked();
			rbtnAsIs.Checked = Settings.SettingsFM.GetDefFMrbtnAsIsCheked();
			rbtnAsSentence.Checked = Settings.SettingsFM.GetDefFMrbtnAsSentenceCheked();
			rbtnLower.Checked = Settings.SettingsFM.GetDefFMrbtnLowerCheked();
			rbtnUpper.Checked = Settings.SettingsFM.GetDefFMrbtnUpperCheked();
			rbtnGenreOne.Checked = Settings.SettingsFM.GetDefFMrbtnGenreOneCheked();
			rbtnGenreAll.Checked = Settings.SettingsFM.GetDefFMrbtnGenreAllCheked();
			rbtnAuthorOne.Checked = Settings.SettingsFM.GetDefFMrbtnAuthorOneCheked();
			rbtnAuthorAll.Checked = Settings.SettingsFM.GetDefFMrbtnAuthorAllCheked();
			rbtnGenreSchema.Checked = Settings.SettingsFM.GetDefFMrbtnGenreSchemaCheked();
			rbtnGenreText.Checked = Settings.SettingsFM.GetDefFMrbtnGenreTextCheked();
			chBoxAddToFileNameBookID.Checked = Settings.SettingsFM.GetDefFMchBoxAddToFileNameBookIDChecked();
			rbtnFMAllFB2.Checked		= Settings.SettingsFM.GetDefFMrbtnFMAllFB2Cheked();
			rbtnFMOnleValidFB2.Checked	= Settings.SettingsFM.GetDefFMrbtnFMOnleValidFB2Cheked();
		}
		private void DefFMProblemFilesDir() {
			// Папки для "проблемных" файлов - для Менеджера Файлов
			txtBoxFB2NotReadDir.Text	= Settings.SettingsFM.GetDefFMFB2NotReadDir();
			txtBoxFB2LongPathDir.Text	= Settings.SettingsFM.GetDefFMFB2LongPathDir();
			txtBoxFB2NotValidDir.Text	= Settings.SettingsFM.GetDefFMFB2NotValidDir();
			txtBoxArchNotOpenDir.Text	= Settings.SettingsFM.GetDefFMArchNotOpenDir();
		}
		private void DefFMDirNameForTagNotData() {
			// название папки шаблонного тэга без данных
			txtBoxFMNoGenreGroup.Text	= Settings.SettingsFM.GetDefFMNoGenreGroup();
			txtBoxFMNoGenre.Text		= Settings.SettingsFM.GetDefFMNoGenre();
			txtBoxFMNoLang.Text			= Settings.SettingsFM.GetDefFMNoLang();
			txtBoxFMNoFirstName.Text	= Settings.SettingsFM.GetDefFMNoFirstName();
			txtBoxFMNoMiddleName.Text	= Settings.SettingsFM.GetDefFMNoMiddleName();
			txtBoxFMNoLastName.Text		= Settings.SettingsFM.GetDefFMNoLastName();
			txtBoxFMNoNickName.Text		= Settings.SettingsFM.GetDefFMNoNickName();
			txtBoxFMNoBookTitle.Text	= Settings.SettingsFM.GetDefFMNoBookTitle();
			txtBoxFMNoSequence.Text		= Settings.SettingsFM.GetDefFMNoSequence();
			txtBoxFMNoNSequence.Text	= Settings.SettingsFM.GetDefFMNoNSequence();
		}
		
		private void DefFMGenresGroups() {
			// название Групп Жанров
			txtboxFMsf.Text			= Settings.SettingsFM.GetDefFMGenresGroupSf();
			txtboxFMdetective.Text	= Settings.SettingsFM.GetDefFMGenresGroupDetective();
			txtboxFMprose.Text		= Settings.SettingsFM.GetDefFMGenresGroupProse();
			txtboxFMlove.Text		= Settings.SettingsFM.GetDefFMGenresGroupLove();
			txtboxFMadventure.Text	= Settings.SettingsFM.GetDefFMGenresGroupAdventure();
			txtboxFMchildren.Text	= Settings.SettingsFM.GetDefFMGenresGroupChildren();
			txtboxFMpoetry.Text		= Settings.SettingsFM.GetDefFMGenresGroupPoetry();
			txtboxFMantique.Text	= Settings.SettingsFM.GetDefFMGenresGroupAntique();
			txtboxFMscience.Text	= Settings.SettingsFM.GetDefFMGenresGroupScience();
			txtboxFMcomputers.Text	= Settings.SettingsFM.GetDefFMGenresGroupComputers();
			txtboxFMreference.Text	= Settings.SettingsFM.GetDefFMGenresGroupReference();
			txtboxFMnonfiction.Text	= Settings.SettingsFM.GetDefFMGenresGroupNonfiction();
			txtboxFMreligion.Text	= Settings.SettingsFM.GetDefFMGenresGroupReligion();
			txtboxFMhumor.Text		= Settings.SettingsFM.GetDefFMGenresGroupHumor();
			txtboxFMhome.Text		= Settings.SettingsFM.GetDefFMGenresGroupHome();
			txtboxFMbusiness.Text	= Settings.SettingsFM.GetDefFMGenresGroupBusiness();
		}

		private void ReadSettings() {
			// чтение настроек из xml-файла
			#region Код
			string sSettings = Settings.Settings.GetSettingsPath();
			if( !File.Exists( sSettings ) ) return;
			XmlReaderSettings settings = new XmlReaderSettings();
			settings.IgnoreWhitespace = true;
			using ( XmlReader reader = XmlReader.Create( sSettings, settings ) ) {
				// Общее 
				try {
					reader.ReadToFollowing("WinRar");
					if (reader.HasAttributes ) {
						tboxWinRarPath.Text = reader.GetAttribute("WinRarPath");
						tboxRarPath.Text = reader.GetAttribute("RarPath");
						tboxUnRarPath.Text = reader.GetAttribute("UnRarPath");
					}
					reader.ReadToFollowing("A7za");
					if (reader.HasAttributes ) {
						tbox7zaPath.Text = reader.GetAttribute("A7zaPath");
					}
					reader.ReadToFollowing("Editors");
					if (reader.HasAttributes ) {
						tboxFBEPath.Text = reader.GetAttribute("FBEPath");
						tboxTextEPath.Text = reader.GetAttribute("TextFB2EPath");
					}
					reader.ReadToFollowing("Reader");
					if (reader.HasAttributes ) {
						tboxReaderPath.Text = reader.GetAttribute("FBReaderPath");
					}
					reader.ReadToFollowing("Diff");
					if (reader.HasAttributes ) {
						tboxDiffPath.Text = reader.GetAttribute("DiffPath");
					}
					// Валидатор
					reader.ReadToFollowing("ValidatorDoubleClick");
					if (reader.HasAttributes ) {
						cboxValidatorForFB2.SelectedIndex = Convert.ToInt16( reader.GetAttribute("cboxValidatorForFB2SelectedIndex") );
						cboxValidatorForFB2Archive.SelectedIndex = Convert.ToInt16( reader.GetAttribute("cboxValidatorForFB2ArchiveSelectedIndex") );
					}
					reader.ReadToFollowing("ValidatorPressEnter");
					if (reader.HasAttributes ) {
						cboxValidatorForFB2PE.SelectedIndex = Convert.ToInt16( reader.GetAttribute("cboxValidatorForFB2SelectedIndexPE") );
						cboxValidatorForFB2ArchivePE.SelectedIndex = Convert.ToInt16( reader.GetAttribute("cboxValidatorForFB2ArchiveSelectedIndexPE") );
					}
					// Менеджер Файлов
					reader.ReadToFollowing("Register");
					if (reader.HasAttributes ) {
						rbtnAsIs.Checked = Convert.ToBoolean( reader.GetAttribute("rbtnAsIsChecked") );
						rbtnAsSentence.Checked = Convert.ToBoolean( reader.GetAttribute("rbtnAsSentenceChecked") );
						rbtnLower.Checked = Convert.ToBoolean( reader.GetAttribute("rbtnLowerChecked") );
						rbtnUpper.Checked = Convert.ToBoolean( reader.GetAttribute("rbtnUpperChecked") );
					}
					reader.ReadToFollowing("Translit");
					if (reader.HasAttributes ) {
						chBoxTranslit.Checked = Convert.ToBoolean( reader.GetAttribute("chBoxTranslitChecked") );
					}
					reader.ReadToFollowing("Strict");
					if (reader.HasAttributes ) {
						chBoxStrict.Checked = Convert.ToBoolean( reader.GetAttribute("chBoxStrictChecked") );
					}
					reader.ReadToFollowing("Space");
					if (reader.HasAttributes ) {
						cboxSpace.SelectedIndex = Convert.ToInt16( reader.GetAttribute("cboxSpaceSelectedIndex") );
					}
					reader.ReadToFollowing("Archive");
					if (reader.HasAttributes ) {
						chBoxToArchive.Checked = Convert.ToBoolean( reader.GetAttribute("chBoxToArchiveChecked") );
						cboxArchiveType.SelectedIndex = Convert.ToInt16( reader.GetAttribute("cboxArchiveTypeSelectedIndex") );
					}
					reader.ReadToFollowing("IsFileExist");
					if (reader.HasAttributes ) {
						cboxFileExist.SelectedIndex = Convert.ToInt16( reader.GetAttribute("cboxFileExistSelectedIndex") );
					}
					reader.ReadToFollowing("AddToFileNameBookID");
					if (reader.HasAttributes ) {
						chBoxAddToFileNameBookID.Checked = Convert.ToBoolean( reader.GetAttribute("chBoxAddToFileNameBookIDChecked") );
					}
					reader.ReadToFollowing("FileDelete");
					if (reader.HasAttributes ) {
						chBoxDelFB2Files.Checked = Convert.ToBoolean( reader.GetAttribute("chBoxDelFB2FilesChecked") );
					}
					reader.ReadToFollowing("AuthorsToDirs");
					if (reader.HasAttributes ) {
						rbtnAuthorOne.Checked = Convert.ToBoolean( reader.GetAttribute("rbtnAuthorOneChecked") );
						rbtnAuthorAll.Checked = Convert.ToBoolean( reader.GetAttribute("rbtnAuthorAllChecked") );
					}
					reader.ReadToFollowing("GenresToDirs");
					if (reader.HasAttributes ) {
						rbtnGenreOne.Checked = Convert.ToBoolean( reader.GetAttribute("rbtnGenreOneChecked") );
						rbtnGenreAll.Checked = Convert.ToBoolean( reader.GetAttribute("rbtnGenreAllChecked") );
					}
					reader.ReadToFollowing("GenresType");
					if (reader.HasAttributes ) {
						rbtnGenreSchema.Checked = Convert.ToBoolean( reader.GetAttribute("rbtnGenreSchemaChecked") );
						rbtnGenreText.Checked = Convert.ToBoolean( reader.GetAttribute("rbtnGenreTextChecked") );
					}
					reader.ReadToFollowing("FMGenresScheme");
					if (reader.HasAttributes ) {
						rbtnFMFB21.Checked = Convert.ToBoolean( reader.GetAttribute("rbtnFMFB21Checked") );
						rbtnFMFB22.Checked = Convert.ToBoolean( reader.GetAttribute("rbtnFMFB22Checked") );
					}
					reader.ReadToFollowing("SortType");
					if (reader.HasAttributes ) {
						rbtnFMAllFB2.Checked		= Convert.ToBoolean( reader.GetAttribute("rbtnFMAllFB2Checked") );
						rbtnFMOnleValidFB2.Checked	= Convert.ToBoolean( reader.GetAttribute("rbtnFMOnleValidFB2Checked") );
					}
					reader.ReadToFollowing("FB2NotReadDir");
					if (reader.HasAttributes ) {
						txtBoxFB2NotReadDir.Text = reader.GetAttribute("txtBoxFB2NotReadDir");
					}
					reader.ReadToFollowing("FB2LongPathDir");
					if (reader.HasAttributes ) {
						txtBoxFB2LongPathDir.Text = reader.GetAttribute("txtBoxFB2LongPathDir");
					}
					reader.ReadToFollowing("FB2NotValidDir");
					if (reader.HasAttributes ) {
						txtBoxFB2NotValidDir.Text = reader.GetAttribute("txtBoxFB2NotValidDir");
					}
					reader.ReadToFollowing("ArchNotOpenDir");
					if (reader.HasAttributes ) {
						txtBoxArchNotOpenDir.Text = reader.GetAttribute("txtBoxArchNotOpenDir");
					}
					reader.ReadToFollowing("TagsNoText");
					if (reader.HasAttributes ) {
						txtBoxFMNoGenreGroup.Text = reader.GetAttribute("txtBoxFMNoGenreGroup");
						txtBoxFMNoGenre.Text = reader.GetAttribute("txtBoxFMNoGenre");
						txtBoxFMNoLang.Text = reader.GetAttribute("txtBoxFMNoLang");
						txtBoxFMNoFirstName.Text = reader.GetAttribute("txtBoxFMNoFirstName");
						txtBoxFMNoMiddleName.Text = reader.GetAttribute("txtBoxFMNoMiddleName");
						txtBoxFMNoLastName.Text = reader.GetAttribute("txtBoxFMNoLastName");
						txtBoxFMNoNickName.Text = reader.GetAttribute("txtBoxFMNoNickName");
						txtBoxFMNoBookTitle.Text = reader.GetAttribute("txtBoxFMNoBookTitle");
						txtBoxFMNoSequence.Text = reader.GetAttribute("txtBoxFMNoSequence");
						txtBoxFMNoNSequence.Text = reader.GetAttribute("txtBoxFMNoNSequence");
					}
					reader.ReadToFollowing("GenresGroups");
					if (reader.HasAttributes ) {
						txtboxFMsf.Text			= reader.GetAttribute("txtboxFMsf");
						txtboxFMdetective.Text	= reader.GetAttribute("txtboxFMdetective");
						txtboxFMprose.Text		= reader.GetAttribute("txtboxFMprose");
						txtboxFMlove.Text		= reader.GetAttribute("txtboxFMlove");
						txtboxFMadventure.Text	= reader.GetAttribute("txtboxFMadventure");
						txtboxFMchildren.Text	= reader.GetAttribute("txtboxFMchildren");
						txtboxFMpoetry.Text		= reader.GetAttribute("txtboxFMpoetry");
						txtboxFMantique.Text	= reader.GetAttribute("txtboxFMantique");
						txtboxFMscience.Text	= reader.GetAttribute("txtboxFMscience");
						txtboxFMcomputers.Text	= reader.GetAttribute("txtboxFMcomputers");
						txtboxFMreference.Text	= reader.GetAttribute("txtboxFMreference");
						txtboxFMnonfiction.Text	= reader.GetAttribute("txtboxFMnonfiction");
						txtboxFMreligion.Text	= reader.GetAttribute("txtboxFMreligion");
						txtboxFMhumor.Text		= reader.GetAttribute("txtboxFMhumor");
						txtboxFMhome.Text		= reader.GetAttribute("txtboxFMhome");
						txtboxFMbusiness.Text	= reader.GetAttribute("txtboxFMbusiness");
					}
				} catch {
					MessageBox.Show( "Поврежден файл настроек: \""+Settings.Settings.GetSettingsPath()+"\".\nУдалите его, он создастся автоматически при сохранении настроек", "SharpFBTools", MessageBoxButtons.OK, MessageBoxIcon.Warning );
				} finally {
					reader.Close();
				}
			}
			#endregion
		}
		
		#endregion
		
		#region Обработчики
				
		void BtnOKClick(object sender, EventArgs e)
		{
			// сохранение настроек в ini
			#region Код
			// устанавливаем текущую папку - папка программы
			Environment.CurrentDirectory = Settings.Settings.GetProgDir();
			XmlWriter writer = null;
			try {
				XmlWriterSettings settings = new XmlWriterSettings();
				settings.Indent = true;
				settings.IndentChars = ("\t");
				settings.OmitXmlDeclaration = true;
				
				writer = XmlWriter.Create( Settings.Settings.GetSettingsPath(), settings );
				writer.WriteStartElement( "SharpFBTools" );
					// Общие
					writer.WriteStartElement( "General" );
						writer.WriteStartElement( "WinRar" );
							writer.WriteAttributeString( "WinRarPath", tboxWinRarPath.Text );
							writer.WriteAttributeString( "RarPath", tboxRarPath.Text );
							writer.WriteAttributeString( "UnRarPath", tboxUnRarPath.Text );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "A7za" );
							writer.WriteAttributeString( "A7zaPath", tbox7zaPath.Text );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "Editors" );
							writer.WriteAttributeString( "FBEPath", tboxFBEPath.Text );
							writer.WriteAttributeString( "TextFB2EPath", tboxTextEPath.Text );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "Reader" );
							writer.WriteAttributeString( "FBReaderPath", tboxReaderPath.Text );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "Diff" );
							writer.WriteAttributeString( "DiffPath", tboxDiffPath.Text );
						writer.WriteFullEndElement();
					writer.WriteEndElement();
					
					// Валидатор
					writer.WriteStartElement( "FB2Validator" );
						writer.WriteStartElement( "ValidatorDoubleClick" );
							writer.WriteAttributeString( "cboxValidatorForFB2SelectedIndex", cboxValidatorForFB2.SelectedIndex.ToString() );
							writer.WriteAttributeString( "cboxValidatorForFB2ArchiveSelectedIndex", cboxValidatorForFB2Archive.SelectedIndex.ToString() );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "ValidatorPressEnter" );
							writer.WriteAttributeString( "cboxValidatorForFB2SelectedIndexPE", cboxValidatorForFB2PE.SelectedIndex.ToString() );
							writer.WriteAttributeString( "cboxValidatorForFB2ArchiveSelectedIndexPE", cboxValidatorForFB2ArchivePE.SelectedIndex.ToString() );
						writer.WriteFullEndElement();
					writer.WriteEndElement();
					
					// Менеджер Файлов
					writer.WriteStartElement( "FileManager" );
						writer.WriteStartElement( "Register" );
							writer.WriteAttributeString( "rbtnAsIsChecked", rbtnAsIs.Checked.ToString() );
							writer.WriteAttributeString( "rbtnAsSentenceChecked", rbtnAsSentence.Checked.ToString() );
							writer.WriteAttributeString( "rbtnLowerChecked", rbtnLower.Checked.ToString() );
							writer.WriteAttributeString( "rbtnUpperChecked", rbtnUpper.Checked.ToString() );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "Translit" );
							writer.WriteAttributeString( "chBoxTranslitChecked", chBoxTranslit.Checked.ToString() );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "Strict" );
							writer.WriteAttributeString( "chBoxStrictChecked", chBoxStrict.Checked.ToString() );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "Space" );
							writer.WriteAttributeString( "cboxSpaceSelectedIndex", cboxSpace.SelectedIndex.ToString() );
							writer.WriteAttributeString( "cboxSpaceText", cboxSpace.Text.ToString() );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "Archive" );
							writer.WriteAttributeString( "chBoxToArchiveChecked", chBoxToArchive.Checked.ToString() );
							writer.WriteAttributeString( "cboxArchiveTypeSelectedIndex", cboxArchiveType.SelectedIndex.ToString() );
							writer.WriteAttributeString( "cboxArchiveTypeText", cboxArchiveType.Text.ToString() );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "IsFileExist" );
							writer.WriteAttributeString( "cboxFileExistSelectedIndex", cboxFileExist.SelectedIndex.ToString() );
							writer.WriteAttributeString( "cboxFileExistText", cboxFileExist.Text.ToString() );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "AddToFileNameBookID" );
							writer.WriteAttributeString( "chBoxAddToFileNameBookIDChecked", chBoxAddToFileNameBookID.Checked.ToString() );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "FileDelete" );
							writer.WriteAttributeString( "chBoxDelFB2FilesChecked", chBoxDelFB2Files.Checked.ToString() );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "AuthorsToDirs" );
							writer.WriteAttributeString( "rbtnAuthorOneChecked", rbtnAuthorOne.Checked.ToString() );
							writer.WriteAttributeString( "rbtnAuthorAllChecked", rbtnAuthorAll.Checked.ToString() );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "GenresToDirs" );
							writer.WriteAttributeString( "rbtnGenreOneChecked", rbtnGenreOne.Checked.ToString() );
							writer.WriteAttributeString( "rbtnGenreAllChecked", rbtnGenreAll.Checked.ToString() );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "GenresType" );
							writer.WriteAttributeString( "rbtnGenreSchemaChecked", rbtnGenreSchema.Checked.ToString() );
							writer.WriteAttributeString( "rbtnGenreTextChecked", rbtnGenreText.Checked.ToString() );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "FMGenresScheme" );
							writer.WriteAttributeString( "rbtnFMFB21Checked", rbtnFMFB21.Checked.ToString() );
							writer.WriteAttributeString( "rbtnFMFB22Checked", rbtnFMFB22.Checked.ToString() );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "SortType" );
							writer.WriteAttributeString( "rbtnFMAllFB2Checked", rbtnFMAllFB2.Checked.ToString() );
							writer.WriteAttributeString( "rbtnFMOnleValidFB2Checked", rbtnFMOnleValidFB2.Checked.ToString() );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "FB2NotReadDir" );
							writer.WriteAttributeString( "txtBoxFB2NotReadDir", txtBoxFB2NotReadDir.Text );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "FB2LongPathDir" );
							writer.WriteAttributeString( "txtBoxFB2LongPathDir", txtBoxFB2LongPathDir.Text );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "FB2NotValidDir" );
							writer.WriteAttributeString( "txtBoxFB2NotValidDir", txtBoxFB2NotValidDir.Text );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "ArchNotOpenDir" );
							writer.WriteAttributeString( "txtBoxArchNotOpenDir", txtBoxArchNotOpenDir.Text );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "TagsNoText" );
							writer.WriteAttributeString( "txtBoxFMNoGenreGroup", txtBoxFMNoGenreGroup.Text );
							writer.WriteAttributeString( "txtBoxFMNoGenre", txtBoxFMNoGenre.Text );
							writer.WriteAttributeString( "txtBoxFMNoLang", txtBoxFMNoLang.Text );
							writer.WriteAttributeString( "txtBoxFMNoFirstName", txtBoxFMNoFirstName.Text );
							writer.WriteAttributeString( "txtBoxFMNoMiddleName", txtBoxFMNoMiddleName.Text );
							writer.WriteAttributeString( "txtBoxFMNoLastName", txtBoxFMNoLastName.Text );
							writer.WriteAttributeString( "txtBoxFMNoNickName", txtBoxFMNoNickName.Text );
							writer.WriteAttributeString( "txtBoxFMNoBookTitle", txtBoxFMNoBookTitle.Text );
							writer.WriteAttributeString( "txtBoxFMNoSequence", txtBoxFMNoSequence.Text );
							writer.WriteAttributeString( "txtBoxFMNoNSequence", txtBoxFMNoNSequence.Text );
						writer.WriteFullEndElement();
						
						writer.WriteStartElement( "GenresGroups" );
							writer.WriteAttributeString( "txtboxFMsf", txtboxFMsf.Text );
							writer.WriteAttributeString( "txtboxFMdetective", txtboxFMdetective.Text );
							writer.WriteAttributeString( "txtboxFMprose", txtboxFMprose.Text );
							writer.WriteAttributeString( "txtboxFMlove", txtboxFMlove.Text );
							writer.WriteAttributeString( "txtboxFMadventure", txtboxFMadventure.Text );
							writer.WriteAttributeString( "txtboxFMchildren", txtboxFMchildren.Text );
							writer.WriteAttributeString( "txtboxFMpoetry", txtboxFMpoetry.Text );
							writer.WriteAttributeString( "txtboxFMantique", txtboxFMantique.Text );
							writer.WriteAttributeString( "txtboxFMscience", txtboxFMscience.Text );
							writer.WriteAttributeString( "txtboxFMcomputers", txtboxFMcomputers.Text );
							writer.WriteAttributeString( "txtboxFMreference", txtboxFMreference.Text );
							writer.WriteAttributeString( "txtboxFMnonfiction", txtboxFMnonfiction.Text );
							writer.WriteAttributeString( "txtboxFMreligion", txtboxFMreligion.Text );
							writer.WriteAttributeString( "txtboxFMhumor", txtboxFMhumor.Text );
							writer.WriteAttributeString( "txtboxFMhome", txtboxFMhome.Text );
							writer.WriteAttributeString( "txtboxFMbusiness", txtboxFMbusiness.Text );
						writer.WriteFullEndElement();
						
					writer.WriteEndElement();
					
				writer.WriteEndElement();
				writer.Flush();
			}  finally  {
				if (writer != null)
				writer.Close();
				this.Close();
			}
			#endregion
		}
		
		#region Общее
		void BtnWinRarPathClick(object sender, EventArgs e)
		{
			// указание пути к WinRar
			ofDlg.Title = "Укажите путь к WinRar:";
			ofDlg.FileName = "";
			ofDlg.Filter = "WinRAR.exe|*.exe|Все файлы (*.*)|*.*";
			DialogResult result = ofDlg.ShowDialog();
			if (result == DialogResult.OK) {
                tboxWinRarPath.Text = ofDlg.FileName;
            }
		}

		void BtnRarPathClick(object sender, EventArgs e)
		{
			// указание пути к Rar (консольному)
			ofDlg.Title = "Укажите путь к Rar (консольному):";
			ofDlg.FileName = "";
			ofDlg.Filter = "Rar.exe|*.exe|Все файлы (*.*)|*.*";
			DialogResult result = ofDlg.ShowDialog();
			if (result == DialogResult.OK) {
                tboxRarPath.Text = ofDlg.FileName;
            }
		}
		
		void BtnUnRarPathClick(object sender, EventArgs e)
		{
			// указание пути к UnRar (консольному)
			ofDlg.Title = "Укажите путь к UnRar (консольному):";
			ofDlg.FileName = "";
			ofDlg.Filter = "UnRar.exe|*.exe|Все файлы (*.*)|*.*";
			DialogResult result = ofDlg.ShowDialog();
			if (result == DialogResult.OK) {
                tboxUnRarPath.Text = ofDlg.FileName;
            }
		}
		
		void Btn7zaPathClick(object sender, EventArgs e)
		{
			// указание пути к 7za (консольному)
			ofDlg.Title = "Укажите путь к 7z(a) (консольному):";
			ofDlg.FileName = "";
			ofDlg.Filter = "Программы (*.exe)|*.exe|Все файлы (*.*)|*.*";
			DialogResult result = ofDlg.ShowDialog();
			if (result == DialogResult.OK) {
                tbox7zaPath.Text = ofDlg.FileName;
            }
		}
		
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
		#endregion

		#region Менеджер Файлов
		void CboxFileExistSelectedIndexChanged(object sender, EventArgs e)
		{
			chBoxAddToFileNameBookID.Visible = cboxFileExist.SelectedIndex != 0;
			if( cboxFileExist.SelectedIndex == 0 ) {
				chBoxAddToFileNameBookID.Checked = false;
			}
		}
		
		void ChBoxToArchiveCheckedChanged(object sender, EventArgs e)
		{
			cboxArchiveType.Enabled = chBoxToArchive.Checked;
		}
		
		void BtnFB2NotReadDirClick(object sender, EventArgs e)
		{
			// указание пути к папке для нечитаемых fb2-файлов
			filesWorker.OpenDirDlg( txtBoxFB2NotReadDir, fbdDir, "Укажите папку для нечитаемых fb2-файлов:" );
		}
		
		void BtnFB2LongPathDirClick(object sender, EventArgs e)
		{
			// указание пути к папке для fb2-файлов с сгенерированными длинными именами
			filesWorker.OpenDirDlg( txtBoxFB2LongPathDir, fbdDir, "Укажите папку для fb2-файлов, у которых после генерации имен путь получается слишком длинный:" );
		}
		
		void BtnFB2NotValidDirClick(object sender, EventArgs e)
		{
			// указание пути к папке для невалидных fb2-файлов
			filesWorker.OpenDirDlg( txtBoxFB2NotValidDir, fbdDir, "Укажите папку для невалидных fb2-файлов:" );
		}
		
		void BtnArchNotOpenDirClick(object sender, EventArgs e)
		{
			// указание пути к папке для "битых" архивов
			filesWorker.OpenDirDlg( txtBoxArchNotOpenDir, fbdDir, "Укажите папку для \"битых\" архивов:" );
		}
		#endregion
		
		#region Восстановление по-умолчанию
		void BtnDefRestoreClick(object sender, EventArgs e) {
			switch( tcOptions.SelectedIndex ) {
				case 0: // общие
					DefGeneral();
					break;
				case 1: // Валидатор
					DefValidator();
					break;
				case 2: // Менеджер Файлов
					switch( tcFM.SelectedIndex ) {
						case 0: // основные - для Менеджера Файлов
							DefFMGeneral();
							break;
						case 1: //Папки для "проблемных" файлов 
							DefFMProblemFilesDir();
							break;
						case 2: // название папки шаблонного тэга без данных
							DefFMDirNameForTagNotData();
							break;
						case 3: // название Групп Жанров
							DefFMGenresGroups();
							break;
					}
					break;
			}
		}
		#endregion
		
		#endregion

		
		
	}
}
