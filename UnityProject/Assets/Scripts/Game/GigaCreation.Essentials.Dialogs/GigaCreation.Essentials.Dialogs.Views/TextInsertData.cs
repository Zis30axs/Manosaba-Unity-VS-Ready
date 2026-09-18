#nullable enable annotations
﻿using System;
using System.Runtime.CompilerServices;
using System.Text;
using Il2CppDummyDll;

namespace GigaCreation.Essentials.Dialogs.Views
{
    [Token(Token = "0x2000013")]
    public record TextInsertData
    {
    	[Token(Token = "0x17000004")]
    	[CompilerGenerated]
    	protected virtual Type EqualityContract
    	{
    		[Token(Token = "0x6000031")]
    		[Address(RVA = "0xCCDA90", Offset = "0xCCC490", VA = "0x180CCDA90", Slot = "5")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    	}

    	[Token(Token = "0x17000005")]
    	public string LabelId
    	{
    		[Token(Token = "0x6000032")]
    		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6000033")]
    		[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x17000006")]
    	public string[] Texts
    	{
    		[Token(Token = "0x6000034")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6000035")]
    		[Address(RVA = "0x36C200", Offset = "0x36AC00", VA = "0x18036C200")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x6000030")]
    	[Address(RVA = "0x36C0D0", Offset = "0x36AAD0", VA = "0x18036C0D0")]
    	public TextInsertData(string LabelId, params string[] Texts)
    	{
    	}

    	[Token(Token = "0x6000036")]
    	[Address(RVA = "0xCCDB10", Offset = "0xCCC510", VA = "0x180CCDB10", Slot = "3")]
    	[CompilerGenerated]
    	public override string ToString()
    	{
    		return null;
    	}

    	[Token(Token = "0x6000037")]
    	[Address(RVA = "0xCCDC50", Offset = "0xCCC650", VA = "0x180CCDC50", Slot = "6")]
    	[CompilerGenerated]
    	protected virtual bool PrintMembers(StringBuilder builder)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600003A")]
    	[Address(RVA = "0xCCDD30", Offset = "0xCCC730", VA = "0x180CCDD30", Slot = "2")]
    	[CompilerGenerated]
    	public override int GetHashCode()
    	{
    		return default(int);
    	}

    	[Token(Token = "0x600003C")]
    	[Address(RVA = "0xCCDEF0", Offset = "0xCCC8F0", VA = "0x180CCDEF0", Slot = "7")]
    	[CompilerGenerated]
    	public virtual bool Equals(TextInsertData? other)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600003E")]
    	[Address(RVA = "0x36C8D0", Offset = "0x36B2D0", VA = "0x18036C8D0")]
    	[CompilerGenerated]
    	protected TextInsertData(TextInsertData original)
    	{
    	}

    	[Token(Token = "0x600003F")]
    	[Address(RVA = "0x36C9B0", Offset = "0x36B3B0", VA = "0x18036C9B0")]
    	[CompilerGenerated]
    	public void Deconstruct(out string LabelId, out string[] Texts)
    	{
    		LabelId = default;
    		Texts = default;
    		LabelId = default;
    		Texts = default;
    	}
    }
}
