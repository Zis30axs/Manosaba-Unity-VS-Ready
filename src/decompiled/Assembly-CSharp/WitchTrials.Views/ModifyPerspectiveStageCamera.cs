using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using Il2CppDummyDll;
using JetBrains.Annotations;
using LitMotion;
using Naninovel;
using Naninovel.Async.CompilerServices;
using WitchTrials.Models;

namespace WitchTrials.Views;

[Token(Token = "0x200004C")]
[UsedImplicitly(ImplicitUseKindFlags.InstantiatedNoFixedConstructorSignature)]
[CommandAlias("stageCamera")]
public class ModifyPerspectiveStageCamera : Command
{
	[StructLayout(3)]
	[Token(Token = "0x200004D")]
	[CompilerGenerated]
	private struct _003CModifyAsync_003Ed__11
{
		[Token(Token = "0x40000F2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40000F3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40000F4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ModifyPerspectiveStageCamera _003C_003E4__this;

		[Token(Token = "0x40000F5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x40000F6")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private Cysharp.Threading.Tasks.UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60000FC")]
		[Address(RVA = "0x31FCB0", Offset = "0x31E6B0", VA = "0x18031FCB0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60000FD")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40000EA")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("")]
	[RequiredParameter]
	public IntegerParameter Index;

	[Token(Token = "0x40000EB")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	public DecimalParameter Height;

	[Token(Token = "0x40000EC")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[VectorContext("X,Y,Z", -1, null)]
	public DecimalListParameter Rotation;

	[Token(Token = "0x40000ED")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	public DecimalParameter Distance;

	[Token(Token = "0x40000EE")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("time")]
	[ParameterDefaultValue("0.35")]
	public DecimalParameter Duration;

	[Token(Token = "0x40000EF")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("easing")]
	[ConstantContext(typeof(Ease), -1, null)]
	public StringParameter EaseName;

	[Token(Token = "0x40000F0")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	public BooleanParameter Wait;

	[Token(Token = "0x40000F1")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
	private IPerspectiveStageCameraModifier _cameraModifier;

	[Token(Token = "0x17000013")]
	private IPerspectiveStageCameraModifier CameraModifier
	{
		[Token(Token = "0x60000F8")]
		[Address(RVA = "0x31F9C0", Offset = "0x31E3C0", VA = "0x18031F9C0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60000F9")]
	[Address(RVA = "0x31FA80", Offset = "0x31E480", VA = "0x18031FA80", Slot = "5")]
	public override Naninovel.UniTask Execute([Optional] AsyncToken token)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x60000FA")]
	[Address(RVA = "0x31FB30", Offset = "0x31E530", VA = "0x18031FB30")]
	[AsyncStateMachine(typeof(_003CModifyAsync_003Ed__11))]
	private Naninovel.UniTask ModifyAsync(AsyncToken token)
	{
		return default(Naninovel.UniTask);
	}

	[Token(Token = "0x60000FB")]
	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
	public ModifyPerspectiveStageCamera()
	{
	}
}
