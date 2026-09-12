using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.UI;

[Token(Token = "0x200052F")]
public class ControlPanel : MonoBehaviour
{
	[Token(Token = "0x400140B")]
	[FieldOffset(Offset = "0x20")]
	[Tooltip("Activated when input mode is not gamepad.")]
	[SerializeField]
	private GameObject buttons;

	[Token(Token = "0x400140C")]
	[FieldOffset(Offset = "0x28")]
	[Tooltip("Activated when input mode is gamepad.")]
	[SerializeField]
	private GameObject legend;

	[Token(Token = "0x60019BF")]
	[Address(RVA = "0x61E8F0", Offset = "0x61D2F0", VA = "0x18061E8F0", Slot = "4")]
	protected virtual void OnEnable()
	{
	}

	[Token(Token = "0x60019C0")]
	[Address(RVA = "0x61EA00", Offset = "0x61D400", VA = "0x18061EA00")]
	protected void OnDisable()
	{
	}

	[Token(Token = "0x60019C1")]
	[Address(RVA = "0x61EAF0", Offset = "0x61D4F0", VA = "0x18061EAF0", Slot = "5")]
	protected virtual void HandleInputModeChanged(InputMode mode)
	{
	}

	[Token(Token = "0x60019C2")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public ControlPanel()
	{
	}
}
