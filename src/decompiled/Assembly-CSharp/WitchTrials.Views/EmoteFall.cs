using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using GigaCreation.NaninovelExtender.ExtendedActors;
using Il2CppDummyDll;
using JetBrains.Annotations;
using Naninovel;
using Naninovel.Async.CompilerServices;

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
	private readonly float[] _scenePositionX;

	[Token(Token = "0x4000068")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xD0")]
	private readonly float[] _scenePositionY;

	[Token(Token = "0x4000069")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xD8")]
	private readonly float[] _rotation;

	[Token(Token = "0x400006A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE0")]
	private readonly float[] _duration;

	[Token(Token = "0x400006B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
	private readonly EasingType[] _easing;

	[Token(Token = "0x6000096")]
	[Address(RVA = "0x3142E0", Offset = "0x312CE0", VA = "0x1803142E0", Slot = "5")]
	[AsyncStateMachine(typeof(_003CExecute_003Ed__5))]
	public override UniTask Execute([Optional] AsyncToken token)
	{
		return default(UniTask);
	}

	[Token(Token = "0x6000097")]
	[Address(RVA = "0x314460", Offset = "0x312E60", VA = "0x180314460", Slot = "17")]
	protected override IReadOnlyList<string> GetParametersForActor(string actorId)
	{
		return null;
	}

	[Token(Token = "0x6000098")]
	[Address(RVA = "0x314930", Offset = "0x313330", VA = "0x180314930")]
	public EmoteFall()
	{
	}
}
