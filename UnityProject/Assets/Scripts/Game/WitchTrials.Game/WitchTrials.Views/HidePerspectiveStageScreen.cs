using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using WitchTrials.Models;

namespace WitchTrials.Views
{
    [Token(Token = "0x2000048")]
    [UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
    [CommandAlias("hideStageScreen")]
    public class HidePerspectiveStageScreen : Command
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000049")]
    	[CompilerGenerated]
    	private struct _003CModifyAsync_003Ed__8
    {
    		[Token(Token = "0x40000DD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40000DE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40000DF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public HidePerspectiveStageScreen _003C_003E4__this;

    		[Token(Token = "0x40000E0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public AsyncToken token;

    		[Token(Token = "0x40000E1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60000F0")]
    		[Address(RVA = "0x31EEE0", Offset = "0x31D8E0", VA = "0x18031EEE0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60000F1")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x40000D8")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	[ParameterAlias("")]
    	[RequiredParameter]
    	public IntegerParameter Index;

    	[Token(Token = "0x40000D9")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	[ParameterAlias("time")]
    	[ParameterDefaultValue("0.35")]
    	public DecimalParameter Duration;

    	// RECONSTRUCTED | Confidence: A | ctor RVA: 0x31EE70, 112 B
    	// Field initializer: the constructor calls `BooleanParameter.op_Implicit(true)`
    	// (`mov cl, 1`), stores the result to +0x40, then tail-jumps to `Command..ctor`.
    	[Token(Token = "0x40000DA")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	[ParameterDefaultValue("true")]
    	public BooleanParameter Reset = true;

    	[Token(Token = "0x40000DB")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
    	public BooleanParameter Wait;

    	[Token(Token = "0x40000DC")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	private IPerspectiveStageScreenModifier _screenModifier;

    	[Token(Token = "0x17000011")]
    	private IPerspectiveStageScreenModifier ScreenModifier
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x31EB80, 186 B
    		// Lazy cache in +0x50. Like `ChangeCourtFireFxActivity`, the service is
    		// looked up as `IPerspectiveStageManager` with `includeDerivedType: true`
    		// (`mov cl, 1`) and stored through its screen-modifier facet.
    		[Token(Token = "0x60000EC")]
    		[Address(RVA = "0x31EB80", Offset = "0x31D580", VA = "0x18031EB80")]
    		get
    		{
    			return _screenModifier ??= ServiceLocator.Get<IPerspectiveStageManager>(true);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x31EC40, 171 B
    	// Builds a `Func<AsyncToken, UniTask>` over `ModifyAsync` and calls vtable
    	// +0x208 = slot 13 = `Command.WaitOrForget(fn, wait, token)` with `Wait`
    	// (+0x48).
    	[Token(Token = "0x60000ED")]
    	[Address(RVA = "0x31EC40", Offset = "0x31D640", VA = "0x18031EC40", Slot = "5")]
    	public override Naninovel.UniTask Execute([Optional] AsyncToken token)
    	{
    		return WaitOrForget(ModifyAsync, Wait, token);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x31ECF0, 383 B (entry) + MoveNext 0x31EEE0
    	// - `duration`: the inlined `Assigned(Duration)` then
    	//   `Naninovel.Nullable<float>.get_Value`, else the rdata float at 0x2AE26D0
    	//   = `3333b33e` = 0.35f, the same value as `[ParameterDefaultValue("0.35")]`.
    	// - The single await is on `sub_3EB0` = IPerspectiveStageScreenModifier
    	//   slot 0 `ChangeScreenVisibilityAsync(screenIndex: Index, visible: false,
    	//   duration, ct: token's CancellationToken)`.
    	// - If `Reset` (the `bool` implicit), `sub_3E00` with ecx = 1 = slot 1
    	//   `SplitScreen(Index, normalizedPosX: 0f, roll: 0f, direction: 1)`, where
    	//   1 = `ScreenSplitDirection.Right`. The service getter and `Index` are
    	//   re-read for the second call.
    	[Token(Token = "0x60000EE")]
    	[Address(RVA = "0x31ECF0", Offset = "0x31D6F0", VA = "0x18031ECF0")]
    	[AsyncStateMachine(typeof(_003CModifyAsync_003Ed__8))]
    	private async Naninovel.UniTask ModifyAsync(AsyncToken token)
    	{
    		float duration = Assigned(Duration) ? Duration.Value : 0.35f;
    		await ScreenModifier.ChangeScreenVisibilityAsync(Index, false, duration, token.CancellationToken);
    		if (Reset)
    		{
    			ScreenModifier.SplitScreen(Index, 0f, 0f, ScreenSplitDirection.Right);
    		}
    	}

    	[Token(Token = "0x60000EF")]
    	[Address(RVA = "0x31EE70", Offset = "0x31D870", VA = "0x18031EE70")]
    	public HidePerspectiveStageScreen()
    	{
    	}
    }
}
