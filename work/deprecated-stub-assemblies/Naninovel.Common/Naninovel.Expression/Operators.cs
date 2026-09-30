using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel.Expression
{
    [Token(Token = "0x200009A")]
    internal static class Operators
    {
    	[Token(Token = "0x40001F7")]
    	[FieldOffset(Offset = "0x0")]
    	public static readonly IReadOnlyDictionary<string, IBinaryOperator> Binary;

    	[Token(Token = "0x40001F8")]
    	[FieldOffset(Offset = "0x8")]
    	public static readonly IReadOnlyDictionary<string, IUnaryOperator> Unary;

    	[Token(Token = "0x40001F9")]
    	[FieldOffset(Offset = "0x10")]
    	private static readonly IReadOnlyDictionary<char, string> singleChar;

    	[Token(Token = "0x40001FA")]
    	[FieldOffset(Offset = "0x18")]
    	private static readonly IReadOnlyDictionary<(char c1, char c2), string> doubleChar;

    	[Token(Token = "0x600036E")]
    	[Address(RVA = "0x10FAAD0", Offset = "0x10F94D0", VA = "0x1810FAAD0")]
    	public static bool IsOperator(char c1, char c2, out string op)
    	{
    		op = default;
    		op = default;
    		return default(bool);
    	}

    	[Token(Token = "0x600036F")]
    	[Address(RVA = "0x10FADD0", Offset = "0x10F97D0", VA = "0x1810FADD0")]
    	private static Dictionary<char, string> BuildSingleChars(params IEnumerable<string>[] collections)
    	{
    		return null;
    	}

    	[Token(Token = "0x6000370")]
    	[Address(RVA = "0x10FB030", Offset = "0x10F9A30", VA = "0x1810FB030")]
    	private static Dictionary<(char, char), string> BuildDoubleChars(params IEnumerable<string>[] collections)
    	{
    		return null;
    	}
    }
}
