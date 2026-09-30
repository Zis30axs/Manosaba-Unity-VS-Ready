using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Parsing;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x20003D9")]
    public static class Compiler
    {
    	[Token(Token = "0x4000E6F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
    	private static ScriptParser scriptParser;

    	[Token(Token = "0x4000E70")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
    	private static IErrorHandler errors;

    	[Token(Token = "0x1700027F")]
    	public static Syntax Syntax
    	{
    		[Token(Token = "0x600128B")]
    		[Address(RVA = "0x5A0990", Offset = "0x59F390", VA = "0x1805A0990")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x600128C")]
    		[Address(RVA = "0x5A09D0", Offset = "0x59F3D0", VA = "0x1805A09D0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000280")]
    	public static string GlobalVariablePrefix
    	{
    		[Token(Token = "0x600128D")]
    		[Address(RVA = "0x5A0A70", Offset = "0x59F470", VA = "0x1805A0A70")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x600128E")]
    		[Address(RVA = "0x5A0AB0", Offset = "0x59F4B0", VA = "0x1805A0AB0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000281")]
    	public static string ScriptConstantPrefix
    	{
    		[Token(Token = "0x600128F")]
    		[Address(RVA = "0x5A0B50", Offset = "0x59F550", VA = "0x1805A0B50")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6001290")]
    		[Address(RVA = "0x5A0B90", Offset = "0x59F590", VA = "0x1805A0B90")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000282")]
    	public static NamedValueParser NamedValueParser
    	{
    		[Token(Token = "0x6001291")]
    		[Address(RVA = "0x5A0C30", Offset = "0x59F630", VA = "0x1805A0C30")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6001292")]
    		[Address(RVA = "0x5A0C70", Offset = "0x59F670", VA = "0x1805A0C70")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000283")]
    	public static ListValueParser ListValueParser
    	{
    		[Token(Token = "0x6001293")]
    		[Address(RVA = "0x5A0D10", Offset = "0x59F710", VA = "0x1805A0D10")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6001294")]
    		[Address(RVA = "0x5A0D50", Offset = "0x59F750", VA = "0x1805A0D50")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000284")]
    	public static ScriptSerializer ScriptSerializer
    	{
    		[Token(Token = "0x6001295")]
    		[Address(RVA = "0x5A0DF0", Offset = "0x59F7F0", VA = "0x1805A0DF0")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6001296")]
    		[Address(RVA = "0x5A0E30", Offset = "0x59F830", VA = "0x1805A0E30")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000285")]
    	public static IScriptParser ScriptAssetParser
    	{
    		[Token(Token = "0x6001297")]
    		[Address(RVA = "0x5A0ED0", Offset = "0x59F8D0", VA = "0x1805A0ED0")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x6001298")]
    		[Address(RVA = "0x5A0F10", Offset = "0x59F910", VA = "0x1805A0F10")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000286")]
    	public static ScriptAssetSerializer ScriptAssetSerializer
    	{
    		[Token(Token = "0x6001299")]
    		[Address(RVA = "0x5A0FB0", Offset = "0x59F9B0", VA = "0x1805A0FB0")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x600129A")]
    		[Address(RVA = "0x5A0FF0", Offset = "0x59F9F0", VA = "0x1805A0FF0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000287")]
    	public static Dictionary<string, CommandLocalization> Commands
    	{
    		[Token(Token = "0x600129B")]
    		[Address(RVA = "0x5A1090", Offset = "0x59FA90", VA = "0x1805A1090")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x600129C")]
    		[Address(RVA = "0x5A10D0", Offset = "0x59FAD0", VA = "0x1805A10D0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000288")]
    	public static Dictionary<string, FunctionLocalization> Functions
    	{
    		[Token(Token = "0x600129D")]
    		[Address(RVA = "0x5A1170", Offset = "0x59FB70", VA = "0x1805A1170")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x600129E")]
    		[Address(RVA = "0x5A11B0", Offset = "0x59FBB0", VA = "0x1805A11B0")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x17000289")]
    	public static Dictionary<string, ConstantLocalization> Constants
    	{
    		[Token(Token = "0x600129F")]
    		[Address(RVA = "0x5A1250", Offset = "0x59FC50", VA = "0x1805A1250")]
    		[CompilerGenerated]
    		get
    		{
    			return null;
    		}
    		[Token(Token = "0x60012A0")]
    		[Address(RVA = "0x5A1290", Offset = "0x59FC90", VA = "0x1805A1290")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x60012A1")]
    	[Address(RVA = "0x5A1330", Offset = "0x59FD30", VA = "0x1805A1330")]
    	public static List<IScriptLine> ParseText(string scriptText, [Optional] IErrorHandler errors)
    	{
    		return null;
    	}

    	[Token(Token = "0x60012A2")]
    	[Address(RVA = "0x5A14E0", Offset = "0x59FEE0", VA = "0x1805A14E0")]
    	public static IScriptLine ParseLine(string lineText, [Optional] IErrorHandler errors)
    	{
    		return null;
    	}

    	[Token(Token = "0x60012A3")]
    	[Address(RVA = "0x5A15C0", Offset = "0x59FFC0", VA = "0x1805A15C0")]
    	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
    	public static void Initialize()
    	{
    	}

    	[Token(Token = "0x60012A4")]
    	[Address(RVA = "0x5A2870", Offset = "0x5A1270", VA = "0x1805A2870")]
    	private static IScriptParser CreateScriptParser(string typeName)
    	{
    		return null;
    	}
    }
}
