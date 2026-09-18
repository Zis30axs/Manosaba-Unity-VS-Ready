using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x200004F")]
    [Conditional("UNITY_EDITOR")]
    public class AssetPathAttribute : PropertyAttribute
    {
    	[Token(Token = "0x6000084")]
    	[Address(RVA = "0x43C650", Offset = "0x43B050", VA = "0x18043C650")]
    	public AssetPathAttribute()
    	{
    	}
    }
}
