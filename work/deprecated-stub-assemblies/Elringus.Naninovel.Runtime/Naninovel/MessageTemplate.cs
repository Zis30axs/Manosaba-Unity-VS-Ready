using System;
using Il2CppDummyDll;
using JetBrains.Annotations;
using UnityEngine;

namespace Naninovel
{
    [Serializable]
    [Token(Token = "0x2000069")]
    public struct MessageTemplate : IEquatable<MessageTemplate>
    {
    	[Token(Token = "0x400015B")]
    	[FieldOffset(Offset = "0x0")]
    	[SerializeField]
    	private string author;

    	[Token(Token = "0x400015C")]
    	[FieldOffset(Offset = "0x8")]
    	[SerializeField]
    	private string template;

    	[Token(Token = "0x17000050")]
    	public string Author
    	{
    		[Token(Token = "0x60001CA")]
    		[Address(RVA = "0x456310", Offset = "0x454D10", VA = "0x180456310")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000051")]
    	public string Template
    	{
    		[Token(Token = "0x60001CB")]
    		[Address(RVA = "0x457050", Offset = "0x455A50", VA = "0x180457050")]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x60001CC")]
    	[Address(RVA = "0x42B240", Offset = "0x429C40", VA = "0x18042B240")]
    	public MessageTemplate(string author, string template)
    	{ this = default; }

    	[Token(Token = "0x60001CD")]
    	[Address(RVA = "0x4572E0", Offset = "0x455CE0", VA = "0x1804572E0")]
    	public bool Applicable([CanBeNull] string authorId)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60001CE")]
    	[Address(RVA = "0x457440", Offset = "0x455E40", VA = "0x180457440", Slot = "4")]
    	public bool Equals(MessageTemplate other)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60001CF")]
    	[Address(RVA = "0x4574F0", Offset = "0x455EF0", VA = "0x1804574F0", Slot = "0")]
    	public override bool Equals(object obj)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60001D0")]
    	[Address(RVA = "0x457590", Offset = "0x455F90", VA = "0x180457590", Slot = "2")]
    	public override int GetHashCode()
    	{
    		return default(int);
    	}
    }
}
