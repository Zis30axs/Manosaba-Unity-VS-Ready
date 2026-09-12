using System;
using Il2CppDummyDll;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GigaCreation.Essentials.Ui;

[Token(Token = "0x200000A")]
public class OptionItemButton : MonoBehaviour
{
	[Token(Token = "0x400001D")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private Button _button;

	[Token(Token = "0x600001E")]
	[Address(RVA = "0xCD80B0", Offset = "0xCD6AB0", VA = "0x180CD80B0")]
	private void Reset()
	{
	}

	[Token(Token = "0x600001F")]
	[Address(RVA = "0x33A4B0", Offset = "0x338EB0", VA = "0x18033A4B0")]
	public IObservable<Unit> OnClickAsObservable()
	{
		return null;
	}

	[Token(Token = "0x6000020")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public OptionItemButton()
	{
	}
}
