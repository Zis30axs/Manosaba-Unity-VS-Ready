using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel.UI
{
    [Token(Token = "0x20005B6")]
    public class RevealState : IRevealInfo
    {
    	[Token(Token = "0x40015E7")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	private readonly Dictionary<int, CharRevealInfo> indexToInfo;

    	[Token(Token = "0x40015E8")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	private readonly Dictionary<int, float> indexToAppendTime;

    	[Token(Token = "0x17000433")]
    	public int LastRevealedCharIndex
    	{
    		[Token(Token = "0x6001C97")]
    		[Address(RVA = "0x38F590", Offset = "0x38DF90", VA = "0x18038F590", Slot = "6")]
    		[CompilerGenerated]
    		get
    		{
    			return default(int);
    		}
    		[Token(Token = "0x6001C98")]
    		[Address(RVA = "0x39B780", Offset = "0x39A180", VA = "0x18039B780")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000434")]
    	public int LastAppendIndex
    	{
    		[Token(Token = "0x6001C99")]
    		[Address(RVA = "0x38F5A0", Offset = "0x38DFA0", VA = "0x18038F5A0", Slot = "7")]
    		[CompilerGenerated]
    		get
    		{
    			return default(int);
    		}
    		[Token(Token = "0x6001C9A")]
    		[Address(RVA = "0x39B790", Offset = "0x39A190", VA = "0x18039B790")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x14000090")]
    	public event Action OnChange
    	{
    		[Token(Token = "0x6001C95")]
    		[Address(RVA = "0x4A2550", Offset = "0x4A0F50", VA = "0x1804A2550", Slot = "4")]
    		[CompilerGenerated]
    		add
    		{
    		}
    		[Token(Token = "0x6001C96")]
    		[Address(RVA = "0x4A2640", Offset = "0x4A1040", VA = "0x1804A2640", Slot = "5")]
    		[CompilerGenerated]
    		remove
    		{
    		}
    	}

    	[Token(Token = "0x6001C9B")]
    	[Address(RVA = "0x4A2730", Offset = "0x4A1130", VA = "0x1804A2730")]
    	public void SetLast(int charIndex, float duration = 0f, AsyncToken token = default)
    	{
    	}

    	[Token(Token = "0x6001C9C")]
    	[Address(RVA = "0x4A2940", Offset = "0x4A1340", VA = "0x1804A2940")]
    	public void SetLatAppendIndex(int charIndex)
    	{
    	}

    	[Token(Token = "0x6001C9D")]
    	[Address(RVA = "0x4A2B00", Offset = "0x4A1500", VA = "0x1804A2B00")]
    	public void Reset()
    	{
    	}

    	[Token(Token = "0x6001C9E")]
    	[Address(RVA = "0x4A2BA0", Offset = "0x4A15A0", VA = "0x1804A2BA0", Slot = "8")]
    	public float GetRevealRatio(int charIndex, float modifier = 1f)
    	{
    		return default(float);
    	}

    	[Token(Token = "0x6001C9F")]
    	[Address(RVA = "0x4A2D90", Offset = "0x4A1790", VA = "0x1804A2D90", Slot = "9")]
    	public float GetAppendTime(int charIndex)
    	{
    		return default(float);
    	}

    	[Token(Token = "0x6001CA0")]
    	[Address(RVA = "0x4A2E10", Offset = "0x4A1810", VA = "0x1804A2E10")]
    	public RevealState()
    	{
    	}
    }
}
