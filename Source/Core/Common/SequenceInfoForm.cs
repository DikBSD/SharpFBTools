/*
 * Сделано в SharpDevelop.
 * Пользователь: VadimK
 * Дата: 01.07.2015
 * Время: 7:09
 * 
 * Для изменения этого шаблона используйте Сервис | Настройка | Кодирование | Правка стандартных заголовков.
 */
using System;
using System.Drawing;
using System.Windows.Forms;

namespace Core.Common
{
	/// <summary>
	/// SequenceInfoForm: форма для создания/правки данных Серий книги / бумажной книги.
	/// </summary>
	public partial class SequenceInfoForm : Form
	{
		#region Закрытые данные класса
		private readonly SequenceInfo m_si = new SequenceInfo( Enums.SequenceEnum.Ebook, true );
		#endregion
		
		public SequenceInfoForm( ref SequenceInfo si )
		{

			InitializeComponent();
			m_si = si;
			string sSequence = string.Empty;
			if( si.SequenceType == Enums.SequenceEnum.Ebook )
				sSequence = "Электронной книги";
			else
				sSequence = "Бумажной книги";
			this.Text = si.IsCreate ? "Создание новой "+sSequence : "Редактирование выбранной "+sSequence;
			
			if( !si.IsCreate ) {
				SequenceTextBox.Text = si.Name;
				SequenceNumberTextBox.Text = si.Number;
			}
		}
		
		#region Открытые свойства
		public virtual SequenceInfo SequenceInfo {
			get {
				return m_si;
			}
		}
		#endregion
		
		#region Обработчики событий
		void ApplyBtnClick(object sender, EventArgs e)
		{
			if( SequenceTextBox.Text.Trim().Length == 0 && SequenceNumberTextBox.Text.Trim().Length == 0 ) {
				MessageBox.Show( "Ни одно поле не заполнено!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				SequenceTextBox.Focus();
				return;
			}
			
			m_si.Name = SequenceTextBox.Text.Trim();
			m_si.Number = SequenceNumberTextBox.Text.Trim();
			Close();
		}
		void CancelBtnClick(object sender, EventArgs e)
		{
			Close();
		}
		#endregion
	}
}
