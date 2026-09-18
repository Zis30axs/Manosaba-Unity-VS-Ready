using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x2000015")]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
    [Conditional("UNITY_EDITOR")]
    public class FixedValuesAttribute : PropertyAttribute
    {
    	[Token(Token = "0x40000C3")]
    	[FieldOffset(Offset = "0x18")]
    	public object[] values;

    	[Token(Token = "0x600000E")]
    	[Address(RVA = "0x43C530", Offset = "0x43AF30", VA = "0x18043C530")]
    	public FixedValuesAttribute(params object[] values)
    	{
    	}
    }
}
