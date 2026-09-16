using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GigaCreation.NaninovelExtender.Common;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;

namespace WitchTrials.Views;

[Token(Token = "0x200003D")]
[UsedImplicitly]
[CommandAlias("correctChoice")]
public class RecordCorrectChoice : SetCustomVariableAsFlag
{
	[StructLayout(3)]
	[Token(Token = "0x200003E")]
	[CompilerGenerated]
	private struct _003CExecute_003Ed__1
{
		[Token(Token = "0x40000AB")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40000AC")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40000AD")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public RecordCorrectChoice _003C_003E4__this;

		[Token(Token = "0x40000AE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60000CE")]
		[Address(RVA = "0x31B3F0", Offset = "0x319DF0", VA = "0x18031B3F0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60000CF")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40000AA")]
	private const string PlayedCorrectChoiceVarName = "g_playedCorrectChoice";

	// RECONSTRUCTED | Confidence: A | RVA: 0x31B2D0 (builder stub); MoveNext 0x31B3F0, 765 B, one await
	// SetCustomVariableAsFlag.SetAndSaveFlagAsync(name, bit, BitwiseOperator.Or) (0xCF9200); the bit is `Number - 1`.
	[Token(Token = "0x60000CC")]
	[Address(RVA = "0x31B2D0", Offset = "0x319CD0", VA = "0x18031B2D0", Slot = "5")]
	[AsyncStateMachine(typeof(_003CExecute_003Ed__1))]
	public override async UniTask Execute([Optional] AsyncToken token)
	{
		await SetAndSaveFlagAsync(PlayedCorrectChoiceVarName, Number - 1, BitwiseOperator.Or);
	}

	[Token(Token = "0x60000CD")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public RecordCorrectChoice()
	{
	}
}
