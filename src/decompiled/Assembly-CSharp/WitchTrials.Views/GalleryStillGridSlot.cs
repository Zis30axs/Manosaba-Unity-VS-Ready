using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
		// RECONSTRUCTED | Confidence: A | RVA: 0x34DB60, 8 B (`mov rax,[rcx+0x118]; ret`, shared body)
		[Token(Token = "0x60002A3")]
		[Address(RVA = "0x34DB60", Offset = "0x34C560", VA = "0x18034DB60", Slot = "80")]
		get
		{
			return _cgTextures;
		}
	}

	[Token(Token = "0x1700004C")]
	private IUnlockableManager NaniUnlockableManager
	{
		// RECONSTRUCTED | Confidence: A | RVA: 0x34DB70, 202 B (`??= Engine.GetServiceOrErr<T>()`, +0x108)
		[Token(Token = "0x60002A4")]
		[Address(RVA = "0x34DB70", Offset = "0x34C570", VA = "0x18034DB70")]
		get
		{
			return _naniUnlockableManager ??= Engine.GetServiceOrErr<IUnlockableManager>();
		}
	}

	[Token(Token = "0x1700004D")]
	private RectTransform ThumbnailRectTransform
	{
		// RECONSTRUCTED | Confidence: A (behaviour), B (if/return spelling) | RVA: 0x34DC40, 388 B
		// Unity-object null test on the cache (+0x110), otherwise CGGalleryGridSlot slot 77 `ThumbnailImage` -> transform
		// with an exact-class RectTransform check (`as`), stored and returned.
		[Token(Token = "0x60002A5")]
		[Address(RVA = "0x34DC40", Offset = "0x34C640", VA = "0x18034DC40")]
		get
		{
			if (_thumbnailRectTransform != null)
			{
				return _thumbnailRectTransform;
			}
			return _thumbnailRectTransform = ThumbnailImage.transform as RectTransform;
		}
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x34DDD0 (builder stub); MoveNext 0x34F090, 1498 B
	// 
	// - `<prevThumbnailImage>5__2` saves ThumbnailImage (slot 77) `.texture` (RawImage +0xE0, getter inlined); the
	//   loading placeholder (slot 79 `LoadingTexture`) is shown while loading and the previous texture is restored after.
	// - Data (slot 76, the CGSlotData struct) is read twice: once for the source and once as the target of a
	//   Func<string, int> bound to Naninovel `LinqUtils.IndexOf<string>`, i.e. the method group `TexturePaths.IndexOf`.
	// - Each path maps to GetOrLoadUnlockedStillTextureAsync; the array is awaited through Naninovel
	//   `UniTaskExtensions.GetAwaiter<Texture2D>` (all at once) and stored in `_cgTextures` (+0x118).
	[Token(Token = "0x60002A6")]
	[Address(RVA = "0x34DDD0", Offset = "0x34C7D0", VA = "0x18034DDD0", Slot = "84")]
	[AsyncStateMachine(typeof(_003CLoadCGTextures_003Ed__9))]
	protected override async UniTask LoadCGTextures()
	{
		Texture prevThumbnailImage = ThumbnailImage.texture;
		ThumbnailImage.texture = LoadingTexture;
		_cgTextures = await Data.TexturePaths.OrderBy(Data.TexturePaths.IndexOf).Select(GetOrLoadUnlockedStillTextureAsync).ToArray();
		ThumbnailImage.texture = prevThumbnailImage;
	}

	// RECONSTRUCTED | Confidence: A (behaviour), B (loop spelling) | RVA: 0x34DEF0 (AsyncVoidMethodBuilder stub);
	// MoveNext 0x34E870, 2051 B; <>c lambda b__10_0 0x34E2C0
	// 
	// - Until the slot is bound (slot 67 `Id` non-null) it waits one frame (Naninovel AsyncUtils.DelayFrame(1)) and
	//   gives up if the component was destroyed (inlined UnityEngine.Object inequality on `this`).
	// - Then LoadCGTextures (slot 84). The thumbnail is the first non-null texture of CGTextures (slot 80) when
	//   AnyUnlocked (slot 81), otherwise LockedTexture (slot 78); `(bool)texture` is the op_Implicit call.
	// - The thumbnail is resized to the texture's aspect ratio keeping the current height (Texture vtable +0x1A8
	//   height, +0x188 width).
	[Token(Token = "0x60002A7")]
	[Address(RVA = "0x34DEF0", Offset = "0x34C8F0", VA = "0x18034DEF0", Slot = "87")]
	[AsyncStateMachine(typeof(_003CHandleItemUpdated_003Ed__10))]
	protected override async void HandleItemUpdated(UnlockableItemUpdatedArgs _)
	{
		while (Id == null)
		{
			await AsyncUtils.DelayFrame(1);
			if (this == null)
			{
				return;
			}
		}
		await LoadCGTextures();
		Texture2D thumbnail = AnyUnlocked ? CGTextures.FirstOrDefault((Texture2D texture) => texture != null) : LockedTexture;
		if ((bool)thumbnail)
		{
			float scale = ThumbnailRectTransform.sizeDelta.y / (float)thumbnail.height;
			RectTransform thumbnailRectTransform = ThumbnailRectTransform;
			float width = (float)thumbnail.width * scale;
			thumbnailRectTransform.sizeDelta = new Vector2(width, thumbnailRectTransform.sizeDelta.y);
		}
		ThumbnailImage.texture = thumbnail;
	}

	// RECONSTRUCTED | Confidence: A (behaviour), B (the shared conversion written as a conditional) |
	// RVA: 0x34E020 (builder stub); MoveNext 0x34E390, 1195 B
	// 
	// - Locked stills resolve to null: IUnlockableManager slot 3 `ItemUnlocked(path)`.
	// - IResourceLoader slot 5 `IsLoaded` on Data.TextureLoader (+0x10); loaded -> IResourceLoader<T> `GetLoaded`
	//   (thunk sub_5960), otherwise `await ResourceLoaderExtensions.LoadOrErr(loader, path, this)`. Both results
	//   go through the same inlined Resource<Texture2D> -> Texture2D conversion.
	[Token(Token = "0x60002A8")]
	[Address(RVA = "0x34E020", Offset = "0x34CA20", VA = "0x18034E020")]
	[AsyncStateMachine(typeof(_003CGetOrLoadUnlockedStillTextureAsync_003Ed__11))]
	private async UniTask<Texture2D> GetOrLoadUnlockedStillTextureAsync(string path)
	{
		if (!NaniUnlockableManager.ItemUnlocked(path))
		{
			return null;
		}
		return Data.TextureLoader.IsLoaded(path) ? Data.TextureLoader.GetLoaded(path) : (await Data.TextureLoader.LoadOrErr(path, this));
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x34E210, 7 B (tail jump to the Naninovel ScriptableGridSlot ctor; no initializers)
	[Token(Token = "0x60002A9")]
	[Address(RVA = "0x34E210", Offset = "0x34CC10", VA = "0x18034E210")]
	public GalleryStillGridSlot()
	{
	}
}
