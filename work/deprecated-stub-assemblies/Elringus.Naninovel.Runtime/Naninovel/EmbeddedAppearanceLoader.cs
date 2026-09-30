using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x2000024")]
    public class EmbeddedAppearanceLoader<TResource> : LocalizableResourceLoader<TResource> where TResource : Object
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000025")]
    	[CompilerGenerated]
    	private struct _003CNaninovel_002DIResourceLoader_002DLoad_003Ed__5
    {
    		[Token(Token = "0x4000093")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000094")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder<Resource> _003C_003Et__builder;

    		[Token(Token = "0x4000095")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public EmbeddedAppearanceLoader<TResource> _003C_003E4__this;

    		[Token(Token = "0x4000096")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public object holder;

    		[Token(Token = "0x4000097")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private UniTask<Resource<TResource>>.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x60000A8")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x60000A9")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000092")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private readonly string actorId;

    	[Token(Token = "0x600009D")]
    	public EmbeddedAppearanceLoader(string actorId, ActorMetadata meta, IResourceProviderManager resources, ILocalizationManager l10n)
    	{
    	}

    	[Token(Token = "0x600009E")]
    	private UniTask<bool> Naninovel_002EIResourceLoader_002EExists(string path)
    	{
    		return default(UniTask<bool>);
    	}

    	[Token(Token = "0x600009F")]
    	private bool Naninovel_002EIResourceLoader_002EIsLoaded(string path)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60000A0")]
    	private Resource Naninovel_002EIResourceLoader_002EGetLoaded(string path)
    	{
    		return null;
    	}

    	[Token(Token = "0x60000A1")]
    	[AsyncStateMachine(typeof(EmbeddedAppearanceLoader<>._003CNaninovel_002DIResourceLoader_002DLoad_003Ed__5))]
    	private UniTask<Resource> Naninovel_002EIResourceLoader_002ELoad(string path, object holder)
    	{
    		return default(UniTask<Resource>);
    	}

    	[Token(Token = "0x60000A2")]
    	private UniTask<Resource<TResource>> Naninovel_002EIResourceLoader_003CTResource_003E_002ELoad(string path, object holder)
    	{
    		return default(UniTask<Resource<TResource>>);
    	}

    	[Token(Token = "0x60000A3")]
    	private void Naninovel_002EIResourceLoader_002EHold(string path, object holder)
    	{
    	}

    	[Token(Token = "0x60000A4")]
    	private void Naninovel_002EIResourceLoader_002ERelease(string path, object holder, bool unload)
    	{
    	}

    	[Token(Token = "0x60000A5")]
    	private void Naninovel_002EIResourceLoader_002EReleaseAll(object holder, bool unload)
    	{
    	}

    	[Token(Token = "0x60000A6")]
    	private bool Naninovel_002EIResourceLoader_002EIsHeldBy(string path, object holder)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60000A7")]
    	private int Naninovel_002EIResourceLoader_002ECountHolders(string path)
    	{
    		return default(int);
    	}
    }
}
