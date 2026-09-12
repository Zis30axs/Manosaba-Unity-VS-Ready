using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x2000063")]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
[CommandAlias("evidence")]
public class ChoiceEvidence : ShowWitchBookUi
{
	[StructLayout(3)]
	[Token(Token = "0x2000065")]
	[CompilerGenerated]
	private struct _003CExecute_003Ed__2
{
		[Token(Token = "0x400014B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400014C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400014D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ChoiceEvidence _003C_003E4__this;

		[Token(Token = "0x400014E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x400014F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private List<WitchBookCategory> _003Ccategories_003E5__2;

		[Token(Token = "0x4000150")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x4000151")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private Naninovel.UniTask<string>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x4000152")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__3;

		[Token(Token = "0x6000146")]
		[Address(RVA = "0x326140", Offset = "0x324B40", VA = "0x180326140", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000147")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000147")]
	private const string ChosenEvidenceIdVarName = "chosenEvidenceId";

	[Token(Token = "0x4000148")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("correct")]
	public StringParameter CorrectEvidenceId;

	[Token(Token = "0x6000140")]
	[Address(RVA = "0x325E70", Offset = "0x324870", VA = "0x180325E70", Slot = "5")]
	[AsyncStateMachine(typeof(_003CExecute_003Ed__2))]
	public override Naninovel.UniTask Execute([Optional] AsyncToken token)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x6000141")]
	[Address(RVA = "0x326000", Offset = "0x324A00", VA = "0x180326000")]
	public ChoiceEvidence()
	{
	}
}
