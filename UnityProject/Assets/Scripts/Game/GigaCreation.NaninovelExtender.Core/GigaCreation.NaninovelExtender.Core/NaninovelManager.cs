using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using Naninovel;

namespace GigaCreation.NaninovelExtender.Core
{
    [Token(Token = "0x2000006")]
    public class NaninovelManager : INaninovelManager, IService, INaninovelStateSaver
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000007")]
    	[CompilerGenerated]
    	private struct _003CInitializeAsync_003Ed__8
    {
    		[Token(Token = "0x4000008")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000009")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400000A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public CancellationToken ct;

    		[Token(Token = "0x400000B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600000E")]
    		[Address(RVA = "0xD02E10", Offset = "0xD01810", VA = "0x180D02E10", Slot = "4")]
    		private void MoveNext()
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0xD02E10, 970 B
    			//
    			// This state machine is *compiler-generated* from the async method below; its recovered
    			// semantics now live in `InitializeAsyncCore`, which the C# compiler turns back into an
    			// equivalent state machine. Keeping a hand-written body here as well would duplicate the
    			// logic (and the compiler would emit a second nested state machine with this name), so the
    			// recovered sequence is documented on `InitializeAsyncCore` instead. Native evidence for
    			// this exact function (annotated disassembly) is in
    			// tools/unity-rebuild/out/logs/disas_gigacore_naninovelmanager_initialize_movenext_0xD02E10.txt
    		}

    		[Token(Token = "0x600000F")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000008")]
    	[CompilerGenerated]
    	private struct _003CInitializeNaninovelAsync_003Ed__10
    {
    		[Token(Token = "0x400000C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400000D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400000E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public CancellationToken ct;

    		[Token(Token = "0x400000F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private Naninovel.UniTask _003Ctask_003E5__2;

    		[Token(Token = "0x4000010")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000010")]
    		[Address(RVA = "0xD031E0", Offset = "0xD01BE0", VA = "0x180D031E0", Slot = "4")]
    		private void MoveNext()
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0xD031E0, 947 B
    			//
    			// Compiler-generated from `InitializeNaninovelAsync`; the recovered sequence lives in
    			// `InitializeNaninovelAsyncCore` below (see the evidence block there). Native evidence for
    			// this exact function (annotated disassembly) is in
    			// tools/unity-rebuild/out/logs/disas_gigacore_naninovelmanager_initializenaninovel_movenext_0xD031E0.txt
    		}

    		[Token(Token = "0x6000011")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000009")]
    	[CompilerGenerated]
    	private struct _003CSaveSettingsAsync_003Ed__9
    {
    		[Token(Token = "0x4000011")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000012")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000013")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public NaninovelManager _003C_003E4__this;

    		[Token(Token = "0x4000014")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public CancellationToken ct;

    		[Token(Token = "0x4000015")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private Naninovel.UniTask _003Ctask_003E5__2;

    		[Token(Token = "0x4000016")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000012")]
    		[Address(RVA = "0xD035A0", Offset = "0xD01FA0", VA = "0x180D035A0", Slot = "4")]
    		private void MoveNext()
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0xD035A0, 1139 B
    			//
    			// Compiler-generated from `SaveSettingsAsync`; the recovered sequence lives in
    			// `SaveSettingsAsyncCore` below (see the evidence block there). Native evidence for this
    			// exact function (annotated disassembly) is in
    			// tools/unity-rebuild/out/logs/disas_gigacore_naninovelmanager_savesettings_movenext_0xD035A0.txt
    		}

    		[Token(Token = "0x6000013")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000006")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    	private IStateManager _naniStateManager;

    	[Token(Token = "0x4000007")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    	private IScriptManager _naniScriptManager;

    	[Token(Token = "0x17000002")]
    	public virtual string StartGameScenarioName
    	{
    		[Token(Token = "0x6000006")]
    		[Address(RVA = "0xD026C0", Offset = "0xD010C0", VA = "0x180D026C0", Slot = "7")]
    		get
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0xD026C0, 239 B
    			//
    			// Native evidence
    			// (tools/unity-rebuild/out/logs/disas_gigacore_naninovelmanager_get_startgamescenarioname_0xD026C0.txt):
    			//   0x180D026C6..0x180D02715  one-time type-init guard for
    			//             TYPE Naninovel.IEngineService<ScriptsConfiguration>_TypeInfo and
    			//             METHODINFO Method$Naninovel.Engine.GetServiceOrErr<IScriptManager>()
    			//   0x180D0271C  r8 = [rbx + 0x18]      -> this._naniScriptManager; on null,
    			//             METHODINFO Engine.GetServiceOrErr<IScriptManager>() -> call 0x180682840 and the
    			//             result is both used and stored back to [rbx + 0x18] (write barrier). That is
    			//             byte-for-byte the body of the private `NaniScriptManager` property below
    			//             (0xD02870, 186 B), i.e. that getter is inlined here.
    			//   0x180D02796  call 0x180008130    -> generic interface-dispatch thunk for
    			//             Naninovel.IEngineService<ScriptsConfiguration> slot 0, invoked on the
    			//             IScriptManager instance (r8). dump.cs: `IEngineService<TConfig>` declares
    			//             exactly one member, `TConfig Configuration { get; }` at Slot 0, and
    			//             `IScriptManager : IEngineService<ScriptsConfiguration>` -- so this is
    			//             `NaniScriptManager.Configuration` (the interface call is emitted against the
    			//             *declaring* interface, which is why the thunk's type is IEngineService<...>,
    			//             not IScriptManager).
    			//   0x180D0279B  `test rax,rax / je THROW NullReferenceException` = IL2CPP's receiver null
    			//             check for the field read below.
    			//   0x180D027A0  rax = qword ptr [rax + 0x48]  -> Naninovel.ScriptsConfiguration field at
    			//             +0x48. dump.cs: `public string StartGameScript; // 0x48`. Naninovel's
    			//             `Configuration` base declares no instance fields (its +0x10 slot belongs to
    			//             UnityEngine.ScriptableObject) and ScriptsConfiguration's first own field
    			//             (`Loader`) is at +0x18, so +0x48 is StartGameScript. Field, not a property:
    			//             the value is read straight out of memory, no call.
    			//
    			// Cross-check: `WitchTrials.Models.WitchTrialsNaninovelManager.get_StartGameScenarioName`
    			// (0x3993C0) inlines this base getter and was reconstructed in this repository from the same
    			// three steps (lazy +0x18 field / IEngineService<ScriptsConfiguration> slot 0 / +0x48
    			// StartGameScript) -- two independent readings agree.
    			//
    			// TODO(evidence): the source spelling uses the private property `NaniScriptManager` (the
    			//   convention used throughout this codebase); writing `_naniScriptManager ??= ...` inline
    			//   instead would compile to identical machine code, so the native body cannot separate the
    			//   two spellings.
    			return NaniScriptManager.Configuration.StartGameScript;
    		}
    	}

    	[Token(Token = "0x17000003")]
    	private IStateManager NaniStateManager
    	{
    		[Token(Token = "0x6000007")]
    		[Address(RVA = "0xD027B0", Offset = "0xD011B0", VA = "0x180D027B0")]
    		get
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0xD027B0, 186 B
    			//
    			// Native evidence (tools/unity-rebuild/out/logs/disas_gigacore_naninovelmanager_get_nanistatemanager_0xD027B0.txt):
    			//   0x180D027B6..0x180D027E4  one-time type-init guard for
    			//             METHODINFO Method$Naninovel.Engine.GetServiceOrErr<IStateManager>() and
    			//             TYPE Naninovel.Engine_TypeInfo
    			//   0x180D027EB  rax = [rbx + 0x10]   -> this._naniStateManager. dump.cs:
    			//             `private IStateManager _naniStateManager; // 0x10`
    			//   0x180D027EF  `test rax,rax / jne 0x180D02864`  = `??=` (non-null short-circuits and is
    			//             returned directly)
    			//   0x180D0280F  call 0x180682840 (Naninovel.Engine.GetServiceOrErr<object>, the shared body
    			//             of the generic `GetServiceOrErr<TService>()` -- no parameters, the MethodInfo is
    			//             the only argument register), result ANDed with the field store at
    			//             0x180D0281E `mov [rbx + 0x10], rax` + GC write barrier -> the cached field
    			//   0x180D02861/0x180D02864  mov rax,r9 / epilogue -> the value is returned
    			//   No null check on the result, no other work.
    			return _naniStateManager ??= Engine.GetServiceOrErr<IStateManager>();
    		}
    	}

    	[Token(Token = "0x17000004")]
    	private IScriptManager NaniScriptManager
    	{
    		[Token(Token = "0x6000008")]
    		[Address(RVA = "0xD02870", Offset = "0xD01270", VA = "0x180D02870")]
    		get
    		{
    			// RECONSTRUCTED | Confidence: A | RVA: 0xD02870, 186 B
    			//
    			// Native evidence (tools/unity-rebuild/out/logs/disas_gigacore_naninovelmanager_get_naniscriptmanager_0xD02870.txt):
    			//   Same lazily-cached pattern as `NaniStateManager` above, one field over:
    			//   0x180D02876..0x180D028A4  type-init guard for
    			//             METHODINFO Method$Naninovel.Engine.GetServiceOrErr<IScriptManager>() and
    			//             TYPE Naninovel.Engine_TypeInfo
    			//   0x180D028AB  rax = [rbx + 0x18]   -> this._naniScriptManager. dump.cs:
    			//             `private IScriptManager _naniScriptManager; // 0x18`
    			//   0x180D028B2  `test rax,rax / jne 0x180D02924`  = `??=`
    			//   0x180D028CF  call 0x180682840 (Engine.GetServiceOrErr<IScriptManager>) and
    			//             0x180D028DE `mov [rbx + 0x18], rax` + GC write barrier -> cached field
    			//   (0xD027B0 and 0xD02870 are the same 186-byte body modulo the field offset and the
    			//   MethodInfo slot -- IL2CPP did not fold them, they only look alike.)
    			return _naniScriptManager ??= Engine.GetServiceOrErr<IScriptManager>();
    		}
    	}

    	[Token(Token = "0x6000009")]
    	[Address(RVA = "0xD02930", Offset = "0xD01330", VA = "0x180D02930", Slot = "8")]
    	[AsyncStateMachine(typeof(_003CInitializeAsync_003Ed__8))]
    	public virtual Cysharp.Threading.Tasks.UniTask InitializeAsync([Optional] CancellationToken ct)
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD02930, 314 B
    		//
    		// Native evidence (tools/unity-rebuild/out/logs/disas_gigacore_naninovelmanager_initialize_kickoff_0xD02930.txt):
    		//   Standard async kickoff for a 16-byte-by-value return: rcx = return buffer (sret) -> rdi,
    		//   rdx = __this (stored nowhere: <InitializeAsync>d__8 has no <>4__this field, i.e. the body
    		//   never touches `this`), r8 = ct -> rbx. Stack state machine at [rsp+0x30]: state +0x30,
    		//   builder +0x38, ct +0x48 (= +0x18, stored from rbx with a write barrier), awaiter +0x50
    		//   (zeroed, 16 B = +0x20). Then `state = -1`,
    		//   AsyncUniTaskMethodBuilder.Start<NaninovelManager.<InitializeAsync>d__8>() (0x331870) and
    		//   `<builder>.get_Task()` (0x3313C0) copied into the return buffer.
    		//
    		// The recovered body lives in InitializeAsyncCore: writing it *here* as an async method would
    		// make the compiler emit a nested state machine whose name collides with the decompiled
    		// `_003CInitializeAsync_003Ed__8` retained above as evidence-bearing metadata.
    		return InitializeAsyncCore(ct);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD02E10 (state machine body, 970 B) / 0xD02930 (kickoff, 314 B)
    	//
    	// Native evidence (tools/unity-rebuild/out/logs/disas_gigacore_naninovelmanager_initialize_movenext_0xD02E10.txt).
    	// State machine layout as retained above: state +0x0, builder +0x8, ct +0x18,
    	// Cysharp UniTask.Awaiter <>u__1 +0x20 -- one awaiter slot and only two reachable states, so the
    	// body contains exactly ONE await:
    	//   1. 0x180D02E6A  eax = state; `test eax,eax / jne 0x180D02E8D`
    	//      - state == 0 (resume after the single await): <>u__1 is loaded from [rdi+0x20] into a stack
    	//        awaiter, [rdi+0x20] is re-zeroed, `state = -1`, then straight to the awaiter's GetResult
    	//        (0x31D0, with the IUniTaskSource type check at 0x180D0311C/0x180D03142) and the epilogue
    	//        (`state = -2` at 0x180D0315A + builder completion). Nothing follows the await.
    	//      - state != 0 (entry) -> 0x180D02E8D: the awaited expression.
    	//   2. 0x180D02E8D  rbx = [rdi + 0x18]   -> d__8.ct, forwarded as the inner call's ct (stored to
    	//      the inner state machine's +0x18 at 0x180D02F2F) -- so the ct passed in is what gets awaited.
    	//   3. 0x180D02EB2..0x180D02F79  a *second*, 0x38-byte state machine is built on d__8's own stack
    	//      ([rsp+0x50]): state 0x50, builder 0x58, ct 0x68, Naninovel.UniTask temp 0x70,
    	//      Cysharp UniTask.Awaiter 0x80 -- exactly the layout of the retained
    	//      `_003CInitializeNaninovelAsync_003Ed__10` struct.
    	//   4. 0x180D02F92  AsyncUniTaskMethodBuilder.Start<NaninovelManager.<InitializeNaninovelAsync>d__10>()
    	//      (0x331870) with rcx = &inner builder, rdx = &inner state machine, r8 = that Start MethodInfo
    	//      loaded from the metadata slot at 0x180D02F81, then
    	//   5. 0x180D02FA1  `<inner builder>.get_Task()` (0x3313C0) -> the awaited 16-byte UniTask.
    	//      Steps 3-5 are the kickoff of `InitializeNaninovelAsync` (0xD02C20, identical code):
    	//      IL2CPP inlined the small private static kickoff -- there is no `call 0xD02C20` anywhere in
    	//      this body, only its inlined body plus the Start MethodInfo that names it.
    	//   6. 0x180D0308B  Cysharp UniTask.Awaiter IsCompleted (0x3000, IUniTaskSource in r8, token r9w);
    	//      when false: `state = 0`, `<>u__1 = awaiter` ([rdi+0x20], write barrier) and
    	//      AwaitUnsafeOnCompleted<UniTask.Awaiter, <InitializeAsync>d__8>() (0x331940) -> return.
    	//
    	// TODO(evidence): `await InitializeNaninovelAsync(ct)` vs `var task = InitializeNaninovelAsync(ct);
    	//   await task;`. Both spellings compile to this exact body -- the awaited UniTask is a plain stack
    	//   temp in d__8 and never becomes a hoisted field (the awaiter replaces it across the suspension
    	//   point), so the native code cannot separate them.
    	private async Cysharp.Threading.Tasks.UniTask InitializeAsyncCore(CancellationToken ct)
    	{
    		await InitializeNaninovelAsync(ct);
    	}

    	[Token(Token = "0x600000A")]
    	[Address(RVA = "0xD02A70", Offset = "0xD01470", VA = "0x180D02A70", Slot = "6")]
    	[AsyncStateMachine(typeof(_003CSaveSettingsAsync_003Ed__9))]
    	public Cysharp.Threading.Tasks.UniTask SaveSettingsAsync([Optional] CancellationToken ct)
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD02A70, 417 B
    		//
    		// Native evidence (tools/unity-rebuild/out/logs/disas_gigacore_naninovelmanager_savesettings_kickoff_0xD02A70.txt):
    		//   Standard async kickoff: rcx = return buffer (sret) -> rsi, rdx = __this -> rdi,
    		//   r8 = ct -> rbx. Stack state machine at [rsp+0x30] matches the retained d__9 struct exactly:
    		//   state +0x30, builder +0x38, <>4__this +0x48 (stored from rdi with a write barrier),
    		//   ct +0x50 (stored from rbx with a write barrier), <task>5__2 +0x58 (zeroed),
    		//   Cysharp UniTask.Awaiter +0x60 (zeroed, 16 B). Then `state = -1`,
    		//   AsyncUniTaskMethodBuilder.Start<NaninovelManager.<SaveSettingsAsync>d__9>() (0x331870),
    		//   `<builder>.get_Task()` (0x3313C0) copied into the return buffer.
    		//
    		// The recovered body lives in SaveSettingsAsyncCore: writing it *here* as an async method would
    		// make the compiler emit a nested state machine whose name collides with the decompiled
    		// `_003CSaveSettingsAsync_003Ed__9` retained above as evidence-bearing metadata.
    		return SaveSettingsAsyncCore(ct);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD035A0 (state machine body, 1139 B) / 0xD02A70 (kickoff, 417 B)
    	//
    	// Native evidence (tools/unity-rebuild/out/logs/disas_gigacore_naninovelmanager_savesettings_movenext_0xD035A0.txt).
    	// State machine layout as retained above: state +0x0, builder +0x8, <>4__this +0x18, ct +0x20,
    	// `Naninovel.UniTask <task>5__2` +0x28 (a hoisted local: 8 B, the fork's UniTask is a struct wrapping
    	// a single IAwaiter reference), Cysharp UniTask.Awaiter <>u__1 +0x30. One hoisted task and one
    	// awaiter, used inside a loop:
    	//   1. 0x180D03623  rbx = [rdi + 0x18]  -> d__9.<>4__this; `test rbx,rbx / je THROW
    	//      NullReferenceException` (0x180D03A0E) = the receiver null check of the instance member
    	//      access below.
    	//   2. 0x180D0368A  r9 = [rbx + 0x10]   -> this._naniStateManager, via the inlined private
    	//      `NaniStateManager` getter (0xD027B0: field read, `??=` Engine.GetServiceOrErr<IStateManager>()
    	//      at 0x180D036BB, store back to +0x10 with a barrier). 0x180D0371C adds the NRE check for the
    	//      interface call's receiver (0x180D03A08).
    	//   3. 0x180D03722  interface call on TYPE Naninovel.IStateManager_TypeInfo with slot ecx = 0x21 (33),
    	//      no other arguments: dump.cs `IStateManager` Slot 33 = `UniTask SaveSettings();`. The returned
    	//      Naninovel.UniTask is stored to [rdi + 0x28] = <task>5__2 (write barrier at 0x180D03736).
    	//   4. loop head 0x180D03789  `[rdi + 0x28] != 0` then Naninovel.Async.IAwaiter slot 1 on it
    	//      (`get_IsCompleted`, dump.cs IAwaiter Slot 1). That null-or-IsCompleted pair is the inlined
    	//      `Naninovel.UniTask.IsCompleted` body (`awaiter == null || awaiter.IsCompleted`,
    	//      Runtime/Common/Async/UniTask/Public/UniTask.cs:39), and a true result jumps to the epilogue.
    	//   5. 0x180D0380E  Cysharp.Threading.Tasks.UniTask.Yield (0x180C760) with rdx = ct from
    	//      [rdi + 0x20] and r8b = 0 -- dump.cs signature
    	//      `public static UniTask Yield(CancellationToken cancellationToken, bool cancelImmediately = False)`,
    	//      so this is `Yield(ct)` with the default second argument. The usual awaiter dance follows
    	//      (0x3000 IsCompleted; on false `state = 0`, `<>u__1 = awaiter` at +0x30, AwaitUnsafeOnCompleted).
    	//   6. resume (state == 0) loads <>u__1, re-zeroes [rdi+0x30], sets state = -1, calls GetResult (0x31D0)
    	//      and jumps back to the loop head at 4 -- i.e. the await is inside the loop.
    	//   Epilogue 0x180D0396B: `state = -2`, <task>5__2 cleared, builder completion. The clearing is the
    	//   compiler's own end-of-scope store (same shape as ServiceLocator.<UnregisterAsync>d__3.MoveNext),
    	//   not a source-level assignment.
    	//
    	// TODO(evidence): the service access is spelled with the private property `NaniStateManager`, as
    	//   everywhere else in this codebase; repeating its `??=` body inline at this call site would compile
    	//   to the same machine code, so the native body cannot separate the two spellings.
    	private async Cysharp.Threading.Tasks.UniTask SaveSettingsAsyncCore(CancellationToken ct)
    	{
    		Naninovel.UniTask task = NaniStateManager.SaveSettings();
    		while (!task.IsCompleted)
    		{
    			await Cysharp.Threading.Tasks.UniTask.Yield(ct);
    		}
    	}

    	[Token(Token = "0x600000B")]
    	[Address(RVA = "0xD02C20", Offset = "0xD01620", VA = "0x180D02C20")]
    	[AsyncStateMachine(typeof(_003CInitializeNaninovelAsync_003Ed__10))]
    	private static Cysharp.Threading.Tasks.UniTask InitializeNaninovelAsync([Optional] CancellationToken ct)
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD02C20, 317 B
    		//
    		// Native evidence (tools/unity-rebuild/out/logs/disas_gigacore_naninovelmanager_initializenaninovel_kickoff_0xD02C20.txt):
    		//   Standard async kickoff for a static method: rcx = return buffer (sret) -> rdi, rdx = ct -> rbx
    		//   (no __this). Stack state machine at [rsp+0x30] matches the retained d__10 struct exactly:
    		//   state +0x30, builder +0x38, ct +0x48 (stored from rbx with a write barrier),
    		//   Naninovel.UniTask <task>5__2 +0x50 (zeroed), Cysharp UniTask.Awaiter +0x58 (zeroed, 16 B).
    		//   Then `state = -1`,
    		//   AsyncUniTaskMethodBuilder.Start<NaninovelManager.<InitializeNaninovelAsync>d__10>() (0x331870),
    		//   `<builder>.get_Task()` (0x3313C0) copied into the return buffer. This is the body that the
    		//   caller `<InitializeAsync>d__8.MoveNext` contains inlined (see `InitializeAsyncCore`).
    		//
    		// The recovered body lives in InitializeNaninovelAsyncCore: writing it *here* as an async method
    		// would make the compiler emit a nested state machine whose name collides with the decompiled
    		// `_003CInitializeNaninovelAsync_003Ed__10` retained above as evidence-bearing metadata.
    		return InitializeNaninovelAsyncCore(ct);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xD031E0 (state machine body, 947 B) / 0xD02C20 (kickoff, 317 B)
    	//
    	// Native evidence (tools/unity-rebuild/out/logs/disas_gigacore_naninovelmanager_initializenaninovel_movenext_0xD031E0.txt).
    	// State machine layout as retained above: state +0x0, builder +0x8, ct +0x18,
    	// `Naninovel.UniTask <task>5__2` +0x20 (hoisted: read again after every await), Cysharp UniTask.Awaiter
    	// <>u__1 +0x28. Sequence:
    	//   1. 0x180D0328B..0x180D032A2  TYPE Naninovel.Engine_TypeInfo dispatch resolve, then
    	//      `Naninovel.Engine.get_Initialized()` (0x564F60, dump.cs `public static bool Initialized`);
    	//      `test al,al / jne 0x180D03501` (straight to the epilogue) -> `if (Engine.Initialized) return;`
    	//      TODO(evidence): `if (Engine.Initialized) return;` and `if (!Engine.Initialized) { <body> }` produce
    	//        the same guard here (the rest of the method is the tail, so both branch to the epilogue);
    	//        the early-return spelling is used because it matches Engine/RuntimeInitializer's own idiom.
    	//   2. 0x180D032B9  `Naninovel.RuntimeInitializer.Initialize()` (0x56A7D0) called with
    	//      rcx = rdx = r8 = 0. dump.cs signature:
    	//      `public static UniTask Initialize(IConfigurationProvider configurationProvider,
    	//       IEnumerable<ServiceInitializationData> customInitializationData, ITime time)` and the real
    	//      package declares all three parameters optional, so the all-null argument registers are the
    	//      parameterless call, not three explicit nulls. The returned Naninovel.UniTask comes back in
    	//      rax (the csig has no `*` on the return type: the fork's UniTask is an 8-byte by-value struct
    	//      wrapping one IAwaiter reference, matching the 8-byte <task>5__2 field) and is stored to
    	//      [rdi + 0x20] with a write barrier.
    	//   3. loop head 0x180D03319  `[rdi + 0x20] != 0` and then Naninovel.Async.IAwaiter slot 1
    	//      (`get_IsCompleted`) on it -- the inlined `Naninovel.UniTask.IsCompleted`
    	//      (`awaiter == null || awaiter.IsCompleted`). Non-null + completed jumps to 0x180D034FB
    	//      (clear <task>5__2, then the epilogue).
    	//   4. 0x180D0339E  Cysharp.Threading.Tasks.UniTask.Yield (0x180C760) with rdx = ct from [rdi + 0x18]
    	//      and r8b = 0 = `cancelImmediately` default -> `await UniTask.Yield(ct)`. Standard awaiter handling:
    	//      0x3000 IsCompleted; on false `state = 0`, `<>u__1 = awaiter` ([rdi + 0x28] + write barrier at
    	//      0x180D03489), AwaitUnsafeOnCompleted<UniTask.Awaiter, d__10>() (0x331940) and return.
    	//   5. resume (state == 0) loads <>u__1, re-zeroes [rdi+0x28], sets state = -1, calls GetResult (0x31D0)
    	//      and jumps back to the loop head at 3 -- the pre-test loop shape, so the condition is checked
    	//      before the first await (a `do { } while` would await first).
    	//   Epilogue 0x180D034FB/0x180D03501: clear <task>5__2, `state = -2`, builder completion.
    	//
    	// This is the tail of the boot chain that used to hang: `InitializeAsyncCore` awaits this method,
    	// so while both were stubs (`return default(UniTask)`) the await never completed.
    	private static async Cysharp.Threading.Tasks.UniTask InitializeNaninovelAsyncCore(CancellationToken ct)
    	{
    		if (Engine.Initialized) return;

    		Naninovel.UniTask task = RuntimeInitializer.Initialize();
    		while (!task.IsCompleted)
    		{
    			await Cysharp.Threading.Tasks.UniTask.Yield(ct);
    		}
    	}

    	[Token(Token = "0x600000C")]
    	[Address(RVA = "0xD02D60", Offset = "0xD01760", VA = "0x180D02D60", Slot = "9")]
    	protected virtual void ReleaseNaninovelServices()
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0xD02D60, 170 B
    		//
    		// Native evidence (tools/unity-rebuild/out/logs/disas_gigacore_naninovelmanager_releasenaninovelservices_0xD02D60.txt):
    		//   0x180D02D73  `mov qword ptr [rcx + 0x10], r10` (r10 = 0) + GC write barrier at
    		//             0x180D02D7B..0x180D02DB7   -> this._naniStateManager = null
    		//   0x180D02DBF  `mov qword ptr [r9 + 0x18], r10` + GC write barrier -> this._naniScriptManager = null
    		//   0x180D02E09  ret -- nothing else. No engine calls, no null checks; both cached services are
    		//   dropped so the next property access re-resolves them through Engine.GetServiceOrErr<T>().
    		//   `Slot = "9"`: this is the class's own protected virtual slot (INaninovelStateSaver's
    		//   SaveSettingsAsync is 6, INaninovelManager's StartGameScenarioName/InitializeAsync are 7/8);
    		//   it implements no interface member.
    		_naniStateManager = null;
    		_naniScriptManager = null;
    	}

    	[Token(Token = "0x600000D")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	public NaninovelManager()
    	{
    	}
    }
}
