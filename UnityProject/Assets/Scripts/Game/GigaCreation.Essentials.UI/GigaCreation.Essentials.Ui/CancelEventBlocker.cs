using System;
using System.Linq;
using Il2CppDummyDll;

namespace GigaCreation.Essentials.Ui
{
    [Token(Token = "0x2000014")]
    public class CancelEventBlocker : IDisposable
    {
    	[Token(Token = "0x400003F")]
    	[FieldOffset(Offset = "0x10")]
    	private readonly ICancelEventBlockable[] _blockables;

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCDB5B0 -- new ICancelEventBlockable[1] { blockable } (covariance-checked store),
    	//   tail jump to the array constructor
    	[Token(Token = "0x600004D")]
    	[Address(RVA = "0xCDB5B0", Offset = "0xCD9FB0", VA = "0x180CDB5B0")]
    	public CancelEventBlocker(ICancelEventBlockable blockable = null)
    		: this(new ICancelEventBlockable[] { blockable })
    	{
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCDB6B0 -- stores +0x10, then over the re-read array: ICancelEventBlockable
    	//   slot 1 set_IsCancelEventBlocked(true); a null element throws (no guard in the binary)
    	// Annotated disassembly: work/title-path/disas/ceb_02_0xCDB6B0.txt
    	[Token(Token = "0x600004E")]
    	[Address(RVA = "0xCDB6B0", Offset = "0xCDA0B0", VA = "0x180CDB6B0")]
    	public CancelEventBlocker(ICancelEventBlockable[] blockables)
    	{
    		_blockables = blockables;
    		ICancelEventBlockable[] blockableArray = _blockables;
    		foreach (ICancelEventBlockable blockable in blockableArray)
    		{
    			blockable.IsCancelEventBlocked = true;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0xCDB820 -- Enumerable.Where with the cached <>c.<Dispose>b__3_0 (0xCDBC70:
    	//   slot 0 get_Exists), IEnumerator<T> loop with slot 1 set_IsCancelEventBlocked(false), enumerator disposed
    	// Annotated disassembly: work/title-path/disas/ceb_03_0xCDB820.txt
    	[Token(Token = "0x600004F")]
    	[Address(RVA = "0xCDB820", Offset = "0xCDA220", VA = "0x180CDB820", Slot = "5")]
    	public virtual void Dispose()
    	{
    		// Shipped null test: 0x180CDB8DF cmp [rbx+0x10], 0 / je past the loop.
    		if (_blockables != null)
    		{
    			foreach (ICancelEventBlockable blockable in _blockables.Where((ICancelEventBlockable b) => b.Exists))
    			{
    				blockable.IsCancelEventBlocked = false;
    			}
    		}
    	}
    }
}
