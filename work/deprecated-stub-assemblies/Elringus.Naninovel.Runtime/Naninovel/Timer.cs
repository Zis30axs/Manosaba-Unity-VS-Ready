using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x20002C2")]
    public class Timer
    {
    	[StructLayout(3)]
    	[Token(Token = "0x20002C3")]
    	[CompilerGenerated]
    	private struct _003CWaitAndComplete_003Ed__26
    {
    		[Token(Token = "0x4000ACC")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000ACD")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000ACE")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public Timer _003C_003E4__this;

    		[Token(Token = "0x4000ACF")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public AsyncToken token;

    		[Token(Token = "0x4000AD0")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private Guid _003CcurrentRunGuid_003E5__2;

    		[Token(Token = "0x4000AD1")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private float _003CstartTime_003E5__3;

    		[Token(Token = "0x4000AD2")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private YieldAwaitable.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000D3D")]
    		[Address(RVA = "0x5431C0", Offset = "0x541BC0", VA = "0x1805431C0", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000D3E")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20002C4")]
    	[CompilerGenerated]
    	private struct _003CWaitAndLoop_003Ed__27
    {
    		[Token(Token = "0x4000AD3")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000AD4")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000AD5")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public Timer _003C_003E4__this;

    		[Token(Token = "0x4000AD6")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public AsyncToken token;

    		[Token(Token = "0x4000AD7")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    		private Guid _003CcurrentRunGuid_003E5__2;

    		[Token(Token = "0x4000AD8")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    		private float _003CstartTime_003E5__3;

    		[Token(Token = "0x4000AD9")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x40")]
    		private YieldAwaitable.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000D3F")]
    		[Address(RVA = "0x543550", Offset = "0x541F50", VA = "0x180543550", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000D40")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000AC7")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    	private readonly Action onLoop;

    	[Token(Token = "0x4000AC8")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	private readonly Action onCompleted;

    	[Token(Token = "0x4000AC9")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	private UnityEngine.Object target;

    	[Token(Token = "0x4000ACA")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	private bool targetSpecified;

    	[Token(Token = "0x4000ACB")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x34")]
    	private Guid lastRunGuid;

    	[Token(Token = "0x170001D9")]
    	public bool Running
    	{
    		[Token(Token = "0x6000D2C")]
    		[Address(RVA = "0x44D320", Offset = "0x44BD20", VA = "0x18044D320")]
    		[CompilerGenerated]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x6000D2D")]
    		[Address(RVA = "0x30C130", Offset = "0x30AB30", VA = "0x18030C130")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x170001DA")]
    	public bool Loop
    	{
    		[Token(Token = "0x6000D2E")]
    		[Address(RVA = "0x542940", Offset = "0x541340", VA = "0x180542940")]
    		[CompilerGenerated]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x6000D2F")]
    		[Address(RVA = "0x542950", Offset = "0x541350", VA = "0x180542950")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x170001DB")]
    	public bool TimeScaleIgnored
    	{
    		[Token(Token = "0x6000D30")]
    		[Address(RVA = "0x542960", Offset = "0x541360", VA = "0x180542960")]
    		[CompilerGenerated]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x6000D31")]
    		[Address(RVA = "0x542970", Offset = "0x541370", VA = "0x180542970")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x170001DC")]
    	public float Duration
    	{
    		[Token(Token = "0x6000D32")]
    		[Address(RVA = "0x3686E0", Offset = "0x3670E0", VA = "0x1803686E0")]
    		[CompilerGenerated]
    		get
    		{
    			return default(float);
    		}
    		[Token(Token = "0x6000D33")]
    		[Address(RVA = "0x39F320", Offset = "0x39DD20", VA = "0x18039F320")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x6000D34")]
    	[Address(RVA = "0x542980", Offset = "0x541380", VA = "0x180542980")]
    	public Timer(float duration = 0f, bool loop = false, bool ignoreTimeScale = false, Action onCompleted = default, Action onLoop = default)
    	{
    	}

    	[Token(Token = "0x6000D35")]
    	[Address(RVA = "0x542B50", Offset = "0x541550", VA = "0x180542B50")]
    	public void Run(float duration, bool loop = false, bool ignoreTimeScale = false, AsyncToken token = default, UnityEngine.Object target = default)
    	{
    	}

    	[Token(Token = "0x6000D36")]
    	[Address(RVA = "0x542CF0", Offset = "0x5416F0", VA = "0x180542CF0")]
    	public void Run(AsyncToken token = default, UnityEngine.Object target = default)
    	{
    	}

    	[Token(Token = "0x6000D37")]
    	[Address(RVA = "0x542D30", Offset = "0x541730", VA = "0x180542D30")]
    	public void Stop()
    	{
    	}

    	[Token(Token = "0x6000D38")]
    	[Address(RVA = "0x542D80", Offset = "0x541780", VA = "0x180542D80")]
    	public void CompleteInstantly()
    	{
    	}

    	[Token(Token = "0x6000D39")]
    	[Address(RVA = "0x542DF0", Offset = "0x5417F0", VA = "0x180542DF0", Slot = "4")]
    	[AsyncStateMachine(typeof(_003CWaitAndComplete_003Ed__26))]
    	protected virtual UniTaskVoid WaitAndComplete(AsyncToken token = default)
    	{
    		return default(UniTaskVoid);
    	}

    	[Token(Token = "0x6000D3A")]
    	[Address(RVA = "0x542F70", Offset = "0x541970", VA = "0x180542F70", Slot = "5")]
    	[AsyncStateMachine(typeof(_003CWaitAndLoop_003Ed__27))]
    	protected virtual UniTaskVoid WaitAndLoop(AsyncToken token = default)
    	{
    		return default(UniTaskVoid);
    	}

    	[Token(Token = "0x6000D3B")]
    	[Address(RVA = "0x5430F0", Offset = "0x541AF0", VA = "0x1805430F0")]
    	private float GetTime()
    	{
    		return default(float);
    	}

    	[Token(Token = "0x6000D3C")]
    	[Address(RVA = "0x543190", Offset = "0x541B90", VA = "0x180543190")]
    	private bool WaitedEnough(float startTime)
    	{
    		return default(bool);
    	}
    }
}
