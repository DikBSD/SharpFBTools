/*
 * Сделано в SharpDevelop.
 * Пользователь: VadimK
 * Дата: 01.07.2015
 * Время: 7:09
 * 
 * License: GPL 2.1
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
			sSequence = si.SequenceType == Enums.SequenceEnum.Ebook
				? "Электронной книги" : "Бумажной книги";
			this.Text = si.IsCreate ? "Создание новой "+sSequence : "Редактирование выбранной "+sSequence;
			
			if ( !si.IsCreate ) {
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
			if ( string.IsNullOrWhiteSpace( SequenceTextBox.Text ) && string.IsNullOrWhiteSpace( SequenceNumberTextBox.Text ) ) {
				MessageBox.Show( "Ни одно поле не заполнено!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning );
				SequenceTextBox.Focus();
				return;
			}
			if ( !string.IsNullOrWhiteSpace( SequenceNumberTextBox.Text ) ) {
				int number = 0;
				if ( !int.TryParse( SequenceNumberTextBox.Text, out number ) ) {
					MessageBox.Show( "Номер Серии не может символы и/или пробелы! Введите число, или оставьте поле пустым, если у данной книги нет номера серии.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error );
					SequenceNumberTextBox.Focus();
					return;
				}
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
