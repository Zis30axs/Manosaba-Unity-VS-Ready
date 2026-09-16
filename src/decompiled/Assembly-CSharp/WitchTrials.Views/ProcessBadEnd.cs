using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GigaCreation.NaninovelExtender.Common;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.Metadata;

namespace WitchTrials.Views;

[Token(Token = "0x2000037")]
[UsedImplicitly]
[Branch(BranchTraits.Endpoint, null, null)]
[CommandAlias("badEnd")]
public class ProcessBadEnd : SetCustomVariableAsFlag
{
	[StructLayout(3)]
	[Token(Token = "0x2000038")]
	[CompilerGenerated]
	private struct _003CExecute_003Ed__3
{
		[Token(Token = "0x4000097")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000098")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000099")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ProcessBadEnd _003C_003E4__this;

		[Token(Token = "0x400009A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60000BF")]
		[Address(RVA = "0x319AD0", Offset = "0x3184D0", VA = "0x180319AD0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60000C0")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000094")]
	private const string BadEndScriptName = "System/System_BadEnd";

	[Token(Token = "0x4000095")]
	private const string PlayedBadEndVarName = "g_playedBadEnd";

	[Token(Token = "0x4000096")]
	private const string CurrentBadEndIdVarName = "currentBadEndId";

	// RECONSTRUCTED | Confidence: A | RVA: 0x3199B0 (builder stub); MoveNext 0x319AD0, 1421 B, two await states
	// The bad end number goes to `currentBadEndId`, is recorded as a played flag, then System/System_BadEnd plays
	// (ScriptPlayerExtensions.LoadAndPlay, playlistIndex 0). SetCustomVariableAsFlag.SetAndSaveFlagAsync(name, bit, BitwiseOperator.Or) (0xCF9200); the bit is `Number - 1`.
	[Token(Token = "0x60000BD")]
	[Address(RVA = "0x3199B0", Offset = "0x3183B0", VA = "0x1803199B0", Slot = "5")]
	[AsyncStateMachine(typeof(_003CExecute_003Ed__3))]
	public override async UniTask Execute([Optional] AsyncToken token)
	{
		NaniVarManager.TrySetVariableValue<int>(CurrentBadEndIdVarName, Number);
		await SetAndSaveFlagAsync(PlayedBadEndVarName, Number - 1, BitwiseOperator.Or);
		await Engine.GetServiceOrErr<IScriptPlayer>().LoadAndPlay(BadEndScriptName);
	}

	[Token(Token = "0x60000BE")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public ProcessBadEnd()
	{
	}
}
