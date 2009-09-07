/*
 * Created by SharpDevelop.
 * User: Кузнецов Вадим (DikBSD)
 * Date: 07.09.2009
 * Time: 13:25
 * 
 * License: GPL 2.1
 */
using System;
using System.Windows.Forms;


namespace Core.Misc
{
	/// <summary>
	/// Description of Misc.
	/// </summary>
	public class Misc
	{
		public Misc()
		{
		}
		
		// увеличение значения 2-й колонки ListView на 1
		public void IncListViewStatus( ListView lv, int nItem ) {
			lv.Items[nItem].SubItems[1].Text =
				Convert.ToString( 1+Convert.ToInt32( lv.Items[nItem].SubItems[1].Text ) );
		}
	}
}
