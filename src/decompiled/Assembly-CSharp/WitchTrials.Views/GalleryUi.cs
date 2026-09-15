using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.UI;
using UnityEngine;

namespace WitchTrials.Views;

[Token(Token = "0x20000C0")]
public sealed class GalleryUi : CGGalleryPanel, IGalleryUi, IManagedUI
{
	[StructLayout(3)]
	[Token(Token = "0x20000C1")]
	[CompilerGenerated]
	private struct _003CLocateAsync_003Ed__16
{
		[Token(Token = "0x4000367")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000368")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<IReadOnlyCollection<string>> _003C_003Et__builder;

		[Token(Token = "0x4000369")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public ResourceLoaderConfiguration config;

		[Token(Token = "0x400036A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		public GalleryUi _003C_003E4__this;

		[Token(Token = "0x400036B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private UniTask<IReadOnlyCollection<string>>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60002C3")]
		[Address(RVA = "0x350850", Offset = "0x34F250", VA = "0x180350850", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60002C4")]
		[Address(RVA = "0x350C90", Offset = "0x34F690", VA = "0x180350C90", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x20000C2")]
	[CompilerGenerated]
	private struct _003CUnlockAllStillsAsync_003Ed__15
{
		[Token(Token = "0x400036C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x400036D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

		[Token(Token = "0x400036E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		public GalleryUi _003C_003E4__this;

		[Token(Token = "0x400036F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private UniTask<IReadOnlyCollection<string>[]>.Awaiter _003C_003Eu__1;

		[Token(Token = "0x60002C5")]
		[Address(RVA = "0x350CC0", Offset = "0x34F6C0", VA = "0x180350CC0", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x60002C6")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000362")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x118")]
	[SerializeField]
	private string[] _cgSourcePaths;

	[Token(Token = "0x4000363")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x120")]
	private IResourceProviderManager _naniResourceProviderManager;

	[Token(Token = "0x4000364")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x128")]
	private ILocalizationManager _naniLocalizationManager;

	[Token(Token = "0x4000365")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x130")]
	private IUnlockableManager _naniUnlockableManager;

	[Token(Token = "0x4000366")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x138")]
	private IStateManager _naniStateManager;

	[Token(Token = "0x17000050")]
	private IResourceProviderManager NaniResourceProviderManager
	{
		[Token(Token = "0x60002BA")]
		[Address(RVA = "0x34FF60", Offset = "0x34E960", VA = "0x18034FF60")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000051")]
	private ILocalizationManager NaniLocalizationManager
	{
		[Token(Token = "0x60002BB")]
		[Address(RVA = "0x350030", Offset = "0x34EA30", VA = "0x180350030")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000052")]
	private IUnlockableManager NaniUnlockableManager
	{
		[Token(Token = "0x60002BC")]
		[Address(RVA = "0x350100", Offset = "0x34EB00", VA = "0x180350100")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000053")]
	private IStateManager NaniStateManager
	{
		[Token(Token = "0x60002BD")]
		[Address(RVA = "0x3501D0", Offset = "0x34EBD0", VA = "0x1803501D0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60002BE")]
	[Address(RVA = "0x3502A0", Offset = "0x34ECA0", VA = "0x1803502A0")]
	public void UpdateCgSourcePaths()
	{
	}

	// RECONSTRUCTED | Confidence: A | RVA: 0x350380, 430 B
	// 
	// The icall `Application::get_isPlaying` (data 0x25EE758) guards a builder Start of
	// <UnlockAllStillsAsync>d__15, then `get_Task` and a tail jump to Naninovel `UniTaskExtensions.ForgetCore`.
	// The same body is inlined byte-for-byte into DebugCommandButtons' unlock-all-stills button lambda.
	[Token(Token = "0x60002BF")]
	[Address(RVA = "0x350380", Offset = "0x34ED80", VA = "0x180350380")]
	public void UnlockAllStills()
	{
		if (Application.isPlaying)
		{
			UnlockAllStillsAsync().Forget();
		}
	}

	[Token(Token = "0x60002C0")]
	[Address(RVA = "0x350530", Offset = "0x34EF30", VA = "0x180350530")]
	[AsyncStateMachine(typeof(_003CUnlockAllStillsAsync_003Ed__15))]
	private UniTask UnlockAllStillsAsync()
	{
		return default(UniTask);
	}

	[Token(Token = "0x60002C1")]
	[Address(RVA = "0x350650", Offset = "0x34F050", VA = "0x180350650")]
	[AsyncStateMachine(typeof(_003CLocateAsync_003Ed__16))]
	private UniTask<IReadOnlyCollection<string>> LocateAsync(ResourceLoaderConfiguration config)
	{
		return default(UniTask<IReadOnlyCollection<string>>);
	}

	[Token(Token = "0x60002C2")]
	[Address(RVA = "0x350840", Offset = "0x34F240", VA = "0x180350840")]
	public GalleryUi()
	{
	}
}
