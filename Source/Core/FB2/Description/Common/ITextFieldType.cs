/*
 * Created by SharpDevelop.
 * User: �������� ����� (DikBSD)
 * Date: 28.04.2009
 * Time: 22:06
 * 
 * License: GPL 2.1
 */
using System;

namespace Core.FB2.Common
{
	/// <summary>
	/// Description of ITextFieldType.
	/// </summary>
	public interface ITextFieldType
    {
        string Value { get; set; }	// �������� ����
        string Lang { set; get; } 	// ������� ����
    }
}
