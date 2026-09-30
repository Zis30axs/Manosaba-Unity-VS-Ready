#nullable enable annotations
﻿using System;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x200006A")]
    public struct PrintedMessage : IEquatable<PrintedMessage>
    {
    	[Token(Token = "0x400015D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	private LocalizableText text;

    	[Token(Token = "0x400015E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    	[SerializeField]
    	private MessageAuthor author;

    	[Token(Token = "0x17000052")]
    	public LocalizableText Text
    	{
    		[Token(Token = "0x60001D1")]
    		[Address(RVA = "0x456310", Offset = "0x454D10", VA = "0x180456310")]
    		get
    		{
    			return default(LocalizableText);
    		}
    	}

    	[Token(Token = "0x17000053")]
    	public MessageAuthor? Author
    	{
    		[Token(Token = "0x60001D2")]
    		[Address(RVA = "0x457610", Offset = "0x456010", VA = "0x180457610")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x60001D3")]
    	[Address(RVA = "0x4576D0", Offset = "0x4560D0", VA = "0x1804576D0")]
    	public PrintedMessage(LocalizableText text, [Optional] MessageAuthor author)
    	{ this = default; }

    	[Token(Token = "0x60001D4")]
    	[Address(RVA = "0x457780", Offset = "0x456180", VA = "0x180457780", Slot = "4")]
    	public bool Equals(PrintedMessage other)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60001D5")]
    	[Address(RVA = "0x457840", Offset = "0x456240", VA = "0x180457840", Slot = "0")]
    	public override bool Equals(object obj)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60001D6")]
    	[Address(RVA = "0x4578E0", Offset = "0x4562E0", VA = "0x1804578E0", Slot = "2")]
    	public override int GetHashCode()
    	{
    		return default(int);
    	}
    }
}
