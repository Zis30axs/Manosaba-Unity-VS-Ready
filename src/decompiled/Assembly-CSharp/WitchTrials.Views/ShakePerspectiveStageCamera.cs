using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using GigaCreation.Tools.Service;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using UnityEngine;
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

	// RECONSTRUCTED | Confidence: A | ctor RVA: 0x320710, 661 B
	// Field initializers, in the constructor's store order before `Command..ctor`:
	// - +0x38 `Force`: a `List<float?>` built with three `Add` calls holding
	//   1f (`0x3f800000`), 0f and 0f, converted through the `List<float?>`
	//   implicit operator (0x5A6370).
	// - +0x40 `Frequency`: `IntegerParameter.op_Implicit(10)`.
	// - +0x48 `DampingRatio`: `DecimalParameter.op_Implicit(1f)` (rdata 0x2AE2820).
	// These match the `[ParameterDefaultValue]` strings.
	[Token(Token = "0x40000F8")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterDefaultValue("1,0,0")]
	[VectorContext("X,Y,Z", -1, null)]
	public DecimalListParameter Force = new List<float?> { 1f, 0f, 0f };

	[Token(Token = "0x40000F9")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterDefaultValue("10")]
	public IntegerParameter Frequency = 10;

	[Token(Token = "0x40000FA")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	[UsedImplicitly(ImplicitUseKindFlags.Assign)]
	[ParameterAlias("damp")]
	[ParameterDefaultValue("1")]
	public DecimalParameter DampingRatio = 1f;

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
		// RECONSTRUCTED | Confidence: A | RVA: 0x320420, 186 B
		// Lazy cache in +0x68 via `ServiceLocator.Get<IPerspectiveStageManager>(true)`.
		[Token(Token = "0x60000FE")]
		[Address(RVA = "0x320420", Offset = "0x31EE20", VA = "0x180320420")]
		get
		{
			return _cameraModifier ??= ServiceLocator.Get<IPerspectiveStageManager>(true);
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x3204E0, 171 B
	// `WaitOrForget` (vtable +0x208, slot 13) over `ShakeAsync` with `Wait` (+0x60).
	[Token(Token = "0x60000FF")]
	[Address(RVA = "0x3204E0", Offset = "0x31EEE0", VA = "0x1803204E0", Slot = "5")]
	public override Naninovel.UniTask Execute([Optional] AsyncToken token)
	{
		return WaitOrForget(ShakeAsync, Wait, token);
	}

	// RECONSTRUCTED | Confidence: A (behavior) | RVA: 0x320590, 383 B (entry) + MoveNext 0x320B10
	// - Early exits straight to SetResult: `Force` not assigned; its `float?[]`
	//   implicit is null; or every component satisfies the cached static lambda
	//   `<>c.<ShakeAsync>b__11_0` (0x320A50) = `!f.HasValue ||
	//   Mathf.Approximately(f.Value, 0f)` (tail jump into the `Approximately`
	//   body 0x331670 with xmm1 = 0).
	// - Power: component 0 is `HasValue ? GetValueOrDefault() : 1f` (the `?? 1f`
	//   shape). Components 1 and 2 are plain `GetValueOrDefault()` (`?? 0f`,
	//   which Roslyn emits the same way); all three come via `ElementAtOrDefault`.
	// - Defaults when unassigned: duration 0.35f (0x2AE26D0), frequency 10
	//   (`mov r12d, 0xA`), damping 1f.
	// - The token passed on is `token.CreateLinkedTokenSource().Token`
	//   (Naninovel `AsyncToken.CreateLinkedTokenSource()`, 0x510DE0). The source
	//   itself is never stored or disposed.
	// - The single await is `sub_3B50` = IPerspectiveStageCameraModifier slot 1
	//   (`mov r8d, 1`) `ShakeCameraAsync(Index, power, duration, frequency,
	//   dampingRatio, Randomly, ct)`; the getter is inlined, and `Index` and
	//   `Randomly` are converted in argument order.
	// The `force == null` spelling and the `?? 0f` vs `GetValueOrDefault()`
	// spelling are Confidence B.
	[Token(Token = "0x6000100")]
	[Address(RVA = "0x320590", Offset = "0x31EF90", VA = "0x180320590")]
	[AsyncStateMachine(typeof(_003CShakeAsync_003Ed__11))]
	private async Naninovel.UniTask ShakeAsync(AsyncToken token)
	{
		if (!Assigned(Force))
		{
			return;
		}
		float?[] force = Force;
		if (force == null || force.All((float? f) => !f.HasValue || Mathf.Approximately(f.Value, 0f)))
		{
			return;
		}
		Vector3 power = new Vector3(force.ElementAtOrDefault(0) ?? 1f, force.ElementAtOrDefault(1) ?? 0f, force.ElementAtOrDefault(2) ?? 0f);
		float duration = Assigned(Duration) ? Duration.Value : 0.35f;
		int frequency = Assigned(Frequency) ? Frequency.Value : 10;
		float dampingRatio = Assigned(DampingRatio) ? DampingRatio.Value : 1f;
		CancellationToken ct = token.CreateLinkedTokenSource().Token;
		await CameraModifier.ShakeCameraAsync(Index, power, duration, frequency, dampingRatio, Randomly, ct);
	}

	[Token(Token = "0x6000101")]
	[Address(RVA = "0x320710", Offset = "0x31F110", VA = "0x180320710")]
	public ShakePerspectiveStageCamera()
	{
	}
}
