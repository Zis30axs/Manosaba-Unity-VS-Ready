using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel;
using Naninovel.Async.CompilerServices;
using Naninovel.UI;
using UnityEngine;

namespace WitchTrials.Views
{
    [Token(Token = "0x20000C0")]
    public sealed class GalleryUi : CGGalleryPanel, IGalleryUi, IManagedUI
    {
        protected override bool IncludeCGPath(ResourceLoaderConfiguration source, string path)
        {
            // This scene plate is an epilogue transition aid, not an additional collectible CG.
            string prefix = source.PathPrefix?.TrimEnd('/');
            return !(path == "EpilogueEmpty" &&
                (prefix == "Backgrounds/Stills" || prefix == "Naninovel/Backgrounds/Stills"));
        }

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
    		// RECONSTRUCTED | Confidence: A | RVA: 0x34FF60, 202 B (`??= Engine.GetServiceOrErr<T>()`)
    		[Token(Token = "0x60002BA")]
    		[Address(RVA = "0x34FF60", Offset = "0x34E960", VA = "0x18034FF60")]
    		get
    		{
    			return _naniResourceProviderManager ??= Engine.GetServiceOrErr<IResourceProviderManager>();
    		}
    	}

    	[Token(Token = "0x17000051")]
    	private ILocalizationManager NaniLocalizationManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x350030, 202 B (`??= Engine.GetServiceOrErr<T>()`)
    		[Token(Token = "0x60002BB")]
    		[Address(RVA = "0x350030", Offset = "0x34EA30", VA = "0x180350030")]
    		get
    		{
    			return _naniLocalizationManager ??= Engine.GetServiceOrErr<ILocalizationManager>();
    		}
    	}

    	[Token(Token = "0x17000052")]
    	private IUnlockableManager NaniUnlockableManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x350100, 202 B (`??= Engine.GetServiceOrErr<T>()`)
    		[Token(Token = "0x60002BC")]
    		[Address(RVA = "0x350100", Offset = "0x34EB00", VA = "0x180350100")]
    		get
    		{
    			return _naniUnlockableManager ??= Engine.GetServiceOrErr<IUnlockableManager>();
    		}
    	}

    	[Token(Token = "0x17000053")]
    	private IStateManager NaniStateManager
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x3501D0, 202 B (`??= Engine.GetServiceOrErr<T>()`)
    		[Token(Token = "0x60002BD")]
    		[Address(RVA = "0x3501D0", Offset = "0x34EBD0", VA = "0x1803501D0")]
    		get
    		{
    			return _naniStateManager ??= Engine.GetServiceOrErr<IStateManager>();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3502A0, 219 B
    	// CGSources is CGGalleryPanel slot 97; ResourceLoaderConfiguration.PathPrefix is +0x10. Extra paths are ignored.
    	[Token(Token = "0x60002BE")]
    	[Address(RVA = "0x3502A0", Offset = "0x34ECA0", VA = "0x1803502A0")]
    	public void UpdateCgSourcePaths()
    	{
    		for (int i = 0; i < _cgSourcePaths.Length; i++)
    		{
    			if (i < CGSources.Length)
    			{
    				CGSources[i].PathPrefix = _cgSourcePaths[i];
    			}
    		}
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

    	// RECONSTRUCTED | Confidence: A | RVA: 0x350530 (builder stub); MoveNext 0x350CC0, 1555 B
    	// 
    	// Every CG source is located in parallel (Naninovel GetAwaiter over IEnumerable<UniTask<T>>), each located path is
    	// unlocked (IUnlockableManager slot 5 `UnlockItem`), then IStateManager slot 32 `SaveGlobal()` is fire-and-forget.
    	[Token(Token = "0x60002C0")]
    	[Address(RVA = "0x350530", Offset = "0x34EF30", VA = "0x180350530")]
    	[AsyncStateMachine(typeof(_003CUnlockAllStillsAsync_003Ed__15))]
    	private async UniTask UnlockAllStillsAsync()
    	{
    		IReadOnlyCollection<string>[] array = await CGSources.Select(LocateAsync);
    		foreach (IReadOnlyCollection<string> paths in array)
    		{
    			foreach (string path in paths)
    			{
    				NaniUnlockableManager.UnlockItem(path);
    			}
    		}
    		NaniStateManager.SaveGlobal().Forget();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x350650 (builder stub); MoveNext 0x350850, 1081 B
    	// `CreateLocalizableFor<Texture2D>(resources, l10n, fallbackToSource: true)` (both service getters inlined), then
    	// ResourceLoader<T> slot 40 `Locate(path)` with a null path.
    	[Token(Token = "0x60002C1")]
    	[Address(RVA = "0x350650", Offset = "0x34F050", VA = "0x180350650")]
    	[AsyncStateMachine(typeof(_003CLocateAsync_003Ed__16))]
    	private async UniTask<IReadOnlyCollection<string>> LocateAsync(ResourceLoaderConfiguration config)
    	{
            var paths = await config.CreateLocalizableFor<Texture2D>(NaniResourceProviderManager, NaniLocalizationManager).Locate(null);
            return paths.Where(path => IncludeCGPath(config, path)).ToArray();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x350840, 7 B (tail jump to CGGalleryPanel..ctor; no initializers)
    	[Token(Token = "0x60002C2")]
    	[Address(RVA = "0x350840", Offset = "0x34F240", VA = "0x180350840")]
    	public GalleryUi()
    	{
    	}
    }
}
