using System;
using System.Diagnostics;
using System.Threading;
using Il2CppDummyDll;

namespace Naninovel.Async.Internal;

[Token(Token = "0x2000636")]
internal sealed class ContinuationQueue
{
	[Token(Token = "0x40017B8")]
	private const int MaxArrayLength = 2146435071;

	[Token(Token = "0x40017B9")]
	private const int InitialSize = 16;

	[Token(Token = "0x40017BA")]
	[FieldOffset(Offset = "0x10")]
	private readonly PlayerLoopTiming timing;

	[Token(Token = "0x40017BB")]
	[FieldOffset(Offset = "0x14")]
	private SpinLock gate;

	[Token(Token = "0x40017BC")]
	[FieldOffset(Offset = "0x18")]
	private bool dequing;

	[Token(Token = "0x40017BD")]
	[FieldOffset(Offset = "0x1C")]
	private int actionListCount;

	[Token(Token = "0x40017BE")]
	[FieldOffset(Offset = "0x20")]
	private Action[] actionList;

	[Token(Token = "0x40017BF")]
	[FieldOffset(Offset = "0x28")]
	private int waitingListCount;

	[Token(Token = "0x40017C0")]
	[FieldOffset(Offset = "0x30")]
	private Action[] waitingList;

	[Token(Token = "0x6001E87")]
	[Address(RVA = "0x4CBDB0", Offset = "0x4CA7B0", VA = "0x1804CBDB0")]
	public ContinuationQueue(PlayerLoopTiming timing)
	{
	}

	[Token(Token = "0x6001E88")]
	[Address(RVA = "0x4CBEE0", Offset = "0x4CA8E0", VA = "0x1804CBEE0")]
	public void Enqueue(Action continuation)
	{
	}

	[Token(Token = "0x6001E89")]
	[Address(RVA = "0x4CC450", Offset = "0x4CAE50", VA = "0x1804CC450")]
	public int Clear()
	{
		return default(int);
	}

	[Token(Token = "0x6001E8A")]
	[Address(RVA = "0x4CC590", Offset = "0x4CAF90", VA = "0x1804CC590")]
	public void Run()
	{
	}

	[Token(Token = "0x6001E8B")]
	[Address(RVA = "0x4CC590", Offset = "0x4CAF90", VA = "0x1804CC590")]
	private void Initialization()
	{
	}

	[Token(Token = "0x6001E8C")]
	[Address(RVA = "0x4CC590", Offset = "0x4CAF90", VA = "0x1804CC590")]
	private void LastInitialization()
	{
	}

	[Token(Token = "0x6001E8D")]
	[Address(RVA = "0x4CC590", Offset = "0x4CAF90", VA = "0x1804CC590")]
	private void EarlyUpdate()
	{
	}

	[Token(Token = "0x6001E8E")]
	[Address(RVA = "0x4CC590", Offset = "0x4CAF90", VA = "0x1804CC590")]
	private void LastEarlyUpdate()
	{
	}

	[Token(Token = "0x6001E8F")]
	[Address(RVA = "0x4CC590", Offset = "0x4CAF90", VA = "0x1804CC590")]
	private void FixedUpdate()
	{
	}

	[Token(Token = "0x6001E90")]
	[Address(RVA = "0x4CC590", Offset = "0x4CAF90", VA = "0x1804CC590")]
	private void LastFixedUpdate()
	{
	}

	[Token(Token = "0x6001E91")]
	[Address(RVA = "0x4CC590", Offset = "0x4CAF90", VA = "0x1804CC590")]
	private void PreUpdate()
	{
	}

	[Token(Token = "0x6001E92")]
	[Address(RVA = "0x4CC590", Offset = "0x4CAF90", VA = "0x1804CC590")]
	private void LastPreUpdate()
	{
	}

	[Token(Token = "0x6001E93")]
	[Address(RVA = "0x4CC590", Offset = "0x4CAF90", VA = "0x1804CC590")]
	private void Update()
	{
	}

	[Token(Token = "0x6001E94")]
	[Address(RVA = "0x4CC590", Offset = "0x4CAF90", VA = "0x1804CC590")]
	private void LastUpdate()
	{
	}

	[Token(Token = "0x6001E95")]
	[Address(RVA = "0x4CC590", Offset = "0x4CAF90", VA = "0x1804CC590")]
	private void PreLateUpdate()
	{
	}

	[Token(Token = "0x6001E96")]
	[Address(RVA = "0x4CC590", Offset = "0x4CAF90", VA = "0x1804CC590")]
	private void LastPreLateUpdate()
	{
	}

	[Token(Token = "0x6001E97")]
	[Address(RVA = "0x4CC590", Offset = "0x4CAF90", VA = "0x1804CC590")]
	private void PostLateUpdate()
	{
	}

	[Token(Token = "0x6001E98")]
	[Address(RVA = "0x4CC590", Offset = "0x4CAF90", VA = "0x1804CC590")]
	private void LastPostLateUpdate()
	{
	}

	[Token(Token = "0x6001E99")]
	[Address(RVA = "0x4CC590", Offset = "0x4CAF90", VA = "0x1804CC590")]
	private void TimeUpdate()
	{
	}

	[Token(Token = "0x6001E9A")]
	[Address(RVA = "0x4CC590", Offset = "0x4CAF90", VA = "0x1804CC590")]
	private void LastTimeUpdate()
	{
	}

	[Token(Token = "0x6001E9B")]
	[Address(RVA = "0x4CC5A0", Offset = "0x4CAFA0", VA = "0x1804CC5A0")]
	[DebuggerHidden]
	private void RunCore()
	{
	}
}
