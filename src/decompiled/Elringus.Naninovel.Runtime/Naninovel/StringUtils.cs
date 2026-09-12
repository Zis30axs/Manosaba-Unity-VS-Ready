using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;

namespace Naninovel;

[Token(Token = "0x2000302")]
public static class StringUtils
{
	[Token(Token = "0x2000303")]
	[CompilerGenerated]
	private sealed class _003CIterateLines_003Ed__16
{
		[Token(Token = "0x4000B6E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
		private int _003C_003E1__state;

		[Token(Token = "0x4000B6F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
		private string _003C_003E2__current;

		[Token(Token = "0x4000B70")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		private int _003C_003El__initialThreadId;

		[Token(Token = "0x4000B71")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private string input;

		[Token(Token = "0x4000B72")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		public string _003C_003E3__input;

		[Token(Token = "0x4000B73")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private StringReader _003Creader_003E5__2;

		[Token(Token = "0x170001EE")]
		private string System_002ECollections_002EGeneric_002EIEnumerator_003CSystem_002EString_003E_002ECurrent
		{
			[Token(Token = "0x6000E71")]
			[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "6")]
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x170001EF")]
		private object System_002ECollections_002EIEnumerator_002ECurrent
		{
			[Token(Token = "0x6000E73")]
			[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0", Slot = "9")]
			[DebuggerHidden]
			get
			{
				return null;
			}
		}

		[Token(Token = "0x6000E6D")]
		[Address(RVA = "0x5518C0", Offset = "0x5502C0", VA = "0x1805518C0")]
		[DebuggerHidden]
		public _003CIterateLines_003Ed__16(int _003C_003E1__state)
		{
		}

		[Token(Token = "0x6000E6E")]
		[Address(RVA = "0x5518E0", Offset = "0x5502E0", VA = "0x1805518E0", Slot = "7")]
		[DebuggerHidden]
		private void System_002EIDisposable_002EDispose()
		{
		}

		[Token(Token = "0x6000E6F")]
		[Address(RVA = "0x5519A0", Offset = "0x5503A0", VA = "0x1805519A0", Slot = "8")]
		private bool MoveNext()
		{
			return default(bool);
		}

		[Token(Token = "0x6000E70")]
		[Address(RVA = "0x551BD0", Offset = "0x5505D0", VA = "0x180551BD0")]
		private void _003C_003Em__Finally1()
		{
		}

		[Token(Token = "0x6000E72")]
		[Address(RVA = "0x551C30", Offset = "0x550630", VA = "0x180551C30", Slot = "10")]
		[DebuggerHidden]
		private void System_002ECollections_002EIEnumerator_002EReset()
		{
		}

		[Token(Token = "0x6000E74")]
		[Address(RVA = "0x551C70", Offset = "0x550670", VA = "0x180551C70", Slot = "4")]
		[DebuggerHidden]
		private IEnumerator<string> System_002ECollections_002EGeneric_002EIEnumerable_003CSystem_002EString_003E_002EGetEnumerator()
		{
			return null;
		}

		[Token(Token = "0x6000E75")]
		[Address(RVA = "0x551C70", Offset = "0x550670", VA = "0x180551C70", Slot = "5")]
		[DebuggerHidden]
		private IEnumerator System_002ECollections_002EIEnumerable_002EGetEnumerator()
		{
			return null;
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000304")]
	[CompilerGenerated]
	private struct _003CUnzipStringAsync_003Ed__29
{
		[Token(Token = "0x4000B74")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000B75")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<string> _003C_003Et__builder;

		[Token(Token = "0x4000B76")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public byte[] content;

		[Token(Token = "0x4000B77")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private MemoryStream _003CinputStream_003E5__2;

		[Token(Token = "0x4000B78")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private DeflateStream _003Cgzip_003E5__3;

		[Token(Token = "0x4000B79")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private StreamReader _003Creader_003E5__4;

		[Token(Token = "0x4000B7A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private object _003C_003E7__wrap4;

		[Token(Token = "0x4000B7B")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private int _003C_003E7__wrap5;

		[Token(Token = "0x4000B7C")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private string _003C_003E7__wrap6;

		[Token(Token = "0x4000B7D")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
		private TaskAwaiter<string> _003C_003Eu__1;

		[Token(Token = "0x4000B7E")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
		private ValueTaskAwaiter _003C_003Eu__2;

		[Token(Token = "0x6000E76")]
		[Address(RVA = "0x551D50", Offset = "0x550750", VA = "0x180551D50", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000E77")]
		[Address(RVA = "0x552C50", Offset = "0x551650", VA = "0x180552C50", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[StructLayout(3)]
	[Token(Token = "0x2000305")]
	[CompilerGenerated]
	private struct _003CZipStringAsync_003Ed__28
{
		[Token(Token = "0x4000B7F")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
		public int _003C_003E1__state;

		[Token(Token = "0x4000B80")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
		public AsyncUniTaskMethodBuilder<byte[]> _003C_003Et__builder;

		[Token(Token = "0x4000B81")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
		public string content;

		[Token(Token = "0x4000B82")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
		private MemoryStream _003Coutput_003E5__2;

		[Token(Token = "0x4000B83")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
		private DeflateStream _003Cgzip_003E5__3;

		[Token(Token = "0x4000B84")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
		private object _003C_003E7__wrap3;

		[Token(Token = "0x4000B85")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
		private int _003C_003E7__wrap4;

		[Token(Token = "0x4000B86")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
		private StreamWriter _003Cwriter_003E5__6;

		[Token(Token = "0x4000B87")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x50")]
		private object _003C_003E7__wrap6;

		[Token(Token = "0x4000B88")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x58")]
		private int _003C_003E7__wrap7;

		[Token(Token = "0x4000B89")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x60")]
		private TaskAwaiter _003C_003Eu__1;

		[Token(Token = "0x4000B8A")]
		[Il2CppDummyDll.FieldOffset(Offset = "0x68")]
		private ValueTaskAwaiter _003C_003Eu__2;

		[Token(Token = "0x6000E78")]
		[Address(RVA = "0x552C80", Offset = "0x551680", VA = "0x180552C80", Slot = "4")]
		private void MoveNext()
		{
		}

		[Token(Token = "0x6000E79")]
		[Address(RVA = "0x553B30", Offset = "0x552530", VA = "0x180553B30", Slot = "5")]
		[DebuggerHidden]
		private void SetStateMachine(IAsyncStateMachine stateMachine)
		{
		}
	}

	[Token(Token = "0x4000B6C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	public static readonly char[] NewLineChars;

	[Token(Token = "0x4000B6D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
	public static readonly string[] NewLineSymbols;

	[Token(Token = "0x6000E4E")]
	[Address(RVA = "0x54F4F0", Offset = "0x54DEF0", VA = "0x18054F4F0")]
	public static bool ContainsLineBreak(this string content)
	{
		return default(bool);
	}

	[Token(Token = "0x6000E4F")]
	[Address(RVA = "0x54F5C0", Offset = "0x54DFC0", VA = "0x18054F5C0")]
	public static bool EqualsFast(this string content, string comparedString)
	{
		return default(bool);
	}

	[Token(Token = "0x6000E50")]
	[Address(RVA = "0x54F660", Offset = "0x54E060", VA = "0x18054F660")]
	public static bool EqualsFastIgnoreCase(this string content, string comparedString)
	{
		return default(bool);
	}

	[Token(Token = "0x6000E51")]
	[Address(RVA = "0x54F740", Offset = "0x54E140", VA = "0x18054F740")]
	public static bool EndsWithFast(this string content, string match)
	{
		return default(bool);
	}

	[Token(Token = "0x6000E52")]
	[Address(RVA = "0x54F850", Offset = "0x54E250", VA = "0x18054F850")]
	public static bool StartsWithFast(this string content, string match)
	{
		return default(bool);
	}

	[Token(Token = "0x6000E53")]
	[Address(RVA = "0x54F950", Offset = "0x54E350", VA = "0x18054F950")]
	public static bool WrappedIn(this string content, string match, StringComparison comp = StringComparison.Ordinal)
	{
		return default(bool);
	}

	[Token(Token = "0x6000E54")]
	[Address(RVA = "0x54F9B0", Offset = "0x54E3B0", VA = "0x18054F9B0")]
	public static string TrySubset(string source, int startIndex, int endIndex)
	{
		return null;
	}

	[Token(Token = "0x6000E55")]
	[Address(RVA = "0x54FA80", Offset = "0x54E480", VA = "0x18054FA80")]
	public static string GetBetween(this string content, string startMatch, string endMatch, StringComparison comp = StringComparison.Ordinal)
	{
		return null;
	}

	[Token(Token = "0x6000E56")]
	[Address(RVA = "0x54FB50", Offset = "0x54E550", VA = "0x18054FB50")]
	public static string GetBetween(this string content, string match, StringComparison comp = StringComparison.Ordinal)
	{
		return null;
	}

	[Token(Token = "0x6000E57")]
	[Address(RVA = "0x54FC60", Offset = "0x54E660", VA = "0x18054FC60")]
	public static string GetBefore(this string content, string matchString, StringComparison comp = StringComparison.Ordinal)
	{
		return null;
	}

	[Token(Token = "0x6000E58")]
	[Address(RVA = "0x54FCE0", Offset = "0x54E6E0", VA = "0x18054FCE0")]
	public static string GetBeforeLast(this string content, string matchString, StringComparison comp = StringComparison.Ordinal)
	{
		return null;
	}

	[Token(Token = "0x6000E59")]
	[Address(RVA = "0x54FD70", Offset = "0x54E770", VA = "0x18054FD70")]
	public static string GetAfter(this string content, string matchString, StringComparison comp = StringComparison.Ordinal)
	{
		return null;
	}

	[Token(Token = "0x6000E5A")]
	[Address(RVA = "0x54FE30", Offset = "0x54E830", VA = "0x18054FE30")]
	public static string GetAfterFirst(this string content, string matchString, StringComparison comp = StringComparison.Ordinal)
	{
		return null;
	}

	[Token(Token = "0x6000E5B")]
	[Address(RVA = "0x54FEF0", Offset = "0x54E8F0", VA = "0x18054FEF0")]
	public static string[] SplitByNewLine(this string content, StringSplitOptions splitOptions = StringSplitOptions.None)
	{
		return null;
	}

	[Token(Token = "0x6000E5C")]
	[Address(RVA = "0x54FF90", Offset = "0x54E990", VA = "0x18054FF90")]
	[IteratorStateMachine(typeof(_003CIterateLines_003Ed__16))]
	public static IEnumerable<string> IterateLines(this string input)
	{
		return null;
	}

	[Token(Token = "0x6000E5D")]
	[Address(RVA = "0x550040", Offset = "0x54EA40", VA = "0x180550040")]
	public static string TrimEnd(this string source, string value)
	{
		return null;
	}

	[Token(Token = "0x6000E5E")]
	[Address(RVA = "0x5501B0", Offset = "0x54EBB0", VA = "0x1805501B0")]
	public static string Remove(this string source, char value)
	{
		return null;
	}

	[Token(Token = "0x6000E5F")]
	[Address(RVA = "0x550230", Offset = "0x54EC30", VA = "0x180550230")]
	public static string Remove(this string source, string value)
	{
		return null;
	}

	[Token(Token = "0x6000E60")]
	[Address(RVA = "0x550260", Offset = "0x54EC60", VA = "0x180550260")]
	public static bool IsNullEmptyOrWhiteSpace(string content)
	{
		return default(bool);
	}

	[Token(Token = "0x6000E61")]
	[Address(RVA = "0x5502D0", Offset = "0x54ECD0", VA = "0x1805502D0")]
	public static string TrimFull(this string source)
	{
		return null;
	}

	[Token(Token = "0x6000E62")]
	[Address(RVA = "0x5503A0", Offset = "0x54EDA0", VA = "0x1805503A0")]
	public static string FormatFileSize(double size, int unit = 0)
	{
		return null;
	}

	[Token(Token = "0x6000E63")]
	[Address(RVA = "0x550620", Offset = "0x54F020", VA = "0x180550620")]
	public static string InsertCamel(this string source, char insert = ' ', bool preserveAcronyms = true)
	{
		return null;
	}

	[Token(Token = "0x6000E64")]
	[Address(RVA = "0x5508F0", Offset = "0x54F2F0", VA = "0x1805508F0")]
	public static string FirstToLower(this string source)
	{
		return null;
	}

	[Token(Token = "0x6000E65")]
	[Address(RVA = "0x5509D0", Offset = "0x54F3D0", VA = "0x1805509D0")]
	public static string FirstToUpper(this string source)
	{
		return null;
	}

	[Token(Token = "0x6000E66")]
	[Address(RVA = "0x550AB0", Offset = "0x54F4B0", VA = "0x180550AB0")]
	public static byte[] ZipString(string content)
	{
		return null;
	}

	[Token(Token = "0x6000E67")]
	[Address(RVA = "0x550D60", Offset = "0x54F760", VA = "0x180550D60")]
	public static string UnzipString(byte[] content)
	{
		return null;
	}

	[Token(Token = "0x6000E68")]
	[Address(RVA = "0x550FF0", Offset = "0x54F9F0", VA = "0x180550FF0")]
	[AsyncStateMachine(typeof(_003CZipStringAsync_003Ed__28))]
	public static UniTask<byte[]> ZipStringAsync(string content)
	{
		return default(UniTask<byte[]>);
	}

	[Token(Token = "0x6000E69")]
	[Address(RVA = "0x5511A0", Offset = "0x54FBA0", VA = "0x1805511A0")]
	[AsyncStateMachine(typeof(_003CUnzipStringAsync_003Ed__29))]
	public static UniTask<string> UnzipStringAsync(byte[] content)
	{
		return default(UniTask<string>);
	}

	[Token(Token = "0x6000E6A")]
	[Address(RVA = "0x551350", Offset = "0x54FD50", VA = "0x180551350")]
	public static string BuildAssetLink(string path, [Optional] int? line)
	{
		return null;
	}
}
