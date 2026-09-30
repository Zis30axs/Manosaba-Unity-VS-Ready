using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x200025B")]
    public class LocalResourceLocator<TResource> : LocateResourcesRunner<TResource> where TResource : Object
    {
    	[Token(Token = "0x4000930")]
    	[FieldOffset(Offset = "0x0")]
    	private readonly IEnumerable<IRawConverter<TResource>> converters;

    	[Token(Token = "0x17000183")]
    	public virtual string RootPath
    	{
    		[Token(Token = "0x6000A88")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x6000A89")]
    	public LocalResourceLocator(IResourceProvider provider, string rootPath, string resourcesPath, IEnumerable<IRawConverter<TResource>> converters)
    	{
    	}

    	[Token(Token = "0x6000A8A")]
    	public override UniTask Run()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000A8B")]
    	private IReadOnlyCollection<string> LocateResources()
    	{
    		return null;
    	}
    }
}
