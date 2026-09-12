using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.UI;

namespace GigaCreation.Tools.Debugging.Ui;

[Token(Token = "0x2000005")]
public class DebugPanel : MonoBehaviour
{
	[Token(Token = "0x4000008")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private Transform[] _contents;

	[Token(Token = "0x4000009")]
	[FieldOffset(Offset = "0x28")]
	[Space]
	[SerializeField]
	private CanvasGroup _showButtonCanvasGroup;

	[Token(Token = "0x400000A")]
	[FieldOffset(Offset = "0x30")]
	[SerializeField]
	private Button _showButton;

	[Token(Token = "0x400000B")]
	[FieldOffset(Offset = "0x38")]
	[SerializeField]
	private CanvasGroup _hideButtonCanvasGroup;

	[Token(Token = "0x400000C")]
	[FieldOffset(Offset = "0x40")]
	[SerializeField]
	private Button _hideButton;

	[Token(Token = "0x6000007")]
	[Address(RVA = "0xD47440", Offset = "0xD45E40", VA = "0x180D47440")]
	private void Start()
	{
	}

	[Token(Token = "0x6000008")]
	[Address(RVA = "0xD47830", Offset = "0xD46230", VA = "0x180D47830", Slot = "4")]
	public virtual void ShowContent()
	{
	}

	[Token(Token = "0x6000009")]
	[Address(RVA = "0xD47BB0", Offset = "0xD465B0", VA = "0x180D47BB0", Slot = "5")]
	public virtual void HideContent()
	{
	}

	[Token(Token = "0x600000A")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public DebugPanel()
	{
	}
}
