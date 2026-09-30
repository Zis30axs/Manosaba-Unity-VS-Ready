using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x2000293")]
    public abstract class LoadResourceRunner<TResource> : ResourceRunner<Resource<TResource>> where TResource : Object
    {
    	[Token(Token = "0x6000B89")]
    	protected LoadResourceRunner(IResourceProvider provider = default, string path = default)
    	{
    	}
    }
}
