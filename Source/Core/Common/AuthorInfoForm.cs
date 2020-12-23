/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 30.06.2015
 * Время: 12:29
 * 
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Core.Common
{
	/// <summary>
	/// AuthorInfoForm: форма для создания нового / правки выбранного Автора, Переводчика...
	/// </summary>
	public partial class AuthorInfoForm : Form
	{
		#region Закрытые данные класса
		private readonly AuthorInfo m_ai = new AuthorInfo( Enums.AuthorEnum.AuthorOfBook, true );
		#endregion
		
		public AuthorInfoForm( ref AuthorInfo ai )
		{
			InitializeComponent();
			m_ai = ai;
			string sAuthor = string.Empty;
			if( ai.AuthorType == Enums.AuthorEnum.AuthorOfBook )
				sAuthor = "Автора книги";
			else if ( ai.AuthorType == Enums.AuthorEnum.AuthorOfFB2 )
				sAuthor = "Создателя fb2-файла";
			else
				sAuthor = "Переводчика";
			this.Text = ai.IsCreate ? "Создание нового "+sAuthor : "Редактирование выбранного "+sAuthor;
			
			if( !ai.IsCreate ) {
				LastNameTextBox.Text = ai.LastName;
				FirstNameTextBox.Text = ai.FirstName;
				MiddleNameTextBox.Text = ai.MiddleName;
				NickNameTextBox.Text = ai.NickName;
				IDTextBox.Text = ai.ID;
				HomePageTextBox.Text = ai.HomePage;
				EmailTextBox.Text = ai.Email;
			}
		}
		
		#region Открытые свойства
		public virtual AuthorInfo AuthorInfo {
			get {
				return m_ai;
			}
		}
		#endregion
		
		#region Обработчики событий
		void NewIDButtonClick(object sender, EventArgs e)
		{
			const string sMess = "Создать новый id?";
			const MessageBoxButtons buttons = MessageBoxButtons.YesNo;
			DialogResult result = MessageBox.Show( sMess, "Создание нового id", buttons, MessageBoxIcon.Question );
			if ( result == DialogResult.Yes )
				IDTextBox.Text = Guid.NewGuid().ToString().ToUpper();
		}
		void CancelBtnClick(object sender, EventArgs e)
		{
			Close();
		}
		void ApplyBtnClick(object sender, EventArgs e)
		{
			if (LastNameTextBox.Text.Trim().Length == 0 && FirstNameTextBox.Text.Trim().Length == 0 &&
			   MiddleNameTextBox.Text.Trim().Length == 0 && NickNameTextBox.Text.Trim().Length == 0 &&
			   IDTextBox.Text.Trim().Length == 0 && HomePageTextBox.Text.Trim().Length == 0 && EmailTextBox.Text.Trim().Length == 0) {
				MessageBox.Show(
					"Ни одно поле не заполнено!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning
				);
				LastNameTextBox.Focus();
				return;
			} else if (LastNameTextBox.Text.Trim().Length == 0) {
				if (m_ai.AuthorType == Enums.AuthorEnum.AuthorOfBook) {
					// Только для Авторов Книги Фамилия нужно вводить обязательно!
					MessageBox.Show(
						"Поле 'Фамилия' должно быть заполнено обязательно!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning
					);
					LastNameTextBox.Focus();
					return;
				}
			}
			
			m_ai.LastName = LastNameTextBox.Text.Trim();
			m_ai.FirstName = FirstNameTextBox.Text.Trim();
			m_ai.MiddleName = MiddleNameTextBox.Text.Trim();
			m_ai.NickName = NickNameTextBox.Text.Trim();
			m_ai.ID = IDTextBox.Text.Trim();
			m_ai.HomePage = HomePageTextBox.Text.Trim();
			m_ai.Email = EmailTextBox.Text.Trim();
			
			Close();
		}
		#endregion
	}
}
