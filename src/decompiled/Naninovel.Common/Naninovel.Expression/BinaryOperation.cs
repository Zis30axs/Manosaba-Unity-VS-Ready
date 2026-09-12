using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Expression;

[Token(Token = "0x20000A8")]
public class BinaryOperation : IExpression
{
	[Token(Token = "0x170000E0")]
	public IBinaryOperator Operator
	{
		[Token(Token = "0x60003AB")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170000E1")]
	public IExpression Lhs
	{
		[Token(Token = "0x60003AC")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170000E2")]
	public IExpression Rhs
	{
		[Token(Token = "0x60003AD")]
		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60003AA")]
	[Address(RVA = "0x38F3B0", Offset = "0x38DDB0", VA = "0x18038F3B0")]
	public BinaryOperation(IBinaryOperator op, IExpression lhs, IExpression rhs)
	{
	}
}
