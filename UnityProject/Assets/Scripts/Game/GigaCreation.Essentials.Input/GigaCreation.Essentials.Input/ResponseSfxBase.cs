using System;
using Il2CppDummyDll;
using JetBrains.Annotations;
using UnityEngine;

namespace GigaCreation.Essentials.Input
{
    [Serializable]
    [Token(Token = "0x200000D")]
    public abstract class ResponseSfxBase
    {
    	[Token(Token = "0x4000020")]
    	[FieldOffset(Offset = "0x10")]
    	[UsedImplicitly]
    	[SerializeField]
    	private string _note;

    	[Token(Token = "0x4000021")]
    	[FieldOffset(Offset = "0x18")]
    	[SerializeField]
    	private ResponseKind _responseKind;

    	[Token(Token = "0x17000004")]
    	public ResponseKind ResponseKind
    	{
    		[Token(Token = "0x600001F")]
    		[Address(RVA = "0x38F590", Offset = "0x38DF90", VA = "0x18038F590")]
    		// RECONSTRUCTED | Confidence: A (by field identity) | RVA: 0x38F590, folded
    		//
    		// The dump body was `return default(ResponseKind);`.  As with `ResponseSfx.Path` this RVA is a folded
    		// trivial getter, so the field is identified from the declaration instead: this abstract base declares
    		// `_note` (+0x10) and `_responseKind` (+0x18), and `_responseKind` is the only member of type
    		// `ResponseKind`.  Leaving the stub would be actively harmful rather than merely incomplete: all five
    		// `AudioConfigurationExtended.ResponseSfxData` entries would report kind 0, so
    		// `AudioManagerExtended.InitializeService`'s `ToDictionary(x => x.ResponseKind, x => x.Path)` would
    		// throw `ArgumentException: An item with the same key has already been added`.
    		get
    		{
    			return _responseKind;
    		}
    	}

    	[Token(Token = "0x6000020")]
    	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
    	protected ResponseSfxBase()
    	{
    	}
    }
}
