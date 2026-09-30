using System;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x2000068")]
    public struct MessageAuthor : IEquatable<MessageAuthor>
    {
    	[Token(Token = "0x4000159")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	private string id;

    	[Token(Token = "0x400015A")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    	[SerializeField]
    	private LocalizableText label;

    	[Token(Token = "0x1700004E")]
    	public string Id
    	{
    		[Token(Token = "0x60001C4")]
    		[Address(RVA = "0x456310", Offset = "0x454D10", VA = "0x180456310")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x1700004F")]
    	public LocalizableText Label
    	{
    		[Token(Token = "0x60001C5")]
    		[Address(RVA = "0x457050", Offset = "0x455A50", VA = "0x180457050")]
    		get
    		{
    			return default(LocalizableText);
    		}
    	}

    	[Token(Token = "0x60001C6")]
    	[Address(RVA = "0x457060", Offset = "0x455A60", VA = "0x180457060")]
    	public MessageAuthor(string id, [Optional] LocalizableText label)
    	{ this = default; }

    	[Token(Token = "0x60001C7")]
    	[Address(RVA = "0x457110", Offset = "0x455B10", VA = "0x180457110", Slot = "4")]
    	public bool Equals(MessageAuthor other)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60001C8")]
    	[Address(RVA = "0x4571C0", Offset = "0x455BC0", VA = "0x1804571C0", Slot = "0")]
    	public override bool Equals(object obj)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60001C9")]
    	[Address(RVA = "0x457260", Offset = "0x455C60", VA = "0x180457260", Slot = "2")]
    	public override int GetHashCode()
    	{
    		return default(int);
    	}
    }
}
