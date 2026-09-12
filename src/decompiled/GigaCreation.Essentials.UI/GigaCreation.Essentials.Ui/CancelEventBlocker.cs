using System;
using Il2CppDummyDll;

namespace GigaCreation.Essentials.Ui;

[Token(Token = "0x2000014")]
public class CancelEventBlocker : IDisposable
{
	[Token(Token = "0x400003F")]
	[FieldOffset(Offset = "0x10")]
	private readonly ICancelEventBlockable[] _blockables;

	[Token(Token = "0x600004D")]
	[Address(RVA = "0xCDB5B0", Offset = "0xCD9FB0", VA = "0x180CDB5B0")]
	public CancelEventBlocker(ICancelEventBlockable blockable = default)
	{
	}

	[Token(Token = "0x600004E")]
	[Address(RVA = "0xCDB6B0", Offset = "0xCDA0B0", VA = "0x180CDB6B0")]
	public CancelEventBlocker(ICancelEventBlockable[] blockables)
	{
	}

	[Token(Token = "0x600004F")]
	[Address(RVA = "0xCDB820", Offset = "0xCDA220", VA = "0x180CDB820", Slot = "5")]
	public virtual void Dispose()
	{
	}
}
