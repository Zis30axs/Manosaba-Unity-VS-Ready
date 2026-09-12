using System.Collections.Generic;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x20002F0")]
public sealed class ArrayEqualityComparer<T>
{
	[Token(Token = "0x4000B5C")]
	[FieldOffset(Offset = "0x0")]
	private static readonly EqualityComparer<T> ITEMS_COMPARER;

	[Token(Token = "0x6000E00")]
	public static bool Equals(T[] first, T[] second)
	{
		return default(bool);
	}

	[Token(Token = "0x6000E01")]
	public static int GetHashCode(T[] array)
	{
		return default(int);
	}

	[Token(Token = "0x6000E02")]
	private bool System_002ECollections_002EGeneric_002EIEqualityComparer_003CT_005B_005D_003E_002EEquals(T[] first, T[] second)
	{
		return default(bool);
	}

	[Token(Token = "0x6000E03")]
	private int System_002ECollections_002EGeneric_002EIEqualityComparer_003CT_005B_005D_003E_002EGetHashCode(T[] array)
	{
		return default(int);
	}

	[Token(Token = "0x6000E04")]
	public ArrayEqualityComparer()
	{
	}
}
