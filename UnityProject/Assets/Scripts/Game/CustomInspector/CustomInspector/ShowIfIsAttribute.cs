using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x200003D")]
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class ShowIfIsAttribute : PropertyAttribute
    {
    	[Token(Token = "0x4000121")]
    	[FieldOffset(Offset = "0x18")]
    	public readonly string fieldPath;

    	[Token(Token = "0x4000122")]
    	[FieldOffset(Offset = "0x20")]
    	public readonly object value;

    	[Token(Token = "0x4000123")]
    	[FieldOffset(Offset = "0x28")]
    	public DisabledStyle style;

    	[Token(Token = "0x4000124")]
    	[FieldOffset(Offset = "0x2C")]
    	public int indent;

    	[Token(Token = "0x17000009")]
    	public bool Inverted
    	{
    		[Token(Token = "0x6000066")]
    		[Address(RVA = "0x43E270", Offset = "0x43CC70", VA = "0x18043E270")]
    		[CompilerGenerated]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x6000067")]
    		[Address(RVA = "0x43E280", Offset = "0x43CC80", VA = "0x18043E280")]
    		[CompilerGenerated]
    		protected set
    		{
    		}
    	}

    	[Token(Token = "0x6000068")]
    	[Address(RVA = "0x43E290", Offset = "0x43CC90", VA = "0x18043E290")]
    	public ShowIfIsAttribute(string fieldPath, object value)
    	{
    	}
    }
}
