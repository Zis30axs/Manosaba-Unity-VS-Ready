using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Il2CppDummyDll;

namespace Naninovel;

[Token(Token = "0x20002EF")]
public class GeneralPropertyComparer<T, TKey> : IEqualityComparer<T>
{
	[Token(Token = "0x4000B5A")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private readonly Func<T, TKey> property;

	[Token(Token = "0x4000B5B")]
	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
	private readonly IEqualityComparer<TKey> propertyComparer;

	[Token(Token = "0x6000DFD")]
	public GeneralPropertyComparer(Func<T, TKey> property, [Optional] IEqualityComparer<TKey> propertyComparer)
	{
	}

	[Token(Token = "0x6000DFE")]
	public bool Equals(T first, T second)
	{
		return default(bool);
	}

	[Token(Token = "0x6000DFF")]
	public int GetHashCode(T obj)
	{
		return default(int);
	}
}
