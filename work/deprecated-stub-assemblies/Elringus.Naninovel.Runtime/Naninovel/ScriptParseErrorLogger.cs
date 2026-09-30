using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel
{
    [Token(Token = "0x200041B")]
    public class ScriptParseErrorLogger
    {
    	[Token(Token = "0x4000F05")]
    	[FieldOffset(Offset = "0x0")]
    	private static readonly Stack<ScriptParseErrorLogger> pool;

    	[Token(Token = "0x4000F06")]
    	[FieldOffset(Offset = "0x18")]
    	private readonly IEnumerator<ScriptParseError> enumerator;

    	[Token(Token = "0x4000F07")]
    	[FieldOffset(Offset = "0x20")]
    	private string scriptPathOrName;

    	[Token(Token = "0x170002C4")]
    	public int Count
    	{
    		[Token(Token = "0x60013F3")]
    		[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650", Slot = "4")]
    		[CompilerGenerated]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x170002C5")]
    	public bool IsReadOnly
    	{
    		[Token(Token = "0x60013F4")]
    		[Address(RVA = "0x3AA360", Offset = "0x3A8D60", VA = "0x1803AA360", Slot = "5")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x60013F5")]
    	[Address(RVA = "0x5B4690", Offset = "0x5B3090", VA = "0x1805B4690")]
    	private ScriptParseErrorLogger()
    	{
    	}

    	[Token(Token = "0x60013F6")]
    	[Address(RVA = "0x5B4730", Offset = "0x5B3130", VA = "0x1805B4730")]
    	public static ScriptParseErrorLogger GetFor(string scriptPathOrName)
    	{
    		return null;
    	}

    	[Token(Token = "0x60013F7")]
    	[Address(RVA = "0x5B4910", Offset = "0x5B3310", VA = "0x1805B4910")]
    	public static void Return(ScriptParseErrorLogger logger)
    	{
    	}

    	[Token(Token = "0x60013F8")]
    	[Address(RVA = "0x5B49A0", Offset = "0x5B33A0", VA = "0x1805B49A0", Slot = "6")]
    	public void Add(ScriptParseError item)
    	{
    	}

    	[Token(Token = "0x60013F9")]
    	[Address(RVA = "0x42F6D0", Offset = "0x42E0D0", VA = "0x18042F6D0", Slot = "10")]
    	public bool Remove(ScriptParseError item)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60013FA")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "7")]
    	public void Clear()
    	{
    	}

    	[Token(Token = "0x60013FB")]
    	[Address(RVA = "0x3AA360", Offset = "0x3A8D60", VA = "0x1803AA360", Slot = "8")]
    	public bool Contains(ScriptParseError item)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x60013FC")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "9")]
    	public void CopyTo(ScriptParseError[] array, int arrayIndex)
    	{
    	}

    	[Token(Token = "0x60013FD")]
    	[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "11")]
    	public IEnumerator<ScriptParseError> GetEnumerator()
    	{
    		return null;
    	}

    	[Token(Token = "0x60013FE")]
    	[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "12")]
    	private IEnumerator System_002ECollections_002EIEnumerable_002EGetEnumerator()
    	{
    		return null;
    	}
    }
}
