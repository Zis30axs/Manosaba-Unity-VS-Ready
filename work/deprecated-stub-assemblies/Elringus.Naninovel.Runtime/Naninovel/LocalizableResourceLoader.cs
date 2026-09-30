using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x20003BD")]
    public class LocalizableResourceLoader<TResource> : ResourceLoader<TResource> where TResource : UnityEngine.Object
    {
    	[StructLayout(3)]
    	[Token(Token = "0x20003C0")]
    	[CompilerGenerated]
    	private struct _003CHandleLocaleChanged_003Ed__15
    {
    		[Token(Token = "0x4000E0D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000E0E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000E0F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public LocalizableResourceLoader<TResource> _003C_003E4__this;

    		[Token(Token = "0x4000E10")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private PooledObject<List<UniTask>> _003C___003E5__2;

    		[Token(Token = "0x4000E11")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600122E")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600122F")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000DFC")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	protected readonly ILocalizationManager L10n;

    	[Token(Token = "0x4000DFD")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	protected readonly List<IResourceProvider> SourceProviders;

    	[Token(Token = "0x4000DFE")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	protected readonly string SourcePrefix;

    	[Token(Token = "0x4000DFF")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	protected readonly bool FallbackToSource;

    	[Token(Token = "0x4000E00")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private string overrideLocale;

    	[Token(Token = "0x17000274")]
    	public string OverrideLocale
    	{
    		[Token(Token = "0x6001221")]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6001222")]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x14000062")]
    	public event Action<Resource<TResource>> OnLocalized
    	{
    		[Token(Token = "0x600121F")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x6001220")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x6001223")]
    	public LocalizableResourceLoader(List<IResourceProvider> providers = default, IHoldersTracker holdersTracker = default, ILocalizationManager l10n = default, string sourcePrefix = default, bool fallbackToSource = true)
    	{
    	}

    	[Token(Token = "0x6001224")]
    	~LocalizableResourceLoader()
    	{
    	}

    	[Token(Token = "0x6001225")]
    	protected void SetOverrideLocale(string locale)
    	{
    	}

    	[Token(Token = "0x6001226")]
    	protected void InitializeProvisionSources()
    	{
    	}

    	[Token(Token = "0x6001227")]
    	[AsyncStateMachine(typeof(LocalizableResourceLoader<>._003CHandleLocaleChanged_003Ed__15))]
    	protected UniTask HandleLocaleChanged(LocaleChangedArgs _)
    	{
    		return default(UniTask);
    	}
    }
}
