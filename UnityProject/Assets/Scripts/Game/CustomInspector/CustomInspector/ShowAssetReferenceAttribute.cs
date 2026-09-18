using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x2000037")]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class ShowAssetReferenceAttribute : PropertyAttribute
    {
    	[Token(Token = "0x400010B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    	public readonly string fileName;

    	[Token(Token = "0x6000053")]
    	[Address(RVA = "0x43C530", Offset = "0x43AF30", VA = "0x18043C530")]
    	public ShowAssetReferenceAttribute([Optional] string fileName)
    	{
    	}
    }
}
