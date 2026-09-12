using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Utilities;

[Token(Token = "0x200001A")]
public static class TextUtils
{
	[Token(Token = "0x200001F")]
	[CompilerGenerated]
	private sealed class _003CIterateLines_003Ed__3
{
		[Token(Token = "0x4000033")]
		[FieldOffset(Offset = "0x10")]
		private int _003C_003E1__state;

		[Token(Token = "0x4000034")]
		[FieldOffset(Offset = "0x18")]
		private string _003C_003E2__current;

		[Token(Token = "0x4000035")]
		[FieldOffset(Offset = "0x20")]
		private int _003C_003El__initialThreadId;

		[Token(Token = "0x4000036")]
		[FieldOffset(Offset = "0x28")]
		private string str;

		[Token(Token = "0x4000037")]
		[FieldOffset(Offset = "0x30")]
		public string _003C_003E3__str;

		[Token(Token = "0x4000038")]
		[FieldOffset(Offset = "0x38")]
		private StringReader _003Creader_003E5__2;

		[Token(Token = "0x17000010")]
		private string System_002ECollections_002EGeneric_002EIEnumerator_003CSystem_002EString_003E_002ECurrent
		{
			[Token(Token = "0x6000081")]
			[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "6")]
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x17000011")]
		private object System_002ECollections_002EIEnumerator_002ECurrent
		{
			[Token(Token = "0x6000083")]
			[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "9")]
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x600007D")]
		[Address(RVA = "0x5518C0", Offset = "0x5502C0", VA = "0x1805518C0")]
		[DebuggerHidden]
		public _003CIterateLines_003Ed__3(int _003C_003E1__state)
		{
		}

		[Token(Token = "0x600007E")]
		[Address(RVA = "0x10D28A0", Offset = "0x10D12A0", VA = "0x1810D28A0", Slot = "7")]
		[DebuggerHidden]
		private void System_002EIDisposable_002EDispose()
		{
		}

		[Token(Token = "0x600007F")]
		[Address(RVA = "0x10D2960", Offset = "0x10D1360", VA = "0x1810D2960", Slot = "8")]
		private bool MoveNext()
		{
			return default(bool);
		}

		[Token(Token = "0x6000080")]
		[Address(RVA = "0x10D2BA0", Offset = "0x10D15A0", VA = "0x1810D2BA0")]
		private void _003C_003Em__Finally1()
		{
		}

		[Token(Token = "0x6000082")]
		[Address(RVA = "0x10D2C00", Offset = "0x10D1600", VA = "0x1810D2C00", Slot = "10")]
		[DebuggerHidden]
		private void System_002ECollections_002EIEnumerator_002EReset()
		{
		}

		[Token(Token = "0x6000084")]
		[Address(RVA = "0x10D2C40", Offset = "0x10D1640", VA = "0x1810D2C40", Slot = "4")]
		[DebuggerHidden]
		private IEnumerator<string> System_002ECollections_002EGeneric_002EIEnumerable_003CSystem_002EString_003E_002EGetEnumerator()
		{
			return null;
		}

		[Token(Token = "0x6000085")]
		[Address(RVA = "0x10D2C40", Offset = "0x10D1640", VA = "0x1810D2C40", Slot = "5")]
		[DebuggerHidden]
		private IEnumerator System_002ECollections_002EIEnumerable_002EGetEnumerator()
		{
			return null;
		}
	}

	[Token(Token = "0x2000020")]
	[CompilerGenerated]
	private sealed class _003CIterateLinesIndexed_003Ed__4
{
		[Token(Token = "0x4000039")]
		[FieldOffset(Offset = "0x10")]
		private int _003C_003E1__state;

		[Token(Token = "0x400003A")]
		[FieldOffset(Offset = "0x18")]
		private (string line, int index) _003C_003E2__current;

		[Token(Token = "0x400003B")]
		[FieldOffset(Offset = "0x28")]
		private int _003C_003El__initialThreadId;

		[Token(Token = "0x400003C")]
		[FieldOffset(Offset = "0x30")]
		private string str;

		[Token(Token = "0x400003D")]
		[FieldOffset(Offset = "0x38")]
		public string _003C_003E3__str;

		[Token(Token = "0x400003E")]
		[FieldOffset(Offset = "0x40")]
		private StringReader _003Creader_003E5__2;

		[Token(Token = "0x400003F")]
		[FieldOffset(Offset = "0x48")]
		private int _003Cindex_003E5__3;

		[Token(Token = "0x17000012")]
		private (string, int) System_002ECollections_002EGeneric_002EIEnumerator_003CSystem_002EValueTuple_003CSystem_002EString_002CSystem_002EInt32_003E_003E_002ECurrent
		{
			[Token(Token = "0x600008A")]
			[Address(RVA = "0x331710", Offset = "0x330110", VA = "0x180331710", Slot = "6")]
			[DebuggerHidden]
			get
			{
				return default((string, int));
			}
		}

		[Token(Token = "0x17000013")]
		private object System_002ECollections_002EIEnumerator_002ECurrent
		{
			[Token(Token = "0x600008C")]
			[Address(RVA = "0x10D3140", Offset = "0x10D1B40", VA = "0x1810D3140", Slot = "9")]
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x6000086")]
		[Address(RVA = "0x10D2D20", Offset = "0x10D1720", VA = "0x1810D2D20")]
		[DebuggerHidden]
		public _003CIterateLinesIndexed_003Ed__4(int _003C_003E1__state)
		{
		}

		[Token(Token = "0x6000087")]
		[Address(RVA = "0x10D2D40", Offset = "0x10D1740", VA = "0x1810D2D40", Slot = "7")]
		[DebuggerHidden]
		private void System_002EIDisposable_002EDispose()
		{
		}

		[Token(Token = "0x6000088")]
		[Address(RVA = "0x10D2E00", Offset = "0x10D1800", VA = "0x1810D2E00", Slot = "8")]
		private bool MoveNext()
		{
			return default(bool);
		}

		[Token(Token = "0x6000089")]
		[Address(RVA = "0x10D30A0", Offset = "0x10D1AA0", VA = "0x1810D30A0")]
		private void _003C_003Em__Finally1()
		{
		}

		[Token(Token = "0x600008B")]
		[Address(RVA = "0x10D3100", Offset = "0x10D1B00", VA = "0x1810D3100", Slot = "10")]
		[DebuggerHidden]
		private void System_002ECollections_002EIEnumerator_002EReset()
		{
		}

		[Token(Token = "0x600008D")]
		[Address(RVA = "0x10D3190", Offset = "0x10D1B90", VA = "0x1810D3190", Slot = "4")]
		[DebuggerHidden]
		private IEnumerator<(string, int)> System_002ECollections_002EGeneric_002EIEnumerable_003CSystem_002EValueTuple_003CSystem_002EString_002CSystem_002EInt32_003E_003E_002EGetEnumerator()
		{
			return null;
		}

		[Token(Token = "0x600008E")]
		[Address(RVA = "0x10D3190", Offset = "0x10D1B90", VA = "0x1810D3190", Slot = "5")]
		[DebuggerHidden]
		private IEnumerator System_002ECollections_002EIEnumerable_002EGetEnumerator()
		{
			return null;
		}
	}

	[Token(Token = "0x4000024")]
	[FieldOffset(Offset = "0x0")]
	private static readonly string[] breaks;

	[Token(Token = "0x4000025")]
	[FieldOffset(Offset = "0x8")]
	private static readonly char[] junk;

	[Token(Token = "0x600005E")]
	[Address(RVA = "0x10D0A30", Offset = "0x10CF430", VA = "0x1810D0A30")]
	public static string[] SplitLines(this string str, StringSplitOptions options = StringSplitOptions.None)
	{
		return null;
	}

	[Token(Token = "0x600005F")]
	[Address(RVA = "0x10D0AC0", Offset = "0x10CF4C0", VA = "0x1810D0AC0")]
	[IteratorStateMachine(typeof(_003CIterateLines_003Ed__3))]
	public static IEnumerable<string> IterateLines(this string str)
	{
		return null;
	}

	[Token(Token = "0x6000060")]
	[Address(RVA = "0x10D0B70", Offset = "0x10CF570", VA = "0x1810D0B70")]
	[IteratorStateMachine(typeof(_003CIterateLinesIndexed_003Ed__4))]
	public static IEnumerable<(string, int)> IterateLinesIndexed(this string str)
	{
		return null;
	}

	[Token(Token = "0x6000061")]
	[Address(RVA = "0x10D0C20", Offset = "0x10CF620", VA = "0x1810D0C20")]
	public static string TrimJunk(this string str)
	{
		return null;
	}

	[Token(Token = "0x6000062")]
	[Address(RVA = "0x54F740", Offset = "0x54E140", VA = "0x18054F740")]
	public static bool EndsWithOrdinal(this string str, string match)
	{
		return default(bool);
	}

	[Token(Token = "0x6000063")]
	[Address(RVA = "0x54F850", Offset = "0x54E250", VA = "0x18054F850")]
	public static bool StartsWithOrdinal(this string str, string match)
	{
		return default(bool);
	}

	[Token(Token = "0x6000064")]
	[Address(RVA = "0x10D0CE0", Offset = "0x10CF6E0", VA = "0x1810D0CE0")]
	public static string GetBefore(this string str, string match, StringComparison comp = StringComparison.Ordinal)
	{
		return null;
	}

	[Token(Token = "0x6000065")]
	[Address(RVA = "0x10D0D80", Offset = "0x10CF780", VA = "0x1810D0D80")]
	public static string GetBeforeLast(this string str, string match, StringComparison comp = StringComparison.Ordinal)
	{
		return null;
	}

	[Token(Token = "0x6000066")]
	[Address(RVA = "0x10D0E30", Offset = "0x10CF830", VA = "0x1810D0E30")]
	public static string GetAfter(this string str, string match, StringComparison comp = StringComparison.Ordinal)
	{
		return null;
	}

	[Token(Token = "0x6000067")]
	[Address(RVA = "0x10D0EF0", Offset = "0x10CF8F0", VA = "0x1810D0EF0")]
	public static string GetAfterFirst(this string str, string match, StringComparison comp = StringComparison.Ordinal)
	{
		return null;
	}

	[Token(Token = "0x6000068")]
	[Address(RVA = "0x10D0FA0", Offset = "0x10CF9A0", VA = "0x1810D0FA0")]
	public static string FirstToLower(this string str)
	{
		return null;
	}

	[Token(Token = "0x6000069")]
	[Address(RVA = "0x10D10A0", Offset = "0x10CFAA0", VA = "0x1810D10A0")]
	public static string FirstToUpper(this string str)
	{
		return null;
	}

	[Token(Token = "0x600006A")]
	[Address(RVA = "0x10D11A0", Offset = "0x10CFBA0", VA = "0x1810D11A0")]
	public static string Sanitize(this string str, IReadOnlyCollection<char> invalid)
	{
		return null;
	}

	[Token(Token = "0x600006B")]
	[Address(RVA = "0x10D13E0", Offset = "0x10CFDE0", VA = "0x1810D13E0")]
	public static string Humanize(this string str)
	{
		return null;
	}

	[Token(Token = "0x600006C")]
	[Address(RVA = "0x10D17D0", Offset = "0x10D01D0", VA = "0x1810D17D0")]
	public static string ToPascalCase(this string str)
	{
		return null;
	}

	[Token(Token = "0x600006D")]
	[Address(RVA = "0x10D1B90", Offset = "0x10D0590", VA = "0x1810D1B90")]
	public static string ToKebabCase(this string str)
	{
		return null;
	}

	[Token(Token = "0x600006E")]
	[Address(RVA = "0x10D1E70", Offset = "0x10D0870", VA = "0x1810D1E70")]
	public static int GetLastCommonIndex(string a, string b)
	{
		return default(int);
	}

	[Token(Token = "0x600006F")]
	[Address(RVA = "0x10D1F60", Offset = "0x10D0960", VA = "0x1810D1F60")]
	public static int GetLastCommonIndexOf(string a, string b, char of)
	{
		return default(int);
	}
}
