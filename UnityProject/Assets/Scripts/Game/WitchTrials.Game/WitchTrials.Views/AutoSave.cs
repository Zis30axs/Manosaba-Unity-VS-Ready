using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;

namespace WitchTrials.Views
{
    // NEW IN THE 2026-10-02 GAME UPDATE.  The shipped scenarios reference
    // `WitchTrials.Views.AutoSave (Assembly-CSharp)` (388 managed references across 350 of the 440
    // script assets), but this checkout's dump (src/il2cpp) predates the update and has no such type,
    // so every Script asset that uses it failed to convert.  Shape and bodies below come from a fresh
    // Il2CppDumper run over the updated GameAssembly.dll / global-metadata.dat
    // (work/il2cpp_out) and from capstone disassembly of the shipped bytes.
    [Token(Token = "0x2000034")]
    [UsedImplicitly]
    public class AutoSave : Command
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000035")]
    	[CompilerGenerated]
    	private struct _003CDoAutoSaveAsync_003Ed__3
    {
    		[Token(Token = "0x4000092")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000093")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000094")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public AutoSave _003C_003E4__this;

    		[Token(Token = "0x4000095")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private IDisposable _003C__003E5__2;

    		[Token(Token = "0x4000096")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x4000097")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask<GameStateMap>.Awaiter _003C_003Eu__2;

    		[Token(Token = "0x60000BA")]
    		[Address(RVA = "0x319670", Offset = "0x317E70", VA = "0x180319670", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60000BB")]
    		[Address(RVA = "0x30C000", Offset = "0x30A800", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	// `DelayDuration` (+0x30) and `Wait` (+0x38) are declared here rather than inherited: the shipped
    	// `Naninovel.Command` base only carries ConditionalExpression (+0x10), playbackSpot (+0x18) and
    	// indent (+0x28).  The dumped managed-reference payload for this command lists exactly these two
    	// extra fields, which is what the sync converter matches on.
    	[Token(Token = "0x4000090")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[UsedImplicitly]
    	[ParameterAlias("delay")]
    	public DecimalParameter DelayDuration;

    	[Token(Token = "0x4000091")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[UsedImplicitly]
    	public BooleanParameter Wait;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x319490, 171 B
    	// `new Func<AsyncToken, UniTask>(DoAutoSaveAsync)`, then vtable +0x208 =
    	// slot 13 `Command.WaitOrForget(fn, Wait (+0x38), token)` — byte-for-byte the same shape as
    	// `QuickSave.Execute` (0x31B140) and `QuickLoad.Execute` (0x31A060).
    	[Token(Token = "0x60000B7")]
    	[Address(RVA = "0x319490", Offset = "0x317C90", VA = "0x180319490", Slot = "5")]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return WaitOrForget(DoAutoSaveAsync, Wait, token);
    	}

    	// RECONSTRUCTED | Confidence: A (behaviour), B (the exact spelling of the delay argument) |
    	// RVA: 0x319540, 297 B (entry) + MoveNext 0x319670, 2141 B
    	// Control flow in MoveNext:
    	// - `Assigned(DelayDuration)` (`ICommandParameter` interface call on the parameter object, null
    	//   guarded), then `DecimalParameter`'s `implicit operator float?` and `comiss` against 0: when the
    	//   parameter is assigned and positive the command awaits first.
    	// - `await UniTask.Delay(...)` resolves to the `Delay(TimeSpan, bool ignoreTimeScale = false,
    	//   PlayerLoopTiming delayTiming = 8 (Update), CancellationToken)` overload at RVA 0x519EB0, fed by
    	//   `System.TimeSpan.FromSeconds` on the `cvtps2pd` of that float.  The awaiter is the state
    	//   machine's plain `UniTask.Awaiter <>u__1`.
    	// - `using` (IDisposable at +0x20, disposed only when state < 0)
    	//   `NaninovelLoadingScreen.Show(LoadingMode.Subtle (0), haltInput: false,
    	//   Array.Empty<ICancelEventBlockable>())` — `Show`'s last parameter is `params`, so the
    	//   `Array.Empty` is the compiler's lowering of an omitted argument, exactly as in `QuickSave`.
    	//   - `Engine.GetServiceOrErr<IUIManager>().GetUI<IClickThroughPanelExtended>()`
    	//     slot 1 `DontBlockInteractionOnNext()`, with no null guard.
    	//   - Await `Engine.GetServiceOrErr<IStateManagerExtended>()` slot 4 `AutoSaveAsync()` (the
    	//     interface's own dump records AutoSaveAsync at slot 4 and QuickSaveAsync at slot 5, and the
    	//     helper at 0x3270 is passed exactly those indices); the result is discarded.  Its awaiter is
    	//     `UniTask<GameStateMap>.Awaiter <>u__2`.
    	// - `token` is not used.
    	[Token(Token = "0x60000B8")]
    	[Address(RVA = "0x319540", Offset = "0x317D40", VA = "0x180319540")]
    	[AsyncStateMachine(typeof(_003CDoAutoSaveAsync_003Ed__3))]
    	private async UniTask DoAutoSaveAsync(AsyncToken token)
    	{
            if (FireworksEpilogueSession.Active) return;
            int saveEpoch = FireworksEpilogueSession.SaveEpoch;
    		if (Assigned(DelayDuration) && DelayDuration > 0)
    		{
    			await UniTask.Delay(TimeSpan.FromSeconds(DelayDuration.Value));
    		}
            // An ending's delayed save must not capture the optional epilogue or the later title screen.
            if (FireworksEpilogueSession.Active || saveEpoch != FireworksEpilogueSession.SaveEpoch) return;
    		using (GigaCreation.NaninovelExtender.Common.NaninovelLoadingScreen.Show(GigaCreation.NaninovelExtender.Common.LoadingMode.Subtle, false))
    		{
    			Engine.GetServiceOrErr<IUIManager>().GetUI<GigaCreation.NaninovelExtender.Common.IClickThroughPanelExtended>().DontBlockInteractionOnNext();
    			await Engine.GetServiceOrErr<GigaCreation.NaninovelExtender.Common.IStateManagerExtended>().AutoSaveAsync();
    		}
    	}

    	[Token(Token = "0x60000B9")]
    	[Address(RVA = "0x318720", Offset = "0x316F20", VA = "0x180318720")]
    	public AutoSave()
    	{
    	}
    }
}
