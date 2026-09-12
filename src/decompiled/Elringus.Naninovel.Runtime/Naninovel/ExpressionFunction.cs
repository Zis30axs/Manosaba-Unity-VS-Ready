using System.Reflection;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x2000360")]
public readonly struct ExpressionFunction
{
	[Token(Token = "0x4000C86")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	public readonly string Id;

	[Token(Token = "0x4000C87")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
	public readonly MethodInfo Method;

	[Token(Token = "0x4000C88")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	public readonly string Summary;

	[Token(Token = "0x4000C89")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
	public readonly string Remarks;

	[Token(Token = "0x4000C8A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	public readonly string[] Examples;

	[Token(Token = "0x600102B")]
	[Address(RVA = "0x571CE0", Offset = "0x5706E0", VA = "0x180571CE0")]
	public ExpressionFunction(string id, MethodInfo method, [Optional] string summary, [Optional] string remarks, [Optional] string[] examples)
	{
	}
}
