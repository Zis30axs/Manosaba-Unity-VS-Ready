using System;
using System.Diagnostics;
using Il2CppDummyDll;

namespace Naninovel.Async.Internal
{
    [Token(Token = "0x200063C")]
    internal sealed class PlayerLoopRunner
    {
    	[Token(Token = "0x40017CB")]
    	private const int InitialSize = 16;

    	[Token(Token = "0x40017CC")]
    	[FieldOffset(Offset = "0x10")]
    	private readonly PlayerLoopTiming timing;

    	[Token(Token = "0x40017CD")]
    	[FieldOffset(Offset = "0x18")]
    	private readonly object runningAndQueueLock;

    	[Token(Token = "0x40017CE")]
    	[FieldOffset(Offset = "0x20")]
    	private readonly object arrayLock;

    	[Token(Token = "0x40017CF")]
    	[FieldOffset(Offset = "0x28")]
    	private readonly Action<Exception> unhandledExceptionCallback;

    	[Token(Token = "0x40017D0")]
    	[FieldOffset(Offset = "0x30")]
    	private int tail;

    	[Token(Token = "0x40017D1")]
    	[FieldOffset(Offset = "0x34")]
    	private bool running;

    	[Token(Token = "0x40017D2")]
    	[FieldOffset(Offset = "0x38")]
    	private IPlayerLoopItem[] loopItems;

    	[Token(Token = "0x40017D3")]
    	[FieldOffset(Offset = "0x40")]
    	private readonly MinimumQueue<IPlayerLoopItem> waitQueue;

    	[Token(Token = "0x6001EBF")]
    	[Address(RVA = "0x4CCE00", Offset = "0x4CB800", VA = "0x1804CCE00")]
    	public PlayerLoopRunner(PlayerLoopTiming timing)
    	{
    	}

    	[Token(Token = "0x6001EC0")]
    	[Address(RVA = "0x4CD180", Offset = "0x4CBB80", VA = "0x1804CD180")]
    	public void AddAction(IPlayerLoopItem item)
    	{
    	}

    	[Token(Token = "0x6001EC1")]
    	[Address(RVA = "0x4CD520", Offset = "0x4CBF20", VA = "0x1804CD520")]
    	public int Clear()
    	{
    		return default(int);
    	}

    	[Token(Token = "0x6001EC2")]
    	[Address(RVA = "0x4CD6A0", Offset = "0x4CC0A0", VA = "0x1804CD6A0")]
    	public void Run()
    	{
    	}

    	[Token(Token = "0x6001EC3")]
    	[Address(RVA = "0x4CD6A0", Offset = "0x4CC0A0", VA = "0x1804CD6A0")]
    	private void Initialization()
    	{
    	}

    	[Token(Token = "0x6001EC4")]
    	[Address(RVA = "0x4CD6A0", Offset = "0x4CC0A0", VA = "0x1804CD6A0")]
    	private void LastInitialization()
    	{
    	}

    	[Token(Token = "0x6001EC5")]
    	[Address(RVA = "0x4CD6A0", Offset = "0x4CC0A0", VA = "0x1804CD6A0")]
    	private void EarlyUpdate()
    	{
    	}

    	[Token(Token = "0x6001EC6")]
    	[Address(RVA = "0x4CD6A0", Offset = "0x4CC0A0", VA = "0x1804CD6A0")]
    	private void LastEarlyUpdate()
    	{
    	}

    	[Token(Token = "0x6001EC7")]
    	[Address(RVA = "0x4CD6A0", Offset = "0x4CC0A0", VA = "0x1804CD6A0")]
    	private void FixedUpdate()
    	{
    	}

    	[Token(Token = "0x6001EC8")]
    	[Address(RVA = "0x4CD6A0", Offset = "0x4CC0A0", VA = "0x1804CD6A0")]
    	private void LastFixedUpdate()
    	{
    	}

    	[Token(Token = "0x6001EC9")]
    	[Address(RVA = "0x4CD6A0", Offset = "0x4CC0A0", VA = "0x1804CD6A0")]
    	private void PreUpdate()
    	{
    	}

    	[Token(Token = "0x6001ECA")]
    	[Address(RVA = "0x4CD6A0", Offset = "0x4CC0A0", VA = "0x1804CD6A0")]
    	private void LastPreUpdate()
    	{
    	}

    	[Token(Token = "0x6001ECB")]
    	[Address(RVA = "0x4CD6A0", Offset = "0x4CC0A0", VA = "0x1804CD6A0")]
    	private void Update()
    	{
    	}

    	[Token(Token = "0x6001ECC")]
    	[Address(RVA = "0x4CD6A0", Offset = "0x4CC0A0", VA = "0x1804CD6A0")]
    	private void LastUpdate()
    	{
    	}

    	[Token(Token = "0x6001ECD")]
    	[Address(RVA = "0x4CD6A0", Offset = "0x4CC0A0", VA = "0x1804CD6A0")]
    	private void PreLateUpdate()
    	{
    	}

    	[Token(Token = "0x6001ECE")]
    	[Address(RVA = "0x4CD6A0", Offset = "0x4CC0A0", VA = "0x1804CD6A0")]
    	private void LastPreLateUpdate()
    	{
    	}

    	[Token(Token = "0x6001ECF")]
    	[Address(RVA = "0x4CD6A0", Offset = "0x4CC0A0", VA = "0x1804CD6A0")]
    	private void PostLateUpdate()
    	{
    	}

    	[Token(Token = "0x6001ED0")]
    	[Address(RVA = "0x4CD6A0", Offset = "0x4CC0A0", VA = "0x1804CD6A0")]
    	private void LastPostLateUpdate()
    	{
    	}

    	[Token(Token = "0x6001ED1")]
    	[Address(RVA = "0x4CD6A0", Offset = "0x4CC0A0", VA = "0x1804CD6A0")]
    	private void TimeUpdate()
    	{
    	}

    	[Token(Token = "0x6001ED2")]
    	[Address(RVA = "0x4CD6A0", Offset = "0x4CC0A0", VA = "0x1804CD6A0")]
    	private void LastTimeUpdate()
    	{
    	}

    	[Token(Token = "0x6001ED3")]
    	[Address(RVA = "0x4CD6B0", Offset = "0x4CC0B0", VA = "0x1804CD6B0")]
    	[DebuggerHidden]
    	private void RunCore()
    	{
    	}
    }
}
