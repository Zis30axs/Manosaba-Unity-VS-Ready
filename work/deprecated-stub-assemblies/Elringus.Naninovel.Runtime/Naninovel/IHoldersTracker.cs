using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x2000253")]
    public interface IHoldersTracker
    {
    	[Token(Token = "0x6000A4F")]
    	int Hold(Object obj, object holder);

    	[Token(Token = "0x6000A50")]
    	int Release(Object obj, object holder);

    	[Token(Token = "0x6000A51")]
    	int CountHolders(Object obj);
    }
}
