using System;
using Il2CppDummyDll;

namespace Naninovel.Expression;

[Token(Token = "0x20000A6")]
public static class OperandExtensions
{
	[Token(Token = "0x60003A5")]
	public static T GetValue<T>(this IOperand op) where T : notnull
	{
		return default(T);
	}

	[Token(Token = "0x60003A6")]
	[Address(RVA = "0x10FF8E0", Offset = "0x10FE2E0", VA = "0x1810FF8E0")]
	public static object GetValue(this IOperand op, Type type)
	{
		return null;
	}
}
