using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.Commands;
using Naninovel.Metadata;

namespace GigaCreation.NaninovelExtender.Common
{
    [Token(Token = "0x2000005")]
    [UsedImplicitly]
    [Branch(BranchTraits.Endpoint, null, null)]
    [CommandAlias("goto")]
    public class GotoModified : Goto
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000007")]
    	[CompilerGenerated]
    	private struct _003CExecute_003Ed__1
    {
    		[Token(Token = "0x400000B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400000C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400000D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public GotoModified _003C_003E4__this;

    		[Token(Token = "0x400000E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private IDisposable _003C__003E5__2;

    		[Token(Token = "0x400000F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600000C")]
    		[Address(RVA = "0xCF7F00", Offset = "0xCF6900", VA = "0x180CF7F00", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600000D")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x2000008")]
    	[CompilerGenerated]
    	private struct _003CNavigateOtherScript_003Ed__2
    {
    		[Token(Token = "0x4000010")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000011")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000012")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public GotoModified _003C_003E4__this;

    		[Token(Token = "0x4000013")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public string scriptPath;

    		[Token(Token = "0x4000014")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public string label;

    		[Token(Token = "0x4000015")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600000E")]
    		[Address(RVA = "0xCF8600", Offset = "0xCF7000", VA = "0x180CF8600", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600000F")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000007")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	[UsedImplicitly]
    	[ParameterAlias("soft")]
    	public BooleanParameter SoftReset;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF7AD0 (builder stub); MoveNext 0xCF7F00.
    	// The body lives in ExecuteCore (XxxCore convention; <Execute>d__1 metadata stays; the token is not used):
    	//   Goto's own flow through vtable slots 16 TryGetScriptPathAndLabel, 17 ShouldNavigatePlayedScript,
    	//   18 NavigatePlayedScript, 19 ShouldMaskWithLoadingScreen, 20 NavigateOtherScript -- except that the mask is
    	//   NaninovelLoadingScreen.Show(1 = Short, false, Array.Empty) held in a `using` local named `_` (<_>5__2).
    	// Annotated disassembly: work/title-path/disas/gm_02_Execute_MoveNext.txt
    	[Token(Token = "0x6000005")]
    	[Address(RVA = "0xCF7AD0", Offset = "0xCF64D0", VA = "0x180CF7AD0", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CExecute_003Ed__1))]
    	public override UniTask Execute([Optional] AsyncToken token)
    	{
    		return ExecuteCore();
    	}

    	private async UniTask ExecuteCore()
    	{
    		if (!TryGetScriptPathAndLabel(out string scriptPath, out string label))
    		{
    			return;
    		}
    		if (ShouldNavigatePlayedScript(scriptPath))
    		{
    			NavigatePlayedScript(label);
    		}
    		else if (ShouldMaskWithLoadingScreen())
    		{
    			using IDisposable _ = NaninovelLoadingScreen.Show(LoadingMode.Short, haltInput: false);
    			await NavigateOtherScript(scriptPath, label);
    		}
    		else
    		{
    			await NavigateOtherScript(scriptPath, label);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF7BF0 (builder stub); MoveNext 0xCF8600.
    	// The body lives in NavigateOtherScriptCore (XxxCore convention; <NavigateOtherScript>d__2 metadata stays):
    	//   closure <>c__DisplayClass2_0 (this, scriptPath, label; b__0 = LoadAndPlay); Engine.GetServiceOrErr<
    	//   IStateManagerExtended>; slot 21 ShouldResetAll -> ResetStateAsync(Engine.Types.DontReset, SoftReset (+0x50,
    	//   Nullable<bool> op_Implicit), [b__0]); slot 22 ShouldResetSpecific -> ResetStateAsync(ResetState
    	//   .ToReadOnlyList(), SoftReset, [b__0]); otherwise LoadAndPlay awaited directly.
    	// Annotated disassembly: work/title-path/disas/gm_04_NavigateOtherScript_MoveNext.txt
    	[Token(Token = "0x6000006")]
    	[Address(RVA = "0xCF7BF0", Offset = "0xCF65F0", VA = "0x180CF7BF0", Slot = "20")]
    	[AsyncStateMachine(typeof(_003CNavigateOtherScript_003Ed__2))]
    	protected override UniTask NavigateOtherScript(string scriptPath, string label)
    	{
    		return NavigateOtherScriptCore(scriptPath, label);
    	}

    	private async UniTask NavigateOtherScriptCore(string scriptPath, string label)
    	{
    		IStateManagerExtended state = Engine.GetServiceOrErr<IStateManagerExtended>();
    		if (ShouldResetAll())
    		{
    			await state.ResetStateAsync(Engine.Types.DontReset, SoftReset, () => LoadAndPlay(scriptPath, label));
    		}
    		else if (ShouldResetSpecific())
    		{
    			await state.ResetStateAsync(ResetState.ToReadOnlyList(), SoftReset, () => LoadAndPlay(scriptPath, label));
    		}
    		else
    		{
    			await LoadAndPlay(scriptPath, label);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCF7DD0 -- string.IsNullOrEmpty inlined; Engine.GetServiceOrErr<IScriptPlayer>()
    	//   (Goto.Player) then ScriptPlayerExtensions.LoadAndPlay(path, 0) or LoadAndPlayAtLabel(path, label)
    	[Token(Token = "0x6000007")]
    	[Address(RVA = "0xCF7DD0", Offset = "0xCF67D0", VA = "0x180CF7DD0")]
    	private UniTask LoadAndPlay(string scriptPath, string label)
    	{
    		return string.IsNullOrEmpty(label) ? Player.LoadAndPlay(scriptPath) : Player.LoadAndPlayAtLabel(scriptPath, label);
    	}

    	[Token(Token = "0x6000008")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public GotoModified()
    	{
    	}
    }
}
