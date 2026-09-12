using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel.Csv;

[Token(Token = "0x20000C0")]
public sealed class Reader
{
	[Token(Token = "0x20000C1")]
	public class Options
	{
		[Token(Token = "0x170000EC")]
		public int BufferSize
		{
			[Token(Token = "0x60003EB")]
			[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650")]
			[CompilerGenerated]
			get
			{
				return default(int);
			}
			[Token(Token = "0x60003EC")]
			[Address(RVA = "0x397FF0", Offset = "0x3969F0", VA = "0x180397FF0")]
			[CompilerGenerated]
			set
			{
			}
		}

		[Token(Token = "0x170000ED")]
		public bool TrimFields
		{
			[Token(Token = "0x60003ED")]
			[Address(RVA = "0xF74F20", Offset = "0xF73920", VA = "0x180F74F20")]
			[CompilerGenerated]
			get
			{
				return default(bool);
			}
			[Token(Token = "0x60003EE")]
			[Address(RVA = "0xF4DD30", Offset = "0xF4C730", VA = "0x180F4DD30")]
			[CompilerGenerated]
			set
			{
			}
		}

		[Token(Token = "0x60003EF")]
		[Address(RVA = "0x11033B0", Offset = "0x1101DB0", VA = "0x1811033B0")]
		public Options()
		{
		}
	}

	[Token(Token = "0x400022A")]
	private const char delimiter = ',';

	[Token(Token = "0x400022B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
	private readonly TextReader reader;

	[Token(Token = "0x400022C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	private readonly Options options;

	[Token(Token = "0x400022D")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
	private readonly List<Field> fields;

	[Token(Token = "0x400022E")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
	private readonly char[] buffer;

	[Token(Token = "0x400022F")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
	private readonly int bufferLength;

	[Token(Token = "0x4000230")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x3C")]
	private readonly int bufferThreshold;

	[Token(Token = "0x4000231")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
	private int lineStartPos;

	[Token(Token = "0x4000232")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x44")]
	private int actualBufferLen;

	[Token(Token = "0x4000233")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x48")]
	private int linesRead;

	[Token(Token = "0x170000EA")]
	public int FieldsCount
	{
		[Token(Token = "0x60003E0")]
		[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650")]
		[CompilerGenerated]
		get
		{
			return default(int);
		}
		[Token(Token = "0x60003E1")]
		[Address(RVA = "0x397FF0", Offset = "0x3969F0", VA = "0x180397FF0")]
		[CompilerGenerated]
		private set
		{
		}
	}

	[Token(Token = "0x170000EB")]
	public string this[int idx]
	{
		[Token(Token = "0x60003E2")]
		[Address(RVA = "0x11026D0", Offset = "0x11010D0", VA = "0x1811026D0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60003E3")]
	[Address(RVA = "0x1102750", Offset = "0x1101150", VA = "0x181102750")]
	public Reader(TextReader reader, [Optional] Options? options)
	{
	}

	[Token(Token = "0x60003E4")]
	[Address(RVA = "0x11029D0", Offset = "0x11013D0", VA = "0x1811029D0")]
	public bool ReadRow()
	{
		return default(bool);
	}

	[Token(Token = "0x60003E5")]
	[Address(RVA = "0x1102CE0", Offset = "0x11016E0", VA = "0x181102CE0")]
	private int ReadBlockAndCheckEof(char[] buffer, int start, int len, ref bool eof)
	{
		return default(int);
	}

	[Token(Token = "0x60003E6")]
	[Address(RVA = "0x1102D30", Offset = "0x1101730", VA = "0x181102D30")]
	private bool FillBuffer()
	{
		return default(bool);
	}

	[Token(Token = "0x60003E7")]
	[Address(RVA = "0x1102E80", Offset = "0x1101880", VA = "0x181102E80")]
	private string GetLineTooLongMsg()
	{
		return null;
	}

	[Token(Token = "0x60003E8")]
	[Address(RVA = "0x1102F50", Offset = "0x1101950", VA = "0x181102F50")]
	private int ReadQuotedFieldToEnd(int start, int maxPos, bool eof, ref int escapedQuotesCount)
	{
		return default(int);
	}

	[Token(Token = "0x60003E9")]
	[Address(RVA = "0x1103050", Offset = "0x1101A50", VA = "0x181103050")]
	private Field GetOrAddField(int startIdx)
	{
		return null;
	}
}
