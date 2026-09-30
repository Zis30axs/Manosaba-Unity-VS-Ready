using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.ManagedText;

namespace Naninovel
{
    [Token(Token = "0x2000399")]
    public class ManagedTextLocalizer
    {
    	[Token(Token = "0x200039A")]
    	public class Options
    	{
    		[Token(Token = "0x1700025C")]
    		public bool Annotate
    		{
    			[Token(Token = "0x600116C")]
    			[Address(RVA = "0x44D320", Offset = "0x44BD20", VA = "0x18044D320")]
    			[CompilerGenerated]
    			get
    			{
    				return default(bool);
    			}
    			[Token(Token = "0x600116D")]
    			[Address(RVA = "0x30C130", Offset = "0x30AB30", VA = "0x18030C130")]
    			[CompilerGenerated]
    			set
    			{
    			}
    		}

    		[Token(Token = "0x1700025D")]
    		public string AnnotationPrefix
    		{
    			[Token(Token = "0x600116E")]
    			[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    			[CompilerGenerated]
    			get
    			{
    				return null;
    			}
    			[Token(Token = "0x600116F")]
    			[Address(RVA = "0x36C200", Offset = "0x36AC00", VA = "0x18036C200")]
    			[CompilerGenerated]
    			set
    			{
    			}
    		}

    		[Token(Token = "0x1700025E")]
    		public Action<string> OnUntranslated
    		{
    			[Token(Token = "0x6001170")]
    			[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
    			[CompilerGenerated]
    			get
    			{
    				return null;
    			}
    			[Token(Token = "0x6001171")]
    			[Address(RVA = "0x45E0F0", Offset = "0x45CAF0", VA = "0x18045E0F0")]
    			[CompilerGenerated]
    			set
    			{
    			}
    		}

    		[Token(Token = "0x6001172")]
    		[Address(RVA = "0x58B250", Offset = "0x589C50", VA = "0x18058B250")]
    		public Options()
    		{
    		}
    	}

    	[Token(Token = "0x4000D5D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    	private readonly List<ManagedTextRecord> records;

    	[Token(Token = "0x4000D5E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    	private readonly Options options;

    	[Token(Token = "0x4000D5F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	private ManagedTextDocument existing;

    	[Token(Token = "0x6001167")]
    	[Address(RVA = "0x58A9E0", Offset = "0x5893E0", VA = "0x18058A9E0")]
    	public ManagedTextLocalizer([Optional] Options options)
    	{
    	}

    	[Token(Token = "0x6001168")]
    	[Address(RVA = "0x58AB30", Offset = "0x589530", VA = "0x18058AB30")]
    	public ManagedTextDocument Localize(ManagedTextDocument source, [Optional] ManagedTextDocument existing)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001169")]
    	[Address(RVA = "0x58AE50", Offset = "0x589850", VA = "0x18058AE50")]
    	private void Reset(ManagedTextDocument existing)
    	{
    	}

    	[Token(Token = "0x600116A")]
    	[Address(RVA = "0x58AF00", Offset = "0x589900", VA = "0x18058AF00")]
    	private void AppendRecord(ManagedTextRecord record)
    	{
    	}

    	[Token(Token = "0x600116B")]
    	[Address(RVA = "0x58B190", Offset = "0x589B90", VA = "0x18058B190")]
    	private string ResolveExisting(string key)
    	{
    		return null;
    	}
    }
}
