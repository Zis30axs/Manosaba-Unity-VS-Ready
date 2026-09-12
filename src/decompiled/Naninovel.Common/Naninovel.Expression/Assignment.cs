using Il2CppDummyDll;

namespace Naninovel.Expression;

[Token(Token = "0x2000096")]
public readonly struct Assignment
{
	[Token(Token = "0x40001EA")]
	[FieldOffset(Offset = "0x0")]
	public readonly string Variable;

	[Token(Token = "0x40001EB")]
	[FieldOffset(Offset = "0x8")]
	public readonly IExpression Expression;

	[Token(Token = "0x6000355")]
	[Address(RVA = "0x42B240", Offset = "0x429C40", VA = "0x18042B240")]
	public Assignment(string variable, IExpression expression)
	{
	}
}
