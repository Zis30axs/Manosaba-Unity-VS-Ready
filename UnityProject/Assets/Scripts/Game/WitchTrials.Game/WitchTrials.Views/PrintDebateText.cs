using System;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using FastEnumUtility;
using GigaCreation.NaninovelExtender.Cameras;
using Il2CppDummyDll;
using YieldAwaitable = Naninovel.YieldAwaitable;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using UnityEngine;
using WitchTrials.Models;

namespace WitchTrials.Views
{
    [Token(Token = "0x2000055")]
    [UsedImplicitly]
    [CommandAlias("printDebate")]
    public class PrintDebateText : PrintText
    {
    	[Token(Token = "0x2000057")]
    	[CompilerGenerated]
    	private sealed class _003C_003Ec__DisplayClass19_0
    	{
    		[Token(Token = "0x4000123")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public PrintDebateText _003C_003E4__this;

    		[Token(Token = "0x4000124")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public ITextPrinterActor printer;

    		[Token(Token = "0x6000127")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public _003C_003Ec__DisplayClass19_0()
    		{
    		}

    		// RECONSTRUCTED | Confidence: A | RVA: 0x323A50
    		//
    		// Native evidence: body-folded with `Naninovel.Commands.PrintText.
    		// <>c__DisplayClass31_0.<Execute>b__0` (identical 62-byte body at this
    		// same address, dump.cs lists both managed names for it). Dispatches
    		// through this type's own vtable (dump.cs `PrintText.Print`, Slot 26 --
    		// the same slot/signature `PrintDebateText.Print` overrides), so on a
    		// `PrintDebateText` instance this correctly resolves to the override
    		// reconstructed above, not `PrintText`'s own base implementation.
    		[Token(Token = "0x6000128")]
    		[Address(RVA = "0x323A50", Offset = "0x322450", VA = "0x180323A50")]
    		internal UniTask _003CExecute_003Eb__0(AsyncToken asyncToken)
    		{
    			return _003C_003E4__this.Print(printer, asyncToken);
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000058")]
    	[CompilerGenerated]
    	private struct _003CExecute_003Ed__19
    {
    		[Token(Token = "0x4000125")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000126")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000127")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public PrintDebateText _003C_003E4__this;

    		[Token(Token = "0x4000128")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x4000129")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private _003C_003Ec__DisplayClass19_0 _003C_003E8__1;

    		[Token(Token = "0x400012A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask<ITextPrinterActor>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x400012B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    		private UniTask.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x6000129")]
    		[Address(RVA = "0x323A90", Offset = "0x322490", VA = "0x180323A90", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600012A")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000059")]
    	[CompilerGenerated]
    	private struct _003CPrint_003Ed__21
    {
    		[Token(Token = "0x400012C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400012D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400012E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public PrintDebateText _003C_003E4__this;

    		[Token(Token = "0x400012F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public ITextPrinterActor printer;

    		[Token(Token = "0x4000130")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public AsyncToken token;

    		[Token(Token = "0x4000131")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600012B")]
    		[Address(RVA = "0x324180", Offset = "0x322B80", VA = "0x180324180", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600012C")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x200005A")]
    	[CompilerGenerated]
    	private struct _003CWaitForDebate_003Ed__25
    {
    		[Token(Token = "0x4000132")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000133")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000134")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public PrintDebateText _003C_003E4__this;

    		[Token(Token = "0x4000135")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public float time;

    		[Token(Token = "0x4000136")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public AsyncToken token;

    		[Token(Token = "0x4000137")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private float _003CelapsedTime_003E5__2;

    		[Token(Token = "0x4000138")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private YieldAwaitable.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600012D")]
    		[Address(RVA = "0x324E60", Offset = "0x323860", VA = "0x180324E60", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600012E")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000115")]
    	private const string DebatePrinterId = "Debate";

    	[Token(Token = "0x4000116")]
    	private const string DebateWaitTimeVarName = "debateWaitTime";

    	[Token(Token = "0x4000117")]
    	private const string EnableDebugSkipVarName = "enableDebugSkip";

    	[Token(Token = "0x4000118")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private static readonly Regex s_pattern1;

    	[Token(Token = "0x4000119")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    	private static readonly Regex s_pattern2;

    	[Token(Token = "0x400011A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    	private static readonly Regex s_linkIdRegex;

    	[Token(Token = "0x400011B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x90")]
    	[UsedImplicitly]
    	[ParameterAlias("pos")]
    	public DecimalListParameter ScenePosition;

    	[Token(Token = "0x400011C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x98")]
    	[UsedImplicitly]
    	public DecimalParameter Roll;

    	[Token(Token = "0x400011D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xA0")]
    	[UsedImplicitly]
    	[ParameterAlias("anim")]
    	public StringParameter AnimationKind;

    	[Token(Token = "0x400011E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xA8")]
    	private ICustomVariableManager _naniVarManager;

    	[Token(Token = "0x400011F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xB0")]
    	private IWitchTrialsScriptPlayer _witchTrialsScriptPlayer;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x322230
    	[Token(Token = "0x17000018")]
    	protected override string AssignedPrinterId
    	{
    		[Token(Token = "0x6000113")]
    		[Address(RVA = "0x322230", Offset = "0x320C30", VA = "0x180322230", Slot = "16")]
    		get
    		{
    			return DebatePrinterId;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x322270
    	[Token(Token = "0x17000019")]
    	private ICustomVariableManager NaniVarManager
    	{
    		[Token(Token = "0x6000114")]
    		[Address(RVA = "0x322270", Offset = "0x320C70", VA = "0x180322270")]
    		get
    		{
    			return _naniVarManager ??= Engine.GetServiceOrErr<ICustomVariableManager>();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x322340
    	[Token(Token = "0x1700001A")]
    	private IWitchTrialsScriptPlayer WitchTrialsScriptPlayer
    	{
    		[Token(Token = "0x6000115")]
    		[Address(RVA = "0x322340", Offset = "0x320D40", VA = "0x180322340")]
    		get
    		{
    			return _witchTrialsScriptPlayer ??= Engine.GetServiceOrErr<IWitchTrialsScriptPlayer>();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x322410
    	// `TryGetVariableValue` seeds its out-param to `false` before the call (seen
    	// directly in the disassembly), so a missing/unset variable reads as "off"
    	// rather than throwing or defaulting to "on".
    	[Token(Token = "0x1700001B")]
    	private bool IsEnabledDebugSkip
    	{
    		[Token(Token = "0x6000116")]
    		[Address(RVA = "0x322410", Offset = "0x320E10", VA = "0x180322410")]
    		get
    		{
    			return NaniVarManager.TryGetVariableValue<bool>(EnableDebugSkipVarName, out bool value) && value;
    		}
    	}

    	// RECONSTRUCTED (from `<Execute>d__19.MoveNext`, RVA 0x323A90, 1762 B, 2
    	// await points -> state 0/1) | Confidence: A | wrapper RVA: 0x322480
    	//
    	// Native evidence:
    	//   - `GetOrAddPrinter(token)` (dump.cs `PrinterCommand.GetOrAddPrinter`,
    	//     Slot 20, `UniTask<ITextPrinterActor>`) is called through this type's
    	//     own vtable -- PrintDebateText does not override it, so this is a
    	//     plain inherited virtual call, not a `base.` call.
    	//   - `if (printer is IDebateTextPrinterActor debateTextPrinterActor)
    	//     ModifyDebatePrinter(debateTextPrinterActor);` is a real IL2Cpp
    	//     `isinst` check (klass-depth + ancestor-array compare, same shape
    	//     used elsewhere in this file), not a guess: the call is skipped
    	//     entirely when the cast fails.
    	//   - `WaitOrForget(fn, wait, token)` (dump.cs `Command.WaitOrForget`,
    	//     Slot 13, `UniTask WaitOrForget(Func<AsyncToken, UniTask> fn,
    	//     BooleanParameter wait, AsyncToken token)`) is confirmed the same
    	//     way -- another inherited, unoverridden virtual call -- with `wait`
    	//     constructed from the literal `true` via `BooleanParameter`'s own
    	//     implicit-bool-conversion operator (`mov cl, 1` immediately before
    	//     the call to `op_Implicit`).
    	//   - Only the closure LAMBDA is body-folded with `Naninovel.Commands.
    	//     PrintText`'s own `<>c__DisplayClass31_0.<Execute>b__0` (same RVA
    	//     0x323A50, same 62-byte body, dump.cs lists both managed names for
    	//     that one address). The enclosing `MoveNext` bodies are NOT
    	//     folded -- `PrintText.<Execute>d__31.MoveNext` is a separate,
    	//     similarly-sized function at its own RVA (0x4FF740, 1672 B) --
    	//     but an independent disassembly of it shows the exact same
    	//     `GetOrAddPrinter` -> await -> `WaitOrForget(lambda, true, token)`
    	//     -> await shape, with NO isinst check and no call to
    	//     `ModifyDebatePrinter`/anything actor-type-specific anywhere in it.
    	//     So `PrintDebateText.Execute()` is not "PrintText's Execute reused
    	//     verbatim" -- it is that exact same two-await shape with ONE debate-
    	//     specific step (the `IDebateTextPrinterActor` configuration hook)
    	//     inserted in between. `PrintText`'s own Slots 27-41 (`ShowPrinter`,
    	//     `PlayAutoVoice`, `WaitInput`, `AddBacklog`, etc.) are called from
    	//     neither this `Execute` nor its base -- that machinery lives one
    	//     level deeper, inside `Print()` (Slot 26/35), which is exactly where
    	//     this file's own `Print()` override (reconstructed above) plugs in.
    	//   - State==0 (after `GetOrAddPrinter`) and state==1 (after
    	//     `WaitOrForget`) were each confirmed by tracing their resume labels
    	//     to the same merge points the synchronous-completion paths use;
    	//     state==1's resume leads straight to `SetResult`, confirming the
    	//     method truly ends right after the `WaitOrForget` await.
    	[Token(Token = "0x6000117")]
    	[Address(RVA = "0x322480", Offset = "0x320E80", VA = "0x180322480", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CExecute_003Ed__19))]
    	public override async UniTask Execute([Optional] AsyncToken token)
    	{
    		_003C_003Ec__DisplayClass19_0 _003C_003E8__1 = new _003C_003Ec__DisplayClass19_0
    		{
    			_003C_003E4__this = this
    		};
    		_003C_003E8__1.printer = await GetOrAddPrinter(token);
    		if (_003C_003E8__1.printer is IDebateTextPrinterActor debateTextPrinterActor)
    		{
    			ModifyDebatePrinter(debateTextPrinterActor);
    		}
    		await WaitOrForget(new Func<AsyncToken, UniTask>(_003C_003E8__1._003CExecute_003Eb__0), true, token);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x322610, 496 B
    	// - `Engine.GetServiceOrErr<IInputManager>()`, then `IInputManager`
    	//   interface index 6 (`GetSampler(string)`, dump.cs) with the string
    	//   literal "Continue".
    	// - No sampler: returns a boxed `Naninovel.EmptyDisposable` (a struct,
    	//   dump.cs) and leaves `token` untouched.
    	// - Otherwise: `IInputSampler` index 16 (`GetNext()`, returns a
    	//   `CancellationToken`), then the 2-argument
    	//   `CancellationTokenSource.CreateLinkedTokenSource(token1, token2)`
    	//   (RVA 0x10A37E0, not the `params` overload) over
    	//   `token.CompletionToken` (`[token+8]`) and that token. `GetNext()` is
    	//   called before `[token+8]` is read; a read through the `ref` can't be
    	//   moved past a call that could write through it, so the call is a
    	//   separate statement.
    	// - **Writes through `ref token`**: `token = new AsyncToken(
    	//   token.CancellationToken, cts.Token)`. The native call target is the
    	//   folded 2-reference struct constructor at RVA 0x457060, which is
    	//   `AsyncToken(CancellationToken, CancellationToken)`. The caller's
    	//   token keeps its cancellation token but its completion token now
    	//   also fires on the next "Continue" input.
    	// - Returns the linked source, so the caller's `using`/`Dispose` owns it.
    	[Token(Token = "0x6000118")]
    	[Address(RVA = "0x322610", Offset = "0x321010", VA = "0x180322610", Slot = "15")]
    	protected override IDisposable CompleteOnContinue(ref AsyncToken token)
    	{
    		IInputSampler sampler = Engine.GetServiceOrErr<IInputManager>().GetSampler("Continue");
    		if (sampler == null)
    		{
    			return new EmptyDisposable();
    		}
    		System.Threading.CancellationToken continueToken = sampler.GetNext();
    		System.Threading.CancellationTokenSource cts = System.Threading.CancellationTokenSource.CreateLinkedTokenSource(token.CompletionToken, continueToken);
    		token = new AsyncToken(token.CancellationToken, cts.Token);
    		return cts;
    	}

    	// RECONSTRUCTED (from `<Print>d__21.MoveNext`, RVA 0x324180, 3290 B, 3
    	// await points -> state 0/1/2) | Confidence: A | wrapper RVA: 0x322810
    	//
    	// Restored to structured `async`/`await`/`if` form, not a state-machine
    	// translation. Native evidence, one await at a time:
    	//
    	//   1. `await base.Print(printer, token);` -- the very first call in the
    	//      state machine's main body is a DIRECT call to `0x1804fd430`, which
    	//      dump.cs identifies exactly as `PrintText.Print(ITextPrinterActor,
    	//      AsyncToken)` (Slot 26 -- the same slot/signature this override
    	//      itself has), called with `this` as receiver: i.e. `base.Print(...)`.
    	//      Its `UniTask` result's `IsCompleted`/`GetResult` are checked/called
    	//      through `Naninovel.Async.IAwaiter` with raw `ecx` selectors 1 and 2
    	//      -- confirmed against dump.cs's `IAwaiter` declaration itself
    	//      (`Slot 0 = get_Status`, `Slot 1 = get_IsCompleted`, `Slot 2 =
    	//      GetResult`), not assumed from naming. State==0 is this await's
    	//      resume point; it rejoins the same "GetResult then continue" code
    	//      the synchronous-completion path also uses, so resuming here
    	//      continues into the `IsEnabledDebugSkip` check below -- confirmed by
    	//      tracing both paths to the same merge address (0x1803244AE).
    	//      (Await #2 below checks `IsCompleted` via a direct, non-interface
    	//      call instead -- that's `UniTask.Awaiter.get_IsCompleted` itself
    	//      getting inlined at that one call site, not a different await
    	//      mechanism; `Naninovel.UniTask` is a struct wrapping a single
    	//      reference, and the `test r8,r8` null guard seen at every one of
    	//      these await sites is that wrapper's own null-forwarding, not a
    	//      source-level null check, so it is correctly omitted here.)
    	//
    	//   2. The `IsEnabledDebugSkip` gate, and inside it, the link-id
    	//      extraction: `s_linkIdRegex.Matches(Text)` (same `Text` ->
    	//      `Nullable<LocalizableText>.get_Value()` -> `LocalizableText.
    	//      op_Implicit` conversion chain already established elsewhere in
    	//      this file for the bare `Text` field) piped through `.Where(...)`
    	//      and `.Select(...)` using the two compiler-cached lambdas
    	//      `PrintDebateText.<>c.<Print>b__21_0`/`b__21_1` (RVA 0x3239A0/
    	//      0x3239F0; ILSpy's stub omits the `<>c` cache class entirely, the
    	//      same kind of scaffolding drop already seen once for this type's
    	//      `.cctor`). Both lambda bodies were verified BYTE-FOR-BYTE against
    	//      independent disassemblies of named BCL members:
    	//        - `b__21_0(Match match)`'s field chain (`match.Groups` via the
    	//          virtual call at dump.cs `Match.get_Groups()` Slot 5, then
    	//          `[+0x10]`/`[+0x68]`/`[+0x18]`) is IDENTICAL to
    	//          `GroupCollection.get_Count()`'s own 35-byte body -- so this is
    	//          `match.Groups.Count == 2`, not a hand-inlined field walk.
    	//        - `b__21_1(Match match)` calls `GroupCollection.GetGroup(1, 0)`
    	//          by name (the public indexer `Groups[1]` is an 8-byte tail-jump
    	//          into this same method, confirmed by an independent
    	//          disassembly of `get_Item`), then its tail
    	//          (`[+0x18]`/`[+0x14]`/`[+0x10]` -> `String.Substring`) is
    	//          IDENTICAL to `Capture.get_Value()`'s 40-byte body -- so this is
    	//          `match.Groups[1].Value`.
    	//      `s_linkIdRegex` is, per this file's own already-reconstructed
    	//      `.cctor`, `new Regex("<link=\"(\\w+?)\">", RegexOptions.Compiled)`
    	//      -- exactly one capturing group -- so `match.Groups.Count == 2`
    	//      (group 0 + that one group) is a TAUTOLOGY for every match this
    	//      specific regex can ever produce. Written faithfully as-is rather
    	//      than "simplified away": it is genuinely inert defensive code in
    	//      the original, not a misreading here.
    	//      `linkIds` ends up the list of ids inside `<link="id">` tags in the
    	//      debate text.
    	//
    	//   3. `if (linkIds.Length != 0)`: resets `debateWaitTime` to 0 (a direct
    	//      `TrySetVariableValue<float>(DebateWaitTimeVarName, 0f)` call, mirror
    	//      of the pattern already established in `WaitForDebate`), then
    	//      `await WitchTrialsScriptPlayer.ProcessAutoObjectionAsync(linkIds);`
    	//      and returns immediately. The interface member itself is confirmed
    	//      by dump.cs: `IWitchTrialsScriptPlayer.ProcessAutoObjectionAsync
    	//      (string[] objectionIds)` is exactly Slot 0, `string[]`-in/
    	//      `UniTask`-out, matching the call's `rdx = linkIds` argument and
    	//      `UniTask`-typed result. State==1 (this await's resume point) jumps
    	//      straight into the same "GetResult then SetResult" tail the
    	//      synchronous-completion path also reaches -- i.e. the method really
    	//      does end here, it does not fall through into step 4. Confirmed by
    	//      tracing the resume label's unconditional jump to 0x18032496e and
    	//      from there straight to the final `SetResult` block, bypassing the
    	//      `CalculateWaitTime`/`WaitForDebate` code entirely.
    	//      Read together with the debug-skip auto-processing this gates,
    	//      `ProcessAutoObjectionAsync`/`CancelGosub` (the interface's only two
    	//      members) read as: when `enableDebugSkip` is on, this bypasses
    	//      waiting for a human to click each `<link="id">` evidence prompt and
    	//      programmatically resolves them all at once.
    	//
    	//   4. Otherwise (not debug-skip, or no links found):
    	//      `await WaitForDebate(CalculateWaitTime(), token);` -- the
    	//      disassembly literally constructs a fresh `<WaitForDebate>d__25`
    	//      value on the stack (`<>4__this`/`time`/`token` fields set directly,
    	//      `<>1__state = -1`) and calls
    	//      `AsyncUniTaskMethodBuilder.Start<...>` + `.get_Task` on it -- the
    	//      standard codegen shape for "call and await your own private async
    	//      method", written here as the plain call it represents. State==2 is
    	//      this await's resume point, rejoining the same final
    	//      "GetResult then SetResult" tail -- i.e. the method ends right after
    	//      this await, with no further logic.
    	[Token(Token = "0x6000119")]
    	[Address(RVA = "0x322810", Offset = "0x321210", VA = "0x180322810", Slot = "26")]
    	[AsyncStateMachine(typeof(_003CPrint_003Ed__21))]
    	protected override async UniTask Print(ITextPrinterActor printer, AsyncToken token)
    	{
    		// RECONSTRUCTED | Confidence: A | <>n__0 base-call accessor, RVA: 0x3238E0, 31 B
    		// Compiler-generated for the `base.Print` call inside this async method:
    		// copies the 16-byte AsyncToken and calls `PrintText.Print` (0x4FD430)
    		// non-virtually. It needs no source of its own beyond this line.
    		await base.Print(printer, token);
    		if (IsEnabledDebugSkip)
    		{
    			string[] linkIds = s_linkIdRegex.Matches(Text)
    				.Where((Match match) => match.Groups.Count == 2)
    				.Select((Match match) => match.Groups[1].Value)
    				.ToArray();
    			if (linkIds.Length != 0)
    			{
    				NaniVarManager.TrySetVariableValue(DebateWaitTimeVarName, 0f);
    				await WitchTrialsScriptPlayer.ProcessAutoObjectionAsync(linkIds);
    				return;
    			}
    		}
    		await WaitForDebate(CalculateWaitTime(), token);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x322A00, 238 B
    	// `Engine.GetServiceOrErr<IAudioManager>()`, then
    	// `IEngineService<AudioConfiguration>` index 0 (`Configuration`), then
    	// the bool at `+0x40`, which dump.cs names `EnableAutoVoicing`. If that
    	// is set, the 16-byte `PlaybackSpot` at `Command+0x18` is copied and its
    	// first field (`scriptPath`) is tested for null and for a zero length
    	// (`[str+0x10]`, `jbe`) -- an inlined `string.IsNullOrEmpty`. This is
    	// NOT `PlaybackSpot.Valid`: that getter (RVA 0x5BD6F0, disassembled)
    	// tests `lineIndex`/`inlineIndex`, not the path. `Command.PlaybackSpot`
    	// is an auto-property over that field, so property vs. field access is
    	// not discriminable; the property is written.
    	[Token(Token = "0x600011A")]
    	[Address(RVA = "0x322A00", Offset = "0x321400", VA = "0x180322A00", Slot = "33")]
    	protected override bool ShouldPlayAutoVoice()
    	{
    		return Engine.GetServiceOrErr<IAudioManager>().Configuration.EnableAutoVoicing && !string.IsNullOrEmpty(PlaybackSpot.ScriptPath);
    	}

    	// RECONSTRUCTED | Confidence: A (spelling: B) | RVA: 0x322AF0, 65 B
    	// Runtime class-init check for `Naninovel.UniTask`, then `xor eax, eax`:
    	// returns an already-completed task, so a debate line never waits for
    	// its auto-voice clip and `token` is ignored. `Naninovel.UniTask
    	// .get_CompletedTask` is itself a shared `xor eax, eax; ret` body, so
    	// `CompletedTask` and `default(UniTask)` return the same value. The
    	// class-init check is what a static-member access emits and a bare
    	// `default` doesn't need, which is why `CompletedTask` is written.
    	[Token(Token = "0x600011B")]
    	[Address(RVA = "0x322AF0", Offset = "0x3214F0", VA = "0x180322AF0", Slot = "40")]
    	protected override UniTask WaitAutoVoice(AsyncToken token)
    	{
    		return UniTask.CompletedTask;
    	}

    	// RECONSTRUCTED (previously deferred as "very hard") | Confidence: A |
    	// RVA: 0x322B40, 1590 B
    	//
    	// `NullableFloat -> float` conversions below use the two-step
    	// `((float?)x).Value` cast established elsewhere in this codebase: a bare
    	// `(float)x` is CS0457-ambiguous between `NullableFloat.implicit operator
    	// float?(NullableFloat)` and `Nullable<float>.implicit operator
    	// float(Nullable<float>)`; routing through `float?` first resolves it
    	// unambiguously, matching exactly the two-step unwrap the disassembly
    	// itself performs (`NullableFloat`'s own conversion, then a separate
    	// `Nullable<float>.op_Implicit`/`.Value`-shaped step).
    	//
    	// Three independent `if (Assigned(...))` blocks (each guard confirmed
    	// byte-for-byte against an independent disassembly of `Command.Assigned`
    	// -- same null-check + `ICommandParameter.get_HasValue()` (dump.cs Slot 0)
    	// shape, and the SAME `data_30DC164` type-init flag shared across all
    	// three, confirming they are genuinely the same inlined static call, not
    	// three separately-written checks):
    	//
    	//   - `ScenePosition` (a `DecimalListParameter` = `ParameterList<
    	//     NullableFloat>`): X/Y are percentages (0-100) of the current scene
    	//     rect when explicitly given, falling back to the printer's own
    	//     current position component when not; Z is used as a plain value
    	//     with no scaling. X and Y each compile to TWO separate calls on
    	//     `ScenePosition` -- `ElementAtOrNull(i)` for the null test
    	//     (`sub_25D0`) and, only when non-null, a SEPARATE `get_Item(i)`
    	//     call for the value (`ParameterList<NullableFloat>.get_Item`) --
    	//     so the faithful source-level shape is `ElementAtOrNull(i) != null
    	//     ? ... ScenePosition[i] ... : printer.Position.<c>`, not a single
    	//     bound-and-reused pattern match. Z is the one case that genuinely
    	//     reuses a single `ElementAtOrNull(2)` result via `??` (the result
    	//     flows straight into `Nullable<float>.op_Implicit` with no second
    	//     `get_Item` call) -- confirmed by this asymmetry between the X/Y
    	//     bytes and the Z bytes, not assumed from source-level style. The
    	//     scene rect comes from `CameraConfigurationUtility.
    	//     EvaluateSceneRect(true)` -- the single-arg `Nullable<bool>`
    	//     overload (dump.cs confirms this exact overload exists), called
    	//     with literal `true` (a `Nullable<bool>(true)` is constructed at
    	//     the call site, matching "match screen width"). `Rect`'s field
    	//     layout (x/y/width/height at 0x0/0x4/0x8/0xC, dump.cs) is what the
    	//     `shufps` component-broadcasts key off of (0xaa=width for X,
    	//     0xff=height and 0x55=y for Y). Every `printer.Position.<c>`
    	//     fallback read dispatches through `IActor.get_Position`, dump.cs
    	//     Slot 5, via the generic getter trampoline `sub_39F0` (slot passed
    	//     dynamically in `edx`, confirmed by disassembling the trampoline --
    	//     it is NOT IActor-specific). The final `printer.Position = new
    	//     Vector3(x, y, z);` assignment dispatches through `IActor.
    	//     set_Position`, dump.cs Slot 6, via a FULLY INLINED
    	//     interface-offset-cache scan directly at THIS call site (scan the
    	//     callee's own cached interface-offset table at `[r10+0x12e]`, and
    	//     only on a cache miss fall back to `mov r8d, 6` + `call sub_243A20`
    	//     to resolve the slot, then `call r9` on the resolved pointer) --
    	//     there is NO shared trampoline involved for this particular call,
    	//     unlike `Roll` below. The `printer.Position`/`.Rotation`
    	//     reads/writes below use the same interface (`IDebateTextPrinterActor
    	//     : ITextPrinterActor, IActor`), so no cast is needed at the source
    	//     level.
    	//   - `Roll`: `printer.Rotation = Quaternion.Euler(0f, 0f, Roll);` --
    	//     confirmed by reading the cached-icall name string at the resolved
    	//     call target: `"UnityEngine.Quaternion::Internal_FromEulerRad_
    	//     Injected(...)"`, called after multiplying by a data-section
    	//     constant that reads back as exactly `Mathf.Deg2Rad`
    	//     (0.017453292...) -- i.e. `Quaternion.Euler`'s own public
    	//     degrees-to-radians conversion, inlined. Unlike `set_Position`
    	//     above, this write genuinely goes through a SHARED runtime
    	//     trampoline: `call sub_38B0` (RVA 0x38B0), independently
    	//     disassembled and confirmed to have `IActor.set_Rotation`'s slot
    	//     (dump.cs Slot 8) hardcoded into its own machine code (`mov r8d,
    	//     8`) rather than taking it as a parameter -- Position and Rotation
    	//     use genuinely different dispatch mechanisms here, not the same
    	//     mechanism with a different slot constant.
    	//   - `AnimationKind`: `FastEnum.TryParse<DebateTextAnimationKind>(
    	//     AnimationKind, true, out var animationKind)` (dump.cs confirms this
    	//     exact `TryParse<T>(string, bool, out T)` overload) either sets
    	//     `printer.AnimationKind` (dump.cs `IDebateTextPrinterActor.
    	//     set_AnimationKind`, Slot 1) on success, or calls `Warn(...)` (dump.cs
    	//     `Command.Warn`, Slot 7) with the exact literal format string read
    	//     from the data section (`"Invalid animation kind: {0}"`) on failure
    	//     -- the parse result is never used un-checked.
    	[Token(Token = "0x600011C")]
    	[Address(RVA = "0x322B40", Offset = "0x321540", VA = "0x180322B40")]
    	private void ModifyDebatePrinter(IDebateTextPrinterActor printer)
    	{
    		if (Assigned(ScenePosition))
    		{
    			Rect sceneRect = CameraConfigurationUtility.EvaluateSceneRect(true);
    			float x = (ScenePosition.ElementAtOrNull(0) != null)
    				? sceneRect.x + ((float?)ScenePosition[0]).Value / 100f * sceneRect.width
    				: printer.Position.x;
    			float y = (ScenePosition.ElementAtOrNull(1) != null)
    				? sceneRect.y + ((float?)ScenePosition[1]).Value / 100f * sceneRect.height
    				: printer.Position.y;
    			float z = ((float?)(ScenePosition.ElementAtOrNull(2) ?? (NullableFloat)printer.Position.z)).Value;
    			printer.Position = new Vector3(x, y, z);
    		}
    		if (Assigned(Roll))
    		{
    			printer.Rotation = Quaternion.Euler(0f, 0f, Roll);
    		}
    		if (Assigned(AnimationKind))
    		{
    			if (FastEnum.TryParse<DebateTextAnimationKind>(AnimationKind, true, out DebateTextAnimationKind animationKind))
    			{
    				printer.AnimationKind = animationKind;
    			}
    			else
    			{
    				Warn(string.Format("Invalid animation kind: {0}", AnimationKind));
    			}
    		}
    	}

    	// RECONSTRUCTED (from the `<WaitForDebate>d__25.MoveNext` state machine,
    	// RVA 0x324E60) | Confidence: A | wrapper RVA: 0x323180
    	//
    	// Restored to structured `async`/`await`/`while` form rather than left as
    	// a state-machine translation. Native evidence:
    	//   - `elapsedTime` starts at 0f (the state machine's pre-state-check
    	//     `xorps xmm0,xmm0`) and, on every resume, is recomputed as
    	//     `elapsedTime + UnityEngine.Time.deltaTime` -- confirmed via the
    	//     data-section icall name string at the helper's target address
    	//     (`"UnityEngine.Time::get_deltaTime()"`, read directly, not guessed).
    	//   - Loop condition is `elapsedTime < time` (native: `time <= elapsedTime`
    	///    exits).
    	//   - Per iteration: `token.ThrowIfCanceled()`, an early `break` if
    	//     `token.Completed`, an unconditional (non-null-conditional --
    	//     confirmed by the NRE-throwing null check on `this`) write of the
    	//     remaining time into the `debateWaitTime` variable, then one frame's
    	//     wait.
    	//   - The awaited call was verified byte-for-byte against an independent
    	//     disassembly of `AsyncUtils.WaitEndOfFrame(AsyncToken)` (RVA
    	//     0x547300): identical `PlayerLoopTiming.PostLateUpdate` (`0xc`)
    	//     constant and struct-construction shape, so it is written as a call
    	//     to that named helper rather than the raw inlined form.
    	//   - On resume (state == 0), the `ThrowIfCanceled()` immediately after
    	//     restoring the awaiter (0x180324F1A) operates on `[rsp+0x48]`, an
    	//     address INSIDE the local copy of the restored `<>u__1` awaiter
    	//     struct -- not on `this.token` (`[rbx+0x28]`, used by the real,
    	//     source-level `ThrowIfCanceled()` at the top of the loop body,
    	//     0x180324F73). That first one is the awaiter's own `GetResult()`,
    	//     which `await` already emits; it is not a second source-level call
    	//     and is intentionally not written out separately.
    	[Token(Token = "0x600011D")]
    	[Address(RVA = "0x323180", Offset = "0x321B80", VA = "0x180323180")]
    	[AsyncStateMachine(typeof(_003CWaitForDebate_003Ed__25))]
    	private async UniTask WaitForDebate(float time, [Optional] AsyncToken token)
    	{
    		float elapsedTime = 0f;
    		while (elapsedTime < time)
    		{
    			token.ThrowIfCanceled();
    			if (token.Completed) break;
    			NaniVarManager.TrySetVariableValue(DebateWaitTimeVarName, time - elapsedTime);
    			await AsyncUtils.WaitEndOfFrame(token);
    			elapsedTime += Time.deltaTime;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x323330
    	//
    	// Native evidence:
    	//   - debug-skip short-circuits to a flat 0.1f wait.
    	//   - otherwise, if `IsPlayingAutoVoice()` is true, looks up
    	//     `AudioManager.GetVoiceTrack(path)` (dump.cs `IAudioManager` Slot 31,
    	//     `IAudioTrack GetVoiceTrack(string path)`, confirmed via the raw `ecx
    	//     = 0x1f` selector loaded immediately before the dispatch) unconditionally
    	//     (throws if `AudioManager` is null, not a graceful skip) and, if the
    	//     returned track `is AudioTrack` (a real IL2Cpp inline isinst-check, not
    	//     a guess -- klass-depth compare then ancestor-array compare), returns
    	//     `audioTrack.Clip.length` -- i.e. the reveal wait syncs to the actual
    	//     voice clip's duration when one is available.
    	//   - Both virtual calls in this method dispatch through `this`'s own
    	//     vtable at offsets that were originally misidentified (as
    	//     `ShouldPlayAutoVoice()`/`BuildAutoVoicePath()`) by assuming one of
    	//     them and back-solving the slot delta, which is circular. The vtable
    	//     base was instead anchored independently from two other call sites in
    	//     this same file/type family: `Naninovel.Commands.Goto.Execute` is
    	//     dump.cs Slot 5 and is dispatched here at `[klass+0x188]`, and
    	//     `StringBuilder.ToString()` (Object's Slot 3) is dispatched at
    	//     `[klass+0x168]` in `TimeSpanToText` -- both give vtable base
    	//     `0x138` (`offset = 0x138 + 16*slot`). Applying that base:
    	//       * `[klass+0x3d8]` -> slot 42 = `PrintText.IsPlayingAutoVoice()`
    	//         (dump.cs confirms Slot 42 on that exact signature; Slot 33,
    	//         previously assumed here, is `ShouldPlayAutoVoice()` and is
    	//         semantically wrong for this call site anyway -- this check is
    	//         gating a voice-track lookup, not a "should we" policy check).
    	//       * `[klass+0x2b8]` -> slot 24 = `PrintText.get_AutoVoicePath()`
    	//         (dump.cs Slot 24 on that exact getter; Slot 29,
    	//         `BuildAutoVoicePath()`, previously guessed here, is 5 slots off).
    	//   - Falls back to `CalculateWaitTimeFromMessage(Text)` when there is no
    	//     voice track to sync to.
    	[Token(Token = "0x600011E")]
    	[Address(RVA = "0x323330", Offset = "0x321D30", VA = "0x180323330")]
    	private float CalculateWaitTime()
    	{
    		if (IsEnabledDebugSkip) return 0.1f;
    		if (IsPlayingAutoVoice())
    		{
    			IAudioTrack voiceTrack = AudioManager.GetVoiceTrack(AutoVoicePath);
    			if (voiceTrack is AudioTrack audioTrack)
    			{
    				return audioTrack.Clip.length;
    			}
    		}
    		return CalculateWaitTimeFromMessage(Text);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3234E0
    	//
    	// Native evidence: strips (in order) `s_pattern1` = `[ 　「」『』【】]`
    	// (spaces and Japanese bracket/quote punctuation) and `s_pattern2` =
    	// `<.+?>` (any rich-text tag) via the standard 3-arg `Regex.Replace(input,
    	// "", -1)` overload (the 4-arg form visible in the disassembly -- with an
    	// explicit `startat` computed from `RightToLeft` -- is that overload's own
    	// well-known inlined body, not separate logic written here), then computes
    	// `cleanedLength * 0.15f + 0.6f` (both constants read directly from the
    	// data section) -- a base 0.6s plus 0.15s per remaining character.
    	[Token(Token = "0x600011F")]
    	[Address(RVA = "0x3234E0", Offset = "0x321EE0", VA = "0x1803234E0")]
    	private static float CalculateWaitTimeFromMessage(string message)
    	{
    		string cleaned = s_pattern2.Replace(s_pattern1.Replace(message, "", -1), "", -1);
    		return cleaned.Length * 0.15f + 0.6f;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3236A0
    	// `.cctor` -- not present in the original ILSpy-generated stub (ILSpy
    	// dropped it), but it is a real native method with its own RVA, found via
    	// the symbol DB rather than the dump.cs excerpt rendered for this type.
    	// Pattern text extracted directly from the data section (not guessed):
    	// s_pattern1 matches spaces/Japanese bracket-quote punctuation, s_pattern2
    	// matches any rich-text tag, s_linkIdRegex extracts a `<link="id">` id.
    	// `RegexOptions.Compiled` (== 8) confirmed from the constructor argument.
    	static PrintDebateText()
    	{
    		s_pattern1 = new Regex("[ 　「」『』【】]", RegexOptions.Compiled);
    		s_pattern2 = new Regex("<.+?>", RegexOptions.Compiled);
    		s_linkIdRegex = new Regex("<link=\"(\\w+?)\">", RegexOptions.Compiled);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x323690, 7 B
    	// FOLDED BODY shared with `PrintTextModified..ctor`: `xor edx, edx; jmp
    	// Naninovel.Commands.PrintText..ctor`. The constructor does nothing but
    	// chain to the base, so there are no instance field initializers.
    	// Not counted as progress: `ranked.json` lists this 7-byte shared body
    	// under `aliased` (alias 2, <= 0x40), outside every subsystem bucket.
    	[Token(Token = "0x6000120")]
    	[Address(RVA = "0x323690", Offset = "0x322090", VA = "0x180323690")]
    	public PrintDebateText()
    	{
    	}
    }
}
