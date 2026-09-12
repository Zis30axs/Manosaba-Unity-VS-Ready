using System.Collections.Generic;
using System.Threading;
using Il2CppDummyDll;

namespace Naninovel.Async;

[Token(Token = "0x2000606")]
public class CancellationTokenEqualityComparer : IEqualityComparer<CancellationToken>
{
	[Token(Token = "0x4001797")]
	[FieldOffset(Offset = "0x0")]
	public static readonly IEqualityComparer<CancellationToken> Default;

	[Token(Token = "0x6001E4D")]
	[Address(RVA = "0x4C7690", Offset = "0x4C6090", VA = "0x1804C7690", Slot = "4")]
	public bool Equals(CancellationToken x, CancellationToken y)
	{
		return default(bool);
	}

	[Token(Token = "0x6001E4E")]
	[Address(RVA = "0x4C76F0", Offset = "0x4C60F0", VA = "0x1804C76F0", Slot = "5")]
	public int GetHashCode(CancellationToken obj)
	{
		return default(int);
	}

	[Token(Token = "0x6001E4F")]
	[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000")]
	public CancellationTokenEqualityComparer()
	{
	}
}
