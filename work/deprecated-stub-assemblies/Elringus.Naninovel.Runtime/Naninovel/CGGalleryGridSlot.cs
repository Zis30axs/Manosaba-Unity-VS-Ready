using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using Naninovel.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Naninovel
{
    [Token(Token = "0x20004BA")]
    public class CGGalleryGridSlot : ScriptableGridSlot
    {
    	[Token(Token = "0x20004BC")]
    	[CompilerGenerated]
    	private sealed class _003C_003Ec__DisplayClass31_0
    	{
    		[Token(Token = "0x40011FD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public CGGalleryGridSlot _003C_003E4__this;

    		[Token(Token = "0x40011FE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public Texture2D[] textures;

    		[Token(Token = "0x60017B5")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    		public _003C_003Ec__DisplayClass31_0()
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20004BE")]
    	[CompilerGenerated]
    	private struct _003CHandleItemUpdated_003Ed__36
    {
    		[Token(Token = "0x4001206")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4001207")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncVoidMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4001208")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public CGGalleryGridSlot _003C_003E4__this;

    		[Token(Token = "0x4001209")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60017B9")]
    		[Address(RVA = "0x5F8B60", Offset = "0x5F7560", VA = "0x1805F8B60", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60017BA")]
    		[Address(RVA = "0x34F080", Offset = "0x34DA80", VA = "0x18034F080", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20004BF")]
    	[CompilerGenerated]
    	private struct _003CLoadCGTextures_003Ed__31
    {
    		[Token(Token = "0x400120A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x400120B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x400120C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public CGGalleryGridSlot _003C_003E4__this;

    		[Token(Token = "0x400120D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private _003C_003Ec__DisplayClass31_0 _003C_003E8__1;

    		[Token(Token = "0x400120E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private Texture _003CprevThumbnailImage_003E5__2;

    		[Token(Token = "0x400120F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60017BB")]
    		[Address(RVA = "0x5F9290", Offset = "0x5F7C90", VA = "0x1805F9290", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60017BC")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x40011F4")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xD8")]
    	[SerializeField]
    	private RawImage thumbnailImage;

    	[Token(Token = "0x40011F5")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xE0")]
    	[SerializeField]
    	private Texture2D lockedTexture;

    	[Token(Token = "0x40011F6")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xE8")]
    	[SerializeField]
    	private Texture2D loadingTexture;

    	[Token(Token = "0x40011F7")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xF0")]
    	private ILocalizationManager l10n;

    	[Token(Token = "0x40011F8")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0xF8")]
    	private IUnlockableManager unlockables;

    	[Token(Token = "0x40011F9")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x100")]
    	private Action<IEnumerable<Texture2D>> showTextures;

    	[Token(Token = "0x17000362")]
    	public override string Id
    	{
    		[Token(Token = "0x6001799")]
    		[Address(RVA = "0x5F7320", Offset = "0x5F5D20", VA = "0x1805F7320", Slot = "67")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000363")]
    	public virtual float LastSelectTime
    	{
    		[Token(Token = "0x600179A")]
    		[Address(RVA = "0x496540", Offset = "0x494F40", VA = "0x180496540", Slot = "75")]
    		[CompilerGenerated]
    		get
    		{
    			return default(float);
    		}
    		[Token(Token = "0x600179B")]
    		[Address(RVA = "0x5F7350", Offset = "0x5F5D50", VA = "0x1805F7350")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000364")]
    	protected virtual CGSlotData Data
    	{
    		[Token(Token = "0x600179C")]
    		[Address(RVA = "0x5F7360", Offset = "0x5F5D60", VA = "0x1805F7360", Slot = "76")]
    		[CompilerGenerated]
    		get
    		{
    			return default(CGSlotData);
    		}
    		[Token(Token = "0x600179D")]
    		[Address(RVA = "0x5F7380", Offset = "0x5F5D80", VA = "0x1805F7380")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000365")]
    	protected virtual RawImage ThumbnailImage
    	{
    		[Token(Token = "0x600179E")]
    		[Address(RVA = "0x5F73F0", Offset = "0x5F5DF0", VA = "0x1805F73F0", Slot = "77")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000366")]
    	protected virtual Texture2D LockedTexture
    	{
    		[Token(Token = "0x600179F")]
    		[Address(RVA = "0x33E0B0", Offset = "0x33CAB0", VA = "0x18033E0B0", Slot = "78")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000367")]
    	protected virtual Texture2D LoadingTexture
    	{
    		[Token(Token = "0x60017A0")]
    		[Address(RVA = "0x49D2A0", Offset = "0x49BCA0", VA = "0x18049D2A0", Slot = "79")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000368")]
    	protected virtual IReadOnlyList<Texture2D> CGTextures
    	{
    		[Token(Token = "0x60017A1")]
    		[Address(RVA = "0x5F7400", Offset = "0x5F5E00", VA = "0x1805F7400", Slot = "80")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x60017A2")]
    		[Address(RVA = "0x5F7410", Offset = "0x5F5E10", VA = "0x1805F7410")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000369")]
    	protected virtual bool AnyUnlocked
    	{
    		[Token(Token = "0x60017A3")]
    		[Address(RVA = "0x5F7470", Offset = "0x5F5E70", VA = "0x1805F7470", Slot = "81")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x60017A4")]
    	[Address(RVA = "0x37A050", Offset = "0x378A50", VA = "0x18037A050", Slot = "82")]
    	public virtual void Initialize(Action<IEnumerable<Texture2D>> showTextures)
    	{
    	}

    	[Token(Token = "0x60017A5")]
    	[Address(RVA = "0x5F7600", Offset = "0x5F6000", VA = "0x1805F7600", Slot = "83")]
    	public virtual void Bind(CGSlotData data)
    	{
    	}

    	[Token(Token = "0x60017A6")]
    	[Address(RVA = "0x5F76B0", Offset = "0x5F60B0", VA = "0x1805F76B0", Slot = "71")]
    	public override void OnSelect(BaseEventData eventData)
    	{
    	}

    	[Token(Token = "0x60017A7")]
    	[Address(RVA = "0x5F7790", Offset = "0x5F6190", VA = "0x1805F7790", Slot = "84")]
    	[AsyncStateMachine(typeof(_003CLoadCGTextures_003Ed__31))]
    	protected virtual UniTask LoadCGTextures()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x60017A8")]
    	[Address(RVA = "0x5F78B0", Offset = "0x5F62B0", VA = "0x1805F78B0", Slot = "85")]
    	public virtual void UnloadCGTextures()
    	{
    	}

    	[Token(Token = "0x60017A9")]
    	[Address(RVA = "0x5F7B40", Offset = "0x5F6540", VA = "0x1805F7B40", Slot = "86")]
    	protected virtual void Refresh()
    	{
    	}

    	[Token(Token = "0x60017AA")]
    	[Address(RVA = "0x5F7B70", Offset = "0x5F6570", VA = "0x1805F7B70", Slot = "4")]
    	protected override void Awake()
    	{
    	}

    	[Token(Token = "0x60017AB")]
    	[Address(RVA = "0x5F7EC0", Offset = "0x5F68C0", VA = "0x1805F7EC0", Slot = "8")]
    	protected override void OnDestroy()
    	{
    	}

    	[Token(Token = "0x60017AC")]
    	[Address(RVA = "0x5F7FD0", Offset = "0x5F69D0", VA = "0x1805F7FD0", Slot = "87")]
    	[AsyncStateMachine(typeof(_003CHandleItemUpdated_003Ed__36))]
    	protected virtual void HandleItemUpdated(UnlockableItemUpdatedArgs _)
    	{
    	}

    	[Token(Token = "0x60017AD")]
    	[Address(RVA = "0x5F8100", Offset = "0x5F6B00", VA = "0x1805F8100", Slot = "88")]
    	protected virtual void HandleLocaleChanged(LocaleChangedArgs _)
    	{
    	}

    	[Token(Token = "0x60017AE")]
    	[Address(RVA = "0x5F8120", Offset = "0x5F6B20", VA = "0x1805F8120", Slot = "62")]
    	protected override void OnButtonClick()
    	{
    	}

    	[Token(Token = "0x60017AF")]
    	[Address(RVA = "0x5F81A0", Offset = "0x5F6BA0", VA = "0x1805F81A0")]
    	private static string PathToUnlockableId(string path)
    	{
    		return null;
    	}

    	[Token(Token = "0x60017B0")]
    	[Address(RVA = "0x34E210", Offset = "0x34CC10", VA = "0x18034E210")]
    	public CGGalleryGridSlot()
    	{
    	}
    }
}
