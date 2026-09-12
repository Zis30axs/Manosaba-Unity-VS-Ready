using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Expression;

namespace Naninovel;

[Token(Token = "0x200035B")]
public static class ExpressionEvaluator
{
	[Token(Token = "0x4000C7A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private static readonly List<ParseDiagnostic> errors;

	[Token(Token = "0x4000C7B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
	private static ILookup<string, ExpressionFunction> fnByName;

	[Token(Token = "0x4000C7C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	private static Parser parser;

	[Token(Token = "0x4000C7D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
	private static Evaluator evaluator;

	[Token(Token = "0x6001016")]
	[Address(RVA = "0x56F280", Offset = "0x56DC80", VA = "0x18056F280")]
	public static void Initialize()
	{
	}

	[Token(Token = "0x6001017")]
	public static TResult Evaluate<TResult>(string text, [Optional] Action<string> onError)
	{
		return default(TResult);
	}

	[Token(Token = "0x6001018")]
	public static bool TryEvaluate<TResult>(string text, out TResult result, [Optional] Action<string> onError)
	{
		result = default;
		result = default;
		return default(bool);
	}

	[Token(Token = "0x6001019")]
	[Address(RVA = "0x56FD10", Offset = "0x56E710", VA = "0x18056FD10")]
	public static IOperand Evaluate(string text, [Optional] Action<string> onError)
	{
		return null;
	}

	[Token(Token = "0x600101A")]
	[Address(RVA = "0x5700B0", Offset = "0x56EAB0", VA = "0x1805700B0")]
	public static IOperand Evaluate(IExpression exp, [Optional] Action<string> onError)
	{
		return null;
	}

	[Token(Token = "0x600101B")]
	[Address(RVA = "0x5701E0", Offset = "0x56EBE0", VA = "0x1805701E0")]
	public static void ParseAssignments(string text, IList<Assignment> assignments, [Optional] Action<string> onError)
	{
	}

	[Token(Token = "0x600101C")]
	[Address(RVA = "0x570410", Offset = "0x56EE10", VA = "0x180570410")]
	private static void EnsureInitialized()
	{
	}

	[Token(Token = "0x600101D")]
	[Address(RVA = "0x570480", Offset = "0x56EE80", VA = "0x180570480")]
	private static IOperand ResolveVariable(string name)
	{
		return null;
	}

	[Token(Token = "0x600101E")]
	[Address(RVA = "0x5709C0", Offset = "0x56F3C0", VA = "0x1805709C0")]
	private static IOperand ResolveFunction(string name, IReadOnlyList<IOperand> args)
	{
		return null;
	}

	[Token(Token = "0x600101F")]
	[Address(RVA = "0x570E30", Offset = "0x56F830", VA = "0x180570E30")]
	private static object InvokeMethod(string name, object[] args, bool strictType)
	{
		return null;
	}

	[Token(Token = "0x6001020")]
	[Address(RVA = "0x571670", Offset = "0x570070", VA = "0x180571670")]
	private static IOperand ValueToOperand(object value)
	{
		return null;
	}
}
