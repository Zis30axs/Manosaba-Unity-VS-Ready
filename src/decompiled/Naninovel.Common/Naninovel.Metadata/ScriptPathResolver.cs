using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel.Metadata;

[Token(Token = "0x2000081")]
public class ScriptPathResolver
{
	[Token(Token = "0x40001B2")]
	[FieldOffset(Offset = "0x10")]
	private readonly Dictionary<(string RootUri, string FileUri), string> memo;

	[Token(Token = "0x40001B3")]
	[FieldOffset(Offset = "0x18")]
	private string rootUri;

	[Token(Token = "0x40001B4")]
	[FieldOffset(Offset = "0x20")]
	private string rootPrefix;

	[Token(Token = "0x170000BF")]
	public string RootUri
	{
		[Token(Token = "0x60002E6")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
		get
		{
			return null;
		}
		[Token(Token = "0x60002E7")]
		[Address(RVA = "0x10EF650", Offset = "0x10EE050", VA = "0x1810EF650")]
		set
		{
		}
	}

	[Token(Token = "0x60002E8")]
	[Address(RVA = "0x10EF660", Offset = "0x10EE060", VA = "0x1810EF660")]
	public string Resolve(string fileUri)
	{
		return null;
	}

	[Token(Token = "0x60002E9")]
	[Address(RVA = "0x10EFD60", Offset = "0x10EE760", VA = "0x1810EFD60")]
	private static string FormatUri(string content)
	{
		return null;
	}

	[Token(Token = "0x60002EA")]
	[Address(RVA = "0x10EFDD0", Offset = "0x10EE7D0", VA = "0x1810EFDD0")]
	private void SetRootUri(string uri)
	{
	}

	[Token(Token = "0x60002EB")]
	[Address(RVA = "0x10EFFD0", Offset = "0x10EE9D0", VA = "0x1810EFFD0")]
	public ScriptPathResolver()
	{
	}
}
