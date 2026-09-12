using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Expression;
using Naninovel.Parsing;

namespace Naninovel.Metadata;

[Token(Token = "0x200006F")]
public class ExpressionEvaluator
{
	[Token(Token = "0x2000070")]
	public struct Context
	{
		[Token(Token = "0x1700007B")]
		public Naninovel.Parsing.Command? Command
		{
			[Token(Token = "0x6000259")]
			[Address(RVA = "0x456310", Offset = "0x454D10", VA = "0x180456310")]
			[CompilerGenerated]
			readonly get
			{
				return null;
			}
			[Token(Token = "0x600025A")]
			[Address(RVA = "0x3912B0", Offset = "0x38FCB0", VA = "0x1803912B0")]
			[CompilerGenerated]
			set
			{
			}
		}

		[Token(Token = "0x1700007C")]
		public Naninovel.Expression.Function? Function
		{
			[Token(Token = "0x600025B")]
			[Address(RVA = "0x457050", Offset = "0x455A50", VA = "0x180457050")]
			[CompilerGenerated]
			readonly get
			{
				return null;
			}
			[Token(Token = "0x600025C")]
			[Address(RVA = "0xDF0FF0", Offset = "0xDEF9F0", VA = "0x180DF0FF0")]
			[CompilerGenerated]
			set
			{
			}
		}
	}

	[Token(Token = "0x4000155")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	[CompilerGenerated]
	private IMetadata _003Cmeta_003EP;

	[Token(Token = "0x4000156")]
	public const string EntryScript = "$EntryScript";

	[Token(Token = "0x4000157")]
	public const string TitleScript = "$TitleScript";

	[Token(Token = "0x4000158")]
	private const char expressionStartSymbol = '{';

	[Token(Token = "0x4000159")]
	private const char expressionEndSymbol = '}';

	[Token(Token = "0x400015A")]
	private const string concatSymbol = "+";

	[Token(Token = "0x400015B")]
	private const string nullCoalescingSymbol = "??";

	[Token(Token = "0x400015C")]
	private const char paramIdSymbol = ':';

	[Token(Token = "0x400015D")]
	private const char paramIndexStartSymbol = '[';

	[Token(Token = "0x400015E")]
	private const char paramIndexEndSymbol = ']';

	[Token(Token = "0x400015F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private static readonly string[] concatSeparator;

	[Token(Token = "0x4000160")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
	private static readonly string[] nullSeparator;

	[Token(Token = "0x4000161")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
	private readonly NamedValueParser namedParser;

	[Token(Token = "0x4000162")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	private Context ctx;

	[Token(Token = "0x600024E")]
	[Address(RVA = "0x10EB890", Offset = "0x10EA290", VA = "0x1810EB890")]
	public ExpressionEvaluator(IMetadata meta)
	{
	}

	[Token(Token = "0x600024F")]
	[Address(RVA = "0x10EB9F0", Offset = "0x10EA3F0", VA = "0x1810EB9F0")]
	public string Evaluate(string expression, [Optional] Context context)
	{
		return null;
	}

	[Token(Token = "0x6000250")]
	[Address(RVA = "0x10EBA70", Offset = "0x10EA470", VA = "0x1810EBA70")]
	public void Evaluate(string expression, IList<string> results, [Optional] Context context)
	{
	}

	[Token(Token = "0x6000251")]
	[Address(RVA = "0x10EBC00", Offset = "0x10EA600", VA = "0x1810EBC00")]
	private string EvaluatePart(string part)
	{
		return null;
	}

	[Token(Token = "0x6000252")]
	[Address(RVA = "0x10EBE00", Offset = "0x10EA800", VA = "0x1810EBE00")]
	private string EvaluateExpression(string expression)
	{
		return null;
	}

	[Token(Token = "0x6000253")]
	[Address(RVA = "0x10EBF00", Offset = "0x10EA900", VA = "0x1810EBF00")]
	private string EvaluateAtom(string atom)
	{
		return null;
	}

	[Token(Token = "0x6000254")]
	[Address(RVA = "0x10EC170", Offset = "0x10EAB70", VA = "0x1810EC170")]
	private string GetParamValue(string id, int? idx)
	{
		return null;
	}

	[Token(Token = "0x6000255")]
	[Address(RVA = "0x10EC1D0", Offset = "0x10EABD0", VA = "0x1810EC1D0")]
	private string GetParamValue(Naninovel.Parsing.Command cmd, string id, int? idx)
	{
		return null;
	}

	[Token(Token = "0x6000256")]
	[Address(RVA = "0x10EC520", Offset = "0x10EAF20", VA = "0x1810EC520")]
	private string GetParamValue(Naninovel.Expression.Function fn, string id, int? idx)
	{
		return null;
	}

	[Token(Token = "0x6000257")]
	[Address(RVA = "0x10EC9C0", Offset = "0x10EB3C0", VA = "0x1810EC9C0")]
	private string GetNamedValue(string raw, int idx)
	{
		return null;
	}
}
