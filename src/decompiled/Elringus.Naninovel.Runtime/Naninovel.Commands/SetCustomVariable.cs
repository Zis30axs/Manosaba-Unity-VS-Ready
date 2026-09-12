using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.Expression;

namespace Naninovel.Commands;

[Token(Token = "0x2000702")]
[CommandAlias("set")]
public class SetCustomVariable : Command
{
	[StructLayout(3)]
	[Token(Token = "0x2000703")]
	[CompilerGenerated]
	private struct _003CExecute_003Ed__1
{
		[Token(Token = "0x4001ACE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4001ACF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4001AD0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SetCustomVariable _003C_003E4__this;

		[Token(Token = "0x4001AD1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6002172")]
		[Address(RVA = "0x5061A0", Offset = "0x504BA0", VA = "0x1805061A0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6002173")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4001ACD")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[ParameterAlias("")]
	[RequiredParameter]
	[AssignmentContext(-1, null)]
	public StringParameter Expression;

	[Token(Token = "0x600216D")]
	[Address(RVA = "0x505AD0", Offset = "0x5044D0", VA = "0x180505AD0", Slot = "5")]
	[AsyncStateMachine(typeof(_003CExecute_003Ed__1))]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x600216E")]
	[Address(RVA = "0x505BF0", Offset = "0x5045F0", VA = "0x180505BF0", Slot = "16")]
	protected virtual void AssignEvaluated(string var, IOperand result, ICustomVariableManager vars)
	{
	}

	[Token(Token = "0x600216F")]
	[Address(RVA = "0x505EE0", Offset = "0x5048E0", VA = "0x180505EE0", Slot = "17")]
	protected virtual bool ShouldSaveGlobalState(IReadOnlyList<Assignment> asses)
	{
		return default(bool);
	}

	[Token(Token = "0x6002170")]
	[Address(RVA = "0x5060D0", Offset = "0x504AD0", VA = "0x1805060D0", Slot = "18")]
	protected virtual void LogErrorMessage([Optional] string desc)
	{
	}

	[Token(Token = "0x6002171")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public SetCustomVariable()
	{
	}
}
