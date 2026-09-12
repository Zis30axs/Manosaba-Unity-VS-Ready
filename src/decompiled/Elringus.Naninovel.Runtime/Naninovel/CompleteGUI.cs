using System;
using System.Collections.Generic;
using System.Reflection;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x2000313")]
internal static class CompleteGUI
{
	[Token(Token = "0x2000314")]
	private struct Option : IEquatable<Option>
	{
		[Token(Token = "0x4000B9A")]
		[FieldOffset(Offset = "0x0")]
		public string Label;

		[Token(Token = "0x4000B9B")]
		[FieldOffset(Offset = "0x8")]
		public string MethodName;

		[Token(Token = "0x6000EAF")]
		[Address(RVA = "0x457440", Offset = "0x455E40", VA = "0x180457440", Slot = "4")]
		public bool Equals(Option other)
		{
			return default(bool);
		}

		[Token(Token = "0x6000EB0")]
		[Address(RVA = "0x5592B0", Offset = "0x557CB0", VA = "0x1805592B0", Slot = "0")]
		public override bool Equals(object obj)
		{
			return default(bool);
		}

		[Token(Token = "0x6000EB1")]
		[Address(RVA = "0x559350", Offset = "0x557D50", VA = "0x180559350", Slot = "2")]
		public override int GetHashCode()
		{
			return default(int);
		}
	}

	[Token(Token = "0x4000B97")]
	[FieldOffset(Offset = "0x0")]
	private static readonly List<Option> options;

	[Token(Token = "0x4000B98")]
	[FieldOffset(Offset = "0x8")]
	private static string cachedInput;

	[Token(Token = "0x4000B99")]
	[FieldOffset(Offset = "0x10")]
	private static float maxWidth;

	[Token(Token = "0x6000EA8")]
	[Address(RVA = "0x557FC0", Offset = "0x5569C0", VA = "0x180557FC0")]
	public static void Draw(ref string input)
	{
	}

	[Token(Token = "0x6000EA9")]
	[Address(RVA = "0x558270", Offset = "0x556C70", VA = "0x180558270")]
	private static void RebuildOptions(string input)
	{
	}

	[Token(Token = "0x6000EAA")]
	[Address(RVA = "0x5587F0", Offset = "0x5571F0", VA = "0x1805587F0")]
	private static string ExtractMethodName(string input)
	{
		return null;
	}

	[Token(Token = "0x6000EAB")]
	[Address(RVA = "0x558860", Offset = "0x557260", VA = "0x180558860")]
	private static Option BuildOption(string methodName, MethodInfo info)
	{
		return default(Option);
	}

	[Token(Token = "0x6000EAC")]
	[Address(RVA = "0x558B20", Offset = "0x557520", VA = "0x180558B20")]
	private static void DrawOption(Option option, ref string input)
	{
	}

	[Token(Token = "0x6000EAD")]
	[Address(RVA = "0x558FF0", Offset = "0x5579F0", VA = "0x180558FF0")]
	private static void HandleInput(ref string input)
	{
	}
}
