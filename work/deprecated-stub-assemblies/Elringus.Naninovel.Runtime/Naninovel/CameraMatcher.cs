using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x2000121")]
    public abstract class CameraMatcher : IDisposable
    {
    	[StructLayout(3)]
    	[Token(Token = "0x2000122")]
    	[CompilerGenerated]
    	private struct _003CMatchInLoop_003Ed__25
    {
    		[Token(Token = "0x4000490")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000491")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x8")]
    		public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000492")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x10")]
    		public CameraMatcher _003C_003E4__this;

    		[Token(Token = "0x4000493")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    		public CancellationToken cancellationToken;

    		[Token(Token = "0x4000494")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    		private YieldAwaitable.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x600055E")]
    		[Address(RVA = "0x50FF90", Offset = "0x50E990", VA = "0x18050FF90", Slot = "4")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x600055F")]
    		[Address(RVA = "0x30C000", Offset = "0x30AA00", VA = "0x18030C000", Slot = "5")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x400048B")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x18")]
    	private readonly UnityEngine.Object target;

    	[Token(Token = "0x400048C")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x20")]
    	private readonly ICameraManager cameraManager;

    	[Token(Token = "0x400048D")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x28")]
    	private CancellationTokenSource cts;

    	[Token(Token = "0x400048E")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x30")]
    	private Vector2 previousCameraSize;

    	[Token(Token = "0x400048F")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x38")]
    	private Vector2 previousReferenceSize;

    	[Token(Token = "0x170000DE")]
    	public AspectMatchMode MatchMode
    	{
    		[Token(Token = "0x600054E")]
    		[Address(RVA = "0x331650", Offset = "0x330050", VA = "0x180331650")]
    		[CompilerGenerated]
    		get
    		{
    			return default(AspectMatchMode);
    		}
    		[Token(Token = "0x600054F")]
    		[Address(RVA = "0x397FF0", Offset = "0x3969F0", VA = "0x180397FF0")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x170000DF")]
    	public float CustomMatchRatio
    	{
    		[Token(Token = "0x6000550")]
    		[Address(RVA = "0x3686E0", Offset = "0x3670E0", VA = "0x1803686E0")]
    		[CompilerGenerated]
    		get
    		{
    			return default(float);
    		}
    		[Token(Token = "0x6000551")]
    		[Address(RVA = "0x39F320", Offset = "0x39DD20", VA = "0x18039F320")]
    		[CompilerGenerated]
    		set
    		{
    		}
    	}

    	[Token(Token = "0x170000E0")]
    	public bool Matching
    	{
    		[Token(Token = "0x6000552")]
    		[Address(RVA = "0x48D6E0", Offset = "0x48C0E0", VA = "0x18048D6E0")]
    		get
    		{
    			return default(bool);
    		}
    	}

    	[Token(Token = "0x6000553")]
    	[Address(RVA = "0x48D6F0", Offset = "0x48C0F0", VA = "0x18048D6F0")]
    	protected CameraMatcher(ICameraManager cameraManager = default, UnityEngine.Object target = default)
    	{
    	}

    	[Token(Token = "0x6000554")]
    	[Address(RVA = "0x48D7A0", Offset = "0x48C1A0", VA = "0x18048D7A0", Slot = "5")]
    	public virtual void Start()
    	{
    	}

    	[Token(Token = "0x6000555")]
    	[Address(RVA = "0x48DAE0", Offset = "0x48C4E0", VA = "0x18048DAE0", Slot = "6")]
    	public virtual void Stop()
    	{
    	}

    	[Token(Token = "0x6000556")]
    	[Address(RVA = "0x48DBC0", Offset = "0x48C5C0", VA = "0x18048DBC0", Slot = "4")]
    	public void Dispose()
    	{
    	}

    	[Token(Token = "0x6000557")]
    	[Address(RVA = "0x48DBE0", Offset = "0x48C5E0", VA = "0x18048DBE0", Slot = "7")]
    	protected virtual bool IsMatchRequired(Vector2 cameraSize, Vector2 referenceSize)
    	{
    		return default(bool);
    	}

    	[Token(Token = "0x6000558")]
    	protected abstract bool TryGetReferenceSize(out Vector2 referenceSize);

    	[Token(Token = "0x6000559")]
    	[Address(RVA = "0x48DC60", Offset = "0x48C660", VA = "0x18048DC60", Slot = "9")]
    	protected virtual bool TryGetCameraSize(out Vector2 cameraSize)
    	{
    		cameraSize = default;
    		cameraSize = default;
    		return default(bool);
    	}

    	[Token(Token = "0x600055A")]
    	protected abstract void ApplyScale(float scaleFactor);

    	[Token(Token = "0x600055B")]
    	[Address(RVA = "0x48DE30", Offset = "0x48C830", VA = "0x18048DE30", Slot = "11")]
    	protected virtual void Match(Vector2 cameraSize, Vector2 referenceSize)
    	{
    	}

    	[Token(Token = "0x600055C")]
    	[Address(RVA = "0x48DEA0", Offset = "0x48C8A0", VA = "0x18048DEA0", Slot = "12")]
    	protected virtual float EvaluateScaleFactor(Vector2 cameraSize, Vector2 contentSize)
    	{
    		return default(float);
    	}

    	[Token(Token = "0x600055D")]
    	[Address(RVA = "0x48E060", Offset = "0x48CA60", VA = "0x18048E060")]
    	[AsyncStateMachine(typeof(_003CMatchInLoop_003Ed__25))]
    	private UniTaskVoid MatchInLoop(CancellationToken cancellationToken)
    	{
    		return default(UniTaskVoid);
    	}
    }
}
