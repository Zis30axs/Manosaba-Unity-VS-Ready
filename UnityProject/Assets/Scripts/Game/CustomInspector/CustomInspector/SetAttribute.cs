using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x2000079")]
    [Conditional("UNITY_EDITOR")]
    public class SetAttribute : PropertyAttribute
    {
    	[Token(Token = "0x60001BD")]
    	[Address(RVA = "0x43C650", Offset = "0x43B050", VA = "0x18043C650")]
    	public SetAttribute()
    	{
    	}
    }
}
