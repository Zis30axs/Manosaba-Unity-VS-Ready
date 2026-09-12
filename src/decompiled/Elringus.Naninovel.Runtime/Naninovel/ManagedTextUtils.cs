using System.Reflection;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.ManagedText;

namespace Naninovel;

[Token(Token = "0x20003AF")]
public static class ManagedTextUtils
{
	[Token(Token = "0x4000DB6")]
	public const BindingFlags ManagedFieldBindings = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;

	[Token(Token = "0x4000DB7")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private static readonly InlineManagedTextParser inlineParser;

	[Token(Token = "0x4000DB8")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
	private static readonly MultilineManagedTextParser multilineParser;

	[Token(Token = "0x60011C8")]
	[Address(RVA = "0x594170", Offset = "0x592B70", VA = "0x180594170")]
	public static ManagedTextDocument Parse(string text, [Optional] string documentPath, [Optional] string name)
	{
		return null;
	}

	[Token(Token = "0x60011C9")]
	[Address(RVA = "0x594270", Offset = "0x592C70", VA = "0x180594270")]
	public static ManagedTextDocument ParseMultiline(string text)
	{
		return null;
	}

	[Token(Token = "0x60011CA")]
	[Address(RVA = "0x5942E0", Offset = "0x592CE0", VA = "0x1805942E0")]
	public static ManagedTextDocument ParseInline(string text, [Optional] string name)
	{
		return null;
	}

	[Token(Token = "0x60011CB")]
	[Address(RVA = "0x594410", Offset = "0x592E10", VA = "0x180594410")]
	public static string Serialize(ManagedTextDocument document, string documentPath, int spacing = 1)
	{
		return null;
	}

	[Token(Token = "0x60011CC")]
	[Address(RVA = "0x5945B0", Offset = "0x592FB0", VA = "0x1805945B0")]
	public static string SerializeMultiline(ManagedTextDocument document, int spacing = 1)
	{
		return null;
	}

	[Token(Token = "0x60011CD")]
	[Address(RVA = "0x5946B0", Offset = "0x5930B0", VA = "0x1805946B0")]
	public static string SerializeInline(ManagedTextDocument document, int spacing = 1)
	{
		return null;
	}

	[Token(Token = "0x60011CE")]
	[Address(RVA = "0x5947B0", Offset = "0x5931B0", VA = "0x1805947B0")]
	public static string ResolveScriptL10nPath(string scriptPath)
	{
		return null;
	}
}
