using Naninovel.Commands;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GigaCreation.NaninovelExtender.ExtendedActors;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;
using System;

namespace WitchTrials.Views;

[Token(Token = "0x2000029")]
[UsedImplicitly]
public class EmoteFall : AnimateActorExtended
{
	[StructLayout(3)]
	[Token(Token = "0x200002A")]
	[CompilerGenerated]
	private struct _003CExecute_003Ed__5
{
		[Token(Token = "0x400006C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400006D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400006E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public EmoteFall _003C_003E4__this;

		[Token(Token = "0x400006F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public AsyncToken token;

		[Token(Token = "0x4000070")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask _003CbaseTask_003E5__2;

		[Token(Token = "0x4000071")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600009A")]
		[Address(RVA = "0x314E00", Offset = "0x313800", VA = "0x180314E00", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600009B")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000067")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xC8")]
	private readonly float[] _scenePositionX = new float[4] { -6f, 0f, 3f, 0f };

	[Token(Token = "0x4000068")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xD0")]
	private readonly float[] _scenePositionY = new float[4] { -2f, -2f, -3f, -50f };

	[Token(Token = "0x4000069")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xD8")]
	private readonly float[] _rotation = new float[4] { 6f, 0f, -3f, 3f };

	[Token(Token = "0x400006A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE0")]
	private readonly float[] _duration = new float[4] { 0.3f, 0.3f, 0.3f, 0.5f };

	[Token(Token = "0x400006B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
	private readonly EasingType[] _easing = new EasingType[4]
	{
		EasingType.EaseInOutCubic,
		EasingType.EaseInCubic,
		EasingType.EaseOutQuad,
		EasingType.EaseInCubic
	};

	// RECONSTRUCTED | Confidence: A | RVA: 0x3142E0 (builder stub); MoveNext 0x314E00, 2985 B, three await states;
	// <>n__0 0x314C70 (= AnimateActor.Execute, the `base.Execute` helper)
	// 
	// - The animation starts first and is kept in `<baseTask>5__2` (+0x30); it is awaited last.
	// - Naninovel UniTask.Delay(TimeSpan, ignoreTimeScale: false, PlayerLoopTiming 8 = Update, token.CancellationToken)
	//   with 1.15 s (double at data RVA 0x2AE2910) and 0.15 s (0x2AE2898).
	// - HideActors: its ctor is inlined (`Lazy = false` field initializer, +0x40), then ActorIds (+0x30) is copied from
	//   this command; Execute is the virtual slot 5 call, fire-and-forget.
	// - IAudioManager.PlaySfx("Body_Fall", 1f, 0f, false, null, token), fire-and-forget.
	[Token(Token = "0x6000096")]
	[Address(RVA = "0x3142E0", Offset = "0x312CE0", VA = "0x1803142E0", Slot = "5")]
	[AsyncStateMachine(typeof(_003CExecute_003Ed__5))]
	public override async UniTask Execute([Optional] AsyncToken token)
	{
		UniTask baseTask = base.Execute(token);
		await UniTask.Delay(TimeSpan.FromSeconds(1.15), ignoreTimeScale: false, PlayerLoopTiming.Update, token.CancellationToken);
		new HideActors
		{
			ActorIds = ActorIds
		}.Execute(token).Forget();
		await UniTask.Delay(TimeSpan.FromSeconds(0.15), ignoreTimeScale: false, PlayerLoopTiming.Update, token.CancellationToken);
		Engine.GetServiceOrErr<IAudioManager>().PlaySfx("Body_Fall", 1f, 0f, false, null, token).Forget();
		await baseTask;
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x314460, 1223 B
	// Each `string.Join<T>(char, IEnumerable<T>)` uses AnimateActor.KeyDelimiters[0] (static char[], element 0) and goes through the StringParameter implicit conversion into the AnimateActor field: ScenePositionX +0x58, ScenePositionY +0x60, Rotation +0x70, EasingTypeName +0x88, Duration +0x90; AnimateActorExtended.Relative (+0xB0) = true. The tail jump is AnimateActorExtended.GetParametersForActor (0xD1E940).
	[Token(Token = "0x6000097")]
	[Address(RVA = "0x314460", Offset = "0x312E60", VA = "0x180314460", Slot = "17")]
	protected override IReadOnlyList<string> GetParametersForActor(string actorId)
	{
		ScenePositionX = string.Join(KeyDelimiters[0], _scenePositionX);
		ScenePositionY = string.Join(KeyDelimiters[0], _scenePositionY);
		Rotation = string.Join(KeyDelimiters[0], _rotation);
		Duration = string.Join(KeyDelimiters[0], _duration);
		EasingTypeName = string.Join(KeyDelimiters[0], _easing);
		Relative = true;
		return base.GetParametersForActor(actorId);
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x314930, 826 B (field initializers only, then AnimateActorExtended..ctor)
	// _scenePositionX is written element by element (-6 at [0], 3 at [2]); the other arrays come from RuntimeHelpers.
	// InitializeArray blobs read from global-metadata.dat v31: Y 0x854A50, rotation 0x85B178, duration 0x854A20, easing 0x8549A8.
	[Token(Token = "0x6000098")]
	[Address(RVA = "0x314930", Offset = "0x313330", VA = "0x180314930")]
	public EmoteFall()
	{
	}
}
