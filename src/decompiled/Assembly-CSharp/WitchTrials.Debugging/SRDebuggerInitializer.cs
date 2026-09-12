using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Debugging;

[Token(Token = "0x20001C3")]
public class SRDebuggerInitializer : MonoBehaviour
{
	[Token(Token = "0x400085E")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private bool _enableInEditor;

	[Token(Token = "0x60007AD")]
	[Address(RVA = "0x3B34A0", Offset = "0x3B1EA0", VA = "0x1803B34A0")]
	private void Awake()
	{
	}

	[Token(Token = "0x60007AE")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public SRDebuggerInitializer()
	{
	}
}
