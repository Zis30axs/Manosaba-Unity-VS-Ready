using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x20000B8")]
[ActorResources(typeof(Texture2D), true)]
public class SpriteBackground : SpriteActor<BackgroundMetadata>, IBackgroundActor, IActor
{
	[StructLayout(3)]
	[Token(Token = "0x20000B9")]
	[CompilerGenerated]
	private struct _003CInitialize_003Ed__2
{
		[Token(Token = "0x40002BE")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40002BF")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40002C0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public SpriteBackground _003C_003E4__this;

		[Token(Token = "0x40002C1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600037C")]
		[Address(RVA = "0x46CEE0", Offset = "0x46B8E0", VA = "0x18046CEE0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600037D")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40002BD")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xB0")]
	private BackgroundMatcher matcher;

	[Token(Token = "0x6000378")]
	[Address(RVA = "0x46C8C0", Offset = "0x46B2C0", VA = "0x18046C8C0")]
	public SpriteBackground(string id = default, BackgroundMetadata meta = default, StandaloneAppearanceLoader<Texture2D> loader = default)
	{
	}

	[Token(Token = "0x6000379")]
	[Address(RVA = "0x313D30", Offset = "0x312730", VA = "0x180313D30", Slot = "41")]
	[AsyncStateMachine(typeof(_003CInitialize_003Ed__2))]
	public override UniTask Initialize()
	{
		return default(UniTask);
	}

	[Token(Token = "0x600037A")]
	[Address(RVA = "0x46C940", Offset = "0x46B340", VA = "0x18046C940", Slot = "48")]
	public override void Dispose()
	{
	}
}
