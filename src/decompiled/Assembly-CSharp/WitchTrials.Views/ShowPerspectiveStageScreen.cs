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

[Token(Token = "0x2000051")]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
[CommandAlias("showStageScreen")]
public class ShowPerspectiveStageScreen : Command
{
	[StructLayout(3)]
	[Token(Token = "0x2000052")]
	[CompilerGenerated]
	private struct _003CModifyAsync_003Ed__7
{
		[Token(Token = "0x400010A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400010B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400010C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ShowPerspectiveStageScreen _003C_003E4__this;

		[Token(Token = "0x400010D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x400010E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600010B")]
		[Address(RVA = "0x3217D0", Offset = "0x3201D0", VA = "0x1803217D0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600010C")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000106")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("")]
	[RequiredParameter]
	public IntegerParameter Index;

	[Token(Token = "0x4000107")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("time")]
	[ParameterDefaultValue("0.35")]
	public DecimalParameter Duration;

	[Token(Token = "0x4000108")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	public BooleanParameter Wait;

	[Token(Token = "0x4000109")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	private IPerspectiveStageScreenModifier _stageModifier;

	[Token(Token = "0x17000015")]
	private IPerspectiveStageScreenModifier StageModifier
	{
		[Token(Token = "0x6000107")]
		[Address(RVA = "0x3214E0", Offset = "0x31FEE0", VA = "0x1803214E0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000108")]
	[Address(RVA = "0x3215A0", Offset = "0x31FFA0", VA = "0x1803215A0", Slot = "5")]
	public override Naninovel.UniTask Execute([Optional] AsyncToken token)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x6000109")]
	[Address(RVA = "0x321650", Offset = "0x320050", VA = "0x180321650")]
	[AsyncStateMachine(typeof(_003CModifyAsync_003Ed__7))]
	private Naninovel.UniTask ModifyAsync(AsyncToken token)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x600010A")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public ShowPerspectiveStageScreen()
	{
	}
}
