using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x200000D")]
    [AttributeUsage(AttributeTargets.Field)]
    [Conditional("UNITY_EDITOR")]
    public class AssetsOnlyAttribute : PropertyAttribute
    {
    	[Token(Token = "0x6000006")]
    	[Address(RVA = "0x43C650", Offset = "0x43B050", VA = "0x18043C650")]
    	public AssetsOnlyAttribute()
    	{
    	}
    }
}
