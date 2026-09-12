using System;
using GigaCreation.Essentials.Input;
using Il2CppDummyDll;
using Naninovel;
using UnityEngine;

namespace GigaCreation.NaninovelExtender.Audio;

[Serializable]
[Token(Token = "0x2000008")]
public sealed class ResponseSfx : ResponseSfxBase
{
	[Token(Token = "0x400000D")]
	[FieldOffset(Offset = "0x20")]
	[ResourcePopup("Audio")]
	[SerializeField]
	private string _path;

	[Token(Token = "0x17000002")]
	public string Path
	{
		[Token(Token = "0x600000F")]
		[Address(RVA = "0x377910", Offset = "0x376310", VA = "0x180377910")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000010")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	public ResponseSfx()
	{
	}
}
