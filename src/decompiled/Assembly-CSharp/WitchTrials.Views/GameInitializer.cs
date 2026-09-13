using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.AddressablesUtils;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.AddressableAssets;
using WitchTrials.Debugging;
using WitchTrials.Presenters;

namespace WitchTrials.Views;

[Token(Token = "0x200001B")]
public class GameInitializer : MonoBehaviour
{
	[StructLayout(3)]
	[Token(Token = "0x200001C")]
	[CompilerGenerated]
	private struct _003CInitializeAsync_003Ed__8
{
		[Token(Token = "0x4000039")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400003A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400003B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public CancellationToken ct;

		[Token(Token = "0x400003C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public GameInitializer _003C_003E4__this;

		[Token(Token = "0x400003D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private DefaultLocalePresenter _003CdefaultLocalePresenter_003E5__2;

		[Token(Token = "0x400003E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask _003CloadDefaultLocaleTask_003E5__3;

		[Token(Token = "0x400003F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private UniTask _003CsplashScreenTask_003E5__4;

		[Token(Token = "0x4000040")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private EntrySequence _003CentrySequence_003E5__5;

		[Token(Token = "0x4000041")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4000042")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
		private UniTask<GameObject>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x6000064")]
		[Address(RVA = "0x30FFE0", Offset = "0x30E9E0", VA = "0x18030FFE0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000065")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000034")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	[Header("Assets")]
	[SerializeField]
	private AssetReferenceGameObject _gamePrefab;

	[Token(Token = "0x4000035")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	[Header("References")]
	[SerializeField]
	private SteamManager _steamManager;

	[Token(Token = "0x4000036")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[SerializeField]
	private SplashScreen _splashScreen;

	[Token(Token = "0x4000037")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[Header("Parameters")]
	[SerializeField]
	private string _gamePrefabInstanceName;

	[Token(Token = "0x4000038")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private IAddressablesManager _addressablesManager;

	[Token(Token = "0x600005F")]
	[Address(RVA = "0x30FA80", Offset = "0x30E480", VA = "0x18030FA80")]
	private void Awake()
	{
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x30FB70
	//
	// Native evidence:
	//   0x30FB80  rcx = _steamManager; null -> NullReferenceException
	//   0x30FB8F  rbx = _steamManager.gameObject   (Component.get_gameObject)
	//   0x30FBBA  al  = DebugOptionsBase.get_IsDebugMode()
	//   0x30FBC1  jne -> if IsDebugMode: dl = *(bool*)DebugOptions's static
	//             storage at offset 0, i.e. DebugOptions.EnableSteamworks's
	//             backing field `s_enableSteamworks` (confirmed against
	//             dump.cs's DebugOptions field layout: it is the first static
	//             field, offset 0x0). Else (not debug mode): dl = 1.
	//             This is exactly how `!IsDebugMode || EnableSteamworks`
	//             compiles: short-circuit to true without reading
	//             EnableSteamworks when not in debug mode, otherwise use it.
	//   0x30FBE7  gameObject.SetActive(dl)
	//   0x30FBF1  rbx = this.destroyCancellationToken (MonoBehaviour built-in)
	//   0x30FC02..0x30FD62  AsyncUniTaskMethodBuilder.Start<InitializeAsync>d__8>
	//             on a freshly zeroed state machine with <>4__this = this and
	//             ct = destroyCancellationToken -- i.e. call InitializeAsync(ct).
	//   0x30FD85  UniTaskExtensions.Forget() on the returned UniTask -- fire and
	//             forget, Start() itself does not await it.
	[Token(Token = "0x6000060")]
	[Address(RVA = "0x30FB70", Offset = "0x30E570", VA = "0x18030FB70")]
	private void Start()
	{
		_steamManager.gameObject.SetActive(!DebugOptions.IsDebugMode || DebugOptions.EnableSteamworks);
		InitializeAsync(destroyCancellationToken).Forget();
	}

	[Token(Token = "0x6000061")]
	[Address(RVA = "0x30FDB0", Offset = "0x30E7B0", VA = "0x18030FDB0")]
	private void OnDestroy()
	{
	}

	[Token(Token = "0x6000062")]
	[Address(RVA = "0x30FE20", Offset = "0x30E820", VA = "0x18030FE20")]
	[AsyncStateMachine(typeof(_003CInitializeAsync_003Ed__8))]
	private UniTask InitializeAsync([Optional] CancellationToken ct)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000063")]
	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
	public GameInitializer()
	{
	}
}
