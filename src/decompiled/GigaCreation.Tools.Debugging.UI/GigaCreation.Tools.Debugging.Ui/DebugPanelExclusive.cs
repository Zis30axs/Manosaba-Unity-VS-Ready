using Il2CppDummyDll;
using UnityEngine;

namespace GigaCreation.Tools.Debugging.Ui;

[Token(Token = "0x2000007")]
public class DebugPanelExclusive : DebugPanel
{
	[Token(Token = "0x400000F")]
	[FieldOffset(Offset = "0x48")]
	private Transform[] _otherDebugPanels;

	[Token(Token = "0x4000010")]
	[FieldOffset(Offset = "0x50")]
	private bool _isQuitting;

	[Token(Token = "0x17000001")]
	private Transform[] OtherDebugPanels
	{
		[Token(Token = "0x6000011")]
		[Address(RVA = "0xD47FF0", Offset = "0xD469F0", VA = "0x180D47FF0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000012")]
	[Address(RVA = "0xD48320", Offset = "0xD46D20", VA = "0x180D48320")]
	private void OnDestroy()
	{
	}

	[Token(Token = "0x6000013")]
	[Address(RVA = "0xD48480", Offset = "0xD46E80", VA = "0x180D48480")]
	private void OnApplicationQuit()
	{
	}

	[Token(Token = "0x6000014")]
	[Address(RVA = "0xD48490", Offset = "0xD46E90", VA = "0x180D48490", Slot = "4")]
	public override void ShowContent()
	{
	}

	[Token(Token = "0x6000015")]
	[Address(RVA = "0xD485F0", Offset = "0xD46FF0", VA = "0x180D485F0", Slot = "5")]
	public override void HideContent()
	{
	}

	[Token(Token = "0x6000016")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public DebugPanelExclusive()
	{
	}
}
