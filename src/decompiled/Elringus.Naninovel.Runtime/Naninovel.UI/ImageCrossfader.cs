using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

namespace Naninovel.UI;

[Token(Token = "0x2000524")]
public class ImageCrossfader : IDisposable
{
	[StructLayout(3)]
	[Token(Token = "0x2000525")]
	[CompilerGenerated]
	private struct _003CCrossfade_003Ed__17
{
		[Token(Token = "0x40013F0")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x40013F1")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x40013F2")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public ImageCrossfader _003C_003E4__this;

		[Token(Token = "0x40013F3")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public Texture texture;

		[Token(Token = "0x40013F4")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public float duration;

		[Token(Token = "0x40013F5")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600199B")]
		[Address(RVA = "0x61C370", Offset = "0x61AD70", VA = "0x18061C370", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600199C")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x40013EA")]
	private const string shaderName = "Naninovel/TransitionalUI";

	[Token(Token = "0x40013EB")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private static readonly int transitionTexId;

	[Token(Token = "0x40013EC")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x4")]
	private static readonly int transitionProgressId;

	[Token(Token = "0x40013ED")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	private readonly RawImage image;

	[Token(Token = "0x40013EE")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
	private readonly Material material;

	[Token(Token = "0x40013EF")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	private readonly Tweener<FloatTween> tweener;

	[Token(Token = "0x170003AC")]
	private Texture mainTexture
	{
		[Token(Token = "0x6001990")]
		[Address(RVA = "0x61BBE0", Offset = "0x61A5E0", VA = "0x18061BBE0")]
		get
		{
			return null;
		}
		[Token(Token = "0x6001991")]
		[Address(RVA = "0x61BC00", Offset = "0x61A600", VA = "0x18061BC00")]
		set
		{
		}
	}

	[Token(Token = "0x170003AD")]
	private Texture transitionTexture
	{
		[Token(Token = "0x6001992")]
		[Address(RVA = "0x61BCF0", Offset = "0x61A6F0", VA = "0x18061BCF0")]
		get
		{
			return null;
		}
		[Token(Token = "0x6001993")]
		[Address(RVA = "0x61BD70", Offset = "0x61A770", VA = "0x18061BD70")]
		set
		{
		}
	}

	[Token(Token = "0x170003AE")]
	private float transitionProgress
	{
		[Token(Token = "0x6001994")]
		[Address(RVA = "0x61BDF0", Offset = "0x61A7F0", VA = "0x18061BDF0")]
		get
		{
			return default(float);
		}
		[Token(Token = "0x6001995")]
		[Address(RVA = "0x61BE70", Offset = "0x61A870", VA = "0x18061BE70")]
		set
		{
		}
	}

	[Token(Token = "0x6001996")]
	[Address(RVA = "0x61BF00", Offset = "0x61A900", VA = "0x18061BF00")]
	public ImageCrossfader(RawImage image)
	{
	}

	[Token(Token = "0x6001997")]
	[Address(RVA = "0x61C130", Offset = "0x61AB30", VA = "0x18061C130", Slot = "4")]
	public void Dispose()
	{
	}

	[Token(Token = "0x6001998")]
	[Address(RVA = "0x61C140", Offset = "0x61AB40", VA = "0x18061C140")]
	[AsyncStateMachine(typeof(_003CCrossfade_003Ed__17))]
	public UniTask Crossfade(Texture texture, float duration)
	{
		return default(UniTask);
	}
}
