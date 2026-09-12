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

[Token(Token = "0x200004E")]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
[CommandAlias("shakeStageCamera")]
public class ShakePerspectiveStageCamera : Command
{
	[StructLayout(3)]
	[Token(Token = "0x2000050")]
	[CompilerGenerated]
	private struct _003CShakeAsync_003Ed__11
{
		[Token(Token = "0x4000101")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000102")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000103")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ShakePerspectiveStageCamera _003C_003E4__this;

		[Token(Token = "0x4000104")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x4000105")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000105")]
		[Address(RVA = "0x320B10", Offset = "0x31F510", VA = "0x180320B10", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000106")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40000F7")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("")]
	[RequiredParameter]
	public IntegerParameter Index;

	[Token(Token = "0x40000F8")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterDefaultValue("1,0,0")]
	[VectorContext("X,Y,Z", -1, null)]
	public DecimalListParameter Force;

	[Token(Token = "0x40000F9")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterDefaultValue("10")]
	public IntegerParameter Frequency;

	[Token(Token = "0x40000FA")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("damp")]
	[ParameterDefaultValue("1")]
	public DecimalParameter DampingRatio;

	[Token(Token = "0x40000FB")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("random")]
	public BooleanParameter Randomly;

	[Token(Token = "0x40000FC")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("time")]
	[ParameterDefaultValue("0.35")]
	public DecimalParameter Duration;

	[Token(Token = "0x40000FD")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	public BooleanParameter Wait;

	[Token(Token = "0x40000FE")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
	private IPerspectiveStageCameraModifier _cameraModifier;

	[Token(Token = "0x17000014")]
	private IPerspectiveStageCameraModifier CameraModifier
	{
		[Token(Token = "0x60000FE")]
		[Address(RVA = "0x320420", Offset = "0x31EE20", VA = "0x180320420")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60000FF")]
	[Address(RVA = "0x3204E0", Offset = "0x31EEE0", VA = "0x1803204E0", Slot = "5")]
	public override Naninovel.UniTask Execute([Optional] AsyncToken token)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x6000100")]
	[Address(RVA = "0x320590", Offset = "0x31EF90", VA = "0x180320590")]
	[AsyncStateMachine(typeof(_003CShakeAsync_003Ed__11))]
	private Naninovel.UniTask ShakeAsync(AsyncToken token)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x6000101")]
	[Address(RVA = "0x320710", Offset = "0x31F110", VA = "0x180320710")]
	public ShakePerspectiveStageCamera()
	{
	}
}
