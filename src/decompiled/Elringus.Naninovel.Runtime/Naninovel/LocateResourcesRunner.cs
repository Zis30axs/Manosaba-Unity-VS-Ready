using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Token(Token = "0x2000292")]
public abstract class LocateResourcesRunner<TResource> : ResourceRunner<IReadOnlyCollection<string>> where TResource : Object
{
	[Token(Token = "0x6000B88")]
	protected LocateResourcesRunner(IResourceProvider provider = default, string path = default)
	{
	}
}
