using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x200042F")]
    public struct PlaybackSpot : IEquatable<PlaybackSpot>
    {
    	[Token(Token = "0x4000F5B")]
    	[FieldOffset(Offset = "0x0")]
    	public static readonly PlaybackSpot Invalid;

    	[Token(Token = "0x4000F5C")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	private string scriptPath;

    	[Token(Token = "0x4000F5D")]
    	[FieldOffset(Offset = "0x8")]
    	[SerializeField]
    	private int lineIndex;

    	[Token(Token = "0x4000F5E")]
    	[FieldOffset(Offset = "0xC")]
    	[SerializeField]
    	private int inlineIndex;

    	[Token(Token = "0x170002E1")]
    	public bool Valid
    	{
    		[Token(Token = "0x6001494")]
    		[Address(RVA = "0x5BD6F0", Offset = "0x5BC0F0", VA = "0x1805BD6F0")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x170002E2")]
    	public string ScriptPath
    	{
    		[Token(Token = "0x6001495")]
    		[Address(RVA = "0x456310", Offset = "0x454D10", VA = "0x180456310")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x170002E3")]
    	public int LineIndex
    	{
    		[Token(Token = "0x6001496")]
    		[Address(RVA = "0x55F3F0", Offset = "0x55DDF0", VA = "0x18055F3F0")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x170002E4")]
    	public int LineNumber
    	{
    		[Token(Token = "0x6001497")]
    		[Address(RVA = "0x5BD710", Offset = "0x5BC110", VA = "0x1805BD710")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x170002E5")]
    	public int InlineIndex
    	{
    		[Token(Token = "0x6001498")]
    		[Address(RVA = "0x5BD720", Offset = "0x5BC120", VA = "0x1805BD720")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x6001499")]
    	[Address(RVA = "0x5BD730", Offset = "0x5BC130", VA = "0x1805BD730")]
    	public PlaybackSpot(string scriptPath, int lineIndex, int inlineIndex)
    	{ this = default; }

    	[Token(Token = "0x600149A")]
    	[Address(RVA = "0x5BD7A0", Offset = "0x5BC1A0", VA = "0x1805BD7A0", Slot = "0")]
    	public override bool Equals(object obj)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600149B")]
    	[Address(RVA = "0x5BD8A0", Offset = "0x5BC2A0", VA = "0x1805BD8A0", Slot = "4")]
    	public bool Equals(PlaybackSpot other)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600149C")]
    	[Address(RVA = "0x5BD920", Offset = "0x5BC320", VA = "0x1805BD920", Slot = "2")]
    	public override int GetHashCode()
    	{
    		return default(int);
    	}

    	[Token(Token = "0x600149D")]
    	[Address(RVA = "0x5BD9D0", Offset = "0x5BC3D0", VA = "0x1805BD9D0")]
    	public static bool operator ==(PlaybackSpot left, PlaybackSpot right)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600149E")]
    	[Address(RVA = "0x5BDA80", Offset = "0x5BC480", VA = "0x1805BDA80")]
    	public static bool operator !=(PlaybackSpot left, PlaybackSpot right)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600149F")]
    	[Address(RVA = "0x5BDB90", Offset = "0x5BC590", VA = "0x1805BDB90", Slot = "3")]
    	public override string ToString()
    	{
    		return null;
    	}
    }
}
