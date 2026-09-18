using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x2000026")]
    [AttributeUsage(AttributeTargets.Field)]
    [Conditional("UNITY_EDITOR")]
    public class MaskAttribute : PropertyAttribute
    {
    	[Token(Token = "0x40000E5")]
    	[FieldOffset(Offset = "0x18")]
    	public readonly int bitsAmount;

    	[Token(Token = "0x40000E6")]
    	[FieldOffset(Offset = "0x20")]
    	public readonly string[] bitNames;

    	[Token(Token = "0x6000028")]
    	[Address(RVA = "0x43CD60", Offset = "0x43B760", VA = "0x18043CD60")]
    	public MaskAttribute()
    	{
    	}

    	[Token(Token = "0x6000029")]
    	[Address(RVA = "0x43CD70", Offset = "0x43B770", VA = "0x18043CD70")]
    	public MaskAttribute(int bitsAmount)
    	{
    	}

    	[Token(Token = "0x600002A")]
    	[Address(RVA = "0x43CE00", Offset = "0x43B800", VA = "0x18043CE00")]
    	public MaskAttribute(params string[] bitNames)
    	{
    	}
    }
}
