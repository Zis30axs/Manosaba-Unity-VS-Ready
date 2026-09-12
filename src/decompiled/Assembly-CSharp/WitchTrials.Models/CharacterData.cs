using System.Collections.Generic;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models;

[Token(Token = "0x2000151")]
public class CharacterData : ScriptableObject
{
	[Token(Token = "0x4000672")]
	[FieldOffset(Offset = "0x18")]
	[SerializeField]
	private List<CharacterDataItem> _items;

	[Token(Token = "0x170000B8")]
	public IList<CharacterDataItem> Items
	{
		[Token(Token = "0x6000597")]
		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x6000598")]
	[Address(RVA = "0x38EF90", Offset = "0x38D990", VA = "0x18038EF90")]
	public CharacterData()
	{
	}
}
