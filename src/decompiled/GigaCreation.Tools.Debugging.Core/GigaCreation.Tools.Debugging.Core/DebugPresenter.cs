using Il2CppDummyDll;
using UniRx;
using UnityEngine;

namespace GigaCreation.Tools.Debugging.Core;

[Token(Token = "0x2000005")]
public class DebugPresenter : MonoBehaviour
{
	[Token(Token = "0x4000007")]
	[FieldOffset(Offset = "0x20")]
	[SerializeField]
	private bool _forceReleaseBuild;

	[Token(Token = "0x4000008")]
	[FieldOffset(Offset = "0x28")]
	[SerializeField]
	private BoolReactiveProperty _isDebugMode;

	[Token(Token = "0x4000009")]
	[FieldOffset(Offset = "0x30")]
	private IDebugManager _debugManager;

	[Token(Token = "0x6000006")]
	[Address(RVA = "0xD42140", Offset = "0xD40B40", VA = "0x180D42140")]
	private void Awake()
	{
	}

	[Token(Token = "0x6000007")]
	[Address(RVA = "0xD42480", Offset = "0xD40E80", VA = "0x180D42480")]
	private void OnApplicationQuit()
	{
	}

	[Token(Token = "0x6000008")]
	[Address(RVA = "0xD424F0", Offset = "0xD40EF0", VA = "0x180D424F0")]
	private void LinkDebugModeFlags(IDebugManager debugManager)
	{
	}

	[Token(Token = "0x6000009")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public DebugPresenter()
	{
	}
}
