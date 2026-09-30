using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x20003E7")]
    public abstract class ScriptLine
    {
    	[Token(Token = "0x4000EA0")]
    	[FieldOffset(Offset = "0x10")]
    	[SerializeField]
    	private int lineIndex;

    	[Token(Token = "0x4000EA1")]
    	[FieldOffset(Offset = "0x14")]
    	[SerializeField]
    	private int indent;

    	[Token(Token = "0x4000EA2")]
    	[FieldOffset(Offset = "0x18")]
    	[SerializeField]
    	private string lineHash;

    	[Token(Token = "0x17000291")]
    	public int LineIndex
    	{
    		[Token(Token = "0x60012C4")]
    		[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x17000292")]
    	public int LineNumber
    	{
    		[Token(Token = "0x60012C5")]
    		[Address(RVA = "0x5A4D40", Offset = "0x5A3740", VA = "0x1805A4D40")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x17000293")]
    	public string LineHash
    	{
    		[Token(Token = "0x60012C6")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000294")]
    	public int Indent
    	{
    		[Token(Token = "0x60012C7")]
    		[Address(RVA = "0x38F580", Offset = "0x38DF80", VA = "0x18038F580")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x60012C8")]
    	[Address(RVA = "0x5A4D50", Offset = "0x5A3750", VA = "0x1805A4D50")]
    	protected ScriptLine(int lineIndex = default, int indent = default, string lineHash = default)
    	{
    	}
    }
}
