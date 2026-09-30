using System;
using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x20003BC")]
    public interface IResourceProviderManager : IEngineService<ResourceProviderConfiguration>, IEngineService, IHoldersTracker
    {
    	[Token(Token = "0x14000061")]
    	event Action<string> OnProviderMessage;

    	[Token(Token = "0x600121B")]
    	bool IsProviderInitialized(string providerType);

    	[Token(Token = "0x600121C")]
    	IResourceProvider GetProvider(string providerType);

    	[Token(Token = "0x600121D")]
    	void GetProviders(IList<IResourceProvider> providers, IReadOnlyList<string> types);

    	[Token(Token = "0x600121E")]
    	[Address(RVA = "0x59A0A0", Offset = "0x598AA0", VA = "0x18059A0A0", Slot = "5")]
    	List<IResourceProvider> GetProviders(IReadOnlyList<string> types)
    	{
    		return null;
    	}
    }
}
