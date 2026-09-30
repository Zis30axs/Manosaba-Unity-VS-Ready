using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Naninovel
{
    [Token(Token = "0x200024B")]
    public class AddressableResourceLocator<TResource> : LocateResourcesRunner<TResource> where TResource : Object
    {
    	[Token(Token = "0x40008FF")]
    	[FieldOffset(Offset = "0x0")]
    	private readonly List<IResourceLocation> locations;

    	[Token(Token = "0x6000A2A")]
    	public AddressableResourceLocator(AddressableResourceProvider provider, string resourcePath, List<IResourceLocation> locations)
    	{
    	}

    	[Token(Token = "0x6000A2B")]
    	public override UniTask Run()
    	{
    		return default(UniTask);
    	}
    }
}
