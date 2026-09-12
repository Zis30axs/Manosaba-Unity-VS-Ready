using System;
using Il2CppDummyDll;
using UnityEngine;

namespace Naninovel;

[Serializable]
[Token(Token = "0x2000245")]
public class Folder
{
	[Token(Token = "0x40008F1")]
	[FieldOffset(Offset = "0x10")]
	[SerializeField]
	private string path;

	[Token(Token = "0x17000174")]
	public string Path
	{
		[Token(Token = "0x6000A15")]
		[Address(RVA = "0x33FBC0", Offset = "0x33E5C0", VA = "0x18033FBC0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x17000175")]
	public string Name
	{
		[Token(Token = "0x6000A16")]
		[Address(RVA = "0x52E120", Offset = "0x52CB20", VA = "0x18052E120")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000A17")]
	[Address(RVA = "0x30C0D0", Offset = "0x30AAD0", VA = "0x18030C0D0")]
	public Folder(string path)
	{
	}
}
