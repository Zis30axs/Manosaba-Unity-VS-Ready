using System;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models;

[Serializable]
[Token(Token = "0x2000162")]
public class VersionedItem<TItem>
{
	[Token(Token = "0x40006B8")]
	[FieldOffset(Offset = "0x0")]
	[SerializeField]
	private string _id;

	[Token(Token = "0x40006B9")]
	[FieldOffset(Offset = "0x0")]
	[SerializeField]
	private int _version;

	[Token(Token = "0x40006BA")]
	[FieldOffset(Offset = "0x0")]
	[SerializeField]
	private TItem _item;

	[NonSerialized]
	[Token(Token = "0x40006BB")]
	[FieldOffset(Offset = "0x0")]
	private IdVersionPair _idVersionPair;

	[Token(Token = "0x170000D5")]
	public string Id
	{
		[Token(Token = "0x60005C3")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x170000D6")]
	public TItem Item
	{
		[Token(Token = "0x60005C4")]
		get
		{
			return default(TItem);
		}
	}

	[Token(Token = "0x170000D7")]
	public IdVersionPair IdVersionPair
	{
		[Token(Token = "0x60005C5")]
		get
		{
			return null;
		}
	}

	[Token(Token = "0x60005C6")]
	public VersionedItem(string id, int version, TItem item)
	{
	}
}
