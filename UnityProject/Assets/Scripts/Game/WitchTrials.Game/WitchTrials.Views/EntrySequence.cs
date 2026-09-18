using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Essentials.Overlays.Models;
using GigaCreation.NaninovelExtender.Localization;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using Naninovel;
using UniRx;
using UnityEngine;
using WitchTrials.Debugging;
using WitchTrials.Models;

namespace WitchTrials.Views
{
    [Token(Token = "0x2000142")]
    public class EntrySequence : MonoBehaviour
    {
    	[Token(Token = "0x2000143")]
    	[CompilerGenerated]
    	private sealed class _003C_003Ec__DisplayClass25_0
    	{
    		[Token(Token = "0x400063A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public EntrySequence _003C_003E4__this;

    		[Token(Token = "0x400063B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public CancellationToken ct;

    		[Token(Token = "0x600056A")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public _003C_003Ec__DisplayClass25_0()
    		{
    		}

    		// RECONSTRUCTED | Confidence: A | RVA: 0x38AA30 (82 bytes)
    		//
    		// The body of the lambda subscribed to `OnEnterTitleScreenAsObservable()` in
    		// PlayInitialSequenceAsyncCore: `ResetDebugVariables()` on `<>4__this` (+0x10) and then
    		// `EnterTitleScreenAsync(ct)` with r8 = this display class's `ct` (+0x18), whose Cysharp UniTask is
    		// dropped through `UniTaskExtensions.Forget` (0x18181F300). The source writes that as the inline
    		// lambda, so the compiler regenerates this method (in a new <>c__DisplayClass, leaving this
    		// retained copy empty). Native evidence: tools/unity-rebuild/out/logs/disas_entrysequence_b__0_0x38AA30.txt
    		[Token(Token = "0x600056B")]
    		[Address(RVA = "0x38AA30", Offset = "0x389430", VA = "0x18038AA30")]
    		internal void _003CPlayInitialSequenceAsync_003Eb__0(Unit _)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000144")]
    	[CompilerGenerated]
    	private struct _003CEnterTitleScreenAsync_003Ed__27
    {
    		[Token(Token = "0x400063C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400063D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400063E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public EntrySequence _003C_003E4__this;

    		[Token(Token = "0x400063F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000640")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private Naninovel.UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600056C")]
    		[Address(RVA = "0x38AA90", Offset = "0x389490", VA = "0x18038AA90", Slot = "4")]
    		private void MoveNext()
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0x38AA90 (1375 bytes)
    			//
    			// Compiler-generated from EnterTitleScreenAsyncCore below; the recovered sequence (and the
    			// evidence for it) is documented there, and the compiler regenerates an equivalent state
    			// machine. Native evidence for this exact function is in
    			// tools/unity-rebuild/out/logs/disas_entrysequence_entertitle_0x38AA90.txt.
    		}

    		[Token(Token = "0x600056D")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000145")]
    	[CompilerGenerated]
    	private struct _003CPlayInitialSequenceAsync_003Ed__25
    {
    		[Token(Token = "0x4000641")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000642")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000643")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public EntrySequence _003C_003E4__this;

    		[Token(Token = "0x4000644")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000645")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private _003C_003Ec__DisplayClass25_0 _003C_003E8__1;

    		[Token(Token = "0x4000646")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000647")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private Naninovel.UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x600056E")]
    		[Address(RVA = "0x38B000", Offset = "0x389A00", VA = "0x18038B000", Slot = "4")]
    		private void MoveNext()
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0x38B000 (3082 bytes)
    			//
    			// This state machine is *compiler-generated* from PlayInitialSequenceAsyncCore below; its
    			// recovered semantics live there, and the compiler regenerates an equivalent state machine
    			// named `_003CPlayInitialSequenceAsyncCore_003Ed__N` (the "Core" in the method name is what
    			// keeps it from colliding with this retained `_003CPlayInitialSequenceAsync_003Ed__25`).
    			// Keeping a hand-written body here would duplicate the logic. Native evidence
    			// for this exact function (annotated disassembly, incl. the state machine's field table) is in
    			// tools/unity-rebuild/out/logs/disas_entrysequence_playinitial_0x38B000.txt.
    		}

    		[Token(Token = "0x600056F")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x400062D")]
    	private const string AllowRollbackVarName = "allowRollback";

    	[Token(Token = "0x400062E")]
    	private const string AllowContinueDebateVarName = "allowContinueDebate";

    	[Token(Token = "0x400062F")]
    	private const string IsDebugDirectJumpVarName = "isDebugDirectJump";

    	[Token(Token = "0x4000630")]
    	private const string EnableDebugSkipVarName = "enableDebugSkip";

    	[Token(Token = "0x4000631")]
    	private const string IsDemoVarName = "isDemo";

    	[Token(Token = "0x4000632")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	[Header("Parameters")]
    	[SerializeField]
    	private float _fadeInDuration;

    	[Token(Token = "0x4000633")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x24")]
    	[SerializeField]
    	private Color _fadeColor;

    	[Token(Token = "0x4000634")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	private IOverlayFader _overlayFader;

    	[Token(Token = "0x4000635")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	private IWitchTrialsNaninovelManager _witchTrialsNaninovelManager;

    	[Token(Token = "0x4000636")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	private IScriptManager _naniScriptManager;

    	[Token(Token = "0x4000637")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	private IScriptPlayer _naniScriptPlayer;

    	[Token(Token = "0x4000638")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    	private ICustomVariableManager _naniVarManager;

    	[Token(Token = "0x4000639")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
    	private ILocalizationManagerExtended _naniLocalizationManager;

    	[Token(Token = "0x170000AD")]
    	private IOverlayFader OverlayFader
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x38A030, 186 B
    		//
    		// Same 186-byte lazily-cached service getter as SplashScreen's: read the field (+0x38), test it, and on
    		// null call `ServiceLocator.Get<IOverlayFader>()` with the includeDerivedType flag set (`mov cl,1`) and
    		// store the result back (`??=`).  Evidence: METHODINFO `ServiceLocator.Get<IOverlayFader>()` at
    		// 0x38A042/0x38A088, `mov rax,[rbx+0x38]` at 0x38A06B, `jne` short-circuit at 0x38A072.
    		[Token(Token = "0x6000560")]
    		[Address(RVA = "0x38A030", Offset = "0x388A30", VA = "0x18038A030")]
    		get
    		{
    			return _overlayFader ??= ServiceLocator.Get<IOverlayFader>(true);
    		}
    	}

    	[Token(Token = "0x170000AE")]
    	private IWitchTrialsNaninovelManager WitchTrialsNaninovelManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x38A0F0, 186 B
    		//
    		// `_witchTrialsNaninovelManager (+0x40) ??= ServiceLocator.Get<IWitchTrialsNaninovelManager>(true)`.
    		// Evidence: METHODINFO `ServiceLocator.Get<IWitchTrialsNaninovelManager>()` at 0x38A102/0x38A148,
    		// `mov rax,[rbx+0x40]` at 0x38A12B, `jne` short-circuit at 0x38A132.
    		//
    		// This getter is the first thing the recovered boot chain touches inside EntrySequence
    		// (`await WitchTrialsNaninovelManager.InitializeAsync(ct)`), so while it returned null the chain died
    		// there with an NRE - measured in G4b.
    		[Token(Token = "0x6000561")]
    		[Address(RVA = "0x38A0F0", Offset = "0x388AF0", VA = "0x18038A0F0")]
    		get
    		{
    			return _witchTrialsNaninovelManager ??= ServiceLocator.Get<IWitchTrialsNaninovelManager>(true);
    		}
    	}

    	[Token(Token = "0x170000AF")]
    	private IScriptManager NaniScriptManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x38A1B0, 186 B
    		//
    		// The same shape, but the source is Naninovel's engine rather than the game's ServiceLocator:
    		// `_naniScriptManager (+0x48) ??= Engine.GetServiceOrErr<IScriptManager>()`.  Evidence: METHODINFO
    		// `Naninovel.Engine.GetServiceOrErr<IScriptManager>()` at 0x38A1C2/0x38A208 (no `includeDerivedType`
    		// argument exists on this API, and none is passed), `mov rax,[rbx+0x48]` at 0x38A1EB.
    		[Token(Token = "0x6000562")]
    		[Address(RVA = "0x38A1B0", Offset = "0x388BB0", VA = "0x18038A1B0")]
    		get
    		{
    			return _naniScriptManager ??= Engine.GetServiceOrErr<IScriptManager>();
    		}
    	}

    	[Token(Token = "0x170000B0")]
    	private IScriptPlayer NaniScriptPlayer
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x38A270, 186 B
    		//
    		// `_naniScriptPlayer (+0x50) ??= Engine.GetServiceOrErr<IScriptPlayer>()`.  Evidence: METHODINFO
    		// `Naninovel.Engine.GetServiceOrErr<IScriptPlayer>()` at 0x38A282/0x38A2C8, `mov rax,[rbx+0x50]`
    		// at 0x38A2AB.
    		[Token(Token = "0x6000563")]
    		[Address(RVA = "0x38A270", Offset = "0x388C70", VA = "0x18038A270")]
    		get
    		{
    			return _naniScriptPlayer ??= Engine.GetServiceOrErr<IScriptPlayer>();
    		}
    	}

    	[Token(Token = "0x170000B1")]
    	private ICustomVariableManager NaniVarManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x38A330
    		// Lazily-cached engine service lookup, same pattern as
    		// StageScreenController.TrialStateManager: `_naniVarManager ??=
    		// Naninovel.Engine.GetServiceOrErr<ICustomVariableManager>();`
    		[Token(Token = "0x6000564")]
    		[Address(RVA = "0x38A330", Offset = "0x388D30", VA = "0x18038A330")]
    		get
    		{
    			return _naniVarManager ??= Engine.GetServiceOrErr<ICustomVariableManager>();
    		}
    	}

    	[Token(Token = "0x170000B2")]
    	private ILocalizationManagerExtended NaniLocalizationManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x38A3F0, 186 B
    		//
    		// `_naniLocalizationManager (+0x60) ??= Engine.GetServiceOrErr<ILocalizationManagerExtended>()`.
    		// Evidence: METHODINFO `Naninovel.Engine.GetServiceOrErr<ILocalizationManagerExtended>()` at
    		// 0x38A402/0x38A448, `mov rax,[rbx+0x60]` at 0x38A42B, `jne` short-circuit at 0x38A432.
    		[Token(Token = "0x6000565")]
    		[Address(RVA = "0x38A3F0", Offset = "0x388DF0", VA = "0x18038A3F0")]
    		get
    		{
    			return _naniLocalizationManager ??= Engine.GetServiceOrErr<ILocalizationManagerExtended>();
    		}
    	}

    	[Token(Token = "0x6000566")]
    	[Address(RVA = "0x38A4B0", Offset = "0x388EB0", VA = "0x18038A4B0")]
    	[AsyncStateMachine(typeof(_003CPlayInitialSequenceAsync_003Ed__25))]
    	public Cysharp.Threading.Tasks.UniTask PlayInitialSequenceAsync([Optional] CancellationToken ct)
    	{
    		// The recovered body lives in PlayInitialSequenceAsyncCore: writing it *here* as an async method
    		// would make the compiler emit a nested state machine whose name collides with the decompiled
    		// `_003CPlayInitialSequenceAsync_003Ed__25` retained above as evidence-bearing metadata.
    		return PlayInitialSequenceAsyncCore(ct);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x38A4B0 (kickoff, 420 B) / 0x38B000 (state machine MoveNext, 3082 B)
    	//
    	// Native evidence (annotated disassembly produced by work/reversing/disas.py:
    	// tools/unity-rebuild/out/logs/disas_entrysequence_playinitial_0x38B000.txt, which also prints the
    	// state machine's field table; the subscribed lambda's body is in
    	// tools/unity-rebuild/out/logs/disas_entrysequence_b__0_0x38AA30.txt):
    	//
    	// Kickoff 0x38A4B0 -- the usual zeroed <PlayInitialSequenceAsync>d__25 with <>4__this (+0x18) and
    	//   ct (+0x20) handed to AsyncUniTaskMethodBuilder.Start, i.e. an ordinary `async UniTask` method.
    	//   Its awaiter fields are <>u__1 (+0x30, Cysharp.Threading.Tasks.UniTask.Awaiter) and <>u__2
    	//   (+0x40, Naninovel.UniTask.Awaiter) -- awaiting a Cysharp UniTask and a Naninovel UniTask.
    	//
    	// MoveNext, in state order. The dispatch at 0x38B159 reads `<>1__state` and branches
    	//   0 -> 0x38B577, 1 -> 0x38B553, 2 -> 0x38B3CD, 3 -> 0x38B3AB; only 0, 1 and 3 are ever stored
    	//   (0x38B378 `state = 0`, 0x38B874 `state = 1`, 0x38BB66 `state = 3`) -- see TODO(evidence) below.
    	//
    	//   1. `il2cpp_codegen_object_new <>c__DisplayClass25_0` -> `<>8__1` (+0x28), then
    	//      `<>4__this = this` (0x38B202, +0x10) and `ct = <parameter>` (0x38B262, +0x18). A display class
    	//      with exactly these two fields is only produced when a lambda captures both `this` and `ct`,
    	//      and every later use of `ct` in this body indeed goes through `<>8__1.ct` (0x38B2CB, 0x38B9C6)
    	//      -- what the compiler does with a captured parameter -- while `this` is still read from the
    	//      state machine's `<>4__this` (0x38B2B6, 0x38B9AE).
    	//   2. Await #1 (state 0): the generic-interface wrapper 0x180007000 is called with rcx = &retbuf
    	//      (16 B), edx = 1, r8 = INaninovelManager_TypeInfo, r9 = get_WitchTrialsNaninovelManager()
    	//      (0x38A0F0) and `<>8__1.ct` as the stack argument. The wrapper resolves slot 1 of
    	//      INaninovelManager on that object and tail-calls it as `(retbuf, this, ct, MethodInfo)`;
    	//      dump.cs records Slot 1 = `UniTask InitializeAsync(CancellationToken)` (token 0x6000004), and a
    	//      CancellationToken argument with a 16-byte Cysharp UniTask return fits no other member, so
    	//      `await WitchTrialsNaninovelManager.InitializeAsync(ct)`. Awaiter -> `<>u__1`, state 0.
    	//   3. Resume 0x38B577 -> 0x38B594: `DebugOptions.get_Locale` (0x3AA280) into a stack Nullable, then
    	//      the inlined shared-generic `Nullable<LocaleKind>.get_HasValue` sequence 0x38B5DD..0x38B62A;
    	//      `je 0x38B925` when it is null -> `if (DebugOptions.Locale.HasValue)`.
    	//   4. Await #2 (state 1), inside that `if`: the lazy `NaniLocalizationManager` property
    	//      (`_naniLocalizationManager` +0x60 ??= Engine.GetServiceOrErr<ILocalizationManagerExtended>(),
    	//      0x38B66B..0x38B6E9) and slot 1 of ILocalizationManagerExtended (fallback 0x38B758 `r8d = 1`,
    	//      found path 0x38BB4E `inc ecx`) = `UniTask SelectLocale(LocaleKind)` (dump.cs Slot 1), called
    	//      with edx = `DebugOptions.Locale.Value` (Nullable<LocaleKind>.get_Value, 0x38B70B). The return
    	//      is a Naninovel.UniTask -> `<>u__2`, state 1 (0x38B874).
    	//   5. Resume 0x38B553 -> 0x38B8B0 -> 0x38B925 (the join after the `if`): get_WitchTrialsNaninovelManager,
    	//      then the interface-call thunk 0x1800022E0 with ecx = 8 -> slot 8 of IWitchTrialsNaninovelManager
    	//      = `IObservable<Unit> OnEnterTitleScreenAsObservable()` (dump.cs Slot 8, the only member
    	//      returning IObservable<Unit>). Its result gets `il2cpp_codegen_object_new Action<Unit>` +
    	//      `.ctor(<>8__1, MethodInfo <PlayInitialSequenceAsync>b__0)` (0x38B97B),
    	//      `ObservableExtensions.Subscribe<Unit>` (0x38B98D) and
    	//      `DisposableExtensions.AddTo<IDisposable>(subscription, this)` (0x38B9A7) -- the AddTo receiver
    	//      is the EntrySequence Component itself.
    	//   6. `ResetDebugVariables()` (0x38B9B1 -> 0x38A660), then `EnterTitleScreenAsync(<>8__1.ct)`
    	//      (0x38B9D2, rdx = this, r8 = that captured ct) awaited as #3: its Cysharp UniTask goes back into
    	//      `<>u__1` (await #1's awaiter is dead by then) with state 3; the resume 0x38B3AB -> 0x38BA58
    	//      sets `state = -2` and calls SetResult, ending the method.
    	//
    	// The subscribed lambda's recovered body (`<>c__DisplayClass25_0.<PlayInitialSequenceAsync>b__0`,
    	// RVA 0x38AA30, 82 B) is `ResetDebugVariables()` on `<>8__1.<>4__this` followed by
    	// `EnterTitleScreenAsync(<>8__1.ct).Forget()` (UniTaskExtensions$$Forget, 0x18181F300). Writing it
    	// here as an inline lambda is the source form that produces exactly this display class; as with the
    	// kickoff the compiler then regenerates it under a fresh <>c__DisplayClass number, so the decompiled
    	// `<>c__DisplayClass25_0` above stays as metadata and its `b__0` stays empty.
    	//
    	// TODO(evidence): the state machine owns a *third* await whose continuation survived but whose start
    	//   code did not, so it cannot be placed in source. Case 2 (0x38B3CD) restores `<>u__2` into
    	//   [rsp+0xF8], clears it, sets `state = -1`, runs the Naninovel IAwaiter GetResult thunk (ecx = 2,
    	//   0x38B454) and then `OverlayFader.FadeIn(0f, _fadeColor)` -- get_OverlayFader 0x38B467,
    	//   `new Nullable<Color>(_fadeColor (+0x24))` at 0x38B49B, slot 3 of IOverlayFader (fallback 0x38B4F4
    	//   `r8d = 3`, found path 0x38B53D `add ecx, 3`; dump.cs Slot 3 = `void FadeIn(float,
    	//   Nullable<Color>)`, which the discarded return value also fits), duration xmm1 = 0.0f -- and then
    	//   falls through to the method exit. Nothing in the 3082 bytes ever stores 2 into `<>1__state`, so
    	//   that case is unreachable: the branch's guard and the awaited call are gone from the binary. All
    	//   the evidence supports is the shape `... await <something returning Naninovel.UniTask>;
    	//   OverlayFader.FadeIn(0f, _fadeColor); ` as a third await between awaits #2 and #3 (the only
    	//   position for which `<>u__2` is the right awaiter slot), most plausibly behind a guard the C++
    	//   compiler folded away as constant false. The one game flag whose name matches that semantics is
    	//   the neighbouring folded-to-`false` getter `DebugOptions.SkipEntrySequenceAtFirstTime`
    	//   (RVA 0x3AA360), but a C#-level constant would have been folded by Roslyn before IL2CPP saw it,
    	//   so that name is NOT established. Nothing is written below for it rather than guessing.
    	private async Cysharp.Threading.Tasks.UniTask PlayInitialSequenceAsyncCore(CancellationToken ct)
    	{
    		await WitchTrialsNaninovelManager.InitializeAsync(ct);

    		if (DebugOptions.Locale.HasValue)
    		{
    			await NaniLocalizationManager.SelectLocale(DebugOptions.Locale.Value);
    		}

    		WitchTrialsNaninovelManager.OnEnterTitleScreenAsObservable()
    			.Subscribe(_ =>
    			{
    				ResetDebugVariables();
    				EnterTitleScreenAsync(ct).Forget();
    			})
    			.AddTo(this);

    		ResetDebugVariables();

    		await EnterTitleScreenAsync(ct);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x38A660
    	//
    	// Native evidence: five calls to the extension method
    	// `ICustomVariableManager.TrySetVariableValue<bool>(name, value)` on
    	// NaniVarManager, each setting one Naninovel custom variable used to drive
    	// debug-only branches in the .nani scripts:
    	//
    	//   "allowRollback"        = DebugOptionsBase.IsDebugMode
    	//   "allowContinueDebate"  = DebugOptionsBase.IsDebugMode && DebugOptions.AllowContinueDebate
    	//   "isDebugDirectJump"    = DebugOptions.StartGameScenario != null
    	//   "enableDebugSkip"      = DebugOptionsBase.IsDebugMode && DebugOptions.DebugSkip
    	//   "isDemo"               = false  (constant, no computation at all)
    	//
    	// The middle three read DebugOptions' private static field storage directly
    	// ([TypeInfo+0xb8] then a fixed byte offset) rather than calling the public
    	// property getters -- almost certainly the property getters were inlined at
    	// this call site. Confirmed for `isDebugDirectJump` specifically: disassembling
    	// DebugOptions.get_StartGameScenario (RVA 0x3AA370) shows it implements
    	// exactly `return IsDebugMode && s_overrideStartGameScenario ? s_startGameScenario
    	// : null;`, i.e. byte-identical to the inlined sequence seen here. So
    	// `DebugOptions.StartGameScenario != null` is written using the real public
    	// API a hand-written caller would have used, not the private field directly.
    	[Token(Token = "0x6000567")]
    	[Address(RVA = "0x38A660", Offset = "0x389060", VA = "0x18038A660")]
    	private void ResetDebugVariables()
    	{
    		NaniVarManager.TrySetVariableValue("allowRollback", DebugOptions.IsDebugMode);
    		NaniVarManager.TrySetVariableValue("allowContinueDebate", DebugOptions.IsDebugMode && DebugOptions.AllowContinueDebate);
    		NaniVarManager.TrySetVariableValue("isDebugDirectJump", DebugOptions.StartGameScenario != null);
    		NaniVarManager.TrySetVariableValue("enableDebugSkip", DebugOptions.IsDebugMode && DebugOptions.DebugSkip);
    		NaniVarManager.TrySetVariableValue("isDemo", false);
    	}

    	[Token(Token = "0x6000568")]
    	[Address(RVA = "0x38A8A0", Offset = "0x3892A0", VA = "0x18038A8A0")]
    	[AsyncStateMachine(typeof(_003CEnterTitleScreenAsync_003Ed__27))]
    	private Cysharp.Threading.Tasks.UniTask EnterTitleScreenAsync([Optional] CancellationToken ct)
    	{
    		// Same reason as PlayInitialSequenceAsync above: the recovered body lives in
    		// EnterTitleScreenAsyncCore so the compiler cannot emit a second `_003CEnterTitleScreenAsync_003Ed__27`.
    		return EnterTitleScreenAsyncCore(ct);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x38A8A0 (kickoff, 400 B) / 0x38AA90 (state machine MoveNext, 1375 B)
    	//
    	// Native evidence (annotated disassembly produced by work/reversing/disas.py:
    	// tools/unity-rebuild/out/logs/disas_entrysequence_entertitle_0x38AA90.txt):
    	//
    	// Kickoff 0x38A8A0 -- a zeroed <EnterTitleScreenAsync>d__27 with <>4__this (+0x18) and ct (+0x20)
    	//   handed to AsyncUniTaskMethodBuilder.Start: an ordinary `async UniTask` method. Its only awaiter
    	//   field is `<>u__1` (+0x28, Naninovel.UniTask.Awaiter) -- exactly one await, of a *Naninovel*
    	//   UniTask, and no display class (so nothing here captures anything).
    	//
    	//   1. Dispatch 0x38AB26: `test eax, eax; jne 0x38AB47`, so only `<>1__state == 0` is a resume
    	//      (0x38AB2A restores `<>u__1`, clears it, sets `state = -1` and jumps to the GetResult at
    	//      0x38ADCC); every other value is the start path.
    	//   2. Start: `get_NaniScriptPlayer` (0x38AB55 -> 0x38A270) and the inlined lazy `NaniScriptManager`
    	//      property -- `_naniScriptManager` (+0x48) is tested and, when null,
    	//      `Engine.GetServiceOrErr<IScriptManager>()` (0x38ABC0, its MethodInfo slot plus
    	//      Engine_TypeInfo) is stored back into the field behind the GC write barrier. That service is
    	//      then passed in r8 to the shared generic wrapper 0x180008130, which scans the object's
    	//      implemented-interface table for `IEngineService<ScriptsConfiguration>_TypeInfo` and tail-calls
    	//      that interface's slot 0 = `TConfig Configuration { get; }`, the only member
    	//      `IEngineService<TConfig>` declares itself (dump.cs token 0x6000FCD). So rax is the
    	//      ScriptsConfiguration and `mov rdx, [rax + 0x40]` (0x38AC38) is `ScriptsConfiguration.TitleScript`
    	//      (dump.cs: `public string TitleScript; // 0x40`, "local resource path of the script to play when
    	//      showing the Title UI").
    	//   3. `Naninovel.ScriptPlayerExtensions.LoadAndPlay` (0x38AC3F -> 0x1805CE2C0) with
    	//      rcx = NaniScriptPlayer, rdx = that path and r8d = 0 -- i.e. the optional
    	//      `playlistIndex = 0` of `LoadAndPlay(this IScriptPlayer player, string scriptPath,
    	//      int playlistIndex = 0)` (ScriptPlayerExtensions.cs:200) is left at its default and no
    	//      CancellationToken is passed. Its Naninovel.UniTask is awaited as state 0 (0x38AD4C).
    	//   4. Resume 0x38ADCC..0x38AE3C (Naninovel IAwaiter GetResult thunk, ecx = 2), then the fade:
    	//      `get_OverlayFader` (0x38AE4F -> 0x38A030), `new Nullable<Color>(_fadeColor)` (0x38AE6B reads
    	//      the 16-byte Color at +0x24, Nullable<Color>..ctor at 0x38AE86), and slot 4 of IOverlayFader
    	//      (fallback 0x38AEDC `r8d = 4`, found path 0x38AF9E `add eax, 4`; dump.cs Slot 4 =
    	//      `UniTask FadeInAsync(float duration, Nullable<Color> color, CancellationToken ct)`) with
    	//      xmm2 = `_fadeInDuration` (+0x20), r9 = &Nullable<Color> and the state machine's `ct` (+0x20) as
    	//      the stack argument. The 16-byte Cysharp UniTask that comes back is *not* awaited -- 0x38AF2A
    	//      calls `UniTaskExtensions.Forget` on it -- and 0x38AF30 then sets `state = -2` and calls
    	//      SetResult, so the source is `OverlayFader.FadeInAsync(_fadeInDuration, _fadeColor, ct).Forget();`.
    	private async Cysharp.Threading.Tasks.UniTask EnterTitleScreenAsyncCore(CancellationToken ct)
    	{
    		await NaniScriptPlayer.LoadAndPlay(NaniScriptManager.Configuration.TitleScript);

    		OverlayFader.FadeInAsync(_fadeInDuration, _fadeColor, ct).Forget();
    	}

    	[Token(Token = "0x6000569")]
    	[Address(RVA = "0x30D8F0", Offset = "0x30C2F0", VA = "0x18030D8F0")]
    	public EntrySequence()
    	{
    	}
    }
}
