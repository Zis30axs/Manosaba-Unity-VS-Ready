using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Il2CppDummyDll;
using Naninovel.Async.CompilerServices;
using UnityEngine;

namespace Naninovel
{
    [Token(Token = "0x20002CE")]
    public class Tweener<TTweenValue> : ITweener<TTweenValue> where TTweenValue : struct, ITweenValue
    {
    	[StructLayout(3)]
    	[Token(Token = "0x20002CF")]
    	[CompilerGenerated]
    	private struct _003CTweenAsync_003Ed__16
    {
    		[Token(Token = "0x4000B17")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000B18")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000B19")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public Tweener<TTweenValue> _003C_003E4__this;

    		[Token(Token = "0x4000B1A")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncToken token;

    		[Token(Token = "0x4000B1B")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private Guid _003CcurrentRunGuid_003E5__2;

    		[Token(Token = "0x4000B1C")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private YieldAwaitable.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000D86")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000D87")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[StructLayout(3)]
    	[Token(Token = "0x20002D0")]
    	[CompilerGenerated]
    	private struct _003CTweenAsyncAndForget_003Ed__17
    {
    		[Token(Token = "0x4000B1D")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public int _003C_003E1__state;

    		[Token(Token = "0x4000B1E")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncUniTaskVoidMethodBuilder _003C_003Et__builder;

    		[Token(Token = "0x4000B1F")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public Tweener<TTweenValue> _003C_003E4__this;

    		[Token(Token = "0x4000B20")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		public AsyncToken token;

    		[Token(Token = "0x4000B21")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private Guid _003CcurrentRunGuid_003E5__2;

    		[Token(Token = "0x4000B22")]
    		[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    		private YieldAwaitable.Awaiter _003C_003Eu__1;

    		[Token(Token = "0x6000D88")]
    		private void MoveNext()
    		{
    		}

    		[Token(Token = "0x6000D89")]
    		[DebuggerHidden]
    		private void SetStateMachine(IAsyncStateMachine stateMachine)
    		{
    		}
    	}

    	[Token(Token = "0x4000B13")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private float elapsedTime;

    	[Token(Token = "0x4000B14")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private Guid lastRunGuid;

    	[Token(Token = "0x4000B15")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private UnityEngine.Object target;

    	[Token(Token = "0x4000B16")]
    	[Il2CppDummyDll.FieldOffset(Offset = "0x0")]
    	private bool targetSpecified;

    	[Token(Token = "0x170001E3")]
    	public TTweenValue Tween
    	{
    		[Token(Token = "0x6000D78")]
    		[CompilerGenerated]
    		get
    		{
    			return default(TTweenValue);
    		}
    		[Token(Token = "0x6000D79")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x170001E4")]
    	public bool Running
    	{
    		[Token(Token = "0x6000D7A")]
    		[CompilerGenerated]
    		get
    		{
    			return default(bool);
    		}
    		[Token(Token = "0x6000D7B")]
    		[CompilerGenerated]
    		private set
    		{
    		}
    	}

    	[Token(Token = "0x6000D7C")]
    	public void Run(TTweenValue tween, [Optional] AsyncToken token, [Optional] UnityEngine.Object target)
    	{
    	}

    	[Token(Token = "0x6000D7D")]
    	public UniTask RunAwaitable(TTweenValue tween, [Optional] AsyncToken token, [Optional] UnityEngine.Object target)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000D7E")]
    	public void Stop()
    	{
    	}

    	[Token(Token = "0x6000D7F")]
    	public void CompleteInstantly()
    	{
    	}

    	[Token(Token = "0x6000D80")]
    	[AsyncStateMachine(typeof(Tweener<>._003CTweenAsync_003Ed__16))]
    	protected UniTask TweenAsync([Optional] AsyncToken token)
    	{
    		return default(UniTask);
    	}

    	[Token(Token = "0x6000D81")]
    	[AsyncStateMachine(typeof(Tweener<>._003CTweenAsyncAndForget_003Ed__17))]
    	protected UniTaskVoid TweenAsyncAndForget([Optional] AsyncToken token)
    	{
    		return default(UniTaskVoid);
    	}

    	[Token(Token = "0x6000D82")]
    	private void PrepareTween()
    	{
    	}

    	[Token(Token = "0x6000D83")]
    	private void PerformTween()
    	{
    	}

    	[Token(Token = "0x6000D84")]
    	private void FinishTween()
    	{
    	}

    	[Token(Token = "0x6000D85")]
    	public Tweener()
    	{
    	}
    }
}
