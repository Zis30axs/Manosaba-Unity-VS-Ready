using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x2000027")]
    [AttributeUsage(AttributeTargets.Field)]
    [Conditional("UNITY_EDITOR")]
    public class MaxAttribute : PropertyAttribute, IMinMaxAttribute
    {
    	[Token(Token = "0x40000E7")]
    	[FieldOffset(Offset = "0x18")]
    	private readonly float max;

    	[Token(Token = "0x40000E8")]
    	[FieldOffset(Offset = "0x20")]
    	private readonly string maxPath;

    	[Token(Token = "0x17000003")]
    	public float CapValue
    	{
    		[Token(Token = "0x600002B")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0x39FB70, 6 B -- `movss xmm0, dword ptr [rcx + 0x18]; ret`: the backing field max (+0x18 in dump.cs)
    		[Address(RVA = "0x39FB70", Offset = "0x39E570", VA = "0x18039FB70", Slot = "7")]
    		get
    		{
    			return max;
    		}
    	}

    	[Token(Token = "0x17000004")]
    	public string CapPath
    	{
    		[Token(Token = "0x600002C")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0x377910, 5 B -- `mov rax, qword ptr [rcx + 0x20]; ret`: the backing field maxPath (+0x20 in dump.cs)
    		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910", Slot = "8")]
    		get
    		{
    			return maxPath;
    		}
    	}

    	[Token(Token = "0x600002D")]
    	[Address(RVA = "0x43C8C0", Offset = "0x43B2C0", VA = "0x18043C8C0")]
    	private MaxAttribute()
    	{
    	}

    	[Token(Token = "0x600002E")]
    	[Address(RVA = "0x43CF00", Offset = "0x43B900", VA = "0x18043CF00")]
    	public MaxAttribute(float max)
    	{
    	}

    	[Token(Token = "0x600002F")]
    	[Address(RVA = "0x43CF20", Offset = "0x43B920", VA = "0x18043CF20")]
    	public MaxAttribute(string maxPath)
    	{
    	}

    	[Token(Token = "0x6000030")]
    	[Address(RVA = "0x43CFF0", Offset = "0x43B9F0", VA = "0x18043CFF0", Slot = "9")]
    	public bool DependsOnOtherProperty()
    	{
    		return default(bool);
    	}
    }
}
