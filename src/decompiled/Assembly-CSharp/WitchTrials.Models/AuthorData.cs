using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models;

[Token(Token = "0x200019F")]
public class AuthorData : ScriptableObject
{
	[Token(Token = "0x4000782")]
	[FieldOffset(Offset = "0x18")]
	[SerializeField]
	private List<AuthorDataItem> _items;

	[Token(Token = "0x1700012A")]
	public IList<AuthorDataItem> Items
	{
		[Token(Token = "0x6000718")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000719")]
	[Address(RVA = "0x3A3070", Offset = "0x3A1A70", VA = "0x1803A3070")]
	public AuthorData()
	{
	}
}
