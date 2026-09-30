using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel.FX
{
    [Token(Token = "0x20005F5")]
    public class ShakeCamera : ShakeTransform
    {
    	[Token(Token = "0x6001DE0")]
    	[Address(RVA = "0x4BF730", Offset = "0x4BE130", VA = "0x1804BF730", Slot = "31")]
    	protected override Transform GetShakenTransform()
    	{
    		return null;
    	}

    	[Token(Token = "0x6001DE1")]
    	[Address(RVA = "0x4BF720", Offset = "0x4BE120", VA = "0x1804BF720")]
    	public ShakeCamera()
    	{
    	}
    }
}
