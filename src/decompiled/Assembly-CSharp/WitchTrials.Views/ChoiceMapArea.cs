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

[Token(Token = "0x2000066")]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
[CommandAlias("map")]
public class ChoiceMapArea : ShowWitchBookUi
{
	[StructLayout(3)]
	[Token(Token = "0x2000067")]
	[CompilerGenerated]
	private struct _003CExecute_003Ed__3
{
		[Token(Token = "0x4000156")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000157")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000158")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ChoiceMapArea _003C_003E4__this;

		[Token(Token = "0x4000159")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x400015A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private IReadOnlyList<MapAreaKind> _003CrestrictionAreas_003E5__2;

		[Token(Token = "0x400015B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private Naninovel.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x400015C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private Naninovel.UniTask<MapAreaKind>.Awaiter _003C_003Eu__2;

		[Token(Token = "0x400015D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__3;

		[Token(Token = "0x600014B")]
		[Address(RVA = "0x327440", Offset = "0x325E40", VA = "0x180327440", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600014C")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000153")]
	private const string ChosenAreaIdVarName = "chosenAreaId";

	[Token(Token = "0x4000154")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[RequiredParameter]
	[ParameterAlias("")]
	public StringListParameter MapAreaNames;

	[Token(Token = "0x4000155")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x70")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("flag")]
	public StringParameter FlagVariableName;

	[Token(Token = "0x6000148")]
	[Address(RVA = "0x326EA0", Offset = "0x3258A0", VA = "0x180326EA0", Slot = "5")]
	[AsyncStateMachine(typeof(_003CExecute_003Ed__3))]
	public override Naninovel.UniTask Execute([Optional] AsyncToken token)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x6000149")]
	[Address(RVA = "0x327030", Offset = "0x325A30", VA = "0x180327030")]
	private List<MapAreaKind> GetRestrictionAreas()
	{
		return null;
	}

	[Token(Token = "0x600014A")]
	[Address(RVA = "0x326000", Offset = "0x324A00", VA = "0x180326000")]
	public ChoiceMapArea()
	{
	}
}
