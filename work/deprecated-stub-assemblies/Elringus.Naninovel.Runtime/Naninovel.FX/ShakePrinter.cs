using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.FX
{
    [Token(Token = "0x20005F9")]
    public class ShakePrinter : ShakeTransform
    {
    	[Token(Token = "0x6001DEB")]
    	[Address(RVA = "0x4C0B00", Offset = "0x4BF500", VA = "0x1804C0B00", Slot = "31")]
    	protected override Transform GetShakenTransform()
    	{
    		return null;
    	}

    	[Token(Token = "0x6001DEC")]
    	[Address(RVA = "0x4BF720", Offset = "0x4BE120", VA = "0x1804BF720")]
    	public ShakePrinter()
    	{
    	}
    }
}
