/*
 * Сделано в SharpDevelop.
 * Пользователь: Кузнецов Вадим (DikBSD)
 * Дата: 02.07.2015
 * Время: 8:04
 * 
  */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Core.Common
{
	/// <summary>
	/// CustomInfoForm: форма для создания новой / правки выбранной Дополнительной информации 
	/// </summary>
	public partial class CustomInfoForm : Form
	{
		#region Закрытые данные класса
		private readonly CustomInfoInfo m_ci = new CustomInfoInfo( true );
		#endregion
		
		public CustomInfoForm( ref CustomInfoInfo ci )
		{
			InitializeComponent();
			m_ci = ci;
			const string sCI = "Дополнительной информации описания книги";
			this.Text = ci.IsCreate ? "Создание новой "+sCI : "Редактирование выбранной "+sCI;
			
			if ( !ci.IsCreate ) {
				TypeTextBox.Text = ci.Type;
				ValueTextBox.Text = ci.Value;
			}
		}
		
		#region Открытые свойства
		public virtual CustomInfoInfo CustomInfoInfo {
			get {
				return m_ci;
			}
		}
		#endregion
		
		#region Обработчики событий
		void ApplyBtnClick(object sender, EventArgs e)
		{
			if ( TypeTextBox.Text.Trim().Length == 0 && ValueTextBox.Text.Trim().Length == 0 ) {
				MessageBox.Show( "Ни одно поле не заполнено!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				TypeTextBox.Focus();
				return;
			}
			if ( TypeTextBox.Text.Trim().Length == 0 ) {
				MessageBox.Show( "Поле типа данных должно быть обязательно заполнено!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				TypeTextBox.Focus();
				return;
			}
			
			m_ci.Type = TypeTextBox.Text.Trim();
			m_ci.Value = ValueTextBox.Text.Trim();
			
			Close();
		}
		void CancelBtnClick(object sender, EventArgs e)
		{
			Close();
		}
		#endregion
		
	}
}
