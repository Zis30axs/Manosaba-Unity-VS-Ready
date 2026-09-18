using System;
using System.Collections.Generic;
using Il2CppDummyDll;

namespace WitchTrials.Models
{
    [Serializable]
    [Token(Token = "0x200019E")]
    public class IdVersionPair : IEquatable<IdVersionPair>, IEqualityComparer<IdVersionPair>
    {
    	[Token(Token = "0x4000780")]
    	[FieldOffset(Offset = "0x10")]
    	public string Id;

    	[Token(Token = "0x4000781")]
    	[FieldOffset(Offset = "0x18")]
    	public int Version;

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3A2CB0, 91 B (shared two-field-store body)
    	// `Id (+0x10) = id` with a write barrier, then `Version (+0x18) = version`.
    	[Token(Token = "0x6000712")]
    	[Address(RVA = "0x3A2CB0", Offset = "0x3A16B0", VA = "0x1803A2CB0")]
    	public IdVersionPair(string id, int version)
    	{
    		Id = id;
    		Version = version;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3A2D10, 223 B
    	// This is the standard IDE-generated comparer shape:
    	// - Same ref → true; either side null → false.
    	// - `x.GetType() != y.GetType()` → false (two calls to the GetType helper
    	//   0x299D20, then a `Type` op_Inequality compare).
    	// - Otherwise the result is `x.Id == y.Id` (ordinal SequenceEqual) and
    	//   `x.Version == y.Version`.
    	[Token(Token = "0x6000713")]
    	[Address(RVA = "0x3A2D10", Offset = "0x3A1710", VA = "0x1803A2D10", Slot = "5")]
    	public bool Equals(IdVersionPair x, IdVersionPair y)
    	{
    		if ((object)x == y)
    		{
    			return true;
    		}
    		if ((object)x == null)
    		{
    			return false;
    		}
    		if ((object)y == null)
    		{
    			return false;
    		}
    		if (x.GetType() != y.GetType())
    		{
    			return false;
    		}
    		if (x.Id == y.Id)
    		{
    			return x.Version == y.Version;
    		}
    		return false;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3A2DF0, 137 B
    	// `HashCode.Combine<string, int>(obj.Id, obj.Version)`; NRE on null obj.
    	[Token(Token = "0x6000714")]
    	[Address(RVA = "0x3A2DF0", Offset = "0x3A17F0", VA = "0x1803A2DF0", Slot = "6")]
    	public int GetHashCode(IdVersionPair obj)
    	{
    		return HashCode.Combine(obj.Id, obj.Version);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3A2E80, 130 B
    	// Null `other` → false; same ref → true; otherwise the result is
    	// `Id == other.Id && Version == other.Version`.
    	[Token(Token = "0x6000715")]
    	[Address(RVA = "0x3A2E80", Offset = "0x3A1880", VA = "0x1803A2E80", Slot = "4")]
    	public bool Equals(IdVersionPair other)
    	{
    		if ((object)other == null)
    		{
    			return false;
    		}
    		if ((object)this == other)
    		{
    			return true;
    		}
    		if (Id == other.Id)
    		{
    			return Version == other.Version;
    		}
    		return false;
    	}

    	// RECONSTRUCTED | Confidence: A (behavior), B (spelling) | RVA: 0x3A2F10, 149 B
    	// The ref/null/length/SequenceEqual body is ordinal string equality, with
    	// null `Id` giving false and no NRE. Unlike the `==` bodies in `Equals`
    	// above, it initializes CompareInfo/CultureInfo metadata, which is the
    	// footprint of an inlined `string.Equals(string, string, StringComparison)`
    	// with the constant `Ordinal`.
    	[Token(Token = "0x6000716")]
    	[Address(RVA = "0x3A2F10", Offset = "0x3A1910", VA = "0x1803A2F10")]
    	public bool EqualId(string id)
    	{
    		return string.Equals(Id, id, StringComparison.Ordinal);
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3A2FB0, 179 B
    	// NRE on null `from`; `new IdVersionPair(from.Id, from.Version)` with the ctor inlined.
    	[Token(Token = "0x6000717")]
    	[Address(RVA = "0x3A2FB0", Offset = "0x3A19B0", VA = "0x1803A2FB0")]
    	public static IdVersionPair Clone(IdVersionPair from)
    	{
    		return new IdVersionPair(from.Id, from.Version);
    	}
    }
}
