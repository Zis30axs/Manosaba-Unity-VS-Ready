using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Cysharp.Threading.Tasks;
using FastEnumUtility;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using JetBrains.Annotations;
using LitMotion;
using Naninovel;
using Naninovel.Async.CompilerServices;
using WitchTrials.Models;

namespace WitchTrials.Views
{
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
    		// RECONSTRUCTED | Confidence: A | RVA: 0x31F9C0, 186 B
    		// Lazy cache in +0x68 via `ServiceLocator.Get<IPerspectiveStageManager>(true)`.
    		[Token(Token = "0x60000F8")]
    		[Address(RVA = "0x31F9C0", Offset = "0x31E3C0", VA = "0x18031F9C0")]
    		get
    		{
    			return _cameraModifier ??= ServiceLocator.Get<IPerspectiveStageManager>(true);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x31FA80, 171 B
    	// `WaitOrForget` (vtable +0x208, slot 13) over `ModifyAsync` with `Wait` (+0x60).
    	[Token(Token = "0x60000F9")]
    	[Address(RVA = "0x31FA80", Offset = "0x31E480", VA = "0x18031FA80", Slot = "5")]
    	public override Naninovel.UniTask Execute([Optional] AsyncToken token)
    	{
    		return WaitOrForget(ModifyAsync, Wait, token);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x31FB30, 383 B (entry) + MoveNext 0x31FCB0
    	// Single await.
    	// - `duration` = `Duration` when assigned, else 0.35f (rdata 0x2AE26D0).
    	// - `ease` is a zero-initialized local (0 = `Ease.Linear`). When `EaseName`
    	//   is assigned, its string (the inlined StringParameter -> string
    	//   conversion: `HasValue` at vtable +0x1C8, then `Nullable<string>.Value`)
    	//   goes to `FastEnum.TryParse<Ease>(value, ignoreCase: true, out ease)`
    	//   (`TryParseInternal` with `dl = 1`). On failure the literal "Failed to
    	//   parse `{0}` easing." is formatted with the raw parameter object and
    	//   sent to Command vtable +0x1A8 = slot 7 `Warn`; execution continues.
    	// - Each tween is built only when its parameter is assigned, else null.
    	//   Height and distance use inlined record constructors (To +0x10,
    	//   Duration +0x14, Ease +0x18); rotation calls its constructor (0x39FA80)
    	//   with the `float?[]` implicit of `Rotation`.
    	// - `sub_3C80` resolves IPerspectiveStageCameraModifier slot 0
    	//   `ModifyCameraAsync(Index, heightTween, rotationTween, distanceTween,
    	//   token's CancellationToken)`; the getter is inlined.
    	[Token(Token = "0x60000FA")]
    	[Address(RVA = "0x31FB30", Offset = "0x31E530", VA = "0x18031FB30")]
    	[AsyncStateMachine(typeof(_003CModifyAsync_003Ed__11))]
    	private async Naninovel.UniTask ModifyAsync(AsyncToken token)
    	{
    		float duration = Assigned(Duration) ? Duration.Value : 0.35f;
    		Ease ease = Ease.Linear;
    		if (Assigned(EaseName) && !FastEnum.TryParse<Ease>((string)EaseName, true, out ease))
    		{
    			Warn(string.Format("Failed to parse `{0}` easing.", EaseName));
    		}
    		ModifyCameraHeightTween heightTween = Assigned(Height) ? new ModifyCameraHeightTween(Height, duration, ease) : null;
    		ModifyCameraRotationTween rotationTween = Assigned(Rotation) ? new ModifyCameraRotationTween(Rotation, duration, ease) : null;
    		ModifyCameraDistanceTween distanceTween = Assigned(Distance) ? new ModifyCameraDistanceTween(Distance, duration, ease) : null;
    		await CameraModifier.ModifyCameraAsync(Index, heightTween, rotationTween, distanceTween, token.CancellationToken);
    	}

    	[Token(Token = "0x60000FB")]
    	[Address(RVA = "0x318720", Offset = "0x317120", VA = "0x180318720")]
    	public ModifyPerspectiveStageCamera()
    	{
    	}
    }
}
