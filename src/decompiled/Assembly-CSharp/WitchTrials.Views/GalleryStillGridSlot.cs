using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace WitchTrials.Views;

[Token(Token = "0x20000BA")]
public sealed class GalleryStillGridSlot : CGGalleryGridSlot
{
	[StructLayout(3)]
	[Token(Token = "0x20000BC")]
	[CompilerGenerated]
	private struct _003CGetOrLoadUnlockedStillTextureAsync_003Ed__11
{
		[Token(Token = "0x4000351")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000352")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<Texture2D> _003C_003Et__builder;

		[Token(Token = "0x4000353")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public GalleryStillGridSlot _003C_003E4__this;

		[Token(Token = "0x4000354")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public string path;

		[Token(Token = "0x4000355")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask<Resource<Texture2D>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60002AD")]
		[Address(RVA = "0x34E390", Offset = "0x34CD90", VA = "0x18034E390", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60002AE")]
		[Address(RVA = "0x34E840", Offset = "0x34D240", VA = "0x18034E840", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000BD")]
	[CompilerGenerated]
	private struct _003CHandleItemUpdated_003Ed__10
{
		[Token(Token = "0x4000356")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000357")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncVoidMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x4000358")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public GalleryStillGridSlot _003C_003E4__this;

		[Token(Token = "0x4000359")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60002AF")]
		[Address(RVA = "0x34E870", Offset = "0x34D270", VA = "0x18034E870", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60002B0")]
		[Address(RVA = "0x34F080", Offset = "0x34DA80", VA = "0x18034F080", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000BE")]
	[CompilerGenerated]
	private struct _003CLoadCGTextures_003Ed__9
{
		[Token(Token = "0x400035A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400035B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400035C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public GalleryStillGridSlot _003C_003E4__this;

		[Token(Token = "0x400035D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private Texture _003CprevThumbnailImage_003E5__2;

		[Token(Token = "0x400035E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask<Texture2D[]>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60002B1")]
		[Address(RVA = "0x34F090", Offset = "0x34DA90", VA = "0x18034F090", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60002B2")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x400034C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x108")]
	private IUnlockableManager _naniUnlockableManager;

	[Token(Token = "0x400034D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x110")]
	private RectTransform _thumbnailRectTransform;

	[Token(Token = "0x400034E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x118")]
	private IReadOnlyList<Texture2D> _cgTextures;

	[Token(Token = "0x1700004B")]
	protected override IReadOnlyList<Texture2D> CGTextures
	{
		[Token(Token = "0x60002A3")]
		[Address(RVA = "0x34DB60", Offset = "0x34C560", VA = "0x18034DB60", Slot = "80")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700004C")]
	private IUnlockableManager NaniUnlockableManager
	{
		[Token(Token = "0x60002A4")]
		[Address(RVA = "0x34DB70", Offset = "0x34C570", VA = "0x18034DB70")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x1700004D")]
	private RectTransform ThumbnailRectTransform
	{
		[Token(Token = "0x60002A5")]
		[Address(RVA = "0x34DC40", Offset = "0x34C640", VA = "0x18034DC40")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60002A6")]
	[Address(RVA = "0x34DDD0", Offset = "0x34C7D0", VA = "0x18034DDD0", Slot = "84")]
	[AsyncStateMachine(typeof(_003CLoadCGTextures_003Ed__9))]
	protected override UniTask LoadCGTextures()
	{
		return default(UniTask);
	}

	[Token(Token = "0x60002A7")]
	[Address(RVA = "0x34DEF0", Offset = "0x34C8F0", VA = "0x18034DEF0", Slot = "87")]
	[AsyncStateMachine(typeof(_003CHandleItemUpdated_003Ed__10))]
	protected override void HandleItemUpdated(UnlockableItemUpdatedArgs _)
	{
	}

	[Token(Token = "0x60002A8")]
	[Address(RVA = "0x34E020", Offset = "0x34CA20", VA = "0x18034E020")]
	[AsyncStateMachine(typeof(_003CGetOrLoadUnlockedStillTextureAsync_003Ed__11))]
	private UniTask<Texture2D> GetOrLoadUnlockedStillTextureAsync(string path)
	{
		return default(UniTask<Texture2D>);
	}

	[Token(Token = "0x60002A9")]
	[Address(RVA = "0x34E210", Offset = "0x34CC10", VA = "0x18034E210")]
	public GalleryStillGridSlot()
	{
	}
}
