using System;
using Il2CppDummyDll;

namespace GigaCreation.NaninovelExtender.Common
{
    [Token(Token = "0x2000014")]
    public interface INaninovelInteractionBlocker : IDisposable
    {
    	[Token(Token = "0x6000025")]
    	void SetSortingOrder(int order);
    }
}
