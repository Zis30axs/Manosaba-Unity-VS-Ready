using Il2CppDummyDll;

namespace Naninovel.Expression
{
    [Token(Token = "0x2000098")]
    public readonly struct ExpressionRange
    {
    	[Token(Token = "0x40001EE")]
    	[FieldOffset(Offset = "0x0")]
    	public readonly IExpression Expression;

    	[Token(Token = "0x40001EF")]
    	[FieldOffset(Offset = "0x8")]
    	public readonly int Index;

    	[Token(Token = "0x40001F0")]
    	[FieldOffset(Offset = "0xC")]
    	public readonly int Length;

    	[Token(Token = "0x600035D")]
    	[Address(RVA = "0x5BD730", Offset = "0x5BC130", VA = "0x1805BD730")]
    	public ExpressionRange(IExpression exp, int index, int length)
    	{ this = default; }
    }
}
