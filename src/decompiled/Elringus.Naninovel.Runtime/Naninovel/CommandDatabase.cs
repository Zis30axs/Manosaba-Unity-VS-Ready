using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x2000311")]
internal static class CommandDatabase
{
	[Token(Token = "0x4000B90")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private static Dictionary<string, MethodInfo> methodInfoCache;

	[Token(Token = "0x170001F1")]
	public static IReadOnlyDictionary<string, MethodInfo> Registered
	{
		[Token(Token = "0x6000E9E")]
		[Address(RVA = "0x5571E0", Offset = "0x555BE0", VA = "0x1805571E0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000E9F")]
	[Address(RVA = "0x557220", Offset = "0x555C20", VA = "0x180557220")]
	public static void ExecuteCommand(string methodName, params string[] args)
	{
	}

	[Token(Token = "0x6000EA0")]
	[Address(RVA = "0x557680", Offset = "0x556080", VA = "0x180557680")]
	internal static void RegisterCommands([Optional] Dictionary<string, MethodInfo> commands)
	{
	}
}
