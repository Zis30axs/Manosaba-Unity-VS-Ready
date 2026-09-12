using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UI;

namespace WitchTrials.Views;

[Token(Token = "0x2000009")]
public sealed class CanvasGroupButton : Button
{
	[Token(Token = "0x4000008")]
	[FieldOffset(Offset = "0x108")]
	[Header("References")]
	[SerializeField]
	private CanvasGroup _normal;

	[Token(Token = "0x4000009")]
	[FieldOffset(Offset = "0x110")]
	[SerializeField]
	private CanvasGroup _highlighted;

	[Token(Token = "0x400000A")]
	[FieldOffset(Offset = "0x118")]
	[SerializeField]
	private CanvasGroup _disabled;

	[Token(Token = "0x400000B")]
	[FieldOffset(Offset = "0x120")]
	[Header("Parameters")]
	[SerializeField]
	private bool _ignoreParentDisabling;

	[Token(Token = "0x600000D")]
	[Address(RVA = "0x30C610", Offset = "0x30B010", VA = "0x18030C610", Slot = "26")]
	protected override void DoStateTransition(SelectionState state, bool instant)
	{
	}

	[Token(Token = "0x600000E")]
	[Address(RVA = "0x30C640", Offset = "0x30B040", VA = "0x18030C640")]
	private void OnStateChanged(SelectionState state)
	{
	}

	[Token(Token = "0x600000F")]
	[Address(RVA = "0x30C900", Offset = "0x30B300", VA = "0x18030C900")]
	public CanvasGroupButton()
	{
	}
}
