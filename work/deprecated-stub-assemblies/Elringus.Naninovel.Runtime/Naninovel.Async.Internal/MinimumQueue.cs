using System.Runtime.CompilerServices;
using Il2CppDummyDll;

namespace Naninovel.Async.Internal
{
    [Token(Token = "0x200063B")]
    internal class MinimumQueue<T>
    {
    	[Token(Token = "0x40017C5")]
    	private const int MinimumGrow = 4;

    	[Token(Token = "0x40017C6")]
    	private const int GrowFactor = 200;

    	[Token(Token = "0x40017C7")]
    	[FieldOffset(Offset = "0x0")]
    	private T[] array;

    	[Token(Token = "0x40017C8")]
    	[FieldOffset(Offset = "0x0")]
    	private int head;

    	[Token(Token = "0x40017C9")]
    	[FieldOffset(Offset = "0x0")]
    	private int tail;

    	[Token(Token = "0x40017CA")]
    	[FieldOffset(Offset = "0x0")]
    	private int size;

    	[Token(Token = "0x17000494")]
    	public int Count
    	{
    		[MethodImpl(256)]
    		[Token(Token = "0x6001EB7")]
    		get
    		{
    			return default(int);
    		}
    	}

    	[Token(Token = "0x6001EB6")]
    	public MinimumQueue(int capacity)
    	{
    	}

    	[Token(Token = "0x6001EB8")]
    	public T Peek()
    	{
    		return default(T);
    	}

    	[MethodImpl(256)]
    	[Token(Token = "0x6001EB9")]
    	public void Enqueue(T item)
    	{
    	}

    	[MethodImpl(256)]
    	[Token(Token = "0x6001EBA")]
    	public T Dequeue()
    	{
    		return default(T);
    	}

    	[Token(Token = "0x6001EBB")]
    	private void Grow()
    	{
    	}

    	[Token(Token = "0x6001EBC")]
    	private void SetCapacity(int capacity)
    	{
    	}

    	[MethodImpl(256)]
    	[Token(Token = "0x6001EBD")]
    	private void MoveNext(ref int index)
    	{
    	}

    	[Token(Token = "0x6001EBE")]
    	private void ThrowForEmptyQueue()
    	{
    	}
    }
}
