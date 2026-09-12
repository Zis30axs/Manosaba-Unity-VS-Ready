using System.Runtime.InteropServices;
using Il2CppDummyDll;
using UnityEngine;

[Token(Token = "0x2000005")]
public static class ConsoleProDebug
{
	[Token(Token = "0x6000005")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	public static void Clear()
	{
	}

	[Token(Token = "0x6000006")]
	[Address(RVA = "0x30C140", Offset = "0x30AB40", VA = "0x18030C140")]
	public static void LogToFilter(string inLog, string inFilterName, [Optional] Object inContext)
	{
	}

	[Token(Token = "0x6000007")]
	[Address(RVA = "0x30C200", Offset = "0x30AC00", VA = "0x18030C200")]
	public static void LogAsType(string inLog, string inTypeName, [Optional] Object inContext)
	{
	}

	[Token(Token = "0x6000008")]
	[Address(RVA = "0x30C2C0", Offset = "0x30ACC0", VA = "0x18030C2C0")]
	public static void Watch(string inName, string inValue)
	{
	}

	[Token(Token = "0x6000009")]
	[Address(RVA = "0x30C410", Offset = "0x30AE10", VA = "0x18030C410")]
	public static void Search(string inText)
	{
	}
}
