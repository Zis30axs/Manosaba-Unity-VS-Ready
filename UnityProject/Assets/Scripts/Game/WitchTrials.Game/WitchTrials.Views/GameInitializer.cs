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

namespace WitchTrials.Views
{
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
    			// RECONSTRUCTED | Confidence: A | RVA: 0x30FFE0 (5300 bytes)
    			//
    			// This state machine is *compiler-generated* from the async method below; its recovered
    			// semantics now live in `InitializeAsync`, which the C# compiler turns back into an
    			// equivalent state machine.  Keeping a hand-written body here as well would duplicate the
    			// logic (and the compiler would emit a second nested state machine with this name), so the
    			// recovered sequence is documented on `InitializeAsync` instead.  Native evidence for this
    			// exact function (annotated disassembly) is in
    			// tools/unity-rebuild/out/logs/disas_gateinitializer.md.
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

    	// RECONSTRUCTED | Confidence: A | RVA: 0x30FA80
    	//
    	// Native evidence:
    	//   0x30FA91  rdi = rcx (__this)
    	//   0x30FA96..0x30FAC9  one-time type-init guard for three metadata slots:
    	//             AddressablesManager_TypeInfo,
    	//             MethodInfo ServiceLocator.Register<IAddressablesManager>(),
    	//             ServiceLocator_TypeInfo
    	//   0x30FAD0  rcx = AddressablesManager_TypeInfo
    	//   0x30FAD7  call il2cpp_codegen_object_new
    	//   0x30FAE4  call -> GigaCreation.Essentials.AddressablesUtils.AddressablesManager$$.ctor
    	//             (no arguments beyond __this: `new AddressablesManager()`)
    	//   0x30FAF0  [rdi + 0x40] = rbx  ->  this._addressablesManager = <new instance>
    	//             (offset 0x40 confirmed against dump.cs's GameInitializer field
    	//             table) followed by the GC write barrier at 0x30FAF6..0x30FB2F
    	//             (card table at data_3134580), i.e. a *reference* field store, not
    	//             a struct copy.
    	//   0x30FB38  rbx = [rdi + 0x40]  ->  reload this._addressablesManager
    	//   0x30FB4A  rdx = MethodInfo ServiceLocator.Register<IAddressablesManager>()
    	//   0x30FB5E  jmp -> GigaCreation.Tools.Service.ServiceLocator$$Register<object>
    	//             tail call with rcx = rbx (the reloaded field), rdx = MethodInfo.
    	//             ServiceLocator.Register<TService>(TService service) is the only
    	//             matching public overload (see ServiceLocator.cs:62), so the
    	//             instance is passed as the argument.
    	//
    	// Recovered behaviour: creates the project's Addressables front-end and
    	// registers it in the service locator. This is the first half of the boot
    	// chain -- GameInitializer::Start (RVA 0x30FB70) only happens after this.
    	[Token(Token = "0x600005F")]
    	[Address(RVA = "0x30FA80", Offset = "0x30E480", VA = "0x18030FA80")]
    	private void Awake()
    	{
    		_addressablesManager = new AddressablesManager();
    		GigaCreation.Tools.Service.ServiceLocator.Register<IAddressablesManager>(_addressablesManager);
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

    	// RECONSTRUCTED | Confidence: A | RVA: 0x30FDB0
    	//
    	// Native evidence:
    	//   0x30FDC4..0x30FDE6  one-time type-init guard for
    	//             MethodInfo ServiceLocator.Unregister<IAddressablesManager>()
    	//             and ServiceLocator_TypeInfo
    	//   0x30FDF2  rbx = [rbx + 0x40]  ->  this._addressablesManager
    	//   0x30FDFF  call interface/virtual dispatch resolve (IContentCatalogManager
    	//             slot lookup on the field's static type, emitted for the generic
    	//             constraint check; no managed side effect of its own)
    	//   0x30FE04  rdx = MethodInfo ServiceLocator.Unregister<IAddressablesManager>()
    	//   0x30FE0B  rcx = rbx (the field)
    	//   0x30FE13  jmp -> GigaCreation.Tools.Service.ServiceLocator$$Unregister<object>
    	//             tail call with rcx = this._addressablesManager,
    	//             rdx = MethodInfo. Unregister<TService>(TService service)
    	//             (ServiceLocator.cs:67) is the matching overload, so the field is
    	//             passed as the argument -- this is NOT a parameterless unregister.
    	//
    	// Recovered behaviour: symmetric teardown of Awake(); removes the registered
    	// Addressables front-end from the service locator when the initializer dies.
    	[Token(Token = "0x6000061")]
    	[Address(RVA = "0x30FDB0", Offset = "0x30E7B0", VA = "0x18030FDB0")]
    	private void OnDestroy()
    	{
    		GigaCreation.Tools.Service.ServiceLocator.Unregister<IAddressablesManager>(_addressablesManager);
    	}

    	[Token(Token = "0x6000062")]
    	[Address(RVA = "0x30FE20", Offset = "0x30E820", VA = "0x18030FE20")]
    	[AsyncStateMachine(typeof(_003CInitializeAsync_003Ed__8))]
    	private UniTask InitializeAsync([Optional] CancellationToken ct)
    	{
    		// The recovered body lives in InitializeAsyncCore: writing it *here* as an async method would
    		// make the compiler emit a nested state machine whose name collides with the decompiled
    		// `_003CInitializeAsync_003Ed__8` retained above as evidence-bearing metadata.
    		return InitializeAsyncCore(ct);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x30FFE0 (state machine body, 5300 bytes) / 0x30FE20 (kickoff)
    	//
    	// Native evidence (annotated disassembly: tools/unity-rebuild/out/logs/disas_gateinitializer.md,
    	// produced by work/reversing/disas.py; fields of the state machine are listed there too):
    	//
    	//   il2cpp_codegen_object_new + TYPE DefaultLocalePresenter_TypeInfo
    	//                                  -> new DefaultLocalePresenter(), stored in <defaultLocalePresenter>5__2
    	//   il2cpp_codegen_object_new + TYPE DefaultLocaleSelector_TypeInfo
    	//   METHODINFO ServiceLocator.Register<IDefaultLocaleSelector>() -> registered in the service locator
    	//   Start<DefaultLocalePresenter.<LoadAsync>d__3>()   -> <loadDefaultLocaleTask>5__3
    	//   Start<SplashScreen.<PlaySequenceAsync>d__12>()    -> <splashScreenTask>5__4
    	//   Start<IAddressablesManager.<InitializeAsync>d__4>()  (its UniTask is never stored: the state
    	//       machine has exactly one UniTask awaiter slot and one ValueTuple<UniTask,UniTask> await,
    	//       so this call is fire-and-forget in the original)
    	//   ValueTuple<UniTask, UniTask>..ctor + AwaitUnsafeOnCompleted<UniTask.Awaiter,...>
    	//                                  -> await UniTask.WhenAll(loadDefaultLocaleTask, splashScreenTask)
    	//   cmp qword ptr [r8+0x28],0 / IDisposable_TypeInfo / interface call
    	//                                  -> null-guarded defaultLocalePresenter.Dispose()
    	//   AddressablesAsyncExtensions.ToUniTask<GameObject>() + UniTask<GameObject>.GetAwaiter/GetResult
    	//                                  -> await <AssetReference>.InstantiateAsync(...).ToUniTask(ct)
    	//   UnityEngine.Object$$SetName      -> the instance is renamed to _gamePrefabInstanceName
    	//   METHODINFO GameObject.GetComponent<EntrySequence>() + <entrySequence>5__5 (+0x50)
    	//   Start<EntrySequence.<PlayInitialSequenceAsync>d__25>()
    	//                                  -> await entrySequence.PlayInitialSequenceAsync(ct)
    	//
    	// Two details the annotated dump does not resolve, marked rather than guessed:
    	//   (resolved) the InstantiateAsync call site is a virtual call, unnamed by the annotator, but the
    	//     argument registers are readable in the raw dump:
    	//       0x18031096F  mov rdx,[r13+0x20]   -> rdx = __this ('this' of the call = _gamePrefab)
    	//       0x18031097F  mov r10,[rax+0x238]  -> virtual slot 0x238 (InstantiateAsync)
    	//       0x180310992  xor r9d,r9d          -> arg2 = false  (instantiateInWorldSpace)
    	//       0x180310995  xor r8d,r8d          -> arg1 = 0/null (parent)
    	//       0x180310998  lea rcx,[rbp+0x80]   -> hidden pointer for the returned AsyncOperationHandle
    	//     所以原调用就是"沿用默认参数"的 InstantiateAsync()（parent = null, instantiateInWorldSpace = false）。
    	private async UniTask InitializeAsyncCore(CancellationToken ct)
    	{
    		DefaultLocalePresenter defaultLocalePresenter = new DefaultLocalePresenter();
    		GigaCreation.Tools.Service.ServiceLocator.Register<GigaCreation.Essentials.Localization.IDefaultLocaleSelector>(
    			new WitchTrials.Models.DefaultLocaleSelector());

    		UniTask loadDefaultLocaleTask = defaultLocalePresenter.LoadAsync(ct);
    		UniTask splashScreenTask = _splashScreen.PlaySequenceAsync(ct);
    		IAddressablesManager.InitializeAsync(ct).Forget();

    		await UniTask.WhenAll(loadDefaultLocaleTask, splashScreenTask);
    		defaultLocalePresenter.Dispose();

    		GameObject gameObject = await _gamePrefab.InstantiateAsync().ToUniTask(cancellationToken: ct);
    		gameObject.name = _gamePrefabInstanceName;

    		EntrySequence entrySequence = gameObject.GetComponent<EntrySequence>();
    		await entrySequence.PlayInitialSequenceAsync(ct);
    	}

    	[Token(Token = "0x6000063")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public GameInitializer()
    	{
    	}
    }
}
