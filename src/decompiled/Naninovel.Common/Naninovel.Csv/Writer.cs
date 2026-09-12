using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel.Csv;

[Token(Token = "0x20000C3")]
public class Writer
{
	[Token(Token = "0x20000C4")]
	public class Options
	{
		[Token(Token = "0x170000EE")]
		public bool QuoteAll
		{
			[Token(Token = "0x60003F4")]
			[Address(RVA = "0x44D320", Offset = "0x44BD20", VA = "0x18044D320")]
			[CompilerGenerated]
			get
			{
				return default(bool);
			}
			[Token(Token = "0x60003F5")]
			[Address(RVA = "0x30C130", Offset = "0x30AB30", VA = "0x18030C130")]
			[CompilerGenerated]
			set
			{
			}
		}

		[Token(Token = "0x170000EF")]
		public bool Trim
		{
			[Token(Token = "0x60003F6")]
			[Address(RVA = "0x542940", Offset = "0x541340", VA = "0x180542940")]
			[CompilerGenerated]
			get
			{
				return default(bool);
			}
			[Token(Token = "0x60003F7")]
			[Address(RVA = "0x542950", Offset = "0x541350", VA = "0x180542950")]
			[CompilerGenerated]
			set
			{
			}
		}

		[Token(Token = "0x60003F8")]
		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
		public Options()
		{
		}
	}

	[Token(Token = "0x400023C")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
	[CompilerGenerated]
	private TextWriter _003Cwriter_003EP;

	[Token(Token = "0x400023D")]
	private const char delimiter = ',';

	[Token(Token = "0x400023E")]
	private const string quote = "\"";

	[Token(Token = "0x400023F")]
	private const string doubleQuote = "\"\"";

	[Token(Token = "0x4000240")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private static readonly char[] quoted;

	[Token(Token = "0x4000241")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
	private readonly Options options;

	[Token(Token = "0x4000242")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
	private int recordFieldCount;

	[Token(Token = "0x60003F0")]
	[Address(RVA = "0x11033C0", Offset = "0x1101DC0", VA = "0x1811033C0")]
	public Writer(TextWriter writer, [Optional] Options? options)
	{
	}

	[Token(Token = "0x60003F1")]
	[Address(RVA = "0x11034C0", Offset = "0x1101EC0", VA = "0x1811034C0")]
	public void WriteField(string field)
	{
	}

	[Token(Token = "0x60003F2")]
	[Address(RVA = "0x11036C0", Offset = "0x11020C0", VA = "0x1811036C0")]
	public void NextRecord()
	{
	}
}
