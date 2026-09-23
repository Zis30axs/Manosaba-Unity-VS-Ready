using System;
using System.Diagnostics;
using Il2CppDummyDll;
using UnityEngine;

namespace CustomInspector
{
    [Token(Token = "0x200002A")]
    [AttributeUsage(AttributeTargets.Field)]
    [Conditional("UNITY_EDITOR")]
    public class Min2Attribute : PropertyAttribute, IMinMaxAttribute
    {
    	[Token(Token = "0x40000F0")]
    	[FieldOffset(Offset = "0x18")]
    	private readonly float min;

    	[Token(Token = "0x40000F1")]
    	[FieldOffset(Offset = "0x20")]
    	private readonly string minPath;

    	[Token(Token = "0x17000005")]
    	public float CapValue
    	{
    		[Token(Token = "0x6000032")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0x39FB70, 6 B -- `movss xmm0, dword ptr [rcx + 0x18]; ret`: the backing field min (+0x18 in dump.cs)
    		[Address(RVA = "0x39FB70", Offset = "0x39E570", VA = "0x18039FB70", Slot = "7")]
    		get
    		{
    			return min;
    		}
    	}

    	[Token(Token = "0x17000006")]
    	public string CapPath
    	{
    		[Token(Token = "0x6000033")]
    		// RECONSTRUCTED | Confidence: A | RVA: 0x377910, 5 B -- `mov rax, qword ptr [rcx + 0x20]; ret`: the backing field minPath (+0x20 in dump.cs)
    		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910", Slot = "8")]
    		get
    		{
    			return minPath;
    		}
    	}

    	[Token(Token = "0x6000034")]
    	[Address(RVA = "0x43C8C0", Offset = "0x43B2C0", VA = "0x18043C8C0")]
    	public Min2Attribute()
    	{
    	}

    	[Token(Token = "0x6000035")]
    	[Address(RVA = "0x43CF00", Offset = "0x43B900", VA = "0x18043CF00")]
    	public Min2Attribute(float min)
    	{
    	}

    	[Token(Token = "0x6000036")]
    	[Address(RVA = "0x43D070", Offset = "0x43BA70", VA = "0x18043D070")]
    	public Min2Attribute(string minPath)
    	{
    	}

    	[Token(Token = "0x6000037")]
    	[Address(RVA = "0x43CFF0", Offset = "0x43B9F0", VA = "0x18043CFF0", Slot = "9")]
    	public bool DependsOnOtherProperty()
    	{
    		return default(bool);
    	}
    }
}
