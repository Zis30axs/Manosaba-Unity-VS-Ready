using System.Threading;
using Il2CppDummyDll;

namespace Naninovel.Async.Internal
{
    [Token(Token = "0x2000640")]
    public abstract class PlayerLoopReusablePromiseBase : ReusablePromise, IPlayerLoopItem
    {
    	[Token(Token = "0x40017DC")]
    	[FieldOffset(Offset = "0x28")]
    	private readonly PlayerLoopTiming timing;

    	[Token(Token = "0x40017DD")]
    	[FieldOffset(Offset = "0x30")]
    	protected readonly CancellationToken cancellationToken;

    	[Token(Token = "0x40017DE")]
    	[FieldOffset(Offset = "0x38")]
    	private bool isRunning;

    	[Token(Token = "0x1700049C")]
    	public override bool IsCompleted
    	{
    		[Token(Token = "0x6001EF1")]
    		[Address(RVA = "0x4CE550", Offset = "0x4CCF50", VA = "0x1804CE550", Slot = "9")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x6001EF0")]
    	[Address(RVA = "0x4CE4F0", Offset = "0x4CCEF0", VA = "0x1804CE4F0")]
    	protected PlayerLoopReusablePromiseBase(PlayerLoopTiming timing = default, CancellationToken cancellationToken = default, int skipTrackFrameCountAdditive = default)
    	{
    	}

    	[Token(Token = "0x6001EF2")]
    	protected abstract void OnRunningStart();

    	[Token(Token = "0x6001EF3")]
    	[Address(RVA = "0x4CE600", Offset = "0x4CD000", VA = "0x1804CE600")]
    	protected void Complete()
    	{
    	}

    	[Token(Token = "0x6001EF4")]
    	public abstract bool MoveNext();
    }
    [Token(Token = "0x2000641")]
    public abstract class PlayerLoopReusablePromiseBase<T> : ReusablePromise<T>, IPlayerLoopItem
    {
    	[Token(Token = "0x40017DF")]
    	[FieldOffset(Offset = "0x0")]
    	private readonly PlayerLoopTiming timing;

    	[Token(Token = "0x40017E0")]
    	[FieldOffset(Offset = "0x0")]
    	protected readonly CancellationToken cancellationToken;

    	[Token(Token = "0x40017E1")]
    	[FieldOffset(Offset = "0x0")]
    	private bool isRunning;

    	[Token(Token = "0x1700049D")]
    	public override bool IsCompleted
    	{
    		[Token(Token = "0x6001EF6")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x6001EF5")]
    	protected PlayerLoopReusablePromiseBase(PlayerLoopTiming timing = default, CancellationToken cancellationToken = default, int skipTrackFrameCountAdditive = default)
    	{
    	}

    	[Token(Token = "0x6001EF7")]
    	protected abstract void OnRunningStart();

    	[Token(Token = "0x6001EF8")]
    	protected void Complete()
    	{
    	}

    	[Token(Token = "0x6001EF9")]
    	public abstract bool MoveNext();
    }
}
