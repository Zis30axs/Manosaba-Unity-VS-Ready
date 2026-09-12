using System;
using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x20003A4")]
[AttributeUsage(AttributeTargets.Field)]
public sealed class ManagedTextAttribute : Attribute
{
	[Token(Token = "0x17000266")]
	public string DocumentPath
	{
		[Token(Token = "0x60011AB")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
		[CompilerGenerated]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60011AC")]
	[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
	public ManagedTextAttribute(string documentPath = "Uncategorized")
	{
	}
}
