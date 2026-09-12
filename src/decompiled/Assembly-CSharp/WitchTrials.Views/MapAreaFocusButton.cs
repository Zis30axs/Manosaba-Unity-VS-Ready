using GigaCreation.Essentials.Ui;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Views;

[Token(Token = "0x200000A")]
public class MapAreaFocusButton : ButtonExtendedBase
{
	[Token(Token = "0x400000C")]
	[FieldOffset(Offset = "0x118")]
	[Header("References")]
	[SerializeField]
	private CanvasGroup _canvasGroup;

	[Token(Token = "0x6000010")]
	[Address(RVA = "0x30C910", Offset = "0x30B310", VA = "0x18030C910", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x6000011")]
	[Address(RVA = "0x30C950", Offset = "0x30B350", VA = "0x18030C950")]
	public void Enable()
	{
	}

	[Token(Token = "0x6000012")]
	[Address(RVA = "0x30C990", Offset = "0x30B390", VA = "0x18030C990")]
	public void Disable()
	{
	}

	[Token(Token = "0x6000013")]
	[Address(RVA = "0x30C9D0", Offset = "0x30B3D0", VA = "0x18030C9D0", Slot = "44")]
	protected override void OnHighlightingChanged(bool isHighlighted)
	{
	}

	[Token(Token = "0x6000014")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "45")]
	protected override void OnEnablingChanged(bool isEnabled)
	{
	}

	[Token(Token = "0x6000015")]
	[Address(RVA = "0x30C9D0", Offset = "0x30B3D0", VA = "0x18030C9D0")]
	public void ModifyInEditor(bool show)
	{
	}

	[Token(Token = "0x6000016")]
	[Address(RVA = "0x30CA10", Offset = "0x30B410", VA = "0x18030CA10")]
	public MapAreaFocusButton()
	{
	}
}
