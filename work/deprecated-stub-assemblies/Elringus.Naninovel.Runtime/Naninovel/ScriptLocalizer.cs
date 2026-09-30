using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.ManagedText;
using Naninovel.Parsing;

namespace Naninovel
{
    [Token(Token = "0x200039D")]
    public class ScriptLocalizer
    {
    	[Token(Token = "0x200039E")]
    	public class Options
    	{
    		[Token(Token = "0x17000260")]
    		public ISyntax Syntax
    		{
    			[Token(Token = "0x6001187")]
    			[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
    			[CompilerGenerated]
    			get
    			{
    				return null;
    			}
    			[Token(Token = "0x6001188")]
    			[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
    			[CompilerGenerated]
    			set
    			{
    			}
    		}

    		[Token(Token = "0x17000261")]
    		public bool Annotate
    		{
    			[Token(Token = "0x6001189")]
    			[Address(RVA = "0x44BDC0", Offset = "0x44A7C0", VA = "0x18044BDC0")]
    			[CompilerGenerated]
    			get
    			{
    				return default(bool);
    			}
    			[Token(Token = "0x600118A")]
    			[Address(RVA = "0x58DEE0", Offset = "0x58C8E0", VA = "0x18058DEE0")]
    			[CompilerGenerated]
    			set
    			{
    			}
    		}

    		[Token(Token = "0x17000262")]
    		public string AnnotationPrefix
    		{
    			[Token(Token = "0x600118B")]
    			[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
    			[CompilerGenerated]
    			get
    			{
    				return null;
    			}
    			[Token(Token = "0x600118C")]
    			[Address(RVA = "0x45E0F0", Offset = "0x45CAF0", VA = "0x18045E0F0")]
    			[CompilerGenerated]
    			set
    			{
    			}
    		}

    		[Token(Token = "0x17000263")]
    		public char Separator
    		{
    			[Token(Token = "0x600118D")]
    			[Address(RVA = "0x58DEF0", Offset = "0x58C8F0", VA = "0x18058DEF0")]
    			[CompilerGenerated]
    			get
    			{
    				return default(char);
    			}
    			[Token(Token = "0x600118E")]
    			[Address(RVA = "0x58DF00", Offset = "0x58C900", VA = "0x18058DF00")]
    			[CompilerGenerated]
    			set
    			{
    			}
    		}

    		[Token(Token = "0x17000264")]
    		public Action<string> OnUntranslated
    		{
    			[Token(Token = "0x600118F")]
    			[Address(RVA = "0x3786A0", Offset = "0x3770A0", VA = "0x1803786A0")]
    			[CompilerGenerated]
    			get
    			{
    				return null;
    			}
    			[Token(Token = "0x6001190")]
    			[Address(RVA = "0x58DF10", Offset = "0x58C910", VA = "0x18058DF10")]
    			[CompilerGenerated]
    			set
    			{
    			}
    		}

    		[Token(Token = "0x6001191")]
    		[Address(RVA = "0x58DF70", Offset = "0x58C970", VA = "0x18058DF70")]
    		public Options()
    		{
    		}
    	}

    	[Token(Token = "0x4000D6B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    	private readonly List<string> ids;

    	[Token(Token = "0x4000D6C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    	private readonly List<string> text;

    	[Token(Token = "0x4000D6D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	private readonly List<ManagedTextRecord> records;

    	[Token(Token = "0x4000D6E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	private readonly ScriptAssetSerializer serde;

    	[Token(Token = "0x4000D6F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	private readonly TextSplitter splitter;

    	[Token(Token = "0x4000D70")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	private readonly Options options;

    	[Token(Token = "0x4000D71")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    	private Script script;

    	[Token(Token = "0x4000D72")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
    	private ManagedTextDocument existing;

    	[Token(Token = "0x4000D73")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
    	private string prevComment;

    	[Token(Token = "0x600117A")]
    	[Address(RVA = "0x58BF50", Offset = "0x58A950", VA = "0x18058BF50")]
    	public ScriptLocalizer([Optional] Options options)
    	{
    	}

    	[Token(Token = "0x600117B")]
    	[Address(RVA = "0x58C2F0", Offset = "0x58ACF0", VA = "0x18058C2F0")]
    	public ManagedTextDocument Localize(Script script, [Optional] ManagedTextDocument existing)
    	{
    		return null;
    	}

    	[Token(Token = "0x600117C")]
    	[Address(RVA = "0x58C770", Offset = "0x58B170", VA = "0x18058C770")]
    	private void Reset(Script script, ManagedTextDocument existing)
    	{
    	}

    	[Token(Token = "0x600117D")]
    	[Address(RVA = "0x58C900", Offset = "0x58B300", VA = "0x18058C900")]
    	private void VisitLine(CommentScriptLine line)
    	{
    	}

    	[Token(Token = "0x600117E")]
    	[Address(RVA = "0x58CA80", Offset = "0x58B480", VA = "0x18058CA80")]
    	private void VisitLine(CommandScriptLine line)
    	{
    	}

    	[Token(Token = "0x600117F")]
    	[Address(RVA = "0x58CB60", Offset = "0x58B560", VA = "0x18058CB60")]
    	private void VisitLine(GenericTextScriptLine line)
    	{
    	}

    	[Token(Token = "0x6001180")]
    	[Address(RVA = "0x58CDD0", Offset = "0x58B7D0", VA = "0x18058CDD0")]
    	private void CollectLocalizableContent(Command command)
    	{
    	}

    	[Token(Token = "0x6001181")]
    	[Address(RVA = "0x58CEC0", Offset = "0x58B8C0", VA = "0x18058CEC0")]
    	private void CollectLocalizableContent(Command command, FieldInfo field)
    	{
    	}

    	[Token(Token = "0x6001182")]
    	[Address(RVA = "0x58D2E0", Offset = "0x58BCE0", VA = "0x18058D2E0")]
    	private void CollectLocalizableContent(string mapId)
    	{
    	}

    	[Token(Token = "0x6001183")]
    	[Address(RVA = "0x58D4A0", Offset = "0x58BEA0", VA = "0x18058D4A0")]
    	private void AppendRecord([Optional] string annotation)
    	{
    	}

    	[Token(Token = "0x6001184")]
    	[Address(RVA = "0x58D800", Offset = "0x58C200", VA = "0x18058D800")]
    	private string Annotate(ScriptLine line)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001185")]
    	[Address(RVA = "0x58DB40", Offset = "0x58C540", VA = "0x18058DB40")]
    	private string ResolveExisting(string key)
    	{
    		return null;
    	}

    	[Token(Token = "0x6001186")]
    	[Address(RVA = "0x58DC00", Offset = "0x58C600", VA = "0x18058DC00")]
    	private bool IsSimpleGenericLine(ScriptLine line)
    	{
    		return default(bool);
    	}
    }
}
