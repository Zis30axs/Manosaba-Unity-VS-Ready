using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;
using UnityEngine.Video;

namespace Naninovel;

[Token(Token = "0x20000CC")]
public readonly struct VideoAppearance
{
	[StructLayout(3)]
	[Token(Token = "0x20000CE")]
	[CompilerGenerated]
	private struct _003CTweenVolume_003Ed__10
{
		[Token(Token = "0x400032D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400032E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400032F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public VideoAppearance _003C_003E4__this;

		[Token(Token = "0x4000330")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public float value;

		[Token(Token = "0x4000331")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x34")]
		public float duration;

		[Token(Token = "0x4000332")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		public AsyncToken token;

		[Token(Token = "0x4000333")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x6000400")]
		[Address(RVA = "0x476030", Offset = "0x474A30", VA = "0x180476030", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000401")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400032B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	private readonly Tweener<FloatTween> volumeTweener;

	[Token(Token = "0x170000A8")]
	public VideoPlayer Video
	{
		[Token(Token = "0x60003F9")]
		[Address(RVA = "0x456310", Offset = "0x454D10", VA = "0x180456310")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170000A9")]
	public AudioSource Audio
	{
		[Token(Token = "0x60003FA")]
		[Address(RVA = "0x457050", Offset = "0x455A50", VA = "0x180457050")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170000AA")]
	public GameObject GameObject
	{
		[Token(Token = "0x60003FB")]
		[Address(RVA = "0x475CB0", Offset = "0x4746B0", VA = "0x180475CB0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60003FC")]
	[Address(RVA = "0x475CD0", Offset = "0x4746D0", VA = "0x180475CD0")]
	public VideoAppearance(VideoPlayer video, AudioSource audio)
	{
	}

	[Token(Token = "0x60003FD")]
	[Address(RVA = "0x475E30", Offset = "0x474830", VA = "0x180475E30")]
	[AsyncStateMachine(typeof(_003CTweenVolume_003Ed__10))]
	public UniTask TweenVolume(float value, float duration, [Optional] AsyncToken token)
	{
		return default(UniTask);
	}
}
