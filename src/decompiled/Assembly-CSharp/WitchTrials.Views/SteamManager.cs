using System.Text;
using AOT;
using Il2CppDummyDll;
using Steamworks;
using UnityEngine;

namespace WitchTrials.Views;

[Token(Token = "0x200013F")]
[DisallowMultipleComponent]
public class SteamManager : MonoBehaviour
{
	[Token(Token = "0x4000628")]
	[FieldOffset(Offset = "0x0")]
	private static bool s_everInitialized;

	[Token(Token = "0x4000629")]
	[FieldOffset(Offset = "0x8")]
	private static SteamManager s_instance;

	[Token(Token = "0x400062A")]
	[FieldOffset(Offset = "0x20")]
	private bool _bInitialized;

	[Token(Token = "0x400062B")]
	[FieldOffset(Offset = "0x28")]
	private SteamAPIWarningMessageHook_t _steamAPIWarningMessageHook;

	[Token(Token = "0x170000AB")]
	private static SteamManager Instance
	{
		[Token(Token = "0x6000555")]
		[Address(RVA = "0x388D80", Offset = "0x387780", VA = "0x180388D80")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170000AC")]
	public static bool Initialized
	{
		[Token(Token = "0x6000556")]
		[Address(RVA = "0x388F30", Offset = "0x387930", VA = "0x180388F30")]
		get
		{
			return default(bool);
		}
	}

	[Token(Token = "0x6000557")]
	[Address(RVA = "0x3890E0", Offset = "0x387AE0", VA = "0x1803890E0")]
	[MonoPInvokeCallback(typeof(SteamAPIWarningMessageHook_t))]
	private static void SteamAPIDebugTextHook(int nSeverity, StringBuilder pchDebugText)
	{
	}

	[Token(Token = "0x6000558")]
	[Address(RVA = "0x389130", Offset = "0x387B30", VA = "0x180389130")]
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
	private static void InitOnPlayMode()
	{
	}

	[Token(Token = "0x6000559")]
	[Address(RVA = "0x3891E0", Offset = "0x387BE0", VA = "0x1803891E0")]
	private void Awake()
	{
	}

	[Token(Token = "0x600055A")]
	[Address(RVA = "0x389790", Offset = "0x388190", VA = "0x180389790")]
	private void OnEnable()
	{
	}

	[Token(Token = "0x600055B")]
	[Address(RVA = "0x389AE0", Offset = "0x3884E0", VA = "0x180389AE0")]
	private void OnDestroy()
	{
	}

	[Token(Token = "0x600055C")]
	[Address(RVA = "0x389F80", Offset = "0x388980", VA = "0x180389F80")]
	private void Update()
	{
	}

	[Token(Token = "0x600055D")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public SteamManager()
	{
	}
}
