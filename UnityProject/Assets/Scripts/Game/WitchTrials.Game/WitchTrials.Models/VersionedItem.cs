using System;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models
{
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
    	// RECONSTRUCTED | Confidence: A | RVA: 0x891400 (shared instantiation) -- returns `_id` through the RGCTX field offset.
    	public string Id
    	{
    		[Token(Token = "0x60005C3")]
    		get
    		{
    			return _id;
    		}
    	}

    	[Token(Token = "0x170000D6")]
    	// RECONSTRUCTED | Confidence: A | RVA: 0x83AC30 -- copies `_item` (size from the RGCTX type).
    	public TItem Item
    	{
    		[Token(Token = "0x60005C4")]
    		get
    		{
    			return _item;
    		}
    	}

    	[Token(Token = "0x170000D7")]
    	// RECONSTRUCTED | Confidence: A | RVA: 0x9D81F0 -- lazily `_idVersionPair = new IdVersionPair(_id, _version)`
    	//   (ctor inlined as two field stores), then returned.
    	public IdVersionPair IdVersionPair
    	{
    		[Token(Token = "0x60005C5")]
    		get
    		{
    			return _idVersionPair ??= new IdVersionPair(_id, _version);
    		}
    	}

    	[Token(Token = "0x60005C6")]
    	// RECONSTRUCTED | Confidence: A | RVA: 0x9D83C0 -- stores id, version and item.
    	public VersionedItem(string id, int version, TItem item)
    	{
    		_id = id;
    		_version = version;
    		_item = item;
    	}
    }
}
