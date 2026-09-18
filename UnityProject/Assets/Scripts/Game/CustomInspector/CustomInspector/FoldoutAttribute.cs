using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x2000016")]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    [Conditional("UNITY_EDITOR")]
    public class FoldoutAttribute : PropertyAttribute
    {
    	[Token(Token = "0x600000F")]
    	[Address(RVA = "0x43C8B0", Offset = "0x43B2B0", VA = "0x18043C8B0")]
    	public FoldoutAttribute()
    	{
    	}
    }
}
