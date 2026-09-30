using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.ManagedText;

namespace Naninovel
{
    [Token(Token = "0x200039B")]
    public class ScriptLocalizationParser
    {
    	[Token(Token = "0x200039C")]
    	public class Options
    	{
    		[Token(Token = "0x1700025F")]
    		public char Separator
    		{
    			[Token(Token = "0x6001177")]
    			[Address(RVA = "0x58BF20", Offset = "0x58A920", VA = "0x18058BF20")]
    			[CompilerGenerated]
    			get
    			{
    				return default(char);
    			}
    			[Token(Token = "0x6001178")]
    			[Address(RVA = "0x58BF30", Offset = "0x58A930", VA = "0x18058BF30")]
    			[CompilerGenerated]
    			set
    			{
    			}
    		}

    		[Token(Token = "0x6001179")]
    		[Address(RVA = "0x58BF40", Offset = "0x58A940", VA = "0x18058BF40")]
    		public Options()
    		{
    		}
    	}

    	[Token(Token = "0x4000D63")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private static readonly MultilineManagedTextParser multilineParser;

    	[Token(Token = "0x4000D64")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    	private readonly List<ManagedTextRecord> records;

    	[Token(Token = "0x4000D65")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    	private readonly List<string> splitKeys;

    	[Token(Token = "0x4000D66")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	private readonly List<string> splitValues;

    	[Token(Token = "0x4000D67")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	private readonly List<string> splitComments;

    	[Token(Token = "0x4000D68")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	private readonly TextSplitter splitter;

    	[Token(Token = "0x4000D69")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	private readonly Options options;

    	[Token(Token = "0x6001173")]
    	[Address(RVA = "0x58B2E0", Offset = "0x589CE0", VA = "0x18058B2E0")]
    	public ScriptLocalizationParser([Optional] Options options)
    	{
    	}

    	[Token(Token = "0x6001174")]
    	[Address(RVA = "0x58B660", Offset = "0x58A060", VA = "0x18058B660")]
    	public ManagedTextDocument Parse(string text)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001175")]
    	[Address(RVA = "0x58BA70", Offset = "0x58A470", VA = "0x18058BA70")]
    	private ManagedTextRecord[] SplitScriptRecord(ManagedTextRecord record)
    	{
    		return null;
    	}
    }
}
