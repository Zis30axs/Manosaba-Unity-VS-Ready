using System;
using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x2000262")]
    public class ProjectResourceLocator<TResource> : LocateResourcesRunner<TResource> where TResource : UnityEngine.Object
    {
    	[Token(Token = "0x4000941")]
    	[FieldOffset(Offset = "0x0")]
    	private readonly IReadOnlyDictionary<string, Type> projectResources;

    	[Token(Token = "0x6000AA3")]
    	public ProjectResourceLocator(IResourceProvider provider, string resourcesPath, IReadOnlyDictionary<string, Type> projectResources)
    	{
    	}

    	[Token(Token = "0x6000AA4")]
    	public override UniTask Run()
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000AA5")]
    	public static IReadOnlyCollection<string> LocateProjectResources(string resourcesPath, IReadOnlyDictionary<string, Type> projectResources)
    	{
    		return null;
    	}
    }
}
