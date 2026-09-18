using System;
using Il2CppDummyDll;

namespace GigaCreation.Essentials.SaveLoad
{
    [Serializable]
    [Token(Token = "0x2000007")]
    public struct VersionNumber
    {
    	[Token(Token = "0x4000007")]
    	[FieldOffset(Offset = "0x0")]
    	public int Major;

    	[Token(Token = "0x4000008")]
    	[FieldOffset(Offset = "0x4")]
    	public int Minor;

    	[Token(Token = "0x4000009")]
    	[FieldOffset(Offset = "0x8")]
    	public int Patch;

    	[Token(Token = "0x17000002")]
    	public static VersionNumber Current
    	{
    		[Token(Token = "0x6000008")]
    		[Address(RVA = "0xCD5FB0", Offset = "0xCD49B0", VA = "0x180CD5FB0")]
    		get
    		{
    			return default(VersionNumber);
    		}
    	}

    	[Token(Token = "0x6000009")]
    	[Address(RVA = "0xCD6190", Offset = "0xCD4B90", VA = "0x180CD6190")]
    	public VersionNumber(int major, int minor, int patch)
    	{ this = default; }

    	[Token(Token = "0x600000A")]
    	[Address(RVA = "0xCD61A0", Offset = "0xCD4BA0", VA = "0x180CD61A0")]
    	public static VersionNumber FromString(string str)
    	{
    		return default(VersionNumber);
    	}

    	[Token(Token = "0x600000B")]
    	[Address(RVA = "0xCD6330", Offset = "0xCD4D30", VA = "0x180CD6330")]
    	public static bool operator <(VersionNumber left, VersionNumber right)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x600000C")]
    	[Address(RVA = "0xCD6350", Offset = "0xCD4D50", VA = "0x180CD6350")]
    	public static bool operator >(VersionNumber left, VersionNumber right)
    	{
    		return default(bool);
    	}
    }
}
