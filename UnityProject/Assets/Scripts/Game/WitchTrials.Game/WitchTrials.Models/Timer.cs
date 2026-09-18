using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Il2CppDummyDll;
using UniRx;

namespace WitchTrials.Models
{
    [Token(Token = "0x20001A8")]
    public class Timer : IDisposable
    {
    	[StructLayout(3)]
    	[Token(Token = "0x20001A9")]
    	[CompilerGenerated]
    	private struct _003CStartAsync_003Ed__14
    {
    		[Token(Token = "0x40007B5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x40007B6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x40007B7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public Timer _003C_003E4__this;

    		[Token(Token = "0x40007B8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		public float initialSpeed;

    		[Token(Token = "0x40007B9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		public CancellationToken ct;

    		[Token(Token = "0x40007BA")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    		private CancellationTokenSource _003ClinkedCts_003E5__2;

    		[Token(Token = "0x40007BB")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private TimeSpan _003CpreviousTime_003E5__3;

    		[Token(Token = "0x40007BC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private UniTask.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000757")]
    		[Address(RVA = "0x3A7230", Offset = "0x3A5C30", VA = "0x1803A7230", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000758")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x40007B1")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    	private readonly bool _useRealtime;

    	[Token(Token = "0x40007B2")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    	private ReactiveProperty<TimeSpan> _remainingTime;

    	[Token(Token = "0x40007B3")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	private CancellationTokenSource _runningCts;

    	// RECONSTRUCTED | Confidence: A
    	// Backing field for the Speed auto-property below. dump.cs shows it as
    	// `<Speed>k__BackingField`, a metadata-only name that isn't valid C#
    	// source syntax, so ILSpy rendered Speed as get/set methods with no field
    	// declaration at all.
    	// Initializer: the constructor (RVA 0x3A6CE0) writes 1f to +0x28 before storing `useRealtime`.
    	private float _speed = 1f;

    	[Token(Token = "0x17000134")]
    	public IReadOnlyReactiveProperty<TimeSpan> RemainingTime
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x33A4E0 (shared generic reference-field getter, +0x18)
    		[Token(Token = "0x600074F")]
    		[Address(RVA = "0x33A4E0", Offset = "0x338EE0", VA = "0x18033A4E0")]
    		get
    		{
    			return _remainingTime;
    		}
    	}

    	[Token(Token = "0x17000135")]
    	public float Speed
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x398CF0 (`movss xmm0,[rcx+0x28]; ret`)
    		[Token(Token = "0x6000750")]
    		[Address(RVA = "0x398CF0", Offset = "0x3976F0", VA = "0x180398CF0")]
    		[CompilerGenerated]
    		private get
    		{
    			return _speed;
    		}
    		// RECONSTRUCTED | Confidence: A | RVA: 0x3A6BA0 (`movss [rcx+0x28],xmm1; ret`)
    		[Token(Token = "0x6000751")]
    		[Address(RVA = "0x3A6BA0", Offset = "0x3A55A0", VA = "0x1803A6BA0")]
    		[CompilerGenerated]
    		set
    		{
    			_speed = value;
    		}
    	}

    	[Token(Token = "0x17000136")]
    	private TimeSpan TimeSinceStartup
    	{
    		// RECONSTRUCTED | Confidence: A | RVA: 0x3A6BB0, 297 B
    		// `_useRealtime` (+0x10) selects icall Time::get_realtimeSinceStartup (0x2608CF0) or Time::get_time (0x2608CD0);
    		// the float is widened to double and passed to `TimeSpan.Interval(value, 1000)`, the inlined `TimeSpan.FromSeconds`.
    		[Token(Token = "0x6000752")]
    		[Address(RVA = "0x3A6BB0", Offset = "0x3A55B0", VA = "0x1803A6BB0")]
    		get
    		{
    			return TimeSpan.FromSeconds(_useRealtime ? UnityEngine.Time.realtimeSinceStartup : UnityEngine.Time.time);
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3A6CE0, 11 B (`mov [rcx+0x28],1f` = `_speed` initializer; `mov [rcx+0x10],dl`)
    	[Token(Token = "0x6000753")]
    	[Address(RVA = "0x3A6CE0", Offset = "0x3A56E0", VA = "0x1803A6CE0")]
    	public Timer(bool useRealtime)
    	{
    		_useRealtime = useRealtime;
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3A6CF0, 386 B
    	// Inlined `CancellationTokenSource.Cancel()` (disposed check -> ThrowObjectDisposedException, NotifyCancellation(false)),
    	// then Dispose and clear; then `ReactiveProperty<TimeSpan>.Dispose()` and clear, each behind its own null check.
    	[Token(Token = "0x6000754")]
    	[Address(RVA = "0x3A6CF0", Offset = "0x3A56F0", VA = "0x1803A6CF0", Slot = "4")]
    	public void Dispose()
    	{
    		if (_runningCts != null)
    		{
    			_runningCts.Cancel();
    			_runningCts.Dispose();
    			_runningCts = null;
    		}
    		if (_remainingTime != null)
    		{
    			_remainingTime.Dispose();
    			_remainingTime = null;
    		}
    	}

    	// RECONSTRUCTED | Confidence: A | RVA: 0x3A6E80, 471 B
    	// Same cancel/dispose/clear of `_runningCts` as Dispose, so a running StartAsync loop stops. The old property is
    	// disposed but not cleared before the new `ReactiveProperty<TimeSpan>(time)` is stored at +0x18.
    	[Token(Token = "0x6000755")]
    	[Address(RVA = "0x3A6E80", Offset = "0x3A5880", VA = "0x1803A6E80")]
    	public void SetTime(TimeSpan time)
    	{
    		if (_runningCts != null)
    		{
    			_runningCts.Cancel();
    			_runningCts.Dispose();
    			_runningCts = null;
    		}
    		_remainingTime?.Dispose();
    		_remainingTime = new ReactiveProperty<TimeSpan>(time);
    	}

    	// RECONSTRUCTED | Confidence: A (behaviour), B (exact block nesting of the loop) | RVA: 0x3A7060 (builder stub); MoveNext 0x3A7230, 2462 B
    	// 
    	// - No time set (+0x18 null): `Debug.LogWarning("Time is not set.")` and finish. Already running (+0x20 set): finish.
    	// - Otherwise `Speed = initialSpeed` (setter inlined), a fresh CTS at +0x20, and `<linkedCts>5__2` (+0x30) =
    	//   CreateLinkedTokenSource(_runningCts.Token, ct), disposed on every exit (using).
    	// - Each iteration stores `<previousTime>5__3` (+0x38), then awaits Cysharp `UniTask.Yield(linkedCts.Token, false)`
    	//   (0x180C760). A non-positive Speed (`comiss 0,[+0x28]; jae`) skips straight back to the loop head.
    	// - Elapsed time is scaled by Speed as double (inlined `TimeSpan * double`: NaN and overflow throws, Math.Round).
    	//   The result is subtracted from the current value.
    	// - A result that is still >= TimeSpan.Zero is published and the loop continues; +0x38 is zeroed on that path
    	//   only, hence grade B for the block nesting.
    	// - Otherwise Zero is published and `_runningCts` is disposed (without a null check) and cleared, ending the loop.
    	[Token(Token = "0x6000756")]
    	[Address(RVA = "0x3A7060", Offset = "0x3A5A60", VA = "0x1803A7060")]
    	[AsyncStateMachine(typeof(_003CStartAsync_003Ed__14))]
    	public async UniTask StartAsync(float initialSpeed = 1f, CancellationToken ct = default)
    	{
    		if (_remainingTime == null)
    		{
    			UnityEngine.Debug.LogWarning("Time is not set.");
    			return;
    		}
    		if (_runningCts != null)
    		{
    			return;
    		}
    		Speed = initialSpeed;
    		_runningCts = new CancellationTokenSource();
    		using (CancellationTokenSource linkedCts = CancellationTokenSource.CreateLinkedTokenSource(_runningCts.Token, ct))
    		{
    			while (true)
    			{
    				TimeSpan previousTime = TimeSinceStartup;
    				await UniTask.Yield(linkedCts.Token);
    				if (Speed <= 0f)
    				{
    					continue;
    				}
    				TimeSpan remaining = _remainingTime.Value - (TimeSinceStartup - previousTime) * Speed;
    				if (remaining >= TimeSpan.Zero)
    				{
    					_remainingTime.Value = remaining;
    					continue;
    				}
    				_remainingTime.Value = TimeSpan.Zero;
    				_runningCts.Dispose();
    				_runningCts = null;
    				break;
    			}
    		}
    	}
    }
}
