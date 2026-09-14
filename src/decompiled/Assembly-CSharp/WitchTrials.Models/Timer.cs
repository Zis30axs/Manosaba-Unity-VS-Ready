using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.CompilerServices;
using Il2CppDummyDll;
using UniRx;

namespace WitchTrials.Models;

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
	private float _speed;

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
		[Token(Token = "0x6000752")]
		[Address(RVA = "0x3A6BB0", Offset = "0x3A55B0", VA = "0x1803A6BB0")]
		get
		{
			return default(TimeSpan);
		}
	}

	[Token(Token = "0x6000753")]
	[Address(RVA = "0x3A6CE0", Offset = "0x3A56E0", VA = "0x1803A6CE0")]
	public Timer(bool useRealtime)
	{
	}

	[Token(Token = "0x6000754")]
	[Address(RVA = "0x3A6CF0", Offset = "0x3A56F0", VA = "0x1803A6CF0", Slot = "4")]
	public void Dispose()
	{
	}

	[Token(Token = "0x6000755")]
	[Address(RVA = "0x3A6E80", Offset = "0x3A5880", VA = "0x1803A6E80")]
	public void SetTime(TimeSpan time)
	{
	}

	[Token(Token = "0x6000756")]
	[Address(RVA = "0x3A7060", Offset = "0x3A5A60", VA = "0x1803A7060")]
	[AsyncStateMachine(typeof(_003CStartAsync_003Ed__14))]
	public UniTask StartAsync(float initialSpeed = 1f, CancellationToken ct = default)
	{
		return default(UniTask);
	}
}
