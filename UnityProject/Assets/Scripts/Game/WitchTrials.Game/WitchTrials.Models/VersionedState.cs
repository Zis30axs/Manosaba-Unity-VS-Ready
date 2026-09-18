using System;
using System.Collections.Generic;
using System.Linq;
using Il2CppDummyDll;
using UnityEngine;

namespace WitchTrials.Models
{
    [Serializable]
    [Token(Token = "0x200019C")]
    public class VersionedState
    {
    	[Token(Token = "0x400077D")]
    	[FieldOffset(Offset = "0x10")]
    	[SerializeField]
    	private List<IdVersionPair> _list;

    	[Token(Token = "0x17000129")]
    	public IReadOnlyCollection<IdVersionPair> ActiveItems
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x3A2300, 492 B
    		// + lambda `<>c.<get_ActiveItems>b__2_0` 0x3A2C90 (26 B: `pair.Version (+0x18) > 0`,
    		// NRE on null pair), cached in `<>c.<>9__2_0`.
    		// `Enumerable.Where(_list, lambda)`, then an inlined `Enumerable.ToList`
    		// (null check → `Error.ArgumentNull("source")`, then `new List<IdVersionPair>(source)`).
    		[Token(Token = "0x6000707")]
    		[Address(RVA = "0x3A2300", Offset = "0x3A0D00", VA = "0x1803A2300")]
    		get
    		{
    			return _list.Where(pair => pair.Version > 0).ToList();
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3A24F0, 109 B
    	// `new VersionedState()` (direct call to 0x3A2AB0), then `Copy(this)` (0x3A25B0).
    	[Token(Token = "0x6000708")]
    	[Address(RVA = "0x3A24F0", Offset = "0x3A0EF0", VA = "0x1803A24F0")]
    	public VersionedState Clone()
    	{
    		VersionedState versionedState = new VersionedState();
    		versionedState.Copy(this);
    		return versionedState;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3A2560, 66 B
    	// Tail-jumps to `List<IdVersionPair>.Clear` on `_list` (+0x10).
    	[Token(Token = "0x6000709")]
    	[Address(RVA = "0x3A2560", Offset = "0x3A0F60", VA = "0x1803A2560")]
    	public void ResetToDefault()
    	{
    		_list.Clear();
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3A25B0, 614 B
    	// - `_list.Clear()`, then `_list.Capacity = from._list.Count` (`from` null → NRE).
    	// - `foreach` over `from._list` (struct `List<T>.Enumerator`): for each item,
    	//   `_list.Add(...)` with an inlined `IdVersionPair.Clone(item)` (0x3A2FB0:
    	//   NRE on null, new object, copy `Id` +0x10 and `Version` +0x18).
    	[Token(Token = "0x600070A")]
    	[Address(RVA = "0x3A25B0", Offset = "0x3A0FB0", VA = "0x1803A25B0")]
    	public void Copy(VersionedState from)
    	{
    		_list.Clear();
    		_list.Capacity = from._list.Count;
    		foreach (IdVersionPair item in from._list)
    		{
    			_list.Add(IdVersionPair.Clone(item));
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3A2820, 531 B
    	// - A reverse `for` from `Count - 1` down to 0 over `_list`, testing each item
    	//   with an inlined `IdVersionPair.EqualId(id)` (0x3A2F10: same CompareInfo/
    	//   CultureInfo metadata init, ordinal compare).
    	// - On a hit: `_list[i] = new IdVersionPair(id, version)`, with a real call to
    	//   ctor 0x3A2CB0, then return.
    	// - After the loop: `_list.Add(new IdVersionPair(id, version))`, with the ctor
    	//   inlined (two field stores).
    	[Token(Token = "0x600070B")]
    	[Address(RVA = "0x3A2820", Offset = "0x3A1220", VA = "0x1803A2820")]
    	public void SetVersion(string id, int version)
    	{
    		for (int i = _list.Count - 1; i >= 0; i--)
    		{
    			if (_list[i].EqualId(id))
    			{
    				_list[i] = new IdVersionPair(id, version);
    				return;
    			}
    		}
    		_list.Add(new IdVersionPair(id, version));
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3A2A40, 88 B
    	// Tail-jumps to `List<IdVersionPair>.Contains(pair)`, which uses
    	// `EqualityComparer<IdVersionPair>.Default` and so `IEquatable.Equals`.
    	[Token(Token = "0x600070C")]
    	[Address(RVA = "0x3A2A40", Offset = "0x3A1440", VA = "0x1803A2A40")]
    	public bool Validate(IdVersionPair pair)
    	{
    		return _list.Contains(pair);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3A2AA0, 9 B (shared body)
    	// `cmp qword [rcx+0x10], 0; sete al`.
    	[Token(Token = "0x600070D")]
    	[Address(RVA = "0x3A2AA0", Offset = "0x3A14A0", VA = "0x1803A2AA0")]
    	public bool IsNull()
    	{
    		return _list == null;
    	}

    	// RECONSTRUCTED | Confidence: A (behavior), B (capacity spelling) | RVA: 0x3A2AB0, 308 B
    	// Field initializer. The `List<IdVersionPair>` ctor is inlined as
    	// `_items = new IdVersionPair[0x40]` (array-new helper 0x297400 with length 64).
    	// The parameterless List ctor would use the static empty array and allocate
    	// nothing, so this is the `(int capacity)` overload with the constant 64
    	// folded in. il2cpp prints every ctor METHODINFO as `..ctor()`.
    	[Token(Token = "0x600070E")]
    	[Address(RVA = "0x3A2AB0", Offset = "0x3A14B0", VA = "0x1803A2AB0")]
    	public VersionedState()
    	{
    		_list = new List<IdVersionPair>(64);
    	}
    }
}
