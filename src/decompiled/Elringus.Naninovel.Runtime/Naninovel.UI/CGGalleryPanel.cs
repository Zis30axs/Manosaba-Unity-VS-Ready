using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel.UI;

[Token(Token = "0x2000500")]
[RequireComponent(typeof(CanvasGroup))]
public class CGGalleryPanel : CustomUI, ICGGalleryUI, IManagedUI
{
	[Token(Token = "0x2000502")]
	[CompilerGenerated]
	private sealed class _003C_003Ec__DisplayClass20_0
	{
		[Token(Token = "0x4001382")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		public CGGalleryPanel _003C_003E4__this;

		[Token(Token = "0x4001383")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public List<CGSlotData> slotData;

		[Token(Token = "0x6001905")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public _003C_003Ec__DisplayClass20_0()
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000505")]
	[CompilerGenerated]
	private struct _003CInitialize_003Ed__20
{
		[Token(Token = "0x400138B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400138C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400138D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public CGGalleryPanel _003C_003E4__this;

		[Token(Token = "0x400138E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private _003C_003Ec__DisplayClass20_0 _003C_003E8__1;

		[Token(Token = "0x400138F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private UniTask.Awaiter _003C_003Eu__1;

		[Token(Token = "0x600190C")]
		[Address(RVA = "0x613A50", Offset = "0x612450", VA = "0x180613A50", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x600190D")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4001377")]
	public const string CGPrefix = "CG";

	[Token(Token = "0x400137A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF0")]
	[Tooltip("The specified resource loaders will be used to retrieve the available CG slots and associated textures.")]
	[SerializeField]
	private ResourceLoaderConfiguration[] cgSources;

	[Token(Token = "0x400137B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0xF8")]
	[Tooltip("Used to view selected CG slots.")]
	[SerializeField]
	private CGViewerPanel viewerPanel;

	[Token(Token = "0x400137C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x100")]
	[Tooltip("Used to host and navigate selectable CG preview thumbnails.")]
	[SerializeField]
	private CGGalleryGrid grid;

	[Token(Token = "0x400137D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x108")]
	private IResourceProviderManager resources;

	[Token(Token = "0x400137E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x110")]
	private ILocalizationManager l10n;

	[Token(Token = "0x1700038F")]
	public virtual int CGCount
	{
		[Token(Token = "0x60018F3")]
		[Address(RVA = "0x4A37B0", Offset = "0x4A21B0", VA = "0x1804A37B0", Slot = "96")]
		[CompilerGenerated]
		get
		{
			return default(int);
		}
		[Token(Token = "0x60018F4")]
		[Address(RVA = "0x4A37C0", Offset = "0x4A21C0", VA = "0x1804A37C0")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x17000390")]
	protected virtual ResourceLoaderConfiguration[] CGSources
	{
		[Token(Token = "0x60018F5")]
		[Address(RVA = "0x49D2B0", Offset = "0x49BCB0", VA = "0x18049D2B0", Slot = "97")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000391")]
	protected virtual CGViewerPanel ViewerPanel
	{
		[Token(Token = "0x60018F6")]
		[Address(RVA = "0x49D250", Offset = "0x49BC50", VA = "0x18049D250", Slot = "98")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000392")]
	protected virtual CGGalleryGrid Grid
	{
		[Token(Token = "0x60018F7")]
		[Address(RVA = "0x37A040", Offset = "0x378A40", VA = "0x18037A040", Slot = "99")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000393")]
	protected virtual CGGalleryGridSlot LastViewed
	{
		[Token(Token = "0x60018F8")]
		[Address(RVA = "0x49D2A0", Offset = "0x49BCA0", VA = "0x18049D2A0", Slot = "100")]
		[CompilerGenerated]
		get
		{
			return null;
		}
		[Token(Token = "0x60018F9")]
		[Address(RVA = "0x4A37E0", Offset = "0x4A21E0", VA = "0x1804A37E0", Slot = "101")]
		[CompilerGenerated]
		set
		{
		}
	}

	[Token(Token = "0x60018FA")]
	[Address(RVA = "0x6118D0", Offset = "0x6102D0", VA = "0x1806118D0", Slot = "83")]
	[AsyncStateMachine(typeof(_003CInitialize_003Ed__20))]
	public override UniTask Initialize()
	{
		return default(UniTask);
	}

	[Token(Token = "0x60018FB")]
	[Address(RVA = "0x6119F0", Offset = "0x6103F0", VA = "0x1806119F0", Slot = "4")]
	protected override void Awake()
	{
	}

	[Token(Token = "0x60018FC")]
	[Address(RVA = "0x611C30", Offset = "0x610630", VA = "0x180611C30", Slot = "102")]
	protected virtual void HandleCancelInput()
	{
	}

	[Token(Token = "0x60018FD")]
	[Address(RVA = "0x611CC0", Offset = "0x6106C0", VA = "0x180611CC0", Slot = "103")]
	protected virtual void HandlePageInput(float value)
	{
	}

	[Token(Token = "0x60018FE")]
	[Address(RVA = "0x611E00", Offset = "0x610800", VA = "0x180611E00", Slot = "92")]
	protected override GameObject FindFocusObject()
	{
		return null;
	}

	[Token(Token = "0x60018FF")]
	[Address(RVA = "0x6123C0", Offset = "0x610DC0", VA = "0x1806123C0")]
	public CGGalleryPanel()
	{
	}
}
