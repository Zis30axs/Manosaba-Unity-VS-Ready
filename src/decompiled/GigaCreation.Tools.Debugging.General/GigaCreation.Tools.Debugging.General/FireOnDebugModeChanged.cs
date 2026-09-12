using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.Events;

namespace GigaCreation.Tools.Debugging.General;

[Token(Token = "0x2000007")]
public class FireOnDebugModeChanged : MonoBehaviour
{
	[Token(Token = "0x400000D")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private UnityEvent _onEnterDebugMode;

	[Token(Token = "0x400000E")]
	[FieldOffset(Offset = "0x28")]
	[SerializeField]
	private UnityEvent _onExitDebugMode;

	[Token(Token = "0x600000E")]
	[Address(RVA = "0xD432F0", Offset = "0xD41CF0", VA = "0x180D432F0")]
	private void Start()
	{
	}

	[Token(Token = "0x600000F")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public FireOnDebugModeChanged()
	{
	}
}
