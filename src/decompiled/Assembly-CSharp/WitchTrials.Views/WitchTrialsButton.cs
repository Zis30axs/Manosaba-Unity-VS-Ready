using System.Runtime.InteropServices;
using GigaCreation.Essentials.Localization;
using GigaCreation.Essentials.Ui;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views;

[Token(Token = "0x200000B")]
public class WitchTrialsButton : RebuildableButton
{
	[Token(Token = "0x400000D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
	[Space]
	[SerializeField]
	private Canvas _canvas;

	[Token(Token = "0x400000E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x78")]
	[SerializeField]
	private GraphicRaycaster _graphicRaycaster;

	[Token(Token = "0x400000F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x80")]
	[Space]
	[SerializeField]
	private Vector2 _defaultSize;

	[Token(Token = "0x17000001")]
	protected override Vector2 DefaultSize
	{
		[Token(Token = "0x6000017")]
		[Address(RVA = "0x30CA20", Offset = "0x30B420", VA = "0x18030CA20", Slot = "4")]
		get
		{
			return default(Vector2);
		}
	}

	[Token(Token = "0x6000018")]
	[Address(RVA = "0x30CA40", Offset = "0x30B440", VA = "0x18030CA40", Slot = "5")]
	protected override void RebuildElements(ButtonData buttonData, bool atRuntime, bool controlsItsOwnSize, [Optional] LocaleKind? locale)
	{
	}

	[Token(Token = "0x6000019")]
	[Address(RVA = "0x30CBE0", Offset = "0x30B5E0", VA = "0x18030CBE0", Slot = "6")]
	protected override void GetReferences()
	{
	}

	[Token(Token = "0x600001A")]
	[Address(RVA = "0x30CD20", Offset = "0x30B720", VA = "0x18030CD20")]
	public void Show()
	{
	}

	[Token(Token = "0x600001B")]
	[Address(RVA = "0x30CD60", Offset = "0x30B760", VA = "0x18030CD60")]
	public void Hide()
	{
	}

	[Token(Token = "0x600001C")]
	[Address(RVA = "0x30CDA0", Offset = "0x30B7A0", VA = "0x18030CDA0")]
	public WitchTrialsButton()
	{
	}
}
